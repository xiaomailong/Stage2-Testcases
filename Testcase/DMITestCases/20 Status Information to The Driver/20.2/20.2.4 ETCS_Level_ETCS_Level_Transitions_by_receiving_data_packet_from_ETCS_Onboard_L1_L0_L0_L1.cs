using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BT_Tools;
using BT_CSB_Tools;
using BT_CSB_Tools.Logging;
using BT_CSB_Tools.Utils.Xml;
using BT_CSB_Tools.SignalPoolGenerator.Signals;
using BT_CSB_Tools.SignalPoolGenerator.Signals.MwtSignal;
using BT_CSB_Tools.SignalPoolGenerator.Signals.MwtSignal.Misc;
using BT_CSB_Tools.SignalPoolGenerator.Signals.PdSignal;
using BT_CSB_Tools.SignalPoolGenerator.Signals.PdSignal.Misc;
using CL345;
using Testcase.Telegrams.DMItoEVC;
using Testcase.Telegrams.EVCtoDMI;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 20.2.4 ETCS Level: ETCS Level Transitions by receiving data packet from ETCS Onboard (L1->L0, L0->L1)
    /// TC-ID: 15.2.4
    /// 
    /// This test case verifes a replaced symbol which should be replaced immediately after driver acknowledged level transition acknowledgement symbol.
    /// 
    /// Tested Requirements:
    /// MMI_gen 9431 (partly: LE07 is used, LE06 is replace respectively LE07); MMI_gen 7025 (partly: 2nd bullet, #4); MMI_gen 1310 (partly:LE07);  MMI_gen 9430 (partly:LE06, LE10); MMI_gen 11470 (partly: Bit #6);
    /// 
    /// Scenario:
    /// Activate Cabin A.Perform SoM in SR mode, Level 1.Drive the train forward pass BG
    /// 1.Then, verifie the display information.BG1: Packet 41Press an acknowledment at area C
    /// 1.Then, verifie the display information.Drive the train forward pass BG
    /// 3.Then, verifie the display information.BG2: Pack 12, 21, 27 and 41
    /// 
    /// Used files:
    /// 15_2_4.tdg
    /// </summary>
    public class TC_ID_15_2_4_ETCS_Level : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:

            // Call the TestCaseBase PreExecution
            base.PreExecution();

            // System is power ON.
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in FS mode, Level 1.

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint
            
            /*
            Test Step 1
            Action: Acivate cabin A
            Expected Result: DMI displays Driver ID window
            */
            // Call generic Check Results Method
            DmiActions.Start_ATP();
            DmiActions.Activate_Cabin_1(this);
            DmiActions.Set_Driver_ID(this, "1234");

            DmiExpectedResults.Driver_ID_window_displayed(this);


            /*
            Test Step 2
            Action: Perform SoM in SR mode, Level 1
            Expected Result: DMI displays in SR mode Level 1
            */
            // Call generic Action Method
            DmiActions.Perform_SoM_in_SR_mode_Level_1(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in SR mode, Level 1.");

            /*
            Test Step 3
            Action: Drive the train forward pass BG1
            Expected Result: DMI displays symbol LE07 in area C1 with flashing yellow frame.Verify the following information,(1)    Use the log file to confirm that DMI receives packet information EVC-8 with the following variables,MMI_Q_TEXT = 276MMI_Q_TEXT_CRITIRIA = 1MMI_N_TEXT = 1MMI_X_TEXT = 0(2)    Use the log file to confirm that DMI sends out packet [MMI_DRIVER_ACTION (EVC-152)] with the value of variable MMI_M_DRIVER_ACTION refer to sequence below,a)   MMI_M_DRIVER_ACTION = 6 (Ack level 0)
            Test Step Comment: (1) MMI_gen 7025 (partly: 2nd bullet, #4, Ack Level 0 transition); MMI_gen 1310 (partly:LE07); MMI_gen 9431 (partly: LE07);(2) MMI_gen 11470 (partly: Bit #6);
            */
            // Spec says 276 (LE06 etc) but 257 is LE07
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 257;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.PlainTextMessage = "\0x00";
            EVC8_MMIDriverMessage.Send();

            EVC152_MMIDriverAction.Check_MMI_M_DRIVER_ACTION = EVC152_MMIDriverAction.MMI_M_DRIVER_ACTION.Level0Ack;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays symbol LE07 in sub-area C1, with a yellow flashing frame.");
            /*
            Test Step 4
            Action: Press an area C1 for acknowledgement
            Expected Result: Verify the following information,The symbol LE06 is display in area C1 instead of symbol LE07 immediately.Use the log file to confirm that DMI receives packet information EVC-8 with the following variables,MMI_Q_TEXT = 276MMI_Q_TEXT_CRITIRIA = 3MMI_N_TEXT = 1MMI_X_TEXT = 0
            Test Step Comment: (1) MMI_gen 9431          (partly: The symbol LE06 is replace respectively LE07); MMI_gen 9430          (partly:LE06);(2) MMI_gen 7025 (partly: 2nd bullet, #4, non-Ack Level 0 transition);
            */
            DmiActions.ShowInstruction(this, @"Press in area C1 to acknowledge");

            EVC8_MMIDriverMessage.MMI_Q_TEXT = 276;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.PlainTextMessage = "\0x00";
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI immediately removes symbol LE07 and displays symbol LE06 in sub-area C1.");

            /*
            Test Step 5
            Action: Drive the train pass a distance to level transition
            Expected Result: Level transition from Level 1 to Level 0.DMI displays symbol LE01 in area C8
            */
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Level = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_LEVEL.L0;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI changes to Level 0 and displays symbol LE01 in sub-area C1.");

            /*
            Test Step 6
            Action: Drive the train forward pass BG3
            Expected Result: Verify the following information,The symbol LE10 is display in area C1.Use the log file to confirm that DMI receives packet information EVC-8 with the following variables,MMI_Q_TEXT = 276MMI_Q_TEXT_CRITIRIA = 3MMI_N_TEXT = 1MMI_X_TEXT = 1
            Test Step Comment: (1) MMI_gen 9430          (partly:LE10);(2) MMI_gen 7025 (partly: 2nd bullet, #4, non-Ack Level 1 transition);
            */
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 276;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.PlainTextMessage = "\0x01";
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays symbol LE10 in sub-area C1.");

            /*
            Test Step 7
            Action: Drive the train pass a distance to level transition
            Expected Result: Level transition from Level 0 to Level 1.DMI displays symbol LE03 in area C8
            */
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Level = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_LEVEL.L1;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI changes to Level 1 and displays symbol LE03 in sub-area C1.");

            /*
            Test Step 8
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
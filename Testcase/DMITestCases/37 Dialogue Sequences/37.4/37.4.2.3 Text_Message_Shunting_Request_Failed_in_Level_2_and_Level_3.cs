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
using Testcase.Telegrams.EVCtoDMI;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 37.4.2.3 Text Message “Shunting Request Failed” in Level 2 and Level 3
    /// TC-ID: 34.4.2.3
    /// 
    /// This test case verifies the display of text message “Shunting request failed” in ETCS level 2 and 3 when RBC doesn’t reply the “Shunting Authorised” to Onboard within the fixed waiting time from the last sending of “Request for Shunting”.
    /// 
    /// Tested Requirements:
    /// MMI_gen 11915 (partly: SH request failed); MMI_gen 134 (partly: E5); MMI_gen 9151;
    /// 
    /// Scenario:
    ///  1.Enter SH mode, level 
    /// 2.Then, verify the display of text message “Shunting request failed” in sub-area E5.2.Restart test system.
    /// 3.Enter SH mode, level 
    /// 3.Then, verify the display of text message “Shunting request failed” in sub-area E5.
    /// 
    /// Used files:
    /// 34_4_2_3.utt
    /// </summary>
    public class Text_Message_Shunting_Request_Failed_in_Level_2_and_Level_3 : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:

            // Call the TestCaseBase PreExecution
            base.PreExecution();
            // Test system is power onCabin is activatedSoM is performed in SR mode, level 2
            EVC0_MMIStartATP.Evc0Type = EVC0_MMIStartATP.EVC0Type.GoToIdle;
            EVC0_MMIStartATP.Send();

            // Set train running number, cab 1 active, and other defaults
            DmiActions.Activate_Cabin_1(this);

            // Set driver ID
            DmiActions.Set_Driver_ID(this, "1234");

            // Set to level 1 and SR mode
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Level = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_LEVEL.L2;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.StaffResponsible;

            // Enable standard buttons including Start, and display Default window.
            DmiActions.FinishedSoM_Default_Window(this);
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays is in SH mode, level 3
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in SR mode, Level 3.");

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint

            /*
            Test Step 1
            Action: Enter SH mode by performing the procedure below,Press ‘Main’ buttonPress and hold ‘Shunting’ button at least 2 seconds.Release ‘Shunting’ button
            Expected Result: DMI displays Main window.(1)    While the Main window is display with hourglass symbol (ST05), the close button is disabled.(2)   The text message ‘Shunting request failed’ is display in sub-area E5 within 2 minutes
            Test Step Comment: Level 2:(1) MMI_gen 9151;(2) MMI_gen 11915 (partly: SH request failed); MMI_gen 134 (partly: E5);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press ‘Main’ button. Press and hold ‘Shunting’ button at least 2 seconds.Release ‘Shunting’ button");

            EVC8_MMIDriverMessage.MMI_Q_TEXT = 716;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;

            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI is in the entry state of ‘ST05’." + Environment.NewLine +
                                "2. The hourglass symbol ST05 is displayed vertically aligned in the center of the window title area." + Environment.NewLine +
                                "3. The hourglass symbol ST05 moves to the right every second." + Environment.NewLine +
                                "4. When the hourglass symbol ST05 has reached the edge of the window title area it is re-displayed on the lefthand side of the window title area and continues to move to the right." + Environment.NewLine +
                                "5. All buttons and the ‘Close’ button are disabled." + Environment.NewLine +
                                "6. ‘Close’ button NA12 is displayed disabled in area G.");

            System.Threading.Thread.Sleep(10000);

            EVC8_MMIDriverMessage.MMI_Q_TEXT = 292;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the main window with text message ‘Shunting request failed’ in sub-area E5.");

            /*
            Test Step 2
            Action: Re-validate the step1 by re-starting OTE Simulator and starting the precondition with ETCS level 3
            Expected Result: See the expected results at Step 1
            Test Step Comment: Level 3:(1) MMI_gen 9151;(2) MMI_gen 11915 (partly: SH request failed); MMI_gen 134 (partly: E5);
            */
            // ?? Is this sufficient...
            // Restart
            EVC0_MMIStartATP.Evc0Type = EVC0_MMIStartATP.EVC0Type.GoToIdle;
            EVC0_MMIStartATP.Send();

            // Set train running number, cab 1 active, and other defaults
            DmiActions.Activate_Cabin_1(this);

            // Set driver ID
            DmiActions.Set_Driver_ID(this, "1234");

            // Set to level 1 and SH mode
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Level = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_LEVEL.L3;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.StaffResponsible;

            DmiActions.ShowInstruction(this, @"Press ‘Main’ button. Press and hold ‘Shunting’ button at least 2 seconds.Release ‘Shunting’ button");

            EVC8_MMIDriverMessage.MMI_Q_TEXT = 716;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;

            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI is in the entry state of ‘ST05’." + Environment.NewLine +
                                "2. The hourglass symbol ST05 is displayed vertically aligned in the center of the window title area." + Environment.NewLine +
                                "3. The hourglass symbol ST05 moves to the right every second." + Environment.NewLine +
                                "4. When the hourglass symbol ST05 has reached the edge of the window title area it is re-displayed on the lefthand side of the window title area and continues to move to the right." + Environment.NewLine +
                                "5. All buttons and the ‘Close’ button are disabled." + Environment.NewLine +
                                "6. ‘Close’ button NA12 is displayed disabled in area G.");

            System.Threading.Thread.Sleep(10000);

            EVC8_MMIDriverMessage.MMI_Q_TEXT = 292;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the main window with text message ‘Shunting request failed’ in sub-area E5.");

            /*
            Test Step 3
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
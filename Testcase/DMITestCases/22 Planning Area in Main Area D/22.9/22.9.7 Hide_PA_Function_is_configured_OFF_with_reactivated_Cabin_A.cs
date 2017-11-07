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
    /// 22.9.7 Hide PA Function is configured ‘OFF’ with reactivated Cabin A
    /// TC-ID: 17.9.7
    /// 
    /// This test case verifies that the default state of Planning area is shown/hidden refer to ‘OFF’ configuration of Hide PA function included with correctness of displayed Planning area when activate ‘Hide’/’Show’ buttons after cabin re-activation.
    /// 
    /// Tested Requirements:
    /// MMI_gen 7340; MMI_gen 2996 (partly: OFF);
    /// 
    /// Scenario:
    /// Activate Cabin A.Perform SoM in SR mode, Level 1.Drive the train forward pass BG1 at position 100m.BG1: Packet 12, 21 and 27 (mode changes to FS)Stop the train at position 300.De-activate cabin A and activate cabin A again.Drive the train forward pass BG2 at position 600m. Then, verify that PA is hidden by default.BG2: packet 12, 21 and 27 (mode changes to FS)
    /// 
    /// Used files:
    /// 17_9_7.tdg
    /// </summary>
    public class TC_ID_17_9_7_Hide_PA_Function_is_configured_OFF_with_reactivated_Cabin_A : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // Set the following tags name in configuration file (See the instruction in Appendix 1)
            // HIDE_PA_FUNCTION = 1 (‘OFF’ state)HIDE_PA_SR_MODE = 0 (PA will not show in SR mode)System is power OFF.

            // Call the TestCaseBase PreExecution
            base.PreExecution();
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
            TraceInfo("This test case requires an ATP configuration change - " +
                      "See Precondition requirements. If this is not done manually, the test may fail!");

            /*
            Test Step 1
            Action: Power On the system
            Expected Result: The DMI displays the default window
            */
            DmiActions.Start_ATP();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Default window.");

            /*
            Test Step 2
            Action: Activate cabin A and Perform SoM to SR mode, Level 1
            Expected Result: The DMI displays in SR mode, level 1
            */
            // Tested elsewhere, force SoM
            DmiActions.Complete_SoM_L1_SR(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in SR mode, Level 1.");

            /*
            Test Step 3
            Action: Drive the train forward with speed = 40 km/h pass BG1
            Expected Result: DMI displays in FS mode, Level 1.There is no PA display on DMI
            Test Step Comment: (1) MMI_gen  2996 (partly: OFF);
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 10;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN = 10000;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in FS mode, Level 1." + Environment.NewLine +
                                "2. The Planning Area is not displayed in area D.");
            
            /*
            Test Step 4
            Action: Press the main area D
            Expected Result: The DMI displays the PA.The Hide PA button is appeared on the area D of the DMI
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press in the main area D");

            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN = 12000;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The Planning Area is displayed in area D." + Environment.NewLine +
                                "2. The ‘Hide PA’ button is displayed in area D.");

            /*
            Test Step 5
            Action: Stop the train. Then, deactivate cabin A
            Expected Result: The train is at standstill.DMI is displays in SB mode
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 10;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN = 35000;

            DmiActions.Deactivate_Cabin(this);

            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.StandBy;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in SB mode." + Environment.NewLine +
                                "2. The speed displayed is ‘0’.");

            /*
            Test Step 6
            Action: Activate cabin A and Perform SoM to SR mode, Level 1
            Expected Result: The DMI displays in SR mode, level 1
            */
            DmiActions.Activate_Cabin_1(this);
            DmiActions.Set_Driver_ID(this, "1234");
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Level = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_LEVEL.L1;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.StaffResponsible;
            DmiActions.Finished_SoM_Default_Window(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in SR mode, Level 1.");

            /*
            Test Step 7
            Action: Drive the train forward with speed = 40 km/h pass BG2
            Expected Result: The DMI shows “Entering FS” message.There is no PA display on DMI
            Test Step Comment: (1) MMI_gen 2996 (partly: OFF); MMI_gen 7340;
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 10;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN = 60000;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 274;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the message ‘Entering FS’." + Environment.NewLine +
                                "2. The Planning Area is not displayed in area D.");

            /*
            Test Step 8
            Action: Press the area D
            Expected Result: The DMI displays the PA.The Hide PA button is appeared on  the area D of the DMI
            */
            DmiActions.ShowInstruction(this, @"Press in the main area D");

            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN = 65000;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The Planning Area is displayed in area D." + Environment.NewLine +
                                "2. The ‘Hide PA’ button is displayed in area D.");
            
            /*
            Test Step 9
            Action: End of test
            Expected Result: 
            */
            
            return GlobalTestResult;
        }
    }
}
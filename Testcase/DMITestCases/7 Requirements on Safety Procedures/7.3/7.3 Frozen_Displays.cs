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
    /// 7.3 Frozen Display(s)
    /// TC-ID: 2.3
    /// 
    /// This test case verifies a fault detection of the display unit (Hardware) by the clock flashing-colons on the ‘Default’ window which complies with [MMI-ETCS-gen].
    /// 
    /// Tested Requirements:
    /// MMI_gen 3204-1 (THR); MMI_gen 3852 (partly: flashing colons);
    /// 
    /// Scenario:
    /// Activate cabin A. Perform SoM to SR mode, level 
    /// 1.Press ‘Special menu’ button and then close the Special window.Press ‘Settings menu’ button. After that close the Settings windowPress ‘Main menu’ button. Then, close the ‘Main’ menu windowVerify the flashing colons for local time at sub-area G13.
    /// 
    /// Used files:
    /// N/A
    /// </summary>
    public class TC_ID_2_3_Frozen_Displays : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:

            // Call the TestCaseBase PreExecution
            base.PreExecution();
            // Test system is power on.            
            EVC0_MMIStartATP.Evc0Type = EVC0_MMIStartATP.EVC0Type.GoToIdle;
            EVC0_MMIStartATP.Send();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in SR mode, level 1
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in SR mode, Level 1.");

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint

            /*
            Test Step 1
            Action: Activate cabin A. Then, perform SoM to SR mode, level 1
            Expected Result: DMI displays in SR mode, level 1.Verify the following information,The local time is displayed in form of ‘hh:mm:ss’ with flashing colons at sub-area G13
            Test Step Comment: (1) MMI_gen 3204-1 (THR); MMI_gen 3852 (partly: flashing colons);
            */
            DmiActions.Activate_Cabin_1(this);

            // Set driver ID
            DmiActions.Set_Driver_ID(this, "1234");

            // Set to level 1 and SR mode
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Level = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_LEVEL.L1;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.FullSupervision;

            // Enable standard buttons including Start, and display Default window.
            DmiActions.FinishedSoM_Default_Window(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in SR mode, Level 1." + Environment.NewLine +
                                "2. The local time is displayed in ‘hh:mm:ss’ format with flashing colons at sub-area G13");

            /*
            Test Step 2
            Action: Perform the following procedure,Press ‘Spec’ button.Press ‘Close’ button on Spedical window
            Expected Result: The Default window is displayed.Verify the following information,The local time is displayed in form of ‘hh:mm:ss’ with flashing colons at sub-area G13
            Test Step Comment: (1) MMI_gen 3204-1 (THR); MMI_gen 3852 (partly: flashing colons);
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Spec’ button. Press ‘Close’ button in the Special window");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Default window." + Environment.NewLine +
                                "2. The local time is displayed in ‘hh:mm:ss’ format with flashing colons at sub-area G13");

            /*
            Test Step 3
            Action: Perform the following procedure,Press ‘Settings’ button.Press ‘Close’ button on Settings window
            Expected Result: The Default window is displayed.Verify the following information,The local time is displayed in form of ‘hh:mm:ss’ with flashing colons at sub-area G13
            Test Step Comment: (1) MMI_gen 3204-1 (THR); MMI_gen 3852 (partly: flashing colons);
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Main’ button. Press ‘Close’ button in the Main window");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Default window." + Environment.NewLine +
                                "2. The local time is displayed in ‘hh:mm:ss’ format with flashing colons at sub-area G13");
                      
            /*
            Test Step 4
            Action: Perform the following procedure,Press ‘Main’ button.Press ‘Close’ button on Main window
            Expected Result: The Default window is displayed.Verify the following information,The local time is displayed in form of ‘hh:mm:ss’ with flashing colons at sub-area G13
            Test Step Comment: (1) MMI_gen 3204-1 (THR); MMI_gen 3852 (partly: flashing colons);
            */
            // Call generic Check Results Method
            DmiExpectedResults
                .The_Default_window_is_displayed_Verify_the_following_information_The_local_time_is_displayed_in_form_of_hhmmss_with_flashing_colons_at_sub_area_G13(this);


            /*
            Test Step 5
            Action: End of test
            Expected Result: 
            */


            return GlobalTestResult;
        }
    }
}
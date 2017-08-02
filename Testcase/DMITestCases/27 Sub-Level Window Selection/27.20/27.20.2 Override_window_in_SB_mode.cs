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

namespace Testcase.DMITestCases
{
    /// <summary>
    /// 27.20.2 Override window in SB mode
    /// TC-ID: 22.20.2
    /// 
    /// This test case verifies the state of ‘EOA’ buttion which enabled in SB mode Level2/3 and verifies the correctness of enable/disable state when DMI received packet information EVC-30.
    /// 
    /// Tested Requirements:
    /// MMI_gen 8415 (partly: touch screen, label “EOA”); MMI_gen 11225;
    /// 
    /// Scenario:
    /// Open RBC Data window. Then, Enter a specific information.Close Main window.Open Override window. Then, verifies the state of EOA button.Open Level window. Then, select and confirm Level 1.Open Override window. Then, verifies the state of EOA button.
    /// 
    /// Used files:
    /// 22_20_2.utt
    /// </summary>
    public class Override_window_in_SB_mode : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // Test system is powered on.The cabin is activatedDriver ID is enteredLevel 2 is selected and confirmed

            // Call the TestCaseBase PreExecution
            base.PreExecution();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in SB mode, level 1.

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint


            /*
            Test Step 1
            Action: Press ‘Enter RBC Data’ button
            Expected Result: DMI displays RBC Data window
            */
            // Call generic Check Results Method
            DmiExpectedResults.DMI_displays_RBC_Data_window(this);


            /*
            Test Step 2
            Action: Enter and confirm the following values, RBC ID= 6996969RBC Phone number = 0031840880100Then, press ‘Yes’ button
            Expected Result: DMI displays Main window
            */
            // Call generic Check Results Method
            DmiExpectedResults.DMI_displays_Main_window(this);


            /*
            Test Step 3
            Action: Perform the following procedure,Press ‘Train data’ button.Enter and validate train data
            Expected Result: DMI displays Main window with enabled ‘Start’ button
            */
            // Call generic Check Results Method
            DmiExpectedResults.DMI_displays_Main_window_with_enabled_Start_button(this);


            /*
            Test Step 4
            Action: Press ‘Close’ button
            Expected Result: DMI displays Default window
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press ‘Close’ button");
            // Call generic Check Results Method
            DmiExpectedResults.DMI_displays_Default_window(this);


            /*
            Test Step 5
            Action: Press ‘Override’ button
            Expected Result: Verify the following information,The ‘EOA’ button is in enable state.Use the log file to confirm that DMI receives EVC-30 with with bit No.9 of variable MMI_Q_REQUEST_ENABLE_64 = 1 (Enable Start Override EOA)
            Test Step Comment: (1) MMI_gen 8415 (partly: touch screen, label “EOA”);              MMI_gen 11225 (partly: EVC-30, enabled);(2) MMI_gen 11225 (partly: enabled);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press ‘Override’ button");


            /*
            Test Step 6
            Action: Perform the following procedure, Press ‘Close’ buttonPress ‘Main’ buttonPress ‘Level’ buttonSelect and confirm Level 1.Press ‘Close’ buttonPress ‘Override’ button
            Expected Result: Verify the following information,The ‘EOA’ button is in disable state.Use the log file to confirm that DMI receives EVC-30 with with bit No.9 of variable MMI_Q_REQUEST_ENABLE_64 = 0 (Disable Start Override EOA)
            Test Step Comment: (1) MMI_gen 8415 (partly: touch screen, label “EOA”);              MMI_gen 11225 (partly: EVC-30, disabled);(2) MMI_gen 11225 (partly: disabled);
            */
            // Call generic Check Results Method
            DmiExpectedResults
                .Verify_the_following_information_The_EOA_button_is_in_disable_state_Use_the_log_file_to_confirm_that_DMI_receives_EVC_30_with_with_bit_No_9_of_variable_MMI_Q_REQUEST_ENABLE_64_0_Disable_Start_Override_EOA(this);


            /*
            Test Step 7
            Action: End of test
            Expected Result: 
            */


            return GlobalTestResult;
        }
    }
}
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
    /// 27.8.1.4 ‘RBC data’ Data Checks: Technical Range Checks by Data Validity
    /// TC-ID: 22.8.1.4
    /// 
    /// This test case verifies the functionalities of the ‘RBC data’ data entry when the RBC data does not comply with ETCS Onboard failed check. The function designs comply with the conditions in [MMI-ETCS-gen]. The data range and interface comply with the data information in [VSIS_gen].
    /// 
    /// Tested Requirements:
    /// MMI_gen 9467;
    /// 
    /// Scenario:
    /// 1.Activate the cabin.
    /// 2.Perform SoM until level 2 is selected. The ‘RBC data’ data entry window appears.
    /// 3.Enter and accept valid RBC data to enable the ‘Yes’ button.
    /// 4.Enter and accept invalid RBC data to verify the ‘Yes’ button.
    /// 
    /// Used files:
    /// 22_8_1_4.utt, 22_8_1_4_a.xml
    /// </summary>
    public class RBC_data_Data_Checks_Technical_Range_Checks_by_Data_Validity : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // 1. The test environment is powered on.2. The cabin is activated.3. The ‘Start of Mission’ procedure is performed until level 2 is selected.4. RBC Data window is opened.

            // Call the TestCaseBase PreExecution
            base.PreExecution();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // 1. ETCS-DMI is in the ‘Start of Mission’ procedure.2. ETCS-DMI is in the ‘Stand-By’ mode.

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint


            /*
            Test Step 1
            Action: Enter valid values with the numeric keypad and press the data input field (Accept) in the same screen.RBC ID6996969RBC phone number0031840880100
            Expected Result: The ‘Yes’ button is enabled
            */
            // Call generic Check Results Method
            DmiExpectedResults.The_Yes_button_is_enabled(this);


            /*
            Test Step 2
            Action: Enter “1” (invalid value) for RBC ID with the numeric keypad and press the data input field (Accept) in the same screen
            Expected Result: EVC-22(1) Use the log file to verify that DMI receives packet EVC-22 with variable MMI_M_BUTTONS = 255 (no button) and the 'Yes' button is disabled
            Test Step Comment: Requirements:(1) MMI_gen 9467;
            */
            // Call generic Check Results Method
            DmiExpectedResults
                .EVC_221_Use_the_log_file_to_verify_that_DMI_receives_packet_EVC_22_with_variable_MMI_M_BUTTONS_255_no_button_and_the_Yes_button_is_disabled(this);


            /*
            Test Step 3
            Action: Enter “6996969” (valid value) for RBC ID with the numeric keypad and press the data input field (Accept) in the same screen
            Expected Result: The ‘Yes’ button is enabled
            */
            // Call generic Check Results Method
            DmiExpectedResults.The_Yes_button_is_enabled(this);


            /*
            Test Step 4
            Action: Enter “1” (invalid value) for RBC phone number with the numeric keypad and press the data input field (Accept) in the same screen
            Expected Result: EVC-22(1) Use the log file to verify that DMI receives packet EVC-22 with variable MMI_M_BUTTONS = 255 (no button) and the 'Yes' button is disabled
            Test Step Comment: Requirements:(1) MMI_gen 9467;
            */
            // Call generic Check Results Method
            DmiExpectedResults
                .EVC_221_Use_the_log_file_to_verify_that_DMI_receives_packet_EVC_22_with_variable_MMI_M_BUTTONS_255_no_button_and_the_Yes_button_is_disabled(this);


            /*
            Test Step 5
            Action: Enter “0031840880100” (valid value) for RBC phone number with the numeric keypad and press the data input field (Accept) in the same screen
            Expected Result: The ‘Yes’ button is enabled
            */
            // Call generic Check Results Method
            DmiExpectedResults.The_Yes_button_is_enabled(this);


            /*
            Test Step 6
            Action: This step is to complete the process of ‘RBC data’:- Press the ‘Yes’ button on the ‘RBC data’ window
            Expected Result: The data entry window disappears
            */


            /*
            Test Step 7
            Action: Follow test step 2 to enable the ‘Yes’ button
            Expected Result: The ‘Yes’ button is enabled
            Test Step Comment: Note: This is a temporary approach for non-support test environment on the data checks.
            */
            // Call generic Check Results Method
            DmiExpectedResults.The_Yes_button_is_enabled(this);


            /*
            Test Step 8
            Action: Send the data of ‘Technical Range Check’ failure to ETCS-DMI by 22_8_1_4_a.xmlEVC-22MMI_M_BUTTONS = 255 (No button)
            Expected Result: EVC-22(1) The 'Yes' button is disabled
            Test Step Comment: Requirements:MMI_gen 9467;Note: This is a temporary approach for non-support test environment on the data checks.
            */


            /*
            Test Step 9
            Action: End of test
            Expected Result: 
            */


            return GlobalTestResult;
        }
    }
}
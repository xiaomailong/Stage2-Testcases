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
    /// 27.6.3.2.5 ‘Wheel diameter’ Data Checks: Technical Range Checks by Variable Range
    /// TC-ID: 22.6.3.2.5
    /// 
    /// This test case verifies the functionalities of ‘Wheel diameter’ data entry when the data of Wheel diameter does not comply with variable-range rules of the technical range check. The function designs comply with the conditions in [MMI-ETCS-gen]. The data range and interface comply with the data information in [VSIS_gen].
    /// 
    /// Tested Requirements:
    /// MMI_gen 11757 (partly: MMI_gen 4713, MMI_gen 12145, MMI_gen 12147, MMI_gen 12148, MMI_gen 4714); MMI_gen 4699 (technical range);
    /// 
    /// Scenario:
    /// 1.Activate the cabin.
    /// 2.Press the ‘Settings’ button in the ‘Driver ID’ window. Then, the ‘Settings’ window is opened.
    /// 3.Press the ‘Maintenance’ button. Then, the ‘Password’ window is opened.
    /// 4.Enter the password of “26728290”. Then, the ‘Maintenance’ window is opened.
    /// 5.Press the ‘Wheel diameter’ button. Then, the ‘Wheel diameter’ window is opened.
    /// 6.Enter the data of Wheel diameter. Then, the ‘Wheel diameter’ window is verified by the following events:a.   The minimum DMI-technical-inbound data of Wheel diameter is entered and accepted.b.   The DMI-technical-outbound data of Wheel diameter is entered and accepted.c.   The maximum DMI-technical-inbound data of Wheel diameter is entered and accepted.
    /// 
    /// Used files:
    /// N/A
    /// </summary>
    public class Wheel_diameter_Data_Checks_Technical_Range_Checks_by_Variable_Range : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // 1. The test environment is powered on.2. The cabin is activated.3. The ‘Settings’ window is opened.

            // Call the TestCaseBase PreExecution
            base.PreExecution();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // ETCS-DMI is in the ‘Stand-By’ mode.

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint


            /*
            Test Step 1
            Action: Open the ‘Wheel diameter’ data entry window from the Settings menu
            Expected Result: The ‘Wheel diameter’ data entry window appears on ETCS-DMI screen instead of the ‘Settings’ menu window
            */
            // Call generic Action Method
            DmiActions.Open_the_Wheel_diameter_data_entry_window_from_the_Settings_menu();
            // Call generic Check Results Method
            DmiExpectedResults
                .The_Wheel_diameter_data_entry_window_appears_on_ETCS_DMI_screen_instead_of_the_Settings_menu_window();


            /*
            Test Step 2
            Action: Enter the minimum inbound values with the numeric keypad and press the data input field (Accept) in the same screen, for the following fields below, Wheel diameter 1: 500Wheel diameter 2: 500Accuracy: 0
            Expected Result: Input Field(1) The eventually displayed data value in the data area of the input field is replaced by the previously entered values (character or value corresponding to the activated data key - state ‘Selected IF/value of pressed key(s)’):Wheel diameter 1: 500Wheel diameter 2: 500Accuracy: 0EVC-140(2) Use the log file to verify that DMI sends packet EVC-140 with variable:-   MMI_M_SDU_WHEEL_SIZE_1 = 500-   MMI_M_SDU_WHEEL_SIZE_2 = 500-   MMI_M_WHEEL_SIZE_ERR = 0-   MMI_Q_MD_DATASET = 0 (Wheel diameter)
            Test Step Comment: Requirements:(1) MMI_gen 11757 (partly:  MMI_gen 4714 (partly: MMI_gen 4679),  MMI_gen 12145 (partly: minimum inbound));(2) MMI_gen 11757 (partly:  MMI_gen 12147)
            */


            /*
            Test Step 3
            Action: Enter the outbound value with the numeric keypad and press the data input field (Accept) in the same screen, for the following fields below, Wheel diameter 1: 1501Wheel diameter 2: 499Accuracy: 33
            Expected Result: Input Field(1) The ‘Enter’ button associated to the data area of the input field is coloured grey and its text is black (state ‘Selected IF/Data value’).(2) The ‘Enter’ button associated to the data area of the input field displays the previously entered value:Wheel diameter 1: 1501Wheel diameter 2: 499Accuracy: 33EVC-140(3) Use the log file to verify that DMI does not send out packet EVC-140 as the ‘Enter’ button is disabled. Echo Texts(4) The data part of the echo text displays “++++”.(5) The data part of the echo text is coloured red
            Test Step Comment: Requirements:(1) MMI_gen 11757 (partly:  MMI_gen 4714 (partly: state 'Selected IF/data value'));(2) MMI_gen 11757 (partly:  MMI_gen 4714 (partly: previously entered (faulty) value), MMI_gen 12145 (partly: outbound)); MMI_gen 4699 (technical range);(3) MMI_gen 11757 (partly: MMI_gen 12148 (partly: not send packets) , MMI_gen 12147);(4) MMI_gen 11757 (partly:  MMI_gen 12148 (MMI_gen 4713 (partly: indication)));(5) MMI_gen 11757 (partly:  MMI_gen 12148 (MMI_gen 4713 (partly: red)));
            */


            /*
            Test Step 4
            Action: Enter the maximum inbound values with the numeric keypad and press the data input field (Accept) in the same screen, for the following fields below, Wheel diameter 1: 1500Wheel diameter 2: 1500Accuracy: 32Then, press the ‘Yes’ button
            Expected Result: Input Field(1) The eventually displayed data value in the data area of the input field is replaced by the entered value (character or value corresponding to the activated data key - state ‘Selected IF/value of pressed key(s)’):Wheel diameter 1: 1500Wheel diameter 2: 1500Accuracy: 32EVC-140(2) Use the log file to verify that DMI sends packet EVC-140 with variable: -   MMI_M_SDU_WHEEL_SIZE_1 = 1500-   MMI_M_SDU_WHEEL_SIZE_2 = 1500-   MMI_M_WHEEL_SIZE_ERR = 32-   MMI_Q_MD_DATASET = 0 (Wheel diameter)
            Test Step Comment: Requirements:(1) MMI_gen 11757 (partly: MMI_gen 4714 (partly: MMI_gen 4679), MMI_gen 12145 (partly: maximum inbound));(2) MMI_gen 11757 (partly: MMI_gen 12147);
            */


            /*
            Test Step 5
            Action: This step is to complete the process of ‘Wheel diameter’:- Press the ‘Yes’ button on the ‘Wheel diameter’ window.- Validate the data in the data validation window
            Expected Result: 1. After pressing the ‘Yes’ button, the data validation window (‘Validate Wheel diameter’) appears instead of the ‘Wheel diameter’ data entry window. The data part of echo text displays in white:Wheel diameter 1: 1500Wheel diameter 2: 1500Accuracy: 322. After the data area of the input field containing “Yes” is pressed, the data validation window disappears and returns to the parent window (‘Settings’ window) of ‘Wheel diameter’ window with enabled ‘Wheel diameter’ button
            */
            // Call generic Action Method
            DmiActions
                .This_step_is_to_complete_the_process_of_Wheel_diameter_Press_the_Yes_button_on_the_Wheel_diameter_window_Validate_the_data_in_the_data_validation_window();


            /*
            Test Step 6
            Action: End of test
            Expected Result: 
            */


            return GlobalTestResult;
        }
    }
}
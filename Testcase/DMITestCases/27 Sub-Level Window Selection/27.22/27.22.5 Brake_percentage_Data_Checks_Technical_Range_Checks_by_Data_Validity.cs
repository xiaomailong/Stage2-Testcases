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
    /// 27.22.5 ‘Brake percentage’ Data Checks: Technical Range Checks by Data Validity
    /// TC-ID: 22.22.5
    /// 
    /// This test case verifies the functionalities of the ‘Brake percentage’ data entry when the data of Brake percentage does not comply with data-validity rules of the technical range check. The function designs comply with the conditions in [MMI-ETCS-gen]. The data range and interface comply with the data information in [VSIS_gen].
    /// 
    /// Tested Requirements:
    /// MMI_gen 11823 (partly: MMI_gen 12148, MMI_gen 4713, MMI_gen 4714, MMI_gen 4679, MMI_gen 9286 (partly: the ‘Enter’ button, disabled, enabled), MMI_gen 12147); MMI_gen 4699 (partly: technical range);                                                                               MMI_gen 11832 (partly: MMI_gen 12148 (MMI_gen 4713 (partly: red)); MMI_gen 9310 (partly: technical range); 
    /// 
    /// Scenario:
    /// 1.Configure the ATP-CU to enable level ‘ATC-2’
    /// 2.Activate the cabin.
    /// 3.Perform SoM until mode SR is confirmed in the default window.
    /// 4.Press the ‘Settings’ button. Then, the ‘Settings’ window is opened.
    /// 5.Press the ‘Brake’ button. Then, the ‘Brake’ window is opened.
    /// 6.Press the ‘Brake percentage’ button. Then, the ‘Brake percentage’ window is opened.
    /// 7.Enter the data of Brake percentage. Then, the ‘Brake percentage’ window is verified by the following events:a.   Enter and accept the invalid data.b.   Repeat a. by valid data in order that the data is entered and accepted after the ‘Enter’ button is disabled.
    /// 
    /// Used files:
    /// 22_22_5_a.xml
    /// </summary>
    public class Brake_percentage_Data_Checks_Technical_Range_Checks_by_Data_Validity : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // 1. The ‘ATC-2’ level is configured in ATP-CU.NID_NTC_Installed = 22, PB_SAFETY_LEVEL = 2, NTC_HW_ADDR = 92, NID_NTC_Default = 22 (M_InstalledLevels and M_DefaultLevels have to be updated according to the number of enabling NTC/STM levels, by bitmasks)2. The test environment is powered on.3. The cabin is activated.4. The ‘Start of Mission’ procedure is performed until the ‘Staff Responsible’ mode, level 1, is confirmed.5. The ‘Brake’ window is opened.

            // Call the TestCaseBase PreExecution
            base.PreExecution();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // ETCS-DMI is in the ‘Staff Responsible’ mode, level 1.

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint


            /*
            Test Step 1
            Action: Open the ‘Brake percentage’ data entry window from the Special menu
            Expected Result: The ‘Brake percentage’ data entry window appears on ETCS-DMI screen instead of the ‘Special’ menu window
            */
            // Call generic Action Method
            DmiActions.Open_the_Brake_percentage_data_entry_window_from_the_Special_menu();
            // Call generic Check Results Method
            DmiExpectedResults
                .The_Brake_percentage_data_entry_window_appears_on_ETCS_DMI_screen_instead_of_the_Special_menu_window();


            /*
            Test Step 2
            Action: Enter “10” (invalid value) for Brake percentage with the numeric keypad and press the data input field (Accept) in the same screen.Then, observe the echo texts on the left hand side
            Expected Result: EVC-50Use the log file to verify that DMI receives packet EVC-50 with variable:(1) MMI_M_BP_CURRENT = 251 in order to indicate the technical range check failure.Input Field(2) The ‘Enter’ button associated to the data area of the input field is coloured grey and its text is black (state ‘Selected IF/Data value’).(3) The ‘Enter’ button associated to the data area of the input field displays “10” (previously entered value).Echo Text(4) The data part of the echo text displays “++++”.(5) The data part of the echo text is coloured red
            Test Step Comment: Requirements:(1) MMI_gen 11823 (partly: data checks, MMI_gen 12147); MMI_gen 11832 (partly: EVC-50),(2) MMI_gen 11823 (partly: MMI_gen 4714 (partly: state 'Selected IF/data value')); MMI_gen 9310 (partly: accept data);(3) MMI_gen 11823 (partly: MMI_gen 4714 (partly: previously entered (faulty) value)); MMI_gen 4699 (technical range);(4) MMI_gen 11823 (partly: MMI_gen 4713 (partly: indication)); MMI_gen 9310 (partly: [technical range, No OK, echo text]);(5) MMI_gen 11832 (partly: red); MMI_gen 11823 (partly: MMI_gen 4713 (partly: red));
            */


            /*
            Test Step 3
            Action: Press the data input field once again (Accept) in the same screen
            Expected Result: Input Field(1) The ‘Enter’ button associated to the data area of the input field is still coloured grey and its text is black (state ‘Selected IF/data value’).(2) The ‘Enter’ button associated to the data area of the input field displays “10” (previously entered value).EVC-150(3) Use the log file to verify that DMI does not send out packet EVC-150 as the ‘Enter’ button is disabled.Echo Text(4) The data part of the echo text displays “++++”.(5) The data part of the echo text is coloured red
            Test Step Comment: Requirements:(1) MMI_gen 11823 (partly: MMI_gen 4714 (partly: state 'Selected IF/data value');(2) MMI_gen 11823 (partly: MMI_gen 4714 (partly: previously entered (faulty) value)); MMI_gen 4699 (partly: technical range);(3) MMI_gen 11823 (partly: MMI_gen 9286 (partly: button ‘Enter’, disabled), MMI_gen 12148 (partly: not send packets)); MMI_gen 9310 (partly: [technical range, No OK, button ‘Enter’ disabled]);(4) MMI_gen 11823 (partly: MMI_gen 12148 (MMI_gen 4713 (partly: indication)));(5) MMI_gen 11823 (partly: MMI_gen 12148 (MMI_gen 4713 (partly: red)));
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(@"Press the data input field once again (Accept) in the same screen");


            /*
            Test Step 4
            Action: Enter “10” (invalid value) for Brake percentage with the numeric keypad in the same screen
            Expected Result: Input Field(1) The eventually displayed data value in the data area of the input field is replaced by “10” (character or value corresponding to the activated data key - state ‘Selected IF/value of pressed key(s)’)
            Test Step Comment: Requirements:(1) MMI_gen 11823 (partly: MMI_gen 4714 (partly: MMI_gen 4679), MMI_gen 9286 (partly: button ‘Enter’, enabled)); MMI_gen 9310 (partly: press one key);
            */


            /*
            Test Step 5
            Action: Press the data input field of Brake percentage (Accept)
            Expected Result: EVC-50Use the log file to verify that DMI receives packet EVC-50 with variable:(1) MMI_M_BP_CURRENT = 251 in order to indicate the technical range check failure.Input Field(2) The ‘Enter’ button associated to the data area of the input field is coloured grey and its text is black (state ‘Selected IF/Data value’)
            Test Step Comment: Requirements:(1) MMI_gen 11823 (partly: technical range, MMI_gen 12147); MMI_gen 9310 (partly: [Up-Type enabled button ‘Enter’], accept data); MMI_gen 11832 (partly: EVC-50),(2) MMI_gen 11823 (partly: MMI_gen 4714 (partly: state 'Selected IF/data value'));
            */


            /*
            Test Step 6
            Action: Press the data input field of Brake percentage once again (Accept) in the same screen
            Expected Result: Input Field(1) The ‘Enter’ button associated to the data area of the input field is still coloured grey and its text is black (state ‘Selected IF/data value’).(2) The ‘Enter’ button associated to the data area of the input field displays “10” (previously entered value).EVC-150(3) Use the log file to verify that DMI does not send out packet EVC-150 as the ‘Enter’ button is disabled. Echo Text(4) The data part of the echo text displays “++++”.(5) The data part of the echo text is coloured red
            Test Step Comment: Requirements:(1) MMI_gen 11823 (partly: MMI_gen 4714 (partly: state 'Selected IF/data value');(2) MMI_gen 11823 (partly: MMI_gen 4714 (partly: previously entered (faulty) value)); MMI_gen 4699 (technical range);(3) MMI_gen 11823 (partly: MMI_gen 9286 (partly: button ‘Enter’, disabled), MMI_gen 12148 (partly: not send packets)); MMI_gen 9310 (partly: [technical range, No OK, button ‘Enter’ disabled]);(4) MMI_gen 11823 (partly: technical range, MMI_gen 12148 (MMI_gen 4713 (partly: indication))); MMI_gen 11832 (partly: EVC-50),(5) MMI_gen 11823 (partly: technical range, MMI_gen 12148 (MMI_gen 4713 (partly: red))); MMI_gen 11832 (partly: EVC-50),
            */


            /*
            Test Step 7
            Action: Enter “40” (valid value) for Brake percentage with the numeric keypad
            Expected Result: Input Field(1) The eventually displayed data value in the data area of the input field is replaced by “40” (character or value corresponding to the activated data key - state ‘Selected IF/value of pressed key(s)’)
            Test Step Comment: Requirements:(1) MMI_gen 11823 (partly: MMI_gen 4714 (partly: MMI_gen 4679), MMI_gen 9286 (partly: button ‘Enter’, enabled)); MMI_gen 9310 (partly: press one key);
            */
            // Call generic Check Results Method
            DmiExpectedResults
                .Input_Field1_The_eventually_displayed_data_value_in_the_data_area_of_the_input_field_is_replaced_by_40_character_or_value_corresponding_to_the_activated_data_key_state_Selected_IFvalue_of_pressed_keys();


            /*
            Test Step 8
            Action: Press the data input field of Brake percentage (Accept) in the same screen
            Expected Result: EVC-150(1) Use the log file to confirm that DMI sends packet EVC-150 with variable:MMI_M_BP_CURRENT = 40EVC-50(2) Use the log file to verify that DMI receives packet EVC-50 with variable:MMI_M_BP_CURRENT = 40
            Test Step Comment: Requirements:(1) MMI_gen 11823 (partly: MMI_gen 9286 (partly: enabled));(2) MMI_gen 11823 (partly: technical range, MMI_gen 9310 (partly: [technical range, Yes OK])); MMI_gen 11832 (partly: EVC-50),
            */


            /*
            Test Step 9
            Action: This step is to complete the process of ‘Brake percentage’:- Press the ‘Yes’ button on the ‘Brake percentage’ window.- Validate the data in the data validation window
            Expected Result: 1. After pressing the ‘Yes’ button, the data validation window (‘Validate Brake percentage’) appears instead of the ‘Brake percentage’ data entry window. The data part of echo text displays in white:Brake percentage: 402. After the data area of the input field containing “Yes” is pressed, the data validation window disappears and returns to the parent window (‘Special’ window) of ‘Brake percentage’ window with enabled ‘Brake percentage’ button
            */
            // Call generic Action Method
            DmiActions
                .This_step_is_to_complete_the_process_of_Brake_percentage_Press_the_Yes_button_on_the_Brake_percentage_window_Validate_the_data_in_the_data_validation_window();


            /*
            Test Step 10
            Action: Send the data of ‘Technical Range Check’ failure to ETCS-DMI by 22_22_5_a.xmlEVC-50MMI_M_BP_CURRENT = 251 (Technical Range Check failed)
            Expected Result: Input Field(1) The ‘Enter’ button associated to the data area of the input field displays the previously entered value.Echo Text(2) The data part of the echo text displays “++++”
            Test Step Comment: Requirements:(1) MMI_gen 11823 (partly: technical range, MMI_gen 4714 (partly: previously entered (faulty) value)); MMI_gen 4699 (technical range);(2) MMI_gen 11823 (partly: technical range, MMI_gen 12148 (MMI_gen 4713 (partly: indication))); MMI_gen 11832 (partly: EVC-50),Note: This is a temporary approach for non-support test environment on the data checks.
            */


            /*
            Test Step 11
            Action: End of test
            Expected Result: 
            */


            return GlobalTestResult;
        }
    }
}
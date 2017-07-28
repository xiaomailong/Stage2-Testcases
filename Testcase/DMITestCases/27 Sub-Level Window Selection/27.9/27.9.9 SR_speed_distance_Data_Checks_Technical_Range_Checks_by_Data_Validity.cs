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
    /// 27.9.9 ‘SR speed / distance’ Data Checks: Technical Range Checks by Data Validity
    /// TC-ID: 22.9.9
    /// 
    /// This test case verifies the functionalities of the ‘SR speed / distance’ data entry when the data of SR speed / distance does not comply with data-validity rules of the technical range check. The function designs comply with the conditions in [MMI-ETCS-gen]. The data range and interface comply with the data information in [VSIS_gen].
    /// 
    /// Tested Requirements:
    /// 1. SR speed / distance Window: MMI_gen 9892; MMI_gen 9896; MMI_gen 9509 (partly: the ‘Enter’ button, accepted data complied with data checks, driver action); MMI_gen 9510 (partly: the ‘Enter’ button, accepted data complied with data checks, driver action);2. Data Checks: MMI_gen 8297 (partly: MMI_gen 12148, MMI_gen 4713, MMI_gen 4714, MMI_gen 4679, MMI_gen 9286 (partly: the ‘Enter’ button, disabled, enabled), MMI_gen 12147); MMI_gen 9889 (partly: MMI_gen 12148 (MMI_gen 4713 (partly: red)); MMI_gen 9310 (partly: technical range);
    /// 
    /// Scenario:
    /// 1.Activate the cabin.
    /// 2.Perform SoM until mode SR is confirmed in the default window.
    /// 3.Press the ‘Special’ button. Then, the ‘Special’ window is opened.
    /// 4.Press the ‘SR speed / distance’ button. Then, the ‘SR speed / distance’ window is opened.
    /// 5.Enter the data of SR speed / distance. Then, the ‘SR speed / distance’ window is verified by the following events:a.   Enter and accept the invalid data. b.   Accept the previuos invalid data without re-enter in order that DMI does not send out any packets (The ‘Enter’ button is disabled).c.   Repeat a. and b. in order that the invalid data is re-entered and accepted although the ‘Enter’ button is disabled.d.   Repeat a. by valid data in order that the data is entered and accepted after the ‘Enter’ button is disabled.Note: The appearance of highlighting in data area has remained in DMI since BL-2 requirement [MMI-ETCS-gen BL2].
    /// 
    /// Used files:
    /// 22_9_9_a.xml, 22_9_9_b.xml
    /// </summary>
    public class SR_speed_distance_Data_Checks_Technical_Range_Checks_by_Data_Validity : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // 1. The test environment is powered on.2. The cabin is activated.3. The ‘Start of Mission’ procedure is performed until the ‘Staff Responsible’ mode, level 1, is confirmed.4. The ‘Special’ window is opened.

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
            Action: Open the ‘SR speed / distance’ data entry window from the Special menu
            Expected Result: The ‘SR speed / distance’ data entry window appears on ETCS-DMI screen instead of the ‘Special’ menu window
            */
            // Call generic Action Method
            DmiActions.Open_the_SR_speed_distance_data_entry_window_from_the_Special_menu();
            // Call generic Check Results Method
            DmiExpectedResults
                .The_SR_speed_distance_data_entry_window_appears_on_ETCS_DMI_screen_instead_of_the_Special_menu_window();


            /*
            Test Step 2
            Action: Enter “1” (invalid value) for SR speed with the numeric keypad and press the data input field (Accept) in the same screen.Then, observe the echo texts on the left hand side
            Expected Result: EVC-11Use the log file to verify that DMI receives packet EVC-11 with variable:(1) MMI_Q_DATA_CHECK = 1 in order to indicate the technical range check failure.(2) MMI_M_BUTTONS = 255 (no button) and the 'Yes' button is disabled.(3) MMI_NID_DATA = 15 (SR Speed)Input Field(4) The ‘Enter’ button associated to the data area of the input field is coloured grey and its text is black (state ‘Selected IF/Data value’).(5) The ‘Enter’ button associated to the data area of the input field displays “1” (previously entered value).Echo Texts of SR Speed(6) The data part of the echo text displays “++++”.(7) The data part of the echo text is coloured red
            Test Step Comment: Requirements:(1) MMI_gen 8297 (partly: EVC-11, MMI_gen 12147);(2) MMI_gen 9892;(3) MMI_gen 9509 (partly: MMI_NID_DATA);(4) MMI_gen 8297 (partly: MMI_gen 4714 (partly: state 'Selected IF/data value')); MMI_gen 9310 (partly: accept data);(5) MMI_gen 8297 (partly: MMI_gen 4714 (partly: previously entered (faulty) value)); MMI_gen 4699 (technical range);(6) MMI_gen 8297 (partly: MMI_gen 4713 (partly: indication)); MMI_gen 9310 (partly: [technical range, No OK, echo text]);(7) MMI_gen 9889 (partly: MMI_gen 4713 (partly: red)), MMI_gen 8297 (partly: MMI_gen 4713 (partly: red));
            */


            /*
            Test Step 3
            Action: Press the data input field once again (Accept) in the same screen
            Expected Result: Input Field(1) The ‘Enter’ button associated to the data area of the input field is still coloured grey and its text is black (state ‘Selected IF/data value’).(2) The ‘Enter’ button associated to the data area of the input field displays “1” (previously entered value).EVC-106(3) Use the log file to verify that DMI does not send out packet EVC-106 as the ‘Enter’ button is disabled.Echo Texts of SR Speed(4) The data part of the echo text displays “++++”.(5) The data part of the echo text is coloured red
            Test Step Comment: Requirements:(1) MMI_gen 8297 (partly: MMI_gen 4714 (partly: state 'Selected IF/data value');(2) MMI_gen 8297 (partly: MMI_gen 4714 (partly: previously entered (faulty) value)); MMI_gen 4699 (technical range);(3) MMI_gen 8297 (partly: MMI_gen 9286 (partly: button ‘Enter’, disabled), MMI_gen 12148 (partly: not send packets)), MMI_gen 9896 (partly: disabled), MMI_gen 9509 (partly: EVC-106); MMI_gen 9310 (partly: [technical range, No OK, button ‘Enter’ disabled]);(4) MMI_gen 8297 (partly: MMI_gen 12148 (MMI_gen 4713 (partly: indication))); MMI_gen 9509 (partly: only affect the object indicated in MMI_NID_DATA);(5) MMI_gen 9889 (partly: MMI_gen 12148 (MMI_gen 4713 (partly: red))), MMI_gen 8297 (partly: MMI_gen 12148 (MMI_gen 4713 (partly: red)));
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(@"Press the data input field once again (Accept) in the same screen");


            /*
            Test Step 4
            Action: Enter “1” (invalid value) for SR speed with the numeric keypad in the same screen
            Expected Result: Input Field(1) The eventually displayed data value in the data area of the input field is replaced by “1” (character or value corresponding to the activated data key - state ‘Selected IF/value of pressed key(s)’)
            Test Step Comment: Requirements:(1) MMI_gen 8297 (partly: MMI_gen 4714 (partly: MMI_gen 4679), MMI_gen 9286 (partly: button ‘Enter’, enabled)), MMI_gen 9896 (partly: state switched); MMI_gen 9310 (partly: press one key);
            */
            // Call generic Check Results Method
            DmiExpectedResults
                .Input_Field1_The_eventually_displayed_data_value_in_the_data_area_of_the_input_field_is_replaced_by_1_character_or_value_corresponding_to_the_activated_data_key_state_Selected_IFvalue_of_pressed_keys();


            /*
            Test Step 5
            Action: Press the data input field of SR speed (Accept)
            Expected Result: EVC-11Use the log file to verify that DMI receives packet EVC-11 with variable:(1) MMI_Q_DATA_CHECK = 1 in order to indicate the technical range check failure.(2) MMI_M_BUTTONS = 255 (no button) and the 'Yes' button is disabled.Input Field(3) The ‘Enter’ button associated to the data area of the input field is coloured grey and its text is black (state ‘Selected IF/Data value’)
            Test Step Comment: Requirements:(1) MMI_gen 8297 (partly: EVC-11, MMI_gen 12147); MMI_gen 9310 (partly: [Up-Type enabled button ‘Enter’], accept data);(2) MMI_gen 9892; (3) MMI_gen 8297 (partly: MMI_gen 4714 (partly: state 'Selected IF/data value'));
            */


            /*
            Test Step 6
            Action: Press the data input field of SR speed once again (Accept) in the same screen
            Expected Result: Input Field(1) The ‘Enter’ button associated to the data area of the input field is still coloured grey and its text is black (state ‘Selected IF/data value’).(2) The ‘Enter’ button associated to the data area of the input field displays “1” (previously entered value).EVC-106(3) Use the log file to verify that DMI does not send out packet EVC-106 as the ‘Enter’ button is disabled. Echo Texts of SR Speed(4) The data part of the echo text displays “++++”.(5) The data part of the echo text is coloured red
            Test Step Comment: Requirements:(1) MMI_gen 8297 (partly: MMI_gen 4714 (partly: state 'Selected IF/data value');(2) MMI_gen 8297 (partly: MMI_gen 4714 (partly: previously entered (faulty) value)); MMI_gen 4699 (technical range);(3) MMI_gen 8297 (partly: MMI_gen 9286 (partly: button ‘Enter’, disabled), MMI_gen 12148 (partly: not send packets)), MMI_gen 9896 (partly: disabled), MMI_gen 9509 (partly: EVC-106); MMI_gen 9310 (partly: [technical range, No OK, button ‘Enter’ disabled]);(4) MMI_gen 8297 (partly: MMI_gen 12148 (MMI_gen 4713 (partly: indication))); MMI_gen 9509 (partly: only affect the object indicated in MMI_NID_DATA);(5) MMI_gen 9889 (partly: MMI_gen 12148 (MMI_gen 4713 (partly: red))), MMI_gen 8297 (partly: MMI_gen 12148 (MMI_gen 4713 (partly: red)));
            */


            /*
            Test Step 7
            Action: Enter “40” (valid value) for SR speed with the numeric keypad
            Expected Result: Input Field(1) The eventually displayed data value in the data area of the input field is replaced by “40” (character or value corresponding to the activated data key - state ‘Selected IF/value of pressed key(s)’)
            Test Step Comment: Requirements:(1) MMI_gen 8297 (partly: MMI_gen 4714 (partly: MMI_gen 4679), MMI_gen 9286 (partly: button ‘Enter’, enabled)), MMI_gen 9896 (partly: state switched); MMI_gen 9310 (partly: press one key);
            */
            // Call generic Check Results Method
            DmiExpectedResults
                .Input_Field1_The_eventually_displayed_data_value_in_the_data_area_of_the_input_field_is_replaced_by_40_character_or_value_corresponding_to_the_activated_data_key_state_Selected_IFvalue_of_pressed_keys();


            /*
            Test Step 8
            Action: Press the data input field of SR speed (Accept) in the same screen
            Expected Result: Input Field(1) The ‘SR distance’ data input field remains the same.EVC-106(1) Use the log file to confirm that DMI sends packet EVC-106 with variable:-   MMI_V_STFF = 40-    MMI_M_BUTTONS =  254 (BTN_ENTER)-    MMI_N_DATA_ELEMENTS = 1-    MMI_NID_DATA = 15 (SR Speed)EVC-11(2) Use the log file to verify that DMI receives packet EVC-11 with variable:-    MMI_Q_DATA_CHECK = 0 (All checks have passed)-   MMI_X_TEXT = 52 (“4”)-    MMI_X_TEXT = 48 (“0”)
            Test Step Comment: Requirements:(1) MMI_gen 9509 (partly: only affect the object indicated in MMI_NID_DATA);(2) MMI_gen 8297 (partly: MMI_gen 9286 (partly: enabled)), MMI_gen 9896 (partly: enabled), MMI_gen 9509 (partly: EVC-106, the ‘Enter’ button, accepted data complied with data checks, driver action);(3) MMI_gen 8297 (partly: EVC-11) MMI_gen 9310 (partly: [technical range, Yes OK]);
            */


            /*
            Test Step 9
            Action: Follow step 2 – step 8 for SR distance with:invalid value of “1”valid value of “10000”
            Expected Result: See step 2 – step 8EVC-106(1) Use the log file to confirm that DMI sends packet EVC-106 with variable:-    MMI_L_STFF = 10000 -    MMI_M_BUTTONS =  254 (BTN_ENTER)-    MMI_N_DATA_ELEMENTS = 1-    MMI_NID_DATA = 16 (SR Distance)EVC-11(2) Use the log file to verify that DMI receives packet EVC-11 with variable:-    MMI_Q_DATA_CHECK = 0 (All checks have passed)-    MMI_X_TEXT = 49 (“1”)-    MMI_X_TEXT = 48 (“0”)-    MMI_X_TEXT = 48 (“0”)-    MMI_X_TEXT = 48 (“0”)-    MMI_X_TEXT = 48 (“0”)
            Test Step Comment: See step 2 – step 8Requirements:(1) MMI_gen 8297 (partly: MMI_gen 9286 (partly: enabled)), MMI_gen 9896 (partly: enabled), MMI_gen 9510 (partly: EVC-106, the ‘Enter’ button, accepted data complied with data checks, driver action, only affect the object indicated in MMI_NID_DATE);(2) MMI_gen 8297 (partly: EVC-11) MMI_gen 9310 (partly: [technical range, Yes OK]);
            */


            /*
            Test Step 10
            Action: This step is to complete the process of ‘SR speed / distance’:- Press the ‘Yes’ button on the ‘SR speed / distance’ window.- Validate the data in the data validation window
            Expected Result: 1. After pressing the ‘Yes’ button, the data validation window (‘Validate SR speed / distance’) appears instead of the ‘SR speed / distance’ data entry window. The data part of echo text displays in white:SR Speed: 40SR Distance: 100002. After the data area of the input field containing “Yes” is pressed, the data validation window disappears and returns to the parent window (‘Special’ window) of ‘SR speed / distance’ window with enabled ‘SR speed / distance’ button
            */
            // Call generic Action Method
            DmiActions
                .This_step_is_to_complete_the_process_of_SR_speed_distance_Press_the_Yes_button_on_the_SR_speed_distance_window_Validate_the_data_in_the_data_validation_window();


            /*
            Test Step 11
            Action: Send the data of ‘Technical Range Check’ failure to ETCS-DMI by 22_9_9_a.xmlEVC-11MMI_Q_DATA_CHECK = 1 (Technical Range Check failed)MMI_NID_DATA = 16 (SR Distance)
            Expected Result: Input Field(1) The ‘Enter’ button associated to the data area of the input field displays the previously entered value.Echo Texts of SR Distance(2) The data part of the echo text displays “++++”
            Test Step Comment: Requirements:(1) MMI_gen 8297 (partly: MMI_gen 4714 (partly: previously entered (faulty) value)); MMI_gen 4699 (technical range);(2) MMI_gen 8297 (partly: MMI_gen 12148 (MMI_gen 4713 (partly: indication))) MMI_gen 9509 (partly: only affect the object indicated in MMI_NID_DATA);Note: This is a temporary approach for non-support test environment on the data checks.
            */


            /*
            Test Step 12
            Action: Send the data of ‘Technical Range Check’ failure to ETCS-DMI by 22_9_9_b.xmlEVC-11MMI_Q_DATA_CHECK = 1 (Technical Range Check failed)MMI_NID_DATA = 15 (SR Speed)
            Expected Result: Input Field(1) The ‘Enter’ button associated to the data area of the input field displays the previously entered value.Echo Texts of SR Speed(2) The data part of the echo text displays “++++”
            Test Step Comment: Requirements:(1) MMI_gen 8297 (partly: MMI_gen 4714 (partly: previously entered (faulty) value)); MMI_gen 4699 (technical range);(2) MMI_gen 8297 (partly: MMI_gen 12148 (MMI_gen 4713 (partly: indication))) MMI_gen 9509 (partly: only affect the object indicated in MMI_NID_DATA);Note: This is a temporary approach for non-support test environment on the data checks.
            */


            /*
            Test Step 13
            Action: End of test
            Expected Result: 
            */


            return GlobalTestResult;
        }
    }
}
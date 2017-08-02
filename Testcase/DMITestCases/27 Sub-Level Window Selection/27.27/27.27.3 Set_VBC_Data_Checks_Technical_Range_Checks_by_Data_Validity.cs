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
    /// 27.27.3 ‘Set VBC’ Data Checks: Technical Range Checks by Data Validity
    /// TC-ID: 22.27.3
    /// 
    /// This test case verifies the functionalities of the ‘Set VBC’ data entry when the VBC code does not comply with data-validity rules of the technical range check. The function designs comply with the conditions in [MMI-ETCS-gen]. The data range and interface comply with the data information in [VSIS_gen].
    /// 
    /// Tested Requirements:
    /// 1. Set VBC Window: MMI_gen 9901; MMI_gen 9905; MMI_gen 9923 (partly: the ‘Enter’ button, accepted data complied with data checks, driver action);2. Data Checks: MMI_gen 8328 (partly: MMI_gen 12148 (MMI_gen 4713 (partly: indication)); MMI_gen 9888 (partly: reactions to failing and succeed, EVC-18, MMI_gen 4713, MMI_gen 4714, MMI_gen 4679, MMI_gen 9286 (partly: the ‘Enter’ button, disabled, enabled), MMI_gen 12147, MMI_gen 12148); MMI_gen 9898 (partly: MMI_gen 12148 (MMI_gen 4713 (partly: red)); MMI_gen 9310 (party: technical range); MMI_gen 4699 (partly: technical range);
    /// 
    /// Scenario:
    /// Activate the cabin.Press the ‘Settings’ button. Then, the ‘Settings’ window is opened from the ‘Driver ID’ window.Press the ‘Set VBC’ button. Then, the ‘Set VBC’ window is opened.Enter a VBC code. Then, the ‘Set VBC’ window is verified by the following events:Enter and accept the invalid data. Accept the previuos invalid data without re-enter in order that DMI does not send out any packets (The ‘Enter’ button is disabled).Repeat a. and b. in order that the invalid data is re-entered and accepted although the ‘Enter’ button is disabled.Repeat a. by valid data in order that the data is entered and accepted after the ‘Enter’ button is disabled.Note: The appearance of highlighting in data area has remained in DMI since BL-2 requirement [MMI-ETCS-gen BL2].
    /// 
    /// Used files:
    /// 22_27_3_a.xml
    /// </summary>
    public class Set_VBC_Data_Checks_Technical_Range_Checks_by_Data_Validity : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // 1. The test environment is powered on.2. The cabin is activated.3. The ‘Settings’ window is opened from the ‘Driver ID’ window.

            // Call the TestCaseBase PreExecution
            base.PreExecution();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // 1. ETCS-DMI is in the ‘Start of Mission’ procedure.2. ETCS-DMI is in the ‘Stand-By’ mode.3. VBC code “65536” is stored onboard.

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint


            /*
            Test Step 1
            Action: Open the ‘Set VBC’ data entry window from the Settings menu
            Expected Result: The ‘Set VBC’ data entry window appears on ETCS-DMI screen instead of the ‘Settings’ menu window
            */
            // Call generic Check Results Method
            DmiExpectedResults
                .The_Set_VBC_data_entry_window_appears_on_ETCS_DMI_screen_instead_of_the_Settings_menu_window(this);


            /*
            Test Step 2
            Action: Enter “1” (invalid value) with the numeric keypad and press the data input field (Accept) in the same screen
            Expected Result: EVC-18(1) MMI_Q_DATA_CHECK = 1 in order to indicate the technical range check failure.(2) MMI_M_BUTTONS = 255 (no button) and the 'Yes' button is disabled.Input Field(3) The ‘Enter’ button associated to the data area of the input field is coloured grey and its text is black (state ‘Selected IF/Data value’).(4) The ‘Enter’ button associated to the data area of the input field displays “1” (previously entered value).Echo Texts(5) The data part of the echo text displays “++++”.(6) The data part of the echo text is coloured red
            Test Step Comment: Requirements:(1) MMI_gen 9888 (partly: reactions to failing, EVC-18, MMI_gen 12147);(2) MMI_gen 9901;(3) MMI_gen 9888 (partly: reactions to failing, MMI_gen 4714 (partly: state 'Selected IF/data value')); MMI_gen 9310 (partly: accept data);(4) MMI_gen 9888 (partly: reactions to failing, MMI_gen 4714 (partly: previously entered (faulty) value)); MMI_gen 4699 (partly: technical range);(5) MMI_gen 8328 (partly: MMI_gen 4713 (partly: indication)), MMI_gen 9888 (partly: reactions to failing, MMI_gen 4713 (partly: indication)); MMI_gen 9310 (partly: [technical range, failed], [echo text]); (6) MMI_gen 9898 (partly: MMI_gen 4713 (partly: red)), MMI_gen 9888 (partly: reactions to failing, MMI_gen 4713 (partly: red));
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(
                @"Enter “1” (invalid value) with the numeric keypad and press the data input field (Accept) in the same screen");


            /*
            Test Step 3
            Action: Press the data input field once again (Accept) in the same screen
            Expected Result: Input Field(1) The ‘Enter’ button associated to the data area of the input field is still coloured grey and its text is black (state ‘Selected IF/data value’).(2) The ‘Enter’ button associated to the data area of the input field displays “1” (previously entered value).EVC-118(3) ETCS-DMI does not send out packet EVC-118 as the ‘Enter’ button is disabled.Echo Texts(4) The data part of the echo text displays “++++”.(5) The data part of the echo text is coloured red
            Test Step Comment: Requirements:(1) MMI_gen 9888 (partly: MMI_gen 4714 (partly: state 'Selected IF/data value');(2) MMI_gen 9888 (partly: reactions to failing, MMI_gen 4714 (partly: previously entered (faulty) value)); MMI_gen 4699 (partly: technical range);(3) MMI_gen 9888 (partly: MMI_gen 9286 (partly: button ‘Enter’, disabled), MMI_gen 12148 (partly: not send packets)), MMI_gen 9905 (partly: disabled), MMI_gen 9923 (partly: EVC-118); MMI_gen 9310 (partly: [technical range, failed], [Button ‘Enter’ disabled]);(4) MMI_gen 8328 (partly: MMI_gen 12148 (MMI_gen 4713 (partly: indication))), MMI_gen 9888 (partly: reactions to failing, MMI_gen 12148 (MMI_gen 4713 (partly: indication)));(5) MMI_gen 9898 (partly: MMI_gen 12148 (MMI_gen 4713 (partly: red))), MMI_gen 9888 (partly: reactions to failing, MMI_gen 12148 (MMI_gen 4713 (partly: red)));
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press the data input field once again (Accept) in the same screen");


            /*
            Test Step 4
            Action: Enter “1” (invalid value) with the numeric keypad in the same screen
            Expected Result: Input Field(1) The eventually displayed data value in the data area of the input field is replaced by “1” (character or value corresponding to the activated data key - state ‘Selected IF/value of pressed key(s)’)
            Test Step Comment: Requirements:(1) MMI_gen 9888 (partly: MMI_gen 4714 (partly: MMI_gen 4679), MMI_gen 9286 (partly: button ‘Enter’, enabled)), MMI_gen 9905 (partly: state switched); MMI_gen 9310 (partly: press one key);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Enter “1” (invalid value) with the numeric keypad in the same screen");
            // Call generic Check Results Method
            DmiExpectedResults
                .Input_Field1_The_eventually_displayed_data_value_in_the_data_area_of_the_input_field_is_replaced_by_1_character_or_value_corresponding_to_the_activated_data_key_state_Selected_IFvalue_of_pressed_keys(this);


            /*
            Test Step 5
            Action: Press the data input field (Accept)
            Expected Result: EVC-18(1) MMI_Q_DATA_CHECK = 1 in order to indicate the technical range check failure.(2) MMI_M_BUTTONS = 255 (no button) and the 'Yes' button is disabled.Input Field(3) The ‘Enter’ button associated to the data area of the input field is coloured grey and its text is black (state ‘Selected IF/Data value’)
            Test Step Comment: Requirements:(1) MMI_gen 9888 (partly: reactions to failing, EVC-18, MMI_gen 12147); MMI_gen 9310 (partly: [Up-type enabled button ‘Enter’], accept data); (2) MMI_gen 9901; (3) MMI_gen 9888 (partly: reactions to failing, MMI_gen 4714 (partly: state 'Selected IF/data value'));
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press the data input field (Accept)");


            /*
            Test Step 6
            Action: Press the data input field once again (Accept) in the same screen
            Expected Result: Input Field(1) The ‘Enter’ button associated to the data area of the input field is still coloured grey and its text is black (state ‘Selected IF/data value’).(2) The ‘Enter’ button associated to the data area of the input field displays “1” (previously entered value).EVC-118(3) ETCS-DMI does not send out packet EVC-118 as the ‘Enter’ button is disabled. Echo Texts(4) The data part of the echo text displays “++++”.(5) The data part of the echo text is coloured red
            Test Step Comment: Requirements:(1) MMI_gen 9888 (partly: MMI_gen 4714 (partly: state 'Selected IF/data value');(2) MMI_gen 9888 (partly: reactions to failing, MMI_gen 4714 (partly: previously entered (faulty) value)); MMI_gen 4699 (partly: technical range);(3) MMI_gen 9888 (partly: MMI_gen 9286 (partly: button ‘Enter’, disabled), MMI_gen 12148 (partly: not send packets)), MMI_gen 9905 (partly: disabled), MMI_gen 9923 (partly: EVC-118); MMI_gen 9310 (partly: [technical range, failed], [Button ‘Enter’ disabled]);(4) MMI_gen 8328 (partly: MMI_gen  (MMI_gen 4713 (partly: indication))), MMI_gen 9888 (partly: reactions to failing, MMI_gen 12148 (MMI_gen 4713 (partly: indication)));(5) MMI_gen 9898 (partly: MMI_gen 12148 (MMI_gen 4713 (partly: red))), MMI_gen 9888 (partly: reactions to failing, MMI_gen 12148 (MMI_gen 4713 (partly: red)));
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press the data input field once again (Accept) in the same screen");


            /*
            Test Step 7
            Action: Enter “65536” (valid value) with the numeric keypad
            Expected Result: Input Field(1) The eventually displayed data value in the data area of the input field is replaced by “65536” (character or value corresponding to the activated data key - state ‘Selected IF/value of pressed key(s)’)
            Test Step Comment: Requirements:(1) MMI_gen 9888 (partly: reactions to succeed, MMI_gen 4714 (partly: MMI_gen 4679), MMI_gen 9286 (partly: button ‘Enter’, enabled)), MMI_gen 9905 (partly: state switched); MMI_gen 9310 (partly: press one key);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Enter “65536” (valid value) with the numeric keypad");
            // Call generic Check Results Method
            DmiExpectedResults
                .Input_Field1_The_eventually_displayed_data_value_in_the_data_area_of_the_input_field_is_replaced_by_65536_character_or_value_corresponding_to_the_activated_data_key_state_Selected_IFvalue_of_pressed_keys(this);


            /*
            Test Step 8
            Action: Press the data input field (Accept) in the same screen
            Expected Result: EVC-118(1) ETCS-DMI sends packet EVC-118 with variable:MMI_M_VBC_CODE = 65536 MMI_M_BUTTONS =  254 (BTN_ENTER)EVC-18(2) ETCS-DMI receives packet EVC-18 with variable:MMI_Q_DATA_CHECK = 0 (All checks have passed)MMI_X_TEXT = 54 (“6”)MMI_X_TEXT = 53 (“5”)MMI_X_TEXT = 53 (“5”) MMI_X_TEXT = 51 (“3”) MMI_X_TEXT = 54 (“6”)
            Test Step Comment: Requirements:(1) MMI_gen 9888 (partly: reactions to succeed, MMI_gen 9286 (partly: enabled)), MMI_gen 9905 (partly: enabled), MMI_gen 9923 (partly: EVC-118, the ‘Enter’ button, accepted data complied with data checks, driver action); MMI_gen 9310 (partly: [technical range, passed]);(2) MMI_gen 9888 (partly: reactions to succeed, EVC-18)
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press the data input field (Accept) in the same screen");


            /*
            Test Step 9
            Action: This step is to complete the process of ‘set VBC’:- Press the ‘Yes’ button on the ‘Set VBC’ window.- Validate the data in the data validation window
            Expected Result: 1. After pressing the ‘Yes’ button, the data validation window (‘Validate Set VBC’) appears instead of the ‘Set VBC’ data entry window. The data part of echo text displays “65536” in white.2. After the data area of the input field containing “Yes” is pressed, the data validation window disappears and returns to the parent window (‘Settings’ window) of ‘Set VBC’ window with enabled ‘Set VBC’ button
            */
            // Call generic Action Method
            DmiActions
                .This_step_is_to_complete_the_process_of_set_VBC_Press_the_Yes_button_on_the_Set_VBC_window_Validate_the_data_in_the_data_validation_window(this);
            // Call generic Check Results Method
            DmiExpectedResults
                .After_pressing_the_Yes_button_the_data_validation_window_Validate_Set_VBC_appears_instead_of_the_Set_VBC_data_entry_window_The_data_part_of_echo_text_displays_65536_in_white_2_After_the_data_area_of_the_input_field_containing_Yes_is_pressed_the_data_validation_window_disappears_and_returns_to_the_parent_window_Settings_window_of_Set_VBC_window_with_enabled_Set_VBC_button(this);


            /*
            Test Step 10
            Action: Send the data of ‘Technical Range Check’ failure to ETCS-DMI by 22_27_3_a.xml,EVC-18MMI_Q_DATA_CHECK = 1 (Technical Range Check Failed)
            Expected Result: Input Field(1) The ‘Enter’ button associated to the data area of the input field displays previously entered value.Echo Texts(2) The data part of the echo text displays “++++”
            Test Step Comment: (1) MMI_gen 9888 (partly: reactions to failing, MMI_gen 4714 (partly: previously entered (faulty) value)); MMI_gen 4699 (partly: technical range);(2) MMI_gen 8328 (partly: MMI_gen  (MMI_gen 4713 (partly: indication))), MMI_gen 9888 (partly: reactions to failing, MMI_gen 12148 (MMI_gen 4713 (partly: indication)));Note: This is a temporary approach for non-support test environment on the data checks.
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
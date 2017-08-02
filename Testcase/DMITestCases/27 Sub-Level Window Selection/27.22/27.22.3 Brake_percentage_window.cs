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
    /// 27.22.3 Brake percentage window
    /// TC-ID: 22.22.3 
    /// 
    /// This test case verifies the display of the Brake percentage window shall comply with [ERA-ERTMS] standard and [MMI-ETCS-gen].
    /// 
    /// Tested Requirements:
    /// MMI_gen 11829; MMI_gen 11675; MMI_gen 11677; MMI_gen 11830; MMI_gen 11678; MMI_gen 11831; MMI_gen 11681; MMI_gen 11680; MMI_gen 11896; MMI_gen 11828; MMI_gen 11821; MMI_gen 11822; MMI_gen 11823; MMI_gen 11824; MMI_gen 11825; MMI_gen 11727 (partly: MMI_gen 5189 (partly: touch screen), MMI_gen 5944 (partly: touch screen), half grid array, MMI_gen 4720, MMI_gen 4722 (partly: Close button, Window Title, Input field), MMI_gen 4637 (partly: Main-areas D and F), MMI_gen 4640, MMI_gen 4912, MMI_gen 4678, MMI_gen 5003, MMI_gen 4697, MMI_gen 4696, MMI_gen 4701, MMI_gen 4702 (partly: right aligned), MMI_gen 4704 (partly: left aligned), MMI_gen 4700, MMI_gen 4913 (partly: MMI_gen 4384, MMI_gen 4386 (partly: except 0.3s)), MMI_gen 4634, MMI_gen 4651, MMI_gen 4647 (partly: left aligned), MMI_gen 4679, MMI_gen 4642, MMI_gen 4681, MMI_gen 4689, MMI_gen 4690, MMI_gen 4691 (partly: flashing), MMI_gen 4692, MMI_gen 4634, MMI_gen 4647 (partly: left aligned)); MMI_gen 4392 (partly: [Delete] NA21, [Close] NA11, returning to the parent window, [Enter], touch screen); MMI_gen 4393 (partly: [Delete]); MMI_gen 4241; MMI_gen 4350; MMI_gen 4351; MMI_gen 4353; MMI_gen 11679; MMI_gen 9390 (partly: Brake percentage window); MMI_gen 8864 (partly: Brake percentage window);
    /// 
    /// Scenario:
    /// Brake percentage window, the window appearance is verified.The data entry functionality of the Brake percentage window is verified with the Down-type button in keypad.The data revalidation functionality of the Brake percentage window is verified.The functionality of ‘Close’ button is verified.
    /// 
    /// Used files:
    /// 22_22_3_a.xml, 22_22_3_b.xml
    /// </summary>
    public class Brake_percentage_window : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // Configure atpcu configuration file as following (See the instruction in Appendix 2)M_InstalledLevels = 31NID_NTC_Installe_0 = 22 (ATC-2)Test system is powered onCabin is activatedLevel ATC-2 is selected and confirmed.Perform SoM until the train running number is entered.Settings window is openedBrake window is opened

            // Call the TestCaseBase PreExecution
            base.PreExecution();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in SB mode, level 1

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint


            /*
            Test Step 1
            Action: Use the test script file 22_22_3_a.xmlSend EVC-30 with,MMI_NID_WINDOW = 4MMI_Q_REQUEST_ENABLE_64 (#31) = 1
            Expected Result: The ‘Brake percentage’ button is enabled
            */


            /*
            Test Step 2
            Action: Press ‘Brake percentage’ button
            Expected Result: DMI displays the Brake percentage window on the right half part of the window.LayersThe layers of window on half-grid array is displayed as followsLayer 0: Main-Area D, F, G, Y and Z.Layer -1: A1, A2+A3*, A4, B*, C1, C2+C3+C4*, C5, C6, C7, C8, C9, E1, E2, E3, E4, E5-E9*Layer -2: B3, B4, B5, B6, B7Note: ‘*’ symbol is mean that specified areas are drawn as one area.Data Entry windowThe window title is displayed with text “Brake percentage”.Verify that the Maintenance password window is displayed in main area D, F and G as half-grid array.A data entry window is containing only one input field covers the Main area D, F and GThe following objects are displayed in Maintenance password window. Enabled Close button (NA11)Window TitleInput FieldInput fieldThe input field is located in main area D and F.Each input field is devided into a Label Area and a Data AreaThe label of input field is ‘Enter brake percentage’.KeyboardThe keyboard associated to the Brake percentage window is displayed as numeric keyboard.The keyboard is presented below the area of input field.The keyboard contains enabled button for the number <1>, <2 >, … , <9 >, <Delete>(NA21), <0> and disabled <Decimal_Separator>.  NA21, Delete button.Echo textAn echo text is composed of Label Part and Data Part.The Label Part of an echo texts is same as The Label area of an input fields.The echo texts are displayed in main area A, B, C and E with same order as their related input fields.The Label part of echo texts are right aligned.The Data part of echo texts are left aligned.The colour of texts in echo texts are grey.The label of first echo field is ‘Original BP’.The label of second echo field is ‘Last measured BP’.The label of third echo field is ‘brake percentage’.Packet transmissionUse the log file to confirm that DMI displays Brake percentage window refer to received packet information [MMI_CURRENT_BRAKE_PERCENTAGE (EVC-50)].The data part of input field and echo text are displayed correspond with the variables in received packet EVC-50 as follows,MMI_M_BP_ORIG = Original BPMMI_M_BP_MEASURED = Last measured BPMMI_M_BP_CURRENT = brake percentageGeneral property of windowThe Brake percentage window is presented with objects, text messages and buttons which is the one of several levels and allocated to areas of DMIAll objects, text messages and buttons are presented within the same layer.The Default window is not displayed and covered the current window
            Test Step Comment: (1) MMI_gen 11727 (partly: MMI_gen 5189 (partly: touch screen), MMI_gen 5944 (partly: touch screen));(2) MMI_gen 11675;(3) MMI_gen 11727 (partly: half grid array);(4) MMI_gen 11727 (partly: MMI_gen 4720);(5) MMI_gen 11727 (partly: MMI_gen 4722 (partly: Close button, Window Title, Input field)); MMI_gen 4392 (partly: [Close] NA11);(6) MMI_gen 11727 (partly: MMI_gen 4637 (partly: Main-areas D and F));(7) MMI_gen 11727 (partly: MMI_gen 4640);(8) MMI_gen 11896 (partly: label);(9) MMI_gen 11727 (partly: MMI_gen 4912); MMI_gen 11822;(10) MMI_gen 11727 (partly: MMI_gen 4678);(11) MMI_gen 11727 (partly: MMI_gen 5003); MMI_gen 4392 (partly: [Delete] NA21);(12) MMI_gen 11727 (partly: MMI_gen 4696); (13) MMI_gen 11727 (partly: MMI_gen 4697);  (14) MMI_gen 11727 (partly: MMI_gen 4701); (15) MMI_gen 11727 (partly: MMI_gen 4702 (partly: right aligned)); (16) MMI_gen 11727 (partly: MMI_gen 4704 (partly: left aligned)); (17) MMI_gen 11727 (partly: MMI_gen 4700 (partly: otherwise, grey)); MMI_gen 4241; (18) MMI_gen 11677 (partly: label); (19) MMI_gen 11678 (partly: label);(20) MMI_gen 11681 (partly: label);(21) MMI_gen 11829;(22) MMI_gen 11830; MMI_gen 11831; MMI_gen 11680; MMI_gen 11821; MMI_gen 11677 (partly: value); MMI_gen 11678 (partly: value); MMI_gen 11681 (partly: value);(23) MMI_gen 4350;(24) MMI_gen 4351;(25) MMI_gen 4353;
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press ‘Brake percentage’ button");


            /*
            Test Step 3
            Action: Press and hold ‘0’ button
            Expected Result: Verify the following information,While press and hold button less than 1.5 secSound ‘Click’ is played once.The state of button is changed to ‘Pressed’ and immediately back to ‘Enabled’ state.The Input Field displays the language associated to the data key according to the pressings in state ‘Pressed’.An input field is used to enter the Brake percentage.The data value is displayed as black colour and the background of the data area is displayed as medium-grey colour.The data value of the input field is aligned to the left of the data area.Only data field with the ‘Enter brake percentage’ is editable by driver
            Test Step Comment: (1) MMI_gen 11727 (partly: MMI_gen 4913 (partly: MMI_gen 4384 (partly: sound ‘Click’)));(2) MMI_gen 11727 (partly: MMI_gen 4913 (partly: MMI_gen 4384 (partly: Change to state ‘Pressed’ and immediately back to state ‘Enabled’)));   (3) MMI_gen 11727 (partly: MMI_gen 4913);                      (4) MMI_gen 11896 (partly: entry); MMI_gen 11727 (partly: MMI_gen 4634);(5) MMI_gen 11727 (partly: MMI_gen 4651);(6) MMI_gen 11727 (partly: MMI_gen 4647 (partly: left aligned));(7) MMI_gen 11828;
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press and hold ‘0’ button");


            /*
            Test Step 4
            Action: Released the pressed button
            Expected Result: Verify the following information, The state of button is changed to ‘Enabled’
            Test Step Comment: (1) MMI_gen 11727 (partly: MMI_gen 4913 (partly: MMI_gen 4384 (partly: ETCS-MMI’s function associated to the button)));
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Released the pressed button");
            // Call generic Check Results Method
            DmiExpectedResults.Verify_the_following_information_The_state_of_button_is_changed_to_Enabled(this);


            /*
            Test Step 5
            Action: Perform action step 3-4 for the ‘1’ to ‘9’ buttons.Note: Press the ‘Del’ button to delete an information when entered data is out of input field range is acceptable
            Expected Result: See the expected results of Step 3 – Step 4 and the following additional information,The pressed key is added in an input field immediately. The cursor is jumped to next position after entered the character immediately
            Test Step Comment: (1) MMI_gen 11727 (partly: MMI_gen 4642);  (2) MMI_gen 11727 (partly: MMI_gen 4692);
            */
            // Call generic Action Method
            DmiActions
                .Perform_action_step_3_4_for_the_1_to_9_buttons_Note_Press_the_Del_button_to_delete_an_information_when_entered_data_is_out_of_input_field_range_is_acceptable(this);
            // Call generic Check Results Method
            DmiExpectedResults
                .See_the_expected_results_of_Step_3_Step_4_and_the_following_additional_information_The_pressed_key_is_added_in_an_input_field_immediately_The_cursor_is_jumped_to_next_position_after_entered_the_character_immediately(this);


            /*
            Test Step 6
            Action: Press and hold ‘Del’ button.Note: Stopwatch is required
            Expected Result: Verify the following information,While press and hold button less than 1.5 secSound ‘Click’ is played once.The state of button is changed to ‘Pressed’ and immediately back to ‘Enabled’ state.The last character is removed from an input field after pressing the button.While press and hold button over 1.5 secThe state ‘pressed’ and ‘released’ are switched repeatly while button is pressed and the characters are removed from an input field repeatly refer to pressed state.The sound ‘Click’ is played repeatly while button is pressed
            Test Step Comment: (1) MMI_gen 11727 (partly: MMI_gen 4913 (partly: MMI_gen 4384 (partly: sound ‘Click’)));(2) MMI_gen 11727 (partly: MMI_gen 4913 (partly: MMI_gen 4384 (partly: Change to state ‘Pressed’ and immediately back to state ‘Enabled’)));(3) MMI_gen 11727 (partly: MMI_gen 4913 (partly: MMI_gen 4384 (partly: ETCS-MMI’s function associated to the button))); MMI_gen 4393 (partly: [Delete]);(4) MMI_gen 11727 (partly: MMI_gen 4913 (partly: MMI_gen 4386 (partly: visual of repeat function)));(5) MMI_gen 11727 (partly: MMI_gen 4913 (partly: MMI_gen 4386 (partly: audible of repeat function)));
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press and hold ‘Del’ button.Note: Stopwatch is required");
            // Call generic Check Results Method
            DmiExpectedResults
                .Verify_the_following_information_While_press_and_hold_button_less_than_1_5_secSound_Click_is_played_once_The_state_of_button_is_changed_to_Pressed_and_immediately_back_to_Enabled_state_The_last_character_is_removed_from_an_input_field_after_pressing_the_button_While_press_and_hold_button_over_1_5_secThe_state_pressed_and_released_are_switched_repeatly_while_button_is_pressed_and_the_characters_are_removed_from_an_input_field_repeatly_refer_to_pressed_state_The_sound_Click_is_played_repeatly_while_button_is_pressed(this);


            /*
            Test Step 7
            Action: Release ‘Del’ button
            Expected Result: Verify the following information, The character is stop removing
            Test Step Comment: (1) MMI_gen 11727 (partly: MMI_gen 4913 (partly: MMI_gen 4384 (partly: ETCS-MMI’s function associated to the button))); 
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Release ‘Del’ button");
            // Call generic Check Results Method
            DmiExpectedResults.Verify_the_following_information_The_character_is_stop_removing(this);


            /*
            Test Step 8
            Action: Press ‘Del’ button on the numeric keyboard until no number is displayed on the Input Field
            Expected Result: No number is displayed on the Input Field
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this,
                @"Press ‘Del’ button on the numeric keyboard until no number is displayed on the Input Field");
            // Call generic Check Results Method
            DmiExpectedResults.No_number_is_displayed_on_the_Input_Field(this);


            /*
            Test Step 9
            Action: Enter the data value with 3 characters
            Expected Result: On next activation of a data key of the associated keyboard, the character or value corresponding to this data key shall be added into the Data Area.The data value is displayed as black colour and the background of the data area is displayed as medium-grey colour.The flashing horizontal-line cursor is always in the next position of the echoed entered-data key in the ‘Selected IF/value of pressed key(s)’ data input field when selected the next character it will be inserted cursor position.An input field is used to enter the brake percentage.The data value of the input field is aligned to the left of the data area
            Test Step Comment: (1) MMI_gen 11727 (partly: MMI_gen 4679, MMI_gen 4642);(2) MMI_gen 11727 (partly: MMI_gen 4651);(3) MMI_gen 11727 (partly: MMI_gen 4689, MMI_gen 4690, MMI_gen 4691 (partly: flashing), MMI_gen 4692);(4) MMI_gen 11896 (partly: entry); MMI_gen 11727 (partly: MMI_gen 4634);(5) MMI_gen 11727 (partly: MMI_gen 4647 (partly: left aligned));
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Enter the data value with 3 characters");


            /*
            Test Step 10
            Action: Delete the old value and enter the value ‘100’ for brake percentage.Then, confirm an entered data by pressing an input field
            Expected Result: Verify the following information,Packet TransmissionUse the log file to confirm that DMI sent out packet [MMI_NEW_BRAKE_PERCENTAGE (EVC-150)] with following variables,MMI_M_BP_CURRENT = 100Use the log file to confirm that the Brake percentage window is closed because of DMI received packet information [MMI_ECHOED_BRAKE_PERCENTAGE (EVC-51)]
            Test Step Comment: (1) MMI_gen 11823; MMI_gen 4392 (partly: [Enter], touch screen); MMI_gen 4681 (partly: accept the entered value in the input field); MMI_gen 8864 (partly: replace the data value by pressing the input field);(2) MMI_gen 11825;
            */


            /*
            Test Step 11
            Action: Press ‘Brake percentage’ button
            Expected Result: DMI displays Brake percentage window.The value of Brake percentage echo text is changed refer to ‘100’ same as entered data
            Test Step Comment: (1) MMI_gen 11727 (partly: MMI_gen 4681 (partly: accept the entered value));
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press ‘Brake percentage’ button");


            /*
            Test Step 12
            Action: Delete the old value and enter the value ‘99’ for brake percentage.Then, press and hold an input field
            Expected Result: Verify the following information,(1)    The state of an input field is changed to ‘Pressed’, the border of button is removed
            Test Step Comment: (1) MMI_gen 9390 (partly: Brake percentage window);
            */
            // Call generic Check Results Method
            DmiExpectedResults
                .Verify_the_following_information_1_The_state_of_an_input_field_is_changed_to_Pressed_the_border_of_button_is_removed(this);


            /*
            Test Step 13
            Action: Slide out an input field
            Expected Result: Verify the following information,(1)     The state of an input field is changed to ‘Enabled, the border of button is shown without a sound
            Test Step Comment: (1) MMI_gen 9390 (partly: Brake percentage window);
            */
            // Call generic Action Method
            DmiActions.Slide_out_an_input_field(this);
            // Call generic Check Results Method
            DmiExpectedResults
                .Verify_the_following_information_1_The_state_of_an_input_field_is_changed_to_Enabled_the_border_of_button_is_shown_without_a_sound(this);


            /*
            Test Step 14
            Action: Slide back into an input field
            Expected Result: Verify the following information,(1)    The state of an input field is changed to ‘Pressed’, the border of button is removed
            Test Step Comment: (1) MMI_gen 9390 (partly: Brake percentage window);
            */
            // Call generic Action Method
            DmiActions.Slide_back_into_an_input_field(this);
            // Call generic Check Results Method
            DmiExpectedResults
                .Verify_the_following_information_1_The_state_of_an_input_field_is_changed_to_Pressed_the_border_of_button_is_removed(this);


            /*
            Test Step 15
            Action: Release the pressed area
            Expected Result: An input field is used to revalidate the Brake percentage, DMI displays Brake window
            Test Step Comment: (1) MMI_gen 11896 (partly: revalidation);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Release the pressed area");


            /*
            Test Step 16
            Action: Press ‘Brake percentage’ button
            Expected Result: DMI displays Brake percentage window.(1)   The value of Brake percentage echo text is changed refer to ‘99’ same as entered data
            Test Step Comment: (1) MMI_gen 11727 (partly: MMI_gen 4681 (partly: replace the current data)); MMI_gen 8864 (partly: Brake percentage window);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press ‘Brake percentage’ button");


            /*
            Test Step 17
            Action: Use the test script file 22_22_3_b.xmlSend EVC-50 with,MMI_M_BP_MEASURED = 255
            Expected Result: Verify the following information,(1)    The value of echo text ‘Last measured BP’ is show as “_ _ _ _”
            Test Step Comment: (1) MMI_gen 11679; MMI_gen 11831 (partly: MMI_gen 11679);
            */


            /*
            Test Step 18
            Action: Press ‘Close’ button
            Expected Result: Verify the following information,Use the log file to confirm that DMI sent out packet [MMI_DRIVER_REQUEST (EVC-101)] with variable MMI_M_REQUEST = 60 (Exit brake percentage).The Brake percentage window is closed. DMI displays Brake window
            Test Step Comment: (1) MMI_gen 11824 (partly: EVC-101);(2) MMI_gen 11824 (partly: open ‘Brake’ window); MMI_gen 4392 (partly: returning to the parent window);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press ‘Close’ button");


            /*
            Test Step 19
            Action: End of test
            Expected Result: 
            */


            return GlobalTestResult;
        }
    }
}
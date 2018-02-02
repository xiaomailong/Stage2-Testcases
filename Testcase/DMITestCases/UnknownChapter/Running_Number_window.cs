namespace Testcase.DMITestCases
{
    /// <summary>
    /// Train Running Number window
    /// TC-ID: 22.18
    /// 
    /// This test case verifies the display of the Train Running Number window that shall comply with [ERA-ERTMS] standard and [MMI-ETCS-gen].
    /// 
    /// Tested Requirements:
    /// MMI_gen 9959; MMI_gen 9961; MMI_gen 7978; MMI_gen 7979; MMI_gen 9958; MMI_gen 7980; MMI_gen 7977 (partly: half grid array, single input field, only data part, MMI_gen 5189 (partly: touch screen), MMI_gen 5944 (partly: touch screen), MMI_gen 4640 (partly: only data area), MMI_gen 4720, MMI_gen 4889 (partly: merge label and data), MMI_gen 4722 (partly: Table 12 <Close> button, Window title, Input field), MMI_gen 4637 (partly: Main-areas D and F), note under the MMI_gen 9412, MMI_gen 4912, MMI_gen 4678, MMI_gen 5003, MMI_gen 4913 (MMI_gen 4384, MMI_gen 4386 (partly: except 0.3s)), MMI_gen 4634, MMI_gen 4651, MMI_gen 4679, MMI_gen 4642, MMI_gen 4689, MMI_gen 4690, MMI_gen 4691 (partly: flashing), MMI_gen 4692, MMI_gen 4647 (partly: left aligned), MMI_gen 4681, MMI_gen 4684 (partly: terminated), MMI_gen 4694 (partly: MMI_gen 4246), MMI_gen 4682); MMI_gen 4392 (partly: [Delete] NA21, [Close] NA11, returning to the parent window, [Enter], touch screen); MMI_gen 4393 (partly: [Delete]); MMI_gen 4350; MMI_gen 4351; MMI_gen 4353; MMI_gen 9390 (partly: Train Running Number window); MMI_gen 8864 (partly: Train Running Number window);
    /// 
    /// Scenario:
    /// The test system information of functionality is powered on and the cabin is inactive.Train Running Number window in cabin inactive is verified.Activate cabin. Then, verify Train Running Number window can enter from Driver ID window.The Down-type button on keypad is verified.The data entry functionality of the Train Running Number window is verified.The revalidate data entry of the Train Running Number window is verified.The window closure is verified.
    /// 
    /// Used files:
    /// 22_18.xml
    /// </summary>
    public class Running_Number_window : TestcaseBase
    {

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint


            /*
            Test Step 1
            Action: Use the test script file 22_18.xml to send EVC-16 with,MMI_NID_OPERATION = 1Note: Please wait for 5-10 seconds to make sure that test script is executed completely
            Expected Result: Train Running Number window is not displayed
            Test Step Comment: (1) MMI_gen 9958 (partly: inactive);
            */


            /*
            Test Step 2
            Action: Activate cabin A and press ‘TRN’ button on the Driver ID window and verify the presentation on the screen
            Expected Result: The Train Running Number window is displayed on the right half part of the window as shown in figure belowLayersThe layers of window on half-grid array is displayed as followsLayer 0: Main-Area D, F, G, Y and Z.Layer -1: A1, A2+A3*, A4, B*, C1, C2+C3+C4*, C5, C6, C7, C8, C9, E1, E2, E3, E4, E5-E9*Layer -2: B3, B4, B5, B6, B7Note: ‘*’ symbol is mean that specified areas are drawn as one area.Data Entry windowThe window title is displayed with text ‘Train running number’.Verify that the Train Running Number window is displayed in main area D, F and G as half-grid array.A data entry window is containing only one input field covers the Main area D, F and GThe following objects are displayed in Train Running Number window. Enabled Close button (NA11)Window TitleInput FieldInput fieldThe input field is located in main area D and F.For a single input field, the window title is clearly explaining the topic of the input field. The Train Running Number window is displayed as a single input field with only the data part.KeyboardThe keyboard associated to the Train Running Number window is displayed as numeric keyboard.The keyboard is presented below the area of input field.The keyboard contains enabled button for the number <1>, <2 >, … , <9 >, <Delete>(NA21), <0> and disabled <Decimal_Separator>.  NA21, Delete button.Packet ReceivingDMI displays 'Train Running Number' window with the data stored onboard from EVC-16 with variable MMI_NID_OPERATION = TRN (The data is the same as displayed on DMI).General property of windowThe Train Running Number window is presented with objects and buttons which is the one of several levels and allocated to areas of DMIAll objects, text messages and buttons are presented within the same layer.The Default window is not displayed and covered the current window
            Test Step Comment: (1) MMI_gen 7977 (partly: MMI_gen 5189 (partly: touch screen), MMI_gen 5944 (partly: touch screen)));(2) MMI_gen 7978;(3) MMI_gen 7977 (partly: half grid array);(4) MMI_gen 7977 (partly: MMI_gen 4640 (partly: only data area), MMI_gen 4720, MMI_gen 4889 (partly: merge label and data));(5) MMI_gen 7977 (party: MMI_gen 4722 (partly: Table 12 <Close> button, Window title , Input field)); MMI_gen 4392 (partly: [Close] NA11);(6) MMI_gen 7977 (partly: MMI_gen 4637 (partly: Main-areas D and F));(7) MMI_gen 7977 (partly: note under the MMI_gen 9412);(8) MMI_gen 7977 (partly: single input field, only data part);(9) MMI_gen 7980; MMI_gen 7977 (partly: MMI_gen 4912);(10) MMI_gen 7977 (partly: MMI_gen 4678);(11) MMI_gen 7977 (partly: MMI_gen 5003); MMI_gen 4392 (partly: [Delete] NA21);(12) MMI_gen 9958 (partly: Display Train Running Number window, EVC-16 with MMI_NID_OPERATION = TRN);(13) MMI_gen 4350;(14) MMI_gen 4351;(15) MMI_gen 4353;
            */


            /*
            Test Step 3
            Action: Press and hold every buttons on the dedicate keyboard respectively.Note:This step is for testing ’0’-‘9’ button
            Expected Result: Verify the following information,On next activation of a data key of the associated keyboard, the character or value corresponding to this data key shall be added into the Data Area.Sound ‘Click’ is played once.The state of button is changed to ‘Pressed’ and immediately back to ‘Enabled’ state.The Input Field displays the number associated to the data key according to the pressings in state ‘Pressed’.An input field is used to enter the Train Running Number.The data value is displayed as black colour and the background of the data area is displayed as medium-grey colour.The data value of the input field is aligned to the left of the data area.The flashing horizontal-line cursor is always in the next position of the echoed entered-data key in the ‘Selected IF/value of pressed key(s)’ data input field when selected the next character it will be inserted cursor position
            Test Step Comment: (1) MMI_gen 7977 (partly: MMI_gen 4679, MMI_gen 4642);(2) MMI_gen 7977 (partly: MMI_gen 4913 (partly: MMI_gen 4384 (partly: sound ‘Click’)));(3) MMI_gen 7977 (partly: MMI_gen 4913 (partly: MMI_gen 4384 (partly: Change to state ‘Pressed’ and immediately back to state ‘Enabled’)));   (4) MMI_gen 7977 (partly: MMI_gen 4913);                      (5) MMI_gen 7979 (partly: entry); MMI_gen 7977 (partly: MMI_gen 4634);(6) MMI_gen 7977 (partly: MMI_gen 4651);(7) MMI_gen 7977 (partly: MMI_gen 4647 (partly: left aligned));(8) MMI_gen 7977 (partly: MMI_gen 4689, MMI_gen 4690, MMI_gen 4691 (partly: flashing), MMI_gen 4692);
            */


            /*
            Test Step 4
            Action: Released the pressed button
            Expected Result: Verify the following information, The state of button is changed to ‘Enabled’
            Test Step Comment: (1) MMI_gen 7977 (partly: MMI_gen 4913 (partly: MMI_gen 4384 (partly: ETCS-MMI’s function associated to the button)));
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Released the pressed button");
            // Call generic Check Results Method
            DmiExpectedResults.Verify_the_following_information_The_state_of_button_is_changed_to_Enabled(this);


            /*
            Test Step 5
            Action: Press and hold ‘Del’ button.Note: Stopwatch is required
            Expected Result: Verify the following information,While press and hold button less than 1.5 secSound ‘Click’ is played once.The state of button is changed to ‘Pressed’ and immediately back to ‘Enabled’ state.The last character is removed from an input field after pressing the button.While press and hold button over 1.5 secThe state ‘pressed’ and ‘released’ are switched repeatly while button is pressed and the characters are removed from an input field repeatly refer to pressed state.The sound ‘Click’ is played repeatly while button is pressed
            Test Step Comment: (1) MMI_gen 7977 (partly: MMI_gen 4913 (partly: MMI_gen 4384 (partly: sound ‘Click’)));(2) MMI_gen 7977 (partly: MMI_gen 4913 (partly: MMI_gen 4384 (partly: Change to state ‘Pressed’ and immediately back to state ‘Enabled’)));(3) MMI_gen 7977 (partly: MMI_gen 4913 (partly: MMI_gen 4384 (partly: ETCS-MMI’s function associated to the button))); MMI_gen 4393 (partly: [Delete]);(4) MMI_gen 7977 (partly: MMI_gen 4913 (partly: MMI_gen 4386 (partly: visual of repeat function)));(5) MMI_gen 7977 (partly: MMI_gen 4913 (partly: MMI_gen 4386 (partly: audible of repeat function)));
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press and hold ‘Del’ button.Note: Stopwatch is required");
            // Call generic Check Results Method
            DmiExpectedResults
                .Verify_the_following_information_While_press_and_hold_button_less_than_1_5_secSound_Click_is_played_once_The_state_of_button_is_changed_to_Pressed_and_immediately_back_to_Enabled_state_The_last_character_is_removed_from_an_input_field_after_pressing_the_button_While_press_and_hold_button_over_1_5_secThe_state_pressed_and_released_are_switched_repeatly_while_button_is_pressed_and_the_characters_are_removed_from_an_input_field_repeatly_refer_to_pressed_state_The_sound_Click_is_played_repeatly_while_button_is_pressed(
                    this);


            /*
            Test Step 6
            Action: Release ‘Del’ button
            Expected Result: Verify the following information, The character is stop removing
            Test Step Comment: (1) MMI_gen 7977 (partly: MMI_gen 4913 (partly: MMI_gen 4384 (partly: ETCS-MMI’s function associated to the button))); 
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Release ‘Del’ button");
            // Call generic Check Results Method
            DmiExpectedResults.Verify_the_following_information_The_character_is_stop_removing(this);


            /*
            Test Step 7
            Action: Press ‘Del’ button on the numeric keyboard until no number is displayed on the Input Field
            Expected Result: No number is displayed on the Input Field
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this,
                @"Press ‘Del’ button on the numeric keyboard until no number is displayed on the Input Field");
            // Call generic Check Results Method
            DmiExpectedResults.No_number_is_displayed_on_the_Input_Field(this);


            /*
            Test Step 8
            Action: Enter the data value with 5 characters
            Expected Result: The 5 characters are added on an input field as one group. (e.g. ‘12345')
            Test Step Comment: (1) MMI_gen 7977 (partly: MMI_gen 4694 (partly: NEGATIVE, 6th character));
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Enter the data value with 5 characters");


            /*
            Test Step 9
            Action: Continue to enter the 6th character
            Expected Result: Verify the following information,The fifth character is shown after a gap of fourth character, separated as 2 groups (e.g. 1234 56)
            Test Step Comment: (1) MMI_gen 7977 (partly: MMI_gen 4694 (partly: MMI_gen 4246));
            */
            // Call generic Action Method
            DmiActions.Continue_to_enter_the_6th_character(this);
            // Call generic Check Results Method
            DmiExpectedResults
                .Verify_the_following_information_The_fifth_character_is_shown_after_a_gap_of_fourth_character_separated_as_2_groups_e_g_1234_56(
                    this);


            /*
            Test Step 10
            Action: Press and hold an input field
            Expected Result: Verify the following information,(1)    The state of an input field is changed to ‘Pressed’, the border of button is removed
            Test Step Comment: (1) MMI_gen 9390 (partly: Train Running Number window);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press and hold an input field");
            // Call generic Check Results Method
            DmiExpectedResults
                .Verify_the_following_information_1_The_state_of_an_input_field_is_changed_to_Pressed_the_border_of_button_is_removed(
                    this);


            /*
            Test Step 11
            Action: Slide out an input field
            Expected Result: Verify the following information,(1)    The state of an input field is changed to ‘Enabled, the border of button is shown without a sound
            Test Step Comment: (1) MMI_gen 9390 (partly: Train Running Number window);
            */
            // Call generic Action Method
            DmiActions.Slide_out_an_input_field(this);
            // Call generic Check Results Method
            DmiExpectedResults
                .Verify_the_following_information_1_The_state_of_an_input_field_is_changed_to_Enabled_the_border_of_button_is_shown_without_a_sound(
                    this);


            /*
            Test Step 12
            Action: Slide back into an input field
            Expected Result: Verify the following information,(1)     The state of an input field is changed to ‘Pressed’, the border of button is removed
            Test Step Comment: (1) MMI_gen 9390 (partly: Train Running Number window);
            */
            // Call generic Action Method
            DmiActions.Slide_back_into_an_input_field(this);
            // Call generic Check Results Method
            DmiExpectedResults
                .Verify_the_following_information_1_The_state_of_an_input_field_is_changed_to_Pressed_the_border_of_button_is_removed(
                    this);


            /*
            Test Step 13
            Action: Release the pressed area
            Expected Result: The Train Running Number window is closed. DMI displays the Driver ID window.Use the log file to confirm that DMI sent out packet EVC-116 with variable MMI_NID_OPERATION = TRN (The entered and confirmed value in the data entry window)Note: A value of MMI_NID_OPERATION shows as hexadecimal value of ASCII which corresponds to its character that displayed in the input field in the Train running number window
            Test Step Comment: (1) MMI_gen 9959 (partly: switch back to the previous window); MMI_gen 7977 (partly: MMI_gen 4681 (partly: accept the entered value), MMI_gen 4684 (partly: terminated));(2) MMI_gen 7979 (partly: entry); MMI_gen 9959 (partly: EVC-116); MMI_gen 7977 (partly: MMI_gen 4682); MMI_gen 4392 (partly: [Enter], touch screen); MMI_gen 9390 (partly: Train Running Number window);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Release the pressed area");


            /*
            Test Step 14
            Action: Perform the following procedure,Enter and confirm the Driver IDSelect and confirm Level 1
            Expected Result: DMI displays Main window
            */
            // Call generic Check Results Method
            DmiExpectedResults.DMI_displays_Main_window(this);


            /*
            Test Step 15
            Action: Press ‘Train Running Number’ button on Main window
            Expected Result: The Train Running Number window is displayed. An input field is used to revalidation the Train running number
            Test Step Comment: (1) MMI_gen 7979 (partly: revalidation);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press ‘Train Running Number’ button on Main window");


            /*
            Test Step 16
            Action: Confirm the current data without re-entry Train running number
            Expected Result: The Train Running Number window is closed. DMI displays the Main window.Use the log file to confirm that DMI sent out packet EVC-116 with variable MMI_NID_OPERATION = TRN (The entered and confirmed value in the data entry window)Note: A value of MMI_NID_OPERATION shows as hexadecimal value of ASCII which corresponds to its character that displayed in the input field in the Driver ID window
            Test Step Comment: (1) MMI_gen 9959 (partly: switch back to the previous window);(2) MMI_gen 7979 (partly: revalidation); MMI_gen 9959 (partly: EVC-116);
            */


            /*
            Test Step 17
            Action: Press ‘Train Running Number’ button on Main window
            Expected Result: The Train Running Number window is displayed
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press ‘Train Running Number’ button on Main window");
            // Call generic Check Results Method
            DmiExpectedResults.TRN_window_displayed(this);


            /*
            Test Step 18
            Action: Enter the new Train running number
            Expected Result: The current data in the input field is replaced by the entered data from the driver
            */


            /*
            Test Step 19
            Action: Confirm the entered value by pressing an input field
            Expected Result: (1)    DMI closes the Train Running Number window and displays Main window
            Test Step Comment: (1) MMI_gen 4681 (partly: accept the entered value in the input field); MMI_gen 8864 (partly: the driver accept the data value by pressing the input field);
            */


            /*
            Test Step 20
            Action: Press ‘Train Running Number’ button on Main menu window
            Expected Result: The Train Running Number window is displayed.(1)   DMI displays ‘Train Running Number’ window with the entered data that confirmed in Step 19. This data stored onboard is received from EVC-16 with variable MMI_NID_OPERATION = TRN (The data is the same as displayed on DMI)
            Test Step Comment: (1) MMI_gen 7977 (partly: MMI_gen 4681 (partly: entered data is replaced)); MMI_gen 8864 (partly: Train Running Number window);
            */


            /*
            Test Step 21
            Action: Close the Train Running Number window and verify the presentation on the screen
            Expected Result: The Train Running Number window is closed. DMI displays the Main window. Use the log file to confirm that DMI sends out EVC-101 with variable MMI_M_REQUEST = 31 (Exit Change Train Running Number)
            Test Step Comment: (1) MMI_gen 9961; MMI_gen 4392 (partly: returning to the parent window);
            */


            /*
            Test Step 22
            Action: End of test
            Expected Result: 
            */


            return GlobalTestResult;
        }
    }
}
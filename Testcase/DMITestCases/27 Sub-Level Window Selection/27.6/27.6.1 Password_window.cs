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
    /// 27.6.1 Password window
    /// TC-ID: 22.6.1
    /// 
    /// This test case verifies the display of the Maintenance Password window which complies with [ERA-ERTMS] standard and [MMI-ETCS-gen].The window operation shall be closed when driver entered wrong password. On the other hand, Maintenance window is displayed, if driver entered correct password.Moreover, this test case also verifies the enable/disable of Maintenance button and the password is configurable via configuration file.
    /// 
    /// Tested Requirements:
    /// MMI_gen 11718; MMI_gen 11720; MMI_gen 11721; MMI_gen 11693; MMI_gen 11722; MMI_gen 11731; MMI_gen 11732; MMI_gen 11719 (partly: half grid array, single input field, only data part, MMI_gen 5189 (partly: touch screen), MMI_gen 5944 (partly: touch screen), MMI_gen 4640 (partly: only data area), MMI_gen 4720, MMI_gen 4889 (partly: merge label and data), MMI_gen 4722 (partly: Close button, Window Title, Input field), MMI_gen 4637 (partly: Main-areas D and F), note under the MMI_gen 9412, MMI_gen 4912, MMI_gen 4678, MMI_gen 5003, MMI_gen 4913 (MMI_gen 4384, MMI_gen 4386 (partly: except 0.3s)), MMI_gen 4634, MMI_gen 4651, MMI_gen 4642, MMI_gen 4689, MMI_gen 4690, MMI_gen 4691 (partly: flashing), MMI_gen 4692, MMI_gen 4634, MMI_gen 4647 (partly: left aligned), MMI_gen 4694 (partly: MMI_gen 4246), MMI_gen 4682, MMI_ gen 4684 (partly: terminate), MMI_gen 4634)); MMI_gen 11737; MMI_gen 4392 (partly: [Close] NA11, [Delete] NA21, returning to the parent window); MMI_gen 4393 (partly: [Delete]); MMI_gen 4350; MMI_gen 4351; MMI_gen 4353; MMI_gen 9390 (partly: Password window);
    /// 
    /// Scenario:
    /// Open Maintenance password window. Then, verify the display information of Maintenance password window.The Down-type button on keypad is verified.The data entry functionality of Maintenance password window is verified.Enter a wrong password and verify the display information at input field.Confirm data by pressing input field and verify that Maintenance password window is closed.Open Maintenance password window. Then, enter and confirm specified password. Verify that Maintenance window is displayed.
    /// 
    /// Used files:
    /// N/A
    /// </summary>
    public class Password_window : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // Set the Maintenance password in tag name ‘PASS_CODE_MTN’ of the configuration file as ‘88888888’. (See the instruction in the Appendix 1)                                                                                                                    Test system is powered on.Cabin is activate.Settings window is opened.

            // Call the TestCaseBase PreExecution
            base.PreExecution();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in SB mode

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint


            /*
            Test Step 1
            Action: Press ‘Maintenance’ button
            Expected Result: Verify the following points,LayersThe layers of window on half-grid array is displayed as followsLayer 0: Main-Area D, F, G, Y and Z.Layer -1: A1, A2+A3*, A4, B*, C1, C2+C3+C4*, C5, C6, C7, C8, C9, E1, E2, E3, E4, E5-E9*Layer -2: B3, B4, B5, B6, B7Note: ‘*’ symbol is mean that specified areas are drawn as one area.Data Entry windowThe window title is displayed with text “Maintenance password”.Verify that the Maintenance password window is displayed in main area D, F and G as half-grid array.A data entry window is containing only one input field covers the Main area D, F and GThe following objects are displayed in Maintenance password window. Enabled Close button (NA11)Window TitleInput FieldInput fieldThe input field is located in main area D and F.For a single input field, the window title is clearly explaining the topic of the input field. The Maintenance password window is displayed as a single input field with only the data part.KeyboardThe keyboard associated to the Maintenance password window is displayed as numeric keyboard.The keyboard is presented below the area of input field.The keyboard contains enabled button for the number <1> to <9>, <Delete>(NA21) , <0> and disabled <Decimal_Separator>. NA21, Delete button.DMI displays Maintenance password window.General property of windowThe Maintenance password window is presented with objects and buttons which is the one of several levels and allocated to areas of DMI. All objects, text messages and buttons are presented within the same layer.The Default window is not displayed and covered the current window
            Test Step Comment: (1) MMI_gen 11719 (partly: MMI_gen 5189 (partly: touch screen), MMI_gen 5944 (partly: touch screen));(2) MMI_gen 11718;(3) MMI_gen 11719 (partly: half grid array)(4) MMI_gen 11719 (partly: MMI_gen 4640 (partly: only data area), MMI_gen 4720, MMI_gen 4889 (partly: merge label and data))(5) MMI_gen 11719 (partly: MMI_gen 4722 (partly: Close button, Window Title, Input field)); MMI_gen 4392 (partly: [Close] NA11);(6) MMI_gen 11719 (partly: MMI_gen 4637 (partly: Main-areas D and F))(7) MMI_gen 11719 (partly: note under the MMI_gen 9412)(8) MMI_gen 11719 (partly: single input field, only data part);(9) MMI_gen 11719 (partly: MMI_gen 4912); MMI_gen 11721;(10) MMI_gen 11719 (partly: MMI_gen 4678)(11) MMI_gen 11719 (partly: MMI_gen 5003); MMI_gen 4392 (partly: [Delete] NA21);(12) MMI_gen 11737;(13) MMI_gen 4350;(14) MMI_gen 4351;(15) MMI_gen 4353;
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press ‘Maintenance’ button");


            /*
            Test Step 2
            Action: Press and hold every buttons on the dedicate keyboard respectively.Note: This step is for testing ‘0’ - ‘9’ button
            Expected Result: Verify the following information,On next activation of a data key of the associated keyboard, the character or value corresponding to this data key shall be added into the Data Area.Sound ‘Click’ is played once.The state of button is changed to ‘Pressed’ and immediately back to ‘Enabled’ state.The Input Field displays the number associated to the data key according to the pressings in state ‘Pressed’.An input field is used to enter the Maintenance password.The data value is displayed as black colour and the background of the data area is displayed as medium-grey colour.The data value of the input field is aligned to the left of the data area.The flashing horizontal-line cursor is always in the next position of the echoed entered-data key in the ‘Selected IF/value of pressed key(s)’ data input field when selected the next character it will be inserted cursor position
            Test Step Comment: (1) MMI_gen 11719 (partly: MMI_gen 4679, MMI_gen 4642);(2) MMI_gen 11719 (partly: MMI_gen 4913 (partly: MMI_gen 4384 (partly: sound ‘Click’)));(3) MMI_gen 11719 (partly: MMI_gen 4913 (partly: MMI_gen 4384 (partly: Change to state ‘Pressed’ and immediately back to state ‘Enabled’)));   (4) MMI_gen 11719 (partly: MMI_gen 4913);(5) MMI_gen 11720; MMI_gen 11719 (partly: MMI_gen 4634);(6) MMI_gen 11719 (partly: MMI_gen 4651);(7) MMI_gen 11719 (partly: MMI_gen 4647 (partly: left aligned));(8) MMI_gen 11719 (partly: MMI_gen 4689, MMI_gen 4690, MMI_gen 4691 (partly: flashing), MMI_gen 4692);
            */


            /*
            Test Step 3
            Action: Released the pressed button
            Expected Result: Verify the following information, The character is stop adding and the state of button is changed to ‘Enabled’
            Test Step Comment: (1) MMI_gen 11719 (partly: MMI_gen 4913 (partly: MMI_gen 4384 (partly: ETCS-MMI’s function associated to the button)));
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Released the pressed button");


            /*
            Test Step 4
            Action: Press and hold ‘Del’ button.Note: Stopwatch is required
            Expected Result: Verify the following information,While press and hold button less than 1.5 secSound ‘Click’ is played once.The state of button is changed to ‘Pressed’ and immediately back to ‘Enabled’ state.The last character is removed from an input field after pressing the button.While press and hold button over 1.5 secThe state ‘pressed’ and ‘released’ are switched repeatly while button is pressed and the characters are removed from an input field repeatly refer to pressed state.The sound ‘Click’ is played repeatly while button is pressed
            Test Step Comment: (1) MMI_gen 11719 (partly: MMI_gen 4913 (partly: MMI_gen 4384 (partly: sound ‘Click’)));(2) MMI_gen 11719 (partly: MMI_gen 4913 (partly: MMI_gen 4384 (partly: Change to state ‘Pressed’ and immediately back to state ‘Enabled’)));(3) MMI_gen 11719 (partly: MMI_gen 4913 (partly: MMI_gen 4384 (partly: ETCS-MMI’s function associated to the button))); MMI_gen 4393 (partly: [Delete]);(4) MMI_gen 11719 (partly: MMI_gen 4913 (partly: MMI_gen 4386 (partly: visual of repeat function)));(5) MMI_gen 11719  (partly: MMI_gen 4913 (partly: MMI_gen 4386 (partly: audible of repeat function)));
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press and hold ‘Del’ button.Note: Stopwatch is required");
            // Call generic Check Results Method
            DmiExpectedResults
                .Verify_the_following_information_While_press_and_hold_button_less_than_1_5_secSound_Click_is_played_once_The_state_of_button_is_changed_to_Pressed_and_immediately_back_to_Enabled_state_The_last_character_is_removed_from_an_input_field_after_pressing_the_button_While_press_and_hold_button_over_1_5_secThe_state_pressed_and_released_are_switched_repeatly_while_button_is_pressed_and_the_characters_are_removed_from_an_input_field_repeatly_refer_to_pressed_state_The_sound_Click_is_played_repeatly_while_button_is_pressed(this);


            /*
            Test Step 5
            Action: Release ‘Del’ button
            Expected Result: Verify the following information, The character is stop removing
            Test Step Comment: (1) MMI_gen 11719 (partly: MMI_gen 4913 (partly: MMI_gen 4384 (partly: ETCS-MMI’s function associated to the button)));
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Release ‘Del’ button");
            // Call generic Check Results Method
            DmiExpectedResults.Verify_the_following_information_The_character_is_stop_removing(this);


            /*
            Test Step 6
            Action: Press ‘Del’ button on the numeric keyboard until no number is displayed on the Input Field
            Expected Result: No character is displayed on the Input Field
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this,
                @"Press ‘Del’ button on the numeric keyboard until no number is displayed on the Input Field");


            /*
            Test Step 7
            Action: The 5 characters are added on an input field as one group. (e.g. ‘12345')
            Expected Result: Verify the following information,The 5 characters are added on an input field as one group. (e.g. ‘*****').Single input field is show on asterisk (*) symbol for each entered number
            Test Step Comment: (1) MMI_gen 11719 (partly: MMI_gen 4694 (partly: NEGATIVE, 6th character));(2) MMI_gen 11693;
            */


            /*
            Test Step 8
            Action: Continue to enter the 6th character
            Expected Result: Verify the following information,The fifth character is shown after a gap of fourth character, separated as 2 groups (e.g. **** **)
            Test Step Comment: (1) MMI_gen 11719 (partly: MMI_gen 4694 (partly: MMI_gen 4246));
            */
            // Call generic Action Method
            DmiActions.Continue_to_enter_the_6th_character(this);


            /*
            Test Step 9
            Action: Delete the old value and enter the new value more than 8 characters which different from configured value in tag PASS_CODE_MTN
            Expected Result: Verify the following information,The data value is displayed only 8 characters (e.g. **** ****)
            Test Step Comment: (1) MMI_gen 11722     (partly: password is configurable at most eight digits);
            */


            /*
            Test Step 10
            Action: Press and hold an input field
            Expected Result: Verify the following information,(1)    The state of an input field is changed to ‘Pressed’, the border of button is removed
            Test Step Comment: (1) MMI_gen 9390 (partly: Password window);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press and hold an input field");
            // Call generic Check Results Method
            DmiExpectedResults
                .Verify_the_following_information_1_The_state_of_an_input_field_is_changed_to_Pressed_the_border_of_button_is_removed(this);


            /*
            Test Step 11
            Action: Slide out an input field
            Expected Result: Verify the following information,(1)    The state of an input field is changed to ‘Enabled, the border of button is shown without a sound
            Test Step Comment: (1) MMI_gen 9390 (partly: Password window);
            */
            // Call generic Action Method
            DmiActions.Slide_out_an_input_field(this);
            // Call generic Check Results Method
            DmiExpectedResults
                .Verify_the_following_information_1_The_state_of_an_input_field_is_changed_to_Enabled_the_border_of_button_is_shown_without_a_sound(this);


            /*
            Test Step 12
            Action: Slide back into an input field
            Expected Result: Verify the following information,(1)    The state of an input field is changed to ‘Pressed’, the border of button is removed
            Test Step Comment: (1) MMI_gen 9390 (partly: Password window);
            */
            // Call generic Action Method
            DmiActions.Slide_back_into_an_input_field(this);
            // Call generic Check Results Method
            DmiExpectedResults
                .Verify_the_following_information_1_The_state_of_an_input_field_is_changed_to_Pressed_the_border_of_button_is_removed(this);


            /*
            Test Step 13
            Action: Release the pressed area
            Expected Result: Verify the followings information,Data entry process is terminated, DMI displays the Settings window
            Test Step Comment: (1) MMI_gen 11732; MMI_gen 11719 (partly: MMI_gen 4682, MMI_ gen 4684 (partly: terminate), MMI_gen 4634)); MMI_gen 4392 (partly: [Enter], touch screen); MMI_gen 9390 (partly: Password window);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Release the pressed area");


            /*
            Test Step 14
            Action: Perform the folowing procedure,Press ‘Maintenance’ button.Enter the password refer to configured value in PASS_CODE_MTNConfirm an entered data
            Expected Result: DMI displays the Maintenance window
            Test Step Comment: MMI_gen 11731 (partly: touch screen); MMI_gen 11722 (partly: password is configurable at most eight digits);
            */
            // Call generic Check Results Method
            DmiExpectedResults.DMI_displays_the_Maintenance_window(this);


            /*
            Test Step 15
            Action: Perform the following step to re-configure the Maintenance passwordPower off systemRe-configure the Maintenance password ‘PASS_CODE_MTN’ to ‘4444’. Perform the following test step to verify configuration result,Power On system.Activate Cabin A.Press ‘Settings icon’ button.Press ‘Maintenance’ button.Enter password as 4444.Confirm entered data by pressing input field
            Expected Result: DMI displays the Maintenance window
            Test Step Comment: MMI_gen 11722     (partly: password is configurable at least four digits);
            */
            // Call generic Check Results Method
            DmiExpectedResults.DMI_displays_the_Maintenance_window(this);


            /*
            Test Step 16
            Action: Perform the following step to re-configure the Maintenance passwordPower off systemRe-configure the Maintenance password ‘PASS_CODE_MTN’ to ‘333’. Perform the following test step to verify configuration result,Power On system.Activate Cabin A.Press ‘Settings icon’ button.Press ‘Maintenance’ button.Enter password as 333.Confirm entered data by pressing input field
            Expected Result: DMI displays the Settings window
            Test Step Comment: MMI_gen 11722     (partly: NEGATIVE, password is configurable at least four digits);
            */
            // Call generic Check Results Method
            DmiExpectedResults.DMI_displays_the_Settings_window(this);


            /*
            Test Step 17
            Action: Perform the following step to re-configure the Maintenance passwordPower off systemRe-configure the Maintenance password ‘PASS_CODE_MTN’ to ‘999999999’. Perform the following test step to verify configuration result,Power On system.Activate Cabin A.Press ‘Settings icon’ button.Press ‘Maintenance’ button.Enter password as 999999999.Confirm entered data by pressing input field
            Expected Result: DMI displays the Settings window
            Test Step Comment: MMI_gen 11722     (partly: NEGATIVE, password is configurable at most eight digits);
            */
            // Call generic Check Results Method
            DmiExpectedResults.DMI_displays_the_Settings_window(this);


            /*
            Test Step 18
            Action: Press the ‘Maintenance’ button.Then, press the ‘Close’ button
            Expected Result: Verify the following informaiton,(1) DMI displays the Settings window
            Test Step Comment: (1) MMI_gen 4392 (partly: returning to the parent window);
            */


            /*
            Test Step 19
            Action: End of test
            Expected Result: 
            */


            return GlobalTestResult;
        }
    }
}
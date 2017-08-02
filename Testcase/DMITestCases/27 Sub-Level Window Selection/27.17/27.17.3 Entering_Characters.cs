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
    /// 27.17.3 Entering Characters
    /// TC-ID: 7.3.2
    /// 
    /// This test case verifies the property of entering characters of the Driver ID window. The property of entering characters shall comply with [ERA-ERTMS] standard.
    /// 
    /// Tested Requirements:
    /// MMI_gen 8033 (partly: MMI_gen 4689, MMI_gen 4690, MMI_gen 4691 (partly: flashing), MMI_gen 4692, MMI_gen 4693, MMI_gen 4694 (partly: MMI_gen 4246, MMI_gen 4247), MMI_gen 4913 (partly: MMI_gen 4384, MMI_gen 4386 (partly: except 0.3s))); MMI_gen 4393 (partly: [Delete]);
    /// 
    /// Scenario:
    /// The test system is powered on and the cabin is activated.The properties of entering characters in Driver ID window is verified.The Down-type button on keypad is verified.
    /// 
    /// Used files:
    /// N/A
    /// </summary>
    public class Entering_Characters : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // System is power on.

            // Call the TestCaseBase PreExecution
            base.PreExecution();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in SB mode.

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint


            /*
            Test Step 1
            Action: Activate cabin A
            Expected Result: DMI displays Driver ID window.Verify the following information,The flashing horizontal-line cursor is always flashed in the next position of the echoed entered-data key in the ‘Selected IF/value of pressed key(s)’ data input field when selected the next character it will be inserted cursor position
            Test Step Comment: (1) MMI_gen 8033 (partly: MMI_gen 4689, MMI_gen 4690, MMI_gen 4691 (partly: flashing));
            */
            // Call generic Action Method
            DmiActions.Activate_Cabin_1(this);


            /*
            Test Step 2
            Action: Press the data key which have only single character once.Note: This step is for testing ‘1’ and ‘0’ button
            Expected Result: Verify the following information,The pressed key is added in an input field immediately.The cursor is jumped to next position after entered the character immediately
            Test Step Comment: (1) MMI_gen 8033 (partly: MMI_gen 4693 (a));  (2) MMI_gen 8033 (partly: MMI_gen 4692);  
            */


            /*
            Test Step 3
            Action: Press and hold the data key which have only single character.Note: This step is for testing ‘1’ and ‘0’ button.Press the ‘Del’ button to delete an information when entered data is out of input field range is acceptable
            Expected Result: Verify the following information,Sound ‘Click’ is played once.The state of button is changed to ‘Pressed’ and immediately back to ‘Enabled’ state.Pressed key is added in an input field after pressing the button
            Test Step Comment: (1) MMI_gen 8033 (partly: MMI_gen 4913 (partly: MMI_gen 4384 (partly: sound ‘Click’)));(2) MMI_gen 8033 (partly: MMI_gen 4913 (partly: MMI_gen 4384 (partly: Change to state ‘Pressed’ and immediately back to state ‘Enabled’)));   (3) MMI_gen 8033 (partly: MMI_gen 4913 (partly: MMI_gen 4384 (partly: ETCS-MMI’s function associated to the button)));
            */


            /*
            Test Step 4
            Action: Released the pressed button
            Expected Result: Verify the following information, The character is stop adding
            Test Step Comment: (1) MMI_gen 8033 (partly: MMI_gen 4913 (partly: MMI_gen 4384 (partly: ETCS-MMI’s function associated to the button)));
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(@"Released the pressed button");


            /*
            Test Step 5
            Action: Press and hold ‘Del’ button.Note: Stopwatch is required
            Expected Result: Verify the following information,While press and hold button less than 1.5 secSound ‘Click’ is played once.The state of button is changed to ‘Pressed’ and immediately back to ‘Enabled’ state.The last character is removed from an input field after pressing the button.While press and hold button over 1.5 secThe state ‘pressed’ and ‘released’ are switched repeatly while button is pressed and the characters are removed from an input field repeatly refer to pressed state.The sound ‘Click’ is played repeatly while button is pressed
            Test Step Comment: (1) MMI_gen 8033 (partly: MMI_gen 4913 (partly: MMI_gen 4384 (partly: sound ‘Click)));(2) MMI_gen 8033 (partly: MMI_gen 4913 (partly: MMI_gen 4384 (partly: Change to state ‘Pressed’ and immediately back to state ‘Enabled’))); MMI_gen 4393 (partly: [Delete]);(3) MMI_gen 8033 (partly: MMI_gen 4913 (partly:MMI_gen 4384 (partly: ETCS-MMI’s function associated to the button)));(4) MMI_gen 8033 (partly: MMI_gen 4913 (partly:  MMI_gen 4386 (partly: visual of repeat function)));(5) MMI_gen 8033 (partly: MMI_gen 4913 (partly: MMI_gen 4386 (partly: audible of repeat function)));
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(@"Press and hold ‘Del’ button.Note: Stopwatch is required");
            // Call generic Check Results Method
            DmiExpectedResults
                .Verify_the_following_information_While_press_and_hold_button_less_than_1_5_secSound_Click_is_played_once_The_state_of_button_is_changed_to_Pressed_and_immediately_back_to_Enabled_state_The_last_character_is_removed_from_an_input_field_after_pressing_the_button_While_press_and_hold_button_over_1_5_secThe_state_pressed_and_released_are_switched_repeatly_while_button_is_pressed_and_the_characters_are_removed_from_an_input_field_repeatly_refer_to_pressed_state_The_sound_Click_is_played_repeatly_while_button_is_pressed(this);


            /*
            Test Step 6
            Action: Release ‘Del’ button
            Expected Result: Verify the following information, The character is stop removing
            Test Step Comment: (1) MMI_gen 8033 (partly: MMI_gen 4913 (partly: MMI_gen 4384 (partly: ETCS-MMI’s function associated to the button)));
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(@"Release ‘Del’ button");
            // Call generic Check Results Method
            DmiExpectedResults.Verify_the_following_information_The_character_is_stop_removing(this);


            /*
            Test Step 7
            Action: Press the data key which have various characters once.Note: This step is for testing ‘2’-‘9’ button.Stopwatch is required
            Expected Result: Verify the following information,The first character of the data key is added on an input field immediately. The cursor is jumped to next position after entered the character after 2 seconds
            Test Step Comment: (1) MMI_gen 8033 (parly: MMI_gen 4693 (a));(2) MMI_gen 8033 (parly: MMI_gen 4693 (b));
            */


            /*
            Test Step 8
            Action: Press the same data key repeatly within these 2 seconds.Then, verify the displayed information at an input field while pressing button
            Expected Result: Verify the following information,The next character of the same data key is selected and displayed on an input field refer to the label of pressed button and wrap-around.For example, the single character is displayed at input field with following step ‘2’ -> ‘a’ -> ‘b’ -> ‘c’ -> ‘2’
            Test Step Comment: (1) MMI_gen 8033 (partly: MMI_gen 4693 (partly: (c), pressing the same data key));
            */


            /*
            Test Step 9
            Action: Press and hold the same data key.Note: This step is for testing ‘’2’-‘9’ button.Stopwatch is required.Press the ‘Del’ button to delete an information when entered data is out of input field range is acceptable
            Expected Result: Verify the following information,While press and hold button less than 1.5 secSound ‘Click’ is played once.The state of button is changed to ‘Pressed’ and immediately back to ‘Enabled’ state.Pressed key is added in an input field after pressing the button.While press and hold button over 1.5 secThe next character of the same data key is selected and displayed on an input field refer to the label ot pressed button and wrap-around.For example, the single character is displayed at input field with following step ‘2’ -> ‘a’ -> ‘b’ -> ‘c’ -> ‘2’
            Test Step Comment: (1) MMI_gen 8033  (partly: MMI_gen 4913 (partly: MMI_gen 4384 (partly: sound ‘Click)));(2) MMI_gen 8033 (partly: MMI_gen 4913 (partly: MMI_gen 4384 (partly: Change to state ‘Pressed’ and immediately back to state ‘Enabled’)));   (3) MMI_gen 8033 (partly: MMI_gen 4913 (partly: MMI_gen 4384 (partly: ETCS-MMI’s function associated to the button)));(4) MMI_gen 8033 (partly: MMI_gen 4693 (partly: (c), hoding for some time));
            */


            /*
            Test Step 10
            Action: Released the pressed button
            Expected Result: Verify the following information, The character is stop changing
            Test Step Comment: (1) MMI_gen 8033 (partly: MMI_gen 4913 (partly:  MMI_gen 4384 (partly: ETCS-MMI’s function associated to the button)));
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(@"Released the pressed button");


            /*
            Test Step 11
            Action: Press the data key which have various characters once.After passed 2 seconds, press the same data key.Note: This step is for testing ‘’2’-‘9’ button.Stopwatch is required
            Expected Result: Verify the following information, An entered data are separated as 2 characters, for example ‘22’
            Test Step Comment: (1) MMI_gen 8033 (partly: MMI_gen 4693 (partly: (c), NEGATIVE, exceed 2 second delay time));
            */


            /*
            Test Step 12
            Action: Press data key and then another data key within 2 seconds, for example 2 then 3Note: This step is for testing ‘’2’-‘9’ button.Stopwatch is required
            Expected Result: Verify the following information, The selected characters are added on an input field.The horizontal line cursor is forced to jump to next position directly
            Test Step Comment: (1) MMI_gen 8033 (partly: MMI_gen 4693 (a));(2) MMI_gen 8033 (partly: MMI_gen 4693 (d));
            */


            /*
            Test Step 13
            Action: Enter the data value with 5 characters
            Expected Result: Verify the following information,The 5 characters are added on an input field as one group. (e.g. ‘12345')
            Test Step Comment: (1) MMI_gen 8033 (partly: MMI_gen 4694 (partly: NEGATIVE, 6th character));
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(@"Enter the data value with 5 characters");
            // Call generic Check Results Method
            DmiExpectedResults
                .Verify_the_following_information_The_5_characters_are_added_on_an_input_field_as_one_group_e_g_12345(this);


            /*
            Test Step 14
            Action: Continue to enter the 6th character
            Expected Result: Verify the following information,The fifth character is shown after a gap of fourth character, separated as 2 groups (e.g. 1234 56)
            Test Step Comment: (1) MMI_gen 8033 (partly: MMI_gen 4694 (partly: MMI_gen 4246));
            */
            // Call generic Action Method
            DmiActions.Continue_to_enter_the_6th_character(this);
            // Call generic Check Results Method
            DmiExpectedResults
                .Verify_the_following_information_The_fifth_character_is_shown_after_a_gap_of_fourth_character_separated_as_2_groups_e_g_1234_56(this);


            /*
            Test Step 15
            Action: Continue to enter the new value more than 8 characters
            Expected Result: Verify the following information,The data value is separated as 2 lines. In each line is displayed only 8 characters
            Test Step Comment: (1) MMI_gen 8033 (partly: MMI_gen 4694 (partly: MMI_gen 4247));
            */
            // Call generic Action Method
            DmiActions.Continue_to_enter_the_new_value_more_than_8_characters(this);
            // Call generic Check Results Method
            DmiExpectedResults
                .Verify_the_following_information_The_data_value_is_separated_as_2_lines_In_each_line_is_displayed_only_8_characters(this);


            /*
            Test Step 16
            Action: End of test
            Expected Result: 
            */


            return GlobalTestResult;
        }
    }
}
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
    /// 27.29.1 Flexible Train data window: General appearances
    /// TC-ID: 22.29.1
    /// 
    /// This test case verifies the display of the ‘Train Data’ window on DMI that shall comply with [ERA-ERTMS] standard and [MMI-ETCS-gen].
    /// 
    /// Tested Requirements:
    /// MMI_gen 9459; MMI_gen 9460 (partly: [enter], [yes], EVC-107); MMI_gen 187; MMI_gen 8087; MMI_gen 8088; MMI_gen 9402 (partly: flexible); MMI_gen 9405; MMI_gen 8115; MMI_gen 8142; MMI_gen 8143; MMI_gen 8243; MMI_gen 11386; MMI_gen 8264; MMI_gen 8086 (partly: MMI_gen 4888, MMI_gen 4799 (partly: Close button, Previous button, Next button, Window Title, Input fields), MMI_gen 4891 (partly: Yes button, Area for [Window Title] Entry complete?), MMI_gen 4910, MMI_gen 4211 (partly: colour), MMI_gen 4909, MMI_gen 4908 (partly: extended), MMI_gen 4637 (partly: Main-areas D and F), MMI_gen 4640, MMI_gen 4641, MMI_gen 9412, MMI_gen 4645, MMI_gen 4646 (partly: right aligned), MMI_gen 4647 (partly: left aligned), MMI_gen 4648, MMI_gen 4720, MMI_gen 4651, MMI_gen 4683, MMI_gen 5211, MMI_gen 4649, MMI_gen 4912, MMI_gen 4678, MMI_gen 9336, MMI_gen 5190, MMI_gen 4696, MMI_gen 4697, MMI_gen 4701, MMI_gen 4702 (partly: right aligned), MMI_gen 4704 (partly: left aligned), MMI_gen 4700, MMI_gen 4691 (partly: flash), MMI_gen 4689, MMI_gen 4690, MMI_gen 9391 (partly: [More], [Previuos], [Next], MMI_gen 4381, MMI_gen 4382), MMI_gen 4913 (partly: MMI_gen 4384, MMI_gen 4386), MMI_gen 4682 , MMI_gen 4634 , MMI_gen 4652, MMI_gen 4684, MMI_gen 4642, MMI_gen 5003, MMI_gen 4681, MMI_gen 4680, MMI_gen 4685, MMI_gen 4911 (partly: MMI_gen 4381, MMI_gen 4382), MMI_gen 4686, MMI_gen 4360 (partly: total number of window), MMI_gen 4679, MMI_gen 4692, MMI_gen 4698, MMI_gen 5006); MMI_gen 4392 (partly: [Previous: NA19], [Next: NA17], [Close] NA11, [More: NA23], [Delete] NA21,[Enter], touch screen, returning to the parent window); MMI_gen 4355; MMI_gen 4396 (partly: Previous, NA19); MMI_gen 4394 (partly: [next], [previous]); MMI_gen 4377 (partly: shown); MMI_gen 4375; MMI_gen 9512; MMI_gen 968; MMI_gen 4374;  MMI_gen 5387; MMI_gen 4241; MMI_gen 9409; MMI_gen 4350; MMI_gen 4351; MMI_gen 4353; MMI_gen 9390 (partly: Flexible Train data window); MMI_gen 4393 (partly: [Delete]);
    /// 
    /// Scenario:
    /// The Train data window appearance is verified.The data entry functionality of the Train data window is verified with the following type of button in keypad,The Train category input field with Dedicated keyboardThe Up-Type button on ‘More’ button.The Down-Type button on another buttons.The Length (m) input field with Numeric keyboardThe Down-Type button with the repeat function on ‘Delete’ button.The Brake percentage input field with Numeric keyboardThe Maximum speed (km/h) input field with Numeric keyboardThe Axle load category input field with Dedicated keyboardThe Airtight input field with Dedicated keyboard The Loading gauge input field with Dedicated keyboardThe state of the ‘Enter’ button associated to the input fieldThe Up-Type button on ‘Yes’ buttonThe Up-Type button on each label part of an input field is verified.The Up-Type button on each data part of an input field is verified.The functionality of ‘Close’ button is verified.The state of ‘Yes’ button when other rule requires to disabling is verified.
    /// 
    /// Used files:
    /// 22_29_1_a.xml
    /// </summary>
    public class Flexible_Train_data_window_General_appearances : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // Test system is powered ON.Cabin is activated.Perform SoM until level 1 until Level 1 is selected and confirmed.

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
            Action: Press ‘Train Data’ button
            Expected Result: DMI displays Train Data window.Verify the following information,Data Entry WindowThe window title is ‘Train data’.The text label on window title is included with sequence number of the current window (e.g. ‘(1/2)’).The text label of the window title is right aligned.The following objects are displayed in Train data window,  Enabled Close button (NA11) Disabled Previous button (NA19)  Enabled Next button (NA17)Window TitleInput fieldsThe following objects are additionally displayed in Train data window,Yes buttonThe text label ‘Train data entry complete?’Yes button is displayed in Disabled state as follows,Text label is black Background colour is dark-greyThe border colour is medium-grey the same as the input field’s colour.The sensitive area of Yes button is extended from text label ‘Train data Entry complete?’Input fieldsThe input fields are located on Main area D and F.Each input field is devided into a Label Area and a Data Area.The Label Area is presented the topic of the input field.The Label Area text is displayed corresponding to the input field i.e. Train length, Maximum Speed or Train type. The Label Area is placed to the left of The Data Area.The text in the Label Area is aligned to the right.The value of data in the Data Area is aligned to the left.The text colour of the Label Area is grey and the background colour of the Label Area is dark-grey.There are only 4 input fields displayed in the first page of window.The first input field is in state ‘Selected’ as follows,The background colour of the Data Area is medium-grey.The colour of data value is black.All other input fields are in state ‘Not selected’ as follows,The background colour of the Data Area is dark-grey.The colour of data value is grey.KeyboardThe keyboard associated to selected input field ‘Train category’ is Dedicated keyboard.The key#12 is dedicated to More button NA23 (see pictures below).  NA23, More buttonThe label of key#1 – key#11 are displayed as follows,PASS 1PASS 2PASS 3TILT 1TILT 2TILT 3TILT 4TILT 5TILT 6TILT 7FP1LayersThe level of layers of all area in window are displayed in Layer 0.Echo TextsAn echo text is composed of Label Part and Data Part.The Label Part of an echo texts is same as The Label area of an input fields.The echo texts are displayed in main area A, B, C and E with same order as their related input fields.The Label part of echo text is right aligned.The Data part of echo text is left aligned.The colour of texts in echo texts are grey.Entering CharactersThe cursor is flashed by changing from visible to not visible.The cursor is displayed as horizontal line below the value in the input field.Packet transmissionUse the log file to confirm that DMI received packet information [MMI_CURRENT_TRAIN_DATA (EVC-6)] with following variables,MMIM_N_DATA_ELEMENTS = 0MMI_M_DATA_ENABLE = 32512MMI_M_TRAINSET_ID = 0The following label part of an input fields are displayed in Train data window refer to received packet EVC-6,Page 1Train CategoryTrain LengthBrake PercentageMaximum speedPage 2Axle Load CategoryAirtightLoading GaugeThe ‘Train Type’ input field is not displayed in Train data window.The data part of each input field and echo text are filled according to received packet EVC-6 with following variables,MMI_NID_KEY_TRAIN_CAT = Train categoryMMI_L_TRAIN = LengthMMI_M_BRAKE_PERC = Brake percentageMMI_NID_KEY_AXLE_LOAD = Axle load categoryMMI_M_AIRTIGHT = AirtightMMI_NID_KEY_LOAD_GAUGE = Loading gaugeNote: Press ‘Next’ button to confirm the display of input field in page 2.Do not forget to press ‘Previous’ button after verification is completed.General property of windowThe Train data window is presented with objects, text messages and buttons which is the one of several levels and allocated to areas of DMI.All objects, text messages and buttons are presented within the same layer.The Default window is not displayed and covered the current window
            Test Step Comment: (1) MMI_gen 8087; MMI_gen 4355 (partly: Window title);(2) MMI_gen 8088; MMI_gen 8086 (partly: MMI_gen 4360 (partly: tonal number of window));(3) MMI_gen 8086 (partly: MMI_gen 4888);(4) MMI_gen 8086 (partly: MMI_gen 4799 (partly: Close button, Previous button, Next button, Window Title, Input fields)); MMI_gen 4392 (partly: [Previous : NA19], [Next: NA17], [Close] NA11); MMI_gen 4355 (partly: Buttons, Close button); MMI_gen 4396 (partly: Previous, NA19); MMI_gen 4394 (partly: disabled [previous]);(5) MMI_gen 8086 (partly: MMI_gen 4891 (partly: Yes button, Area for [Window Title] Entry complete?));(6) MMI_gen 8086 (partly: MMI_gen 4910 (partly: Disabled, MMI_gen 4211 (partly: colour)), MMI_gen 4909 (partly: Disabled)); MMI_gen 4377 (partly: shown);(7) MMI_gen 8086 (partly: MMI_gen 4908 (partly: extended));(8) MMI_gen 8086 (partly: MMI_gen 4637 (partly: Main-areas D and F)); MMI_gen 4355 (partly: input fields);(9) MMI_gen 8086 (partly: MMI_gen 4640);(10) MMI_gen 8086 (partly: MMI_gen 4641);(11) MMI_gen 8086 (partly: MMI_gen 9412);(12) MMI_gen 8086 (partly: MMI_gen 4645);(13) MMI_gen 8086 (partly: MMI_gen 4646 (partly: right aligned));(14) MMI_gen 8086 (partly: MMI_gen 4647 (partly: left aligned));(15) MMI_gen 8086 (partly: MMI_gen 4648);(16) MMI_gen 8086 (partly: MMI_gen 4720);(17) MMI_gen 8086 (partly: MMI_gen 4651 (partly: Train category), MMI_gen 4683 (partly: selected), MMI_gen 5211 (partly: selected));(18) MMI_gen 8086 (partly: MMI_gen 4649 (partly: selected ‘Train category’), MMI_gen 4651 (partly: Length, Brake percentage, Max speed), MMI_gen 4683 (partly: not selected), MMI_gen 5211 (partly: not selected));(19) MMI_gen 8115 (partly: Train category); MMI_gen 8086 (partly: MMI_gen 4912 ( partly: Train category), MMI_gen 4678 (partly: Train category)); (20) MMI_gen 8086 (partly: MMI_gen 9336 (partly: Train category, More button)); MMI_gen 4392 (partly: [More: NA23]);(21) MMI_gen 8142 (partly: First page of Train category keyboard associated); MMI_gen 8143 (partly: PASS, TILT, FP1);(22) MMI_gen 8086 (partly: MMI_gen 5190);(23) MMI_gen 8086 (partly: MMI_gen 4696);(24) MMI_gen 8086 (partly: MMI_gen 4697); MMI_gen 187 (partly: label part of echo text);(25) MMI_gen 8086 (partly: MMI_gen 4701);(26) MMI_gen 8086 (partly: MMI_gen 4702 (partly: right aligned));(27) MMI_gen 8086 (partly: MMI_gen 4704 (partly: left aligned));(28) MMI_gen 8086 (partly: MMI_gen 4700 (partly: otherwise, grey)); MMI_gen 4241;(29) MMI_gen 8086 (partly: MMI_gen 4691 (partly: flash, Train category));(30) MMI_gen 8086 (partly: MMI_gen 4689, MMI_gen 4690);(31) MMI_gen 187 (partly: EVC-6, MMI_N_DATA_ELEMENT = 0, MMI_M_DATA_ENABLE ); MMI_gen 9402 (partly: flexible);(32) MMI_gen 187 (partly: enable editing of the parameters pointed out, label part of input fields);(33) MMI_gen 187 (partly: Not editable  data);(34) MMI_gen 187 (partly: Data value part of echo text and input fields, arn_043#3306);(35) MMI_gen 4350;(36) MMI_gen 4351;(37) MMI_gen 4353;
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press ‘Train Data’ button");


            /*
            Test Step 2
            Action: Press ‘More’ button
            Expected Result: Verify the following information,The label of key#1 – key#7 are displayed as follows,FP 2FP 3FP 4FG 1FG 2 FG 3FG 4
            Test Step Comment: (1) MMI_gen 8086 (partly: MMI_gen 9336 (partly: next predefined choices)); MMI_gen 8142 (partly: Second page of Train category keyboard associated); MMI_gen 8143 (partly: FP, FG);
            */


            /*
            Test Step 3
            Action: Press and hold ‘More’ button
            Expected Result: Verify the following information,The state of button is changed to ‘Pressed’, the border of button is removed.The sound ‘Click’ is played once
            Test Step Comment: (1) MMI_gen 8086 (partly: MMI_gen 9391 (partly: [More], Train category, MMI_gen 4381 (partly: change to state ‘Pressed’ as long as remain actuated)); MMI_gen 4375;(2) MMI_gen 8086 (partly: MMI_gen 9391 (partly: [More], Train category, MMI_gen 4381 (partly: sound ‘Click’)); MMI_gen 9512; MMI_gen 968;
            */
            // Call generic Check Results Method
            DmiExpectedResults
                .Verify_the_following_information_The_state_of_button_is_changed_to_Pressed_the_border_of_button_is_removed_The_sound_Click_is_played_once(this);


            /*
            Test Step 4
            Action: Slide out the ‘More’ button
            Expected Result: Verify the following information,The border of the button is shown (state ‘Enabled’) without a sound
            Test Step Comment: (1) MMI_gen 8086 (partly: MMI_gen 9391 (partly: [More], Train category, MMI_gen 4382 (partly: state ‘Enabled’ when slide out with force applied, no sound))); MMI_gen 4374;
            */
            // Call generic Check Results Method
            DmiExpectedResults
                .Verify_the_following_information_The_border_of_the_button_is_shown_state_Enabled_without_a_sound(this);


            /*
            Test Step 5
            Action: Slide back into the ‘More’ button
            Expected Result: Verify the following information,The button is back to state ‘Pressed’ without a sound
            Test Step Comment: (1) MMI_gen 8086 (partly: MMI_gen 9391 (partly: [More], Train category, MMI_gen 4382 (partly: state ‘Pressed’ when slide back, no sound))); MMI_gen 4375;
            */
            // Call generic Check Results Method
            DmiExpectedResults.Verify_the_following_information_The_button_is_back_to_state_Pressed_without_a_sound(this);


            /*
            Test Step 6
            Action: Release ‘More’ button
            Expected Result: Verify the following information,The state of released button is changed to enabled.The label of keypad is changed to previous page (same as expected result No.21 in step 1)
            Test Step Comment: (1) MMI_gen 8086 (partly: MMI_gen 9391 (partly: [More], MMI_gen 4381 (partly: exite state ‘pressed’)));(2) MMI_gen 8086 (partly: MMI_gen 4679 (partly: Train category), MMI_gen 9336 (partly: circular), MMI_gen 9391 (partly: [More], MMI_gen 4384 (partly: ETCS-MMI’s function associated to the button));
            */


            /*
            Test Step 7
            Action: Press and hold ‘PASS 2’ button
            Expected Result: Verify the following information,The state of ‘PASS 2‘ button is changed to ‘Pressed’ and immediately back to ‘Enabled’ state.The sound ‘Click’ is played once.The Input Field displays the value associated to the data key according to the pressings in state ‘Pressed’.The cursor is displayed as horizontal line below the value of the dedicated-keyboard data key in the input field
            Test Step Comment: (1) MMI_gen 8086 (partly: MMI_gen 4913 (partly: Train category); MMI_gen 4384 (partly: Change to state ‘Pressed’ and immediately back to state ‘Enabled’);   (2) MMI_gen 8086 (partly: MMI_gen 4913 (partly: Train category); MMI_gen 4384 (partly: sound ‘Click’); MMI_gen 9512; MMI_gen 968;(3) MMI_gen 8086 (partly: MMI_gen 4679 (partly: Train category, MMI_gen 4913 (partly: Train category); MMI_gen 4384 (partly: ETCS-MMI’s function associated to the button));(4) MMI_gen 8086 (partly: MMI_gen 4689, MMI_gen 4690);
            */


            /*
            Test Step 8
            Action: Release ‘PASS 2’ button
            Expected Result: Verify the following information,The state of released button is changed to enabled
            Test Step Comment: (1) MMI_gen 8086 (partly: MMI_gen 4913 (partly: Train category); MMI_gen 4384 (partly: ETCS-MMI’s function associated to the button));
            */
            // Call generic Check Results Method
            DmiExpectedResults.Verify_the_following_information_The_state_of_released_button_is_changed_to_enabled(this);


            /*
            Test Step 9
            Action: Perform action step 7-8 for the remaining buttons on keypad
            Expected Result: See the expected results of Step 7 – Step 8
            */
            // Call generic Check Results Method
            DmiExpectedResults.See_the_expected_results_of_Step_7_Step_8(this);


            /*
            Test Step 10
            Action: Enter value ‘PASS 1’ for train category.Then, confirm an entered data by pressing an input field
            Expected Result: Verify the following information,Input fieldsThe associated ‘Enter’ button is data field itself.An input field is used to allow the driver to enter data.The state of ‘Train category’ input field is changed to ‘accepted’ as follows,The background colour of the Data Area is dark-grey.The colour of data value is white.The next input field ‘Length’ is in state ‘selected’ as follows,The background colour of the Data Area is medium-grey.The colour of data value is black.Echo TextsThe echo text of ‘Train category’ is changed to white colour.The value of echo text is changed refer to entered data.Entering CharactersThe cursor is displayed as a horizontal line below the position of the next character to be entered.The cursor is flashed by changing from visible to not visible.KeyboardThe keyboard associated to selected input field ‘Length’ is Numeric keyboard.The keyboard contains enabled button for the number <1> to <9>, <Delete>(NA21) , <0> and disabled <Decimal_Separator>. NA21, Delete button.Packet transmissionUse the log file to confirm that DMI sent out packet [MMI_NEW_TRAIN_DATA (EVC-107)] with following variablesMMI_NID_KEY_TRAIN_CAT = 3 MMI_N_DATA_ELEMENTS = 1MMI_M_BUTTONS = 254The data part of the echo text of train category is displayed according to [MMI_CURRENT_TRAIN_DATA  (EVC-6)] with the following variables,MMI_NID_DATA = 7 (Train category)MMI_N_TEXT = 6MMI_X_TEXT = “PASS 1”
            Test Step Comment: (1) MMI_gen 8086 (partly: MMI_gen 4682 (partly: Train category));(2) MMI_gen 8086 (partly: MMI_gen 4634 (partly: Train category));(3) MMI_gen 8086 (partly: MMI_gen 4652 (partly: Train category), MMI_gen 4684 (partly: accepted, Train category));(4) MMI_gen 8086 (partly: MMI_gen 4684 (partly: Length, selected automatically), MMI_gen 4651 (partly: Length));(5) MMI_gen 8086 (partly: MMI_gen 4700 (partly: Train category));(6) MMI_gen 8086 (partly: MMI_gen 4681 (partly: Train category), MMI_gen 4890, MMI_gen 4698);(7) MMI_gen 8086 (partly: MMI_gen 4689, MMI_gen 4690);(8) MMI_gen 8086 (partly: MMI_gen 4691 (partly: flash, Length));(9) MMI_gen 8115 (partly: Length); MMI_gen 8086 (partly: MMI_gen 4912 (partly: Length), MMI_gen 4678 (partly: Length));(10) MMI_gen 8086 (partly: MMI_gen 5003 (partly: Length)); MMI_gen 4392 (partly: [Delete] NA21);(11) MMI_gen 9460 (partly: [Enter] EVC-107); MMI_gen 11386;(12) MMI_gen 9405 (partly: Train category);
            */


            /*
            Test Step 11
            Action: Perform action step 7-8 for the ‘0’ to ‘9’ buttons.Note: Press the ‘Del’ button to delete an information when entered data is out of input field range is acceptable
            Expected Result: See the expected results of Step 7 – Step 8 and the following additional information,The pressed key is added in an input field immediately. The cursor is jumped to next position after entered the character immediately
            Test Step Comment: (1) MMI_gen 8086 (partly: MMI_gen 4642 (partly: Length));  (2) MMI_gen 8086 (partly: MMI_gen 4692 (partly: Length));  
            */


            /*
            Test Step 12
            Action: Press and hold ‘Del’ button.Note: Stopwatch is required
            Expected Result: Verify the following information,While press and hold button less than 1.5 secSound ‘Click’ is played once.The state of button is changed to ‘Pressed’ and immediately back to ‘Enabled’ state.The last character is removed from an input field after pressing the button.While press and hold button over 1.5 secThe state ‘pressed’ and ‘released’ are switched repeatly while button is pressed and the characters are removed from an input field repeatly refer to pressed state.The sound ‘Click’ is played repeatly while button is pressed
            Test Step Comment: (1) MMI_gen 8086 (partly: MMI_gen 4913 (partly: Length)); MMI_gen 4384 (partly: sound ‘Click’);(2) MMI_gen 8086 (partly: MMI_gen 4913 (partly: Length)); MMI_gen 4384 (partly: Change to state ‘Pressed’ and immediately back to state ‘Enabled’);   (3) MMI_gen 8086 (partly: MMI_gen 4913 (partly: Length)); MMI_gen 4384 (partly: ETCS-MMI’s function associated to the button); MMI_gen 4393 (partly: [Delete]);(4) MMI_gen 8086 (partly: MMI_gen 4913 (partly: Length)); MMI_gen 4386 (partly: visual of repeat function);(5) MMI_gen 8086 (partly: MMI_gen 4913 (partly: Length)); MMI_gen 4386 (partly: audible of repeat function);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press and hold ‘Del’ button.Note: Stopwatch is required");
            // Call generic Check Results Method
            DmiExpectedResults
                .Verify_the_following_information_While_press_and_hold_button_less_than_1_5_secSound_Click_is_played_once_The_state_of_button_is_changed_to_Pressed_and_immediately_back_to_Enabled_state_The_last_character_is_removed_from_an_input_field_after_pressing_the_button_While_press_and_hold_button_over_1_5_secThe_state_pressed_and_released_are_switched_repeatly_while_button_is_pressed_and_the_characters_are_removed_from_an_input_field_repeatly_refer_to_pressed_state_The_sound_Click_is_played_repeatly_while_button_is_pressed(this);


            /*
            Test Step 13
            Action: Release ‘Del’ button
            Expected Result: Verify the following information, The character is stop removing
            Test Step Comment: (1) MMI_gen 8086 (partly: MMI_gen 4913 (partly: Length)); MMI_gen 4384 (partly: ETCS-MMI’s function associated to the button);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Release ‘Del’ button");
            // Call generic Check Results Method
            DmiExpectedResults.Verify_the_following_information_The_character_is_stop_removing(this);


            /*
            Test Step 14
            Action: Enter the value ‘100’ for train length.Then, confirm an entered data by pressing an input field
            Expected Result: Verify the following information,Input fieldsThe associated ‘Enter’ button is data field itself.An input field is used to allow the driver to enter data.The state of ‘Length’ input field is changed to ‘accepted’ as follows,The background colour of the Data Area is dark-grey.The colour of data value is white.The next input field ‘Brake percentage’ is in state ‘selected’ as follows,The background colour of the Data Area is Medium-grey.The colour of data value is black.Echo TextsThe echo text of ‘Length’ is changed to white colour.The value of echo text is changed refer to entered data.Entering CharactersThe cursor is displayed as a horizontal line below the position of the next character to be entered.The cursor is flashed by changing from visible to not visible.KeyboardThe keyboard associated to selected input field ‘Length’ is Numeric keyboard.The keyboard contains enabled button for the number <1> to <9>, <Delete>(NA21) , <0> and disabled <Decimal_Separator>. NA21, Delete buttonPacket transmissionUse the log file to confirm that DMI sent out packet [MMI_NEW_TRAIN_DATA (EVC-107)] with the following variables MMI_L_TRAIN = 100MMI_N_DATA_ELEMENTS = 1MMI_M_BUTTONS = 254The data part of the echo text of train category is displayed according to [MMI_CURRENT_TRAIN_DATA  (EVC-6)] with the following variables,MMI_NID_DATA = 8 (Length)MMI_N_TEXT = 3MMI_X_TEXT = “100”
            Test Step Comment: (1) MMI_gen 8086 (partly: MMI_gen 4682 (partly: Length));(2) MMI_gen 8086 (partly: MMI_gen 4634 (partly: Length));(3) MMI_gen 8086 (partly: MMI_gen 4652 (partly: Length), MMI_gen 4684 (partly: accepted, Length));(4) MMI_gen 8086 (partly: MMI_gen 4684 (partly: Length, selected automatically), MMI_gen 4651 (partly: Brake percentage));(5) MMI_gen 8086 (partly: MMI_gen 4700 (partly: Length));(6) MMI_gen 8086 (partly: MMI_gen 4681 (partly: Length), MMI_gen 4890, MMI_gen 4698);(7) MMI_gen 8086 (partly: MMI_gen 4689, MMI_gen 4690);(8) MMI_gen 8086 (partly: MMI_gen 4691 (partly: flash, Brake percentage));(9) MMI_gen 8115 (partly: Brake percentage); MMI_gen 8086 (partly: MMI_gen 4912 (partly: Brake percentage), MMI_gen 4678 (partly: Brake percentage));(10) MMI_gen 8086 (partly: MMI_gen 5003 (partly: Brake percentage)); MMI_gen 4392 (partly: [Delete] NA21);(11) MMI_gen 9460 (partly: [Enter] EVC-107); (12) MMI_gen 9405 (partly: Length);
            */


            /*
            Test Step 15
            Action: Perform action step 11-13 for keypad of the ‘Brake percentage’ input field
            Expected Result: See the expected results of Step 11 – Step 13
            */
            // Call generic Check Results Method
            DmiExpectedResults.See_the_expected_results_of_Step_11_Step_13(this);


            /*
            Test Step 16
            Action: Enter the value ‘70’ for Brake percentage.Confirm an entered data by pressing an input field
            Expected Result: Verify the following information,Input fieldsThe associated ‘Enter’ button is data field itself.An input field is used to allow the driver to enter data.The state of ‘Brake percentage’ input field is changed to ‘accepted’ as follows,The background colour of the Data Area is dark-grey.The colour of data value is white.The next input field ‘Maximum speed’ is in state ‘selected’ as follows,The background colour of the Data Area is Medium-grey.The colour of data value is black.Echo TextsThe echo text of ‘Brake percentage’ is changed to white colour.The value of echo text is changed refer to entered data.Entering CharactersThe cursor is displayed as a horizontal line below the position of the next character to be entered.The cursor is flashed by changing from visible to not visible.KeyboardThe keyboard associated to selected input field ‘Maximum speed’ is Numeric keyboard.The keyboard contains enabled button for the number <1> to <9>, <Delete>(NA21) , <0> and disabled <Decimal_Separator>. NA21, Delete buttonPacket transmissionUse the log file to confirm that DMI sent out packet [MMI_NEW_TRAIN_DATA (EVC-107)] with the following variables MMI_M_BRAKE_PERC = 70MMI_N_DATA_ELEMENTS = 1MMI_M_BUTTONS = 254The data part of the echo text of train category is displayed according to [MMI_CURRENT_TRAIN_DATA  (EVC-6)] with the following variables,MMI_NID_DATA = 9 (Brake percentage)MMI_N_TEXT = 2MMI_X_TEXT = “70”
            Test Step Comment: (1) MMI_gen 8086 (partly: MMI_gen 4682 (partly: Brake percentage));(2) MMI_gen 8086 (partly: MMI_gen 4634 (partly: Brake percentage));(3) MMI_gen 8086 (partly: MMI_gen 4652 (partly: Brake percentage), MMI_gen 4684 (partly: accepted, Brake percentage));(4) MMI_gen 8086 (partly: MMI_gen 4684 (partly: Brake percentage, selected automatically), MMI_gen 4651 (partly: Maximum speed));(5) MMI_gen 8086 (partly: MMI_gen 4700 (partly: Brake percentage));(6) MMI_gen 8086 (partly: MMI_gen 4681 (partly: Brake percentage), MMI_gen 4890, MMI_gen 4698);(7) MMI_gen 8086 (partly: MMI_gen 4689, MMI_gen 4690);(8) MMI_gen 8086 (partly: MMI_gen 4691 (partly: flash, Maximum speed));(9) MMI_gen 8115 (partly: Maximum speed); MMI_gen 8086 (partly: MMI_gen 4912 (partly: Maximum speed), MMI_gen 4678 (partly: Maximum speed));(10) MMI_gen 8086 (partly: MMI_gen 5003 (partly: Maximum speed)); MMI_gen 4392 (partly: [Delete] NA21);(11) MMI_gen 9460 (partly: [Enter] EVC-107); (12) MMI_gen 9405 (partly: Brake percentage);
            */


            /*
            Test Step 17
            Action: Perform action step 11-13 for keypad of the ‘Maximum speed’ input field
            Expected Result: See the expected results of Step 11 – Step 13
            */
            // Call generic Check Results Method
            DmiExpectedResults.See_the_expected_results_of_Step_11_Step_13(this);


            /*
            Test Step 18
            Action: Enter the value ‘170’ for Maximum speed.Then, confirm an entered data by pressing an input field
            Expected Result: Verify the following information,Input fieldsThe associated ‘Enter’ button is data field itself.An input field is used to allow the driver to enter data.The state of ‘Maximum speed’ input field is changed to ‘accepted’ as follows,The background colour of the Data Area is dark-grey.The colour of data value is white.The next input field ‘Axle load category’ is in state ‘selected’ as follows,The background colour of the Data Area is Medium-grey.The colour of data value is black.Echo TextsThe echo text of ‘Maximum speed’ is changed to white colour.The value of echo text is changed refer to entered data.Entering CharactersThe cursor is displayed as a horizontal line below the position of the next character to be entered.The cursor is flashed by changing from visible to not visible.KeyboardThe keyboard associated to selected input field ‘Axle load category’ is Dedicated keyboard.The key#12 is dedicated to More button.The label of key#1 – key#11 are displayed as follows,AHS17B1B2C2C3C4D2D3D4D4XLNote: Reference of label of Axle load category is specified in chapter A3.11 [SUBSET-026].Packet transmissionUse the log file to confirm that DMI sent out packet [MMI_NEW_TRAIN_DATA (EVC-107)] with the following variables MMI_V_MAXTRAIN = 170 MMI_N_DATA_ELEMENTS = 1MMI_M_BUTTONS = 254The data part of the echo text of train category is displayed according to [MMI_CURRENT_TRAIN_DATA  (EVC-6)] with the following variables,MMI_NID_DATA = 10 (Maximum speed)MMI_N_TEXT = 3MMI_X_TEXT = “170”
            Test Step Comment: (1) MMI_gen 8086 (partly: MMI_gen 4682 (partly: Maximum speed));(2) MMI_gen 8086 (partly: MMI_gen 4634 (partly: Maximum speed));(3) MMI_gen 8086 (partly: MMI_gen 4652 (partly: Maximum speed), MMI_gen 4684 (partly: accepted, Maximum speed));(4) MMI_gen 8086 (partly: MMI_gen 4684 (partly: Axle load category, selected automatically, next page), MMI_gen 4651 (partly: Axle load category));(5) MMI_gen 8086 (partly: MMI_gen 4700 (partly: Maximum speed));(6) MMI_gen 8086 (partly: MMI_gen 4681 (partly: Maximum speed), MMI_gen 4890, MMI_gen 4698);(7) MMI_gen 8086 (partly: MMI_gen 4689, MMI_gen 4690);(8) MMI_gen 8086 (partly: MMI_gen 4691 (partly: flash, Axle load category));(9) MMI_gen 8115 (partly: Axle load category); MMI_gen 8086 (partly: MMI_gen 4912 (partly: Axle load category), MMI_gen 4678 (partly: Axle load category));(10) MMI_gen 8086 (partly: MMI_gen 9336 (partly: Axle load category, More button));(11) MMI_gen 8142 (partly: First page of Axle load category keyboard associated); MMI_gen 8264;(12) MMI_gen 9460 (partly: [Enter] EVC-107); (13) MMI_gen 9405 (partly: Maximum speed);
            */


            /*
            Test Step 19
            Action: Follow Step 2 – Step 6 for the ‘More’ button
            Expected Result: See the expected results of Step 2 – Step 6 with the expected result for the label for keypad as follows,(1)   After ‘More’ button is pressed refer to step 2, The label of key#1 – key#2 are displayed as follows,E4E5Note: Reference of label of Axle load category is specified in chapter A3.11 [SUBSET-026].After ‘More’ button is released refer to step 6, the label of keypard is circularly changed to previous page (same as expected result No.11 in step 18)
            Test Step Comment: (1) MMI_gen 8264;(2) See step 6.
            */


            /*
            Test Step 20
            Action: Perform action step 7-8 for keypad of the ‘Axle Load Category’ input field
            Expected Result: See the expected results of Step 7 – Step 8
            */
            // Call generic Check Results Method
            DmiExpectedResults.See_the_expected_results_of_Step_7_Step_8(this);


            /*
            Test Step 21
            Action: Enter the value ‘A’ for Axle Load Category.Then, confirm an entered data by pressing an input field
            Expected Result: Verify the following information,Input fields(1)   The associated ‘Enter’ button is data field itself.(2)   An input field is used to allow the driver to enter data.(3)   The state of ‘Axle load category’ input field is changed to ‘accepted’ as follows,The background colour of the Data Area is dark-grey.The colour of data value is white.(4)   The next input field ‘Airtight’ is in state ‘selected’ as follows,The background colour of the Data Area is medium-grey.The colour of data value is black.Echo Texts(5)   The echo text of ‘Axle load category’ is changed to white colour.(6)   The value of echo text is changed refer to entered data.Entering Characters(7)   The cursor is displayed as a horizontal line below the position of the next character to be entered.(8)   The cursor is flashed by changing from visible to not visible.Keyboard(9)    The keyboard associated to selected input field ‘Airtight’ is Dedicated keyboard.(10)  The label of each button in keypad are displayed as follows,Key #7 is No buttonKey #8 is Yes buttonPacket transmission(11)  Use the log file to confirm that DMI sent out packet [MMI_NEW_TRAIN_DATA (EVC-107)] with the following variables, MMI_NID_KEY_AXLE_LOAD = 21 MMI_N_DATA_ELEMENTS = 1MMI_M_BUTTONS = 254(12)  The data part of the echo text of train category is displayed according to [MMI_CURRENT_TRAIN_DATA  (EVC-6)] with the following variables,MMI_NID_DATA = 11 (Axle Load Category)MMI_N_TEXT = 1MMI_X_TEXT = “A”Navigation button(13)  The state of ‘Previous’ and ‘Next’ button are displayed as follows, ‘Next’ button is disabled, display symbol NA18.2‘Previous’ button is enable, display symbol NA18
            Test Step Comment: (1) MMI_gen 8086 (partly: MMI_gen 4682 (partly: Axle load category));(2) MMI_gen 8086 (partly: MMI_gen 4634 (partly: Axle load category));(3) MMI_gen 8086 (partly: MMI_gen 4652 (partly: Axle load category), MMI_gen 4684 (partly: accepted, Axle load category));(4) MMI_gen 8086 (partly: MMI_gen 4684 (partly: Airtight, selected automatically), MMI_gen 4651 (partly: Airtight));(5) MMI_gen 8086 (partly: MMI_gen 4700 (partly: Axle load category));(6) MMI_gen 8086 (partly: MMI_gen 4681 (partly: Axle load category), MMI_gen 4890, MMI_gen 4698);(7) MMI_gen 8086 (partly: MMI_gen, 4689, MMI_gen 4690);(8) MMI_gen 8086 (partly: MMI_gen 4691 (partly: flash, Airtight));(9) MMI_gen 8115 (partly: Airtight); MMI_gen 8086 (partly: MMI_gen 4912 (partly: Airtight), MMI_gen 4678 (partly: Airtight));(10) MMI_gen 8086 (partly: MMI_gen 5006);(11) MMI_gen 9460 (partly: [Enter] EVC-107);(12) MMI_gen 9405 (partly: Axle Load Category);(13) MMI_gen 4394 (partly: enabled [previous], disabled [next]);
            */


            /*
            Test Step 22
            Action: Follow Step 7 – Step 8 for every buttons on the keypad of Airtight
            Expected Result: See the expected results of Step 7 – Step 8
            */
            // Call generic Check Results Method
            DmiExpectedResults.See_the_expected_results_of_Step_7_Step_8(this);


            /*
            Test Step 23
            Action: Press ‘No’ button on keypad.Then, confirm an entered data by pressing an input field
            Expected Result: Verify the following information,Input fieldsThe associated ‘Enter’ button is data field itself.An input field is used to allow the driver to enter data.The state of ‘Airtight’ input field is changed to ‘accepted’ as follows,The background colour of the Data Area is dark-grey.The colour of data value is white.The next input field ‘Loading gauge’ is in state ‘selected’ as follows,The background colour of the Data Area is dark-grey.The colour of data value is grey.Echo TextsThe echo text of ‘Airtight’ is changed to white colour.The value of echo text is changed refer to entered data.Entering CharactersThe cursor is displayed as a horizontal line below the position of the next character to be entered.The cursor is flashed by changing from visible to not visible.KeyboardThe keyboard associated to selected input field ‘Loading gauge’ is Dedicated keyboard.The label of key#1 – key#5 is display as follows,G1GAGBGCOut of GCPacket transmissionUse the log file to confirm that DMI sent out packet [MMI_NEW_TRAIN_DATA (EVC-107)] with the following variables,MMI_M_AIRTIGHT = 0 MMI_N_DATA_ELEMENTS = 1MMI_M_BUTTONS = 254The data part of the echo text of train category is displayed according to [MMI_CURRENT_TRAIN_DATA  (EVC-6)] with the following variables,MMI_NID_DATA = 12 (Airtight)MMI_N_TEXT = 2MMI_X_TEXT = “No”
            Test Step Comment: (1) MMI_gen 8086 (partly: MMI_gen 4682 (partly: Airtight));(2) MMI_gen 8086 (partly: MMI_gen 4634 (partly: Airtight));(3) MMI_gen 8086 (partly: MMI_gen 4652 (partly: Airtight), MMI_gen 4684 (partly: accepted, Airtight));(4) MMI_gen 8086 (partly: MMI_gen 4684 (partly: Loading gauge, selected automatically), MMI_gen 4651 (partly: Loading gauge));(5) MMI_gen 8086 (partly: MMI_gen 4700 (partly: Airtight));(6) MMI_gen 8086 (partly: MMI_gen 4681 (partly: Airtight), MMI_gen 4698, MMI_gen 4890);(7) MMI_gen 8086 (partly: MMI-gen 4689, MMI_gen 4690);(8) MMI_gen 8086 (partly: MMI_gen 4691 (partly: flash, Loading gauge));(9) MMI_gen 8115 (partly: Loading gauge); MMI_gen 8086 (partly: MMI_gen 4912 (partly: Loading gauge), MMI_gen 4678 (partly: Loading gauge));(10) MMI_gen 8142 (partly: Loading gauge keyboard associated); MMI_gen 8243;(11) MMI_gen 9460 (partly: [Enter] EVC-107); (12) MMI_gen 9405 (partly:Airtight);
            */


            /*
            Test Step 24
            Action: Follow Step 7 – Step 8 for every buttons on the keypad of Loading Gauge
            Expected Result: See the expected results of Step 7 – Step 8
            */
            // Call generic Check Results Method
            DmiExpectedResults.See_the_expected_results_of_Step_7_Step_8(this);


            /*
            Test Step 25
            Action: Enter value ‘Out of GC’ for Loading gauge.Then, confirm an entered data by pressing an input field
            Expected Result: Verify the following information,Input fields(1) The associated ‘Enter’ button is data field itself.(2) An input field is used to allow the driver to enter data.(3) The state of ‘Loading gauge’ input field is changed to ‘accepted’ as follows,The background colour of the Data Area is dark-grey.The colour of data value is white.(4) There is no input field selected.Echo Texts(5) The echo text of ‘Loading gauge’ is changed to white colour.(6) The value of echo text is changed refer to entered data.Data Entry window(7) The state of ‘Yes’ button below text label ‘Train data Entry is complete?’ is enabled as follows,The background colour of the Data Area is medium-grey.The colour of data value is black.The colour of border is medium-grey.Packet transmission(8) Use the log file to confirm that DMI sent out packet [MMI_NEW_TRAIN_DATA (EVC-107)] with following variablesMMI_NID_KEY_LOAD_GAUGE = 34MMI_N_DATA_ELEMENTS = 1MMI_M_BUTTONS = 254(9) The data part of the echo text of train category is displayed according to [MMI_CURRENT_TRAIN_DATA  (EVC-6)] with the following variables,MMI_NID_DATA = 13 (Loading gauge)MMI_N_TEXT = 9MMI_X_TEXT = “Out of GC”(10)    Use the log file to confirm that DMI received packet EVC-6 with variable MMI_M_BUTTONS = 36 (BTN_YES_DATA_ENTRY_COMPLETE)
            Test Step Comment: (1) MMI_gen 8086 (partly: MMI_gen 4682 (partly: Loading gauge));(2) MMI_gen 8086 (partly: MMI_gen 4634 (partly: Loading gauge));(3) MMI_gen 8086 (partly: MMI_gen 4652 (partly: Loading gauge), MMI_gen 4684 (partly: accepted, Loading gauge));(4) MMI_gen 8086 (partly: MMI_gen 4684 (partly: No next input field, data entry process terminated));(5) MMI_gen 8086 (partly: MMI_gen 4700 (partly: Loading gauge));(6) MMI_gen 8086 (partly: MMI_gen 4681 (partly: Loading gauge), MMI_gen 4698, MMI_gen 4890);(7) MMI_gen 8086 (partly: MMI_gen 4909 (partly:Enabled), MMI_gen 4910 (partly: Enabled, MMI_gen 4211 (partly: colour))); MMI_gen 4374;(8) MMI_gen 9460 (partly: [Enter] EVC-107); (9) MMI_gen 9405 (partly:Loading gauge);(10) MMI_gen 9409 (partly: EVC-6, MMI_M_BUTTONS);
            */


            /*
            Test Step 26
            Action: Apply the action step 3-6 for ‘Previous’ and ‘Next’ button
            Expected Result: See the expected results of Step 3 – Step 6
            Test Step Comment: MMI_gen 9391 (partly: [Previuos], [Next]); MMI_gen 4355 (partly: [Previuos], [Next]);
            */


            /*
            Test Step 27
            Action: Perform the following procedure,Press ‘Previous’ button.Select ‘Train category’ input field.Press ‘PASS 1’ button.Select ‘Brake percentage’ input field
            Expected Result: Verify the following information,(1)   The state of ‘Yes’ button below text label ‘Train data Entry is complete?’ is disabled. (2)   The state of input field ‘Train category’ is changed to ‘Not selected’ as follows,The value of ‘Train category’ input field is removed, display as blank.The background colour of the input field is dark-grey
            Test Step Comment: (1) MMI_gen 8086 (partly: MMI_gen 4909 (partly: state selected and with recently entered key), MMI_gen 4680 (partly: value has been modified)); (2) MMI_gen 8086 (partly: MMI_gen 4680 (partly: Train category, Not selected, Data area is blank), MMI_gen 4649 (partly: data entry, background colour));
            */


            /*
            Test Step 28
            Action: Perform the following procedure,Press ‘Next’ button.Select ‘Loading gauge’ input field.Confirm the value of ‘Loading gauge’ by pressing an input field
            Expected Result: Verify the following information,The state of input field ‘Train category’ is changed to ‘Selected.Entering CharactersThe cursor is displayed as a horizontal line below the position of the next character to be entered.The cursor is flashed by changing from visible to not visible
            Test Step Comment: (1) MMI_gen 8086 (partly: MMI_gen 4685);(2) MMI_gen 8086 (partly: MMI_gen 4689, MMI_gen 4690);(3) MMI_gen 8086 (partly: MMI_gen 4691 (partly: flash, Train category));
            */


            /*
            Test Step 29
            Action: Perform the following procedure,Press ‘PASS 1’ button.Confirm the value of ‘Train category’ by pressing an input field
            Expected Result: The state of ‘Yes’ button is changed to enabled
            */
            // Call generic Check Results Method
            DmiExpectedResults.The_state_of_Yes_button_is_changed_to_enabled(this);


            /*
            Test Step 30
            Action: Apply the action step 3-6 for ‘Yes’ button
            Expected Result: See the expected results of Step 3 – Step 6 and the following points,DMI displays Train data validation window.Use the log file to confirm that DMI sent out packet [MMI_NEW_TRAIN_DATA (EVC-107)] with following variablesMMI_N_DATA_ELEMENTS = 0MMI_M_TRAINSET_ID = 0MMI_M_BUTTONS = 36
            Test Step Comment: (1) MMI_gen 8086 (partly: MMI_gen 4911 (partly: MMI_gen 4381 (partly: change to state ‘Pressed’ as long as remain actuated))); MMI_gen 5387 (partly: closure);(2) MMI_gen 8086 (partly: MMI_gen 4911 (partly: MMI_gen 4381 (partly: exit state ‘Pressed’, execute function associated to the button))); MMI_gen 9460 (partly: [Yes], EVC-107); MMI_gen 5387 (partly: transmission);
            */


            /*
            Test Step 31
            Action: Press ‘Close’ button
            Expected Result: DMI displays Train data window
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press ‘Close’ button");
            // Call generic Check Results Method
            DmiExpectedResults.DMI_displays_Train_data_window(this);


            /*
            Test Step 32
            Action: Press and hold the Label area of ‘Length’ input field
            Expected Result: Verify the following information,The state of ‘Length’ input field is changed to ‘Pressed’, the border of button is removed.The state of ‘Length’ input field remains ‘not selected’. The state of ‘Train category’ input field remains ‘selected’.The sound ‘Click’ is played once
            Test Step Comment: (1) MMI_gen 8086 (partly: MMI_gen 4686 (partly: Label area, Length), MMI_gen 4381 (partly: change to state ‘Pressed’ as long as remain actuated))); MMI_gen 4392 (partly: [Enter], touch screen); MMI_gen 4375;(2) MMI_gen 8086 (partly: MMI_gen 4686 (partly: Label area, Length), MMI_gen 4381 (partly: the sound for Up-Type button));
            */


            /*
            Test Step 33
            Action: Slide out the Label area of ‘Length’ input field
            Expected Result: Verify the following information,The border of ‘Length’ input field is shown (state ‘Enabled’) without a sound.The state of ‘Length’ input field remains ‘not selected’. The state of ‘Train category’ input field remains ‘selected’
            Test Step Comment: (1) MMI_gen 8086 (partly: MMI_gen 4686 (partly: Label area, Length), MMI_gen 4382 (partly: state ‘Enabled’ when slide out with force applied, no sound); MMI_gen 4374;
            */


            /*
            Test Step 34
            Action: Slide back into the Label area of ‘Length’ input field
            Expected Result: Verify the following information,The state of ‘Length’ input field is changed to ‘Pressed’, the border of button is removed.The state of ‘Length’ input field remains ‘not selected’. The state of ‘Train category’ input field remains ‘selected’
            Test Step Comment: (1) MMI_gen 8086 (partly: MMI_gen 4686 (partly: Label area, Length), MMI_gen 4382 (partly: state ‘Pressed’ when slide back, no sound); MMI_gen 4375;
            */


            /*
            Test Step 35
            Action: Release the pressed area
            Expected Result: Verify the following information,The state of ‘Length’ input field is changed to selected
            Test Step Comment: (1) MMI_gen 8086 (partly: MMI_gen 4686 (partly: Label area, Length), MMI_gen 4381 (partly: exit state ‘Pressed’, execute function associated to the button)); MMI_gen 4374;
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Release the pressed area");


            /*
            Test Step 36
            Action: Perform action step 32-35 for the Label area of each input field
            Expected Result: Verify the following information,The state of an input field is changed to ‘selected’ when release the pressed area at the Label area of input field
            Test Step Comment: (1) MMI_gen 8086 (partly: MMI_gen 4686 (partly: Label area, Up-Type button));
            */
            // Call generic Check Results Method
            DmiExpectedResults
                .Verify_the_following_information_The_state_of_an_input_field_is_changed_to_selected_when_release_the_pressed_area_at_the_Label_area_of_input_field(this);


            /*
            Test Step 37
            Action: Perform action step 32-35 for the Data area of each input field
            Expected Result: Verify the following information,The state of an input field is changed to ‘selected’ when release the pressed area at the Data area of input field
            Test Step Comment: (1) MMI_gen 8086 (partly: MMI_gen 4686 (partly: Data area, Up-Type button)); MMI_gen 9390 (partly: Flexible Train data window);
            */
            // Call generic Check Results Method
            DmiExpectedResults
                .Verify_the_following_information_The_state_of_an_input_field_is_changed_to_selected_when_release_the_pressed_area_at_the_Data_area_of_input_field(this);


            /*
            Test Step 38
            Action: Press ‘Close’ button
            Expected Result: Verify the following information, Use the log file to confirm that DMI sent out packet [MMI_DRIVER_REQUEST (EVC-101)] with variable MMI_M_REQUEST = 4 (Exit Train Data Entry).Use the log file to confirm that DMI sent out packet [MMI_ENABLE_REQUEST (EVC-30)] with variable MMI_NID_WINDOW = 254.The window is closed and the Main window is displayed
            Test Step Comment: (1) MMI_gen 9459 (partly: EVC-101);(2) MMI_gen 9459 (partly: MMI_gen 12071 (partly: EVC-30), MMI_gen 4355 (partly: [Close]));(3) MMI_gen 9459 (partly: MMI_gen 12071 (partly: closure), MMI_gen 4355 (partly: [Close])); MMI_gen 4392 (partly: returning to the parent window);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press ‘Close’ button");


            /*
            Test Step 39
            Action: Press ‘Train data’ button.Then, use the test script file 22_29_1_a.xml to send EVC-6 with,MMI_M_BUTTONS = 36
            Expected Result: DMI displays Train data window.Verify the following information,(1)     The state of ‘Yes’ button below text label ‘Train data Entry is complete?’ still disabled
            Test Step Comment: (1) MMI_gen 9409 (partly: other rule, MMI_gen 4909 (partly: otherwise));
            */
            // Call generic Check Results Method
            DmiExpectedResults
                .DMI_displays_Train_data_window_Verify_the_following_information_1_The_state_of_Yes_button_below_text_label_Train_data_Entry_is_complete_still_disabled(this);


            /*
            Test Step 40
            Action: End of test
            Expected Result: 
            */


            return GlobalTestResult;
        }
    }
}
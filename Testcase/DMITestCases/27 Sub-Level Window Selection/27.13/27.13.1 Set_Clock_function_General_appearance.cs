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
    /// 27.13.1 Set Clock function: General appearance
    /// TC-ID: 22.13.1
    /// 
    /// This test case verifies the display of the ‘Set Clock’ window on DMI that shall comply with [ERA-ERTMS] standard and [MMI-ETCS-gen].
    /// 
    /// Tested Requirements:
    /// MMI_gen 1563; MMI_gen 11939; MMI_gen 178; MMI_gen 2450; MMI_gen 11941; MMI_gen 11946; MMI_gen 11951; MMI_gen 11952; MMI_gen 11953; MMI_gen 12125; MMI_gen 11956; MMI_gen 11957; MMI_gen 11958; MMI_gen 11954; MMI_gen 11943; MMI_gen 11944; MMI_gen 11945; MMI_gen 12127; MMI_gen 12128;  MMI_gen 11940; MMI_gen 11942 (partly: MMI_gen 4692, MMI_gen 4679, MMI_gen 4888, MMI_gen 4799 (partly: Close button, Previous button, Next button, Window Title, Input fields), MMI_gen 4891 (partly: Yes button, Area for [Window Title] Entry complete?), MMI_gen 4910, MMI_gen 4211 (partly: colour), MMI_gen 4909, MMI_gen 4908 (partly: extended), MMI_gen 4637 (partly: Main-areas D and F), MMI_gen 4640, MMI_gen 4641, MMI_gen 9412, MMI_gen 4645, MMI_gen 4646 (partly: right aligned), MMI_gen 4647 (partly: left aligned), MMI_gen 4648, MMI_gen 4720, MMI_gen 4651, MMI_gen 4683, MMI_gen 5211, MMI_gen 4649, MMI_gen 4912, MMI_gen 4678, MMI_gen 9336, MMI_gen 5190, MMI_gen 4696, MMI_gen 4701, MMI_gen 4702 (partly: right aligned), MMI_gen 4704 (partly: left aligned), MMI_gen 4700, MMI_gen 4691 (partly: flash), MMI_gen 4689, MMI_gen 4690, MMI_gen 9391 (partly: [More], [Previuos], [Next], MMI_gen 4381, MMI_gen 4382), MMI_gen 4913 (partly: MMI_gen 4384, MMI_gen 4386), MMI_gen 4682 , MMI_gen 4634 , MMI_gen 4652,  MMI_gen 4684, MMI_gen 4642, MMI_gen 5003, MMI_gen 4681, MMI_gen 4680, MMI_gen 4685, MMI_gen 4911 (partly: MMI_gen 4381, MMI_gen 4382), MMI_gen 4686); MMI_gen 4392 (partly: [Previous : NA19], [Next: NA17], [Close] NA11, [Delete] NA21,Enter), touch screen, returning to the parent window); MMI_gen 4355; MMI_gen 4396 (partly: Previous, NA19); MMI_gen 4394 (partly: [next], [previous]); MMI_gen 4377 (partly: shown); MMI_gen 4375; MMI_gen 9512; MMI_gen 968; MMI_gen 4374;  MMI_gen 5387; MMI_gen 2451; MMI_gen 2452; MMI_gen 4241; MMI_gen 4350; MMI_gen 4351; MMI_gen 4353; MMI_gen 4358; MMI_gen 4360 (partly: total number of window); MMI_gen 9390 (partly: Set Clock window); MMI_gen 4393 (partly: [Delete]);
    /// 
    /// Scenario:
    /// The concerned buttons in the ‘Set Clock’ window are verified by the following actions:Press the button and holdSlide the button out with force appliedSlide the button back with force appliedRelease the buttonThe state of ‘Set Clock’ button is verified by using test script files.The Set Clock window appearance is verified. The data entry functionality of the Set Clock window is verified with the following type of button in keypadThe Year Input field with Numeric keyboardThe Month Input field with Numeric keyboardThe Day Input field with Numeric keyboardThe Hour Input field with Numeric keyboardThe Minute Input field with Numeric keyboardThe Second Input field with Numeric keyboardThe Offset Input field with Dedicated keyboardThe Up-Type of ‘Previous’, ‘Next’ and ‘Yes’ button are verified.The Up-Type button on each label part of an input field is verified.The Up-Type button on each data part of an input field is verified.Select new language. Then, verify that each label in Set Clock window is updated refer to selected language.
    /// 
    /// Used files:
    /// 22_13_1_a.xml, 22_13_1_b.xml, 22_13_1_c.xml, 22_13_1_d.xml
    /// </summary>
    public class Set_Clock_function_General_appearance : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // Test system is power on.Cabin is activateSettings window is opened. 

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
            Action: Use the test script file 22_13_1_a.xml to send EVC-30 with,MMI_NID_WINDOW = 4MMI_Q_REQUEST_ENABLE_64 (#25) = 0MMI_Q_REQUEST_ENABLE_64 (#26) = 0
            Expected Result: The Set clock button is disabled
            Test Step Comment: MMI_gen 1563         (partly: disabled);             
            */
            // Call generic Check Results Method
            DmiExpectedResults.The_Set_clock_button_is_disabled(this);


            /*
            Test Step 2
            Action: Perform the following perocedure,Deactivate cabin.Use the test script file 22_13_1_d.xml to send EVC-30 with,MMI_NID_WINDOW = 4MMI_Q_REQUEST_ENABLE_64 (#25) = 1MMI_Q_REQUEST_ENABLE_64 (#26) = 1Activate Cabin
            Expected Result: The Set clock button is still disabled
            Test Step Comment: MMI_gen 1563 (partly: NEGATIVE, unfulfill condition);
            */


            /*
            Test Step 3
            Action: Use the test script file 22_13_1_d.xml to send EVC-30 with,MMI_NID_WINDOW = 4MMI_Q_REQUEST_ENABLE_64 (#25) = 1MMI_Q_REQUEST_ENABLE_64 (#26) = 1
            Expected Result: The Set clock button is enabled
            Test Step Comment: MMI_gen 1563         (partly: enabled);             
            */
            // Call generic Check Results Method
            DmiExpectedResults.The_Set_clock_button_is_enabled(this);


            /*
            Test Step 4
            Action: Press ‘Set Clock’ button
            Expected Result: Verify the following information,DMI displays Set Clock window.Data entry windowThe window title is ‘Set clock’.The text label on window title is included with sequence number of the current window (e.g. ‘(1/2)’).The text label of the window title is right aligned.The following objects are displayed in Set Clock window,  Enabled Close button (NA11) Disabled Previous button (NA19)  Enabled Next button (NA17)Window TitleInput fieldsThe following objects are additionally displayed in Train data window,Yes buttonThe text label ‘Clock set?’Yes button is displayed in Disabled state as follows,Text label is black Background colour is dark-greyThe border colour is medium-grey the same as the input field’s colour.The sensitive area of Yes button is extended from text label ‘Clock set?’Input fieldsThe input fields are located on Main area D and F.Each input field is devided into a Label Area and a Data Area.The Label Area is give the topic of the input field.The Label Area text is displayed corresponding to the input field i.e. Year, Month and Day.The Label Area is placed to the left of The Data Area.The text in the Label Area is aligned to the right.The value of data in the Data Area is aligned to the left.The text colour of the Label Area is grey and the background colour of the Label Area is dark-grey.There are only 3 input fields display in the first page of window.The first input field is in state ‘Selected’ as follows,The background colour of the Data Area is medium-grey.The colour of data value is black.All other input fields are in state ‘Not selected’ as follows,The background colour of the Data Area is dark-grey.The colour of data value is grey.The label of 1st input field is ‘Year’.The label of 2nd input field is ‘Month’.The label of 3rd input field is ‘Day’.KeyboardThe keyboard associated to selected input field ‘Year’ is Numeric keyboard.The keyboard contains enabled button for the number <1> to <9>, <Delete>(NA21) , <0> and disabled <Decimal_Separator>. NA21, Delete button.LayersThe level of layers of all areas in window are in Layer 0.Echo TextsAn echo text is composed of Label Part and Data Part.The echo texts are displayed in main area A, B, C and E with same order as their related input fields.The Label part of echo text is right aligned.The Data part of echo text is left aligned.The colour of texts in echo texts are grey.The format of UTC time and date is ‘UTC YYYY-MM-DD hh:mm:ss’The format of Local time and date is ‘Local YYYY-MM-DD hh:mm:ss’The date and time are updated every second.Note: Stopwatch is required.Entering CharactersThe cursor is flashed by changing from visible to not visible.The cursor is displayed as horizontal line below the value in the input field.General property of windowThe Set Clock window is presented with objects, text messages and buttons which is the one of several levels and allocated to areas of DMI. All objects, text messages and buttons are presented within the same layer.The Default window is not displayed and covered the current window
            Test Step Comment: (1) MMI_gen 11939;(2) MMI_gen 11941; MMI_gen 4355 (partly: Window title);   (3) MMI_gen 4360 (partly: total number of window);(4) MMI_gen 11942 (partly: MMI_gen 4888);(5) MMI_gen 11942 (partly: MMI_gen 4799 (partly: Close button, Previous button, Next button, Window Title, Input fields)); MMI_gen 4392 (partly: [Previous : NA19], [Next: NA17], [Close] NA11); MMI_gen 4355 (partly: Buttons, Close button); MMI_gen 4396 (partly: Previous, NA19); MMI_gen 4394 (partly: disabled [previous]); MMI_gen 4358;(6) MMI_gen 11942 (partly: MMI_gen 4891 (partly: Yes button, Area for [Window Title] Entry complete?), question ‘Clock set?);(7) MMI_gen 11942 (partly: MMI_gen 4910 (partly: Disabled, MMI_gen 4211 (partly: colour)), MMI_gen 4909 (partly: Disabled)); MMI_gen 4377 (partly: shown);(8) MMI_gen 11942 (partly: MMI_gen 4908 (partly: extended));(9) MMI_gen 11942 (partly: MMI_gen 4640);(10 MMI_gen 11942 (partly: MMI_gen 4640);(11) MMI_gen 11942 (partly: MMI_gen 4641);(12) MMI_gen 11942 (partly: MMI_gen 9412);(13) MMI_gen 11942 (partly: MMI_gen 4645);(14) MMI_gen 11942 (partly: MMI_gen 4646 (partly: right aligned));(15) MMI_gen 11942 (partly: MMI_gen 4647 (partly: left aligned));(16) MMI_gen 11942 (partly: MMI_gen 4648);(17) MMI_gen 11942 (partly: MMI_gen 4720); MMI_gen 12125 (partly: page 1);(18) MMI_gen 11942 (partly: MMI_gen 4651 (partly: Train category), MMI_gen 4683 ( partly: selected), MMI_gen 5211 (partly: selected));(19) MMI_gen 11942 (partly: MMI_gen 4649 (partly: selected ‘Year’), MMI_gen 4651 (partly: Month, Day), MMI_gen 4683 (partly: not selected), MMI_gen 5211 (partly: not selected));(20) MMI_gen 11951 (partly: label);(21) MMI_gen 11952 (partly: label);    (22) MMI_gen 11953 (partly: label);(23) MMI_gen 11944 (partly: Year); MMI_gen 11942 (partly: MMI_gen 4912 (partly: Year), MMI_gen 4678 (partly: Year));(24) MMI_gen 11942 (partly: MMI_gen 5003 (partly: Year)); MMI_gen 4392 (partly: [Delete] NA21);(25) MMI_gen 11942 (partly: MMI_gen 5190);(26) MMI_gen 11942 (partly: MMI_gen 4696);(27) MMI_gen 11942 (partly: MMI_gen 4701);(28) MMI_gen 11942 (partly: MMI_gen 4702 (partly: right aligned));(29) MMI_gen 11942 (partly: MMI_gen 4704 (partly: left aligned));(30) MMI_gen 11942 (partly: MMI_gen 4700 (partly: otherwise, grey)); MMI_gen 4241;(31) MMI_gen 12127;(32) MMI_gen 12128;(33) MMI_gen 11946 (partly: updated);(34) MMI_gen 11942 (partly: MMI_gen 4691 (partly: flash, Year));(35) MMI_gen 11942 (partly: MMI_gen 4689, MMI_gen 4690);(36) MMI_gen 4350;(37) MMI_gen 4351;(38) MMI_gen 4353;
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press ‘Set Clock’ button");


            /*
            Test Step 5
            Action: Press and hold ‘0’ button
            Expected Result: Verify the following information,The state of ‘0‘ button is changed to ‘Pressed’ and immediately back to ‘Enabled’ state.The sound ‘Click’ is played once.The Input Field displays the value associated to the data key according to the pressings in state ‘Pressed’.The cursor is displayed as horizontal line below the value of the numeric-keyboard data key in the input field.The input field is used to enter the Year.The local time is editable refer to received packet EVC-30 with the one of following condition is true,MMI_Q_REQUEST_ENABLE_64 #25 = 1MMI_Q_REQUEST_ENABLE_64 #26 = 1 The echo text of date and time are stop updating refer to driver modification
            Test Step Comment: (1) MMI_gen 11942 (partly: MMI_gen 4913 (partly: Year); MMI_gen 4384 (partly: Change to state ‘Pressed’ and immediately back to state ‘Enabled’);   (2) MMI_gen 11942 (partly: MMI_gen 4913 (partly: Year); MMI_gen 4384 (partly: sound ‘Click’); MMI_gen 9512; MMI_gen 968;(3) MMI_gen 11942 partly: MMI_gen 4679 (partly: Year), MMI_gen 4913 (partly: RBC ID), MMI_gen 4384 (partly: ETCS-MMI’s function associated to the button));(4) MMI_gen 11942 (partly: MMI_gen 4689, MMI_gen 4690);(5) MMI_gen 11951 (partly: entry);(6) MMI_gen 178; (7) MMI_gen 11946 (partly: driver modified);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press and hold ‘0’ button");


            /*
            Test Step 6
            Action: Release pressed button
            Expected Result: Verify the following information,The state of released button is changed to enabled
            Test Step Comment: (1) MMI_gen 11942 (partly: MMI_gen 4913 (partly: Year); MMI_gen 4384 (partly: ETCS-MMI’s function associated to the button);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Release pressed button");
            // Call generic Check Results Method
            DmiExpectedResults.Verify_the_following_information_The_state_of_released_button_is_changed_to_enabled(this);


            /*
            Test Step 7
            Action: Perform action step 3-4 for the ‘1’ to ‘9’ buttons.Note: Press the ‘Del’ button to delete an information when entered data is out of input field range is acceptable
            Expected Result: See the expected results of Step 3 – Step 4 and the following additional information,(1)   The pressed key is added in an input field immediately. (2)   The cursor is jumped to next position after entered the character immediately
            Test Step Comment: (1) MMI_gen 11942 (partly: MMI_gen 4642 (partly: Year));  (2) MMI_gen 11942 (partly: MMI_gen 4692 (partly: Year));  
            */
            // Call generic Action Method
            DmiActions
                .Perform_action_step_3_4_for_the_1_to_9_buttons_Note_Press_the_Del_button_to_delete_an_information_when_entered_data_is_out_of_input_field_range_is_acceptable(this);


            /*
            Test Step 8
            Action: Press and hold ‘Del’ button.Note: Stopwatch is required
            Expected Result: Verify the following information,While press and hold button less than 1.5 sec(1) Sound ‘Click’ is played once.(2)The state of button is changed to ‘Pressed’ and immediately back to ‘Enabled’ state.(3)The last character is removed from an input field after pressing the button.While press and hold button over 1.5 sec(4)The state ‘pressed’ and ‘released’ are switched repeatly while button is pressed and the characters are removed from an input field repeatly refer to pressed state.(5)The sound ‘Click’ is played repeatly while button is pressed
            Test Step Comment: (1) MMI_gen 11942 (partly: MMI_gen 4913 (partly: Year), MMI_gen 4384 (partly: sound ‘Click’));(2) MMI_gen 11942 (partly: MMI_gen 4913 (partly: Year), MMI_gen 4384 (partly: Change to state ‘Pressed’ and immediately back to state ‘Enabled’));    (3) MMI_gen 11942 (partly: MMI_gen 4913 (partly: Year), MMI_gen 4384 (partly: ETCS-MMI’s function associated to the button)); MMI_gen 4393 (partly: [Delete]);(4) MMI_gen 11942  (partly: MMI_gen 4913 (partly: Year), MMI_gen 4386 (partly: visual of repeat function));(5) MMI_gen 11942 (partly: MMI_gen 4913 (partly: Year), MMI_gen 4386 (partly: audible of repeat function));
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press and hold ‘Del’ button.Note: Stopwatch is required");


            /*
            Test Step 9
            Action: Release ‘Del’ button
            Expected Result: Verify the following information, (1)The character is stop removing
            Test Step Comment: (1) MMI_gen 11942 (partly: MMI_gen 4913 (partly: Year)), MMI_gen 4384 (partly: ETCS-MMI’s function associated to the button));
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Release ‘Del’ button");


            /*
            Test Step 10
            Action: Delete the old value and enter the new value ‘2016 for Year.Then, confirm an entered data by pressing an input field
            Expected Result: Verify the following information,Input fields(1)The associated ‘Enter’ button is data field itself.(2)An input field is used to allow the driver to enter data.(3)The state of ‘Year’ input field is changed to ‘accepted’ as follows,The background colour of the Data Area is dark-grey.The colour of data value is white.(4)The next input field ‘Month’ is in state ‘selected’ as follows,The background colour of the Data Area is medium-grey.The colour of data value is black.Echo Texts(5)The echo text of ‘Year‘ is changed to white colour.(6)The value of echo text is changed refer to entered data.Entering Characters (7)The cursor is displayed as a horizontal line below the position of the next character to be entered.(8)The cursor is flashed by changing from visible to not visible.Keyboard(9)The keyboard associated to selected input field ‘Month’ is Numeric keyboard.(10)The keyboard contains enabled button for the number <1> to <9>, <Delete>(NA21) , <0> and disabled <Decimal_Separator>. NA21, Delete button
            Test Step Comment: (1) MMI_gen 11942 (partly: MMI_gen 4682 (partly: Year));(2) MMI_gen 11942 (partly: MMI_gen 4634 (partly: Year));(3) MMI_gen 11942 (partly: MMI_gen 4652 (partly: Year), MMI_gen 4684 (partly: accepted, Year));(4) MMI_gen 11942 (partly: MMI_gen 4684 (partly: Month, selected automatically), MMI_gen 4651 (partly: Month));(5) MMI_gen 11942 (partly: MMI_gen 4700 (partly: Year));(6) MMI_gen 11942 (partly: MMI_gen 4681 (partly: Year), MMI_gen 4890, MMI_gen 4698);(7) MMI_gen 11942 (partly: MMI_gen 4689, MMI_gen 4690);(8) MMI_gen 11942 (partly: MMI_gen 4691 (partly: flash, Month));(9) ) MMI_gen 11944  (partly: Month); MMI_gen 11942 (partly: MMI_gen 4912 (partly: Month), MMI_gen 4678 (partly: Month));(10) MMI_gen 11942 (partly: MMI_gen 5003 (partly: Month)); MMI_gen 4392 (partly: [Delete] NA21);
            */


            /*
            Test Step 11
            Action: Perform action step 2-7 for keypad of the ‘Month’ input field
            Expected Result: See the expected results of Step 2 – Step 7 and the following additional information,(1)The pressed key is added in an input field immediately. (2)The cursor is jumped to next position after entered the character immediately.(3)The input field is used to enter the Month
            Test Step Comment: (1) MMI_gen 11942 (partly: MMI_gen 4642 (partly: Month));  (2) MMI_gen 11942 (partly: MMI_gen 4692 (partly: Month));  (3) MMI_gen 11952 (partly: entry);
            */


            /*
            Test Step 12
            Action: Delete the old value and enter the new value ‘12’ for Month.Then, confirm an entered data by pressing an input field
            Expected Result: Verify the following information,Input fields(1)The associated ‘Enter’ button is data field itself.(2)An input field is used to allow the driver to enter data.(3)The state of ‘Month’ input field is changed to ‘accepted’ as follows,The background colour of the Data Area is dark-grey.The colour of data value is white.(4)The next input field ‘Day’ is in state ‘selected’ as follows,The background colour of the Data Area is medium-grey.The colour of data value is black.Echo Texts(5)The echo text of ‘Month ‘is changed to white colour.(6)The value of echo text is changed refer to entered data.Entering Characters (7)The cursor is displayed as a horizontal line below the position of the next character to be entered.(8)The cursor is flashed by changing from visible to not visible.Keyboard(9)The keyboard associated to selected input field ‘Day’ is Numeric keyboard.(10)The keyboard contains enabled button for the number <1> to <9>, <Delete>(NA21) , <0> and disabled <Decimal_Separator>. NA21, Delete button
            Test Step Comment: (1) MMI_gen 11942 (partly: MMI_gen 4682 (partly: Month));(2) MMI_gen 11942 (partly: MMI_gen 4634 (partly: Month));(3) MMI_gen 11942 (partly: MMI_gen 4652 (partly: Month), MMI_gen 4684 (partly: accepted, Month));(4) MMI_gen 11942 (partly: MMI_gen 4684 (partly: Day, selected automatically), MMI_gen 4651 (partly: Day));(5) MMI_gen 11942 (partly: MMI_gen 4700 (partly: Month));(6) MMI_gen 11942 (partly: MMI_gen 4681 (partly: Month), MMI_gen 4890, MMI_gen 4698);(7) MMI_gen 11942 (partly: MMI_gen 4689, MMI_gen 4690);(8) MMI_gen 11942 (partly: MMI_gen 4691 (partly: flash, Day));(9) ) MMI_gen 11944  (partly: Day); MMI_gen 11942 (partly: MMI_gen 4912 (partly: Day), MMI_gen 4678 (partly: Day));(10) MMI_gen 11942 (partly: MMI_gen 5003 (partly: Day)); MMI_gen 4392 (partly: [Delete] NA21);
            */


            /*
            Test Step 13
            Action: Perform action step 2-7 for keypad of the ‘Day’ input field
            Expected Result: See the expected results of Step 2 – Step 7 and the following additional information,(1)The pressed key is added in an input field immediately. (2)The cursor is jumped to next position after entered the character immediately.(3)The input field is used to enter the Day
            Test Step Comment: (1) MMI_gen 11942 (partly: MMI_gen 4642 (partly: Day));  (2) MMI_gen 11942 (partly: MMI_gen 4692 (partly: Day));  (3) MMI_gen 11953 (partly: entry);
            */


            /*
            Test Step 14
            Action: Delete the old value and enter the new value ‘31’ for Day.Then, confirm an entered data by pressing an input field
            Expected Result: Verify the following information,Input fields(1)The associated ‘Enter’ button is data field itself.(2)An input field is used to allow the driver to enter data.(3)The state of ‘Day’ input field is changed to ‘accepted’ as follows,The background colour of the Data Area is dark-grey.The colour of data value is white.(4)The next input field ‘Hour’ is in state ‘selected’ as follows,The background colour of the Data Area is medium-grey.The colour of data value is black.(5) There are only 4 input fields displayed in the second page of window.(6)The label of 4th input field is ‘Hour’.(7)The label of 5th input field is ‘Minute’.(8)The label of 6th input field is ‘Second’.(9)The label of 7th input field is ‘Offset’.Echo Texts(10)The echo text of ‘Day ‘is changed to white colour.(11)The value of echo text is changed refer to entered data.Entering Characters (12)The cursor is displayed as a horizontal line below the position of the next character to be entered.(13)The cursor is flashed by changing from visible to not visible.Keyboard(14)The keyboard associated to selected input field ‘Hour’ is Numeric keyboard.(15)The keyboard contains enabled button for the number <1> to <9>, <Delete>(NA21) , <0> and disabled <Decimal_Separator>. NA21, Delete button.Navigation button(16)The state of ‘Previous’ and ‘Next’ button are displayed as follows, ‘Next’ button is disabled, display symbol NA18.2 ‘Previous’ button is enable, display symbol NA18(17)   The window title is displayed as ‘Set clock (2/2)’
            Test Step Comment: (1) MMI_gen 11942 (partly: MMI_gen 4682 (partly: Day));(2) MMI_gen 11942 (partly: MMI_gen 4634 (partly: Day));(3) MMI_gen 11942 (partly: MMI_gen 4652 (partly: Day), MMI_gen 4684 (partly: accepted, Day));(4) MMI_gen 11942 (partly: MMI_gen 4684 (partly: Hour, selected automatically), MMI_gen 4651 (partly: Hour));(5) MMI_gen 12125 (partly: second page);(6) MMI_gen 11956 (partly: label);(7) MMI_gen 11957 (partly: label);(8) MMI_gen 11958 (partly: label);(9) MMI_gen 11954 (partly: label);(10) MMI_gen 11942 (partly: MMI_gen 4700 (partly: Day));(11) MMI_gen 11942 (partly: MMI_gen 4681 (partly: Day), MMI_gen 4890, MMI_gen 4698);(12 )MMI_gen 11942 (partly: MMI_gen 4689, MMI_gen 4690);(13) MMI_gen 11942 (partly: MMI_gen 4691 (partly: flash, Hour));(14) MMI_gen 11944  (partly: Hour); MMI_gen 11942 (partly: MMI_gen 4912 (partly: Hour), MMI_gen 4678 (partly: Hour));(15) MMI_gen 11942 (partly: MMI_gen 5003 (partly: Hour)); MMI_gen 4392 (partly: [Delete] NA21); (16)  MMI_gen 4394 (partly: enabled [previous], disabled [next]); MMI_gen 4358;(17) MMI_gen 4360 (partly: total number of window);
            */


            /*
            Test Step 15
            Action: Perform action step 2-7 for keypad of the ‘Hour’ input field
            Expected Result: See the expected results of Step 2 – Step 7 and the following additional information,(1)The pressed key is added in an input field immediately. (2)The cursor is jumped to next position after entered the character immediately.(3)The input field is used to enter the Hour
            Test Step Comment: (1) MMI_gen 11942 (partly: MMI_gen 4642 (partly: Hour));  (2) MMI_gen 11942 (partly: MMI_gen 4692 (partly: Hour));  (3) MMI_gen 11956 (partly: entry);
            */


            /*
            Test Step 16
            Action: Delete the old value and enter the new value ‘11’ for Hour.Then, confirm an entered data by pressing an input field
            Expected Result: Verify the following information,Input fields(1)The associated ‘Enter’ button is data field itself.(2)An input field is used to allow the driver to enter data.(3)The state of ‘Hour’ input field is changed to ‘accepted’ as follows,The background colour of the Data Area is dark-grey.The colour of data value is white.(4)The next input field ‘Minute’ is in state ‘selected’ as follows,The background colour of the Data Area is medium-grey.The colour of data value is black.Echo Texts(5)The echo text of ‘Hour ‘is changed to white colour.(6)The value of echo text is changed refer to entered data.Entering Characters (7)The cursor is displayed as a horizontal line below the position of the next character to be entered.(8)The cursor is flashed by changing from visible to not visible.Keyboard(9)The keyboard associated to selected input field ‘Minute’ is Numeric keyboard.(10)The keyboard contains enabled button for the number <1> to <9>, <Delete>(NA21) , <0> and disabled <Decimal_Separator>. NA21, Delete button
            Test Step Comment: (1) MMI_gen 11942 (partly: MMI_gen 4682 (partly: Hour));(2) MMI_gen 11942 (partly: MMI_gen 4634 (partly: Hour));(3) MMI_gen 11942 (partly: MMI_gen 4652 (partly: Hour), MMI_gen 4684 (partly: accepted, Hour));(4) MMI_gen 11942 (partly: MMI_gen 4684 (partly: Minute, selected automatically), MMI_gen 4651 (partly: Minute));(5) MMI_gen 11942 (partly: MMI_gen 4700 (partly: Hour));(6) MMI_gen 11942 (partly: MMI_gen 4681 (partly: Hour), MMI_gen 4890, MMI_gen 4698);(7) MMI_gen 11942 (partly: MMI_gen 4689, MMI_gen 4690);(8) MMI_gen 11942 (partly: MMI_gen 4691 (partly: flash, Minute));(9) ) MMI_gen 11944  (partly: Minute); MMI_gen 11942 (partly: MMI_gen 4912 (partly: Minute), MMI_gen 4678 (partly: Minute));(10) MMI_gen 11942 (partly: MMI_gen 5003 (partly: Minute)); MMI_gen 4392 (partly: [Delete] NA21);
            */


            /*
            Test Step 17
            Action: Perform action step 2-7 for keypad of the ‘Minute’ input field
            Expected Result: See the expected results of Step 2 – Step 7 and the following additional information,(1)The pressed key is added in an input field immediately. (2)The cursor is jumped to next position after entered the character immediately.(3)The input field is used to enter the Minute
            Test Step Comment: (1) MMI_gen 11942 (partly: MMI_gen 4642 (partly: Minute));  (2) MMI_gen 11942 (partly: MMI_gen 4692 (partly: Minute));  (3) MMI_gen 11957 (partly: entry);
            */


            /*
            Test Step 18
            Action: Delete the old value and enter the new value ‘11’ for Minute.Then, confirm an entered data by pressing an input field
            Expected Result: Verify the following information,Input fields(1)The associated ‘Enter’ button is data field itself.(2)An input field is used to allow the driver to enter data.(3)The state of ‘Minute’ input field is changed to ‘accepted’ as follows,The background colour of the Data Area is dark-grey.The colour of data value is white.(4)The next input field ‘Second’ is in state ‘selected’ as follows,The background colour of the Data Area is medium-grey.The colour of data value is black.Echo Texts(5)The echo text of ‘Minute ‘is changed to white colour.(6)The value of echo text is changed refer to entered data.Entering Characters (7)The cursor is displayed as a horizontal line below the position of the next character to be entered.(8)The cursor is flashed by changing from visible to not visible.Keyboard(9)The keyboard associated to selected input field ‘Second’ is Numeric keyboard.(10)The keyboard contains enabled button for the number <1> to <9>, <Delete>(NA21) , <0> and disabled <Decimal_Separator>. NA21, Delete button
            Test Step Comment: (1) MMI_gen 11942 (partly: MMI_gen 4682 (partly: Minute));(2) MMI_gen 11942 (partly: MMI_gen 4634 (partly: Minute));(3) MMI_gen 11942 (partly: MMI_gen 4652 (partly: Minute), MMI_gen 4684 (partly: accepted, Minute));(4) MMI_gen 11942 (partly: MMI_gen 4684 (partly: Second, selected automatically), MMI_gen 4651 (partly: Second));(5) MMI_gen 11942 (partly: MMI_gen 4700 (partly: Minute));(6) MMI_gen 11942 (partly: MMI_gen 4681 (partly: Minute), MMI_gen 4890, MMI_gen 4698);(7) MMI_gen 11942 (partly: MMI_gen 4689, MMI_gen 4690);(8) MMI_gen 11942 (partly: MMI_gen 4691 (partly: flash, Second));(9) ) MMI_gen 11944  (partly: Second); MMI_gen 11942 (partly: MMI_gen 4912 (partly: Second), MMI_gen 4678 (partly: Second));(10) MMI_gen 11942 (partly: MMI_gen 5003 (partly: Second)); MMI_gen 4392 (partly: [Delete] NA21);
            */


            /*
            Test Step 19
            Action: Perform action step 2-7 for keypad of the ‘Second’ input field
            Expected Result: See the expected results of Step 2 – Step 7 and the following additional information,(1)The pressed key is added in an input field immediately. (2)The cursor is jumped to next position after entered the character immediately.(3)The input field is used to enter the Second
            Test Step Comment: (1) MMI_gen 11942 (partly: MMI_gen 4642 (partly: Second));  (2) MMI_gen 11942 (partly: MMI_gen 4692 (partly: Second));  (3) MMI_gen 11958 (partly: entry);
            */


            /*
            Test Step 20
            Action: Delete the old value and enter the new value ‘11’ for Second.Then, confirm an entered data by pressing an input field
            Expected Result: Verify the following information,Input fields(1)The associated ‘Enter’ button is data field itself.(2)An input field is used to allow the driver to enter data.(3)The state of ‘Second’ input field is changed to ‘accepted’ as follows,The background colour of the Data Area is dark-grey.The colour of data value is white.(4)The next input field ‘Offset’ is in state ‘selected’ as follows,The background colour of the Data Area is medium-grey.The colour of data value is black.Echo Texts(5)The echo text of ‘Second ‘is changed to white colour.(6)The value of echo text is changed refer to entered data.Entering Characters (7)The cursor is displayed as a horizontal line below the position of the next character to be entered.(8)The cursor is flashed by changing from visible to not visible.Keyboard(9)The keyboard associated to selected input field ‘Offset’ is Dedicated keyboard. (10)The label of key#1 is ‘+’ and the label of key#2 is ‘-‘
            Test Step Comment: (1) MMI_gen 11942 (partly: MMI_gen 4682 (partly: Second));(2) MMI_gen 11942 (partly: MMI_gen 4634 (partly: Second));(3) MMI_gen 11942 (partly: MMI_gen 4652 (partly: Second), MMI_gen 4684 (partly: accepted, Second));(4) MMI_gen 11942 (partly: MMI_gen 4684 (partly: Offset, selected automatically), MMI_gen 4651 (partly: Offset));(5) MMI_gen 11942 (partly: MMI_gen 4700 (partly: Second));(6) MMI_gen 11942 (partly: MMI_gen 4681 (partly: Second), MMI_gen 4890, MMI_gen 4698);(7) MMI_gen 11942 (partly: MMI_gen 4689, MMI_gen 4690);(8) MMI_gen 11942 (partly: MMI_gen 4691 (partly: flash, Offset));(9) MMI_gen 11944  (partly: Offset); MMI_gen 11942 (partly: MMI_gen 4912 (partly: except Offset), MMI_gen 4678 (partly: Offset)); MMI_gen 11945 (partly: dedicated keyboard);(10) MMI_gen 11945 (partly: ‘+’ and ‘-‘ keys);
            */


            /*
            Test Step 21
            Action: Perform action step 2-7 for keypad of ‘+’ button for the ‘Offset’ input field
            Expected Result: See the expected results of Step 2 – Step 7 and the following additional information,(1)The value of offset time is increased 15 minute. (2)The input field is used to enter the Offset.(3)   The Offset time is editable refer to received packet EVC-30 with variable and MMI_Q_REQUEST_ENABLE_64 #25 = 1 and MMI_Q_REQUEST_ENABLE_64 #26 = 1
            Test Step Comment: (1) MMI_gen 11942 (partly: MMI_gen 4642 (partly: Offset));  MMI_gen 11945 (partly: increase with 15 minute);(2) MMI_gen 11954 (partly: entry);(3) MMI_gen 2450;
            */


            /*
            Test Step 22
            Action: Press ‘+’ button until the value is increased to ‘+12:00’.Then, press ‘+’ button again
            Expected Result: Verify the following information,(1) The value of offset time still not changes and display as ‘+12:00’
            Test Step Comment: (1) MMI_gen 11945 (partly: offset range +12:00);
            */


            /*
            Test Step 23
            Action: Perform action step 2-7 for keypad of ‘-’ button for the ‘Offset’ input field
            Expected Result: See the expected results of Step 2 – Step 7 and the following additional information,(1)The value of offset time is decreased 15 minute. (2)The input field is used to enter the Offset
            Test Step Comment: (1) MMI_gen 11942 (partly: MMI_gen 4642 (partly: Offset));  MMI_gen 11945 (partly: decrease with 15 minute);(2) MMI_gen 11954 (partly: entry);
            */


            /*
            Test Step 24
            Action: Press ‘+’ button until the value is increased to ‘-12:00’.Then, press ‘-’ button again
            Expected Result: Verify the following information,(1) The value of offset time is still not changes and it is displayed as ‘-12:00’
            Test Step Comment: (1) MMI_gen 11945 (partly: offset range -12:00);
            */


            /*
            Test Step 25
            Action: Confirm an entered data by pressing an input field
            Expected Result: Verify the following information,Input fields(1)The associated ‘Enter’ button is data field itself.(2)An input field is used to allow the driver to enter data.(3)The state of ‘Offset’ input field is changed to ‘accepted’ as follows,The background colour of the Data Area is dark-grey.The colour of data value is white.(4)There is no input field selected.Echo Texts(5)The echo text of ‘Offset ‘is changed to white colour.(6)The value of echo text is changed refer to entered data.Data Entry Window(7)The state of ‘Yes’ button below text label ‘Clock set?’ is enabled as follows,The background colour of the Data Area is medium-grey.The colour of data value is black.The colour of border is medium-grey
            Test Step Comment: (1) MMI_gen 11942 (partly: MMI_gen 4682 (partly: Offset));(2) MMI_gen 11942 (partly: MMI_gen 4634 (partly: Offset));(3) MMI_gen 11942 (partly: MMI_gen 4652 (partly: Offset), MMI_gen 4684 (partly: accepted, Offset));(4) MMI_gen 11942 (partly: MMI_gen 4684 (partly: No next input field, data entry process terminated));(5) MMI_gen 11942 (partly: MMI_gen 4700 (partly: Offset));(6) MMI_gen 11942 (partly: MMI_gen 4681 (partly: Offset), MMI_gen 4890, MMI_gen 4698);(7) MMI_gen 11942 (partly: MMI_gen 4909 (partly: Enabled), MMI_gen 4910 (partly: Enabled, MMI_gen 4211 (partly: colour))); MMI_gen 4374;
            */
            // Call generic Action Method
            DmiActions.Confirm_an_entered_data_by_pressing_an_input_field(this);


            /*
            Test Step 26
            Action: Press and hold ‘Previous’ button
            Expected Result: Verify the following information,(1)The state of button is changed to ‘Pressed’, the border of button is removed.(2)The sound ‘Click’ is played once
            Test Step Comment: (1) MMI_gen 11942 (partly: MMI_gen 9391 (partly: [Previous], MMI_gen 4381 (partly: change to state ‘Pressed’ as long as remain actuated)); MMI_gen 4375;(2) MMI_gen 11942 (partly: MMI_gen 9391 (partly: [Previous], MMI_gen 4381 (partly: sound ‘Click’)); MMI_gen 9512; MMI_gen 968;
            */


            /*
            Test Step 27
            Action: Slide out the ‘Previous’ button
            Expected Result: Verify the following information,(1)The border of the button is shown (state ‘Enabled’) without a sound
            Test Step Comment: (1) MMI_gen 11942 (partly: MMI_gen 9391 (partly: [Previous], MMI_gen 4382 (partly: state ‘Enabled’ when slide out with force applied, no sound))); MMI_gen 4374;
            */


            /*
            Test Step 28
            Action: Slide back into the ‘Previous’ button
            Expected Result: Verify the following information,(1)The border of the button is back to state ‘Pressed’ without a sound
            Test Step Comment: (1) MMI_gen 11942 (partly: MMI_gen 9391 (partly: [Previous], MMI_gen 4382 (partly: state ‘Pressed’ when slide back, no sound))); MMI_gen 4375;
            */


            /*
            Test Step 29
            Action: Release ‘Previous’ button
            Expected Result: Verify the following information,(1)The state of released button is changed to disabled.(2)   DMI displays the first page of Set clock window
            Test Step Comment: (1) MMI_gen 11942 (partly: MMI_gen 9391 (partly: [Previous], MMI_gen 4381 (partly: exit state ‘pressed’)));(2) MMI_gen 11942 (partly: MMI_gen 9391 (partly: [Previous], MMI_gen 4384 (partly: ETCS-MMI’s function associated to the button));
            */


            /*
            Test Step 30
            Action: Perform action step 24-27 for ‘Next’ button
            Expected Result: See the expected results of Step 24 – Step 27 and the following additional information,(1)The state of released button is changed to disabled.(2)   DMI displays the second page of Set clock window
            Test Step Comment: (1) MMI_gen 11942 (partly: MMI_gen 9391 (partly: [Next], MMI_gen 4381 (partly: exit state ‘pressed’)));(2) MMI_gen 11942 (partly: MMI_gen 9391 (partly: [Next], MMI_gen 4384 (partly: ETCS-MMI’s function associated to the button));
            */


            /*
            Test Step 31
            Action: Perform the following procedure,Press ‘Previous’ button.Select ‘Year’ input field.Enter the new value without pressing an input field.Select ‘Day’ input field
            Expected Result: Verify the following information,(1) The state of ‘Yes’ button below text label ‘Clock set?’ is disabled. (2) The state of input field ‘Year’ is changed to ‘Not selected’ as follows,The value of ‘Year’ input field is removed and displayed as blank.The background colour of the input field is dark-grey
            Test Step Comment: (1) MMI_gen 11942 (partly: MMI_gen 4909 (partly: state selected and with recently entered key), MMI_gen 4680 (partly: value has been modified));(2) MMI_gen 11942 (partly: MMI_gen 4680 (partly: Year, Not selected, Data area is blank), MMI_gen 4649 (partly: data entry, background colour));
            */


            /*
            Test Step 32
            Action: Perform the following procedure,Press ‘Next button.Select ‘Minute’ input field.Confirm the value of ‘Minute’ by pressing an input field
            Expected Result: Verify the following information,(1) The state of input field ‘Year’ is changed to ‘selected’
            Test Step Comment: (1) MMI_gen 11942 (partly: MMI_gen 4685);
            */


            /*
            Test Step 33
            Action: Enter the value ‘2016’ for ‘Year’.Then, confirm an entered data by pressing an input field
            Expected Result: The state of ‘Yes’ button is changed to enabled
            */
            // Call generic Check Results Method
            DmiExpectedResults.The_state_of_Yes_button_is_changed_to_enabled(this);


            /*
            Test Step 34
            Action: Perform action step 24-27 for ‘Yes’ button
            Expected Result: See the expected results of Step 24 – Step 27 and the following additional information,(1)DMI displays Settings window. (2)Use the log file to confirm that DMI sent out packet [MMI_SET_TIME_MMI (EVC-109)] with,MMI_T_ZONE_OFFSET = -48MMI_T_UTC = 1481429471
            Test Step Comment: (1) MMI_gen 11942 (partly: MMI_gen 4911); MMI_gen 11940 (partly: close the window);(2) MMI_gen 11940 (partly: EVC-109); MMI_gen 2451;
            */


            /*
            Test Step 35
            Action: Press ‘Set clock’ button
            Expected Result: DMI displays Set clock window
            */


            /*
            Test Step 36
            Action: Press and hold the Label area of ‘Month’ input field
            Expected Result: Verify the following information,(1)    The state of ‘Month’ input field is changed to ‘Pressed’, the border of button is removed.The state of ‘Month’ input field remains ‘not selected’. The state of ‘Year’ input field remains ‘selected’.(2)    The sound ‘Click’ is played once
            Test Step Comment: (1) MMI_gen 11942 (partly: MMI_gen 4686 (partly: Label area, Month), MMI_gen 4381 (partly: change to state ‘Pressed’ as long as remain actuated))); MMI_gen 4392 (partly: [Enter], touch screen); MMI_gen 4375;(2) MMI_gen 11942 (partly: MMI_gen 4686 (partly: Label area, Month), MMI_gen 4381 (partly: the sound for Up-Type button));
            */


            /*
            Test Step 37
            Action: Slide out the Label area of ‘Month’ input field
            Expected Result: Verify the following information,(1)    The border of ‘Month’ input field is shown (state ‘Enabled’) without a sound.The state of ‘Month’ input field remains ‘not selected’. The state of ‘Year’ input field remains ‘selected’
            Test Step Comment: (1) MMI_gen 11942 (partly: MMI_gen 4686 (partly: Label area, Month), MMI_gen 4382 (partly: state ‘Enabled’ when slide out with force applied, no sound); MMI_gen 4374;
            */


            /*
            Test Step 38
            Action: Slide back into the Label area of ‘Month’ input field
            Expected Result: Verify the following information,(1)    The state of ‘Month’ input field is changed to ‘Pressed’, the border of button is removed.The state of ‘Month’ input field remains ‘not selected’. The state of ‘Year’ input field remains ‘selected’
            Test Step Comment: (1) MMI_gen 11942 (partly: MMI_gen 4686 (partly: Label area, Month), MMI_gen 4382 (partly: state ‘Pressed’ when slide back, no sound); MMI_gen 4375;
            */


            /*
            Test Step 39
            Action: Release the pressed area
            Expected Result: Verify the following information,(1)    The state of ‘Month’ input field is changed to selected
            Test Step Comment: (1) MMI_gen 11942 (partly: MMI_gen 4686 (partly: Label area, Month), MMI_gen 4381 (partly: exit state ‘Pressed’, execute function associated to the button)); MMI_gen 4374;
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Release the pressed area");


            /*
            Test Step 40
            Action: Perform action step 34-37 for the Label area of each input field
            Expected Result: Verify the following information,(1)    The state of an input field is changed to ‘selected’ when release the pressed area at the Label area of input field
            Test Step Comment: (1) MMI_gen 11942 (partly: MMI_gen 4686 (partly: Label area, Up-Type button));
            */


            /*
            Test Step 41
            Action: Perform action step 34-37 for the Data area of each input field
            Expected Result: Verify the following information,(1)    The state of an input field is changed to ‘selected’ when release the pressed area at the Data area of input field
            Test Step Comment: (1) MMI_gen 11942 (partly: MMI_gen 4686 (partly: Data area)); MMI_gen 9390 (partly: Set Clock window);
            */


            /*
            Test Step 42
            Action: Perform the following procedure,Press ‘Language’ button.Select and confirm another language except English.Press ‘Set Clock’ button
            Expected Result: DMI displays Set clock window.Verify the following information,The labels are updated refer to selected language
            Test Step Comment: (1) MMI_gen 11943;
            */


            /*
            Test Step 43
            Action: Perform the following procedure,Press ‘Close’ button.Press ‘Language’ button.Select and confirm English language
            Expected Result: The labels are updated refer to selected language
            */


            /*
            Test Step 44
            Action: Use the test script file 22_13_1_a.xml to send EVC-30
            Expected Result: The Set clock button is disabled
            */
            // Call generic Check Results Method
            DmiExpectedResults.The_Set_clock_button_is_disabled(this);


            /*
            Test Step 45
            Action: Use the test script file 22_13_1_c.xml to send EVC-30 with,MMI_NID_WINDOW = 4MMI_Q_REQUEST_ENABLE_64 (#25) = 0MMI_Q_REQUEST_ENABLE_64 (#26) = 1
            Expected Result: The Set clock button is enabled
            Test Step Comment: MMI_gen 1563         (partly: enabled bit#26);             
            */
            // Call generic Check Results Method
            DmiExpectedResults.The_Set_clock_button_is_enabled(this);


            /*
            Test Step 46
            Action: Press ‘Set Clock’ button. Then, select an input field ‘Offset’ at second page
            Expected Result: Verify the following information, (1)   The Offset time is editable refer to received packet EVC-30 with variable MMI_Q_REQUEST_ENABLE_64 #26 = 1(2)   All other input fields except Offset time are non-editable refer to received packet EVC-30 with variable MMI_Q_REQUEST_ENABLE_64 #25 = 0
            Test Step Comment: (1) MMI_gen 2450 (partly: bit #26);(2) MMI_gen 1563 (partly: disabled bit #25);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this,
                @"Press ‘Set Clock’ button. Then, select an input field ‘Offset’ at second page");


            /*
            Test Step 47
            Action: Perform the following procedure,Change the value of Offset time to “+12:00”Confirm an entered value by pressing an input field.Press ‘Yes’ button
            Expected Result: Verify the following information,(1)  Use the log file to confirm that DMI sent out packet [MMI_SET_TIME_MMI (EVC-109)] with,MMI_T_ZONE_OFFSET = 48MMI_T_UTC = current UTC time calculated from current UTC time
            Test Step Comment: (1) MMI_gen 2452;
            */


            /*
            Test Step 48
            Action: Perform the following procedure,Use the test script file 22_13_1_a.xml to send EVC-30.Then, Use the test script file 22_13_1_b.xml to send EVC-30 with,MMI_NID_WINDOW = 4MMI_Q_REQUEST_ENABLE_64 (#25) = 1MMI_Q_REQUEST_ENABLE_64 (#26) = 0
            Expected Result: The Set clock button is enabled
            Test Step Comment: MMI_gen 1563         (partly: enabled bit#25);             
            */
            // Call generic Check Results Method
            DmiExpectedResults.The_Set_clock_button_is_enabled(this);


            /*
            Test Step 49
            Action: Press ‘Set Clock’ button. Then, select an input field ‘Offset’ at second page
            Expected Result: Verify the following information, (1)   The Offset time is editable  refer to received packet EVC-30 with variable MMI_Q_REQUEST_ENABLE_64 #25 = 1
            Test Step Comment: (1) MMI_gen 2450 (partly: bit #25);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this,
                @"Press ‘Set Clock’ button. Then, select an input field ‘Offset’ at second page");


            /*
            Test Step 50
            Action: Press the ‘Close’ button
            Expected Result: Verify the following information,(1)   DMI displays Settings window
            Test Step Comment: (1) MMI_gen 4392 (partly: returning to the parent window);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button");
            // Call generic Check Results Method
            DmiExpectedResults.Verify_the_following_information_1_DMI_displays_Settings_window(this);


            /*
            Test Step 51
            Action: End of test
            Expected Result: 
            */


            return GlobalTestResult;
        }
    }
}
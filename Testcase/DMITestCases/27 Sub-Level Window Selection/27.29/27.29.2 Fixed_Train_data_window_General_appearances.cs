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
    /// 27.29.2 Fixed Train data window: General appearances
    /// TC-ID: 22.29.2
    /// 
    /// This test case verifies the display of the ‘Train Data’ window on DMI that shall comply with [ERA-ERTMS] standard and [MMI-ETCS-gen].
    /// 
    /// Tested Requirements:
    /// MMI_gen 9414; MMI_gen 9459; MMI_gen 9460 (partly: [enter], [yes], EVC-107); MMI_gen 187; MMI_gen 8087; MMI_gen 9402 (partly: fixed); MMI_gen 9405; MMI_gen 8104; MMI_gen 11384; MMI_gen 11385; MMI_gen 8086 (partly: MMI_gen 4888, MMI_gen 4799 (partly: Close button, Previous button, Next button, Window Title, Input fields), MMI_gen 4891 (partly: Yes button, Area for [Window Title] Entry complete?), MMI_gen 4910, MMI_gen 4211 (partly: colour), MMI_gen 4909, MMI_gen 4908 (partly: extended), MMI_gen 4637 (partly: Main-areas D and F), MMI_gen 4640, MMI_gen 4641, MMI_gen 9412, MMI_gen 4645, MMI_gen 4646 (partly: right aligned), MMI_gen 4647 (partly: left aligned), MMI_gen 4648, MMI_gen 4720, MMI_gen 4651, MMI_gen 4683, MMI_gen 5211, MMI_gen 4649, MMI_gen 4912, MMI_gen 4678, MMI_gen 9336, MMI_gen 5190, MMI_gen 4696, MMI_gen 4697, MMI_gen 4701, MMI_gen 4702 (partly: right aligned), MMI_gen 4704 (partly: left aligned), MMI_gen 4700, MMI_gen 4691 (partly: flash), MMI_gen 4689, MMI_gen 4690, MMI_gen 9391 (partly: [More], [Previuos], [Next], MMI_gen 4381, MMI_gen 4382), MMI_gen 4913 (partly: MMI_gen 4384, MMI_gen 4386), MMI_gen 4682 , MMI_gen 4634 , MMI_gen 4652,  MMI_gen 4684, MMI_gen 4642, MMI_gen 5003, MMI_gen 4681, MMI_gen 4680, MMI_gen 4685, MMI_gen 4911 (partly: MMI_gen 4381, MMI_gen 4382), MMI_gen 4686, MMI_gen 4698); MMI_gen 4392 (partly: [Close] NA11, [More: NA23], [Delete] NA21,Enter], touch screen, returning to the parent window); MMI_gen 4355; MMI_gen 4396 (partly: Previous, NA19); MMI_gen 4377 (partly: shown); MMI_gen 4375; MMI_gen 9512; MMI_gen 968; MMI_gen 4374;  MMI_gen 5387; MMI_gen 11382; MMI_gen 4241; MMI_gen 9409; MMI_gen 4350; MMI_gen 4351; MMI_gen 4353; MMI_gen 9390 (partly: Fixed Train data window);
    /// 
    /// Scenario:
    /// 1.The Train data window appearance is verified.
    /// 2.The data entry functionality of the Train data window is verified with the following type of button in keypad,The Train type input field with Dedicated keyboardThe state of the ‘Enter’ button associated to the input fieldThe Up-Type button on ‘Yes’ button
    /// 3.The Up-Type button on each label part of an input field are verified.
    /// 4.The Up-Type button on each data part of an input field are verified.
    /// 5.The functionality of ‘Close’ button is verified.
    /// 6.The state of ‘Yes’ button when other rule is requires to disabling is verified.
    /// 
    /// Used files:
    /// 22_29_2_a.xml, 22_29_2_b.xml
    /// </summary>
    public class Fixed_Train_data_window_General_appearances : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // Open defaultValues_default.xml in OTE  then set all value of parameter "TR_OBU_TrainType" to 1Test system is powered ON.Cabin is activated.Perform SoM until until Level 1 is selected and confirmed.

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
            Expected Result: DMI displays Train Data window.Verify the following information,Data Entry Window(1)   The window title is ‘Train data’.(2)   The text label of the window title is right aligned.(3)   The following objects are displayed in Train data window,  Enabled Close button (NA11)Window TitleInput fields(4)   The following objects are additionally displayed in Train data window,Yes buttonThe text label ‘Train data entry complete?’(5)   Yes button is displayed in Disabled state as follows,Text label is black Background colour is dark-greyThe border colour is medium-grey the same as the input field’s colour.(6)   The sensitive area of Yes button is extended from text label ‘Train data Entry complete?’Input fields(7)   The input fields are located on Main area D and F.(8)   Each input field is devided into a Label Area and a Data Area.(9)   The Label Area is presented give the topic of the input field.(10)  The Label Area text is displayed corresponding to the input field i.e. Train type. (11) The Label Area is placed to the left of The Data Area.(12)  The text in the Label Area is aligned to the right.(13)  The value of data in the Data Area is aligned to the left.(14)  The text colour of the Label Area is grey and the background colour of the Label Area is dark-grey.(15)  There are only 1 input field that displayed in the window.(16)  The first input field is in state ‘Selected’ as follows,The background colour of the Data Area is medium-grey.The colour of data value is black.Keyboard(17)  The keyboard associated to selected input field ‘Train Type’ is Dedicated keyboard with each key label giving the name of the train type.Layers(18) The level of layers of all area in window are displayed in Layer 0.Echo Texts(19)  An echo text is composed of Label Part and Data Part.(20)  The Label Part of an echo texts is same as The Label area of an input fields.(21)  The echo texts are displayed in main area A, B, C and E with same order as their related input fields.(22)  The Label part of echo text is right aligned.(23)  The Data part of echo text is left aligned.(24)  The colour of texts in echo texts are grey.Entering Characters(25)  The cursor is flashed by changing from visible to not visible.(26)  The cursor is displayed as horizontal line below the value in the input field.Packet transmission(27)  Use the log file to confirm that DMI received packet information [MMI_CURRENT_TRAIN_DATA (EVC-6)] with following variables,MMIM_N_DATA_ELEMENTS = 0MMI_M_DATA_ENABLE (bit #0) = 1MMI_M_TRAINSET_ID = 1-9(28)  An amount of keypad buttons is corresponding with value of MMI_N_TRAINSETS in received packet EVC-6.(29)  There is only ‘Train Type’ input field displays in Train data window.(30)  The label of each button on keypad are corresponding with each index of MMI_X_CAPTION_TRAINSET in received packet EVC-6.(31) The data value of an input field is displayed same as the label on keypad button refer to an index in the value MMI_N_TRAINSET_ID.(e.g. MMI_N_TRAINSET_ID = 3 is mean to label of keypad#3, see picture below).General property of window(32) The Train data window is presented with objects and buttons which is the one of several levels and allocated to areas of DMI.(33) All objects, text messages and buttons are presented within the same layer.(34) The Default window is not displayed and covered the current window
            Test Step Comment: (1) MMI_gen 8087; MMI_gen 4355 (partly: Window title);(2) MMI_gen 8086 (partly: MMI_gen 4888);(3) MMI_gen 8086 (partly: MMI_gen 4799 (partly: Close button, Previous button, Next button, Window Title, Input fields)); MMI_gen 4392 (partly: [Close] NA11); MMI_gen 4355 (partly: Buttons, Close button);(4) MMI_gen 8086 (partly: MMI_gen 4891 (partly: Yes button, Area for [Window Title] Entry complete?));(5) MMI_gen 8086 (partly: MMI_gen 4910 (partly: Disabled, MMI_gen 4211 (partly: colour)), MMI_gen 4909 (partly: Disabled)); MMI_gen 4377 (partly: shown);(6) MMI_gen 8086 (partly: MMI_gen 4908 (partly: extended));(7) MMI_gen 8086 (partly: MMI_gen 4637 (partly: Main-areas D and F)); MMI_gen 4355 (partly: input fields);(8) MMI_gen 8086 (partly: MMI_gen 4640);(9) MMI_gen 8086 (partly: MMI_gen 4641);(10) MMI_gen 8086 (partly: MMI_gen 9412);(11) MMI_gen 8086 (partly: MMI_gen 4645);(12) MMI_gen 8086 (partly: MMI_gen 4646 (partly: right aligned));(13) MMI_gen 8086 (partly: MMI_gen 4647 (partly: left aligned));(14) MMI_gen 8086 (partly: MMI_gen 4648);(15) MMI_gen 8086 (partly: MMI_gen 4720);(16) MMI_gen 8086 (partly: MMI_gen 4651 (partly: Train Type), MMI_gen 4683 (partly: selected), MMI_gen 5211 (partly: selected));(17) MMI_gen 8104; MMI_gen 8086 (partly: MMI_gen 4912, MMI_gen 4678);(18) MMI_gen 8086 (partly: MMI_gen 5190);(19) MMI_gen 8086 (partly: MMI_gen 4696);(20) MMI_gen 8086 (partly: MMI_gen 4697); MMI_gen 187 (partly: label part of echo text);(21) MMI_gen 8086 (partly: MMI_gen 4701);(22) MMI_gen 8086 (partly: MMI_gen 4702 (partly: right aligned));(23) MMI_gen 8086 (partly: MMI_gen 4704 (partly: left aligned));(24) MMI_gen 8086 (partly: MMI_gen 4700 (partly: otherwise, grey)); MMI_gen 4241;(25) MMI_gen 8086 (partly: MMI_gen 4691 (partly: flash, Train category));(26) MMI_gen 8086 (partly: MMI_gen 4689, MMI_gen 4690);(27) MMI_gen 187 (partly: EVC-6, MMI_N_DATA_ELEMENT = 0, MMI_M_DATA_ENABLE ); MMI_gen 9402 (partly: fixed);(28) MMI_gen 9414; MMI_gen 11384 (partly: number of train sets);(29) MMI_gen 187 (partly: Not editable  data);(30) MMI_gen 11384 (partly: caption texts);(31) MMI_gen 11384 (partly: indicate the train set);(32) MMI_gen 4350;(33) MMI_gen 4351;(34) MMI_gen 4353;
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(@"Press ‘Train Data’ button");


            /*
            Test Step 2
            Action: Press and hold the button on keypad
            Expected Result: Verify the following information,(1)   The state of ‘PASS 2‘ button is changed to ‘Pressed’ and immediately back to ‘Enabled’ state.(2)   The sound ‘Click’ is played once.(3)   The Input Field displays the value associated to the data key according to the pressings in state ‘Pressed’.(4)   The cursor is displayed as horizontal line below the value of the dedicated-keyboard data key in the input field
            Test Step Comment: (1) MMI_gen 8086 (partly: MMI_gen 4913 (partly: Train Set); MMI_gen 4384 (partly: Change to state ‘Pressed’ and immediately back to state ‘Enabled’);   (2) MMI_gen 8086 (partly: MMI_gen 4913 (partly: Train Set)); MMI_gen 4384 (partly: sound ‘Click’); MMI_gen 9512; MMI_gen 968;(3) MMI_gen 8086 (partly: MMI_gen 4679 (partly: Train Set, MMI_gen 4913 (partly: Train Set); MMI_gen 4384 (partly: ETCS-MMI’s function associated to the button));(4) MMI_gen 8086 (partly: MMI_gen 4689, MMI_gen 4690);
            */


            /*
            Test Step 3
            Action: Release pressed button
            Expected Result: Verify the following information,(1)   The state of released button is changed to enabled
            Test Step Comment: (1) MMI_gen 8086 (partly: MMI_gen 4913 (partly: Train category); MMI_gen 4384 (partly: ETCS-MMI’s function associated to the button));
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(@"Release pressed button");


            /*
            Test Step 4
            Action: Perform action step 2-3 for the remaining buttons on keypad
            Expected Result: See the expected results of Step 2 – Step 3
            */


            /*
            Test Step 5
            Action: Press the keypad#1 (see picture below)Then, confirm an entered data by pressing an input field
            Expected Result: Verify the following information,Input fields(1) The associated ‘Enter’ button is data field itself.(2) An input field is used to allow the driver to enter data.(3) The state of ‘Train Set’ input field is changed to ‘accepted’ as follows,The background colour of the Data Area is dark-grey.The colour of data value is white.(4) There is no input field selected.Echo Texts(5) The echo text of ‘Train Set’ is changed to white colour.(6) The value of echo text is changed refer to entered data.Data Entry window(7) The state of ‘Yes’ button below text label ‘Train data Entry is complete?’ is enabled as follows,The background colour of the Data Area is medium-grey.The colour of data value is black.The colour of border is medium-grey.Packet transmission(8) Use the log file to confirm that DMI sent out packet [MMI_NEW_TRAIN_DATA (EVC-107)] with following variablesMMI_M_TRAINSET_ID = 1MMI_N_DATA_ELEMENTS = 1MMI_M_BUTTONS = 254(9) The data part of the echo text of train category is displayed according to [MMI_CURRENT_TRAIN_DATA  (EVC-6)] with the following variables,MMI_NID_DATA = 6 (Train Data Set Identifier)MMI_N_TEXT = Character lengthMMI_X_TEXT = Selected button(10)   Use the log file to confirm that DMI received packet EVC-6 with variable MMI_M_BUTTONS = 36 (BTN_YES_DATA_ENTRY_COMPLETE)
            Test Step Comment: (1) MMI_gen 8086 (partly: MMI_gen 4682 (partly: Train Set));(2) MMI_gen 8086 (partly: MMI_gen 4634 (partly: Train Set));(3) MMI_gen 8086 (partly: MMI_gen 4652 (partly: Train Set), MMI_gen 4684 (partly: accepted, Train Set));(4) MMI_gen 8086 (partly: MMI_gen 4684 (partly: No next input field, data entry process terminated));(5) MMI_gen 8086 (partly: MMI_gen 4700 (partly: Train Set));(6) MMI_gen 8086 (partly: MMI_gen 4681 (partly: Train Set), MMI_gen 4698, MMI_gen 4890);(7) MMI_gen 8086 (partly: MMI_gen 4909 (partly: Enabled), MMI_gen 4910 (partly: Enabled, MMI_gen 4211 (partly: colour))); MMI_gen 4374;(8) MMI_gen 9460 (partly: [Enter] EVC-107); MMI_gen 11385; (9) MMI_gen 9405 (partly:Train Set);(10) MMI_gen 9409;
            */


            /*
            Test Step 6
            Action: Press and hold ‘Yes’ button
            Expected Result: Verify the following information,(1)   The state of button is changed to ‘Pressed’, the border of button is removed.(2)   The sound ‘Click’ is played once
            Test Step Comment: (1) MMI_gen 8086 (partly: MMI_gen 4911 (partly: [Yes], Train category, MMI_gen 4381 (partly: change to state ‘Pressed’ as long as remain actuated)); MMI_gen 4375;(2) MMI_gen 8086 (partly: MMI_gen 4911 (partly: [Yes], Train category, MMI_gen 4381 (partly: sound ‘Click’)); MMI_gen 9512; MMI_gen 968;
            */
            // Call generic Check Results Method
            DmiExpectedResults
                .Verify_the_following_information_1_The_state_of_button_is_changed_to_Pressed_the_border_of_button_is_removed_2_The_sound_Click_is_played_once();


            /*
            Test Step 7
            Action: Slide out the ‘Yes’ button
            Expected Result: Verify the following information,(1)   The border of the button is shown (state ‘Enabled’) without a sound
            Test Step Comment: (1) MMI_gen 8086 (partly: MMI_gen 4911 (partly: [Yes], Train category, MMI_gen 4382 (partly: state ‘Enabled’ when slide out with force applied, no sound))); MMI_gen 4374;
            */
            // Call generic Action Method
            DmiActions.Slide_out_the_Yes_button();
            // Call generic Check Results Method
            DmiExpectedResults
                .Verify_the_following_information_1_The_border_of_the_button_is_shown_state_Enabled_without_a_sound();


            /*
            Test Step 8
            Action: Slide back into the ‘Yes’ button
            Expected Result: Verify the following information,(1)   The button is back to state ‘Pressed’ without a sound
            Test Step Comment: (1) MMI_gen 8086 (partly: MMI_gen 4911 (partly: [Yes], Train category, MMI_gen 4382 (partly: state ‘Pressed’ when slide back, no sound))); MMI_gen 4375;
            */
            // Call generic Action Method
            DmiActions.Slide_back_into_the_Yes_button();
            // Call generic Check Results Method
            DmiExpectedResults.Verify_the_following_information_1_The_button_is_back_to_state_Pressed_without_a_sound();


            /*
            Test Step 9
            Action: Release ‘Yes’ button
            Expected Result: Verify the following information,(1)   DMI displays Train data validation window.(2)   Use the log file to confirm that DMI sent out packet [MMI_NEW_TRAIN_DATA (EVC-107)] with following variablesMMI_N_DATA_ELEMENTS = 0MMI_M_TRAINSET_ID = 1MMI_M_BUTTONS = 36
            Test Step Comment: (1) MMI_gen 8086 (partly: MMI_gen 4911 (partly: [Yes], MMI_gen 4381 (partly: exite state ‘pressed’)));(2) MMI_gen 8086 (partly: MMI_gen 4679 (partly: Train category), MMI_gen 9336 (partly: circular), MMI_gen 4911 (partly: [Yes], MMI_gen 4384 (partly: ETCS-MMI’s function associated to the button)); MMI_gen 9460 (partly: [Yes], EVC-107); MMI_gen 5387 (partly: transmission);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(@"Release ‘Yes’ button");


            /*
            Test Step 10
            Action: Press ‘Close’ button
            Expected Result: DMI displays Train data window
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(@"Press ‘Close’ button");
            // Call generic Check Results Method
            DmiExpectedResults.DMI_displays_Train_data_window();


            /*
            Test Step 11
            Action: Confirm the current data without re-entry by pressing an input field
            Expected Result: The state of train type input field is changed to ‘Selected’
            */


            /*
            Test Step 12
            Action: Press and hold the Label area of ‘Train type’ input field
            Expected Result: Verify the following information,(1)   The state of ‘Train type’ input field is changed to ‘Pressed’, the border of button is removed.The state of ‘Train type’ input field remains ‘accepted’. (2)   The sound ‘Click’ is played once
            Test Step Comment: (1) MMI_gen 8086 (partly: MMI_gen 4686 (partly: Label area, Train type), MMI_gen 4381 (partly: change to state ‘Pressed’ as long as remain actuated))); MMI_gen 4392 (partly: [Enter], touch screen); MMI_gen 4375;(2) MMI_gen 8086 (partly: MMI_gen 4686 (partly: Label area, Train type), MMI_gen 4381 (partly: the sound for Up-Type button));
            */


            /*
            Test Step 13
            Action: Slide out the Label area of ‘Train type’ input field
            Expected Result: Verify the following information,(1)   The border of ‘Train type’ input field is shown (state ‘Enabled’) without a sound.The state of ‘Train type’ input field remains ‘accepted’
            Test Step Comment: (1) MMI_gen 8086 (partly: MMI_gen 4686 (partly: Label area, Train type), MMI_gen 4382 (partly: state ‘Enabled’ when slide out with force applied, no sound); MMI_gen 4374;
            */


            /*
            Test Step 14
            Action: Slide back into the Label area of ‘Train type’ input field
            Expected Result: Verify the following information,(1)   The state of ‘Train type’ input field is changed to ‘Pressed’, the border of button is removed.The state of ‘Train type’ input field remains ‘accepted’
            Test Step Comment: (1) MMI_gen 8086 (partly: MMI_gen 4686 (partly: Label area, Train type), MMI_gen 4382 (partly: state ‘Pressed’ when slide back, no sound)); MMI_gen 4375;
            */


            /*
            Test Step 15
            Action: Release the pressed area
            Expected Result: Verify the following information,(1)   The state of ‘Train type’ input field is changed to selected
            Test Step Comment: (1) MMI_gen 8086 (partly: MMI_gen 4686 (partly: Label area, Train type), MMI_gen 4381 (partly: exit state ‘Pressed’, execute function associated to the button)); MMI_gen 4374;
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(@"Release the pressed area");


            /*
            Test Step 16
            Action: Confirm the current data without re-entry by pressing an input field.Then, perform action step 12-15 for the Data area of each input field
            Expected Result: Verify the following information,(1)   The state of an input field is changed to ‘selected’ when release the pressed area at the Data area of input field
            Test Step Comment: (1) MMI_gen 8086 (partly: MMI_gen 4686 (partly: Data area, Up-Type button)); MMI_gen 9390 (partly: Fixed Train data window);
            */


            /*
            Test Step 17
            Action: Press ‘Close’ button
            Expected Result: Verify the following information, (1)   Use the log file to confirm that DMI sent out packet [MMI_DRIVER_REQUEST (EVC-101)] with variable MMI_M_REQUEST = 4 (Exit Train Data Entry).(2)   Use the log file to confirm that DMI sent out packet [MMI_ENABLE_REQUEST (EVC-30)] with variable MMI_NID_WINDOW = 254.(3)   The window is closed and the Main window is displayed
            Test Step Comment: (1) MMI_gen 9459 (partly: EVC-101);(2) MMI_gen 9459 (partly: MMI_gen 12071 (partly: EVC-30), MMI_gen 4355 (partly: [Close]));(3) MMI_gen 9459 (partly: MMI_gen 12071 (partly: closure), MMI_gen 4355 (partly: [Close])); MMI_gen 4392 (partly: returning to the parent window);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(@"Press ‘Close’ button");


            /*
            Test Step 18
            Action: Press ‘Train data’ button.Then, use the test script file 22_29_2_b.xml to send EVC-6 with,MMI_M_BUTTONS = 36
            Expected Result: DMI displays Train data window.Verify the following information,(1)     The state of ‘Yes’ button below text label ‘Train data Entry is complete?’ still disabled
            Test Step Comment: (1) MMI_gen 9409 (partly: other rule, MMI_gen 4909 (partly: otherwise));
            */
            // Call generic Check Results Method
            DmiExpectedResults
                .DMI_displays_Train_data_window_Verify_the_following_information_1_The_state_of_Yes_button_below_text_label_Train_data_Entry_is_complete_still_disabled();


            /*
            Test Step 19
            Action: Press ‘Close’ button
            Expected Result: DMI displays Main window
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(@"Press ‘Close’ button");
            // Call generic Check Results Method
            DmiExpectedResults.DMI_displays_Main_window();


            /*
            Test Step 20
            Action: Use the test script file 22_29_2_a.xml to send EVC-6 with,MMI_M_DATA_ENABLE = 1MMI_N_TRAINSETS = 10MMI_N_CAPTION_TRAINSET = 1 for every indexEach index of MMI_X_CAPTION_TRAINSET are set as following value,ABCDEFGHIJ
            Expected Result: Verify the following information,(1)   DMI displays Train data window with only 9 buttons of keypad that all labels correspond with received packet EVC-20.OR(1)   DMI does not display Train data window because of the value of MMI_N_TRAINSETS is invalid
            Test Step Comment: (1) MMI_gen 11382;
            */


            /*
            Test Step 21
            Action: End ot Test
            Expected Result: 
            */


            return GlobalTestResult;
        }
    }
}
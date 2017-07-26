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
    /// 27.28.1 ‘Remove VBC’ Data Entry Window
    /// TC-ID: 22.28.1
    /// 
    /// This test case verifies the display of the ‘Remove VBC’ window on DMI that shall comply with [ERA-ERTMS] standard and [MMI-ETCS-gen].
    /// 
    /// Tested Requirements:
    /// MMI_gen 8338; MMI_gen 9909; MMI_gen 9907; MMI_gen 9910 (partly: MMI_gen 12710, MMI_gen 4355); MMI_gen 9915; MMI_gen 9917; MMI_gen 8340; MMI_gen 9924 (partly: [enter], [yes], EVC_119); MMI_gen 8342; MMI_gen 8337 (partly: MMI_gen 4888, MMI_gen 4799 (partly: Close button, Window Title, Input fields)), MMI_gen 4392 (partly: [Close] NA11), MMI_gen 4355 (partly: Close button), MMI_gen 4891 (partly: Yes button, Area for [Window Title] Entry complete?), MMI_gen 4910 (partly: MMI_gen 4211 (partly: colour)), MMI_gen 4909, MMI_gen 4377 (partly: shown), MMI_gen 4908 (partly: extended), MMI_gen 4637 (partly: Main-areas D and F)), MMI_gen 4640, MMI_gen 4641, MMI_gen 9412, MMI_gen 4645, MMI_gen 4646 (partly: right aligned), MMI_gen 4647 (partly: left aligned),  MMI_gen 4648, MMI_gen 4720, MMI_gen 4651, MMI_gen 4683 (partly: selected), MMI_gen 5211 (partly: selected), MMI_gen 4912, MMI_gen 4678, MMI_gen 5003, MMI_gen 5190, MMI_gen 4697, MMI_gen 4701, MMI_gen 4702 (partly: right aligned), MMI_gen 4700, MMI_gen 4691 (partly: flash), MMI_gen 4689, MMI_gen 4690, MMI_gen 4913 (partly: MMI_gen 4384, MMI_gen 4386), MMI_gen 4679, MMI_gen 4689, MMI_gen 4642, MMI_gen 4692, MMI_gen 4694 (partly: MMI_gen 4647, MMI_gen 4247), MMI_gen 4682, MMI_gen 4634, MMI_gen 4652 , MMI_gen 4684, MMI_gen 4696, MMI_gen 4681, MMI_gen 4704 (partly: left aligned), MMI_gen 4911 (partly: MMI_gen 4381), MMI_gen 4686 (partly: MMI_gen 4381, MMI_gen 4386, MMI_gen 4382)); MMI_gen 4392 (partly: [Close] NA11, [Delete] NA21, [Enter], touch screen, returning to the parent window); MMI_gen 4355 (partly: Close button); MMMI_gen 4377 (partly: shown); MMI_gen 4355 (partly: input field); MMI_gen 4375; MMI_gen 9512; MMI_gen 968; MMI_gen 4374;  MMI_gen 5387; MMI_gen 4241; MMI_gen 4350; MMI_gen 4351; MMI_gen 4353; MMI_gen 9390 (partly: Remove VBC window); MMI_gen 4393 (partly: [Delete]);
    /// 
    /// Scenario:
    /// The Remove VBC window appearance is verified.The data entry functionality of the Remove VBC window is verified with the Down-type button in keypad.The Up-Type button on label part of an input field is verified.The Up-Type button on data part of an input field is verified.The functionality of ‘Close’ button is verified.
    /// 
    /// Used files:
    ///  N/A
    /// </summary>
    public class Remove_VBC_Data_Entry_Window : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // Test system is powered ON.Cabin is activated.Settings window is opened.The VBC code “65536” is stored on ETCS. (See the information in the “Data View” menu)

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
            Action: Press ‘Remove VBC’ button.
            Expected Result: DMI displays Remove VBC window.Verify the following information,Data Entry WindowThe window title is ‘Remove VBC’.The text label of the window title is right aligned.The following objects are displayed in Remove VBC window,  Enabled Close button (NA11)Window TitleInput fieldsThe following objects are additionally displayed in Remove VBC window,Yes buttonThe text label ‘Remove VBC Entry complete?’Yes button is displayed in Disabled state as follows,Text label is black Background colour is dark-greyThe border colour is medium-grey the same as the input field’s colour.The sensitive area of Yes button is extended from text label ‘Remove VBC Entry complete?’Input fieldsThe input fields are located on Main area D and F.Each input field is devided into a Label Area and a Data Area.The Label Area is give the topic of the input field.The Label Area text is displayed corresponding to the input field as ‘VBC code’.The Label Area is placed to the left of The Data Area.The text in the Label Area is aligned to the right.The value of data in the Data Area is aligned to the left.The text colour of the Label Area is grey and the background colour of the Label Area is dark-grey.There are only single input fields displayed in the window.The first input field is in state ‘Selected’ as follows,The background colour of the Data Area is medium-grey.KeyboardThe keyboard associated to selected input field ‘Remove VBC’ is Numeric keyboard.The keyboard contains enabled button for the number <1> to <9>, <Delete>(NA21) , <0> and disabled <Decimal_Separator>. NA21, Delete button.LayersThe level of layers of all areas in window are in Layer 0.Echo TextsThe Label Part of an echo texts is same as The Label area of an input fields.The echo texts are displayed in main area A, B, C and E with same order as their related input fields.The Label part of echo text is right aligned.The colour of texts in echo texts are grey.Entering CharactersThe cursor is flashed by changing from visible to not visible.The cursor is displayed as horizontal line below the value in the input field.Packet transmissionUse the log file to confirm that DMI sent out packet information [MMI_DRIVER_REQUEST (EVC-101)] with variable MMI_M_REQUEST = 24 (Start Remove VBC).Use the log file to confirm that DMI received packet information [MMI_REMOVE_VBC (EVC-19)] with MMI_N_VBC = 0.General property of windowThe Remove VBC window is presented with objects, text messages and buttons which is the one of several levels and allocated to areas of DMI. All objects, text messages and buttons are presented within the same layer.The Default window is not displayed and covered the current window.
            Test Step Comment: (1) MMI_gen 8338; MMI_gen 4355 (partly: Window title);(2) MMI_gen 8337 (partly: MMI_gen 4888);(3) MMI_gen 8337 (partly: MMI_gen 4799 (partly: Close button, Window Title, Input fields)); MMI_gen 4392 (partly: [Close] NA11); MMI_gen 4355 (partly: Close button); (4) MMI_gen 8337 (partly: MMI_gen 4891 (partly: Yes button, Area for [Window Title] Entry complete?));(5) MMI_gen 8337 (partly: MMI_gen 4910 (partly: Disabled, MMI_gen 4211 (partly: colour)), MMI_gen 4909 (partly: Disabled)); MMI_gen 4377 (partly: shown);(6) MMI_gen 8337 (partly: MMI_gen 4908 (partly: extended));(7) MMI_gen 8337 (partly: MMI_gen 4637 (partly: Main-areas D and F)); MMI_gen 4355 (partly: input fields);(8) MMI_gen 8337 (partly: MMI_gen 4640);(9) MMI_gen 8337 (partly: MMI_gen 4641);(10) MMI_gen 8337 (partly: MMI_gen 9412); MMI_gen 8340 (partly: label);(11) MMI_gen 8337 (partly: MMI_gen 4645);(12) MMI_gen 8337 (partly: MMI_gen 4646 (partly: right aligned));(13) MMI_gen 8337 (partly: MMI_gen 4647 (partly: left aligned));(14) MMI_gen 8337 (partly: MMI_gen 4648);(15) MMI_gen 8337 (partly: MMI_gen 4720); MMI_gen 8340 (partly: single input field);(16) MMI_gen 8337 (partly: MMI_gen 4651 (partly: background colour), MMI_gen 4683 (partly: selected), MMI_gen 5211 (partly: selected));(17) MMI_gen 8342; MMI_gen 8337 (partly: MMI_gen 4912, MMI_gen 4678); (18) MMI_gen 8337 (partly: MMI_gen 5003); MMI_gen 4392 (partly: [Delete] NA21);(19) MMI_gen 8337 (partly: MMI_gen 5190);(20) MMI_gen 8337 (partly: MMI_gen 4697); (21) MMI_gen 8337 (partly: MMI_gen 4701);(22) MMI_gen 8337 (partly: MMI_gen 4702 (partly: right aligned));(23) MMI_gen 8337 (partly: MMI_gen 4700 (partly: otherwise, grey)); MMI_gen 4241;(24) MMI_gen 8337 (partly: MMI_gen 4691 (partly: flash));(25) MMI_gen 8337 (partly: MMI_gen 4689, MMI_gen 4690);(26) MMI_gen 9907;(27) MMI_gen 9909;(28) MMI_gen 4350;(29) MMI_gen 4351;(30) MMI_gen 4353;
            */

            /*
            Test Step 2
            Action: Press and hold ‘0’ button.
            Expected Result: Verify the following information,The state of button is changed to ‘Pressed’ and immediately back to ‘Enabled’ state.The sound ‘Click’ is played once.The Input Field displays the value associated to the data key according to the pressings in state ‘Pressed’.The cursor is displayed as horizontal line below the value of the numeric-keyboard data key in the input field.The input field is used to enter the VBC code.The colour of data value is black.An echo text is composed of Label Part and Data Part.The Data part of echo text is left aligned.
            Test Step Comment: (1) MMI_gen 8337 (partly: MMI_gen 4913 (partly: MMI_gen 4384 (partly: Change to state ‘Pressed’ and immediately back to state ‘Enabled’)));   (2) MMI_gen 8337 (partly: MMI_gen 4913 (partly: MMI_gen 4384 (partly: sound ‘Click’))); MMI_gen 9512; MMI_gen 968;(3) MMI_gen 8337 (partly: MMI_gen 4679, MMI_gen 4913 (partly: MMI_gen 4384 (partly: ETCS-MMI’s function associated to the button)));(4) MMI_gen 8337 (partly: MMI_gen 4689, MMI_gen 4690);(5) MMI_gen 8340 (partly: entry);(6) MMI_gen 8337 (partly: MMI_gen 4651 (partly: data value);(7) MMI_gen 8337 (partly: MMI_gen 4696);(8) MMI_gen 8337 (partly: MMI_gen 4704 (partly: left aligned));
            */

            /*
            Test Step 3
            Action: Release the pressed button.
            Expected Result: Verify the following information,The state of released button is changed to enabled.
            Test Step Comment: (1) MMI_gen 8337 (partly: MMI_gen 4913 (partly: MMI_gen 4384 (partly: ETCS-MMI’s function associated to the button)));
            */

            /*
            Test Step 4
            Action: Perform action step 3-4 for the ‘1’ to ‘9’ buttons.Note: Press the ‘Del’ button to delete an information when entered data is out of input field range is acceptable.
            Expected Result: See the expected results of Step 3 – Step 4 and the following additional information,The pressed key is added in an input field immediately. The cursor is jumped to next position after entered the character immediately.
            Test Step Comment: (1) MMI_gen 8337 (partly: MMI_gen 4642);  (2) MMI_gen 8337 (partly: MMI_gen 4692);
            */

            /*
            Test Step 5
            Action: Press and hold ‘Del’ button.Note: Stopwatch is required.
            Expected Result: Verify the following information,While press and hold button less than 1.5 secSound ‘Click’ is played once.The state of button is changed to ‘Pressed’ and immediately back to ‘Enabled’ state.The last character is removed from an input field after pressing the button.While press and hold button over 1.5 secThe state ‘pressed’ and ‘released’ are switched repeatly while button is pressed and the characters are removed from an input field repeatly refer to pressed state.The sound ‘Click’ is played repeatly while button is pressed.
            Test Step Comment: (1) MMI_gen 8337 (partly: MMI_gen 4913 (partly: MMI_gen 4384 (partly: sound ‘Click’))); MMI_gen 9512; MMI_gen 968;(2) MMI_gen 8337 (partly: MMI_gen 4913 (partly: MMI_gen 4384 (partly: Change to state ‘Pressed’ and immediately back to state ‘Enabled’)));   (3) MMI_gen 8337 (partly: MMI_gen 4913 (partly: MMI_gen 4384 (partly: ETCS-MMI’s function associated to the button))); MMI_gen 4393 (partly: [Delete]);(4) MMI_gen 8337 (partly: MMI_gen 4913 (partly: MMI_gen 4386 (partly: visual of repeat function)));(5) MMI_gen 8337 (partly: MMI_gen 4913 (partly: MMI_gen 4386 (partly: audible of repeat function)));
            */

            /*
            Test Step 6
            Action: Release ‘Del’ button.
            Expected Result: Verify the following information, The character is stop removing.
            Test Step Comment: (1) MMI_gen 8337 (partly: MMI_gen 4913 (partly: MMI_gen 4384 (partly: ETCS-MMI’s function associated to the button)));
            */

            /*
            Test Step 7
            Action: Enter the data value with 5 characters
            Expected Result: Verify the following information,The 5 characters are added on an input field as one group. (e.g. ‘12345')
            Test Step Comment: (1) MMI_gen 8337 (partly: MMI_gen 4694 (partly: NEGATIVE, 6th character));
            */

            /*
            Test Step 8
            Action: Continue to enter the 6th character.
            Expected Result: Verify the following information,The fifth character is shown after a gap of fourth character, separated as 2 groups (e.g. 1234 56) 
            Test Step Comment: (1) MMI_gen 8337 (partly: MMI_gen 4694 (partly: MMI_gen 4246));
            */

            /*
            Test Step 9
            Action: Continue to enter the new value more than 8 characters.
            Expected Result: Verify the following information,The data value is separated into 2 lines. In each line is displayed only 8 characters.
            Test Step Comment: (1) MMI_gen 8337 (partly: MMI_gen 4694 (partly: MMI_gen 4247));
            */

            /*
            Test Step 10
            Action: Delete the old value and enter the value ‘65536’ for VBC code.Then, confirm an entered data by pressing an input field.
            Expected Result: Verify the following information,Input fieldsThe associated ‘Enter’ button is data field itself.An input field is used to allow the driver to enter data.The state of ‘VBC Code’ input field is changed to ‘accepted’ as follows,The background colour of the Data Area is dark-grey.The colour of data value is white.There is no input field selected.Echo TextsThe echo text of ‘VBC Code’ is changed to white colour.The value of echo text is changed refer to entered data.Data Entry windowThe state of ‘Yes’ button below text label ‘Train data Entry is complete?’ is enabled as follows,The background colour of the Data Area is medium-grey.The colour of data value is black.The colour of border is medium-grey.Packet transmissionUse the log file to confirm that DMI sent out packet [MMI_NEW_REMOVE_VBC (EVC-119)] with following variablesMMI_M_VBC_CODE (bit 16-23) = 65536MMI_M_BUTTONS = 254The data part of the echo text of train category is displayed according to [MMI_REMOVE_VBC (EVC-19)] with the following variables,MMI_N_TEXT = 5MMI_X_TEXT = “65536”
            Test Step Comment: (1) MMI_gen 8337 (partly: MMI_gen 4682);(2) MMI_gen 8337 (partly: MMI_gen 4634);(3) MMI_gen 8337 (partly: MMI_gen 4652 , MMI_gen 4684 (partly: accepted));(4) MMI_gen 8337 (partly: MMI_gen 4684 (partly: No next input field, data entry process terminated));(5) MMI_gen 8337 (partly: MMI_gen 4700);(6) MMI_gen 8337 (partly: MMI_gen 4681 , MMI_gen 4890, MMI_gen 4698);(7) MMI_gen 8337 (partly: MMI_gen 4909 (partly: Enabled), MMI_gen 4910 (partly: Enabled, MMI_gen 4211 (partly: colour))); MMI_gen 4374;(8) MMI_gen 9924 (partly: [enter], EVC-119);(9) MMI_gen 9915;
            */

            /*
            Test Step 11
            Action: Select and enter the value ‘65536’ for VBC code again.
            Expected Result: Verify the following information,The state of ‘Yes’ button below text label ‘Remove VBC entry is complete?’ is disabled. 
            Test Step Comment: (1) MMI_gen 8337 (partly: MMI_gen 4909 (partly: state selected and with recently entered key), MMI_gen 4680 (partly: value has been modified));
            */

            /*
            Test Step 12
            Action: Confirm an entered data.Then, apply the action step 2-3 for ‘Yes’ button
            Expected Result: See the expected results of Step 2 – Step 3 and the following points,DMI displays Remove VBC validation window.Use the log file to confirm that DMI sent out packet [MMI_NEW_REMOVE_VBC (EVC-119)] with following variablesMMI_M_VBC_CODE (bit 16-23) = 65536MMI_M_BUTTONS = 36
            Test Step Comment: (1) MMI_gen 8337 (partly: MMI_gen 4911 (partly:  MMI_gen 4381 (partly: change to state ‘Pressed’ as long as remain actuated))); MMI_gen 5387 (partly: closure);(2) MMI_gen 8337 (partly: MMI_gen 4911 (partly: MMI_gen 4381 (partly: exit state ‘Pressed’, execute function associated to the button))); MMI_gen 9924 (partly: [Yes], EVC-107); MMI_gen 9917; MMI_gen 5387 (partly: transmission);
            */

            /*
            Test Step 13
            Action: Press ‘Close’ button.
            Expected Result: DMI displays Remove VBC window.
            */

            /*
            Test Step 14
            Action: Enter the value ‘65536’ for VBC code.Then, confirm an entered data by pressing an input field.
            Expected Result: The state of ‘VBC Code’ input field is changed to ‘accepted’.
            */

            /*
            Test Step 15
            Action: Press and hold the Label area of ‘Remove VBC’ input field.
            Expected Result: Verify the following information,The state of ‘Remove VBC’ input field is changed to ‘Pressed’, the border of button is removed.The state of ‘Remove VBC’ input field remains ‘accecpted’. The sound ‘Click’ is played once.
            Test Step Comment: (1) MMI_gen 8337 (partly: MMI_gen 4686 (partly: Label area, Remove VBC), MMI_gen 4381 (partly: change to state ‘Pressed’ as long as remain actuated))); MMI_gen 4392 (partly: [Enter], touch screen); MMI_gen 4375;(2) MMI_gen 8337 (partly: MMI_gen 4686 (partly: Label area, Remove VBC), MMI_gen 4381 (partly: the sound for Up-Type button)); MMI_gen 9512; MMI_gen 968;
            */

            /*
            Test Step 16
            Action: Slide out the Label area of ‘Remove VBC’ input field.
            Expected Result: Verify the following information,The border of ‘Remove VBC’ input field is shown (state ‘Enabled’) without a sound.The state of ‘Remove VBC’ input field remains ‘accecpted’. 
            Test Step Comment: (1) MMI_gen 8337 (partly: MMI_gen 4686 (partly: Label area, Remove VBC), MMI_gen 4382 (partly: state ‘Enabled’ when slide out with force applied, no sound); MMI_gen 4374;
            */

            /*
            Test Step 17
            Action: Slide back into the Label area of ‘Remove VBC’ input field.
            Expected Result: Verify the following information,The state of ‘Remove VBC’ input field is changed to ‘Pressed’, the border of button is removed.The state of ‘Remove VBC’ input field remains ‘accecpted’. 
            Test Step Comment: (1) MMI_gen 8337 (partly: MMI_gen 4686 (partly: Label area, Remove VBC), MMI_gen 4382 (partly: state ‘Pressed’ when slide back, no sound); MMI_gen 4375;
            */

            /*
            Test Step 18
            Action: Release the pressed area.
            Expected Result: Verify the following information,The state of ‘Remove VBC’ input field is changed to selected.
            Test Step Comment: (1) MMI_gen 8337 (partly: MMI_gen 4686 (partly: Label area, Remove VBC), MMI_gen 4381 (partly: exit state ‘Pressed’, execute function associated to the button)); MMI_gen 4374;
            */

            /*
            Test Step 19
            Action: Perform action step 13-17 for the Data area of an input field.
            Expected Result: Verify the following information,The state of an input field is changed to ‘accepted’ when release the pressed area at the Data area of input field.
            Test Step Comment: (1) MMI_gen 8337 (partly: MMI_gen 4686 (partly: Data area, Up-Type button)); MMI_gen 9390 (partly: Remove VBC window);
            */

            /*
            Test Step 20
            Action: Press ‘Close’ button.
            Expected Result: Verify the following information, Use the log file to confirm that DMI sent out packet [MMI_DRIVER_REQUEST (EVC-101)] with variable MMI_M_REQUEST = 26 (Exit Remove VBC Entry).Use the log file to confirm that DMI sent out packet [MMI_ENABLE_REQUEST (EVC-30)] with variable MMI_NID_WINDOW = 254.The window is closed and the Settings window is displayed. 
            Test Step Comment: (1) MMI_gen 9910 (partly: EVC-101);(2) MMI_gen 9910 (partly: MMI_gen 12071 (partly: EVC-30), MMI_gen 4355 (partly: [Close]));(3) MMI_gen 9910 (partly: MMI_gen 12071 (partly: closure), MMI_gen 4355 (partly: [Close])); MMI_gen 4392 (partly: returning to the parent window);
            */

            /*
            Test Step 21
            Action: End of test.
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
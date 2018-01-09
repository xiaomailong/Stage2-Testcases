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
    /// 27.27.1 ‘Set VBC’ Data Entry Window
    /// TC-ID: 22.27.1
    /// 
    /// This test case verifies the display of the ‘Set VBC’ window on DMI that shall comply with [ERA-ERTMS] standard and [MMI-ETCS-gen].
    /// 
    /// Tested Requirements:
    /// MMI_gen 8327; MMI_gen 9881; MMI_gen 9885 (partly: MMI_gen 12071, MMI_gen 4355); MMI_gen 9900; MMI_gen 9902; MMI_gen 8329; MMI_gen 9923 (partly: [enter], [yes], EVC-118); MMI_gen 8331; MMI_gen 8326 (partly: MMI_gen 4888, MMI_gen 4799 (partly: Close button, Window Title, Input fields)), MMI_gen 4355 (partly: Close button), MMI_gen 4891 (partly: Yes button, Area for [Window Title] Entry complete?), MMI_gen 4910 (partly: MMI_gen 4211 (partly: colour)), MMI_gen 4909, MMI_gen 4377 (partly: shown), MMI_gen 4908 (partly: extended), MMI_gen 4637 (partly: Main-areas D and F)), MMI_gen 4640, MMI_gen 4641, MMI_gen 9412, MMI_gen 4645, MMI_gen 4646 (partly: right aligned), MMI_gen 4647 (partly: left aligned), MMI_gen 4648, MMI_gen 4720, MMI_gen 4651, MMI_gen 4683 ( partly: selected), MMI_gen 5211 (partly: selected), MMI_gen 4912, MMI_gen 4678, MMI_gen 5003, MMI_gen 5190, MMI_gen 4697, MMI_gen 4701, MMI_gen 4702 (partly: right aligned), MMI_gen 4700, MMI_gen 4691 (partly: flash), MMI_gen 4689, MMI_gen 4690, MMI_gen 4913 (partly: MMI_gen 4384, MMI_gen 4386), MMI_gen 4679, MMI_gen 4689, MMI_gen 4642, MMI_gen 4692, MMI_gen 4694 (partly: MMI_gen 4647, MMI_gen 4247), MMI_gen 4682, MMI_gen 4634, MMI_gen 4652 , MMI_gen 4684, MMI_gen 4696, MMI_gen 4681, MMI_gen 4704 (partly: left aligned), MMI_gen 4911 (partly: MMI_gen 4381), MMI_gen 4686 (partly: MMI_gen 4381, MMI_gen 4386, MMI_gen 4382)); MMI_gen 4392 (partly:[Close] NA11, [Delete] NA21, [Enter], touch screen, returning to the parent window); MMI_gen 4355 (partly: Close button); MMMI_gen 4377 (partly: shown); MMI_gen 4355 (partly: input field); MMI_gen 4375; MMI_gen 9512; MMI_gen 968; MMI_gen 4374; MMI_gen 5387; MMI_gen 9883; MMI_gen 4241; MMI_gen 9390 (partly: Set VBC window); MMI_gen 4393 (partly: [Delete]);
    /// 
    /// Scenario:
    /// The Set VBC window appearance is verified.The data entry functionality of the Set VBC window is verified with the Down-type button in keypad.The Up-Type button on label part of an input field is verified.The Up-Type button on data part of an input field is verified.The functionality of ‘Close’ button is verified.
    /// 
    /// Used files:
    ///  N/A
    /// </summary>
    public class TC_22_27_1_Set_VBC_Window : TestcaseBase
    {
        public override void PreExecution()
        {
            // Call the TestCaseBase PreExecution
            base.PreExecution();


            // Pre-conditions from TestSpec:
            // Test system is powered ON.
            // Cabin is activated.
            // Settings window is opened.
            DmiActions.Start_ATP();
            DmiActions.Activate_Cabin_1(this);
            DmiActions.Open_the_Settings_window(this);

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

            #region Test Step 1
            /*
            Test Step 1
            Action: Press ‘Set VBC’ button
            */

            DmiExpectedResults.Set_VBC_Button_pressed_and_released(this);
            DmiActions.Display_Set_VBC_Window(this);

            /*
            Expected Result: DMI displays Set VBC window.
            Verify the following information,
            */

            WaitForVerification("DATA ENTRY WINDOW:" + Environment.NewLine + Environment.NewLine +
                "The window title is ‘Set VBC’." + Environment.NewLine +
                "The text label of the window title is right aligned." + Environment.NewLine +
                "The following objects are displayed in Set VBC window:" + Environment.NewLine +
                " - Enabled Close button(NA11)" + Environment.NewLine +
                " - Window Title" + Environment.NewLine +
                " - Input fields" + Environment.NewLine + Environment.NewLine +
                "The following objects are additionally displayed in Set VBC window:" + Environment.NewLine +
                " - Yes button" + Environment.NewLine +
                " - The text label ‘Set VBC Entry complete ?’" + Environment.NewLine + Environment.NewLine +
                "Yes button is displayed in Disabled state as follows:" + Environment.NewLine +
                " - Text label is black" + Environment.NewLine +
                " - Background colour is dark - grey" + Environment.NewLine +
                " - The border colour is medium - grey the same as the input field’s colour." + Environment.NewLine + Environment.NewLine +
                "The sensitive area of Yes button is extended from text label ‘Set VBC entry complete ?’");

            WaitForVerification("INPUT FIELDS:" + Environment.NewLine + Environment.NewLine +
                "The input fields are located on Main area D and F." + Environment.NewLine +
                "Each input field is devided into a Label Area and a Data Area." + Environment.NewLine +
                "The Label Area is give the topic of the input field." + Environment.NewLine +
                "The Label Area text is displayed corresponding to the input field as ‘VBC code’." + Environment.NewLine +
                "The Label Area is placed to the left of The Data Area." + Environment.NewLine +
                "The text in the Label Area is aligned to the right." + Environment.NewLine +
                "The value of data in the Data Area is aligned to the left." + Environment.NewLine +
                "The text colour of the Label Area is grey and the background colour of the Label Area is dark-grey." + Environment.NewLine +
                "There are only single input fields displayed in the window." + Environment.NewLine +
                "The first input field is in state ‘Selected’ as follows," + Environment.NewLine + Environment.NewLine +
                " - The background colour of the Data Area is medium-grey.");

            WaitForVerification("KEYBOARD:" + Environment.NewLine + Environment.NewLine +
                "The keyboard associated to selected input field ‘Set VBC’ is Numeric keyboard." + Environment.NewLine +
                "The keyboard contains enabled button for the number <1> to <9>, <Delete>(NA21) , <0> and disabled <Decimal_Separator>." + Environment.NewLine +
                "NA21, Delete button.");

            WaitForVerification("LAYERS:" + Environment.NewLine + Environment.NewLine +
                "The level of layers of all areas in window are in Layer 0.");

            WaitForVerification("ECHO TEXTS:" + Environment.NewLine + Environment.NewLine +
                "The Label Part of an echo texts is same as the Label area of an input fields." + Environment.NewLine +
                "The echo texts are displayed in main area A, B, C and E with same order as their related input fields." + Environment.NewLine +
                "The Label part of echo text is right aligned." + Environment.NewLine +
                "The colour of texts in echo texts are grey.");

            WaitForVerification("ENTERING CHARACTERS:" + Environment.NewLine + Environment.NewLine +
                "The cursor is flashed by changing from visible to not visible." + Environment.NewLine +
                "The cursor is displayed as horizontal line below the value in the input field.");

            /*                                                   
            Test Step Comment: (1) MMI_gen 8327;
                               (2) MMI_gen 8326 (partly: MMI_gen 4888);
                               (3) MMI_gen 8326 (partly: MMI_gen 4799 (partly: Close button, Window Title, Input fields)); 
                                   MMI_gen 4392 (partly: [Close] NA11); 
                                   MMI_gen 4355 (partly: Close button); 
                               (4) MMI_gen 8326 (partly: MMI_gen 4891 (partly: Yes button, Area for [Window Title] Entry complete?));
                               (5) MMI_gen 8326 (partly: MMI_gen 4910 (partly: Disabled, MMI_gen 4211 (partly: colour)), 
                                   MMI_gen 4909 (partly: Disabled)); 
                                   MMI_gen 4377 (partly: shown);
                               (6) MMI_gen 8326 (partly: MMI_gen 4908 (partly: extended));
                               (7) MMI_gen 8326 (partly: MMI_gen 4637 (partly: Main-areas D and F)); 
                                   MMI_gen 4355 (partly: input fields);
                               (8) MMI_gen 8326 (partly: MMI_gen 4640);
                               (9) MMI_gen 8326 (partly: MMI_gen 4641);
                               (10) MMI_gen 8326 (partly: MMI_gen 9412); 
                                    MMI_gen 8329 (partly: label);
                               (11) MMI_gen 8326 (partly: MMI_gen 4645);
                               (12) MMI_gen 8326 (partly: MMI_gen 4646 (partly: right aligned));
                               (13) MMI_gen 8326 (partly: MMI_gen 4647 (partly: left aligned));
                               (14) MMI_gen 8326 (partly: MMI_gen 4648);
                               (15) MMI_gen 8326 (partly: MMI_gen 4720); 
                                    MMI_gen 8329 (partly: single input field);
                               (16) MMI_gen 8326 (partly: MMI_gen 4651 (partly: background colour), MMI_gen 4683 (partly: selected), MMI_gen 5211 (partly: selected));
                               (17) MMI_gen 8331; 
                                    MMI_gen 8326 (partly: MMI_gen 4912, MMI_gen 4678); 
                               (18) MMI_gen 8326 (partly: MMI_gen 5003); 
                                    MMI_gen 4392 (partly: [Delete] NA21);
                               (19) MMI_gen 8326 (partly: MMI_gen 5190);
                               (20) MMI_gen 8326 (partly: MMI_gen 4697); 
                               (21) MMI_gen 8326 (partly: MMI_gen 4701);
                               (22) MMI_gen 8326 (partly: MMI_gen 4702 (partly: right aligned));
                               (23) MMI_gen 8326 (partly: MMI_gen 4700 (partly: otherwise, grey)); 
                                    MMI_gen 4241;
                               (24) MMI_gen 8326 (partly: MMI_gen 4691 (partly: flash));
                               (25) MMI_gen 8326 (partly: MMI_gen 4689, MMI_gen 4690);
                               (26) MMI_gen 9881;
                               (27) MMI_gen 9883;
            */
            #endregion

            #region Test Step 2
            /*
            Test Step 2
            Action: Press and hold ‘0’ button
            */

            DmiActions.ShowInstruction(this, @"Press and hold ‘0’ button");

            /*
            Expected Result: Verify the following information,
            */

            WaitForVerification("Verify the following information," + Environment.NewLine + Environment.NewLine +
                "The state of button is changed to ‘Pressed’ and immediately back to ‘Enabled’ state." + Environment.NewLine +
                "The sound ‘Click’ is played once." + Environment.NewLine +
                "The Input Field displays the value associated to the data key according to the pressings in state ‘Pressed’." + Environment.NewLine +
                "The cursor is displayed as horizontal line below the value of the numeric-keyboard data key in the input field." + Environment.NewLine +
                "The input field is used to enter the VBC code." + Environment.NewLine +
                "The colour of data value is black." + Environment.NewLine +
                "An echo text is composed of Label Part and Data Part." + Environment.NewLine +
                "The Data part of echo text is left aligned");

            /*
            Test Step Comment: (1) MMI_gen 8326 (partly: MMI_gen 4913 (partly: MMI_gen 4384 (partly: Change to state ‘Pressed’ and immediately back to state ‘Enabled’)));   
                               (2) MMI_gen 8326 (partly: MMI_gen 4913 (partly: MMI_gen 4384 (partly: sound ‘Click’))); 
                                   MMI_gen 9512; 
                                   MMI_gen 968;
                               (3) MMI_gen 8326 (partly: MMI_gen 4679, MMI_gen 4913 (partly: MMI_gen 4384 (partly: ETCS-MMI’s function associated to the button)));
                               (4) MMI_gen 8326 (partly: MMI_gen 4689, MMI_gen 4690);
                               (5) MMI_gen 8329 (partly: entry);
                               (6) MMI_gen 8326 (partly: MMI_gen 4651 (partly: data value);
                               (7) MMI_gen 8326 (partly: MMI_gen 4696);
                               (8) MMI_gen 8326 (partly: MMI_gen 4704 (partly: left aligned));
            */
            #endregion

            #region Test Step 3
            /*
            Test Step 3
            Action: Release the pressed button
            */

            DmiActions.ShowInstruction(this, @"Release the pressed button");

            /*
            Expected Result: Verify the following information,
            The state of released button is changed to enabled            
            */

            WaitForVerification("The state of released button is changed to enabled.");

            /*
            Test Step Comment: (1) MMI_gen 8326(partly: MMI_gen 4913(partly: MMI_gen 4384(partly: ETCS - MMI’s function associated to the button)));
            */
            #endregion

            #region Test Step 4
            /*
            Test Step 4
            Action: Perform action step 3-4 for the ‘1’ to ‘9’ buttons.
            Note: Press the ‘Del’ button to delete an information when entered data is out of input field range is acceptable
            */

            DmiActions.ShowInstruction(this, @"Perform action step 3-4 for the ‘1’ to ‘9’ buttons.");

            /*
            Expected Result: See the expected results of Step 3 – Step 4 and the following additional information,
            The pressed key is added in an input field immediately. 
            The cursor is jumped to next position after entered the character immediately
            */

            WaitForVerification("See the expected results of Step 3 – Step 4 and the following additional information:" + Environment.NewLine + Environment.NewLine +
                "The pressed key is added in an input field immediately. " + Environment.NewLine +
                "The cursor is jumped to next position after entered the character immediately.");

            /*
            Test Step Comment: (1) MMI_gen 8326 (partly: MMI_gen 4642);  
                               (2) MMI_gen 8326 (partly: MMI_gen 4692);
            */
            #endregion

            #region Test Step 5
            /*
            Test Step 5
            Action: Press and hold ‘Del’ button.
            Note: Stopwatch is required
            */

            DmiActions.ShowInstruction(this, @"Press and hold ‘Del’ button.");

            /*
            Expected Result: Verify the following information,
            */

            WaitForVerification("Verify the following information," + Environment.NewLine + Environment.NewLine +
                "While press and hold button less than 1.5 secSound ‘Click’ is played once." + Environment.NewLine +
                "The state of button is changed to ‘Pressed’ and immediately back to ‘Enabled’ state." + Environment.NewLine +
                "The last character is removed from an input field after pressing the button." + Environment.NewLine +
                "While press and hold button over 1.5 sec" + Environment.NewLine +
                "The state ‘pressed’ and ‘released’ are switched repeatly while button is pressed and the characters are removed from an input field repeatly refer to pressed state." + Environment.NewLine +
                "The sound ‘Click’ is played repeatly while button is pressed");

            /*
            Test Step Comment: (1) MMI_gen 8326 (partly: MMI_gen 4913 (partly: MMI_gen 4384 (partly: sound ‘Click’))); 
                                   MMI_gen 9512; 
                                   MMI_gen 968;
                               (2) MMI_gen 8326 (partly: MMI_gen 4913 (partly: MMI_gen 4384 (partly: Change to state ‘Pressed’ and immediately back to state ‘Enabled’)));   
                               (3) MMI_gen 8326 (partly: MMI_gen 4913 (partly: MMI_gen 4384 (partly: ETCS-MMI’s function associated to the button))); 
                                   MMI_gen 4393 (partly: [Delete]);
                               (4) MMI_gen 8326 (partly: MMI_gen 4913 (partly: MMI_gen 4386 (partly: visual of repeat function)));
                               (5) MMI_gen 8326 (partly: MMI_gen 4913 (partly: MMI_gen 4386 (partly: audible of repeat function)));
            */
            #endregion

            #region Test Step 6
            /*
            Test Step 6
            Action: Release ‘Del’ button
            Expected Result: Verify the following information, The character is stop removing
            Test Step Comment: (1) MMI_gen 8326 (partly: MMI_gen 4913 (partly: MMI_gen 4384 (partly: ETCS-MMI’s function associated to the button)));
            */

            DmiActions.ShowInstruction(this, @"Release the pressed button");
            WaitForVerification("The character is stop removing.");

            #endregion

            #region Test Step 7
            /*
            Test Step 7
            Action: Enter the data value with 5 characters
            Expected Result: Verify the following information,
            The 5 characters are added on an input field as one group. (e.g. ‘12345')
            Test Step Comment: (1) MMI_gen 8326 (partly: MMI_gen 4694 (partly: NEGATIVE, 6th character));
            */

            DmiActions.ShowInstruction(this, @"Enter the data value with 5 characters");
            WaitForVerification("The 5 characters are added on an input field as one group. (e.g. ‘12345')");

            #endregion

            #region Test Step 8
            /*
            Test Step 8
            Action: Continue to enter the 6th character
            Expected Result: Verify the following information,
            The fifth character is shown after a gap of fourth character, separated as 2 groups (e.g. 1234 56)
            Test Step Comment: (1) MMI_gen 8326 (partly: MMI_gen 4694 (partly: MMI_gen 4246));
            */

            DmiActions.ShowInstruction(this, @"Continue to enter the 6th character");
            WaitForVerification("The fifth character is shown after a gap of fourth character, separated as 2 groups (e.g. 1234 56)");

            #endregion

            #region Test Step 9
            /*
            Test Step 9
            Action: Continue to enter the new value more than 8 characters
            Expected Result: Verify the following information,
            The data value is separated into 2 lines. 
            In each line is displayed only 8 characters
            Test Step Comment: (1) MMI_gen 8326 (partly: MMI_gen 4694 (partly: MMI_gen 4247));
            */

            DmiActions.ShowInstruction(this, @"Continue to enter the new value more than 8 characters");
            WaitForVerification("Verify the following information:" + Environment.NewLine + Environment.NewLine +
                "The data value is separated into 2 lines. " + Environment.NewLine +
                "In each line is displayed only 8 characters.");
            #endregion

            #region Test Step 10
            /*
            Test Step 10
            Action: Delete the old value and enter the value ‘65536’ for VBC code.
            Then, confirm an entered data by pressing an input field
            Expected Result: Verify the following information,           

            Packet transmission

            Use the log file to confirm that DMI sent out packet [MMI_NEW_SET_VBC (EVC-118)] with following variables
            - MMI_M_VBC_CODE (bit 16-23) = 65536
            - MMI_M_BUTTONS = 254
            The data part of the echo text of train category is displayed according to [MMI_SET_VBC (EVC-18)] with the following variables,
            - MMI_N_TEXT = 5
            - MMI_X_TEXT = “65536”

            Test Step Comment: (1) MMI_gen 8326 (partly: MMI_gen 4682);
                               (2) MMI_gen 8326 (partly: MMI_gen 4634);
                               (3) MMI_gen 8326 (partly: MMI_gen 4652, MMI_gen 4684 (partly: accepted));
                               (4) MMI_gen 8326 (partly: MMI_gen 4684 (partly: No next input field, data entry process terminated));
                               (5) MMI_gen 8326 (partly: MMI_gen 4700);
                               (6) MMI_gen 8326 (partly: MMI_gen 4681, MMI_gen 4890, MMI_gen 4698);
                               (7) MMI_gen 8326 (partly: MMI_gen 4909 (partly:Enabled), MMI_gen 4910 (partly: Enabled, MMI_gen 4211 (partly: colour))); 
                                   MMI_gen 4374;
                               (8) MMI_gen 9923 (partly: [enter], EVC-118);
                               (9) MMI_gen 9900;
            */

            DmiActions.ShowInstruction(this, @"Delete the old value and enter the value ‘65536’ for VBC code.");
            DmiExpectedResults.VBC_code_entered(this);

            DmiActions.Set_VBC_Code(this, "65536");                     

            WaitForVerification("INPUT FIELDS:" + Environment.NewLine + Environment.NewLine +
               "The associated ‘Enter’ button is data field itself." + Environment.NewLine +
               "An input field is used to allow the driver to enter data." + Environment.NewLine +
               "The state of ‘VBC Code’ input field is changed to ‘accepted’ as follows:" + Environment.NewLine +
               " - The background colour of the Data Area is dark - grey." + Environment.NewLine +
               " - The colour of data value is white." + Environment.NewLine +
               "There is no input field selected.");            
           
            WaitForVerification("ECHO TEXTS:" + Environment.NewLine + Environment.NewLine +
                "The echo text of ‘VBC Code’ is changed to white colour." + Environment.NewLine +
                "The value of echo text is changed refer to entered data.");

           WaitForVerification("DATA ENTRY WINDOW:" + Environment.NewLine + Environment.NewLine +
               "The state of ‘Yes’ button below text label ‘Train data Entry is complete?’ is enabled as follows," + Environment.NewLine +
               " - The background colour of the Data Area is medium - grey." + Environment.NewLine +
               " - The colour of data value is black." + Environment.NewLine +
               " - The colour of border is medium - grey.");

            #endregion

            /*
            Test Step 11
            Action: Select and enter the value ‘65536’ for VBC code again
            Expected Result: Verify the following information,
            The state of ‘Yes’ button below text label ‘Set VBC entry is complete?’ is disabled
            Test Step Comment: (1) MMI_gen 8326 (partly: MMI_gen 4909 (partly: state selected and with recently entered key), MMI_gen 4680 (partly: value has been modified));
            */



            /*
            Test Step 12
            Action: Confirm an entered data.
            Then, apply the action step 2-3 for ‘Yes’ button
            Expected Result: See the expected results of Step 2 – Step 3 and the following points,
            DMI displays Set VBC validation window.
            Use the log file to confirm that DMI sent out packet [MMI_NEW_SET_VBC (EVC-118)] with following variables
            MMI_M_VBC_CODE (bit 16-23) = 65536
            MMI_M_BUTTONS = 36
            Test Step Comment: (1) MMI_gen 8326 (partly: MMI_gen 4911 (partly:  MMI_gen 4381 (partly: change to state ‘Pressed’ as long as remain actuated))); 
                                   MMI_gen 5387 (partly: closure);
                               (2) MMI_gen 8326 (partly: MMI_gen 4911 (partly: MMI_gen 4381 (partly: exit state ‘Pressed’, execute function associated to the button))); 
                                   MMI_gen 9923 (partly: [Yes], EVC-107); 
                                   MMI_gen 9902; 
                                   MMI_gen 5387 (partly: transmission);
            */
            DmiActions.Confirm_an_entered_data_Then_apply_the_action_step_2_3_for_Yes_button(this);


            /*
            Test Step 13
            Action: Press ‘Close’ button
            Expected Result: DMI displays Set VBC window
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press ‘Close’ button");
            // Call generic Check Results Method
            DmiExpectedResults.DMI_displays_Set_VBC_window(this);


            /*
            Test Step 14
            Action: Enter the value ‘65536’ for VBC code.Then, confirm an entered data by pressing an input field
            Expected Result: The state of ‘VBC Code’ input field is changed to ‘accepted’
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this,
                @"Enter the value ‘65536’ for VBC code.Then, confirm an entered data by pressing an input field");
            // Call generic Check Results Method
            DmiExpectedResults.The_state_of_VBC_Code_input_field_is_changed_to_accepted(this);


            /*
            Test Step 15
            Action: Press and hold the Label area of ‘Set VBC’ input field
            Expected Result: Verify the following information,The state of ‘Set VBC’ input field is changed to ‘Pressed’, the border of button is removed.The state of ‘Set VBC’ input field remains ‘accecpted’. The sound ‘Click’ is played once
            Test Step Comment: (1) MMI_gen 8326 (partly: MMI_gen 4686 (partly: Label area, Set VBC), MMI_gen 4381 (partly: change to state ‘Pressed’ as long as remain actuated))); MMI_gen 4392 (partly: [Enter], touch screen); MMI_gen 4375;(2) MMI_gen 8326 (partly: MMI_gen 4686 (partly: Label area, Set VBC), MMI_gen 4381 (partly: the sound for Up-Type button)); MMI_gen 9512; MMI_gen 968;
            */


            /*
            Test Step 16
            Action: Slide out the Label area of ‘Set VBC’ input field
            Expected Result: Verify the following information,The border of ‘Set VBC’ input field is shown (state ‘Enabled’) without a sound.The state of ‘Set VBC’ input field remains ‘accecpted’
            Test Step Comment: (1) MMI_gen 8326 (partly: MMI_gen 4686 (partly: Label area, Set VBC), MMI_gen 4382 (partly: state ‘Enabled’ when slide out with force applied, no sound); MMI_gen 4374;
            */


            /*
            Test Step 17
            Action: Slide back into the Label area of ‘Set VBC’ input field
            Expected Result: Verify the following information,The state of ‘Set VBC’ input field is changed to ‘Pressed’, the border of button is removed.The state of ‘Set VBC’ input field remains ‘accecpted’
            Test Step Comment: (1) MMI_gen 8326 (partly: MMI_gen 4686 (partly: Label area, Set VBC), MMI_gen 4382 (partly: state ‘Pressed’ when slide back, no sound)); MMI_gen 4375;
            */


            /*
            Test Step 18
            Action: Release the pressed area
            Expected Result: Verify the following information,The state of ‘Set VBC’ input field is changed to selected
            Test Step Comment: (1) MMI_gen 8326 (partly: MMI_gen 4686 (partly: Label area, Set VBC), MMI_gen 4381 (partly: exit state ‘Pressed’, execute function associated to the button)); MMI_gen 4374;
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Release the pressed area");


            /*
            Test Step 19
            Action: Perform action step 13-17 for the Data area of an input field
            Expected Result: Verify the following information,The state of an input field is changed to ‘accepted’ when release the pressed area at the Data area of input field
            Test Step Comment: (1) MMI_gen 8326 (partly: MMI_gen 4686 (partly: Data area)); MMI_gen 9390 (partly: Set VBC window);
            */
            // Call generic Action Method
            DmiActions.Perform_action_step_13_17_for_the_Data_area_of_an_input_field(this);
            // Call generic Check Results Method
            DmiExpectedResults
                .Verify_the_following_information_The_state_of_an_input_field_is_changed_to_accepted_when_release_the_pressed_area_at_the_Data_area_of_input_field(this);


            /*
            Test Step 20
            Action: Press ‘Close’ button
            Expected Result: Verify the following information, Use the log file to confirm that DMI sent out packet [MMI_DRIVER_REQUEST (EVC-101)] with variable MMI_M_REQUEST = 25 (Exit Set VBC Entry).Use the log file to confirm that DMI sent out packet [MMI_ENABLE_REQUEST (EVC-30)] with variable MMI_NID_WINDOW = 254.The window is closed and the Settings window is displayed
            Test Step Comment: (1) MMI_gen 9885 (partly: EVC-101);(2) MMI_gen 9885 (partly: MMI_gen 12071 (partly: EVC-30), MMI_gen 4355 (partly: [Close]));(3) MMI_gen 9885 (partly: MMI_gen 12071 (partly: closure), MMI_gen 4355 (partly: [Close])); MMI_gen 4392 (partly: returning to the parent window);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press ‘Close’ button");


            /*
            Test Step 21
            Action: End of test
            Expected Result: 
            */


            return GlobalTestResult;
        }
    }
}
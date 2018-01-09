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
using Testcase.Telegrams.EVCtoDMI;
using Testcase.Telegrams.DMItoEVC;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 27.6.3.1 Wheel diameter window: General apearance
    /// TC-ID: 22.6.3.1
    /// 
    /// This test case verifies the display of the Wheel diameter window on DMI that shall comply with [ERA-ERTMS] standard and [MMI-ETCS-gen].
    /// 
    /// Tested Requirements:
    /// MMI_gen 11734; MMI_gen 11694; MMI_gen 11697; MMI_gen 11751; MMI_gen 11696; MMI_gen 11698; MMI_gen 11755; MMI_gen 11753; MMI_gen 11699; MMI_gen 11700; MMI_gen 11757; MMI_gen 11758; MMI_gen 11759; MMI_gen 11716 (partly: MMI_gen 4888, MMI_gen 4799 (partly: Close button, Window Title, Input fields), MMI_gen 4891 (partly: Yes button, Area for [Window Title] Entry complete?), MMI_gen 4910, MMI_gen 4211 (partly: colour), MMI_gen 4909, MMI_gen 4908 (partly: extended), MMI_gen 4637 (partly: Main-areas D and F), MMI_gen 4640, MMI_gen 4641, MMI_gen 9412, MMI_gen 4645, MMI_gen 4646 (partly: right aligned), MMI_gen 4647 (partly: left aligned), MMI_gen 4648, MMI_gen 4651, MMI_gen 4683, MMI_gen 5211, MMI_gen 4649, MMI_gen 4912, MMI_gen 5003, MMI_gen 5190, MMI_gen 4696, MMI_gen 4697, MMI_gen 4701, MMI_gen 4702 (partly: right aligned), MMI_gen 4700, MMI_gen 4691 (partly: flash), MMI_gen 4689, MMI_gen 4690, MMI_gen 4913 (partly: MMI_gen 4384, MMI_gen 4386), MMI_gen 4642, MMI_gen 4682, MMI_gen 4634, MMI_gen 4652, MMI_gen 4684, MMI_gen 4890, MMI_gen 4698, MMI_gen 4681, MMI_gen 4692, MMI_gen 4680, MMI_gen 4685, MMI_gen 4911 (partly: MMI_gen 4381, MMI_gen 4382), MMI_gen 4686, MMI_gen 4720); MMI_gen 4392 (partly: [Close] NA11, [Delete] NA21, returning to the parent window); MMI_gen 4355 (partly: Buttons, Close button, input fields); MMI_gen 4377 (partly: shown); MMI_gen 4393 (partly: [Delete]); MMI_gen 4374; MMI_gen 4375; MMI_gen 4241; MMI_gen 4350; MMI_gen 4351; MMI_gen 4353; MMI_gen 9390 (partly: Wheel diameter window);
    /// 
    /// Scenario:
    /// Wheel diameter window, the window appearance is verified.The data entry functionality of the Wheel diameter window is verified with the Down-type button in keypad.The data revalidation functionality of the Wheel diameter window is verified.The Up-Type button on ‘Yes’ button is verified.The Up-Type button on each label part of an input field is verified.The Up-Type button on each data part of an input field is verified.The functionality of ‘Close’ button is verified.
    /// 
    /// Used files:
    /// N/A
    /// </summary>
    public class TC_ID_22_6_3_1_Wheel_diameter_window : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // The Maintenance password in tag name ‘PASS_CODE_MTN’ of the configuration file is set correctly refer to MMI_gen 11722.  Test system is power on.Cabin is activated.Settings window is opened from Driver ID window.Maintenance window is opened.

            // Call the TestCaseBase PreExecution
            base.PreExecution();

            DmiActions.Start_ATP();
            DmiActions.Activate_Cabin_1(this);
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
            Action: Press ‘Wheel diameter’ button
            Expected Result: DMI displays Wheel diameter window.Verify the following information,Data Entry WindowThe window title is ‘Wheel diameter’.The text label of the window title is right aligned.The following objects are displayed in Wheel diameter window,  Enabled Close button (NA11)Window TitleInput fieldsThe following objects are additionally displayed in Wheel diameter window,Yes buttonThe text label ‘Wheel diameter entry complete?’Yes button is displayed in Disabled state as follows,Text label is black Background colour is dark-greyThe border colour is medium-grey the same as the input field’s colour.The sensitive area of Yes button is extended from text label ‘Wheel diameter entry complete?’Input fieldsThe input fields are located on Main area D and F.Each input field is devided into a Label Area and a Data Area.The Label Area is give the topic of the input field.The Label Area text is displayed corresponding to the input field i.e. Wheel diameter 1 (mm), Wheel diameter 2 (mm), Accuracy (mm). The Label Area is placed to the left of The Data Area.The text in the Label Area is aligned to the right.The value of data in the Data Area is aligned to the left.The text colour of the Label Area is grey and the background colour of the Label Area is dark-grey.There are only 3 input fields displayed in the first page of window.The first input field is in state ‘Selected’ as follows,The background colour of the Data Area is medium-grey.The colour of data value is black.All other input fields are in state ‘Not selected’ as follows,The background colour of the Data Area is dark-grey.The colour of data value is grey.KeyboardThe keyboard associated to selected input field ‘Wheel diameter 1’ is Numeric keyboard.The keyboard contains enabled button for the number <1> to <9>, <Delete>(NA21) , <0> and disabled <Decimal_Separator>. NA21, Delete button.LayersThe level of layers of all areas in window are in Layer 0.Echo TextsAn echo text is composed of Label Part and Data Part.The Label Part of an echo texts is same as The Label area of an input fields.The echo texts are displayed in main area A, B, C and E with same order as their related input fields.The Label part of echo text is right aligned.The Data part of echo text is left aligned.The colour of texts in echo texts are grey.Entering CharactersThe cursor is flashed by changing from visible to not visible.The cursor is displayed as horizontal line below the value in the input field.Packet transmissionUse the log file to confirm that DMI received packet information [MMI_CURRENT_MAINTENANCE_DATA (EVC-40)] with variable MMI_Q_MD_DATASET = 0.The data part of input field and echo text are displayed correspond with the variables in received packet EVC-40 as follows,MMI_M_SDU_WHEEL_SIZE_1 = Wheel diameter 1MMI_M_SDU_WHEEL_SIZE_2 = Wheel diameter 2MMI_M_WHEEL_DIZE_ERR = AccuracyGeneral property of windowThe Wheel diameter window is presented with objects and buttons which is the one of several levels and allocated to areas of DMI. All objects, text messages and buttons are presented within the same layer.The Default window is not displayed and covered the current window
            Test Step Comment: (1) MMI_gen 11694;(2) MMI_gen 11716 (partly: MMI_gen 4888);(3) MMI_gen 11716 (partly: MMI_gen 4799 (partly: Close button, Window Title, Input fields)); MMI_gen 4392 (partly: [Close] NA11); MMI_gen 4355 (partly: Buttons, Close button); (4) MMI_gen 11716 (partly: MMI_gen 4891 (partly: Yes button, Area for [Window Title] Entry complete?));(5) MMI_gen 11716 (partly: MMI_gen 4910 (partly: Disabled, MMI_gen 4211 (partly: colour)), MMI_gen 4909 (partly: Disabled)); MMI_gen 4377 (partly: shown);(6) MMI_gen 11716 (partly: MMI_gen 4908 (partly: extended));(7) MMI_gen 11716 (partly: MMI_gen 4637 (partly: Main-areas D and F)); MMI_gen 4355 (partly: input fields);(8) MMI_gen 11716 (partly: MMI_gen 4640);(9) MMI_gen 11716 (partly: MMI_gen 4641);(10) MMI_gen 11716 (partly: MMI_gen 9412); MMI_gen 11697 (partly: label); MMI_gen 11696 (partly: label); MMI_gen 11698 (partly: label);(11) MMI_gen 11716 (partly: MMI_gen 4645);(12) MMI_gen 11716 (partly: MMI_gen 4646 (partly: right aligned));(13) MMI_gen 11716 (partly: MMI_gen 4647 (partly: left aligned));(14) MMI_gen 11716 (partly: MMI_gen 4648);(15) MMI_gen 11716 (partly: MMI_gen 4720);(16) MMI_gen 11716 (partly: MMI_gen 4651 (partly: Wheel diameter 1), MMI_gen 4683 (partly: selected), MMI_gen 5211 (partly: selected));(17) MMI_gen 11716 (partly: MMI_gen 4649 (partly: selected ‘Wheel diameter 1’), MMI_gen 4651 (partly: Wheel diameter 2), MMI_gen 4683 (partly: not selected), MMI_gen 5211 (partly: not selected));(18) MMI_gen 11700 (partly: Wheel diameter 1); MMI_gen 11716 (partly: MMI_gen 4912 (partly: Wheel diameter 1), MMI_gen 4678 (partly: Wheel diameter 1)); (19) MMI_gen 11716 (partly: MMI_gen 5003); MMI_gen 4392 (partly: [Delete] NA21);(20) MMI_gen 11716 (partly: MMI_gen 5190);(21) MMI_gen 11716 (partly: MMI_gen 4696);(22) MMI_gen 11716 (partly: MMI_gen 4697); (23) MMI_gen 11716 (partly: MMI_gen 4701);(24) MMI_gen 11716 (partly: MMI_gen 4702 (partly: right aligned));(25) MMI_gen 11716 (partly: MMI_gen 4704 (partly: left aligned));(26) MMI_gen 11716 (partly: MMI_gen 4700 (partly: otherwise, grey)); MMI_gen 4241;(27) MMI_gen 11716 (partly: MMI_gen 4691 (partly: flash, Wheel diameter 1));(28) MMI_gen 11716 (partly: MMI_gen 4689, MMI_gen 4690);(29) MMI_gen 11734;(30) MMI_gen 11755; MMI_gen 11753; MMI_gen 11751; MMI_gen 11699;(31) MMI_gen 4350;(32) MMI_gen 4351;(33) MMI_gen 4353;
            */
            // Call generic Action Method

            // Spec says settings window is opened from driver ID window            
            EVC14_MMICurrentDriverID.MMI_Q_ADD_ENABLE = EVC14_MMICurrentDriverID.MMI_Q_ADD_ENABLE_BUTTONS.Settings;
            EVC14_MMICurrentDriverID.MMI_Q_CLOSE_ENABLE = Variables.MMI_Q_CLOSE_ENABLE.Enabled;
            EVC14_MMICurrentDriverID.Send();

            DmiActions.ShowInstruction(this, @"Press the ‘Settings’ button"); 

            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Settings;        // Settings window
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.EnableWheelDiameter;
            EVC30_MMIRequestEnable.Send();

            DmiActions.ShowInstruction(this, @"Press the ‘Maintenance’ button, enter the password from the PASS_CODE_MTN tag in the configuration and confirm the password" + Environment.NewLine +
                                             @"Press the ‘Wheel diameter’ button");

            EVC40_MMICurrentMaintenanceData.MMI_Q_MD_DATASET = Variables.MMI_Q_MD_DATASET.WheelDiameter;
            EVC40_MMICurrentMaintenanceData.Send();
            
            WaitForVerification("Check the following (* indicates sub-areas drawn as one area):" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Wheel diameter window." + Environment.NewLine +
                                "2. The window title is ‘Wheel diameter’, right-aligned." + Environment.NewLine +
                                "3. The Wheel diameter window displays the title, an enabled ‘Close’ button, symbol NA11, and data input fields." + Environment.NewLine +
                                "4. The Wheel diameter window also displays a ‘Yes’ button and the label ‘Wheel diameter entry complete?’." + Environment.NewLine +
                                "5. The ‘Yes’ button is displayed disabled in black on a Dark-grey background with a Medium-grey border." + Environment.NewLine +
                                "6. The data input fields are in Medium-grey." + Environment.NewLine +
                                "7. The touch-sensitive area of the ‘Yes’ button extends from the ‘Wheel diameter entry complete?’ label." + Environment.NewLine +
                                "8. The data input fields are displayed in areas D and F." + Environment.NewLine +
                                "9. Data input fields have a label area and a data part area." + Environment.NewLine +
                                "10. Data input field ‘Wheel diameter 1 (mm)’ corresponds to the Wheel diameter 1 value, ‘Wheel diameter 2 (mm)’ to the Wheel Diameter 2 value and ‘Accuracy’ to the accuracy value." + Environment.NewLine +
                                "11. The label areas are to the left of their data parts." + Environment.NewLine +
                                "12. The label area text is right-aligned in grey on a Dark-grey background and the data part text is left-aligned." + Environment.NewLine +
                                "13. The first page of the Wheel diameter window contains three data input fields." + Environment.NewLine +
                                "14. The first data input field is pre-selected with the data part in black on a Medium-grey background." + Environment.NewLine +
                                "15. All other data input fields are not selected with the data parts in grey on a Dark-grey background." + Environment.NewLine +
                                "16. A numeric keypad is displayed when data input field ‘Wheel diameter 1 (mm)’ is selected" + Environment.NewLine +
                                "17. The numeric keypad displays enabled buttons for numbers <1> to <9>, <Delete> (symbol NA21), <0> and the <Decimal_Separator> (disabled)." + Environment.NewLine +
                                "18, All the areas of the window are displayed in Layer 0." + Environment.NewLine +
                                "19. Echo texts of the data input fields set are displayed in areas A, B, C, D and E in the same order as the related data input fields." + Environment.NewLine +
                                "20. Echo texts have a label part with right-aligned text and a data part with left-aligned text, both in grey." + Environment.NewLine +
                                "21. Echo text labels are the same as the related data input field label areas." + Environment.NewLine +
                                "22. A flashing (visible/invisible) underscore character is displayed at the cursor position in the data input field.");

            /*
            Test Step 2
            Action: Press and hold ‘0’ button
            Expected Result: Verify the following information,The state of button is changed to ‘Pressed’ and immediately back to ‘Enabled’ state.The sound ‘Click’ is played once.The Input Field displays the value associated to the data key according to the pressings in state ‘Pressed’.The cursor is displayed as horizontal line below the value of the numeric-keyboard data key in the input field.The input field is used to enter the Wheel diameter 1 value
            Test Step Comment: (1) MMI_gen 11716 (partly: MMI_gen 4913 (partly: Wheel diameter 1), MMI_gen 4384 (partly: Change to state ‘Pressed’ and immediately back to state ‘Enabled’));   (2) MMI_gen 11716 (partly: MMI_gen 4913 (partly: Wheel diameter 1), MMI_gen 4384 (partly: sound ‘Click’)); MMI_gen 9512; MMI_gen 968;(3) MMI_gen 11716 (partly: MMI_gen 4679 (partly: Wheel diameter 1), MMI_gen 4913 (partly: Wheel diameter 1), MMI_gen 4384 (partly: ETCS-MMI’s function associated to the button));(4) MMI_gen 11716 (partly: MMI_gen 4689, MMI_gen 4690);(5) MMI_gen 11697 (partly: entry);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press and hold the ‘0’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘0’ button is displayed pressed and immediately re-displayed enabled." + Environment.NewLine +
                                "2. The ‘Click’ sound is played once." + Environment.NewLine +
                                "3. The ‘Wheel diameter 1 (mm)’ data input field displays the value ‘0’." + Environment.NewLine +
                                "4. The cursor is displayed beneath the ‘0’ in the data input field.");

            /*
            Test Step 3
            Action: Release the pressed button
            Expected Result: Verify the following information,The state of released button is changed to enabled
            Test Step Comment: (1) MMI_gen 11716 (partly: MMI_gen 4913 (partly: Wheel diameter 1), MMI_gen 4384 (partly: ETCS-MMI’s function associated to the button));
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Release the ‘0’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘0’ button is displayed enabled");

            /*
            Test Step 4
            Action: Perform action step 2-3 for the ‘1’ to ‘9’ buttons.Note: Press the ‘Del’ button to delete an information when entered data is out of input field range is acceptable
            Expected Result: See the expected results of Step 2 – Step 3 and the following additional information,The pressed key is added in an input field immediately. The cursor is jumped to next position after entered the character immediately
            Test Step Comment: (1) MMI_gen 11716 (partly: MMI_gen 4642 (partly: Wheel diameter 1));  (2) MMI_gen 11716 (partly: MMI_gen 4692 (partly: Wheel diameter 1));  
            */
            // Repeat Steps 2-3 for the <1> button
            DmiActions.ShowInstruction(this, @"Press and hold the ‘1’ button");
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘0’ button is displayed pressed and immediately re-displayed enabled." + Environment.NewLine +
                                "2. The ‘Click’ sound is played once." + Environment.NewLine +
                                "3. The value for the button pressed is added to the end of the ‘Wheel diameter 1 (mm)’ data input field." + Environment.NewLine +
                                "4. The cursor is displayed beneath the ‘1’ in the data input field.");

            DmiActions.ShowInstruction(this, @"Release the ‘1’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘1’ button is displayed enabled");

            // Repeat Steps 2-3 for the <2> button
            DmiActions.ShowInstruction(this, @"Press and hold the ‘2’ button");
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘2’ button is displayed pressed and immediately re-displayed enabled." + Environment.NewLine +
                                "2. The ‘Click’ sound is played once." + Environment.NewLine +
                                "3. The value for the button pressed is added to the end of the ‘Wheel diameter 1 (mm)’ data input field." + Environment.NewLine +
                                "4. The cursor is displayed beneath the ‘2’ in the data input field.");

            DmiActions.ShowInstruction(this, @"Release the ‘2’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘2’ button is displayed enabled");

            // Repeat Steps 2-3 for the <3> button
            DmiActions.ShowInstruction(this, @"Press and hold the ‘3’ button");
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘3’ button is displayed pressed and immediately re-displayed enabled." + Environment.NewLine +
                                "2. The ‘Click’ sound is played once." + Environment.NewLine +
                                "3. The value for the button pressed is added to the end of the ‘Wheel diameter 1 (mm)’ data input field." + Environment.NewLine +
                                "4. The cursor is displayed beneath the ‘3’ in the data input field.");

            DmiActions.ShowInstruction(this, @"Release the ‘3’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘3’ button is displayed enabled");

            // Repeat Steps 2-3 for the <4> button
            DmiActions.ShowInstruction(this, @"Press and hold the ‘4’ button");
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘4’ button is displayed pressed and immediately re-displayed enabled." + Environment.NewLine +
                                "2. The ‘Click’ sound is played once." + Environment.NewLine +
                                "3. The value for the button pressed is added to the end of the ‘Wheel diameter 1 (mm)’ data input field." + Environment.NewLine +
                                "4. The cursor is displayed beneath the ‘4’ in the data input field.");

            DmiActions.ShowInstruction(this, @"Release the ‘4’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘4’ button is displayed enabled");

            // Repeat Steps 2-3 for the <5> button
            DmiActions.ShowInstruction(this, @"Press and hold the ‘5’ button");
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘5’ button is displayed pressed and immediately re-displayed enabled." + Environment.NewLine +
                                "2. The ‘Click’ sound is played once." + Environment.NewLine +
                                "3. The value for the button pressed is added to the end of the ‘Wheel diameter 1 (mm)’ data input field." + Environment.NewLine +
                                "4. The cursor is displayed beneath the ‘5’ in the data input field.");

            DmiActions.ShowInstruction(this, @"Release the ‘5’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘5’ button is displayed enabled");

            // Repeat Steps 2-3 for the <6> button
            DmiActions.ShowInstruction(this, @"Press and hold the ‘6’ button");
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘6’ button is displayed pressed and immediately re-displayed enabled." + Environment.NewLine +
                                "2. The ‘Click’ sound is played once." + Environment.NewLine +
                                "3. The value for the button pressed is added to the end of the ‘Wheel diameter 1 (mm)’ data input field." + Environment.NewLine +
                                "4. The cursor is displayed beneath the ‘6’ in the data input field.");

            DmiActions.ShowInstruction(this, @"Release the ‘6’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘6’ button is displayed enabled");

            // Repeat Steps 2-3 for the <7> button
            DmiActions.ShowInstruction(this, @"Press and hold the ‘7’ button");
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘7’ button is displayed pressed and immediately re-displayed enabled." + Environment.NewLine +
                                "2. The ‘Click’ sound is played once." + Environment.NewLine +
                                "3. The value for the button pressed is added to the end of the ‘Wheel diameter 1 (mm)’ data input field." + Environment.NewLine +
                                "4. The cursor is displayed beneath the ‘7’ in the data input field.");

            DmiActions.ShowInstruction(this, @"Release the ‘7’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘7’ button is displayed enabled");

            // Repeat Steps 2-3 for the <8> button
            DmiActions.ShowInstruction(this, @"Press and hold the ‘8’ button");
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘8’ button is displayed pressed and immediately re-displayed enabled." + Environment.NewLine +
                                "2. The ‘Click’ sound is played once." + Environment.NewLine +
                                "3. The value for the button pressed is added to the end of the ‘Wheel diameter 1 (mm)’ data input field." + Environment.NewLine +
                                "4. The cursor is displayed beneath the ‘8’ in the data input field.");

            DmiActions.ShowInstruction(this, @"Release the ‘8’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘8’ button is displayed enabled");

            // Repeat Steps 2-3 for the <9> button
            DmiActions.ShowInstruction(this, @"Press and hold the ‘9’ button");
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘9’ button is displayed pressed and immediately re-displayed enabled." + Environment.NewLine +
                                "2. The ‘Click’ sound is played once." + Environment.NewLine +
                                "3. The value for the button pressed is added to the end of the ‘Wheel diameter 1 (mm)’ data input field." + Environment.NewLine +
                                "4. The cursor is displayed beneath the ‘9’ in the data input field.");

            DmiActions.ShowInstruction(this, @"Release the ‘9’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘9’ button is displayed enabled");

            /*
            Test Step 5
            Action: Press and hold ‘Del’ button.Note: Stopwatch is required
            Expected Result: Verify the following information,While press and hold button less than 1.5 secSound ‘Click’ is played once.The state of button is changed to ‘Pressed’ and immediately back to ‘Enabled’ state.The last character is removed from an input field after pressing the button.While press and hold button over 1.5 secThe state ‘pressed’ and ‘released’ are switched repeatly while button is pressed and the characters are removed from an input field repeatly refer to pressed state.The sound ‘Click’ is played repeatly while button is pressed
            Test Step Comment: (1) MMI_gen 11716 (partly: MMI_gen 4913 (partly: Wheel diameter 1), MMI_gen 4384 (partly: sound ‘Click’)); MMI_gen 9512; MMI_gen 968;(2) MMI_gen 11716 (partly: MMI_gen 4913 (partly: Wheel diameter 1), MMI_gen 4384 (partly: Change to state ‘Pressed’ and immediately back to state ‘Enabled’));   (3) MMI_gen 11716 (partly: MMI_gen 4913 (partly: Wheel diameter 1), MMI_gen 4384 (partly: ETCS-MMI’s function associated to the button)); MMI_gen 4393 (partly: [Delete]);(4) MMI_gen 11716 (partly: MMI_gen 4913 (partly: Wheel diameter 1), MMI_gen 4386 (partly: visual of repeat function));(5) MMI_gen 11716 (partly: MMI_gen 4913 (partly: Wheel diameter 1), MMI_gen 4386 (partly: audible of repeat function));
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press and hold ‘Del’ button for more than 1.5 s.");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Click’ sound is played once." + Environment.NewLine +
                                "2. The ‘Del’ button is displayed pressed and immediately re-displayed enabled." + Environment.NewLine +
                                "3. The last character in the data input field is deleted." + Environment.NewLine +
                                "4. After 1.5s the ‘Del’ button repeatedly changes from pressed to released and the last character in the data input field is deleted repeatedly." +
                                "5. After 1.5s the ‘Click’ sound is played repeatedly while the button is pressed.");

            /*
            Test Step 6
            Action: Release ‘Del’ button
            Expected Result: Verify the following information, The character is stop removing
            Test Step Comment: (1) MMI_gen 11716 (partly: MMI_gen 4913 (partly: Wheel diameter 1)), MMI_gen 4384 (partly: ETCS-MMI’s function associated to the button));
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Release the ‘Del’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Characters stop being removed from the end of the data input field");

            /*
            Test Step 7
            Action: Delete the old value and enter the value ‘1000’ for Wheel diameter 1.Then, confirm an entered data by pressing an input field
            Expected Result: Verify the following information,Input fieldsThe associated ‘Enter’ button is data field itself.An input field is used to allow the driver to enter data.The state of ‘Wheel diameter 1’ input field is changed to ‘accepted’ as follows,The background colour of the Data Area is dark-grey.The colour of data value is white.The next input field ‘Wheel diameter 2’ is in state ‘selected’ as follows,The background colour of the Data Area is medium-grey.The colour of data value is black.Echo TextsThe echo text of ‘Wheel diameter 1’ is changed to white colour.The value of echo text is changed refer to entered data.Entering CharactersThe cursor is displayed as a horizontal line below the position of the next character to be entered.The cursor is flashed by changing from visible to not visible.KeyboardThe keyboard associated to selected input field ‘Wheel diameter 2’ is Numeric keyboard.The keyboard contains enabled button for the number <1> to <9>, <Delete>(NA21) , <0> and disabled <Decimal_Separator>. NA21, Delete button
            Test Step Comment: (1) MMI_gen 11716 (partly: MMI_gen 4682 (partly: Wheel diameter 1));(2) MMI_gen 11716 (partly: MMI_gen 4634 (partly: Wheel diameter 1)); MMI_gen 11697 (partly: entry);(3) MMI_gen 11716 (partly: MMI_gen 4652 (partly: Wheel diameter 1), MMI_gen 4684 (partly: accepted, Wheel diameter 1));(4) MMI_gen 11716 (partly: MMI_gen 4684 (partly: Wheel diameter 2, selected automatically), MMI_gen 4651 (partly: Wheel diameter 2));(5) MMI_gen 11716 (partly: MMI_gen 4700 (partly: Wheel diameter 1));(6) MMI_gen 11716 (partly: MMI_gen 4681 (partly: Wheel diameter 1), MMI_gen 4890, MMI_gen 4698);(7) MMI_gen 11716 (partly: MMI_gen 4689, MMI_gen 4690);(8) MMI_gen 11716 (partly: MMI_gen 4691 (partly: flash, Wheel diameter 2));(9) MMI_gen 11700 (partly: Wheel diameter 2); MMI_gen 11716 (partly: MMI_gen 4912 (partly: Wheel diameter 2), MMI_gen 4678 (partly: Wheel diameter 2));(10) MMI_gen 11716 (partly: MMI_gen 5003 (partly: Wheel diameter 2)); MMI_gen 4392 (partly: [Delete] NA21);
            */
            DmiActions.ShowInstruction(this, @"Delete the value in the ‘Wheel diameter 1 (mm)’ data input field, enter the value ‘1000’ and confirm the value by pressing a data input field");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The data input field acts an ‘Enter’ button" + Environment.NewLine +
                                "2. Data can be entered in the data input field" + Environment.NewLine +
                                "3. The ‘Wheel diameter 1’ input field is ‘accepted’, with the data value in white on a Dark-grey background." + Environment.NewLine +
                                "4. The next data input field ‘Wheel diameter 2’ is ‘selected’, with the data value in black on a Medium-grey background." + Environment.NewLine +
                                "5. The echo text of ‘Wheel diameter 1’ is displayed in white." + Environment.NewLine +
                                "6. The echo text value changes to the value entered in the data input field." + Environment.NewLine +
                                "7. The cursor is displayed as a horizontal line below the position of the next character to be entered." + Environment.NewLine +
                                "8. The cursor flashes by changing from visible to invisible." + Environment.NewLine +
                                "9. The keypad for the ‘Wheel diameter 2’ data input field is numeric." + Environment.NewLine +
                                "10. The keypad contains enabled buttons for the number <1> to <9>, <Delete> (symbol NA21), <0> and a disabled <Decimal_Separator> button.");

            /*
            Test Step 8
            Action: Perform action step 2-6 for keypad of the ‘Wheel diameter 2’ input field
            Expected Result: See the expected results of Step 2 – Step 6 and the following additional information,The pressed key is added in an input field immediately. The cursor is jumped to next position after entered the character immediately
            Test Step Comment: (1) MMI_gen 11716 (partly: MMI_gen 4642 (partly: Wheel diameter 2));  (2) MMI_gen 11716 (partly: MMI_gen 4692 (partly: Wheel diameter 2));  
            */
            DmiActions.ShowInstruction(this, @"Press and hold the ‘0’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘0’ button is displayed pressed and immediately re-displayed enabled." + Environment.NewLine +
                                "2. The ‘Click’ sound is played once." + Environment.NewLine +
                                "3. The ‘Wheel diameter 2 (mm)’ data input field displays the value ‘0’." + Environment.NewLine +
                                "4. The cursor is displayed after the ‘0’ in the data input field.");

            DmiActions.ShowInstruction(this, @"Release the ‘0’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘0’ button is displayed enabled");

            // Repeat Steps 2-3 for the <1> button
            DmiActions.ShowInstruction(this, @"Press and hold the ‘1’ button");
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘0’ button is displayed pressed and immediately re-displayed enabled." + Environment.NewLine +
                                "2. The ‘Click’ sound is played once." + Environment.NewLine +
                                "3. The value for the button pressed is added to the end of the ‘Wheel diameter 2 (mm)’ data input field." + Environment.NewLine +
                                "4. The cursor is displayed after the ‘1’ in the data input field.");

            DmiActions.ShowInstruction(this, @"Release the ‘1’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘1’ button is displayed enabled");

            // Repeat Steps 2-3 for the <2> button
            DmiActions.ShowInstruction(this, @"Press and hold the ‘2’ button");
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘2’ button is displayed pressed and immediately re-displayed enabled." + Environment.NewLine +
                                "2. The ‘Click’ sound is played once." + Environment.NewLine +
                                "3. The value for the button pressed is added to the end of the ‘Wheel diameter 2 (mm)’ data input field." + Environment.NewLine +
                                "4. The cursor is displayed after the ‘2’ in the data input field.");

            DmiActions.ShowInstruction(this, @"Release the ‘2’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘2’ button is displayed enabled");

            // Repeat Steps 2-3 for the <3> button
            DmiActions.ShowInstruction(this, @"Press and hold the ‘3’ button");
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘3’ button is displayed pressed and immediately re-displayed enabled." + Environment.NewLine +
                                "2. The ‘Click’ sound is played once." + Environment.NewLine +
                                "3. The value for the button pressed is added to the end of the ‘Wheel diameter 2 (mm)’ data input field." + Environment.NewLine +
                                "4. The cursor is displayed after the ‘3’ in the data input field.");

            DmiActions.ShowInstruction(this, @"Release the ‘3’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘3’ button is displayed enabled");

            // Repeat Steps 2-3 for the <4> button
            DmiActions.ShowInstruction(this, @"Press and hold the ‘4’ button");
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘4’ button is displayed pressed and immediately re-displayed enabled." + Environment.NewLine +
                                "2. The ‘Click’ sound is played once." + Environment.NewLine +
                                "3. The value for the button pressed is added to the end of the ‘Wheel diameter 2 (mm)’ data input field." + Environment.NewLine +
                                "4. The cursor is displayed after the ‘4’ in the data input field.");

            DmiActions.ShowInstruction(this, @"Release the ‘4’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘4’ button is displayed enabled");

            // Repeat Steps 2-3 for the <5> button
            DmiActions.ShowInstruction(this, @"Press and hold the ‘5’ button");
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘5’ button is displayed pressed and immediately re-displayed enabled." + Environment.NewLine +
                                "2. The ‘Click’ sound is played once." + Environment.NewLine +
                                "3. The value for the button pressed is added to the end of the ‘Wheel diameter 2 (mm)’ data input field." + Environment.NewLine +
                                "4. The cursor is displayed after the ‘5’ in the data input field.");

            DmiActions.ShowInstruction(this, @"Release the ‘5’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘5’ button is displayed enabled");

            // Repeat Steps 2-3 for the <6> button
            DmiActions.ShowInstruction(this, @"Press and hold the ‘6’ button");
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘6’ button is displayed pressed and immediately re-displayed enabled." + Environment.NewLine +
                                "2. The ‘Click’ sound is played once." + Environment.NewLine +
                                "3. The value for the button pressed is added to the end of the ‘Wheel diameter 2 (mm)’ data input field." + Environment.NewLine +
                                "4. The cursor is displayed after the ‘6’ in the data input field.");

            DmiActions.ShowInstruction(this, @"Release the ‘6’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘6’ button is displayed enabled");

            // Repeat Steps 2-3 for the <7> button
            DmiActions.ShowInstruction(this, @"Press and hold the ‘7’ button");
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘7’ button is displayed pressed and immediately re-displayed enabled." + Environment.NewLine +
                                "2. The ‘Click’ sound is played once." + Environment.NewLine +
                                "3. The value for the button pressed is added to the end of the ‘Wheel diameter 2 (mm)’ data input field." + Environment.NewLine +
                                "4. The cursor is displayed after the ‘7’ in the data input field.");

            DmiActions.ShowInstruction(this, @"Release the ‘7’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘7’ button is displayed enabled");

            // Repeat Steps 2-3 for the <8> button
            DmiActions.ShowInstruction(this, @"Press and hold the ‘8’ button");
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘8’ button is displayed pressed and immediately re-displayed enabled." + Environment.NewLine +
                                "2. The ‘Click’ sound is played once." + Environment.NewLine +
                                "3. The value for the button pressed is added to the end of the ‘Wheel diameter 2 (mm)’ data input field." + Environment.NewLine +
                                "4. The cursor is displayed after the ‘8’ in the data input field.");

            DmiActions.ShowInstruction(this, @"Release the ‘8’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘8’ button is displayed enabled");

            // Repeat Steps 2-3 for the <9> button
            DmiActions.ShowInstruction(this, @"Press and hold the ‘9’ button");
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘9’ button is displayed pressed and immediately re-displayed enabled." + Environment.NewLine +
                                "2. The ‘Click’ sound is played once." + Environment.NewLine +
                                "3. The value for the button pressed is added to the end of the ‘Wheel diameter 2 (mm)’ data input field." + Environment.NewLine +
                                "4. The cursor is displayed after the ‘9’ in the data input field.");

            DmiActions.ShowInstruction(this, @"Release the ‘9’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘9’ button is displayed enabled");

            // Repeat Step 5
            DmiActions.ShowInstruction(this, @"Press and hold ‘Del’ button for more than 1.5 s.");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Click’ sound is played once." + Environment.NewLine +
                                "2. The ‘Del’ button is displayed pressed and immediately re-displayed enabled." + Environment.NewLine +
                                "3. The last character in the data input field is deleted." + Environment.NewLine +
                                "4. After 1.5s the ‘Del’ button repeatedly changes from pressed to released and the last character in the data input field is deleted repeatedly." +
                                "5. After 1.5s the ‘Click’ sound is played repeatedly while the button is pressed.");

            // Repeat Step 6
            DmiActions.ShowInstruction(this, @"Release the ‘Del’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Characters stop being removed from the end of the data input field");

            /*
            Test Step 9
            Action: Delete the old value and enter the value ‘1000’ for Wheel diameter 2.Then, confirm an entered data by pressing an input field
            Expected Result: Verify the following information,Input fieldsThe associated ‘Enter’ button is data field itself.An input field is used to allow the driver to enter data.The state of ‘Wheel diameter 2’ input field is changed to ‘accepted’ as follows,The background colour of the Data Area is dark-grey.The colour of data value is white.The next input field ‘Accuracy’ is in state ‘selected’ as follows,The background colour of the Data Area is medium-grey.The colour of data value is black.Echo TextsThe echo text of ‘Wheel diameter 2’ is changed to white colour.The value of echo text is changed refer to entered data.Entering CharactersThe cursor is displayed as a horizontal line below the position of the next character to be entered.The cursor is flashed by changing from visible to not visible.KeyboardThe keyboard associated to selected input field ‘Accuracy’ is Numeric keyboard.The keyboard contains enabled button for the number <1> to <9>, <Delete>(NA21) , <0> and disabled <Decimal_Separator>. NA21, Delete button
            Test Step Comment: (1) MMI_gen 11716 (partly: MMI_gen 4682 (partly: Wheel diameter 2));(2) MMI_gen 11716 (partly: MMI_gen 4634 (partly: Wheel diameter 2)); MMI_gen 11696 (partly: entry);(3) MMI_gen 11716 (partly: MMI_gen 4652 (partly: Wheel diameter 2), MMI_gen 4684 (partly: accepted, Wheel diameter 2));(4) MMI_gen 11716 (partly: MMI_gen 4684 (partly: Accuracy, selected automatically), MMI_gen 4651 (partly: Accuracy));(5) MMI_gen 11716 (partly: MMI_gen 4700 (partly: Wheel diameter 2));(6) MMI_gen 11716 (partly: MMI_gen 4681 (partly: Wheel diameter 2), MMI_gen 4890, MMI_gen 4698);(7) MMI_gen 11716 (partly: MMI_gen 4689, MMI_gen 4690);(8) MMI_gen 11716 (partly: MMI_gen 4691 (partly: flash, Accuracy));(9) MMI_gen 11700 (partly: Accuracy); MMI_gen 11716 (partly: MMI_gen 4912 (partly: Accuracy), MMI_gen 4678 (partly: Accuracy));(10) MMI_gen 11716 (partly: MMI_gen 5003 (partly: Accuracy)); MMI_gen 4392 (partly: [Delete] NA21);
            */
            DmiActions.ShowInstruction(this, @"Delete the value in the ‘Wheel diameter 2 (mm)’ data input field, enter the value ‘1000’ and confirm the value by pressing a data input field");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The data input field acts an ‘Enter’ button" + Environment.NewLine +
                                "2. Data can be entered in the data input field" + Environment.NewLine +
                                "3. The ‘Wheel diameter 2’ input field is ‘accepted’, with the data value in white on a Dark-grey background." + Environment.NewLine +
                                "4. The next data input field ‘Accuracy’ is ‘selected’, with the data value in black on a Medium-grey background." + Environment.NewLine +
                                "5. The echo text of ‘Wheel diameter 2’ is displayed in white." + Environment.NewLine +
                                "6. The echo text value changes to the value entered in the data input field." + Environment.NewLine +
                                "7. The cursor is displayed as a horizontal line below the position of the next character to be entered." + Environment.NewLine +
                                "8. The cursor flashes by changing from visible to invisible." + Environment.NewLine +
                                "9. The keypad for the ‘Accuracy’ data input field is numeric." + Environment.NewLine +
                                "10. The keypad contains enabled buttons for the number <1> to <9>, <Delete> (symbol NA21), <0> and a disabled <Decimal_Separator> button.");

            /*
            Test Step 10
            Action: Perform action step 2-6 for keypad of the ‘Accuracy’ input field
            Expected Result: See the expected results of Step 2 – Step 6 and the following additional information,The pressed key is added in an input field immediately. The cursor is jumped to next position after entered the character immediately
            Test Step Comment: (1) MMI_gen 11716 (partly: MMI_gen 4642 (partly: Accuracy));  (2) MMI_gen 11716 (partly: MMI_gen 4692 (partly: Accuracy));  
            */
            DmiActions.ShowInstruction(this, @"Press and hold the ‘0’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘0’ button is displayed pressed and immediately re-displayed enabled." + Environment.NewLine +
                                "2. The ‘Click’ sound is played once." + Environment.NewLine +
                                "3. The ‘Accuracy’ data input field displays the value ‘0’." + Environment.NewLine +
                                "4. The cursor is displayed after the ‘0’ in the data input field.");

            DmiActions.ShowInstruction(this, @"Release the ‘0’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘0’ button is displayed enabled");

            // Repeat Steps 2-3 for the <1> button
            DmiActions.ShowInstruction(this, @"Press and hold the ‘1’ button");
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘0’ button is displayed pressed and immediately re-displayed enabled." + Environment.NewLine +
                                "2. The ‘Click’ sound is played once." + Environment.NewLine +
                                "3. The value for the button pressed is added to the end of the ‘Accuracy’ data input field." + Environment.NewLine +
                                "4. The cursor is displayed after the ‘1’ in the data input field.");

            DmiActions.ShowInstruction(this, @"Release the ‘1’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘1’ button is displayed enabled");

            // Repeat Steps 2-3 for the <2> button
            DmiActions.ShowInstruction(this, @"Press and hold the ‘2’ button");
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘2’ button is displayed pressed and immediately re-displayed enabled." + Environment.NewLine +
                                "2. The ‘Click’ sound is played once." + Environment.NewLine +
                                "3. The value for the button pressed is added to the end of the ‘Accuracy’ data input field." + Environment.NewLine +
                                "4. The cursor is displayed after the ‘2’ in the data input field.");

            DmiActions.ShowInstruction(this, @"Release the ‘2’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘2’ button is displayed enabled");

            // Repeat Steps 2-3 for the <3> button
            DmiActions.ShowInstruction(this, @"Press and hold the ‘3’ button");
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘3’ button is displayed pressed and immediately re-displayed enabled." + Environment.NewLine +
                                "2. The ‘Click’ sound is played once." + Environment.NewLine +
                                "3. The value for the button pressed is added to the end of the ‘Accuracy’ data input field." + Environment.NewLine +
                                "4. The cursor is displayed after the ‘3’ in the data input field.");

            DmiActions.ShowInstruction(this, @"Release the ‘3’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘3’ button is displayed enabled");

            // Repeat Steps 2-3 for the <4> button
            DmiActions.ShowInstruction(this, @"Press and hold the ‘4’ button");
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘4’ button is displayed pressed and immediately re-displayed enabled." + Environment.NewLine +
                                "2. The ‘Click’ sound is played once." + Environment.NewLine +
                                "3. The value for the button pressed is added to the end of the ‘Accuracy’ data input field." + Environment.NewLine +
                                "4. The cursor is displayed after the ‘4’ in the data input field.");

            DmiActions.ShowInstruction(this, @"Release the ‘4’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘4’ button is displayed enabled");

            // Repeat Steps 2-3 for the <5> button
            DmiActions.ShowInstruction(this, @"Press and hold the ‘5’ button");
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘5’ button is displayed pressed and immediately re-displayed enabled." + Environment.NewLine +
                                "2. The ‘Click’ sound is played once." + Environment.NewLine +
                                "3. The value for the button pressed is added to the end of the ‘Accuracy’ data input field." + Environment.NewLine +
                                "4. The cursor is displayed after the ‘5’ in the data input field.");

            DmiActions.ShowInstruction(this, @"Release the ‘5’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘5’ button is displayed enabled");

            // Repeat Steps 2-3 for the <6> button
            DmiActions.ShowInstruction(this, @"Press and hold the ‘6’ button");
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘6’ button is displayed pressed and immediately re-displayed enabled." + Environment.NewLine +
                                "2. The ‘Click’ sound is played once." + Environment.NewLine +
                                "3. The value for the button pressed is added to the end of the ‘Accuracy’ data input field." + Environment.NewLine +
                                "4. The cursor is displayed after the ‘6’ in the data input field.");

            DmiActions.ShowInstruction(this, @"Release the ‘6’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘6’ button is displayed enabled");

            // Repeat Steps 2-3 for the <7> button
            DmiActions.ShowInstruction(this, @"Press and hold the ‘7’ button");
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘7’ button is displayed pressed and immediately re-displayed enabled." + Environment.NewLine +
                                "2. The ‘Click’ sound is played once." + Environment.NewLine +
                                "3. The value for the button pressed is added to the end of the ‘Accuracy’ data input field." + Environment.NewLine +
                                "4. The cursor is displayed after the ‘7’ in the data input field.");

            DmiActions.ShowInstruction(this, @"Release the ‘7’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘7’ button is displayed enabled");

            // Repeat Steps 2-3 for the <8> button
            DmiActions.ShowInstruction(this, @"Press and hold the ‘8’ button");
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘8’ button is displayed pressed and immediately re-displayed enabled." + Environment.NewLine +
                                "2. The ‘Click’ sound is played once." + Environment.NewLine +
                                "3. The value for the button pressed is added to the end of the ‘Accuracy’ data input field." + Environment.NewLine +
                                "4. The cursor is displayed after the ‘8’ in the data input field.");

            DmiActions.ShowInstruction(this, @"Release the ‘8’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘8’ button is displayed enabled");

            // Repeat Steps 2-3 for the <9> button
            DmiActions.ShowInstruction(this, @"Press and hold the ‘9’ button");
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘9’ button is displayed pressed and immediately re-displayed enabled." + Environment.NewLine +
                                "2. The ‘Click’ sound is played once." + Environment.NewLine +
                                "3. The value for the button pressed is added to the end of the ‘Accuracy’ data input field." + Environment.NewLine +
                                "4. The cursor is displayed after the ‘9’ in the data input field.");

            DmiActions.ShowInstruction(this, @"Release the ‘9’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘9’ button is displayed enabled");

            // Repeat Step 5
            DmiActions.ShowInstruction(this, @"Press and hold ‘Del’ button for more than 1.5 s.");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Click’ sound is played once." + Environment.NewLine +
                                "2. The ‘Del’ button is displayed pressed and immediately re-displayed enabled." + Environment.NewLine +
                                "3. The last character in the data input field is deleted." + Environment.NewLine +
                                "4. After 1.5s the ‘Del’ button repeatedly changes from pressed to released and the last character in the data input field is deleted repeatedly." +
                                "5. After 1.5s the ‘Click’ sound is played repeatedly while the button is pressed.");

            // Repeat Step 6
            DmiActions.ShowInstruction(this, @"Release the ‘Del’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Characters stop being removed from the end of the data input field");

            /*
            Test Step 11
            Action: Delete the old value and enter the value ‘30’ for Accuracy.Then, confirm an entered data by pressing an input field
            Expected Result: Verify the following information,Input fieldsThe associated ‘Enter’ button is data field itself.An input field is used to allow the driver to enter data.The state of ‘Accuracy’ input field is changed to ‘accepted’ as follows,The background colour of the Data Area is dark-grey.The colour of data value is white.There is no input field selected.Echo TextsThe echo text of ‘Accuracy’ is changed to white colour.The value of echo text is changed refer to entered data.Data Entry windowThe state of ‘Yes’ button below text label ‘Train data Entry is complete?’ is enabled as follows,The background colour of the Data Area is medium-grey.The colour of data value is black.The border colour is medium-grey
            Test Step Comment: (1) MMI_gen 11716 (partly: MMI_gen 4682 (partly: Accuracy));(2) MMI_gen 11716 (partly: MMI_gen 4634 (partly: Accuracy)); MMI_gen 11698 (partly: entry);(3) MMI_gen 11716 (partly: MMI_gen 4652 (partly: Accuracy), MMI_gen 4684 (partly: accepted, Accuracy));(4) MMI_gen 11716 (partly: MMI_gen 4684 (partly: No next input field, data entry process terminated));(5) MMI_gen 11716 (partly: MMI_gen 4700 (partly: Accuracy));(6) MMI_gen 11716 (partly: MMI_gen 4681 (partly: Accuracy), MMI_gen 4698, MMI_gen 4890);(7) MMI_gen 11716 (partly: MMI_gen 4909 (partly:Enabled), MMI_gen 4910 (partly: Enabled, MMI_gen 4211 (partly: colour))); MMI_gen 4374; 
            */
            DmiActions.ShowInstruction(this, @"Delete the value in the ‘Accuracy’ data input field, enter the value ‘30’ and confirm the value by pressing a data input field");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The data input field acts an ‘Enter’ button" + Environment.NewLine +
                                "2. Data can be entered in the data input field" + Environment.NewLine +
                                "3. The ‘Accuracy’ input field is ‘accepted’, with the data value in white on a Dark-grey background." + Environment.NewLine +
                                "4. No data input field is ‘selected’." + Environment.NewLine +
                                "5. The echo text of ‘Accuracy’ is displayed in white." + Environment.NewLine +
                                "6. The echo text value changes to the value entered in the data input field." + Environment.NewLine +
                                "7. The ‘Yes’ button below the label ‘Train data entry is complete’ is displayed enabled." + Environment.NewLine +
                                "8. The data input field value is in black  on a Medium-grey background with a Medium-grey border.");

            /*
            Test Step 12
            Action: Perform the following procedure,Select ‘Wheel diameter 1’ input field.Enter new value for ‘Wheel diameter 1’.Select ‘Accuracy’ input field
            Expected Result: Verify the following information,The state of ‘Yes’ button below text label ‘Wheel diameter entry is complete?’ is disabled. The state of input field ‘Wheel diameter 1’ is changed to ‘Not selected’ as follows,The value of ‘Wheel diameter 1’ input field is removed, display as blank.The background colour of the input field is dark-grey
            Test Step Comment: (1) MMI_gen 11716 (partly: MMI_gen 4909 (partly: state selected and with recently entered key), MMI_gen 4680 (partly: value has been modified));(2) MMI_gen 11716 (partly: MMI_gen 4680 (partly: Wheel diameter 1, Not selected, Data area is blank), MMI_gen 4649 (partly: data entry, background colour));
            */
            DmiActions.ShowInstruction(this, @"Select the ‘Wheel diameter 1’ data input field and enter a new value for ‘Wheel diameter 1’, then select the ‘Accuracy’ data input field");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Yes’ button below the label ‘Train data entry is complete’ is displayed disabled." + Environment.NewLine +
                                "2. The ‘Wheel diameter 1’ data input field value is not selected." + Environment.NewLine +
                                "3. The ‘Wheel diameter 1’ data input field value is blank." + Environment.NewLine +
                                "4. The data input field value background is Dark-grey.");

            /*
            Test Step 13
            Action: Confirm the value of ‘Accuracy’
            Expected Result: Verify the following information,The state of input field ‘Wheel diameter 1’ is changed to ‘Selected’
            Test Step Comment: (1) MMI_gen 11716 (partly: MMI_gen 4685);
            */
            DmiActions.ShowInstruction(this, @"Confirm value of the ‘Accuracy’ data input field");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Wheel diameter 1’ data input field value is selected.");

            /*
            Test Step 14
            Action: Enter and confirm the new value ‘1000’ for ‘Wheel diameter 1’ value.Press and hold ‘Yes’ button
            Expected Result: Verify the following information,The state of button is changed to ‘Pressed’, the border of button is removed.The sound ‘Click’ is played once
            Test Step Comment: (1) MMI_gen 11716 (partly: MMI_gen 4911 (partly: MMI_gen 4381 (partly: change to state ‘Pressed’ as long as remain actuated)); MMI_gen 4375;(2) MMI_gen 11716 (partly: MMI_gen 4911 (partly: MMI_gen 4381 (partly: sound ‘Click’))); MMI_gen 9512; MMI_gen 968;
            */
            DmiActions.ShowInstruction(this, @"Enter and confirm the value ‘1000’ in the ‘Wheel diameter 1’ data input field, then press and hold the ‘Yes’ button");
            
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Yes’ button is displayed pressed, without a border.");

            /*
            Test Step 15
            Action: Slide out the ‘Yes’ button
            Expected Result: Verify the following information,The border of the input field is shown (state ‘Enabled’) without a sound
            Test Step Comment: (1) MMI_gen 11716 (partly: MMI_gen 4911 (partly: MMI_gen 4382 (partly: state ‘Enabled’ when slide out with force applied, no sound))); MMI_gen 4374;
            */
            DmiActions.ShowInstruction(this, @"Drag the ‘Yes’ button outside of its area, keeping the button pressed");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Yes’ button is displayed enabled." + Environment.NewLine +
                                "2. No sound is played.");

            /*
            Test Step 16
            Action: Slide back into the ‘Yes’ button
            Expected Result: Verify the following information,The button is back to state ‘Pressed’ without a sound
            Test Step Comment: (1) MMI_gen 11716 (partly: MMI_gen 4911 (partly: MMI_gen 4382 (partly: state ‘Pressed’ when slide back, no sound))); MMI_gen 4375;
            */
            DmiActions.ShowInstruction(this, @"Whilst keeping the ‘Yes’ button pressed, drag it back inside its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Yes’ button is displayed pressed." + Environment.NewLine +
                                "2. No sound is played.");

            /*
            Test Step 17
            Action: Release ‘Yes’ button
            Expected Result: Verify the following information,DMI displays Wheel diameter validation window.Use the log file to confirm that DMI sent out packet [MMI_NEW_MAINTENANCE_DATA (EVC-140)] with following variables,MMI_Q_MD_DATASET = 0MMI_M_SDU_WHEEL_SIZE_1 = 1000MMI_M_SDU_WHEEL_SIZE_2 = 1000MMI_M_WHEEL_DIZE_ERR = 30 Use the log file to confirm that the Wheel diameter window is closed because of DMI received packet information [MMI_ECHOED_MAINTENANCE_DATA (EVC-41)]
            Test Step Comment: (1) MMI_gen 11716 (partly: MMI_gen 4911 (partly: MMI_gen 4381 (partly: exit state ‘pressed’), MMI_gen 4384 (partly: ETCS-MMI’s function associated to the button)));(2) MMI_gen 11757;(3) MMI_gen 11759;
            */
            DmiActions.ShowInstruction(this, @"Release the ‘Yes’ button");

            EVC140_MMINewMaintenanceData.MMI_Q_MD_DATASET = Variables.MMI_Q_MD_DATASET.WheelDiameter;
            EVC140_MMINewMaintenanceData.MMI_M_SDU_WHEEL_SIZE_1 = (Telegrams.EVCtoDMI.Variables.MMI_M_SDU_WHEEL_SIZE)0x3e8;
            EVC140_MMINewMaintenanceData.MMI_M_SDU_WHEEL_SIZE_2 =  (Telegrams.EVCtoDMI.Variables.MMI_M_SDU_WHEEL_SIZE)0x3e8;
            EVC140_MMINewMaintenanceData.MMI_M_WHEEL_SIZE_ERR = (Telegrams.EVCtoDMI.Variables.MMI_M_WHEEL_SIZE_ERR)0x1d;
            EVC140_MMINewMaintenanceData.CheckTelegram();


            // assume that EVC41 will invert the values
            EVC41_MMIEchoedMaintenanceData.MMI_M_WHEEL_SIZE_ERR_ = (Variables.MMI_M_WHEEL_SIZE_ERR)0x1d;
            EVC41_MMIEchoedMaintenanceData.MMI_M_SDU_WHEEL_SIZE_2_ = (Variables.MMI_M_SDU_WHEEL_SIZE)0x03e8;
            EVC41_MMIEchoedMaintenanceData.MMI_M_SDU_WHEEL_SIZE_1_ = (Variables.MMI_M_SDU_WHEEL_SIZE)0x03e8;
            EVC41_MMIEchoedMaintenanceData.MMI_Q_MD_DATASET_ = Variables.MMI_Q_MD_DATASET.WheelDiameter;
            EVC41_MMIEchoedMaintenanceData.Send();
           
            // Spec says EVC41 closes the validation window: it displays it...
            /*
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI closes the Wheel diameter validation window.");
                                */

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Wheel diameter validation window.");

            /*
            Test Step 18
            Action: Press ‘Yes’ button and confirm entered data by pressing an input field
            Expected Result: DMI displays the Maintenance window
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press the ‘Yes’ button and confirm the entered data by pressing a data input field");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Maintenance window.");

            /*
            Test Step 19
            Action: Perform the following procedure,Press ‘Wheel diameter’ button.Confirm the current data of all input fields without re-entry.Press ‘Yes button.Press ‘Yes’ button and confirm entered data at Wheel diameter validation window
            Expected Result: Verify the following information,The first input field is used to revalidation the Wheel diameter 1.The second input field is used to revalidation the Wheel diameter 2.The third input field is used to revalidation the Accuracy
            Test Step Comment: (1) MMI_gen 11697 (partly: revalidation); (2) MMI_gen 11696 (partly: revalidation);(3) MMI_gen 11698 (partly: revalidation);
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Wheel diameter’ button");

            EVC40_MMICurrentMaintenanceData.MMI_M_WHEEL_SIZE_ERR = (Variables.MMI_M_WHEEL_SIZE_ERR)0x1d;
            EVC40_MMICurrentMaintenanceData.MMI_M_SDU_WHEEL_SIZE_2 = (Variables.MMI_M_SDU_WHEEL_SIZE)0x03e8;
            EVC40_MMICurrentMaintenanceData.MMI_M_SDU_WHEEL_SIZE_1 = (Variables.MMI_M_SDU_WHEEL_SIZE)0x03e8;
            EVC40_MMICurrentMaintenanceData.MMI_Q_MD_DATASET = Variables.MMI_Q_MD_DATASET.WheelDiameter;
            EVC40_MMICurrentMaintenanceData.Send();

            DmiActions.ShowInstruction(this, @"Confirm the current value of all data input fields, then press the ‘Yes button");

            EVC41_MMIEchoedMaintenanceData.MMI_M_WHEEL_SIZE_ERR_ = (Variables.MMI_M_WHEEL_SIZE_ERR)0x1d;
            EVC41_MMIEchoedMaintenanceData.MMI_M_SDU_WHEEL_SIZE_2_ = (Variables.MMI_M_SDU_WHEEL_SIZE)0x03e8;
            EVC41_MMIEchoedMaintenanceData.MMI_M_SDU_WHEEL_SIZE_1_ = (Variables.MMI_M_SDU_WHEEL_SIZE)0x03e8;
            EVC41_MMIEchoedMaintenanceData.MMI_Q_MD_DATASET_ = Variables.MMI_Q_MD_DATASET.WheelDiameter;
            EVC41_MMIEchoedMaintenanceData.Send();

            DmiActions.ShowInstruction(this, @"Confirm the entered data in the Wheel diameter validation window");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The first data input field displays the value entered for ‘Wheel diameter 1’ for validation." + Environment.NewLine +
                                "2. The second data input field displays the value entered for ‘Wheel diameter 2’ for validation." + Environment.NewLine +
                                "3. The third data input field displays the value entered for ‘Accuracy’ for validation.");

            /*
            Test Step 20
            Action: Press ‘Wheel diameter’ button
            Expected Result: DMI displays Wheel diameter window
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press the ‘Wheel diameter’ button");

            EVC40_MMICurrentMaintenanceData.MMI_M_WHEEL_SIZE_ERR = (Variables.MMI_M_WHEEL_SIZE_ERR)0x1d;
            EVC40_MMICurrentMaintenanceData.MMI_M_SDU_WHEEL_SIZE_2 = (Variables.MMI_M_SDU_WHEEL_SIZE)0x03e8;
            EVC40_MMICurrentMaintenanceData.MMI_M_SDU_WHEEL_SIZE_1 = (Variables.MMI_M_SDU_WHEEL_SIZE)0x03e8;
            EVC40_MMICurrentMaintenanceData.MMI_Q_MD_DATASET = Variables.MMI_Q_MD_DATASET.WheelDiameter;
            EVC40_MMICurrentMaintenanceData.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Wheel diameter window.");

            /*
            Test Step 21
            Action: Press and hold the Label area of ‘Wheel diameter 2’ input field
            Expected Result: Verify the following information,The state of ‘Wheel diameter 2’ input field is changed to ‘Pressed’, the border of button is removed.The state of ‘Wheel diameter 2’ input field remains ‘not selected’. The state of ‘Wheel diameter 1’ input field remains ‘selected’.The sound ‘Click’ is played once
            Test Step Comment: (1) MMI_gen 11716 (partly: MMI_gen 4686 (partly: Label part, Wheel diameter 2), MMI_gen 4381 (partly: change to state ‘Pressed’ as long as remain actuated))); MMI_gen 4392 (partly: [Enter], touch screen); MMI_gen 4375;(2) MMI_gen 11716 (partly: MMI_gen 4686 (partly: Label part, Wheel diameter 2), MMI_gen 4381 (partly: the sound for Up-Type button));
            */
            DmiActions.ShowInstruction(this, @"Press in and hold the label part of the ‘Wheel diameter 2’ data input field");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Wheel diameter 2’ data input field is displayed entered, without a border." + Environment.NewLine +
                                "2. The ‘Wheel diameter 2’ data input field is not selected." + Environment.NewLine +
                                "3. The ‘Wheel diameter 1’ data input field stays selected." + Environment.NewLine +
                                "4. The ‘Click’ sound is played once.");

            /*
            Test Step 22
            Action: Slide out the Label part of ‘Wheel diameter 2’ input field
            Expected Result: Verify the following information,The border of ‘Wheel diameter 2’ input field is shown (state ‘Enabled’) without a sound.The state of ‘Wheel diameter 2’ input field remains ‘not selected’. The state of ‘Wheel diameter 1’ input field remains ‘selected’
            Test Step Comment: (1) MMI_gen 11716 (partly: MMI_gen 4686 (partly: Label part, Wheel diameter 2), MMI_gen 4382 (partly: state ‘Enabled’ when slide out with force applied, no sound); MMI_gen 4374;
            */
            DmiActions.ShowInstruction(this, @"Drag the label part of the ‘Wheel diameter 2’ data input field outside of its area, keeping the area pressed");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Wheel diameter 2’ data input field is displayed with a border." + Environment.NewLine +
                                "2. No sound is played." + Environment.NewLine +
                                "3. The ‘Wheel diameter 2’ data input field stays not selected.");
            
            /*
            Test Step 23
            Action: Slide back into the Label part of ‘Wheel diameter 2’ input field
            Expected Result: Verify the following information,The state of ‘Wheel diameter 2’ input field is changed to ‘Pressed’, the border of button is removed.The state of ‘Wheel diameter 2’ input field remains ‘not selected’. The state of ‘Wheel diameter 1’ input field remains ‘selected’
            Test Step Comment: (1) MMI_gen 11716 (partly: MMI_gen 4686 (partly: Label part, Wheel diameter 2), MMI_gen 4382 (partly: state ‘Pressed’ when slide back, no sound); MMI_gen 4375;
            */
            DmiActions.ShowInstruction(this, @"Whilst keeping the label part of the ‘Wheel diameter 2’ data input field pressed, drag it back inside its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Wheel diameter 2’ data input field button is displayed pressed without a border." + Environment.NewLine +
                                "2. The ‘Wheel diameter 2’ data input field stays not selected." + Environment.NewLine +
                                "3. The ‘Wheel diameter 1’ data input field stays selected.");

            /*
            Test Step 24
            Action: Release the pressed area
            Expected Result: Verify the following information,The state of ‘Wheel diameter 2’ input field is changed to selected
            Test Step Comment: (1) MMI_gen 11716 (partly: MMI_gen 4686 (partly: Label part, Wheel diameter 2), MMI_gen 4381 (partly: exit state ‘Pressed’, execute function associated to the button)); MMI_gen 4374;
            */
            DmiActions.ShowInstruction(this, @"Release the label part of the ‘Wheel diameter 2’ data input field");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Wheel diameter 2’ data input field becomes selected.");

            /*
            Test Step 25
            Action: Perform action step 21-24 for the Label part of Wheel diameter 1 and Accuracy input field
            Expected Result: Verify the following information,The state of an input field is changed to ‘selected’ when release the pressed area at the Label part of input field
            Test Step Comment: (1) MMI_gen 11716 (partly: MMI_gen 4686 (partly: Label part));
            */
            DmiActions.ShowInstruction(this, @"Press in and hold the label part of the ‘Wheel diameter 1’ data input field");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Wheel diameter 1’ data input field is displayed entered, without a border." + Environment.NewLine +
                                "2. The ‘Wheel diameter 1’ data input field is not selected." + Environment.NewLine +
                                "3. The ‘Accuracy’ data input field stays selected." + Environment.NewLine +
                                "4. The ‘Click’ sound is played once.");
            DmiActions.ShowInstruction(this, @"Drag the label part of the ‘Wheel diameter 2’ data input field outside of its area, keeping the area pressed");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Wheel diameter 1’ data input field is displayed with a border." + Environment.NewLine +
                                "2. No sound is played." + Environment.NewLine +
                                "3. The ‘Wheel diameter 1’ data input field stays not selected.");
            DmiActions.ShowInstruction(this, @"Whilst keeping the label part of the ‘Wheel diameter 2’ data input field pressed, drag it back inside its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Wheel diameter 1’ data input field button is displayed pressed without a border." + Environment.NewLine +
                                "2. The ‘Wheel diameter 1’ data input field stays not selected." + Environment.NewLine +
                                "3. The ‘Accuracy’ data input field stays selected.");

            DmiActions.ShowInstruction(this, @"Release the label part of the ‘Wheel diameter 1’ data input field");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Wheel diameter 1’ data input field becomes selected.");

            /*
            Test Step 26
            Action: Perform action step 21-24 for the Data part of each input field
            Expected Result: Verify the following information,The state of an input field is changed to ‘selected’ when release the pressed area at the Data part of input field
            Test Step Comment: (1) MMI_gen 11716 (partly: MMI_gen 4686 (partly: Data part)); MMI_gen 9390 (partly: Wheel diameter window);
            */
            // Wheel diameter 1
            DmiActions.ShowInstruction(this, @"Press in and hold the data part of the ‘Wheel diameter 1’ data input field");
            DmiActions.ShowInstruction(this, @"Drag the data part of the ‘Wheel diameter 1’ data input field outside of its area, keeping the area pressed");
            DmiActions.ShowInstruction(this, @"Whilst keeping the label part of the ‘Wheel diameter 1’ data input field pressed, drag it back inside its area");
            DmiActions.ShowInstruction(this, @"Release the label part of the ‘Wheel diameter 1’ data input field");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Wheel diameter 2’ data input field becomes selected.");

            // Wheel diameter 2
            DmiActions.ShowInstruction(this, @"Press in and hold the data part of the ‘Wheel diameter 2’ data input field");
            DmiActions.ShowInstruction(this, @"Drag the data part of the ‘Wheel diameter 2’ data input field outside of its area, keeping the area pressed");
            DmiActions.ShowInstruction(this, @"Whilst keeping the label part of the ‘Wheel diameter 2’ data input field pressed, drag it back inside its area");
            DmiActions.ShowInstruction(this, @"Release the label part of the ‘Wheel diameter 2’ data input field");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Wheel diameter 2’ data input field becomes selected.");
            // Wheel diameter 3
            DmiActions.ShowInstruction(this, @"Press in and hold the data part of the ‘Accuracy’ data input field");
            DmiActions.ShowInstruction(this, @"Drag the data part of the ‘Accuracy’ data input field outside of its area, keeping the area pressed");
            DmiActions.ShowInstruction(this, @"Whilst keeping the label part of the ‘Accuracy’ data input field pressed, drag it back inside its area");
            DmiActions.ShowInstruction(this, @"Release the label part of the ‘WAccuracy’ data input field");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Accuracy’ data input field becomes selected.");

            /*
            Test Step 27
            Action: Press ‘Close’ button
            Expected Result: Verify the following information,Use the log file to confirm that DMI sent out packet [MMI_DRIVER_REQUEST (EVC-101)] with variable MMI_M_REQUEST = 54 (Exit Maintenance).The window is closed and the Maintenance window is displayed
            Test Step Comment: (1) MMI_gen 11758 (partly: EVC-101);(2) MMI_gen 11758 (partly: closure); MMI_gen 4392 (partly: returning to the parent window);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button");

            EVC101_MMIDriverRequest.CheckMRequestReleased = Variables.MMI_M_REQUEST.ExitMaintenance;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI closes the Wheel diameter window and displays the Maintenance window.");

            /*
            Test Step 28
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
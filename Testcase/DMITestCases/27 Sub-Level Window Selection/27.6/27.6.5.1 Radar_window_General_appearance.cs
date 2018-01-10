using System;
using Testcase.Telegrams.DMItoEVC;
using Testcase.Telegrams.EVCtoDMI;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 27.6.5.1 Radar window: General appearance
    /// TC-ID: 22.6.5.1
    /// 
    /// This test case verifies the display of the Radar window on DMI that shall comply with [ERA-ERTMS] standard and [MMI-ETCS-gen].
    /// 
    /// Tested Requirements:
    /// MMI_gen 11772; MMI_gen 11773; MMI_gen 11774; MMI_gen 11775; MMI_gen 11783; MMI_gen 11784; MMI_gen 11785; MMI_gen 11786; MMI_gen 11787; MMI_gen 11771 (partly: MMI_gen 4888, MMI_gen 4799 (partly: Close button, Window Title, Input fields), MMI_gen 4891 (partly: Yes button, Area for [Window Title] Entry complete?), MMI_gen 4910, MMI_gen 4211 (partly: colour), MMI_gen 4909, MMI_gen 4908 (partly: extended), MMI_gen 4637 (partly: Main-areas D and F), MMI_gen 4640, MMI_gen 4641, MMI_gen 9412, MMI_gen 4645, MMI_gen 4646 (partly: right aligned), MMI_gen 4647 (partly: left aligned), MMI_gen 4648, MMI_gen 4651, MMI_gen 4683, MMI_gen 5211, MMI_gen 4649, MMI_gen 4912, MMI_gen 5003, MMI_gen 5190, MMI_gen 4696, MMI_gen 4697, MMI_gen 4701, MMI_gen 4702 (partly: right aligned), MMI_gen 4704 (partly: left aligned), MMI_gen 4700, MMI_gen 4691 (partly: flash), MMI_gen 4689, MMI_gen 4690, MMI_gen 4913 (partly: MMI_gen 4384, MMI_gen 4386), MMI_gen 4642, MMI_gen 4682, MMI_gen 4634, MMI_gen 4652, MMI_gen 4684, MMI_gen 4890, MMI_gen 4698, MMI_gen 4681, MMI_gen 4692, MMI_gen 4680, MMI_gen 4685, MMI_gen 4911 (partly: MMI_gen 4381, MMI_gen 4382), MMI_gen 4686, MMI_gen 4679, MMI_gen 4720); MMI_gen 4392 (partly: [Close] NA11, [Delete] NA21, returning to the parent window); MMI_gen 4355 (partly: Buttons, Close button, input fields); MMI_gen 4377 (partly: shown); MMI_gen 4393 (partly: [Delete]); MMI_gen 4374; MMI_gen 4375; MMI_gen 4241; MMI_gen 4350; MMI_gen 4351; MMI_gen 4353; MMI_gen 9390 (partly: Radar window);
    /// 
    /// Scenario:
    /// Radar window, the window appearance is verified.The data entry functionality of the Radar window is verified with the Down-type button in keypad.The data revalidation functionality of the Radar window is verified.The Up-Type button on ‘Yes’ button is verified.The Up-Type button on each label part of an input field is verified.The Up-Type button on each data part of an input field is verified.The functionality of ‘Close’ button is verified.
    /// 
    /// Used files:
    /// N/A
    /// </summary>
    public class TC_22_6_5_1_Radar_window : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // The Maintenance password in tag name ‘PASS_CODE_MTN’ of the configuration file is set correctly refer to MMI_gen 11722.  Test system is power on.Cabin is activated.Settings window is opened from Driver ID window.Maintenance window is opened.

            // Call the TestCaseBase PreExecution
            base.PreExecution();

            DmiActions.Start_ATP();
            DmiActions.Activate_Cabin_1(this);
            DmiActions.Set_Driver_ID(this, "1234");
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Default; // settings window
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.EnableDoppler;
            EVC30_MMIRequestEnable.Send();
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
            Action: Press ‘Radar’ button
            Expected Result: DMI displays Radar window.Verify the following information,Data Entry WindowThe window title is ‘Radar’.The text label of the window title is right aligned.The following objects are displayed in Radar window,  Enabled Close button (NA11)Window TitleInput fieldsThe following objects are additionally displayed in Radar window,Yes buttonThe text label ‘Radar entry complete?’Yes button is displayed in Disabled state as follows,Text label is black Background colour is dark-greyThe border colour is medium-grey the same as the input field’s colour.The sensitive area of Yes button is extended from text label ‘Radar entry complete?’Input fieldsThe input fields are located on Main area D and F.Each input field is devided into a Label Area and a Data Area.The Label Area is give the topic of the input field.The Label Area text is displayed corresponding to the input field i.e. Radar 1 pulses (1/km). The Label Area is placed to the left of The Data Area.The text in the Label Area is aligned to the right.The value of data in the Data Area is aligned to the left.The text colour of the Label Area is grey and the background colour of the Label Area is dark-grey.There are only 2 input fields displayed in the first page of window.The first input field is in state ‘Selected’ as follows,The background colour of the Data Area is medium-grey.The colour of data value is black.All other input fields are in state ‘Not selected’ as follows,The background colour of the Data Area is dark-grey.The colour of data value is grey.KeyboardThe keyboard associated to selected input field ‘Radar 1’ is Numeric keyboard.The keyboard contains enabled button for the number <1> to <9>, <Delete>(NA21) , <0> and disabled <Decimal_Separator>. NA21, Delete button.LayersThe level of layers of all areas in window are in Layer 0.Echo TextsAn echo text is composed of Label Part and Data Part.The Label Part of an echo texts is same as The Label area of an input field.The echo texts are displayed in main area A, B, C and E with same order as their related input fields.The Label part of echo text is right aligned.The Data part of echo text is left aligned.The colour of texts in echo texts are grey.Entering CharactersThe cursor is flashed by changing from visible to not visible.The cursor is displayed as horizontal line below the value in the input field.Packet transmissionUse the log file to confirm that DMI received packet information [MMI_CURRENT_MAINTENANCE_DATA (EVC-40)] with variable MMI_Q_MD_DATASET = 1.The data part of input field and echo text are displayed correspond with the variables in received packet EVC-40 as follows,MMI_M_PULSE_PER_KM_1 = Radar 1MMI_M_PULSE_PULSE_PER_KM_2 = Radar 2General property of windowThe Radar window is presented with objects and buttons which is the one of several levels and allocated to areas of DMI. All objects, text messages and buttons are presented within the same layer.The Default window is not displayed and covered the current window
            Test Step Comment: (1) MMI_gen 11773;(2) MMI_gen 11771 (partly: MMI_gen 4888);(3) MMI_gen 11771 (partly: MMI_gen 4799 (partly: Close button, Window Title, Input fields)); MMI_gen 4392 (partly: [Close] NA11); MMI_gen 4355 (partly: Buttons, Close button); (4) MMI_gen 11771 (partly: MMI_gen 4891 (partly: Yes button, Area for [Window Title] Entry complete?));(5) MMI_gen 11771 (partly: MMI_gen 4910 (partly: Disabled, MMI_gen 4211 (partly: colour)), MMI_gen 4909 (partly: Disabled)); MMI_gen 4377 (partly: shown);(6) MMI_gen 11771 (partly: MMI_gen 4908 (partly: extended));(7) MMI_gen 11771 (partly: MMI_gen 4637 (partly: Main-areas D and F)); MMI_gen 4355 (partly: input fields);(8) MMI_gen 11771 (partly: MMI_gen 4640);(9) MMI_gen 11771 (partly: MMI_gen 4641);(10) MMI_gen 11771 (partly: MMI_gen 9412); MMI_gen 11774 (partly: label);(11) MMI_gen 11771 (partly: MMI_gen 4645);(12) MMI_gen 11771 (partly: MMI_gen 4646 (partly: right aligned));(13) MMI_gen 11771 (partly: MMI_gen 4647 (partly: left aligned));(14) MMI_gen 11771 (partly: MMI_gen 4648);(15) MMI_gen 11771 (partly: MMI_gen 4720);(16) MMI_gen 11771 (partly: MMI_gen 4651 (partly: Radar 1), MMI_gen 4683 (partly: selected), MMI_gen 5211 (partly: selected));(17) MMI_gen 11771 (partly: MMI_gen 4649 (partly: selected ‘Radar 1’), MMI_gen 4651 (partly: Radar 2), MMI_gen 4683 (partly: not selected), MMI_gen 5211 (partly: not selected));(18) MMI_gen 11784 (partly: Radar 1); MMI_gen 11771 (partly: MMI_gen 4912 (partly: Radar 1), MMI_gen 4678 (partly: Radar 1)); (19) MMI_gen 11771 (partly: MMI_gen 5003); MMI_gen 4392 (partly: [Delete] NA21);(20) MMI_gen 11771 (partly: MMI_gen 5190);(21) MMI_gen 11771 (partly: MMI_gen 4696);(22) MMI_gen 11771 (partly: MMI_gen 4697); (23) MMI_gen 11771 (partly: MMI_gen 4701);(24) MMI_gen 11771 (partly: MMI_gen 4702 (partly: right aligned));(25) MMI_gen 11771 (partly: MMI_gen 4704 (partly: left aligned));(26) MMI_gen 11771 (partly: MMI_gen 4700 (partly: otherwise, grey)); MMI_gen 4241;(27) MMI_gen 11771 (partly: MMI_gen 4691 (partly: flash, Radar 1));(28) MMI_gen 11771 (partly: MMI_gen 4689, MMI_gen 4690);(29) MMI_gen 11772;(30) MMI_gen 11775; MMI_gen 11783;(31) MMI_gen 4350;(32) MMI_gen 4351;(33) MMI_gen 4353;
            */
            DmiActions.ShowInstruction(this,
                "Press the ‘Maintenance’ button and enter the password from the PASS_CODE_MTN in the configuration file " +
                Environment.NewLine +
                "in the Password Maintenance window");

            EVC40_MMICurrentMaintenanceData.MMI_Q_MD_DATASET = Variables.MMI_Q_MD_DATASET.Doppler;
            EVC40_MMICurrentMaintenanceData.MMI_M_PULSE_PER_KM_1 = (Variables.MMI_M_PULSE_PER_KM) 22000;
            EVC40_MMICurrentMaintenanceData.MMI_M_PULSE_PER_KM_2 = (Variables.MMI_M_PULSE_PER_KM) 23000;
            EVC40_MMICurrentMaintenanceData.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. DMI displays the Radar window with the title ‘Radar’, right aligned." +
                                Environment.NewLine +
                                @"2  The Radar window displays the (enabled) ‘Close’ button, symbol NA11, two data input fields (on page 1), " +
                                Environment.NewLine +
                                @"   a ‘Yes’ button (disabled) with a label ‘Radar entry complete?’ in black on a Dark-grey Background colour with a border in Medium-grey (the same as the input field’s colour)." +
                                Environment.NewLine +
                                @"3. The sensitive area of the ‘Yes’ button includes the label." + Environment.NewLine +
                                @"4. The data input fields are displayed in areas D and F, with a Label Area and a Data Part Area." +
                                Environment.NewLine +
                                "5. The text in the Label area corresponds to the (current input field) i.e. Radar 1 pulses (1/km)." +
                                Environment.NewLine +
                                "6. The Label Area is to the left of The Data Area with right-aligned text in grey on a Dark-grey background. The data in the Data Part is aligned to the left." +
                                Environment.NewLine +
                                "7. There are only 2 input fields displayed in the first page of window." +
                                Environment.NewLine +
                                @"8. The first data input field is displayed ‘Selected’, with the value in black on a Medium-grey background." +
                                Environment.NewLine +
                                "9. All other input fields are ‘Not selected’ as follows, in grey on a Dark-grey background." +
                                "10. A numeric keypad is displayed for the data input field ‘Radar 1’, with enabled buttons for the numbers <1> to <9>,  a <Delete> button (symbol NA21), number <0> and the <Decimal_Separator>(disabled)." +
                                Environment.NewLine +
                                "11. All areas of the window are in Layer 0." + Environment.NewLine +
                                "12. Echo texts are displayed in areas A, B, C and E in the same order as their related input fields, with a Label Part and a Data Part." +
                                Environment.NewLine +
                                "13. The echo text Label Part is the same as The Label area of the corresponding data input field." +
                                Environment.NewLine +
                                "14. The echo text Label part is right-aligned. The echo text Data part is left-aligned." +
                                Environment.NewLine +
                                "15. Echo texts are in grey." + Environment.NewLine +
                                "16. A flashing (visible/invisible) underscore is displayed below the value in the input field." +
                                Environment.NewLine +
                                "17. Objects and buttons can be displayed in several levels. Within a level they are allocated to areas." +
                                Environment.NewLine +
                                "18. All objects, text messages and buttons are presented within the same layer." +
                                Environment.NewLine +
                                "19. The Default window is not displayed covering the current window." +
                                Environment.NewLine +
                                @"20. The value displayed in the Radar 1 data input field and echo text is ‘22000’." +
                                Environment.NewLine +
                                @"21. The value displayed in the Radar 2 data input field and echo text is ‘23000’.");

            /*
            Test Step 2
            Action: Press and hold ‘0’ button
            Expected Result: Verify the following information,The state of button is changed to ‘Pressed’ and immediately back to ‘Enabled’ state.The sound ‘Click’ is played once.The Input Field displays the value associated to the data key according to the pressings in state ‘Pressed’.The cursor is displayed as horizontal line below the value of the numeric-keyboard data key in the input field.The input field is used to enter the Radar 1 value
            Test Step Comment: (1) MMI_gen 11771 (partly: MMI_gen 4913 (partly: Radar 1), MMI_gen 4384 (partly: Change to state ‘Pressed’ and immediately back to state ‘Enabled’));   (2) MMI_gen 11771 (partly: MMI_gen 4913 (partly: Radar 1), MMI_gen 4384 (partly: sound ‘Click’)); MMI_gen 9512; MMI_gen 968;(3) MMI_gen 11771 (partly: MMI_gen 4679 (partly: Radar 1), MMI_gen 4913 (partly: Radar 1), MMI_gen 4384 (partly: ETCS-MMI’s function associated to the button));(4) MMI_gen 11771 (partly: MMI_gen 4689, MMI_gen 4690);(5) MMI_gen 11774 (partly: entry);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press and hold the ‘0’ button");

            // Spec says cursor is underneath the character entered but gen 4690 says under the next character to be entered...
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The button is displayed pressed and immedately re-displayed enabled." +
                                Environment.NewLine +
                                @"2. The ‘Click’ sound is played once." + Environment.NewLine +
                                "3. The Radar 1 data input field displays the value of the button pressed." +
                                Environment.NewLine +
                                "4. A cursor is displayed after the character just added to the data input field.");

            /*
            Test Step 3
            Action: Release the pressed button
            Expected Result: Verify the following information,The state of released button is changed to enabled
            Test Step Comment: (1) MMI_gen 11771 (partly: MMI_gen 4913 (partly: Radar 1), MMI_gen 4384 (partly: ETCS-MMI’s function associated to the button));
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Release the pressed button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The button is displayed enabled.");

            /*
            Test Step 4
            Action: Perform action step 2-3 for the ‘1’ to ‘9’ buttons.Note: Press the ‘Del’ button to delete an information when entered data is out of input field range is acceptable
            Expected Result: See the expected results of Step 2 – Step 3 and the following additional information,The pressed key is added in an input field immediately. The cursor is jumped to next position after entered the character immediately
            Test Step Comment: (1) MMI_gen 11771 (partly: MMI_gen 4642 (partly: Radar 1));  (2) MMI_gen 11771 (partly: MMI_gen 4692 (partly: Radar 1));  
            */
            // Repeat for the 1 button
            DmiActions.ShowInstruction(this, @"Press and hold the ‘1’ button");

            // Spec says cursor is underneath the character entered but gen 4690 says under the next character to be entered...
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The button is displayed pressed and immedately re-displayed enabled." +
                                Environment.NewLine +
                                @"2. The ‘Click’ sound is played once." + Environment.NewLine +
                                "3. The Radar 1 data input field displays the value of the button pressed." +
                                Environment.NewLine +
                                "4. A cursor is displayed after the character just added to the data input field.");

            DmiActions.ShowInstruction(this, @"Release the pressed button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The button is displayed enabled.");

            // Repeat for the 2 button
            DmiActions.ShowInstruction(this, @"Press and hold the ‘2’ button");

            // Spec says cursor is underneath the character entered but gen 4690 says under the next character to be entered...
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The button is displayed pressed and immedately re-displayed enabled." +
                                Environment.NewLine +
                                @"2. The ‘Click’ sound is played once." + Environment.NewLine +
                                "3. The Radar 1 data input field displays the value of the button pressed." +
                                Environment.NewLine +
                                "4. A cursor is displayed after the character just added to the data input field.");

            DmiActions.ShowInstruction(this, @"Release the pressed button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The button is displayed enabled.");

            // Repeat for the 3 button
            DmiActions.ShowInstruction(this, @"Press and hold the ‘3’ button");

            // Spec says cursor is underneath the character entered but gen 4690 says under the next character to be entered...
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The button is displayed pressed and immedately re-displayed enabled." +
                                Environment.NewLine +
                                @"2. The ‘Click’ sound is played once." + Environment.NewLine +
                                "3. The Radar 1 data input field displays the value of the button pressed." +
                                Environment.NewLine +
                                "4. A cursor is displayed after the character just added to the data input field.");

            DmiActions.ShowInstruction(this, @"Release the pressed button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The button is displayed enabled.");
            // Repeat for the 4 button
            DmiActions.ShowInstruction(this, @"Press and hold the ‘4’ button");

            // Spec says cursor is underneath the character entered but gen 4690 says under the next character to be entered...
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The button is displayed pressed and immedately re-displayed enabled." +
                                Environment.NewLine +
                                @"2. The ‘Click’ sound is played once." + Environment.NewLine +
                                "3. The Radar 1 data input field displays the value of the button pressed." +
                                Environment.NewLine +
                                "4. A cursor is displayed after the character just added to the data input field.");

            DmiActions.ShowInstruction(this, @"Release the pressed button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The button is displayed enabled.");

            // Repeat for the 5 button
            DmiActions.ShowInstruction(this, @"Press and hold the ‘5’ button");

            // Spec says cursor is underneath the character entered but gen 4690 says under the next character to be entered...
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The button is displayed pressed and immedately re-displayed enabled." +
                                Environment.NewLine +
                                @"2. The ‘Click’ sound is played once." + Environment.NewLine +
                                "3. The Radar 1 data input field displays the value of the button pressed." +
                                Environment.NewLine +
                                "4. A cursor is displayed after the character just added to the data input field.");

            DmiActions.ShowInstruction(this, @"Release the pressed button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The button is displayed enabled.");

            // Repeat for the 6 button
            DmiActions.ShowInstruction(this, @"Press and hold the ‘6’ button");

            // Spec says cursor is underneath the character entered but gen 4690 says under the next character to be entered...
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The button is displayed pressed and immedately re-displayed enabled." +
                                Environment.NewLine +
                                @"2. The ‘Click’ sound is played once." + Environment.NewLine +
                                "3. The Radar 1 data input field displays the value of the button pressed." +
                                Environment.NewLine +
                                "4. A cursor is displayed after the character just added to the data input field.");

            DmiActions.ShowInstruction(this, @"Release the pressed button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The button is displayed enabled.");

            // Repeat for the 7 button
            DmiActions.ShowInstruction(this, @"Press and hold the ‘7’ button");

            // Spec says cursor is underneath the character entered but gen 4690 says under the next character to be entered...
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The button is displayed pressed and immedately re-displayed enabled." +
                                Environment.NewLine +
                                @"2. The ‘Click’ sound is played once." + Environment.NewLine +
                                "3. The Radar 1 data input field displays the value of the button pressed." +
                                Environment.NewLine +
                                "4. A cursor is displayed after the character just added to the data input field.");

            DmiActions.ShowInstruction(this, @"Release the pressed button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The button is displayed enabled.");

            // Repeat for the 8 button
            DmiActions.ShowInstruction(this, @"Press and hold the ‘8’ button");

            // Spec says cursor is underneath the character entered but gen 4690 says under the next character to be entered...
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The button is displayed pressed and immedately re-displayed enabled." +
                                Environment.NewLine +
                                @"2. The ‘Click’ sound is played once." + Environment.NewLine +
                                "3. The Radar 1 data input field displays the value of the button pressed." +
                                Environment.NewLine +
                                "4. A cursor is displayed after the character just added to the data input field.");

            DmiActions.ShowInstruction(this, @"Release the pressed button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The button is displayed enabled.");

            // Repeat for the 9 button
            DmiActions.ShowInstruction(this, @"Press and hold the ‘9’ button");

            // Spec says cursor is underneath the character entered but gen 4690 says under the next character to be entered...
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The button is displayed pressed and immedately re-displayed enabled." +
                                Environment.NewLine +
                                @"2. The ‘Click’ sound is played once." + Environment.NewLine +
                                "3. The Radar 1 data input field displays the value of the button pressed." +
                                Environment.NewLine +
                                "4. A cursor is displayed after the character just added to the data input field.");

            DmiActions.ShowInstruction(this, @"Release the pressed button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The button is displayed enabled.");

            /*
            Test Step 5
            Action: Press and hold ‘Del’ button.Note: Stopwatch is required
            Expected Result: Verify the following information,While press and hold button less than 1.5 secSound ‘Click’ is played once.The state of button is changed to ‘Pressed’ and immediately back to ‘Enabled’ state.The last character is removed from an input field after pressing the button.While press and hold button over 1.5 secThe state ‘pressed’ and ‘released’ are switched repeatly while button is pressed and the characters are removed from an input field repeatly refer to pressed state.The sound ‘Click’ is played repeatly while button is pressed
            Test Step Comment: (1) MMI_gen 11771 (partly: MMI_gen 4913 (partly: Radar 1), MMI_gen 4384 (partly: sound ‘Click’)); MMI_gen 9512; MMI_gen 968;(2) MMI_gen 11771 (partly: MMI_gen 4913 (partly: Radar 1), MMI_gen 4384 (partly: Change to state ‘Pressed’ and immediately back to state ‘Enabled’));   (3) MMI_gen 11771 (partly: MMI_gen 4913 (partly: Radar 1), MMI_gen 4384 (partly: ETCS-MMI’s function associated to the button)); MMI_gen 4393 (partly: [Delete]);(4) MMI_gen 11771 (partly: MMI_gen 4913 (partly: Radar 1), MMI_gen 4386 (partly: visual of repeat function));(5) MMI_gen 11771 (partly: MMI_gen 4913 (partly: Radar 1), MMI_gen 4386 (partly: audible of repeat function));
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press and hold the ‘Del’ button. Note: Stopwatch is required");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The button is displayed pressed and immedately re-displayed enabled." +
                                Environment.NewLine +
                                @"2. The ‘Click’ sound is played once." + Environment.NewLine +
                                "3. The last character in the data input field is deleted." +
                                "4. After the button has been pressed for 1.5s, the button changes repeatedly between pressed and enabled." +
                                Environment.NewLine +
                                "5. The ‘Click’ sound is played repeatedly." + Environment.NewLine +
                                "6. The last character in the data input field is deleted repeatedly.");

            /*
            Test Step 6
            Action: Release ‘Del’ button
            Expected Result: Verify the following information, The character is stop removing
            Test Step Comment: (1) MMI_gen 11771 (partly: MMI_gen 4913 (partly: Radar 1)), MMI_gen 4384 (partly: ETCS-MMI’s function associated to the button));
            */
            DmiActions.ShowInstruction(this, @"Release the ‘Del’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Characters in the data input field are not deleted.");
            /*
            Test Step 7
            Action: Delete the old value and enter the value ‘20001 for Radar 1.Then, confirm an entered data by pressing an input field
            Expected Result: Verify the following information,Input fieldsThe associated ‘Enter’ button is data field itself.An input field is used to allow the driver to enter data.The state of ‘Radar 1’ input field is changed to ‘accepted’ as follows,The background colour of the Data Area is dark-grey.The colour of data value is white.The next input field ‘Radar 2’ is in state ‘selected’ as follows,The background colour of the Data Area is medium-grey.The colour of data value is black.Echo TextsThe echo text of ‘Radar 1’ is changed to white colour.The value of echo text is changed refer to entered data.Entering CharactersThe cursor is displayed as a horizontal line below the position of the next character to be entered.The cursor is flashed by changing from visible to not visible.KeyboardThe keyboard associated to selected input field ‘Radar 2’ is Numeric keyboard.The keyboard contains enabled button for the number <1> to <9>, <Delete>(NA21) , <0> and disabled <Decimal_Separator>. NA21, Delete button
            Test Step Comment: (1) MMI_gen 11771 (partly: MMI_gen 4682 (partly: Radar 1));(2) MMI_gen 11771 (partly: MMI_gen 4634 (partly: Radar 1)); MMI_gen 11774 (partly: entry);(3) MMI_gen 11771 (partly: MMI_gen 4652 (partly: Radar 1), MMI_gen 4684 (partly: accepted, Radar 1));(4) MMI_gen 11771 (partly: MMI_gen 4684 (partly: Radar 2, selected automatically), MMI_gen 4651 (partly: Radar 2));(5) MMI_gen 11771 (partly: MMI_gen 4700 (partly: Radar 1));(6) MMI_gen 11771 (partly: MMI_gen 4681 (partly: Radar 1), MMI_gen 4890, MMI_gen 4698);(7) MMI_gen 11771 (partly: MMI_gen 4689, MMI_gen 4690);(8) MMI_gen 11771 (partly: MMI_gen 4691 (partly: flash, Radar 2));(9) MMI_gen 11784 (partly: Radar 2); MMI_gen 11771 (partly: MMI_gen 4912 (partly: Radar 2), MMI_gen 4678 (partly: Radar 2));(10) MMI_gen 11771 (partly: MMI_gen 5003 (partly: Radar 2)); MMI_gen 4392 (partly: [Delete] NA21);
            */
            DmiActions.ShowInstruction(this,
                @"Delete the old value  for Radar 1 (if any), enter the value ‘20001’ and confirm the entered data by pressing the data input field");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The data input field acts as an ‘Enter’ button." + Environment.NewLine +
                                "2. Data can be entered in the data input field" + Environment.NewLine +
                                "3. The Radar 1 data input field is displayed accepted (the value is white on a Dark-grey background." +
                                Environment.NewLine +
                                "4. The Radar 2 data input field (the next field) is displayed selected (the value is black on a Medium-grey background" +
                                Environment.NewLine +
                                "5. The Radar 1 echo text displays the value of the data entered in white." +
                                Environment.NewLine +
                                "6. A flashing underscore (visible/invisible) is displayed after the last character entered." +
                                Environment.NewLine +
                                @"7. A numeric keypad is displayed for the data input field ‘Radar 2’, with enabled buttons for the numbers <1> to <9>,  a <Delete> button (symbol NA21), number <0> and the <Decimal_Separator>(disabled).");

            /*
            Test Step 8
            Action: Perform action step 2-6 for keypad of the ‘Radar 2’ input field
            Expected Result: See the expected results of Step 2 – Step 6 and the following additional information,The pressed key is added in an input field immediately. The cursor is jumped to next position after entered the character immediately
            Test Step Comment: (1) MMI_gen 11771 (partly: MMI_gen 4642 (partly: Radar 2));  (2) MMI_gen 11771 (partly: MMI_gen 4692 (partly: Radar 2));  
            */

            // The spec doesn't make sense: should be press and hold each button... after all buttons do Steps 5/6

            DmiActions.ShowInstruction(this, @"Press and hold the ‘0’ button");

            // Spec says cursor is underneath the character entered but gen 4690 says under the next character to be entered...
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The button is displayed pressed and immedately re-displayed enabled." +
                                Environment.NewLine +
                                @"2. The ‘Click’ sound is played once." + Environment.NewLine +
                                "3. The Radar 2 data input field displays the value of the button pressed." +
                                Environment.NewLine +
                                "4. A cursor is displayed after the character just added to the data input field.");

            DmiActions.ShowInstruction(this, @"Release the pressed button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The button is displayed enabled.");

            // Repeat for the 1 button
            DmiActions.ShowInstruction(this, @"Press and hold the ‘1’ button");

            // Spec says cursor is underneath the character entered but gen 4690 says under the next character to be entered...
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The button is displayed pressed and immedately re-displayed enabled." +
                                Environment.NewLine +
                                @"2. The ‘Click’ sound is played once." + Environment.NewLine +
                                "3. The Radar 2 data input field displays the value of the button pressed." +
                                Environment.NewLine +
                                "4. A cursor is displayed after the character just added to the data input field.");

            DmiActions.ShowInstruction(this, @"Release the pressed button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The button is displayed enabled.");

            // Repeat for the 1 button
            DmiActions.ShowInstruction(this, @"Press and hold the ‘1’ button");

            // Spec says cursor is underneath the character entered but gen 4690 says under the next character to be entered...
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The button is displayed pressed and immedately re-displayed enabled." +
                                Environment.NewLine +
                                @"2. The ‘Click’ sound is played once." + Environment.NewLine +
                                "3. The Radar 2 data input field displays the value of the button pressed." +
                                Environment.NewLine +
                                "4. A cursor is displayed after the character just added to the data input field.");

            DmiActions.ShowInstruction(this, @"Release the pressed button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The button is displayed enabled.");

            // Repeat for the 2 button
            DmiActions.ShowInstruction(this, @"Press and hold the ‘2’ button");

            // Spec says cursor is underneath the character entered but gen 4690 says under the next character to be entered...
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The button is displayed pressed and immedately re-displayed enabled." +
                                Environment.NewLine +
                                @"2. The ‘Click’ sound is played once." + Environment.NewLine +
                                "3. The Radar 2 data input field displays the value of the button pressed." +
                                Environment.NewLine +
                                "4. A cursor is displayed after the character just added to the data input field.");

            DmiActions.ShowInstruction(this, @"Release the pressed button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The button is displayed enabled.");

            // Repeat for the 3 button
            DmiActions.ShowInstruction(this, @"Press and hold the ‘3’ button");

            // Spec says cursor is underneath the character entered but gen 4690 says under the next character to be entered...
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The button is displayed pressed and immedately re-displayed enabled." +
                                Environment.NewLine +
                                @"2. The ‘Click’ sound is played once." + Environment.NewLine +
                                "3. The Radar 2 data input field displays the value of the button pressed." +
                                Environment.NewLine +
                                "4. A cursor is displayed after the character just added to the data input field.");

            DmiActions.ShowInstruction(this, @"Release the pressed button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The button is displayed enabled.");
            // Repeat for the 4 button
            DmiActions.ShowInstruction(this, @"Press and hold the ‘4’ button");

            // Spec says cursor is underneath the character entered but gen 4690 says under the next character to be entered...
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The button is displayed pressed and immedately re-displayed enabled." +
                                Environment.NewLine +
                                @"2. The ‘Click’ sound is played once." + Environment.NewLine +
                                "3. The Radar 2 data input field displays the value of the button pressed." +
                                Environment.NewLine +
                                "4. A cursor is displayed after the character just added to the data input field.");

            DmiActions.ShowInstruction(this, @"Release the pressed button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The button is displayed enabled.");

            // Repeat for the 5 button
            DmiActions.ShowInstruction(this, @"Press and hold the ‘5’ button");

            // Spec says cursor is underneath the character entered but gen 4690 says under the next character to be entered...
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The button is displayed pressed and immedately re-displayed enabled." +
                                Environment.NewLine +
                                @"2. The ‘Click’ sound is played once." + Environment.NewLine +
                                "3. The Radar 2 data input field displays the value of the button pressed." +
                                Environment.NewLine +
                                "4. A cursor is displayed after the character just added to the data input field.");

            DmiActions.ShowInstruction(this, @"Release the pressed button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The button is displayed enabled.");

            // Repeat for the 6 button
            DmiActions.ShowInstruction(this, @"Press and hold the ‘1’ button");

            // Spec says cursor is underneath the character entered but gen 4690 says under the next character to be entered...
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The button is displayed pressed and immedately re-displayed enabled." +
                                Environment.NewLine +
                                @"2. The ‘Click’ sound is played once." + Environment.NewLine +
                                "3. The Radar 2 data input field displays the value of the button pressed." +
                                Environment.NewLine +
                                "4. A cursor is displayed after the character just added to the data input field.");

            DmiActions.ShowInstruction(this, @"Release the pressed button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The button is displayed enabled.");

            // Repeat for the 7 button
            DmiActions.ShowInstruction(this, @"Press and hold the ‘7’ button");

            // Spec says cursor is underneath the character entered but gen 4690 says under the next character to be entered...
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The button is displayed pressed and immedately re-displayed enabled." +
                                Environment.NewLine +
                                @"2. The ‘Click’ sound is played once." + Environment.NewLine +
                                "3. The Radar 2 data input field displays the value of the button pressed." +
                                Environment.NewLine +
                                "4. A cursor is displayed after the character just added to the data input field.");

            DmiActions.ShowInstruction(this, @"Release the pressed button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The button is displayed enabled.");

            // Repeat for the 8 button
            DmiActions.ShowInstruction(this, @"Press and hold the ‘8’ button");

            // Spec says cursor is underneath the character entered but gen 4690 says under the next character to be entered...
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The button is displayed pressed and immedately re-displayed enabled." +
                                Environment.NewLine +
                                @"2. The ‘Click’ sound is played once." + Environment.NewLine +
                                "3. The Radar 2 data input field displays the value of the button pressed." +
                                Environment.NewLine +
                                "4. A cursor is displayed after the character just added to the data input field.");

            DmiActions.ShowInstruction(this, @"Release the pressed button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The button is displayed enabled.");

            // Repeat for the 9 button
            DmiActions.ShowInstruction(this, @"Press and hold the ‘9’ button");

            // Spec says cursor is underneath the character entered but gen 4690 says under the next character to be entered...
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The button is displayed pressed and immedately re-displayed enabled." +
                                Environment.NewLine +
                                @"2. The ‘Click’ sound is played once." + Environment.NewLine +
                                "3. The Radar 2 data input field displays the value of the button pressed." +
                                Environment.NewLine +
                                "4. A cursor is displayed after the character just added to the data input field.");

            DmiActions.ShowInstruction(this, @"Release the pressed button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The button is displayed enabled.");

            // Repeat Step 5
            DmiActions.ShowInstruction(this, @"Press and hold the ‘Del’ button. Note: Stopwatch is required");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The button is displayed pressed and immedately re-displayed enabled." +
                                Environment.NewLine +
                                @"2. The ‘Click’ sound is played once." + Environment.NewLine +
                                "3. The last character in the data input field is deleted." +
                                "4. After the button has been pressed for 1.5s, the button changes repeatedly between pressed and enabled." +
                                Environment.NewLine +
                                "5. The ‘Click’ sound is played repeatedly." + Environment.NewLine +
                                "6. The last character in the data input field is deleted repeatedly.");

            // Repeat Step 6
            DmiActions.ShowInstruction(this, @"Release the ‘Del’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Characters in the data input field are not deleted.");

            /*
            Test Step 9
            Action: Delete the old value and enter the value ‘20001’ for Radar 2.Then, confirm an entered data by pressing an input field
            Expected Result: Verify the following information,Input fieldsThe associated ‘Enter’ button is data field itself.An input field is used to allow the driver to enter data.The state of ‘Radar 2’ input field is changed to ‘accepted’ as follows,The background colour of the Data Area is dark-grey.The colour of data value is white.There is no input field selected.Echo TextsThe echo text of ‘Radar 2’ is changed to white colour.The value of echo text is changed refer to entered data.Data Entry windowThe state of ‘Yes’ button below text label ‘Train data Entry is complete?’ is enabled as follows,The background colour of the Data Area is medium-grey.The colour of data value is black.The colour of border is medium-grey
            Test Step Comment: (1) MMI_gen 11771 (partly: MMI_gen 4682 (partly: Radar 2));(2) MMI_gen 11771 (partly: MMI_gen 4634 (partly: Radar 2));(3) MMI_gen 11771 (partly: MMI_gen 4652 (partly: Radar 2), MMI_gen 4684 (partly: accepted, Radar 2));(4) MMI_gen 11771 (partly: MMI_gen 4684 (partly: No next input field, data entry process terminated));(5) MMI_gen 11771 (partly: MMI_gen 4700 (partly: Radar 2));(6) MMI_gen 11771 (partly: MMI_gen 4681 (partly: Radar 2), MMI_gen 4698, MMI_gen 4890);(7) MMI_gen 11771 (partly: MMI_gen 4909 (partly: Enabled), MMI_gen 4910 (partly: Enabled, MMI_gen 4211 (partly: colour))); MMI_gen 4374; 
            */
            DmiActions.ShowInstruction(this,
                @"Delete the old value  for Radar 2 (if any), enter the value ‘20001’ and confirm the entered data by pressing the data input field");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The data input field acts as an ‘Enter’ button." + Environment.NewLine +
                                "2. Data can be entered in the data input field" + Environment.NewLine +
                                "3. The Radar 2 data input field is displayed accepted (the value is white on a Dark-grey background." +
                                Environment.NewLine +
                                "4. The Radar 2 echo text displays the value of the data entered in white." +
                                Environment.NewLine +
                                @"5. The ‘Yes’ button beneath the label ‘Train data Entry is complete?’ is displayed enabled, with the text in white on a Medium-grey background, with a Medium-grey border.");

            /*
            Test Step 10
            Action: Perform the following procedure,Select ‘Radar 1 input field.Enter new value for ‘Radar 1’.Select ‘Radar 2’ input field
            Expected Result: Verify the following information,The state of ‘Yes’ button below text label ‘Radar entry is complete?’ is disabled. The state of input field ‘Radar 1’ is changed to ‘Not selected’ as follows,The value of ‘Radar 1’ input field is removed, display as blank.The background colour of the input field is dark-grey
            Test Step Comment: (1) MMI_gen 11771 (partly: MMI_gen 4909 (partly: state selected and with recently entered key), MMI_gen 4680 (partly: value has been modified));(2) MMI_gen 11771 (partly: MMI_gen 4680 (partly: Radar 1, Not selected, Data area is blank), MMI_gen 4649 (partly: data entry, background colour));
            */
            DmiActions.ShowInstruction(this,
                @"Select the Radar 1 data input field and enter a new value for Radar 1, then selected the Radar 2 data input field");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘Yes’ button beneath the label ‘Train data Entry is complete?’ is displayed disabled." +
                                Environment.NewLine +
                                "2. The Radar 1 data input field is displayed ‘Not selected’ with a blank value on a Dark-grey background.");

            /*
            Test Step 11
            Action: Confirm the value of ‘Radar 2’
            Expected Result: Verify the following information,The state of input field ‘Radar 1’ is changed to ‘Selected’
            Test Step Comment: (1) MMI_gen 11771 (partly: MMI_gen 4685);
            */
            DmiActions.ShowInstruction(this, @"Confirm the Radar 2 value");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The Radar 1 data input field is displayed ‘Selected’.");

            /*
            Test Step 12
            Action: Enter and confirm the value ‘20001’ for ‘Radar 1’ value.Press and hold ‘Yes’ button
            Expected Result: Verify the following information,The state of button is changed to ‘Pressed’, the border of button is removed.The sound ‘Click’ is played once
            Test Step Comment: (1) MMI_gen 11771 (partly: MMI_gen 4911 (partly: MMI_gen 4381 (partly: change to state ‘Pressed’ as long as remain actuated)); MMI_gen 4375;(2) MMI_gen 11771 (partly: MMI_gen 4911 (partly: MMI_gen 4381 (partly: sound ‘Click’))); MMI_gen 9512; MMI_gen 968;
            */
            DmiActions.ShowInstruction(this,
                @"Enter the value ‘20001’ for Radar 2 and confirm by pressing the data input field, then press and hold the ‘Yes’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘Yes’ button is displayed pressed, without a border." +
                                @"2. The ‘Click’ sound is played once.");

            /*
            Test Step 13
            Action: Slide out the ‘Yes’ button
            Expected Result: Verify the following information,The border of the input field is shown (state ‘Enabled’) without a sound
            Test Step Comment: (1) MMI_gen 11771 (partly: MMI_gen 4911 (partly: MMI_gen 4382 (partly: state ‘Enabled’ when slide out with force applied, no sound))); MMI_gen 4374;
            */
            DmiActions.ShowInstruction(this, "Whilst keeping the ‘Yes’ button pressed, drag it out of its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Yes’ button is displayed enabled, with a border." + Environment.NewLine +
                                "2. No sound is played.");

            /*
            Test Step 14
            Action: Slide back into the ‘Yes’ button
            Expected Result: Verify the following information,The button is back to state ‘Pressed’ without a sound
            Test Step Comment: (1) MMI_gen 11771 (partly: MMI_gen 4911 (partly: MMI_gen 4382 (partly: state ‘Pressed’ when slide back, no sound))); MMI_gen 4375;
            */
            DmiActions.ShowInstruction(this, "Whilst keeping the ‘Yes’ button pressed, drag it back inside its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Yes’ button is displayed pressed." + Environment.NewLine +
                                "2. No sound is played.");

            /*
            Test Step 15
            Action: Release ‘Yes’ button
            Expected Result: Verify the following information,DMI displays Radar validation window.Use the log file to confirm that DMI sent out packet [MMI_NEW_MAINTENANCE_DATA (EVC-140))] with following variablesMMI_Q_MD_DATASET = 1MMI_M_PULSE_PER_KM_1 = 20001MMI_M_PULSE_PER_KM_2 = 20001Use the log file to confirm that the Radar window is closed because of DMI received packet information [MMI_ECHOED_MAINTENANCE_DATA (EVC-41)]
            Test Step Comment: (1) MMI_gen 11771 (partly: MMI_gen 4911 (partly: MMI_gen 4381 (partly: exit state ‘pressed’), MMI_gen 4384 (partly: ETCS-MMI’s function associated to the button)));(2) MMI_gen 11785;(3) MMI_gen 11787;
            */
            DmiActions.ShowInstruction(this, @"Release the ‘Yes’ button");

            EVC140_MMINewMaintenanceData.MMI_Q_MD_DATASET = Variables.MMI_Q_MD_DATASET.Doppler;
            EVC140_MMINewMaintenanceData.MMI_M_PULSE_PER_KM_1 = (Variables.MMI_M_PULSE_PER_KM) 20001;
            EVC140_MMINewMaintenanceData.MMI_M_PULSE_PER_KM_2 = (Variables.MMI_M_PULSE_PER_KM) 20001;
            EVC140_MMINewMaintenanceData.CheckTelegram();

            // Open the Radar validation window
            EVC41_MMIEchoedMaintenanceData.MMI_M_PULSE_PER_KM_1_ = (Variables.MMI_M_PULSE_PER_KM) 20001;
            EVC41_MMIEchoedMaintenanceData.MMI_M_PULSE_PER_KM_2_ = (Variables.MMI_M_PULSE_PER_KM) 20001;
            EVC41_MMIEchoedMaintenanceData.MMI_Q_MD_DATASET_ = Variables.MMI_Q_MD_DATASET.Doppler;
            EVC41_MMIEchoedMaintenanceData.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Radar validation window");

            /*
            Test Step 16
            Action: Press ‘Yes’ button and confirm entered data by pressing an input field
            Expected Result: DMI displays the Maintenance window
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this,
                @"Press the ‘Yes’ button and confirm the entered data by pressing a data input field");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Maintenance window");

            /*
            Test Step 17
            Action: Perform the following procedure,Press ‘Radar’ button.Confirm the current data without re-entry Radar 1 and Radar 2.Press ‘Yes’ button.Press ‘Yes’ button and confirm entered data at Radar validation window
            Expected Result: Verify the following information,The first input field is used to revalidation the Radar 1
            Test Step Comment: (1) MMI_gen 11774 (partly: revalidation); 
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Radar’ button");

            EVC40_MMICurrentMaintenanceData.MMI_M_PULSE_PER_KM_1 = (Variables.MMI_M_PULSE_PER_KM) 20001;
            EVC40_MMICurrentMaintenanceData.MMI_M_PULSE_PER_KM_2 = (Variables.MMI_M_PULSE_PER_KM) 20001;
            EVC40_MMICurrentMaintenanceData.MMI_Q_MD_DATASET = Variables.MMI_Q_MD_DATASET.Doppler;
            EVC40_MMICurrentMaintenanceData.Send();

            DmiActions.ShowInstruction(this,
                @"Confirm the current data without re-entering Radar 1 or Radar 2 and press the ‘Yes’ button");

            EVC41_MMIEchoedMaintenanceData.MMI_M_PULSE_PER_KM_1_ = (Variables.MMI_M_PULSE_PER_KM) 20001;
            EVC41_MMIEchoedMaintenanceData.MMI_M_PULSE_PER_KM_2_ = (Variables.MMI_M_PULSE_PER_KM) 20001;
            EVC41_MMIEchoedMaintenanceData.MMI_Q_MD_DATASET_ = Variables.MMI_Q_MD_DATASET.Doppler;
            EVC41_MMIEchoedMaintenanceData.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The Radar 1 data input field is used to re-validate the Radar 1 value");

            DmiActions.ShowInstruction(this,
                @"Press the ‘Yes’ button to confirm the data in the Radar validation window");

            /*
            Test Step 18
            Action: Press ‘Radar’ button
            Expected Result: DMI displays Radar window
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press the ‘Radar’ button");

            EVC40_MMICurrentMaintenanceData.MMI_M_PULSE_PER_KM_1 = (Variables.MMI_M_PULSE_PER_KM) 20001;
            EVC40_MMICurrentMaintenanceData.MMI_M_PULSE_PER_KM_2 = (Variables.MMI_M_PULSE_PER_KM) 20001;
            EVC40_MMICurrentMaintenanceData.MMI_Q_MD_DATASET = Variables.MMI_Q_MD_DATASET.Doppler;
            EVC40_MMICurrentMaintenanceData.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Radar window");

            /*
            Test Step 19
            Action: Press and hold the Label area of ‘Radar 2’ input field
            Expected Result: Verify the following information,The state of ‘Radar 2’ input field is changed to ‘Pressed’, the border of button is removed.The state of ‘Radar 2’ input field remains ‘not selected’. The state of ‘Radar 1’ input field remains ‘selected’.The sound ‘Click’ is played once
            Test Step Comment: (1) MMI_gen 11771 (partly: MMI_gen 4686 (partly: Label area, Radar 2), MMI_gen 4381 (partly: change to state ‘Pressed’ as long as remain actuated))); MMI_gen 4392 (partly: [Enter], touch screen); MMI_gen 4375;(2) MMI_gen 11771 (partly: MMI_gen 4686 (partly: Label part, Radar 2), MMI_gen 4381 (partly: the sound for Up-Type button));
            */
            DmiActions.ShowInstruction(this, @"Press and hold the Label area of the Radar 2 data input field");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The data input field is displayed pressed, without a border." +
                                Environment.NewLine +
                                "2. No sound is played." + Environment.NewLine +
                                @"3. The Radar 2 data input field stays ‘Not Selected’." + Environment.NewLine +
                                @"4. The Radar 1 data input field stays ‘Selected’.");

            /*
            Test Step 20
            Action: Slide out the Label areaof ‘Radar 2’ input field
            Expected Result: Verify the following information,The border of ‘Radar 2’ input field is shown (state ‘Enabled’) without a sound.The state of ‘Radar 2’ input field remains ‘not selected’. The state of ‘Radar 1’ input field remains ‘selected’
            Test Step Comment: (1) MMI_gen 11771 (partly: MMI_gen 4686 (partly: Label area, Radar 2), MMI_gen 4382 (partly: state ‘Enabled’ when slide out with force applied, no sound); MMI_gen 4374;
            */
            DmiActions.ShowInstruction(this,
                "Whilst keeping the Radar 2 data input field pressed, drag it out of its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The data input field is displayed enabled, with a border." + Environment.NewLine +
                                "2. No sound is played." + Environment.NewLine +
                                @"3. The Radar 2 data input field stays ‘Not Selected’." + Environment.NewLine +
                                @"4. The Radar 1 data input field stays ‘Selected’.");

            /*
            Test Step 21
            Action: Slide back into the Label area of ‘Radar 2’ input field
            Expected Result: Verify the following information,The state of ‘Radar 2’ input field is changed to ‘Pressed’, the border of button is removed.The state of ‘Radar 2’ input field remains ‘not selected’. The state of ‘Radar 1’ input field remains ‘selected’
            Test Step Comment: (1) MMI_gen 11771 (partly: MMI_gen 4686 (partly: Label area, Radar 2), MMI_gen 4382 (partly: state ‘Pressed’ when slide back, no sound); MMI_gen 4375;
            */
            DmiActions.ShowInstruction(this,
                "Whilst keeping the Radar 2 data input field pressed, drag it back inside its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The data input field is displayed pressed, without a border." +
                                Environment.NewLine +
                                @"2. The Radar 2 data input field stays ‘Not Selected’." + Environment.NewLine +
                                @"3. The Radar 1 data input field stays ‘Selected’.");

            /*
            Test Step 22
            Action: Release the pressed area
            Expected Result: Verify the following information,The state of ‘Radar 2’ input field is changed to selected
            Test Step Comment: (1) MMI_gen 11771 (partly: MMI_gen 4686 (partly: Label area, Radar 2), MMI_gen 4381 (partly: exit state ‘Pressed’, execute function associated to the button)); MMI_gen 4374;
            */
            DmiActions.ShowInstruction(this, @"Release the Radar 2 data input field");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The Radar 2 data input field is displayed ‘Selected’.");

            /*
            Test Step 23
            Action: Perform action step 19-22 for the Label area of Radar 1 input field
            Expected Result: Verify the following information,The state of an input field is changed to ‘selected’ when release the pressed area at the Label part of input field
            Test Step Comment: (1) MMI_gen 11771 (partly: MMI_gen 4686 (partly: Label area));
            */
            // Repeat Step 19 for Radar 1 data input field
            DmiActions.ShowInstruction(this, @"Press and hold the Label area of the Radar 1 data input field");

            // Repeat Step 20 for Radar 1 data input field
            DmiActions.ShowInstruction(this,
                "Whilst keeping the Label area of the Radar 1 data input field pressed, drag it out of its area");


            // Repeat Step 21 for Radar 1 data input field
            DmiActions.ShowInstruction(this,
                "Whilst keeping the Label area of the Radar 1 data input field pressed, drag it back inside its area");

            DmiActions.ShowInstruction(this, @"Release the Label area of the Radar 1 data input field");

            // Repeat Step 22 for Radar 1 data input field
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The Radar 1 data input field is displayed ‘Selected’.");

            /*
            Test Step 24
            Action: Perform action step 19-22 for the Data area of each input field
            Expected Result: Verify the following information,The state of an input field is changed to ‘selected’ when release the pressed area at the Data area of input field
            Test Step Comment: (1) MMI_gen 11771 (partly: MMI_gen 4686 (partly: Data area)); MMI_gen 9390 (partly: Radar window);
            */
            // Repeat Step 19 for Radar 1 data input field Data area
            DmiActions.ShowInstruction(this, @"Press and hold the Data area of the Radar 1 data input field");

            // Repeat Step 20 for Radar 1 data input field Data area
            DmiActions.ShowInstruction(this,
                "Whilst keeping the Data area of the Radar 1 data input field pressed, drag it out of its area");

            // Repeat Step 21 for Radar 1 data input field Data area
            DmiActions.ShowInstruction(this,
                "Whilst keeping the Data area of the Radar 1 data input field pressed, drag it back inside its area");

            // Repeat Step 22 for Radar 1 data input field Data area
            DmiActions.ShowInstruction(this, @"Release the Data area of the Radar 1 data input field");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The Radar 1 data input field is displayed ‘Selected’.");

            // Repeat Step 19 for Radar 2 data input field Data area
            DmiActions.ShowInstruction(this, @"Press and hold the Data area of the Radar 2 data input field");

            // Repeat Step 20 for Radar 2 data input field Data area
            DmiActions.ShowInstruction(this,
                "Whilst keeping the Data area of the Radar 2 data input field pressed, drag it out of its area");

            // Repeat Step 21 for Radar 2 data input field Data area
            DmiActions.ShowInstruction(this,
                "Whilst keeping the Data area of the Radar 2 data input field pressed, drag it back inside its area");

            // Repeat Step 22 for Radar 2 data input field Data area
            DmiActions.ShowInstruction(this, @"Release the Data area of the Radar 2 data input field");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The Radar 2 data input field is displayed ‘Selected’.");

            /*
            Test Step 25
            Action: Press ‘Close’ button
            Expected Result: Verify the following information,Use the log file to confirm that DMI sent out packet [MMI_DRIVER_REQUEST (EVC-101)] with variable MMI_M_REQUEST = 54 (Exit Maintenance).The window is closed and the Maintenance window is displayed
            Test Step Comment: (1) MMI_gen 11786 (partly: EVC-101);(2) MMI_gen 11786 (partly: closure); MMI_gen 4392 (partly: returning to the parent window);
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button");

            EVC101_MMIDriverRequest.CheckMRequestReleased = Variables.MMI_M_REQUEST.ExitMaintenance;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI closes the window and displays the Maintenance window");
            /*
            Test Step 26
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
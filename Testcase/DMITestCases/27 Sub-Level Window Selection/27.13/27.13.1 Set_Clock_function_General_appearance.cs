using System;
using Testcase.Telegrams.EVCtoDMI;


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
    public class TC_ID_22_13_1_Set_Clock_function_General_appearance : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:

            // Call the TestCaseBase PreExecution
            base.PreExecution();

            // Test system is power on.Cabin is activateSettings window is opened. 
            DmiActions.Start_ATP();
            DmiActions.Activate_Cabin_1(this);
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Default;
            EVC30_MMIRequestEnable.Send();
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
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 0;
            // Testcase entrypoint


            TraceHeader("Test Step 1");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Use the test script file 22_13_1_a.xml to send EVC-30 with,MMI_NID_WINDOW = 4MMI_Q_REQUEST_ENABLE_64 (#25) = 0MMI_Q_REQUEST_ENABLE_64 (#26) = 0");
            TraceReport("Expected Result");
            TraceInfo("The Set clock button is disabled");
            /*
            Test Step 1
            Action: Use the test script file 22_13_1_a.xml to send EVC-30 with,MMI_NID_WINDOW = 4MMI_Q_REQUEST_ENABLE_64 (#25) = 0MMI_Q_REQUEST_ENABLE_64 (#26) = 0
            Expected Result: The Set clock button is disabled
            Test Step Comment: MMI_gen 1563         (partly: disabled);             
            */
            XML_22_13_1(msgType.typea);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Set’ clock button is disabled");

            TraceHeader("Test Step 2");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Perform the following perocedure,Deactivate cabin.Use the test script file 22_13_1_d.xml to send EVC-30 with,MMI_NID_WINDOW = 4MMI_Q_REQUEST_ENABLE_64 (#25) = 1MMI_Q_REQUEST_ENABLE_64 (#26) = 1Activate Cabin");
            TraceReport("Expected Result");
            TraceInfo("The Set clock button is still disabled");
            /*
            Test Step 2
            Action: Perform the following perocedure,Deactivate cabin.Use the test script file 22_13_1_d.xml to send EVC-30 with,MMI_NID_WINDOW = 4MMI_Q_REQUEST_ENABLE_64 (#25) = 1MMI_Q_REQUEST_ENABLE_64 (#26) = 1Activate Cabin
            Expected Result: The Set clock button is still disabled
            Test Step Comment: MMI_gen 1563 (partly: NEGATIVE, unfulfill condition);
            */
            DmiActions.Deactivate_Cabin(this);

            XML_22_13_1(msgType.typed);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Set’ clock button stays disabled");

            DmiActions.Activate_Cabin_1(this);

            TraceHeader("Test Step 3");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Use the test script file 22_13_1_d.xml to send EVC-30 with,MMI_NID_WINDOW = 4MMI_Q_REQUEST_ENABLE_64 (#25) = 1MMI_Q_REQUEST_ENABLE_64 (#26) = 1");
            TraceReport("Expected Result");
            TraceInfo("The Set clock button is enabled");
            /*
            Test Step 3
            Action: Use the test script file 22_13_1_d.xml to send EVC-30 with,MMI_NID_WINDOW = 4MMI_Q_REQUEST_ENABLE_64 (#25) = 1MMI_Q_REQUEST_ENABLE_64 (#26) = 1
            Expected Result: The Set clock button is enabled
            Test Step Comment: MMI_gen 1563         (partly: enabled);             
            */
            XML_22_13_1(msgType.typed);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Set’ clock button is enabled");

            TraceHeader("Test Step 4");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Press ‘Set Clock’ button");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following information,DMI displays Set Clock window.Data entry windowThe window title is ‘Set clock’.The text label on window title is included with sequence number of the current window (e.g. ‘(1/2)’).The text label of the window title is right aligned.The following objects are displayed in Set Clock window,  Enabled Close button (NA11) Disabled Previous button (NA19)  Enabled Next button (NA17)Window TitleInput fieldsThe following objects are additionally displayed in Train data window,Yes buttonThe text label ‘Clock set?’Yes button is displayed in Disabled state as follows,Text label is black Background colour is dark-greyThe border colour is medium-grey the same as the input field’s colour.The sensitive area of Yes button is extended from text label ‘Clock set?’Input fieldsThe input fields are located on Main area D and F.Each input field is devided into a Label Area and a Data Area.The Label Area is give the topic of the input field.The Label Area text is displayed corresponding to the input field i.e. Year, Month and Day.The Label Area is placed to the left of The Data Area.The text in the Label Area is aligned to the right.The value of data in the Data Area is aligned to the left.The text colour of the Label Area is grey and the background colour of the Label Area is dark-grey.There are only 3 input fields display in the first page of window.The first input field is in state ‘Selected’ as follows,The background colour of the Data Area is medium-grey.The colour of data value is black.All other input fields are in state ‘Not selected’ as follows,The background colour of the Data Area is dark-grey.The colour of data value is grey.The label of 1st input field is ‘Year’.The label of 2nd input field is ‘Month’.The label of 3rd input field is ‘Day’.KeyboardThe keyboard associated to selected input field ‘Year’ is Numeric keyboard.The keyboard contains enabled button for the number <1> to <9>, <Delete>(NA21) , <0> and disabled <Decimal_Separator>. NA21, Delete button.LayersThe level of layers of all areas in window are in Layer 0.Echo TextsAn echo text is composed of Label Part and Data Part.The echo texts are displayed in main area A, B, C and E with same order as their related input fields.The Label part of echo text is right aligned.The Data part of echo text is left aligned.The colour of texts in echo texts are grey.The format of UTC time and date is ‘UTC YYYY-MM-DD hh:mm:ss’The format of Local time and date is ‘Local YYYY-MM-DD hh:mm:ss’The date and time are updated every second.Note: Stopwatch is required.Entering CharactersThe cursor is flashed by changing from visible to not visible.The cursor is displayed as horizontal line below the value in the input field.General property of windowThe Set Clock window is presented with objects, text messages and buttons which is the one of several levels and allocated to areas of DMI. All objects, text messages and buttons are presented within the same layer.The Default window is not displayed and covered the current window");
            /*
            Test Step 4
            Action: Press ‘Set Clock’ button
            Expected Result: Verify the following information,DMI displays Set Clock window.Data entry windowThe window title is ‘Set clock’.The text label on window title is included with sequence number of the current window (e.g. ‘(1/2)’).The text label of the window title is right aligned.The following objects are displayed in Set Clock window,  Enabled Close button (NA11) Disabled Previous button (NA19)  Enabled Next button (NA17)Window TitleInput fieldsThe following objects are additionally displayed in Train data window,Yes buttonThe text label ‘Clock set?’Yes button is displayed in Disabled state as follows,Text label is black Background colour is dark-greyThe border colour is medium-grey the same as the input field’s colour.The sensitive area of Yes button is extended from text label ‘Clock set?’Input fieldsThe input fields are located on Main area D and F.Each input field is devided into a Label Area and a Data Area.The Label Area is give the topic of the input field.The Label Area text is displayed corresponding to the input field i.e. Year, Month and Day.The Label Area is placed to the left of The Data Area.The text in the Label Area is aligned to the right.The value of data in the Data Area is aligned to the left.The text colour of the Label Area is grey and the background colour of the Label Area is dark-grey.There are only 3 input fields display in the first page of window.The first input field is in state ‘Selected’ as follows,The background colour of the Data Area is medium-grey.The colour of data value is black.All other input fields are in state ‘Not selected’ as follows,The background colour of the Data Area is dark-grey.The colour of data value is grey.The label of 1st input field is ‘Year’.The label of 2nd input field is ‘Month’.The label of 3rd input field is ‘Day’.KeyboardThe keyboard associated to selected input field ‘Year’ is Numeric keyboard.The keyboard contains enabled button for the number <1> to <9>, <Delete>(NA21) , <0> and disabled <Decimal_Separator>. NA21, Delete button.LayersThe level of layers of all areas in window are in Layer 0.Echo TextsAn echo text is composed of Label Part and Data Part.The echo texts are displayed in main area A, B, C and E with same order as their related input fields.The Label part of echo text is right aligned.The Data part of echo text is left aligned.The colour of texts in echo texts are grey.The format of UTC time and date is ‘UTC YYYY-MM-DD hh:mm:ss’The format of Local time and date is ‘Local YYYY-MM-DD hh:mm:ss’The date and time are updated every second.Note: Stopwatch is required.Entering CharactersThe cursor is flashed by changing from visible to not visible.The cursor is displayed as horizontal line below the value in the input field.General property of windowThe Set Clock window is presented with objects, text messages and buttons which is the one of several levels and allocated to areas of DMI. All objects, text messages and buttons are presented within the same layer.The Default window is not displayed and covered the current window
            Test Step Comment: (1) MMI_gen 11939;(2) MMI_gen 11941; MMI_gen 4355 (partly: Window title);   (3) MMI_gen 4360 (partly: total number of window);(4) MMI_gen 11942 (partly: MMI_gen 4888);(5) MMI_gen 11942 (partly: MMI_gen 4799 (partly: Close button, Previous button, Next button, Window Title, Input fields)); MMI_gen 4392 (partly: [Previous : NA19], [Next: NA17], [Close] NA11); MMI_gen 4355 (partly: Buttons, Close button); MMI_gen 4396 (partly: Previous, NA19); MMI_gen 4394 (partly: disabled [previous]); MMI_gen 4358;(6) MMI_gen 11942 (partly: MMI_gen 4891 (partly: Yes button, Area for [Window Title] Entry complete?), question ‘Clock set?);(7) MMI_gen 11942 (partly: MMI_gen 4910 (partly: Disabled, MMI_gen 4211 (partly: colour)), MMI_gen 4909 (partly: Disabled)); MMI_gen 4377 (partly: shown);(8) MMI_gen 11942 (partly: MMI_gen 4908 (partly: extended));(9) MMI_gen 11942 (partly: MMI_gen 4640);(10 MMI_gen 11942 (partly: MMI_gen 4640);(11) MMI_gen 11942 (partly: MMI_gen 4641);(12) MMI_gen 11942 (partly: MMI_gen 9412);(13) MMI_gen 11942 (partly: MMI_gen 4645);(14) MMI_gen 11942 (partly: MMI_gen 4646 (partly: right aligned));(15) MMI_gen 11942 (partly: MMI_gen 4647 (partly: left aligned));(16) MMI_gen 11942 (partly: MMI_gen 4648);(17) MMI_gen 11942 (partly: MMI_gen 4720); MMI_gen 12125 (partly: page 1);(18) MMI_gen 11942 (partly: MMI_gen 4651 (partly: Train category), MMI_gen 4683 ( partly: selected), MMI_gen 5211 (partly: selected));(19) MMI_gen 11942 (partly: MMI_gen 4649 (partly: selected ‘Year’), MMI_gen 4651 (partly: Month, Day), MMI_gen 4683 (partly: not selected), MMI_gen 5211 (partly: not selected));(20) MMI_gen 11951 (partly: label);(21) MMI_gen 11952 (partly: label);    (22) MMI_gen 11953 (partly: label);(23) MMI_gen 11944 (partly: Year); MMI_gen 11942 (partly: MMI_gen 4912 (partly: Year), MMI_gen 4678 (partly: Year));(24) MMI_gen 11942 (partly: MMI_gen 5003 (partly: Year)); MMI_gen 4392 (partly: [Delete] NA21);(25) MMI_gen 11942 (partly: MMI_gen 5190);(26) MMI_gen 11942 (partly: MMI_gen 4696);(27) MMI_gen 11942 (partly: MMI_gen 4701);(28) MMI_gen 11942 (partly: MMI_gen 4702 (partly: right aligned));(29) MMI_gen 11942 (partly: MMI_gen 4704 (partly: left aligned));(30) MMI_gen 11942 (partly: MMI_gen 4700 (partly: otherwise, grey)); MMI_gen 4241;(31) MMI_gen 12127;(32) MMI_gen 12128;(33) MMI_gen 11946 (partly: updated);(34) MMI_gen 11942 (partly: MMI_gen 4691 (partly: flash, Year));(35) MMI_gen 11942 (partly: MMI_gen 4689, MMI_gen 4690);(36) MMI_gen 4350;(37) MMI_gen 4351;(38) MMI_gen 4353;
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Set clock’ button");

            // gen 4690 says the cursor is to the right of the characters
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Set clock window with the title (right-aligned) which includes the sequence number of the current window, e.g ‘Set clock(1/2)’" +
                                Environment.NewLine +
                                "2. The window title  (e.g. )." + Environment.NewLine +
                                "3. The Set Clock window displays: an enabled Close button (symbol NA11), a disabled Previous button (symbol NA19); " +
                                Environment.NewLine +
                                @"                                  data input fields, a ‘Yes’ button, and a ‘Clock set ?’ label." +
                                Environment.NewLine +
                                @"4. The Yes’ button is displayed disabled state with black text on a Dark-grey background, with a Medium-grey border." +
                                Environment.NewLine +
                                @"5. The sensitive area of the Yes’ button is extends from the ‘Clock set ?’ label." +
                                Environment.NewLine +
                                "6. The data input fields are displayed in areas D and F." + Environment.NewLine +
                                "7. Data input fields have a Label Area and a Data Area." + Environment.NewLine +
                                "8. The Label Area text is displayed with its corresponding data input field i.e. Year, Month and Day." +
                                Environment.NewLine +
                                "9. The Label Area is displayed to the left of The Data Area with right-aligned text in grey on a Dark-grey background." +
                                Environment.NewLine +
                                "10. The value in the Data Area is left-aligned in black." + Environment.NewLine +
                                "11. 3 data input fields are displayed in the first page of the window." +
                                Environment.NewLine +
                                @"12. The first input field is ‘Selected’ with black text on a Meduium-grey background." +
                                Environment.NewLine +
                                @"13. The other input fields are ‘Not selected’ with data values in grey on a Dark-grey background." +
                                Environment.NewLine +
                                @"14. The 1st data input field is labelled ‘Year’." + Environment.NewLine +
                                @"15. The 2nd data input field label of is ‘Month’." + Environment.NewLine +
                                @"16. The 3rd data input fieldlabel of  is ‘Day’." + Environment.NewLine +
                                @"16. A numeric keypad is displayed for the ‘Year’ data input field." +
                                Environment.NewLine +
                                "17. The keypad contains enabled keys for the numbers <1> to <9>, <Delete> (symbol NA21), <0> and a disabled <Decimal_Separator> key." +
                                Environment.NewLine +
                                "18. All areas of the window are in Layer 0." + Environment.NewLine +
                                "19. Echo texts are displayed in areas A, B, C and E in the same order as their corresponding data input fields." +
                                Environment.NewLine +
                                "20. Echo texts have a Label Part with right-aligned text and a Data Part with left-aligned text in grey." +
                                Environment.NewLine +
                                "21. The UTC Time and date is in ‘UTC YYYY - MM - DD hh:mm:ss’ format." +
                                Environment.NewLine +
                                "22. The Local time and date is in ‘Local YYYY - MM - DD hh:mm:ss’ format." +
                                Environment.NewLine +
                                "23. The date and time are updated every second." + Environment.NewLine +
                                "24. A flashing (visible/invisible) underscore is displayed as a cursor to the right of the last character entered in the data input field." +
                                Environment.NewLine +
                                "26. Objects, text messages and buttons can be displayed in several levels. Within a level they are allocated to areas." +
                                Environment.NewLine +
                                "27. All objects, text messages and buttons are displayed in the same layer." +
                                Environment.NewLine +
                                "28. The Default window is not displayed covering the current window.");

            TraceHeader("Test Step 5");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Press and hold ‘0’ button");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following information,The state of ‘0‘ button is changed to ‘Pressed’ and immediately back to ‘Enabled’ state.The sound ‘Click’ is played once.The Input Field displays the value associated to the data key according to the pressings in state ‘Pressed’.The cursor is displayed as horizontal line below the value of the numeric-keyboard data key in the input field.The input field is used to enter the Year.The local time is editable refer to received packet EVC-30 with the one of following condition is true,MMI_Q_REQUEST_ENABLE_64 #25 = 1MMI_Q_REQUEST_ENABLE_64 #26 = 1 The echo text of date and time are stop updating refer to driver modification");
            /*
            Test Step 5
            Action: Press and hold ‘0’ button
            Expected Result: Verify the following information,The state of ‘0‘ button is changed to ‘Pressed’ and immediately back to ‘Enabled’ state.The sound ‘Click’ is played once.The Input Field displays the value associated to the data key according to the pressings in state ‘Pressed’.The cursor is displayed as horizontal line below the value of the numeric-keyboard data key in the input field.The input field is used to enter the Year.The local time is editable refer to received packet EVC-30 with the one of following condition is true,MMI_Q_REQUEST_ENABLE_64 #25 = 1MMI_Q_REQUEST_ENABLE_64 #26 = 1 The echo text of date and time are stop updating refer to driver modification
            Test Step Comment: (1) MMI_gen 11942 (partly: MMI_gen 4913 (partly: Year); MMI_gen 4384 (partly: Change to state ‘Pressed’ and immediately back to state ‘Enabled’);   (2) MMI_gen 11942 (partly: MMI_gen 4913 (partly: Year); MMI_gen 4384 (partly: sound ‘Click’); MMI_gen 9512; MMI_gen 968;(3) MMI_gen 11942 partly: MMI_gen 4679 (partly: Year), MMI_gen 4913 (partly: RBC ID), MMI_gen 4384 (partly: ETCS-MMI’s function associated to the button));(4) MMI_gen 11942 (partly: MMI_gen 4689, MMI_gen 4690);(5) MMI_gen 11951 (partly: entry);(6) MMI_gen 178; (7) MMI_gen 11946 (partly: driver modified);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press and hold the ‘0’ key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘0’ key is displayed pressed and immediately re-displayed enabled." +
                                Environment.NewLine +
                                @"2. The ‘Click’ sound is played once." + Environment.NewLine +
                                @"3. The ‘Year’ data input field displays ‘0’." + Environment.NewLine +
                                "4. The cursor is displayed after the ‘0’." + Environment.NewLine +
                                "5. The data input field is used to enter the ‘Year’." + Environment.NewLine +
                                "6. The local time can be edited." + Environment.NewLine +
                                "7. The date and time echo texts stop being updated.");

            TraceHeader("Test Step 6");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Release pressed button");
            TraceReport("Expected Result");
            TraceInfo("Verify the following information,The state of released button is changed to enabled");
            /*
            Test Step 6
            Action: Release pressed button
            Expected Result: Verify the following information,The state of released button is changed to enabled
            Test Step Comment: (1) MMI_gen 11942 (partly: MMI_gen 4913 (partly: Year); MMI_gen 4384 (partly: ETCS-MMI’s function associated to the button);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Release the key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘0’ key is displayed enabled.");

            TraceHeader("Test Step 7");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Perform action step 3-4 for the ‘1’ to ‘9’ buttons.Note: Press the ‘Del’ button to delete an information when entered data is out of input field range is acceptable");
            TraceReport("Expected Result");
            TraceInfo(
                "See the expected results of Step 3 – Step 4 and the following additional information,(1)   The pressed key is added in an input field immediately. (2)   The cursor is jumped to next position after entered the character immediately");
            /*
            Test Step 7
            Action: Perform action step 3-4 for the ‘1’ to ‘9’ buttons.Note: Press the ‘Del’ button to delete an information when entered data is out of input field range is acceptable
            Expected Result: See the expected results of Step 3 – Step 4 and the following additional information,(1)   The pressed key is added in an input field immediately. (2)   The cursor is jumped to next position after entered the character immediately
            Test Step Comment: (1) MMI_gen 11942 (partly: MMI_gen 4642 (partly: Year));  (2) MMI_gen 11942 (partly: MMI_gen 4692 (partly: Year));  
            */
            // Surely steps 5 and 6
            // Repeat for the <1> key
            DmiActions.ShowInstruction(this, @"Press and hold the ‘1’ key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘0’ key is displayed pressed and immediately re-displayed enabled." +
                                Environment.NewLine +
                                @"2. The ‘Click’ sound is played once." + Environment.NewLine +
                                @"3. The ‘Year’ data input field displays ‘01’." + Environment.NewLine +
                                "4. The cursor is displayed after the ‘1’." + Environment.NewLine +
                                "5. The data input field is used to enter the ‘Year’." + Environment.NewLine +
                                "6. The local time can be edited." + Environment.NewLine +
                                "7. The date and time echo texts are not updated.");
            DmiActions.ShowInstruction(this, @"Release the key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘1’ key is displayed enabled.");

            // Repeat for the <2> key
            DmiActions.ShowInstruction(this, @"Press and hold the ‘2’ key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘2’ key is displayed pressed and immediately re-displayed enabled." +
                                Environment.NewLine +
                                @"2. The ‘Click’ sound is played once." + Environment.NewLine +
                                @"3. The ‘Year’ data input field displays ‘012’." + Environment.NewLine +
                                "4. The cursor is displayed after the ‘2’." + Environment.NewLine +
                                "5. The data input field is used to enter the ‘Year’." + Environment.NewLine +
                                "6. The local time can be edited." + Environment.NewLine +
                                "7. The date and time echo texts are not updated.");

            DmiActions.ShowInstruction(this, @"Release the key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘2’ key is displayed enabled.");

            // Repeat for the <3> button
            DmiActions.ShowInstruction(this, @"Press and hold the ‘3’ key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘3’ key is displayed pressed and immediately re-displayed enabled." +
                                Environment.NewLine +
                                @"2. The ‘Click’ sound is played once." + Environment.NewLine +
                                @"3. The ‘Year’ data input field displays ‘0123’." + Environment.NewLine +
                                "4. The cursor is displayed after the ‘3’." + Environment.NewLine +
                                "5. The data input field is used to enter the ‘Year’." + Environment.NewLine +
                                "6. The local time can be edited." + Environment.NewLine +
                                "7. The date and time echo texts are not updated.");

            DmiActions.ShowInstruction(this, @"Release the key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘3’ key is displayed enabled.");

            DmiActions.ShowInstruction(this, @"Press the <Delete> key");

            // Repeat for the <4> button
            DmiActions.ShowInstruction(this, @"Press and hold the ‘4’ key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘3’ key is displayed pressed and immediately re-displayed enabled." +
                                Environment.NewLine +
                                @"2. The ‘Click’ sound is played once." + Environment.NewLine +
                                @"3. The ‘Year’ data input field displays ‘0124’." + Environment.NewLine +
                                "4. The cursor is displayed after the ‘4’." + Environment.NewLine +
                                "5. The data input field is used to enter the ‘Year’." + Environment.NewLine +
                                "6. The local time can be edited." + Environment.NewLine +
                                "7. The date and time echo texts are not updated.");

            DmiActions.ShowInstruction(this, @"Release the key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘4’ key is displayed enabled.");

            DmiActions.ShowInstruction(this, @"Press the <Delete> key");

            // Repeat for the <5> button
            DmiActions.ShowInstruction(this, @"Press and hold the ‘5’ key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘4’ key is displayed pressed and immediately re-displayed enabled." +
                                Environment.NewLine +
                                @"2. The ‘Click’ sound is played once." + Environment.NewLine +
                                @"3. The ‘Year’ data input field displays ‘0125’." + Environment.NewLine +
                                "4. The cursor is displayed after the ‘5’." + Environment.NewLine +
                                "5. The data input field is used to enter the ‘Year’." + Environment.NewLine +
                                "6. The local time can be edited." + Environment.NewLine +
                                "7. The date and time echo texts are not updated.");

            DmiActions.ShowInstruction(this, @"Release the key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘5’ key is displayed enabled.");

            DmiActions.ShowInstruction(this, @"Press the <Delete> key");

            // Repeat for the <6> button
            DmiActions.ShowInstruction(this, @"Press and hold the ‘6’ key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘6’ key is displayed pressed and immediately re-displayed enabled." +
                                Environment.NewLine +
                                @"2. The ‘Click’ sound is played once." + Environment.NewLine +
                                @"3. The ‘Year’ data input field displays ‘0126’." + Environment.NewLine +
                                "4. The cursor is displayed after the ‘6’." + Environment.NewLine +
                                "5. The data input field is used to enter the ‘Year’." + Environment.NewLine +
                                "6. The local time can be edited." + Environment.NewLine +
                                "7. The date and time echo texts are not updated.");

            DmiActions.ShowInstruction(this, @"Release the key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘6’ key is displayed enabled.");


            DmiActions.ShowInstruction(this, @"Press the <Delete> key");

            // Repeat for the <7> button
            DmiActions.ShowInstruction(this, @"Press and hold the ‘7’ key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘7’ key is displayed pressed and immediately re-displayed enabled." +
                                Environment.NewLine +
                                @"2. The ‘Click’ sound is played once." + Environment.NewLine +
                                @"3. The ‘Year’ data input field displays ‘0127’." + Environment.NewLine +
                                "4. The cursor is displayed after the ‘7’." + Environment.NewLine +
                                "5. The data input field is used to enter the ‘Year’." + Environment.NewLine +
                                "6. The local time can be edited." + Environment.NewLine +
                                "7. The date and time echo texts are not updated.");

            DmiActions.ShowInstruction(this, @"Release the key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘7’ key is displayed enabled.");

            DmiActions.ShowInstruction(this, @"Press the <Delete> key");

            // Repeat for the <8> button
            DmiActions.ShowInstruction(this, @"Press and hold the ‘8’ key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘8’ key is displayed pressed and immediately re-displayed enabled." +
                                Environment.NewLine +
                                @"2. The ‘Click’ sound is played once." + Environment.NewLine +
                                @"3. The ‘Year’ data input field displays ‘0128’." + Environment.NewLine +
                                "4. The cursor is displayed after the ‘8’." + Environment.NewLine +
                                "5. The data input field is used to enter the ‘Year’." + Environment.NewLine +
                                "6. The local time can be edited." + Environment.NewLine +
                                "7. The date and time echo texts are not updated.");

            DmiActions.ShowInstruction(this, @"Release the key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘8’ key is displayed enabled.");

            DmiActions.ShowInstruction(this, @"Press the <Delete> key");

            // Repeat for the <9> button
            DmiActions.ShowInstruction(this, @"Press and hold the ‘9’ key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘9’ key is displayed pressed and immediately re-displayed enabled." +
                                Environment.NewLine +
                                @"2. The ‘Click’ sound is played once." + Environment.NewLine +
                                @"3. The ‘Year’ data input field displays ‘0129’." + Environment.NewLine +
                                "4. The cursor is displayed after the ‘9’." + Environment.NewLine +
                                "5. The data input field is used to enter the ‘Year’." + Environment.NewLine +
                                "6. The local time can be edited." + Environment.NewLine +
                                "7. The date and time echo texts are not updated.");

            DmiActions.ShowInstruction(this, @"Release the key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘9’ key is displayed enabled.");

            TraceHeader("Test Step 8");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Press and hold ‘Del’ button.Note: Stopwatch is required");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following information,While press and hold button less than 1.5 sec(1) Sound ‘Click’ is played once.(2)The state of button is changed to ‘Pressed’ and immediately back to ‘Enabled’ state.(3)The last character is removed from an input field after pressing the button.While press and hold button over 1.5 sec(4)The state ‘pressed’ and ‘released’ are switched repeatly while button is pressed and the characters are removed from an input field repeatly refer to pressed state.(5)The sound ‘Click’ is played repeatly while button is pressed");
            /*
            Test Step 8
            Action: Press and hold ‘Del’ button.Note: Stopwatch is required
            Expected Result: Verify the following information,While press and hold button less than 1.5 sec(1) Sound ‘Click’ is played once.(2)The state of button is changed to ‘Pressed’ and immediately back to ‘Enabled’ state.(3)The last character is removed from an input field after pressing the button.While press and hold button over 1.5 sec(4)The state ‘pressed’ and ‘released’ are switched repeatly while button is pressed and the characters are removed from an input field repeatly refer to pressed state.(5)The sound ‘Click’ is played repeatly while button is pressed
            Test Step Comment: (1) MMI_gen 11942 (partly: MMI_gen 4913 (partly: Year), MMI_gen 4384 (partly: sound ‘Click’));(2) MMI_gen 11942 (partly: MMI_gen 4913 (partly: Year), MMI_gen 4384 (partly: Change to state ‘Pressed’ and immediately back to state ‘Enabled’));    (3) MMI_gen 11942 (partly: MMI_gen 4913 (partly: Year), MMI_gen 4384 (partly: ETCS-MMI’s function associated to the button)); MMI_gen 4393 (partly: [Delete]);(4) MMI_gen 11942  (partly: MMI_gen 4913 (partly: Year), MMI_gen 4386 (partly: visual of repeat function));(5) MMI_gen 11942 (partly: MMI_gen 4913 (partly: Year), MMI_gen 4386 (partly: audible of repeat function));
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this,
                @"Press and hold the ‘Del’ key for more than 1.5s. Note: Stopwatch is required");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. Whilst the ‘Del’ key has been pressed for less than 1.5 s, it is displayed pressed and immediately re-displayed enabled;" +
                                Environment.NewLine +
                                @"2. The ‘Click’ sound is played once;" + Environment.NewLine +
                                @"3. ‘9’ is deleted from the ‘Year’ data input field, which displays ‘012’." +
                                Environment.NewLine +
                                @"4. After the ‘Del’ key has been pressed for more than 1.5s, it is repeatedly displayed pressed and immediately re-displayed enabled;" +
                                Environment.NewLine +
                                "5. Characters are repeatedly deleted from the end of the data input field;" +
                                Environment.NewLine +
                                @"6. The ‘Click’ sound is played repeatedly.");

            TraceHeader("Test Step 9");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Release ‘Del’ button");
            TraceReport("Expected Result");
            TraceInfo("Verify the following information, (1)The character is stop removing");
            /*
            Test Step 9
            Action: Release ‘Del’ button
            Expected Result: Verify the following information, (1)The character is stop removing
            Test Step Comment: (1) MMI_gen 11942 (partly: MMI_gen 4913 (partly: Year)), MMI_gen 4384 (partly: ETCS-MMI’s function associated to the button));
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Release the ‘Del’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. Characters stop being deleted from the ‘Year’ data input field.");

            TraceHeader("Test Step 10");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Delete the old value and enter the new value ‘2016 for Year.Then, confirm an entered data by pressing an input field");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following information,Input fields(1)The associated ‘Enter’ button is data field itself.(2)An input field is used to allow the driver to enter data.(3)The state of ‘Year’ input field is changed to ‘accepted’ as follows,The background colour of the Data Area is dark-grey.The colour of data value is white.(4)The next input field ‘Month’ is in state ‘selected’ as follows,The background colour of the Data Area is medium-grey.The colour of data value is black.Echo Texts(5)The echo text of ‘Year‘ is changed to white colour.(6)The value of echo text is changed refer to entered data.Entering Characters (7)The cursor is displayed as a horizontal line below the position of the next character to be entered.(8)The cursor is flashed by changing from visible to not visible.Keyboard(9)The keyboard associated to selected input field ‘Month’ is Numeric keyboard.(10)The keyboard contains enabled button for the number <1> to <9>, <Delete>(NA21) , <0> and disabled <Decimal_Separator>. NA21, Delete button");
            /*
            Test Step 10
            Action: Delete the old value and enter the new value ‘2016 for Year.Then, confirm an entered data by pressing an input field
            Expected Result: Verify the following information,Input fields(1)The associated ‘Enter’ button is data field itself.(2)An input field is used to allow the driver to enter data.(3)The state of ‘Year’ input field is changed to ‘accepted’ as follows,The background colour of the Data Area is dark-grey.The colour of data value is white.(4)The next input field ‘Month’ is in state ‘selected’ as follows,The background colour of the Data Area is medium-grey.The colour of data value is black.Echo Texts(5)The echo text of ‘Year‘ is changed to white colour.(6)The value of echo text is changed refer to entered data.Entering Characters (7)The cursor is displayed as a horizontal line below the position of the next character to be entered.(8)The cursor is flashed by changing from visible to not visible.Keyboard(9)The keyboard associated to selected input field ‘Month’ is Numeric keyboard.(10)The keyboard contains enabled button for the number <1> to <9>, <Delete>(NA21) , <0> and disabled <Decimal_Separator>. NA21, Delete button
            Test Step Comment: (1) MMI_gen 11942 (partly: MMI_gen 4682 (partly: Year));(2) MMI_gen 11942 (partly: MMI_gen 4634 (partly: Year));(3) MMI_gen 11942 (partly: MMI_gen 4652 (partly: Year), MMI_gen 4684 (partly: accepted, Year));(4) MMI_gen 11942 (partly: MMI_gen 4684 (partly: Month, selected automatically), MMI_gen 4651 (partly: Month));(5) MMI_gen 11942 (partly: MMI_gen 4700 (partly: Year));(6) MMI_gen 11942 (partly: MMI_gen 4681 (partly: Year), MMI_gen 4890, MMI_gen 4698);(7) MMI_gen 11942 (partly: MMI_gen 4689, MMI_gen 4690);(8) MMI_gen 11942 (partly: MMI_gen 4691 (partly: flash, Month));(9) ) MMI_gen 11944  (partly: Month); MMI_gen 11942 (partly: MMI_gen 4912 (partly: Month), MMI_gen 4678 (partly: Month));(10) MMI_gen 11942 (partly: MMI_gen 5003 (partly: Month)); MMI_gen 4392 (partly: [Delete] NA21);
            */
            DmiActions.ShowInstruction(this,
                @"Delete the value in the ‘Year’ data input field and enter ‘2017’, then confirm the value by pressing the ‘Year’ data input field");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The data input field is an ‘Enter’ button." + Environment.NewLine +
                                "2. The data input field can be used to enter data." + Environment.NewLine +
                                @"3. The ‘Year’ data input field is displayed ‘Accepted’, with the value in white on a Dark-grey background." +
                                Environment.NewLine +
                                @"4. The next data input field (‘Month’) is  ‘Selected’, with the value in black on a Medium-grey background." +
                                Environment.NewLine +
                                @"5. A corresponding numeric keypad is displayed for the next data input field." +
                                Environment.NewLine +
                                @"6. The keypad contains enabled keys for the numbers <1> to <9>, <Delete> (symbol NA21), <0> and a disabled <Decimal_Separator> key." +
                                Environment.NewLine +
                                @"7. The ‘Year’ echo text displays ‘2017’ in white." + Environment.NewLine +
                                "8. A flashing (visible/invisible) underscore is displayed as a cursor after the last character of the data input field.");

            TraceHeader("Test Step 11");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Perform action step 2-7 for keypad of the ‘Month’ input field");
            TraceReport("Expected Result");
            TraceInfo(
                "See the expected results of Step 2 – Step 7 and the following additional information,(1)The pressed key is added in an input field immediately. (2)The cursor is jumped to next position after entered the character immediately.(3)The input field is used to enter the Month");
            /*
            Test Step 11
            Action: Perform action step 2-7 for keypad of the ‘Month’ input field
            Expected Result: See the expected results of Step 2 – Step 7 and the following additional information,(1)The pressed key is added in an input field immediately. (2)The cursor is jumped to next position after entered the character immediately.(3)The input field is used to enter the Month
            Test Step Comment: (1) MMI_gen 11942 (partly: MMI_gen 4642 (partly: Month));  (2) MMI_gen 11942 (partly: MMI_gen 4692 (partly: Month));  (3) MMI_gen 11952 (partly: entry);
            */
            // Surely steps 5 and 6
            DmiActions.ShowInstruction(this, @"Press and hold the ‘0’ key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘0’ key is displayed pressed and immediately re-displayed enabled." +
                                Environment.NewLine +
                                @"2. The ‘Click’ sound is played once." + Environment.NewLine +
                                @"3. The ‘Month’ data input field displays ‘0’." + Environment.NewLine +
                                "4. The cursor is displayed after the ‘0’." + Environment.NewLine +
                                "5. The data input field is used to enter the ‘Month’ value." + Environment.NewLine +
                                "6. The local time can be edited." + Environment.NewLine +
                                "7. The date and time echo texts stop being updated.");

            DmiActions.ShowInstruction(this, @"Release the key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘0’ key is displayed enabled.");

            // Repeat for the <1> key
            DmiActions.ShowInstruction(this, @"Press and hold the ‘1’ key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘0’ key is displayed pressed and immediately re-displayed enabled." +
                                Environment.NewLine +
                                @"2. The ‘Click’ sound is played once." + Environment.NewLine +
                                @"3. The ‘Month’ data input field displays ‘01’." + Environment.NewLine +
                                "4. The cursor is displayed after the ‘1’." + Environment.NewLine +
                                "5. The data input field is used to enter the ‘Month’." + Environment.NewLine +
                                "6. The local time can be edited." + Environment.NewLine +
                                "7. The date and time echo texts are not updated.");
            DmiActions.ShowInstruction(this, @"Release the key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘1’ key is displayed enabled.");

            DmiActions.ShowInstruction(this, @"Press the <Delete> key");

            // Repeat for the <2> key
            DmiActions.ShowInstruction(this, @"Press and hold the ‘2’ key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘2’ key is displayed pressed and immediately re-displayed enabled." +
                                Environment.NewLine +
                                @"2. The ‘Click’ sound is played once." + Environment.NewLine +
                                @"3. The ‘Month’ data input field displays ‘02’." + Environment.NewLine +
                                "4. The cursor is displayed after the ‘2’." + Environment.NewLine +
                                "5. The data input field is used to enter the ‘Month’." + Environment.NewLine +
                                "6. The local time can be edited." + Environment.NewLine +
                                "7. The date and time echo texts are not updated.");

            DmiActions.ShowInstruction(this, @"Release the key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘2’ key is displayed enabled.");

            DmiActions.ShowInstruction(this, @"Press the <Delete> key");

            // Repeat for the <3> button
            DmiActions.ShowInstruction(this, @"Press and hold the ‘3’ key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘3’ key is displayed pressed and immediately re-displayed enabled." +
                                Environment.NewLine +
                                @"2. The ‘Click’ sound is played once." + Environment.NewLine +
                                @"3. The ‘Month’ data input field displays ‘03’." + Environment.NewLine +
                                "4. The cursor is displayed after the ‘3’." + Environment.NewLine +
                                "5. The data input field is used to enter the ‘Month’." + Environment.NewLine +
                                "6. The local time can be edited." + Environment.NewLine +
                                "7. The date and time echo texts are not updated.");

            DmiActions.ShowInstruction(this, @"Release the key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘3’ key is displayed enabled.");

            DmiActions.ShowInstruction(this, @"Press the <Delete> key");

            // Repeat for the <4> button
            DmiActions.ShowInstruction(this, @"Press and hold the ‘4’ key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘3’ key is displayed pressed and immediately re-displayed enabled." +
                                Environment.NewLine +
                                @"2. The ‘Click’ sound is played once." + Environment.NewLine +
                                @"3. The ‘Month’ data input field displays ‘04’." + Environment.NewLine +
                                "4. The cursor is displayed after the ‘4’." + Environment.NewLine +
                                "5. The data input field is used to enter the ‘Month’." + Environment.NewLine +
                                "6. The local time can be edited." + Environment.NewLine +
                                "7. The date and time echo texts are not updated.");

            DmiActions.ShowInstruction(this, @"Release the key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘4’ key is displayed enabled.");

            DmiActions.ShowInstruction(this, @"Press the <Delete> key");

            // Repeat for the <5> button
            DmiActions.ShowInstruction(this, @"Press and hold the ‘5’ key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘4’ key is displayed pressed and immediately re-displayed enabled." +
                                Environment.NewLine +
                                @"2. The ‘Click’ sound is played once." + Environment.NewLine +
                                @"3. The ‘Month’ data input field displays ‘05’." + Environment.NewLine +
                                "4. The cursor is displayed after the ‘5’." + Environment.NewLine +
                                "5. The data input field is used to enter the ‘Month’." + Environment.NewLine +
                                "6. The local time can be edited." + Environment.NewLine +
                                "7. The date and time echo texts are not updated.");

            DmiActions.ShowInstruction(this, @"Release the key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘5’ key is displayed enabled.");

            DmiActions.ShowInstruction(this, @"Press the <Delete> key");

            // Repeat for the <6> button
            DmiActions.ShowInstruction(this, @"Press and hold the ‘6’ key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘6’ key is displayed pressed and immediately re-displayed enabled." +
                                Environment.NewLine +
                                @"2. The ‘Click’ sound is played once." + Environment.NewLine +
                                @"3. The ‘Month’ data input field displays ‘06’." + Environment.NewLine +
                                "4. The cursor is displayed after the ‘6’." + Environment.NewLine +
                                "5. The data input field is used to enter the ‘Month’." + Environment.NewLine +
                                "6. The local time can be edited." + Environment.NewLine +
                                "7. The date and time echo texts are not updated.");

            DmiActions.ShowInstruction(this, @"Release the key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘6’ key is displayed enabled.");


            DmiActions.ShowInstruction(this, @"Press the <Delete> key");

            // Repeat for the <7> button
            DmiActions.ShowInstruction(this, @"Press and hold the ‘7’ key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘7’ key is displayed pressed and immediately re-displayed enabled." +
                                Environment.NewLine +
                                @"2. The ‘Click’ sound is played once." + Environment.NewLine +
                                @"3. The ‘Month’ data input field displays ‘07’." + Environment.NewLine +
                                "4. The cursor is displayed after the ‘7’." + Environment.NewLine +
                                "5. The data input field is used to enter the ‘Month’." + Environment.NewLine +
                                "6. The local time can be edited." + Environment.NewLine +
                                "7. The date and time echo texts are not updated.");

            DmiActions.ShowInstruction(this, @"Release the key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘7’ key is displayed enabled.");

            DmiActions.ShowInstruction(this, @"Press the <Delete> key");

            // Repeat for the <8> button
            DmiActions.ShowInstruction(this, @"Press and hold the ‘8’ key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘8’ key is displayed pressed and immediately re-displayed enabled." +
                                Environment.NewLine +
                                @"2. The ‘Click’ sound is played once." + Environment.NewLine +
                                @"3. The ‘Month’ data input field displays ‘08’." + Environment.NewLine +
                                "4. The cursor is displayed after the ‘8’." + Environment.NewLine +
                                "5. The data input field is used to enter the ‘Month’." + Environment.NewLine +
                                "6. The local time can be edited." + Environment.NewLine +
                                "7. The date and time echo texts are not updated.");

            DmiActions.ShowInstruction(this, @"Release the key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘8’ key is displayed enabled.");

            DmiActions.ShowInstruction(this, @"Press the <Delete> key");

            // Repeat for the <9> button
            DmiActions.ShowInstruction(this, @"Press and hold the ‘9’ key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘9’ key is displayed pressed and immediately re-displayed enabled." +
                                Environment.NewLine +
                                @"2. The ‘Click’ sound is played once." + Environment.NewLine +
                                @"3. The ‘Month’ data input field displays ‘09’." + Environment.NewLine +
                                "4. The cursor is displayed after the ‘9’." + Environment.NewLine +
                                "5. The data input field is used to enter the ‘Month’." + Environment.NewLine +
                                "6. The local time can be edited." + Environment.NewLine +
                                "7. The date and time echo texts are not updated.");

            DmiActions.ShowInstruction(this, @"Release the key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘9’ key is displayed enabled.");

            TraceHeader("Test Step 12");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Delete the old value and enter the new value ‘12’ for Month.Then, confirm an entered data by pressing an input field");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following information,Input fields(1)The associated ‘Enter’ button is data field itself.(2)An input field is used to allow the driver to enter data.(3)The state of ‘Month’ input field is changed to ‘accepted’ as follows,The background colour of the Data Area is dark-grey.The colour of data value is white.(4)The next input field ‘Day’ is in state ‘selected’ as follows,The background colour of the Data Area is medium-grey.The colour of data value is black.Echo Texts(5)The echo text of ‘Month ‘is changed to white colour.(6)The value of echo text is changed refer to entered data.Entering Characters (7)The cursor is displayed as a horizontal line below the position of the next character to be entered.(8)The cursor is flashed by changing from visible to not visible.Keyboard(9)The keyboard associated to selected input field ‘Day’ is Numeric keyboard.(10)The keyboard contains enabled button for the number <1> to <9>, <Delete>(NA21) , <0> and disabled <Decimal_Separator>. NA21, Delete button");
            /*
            Test Step 12
            Action: Delete the old value and enter the new value ‘12’ for Month.Then, confirm an entered data by pressing an input field
            Expected Result: Verify the following information,Input fields(1)The associated ‘Enter’ button is data field itself.(2)An input field is used to allow the driver to enter data.(3)The state of ‘Month’ input field is changed to ‘accepted’ as follows,The background colour of the Data Area is dark-grey.The colour of data value is white.(4)The next input field ‘Day’ is in state ‘selected’ as follows,The background colour of the Data Area is medium-grey.The colour of data value is black.Echo Texts(5)The echo text of ‘Month ‘is changed to white colour.(6)The value of echo text is changed refer to entered data.Entering Characters (7)The cursor is displayed as a horizontal line below the position of the next character to be entered.(8)The cursor is flashed by changing from visible to not visible.Keyboard(9)The keyboard associated to selected input field ‘Day’ is Numeric keyboard.(10)The keyboard contains enabled button for the number <1> to <9>, <Delete>(NA21) , <0> and disabled <Decimal_Separator>. NA21, Delete button
            Test Step Comment: (1) MMI_gen 11942 (partly: MMI_gen 4682 (partly: Month));(2) MMI_gen 11942 (partly: MMI_gen 4634 (partly: Month));(3) MMI_gen 11942 (partly: MMI_gen 4652 (partly: Month), MMI_gen 4684 (partly: accepted, Month));(4) MMI_gen 11942 (partly: MMI_gen 4684 (partly: Day, selected automatically), MMI_gen 4651 (partly: Day));(5) MMI_gen 11942 (partly: MMI_gen 4700 (partly: Month));(6) MMI_gen 11942 (partly: MMI_gen 4681 (partly: Month), MMI_gen 4890, MMI_gen 4698);(7) MMI_gen 11942 (partly: MMI_gen 4689, MMI_gen 4690);(8) MMI_gen 11942 (partly: MMI_gen 4691 (partly: flash, Day));(9) ) MMI_gen 11944  (partly: Day); MMI_gen 11942 (partly: MMI_gen 4912 (partly: Day), MMI_gen 4678 (partly: Day));(10) MMI_gen 11942 (partly: MMI_gen 5003 (partly: Day)); MMI_gen 4392 (partly: [Delete] NA21);
            */
            DmiActions.ShowInstruction(this,
                @"Delete the value in the ‘Month’ data input field and enter ‘12’, then confirm the value by pressing the ‘Month’ data input field");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The data input field is an ‘Enter’ button." + Environment.NewLine +
                                "2. The data input field can be used to enter data." + Environment.NewLine +
                                @"3. The ‘Month’ data input field is displayed ‘Accepted’, with the value in white on a Dark-grey background." +
                                Environment.NewLine +
                                @"4. The next data input field (‘Day’) is  ‘Selected’, with the value in black on a Medium-grey background." +
                                Environment.NewLine +
                                @"5. A corresponding numeric keypad is displayed for the next data input field." +
                                Environment.NewLine +
                                @"6. The keypad contains enabled keys for the numbers <1> to <9>, <Delete> (symbol NA21), <0> and a disabled <Decimal_Separator> key." +
                                Environment.NewLine +
                                @"7. The ‘Month’ echo text displays ‘12’ in white." + Environment.NewLine +
                                "8. A flashing (visible/invisible) underscore is displayed as a cursor after the last character of the data input field.");

            TraceHeader("Test Step 13");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Perform action step 2-7 for keypad of the ‘Day’ input field");
            TraceReport("Expected Result");
            TraceInfo(
                "See the expected results of Step 2 – Step 7 and the following additional information,(1)The pressed key is added in an input field immediately. (2)The cursor is jumped to next position after entered the character immediately.(3)The input field is used to enter the Day");
            /*
            Test Step 13
            Action: Perform action step 2-7 for keypad of the ‘Day’ input field
            Expected Result: See the expected results of Step 2 – Step 7 and the following additional information,(1)The pressed key is added in an input field immediately. (2)The cursor is jumped to next position after entered the character immediately.(3)The input field is used to enter the Day
            Test Step Comment: (1) MMI_gen 11942 (partly: MMI_gen 4642 (partly: Day));  (2) MMI_gen 11942 (partly: MMI_gen 4692 (partly: Day));  (3) MMI_gen 11953 (partly: entry);
            */
            // Surely steps 5 and 6
            DmiActions.ShowInstruction(this, @"Press and hold the ‘0’ key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘0’ key is displayed pressed and immediately re-displayed enabled." +
                                Environment.NewLine +
                                @"2. The ‘Click’ sound is played once." + Environment.NewLine +
                                @"3. The ‘Day’ data input field displays ‘0’." + Environment.NewLine +
                                "4. The cursor is displayed after the ‘0’." + Environment.NewLine +
                                "5. The data input field is used to enter the ‘Day’ value." + Environment.NewLine +
                                "6. The local time can be edited." + Environment.NewLine +
                                "7. The date and time echo texts stop being updated.");

            DmiActions.ShowInstruction(this, @"Release the key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘0’ key is displayed enabled.");

            // Repeat for the <1> key
            DmiActions.ShowInstruction(this, @"Press and hold the ‘1’ key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘0’ key is displayed pressed and immediately re-displayed enabled." +
                                Environment.NewLine +
                                @"2. The ‘Click’ sound is played once." + Environment.NewLine +
                                @"3. The ‘Day’ data input field displays ‘01’." + Environment.NewLine +
                                "4. The cursor is displayed after the ‘1’." + Environment.NewLine +
                                "5. The data input field is used to enter the ‘Day’." + Environment.NewLine +
                                "6. The local time can be edited." + Environment.NewLine +
                                "7. The date and time echo texts are not updated.");

            DmiActions.ShowInstruction(this, @"Release the key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘1’ key is displayed enabled.");

            DmiActions.ShowInstruction(this, @"Press the <Delete> key");

            // Repeat for the <2> key
            DmiActions.ShowInstruction(this, @"Press and hold the ‘2’ key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘2’ key is displayed pressed and immediately re-displayed enabled." +
                                Environment.NewLine +
                                @"2. The ‘Click’ sound is played once." + Environment.NewLine +
                                @"3. The ‘Day’ data input field displays ‘02’." + Environment.NewLine +
                                "4. The cursor is displayed after the ‘2’." + Environment.NewLine +
                                "5. The data input field is used to enter the ‘Day’." + Environment.NewLine +
                                "6. The local time can be edited." + Environment.NewLine +
                                "7. The date and time echo texts are not updated.");

            DmiActions.ShowInstruction(this, @"Release the key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘2’ key is displayed enabled.");

            // Repeat for the <3> button
            DmiActions.ShowInstruction(this, @"Press and hold the ‘3’ key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘3’ key is displayed pressed and immediately re-displayed enabled." +
                                Environment.NewLine +
                                @"2. The ‘Click’ sound is played once." + Environment.NewLine +
                                @"3. The ‘Day’ data input field displays ‘03’." + Environment.NewLine +
                                "4. The cursor is displayed after the ‘3’." + Environment.NewLine +
                                "5. The data input field is used to enter the ‘Day’." + Environment.NewLine +
                                "6. The local time can be edited." + Environment.NewLine +
                                "7. The date and time echo texts are not updated.");

            DmiActions.ShowInstruction(this, @"Release the key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘3’ key is displayed enabled.");

            DmiActions.ShowInstruction(this, @"Press the <Delete> key");

            // Repeat for the <4> button
            DmiActions.ShowInstruction(this, @"Press and hold the ‘4’ key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘3’ key is displayed pressed and immediately re-displayed enabled." +
                                Environment.NewLine +
                                @"2. The ‘Click’ sound is played once." + Environment.NewLine +
                                @"3. The ‘Day’ data input field displays ‘04’." + Environment.NewLine +
                                "4. The cursor is displayed after the ‘4’." + Environment.NewLine +
                                "5. The data input field is used to enter the ‘Day’." + Environment.NewLine +
                                "6. The local time can be edited." + Environment.NewLine +
                                "7. The date and time echo texts are not updated.");

            DmiActions.ShowInstruction(this, @"Release the key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘4’ key is displayed enabled.");

            DmiActions.ShowInstruction(this, @"Press the <Delete> key");

            // Repeat for the <5> button
            DmiActions.ShowInstruction(this, @"Press and hold the ‘5’ key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘4’ key is displayed pressed and immediately re-displayed enabled." +
                                Environment.NewLine +
                                @"2. The ‘Click’ sound is played once." + Environment.NewLine +
                                @"3. The ‘Day’ data input field displays ‘05’." + Environment.NewLine +
                                "4. The cursor is displayed after the ‘5’." + Environment.NewLine +
                                "5. The data input field is used to enter the ‘Day’." + Environment.NewLine +
                                "6. The local time can be edited." + Environment.NewLine +
                                "7. The date and time echo texts are not updated.");

            DmiActions.ShowInstruction(this, @"Release the key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘5’ key is displayed enabled.");

            DmiActions.ShowInstruction(this, @"Press the <Delete> key");

            // Repeat for the <6> button
            DmiActions.ShowInstruction(this, @"Press and hold the ‘6’ key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘6’ key is displayed pressed and immediately re-displayed enabled." +
                                Environment.NewLine +
                                @"2. The ‘Click’ sound is played once." + Environment.NewLine +
                                @"3. The ‘Day’ data input field displays ‘06’." + Environment.NewLine +
                                "4. The cursor is displayed after the ‘6’." + Environment.NewLine +
                                "5. The data input field is used to enter the ‘Day’." + Environment.NewLine +
                                "6. The local time can be edited." + Environment.NewLine +
                                "7. The date and time echo texts are not updated.");

            DmiActions.ShowInstruction(this, @"Release the key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘6’ key is displayed enabled.");


            DmiActions.ShowInstruction(this, @"Press the <Delete> key");

            // Repeat for the <7> button
            DmiActions.ShowInstruction(this, @"Press and hold the ‘7’ key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘7’ key is displayed pressed and immediately re-displayed enabled." +
                                Environment.NewLine +
                                @"2. The ‘Click’ sound is played once." + Environment.NewLine +
                                @"3. The ‘Day’ data input field displays ‘07’." + Environment.NewLine +
                                "4. The cursor is displayed after the ‘7’." + Environment.NewLine +
                                "5. The data input field is used to enter the ‘Day’." + Environment.NewLine +
                                "6. The local time can be edited." + Environment.NewLine +
                                "7. The date and time echo texts are not updated.");

            DmiActions.ShowInstruction(this, @"Release the key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘7’ key is displayed enabled.");

            DmiActions.ShowInstruction(this, @"Press the <Delete> key");

            // Repeat for the <8> button
            DmiActions.ShowInstruction(this, @"Press and hold the ‘8’ key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘8’ key is displayed pressed and immediately re-displayed enabled." +
                                Environment.NewLine +
                                @"2. The ‘Click’ sound is played once." + Environment.NewLine +
                                @"3. The ‘Day’ data input field displays ‘08’." + Environment.NewLine +
                                "4. The cursor is displayed after the ‘8’." + Environment.NewLine +
                                "5. The data input field is used to enter the ‘Day’." + Environment.NewLine +
                                "6. The local time can be edited." + Environment.NewLine +
                                "7. The date and time echo texts are not updated.");

            DmiActions.ShowInstruction(this, @"Release the key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘8’ key is displayed enabled.");

            DmiActions.ShowInstruction(this, @"Press the <Delete> key");

            // Repeat for the <9> button
            DmiActions.ShowInstruction(this, @"Press and hold the ‘9’ key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘9’ key is displayed pressed and immediately re-displayed enabled." +
                                Environment.NewLine +
                                @"2. The ‘Click’ sound is played once." + Environment.NewLine +
                                @"3. The ‘Day’ data input field displays ‘09’." + Environment.NewLine +
                                "4. The cursor is displayed after the ‘9’." + Environment.NewLine +
                                "5. The data input field is used to enter the ‘Day’." + Environment.NewLine +
                                "6. The local time can be edited." + Environment.NewLine +
                                "7. The date and time echo texts are not updated.");

            DmiActions.ShowInstruction(this, @"Release the key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘9’ key is displayed enabled.");

            TraceHeader("Test Step 14");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Delete the old value and enter the new value ‘31’ for Day.Then, confirm an entered data by pressing an input field");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following information,Input fields(1)The associated ‘Enter’ button is data field itself.(2)An input field is used to allow the driver to enter data.(3)The state of ‘Day’ input field is changed to ‘accepted’ as follows,The background colour of the Data Area is dark-grey.The colour of data value is white.(4)The next input field ‘Hour’ is in state ‘selected’ as follows,The background colour of the Data Area is medium-grey.The colour of data value is black.(5) There are only 4 input fields displayed in the second page of window.(6)The label of 4th input field is ‘Hour’.(7)The label of 5th input field is ‘Minute’.(8)The label of 6th input field is ‘Second’.(9)The label of 7th input field is ‘Offset’.Echo Texts(10)The echo text of ‘Day ‘is changed to white colour.(11)The value of echo text is changed refer to entered data.Entering Characters (12)The cursor is displayed as a horizontal line below the position of the next character to be entered.(13)The cursor is flashed by changing from visible to not visible.Keyboard(14)The keyboard associated to selected input field ‘Hour’ is Numeric keyboard.(15)The keyboard contains enabled button for the number <1> to <9>, <Delete>(NA21) , <0> and disabled <Decimal_Separator>. NA21, Delete button.Navigation button(16)The state of ‘Previous’ and ‘Next’ button are displayed as follows, ‘Next’ button is disabled, display symbol NA18.2 ‘Previous’ button is enable, display symbol NA18(17)   The window title is displayed as ‘Set clock (2/2)’");
            /*
            Test Step 14
            Action: Delete the old value and enter the new value ‘31’ for Day.Then, confirm an entered data by pressing an input field
            Expected Result: Verify the following information,Input fields(1)The associated ‘Enter’ button is data field itself.(2)An input field is used to allow the driver to enter data.(3)The state of ‘Day’ input field is changed to ‘accepted’ as follows,The background colour of the Data Area is dark-grey.The colour of data value is white.(4)The next input field ‘Hour’ is in state ‘selected’ as follows,The background colour of the Data Area is medium-grey.The colour of data value is black.(5) There are only 4 input fields displayed in the second page of window.(6)The label of 4th input field is ‘Hour’.(7)The label of 5th input field is ‘Minute’.(8)The label of 6th input field is ‘Second’.(9)The label of 7th input field is ‘Offset’.Echo Texts(10)The echo text of ‘Day ‘is changed to white colour.(11)The value of echo text is changed refer to entered data.Entering Characters (12)The cursor is displayed as a horizontal line below the position of the next character to be entered.(13)The cursor is flashed by changing from visible to not visible.Keyboard(14)The keyboard associated to selected input field ‘Hour’ is Numeric keyboard.(15)The keyboard contains enabled button for the number <1> to <9>, <Delete>(NA21) , <0> and disabled <Decimal_Separator>. NA21, Delete button.Navigation button(16)The state of ‘Previous’ and ‘Next’ button are displayed as follows, ‘Next’ button is disabled, display symbol NA18.2 ‘Previous’ button is enable, display symbol NA18(17)   The window title is displayed as ‘Set clock (2/2)’
            Test Step Comment: (1) MMI_gen 11942 (partly: MMI_gen 4682 (partly: Day));(2) MMI_gen 11942 (partly: MMI_gen 4634 (partly: Day));(3) MMI_gen 11942 (partly: MMI_gen 4652 (partly: Day), MMI_gen 4684 (partly: accepted, Day));(4) MMI_gen 11942 (partly: MMI_gen 4684 (partly: Hour, selected automatically), MMI_gen 4651 (partly: Hour));(5) MMI_gen 12125 (partly: second page);(6) MMI_gen 11956 (partly: label);(7) MMI_gen 11957 (partly: label);(8) MMI_gen 11958 (partly: label);(9) MMI_gen 11954 (partly: label);(10) MMI_gen 11942 (partly: MMI_gen 4700 (partly: Day));(11) MMI_gen 11942 (partly: MMI_gen 4681 (partly: Day), MMI_gen 4890, MMI_gen 4698);(12 )MMI_gen 11942 (partly: MMI_gen 4689, MMI_gen 4690);(13) MMI_gen 11942 (partly: MMI_gen 4691 (partly: flash, Hour));(14) MMI_gen 11944  (partly: Hour); MMI_gen 11942 (partly: MMI_gen 4912 (partly: Hour), MMI_gen 4678 (partly: Hour));(15) MMI_gen 11942 (partly: MMI_gen 5003 (partly: Hour)); MMI_gen 4392 (partly: [Delete] NA21); (16)  MMI_gen 4394 (partly: enabled [previous], disabled [next]); MMI_gen 4358;(17) MMI_gen 4360 (partly: total number of window);
            */
            // This test is brain-dead. After confirming the day value the second window should be displayed so the Day fields are not visible. Doh!
            DmiActions.ShowInstruction(this,
                @"Delete the value in the ‘Day’ data input field and enter ‘31’, then confirm the value by pressing the ‘Day’ data input field");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The data input field is an ‘Enter’ button." + Environment.NewLine +
                                "2. The data input field can be used to enter data." + Environment.NewLine +
                                @"3. The ‘Day’ data input field is displayed ‘Accepted’, with the value in white on a Dark-grey background." +
                                Environment.NewLine +
                                "4. The next page of the window, with title ‘Set clock(2/2)’, is displayed with 4 data input fields." +
                                Environment.NewLine +
                                "5. The data fields are labelled ‘Hour’, ‘Minute’, ‘Second’, and ‘Offset’." +
                                Environment.NewLine +
                                @"6. The data input field (‘Hour’) is ‘Selected’, with the value in black on a Medium-grey background." +
                                Environment.NewLine +
                                @"7. A corresponding numeric keypad is displayed for the next data input field." +
                                Environment.NewLine +
                                @"8. The keypad contains enabled keys for the numbers <1> to <9>, <Delete> (symbol NA21), <0> and a disabled <Decimal_Separator> key." +
                                Environment.NewLine +
                                @"9. A flashing (visible/invisible) underscore is displayed as a cursor after the last character of the data input field." +
                                Environment.NewLine +
                                @"10. The ‘Next’ button is displayed disabled (symbol NA18.2) and the ‘Previous’ button is displayed enabled (symbol 18).");

            TraceHeader("Test Step 15");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Perform action step 2-7 for keypad of the ‘Hour’ input field");
            TraceReport("Expected Result");
            TraceInfo(
                "See the expected results of Step 2 – Step 7 and the following additional information,(1)The pressed key is added in an input field immediately. (2)The cursor is jumped to next position after entered the character immediately.(3)The input field is used to enter the Hour");
            /*
            Test Step 15
            Action: Perform action step 2-7 for keypad of the ‘Hour’ input field
            Expected Result: See the expected results of Step 2 – Step 7 and the following additional information,(1)The pressed key is added in an input field immediately. (2)The cursor is jumped to next position after entered the character immediately.(3)The input field is used to enter the Hour
            Test Step Comment: (1) MMI_gen 11942 (partly: MMI_gen 4642 (partly: Hour));  (2) MMI_gen 11942 (partly: MMI_gen 4692 (partly: Hour));  (3) MMI_gen 11956 (partly: entry);
            */
            // Surely steps 5 and 6
            DmiActions.ShowInstruction(this, @"Press and hold the ‘0’ key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘0’ key is displayed pressed and immediately re-displayed enabled." +
                                Environment.NewLine +
                                @"2. The ‘Click’ sound is played once." + Environment.NewLine +
                                @"3. The ‘Hour’ data input field displays ‘0’." + Environment.NewLine +
                                "4. The cursor is displayed after the ‘0’." + Environment.NewLine +
                                "5. The data input field is used to enter the ‘Hour’ value." + Environment.NewLine +
                                "6. The local time can be edited." + Environment.NewLine +
                                "7. The date and time echo texts stop being updated.");

            DmiActions.ShowInstruction(this, @"Release the key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘0’ key is displayed enabled.");

            // Repeat for the <1> key
            DmiActions.ShowInstruction(this, @"Press and hold the ‘1’ key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘0’ key is displayed pressed and immediately re-displayed enabled." +
                                Environment.NewLine +
                                @"2. The ‘Click’ sound is played once." + Environment.NewLine +
                                @"3. The ‘Hour’ data input field displays ‘01’." + Environment.NewLine +
                                "4. The cursor is displayed after the ‘1’." + Environment.NewLine +
                                "5. The data input field is used to enter the ‘Hour’." + Environment.NewLine +
                                "6. The local time can be edited." + Environment.NewLine +
                                "7. The date and time echo texts are not updated.");

            DmiActions.ShowInstruction(this, @"Release the key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘1’ key is displayed enabled.");

            DmiActions.ShowInstruction(this, @"Press the <Delete> key");

            // Repeat for the <2> key
            DmiActions.ShowInstruction(this, @"Press and hold the ‘2’ key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘2’ key is displayed pressed and immediately re-displayed enabled." +
                                Environment.NewLine +
                                @"2. The ‘Click’ sound is played once." + Environment.NewLine +
                                @"3. The ‘Hour’ data input field displays ‘02’." + Environment.NewLine +
                                "4. The cursor is displayed after the ‘2’." + Environment.NewLine +
                                "5. The data input field is used to enter the ‘Hour’." + Environment.NewLine +
                                "6. The local time can be edited." + Environment.NewLine +
                                "7. The date and time echo texts are not updated.");

            DmiActions.ShowInstruction(this, @"Release the key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘2’ key is displayed enabled.");

            // Repeat for the <3> button
            DmiActions.ShowInstruction(this, @"Press and hold the ‘3’ key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘3’ key is displayed pressed and immediately re-displayed enabled." +
                                Environment.NewLine +
                                @"2. The ‘Click’ sound is played once." + Environment.NewLine +
                                @"3. The ‘Hour’ data input field displays ‘03’." + Environment.NewLine +
                                "4. The cursor is displayed after the ‘3’." + Environment.NewLine +
                                "5. The data input field is used to enter the ‘Hour’." + Environment.NewLine +
                                "6. The local time can be edited." + Environment.NewLine +
                                "7. The date and time echo texts are not updated.");

            DmiActions.ShowInstruction(this, @"Release the key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘3’ key is displayed enabled.");

            DmiActions.ShowInstruction(this, @"Press the <Delete> key");

            // Repeat for the <4> button
            DmiActions.ShowInstruction(this, @"Press and hold the ‘4’ key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘3’ key is displayed pressed and immediately re-displayed enabled." +
                                Environment.NewLine +
                                @"2. The ‘Click’ sound is played once." + Environment.NewLine +
                                @"3. The ‘Hour’ data input field displays ‘04’." + Environment.NewLine +
                                "4. The cursor is displayed after the ‘4’." + Environment.NewLine +
                                "5. The data input field is used to enter the ‘Hour’." + Environment.NewLine +
                                "6. The local time can be edited." + Environment.NewLine +
                                "7. The date and time echo texts are not updated.");

            DmiActions.ShowInstruction(this, @"Release the key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘4’ key is displayed enabled.");

            DmiActions.ShowInstruction(this, @"Press the <Delete> key");

            // Repeat for the <5> button
            DmiActions.ShowInstruction(this, @"Press and hold the ‘5’ key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘4’ key is displayed pressed and immediately re-displayed enabled." +
                                Environment.NewLine +
                                @"2. The ‘Click’ sound is played once." + Environment.NewLine +
                                @"3. The ‘Hour’ data input field displays ‘05’." + Environment.NewLine +
                                "4. The cursor is displayed after the ‘5’." + Environment.NewLine +
                                "5. The data input field is used to enter the ‘Hour’." + Environment.NewLine +
                                "6. The local time can be edited." + Environment.NewLine +
                                "7. The date and time echo texts are not updated.");

            DmiActions.ShowInstruction(this, @"Release the key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘5’ key is displayed enabled.");

            DmiActions.ShowInstruction(this, @"Press the <Delete> key");

            // Repeat for the <6> button
            DmiActions.ShowInstruction(this, @"Press and hold the ‘6’ key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘6’ key is displayed pressed and immediately re-displayed enabled." +
                                Environment.NewLine +
                                @"2. The ‘Click’ sound is played once." + Environment.NewLine +
                                @"3. The ‘Hour’ data input field displays ‘06’." + Environment.NewLine +
                                "4. The cursor is displayed after the ‘6’." + Environment.NewLine +
                                "5. The data input field is used to enter the ‘Hour’." + Environment.NewLine +
                                "6. The local time can be edited." + Environment.NewLine +
                                "7. The date and time echo texts are not updated.");

            DmiActions.ShowInstruction(this, @"Release the key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘6’ key is displayed enabled.");


            DmiActions.ShowInstruction(this, @"Press the <Delete> key");

            // Repeat for the <7> button
            DmiActions.ShowInstruction(this, @"Press and hold the ‘7’ key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘7’ key is displayed pressed and immediately re-displayed enabled." +
                                Environment.NewLine +
                                @"2. The ‘Click’ sound is played once." + Environment.NewLine +
                                @"3. The ‘Hour’ data input field displays ‘07’." + Environment.NewLine +
                                "4. The cursor is displayed after the ‘7’." + Environment.NewLine +
                                "5. The data input field is used to enter the ‘Hour’." + Environment.NewLine +
                                "6. The local time can be edited." + Environment.NewLine +
                                "7. The date and time echo texts are not updated.");

            DmiActions.ShowInstruction(this, @"Release the key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘7’ key is displayed enabled.");

            DmiActions.ShowInstruction(this, @"Press the <Delete> key");

            // Repeat for the <8> button
            DmiActions.ShowInstruction(this, @"Press and hold the ‘8’ key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘8’ key is displayed pressed and immediately re-displayed enabled." +
                                Environment.NewLine +
                                @"2. The ‘Click’ sound is played once." + Environment.NewLine +
                                @"3. The ‘Hour’ data input field displays ‘08’." + Environment.NewLine +
                                "4. The cursor is displayed after the ‘8’." + Environment.NewLine +
                                "5. The data input field is used to enter the ‘Hour’." + Environment.NewLine +
                                "6. The local time can be edited." + Environment.NewLine +
                                "7. The date and time echo texts are not updated.");

            DmiActions.ShowInstruction(this, @"Release the key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘8’ key is displayed enabled.");

            DmiActions.ShowInstruction(this, @"Press the <Delete> key");

            // Repeat for the <9> button
            DmiActions.ShowInstruction(this, @"Press and hold the ‘9’ key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘9’ key is displayed pressed and immediately re-displayed enabled." +
                                Environment.NewLine +
                                @"2. The ‘Click’ sound is played once." + Environment.NewLine +
                                @"3. The ‘Hour’ data input field displays ‘09’." + Environment.NewLine +
                                "4. The cursor is displayed after the ‘9’." + Environment.NewLine +
                                "5. The data input field is used to enter the ‘Hour’." + Environment.NewLine +
                                "6. The local time can be edited." + Environment.NewLine +
                                "7. The date and time echo texts are not updated.");

            DmiActions.ShowInstruction(this, @"Release the key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘9’ key is displayed enabled.");

            TraceHeader("Test Step 16");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Delete the old value and enter the new value ‘11’ for Hour.Then, confirm an entered data by pressing an input field");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following information,Input fields(1)The associated ‘Enter’ button is data field itself.(2)An input field is used to allow the driver to enter data.(3)The state of ‘Hour’ input field is changed to ‘accepted’ as follows,The background colour of the Data Area is dark-grey.The colour of data value is white.(4)The next input field ‘Minute’ is in state ‘selected’ as follows,The background colour of the Data Area is medium-grey.The colour of data value is black.Echo Texts(5)The echo text of ‘Hour ‘is changed to white colour.(6)The value of echo text is changed refer to entered data.Entering Characters (7)The cursor is displayed as a horizontal line below the position of the next character to be entered.(8)The cursor is flashed by changing from visible to not visible.Keyboard(9)The keyboard associated to selected input field ‘Minute’ is Numeric keyboard.(10)The keyboard contains enabled button for the number <1> to <9>, <Delete>(NA21) , <0> and disabled <Decimal_Separator>. NA21, Delete button");
            /*
            Test Step 16
            Action: Delete the old value and enter the new value ‘11’ for Hour.Then, confirm an entered data by pressing an input field
            Expected Result: Verify the following information,Input fields(1)The associated ‘Enter’ button is data field itself.(2)An input field is used to allow the driver to enter data.(3)The state of ‘Hour’ input field is changed to ‘accepted’ as follows,The background colour of the Data Area is dark-grey.The colour of data value is white.(4)The next input field ‘Minute’ is in state ‘selected’ as follows,The background colour of the Data Area is medium-grey.The colour of data value is black.Echo Texts(5)The echo text of ‘Hour ‘is changed to white colour.(6)The value of echo text is changed refer to entered data.Entering Characters (7)The cursor is displayed as a horizontal line below the position of the next character to be entered.(8)The cursor is flashed by changing from visible to not visible.Keyboard(9)The keyboard associated to selected input field ‘Minute’ is Numeric keyboard.(10)The keyboard contains enabled button for the number <1> to <9>, <Delete>(NA21) , <0> and disabled <Decimal_Separator>. NA21, Delete button
            Test Step Comment: (1) MMI_gen 11942 (partly: MMI_gen 4682 (partly: Hour));(2) MMI_gen 11942 (partly: MMI_gen 4634 (partly: Hour));(3) MMI_gen 11942 (partly: MMI_gen 4652 (partly: Hour), MMI_gen 4684 (partly: accepted, Hour));(4) MMI_gen 11942 (partly: MMI_gen 4684 (partly: Minute, selected automatically), MMI_gen 4651 (partly: Minute));(5) MMI_gen 11942 (partly: MMI_gen 4700 (partly: Hour));(6) MMI_gen 11942 (partly: MMI_gen 4681 (partly: Hour), MMI_gen 4890, MMI_gen 4698);(7) MMI_gen 11942 (partly: MMI_gen 4689, MMI_gen 4690);(8) MMI_gen 11942 (partly: MMI_gen 4691 (partly: flash, Minute));(9) ) MMI_gen 11944  (partly: Minute); MMI_gen 11942 (partly: MMI_gen 4912 (partly: Minute), MMI_gen 4678 (partly: Minute));(10) MMI_gen 11942 (partly: MMI_gen 5003 (partly: Minute)); MMI_gen 4392 (partly: [Delete] NA21);
            */
            DmiActions.ShowInstruction(this,
                @"Delete the value in the ‘Hour’ data input field and enter ‘11’, then confirm the value by pressing the ‘Day’ data input field");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The data input field is an ‘Enter’ button." + Environment.NewLine +
                                "2. The data input field can be used to enter data." + Environment.NewLine +
                                @"3. The ‘Hour’ data input field is displayed ‘Accepted’, with the value in white on a Dark-grey background." +
                                Environment.NewLine +
                                @"4. The next data input field (‘Minute’) is  ‘Selected’, with the value in black on a Medium-grey background." +
                                Environment.NewLine +
                                @"5. A corresponding numeric keypad is displayed for the next data input field." +
                                Environment.NewLine +
                                @"6. The keypad contains enabled keys for the numbers <1> to <9>, <Delete> (symbol NA21), <0> and a disabled <Decimal_Separator> key." +
                                Environment.NewLine +
                                @"7. The ‘Hour’ echo text displays ‘12’ in white." + Environment.NewLine +
                                "8. A flashing (visible/invisible) underscore is displayed as a cursor after the last character of the data input field.");

            TraceHeader("Test Step 17");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Perform action step 2-7 for keypad of the ‘Minute’ input field");
            TraceReport("Expected Result");
            TraceInfo(
                "See the expected results of Step 2 – Step 7 and the following additional information,(1)The pressed key is added in an input field immediately. (2)The cursor is jumped to next position after entered the character immediately.(3)The input field is used to enter the Minute");
            /*
            Test Step 17
            Action: Perform action step 2-7 for keypad of the ‘Minute’ input field
            Expected Result: See the expected results of Step 2 – Step 7 and the following additional information,(1)The pressed key is added in an input field immediately. (2)The cursor is jumped to next position after entered the character immediately.(3)The input field is used to enter the Minute
            Test Step Comment: (1) MMI_gen 11942 (partly: MMI_gen 4642 (partly: Minute));  (2) MMI_gen 11942 (partly: MMI_gen 4692 (partly: Minute));  (3) MMI_gen 11957 (partly: entry);
            */
            // Surely steps 5 and 6
            DmiActions.ShowInstruction(this, @"Press and hold the ‘0’ key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘0’ key is displayed pressed and immediately re-displayed enabled." +
                                Environment.NewLine +
                                @"2. The ‘Click’ sound is played once." + Environment.NewLine +
                                @"3. The ‘Minute’ data input field displays ‘0’." + Environment.NewLine +
                                "4. The cursor is displayed after the ‘0’." + Environment.NewLine +
                                "5. The data input field is used to enter the ‘Minute’ value." + Environment.NewLine +
                                "6. The local time can be edited." + Environment.NewLine +
                                "7. The date and time echo texts stop being updated.");

            DmiActions.ShowInstruction(this, @"Release the key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘0’ key is displayed enabled.");

            // Repeat for the <1> key
            DmiActions.ShowInstruction(this, @"Press and hold the ‘1’ key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘0’ key is displayed pressed and immediately re-displayed enabled." +
                                Environment.NewLine +
                                @"2. The ‘Click’ sound is played once." + Environment.NewLine +
                                @"3. The ‘Minute’ data input field displays ‘01’." + Environment.NewLine +
                                "4. The cursor is displayed after the ‘1’." + Environment.NewLine +
                                "5. The data input field is used to enter the ‘Minute’." + Environment.NewLine +
                                "6. The local time can be edited." + Environment.NewLine +
                                "7. The date and time echo texts are not updated.");

            DmiActions.ShowInstruction(this, @"Release the key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘1’ key is displayed enabled.");

            DmiActions.ShowInstruction(this, @"Press the <Delete> key");

            // Repeat for the <2> key
            DmiActions.ShowInstruction(this, @"Press and hold the ‘2’ key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘2’ key is displayed pressed and immediately re-displayed enabled." +
                                Environment.NewLine +
                                @"2. The ‘Click’ sound is played once." + Environment.NewLine +
                                @"3. The ‘Minute’ data input field displays ‘02’." + Environment.NewLine +
                                "4. The cursor is displayed after the ‘2’." + Environment.NewLine +
                                "5. The data input field is used to enter the ‘Minute’." + Environment.NewLine +
                                "6. The local time can be edited." + Environment.NewLine +
                                "7. The date and time echo texts are not updated.");

            DmiActions.ShowInstruction(this, @"Release the key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘2’ key is displayed enabled.");

            // Repeat for the <3> button
            DmiActions.ShowInstruction(this, @"Press and hold the ‘3’ key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘3’ key is displayed pressed and immediately re-displayed enabled." +
                                Environment.NewLine +
                                @"2. The ‘Click’ sound is played once." + Environment.NewLine +
                                @"3. The ‘Minute’ data input field displays ‘03’." + Environment.NewLine +
                                "4. The cursor is displayed after the ‘3’." + Environment.NewLine +
                                "5. The data input field is used to enter the ‘Minute’." + Environment.NewLine +
                                "6. The local time can be edited." + Environment.NewLine +
                                "7. The date and time echo texts are not updated.");

            DmiActions.ShowInstruction(this, @"Release the key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘3’ key is displayed enabled.");

            DmiActions.ShowInstruction(this, @"Press the <Delete> key");

            // Repeat for the <4> button
            DmiActions.ShowInstruction(this, @"Press and hold the ‘4’ key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘3’ key is displayed pressed and immediately re-displayed enabled." +
                                Environment.NewLine +
                                @"2. The ‘Click’ sound is played once." + Environment.NewLine +
                                @"3. The ‘Minute’ data input field displays ‘04’." + Environment.NewLine +
                                "4. The cursor is displayed after the ‘4’." + Environment.NewLine +
                                "5. The data input field is used to enter the ‘Minute’." + Environment.NewLine +
                                "6. The local time can be edited." + Environment.NewLine +
                                "7. The date and time echo texts are not updated.");

            DmiActions.ShowInstruction(this, @"Release the key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘4’ key is displayed enabled.");

            DmiActions.ShowInstruction(this, @"Press the <Delete> key");

            // Repeat for the <5> button
            DmiActions.ShowInstruction(this, @"Press and hold the ‘5’ key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘4’ key is displayed pressed and immediately re-displayed enabled." +
                                Environment.NewLine +
                                @"2. The ‘Click’ sound is played once." + Environment.NewLine +
                                @"3. The ‘Minute’ data input field displays ‘05’." + Environment.NewLine +
                                "4. The cursor is displayed after the ‘5’." + Environment.NewLine +
                                "5. The data input field is used to enter the ‘Minute’." + Environment.NewLine +
                                "6. The local time can be edited." + Environment.NewLine +
                                "7. The date and time echo texts are not updated.");

            DmiActions.ShowInstruction(this, @"Release the key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘5’ key is displayed enabled.");

            DmiActions.ShowInstruction(this, @"Press the <Delete> key");

            // Repeat for the <6> button
            DmiActions.ShowInstruction(this, @"Press and hold the ‘6’ key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘6’ key is displayed pressed and immediately re-displayed enabled." +
                                Environment.NewLine +
                                @"2. The ‘Click’ sound is played once." + Environment.NewLine +
                                @"3. The ‘Minute’ data input field displays ‘06’." + Environment.NewLine +
                                "4. The cursor is displayed after the ‘6’." + Environment.NewLine +
                                "5. The data input field is used to enter the ‘Minute’." + Environment.NewLine +
                                "6. The local time can be edited." + Environment.NewLine +
                                "7. The date and time echo texts are not updated.");

            DmiActions.ShowInstruction(this, @"Release the key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘6’ key is displayed enabled.");


            DmiActions.ShowInstruction(this, @"Press the <Delete> key");

            // Repeat for the <7> button
            DmiActions.ShowInstruction(this, @"Press and hold the ‘7’ key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘7’ key is displayed pressed and immediately re-displayed enabled." +
                                Environment.NewLine +
                                @"2. The ‘Click’ sound is played once." + Environment.NewLine +
                                @"3. The ‘Minute’ data input field displays ‘07’." + Environment.NewLine +
                                "4. The cursor is displayed after the ‘7’." + Environment.NewLine +
                                "5. The data input field is used to enter the ‘Minute’." + Environment.NewLine +
                                "6. The local time can be edited." + Environment.NewLine +
                                "7. The date and time echo texts are not updated.");

            DmiActions.ShowInstruction(this, @"Release the key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘7’ key is displayed enabled.");

            DmiActions.ShowInstruction(this, @"Press the <Delete> key");

            // Repeat for the <8> button
            DmiActions.ShowInstruction(this, @"Press and hold the ‘8’ key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘8’ key is displayed pressed and immediately re-displayed enabled." +
                                Environment.NewLine +
                                @"2. The ‘Click’ sound is played once." + Environment.NewLine +
                                @"3. The ‘Minute’ data input field displays ‘08’." + Environment.NewLine +
                                "4. The cursor is displayed after the ‘8’." + Environment.NewLine +
                                "5. The data input field is used to enter the ‘Minute’." + Environment.NewLine +
                                "6. The local time can be edited." + Environment.NewLine +
                                "7. The date and time echo texts are not updated.");

            DmiActions.ShowInstruction(this, @"Release the key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘8’ key is displayed enabled.");

            DmiActions.ShowInstruction(this, @"Press the <Delete> key");

            // Repeat for the <9> button
            DmiActions.ShowInstruction(this, @"Press and hold the ‘9’ key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘9’ key is displayed pressed and immediately re-displayed enabled." +
                                Environment.NewLine +
                                @"2. The ‘Click’ sound is played once." + Environment.NewLine +
                                @"3. The ‘Minute’ data input field displays ‘09’." + Environment.NewLine +
                                "4. The cursor is displayed after the ‘9’." + Environment.NewLine +
                                "5. The data input field is used to enter the ‘Minute’." + Environment.NewLine +
                                "6. The local time can be edited." + Environment.NewLine +
                                "7. The date and time echo texts are not updated.");

            DmiActions.ShowInstruction(this, @"Release the key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘9’ key is displayed enabled.");

            TraceHeader("Test Step 18");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Delete the old value and enter the new value ‘11’ for Minute.Then, confirm an entered data by pressing an input field");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following information,Input fields(1)The associated ‘Enter’ button is data field itself.(2)An input field is used to allow the driver to enter data.(3)The state of ‘Minute’ input field is changed to ‘accepted’ as follows,The background colour of the Data Area is dark-grey.The colour of data value is white.(4)The next input field ‘Second’ is in state ‘selected’ as follows,The background colour of the Data Area is medium-grey.The colour of data value is black.Echo Texts(5)The echo text of ‘Minute ‘is changed to white colour.(6)The value of echo text is changed refer to entered data.Entering Characters (7)The cursor is displayed as a horizontal line below the position of the next character to be entered.(8)The cursor is flashed by changing from visible to not visible.Keyboard(9)The keyboard associated to selected input field ‘Second’ is Numeric keyboard.(10)The keyboard contains enabled button for the number <1> to <9>, <Delete>(NA21) , <0> and disabled <Decimal_Separator>. NA21, Delete button");
            /*
            Test Step 18
            Action: Delete the old value and enter the new value ‘11’ for Minute.Then, confirm an entered data by pressing an input field
            Expected Result: Verify the following information,Input fields(1)The associated ‘Enter’ button is data field itself.(2)An input field is used to allow the driver to enter data.(3)The state of ‘Minute’ input field is changed to ‘accepted’ as follows,The background colour of the Data Area is dark-grey.The colour of data value is white.(4)The next input field ‘Second’ is in state ‘selected’ as follows,The background colour of the Data Area is medium-grey.The colour of data value is black.Echo Texts(5)The echo text of ‘Minute ‘is changed to white colour.(6)The value of echo text is changed refer to entered data.Entering Characters (7)The cursor is displayed as a horizontal line below the position of the next character to be entered.(8)The cursor is flashed by changing from visible to not visible.Keyboard(9)The keyboard associated to selected input field ‘Second’ is Numeric keyboard.(10)The keyboard contains enabled button for the number <1> to <9>, <Delete>(NA21) , <0> and disabled <Decimal_Separator>. NA21, Delete button
            Test Step Comment: (1) MMI_gen 11942 (partly: MMI_gen 4682 (partly: Minute));(2) MMI_gen 11942 (partly: MMI_gen 4634 (partly: Minute));(3) MMI_gen 11942 (partly: MMI_gen 4652 (partly: Minute), MMI_gen 4684 (partly: accepted, Minute));(4) MMI_gen 11942 (partly: MMI_gen 4684 (partly: Second, selected automatically), MMI_gen 4651 (partly: Second));(5) MMI_gen 11942 (partly: MMI_gen 4700 (partly: Minute));(6) MMI_gen 11942 (partly: MMI_gen 4681 (partly: Minute), MMI_gen 4890, MMI_gen 4698);(7) MMI_gen 11942 (partly: MMI_gen 4689, MMI_gen 4690);(8) MMI_gen 11942 (partly: MMI_gen 4691 (partly: flash, Second));(9) ) MMI_gen 11944  (partly: Second); MMI_gen 11942 (partly: MMI_gen 4912 (partly: Second), MMI_gen 4678 (partly: Second));(10) MMI_gen 11942 (partly: MMI_gen 5003 (partly: Second)); MMI_gen 4392 (partly: [Delete] NA21);
            */
            DmiActions.ShowInstruction(this,
                @"Delete the value in the ‘Minute’ data input field and enter ‘11’, then confirm the value by pressing the ‘Day’ data input field");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The data input field is an ‘Enter’ button." + Environment.NewLine +
                                "2. The data input field can be used to enter data." + Environment.NewLine +
                                @"3. The ‘Minute’ data input field is displayed ‘Accepted’, with the value in white on a Dark-grey background." +
                                Environment.NewLine +
                                @"4. The next data input field (‘Second’) is  ‘Selected’, with the value in black on a Medium-grey background." +
                                Environment.NewLine +
                                @"5. A corresponding numeric keypad is displayed for the next data input field." +
                                Environment.NewLine +
                                @"6. The keypad contains enabled keys for the numbers <1> to <9>, <Delete> (symbol NA21), <0> and a disabled <Decimal_Separator> key." +
                                Environment.NewLine +
                                @"7. The ‘Minute’ echo text displays ‘12’ in white." + Environment.NewLine +
                                "8. A flashing (visible/invisible) underscore is displayed as a cursor after the last character of the data input field.");

            TraceHeader("Test Step 19");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Perform action step 2-7 for keypad of the ‘Second’ input field");
            TraceReport("Expected Result");
            TraceInfo(
                "See the expected results of Step 2 – Step 7 and the following additional information,(1)The pressed key is added in an input field immediately. (2)The cursor is jumped to next position after entered the character immediately.(3)The input field is used to enter the Second");
            /*
            Test Step 19
            Action: Perform action step 2-7 for keypad of the ‘Second’ input field
            Expected Result: See the expected results of Step 2 – Step 7 and the following additional information,(1)The pressed key is added in an input field immediately. (2)The cursor is jumped to next position after entered the character immediately.(3)The input field is used to enter the Second
            Test Step Comment: (1) MMI_gen 11942 (partly: MMI_gen 4642 (partly: Second));  (2) MMI_gen 11942 (partly: MMI_gen 4692 (partly: Second));  (3) MMI_gen 11958 (partly: entry);
            */
            // Surely steps 5 and 6
            DmiActions.ShowInstruction(this, @"Press and hold the ‘0’ key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘0’ key is displayed pressed and immediately re-displayed enabled." +
                                Environment.NewLine +
                                @"2. The ‘Click’ sound is played once." + Environment.NewLine +
                                @"3. The ‘Second’ data input field displays ‘0’." + Environment.NewLine +
                                "4. The cursor is displayed after the ‘0’." + Environment.NewLine +
                                "5. The data input field is used to enter the ‘Second’ value." + Environment.NewLine +
                                "6. The local time can be edited." + Environment.NewLine +
                                "7. The date and time echo texts stop being updated.");

            DmiActions.ShowInstruction(this, @"Release the key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘0’ key is displayed enabled.");

            // Repeat for the <1> key
            DmiActions.ShowInstruction(this, @"Press and hold the ‘1’ key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘0’ key is displayed pressed and immediately re-displayed enabled." +
                                Environment.NewLine +
                                @"2. The ‘Click’ sound is played once." + Environment.NewLine +
                                @"3. The ‘Second’ data input field displays ‘01’." + Environment.NewLine +
                                "4. The cursor is displayed after the ‘1’." + Environment.NewLine +
                                "5. The data input field is used to enter the ‘Second’." + Environment.NewLine +
                                "6. The local time can be edited." + Environment.NewLine +
                                "7. The date and time echo texts are not updated.");

            DmiActions.ShowInstruction(this, @"Release the key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘1’ key is displayed enabled.");

            DmiActions.ShowInstruction(this, @"Press the <Delete> key");

            // Repeat for the <2> key
            DmiActions.ShowInstruction(this, @"Press and hold the ‘2’ key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘2’ key is displayed pressed and immediately re-displayed enabled." +
                                Environment.NewLine +
                                @"2. The ‘Click’ sound is played once." + Environment.NewLine +
                                @"3. The ‘Second’ data input field displays ‘02’." + Environment.NewLine +
                                "4. The cursor is displayed after the ‘2’." + Environment.NewLine +
                                "5. The data input field is used to enter the ‘Second’." + Environment.NewLine +
                                "6. The local time can be edited." + Environment.NewLine +
                                "7. The date and time echo texts are not updated.");

            DmiActions.ShowInstruction(this, @"Release the key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘2’ key is displayed enabled.");

            // Repeat for the <3> button
            DmiActions.ShowInstruction(this, @"Press and hold the ‘3’ key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘3’ key is displayed pressed and immediately re-displayed enabled." +
                                Environment.NewLine +
                                @"2. The ‘Click’ sound is played once." + Environment.NewLine +
                                @"3. The ‘Second’ data input field displays ‘03’." + Environment.NewLine +
                                "4. The cursor is displayed after the ‘3’." + Environment.NewLine +
                                "5. The data input field is used to enter the ‘Second’." + Environment.NewLine +
                                "6. The local time can be edited." + Environment.NewLine +
                                "7. The date and time echo texts are not updated.");

            DmiActions.ShowInstruction(this, @"Release the key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘3’ key is displayed enabled.");

            DmiActions.ShowInstruction(this, @"Press the <Delete> key");

            // Repeat for the <4> button
            DmiActions.ShowInstruction(this, @"Press and hold the ‘4’ key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘3’ key is displayed pressed and immediately re-displayed enabled." +
                                Environment.NewLine +
                                @"2. The ‘Click’ sound is played once." + Environment.NewLine +
                                @"3. The ‘Second’ data input field displays ‘04’." + Environment.NewLine +
                                "4. The cursor is displayed after the ‘4’." + Environment.NewLine +
                                "5. The data input field is used to enter the ‘Second’." + Environment.NewLine +
                                "6. The local time can be edited." + Environment.NewLine +
                                "7. The date and time echo texts are not updated.");

            DmiActions.ShowInstruction(this, @"Release the key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘4’ key is displayed enabled.");

            DmiActions.ShowInstruction(this, @"Press the <Delete> key");

            // Repeat for the <5> button
            DmiActions.ShowInstruction(this, @"Press and hold the ‘5’ key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘4’ key is displayed pressed and immediately re-displayed enabled." +
                                Environment.NewLine +
                                @"2. The ‘Click’ sound is played once." + Environment.NewLine +
                                @"3. The ‘Second’ data input field displays ‘05’." + Environment.NewLine +
                                "4. The cursor is displayed after the ‘5’." + Environment.NewLine +
                                "5. The data input field is used to enter the ‘Second’." + Environment.NewLine +
                                "6. The local time can be edited." + Environment.NewLine +
                                "7. The date and time echo texts are not updated.");

            DmiActions.ShowInstruction(this, @"Release the key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘5’ key is displayed enabled.");

            DmiActions.ShowInstruction(this, @"Press the <Delete> key");

            // Repeat for the <6> button
            DmiActions.ShowInstruction(this, @"Press and hold the ‘6’ key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘6’ key is displayed pressed and immediately re-displayed enabled." +
                                Environment.NewLine +
                                @"2. The ‘Click’ sound is played once." + Environment.NewLine +
                                @"3. The ‘Second’ data input field displays ‘06’." + Environment.NewLine +
                                "4. The cursor is displayed after the ‘6’." + Environment.NewLine +
                                "5. The data input field is used to enter the ‘Second’." + Environment.NewLine +
                                "6. The local time can be edited." + Environment.NewLine +
                                "7. The date and time echo texts are not updated.");

            DmiActions.ShowInstruction(this, @"Release the key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘6’ key is displayed enabled.");


            DmiActions.ShowInstruction(this, @"Press the <Delete> key");

            // Repeat for the <7> button
            DmiActions.ShowInstruction(this, @"Press and hold the ‘7’ key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘7’ key is displayed pressed and immediately re-displayed enabled." +
                                Environment.NewLine +
                                @"2. The ‘Click’ sound is played once." + Environment.NewLine +
                                @"3. The ‘Second’ data input field displays ‘07’." + Environment.NewLine +
                                "4. The cursor is displayed after the ‘7’." + Environment.NewLine +
                                "5. The data input field is used to enter the ‘Second’." + Environment.NewLine +
                                "6. The local time can be edited." + Environment.NewLine +
                                "7. The date and time echo texts are not updated.");

            DmiActions.ShowInstruction(this, @"Release the key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘7’ key is displayed enabled.");

            DmiActions.ShowInstruction(this, @"Press the <Delete> key");

            // Repeat for the <8> button
            DmiActions.ShowInstruction(this, @"Press and hold the ‘8’ key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘8’ key is displayed pressed and immediately re-displayed enabled." +
                                Environment.NewLine +
                                @"2. The ‘Click’ sound is played once." + Environment.NewLine +
                                @"3. The ‘Second’ data input field displays ‘08’." + Environment.NewLine +
                                "4. The cursor is displayed after the ‘8’." + Environment.NewLine +
                                "5. The data input field is used to enter the ‘Second’." + Environment.NewLine +
                                "6. The local time can be edited." + Environment.NewLine +
                                "7. The date and time echo texts are not updated.");

            DmiActions.ShowInstruction(this, @"Release the key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘8’ key is displayed enabled.");

            DmiActions.ShowInstruction(this, @"Press the <Delete> key");

            // Repeat for the <9> button
            DmiActions.ShowInstruction(this, @"Press and hold the ‘9’ key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘9’ key is displayed pressed and immediately re-displayed enabled." +
                                Environment.NewLine +
                                @"2. The ‘Click’ sound is played once." + Environment.NewLine +
                                @"3. The ‘Second’ data input field displays ‘09’." + Environment.NewLine +
                                "4. The cursor is displayed after the ‘9’." + Environment.NewLine +
                                "5. The data input field is used to enter the ‘Second’." + Environment.NewLine +
                                "6. The local time can be edited." + Environment.NewLine +
                                "7. The date and time echo texts are not updated.");

            DmiActions.ShowInstruction(this, @"Release the key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘9’ key is displayed enabled.");

            TraceHeader("Test Step 20");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Delete the old value and enter the new value ‘11’ for Second.Then, confirm an entered data by pressing an input field");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following information,Input fields(1)The associated ‘Enter’ button is data field itself.(2)An input field is used to allow the driver to enter data.(3)The state of ‘Second’ input field is changed to ‘accepted’ as follows,The background colour of the Data Area is dark-grey.The colour of data value is white.(4)The next input field ‘Offset’ is in state ‘selected’ as follows,The background colour of the Data Area is medium-grey.The colour of data value is black.Echo Texts(5)The echo text of ‘Second ‘is changed to white colour.(6)The value of echo text is changed refer to entered data.Entering Characters (7)The cursor is displayed as a horizontal line below the position of the next character to be entered.(8)The cursor is flashed by changing from visible to not visible.Keyboard(9)The keyboard associated to selected input field ‘Offset’ is Dedicated keyboard. (10)The label of key#1 is ‘+’ and the label of key#2 is ‘-‘");
            /*
            Test Step 20
            Action: Delete the old value and enter the new value ‘11’ for Second.Then, confirm an entered data by pressing an input field
            Expected Result: Verify the following information,Input fields(1)The associated ‘Enter’ button is data field itself.(2)An input field is used to allow the driver to enter data.(3)The state of ‘Second’ input field is changed to ‘accepted’ as follows,The background colour of the Data Area is dark-grey.The colour of data value is white.(4)The next input field ‘Offset’ is in state ‘selected’ as follows,The background colour of the Data Area is medium-grey.The colour of data value is black.Echo Texts(5)The echo text of ‘Second ‘is changed to white colour.(6)The value of echo text is changed refer to entered data.Entering Characters (7)The cursor is displayed as a horizontal line below the position of the next character to be entered.(8)The cursor is flashed by changing from visible to not visible.Keyboard(9)The keyboard associated to selected input field ‘Offset’ is Dedicated keyboard. (10)The label of key#1 is ‘+’ and the label of key#2 is ‘-‘
            Test Step Comment: (1) MMI_gen 11942 (partly: MMI_gen 4682 (partly: Second));(2) MMI_gen 11942 (partly: MMI_gen 4634 (partly: Second));(3) MMI_gen 11942 (partly: MMI_gen 4652 (partly: Second), MMI_gen 4684 (partly: accepted, Second));(4) MMI_gen 11942 (partly: MMI_gen 4684 (partly: Offset, selected automatically), MMI_gen 4651 (partly: Offset));(5) MMI_gen 11942 (partly: MMI_gen 4700 (partly: Second));(6) MMI_gen 11942 (partly: MMI_gen 4681 (partly: Second), MMI_gen 4890, MMI_gen 4698);(7) MMI_gen 11942 (partly: MMI_gen 4689, MMI_gen 4690);(8) MMI_gen 11942 (partly: MMI_gen 4691 (partly: flash, Offset));(9) MMI_gen 11944  (partly: Offset); MMI_gen 11942 (partly: MMI_gen 4912 (partly: except Offset), MMI_gen 4678 (partly: Offset)); MMI_gen 11945 (partly: dedicated keyboard);(10) MMI_gen 11945 (partly: ‘+’ and ‘-‘ keys);
            */
            DmiActions.ShowInstruction(this,
                @"Delete the value in the ‘Second’ data input field and enter ‘11’, then confirm the value by pressing the ‘Day’ data input field");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The data input field is an ‘Enter’ button." + Environment.NewLine +
                                "2. The data input field can be used to enter data." + Environment.NewLine +
                                @"3. The ‘Second’ data input field is displayed ‘Accepted’, with the value in white on a Dark-grey background." +
                                Environment.NewLine +
                                @"4. The next data input field (‘Offset’) is  ‘Selected’, with the value in black on a Medium-grey background." +
                                Environment.NewLine +
                                @"5. A corresponding numeric keypad is displayed for the next data input field." +
                                Environment.NewLine +
                                @"6. The keypad contains enabled keys <+> and <->." + Environment.NewLine +
                                @"7. The ‘Second’ echo text displays ‘12’ in white." + Environment.NewLine +
                                "8. A flashing (visible/invisible) underscore is displayed as a cursor after the last character of the data input field.");

            TraceHeader("Test Step 21");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Perform action step 2-7 for keypad of ‘+’ button for the ‘Offset’ input field");
            TraceReport("Expected Result");
            TraceInfo(
                "See the expected results of Step 2 – Step 7 and the following additional information,(1)The value of offset time is increased 15 minute. (2)The input field is used to enter the Offset.(3)   The Offset time is editable refer to received packet EVC-30 with variable and MMI_Q_REQUEST_ENABLE_64 #25 = 1 and MMI_Q_REQUEST_ENABLE_64 #26 = 1");
            /*
            Test Step 21
            Action: Perform action step 2-7 for keypad of ‘+’ button for the ‘Offset’ input field
            Expected Result: See the expected results of Step 2 – Step 7 and the following additional information,(1)The value of offset time is increased 15 minute. (2)The input field is used to enter the Offset.(3)   The Offset time is editable refer to received packet EVC-30 with variable and MMI_Q_REQUEST_ENABLE_64 #25 = 1 and MMI_Q_REQUEST_ENABLE_64 #26 = 1
            Test Step Comment: (1) MMI_gen 11942 (partly: MMI_gen 4642 (partly: Offset));  MMI_gen 11945 (partly: increase with 15 minute);(2) MMI_gen 11954 (partly: entry);(3) MMI_gen 2450;
            */
            DmiActions.ShowInstruction(this, @"Press and hold the ‘+’ key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The previous time displayed is incremented by 15 minutes." + Environment.NewLine +
                                "2. The data input fields are used to enter the Offset.");

            DmiActions.ShowInstruction(this, @"Release the key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘+’ key is displayed enabled.");

            TraceHeader("Test Step 22");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Press ‘+’ button until the value is increased to ‘+12:00’.Then, press ‘+’ button again");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following information,(1) The value of offset time still not changes and display as ‘+12:00’");
            /*
            Test Step 22
            Action: Press ‘+’ button until the value is increased to ‘+12:00’.Then, press ‘+’ button again
            Expected Result: Verify the following information,(1) The value of offset time still not changes and display as ‘+12:00’
            Test Step Comment: (1) MMI_gen 11945 (partly: offset range +12:00);
            */
            DmiActions.ShowInstruction(this,
                @"Press the ‘+’ key repeatedly until the offset is displayed as ‘+12:00’, then press the ‘+’ key again.");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The offset time stays displayed as ‘+12:00’.");

            TraceHeader("Test Step 23");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Perform action step 2-7 for keypad of ‘-’ button for the ‘Offset’ input field");
            TraceReport("Expected Result");
            TraceInfo(
                "See the expected results of Step 2 – Step 7 and the following additional information,(1)The value of offset time is decreased 15 minute. (2)The input field is used to enter the Offset");
            /*
            Test Step 23
            Action: Perform action step 2-7 for keypad of ‘-’ button for the ‘Offset’ input field
            Expected Result: See the expected results of Step 2 – Step 7 and the following additional information,(1)The value of offset time is decreased 15 minute. (2)The input field is used to enter the Offset
            Test Step Comment: (1) MMI_gen 11942 (partly: MMI_gen 4642 (partly: Offset));  MMI_gen 11945 (partly: decrease with 15 minute);(2) MMI_gen 11954 (partly: entry);
            */
            DmiActions.ShowInstruction(this, @"Press and hold the ‘-’ key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The previous time displayed is decremented by 15 minutes." + Environment.NewLine +
                                "2. The data input fields are used to enter the Offset.");

            DmiActions.ShowInstruction(this, @"Release the key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘-’ key is displayed enabled.");

            TraceHeader("Test Step 24");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Press ‘+’ button until the value is increased to ‘-12:00’.Then, press ‘-’ button again");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following information,(1) The value of offset time is still not changes and it is displayed as ‘-12:00’");
            /*
            Test Step 24
            Action: Press ‘+’ button until the value is increased to ‘-12:00’.Then, press ‘-’ button again
            Expected Result: Verify the following information,(1) The value of offset time is still not changes and it is displayed as ‘-12:00’
            Test Step Comment: (1) MMI_gen 11945 (partly: offset range -12:00);
            */
            // Test says press - key (Doh!)
            DmiActions.ShowInstruction(this,
                @"Press the ‘-’ key repeatedly until the offset is displayed as ‘-012:00’, then press the ‘-’ key again.");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The offset time stays displayed as ‘-12:00’.");

            TraceHeader("Test Step 25");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Confirm an entered data by pressing an input field");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following information,Input fields(1)The associated ‘Enter’ button is data field itself.(2)An input field is used to allow the driver to enter data.(3)The state of ‘Offset’ input field is changed to ‘accepted’ as follows,The background colour of the Data Area is dark-grey.The colour of data value is white.(4)There is no input field selected.Echo Texts(5)The echo text of ‘Offset ‘is changed to white colour.(6)The value of echo text is changed refer to entered data.Data Entry Window(7)The state of ‘Yes’ button below text label ‘Clock set?’ is enabled as follows,The background colour of the Data Area is medium-grey.The colour of data value is black.The colour of border is medium-grey");
            /*
            Test Step 25
            Action: Confirm an entered data by pressing an input field
            Expected Result: Verify the following information,Input fields(1)The associated ‘Enter’ button is data field itself.(2)An input field is used to allow the driver to enter data.(3)The state of ‘Offset’ input field is changed to ‘accepted’ as follows,The background colour of the Data Area is dark-grey.The colour of data value is white.(4)There is no input field selected.Echo Texts(5)The echo text of ‘Offset ‘is changed to white colour.(6)The value of echo text is changed refer to entered data.Data Entry Window(7)The state of ‘Yes’ button below text label ‘Clock set?’ is enabled as follows,The background colour of the Data Area is medium-grey.The colour of data value is black.The colour of border is medium-grey
            Test Step Comment: (1) MMI_gen 11942 (partly: MMI_gen 4682 (partly: Offset));(2) MMI_gen 11942 (partly: MMI_gen 4634 (partly: Offset));(3) MMI_gen 11942 (partly: MMI_gen 4652 (partly: Offset), MMI_gen 4684 (partly: accepted, Offset));(4) MMI_gen 11942 (partly: MMI_gen 4684 (partly: No next input field, data entry process terminated));(5) MMI_gen 11942 (partly: MMI_gen 4700 (partly: Offset));(6) MMI_gen 11942 (partly: MMI_gen 4681 (partly: Offset), MMI_gen 4890, MMI_gen 4698);(7) MMI_gen 11942 (partly: MMI_gen 4909 (partly: Enabled), MMI_gen 4910 (partly: Enabled, MMI_gen 4211 (partly: colour))); MMI_gen 4374;
            */
            DmiActions.ShowInstruction(this, "Confirm the entered data by pressing a data input field");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The data input field is an ‘Enter’ button." + Environment.NewLine +
                                "2. The data input field can be used to enter data." + Environment.NewLine +
                                @"3. The ‘Offset’ data input field is displayed ‘Accepted’, with white text on a Dark-grey background." +
                                Environment.NewLine +
                                @"4. No data input field is displayed ‘Selected’." + Environment.NewLine +
                                @"5. The echo text of the ‘Offset’ data input field displays ‘-12:00’ in white." +
                                Environment.NewLine +
                                "6. The ‘Yes’ button below the label ‘Clock set?’ is displayed enabled, with black text on a Medium-grey background and a Medium-grey border.");

            TraceHeader("Test Step 26");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Press and hold ‘Previous’ button");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following information,(1)The state of button is changed to ‘Pressed’, the border of button is removed.(2)The sound ‘Click’ is played once");
            /*
            Test Step 26
            Action: Press and hold ‘Previous’ button
            Expected Result: Verify the following information,(1)The state of button is changed to ‘Pressed’, the border of button is removed.(2)The sound ‘Click’ is played once
            Test Step Comment: (1) MMI_gen 11942 (partly: MMI_gen 9391 (partly: [Previous], MMI_gen 4381 (partly: change to state ‘Pressed’ as long as remain actuated)); MMI_gen 4375;(2) MMI_gen 11942 (partly: MMI_gen 9391 (partly: [Previous], MMI_gen 4381 (partly: sound ‘Click’)); MMI_gen 9512; MMI_gen 968;
            */
            DmiActions.ShowInstruction(this, @"Press and hold the ‘Previous’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘Previous’ button is displayed pressed, with no border." +
                                Environment.NewLine +
                                @"2. The ‘Click’ sound is played once.");

            TraceHeader("Test Step 27");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Slide out the ‘Previous’ button");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following information,(1)The border of the button is shown (state ‘Enabled’) without a sound");
            /*
            Test Step 27
            Action: Slide out the ‘Previous’ button
            Expected Result: Verify the following information,(1)The border of the button is shown (state ‘Enabled’) without a sound
            Test Step Comment: (1) MMI_gen 11942 (partly: MMI_gen 9391 (partly: [Previous], MMI_gen 4382 (partly: state ‘Enabled’ when slide out with force applied, no sound))); MMI_gen 4374;
            */
            DmiActions.ShowInstruction(this,
                @"Whilst keeping the ‘Previous’ button pressed, drag it back inside its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Previous’ button is displayed enabled, with a border." + Environment.NewLine +
                                "2. No sound is played.");

            TraceHeader("Test Step 28");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Slide back into the ‘Previous’ button");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following information,(1)The border of the button is back to state ‘Pressed’ without a sound");
            /*
            Test Step 28
            Action: Slide back into the ‘Previous’ button
            Expected Result: Verify the following information,(1)The border of the button is back to state ‘Pressed’ without a sound
            Test Step Comment: (1) MMI_gen 11942 (partly: MMI_gen 9391 (partly: [Previous], MMI_gen 4382 (partly: state ‘Pressed’ when slide back, no sound))); MMI_gen 4375;
            */
            DmiActions.ShowInstruction(this,
                @"Whilst keeping the ‘Previous’ button pressed, drag it back inside its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Previous’ button is displayed pressed, without a border." +
                                "2. The ‘Click’ sound is not played.");

            TraceHeader("Test Step 29");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Release ‘Previous’ button");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following information,(1)The state of released button is changed to disabled.(2)   DMI displays the first page of Set clock window");
            /*
            Test Step 29
            Action: Release ‘Previous’ button
            Expected Result: Verify the following information,(1)The state of released button is changed to disabled.(2)   DMI displays the first page of Set clock window
            Test Step Comment: (1) MMI_gen 11942 (partly: MMI_gen 9391 (partly: [Previous], MMI_gen 4381 (partly: exit state ‘pressed’)));(2) MMI_gen 11942 (partly: MMI_gen 9391 (partly: [Previous], MMI_gen 4384 (partly: ETCS-MMI’s function associated to the button));
            */
            DmiActions.ShowInstruction(this, @"Release the ‘Previous’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the first ‘Set clock’ window." + Environment.NewLine +
                                "2. The ‘Previous’ button is displayed disabled.");

            TraceHeader("Test Step 30");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Perform action step 24-27 for ‘Next’ button");
            TraceReport("Expected Result");
            TraceInfo(
                "See the expected results of Step 24 – Step 27 and the following additional information,(1)The state of released button is changed to disabled.(2)   DMI displays the second page of Set clock window");
            /*
            Test Step 30
            Action: Perform action step 24-27 for ‘Next’ button
            Expected Result: See the expected results of Step 24 – Step 27 and the following additional information,(1)The state of released button is changed to disabled.(2)   DMI displays the second page of Set clock window
            Test Step Comment: (1) MMI_gen 11942 (partly: MMI_gen 9391 (partly: [Next], MMI_gen 4381 (partly: exit state ‘pressed’)));(2) MMI_gen 11942 (partly: MMI_gen 9391 (partly: [Next], MMI_gen 4384 (partly: ETCS-MMI’s function associated to the button));
            */
            // Steps 24-27 are not relevant here
            DmiActions.ShowInstruction(this, @"Press and release the ‘Next’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the second ‘Set clock’ window." + Environment.NewLine +
                                "2. The ‘Next’ button is displayed disabled.");

            TraceHeader("Test Step 31");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Perform the following procedure,Press ‘Previous’ button.Select ‘Year’ input field.Enter the new value without pressing an input field.Select ‘Day’ input field");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following information,(1) The state of ‘Yes’ button below text label ‘Clock set?’ is disabled. (2) The state of input field ‘Year’ is changed to ‘Not selected’ as follows,The value of ‘Year’ input field is removed and displayed as blank.The background colour of the input field is dark-grey");
            /*
            Test Step 31
            Action: Perform the following procedure,Press ‘Previous’ button.Select ‘Year’ input field.Enter the new value without pressing an input field.Select ‘Day’ input field
            Expected Result: Verify the following information,(1) The state of ‘Yes’ button below text label ‘Clock set?’ is disabled. (2) The state of input field ‘Year’ is changed to ‘Not selected’ as follows,The value of ‘Year’ input field is removed and displayed as blank.The background colour of the input field is dark-grey
            Test Step Comment: (1) MMI_gen 11942 (partly: MMI_gen 4909 (partly: state selected and with recently entered key), MMI_gen 4680 (partly: value has been modified));(2) MMI_gen 11942 (partly: MMI_gen 4680 (partly: Year, Not selected, Data area is blank), MMI_gen 4649 (partly: data entry, background colour));
            */
            DmiActions.ShowInstruction(this,
                @"Press and release the ‘Previous’ button. Select the ‘Year’ data input field, then enter ‘12’ " +
                Environment.NewLine +
                @"and select the ‘Day’ data input field without accepting the data (pressing a data input field)");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Yes’ button below the label ‘Clock set?’ is displayed disabled." +
                                Environment.NewLine +
                                "2. The ‘Year’ data input field is displayed ‘Not Selected’ with a blank value on a Dark-grey background.");

            TraceHeader("Test Step 32");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Perform the following procedure,Press ‘Next button.Select ‘Minute’ input field.Confirm the value of ‘Minute’ by pressing an input field");
            TraceReport("Expected Result");
            TraceInfo("Verify the following information,(1) The state of input field ‘Year’ is changed to ‘selected’");
            /*
            Test Step 32
            Action: Perform the following procedure,Press ‘Next button.Select ‘Minute’ input field.Confirm the value of ‘Minute’ by pressing an input field
            Expected Result: Verify the following information,(1) The state of input field ‘Year’ is changed to ‘selected’
            Test Step Comment: (1) MMI_gen 11942 (partly: MMI_gen 4685);
            */
            DmiActions.ShowInstruction(this,
                @"Press and release the ‘Next’ button. Select the ‘Minute’ data input field and confirm by pressing a data input field)");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Year’  data input field is displayed ‘Selected’.");

            TraceHeader("Test Step 33");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Enter the value ‘2016’ for ‘Year’.Then, confirm an entered data by pressing an input field");
            TraceReport("Expected Result");
            TraceInfo("The state of ‘Yes’ button is changed to enabled");
            /*
            Test Step 33
            Action: Enter the value ‘2016’ for ‘Year’.Then, confirm an entered data by pressing an input field
            Expected Result: The state of ‘Yes’ button is changed to enabled
            */
            DmiActions.ShowInstruction(this,
                @"Enter the value ‘2016’ for ‘Year’, then confirm the entered value by pressing an input field");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Yes’ button below the label ‘Clock set?’ is displayed enabled.");

            TraceHeader("Test Step 34");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Perform action step 24-27 for ‘Yes’ button");
            TraceReport("Expected Result");
            TraceInfo(
                "See the expected results of Step 24 – Step 27 and the following additional information,(1)DMI displays Settings window. (2)Use the log file to confirm that DMI sent out packet [MMI_SET_TIME_MMI (EVC-109)] with,MMI_T_ZONE_OFFSET = -48MMI_T_UTC = 1481429471");
            /*
            Test Step 34
            Action: Perform action step 24-27 for ‘Yes’ button
            Expected Result: See the expected results of Step 24 – Step 27 and the following additional information,(1)DMI displays Settings window. (2)Use the log file to confirm that DMI sent out packet [MMI_SET_TIME_MMI (EVC-109)] with,MMI_T_ZONE_OFFSET = -48MMI_T_UTC = 1481429471
            Test Step Comment: (1) MMI_gen 11942 (partly: MMI_gen 4911); MMI_gen 11940 (partly: close the window);(2) MMI_gen 11940 (partly: EVC-109); MMI_gen 2451;
            */
            // Presumably Steps 26-29 
            // Repeat Step 26
            DmiActions.ShowInstruction(this, @"Press and hold the ‘Yes’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘Yes’ button is displayed pressed, with no border." + Environment.NewLine +
                                @"2. The ‘Click’ sound is played once.");

            // Repeat Step 27
            DmiActions.ShowInstruction(this, @"Whilst keeping the ‘Yes’ button pressed, drag it back inside its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Yes’ button is displayed enabled, with a border." + Environment.NewLine +
                                "2. No sound is played.");

            // Repeat Step 28
            DmiActions.ShowInstruction(this, @"Whilst keeping the ‘Yes’ button pressed, drag it back inside its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Yes’ button is displayed pressed, without a border." + Environment.NewLine +
                                "2. The ‘Click’ sound is not played.");

            // Repeat Step 29
            DmiActions.ShowInstruction(this, @"Release the ‘Yes’ button");

            //EVC109_MMISetTimeMMI.CheckMmiTZoneOffset = -48;
            //EVC109_MMISetTimeMMI.CheckMmiTUTC = 1481429471;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Settings window.");

            TraceHeader("Test Step 35");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Press ‘Set clock’ button");
            TraceReport("Expected Result");
            TraceInfo("DMI displays Set clock window");
            /*
            Test Step 35
            Action: Press ‘Set clock’ button
            Expected Result: DMI displays Set clock window
            */
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.None;
            EVC30_MMIRequestEnable.Send();

            // The language is changed later...
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Settings; // Settings
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH =
                EVC30_MMIRequestEnable.EnabledRequests.SetLocalTimeDateAndOffset |
                EVC30_MMIRequestEnable.EnabledRequests.SetLocalOffset |
                EVC30_MMIRequestEnable.EnabledRequests.Language;

            EVC30_MMIRequestEnable.Send();

            DmiActions.ShowInstruction(this, @"Press ‘Set clock’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Set clock window");

            TraceHeader("Test Step 36");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Press and hold the Label area of ‘Month’ input field");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following information,(1)    The state of ‘Month’ input field is changed to ‘Pressed’, the border of button is removed.The state of ‘Month’ input field remains ‘not selected’. The state of ‘Year’ input field remains ‘selected’.(2)    The sound ‘Click’ is played once");
            /*
            Test Step 36
            Action: Press and hold the Label area of ‘Month’ input field
            Expected Result: Verify the following information,(1)    The state of ‘Month’ input field is changed to ‘Pressed’, the border of button is removed.The state of ‘Month’ input field remains ‘not selected’. The state of ‘Year’ input field remains ‘selected’.(2)    The sound ‘Click’ is played once
            Test Step Comment: (1) MMI_gen 11942 (partly: MMI_gen 4686 (partly: Label area, Month), MMI_gen 4381 (partly: change to state ‘Pressed’ as long as remain actuated))); MMI_gen 4392 (partly: [Enter], touch screen); MMI_gen 4375;(2) MMI_gen 11942 (partly: MMI_gen 4686 (partly: Label area, Month), MMI_gen 4381 (partly: the sound for Up-Type button));
            */
            DmiActions.ShowInstruction(this, @" Press and hold the Label area of the ‘Month’ data input field");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘Month’ data input field is displayed pressed, without a border." +
                                Environment.NewLine +
                                @"2. The ‘Month’ data input field stays ‘Not Selected’." + Environment.NewLine +
                                @"3. The ‘Year’ data input field stays ‘Selected’.");

            TraceHeader("Test Step 37");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Slide out the Label area of ‘Month’ input field");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following information,(1)    The border of ‘Month’ input field is shown (state ‘Enabled’) without a sound.The state of ‘Month’ input field remains ‘not selected’. The state of ‘Year’ input field remains ‘selected’");
            /*
            Test Step 37
            Action: Slide out the Label area of ‘Month’ input field
            Expected Result: Verify the following information,(1)    The border of ‘Month’ input field is shown (state ‘Enabled’) without a sound.The state of ‘Month’ input field remains ‘not selected’. The state of ‘Year’ input field remains ‘selected’
            Test Step Comment: (1) MMI_gen 11942 (partly: MMI_gen 4686 (partly: Label area, Month), MMI_gen 4382 (partly: state ‘Enabled’ when slide out with force applied, no sound); MMI_gen 4374;
            */
            DmiActions.ShowInstruction(this,
                @"Whilst keeping the Label area of the ‘Month’ data input field pressed, drag it back inside its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘Month’ data input field is displayed enabled, with a border." +
                                Environment.NewLine +
                                @"2. The ‘Month’ data input field stays ‘Not Selected’." + Environment.NewLine +
                                @"3. The ‘Year’ data input field stays ‘Selected’.");

            TraceHeader("Test Step 38");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Slide back into the Label area of ‘Month’ input field");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following information,(1)    The state of ‘Month’ input field is changed to ‘Pressed’, the border of button is removed.The state of ‘Month’ input field remains ‘not selected’. The state of ‘Year’ input field remains ‘selected’");
            /*
            Test Step 38
            Action: Slide back into the Label area of ‘Month’ input field
            Expected Result: Verify the following information,(1)    The state of ‘Month’ input field is changed to ‘Pressed’, the border of button is removed.The state of ‘Month’ input field remains ‘not selected’. The state of ‘Year’ input field remains ‘selected’
            Test Step Comment: (1) MMI_gen 11942 (partly: MMI_gen 4686 (partly: Label area, Month), MMI_gen 4382 (partly: state ‘Pressed’ when slide back, no sound); MMI_gen 4375;
            */
            DmiActions.ShowInstruction(this,
                @"Whilst keeping the Label area of the ‘Month’ data input field pressed, drag it back inside its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Month’ data input field is displayed pressed, without a border." +
                                Environment.NewLine +
                                @"2. The ‘Month’ data input field stays ‘Not Selected’." + Environment.NewLine +
                                @"3. The ‘Year’ data input field stays ‘Selected’.");

            TraceHeader("Test Step 39");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Release the pressed area");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following information,(1)    The state of ‘Month’ input field is changed to selected");
            /*
            Test Step 39
            Action: Release the pressed area
            Expected Result: Verify the following information,(1)    The state of ‘Month’ input field is changed to selected
            Test Step Comment: (1) MMI_gen 11942 (partly: MMI_gen 4686 (partly: Label area, Month), MMI_gen 4381 (partly: exit state ‘Pressed’, execute function associated to the button)); MMI_gen 4374;
            */
            DmiActions.ShowInstruction(this, @"Release the Label area of the ‘Month’ data input field");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Month’ data input field is displayed ‘Selected’.");

            TraceHeader("Test Step 40");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Perform action step 34-37 for the Label area of each input field");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following information,(1)    The state of an input field is changed to ‘selected’ when release the pressed area at the Label area of input field");
            /*
            Test Step 40
            Action: Perform action step 34-37 for the Label area of each input field
            Expected Result: Verify the following information,(1)    The state of an input field is changed to ‘selected’ when release the pressed area at the Label area of input field
            Test Step Comment: (1) MMI_gen 11942 (partly: MMI_gen 4686 (partly: Label area, Up-Type button));
            */
            // Should be Steps 36-39...
            // Repeat Step 36 for the Day field
            DmiActions.ShowInstruction(this, @" Press and hold the Label area of the ‘Day’ data input field");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘Day’ data input field is displayed pressed, without a border." +
                                Environment.NewLine +
                                @"2. The ‘Day’ data input field stays ‘Not Selected’." + Environment.NewLine +
                                @"3. The ‘Month’ data input field stays ‘Selected’.");

            // Repeat Step 37 for the Day field
            DmiActions.ShowInstruction(this,
                @"Whilst keeping the Label area of the ‘Day’ data input field pressed, drag it back inside its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘Day’ data input field is displayed enabled, with a border." +
                                Environment.NewLine +
                                @"2. The ‘Day’ data input field stays ‘Not Selected’." + Environment.NewLine +
                                @"3. The ‘Month’ data input field stays ‘Selected’.");

            // Repeat Step 38 for the Day field
            DmiActions.ShowInstruction(this,
                @"Whilst keeping the Label area of the ‘Day’ data input field pressed, drag it back inside its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Day’ data input field is displayed pressed, without a border." +
                                Environment.NewLine +
                                @"2. The ‘Day’ data input field stays ‘Not Selected’." + Environment.NewLine +
                                @"3. The ‘Month’ data input field stays ‘Selected’.");

            // Repeat Step 39 for the Day field
            DmiActions.ShowInstruction(this, @"Release the Label area of the ‘Day’ data input field");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Day’ data input field is displayed ‘Selected’.");

            // Repeat Step 36 for the Hour field
            DmiActions.ShowInstruction(this, @" Press and hold the Label area of the ‘Hour’ data input field");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘Hour’ data input field is displayed pressed, without a border." +
                                Environment.NewLine +
                                @"2. The ‘Hour’ data input field stays ‘Not Selected’." + Environment.NewLine +
                                @"3. The ‘Day’ data input field stays ‘Selected’.");

            // Repeat Step 37 for the Hour field
            DmiActions.ShowInstruction(this,
                @"Whilst keeping the Label area of the ‘Hour’ data input field pressed, drag it back inside its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘Hour’ data input field is displayed enabled, with a border." +
                                Environment.NewLine +
                                @"2. The ‘Hour’ data input field stays ‘Not Selected’." + Environment.NewLine +
                                @"3. The ‘Day’ data input field stays ‘Selected’.");

            // Repeat Step 38 for the Hour field
            DmiActions.ShowInstruction(this,
                @"Whilst keeping the Label area of the ‘Hour’ data input field pressed, drag it back inside its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Hour’ data input field is displayed pressed, without a border." +
                                Environment.NewLine +
                                @"2. The ‘Hour’ data input field stays ‘Not Selected’." + Environment.NewLine +
                                @"3. The ‘Day’ data input field stays ‘Selected’.");

            // Repeat Step 39 for the Hour field
            DmiActions.ShowInstruction(this, @"Release the Label area of the ‘Hour’ data input field");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Hour’ data input field is displayed ‘Selected’.");

            TraceHeader("Test Step 41");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Perform action step 34-37 for the Data area of each input field");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following information,(1)    The state of an input field is changed to ‘selected’ when release the pressed area at the Data area of input field");
            /*
            Test Step 41
            Action: Perform action step 34-37 for the Data area of each input field
            Expected Result: Verify the following information,(1)    The state of an input field is changed to ‘selected’ when release the pressed area at the Data area of input field
            Test Step Comment: (1) MMI_gen 11942 (partly: MMI_gen 4686 (partly: Data area)); MMI_gen 9390 (partly: Set Clock window);
            */
            // Should be Steps 36-39...
            // Repeat Step 36 for the Month field data area
            DmiActions.ShowInstruction(this, @" Press and hold the Data area of the ‘Month’ data input field");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘Month’ data input field is displayed pressed, without a border." +
                                Environment.NewLine +
                                @"2. The ‘Month’ data input field stays ‘Not Selected’." + Environment.NewLine +
                                @"3. The ‘Year’ data input field stays ‘Selected’.");

            // Repeat Step 37 for the Month field data area
            DmiActions.ShowInstruction(this,
                @"Whilst keeping the Data area of the ‘Month’ data input field pressed, drag it back inside its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘Month’ data input field is displayed enabled, with a border." +
                                Environment.NewLine +
                                @"2. The ‘Month’ data input field stays ‘Not Selected’." + Environment.NewLine +
                                @"3. The ‘Year’ data input field stays ‘Selected’.");

            // Repeat Step 38 for the Month field data area
            DmiActions.ShowInstruction(this,
                @"Whilst keeping the Data area of the ‘Month’ data input field pressed, drag it back inside its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Month’ data input field is displayed pressed, without a border." +
                                Environment.NewLine +
                                @"2. The ‘Month’ data input field stays ‘Not Selected’." + Environment.NewLine +
                                @"3. The ‘Year’ data input field stays ‘Selected’.");

            // Repeat Step 39 for the Month field data area
            DmiActions.ShowInstruction(this, @"Release the Data area of the ‘Month’ data input field");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Month’ data input field is displayed ‘Selected’.");

            // Should be Steps 36-39...
            // Repeat Step 36 for the Day field data area
            DmiActions.ShowInstruction(this, @" Press and hold the Data area of the ‘Day’ data input field");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘Day’ data input field is displayed pressed, without a border." +
                                Environment.NewLine +
                                @"2. The ‘Day’ data input field stays ‘Not Selected’." + Environment.NewLine +
                                @"3. The ‘Month’ data input field stays ‘Selected’.");

            // Repeat Step 37 for the Day field field data area
            DmiActions.ShowInstruction(this,
                @"Whilst keeping the Data area of the ‘Day’ data input field pressed, drag it back inside its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘Day’ data input field is displayed enabled, with a border." +
                                Environment.NewLine +
                                @"2. The ‘Day’ data input field stays ‘Not Selected’." + Environment.NewLine +
                                @"3. The ‘Month’ data input field stays ‘Selected’.");

            // Repeat Step 38 for the Day field field data area
            DmiActions.ShowInstruction(this,
                @"Whilst keeping the Data area of the ‘Day’ data input field pressed, drag it back inside its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Day’ data input field is displayed pressed, without a border." +
                                Environment.NewLine +
                                @"2. The ‘Day’ data input field stays ‘Not Selected’." + Environment.NewLine +
                                @"3. The ‘Month’ data input field stays ‘Selected’.");

            // Repeat Step 39 for the Day field data area
            DmiActions.ShowInstruction(this, @"Release the Data area of the ‘Day’ data input field");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Day’ data input field is displayed ‘Selected’.");

            // Repeat Step 36 for the Hour field data area
            DmiActions.ShowInstruction(this, @" Press and hold the Data area of the ‘Hour’ data input field");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘Hour’ data input field is displayed pressed, without a border." +
                                Environment.NewLine +
                                @"2. The ‘Hour’ data input field stays ‘Not Selected’." + Environment.NewLine +
                                @"3. The ‘Day’ data input field stays ‘Selected’.");

            // Repeat Step 37 for the Hour field data area
            DmiActions.ShowInstruction(this,
                @"Whilst keeping the Data area of the ‘Hour’ data input field pressed, drag it back inside its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘Hour’ data input field is displayed enabled, with a border." +
                                Environment.NewLine +
                                @"2. The ‘Hour’ data input field stays ‘Not Selected’." + Environment.NewLine +
                                @"3. The ‘Day’ data input field stays ‘Selected’.");

            // Repeat Step 38 for the Hour field data area
            DmiActions.ShowInstruction(this,
                @"Whilst keeping the Data area of the ‘Hour’ data input field pressed, drag it back inside its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Hour’ data input field is displayed pressed, without a border." +
                                Environment.NewLine +
                                @"2. The ‘Hour’ data input field stays ‘Not Selected’." + Environment.NewLine +
                                @"3. The ‘Day’ data input field stays ‘Selected’.");

            // Repeat Step 39 for the Hour field data area
            DmiActions.ShowInstruction(this, @"Release the Data area of the ‘Hour’ data input field");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Hour’ data input field is displayed ‘Selected’.");

            TraceHeader("Test Step 42");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Perform the following procedure,Press ‘Language’ button.Select and confirm another language except English.Press ‘Set Clock’ button");
            TraceReport("Expected Result");
            TraceInfo(
                "DMI displays Set clock window.Verify the following information,The labels are updated refer to selected language");
            /*
            Test Step 42
            Action: Perform the following procedure,Press ‘Language’ button.Select and confirm another language except English.Press ‘Set Clock’ button
            Expected Result: DMI displays Set clock window.Verify the following information,The labels are updated refer to selected language
            Test Step Comment: (1) MMI_gen 11943;
            */
            DmiActions.ShowInstruction(this,
                @"Press a data input field to confirm the data and press the ‘Yes’ button, then press the ‘Language button’;" +
                Environment.NewLine +
                @"in the language window select ‘Deutsch’ and confirm the selection, then press the ‘Set clock’ button in the Settings window");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. All the labels are displayed in German.");

            TraceHeader("Test Step 43");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Perform the following procedure,Press ‘Close’ button.Press ‘Language’ button.Select and confirm English language");
            TraceReport("Expected Result");
            TraceInfo("The labels are updated refer to selected language");
            /*
            Test Step 43
            Action: Perform the following procedure,Press ‘Close’ button.Press ‘Language’ button.Select and confirm English language
            Expected Result: The labels are updated refer to selected language
            */
            DmiActions.ShowInstruction(this,
                @"Press the ‘Close’ button. Press the ‘Language’ button, then select and confirm English language;" +
                Environment.NewLine +
                @"in the Settings window, press the ‘Set clock’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. All the labels are displayed in English.");

            TraceHeader("Test Step 44");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Use the test script file 22_13_1_a.xml to send EVC-30");
            TraceReport("Expected Result");
            TraceInfo("The Set clock button is disabled");
            /*
            Test Step 44
            Action: Use the test script file 22_13_1_a.xml to send EVC-30
            Expected Result: The Set clock button is disabled
            */
            XML_22_13_1(msgType.typea);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Set clock’ button is disabled");

            TraceHeader("Test Step 45");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Use the test script file 22_13_1_c.xml to send EVC-30 with,MMI_NID_WINDOW = 4MMI_Q_REQUEST_ENABLE_64 (#25) = 0MMI_Q_REQUEST_ENABLE_64 (#26) = 1");
            TraceReport("Expected Result");
            TraceInfo("The Set clock button is enabled");
            /*
            Test Step 45
            Action: Use the test script file 22_13_1_c.xml to send EVC-30 with,MMI_NID_WINDOW = 4MMI_Q_REQUEST_ENABLE_64 (#25) = 0MMI_Q_REQUEST_ENABLE_64 (#26) = 1
            Expected Result: The Set clock button is enabled
            Test Step Comment: MMI_gen 1563         (partly: enabled bit#26);             
            */
            XML_22_13_1(msgType.typec);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Set clock’ button is enabled");

            TraceHeader("Test Step 46");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Press ‘Set Clock’ button. Then, select an input field ‘Offset’ at second page");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following information, (1)   The Offset time is editable refer to received packet EVC-30 with variable MMI_Q_REQUEST_ENABLE_64 #26 = 1(2)   All other input fields except Offset time are non-editable refer to received packet EVC-30 with variable MMI_Q_REQUEST_ENABLE_64 #25 = 0");
            /*
            Test Step 46
            Action: Press ‘Set Clock’ button. Then, select an input field ‘Offset’ at second page
            Expected Result: Verify the following information, (1)   The Offset time is editable refer to received packet EVC-30 with variable MMI_Q_REQUEST_ENABLE_64 #26 = 1(2)   All other input fields except Offset time are non-editable refer to received packet EVC-30 with variable MMI_Q_REQUEST_ENABLE_64 #25 = 0
            Test Step Comment: (1) MMI_gen 2450 (partly: bit #26);(2) MMI_gen 1563 (partly: disabled bit #25);
            */
            DmiActions.ShowInstruction(this,
                @"Press the ‘Set clock’ button and press the ‘Next’ button in the Set clock window, " +
                Environment.NewLine +
                "then select the Offset data input field (on the second page)");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Offset’ data input field can be edited." + Environment.NewLine +
                                "2. All other data input fields are disabled.");

            TraceHeader("Test Step 47");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Perform the following procedure,Change the value of Offset time to “+12:00”Confirm an entered value by pressing an input field.Press ‘Yes’ button");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following information,(1)  Use the log file to confirm that DMI sent out packet [MMI_SET_TIME_MMI (EVC-109)] with,MMI_T_ZONE_OFFSET = 48MMI_T_UTC = current UTC time calculated from current UTC time");
            /*
            Test Step 47
            Action: Perform the following procedure,Change the value of Offset time to “+12:00”Confirm an entered value by pressing an input field.Press ‘Yes’ button
            Expected Result: Verify the following information,(1)  Use the log file to confirm that DMI sent out packet [MMI_SET_TIME_MMI (EVC-109)] with,MMI_T_ZONE_OFFSET = 48MMI_T_UTC = current UTC time calculated from current UTC time
            Test Step Comment: (1) MMI_gen 2452;
            */
            DmiActions.ShowInstruction(this,
                @"Change the ‘Offset’ value to ‘+12:00’ and confirm the value by pressing the data input field, then press the ‘Yes’ button");

            //EVC109_MmiSetTimeMmi.CheckMmiTZoneOffset = -48;
            //EVC109_MmiSetTimeMmi.CheckMmiTUTC = 1481429471;   // ??

            TraceHeader("Test Step 48");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Perform the following procedure,Use the test script file 22_13_1_a.xml to send EVC-30.Then, Use the test script file 22_13_1_b.xml to send EVC-30 with,MMI_NID_WINDOW = 4MMI_Q_REQUEST_ENABLE_64 (#25) = 1MMI_Q_REQUEST_ENABLE_64 (#26) = 0");
            TraceReport("Expected Result");
            TraceInfo("The Set clock button is enabled");
            /*
            Test Step 48
            Action: Perform the following procedure,Use the test script file 22_13_1_a.xml to send EVC-30.Then, Use the test script file 22_13_1_b.xml to send EVC-30 with,MMI_NID_WINDOW = 4MMI_Q_REQUEST_ENABLE_64 (#25) = 1MMI_Q_REQUEST_ENABLE_64 (#26) = 0
            Expected Result: The Set clock button is enabled
            Test Step Comment: MMI_gen 1563         (partly: enabled bit#25);             
            */
            XML_22_13_1(msgType.typeb);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Set clock’ button is enabled");

            TraceHeader("Test Step 49");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Press ‘Set Clock’ button. Then, select an input field ‘Offset’ at second page");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following information, (1)   The Offset time is editable  refer to received packet EVC-30 with variable MMI_Q_REQUEST_ENABLE_64 #25 = 1");
            /*
            Test Step 49
            Action: Press ‘Set Clock’ button. Then, select an input field ‘Offset’ at second page
            Expected Result: Verify the following information, (1)   The Offset time is editable  refer to received packet EVC-30 with variable MMI_Q_REQUEST_ENABLE_64 #25 = 1
            Test Step Comment: (1) MMI_gen 2450 (partly: bit #25);
            */
            DmiActions.ShowInstruction(this,
                @"Press the ‘Set Clock’ button, then press the ‘Next’ button and select the ‘Offset’ data input field");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Offset’ data input field can be edited.");

            TraceHeader("Test Step 50");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Press the ‘Close’ button");
            TraceReport("Expected Result");
            TraceInfo("Verify the following information,(1)   DMI displays Settings window");
            /*
            Test Step 50
            Action: Press the ‘Close’ button
            Expected Result: Verify the following information,(1)   DMI displays Settings window
            Test Step Comment: (1) MMI_gen 4392 (partly: returning to the parent window);
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Settings window.");

            TraceHeader("Test Step 51");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("End of test");
            
            /*
            Test Step 51
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }

        #region Send_XML_22_13_1_DMI_Test_Specification

        enum msgType
        {
            typea,
            typeb,
            typec,
            typed
        }

        private void XML_22_13_1(msgType type)
        {
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.No_window_specified; // Settings
            switch (type)
            {
                case msgType.typea:
                    break;
                case msgType.typeb:
                    EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH =
                        EVC30_MMIRequestEnable.EnabledRequests.SetLocalTimeDateAndOffset;
                    break;
                case msgType.typec:
                    EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH =
                        EVC30_MMIRequestEnable.EnabledRequests.SetLocalOffset;
                    break;
                case msgType.typed:
                    EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH =
                        EVC30_MMIRequestEnable.EnabledRequests.SetLocalTimeDateAndOffset |
                        EVC30_MMIRequestEnable.EnabledRequests.SetLocalOffset;
                    break;
            }

            EVC30_MMIRequestEnable.Send();
        }

        #endregion
    }
}
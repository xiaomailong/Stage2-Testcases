using System;
using Testcase.Telegrams.EVCtoDMI;


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
    public class TC_ID_22_6_1_Password_window : TestcaseBase
    {
        public override bool TestcaseEntryPoint()
        {
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 24867;
            // Testcase entrypoint
            TraceInfo("This test case requires an ATP configuration change - " +
                      "See Precondition requirements. If this is not done manually, the test may fail!");

            MakeTestStepHeader(1, UniqueIdentifier++, "Press ‘Maintenance’ button",
                "Verify the following points,LayersThe layers of window on half-grid array is displayed as followsLayer 0: Main-Area D, F, G, Y and Z.Layer -1: A1, A2+A3*, A4, B*, C1, C2+C3+C4*, C5, C6, C7, C8, C9, E1, E2, E3, E4, E5-E9*Layer -2: B3, B4, B5, B6, B7Note: ‘*’ symbol is mean that specified areas are drawn as one area.Data Entry windowThe window title is displayed with text “Maintenance password”.Verify that the Maintenance password window is displayed in main area D, F and G as half-grid array.A data entry window is containing only one input field covers the Main area D, F and GThe following objects are displayed in Maintenance password window. Enabled Close button (NA11)Window TitleInput FieldInput fieldThe input field is located in main area D and F.For a single input field, the window title is clearly explaining the topic of the input field. The Maintenance password window is displayed as a single input field with only the data part.KeyboardThe keyboard associated to the Maintenance password window is displayed as numeric keyboard.The keyboard is presented below the area of input field.The keyboard contains enabled button for the number <1> to <9>, <Delete>(NA21) , <0> and disabled <Decimal_Separator>. NA21, Delete button.DMI displays Maintenance password window.General property of windowThe Maintenance password window is presented with objects and buttons which is the one of several levels and allocated to areas of DMI. All objects, text messages and buttons are presented within the same layer.The Default window is not displayed and covered the current window");
            /*
            Test Step 1
            Action: Press ‘Maintenance’ button
            Expected Result: Verify the following points,LayersThe layers of window on half-grid array is displayed as followsLayer 0: Main-Area D, F, G, Y and Z.Layer -1: A1, A2+A3*, A4, B*, C1, C2+C3+C4*, C5, C6, C7, C8, C9, E1, E2, E3, E4, E5-E9*Layer -2: B3, B4, B5, B6, B7Note: ‘*’ symbol is mean that specified areas are drawn as one area.Data Entry windowThe window title is displayed with text “Maintenance password”.Verify that the Maintenance password window is displayed in main area D, F and G as half-grid array.A data entry window is containing only one input field covers the Main area D, F and GThe following objects are displayed in Maintenance password window. Enabled Close button (NA11)Window TitleInput FieldInput fieldThe input field is located in main area D and F.For a single input field, the window title is clearly explaining the topic of the input field. The Maintenance password window is displayed as a single input field with only the data part.KeyboardThe keyboard associated to the Maintenance password window is displayed as numeric keyboard.The keyboard is presented below the area of input field.The keyboard contains enabled button for the number <1> to <9>, <Delete>(NA21) , <0> and disabled <Decimal_Separator>. NA21, Delete button.DMI displays Maintenance password window.General property of windowThe Maintenance password window is presented with objects and buttons which is the one of several levels and allocated to areas of DMI. All objects, text messages and buttons are presented within the same layer.The Default window is not displayed and covered the current window
            Test Step Comment: (1) MMI_gen 11719 (partly: MMI_gen 5189 (partly: touch screen), MMI_gen 5944 (partly: touch screen));(2) MMI_gen 11718;(3) MMI_gen 11719 (partly: half grid array)(4) MMI_gen 11719 (partly: MMI_gen 4640 (partly: only data area), MMI_gen 4720, MMI_gen 4889 (partly: merge label and data))(5) MMI_gen 11719 (partly: MMI_gen 4722 (partly: Close button, Window Title, Input field)); MMI_gen 4392 (partly: [Close] NA11);(6) MMI_gen 11719 (partly: MMI_gen 4637 (partly: Main-areas D and F))(7) MMI_gen 11719 (partly: note under the MMI_gen 9412)(8) MMI_gen 11719 (partly: single input field, only data part);(9) MMI_gen 11719 (partly: MMI_gen 4912); MMI_gen 11721;(10) MMI_gen 11719 (partly: MMI_gen 4678)(11) MMI_gen 11719 (partly: MMI_gen 5003); MMI_gen 4392 (partly: [Delete] NA21);(12) MMI_gen 11737;(13) MMI_gen 4350;(14) MMI_gen 4351;(15) MMI_gen 4353;
            */
            StartUp();

            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH =
                EVC30_MMIRequestEnable.EnabledRequests.EnableWheelDiameter;
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Default;
            EVC30_MMIRequestEnable.Send();

            DmiActions.ShowInstruction(this,
                @"Press the ‘Settings’ button and then the ‘Maintenance’ button in the Settings window.");

            WaitForVerification("Check the following (* indicates sub-areas drawn as one area):" + Environment.NewLine +
                                Environment.NewLine +
                                "1. The window is displayed as a half-grid array in areas D, F and G, with the title ‘Maintenance password’." +
                                Environment.NewLine +
                                "2. The following screen areas are in Layer 0: D, F, G, Z and Y." +
                                Environment.NewLine +
                                "3. The following screen areas are in Layer 1: A1, (A2 + A3)*, A4, C1, (C2 + C3 + C4)*, C5, C6, C7, C8, C9, E1, E2, E3, E4, (E5-E9)*." +
                                Environment.NewLine +
                                "4. The following screen areas are in Layer 2: B3, B4, B5, B6, B7" +
                                Environment.NewLine +
                                "5. A data entry window with one data input field is covers areas D, F and G." +
                                Environment.NewLine +
                                "6. DMI displays the ‘Close’ button NA11 enabled (symbol NA11) in the Maintenance password window." +
                                Environment.NewLine +
                                "7. A data input field is displayed in areas D and F." + Environment.NewLine +
                                "8. The Maintenance password window has one data input field with no label." +
                                Environment.NewLine +
                                "9. DMI displays a numeric keypad for the Maintenance password window." +
                                Environment.NewLine +
                                "10. The keypad contains (enabled) buttons for the numbers <1> to <9>, <0>, <Delete> (symbol NA21) and disabled <decimal-separator> button." +
                                Environment.NewLine +
                                "11 Objects, text messages and buttons can be displayed in several levels. Within a level they are allocated to areas." +
                                Environment.NewLine +
                                "12. Objects, text messages and buttons in a layer form a window." +
                                Environment.NewLine +
                                "13. The Default window is not displayed covering the current window.");

            MakeTestStepHeader(2, UniqueIdentifier++,
                "Press and hold every buttons on the dedicate keyboard respectively.Note: This step is for testing ‘0’ - ‘9’ button",
                "Verify the following information,On next activation of a data key of the associated keyboard, the character or value corresponding to this data key shall be added into the Data Area.Sound ‘Click’ is played once.The state of button is changed to ‘Pressed’ and immediately back to ‘Enabled’ state.The Input Field displays the number associated to the data key according to the pressings in state ‘Pressed’.An input field is used to enter the Maintenance password.The data value is displayed as black colour and the background of the data area is displayed as medium-grey colour.The data value of the input field is aligned to the left of the data area.The flashing horizontal-line cursor is always in the next position of the echoed entered-data key in the ‘Selected IF/value of pressed key(s)’ data input field when selected the next character it will be inserted cursor position");
            /*
            Test Step 2
            Action: Press and hold every buttons on the dedicate keyboard respectively.Note: This step is for testing ‘0’ - ‘9’ button
            Expected Result: Verify the following information,On next activation of a data key of the associated keyboard, the character or value corresponding to this data key shall be added into the Data Area.Sound ‘Click’ is played once.The state of button is changed to ‘Pressed’ and immediately back to ‘Enabled’ state.The Input Field displays the number associated to the data key according to the pressings in state ‘Pressed’.An input field is used to enter the Maintenance password.The data value is displayed as black colour and the background of the data area is displayed as medium-grey colour.The data value of the input field is aligned to the left of the data area.The flashing horizontal-line cursor is always in the next position of the echoed entered-data key in the ‘Selected IF/value of pressed key(s)’ data input field when selected the next character it will be inserted cursor position
            Test Step Comment: (1) MMI_gen 11719 (partly: MMI_gen 4679, MMI_gen 4642);(2) MMI_gen 11719 (partly: MMI_gen 4913 (partly: MMI_gen 4384 (partly: sound ‘Click’)));(3) MMI_gen 11719 (partly: MMI_gen 4913 (partly: MMI_gen 4384 (partly: Change to state ‘Pressed’ and immediately back to state ‘Enabled’)));   (4) MMI_gen 11719 (partly: MMI_gen 4913);(5) MMI_gen 11720; MMI_gen 11719 (partly: MMI_gen 4634);(6) MMI_gen 11719 (partly: MMI_gen 4651);(7) MMI_gen 11719 (partly: MMI_gen 4647 (partly: left aligned));(8) MMI_gen 11719 (partly: MMI_gen 4689, MMI_gen 4690, MMI_gen 4691 (partly: flashing), MMI_gen 4692);
            */
            DmiActions.ShowInstruction(this, @"Press and hold the ‘1’ button on the keypad");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. On pressing the button, the corresponding value is added to the data input field" +
                                Environment.NewLine +
                                "2. The ‘Click’ sound is played once." + Environment.NewLine +
                                "3. The button is displayed pressed and immediately re-displayed enabled." +
                                Environment.NewLine +
                                "4. The data input field value is black with a Medium-grey background." +
                                Environment.NewLine +
                                "5. The value in the input field is displayed left-aligned." + Environment.NewLine +
                                "6. A flashing underscore cursor is displayed to the right of the number appearing after the button is pressed.");

            DmiActions.ShowInstruction(this,
                @"Repeat the previous step with each numeric button on the keypad (‘2’ to ‘0’) individually");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. On pressing the button, the corresponding value is added to the data input field" +
                                Environment.NewLine +
                                "2. The ‘Click’ sound is played once." + Environment.NewLine +
                                "3. The button is displayed pressed and immediately re-displayed enabled." +
                                Environment.NewLine +
                                "4. The data input field value is black with a Medium-grey background." +
                                Environment.NewLine +
                                "5. The value in the input field is displayed left-aligned." + Environment.NewLine +
                                "6. A flashing underscore cursor is displayed to the right of the number appearing after the button is pressed");

            DmiActions.ShowInstruction(this,
                @"Press the ‘Delete’ button to remove the number from the data input field");

            MakeTestStepHeader(3, UniqueIdentifier++, "Released the pressed button",
                "Verify the following information, The character is stop adding and the state of button is changed to ‘Enabled’");
            /*
            Test Step 3
            Action: Released the pressed button
            Expected Result: Verify the following information, The character is stop adding and the state of button is changed to ‘Enabled’
            Test Step Comment: (1) MMI_gen 11719 (partly: MMI_gen 4913 (partly: MMI_gen 4384 (partly: ETCS-MMI’s function associated to the button)));
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Released the pressed button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The pressed button is displayed enabled.");

            MakeTestStepHeader(4, UniqueIdentifier++, "Press and hold ‘Del’ button.Note: Stopwatch is required",
                "Verify the following information,While press and hold button less than 1.5 secSound ‘Click’ is played once.The state of button is changed to ‘Pressed’ and immediately back to ‘Enabled’ state.The last character is removed from an input field after pressing the button.While press and hold button over 1.5 secThe state ‘pressed’ and ‘released’ are switched repeatly while button is pressed and the characters are removed from an input field repeatly refer to pressed state.The sound ‘Click’ is played repeatly while button is pressed");
            /*
            Test Step 4
            Action: Press and hold ‘Del’ button.Note: Stopwatch is required
            Expected Result: Verify the following information,While press and hold button less than 1.5 secSound ‘Click’ is played once.The state of button is changed to ‘Pressed’ and immediately back to ‘Enabled’ state.The last character is removed from an input field after pressing the button.While press and hold button over 1.5 secThe state ‘pressed’ and ‘released’ are switched repeatly while button is pressed and the characters are removed from an input field repeatly refer to pressed state.The sound ‘Click’ is played repeatly while button is pressed
            Test Step Comment: (1) MMI_gen 11719 (partly: MMI_gen 4913 (partly: MMI_gen 4384 (partly: sound ‘Click’)));(2) MMI_gen 11719 (partly: MMI_gen 4913 (partly: MMI_gen 4384 (partly: Change to state ‘Pressed’ and immediately back to state ‘Enabled’)));(3) MMI_gen 11719 (partly: MMI_gen 4913 (partly: MMI_gen 4384 (partly: ETCS-MMI’s function associated to the button))); MMI_gen 4393 (partly: [Delete]);(4) MMI_gen 11719 (partly: MMI_gen 4913 (partly: MMI_gen 4386 (partly: visual of repeat function)));(5) MMI_gen 11719  (partly: MMI_gen 4913 (partly: MMI_gen 4386 (partly: audible of repeat function)));
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press and hold the ‘Delete’ button for more than 1.5s");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Whilst the button has been pressed for less than 1.5s, the ‘Click’ sound is played once." +
                                Environment.NewLine +
                                "2. Whilst the button has been pressed for less than 1.5s, the button is displayed pressed and immediately re-displayed enabled." +
                                Environment.NewLine +
                                "3. Whilst the button has been pressed for less than 1.5s, the last character in the data input field is removed." +
                                Environment.NewLine +
                                "4. After the button has been pressed for more than 1.5s it is repeately displayed pressed and enabled." +
                                Environment.NewLine +
                                "5. After the button has been pressed for more than 1.5s, the ‘Click’ sound is played repeatedly." +
                                Environment.NewLine +
                                "6. After the button has been pressed for more than 1.5s, characters are removed from the end of the data input field repeatedly.");

            MakeTestStepHeader(5, UniqueIdentifier++, "Release ‘Del’ button",
                "Verify the following information, The character is stop removing");
            /*
            Test Step 5
            Action: Release ‘Del’ button
            Expected Result: Verify the following information, The character is stop removing
            Test Step Comment: (1) MMI_gen 11719 (partly: MMI_gen 4913 (partly: MMI_gen 4384 (partly: ETCS-MMI’s function associated to the button)));
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Release the ‘Delete’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. No more characters are removed from the data input field");

            MakeTestStepHeader(6, UniqueIdentifier++,
                "Press ‘Del’ button on the numeric keyboard until no number is displayed on the Input Field",
                "No character is displayed on the Input Field");
            /*
            Test Step 6
            Action: Press ‘Del’ button on the numeric keyboard until no number is displayed on the Input Field
            Expected Result: No character is displayed on the Input Field
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this,
                @"Press ‘Del’ button on the numeric keyboard until the data input field is blank");

            MakeTestStepHeader(7, UniqueIdentifier++,
                "The 5 characters are added on an input field as one group. (e.g. ‘12345')",
                "Verify the following information,The 5 characters are added on an input field as one group. (e.g. ‘*****').Single input field is show on asterisk (*) symbol for each entered number");
            /*
            Test Step 7
            Action: The 5 characters are added on an input field as one group. (e.g. ‘12345')
            Expected Result: Verify the following information,The 5 characters are added on an input field as one group. (e.g. ‘*****').Single input field is show on asterisk (*) symbol for each entered number
            Test Step Comment: (1) MMI_gen 11719 (partly: MMI_gen 4694 (partly: NEGATIVE, 6th character));(2) MMI_gen 11693;
            */
            DmiActions.ShowInstruction(this, @"Press and release buttons ‘1’, ‘2’, ‘3’, ‘4’, ‘5’ in order");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The data input field is displayed with the value ‘*****’.");

            MakeTestStepHeader(8, UniqueIdentifier++, "Continue to enter the 6th character",
                "Verify the following information,The fifth character is shown after a gap of fourth character, separated as 2 groups (e.g. **** **)");
            /*
            Test Step 8
            Action: Continue to enter the 6th character
            Expected Result: Verify the following information,The fifth character is shown after a gap of fourth character, separated as 2 groups (e.g. **** **)
            Test Step Comment: (1) MMI_gen 11719 (partly: MMI_gen 4694 (partly: MMI_gen 4246));
            */
            DmiActions.ShowInstruction(this, @"Press and release button ‘6’");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The data input field is displayed with the value ‘**** **’ (with a space between the fourth and fifth ‘*’.");

            MakeTestStepHeader(9, UniqueIdentifier++,
                "Delete the old value and enter the new value more than 8 characters which different from configured value in tag PASS_CODE_MTN",
                "Verify the following information,The data value is displayed only 8 characters (e.g. **** ****)");
            /*
            Test Step 9
            Action: Delete the old value and enter the new value more than 8 characters which different from configured value in tag PASS_CODE_MTN
            Expected Result: Verify the following information,The data value is displayed only 8 characters (e.g. **** ****)
            Test Step Comment: (1) MMI_gen 11722     (partly: password is configurable at most eight digits);
            */
            DmiActions.ShowInstruction(this,
                @"Delete the value entered in the data input field, then press and release nine numeric buttons");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The data input field is displayed with the value ‘**** ****’.");

            MakeTestStepHeader(10, UniqueIdentifier++, "Press and hold an input field",
                "Verify the following information,(1)    The state of an input field is changed to ‘Pressed’, the border of button is removed");
            /*
            Test Step 10
            Action: Press and hold an input field
            Expected Result: Verify the following information,(1)    The state of an input field is changed to ‘Pressed’, the border of button is removed
            Test Step Comment: (1) MMI_gen 9390 (partly: Password window);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press and hold the data input field");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The data input field is displayed pressed, without a border.");

            MakeTestStepHeader(11, UniqueIdentifier++, "Slide out an input field",
                "Verify the following information,(1)    The state of an input field is changed to ‘Enabled, the border of button is shown without a sound");
            /*
            Test Step 11
            Action: Slide out an input field
            Expected Result: Verify the following information,(1)    The state of an input field is changed to ‘Enabled, the border of button is shown without a sound
            Test Step Comment: (1) MMI_gen 9390 (partly: Password window);
            */
            DmiActions.ShowInstruction(this, @"Whilst keeping the data input field pressed, drag outside its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the data input field enabled, with a border." + Environment.NewLine +
                                "2. No sound is played.");

            MakeTestStepHeader(12, UniqueIdentifier++, "Slide back into an input field",
                "Verify the following information,(1)    The state of an input field is changed to ‘Pressed’, the border of button is removed");
            /*
            Test Step 12
            Action: Slide back into an input field
            Expected Result: Verify the following information,(1)    The state of an input field is changed to ‘Pressed’, the border of button is removed
            Test Step Comment: (1) MMI_gen 9390 (partly: Password window);
            */
            DmiActions.ShowInstruction(this,
                @"Whilst keeping the data input field pressed, drag it back inside its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the data input field pressed with no border." + Environment.NewLine +
                                "2. No sound is played.");


            MakeTestStepHeader(13, UniqueIdentifier++, "Release the pressed area",
                "Verify the followings information,Data entry process is terminated, DMI displays the Settings window");
            /*
            Test Step 13
            Action: Release the pressed area
            Expected Result: Verify the followings information,Data entry process is terminated, DMI displays the Settings window
            Test Step Comment: (1) MMI_gen 11732; MMI_gen 11719 (partly: MMI_gen 4682, MMI_ gen 4684 (partly: terminate), MMI_gen 4634)); MMI_gen 4392 (partly: [Enter], touch screen); MMI_gen 9390 (partly: Password window);
            */
            DmiActions.ShowInstruction(this, @"Release the data input field");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays closes the data input window and displays the Settings window.");

            MakeTestStepHeader(14, UniqueIdentifier++,
                "Perform the folowing procedure,Press ‘Maintenance’ button.Enter the password refer to configured value in PASS_CODE_MTNConfirm an entered data",
                "DMI displays the Maintenance window");
            /*
            Test Step 14
            Action: Perform the folowing procedure,Press ‘Maintenance’ button.Enter the password refer to configured value in PASS_CODE_MTNConfirm an entered data
            Expected Result: DMI displays the Maintenance window
            Test Step Comment: MMI_gen 11731 (partly: touch screen); MMI_gen 11722 (partly: password is configurable at most eight digits);
            */
            DmiActions.ShowInstruction(this,
                @"Press the ‘Maintenance’ button. Enter the password from the configuration setting and confirm");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Maintenance window.");

            MakeTestStepHeader(15, UniqueIdentifier++,
                "Perform the following step to re-configure the Maintenance passwordPower off systemRe-configure the Maintenance password ‘PASS_CODE_MTN’ to ‘4444’. Perform the following test step to verify configuration result,Power On system.Activate Cabin A.Press ‘Settings icon’ button.Press ‘Maintenance’ button.Enter password as 4444.Confirm entered data by pressing input field",
                "DMI displays the Maintenance window");
            /*
            Test Step 15
            Action: Perform the following step to re-configure the Maintenance passwordPower off systemRe-configure the Maintenance password ‘PASS_CODE_MTN’ to ‘4444’. Perform the following test step to verify configuration result,Power On system.Activate Cabin A.Press ‘Settings icon’ button.Press ‘Maintenance’ button.Enter password as 4444.Confirm entered data by pressing input field
            Expected Result: DMI displays the Maintenance window
            Test Step Comment: MMI_gen 11722     (partly: password is configurable at least four digits);
            */
            DmiActions.ShowInstruction(this,
                @"Power off the system and reset the configuration of the Maintenance password to ‘4444’");

            DmiActions.Start_ATP();
            DmiActions.Activate_Cabin_1(this);
            DmiActions.Set_Driver_ID(this, "1234");

            DmiActions.ShowInstruction(this,
                @"Press the ‘Maintenance’ button and enter ‘4444’ for the password, then confirm by pressing the data input field");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Maintenance window.");

            MakeTestStepHeader(16, UniqueIdentifier++,
                "Perform the following step to re-configure the Maintenance passwordPower off systemRe-configure the Maintenance password ‘PASS_CODE_MTN’ to ‘333’. Perform the following test step to verify configuration result,Power On system.Activate Cabin A.Press ‘Settings icon’ button.Press ‘Maintenance’ button.Enter password as 333.Confirm entered data by pressing input field",
                "DMI displays the Settings window");
            /*
            Test Step 16
            Action: Perform the following step to re-configure the Maintenance passwordPower off systemRe-configure the Maintenance password ‘PASS_CODE_MTN’ to ‘333’. Perform the following test step to verify configuration result,Power On system.Activate Cabin A.Press ‘Settings icon’ button.Press ‘Maintenance’ button.Enter password as 333.Confirm entered data by pressing input field
            Expected Result: DMI displays the Settings window
            Test Step Comment: MMI_gen 11722     (partly: NEGATIVE, password is configurable at least four digits);
            */
            DmiActions.ShowInstruction(this,
                @"Power off the system and reset the configuration of the Maintenance password to ‘333’");

            DmiActions.Start_ATP();
            DmiActions.Activate_Cabin_1(this);
            DmiActions.Set_Driver_ID(this, "1234");

            DmiActions.ShowInstruction(this, @"Press the ‘Settings’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Settings window.");

            MakeTestStepHeader(17, UniqueIdentifier++,
                "Perform the following step to re-configure the Maintenance passwordPower off systemRe-configure the Maintenance password ‘PASS_CODE_MTN’ to ‘999999999’. Perform the following test step to verify configuration result,Power On system.Activate Cabin A.Press ‘Settings icon’ button.Press ‘Maintenance’ button.Enter password as 999999999.Confirm entered data by pressing input field",
                "DMI displays the Settings window");
            /*
            Test Step 17
            Action: Perform the following step to re-configure the Maintenance passwordPower off systemRe-configure the Maintenance password ‘PASS_CODE_MTN’ to ‘999999999’. Perform the following test step to verify configuration result,Power On system.Activate Cabin A.Press ‘Settings icon’ button.Press ‘Maintenance’ button.Enter password as 999999999.Confirm entered data by pressing input field
            Expected Result: DMI displays the Settings window
            Test Step Comment: MMI_gen 11722     (partly: NEGATIVE, password is configurable at most eight digits);
            */
            DmiActions.ShowInstruction(this,
                @"Power off the system and reset the configuration of the Maintenance password to ‘999999999’");

            DmiActions.Start_ATP();
            DmiActions.Activate_Cabin_1(this);
            DmiActions.Set_Driver_ID(this, "1234");

            DmiActions.ShowInstruction(this,
                @"Press the ‘Settings’ button, then the Maintenance button. Enter ‘999999999’ for the password, then confirm by pressing the data input field");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Settings window.");

            MakeTestStepHeader(18, UniqueIdentifier++, "Press the ‘Maintenance’ button.Then, press the ‘Close’ button",
                "Verify the following informaiton,(1) DMI displays the Settings window");
            /*
            Test Step 18
            Action: Press the ‘Maintenance’ button.Then, press the ‘Close’ button
            Expected Result: Verify the following informaiton,(1) DMI displays the Settings window
            Test Step Comment: (1) MMI_gen 4392 (partly: returning to the parent window);
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Maintenance’ button then the ‘Close’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Settings window.");

            TraceHeader("End of test");

            /*
            Test Step 19
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
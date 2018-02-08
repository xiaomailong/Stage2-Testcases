using System;
using Testcase.Telegrams.EVCtoDMI;
using Testcase.Telegrams.DMItoEVC;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 27.18 Entering Characters
    /// TC-ID: 22.18
    /// 
    /// This test case verifies the display of the Train Running Number window that shall comply with [ERA-ERTMS] standard and [MMI-ETCS-gen].
    /// 
    /// Tested Requirements:
    /// MMI_gen 9959; MMI_gen 9961; MMI_gen 7978; MMI_gen 7979; MMI_gen 9958; MMI_gen 7980; MMI_gen 7977 (partly: half grid array, single input field, only data part, MMI_gen 5189 (partly: touch screen), MMI_gen 5944 (partly: touch screen), MMI_gen 4640 (partly: only data area), MMI_gen 4720, MMI_gen 4889 (partly: merge label and data), MMI_gen 4722 (partly: Table 12 <Close> button, Window title, Input field), MMI_gen 4637 (partly: Main-areas D and F), note under the MMI_gen 9412, MMI_gen 4912, MMI_gen 4678, MMI_gen 5003, MMI_gen 4913 (MMI_gen 4384, MMI_gen 4386 (partly: except 0.3s)), MMI_gen 4634, MMI_gen 4651, MMI_gen 4679, MMI_gen 4642, MMI_gen 4689, MMI_gen 4690, MMI_gen 4691 (partly: flashing), MMI_gen 4692, MMI_gen 4647 (partly: left aligned), MMI_gen 4681, MMI_gen 4684 (partly: terminated), MMI_gen 4694 (partly: MMI_gen 4246), MMI_gen 4682); MMI_gen 4392 (partly: [Delete] NA21, [Close] NA11, returning to the parent window, [Enter], touch screen); MMI_gen 4393 (partly: [Delete]); MMI_gen 4350; MMI_gen 4351; MMI_gen 4353; MMI_gen 9390 (partly: Train Running Number window); MMI_gen 8864 (partly: Train Running Number window);
    /// 
    /// Scenario:
    /// The test system is powered on and the cabin is inactive.
    /// 
    /// Used files:
    /// 22_18.xml
    /// </summary>
    public class TC_ID_22_18_Entering_Characters : TestcaseBase
    {
        public override bool TestcaseEntryPoint()
        {
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 25672;
            // Testcase entrypoint

            StartUp();

            MakeTestStepHeader(1, UniqueIdentifier++,
                "Use the test script file 22_18.xml to send EVC-16 with,	MMI_NID_OPERATION = 1 Note: Please wait for 5-10 seconds to make sure that test script is executed completely.",
                "Train Running Number window is not displayed.");
            /*
            Test Step 1
            Action: Use the test script file 22_18.xml to send EVC-16 with,	MMI_NID_OPERATION = 1 Note: Please wait for 5-10 seconds to make sure that test script is executed completely.
            Expected Result: Train Running Number window is not displayed.
            Test Step Comment: (1) MMI_gen 9958 (partly: inactive);
            */

            #region Send_XML_22_18_DMI_Test_Specification

            EVC16_CurrentTrainNumber.TrainRunningNumber = 1;
            EVC16_CurrentTrainNumber.Send();

            #endregion

            Wait_Realtime(10000);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI does not display the Train running number window.");

            MakeTestStepHeader(2, UniqueIdentifier++,
                "Activate cabin A and press ‘TRN’ button on the Driver ID window and verify the presentation on the screen",
                "The Train Running Number window is displayed on the right half part of the window as shown in figure belowLayers(1)	The layers of window on half-grid array is displayed as followsLayer 0: Main-Area D, F, G, Y and Z.Layer -1: A1, A2+A3*, A4, B*, C1, C2+C3+C4*, C5, C6, C7, C8, C9, E1, E2, E3, E4, E5-E9*Layer -2: B3, B4, B5, B6, B7Note: ‘*’ symbol is mean that specified areas are drawn as one area.Data Entry window(2)	The window title is displayed with text ‘Train running number’.(3)	Verify that the Train Running Number window is displayed in main area D, F and G as half-grid array.(4)	A data entry window is containing only one input field covers the Main area D, F and G(5)	The following objects are displayed in Train Running Number window.  Enabled Close button (NA11)Window TitleInput FieldInput field(6)	The input field is located in main area D and F.(7)	For a single input field, the window title is clearly explaining the topic of the input field. (8)	The Train Running Number window is displayed as a single input field with only the data part.Keyboard(9)	The keyboard associated to the Train Running Number window is displayed as numeric keyboard.(10)	The keyboard is presented below the area of input field.(11)	The keyboard contains enabled button for the number <1>, <2 >, … , <9 >, <Delete>(NA21), <0> and disabled <Decimal_Separator>.   NA21, Delete button.Packet Receiving(12)	DMI displays 'Train Running Number' window with the data stored onboard from EVC-16 with variable MMI_NID_OPERATION = TRN (The data is the same as displayed on DMI).General property of window(13)	The Train Running Number window is presented with objects and buttons which is the one of several levels and allocated to areas of DMI(14)	All objects, text messages and buttons are presented within the same layer.(15)	The Default window is not displayed and covered the current window.            ");
            /*
            Test Step 2
            Action: Activate cabin A and press ‘TRN’ button on the Driver ID window and verify the presentation on the screen
            Expected Result: The Train Running Number window is displayed on the right half part of the window as shown in figure belowLayers(1)	The layers of window on half-grid array is displayed as followsLayer 0: Main-Area D, F, G, Y and Z.Layer -1: A1, A2+A3*, A4, B*, C1, C2+C3+C4*, C5, C6, C7, C8, C9, E1, E2, E3, E4, E5-E9*Layer -2: B3, B4, B5, B6, B7Note: ‘*’ symbol is mean that specified areas are drawn as one area.Data Entry window(2)	The window title is displayed with text ‘Train running number’.(3)	Verify that the Train Running Number window is displayed in main area D, F and G as half-grid array.(4)	A data entry window is containing only one input field covers the Main area D, F and G(5)	The following objects are displayed in Train Running Number window.  Enabled Close button (NA11)Window TitleInput FieldInput field(6)	The input field is located in main area D and F.(7)	For a single input field, the window title is clearly explaining the topic of the input field. (8)	The Train Running Number window is displayed as a single input field with only the data part.Keyboard(9)	The keyboard associated to the Train Running Number window is displayed as numeric keyboard.(10)	The keyboard is presented below the area of input field.(11)	The keyboard contains enabled button for the number <1>, <2 >, … , <9 >, <Delete>(NA21), <0> and disabled <Decimal_Separator>.   NA21, Delete button.Packet Receiving(12)	DMI displays 'Train Running Number' window with the data stored onboard from EVC-16 with variable MMI_NID_OPERATION = TRN (The data is the same as displayed on DMI).General property of window(13)	The Train Running Number window is presented with objects and buttons which is the one of several levels and allocated to areas of DMI(14)	All objects, text messages and buttons are presented within the same layer.(15)	The Default window is not displayed and covered the current window.            
            Test Step Comment: (1) MMI_gen 7977 (partly: MMI_gen 5189 (partly: touch screen), MMI_gen 5944 (partly: touch screen)));(2) MMI_gen 7978;(3) MMI_gen 7977 (partly: half grid array);(4) MMI_gen 7977 (partly: MMI_gen 4640 (partly: only data area), MMI_gen 4720, MMI_gen 4889 (partly: merge label and data));(5) MMI_gen 7977 (party: MMI_gen 4722 (partly: Table 12 <Close> button, Window title , Input field)); MMI_gen 4392 (partly: [Close] NA11);(6) MMI_gen 7977 (partly: MMI_gen 4637 (partly: Main-areas D and F));(7) MMI_gen 7977 (partly: note under the MMI_gen 9412);(8) MMI_gen 7977 (partly: single input field, only data part);(9) MMI_gen 7980; MMI_gen 7977 (partly: MMI_gen 4912);(10) MMI_gen 7977 (partly: MMI_gen 4678);(11) MMI_gen 7977 (partly: MMI_gen 5003); MMI_gen 4392 (partly: [Delete] NA21);(12) MMI_gen 9958 (partly: Display Train Running Number window, EVC-16 with MMI_NID_OPERATION = TRN);(13) MMI_gen 4350;(14) MMI_gen 4351;(15) MMI_gen 4353;            
            */
            DmiActions.Activate_Cabin_1(this);

            // Display the Main window to end at
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.None;
            EVC30_MMIRequestEnable.Send();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Main;
            EVC30_MMIRequestEnable.Send();

            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.StandBy;

            EVC14_MMICurrentDriverID.MMI_X_DRIVER_ID = "";
            EVC14_MMICurrentDriverID.MMI_Q_ADD_ENABLE = EVC14_MMICurrentDriverID.MMI_Q_ADD_ENABLE_BUTTONS.TRN;
            EVC14_MMICurrentDriverID.MMI_Q_CLOSE_ENABLE = Variables.MMI_Q_CLOSE_ENABLE.Disabled;
            EVC14_MMICurrentDriverID.Send();

            DmiActions.ShowInstruction(this, @"Press the ‘TRN’ button in the Driver ID window");

            EVC16_CurrentTrainNumber.TrainRunningNumber = 1;
            EVC16_CurrentTrainNumber.Send();

            WaitForVerification("Check the following (* indicates sub-areas drawn as one area):" + Environment.NewLine +
                                Environment.NewLine +
                                @"1. DMI displays the Train running number window with 3 layers in a half-grid array with the title ‘Train running number’." +
                                Environment.NewLine +
                                "2. The Train running number window is displayed in areas D, F and G with a data entry window with one data input field covering these areas." +
                                Environment.NewLine +
                                "3. Layer 0 comprises areas D, F, G, Y and Z." + Environment.NewLine +
                                "4. Layer 1 comprises areas A1, (A2+A3)*, A4, B, C1, (C2+C3+c4)*, C5, C6, C7, C8, C9, E1, E2, E3, E4, (E5-E9)*." +
                                Environment.NewLine +
                                "5. Layer 2 comprises areas B3, B4, B5, B6 and B7." + Environment.NewLine +
                                @"6. The Train running number window displays a data input field, with only a Data part, in areas D and F and an ‘Enabled Close’ button (symbol NA11)." +
                                Environment.NewLine +
                                "7. A keypad is displayed below the data input field, containing enabled keys for the numbers <1> to <9>, <Del> (symbol NA21), <0> and (disabled) <Decimal_Separator>." +
                                Environment.NewLine +
                                "8. Objects, text messages and buttons can be displayed in several levels. Within a level they are allocated to areas." +
                                Environment.NewLine +
                                "9. Objects, text messages and buttons in a layer form a window." +
                                Environment.NewLine +
                                "10. The Default window does not cover the current window.");

            MakeTestStepHeader(3, UniqueIdentifier++,
                "Press and hold every buttons on the dedicate keyboard respectively.Note:	This step is for testing ’0’-‘9’ button.",
                "Verify the following information,(1)	On next activation of a data key of the associated keyboard, the character or value corresponding to this data key shall be added into the Data Area.(2)	Sound ‘Click’ is played once.(3)	The state of button is changed to ‘Pressed’ and immediately back to ‘Enabled’ state.(4)	The Input Field displays the number associated to the data key according to the pressings in state ‘Pressed’.(5)	An input field is used to enter the Train Running Number.(6)	The data value is displayed as black colour and the background of the data area is displayed as medium-grey colour.(7)	The data value of the input field is aligned to the left of the data area.(8)	The flashing horizontal-line cursor is always in the next position of the echoed entered-data key in the ‘Selected IF/value of pressed key(s)’ data input field when selected the next character it will be inserted cursor position.");
            /*
            Test Step 3
            Action: Press and hold every buttons on the dedicate keyboard respectively.Note:	This step is for testing ’0’-‘9’ button.
            Expected Result: Verify the following information,(1)	On next activation of a data key of the associated keyboard, the character or value corresponding to this data key shall be added into the Data Area.(2)	Sound ‘Click’ is played once.(3)	The state of button is changed to ‘Pressed’ and immediately back to ‘Enabled’ state.(4)	The Input Field displays the number associated to the data key according to the pressings in state ‘Pressed’.(5)	An input field is used to enter the Train Running Number.(6)	The data value is displayed as black colour and the background of the data area is displayed as medium-grey colour.(7)	The data value of the input field is aligned to the left of the data area.(8)	The flashing horizontal-line cursor is always in the next position of the echoed entered-data key in the ‘Selected IF/value of pressed key(s)’ data input field when selected the next character it will be inserted cursor position.
            Test Step Comment: MMI_gen 7977 (partly: MMI_gen 4679, MMI_gen 4642);(2) MMI_gen 7977 (partly: MMI_gen 4913 (partly: MMI_gen 4384 (partly: sound ‘Click’)));(3) MMI_gen 7977 (partly: MMI_gen 4913 (partly: MMI_gen 4384 (partly: Change to state ‘Pressed’ and immediately back to state ‘Enabled’)));   (4) MMI_gen 7977 (partly: MMI_gen 4913);                      (5) MMI_gen 7979 (partly: entry); MMI_gen 7977 (partly: MMI_gen 4634);(6) MMI_gen 7977 (partly: MMI_gen 4651);(7) MMI_gen 7977 (partly: MMI_gen 4647 (partly: left aligned));(8) MMI_gen 7977 (partly: MMI_gen 4689, MMI_gen 4690, MMI_gen 4691 (partly: flashing), MMI_gen 4692);
            */
            DmiActions.ShowInstruction(this, "Press and hold the <0> key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The key is displayed pressed and immediately re-displayed enabled." +
                                Environment.NewLine +
                                "2. The ‘Click’ sound is played once." + Environment.NewLine +
                                "3. The data input field displays ‘0’ in black on a Medium-grey background, left-aligned in the Data part." +
                                Environment.NewLine +
                                "4. The data input field accepts the value according to the key pressed." +
                                Environment.NewLine +
                                "5. A flashing underscore is displayed as a cursor after the ‘0’ entered.");

            DmiActions.ShowInstruction(this, "Press the <Del> key until the input field is blank");

            // Repeat for the <1> key
            DmiActions.ShowInstruction(this, "Press and hold the <1> key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The key is displayed pressed and immediately re-displayed enabled." +
                                Environment.NewLine +
                                "2. The ‘Click’ sound is played once." + Environment.NewLine +
                                "3. The data input field displays ‘01’ in black on a Medium-grey background, left-aligned in the Data part." +
                                Environment.NewLine +
                                "4. The data input field accepts the value according to the key pressed." +
                                Environment.NewLine +
                                "5. A flashing underscore is displayed as a cursor after the ‘1’ entered.");

            // Repeat for the <2> key
            DmiActions.ShowInstruction(this, "Press and hold the <2> key");
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The key is displayed pressed and immediately re-displayed enabled." +
                                Environment.NewLine +
                                "2. The ‘Click’ sound is played once." + Environment.NewLine +
                                "3. The data input field displays ‘012’ in black on a Medium-grey background, left-aligned in the Data part." +
                                Environment.NewLine +
                                "4. The data input field accepts the value according to the key pressed." +
                                Environment.NewLine +
                                "5. A flashing underscore is displayed as a cursor after the ‘2’ entered.");

            // Repeat for the <3> key
            DmiActions.ShowInstruction(this, "Press and hold the <3> key");
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The key is displayed pressed and immediately re-displayed enabled." +
                                Environment.NewLine +
                                "2. The ‘Click’ sound is played once." + Environment.NewLine +
                                "3. The data input field displays ‘0123’ in black on a Medium-grey background, left-aligned in the Data part." +
                                Environment.NewLine +
                                "4. The data input field accepts the value according to the key pressed." +
                                Environment.NewLine +
                                "5. A flashing underscore is displayed as a cursor after the ‘3’ entered.");

            // Repeat for the <4> key
            DmiActions.ShowInstruction(this, "Press and hold the <4> key");
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The key is displayed pressed and immediately re-displayed enabled." +
                                Environment.NewLine +
                                "2. The ‘Click’ sound is played once." + Environment.NewLine +
                                "3. The data input field displays ‘01234’ in black on a Medium-grey background, left-aligned in the Data part." +
                                Environment.NewLine +
                                "4. The data input field accepts the value according to the key pressed." +
                                Environment.NewLine +
                                "5. A flashing underscore is displayed as a cursor after the ‘4’ entered.");

            // Repeat for the <5> key
            DmiActions.ShowInstruction(this, "Press and hold the <5> key");
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The key is displayed pressed and immediately re-displayed enabled." +
                                Environment.NewLine +
                                "2. The ‘Click’ sound is played once." + Environment.NewLine +
                                "3. The data input field displays ‘012345’ in black on a Medium-grey background, left-aligned in the Data part." +
                                Environment.NewLine +
                                "4. The data input field accepts the value according to the key pressed." +
                                Environment.NewLine +
                                "5. A flashing underscore is displayed as a cursor after the ‘5’ entered.");

            // Repeat for the <6> key
            DmiActions.ShowInstruction(this, "Press and hold the <6> key");
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The key is displayed pressed and immediately re-displayed enabled." +
                                Environment.NewLine +
                                "2. The ‘Click’ sound is played once." + Environment.NewLine +
                                "3. The data input field displays ‘01234 56’ in black on a Medium-grey background, left-aligned in the Data part." +
                                Environment.NewLine +
                                "4. The data input field accepts the value according to the key pressed." +
                                Environment.NewLine +
                                "5. A flashing underscore is displayed as a cursor after the ‘6’ entered.");

            // Repeat for the <7> key
            DmiActions.ShowInstruction(this, "Press and hold the <7> key");
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The key is displayed pressed and immediately re-displayed enabled." +
                                Environment.NewLine +
                                "2. The ‘Click’ sound is played once." + Environment.NewLine +
                                "3. The data input field displays ‘01234 567’ in black on a Medium-grey background, left-aligned in the Data part." +
                                Environment.NewLine +
                                "4. The data input field accepts the value according to the key pressed." +
                                Environment.NewLine +
                                "5. A flashing underscore is displayed as a cursor after the ‘7’ entered.");

            // Repeat for the <8> key
            DmiActions.ShowInstruction(this, "Press and hold the <8> key");
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The key is displayed pressed and immediately re-displayed enabled." +
                                Environment.NewLine +
                                "2. The ‘Click’ sound is played once." + Environment.NewLine +
                                "3. The data input field displays ‘01234 5678’ in black on a Medium-grey background, left-aligned in the Data part." +
                                Environment.NewLine +
                                "4. The data input field accepts the value according to the key pressed." +
                                Environment.NewLine +
                                "5. A flashing underscore is displayed as a cursor after the ‘8’ entered.");

            // Repeat for the <9> key
            DmiActions.ShowInstruction(this, "Press and hold the <9> key");
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The key is displayed pressed and immediately re-displayed enabled." +
                                Environment.NewLine +
                                "2. The ‘Click’ sound is played once." + Environment.NewLine +
                                "3. The data input field displays ‘01234 5678 9’ in black on a Medium-grey background, left-aligned in the Data part." +
                                Environment.NewLine +
                                "4. The data input field accepts the value according to the key pressed." +
                                Environment.NewLine +
                                "5. A flashing underscore is displayed as a cursor after the ‘9’ entered.");

            MakeTestStepHeader(4, UniqueIdentifier++, "Released the pressed button",
                "Verify the following information, (1)	The state of button is changed to ‘Enabled’");
            /*
            Test Step 4
            Action: Released the pressed button
            Expected Result: Verify the following information, (1)	The state of button is changed to ‘Enabled’
            Test Step Comment: (1) MMI_gen 7977 (partly: MMI_gen 4913 (partly: MMI_gen 4384 (partly: ETCS-MMI’s function associated to the button)));
            */
            DmiActions.ShowInstruction(this, @"Release the <9> key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The <9> key is displayed enabled.");

            MakeTestStepHeader(5, UniqueIdentifier++, "Press and hold ‘Del’ button.Note: Stopwatch is required.",
                "Verify the following information,While press and hold button less than 1.5 sec(1)	Sound ‘Click’ is played once.(2)	The state of button is changed to ‘Pressed’ and immediately back to ‘Enabled’ state.(3)	The last character is removed from an input field after pressing the button.While press and hold button over 1.5 sec(4)	The state ‘pressed’ and ‘released’ are switched repeatly while button is pressed and the characters are removed from an input field repeatly refer to pressed state.(5)	The sound ‘Click’ is played repeatly while button is pressed.");
            /*
            Test Step 5
            Action: Press and hold ‘Del’ button.Note: Stopwatch is required.
            Expected Result: Verify the following information,While press and hold button less than 1.5 sec(1)	Sound ‘Click’ is played once.(2)	The state of button is changed to ‘Pressed’ and immediately back to ‘Enabled’ state.(3)	The last character is removed from an input field after pressing the button.While press and hold button over 1.5 sec(4)	The state ‘pressed’ and ‘released’ are switched repeatly while button is pressed and the characters are removed from an input field repeatly refer to pressed state.(5)	The sound ‘Click’ is played repeatly while button is pressed.
            Test Step Comment: (1) MMI_gen 7977 (partly: MMI_gen 4913 (partly: MMI_gen 4384 (partly: sound ‘Click’)));(2) MMI_gen 7977 (partly: MMI_gen 4913 (partly: MMI_gen 4384 (partly: Change to state ‘Pressed’ and immediately back to state ‘Enabled’)));(3) MMI_gen 7977 (partly: MMI_gen 4913 (partly: MMI_gen 4384 (partly: ETCS-MMI’s function associated to the button))); MMI_gen 4393 (partly: [Delete]);(4) MMI_gen 7977 (partly: MMI_gen 4913 (partly: MMI_gen 4386 (partly: visual of repeat function)));(5) MMI_gen 7977 (partly: MMI_gen 4913 (partly: MMI_gen 4386 (partly: audible of repeat function)));
            */
            DmiActions.ShowInstruction(this,
                @"Press and hold the ‘Del’ key for more than 1.5s. Note: Stopwatch is required");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Click’ sound is played once." + Environment.NewLine +
                                "2. The key is displayed pressed then immediately re-displayed enabled." +
                                Environment.NewLine +
                                "3. The last character is deleted from the data input field." + Environment.NewLine +
                                "4. After the key has been pressed for more than 1.5 s, the key is repeatedly displayed pressed and re-displayed enabled;" +
                                Environment.NewLine +
                                "5. The ‘Click’ sound is played repeatedly while the key is pressed and characters are deleted repeatedly from the end of the data input field.");

            MakeTestStepHeader(6, UniqueIdentifier++, "Release ‘Del’ button",
                "Verify the following information, The character is stop removing");
            /*
            Test Step 6
            Action: Release ‘Del’ button
            Expected Result: Verify the following information, The character is stop removing
            Test Step Comment: (1) MMI_gen 7977 (partly: MMI_gen 4913 (partly: MMI_gen 4384 (partly: ETCS-MMI’s function associated to the button))); 
            */
            DmiActions.ShowInstruction(this, @"Release the <Del> key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Characters stop being deleted from the end of the data input field.");

            MakeTestStepHeader(7, UniqueIdentifier++,
                "Press ‘Del’ button on the numeric keyboard until no number is displayed on the Input Field",
                "No number is displayed on the Input Field.");
            /*
            Test Step 7
            Action: Press ‘Del’ button on the numeric keyboard until no number is displayed on the Input Field
            Expected Result: No number is displayed on the Input Field.
            Test Step Comment:
            */
            DmiActions.ShowInstruction(this,
                "Press and hold the <Del> key until the value in the data input field is blank");

            MakeTestStepHeader(8, UniqueIdentifier++, "Enter the data value with 5 characters.",
                "(1)	The 5 characters are added on an input field as one group. (e.g. ‘12345').");
            /*
            Test Step 8
            Action: Enter the data value with 5 characters.
            Expected Result: (1)	The 5 characters are added on an input field as one group. (e.g. ‘12345').
            Test Step Comment: (1) MMI_gen 7977 (partly: MMI_gen 4694 (partly: NEGATIVE, 6th character));
            */
            DmiActions.ShowInstruction(this, "Enter the value ‘12345’ in the data input field");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The data input field displays ‘12345’ (as a single group of digits).");

            MakeTestStepHeader(9, UniqueIdentifier++, "Continue to enter the 6th character.",
                "Verify the following information,(1)	The fifth character is shown after a gap of fourth character, separated as 2 groups (e.g. 1234 56) ");
            /*
            Test Step 9
            Action: Continue to enter the 6th character.
            Expected Result: Verify the following information,(1)	The fifth character is shown after a gap of fourth character, separated as 2 groups (e.g. 1234 56) 
            Test Step Comment:(1) MMI_gen 7977 (partly: MMI_gen 4694 (partly: MMI_gen 4246)); 
            */
            DmiActions.ShowInstruction(this, "Press (and release) the <6> key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The data input field displays ‘1234 56’ (as two groups of digits).");

            MakeTestStepHeader(10, UniqueIdentifier++, "Press and hold an input field.",
                "Verify the following information,(1)    The state of an input field is changed to ‘Pressed’, the border of button is removed.");
            /*
            Test Step 10
            Action: Press and hold an input field.
            Expected Result: Verify the following information,(1)    The state of an input field is changed to ‘Pressed’, the border of button is removed.
            Test Step Comment: 1) MMI_gen 9390 (partly: Train Running Number window);
            */
            DmiActions.ShowInstruction(this, @"Press in and hold the data input field");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The data input field is displayed pressed, without a border.");

            MakeTestStepHeader(11, UniqueIdentifier++, "Slide out an input field",
                "Verify the following information, The state of an input field is changed to ‘Enabled, the border of button is shown without a sound.");
            /*
            Test Step 11
            Action: Slide out an input field
            Expected Result: Verify the following information, The state of an input field is changed to ‘Enabled, the border of button is shown without a sound.
            Test Step Comment: (1) MMI_gen 9390 (partly: Train Running Number window);
            */
            DmiActions.ShowInstruction(this, @"Whilst keeping the data input field pressed, drag it out of its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The data input field is displayed enabled, with a border." + Environment.NewLine +
                                "2. No sound is played.");

            MakeTestStepHeader(12, UniqueIdentifier++, "Slide back into an input field.",
                "Verify the following information, The state of an input field is changed to ‘Pressed’, the border of button is removed.");
            /*
            Test Step 12
            Action: Slide back into an input field.
            Expected Result: Verify the following information, The state of an input field is changed to ‘Pressed’, the border of button is removed.
            Test Step Comment: (1) MMI_gen 9390 (partly: Train Running Number window);
            */
            DmiActions.ShowInstruction(this,
                @"Whilst keeping the data input field pressed, drag it back inside its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The data input field is displayed pressed, without a border.");

            MakeTestStepHeader(13, UniqueIdentifier++, "Release the pressed area.",
                "(1)	The Train Running Number window is closed. DMI displays the Driver ID window.(2)	Use the log file to confirm that DMI sent out packet EVC-116 with variable MMI_NID_OPERATION = TRN (The entered and confirmed value in the data entry window)Note: A value of MMI_NID_OPERATION shows as hexadecimal value of ASCII which corresponds to its character that displayed in the input field in the Train running number window.");
            /*
            Test Step 13
            Action: Release the pressed area.
            Expected Result: (1)	The Train Running Number window is closed. DMI displays the Driver ID window.(2)	Use the log file to confirm that DMI sent out packet EVC-116 with variable MMI_NID_OPERATION = TRN (The entered and confirmed value in the data entry window)Note: A value of MMI_NID_OPERATION shows as hexadecimal value of ASCII which corresponds to its character that displayed in the input field in the Train running number window.
            Test Step Comment: MMI_gen 9959 (partly: switch back to the previous window); MMI_gen 7977 (partly: MMI_gen 4681 (partly: accept the entered value), MMI_gen 4684 (partly: terminated));(2) MMI_gen 7979 (partly: entry); MMI_gen 9959 (partly: EVC-116); MMI_gen 7977 (partly: MMI_gen 4682); MMI_gen 4392 (partly: [Enter], touch screen); MMI_gen 9390 (partly: Train Running Number window);
            */
            DmiActions.ShowInstruction(this, "Release the data input field");

            EVC116_MMINewTrainNumber.Check_NID_OPERATION = 123456;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI closes the Train running number window and displays the Driver ID window.");

            MakeTestStepHeader(14, UniqueIdentifier++,
                "Perform the following procedure, Enter and confirm the Driver ID	Select and confirm Level 1.",
                "DMI displays Main window.");
            /*
            Test Step 14
            Action: Perform the following procedure, Enter and confirm the Driver ID	Select and confirm Level 1.
            Expected Result: DMI displays Main window.
            Test Step Comment: 
            */
            DmiActions.ShowInstruction(this, "Enter ‘1234’ for the Driver ID and confirm");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Main window.");

            MakeTestStepHeader(15, UniqueIdentifier++, "Press ‘Train Running Number’ button on Main window",
                "The Train Running Number window is displayed.(1)	 An input field is used to revalidation the Train running number.");
            /*
            Test Step 15
            Action: Press ‘Train Running Number’ button on Main window
            Expected Result: The Train Running Number window is displayed.(1)	 An input field is used to revalidation the Train running number.
            Test Step Comment: (1)MMI_gen 7979 (partly: revalidation);
            */
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH =
                EVC30_MMIRequestEnable.EnabledRequests.TrainRunningNumber;
            EVC30_MMIRequestEnable.Send();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Main;
            EVC30_MMIRequestEnable.Send();

            DmiActions.ShowInstruction(this, "Press the ‘Train Running Number’ button in the Main window");

            EVC16_CurrentTrainNumber.TrainRunningNumber = 1;
            EVC16_CurrentTrainNumber.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Train running number window." + Environment.NewLine +
                                "2. The data input field displays ‘1’.");

            MakeTestStepHeader(16, UniqueIdentifier++,
                "Confirm the current data without re-entry Train running number. ",
                " (1) The Train Running Number window is closed.DMI displays the Main window.(2) Use the log file to confirm that DMI sent out packet EVC-116 with variable MMI_NID_OPERATION = TRN(The entered and confirmed value in the data entry window)Note: A value of MMI_NID_OPERATION shows as hexadecimal value of ASCII which corresponds to its character that displayed in the input field in the Driver ID window.	(1) MMI_gen 9959(partly: switch back to the previous window);                        ");
            /*
            Test Step 16
            Action: Confirm the current data without re-entry Train running number. 
            Expected Result:  (1) The Train Running Number window is closed.DMI displays the Main window.(2) Use the log file to confirm that DMI sent out packet EVC-116 with variable MMI_NID_OPERATION = TRN(The entered and confirmed value in the data entry window)Note: A value of MMI_NID_OPERATION shows as hexadecimal value of ASCII which corresponds to its character that displayed in the input field in the Driver ID window.	(1) MMI_gen 9959(partly: switch back to the previous window);                        
            Test Step Comment: (2) MMI_gen 7979(partly: revalidation); MMI_gen 9959(partly: EVC - 116);
            */
            DmiActions.ShowInstruction(this, "Confirm the data without re-entering the Train running number");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Main window.");

            MakeTestStepHeader(17, UniqueIdentifier++, "Press ‘Train Running Number’ button on Main window.	",
                "The Train Running Number window is displayed.");
            /*
            Test Step 17
            Action: Press ‘Train Running Number’ button on Main window.	
            Expected Result: The Train Running Number window is displayed.
            Test Step Comment: 
            */
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH =
                EVC30_MMIRequestEnable.EnabledRequests.TrainRunningNumber;
            EVC30_MMIRequestEnable.Send();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Main;
            EVC30_MMIRequestEnable.Send();

            DmiActions.ShowInstruction(this, "Press the ‘Train Running Number’ button in the Main window");

            EVC16_CurrentTrainNumber.TrainRunningNumber = 1;
            EVC16_CurrentTrainNumber.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Train running number window.");

            MakeTestStepHeader(18, UniqueIdentifier++, "Enter the new Train running number.	",
                "The current data in the input field is replaced by the entered data from the driver.");
            /*
            Test Step 18
            Action: Enter the new Train running number.	
            Expected Result: The current data in the input field is replaced by the entered data from the driver.
            Test Step Comment: 
            */
            DmiActions.ShowInstruction(this, "Enter ‘1234’ for a new Train running number");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The data input field displays ‘1234’.");

            MakeTestStepHeader(19, UniqueIdentifier++, "Confirm the entered value by pressing an input field.	",
                "(1)    DMI closes the Train Running Number window and displays Main window.	(1) MMI_gen 4681 (partly: accept the entered value in the input field); ");
            /*
            Test Step 19
            Action: Confirm the entered value by pressing an input field.	
            Expected Result: (1)    DMI closes the Train Running Number window and displays Main window.	(1) MMI_gen 4681 (partly: accept the entered value in the input field); 
            Test Step Comment: MMI_gen 8864 (partly: the driver accept the data value by pressing the input field);
            */
            DmiActions.ShowInstruction(this, "Confirm the data by pressing in the data input field");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI closes the Train running number window and displays the Main window.");

            MakeTestStepHeader(20, UniqueIdentifier++, "Press ‘Train Running Number’ button on Main menu window",
                "	The Train Running Number window is displayed.(1)   DMI displays ‘Train Running Number’ window with the entered data that confirmed in Step 19. This data stored onboard is received from EVC-16 with variable MMI_NID_OPERATION = TRN (The data is the same as displayed on DMI).	");
            /*
            Test Step 20
            Action: Press ‘Train Running Number’ button on Main menu window
            Expected Result: 	The Train Running Number window is displayed.(1)   DMI displays ‘Train Running Number’ window with the entered data that confirmed in Step 19. This data stored onboard is received from EVC-16 with variable MMI_NID_OPERATION = TRN (The data is the same as displayed on DMI).	
            Test Step Comment: (1) MMI_gen 7977 (partly: MMI_gen 4681 (partly: entered data is replaced)); MMI_gen 8864 (partly: Train Running Number window);
            */
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH =
                EVC30_MMIRequestEnable.EnabledRequests.TrainRunningNumber;
            EVC30_MMIRequestEnable.Send();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Main;
            EVC30_MMIRequestEnable.Send();

            DmiActions.ShowInstruction(this, "Press the ‘Train Running Number’ button in the Main window");

            EVC16_CurrentTrainNumber.TrainRunningNumber = 1234;
            EVC16_CurrentTrainNumber.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Train running number window." + Environment.NewLine +
                                @"2. The data input field displays ‘1234’.");

            MakeTestStepHeader(21, UniqueIdentifier++,
                "Close the Train Running Number window and verify the presentation on the screen	",
                "The Train Running Number window is closed. DMI displays the Main window.(1)	 Use the log file to confirm that DMI sends out EVC-101 with variable MMI_M_REQUEST = 31 (Exit Change Train Running Number)	");
            /*
            Test Step 21
            Action: Close the Train Running Number window and verify the presentation on the screen	
            Expected Result: The Train Running Number window is closed. DMI displays the Main window.(1)	 Use the log file to confirm that DMI sends out EVC-101 with variable MMI_M_REQUEST = 31 (Exit Change Train Running Number)	
            Test Step Comment: (1) MMI_gen 9961; MMI_gen 4392 (partly: returning to the parent window);
            */
            DmiActions.ShowInstruction(this, "Close the Train running number window");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI closes the Train running number window and displays the Main window.");

            MakeTestStepHeader(22, UniqueIdentifier++, "End of test", "");

            /*
            Test Step 22
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
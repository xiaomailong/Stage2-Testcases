using System;
using Testcase.Telegrams.DMItoEVC;
using Testcase.Telegrams.EVCtoDMI;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 27.17.1 Driver ID window: General Display
    /// TC-ID: 22.17
    /// 
    /// This test case verifies the display of the ‘Driver ID’ window on DMI that shall comply with [ERA-ERTMS] standard and [MMI-ETCS-gen].
    /// 
    /// Tested Requirements:
    /// MMI_gen 183; MMI_gen 8033 (partly: MMI_gen 5189 (partly: touch screen), MMI_gen 5944 (partly: touch screen), half grid array, MMI_gen 4640 (partly: only data area), MMI_gen 4720, MMI_gen 4889 (partly: merge label and data), MMI_gen 4637 (partly: Main-areas D, F), MMI_gen 4634, note under the MMI_gen 9412, MMI_gen 4647 (partly: left aligned), MMI_gen 4651, MMI_gen 4642, MMI_gen 4679, MMI_gen 4722 (partly: Table 12 <Close> button), MMI_gen 4912, MMI_gen 4678, MMI_gen 5005, MMI_gen 4682, MMI_gen 4681, MMI_gen 4684 (partly: terminated), MMI_gen 4689, MMI_gen 4690, MMI_gen 4691 (partly: flashing), MMI_gen 4692, MMI_gen 4913); MMI_gen 8034; MMI_gen 8037; MMI_gen 8038 (partly: button position, touch screen); MMI_gen 8039 (partly: button position, touch screen); MMI_gen 8035; MMI_gen 8036; MMI_gen 9960; MMI_gen 184; MMI_gen 9962; MMI_gen 4392 (partly: [Delete] NA21, [Close] NA11, returning to the parent window, [Enter], touch screen); MMI_gen 4396 (partly: Close); MMI_gen 4395 (partly: Close); MMI_gen 9390 (partly: Drive ID window); MMI_gen 8864 (partly: Driver ID window);
    /// 
    /// Scenario:
    /// The test system is powered on and the cabin is inactive.Driver ID window in cabin inactive state is verified.After cabin activation, the Driver ID window appearance is verified.The TRN button functionality in Driver ID window is verified.The data entry functionality of the Driver ID window is verified.SoM is completed in mode SR, ETCS level 1The revalidate data entry of the ‘Driver ID’ window is verified.The state of TRN button and settings button are verified.The window closure is verified.
    /// 
    /// Used files:
    /// 22_17_a.xml, 22_17_b.xml, 22_17_c.xml, 22_17_d.xml, 22_17_e.xml
    /// </summary>
    public class TC_ID_22_17_Driver_ID_window : TestcaseBase
    {
        public override bool TestcaseEntryPoint()
        {
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 25599;
            // Testcase entrypoint
            StartUp();

            MakeTestStepHeader(1, UniqueIdentifier++,
                "Use the test script file 22_17_a.xml to send EVC-14 withMMI_X_DRIVER_ID = ‘4444’Note: Please wait for 5-10 seconds to make sure that test script is executed completely",
                "(1) Driver ID window is not displayed");
            /*
            Test Step 1
            Action: Use the test script file 22_17_a.xml to send EVC-14 withMMI_X_DRIVER_ID = ‘4444’Note: Please wait for 5-10 seconds to make sure that test script is executed completely
            Expected Result: (1) Driver ID window is not displayed
            Test Step Comment: (1) MMI_gen 183 (partly: inactive);
            */
            XML_22_17(msgType.typea);

            // Wait 10s
            this.Wait_Realtime(10000);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI does not display the Driver ID window");

            MakeTestStepHeader(2, UniqueIdentifier++, "Activate cabin A and verify the presentation on the DMI screen",
                "DMI displays the Main window for a few moment prior to the Driver ID window is displayed on the right half part of the window as shown in figure belowLayersThe layers of window on half-grid array is displayed as followsLayer 0: Main-Area D, F, G, Y and Z.Layer -1: A1, A2+A3*, A4, B*, C1, C2+C3+C4*, C5, C6, C7, C8, C9, E1, E2, E3, E4, E5-E9*Layer -2: B3, B4, B5, B6, B7Note: ‘*’ symbol is mean that specified areas are drawn as one area.Data Entry windowThe window title is displayed with text “Driver ID”.Verify that the Driver ID window is displayed in main area D, F and G as half-grid array.The Driver ID window is presented ‘Settings’ button with the symbol SE04 and a ‘Train running number’ button with the label ‘TRN’.A data entry window is containing only one input field covers the Main area D, F and GThe Settings button is positioned in the bottom right corner of area D,F and GThe Train running number button is positioned at left aligned of Settings buttonInput fieldThe input field is located in main area D and F.For a single input field, the window title is clearly explaining the topic of the input field. The Driver ID window is displayed as a single input field with only the data part.KeyboardThe keyboard associated to the Driver ID window is displayed as alphanumeric keyboard.The keyboard is presented below the area of input field.The keyboard contains enabled button for the number <1>, <2/a/b /c>, … , <9/w/x/y/z>, <Delete>(NA21), <0> and disabled <Decimal_Separator>. The labels of the keys shall separate the number from the letters by a space character e.g. '2 abc'. NA21, Delete button.Packet receivingUse the log file to confirm that DMI receives EVC-14 with variable MMI_X_DRIVER_ID = 0 (NULL) and the value of input field is empty.Disabled Close buttonThe disabled ‘Close’ button NA12 is displayed on the Driver ID window");
            /*
            Test Step 2
            Action: Activate cabin A and verify the presentation on the DMI screen
            Expected Result: DMI displays the Main window for a few moment prior to the Driver ID window is displayed on the right half part of the window as shown in figure belowLayersThe layers of window on half-grid array is displayed as followsLayer 0: Main-Area D, F, G, Y and Z.Layer -1: A1, A2+A3*, A4, B*, C1, C2+C3+C4*, C5, C6, C7, C8, C9, E1, E2, E3, E4, E5-E9*Layer -2: B3, B4, B5, B6, B7Note: ‘*’ symbol is mean that specified areas are drawn as one area.Data Entry windowThe window title is displayed with text “Driver ID”.Verify that the Driver ID window is displayed in main area D, F and G as half-grid array.The Driver ID window is presented ‘Settings’ button with the symbol SE04 and a ‘Train running number’ button with the label ‘TRN’.A data entry window is containing only one input field covers the Main area D, F and GThe Settings button is positioned in the bottom right corner of area D,F and GThe Train running number button is positioned at left aligned of Settings buttonInput fieldThe input field is located in main area D and F.For a single input field, the window title is clearly explaining the topic of the input field. The Driver ID window is displayed as a single input field with only the data part.KeyboardThe keyboard associated to the Driver ID window is displayed as alphanumeric keyboard.The keyboard is presented below the area of input field.The keyboard contains enabled button for the number <1>, <2/a/b /c>, … , <9/w/x/y/z>, <Delete>(NA21), <0> and disabled <Decimal_Separator>. The labels of the keys shall separate the number from the letters by a space character e.g. '2 abc'. NA21, Delete button.Packet receivingUse the log file to confirm that DMI receives EVC-14 with variable MMI_X_DRIVER_ID = 0 (NULL) and the value of input field is empty.Disabled Close buttonThe disabled ‘Close’ button NA12 is displayed on the Driver ID window
            Test Step Comment: (1) MMI_gen 8033 (partly: MMI_gen 5189 (partly: touch screen), MMI_gen 5944 (partly: touch screen));(2) MMI_gen 8034;(3) MMI_gen 8033 (partly: half grid array);(4) MMI_gen 8037 (partly: display ‘Settings’ button and ‘TRN’ button);(5) MMI_gen 8033 (partly: MMI_gen 4640 (partly: only data area), MMI_gen 4720, MMI_gen 4889 (partly: merge label and data));(6) MMI_gen 8038 (partly: button position, touch screen);(7) MMI_gen 8039 (partly: button position, touch screen);(8) MMI_gen 8033  (partly: MMI_gen 4637 (partly: Main-areas D and F));(9) MMI_gen 8033 (partly: note under the MMI_gen 9412);(10) MMI_gen 8033 (partly: single input field, only data part);(11) MMI_gen 8033 (partly: MMI_gen 4912); MMI_gen 8036;(12) MMI_gen 8033 (partly: MMI_gen 4678);(13) MMI_gen 8033 (partly: MMI_gen 5005), MMI_gen 8036; MMI_gen 4392 (partly: [Delete] NA21);(14) MMI_gen 183 (partly: active, EVC-14,null);(15) MMI_gen 4396 (partly: Close, NA12); MMI_gen 4395 (partly: Close);
            */
            DmiActions.Activate_Cabin_1(this);

            // Force display of the DriverID window
            XML_22_17(msgType.typea);

            WaitForVerification("Check the following (* indicates sub-areas drawn as one area):" + Environment.NewLine +
                                Environment.NewLine +
                                "1. DMI displays the Main window and, after a pause, the Driver ID window with the title ‘Driver ID’." +
                                Environment.NewLine +
                                "   as a half-grid array (with 4 rows of 3 keys) in areas D, F and G, with 3 layers on the right side of the screen." +
                                Environment.NewLine +
                                "2. Layer 0 is displayed in areas D, F, G, Y and Z." + Environment.NewLine +
                                "3. Layer 1 is displayed in areas A1, (A2 + A3)*, A4, B*, C1, (C2 + C3 + C4)*, C5, C6, C7, C8, C9, E1, E2, E3, E4, (E5 - E9)*." +
                                Environment.NewLine +
                                "4. Layer 2 is displayed in areas B3, B4, B5, B6, B7." + Environment.NewLine +
                                "5. The Driver ID window displays a ‘Settings’ button, symbol SE04, and a ‘Train running number’ button with the label ‘TRN’." +
                                Environment.NewLine +
                                "6. A data entry window with one data entry field covers areas D, F and G." +
                                Environment.NewLine +
                                @"7. The ‘Settings’ button is displayed in the bottom right-hand corner of areas D, F & G." +
                                Environment.NewLine +
                                @"8. The ‘Train running number’ button is displayed to the left of the ‘Settings’ button." +
                                Environment.NewLine +
                                "9. The data input field is displayed in areas D & F with only a Data part." +
                                Environment.NewLine +
                                "10. A keypad is displayed below the data input field with (enabled) keys <1>, <2 abc> to <9 wxyz>, <Del> (symbol NA21), <0> and a (disabled) <Decimal_Separator> key." +
                                Environment.NewLine +
                                @"11. The Driver ID window displays a (disabled) ‘Close’ button, symbol NA12, in the bottom left corner of areas D, F & G.");

            MakeTestStepHeader(3, UniqueIdentifier++, "Press TRN button on the Driver ID window",
                "The Train Runing Number window is displayed. Use the log file to verify that DMI sends out EVC-101 with variable MMI_M_REQUEST = 30");
            /*
            Test Step 3
            Action: Press TRN button on the Driver ID window
            Expected Result: The Train Runing Number window is displayed. Use the log file to verify that DMI sends out EVC-101 with variable MMI_M_REQUEST = 30
            Test Step Comment:  (1) MMI_gen 9960;
            */
            DmiActions.ShowInstruction(this, @"Press the ‘TRN’ button in the Driver ID window");

            EVC101_MMIDriverRequest.CheckMRequestReleased =
                Telegrams.EVCtoDMI.Variables.MMI_M_REQUEST.ChangeTrainRunningNumber;

            EVC16_CurrentTrainNumber.TrainRunningNumber = 1;
            EVC16_CurrentTrainNumber.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Train running number window");

            MakeTestStepHeader(4, UniqueIdentifier++, "Close the Train Running Number window",
                "The Driver ID window is displayed");
            /*
            Test Step 4
            Action: Close the Train Running Number window
            Expected Result: The Driver ID window is displayed
            */
            DmiActions.ShowInstruction(this, "Close the Train Running Number window");

            DmiExpectedResults.Driver_ID_window_displayed(this);

            MakeTestStepHeader(5, UniqueIdentifier++, "Press every buttons on the dedicated keyboard",
                "The input field displays the number associated to the data key according to the pressings in state Pressed");
            /*
            Test Step 5
            Action: Press every buttons on the dedicated keyboard
            Expected Result: The input field displays the number associated to the data key according to the pressings in state Pressed
            Test Step Comment: (1) MMI_gen 8033 (partly: MMI_gen 4913);
            */
            DmiActions.ShowInstruction(this,
                "Press the <1> key, the <2> key, the <3> key, <4> key, <5> key, <6> key, " + Environment.NewLine +
                "          <7> key, <8> key, and then the <9> key in that order");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The data input field displays ‘1234 5678 9’.");

            DmiActions.ShowInstruction(this, "Press the <Del> key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The data input field displays ‘1234 5678’.");

            DmiActions.ShowInstruction(this, "Press the <0> key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The data input field displays ‘1234 5678 0’.");

            MakeTestStepHeader(6, UniqueIdentifier++, "Enter the Driver ID with ‘1234567’",
                "On next activation of a data key of the associated keyboard, the character or value corresponding to this data key shall be added into the Data Area.Number 1234567 displays on the input fieldThe data value is displayed as black colour and the background of the data area is displayed as medium-grey colour.The flashing horizontal-line cursor is always in the next position of the echoed entered-data key in the ‘Selected IF/value of pressed key(s)’ data input field when selected the next character it will be inserted cursor position.An input field is used to enter the Driver ID.The data value of the input field is aligned to the left of the data area");
            /*
            Test Step 6
            Action: Enter the Driver ID with ‘1234567’
            Expected Result: On next activation of a data key of the associated keyboard, the character or value corresponding to this data key shall be added into the Data Area.Number 1234567 displays on the input fieldThe data value is displayed as black colour and the background of the data area is displayed as medium-grey colour.The flashing horizontal-line cursor is always in the next position of the echoed entered-data key in the ‘Selected IF/value of pressed key(s)’ data input field when selected the next character it will be inserted cursor position.An input field is used to enter the Driver ID.The data value of the input field is aligned to the left of the data area
            Test Step Comment: (1) MMI_gen 8033 (partly: MMI_gen 4679, MMI_gen 4642);(2) MMI_gen 8033 (partly: MMI_gen 4651);(3) MMI_gen 8033 (partly: MMI_gen 4689, MMI_gen 4690, MMI_gen 4691 (partly: flashing), MMI_gen 4692);(4) MMI_gen 8033 (partly: MMI_gen 4634); MMI_gen 8035 (partly: entry);(5) MMI_gen 8033 (partly: MMI_gen 4647 (partly: left aligned));
            */
            DmiActions.ShowInstruction(this, @"Enter ‘1234567’ for the Driver ID");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. As each key is pressed, the corresponding number is added to the data input field." +
                                Environment.NewLine +
                                "2. The data input field displays ‘1234 567’ in black (with a space after the 1st 4 digits) on a Medium-grey background." +
                                Environment.NewLine +
                                "3. A flashing underscore is displayed as a cursor after the last character displayed in the data input field,  " +
                                Environment.NewLine +
                                "4. each character is added at the cursor position and the cursor is then re-displayed after the character added." +
                                Environment.NewLine +
                                "5. The data input field is used to enter the Driver ID." + Environment.NewLine +
                                "6. The value in the data input field is left-aligned.");

            MakeTestStepHeader(7, UniqueIdentifier++, "Confirm the Driver ID",
                "DMI closes the Driver ID window and displays Main window.Use the log file to confirm that DMI sends out  EVC-104 with variable MMI_X_DRIVER_ID = 0x31323334353637000000000000000000.Note: A value of MMI_X_DRIVER_ID shows as hexadecimal value of ASCII which corresponds to its character that displayed in the input field in the Driver ID window");
            /*
            Test Step 7
            Action: Confirm the Driver ID
            Expected Result: DMI closes the Driver ID window and displays Main window.Use the log file to confirm that DMI sends out  EVC-104 with variable MMI_X_DRIVER_ID = "0x31323334353637000000000000000000".Note: A value of MMI_X_DRIVER_ID shows as hexadecimal value of ASCII which corresponds to its character that displayed in the input field in the Driver ID window
            Test Step Comment: (1) MMI_gen 184 (partly: closed), MMI_gen 8033 (partly: MMI_gen 4682, MMI_gen 4681 (partly: accept the entered value), MMI_gen 4684 (partly: terminated));(2) MMI_gen 184 (partly: EVC-104), MMI_gen 8033 (partly: MMI_gen 4682) MMI_gen 4392 (partly: [Enter], touch screen); MMI_gen 8864 (partly: the value stored onboard);
            */
            DmiActions.ShowInstruction(this, "Confirm the Driver ID");

            EVC104_MMINewDriverData.Check_Entered_MMI_X_DRIVER_ID = "1234567";

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI closes the Driver ID window and displays the Main window.");

            MakeTestStepHeader(8, UniqueIdentifier++,
                "Perform the remaining part of SoM to enter mode SR, ETCS level 1 and then open the Main window",
                "DMI displays the Main window.Verify that the Driver ID button is enabled");
            /*
            Test Step 8
            Action: Perform the remaining part of SoM to enter mode SR, ETCS level 1 and then open the Main window
            Expected Result: DMI displays the Main window.Verify that the Driver ID button is enabled
            */
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode =
                EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.StaffResponsible;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Level = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_LEVEL.L1;

            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.None;
            EVC30_MMIRequestEnable.Send();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Main;
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.DriverID;
            EVC30_MMIRequestEnable.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Main window." + Environment.NewLine +
                                "2. The ‘Driver ID’ button is enabled.");

            MakeTestStepHeader(9, UniqueIdentifier++,
                "Press Driver ID button and verify the presentation on the DMI screen",
                "The Driver ID window is displayed with the current Driver ID.1917705651500 Use the log file to confirm that DMI receives EVC-14 with variable MMI_X_DRIVER_ID = 0x31323334353637000000000000000000 and replace the current data value with “1234 567” in input field.Note: A value of MMI_X_DRIVER_ID shows as hexadecimal value of ASCII which corresponds to its character that displayed in the input field in the Driver ID window.An input field is used to revalidation the Driver ID.The following objects are displayed in Driver ID window. Enabled Close button (NA11)Window TitleInput Field");
            /*
            Test Step 9
            Action: Press Driver ID button and verify the presentation on the DMI screen
            Expected Result: The Driver ID window is displayed with the current Driver ID.1917705651500 Use the log file to confirm that DMI receives EVC-14 with variable MMI_X_DRIVER_ID = "0x31323334353637000000000000000000" and replace the current data value with “1234 567” in input field.Note: A value of MMI_X_DRIVER_ID shows as hexadecimal value of ASCII which corresponds to its character that displayed in the input field in the Driver ID window.An input field is used to revalidation the Driver ID.The following objects are displayed in Driver ID window. Enabled Close button (NA11)Window TitleInput Field
            Test Step Comment: (1) MMI_gen 183 (partly: active);(2) MMI_gen 8035 (partly: revalidation);(3) MMI_gen 8033 (party: MMI_gen 4722 (partly: Table 12 <Close> button, Window title, Input field)) MMI_gen 4392 (partly: [Close] NA11); MMI_gen 4396 (partly: Close, NA11);
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Driver ID’ button");

            EVC14_MMICurrentDriverID.MMI_X_DRIVER_ID = "1234567";
            EVC14_MMICurrentDriverID.MMI_Q_ADD_ENABLE = EVC14_MMICurrentDriverID.MMI_Q_ADD_ENABLE_BUTTONS.Settings &
                                                        ~EVC14_MMICurrentDriverID.MMI_Q_ADD_ENABLE_BUTTONS.TRN;
            EVC14_MMICurrentDriverID.MMI_Q_CLOSE_ENABLE = Variables.MMI_Q_CLOSE_ENABLE.Disabled;
            EVC14_MMICurrentDriverID.Send();

            WaitForVerification("Check the following (* indicates sub-areas drawn as one area):" + Environment.NewLine +
                                Environment.NewLine +
                                "1. DMI displays the Driver ID window as a half-grid array with the title ‘Driver ID’ (with a keypad of 4 rows of 3 keys)." +
                                Environment.NewLine +
                                "2. The keypad is displayed below the data input field with (enabled) keys <1>, <2 abc> to <9 wxyz>, <Del> (symbol NA21), <0> and a (disabled) <Decimal_Separator> key." +
                                Environment.NewLine +
                                "3. A data entry window with one data entry field covers areas D, F and G." +
                                Environment.NewLine +
                                @"4. The ‘Settings’ button is displayed enabled in the bottom right-hand corner of areas D, F & G." +
                                Environment.NewLine +
                                @"5. The ‘Train running number’ button is displayed disabled to the left of the ‘Settings’ button." +
                                Environment.NewLine +
                                "6. The data input field displays ‘1234 567’ in areas D & F with only a Data part for re-validation of the Driver ID." +
                                Environment.NewLine +
                                @"7. The Driver ID window displays a (disabled) ‘Close’ button, symbol NA12, in the bottom left corner of areas D, F & G.");

            MakeTestStepHeader(10, UniqueIdentifier++, "Press and hold an input field",
                "Verify the following information,(1)    The state of an input field is changed to ‘Pressed’, the border of button is removed");
            /*
            Test Step 10
            Action: Press and hold an input field
            Expected Result: Verify the following information,(1)    The state of an input field is changed to ‘Pressed’, the border of button is removed
            Test Step Comment: (1) MMI_gen 9390 (partly: Driver ID window);
            */
            DmiActions.ShowInstruction(this, @"Press and hold the data input field");

            WaitForVerification("The data input field is displayed pressed, without a border.");

            MakeTestStepHeader(11, UniqueIdentifier++, "Slide out an input field",
                "Verify the following information,(1)    The state of an input field is changed to ‘Enabled, the border of button is shown without a sound");
            /*
            Test Step 11
            Action: Slide out an input field
            Expected Result: Verify the following information,(1)    The state of an input field is changed to ‘Enabled, the border of button is shown without a sound
            Test Step Comment: (1) MMI_gen 9390 (partly: Driver ID window);
            */
            DmiActions.ShowInstruction(this, @"Whilst keeping the data input field pressed, drag it out of its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The data input field is displayed enabled, with a border." + Environment.NewLine +
                                "2. No sound is played.");

            MakeTestStepHeader(12, UniqueIdentifier++, "Slide back into an input field",
                "Verify the following information,(1)    The state of an input field is changed to ‘Pressed’, the border of button is removed");
            /*
            Test Step 12
            Action: Slide back into an input field
            Expected Result: Verify the following information,(1)    The state of an input field is changed to ‘Pressed’, the border of button is removed
            Test Step Comment: (1) MMI_gen 9390 (partly: Driver ID window);
            */
            DmiActions.ShowInstruction(this,
                @"Whilst keeping the data input field pressed, drag it back inside its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The data input field is displayed pressed, without a border." +
                                Environment.NewLine +
                                "2. No sound is played.");

            MakeTestStepHeader(13, UniqueIdentifier++, "Release the pressed area",
                "DMI closes the Driver ID window and displays Main window. Use the log file to verify that DMI sends out EVC-104 with variable MMI_X_DRIVER_ID = “0x 31323334353637000000000000000000”.Note: A value of MMI_X_DRIVER_ID shows as hexadecimal value of ASCII which corresponds to its character that displayed in the input field in the Driver ID window");
            /*
            Test Step 13
            Action: Release the pressed area
            Expected Result: DMI closes the Driver ID window and displays Main window. Use the log file to verify that DMI sends out EVC-104 with variable MMI_X_DRIVER_ID = “0x 31323334353637000000000000000000”.Note: A value of MMI_X_DRIVER_ID shows as hexadecimal value of ASCII which corresponds to its character that displayed in the input field in the Driver ID window
            Test Step Comment: (1) MMI_gen 184 (partly: closed), MMI_gen 8033 (partly: MMI_gen 4682, MMI_gen 4681 (partly: accept the entered value), MMI_gen 4684 (partly: terminated);(2) MMI_gen 8035 (partly: revalidation), MMI_gen 184 (partly: EVC-104); MMI_gen 9390 (partly: Driver ID window);
            */
            DmiActions.ShowInstruction(this, @"Release the data input field");

            EVC104_MMINewDriverData.Check_Entered_MMI_X_DRIVER_ID = "1234567";

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI closes the Driver ID window and displays the Main window.");

            MakeTestStepHeader(14, UniqueIdentifier++, "Open the Driver ID window in the Main menu window",
                "The Driver ID window is displayed.Number 1234567 is still displays on the input field.Use the log file to verify that DMI receives EVC-14 with variable MMI_X_DRIVER_ID = 0x31323334353637000000000000000000.Note: A value of MMI_X_DRIVER_ID shows as hexadecimal value of ASCII which corresponds to its character that displayed in the input field in the Driver ID window");
            /*
            Test Step 14
            Action: Open the Driver ID window in the Main menu window
            Expected Result: The Driver ID window is displayed.Number 1234567 is still displays on the input field.Use the log file to verify that DMI receives EVC-14 with variable MMI_X_DRIVER_ID = "0x31323334353637000000000000000000".Note: A value of MMI_X_DRIVER_ID shows as hexadecimal value of ASCII which corresponds to its character that displayed in the input field in the Driver ID window
            Test Step Comment: (1) MMI_gen 8035 dth the current driver IDlay ‘s(partly: revalidation, post);(2) MMI_gen 183 (partly: active);
            */
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.None;
            EVC30_MMIRequestEnable.Send();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Main;
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.DriverID;
            EVC30_MMIRequestEnable.Send();

            DmiActions.ShowInstruction(this, @"Press the ‘Driver ID’ button");

            EVC14_MMICurrentDriverID.MMI_X_DRIVER_ID = "1234567";
            EVC14_MMICurrentDriverID.MMI_Q_ADD_ENABLE = EVC14_MMICurrentDriverID.MMI_Q_ADD_ENABLE_BUTTONS.Settings &
                                                        ~EVC14_MMICurrentDriverID.MMI_Q_ADD_ENABLE_BUTTONS.TRN;
            EVC14_MMICurrentDriverID.MMI_Q_CLOSE_ENABLE = Variables.MMI_Q_CLOSE_ENABLE.Disabled;
            EVC14_MMICurrentDriverID.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Driver ID window." + Environment.NewLine +
                                "2. The data input field still displays ‘1234 567’.");

            MakeTestStepHeader(15, UniqueIdentifier++, "Enter the new Driver ID as ‘987654’ in the input field",
                "The current data in the input field is replaced by the new entered data from the driver");
            /*
            Test Step 15
            Action: Enter the new Driver ID as ‘987654’ in the input field
            Expected Result: The current data in the input field is replaced by the new entered data from the driver
            */
            DmiActions.ShowInstruction(this, @"Enter ‘987654’ for the Driver ID");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The data input field displays ‘9876 54’.");

            MakeTestStepHeader(16, UniqueIdentifier++, "Confirm an entered data by pressing an input field",
                "(1) DMI closes the Driver ID window and displays Main window");
            /*
            Test Step 16
            Action: Confirm an entered data by pressing an input field
            Expected Result: (1) DMI closes the Driver ID window and displays Main window
            Test Step Comment: (1) MMI_gen 4681 (partly: accept the entered data);
            */
            DmiActions.ShowInstruction(this, "Confirm the entered data by pressing the data input field");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI closes the Driver ID window and displays the Main window.");

            MakeTestStepHeader(17, UniqueIdentifier++, "Press ‘Driver ID’ button",
                "Verify the following information,(1)    The Driver ID window is displayed with the entered data of the Driver ID from Step 15.Use the log file to confirm that DMI receives EVC-14 with variable MMI_X_DRIVER_ID = 0x39383736353400000000000000000000 and replace the current data value with “9876 54” in input field.Note: A value of MMI_X_DRIVER_ID shows as hexadecimal value of ASCII which corresponds to its character that displayed in the input field in the Driver ID window");
            /*
            Test Step 17
            Action: Press ‘Driver ID’ button
            Expected Result: Verify the following information,(1)    The Driver ID window is displayed with the entered data of the Driver ID from Step 15.Use the log file to confirm that DMI receives EVC-14 with variable MMI_X_DRIVER_ID = "0x39383736353400000000000000000000" and replace the current data value with “9876 54” in input field.Note: A value of MMI_X_DRIVER_ID shows as hexadecimal value of ASCII which corresponds to its character that displayed in the input field in the Driver ID window
            Test Step Comment: (1) MMI_gen 8033 (partly: MMI_gen 4681 (partly: entered value in the input field replace the current data)); MMI_gen 8864 (partly: Driver ID window); 
            */
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.None;
            EVC30_MMIRequestEnable.Send();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Main;
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.DriverID;
            EVC30_MMIRequestEnable.Send();

            DmiActions.ShowInstruction(this, @"Press the ‘Driver ID’ button");

            EVC14_MMICurrentDriverID.MMI_X_DRIVER_ID = "987654";
            EVC14_MMICurrentDriverID.MMI_Q_ADD_ENABLE = EVC14_MMICurrentDriverID.MMI_Q_ADD_ENABLE_BUTTONS.Settings &
                                                        ~EVC14_MMICurrentDriverID.MMI_Q_ADD_ENABLE_BUTTONS.TRN;
            EVC14_MMICurrentDriverID.MMI_Q_CLOSE_ENABLE = Variables.MMI_Q_CLOSE_ENABLE.Disabled;
            EVC14_MMICurrentDriverID.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Driver ID window." + Environment.NewLine +
                                "2. The data input field displays ‘9876 54’.");

            MakeTestStepHeader(18, UniqueIdentifier++,
                "Use the test script file 22_17_b.xml to send EVC-14 with,MMI_Q_ADD_ENABLE (#0)= 1MMI_Q_ADD_ENABLE (#1) = 1",
                "Verify the state of ‘TRN’ button and ‘Settings’ button as follows,‘Settings’ button is enabled.‘TRN’ button is enabled");
            /*
            Test Step 18
            Action: Use the test script file 22_17_b.xml to send EVC-14 with,MMI_Q_ADD_ENABLE (#0)= 1MMI_Q_ADD_ENABLE (#1) = 1
            Expected Result: Verify the state of ‘TRN’ button and ‘Settings’ button as follows,‘Settings’ button is enabled.‘TRN’ button is enabled
            Test Step Comment: (1) MMI_gen 8037 (partly: enabled settings, enabled TRN);
            */
            XML_22_17(msgType.typeb);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘TRN’ button and ‘Settings’ buttons are displayed enabled.");

            MakeTestStepHeader(19, UniqueIdentifier++,
                "Use the test script file 22_17_c.xml to send EVC-14 with,MMI_Q_ADD_ENABLE (#0)= 0MMI_Q_ADD_ENABLE (#1) = 0",
                "Verify the state of ‘TRN’ button and ‘Settings’ button as follows,‘Settings’ button is disabled.‘TRN’ button is disabled");
            /*
            Test Step 19
            Action: Use the test script file 22_17_c.xml to send EVC-14 with,MMI_Q_ADD_ENABLE (#0)= 0MMI_Q_ADD_ENABLE (#1) = 0
            Expected Result: Verify the state of ‘TRN’ button and ‘Settings’ button as follows,‘Settings’ button is disabled.‘TRN’ button is disabled
            Test Step Comment: (1) MMI_gen 8037 (partly: disabled settings, disabled TRN);
            */
            XML_22_17(msgType.typec);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘TRN’ button and ‘Settings’ buttons are displayed disabled.");

            MakeTestStepHeader(20, UniqueIdentifier++,
                "Use the test script file 22_17_d.xml to send EVC-14 with,MMI_Q_ADD_ENABLE (#0)= 0MMI_Q_ADD_ENABLE (#1) = 1",
                "Verify the state of ‘TRN’ button and ‘Settings’ button as follows,‘Settings’ button is enabled.‘TRN’ button is disabled");
            /*
            Test Step 20
            Action: Use the test script file 22_17_d.xml to send EVC-14 with,MMI_Q_ADD_ENABLE (#0)= 0MMI_Q_ADD_ENABLE (#1) = 1
            Expected Result: Verify the state of ‘TRN’ button and ‘Settings’ button as follows,‘Settings’ button is enabled.‘TRN’ button is disabled
            Test Step Comment: (1) MMI_gen 8037 (partly: enabled settings, disabled TRN);
            */
            XML_22_17(msgType.typed);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Settings’ buttons is displayed enabled." + Environment.NewLine +
                                "2. The ‘TRN’ button button is displayed disabled.");

            MakeTestStepHeader(21, UniqueIdentifier++,
                "Use the test script file 22_17_e.xml to send EVC-14 with,MMI_Q_ADD_ENABLE (#0)= 1MMI_Q_ADD_ENABLE (#1) = 0",
                "Verify the state of ‘TRN’ button and ‘Settings’ button as follows,‘Settings’ button is disabled.‘TRN’ button is enabled");
            /*
            Test Step 21
            Action: Use the test script file 22_17_e.xml to send EVC-14 with,MMI_Q_ADD_ENABLE (#0)= 1MMI_Q_ADD_ENABLE (#1) = 0
            Expected Result: Verify the state of ‘TRN’ button and ‘Settings’ button as follows,‘Settings’ button is disabled.‘TRN’ button is enabled
            Test Step Comment: (1) MMI_gen 8037 (partly: disabled settings, enabled TRN);
            */
            XML_22_17(msgType.typee);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Settings’ buttons is displayed disabled." + Environment.NewLine +
                                "2. The ‘TRN’ button button is displayed enabled.");

            MakeTestStepHeader(22, UniqueIdentifier++, "Close the Driver ID window",
                "DMI closes the Driver ID window and the Main menu window is displayed.(1)   Use the log file to verify that DMI sends out EVC-101 with variable MMI_M_REQUEST = 34 (Exit Driver Data Entry)");
            /*
            Test Step 22
            Action: Close the Driver ID window
            Expected Result: DMI closes the Driver ID window and the Main menu window is displayed.(1)   Use the log file to verify that DMI sends out EVC-101 with variable MMI_M_REQUEST = 34 (Exit Driver Data Entry)
            Test Step Comment: (1) MMI_gen 9962; MMI_gen 4392 (partly: returning to the parent window);
            */
            DmiActions.ShowInstruction(this, "Close the Driver ID window");

            EVC101_MMIDriverRequest.CheckMRequestReleased = Variables.MMI_M_REQUEST.ExitDriverDataEntry;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI closes the Driver ID window and displays the Main window.");

            TraceHeader("End of test");

            /*
            Test Step 23
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }

        #region Send_XML_22_17_DMI_Test_Specification

        enum msgType
        {
            typea,
            typeb,
            typec,
            typed,
            typee
        }

        private void XML_22_17(msgType type)
        {
            switch (type)
            {
                case msgType.typea:
                    EVC14_MMICurrentDriverID.MMI_X_DRIVER_ID = "4444";
                    EVC14_MMICurrentDriverID.MMI_Q_ADD_ENABLE =
                        EVC14_MMICurrentDriverID.MMI_Q_ADD_ENABLE_BUTTONS.Settings |
                        EVC14_MMICurrentDriverID.MMI_Q_ADD_ENABLE_BUTTONS.TRN;
                    EVC14_MMICurrentDriverID.MMI_Q_CLOSE_ENABLE = Variables.MMI_Q_CLOSE_ENABLE.Disabled;
                    break;
                case msgType.typeb:
                    EVC14_MMICurrentDriverID.MMI_X_DRIVER_ID = "";
                    EVC14_MMICurrentDriverID.MMI_Q_ADD_ENABLE =
                        EVC14_MMICurrentDriverID.MMI_Q_ADD_ENABLE_BUTTONS.Settings |
                        EVC14_MMICurrentDriverID.MMI_Q_ADD_ENABLE_BUTTONS.TRN;
                    EVC14_MMICurrentDriverID.MMI_Q_CLOSE_ENABLE = Variables.MMI_Q_CLOSE_ENABLE.Enabled;
                    break;
                case msgType.typec:
                    EVC14_MMICurrentDriverID.MMI_X_DRIVER_ID = "";
                    EVC14_MMICurrentDriverID.MMI_Q_ADD_ENABLE = (EVC14_MMICurrentDriverID.MMI_Q_ADD_ENABLE_BUTTONS) 0;
                    EVC14_MMICurrentDriverID.MMI_Q_CLOSE_ENABLE = Variables.MMI_Q_CLOSE_ENABLE.Enabled;
                    break;
                case msgType.typed:
                    EVC14_MMICurrentDriverID.MMI_X_DRIVER_ID = "";
                    EVC14_MMICurrentDriverID.MMI_Q_ADD_ENABLE =
                        EVC14_MMICurrentDriverID.MMI_Q_ADD_ENABLE_BUTTONS.Settings;
                    EVC14_MMICurrentDriverID.MMI_Q_CLOSE_ENABLE = Variables.MMI_Q_CLOSE_ENABLE.Enabled;
                    break;
                case msgType.typee:
                    EVC14_MMICurrentDriverID.MMI_X_DRIVER_ID = "";
                    EVC14_MMICurrentDriverID.MMI_Q_ADD_ENABLE = EVC14_MMICurrentDriverID.MMI_Q_ADD_ENABLE_BUTTONS.TRN;
                    EVC14_MMICurrentDriverID.MMI_Q_CLOSE_ENABLE = Variables.MMI_Q_CLOSE_ENABLE.Enabled;
                    break;
            }

            EVC14_MMICurrentDriverID.Send();
        }

        #endregion
    }
}
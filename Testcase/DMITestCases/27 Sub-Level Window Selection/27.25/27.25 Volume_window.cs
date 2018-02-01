using System;
using Testcase.Telegrams.EVCtoDMI;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 27.25 Volume window
    /// TC-ID: 22.25
    /// 
    /// This test case verifies the display of the ‘Volume’ window on DMI that shall comply with [ERA-ERTMS] standard and [MMI-ETCS-gen].
    /// 
    /// Tested Requirements:
    /// MMI_gen 8075; MMI_gen 8076; MMI_gen 8077; MMI_gen 8074 (partly: half grid array, single input field, only data part, MMI_gen 5189 (partly: touch screen), MMI_gen 5944 (partly: touch screen), MMI_gen 4640 (partly: only data area), MMI_gen 4679, MMI_gen 4720, MMI_gen 4889 (partly: merge label and data), MMI_gen 4722 (partly: Table 12 <Close> button, Window title, Input field), MMI_gen 4637 (partly: Main-areas D and F), note under the MMI_gen 9412, MMI_gen 4912, MMI_gen 4678, MMI_gen 4913 (partly: MMI_gen 4384, MMI_gen 4386 (partly: except 0.3s)), MMI_gen 4634, MMI_gen 4651, MMI_gen 4682, MMI_gen 4681, MMI_gen 4647 (partly: left aligned), MMI_gen 4684 (partly: terminated)); MMI_gen 4392 (partly: [Close] NA11, returning to the parent window); MMI_gen 4350; MMI_gen 4351; MMI_gen 4353; MMI_gen 9390 (partly: Volume window);
    /// 
    /// Scenario:
    /// The test system is powered on and the cabin is activated.The ‘Volume’ window is opened.The data entry of the ‘Volume’ window is verified
    /// 
    /// Used files:
    /// N/A
    /// </summary>
    public class TC_ID_22_25_Volume_window : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:

            // Call the TestCaseBase PreExecution
            base.PreExecution();

            // Test system is powered onCabin is activeSettings window is opened
            DmiActions.Start_ATP();
            DmiActions.Activate_Cabin_1(this);
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Default; // Settings
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.Volume;
            EVC30_MMIRequestEnable.Send();
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
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 0;
            // Testcase entrypoint


            TraceHeader("Test Step 1");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Press ‘Volume’ button");
            TraceReport("Expected Result");
            TraceInfo(
                "DMI displays the Volume window on the right half part of the window.LayersThe layers of window on half-grid array is displayed as followsLayer 0: Main-Area D, F, G, Y and Z.Layer -1: A1, A2+A3*, A4, B*, C1, C2+C3+C4*, C5, C6, C7, C8, C9, E1, E2, E3, E4, E5-E9*Layer -2: B3, B4, B5, B6, B7Note: ‘*’ symbol is mean that specified areas are drawn as one area.Data Entry windowThe window title is displayed with text “Volume”.Verify that the Volume window is displayed in main area D, F and G as half-grid array.A data entry window is contains only one input field that covers the Main area D, F and G.The following objects are displayed in Volume window. Enabled Close button (NA11)Window TitleInput FieldInput fieldThe input field is located in main area D and F.For a single input field, the window title is clearly explaining the topic of the input field. The Volume window is displayed as a single input field with only the data part.KeyboardThe keyboard associated to the Volume window is displayed as dedicated keyboard.The keyboard is presented below the area of input field.General property of windowThe Volume window is presented with objects and buttons which is the one of several levels and allocated to areas of DMIAll objects, text messages and buttons are presented within the same layer.The Default window is not displayed and covered the current window");
            /*
            Test Step 1
            Action: Press ‘Volume’ button
            Expected Result: DMI displays the Volume window on the right half part of the window.LayersThe layers of window on half-grid array is displayed as followsLayer 0: Main-Area D, F, G, Y and Z.Layer -1: A1, A2+A3*, A4, B*, C1, C2+C3+C4*, C5, C6, C7, C8, C9, E1, E2, E3, E4, E5-E9*Layer -2: B3, B4, B5, B6, B7Note: ‘*’ symbol is mean that specified areas are drawn as one area.Data Entry windowThe window title is displayed with text “Volume”.Verify that the Volume window is displayed in main area D, F and G as half-grid array.A data entry window is contains only one input field that covers the Main area D, F and G.The following objects are displayed in Volume window. Enabled Close button (NA11)Window TitleInput FieldInput fieldThe input field is located in main area D and F.For a single input field, the window title is clearly explaining the topic of the input field. The Volume window is displayed as a single input field with only the data part.KeyboardThe keyboard associated to the Volume window is displayed as dedicated keyboard.The keyboard is presented below the area of input field.General property of windowThe Volume window is presented with objects and buttons which is the one of several levels and allocated to areas of DMIAll objects, text messages and buttons are presented within the same layer.The Default window is not displayed and covered the current window
            Test Step Comment: (1) MMI_gen 8074 (partly: MMI_gen 5189 (partly: touch screen), MMI_gen 5944 (partly: touch screen));(2) MMI_gen 8075;(3) MMI_gen 8074 (partly: half grid array);(4) MMI_gen 8074 (partly: MMI_gen 4640 (partly: only data area), MMI_gen 4720, MMI_gen 4889 (partly: merge label and data));(5) MMI_gen 8074 (party: MMI_gen 4722 (partly: Table 12 <Close> button, Window title, Input field)); MMI_gen 4392 (partly: [Close] NA11);(6) MMI_gen 8074 (partly: MMI_gen 4637 (partly: Main-areas D and F));(7) MMI_gen 8074 (partly: note under the MMI_gen 9412);(8) MMI_gen 8074 (partly: single input field, only data part);(9) MMI_gen 8077; MMI_gen 8074 (partly: MMI_gen 4912);(10) MMI_gen 8074 (partly: MMI_gen 4678);(11) MMI_gen 4350;(12) MMI_gen 4351;(13) MMI_gen 4353; 
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press the ‘Volume’ button");

            WaitForVerification("Check the following (* indicates sub-areas drawn as one area):" + Environment.NewLine +
                                Environment.NewLine +
                                @"1. DMI displays the Volume window on the right half of the screen with 3 layers in a half-grid array, with the title ‘Volume’." +
                                Environment.NewLine +
                                "2. The Volume window is displayed in areas D, F and G." + Environment.NewLine +
                                "3. Layer 0 comprises areas D, F, G, Y and Z." + Environment.NewLine +
                                "4. Layer 1 comprises areas A1, (A2+A3)*, A4, B, C1, (C2+C3+c4)*, C5, C6, C7, C8, C9, E1, E2, E3, E4, (E5-E9)*." +
                                Environment.NewLine +
                                "5. Layer 2 comprises areas B3, B4, B5, B6 and B7." + Environment.NewLine +
                                @"6. The Volume windows also displays a data entry window with one data input field, covering areas D, F and G and an ‘Enabled Close’ button (symbol NA11)." +
                                Environment.NewLine +
                                "7. The data input field is in areas D and F and only has a data part." +
                                Environment.NewLine +
                                "8. A dedicated keypad (with <+> and <-> keys) is displayed below  the data input field." +
                                Environment.NewLine +
                                "9. Objects, text messages and buttons can be displayed in several levels. Within a level they are allocated to areas." +
                                Environment.NewLine +
                                "10. Objects, text messages and buttons in a layer form a window." +
                                Environment.NewLine +
                                "11. The Default window does not cover the current window.");

            TraceHeader("Test Step 2");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Press and hold ‘-’ button.Note: Stopwatch is required for accuracy of test result");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following information,While press and hold button less than 1.5 secSound ‘Click’ is played once.The state of ‘-‘ button is changed to ‘Pressed’ and immediately back to ‘Enabled’ stateThe eventually displayed data value (Data Area) is decreased after pressing the button.The single input field is used for the entry of the acoustic.The data value is displayed as black colour and the background of the data area is displayed as medium-grey colour.The data value of the input field is aligned to the left of the data area.While press and hold button over 1.5 secThe state ‘pressed’ and ‘released’ are switched repeatly while button is pressed and the value of input field is decreased repeatly refer to pressed state.Sound ‘Click’ is played repeatly while button is pressed");
            /*
            Test Step 2
            Action: Press and hold ‘-’ button.Note: Stopwatch is required for accuracy of test result
            Expected Result: Verify the following information,While press and hold button less than 1.5 secSound ‘Click’ is played once.The state of ‘-‘ button is changed to ‘Pressed’ and immediately back to ‘Enabled’ stateThe eventually displayed data value (Data Area) is decreased after pressing the button.The single input field is used for the entry of the acoustic.The data value is displayed as black colour and the background of the data area is displayed as medium-grey colour.The data value of the input field is aligned to the left of the data area.While press and hold button over 1.5 secThe state ‘pressed’ and ‘released’ are switched repeatly while button is pressed and the value of input field is decreased repeatly refer to pressed state.Sound ‘Click’ is played repeatly while button is pressed
            Test Step Comment: (1) MMI_gen 8074 (partly: MMI_gen 4913 (partly:  MMI_gen 4384 (partly: sound ‘Click’)));(2) MMI_gen 8074 (partly: MMI_gen 4913 (partly: MMI_gen 4384 (partly: Change to state ‘Pressed’ and immediately back to state ‘Enabled’)));   (3) MMI_gen 8074 (partly: MMI_gen 4679, MMI_gen 4913 (partly: MMI_gen 4384 (partly: ETCS-MMI’s function associated to the button)));(4) MMI_gen 8076 (partly: entry); MMI_gen 8074 (partly: MMI_gen 4634);(5) MMI_gen 8074 (partly: MMI_gen 4651);(6) MMI_gen 8074 (partly: MMI_gen 4647 (partly: left aligned));(7) MMI_gen 8074 (partly: MMI_gen 4913 (partly: MMI_gen 4386 (partly: visual of repeat function)));(8) MMI_gen 8074 (partly: MMI_gen 4913 (partly: MMI_gen 4386 (partly: audible of repeat function))); Note: The repeat function is expected refer to developer’s action list (no reference requirement).                                                         
            */
            DmiActions.ShowInstruction(this,
                "Press and hold the <-> key for more than 1.5s. Note: Stopwatch is required for accuracy of test result");

            WaitForVerification("Check the following:" + Environment.NewLine +
                                @"1. The ‘Click’ sound is played once." + Environment.NewLine +
                                "2. The <-> key is displayed pressed then immediately re-displayed enabled." +
                                Environment.NewLine +
                                "3. The value displayed in the data input field is decreased after pressing the key." +
                                Environment.NewLine +
                                "4. The data input field is used to enter the volume level." + Environment.NewLine +
                                "5. The data input field value is displayed in black, left-aligned, on a Medium-grey background." +
                                Environment.NewLine +
                                "6. When the key has been pressed for more than 1.5s, it is repeatedly displayed pressed then enabled and " +
                                Environment.NewLine +
                                "        the data input value is repeatedly decreased;" + Environment.NewLine +
                                @"7. The ‘Click’ sound is played repeatedly while the key is pressed.");

            TraceHeader("Test Step 3");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Release ‘-’ button");
            TraceReport("Expected Result");
            TraceInfo("Verify the following information,The input field is stop decreasing");
            /*
            Test Step 3
            Action: Release ‘-’ button
            Expected Result: Verify the following information,The input field is stop decreasing
            Test Step Comment: (1) MMI_gen 8064 (partly: MMI_gen 4913 (partly: MMI_gen 4384 (partly: ETCS-MMI’s function associated to the button)));
            */
            DmiActions.ShowInstruction(this, "Release the <-> key");

            WaitForVerification("Check the following:" + Environment.NewLine +
                                "1. The value displayed in the data input field stops decreasing.");

            TraceHeader("Test Step 4");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Press and hold ‘+’ button.Note: Stopwatch is required for accuracy of test result");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following information,While press and hold button less than 1.5 secSound ‘Click’ is played once.The state of ‘+‘ button is changed to ‘Pressed’ and immediately back to ‘Enabled’ stateThe eventually displayed data value (Data Area) is increased after pressing the button.While press and hold button over 1.5 secThe state ‘pressed’ and ‘released’ are switched repeatly while button is pressed and the value of input field is increased repeatly refer to pressed state.Sound ‘Click’ is played repeatly while button is pressed");
            /*
            Test Step 4
            Action: Press and hold ‘+’ button.Note: Stopwatch is required for accuracy of test result
            Expected Result: Verify the following information,While press and hold button less than 1.5 secSound ‘Click’ is played once.The state of ‘+‘ button is changed to ‘Pressed’ and immediately back to ‘Enabled’ stateThe eventually displayed data value (Data Area) is increased after pressing the button.While press and hold button over 1.5 secThe state ‘pressed’ and ‘released’ are switched repeatly while button is pressed and the value of input field is increased repeatly refer to pressed state.Sound ‘Click’ is played repeatly while button is pressed
            Test Step Comment: (1) MMI_gen 8074 (partly: MMI_gen 4913 (partly: MMI_gen 4384 (partly: sound ‘Click’)));(2) MMI_gen 8074 (partly: MMI_gen 4913 (partly: MMI_gen 4384 (partly: Change to state ‘Pressed’ and immediately back to state ‘Enabled’)));   (3) MMI_gen 8074 (partly: MMI_gen 4679, MMI_gen 4913 (partly:  MMI_gen 4384 (partly: ETCS-MMI’s function associated to the button))); (4) MMI_gen 8074 (partly: MMI_gen 4913 (partly: MMI_gen 4386 (partly: visual of repeat function)));(5) MMI_gen 8074 (partly: MMI_gen 4913 (partly: MMI_gen 4386 (partly: audible of repeat function)));Note: The repeat function is expected refer to developer’s action list (no reference requirement).                    
            */
            // Call g
            DmiActions.ShowInstruction(this,
                "Press and hold the <+> key for more than 1.5s. Note: Stopwatch is required for accuracy of test result");

            WaitForVerification("Check the following:" + Environment.NewLine +
                                @"1. The ‘Click’ sound is played once." + Environment.NewLine +
                                "2. The <+> key is displayed pressed then immediately re-displayed enabled." +
                                Environment.NewLine +
                                "3. The value displayed in the data input field is increased after pressing the key." +
                                Environment.NewLine +
                                "4. When the key has been pressed for more than 1.5s, it is repeatedly displayed pressed then enabled and " +
                                Environment.NewLine +
                                "        the data input value is repeatedly increased;" + Environment.NewLine +
                                @"5. The ‘Click’ sound is played repeatedly while the key is pressed.");

            TraceHeader("Test Step 5");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Release ‘+’ button");
            TraceReport("Expected Result");
            TraceInfo("Verify the following information,The input field is stop increasing");
            /*
            Test Step 5
            Action: Release ‘+’ button
            Expected Result: Verify the following information,The input field is stop increasing
            Test Step Comment: (1) MMI_gen 8074 (partly: MMI_gen 4913 (partly: MMI_gen 4384 (partly: ETCS-MMI’s function associated to the button)));
            */
            DmiActions.ShowInstruction(this, "Release the <+> key");

            WaitForVerification("Check the following:" + Environment.NewLine +
                                "1. The value displayed in the data input field stops increasing.");

            TraceHeader("Test Step 6");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Press and hold an input field");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following information,(1)    The state of an input field is changed to ‘Pressed’, the border of button is removed");
            /*
            Test Step 6
            Action: Press and hold an input field
            Expected Result: Verify the following information,(1)    The state of an input field is changed to ‘Pressed’, the border of button is removed
            Test Step Comment: (1) MMI_gen 9390 (partly: Volume window);
            */
            DmiActions.ShowInstruction(this, "Press in and hold the data input field");

            WaitForVerification("Check the following:" + Environment.NewLine +
                                "1. The data input field is displayed pressed, without a border.");

            TraceHeader("Test Step 7");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Slide out an input field");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following information,(1)    The state of an input field is changed to ‘Enabled, the border of button is shown without a sound");
            /*
            Test Step 7
            Action: Slide out an input field
            Expected Result: Verify the following information,(1)    The state of an input field is changed to ‘Enabled, the border of button is shown without a sound
            Test Step Comment: (1) MMI_gen 9390 (partly: Volume window);
            */
            DmiActions.ShowInstruction(this, "Whilst keeping the data input field pressed, drag it out of its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The data input field is displayed enabled, with a border." + Environment.NewLine +
                                "2. No sound is played.");

            TraceHeader("Test Step 8");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Slide back into an input field");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following information,(1)    The state of an input field is changed to ‘Pressed’, the border of button is removed");
            /*
            Test Step 8
            Action: Slide back into an input field
            Expected Result: Verify the following information,(1)    The state of an input field is changed to ‘Pressed’, the border of button is removed
            Test Step Comment: (1) MMI_gen 9390 (partly: Volume window);
            */
            DmiActions.ShowInstruction(this,
                "Whilst keeping the the data input field pressed, drag it back inside its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The data input field is displayed pressed, without a border." +
                                Environment.NewLine +
                                "2. No sound is played.");

            TraceHeader("Test Step 9");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Released the pressed area");
            TraceReport("Expected Result");
            TraceInfo("(1)  The Volume window is closed. DMI displays the Settings window");
            /*
            Test Step 9
            Action: Released the pressed area
            Expected Result: (1)  The Volume window is closed. DMI displays the Settings window
            Test Step Comment: (1) MMI_gen 8074 (partly: MMI_gen 4682, MMI_gen 4681 (partly: accept the entered value), MMI_gen 4684 (partly: terminated)); MMI_gen 9390 (partly: Volume window);
            */
            DmiActions.ShowInstruction(this, "Release the pressed area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI closes the Volume window and displays the Settings window.");

            TraceHeader("Test Step 10");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Press ‘Volume’ button on Settings menu window");
            TraceReport("Expected Result");
            TraceInfo(
                "DMI displays Volume window.(1)   The acoustic is saved by remaining sound as confirmed an entered data is replaced in input field");
            /*
            Test Step 10
            Action: Press ‘Volume’ button on Settings menu window
            Expected Result: DMI displays Volume window.(1)   The acoustic is saved by remaining sound as confirmed an entered data is replaced in input field
            Test Step Comment: (1) MMI_gen 8074 (partly: MMI_gen 4681 (partly: entered data is replaced);
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Volume’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Volume window." + Environment.NewLine +
                                "2. The data input field displays the same value for volume level as previously confirmed.");

            TraceHeader("Test Step 11");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Confirm the current data without re-entry by pressing an input field.Then, press ‘Volume’ button");
            TraceReport("Expected Result");
            TraceInfo("DMI displays Volume window.The acoustic is still same");
            /*
            Test Step 11
            Action: Confirm the current data without re-entry by pressing an input field.Then, press ‘Volume’ button
            Expected Result: DMI displays Volume window.The acoustic is still same
            Test Step Comment: (1) MMI_gen 8076 (partly: revalidation);
            */
            DmiActions.ShowInstruction(this,
                "Without changing the volume level, confirm the current value by pressing in the data input field." +
                Environment.NewLine +
                @"Press the ‘Volume’ button in the Settings window.");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Volume window." + Environment.NewLine +
                                "2. The data input field displays the same value for volume level as before.");

            TraceHeader("Test Step 12");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Press ‘Close’ button");
            TraceReport("Expected Result");
            TraceInfo("Verify the following information,(1)   DMI displays the Settings window");
            /*
            Test Step 12
            Action: Press ‘Close’ button
            Expected Result: Verify the following information,(1)   DMI displays the Settings window
            Test Step Comment: (1) MMI_gen 4392 (partly: returning to the parent window);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Settings window.");

            TraceHeader("Test Step 13");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("End of test");
            
            /*
            Test Step 13
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
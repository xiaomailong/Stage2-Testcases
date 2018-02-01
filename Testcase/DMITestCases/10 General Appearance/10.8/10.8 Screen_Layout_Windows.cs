using System;
using Testcase.Telegrams.EVCtoDMI;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 10.8 Screen Layout: Windows
    /// TC-ID: 5.8
    /// 
    /// This test case verifies that the presentation of each window that displays on DMI’s screen. Verify that the Default window is displayed according to [ERA] standard and the visualisation, sound, and button activation of up-type buttons(Main, Spec, Setting buttons) in this window shall comply with condition of [MMI-ETCS-gen].  The other window(s) (other than default window) and its Sub-level window is displayed according to [ERA] standard.
    /// 
    /// Tested Requirements:
    /// MMI_gen 4350; MMI_gen 4351; MMI_gen 4352; MMI_gen 4353; MMI_gen 4354; MMI_gen 4355; MMI_gen 4361; MMI_gen 4358; MMI_gen 4360;                      MMI_gen 4381 (Main, Spec, Setting buttons); MMI_gen 4382 (Main, Spec, Setting buttons);
    /// 
    /// Scenario:
    /// Perform SoM and during start of mission observe the correctness of different windows.Verify the correctness of the presentation and the behavior of up-type buttons in default window.Continue with the Driver ID window and the Train data window. Verify the correctness of the presentation in each window.
    /// 
    /// Used files:
    /// N/A
    /// </summary>
    public class TC_ID_5_8_Screen_Layout_Windows : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // Power on the system.

            // Call the TestCaseBase PreExecution
            base.PreExecution();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in SB mode, Level 1
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Default window in SB mode, Level 1.");

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
            TraceInfo("Activate cabin A. ");
            TraceReport("Expected Result");
            TraceInfo("The Driver ID window is displayed. ");
            /*
            Test Step 1
            Action: Activate cabin A. 
            Then, observe the appearance of the Driver ID window
            Expected Result: The Driver ID window is displayed. 
            All objects, text messages and buttons are presented within the same layer
            Test Step Comment: MMI_gen 4351;
            */
            DmiActions.Activate_Cabin_1(this);
            DmiActions.Set_Driver_ID(this, "1234");
            DmiActions.Send_SB_Mode(this);
            DmiExpectedResults.Driver_ID_window_displayed_in_SB_mode(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Driver ID window." + Environment.NewLine +
                                "2. All objects, text messages and buttons are displayed in the same layer.");

            TraceHeader("Test Step 2");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Enter the Driver ID. ");
            TraceReport("Expected Result");
            TraceInfo("The Level window is displayed.");
            /*
            Test Step 2
            Action: Enter the Driver ID. 
                    Perform brake test. 
                    Then select level 1
            Expected Result: The Level window is displayed.
            Verify the following:
            The Sub-level window covers partially on the screen.
            When this window is active, driver cannot select anything on the default window underneath e.g. ‘Main menu’ or ‘Settings menu’.
            All objects, text messages and buttons are presented within the same layer
            Test Step Comment: MMI_gen 4354;MMI_gen 4351;
            */
            // Brake test checks are done in section 27.22: irrelevant here
            DmiActions.ShowInstruction(this, "Enter the Driver ID (and confirm). Then Press ok.");

            DmiActions.Request_Brake_Test(this);
            DmiActions.ShowInstruction(this, "Perform Brake Test");
            DmiActions.Perform_Brake_Test(this, 2);
            Wait_Realtime(5000);
            DmiActions.Display_Brake_Test_Successful(this, 3);

            DmiActions.Display_Level_Window(this);
            DmiActions.Delete_Brake_Test_Successful(this, 3);

            DmiActions.ShowInstruction(this, "Select and enter Level 1");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Level window." + Environment.NewLine +
                                "2. The Sub-level window partially covers the screen." + Environment.NewLine +
                                "3. While this window is active, the driver cannot select anything on the default window underneath e.g. ‘Main menu’ or ‘Settings menu’." +
                                Environment.NewLine +
                                "4. All objects, text messages and buttons are displayed in the same layer.");

            TraceHeader("Test Step 3");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Confirm level 1 and Select ‘Train data’ button");
            TraceReport("Expected Result");
            TraceInfo("The Train data window is displayed.");
            /*
            Test Step 3
            Action: Confirm level 1 and Select ‘Train data’ button
            Expected Result: The Train data window is displayed.
            Verify the following:
            (1)   All objects, text messages and buttons are presented within the same layer.
            (2)   At the top of the window, it displays title with text ‘Train data’, 
            background is black and text label is displayed as grey colour.
            (3)   For the title, when the number of DMI objects cannot fit within a window is displayed as (1/2) i.e. in this case it displays Train data (1/2) and Train data (2/2).
            (4)   The Data entry window contains a maximum of 4 or 3 input field. 
            (5)   A close button is displayed as enabled.
            (6)   Sub-level window covers totally depending on the size of the Sub-Level window.
            (7)   ‘Next’ and/or ‘Previous’ button is enabled. 
            The scrolling between various windows is not displayed as circular
            Test Step Comment: MMI_gen 4351;MMI_gen 4354;MMI_gen 4355;MMI_gen 4358;MMI_gen 4360;
            */
            DmiActions.ShowInstruction(this, "Accept level 1");

            DmiActions.Display_Main_Window_with_Start_button_not_enabled(this);
            DmiActions.ShowInstruction(this, @"Press ‘Train data’ button");

            DmiActions.Display_Fixed_Train_Data_Window(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Train data window." + Environment.NewLine +
                                "2. All objects, text messages and buttons are displayed in the same layer." +
                                Environment.NewLine +
                                "3. The window title ‘Train data’ is displayed at the top in grey on a black background." +
                                Environment.NewLine +
                                "4. The window title displayed is ‘Train data (1/2)’ and if scrolled to the other page using ‘Next’ or ‘Previous’ button is displayed as ‘Train data (2/2)’." +
                                Environment.NewLine +
                                "5. A ‘Close’ button is displayed enabled." + Environment.NewLine +
                                "6. The Sub-Level window is on layer more raised than its parent and, depending on its size, may totally cover its parent" +
                                "7. The ‘Next’ and/or ‘Previous’ button is enabled. Scrolling between windows is not circular.");

            TraceHeader("Test Step 4");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Confirm and Validate Train data.");
            TraceReport("Expected Result");
            TraceInfo("The Train Running Number window is displayed.");
            /*
            Test Step 4
            Action: Confirm and Validate Train data.
            Then, observe the appearance of the Train Running Number window
            Expected Result: The Train Running Number window is displayed.
            All objects, text messages and buttons are presented within the same layer
            Test Step Comment: MMI_gen 4351;
            */
            DmiActions.ShowInstruction(this, @"Enter FLU and confirm value in each input field.");

            DmiActions.Enable_Fixed_Train_Data_Validation(this, Variables.Fixed_Trainset_Captions.FLU);
            DmiActions.ShowInstruction(this, @"Press ‘Yes’ button.");

            DmiActions.Complete_Fixed_Train_Data_Entry(this, Variables.Fixed_Trainset_Captions.FLU);
            DmiActions.ShowInstruction(this, @"Perform the following actions on the DMI: " + Environment.NewLine +
                                             Environment.NewLine +
                                             "1. Press ‘Yes’ button." + Environment.NewLine +
                                             "2. Confirmed the selected value by pressing the input field." +
                                             Environment.NewLine +
                                             "3. Press OK on THIS window.");

            DmiActions.Display_Train_data_validation_Window(this);
            DmiActions.ShowInstruction(this, @"Perform the following actions on the DMI: " + Environment.NewLine +
                                             Environment.NewLine +
                                             "1. Press ‘Yes’ button." + Environment.NewLine +
                                             "2. Confirmed the selected value by pressing the input field.");

            DmiActions.Display_TRN_Window(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Train Running Number window." + Environment.NewLine +
                                "2. All objects, text messages and buttons are displayed in the same layer.");

            TraceHeader("Test Step 5");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Enter and confirm the train running number. Then close the Main window");
            TraceReport("Expected Result");
            TraceInfo("The Default window is displayed.");
            /*
            Test Step 5
            Action: Enter and confirm the train running number. Then close the Main window
            Expected Result: The Default window is displayed.
            Verify the following:
            The Default window is presented as  ‘Total image’ and displayed area with allocation of objects, text messages, and buttons.
            The Default window is not composed of title, Input field, Close button, ‘Next’ or ‘Previous’ button, 
            The Default window is not displayed the topic of the window.
            (4)   The Default window is not covering other windows
            Test Step Comment: MMI_gen 4350;MMI_gen 4352;MMI_gen 4353;MMI_gen 4361;Check more information about ‘Total image’ in [MMI-ETCS-gen]
            */
            DmiActions.ShowInstruction(this, "Enter and confirm Train Running Number");

            DmiActions.Display_Main_Window_with_Start_button_enabled(this);

            DmiActions.ShowInstruction(this, "Close the Main window");

            //DmiActions.Display_Default_Window(this); (optional)

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Default window." + Environment.NewLine +
                                "2. All objects, text messages and buttons are displayed in the same layer." +
                                Environment.NewLine +
                                @"3. The Default window does not contain a title, any Input fields, ‘Close’, ‘Next’ or ‘Previous’ buttons" +
                                Environment.NewLine +
                                "4. The Default window does not display the window topic." + Environment.NewLine +
                                "5. The Default window does not cover any other window.");

            TraceHeader("Test Step 6");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Then press and hold the Main menu button");
            TraceReport("Expected Result");
            TraceInfo("DMI still displays the default window.");
            /*
            Test Step 6
            Action: Then press and hold the Main menu button
            Expected Result: DMI still displays the default window.
            The Main menu button is shown as pressed state.
            The sound ‘click’ is played sound once
            Test Step Comment: MMI_gen 4381;
            */
            DmiActions.ShowInstruction(this, "Press and hold the ‘Main menu’ button");
            //Telegrams.DMItoEVC.EVC101_MMIDriverRequest.CheckMRequestPressed = ? (Main menu button state)
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI still displays the default window." + Environment.NewLine +
                                "2. The ‘Main menu’ button is displayed pressed." + Environment.NewLine +
                                "3. The ‘Click’ sound is played once.");

            TraceHeader("Test Step 7");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Slide out of the ‘Main menu’ button");
            TraceReport("Expected Result");
            TraceInfo("DMI still displays the default window");
            /*
            Test Step 7
            Action: Slide out of the ‘Main menu’ button
            Expected Result: DMI still displays the default window
            The visualisation of Main menu button is displayed as enabled state
            Test Step Comment: MMI_gen 4382;
            */
            DmiActions.ShowInstruction(this, "Whilst keeping the ‘Main menu’ button pressed, drag it out of its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI still displays the default window." + Environment.NewLine +
                                @"2. The ‘Main menu’ button is displayed enabled.");

            TraceHeader("Test Step 8");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Slide back into the ‘Main menu’ button");
            TraceReport("Expected Result");
            TraceInfo("DMI still displays default window");
            /*
            Test Step 8
            Action: Slide back into the ‘Main menu’ button
            Expected Result: DMI still displays default window
            The visualisation of Main menu button is displayed as pressed state.
            The sound ‘click’ is not played
            Test Step Comment: MMI_gen 4382;
            */
            DmiActions.ShowInstruction(this,
                "Whilst keeping the ‘Main menu’ button pressed, drag it back inside its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI still displays the default window." + Environment.NewLine +
                                @"2. The ‘Main menu’ button is displayed pressed." + Environment.NewLine +
                                @"3. The ‘Click’ sound is not played.");

            TraceHeader("Test Step 9");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Release the ‘Main menu’ button");
            TraceReport("Expected Result");
            TraceInfo("DMI displays sub-menu of the Main window");
            /*
            Test Step 9
            Action: Release the ‘Main menu’ button
            Expected Result: DMI displays sub-menu of the Main window
            Test Step Comment: MMI_gen 4381;   MMI_gen 4382;
            */
            DmiActions.ShowInstruction(this, "Release the ‘Main menu’ button");

            DmiActions.Display_Main_Window_with_Start_button_enabled(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the sub-menu of the Main window.");

            TraceHeader("Test Step 10");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Press ‘Close’ button");
            TraceReport("Expected Result");
            TraceInfo("DMI displays the default window");
            /*
            Test Step 10
            Action: Press ‘Close’ button
            Expected Result: DMI displays the default window
            Test Step Comment: MMI_gen 4381;
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button");
            //DmiActions.Display_Default_Window(this); (optional)

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Default window.");

            TraceHeader("Test Step 11");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Press and hold the Special button");
            TraceReport("Expected Result");
            TraceInfo("DMI still displays the default window");
            /*
            Test Step 11
            Action: Press and hold the Special button
            Expected Result: DMI still displays the default window
            The Special button is shown as pressed state.
            The sound ‘click’ is played once
            Test Step Comment: MMI_gen 4381;
            */
            DmiActions.ShowInstruction(this, @"Press and hold the ‘Special’ button");
            //Telegrams.DMItoEVC.EVC101_MMIDriverRequest.CheckMRequestPressed = ? (Special button state)
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI still displays the Default window." + Environment.NewLine +
                                @"2. The ‘Special’ button is displayed pressed" + Environment.NewLine +
                                @"3. The ‘Click’ sound is played once.");

            TraceHeader("Test Step 12");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Slide out of Special button");
            TraceReport("Expected Result");
            TraceInfo("DMI still displays the default window");
            /*
            Test Step 12
            Action: Slide out of Special button
            Expected Result: DMI still displays the default window
            The visualisation of Special button is shown as enabled state
            Test Step Comment: MMI_gen 4382;
            */
            DmiActions.ShowInstruction(this, "Whilst keeping the ‘Special’ button pressed, drag it out of its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI still displays the default window." + Environment.NewLine +
                                @"2. The ‘Special’ button is displayed enabled.");

            TraceHeader("Test Step 13");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Slide back into ‘Special menu’ button");
            TraceReport("Expected Result");
            TraceInfo("DMI still displays the default window");
            /*
            Test Step 13
            Action: Slide back into ‘Special menu’ button
            Expected Result: DMI still displays the default window
            The visualisation of Special button is displayed in pressed state
            The sound ‘click’ is not played
            Test Step Comment: MMI_gen 4382;
            */
            DmiActions.ShowInstruction(this,
                "Whilst keeping the ‘Special’ button pressed, drag it back inside its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI still displays the default window." + Environment.NewLine +
                                @"2. The ‘Special’ button is displayed pressed." + Environment.NewLine +
                                @"3. The ‘Click’ sound is not played.");

            TraceHeader("Test Step 14");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Release ‘Special menu’ button");
            TraceReport("Expected Result");
            TraceInfo("DMI displays sub-menu of Special window");
            /*
            Test Step 14
            Action: Release ‘Special menu’ button
            Expected Result: DMI displays sub-menu of Special window
            Test Step Comment: MMI_gen 4381;   MMI_gen 4382;
            */
            DmiActions.ShowInstruction(this, "Release the ‘Special’ button");

            DmiActions.Open_the_Special_window(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the sub-menu of the Special window.");

            TraceHeader("Test Step 15");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Press ‘Close’ button");
            TraceReport("Expected Result");
            TraceInfo("DMI displays the default window");
            /*
            Test Step 15
            Action: Press ‘Close’ button
            Expected Result: DMI displays the default window
            Test Step Comment: MMI_gen 4381;
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button");
            //DmiActions.Display_Default_Window(this); (optional)

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI  displays the default window.");

            TraceHeader("Test Step 16");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Press and hold the ‘Settings menu’ button");
            TraceReport("Expected Result");
            TraceInfo("DMI still displays the default window");
            /*
            Test Step 16
            Action: Press and hold the ‘Settings menu’ button
            Expected Result: DMI still displays the default window
            The Setting button is displayed as pressed state.
            The sound ‘click’ is played once
            Test Step Comment: MMI_gen 4381;
            */
            DmiActions.ShowInstruction(this, @"Press and hold the ‘Settings menu’ button");
            Telegrams.DMItoEVC.EVC101_MMIDriverRequest.CheckMRequestPressed = Variables.MMI_M_REQUEST.Settings;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI still displays the default window." + Environment.NewLine +
                                @"2. The ‘Settings menu’ button is displayed pressed" + Environment.NewLine +
                                @"3. The ‘Click’ sound is played once.");

            TraceHeader("Test Step 17");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Slide out of the ‘Setting menu’ button");
            TraceReport("Expected Result");
            TraceInfo("DMI still displays the default window");
            /*
            Test Step 17
            Action: Slide out of the ‘Setting menu’ button
            Expected Result: DMI still displays the default window
            The Setting button is shown as enabled state
            Test Step Comment: MMI_gen 4382;
            */
            DmiActions.ShowInstruction(this,
                "Whilst keeping the ‘Settings menu’ button pressed, drag it out of its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI still displays the default window." + Environment.NewLine +
                                @"2. The ‘Settings menu’ button is displayed enabled.");

            TraceHeader("Test Step 18");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Slide back into the ‘Settings menu’ button");
            TraceReport("Expected Result");
            TraceInfo("DMI still displays the default window");
            /*
            Test Step 18
            Action: Slide back into the ‘Settings menu’ button
            Expected Result: DMI still displays the default window
            The Setting button is shown as  pressed state.
            The sound ‘click’ is not played
            Test Step Comment: MMI_gen 4382;
            */
            DmiActions.ShowInstruction(this,
                "Whilst keeping the ‘Settings menu’ button pressed, drag it back inside its area");

            Telegrams.DMItoEVC.EVC101_MMIDriverRequest.CheckMRequestPressed = Variables.MMI_M_REQUEST.Settings;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI still displays the default window." + Environment.NewLine +
                                @"2. The ‘Settings menu’ button is displayed pressed." + Environment.NewLine +
                                @"3. The ‘Click’ sound is not played.");

            TraceHeader("Test Step 19");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Release the ‘Settings menu’ button");
            TraceReport("Expected Result");
            TraceInfo("DMI displays all sub-menus of Setting window");
            /*
            Test Step 19
            Action: Release the ‘Settings menu’ button
            Expected Result: DMI displays all sub-menus of Setting window
            Test Step Comment: MMI_gen 4381;   MMI_gen 4382;
            */
            DmiActions.ShowInstruction(this, "Release the ‘Settings menu’ button");
            Telegrams.DMItoEVC.EVC101_MMIDriverRequest.CheckMRequestReleased = Variables.MMI_M_REQUEST.Settings;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays all the sub-menus of the Settings window.");

            TraceHeader("Test Step 20");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("End of test");
            TraceReport("Expected Result");
            TraceInfo("");
            /*
            Test Step 20
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
using System;
using Testcase.Telegrams.EVCtoDMI;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 36.1 The relationship between parent and child windows (1)
    /// TC-ID: 33.1
    /// 
    /// This test case verifies the relationship between parent and child window when the driver presses ‘Close’ button in each window. The relationship between parent and child windows shall comply with [ERA-ERTMS] standard and [MMI-ETCS-gen].
    /// 
    /// Tested Requirements:
    /// MMI_gen 8785 (partly: Driver ID window, Train data window, Train data validation window, Level window, Train running number window, Main window, Language window, Volume window, Brightness window, System version window, Settings window); MMI_gen 8787;
    /// 
    /// Scenario:
    /// Activate Cabin A.Press ‘Settings’ button.Close the Settings window to verify that the Settings window is closed and displayed the Driver ID window.Press ‘TRN’ button.Close the Train Running Number window to verify that the Train Running Number window is closed and displayed the Driver ID window.Enter Driver ID and perform brake test.Select and confirm Level 1.Press ‘Driver ID’ button.Close the Driver ID window to verify that the Driver ID window is closed and displayed the Main window.Press ‘Train data’ button. Close the train data window to verify that the Train data window is closed and the Main window is displayed.Press ‘Train data’ button.Enter and confirm all valid train data.Press ‘Yes’ button.Close the Train data validation window. Verify that the Train data validation window is closed and the Main window is displayed.Press ‘Level’ button.Close the Level window. Verify the Level window is closed and the Main window is displayed.Press ‘Train running number’ button.Close the Train Running Number window. Verify the train running number is closed and the Main window is displayed.Close the Main window. Verify that the Main window is closed and the Default window is displayed. 
    /// 
    /// Used files:
    /// N/A
    /// </summary>
    public class TC_ID_33_1_Parent_Child_Relationship : TestcaseBase
    {
        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays  in SB mode, Level 1
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in SB mode, Level 1.");

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 26483;
            // Testcase entrypoint

            MakeTestStepHeader(1, UniqueIdentifier++, "Activate cabin A", "The Driver ID window is displayed");
            /*
            Test Step 1
            Action: Activate cabin A
            Expected Result: The Driver ID window is displayed
            */
            // Call generic Action Method
            StartUp();

            // Call generic Check Results Method
            DmiExpectedResults.Driver_ID_window_displayed(this);

            MakeTestStepHeader(2, UniqueIdentifier++, "Press ‘Settings’ button", "The Settings window is displayed");
            /*
            Test Step 2
            Action: Press ‘Settings’ button
            Expected Result: The Settings window is displayed
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press the ‘Settings’ button");
            DmiActions.Open_the_Settings_window(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Settings window.");

            MakeTestStepHeader(3, UniqueIdentifier++, "Press ‘Close’ button",
                "Verify that the Settings window is closed. The Driver ID window is displayed");
            /*
            Test Step 3
            Action: Press ‘Close’ button
            Expected Result: Verify that the Settings window is closed. The Driver ID window is displayed
            Test Step Comment: MMI_gen 8787 (partly: Close the Settings window);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button");

            //DmiActions.Display_Driver_ID_Window(this, "1234"); (optionnal?)

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI closes the Settings window and displays the Driver ID window.");
            MakeTestStepHeader(4, UniqueIdentifier++, "Press ‘TRN’ button",
                "The Train Running Number window is displayed");
            /*
            Test Step 4
            Action: Press ‘TRN’ button
            Expected Result: The Train Running Number window is displayed
            */
            DmiActions.ShowInstruction(this, @"Press ‘TRN’ button");

            DmiActions.Display_TRN_Window(this);
            DmiExpectedResults.TRN_window_displayed(this);

            MakeTestStepHeader(5, UniqueIdentifier++, "Press ‘Close’ button",
                "Verify that the Train Running Number window is closed. The Driver ID window is displayed");
            /*
            Test Step 5
            Action: Press ‘Close’ button
            Expected Result: Verify that the Train Running Number window is closed. The Driver ID window is displayed
            Test Step Comment: MMI_gen 8787 (partly: Close the Train Running Number window);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button");
            //DmiActions.Display_Driver_ID_Window(this, "1234"); (optionnal?)

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI closes the Train Running Number window and displays the Driver ID window.");

            MakeTestStepHeader(6, UniqueIdentifier++, "Enter Driver ID and perform brake test",
                "The Level window is displayed");
            /*
            Test Step 6
            Action: Enter Driver ID and perform brake test
            Expected Result: The Level window is displayed
            */
            DmiActions.ShowInstruction(this, "Enter and confirm Driver ID");

            DmiActions.Request_Brake_Test(this);
            DmiActions.ShowInstruction(this, "Perform Brake Test");
            DmiActions.Perform_Brake_Test(this, 2);
            Wait_Realtime(5000);
            DmiActions.Display_Brake_Test_Successful(this, 3);

            DmiActions.Display_Level_Window(this);
            DmiActions.Delete_Brake_Test_Successful(this, 3);

            MakeTestStepHeader(7, UniqueIdentifier++, "Select and confirm Level 1", "The Main window is displayed");
            /*
            Test Step 7
            Action: Select and confirm Level 1
            Expected Result: The Main window is displayed
            */
            DmiActions.ShowInstruction(this, "Select and enter Level 1");

            DmiActions.Display_Main_Window_with_Start_button_not_enabled(this);

            MakeTestStepHeader(8, UniqueIdentifier++, "Press ‘Driver ID’ button",
                "The Driver ID window is displayed. The ‘Close’ button is presented as enabled state");
            /*
            Test Step 8
            Action: Press ‘Driver ID’ button
            Expected Result: The Driver ID window is displayed. The ‘Close’ button is presented as enabled state
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press the ‘Driver ID’ button");

            DmiActions.Display_Driver_ID_Window(this, "1234");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Driver ID window." + Environment.NewLine +
                                "2. The ‘Close’ button is displayed enabled.");

            MakeTestStepHeader(9, UniqueIdentifier++, "Close the Driver ID window",
                "Verify that the Driver ID window is closed. The Main window is displayed");
            /*
            Test Step 9
            Action: Close the Driver ID window
            Expected Result: Verify that the Driver ID window is closed. The Main window is displayed
            Test Step Comment: MMI_gen 8785 (partly: Close the Driver ID window);
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button in the Driver ID window");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI closes the Driver ID window and displays the Main window.");

            MakeTestStepHeader(10, UniqueIdentifier++, "Press ‘Train data’ button",
                "The Train data window is displayed. ");
            /*
            Test Step 10
            Action: Press ‘Train data’ button
            Expected Result: The Train data window is displayed. 
            The ‘Close’ button is presented as enabled state
            */
            // Call generic Action Method
            DmiExpectedResults.Train_Data_Button_pressed_and_released(this);

            DmiActions.Display_Fixed_Train_Data_Window(this);
            DmiExpectedResults.Train_data_window_displayed(this);

            MakeTestStepHeader(11, UniqueIdentifier++, "Close the Train data window",
                "Verify that the Train data window is closed. ");
            /*
            Test Step 11
            Action: Close the Train data window
            Expected Result: Verify that the Train data window is closed. 
            The Main window is displayed
            Test Step Comment: MMI_gen 8785 (partly: Close the Train data window);
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button in the Train data window");

            //DmiActions.Display_Main_Window_with_Start_button_not_enabled(this);

            DmiExpectedResults.Main_Window_displayed(this, false);

            MakeTestStepHeader(12, UniqueIdentifier++, "Press ‘Train data’ button",
                "The Train data window is displayed");
            /*
            Test Step 12
            Action: Press ‘Train data’ button
            Expected Result: The Train data window is displayed
            */
            // Call generic Action Method
            DmiExpectedResults.Train_Data_Button_pressed_and_released(this);

            DmiActions.Display_Fixed_Train_Data_Window(this);
            DmiExpectedResults.Train_data_window_displayed(this);

            MakeTestStepHeader(13, UniqueIdentifier++,
                "If the train data is fixed, re-select the train type and then press ‘Yes’ button.",
                "The Train data validation window is displayed.");
            /*
            Test Step 13
            Action: If the train data is fixed, re-select the train type and then press ‘Yes’ button.
            If the train data is flexible, re-entry all train data and then press ‘Yes’ button
            Expected Result: The Train data validation window is displayed.
            The ‘Close’ button is presented as enabled state
            */
            DmiExpectedResults.Fixed_Train_Data_entered(this, Variables.Fixed_Trainset_Captions.FLU);

            DmiActions.Enable_Fixed_Train_Data_Validation(this, Variables.Fixed_Trainset_Captions.FLU);
            DmiExpectedResults.Fixed_Train_Data_validated(this, Variables.Fixed_Trainset_Captions.FLU);

            DmiActions.Complete_Fixed_Train_Data_Entry(this);

            Wait_Realtime(1000);

            DmiActions.Display_Train_data_validation_Window(this);
            DmiExpectedResults.Train_data_validation_window_displayed(this);

            MakeTestStepHeader(14, UniqueIdentifier++, "Press ‘Close’ button",
                "Verify that the Train data validation window is closed. ");
            /*
            Test Step 14
            Action: Press ‘Close’ button
            Expected Result: Verify that the Train data validation window is closed. 
            The Main window is displayed
            Test Step Comment: MMI_gen 8785 (partly: Close the Train data validation window);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button in the Train data validation window");

            //DmiActions.Display_Main_Window_with_Start_button_not_enabled(this);

            DmiExpectedResults.Main_Window_displayed(this, false);

            MakeTestStepHeader(15, UniqueIdentifier++, "Press ‘Level’ button",
                "The Level window is displayed. The ‘Close’ button is presented as enabled state");
            /*
            Test Step 15
            Action: Press ‘Level’ button
            Expected Result: The Level window is displayed. The ‘Close’ button is presented as enabled state
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press the ‘Level’ button");

            DmiActions.Display_Level_Window(this);
            DmiExpectedResults.Level_window_displayed(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Level window." + Environment.NewLine +
                                "2. The ‘Close’ button is displayed enabled.");

            MakeTestStepHeader(16, UniqueIdentifier++, "Close the Level window",
                "Verify that the Level window is closed. The Main window is displayed");
            /*
            Test Step 16
            Action: Close the Level window
            Expected Result: Verify that the Level window is closed. The Main window is displayed
            Test Step Comment: MMI_gen 8785 (partly: Close the Level window);
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button in the Level window");

            //DmiActions.Display_Main_Window_with_Start_button_not_enabled(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI closes the Level window and displays the Main window.");

            MakeTestStepHeader(17, UniqueIdentifier++, "Press ‘Train running number’ button",
                "The Train Running Number window is displayed. The ‘Close’ button is presented as enabled state");
            /*
            Test Step 17
            Action: Press ‘Train running number’ button
            Expected Result: The Train Running Number window is displayed. The ‘Close’ button is presented as enabled state
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Train Running Number’ button");

            DmiActions.Display_TRN_Window(this);
            DmiExpectedResults.TRN_window_displayed(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Train Running Number window." + Environment.NewLine +
                                "2. The ‘Close’ button is displayed enabled.");

            MakeTestStepHeader(18, UniqueIdentifier++, "Close the Train Running Number window",
                "Verify that the Train Running Number window is closed. The Main window is displayed");
            /*
            Test Step 18
            Action: Close the Train Running Number window
            Expected Result: Verify that the Train Running Number window is closed. The Main window is displayed
            Test Step Comment: MMI_gen 8785 (partly: Close the Train Running Number window);
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button in the  Train Running Number window");

            //DmiActions.Display_Main_Window_with_Start_button_not_enabled(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI closes the  Train Running Number window and displays the Main window.");

            MakeTestStepHeader(19, UniqueIdentifier++, "Press ‘Close’ button",
                "Verify that the Main window is closed. ");
            /*
            Test Step 19
            Action: Press ‘Close’ button
            Expected Result: Verify that the Main window is closed. 
            The Default window is displayed
            Test Step Comment: MMI_gen 8785 (partly: Close the Main window);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press ‘Close’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI closes the  Main window and displays the Default window.");

            TraceHeader("End of test");

            /*
            Test Step 20
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
using System;

namespace Testcase.DMITestCases
{
    /// <summary>
    /// 6.3.1 Performance of the new selection language
    /// TC-ID: 1.3.1
    /// 
    /// This test case verifies the presentation of all predefined  texts with default language when DMI powered on and start up. DMI shall support and save entire new selection language which made by driver.
    /// 
    /// Tested Requirements:
    /// MMI_gen 60; MMI_gen 61; MMI_gen 62,MMI_gen 11892; MMI_gen 11893; MMI_gen 4368; MMI_gen 7506; MMI_gen 7507; MMI_gen 11470 (partly: Bit # 29);
    /// 
    /// Scenario:
    /// Power on the system
    /// Activate cabin A.
    /// Enter the Driver ID and perform brake test.
    /// Select and confirm level 1.
    /// Verify that the text on windows, sub windows, buttons and text messages are displayed accroding to selected language
    /// Power off DMI for 10 mins and power on again.
    /// Verify that the default window represents the text with the selected language
    /// Restest with all stored languages
    /// 
    /// Used files:
    /// N/A
    /// </summary>
    public class TC_1_3_1_Performance_of_the_new_selection_language : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // System is powered off.Default Language Is Engilsh

            // Call the TestCaseBase PreExecution
            base.PreExecution();
            DmiActions.ShowInstruction(this, "This test need to be done with the real EVC" + Environment.NewLine +
                                             Environment.NewLine +
                                             "The aim of this test is to check that the text on sub windows and buttons are presented in Engilsh language. ");
        }

        public override bool TestcaseEntryPoint()
        {
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 0;
            // Testcase entrypoint


            TraceHeader("Test Step 1");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Power on DMI and start up the system");
            TraceReport("Expected Result");
            TraceInfo("‘Driver’s desk not active’ is displayed on DMI");
            /*
            Test Step 1
            Action: Power on DMI and start up the system
            Expected Result: ‘Driver’s desk not active’ is displayed on DMI
            */

            //DmiActions.Start_ATP();
            //WaitForVerification("Is ‘Driver’s desk not active’  displayed on DMI?");

            TraceHeader("Test Step 2");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Activate cabin A");
            TraceReport("Expected Result");
            TraceInfo("The Driver ID window is displayed.");
            /*
            Test Step 2
            Action: Activate cabin A
            Expected Result: The Driver ID window is displayed.
            Time between step 1 and the display of the Driver ID window is <= 120 seconds
            Test Step Comment: MMI_gen 60;
            */

            //DmiActions.Activate_Cabin_1(this);
            //DmiExpectedResults.Cabin_A_is_activated(this);

            //DmiActions.Set_Driver_ID(this, "1234");
            //DmiActions.Send_SB_Mode(this);
            //DmiExpectedResults.Driver_ID_window_displayed_in_SB_mode(this);

            TraceHeader("Test Step 3");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Perform the following procedure,");
            TraceReport("Expected Result");
            TraceInfo(
                "The Main window is presented with the text message ‘Brake test in progress’ is displayed as English language");
            /*
            Test Step 3
            Action: Perform the following procedure,
                    Enter Driver ID and perform brake test.
                    Select and confirm Level 1
            Expected Result: The Main window is presented with the text message ‘Brake test in progress’ is displayed as English language
            Test Step Comment: MMI_gen 61 (partly: Engilsh);
            */

            //DmiExpectedResults.Driver_ID_entered(this);

            //DmiActions.Request_Brake_Test(this, 1);
            //DmiExpectedResults.Brake_Test_Perform_Order(this, true);

            //DmiActions.Perform_Brake_Test(this, 2);
            //WaitForVerification("Is ‘Brake test in progress’ displayed as English language on DMI?");

            //Wait_Realtime(5000);

            //DmiActions.Display_Brake_Test_Successful(this, 3);

            //DmiActions.Display_Level_Window(this);
            //DmiExpectedResults.Level_window_displayed(this);

            //DmiActions.Delete_Brake_Test_Successful(this, 3);

            //DmiExpectedResults.Level_1_Selected(this);

            //DmiActions.Display_Main_Window_with_Start_button_not_enabled(this);

            TraceHeader("Test Step 4");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Enter all sub windows on DMI");
            TraceReport("Expected Result");
            TraceInfo("The text on sub windows and buttons are presented in Engilsh language");
            /*
            Test Step 4
            Action: Enter all sub windows on DMI
            Expected Result: The text on sub windows and buttons are presented in Engilsh language
            Test Step Comment: MMI_gen 61 (partly: Engilsh);
                               MMI_gen_11892 (partly: Engilsh);
                               MMI_gen 4368 (partly: (partly: language dependent text));
            */

            //DmiActions.ShowInstruction(this,"Enter all sub windows on DMI -> This need to be done with the real EVC" + Environment.NewLine + Environment.NewLine +
            //                           "The aim of this test is to check that the text on sub windows and buttons are presented in Engilsh language. ");

            TraceHeader("Test Step 5");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Complete start of mission");
            TraceReport("Expected Result");
            TraceInfo("The text on winsows, sub windows buttons and text messages are presented in Engilsh language");
            /*
            Test Step 5
            Action: Complete start of mission
            Expected Result: The text on winsows, sub windows buttons and text messages are presented in Engilsh language
            Test Step Comment: MMI_gen 61 (partly: Engilsh);MMI_gen_11892 (partly: Engilsh);MMI_gen 4368 (partly: language dependent text);
            */

            //DmiActions.ShowInstruction(this, @"Perform the following actions on the DMI: " + Environment.NewLine + Environment.NewLine +
            //                    "1. Press ‘Train data’ button." + Environment.NewLine +
            //                    "2. Press OK on THIS window.");

            //DmiActions.Display_Fixed_Train_Data_Window(this);
            //DmiActions.ShowInstruction(this, @"Perform the following actions on the DMI: " + Environment.NewLine + Environment.NewLine +
            //                    "1. Enter FLU and confirm value in each input field." + Environment.NewLine +
            //                    "2. Press OK on THIS window.");

            //DmiActions.Enable_Fixed_Train_Data_Validation(this, Fixed_Trainset_Captions.FLU);
            //DmiActions.ShowInstruction(this, @"Perform the following actions on the DMI: " + Environment.NewLine + Environment.NewLine +
            //                    "1. Press ‘Yes’ button." + Environment.NewLine +
            //                    "2. Press OK on THIS window.");

            //DmiActions.Complete_Fixed_Train_Data_Entry(this, Fixed_Trainset_Captions.FLU);
            //DmiActions.Display_Train_data_validation_Window(this);
            //DmiActions.ShowInstruction(this, @"Perform the following actions on the DMI: " + Environment.NewLine + Environment.NewLine +
            //                    "1. Press ‘Yes’ button." + Environment.NewLine +
            //                    "2. Confirmed the selected value by pressing the input field." + Environment.NewLine +
            //                    "3. Press OK on THIS window.");

            //DmiActions.Display_TRN_Window(this);
            //DmiActions.ShowInstruction(this, @"Perform the following actions on the DMI: " + Environment.NewLine + Environment.NewLine +
            //                    "1. Enter and confirm Train Running Number." + Environment.NewLine +
            //                    "2. Press OK on THIS window.");

            //DmiActions.Display_Main_Window_with_Start_button_enabled(this);
            //DmiActions.ShowInstruction(this, @"Perform the following actions on the DMI: " + Environment.NewLine + Environment.NewLine +
            //                    "1. Press ‘Start’ button." + Environment.NewLine +
            //                    "2. Press OK on THIS window.");

            //DmiActions.Send_SR_Mode_Ack(this);
            //DmiActions.ShowInstruction(this, @"Perform the following action after pressing OK: " + Environment.NewLine + Environment.NewLine +
            //                    "1. Press DMI Sub Area C1.");
            //DmiExpectedResults.SR_Mode_Ack_pressed_and_hold(this);

            //DmiActions.Send_SR_Mode(this);
            //DmiActions.Send_L1(this);
            //DmiActions.Finished_SoM_Default_Window(this);


            TraceHeader("Test Step 6");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Power off DMI for 10 min and then power on again");
            TraceReport("Expected Result");
            TraceInfo("The default window represents the objects in the language selected by the driver");
            /*
            Test Step 6
            Action: Power off DMI for 10 min and then power on again
            Expected Result: The default window represents the objects in the language selected by the driver
            Test Step Comment: MMI_gen 62;
            */


            TraceHeader("Test Step 7");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Selects ‘Settings’ button  SE 04");
            TraceReport("Expected Result");
            TraceInfo(
                "(1) The Settings window is displayed with all button with text and symbolsSE01  SE02 SE 03 (2) The button with text is displayed 2 lines of text and truncated when the text exceeds the button’s width");
            /*
            Test Step 7
            Action: Selects ‘Settings’ button  SE 04
            Expected Result: (1) The Settings window is displayed with all button with text and symbolsSE01  SE02 SE 03 (2) The button with text is displayed 2 lines of text and truncated when the text exceeds the button’s width
            Test Step Comment: (1) MMI_gen 4368 (partly:symbol);(2) MMI_gen 7506;MMI_gen 7507;
            */


            TraceHeader("Test Step 8");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Press ‘Language’ icon menu");
            TraceReport("Expected Result");
            TraceInfo(
                "The selection language window is displayed with all stored language data for driver to make a new selection");
            /*
            Test Step 8
            Action: Press ‘Language’ icon menu
            Expected Result: The selection language window is displayed with all stored language data for driver to make a new selection
            Test Step Comment: MMI_gen 61 (partly:selected languages);MMI_gen 11893;
            */


            TraceHeader("Test Step 9");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Change to all stored languges and retest with step 3 to 7");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following information,(1)   The text on window, sub windows,  buttons and text messages are presented according to selected language.(2)    Use the log file to confirm that DMI sends out packet [MMI_DRIVER_ACTION (EVC-152)] with the value of variable MMI_M_DRIVER_ACTION refer to sequence below,a)   MMI_M_DRIVER_ACTION = 29 (Selection of Language)");
            /*
            Test Step 9
            Action: Change to all stored languges and retest with step 3 to 7
            Expected Result: Verify the following information,(1)   The text on window, sub windows,  buttons and text messages are presented according to selected language.(2)    Use the log file to confirm that DMI sends out packet [MMI_DRIVER_ACTION (EVC-152)] with the value of variable MMI_M_DRIVER_ACTION refer to sequence below,a)   MMI_M_DRIVER_ACTION = 29 (Selection of Language)
            Test Step Comment: (1) MMI_gen 61 (partly:selected languages);MMI_gen 11892 (partly:selected languages);MMI_gen 4368 (partly:selected language);(2) MMI_gen 11470 (partly: Bit # 29);
            */


            TraceHeader("Test Step 10");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("End of test");
            
            /*
            Test Step 10
            Action: End of test
            Expected Result: 
            */


            return GlobalTestResult;
        }
    }
}
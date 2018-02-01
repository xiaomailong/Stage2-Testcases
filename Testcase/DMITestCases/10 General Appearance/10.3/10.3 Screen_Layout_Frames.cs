using System;
using Testcase.Telegrams.EVCtoDMI;
using Testcase.Telegrams.DMItoEVC;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 10.3 Screen Layout: Frames
    /// TC-ID: 5.3
    /// 
    /// This test case verifies the appearance of all ETCS objects on DMI that related to the control information from ETCS Onboard i.e. display of mode symbols, supervision status or the release speed. 
    /// 
    /// Tested Requirements:
    /// MMI_gen 4222 (partly: frame is displayed with yellow); MMI_gen 6468 (partly: UN mode, supervision status and the release speed are not displayed); MMI_gen 11470 (partly: Bit #2);   
    /// 
    /// Scenario:
    /// 1.SoM_L0_UN
    /// 2.DMI displays in UN mode.
    /// 3.Start driving the train forward with train speed = 5 km/h. Then, stop the train.
    /// 4.Driver acknowledges the train trip.
    /// 
    /// Used files:
    /// N/A
    /// </summary>
    public class TC_ID_5_3_Screen_Layout_Frames : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // System is power on.

            // Call the TestCaseBase PreExecution
            base.PreExecution();
            DmiActions.Start_ATP();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec


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
            TraceInfo("Activate cabin A");
            TraceReport("Expected Result");
            TraceInfo("DMI displays the default window. The Driver ID window is displayed");
            /*
            Test Step 1
            Action: Activate cabin A
            Expected Result: DMI displays the default window. The Driver ID window is displayed
            */

            DmiActions.Activate_Cabin_1(this);
            //DmiExpectedResults.Cabin_A_is_activated(this);

            DmiActions.Set_Driver_ID(this, "1234");
            DmiActions.Send_SB_Mode(this);
            DmiExpectedResults.Driver_ID_window_displayed_in_SB_mode(this);

            TraceHeader("Test Step 2");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Enter the Driver ID. ");
            TraceReport("Expected Result");
            TraceInfo("ATP enters level 0.");
            /*
            Test Step 2
            Action: Enter the Driver ID. 
            Perform brake test and then select Level 0
            Expected Result: ATP enters level 0.
            DMI displays the symbol of Level 0 in sub-area C8
            */

            DmiExpectedResults.Driver_ID_entered(this);

            DmiActions.Request_Brake_Test(this, 1);
            DmiExpectedResults.Brake_Test_Perform_Order(this, true);

            DmiActions.Perform_Brake_Test(this, 2);

            Wait_Realtime(5000);

            DmiActions.Display_Brake_Test_Successful(this, 3);

            DmiActions.Display_Level_Window(this);
            DmiExpectedResults.Level_window_displayed(this);

            DmiActions.Delete_Brake_Test_Successful(this, 3);

            DmiExpectedResults.Level_0_Selected(this);
            DmiActions.Send_L0(this);

            DmiActions.Display_Main_Window_with_Start_button_not_enabled(this);
            DmiExpectedResults.Level_0_displayed(this);

            TraceHeader("Test Step 3");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Select ‘Train data’ button");
            TraceReport("Expected Result");
            TraceInfo("The Train data window is displayed");
            /*
            Test Step 3
            Action: Select ‘Train data’ button
            Expected Result: The Train data window is displayed
            */

            DmiExpectedResults.Train_Data_Button_pressed_and_released(this);

            DmiActions.Display_Fixed_Train_Data_Window(this);
            DmiExpectedResults.Train_data_window_displayed(this);


            TraceHeader("Test Step 4");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Enter and confirm the train data");
            TraceReport("Expected Result");
            TraceInfo("The Train data validation window is displayed");
            /*
            Test Step 4
            Action: Enter and confirm the train data
            Expected Result: The Train data validation window is displayed
            */

            DmiExpectedResults.Fixed_Train_Data_entered(this, Variables.Fixed_Trainset_Captions.FLU);

            DmiActions.Enable_Fixed_Train_Data_Validation(this, Variables.Fixed_Trainset_Captions.FLU);
            DmiExpectedResults.Fixed_Train_Data_validated(this, Variables.Fixed_Trainset_Captions.FLU);

            DmiActions.Complete_Fixed_Train_Data_Entry(this, Variables.Fixed_Trainset_Captions.FLU);

            Wait_Realtime(1000);

            DmiActions.Display_Train_data_validation_Window(this);
            DmiExpectedResults.Train_data_validation_window_displayed(this);

            DmiExpectedResults.Train_Data_validation_completed(this);

            Wait_Realtime(5000);

            TraceHeader("Test Step 5");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Enter and confirm the Train running number");
            TraceReport("Expected Result");
            TraceInfo("DMI displays the Main window");
            /*
            Test Step 5
            Expected Result: DMI displays the Train running window
            */

            DmiActions.Display_TRN_Window(this);
            DmiExpectedResults.TRN_window_displayed(this);

            /*
            Test Step 6
            Action: Enter and confirm the Train running number
            Expected Result: DMI displays the Main window
            */

            DmiExpectedResults.TRN_entered(this);

            DmiActions.Display_Main_Window_with_Start_button_enabled(this);
            DmiExpectedResults.Main_Window_displayed(this, true);

            TraceHeader("Test Step 7");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Press ‘Start’ button and confirm UN mode");
            TraceReport("Expected Result");
            TraceInfo("DMI displays in UN mode, level 0");
            /*
            Test Step 7
            Action: Press ‘Start’ button and confirm UN mode
            Expected Result: DMI displays in UN mode, level 0
            */

            DmiExpectedResults.Start_Button_pressed_and_released(this);

            DmiActions.Display_Default_Window(this);

            DmiActions.Send_UN_Mode_Ack(this);
            DmiExpectedResults.UN_Mode_Ack_requested(this);

            DmiActions.ShowInstruction(this, "Press DMI Sub Area C1");
            DmiExpectedResults.UN_Mode_Ack_pressed_and_released(this);

            DmiActions.Send_UN_Mode(this);
            DmiActions.Send_L0(this);

            DmiExpectedResults.UN_Mode_displayed(this);
            DmiExpectedResults.Driver_symbol_displayed(this, "Level 0", "LE01", "C8", true);

            TraceHeader("Test Step 8");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Drive the train forward and observe all objects on DMI’s screen");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify that when DMI displays in UN mode, the supervision status is not presented to the driver and there is no release speed on DMI");
            /*
            Test Step 8
            Action: Drive the train forward and observe all objects on DMI’s screen
            Expected Result: Verify that when DMI displays in UN mode, the supervision status is not presented to the driver and there is no release speed on DMI
            Test Step Comment: MMI_gen 6468 (partly: UN mode, supervision status and the release speed are not displayed);   
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 5;

            WaitForVerification("Observe all the objects on the DMI screen and check the following:" +
                                Environment.NewLine + Environment.NewLine +
                                "1. Supervision status is not presented to the driver." + Environment.NewLine +
                                "2. DMI does not display a release speed." + Environment.NewLine +
                                "3. UN (Unfitted) mode is displayed.");

            TraceHeader("Test Step 9");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Stop at position 100m. Then, select level 1");
            TraceReport("Expected Result");
            TraceInfo("DMI displays the symbol of level 1 in sub-area C8 instead of level 0.");
            /*
            Test Step 9
            Action: Stop at position 100m. Then, select level 1
            Expected Result: DMI displays the symbol of level 1 in sub-area C8 instead of level 0.
            DMI displays in level 1 with train trip announcement symbol which requires the driver’s action. 
            The train trip symbol is displayed with yellow flashing frame
            Test Step Comment: MMI_gen 4222 (partly: frame is displayed with yellow flashing);
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 0;

            DmiActions.Send_L1(this);
            DmiActions.Send_TR_Mode_Ack(this);

            DmiExpectedResults.TR_Mode_Ack_requested(this);


            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI removes the level 0 symbol from sub-area C8 and displays the level 1 symbol." +
                                Environment.NewLine +
                                "2. DMI displays the train trip announcement symbol requiring driver action." +
                                Environment.NewLine +
                                "3. The train trip symbol is displayed with a yellow flashing frame.");

            TraceHeader("Test Step 10");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Driver acknowledges train trip");
            TraceReport("Expected Result");
            TraceInfo(
                "DMI displays in PT mode.Use the log file to confirm that DMI sends out packet [MMI_DRIVER_ACTION (EVC-152)] with the value of variable MMI_M_DRIVER_ACTION refer to sequence below,a)   MMI_M_DRIVER_ACTION = 2 (Ack of Train Trip)");
            /*
            Test Step 10
            Action: Driver acknowledges train trip
            Expected Result: DMI displays in PT mode.Use the log file to confirm that DMI sends out packet [MMI_DRIVER_ACTION (EVC-152)] with the value of variable MMI_M_DRIVER_ACTION refer to sequence below,a)   MMI_M_DRIVER_ACTION = 2 (Ack of Train Trip)
            Test Step Comment: MMI_gen 11470 (partly: Bit #2);   
            */

            DmiActions.ShowInstruction(this, @"Acknowledge train trip");
            DmiExpectedResults.TR_Mode_Ack_pressed_and_released(this);

            DmiActions.Send_PT_Mode(this);
            DmiExpectedResults.PT_Mode_displayed(this);

            TraceHeader("Test Step 11");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Press ‘Start’ button and confirm SR mode");
            TraceReport("Expected Result");
            TraceInfo("DMI displays in SR mode, level 1");
            /*
            Test Step 11
            Action: Press ‘Start’ button and confirm SR mode
            Expected Result: DMI displays in SR mode, level 1
            */

            DmiActions.Display_Main_Window_with_Start_button_enabled(this);
            DmiActions.ShowInstruction(this, @"Perform the following actions on the DMI: " + Environment.NewLine +
                                             Environment.NewLine +
                                             "1. Press ‘Start’ button." + Environment.NewLine +
                                             "2. Press OK on THIS window.");

            DmiActions.Send_SR_Mode_Ack(this);
            DmiActions.ShowInstruction(this, @"Perform the following action after pressing OK: " + Environment.NewLine +
                                             Environment.NewLine +
                                             "1. Press DMI Sub Area C1.");
            DmiExpectedResults.SR_Mode_Ack_pressed_and_hold(this);

            DmiActions.Send_SR_Mode(this);
            DmiActions.Send_L1(this);
            DmiActions.Finished_SoM_Default_Window(this);
            DmiExpectedResults.SR_Mode_displayed(this);
            DmiExpectedResults.Driver_symbol_displayed(this, "Level 1", "LE03", "C8", true);

            TraceHeader("Test Step 12");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("End of test");
            TraceReport("Expected Result");
            TraceInfo("");
            /*
            Test Step 12
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
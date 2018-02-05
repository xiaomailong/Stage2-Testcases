using System;
using System.Collections.Generic;
using Testcase.Telegrams.EVCtoDMI;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 15.2.1 State 'ST05': General Appearance
    /// TC-ID: 10.2
    /// 
    /// This test case verifies the visualization of ST05 symbol when DMI received packet EVC-8.
    /// 
    /// Tested Requirements:
    /// MMI_gen 12018; MMI_gen 168 (partly: menu window ‘Main’, data-entry window ‘Train data’, data-validation window ‘Train data’, data-view window); MMI_gen 5728 (partly: menu window ‘Main’, data-entry window ‘Train data’, data-validation window ‘Train data’, data-view window, button ‘Close’, ‘Start’, ‘Driver ID’, Train data’, ‘Level’, Train running number’ and ‘Shunting’), Note (partly: navigation buttons and buttons of numeric/dedicated keypad in the ‘Train data’ data entry and validation windows) under MMI_gen 5728; MMI_gen 5731 (partly: window ‘Data view’, closure, to window ‘Default’, on expiration of 45 seconds); MMI_gen 5732 (partly: window ‘Main’, communication lost, closure, to window ‘Default’); MMI_gen 8355 (partly: vertically centered, move to the right every second, no more possible to display, EVC-8); MMI_gen 5464 (partly: state ST05, 4th bullet); MMI_gen 4395 (partly: close button, disabled); MMI_gen 4396 (partly: close button, NA11, NA12); MMI_gen 1088 (partly: Bit #1 to #9 and #28, #32);
    /// 
    /// Scenario:
    /// The ‘Main’ menu window is displayed.Use the test script files to send packets in order to verify state ‘ST05’ in a menu window.Open the ‘Train data’ window and accept all values.Use the test script files to send packets in order to verify state ‘ST05’ in a data entry window.Accept and confirm the train data again.Use the test script files to send packets in order to verify state ‘ST05’ in a data validation window.Validate the train data and complete entering the train running number. The ‘Main’ menu window is displayed.Use the test script files to send packets in order to verify state ‘ST05’ when the communication is lost.Open the data view window.Use the test script files to send packets in order to verify state ‘ST05’ in a data view window.Use the test script files to send packets in order to verify state ‘ST05’ when the state stays longer than 45 seconds. Open Data view window.
    /// 
    /// Used files:
    /// 10_2_a.xml, 10_2_b.xml
    /// </summary>
    public class TC_ID_10_2_State_ST05 : TestcaseBase
    {

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in SB mode, level 1.
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in SB mode, Level 1.");

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 0;
            // Testcase entrypoint
            StartUp();

            DmiActions.Complete_SoM_L1_SB(this);

            MakeTestStepHeader(1, UniqueIdentifier++,
                "Perform the following procedure,Activate Cabin A.Enter Driver ID and perform break test.Select and confirm Level 1",
                "DMI displays Main window.Verify the following information,Use the log file to confirm that DMI receives packet EVC-30 with the value of following bit in variable MMI_Q_REQUEST_ENABLE_64,Bit #0 = 0 (Start)Bit #1 = 1 (Driver ID)Bit #2 = 1 (Train Data)Bit #3 = 1 (Level )Bit #4 = 1 (Train running number)Bit #5 = 1 (Shunting)Bit #7 = 0 (Non-Leading)Bit #8 = 0 (Maintain Shunting)Bit #9 = 0 (EOA)Bit #28 =1 (Start Brake Test)Bit #32 = 1 (System info)And displays the buttons which have the bit value is 1");
            /*
            Test Step 1
            Action: Perform the following procedure,Activate Cabin A.Enter Driver ID and perform break test.Select and confirm Level 1
            Expected Result: DMI displays Main window.Verify the following information,Use the log file to confirm that DMI receives packet EVC-30 with the value of following bit in variable MMI_Q_REQUEST_ENABLE_64,Bit #0 = 0 (Start)Bit #1 = 1 (Driver ID)Bit #2 = 1 (Train Data)Bit #3 = 1 (Level )Bit #4 = 1 (Train running number)Bit #5 = 1 (Shunting)Bit #7 = 0 (Non-Leading)Bit #8 = 0 (Maintain Shunting)Bit #9 = 0 (EOA)Bit #28 =1 (Start Brake Test)Bit #32 = 1 (System info)And displays the buttons which have the bit value is 1
            Test Step Comment: (1) MMI_gen 5728 (partly: ‘Main’ window, menu window, EVC-30, before ST05 state); MMI_gen 1088 (partly: Bit #1 to #9 and #28, #32);
            */
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays Main Window.");

            EVC30_MMIRequestEnable.SendBlank();
            // The spec says the least significant bit #32 is on 
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_LOW = true;
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = (EVC30_MMIRequestEnable.EnabledRequests.DriverID |
                                                                EVC30_MMIRequestEnable.EnabledRequests.TrainData |
                                                                EVC30_MMIRequestEnable.EnabledRequests.Level |
                                                                EVC30_MMIRequestEnable.EnabledRequests
                                                                    .TrainRunningNumber |
                                                                EVC30_MMIRequestEnable.EnabledRequests.Shunting |
                                                                EVC30_MMIRequestEnable.EnabledRequests.StartBrakeTest) &
                                                               ~(EVC30_MMIRequestEnable.EnabledRequests.Start |
                                                                 EVC30_MMIRequestEnable.EnabledRequests.NonLeading |
                                                                 EVC30_MMIRequestEnable.EnabledRequests
                                                                     .MaintainShunting |
                                                                 EVC30_MMIRequestEnable.EnabledRequests.EOA);

            EVC30_MMIRequestEnable.Send();

            WaitForVerification("Check that DMI displays the following buttons in the state specified:" +
                                Environment.NewLine + Environment.NewLine +
                                "1. ‘Start’ disabled." + Environment.NewLine +
                                "2. ‘Driver ID’ enabled." + Environment.NewLine +
                                "3. ‘Train Data’ enabled." + Environment.NewLine +
                                "4. ‘Level’ enabled." + Environment.NewLine +
                                "5. ‘Train Running Number’ enabled." + Environment.NewLine +
                                "6. ‘Shunting’ enabled." + Environment.NewLine +
                                "7. ‘Non Leading’ disabled." + Environment.NewLine +
                                "8. ‘Maintain Shunting’ disabled." + Environment.NewLine +
                                "9. ‘EOA’ disabled." + Environment.NewLine +
                                "10. ‘Start Brake Test’ enabled." + Environment.NewLine +
                                "11. ‘System Info’ enabled.");

            MakeTestStepHeader(2, UniqueIdentifier++,
                "Use the test script file 10_2_a.xml to send EVC-8 withMMI_Q_TEXT_CRITERIA = 3 MMI_Q_TEXT = 716Note: Stopwatch is required for accuracy of test result",
                "Verify the following information,The hourglass symbol ST05 is displayed at window title area.The hourglass symbol ST05 is vertically aligned center of the window title area.All buttons and the ‘Close’ button are disabled.The disabled Close button NA12 is display in area G.The symbol ST05 is move to the right every second.After symbol ST05 is moved to the end of the window title area, the symbol comes back to the first position and keeps moving to the right");
            /*
            Test Step 2
            Action: Use the test script file 10_2_a.xml to send EVC-8 withMMI_Q_TEXT_CRITERIA = 3 MMI_Q_TEXT = 716Note: Stopwatch is required for accuracy of test result
            Expected Result: Verify the following information,The hourglass symbol ST05 is displayed at window title area.The hourglass symbol ST05 is vertically aligned center of the window title area.All buttons and the ‘Close’ button are disabled.The disabled Close button NA12 is display in area G.The symbol ST05 is move to the right every second.After symbol ST05 is moved to the end of the window title area, the symbol comes back to the first position and keeps moving to the right
            Test Step Comment: (1) MMI_gen 12018, MMI_gen 8355 (partly: EVC-8);(2) MMI_gen 8355 (partly: vertically centered)(3) MMI_gen 168 (partly: disabled buttons, ‘Main’ window, menu window); MMI_gen 5464 (partly: state ST05, 4th bullet); MMI_gen 4395 (partly: close button, disabled); (4) MMI_gen 4396 (partly: close, NA12);(5) MMI_gen 8355 (partly: Move to the right every second);(6) MMI_gen 8355 (partly: no more possible to display);
            */
            Send_XML_10_2_a_b(msgType.typea);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the hourglass symbol ST05, vertically aligned, in the centre of the window title area." +
                                Environment.NewLine +
                                "2. All buttons and the ‘Close’ button are disabled." + Environment.NewLine +
                                "3. The ‘Close’ button NA12 is displayed disabled in area G." + Environment.NewLine +
                                "4. The hourglass symbol ST05 moves to the right each second." + Environment.NewLine +
                                "5. When the hourglass symbol ST05 has reached the edge of the window title area it is re-displayed on the lefthand side of the window title area and continues to move to the right.");
            MakeTestStepHeader(3, UniqueIdentifier++,
                "Use the test script file 10_2_b.xml to send EVC-8 withMMI_Q_TEXT_CRITERIA = 4MMI_Q_TEXT = 716",
                "Verify the following information,The hourglass symbol ST05 is removed.The state of all buttons is restored according to the last received EVC-30, see Step 3:Button Start is disabledButton Driver ID is enabledButton Train Data is enabledButton Level is enabledButton Train running number is enabledButton Shunting is enabledButton Non-Leading is disabledButton Maintain Shunting is disabledThe enabled Close button NA11 is display in area G");
            /*
            Test Step 3
            Action: Use the test script file 10_2_b.xml to send EVC-8 withMMI_Q_TEXT_CRITERIA = 4MMI_Q_TEXT = 716
            Expected Result: Verify the following information,The hourglass symbol ST05 is removed.The state of all buttons is restored according to the last received EVC-30, see Step 3:Button Start is disabledButton Driver ID is enabledButton Train Data is enabledButton Level is enabledButton Train running number is enabledButton Shunting is enabledButton Non-Leading is disabledButton Maintain Shunting is disabledThe enabled Close button NA11 is display in area G
            Test Step Comment: (1) MMI_gen 5728 (partly: removal, EVC);(2) MMI_gen 5728 (partly: restore (after ST05), ‘Main’ window, menu window);(3) MMI_gen 4396 (partly: close, NA11);
            */
            Send_XML_10_2_a_b(msgType.typeb);

            WaitForVerification(
                "Check that DMI displays or hides the following objects in the state specified according to the last received EVC-30 message:" +
                Environment.NewLine + Environment.NewLine +
                "1. The hourglass symbol ST05 is removed." + Environment.NewLine +
                "2. ‘Start’ button is disabled." + Environment.NewLine +
                "3. ‘Driver ID’ button is enabled." + Environment.NewLine +
                "4. ‘Train Data’ button is enabled." + Environment.NewLine +
                "5. ‘Level’ button is  enabled." + Environment.NewLine +
                "6. ‘Train Running Number’ button is enabled." + Environment.NewLine +
                "7. ‘Shunting’ button is enabled." + Environment.NewLine +
                "8. ‘Non Leading’ button is disabled." + Environment.NewLine +
                "9. ‘Maintain Shunting’ button is disabled." + Environment.NewLine +
                "10. ‘EOA’ button is disabled." + Environment.NewLine +
                "11. ‘Start Brake Test’ button is enabled." + Environment.NewLine +
                "12. ‘System Info’ button is enabled." + Environment.NewLine +
                "13. ‘EOA’ button is displayed enabled in area G");

            MakeTestStepHeader(4, UniqueIdentifier++, "Press ‘Train data’ button", "DMI displays Train data window");
            /*
            Test Step 4
            Action: Press ‘Train data’ button
            Expected Result: DMI displays Train data window
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press ‘Train data’ button");

            // Send fixed data first
            EVC6_MMICurrentTrainData.MMI_M_BUTTONS = EVC6_MMICurrentTrainData.MMI_M_BUTTONS_CURRENT_TRAIN_DATA
                .BTN_YES_DATA_ENTRY_COMPLETE;
            DmiActions.Send_EVC6_MMICurrentTrainData_FixedDataEntry(this,
                new[] {"FLU", "RLU", "Rescue"},
                1);

            DmiExpectedResults.Train_data_window_displayed(this);

            MakeTestStepHeader(5, UniqueIdentifier++,
                "Confirm the value of input field refer to specified type of Train data window below,Fixed Train data window: Confirm the value of Train typeFlexible Train data window: Confirm the value of Train length",
                "Verify the following information,The ‘Selected’ state (medium-grey background with black text) of the input fields are changed to the ‘Accepted’ state (dark-grey background with white text).The buttons are stated as follows:The keypad is enabled.The ‘Close’ button is enabled.The ‘Yes’ button is enabled");
            /*
            Test Step 5
            Action: Confirm the value of input field refer to specified type of Train data window below,Fixed Train data window: Confirm the value of Train typeFlexible Train data window: Confirm the value of Train length
            Expected Result: Verify the following information,The ‘Selected’ state (medium-grey background with black text) of the input fields are changed to the ‘Accepted’ state (dark-grey background with white text).The buttons are stated as follows:The keypad is enabled.The ‘Close’ button is enabled.The ‘Yes’ button is enabled
            Test Step Comment: (1) MMI_gen 5728 (partly: ‘Train data’ window, data entry window, before ST05 state)(2) MMI_gen 5728 (partly: ‘Train data’ window, data entry window, before ST05 state, state of buttons)
            */
            DmiActions.ShowInstruction(this, @"Accept the value of ‘Train type’ in the Fixed Train data window");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Selected’ state of the input fields (black text on medium-grey background) changes to ‘Accepted’ (white text on dark-grey background).");

            DmiActions.ShowInstruction(this, @"Press the ‘Close button");

            EVC6_MMICurrentTrainData.MMI_M_BUTTONS = EVC6_MMICurrentTrainData.MMI_M_BUTTONS_CURRENT_TRAIN_DATA
                .BTN_YES_DATA_ENTRY_COMPLETE;
            EVC6_MMICurrentTrainData.MMI_L_TRAIN = 100;
            EVC6_MMICurrentTrainData.MMI_M_DATA_ENABLE = Variables.MMI_M_DATA_ENABLE.TrainLength;
            EVC6_MMICurrentTrainData.TrainSetCaptions = new List<string> {"FLU", "RLU", "Rescue"};
            EVC6_MMICurrentTrainData.Send();

            DmiActions.ShowInstruction(this, @"Accept the value of ‘Train length’ in the Flexible Train data window");
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The keypad is enabled." + Environment.NewLine +
                                "2. The ‘Close’ button is enabled." + Environment.NewLine +
                                "3. The ‘Yes’ button is enabled.");

            MakeTestStepHeader(6, UniqueIdentifier++,
                "Use the test script file 10_2_a.xml to send EVC-8 withMMI_Q_TEXT_CRITERIA = 3 MMI_Q_TEXT = 716",
                "Verify the following information,The hourglass symbol ST05 is displayed.The buttons are disabled as follows:The ‘Close’ button is disabled.The ‘Yes’ button is disabled.The keypad is still enabled.All input field is in the ‘Not Selected’ state (dark-grey background with grey text)");
            /*
            Test Step 6
            Action: Use the test script file 10_2_a.xml to send EVC-8 withMMI_Q_TEXT_CRITERIA = 3 MMI_Q_TEXT = 716
            Expected Result: Verify the following information,The hourglass symbol ST05 is displayed.The buttons are disabled as follows:The ‘Close’ button is disabled.The ‘Yes’ button is disabled.The keypad is still enabled.All input field is in the ‘Not Selected’ state (dark-grey background with grey text)
            Test Step Comment: (1) MMI_gen 12018(2) MMI_gen 168 (partly: disabled button, ‘Train data’ window, data entry window);(3) Note under MMI_gen 5728;(4) MMI_gen 168 (partly: deselect input field);
            */
            Send_XML_10_2_a_b(msgType.typea);
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The hourglass symbol ST05 is displayed." + Environment.NewLine +
                                "2. The ‘Close’ button is disabled." + Environment.NewLine +
                                "3. The ‘Yes’ button is disabled." + Environment.NewLine +
                                "4. The keypad is still enabled." + Environment.NewLine +
                                "5. All input fields are in the not ‘Selected’ state (grey text on dark-grey background)");

            MakeTestStepHeader(7, UniqueIdentifier++,
                "Use the test script file 10_2_b.xml to send EVC-8 withMMI_Q_TEXT_CRITERIA = 4MMI_Q_TEXT = 716",
                "Verify the following information,The hourglass symbol ST05 is removed.The button state is resumed as follows:The ‘Close’ button is enabled.The ‘Yes’ button is enabled.The input field is stated as follows:The first input field is in the ‘Selected’ state.The rests are in the ‘Not selected’ state");
            /*
            Test Step 7
            Action: Use the test script file 10_2_b.xml to send EVC-8 withMMI_Q_TEXT_CRITERIA = 4MMI_Q_TEXT = 716
            Expected Result: Verify the following information,The hourglass symbol ST05 is removed.The button state is resumed as follows:The ‘Close’ button is enabled.The ‘Yes’ button is enabled.The input field is stated as follows:The first input field is in the ‘Selected’ state.The rests are in the ‘Not selected’ state
            Test Step Comment: (1) MMI_gen 5728 (partly: removal, EVC);(2) MMI_gen 5728 (partly: restore (after ST05), ‘Train data’ window, data entry window);(3) MMI_gen 5728 (partly: MMI_gen 5211, MMI_gen 4683 (partly: data entry))
            */
            Send_XML_10_2_a_b(msgType.typeb);
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The hourglass symbol ST05 is removed." + Environment.NewLine +
                                "2. The ‘Close’ button is enabled." + Environment.NewLine +
                                "3. The ‘Yes’ button is enabled." + Environment.NewLine +
                                "4. The first input field is in the ‘Selected’ state." + Environment.NewLine +
                                "5. All other input fields are in the not ‘Selected’ state");

            MakeTestStepHeader(8, UniqueIdentifier++,
                "Perform the following procedure,Confirm all value in Train data window.Press ‘Yes’ button.Press ‘Yes’ button (on keypad)",
                "DMI displays Train data validation window with “Yes” in the input field.Verify the following information,The input field is in ‘Selected’ state (medium-grey background with black text). The buttons are stated as follows:The keypad is enabled.The ‘Close’ button is enabled");
            /*
            Test Step 8
            Action: Perform the following procedure,Confirm all value in Train data window.Press ‘Yes’ button.Press ‘Yes’ button (on keypad)
            Expected Result: DMI displays Train data validation window with “Yes” in the input field.Verify the following information,The input field is in ‘Selected’ state (medium-grey background with black text). The buttons are stated as follows:The keypad is enabled.The ‘Close’ button is enabled
            Test Step Comment: (1) MMI_gen 5728 (partly: ‘Train data’ validation window, data validation window, before ST05 state)(2) MMI_gen 5728 (partly: ‘Train data’ validation window, data validation window, before ST05 state, state of buttons)
            */
            DmiActions.ShowInstruction(this,
                @"Accept all values in the Train data window. Press ‘Yes’ button. Press ‘Yes’ button (on keypad)");
            DmiActions.Send_EVC10_MMIEchoedTrainData_FixedDataEntry(this, new[] {"FLU", "RLU", "Rescue"});

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays Train data validation window with ‘Yes’ in the input field." +
                                Environment.NewLine +
                                "2. The input field is in ‘Selected’ state (black text on medium-grey background)." +
                                Environment.NewLine +
                                "3. The keypad is enabled." + Environment.NewLine +
                                "4. The ‘Close’ button is enabled.");

            MakeTestStepHeader(9, UniqueIdentifier++,
                "Use the test script file 10_2_a.xml to send EVC-8 withMMI_Q_TEXT_CRITERIA = 3 MMI_Q_TEXT = 716",
                "Verifies the following points,The hourglass symbol ST05 is displayed.The ‘Close’ button is disabled.The keypad is still enabled.The input field is in the ‘Not Selected’ state (dark-grey background with grey text)");
            /*
            Test Step 9
            Action: Use the test script file 10_2_a.xml to send EVC-8 withMMI_Q_TEXT_CRITERIA = 3 MMI_Q_TEXT = 716
            Expected Result: Verifies the following points,The hourglass symbol ST05 is displayed.The ‘Close’ button is disabled.The keypad is still enabled.The input field is in the ‘Not Selected’ state (dark-grey background with grey text)
            Test Step Comment: (1) MMI_gen 12018;(2) MMI_gen 168 (partly: disabled button, ‘Train data’ validation window, data validation window);(3) Note under MMI_gen 5728; MMI_gen 5719 (partly: exception, state ST05);(4) MMI_gen 168 (partly: deselect input field);
            */
            Send_XML_10_2_a_b(msgType.typea);
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The hourglass symbol ST05 is displayed." + Environment.NewLine +
                                "2. The ‘Close’ button is disabled." + Environment.NewLine +
                                "3. The keypad is still enabled." + Environment.NewLine +
                                "4. The input fields is in the ‘Not Selected’ state (grey text on dark-grey background)");

            MakeTestStepHeader(10, UniqueIdentifier++,
                "Use the test script file 10_2_b.xml to send EVC-8 withMMI_Q_TEXT_CRITERIA = 4MMI_Q_TEXT = 716",
                "Verifies the following points,The hourglass symbol ST05 is removed.The ‘Close’ button is enabled.The input field is in the ‘Selected’ state");
            /*
            Test Step 10
            Action: Use the test script file 10_2_b.xml to send EVC-8 withMMI_Q_TEXT_CRITERIA = 4MMI_Q_TEXT = 716
            Expected Result: Verifies the following points,The hourglass symbol ST05 is removed.The ‘Close’ button is enabled.The input field is in the ‘Selected’ state
            Test Step Comment: (1) MMI_gen 5728 (partly: removal, EVC);(2) MMI_gen 5728 (partly: restore (after ST05), ‘Train data’ validation window, data validation window);(3) MMI_gen 5728 (partly: MMI_gen 5211, MMI_gen 4683 (partly: data validation))
            */
            Send_XML_10_2_a_b(msgType.typeb);
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The hourglass symbol ST05 is removed." + Environment.NewLine +
                                "2. The ‘Close’ button is enabled." + Environment.NewLine +
                                "3. The input field is in the not ‘Selected’ state");

            MakeTestStepHeader(11, UniqueIdentifier++, "Confirm entered data by pressing an input field",
                "DMI displays Train Running Number window");
            /*
            Test Step 11
            Action: Confirm entered data by pressing an input field
            Expected Result: DMI displays Train Running Number window
            */
            DmiActions.ShowInstruction(this, "Accept entered data by pressing an input field");

            EVC16_CurrentTrainNumber.TrainRunningNumber = 1;
            EVC16_CurrentTrainNumber.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays Train Running Number window");

            MakeTestStepHeader(12, UniqueIdentifier++, "Enter and confirm Train running number",
                "DMI displays Main window.Verify the following information,Use the log file to confirm that DMI receives packet EVC-30 with the value of following bit in variable MMI_Q_REQUEST_ENABLE_64,Bit #0 = 1 (Start)Bit #1 = 1 (Driver ID)Bit #2 = 1 (Train Data)Bit #3 = 1 (Level )Bit #4 = 1 (Train running number)Bit #5 = 1 (Shunting)Bit #7 = 0 (Non-Leading)Bit #8 = 0 (Maintain Shunting)And displays the buttons which have the bit value is 1");
            /*
            Test Step 12
            Action: Enter and confirm Train running number
            Expected Result: DMI displays Main window.Verify the following information,Use the log file to confirm that DMI receives packet EVC-30 with the value of following bit in variable MMI_Q_REQUEST_ENABLE_64,Bit #0 = 1 (Start)Bit #1 = 1 (Driver ID)Bit #2 = 1 (Train Data)Bit #3 = 1 (Level )Bit #4 = 1 (Train running number)Bit #5 = 1 (Shunting)Bit #7 = 0 (Non-Leading)Bit #8 = 0 (Maintain Shunting)And displays the buttons which have the bit value is 1
            Test Step Comment: (1) MMI_gen 5728 (partly: default window, EVC-30, before ST05 state)
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Enter and confirm Train running number");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays Main window");

            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Main; // Main window
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = (EVC30_MMIRequestEnable.EnabledRequests.Start |
                                                                EVC30_MMIRequestEnable.EnabledRequests.DriverID |
                                                                EVC30_MMIRequestEnable.EnabledRequests.TrainData |
                                                                EVC30_MMIRequestEnable.EnabledRequests.Level |
                                                                EVC30_MMIRequestEnable.EnabledRequests
                                                                    .TrainRunningNumber |
                                                                EVC30_MMIRequestEnable.EnabledRequests.Shunting) &
                                                               (EVC30_MMIRequestEnable.EnabledRequests.NonLeading |
                                                                EVC30_MMIRequestEnable.EnabledRequests
                                                                    .MaintainShunting);

            EVC30_MMIRequestEnable.Send();

            WaitForVerification("Check that DMI displays the following buttons in the state specified:" +
                                Environment.NewLine + Environment.NewLine +
                                "1. ‘Start’ enabled." + Environment.NewLine +
                                "2. ‘Driver ID’ enabled." + Environment.NewLine +
                                "3. ‘Train Data’ enabled." + Environment.NewLine +
                                "4. ‘Level’ enabled." + Environment.NewLine +
                                "5. ‘Train Running Number’ enabled." + Environment.NewLine +
                                "6. ‘Shunting’ enabled." + Environment.NewLine +
                                "7. ‘Non Leading’  disabled." + Environment.NewLine +
                                "8. ‘Maintain Shunting’ disabled.");

            MakeTestStepHeader(13, UniqueIdentifier++,
                "Use the test script file 10_2_a.xml to send EVC-8 withMMI_Q_TEXT_CRITERIA = 3 MMI_Q_TEXT = 716",
                "DMI displays the  message “ATP Down Alarm” with sound alarm.Verify the following information,The hourglass symbol ST05 is displayed.All buttons and the ‘Close’ button are disabled");
            /*
            Test Step 13
            Action: Use the test script file 10_2_a.xml to send EVC-8 withMMI_Q_TEXT_CRITERIA = 3 MMI_Q_TEXT = 716
            Expected Result: DMI displays the  message “ATP Down Alarm” with sound alarm.Verify the following information,The hourglass symbol ST05 is displayed.All buttons and the ‘Close’ button are disabled
            Test Step Comment: (1) MMI_gen 12018, MMI_gen 5732 (partly: before communication lost, current window ‘Main’);(2) MMI_gen 168 (partly: disabled buttons, ‘Main’ window, menu window);
            */
            Send_XML_10_2_a_b(msgType.typea);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the  message ‘ATP Down Alarm’." + Environment.NewLine +
                                "2. An alarm sound is played." + Environment.NewLine +
                                "3. The hourglass symbol ST05 is displayed." + Environment.NewLine +
                                "4. All buttons and the ‘Close’ button are disabled.");

            MakeTestStepHeader(14, UniqueIdentifier++,
                "Then, simulate the communication loss between ETCS Onboard and DMI and re-establish the communication between ETCS onboard and DMI",
                "Verify the following information,The hourglass symbol ST05 is removed.The ‘Main’ window is closed and DMI returns to the default window.The state of all buttons is restored according to the last received EVC-30, see Step 12:Button Start is enabledButton Driver ID is enabledButton Train Data is enabledButton Level is enabledButton Train running number is enabledButton Shunting is enabledButton Non-Leading is disabledButton Maintain Shunting is disabled");
            /*
            Test Step 14
            Action: Then, simulate the communication loss between ETCS Onboard and DMI and re-establish the communication between ETCS onboard and DMI
            Expected Result: Verify the following information,The hourglass symbol ST05 is removed.The ‘Main’ window is closed and DMI returns to the default window.The state of all buttons is restored according to the last received EVC-30, see Step 12:Button Start is enabledButton Driver ID is enabledButton Train Data is enabledButton Level is enabledButton Train running number is enabledButton Shunting is enabledButton Non-Leading is disabledButton Maintain Shunting is disabled
            Test Step Comment: (1) MMI_gen 5728 (partly: removal);(2) MMI_gen 5732 (partly: close the ‘Main’ window, switch back the default window);(3) MMI_gen 5728 (partly: restore (after ST05), default window);
            */
            DmiActions.Simulate_communication_loss_EVC_DMI(this);
            DmiActions.Re_establish_communication_EVC_DMI(this);

            // Is the previous state restored?
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The hourglass symbol ST05 is removed." + Environment.NewLine +
                                "2. The ‘Main’ window is closed and DMI returns to the default window." +
                                Environment.NewLine +
                                "3. ‘Start’ button is enabled." + Environment.NewLine +
                                "4. ‘Driver ID’ button is enabled." + Environment.NewLine +
                                "5. ‘Train Data’ button is enabled." + Environment.NewLine +
                                "6. ‘Level’ button is enabled." + Environment.NewLine +
                                "7. ‘Train Running Number’ button is enabled." + Environment.NewLine +
                                "8. ‘Shunting’ button is enabled." + Environment.NewLine +
                                "9. ‘Non Leading’ button is disabled." + Environment.NewLine +
                                "10. ‘Maintain Shunting’ button is disabled.");

            MakeTestStepHeader(15, UniqueIdentifier++, "Press ‘Data view’ button",
                "DMI displays Data view window.Verify the following information,The buttons are stated as follows:The navigation buttons are enabled.The ‘Close’ button is enabled");
            /*
            Test Step 15
            Action: Press ‘Data view’ button
            Expected Result: DMI displays Data view window.Verify the following information,The buttons are stated as follows:The navigation buttons are enabled.The ‘Close’ button is enabled
            Test Step Comment: (1) MMI_gen 5728 (partly: data view window, before ST05 state, state of buttons);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press ‘Data view’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays Data view window." + Environment.NewLine +
                                "2. The navigation buttons are enabled." + Environment.NewLine +
                                "3.	The ‘Close’ button is enabled.");

            MakeTestStepHeader(16, UniqueIdentifier++,
                "Use the test script file 10_2_a.xml to send EVC-8 withMMI_Q_TEXT_CRITERIA = 3 MMI_Q_TEXT = 716",
                "Verify the following information,The hourglass symbol ST05 is displayed.The ‘Close’ button is disabled.The navigation buttons are still enabled");
            /*
            Test Step 16
            Action: Use the test script file 10_2_a.xml to send EVC-8 withMMI_Q_TEXT_CRITERIA = 3 MMI_Q_TEXT = 716
            Expected Result: Verify the following information,The hourglass symbol ST05 is displayed.The ‘Close’ button is disabled.The navigation buttons are still enabled
            Test Step Comment: (1) MMI_gen 12018;(2) MMI_gen 168 (partly: disabled button, data view window);(3) Note under MMI_gen 5728;
            */
            Send_XML_10_2_a_b(msgType.typea);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The hourglass symbol ST05 is displayed." + Environment.NewLine +
                                "2. The ‘Close’ button is disabled." + Environment.NewLine +
                                "3. The navigation buttons are still enabled.");

            MakeTestStepHeader(17, UniqueIdentifier++,
                "Use the test script file 10_2_b.xml to send EVC-8 withMMI_Q_TEXT_CRITERIA = 4MMI_Q_TEXT = 716",
                "Verify the following information,The hourglass symbol ST05 is removed.The ‘Close’ button is enabled");
            /*
            Test Step 17
            Action: Use the test script file 10_2_b.xml to send EVC-8 withMMI_Q_TEXT_CRITERIA = 4MMI_Q_TEXT = 716
            Expected Result: Verify the following information,The hourglass symbol ST05 is removed.The ‘Close’ button is enabled
            Test Step Comment: (1) MMI_gen 5728 (partly: removal, EVC);(2) MMI_gen 5728 (partly: restore (after ST05), data view window);
            */
            Send_XML_10_2_a_b(msgType.typeb);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The hourglass symbol ST05 is removed." + Environment.NewLine +
                                "2. The ‘Close’ button is enabled.");

            MakeTestStepHeader(18, UniqueIdentifier++,
                "Use the test script file 10_2_a.xml to send EVC-8 withMMI_Q_TEXT_CRITERIA = 3 MMI_Q_TEXT = 716Then, start the stopwatch (to verify expected result of the next step).Note: Stopwatch is required for accuracy of test result",
                "Verify the following information,The hourglass symbol ST05 is displayed");
            /*
            Test Step 18
            Action: Use the test script file 10_2_a.xml to send EVC-8 withMMI_Q_TEXT_CRITERIA = 3 MMI_Q_TEXT = 716Then, start the stopwatch (to verify expected result of the next step).Note: Stopwatch is required for accuracy of test result
            Expected Result: Verify the following information,The hourglass symbol ST05 is displayed
            Test Step Comment: (1) MMI_gen 5731 (partly: before the expiration of 45 seconds, current window ‘Data view’);
            */
            Send_XML_10_2_a_b(msgType.typea);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The hourglass symbol ST05 is displayed.");

            MakeTestStepHeader(19, UniqueIdentifier++,
                "Wait for 46 second.Note: Stopwatch is required for accuracy of test result",
                "Verify the following information,The hourglass symbol ST05 is removed.The ‘Data view’ window is closed and DMI returns to the default window.The state of all buttons is restored according to the last received EVC-30, see Step 12");
            /*
            Test Step 19
            Action: Wait for 46 second.Note: Stopwatch is required for accuracy of test result
            Expected Result: Verify the following information,The hourglass symbol ST05 is removed.The ‘Data view’ window is closed and DMI returns to the default window.The state of all buttons is restored according to the last received EVC-30, see Step 12
            Test Step Comment: (1) MMI_gen 5728 (partly: removal);(2) MMI_gen 5731 (partly: close the ‘Data view’ window, switch back the default window);(3) MMI_gen 5728 (partly: restore (after ST05), default window);
            */
            this.Wait_Realtime(46000);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The hourglass symbol ST05 is removed." + Environment.NewLine +
                                "2. The ‘Data view’ window is closed and DMI returns to the default window." +
                                Environment.NewLine +
                                "3. ‘Start’ button is enabled." + Environment.NewLine +
                                "4. ‘Driver ID’ button is enabled." + Environment.NewLine +
                                "5. ‘Train Data’ button is enabled." + Environment.NewLine +
                                "6. ‘Level’ button is enabled." + Environment.NewLine +
                                "7. ‘Train Running Number’ button is enabled." + Environment.NewLine +
                                "8. ‘Shunting’ button is enabled." + Environment.NewLine +
                                "9. ‘Non Leading’ button is disabled." + Environment.NewLine +
                                "10. ‘Maintain Shunting’ button is disabled.");

            MakeTestStepHeader(20, UniqueIdentifier++, "End of test", "");

            /*
            Test Step 20
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }

        #region Send_XML_10_2_a_b_DMI_Test_Specification

        enum msgType
        {
            typea,
            typeb
        }

        private void Send_XML_10_2_a_b(msgType type)
        {
            if (type == msgType.typea)
            {
                EVC8_MMIDriverMessage.MMI_Q_TEXT = 716;
                EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
                EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;

                EVC8_MMIDriverMessage.Send();
            }
            else if (type == msgType.typeb)
            {
                EVC8_MMIDriverMessage.MMI_Q_TEXT = 716;
                EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
                EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 4;

                EVC8_MMIDriverMessage.Send();
            }
        }

        #endregion
    }
}
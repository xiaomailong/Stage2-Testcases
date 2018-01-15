using System;
using System.Collections.Generic;
using Testcase.Telegrams.EVCtoDMI;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 10.10 Screen Layout: Button States
    /// TC-ID: 5.10
    /// 
    /// This test case verifies the display of the buttons in disable state.
    /// 
    /// Tested Requirements:
    /// MMI_gen 4377;
    /// 
    /// Scenario:
    /// 1.Perform SoM until train running number is entered.
    /// 2.Send XML script (EVC-30) to disable the buttons on Main window, override window, Special window and Setting window
    /// 3.Verify that the disable buttons shall be shown as a enable button with text label in dark gray. 
    /// 
    /// Used files:
    /// 5_10_a.xml, 5_10.tdg, 5_10.utt
    /// </summary>
    public class TC_ID_5_10_Screen_Layout_Button_States : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:

            // Call the TestCaseBase PreExecution
            base.PreExecution();

            // Test system is powered on    -> Cabin is active: not in spec
            DmiActions.Start_ATP();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in SB mode
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Default window in SB mode, Level 1.");

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint


            /*
            Test Step 1
            Action: Perform SoM until train running number is entered
            Expected Result: DMI displays Main window with enabled ‘Start’ button
            */

            DmiActions.Set_Driver_ID(this, "1234");
            DmiActions.Send_SB_Mode(this);
            DmiActions.Send_L1(this);
            DmiActions.ShowInstruction(this, "Enter and confirm Driver ID");

            DmiActions.Request_Brake_Test(this);
            DmiActions.ShowInstruction(this, "Perform Brake Test");

            DmiActions.Perform_Brake_Test(this, 2);
            Wait_Realtime(5000);
            DmiActions.Display_Brake_Test_Successful(this, 3);

            DmiActions.Display_Level_Window(this);
            DmiActions.Delete_Brake_Test_Successful(this, 3);
            DmiActions.ShowInstruction(this, "Select and enter Level 1");

            DmiActions.Display_Main_Window_with_Start_button_not_enabled(this);
            DmiActions.ShowInstruction(this, @"Press ‘Train data’ button");

            DmiActions.Display_Fixed_Train_Data_Window(this);
            DmiActions.ShowInstruction(this, @"Perform the following actions on the DMI: " + Environment.NewLine +
                                  Environment.NewLine +
                                  "1. Enter FLU and confirm value in each input field." + Environment.NewLine +
                                  "2. Press OK on THIS window.");

            DmiActions.Enable_Fixed_Train_Data_Validation(this, Variables.Fixed_Trainset_Captions.FLU);
            DmiActions.ShowInstruction(this, @"Perform the following actions on the DMI: " + Environment.NewLine +
                                  Environment.NewLine +
                                  "1. Press ‘Yes’ button." + Environment.NewLine +
                                  "2. Press OK on THIS window.");

            DmiActions.Complete_Fixed_Train_Data_Entry(this, Variables.Fixed_Trainset_Captions.FLU);
            DmiActions.Display_Train_data_validation_Window(this);
            DmiActions.ShowInstruction(this, @"Perform the following actions on the DMI: " + Environment.NewLine +
                                  Environment.NewLine +
                                  "1. Press ‘Yes’ button." + Environment.NewLine +
                                  "2. Confirmed the selected value by pressing the input field." + Environment.NewLine +
                                  "3. Press OK on THIS window.");

            DmiActions.Display_Train_data_validation_Window(this);
            DmiActions.ShowInstruction(this, @"Perform the following actions on the DMI: " + Environment.NewLine +
                                  Environment.NewLine +
                                  "1. Press ‘Yes’ button." + Environment.NewLine +
                                  "2. Confirmed the selected value by pressing the input field.");

            DmiActions.Display_TRN_Window(this);
            DmiActions.ShowInstruction(this, "Enter and confirm Train Running Number");

            DmiActions.Display_Main_Window_with_Start_button_enabled(this);


            DmiExpectedResults.Main_Window_displayed(this, true);

            // Steps 2 to 22 are in XML_5_10_a.cs
            /*
            Test Step 2
            Action: Send the test script file 5_10_a.xml to send EVC-30 with,MMI_Q_REQUEST_ENABLE           (#0) = 0           (#1) = 0           (#2) = 0           (#3) = 0           (#4) = 0           (#5) = 0           (#6) = 0           (#7) = 0           (#8) = 0
            Expected Result: The following buttons are shown with a border and its text is coloured Dark-Grey:The ‘Start’ buttonThe ‘Driver ID’ buttonThe ‘Train data’ buttonThe ‘Level’ buttonThe ‘Train running number’ buttonThe ‘Shunting’ buttonThe ‘Non-Leading’ buttonThe ‘Maintain Shunting’ button
            Test Step Comment: MMI_gen 4377 (partly: Main window);
            */
            XML_5_10();

            /*
            Test Step 3
            Action: Press Exit button and select Override menu then run test script 5_10_a.xml to send EVC-30 with,MMI_Q_REQUEST_ENABLE         (#9) = 0
            Expected Result: The following button is shown with a border and its text is coloured Dark-Grey:The ‘EOA’ button
            Test Step Comment: MMI_gen 4377 (partly: Override window);
            */


            /*
            Test Step 4
            Action: Press Exit button and select Special menu then run test script 5_10_a.xml to send EVC-30 with,MMI_Q_REQUEST_ENABLE         (#10) = 0         (#11) = 0         (#12) = 0
            Expected Result: The following buttons are shown with a border and its text is coloured Dark-Grey:The ‘Adhesion’ buttonThe ‘SR speed/distance’ buttonThe ‘Train Integrity’ button
            Test Step Comment: MMI_gen 4377 (partly: Override window);
            */


            /*
            Test Step 5
            Action: Press Exit button and select Setting menu then run test script 5_10_a.xml to send EVC-30 with,MMI_Q_REQUEST_ENABLE         (#13) = 0         (#14) = 0         (#15) = 0         (#16) = 0         (#17) = 0         (#18) = 0         (#25) = 0         (#26) = 0         (#32) = 0
            Expected Result: The following buttons are shown with a border and its text is coloured Dark-Grey:The ‘Language’ buttonThe ‘Volume’ buttonThe ‘Brightness’ buttonThe ‘System version’ buttonThe ‘Set VBC’ buttonThe ‘Remove VBC’ buttonThe ‘Set Clock’ button The ‘System info’ button
            Test Step Comment: MMI_gen 4377 (partly: Setting window);
            */


            /*
            Test Step 6
            Action: Deativate and activate the cabin
            Expected Result: DMI disiplays in SB mode
            */

            /*
            Test Step 7
            Action: Press Setting menu and select Maintenace button and passward. The  run test script 5_10_a.xml to send EVC-30 with,MMI_Q_REQUEST_ENABLE         (#29) = 0         (#30) = 0
            Expected Result: The following buttons are shown with a border and its text is coloured Dark-Grey:The ‘Wheel diameter’ buttonThe ‘Radar’ button
            Test Step Comment: MMI_gen 4377 (partly: Mainteance window);
            */


            /*
            Test Step 8
            Action: Deativate and activate the cabin
            Expected Result: DMI disiplays in SB mode
            */


            /*
            Test Step 9
            Action: Enter Driver ID, skip brake test, select Level 1 then  shunting mode
            Expected Result: DMI disiplays in SH mode
            Test Step Comment: MMI_gen 4377 (partly: Exit Shunting);
            */


            /*
            Test Step 10
            Action: Run test script 5_10_a.xml to send EVC-30 with,MMI_Q_REQUEST_ENABLE         (#6) = 0
            Expected Result: The following button is shown with a border and its text is coloured Dark-Grey:The ‘Exit Shuntingr’ button
            */


            /*
            Test Step 11
            Action: Deativate and activate the cabin
            Expected Result: DMI disiplays in SB mode
            */
            // De-activate and activate cabin


            /*
            Test Step 12
            Action: Perform Start of Mission to SR mode , Level 1
            Expected Result: DMI disiplays in SR mode
            */

            /*
            Test Step 13
            Action: Drive the train forward with 40 km/h then select Setting menu
            Expected Result: The following buttons are shown with a border and its text is coloured Dark-Grey:The ‘Lock screen for cleanning’ buttonThe ‘Brake’ buttonThe ‘National’ buttonThe ‘Maintenance’ button
            Test Step Comment: MMI_gen 4377 (partly: Setting window);
            */

            /*
            Test Step 14
            Action: Pass BG1 with Pkt 12,21 and 27
            Expected Result: DMI disiplays in FS mode
            */


            /*
            Test Step 15
            Action: Pass BG2 with pkt 79 Geographical position then Run test script 5_10_a.xml to send EVC-30 with,MMI_Q_REQUEST_ENABLE         (#23) = 0
            Expected Result: The following button is shown with a border and its text is coloured Dark-Grey:The ‘Geographical        position’ button
            Test Step Comment: MMI_gen 4377 (partly: Geographical position);
            */


            /*
            Test Step 16
            Action: Stop the train
            Expected Result: Train is at standstill
            */
            // Call generic Action Method


            /*
            Test Step 17
            Action: Deactivate and activate cabin
            Expected Result: DMI disiplays in SB mode
            */

            /*
            Test Step 18
            Action: Perform the following procedure,Activate Cabin AEnter Driver ID and perform brake testSelect and confirm Level 2Then run test script 5_10_a.xml to send EVC-30 with,MMI_Q_REQUEST_ENABLE         (#19) = 0         (#20) = 0         (#21) = 0         (#22) = 0
            Expected Result: The following buttons are shown with a border and its text is coloured Dark-Grey:The ‘Contract last window’ buttonThe ‘Use short number’ buttonThe ‘Enter RBC data’ buttonThe ‘Radio Network ID’ button
            Test Step Comment: MMI_gen 4377 (partly: RBC data window);
            */


            /*
            Test Step 19
            Action: Deactivate and activate cabin
            Expected Result: DMI disiplays in SB mode
            */

            /*
            Test Step 20
            Action: Perform the following procedure,Activate Cabin AEnter Driver ID and perform brake testSelect and confirm Level STM PLZBEnter train data entry and comfirm Then run test script 5_10_a.xml to send EVC-30 with,MMI_Q_REQUEST_ENABLE         (#24) = 0
            Expected Result: The following button is shown with a border and its text is coloured Dark-Grey:The ‘End of data entry’ button
            */


            /*
            Test Step 21
            Action: Deactivate and activate cabin cabin
            Expected Result: DMI displays in SB mode
            */


            /*
            Test Step 22
            Action: Perform the following procedure,Activate Cabin AEnter Driver ID and perform brake testAt the Main window, press ‘Close’ buttonPress ‘Settings’ buttonPress ‘Brake’ button.Then run test script 5_10_a.xml to send EVC-30 with,MMI_Q_REQUEST_ENABLE(#28) = 0
            Expected Result: The following button is shown with a border and its text is coloured Dark-Grey:The ‘Brake test’ button
            Test Step Comment: MMI_gen 4377 (partly: Start Brake Test button);
            */


            /*
            Test Step 23
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }

        #region Send_XML_5_10_DMI_Test_Specification

        private void XML_5_10()
        {
            // Step 2
            // Assume flags are initially on and setting them disabled changes the state on the DMI
            EVC30_MMIRequestEnable.SendBlank();

            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = (EVC30_MMIRequestEnable.EnabledRequests.ExitShunting |
                                                                EVC30_MMIRequestEnable.EnabledRequests.EOA |
                                                                EVC30_MMIRequestEnable.EnabledRequests.Adhesion |
                                                                EVC30_MMIRequestEnable.EnabledRequests.SRSpeedDistance |
                                                                EVC30_MMIRequestEnable.EnabledRequests.TrainIntegrity |
                                                                EVC30_MMIRequestEnable.EnabledRequests.Language |
                                                                EVC30_MMIRequestEnable.EnabledRequests.Volume |
                                                                EVC30_MMIRequestEnable.EnabledRequests.Brightness |
                                                                EVC30_MMIRequestEnable.EnabledRequests.SystemVersion |
                                                                EVC30_MMIRequestEnable.EnabledRequests.SetVBC |
                                                                EVC30_MMIRequestEnable.EnabledRequests.RemoveVBC |
                                                                EVC30_MMIRequestEnable.EnabledRequests.ContactLastRBC |
                                                                EVC30_MMIRequestEnable.EnabledRequests.UseShortNumber |
                                                                EVC30_MMIRequestEnable.EnabledRequests.EnterRBCData |
                                                                EVC30_MMIRequestEnable.EnabledRequests.RadioNetworkID |
                                                                EVC30_MMIRequestEnable.EnabledRequests
                                                                    .GeographicalPosition |
                                                                EVC30_MMIRequestEnable.EnabledRequests
                                                                    .EndOfDataEntryNTC |
                                                                EVC30_MMIRequestEnable.EnabledRequests
                                                                    .SetLocalTimeDateAndOffset |
                                                                EVC30_MMIRequestEnable.EnabledRequests.SetLocalOffset |
                                                                EVC30_MMIRequestEnable.EnabledRequests.Reserved |
                                                                EVC30_MMIRequestEnable.EnabledRequests.StartBrakeTest |
                                                                EVC30_MMIRequestEnable.EnabledRequests
                                                                    .EnableWheelDiameter |
                                                                EVC30_MMIRequestEnable.EnabledRequests.EnableDoppler |
                                                                EVC30_MMIRequestEnable.EnabledRequests
                                                                    .EnableBrakePercentage) &
                                                               ~(EVC30_MMIRequestEnable.EnabledRequests.Start |
                                                                 EVC30_MMIRequestEnable.EnabledRequests.DriverID |
                                                                 EVC30_MMIRequestEnable.EnabledRequests.TrainData |
                                                                 EVC30_MMIRequestEnable.EnabledRequests.Level |
                                                                 EVC30_MMIRequestEnable.EnabledRequests
                                                                     .TrainRunningNumber |
                                                                 EVC30_MMIRequestEnable.EnabledRequests.Shunting |
                                                                 EVC30_MMIRequestEnable.EnabledRequests.NonLeading |
                                                                 EVC30_MMIRequestEnable.EnabledRequests
                                                                     .MaintainShunting);
            EVC30_MMIRequestEnable.Send();

            WaitForVerification("Check that the following buttons are displayed with a border with Dark-Grey text:" +
                                Environment.NewLine + Environment.NewLine +
                                @"1. The ‘Start’ button." + Environment.NewLine +
                                @"2. The ‘Driver ID’ button." + Environment.NewLine +
                                @"3. The ‘Train data’ button." + Environment.NewLine +
                                @"4. The ‘Level’ button." + Environment.NewLine +
                                @"5. The ‘Train running number’ button." + Environment.NewLine +
                                @"6. The ‘Shunting’ button." + Environment.NewLine +
                                @"7. The ‘Non - Leading’ button." + Environment.NewLine +
                                @"8. The ‘Maintain Shunting’ button");


            // Step 3
            DMITestCases.DmiActions.ShowInstruction(this, "Press ‘Exit’ button then select Override menu");
            DmiActions.Display_Override_Window(this);
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = (EVC30_MMIRequestEnable.EnabledRequests.Start |
                                                                EVC30_MMIRequestEnable.EnabledRequests.DriverID |
                                                                EVC30_MMIRequestEnable.EnabledRequests.TrainData |
                                                                EVC30_MMIRequestEnable.EnabledRequests.Level |
                                                                EVC30_MMIRequestEnable.EnabledRequests
                                                                    .TrainRunningNumber |
                                                                EVC30_MMIRequestEnable.EnabledRequests.Shunting |
                                                                EVC30_MMIRequestEnable.EnabledRequests.ExitShunting |
                                                                EVC30_MMIRequestEnable.EnabledRequests.NonLeading |
                                                                EVC30_MMIRequestEnable.EnabledRequests
                                                                    .MaintainShunting |
                                                                EVC30_MMIRequestEnable.EnabledRequests.Adhesion |
                                                                EVC30_MMIRequestEnable.EnabledRequests.SRSpeedDistance |
                                                                EVC30_MMIRequestEnable.EnabledRequests.TrainIntegrity |
                                                                EVC30_MMIRequestEnable.EnabledRequests.Language |
                                                                EVC30_MMIRequestEnable.EnabledRequests.Volume |
                                                                EVC30_MMIRequestEnable.EnabledRequests.Brightness |
                                                                EVC30_MMIRequestEnable.EnabledRequests.SystemVersion |
                                                                EVC30_MMIRequestEnable.EnabledRequests.SetVBC |
                                                                EVC30_MMIRequestEnable.EnabledRequests.RemoveVBC |
                                                                EVC30_MMIRequestEnable.EnabledRequests.ContactLastRBC |
                                                                EVC30_MMIRequestEnable.EnabledRequests.UseShortNumber |
                                                                EVC30_MMIRequestEnable.EnabledRequests.EnterRBCData |
                                                                EVC30_MMIRequestEnable.EnabledRequests.RadioNetworkID |
                                                                EVC30_MMIRequestEnable.EnabledRequests
                                                                    .GeographicalPosition |
                                                                EVC30_MMIRequestEnable.EnabledRequests
                                                                    .EndOfDataEntryNTC |
                                                                EVC30_MMIRequestEnable.EnabledRequests
                                                                    .SetLocalTimeDateAndOffset |
                                                                EVC30_MMIRequestEnable.EnabledRequests.SetLocalOffset |
                                                                EVC30_MMIRequestEnable.EnabledRequests.Reserved |
                                                                EVC30_MMIRequestEnable.EnabledRequests.StartBrakeTest |
                                                                EVC30_MMIRequestEnable.EnabledRequests
                                                                    .EnableWheelDiameter |
                                                                EVC30_MMIRequestEnable.EnabledRequests.EnableDoppler |
                                                                EVC30_MMIRequestEnable.EnabledRequests
                                                                    .EnableBrakePercentage) &
                                                               ~EVC30_MMIRequestEnable.EnabledRequests.EOA;
            EVC30_MMIRequestEnable.Send();

            WaitForVerification("Check that the following button is displayed with a border with Dark-Grey text:" +
                                Environment.NewLine + Environment.NewLine +
                                @"1. The ‘EOA’ button.");


            // Step 4
            DMITestCases.DmiActions.ShowInstruction(this, "Press the ‘Exit’ button and select the Special menu");
            DmiActions.Open_the_Special_window(this);
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = (EVC30_MMIRequestEnable.EnabledRequests.Start |
                                                                EVC30_MMIRequestEnable.EnabledRequests.DriverID |
                                                                EVC30_MMIRequestEnable.EnabledRequests.TrainData |
                                                                EVC30_MMIRequestEnable.EnabledRequests.Level |
                                                                EVC30_MMIRequestEnable.EnabledRequests
                                                                    .TrainRunningNumber |
                                                                EVC30_MMIRequestEnable.EnabledRequests.Shunting |
                                                                EVC30_MMIRequestEnable.EnabledRequests.ExitShunting |
                                                                EVC30_MMIRequestEnable.EnabledRequests.NonLeading |
                                                                EVC30_MMIRequestEnable.EnabledRequests
                                                                    .MaintainShunting |
                                                                EVC30_MMIRequestEnable.EnabledRequests.EOA |
                                                                EVC30_MMIRequestEnable.EnabledRequests.Language |
                                                                EVC30_MMIRequestEnable.EnabledRequests.Volume |
                                                                EVC30_MMIRequestEnable.EnabledRequests.Brightness |
                                                                EVC30_MMIRequestEnable.EnabledRequests.SystemVersion |
                                                                EVC30_MMIRequestEnable.EnabledRequests.SetVBC |
                                                                EVC30_MMIRequestEnable.EnabledRequests.RemoveVBC |
                                                                EVC30_MMIRequestEnable.EnabledRequests.ContactLastRBC |
                                                                EVC30_MMIRequestEnable.EnabledRequests.UseShortNumber |
                                                                EVC30_MMIRequestEnable.EnabledRequests.EnterRBCData |
                                                                EVC30_MMIRequestEnable.EnabledRequests.RadioNetworkID |
                                                                EVC30_MMIRequestEnable.EnabledRequests
                                                                    .GeographicalPosition |
                                                                EVC30_MMIRequestEnable.EnabledRequests
                                                                    .EndOfDataEntryNTC |
                                                                EVC30_MMIRequestEnable.EnabledRequests
                                                                    .SetLocalTimeDateAndOffset |
                                                                EVC30_MMIRequestEnable.EnabledRequests.SetLocalOffset |
                                                                EVC30_MMIRequestEnable.EnabledRequests.Reserved |
                                                                EVC30_MMIRequestEnable.EnabledRequests.StartBrakeTest |
                                                                EVC30_MMIRequestEnable.EnabledRequests
                                                                    .EnableWheelDiameter |
                                                                EVC30_MMIRequestEnable.EnabledRequests.EnableDoppler |
                                                                EVC30_MMIRequestEnable.EnabledRequests
                                                                    .EnableBrakePercentage) &
                                                               ~(EVC30_MMIRequestEnable.EnabledRequests.Adhesion |
                                                                 EVC30_MMIRequestEnable.EnabledRequests
                                                                     .SRSpeedDistance |
                                                                 EVC30_MMIRequestEnable.EnabledRequests.TrainIntegrity);
            EVC30_MMIRequestEnable.Send();

            WaitForVerification("Check that the following buttons are displayed with a border with Dark-Grey text:" +
                                Environment.NewLine + Environment.NewLine +
                                @"1. The ‘Adhesion’ button." + Environment.NewLine +
                                @"2. The ‘SR speed/distance’ button." + Environment.NewLine +
                                @"3. The ‘Train Integrity’ button.");

            // Step 5
            DMITestCases.DmiActions.ShowInstruction(this, "Press the ‘Exit’ button and select the Settings menu");
            DmiActions.Open_the_Settings_window(this);
            // The spec indicates 9 bits to set but only tests 8 buttons: assume the first 6 are correct and Set Clock (#25) is SetLocalTimeDateAndOffset
            // Bit #32 would be in the next word so if MMI_Q_REQUEST_ENABLE_LOW is available and the only button to be set it could be a bool as suggested
            // but being tested
            EVC30_MMIRequestEnable.SendBlank();
            // Other signals need setting 
            // from VSIS looks like the other buttons are controlled by MMI_Q_REQUEST_ENABLE_LOW
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_LOW = false; // System info disabled

            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = (EVC30_MMIRequestEnable.EnabledRequests.Start |
                                                                EVC30_MMIRequestEnable.EnabledRequests.DriverID |
                                                                EVC30_MMIRequestEnable.EnabledRequests.TrainData |
                                                                EVC30_MMIRequestEnable.EnabledRequests.Level |
                                                                EVC30_MMIRequestEnable.EnabledRequests
                                                                    .TrainRunningNumber |
                                                                EVC30_MMIRequestEnable.EnabledRequests.Shunting |
                                                                EVC30_MMIRequestEnable.EnabledRequests.ExitShunting |
                                                                EVC30_MMIRequestEnable.EnabledRequests.NonLeading |
                                                                EVC30_MMIRequestEnable.EnabledRequests
                                                                    .MaintainShunting |
                                                                EVC30_MMIRequestEnable.EnabledRequests.EOA |
                                                                EVC30_MMIRequestEnable.EnabledRequests.Adhesion |
                                                                EVC30_MMIRequestEnable.EnabledRequests.SRSpeedDistance |
                                                                EVC30_MMIRequestEnable.EnabledRequests.TrainIntegrity |
                                                                EVC30_MMIRequestEnable.EnabledRequests.ContactLastRBC |
                                                                EVC30_MMIRequestEnable.EnabledRequests.UseShortNumber |
                                                                EVC30_MMIRequestEnable.EnabledRequests.EnterRBCData |
                                                                EVC30_MMIRequestEnable.EnabledRequests.RadioNetworkID |
                                                                EVC30_MMIRequestEnable.EnabledRequests
                                                                    .GeographicalPosition |
                                                                EVC30_MMIRequestEnable.EnabledRequests
                                                                    .EndOfDataEntryNTC |
                                                                EVC30_MMIRequestEnable.EnabledRequests.Reserved |
                                                                EVC30_MMIRequestEnable.EnabledRequests.StartBrakeTest |
                                                                EVC30_MMIRequestEnable.EnabledRequests
                                                                    .EnableWheelDiameter |
                                                                EVC30_MMIRequestEnable.EnabledRequests.EnableDoppler |
                                                                EVC30_MMIRequestEnable.EnabledRequests
                                                                    .EnableBrakePercentage) &
                                                               ~(EVC30_MMIRequestEnable.EnabledRequests.Language |
                                                                 EVC30_MMIRequestEnable.EnabledRequests.Volume |
                                                                 EVC30_MMIRequestEnable.EnabledRequests.Brightness |
                                                                 EVC30_MMIRequestEnable.EnabledRequests.SystemVersion |
                                                                 EVC30_MMIRequestEnable.EnabledRequests.SetVBC |
                                                                 EVC30_MMIRequestEnable.EnabledRequests.RemoveVBC |
                                                                 EVC30_MMIRequestEnable.EnabledRequests.SetLocalOffset |
                                                                 EVC30_MMIRequestEnable.EnabledRequests
                                                                     .SetLocalTimeDateAndOffset);

            EVC30_MMIRequestEnable.Send();

            WaitForVerification("Check that the following buttons are displayed with a border with Dark-Grey text:" +
                                Environment.NewLine + Environment.NewLine +
                                @"1. The ‘Language’ button." + Environment.NewLine +
                                @"2. The ‘Volume’ button." + Environment.NewLine +
                                @"3. The ‘Brightness’ button." + Environment.NewLine +
                                @"4. The ‘System version’ button." + Environment.NewLine +
                                @"5. The ‘Set VBC’ button." + Environment.NewLine +
                                @"6. The ‘Remove VBC’ button." + Environment.NewLine +
                                @"7. The ‘Set Clock’ button." + Environment.NewLine +
                                @"8. The ‘System info’ button");

            // Step 6
            // De-activate and activate cabin
            // More to do??           

            DmiActions.Deactivate_Cabin(this);
            Wait_Realtime(5000);
            DmiActions.Activate_Cabin_1(this);

            DmiActions.Set_Driver_ID(this, "1234");

            DmiActions.Send_SB_Mode(this);
            DmiExpectedResults.SB_Mode_displayed(this);

            // Step 7
            DMITestCases.DmiActions.ShowInstruction(this,"Press Setting menu and select Maintenance button and password");
            DmiActions.Open_the_Settings_window(this);
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = (EVC30_MMIRequestEnable.EnabledRequests.Start |
                                                                EVC30_MMIRequestEnable.EnabledRequests.DriverID |
                                                                EVC30_MMIRequestEnable.EnabledRequests.TrainData |
                                                                EVC30_MMIRequestEnable.EnabledRequests.Level |
                                                                EVC30_MMIRequestEnable.EnabledRequests
                                                                    .TrainRunningNumber |
                                                                EVC30_MMIRequestEnable.EnabledRequests.Shunting |
                                                                EVC30_MMIRequestEnable.EnabledRequests.ExitShunting |
                                                                EVC30_MMIRequestEnable.EnabledRequests.NonLeading |
                                                                EVC30_MMIRequestEnable.EnabledRequests
                                                                    .MaintainShunting |
                                                                EVC30_MMIRequestEnable.EnabledRequests.EOA |
                                                                EVC30_MMIRequestEnable.EnabledRequests.Adhesion |
                                                                EVC30_MMIRequestEnable.EnabledRequests.SRSpeedDistance |
                                                                EVC30_MMIRequestEnable.EnabledRequests.TrainIntegrity |
                                                                EVC30_MMIRequestEnable.EnabledRequests.Language |
                                                                EVC30_MMIRequestEnable.EnabledRequests.Volume |
                                                                EVC30_MMIRequestEnable.EnabledRequests.Brightness |
                                                                EVC30_MMIRequestEnable.EnabledRequests.SystemVersion |
                                                                EVC30_MMIRequestEnable.EnabledRequests.SetVBC |
                                                                EVC30_MMIRequestEnable.EnabledRequests.RemoveVBC |
                                                                EVC30_MMIRequestEnable.EnabledRequests.ContactLastRBC |
                                                                EVC30_MMIRequestEnable.EnabledRequests.UseShortNumber |
                                                                EVC30_MMIRequestEnable.EnabledRequests.EnterRBCData |
                                                                EVC30_MMIRequestEnable.EnabledRequests.RadioNetworkID |
                                                                EVC30_MMIRequestEnable.EnabledRequests
                                                                    .GeographicalPosition |
                                                                EVC30_MMIRequestEnable.EnabledRequests
                                                                    .EndOfDataEntryNTC |
                                                                EVC30_MMIRequestEnable.EnabledRequests
                                                                    .SetLocalTimeDateAndOffset |
                                                                EVC30_MMIRequestEnable.EnabledRequests.SetLocalOffset |
                                                                EVC30_MMIRequestEnable.EnabledRequests.Reserved |
                                                                EVC30_MMIRequestEnable.EnabledRequests.StartBrakeTest) |
                                                               EVC30_MMIRequestEnable.EnabledRequests
                                                                   .EnableBrakePercentage &
                                                               ~(EVC30_MMIRequestEnable.EnabledRequests
                                                                     .EnableWheelDiameter |
                                                                 EVC30_MMIRequestEnable.EnabledRequests.EnableDoppler);
            EVC30_MMIRequestEnable.Send();

            WaitForVerification("Check that the following buttons are displayed with a border with Dark-Grey text:" +
                                Environment.NewLine + Environment.NewLine +
                                @"1. The ‘Wheel diameter’ button." + Environment.NewLine +
                                @"2. The ‘Radar’ button."); // aka Enable Doppler

            // Step 8
            // De-activate and activate cabin
            // More to do????
            DmiActions.Deactivate_Cabin(this);
            Wait_Realtime(5000);
            DmiActions.Activate_Cabin_1(this);

            DmiActions.Set_Driver_ID(this, "1234");

            DmiActions.Send_SB_Mode(this);
            DmiExpectedResults.SB_Mode_displayed(this);

            // Step 9
            // Test says this: but level entry and shunting is tested elsewhere
            
            DmiActions.ShowInstruction(this, "Enter Driver ID.");

            DmiActions.Request_Brake_Test(this);
            DmiActions.ShowInstruction(this, "Skip brake test. ");

            DmiActions.Display_Level_Window(this);
            DmiActions.ShowInstruction(this, "Select Level 1. ");

            DmiActions.Display_Main_Window_with_Start_button_not_enabled(this);
            DmiActions.ShowInstruction(this, $"Press and hold ‘Shunting’ button for up to 2s then release the ‘Shunting’ button");

            DmiActions.Send_SH_Mode(this);
            DmiActions.Send_L1(this);

            DMITestCases.DmiExpectedResults.SH_Mode_displayed(this);

            // Step 10
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = (EVC30_MMIRequestEnable.EnabledRequests.Start |
                                                                EVC30_MMIRequestEnable.EnabledRequests.DriverID |
                                                                EVC30_MMIRequestEnable.EnabledRequests.TrainData |
                                                                EVC30_MMIRequestEnable.EnabledRequests.Level |
                                                                EVC30_MMIRequestEnable.EnabledRequests
                                                                    .TrainRunningNumber |
                                                                EVC30_MMIRequestEnable.EnabledRequests.Shunting |
                                                                EVC30_MMIRequestEnable.EnabledRequests.NonLeading |
                                                                EVC30_MMIRequestEnable.EnabledRequests
                                                                    .MaintainShunting |
                                                                EVC30_MMIRequestEnable.EnabledRequests.EOA |
                                                                EVC30_MMIRequestEnable.EnabledRequests.Adhesion |
                                                                EVC30_MMIRequestEnable.EnabledRequests.SRSpeedDistance |
                                                                EVC30_MMIRequestEnable.EnabledRequests.TrainIntegrity |
                                                                EVC30_MMIRequestEnable.EnabledRequests.Language |
                                                                EVC30_MMIRequestEnable.EnabledRequests.Volume |
                                                                EVC30_MMIRequestEnable.EnabledRequests.Brightness |
                                                                EVC30_MMIRequestEnable.EnabledRequests.SystemVersion |
                                                                EVC30_MMIRequestEnable.EnabledRequests.SetVBC |
                                                                EVC30_MMIRequestEnable.EnabledRequests.RemoveVBC |
                                                                EVC30_MMIRequestEnable.EnabledRequests.ContactLastRBC |
                                                                EVC30_MMIRequestEnable.EnabledRequests.UseShortNumber |
                                                                EVC30_MMIRequestEnable.EnabledRequests.EnterRBCData |
                                                                EVC30_MMIRequestEnable.EnabledRequests.RadioNetworkID |
                                                                EVC30_MMIRequestEnable.EnabledRequests
                                                                    .GeographicalPosition |
                                                                EVC30_MMIRequestEnable.EnabledRequests
                                                                    .EndOfDataEntryNTC |
                                                                EVC30_MMIRequestEnable.EnabledRequests
                                                                    .SetLocalTimeDateAndOffset |
                                                                EVC30_MMIRequestEnable.EnabledRequests.SetLocalOffset |
                                                                EVC30_MMIRequestEnable.EnabledRequests.Reserved |
                                                                EVC30_MMIRequestEnable.EnabledRequests.StartBrakeTest |
                                                                EVC30_MMIRequestEnable.EnabledRequests
                                                                    .EnableWheelDiameter |
                                                                EVC30_MMIRequestEnable.EnabledRequests.EnableDoppler |
                                                                EVC30_MMIRequestEnable.EnabledRequests
                                                                    .EnableBrakePercentage) &
                                                               ~EVC30_MMIRequestEnable.EnabledRequests.ExitShunting;
            EVC30_MMIRequestEnable.Send();

            WaitForVerification("Check that the following button is displayed with a border with Dark-Grey text:" +
                                Environment.NewLine + Environment.NewLine +
                                @"1. The ‘Exit Shunting’ button.");

            // Step 11
            // More to do????
            DmiActions.Deactivate_Cabin(this);
            Wait_Realtime(5000);
            DmiActions.Activate_Cabin_1(this);


            // Step 12

            DMITestCases.DmiActions.Perform_SoM_in_SR_mode_Level_1(this);
            DmiExpectedResults.SR_Mode_displayed(this);


            // Step 13
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 40;

            DmiActions.ShowInstruction(this, "Select Settings menu");
            DmiActions.Open_the_Settings_window(this);

            WaitForVerification("Check that the following buttons are displayed with a border with Dark-Grey text:" +
                                Environment.NewLine + Environment.NewLine +
                                @"1. The ‘Lock screen for cleaning’ button." + Environment.NewLine +
                                @"2. The ‘Brake’ button." + Environment.NewLine +
                                @"3. The ‘National’ button." + Environment.NewLine +
                                @"4. The ‘Maintenance’ button.");

            // Step 14

            DmiActions.Send_FS_Mode(this);
            DmiExpectedResults.FS_mode_displayed(this);


            // Step 15
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.Start |
                                                               EVC30_MMIRequestEnable.EnabledRequests.DriverID |
                                                               EVC30_MMIRequestEnable.EnabledRequests.TrainData |
                                                               EVC30_MMIRequestEnable.EnabledRequests.Level |
                                                               EVC30_MMIRequestEnable.EnabledRequests
                                                                   .TrainRunningNumber |
                                                               EVC30_MMIRequestEnable.EnabledRequests.Shunting |
                                                               EVC30_MMIRequestEnable.EnabledRequests.ExitShunting |
                                                               EVC30_MMIRequestEnable.EnabledRequests.NonLeading |
                                                               EVC30_MMIRequestEnable.EnabledRequests.MaintainShunting |
                                                               EVC30_MMIRequestEnable.EnabledRequests.EOA |
                                                               EVC30_MMIRequestEnable.EnabledRequests.Adhesion |
                                                               EVC30_MMIRequestEnable.EnabledRequests.SRSpeedDistance |
                                                               EVC30_MMIRequestEnable.EnabledRequests.TrainIntegrity |
                                                               EVC30_MMIRequestEnable.EnabledRequests.Language |
                                                               EVC30_MMIRequestEnable.EnabledRequests.Volume |
                                                               EVC30_MMIRequestEnable.EnabledRequests.Brightness |
                                                               EVC30_MMIRequestEnable.EnabledRequests.SystemVersion |
                                                               EVC30_MMIRequestEnable.EnabledRequests.SetVBC |
                                                               EVC30_MMIRequestEnable.EnabledRequests.RemoveVBC |
                                                               EVC30_MMIRequestEnable.EnabledRequests.ContactLastRBC |
                                                               EVC30_MMIRequestEnable.EnabledRequests.UseShortNumber |
                                                               EVC30_MMIRequestEnable.EnabledRequests.EnterRBCData |
                                                               EVC30_MMIRequestEnable.EnabledRequests.RadioNetworkID |
                                                               EVC30_MMIRequestEnable.EnabledRequests
                                                                   .EndOfDataEntryNTC |
                                                               EVC30_MMIRequestEnable.EnabledRequests
                                                                   .SetLocalTimeDateAndOffset |
                                                               EVC30_MMIRequestEnable.EnabledRequests.SetLocalOffset |
                                                               EVC30_MMIRequestEnable.EnabledRequests.Reserved |
                                                               EVC30_MMIRequestEnable.EnabledRequests.StartBrakeTest |
                                                               EVC30_MMIRequestEnable.EnabledRequests
                                                                   .EnableWheelDiameter |
                                                               EVC30_MMIRequestEnable.EnabledRequests.EnableDoppler |
                                                               EVC30_MMIRequestEnable.EnabledRequests
                                                                   .EnableBrakePercentage;
            EVC30_MMIRequestEnable.Send();

            WaitForVerification("Check that the following button is displayed with a border with Dark-Grey text:" +
                                Environment.NewLine + Environment.NewLine +
                                @"1. The ‘Geographical position’ button.");


            // Step 16
            // Stop the train

            DmiActions.Stop_the_train(this);

            // Train is at a standstill
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                      "1. DMI displays speed = 0 km/h.");


            // Step 17

            DmiActions.Deactivate_Cabin(this);
            Wait_Realtime(5000);
            DmiActions.Activate_Cabin_1(this);

            DmiActions.Set_Driver_ID(this, "1234");

            DmiActions.Send_SB_Mode(this);
            DmiExpectedResults.SB_Mode_displayed(this);

            // Step 18
            DmiActions.ShowInstruction(this, @"Perform the following actions on the DMI: " + Environment.NewLine +
                                  Environment.NewLine +
                                  "1. Enter and confirm Driver ID." + Environment.NewLine +
                                  "2. Press OK on THIS window.");

            DmiActions.Request_Brake_Test(this);
            DmiActions.ShowInstruction(this, @"Perform the following actions on the DMI: " + Environment.NewLine +
                                  Environment.NewLine +
                                  "1. Perform Brake Test" + Environment.NewLine +
                                  "2. Press OK on THIS window.");
            DmiActions.Perform_Brake_Test(this, 2);
            Wait_Realtime(5000);
            DmiActions.Display_Brake_Test_Successful(this, 3);

            DmiActions.Display_Level_Window(this);
            DmiActions.Delete_Brake_Test_Successful(this, 3);
            DmiActions.ShowInstruction(this, @"Perform the following actions on the DMI: " + Environment.NewLine +
                                  Environment.NewLine +
                                  "1. Select and enter Level 2" + Environment.NewLine +
                                  "2. Press OK on THIS window.");

            DmiActions.Display_RBC_Contact_Window(this);

            // force Level 2
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Level = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_LEVEL.L2;

            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.Start |
                                                               EVC30_MMIRequestEnable.EnabledRequests.DriverID |
                                                               EVC30_MMIRequestEnable.EnabledRequests.TrainData |
                                                               EVC30_MMIRequestEnable.EnabledRequests.Level |
                                                               EVC30_MMIRequestEnable.EnabledRequests
                                                                   .TrainRunningNumber |
                                                               EVC30_MMIRequestEnable.EnabledRequests.Shunting |
                                                               EVC30_MMIRequestEnable.EnabledRequests.ExitShunting |
                                                               EVC30_MMIRequestEnable.EnabledRequests.NonLeading |
                                                               EVC30_MMIRequestEnable.EnabledRequests.MaintainShunting |
                                                               EVC30_MMIRequestEnable.EnabledRequests.EOA |
                                                               EVC30_MMIRequestEnable.EnabledRequests.Adhesion |
                                                               EVC30_MMIRequestEnable.EnabledRequests.SRSpeedDistance |
                                                               EVC30_MMIRequestEnable.EnabledRequests.TrainIntegrity |
                                                               EVC30_MMIRequestEnable.EnabledRequests.Language |
                                                               EVC30_MMIRequestEnable.EnabledRequests.Volume |
                                                               EVC30_MMIRequestEnable.EnabledRequests.Brightness |
                                                               EVC30_MMIRequestEnable.EnabledRequests.SystemVersion |
                                                               EVC30_MMIRequestEnable.EnabledRequests.SetVBC |
                                                               EVC30_MMIRequestEnable.EnabledRequests.RemoveVBC |
                                                               EVC30_MMIRequestEnable.EnabledRequests
                                                                   .GeographicalPosition |
                                                               EVC30_MMIRequestEnable.EnabledRequests
                                                                   .EndOfDataEntryNTC |
                                                               EVC30_MMIRequestEnable.EnabledRequests
                                                                   .SetLocalTimeDateAndOffset |
                                                               EVC30_MMIRequestEnable.EnabledRequests.SetLocalOffset |
                                                               EVC30_MMIRequestEnable.EnabledRequests.Reserved |
                                                               EVC30_MMIRequestEnable.EnabledRequests.StartBrakeTest |
                                                               EVC30_MMIRequestEnable.EnabledRequests
                                                                   .EnableWheelDiameter |
                                                               EVC30_MMIRequestEnable.EnabledRequests.EnableDoppler |
                                                               EVC30_MMIRequestEnable.EnabledRequests
                                                                   .EnableBrakePercentage;
            EVC30_MMIRequestEnable.Send();

            WaitForVerification("Check that the following buttons are displayed with a border with Dark-Grey text:" +
                                Environment.NewLine + Environment.NewLine +
                                @"1. The ‘Contract last window’ button." + Environment.NewLine +
                                @"2. The ‘Use short number’ button." + Environment.NewLine +
                                @"3. The ‘Enter RBC data’ button." + Environment.NewLine +
                                @"4. The ‘Radio Network ID’ button.");

            // Step 19

            DmiActions.Deactivate_Cabin(this);
            Wait_Realtime(5000);
            DmiActions.Activate_Cabin_1(this);

            DmiActions.Set_Driver_ID(this, "1234");

            DmiActions.Send_SB_Mode(this);
            DmiExpectedResults.SB_Mode_displayed(this);

            // Step 20
            // Test says do all this: but tested elsewhere (activating cabin cannot be done by driver)

            DmiActions.Set_Driver_ID(this, "1234");
            DmiActions.Send_SB_Mode(this);
            DmiActions.ShowInstruction(this, "Enter and confirm Driver ID");

            DmiActions.Request_Brake_Test(this);
            DmiActions.ShowInstruction(this, "Perform Brake Test");

            DmiActions.Perform_Brake_Test(this, 2);
            Wait_Realtime(5000);
            DmiActions.Display_Brake_Test_Successful(this, 3);

            DmiActions.Display_Level_Window(this);
            DmiActions.Delete_Brake_Test_Successful(this, 3);
            DmiActions.ShowInstruction(this, "Select and enter Level AWS/TPWS");
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Level = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_LEVEL.LNTC;

            DmiActions.Display_Main_Window_with_Start_button_not_enabled(this);
            DmiActions.ShowInstruction(this, @"Press ‘Train data’ button");

            DmiActions.Display_Fixed_Train_Data_Window(this);
            DmiActions.ShowInstruction(this, @"Perform the following actions on the DMI: " + Environment.NewLine +
                                  Environment.NewLine +
                                  "1. Enter FLU and confirm value in each input field." + Environment.NewLine +
                                  "2. Press OK on THIS window.");
          
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.Start |
                                                               EVC30_MMIRequestEnable.EnabledRequests.DriverID |
                                                               EVC30_MMIRequestEnable.EnabledRequests.TrainData |
                                                               EVC30_MMIRequestEnable.EnabledRequests.Level |
                                                               EVC30_MMIRequestEnable.EnabledRequests
                                                                   .TrainRunningNumber |
                                                               EVC30_MMIRequestEnable.EnabledRequests.Shunting |
                                                               EVC30_MMIRequestEnable.EnabledRequests.ExitShunting |
                                                               EVC30_MMIRequestEnable.EnabledRequests.NonLeading |
                                                               EVC30_MMIRequestEnable.EnabledRequests.MaintainShunting |
                                                               EVC30_MMIRequestEnable.EnabledRequests.EOA |
                                                               EVC30_MMIRequestEnable.EnabledRequests.Adhesion |
                                                               EVC30_MMIRequestEnable.EnabledRequests.SRSpeedDistance |
                                                               EVC30_MMIRequestEnable.EnabledRequests.TrainIntegrity |
                                                               EVC30_MMIRequestEnable.EnabledRequests.Language |
                                                               EVC30_MMIRequestEnable.EnabledRequests.Volume |
                                                               EVC30_MMIRequestEnable.EnabledRequests.Brightness |
                                                               EVC30_MMIRequestEnable.EnabledRequests.SystemVersion |
                                                               EVC30_MMIRequestEnable.EnabledRequests.SetVBC |
                                                               EVC30_MMIRequestEnable.EnabledRequests.RemoveVBC |
                                                               EVC30_MMIRequestEnable.EnabledRequests.ContactLastRBC |
                                                               EVC30_MMIRequestEnable.EnabledRequests.UseShortNumber |
                                                               EVC30_MMIRequestEnable.EnabledRequests.EnterRBCData |
                                                               EVC30_MMIRequestEnable.EnabledRequests.RadioNetworkID |
                                                               EVC30_MMIRequestEnable.EnabledRequests
                                                                   .GeographicalPosition |
                                                               EVC30_MMIRequestEnable.EnabledRequests
                                                                   .SetLocalTimeDateAndOffset |
                                                               EVC30_MMIRequestEnable.EnabledRequests.SetLocalOffset |
                                                               EVC30_MMIRequestEnable.EnabledRequests.Reserved |
                                                               EVC30_MMIRequestEnable.EnabledRequests.StartBrakeTest |
                                                               EVC30_MMIRequestEnable.EnabledRequests
                                                                   .EnableWheelDiameter |
                                                               EVC30_MMIRequestEnable.EnabledRequests.EnableDoppler |
                                                               EVC30_MMIRequestEnable.EnabledRequests
                                                                   .EnableBrakePercentage;
            EVC30_MMIRequestEnable.Send();
            EVC31_MMINTCDeSelect.Mmi_Q_Ntc_Enable =
                EVC31_MMINTCDeSelect.MMI_Q_NTC_ENABLE.NTC1 | EVC31_MMINTCDeSelect.MMI_Q_NTC_ENABLE.NTC2;
            //byte[] ntcBytes = new byte[2];
            //ntcBytes[0] = 1;
            //ntcBytes[1] = 2;
            //EVC31_MMINTCDeSelect.MMI_NID_NTC = ntcBytes;
            EVC31_MMINTCDeSelect.Send();

            WaitForVerification("Check that the following button is displayed with a border with Dark-Grey text:" +
                                Environment.NewLine + Environment.NewLine +
                                @"1. The ‘End of data entry’ button.");

            // Step 21

            DmiActions.Deactivate_Cabin(this);
            Wait_Realtime(5000);
            DmiActions.Activate_Cabin_1(this);

            DmiActions.Set_Driver_ID(this, "1234");

            DmiActions.Send_SB_Mode(this);
            DmiExpectedResults.SB_Mode_displayed(this);

            // Step 22
            // Test says active cabin (It is) and do brake test: as before ignore
            DMITestCases.DmiActions.ShowInstruction(this, @"Press the ‘Close’ button in the Main window.");


            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.Start |
                                                               EVC30_MMIRequestEnable.EnabledRequests.DriverID |
                                                               EVC30_MMIRequestEnable.EnabledRequests.TrainData |
                                                               EVC30_MMIRequestEnable.EnabledRequests.Level |
                                                               EVC30_MMIRequestEnable.EnabledRequests
                                                                   .TrainRunningNumber |
                                                               EVC30_MMIRequestEnable.EnabledRequests.Shunting |
                                                               EVC30_MMIRequestEnable.EnabledRequests.ExitShunting |
                                                               EVC30_MMIRequestEnable.EnabledRequests.NonLeading |
                                                               EVC30_MMIRequestEnable.EnabledRequests.MaintainShunting |
                                                               EVC30_MMIRequestEnable.EnabledRequests.EOA |
                                                               EVC30_MMIRequestEnable.EnabledRequests.Adhesion |
                                                               EVC30_MMIRequestEnable.EnabledRequests.SRSpeedDistance |
                                                               EVC30_MMIRequestEnable.EnabledRequests.TrainIntegrity |
                                                               EVC30_MMIRequestEnable.EnabledRequests.Language |
                                                               EVC30_MMIRequestEnable.EnabledRequests.Volume |
                                                               EVC30_MMIRequestEnable.EnabledRequests.Brightness |
                                                               EVC30_MMIRequestEnable.EnabledRequests.SystemVersion |
                                                               EVC30_MMIRequestEnable.EnabledRequests.SetVBC |
                                                               EVC30_MMIRequestEnable.EnabledRequests.RemoveVBC |
                                                               EVC30_MMIRequestEnable.EnabledRequests.ContactLastRBC |
                                                               EVC30_MMIRequestEnable.EnabledRequests.UseShortNumber |
                                                               EVC30_MMIRequestEnable.EnabledRequests.EnterRBCData |
                                                               EVC30_MMIRequestEnable.EnabledRequests.RadioNetworkID |
                                                               EVC30_MMIRequestEnable.EnabledRequests
                                                                   .GeographicalPosition |
                                                               EVC30_MMIRequestEnable.EnabledRequests
                                                                   .EndOfDataEntryNTC |
                                                               EVC30_MMIRequestEnable.EnabledRequests
                                                                   .SetLocalTimeDateAndOffset |
                                                               EVC30_MMIRequestEnable.EnabledRequests.SetLocalOffset |
                                                               EVC30_MMIRequestEnable.EnabledRequests.Reserved |
                                                               EVC30_MMIRequestEnable.EnabledRequests
                                                                   .EnableWheelDiameter |
                                                               EVC30_MMIRequestEnable.EnabledRequests.EnableDoppler |
                                                               EVC30_MMIRequestEnable.EnabledRequests
                                                                   .EnableBrakePercentage;
            EVC30_MMIRequestEnable.Send();

            DMITestCases.DmiActions.ShowInstruction(this,
                @"Press the ‘Settings’ button, then press the ‘Brake’ button");

            DmiActions.Open_the_Settings_window(this);

            WaitForVerification("Check that the following button is displayed with a border with Dark-Grey text:" +
                                Environment.NewLine + Environment.NewLine +
                                @"1. The ‘Brake test’ button.");
        }

        #endregion
    }
}
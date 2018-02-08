using System;
using Testcase.Telegrams.EVCtoDMI;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 37.7 Dialogue Sequence of Settings window
    /// TC-ID: 34.7
    /// 
    /// This test case verifies the dialogue sequence of the Settings window. The dialogue sequence shows the interaction with the driver when presses the ‘Settings’ button on the default window. The dialogue sequence of the Settings window shall comply with [ERA-ERTMS] standard and [MMI-ETCS-gen].
    /// 
    /// Tested Requirements:
    /// MMI_gen 9231; MMI_gen 11992; MMI_gen 11993; MMI_gen 11994; MMI_gen 11995; MMI_gen 8785 (partly: Settings, Language, Volumne, Brightness, System version, Set VBC, Set VBC validation, Remove VBC, Remove VBC validation, additional DMI technical function, Maintenance password, Maintenance, Brake, System Info, Set clock);
    /// 
    /// Scenario:
    /// Verifies the state of ‘Close’ button in the following windows,Maintenance password windowMaintenance windowSettings WindowSystem version WindowLanguage WindowVolume WindowBrightness WindowSet VBC WindowValidate Set VBC WindowRemove VBC WindowValidate Remove VBC WindowBrake windowSystem info windowSet Clock windowPress the ‘Close’ button in each window according to scenarion No.1 to verifiy an operation of enabled button and dialogue sequence of Settings window.Simulate Loss-communication between ETCS and DMI. Then, verify the state of the following buttons,SettingsLanguageBrightnessVolumePress on each button according to scenario No. 3 to verify that DMI does not send out packet EVC-101 and EVC-122.Note: This test case is verifies only SB mode Level 
    /// 1.However, tester can use this scenario to verify test result in SB mode for Level 2 and 3 also.
    /// 
    /// Used files:
    /// N/A
    /// </summary>
    public class TC_34_7_Dialogue_Sequences : TestcaseBase
    {
        public override bool TestcaseEntryPoint()
        {
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 26719;
            // Testcase entrypoint
            StartUp();

            MakeTestStepHeader(1, UniqueIdentifier++, "Press ‘Settings’ button",
                "DMI displays Settings window.Verify the following information,The ‘Close’ button is enabled");
            /*
            Test Step 1
            Action: Press ‘Settings’ button
            Expected Result: DMI displays Settings window.Verify the following information,The ‘Close’ button is enabled
            Test Step Comment: (1) MMI_gen 9231 (partly: Settings window);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press the ‘Settings’ button");

            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Settings; // Settings;
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH =
                EVC30_MMIRequestEnable.EnabledRequests.EnableWheelDiameter |
                EVC30_MMIRequestEnable.EnabledRequests.SystemVersion |
                EVC30_MMIRequestEnable.EnabledRequests.Language |
                EVC30_MMIRequestEnable.EnabledRequests.Volume |
                EVC30_MMIRequestEnable.EnabledRequests.Brightness |
                EVC30_MMIRequestEnable.EnabledRequests.SetVBC |
                EVC30_MMIRequestEnable.EnabledRequests.EnableBrakePercentage |
                EVC30_MMIRequestEnable.EnabledRequests.SetLocalTimeDateAndOffset;
            EVC30_MMIRequestEnable.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Settings window." + Environment.NewLine +
                                "2. The ‘Close’ button is enabled");

            MakeTestStepHeader(2, UniqueIdentifier++, "Press ‘Maintenance’ button",
                "DMI displays Maintenance password window.Verify the following information,The ‘Close’ button is enabled");
            /*
            Test Step 2
            Action: Press ‘Maintenance’ button
            Expected Result: DMI displays Maintenance password window.Verify the following information,The ‘Close’ button is enabled
            Test Step Comment: (1) MMI_gen 9231 (partly: Maintenance password);   
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press the ‘Maintenance’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Maintenance password window." + Environment.NewLine +
                                "2. The ‘Close’ button is enabled");

            MakeTestStepHeader(3, UniqueIdentifier++, "Press ‘Close’ button", "DMI displays Settings window");
            /*
            Test Step 3
            Action: Press ‘Close’ button
            Expected Result: DMI displays Settings window
            Test Step Comment: MMI_gen 8785 (partly: additional DMI technical function, Maintenance password);
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button in the Maintenance Window");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI closes the Maintenance password window and displays the Settings window.");

            MakeTestStepHeader(4, UniqueIdentifier++,
                "Perform the following procedure,Press ‘Maintenance’ button.Enter the Maintenance window by entering the password same as a value in tag ‘PASS_CODE_MTN’ of the configuration file and confirming the password",
                "DMI displays Maintenance window.Verify the following information,The ‘Close’ button is enabled");
            /*
            Test Step 4
            Action: Perform the following procedure,Press ‘Maintenance’ button.Enter the Maintenance window by entering the password same as a value in tag ‘PASS_CODE_MTN’ of the configuration file and confirming the password
            Expected Result: DMI displays Maintenance window.Verify the following information,The ‘Close’ button is enabled
            Test Step Comment: (1) MMI_gen 9231 (partly: Maintenance window);   
            */
            DmiActions.ShowInstruction(this,
                @"Press the ‘Maintenance’ button. Enter the value from the configuration file PASS_CDE_MTN tag and confirm the password");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Close’ button is enabled");

            MakeTestStepHeader(5, UniqueIdentifier++, "Press ‘Close’ button", "DMI displays Settings window");
            /*
            Test Step 5
            Action: Press ‘Close’ button
            Expected Result: DMI displays Settings window
            Test Step Comment: MMI_gen 8785 (partly: additional DMI technical function, Maintenance);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button in the Maintenance window");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Settings window.");

            MakeTestStepHeader(6, UniqueIdentifier++,
                "Perform the following procedure, Press ‘Close’ button.Enter Driver ID and perform brake test.Select and confirm Level 1.Press ‘Close’ button",
                "DMI displays Default window");
            /*
            Test Step 6
            Action: Perform the following procedure, Press ‘Close’ button.Enter Driver ID and perform brake test.Select and confirm Level 1.Press ‘Close’ button
            Expected Result: DMI displays Default window
            */
            // Ignore brake test
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button");

            DmiActions.Set_Driver_ID(this, "1234");
            DmiActions.ShowInstruction(this, @"Confirm driver ID");

            EVC20_MMISelectLevel.MMI_Q_CLOSE_ENABLE = Variables.MMI_Q_CLOSE_ENABLE.Disabled;
            EVC20_MMISelectLevel.MMI_Q_LEVEL_NTC_ID = new Variables.MMI_Q_LEVEL_NTC_ID[]
                {Variables.MMI_Q_LEVEL_NTC_ID.ETCS_Level};
            EVC20_MMISelectLevel.MMI_M_CURRENT_LEVEL = new Variables.MMI_M_CURRENT_LEVEL[]
                {Variables.MMI_M_CURRENT_LEVEL.NotLastUsedLevel};
            EVC20_MMISelectLevel.MMI_M_LEVEL_FLAG = new Variables.MMI_M_LEVEL_FLAG[]
                {Variables.MMI_M_LEVEL_FLAG.MarkedLevel};
            EVC20_MMISelectLevel.MMI_M_INHIBITED_LEVEL = new Variables.MMI_M_INHIBITED_LEVEL[]
                {Variables.MMI_M_INHIBITED_LEVEL.NotInhibited};
            EVC20_MMISelectLevel.MMI_M_INHIBIT_ENABLE = new Variables.MMI_M_INHIBIT_ENABLE[]
                {Variables.MMI_M_INHIBIT_ENABLE.AllowedForInhibiting};
            EVC20_MMISelectLevel.MMI_M_LEVEL_NTC_ID = new Variables.MMI_M_LEVEL_NTC_ID[]
                {Variables.MMI_M_LEVEL_NTC_ID.L1};
            EVC20_MMISelectLevel.Send();

            DmiActions.ShowInstruction(this, "Select and confirm Level 1, then press the ‘Close’ button.");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Default window.");

            MakeTestStepHeader(7, UniqueIdentifier++, "Press ‘Settings’ button",
                "DMI displays Settings window.Verify the following information,The ‘Close’ button is enabled");
            /*
            Test Step 7
            Action: Press ‘Settings’ button
            Expected Result: DMI displays Settings window.Verify the following information,The ‘Close’ button is enabled
            Test Step Comment: (1) MMI_gen 9231 (partly: Settings window);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press the ‘Settings’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Settings window." + Environment.NewLine +
                                "2. The ‘Close’ button is enabled");

            MakeTestStepHeader(8, UniqueIdentifier++, "Press ‘Close’ button", "DMI displays the Default window");
            /*
            Test Step 8
            Action: Press ‘Close’ button
            Expected Result: DMI displays the Default window
            Test Step Comment: MMI_gen 8785 (partly: Settings);
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button in the Settings window");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Default window.");

            MakeTestStepHeader(9, UniqueIdentifier++, "Press ‘Settings’ button", "DMI displays Settings window");
            /*
            Test Step 9
            Action: Press ‘Settings’ button
            Expected Result: DMI displays Settings window
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press the ‘Settings’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Settings window.");

            MakeTestStepHeader(10, UniqueIdentifier++, "Press ‘System version’ button",
                "DMI displays System version window");
            /*
            Test Step 10
            Action: Press ‘System version’ button
            Expected Result: DMI displays System version window
            */
            DmiActions.ShowInstruction(this, @"Press the ‘System version’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the System version window.");

            MakeTestStepHeader(11, UniqueIdentifier++, "Press ‘Close’ button", "DMI displays Settings window");
            /*
            Test Step 11
            Action: Press ‘Close’ button
            Expected Result: DMI displays Settings window
            Test Step Comment: MMI_gen 8785 (partly: System version);
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button in the System version window");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Settings window.");

            MakeTestStepHeader(12, UniqueIdentifier++, "Press ‘Language’ button",
                "Verify the following information,DMI displays Language window.The ‘Close’ button is enabled");
            /*
            Test Step 12
            Action: Press ‘Language’ button
            Expected Result: Verify the following information,DMI displays Language window.The ‘Close’ button is enabled
            Test Step Comment: (1) MMI_gen 11992;(2) MMI_gen 9231 (partly: Language window);   
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Language’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Language window." + Environment.NewLine +
                                "2. The ‘Close’ button is enabled");

            MakeTestStepHeader(13, UniqueIdentifier++, "Press ‘Close’ button", "DMI displays Settings window");
            /*
            Test Step 13
            Action: Press ‘Close’ button
            Expected Result: DMI displays Settings window
            Test Step Comment: MMI_gen 8785 (partly: Language);
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button in the Language window");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Settings window.");

            MakeTestStepHeader(14, UniqueIdentifier++, "Press ‘Language’ button", "DMI displays Language window");
            /*
            Test Step 14
            Action: Press ‘Language’ button
            Expected Result: DMI displays Language window
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Language’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Language window.");

            MakeTestStepHeader(15, UniqueIdentifier++, "Confirm entered data by pressing input field",
                "DMI displays Settings window");
            /*
            Test Step 15
            Action: Confirm entered data by pressing input field
            Expected Result: DMI displays Settings window
            Test Step Comment: Table 71 (Partly: step S2 (Language window));
            */
            DmiActions.ShowInstruction(this, @"Press the data input field to accept the data");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Settings window.");

            MakeTestStepHeader(16, UniqueIdentifier++, "Press ‘Volume’ button",
                "Verify the following information,DMI displays Volume window.The ‘Close’ button is enabled");
            /*
            Test Step 16
            Action: Press ‘Volume’ button
            Expected Result: Verify the following information,DMI displays Volume window.The ‘Close’ button is enabled
            Test Step Comment: (1) MMI_gen 11994; (2) MMI_gen 9231 (partly: Volume window);   
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press the ‘Volume’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Volume window." + Environment.NewLine +
                                "2. The ‘Close’ button is enabled");

            MakeTestStepHeader(17, UniqueIdentifier++, "Press ‘Close’ button", "DMI displays Settings window");
            /*
            Test Step 17
            Action: Press ‘Close’ button
            Expected Result: DMI displays Settings window
            Test Step Comment: MMI_gen 8785 (partly: Volume);
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button in the Volume window");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Settings window.");

            MakeTestStepHeader(18, UniqueIdentifier++, "Press ‘Volume’ button", "DMI displays Volume window");
            /*
            Test Step 18
            Action: Press ‘Volume’ button
            Expected Result: DMI displays Volume window
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press the ‘Volume’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Volume window.");

            MakeTestStepHeader(19, UniqueIdentifier++, "Confirm entered data by pressing input field",
                "DMI displays Settings window");
            /*
            Test Step 19
            Action: Confirm entered data by pressing input field
            Expected Result: DMI displays Settings window
            Test Step Comment: Table 71 (Partly: step S3 (Volume window));
            */
            DmiActions.ShowInstruction(this, @"Press the data input field to accept the data");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Settings window.");

            MakeTestStepHeader(20, UniqueIdentifier++, "Press ‘Brightness’ button",
                "Verify the following information,DMI displays Brightness window.The ‘Close’ button is enabled");
            /*
            Test Step 20
            Action: Press ‘Brightness’ button
            Expected Result: Verify the following information,DMI displays Brightness window.The ‘Close’ button is enabled
            Test Step Comment: (1) MMI_gen 11993; (2) MMI_gen 9231 (partly: Volume window);   
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Brightness’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Brightness window." + Environment.NewLine +
                                "2. The ‘Close’ button is enabled");

            MakeTestStepHeader(21, UniqueIdentifier++, "Press ‘Close’ button", "DMI displays Settings window");
            /*
            Test Step 21
            Action: Press ‘Close’ button
            Expected Result: DMI displays Settings window
            Test Step Comment: MMI_gen 8785 (partly: Brightness);
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button in the Brightness window");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Settings window.");

            MakeTestStepHeader(22, UniqueIdentifier++, "Press ‘Brightness’ button", "DMI displays Brightness window");
            /*
            Test Step 22
            Action: Press ‘Brightness’ button
            Expected Result: DMI displays Brightness window
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Brightness’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Brightness window.");

            MakeTestStepHeader(23, UniqueIdentifier++, "Confirm entered data by pressing input field",
                "DMI displays Settings window");
            /*
            Test Step 23
            Action: Confirm entered data by pressing input field
            Expected Result: DMI displays Settings window
            Test Step Comment: Table 71 (Partly: step S4 (Brightness window));
            */
            DmiActions.ShowInstruction(this, @"Press the data input field to accept the data");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Settings window.");

            MakeTestStepHeader(24, UniqueIdentifier++, "Press ‘System version’ button",
                "DMI displays System version window.Verify the following information,The ‘Close’ button is enabled");
            /*
            Test Step 24
            Action: Press ‘System version’ button
            Expected Result: DMI displays System version window.Verify the following information,The ‘Close’ button is enabled
            Test Step Comment: (1) MMI_gen 9231 (partly: System version window);    
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press the ‘System version’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the System version window." + Environment.NewLine +
                                "2. The ‘Close’ button is enabled");

            MakeTestStepHeader(25, UniqueIdentifier++, "Press ‘Close’ button", "DMI displays Settings window");
            /*
            Test Step 25
            Action: Press ‘Close’ button
            Expected Result: DMI displays Settings window
            Test Step Comment: MMI_gen 8785 (partly: System version);Table 71 (Partly: step S5 (System version window));
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button in the System version window");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Settings window.");

            MakeTestStepHeader(26, UniqueIdentifier++, "Press ‘Set VBC’ button",
                "DMI displays Set VBC window.Verify the following information,The ‘Close’ button is enabled");
            /*
            Test Step 26
            Action: Press ‘Set VBC’ button
            Expected Result: DMI displays Set VBC window.Verify the following information,The ‘Close’ button is enabled
            Test Step Comment: (1) MMI_gen 9231 (partly: Set VBC window);    
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press the  ‘Set VBC’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Set VBC window." + Environment.NewLine +
                                "2. The ‘Close’ button is enabled");

            MakeTestStepHeader(27, UniqueIdentifier++, "Press ‘Close’ button", "DMI displays Settings window");
            /*
            Test Step 27
            Action: Press ‘Close’ button
            Expected Result: DMI displays Settings window
            Test Step Comment: MMI_gen 8785 (partly: Set VBC);
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button in the Set VBC window");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Settings window.");

            MakeTestStepHeader(28, UniqueIdentifier++,
                "Perforrm the following procedure,Press ‘Set VBC’ button. Enter the value 65536 and confirm by pressing an input field.Press ‘Yes’ button",
                "DMI displays Validate Set VBC window. Verify the following information,The ‘Close’ button is enabled");
            /*
            Test Step 28
            Action: Perforrm the following procedure,Press ‘Set VBC’ button. Enter the value 65536 and confirm by pressing an input field.Press ‘Yes’ button
            Expected Result: DMI displays Validate Set VBC window. Verify the following information,The ‘Close’ button is enabled
            Test Step Comment: (1) MMI_gen 9231 (partly: Validate Set VBC window);    Table 71 (Partly: step S6-1 (Set VBC window));
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Set VBC’ button");

            EVC18_MMISetVBC.MMI_M_BUTTONS = Variables.MMI_M_BUTTONS_VBC.BTN_YES_DATA_ENTRY_COMPLETE;
            EVC18_MMISetVBC.MMI_N_VBC = 0;
            EVC18_MMISetVBC.Send();

            DmiActions.ShowInstruction(this,
                @"Enter the value 65536 and press a data input field to confirm. Press the ‘Yes’ button");

            EVC28_MMIEchoedSetVBCData.MMI_M_VBC_CODE_ = 65536;
            EVC28_MMIEchoedSetVBCData.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Set VBC validation window." + Environment.NewLine +
                                "2. The ‘Close’ button is enabled");

            MakeTestStepHeader(29, UniqueIdentifier++, "Press ‘Close’ button", "DMI displays Settings window");
            /*
            Test Step 29
            Action: Press ‘Close’ button
            Expected Result: DMI displays Settings window
            Test Step Comment: MMI_gen 8785 (partly: Set VBC validation);
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button in the Set VBC validation window");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Settings window.");

            MakeTestStepHeader(30, UniqueIdentifier++,
                "Perform a following procedure,Press ‘Set VBC’ buttonEnter and confirm value 65536At Validate set VBC window, press ‘No’ button and press an input field",
                "DMI displays Set VBC window");
            /*
            Test Step 30
            Action: Perform a following procedure,Press ‘Set VBC’ buttonEnter and confirm value 65536At Validate set VBC window, press ‘No’ button and press an input field
            Expected Result: DMI displays Set VBC window
            Test Step Comment: Table 71 (Partly: step S6-2 (Set VBC validation window));
            */
            EVC18_MMISetVBC.MMI_M_BUTTONS = Variables.MMI_M_BUTTONS_VBC.BTN_YES_DATA_ENTRY_COMPLETE;
            EVC18_MMISetVBC.MMI_N_VBC = 0;
            EVC18_MMISetVBC.Send();

            DmiActions.ShowInstruction(this,
                @"Press the ‘Set VBC’ button. Enter the value 65536. Press the ‘No’ button in the Set VBC validation window and press an input field");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Set VBC window");

            MakeTestStepHeader(31, UniqueIdentifier++,
                "Perform a following procedure,Enter and confirm value 65536At Validate set VBC window, press ‘Yes’ button and press an input field",
                "DMI displays Settings window.The ‘Remove VBC’ button is enabled");
            /*
            Test Step 31
            Action: Perform a following procedure,Enter and confirm value 65536At Validate set VBC window, press ‘Yes’ button and press an input field
            Expected Result: DMI displays Settings window.The ‘Remove VBC’ button is enabled
            Test Step Comment: Table 71 (Partly: step S6-2 (Set VBC validation window));
            */
            DmiActions.ShowInstruction(this, @"Enter and confirm the value 65536");

            EVC28_MMIEchoedSetVBCData.MMI_M_VBC_CODE_ = 65536;
            EVC28_MMIEchoedSetVBCData.Send();

            DmiActions.ShowInstruction(this,
                @"Press the ‘Yes’ button in the Set VBC validation window and press an input field");

            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Settings; // Settings;
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH =
                EVC30_MMIRequestEnable.EnabledRequests.EnableWheelDiameter |
                EVC30_MMIRequestEnable.EnabledRequests.SystemVersion |
                EVC30_MMIRequestEnable.EnabledRequests.Language |
                EVC30_MMIRequestEnable.EnabledRequests.Volume |
                EVC30_MMIRequestEnable.EnabledRequests.Brightness |
                EVC30_MMIRequestEnable.EnabledRequests.SetVBC |
                EVC30_MMIRequestEnable.EnabledRequests.EnableBrakePercentage |
                EVC30_MMIRequestEnable.EnabledRequests.SetLocalTimeDateAndOffset |
                EVC30_MMIRequestEnable.EnabledRequests.RemoveVBC;
            EVC30_MMIRequestEnable.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Settings window." + Environment.NewLine +
                                "2. The ‘Remove VBC’ button is enabled.");

            MakeTestStepHeader(32, UniqueIdentifier++, "Press ‘Remove VBC’ button",
                "DMI displays Remove VBC window.Verify the following information,The ‘Close’ button is enabled");
            /*
            Test Step 32
            Action: Press ‘Remove VBC’ button
            Expected Result: DMI displays Remove VBC window.Verify the following information,The ‘Close’ button is enabled
            Test Step Comment: (1) MMI_gen 9231 (partly: Remove VBC window);    
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press the ‘Remove VBC’ button");

            EVC19_MMIRemoveVBC.MMI_N_VBC = 0;
            EVC19_MMIRemoveVBC.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Remove VBC window." + Environment.NewLine +
                                "2. The ‘Close’ button is enabled.");

            MakeTestStepHeader(33, UniqueIdentifier++, "Press ‘Close’ button", "DMI displays Settings window");
            /*
            Test Step 33
            Action: Press ‘Close’ button
            Expected Result: DMI displays Settings window
            Test Step Comment: MMI_gen 8785 (partly: Remove VBC);
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button in the Remove VBC window");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Settings window.");

            MakeTestStepHeader(34, UniqueIdentifier++,
                "Perforrm the following procedure,Enter the value 65536 and confirm by pressing an input field.Press ‘Yes’ button",
                "DMI displays Validate Remove VBC window. Verify the following information,The ‘Close’ button is enabled");
            /*
            Test Step 34
            Action: Perforrm the following procedure,Enter the value 65536 and confirm by pressing an input field.Press ‘Yes’ button
            Expected Result: DMI displays Validate Remove VBC window. Verify the following information,The ‘Close’ button is enabled
            Test Step Comment: (1) MMI_gen 9231 (partly: Validate Remove VBC window);    
            */
            // Test omits press RBC button...
            DmiActions.ShowInstruction(this, @"Press the ‘Remove VBC’ button");

            EVC19_MMIRemoveVBC.MMI_N_VBC = 0;
            EVC19_MMIRemoveVBC.MMI_M_BUTTONS = Variables.MMI_M_BUTTONS_VBC.BTN_YES_DATA_ENTRY_COMPLETE;
            EVC19_MMIRemoveVBC.Send();

            DmiActions.ShowInstruction(this,
                @"Enter the value 65536 and press an input field to confirm. Press the ‘Yes’ button");

            EVC29_MMIEchoedRemoveVBCData.MMI_M_VBC_CODE_ = 65536;
            EVC29_MMIEchoedRemoveVBCData.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Remove VBC validation window." + Environment.NewLine +
                                "2. The ‘Close’ button is enabled.");

            MakeTestStepHeader(35, UniqueIdentifier++, "Press ‘Close’ button", "DMI displays Settings window");
            /*
            Test Step 35
            Action: Press ‘Close’ button
            Expected Result: DMI displays Settings window
            Test Step Comment: MMI_gen 8785 (partly: Remove VBC validation);
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button in the Remove VBC validation window");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Settings window.");

            MakeTestStepHeader(36, UniqueIdentifier++,
                "Perforrm the following procedure,Press ‘Remove VBC’ button. Enter the value 65536 and confirm by pressing an input field.At the Validate remove VBC window, press ‘No’ button and press an input field",
                "DMI displays Remove VBC window");
            /*
            Test Step 36
            Action: Perforrm the following procedure,Press ‘Remove VBC’ button. Enter the value 65536 and confirm by pressing an input field.At the Validate remove VBC window, press ‘No’ button and press an input field
            Expected Result: DMI displays Remove VBC window
            Test Step Comment: Table 71 (Partly: step 76-2 (Remove VBC validation window));
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Remove VBC’ button");

            EVC19_MMIRemoveVBC.MMI_N_VBC = 0;
            EVC19_MMIRemoveVBC.MMI_M_BUTTONS = Variables.MMI_M_BUTTONS_VBC.BTN_YES_DATA_ENTRY_COMPLETE;
            EVC19_MMIRemoveVBC.Send();

            DmiActions.ShowInstruction(this,
                @"Enter the value 65536 and confirm by pressing an input field. Press the ‘No’ button in the Remove VBC validation window and press an input field");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Remove VBC window.");

            MakeTestStepHeader(37, UniqueIdentifier++,
                "Perform a following procedure,Enter and confirm value 65536At the Validate remove VBC window, press ‘Yes’ button and press an input field",
                "DMI displays Settings window.The ‘Remove VBC’ button is disabled");
            /*
            Test Step 37
            Action: Perform a following procedure,Enter and confirm value 65536At the Validate remove VBC window, press ‘Yes’ button and press an input field
            Expected Result: DMI displays Settings window.The ‘Remove VBC’ button is disabled
            Test Step Comment: Table 71 (Partly: step 76-2 (Remove VBC validation window));
            */
            // Step missing this
            DmiActions.ShowInstruction(this, @"Press the ‘Remove VBC’ button");

            EVC19_MMIRemoveVBC.MMI_N_VBC = 0;
            EVC19_MMIRemoveVBC.MMI_M_BUTTONS = Variables.MMI_M_BUTTONS_VBC.BTN_YES_DATA_ENTRY_COMPLETE;
            EVC19_MMIRemoveVBC.Send();

            DmiActions.ShowInstruction(this, @"Enter the value 65536 and confirm by pressing an input field");


            EVC29_MMIEchoedRemoveVBCData.MMI_M_VBC_CODE_ = 65536;
            EVC29_MMIEchoedRemoveVBCData.Send();

            DmiActions.ShowInstruction(this,
                @"Press the ‘Yes’ button in the Remove VBC validation window and press an input field");

            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Settings;
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH =
                (EVC30_MMIRequestEnable.EnabledRequests.EnableWheelDiameter |
                 EVC30_MMIRequestEnable.EnabledRequests.SystemVersion |
                 EVC30_MMIRequestEnable.EnabledRequests.Language |
                 EVC30_MMIRequestEnable.EnabledRequests.Volume |
                 EVC30_MMIRequestEnable.EnabledRequests.Brightness |
                 EVC30_MMIRequestEnable.EnabledRequests.SetVBC |
                 EVC30_MMIRequestEnable.EnabledRequests.EnableBrakePercentage |
                 EVC30_MMIRequestEnable.EnabledRequests.SetLocalTimeDateAndOffset) &
                ~EVC30_MMIRequestEnable.EnabledRequests.RemoveVBC;
            EVC30_MMIRequestEnable.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Settings window." + Environment.NewLine +
                                "2. The ‘Remove VBC’ button is disabled.");

            MakeTestStepHeader(38, UniqueIdentifier++, "Press ‘Brake’ button",
                "DMI displays Brake window.Verify the following information,The ‘Close’ button is enabled");
            /*
            Test Step 38
            Action: Press ‘Brake’ button
            Expected Result: DMI displays Brake window.Verify the following information,The ‘Close’ button is enabled
            Test Step Comment: (1) MMI_gen 9231 (partly: Brake window);    
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press the ‘Brake’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Brake window." + Environment.NewLine +
                                "2. The ‘Close’ button is enabled.");

            MakeTestStepHeader(39, UniqueIdentifier++, "Press ‘Close’ button", "DMI displays Settings window");
            /*
            Test Step 39
            Action: Press ‘Close’ button
            Expected Result: DMI displays Settings window
            Test Step Comment: MMI_gen 8785 (partly: additional DMI technical function, Brake);
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Settings window.");

            MakeTestStepHeader(40, UniqueIdentifier++, "Press ‘System Info’ button",
                "DMI displays System Info window.Verify the following information,The ‘Close’ button is enabled");
            /*
            Test Step 40
            Action: Press ‘System Info’ button
            Expected Result: DMI displays System Info window.Verify the following information,The ‘Close’ button is enabled
            Test Step Comment: (1) MMI_gen 9231 (partly: System Info window);    
            */
            DmiActions.ShowInstruction(this, @"Press the ‘System Info’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the System Info window." + Environment.NewLine +
                                "2. The ‘Close’ button is enabled.");

            MakeTestStepHeader(41, UniqueIdentifier++, "Press ‘Close’ button", "DMI displays Settings window");
            /*
            Test Step 41
            Action: Press ‘Close’ button
            Expected Result: DMI displays Settings window
            Test Step Comment: MMI_gen 8785 (partly: additional DMI technical function, System Info);
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button in the System Info window");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Settings window.");

            MakeTestStepHeader(42, UniqueIdentifier++, "Press ‘Set Clock’ button",
                "DMI displays Set clock window.Verify the following information,The ‘Close’ button is enabled");
            /*
            Test Step 42
            Action: Press ‘Set Clock’ button
            Expected Result: DMI displays Set clock window.Verify the following information,The ‘Close’ button is enabled
            Test Step Comment: (1) MMI_gen 9231 (partly: Set Clock window);    
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Set Clock’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Set Clock window." + Environment.NewLine +
                                "2. The ‘Close’ button is enabled.");

            MakeTestStepHeader(43, UniqueIdentifier++, "Press ‘Close’ button", "DMI displays Settings window");
            /*
            Test Step 43
            Action: Press ‘Close’ button
            Expected Result: DMI displays Settings window
            Test Step Comment: MMI_gen 8785 (partly: additional DMI technical function, Set Clock);
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button in the Set Clock  window");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Settings window.");

            MakeTestStepHeader(44, UniqueIdentifier++,
                "Press ‘Close’ button.Then, simulate loss-communication between ETCS onboard and DMI",
                "DMI displays Default window with the  message “ATP Down Alarm” and sound alarm.Verify the following information,The ‘Settings’ button is displays as enabled state in sub-area F5");
            /*
            Test Step 44
            Action: Press ‘Close’ button.Then, simulate loss-communication between ETCS onboard and DMI
            Expected Result: DMI displays Default window with the  message “ATP Down Alarm” and sound alarm.Verify the following information,The ‘Settings’ button is displays as enabled state in sub-area F5
            Test Step Comment: (1) MMI_gen 11995 (partly: enable settings);
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button");
            DmiActions.Simulate_communication_loss_EVC_DMI(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Default window with the message ‘ATP-Down Alarm’." +
                                Environment.NewLine +
                                "2. DMI plays the ‘Alarm’ sound");

            MakeTestStepHeader(45, UniqueIdentifier++, "Press ‘Settings’ button",
                "DMI displays Settings window.Verify the following information,The buttons below are in enabled state,LanguageBrightnessVolumeUse the log file to confirm that DMI does not send out packet EVC-101 and EVC122");
            /*
            Test Step 45
            Action: Press ‘Settings’ button
            Expected Result: DMI displays Settings window.Verify the following information,The buttons below are in enabled state,LanguageBrightnessVolumeUse the log file to confirm that DMI does not send out packet EVC-101 and EVC122
            Test Step Comment: (1) MMI_gen 11995 (partly: enable language, brightnesss and volume);(2) MMI_gen 11995 (partly: no request send to ETC, settings button is pressed);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press the ‘Settings’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Settings window." + Environment.NewLine +
                                "2. The ‘Language’ button is enabled." + Environment.NewLine +
                                "3. The ‘Brightness’ button is enabled." + Environment.NewLine +
                                "4. The ‘Volume’ button is enabled.");

            MakeTestStepHeader(46, UniqueIdentifier++, "Press ‘Language’ button",
                "Verify the following information,DMI displays Language window.The ‘Close’ button is enabled.Use the log file to confirm that DMI does not send out packet EVC-101 and EVC122");
            /*
            Test Step 46
            Action: Press ‘Language’ button
            Expected Result: Verify the following information,DMI displays Language window.The ‘Close’ button is enabled.Use the log file to confirm that DMI does not send out packet EVC-101 and EVC122
            Test Step Comment: (1) MMI_gen 11992;(2) MMI_gen 9231 (partly: Language window);   (3) MMI_gen 11995 (partly: no request send to ETC, language button is pressed);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press the ‘Language’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Language window." + Environment.NewLine +
                                "2. The ‘Close’ button is enabled.");

            MakeTestStepHeader(47, UniqueIdentifier++, "Press ‘Close’ button", "DMI displays Settings window");
            /*
            Test Step 47
            Action: Press ‘Close’ button
            Expected Result: DMI displays Settings window
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button in the Language window");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Settings window.");

            MakeTestStepHeader(48, UniqueIdentifier++, "Press ‘Brightness’ button",
                "Verify the following information,DMI displays Brightness window.The ‘Close’ button is enabled.Use the log file to confirm that DMI does not send out packet EVC-101 and EVC122");
            /*
            Test Step 48
            Action: Press ‘Brightness’ button
            Expected Result: Verify the following information,DMI displays Brightness window.The ‘Close’ button is enabled.Use the log file to confirm that DMI does not send out packet EVC-101 and EVC122
            Test Step Comment: (1) MMI_gen 11993;(2) MMI_gen 9231 (partly: Brightness window);   (3) MMI_gen 11995 (partly: no request send to ETC, Brightness button is pressed);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press ‘Brightness’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Brightness window." + Environment.NewLine +
                                "2. The ‘Close’ button is enabled.");

            MakeTestStepHeader(49, UniqueIdentifier++, "Press ‘Close’ button", "DMI displays Settings window");
            /*
            Test Step 49
            Action: Press ‘Close’ button
            Expected Result: DMI displays Settings window
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button in the Brightness window");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Settings window.");

            MakeTestStepHeader(50, UniqueIdentifier++, "Press ‘Volume’ button",
                "Verify the following information,DMI displays Volume window.The ‘Close’ button is enabled.Use the log file to confirm that DMI does not send out packet EVC-101 and EVC122");
            /*
            Test Step 50
            Action: Press ‘Volume’ button
            Expected Result: Verify the following information,DMI displays Volume window.The ‘Close’ button is enabled.Use the log file to confirm that DMI does not send out packet EVC-101 and EVC122
            Test Step Comment: (1) MMI_gen 11994;(2) MMI_gen 9231 (partly: Volume window);   (3) MMI_gen 11995 (partly: no request send to ETC, Volume button is pressed);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press ‘Volume’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Volume window." + Environment.NewLine +
                                "2. The ‘Close’ button is enabled.");

            MakeTestStepHeader(51, UniqueIdentifier++, "Press ‘Close’ button", "DMI displays Settings window");
            /*
            Test Step 51
            Action: Press ‘Close’ button
            Expected Result: DMI displays Settings window
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button in the Volume window");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Settings window.");

            MakeTestStepHeader(52, UniqueIdentifier++, "End of test", "");

            /*
            Test Step 52
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
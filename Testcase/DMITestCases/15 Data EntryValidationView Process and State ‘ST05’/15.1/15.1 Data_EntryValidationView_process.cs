using System;
using Testcase.Telegrams.EVCtoDMI;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 15.1 Data Entry/Validation/View process
    /// TC-ID: 10.1
    /// 
    /// This test case verifies the buttons state in menu windows.
    /// 
    /// Tested Requirements:
    /// MMI_gen 1316 (partly: disabled state in Table 23 (Active state, Idle state), enable state in Table 23 (Active state, Idle state)); MMI_gen 5647;
    /// 
    /// Scenario:
    /// 1. The ‘Settings’ menu window is displayed.
    /// 2. Use the test script files to send packets in order to verify button state in a Setting menu window. 
    /// 3. Active the cabin and perform SoM until select and confirm Level 1.
    /// 4. Use the test script files to send packets in order to verify button state in a Main menu window. 
    /// 5. Open the ‘Override’ window and use the test script files to send packets in order to verify buttons state. 
    /// 6. Open the ‘Special’ window and use the test script files to send packets in order to verify buttons state. 
    /// 7. Open the ‘Setting’ window and use the test script files to send packets in order to verify buttons state.
    /// 8. Reactive the cabin and perform SoM until select and confirm Level 2.
    /// 9. Open the ‘RBC contact’ window and use the test script files to send packets in order to verify buttons state.
    /// </summary>
    public class TC_ID_10_1_Data_EntryValidationView_process : TestcaseBase
    {

        public override bool TestcaseEntryPoint()
        {
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 0;
            // Testcase entrypoint
            StartUp();

            DmiActions.Complete_SoM_L1_SB(this);

            MakeTestStepHeader(1, UniqueIdentifier++, "Press ‘Settings menu’ button",
                "Settings menu window is displayed");
            /*
            Test Step 1
            Action: Press ‘Settings menu’ button
            Expected Result: Settings menu window is displayed
            */
            DmiActions.ShowInstruction(this, "Press the ‘Settings’ button");

            // Can you tell this?
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Settings window.");

            MakeTestStepHeader(2, UniqueIdentifier++,
                "Use the test script file 10_1_a.xml to disable and enable button via EVC-30 with",
                "All buttons in Settings menu window are disabled, except ‘Lock screen for cleaning’. 10 seconds later");
            /*
            Test Step 2
            Action: Use the test script file 10_1_a.xml to disable and enable button via EVC-30 with
                    Packet 1 (disable all button) all bit of variable MMI_Q_REQUEST_ENABLE = ‘0’ and MMI_NID_WINDOW = 255
                    Packet 2 (enable all button) Send EVC-30 with all bit of variable MMI_Q_REQUEST_ENABLE = ‘1’ and MMI_NID_WINDOW = 255
            Expected Result: All buttons in Settings menu window are disabled, except ‘Lock screen for cleaning’. 10 seconds later
                            All buttons in Settings menu window are enabled, except ‘Lock screen for cleaning’.
                            Note: Button ‘Lock screen for cleaning’ is not controlled by ETCS onboard
            Test Step Comment: (1) MMI_gen 1316 (partly: disabled state in Table 23, Idle state);
                                (2) MMI_gen 1316 (partly: enable state in Table 23, Idle state); 
            */
            XML_10_1_a("Settings");

            MakeTestStepHeader(3, UniqueIdentifier++,
                "Close the Setting window. Cabin A is activated. Perform SoM until select and confirm Level 1",
                "Main menu window is displayed");
            /*
            Test Step 3
            Action: Close the Setting window. Cabin A is activated. Perform SoM until select and confirm Level 1
            Expected Result: Main menu window is displayed
            */
            DmiActions.ShowInstruction(this, "Close the Setting window.");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Main menu window.");

            MakeTestStepHeader(4, UniqueIdentifier++,
                "Use the test script file 10_1_b.xml to disable and enable button via EVC-30 with",
                "(1) All buttons in Main menu window are disabled. 10 seconds later");
            /*
            Test Step 4
            Action: Use the test script file 10_1_b.xml to disable and enable button via EVC-30 with
                    Packet 1 (disable all button) all bit of variable MMI_Q_REQUEST_ENABLE = ‘0’ and MMI_NID_WINDOW = 1
                    Packet 2 (enable all button) Send EVC-30 with all bit of variable MMI_Q_REQUEST_ENABLE = ‘1’ and MMI_NID_WINDOW = 1
            Expected Result: (1) All buttons in Main menu window are disabled. 10 seconds later
                            (2) All buttons in Main menu window are enabled
            Test Step Comment: (1) MMI_gen 1316 (partly: disabled state in Table 23, Active state);
                                (2) MMI_gen 1316 (partly: enable state in Table 23, Active state); 
            */
            XML_10_1_b();

            MakeTestStepHeader(5, UniqueIdentifier++, "Close the Main window", "(1) The Default window is displayed");
            /*
            Test Step 5
            Action: Close the Main window
            Expected Result: (1) The Default window is displayed
            Test Step Comment: (1) MMI_gen 5647
            */
            DmiActions.ShowInstruction(this, "Close the Main window");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Default window.");

            MakeTestStepHeader(6, UniqueIdentifier++,
                "Press the Override button. Repeat action step 2 with Override window",
                "Override menu window is displayed.");
            /*
            Test Step 6
            Action: Press the Override button. Repeat action step 2 with Override window
            Expected Result: Override menu window is displayed.
                            (1) All buttons in Override menu window are disabled. 10 seconds later
                            (2) All buttons in Override menu window are enabled
            Test Step Comment: (1) MMI_gen 1316 (partly: disabled state in Table 23, Active state);
                                (2) MMI_gen 1316 (partly: enable state in Table 23, Active state); 
            */
            DmiActions.ShowInstruction(this, "Press the ‘Override’ button");
            XML_10_1_a("Override menu", false);

            MakeTestStepHeader(7, UniqueIdentifier++, "Close the Override menu window",
                "(1) The Default window is displayed");
            /*
            Test Step 7
            Action: Close the Override menu window
            Expected Result: (1) The Default window is displayed
            Test Step Comment: (1) MMI_gen 5647
            */
            DmiActions.ShowInstruction(this, "Close the Override menu window");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Default window.");

            MakeTestStepHeader(8, UniqueIdentifier++, "Press ‘Spec’ button. Repeat action step 2 with Special window",
                "Special menu window is displayed.");
            /*
            Test Step 8
            Action: Press ‘Spec’ button. Repeat action step 2 with Special window
            Expected Result: Special menu window is displayed.
                            (1) All buttons in Special menu window are disabled. 10 seconds later
                            (2) All buttons in Special menu window are enabled
            Test Step Comment: (1) MMI_gen 1316 (partly: disabled state in Table 23, Active state);
                                (2) MMI_gen 1316 (partly: enable state in Table 23, Active state); 
            */
            DmiActions.ShowInstruction(this, "Press the ‘Spec’ button");
            XML_10_1_a("Special menu", false);

            MakeTestStepHeader(9, UniqueIdentifier++, "Close the Special menu window",
                "(1) The Default window is displayed");
            /*
            Test Step 9
            Action: Close the Special menu window
            Expected Result: (1) The Default window is displayed
            Test Step Comment: (1) MMI_gen 5647
            */
            DmiActions.ShowInstruction(this, "Close the Special menu window");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Default window.");

            MakeTestStepHeader(10, UniqueIdentifier++,
                "Press ‘Settings’ button. Repeat action step 2 with Settings window",
                "Settings menu window is displayed.");
            /*
            Test Step 10
            Action: Press ‘Settings’ button. Repeat action step 2 with Settings window
            Expected Result: Settings menu window is displayed.
                    (1) All buttons in Settings menu window are disabled, except ‘Lock screen for cleaning’. 10 seconds later
                    (2) All buttons in Settings menu window are enabled, except ‘Lock screen for cleaning’.
                    Note: Button ‘Lock screen for cleaning’ is not controlled by ETCS onboard
            Test Step Comment: (1) MMI_gen 1316 (partly: disabled state in Table 23, Active state);
                                (2) MMI_gen 1316 (partly: enable state in Table 23, Active state); 
            */
            DmiActions.ShowInstruction(this, "Press the ‘Settings’ button");
            XML_10_1_a("Settings menu");

            MakeTestStepHeader(11, UniqueIdentifier++, "Press ‘Brake’ button. Repeat action step 2 with Brake window",
                "Brake menu window is displayed.");
            /*
            Test Step 11
            Action: Press ‘Brake’ button. Repeat action step 2 with Brake window
            Expected Result: Brake menu window is displayed.
                            (1) All buttons in Brake menu window are disabled. 10 seconds later
                            (2) All buttons in Brake menu window are enabled
            Test Step Comment: (1) MMI_gen 1316 (partly: disabled state in Table 23, Active state);
                                (2) MMI_gen 1316 (partly: enable state in Table 23, Active state); 
            */
            DmiActions.ShowInstruction(this, "Press the ‘Brake’ button");
            XML_10_1_a("Brake menu", false);

            MakeTestStepHeader(12, UniqueIdentifier++,
                "Press ‘Test’ button. Repeat action step 2 with Brake Test window",
                "Brake Test menu window is displayed.");
            /*
            Test Step 12
            Action: Press ‘Test’ button. Repeat action step 2 with Brake Test window
            Expected Result: Brake Test menu window is displayed.
                            (1) All buttons in Brake Test menu window are disabled. 10 seconds later
                            (2) All buttons in Brake Test menu window are enabled
            Test Step Comment: (1) MMI_gen 1316 (partly: disabled state in Table 23, Active state);
                                (2) MMI_gen 1316 (partly: enable state in Table 23, Active state); 
            */
            DmiActions.ShowInstruction(this, "Press the ‘Test’ button");
            XML_10_1_a("Brake Test menu", false);

            MakeTestStepHeader(13, UniqueIdentifier++, "Close the Brake Test window and Brake window",
                "(1) The Settings window is displayed");
            /*
            Test Step 13
            Action: Close the Brake Test window and Brake window
            Expected Result: (1) The Settings window is displayed
            Test Step Comment: (1) MMI_gen 5647
            */
            DmiActions.ShowInstruction(this, "Close the Brake Test and Brake window");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Settings window.");

            MakeTestStepHeader(14, UniqueIdentifier++, "Close the Settings menu window",
                "(1) The Default window is displayed");
            /*
            Test Step 14
            Action: Close the Settings menu window
            Expected Result: (1) The Default window is displayed
            Test Step Comment: (1) MMI_gen 5647
            */
            DmiActions.ShowInstruction(this, "Close the Settings window");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Default window.");

            MakeTestStepHeader(15, UniqueIdentifier++,
                "Press the Main button. Press the Level button. Select and confirm Level 2.",
                "RBC contact window is displayed");
            /*
            Test Step 15
            Action: Press the Main button. Press the Level button. Select and confirm Level 2.
                    Repeat action step 2 with RBC contact window
            Expected Result: RBC contact window is displayed
                            (1) All buttons in RBC contact window are disabled. 10 seconds later
                            (2) All buttons in RBC contact window are enabled
            Test Step Comment: (1) MMI_gen 1316 (partly: disabled state in Table 23, Active state);
                                (2) MMI_gen 1316 (partly: enable state in Table 23, Active state); 
            */
            DmiActions.ShowInstruction(this, "Press the ‘Main’ button, then press the ‘Level’ button");

            EVC20_MMISelectLevel.MMI_Q_CLOSE_ENABLE = Variables.MMI_Q_CLOSE_ENABLE.Disabled;

            EVC20_MMISelectLevel.MMI_Q_LEVEL_NTC_ID = new[] {Variables.MMI_Q_LEVEL_NTC_ID.ETCS_Level};
            EVC20_MMISelectLevel.MMI_M_CURRENT_LEVEL = new[] {Variables.MMI_M_CURRENT_LEVEL.LastUsedLevel};
            EVC20_MMISelectLevel.MMI_M_LEVEL_FLAG = new[] {Variables.MMI_M_LEVEL_FLAG.MarkedLevel};
            EVC20_MMISelectLevel.MMI_M_INHIBITED_LEVEL = new[] {Variables.MMI_M_INHIBITED_LEVEL.NotInhibited};
            EVC20_MMISelectLevel.MMI_M_INHIBIT_ENABLE = new[] {Variables.MMI_M_INHIBIT_ENABLE.AllowedForInhibiting};
            EVC20_MMISelectLevel.MMI_M_LEVEL_NTC_ID = new[] {Variables.MMI_M_LEVEL_NTC_ID.L2};
            EVC20_MMISelectLevel.Send();

            DmiActions.ShowInstruction(this, "Select and confirm Level 2");

            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Level = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_LEVEL.L2;
            EVC22_MMICurrentRBC.MMI_Q_CLOSE_ENABLE = Variables.MMI_Q_CLOSE_ENABLE.Enabled;
            EVC22_MMICurrentRBC.MMI_NID_WINDOW = 5;
            EVC22_MMICurrentRBC.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the RBC contact window.");

            XML_10_1_a("RBC Contact", false);

            MakeTestStepHeader(16, UniqueIdentifier++, "Close the RBC contact window",
                "(1) The Main menu window is displayed");
            /*
            Test Step 16
            Action: Close the RBC contact window
            Expected Result: (1) The Main menu window is displayed
            Test Step Comment: (1) MMI_gen 5647
            */
            DmiActions.ShowInstruction(this, "Close the RBC contact window");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Main menu window.");

            MakeTestStepHeader(17, UniqueIdentifier++, "End of test", "");

            /*
            Test Step 17
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }

        #region Send_XML_10_1_a_DMI_Test_Specification

        private void XML_10_1_a(string windowName, bool showLock = true)
        {
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW =
                EVC30_MMIRequestEnable.WindowID.No_window_specified; // No window specified

            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.None;
            EVC30_MMIRequestEnable.Send();

            if (showLock)
            {
                WaitForVerification(
                    string.Format("Check that all but one of the buttons in the {0} window are disabled (displayed ",
                        windowName) +
                    "with a border with dark-grey text) and the following:" + Environment.NewLine +
                    Environment.NewLine +
                    @"1. The ‘Lock screen for cleaning’ button is not disabled.");
            }
            else
            {
                WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                    string.Format(
                                        "All the buttons in the {0} window are disabled (displayed with a border with dark-grey text).",
                                        windowName));
            }

            // Reduced waiting time to speed up testing
            Wait_Realtime(3000);

            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.No_window_specified;
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_LOW = true;
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
                                                               EVC30_MMIRequestEnable.EnabledRequests.StartBrakeTest |
                                                               EVC30_MMIRequestEnable.EnabledRequests
                                                                   .EnableWheelDiameter |
                                                               EVC30_MMIRequestEnable.EnabledRequests.EnableDoppler |
                                                               EVC30_MMIRequestEnable.EnabledRequests
                                                                   .EnableBrakePercentage;
            EVC30_MMIRequestEnable.Send();

            if (showLock)
            {
                WaitForVerification(
                    string.Format("Check that all but one of the buttons in the {0} window are enabled with ",
                        windowName) +
                    "the exception of the following:" + Environment.NewLine + Environment.NewLine +
                    @"1. The ‘Lock screen for cleaning’ button.");
            }
            else
            {
                WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                    string.Format("1. All the buttons in the {0} window are enabled.", windowName));
            }
        }

        #endregion

        #region Send_XML_10_1_b_DMI_Test_Specification

        private void XML_10_1_b()
        {
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Main; // Enable Main window

            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.None;
            EVC30_MMIRequestEnable.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. All the buttons in the Main menu window are disabled (displayed with a border with dark-grey text).");

            // Reduced waiting time to speed up testing
            Wait_Realtime(3000);

            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Main; // Enable Main window
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
                                                               EVC30_MMIRequestEnable.EnabledRequests.StartBrakeTest |
                                                               EVC30_MMIRequestEnable.EnabledRequests
                                                                   .EnableWheelDiameter |
                                                               EVC30_MMIRequestEnable.EnabledRequests.EnableDoppler |
                                                               EVC30_MMIRequestEnable.EnabledRequests
                                                                   .EnableBrakePercentage;
            EVC30_MMIRequestEnable.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. All the buttons in the Main window are enabled.");
        }

        #endregion
    }
}
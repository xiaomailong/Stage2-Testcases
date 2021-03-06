using System;
using Testcase.Telegrams.DMItoEVC;
using Testcase.Telegrams.EVCtoDMI;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 27.20.2 Override window in SB mode
    /// TC-ID: 22.20.2
    /// 
    /// This test case verifies the state of ‘EOA’ buttion which enabled in SB mode Level2/3 and verifies the correctness of enable/disable state when DMI received packet information EVC-30.
    /// 
    /// Tested Requirements:
    /// MMI_gen 8415 (partly: touch screen, label “EOA”); MMI_gen 11225;
    /// 
    /// Scenario:
    /// Open RBC Data window. Then, Enter a specific information.Close Main window.Open Override window. Then, verifies the state of EOA button.Open Level window. Then, select and confirm Level 1.Open Override window. Then, verifies the state of EOA button.
    /// 
    /// Used files:
    /// 22_20_2.utt
    /// </summary>
    public class TC_ID_22_20_2_Override_window : TestcaseBase
    {
        public override bool TestcaseEntryPoint()
        {
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 25749;
            // Testcase entrypoint

            MakeTestStepHeader(1, UniqueIdentifier++, "Press ‘Enter RBC Data’ button", "DMI displays RBC Data window");
            /*
            Test Step 1
            Action: Press ‘Enter RBC Data’ button
            Expected Result: DMI displays RBC Data window
            */
            StartUp();
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Level = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_LEVEL.L2;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.StandBy;

            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Main; // Main window
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.EnterRBCData |
                                                               EVC30_MMIRequestEnable.EnabledRequests.TrainData;
            EVC30_MMIRequestEnable.Send();

            EVC22_MMICurrentRBC.MMI_NID_WINDOW = 5;
            EVC22_MMICurrentRBC.Send();

            DmiActions.ShowInstruction(this, "Press the ‘Enter RBC Data’ button");

            EVC101_MMIDriverRequest.CheckMRequestPressed = Variables.MMI_M_REQUEST.StartRBCdataEntry;

            EVC22_MMICurrentRBC.MMI_NID_WINDOW = 10; // RBC data
            EVC22_MMICurrentRBC.MMI_M_BUTTONS = EVC22_MMICurrentRBC.EVC22BUTTONS.BTN_YES_DATA_ENTRY_COMPLETE;
            EVC22_MMICurrentRBC.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the RBC Data window");

            MakeTestStepHeader(2, UniqueIdentifier++,
                "Enter and confirm the following values, RBC ID= 6996969 RBC Phone number = 0031840880100 Then, press ‘Yes’ button",
                "DMI displays Main window");
            /*
            Test Step 2
            Action: Enter and confirm the following values, RBC ID= 6996969RBC Phone number = 0031840880100Then, press ‘Yes’ button
            Expected Result: DMI displays Main window
            */
            DmiActions.ShowInstruction(this,
                "Enter and confirm the following values: RBC ID = 6996969, RBC Phone number = 0031840880100, then, press the ‘Yes’ button");

            EVC112_MMINewRbcData.MMI_NID_RADIO = 0031840880100;
            EVC112_MMINewRbcData.MMI_NID_RBC = 6996969;
            EVC112_MMINewRbcData.MMI_NID_MN = 0;
            EVC112_MMINewRbcData.MMI_M_BUTTONS = Variables.MMI_M_BUTTONS_RBC_DATA.BTN_ENTER;
            EVC112_MMINewRbcData.CheckPacketContent();

            // Need to close RBC Contact window
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Main;
            EVC30_MMIRequestEnable.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Main window.");

            MakeTestStepHeader(3, UniqueIdentifier++,
                "Perform the following procedure,Press ‘Train data’ button.Enter and validate train data",
                "DMI displays Main window with enabled ‘Start’ button");
            /*
            Test Step 3
            Action: Perform the following procedure,Press ‘Train data’ button.Enter and validate train data
            Expected Result: DMI displays Main window with enabled ‘Start’ button
            */
            DmiActions.ShowInstruction(this, "Press the ‘Train data’ button");

            DmiActions.Send_EVC6_MMICurrentTrainData_FixedDataEntry(this, new[] {"FLU", "RLU", "Rescue"}, 2);

            DmiActions.ShowInstruction(this, "Enter and validate the train data");

            DmiActions.Send_EVC10_MMIEchoedTrainData_FixedDataEntry(this, new[] {"FLU", "RLU", "Rescue"});

            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Main;
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.Start |
                                                               EVC30_MMIRequestEnable.EnabledRequests.EOA |
                                                               EVC30_MMIRequestEnable.EnabledRequests.Level;
            EVC30_MMIRequestEnable.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Main window with an enabled ‘Start’ button.");

            MakeTestStepHeader(4, UniqueIdentifier++, "Press ‘Close’ button", "DMI displays Default window");
            /*
            Test Step 4
            Action: Press ‘Close’ button
            Expected Result: DMI displays Default window
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Default window.");

            MakeTestStepHeader(5, UniqueIdentifier++, "Press ‘Override’ button",
                "Verify the following information,The ‘EOA’ button is in enable state.Use the log file to confirm that DMI receives EVC-30 with with bit No.9 of variable MMI_Q_REQUEST_ENABLE_64 = 1 (Enable Start Override EOA)");
            /*
            Test Step 5
            Action: Press ‘Override’ button
            Expected Result: Verify the following information,The ‘EOA’ button is in enable state.Use the log file to confirm that DMI receives EVC-30 with with bit No.9 of variable MMI_Q_REQUEST_ENABLE_64 = 1 (Enable Start Override EOA)
            Test Step Comment: (1) MMI_gen 8415 (partly: touch screen, label “EOA”);              MMI_gen 11225 (partly: EVC-30, enabled);(2) MMI_gen 11225 (partly: enabled);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press the ‘Override’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Override window with an enabled ‘EOA’ button.");

            MakeTestStepHeader(6, UniqueIdentifier++,
                "Perform the following procedure, Press ‘Close’ button Press ‘Main’ button Press ‘Level’ button Select and confirm Level 1. Press ‘Close’ buttonPress ‘Override’ button",
                "Verify the following information,The ‘EOA’ button is in disable state.Use the log file to confirm that DMI receives EVC-30 with with bit No.9 of variable MMI_Q_REQUEST_ENABLE_64 = 0 (Disable Start Override EOA)");
            /*
            Test Step 6
            Action: Perform the following procedure, Press ‘Close’ buttonPress ‘Main’ buttonPress ‘Level’ buttonSelect and confirm Level 1.Press ‘Close’ buttonPress ‘Override’ button
            Expected Result: Verify the following information,The ‘EOA’ button is in disable state.Use the log file to confirm that DMI receives EVC-30 with with bit No.9 of variable MMI_Q_REQUEST_ENABLE_64 = 0 (Disable Start Override EOA)
            Test Step Comment: (1) MMI_gen 8415 (partly: touch screen, label “EOA”);              MMI_gen 11225 (partly: EVC-30, disabled);(2) MMI_gen 11225 (partly: disabled);
            */
            DmiActions.ShowInstruction(this, "Press the ‘Close’ button, then the ‘Main’ button, then the ‘Level’ button.");

            EVC101_MMIDriverRequest.CheckMRequestReleased = Variables.MMI_M_REQUEST.ChangeLevel;

            EVC20_MMISelectLevel.MMI_Q_CLOSE_ENABLE = Variables.MMI_Q_CLOSE_ENABLE.Enabled;
            EVC20_MMISelectLevel.MMI_Q_LEVEL_NTC_ID = new [] {Variables.MMI_Q_LEVEL_NTC_ID.ETCS_Level};
            EVC20_MMISelectLevel.MMI_M_CURRENT_LEVEL = new [] {Variables.MMI_M_CURRENT_LEVEL.NotLastUsedLevel};
            EVC20_MMISelectLevel.MMI_M_LEVEL_FLAG = new [] {Variables.MMI_M_LEVEL_FLAG.MarkedLevel};
            EVC20_MMISelectLevel.MMI_M_INHIBITED_LEVEL = new [] {Variables.MMI_M_INHIBITED_LEVEL.NotInhibited};
            EVC20_MMISelectLevel.MMI_M_INHIBIT_ENABLE = new [] {Variables.MMI_M_INHIBIT_ENABLE.AllowedForInhibiting};
            EVC20_MMISelectLevel.MMI_M_LEVEL_NTC_ID = new [] {Variables.MMI_M_LEVEL_NTC_ID.L1};
            EVC20_MMISelectLevel.Send();

            DmiActions.ShowInstruction(this, "Select and confirm Level 1. Press the ‘Close’ button then the ‘Override’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Override window with a disabled ‘EOA’ button.");

            TraceHeader("End of test");

            /*
            Test Step 7
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
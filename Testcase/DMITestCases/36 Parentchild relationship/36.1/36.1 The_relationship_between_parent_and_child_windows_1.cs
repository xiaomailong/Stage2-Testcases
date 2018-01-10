using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BT_Tools;
using BT_CSB_Tools;
using BT_CSB_Tools.Logging;
using BT_CSB_Tools.Utils.Xml;
using BT_CSB_Tools.SignalPoolGenerator.Signals;
using BT_CSB_Tools.SignalPoolGenerator.Signals.MwtSignal;
using BT_CSB_Tools.SignalPoolGenerator.Signals.MwtSignal.Misc;
using BT_CSB_Tools.SignalPoolGenerator.Signals.PdSignal;
using BT_CSB_Tools.SignalPoolGenerator.Signals.PdSignal.Misc;
using CL345;
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
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:

            // Call the TestCaseBase PreExecution
            base.PreExecution();
            // System is powered on.
            DmiActions.Start_ATP();
        }

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
            // Testcase entrypoint

            /*
            Test Step 1
            Action: Activate cabin A
            Expected Result: The Driver ID window is displayed
            */
            // Call generic Action Method
            DmiActions.Activate_Cabin_1(this);
            DmiActions.Set_Driver_ID(this, "1234");

            // Call generic Check Results Method
            DmiExpectedResults.Driver_ID_window_displayed(this);

            /*
            Test Step 2
            Action: Press ‘Settings’ button
            Expected Result: The Settings window is displayed
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Confirm the Driver ID, then press the ‘Settings’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Settings window.");

            /*
            Test Step 3
            Action: Press ‘Close’ button
            Expected Result: Verify that the Settings window is closed. The Driver ID window is displayed
            Test Step Comment: MMI_gen 8787 (partly: Close the Settings window);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button");

            EVC14_MMICurrentDriverID.MMI_X_DRIVER_ID = "1";
            EVC14_MMICurrentDriverID.MMI_Q_ADD_ENABLE = EVC14_MMICurrentDriverID.MMI_Q_ADD_ENABLE_BUTTONS.TRN;
            EVC14_MMICurrentDriverID.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI closes the Settings window and displays the Driver ID window.");
            /*
            Test Step 4
            Action: Press ‘TRN’ button
            Expected Result: The Train Running Number window is displayed
            */
            DmiActions.ShowInstruction(this, @"Press ‘TRN’ button");

            EVC16_CurrentTrainNumber.TrainRunningNumber = 1;
            EVC16_CurrentTrainNumber.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Train Running Number window.");

            /*
            Test Step 5
            Action: Press ‘Close’ button
            Expected Result: Verify that the Train Running Number window is closed. The Driver ID window is displayed
            Test Step Comment: MMI_gen 8787 (partly: Close the Train Running Number window);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button");
            EVC14_MMICurrentDriverID.MMI_X_DRIVER_ID = "";
            EVC14_MMICurrentDriverID.MMI_Q_ADD_ENABLE = EVC14_MMICurrentDriverID.MMI_Q_ADD_ENABLE_BUTTONS.Settings |
                                                        EVC14_MMICurrentDriverID.MMI_Q_ADD_ENABLE_BUTTONS.TRN;
            EVC14_MMICurrentDriverID.MMI_Q_CLOSE_ENABLE = Variables.MMI_Q_CLOSE_ENABLE.Enabled;
            EVC14_MMICurrentDriverID.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI closes the Train Running Number window and displays the Driver ID window.");

            /*
            Test Step 6
            Action: Enter Driver ID and perform brake test
            Expected Result: The Level window is displayed
            */
            DmiActions.ShowInstruction(this, @"Enter Driver ID and perform brake test");

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

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Level window.");

            /*
            Test Step 7
            Action: Select and confirm Level 1
            Expected Result: The Main window is displayed
            */
            DmiActions.ShowInstruction(this, @"Select and confirm Level 1");

            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Main; // Main
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.DriverID |
                                                               EVC30_MMIRequestEnable.EnabledRequests.TrainData |
                                                               EVC30_MMIRequestEnable.EnabledRequests.Level;
            EVC30_MMIRequestEnable.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Main window.");

            /*
            Test Step 8
            Action: Press ‘Driver ID’ button
            Expected Result: The Driver ID window is displayed. The ‘Close’ button is presented as enabled state
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press the ‘Driver ID’ button");

            EVC14_MMICurrentDriverID.MMI_X_DRIVER_ID = "";
            EVC14_MMICurrentDriverID.MMI_Q_ADD_ENABLE = EVC14_MMICurrentDriverID.MMI_Q_ADD_ENABLE_BUTTONS.Settings |
                                                        EVC14_MMICurrentDriverID.MMI_Q_ADD_ENABLE_BUTTONS.TRN;
            EVC14_MMICurrentDriverID.MMI_Q_CLOSE_ENABLE = Variables.MMI_Q_CLOSE_ENABLE.Enabled;
            EVC14_MMICurrentDriverID.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Driver ID window." + Environment.NewLine +
                                "2. The ‘Close’ button is displayed enabled.");

            /*
            Test Step 9
            Action: Close the Driver ID window
            Expected Result: Verify that the Driver ID window is closed. The Main window is displayed
            Test Step Comment: MMI_gen 8785 (partly: Close the Driver ID window);
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button in the Driver ID window");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI closes the Driver ID window and displays the Main window.");

            /*
            Test Step 10
            Action: Press ‘Train data’ button
            Expected Result: The Train data window is displayed. The ‘Close’ button is presented as enabled state
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press the ‘Train data’ button");

            DmiActions.Send_EVC6_MMICurrentTrainData(Variables.MMI_M_DATA_ENABLE.TrainSetID |
                                                     Variables.MMI_M_DATA_ENABLE.TrainCategory |
                                                     Variables.MMI_M_DATA_ENABLE.TrainLength |
                                                     Variables.MMI_M_DATA_ENABLE.BrakePercentage |
                                                     Variables.MMI_M_DATA_ENABLE.MaxTrainSpeed |
                                                     Variables.MMI_M_DATA_ENABLE.AxleLoadCategory |
                                                     Variables.MMI_M_DATA_ENABLE.Airtightness,
                100, 200,
                Variables.MMI_NID_KEY.PASS2,
                70,
                Variables.MMI_NID_KEY.CATA,
                0,
                Variables.MMI_NID_KEY_Load_Gauge.G1,
                EVC6_MMICurrentTrainData.MMI_M_BUTTONS_CURRENT_TRAIN_DATA.BTN_YES_DATA_ENTRY_COMPLETE,
                0, 0, new[] {"FLU", "RLU", "Rescue"});

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Train data window." + Environment.NewLine +
                                "2. The ‘Close’ button is displayed enabled.");

            /*
            Test Step 11
            Action: Close the Train data window
            Expected Result: Verify that the Train data window is closed. The Main window is displayed
            Test Step Comment: MMI_gen 8785 (partly: Close the Train data window);
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button in the Train data window");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI closes the Train data window and displays the Main window.");

            /*
            Test Step 12
            Action: Press ‘Train data’ button
            Expected Result: The Train data window is displayed
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press ‘Train data’ button");

            DmiActions.Send_EVC6_MMICurrentTrainData(Variables.MMI_M_DATA_ENABLE.TrainSetID |
                                                     Variables.MMI_M_DATA_ENABLE.TrainCategory |
                                                     Variables.MMI_M_DATA_ENABLE.TrainLength |
                                                     Variables.MMI_M_DATA_ENABLE.BrakePercentage |
                                                     Variables.MMI_M_DATA_ENABLE.MaxTrainSpeed |
                                                     Variables.MMI_M_DATA_ENABLE.AxleLoadCategory |
                                                     Variables.MMI_M_DATA_ENABLE.Airtightness |
                                                     Variables.MMI_M_DATA_ENABLE.LoadingGauge,
                100, 200,
                Variables.MMI_NID_KEY.PASS2,
                70,
                Variables.MMI_NID_KEY.CATA,
                0,
                Variables.MMI_NID_KEY_Load_Gauge.G1,
                EVC6_MMICurrentTrainData.MMI_M_BUTTONS_CURRENT_TRAIN_DATA.BTN_YES_DATA_ENTRY_COMPLETE,
                0, 0, new[] {"FLU", "RLU", "Rescue"});

            DmiExpectedResults.Train_data_window_displayed(this);

            /*
            Test Step 13
            Action: If the train data is fixed, re-select the train type and then press ‘Yes’ button.If the train data is flexible, re-entry all train data and then press ‘Yes’ button
            Expected Result: The Train data validation window is displayed.The ‘Close’ button is presented as enabled state
            */
            DmiActions.ShowInstruction(this,
                "If the train data area fixed, re-select the train type and press the ‘Yes’ button" +
                Environment.NewLine +
                "If the train data are flexible, enter all the train data and press the ‘Yes’ button");

            // Difficult to investigate EVC107 response from DMI to check what was set so display some data
            DmiActions.Send_EVC10_MMIEchoedTrainData(this, Variables.MMI_M_DATA_ENABLE.TrainSetID |
                                                           Variables.MMI_M_DATA_ENABLE.TrainCategory |
                                                           Variables.MMI_M_DATA_ENABLE.TrainLength |
                                                           Variables.MMI_M_DATA_ENABLE.BrakePercentage |
                                                           Variables.MMI_M_DATA_ENABLE.MaxTrainSpeed |
                                                           Variables.MMI_M_DATA_ENABLE.AxleLoadCategory |
                                                           Variables.MMI_M_DATA_ENABLE.Airtightness,
                100, 200,
                Variables.MMI_NID_KEY.PASS2,
                70,
                Variables.MMI_NID_KEY.CATA,
                0,
                Variables.MMI_NID_KEY.G1,
                new[] {"FLU", "RLU", "Rescue"});
            DmiActions.Send_EVC10_MMIEchoedTrainData_FixedDataEntry(this, Variables.paramEvc6FixedTrainsetCaptions);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Train data validation window." + Environment.NewLine +
                                "2. The ‘Close’ button is displayed enabled.");

            /*
            Test Step 14
            Action: Press ‘Close’ button
            Expected Result: Verify that the Train data validation window is closed. The Main window is displayed
            Test Step Comment: MMI_gen 8785 (partly: Close the Train data validation window);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button in the Train data validation window");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI closes the Train data validation window and displays the Main window.");

            /*
            Test Step 15
            Action: Press ‘Level’ button
            Expected Result: The Level window is displayed. The ‘Close’ button is presented as enabled state
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press the ‘Level’ button");

            EVC20_MMISelectLevel.MMI_Q_CLOSE_ENABLE = Variables.MMI_Q_CLOSE_ENABLE.Enabled;
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


            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Level window." + Environment.NewLine +
                                "2. The ‘Close’ button is displayed enabled.");

            /*
            Test Step 16
            Action: Close the Level window
            Expected Result: Verify that the Level window is closed. The Main window is displayed
            Test Step Comment: MMI_gen 8785 (partly: Close the Level window);
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button in the Level window");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI closes the Level window and displays the Main window.");

            /*
            Test Step 17
            Action: Press ‘Train running number’ button
            Expected Result: The Train Running Number window is displayed. The ‘Close’ button is presented as enabled state
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Train Running Number’ button");

            EVC16_CurrentTrainNumber.TrainRunningNumber = 1;
            EVC16_CurrentTrainNumber.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Train Running Number window." + Environment.NewLine +
                                "2. The ‘Close’ button is displayed enabled.");

            /*
            Test Step 18
            Action: Close the Train Running Number window
            Expected Result: Verify that the Train Running Number window is closed. The Main window is displayed
            Test Step Comment: MMI_gen 8785 (partly: Close the Train Running Number window);
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button in the  Train Running Number window");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI closes the  Train Running Number window and displays the Main window.");

            /*
            Test Step 19
            Action: Press ‘Close’ button
            Expected Result: Verify that the Main window is closed. The Default window is displayed
            Test Step Comment: MMI_gen 8785 (partly: Close the Main window);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press ‘Close’ button");
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI closes the  Main window and displays the Default window.");

            /*
            Test Step 20
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
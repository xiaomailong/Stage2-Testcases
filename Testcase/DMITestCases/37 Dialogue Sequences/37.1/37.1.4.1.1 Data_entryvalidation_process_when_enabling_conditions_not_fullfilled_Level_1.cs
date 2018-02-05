using System;
using Testcase.Telegrams.EVCtoDMI;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 37.1.4.1.1 Data entry/validation process when enabling conditions not fullfilled: Level 1
    /// TC-ID: 34.1.4
    /// 
    /// This test case verifies the dialogue sequences when the button-enabling conditions of the concerned windows are not fulfilled and the display of the ‘Close’ button on the ‘Override’, ‘Special’, ‘Adhesion’ and ‘SR speed/distance’ windows.
    /// 
    /// Tested Requirements:
    /// MMI_gen 8868 (partly: Driver ID, Level, Train data, Train data validaion, Train running number, Language, Brightness, Volume, Adhesion, SR speed/distance); MMI_gen 11283 (partly: Driver ID, Level, Train data, Train data validaion, Train running number, Language, Brightness, Volume, Adhesion, SR speed/distance); MMI_gen 9199 (partly: Adhesion, Special, SR speed/distance); MMI_gen 9179; MMI_gen 3374 (partly: NEGATIVE, close by ETCS OB);
    /// 
    /// Scenario:
    /// SoM is performed until level 1 (mode SB) is confirmed. (Start-up Dialogue Sequence)Level 1 (mode SB) is entered and confirmed again. (After Start-up Dialogue Sequence)A concerned window is opened. Then, the driver drives the train forward without movement authorities.The concerned window is verified.SoM is completed in mode SR, level 1.The ‘SR speed/distance’ window is verified.Note: Please see more detail of enabling condition in chapter 8.2 of requirement specificaiton document. The following windows are concerned:Driver ID windowLevel windowTrain data windowTrain data validation windowLanguage windowBrightness windowVolumn windowSR speed/distance windowAdhesion window
    /// 
    /// Used files:
    /// N/A
    /// </summary>
    public class TC_34_1_4_Dialogue_Sequences : TestcaseBase
    {

        public override bool TestcaseEntryPoint()
        {
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 0;
            // Testcase entrypoint

            StartUp();
            // Test system is powered on Cabin A is activated.Enter Driver ID and perform brake test.Level 1 is entered and confirmed.
            DmiActions.Complete_SoM_L1_SB(this);

            MakeTestStepHeader(1, UniqueIdentifier++, "Press ‘Driver ID’ button", "DMI displays Driver ID window");
            /*
            Test Step 1
            Action: Press ‘Driver ID’ button
            Expected Result: DMI displays Driver ID window
            */
            DmiActions.ShowInstruction(this, @"Press ‘Driver ID’ button");
            DmiActions.Set_Driver_ID(this, "");

            DmiExpectedResults.Driver_ID_window_displayed(this);

            MakeTestStepHeader(2, UniqueIdentifier++,
                "Perform the following procedure,Drive the train forward until the brake is appliedStop driving the trainAcknowledge the ‘Brake intervention’ symbol by pressing area E1",
                "Verify the following information,DMI closes the Drive ID window and displays Main window instead.Use the log file to confirm that DMI receives packet information [MMI_ENABLE_REQUEST (EVC-30)] with variable MMI_Q_REQUEST_ENABLE_64 (#0) = 0");
            /*
            Test Step 2
            Action: Perform the following procedure,Drive the train forward until the brake is appliedStop driving the trainAcknowledge the ‘Brake intervention’ symbol by pressing area E1
            Expected Result: Verify the following information,DMI closes the Drive ID window and displays Main window instead.Use the log file to confirm that DMI receives packet information [MMI_ENABLE_REQUEST (EVC-30)] with variable MMI_Q_REQUEST_ENABLE_64 (#0) = 0
            Test Step Comment: (1) MMI_gen 8868 (partly: Driver ID);(2) MMI_gen 11283 (partly: Driver ID);
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 5;

            // Spec says set bit 0 to 0 which would disable the Start button ???
            // Enable Level was presumbably meant
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.Level;
            EVC30_MMIRequestEnable.Send();

            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 260;
            EVC8_MMIDriverMessage.Send();
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 0;

            DmiActions.ShowInstruction(this, @"Press area E1 to acknowledge the ‘Brake intervention’ symbol");

            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Main; // Main window
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.Level;
            EVC30_MMIRequestEnable.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI closes the Driver ID window and displays the Main window");

            MakeTestStepHeader(3, UniqueIdentifier++, "Press ‘Level’ button", "DMI displays Level window");
            /*
            Test Step 3
            Action: Press ‘Level’ button
            Expected Result: DMI displays Level window
            */
            DmiActions.ShowInstruction(this, @"Press ‘Level’ button");

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

            DmiExpectedResults.Level_window_displayed(this);

            MakeTestStepHeader(4, UniqueIdentifier++,
                "Perform the following procedure,Drive the train forward until the brake is appliedStop driving the trainAcknowledge the ‘Brake intervention’ symbol by pressing area E1",
                "Verify the following information,DMI closes the Level window and displays Main window instead.Use the log file to confirm that DMI receives packet information [MMI_ENABLE_REQUEST (EVC-30)] with variable MMI_Q_REQUEST_ENABLE_64 (#3) = 0");
            /*
            Test Step 4
            Action: Perform the following procedure,Drive the train forward until the brake is appliedStop driving the trainAcknowledge the ‘Brake intervention’ symbol by pressing area E1
            Expected Result: Verify the following information,DMI closes the Level window and displays Main window instead.Use the log file to confirm that DMI receives packet information [MMI_ENABLE_REQUEST (EVC-30)] with variable MMI_Q_REQUEST_ENABLE_64 (#3) = 0
            Test Step Comment: (1) MMI_gen 8868 (partly: Level);(2) MMI_gen 11283 (partly: Level);
            */
            // Spec wants EVC30 with enable high bit#0 = 0 (Start off) 

            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 0;

            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 260;
            EVC8_MMIDriverMessage.Send(); // send out brake intervention symbol

            DmiActions.ShowInstruction(this, @"Press area E1 to acknowledge the ‘Brake intervention’ symbol");

            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Close_current_return_to_parent;
            EVC30_MMIRequestEnable.Send();

            EVC30_MMIRequestEnable.SendBlank(); // bit#0 (Start) now disabled
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Main;
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.TrainData;
            EVC30_MMIRequestEnable.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI closes the Level window and displays the Main window.");

            MakeTestStepHeader(5, UniqueIdentifier++, "Press ‘Train data’ button", "DMI displays Train data window");
            /*
            Test Step 5
            Action: Press ‘Train data’ button
            Expected Result: DMI displays Train data window
            */
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

            // Call generic Check Results Method
            DmiExpectedResults.Train_data_window_displayed(this);

            MakeTestStepHeader(6, UniqueIdentifier++,
                "Perform the following procedure,Drive the train forward until the brake is appliedStop driving the trainAcknowledge the ‘Brake intervention’ symbol by pressing area E1",
                "Verify the following information,DMI closes the Train data window and displays Main window instead.Use the log file to confirm that DMI receives packet information [MMI_ENABLE_REQUEST (EVC-30)] with variable MMI_Q_REQUEST_ENABLE_64 (#2) = 0");
            /*
            Test Step 6
            Action: Perform the following procedure,Drive the train forward until the brake is appliedStop driving the trainAcknowledge the ‘Brake intervention’ symbol by pressing area E1
            Expected Result: Verify the following information,DMI closes the Train data window and displays Main window instead.Use the log file to confirm that DMI receives packet information [MMI_ENABLE_REQUEST (EVC-30)] with variable MMI_Q_REQUEST_ENABLE_64 (#2) = 0
            Test Step Comment: (1) MMI_gen 8868 (partly: Train data); (2) MMI_gen 11283 (partly: train data); MMI_gen 3374 (partly: NEGATIVE, close by ETCS OB);
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 5;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 260;
            EVC8_MMIDriverMessage.Send(); // send out brake intervention symbol
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 0;
            DmiActions.ShowInstruction(this, @"Press area E1 to acknowledge the ‘Brake intervention’ symbol");
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Close_current_return_to_parent;
            EVC30_MMIRequestEnable.Send();

            // Spec says set bit 2 to 0 which would disable the TrainData button ???
            // Enable was presumbably meant
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Main; // Main window
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.TrainData;
            EVC30_MMIRequestEnable.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI closes the Train data window and displays the Main window.");

            MakeTestStepHeader(7, UniqueIdentifier++,
                "Perform the following procedure,Press ‘Train data’ button.Enter and confirm all value of train data window.Press ‘Yes’ button",
                "DMI displays Train data validation window");
            /*
            Test Step 7
            Action: Perform the following procedure,Press ‘Train data’ button.Enter and confirm all value of train data window.Press ‘Yes’ button
            Expected Result: DMI displays Train data validation window
            */
            DmiActions.ShowInstruction(this, @"Press ‘Train data’ button.");

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

            DmiActions.ShowInstruction(this,
                "Confirm all values in the train data window, then press the ‘Yes’ button");

            //DmiActions.Send_EVC10_MMIEchoedTrainData(Variables.MMI_M_DATA_ENABLE.TrainSetID |
            //                                         Variables.MMI_M_DATA_ENABLE.TrainCategory |
            //                                         Variables.MMI_M_DATA_ENABLE.TrainLength |
            //                                         Variables.MMI_M_DATA_ENABLE.BrakePercentage |
            //                                         Variables.MMI_M_DATA_ENABLE.MaxTrainSpeed |
            //                                         Variables.MMI_M_DATA_ENABLE.AxleLoadCategory |
            //                                         Variables.MMI_M_DATA_ENABLE.Airtightness |
            //                                         Variables.MMI_M_DATA_ENABLE.LoadingGauge,
            //                                         100, 200,
            //                                         Variables.MMI_NID_KEY.PASS2,
            //                                         100,
            //                                         Variables.MMI_NID_KEY.CATA,
            //                                         0,
            //                                         Variables.MMI_NID_KEY.G1,
            //                                         0, 0, new[] { "FLU", "RLU", "Rescue" }, null);
            DmiActions.Send_EVC10_MMIEchoedTrainData_FixedDataEntry(this, Variables.paramEvc6FixedTrainsetCaptions);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays Train data validation window.");

            MakeTestStepHeader(8, UniqueIdentifier++,
                "Perform the following procedure,Drive the train forward until the brake is appliedStop driving the trainAcknowledge the ‘Brake intervention’ symbol by pressing area E1",
                "Verify the following information,DMI closes the Train data validation window and displays Main window instead.Use the log file to confirm that DMI receives packet information [MMI_ENABLE_REQUEST (EVC-30)] with variable MMI_Q_REQUEST_ENABLE_64 (#2) = 0");
            /*
            Test Step 8
            Action: Perform the following procedure,Drive the train forward until the brake is appliedStop driving the trainAcknowledge the ‘Brake intervention’ symbol by pressing area E1
            Expected Result: Verify the following information,DMI closes the Train data validation window and displays Main window instead.Use the log file to confirm that DMI receives packet information [MMI_ENABLE_REQUEST (EVC-30)] with variable MMI_Q_REQUEST_ENABLE_64 (#2) = 0
            Test Step Comment: (1) MMI_gen 8868 (partly: Train data validation);(2) MMI_gen 11283 (partly: train data validation);
            */
            // see above discussion...
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 5;

            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 260;
            EVC8_MMIDriverMessage.Send(); // send out brake intervention symbol

            DmiActions.ShowInstruction(this, @"Press area E1 to acknowledge the ‘Brake intervention’ symbol");

            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 0;

            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Close_current_return_to_parent;
            EVC30_MMIRequestEnable.Send();

            // Spec says set bit 2 to 0 which would disable the TrainData button ???
            // Enable was presumbably meant
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.TrainData;
            EVC30_MMIRequestEnable.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI closes the Train data validation window and displays the Main window.");

            MakeTestStepHeader(9, UniqueIdentifier++,
                "Perform the following procedure,Press ‘Train data’ button.Enter and confirm all value of train data window.Press ‘Yes’ button.At the train data validation window, press ‘Yes’ button.Confirm entered data by pressing an input field",
                "DMI displays Train Running Number window");
            /*
            Test Step 9
            Action: Perform the following procedure,Press ‘Train data’ button.Enter and confirm all value of train data window.Press ‘Yes’ button.At the train data validation window, press ‘Yes’ button.Confirm entered data by pressing an input field
            Expected Result: DMI displays Train Running Number window
            */
            DmiActions.ShowInstruction(this, @"Press ‘Train data’ button.");

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

            DmiActions.ShowInstruction(this, @"Confirm all values in the train data window. Press ‘Yes’ button");

            //DmiActions.Send_EVC10_MMIEchoedTrainData(Variables.MMI_M_DATA_ENABLE.TrainSetID |
            //                                         Variables.MMI_M_DATA_ENABLE.TrainCategory |
            //                                         Variables.MMI_M_DATA_ENABLE.TrainLength |
            //                                         Variables.MMI_M_DATA_ENABLE.BrakePercentage |
            //                                         Variables.MMI_M_DATA_ENABLE.MaxTrainSpeed |
            //                                         Variables.MMI_M_DATA_ENABLE.AxleLoadCategory |
            //                                         Variables.MMI_M_DATA_ENABLE.Airtightness |
            //                                         Variables.MMI_M_DATA_ENABLE.LoadingGauge,
            //                                         100, 200,
            //                                         Variables.MMI_NID_KEY.PASS2,
            //                                         100,
            //                                         Variables.MMI_NID_KEY.CATA,
            //                                         0,
            //                                         Variables.MMI_NID_KEY.G1,
            //                                         0, 0, new[] { "FLU", "RLU", "Rescue" }, null);
            DmiActions.Send_EVC10_MMIEchoedTrainData_FixedDataEntry(this, Variables.paramEvc6FixedTrainsetCaptions);

            DmiActions.ShowInstruction(this, @"Press ‘Yes’ button in the train data validation window");

            EVC16_CurrentTrainNumber.TrainRunningNumber = 1;
            EVC16_CurrentTrainNumber.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays Train Runing Number window.");

            MakeTestStepHeader(10, UniqueIdentifier++,
                "Perform the following procedure,Drive the train forward until the brake is appliedStop driving the trainAcknowledge the ‘Brake intervention’ symbol by pressing area E1",
                "Verify the following information,DMI closes the Train Running Number window and displays Main window instead.Use the log file to confirm that DMI receives packet information [MMI_ENABLE_REQUEST (EVC-30)] with variable MMI_Q_REQUEST_ENABLE_64 (#4) = 0");
            /*
            Test Step 10
            Action: Perform the following procedure,Drive the train forward until the brake is appliedStop driving the trainAcknowledge the ‘Brake intervention’ symbol by pressing area E1
            Expected Result: Verify the following information,DMI closes the Train Running Number window and displays Main window instead.Use the log file to confirm that DMI receives packet information [MMI_ENABLE_REQUEST (EVC-30)] with variable MMI_Q_REQUEST_ENABLE_64 (#4) = 0
            Test Step Comment: (1) MMI_gen 8868 (partly: Train running number);(2) MMI_gen 11283 (partly: train running number);
            */
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 260;
            EVC8_MMIDriverMessage.Send(); // send out brake intervention symbol

            DmiActions.ShowInstruction(this, @"Press area E1 to acknowledge the ‘Brake intervention’ symbol");

            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Close_current_return_to_parent;
            EVC30_MMIRequestEnable.Send();

            // Spec says set bit 4 to 0 which would disable the TrainRunningNumber button ???
            // Enable was presumbably meant
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH =
                EVC30_MMIRequestEnable.EnabledRequests.TrainRunningNumber;
            EVC30_MMIRequestEnable.Send();
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 0;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI closes the Train Running Number window and displays the Main window.");

            MakeTestStepHeader(11, UniqueIdentifier++,
                "Press ‘Train running number’ button. Then, enter and confirm Train running number",
                "DMI displays Main window");
            /*
            Test Step 11
            Action: Press ‘Train running number’ button. Then, enter and confirm Train running number
            Expected Result: DMI displays Main window
            */
            DmiActions.ShowInstruction(this, @"Press ‘Train Running Number’ button.");

            EVC16_CurrentTrainNumber.TrainRunningNumber = 1;
            EVC16_CurrentTrainNumber.Send();

            DmiActions.ShowInstruction(this, "Enter and confirm Train running number");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Main window.");

            MakeTestStepHeader(12, UniqueIdentifier++,
                "Perform the following procedure, Press ‘Close’ buttonPress ‘Settings’ buttonPress ‘Language’ button",
                "DMI displays Language window");
            /*
            Test Step 12
            Action: Perform the following procedure, Press ‘Close’ buttonPress ‘Settings’ buttonPress ‘Language’ button
            Expected Result: DMI displays Language window
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button, then press the ‘Settings’ button");

            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Settings; // Settings
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.Language;
            EVC30_MMIRequestEnable.Send();

            DmiActions.ShowInstruction(this, @"Press the ‘Language’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Language window.");

            MakeTestStepHeader(13, UniqueIdentifier++,
                "Perform the following procedure,Drive the train forward until the brake is appliedStop driving the trainAcknowledge the ‘Brake intervention’ symbol by pressing area E1",
                "Verify the following information,DMI closes the Language window and displays Settings window instead.Use the log file to confirm that DMI receives packet information [MMI_ENABLE_REQUEST (EVC-30)] with variable MMI_Q_REQUEST_ENABLE_64 (#13) = 0");
            /*
            Test Step 13
            Action: Perform the following procedure,Drive the train forward until the brake is appliedStop driving the trainAcknowledge the ‘Brake intervention’ symbol by pressing area E1
            Expected Result: Verify the following information,DMI closes the Language window and displays Settings window instead.Use the log file to confirm that DMI receives packet information [MMI_ENABLE_REQUEST (EVC-30)] with variable MMI_Q_REQUEST_ENABLE_64 (#13) = 0
            Test Step Comment: (1) MMI_gen 8868 (partly: Language);(2) MMI_gen 11283 (partly: Language);
            */
            // see above discussion...
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 5;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 260;
            EVC8_MMIDriverMessage.Send(); // send out brake intervention symbol

            DmiActions.ShowInstruction(this, @"Press area E1 to acknowledge the ‘Brake intervention’ symbol");

            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Close_current_return_to_parent;
            EVC30_MMIRequestEnable.Send();

            // Spec says set bit 13 to 0 which would disable the Language button - Brightness (#15) in next test???
            // Enable was presumbably meant
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.Brightness;
            EVC30_MMIRequestEnable.Send();

            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 0;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI closes the Language window and displays the Settings window.");

            MakeTestStepHeader(14, UniqueIdentifier++, "Press ‘Brightness’ button", "DMI displays Brighness window");
            /*
            Test Step 14
            Action: Press ‘Brightness’ button
            Expected Result: DMI displays Brighness window
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press ‘Brightness’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Brightness window.");

            MakeTestStepHeader(15, UniqueIdentifier++,
                "Perform the following procedure,Drive the train forward until the brake is appliedStop driving the trainAcknowledge the ‘Brake intervention’ symbol by pressing area E1",
                "Verify the following information,DMI closes the Brightness window and displays Settings window instead.Use the log file to confirm that DMI receives packet information [MMI_ENABLE_REQUEST (EVC-30)] with variable MMI_Q_REQUEST_ENABLE_64 (#15) = 0");
            /*
            Test Step 15
            Action: Perform the following procedure,Drive the train forward until the brake is appliedStop driving the trainAcknowledge the ‘Brake intervention’ symbol by pressing area E1
            Expected Result: Verify the following information,DMI closes the Brightness window and displays Settings window instead.Use the log file to confirm that DMI receives packet information [MMI_ENABLE_REQUEST (EVC-30)] with variable MMI_Q_REQUEST_ENABLE_64 (#15) = 0
            Test Step Comment: (1) MMI_gen 8868 (partly: Brightness);(2) MMI_gen 11283 (partly: Brightness);
            */
            // see above discussion...
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 5;

            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 260;
            EVC8_MMIDriverMessage.Send(); // send out brake intervention symbol

            DmiActions.ShowInstruction(this, @"Press area E1 to acknowledge the ‘Brake intervention’ symbol");

            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Close_current_return_to_parent;
            EVC30_MMIRequestEnable.Send();

            // Spec says set bit 15 to 0 which would disable the Brightness button - Volume (#14) in next test???
            // Enable was presumbably meant
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.Volume;
            EVC30_MMIRequestEnable.Send();

            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 0;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI closes the Brightness window and displays the Settings window.");

            MakeTestStepHeader(16, UniqueIdentifier++, "Press ‘Volume’ button", "DMI displays Volume window");
            /*
            Test Step 16
            Action: Press ‘Volume’ button
            Expected Result: DMI displays Volume window
            */
            DmiActions.ShowInstruction(this, @"Press ‘Volume’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Volume window.");

            MakeTestStepHeader(17, UniqueIdentifier++,
                "Perform the following procedure,Drive the train forward until the brake is appliedStop driving the trainAcknowledge the ‘Brake intervention’ symbol by pressing area E1",
                "Verify the following information,DMI closes the Volume window and displays Settings window instead.Use the log file to confirm that DMI receives packet information [MMI_ENABLE_REQUEST (EVC-30)] with variable MMI_Q_REQUEST_ENABLE_64 (#14) = 0");
            /*
            Test Step 17
            Action: Perform the following procedure,Drive the train forward until the brake is appliedStop driving the trainAcknowledge the ‘Brake intervention’ symbol by pressing area E1
            Expected Result: Verify the following information,DMI closes the Volume window and displays Settings window instead.Use the log file to confirm that DMI receives packet information [MMI_ENABLE_REQUEST (EVC-30)] with variable MMI_Q_REQUEST_ENABLE_64 (#14) = 0
            Test Step Comment: (1) MMI_gen 8868 (partly: volume);(2) MMI_gen 11283 (partly: volume);
            */
            // see above discussion...
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 5;

            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 260;
            EVC8_MMIDriverMessage.Send(); // send out brake intervention symbol

            DmiActions.ShowInstruction(this, @"Press area E1 to acknowledge the ‘Brake intervention’ symbol");

            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Close_current_return_to_parent;
            EVC30_MMIRequestEnable.Send();

            // Spec says set bit 14 to 0 which would disable the Volume button - Adhesion (#10) in next test???
            // Enable was presumbably meant

            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 0;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI closes the Volume window and displays the Settings window.");

            MakeTestStepHeader(18, UniqueIdentifier++,
                "Perform the following procedure,Press ‘Close’ buttonPress ‘Special’ button.Press ‘Adhesion’ button",
                "DMI displays Adhesion window.Verify the Close button on this window is enabling");
            /*
            Test Step 18
            Action: Perform the following procedure,Press ‘Close’ buttonPress ‘Special’ button.Press ‘Adhesion’ button
            Expected Result: DMI displays Adhesion window.Verify the Close button on this window is enabling
            Test Step Comment: MMI_gen 9199 (partly: Adhesion);
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button, then press the ‘Special’ button");

            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Special;
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.Adhesion;
            EVC30_MMIRequestEnable.Send();

            DmiActions.ShowInstruction(this, @"Press ‘Adhesion’ button");
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI closes the Adhesion window." + Environment.NewLine +
                                "2. The ‘Close’ button in the Adhesion window is enabled.");

            MakeTestStepHeader(19, UniqueIdentifier++,
                "Perform the following procedure,Drive the train forward until the brake is appliedStop driving the trainAcknowledge the ‘Brake intervention’ symbol by pressing area E1",
                "Verify the following information,DMI closes the Adhesion window and displays Special window instead.Use the log file to confirm that DMI receives packet information [MMI_ENABLE_REQUEST (EVC-30)] with variable MMI_Q_REQUEST_ENABLE_64 (#10) = 0");
            /*
            Test Step 19
            Action: Perform the following procedure,Drive the train forward until the brake is appliedStop driving the trainAcknowledge the ‘Brake intervention’ symbol by pressing area E1
            Expected Result: Verify the following information,DMI closes the Adhesion window and displays Special window instead.Use the log file to confirm that DMI receives packet information [MMI_ENABLE_REQUEST (EVC-30)] with variable MMI_Q_REQUEST_ENABLE_64 (#10) = 0
            Test Step Comment: (1) MMI_gen 8868 (partly: Adhesion);(2) MMI_gen 11283 (partly: Adhesion);
            */
            // see above discussion...
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 5;

            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 260;
            EVC8_MMIDriverMessage.Send(); // send out brake intervention symbol

            DmiActions.ShowInstruction(this, @"Press area E1 to acknowledge the ‘Brake intervention’ symbol");

            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Close_current_return_to_parent;
            EVC30_MMIRequestEnable.Send();

            // Spec says set bit 10 to 0 which would disable the Adhesion button - Start is enabled (#0) in next test???
            // Enable was presumbably meant

            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 0;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI closes the Adhesion window and displays the Special window.");

            MakeTestStepHeader(20, UniqueIdentifier++,
                "Perform the following procedure,Press ‘Close’ buttonPress ‘Main’ button.Press ‘Start’ button.Acknowledge SR mode.Press ‘Special’ button",
                "DMI displays Special window.Verify the Close button on this window is enabling");
            /*
            Test Step 20
            Action: Perform the following procedure,Press ‘Close’ buttonPress ‘Main’ button.Press ‘Start’ button.Acknowledge SR mode.Press ‘Special’ button
            Expected Result: DMI displays Special window.Verify the Close button on this window is enabling
            Test Step Comment: MMI_gen 9199 (partly: special);
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button, then press the ‘Main’ button");

            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Main; // Main window
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.Start;
            EVC30_MMIRequestEnable.Send();

            DmiActions.ShowInstruction(this, @"Press the ‘Start’ button");

            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 260;
            EVC8_MMIDriverMessage.Send(); // send out ack SR mode

            DmiActions.ShowInstruction(this, @"Acknowledge SR mode, then press the ‘Special’ button.");
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Special window." + Environment.NewLine +
                                "2. The ‘Close’ button in the Special window is enabled.");

            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode =
                EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.StaffResponsible;

            MakeTestStepHeader(21, UniqueIdentifier++, "Press ‘SR speed/distance’ button",
                "DMI displays SR speed/distance window.Verify the Close button on this window is enabling");
            /*
            Test Step 21
            Action: Press ‘SR speed/distance’ button
            Expected Result: DMI displays SR speed/distance window.Verify the Close button on this window is enabling
            Test Step Comment: MMI_gen 9199 (partly: SR speed/distance);
            */
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Special; // Special window
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.SRSpeedDistance;
            EVC30_MMIRequestEnable.Send();

            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press the ‘SR speed/distance’ button");

            // Enabled close button is a default
            EVC11_MMICurrentSRRules.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the SR speed/distance window." + Environment.NewLine +
                                "2. The ‘Close’ button in the SR speed/distance window is enabled.");

            MakeTestStepHeader(22, UniqueIdentifier++, "Drive the train forward.Then, stop the train",
                "Verify the following information,DMI closes the SR speed/distance window and displays Special window instead.Use the log file to confirm that DMI receives packet information [MMI_ENABLE_REQUEST (EVC-30)] with variable MMI_Q_REQUEST_ENABLE_64 (#11) = 0");
            /*
            Test Step 22
            Action: Drive the train forward.Then, stop the train
            Expected Result: Verify the following information,DMI closes the SR speed/distance window and displays Special window instead.Use the log file to confirm that DMI receives packet information [MMI_ENABLE_REQUEST (EVC-30)] with variable MMI_Q_REQUEST_ENABLE_64 (#11) = 0
            Test Step Comment: (1) MMI_gen 8868 (partly: SR speed/distance);(2) MMI_gen 11283 (partly: SR speed/distance);
            */

            // DMI needs to have the last window closed
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Close_current_return_to_parent;
            EVC30_MMIRequestEnable.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI closes the SR speed/distance window and displays the Special window.");

            MakeTestStepHeader(23, UniqueIdentifier++, "Press ‘Close’ button.Then, press ‘Override’ button",
                "DMI displays Override button.Verify the Close button on this window is enabling");
            /*
            Test Step 23
            Action: Press ‘Close’ button.Then, press ‘Override’ button
            Expected Result: DMI displays Override button.Verify the Close button on this window is enabling
            Test Step Comment: MMI_gen 9179;
            */
            DmiActions.ShowInstruction(this, @"Press ‘Close’ button. Press ‘Override’ button");
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Override window." + Environment.NewLine +
                                "2. The ‘Close’ button in the Override window is enabled.");

            MakeTestStepHeader(24, UniqueIdentifier++, "End of test", "");

            /*
            Test Step 24
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
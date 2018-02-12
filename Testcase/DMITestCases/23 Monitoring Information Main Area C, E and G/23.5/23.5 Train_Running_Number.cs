using System;
using Testcase.Telegrams.EVCtoDMI;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 23.5 Train Running Number
    /// TC-ID: 18.5
    /// 
    /// This test case verifies the validity and display of Train Running number refer to received packet information
    /// 
    /// Tested Requirements:
    /// MMI_gen 1043; MMI_gen 1044; MMI_gen 11905 (partly: text is grey);
    /// 
    /// Scenario:
    /// Enter and confirm the train running number. Then, verify the display information of train runnning number in sub-area G11.Use the test script file to send EVC-2 with an invalid value. Then, verify the display information of train running number in sub-area G11.
    /// 
    /// Used files:
    /// 18_5.xml
    /// </summary>
    public class TC_ID_18_5_Train_Running_Number : TestcaseBase
    {
        public override bool TestcaseEntryPoint()
        {
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 24408;
            // Testcase entrypoint
            StartUp();

            MakeTestStepHeader(1, UniqueIdentifier++,
                "Perform the following procedure, Enter Driver ID and perform brake test.Select and confirm Level 1.Press ‘Train data’ button.Enter an confirm the train data.Validate the Train data",
                "The Train Running Number window is displayed");
            /*
            Test Step 1
            Action: Perform the following procedure, Enter Driver ID and perform brake test.Select and confirm Level 1.Press ‘Train data’ button.Enter an confirm the train data.Validate the Train data
            Expected Result: The Train Running Number window is displayed
            */
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Main; // Main
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.DriverID |
                                                               EVC30_MMIRequestEnable.EnabledRequests.Level |
                                                               EVC30_MMIRequestEnable.EnabledRequests
                                                                   .TrainRunningNumber |
                                                               EVC30_MMIRequestEnable.EnabledRequests.StartBrakeTest |
                                                               EVC30_MMIRequestEnable.EnabledRequests
                                                                   .EnableBrakePercentage |
                                                               EVC30_MMIRequestEnable.EnabledRequests.TrainData;
            EVC30_MMIRequestEnable.Send();
            EVC14_MMICurrentDriverID.Send();

            DmiActions.ShowInstruction(this, "Enter and confirm the Driver ID");

            DmiActions.Request_Brake_Test(this);

            DmiActions.ShowInstruction(this, "Perform the brake test");

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

            DmiActions.ShowInstruction(this, "Select and confirm Level 1.");

            DmiActions.ShowInstruction(this, @"Press the ‘Train data’ button");


            DmiActions.Send_EVC6_MMICurrentTrainData(Variables.MMI_M_DATA_ENABLE.TrainCategory |
                                                     Variables.MMI_M_DATA_ENABLE.TrainLength |
                                                     Variables.MMI_M_DATA_ENABLE.BrakePercentage |
                                                     Variables.MMI_M_DATA_ENABLE.MaxTrainSpeed |
                                                     Variables.MMI_M_DATA_ENABLE.AxleLoadCategory |
                                                     Variables.MMI_M_DATA_ENABLE.Airtightness |
                                                     Variables.MMI_M_DATA_ENABLE.LoadingGauge,
                100, 200,
                Variables.MMI_NID_KEY.PASS1,
                70,
                Variables.MMI_NID_KEY.CATA,
                1,
                Variables.MMI_NID_KEY_Load_Gauge.G1,
                EVC6_MMICurrentTrainData.MMI_M_BUTTONS_CURRENT_TRAIN_DATA.BTN_YES_DATA_ENTRY_COMPLETE,
                0, 0, new[] {"FLU", "RLU", "Rescue"},
                new Variables.DataElement[0]);

            DmiActions.ShowInstruction(this, " Enter and confirm the data");

            DmiActions.Send_EVC10_MMIEchoedTrainData_FixedDataEntry(this, new[] {"FLU", "RLU", "Rescue"});

            DmiActions.ShowInstruction(this, "Validate the train data.");

            EVC16_CurrentTrainNumber.TrainRunningNumber = 1;

            DmiExpectedResults.TRN_window_displayed(this);

            MakeTestStepHeader(2, UniqueIdentifier++,
                "Enter and confirm the train running number.Then, press ‘Close’ button at Main window",
                "DMI displays Default window.Verify the following information,DMI displays the train running number in sub-area G11.The displayed train running number is taken from [MMI_STATUS (EVC-2).MMI_NID_OPERATION].Note: The hexadecimal value ‘F’ is mean ‘No digit’.(i.e. ‘123456’ = 0x123456FF).The text of displayed train running number is coloured grey");
            /*
            Test Step 2
            Action: Enter and confirm the train running number.Then, press ‘Close’ button at Main window
            Expected Result: DMI displays Default window.Verify the following information,DMI displays the train running number in sub-area G11.The displayed train running number is taken from [MMI_STATUS (EVC-2).MMI_NID_OPERATION].Note: The hexadecimal value ‘F’ is mean ‘No digit’.(i.e. ‘123456’ = 0x123456FF).The text of displayed train running number is coloured grey
            Test Step Comment: (1) MMI_gen 1043 (partly: valid, G11);   (2) MMI_gen 1044;  (3)MMI_gen 11905 (partly: text is grey);
            */
            DmiActions.ShowInstruction(this,
                "Enter and confirm the train running number (‘234’). Then press the ‘Close’ button in the Main window");

            EVC2_MMIStatus.TrainRunningNumber = 0x234FFFFF;
            EVC2_MMIStatus.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Default window." + Environment.NewLine +
                                "2. DMI displays the Train Running number ‘234’ in grey in sub-area G11.");

            MakeTestStepHeader(3, UniqueIdentifier++,
                "Use the test script file 18_5.xml to send EVC-2 with,MMI_NID_OPERATION = 173069631487 (0xA12FFFFF)Note: It’s necessary to execute the test script file repeatly to verify the test result",
                "Verify the following information,Verify that the train running number is disappear from sub-area G11.Note: The result will be appear only short time because it’s interrupted by ATP-CU packet information");
            /*
            Test Step 3
            Action: Use the test script file 18_5.xml to send EVC-2 with,MMI_NID_OPERATION = 173069631487 (0xA12FFFFF)Note: It’s necessary to execute the test script file repeatly to verify the test result
            Expected Result: Verify the following information,Verify that the train running number is disappear from sub-area G11.Note: The result will be appear only short time because it’s interrupted by ATP-CU packet information
            Test Step Comment: (1) MMI_gen 1043 (partly: invalid, G11);
            */
            XML_18_5();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI removes the train running number.");

            TraceHeader("End of test");

            /*
            Test Step 4
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }

        #region Send_XML_18_5_DMI_Test_Specification

        private void XML_18_5()
        {
            EVC2_MMIStatus.TrainRunningNumber = 0xa12f; // an invalid number
            EVC2_MMIStatus.MMI_M_ADHESION = 0;
            EVC2_MMIStatus.MMI_M_ACTIVE_CABIN = Variables.MMI_M_ACTIVE_CABIN.Cabin1Active;
            EVC2_MMIStatus.MMI_M_OVERRIDE_EOA = false;

            EVC2_MMIStatus.Send();

            Wait_Realtime(500);
            EVC2_MMIStatus.Send();

            Wait_Realtime(500);
            EVC2_MMIStatus.Send();

            Wait_Realtime(500);
            EVC2_MMIStatus.Send();

            Wait_Realtime(500);
            EVC2_MMIStatus.Send();
        }

        #endregion
    }
}
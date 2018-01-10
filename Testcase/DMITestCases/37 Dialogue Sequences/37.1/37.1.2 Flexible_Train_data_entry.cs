using System;
using Testcase.Telegrams.EVCtoDMI;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 37.1.2 Flexible Train data entry
    /// TC-ID: 34.1.2
    /// 
    /// This test case verifies the display information of window covering the total grid array for Flexible Train data windows when driver enter ‘valid’ value.Moreover, this test case also confirm result of value(s) stored onboard replacing informaiton which occur only when driver press the ‘Yes’ button at Train data validation window.
    /// 
    /// Tested Requirements:
    /// MMI_gen 8863 (partly: Exception); MMI_gen 8865 (partly: Exception 1);
    /// 
    /// Scenario:
    /// Activate Cabin AEnter Driver ID and perform brake test.Select and confirm Level 1.Open Train data window.Modifies and confirm each value of input field as specified value.Close and re-open Train data window again. Then, verify that all train data are not changed Modifies all input fields as specified value. Then, press ‘Yes’ button to enter Train data validation window.Select and confirm ‘No’ button at Train data validation window. Then, verify that all train data are not changed.Modifies all input fields as specified value. Then, press ‘Yes’ button to enter Train data validation window.Select and confirm ‘Yes’ button at Train data validation windowOpen Train data window and verify that all train data are changedClose Train data window.
    /// 
    /// Used files:
    /// N/A
    /// </summary>
    public class TC_34_1_2_Dialogue_Sequences : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // System is power on

            // Call the TestCaseBase PreExecution
            base.PreExecution();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in SB mode, level 1

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint

            /*
            Test Step 1
            Action: Perform the following procedure,Activate Cabin AEnter Driver ID and perform brake testSelect and confirm Level 1
            Expected Result: DMI displays Main window
            */
            DmiActions.Complete_SoM_L1_SB(this);

            DmiActions.ShowInstruction(this, "Press the ‘Main’ button");

            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Main; // Main
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.TrainData;
            EVC30_MMIRequestEnable.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Main window.");

            /*
            Test Step 2
            Action: Press ‘Train data’ button
            Expected Result: DMI displays Train data window.Note: Please memo the value of each input field to be compare with action step 4
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press the ‘Train data’ button");

            DmiActions.Send_EVC6_MMICurrentTrainData(Variables.MMI_M_DATA_ENABLE.TrainSetID |
                                                     Variables.MMI_M_DATA_ENABLE.TrainCategory |
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
                0, 0, new[] {"FLU", "RLU", "Rescue"}, null);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays Train data window" + Environment.NewLine +
                                "Note the value of each Input Field displayed for later comparison.");

            /*
            Test Step 3
            Action: Modify and confirm an valid value for each following input field,Train category = ‘TILT1’Train length = ‘4000’Brake percentage = ‘100’Maximum speed = ‘120’Axel load category = ‘B1’Airtight = ‘No’Loading gauge = ‘Out of GC’
            Expected Result: Verifies that the value of each input field are changed refer to specifies entered data
            */
            WaitForVerification(
                "Modify the value for the Train Category Input Field to ‘TILT1’, confirm the value and check the following:" +
                Environment.NewLine + Environment.NewLine +
                "1. The Input Field value changes according to the data entered.");

            WaitForVerification(
                "Modify the value for the Train length Input Field to ‘4000’, confirm the value and check the following:" +
                Environment.NewLine + Environment.NewLine +
                "1. The Input Field value changes according to the data entered.");

            WaitForVerification(
                "Modify the value for the Brake percentage Input Field to ‘100’, confirm the value and check the following:" +
                Environment.NewLine + Environment.NewLine +
                "1. The Input Field value changes according to the data entered.");

            WaitForVerification(
                "Modify the value for the Maximum speed  Input Field to ‘120’, confirm the value and check the following:" +
                Environment.NewLine + Environment.NewLine +
                "1. The Input Field value changes according to the data entered.");

            WaitForVerification(
                "Modify the value for the Axle load category Input Field to ‘B1’, confirm the value and check the following:" +
                Environment.NewLine + Environment.NewLine +
                "1. The Input Field value changes according to the data entered.");

            WaitForVerification(
                "Modify the value for the Airtight Input Field to ‘No’, confirm the value and check the following:" +
                Environment.NewLine + Environment.NewLine +
                "1. The Input Field value changes according to the data entered.");

            WaitForVerification(
                "Modify the value for the Loading gauge  Input Field to ‘Out of GC’, confirm the value and check the following:" +
                Environment.NewLine + Environment.NewLine +
                "1. The Input Field value changes according to the data entered.");

            /*
            Test Step 4
            Action: Press ‘Close’ button.Then, select Train data button
            Expected Result: DMI displays Train data window.Verifies the displayed values of each input field are same as action step No.2
            Test Step Comment: MMI_gen 8865 (partly: Exception 1);
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button then press ‘Train data’ button");

            DmiActions.Send_EVC6_MMICurrentTrainData(Variables.MMI_M_DATA_ENABLE.TrainSetID |
                                                     Variables.MMI_M_DATA_ENABLE.TrainCategory |
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
                0, 0, new[] {"FLU", "RLU", "Rescue"}, null);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays Train data window" + Environment.NewLine +
                                "2. The values of each data input field are as in Step 2.");

            /*
            Test Step 5
            Action: Repeat action step 3.Then, press ‘Yes’ button
            Expected Result: DMI displays Train data validation window
            */
            WaitForVerification(
                "Modify the value for the Train Category Input Field to ‘TILT1’, confirm the value and check the following:" +
                Environment.NewLine + Environment.NewLine +
                "1. The Input Field value changes according to the data entered.");

            WaitForVerification(
                "Modify the value for the Train length Input Field to ‘4000’, confirm the value and check the following:" +
                Environment.NewLine + Environment.NewLine +
                "1. The Input Field value changes according to the data entered.");

            WaitForVerification(
                "Modify the value for the Brake percentage Input Field to ‘100’, confirm the value and check the following:" +
                Environment.NewLine + Environment.NewLine +
                "1. The Input Field value changes according to the data entered.");

            WaitForVerification(
                "Modify the value for the Maximum speed  Input Field to ‘120’, confirm the value and check the following:" +
                Environment.NewLine + Environment.NewLine +
                "1. The Input Field value changes according to the data entered.");

            WaitForVerification(
                "Modify the value for the Axle load category Input Field to ‘B1’, confirm the value and check the following:" +
                Environment.NewLine + Environment.NewLine +
                "1. The Input Field value changes according to the data entered.");

            WaitForVerification(
                "Modify the value for the Airtight Input Field to ‘No’, confirm the value and check the following:" +
                Environment.NewLine + Environment.NewLine +
                "1. The Input Field value changes according to the data entered.");

            WaitForVerification(
                "Modify the value for the Loading gauge  Input Field to ‘Out of GC’, confirm the value and check the following:" +
                Environment.NewLine + Environment.NewLine +
                "1. The Input Field value changes according to the data entered.");

            DmiActions.ShowInstruction(this, @"Press ‘Yes’ button");

            /*DmiActions.Send_EVC10_MMIEchoedTrainData(Variables.MMI_M_DATA_ENABLE.TrainSetID |
                                                     Variables.MMI_M_DATA_ENABLE.TrainCategory |
                                                     Variables.MMI_M_DATA_ENABLE.TrainLength |
                                                     Variables.MMI_M_DATA_ENABLE.BrakePercentage |
                                                     Variables.MMI_M_DATA_ENABLE.MaxTrainSpeed |
                                                     Variables.MMI_M_DATA_ENABLE.AxleLoadCategory |
                                                     Variables.MMI_M_DATA_ENABLE.Airtightness |
                                                     Variables.MMI_M_DATA_ENABLE.LoadingGauge,
                                                     4000, 
                                                     120,
                                                     Variables.MMI_NID_KEY.TILT1,
                                                     100, 
                                                     Variables.MMI_NID_KEY.CATB1,
                                                     0,
                                                     Variables.MMI_NID_KEY.G1,
                                                     0,
                                                     0,
                                                      new[] { "FLU", "RLU", "Rescue" }, null);*/
            DmiActions.Send_EVC10_MMIEchoedTrainData_FixedDataEntry(this, Variables.paramEvc6FixedTrainsetCaptions);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Train validation data window.");

            /*
            Test Step 6
            Action: Select and confirm ‘No’ button
            Expected Result: DMI displays Train data window.Verifies the displayed values of each input field are different from entered data in step 3
            Test Step Comment: MMI_gen 8865 (partly: Exception 1);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press and confirm ‘No’ button");

            DmiActions.Send_EVC6_MMICurrentTrainData(Variables.MMI_M_DATA_ENABLE.TrainSetID |
                                                     Variables.MMI_M_DATA_ENABLE.TrainCategory |
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
                0, 0, new[] {"FLU", "RLU", "Rescue"}, null);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays Train data window." + Environment.NewLine +
                                "2. The values of each Input Field are different from those entered in Step 5");

            /*
            Test Step 7
            Action: Repeat action step 3.Then, press ‘Yes’ button
            Expected Result: DMI displays Train data validation window
            */
            WaitForVerification(
                "Modify the value for the Train Category Input Field to ‘TILT1’, confirm the value and check the following:" +
                Environment.NewLine + Environment.NewLine +
                "1. The Input Field value changes according to the data entered.");

            WaitForVerification(
                "Modify the value for the Train length Input Field to ‘4000’, confirm the value and check the following:" +
                Environment.NewLine + Environment.NewLine +
                "1. The Input Field value changes according to the data entered.");

            WaitForVerification(
                "Modify the value for the Brake percentage Input Field to ‘100’, confirm the value and check the following:" +
                Environment.NewLine + Environment.NewLine +
                "1. The Input Field value changes according to the data entered.");

            WaitForVerification(
                "Modify the value for the Maximum speed  Input Field to ‘120’, confirm the value and check the following:" +
                Environment.NewLine + Environment.NewLine +
                "1. The Input Field value changes according to the data entered.");

            WaitForVerification(
                "Modify the value for the Axle load category Input Field to ‘B1’, confirm the value and check the following:" +
                Environment.NewLine + Environment.NewLine +
                "1. The Input Field value changes according to the data entered.");

            WaitForVerification(
                "Modify the value for the Airtight Input Field to ‘No’, confirm the value and check the following:" +
                Environment.NewLine + Environment.NewLine +
                "1. The Input Field value changes according to the data entered.");

            WaitForVerification(
                "Modify the value for the Loading gauge  Input Field to ‘Out of GC’, confirm the value and check the following:" +
                Environment.NewLine + Environment.NewLine +
                "1. The Input Field value changes according to the data entered.");

            DmiActions.ShowInstruction(this, @"Press ‘Yes’ button");

            /*DmiActions.Send_EVC10_MMIEchoedTrainData(Variables.MMI_M_DATA_ENABLE.TrainSetID |
                                                     Variables.MMI_M_DATA_ENABLE.TrainCategory |
                                                     Variables.MMI_M_DATA_ENABLE.TrainLength |
                                                     Variables.MMI_M_DATA_ENABLE.BrakePercentage |
                                                     Variables.MMI_M_DATA_ENABLE.MaxTrainSpeed |
                                                     Variables.MMI_M_DATA_ENABLE.AxleLoadCategory |
                                                     Variables.MMI_M_DATA_ENABLE.Airtightness |
                                                     Variables.MMI_M_DATA_ENABLE.LoadingGauge,
                                                     4000,
                                                     120,
                                                     Variables.MMI_NID_KEY.TILT1,
                                                     100,
                                                     Variables.MMI_NID_KEY.CATB1,
                                                     0,
                                                     Variables.MMI_NID_KEY.G1,
                                                     0,
                                                     0,
                                                      new[] { "FLU", "RLU", "Rescue" }, null);*/
            DmiActions.Send_EVC10_MMIEchoedTrainData_FixedDataEntry(this, Variables.paramEvc6FixedTrainsetCaptions);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays Train data validation window.");

            /*
            Test Step 8
            Action: Select and confirm ‘Yes’ button in Train data validation window
            Expected Result: DMI displays Main window
            */
            DmiActions.ShowInstruction(this, @"Press and confirm ‘Yes’ button in Train data validation window");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays Train data window.");

            /*
            Test Step 9
            Action: Select Train data button
            Expected Result: DMI displays Train data window.Verifies the displayed values of each input field are changed refer to entered data in step 3
            Test Step Comment: MMI_gen 8863 (partly: Exception);
            */
            DmiActions.ShowInstruction(this, @"Press ‘Train data’ button");

            DmiActions.Send_EVC6_MMICurrentTrainData(Variables.MMI_M_DATA_ENABLE.TrainSetID |
                                                     Variables.MMI_M_DATA_ENABLE.TrainCategory |
                                                     Variables.MMI_M_DATA_ENABLE.TrainLength |
                                                     Variables.MMI_M_DATA_ENABLE.BrakePercentage |
                                                     Variables.MMI_M_DATA_ENABLE.MaxTrainSpeed |
                                                     Variables.MMI_M_DATA_ENABLE.AxleLoadCategory |
                                                     Variables.MMI_M_DATA_ENABLE.Airtightness |
                                                     Variables.MMI_M_DATA_ENABLE.LoadingGauge,
                4000,
                120,
                Variables.MMI_NID_KEY.TILT1,
                100,
                Variables.MMI_NID_KEY.CATB1,
                0,
                Variables.MMI_NID_KEY_Load_Gauge.G1,
                EVC6_MMICurrentTrainData.MMI_M_BUTTONS_CURRENT_TRAIN_DATA.BTN_YES_DATA_ENTRY_COMPLETE,
                0,
                0,
                new[] {"FLU", "RLU", "Rescue"}, null);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays Train data window." + Environment.NewLine +
                                "2. The values of each Input Field are those entered in Step 7");

            /*
            Test Step 10
            Action: Press ‘Close’ button
            Expected Result: DMI displays Main window
            */
            DmiActions.ShowInstruction(this, @"Press ‘Close’ button");
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays Main window.");

            /*
            Test Step 11
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
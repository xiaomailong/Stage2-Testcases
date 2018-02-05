using System;
using Testcase.Telegrams.DMItoEVC;
using Testcase.Telegrams.EVCtoDMI;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 27.23 Switchable train data entry
    /// TC-ID: 22.23
    /// 
    /// This test case verifies the displays information of ‘switch’ button including with packet sending when the button is pressed. The switchable train data entry shall comply with [ERA-ERTMS] standard and [MMI-ETCS-gen].
    /// 
    /// Tested Requirements:
    /// MMI_gen 8095; MMI_gen 8098; MMI_gen 8099 (partly: bottom right corner); MMI_gen 8096; MMI_gen 8097; MMI_gen 9402 (partly:  MMI_M_ALT_DEM, switchable);
    /// 
    /// Scenario:
    /// Open Train data window. Then, verify the location and label of ‘switch’ button.Press ‘switch’ button. Then, verify that label of ‘switch’ button is changed and DMI send out packet information EVC-101.
    /// 
    /// Used files:
    /// N/A
    /// </summary>
    public class TC_ID_22_23_Switchable_train_data_entry : TestcaseBase
    {

        public override bool TestcaseEntryPoint()
        {
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 0;
            // Testcase entrypoint

            StartUp();
            // Test system is powered on.Cabin is activated.SoM is performed until Level 1 is selcted and confirmed.
            DmiActions.Complete_SoM_L1_SB(this);


            MakeTestStepHeader(1, UniqueIdentifier++, "Press ‘Train data’ button",
                "The Train data window is displayed as  Flexible train data entry or Fixed train data entry.Verify the following information,The Train data window is presented an enabled ‘switch’ button.The ‘switch’ button is located in the bottom right corner of D/F/G area.The label of ‘switch’ button is displayed correctly refer to the type of displayed window as follows,Flexible Train DataThe label of switch button is presented with text ‘Select type’. Fixed Train DataThe label of switch button is presented with text ‘Enter data’.Use the log file to confirm that DMI receives packet EVC-6 with variable MMI_M_ALT_DEM = 1 (switchable)");
            /*
            Test Step 1
            Action: Press ‘Train data’ button
            Expected Result: The Train data window is displayed as  Flexible train data entry or Fixed train data entry.Verify the following information,The Train data window is presented an enabled ‘switch’ button.The ‘switch’ button is located in the bottom right corner of D/F/G area.The label of ‘switch’ button is displayed correctly refer to the type of displayed window as follows,Flexible Train DataThe label of switch button is presented with text ‘Select type’. Fixed Train DataThe label of switch button is presented with text ‘Enter data’.Use the log file to confirm that DMI receives packet EVC-6 with variable MMI_M_ALT_DEM = 1 (switchable)
            Test Step Comment: (1) MMI_gen 8095;   (2) MMI_gen 8098; MMI_gen 8099 (partly: bottom right corner);(3) MMI_gen 8097; (4) MMI_gen 9402 (partly:  MMI_M_ALT_DEM, switchable);
            */
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Main; // Main
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.TrainData;
            EVC30_MMIRequestEnable.Send();

            DmiActions.ShowInstruction(this, @"Press the ‘Train data’ button");
            DmiActions.Send_EVC6_MMICurrentTrainData_FixedDataEntry(this, Variables.paramEvc6FixedTrainsetCaptions, 1);
            EVC6_MMICurrentTrainData.MMI_M_ALT_DEM = 1;
            EVC6_MMICurrentTrainData.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Train data entry window." + Environment.NewLine +
                                "2. An enabled ‘switch’ button is displayed in the bottom right-hand corner of areas D, F and G." +
                                "3. If the Train data entry window is in ‘Flexible Train Data’ mode the label of the ‘switch’ button displays ‘Select type’." +
                                Environment.NewLine +
                                "4. If the Train data entry window is in ‘Fixed Train Data’ mode the label of the ‘switch’ button displays ‘Enter data’.");

            MakeTestStepHeader(2, UniqueIdentifier++, "Press switch button",
                "Verify the following information,Use the log file to confirm that DMI send out packet EVC-101 [MMI_DRIVER_REQUEST] with variable MMI_M_REQUEST = 59 (Switch).The label of ‘switch’ button is changed correctly refer to the type of displayed window as follows,Flexible Train DataThe label of switch button is presented with text ‘Select type’. Fixed Train DataThe label of switch button is presented with text ‘Enter data’");
            /*
            Test Step 2
            Action: Press switch button
            Expected Result: Verify the following information,Use the log file to confirm that DMI send out packet EVC-101 [MMI_DRIVER_REQUEST] with variable MMI_M_REQUEST = 59 (Switch).The label of ‘switch’ button is changed correctly refer to the type of displayed window as follows,Flexible Train DataThe label of switch button is presented with text ‘Select type’. Fixed Train DataThe label of switch button is presented with text ‘Enter data’
            Test Step Comment: (1) MMI_gen 8096;  (2) MMI_gen 8097;
            */
            DmiActions.ShowInstruction(this, @"Press the ‘switch’ button");

            EVC101_MMIDriverRequest.CheckMRequestReleased = Variables.MMI_M_REQUEST.Switch;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. If the ‘switch’ button displayed ‘Select type’ before being pressed it now displays ‘Enter data’." +
                                Environment.NewLine +
                                @"2. If the ‘switch’ button displayed ‘Enter data’ before being pressed it now displays ‘Select type’.");

            MakeTestStepHeader(3, UniqueIdentifier++, "End of test", "");

            /*
            Test Step 3
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
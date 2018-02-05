using System;
using Testcase.Telegrams.EVCtoDMI;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 27.29.4 ‘Train data’ Data Checks: Technical Range Checks by Variable Range
    /// TC-ID: 22.29.4
    /// 
    /// This test case verifies the functionalities of ‘Train data’ data entry when the data of Train data does not comply with variable-range rules of the technical range check. The function designs comply with the conditions in [MMI-ETCS-gen]. The data range and interface comply with the data information in [VSIS_gen].
    /// 
    /// Tested Requirements:
    /// 1. Train data Window: MMI_gen 9413;2. Data Checks: MMI_gen 8089 (partly: reactions to failing and succeed, EVC-18, MMI_gen 4713, MMI_gen 12145, MMI_gen 12147, MMI_gen 12148, MMI_gen 4714, MMI_gen 9286 (partly: the ‘Enter’ button, switched state, disabled, enabled)), MMI_gen 9404 (partly: MMI_gen 12148 (partly: MMI_gen 4713 (partly:red))); MMI_gen 4699 (technical range);
    /// 
    /// Scenario:
    /// 1.Activate the cabin.
    /// 2.Perform SoM until mode SR is confirmed in the default window.
    /// 3.Press the ‘Main’ button. Then, the ‘Main’ window is opened.
    /// 4.Press the ‘Train data’ button. Then, the ‘Train data’ window is opened.
    /// 5.Enter the numeric data of Train data, i.e. the maximum train length, maximum train speed, brake percentage. Then, the ‘Train data’ window is verified by the following events:      a. The minimum DMI-technical-inbound VBC code is entered and accepted.      b. The DMI-technical-outbound VBC code is entered and accepted.      c. The maximum DMI-technical-inbound VBC code is entered and accepted.
    /// 
    /// Used files:
    /// N/A
    /// </summary>
    public class TC_ID_22_29_4_Train_data_Data_Checks_Technical_Range_Checks_by_Variable_Range : TestcaseBase
    {

        public override bool TestcaseEntryPoint()
        {
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 0;
            // Testcase entrypoint

            StartUp();
            // 1. The test environment is powered on.2. ATP-CU is verified that the train is set as ‘Flexible’.TR_OBU_TrainType = 23. 
            //The cabin is activated.4. The ‘Start of Mission’ procedure is performed until the ‘Staff Resonsible’ mode, level 1, is confirmed.5. The ‘Main’ window is opened.
            DmiActions.Complete_SoM_L1_SR(this);

            TraceInfo("This test case requires an ATP configuration change - " +
                      "See Precondition requirements. If this is not done manually, the test may fail!");

            MakeTestStepHeader(1, UniqueIdentifier++, "Open the ‘Train data’ data entry window from the Main menu",
                "The ‘Train data’ data entry window appears on ETCS-DMI screen instead of the ‘Main’ menu window");
            /*
            Test Step 1
            Action: Open the ‘Train data’ data entry window from the Main menu
            Expected Result: The ‘Train data’ data entry window appears on ETCS-DMI screen instead of the ‘Main’ menu window
            */
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Main;
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.TrainData;
            EVC30_MMIRequestEnable.Send();

            DmiActions.ShowInstruction(this, "Press the ‘Train data’ button");

            DmiActions.Send_EVC6_MMICurrentTrainData(Variables.MMI_M_DATA_ENABLE.TrainLength |
                                                     Variables.MMI_M_DATA_ENABLE.BrakePercentage |
                                                     Variables.MMI_M_DATA_ENABLE.MaxTrainSpeed,
                100, 200,
                Variables.MMI_NID_KEY.PASS2,
                70,
                Variables.MMI_NID_KEY.CATA,
                0,
                Variables.MMI_NID_KEY_Load_Gauge.G1,
                EVC6_MMICurrentTrainData.MMI_M_BUTTONS_CURRENT_TRAIN_DATA.BTN_YES_DATA_ENTRY_COMPLETE,
                0, 0, new[] {"FLU", "RLU", "Rescue"}, new Variables.DataElement[0]);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI replaces the Main window with the Train data window.");

            MakeTestStepHeader(2, UniqueIdentifier++,
                "Enter “0” (minimum inbound) for Train Length with the numeric keypad and press the data input field (Accept) in the same screen",
                "Input Field(1) The eventually displayed data value in the data area of the input field is replaced by “0” (character or value corresponding to the activated data key - state ‘Selected IF/value of pressed key(s)’).EVC-107(2) Use the log file to verify that DMI sends packet EVC-107 with variable:MMI_L_TRAIN = 0 MMI_NID_DATA = 8 (Length)");
            /*
            Test Step 2
            Action: Enter “0” (minimum inbound) for Train Length with the numeric keypad and press the data input field (Accept) in the same screen
            Expected Result: Input Field(1) The eventually displayed data value in the data area of the input field is replaced by “0” (character or value corresponding to the activated data key - state ‘Selected IF/value of pressed key(s)’).EVC-107(2) Use the log file to verify that DMI sends packet EVC-107 with variable:MMI_L_TRAIN = 0 MMI_NID_DATA = 8 (Length)
            Test Step Comment: Requirements:(1) MMI_gen 8089 (partly: reactions to succeed, MMI_gen 4714 (partly: MMI_gen 4679), MMI_gen 9286 (partly: state switched), MMI_gen 12145 (partly: minimum inbound)), MMI_gen 9413 (partly: state switched);(2) MMI_gen 8089 (partly: reactions to succeed, MMI_gen 12147, MMI_gen 9286 (partly: enabled)), MMI_gen 9413 (partly: enabled);
            */
            DmiActions.ShowInstruction(this,
                "Using the numeric keypad, enter ‘0’ for the train length and press in the data input field to accept the value");

            //EVC107_MMINewTrainData.Check_MMI_L_TRAIN = 0;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The value ‘0’ is displayed for train length (instead of 100)." +
                                Environment.NewLine +
                                "2. The value for train length is ‘Selected’.");

            MakeTestStepHeader(3, UniqueIdentifier++,
                "Enter “4096” (outbound) for Train Length with the numeric keypad and press the data input field (Accept) in the same screen",
                "Input Field(1) The ‘Enter’ button associated to the data area of the input field is coloured grey and its text is black (state ‘Selected IF/Data value’).(2) The ‘Enter’ button associated to the data area of the input field displays “4096” (previously entered value).EVC-107(3) Use the log file to verify that DMI does not send out packet EVC-107 as the ‘Enter’ button is disabled. Echo Texts(4) The data part of the echo text displays “++++”.(5) The data part of the echo text is coloured red");
            /*
            Test Step 3
            Action: Enter “4096” (outbound) for Train Length with the numeric keypad and press the data input field (Accept) in the same screen
            Expected Result: Input Field(1) The ‘Enter’ button associated to the data area of the input field is coloured grey and its text is black (state ‘Selected IF/Data value’).(2) The ‘Enter’ button associated to the data area of the input field displays “4096” (previously entered value).EVC-107(3) Use the log file to verify that DMI does not send out packet EVC-107 as the ‘Enter’ button is disabled. Echo Texts(4) The data part of the echo text displays “++++”.(5) The data part of the echo text is coloured red
            Test Step Comment: Requirements:(1) MMI_gen 8089 (partly: reactions to failing, MMI_gen 4714 (partly: state 'Selected IF/data value'));(2) MMI_gen 8089 (partly: reactions to failing, MMI_gen 4714 (partly: previously entered (faulty) value), MMI_gen 12145 (partly: outbound)); MMI_gen 4699 (technical range);(3) MMI_gen 8089 (partly: MMI_gen 9286 (partly: button ‘Enter’, disabled), MMI_gen 12148 (partly: not send packets), MMI_gen 12147), MMI_gen 9413 (partly: disabled);(4) MMI_gen 8089 (partly: reactions to failing, MMI_gen 12148 (MMI_gen 4713 (partly: indication)));(5) MMI_gen 9404 (partly: MMI_gen 12148 (MMI_gen 4713 (partly: red))), MMI_gen 8089 (partly: reactions to failing, MMI_gen 12148 (MMI_gen 4713 (partly: red)));
            */
            DmiActions.ShowInstruction(this,
                "Using the numeric keypad, enter ‘4096’ for the train length and press in the data input field to accept the value");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. ‘4096’ is displayed for train length as the ‘Enter’ button value." +
                                Environment.NewLine +
                                "2. The ‘Enter’ button (the data area of the data input field) has black text on grey background." +
                                Environment.NewLine +
                                "3. The echo text for train length displays ‘++++’ in red.");

            MakeTestStepHeader(4, UniqueIdentifier++,
                "Enter “4095” (maximum inbound) for Train Length with the numeric keypad and press the data input field (Accept) in the same screen",
                "Input Field(1) The eventually displayed data value in the data area of the input field is replaced by “4095” (character or value corresponding to the activated data key - state ‘Selected IF/value of pressed key(s)’).EVC-107(2) Use the log file to verify that DMI sends packet EVC-107 with variable:MMI_L_TRAIN = 4095 MMI_NID_DATA = 8 (Length)");
            /*
            Test Step 4
            Action: Enter “4095” (maximum inbound) for Train Length with the numeric keypad and press the data input field (Accept) in the same screen
            Expected Result: Input Field(1) The eventually displayed data value in the data area of the input field is replaced by “4095” (character or value corresponding to the activated data key - state ‘Selected IF/value of pressed key(s)’).EVC-107(2) Use the log file to verify that DMI sends packet EVC-107 with variable:MMI_L_TRAIN = 4095 MMI_NID_DATA = 8 (Length)
            Test Step Comment: Requirements:(1) MMI_gen 8089 (partly: MMI_gen 4714 (partly: MMI_gen 4679), MMI_gen 9286 (partly: state switched), MMI_gen 12145 (partly: maximum inbound)), MMI_gen 9413 (partly: state switched); (2) MMI_gen 8089 (partly: reactions to succeed, MMI_gen 12147, MMI_gen 9286 (partly: enabled)), MMI_gen 9413 (partly: enabled);
            */
            DmiActions.ShowInstruction(this,
                "Using the numeric keypad, enter ‘4095’ for the train length and press in the data input field to accept the value");

            //EVC107_MMINewTrainData.Check_MMI_L_TRAIN = 4095;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The data input field displays ‘4095’ for train length.");

            MakeTestStepHeader(5, UniqueIdentifier++,
                "Follow step 2 – step 4 for Brake Percentage with:Minimum inbound = 10Outbound = 251Maximum inbound = 250",
                "See step 2 – step 4EVC-107(1) Use the log file to confirm that DMI sends packet EVC-107 with variable:MMI_M_BRAKE_PERC = See Action MMI_NID_DATA = 9 (Brake Percentage)");
            /*
            Test Step 5
            Action: Follow step 2 – step 4 for Brake Percentage with:Minimum inbound = 10Outbound = 251Maximum inbound = 250
            Expected Result: See step 2 – step 4EVC-107(1) Use the log file to confirm that DMI sends packet EVC-107 with variable:MMI_M_BRAKE_PERC = See Action MMI_NID_DATA = 9 (Brake Percentage)
            Test Step Comment: See step 2 – step 4
            */
            // Repeat Step 2 for brake percentage
            DmiActions.ShowInstruction(this,
                "Using the numeric keypad, enter ‘10’ for the brake percentage and press in the data input field to accept the value");

            //EVC107_MMINewTrainData.Check_MMI_M_BRAKE_PERC = 10;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The value ‘10’ is displayed for the brake percentage (instead of 70)." +
                                Environment.NewLine +
                                "2. The value for brake percentage is ‘Selected’.");

            // Repeat Step 3 for brake percentage
            DmiActions.ShowInstruction(this,
                "Using the numeric keypad, enter ‘251’ for the brake percentage and press in the data input field to accept the value");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. ‘251 is displayed for train length as the ‘Enter’ button value." +
                                Environment.NewLine +
                                "2. The ‘Enter’ button (the data area of the data input field) has black text on grey background." +
                                Environment.NewLine +
                                "3. The echo text for train length displays ‘++++’ in red.");

            // Repeat Step 4 for brake percentage
            DmiActions.ShowInstruction(this,
                "Using the numeric keypad, enter ‘250’ for the brake percentage and press in the data input field to accept the value");

            //EVC107_MMINewTrainData.Check_MMI_M_BRAKE_PERC = 4095;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The data input field displays ‘250’ for brake percentage.");

            MakeTestStepHeader(6, UniqueIdentifier++,
                "Follow step 2 – step 4 for Max speed with:Minimum inbound = 0Outbound = 601Maximum inbound = 600",
                "See step 2 – step 4EVC-107(1) Use the log file to confirm that DMI sends packet EVC-107 with variable:MMI_V_MAXTRAIN = See Action MMI_NID_DATA = 10 (Maximum speed)");
            /*
            Test Step 6
            Action: Follow step 2 – step 4 for Max speed with:Minimum inbound = 0Outbound = 601Maximum inbound = 600
            Expected Result: See step 2 – step 4EVC-107(1) Use the log file to confirm that DMI sends packet EVC-107 with variable:MMI_V_MAXTRAIN = See Action MMI_NID_DATA = 10 (Maximum speed)
            Test Step Comment: See step 2 – step 4
            */
            // Repeat Step 2 for maximum speed
            DmiActions.ShowInstruction(this,
                "Using the numeric keypad, enter ‘0’ for the maximum speed and press in the data input field to accept the value");

            //EVC107_MMINewTrainData.Check_MMI_V_MAXTRAIN = 0;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The value ‘0’ is displayed for the maximum speed (instead of 200)." +
                                Environment.NewLine +
                                "2. The value for maximum speed is ‘Selected’.");

            // Repeat Step 3 for maximum speed
            DmiActions.ShowInstruction(this,
                "Using the numeric keypad, enter ‘601’ for the maximum speed and press in the data input field to accept the value");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. ‘251 is displayed for train length as the ‘Enter’ button value." +
                                Environment.NewLine +
                                "2. The ‘Enter’ button (the data area of the data input field) has black text on grey background." +
                                Environment.NewLine +
                                "3. The echo text for train length displays ‘++++’ in red.");

            // Repeat Step 4 for maximum speed
            DmiActions.ShowInstruction(this,
                "Using the numeric keypad, enter ‘600’ for the maximum speed and press in the data input field to accept the value");

            //EVC107_MMINewTrainData.Check_MMI_V_MAXTRAIN = 600;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The data input field displays ‘250’ for maximum speed.");

            MakeTestStepHeader(7, UniqueIdentifier++,
                "This step is to complete the process of ‘Train data’:- Press the ‘Yes’ button on the ‘Train data’ window.- Validate the data in the data validation window",
                "1. After pressing the ‘Yes’ button, the data validation window (‘Validate Train data’) appears instead of the ‘Train data’ data entry window. The data part of echo text displays “600” in white.2. After the data area of the input field containing “Yes” is pressed, the data validation window disappears and returns to the parent window (‘Settings’ window) of ‘Train data’ window with enabled ‘Train data’ button");
            /*
            Test Step 7
            Action: This step is to complete the process of ‘Train data’:- Press the ‘Yes’ button on the ‘Train data’ window.- Validate the data in the data validation window
            Expected Result: 1. After pressing the ‘Yes’ button, the data validation window (‘Validate Train data’) appears instead of the ‘Train data’ data entry window. The data part of echo text displays “600” in white.2. After the data area of the input field containing “Yes” is pressed, the data validation window disappears and returns to the parent window (‘Settings’ window) of ‘Train data’ window with enabled ‘Train data’ button
            */
            DmiActions.ShowInstruction(this, "Press the ‘Yes’ button in the ‘Train data’ window");

            DmiActions.Send_EVC10_MMIEchoedTrainData(this,
                Variables.MMI_M_DATA_ENABLE.TrainLength |
                Variables.MMI_M_DATA_ENABLE.BrakePercentage |
                Variables.MMI_M_DATA_ENABLE.MaxTrainSpeed,
                4095,
                600,
                Variables.MMI_NID_KEY.PASS2,
                250,
                Variables.MMI_NID_KEY.CATA,
                0,
                Variables.MMI_NID_KEY.G1,
                new[] {"FLU", "RLU", "Rescue"});

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Validate train data  window.");

            DmiActions.ShowInstruction(this, "Validate (confirm) the data");

            // Test says settings window but main window is parent of Train data window
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI closes the Validate train data window and displays the Main window." +
                                Environment.NewLine +
                                "2. The ‘Train data’ button is displayed enabled.");

            MakeTestStepHeader(8, UniqueIdentifier++, "End of test", "");

            /*
            Test Step 8
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
using System;
using Testcase.Telegrams.EVCtoDMI;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 27.22.6 ‘Brake percentage’ Data Checks: Technical Range Checks by Variable Range
    /// TC-ID: 22.22.6
    /// 
    /// This test case verifies the functionalities of ‘Brake percentage’ data entry when the data of Brake percentage does not comply with variable-range rules of the technical range check. The function designs comply with the conditions in [MMI-ETCS-gen]. The data range and interface comply with the data information in [VSIS_gen].
    /// 
    /// Tested Requirements:
    /// MMI_gen 11823 (partly: MMI_gen 4713, MMI_gen 12145, MMI_gen 12147, MMI_gen 12148, MMI_gen 4714, MMI_gen 9286 (partly: the ‘Enter’ button, switched state, disabled, enabled)); MMI_gen 4699 (technical range);
    /// 
    /// Scenario:
    /// 1.Configure the ATP-CU to enable level ‘ATC-2’
    /// 2.Activate the cabin.
    /// 3.Perform SoM until mode SR is confirmed in the default window.
    /// 4.Press the ‘Settings’ button. Then, the ‘Settings’ window is opened.
    /// 5.Press the ‘Brake’ button. Then, the ‘Brake’ window is opened.
    /// 6.Press the ‘Brake percentage’ button. Then, the ‘Brake percentage’ window is opened.
    /// 7.Enter Brake percentage. Then, the ‘Brake percentage’ window is verified by the following events:a.   The minimum DMI-technical-inbound data of Brake percentage is entered and accepted.b.   The DMI-technical-outbound data of Brake percentage is entered and accepted.c.   The maximum DMI-technical-inbound data of Brake percentage is entered and accepted.
    /// 
    /// Used files:
    /// N/A
    /// </summary>
    public class TC_ID_22_22_6_Brake_percentage_Data_Checks : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // 1. The ‘ATC-2’ level is configured in ATP-CU.NID_NTC_Installed = 22, PB_SAFETY_LEVEL = 2, NTC_HW_ADDR = 92, NID_NTC_Default = 22 (M_InstalledLevels and M_DefaultLevels have to be updated according to the number of enabling NTC/STM levels, by bitmasks)

            // Call the TestCaseBase PreExecution
            base.PreExecution();

            // 2. The test environment is powered on.3. The cabin is activated.4. The ‘Start of Mission’ procedure is performed until the ‘Staff Responsible’ mode, level 1, is confirmed.5. The ‘Brake’ window is opened.
            DmiActions.Complete_SoM_L1_SR(this);
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // ETCS-DMI is in the ‘Staff Responsible’ mode, level 1.

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 0;
            // Testcase entrypoint
            TraceInfo("This test case requires an ATP configuration change - " +
                      "See Precondition requirements. If this is not done manually, the test may fail!");

            TraceHeader("Test Step 1");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Open the ‘Brake percentage’ data entry window from the Special menu");
            TraceReport("Expected Result");
            TraceInfo(
                "The ‘Brake percentage’ data entry window appears on ETCS-DMI screen instead of the ‘Special’ menu window");
            /*
            Test Step 1
            Action: Open the ‘Brake percentage’ data entry window from the Special menu
            Expected Result: The ‘Brake percentage’ data entry window appears on ETCS-DMI screen instead of the ‘Special’ menu window
            */
            // Settings not special??
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Settings; // Settings
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH =
                EVC30_MMIRequestEnable.EnabledRequests.EnableBrakePercentage;
            EVC30_MMIRequestEnable.Send();

            DmiActions.ShowInstruction(this,
                @"Press the ‘Brake’ button, then the ‘Brake percentage’ button in the Brake percentage window");

            EVC50_MMICurrentBrakePercentage.MMI_M_BP_CURRENT = 40;
            EVC50_MMICurrentBrakePercentage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Brake percentage window");

            TraceHeader("Test Step 2");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Enter “10” (minimum inbound) with the numeric keypad and press the data input field (Accept) in the same screen");
            TraceReport("Expected Result");
            TraceInfo(
                "Input Field(1) The eventually displayed data value in the data area of the input field is replaced by “10” (character or value corresponding to the activated data key - state ‘Selected IF/value of pressed key(s)’).EVC-150(2) Use the log file to verify that DMI sends packet EVC-150 with variable:-   MMI_M_BP_CURRENT = 10");
            /*
            Test Step 2
            Action: Enter “10” (minimum inbound) with the numeric keypad and press the data input field (Accept) in the same screen
            Expected Result: Input Field(1) The eventually displayed data value in the data area of the input field is replaced by “10” (character or value corresponding to the activated data key - state ‘Selected IF/value of pressed key(s)’).EVC-150(2) Use the log file to verify that DMI sends packet EVC-150 with variable:-   MMI_M_BP_CURRENT = 10
            Test Step Comment: Requirements:(1) MMI_gen 11823 (partly: MMI_gen 4714 (partly: MMI_gen 4679), MMI_gen 9286 (partly: state switched), MMI_gen 12145 (partly: minimum inbound));(2) MMI_gen 11823 (partly: MMI_gen 12147, MMI_gen 9286 (partly: enabled));
            */
            DmiActions.ShowInstruction(this,
                "Enter ‘10’ for Brake percentage and confirm by pressing in the data input field");

            //EVC150_MMINewBrakePercentage.CheckMmiMBPCurrent = 10;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The data input field displays ‘1’ then ‘10’ (replacing the value ‘40’ as the characters are entered.");

            TraceHeader("Test Step 3");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Enter “251” (outbound) with the numeric keypad and press the data input field (Accept) in the same screen");
            TraceReport("Expected Result");
            TraceInfo(
                "Input Field(1) The ‘Enter’ button associated to the data area of the input field is coloured grey and its text is black (state ‘Selected IF/Data value’).(2) The ‘Enter’ button associated to the data area of the input field displays “251” (previously entered value).EVC-150(3) Use the log file to verify that DMI does not send out packet EVC-150 as the ‘Enter’ button is disabled. Echo Texts(4) The data part of the echo text displays “++++”.(5) The data part of the echo text is coloured red");
            /*
            Test Step 3
            Action: Enter “251” (outbound) with the numeric keypad and press the data input field (Accept) in the same screen
            Expected Result: Input Field(1) The ‘Enter’ button associated to the data area of the input field is coloured grey and its text is black (state ‘Selected IF/Data value’).(2) The ‘Enter’ button associated to the data area of the input field displays “251” (previously entered value).EVC-150(3) Use the log file to verify that DMI does not send out packet EVC-150 as the ‘Enter’ button is disabled. Echo Texts(4) The data part of the echo text displays “++++”.(5) The data part of the echo text is coloured red
            Test Step Comment: Requirements:(1) MMI_gen 11823 (partly: MMI_gen 4714 (partly: state 'Selected IF/data value'));(2) MMI_gen 11823 (partly: MMI_gen 4714 (partly: previously entered (faulty) value), MMI_gen 12145 (partly: outbound)); MMI_gen 4699 (technical range);(3) MMI_gen 11823 (partly: MMI_gen 9286 (partly: button ‘Enter’, disabled), MMI_gen 12148 (partly: not send packets), MMI_gen 12147);(4) MMI_gen 11823 (partly: MMI_gen 12148 (MMI_gen 4713 (partly: indication)));(5) MMI_gen 11823 (partly: MMI_gen 12148 (MMI_gen 4713 (partly: red)));
            */
            DmiActions.ShowInstruction(this,
                "Enter ‘251’ for Brake percentage and confirm by pressing in the data input field");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘Enter’ button of the data input field displays ‘251’ in black text on a grey background." +
                                Environment.NewLine +
                                @"2. The ‘Brake percentage’ echo text displays ‘++++’ in red.");

            TraceHeader("Test Step 4");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Enter “250” (maximum inbound) with the numeric keypad and press the data input field (Accept) in the same screen");
            TraceReport("Expected Result");
            TraceInfo(
                "Input Field(1) The eventually displayed data value in the data area of the input field is replaced by “250” (character or value corresponding to the activated data key - state ‘Selected IF/value of pressed key(s)’).EVC-150(2) Use the log file to verify that DMI sends packet EVC-150 with variable:-   MMI_M_BP_CURRENT = 250");
            /*
            Test Step 4
            Action: Enter “250” (maximum inbound) with the numeric keypad and press the data input field (Accept) in the same screen
            Expected Result: Input Field(1) The eventually displayed data value in the data area of the input field is replaced by “250” (character or value corresponding to the activated data key - state ‘Selected IF/value of pressed key(s)’).EVC-150(2) Use the log file to verify that DMI sends packet EVC-150 with variable:-   MMI_M_BP_CURRENT = 250
            Test Step Comment: Requirements:(1) MMI_gen 11823 (partly: MMI_gen 4714 (partly: MMI_gen 4679), MMI_gen 9286 (partly: state switched), MMI_gen 12145 (partly: maximum inbound));(2) MMI_gen 11823 (partly: MMI_gen 12147, MMI_gen 9286 (partly: enabled));
            */
            DmiActions.ShowInstruction(this,
                @"Enter ‘250’ for Brake percentage and confirm by pressing in the data input field");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The data input field displays ‘2’ then ‘25’, then ‘250’ (replacing the value ‘251’ as the characters are entered.");

            TraceHeader("Test Step 5");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "This step is to complete the process of ‘Brake percentage’:- Press the ‘Yes’ button on the ‘Brake percentage’ window.- Validate the data in the data validation window");
            TraceReport("Expected Result");
            TraceInfo(
                "1. After pressing the ‘Yes’ button, the data validation window (‘Validate Brake percentage’) appears instead of the ‘Brake percentage’ data entry window. The data part of echo text displays “250” in white.2. After the data area of the input field containing “Yes” is pressed, the data validation window disappears and returns to the parent window (‘Settings’ window) of ‘Brake percentage’ window with enabled ‘Brake percentage’ button");
            /*
            Test Step 5
            Action: This step is to complete the process of ‘Brake percentage’:- Press the ‘Yes’ button on the ‘Brake percentage’ window.- Validate the data in the data validation window
            Expected Result: 1. After pressing the ‘Yes’ button, the data validation window (‘Validate Brake percentage’) appears instead of the ‘Brake percentage’ data entry window. The data part of echo text displays “250” in white.2. After the data area of the input field containing “Yes” is pressed, the data validation window disappears and returns to the parent window (‘Settings’ window) of ‘Brake percentage’ window with enabled ‘Brake percentage’ button
            */
            EVC51_MMIEchoedBrakePercentage.MMI_M_BP_ORIG_ = 250;
            EVC51_MMIEchoedBrakePercentage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Brake percentage validation window" + Environment.NewLine +
                                "2. The echo text for the Brake percentage value displays ‘250’ in white.");

            DmiActions.ShowInstruction(this, @"Press the ‘Yes’ button in the ‘Validate Brake percentage’ window");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Settings window with the ‘Brake percentage’ button enabled.");

            TraceHeader("Test Step 6");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("End of test");
            TraceReport("Expected Result");
            TraceInfo("");
            /*
            Test Step 6
            Action: End of test
            Expected Result: 
            */


            return GlobalTestResult;
        }
    }
}
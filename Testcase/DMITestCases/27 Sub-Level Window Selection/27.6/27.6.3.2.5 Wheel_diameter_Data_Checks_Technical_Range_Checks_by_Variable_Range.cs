using System;
using Testcase.Telegrams.EVCtoDMI;
using Testcase.Telegrams.DMItoEVC;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 27.6.3.2.5 ‘Wheel diameter’ Data Checks: Technical Range Checks by Variable Range
    /// TC-ID: 22.6.3.2.5
    /// 
    /// This test case verifies the functionalities of ‘Wheel diameter’ data entry when the data of Wheel diameter does not comply with variable-range rules of the technical range check. The function designs comply with the conditions in [MMI-ETCS-gen]. The data range and interface comply with the data information in [VSIS_gen].
    /// 
    /// Tested Requirements:
    /// MMI_gen 11757 (partly: MMI_gen 4713, MMI_gen 12145, MMI_gen 12147, MMI_gen 12148, MMI_gen 4714); MMI_gen 4699 (technical range);
    /// 
    /// Scenario:
    /// 1.Activate the cabin.
    /// 2.Press the ‘Settings’ button in the ‘Driver ID’ window. Then, the ‘Settings’ window is opened.
    /// 3.Press the ‘Maintenance’ button. Then, the ‘Password’ window is opened.
    /// 4.Enter the password of “26728290”. Then, the ‘Maintenance’ window is opened.
    /// 5.Press the ‘Wheel diameter’ button. Then, the ‘Wheel diameter’ window is opened.
    /// 6.Enter the data of Wheel diameter. Then, the ‘Wheel diameter’ window is verified by the following events:a.   The minimum DMI-technical-inbound data of Wheel diameter is entered and accepted.b.   The DMI-technical-outbound data of Wheel diameter is entered and accepted.c.   The maximum DMI-technical-inbound data of Wheel diameter is entered and accepted.
    /// 
    /// Used files:
    /// N/A
    /// </summary>
    public class TC_ID_22_6_2_5_Wheel_diameter : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:

            // Call the TestCaseBase PreExecution
            base.PreExecution();

            // 1. The test environment is powered on.2. The cabin is activated.3. The ‘Settings’ window is opened.
            DmiActions.Start_ATP();
            DmiActions.Activate_Cabin_1(this);
            DmiActions.Set_Driver_ID(this, "1234");
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Default;
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH =
                EVC30_MMIRequestEnable.EnabledRequests.EnableWheelDiameter;
            EVC30_MMIRequestEnable.Send();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // ETCS-DMI is in the ‘Stand-By’ mode.

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 0;
            // Testcase entrypoint


            TraceHeader("Test Step 1");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Open the ‘Wheel diameter’ data entry window from the Settings menu");
            TraceReport("Expected Result");
            TraceInfo(
                "The ‘Wheel diameter’ data entry window appears on ETCS-DMI screen instead of the ‘Settings’ menu window");
            /*
            Test Step 1
            Action: Open the ‘Wheel diameter’ data entry window from the Settings menu
            Expected Result: The ‘Wheel diameter’ data entry window appears on ETCS-DMI screen instead of the ‘Settings’ menu window
            */
            DmiActions.ShowInstruction(this,
                "Press the ‘Maintenance’ button and enter the password ‘26728290’ in the Password Maintenance window, then press the ‘Wheel Diameter’ button");

            EVC40_MMICurrentMaintenanceData.MMI_Q_MD_DATASET = Variables.MMI_Q_MD_DATASET.WheelDiameter;
            EVC40_MMICurrentMaintenanceData.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Wheel Diameter window.");


            TraceHeader("Test Step 2");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Enter the minimum inbound values with the numeric keypad and press the data input field (Accept) in the same screen, for the following fields below, Wheel diameter 1: 500Wheel diameter 2: 500Accuracy: 0");
            TraceReport("Expected Result");
            TraceInfo(
                "Input Field(1) The eventually displayed data value in the data area of the input field is replaced by the previously entered values (character or value corresponding to the activated data key - state ‘Selected IF/value of pressed key(s)’):Wheel diameter 1: 500Wheel diameter 2: 500Accuracy: 0EVC-140(2) Use the log file to verify that DMI sends packet EVC-140 with variable:-   MMI_M_SDU_WHEEL_SIZE_1 = 500-   MMI_M_SDU_WHEEL_SIZE_2 = 500-   MMI_M_WHEEL_SIZE_ERR = 0-   MMI_Q_MD_DATASET = 0 (Wheel diameter)");
            /*
            Test Step 2
            Action: Enter the minimum inbound values with the numeric keypad and press the data input field (Accept) in the same screen, for the following fields below, Wheel diameter 1: 500Wheel diameter 2: 500Accuracy: 0
            Expected Result: Input Field(1) The eventually displayed data value in the data area of the input field is replaced by the previously entered values (character or value corresponding to the activated data key - state ‘Selected IF/value of pressed key(s)’):Wheel diameter 1: 500Wheel diameter 2: 500Accuracy: 0EVC-140(2) Use the log file to verify that DMI sends packet EVC-140 with variable:-   MMI_M_SDU_WHEEL_SIZE_1 = 500-   MMI_M_SDU_WHEEL_SIZE_2 = 500-   MMI_M_WHEEL_SIZE_ERR = 0-   MMI_Q_MD_DATASET = 0 (Wheel diameter)
            Test Step Comment: Requirements:(1) MMI_gen 11757 (partly:  MMI_gen 4714 (partly: MMI_gen 4679),  MMI_gen 12145 (partly: minimum inbound));(2) MMI_gen 11757 (partly:  MMI_gen 12147)
            */
            DmiActions.ShowInstruction(this,
                "Using the numeric keypad enter ‘500’ for Wheel Diameter 1 and press its data input field" +
                Environment.NewLine +
                "Enter ‘500’ for Wheel Diameter 2 and press its data input field" + Environment.NewLine +
                "Enter ‘0’ for Accuracy and press its data input field");

            EVC140_MMINewMaintenanceData.MMI_Q_MD_DATASET = Variables.MMI_Q_MD_DATASET.WheelDiameter;
            EVC140_MMINewMaintenanceData.MMI_M_SDU_WHEEL_SIZE_1 = (Variables.MMI_M_SDU_WHEEL_SIZE) 500; // A check fn.?
            EVC140_MMINewMaintenanceData.MMI_M_SDU_WHEEL_SIZE_2 = (Variables.MMI_M_SDU_WHEEL_SIZE) 500;
            EVC140_MMINewMaintenanceData.MMI_M_WHEEL_SIZE_ERR = (Variables.MMI_M_WHEEL_SIZE_ERR) 0;
            EVC140_MMINewMaintenanceData.CheckTelegram();
            EVC40_MMICurrentMaintenanceData.MMI_M_SDU_WHEEL_SIZE_1 = (Variables.MMI_M_SDU_WHEEL_SIZE) 500;
            EVC40_MMICurrentMaintenanceData.MMI_M_SDU_WHEEL_SIZE_2 = (Variables.MMI_M_SDU_WHEEL_SIZE) 500;
            EVC40_MMICurrentMaintenanceData.MMI_M_WHEEL_SIZE_ERR = (Variables.MMI_M_WHEEL_SIZE_ERR) 0;
            EVC40_MMICurrentMaintenanceData.Send();

            // MMI_gen 4651 gives the selected state as this:
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The values entered are displayed in black on a Medium-grey background.");

            TraceHeader("Test Step 3");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Enter the outbound value with the numeric keypad and press the data input field (Accept) in the same screen, for the following fields below, Wheel diameter 1: 1501Wheel diameter 2: 499Accuracy: 33");
            TraceReport("Expected Result");
            TraceInfo(
                "Input Field(1) The ‘Enter’ button associated to the data area of the input field is coloured grey and its text is black (state ‘Selected IF/Data value’).(2) The ‘Enter’ button associated to the data area of the input field displays the previously entered value:Wheel diameter 1: 1501Wheel diameter 2: 499Accuracy: 33EVC-140(3) Use the log file to verify that DMI does not send out packet EVC-140 as the ‘Enter’ button is disabled. Echo Texts(4) The data part of the echo text displays “++++”.(5) The data part of the echo text is coloured red");
            /*
            Test Step 3
            Action: Enter the outbound value with the numeric keypad and press the data input field (Accept) in the same screen, for the following fields below, Wheel diameter 1: 1501Wheel diameter 2: 499Accuracy: 33
            Expected Result: Input Field(1) The ‘Enter’ button associated to the data area of the input field is coloured grey and its text is black (state ‘Selected IF/Data value’).(2) The ‘Enter’ button associated to the data area of the input field displays the previously entered value:Wheel diameter 1: 1501Wheel diameter 2: 499Accuracy: 33EVC-140(3) Use the log file to verify that DMI does not send out packet EVC-140 as the ‘Enter’ button is disabled. Echo Texts(4) The data part of the echo text displays “++++”.(5) The data part of the echo text is coloured red
            Test Step Comment: Requirements:(1) MMI_gen 11757 (partly:  MMI_gen 4714 (partly: state 'Selected IF/data value'));(2) MMI_gen 11757 (partly:  MMI_gen 4714 (partly: previously entered (faulty) value), MMI_gen 12145 (partly: outbound)); MMI_gen 4699 (technical range);(3) MMI_gen 11757 (partly: MMI_gen 12148 (partly: not send packets) , MMI_gen 12147);(4) MMI_gen 11757 (partly:  MMI_gen 12148 (MMI_gen 4713 (partly: indication)));(5) MMI_gen 11757 (partly:  MMI_gen 12148 (MMI_gen 4713 (partly: red)));
            */
            DmiActions.ShowInstruction(this,
                "Using the numeric keypad enter ‘1501’ for Wheel Diameter 1 and press its data input field" +
                Environment.NewLine +
                "Enter ‘499’ for Wheel Diameter 2 and press its data input field" + Environment.NewLine +
                "Enter ‘33’ for Accuracy and press its data input field");

            EVC40_MMICurrentMaintenanceData.MMI_M_SDU_WHEEL_SIZE_1 =
                Variables.MMI_M_SDU_WHEEL_SIZE.TechnicalRangeCheckFailed;
            EVC40_MMICurrentMaintenanceData.MMI_M_SDU_WHEEL_SIZE_2 =
                Variables.MMI_M_SDU_WHEEL_SIZE.TechnicalRangeCheckFailed;
            EVC40_MMICurrentMaintenanceData.MMI_M_WHEEL_SIZE_ERR =
                Variables.MMI_M_WHEEL_SIZE_ERR.TechnicalRangeCheckFailed;
            EVC40_MMICurrentMaintenanceData.Send();

            // Note:MMI_gen 4651
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The values entered are displayed in black on a grey background." +
                                Environment.NewLine +
                                "2. The echo texts corresponding to the values entered display ‘++++’ in red.");

            TraceHeader("Test Step 4");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Enter the maximum inbound values with the numeric keypad and press the data input field (Accept) in the same screen, for the following fields below, Wheel diameter 1: 1500Wheel diameter 2: 1500Accuracy: 32Then, press the ‘Yes’ button");
            TraceReport("Expected Result");
            TraceInfo(
                "Input Field(1) The eventually displayed data value in the data area of the input field is replaced by the entered value (character or value corresponding to the activated data key - state ‘Selected IF/value of pressed key(s)’):Wheel diameter 1: 1500Wheel diameter 2: 1500Accuracy: 32EVC-140(2) Use the log file to verify that DMI sends packet EVC-140 with variable: -   MMI_M_SDU_WHEEL_SIZE_1 = 1500-   MMI_M_SDU_WHEEL_SIZE_2 = 1500-   MMI_M_WHEEL_SIZE_ERR = 32-   MMI_Q_MD_DATASET = 0 (Wheel diameter)");
            /*
            Test Step 4
            Action: Enter the maximum inbound values with the numeric keypad and press the data input field (Accept) in the same screen, for the following fields below, Wheel diameter 1: 1500Wheel diameter 2: 1500Accuracy: 32Then, press the ‘Yes’ button
            Expected Result: Input Field(1) The eventually displayed data value in the data area of the input field is replaced by the entered value (character or value corresponding to the activated data key - state ‘Selected IF/value of pressed key(s)’):Wheel diameter 1: 1500Wheel diameter 2: 1500Accuracy: 32EVC-140(2) Use the log file to verify that DMI sends packet EVC-140 with variable: -   MMI_M_SDU_WHEEL_SIZE_1 = 1500-   MMI_M_SDU_WHEEL_SIZE_2 = 1500-   MMI_M_WHEEL_SIZE_ERR = 32-   MMI_Q_MD_DATASET = 0 (Wheel diameter)
            Test Step Comment: Requirements:(1) MMI_gen 11757 (partly: MMI_gen 4714 (partly: MMI_gen 4679), MMI_gen 12145 (partly: maximum inbound));(2) MMI_gen 11757 (partly: MMI_gen 12147);
            */
            DmiActions.ShowInstruction(this,
                "Using the numeric keypad enter ‘1500’ for Wheel Diameter 1 and press its data input field" +
                Environment.NewLine +
                "Enter ‘1500’ for Wheel Diameter 2 and press its data input field" + Environment.NewLine +
                "Enter ‘32’ for Accuracy and press its data input field");

            EVC140_MMINewMaintenanceData.MMI_M_SDU_WHEEL_SIZE_1 = (Variables.MMI_M_SDU_WHEEL_SIZE) 1500;
            EVC140_MMINewMaintenanceData.MMI_M_SDU_WHEEL_SIZE_2 = (Variables.MMI_M_SDU_WHEEL_SIZE) 1500;
            EVC140_MMINewMaintenanceData.MMI_M_WHEEL_SIZE_ERR = (Variables.MMI_M_WHEEL_SIZE_ERR) 32;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The values entered are displayed selected.");

            TraceHeader("Test Step 5");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "This step is to complete the process of ‘Wheel diameter’:- Press the ‘Yes’ button on the ‘Wheel diameter’ window.- Validate the data in the data validation window");
            TraceReport("Expected Result");
            TraceInfo(
                "1. After pressing the ‘Yes’ button, the data validation window (‘Validate Wheel diameter’) appears instead of the ‘Wheel diameter’ data entry window. The data part of echo text displays in white:Wheel diameter 1: 1500Wheel diameter 2: 1500Accuracy: 322. After the data area of the input field containing “Yes” is pressed, the data validation window disappears and returns to the parent window (‘Settings’ window) of ‘Wheel diameter’ window with enabled ‘Wheel diameter’ button");
            /*
            Test Step 5
            Action: This step is to complete the process of ‘Wheel diameter’:- Press the ‘Yes’ button on the ‘Wheel diameter’ window.- Validate the data in the data validation window
            Expected Result: 1. After pressing the ‘Yes’ button, the data validation window (‘Validate Wheel diameter’) appears instead of the ‘Wheel diameter’ data entry window. The data part of echo text displays in white:Wheel diameter 1: 1500Wheel diameter 2: 1500Accuracy: 322. After the data area of the input field containing “Yes” is pressed, the data validation window disappears and returns to the parent window (‘Settings’ window) of ‘Wheel diameter’ window with enabled ‘Wheel diameter’ button
            */
            DmiActions.ShowInstruction(this, "Press the ‘Yes’ button in the ‘Wheel diameter’ window");

            EVC41_MMIEchoedMaintenanceData.MMI_M_SDU_WHEEL_SIZE_1_ = (Variables.MMI_M_SDU_WHEEL_SIZE) 1500;
            EVC41_MMIEchoedMaintenanceData.MMI_M_SDU_WHEEL_SIZE_2_ = (Variables.MMI_M_SDU_WHEEL_SIZE) 1500;
            EVC41_MMIEchoedMaintenanceData.MMI_M_WHEEL_SIZE_ERR_ = (Variables.MMI_M_WHEEL_SIZE_ERR) 32;
            EVC41_MMIEchoedMaintenanceData.MMI_Q_MD_DATASET_ = Variables.MMI_Q_MD_DATASET.WheelDiameter;
            EVC41_MMIEchoedMaintenanceData.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The Validate Wheel Diameter window is displayed." + Environment.NewLine +
                                "2. The Wheel Diameter 1 data input field displays ‘1500’." + Environment.NewLine +
                                "3. The Wheel Diameter 2 data input field displays ‘1500’." + Environment.NewLine +
                                "4. The Accuracy data input field displays ‘32’." + Environment.NewLine +
                                "5. The echo texts are displayed in white.");

            DmiActions.ShowInstruction(this,
                "Validate the data in the data validation window by pressing in the ‘Yes’ data input field");

            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Settings; // settings window
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH =
                EVC30_MMIRequestEnable.EnabledRequests.EnableWheelDiameter;
            EVC30_MMIRequestEnable.Send();

            // Don't understand the spec here...
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI removes the Validate Wheel Diameter window and displays the Maintenance window with the ‘Wheel Diameter’ button enabled.");

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
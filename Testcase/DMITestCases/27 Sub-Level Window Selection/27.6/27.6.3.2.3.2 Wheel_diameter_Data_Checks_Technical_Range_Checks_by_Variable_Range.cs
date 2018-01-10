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
    /// 27.6.3.2.3.2 ‘Wheel diameter’ Data Checks: Technical Range Checks by Data Validity
    /// TC-ID: 22.6.3.2.3.2
    /// 
    /// This test case verifies the functionalities of ‘Wheel diameter’ data entry when the data of Wheel diameter does not comply with variable-range rules of the technical range check. The function designs comply with the conditions in [MMI-ETCS-gen]. The data range and interface comply with the data information in [VSIS_gen].
    /// 
    /// Tested Requirements:
    /// MMI_gen 11752 (partly: technical range); MMI_gen 11754 (partly: technical range); MMI_gen 11756 (partly: technical range); MMI_gen 11757 (partly: MMI_gen 12148, MMI_gen 4713, MMI_gen 4714, MMI_gen 4679, MMI_gen 12147); MMI_gen 9310 (partly: technical range); MMI_gen 4699 (technical range);
    /// 
    /// Scenario:
    /// 1.Activate the cabin.
    /// 2.Press the ‘Settings’ button in the ‘Driver ID’ window. Then, the ‘Settings’ window is opened.
    /// 3.Press the ‘Maintenance’ button. Then, the ‘Password’ window is opened.
    /// 4.Enter the password of “26728290”. Then, the ‘Maintenance’ window is opened.
    /// 5.Press the ‘Wheel diameter’ button. Then, the ‘Wheel diameter’ window is opened.
    /// 6.Enter the data of Wheel diameter. Then, the ‘Wheel diameter’ window is verified by the following events:a.Enter and accept the invalid data.b.Confirm the data by pressing the ‘Yes’ buttonc.Repeat a. by valid data in order that the data is entered and accepted after the ‘Enter’ button is disabled.
    /// 
    /// Used files:
    /// 22_6_3_2_3_2.xml
    /// </summary>
    public class TC_ID_22_6_2_3_2_Wheel_diameter : TestcaseBase
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
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Level = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_LEVEL.L1;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.StandBy;
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH =
                EVC30_MMIRequestEnable.EnabledRequests.EnableWheelDiameter;
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Default;
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
            // Testcase entrypoint


            /*
            Test Step 1
            Action: Open the ‘Wheel diameter’ data entry window from the Settings menu
            Expected Result: The ‘Wheel diameter’ data entry window appears on ETCS-DMI screen instead of the ‘Settings’ menu window
            */
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Default;
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH =
                EVC30_MMIRequestEnable.EnabledRequests.EnableWheelDiameter;
            EVC30_MMIRequestEnable.Send();

            DmiActions.ShowInstruction(this,
                @"Press the Settings button then press the ‘Wheel diameter’ button in the Settings window");

            EVC40_MMICurrentMaintenanceData.MMI_Q_MD_DATASET = Variables.MMI_Q_MD_DATASET.WheelDiameter;
            EVC40_MMICurrentMaintenanceData.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI removes the Settings window and displays the Wheel diameter window.");

            /*
            Test Step 2
            Action: Enter the invalid value with the numeric keypad and, also press the data input fields (Accept) in the same screen, for the following fields below, Wheel diameter 1: 500 Wheel diameter 2: 500 Accuracy: 1
            Expected Result: Use the log file to verify that DMI receives packet EVC-40 with variable:(1) MMI_M_SDU_WHEEL_SIZE_1 = 65530 (Technical Range Check failed)(2) MMI_M_SDU_WHEEL_SIZE_2 = 65530 (Technical Range Check failed)(3) MMI_M_WHEEL_SIZE_ERR = 250 (Technical Range Check failed)(4) MMI_Q_MD_DATASET = 0 (Wheel diameter)Input Field (All)(5) The ‘Enter’ button associated to the data area of the input field is coloured grey and its text is black (state ‘Selected IF/Data value’).(6) The ‘Enter’ button associated to the data area of the input field displays the previously entered value:Wheel diameter 1: 500Wheel diameter 2: 500Accuracy: 1Echo Texts (All)(7) The data parts of the echo texts display “++++”.(8) The data parts of the echo texts are coloured red.
            Test Step Comment: Requirements: MMI_gen 11752 (partly: EVC-40, 65530, Technical Range); MMI_gen 11754 (partly: EVC-40, 65530, Technical Range);MMI_gen 11756 (partly: EVC-40, 250, Technical Range);MMI_gen 11752 (partly: DATASET); MMI_gen 11754 (partly: DATASET); MMI_gen 11756 (partly: DATASET);MMI_gen 11757 (partly: MMI_gen 4714 (partly: state 'Selected IF/data value')); MMI_gen 9310 (partly: accept data);MMI_gen 11757 (partly: MMI_gen 4714 (partly: previously entered (faulty) value)); MMI_gen 4699 (technical range);MMI_gen 11757 (partly: MMI_gen 4713 (partly: indication)); MMI_gen 9310 (partly: [technical range, No OK, echo text]); MMI_gen 11752 (partly: echo part, Technical Range); MMI_gen 11754 (partly: echo part, Technical Range); MMI_gen 11756 (partly: echo part, Technical Range);MMI_gen 11757 (partly: MMI_gen 4713 (partly: red)); MMI_gen 11752 (partly: echo part, Technical Range); MMI_gen 11754 (partly: echo part, Technical Range); MMI_gen 11756 (partly: echo part, Technical Range);
            */
            DmiActions.ShowInstruction(this,
                "Using the numeric keypad, enter the value ‘500’ for Wheel Diameter 1 and press on the data input field to accept the value" +
                Environment.NewLine +
                "Enter the value ‘500’ for Wheel Diameter 2 and press on the data input field to accept the value" +
                Environment.NewLine +
                "Enter the value ‘1’ for Accuracy and press on the data input field to accept the value");

            // What should happen here is that EVC140 is sent when the data input is confirmed and ETCS responds with a validation EVC40 packet
            // EVC140_MMINewMaintenanceData.MMI_M_SDU_WHEEL_SIZE_1 = 500 et c.
            // ETCS should respond with EVC40...
            // The values specified are all valid according to VSIS
            //EVC40_MMICurrentMaintenanceData.MMI_M_SDU_WHEEL_SIZE_1 = (Variables.MMI_M_SDU_WHEEL_SIZE)500;
            //EVC40_MMICurrentMaintenanceData.MMI_M_SDU_WHEEL_SIZE_1 = (Variables.MMI_M_SDU_WHEEL_SIZE)500;
            //EVC40_MMICurrentMaintenanceData.MMI_M_WHEEL_SIZE_ERR = (Variables.MMI_M_WHEEL_SIZE_ERR)1;
            // ... and NOT:
            EVC40_MMICurrentMaintenanceData.MMI_M_SDU_WHEEL_SIZE_1 =
                Variables.MMI_M_SDU_WHEEL_SIZE.TechnicalRangeCheckFailed;
            EVC40_MMICurrentMaintenanceData.MMI_M_SDU_WHEEL_SIZE_2 =
                Variables.MMI_M_SDU_WHEEL_SIZE.TechnicalRangeCheckFailed;
            EVC40_MMICurrentMaintenanceData.MMI_M_WHEEL_SIZE_ERR =
                Variables.MMI_M_WHEEL_SIZE_ERR.TechnicalRangeCheckFailed;
            EVC40_MMICurrentMaintenanceData.MMI_Q_MD_DATASET = Variables.MMI_Q_MD_DATASET.WheelDiameter;
            EVC40_MMICurrentMaintenanceData.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text of the data parts of the input field are in black on a grey background." +
                                Environment.NewLine +
                                "2. The data input fields display the values ‘500’, ‘500’ and ‘1’." +
                                Environment.NewLine +
                                "3. The data parts of the echo texts display ‘++++’ in red.");

            /*
            Test Step 3
            Action: Enter the valid value with the numeric keypad and, also press the data input fields (Accept) in the same screen, for the following fields below, Wheel diameter 1: 1000 Wheel diameter 2: 1500 Accuracy: 5Then, Observe the echo texts on the left hand side. Press the ‘Yes’ button.
            Expected Result: The eventually displayed data value in the data area of the input field is replaced by the entered value (character or value corresponding to the activated data key - state ‘Selected IF/value of pressed key(s)’):Wheel diameter 1: 1000Wheel diameter 2: 1500Accuracy: 5
            Test Step Comment: MMI_gen 11757 (partly: MMI_gen 4714 (partly: MMI_gen 4679), MMI_gen 9310 (partly: press one key);
            */
            DmiActions.ShowInstruction(this,
                "Using the numeric keypad, enter the value ‘1000’ for Wheel Diameter 1 and press on the data input field to accept the value" +
                Environment.NewLine +
                "Enter the value ‘1000’ for Wheel Diameter 2 and press on the data input field to accept the value" +
                Environment.NewLine +
                "Enter the value ‘5’ for Accuracy and press on the data input field to accept the value");

            EVC40_MMICurrentMaintenanceData.MMI_M_SDU_WHEEL_SIZE_1 = (Variables.MMI_M_SDU_WHEEL_SIZE) 1000;
            EVC40_MMICurrentMaintenanceData.MMI_M_SDU_WHEEL_SIZE_2 = (Variables.MMI_M_SDU_WHEEL_SIZE) 1000;
            EVC40_MMICurrentMaintenanceData.MMI_M_WHEEL_SIZE_ERR = (Variables.MMI_M_WHEEL_SIZE_ERR) 5;
            EVC40_MMICurrentMaintenanceData.MMI_Q_MD_DATASET = Variables.MMI_Q_MD_DATASET.WheelDiameter;
            EVC40_MMICurrentMaintenanceData.Send();
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The data input fields display the values ‘1000’, ‘1000’ and ‘5’ in black on a grey background.");


            /*
            Test Step 4
            Action: This step is to complete the process of ‘Wheel diameter’: Press the ‘Yes’ button on the ‘Wheel diameter’ window. Validate the data in the data validation window.
            Expected Result: 1. After pressing the ‘Yes’ button, the data validation window (‘Validate Wheel diameter’) appears instead of the ‘Wheel diameter’ data entry window. The data part of echo text displays in white:Wheel diameter 1: 1000Wheel diameter 2: 1500Accuracy: 52. After the data area of the input field containing “Yes” is pressed, the data validation window disappears and returns to the parent window (‘Settings’ window) of ‘Wheel diameter’ window with enabled ‘Wheel diameter’ button.
            */
            DmiActions.ShowInstruction(this, "Press the ‘Yes’ button");

            EVC41_MMIEchoedMaintenanceData.MMI_M_WHEEL_SIZE_ERR_ = (Variables.MMI_M_WHEEL_SIZE_ERR) 5;
            EVC41_MMIEchoedMaintenanceData.MMI_M_SDU_WHEEL_SIZE_2_ = (Variables.MMI_M_SDU_WHEEL_SIZE) 100;
            EVC41_MMIEchoedMaintenanceData.MMI_M_SDU_WHEEL_SIZE_1_ = (Variables.MMI_M_SDU_WHEEL_SIZE) 1000;
            EVC41_MMIEchoedMaintenanceData.MMI_Q_MD_DATASET_ = Variables.MMI_Q_MD_DATASET.WheelDiameter;
            EVC41_MMIEchoedMaintenanceData.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Validate Wheel Diameter window");

            EVC40_MMICurrentMaintenanceData.MMI_M_SDU_WHEEL_SIZE_1 = (Variables.MMI_M_SDU_WHEEL_SIZE) 1000;
            EVC40_MMICurrentMaintenanceData.MMI_M_SDU_WHEEL_SIZE_2 = (Variables.MMI_M_SDU_WHEEL_SIZE) 1000;
            EVC40_MMICurrentMaintenanceData.MMI_M_WHEEL_SIZE_ERR = (Variables.MMI_M_WHEEL_SIZE_ERR) 5;
            EVC40_MMICurrentMaintenanceData.MMI_Q_MD_DATASET = Variables.MMI_Q_MD_DATASET.WheelDiameter;
            EVC40_MMICurrentMaintenanceData.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI removes the Validate Wheel Diameter window and displays the Wheel Diameter data entry window");

            /*
            Test Step 5
            Action: Send the data of ‘Technical Range Check’ failure to ETCS-DMI by 22_6_3_2_3_2_a.xml EVC-40 MMI_Q_MD_DATASET = 0MMI_M_SDU_WHEEL_SIZE_1 =65530MMI_M_SDU_WHEEL_SIZE_2 = 65530MMI_M_WHEEL_SIZE_ERR = 250
            Expected Result: Input Field (All)(1) The ‘Enter’ button associated to the data area of the input field displays the previously entered value.Echo Texts (All)(2) The data part of the echo text displays “++++”.
            Test Step Comment: Requirements: (1) MMI_gen 11757 (partly: MMI_gen 4714 (partly: previously entered (faulty) value)); MMI_gen 4699 (technical range);(2) MMI_gen 11757 (partly: MMI_gen 12148 (MMI_gen 4713 (partly: indication)));
            */

            #region Send_XML_22_6_3_2_3_2_DMI_Test_Specification

            EVC40_MMICurrentMaintenanceData.MMI_Q_MD_DATASET = Variables.MMI_Q_MD_DATASET.WheelDiameter;
            EVC40_MMICurrentMaintenanceData.MMI_M_SDU_WHEEL_SIZE_1 =
                Variables.MMI_M_SDU_WHEEL_SIZE.TechnicalRangeCheckFailed;
            EVC40_MMICurrentMaintenanceData.MMI_M_SDU_WHEEL_SIZE_2 =
                Variables.MMI_M_SDU_WHEEL_SIZE.TechnicalRangeCheckFailed;
            EVC40_MMICurrentMaintenanceData.MMI_M_WHEEL_SIZE_ERR =
                Variables.MMI_M_WHEEL_SIZE_ERR.TechnicalRangeCheckFailed;

            EVC40_MMICurrentMaintenanceData.Send();

            #endregion

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The data part of the data input fields display the values entered previously." +
                                Environment.NewLine +
                                "2. The echo text values display ‘++++’");
            /*
            Test Step 6
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
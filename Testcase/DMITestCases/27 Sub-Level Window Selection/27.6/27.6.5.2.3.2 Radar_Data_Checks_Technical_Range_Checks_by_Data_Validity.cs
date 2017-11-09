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
using Testcase.Telegrams.DMItoEVC;
using Testcase.Telegrams.EVCtoDMI;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 27.6.5.2.3.2 Radar Data Checks: Technical Range Checks by Data Validity
    /// TC-ID: 22.6.5.2.3.2
    /// 
    /// This test case verifies the functionalities of the ‘Radar’ data entry when the data of Radar does not comply with data-validity rules of the technical range check.The function designs comply with the conditions in [MMI-ETCS-gen]. The data range and interface comply with the data information in [VSIS_gen].
    /// 
    /// Tested Requirements:MMI_gen 11776 (partly: technical range);MMI_gen 11785 (partly: MMI_gen 12148, MMI_gen 4713, MMI_gen 4714, MMI_gen 4679, MMI_gen 12147); MMI_gen 9310 (partly: technical range); MMI_gen 4699 (technical range);

    /// 
    /// 
    /// Scenario:
    /// 1.  Activate the cabin.2.   Press the ‘Settings’ button in the ‘Driver ID’ window.Then, the ‘Settings’ window is opened.3.   Press the ‘Maintenance’ button.Then, the ‘Password’ window is opened.4.   Enter the password of “26728290”. Then, the ‘Maintenance’ window is opened.5.   Press the ‘Radar’ button.Then, the ‘Radar’ window is opened.6.   Enter the data of Radar.Then, the ‘Radar’ window is verified by the following events:a.Enter and accept the invalid data.b.Confirm the data by pressing the ‘Yes’ button           c.   Repeat a. by valid data in order that the data is entered and accepted after the ‘Enter’ button is disabled.
    /// 
    /// Used files: 22_6_5_2_3_2_a.xml
    /// N/A
    /// </summary>
    public class TC_22_6_5_2_3_2_Radar_Data_Checks : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec: 
            // 1. The test environment is powered on.
            // 2.The cabin is activated.
            // 3.The ‘Settings’ window is opened.

            // 

            // Call the TestCaseBase PreExecution
            base.PreExecution();

            DmiActions.Start_ATP();
            DmiActions.Activate_Cabin_1(this);
            DmiActions.Set_Driver_ID(this, "1234");
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Settings;  // settings window
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.EnableDoppler;
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
            Action: Open the ‘Radar’ data entry window from the Settings menu.
            Expected Result: The ‘Radar’ data entry window appears on ETCS-DMI screen instead of the ‘Settings’ menu window.
            Test Step Comment: Requirements:(1) MMI_gen 11776 (partly: EVC-40, 65530, Technical Range);(2) MMI_gen 11776 (partly: DATASET);(3) MMI_gen 11785 (partly: MMI_gen 4714 (partly: state 'Selected IF/data value')); MMI_gen 9310 (partly: accept data);(4) MMI_gen 11785 (partly: MMI_gen 4714 (partly: previously entered (faulty) value)); MMI_gen 4699 (technical range);(5) MMI_gen 11785 (partly: MMI_gen 4713 (partly: indication)); MMI_gen 9310 (partly: [technical range, No OK, echo text]); MMI_gen 11776 (partly: echo part, Technical Range);(6) MMI_gen 11785 (partly: MMI_gen 4713 (partly: red)); MMI_gen 11776 (partly: echo part, Technical Range); 
            */
            DmiActions.ShowInstruction(this, "Press the ‘Maintenance’ button and enter the password from the PASS_CODE_MTN in the configuration file " + Environment.NewLine +
                                             "in the Password Maintenance window, then press the ‘Radar’ button");
            
            EVC40_MMICurrentMaintenanceData.MMI_Q_MD_DATASET = Variables.MMI_Q_MD_DATASET.Doppler;
            EVC40_MMICurrentMaintenanceData.MMI_M_PULSE_PER_KM_1 = Variables.MMI_M_PULSE_PER_KM.NoRadarOnBoard;
            EVC40_MMICurrentMaintenanceData.MMI_M_PULSE_PER_KM_2 = Variables.MMI_M_PULSE_PER_KM.NoRadarOnBoard;
            EVC40_MMICurrentMaintenanceData.Send();
            
            DmiActions.ShowInstruction(this, @"Press the ‘Radar’ button");


            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. DMI displays the Radar window");

            /*
            Test Step 2
            Action: Enter the invalid value with the numeric keypad and, also press the data input fields (Accept) in the same screen, for the following fields below, Radar 1: 20001 Then, Observe the echo texts on the left hand side.
            Expected Result: EVC-40Use the log file to verify that DMI receives packet EVC-40 with variable:(1) MMI_M_PULSE_PER_KM_1 = 4294967290 (Technical Range Check failed)(2) MMI_Q_MD_DATASET = 1 (Radar)Input Field (All)(3) The ‘Enter’ button associated to the data area of the input field is coloured grey and its text is black (state ‘Selected IF/Data value’).(4) The ‘Enter’ button associated to the data area of the input field displays the previously entered value:Radar 1: 20001Echo Texts (All)(5) The data parts of the echo texts display “++++”.(6) The data parts of the echo texts are coloured red.
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Enter the (invalid) value ‘20001’ for Radar 1 and confirm by pressing the data input field");

            EVC40_MMICurrentMaintenanceData.MMI_Q_MD_DATASET = Variables.MMI_Q_MD_DATASET.Doppler;
            EVC40_MMICurrentMaintenanceData.MMI_M_PULSE_PER_KM_1 = Variables.MMI_M_PULSE_PER_KM.TechnicalRangeCheckFailed;
            EVC40_MMICurrentMaintenanceData.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The Data area of the data input field (as an ‘Enter’ button) displays the value entered (‘Selected’) in black on a grey background." + Environment.NewLine +
                                @"2. The echo texts display ‘++++’ in red.");

            /*
            Test Step 3
            Action: Enter the valid value with the numeric keypad and, also press the data input fields (Accept) in the same screen, for the following fields below, Radar 1: 85534Then, Observe the echo texts on the left hand side
            Expected Result: Input Field (All)(1) The eventually displayed data value in the data area of the input field is replaced by the entered value (character or value corresponding to the activated data key - state ‘Selected IF/value of pressed key(s)’):Radar 1: 85534
            Test Step Comment: MMI_gen 11785 (partly: MMI_gen 4714 (partly: MMI_gen 4679), MMI_gen 9310 (partly: press one key);
            */
            DmiActions.ShowInstruction(this, @"Enter the (valid) value ‘85534’ for Radar 1 and confirm by pressing the data input field");
            
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The value ‘85534’ is displayed in the data input field.");

            /*
            Test Step 4
            Action: This step is to complete the process of ‘Radar’:Press the ‘Yes’ button on the ‘Radar’ window. Validate the data in the data validation window.
            Expected Result: 1. After pressing the ‘Yes’ button, the data validation window (‘Validate Radar’) appears instead of the ‘Radar’ data entry window. The data part of echo text displays in white:Radar 1: 855342. After the data area of the input field containing “Yes” is pressed, the data validation window disappears and returns to the parent window (‘Settings’ window) of ‘Radar’ window with enabled ‘Radar’ button.1
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Yes’ button in the Radar window.");

            // Open the Radar validation window
            EVC41_MMIEchoedMaintenanceData.MMI_M_PULSE_PER_KM_1_ = (Variables.MMI_M_PULSE_PER_KM)85534;
            EVC41_MMIEchoedMaintenanceData.MMI_Q_MD_DATASET_ = Variables.MMI_Q_MD_DATASET.Doppler;
            EVC41_MMIEchoedMaintenanceData.Send();

            // Spec says cursor is underneath the character entered but gen 4690 says under the next character to be entered...
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. DMI removes the Radar window and displays the ‘Validate Radar’ window." + Environment.NewLine +
                                @"2. Echo text is displayed in white.");

            DmiActions.ShowInstruction(this, @"Validate the data in the data validation window by pressing in the data area of the data input field displaying ‘Yes’");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Settings window with the ‘Radar’ button enabled.");

            /*
            Test Step 5
            Action: Send the data of ‘Technical Range Check’ failure to ETCS-DMI by 22_6_5_2_3_2_a.xmlEVC-40MMI_Q_MD_DATASET = 1MMI_M_PULSE_PER_KM_1 = 4294967290
            Expected Result: Input Field (All)(1) The ‘Enter’ button associated to the data area of the input field displays the previously entered value.Echo Texts (All)(2) The data part of the echo text displays “++++”.
            Test Step Comment: MMI_gen 11785 (partly: MMI_gen 4714 (partly: previously entered (faulty) value)); MMI_gen 4699 (technical range); MMI_gen 11785 (partly: MMI_gen 12148 (MMI_gen 4713 (partly: indication))); Note: This is a temporary approach for non-support test environment on the data checks.
            */
            // Call generic Action Method
            #region Send_XML_22_6_5_2_3_2_DMI_Test_Specification
            EVC40_MMICurrentMaintenanceData.MMI_Q_MD_DATASET = Variables.MMI_Q_MD_DATASET.Doppler;
            EVC40_MMICurrentMaintenanceData.MMI_M_PULSE_PER_KM_1 = Variables.MMI_M_PULSE_PER_KM.TechnicalRangeCheckFailed;

            EVC40_MMICurrentMaintenanceData.Send();

            #endregion

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘Enter’ button of the data input field displays the previously entered value." + Environment.NewLine +
                                @"2. The echo text displayes ‘++++’.");

            /*
            Test Step 6
            Action: End of test
            Expected Result: 
            */
            
            return GlobalTestResult;
        }
    }
}
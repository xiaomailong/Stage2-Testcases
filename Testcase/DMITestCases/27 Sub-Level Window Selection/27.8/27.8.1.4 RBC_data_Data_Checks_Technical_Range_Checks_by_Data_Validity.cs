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
    /// 27.8.1.4 ‘RBC data’ Data Checks: Technical Range Checks by Data Validity
    /// TC-ID: 22.8.1.4
    /// 
    /// This test case verifies the functionalities of the ‘RBC data’ data entry when the RBC data does not comply with ETCS Onboard failed check. The function designs comply with the conditions in [MMI-ETCS-gen]. The data range and interface comply with the data information in [VSIS_gen].
    /// 
    /// Tested Requirements:
    /// MMI_gen 9467;
    /// 
    /// Scenario:
    /// 1.Activate the cabin.
    /// 2.Perform SoM until level 2 is selected. The ‘RBC data’ data entry window appears.
    /// 3.Enter and accept valid RBC data to enable the ‘Yes’ button.
    /// 4.Enter and accept invalid RBC data to verify the ‘Yes’ button.
    /// 
    /// Used files:
    /// 22_8_1_4.utt, 22_8_1_4_a.xml
    /// </summary>
    public class TC_ID_22_8_1_4_RBC_Data_window : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:

            // Call the TestCaseBase PreExecution
            base.PreExecution();

            // 1. The test environment is powered on.2. The cabin is activated.3. The ‘Start of Mission’ procedure is performed until level 2 is selected.4. RBC Data window is opened.
            DmiActions.Complete_SoM_L1_SB(this);
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Level = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_LEVEL.L2;
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = 1;
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.EnterRBCData;
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // 1. ETCS-DMI is in the ‘Start of Mission’ procedure.2. ETCS-DMI is in the ‘Stand-By’ mode.

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint

            /*
            Test Step 1
            Action: Enter valid values with the numeric keypad and press the data input field (Accept) in the same screen.RBC ID6996969RBC phone number0031840880100
            Expected Result: The ‘Yes’ button is enabled
            */
            DmiActions.ShowInstruction(this, @"Press the ‘RBC Data’ button.");

            EVC22_MMICurrentRBC.MMI_Q_CLOSE_ENABLE = Variables.MMI_Q_CLOSE_ENABLE.Enabled;
            EVC22_MMICurrentRBC.MMI_NID_WINDOW = 10;
            EVC22_MMICurrentRBC.Send();

            DmiActions.ShowInstruction(this, "On the numeric keypad: enter the value ‘6996969’ for ‘RBC ID’ and confirm the entered data by pressing the data input field;" + Environment.NewLine +
                                             "                       enter the value ‘0031840880100’ for ‘RBC phone number’ and confirm the entered data by pressing the data input field");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Yes’ button is displayed enabled");

            /*
            Test Step 2
            Action: Enter “1” (invalid value) for RBC ID with the numeric keypad and press the data input field (Accept) in the same screen
            Expected Result: EVC-22(1) Use the log file to verify that DMI receives packet EVC-22 with variable MMI_M_BUTTONS = 255 (no button) and the 'Yes' button is disabled
            Test Step Comment: Requirements:(1) MMI_gen 9467;
            */
            DmiActions.ShowInstruction(this, "On the numeric keypad, enter the (invalid) value ‘1’ for ‘RBC ID’ and confirm the entered data by pressing the data input field");

            EVC22_MMICurrentRBC.MMI_NID_WINDOW = 10;
            EVC22_MMICurrentRBC.MMI_M_BUTTONS = EVC22_MMICurrentRBC.EVC22BUTTONS.NoButton;
            EVC22_MMICurrentRBC.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Yes’ button is displayed disabled");

            /*
            Test Step 3
            Action: Enter “6996969” (valid value) for RBC ID with the numeric keypad and press the data input field (Accept) in the same screen
            Expected Result: The ‘Yes’ button is enabled
            */
            DmiActions.ShowInstruction(this, "Using the numeric keypad, enter the (valid) value ‘6996969’ for ‘RBC ID’ and confirm the entered data by pressing the data input field");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Yes’ button is displayed enabled");

            /*
            Test Step 4
            Action: Enter “1” (invalid value) for RBC phone number with the numeric keypad and press the data input field (Accept) in the same screen
            Expected Result: EVC-22(1) Use the log file to verify that DMI receives packet EVC-22 with variable MMI_M_BUTTONS = 255 (no button) and the 'Yes' button is disabled
            Test Step Comment: Requirements:(1) MMI_gen 9467;
            */
            DmiActions.ShowInstruction(this, "Using the numeric keypad, enter the (invalid) value ‘1’ for ‘RBC phone number’ and confirm the entered data by pressing the data input field");

            EVC22_MMICurrentRBC.MMI_NID_WINDOW = 10;
            EVC22_MMICurrentRBC.MMI_M_BUTTONS = EVC22_MMICurrentRBC.EVC22BUTTONS.NoButton;
            EVC22_MMICurrentRBC.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Yes’ button is displayed disabled");

            /*
            Test Step 5
            Action: Enter “0031840880100” (valid value) for RBC phone number with the numeric keypad and press the data input field (Accept) in the same screen
            Expected Result: The ‘Yes’ button is enabled
            */
            DmiActions.ShowInstruction(this, "Using the numeric keypad, enter the (valid) value ‘0031840880100’ for ‘RBC phone number’ and confirm the entered data by pressing the data input field");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Yes’ button is displayed enabled");

            /*
            Test Step 6
            Action: This step is to complete the process of ‘RBC data’:- Press the ‘Yes’ button on the ‘RBC data’ window
            Expected Result: The data entry window disappears
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Yes’ button in the RBC Data window");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The data entry window is removed");

            /*
            Test Step 7
            Action: Follow test step 2 to enable the ‘Yes’ button
            Expected Result: The ‘Yes’ button is enabled
            Test Step Comment: Note: This is a temporary approach for non-support test environment on the data checks.
            */
            DmiActions.ShowInstruction(this, @"Press the ‘RBC Data’ button.");

            EVC22_MMICurrentRBC.MMI_Q_CLOSE_ENABLE = Variables.MMI_Q_CLOSE_ENABLE.Enabled;
            EVC22_MMICurrentRBC.MMI_NID_WINDOW = 10;
            EVC22_MMICurrentRBC.Send();

            DmiActions.ShowInstruction(this, "On the numeric keypad: enter the value ‘6996969’ for ‘RBC ID’ and confirm the entered data by pressing the data input field;" + Environment.NewLine +
                                             "                       enter the value ‘0031840880100’ for ‘RBC phone number’ and confirm the entered data by pressing the data input field");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Yes’ button is displayed enabled");

            /*
            Test Step 8
            Action: Send the data of ‘Technical Range Check’ failure to ETCS-DMI by 22_8_1_4_a.xmlEVC-22MMI_M_BUTTONS = 255 (No button)
            Expected Result: EVC-22(1) The 'Yes' button is disabled
            Test Step Comment: Requirements:MMI_gen 9467;Note: This is a temporary approach for non-support test environment on the data checks.
            */
            #region Send_XML_22_8_1_4_DMI_Test_Specification
            EVC22_MMICurrentRBC.MMI_NID_WINDOW = 10;
            EVC22_MMICurrentRBC.NID_RBC = 1234;
            EVC22_MMICurrentRBC.MMI_NID_RADIO = 0xffffffffffffffff;
            EVC22_MMICurrentRBC.MMI_Q_CLOSE_ENABLE = Variables.MMI_Q_CLOSE_ENABLE.Enabled;
            EVC22_MMICurrentRBC.MMI_M_BUTTONS = EVC22_MMICurrentRBC.EVC22BUTTONS.NoButton;

            EVC22_MMICurrentRBC.Send();
            #endregion

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Yes’ button is displayed disabled");

            /*
            Test Step 9
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
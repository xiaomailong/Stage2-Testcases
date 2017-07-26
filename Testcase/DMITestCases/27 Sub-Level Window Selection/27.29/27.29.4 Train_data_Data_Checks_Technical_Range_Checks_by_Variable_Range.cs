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
    public class Train_data_Data_Checks_Technical_Range_Checks_by_Variable_Range : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // 1. The test environment is powered on.2. ATP-CU is verified that the train is set as ‘Flexible’.TR_OBU_TrainType = 23. The cabin is activated.4. The ‘Start of Mission’ procedure is performed until the ‘Staff Resonsible’ mode, level 1, is confirmed.5. The ‘Main’ window is opened.

            // Call the TestCaseBase PreExecution
            base.PreExecution();
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
            // Testcase entrypoint


            /*
            Test Step 1
            Action: Open the ‘Train data’ data entry window from the Main menu.
            Expected Result: The ‘Train data’ data entry window appears on ETCS-DMI screen instead of the ‘Main’ menu window.
            */

            /*
            Test Step 2
            Action: Enter “0” (minimum inbound) for Train Length with the numeric keypad and press the data input field (Accept) in the same screen.
            Expected Result: Input Field(1) The eventually displayed data value in the data area of the input field is replaced by “0” (character or value corresponding to the activated data key - state ‘Selected IF/value of pressed key(s)’).EVC-107(2) Use the log file to verify that DMI sends packet EVC-107 with variable:MMI_L_TRAIN = 0 MMI_NID_DATA = 8 (Length)
            Test Step Comment: Requirements:(1) MMI_gen 8089 (partly: reactions to succeed, MMI_gen 4714 (partly: MMI_gen 4679), MMI_gen 9286 (partly: state switched), MMI_gen 12145 (partly: minimum inbound)), MMI_gen 9413 (partly: state switched);(2) MMI_gen 8089 (partly: reactions to succeed, MMI_gen 12147, MMI_gen 9286 (partly: enabled)), MMI_gen 9413 (partly: enabled);
            */

            /*
            Test Step 3
            Action: Enter “4096” (outbound) for Train Length with the numeric keypad and press the data input field (Accept) in the same screen.
            Expected Result: Input Field(1) The ‘Enter’ button associated to the data area of the input field is coloured grey and its text is black (state ‘Selected IF/Data value’).(2) The ‘Enter’ button associated to the data area of the input field displays “4096” (previously entered value).EVC-107(3) Use the log file to verify that DMI does not send out packet EVC-107 as the ‘Enter’ button is disabled. Echo Texts(4) The data part of the echo text displays “++++”.(5) The data part of the echo text is coloured red.
            Test Step Comment: Requirements:(1) MMI_gen 8089 (partly: reactions to failing, MMI_gen 4714 (partly: state 'Selected IF/data value'));(2) MMI_gen 8089 (partly: reactions to failing, MMI_gen 4714 (partly: previously entered (faulty) value), MMI_gen 12145 (partly: outbound)); MMI_gen 4699 (technical range);(3) MMI_gen 8089 (partly: MMI_gen 9286 (partly: button ‘Enter’, disabled), MMI_gen 12148 (partly: not send packets), MMI_gen 12147), MMI_gen 9413 (partly: disabled);(4) MMI_gen 8089 (partly: reactions to failing, MMI_gen 12148 (MMI_gen 4713 (partly: indication)));(5) MMI_gen 9404 (partly: MMI_gen 12148 (MMI_gen 4713 (partly: red))), MMI_gen 8089 (partly: reactions to failing, MMI_gen 12148 (MMI_gen 4713 (partly: red)));
            */

            /*
            Test Step 4
            Action: Enter “4095” (maximum inbound) for Train Length with the numeric keypad and press the data input field (Accept) in the same screen.
            Expected Result: Input Field(1) The eventually displayed data value in the data area of the input field is replaced by “4095” (character or value corresponding to the activated data key - state ‘Selected IF/value of pressed key(s)’).EVC-107(2) Use the log file to verify that DMI sends packet EVC-107 with variable:MMI_L_TRAIN = 4095 MMI_NID_DATA = 8 (Length)
            Test Step Comment: Requirements:(1) MMI_gen 8089 (partly: MMI_gen 4714 (partly: MMI_gen 4679), MMI_gen 9286 (partly: state switched), MMI_gen 12145 (partly: maximum inbound)), MMI_gen 9413 (partly: state switched); (2) MMI_gen 8089 (partly: reactions to succeed, MMI_gen 12147, MMI_gen 9286 (partly: enabled)), MMI_gen 9413 (partly: enabled);
            */

            /*
            Test Step 5
            Action: Follow step 2 – step 4 for Brake Percentage with:Minimum inbound = 10Outbound = 251Maximum inbound = 250
            Expected Result: See step 2 – step 4EVC-107(1) Use the log file to confirm that DMI sends packet EVC-107 with variable:MMI_M_BRAKE_PERC = See Action MMI_NID_DATA = 9 (Brake Percentage)
            Test Step Comment: See step 2 – step 4
            */

            /*
            Test Step 6
            Action: Follow step 2 – step 4 for Max speed with:Minimum inbound = 0Outbound = 601Maximum inbound = 600
            Expected Result: See step 2 – step 4EVC-107(1) Use the log file to confirm that DMI sends packet EVC-107 with variable:MMI_V_MAXTRAIN = See Action MMI_NID_DATA = 10 (Maximum speed)
            Test Step Comment: See step 2 – step 4
            */

            /*
            Test Step 7
            Action: This step is to complete the process of ‘Train data’:- Press the ‘Yes’ button on the ‘Train data’ window.- Validate the data in the data validation window.
            Expected Result: 1. After pressing the ‘Yes’ button, the data validation window (‘Validate Train data’) appears instead of the ‘Train data’ data entry window. The data part of echo text displays “600” in white.2. After the data area of the input field containing “Yes” is pressed, the data validation window disappears and returns to the parent window (‘Settings’ window) of ‘Train data’ window with enabled ‘Train data’ button.
            */

            /*
            Test Step 8
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
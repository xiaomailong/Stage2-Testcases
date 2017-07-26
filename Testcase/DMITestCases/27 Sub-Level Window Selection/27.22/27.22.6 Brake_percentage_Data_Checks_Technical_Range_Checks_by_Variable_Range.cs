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
    public class Brake_percentage_Data_Checks_Technical_Range_Checks_by_Variable_Range : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // 1. The ‘ATC-2’ level is configured in ATP-CU.NID_NTC_Installed = 22, PB_SAFETY_LEVEL = 2, NTC_HW_ADDR = 92, NID_NTC_Default = 22 (M_InstalledLevels and M_DefaultLevels have to be updated according to the number of enabling NTC/STM levels, by bitmasks)2. The test environment is powered on.3. The cabin is activated.4. The ‘Start of Mission’ procedure is performed until the ‘Staff Responsible’ mode, level 1, is confirmed.5. The ‘Brake’ window is opened.

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
            Action: Open the ‘Brake percentage’ data entry window from the Special menu.
            Expected Result: The ‘Brake percentage’ data entry window appears on ETCS-DMI screen instead of the ‘Special’ menu window.
            */

            /*
            Test Step 2
            Action: Enter “10” (minimum inbound) with the numeric keypad and press the data input field (Accept) in the same screen.
            Expected Result: Input Field(1) The eventually displayed data value in the data area of the input field is replaced by “10” (character or value corresponding to the activated data key - state ‘Selected IF/value of pressed key(s)’).EVC-150(2) Use the log file to verify that DMI sends packet EVC-150 with variable:-   MMI_M_BP_CURRENT = 10 
            Test Step Comment: Requirements:(1) MMI_gen 11823 (partly: MMI_gen 4714 (partly: MMI_gen 4679), MMI_gen 9286 (partly: state switched), MMI_gen 12145 (partly: minimum inbound));(2) MMI_gen 11823 (partly: MMI_gen 12147, MMI_gen 9286 (partly: enabled));
            */

            /*
            Test Step 3
            Action: Enter “251” (outbound) with the numeric keypad and press the data input field (Accept) in the same screen.
            Expected Result: Input Field(1) The ‘Enter’ button associated to the data area of the input field is coloured grey and its text is black (state ‘Selected IF/Data value’).(2) The ‘Enter’ button associated to the data area of the input field displays “251” (previously entered value).EVC-150(3) Use the log file to verify that DMI does not send out packet EVC-150 as the ‘Enter’ button is disabled. Echo Texts(4) The data part of the echo text displays “++++”.(5) The data part of the echo text is coloured red.
            Test Step Comment: Requirements:(1) MMI_gen 11823 (partly: MMI_gen 4714 (partly: state 'Selected IF/data value'));(2) MMI_gen 11823 (partly: MMI_gen 4714 (partly: previously entered (faulty) value), MMI_gen 12145 (partly: outbound)); MMI_gen 4699 (technical range);(3) MMI_gen 11823 (partly: MMI_gen 9286 (partly: button ‘Enter’, disabled), MMI_gen 12148 (partly: not send packets), MMI_gen 12147);(4) MMI_gen 11823 (partly: MMI_gen 12148 (MMI_gen 4713 (partly: indication)));(5) MMI_gen 11823 (partly: MMI_gen 12148 (MMI_gen 4713 (partly: red)));
            */

            /*
            Test Step 4
            Action: Enter “250” (maximum inbound) with the numeric keypad and press the data input field (Accept) in the same screen.
            Expected Result: Input Field(1) The eventually displayed data value in the data area of the input field is replaced by “250” (character or value corresponding to the activated data key - state ‘Selected IF/value of pressed key(s)’).EVC-150(2) Use the log file to verify that DMI sends packet EVC-150 with variable:-   MMI_M_BP_CURRENT = 250
            Test Step Comment: Requirements:(1) MMI_gen 11823 (partly: MMI_gen 4714 (partly: MMI_gen 4679), MMI_gen 9286 (partly: state switched), MMI_gen 12145 (partly: maximum inbound));(2) MMI_gen 11823 (partly: MMI_gen 12147, MMI_gen 9286 (partly: enabled));
            */

            /*
            Test Step 5
            Action: This step is to complete the process of ‘Brake percentage’:- Press the ‘Yes’ button on the ‘Brake percentage’ window.- Validate the data in the data validation window.
            Expected Result: 1. After pressing the ‘Yes’ button, the data validation window (‘Validate Brake percentage’) appears instead of the ‘Brake percentage’ data entry window. The data part of echo text displays “250” in white.2. After the data area of the input field containing “Yes” is pressed, the data validation window disappears and returns to the parent window (‘Settings’ window) of ‘Brake percentage’ window with enabled ‘Brake percentage’ button.
            */

            /*
            Test Step 6
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
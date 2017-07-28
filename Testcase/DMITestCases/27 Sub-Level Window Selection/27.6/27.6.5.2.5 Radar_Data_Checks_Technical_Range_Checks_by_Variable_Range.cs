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
    /// 27.6.5.2.5 ‘Radar’ Data Checks: Technical Range Checks by Variable Range
    /// TC-ID: 22.6.5.2.5
    /// 
    /// This test case verifies the functionalities of ‘Radar’ data entry when the data of Radar does not comply with variable-range rules of the technical range check. The function designs comply with the conditions in [MMI-ETCS-gen]. The data range and interface comply with the data information in [VSIS_gen].
    /// 
    /// Tested Requirements:
    /// MMI_gen 11785 (partly: MMI_gen 4713, MMI_gen 12145, MMI_gen 12147, MMI_gen 12148, MMI_gen 4714, MMI_gen 9286 (partly: the ‘Enter’ button, switched state, disabled, enabled)); MMI_gen 4699 (technical range);
    /// 
    /// Scenario:
    /// 1.Activate the cabin.
    /// 2.Press the ‘Settings’ button in the ‘Driver ID’ window. Then, the ‘Settings’ window is opened.
    /// 3.Press the ‘Maintenance’ button. Then, the ‘Password’ window is opened.
    /// 4.Enter the password of “26728290”. Then, the ‘Maintenance’ window is opened.
    /// 5.Press the ‘Radar’ button. Then, the ‘Radar’ window is opened.
    /// 6.Enter the data of Radar. Then, the ‘Radar’ window is verified by the following events:a.   The minimum DMI-technical-inbound data of Radar is entered and accept.b.   The DMI-technical-outbound data of Radar is entered and accepted.c.   The maximum DMI-technical-inbound data of Radar is entered and accepted.
    /// 
    /// Used files:
    /// N/A
    /// </summary>
    public class Radar_Data_Checks_Technical_Range_Checks_by_Variable_Range : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // 1. The test environment is powered on.2. The cabin is activated.3. The ‘Settings’ window is opened.

            // Call the TestCaseBase PreExecution
            base.PreExecution();
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
            Action: Open the ‘Radar’ data entry window from the Settings menu
            Expected Result: The ‘Radar’ data entry window appears on ETCS-DMI screen instead of the ‘Settings’ menu window
            */
            // Call generic Action Method
            DmiActions.Open_the_Radar_data_entry_window_from_the_Settings_menu();
            // Call generic Check Results Method
            DmiExpectedResults
                .The_Radar_data_entry_window_appears_on_ETCS_DMI_screen_instead_of_the_Settings_menu_window();


            /*
            Test Step 2
            Action: Enter “20001” (minimum inbound) with the numeric keypad and press the data input field (Accept) in the same screen
            Expected Result: Input Field(1) The eventually displayed data value in the data area of the input field is replaced by “20001” (character or value corresponding to the activated data key - state ‘Selected IF/value of pressed key(s)’).EVC-140(2) Use the log file to verify that DMI sends packet EVC-140 with variable:MMI_M_PULSE_PER_KM_1 = 20001 MMI_Q_MD_DATASET = 1 (Radar)
            Test Step Comment: Requirements:(1) MMI_gen 11785 (partly: MMI_gen 4714 (partly: MMI_gen 4679), MMI_gen 9286 (partly: state switched), MMI_gen 12145 (partly: minimum inbound));(2) MMI_gen 11785 (partly: MMI_gen 12147, MMI_gen 9286 (partly: enabled));
            */


            /*
            Test Step 3
            Action: Enter “20000” (outbound) with the numeric keypad and press the data input field (Accept) in the same screen
            Expected Result: Input Field(1) The ‘Enter’ button associated to the data area of the input field is coloured grey and its text is black (state ‘Selected IF/Data value’).(2) The ‘Enter’ button associated to the data area of the input field displays “20000” (previously entered value).EVC-140(3) Use the log file to verify that DMI does not send out packet EVC-140 as the ‘Enter’ button is disabled. Echo Texts(4) The data part of the echo text displays “++++”.(5) The data part of the echo text is coloured red
            Test Step Comment: Requirements:(1) MMI_gen 11785 (partly: MMI_gen 4714 (partly: state 'Selected IF/data value'));(2) MMI_gen 11785 (partly: MMI_gen 4714 (partly: previously entered (faulty) value), MMI_gen 12145 (partly: outbound)); MMI_gen 4699 (technical range);(3) MMI_gen 11785 (partly: MMI_gen 9286 (partly: button ‘Enter’, disabled), MMI_gen 12148 (partly: not send packets), MMI_gen 12147);(4) MMI_gen 11785 (partly: MMI_gen 12148 (MMI_gen 4713 (partly: indication)));(5) MMI_gen 11785 (partly: MMI_gen 12148 (MMI_gen 4713 (partly: red)));
            */


            /*
            Test Step 4
            Action: Enter “85534” (maximum inbound) with the numeric keypad and press the data input field (Accept) in the same screen
            Expected Result: Input Field(1) The eventually displayed data value in the data area of the input field is replaced by “85534” (character or value corresponding to the activated data key - state ‘Selected IF/value of pressed key(s)’).EVC-140(2) Use the log file to verify that DMI sends packet EVC-140 with variable:MMI_M_PULSE_PER_KM_1 = 85534 MMI_Q_MD_DATASET = 1 (Radar)
            Test Step Comment: Requirements:(1) MMI_gen 11785 (partly: MMI_gen 4714 (partly: MMI_gen 4679), MMI_gen 9286 (partly: state switched), MMI_gen 12145 (partly: maximum inbound));(2) MMI_gen 11785 (partly: MMI_gen 12147, MMI_gen 9286 (partly: enabled));
            */


            /*
            Test Step 5
            Action: This step is to complete the process of ‘Radar’:- Press the ‘Yes’ button on the ‘Radar’ window.- Validate the data in the data validation window
            Expected Result: 1. After pressing the ‘Yes’ button, the data validation window (‘Validate Radar’) appears instead of the ‘Radar’ data entry window. The data part of echo text displays “65536” in white.2. After the data area of the input field containing “Yes” is pressed, the data validation window disappears and returns to the parent window (‘Settings’ window) of ‘Radar’ window with enabled ‘Radar’ button
            */
            // Call generic Action Method
            DmiActions
                .This_step_is_to_complete_the_process_of_Radar_Press_the_Yes_button_on_the_Radar_window_Validate_the_data_in_the_data_validation_window();


            /*
            Test Step 6
            Action: End of test
            Expected Result: 
            */


            return GlobalTestResult;
        }
    }
}
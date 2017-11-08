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
using Testcase.Telegrams.DMItoEVC;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 27.28.9 ‘Remove VBC’ Data Checks: Technical Range Checks by Variable Range
    /// TC-ID: 22.28.9
    /// 
    /// This test case verifies the functionalities of ‘Remove VBC’ data entry when the VBC code does not comply with variable-range rules of the technical range check. The function designs comply with the conditions in [MMI-ETCS-gen]. The data range and interface comply with the data information in [VSIS_gen].
    /// 
    /// Tested Requirements:
    /// 1. Remove VBC Window: MMI_gen 9920; MMI_gen 9924 (partly: the ‘Enter’ button, accepted data complied with data checks, driver action);2. Data Checks: MMI_gen 8339 (partly: MMI_gen 12148 (partly: MMI_gen 4713 (partly: indication))), MMI_gen 9912 (partly: reactions to failing and succeed, EVC-19, MMI_gen 4713, MMI_gen 12145, MMI_gen 12147, MMI_gen 12148, MMI_gen 4714, MMI_gen 9286 (partly: the ‘Enter’ button, switched state, disabled, enabled)), MMI_gen 9913 (partly: MMI_gen 12148 (partly: MMI_gen 4713 (partly:red))); MMI_gen 4699 (technical range);
    /// 
    /// Scenario:
    /// Activate the cabin.Press the ‘Settings’ button. Then, the ‘Settings’ window is opened from the ‘Driver ID’ windowAdd VBC code “16777215” in the ‘Set VBC’ data entry window.When the ‘Set VBC’ procedure is completed, the ‘Settings’ window appears.Press the ‘Remove VBC’ button. Then, the ‘Remove VBC’ window is opened.Enter a VBC code. Then, the ‘Remove VBC’ window is verified by the following events:The minimum DMI-technical-inbound VBC code is entered and accepted.The DMI-technical-outbound VBC code is entered and accepted.The maximum DMI-technical-inbound VBC code is entered and accepted.
    /// 
    /// Used files:
    /// N/A
    /// </summary>
    public class TC_ID_22_28_9_Remove_VBC_Data_Checks_Technical_Range_Checks_by_Variable_Range : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // 1. The test environment is powered on.2. The cabin is activated.
            // 3. The ‘Settings’ window is opened from the ‘Driver ID’ window.4. VBC code “16777215” is stored onboard.

            // Call the TestCaseBase PreExecution
            base.PreExecution();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec

             // Call the TestCaseBase PostExecution
            base.PostExecution();

            // 1. ETCS-DMI is in the ‘Start of Mission’ procedure2. ETCS-DMI is in the ‘Stand-By’ mode.3. VBC code “16777215” is not stored onboard.
            DmiActions.Start_ATP();
            DmiActions.Activate_Cabin_1(this);
            DmiActions.Set_Driver_ID(this, "1234");
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint

            /*
            Test Step 1
            Action: Open the ‘Remove VBC’ data entry window from the Settings menu
            Expected Result: The ‘Remove VBC’ data entry window appears on ETCS-DMI screen instead of the ‘Settings’ menu window
            */
            DmiActions.ShowInstruction(this, "Press the ‘Settings’ button");

            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Settings;
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.RemoveVBC;
            EVC30_MMIRequestEnable.Send();

            DmiActions.ShowInstruction(this, "Press the ‘Remove VBC’ button");

            EVC19_MMIRemoveVBC.MMI_M_BUTTONS = Variables.MMI_M_BUTTONS_VBC.BTN_ENTER;
            EVC19_MMIRemoveVBC.MMI_N_VBC = 0;
            EVC19_MMIRemoveVBC.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Remove VBC window, with the title ‘Remove VBC’, instead of the Settings window." + Environment.NewLine +
                                "2. One data input field (and a corresponding echo text) labelled ‘VBC code’ are displayed." + Environment.NewLine +
                                "3. A dedicated numeric keypad is displayed below the data input field with an enabled ‘Close’ button below it." + Environment.NewLine +
                                "4. A ‘Set VBC entry complete?’ label is displayed in the bottom left-hand corner with an enabled ‘Yes’ button below it.");
            
            /*
            Test Step 2
            Action: Enter “0” (minimum inbound) with the numeric keypad and press the data input field (Accept) in the same screen
            Expected Result: Input Field(1) The eventually displayed data value in the data area of the input field is replaced by “0” (character or value corresponding to the activated data key - state ‘Selected IF/value of pressed key(s)’).EVC-119(2) Use the log file to verify that DMI sends packet EVC-119 with variable:MMI_M_VBC_CODE = 0 MMI_M_BUTTONS =  254 (BTN_ENTER)EVC-19 (3) Use the log file to verify that DMI receives packet EVC-19 with variable:MMI_Q_DATA_CHECK = 0 (All checks have passed)MMI_X_TEXT = 48 (“0”)
            Test Step Comment: Requirements:(1) MMI_gen 9912 (partly: reactions to succeed, MMI_gen 4714 (partly: MMI_gen 4679), MMI_gen 9286 (partly: state switched), MMI_gen 12145 (partly: minimum inbound)), MMI_gen 9920 (partly: state switched);(2) MMI_gen 9912 (partly: reactions to succeed, MMI_gen 12147, MMI_gen 9286 (partly: enabled)), MMI_gen 9920 (partly: enabled), MMI_gen 9924 (partly: EVC-119, the ‘Enter’ button, accepted data complied with data checks, driver action);(3) MMI_gen 9912 (partly: reactions to succeed, EVC-19)
            */
            DmiActions.ShowInstruction(this, 
                                       @"Enter “0” (minimum inbound) with the numeric keypad and press the data input field (Accept) in the same screen");

            EVC19_MMIRemoveVBC.MMI_Q_DATA_CHECK = Variables.Q_DATA_CHECK.All_checks_passed;
            EVC19_MMIRemoveVBC.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The data input field displays ‘0’");

            /*
            Test Step 3
            Action: Enter “16777216” (outbound) with the numeric keypad and press the data input field (Accept) in the same screen
            Expected Result: Input Field(1) The ‘Enter’ button associated to the data area of the input field is coloured grey and its text is black (state ‘Selected IF/Data value’).(2) The ‘Enter’ button associated to the data area of the input field displays “16777216” (previously entered value).EVC-119(3) Use the log file to verify that DMI does not send out packet EVC-119 as the ‘Enter’ button is disabled. Echo Texts(4) The data part of the echo text displays “++++”.(5) The data part of the echo text is coloured red
            Test Step Comment: Requirements:(1) MMI_gen 9912 (partly: reactions to failing, MMI_gen 4714 (partly: state 'Selected IF/data value'));(2) MMI_gen 9912 (partly: reactions to failing, MMI_gen 4714 (partly: previously entered (faulty) value), MMI_gen 12145 (partly: outbound)); MMI_gen 4699 (technical range);(3) MMI_gen 9912 (partly: MMI_gen 9286 (partly: button ‘Enter’, disabled), MMI_gen 12148 (partly: not send packets), MMI_gen 12147), MMI_gen 9920 (partly: disabled), MMI_gen 9924 (partly: EVC-119); (4) MMI_gen 8339 (partly: MMI_gen 12148 (MMI_gen 4713 (partly: indication))), MMI_gen 9912 (partly: reactions to failing, MMI_gen 12148 (MMI_gen 4713 (partly: indication)));(5) MMI_gen 9913 (partly: MMI_gen 12148 (MMI_gen 4713 (partly: red))), MMI_gen 9912 (partly: reactions to failing, MMI_gen 12148 (MMI_gen 4713 (partly: red)));
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Enter ‘16777216’ (outbound) with the numeric keypad and press the data input field (Accept) in the same screen");

            EVC119_MMINewRemoveVbc.MMI_M_VBC_CODE = 0;
            EVC119_MMINewRemoveVbc.MMI_M_BUTTONS = Variables.MMI_M_BUTTONS_VBC.BTN_ENTER;
            EVC119_MMINewRemoveVbc.CheckPacketContent();
            
            EVC19_MMIRemoveVBC.MMI_Q_DATA_CHECK = Variables.Q_DATA_CHECK.All_checks_passed;
            EVC19_MMIRemoveVBC.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The data input field ‘Enter’ button displays ‘16777216’ in black on a grey background." + Environment.NewLine +
                                "2. The echo text data part displays ‘++++’ in red.");

            /*
            Test Step 4
            Action: Enter “16777215” (maximum inbound) with the numeric keypad and press the data input field (Accept) in the same screen
            Expected Result: Input Field(1) The eventually displayed data value in the data area of the input field is replaced by “16777215” (character or value corresponding to the activated data key - state ‘Selected IF/value of pressed key(s)’).EVC-119(2) Use the log file to verify that DMI sends packet EVC-119 with variable:MMI_M_VBC_CODE = 16777215MMI_M_BUTTONS =  254 (BTN_ENTER)EVC-19(3) Use the log file to verify that DMI receives packet EVC-19 with variable:MMI_Q_DATA_CHECK = 0MMI_X_TEXT = 49 (“1”)MMI_X_TEXT = 54 (“6”)MMI_X_TEXT = 55 (“7”)MMI_X_TEXT = 55 (“7”)MMI_X_TEXT = 55 (“7”)MMI_X_TEXT = 50 (“2”)MMI_X_TEXT = 49 (“1”)MMI_X_TEXT = 53 (“5”)
            Test Step Comment: Requirements:(1) MMI_gen 9912 (partly: MMI_gen 4714 (partly: MMI_gen 4679), MMI_gen 9286 (partly: state switched), MMI_gen 12145 (partly: maximum inbound)), MMI_gen 9920 (partly: state switched); (2) MMI_gen 9912 (partly: reactions to succeed, MMI_gen 12147, MMI_gen 9286 (partly: enabled)), MMI_gen 9920 (partly: enabled), MMI_gen 9924 (partly: EVC-119, the ‘Enter’ button, accepted data complied with data checks, driver action);(3) MMI_gen 9912 (partly: reactions to succeed, EVC-19)
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this,
                @"Enter ‘16777215’ (maximum inbound) with the numeric keypad and press the data input field (Accept) in the same screen");

            EVC119_MMINewRemoveVbc.MMI_M_VBC_CODE = 16777215;
            EVC119_MMINewRemoveVbc.MMI_M_BUTTONS = Variables.MMI_M_BUTTONS_VBC.BTN_ENTER;
            EVC119_MMINewRemoveVbc.CheckPacketContent();

            EVC19_MMIRemoveVBC.ECHO_TEXT = "16777215";
            EVC19_MMIRemoveVBC.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The data input field ‘Enter’ button displays ‘16777215’ instead of ‘16777216’, in black on a grey background.");

            /*
            Test Step 5
            Action: This step is to complete the process of ‘Remove VBC’:- Press the ‘Yes’ button on the ‘Remove VBC’ window.- Validate the data in the data validation window
            Expected Result: 1. After pressing the ‘Yes’ button, the data validation window (‘Validate Remove VBC’) appears instead of the ‘Remove VBC’ data entry window. The data part of echo text displays “16777215” in white.2. After the data area of the input field containing “Yes” is pressed, the data validation window disappears and returns to the parent window (‘Settings’ window) of ‘Remove VBC’ window with enabled ‘Remove VBC’ button
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Yes’ button (maximum inbound) with the numeric keypad and press the data input field (Accept) in the same screen");

            EVC29_MMIEchoedRemoveVBCData.MMI_M_VBC_CODE_ = 16777215;
            EVC29_MMIEchoedRemoveVBCData.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Data validation window." + Environment.NewLine +
                                "2. The data part of the echo text displays ‘16777215’ in white.");

            DmiActions.ShowInstruction(this, "Press the data input field containing ‘Yes’ to confirm the data.");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Settings window with the ‘Remove VBC’ button enabled.");

            /*
            Test Step 6
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
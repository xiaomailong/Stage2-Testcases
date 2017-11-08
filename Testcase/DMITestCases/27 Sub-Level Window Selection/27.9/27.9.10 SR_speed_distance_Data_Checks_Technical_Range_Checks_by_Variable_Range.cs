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
using static Testcase.Telegrams.EVCtoDMI.Variables;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 27.9.10 ‘SR speed / distance’ Data Checks: Technical Range Checks by Variable Range
    /// TC-ID: 22.9.10
    /// 
    /// This test case verifies the functionalities of ‘SR speed / distance’ data entry when the the data of SR speed / distance does not comply with variable-range rules of the technical range check. The function designs comply with the conditions in [MMI-ETCS-gen]. The data range and interface comply with the data information in [VSIS_gen].
    /// 
    /// Tested Requirements:
    /// 1. SR speed / distance Window: MMI_gen 9509 (partly: the ‘Enter’ button, accepted data complied with data checks, driver action); MMI_gen 9510 (partly: the ‘Enter’ button, accepted data complied with data checks, driver action);2. Data Checks: MMI_gen 8297 (partly: reactions to failing and succeed, EVC-18, MMI_gen 4713, MMI_gen 12145, MMI_gen 12147, MMI_gen 12148, MMI_gen 4714, MMI_gen 9286 (partly: the ‘Enter’ button, switched state, disabled, enabled)); MMI_gen 4699 (technical range);
    /// 
    /// Scenario:
    /// 1.Activate the cabin.
    /// 2.Perform SoM until mode SR is confirmed in the default window.
    /// 3.Press the ‘Special’ button. Then, the ‘Special’ window is opened.
    /// 4.Press the ‘SR speed / distance’ button. Then, the ‘SR speed / distance’ window is opened.
    /// 5.Enter the data of SR speed / distance. Then, the ‘SR speed / distance’ window is verified by the following events:The minimum DMI-technical-inbound VBC code is entered and accepted.The DMI-technical-outbound VBC code is entered and accepted.The maximum DMI-technical-inbound VBC code is entered and accepted.
    /// 
    /// Used files:
    /// N/A
    /// </summary>
    public class TC_22_9_10_SR_Speed_Distance_window : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
       
            // Call the TestCaseBase PreExecution
            base.PreExecution();

            // 1. The test environment is powered on.2. The cabin is activated.3. The ‘Start of Mission’ procedure is performed until the ‘Staff Responsible’ mode, level 1, is confirmed.4. The ‘Special’ window is opened.
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
            // Testcase entrypoint
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Main;
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.SRSpeedDistance;
            EVC30_MMIRequestEnable.Send();

            /*
            Test Step 1
            Action: Open the ‘SR speed / distance’ data entry window from the Special menu
            Expected Result: The ‘SR speed / distance’ data entry window appears on ETCS-DMI screen instead of the ‘Special’ menu window
            */
            DmiActions.ShowInstruction(this, "Press the ‘Spec’ button, then press the ‘SR speed/distance’ button");
            
            EVC11_MMICurrentSRRules.MMI_M_BUTTONS = MMI_M_BUTTONS.BTN_YES_DATA_ENTRY_COMPLETE;
            EVC11_MMICurrentSRRules.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the SR/speed distance window.");

            /*
            Test Step 2
            Action: Enter “0” (minimum inbound) for SR speed with the numeric keypad and press the data input field (Accept) in the same screen
            Expected Result: Input Field(1) The eventually displayed data value in the data area of the input field is replaced by “0” (character or value corresponding to the activated data key - state ‘Selected IF/value of pressed key(s)’).EVC-106(2) Use the log file to verify that DMI sends packet EVC-106 with variable:MMI_V_STFF = 0 MMI_M_BUTTONS =  254 (BTN_ENTER) MMI_NID_DATA = 15 (SR Speed)
            Test Step Comment: Requirements:(1) MMI_gen 8297 (partly: reactions to succeed, MMI_gen 4714 (partly: MMI_gen 4679), MMI_gen 9286 (partly: state switched), MMI_gen 12145 (partly: minimum inbound));(2) MMI_gen 8297 (partly: reactions to succeed, MMI_gen 12147, MMI_gen 9286 (partly: enabled)); MMI_gen 9509 (partly: EVC-106, the ‘Enter’ button, accepted data complied with data checks, driver action);
            */
            DmiActions.ShowInstruction(this, @"Enter the value ‘0’ in the SR speed data input field and press in the data input field to accept the valu.");

            EVC106_MMINewSrRules.MMI_V_STFF = 0;
            EVC106_MMINewSrRules.MMI_NID_DATA = new List<byte> { 15 };
            EVC106_MMINewSrRules.MMI_M_BUTTONS = Variables.MMI_M_BUTTONS_SR_RULES.BTN_ENTER;
            EVC106_MMINewSrRules.CheckPacketContent();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The SR speed data input field displays ‘0’.");

            /*
            Test Step 3
            Action: Enter “601” (outbound) for SR speed with the numeric keypad and press the data input field (Accept) in the same screen
            Expected Result: Input Field(1) The ‘Enter’ button associated to the data area of the input field is coloured grey and its text is black (state ‘Selected IF/Data value’).(2) The ‘Enter’ button associated to the data area of the input field displays “601” (previously entered value).EVC-106(3) Use the log file to verify that DMI does not send out packet EVC-106 as the ‘Enter’ button is disabled. Echo Texts(4) The data part of the echo text displays “++++”.(5) The data part of the echo text is coloured red
            Test Step Comment: Requirements:(1) MMI_gen 8297 (partly: reactions to failing, MMI_gen 4714 (partly: state 'Selected IF/data value'));(2) MMI_gen 8297 (partly: reactions to failing, MMI_gen 4714 (partly: previously entered (faulty) value), MMI_gen 12145 (partly: outbound)); MMI_gen 4699 (technical range);(3) MMI_gen 8297 (partly: MMI_gen 9286 (partly: button ‘Enter’, disabled), MMI_gen 12148 (partly: not send packets) , MMI_gen 12147); MMI_gen 9509 (partly: EVC-106); (4) MMI_gen 8297 (partly: reactions to failing, MMI_gen 12148 (MMI_gen 4713 (partly: indication)));(5) MMI_gen 8297 (partly: reactions to failing, MMI_gen 12148 (MMI_gen 4713 (partly: red)));
            */
            DmiActions.ShowInstruction(this, @"Enter the value ‘601’ in the SR speed data input field and press in the data input field to accept the valu.");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘Enter’ button of the SR speed data input field displays ‘601’ in black on a grey background");

            /*
            Test Step 4
            Action: Enter “600” (maximum inbound) for SR speed with the numeric keypad and press the data input field (Accept) in the same screen
            Expected Result: Input Field(1) The eventually displayed data value in the data area of the input field is replaced by “600” (character or value corresponding to the activated data key - state ‘Selected IF/value of pressed key(s)’).EVC-106(2) Use the log file to verify that DMI sends packet EVC-106 with variable:MMI_V_STFF = 600MMI_M_BUTTONS =  254 (BTN_ENTER)MMI_NID_DATA = 15 (SR Speed)
            Test Step Comment: Requirements:(1) MMI_gen 8297 (partly: MMI_gen 4714 (partly: MMI_gen 4679), MMI_gen 9286 (partly: state switched), MMI_gen 12145 (partly: maximum inbound)); (2) MMI_gen 8297 (partly: reactions to succeed, MMI_gen 12147, MMI_gen 9286 (partly: enabled)); MMI_gen 9509 (partly: EVC-106, the ‘Enter’ button, accepted data complied with data checks, driver action);
            */
            DmiActions.ShowInstruction(this, @"Enter the value ‘600’ in the SR speed data input field and press in the data input field to accept the valu.");

            EVC106_MMINewSrRules.MMI_V_STFF = 600;
            EVC106_MMINewSrRules.MMI_NID_DATA = new List<byte> { 15 };
            EVC106_MMINewSrRules.MMI_M_BUTTONS = Variables.MMI_M_BUTTONS_SR_RULES.BTN_ENTER;
            EVC106_MMINewSrRules.CheckPacketContent();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘Enter’ button of the SR speed data input field displays ‘601’ in black on a grey background" + Environment.NewLine +
                                @"2. The data part of the SR speed echo text displays ‘++++’ in red.");

            /*
            Test Step 5
            Action: Follow step 2 – step 8 for SR distance with:Minimum inbound = 0Outbound = 100001Maximum inbound = 100000
            Expected Result: See step 2 – step 4EVC-106(1) Use the log file to confirm that DMI sends packet EVC-106 with variable:MMI_L_STFF = See ActionMMI_M_BUTTONS =  254 (BTN_ENTER)MMI_NID_DATA = 16 (SR Distance)
            Test Step Comment: See step 2 – step 4Requirements:(1) MMI_gen 8297 (partly: MMI_gen 9286 (partly: enabled)); MMI_gen 9510 (partly: EVC-106, the ‘Enter’ button, accepted data complied with data checks, driver action, only affect the object indicated in MMI_NID_DATA);
            */
            // Repeat Step 2 for SR distance
            DmiActions.ShowInstruction(this, @"Enter the value ‘0’ in the SR speed data input field and press in the data input field to accept the valu.");

            EVC106_MMINewSrRules.MMI_V_STFF = 0;
            EVC106_MMINewSrRules.MMI_NID_DATA = new List<byte> { 16 };
            EVC106_MMINewSrRules.MMI_M_BUTTONS = Variables.MMI_M_BUTTONS_SR_RULES.BTN_ENTER;
            EVC106_MMINewSrRules.CheckPacketContent();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The SR distance data input field displays ‘0’.");

            // Repeat Step 3 for SR distance
            DmiActions.ShowInstruction(this, @"Enter the value ‘100001’ in the SR distance data input field and press in the data input field to accept the valu.");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘Enter’ button of the SR distance data input field displays ‘100001’ in black on a grey background");

            // Repeat Step 4 for SR distance
            DmiActions.ShowInstruction(this, @"Enter the value ‘100000’ in the SR distance data input field and press in the data input field to accept the valu.");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘Enter’ button of the SR distance data input field displays ‘601’ in black on a grey background" + Environment.NewLine +
                                @"2. The data part of the SR distance echo text displays ‘++++’ in red.");

            /*
            Test Step 6
            Action: This step is to complete the process of ‘SR speed / distance’:- Press the ‘Yes’ button on the ‘SR speed / distance’ window.- Validate the data in the data validation window
            Expected Result: 1. After pressing the ‘Yes’ button, the data validation window (‘Validate SR speed / distance’) appears instead of the ‘SR speed / distance’ data entry window. The data part of echo text displays in white:SR Speed: 600SR Distance: 1000002. After the data area of the input field containing “Yes” is pressed, the data validation window disappears and returns to the parent window (‘Settings’ window) of ‘SR speed / distance’ window with enabled ‘SR speed / distance’ button
            */
            DmiActions.ShowInstruction(this, @"Press ‘Yes’ button on the ‘SR speed / distance’ window");

            EVC11_MMICurrentSRRules.DataElements = new List<DataElement> { new DataElement { Identifier = 15, EchoText = "600", QDataCheck = 1 },
                                                                           new DataElement { Identifier = 16, EchoText = "100000", QDataCheck = 1} };
            EVC11_MMICurrentSRRules.Send();
            
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. DMI displays the ‘Validate SR speed/distance’ window." + Environment.NewLine +
                                @"2. The data part of the SR speed echo text displays ‘600’ in white." + Environment.NewLine +
                                @"3. The data part of the SR distance echo text displays ‘100000’ in white.");

            DmiActions.ShowInstruction(this, @"Validate the data in the data validation window");
            
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. DMI displays the SR speed/distance window.");

            /*
            Test Step 7
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
using System;
using System.Collections.Generic;
using Testcase.Telegrams.EVCtoDMI;
using Testcase.Telegrams.DMItoEVC;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 27.9.9 ‘SR speed / distance’ Data Checks: Technical Range Checks by Data Validity
    /// TC-ID: 22.9.9
    /// 
    /// This test case verifies the functionalities of the ‘SR speed / distance’ data entry when the data of SR speed / distance does not comply with data-validity rules of the technical range check. The function designs comply with the conditions in [MMI-ETCS-gen]. The data range and interface comply with the data information in [VSIS_gen].
    /// 
    /// Tested Requirements:
    /// 1. SR speed / distance Window: MMI_gen 9892; MMI_gen 9896; MMI_gen 9509 (partly: the ‘Enter’ button, accepted data complied with data checks, driver action); MMI_gen 9510 (partly: the ‘Enter’ button, accepted data complied with data checks, driver action);2. Data Checks: MMI_gen 8297 (partly: MMI_gen 12148, MMI_gen 4713, MMI_gen 4714, MMI_gen 4679, MMI_gen 9286 (partly: the ‘Enter’ button, disabled, enabled), MMI_gen 12147); MMI_gen 9889 (partly: MMI_gen 12148 (MMI_gen 4713 (partly: red)); MMI_gen 9310 (partly: technical range);
    /// 
    /// Scenario:
    /// 1.Activate the cabin.
    /// 2.Perform SoM until mode SR is confirmed in the default window.
    /// 3.Press the ‘Special’ button. Then, the ‘Special’ window is opened.
    /// 4.Press the ‘SR speed / distance’ button. Then, the ‘SR speed / distance’ window is opened.
    /// 5.Enter the data of SR speed / distance. Then, the ‘SR speed / distance’ window is verified by the following events:a.   Enter and accept the invalid data. b.   Accept the previuos invalid data without re-enter in order that DMI does not send out any packets (The ‘Enter’ button is disabled).c.   Repeat a. and b. in order that the invalid data is re-entered and accepted although the ‘Enter’ button is disabled.d.   Repeat a. by valid data in order that the data is entered and accepted after the ‘Enter’ button is disabled.Note: The appearance of highlighting in data area has remained in DMI since BL-2 requirement [MMI-ETCS-gen BL2].
    /// 
    /// Used files:
    /// 22_9_9_a.xml, 22_9_9_b.xml
    /// </summary>
    public class TC_22_9_9_SR_SpeedDistance_window : TestcaseBase
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
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 0;
            // Testcase entrypoint

            TraceHeader("Test Step 1");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Open the ‘SR speed / distance’ data entry window from the Special menu");
            TraceReport("Expected Result");
            TraceInfo(
                "The ‘SR speed / distance’ data entry window appears on ETCS-DMI screen instead of the ‘Special’ menu window");
            /*
            Test Step 1
            Action: Open the ‘SR speed / distance’ data entry window from the Special menu
            Expected Result: The ‘SR speed / distance’ data entry window appears on ETCS-DMI screen instead of the ‘Special’ menu window
            */
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Special;
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.SRSpeedDistance;
            EVC30_MMIRequestEnable.Send();

            DmiActions.ShowInstruction(this, "Press the ‘Spec’ button, then press the ‘SR speed/distance’ button");
            EVC11_MMICurrentSRRules.MMI_M_BUTTONS = Variables.MMI_M_BUTTONS.BTN_YES_DATA_ENTRY_COMPLETE;
            EVC11_MMICurrentSRRules.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the SR/speed distance window.");

            TraceHeader("Test Step 2");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Enter “1” (invalid value) for SR speed with the numeric keypad and press the data input field (Accept) in the same screen.Then, observe the echo texts on the left hand side");
            TraceReport("Expected Result");
            TraceInfo(
                "EVC-11Use the log file to verify that DMI receives packet EVC-11 with variable:(1) MMI_Q_DATA_CHECK = 1 in order to indicate the technical range check failure.(2) MMI_M_BUTTONS = 255 (no button) and the 'Yes' button is disabled.(3) MMI_NID_DATA = 15 (SR Speed)Input Field(4) The ‘Enter’ button associated to the data area of the input field is coloured grey and its text is black (state ‘Selected IF/Data value’).(5) The ‘Enter’ button associated to the data area of the input field displays “1” (previously entered value).Echo Texts of SR Speed(6) The data part of the echo text displays “++++”.(7) The data part of the echo text is coloured red");
            /*
            Test Step 2
            Action: Enter “1” (invalid value) for SR speed with the numeric keypad and press the data input field (Accept) in the same screen.Then, observe the echo texts on the left hand side
            Expected Result: EVC-11Use the log file to verify that DMI receives packet EVC-11 with variable:(1) MMI_Q_DATA_CHECK = 1 in order to indicate the technical range check failure.(2) MMI_M_BUTTONS = 255 (no button) and the 'Yes' button is disabled.(3) MMI_NID_DATA = 15 (SR Speed)Input Field(4) The ‘Enter’ button associated to the data area of the input field is coloured grey and its text is black (state ‘Selected IF/Data value’).(5) The ‘Enter’ button associated to the data area of the input field displays “1” (previously entered value).Echo Texts of SR Speed(6) The data part of the echo text displays “++++”.(7) The data part of the echo text is coloured red
            Test Step Comment: Requirements:(1) MMI_gen 8297 (partly: EVC-11, MMI_gen 12147);(2) MMI_gen 9892;(3) MMI_gen 9509 (partly: MMI_NID_DATA);(4) MMI_gen 8297 (partly: MMI_gen 4714 (partly: state 'Selected IF/data value')); MMI_gen 9310 (partly: accept data);(5) MMI_gen 8297 (partly: MMI_gen 4714 (partly: previously entered (faulty) value)); MMI_gen 4699 (technical range);(6) MMI_gen 8297 (partly: MMI_gen 4713 (partly: indication)); MMI_gen 9310 (partly: [technical range, No OK, echo text]);(7) MMI_gen 9889 (partly: MMI_gen 4713 (partly: red)), MMI_gen 8297 (partly: MMI_gen 4713 (partly: red));
            */
            DmiActions.ShowInstruction(this,
                @"Enter the (invalid) value ‘1’ in the SR speed data input field and press in the data input field to accept the valu.");

            EVC11_MMICurrentSRRules.MMI_M_BUTTONS = Variables.MMI_M_BUTTONS.No_Button;
            Variables.DataElement dataElement =
                new Variables.DataElement {Identifier = 15, EchoText = "1", QDataCheck = 1};
            List<Variables.DataElement> dataElements = new List<Variables.DataElement> {dataElement};
            EVC11_MMICurrentSRRules.DataElements = dataElements;
            EVC11_MMICurrentSRRules.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘Enter’ button for the data area of the SR speed data input field is displayed ‘Selected’ with black text on a grey background." +
                                Environment.NewLine +
                                @"2. The ‘Enter’ button for the data area of the SR distance data input field displays ‘1’." +
                                Environment.NewLine +
                                @"3. The echo text for SR speed displays ‘++++’ in red." + Environment.NewLine +
                                @"4. The ‘Yes’ button is disabled.");

            TraceHeader("Test Step 3");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Press the data input field once again (Accept) in the same screen");
            TraceReport("Expected Result");
            TraceInfo(
                "Input Field(1) The ‘Enter’ button associated to the data area of the input field is still coloured grey and its text is black (state ‘Selected IF/data value’).(2) The ‘Enter’ button associated to the data area of the input field displays “1” (previously entered value).EVC-106(3) Use the log file to verify that DMI does not send out packet EVC-106 as the ‘Enter’ button is disabled.Echo Texts of SR Speed(4) The data part of the echo text displays “++++”.(5) The data part of the echo text is coloured red");
            /*
            Test Step 3
            Action: Press the data input field once again (Accept) in the same screen
            Expected Result: Input Field(1) The ‘Enter’ button associated to the data area of the input field is still coloured grey and its text is black (state ‘Selected IF/data value’).(2) The ‘Enter’ button associated to the data area of the input field displays “1” (previously entered value).EVC-106(3) Use the log file to verify that DMI does not send out packet EVC-106 as the ‘Enter’ button is disabled.Echo Texts of SR Speed(4) The data part of the echo text displays “++++”.(5) The data part of the echo text is coloured red
            Test Step Comment: Requirements:(1) MMI_gen 8297 (partly: MMI_gen 4714 (partly: state 'Selected IF/data value');(2) MMI_gen 8297 (partly: MMI_gen 4714 (partly: previously entered (faulty) value)); MMI_gen 4699 (technical range);(3) MMI_gen 8297 (partly: MMI_gen 9286 (partly: button ‘Enter’, disabled), MMI_gen 12148 (partly: not send packets)), MMI_gen 9896 (partly: disabled), MMI_gen 9509 (partly: EVC-106); MMI_gen 9310 (partly: [technical range, No OK, button ‘Enter’ disabled]);(4) MMI_gen 8297 (partly: MMI_gen 12148 (MMI_gen 4713 (partly: indication))); MMI_gen 9509 (partly: only affect the object indicated in MMI_NID_DATA);(5) MMI_gen 9889 (partly: MMI_gen 12148 (MMI_gen 4713 (partly: red))), MMI_gen 8297 (partly: MMI_gen 12148 (MMI_gen 4713 (partly: red)));
            */
            DmiActions.ShowInstruction(this, @"Press again in the data input field to accept the value");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘Enter’ button for the data area of the SR speed data input field is stays ‘Selected’ with black text on a grey background." +
                                Environment.NewLine +
                                @"2. The ‘Enter’ button for the data area of the SR distance data input field still displays ‘1’." +
                                Environment.NewLine +
                                @"3. The echo text for SR speed still displays ‘++++’ in red." + Environment.NewLine +
                                @"4. The ‘Yes’ button is disabled.");

            TraceHeader("Test Step 4");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Enter “1” (invalid value) for SR speed with the numeric keypad in the same screen");
            TraceReport("Expected Result");
            TraceInfo(
                "Input Field(1) The eventually displayed data value in the data area of the input field is replaced by “1” (character or value corresponding to the activated data key - state ‘Selected IF/value of pressed key(s)’)");
            /*
            Test Step 4
            Action: Enter “1” (invalid value) for SR speed with the numeric keypad in the same screen
            Expected Result: Input Field(1) The eventually displayed data value in the data area of the input field is replaced by “1” (character or value corresponding to the activated data key - state ‘Selected IF/value of pressed key(s)’)
            Test Step Comment: Requirements:(1) MMI_gen 8297 (partly: MMI_gen 4714 (partly: MMI_gen 4679), MMI_gen 9286 (partly: button ‘Enter’, enabled)), MMI_gen 9896 (partly: state switched); MMI_gen 9310 (partly: press one key);
            */
            DmiActions.ShowInstruction(this, @"Enter the value ‘1’ in the SR speed data input field");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The SR speed data input field displays ‘1’");

            TraceHeader("Test Step 5");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Press the data input field of SR speed (Accept)");
            TraceReport("Expected Result");
            TraceInfo(
                "EVC-11Use the log file to verify that DMI receives packet EVC-11 with variable:(1) MMI_Q_DATA_CHECK = 1 in order to indicate the technical range check failure.(2) MMI_M_BUTTONS = 255 (no button) and the 'Yes' button is disabled.Input Field(3) The ‘Enter’ button associated to the data area of the input field is coloured grey and its text is black (state ‘Selected IF/Data value’)");
            /*
            Test Step 5
            Action: Press the data input field of SR speed (Accept)
            Expected Result: EVC-11Use the log file to verify that DMI receives packet EVC-11 with variable:(1) MMI_Q_DATA_CHECK = 1 in order to indicate the technical range check failure.(2) MMI_M_BUTTONS = 255 (no button) and the 'Yes' button is disabled.Input Field(3) The ‘Enter’ button associated to the data area of the input field is coloured grey and its text is black (state ‘Selected IF/Data value’)
            Test Step Comment: Requirements:(1) MMI_gen 8297 (partly: EVC-11, MMI_gen 12147); MMI_gen 9310 (partly: [Up-Type enabled button ‘Enter’], accept data);(2) MMI_gen 9892; (3) MMI_gen 8297 (partly: MMI_gen 4714 (partly: state 'Selected IF/data value'));
            */
            DmiActions.ShowInstruction(this, "Press in the SR speed data input field to accept the value");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘Enter’ button has black text on a grey background." + Environment.NewLine +
                                @"2. The ‘Yes’ button is disabled.");

            TraceHeader("Test Step 6");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Press the data input field of SR speed once again (Accept) in the same screen");
            TraceReport("Expected Result");
            TraceInfo(
                "Input Field(1) The ‘Enter’ button associated to the data area of the input field is still coloured grey and its text is black (state ‘Selected IF/data value’).(2) The ‘Enter’ button associated to the data area of the input field displays “1” (previously entered value).EVC-106(3) Use the log file to verify that DMI does not send out packet EVC-106 as the ‘Enter’ button is disabled. Echo Texts of SR Speed(4) The data part of the echo text displays “++++”.(5) The data part of the echo text is coloured red");
            /*
            Test Step 6
            Action: Press the data input field of SR speed once again (Accept) in the same screen
            Expected Result: Input Field(1) The ‘Enter’ button associated to the data area of the input field is still coloured grey and its text is black (state ‘Selected IF/data value’).(2) The ‘Enter’ button associated to the data area of the input field displays “1” (previously entered value).EVC-106(3) Use the log file to verify that DMI does not send out packet EVC-106 as the ‘Enter’ button is disabled. Echo Texts of SR Speed(4) The data part of the echo text displays “++++”.(5) The data part of the echo text is coloured red
            Test Step Comment: Requirements:(1) MMI_gen 8297 (partly: MMI_gen 4714 (partly: state 'Selected IF/data value');(2) MMI_gen 8297 (partly: MMI_gen 4714 (partly: previously entered (faulty) value)); MMI_gen 4699 (technical range);(3) MMI_gen 8297 (partly: MMI_gen 9286 (partly: button ‘Enter’, disabled), MMI_gen 12148 (partly: not send packets)), MMI_gen 9896 (partly: disabled), MMI_gen 9509 (partly: EVC-106); MMI_gen 9310 (partly: [technical range, No OK, button ‘Enter’ disabled]);(4) MMI_gen 8297 (partly: MMI_gen 12148 (MMI_gen 4713 (partly: indication))); MMI_gen 9509 (partly: only affect the object indicated in MMI_NID_DATA);(5) MMI_gen 9889 (partly: MMI_gen 12148 (MMI_gen 4713 (partly: red))), MMI_gen 8297 (partly: MMI_gen 12148 (MMI_gen 4713 (partly: red)));
            */
            DmiActions.ShowInstruction(this, "Press in the SR speed data input field again");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘Enter’ button still has black text on a grey background." +
                                Environment.NewLine +
                                @"2. The ‘Enter’ button displays ‘1’.");

            TraceHeader("Test Step 7");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Enter “40” (valid value) for SR speed with the numeric keypad");
            TraceReport("Expected Result");
            TraceInfo(
                "Input Field(1) The eventually displayed data value in the data area of the input field is replaced by “40” (character or value corresponding to the activated data key - state ‘Selected IF/value of pressed key(s)’)");
            /*
            Test Step 7
            Action: Enter “40” (valid value) for SR speed with the numeric keypad
            Expected Result: Input Field(1) The eventually displayed data value in the data area of the input field is replaced by “40” (character or value corresponding to the activated data key - state ‘Selected IF/value of pressed key(s)’)
            Test Step Comment: Requirements:(1) MMI_gen 8297 (partly: MMI_gen 4714 (partly: MMI_gen 4679), MMI_gen 9286 (partly: button ‘Enter’, enabled)), MMI_gen 9896 (partly: state switched); MMI_gen 9310 (partly: press one key);
            */
            DmiActions.ShowInstruction(this, @"Enter the (valid) value ‘40’ in the SR speed data input field");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The SR speed data input field displays ‘40’.");

            TraceHeader("Test Step 8");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Press the data input field of SR speed (Accept) in the same screen");
            TraceReport("Expected Result");
            TraceInfo(
                "Input Field(1) The ‘SR distance’ data input field remains the same.EVC-106(1) Use the log file to confirm that DMI sends packet EVC-106 with variable:-   MMI_V_STFF = 40-    MMI_M_BUTTONS =  254 (BTN_ENTER)-    MMI_N_DATA_ELEMENTS = 1-    MMI_NID_DATA = 15 (SR Speed)EVC-11(2) Use the log file to verify that DMI receives packet EVC-11 with variable:-    MMI_Q_DATA_CHECK = 0 (All checks have passed)-   MMI_X_TEXT = 52 (“4”)-    MMI_X_TEXT = 48 (“0”)");
            /*
            Test Step 8
            Action: Press the data input field of SR speed (Accept) in the same screen
            Expected Result: Input Field(1) The ‘SR distance’ data input field remains the same.EVC-106(1) Use the log file to confirm that DMI sends packet EVC-106 with variable:-   MMI_V_STFF = 40-    MMI_M_BUTTONS =  254 (BTN_ENTER)-    MMI_N_DATA_ELEMENTS = 1-    MMI_NID_DATA = 15 (SR Speed)EVC-11(2) Use the log file to verify that DMI receives packet EVC-11 with variable:-    MMI_Q_DATA_CHECK = 0 (All checks have passed)-   MMI_X_TEXT = 52 (“4”)-    MMI_X_TEXT = 48 (“0”)
            Test Step Comment: Requirements:(1) MMI_gen 9509 (partly: only affect the object indicated in MMI_NID_DATA);(2) MMI_gen 8297 (partly: MMI_gen 9286 (partly: enabled)), MMI_gen 9896 (partly: enabled), MMI_gen 9509 (partly: EVC-106, the ‘Enter’ button, accepted data complied with data checks, driver action);(3) MMI_gen 8297 (partly: EVC-11) MMI_gen 9310 (partly: [technical range, Yes OK]);
            */
            DmiActions.ShowInstruction(this, "Press in the SR speed data input field to accept the value");

            dataElement.QDataCheck = 0;
            dataElement.EchoText = "40";

            EVC106_MMINewSrRules.MMI_V_STFF = 40;
            EVC106_MMINewSrRules.MMI_NID_DATA = new List<byte> {15};
            EVC106_MMINewSrRules.MMI_M_BUTTONS = Variables.MMI_M_BUTTONS_SR_RULES.BTN_ENTER;
            EVC106_MMINewSrRules.CheckPacketContent();

            EVC11_MMICurrentSRRules.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The SR speed data input field still displays ‘40’.");

            TraceHeader("Test Step 9");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Follow step 2 – step 8 for SR distance with:invalid value of “1”valid value of “10000”");
            TraceReport("Expected Result");
            TraceInfo(
                "See step 2 – step 8EVC-106(1) Use the log file to confirm that DMI sends packet EVC-106 with variable:-    MMI_L_STFF = 10000 -    MMI_M_BUTTONS =  254 (BTN_ENTER)-    MMI_N_DATA_ELEMENTS = 1-    MMI_NID_DATA = 16 (SR Distance)EVC-11(2) Use the log file to verify that DMI receives packet EVC-11 with variable:-    MMI_Q_DATA_CHECK = 0 (All checks have passed)-    MMI_X_TEXT = 49 (“1”)-    MMI_X_TEXT = 48 (“0”)-    MMI_X_TEXT = 48 (“0”)-    MMI_X_TEXT = 48 (“0”)-    MMI_X_TEXT = 48 (“0”)");
            /*
            Test Step 9
            Action: Follow step 2 – step 8 for SR distance with:invalid value of “1”valid value of “10000”
            Expected Result: See step 2 – step 8EVC-106(1) Use the log file to confirm that DMI sends packet EVC-106 with variable:-    MMI_L_STFF = 10000 -    MMI_M_BUTTONS =  254 (BTN_ENTER)-    MMI_N_DATA_ELEMENTS = 1-    MMI_NID_DATA = 16 (SR Distance)EVC-11(2) Use the log file to verify that DMI receives packet EVC-11 with variable:-    MMI_Q_DATA_CHECK = 0 (All checks have passed)-    MMI_X_TEXT = 49 (“1”)-    MMI_X_TEXT = 48 (“0”)-    MMI_X_TEXT = 48 (“0”)-    MMI_X_TEXT = 48 (“0”)-    MMI_X_TEXT = 48 (“0”)
            Test Step Comment: See step 2 – step 8Requirements:(1) MMI_gen 8297 (partly: MMI_gen 9286 (partly: enabled)), MMI_gen 9896 (partly: enabled), MMI_gen 9510 (partly: EVC-106, the ‘Enter’ button, accepted data complied with data checks, driver action, only affect the object indicated in MMI_NID_DATE);(2) MMI_gen 8297 (partly: EVC-11) MMI_gen 9310 (partly: [technical range, Yes OK]);
            */
            // Repeat Step 2 for SR distance
            DmiActions.ShowInstruction(this,
                @"Enter the (invalid) value ‘1’ in the SR distance data input field and press in the data input field to accept the valu.");

            EVC11_MMICurrentSRRules.MMI_M_BUTTONS = Variables.MMI_M_BUTTONS.No_Button;
            dataElement.Identifier = 16;
            EVC11_MMICurrentSRRules.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘Enter’ button for the data area of the SR distance data input field is displayed ‘Selected’ with black text on a grey background." +
                                Environment.NewLine +
                                @"2. The ‘Enter’ button for the data area of the SR speed data input field displays ‘1’." +
                                Environment.NewLine +
                                @"3. The echo text for SR distance displays ‘++++’ in red." + Environment.NewLine +
                                @"4. The ‘Yes’ button is disabled.");

            // Repeat Step 3 for SR distance
            DmiActions.ShowInstruction(this, @"Press again in the data input field to accept the value");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘Enter’ button for the data area of the SR distance data input field is stays ‘Selected’ with black text on a grey background." +
                                Environment.NewLine +
                                @"2. The ‘Enter’ button for the data area of the SR speed data input field still displays ‘1’." +
                                Environment.NewLine +
                                @"3. The echo text for SR distance still displays ‘++++’ in red." +
                                Environment.NewLine +
                                @"4. The ‘Yes’ button is disabled.");

            // Repeat Step 4 for SR distance
            DmiActions.ShowInstruction(this, @"Enter the value ‘1’ in the SR distance data input field");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The SR distance data input field displays ‘1’");

            // Repeat Step 5 for SR distance
            DmiActions.ShowInstruction(this, "Press in the SR distance data input field to accept the value");

            EVC11_MMICurrentSRRules.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘Enter’ button has black text on a grey background." + Environment.NewLine +
                                @"2. The ‘Yes’ button is disabled.");

            // Repeat Step 6 for SR distance
            DmiActions.ShowInstruction(this, "Press in the SR distance data input field again");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘Enter’ button still has black text on a grey background." +
                                Environment.NewLine +
                                @"2. The ‘Enter’ button displays ‘1’.");

            // Repeat Step 7 for SR distance
            DmiActions.ShowInstruction(this, @"Enter the (valid) value ‘10000’ in the SR distance data input field");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The SR distance data input field displays ‘40’.");

            DmiActions.ShowInstruction(this, "Press in the SR speed data input field to accept the value");

            // Repeat Step 8 for SR distance
            dataElement.EchoText = "10000";
            dataElement.Identifier = 16;
            EVC11_MMICurrentSRRules.Send();


            EVC106_MMINewSrRules.MMI_V_STFF = 10000;
            EVC106_MMINewSrRules.MMI_NID_DATA = new List<byte> {16};
            EVC106_MMINewSrRules.MMI_M_BUTTONS = Variables.MMI_M_BUTTONS_SR_RULES.BTN_ENTER;
            EVC106_MMINewSrRules.CheckPacketContent();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The SR distance data input field still displays ‘10000’.");

            TraceHeader("Test Step 10");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "This step is to complete the process of ‘SR speed / distance’:- Press the ‘Yes’ button on the ‘SR speed / distance’ window.- Validate the data in the data validation window");
            TraceReport("Expected Result");
            TraceInfo(
                "1. After pressing the ‘Yes’ button, the data validation window (‘Validate SR speed / distance’) appears instead of the ‘SR speed / distance’ data entry window. The data part of echo text displays in white:SR Speed: 40SR Distance: 100002. After the data area of the input field containing “Yes” is pressed, the data validation window disappears and returns to the parent window (‘Special’ window) of ‘SR speed / distance’ window with enabled ‘SR speed / distance’ button");
            /*
            Test Step 10
            Action: This step is to complete the process of ‘SR speed / distance’:- Press the ‘Yes’ button on the ‘SR speed / distance’ window.- Validate the data in the data validation window
            Expected Result: 1. After pressing the ‘Yes’ button, the data validation window (‘Validate SR speed / distance’) appears instead of the ‘SR speed / distance’ data entry window. The data part of echo text displays in white:SR Speed: 40SR Distance: 100002. After the data area of the input field containing “Yes” is pressed, the data validation window disappears and returns to the parent window (‘Special’ window) of ‘SR speed / distance’ window with enabled ‘SR speed / distance’ button
            */
            DmiActions.ShowInstruction(this, "Press the ‘Yes’ button");

            // Add a data element for correct SR speed
            dataElements.Add(new Variables.DataElement {Identifier = 15, EchoText = "40", QDataCheck = 1});
            EVC11_MMICurrentSRRules.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The echo text for the data area of the SR speed data input field displays ‘40’ in white." +
                                Environment.NewLine +
                                @"2. The echo text for the data area of the SR distance data input field displays ‘10000’ in white.");

            DmiActions.ShowInstruction(this, "Press the data area of the ‘Yes’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. DMI displays the SR speed/distance window with the ‘SR speed / distance’ button enabled.");

            TraceHeader("Test Step 11");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Send the data of ‘Technical Range Check’ failure to ETCS-DMI by 22_9_9_a.xmlEVC-11MMI_Q_DATA_CHECK = 1 (Technical Range Check failed)MMI_NID_DATA = 16 (SR Distance)");
            TraceReport("Expected Result");
            TraceInfo(
                "Input Field(1) The ‘Enter’ button associated to the data area of the input field displays the previously entered value.Echo Texts of SR Distance(2) The data part of the echo text displays “++++”");
            /*
            Test Step 11
            Action: Send the data of ‘Technical Range Check’ failure to ETCS-DMI by 22_9_9_a.xmlEVC-11MMI_Q_DATA_CHECK = 1 (Technical Range Check failed)MMI_NID_DATA = 16 (SR Distance)
            Expected Result: Input Field(1) The ‘Enter’ button associated to the data area of the input field displays the previously entered value.Echo Texts of SR Distance(2) The data part of the echo text displays “++++”
            Test Step Comment: Requirements:(1) MMI_gen 8297 (partly: MMI_gen 4714 (partly: previously entered (faulty) value)); MMI_gen 4699 (technical range);(2) MMI_gen 8297 (partly: MMI_gen 12148 (MMI_gen 4713 (partly: indication))) MMI_gen 9509 (partly: only affect the object indicated in MMI_NID_DATA);Note: This is a temporary approach for non-support test environment on the data checks.
            */
            XML_22_9_9(msgType.typea);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The echo text for SR speed still displays ‘40’." + Environment.NewLine +
                                @"2. The echo text for SR distance still displays ‘10000’." + Environment.NewLine +
                                @"2. The data parts of the echo texts display ‘++++’.");

            TraceHeader("Test Step 12");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Send the data of ‘Technical Range Check’ failure to ETCS-DMI by 22_9_9_b.xmlEVC-11MMI_Q_DATA_CHECK = 1 (Technical Range Check failed)MMI_NID_DATA = 15 (SR Speed)");
            TraceReport("Expected Result");
            TraceInfo(
                "Input Field(1) The ‘Enter’ button associated to the data area of the input field displays the previously entered value.Echo Texts of SR Speed(2) The data part of the echo text displays “++++”");
            /*
            Test Step 12
            Action: Send the data of ‘Technical Range Check’ failure to ETCS-DMI by 22_9_9_b.xmlEVC-11MMI_Q_DATA_CHECK = 1 (Technical Range Check failed)MMI_NID_DATA = 15 (SR Speed)
            Expected Result: Input Field(1) The ‘Enter’ button associated to the data area of the input field displays the previously entered value.Echo Texts of SR Speed(2) The data part of the echo text displays “++++”
            Test Step Comment: Requirements:(1) MMI_gen 8297 (partly: MMI_gen 4714 (partly: previously entered (faulty) value)); MMI_gen 4699 (technical range);(2) MMI_gen 8297 (partly: MMI_gen 12148 (MMI_gen 4713 (partly: indication))) MMI_gen 9509 (partly: only affect the object indicated in MMI_NID_DATA);Note: This is a temporary approach for non-support test environment on the data checks.
            */
            XML_22_9_9(msgType.typeb);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘Enter’ button for the data area of the SR speed data input field  still displays ‘40’." +
                                Environment.NewLine +
                                @"2. The ‘Enter’ button for the data area of the SR distance data input field still displays ‘10000’." +
                                Environment.NewLine +
                                @"3. The echo text for SR speed still displays ‘++++’.");

            TraceHeader("Test Step 13");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("End of test");
            TraceReport("Expected Result");
            TraceInfo("");
            /*
            Test Step 13
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }

        #region Send_XML_22_9_9_DMI_Test_Specification

        enum msgType
        {
            typea,
            typeb
        }

        private void XML_22_9_9(msgType type)
        {
            switch (type)
            {
                case msgType.typea:
                    //some values taken from xml file not spec where different
                    EVC11_MMICurrentSRRules.DataElements = new List<Variables.DataElement>
                    {
                        new Variables.DataElement {Identifier = 16, EchoText = "", QDataCheck = 1}
                    };
                    EVC11_MMICurrentSRRules.MMI_M_BUTTONS = Variables.MMI_M_BUTTONS.No_Button;
                    break;
                case msgType.typeb:
                    //some values taken from xml file not spec where different
                    EVC11_MMICurrentSRRules.MMI_L_STFF = 100000;
                    EVC11_MMICurrentSRRules.MMI_V_STFF = 100;
                    EVC11_MMICurrentSRRules.MMI_M_BUTTONS = Variables.MMI_M_BUTTONS.BTN_LEVEL;
                    EVC11_MMICurrentSRRules.DataElements =
                        new List<Variables.DataElement> {new Variables.DataElement {Identifier = 15, QDataCheck = 1}};
                    break;
            }

            EVC11_MMICurrentSRRules.Send();
        }

        #endregion
    }
}
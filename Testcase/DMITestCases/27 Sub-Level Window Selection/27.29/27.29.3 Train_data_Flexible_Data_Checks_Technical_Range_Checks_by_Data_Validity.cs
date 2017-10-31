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
using static Testcase.Telegrams.EVCtoDMI.Variables;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 27.29.3 ‘Train data’ (Flexible) Data Checks: Technical Range Checks by Data Validity
    /// TC-ID: 22.29.3
    /// 
    /// This test case verifies the functionalities of the ‘Train data’ data entry when the data of Train data does not comply with data-validity rules of the technical range check. The function designs comply with the conditions in [MMI-ETCS-gen]. The data range and interface comply with the data information in [VSIS_gen].
    /// 
    /// Tested Requirements:
    /// 1. Train data Window: MMI_gen 9408; MMI_gen 9413; MMI_gen 9419 (partly: the ‘Enter’ button, accepted data complied with data checks, technical range, echo text);2. Data Checks: MMI_gen 8089 (partly: MMI_gen 12148, MMI_gen 4713, MMI_gen 4714, MMI_gen 4679, MMI_gen 9286 (partly: the ‘Enter’ button, disabled, enabled), MMI_gen 12147); MMI_gen 9404 (partly: MMI_gen 12148 (MMI_gen 4713 (partly: red)); MMI_gen 9310 (partly: technical range);
    /// 
    /// Scenario:
    /// 1.Activate the cabin.
    /// 2.Perform SoM until mode SR is confirmed in the default window.
    /// 3.Press the ‘Main’ button. Then, the ‘Main’ window is opened.
    /// 4.Press the ‘Train data’ button. Then, the ‘Train data’ window is opened.
    /// 5.Enter the numeric data of Train data, i.e. the maximum train length, maximum train speed, brake percentage, . Then, the ‘Train data’ window is verified by the following events:a.   Enter and accept the invalid data. b.   Accept the previuos invalid data without re-enter in order that DMI does not send out any packets (The ‘Enter’ button is disabled).c.   Repeat a. and b. in order that the invalid data is re-entered and accepted although the ‘Enter’ button is disabled.d.   Repeat a. by valid data in order that the data is entered and accepted after the ‘Enter’ button is disabled.Note: The appearance of highlighting in data area has remained in DMI since BL-2 requirement [MMI-ETCS-gen BL2].
    /// 
    /// Used files:
    /// 22_29_3_a.xml, 22_29_3_b.xml, 22_29_3_c.xml 
    /// </summary>
    /// 

    //------------------------------------------------------------
    // This test has been left incomplete: not clear if/when flexible train tests will be needed
    // EVC6 needs extension to allow correct data checking (echo test display...)
    // ===========================================================

    public class TC_ID_22_29_3_Train_data_Flexible_Data_Checks_Technical_Range_Checks_by_Data_Validity : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:

            // Call the TestCaseBase PreExecution
            base.PreExecution();

            // 1. The test environment is powered on.
            // 2. ATP-CU is verified that the train is set as ‘Flexible’.TR_OBU_TrainType = 2
            // 3. The cabin is activated.
            // 4. The ‘Start of Mission’ procedure is performed until the ‘Staff Resonsible’ mode, level 1, is confirmed.
            // 5. The ‘Main’ window is opened.
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
            TraceInfo("This test case requires an ATP configuration change - " +
                      "See Precondition requirements. If this is not done manually, the test may fail!");


            /*
            Test Step 1
            Action: Open the ‘Train data’ data entry window from the Main menu
            Expected Result: The ‘Train data’ data entry window appears on DMI screen instead of the ‘Main’ menu window
            */
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = 1;
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.TrainData;
            EVC30_MMIRequestEnable.Send();

            DmiActions.ShowInstruction(this, "Press the ‘Train data’ button");
            
            DmiActions.Send_EVC6_MMICurrentTrainData(Variables.MMI_M_DATA_ENABLE.TrainLength |
                                                     Variables.MMI_M_DATA_ENABLE.BrakePercentage |
                                                     Variables.MMI_M_DATA_ENABLE.MaxTrainSpeed,
                                                     100, 200,
                                                     Variables.MMI_NID_KEY.PASS2,
                                                     70,
                                                     Variables.MMI_NID_KEY.CATA,
                                                     0,
                                                     Variables.MMI_NID_KEY.G1,
                                                     EVC6_MMICurrentTrainData.MMI_M_BUTTONS_CURRENT_TRAIN_DATA.BTN_YES_DATA_ENTRY_COMPLETE,
                                                     0, 0, new[] { "FLU", "RLU", "Rescue" }, null);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Train data window instead of the Main window.");

            /*
            Test Step 2
            Action: Enter “1” (invalid value) for Train Length with the numeric keypad and press the data input field (Accept) in the same screen
            Expected Result: EVC-6Use the log file to verify that DMI receives variables of packet EVC-6 as below:(1) MMI_Q_DATA_CHECK = 1 in order to indicate the technical range check failure.(2) MMI_M_BUTTONS = 255 (no button) and the 'Yes' button is disabled.(3) MMI_NID_DATA = 8 (Length)Input Field(4) The ‘Enter’ button associated to the data area of the input field is coloured grey and its text is black (state ‘Selected IF/Data value’).(5) The ‘Enter’ button associated to the data area of the input field displays “1” (previously entered value).Echo Texts of Train Length(6) The data part of the echo text displays “++++”.(7) The data part of the echo text is coloured red
            Test Step Comment: Requirements:(1) MMI_gen 8089 (partly: EVC-6, MMI_gen 12147);(2) MMI_gen 9408;(3) MMI_gen 9419 (partly: MMI_NID_DATA);(4) MMI_gen 8089 (partly: MMI_gen 4714 (partly: state 'Selected IF/data value')); MMI_gen 9310 (partly: accept data);(5) MMI_gen 8089 (partly: MMI_gen 4714 (partly: previously entered (faulty) value)); MMI_gen 4699 (technical range); MMI_gen 9419 (partly: EVC-6 does not affect);(6) MMI_gen 8089 (partly: MMI_gen 4713 (partly: indication)); MMI_gen 9310 (partly: [technical range, No OK, echo text]); MMI_gen 9419 (partly: technical range, echo text);(7) MMI_gen 9404 (partly: MMI_gen 4713 (partly: red)), MMI_gen 8089 (partly: MMI_gen 4713 (partly: red)); MMI_gen 9419 (partly: technical range, red);
            */
            DmiActions.ShowInstruction(this, "Using the numeric keypad, enter ‘1’ for the Train length and press the data input field to accept the value");

            EVC6_MMICurrentTrainData.MMI_M_BUTTONS = EVC6_MMICurrentTrainData.MMI_M_BUTTONS_CURRENT_TRAIN_DATA.NoButton;

            /*
            Test Step 3
            Action: Press the data input field once again (Accept) in the same screen
            Expected Result: Input Field(1) The ‘Enter’ button associated to the data area of the input field is still coloured grey and its text is black (state ‘Selected IF/data value’).(2) The ‘Enter’ button associated to the data area of the input field displays “1” (previously entered value).EVC-107(3) Use the log file to verify that DMI does not send out packet EVC-107 as the ‘Enter’ button is disabled.Echo Texts of Train Length(4) The data part of the echo text displays “++++”.(5) The data part of the echo text is coloured red
            Test Step Comment: Requirements:(1) MMI_gen 8089 (partly: MMI_gen 4714 (partly: state 'Selected IF/data value');(2) MMI_gen 8089 (partly: MMI_gen 4714 (partly: previously entered (faulty) value)); MMI_gen 4699 (technical range); MMI_gen 9419 (partly: EVC-6 does not affect);(3) MMI_gen 8089 (partly: MMI_gen 9286 (partly: button ‘Enter’, disabled), MMI_gen 12148 (partly: not send packets)), MMI_gen 9413 (partly: disabled), MMI_gen 9419 (partly: EVC-107); MMI_gen 9310 (partly: [technical range, No OK, button ‘Enter’ disabled]);(4) MMI_gen 8089 (partly: MMI_gen 12148 (MMI_gen 4713 (partly: indication))); MMI_gen 9419 (partly: only affect the object indicated in MMI_NID_DATA); MMI_gen 9419 (partly: technical range, echo text);(5) MMI_gen 9404 (partly: MMI_gen 12148 (MMI_gen 4713 (partly: red))), MMI_gen 8089 (partly: MMI_gen 12148 (MMI_gen 4713 (partly: red)));
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press the data input field once again (Accept) in the same screen");


            /*
            Test Step 4
            Action: Enter “1” (invalid value) for Train Length with the numeric keypad in the same screen
            Expected Result: Input Field(1) The eventually displayed data value in the data area of the input field is replaced by “1” (character or value corresponding to the activated data key - state ‘Selected IF/value of pressed key(s)’)
            Test Step Comment: Requirements:(1) MMI_gen 8089 (partly: MMI_gen 4714 (partly: MMI_gen 4679), MMI_gen 9286 (partly: button ‘Enter’, enabled)), MMI_gen 9413 (partly: state switched); MMI_gen 9310 (partly: press one key);
            */
            // Call generic Check Results Method
            DmiExpectedResults
                .Input_Field1_The_eventually_displayed_data_value_in_the_data_area_of_the_input_field_is_replaced_by_1_character_or_value_corresponding_to_the_activated_data_key_state_Selected_IFvalue_of_pressed_keys(this);


            /*
            Test Step 5
            Action: Press the data input field of Train Length (Accept)
            Expected Result: EVC-6Use the log file to verify that DMI receives variables in packet EVC-6 as below:(1) MMI_Q_DATA_CHECK = 1 in order to indicate the technical range check failure.(2) MMI_M_BUTTONS = 255 (no button) and the 'Yes' button is disabled.Input Field(3) The ‘Enter’ button associated to the data area of the input field is coloured grey and its text is black (state ‘Selected IF/Data value’)
            Test Step Comment: Requirements:(1) MMI_gen 8089 (partly: EVC-6, MMI_gen 12147); MMI_gen 9310 (partly: [Up-Type enabled button ‘Enter’], accept data);(2) MMI_gen 9408; (3) MMI_gen 8089 (partly: MMI_gen 4714 (partly: state 'Selected IF/data value'));
            */


            /*
            Test Step 6
            Action: Press the data input field of Train Length once again (Accept) in the same screen
            Expected Result: Input Field(1) The ‘Enter’ button associated to the data area of the input field is still coloured grey and its text is black (state ‘Selected IF/data value’).(2) The ‘Enter’ button associated to the data area of the input field displays “1” (previously entered value).EVC-107(3) Use the log file to verify DMI does not send out packet EVC-107 as the ‘Enter’ button is disabled. Echo Texts of Train Length(4) The data part of the echo text displays “++++”.(5) The data part of the echo text is coloured red
            Test Step Comment: Requirements:(1) MMI_gen 8089 (partly: MMI_gen 4714 (partly: state 'Selected IF/data value');(2) MMI_gen 8089 (partly: MMI_gen 4714 (partly: previously entered (faulty) value)); MMI_gen 4699 (technical range); MMI_gen 9419 (partly: EVC-6 does not affect);(3) MMI_gen 8089 (partly: MMI_gen 9286 (partly: button ‘Enter’, disabled), MMI_gen 12148 (partly: not send packets)), MMI_gen 9413 (partly: disabled), MMI_gen 9419 (partly: EVC-107); MMI_gen 9310 (partly: [technical range, No OK, button ‘Enter’ disabled]);(4) MMI_gen 8089 (partly: MMI_gen 12148 (MMI_gen 4713 (partly: indication))); MMI_gen 9419 (partly: only affect the object indicated in MMI_NID_DATA); MMI_gen 9419 (partly: technical range, echo text);(5) MMI_gen 9404 (partly: MMI_gen 12148 (MMI_gen 4713 (partly: red))), MMI_gen 8089 (partly: MMI_gen 12148 (MMI_gen 4713 (partly: red)));
            */


            /*
            Test Step 7
            Action: Enter “200” (valid value) for Train Length with the numeric keypad
            Expected Result: Input Field(1) The eventually displayed data value in the data area of the input field is replaced by “200” (character or value corresponding to the activated data key - state ‘Selected IF/value of pressed key(s)’)
            Test Step Comment: Requirements:(1) MMI_gen 8089 (partly: MMI_gen 4714 (partly: MMI_gen 4679), MMI_gen 9286 (partly: button ‘Enter’, enabled)), MMI_gen 9413 (partly: state switched); MMI_gen 9310 (partly: press one key);
            */


            /*
            Test Step 8
            Action: Press the data input field of Train Length (Accept) in the same screen
            Expected Result: Input Field(1) The ‘Brake Percentage’ data input field remains the same.EVC-107(2) Use the log file to verify that DMI sends packet EVC-107 with variable:MMI_L_TRAIN_ = 200MMI_M_BUTTONS =  254 (BTN_ENTER)MMI_N_DATA_ELEMENTS = 1MMI_NID_DATA = 8 (Length)EVC-6(3) Use the log file to verify that DMI receives packet EVC-6 with variable:MMI_N_DATA_ELEMENTS = 1MMI_Q_DATA_CHECK = 0 (All checks have passed)MMI_X_TEXT = 50 (“2”)MMI_X_TEXT = 48 (“0”)MMI_X_TEXT = 48 (“0”)
            Test Step Comment: Requirements:(1) MMI_gen 9419 (partly: only affect the object indicated in MMI_NID_DATA);(2) MMI_gen 8089 (partly: MMI_gen 9286 (partly: enabled)), MMI_gen 9413 (partly: enabled), MMI_gen 9419 (partly: EVC-107, the ‘Enter’ button, accepted data complied with data checks, driver action);(3) MMI_gen 8089 (partly: EVC-6) MMI_gen 9310 (partly: [technical range, Yes OK]); MMI_gen 9419 (partly: technical range, echo text, MMI_X_TEXT);
            */


            /*
            Test Step 9
            Action: Follow step 2 – step 8 for Brake Percentage with:invalid value of “1”valid value of “135”
            Expected Result: See step 2 – step 8EVC-107(1) Use the log file to verify that DMI sends packet EVC-107 with variable:MMI_M_BRAKE_PERC = 135 MMI_M_BUTTONS =  254 (BTN_ENTER)MMI_N_DATA_ELEMENTS = 1MMI_NID_DATA = 9 (Brake Percentage)EVC-6(2) Use the log file to verify that DMI receives packet EVC-6 with variable:MMI_N_DATA_ELEMENTS = 1MMI_Q_DATA_CHECK = 0 (All checks have passed)MMI_X_TEXT = 49 (“1”)MMI_X_TEXT = 51 (“3”)MMI_X_TEXT = 53 (“5”)
            Test Step Comment: See step 2 – step 8Requirements:(1) MMI_gen 8089 (partly: MMI_gen 9286 (partly: enabled)), MMI_gen 9413 (partly: enabled), MMI_gen 9419 (partly: EVC-107, the ‘Enter’ button, accepted data complied with data checks, driver action, only affect the object indicated in MMI_NID_DATA);(2) MMI_gen 8089 (partly: EVC-6) MMI_gen 9310 (partly: [technical range, Yes OK]); MMI_gen 9419 (partly: technical range, echo text, MMI_X_TEXT);
            */


            /*
            Test Step 10
            Action: Follow step 2 – step 8 for Max speed with:invalid value of “1”valid value of “160”
            Expected Result: See step 2 – step 8EVC-107(1) Use the log file to verify that DMI sends packet EVC-107 with variable:MMI_V_MAXTRAIN = 160 MMI_M_BUTTONS =  254 (BTN_ENTER)MMI_N_DATA_ELEMENTS = 1MMI_NID_DATA = 10 (Maximum speed)EVC-6(2) Use the log file to verify that DMI receives packet EVC-6 with variable:MMI_N_DATA_ELEMENTS = 1MMI_Q_DATA_CHECK = 0 (All checks have passed)MMI_X_TEXT = 49 (“1”)MMI_X_TEXT = 54 (“6”)MMI_X_TEXT = 48 (“0”)
            Test Step Comment: See step 2 – step 8Requirements:(1) MMI_gen 8089 (partly: MMI_gen 9286 (partly: enabled)), MMI_gen 9413 (partly: enabled), MMI_gen 9419 (partly: EVC-107, the ‘Enter’ button, accepted data complied with data checks, driver action, only affect the object indicated in MMI_NID_DATA);(2) MMI_gen 8089 (partly: EVC-6) MMI_gen 9310 (partly: [technical range, Yes OK]); MMI_gen 9419 (partly: technical range, echo text, MMI_X_TEXT);
            */


            /*
            Test Step 11
            Action: This step is to complete the process of ‘Train data’:- Press the ‘Yes’ button on the ‘Train data’ window.- Validate the data in the data validation window
            Expected Result: 1. After pressing the ‘Yes’ button, the data validation window (‘Validate Train data’) appears instead of the ‘Train data’ data entry window. The data part of echo text displays in white:Train Length: 200Brake Percentage: 135Max speed: 1602. After the data area of the input field containing “Yes” is pressed, the data validation window disappears and returns to the parent window (‘Main’ window) of ‘Train data’ window with enabled ‘Train data’ button
            */
            // Call generic Action Method
            DmiActions
                .This_step_is_to_complete_the_process_of_Train_data_Press_the_Yes_button_on_the_Train_data_window_Validate_the_data_in_the_data_validation_window(this);


            /*
            Test Step 12
            Action: Send the data of ‘Technical Range Check’ failure to ETCS-DMI by 22_29_3_a.xmlEVC-6MMI_Q_DATA_CHECK = 1 (Technical Range Check failed)MMI_NID_DATA = 9 (Brake Percentage)
            Expected Result: Input Field(1) The ‘Enter’ button associated to the data area of the input field displays the previously entered value.Echo Texts of Brake Percentage(2) The data part of the echo text displays “++++”
            Test Step Comment: Requirements:(1) MMI_gen 8089 (partly: MMI_gen 4714 (partly: previously entered (faulty) value)); MMI_gen 4699 (technical range); MMI_gen 9419 (partly: EVC-6 does not affect);(2) MMI_gen 8089 (partly: MMI_gen 12148 (MMI_gen 4713 (partly: indication))) MMI_gen 9419 (partly: only affect the object indicated in MMI_NID_DATA);Note: This is a temporary approach for non-support test environment on the data checks.
            */


            /*
            Test Step 13
            Action: Send the data of ‘Technical Range Check’ failure to ETCS-DMI by 22_29_3_b.xmlEVC-6MMI_Q_DATA_CHECK = 1 (Technical Range Check failed)MMI_NID_DATA = 8 (Train Length)
            Expected Result: Input Field(1) The ‘Enter’ button associated to the data area of the input field displays the previously entered value.Echo Texts of Train Length(2) The data part of the echo text displays “++++”
            Test Step Comment: Requirements:(1) MMI_gen 8089 (partly: MMI_gen 4714 (partly: previously entered (faulty) value)); MMI_gen 4699 (technical range); MMI_gen 9419 (partly: EVC-6 does not affect);(2) MMI_gen 8089 (partly: MMI_gen 12148 (MMI_gen 4713 (partly: indication))) MMI_gen 9419 (partly: only affect the object indicated in MMI_NID_DATA);Note: This is a temporary approach for non-support test environment on the data checks.
            */


            /*
            Test Step 14
            Action: Send the data of ‘Technical Range Check’ failure to ETCS-DMI by 22_29_3_c.xmlEVC-6MMI_Q_DATA_CHECK = 1 (Technical Range Check failed)MMI_NID_DATA = 10 (Maximum speed)
            Expected Result: Input Field(1) The ‘Enter’ button associated to the data area of the input field displays the previously entered value.Echo Texts of Max speed(2) The data part of the echo text displays “++++”
            Test Step Comment: Requirements:(1) MMI_gen 8089 (partly: MMI_gen 4714 (partly: previously entered (faulty) value)); MMI_gen 4699 (technical range); MMI_gen 9419 (partly: EVC-6 does not affect);(2) MMI_gen 8089 (partly: MMI_gen 12148 (MMI_gen 4713 (partly: indication))) MMI_gen 9419 (partly: only affect the object indicated in MMI_NID_DATA);Note: This is a temporary approach for non-support test environment on the data checks.
            */


            /*
            Test Step 15
            Action: End of test
            Expected Result: 
            */


            return GlobalTestResult;
        }
    }
}
using System;
using Testcase.Telegrams.EVCtoDMI;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 15.2.5 State 'ST05': Special window and windows in the special menu
    /// TC-ID: 10.2.5
    /// 
    /// This test case verifies the buttons of the default window, the special window and windows in the special menu when entry and exit on state of 'ST05'.
    /// 
    /// Tested Requirements:
    /// MMI_gen 12018 (partly: the default window, the special window and windows in the special menu); MMI_gen 168 (partly: deselect input field, disabled buttons, the default window, the special window and windows in the special menu); MMI_gen 4395 (partly: close button, disabled, the special window and windows in the special menu); MMI_gen 4396 (partly: close, NA11, NA12, the special window and windows in the special menu); MMI_gen 5646 (partly: always enable, State ‘ST05’ button is disabled, the special window and windows in the special menu); MMI_gen 5728 (partly: input field, removal, restore after ST05, default window without ST05 restore, the special window and windows in the special menu); MMI_gen 8859 (partly: the special window and windows in the special menu); Note under the MMI_gen 5728;
    /// 
    /// Scenario:
    /// 1.The ‘Special’ menu window is displayed.
    /// 2.Use the test script files to send packets in order to verify state ‘ST05’ in a menu window.
    /// 3.Open the ‘SR speed/distance’ window and use the test script files to send packets in order to verify state ‘ST05’.
    /// 4.Open the ‘default’ window and use the test script files to send packets in order to verify state ‘ST05’.
    /// 5.Drive the train forward pass BG1 at position 100m.
    /// 6.Open the ‘Adhesion’ window and use the test script files to send packets in order to verify state ‘ST05’.
    /// 
    /// Used files:
    /// 10_2_5_a.xml, 10_2_5.tdg
    /// </summary>
    public class TC_ID_10_2_5_State_ST05 : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:

            // Call the TestCaseBase PreExecution
            base.PreExecution();
            // Test system is powered onCabin is activeSoM is performed in SR mode, level 1.
            DmiActions.Complete_SoM_L1_SR(this);
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint

            /*
            Test Step 1
            Action: Press ‘Spec’ button
            Expected Result: Verify the following information;(1)   Verify DMI still displays Default window until Special window is displayed.(2)   Verify the close button is always enable
            Test Step Comment: (1) MMI_gen 8859 (partly: special window);(2) MMI_gen 5646 (partly: always enable, special window);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press the ‘Spec’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays Default window until Special window is displayed." +
                                Environment.NewLine +
                                "2. ‘Close’ button is always enabled.");

            /*
            Test Step 2
            Action: Use the test script file 10_2_5_a.xml to disable and enable button of the special menu via EVC-8 withPacket 1 (Entry state of ‘ST05’)MMI_Q_TEXT_CRITERIA = 3 MMI_Q_TEXT = 716Packet 2 (Exit state of ‘ST05’)MMI_Q_TEXT_CRITERIA = 4MMI_Q_TEXT = 716Note: Stopwatch is required for accuracy of test result
            Expected Result: Verify the following information;DMI in the entry state of ‘ST05’(1)   The hourglass symbol ST05 is displayed.(2)   Verify all buttons and the close button is disable.(3)   The disabled Close button NA12 is displayed in area G.10 seconds laterDMI in the exit state of ‘ST05’(4)   The hourglass symbol ST05 is removed.(5)   The state of all buttons is restored according to the last status before script is sent.(6)  The enabled Close button NA11 is displayed in area G
            Test Step Comment: (1) MMI_gen 12018 (partly: special window);(2) MMI_gen 168 (partly: disabled buttons, special window); MMI_gen 5646 (partly: State ‘ST05’ button is disabled, special window); MMI_gen 4395 (partly: close button, disabled, special window);(3) MMI_gen 4396 (partly: close, NA12, special window);(4) MMI_gen 5728 (partly: removal, EVC, special window);(5) MMI_gen 5728 (partly: restore after ST05, special window);(6) MMI_gen 4396 (partly: close, NA11, special window);
            */
            XML_10_5_a();

            /*
            Test Step 3
            Action: Perform the following procedure;Press ‘Spec’ button Press ‘SR speed/distance’ button
            Expected Result: Verify the following information;(1)   Verify DMI still displays Special window until SR speed/distance window is displayed.(2)   Verify the close button is always enable
            Test Step Comment: (1) MMI_gen 8859 (partly: windows in special menu);(2) MMI_gen 5646 (partly: always enable, windows in special menu);
            */
            DmiActions.ShowInstruction(this, @"Close the Special window then press the ‘Spec’ button");

            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Special; // Special window
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.SRSpeedDistance |
                                                               EVC30_MMIRequestEnable.EnabledRequests.Adhesion;
            EVC30_MMIRequestEnable.Send();

            DmiActions.ShowInstruction(this, @"Press ‘S/R speed distance’ button.");

            EVC11_MMICurrentSRRules.MMI_M_BUTTONS = Variables.MMI_M_BUTTONS.BTN_YES_DATA_ENTRY_COMPLETE;
            EVC11_MMICurrentSRRules.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays Special window until SR speed/distance window is displayed." +
                                Environment.NewLine +
                                "2. ‘Close’ button is always enabled.");

            /*
            Test Step 4
            Action: Repeat action step 2 with SR speed/distance window
            Expected Result: See the expectation in step 2 and also verify the following information;DMI in the entry state of ‘ST05’(1)   All Input Field are deselected.10 seconds laterDMI in the exit state of ‘ST05’(2)  The input field is stated as follows:The first input field is in the ‘Selected’ state.The all others are in the ‘Not selected’ state
            Test Step Comment: See step 2 for the SR speed/distance window in the special menu(1) MMI_gen 168 (partly: deselect input field);(2) MMI_gen 5728 (partly: input field);
            */
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI is in the entry state of ‘ST05’." + Environment.NewLine +
                                "2. The hourglass symbol ST05 is displayed." + Environment.NewLine +
                                "3. All buttons and the ‘Close’ button are disabled." + Environment.NewLine +
                                "4. ‘Close’ button NA12 is displayed disabled in area G." + Environment.NewLine +
                                "5. All Input Fields are not selected");

            this.Wait_Realtime(10000);

            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 4;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI is in the exit state of ‘ST05’." + Environment.NewLine +
                                "2. The hourglass symbol ST05 is removed." + Environment.NewLine +
                                "3. All buttons are enabled." + Environment.NewLine +
                                "4. ‘Close’ button NA11 is displayed enabled in area G." + Environment.NewLine +
                                "5. The first Input Field is selected" + Environment.NewLine +
                                "6. All other Input Fields are not selected");

            /*
            Test Step 5
            Action: Perform the following procedure;Press ‘close’ button (SR speed/distance window) Press ‘close’ button (Special window)
            Expected Result: DMI displays default window
            */
            DmiActions.ShowInstruction(this,
                @"Press ‘Close’ button in SR speed/distance window. Press ‘Close’ button in Special  window.");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays default window.");

            /*
            Test Step 6
            Action: Repeat action step 2 with default window
            Expected Result: Verify the following information;(1)   The Main, Override, Data view, Spec and Setting buttons are always enabled
            Test Step Comment: (1) Note under the MMI_gen 5728;
            */
            DmiActions.Finished_SoM_Default_Window(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The Main, Override, Data view, Spec and Setting buttons are always enabled.");

            EVC8_MMIDriverMessage.MMI_Q_TEXT = 716;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.Send();


            this.Wait_Realtime(10000);

            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 4;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The Main, Override, Data view, Spec and Setting buttons are always enabled.");

            /*
            Test Step 7
            Action: Drive the train forward passing BG1
            Expected Result: 
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 5;
            //????? More required?

            /*
            Test Step 8
            Action: Stop the train and press the ‘Spec’ button
            Expected Result: DMI displays Special window with enabled Adhesion button
            */
            // Call generic Check Results Method
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 0;
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Special; // Special window
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.Adhesion;
            EVC30_MMIRequestEnable.Send();

            WaitForVerification(@"Press the ‘Spec’ button and check the following:" + Environment.NewLine +
                                Environment.NewLine +
                                "1. DMI displays the Special window with the Adhesion button enabled.");

            /*
            Test Step 9
            Action: Press the ‘Adhesion’ button
            Expected Result: Verify the following information;(1)   Verify DMI still displays Special window until Adhesion window is displayed.(2)   Verify the close button is always enabled
            Test Step Comment: (1) MMI_gen 8859 (partly: windows in special menu);(2) MMI_gen 5646 (partly: always enable, windows in special menu);
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Adhesion’ button in the Special window.");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays Special window until Adhesion window is displayed." +
                                Environment.NewLine +
                                "2. ‘Close’ button is always enabled.");

            /*
            Test Step 10
            Action: Repeat action step 2 with Adhesion window
            Expected Result: See the expectation in step 2 and also verify the following information;DMI in the entry state of ‘ST05’(1)   The Input Field is deselected.10 seconds laterDMI in the exit state of ‘ST05’(2)   The input field is in the ‘Selected’ state
            Test Step Comment: See step 2 for the Adhesion window in the special menu(1) MMI_gen 168 (partly: deselect input field);(2) MMI_gen 5728 (partly: input field);
            */
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI is in the entry state of ‘ST05’." + Environment.NewLine +
                                "2. The hourglass symbol ST05 is displayed." + Environment.NewLine +
                                "3. All buttons and the ‘Close’ button are disabled." + Environment.NewLine +
                                "4. ‘Close’ button NA12 is displayed disabled in area G." + Environment.NewLine +
                                "5. The Input Field is not selected");

            this.Wait_Realtime(10000);

            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 4;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI is in the exit state of ‘ST05’." + Environment.NewLine +
                                "2. The hourglass symbol ST05 is removed." + Environment.NewLine +
                                "3. All buttons are enabled." + Environment.NewLine +
                                "4. ‘Close’ button NA11 is displayed enabled in area G." + Environment.NewLine +
                                "5. The Input Field is selected");

            /*
            Test Step 11
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }

        #region Send_XML_10_5_a_DMI_Test_Specification

        private void XML_10_5_a()
        {
            // Step 2/1
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 716;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;

            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI is in the entry state of ‘ST05’." + Environment.NewLine +
                                "2. The hourglass symbol ST05 is displayed." + Environment.NewLine +
                                "3. All buttons and the ‘Close’ button are disabled." + Environment.NewLine +
                                "4. ‘Close’ button NA12 is displayed disabled in area G.");

            Wait_Realtime(10000);

            // Step 2/2
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 4;

            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI is in the exit state of ‘ST05’." + Environment.NewLine +
                                "2. The hourglass symbol ST05 is removed." + Environment.NewLine +
                                "3. All buttons are enabled." + Environment.NewLine +
                                "4. ‘Close’ button NA11 is displayed enabled in area G.");
        }

        #endregion
    }
}
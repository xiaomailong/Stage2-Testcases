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
    /// 15.2.6 State 'ST05': Settings window and windows in setting menu
    /// TC-ID: 10.2.6
    /// 
    /// This test case verifies the buttons of Settings window and windows in Setting menu when entry and exit on state of 'ST05'.
    /// 
    /// Tested Requirements:
    /// MMI_gen 12018 (partly: windows in setting menu); MMI_gen 168 (partly: deselect input field, disabled buttons, the settings window and windows in setting menu); MMI_gen 4395 (partly: close button, disabled, the settings window and windows in setting menu); MMI_gen 4396 (partly: close, NA11, NA12, the settings window and windows in setting menu); MMI_gen 5646 (partly: always enable, State ‘ST05’ button is disabled, the settings window and windows in setting menu); MMI_gen 5719 (partly: always enable, State 'ST05' button is disabled, the settings window and windows in setting menu); MMI_gen 5728 (partly: input field, removal, EVC, restore after ST05, the settings window and windows in setting menu); MMI_gen 8859 (partly: the settings window and windows in setting menu); Note under the MMI_gen 5728;
    /// 
    /// Scenario:
    /// 1.The ‘Setting’ menu window is displayed. 
    /// 2.Open the ‘Maintenance password’ window and use the test script files to send packets in order to verify state ‘ST05’.
    /// 3.Open the ‘Maintenance’ window and use the test script files to send packets in order to verify state ‘ST05’. 
    /// 4.Open the ‘Wheel diameter’ window and use the test script files to send packets in order to verify state ‘ST05’.
    /// 5.Open the ‘Validate wheel diameter’ window and use the test script files to send packets in order to verify state ‘ST05’.
    /// 6.Open the ‘Radar’ window and use the test script files to send packets in order to verify state ‘ST05’. 
    /// 7.Open the ‘Validate radar’ window and use the test script files to send packets in order to verify state ‘ST05’.
    /// 8.SoM is performed in SR mode, Level 1.
    /// 9.Open the ‘Setting’ window and use the test script files to send packets in order to verify state ‘ST05’. 
    /// 10.Open the ‘Language’ window and use the test script files to send packets in order to verify state ‘ST05’. 
    /// 11.Open the ‘Volume’ window and use the test script files to send packets in order to verify state ‘ST05’. 
    /// 12.Open the ‘Brightness’ window and use the test script files to send packets in order to verify state ‘ST05’. 
    /// 13.Open the ‘System Version’ window and use the test script files to send packets in order to verify state ‘ST05’. 
    /// 14.Open the ‘Set VBC’ window and use the test script files to send packets in order to verify state ‘ST05’. 
    /// 15.Open the ‘Validate set VBC’ window and use the test script files to send packets in order to verify state ‘ST05’. 
    /// 16.Open the ‘Remove VBC’ window and use the test script files to send packets in order to verify state ‘ST05’. 
    /// 17.Open the Validate remove VBC’ window and use the test script files to send packets in order to verify state ‘ST05’. 
    /// 18.Open the ‘Set Clock’ window and use the test script files to send packets in order to verify state ‘ST05’. 
    /// 19.Open the ‘Brake’ window and use the test script files to send packets in order to verify state ‘ST05’. 
    /// 20.Open the ‘Brake test’ window and use the test script files to send packets in order to verify state ‘ST05’. 
    /// 21.Open the ‘Brake percentage’ window and use the test script files to send packets in order to verify state ‘ST05’. 
    /// 22.Open the ‘Validate brake percentage’ window and use the test script files to send packets in order to verify state ‘ST05’. 
    /// 23.Open the ‘System Info’ window and use the test script files to send packets in order to verify state ‘ST05’.
    /// 
    /// Used files:
    /// 10_2_6_a.xml
    /// </summary>
    public class TC_ID_10_2_6_State_ST05 : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // The ‘ATC-2’ level is configured in ATP-CU.NID_NTC_Installed = 22, PB_SAFETY_LEVEL = 2, NTC_HW_ADDR = 92, NID_NTC_Default = 22 (M_InstalledLevels and M_DefaultLevels have to be updated according to the number of enabling NTC/STM levels, by bitmasks)Test system is powered on with STM ATC-2 is started in ‘CO’ stateCabin is active

            // Call the TestCaseBase PreExecution
            base.PreExecution();
            DmiActions.Complete_SoM_L1_SR(this);
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in SR mode
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in SR mode, Level 1.");

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint

            /*
            Test Step 1
            Action: Perform the following procedure;Press ‘Setting’ buttonPress ‘Maintenance’ button
            Expected Result: Verify the following information;(1)   Verify DMI still displays Setting window until Maintenance password window is displayed.(2)   Verify the close button is always enable
            Test Step Comment: (1) MMI_gen 8859 (partly: windows in setting menu);(2) MMI_gen 5646 (partly: always enable, windows in setting menu);
            */
            // Enable all buttons and (test) system info
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.Start | Variables.standardFlags;
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_LOW = true;
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = 0;
            EVC30_MMIRequestEnable.Send();

            DmiActions.ShowInstruction(this, @"Press the ‘Settings’ button. Press the ‘Maintenance’ button.");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Settings window until the Maintenance password window is displayed." + Environment.NewLine +
                                "2. The ‘Close’ button is always enabled.");

            /*
            Test Step 2
            Action: Use the test script file 10_2_6_a.xml to disable and enable button of the password window via EVC-8 withPacket 1 (Entry state of ‘ST05’)MMI_Q_TEXT_CRITERIA = 3 MMI_Q_TEXT = 716Packet 2 (Exit state of ‘ST05’)MMI_Q_TEXT_CRITERIA = 4MMI_Q_TEXT = 716Note: Stopwatch is required for accuracy of test result
            Expected Result: Verify the following information;DMI in the entry state of ‘ST05’(1)   The hourglass symbol ST05 is displayed.(2)   Verify all buttons and the close button is disable.(3)   The disabled Close button NA12 is display in area G.(4)   The Input Field is deselected.10 seconds laterDMI in the exit state of ‘ST05’(5)   The hourglass symbol ST05 is removed.(6)   The state of all buttons is restored according to the last status before script is sent.(7)   The enabled Close button NA11 is display in area G.(8)   The input field is in the ‘Selected’ state
            Test Step Comment: (1) MMI_gen 12018 (partly: windows in setting menu);(2) MMI_gen 168 (partly: disabled buttons, windows in setting menu); MMI_gen 5646 (partly: State ‘ST05’ button is disabled, windows in setting menu); MMI_gen 4395 (partly: close button, disabled, windows in setting menu);(3) MMI_gen 4396 (partly: close, NA12, windows in setting menu);(4) MMI_gen 168 (partly: deselect input field, windows in setting menu);(5) MMI_gen 5728 (partly: removal, EVC, windows in setting menu);(6) MMI_gen 5728 (partly: restore after ST05, windows in setting menu);(7) MMI_gen 4396 (partly: close, NA11, windows in setting menu);(8) MMI_gen 5728 (partly: input field, windows in setting menu);
            */
            XML_10_2_6_a();

            /*
            Test Step 3
            Action: Enter and confirm the password
            Expected Result: Verify the following information;(1)   Verify DMI still displays Maintenance password window until Maintenance window is displayed.(2)   Verify the close button is always enable
            Test Step Comment: (1) MMI_gen 8859 (partly: windows in setting menu);(2) MMI_gen 5646 (partly: always enable, windows in setting menu);
            */
            DmiActions.ShowInstruction(this, "Enter and accept the password.");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Maintenance password window until the  Maintenance window is displayed." + Environment.NewLine +
                                "2. The ‘Close’ button is always enabled.");

            /*
            Test Step 4
            Action: Repeat action step 2 with Maintenance window
            Expected Result: Verify the following information;DMI in the entry state of ‘ST05’(1)   The hourglass symbol ST05 is displayed.(2)   Verify all buttons and the close button is disable.(3)   The disabled Close button NA12 is display in area G.10 seconds laterDMI in the exit state of ‘ST05’(4)   The hourglass symbol ST05 is removed.(5)   The state of all buttons is restored according to the last status before script is sent.(6)   The enabled Close button NA11 is display in area G
            Test Step Comment: (1) MMI_gen 12018 (partly: windows in setting menu);(2) MMI_gen 168 (partly: disabled buttons, windows in setting menu); MMI_gen 5646 (partly: State ‘ST05’ button is disabled, windows in setting menu); MMI_gen 4395 (partly: close button, disabled, windows in setting menu);(3) MMI_gen 4396 (partly: close, NA12, windows in setting menu);(4) MMI_gen 5728 (partly: removal, EVC, windows in setting menu);(5) MMI_gen 5728 (partly: restore after ST05, windows in setting menu);(6) MMI_gen 4396 (partly: close, NA11, windows in setting menu);
            */
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 716;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI is in the entry state of ‘ST05’." + Environment.NewLine +
                                "2. The hourglass symbol ST05 is displayed." + Environment.NewLine +
                                "3. All buttons and the ‘Close’ button are disabled." + Environment.NewLine +
                                "4. The ‘Close’ button NA12 is displayed disabled in area G.");

            Wait_Realtime(10000);
            
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 4;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI is in the exit state of ‘ST05’." + Environment.NewLine +
                                "2. The hourglass symbol ST05 is removed." + Environment.NewLine +
                                "3. All buttons are enabled." + Environment.NewLine +
                                "4. The ‘Close’ button NA11 is displayed enabled in area G.");

            /*
            Test Step 5
            Action: Press ‘Wheel diameter’ button
            Expected Result: Verify the following information;(1)   Verify DMI still displays Maintenance window until Wheel diameter window is displayed.(2)   Verify the close button is always enable
            Test Step Comment: (1) MMI_gen 8859 (partly: windows in setting menu);(2) MMI_gen 5646 (partly: always enable, windows in setting menu);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press the ‘Wheel diameter’ button");

            EVC40_MMICurrentMaintenanceData.MMI_Q_MD_DATASET = Variables.MMI_Q_MD_DATASET.WheelDiameter;
            EVC40_MMICurrentMaintenanceData.MMI_M_SDU_WHEEL_SIZE_1 = (Variables.MMI_M_SDU_WHEEL_SIZE)500;
            EVC40_MMICurrentMaintenanceData.MMI_M_SDU_WHEEL_SIZE_2 = (Variables.MMI_M_SDU_WHEEL_SIZE)500;
            EVC40_MMICurrentMaintenanceData.MMI_M_WHEEL_SIZE_ERR = (Variables.MMI_M_WHEEL_SIZE_ERR)10;
            EVC40_MMICurrentMaintenanceData.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Maintenance password window until the Wheel diameter window is displayed." + Environment.NewLine +
                                "2. The ‘Close’ button is always enabled.");

            /*
            Test Step 6
            Action: Repeat action step 2 with Wheel diameter window
            Expected Result: Verify the following information;DMI in the entry state of ‘ST05’(1)   The hourglass symbol ST05 is displayed.(2)   Verify all buttons and the close button is disable.(3)   The disabled Close button NA12 is display in area G.(4)   All Input Field are deselected.10 seconds laterDMI in the exit state of ‘ST05’(5)    The hourglass symbol ST05 is removed.(6)    The state of all buttons is restored according to the last status before script is sent.(7)    The enabled Close button NA11 is display in area G.(8)   The input field is stated as follows:The first input field is in the ‘Selected’ state.The all others are in the ‘Not selected’ state
            Test Step Comment: (1) MMI_gen 12018 (partly: windows in setting menu);(2) MMI_gen 168 (partly: disabled buttons, windows in setting menu); MMI_gen 5646 (partly: State ‘ST05’ button is disabled, windows in setting menu); MMI_gen 4395 (partly: close button, disabled, windows in setting menu);(3) MMI_gen 4396 (partly: close, NA12, windows in setting menu);(4) MMI_gen 168 (partly: deselect input field, windows in setting menu);(5) MMI_gen 5728 (partly: removal, EVC, windows in setting menu);(6) MMI_gen 5728 (partly: restore after ST05, windows in setting menu);(7) MMI_gen 4396 (partly: close, NA11, windows in setting menu);(8) MMI_gen 5728 (partly: input field, windows in setting menu);
            */
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 716;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI is in the entry state of ‘ST05’." + Environment.NewLine +
                                "2. The hourglass symbol ST05 is displayed." + Environment.NewLine +
                                "3. All buttons and the ‘Close’ button are disabled." + Environment.NewLine +
                                "4. The ‘Close’ button NA12 is displayed disabled in area G." + Environment.NewLine +
                                "5. All Input Fields are not selected.");

            Wait_Realtime(10000);
            
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 4;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI is in the exit state of ‘ST05’." + Environment.NewLine +
                                "2. The hourglass symbol ST05 is removed." + Environment.NewLine +
                                "3. All buttons are enabled." + Environment.NewLine +
                                "4. The ‘Close’ button NA11 is displayed enabled in area G." + Environment.NewLine +                                
                                "5. The first Input Field is selected" + Environment.NewLine +
                                "6. All other Input Fields are not selected.");

            /*
            Test Step 7
            Action: Perform the following procedure;Confirm all value in Wheel diameter window.Press ‘Yes’ button.Press ‘Yes’ button (on keypad)
            Expected Result: Verify the following information;(1)   Verify DMI still displays Wheel diameter window until Validate wheel diameter window is displayed.(2)   Verify the close button is always enable. (3)   Verify the <Yes> button is always enable
            Test Step Comment: (1) MMI_gen 8859 (partly: windows in setting menu);(2) MMI_gen 5646 (partly: always enable, windows in setting menu);(3) MMI_gen 5719 (partly: always enable, windows in setting menu);
            */
            DmiActions.ShowInstruction(this, @"Accept all values in the ‘Wheel diameter’ window, then press the ‘Yes’ button");

            EVC41_MMIEchoedMaintenanceData.MMI_Q_MD_DATASET_ = Variables.MMI_Q_MD_DATASET.WheelDiameter;
            EVC41_MMIEchoedMaintenanceData.MMI_M_SDU_WHEEL_SIZE_1_ = (Variables.MMI_M_SDU_WHEEL_SIZE)500;
            EVC41_MMIEchoedMaintenanceData.MMI_M_SDU_WHEEL_SIZE_2_ = (Variables.MMI_M_SDU_WHEEL_SIZE)500;
            EVC41_MMIEchoedMaintenanceData.MMI_M_WHEEL_SIZE_ERR_ = (Variables.MMI_M_WHEEL_SIZE_ERR)10;
            EVC41_MMIEchoedMaintenanceData.Send();

            DmiActions.ShowInstruction(this, @"Press the ‘Yes’ button on the keypad.");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Wheel diameter window until the Validate wheel diameter window is displayed." + Environment.NewLine +
                                @"2. The ‘Close’ button is always enabled." + Environment.NewLine +
                                @"3. <Yes> button is always enabled.");

            /*
            Test Step 8
            Action: Repeat action step 2 with Validate wheel diameter window
            Expected Result: Verify the following information;DMI in the entry state of ‘ST05’(1)   The hourglass symbol ST05 is displayed.(2)   Verify all buttons and the close button is disable.(3)   The disabled Close button NA12 is display in area G.(4)   The Input Field is deselected.10 seconds laterDMI in the exit state of ‘ST05’(5)   The hourglass symbol ST05 is removed.(6)   The state of all buttons is restored according to the last status before script is sent.(7)   The enabled Close button NA11 is display in area G.(8)   The input field is in the ‘Selected’ state
            Test Step Comment: (1) MMI_gen 12018 (partly: windows in setting menu);(2) MMI_gen 168 (partly: disabled buttons, windows in setting menu); MMI_gen 5646 (partly: State ‘ST05’ button is disabled, windows in setting menu); MMI_gen 5719 (partly: State 'ST05' button is disabled, windows in setting menu); MMI_gen 4395 (partly: close button, disabled, windows in setting menu);(3) MMI_gen 4396 (partly: close, NA12, windows in setting menu);(4) MMI_gen 168 (partly: deselect input field, windows in setting menu);(5) MMI_gen 5728 (partly: removal, EVC, windows in setting menu);(6) MMI_gen 5728 (partly: restore after ST05, windows in setting menu);(7) MMI_gen 4396 (partly: close, NA11, windows in setting menu);(8) MMI_gen 5728 (partly: input field, windows in setting menu);
            */
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 716;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI is in the entry state of ‘ST05’." + Environment.NewLine +
                                "2. The hourglass symbol ST05 is displayed." + Environment.NewLine +
                                "3. All buttons and the ‘Close’ button are disabled." + Environment.NewLine +
                                "4. The ‘Close’ button NA12 is displayed disabled in area G." + Environment.NewLine +
                                "5. The Input Fields is not selected.");

            Wait_Realtime(10000);

            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 4;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI is in the exit state of ‘ST05’." + Environment.NewLine +
                                "2. The hourglass symbol ST05 is removed." + Environment.NewLine +
                                "3. All buttons are enabled." + Environment.NewLine +
                                "4. The ‘Close’ button NA11 is displayed enabled in area G." + Environment.NewLine +
                                "5. The Input Field is selected.");

            /*
            Test Step 9
            Action: Perform the following procedure;Confirm entered data by pressing an input field.Press ‘Radar’ button
            Expected Result: Verify the following information;(1)   Verify DMI still displays Validate wheel diameter window until Radar window is displayed.(2)   Verify the close button is always enable
            Test Step Comment: (1) MMI_gen 8859 (partly: windows in setting menu);(2) MMI_gen 5646 (partly: always enable, windows in setting menu);
            */
            DmiActions.ShowInstruction(this, @"Accept the entered data by pressing an Input Field, then press the ‘Radar’ button");

            EVC40_MMICurrentMaintenanceData.MMI_Q_MD_DATASET = Variables.MMI_Q_MD_DATASET.Doppler;
            EVC40_MMICurrentMaintenanceData.MMI_M_PULSE_PER_KM_1 = (Variables.MMI_M_PULSE_PER_KM)20001;
            EVC40_MMICurrentMaintenanceData.MMI_M_PULSE_PER_KM_2 = (Variables.MMI_M_PULSE_PER_KM)20001;
            EVC40_MMICurrentMaintenanceData.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Validate wheel diameter window until the Radar window is displayed." + Environment.NewLine +
                                @"2. The ‘Close’ button is always enabled." + Environment.NewLine +
                                @"3. <Yes> button is always enabled.");

            /*
            Test Step 10
            Action: Repeat action step 2 with Radar window
            Expected Result: See the expectation in step 2
            Test Step Comment: See step 2 for Radar window in the Settings menu
            */
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 716;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI is in the entry state of ‘ST05’." + Environment.NewLine +
                                "2. The hourglass symbol ST05 is displayed." + Environment.NewLine +
                                "3. All buttons and the ‘Close’ button are disabled." + Environment.NewLine +
                                "4. The ‘Close’ button NA12 is displayed disabled in area G." + Environment.NewLine +
                                "5. The Input Fields is not selected.");

            Wait_Realtime(10000);

            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 4;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI is in the exit state of ‘ST05’." + Environment.NewLine +
                                "2. The hourglass symbol ST05 is removed." + Environment.NewLine +
                                "3. All buttons are enabled." + Environment.NewLine +
                                "4. The ‘Close’ button NA11 is displayed enabled in area G." + Environment.NewLine +
                                "5. The Input Field is selected.");

            /*
            Test Step 11
            Action: Perform the following procedure;Confirm value in Radar window.Press ‘Yes’ button.Press ‘Yes’ button (on keypad)
            Expected Result: Verify the following information;(1)   Verify DMI still displays Radar window until Validate Radar window is displayed.(2)   Verify the close button is always enable. (3)   Verify the <Yes> button is always enable
            Test Step Comment: (1) MMI_gen 8859 (partly: windows in setting menu);(2) MMI_gen 5646 (partly: always enable, windows in setting menu);(3) MMI_gen 5719 (partly: always enable, windows in setting menu);
            */
            DmiActions.ShowInstruction(this, @"Accept the value in ‘Radar’ window, then press the ‘Yes’ button");

            EVC41_MMIEchoedMaintenanceData.MMI_Q_MD_DATASET_ = Variables.MMI_Q_MD_DATASET.Doppler;
            EVC41_MMIEchoedMaintenanceData.MMI_M_PULSE_PER_KM_1_ = (Variables.MMI_M_PULSE_PER_KM)20001;
            EVC41_MMIEchoedMaintenanceData.MMI_M_PULSE_PER_KM_2_ = (Variables.MMI_M_PULSE_PER_KM)20001;
            EVC41_MMIEchoedMaintenanceData.Send();
            
            DmiActions.ShowInstruction(this, @"Press the ‘Yes’ button on the keypad.");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Radar window until the Validate Radar window is displayed." + Environment.NewLine +
                                @"2. The ‘Close’ button is always enabled." + Environment.NewLine +
                                @"3. The <Yes> key is always enabled.");

            /*
            Test Step 12
            Action: Repeat action step 2 with Validate Radar window
            Expected Result: See the expectation in step 8
            Test Step Comment: See step 8 for Validate Radar window in the Settings menu
            */
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 716;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI is in the entry state of ‘ST05’." + Environment.NewLine +
                                "2. The hourglass symbol ST05 is displayed." + Environment.NewLine +
                                "3. All buttons and the ‘Close’ button are disabled." + Environment.NewLine +
                                "4. The ‘Close’ button NA12 is displayed disabled in area G." + Environment.NewLine +
                                "5. The Input Field is not selected.");

            Wait_Realtime(10000);

            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 4;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI is in the exit state of ‘ST05’." + Environment.NewLine +
                                "2. The hourglass symbol ST05 is removed." + Environment.NewLine +
                                "3. All buttons are enabled." + Environment.NewLine +
                                "4. The ‘Close’ button NA11 is displayed enabled in area G." + Environment.NewLine +
                                "5. The Input Field is selected.");

            /*
            Test Step 13
            Action: Perform the following procedure;Confirm entered data by pressing an input field.Press ‘close’ button (Maintenance window) Press ‘close’ button (Settings window)SoM is performed in SR mode, Level 1.Note: in NTC data entry windows, enter and confirm the input field data follow echo text data
            Expected Result: DMI displays in SR mode, level 1
            */
            // Call generic Check Results Method
            DmiActions.ShowInstruction(this, @"Accept the entered data by pressing an Input Field. Press the ‘Close’ button in the Maintenance window." + Environment.NewLine +
                                             @"Press the ‘Close’ button in the Settings window");
            DmiActions.Complete_SoM_L1_SR(this);

            DmiExpectedResults.SR_Mode_displayed(this);

            /*
            Test Step 14
            Action: Press ‘Setting’ button
            Expected Result: Verify the following information;(1)   Verify DMI still displays Default window until Setting window is displayed.(2)   Verify the close button is always enable
            Test Step Comment: (1) MMI_gen 8859 (partly: settings window);(2) MMI_gen 5646 (partly: always enable, settings window);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press the ‘Settings’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays Default window until Setting window is displayed." + Environment.NewLine +
                                @"2. The ‘Close’ button is always enabled.");

            /*
            Test Step 15
            Action: Repeat action step 2 with Setting window
            Expected Result: Verify the following information;DMI in the entry state of ‘ST05’(1)    The hourglass symbol ST05 is displayed.(2)    Verify all buttons and the close button is disable. (except ‘Lock screen for cleaning’ button)(3)    The disabled Close button NA12 is display in area G.10 seconds laterDMI in the exit state of ‘ST05’(4)   The hourglass symbol ST05 is removed.(5)   The state of all buttons is restored according to the last status before script is sent.(6)   The enabled Close button NA11 is display in area G
            Test Step Comment: (1) MMI_gen 12018 (partly: windows in setting menu);(2) MMI_gen 168 (partly: disabled buttons, windows in setting menu); MMI_gen 5646 (partly: State ‘ST05’ button is disabled, windows in setting menu); MMI_gen 4395 (partly: close button, disabled, windows in setting menu); Note under the MMI_gen 5728;(3) MMI_gen 4396 (partly: close, NA12, windows in setting menu);(4) MMI_gen 5728 (partly: removal, EVC, windows in setting menu);(5) MMI_gen 5728 (partly: restore after ST05, windows in setting menu);(6) MMI_gen 4396 (partly: close, NA11, windows in setting menu);
            */
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 716;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI is in the entry state of ‘ST05’." + Environment.NewLine +
                                "2. The hourglass symbol ST05 is displayed." + Environment.NewLine +
                                "3. All buttons and the ‘Close’ button are disabled." + Environment.NewLine +
                                "4. The ‘Close’ button NA12 is displayed disabled in area G.");

            Wait_Realtime(10000);

            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 4;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI is in the exit state of ‘ST05’." + Environment.NewLine +
                                "2. The hourglass symbol ST05 is removed." + Environment.NewLine +
                                "3. All buttons are enabled." + Environment.NewLine +
                                "4. The ‘Close’ button NA11 is displayed enabled in area G.");

            /*
            Test Step 16
            Action: Press ‘Language’ button
            Expected Result: Verify the following information;(1)   Verify DMI still displays Setting window until Language window is displayed.(2)   Verify the close button is always enable
            Test Step Comment: (1) MMI_gen 8859 (partly: windows in setting menu);(2) MMI_gen 5646 (partly: always enable, windows in setting menu);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press the ‘Language’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Settings window until the Language window is displayed." + Environment.NewLine +
                                @"2. The ‘Close’ button is always enabled.");

            /*
            Test Step 17
            Action: Repeat action step 2 with Language window
            Expected Result: See the expectation in step 2
            Test Step Comment: See step 2 for Language window in the Settings menu
            */
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 716;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI is in the entry state of ‘ST05’." + Environment.NewLine +
                                "2. The hourglass symbol ST05 is displayed." + Environment.NewLine +
                                "3. All buttons and the ‘Close’ button are disabled." + Environment.NewLine +
                                "4. The ‘Close’ button NA12 is displayed disabled in area G." + Environment.NewLine +
                                "5. The Input Field is not selected.");

            Wait_Realtime(10000);
            
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 4;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI is in the exit state of ‘ST05’." + Environment.NewLine +
                                "2. The hourglass symbol ST05 is removed." + Environment.NewLine +
                                "3. All buttons are enabled." + Environment.NewLine +
                                "4. The ‘Close’ button NA11 is displayed enabled in area G." + Environment.NewLine +
                                "5. The Input Field is selected.");

            /*
            Test Step 18
            Action: Perform the following procedure;Press ‘close’ button (Language window) Press ‘Volume’ button
            Expected Result: Verify the following information;(1)   Verify DMI still displays Setting window until Volume window is displayed.(2)   Verify the close button is always enable
            Test Step Comment: (1) MMI_gen 8859 (partly: windows in setting menu);(2) MMI_gen 5646 (partly: always enable, windows in setting menu);
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button in the Language window. Press the ‘Volume’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Settings window until the Volume window is displayed." + Environment.NewLine +
                                @"2. The ‘Close’ button is always enabled.");

            /*
            Test Step 19
            Action: Repeat action step 2 with Volume window
            Expected Result: See the expectation in step 2
            Test Step Comment: See step 2 for Volume window in the Settings menu
            */
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 716;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI is in the entry state of ‘ST05’." + Environment.NewLine +
                                "2. The hourglass symbol ST05 is displayed." + Environment.NewLine +
                                "3. All buttons and the ‘Close’ button are disabled." + Environment.NewLine +
                                "4. The ‘Close’ button NA12 is displayed disabled in area G." + Environment.NewLine +
                                "5. The Input Field is not selected.");

            Wait_Realtime(10000);

            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 4;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI is in the exit state of ‘ST05’." + Environment.NewLine +
                                "2. The hourglass symbol ST05 is removed." + Environment.NewLine +
                                "3. All buttons are enabled." + Environment.NewLine +
                                "4. The ‘Close’ button NA11 is displayed enabled in area G." + Environment.NewLine +
                                "5. The Input Field is selected.");

            /*
            Test Step 20
            Action: Perform the following procedure;Press ‘close’ button (Volume window) Press ‘Brightness’ button
            Expected Result: Verify the following information;(1)   Verify DMI still displays Setting window until Brightness window is displayed.(2)   Verify the close button is always enable
            Test Step Comment: (1) MMI_gen 8859 (partly: windows in setting menu);(2) MMI_gen 5646 (partly: always enable, windows in setting menu);
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button in the Volume window. Press the ‘Brightness’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Settings window until the Brightness window is displayed." + Environment.NewLine +
                                @"2. The ‘Close’ button is always enabled.");

            /*
            Test Step 21
            Action: Repeat action step 2 with Brightness window
            Expected Result: See the expectation in step 2
            Test Step Comment: See step 2 for Brightness window in the Settings menu
            */
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 716;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI is in the entry state of ‘ST05’." + Environment.NewLine +
                                "2. The hourglass symbol ST05 is displayed." + Environment.NewLine +
                                "3. All buttons and the ‘Close’ button are disabled." + Environment.NewLine +
                                "4. The ‘Close’ button NA12 is displayed disabled in area G." + Environment.NewLine +
                                "5. The Input Field is not selected.");

            Wait_Realtime(10000);

            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 4;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI is in the exit state of ‘ST05’." + Environment.NewLine +
                                "2. The hourglass symbol ST05 is removed." + Environment.NewLine +
                                "3. All buttons are enabled." + Environment.NewLine +
                                "4. The ‘Close’ button NA11 is displayed enabled in area G." + Environment.NewLine +
                                "5. The Input Field is selected.");

            /*
            Test Step 22
            Action: Perform the following procedure;Press ‘close’ button (Brightness window) Press ‘System Version’ button
            Expected Result: Verify the following information;(1)   Verify DMI still displays Setting window until System Version window is displayed.(2)   Verify the close button is always enable
            Test Step Comment: (1) MMI_gen 8859 (partly: windows in setting menu);(2) MMI_gen 5646 (partly: always enable, windows in setting menu);
            */
            DmiActions.ShowInstruction(this, @"Press the  ‘Close’ button in the Brightness window. Press the ‘System Version’ button");

            EVC34_MMISystemVersion.SYSTEM_VERSION_X = 255;
            EVC34_MMISystemVersion.SYSTEM_VERSION_Y = 255;
            EVC34_MMISystemVersion.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Brightness window until the System Version window is displayed." + Environment.NewLine +
                                @"2. The ‘Close’ button is always enabled.");

            /*
            Test Step 23
            Action: Repeat action step 2 with System Version window
            Expected Result: Verify the following information;DMI in the entry state of ‘ST05’(1)   The hourglass symbol ST05 is displayed.(2)   The disabled Close button NA12 is display in area G.10 seconds laterDMI in the exit state of ‘ST05’(3)   The hourglass symbol ST05 is removed.(4)   The enabled Close button NA11 is display in area G
            Test Step Comment: (1) MMI_gen 12018 (partly: windows in setting menu);(3) MMI_gen 4396 (partly: close, NA12, windows in setting menu); MMI_gen 5646 (partly: State ‘ST05’ button is disabled, windows in setting menu); MMI_gen 4395 (partly: close button, disabled, windows in setting menu);(5) MMI_gen 5728 (partly: removal, EVC, windows in setting menu);(7) MMI_gen 4396 (partly: close, NA11, windows in setting menu);
            */
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 716;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI is in the entry state of ‘ST05’." + Environment.NewLine +
                                "2. The hourglass symbol ST05 is displayed." + Environment.NewLine +
                                "3. The ‘Close’ button NA12 is displayed disabled in area G.");

            Wait_Realtime(10000);

            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 4;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI is in the exit state of ‘ST05’." + Environment.NewLine +
                                "2. The hourglass symbol ST05 is removed." + Environment.NewLine +
                                "3. The ‘Close’ button NA11 is displayed enabled in area G.");

            /*
            Test Step 24
            Action: Perform the following procedure;Press ‘close’ button (System Version window) Press ‘Set VBC’ button
            Expected Result: Verify the following information;(1)   Verify DMI still displays Setting window until Set VBC window is displayed.(2)   Verify the close button is always enable
            Test Step Comment: (1) MMI_gen 8859 (partly: windows in setting menu);(2) MMI_gen 5646 (partly: always enable, windows in setting menu);
            */
            DmiActions.ShowInstruction(this, @"Press ‘Close’ button in the System Version window. Press ‘Set VBC’ button");

            EVC18_MMISetVBC.MMI_M_BUTTONS = Variables.MMI_M_BUTTONS_VBC.BTN_YES_DATA_ENTRY_COMPLETE;
            EVC18_MMISetVBC.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Settings window until the Set VBC window is displayed." + Environment.NewLine +
                                @"2. The ‘Close’ button is always enabled.");

            /*
            Test Step 25
            Action: Repeat action step 2 with Set VBC window
            Expected Result: See the expectation in step 2
            Test Step Comment: See step 2 for Set VBC window in the Settings menu
            */
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 716;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI is in the entry state of ‘ST05’." + Environment.NewLine +
                                "2. The hourglass symbol ST05 is displayed." + Environment.NewLine +
                                "3. All buttons and the ‘Close’ button are disabled." + Environment.NewLine +
                                "4. The ‘Close’ button NA12 is displayed disabled in area G." + Environment.NewLine +
                                "5. The Input Field is not selected.");

            Wait_Realtime(10000);

            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 4;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI is in the exit state of ‘ST05’." + Environment.NewLine +
                                "2. The hourglass symbol ST05 is removed." + Environment.NewLine +
                                "3. All buttons are enabled." + Environment.NewLine +
                                "4. The ‘Close’ button NA11 is displayed enabled in area G." + Environment.NewLine +
                                "5. The Input Field is selected.");

            /*
            Test Step 26
            Action: Perform the following procedure;Enter VBC Code ‘65536’Confirm entered data by pressing an input field.Press ‘Yes’ button.Press ‘Yes’ button (on keypad)
            Expected Result: Verify the following information;(1)   Verify DMI still displays Set VBC window until Validate Set VBC window is displayed.(2)   Verify the close button is always enable. (3)   Verify the <Yes> button is always enable
            Test Step Comment: (1) MMI_gen 8859 (partly: windows in setting menu);(2) MMI_gen 5646 (partly: always enable, windows in setting menu);(3) MMI_gen 5719 (partly: always enable, windows in setting menu);
            */
            DmiActions.ShowInstruction(this, @"Enter VBC Code ‘65536’ and accept entered data by pressing an input field. Press ‘Yes’ button. Press ‘Yes’ button on keypad");

            EVC28_MMIEchoedSetVBCData.MMI_M_VBC_CODE = 65536;
            EVC28_MMIEchoedSetVBCData.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays Set VBC window until Validate Set VBC window is displayed." + Environment.NewLine +
                                @"2. The ‘Close’ button is always enabled." + Environment.NewLine +
                                @"3. <Yes> button is always enabled.");

            /*
            Test Step 27
            Action: Repeat action step 2 with Validate set VBC window
            Expected Result: See the expectation in step 8
            Test Step Comment: See step 8 for Validate set VBC window in the Settings menu
            */
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 716;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI is in the entry state of ‘ST05’." + Environment.NewLine +
                                "2. The hourglass symbol ST05 is displayed." + Environment.NewLine +
                                "3. All buttons and the ‘Close’ button are disabled." + Environment.NewLine +
                                "4. The ‘Close’ button NA12 is displayed disabled in area G." + Environment.NewLine +
                                "5. The Input Fields is not selected.");

            Wait_Realtime(10000);

            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 4;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI is in the exit state of ‘ST05’." + Environment.NewLine +
                                "2. The hourglass symbol ST05 is removed." + Environment.NewLine +
                                "3. All buttons are enabled." + Environment.NewLine +
                                "4. The ‘Close’ button NA11 is displayed enabled in area G." + Environment.NewLine +
                                "5. The Input Field is selected.");

            /*
            Test Step 28
            Action: Perform the following procedure;Confirm entered data by pressing an input field.Press ‘Remove VBC’ button
            Expected Result: Verify the following information;(1)   Verify DMI still displays Setting window until Remove VBC window is displayed.(2)   Verify the close button is always enable
            Test Step Comment: (1) MMI_gen 8859 (partly: windows in setting menu);(2) MMI_gen 5646 (partly: always enable, windows in setting menu);
            */
            DmiActions.ShowInstruction(this, @"Accept the entered data by pressing an input field. Press the ‘Remove VBC’ button");

            EVC19_MMIRemoveVBC.MMI_M_BUTTONS = Variables.MMI_M_BUTTONS_VBC.BTN_YES_DATA_ENTRY_COMPLETE;
            EVC19_MMIRemoveVBC.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Settings window until the Remove VBC window is displayed." + Environment.NewLine +
                                @"2. The ‘Close’ button is always enabled.");

            /*
            Test Step 29
            Action: Repeat action step 2 with Remove VBC window
            Expected Result: See the expectation in step 2
            Test Step Comment: See step 2 for Remove VBC window in the Settings menu
            */
            XML_10_2_6_a();

            /*
            Test Step 30
            Action: Perform the following procedure;Enter VBC Code ‘65536’Confirm entered data by pressing an input field.Press ‘Yes’ button.Press ‘Yes’ button (on keypad)
            Expected Result: Verify the following information;(1)   Verify DMI still displays Remove VBC window until Validate Remove VBC window is displayed.(2)   Verify the close button is always enable. (3)   Verify the <Yes> button is always enable
            Test Step Comment: (1) MMI_gen 8859 (partly: windows in setting menu);(2) MMI_gen 5646 (partly: always enable, windows in setting menu);(3) MMI_gen 5719 (partly: always enable, windows in setting menu);
            */
            DmiActions.ShowInstruction(this, @"Enter VBC Code ‘65536’ and accept entered data by pressing an input field. Press ‘Yes’ button. Press ‘Yes’ button on keypad");

            
            EVC29_MMIEchoedRemoveVBCData.MMI_M_VBC_CODE_ = 65536;
            EVC29_MMIEchoedRemoveVBCData.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Remove VBC window until the Validate Remove VBC window is displayed." + Environment.NewLine +
                                @"2. The ‘Close’ button is always enabled." + Environment.NewLine +
                                @"3. <Yes> button is always enabled.");

            /*
            Test Step 31
            Action: Repeat action step 2 with Validate remove VBC window
            Expected Result: See the expectation in step 8
            Test Step Comment: See step 8 for Validate remove VBC window in the Settings menu
            */
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 716;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI is in the entry state of ‘ST05’." + Environment.NewLine +
                                "2. The hourglass symbol ST05 is displayed." + Environment.NewLine +
                                "3. All buttons and the ‘Close’ button are disabled." + Environment.NewLine +
                                "4. The ‘Close’ button NA12 is displayed disabled in area G." + Environment.NewLine +
                                "5. The Input Fields is not selected.");

            Wait_Realtime(10000);

            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 4;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI is in the exit state of ‘ST05’." + Environment.NewLine +
                                "2. The hourglass symbol ST05 is removed." + Environment.NewLine +
                                "3. All buttons are enabled." + Environment.NewLine +
                                "4. The ‘Close’ button NA11 is displayed enabled in area G." + Environment.NewLine +
                                "5. The Input Field is selected.");

            /*
            Test Step 32
            Action: Perform the following procedure;Confirm entered data by pressing an input field.Press ‘Set Clock’ button
            Expected Result: Verify the following information;(1)   Verify DMI still displays Setting window until Set Clock window is displayed.(2)   Verify the close button is always enable
            Test Step Comment: (1) MMI_gen 8859 (partly: windows in setting menu);(2) MMI_gen 5646 (partly: always enable, windows in setting menu);
            */
            DmiActions.ShowInstruction(this, @"Accept entered data by pressing an input field. Press ‘Set Clock’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays Setting window until Set Clock window is displayed." + Environment.NewLine +
                                @"2. The ‘Close’ button is always enabled.");

            /*
            Test Step 33
            Action: Repeat action step 2 with Set Clock window
            Expected Result: Verify the following information;DMI in the entry state of ‘ST05’.(1)   The hourglass symbol ST05 is displayed.(2)   Verify all buttons and the close button is disable. (except ‘Navigator’ button)(3)   The disabled Close button NA12 is display in area G.(4)   All Input Field are deselected.10 seconds laterDMI in the exit state of ‘ST05’.(5)   The hourglass symbol ST05 is removed.(6)   The state of all buttons is restored according to the last status before script is sent.(7)   The enabled Close button NA11 is display in area G.(8)   The input field is stated as follows:The first input field is in the ‘Selected’ state.The all others are in the ‘Not selected’ state
            Test Step Comment: (1) MMI_gen 12018 (partly: windows in setting menu);(2) MMI_gen 168 (partly: disabled buttons, windows in setting menu); MMI_gen 5646 (partly: State ‘ST05’ button is disabled, windows in setting menu); MMI_gen 4395 (partly: close button, disabled, windows in setting menu); Note under the MMI_gen 5728;(3) MMI_gen 4396 (partly: close, NA12, windows in setting menu);(4) MMI_gen 168 (partly: deselect input field, windows in setting menu);(5) MMI_gen 5728 (partly: removal, EVC, windows in setting menu);(6) MMI_gen 5728 (partly: restore after ST05, windows in setting menu);(7) MMI_gen 4396 (partly: close, NA11, windows in setting menu);(8) MMI_gen 5728 (partly: input field, windows in setting menu);
            */
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI is in the entry state of ‘ST05’." + Environment.NewLine +
                                "2. The hourglass symbol ST05 is displayed." + Environment.NewLine +
                                "3. All buttons and the ‘Close’ button are disabled except ‘Navigator’ button." + Environment.NewLine +
                                "4. The ‘Close’ button NA12 is displayed disabled in area G." + Environment.NewLine +
                                "5. All Input Fields are not selected.");

            Wait_Realtime(10000);

            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 4;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI is in the exit state of ‘ST05’." + Environment.NewLine +
                                "2. The hourglass symbol ST05 is removed." + Environment.NewLine +
                                "3. All buttons are enabled." + Environment.NewLine +
                                "4. The ‘Close’ button NA11 is displayed enabled in area G." + Environment.NewLine +
                                "5. The first Input Field is selected" + Environment.NewLine +
                                "6. All other Input Fields are not selected.");

            /*
            Test Step 34
            Action: Perform the following procedure;Press ‘close’ button (Set Clock window) Press ‘Brake’ button
            Expected Result: Verify the following information;(1)   Verify DMI still displays Setting window until Brake window is displayed.(2)   Verify the close button is always enable
            Test Step Comment: (1) MMI_gen 8859 (partly: windows in setting menu);(2) MMI_gen 5646 (partly: always enable, windows in setting menu);
            */
            DmiActions.ShowInstruction(this, @"Press ‘Close’ button in Set Clock Window. Press ‘Brake’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays Setting window until Brake window is displayed." + Environment.NewLine +
                                @"2. The ‘Close’ button is always enabled.");

            /*
            Test Step 35
            Action: Repeat action step 2 with Brake window
            Expected Result: See the expectation in step 4
            Test Step Comment: See step 4 for Brake window in the Settings menu
            */
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 716;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI is in the entry state of ‘ST05’." + Environment.NewLine +
                                "2. The hourglass symbol ST05 is displayed." + Environment.NewLine +
                                "3. All buttons and the ‘Close’ button are disabled." + Environment.NewLine +
                                "4. The ‘Close’ button NA12 is displayed disabled in area G.");

            Wait_Realtime(10000);

            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 4;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI is in the exit state of ‘ST05’." + Environment.NewLine +
                                "2. The hourglass symbol ST05 is removed." + Environment.NewLine +
                                "3. All buttons are enabled." + Environment.NewLine +
                                "4. The ‘Close’ button NA11 is displayed enabled in area G.");

            /*
            Test Step 36
            Action: Press ‘Test’ button
            Expected Result: Verify the following information;(1)   Verify DMI still displays Brake window until Brake test window is displayed.(2)   Verify the close button is always enable
            Test Step Comment: (1) MMI_gen 8859 (partly: windows in setting menu);(2) MMI_gen 5646 (partly: always enable, windows in setting menu);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press ‘Test’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays Brake window until Brake test window is displayed." + Environment.NewLine +
                                @"2. The ‘Close’ button is always enabled.");

            /*
            Test Step 37
            Action: Repeat action step 2 with Brake test window
            Expected Result: See the expectation in step 4
            Test Step Comment: See step 4 for Brake test window in the Settings menu
            */
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 716;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI is in the entry state of ‘ST05’." + Environment.NewLine +
                                "2. The hourglass symbol ST05 is displayed." + Environment.NewLine +
                                "3. All buttons and the ‘Close’ button are disabled." + Environment.NewLine +
                                "4. The ‘Close’ button NA12 is displayed disabled in area G.");

            Wait_Realtime(10000);

            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 4;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI is in the exit state of ‘ST05’." + Environment.NewLine +
                                "2. The hourglass symbol ST05 is removed." + Environment.NewLine +
                                "3. All buttons are enabled." + Environment.NewLine +
                                "4. The ‘Close’ button NA11 is displayed enabled in area G.");

            /*
            Test Step 38
            Action: Perform the following procedure;Press ‘close’ button (Brake test window)Press ‘Percentage’ button
            Expected Result: Verify the following information;(1)   Verify DMI still displays Brake window until Brake percentage window is displayed.(2)   Verify the close button is always enable
            Test Step Comment: (1) MMI_gen 8859 (partly: windows in setting menu);(2) MMI_gen 5646 (partly: always enable, windows in setting menu);
            */
            DmiActions.ShowInstruction(this, @"Press ‘Close’ button in Brake Test Window. Press ‘Percentage’ button");

            EVC50_MMICurrentBrakePercentage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays Brake window until Brake percentage window is displayed." + Environment.NewLine +
                                @"2. The ‘Close’ button is always enabled.");

            /*
            Test Step 39
            Action: Repeat action step 2 with Brake percentage window
            Expected Result: See the expectation in step 2
            Test Step Comment: See step 2 for Brake percentage window in the Settings menu
            */
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 716;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI is in the entry state of ‘ST05’." + Environment.NewLine +
                                "2. The hourglass symbol ST05 is displayed." + Environment.NewLine +
                                "3. All buttons and the ‘Close’ button are disabled." + Environment.NewLine +
                                "4. The ‘Close’ button NA12 is displayed disabled in area G." + Environment.NewLine +
                                "5. The Input Field is not selected.");

            Wait_Realtime(10000);
            
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 4;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI is in the exit state of ‘ST05’." + Environment.NewLine +
                                "2. The hourglass symbol ST05 is removed." + Environment.NewLine +
                                "3. All buttons are enabled." + Environment.NewLine +
                                "4. The ‘Close’ button NA11 is displayed enabled in area G." + Environment.NewLine +
                                "5. The Input Field is selected.");

            /*
            Test Step 40
            Action: Perform the following procedure;Enter brake percentage ‘80’Confirm entered data by pressing an input field.Press ‘Yes’ button (on keypad)
            Expected Result: Verify the following information;(1)   Verify DMI still displays Brake percentage window until Validate Brake percentage window is displayed.(2)   Verify the close button is always enable. (3)   Verify the <Yes> button is always enable
            Test Step Comment: (1) MMI_gen 8859 (partly: windows in setting menu);(2) MMI_gen 5646 (partly: always enable, windows in setting menu);(3) MMI_gen 5719 (partly: always enable, windows in setting menu);
            */
            DmiActions.ShowInstruction(this, @"Enter Brake percentage ‘80’ and accept entered data by pressing an input field. Press ‘Yes’ button on keypad");

            EVC51_MMIEchoedBrakePercentage.MMI_M_BP_CURRENT_ = 80;
            EVC51_MMIEchoedBrakePercentage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays Brake percentage window until Validate Brake percentage window is displayed." + Environment.NewLine +
                                @"2. The ‘Close’ button is always enabled." + Environment.NewLine +
                                @"3. <Yes> button is always enabled.");

            /*
            Test Step 41
            Action: Repeat action step 2 with Validate brake percentage window
            Expected Result: See the expectation in step 8
            Test Step Comment: See step 8 for Validate brake percentage window in the Settings menu
            */
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 716;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI is in the entry state of ‘ST05’." + Environment.NewLine +
                                "2. The hourglass symbol ST05 is displayed." + Environment.NewLine +
                                "3. All buttons and the ‘Close’ button are disabled." + Environment.NewLine +
                                "4. The ‘Close’ button NA12 is displayed disabled in area G." + Environment.NewLine +
                                "5. The Input Fields is not selected.");

            Wait_Realtime(10000);

            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 4;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI is in the exit state of ‘ST05’." + Environment.NewLine +
                                "2. The hourglass symbol ST05 is removed." + Environment.NewLine +
                                "3. All buttons are enabled." + Environment.NewLine +
                                "4. The ‘Close’ button NA11 is displayed enabled in area G." + Environment.NewLine +
                                "5. The Input Field is selected.");

            /*
            Test Step 42
            Action: Perform the following procedure;Confirm entered data by pressing an input field.Press ‘close’ button (Brake window) Press ‘System Info’ button
            Expected Result: Verify the following information;(1)   Verify DMI still displays Setting window until System Info window is displayed.(2)   Verify the close button is always enable
            Test Step Comment: (1) MMI_gen 8859 (partly: windows in setting menu);(2) MMI_gen 5646 (partly: always enable, windows in setting menu);
            */
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = 255;
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_LOW = true;
            EVC30_MMIRequestEnable.Send();

            DmiActions.ShowInstruction(this, @"Accept entered data by pressing an input field. Press ‘Close’ button in Brake window. Press ‘System Info’ button");

            //EVC24_MMISystemInfo.Send(); 

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays Setting window until System Info window is displayed." + Environment.NewLine +
                                @"2. The ‘Close’ button is always enabled.");

            /*
            Test Step 43
            Action: Repeat action step 2 with System Info window
            Expected Result: See the expectation in step 23
            Test Step Comment: See step 23 for System Info window in the Settings menu
            */
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 716;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI is in the entry state of ‘ST05’." + Environment.NewLine +
                                "2. The hourglass symbol ST05 is displayed." + Environment.NewLine +
                                "3. The ‘Close’ button NA12 is displayed disabled in area G.");

            Wait_Realtime(10000);

            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 4;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI is in the exit state of ‘ST05’." + Environment.NewLine +
                                "2. The hourglass symbol ST05 is removed." + Environment.NewLine +
                                "3. The ‘Close’ button NA11 is displayed enabled in area G.");

            /*
            Test Step 44
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
        #region Send_XML_10_2_6_a_DMI_Test_Specification
        private void XML_10_2_6_a()
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
                                "4. ‘Close’ button NA12 is displayed disabled in area G." + Environment.NewLine +
                                "5. The Input Field is not selected.");

            Wait_Realtime(10000);

            // Step 2/2
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 4;

            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI is in the exit state of ‘ST05’." + Environment.NewLine +
                                "2. The hourglass symbol ST05 is removed." + Environment.NewLine +
                                "3. All buttons are enabled." + Environment.NewLine +
                                "4. ‘Close’ button NA11 is displayed enabled in area G." + Environment.NewLine +
                                "5. The Input Field is selected.");
        }
        #endregion

    }
}
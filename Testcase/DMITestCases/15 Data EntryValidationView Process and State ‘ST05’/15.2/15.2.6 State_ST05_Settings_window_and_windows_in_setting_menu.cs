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
    public class State_ST05_Settings_window_and_windows_in_setting_menu : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // The ‘ATC-2’ level is configured in ATP-CU.NID_NTC_Installed = 22, PB_SAFETY_LEVEL = 2, NTC_HW_ADDR = 92, NID_NTC_Default = 22 (M_InstalledLevels and M_DefaultLevels have to be updated according to the number of enabling NTC/STM levels, by bitmasks)Test system is powered on with STM ATC-2 is started in ‘CO’ stateCabin is active

            // Call the TestCaseBase PreExecution
            base.PreExecution();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in SR mode

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


            /*
            Test Step 2
            Action: Use the test script file 10_2_6_a.xml to disable and enable button of the password window via EVC-8 withPacket 1 (Entry state of ‘ST05’)MMI_Q_TEXT_CRITERIA = 3 MMI_Q_TEXT = 716Packet 2 (Exit state of ‘ST05’)MMI_Q_TEXT_CRITERIA = 4MMI_Q_TEXT = 716Note: Stopwatch is required for accuracy of test result
            Expected Result: Verify the following information;DMI in the entry state of ‘ST05’(1)   The hourglass symbol ST05 is displayed.(2)   Verify all buttons and the close button is disable.(3)   The disabled Close button NA12 is display in area G.(4)   The Input Field is deselected.10 seconds laterDMI in the exit state of ‘ST05’(5)   The hourglass symbol ST05 is removed.(6)   The state of all buttons is restored according to the last status before script is sent.(7)   The enabled Close button NA11 is display in area G.(8)   The input field is in the ‘Selected’ state
            Test Step Comment: (1) MMI_gen 12018 (partly: windows in setting menu);(2) MMI_gen 168 (partly: disabled buttons, windows in setting menu); MMI_gen 5646 (partly: State ‘ST05’ button is disabled, windows in setting menu); MMI_gen 4395 (partly: close button, disabled, windows in setting menu);(3) MMI_gen 4396 (partly: close, NA12, windows in setting menu);(4) MMI_gen 168 (partly: deselect input field, windows in setting menu);(5) MMI_gen 5728 (partly: removal, EVC, windows in setting menu);(6) MMI_gen 5728 (partly: restore after ST05, windows in setting menu);(7) MMI_gen 4396 (partly: close, NA11, windows in setting menu);(8) MMI_gen 5728 (partly: input field, windows in setting menu);
            */
            // Call generic Check Results Method
            DmiExpectedResults
                .Verify_the_following_informationDMI_in_the_entry_state_of_ST051_The_hourglass_symbol_ST05_is_displayed_2_Verify_all_buttons_and_the_close_button_is_disable_3_The_disabled_Close_button_NA12_is_display_in_area_G_4_The_Input_Field_is_deselected_10_seconds_laterDMI_in_the_exit_state_of_ST055_The_hourglass_symbol_ST05_is_removed_6_The_state_of_all_buttons_is_restored_according_to_the_last_status_before_script_is_sent_7_The_enabled_Close_button_NA11_is_display_in_area_G_8_The_input_field_is_in_the_Selected_state(this);


            /*
            Test Step 3
            Action: Enter and confirm the password
            Expected Result: Verify the following information;(1)   Verify DMI still displays Maintenance password window until Maintenance window is displayed.(2)   Verify the close button is always enable
            Test Step Comment: (1) MMI_gen 8859 (partly: windows in setting menu);(2) MMI_gen 5646 (partly: always enable, windows in setting menu);
            */


            /*
            Test Step 4
            Action: Repeat action step 2 with Maintenance window
            Expected Result: Verify the following information;DMI in the entry state of ‘ST05’(1)   The hourglass symbol ST05 is displayed.(2)   Verify all buttons and the close button is disable.(3)   The disabled Close button NA12 is display in area G.10 seconds laterDMI in the exit state of ‘ST05’(4)   The hourglass symbol ST05 is removed.(5)   The state of all buttons is restored according to the last status before script is sent.(6)   The enabled Close button NA11 is display in area G
            Test Step Comment: (1) MMI_gen 12018 (partly: windows in setting menu);(2) MMI_gen 168 (partly: disabled buttons, windows in setting menu); MMI_gen 5646 (partly: State ‘ST05’ button is disabled, windows in setting menu); MMI_gen 4395 (partly: close button, disabled, windows in setting menu);(3) MMI_gen 4396 (partly: close, NA12, windows in setting menu);(4) MMI_gen 5728 (partly: removal, EVC, windows in setting menu);(5) MMI_gen 5728 (partly: restore after ST05, windows in setting menu);(6) MMI_gen 4396 (partly: close, NA11, windows in setting menu);
            */
            // Call generic Check Results Method
            DmiExpectedResults
                .Verify_the_following_informationDMI_in_the_entry_state_of_ST051_The_hourglass_symbol_ST05_is_displayed_2_Verify_all_buttons_and_the_close_button_is_disable_3_The_disabled_Close_button_NA12_is_display_in_area_G_10_seconds_laterDMI_in_the_exit_state_of_ST054_The_hourglass_symbol_ST05_is_removed_5_The_state_of_all_buttons_is_restored_according_to_the_last_status_before_script_is_sent_6_The_enabled_Close_button_NA11_is_display_in_area_G(this);


            /*
            Test Step 5
            Action: Press ‘Wheel diameter’ button
            Expected Result: Verify the following information;(1)   Verify DMI still displays Maintenance window until Wheel diameter window is displayed.(2)   Verify the close button is always enable
            Test Step Comment: (1) MMI_gen 8859 (partly: windows in setting menu);(2) MMI_gen 5646 (partly: always enable, windows in setting menu);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press ‘Wheel diameter’ button");


            /*
            Test Step 6
            Action: Repeat action step 2 with Wheel diameter window
            Expected Result: Verify the following information;DMI in the entry state of ‘ST05’(1)   The hourglass symbol ST05 is displayed.(2)   Verify all buttons and the close button is disable.(3)   The disabled Close button NA12 is display in area G.(4)   All Input Field are deselected.10 seconds laterDMI in the exit state of ‘ST05’(5)    The hourglass symbol ST05 is removed.(6)    The state of all buttons is restored according to the last status before script is sent.(7)    The enabled Close button NA11 is display in area G.(8)   The input field is stated as follows:The first input field is in the ‘Selected’ state.The all others are in the ‘Not selected’ state
            Test Step Comment: (1) MMI_gen 12018 (partly: windows in setting menu);(2) MMI_gen 168 (partly: disabled buttons, windows in setting menu); MMI_gen 5646 (partly: State ‘ST05’ button is disabled, windows in setting menu); MMI_gen 4395 (partly: close button, disabled, windows in setting menu);(3) MMI_gen 4396 (partly: close, NA12, windows in setting menu);(4) MMI_gen 168 (partly: deselect input field, windows in setting menu);(5) MMI_gen 5728 (partly: removal, EVC, windows in setting menu);(6) MMI_gen 5728 (partly: restore after ST05, windows in setting menu);(7) MMI_gen 4396 (partly: close, NA11, windows in setting menu);(8) MMI_gen 5728 (partly: input field, windows in setting menu);
            */


            /*
            Test Step 7
            Action: Perform the following procedure;Confirm all value in Wheel diameter window.Press ‘Yes’ button.Press ‘Yes’ button (on keypad)
            Expected Result: Verify the following information;(1)   Verify DMI still displays Wheel diameter window until Validate wheel diameter window is displayed.(2)   Verify the close button is always enable. (3)   Verify the <Yes> button is always enable
            Test Step Comment: (1) MMI_gen 8859 (partly: windows in setting menu);(2) MMI_gen 5646 (partly: always enable, windows in setting menu);(3) MMI_gen 5719 (partly: always enable, windows in setting menu);
            */


            /*
            Test Step 8
            Action: Repeat action step 2 with Validate wheel diameter window
            Expected Result: Verify the following information;DMI in the entry state of ‘ST05’(1)   The hourglass symbol ST05 is displayed.(2)   Verify all buttons and the close button is disable.(3)   The disabled Close button NA12 is display in area G.(4)   The Input Field is deselected.10 seconds laterDMI in the exit state of ‘ST05’(5)   The hourglass symbol ST05 is removed.(6)   The state of all buttons is restored according to the last status before script is sent.(7)   The enabled Close button NA11 is display in area G.(8)   The input field is in the ‘Selected’ state
            Test Step Comment: (1) MMI_gen 12018 (partly: windows in setting menu);(2) MMI_gen 168 (partly: disabled buttons, windows in setting menu); MMI_gen 5646 (partly: State ‘ST05’ button is disabled, windows in setting menu); MMI_gen 5719 (partly: State 'ST05' button is disabled, windows in setting menu); MMI_gen 4395 (partly: close button, disabled, windows in setting menu);(3) MMI_gen 4396 (partly: close, NA12, windows in setting menu);(4) MMI_gen 168 (partly: deselect input field, windows in setting menu);(5) MMI_gen 5728 (partly: removal, EVC, windows in setting menu);(6) MMI_gen 5728 (partly: restore after ST05, windows in setting menu);(7) MMI_gen 4396 (partly: close, NA11, windows in setting menu);(8) MMI_gen 5728 (partly: input field, windows in setting menu);
            */
            // Call generic Check Results Method
            DmiExpectedResults
                .Verify_the_following_informationDMI_in_the_entry_state_of_ST051_The_hourglass_symbol_ST05_is_displayed_2_Verify_all_buttons_and_the_close_button_is_disable_3_The_disabled_Close_button_NA12_is_display_in_area_G_4_The_Input_Field_is_deselected_10_seconds_laterDMI_in_the_exit_state_of_ST055_The_hourglass_symbol_ST05_is_removed_6_The_state_of_all_buttons_is_restored_according_to_the_last_status_before_script_is_sent_7_The_enabled_Close_button_NA11_is_display_in_area_G_8_The_input_field_is_in_the_Selected_state(this);


            /*
            Test Step 9
            Action: Perform the following procedure;Confirm entered data by pressing an input field.Press ‘Radar’ button
            Expected Result: Verify the following information;(1)   Verify DMI still displays Validate wheel diameter window until Radar window is displayed.(2)   Verify the close button is always enable
            Test Step Comment: (1) MMI_gen 8859 (partly: windows in setting menu);(2) MMI_gen 5646 (partly: always enable, windows in setting menu);
            */


            /*
            Test Step 10
            Action: Repeat action step 2 with Radar window
            Expected Result: See the expectation in step 2
            Test Step Comment: See step 2 for Radar window in the Settings menu
            */
            // Call generic Check Results Method
            DmiExpectedResults.See_the_expectation_in_step_2(this);


            /*
            Test Step 11
            Action: Perform the following procedure;Confirm value in Radar window.Press ‘Yes’ button.Press ‘Yes’ button (on keypad)
            Expected Result: Verify the following information;(1)   Verify DMI still displays Radar window until Validate Radar window is displayed.(2)   Verify the close button is always enable. (3)   Verify the <Yes> button is always enable
            Test Step Comment: (1) MMI_gen 8859 (partly: windows in setting menu);(2) MMI_gen 5646 (partly: always enable, windows in setting menu);(3) MMI_gen 5719 (partly: always enable, windows in setting menu);
            */


            /*
            Test Step 12
            Action: Repeat action step 2 with Validate Radar window
            Expected Result: See the expectation in step 8
            Test Step Comment: See step 8 for Validate Radar window in the Settings menu
            */
            // Call generic Check Results Method
            DmiExpectedResults.See_the_expectation_in_step_8(this);


            /*
            Test Step 13
            Action: Perform the following procedure;Confirm entered data by pressing an input field.Press ‘close’ button (Maintenance window) Press ‘close’ button (Settings window)SoM is performed in SR mode, Level 1.Note: in NTC data entry windows, enter and confirm the input field data follow echo text data
            Expected Result: DMI displays in SR mode, level 1
            */
            // Call generic Check Results Method
            DmiExpectedResults.DMI_displays_in_SR_mode_level_1(this);


            /*
            Test Step 14
            Action: Press ‘Setting’ button
            Expected Result: Verify the following information;(1)   Verify DMI still displays Default window until Setting window is displayed.(2)   Verify the close button is always enable
            Test Step Comment: (1) MMI_gen 8859 (partly: settings window);(2) MMI_gen 5646 (partly: always enable, settings window);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press ‘Setting’ button");


            /*
            Test Step 15
            Action: Repeat action step 2 with Setting window
            Expected Result: Verify the following information;DMI in the entry state of ‘ST05’(1)    The hourglass symbol ST05 is displayed.(2)    Verify all buttons and the close button is disable. (except ‘Lock screen for cleaning’ button)(3)    The disabled Close button NA12 is display in area G.10 seconds laterDMI in the exit state of ‘ST05’(4)   The hourglass symbol ST05 is removed.(5)   The state of all buttons is restored according to the last status before script is sent.(6)   The enabled Close button NA11 is display in area G
            Test Step Comment: (1) MMI_gen 12018 (partly: windows in setting menu);(2) MMI_gen 168 (partly: disabled buttons, windows in setting menu); MMI_gen 5646 (partly: State ‘ST05’ button is disabled, windows in setting menu); MMI_gen 4395 (partly: close button, disabled, windows in setting menu); Note under the MMI_gen 5728;(3) MMI_gen 4396 (partly: close, NA12, windows in setting menu);(4) MMI_gen 5728 (partly: removal, EVC, windows in setting menu);(5) MMI_gen 5728 (partly: restore after ST05, windows in setting menu);(6) MMI_gen 4396 (partly: close, NA11, windows in setting menu);
            */


            /*
            Test Step 16
            Action: Press ‘Language’ button
            Expected Result: Verify the following information;(1)   Verify DMI still displays Setting window until Language window is displayed.(2)   Verify the close button is always enable
            Test Step Comment: (1) MMI_gen 8859 (partly: windows in setting menu);(2) MMI_gen 5646 (partly: always enable, windows in setting menu);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press ‘Language’ button");


            /*
            Test Step 17
            Action: Repeat action step 2 with Language window
            Expected Result: See the expectation in step 2
            Test Step Comment: See step 2 for Language window in the Settings menu
            */
            // Call generic Check Results Method
            DmiExpectedResults.See_the_expectation_in_step_2(this);


            /*
            Test Step 18
            Action: Perform the following procedure;Press ‘close’ button (Language window) Press ‘Volume’ button
            Expected Result: Verify the following information;(1)   Verify DMI still displays Setting window until Volume window is displayed.(2)   Verify the close button is always enable
            Test Step Comment: (1) MMI_gen 8859 (partly: windows in setting menu);(2) MMI_gen 5646 (partly: always enable, windows in setting menu);
            */


            /*
            Test Step 19
            Action: Repeat action step 2 with Volume window
            Expected Result: See the expectation in step 2
            Test Step Comment: See step 2 for Volume window in the Settings menu
            */
            // Call generic Check Results Method
            DmiExpectedResults.See_the_expectation_in_step_2(this);


            /*
            Test Step 20
            Action: Perform the following procedure;Press ‘close’ button (Volume window) Press ‘Brightness’ button
            Expected Result: Verify the following information;(1)   Verify DMI still displays Setting window until Brightness window is displayed.(2)   Verify the close button is always enable
            Test Step Comment: (1) MMI_gen 8859 (partly: windows in setting menu);(2) MMI_gen 5646 (partly: always enable, windows in setting menu);
            */


            /*
            Test Step 21
            Action: Repeat action step 2 with Brightness window
            Expected Result: See the expectation in step 2
            Test Step Comment: See step 2 for Brightness window in the Settings menu
            */
            // Call generic Check Results Method
            DmiExpectedResults.See_the_expectation_in_step_2(this);


            /*
            Test Step 22
            Action: Perform the following procedure;Press ‘close’ button (Brightness window) Press ‘System Version’ button
            Expected Result: Verify the following information;(1)   Verify DMI still displays Setting window until System Version window is displayed.(2)   Verify the close button is always enable
            Test Step Comment: (1) MMI_gen 8859 (partly: windows in setting menu);(2) MMI_gen 5646 (partly: always enable, windows in setting menu);
            */


            /*
            Test Step 23
            Action: Repeat action step 2 with System Version window
            Expected Result: Verify the following information;DMI in the entry state of ‘ST05’(1)   The hourglass symbol ST05 is displayed.(2)   The disabled Close button NA12 is display in area G.10 seconds laterDMI in the exit state of ‘ST05’(3)   The hourglass symbol ST05 is removed.(4)   The enabled Close button NA11 is display in area G
            Test Step Comment: (1) MMI_gen 12018 (partly: windows in setting menu);(3) MMI_gen 4396 (partly: close, NA12, windows in setting menu); MMI_gen 5646 (partly: State ‘ST05’ button is disabled, windows in setting menu); MMI_gen 4395 (partly: close button, disabled, windows in setting menu);(5) MMI_gen 5728 (partly: removal, EVC, windows in setting menu);(7) MMI_gen 4396 (partly: close, NA11, windows in setting menu);
            */


            /*
            Test Step 24
            Action: Perform the following procedure;Press ‘close’ button (System Version window) Press ‘Set VBC’ button
            Expected Result: Verify the following information;(1)   Verify DMI still displays Setting window until Set VBC window is displayed.(2)   Verify the close button is always enable
            Test Step Comment: (1) MMI_gen 8859 (partly: windows in setting menu);(2) MMI_gen 5646 (partly: always enable, windows in setting menu);
            */


            /*
            Test Step 25
            Action: Repeat action step 2 with Set VBC window
            Expected Result: See the expectation in step 2
            Test Step Comment: See step 2 for Set VBC window in the Settings menu
            */
            // Call generic Check Results Method
            DmiExpectedResults.See_the_expectation_in_step_2(this);


            /*
            Test Step 26
            Action: Perform the following procedure;Enter VBC Code ‘65536’Confirm entered data by pressing an input field.Press ‘Yes’ button.Press ‘Yes’ button (on keypad)
            Expected Result: Verify the following information;(1)   Verify DMI still displays Set VBC window until Validate Set VBC window is displayed.(2)   Verify the close button is always enable. (3)   Verify the <Yes> button is always enable
            Test Step Comment: (1) MMI_gen 8859 (partly: windows in setting menu);(2) MMI_gen 5646 (partly: always enable, windows in setting menu);(3) MMI_gen 5719 (partly: always enable, windows in setting menu);
            */
            // Call generic Action Method
            DmiActions
                .Perform_the_following_procedureEnter_VBC_Code_65536Confirm_entered_data_by_pressing_an_input_field_Press_Yes_button_Press_Yes_button_on_keypad(this);


            /*
            Test Step 27
            Action: Repeat action step 2 with Validate set VBC window
            Expected Result: See the expectation in step 8
            Test Step Comment: See step 8 for Validate set VBC window in the Settings menu
            */
            // Call generic Check Results Method
            DmiExpectedResults.See_the_expectation_in_step_8(this);


            /*
            Test Step 28
            Action: Perform the following procedure;Confirm entered data by pressing an input field.Press ‘Remove VBC’ button
            Expected Result: Verify the following information;(1)   Verify DMI still displays Setting window until Remove VBC window is displayed.(2)   Verify the close button is always enable
            Test Step Comment: (1) MMI_gen 8859 (partly: windows in setting menu);(2) MMI_gen 5646 (partly: always enable, windows in setting menu);
            */


            /*
            Test Step 29
            Action: Repeat action step 2 with Remove VBC window
            Expected Result: See the expectation in step 2
            Test Step Comment: See step 2 for Remove VBC window in the Settings menu
            */
            // Call generic Check Results Method
            DmiExpectedResults.See_the_expectation_in_step_2(this);


            /*
            Test Step 30
            Action: Perform the following procedure;Enter VBC Code ‘65536’Confirm entered data by pressing an input field.Press ‘Yes’ button.Press ‘Yes’ button (on keypad)
            Expected Result: Verify the following information;(1)   Verify DMI still displays Remove VBC window until Validate Remove VBC window is displayed.(2)   Verify the close button is always enable. (3)   Verify the <Yes> button is always enable
            Test Step Comment: (1) MMI_gen 8859 (partly: windows in setting menu);(2) MMI_gen 5646 (partly: always enable, windows in setting menu);(3) MMI_gen 5719 (partly: always enable, windows in setting menu);
            */
            // Call generic Action Method
            DmiActions
                .Perform_the_following_procedureEnter_VBC_Code_65536Confirm_entered_data_by_pressing_an_input_field_Press_Yes_button_Press_Yes_button_on_keypad(this);


            /*
            Test Step 31
            Action: Repeat action step 2 with Validate remove VBC window
            Expected Result: See the expectation in step 8
            Test Step Comment: See step 8 for Validate remove VBC window in the Settings menu
            */
            // Call generic Check Results Method
            DmiExpectedResults.See_the_expectation_in_step_8(this);


            /*
            Test Step 32
            Action: Perform the following procedure;Confirm entered data by pressing an input field.Press ‘Set Clock’ button
            Expected Result: Verify the following information;(1)   Verify DMI still displays Setting window until Set Clock window is displayed.(2)   Verify the close button is always enable
            Test Step Comment: (1) MMI_gen 8859 (partly: windows in setting menu);(2) MMI_gen 5646 (partly: always enable, windows in setting menu);
            */


            /*
            Test Step 33
            Action: Repeat action step 2 with Set Clock window
            Expected Result: Verify the following information;DMI in the entry state of ‘ST05’.(1)   The hourglass symbol ST05 is displayed.(2)   Verify all buttons and the close button is disable. (except ‘Navigator’ button)(3)   The disabled Close button NA12 is display in area G.(4)   All Input Field are deselected.10 seconds laterDMI in the exit state of ‘ST05’.(5)   The hourglass symbol ST05 is removed.(6)   The state of all buttons is restored according to the last status before script is sent.(7)   The enabled Close button NA11 is display in area G.(8)   The input field is stated as follows:The first input field is in the ‘Selected’ state.The all others are in the ‘Not selected’ state
            Test Step Comment: (1) MMI_gen 12018 (partly: windows in setting menu);(2) MMI_gen 168 (partly: disabled buttons, windows in setting menu); MMI_gen 5646 (partly: State ‘ST05’ button is disabled, windows in setting menu); MMI_gen 4395 (partly: close button, disabled, windows in setting menu); Note under the MMI_gen 5728;(3) MMI_gen 4396 (partly: close, NA12, windows in setting menu);(4) MMI_gen 168 (partly: deselect input field, windows in setting menu);(5) MMI_gen 5728 (partly: removal, EVC, windows in setting menu);(6) MMI_gen 5728 (partly: restore after ST05, windows in setting menu);(7) MMI_gen 4396 (partly: close, NA11, windows in setting menu);(8) MMI_gen 5728 (partly: input field, windows in setting menu);
            */


            /*
            Test Step 34
            Action: Perform the following procedure;Press ‘close’ button (Set Clock window) Press ‘Brake’ button
            Expected Result: Verify the following information;(1)   Verify DMI still displays Setting window until Brake window is displayed.(2)   Verify the close button is always enable
            Test Step Comment: (1) MMI_gen 8859 (partly: windows in setting menu);(2) MMI_gen 5646 (partly: always enable, windows in setting menu);
            */


            /*
            Test Step 35
            Action: Repeat action step 2 with Brake window
            Expected Result: See the expectation in step 4
            Test Step Comment: See step 4 for Brake window in the Settings menu
            */
            // Call generic Check Results Method
            DmiExpectedResults.See_the_expectation_in_step_4(this);


            /*
            Test Step 36
            Action: Press ‘Test’ button
            Expected Result: Verify the following information;(1)   Verify DMI still displays Brake window until Brake test window is displayed.(2)   Verify the close button is always enable
            Test Step Comment: (1) MMI_gen 8859 (partly: windows in setting menu);(2) MMI_gen 5646 (partly: always enable, windows in setting menu);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press ‘Test’ button");


            /*
            Test Step 37
            Action: Repeat action step 2 with Brake test window
            Expected Result: See the expectation in step 4
            Test Step Comment: See step 4 for Brake test window in the Settings menu
            */
            // Call generic Check Results Method
            DmiExpectedResults.See_the_expectation_in_step_4(this);


            /*
            Test Step 38
            Action: Perform the following procedure;Press ‘close’ button (Brake test window)Press ‘Percentage’ button
            Expected Result: Verify the following information;(1)   Verify DMI still displays Brake window until Brake percentage window is displayed.(2)   Verify the close button is always enable
            Test Step Comment: (1) MMI_gen 8859 (partly: windows in setting menu);(2) MMI_gen 5646 (partly: always enable, windows in setting menu);
            */


            /*
            Test Step 39
            Action: Repeat action step 2 with Brake percentage window
            Expected Result: See the expectation in step 2
            Test Step Comment: See step 2 for Brake percentage window in the Settings menu
            */
            // Call generic Check Results Method
            DmiExpectedResults.See_the_expectation_in_step_2(this);


            /*
            Test Step 40
            Action: Perform the following procedure;Enter brake percentage ‘80’Confirm entered data by pressing an input field.Press ‘Yes’ button (on keypad)
            Expected Result: Verify the following information;(1)   Verify DMI still displays Brake percentage window until Validate Brake percentage window is displayed.(2)   Verify the close button is always enable. (3)   Verify the <Yes> button is always enable
            Test Step Comment: (1) MMI_gen 8859 (partly: windows in setting menu);(2) MMI_gen 5646 (partly: always enable, windows in setting menu);(3) MMI_gen 5719 (partly: always enable, windows in setting menu);
            */


            /*
            Test Step 41
            Action: Repeat action step 2 with Validate brake percentage window
            Expected Result: See the expectation in step 8
            Test Step Comment: See step 8 for Validate brake percentage window in the Settings menu
            */
            // Call generic Check Results Method
            DmiExpectedResults.See_the_expectation_in_step_8(this);


            /*
            Test Step 42
            Action: Perform the following procedure;Confirm entered data by pressing an input field.Press ‘close’ button (Brake window) Press ‘System Info’ button
            Expected Result: Verify the following information;(1)   Verify DMI still displays Setting window until System Info window is displayed.(2)   Verify the close button is always enable
            Test Step Comment: (1) MMI_gen 8859 (partly: windows in setting menu);(2) MMI_gen 5646 (partly: always enable, windows in setting menu);
            */


            /*
            Test Step 43
            Action: Repeat action step 2 with System Info window
            Expected Result: See the expectation in step 23
            Test Step Comment: See step 23 for System Info window in the Settings menu
            */


            /*
            Test Step 44
            Action: End of test
            Expected Result: 
            */


            return GlobalTestResult;
        }
    }
}
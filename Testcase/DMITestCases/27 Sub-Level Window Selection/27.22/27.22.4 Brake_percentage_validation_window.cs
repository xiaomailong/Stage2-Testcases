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
    /// 27.22.4 Brake percentage validation window
    /// TC-ID: 22.22.4 
    /// 
    /// This test case verifies the general appearances of Brake percentage validation window based on chapter 6.5.6.3 of requirement specification with packet information sending and receiving.
    /// 
    /// Tested Requirements:
    /// MMI_gen 11834; MMI_gen 11835; MMI_gen 11836; MMI_gen 11837; MMI_gen 11838; MMI_gen 11839; MMI_gen 11833 (partly: MMI_gen 5215 (partly: Close button, Window title, Input field, No button, Yes button), MMI_gen 5216, MMI_gen 7943, window on half grid array, MMI_gen 5214, MMI_gen 5006,  MMI_gen 5484, MMI_gen 5263 (partly: MMI_gen 4696, MMI_gen 4697, MMI_gen 4698, MMI_gen 4700 (partly: data validation process), MMI_gen 4702 (partly: right aligned), MMI_gen 4704 (partly: left aligned), partly: MMI_gen 4701), MMI_gen 5303); MMI_gen 5724; MMI_gen 5720; MMI_gen 2519 (partly: Brake validation window); MMI_gen 1426 (partly: Brake validation window); MMI_gen 4392 (partly: [Close] NA11, returning to the parent window, [Enter], touch screen); MMI_gen 4350; MMI_gen 4351; MMI_gen 4353; MMI_gen 4377 (partly: shown);
    /// 
    /// Scenario:
    /// Enter and confirm all data in Brake Percentage window. Then, verify the display information and received packet data EVC-51.Press ‘No’ button and verify that the value of an input field is changed refer to pressed button.Confirm entered data by pressing an input field. Then, verify that DMI closes Brake Percentage Validation window and open Brake window with sending out packet EVC-101.Open Brake Percentage window. Then, enter and confirm all data.Press ‘Close’ button. Then, verify that DMI closes Brake Percentage Validation window and open Brake window with sending out packet EVC-101.Open Brake Percentage window. Then, enter and confirm all data.Press ‘Yes’ button and verify that the value of an input field is changed refer to pressed button.Confirm entered data by pressing an input field. Then, verify that DMI close Brake Percentage Validation window and open Brake window with sending out packet EVC-151.Simulate loss-communication between ETCS and DMI. Then, re-establish communication and verify the state of buttons in Brake Percentage Validation window.
    /// 
    /// Used files:
    /// N/A
    /// </summary>
    public class Brake_percentage_validation_window : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // Configure atpcu configuration file as following (See the instruction in Appendix 2)M_InstalledLevels = 31NID_NTC_Installe_0 = 22 (ATC-2)Test system is powered on.Cabin is activated.Level ATC-2 is selected and confirmed.SoM is performed until train running number is entered.Settings window is opened.Brake button is enabled.Brake Percentage button is enabled.Brake Percentage window is opened.

            // Call the TestCaseBase PreExecution
            base.PreExecution();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in SB mode, level 1

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint


            /*
            Test Step 1
            Action: Enter and confirm all data in Brake Percentage window
            Expected Result: Verify the following information,(1)   Use the log file to confirm that DMI received the packet MMI_ECHOED_BRAKE_PERCENTAGE (EVC-51). (2)   Use the log file to confirm that the following variables in packet EVC-51 are same as entered data,MMI_M_BP_CURRENT = entered brake percentageMMI_M_BP_MEASURED = entered Last measured BPMMI_M_BP_ORIG = entered Original BP(3)   DMI displays Brake Percentage Validation window.(4)   The following objects are displayed in Brake Percentage Validation window. Enabled Close button (NA11)Window TitleInput fieldYes buttonNo buttonWindow Title(5)   The window title is ‘Validate brake percentage’.(6)   The window title is right aligned.Layer(7)   The window is displayed in main area A/B/C/D/E/F/G.(8)   All areas of Data validation window are displayed in Layer 0.Input field(9)   The window contains a single input field which has only data area.(10)   The value of input field is empty.Keyboard(11)   The displayed keyboard type is dedicated keyboard which contain only ‘Yes’ and ‘No’ button.The key #7 is No button.The key #8 is Yes button.Echo Text(12)   Echo Text is composed of a Label part and Data part.(13)   The Label of echo text is right aligned.(14)   The Data part of echo text is left aligned.(15)   The order of echo texts are same as of the Brake Percentage window as follows,Original BPLast measured BPBrake percentage(16)   The data part of echo texts are display the data value same as of the Brake percentage window.(17)   The echo texts are located in Main area A,B,C and E.(18)   The echo texts colour is white.General property of window(19)   The Brake percentage validation window is presented with objects and buttons which is the one of several levels and allocated to areas of DMI(20)   All objects, text messages and buttons are presented within the same layer.(21)   The Default window is not displayed and covered the current window
            Test Step Comment: (1) MMI_gen 11834 (partly: EVC-51);(2) MMI_gen 11839;(3) MMI_gen 11834 (partly: open Brake Percentage Validation window, touch screen);(4) MMI_gen 11833 (partly: MMI_gen 5215 (partly: Close button, Window title, Input field, No button, Yes button)); MMI_gen 4392 (partly: [Close] NA11);(5) MMI_gen 11837;(6) MMI_gen 11833 (partly: MMI_gen 5216);(7) MMI_gen 11833 (partly: MMI_gen 7943);(8) MMI_gen 11833 (partly: MMI_gen 5303);(9) MMI_gen 11833 (partly: MMI_gen 5214 (partly: single input field));          (10) MMI_gen 11833 (partly: MMI_gen 5484 (partly: empty)); (11) MMI_gen 11833 (partly: MMI_gen 5214 (partly: dedicated keyboard, MMI_gen 5006), MMI_gen 5006);(12) MMI_gen 11833 (partly: MMI_gen 5263 (partly: MMI_gen 4696));(13) MMI_gen 11833 (partly: MMI_gen 5263 (partly: MMI_gen 4702 (partly: right aligned)));(14) MMI_gen 11833 (partly: MMI_gen 5263 (partly: MMI_gen 4704 (partly: left aligned)));(15) MMI_gen 11838;                  MMI_gen 11833 (partly: MMI_gen 5263 (partly: MMI_gen 4701 (partly: same order), MMI_gen 4697));(16) MMI_gen 11833 (partly: MMI_gen 5263 (partly: MMI_gen 4698));(17) MMI_gen 11833 (partly: MMI_gen 5263 (partly: MMI_gen 4701 (partly: Main area A, B, C and E));(18) MMI_gen 11833 (partly: MMI_gen 5263 (partly: MMI_gen 4700 (partly: data validation process)));(19) MMI_gen 4350;(20) MMI_gen 4351;(21) MMI_gen 4353;
            */


            /*
            Test Step 2
            Action: Press ‘No’ button
            Expected Result: The value of input field is changed refer to selected button
            Test Step Comment: MMI_gen 11833 (partly: MMI_gen 5484 (partly: filled ‘No’));
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press ‘No’ button");
            // Call generic Check Results Method
            DmiExpectedResults.The_value_of_input_field_is_changed_refer_to_selected_button(this);


            /*
            Test Step 3
            Action: Confirm entered data by pressing an input field
            Expected Result: Verify the following information,DMI displays Brake window.Use the log file to confirm that DMI sends out the packet [MMI_DRIVER_REQUEST (EVC-101)] with variable [MMI_DRIVER_REQUEST (EVC-101).MMI_M_REQUEST] = 60 (Exit brake percentage)
            Test Step Comment: (1) MMI_gen 11836 (partly: No button, open Brake window);(2) MMI_gen 11836 (partly: EVC-101); MMI_gen 5724; MMI_gen 4392 (partly: [Enter], touch screen);
            */
            // Call generic Action Method
            DmiActions.Confirm_entered_data_by_pressing_an_input_field(this);
            // Call generic Check Results Method
            DmiExpectedResults
                .Verify_the_following_information_DMI_displays_Brake_window_Use_the_log_file_to_confirm_that_DMI_sends_out_the_packet_MMI_DRIVER_REQUEST_EVC_101_with_variable_MMI_DRIVER_REQUEST_EVC_101_MMI_M_REQUEST_60_Exit_brake_percentage(this);


            /*
            Test Step 4
            Action: Perform the following procedure,Press ‘Brake’ percentage button.Enter and confirm all data in brake percentage window.Press ‘Yes’ button
            Expected Result: DMI displays Brake percentage validation window
            */
            // Call generic Action Method
            DmiActions
                .Perform_the_following_procedure_Press_Brake_percentage_button_Enter_and_confirm_all_data_in_brake_percentage_window_Press_Yes_button(this);
            // Call generic Check Results Method
            DmiExpectedResults.DMI_displays_Brake_percentage_validation_window(this);


            /*
            Test Step 5
            Action: Press ‘Close’ button
            Expected Result: Verify the following information,DMI displays Brake window.Use the log file to confirm that DMI sends out the packet [MMI_DRIVER_REQUEST (EVC-101)] with variable [MMI_DRIVER_REQUEST (EVC-101).MMI_M_REQUEST] = 60 (Exit brake percentage)
            Test Step Comment: (1) MMI_gen 11836 (partly: Close button, open Brake window);(2) MMI_gen 11836 (partly: EVC-101); MMI_gen 4392 (partly: returning to the parent window);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press ‘Close’ button");
            // Call generic Check Results Method
            DmiExpectedResults
                .Verify_the_following_information_DMI_displays_Brake_window_Use_the_log_file_to_confirm_that_DMI_sends_out_the_packet_MMI_DRIVER_REQUEST_EVC_101_with_variable_MMI_DRIVER_REQUEST_EVC_101_MMI_M_REQUEST_60_Exit_brake_percentage(this);


            /*
            Test Step 6
            Action: Perform the following procedure,Press ‘Brake’ percentage button.Enter and confirm all data in brake percentage window.Press ‘Yes’ button
            Expected Result: DMI displays Brake percentage validation window
            */
            // Call generic Action Method
            DmiActions
                .Perform_the_following_procedure_Press_Brake_percentage_button_Enter_and_confirm_all_data_in_brake_percentage_window_Press_Yes_button(this);
            // Call generic Check Results Method
            DmiExpectedResults.DMI_displays_Brake_percentage_validation_window(this);


            /*
            Test Step 7
            Action: Press ‘Yes’ button
            Expected Result: The value of input field is changed refer to selected button
            Test Step Comment: MMI_gen 11833 (partly: MMI_gen 5484 (partly: filled ‘Yes’));
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press ‘Yes’ button");
            // Call generic Check Results Method
            DmiExpectedResults.The_value_of_input_field_is_changed_refer_to_selected_button(this);


            /*
            Test Step 8
            Action: Confirm entered data by pressing an input field
            Expected Result: Verify the following information,The Brake percentage validation is closed.DMI displays Brake window.Use the log file to confirm that DMI sends out the packet [MMI_CONFIRMED_BRAKE_PERCENTAGE (EVC-151)] with variable based on confirmed data
            Test Step Comment: (1) MMI_gen 11835 (partly: closure); MMI_gen 11833 (partly: MMI_gen 5720 (partly: closed));(2) MMI_gen 11835 (partly: open Brake window);(3) MMI_gen 11836 (partly: EVC-151); MMI_gen 11833 (partly: MMI_gen 5720 (partly: ConfirmedData-Packet));
            */
            // Call generic Action Method
            DmiActions.Confirm_entered_data_by_pressing_an_input_field(this);


            /*
            Test Step 9
            Action: Perform the following procedure,Press ‘Brake percentage’ button.Enter and confirm all data in brake percentage window.Press ‘Yes’ button.Then, Simulate loss-communication between ETCS onboard and DMI
            Expected Result: DMI displays Default window with the  message “ATP Down Alarm” and sound alarm
            */
            // Call generic Check Results Method
            DmiExpectedResults.DMI_displays_Default_window_with_the_message_ATP_Down_Alarm_and_sound_alarm(this);


            /*
            Test Step 10
            Action: Re-establish communication between ETCS onboard and DMI
            Expected Result: Verify the following informaiton,All buttons except ‘No’ button are disabled.The state of ‘No’ button is enabled.The disabled buttons are shown as a button in state ‘disabled‘ with text label in dark-grey
            Test Step Comment: (1) MMI_gen 2519 (partly: Brake percentage Validation window, All Request buttons except negative validations);(2) MMI_gen 2519 (partly: Brake percentage Validation window, All negative validations);(3) MMI_gen 1426 (partly: Brake percentage Validation window); MMI_gen 4377 (partly: shown);
            */
            // Call generic Action Method
            DmiActions.Re_establish_communication_between_ETCS_onboard_and_DMI(this);


            /*
            Test Step 11
            Action: End of test
            Expected Result: 
            */


            return GlobalTestResult;
        }
    }
}
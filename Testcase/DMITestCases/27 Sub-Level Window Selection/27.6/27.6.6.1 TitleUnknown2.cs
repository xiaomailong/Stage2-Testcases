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
    /// 27.6.6.1
    /// TC-ID: 22.6.6.1
    /// 
    /// This test case verifies the general appearances of Radar validation window based on chapter 6.5.6.3 of requirement specification with packet information sending and receiving.
    /// 
    /// Tested Requirements:
    /// MMI_gen 11792; MMI_gen 11793; MMI_gen 11794 (partly: MMI_gen 5724, MMI_gen 5720; MMI_gen 2519 (partly: Radar validation window); MMI_gen 1426 (partly: Radar validation window)); MMI_gen 11795; MMI_gen 11796; MMI_gen 11797; MMI_gen 11791 (partly: MMI_gen 5215 (partly: Close button, Window title, Input field, No button, Yes button), MMI_gen 5216, MMI_gen 7943, window on half grid array, MMI_gen 5214, MMI_gen 5006, MMI_gen 5484, MMI_gen 5263 (partly: MMI_gen 4696, MMI_gen 4697, MMI_gen 4698, MMI_gen 4700 (partly: data validation process), MMI_gen 4702 (partly: right aligned), MMI_gen 4704 (partly: left aligned), partly: MMI_gen 4701), MMI_gen 5303); MMI_gen 4392 (partly: [Close] NA11, returning to the parent window, [Enter], touch screen); MMI_gen 4350; MMI_gen 4351; MMI_gen 4353; MMI_gen 9390 (partly: Radar Validation window); MMI_gen 4377 (partly: shown);
    /// 
    /// Scenario:
    /// Enter and confirm all data in Radar window. Then, verify the display information and received packet data EVC-41.Press ‘No’ button and verify that the value of an input field is changed refer to pressed button.Confirm entered data by pressing an input field. Then, verify that DMI closes Radar Validation window and openss Radar window with sending out packet EVC-101.Open Radar window. Then, enter and confirm all data.Press ‘Close’ button. Then, verify that DMI closes Radar Validation window and opens Brake window with sending out packet EVC-101.Open Radar window. Then, enter and confirm all data.Press ‘Yes’ button and verify that the value of an input field is changed refer to pressed button.Confirm entered data by pressing an input field. Then, verify that DMI closes Radar Validation window and open Brake window with sending out packet EVC-141.Simulate loss-communication between ETCS and DMI. Then, re-establish communication and verify the state of buttons in Radar Validation window.
    /// 
    /// Used files:
    /// N/A
    /// </summary>
    public class TitleUnknown2 : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // The Maintenance password in tag name ‘PASS_CODE_MTN’ of the configuration file is set correctly refer to MMI_gen 11722Test system is power on.Cabin is activated.Settings window is opened.Maintenance window is opened.Radar window is opened.

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
            Action: Enter and confirm all data in Radar window
            Expected Result: Verify the following information,Use the log file to confirm that DMI received the packet MMI_ECHOED_MAINTENANCE_DATA (EVC-41) with variable MMI_Q_MD_DATASET = 1. Use the log file to confirm that the following variables in packet EVC-41 are same as the entered data,MMI_M_PULSE_PER_KM_1 = entered radar 1DMI displays Radar Validation window.The following objects are displayed in Radar Validation window. Enabled Close button (NA11)Window TitleInput fieldYes buttonNo buttonWindow TitleThe window title is ‘Validate radar’.The window title is right aligned.LayerThe window is displayed in main area A/B/C/D/E/F/G.All areas of Data validation window are Layer 0.Input fieldThe window contains a single input field which have only data area.The value of input field is empty.KeyboardThe displayed keyboard type is dedicated keyboard which contain only ‘Yes’ and ‘No’ button.The key #7 is No button.The key #8 is Yes button.Echo TextEcho Text is composed of a Label part and Data part.The Label of echo text is right aligned.The Data part of echo text is left aligned.The order of echo texts are same as of the Radar window as follows,Radar 1 (mm)Radar 2 (mm)The data part of echo texts is displayed the data value same as of the Radar window.The echo texts are located in Main area A,B,C and E.The colour of echo texts is white.General property of windowThe Radar Validation window is presented with objects, text messages and buttons which is the one of several levels and allocated to areas of DMI. All objects, text messages and buttons are presented within the same layer.The Default window is not displayed and covered the current window
            Test Step Comment: (1) MMI_gen 11792 (partly: EVC-41);(2) MMI_gen 11797;(3) MMI_gen 11792 (partly: open Radar Validation window, touch screen);(4) MMI_gen 11791 (partly: MMI_gen 5215 (partly: Close button, Window title, Input field, No button, Yes button)); MMI_gen 4392 (partly: [Close] NA11);(5) MMI_gen 11795;(6) MMI_gen 11791 (partly: MMI_gen 5216);(7) MMI_gen 11791 (partly: MMI_gen 7943);(8) MMI_gen 11791 (partly: MMI_gen 5303);(9) MMI_gen 11791 (partly: MMI_gen 5214 (partly: single input field));          (10) MMI_gen 11791 (partly: MMI_gen 5484 (partly: empty)); (11) MMI_gen 11791 (partly: MMI_gen 5214 (partly: dedicated keyboard, MMI_gen 5006), MMI_gen 5006);(12) MMI_gen 11791 (partly: MMI_gen 5263 (partly: MMI_gen 4696));(13) MMI_gen 11791 (partly: MMI_gen 5263 (partly: MMI_gen 4702 (partly: right aligned)));(14) MMI_gen 11791 (partly: MMI_gen 5263 (partly: MMI_gen 4704 (partly: left aligned)));(15) MMI_gen 11796;                  MMI_gen 11791 (partly: MMI_gen 5263 (partly: MMI_gen 4701 (partly: same order), MMI_gen 4697));(16) MMI_gen 11791 (partly: MMI_gen 5263 (partly: MMI_gen 4698));(17) MMI_gen 11791 (partly: MMI_gen 5263 (partly: MMI_gen 4701 (partly: Main area A, B, C and E));(18) MMI_gen 11791 (partly: MMI_gen 5263 (partly: MMI_gen 4700 (partly: data validation process)));(19) MMI_gen 4350;(20) MMI_gen 4351;(21) MMI_gen 4353;
            */


            /*
            Test Step 2
            Action: Press ‘No’ button
            Expected Result: The value of input field is changed refer to selected button
            Test Step Comment: MMI_gen 11791 (partly: MMI_gen 5484 (partly: filled ‘No’));
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(@"Press ‘No’ button");
            // Call generic Check Results Method
            DmiExpectedResults.The_value_of_input_field_is_changed_refer_to_selected_button(this);


            /*
            Test Step 3
            Action: Press and hold an input field
            Expected Result: Verify the following information,(1)    The state of an input field is changed to ‘Pressed’, the border of button is removed
            Test Step Comment: (1) MMI_gen 9390 (partly: Radar Validation window);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(@"Press and hold an input field");
            // Call generic Check Results Method
            DmiExpectedResults
                .Verify_the_following_information_1_The_state_of_an_input_field_is_changed_to_Pressed_the_border_of_button_is_removed(this);


            /*
            Test Step 4
            Action: Slide out an input field
            Expected Result: Verify the following information,(1)    The state of an input field is changed to ‘Enabled, the border of button is shown without a sound
            Test Step Comment: (1) MMI_gen 9390 (partly: Radar Validation window);
            */
            // Call generic Action Method
            DmiActions.Slide_out_an_input_field(this);
            // Call generic Check Results Method
            DmiExpectedResults
                .Verify_the_following_information_1_The_state_of_an_input_field_is_changed_to_Enabled_the_border_of_button_is_shown_without_a_sound(this);


            /*
            Test Step 5
            Action: Slide back into an input field
            Expected Result: Verify the following information,(1)    The state of an input field is changed to ‘Pressed’, the border of button is removed
            Test Step Comment: (1) MMI_gen 9390 (partly: Radar Validation window);
            */
            // Call generic Action Method
            DmiActions.Slide_back_into_an_input_field(this);
            // Call generic Check Results Method
            DmiExpectedResults
                .Verify_the_following_information_1_The_state_of_an_input_field_is_changed_to_Pressed_the_border_of_button_is_removed(this);


            /*
            Test Step 6
            Action: Release the pressed area
            Expected Result: Verify the following information,DMI displays Maintenance window.Use the log file to confirm that DMI sends out the packet [MMI_DRIVER_REQUEST (EVC-101)] with variable [MMI_DRIVER_REQUEST (EVC-101).MMI_M_REQUEST] = 54 (Exit Maintenance)
            Test Step Comment: (1) MMI_gen 11794 (partly: No button, open Maintenance window);(2) MMI_gen 11794 (partly: EVC-101, MMI_gen 5724);  MMI_gen 4392 (partly: [Enter], touch screen); MMI_gen 9390 (partly: Radar Validation window);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(@"Release the pressed area");


            /*
            Test Step 7
            Action: Perform the following procedure,Press ‘Radar’ button.Enter and confirm all data in Radar window.Press ‘Yes’ button
            Expected Result: DMI displays Radar validation window
            */
            // Call generic Action Method
            DmiActions
                .Perform_the_following_procedure_Press_Radar_button_Enter_and_confirm_all_data_in_Radar_window_Press_Yes_button(this);
            // Call generic Check Results Method
            DmiExpectedResults.DMI_displays_Radar_validation_window(this);


            /*
            Test Step 8
            Action: Press ‘Close’ button
            Expected Result: Verify the following information,DMI displays Maintenance window.Use the log file to confirm that DMI sends out the packet [MMI_DRIVER_REQUEST (EVC-101)] with variable ;[MMI_DRIVER_REQUEST (EVC-101).MMI_M_REQUEST] = 54 (Exit Maintenance)
            Test Step Comment: (1) MMI_gen 11794 (partly: Close button, open Maintenance window); MMI_gen 4392 (partly: returning to the parent window);(2) MMI_gen 11794 (partly: EVC-101); 
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(@"Press ‘Close’ button");
            // Call generic Check Results Method
            DmiExpectedResults
                .Verify_the_following_information_DMI_displays_Maintenance_window_Use_the_log_file_to_confirm_that_DMI_sends_out_the_packet_MMI_DRIVER_REQUEST_EVC_101_with_variable_MMI_DRIVER_REQUEST_EVC_101_MMI_M_REQUEST_54_Exit_Maintenance(this);


            /*
            Test Step 9
            Action: Perform the following procedure,Press ‘Radar’ button.Enter and confirm all data in Radar window.Press ‘Yes’ button
            Expected Result: DMI displays Radar validation window
            */
            // Call generic Action Method
            DmiActions
                .Perform_the_following_procedure_Press_Radar_button_Enter_and_confirm_all_data_in_Radar_window_Press_Yes_button(this);
            // Call generic Check Results Method
            DmiExpectedResults.DMI_displays_Radar_validation_window(this);


            /*
            Test Step 10
            Action: Press ‘Yes’ button
            Expected Result: The value of input field is changed refer to selected button
            Test Step Comment: MMI_gen 11791 (partly: MMI_gen 5484 (partly: filled ‘Yes’));
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(@"Press ‘Yes’ button");
            // Call generic Check Results Method
            DmiExpectedResults.The_value_of_input_field_is_changed_refer_to_selected_button(this);


            /*
            Test Step 11
            Action: Confirm entered data by pressing an input field
            Expected Result: Verify the following information,The Radar validation is closed.DMI displays Maintenance window.Use the log file to confirm that DMI sends out the packet [MMI_CONFIRMED_MAINTENANCE_DATA (EVC-141)] with variable based on confirmed data
            Test Step Comment: (1) MMI_gen 11793 (partly: closure); MMI_gen 11794 (partly: MMI_gen 5720 (partly: closed));(2) MMI_gen 11793 (partly: open Maintenance window);(3) MMI_gen 11793 (partly: EVC-141); MMI_gen 11794 (partly: MMI_gen 5720 (partly: ConfirmedData-Packet));
            */
            // Call generic Action Method
            DmiActions.Confirm_entered_data_by_pressing_an_input_field(this);


            /*
            Test Step 12
            Action: Perform the following procedure,Press ‘Radar’ button.Enter and confirm all data in Radar window.Press ‘Yes’ button.Then, Simulate loss-communication between ETCS onboard and DMI
            Expected Result: DMI displays Default window with the  message “ATP Down Alarm” and sound alarm
            */
            // Call generic Check Results Method
            DmiExpectedResults.DMI_displays_Default_window_with_the_message_ATP_Down_Alarm_and_sound_alarm(this);


            /*
            Test Step 13
            Action: Re-establish communication between ETCS onboard and DMI
            Expected Result: Verify the following informaiton,All buttons except ‘No’ button are disabled.The state of ‘No’ button is enabled.The disabled button are shown as a button in state ‘disabled’ with text label in dark-grey
            Test Step Comment: (1) MMI_gen 11794 (partly: MMI_gen 2519 (partly: Radar Validation window, All Request buttons except negative validations));(2) MMI_gen 11794 (partly: MMI_gen 2519 (partly: Radar Validation window, All negative validations));(3) MMI_gen 11794 (partly: MMI_gen 1426 (partly: Radar Validation window)); MMI_gen 4377 (partly: shown);
            */
            // Call generic Action Method
            DmiActions.Re_establish_communication_between_ETCS_onboard_and_DMI(this);


            /*
            Test Step 14
            Action: End of test
            Expected Result: 
            */


            return GlobalTestResult;
        }
    }
}
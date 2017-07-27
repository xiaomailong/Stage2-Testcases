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
    /// 27.28.2 ‘Remove VBC’ Validation Window
    /// TC-ID: 22.28.2
    /// 
    /// This test case verifies the general appearances of Remove VBC validation window based on chapter 6.5.6.3 of requirement specification with packet information sending and receiving.
    /// 
    /// Tested Requirements:
    /// MMI_gen 9932; MMI_gen 9933 (partly: MMI_gen 5724, MMI_gen 5720, MMI_gen 2519 (partly: Remove VBC validation window); MMI_gen 1426 (partly: Remove VBC validation window)); MMI_gen 9934; MMI_gen 8573; MMI_gen 8574; MMI_gen 9935; MMI_gen 8572 (partly: MMI_gen 5215 (partly: Close button, Window title, Input field, No button, Yes button), MMI_gen 5216, MMI_gen 7943, window on half grid array, MMI_gen 5214, MMI_gen 5006, MMI_gen 5484, MMI_gen 5263 (partly: MMI_gen 4696, MMI_gen 4697, MMI_gen 4698, MMI_gen 4700 (partly: data validation process), MMI_gen 4702 (partly: right aligned), MMI_gen 4704 (partly: left aligned), partly: MMI_gen 4701), MMI_gen 5303); MMI_gen 4392 (partly: [Close] NA11, returning to the parent window, [Enter], touch screen); MMI_gen 4350; MMI_gen 4351; MMI_gen 4353; MMI_gen 9390 (partly: Remove VBC Validation window); MMI_gen 4377 (partly: shown);
    /// 
    /// Scenario:
    /// Enter and confirm all data in Remove VBC window. Then, verify the display information and received packet data EVC-29.Press ‘No’ button and verify that the value of an input field is changed refer to pressed button.Confirm entered data by pressing an input field. Then, verify that DMI closes Remove VBC Validation window and opens Remove VBC window with sending out packet EVC-101.Open Remove VBC window. Then, enter and confirm all data.Press ‘Close’ button. Then, verify that DMI closes Remove VBC Validation window and opens Remove VBC window with sending out packet EVC-101.Open Remove VBC window. Then, enter and confirm all data.Press ‘Yes’ button and verify that the value of an input field is changed refer to pressed button.Confirm entered data by pressing an input field. Then, verify that DMI closes Remove VBC Validation window and opens Remove VBC window with sending out packet EVC-129.Simulate loss-communication between ETCS and DMI. Then, re-establish communication and verify the state of buttons in Remove VBC Validation window.
    /// 
    /// Used files:
    /// N/A
    /// </summary>
    public class Remove_VBC_Validation_Window : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // System is powered ON.Cabin is activated.Perform SoM until Level 1 is selected and confirmed.The VBC code “65536” is stored on ETCS. (See the information in the “Data View” menu)Settings window is opened.Remove VBC window is opened.
            
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
            Action: Perform the following procedure,Enter and confirm value ‘65536’ at input field.Press ‘Yes’ button
            Expected Result: Verify the following information,Use the log file to confirm that DMI received the packet MMI_ECHOED_REMOVE_VBC_DATA (EVC-29). DMI displays Remove VBC Validation window.The following objects are displayed in Remove VBC Validation window. Enabled Close button (NA11)Window TitleInput fieldYes buttonNo buttonWindow TitleThe window title is ‘Validate Remove VBC’.The window title is right aligned.LayerThe window is displayed in main area A/B/C/D/E/F/G.All areas of Data validation window are displayed in Layer 0.Input fieldThe window contains a single input field which has only data area.The value of input field is empty.KeyboardThe displayed keyboard type is dedicated keyboard which contain only ‘Yes’ and ‘No’ button.The key #7 is No button.The key #8 is Yes button.Echo TextEcho Text is composed of a Label part and Data part.The Label of echo text is right aligned.The Data part of echo text is left aligned.The order of echo texts are same as of the Remove VBC window.The data part of echo texts are displayed the data value same as of the Remove VBC window.The echo texts are located in Main area A,B,C and E.The echo texts colour is white.Use the log file to confirm that the following variable in packet EVC-29 is same as entered data and display in the data part of echo text,MMI_M_VBC_CODE = entered VBC codeGeneral property of windowThe Remove VBC Validation window is presented with objects, text messages and buttons which is the one of several levels and allocated to areas of DMIAll objects, text messages and buttons are presented within the same layer.The Default window is not displayed and covered the current window
            Test Step Comment: (1) MMI_gen 9932 (partly: EVC-28);(2) MMI_gen 9932 (partly: open Remove VBC Validation window, touch screen);(3) MMI_gen 8572 (partly: MMI_gen 5215 (partly: Close button, Window title, Input field, No button, Yes button)); MMI_gen 4392 (partly: [Close] NA11);(4) MMI_gen 8573;(5) MMI_gen 8572 (partly: MMI_gen 5216);(6) MMI_gen 8572 (partly: MMI_gen 7943);(7) MMI_gen 8572 (partly: MMI_gen 5303);(8) MMI_gen 8572 (partly: MMI_gen 5214 (partly: single input field));          (9) MMI_gen 8572 (partly: MMI_gen 5484 (partly: empty)); (10) MMI_gen 8572 (partly: MMI_gen 5214 (partly: dedicated keyboard, MMI_gen 5006), MMI_gen 5006);(11) MMI_gen 8572 (partly: MMI_gen 5263 (partly: MMI_gen 4696));(12) MMI_gen 8572 (partly: MMI_gen 5263 (partly: MMI_gen 4702 (partly: right aligned)));(13) MMI_gen 8572 (partly: MMI_gen 5263 (partly: MMI_gen 4704 (partly: left aligned)));(14) MMI_gen 8572 (partly: MMI_gen 5263 (partly: MMI_gen 4701 (partly: same order), MMI_gen 4697));(15) MMI_gen 8572 (partly: MMI_gen 5263 (partly: MMI_gen 4698));(16) MMI_gen 8572 (partly: MMI_gen 5263 (partly: MMI_gen 4701 (partly: Main area A, B, C and E));(17) MMI_gen 8572 (partly: MMI_gen 5263 (partly: MMI_gen 4700 (partly: data validation process)));(18) MMI_gen 9935; MMI_gen 8574;(19) MMI_gen 4350;(20) MMI_gen 4351;(21) MMI_gen 4353;
            */
            // Call generic Action Method
            DmiActions.Perform_the_following_procedure_Enter_and_confirm_value_65536_at_input_field_Press_Yes_button();
            
            
            /*
            Test Step 2
            Action: Press ‘No’ button
            Expected Result: The value of input field is changed refer to selected button
            Test Step Comment: MMI_gen 8572 (partly: MMI_gen 5484 (partly: filled ‘No’));
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(@"Press ‘No’ button");
            // Call generic Check Results Method
            DmiExpectedResults.The_value_of_input_field_is_changed_refer_to_selected_button();
            
            
            /*
            Test Step 3
            Action: Confirm entered data by pressing an input field
            Expected Result: Verify the following information,DMI displays Remove VBC window.Use the log file to confirm that DMI sends out the packet [MMI_DRIVER_REQUEST (EVC-101)] with variable [MMI_DRIVER_REQUEST (EVC-101).MMI_M_REQUEST] = 26 (Exit Remove VBC)
            Test Step Comment: (1) MMI_gen 9933 (partly: No button, open Remove VBC window);(2) MMI_gen 9933 (partly: EVC-101, MMI_gen 5724); MMI_gen 4392 (partly: [Enter], touch screen);
            */
            // Call generic Action Method
            DmiActions.Confirm_entered_data_by_pressing_an_input_field();
            
            
            /*
            Test Step 4
            Action: Perform the following procedure,Enter and confirm value ‘65536’ at input field.Press ‘Yes’ button
            Expected Result: DMI displays Remove VBC Validation window
            */
            // Call generic Action Method
            DmiActions.Perform_the_following_procedure_Enter_and_confirm_value_65536_at_input_field_Press_Yes_button();
            // Call generic Check Results Method
            DmiExpectedResults.DMI_displays_Remove_VBC_Validation_window();
            
            
            /*
            Test Step 5
            Action: Press ‘Close’ button
            Expected Result: Verify the following information,DMI displays Settings window.Use the log file to confirm that DMI sends out the packet [MMI_DRIVER_REQUEST (EVC-101)] with variable [MMI_DRIVER_REQUEST (EVC-101).MMI_M_REQUEST] = 25 (Exit Remove VBC)
            Test Step Comment: (1) MMI_gen 9933 (partly: Close button, open Remove VBC window); MMI_gen 4392 (partly: returning to the parent window);(2) MMI_gen 9933 (partly: EVC-101);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(@"Press ‘Close’ button");
            
            
            /*
            Test Step 6
            Action: Perform the following procedure,Press ‘Remove VBC’ button.Enter and confirm value ‘65536’ at input field.Press ‘Yes’ button
            Expected Result: DMI displays Remove VBC Validation window
            */
            // Call generic Check Results Method
            DmiExpectedResults.DMI_displays_Remove_VBC_Validation_window();
            
            
            /*
            Test Step 7
            Action: Press ‘Yes’ button
            Expected Result: The value of input field is changed refer to selected button
            Test Step Comment: MMI_gen 8572 (partly: MMI_gen 5484 (partly: filled ‘Yes’));
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(@"Press ‘Yes’ button");
            // Call generic Check Results Method
            DmiExpectedResults.The_value_of_input_field_is_changed_refer_to_selected_button();
            
            
            /*
            Test Step 8
            Action: Press and hold an input field
            Expected Result: Verify the following information,(1)    The state of an input field is changed to ‘Pressed’, the border of button is removed
            Test Step Comment: (1) MMI_gen 9390 (partly: Remove VBC Validation window);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(@"Press and hold an input field");
            // Call generic Check Results Method
            DmiExpectedResults.Verify_the_following_information_1_The_state_of_an_input_field_is_changed_to_Pressed_the_border_of_button_is_removed();
            
            
            /*
            Test Step 9
            Action: Slide out an input field
            Expected Result: Verify the following information,(1)     The state of an input field is changed to ‘Enabled, the border of button is shown without a sound
            Test Step Comment: (1) MMI_gen 9390 (partly: Remove VBC Validation window);
            */
            // Call generic Action Method
            DmiActions.Slide_out_an_input_field();
            // Call generic Check Results Method
            DmiExpectedResults.Verify_the_following_information_1_The_state_of_an_input_field_is_changed_to_Enabled_the_border_of_button_is_shown_without_a_sound();
            
            
            /*
            Test Step 10
            Action: Slide back into an input field
            Expected Result: Verify the following information,(1)     The state of an input field is changed to ‘Pressed’, the border of button is removed
            Test Step Comment: (1) MMI_gen 9390 (partly: Remove VBC Validation window);
            */
            // Call generic Action Method
            DmiActions.Slide_back_into_an_input_field();
            // Call generic Check Results Method
            DmiExpectedResults.Verify_the_following_information_1_The_state_of_an_input_field_is_changed_to_Pressed_the_border_of_button_is_removed();
            
            
            /*
            Test Step 11
            Action: Release the pressed area
            Expected Result: DMI displays Settings window.Verify the following information,The Remove VBC validation is closed.Use the log file to confirm that DMI sends out the packet [MMI_CONFIRMED_REMOVE_VBC (EVC-129)] with variable based on confirmed data
            Test Step Comment: (1) MMI_gen 9933 (partly: MMI_gen 5720 (partly: closed));(2) MMI_gen 9934; MMI_gen 9933 (partly: MMI_gen 5720 (partly: ConfirmedData-Packet)); MMI_gen 9390 (partly: Remove VBC Validation window);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(@"Release the pressed area");
            
            
            /*
            Test Step 12
            Action: Perform the following procedure,Press ‘Set VBC’ button.Enter and validate value ‘65536’Press ‘Remove VBC button.Enter and confirm value ‘65536’ at input field.Press ‘Yes’ button.Then, simulate loss-communication between ETCS onboard and DMI
            Expected Result: DMI displays Default window with the  message “ATP Down Alarm” and sound alarm
            */
            // Call generic Check Results Method
            DmiExpectedResults.DMI_displays_Default_window_with_the_message_ATP_Down_Alarm_and_sound_alarm();
            
            
            /*
            Test Step 13
            Action: Re-establish communication between ETCS onboard and DMI
            Expected Result: Verify the following information,All buttons except ‘No’ button are disabled.The state of ‘No’ button is enabled.The disabled buttons are shown as a button is state ‘disabled’ with text label in dark-grey
            Test Step Comment: (1) MMI_gen 9934 (partly: MMI_gen 2519 (partly: Remove VBC Validation window, All Request buttons except negative validations));(2) MMI_gen 9934 (partly: MMI_gen 2519 (partly: Remove VBC Validation window, All negative validations));(3) MMI_gen 9934 (partly: MMI_gen 1426 (partly: Remove VBC Validation window)); MMI_gen 4377 (partly: shown);
            */
            // Call generic Action Method
            DmiActions.Re_establish_communication_between_ETCS_onboard_and_DMI();
            
            
            /*
            Test Step 14
            Action: End of test
            Expected Result: 
            */
            

            return GlobalTestResult;
        }
    }
}

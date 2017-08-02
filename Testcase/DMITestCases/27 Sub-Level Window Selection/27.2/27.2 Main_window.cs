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
    /// 27.2 Main window
    /// TC-ID: 7.1
    /// 
    /// This test case verifies the display of the menu buttons in ‘Main’ window for touch screen technology which complies with [ERA-ERTMS] standard and [MMI-ETCS-gen].
    /// 
    /// Tested Requirements:
    /// MMI_gen 8349 (partly: MMI_gen 7909, MMI_gen 4557, MMI_gen 4630 (partly: MMI_gen 5944 (partly: touch screen)), MMI_gen 4556 (partly: Close button, Window Title));MMI_gen 8350; MMI_gen 1205; MMI_gen 2490; MMI_gen 11908; MMI_gen 11909; MMI_gen 11910; MMI_gen 9954; MMI_gen 1423; MMI_gen 8354-1 (THR); MMI_gen 8351 (partly: touch screen); MMI_gen 2479; MMI_gen 11236; MMI_gen 11450-1 (THR); MMI_gen 591; MMI_gen 4355 (partly: Buttons, Close button); MMI_gen 4360 (partly: window title); MMI_gen 4392 (partly: [Close] NA11, returning to the parent window); MMI_gen 11911; MMI_gen 4389 (partly: Main window); MMI_gen 4350; MMI_gen 4351;MMI_gen 4353; MMI_gen 4354; MMI_gen 4377 (partly: shown);
    /// 
    /// Scenario:
    /// The concerned buttons in the ‘Main’ window are verified by the following actions:Press the button once (for the Delay-Type button)Press the button and holdSlide the button out with force appliedSlide the button back with force appliedRelease the buttonSoM is performed until the train running number is entered.The Up-Type ‘Start’ is verified.The ‘Main’ menu window is opened and verified.The Up-Type ‘Driver ID’, ‘Level’, ‘Train data’, and ‘Train running number’ are verified.The Safe Delay-Type ‘Non-leading’, ‘Shunting’, ‘Shunting Exit’, ‘Maintain Shunting’ buttons are verified.Note: ‘Non-leading’ and ‘Maintain Shunting’ signals is required to be simulated by OTE simulation.The enabling/disabling buttons are verified after preconditions, the ‘Non-leading’, ‘Shunting’, ‘Shunting Exit’, and ‘Maintain Shunting’ button actuated.
    /// 
    /// Used files:
    /// 7_1_a.xml, 7_1_b.xml
    /// </summary>
    public class Main_window : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // Test system is powered onCabin is activeSoM is performed until the train data validated.

            // Call the TestCaseBase PreExecution
            base.PreExecution();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in NL mode, level 1.

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint


            /*
            Test Step 1
            Action: Enter and confirm the Train running number
            Expected Result: DMI displays Main window.Use the log file to confirm that DMI received packet EVC-30 with the variable MMI_Q_REQUEST_ENABLE_64 (#0) = 1 (Enable Start) and the ‘Start’ button is enabled.The Main window is presented with objects and buttons which is the one of several levels and allocated to areas of DMI. All objects, text messages and buttons in Main window are presented within the same layer.The Default window is not displayed and covered the current window.Sub-level window covers partially depending on the size of the Sub-Level window. There is no other window is displayed and activated at the same time
            Test Step Comment: (1) MMI_gen 8351 (partly: touch screen, enabling condition of ‘Start’ button); MMI_gen 591 (partly: EVC-30, enabling, start);(2) MMI_gen 4350;(3) MMI_gen 4351;(4) MMI_gen 4353;(5) MMI_gen 4354;
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Enter and confirm the Train running number");


            /*
            Test Step 2
            Action: Press and hold ‘Start’ button
            Expected Result: Verify the following points,The sound ‘Click’ played once.The ‘Start’ button is shown as ‘Pressed’ state, the border of button is removed
            Test Step Comment: (1) MMI_gen 8349 (partly: MMI_gen 4557 (partly: button ‘Start’, MMI_gen 4381 (partly: the sound for Up-Type button))), MMI_gen 9512, MMI_gen 968;(2) MMI_gen 8349 (partly: MMI_gen 4557 (partly: button ‘Start’, MMI_gen 4381 (partly: change to state ‘Pressed’ as long as remain actuated))); MMI_gen 4375;
            */


            /*
            Test Step 3
            Action: Slide out of ‘Start’ button
            Expected Result: The border of the button is shown (state ‘Enabled’) without a sound
            Test Step Comment: MMI_gen 8349 (partly: MMI_gen 4557 (partly: button ‘Start’, MMI_gen 4382 (partly: state ‘Enabled’ when slide out with force applied, no sound))); MMI_gen 4374;
            */
            // Call generic Check Results Method
            DmiExpectedResults.The_border_of_the_button_is_shown_state_Enabled_without_a_sound(this);


            /*
            Test Step 4
            Action: Slide back into ‘Start’ button
            Expected Result: The button is back to state ‘Pressed’ without a sound
            Test Step Comment: MMI_gen 8349 (partly: MMI_gen 4557 (partly: button ‘Start’, MMI_gen 4382 (partly: state ‘Pressed’ when slide back, no sound))); MMI_gen 4375;
            */
            // Call generic Check Results Method
            DmiExpectedResults.The_button_is_back_to_state_Pressed_without_a_sound(this);


            /*
            Test Step 5
            Action: Release ‘Start’ button
            Expected Result: Verify the following points,DMI displays Default window.Use the log file to confirm that DMI sends out the packet [MMI_DRIVER_REQUEST (EVC-101)] with variable [MMI_DRIVER_REQUEST (EVC-101).MMI_M_REQUEST] = 9 (Start)
            Test Step Comment: (1) MMI_gen 8349 (partly: MMI_gen 4557 (partly: button ‘Start’, MMI_gen 4381 (partly: exit state ‘Pressed’, execute function associated to the button)));(2) MMI_gen 11908;
            */


            /*
            Test Step 6
            Action: Acknowledge ‘SR’ mode.Then, press ‘Main’ button
            Expected Result: DMI displays Main window.Verify the following points,Menu windowThe Main window is displayed in main area D/F/G.The window title is ‘Main’.The following objects are display in Main window, Enabled Close button (NA11)Window TitleButton 1 with label ‘Start’Button 2 with label ‘Driver ID’Button 3 with label ‘Train data’ Button 5 with label ‘Level’Button 6 with label ‘Train running number’Button 7 with label ‘Shunting’Button 8 with label ‘Non-Leading’Button 9 with label ‘Maintain shunting’Note: See the position of buttons in picture below,LayersThe level of layers in each area of window as follows,Layer 0: Area D, F, G, E10, E11, Y, and ZLayer -1: Area A1, (A2+A3)*, A4, B*, C1, (C2+C3+C4)*, C5, C6, C7, C8, C9, E1, E2, E3, E4, (E5-E9)*.Layer -2: Area B3, B4, B5, B6 and B7.Note: ‘*’ symbol is mean that specified area are drawn as one area
            Test Step Comment: (1) MMI_gen 8349 (partly: MMI_gen 7909);(2) MMI_gen 8350; MMI_gen 4360 (partly: window title);(3) MMI_gen 8349 (partly: MMI_gen 4556 (partly: Close button, Window Title)); MMI_gen 4355 (partly: Buttons, Close button); MMI_gen 8351 (partly: touch screen, button with label, Driver ID, Train data, Level, Train Running Number, Shunting, Non-Leading, Maintain shunting); MMI_gen 4392 (partly: [Close] NA11);(4) MMI_gen 8349 (partly: MMI_gen 4630, MMI gen 5944 (partly: touch screen));
            */


            /*
            Test Step 7
            Action: Follow action step 2 – step 5. Then, close an opened window respectively for the following button.‘Driver ID’ button.‘Level’ button.‘Train data’ button.‘Train running number’ button
            Expected Result: See the expected results of Step 2 – Step 5 and the following additional information,DMI displays corresponding window refer to released button.Use the log file to confirm that DMI sends out the packet EVC-101 with variable according to the actuated buttons,Driver ID buttonMMI_M_REQUEST= 20 (Change Driver Identity)Level buttonMMI_M_REQUEST = 27 (Change Level or Inhibit status)Train data buttonMMI_M_REQUEST = 3 (Start Train data entry)Train running number buttonMMI_M_REQUEST = 30 (Change Train running Number)
            Test Step Comment: (1) MMI_gen 8349 (partly: MMI_gen 4557 (partly: button ‘Driver ID’, ‘Level’, ‘Train data’, ‘Train running number’));(2) MMI_gen 2479; MMI_gen 1205; MMI_gen 2490; MMI_gen 9954; 
            */


            /*
            Test Step 8
            Action: Press ‘Shunting’ button
            Expected Result: Verify the following information,The ‘Shunting’ button becomes state ‘Pressed’, then state ‘Enabled’ once the button is immediately released.DMI still displays the Main window.The ‘Click’ sound is played once.Use the log file to confirm that DMI sends EVC-101 twice with different value of MMI_T_BUTTONEVENT and MMI_Q_BUTTON (1 = pressed, 0 = released)
            Test Step Comment: (1) MMI_gen 8354-1 (THR) (partly: button ‘Shunting’, MMI_gen 11450-1 (THR) (partly: Delay-Type button, MMI_gen 4388 (partly: less than the 2 seconds, return to state ‘Enabled’)));(2) MMI_gen 8354-1 (THR) (partly: button ‘Shunting’, MMI_gen 11450-1 (THR) (partly: Delay-Type button, MMI_gen 4388 (partly: less than the 2 seconds, no valid button activation considered by onboard)));(3) MMI_gen 8354-1 (THR) (partly: button ‘Shunting’, MMI_gen 11450-1 (THR) (partly: Delay-Type button, MMI_gen 4388 (partly: the sound for button Delay-Type))); MMI_gen 9512, MMI_gen 968;(4) MMI_gen 8354-1 (THR) (partly: button ‘Shunting’, MMI_gen 11450-1 (THR) (partly: send events of Pressed and Released independently to ETCS), MMI_gen 11907 (partly: EVC-101, timestamp)); MMI_gen 3375;
            */


            /*
            Test Step 9
            Action: Press and hold ‘Shunting’ button for 2s.Note: Stopwatch is required for accuracy of test result
            Expected Result: Verify the following information,While press and hold button less than 2 secThe ‘Click’ sound is played once.The state of button is changed to ‘Pressed’.The state ‘pressed’ and ‘enabled’ are switched repeatly while button is pressed. Use the log file to confirm that DMI sends EVC-101 with variable MMI_T_BUTTONEVENT and MMI_Q_BUTTON = 1 (pressed).While press and hold button over 2 secThe state of button is changed to ‘Pressed’ and without toggle
            Test Step Comment: (1)  MMI_gen 8354-1 (THR) (partly: button ‘Shunting’, MMI_gen 11450-1 (THR) (partly: Delay-Type button, MMI_gen 4388 (partly: the sound for button Delay-Type))); MMI_gen 9512; MMI_gen 968;(2) MMI_gen 8354-1 (THR) (partly: button ‘Shunting’, MMI_gen 11450-1 (THR) (partly: Delay-Type button, MMI_gen 4388 (partly: change to pressed state)));(3) MMI_gen 8354-1 (THR) (partly: button ‘Shunting’, MMI_gen 11450-1 (THR) (partly: Delay-Type button, MMI_gen 4388 (partly: toggle between the “pressed” and “enabled” states as long as the button remains pressed by the driver)));(4) MMI_gen 8354-1 (THR) (partly: button ‘Shunting’, MMI_gen 11450-1 (THR) (partly: send events of Pressed independently to ETCS), MMI_gen 11907 (partly: EVC-101, timestamp))); MMI_gen 3375;(5) MMI_gen 8354-1 (THR) (partly: button ‘Shunting’, MMI_gen 11450-1 (THR) (partly: Delay-Type button, MMI_gen 4388 (partly: after 2 seconds, the button is change again to the state ‘Pressed’)));
            */
            // Call generic Check Results Method
            DmiExpectedResults
                .Verify_the_following_information_While_press_and_hold_button_less_than_2_secThe_Click_sound_is_played_once_The_state_of_button_is_changed_to_Pressed_The_state_pressed_and_enabled_are_switched_repeatly_while_button_is_pressed_Use_the_log_file_to_confirm_that_DMI_sends_EVC_101_with_variable_MMI_T_BUTTONEVENT_and_MMI_Q_BUTTON_1_pressed_While_press_and_hold_button_over_2_secThe_state_of_button_is_changed_to_Pressed_and_without_toggle(this);


            /*
            Test Step 10
            Action: Slide out from the “Shunting” button
            Expected Result: Verify the following information,The ‘Shunting’ button turns to the ‘Enabled’ state without a sound
            Test Step Comment: (1) MMI_gen 8354-1 (THR) (partly: button ‘Shunting’, MMI_gen 11450-1 (THR) (partly: Delay-Type button, MMI_gen 4389 (partly: state ‘Enabled’ when slide out with force applied, stop toggling state ‘Pressed’ and ‘Enabled’, no sound))); 
            */
            // Call generic Check Results Method
            DmiExpectedResults
                .Verify_the_following_information_The_Shunting_button_turns_to_the_Enabled_state_without_a_sound(this);


            /*
            Test Step 11
            Action: Slide back to the “Shunting” button and hold it for 1 seconds. Then, slide out again.Note: Stopwatch is required for accuracy of test result
            Expected Result: Verify the following information,The ‘Shunting’ button turns to the ‘Enabled’ state without a sound
            Test Step Comment: (1) MMI_gen 8354-1 (THR) (partly: button ‘Shunting’, MMI_gen 11450-1 (THR) (partly: Delay-Type button, MMI_gen 4388 (partly: to reset toggling state ‘Pressed’ and ‘Enabled’, no sound), MMI_gen 4389 (partly: to reset toggling state ‘Pressed’ and ‘Enabled’, no sound, Slide back)));
            */
            // Call generic Check Results Method
            DmiExpectedResults
                .Verify_the_following_information_The_Shunting_button_turns_to_the_Enabled_state_without_a_sound(this);


            /*
            Test Step 12
            Action: Slide back to the “Shunting” button and hold it for 2 seconds.Note: Stopwatch is required for accuracy of test result
            Expected Result: While press and hold button less than 2 secThe state ‘pressed’ and ‘enabled’ are switched repeatly while button is pressed without a sound. While press and hold button over 2 secThe state of button is changed to ‘Pressed’ and without toggle
            Test Step Comment: (1) MMI_gen 8354-1 (THR) (partly: button ‘Shunting’, MMI_gen 11450-1 (THR) (partly: Delay-Type button, MMI_gen 4389 (partly: start toggling state ‘Pressed’ and ‘Enabled’ when slide back, no sound)));(2) MMI_gen 8354-1 (THR) (partly: button ‘Shunting’, MMI_gen 11450-1 (THR) (partly: Delay-Type button, MMI_gen 4388 (partly: after 2 seconds, the button is change again to the state ‘Pressed’))); MMI_gen 4389 (partly:Main Window, 2 seconds timer);
            */
            // Call generic Check Results Method
            DmiExpectedResults
                .While_press_and_hold_button_less_than_2_secThe_state_pressed_and_enabled_are_switched_repeatly_while_button_is_pressed_without_a_sound_While_press_and_hold_button_over_2_secThe_state_of_button_is_changed_to_Pressed_and_without_toggle(this);


            /*
            Test Step 13
            Action: Release ‘Shunting’ button
            Expected Result: DMI displays Default window in SH mode, Level 1.Verify the following information,Use the log file to confirm that DMI sends EVC-101 with the following variable,MMI_Q_BUTTON = 0 (Released) MMI_M_REQUEST = 1 (Start Shunting) MMI_T_BUTTONEVENT is not blank.Use the log file to confirm that DMI receives EVC-7 with variable OBU_TR_M_MODE = 3 (SH – Shunting)
            Test Step Comment: (1) MMI_gen 8354-1 (THR) (partly: button ‘Shunting’, MMI_gen 11450-1 (THR) (partly: send events of Released to ETCS, MMI_gen 4388 (partly: after 2 seconds button Up-Type is followed, button Delay-Type, MMI_gen 4381 (partly: exit state ‘Pressed’, execute function associated to the button)))); MMI_gen 11907 (partly: EVC-101, timestamp)); MMI_gen 11909; MMI_gen 3375;(2) MMI_gen 1423 (partly: DMI equipped with TS, current mode SH) ;
            */


            /*
            Test Step 14
            Action: Press ‘Main’ button
            Expected Result: Verify the following information,The ‘Shunting’ button is replaced with the ‘Exit Shunting’ button.Use the log file to confirm that DMI receives EVC-30 with the following value in variable MMI_Q_REQUEST_ENABLE_64MMI_Q_REQUEST_ENABLE_64 (#0) = 0 (Start)MMI_Q_REQUEST_ENABLE_64 (#1) = 1 (Driver ID)MMI_Q_REQUEST_ENABLE_64 (#2) = 0 (Train Data)MMI_Q_REQUEST_ENABL15E_64 (#3) = 0 (Level)MMI_Q_REQUEST_ENABLE_64 (#4) = 0 (Train running number)MMI_Q_REQUEST_ENABLE_64 (#5) = 0 (Shunting)MMI_Q_REQUEST_ENABLE_64 (#6) = 1 (Exit Shunting)MMI_Q_REQUEST_ENABLE_64 (#7) = 0 (Non-Leading)MMI_Q_REQUEST_ENABLE_64 (#8) = 0 (Maintain Shunting)The following buttons are shown with a border and its text is coloured Dark-Grey:The ‘Start’ buttonThe ‘Train data’ buttonThe ‘Level’ buttonThe ‘Train running number’ buttonThe ‘Non-Leading’ buttonThe ‘Maintain Shunting’ button
            Test Step Comment: (1) MMI_gen 1423 (partly: DMI equipped with TS, replace)(2) MMI_gen 591 (partly: enabling condition of ‘Exit Shunting’ button, disabling condition of ‘Start’, ‘Train data’, ‘Level’, ‘Train running number’ and ‘Shunting’ buttons); (3) MMI_gen 591 (partly: enabling condition of ‘Exit Shunting’ button, disabling condition of ‘Start’, ‘Train data’, ‘Level’, ‘Train running number’ and ‘Shunting’ buttons”); MMI_gen 4377 (partly: shown);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press ‘Main’ button");


            /*
            Test Step 15
            Action: Simulate the ‘Passive-Shunting’ signal by activating the ‘Passive-Shunting’ checkbox on OTE
            Expected Result: Verify the following information,The state of ‘Maintain Shunting’ button is changed to enabled.Use the log file to confirm that DMI receives EVC-30 with the MMI_Q_REQUEST_ENABLE_64 (#8) = 1 (Maintain Shunting)
            Test Step Comment: (1) MMI_gen 8351 (partly: enabling condition of ‘Maintain Shunting’ button);(2) MMI_gen 591 (partly: enabling condition of ‘Maintain Shunting’ button);
            */


            /*
            Test Step 16
            Action: Perform action follow step 8 – step 14 for the ‘Maintain Shunting’ button
            Expected Result: See the expected results of Step 8 – Step 14 and the following additional information,DMI displays Default window after button is released from action step 13.Use the log file to confirm that DMI sends EVC-101 with the following variable,MMI_Q_BUTTON = 0 (Released) MMI_M_REQUEST = 14 (Continue shunting on desk closure) MMI_T_BUTTONEVENT is not blank
            Test Step Comment: (1) MMI_gen 8354-1 (THR) (partly: button’ Maintain Shunting’); MMI_gen 11236 (partly: Additionally);(2) MMI_gen 11907 (partly: EVC-101, Maintain Shunting); MMI_gen 11236 (partly: released, pressed, MMI_Q_BUTTON, EVC-101); MMI_gen 3375;
            */


            /*
            Test Step 17
            Action: Perform action follow step 8 – step 13 for the ‘Exit Shunting’ button
            Expected Result: See the expected results of Step 8 – Step 13 and the following additional information,DMI displays Driver ID window in SB mode, Level 1.Use the log file to confirm that DMI sends EVC-101 with the following variable,MMI_Q_BUTTON = 0 (Released) MMI_M_REQUEST = 2 (Exit Shunting)MMI_T_BUTTONEVENT is not blank
            Test Step Comment: (1) MMI_gen 8354-1 (THR) (partly: button’ Exit Shunting’);(2) MMI_gen 11910 (partly: EVC-101, Exit Shunting); MMI_gen 3375;
            */


            /*
            Test Step 18
            Action: Perform the following procedure,Enter Driver IDSelect and confirm Level 1. Note: If Level window is display
            Expected Result: DMI displays Main window.Verify the following information,Use the log file to confirm that DMI receives EVC-30 with the following value in variable MMI_Q_REQUEST_ENABLE_64MMI_Q_REQUEST_ENABLE_64 (#0) = 0 (Start)MMI_Q_REQUEST_ENABLE_64 (#1) = 1 (Driver ID)MMI_Q_REQUEST_ENABLE_64 (#2) = 1 (Train data)MMI_Q_REQUEST_ENABLE_64 (#3) = 1 (Level)MMI_Q_REQUEST_ENABLE_64 (#4) = 1 (Train running number)MMI_Q_REQUEST_ENABLE_64 (#5) = 1 (Shunting)MMI_Q_REQUEST_ENABLE_64 (#6) = 0 (Exit Shunting)MMI_Q_REQUEST_ENABLE_64 (#7) = 0 (Non-Leading)MMI_Q_REQUEST_ENABLE_64 (#8) = 0 (Maintain Shunting)
            Test Step Comment: (1) MMI_gen 591 (partly: EVC-30, disabling condition of ‘Exit Shunting’ button, enabling condition of ‘Train data’, ‘Level’, ‘Train running number’ and ‘Shunting’ buttons);
            */
            // Call generic Action Method
            DmiActions
                .Perform_the_following_procedure_Enter_Driver_IDSelect_and_confirm_Level_1_Note_If_Level_window_is_display(this);


            /*
            Test Step 19
            Action: Simulates the ‘Non-leading’ signal by activating the ‘Non-leading’ checkbox on OTE
            Expected Result: Verify the following information,The state of ‘Non-leading’ button is changed to enabled.Use the log file to confirm that DMI receives EVC-30 with the MMI_Q_REQUEST_ENABLE_64 (#7) = 1 (Non-Leading)
            Test Step Comment: (1) MMI_gen 8351 (partly: enabling condition of ‘Non-leading’ button);(2) MMI_gen 591 (partly: enabling condition of ‘Non-leading’ button);
            */


            /*
            Test Step 20
            Action: Perform action follow step 8 – step 14 for the ‘Non-leading’ button
            Expected Result: See the expected results of Step 8 – Step 14 and the following additional information,DMI displays Default window after button is released from action step 13.Use the log file to confirm that DMI sends EVC-101 with the following variable,MMI_Q_BUTTON = 0 (Released) MMI_M_REQUEST = 5 (Start Non-Leading) MMI_T_BUTTONEVENT is not blank
            Test Step Comment: (1) MMI_gen 8354-1 (THR) (partly: button’ Non Leading);(2) MMI_gen 11911 (partly: EVC-101, Non Leading); MMI_gen 3375;
            */


            /*
            Test Step 21
            Action: Remove the ‘Non-leading’ signal by de-activating the ‘Non-leading’ checkbox on OTE.Then, perform the following procedure,Enter Driver IDSelect and confirm Level 1. Note: If Level window is display
            Expected Result: DMI displays Main window.Verify the following information,Use the log file to confirm that DMI receives EVC-30 with the MMI_Q_REQUEST_ENABLE_64 (#7) = 0 (Non-Leading)The state of ‘Non-leading’ button is disabled
            Test Step Comment: (1) MMI_gen 591 (partly: disabling condition of ‘Non-leading’ button);(2) MMI_gen 591 (partly: disabling condition of ‘Non-leading’ button);
            */


            /*
            Test Step 22
            Action: Use the test script file 7_1_a.xml to send EVC-30 with,MMI_Q_REQUEST_ENABLE_64 (#1) = 0MMI_NID_WINDOW = 1
            Expected Result: Verify that the ‘Drive ID’ button is disabled
            Test Step Comment: MMI_gen 591 (partly: disabling condition of ‘Driver ID’ button);
            */


            /*
            Test Step 23
            Action: Use the test script file 7_1_b.xml to send EVC-30 with,MMI_Q_REQUEST_ENABLE_64 (#1) = 1MMI_NID_WINDOW = 1
            Expected Result: Verify that the ‘Drive ID’ button is enabled
            Test Step Comment: MMI_gen 591 (partly: enabling condition of ‘Driver ID’ button);
            */


            /*
            Test Step 24
            Action: Follow action step 2 – step 5 for the ‘Close’ button
            Expected Result: See the expected results of Step 2 – Step 5 and the following additional information,  (1) DMI displays Default window refer to released button
            Test Step Comment: (1) MMI_gen 8349 (partly: MMI_gen 4557 (partly: button ‘Close’)); MMI_gen 4392 (partly: returning to the parent window);
            */


            /*
            Test Step 25
            Action: End of test
            Expected Result: 
            */


            return GlobalTestResult;
        }
    }
}
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
    /// 27.20.1 Override window: General appearance
    /// TC-ID: 22.20.1
    /// 
    /// This test case verifies the display of the ‘Override’ window on DMI that shall comply with [ERA-ERTMS] standard and [MMI-ETCS-gen].
    /// 
    /// Tested Requirements:
    /// MMI_gen 8415 (partly: touch screen, label “EOA”); MMI_gen 11225; MMI_gen 8413 (partly: MMI_gen 7909, MMI_gen 4630, MMI gen 5944 (partly: touch screen), MMI_gen 4556 (partly: Close button, Window Title)); MMI_gen 8414; MMI_gen 11415 (partly: MMI_gen 11387 (partly: button Up-Type, send events of Pressed independently to ETCS), MMI_gen 4381, MMI_gen 4382); MMI_gen 9512; MMI_gen 968; MMI_gen 11907 (partly: EVC-101, timestamp), MMI_gen 11226; MMI_gen 11231; MMI_gen 4360 (partly: window title); MMI_gen 4355 (partly: Buttons, Close button); MMI_gen 4392 (partly: [Close] NA11, returning to the parent window); MMI_gen 3375; MMI_gen 4350; MMI_gen 4351; MMI_gen 4353; MMI_gen 4354;
    /// 
    /// Scenario:
    /// The concerned buttons in the ‘Override window are verified by the following actions:Press the button and holdSlide the button out with force appliedSlide the button back with force appliedRelease the buttonSoM is performed until Train Running number is confirmed and, then, the ‘Main’ window is closed.The disabled state of  ‘EOA’ button is verified.SoM is completed in mode SR, level 
    /// 1.Then, the ‘Main’ window is closed.The ‘Override’ window is opened and verified.The Safe-up-Type ‘EOA’ button is verified.Perform action for making DMI entered mode SB, level 
    /// 1.Then, the ‘EOA’ button is verified.
    /// 
    /// Used files:
    /// N/A
    /// </summary>
    public class Override_window_General_appearance : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // Test system is powered on.The cabin is activated.SoM is performed until Train Running number is confirmed.

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
            Action: Perform the following procedure,Press ‘Close’ button.Press ‘Override’ button
            Expected Result: Verify the following information,The ‘EOA’ button is in disable state.Use the log file to confirm that DMI receives EVC-30 with with bit No.9 of variable MMI_Q_REQUEST_ENABLE_64 = 0 (Disable Start Override EOA)
            Test Step Comment: (1) MMI_gen 8415 (partly: touch screen, label “EOA”);              MMI_gen 11225 (partly: EVC-30, disabled);(2) MMI_gen 11225 (partly: disalbed);
            */
            // Call generic Check Results Method
            DmiExpectedResults
                .Verify_the_following_information_The_EOA_button_is_in_disable_state_Use_the_log_file_to_confirm_that_DMI_receives_EVC_30_with_with_bit_No_9_of_variable_MMI_Q_REQUEST_ENABLE_64_0_Disable_Start_Override_EOA(this);


            /*
            Test Step 2
            Action: Perform the following procedure,Press ‘Close’ button.Press ‘Main’ button
            Expected Result: DMI displays Main window
            */
            // Call generic Check Results Method
            DmiExpectedResults.DMI_displays_Main_window(this);


            /*
            Test Step 3
            Action: Press ‘Start’ button.Then, acknowledge ‘SR’ mode
            Expected Result: DMI displays Default window in SR mode, Level 1.Use the log file to confirm that DMI receives EVC-30 with with bit No.9 of variable MMI_Q_REQUEST_ENABLE_64 = 1 (Enable Start Override EOA)
            Test Step Comment: (1) MMI_gen 11225 (partly: EVC-30, enabled);
            */


            /*
            Test Step 4
            Action: Press ‘Override’ button
            Expected Result: DMI displays Override windowVerify the following points,Menu windowThe Override window is displayed in main area D/F/G. The window title is ‘Override’.The following objects are displayed in Main window, Enabled Close button (NA11).Window TitleButton 1 with label ‘EOA’Note: See the position of buttons in picture below,The ‘EOA’ button is in enable state.LayersThe level of layers in each area of window as follows,Layer 0: Area D, F, G, E10, E11, Y, and ZLayer -1: Area A1, (A2+A3)*, A4, B*, C1, (C2+C3+C4)*, C5, C6, C7, C8, C9, E1, E2, E3, E4, (E5-E9)*.Layer -2: Area B3, B4, B5, B6 and B7.Note: ‘*’ symbol is mean that specified areas are drawn as one area.General property of windowThe Override window is presented with objects and buttons which is the one of several levels and allocated to areas of DMI.All objects, text messages and buttons are presented within the same layer.The Default window is not displayed and covered the current window.Sub-level window covers partially depending on the size of the Sub-Level window. There is no other window is displayed and activated at the same time
            Test Step Comment: (1) MMI_gen 8413 (partly: MMI_gen 7909);    (2) MMI_gen 8414;  MMI_gen 4360 (partly: window title);(3) MMI_gen 8413 (partly: MMI_gen 4556 (partly: Close button, Window Title)); MMI_gen 8415 (partly: touch screen); MMI_gen 4392 (partly: [Close] NA11); MMI_gen 4355 (partly: Buttons, Close button);(4) MMI_gen 11225 (partly: enabled);(5) MMI_gen 8413 (partly: MMI_gen 4630, MMI gen 5944 (partly: touch screen));   (6) MMI_gen 4350;(7) MMI_gen 4351;(8) MMI_gen 4353;(9) MMI_gen 4354;
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(@"Press ‘Override’ button");


            /*
            Test Step 5
            Action: Press and hold ‘EOA’ button
            Expected Result: DMI displays the Override window.The sound ‘Click’ is played once.The ‘EOA’ button is shown as the ‘Pressed’ state, the border of button is removed. Use the log file to confirm that DMI sends EVC-101 with variable MMI_M_REQUEST = 7 (Start Override EOA (Pass stop) and MMI_T_BUTTONEVENT is not blank
            Test Step Comment: (1) MMI_gen 11415 (partly: MMI_gen 11387 (partly: button Up-Type, MMI_gen 4381 (partly: the sound for Up-Type button))), MMI_gen 9512, MMI_gen 968;(2) MMI_gen 11415 (partly: MMI_gen 11387 (partly: button Up-Type, MMI_gen 4381 (partly: change to state ‘Pressed’ as long as remain actuated))); MMI_gen 4375;(3) MMI_gen 11415 (partly: MMI_gen 11387 (partly: send events of Pressed independently to ETCS), MMI_gen 11907 (partly: EVC-101, timestamp)), MMI_gen 11226 (partly: EVC-101); MMI_gen 3375;
            */


            /*
            Test Step 6
            Action: Slide out of ‘EOA’ button
            Expected Result: The border of the button is shown (state ‘Enabled’) without a sound
            Test Step Comment: MMI_gen 11415 (partly: MMI_gen 11387 (partly: button Up-Type, MMI_gen 4382 (partly: state ‘Enabled’ when slide out with force applied, no sound))); MMI_gen 4374;
            */
            // Call generic Check Results Method
            DmiExpectedResults.The_border_of_the_button_is_shown_state_Enabled_without_a_sound(this);


            /*
            Test Step 7
            Action: Slide back into ‘EOA’ button
            Expected Result: The button is back to state ‘Pressed’ without a sound
            Test Step Comment: MMI_gen 11415 (partly: MMI_gen 11387 (partly: button Up-Type, MMI_gen 4382 (partly: state ‘Pressed’ when slide back, no sound))); MMI_gen 4375;
            */
            // Call generic Check Results Method
            DmiExpectedResults.The_button_is_back_to_state_Pressed_without_a_sound(this);


            /*
            Test Step 8
            Action: Release ‘EOA’ button
            Expected Result: Verify the following information,DMI displays the ‘Default’ window.Use the log file to confirm that DMI sends EVC-101 with variable MMI_M_REQUEST = 7 (Start Override EOA (Pass stop)) and MMI_T_BUTTONEVENT is not blank.Use the log file to confirm that DMI receives EVC-2 with variable MMI_M_OVERRIDE_EOA = 1 (function is active)  and DMI displays symbol ‘Override’ MO03 in sub-area C7
            Test Step Comment: (1) MMI_gen 11415 (partly: MMI_gen 11387 (partly: button Up-Type, MMI_gen 4381 (partly: exit state ‘Pressed’, execute function associated to the button))), MMI_gen 11226 (partly: closure, parent window);(2) MMI_gen 11415 (partly: MMI_gen 11387 (partly: send events of Released independently to ETCS), MMI_gen 11907 (partly: EVC-101, timestamp)), MMI_gen 11226 (partly: EVC-101); MMI_gen 3375;(3) MMI_gen 11231;
            */


            /*
            Test Step 9
            Action: Perform the following procedure, Press ‘Main’ buttonPress and hold ‘Shunting’ button up to 2 secondRelease ‘Shunting’ button
            Expected Result: DMI displays Default window in SH mode, Level 1
            */


            /*
            Test Step 10
            Action: Perform the following procedure,Press ‘Main’ buttonPress and hold ‘Exit Shunting’ button up to 2 secondRelease ‘Exit Shunting’ buttonEnter Driver IDClose the ‘Main’ window
            Expected Result: DMI displays Default window in SB mode, Level 1
            */


            /*
            Test Step 11
            Action: Press ‘Override’ button
            Expected Result: Verify the following information,The ‘EOA’ button is in disable state.Use the log file to confirm that DMI receives EVC-30 with with bit No.9 of variable MMI_Q_REQUEST_ENABLE_64 = 0 (Disable Start Override EOA)
            Test Step Comment: (1) MMI_gen 8415 (partly: touch screen, label “EOA”);              MMI_gen 11225 (partly: EVC-30, disabled);(2) MMI_gen 11225 (partly: disabled);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(@"Press ‘Override’ button");
            // Call generic Check Results Method
            DmiExpectedResults
                .Verify_the_following_information_The_EOA_button_is_in_disable_state_Use_the_log_file_to_confirm_that_DMI_receives_EVC_30_with_with_bit_No_9_of_variable_MMI_Q_REQUEST_ENABLE_64_0_Disable_Start_Override_EOA(this);


            /*
            Test Step 12
            Action: Press ‘Close’ button
            Expected Result: Verify the following information,(1)   DMI displays Default window
            Test Step Comment: (1) MMI_gen 4392 (partly: returning to the parent window);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(@"Press ‘Close’ button");
            // Call generic Check Results Method
            DmiExpectedResults.Verify_the_following_information_1_DMI_displays_Default_window(this);


            /*
            Test Step 13
            Action: End of test
            Expected Result: 
            */


            return GlobalTestResult;
        }
    }
}
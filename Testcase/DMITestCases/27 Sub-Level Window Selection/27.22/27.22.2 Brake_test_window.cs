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
    /// 27.22.2 Brake test window
    /// TC-ID: 22.22.2 
    /// 
    /// This test case verifies the display of the ‘Brake Test’ window and its buttons.
    /// 
    /// Tested Requirements:
    /// MMI_gen 11808; MMI_gen 11809; MMI_gen 11811; MMI_gen 11807 (partly: MMI_gen 7909, MMI_gen 4557, MMI_gen 4381, MMI_gen 4382, MMI_gen 9512, MMI_gen 968, MMI_gen 4556 (partly: Close button, Window Title, Button 1), MMI_gen 4630, MMI gen 5944 (partly: touch screen)); MMI_gen 4360 (partly: window title); MMI_gen 4375; MMI_gen 4374; MMI_gen 4392 (partly: [Close] NA11, returning to the parent window); MMI_gen 4350; MMI_gen 4351; MMI_gen 4353;
    /// 
    /// Scenario:
    /// The concerned buttons in the ‘Brake Test’ window are verified by the following actions:Press the button and holdSlide the button out with force appliedSlide the button back with force appliedRelease the buttonThe Brake Test window appearance is verified.The Up-Type button of ‘ETCS’, ‘Close’ buttons are verified.
    /// 
    /// Used files:
    /// N/A
    /// </summary>
    public class Brake_test_window : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // System is powered ON.Cabin is activated.SoM is performed until Level 1 is selcted and confirmed.Settings window is opened.Brake button is enabled.Brake window is opened.

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
            Action: Press ‘Test’ button
            Expected Result: DMI displays Brake test window.Verifies the following information,Menu windowThe Brake Test window is displayed in main area D/F/G.The window title is ‘Brake test’.The following objects are displayed in Main window, Enabled Close button (NA11)Window TitleButton 1 with label ‘ETCS’Note: See the position of buttons in picture below,LayersThe level of layers in each area of window as follows,Layer 0: Area D,F,GLayer -1: Area A1, (A2+A3)*, A4, B*, C1, (C2+C3+C4)*, C5, C6, C7, C8, C9, E1, E2, E3, E4, (E5-E9)*.Layer -2: Area B3, B4, B5, B6 and B7.Note: ‘*’ symbol is mean that specified area are drawn as one area.General property of windowThe Brake Test window is presented with objects and buttons which is the one of several levels and allocated to areas of DMI.All objects, text messages and buttons are presented within the same layer.The Default window is not displayed and covered the current window
            Test Step Comment: (1) MMI_gen 11807 (partly: MMI_gen 7909);(2) MMI_gen 11808; MMI_gen 4360 (partly: window title);(3) MMI_gen 11807 (partly: MMI_gen 4556 (partly: Close button, Window Title, Button 1)); MMI_gen 11809; MMI_gen 4392 (partly: [Close] NA11);          (4) MMI_gen 11807 (partly: MMI_gen 4630, MMI gen 5944 (partly: touch screen));(5) MMI_gen 4350;(6) MMI_gen 4351;(7) MMI_gen 4353;
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press ‘Test’ button");


            /*
            Test Step 2
            Action: Press and hold ‘Close’ button
            Expected Result: Verify the following information,The sound ‘Click’ played once.The ‘Close’ button is shown as pressed state, the border of button is removed
            Test Step Comment: (1) MMI_gen 11807 (partly: MMI_gen 4557 (partly: button ‘Close), MMI_gen 4381 (partly: the sound for Up-Type button)); MMI_gen 9512; MMI_gen 968;(2) MMI_gen 11807 (partly: partly: MMI_gen 4557 (partly: button ‘Close’), MMI_gen 4381 (partly: change to state ‘Pressed’ as long as remain actuated)); MMI_gen 4375;
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press and hold ‘Close’ button");


            /*
            Test Step 3
            Action: Slide out ‘Close’ button
            Expected Result: The border of the button is shown (state ‘Enabled’) without a sound
            Test Step Comment: MMI_gen 11807 (partly: MMI_gen 4557 (partly: button ‘Close’, MMI_gen 4382 (partly: state ‘Enabled’ when slide out with force applied, no sound))); MMI_gen 4374;
            */
            // Call generic Action Method
            DmiActions.Slide_out_Close_button(this);
            // Call generic Check Results Method
            DmiExpectedResults.The_border_of_the_button_is_shown_state_Enabled_without_a_sound(this);


            /*
            Test Step 4
            Action: Slide back into ‘Close’ button
            Expected Result: The button is back to state ‘Pressed’ without a sound
            Test Step Comment: MMI_gen 11807 (partly: MMI_gen 4557 (partly: button ‘Close’, MMI_gen 4382 (partly: state ‘Pressed’ when slide back, no sound))); MMI_gen 4375;
            */
            // Call generic Action Method
            DmiActions.Slide_back_into_Close_button(this);
            // Call generic Check Results Method
            DmiExpectedResults.The_button_is_back_to_state_Pressed_without_a_sound(this);


            /*
            Test Step 5
            Action: Release ‘Close’ button
            Expected Result: DMI displays Brake window
            Test Step Comment: MMI_gen 11807 (partly: MMI_gen 4557 (partly: button ‘Close’, MMI_gen 4381 (partly: exit state ‘Pressed’, execute function associated to the button))); MMI_gen 4392 (partly: returning to the parent window);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Release ‘Close’ button");
            // Call generic Check Results Method
            DmiExpectedResults.DMI_displays_Brake_window(this);


            /*
            Test Step 6
            Action: Press ‘Test’ button
            Expected Result: DMI displays Brake test window
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press ‘Test’ button");


            /*
            Test Step 7
            Action: Follow action step 2 – step 5 for ‘ETCS’ button
            Expected Result: See the expected results of Step 2 – Step 5 and the following additional information,DMI close the Brake Test window and displays Brake window instead.Use the log file to confirm that DMI sends out the packet [MMI_DRIVER_REQUEST (EVC-101)] with variable [MMI_DRIVER_REQUEST (EVC-101).MMI_M_REQUEST] = 22 (Start Brake Test)
            Test Step Comment: (1) MMI_gen 11807 (partly: MMI_gen 4557 (partly: button ‘Percentage’));                                   MMI_gen 11811 (partly: close the window and open ‘Brake’window);(2) MMI_gen 11811 (partly: EVC-101);
            */


            /*
            Test Step 8
            Action: End of test
            Expected Result: 
            */


            return GlobalTestResult;
        }
    }
}
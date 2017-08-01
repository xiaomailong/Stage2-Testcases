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
    /// 27.26 System info window
    /// TC-ID: 22.26
    /// 
    /// This test case verifies the display information of System info window refer to chapter 6.5.6.4 of requirement specification document and verifies the value of displayed information is correct refer to received data from packet sending EVC-24.
    /// 
    /// Tested Requirements:
    /// MMI_gen 11902 (partly:MMI_gen 5338, MMI_gen 5306 (partly: Object in table 21),  MMI_gen 5383 (partly: MMI_gen 5944 (partly: touchscreen)), MMI_gen 5337, MMI_gen 5340 (partly: right aligned), MMI_gen 5342 (partly: left aligned), MMI_gen 5336 (partly: valid), MMI_gen 5335, MMI_gen 7510); MMI_gen 11903; MMI_gen 1552; MMI_gen 1551; MMI_gen 2463; MMI_gen 11695; MMI_gen 4392 (partly: [Previous : NA19], [Next: NA17], [Close] NA11, returning to the parent window); MMI_gen 4394 (partly: next, previous); MMI_gen 4396 (party: next, previous); MMI_gen 4350; MMI_gen 4351; MMI_gen 4353; MMI_gen 4358; MMI_gen 4360 (partly: total number of window); MMI_gen 9391 (partly: [Next], MMI_gen 4381 (partly: change to state ‘Pressed’ as long as remain actuated));
    /// 
    /// Scenario:
    /// Activate Cabin A.Open Settings window.Open System info window and verify the display information.Close System info window.Use the test scrip file to send data packet and verify the display information.Verify the functionality of Up-type button for the ‘Next’ and ‘Previouse’ buttons.
    /// 
    /// Used files:
    /// 22_26_a.xml, 22_26_b.xml
    /// </summary>
    public class System_info_window : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // System is powered ON.

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
            Action: Activate Cabin A
            Expected Result: DMI displays Driver ID window
            */
            // Call generic Action Method
            DmiActions.Activate_Cabin_A(this);
            // Call generic Check Results Method
            DmiExpectedResults.DMI_displays_Driver_ID_window(this);


            /*
            Test Step 2
            Action: Press Settings button icon. Then, press ‘System info’ button
            Expected Result: DMI displays System info window.Verify the following points below,Data View WindowsThe window is displayed in main area D, F and G.The text colour of Data View is grey.The following objects are displayed on DMI (see example picture in comment column), Enabled Close button (NA11) Enabled Next button (NA17) Disabled Previous button (NA19)Window titleLabelsThe Label part are consisted with an information in Data part.The labels of Data View is right aligned.The data of Data View is left aligned.If the values are valid, the Data Part is displayed the values.The window title is ‘System info’.LayersThe level of layers in each area of window as follows,Layer 0: Area D, F, G, E10, E11, Z, YLayer -1: Area A1, (A2+A3)*, A4, B*, C1, (C2+C3+C4)*, C5, C6, C7, C8, C9, E1, E2, E3, E4, (E5-E9)*.Layer -2: Area B3, B4, B5, B6 and B7.Note: ‘*’ symbol is mean that specified area are drawn as one area.Packet SendingUse the log file to verify that DMI send information EVC-101 with the following variable,MMI_M_REQUEST = 29General property of windowThe System info window is presented with objects, text messages and buttons which is the one of several levels and allocated to areas of DMI.All objects, text messages and buttons are presented within the same layer.The Default window is not displayed and covered the current window.The text label on window title is included with sequence number of the current window (e.g. ‘(1/2)’)
            Test Step Comment: (1) MMI_gen 11902 (partly: MMI_gen 5338); (2) MMI_gen 11902 (partly: MMI_gen 5337);   (3) MMI_gen 11902 (partly: MMI_gen 5306 (partly: Objects in table 21); MMI_gen 4392 (partly: [Previous : NA19], [Next: NA17], [Close] NA11); MMI_gen 4355 (partly: Buttons, Close button); MMI_gen 4396 (partly: Previous, NA19); MMI_gen 4394 (partly: disabled [previous]); (4) MMI_gen 11902 (partly: MMI_gen 5335);  (5) MMI_gen 11902 (partly: MMI_gen 5340 (partly: right aligned));  (6) MMI_gen 11902 (partly: MMI_gen 5342 (partly: left aligned));     (7) MMI_gen 11902 (partly: MMI_gen 5336);  (8) MMI_gen 11903;     (9) MMI_gen 11902 (partly: MMI_gen 5383 (MMI_gen 5944 (partly: touchscreen)); (10) MMI_gen 1552;(11) MMI_gen 4350;(12) MMI_gen 4351;(13) MMI_gen 4353;(14) MMI_gen 4360 (partly: partly: total number of window);
            */


            /*
            Test Step 3
            Action: Press and hold ‘Next’ button
            Expected Result: Verify the following information,(1)   The state of button is changed to ‘Pressed’, the border of button is removed.(2)   The sound ‘Click’ is played once
            Test Step Comment: (1) MMI_gen 9391 (partly: [Next], MMI_gen 4381 (partly: change to state ‘Pressed’ as long as remain actuated));(2) MMI_gen 9391 (partly: [Next], MMI_gen 4381 (partly: sound ‘Click’));
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(@"Press and hold ‘Next’ button");
            // Call generic Check Results Method
            DmiExpectedResults
                .Verify_the_following_information_1_The_state_of_button_is_changed_to_Pressed_the_border_of_button_is_removed_2_The_sound_Click_is_played_once(this);


            /*
            Test Step 4
            Action: Slide out the ‘Next’ button
            Expected Result: Verify the following information,(1)   The border of the button is shown (state ‘Enabled’) without a sound
            Test Step Comment: (1) MMI_gen 9391 (partly: [Next], MMI_gen 4382 (partly: state ‘Enabled’ when slide out with force applied, no sound));
            */
            // Call generic Action Method
            DmiActions.Slide_out_the_Next_button(this);
            // Call generic Check Results Method
            DmiExpectedResults
                .Verify_the_following_information_1_The_border_of_the_button_is_shown_state_Enabled_without_a_sound(this);


            /*
            Test Step 5
            Action: Slide back into the ‘Next’ button
            Expected Result: Verify the following information,(1)   The button is back to state ‘Pressed’ without a sound
            Test Step Comment: (1) MMI_gen 9391 (partly: [Next], MMI_gen 4382 (partly: state ‘Enabled’ when slide out with force applied, no sound));
            */
            // Call generic Action Method
            DmiActions.Slide_back_into_the_Next_button(this);
            // Call generic Check Results Method
            DmiExpectedResults.Verify_the_following_information_1_The_button_is_back_to_state_Pressed_without_a_sound(this);


            /*
            Test Step 6
            Action: Release ‘Next’ button
            Expected Result: Verify the following information,(1)   The state of ‘Previous’ and ‘Next’ button are displayed as follows,   ‘Next’ button is disabled, displays as symbol NA18.2‘Previous’ button is enabled, displays as symbol NA18(2)   The window title is displayed as ‘System window (2/2)’ The scrolling between various windows is not displayed as circular
            Test Step Comment: (1) MMI_gen 4394 (partly: enabled [previous], disabled [next]); MMI_gen 4396 (partly: Next, NA18.2, Previous, NA18);(2) MMI_gen 4358;
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(@"Release ‘Next’ button");


            /*
            Test Step 7
            Action: Perform action step 3-6 for ‘Previous’ button
            Expected Result: See the expected result of step 3-6 and the following points,(1)   The state of ‘Previous’ and ‘Next’ button are displayed as follows,‘Next’ button is enabled, displays as symbol NA17 ‘Previous’ button is enabled, displays as symbol NA19(2)    The window title is displayed as ‘System window (1/2)’ The scrolling between various windows is not displayed as circular
            Test Step Comment: (1) MMI_gen 4394 (partly: enabled [next], disabled [previous]); MMI_gen 4396 (partly: Next, NA17, Previous, NA19);(2) MMI_gen 4358;
            */


            /*
            Test Step 8
            Action: Press ‘Close’ button
            Expected Result: DMI displays Settings window
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(@"Press ‘Close’ button");
            // Call generic Check Results Method
            DmiExpectedResults.DMI_displays_Settings_window(this);


            /*
            Test Step 9
            Action: Use the test script file 22_26_a.xml to send EVC-30 withMMI_Q_REQUEST_ENABLE_64 (#32) = 0
            Expected Result: Verify that ‘System info’ button is disabled
            Test Step Comment: MMI_gen 1551 (partly: disabling);
            */


            /*
            Test Step 10
            Action: Press ‘System info’ button
            Expected Result: Use the log file to verify that there is no packet information EVC-101 with MMI_M_REQUEST = 29 sent from DMI to ETCS
            Test Step Comment: MMI_gen 1552 (partly:pressed disabled button);
            */


            /*
            Test Step 11
            Action: Use the test script file 22_26_b.xml to send EVC-30 withMMI_Q_REQUEST_ENABLE_64 (#32) = 1Then, send EVC-24 with MMI_NID_ENGINE_1 = 1234MMI_M_BRAKE_CONFIG = 55MMI_M_AVAIL_SERVICES = 65535MMI_M_ETC_VER = 16755215
            Expected Result: Verify the state of ‘System info’ button is enabled.DMI displays System info window after received packet EVC-24.The Data part of Data view text is able to display multiples value in same label (see the value of hardware configuration are splitted to be a multiple lines).Verify the display information are displayed correctly refer to received packet as follows,MMI Product version = 255.170.15Vehicle ID = 1234Brake configuration is display only the following items,SB availableSB as RTWRelease TCO @BRTCO feedback OKSoft isolation allowedHardware configuration is display only the following items,MMI 1MMI 2Redundant MMI 1Redundant MMI 2BTM antenna 1BTM antenna 2Radio modem 1Radio modem 2DRUEuroloop BTM(s)
            Test Step Comment: (1) MMI_gen 1551 (partly: enabling); (2) MMI_gen 2463 (partly: open System info window);(3) MMI_gen 11902 (partly:MMI_gen 7510);      (4) MMI_gen 2463 (partly: presentation of data); MMI_gen 11695; MMI_gen 11902 (partly: MMI_gen 5336 (partly: valid));
            */


            /*
            Test Step 12
            Action: End of test
            Expected Result: 
            */


            return GlobalTestResult;
        }
    }
}
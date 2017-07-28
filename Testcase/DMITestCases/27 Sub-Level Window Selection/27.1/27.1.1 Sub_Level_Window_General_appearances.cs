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
    /// 27.1.1 Sub-Level Window: General appearances
    /// TC-ID: 22.1.1
    /// 
    /// This test case verifies the selection and display of the Sub-level window on the ‘Default’ window which comply with [ERA-ERTMS] standard.
    /// 
    /// Tested Requirements:
    /// MMI_gen 2959; MMI_gen 167; MMI_gen 11925; MMI_gen 2956; MMI_gen 11924; MMI_gen 4381; MMI_gen 4382; MMI_gen 9512; MMI_gen 968; MMI_gen 12137; MMI_gen 2955; MMI_gen 2957;
    /// 
    /// Scenario:
    /// Power on DMI without ATP to verify the ‘Settings’ button when DMI has no connection with ATP.Power on ATP together with DMI without cabin activation to verify the ‘Settings’ button when the cabin is not active.Activate the cabin and complete SoM to mode SB, level 1.Close the ‘Main’ window to verify the display of all buttons on the default window by the following actions:Press and hold the buttonSlide out from the buttonSlide back to the buttonRelease the buttonUpdate all languages on DMI to verify the label on all button in different languagePower off ATP to verify the ‘Settings’ button when ATP is down without function Fallback.Activate function Fallback to verify the ‘Settings button when ATP is down with function Fallback.
    /// 
    /// Used files:
    /// N/A
    /// </summary>
    public class Sub_Level_Window_General_appearances : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // N/A

            // Call the TestCaseBase PreExecution
            base.PreExecution();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays is SB, level 1.

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint


            /*
            Test Step 1
            Action: Power on DMI (without ATP)
            Expected Result: Verify the following information,The ‘Main’, ‘Override’, Data View’ and ‘Special’ buttons are invisible.The ‘Settings’ button is visible
            Test Step Comment: (1) MMI_gen 2959       (partly: state inactive, ‘Main’, ‘Override’, ‘Data View’ and ‘Special’);(2) MMI_gen 2959       (partly: enabled ‘Settings’, state inactive);
            */
            // Call generic Check Results Method
            DmiExpectedResults
                .Verify_the_following_information_The_Main_Override_Data_View_and_Special_buttons_are_invisible_The_Settings_button_is_visible();


            /*
            Test Step 2
            Action: Then, power on ATP (without cabin activation)
            Expected Result: Verify the following information,The ‘Main’, ‘Override’, Data View’ and ‘Special’ buttons are invisible.The ‘Settings’ button is visible
            Test Step Comment: (1) MMI_gen 2959       (partly: state inactive, ‘Main’, ‘Override’, ‘Data View’ and ‘Special’);(2) MMI_gen 2959       (partly: enabled ‘Settings’, state inactive);
            */
            // Call generic Check Results Method
            DmiExpectedResults
                .Verify_the_following_information_The_Main_Override_Data_View_and_Special_buttons_are_invisible_The_Settings_button_is_visible();


            /*
            Test Step 3
            Action: Perform the following procedure,Activate Cabin AEnter Driver ID and perform brake testSelect and confirm Level 1
            Expected Result: DMI displays Main window
            */
            // Call generic Action Method
            DmiActions
                .Perform_the_following_procedure_Activate_Cabin_AEnter_Driver_ID_and_perform_brake_testSelect_and_confirm_Level_1();
            // Call generic Check Results Method
            DmiExpectedResults.DMI_displays_Main_window();


            /*
            Test Step 4
            Action: Press ‘Close’ button
            Expected Result: DMI displays Default window.Verifies the following points,The sub-level window is composed of 5 buttons displayed in area F1-F5.The following buttons are all enabled and labeled in English as follows:The ‘Main’ button in area F1The ‘Override’ button in area F2The ‘Data View’ button in area F3The ‘Special’ button in area F4The ‘Settings’ button displaying with symbol SE04 in area F5
            Test Step Comment: (1) MMI_gen 167;(2) MMI_gen 11925, MMI_gen 2956, MMI_gen 2959 (partly: state ‘Active’), MMI_gen 11924
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(@"Press ‘Close’ button");


            /*
            Test Step 5
            Action: Press and hold ‘Main’ button
            Expected Result: Verify the following potins,The ‘Main’ button is shown as pressed state. The sound ‘Click’ played once
            Test Step Comment: (1) MMI_gen 2955 (partly: ‘Main’, MMI_gen 4381 (partly: change to state ‘Pressed’ as long as remain actuated));(2) MMI_gen 2955 (partly: button ‘Main’), MMI_gen 4381 (partly: the sound for Up-Type button)); MMI_gen 9512; MMI_gen 968;
            */


            /*
            Test Step 6
            Action: Slide out of ‘Main’ button
            Expected Result: DMI still displays the Default window.The ‘Main’ button becomes the ‘Enabled’ state without a sound
            Test Step Comment: (1) MMI_gen 2955 (partly: button ‘Main’), MMI_gen 4382 (partly: state ‘Enabled’ when slide out with force applied, no sound));
            */


            /*
            Test Step 7
            Action: Slide back into ‘Main’ button
            Expected Result: DMI still displays the Default window.The Main button is shown as pressed state and no sound ‘Click’ is played
            Test Step Comment: (1) MMI_gen 2955 (partly: button ‘Main’), MMI_gen 4382 (partly: state ‘Pressed’ when slide back, no sound));
            */


            /*
            Test Step 8
            Action: Release the ‘Main’ button
            Expected Result: Verify the following information,DMI displays Main window
            Test Step Comment: (1) MMI_gen 2955 (partly: ‘Main’, MMI_gen 4381 (partly: exit state ‘Pressed’, execute function associated to the button)) , MMI_gen 12137 (partly: Sub-level window, main)
            */
            // Call generic Check Results Method
            DmiExpectedResults.Verify_the_following_information_DMI_displays_Main_window();


            /*
            Test Step 9
            Action: Press ‘Close’ button
            Expected Result: DMI displays Default window
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(@"Press ‘Close’ button");
            // Call generic Check Results Method
            DmiExpectedResults.DMI_displays_Default_window();


            /*
            Test Step 10
            Action: Press and hold ‘Override’ button
            Expected Result: Verify the following potins,The ‘Override’ button is shown as pressed state. The sound ‘Click’ played once
            Test Step Comment: (1) MMI_gen 2955 (partly: ‘Override’, MMI_gen 4381 (partly: change to state ‘Pressed’ as long as remain actuated));(2) MMI_gen 2955 (partly: button ‘Override’), MMI_gen 4381 (partly: the sound for Up-Type button)); MMI_gen 9512; MMI_gen 968;
            */


            /*
            Test Step 11
            Action: Slide out of ‘Override’ button
            Expected Result: DMI still displays the Default window.The ‘Override’ button becomes the ‘Enabled’ state without a sound
            Test Step Comment: (1) MMI_gen 2955 (partly: button ‘Override’), MMI_gen 4382 (partly: state ‘Enabled’ when slide out with force applied, no sound));
            */


            /*
            Test Step 12
            Action: Slide back into ‘Override’ button
            Expected Result: DMI still displays the Default window.The Override button is shown as pressed state and no sound ‘Click’ is played
            Test Step Comment: (1) MMI_gen 2955 (partly: button ‘Override’), MMI_gen 4382 (partly: state ‘Pressed’ when slide back, no sound));
            */


            /*
            Test Step 13
            Action: Release the ‘Override’ button
            Expected Result: Verify the following information,DMI displays Override window
            Test Step Comment: (1) MMI_gen 2955 (partly: ‘Override, MMI_gen 4381 (partly: exit state ‘Pressed’, execute function associated to the button)) , MMI_gen 12137 (partly: Sub-level window, override)
            */


            /*
            Test Step 14
            Action: Press ‘Close’ button
            Expected Result: DMI displays Default window
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(@"Press ‘Close’ button");
            // Call generic Check Results Method
            DmiExpectedResults.DMI_displays_Default_window();


            /*
            Test Step 15
            Action: Press and hold ‘Data view’ button
            Expected Result: Verify the following potins,The ‘Data View’ button is shown as pressed state. The sound ‘Click’ played once
            Test Step Comment: (1) MMI_gen 2955 (partly: ‘Data view’, MMI_gen 4381 (partly: change to state ‘Pressed’ as long as remain actuated));(2) MMI_gen 2955 (partly: button ‘Data view’), MMI_gen 4381 (partly: the sound for Up-Type button)); MMI_gen 9512; MMI_gen 968;
            */


            /*
            Test Step 16
            Action: Slide out of ‘Data view’ button
            Expected Result: DMI still displays the Default window.The ‘Data View’ button becomes the ‘Enabled’ state without a sound
            Test Step Comment: (1) MMI_gen 2955 (partly: button ‘Data view’), MMI_gen 4382 (partly: state ‘Enabled’ when slide out with force applied, no sound));
            */


            /*
            Test Step 17
            Action: Slide back into ‘Data view’ button
            Expected Result: DMI still displays the Default window.The Data View button is shown as pressed state and no sound ‘Click’ is played
            Test Step Comment: (1) MMI_gen 2955 (partly: button ‘Data view), MMI_gen 4382 (partly: state ‘Pressed’ when slide back, no sound));
            */


            /*
            Test Step 18
            Action: Release the ‘Data view’ button
            Expected Result: Verify the following information,DMI displays Data View window
            Test Step Comment: (1) MMI_gen 2955 (partly: ‘Data view’, MMI_gen 4381 (partly: exit state ‘Pressed’, execute function associated to the button)) , MMI_gen 12137 (partly: Sub-level window, data view)
            */


            /*
            Test Step 19
            Action: Press ‘Close’ button
            Expected Result: DMI displays Default window
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(@"Press ‘Close’ button");
            // Call generic Check Results Method
            DmiExpectedResults.DMI_displays_Default_window();


            /*
            Test Step 20
            Action: Press and hold ‘Special’ button
            Expected Result: Verify the following potins,The ‘Special’ button is shown as pressed state. The sound ‘Click’ played once
            Test Step Comment: (1) MMI_gen 2955 (partly: ‘Special’, MMI_gen 4381 (partly: change to state ‘Pressed’ as long as remain actuated));(2) MMI_gen 2955 (partly: button ‘Special’), MMI_gen 4381 (partly: the sound for Up-Type button)); MMI_gen 9512; MMI_gen 968;
            */


            /*
            Test Step 21
            Action: Slide out of ‘Special’ button
            Expected Result: DMI still displays the Default window.The ‘Special’ button becomes the ‘Enabled’ state without a sound
            Test Step Comment: (1) MMI_gen 2955 (partly: button ‘Special’), MMI_gen 4382 (partly: state ‘Enabled’ when slide out with force applied, no sound));
            */


            /*
            Test Step 22
            Action: Slide back into ‘Special’ button
            Expected Result: DMI still displays the Default window.The Special button is shown as pressed state and no sound ‘Click’ is played
            Test Step Comment: (1) MMI_gen 2955 (partly: button ‘Special’), MMI_gen 4382 (partly: state ‘Pressed’ when slide back, no sound));
            */


            /*
            Test Step 23
            Action: Release the ‘Special’ button
            Expected Result: Verify the following information,DMI displays Special window
            Test Step Comment: (1) MMI_gen 2955 (partly: ‘Special’, MMI_gen 4381 (partly: exit state ‘Pressed’, execute function associated to the button)) , MMI_gen 12137 (partly: Sub-level window, special)
            */


            /*
            Test Step 24
            Action: Press ‘Close’ button
            Expected Result: DMI displays Default window
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(@"Press ‘Close’ button");
            // Call generic Check Results Method
            DmiExpectedResults.DMI_displays_Default_window();


            /*
            Test Step 25
            Action: Press and hold ‘Setting’ button
            Expected Result: Verify the following potins,The ‘Settings’ button is shown as pressed state. The sound ‘Click’ played once
            Test Step Comment: (1) MMI_gen 2955 (partly: ‘Setting’, MMI_gen 4381 (partly: change to state ‘Pressed’ as long as remain actuated));(2) MMI_gen 2955 (partly: button ‘Setting’), MMI_gen 4381 (partly: the sound for Up-Type button)); MMI_gen 9512; MMI_gen 968;
            */


            /*
            Test Step 26
            Action: Slide out of ‘Setting’ button
            Expected Result: DMI still displays the Default window.The ‘Settings’ button becomes the ‘Enabled’ state without a sound
            Test Step Comment: (1) MMI_gen 2955 (partly: button ‘Setting’), MMI_gen 4382 (partly: state ‘Enabled’ when slide out with force applied, no sound));
            */


            /*
            Test Step 27
            Action: Slide back into ‘Setting’ button
            Expected Result: DMI still displays the Default window.The Settings button is shown as pressed state and no sound ‘Click’ is played
            Test Step Comment: (1) MMI_gen 2955 (partly: button ‘Setting’), MMI_gen 4382 (partly: state ‘Pressed’ when slide back, no sound));
            */


            /*
            Test Step 28
            Action: Release the ‘Setting’ button
            Expected Result: Verify the following information,DMI displays Settings window
            Test Step Comment: (1) MMI_gen 2955 (partly: ‘Setting’, MMI_gen 4381 (partly: exit state ‘Pressed’, execute function associated to the button)), MMI_gen 12137 (partly: Sub-level window, settings)
            */


            /*
            Test Step 29
            Action: Press ‘Language’ button
            Expected Result: DMI displays Language window
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(@"Press ‘Language’ button");
            // Call generic Check Results Method
            DmiExpectedResults.DMI_displays_Language_window();


            /*
            Test Step 30
            Action: Select and confirm ‘Deutsch’ language
            Expected Result: DMI displays Setting window with changed display refer to selected language
            */


            /*
            Test Step 31
            Action: Press ‘Close’ button
            Expected Result: DMI display Default window.Verify that symbol SE04 still applies for Deutsch
            Test Step Comment: (1) MMI_gen 2957 (partly: Deutsch);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(@"Press ‘Close’ button");


            /*
            Test Step 32
            Action: Press ‘Setting’ button
            Expected Result: DMI displays Setting window
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(@"Press ‘Setting’ button");


            /*
            Test Step 33
            Action: Repeat action setp 30-31 for another language
            Expected Result: Verify the following information,Verify the label of are F5 is presented symbol SE04 independently from the currently active language
            Test Step Comment: (1) MMI_gen 2957;
            */


            /*
            Test Step 34
            Action: Power off ATP (without Fallback)
            Expected Result: Verify the following information,The buttons ‘Main’, ‘Override’, Data View’ and ‘Special’ buttons are invisibled in any other state.The ‘Settings’ button is still visible
            Test Step Comment: (1) MMI_gen 2959 (partly: They are invisible in any other state, unavailable fallback);(2) MMI_gen 2959 (partly: ‘Settings’, inactive cabin, unavailable fallback)
            */
            // Call generic Check Results Method
            DmiExpectedResults
                .Verify_the_following_information_The_buttons_Main_Override_Data_View_and_Special_buttons_are_invisibled_in_any_other_state_The_Settings_button_is_still_visible();


            /*
            Test Step 35
            Action: Run the ‘Fallback’ function (Drive the train with speed Fallback)
            Expected Result: Verify the following information,The buttons ‘Main’, ‘Override’, Data View’ and ‘Special’ buttons are invisibled in any other state.The ‘Settings’ button is still visible
            Test Step Comment: (1) MMI_gen 2959 (partly: They are invisible in any other state, fallback);(2) MMI_gen 2959 (partly: ‘Settings’, inactive cabin, fallback)
            */
            // Call generic Check Results Method
            DmiExpectedResults
                .Verify_the_following_information_The_buttons_Main_Override_Data_View_and_Special_buttons_are_invisibled_in_any_other_state_The_Settings_button_is_still_visible();


            /*
            Test Step 36
            Action: End of test
            Expected Result: 
            */


            return GlobalTestResult;
        }
    }
}
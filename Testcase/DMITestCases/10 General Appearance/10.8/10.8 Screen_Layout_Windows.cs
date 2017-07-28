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
    /// 10.8 Screen Layout: Windows
    /// TC-ID: 5.8
    /// 
    /// This test case verifies that the presentation of each window that displays on DMI’s screen. Verify that the Default window is displayed according to [ERA] standard and the visualisation, sound, and button activation of up-type buttons(Main, Spec, Setting buttons) in this window shall comply with condition of [MMI-ETCS-gen].  The other window(s) (other than default window) and its Sub-level window is displayed according to [ERA] standard.
    /// 
    /// Tested Requirements:
    /// MMI_gen 4350; MMI_gen 4351; MMI_gen 4352; MMI_gen 4353; MMI_gen 4354; MMI_gen 4355; MMI_gen 4361; MMI_gen 4358; MMI_gen 4360;                      MMI_gen 4381 (Main, Spec, Setting buttons); MMI_gen 4382 (Main, Spec, Setting buttons);
    /// 
    /// Scenario:
    /// Perform SoM and during start of mission observe the correctness of different windows.Verify the correctness of the presentation and the behavior of up-type buttons in default window.Continue with the Driver ID window and the Train data window. Verify the correctness of the presentation in each window.
    /// 
    /// Used files:
    /// N/A
    /// </summary>
    public class Screen_Layout_Windows : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // Power on the system.

            // Call the TestCaseBase PreExecution
            base.PreExecution();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in SB mode, Level 1

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint


            /*
            Test Step 1
            Action: Activate cabin A. Then, observe the appearance of the Driver ID window
            Expected Result: The Driver ID window is displayed. All objects, text messages and buttons are presented within the same layer
            Test Step Comment: MMI_gen 4351;
            */


            /*
            Test Step 2
            Action: Enter the Driver ID. Perform brake test. Then select level 1
            Expected Result: The Level window is displayed.Verify the following:The Sub-level window covers partially on the screen.When this window is active, driver cannot select anything on the default window underneath e.g. ‘Main menu’ or ‘Settings menu’.All objects, text messages and buttons are presented within the same layer
            Test Step Comment: MMI_gen 4354;MMI_gen 4351;
            */


            /*
            Test Step 3
            Action: Confirm level 1 and Select ‘Train data’ button
            Expected Result: The Train data window is displayed.Verify the following:(1)   All objects, text messages and buttons are presented within the same layer.(2)   At the top of the window, it displays title with text ‘Train data’, background is black and text label is displayed as grey colour.(3)   For the title, when the number of DMI objects cannot fit within a window is displayed as (1/2) i.e. in this case it displays Train data (1/2) and Train data (2/2).(4)   The Data entry window contains a maximum of 4 or 3 input field. (5)   A close button is displayed as enabled.(6)   Sub-level window covers totally depending on the size of the Sub-Level window.(7)   ‘Next’ and/or ‘Previous’ button is enabled. The scrolling between various windows is not displayed as circular
            Test Step Comment: MMI_gen 4351;MMI_gen 4354;MMI_gen 4355;MMI_gen 4358;MMI_gen 4360;
            */


            /*
            Test Step 4
            Action: Confirm and Validate Train data.Then, observe the appearance of the Train Running Number window
            Expected Result: The Train Running Number window is displayed.All objects, text messages and buttons are presented within the same layer
            Test Step Comment: MMI_gen 4351;
            */


            /*
            Test Step 5
            Action: Enter and confirm the train running number. Then close the Main window
            Expected Result: The Default window is displayed.Verify the following:The Default window is presented as  ‘Total image’ and displayed area with allocation of objects, text messages, and buttons.The Default window is not composed of title, Input field, Close button, ‘Next’ or ‘Previous’ button, The Default window is not displayed the topic of the window.(4)   The Default window is not covering other windows
            Test Step Comment: MMI_gen 4350;MMI_gen 4352;MMI_gen 4353;MMI_gen 4361;Check more information about ‘Total image’ in [MMI-ETCS-gen]
            */


            /*
            Test Step 6
            Action: Then press and hold the Main menu button
            Expected Result: DMI still displays the default window.The Main menu button is shown as pressed state.The sound ‘click’ is played sound once
            Test Step Comment: MMI_gen 4381;
            */


            /*
            Test Step 7
            Action: Slide out of the ‘Main menu’ button
            Expected Result: DMI still displays the default windowThe visualisation of Main menu button is displayed as enabled state
            Test Step Comment: MMI_gen 4382;
            */


            /*
            Test Step 8
            Action: Slide back into the ‘Main menu’ button
            Expected Result: DMI still displays default windowThe visualisation of Main menu button is displayed as pressed state.The sound ‘click’ is not played
            Test Step Comment: MMI_gen 4382;
            */


            /*
            Test Step 9
            Action: Release the ‘Main menu’ button
            Expected Result: DMI displays sub-menu of the Main window
            Test Step Comment: MMI_gen 4381;   MMI_gen 4382;
            */


            /*
            Test Step 10
            Action: Press ‘Close’ button
            Expected Result: DMI displays the default window
            Test Step Comment: MMI_gen 4381;
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(@"Press ‘Close’ button");
            // Call generic Check Results Method
            DmiExpectedResults.DMI_displays_the_default_window();


            /*
            Test Step 11
            Action: Press and hold the Special button
            Expected Result: DMI still displays the default windowThe Special button is shown as pressed state.The sound ‘click’ is played once
            Test Step Comment: MMI_gen 4381;
            */


            /*
            Test Step 12
            Action: Slide out of Special button
            Expected Result: DMI still displays the default windowThe visualisation of Special button is shown as enabled state
            Test Step Comment: MMI_gen 4382;
            */


            /*
            Test Step 13
            Action: Slide back into ‘Special menu’ button
            Expected Result: DMI still displays the default windowThe visualisation of Special button is displayed in pressed stateThe sound ‘click’ is not played
            Test Step Comment: MMI_gen 4382;
            */


            /*
            Test Step 14
            Action: Release ‘Special menu’ button
            Expected Result: DMI displays sub-menu of Special window
            Test Step Comment: MMI_gen 4381;   MMI_gen 4382;
            */


            /*
            Test Step 15
            Action: Press ‘Close’ button
            Expected Result: DMI displays the default window
            Test Step Comment: MMI_gen 4381;
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(@"Press ‘Close’ button");
            // Call generic Check Results Method
            DmiExpectedResults.DMI_displays_the_default_window();


            /*
            Test Step 16
            Action: Press and hold the ‘Settings menu’ button
            Expected Result: DMI still displays the default windowThe Setting button is displayed as pressed state.The sound ‘click’ is played once
            Test Step Comment: MMI_gen 4381;
            */


            /*
            Test Step 17
            Action: Slide out of the ‘Setting menu’ button
            Expected Result: DMI still displays the default windowThe Setting button is shown as enabled state
            Test Step Comment: MMI_gen 4382;
            */


            /*
            Test Step 18
            Action: Slide back into the ‘Settings menu’ button
            Expected Result: DMI still displays the default windowThe Setting button is shown as  pressed state.The sound ‘click’ is not played
            Test Step Comment: MMI_gen 4382;
            */


            /*
            Test Step 19
            Action: Release the ‘Settings menu’ button
            Expected Result: DMI displays all sub-menus of Setting window
            Test Step Comment: MMI_gen 4381;   MMI_gen 4382;
            */


            /*
            Test Step 20
            Action: End of test
            Expected Result: 
            */


            return GlobalTestResult;
        }
    }
}
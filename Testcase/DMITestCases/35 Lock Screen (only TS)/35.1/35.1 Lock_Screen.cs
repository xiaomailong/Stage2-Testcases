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
    /// 35.1 Lock Screen
    /// TC-ID: 32.1
    /// 
    /// This test case verifies the properties of Lock Screen function that do not have any effect to operation.
    /// 
    /// Tested Requirements:
    /// MMI_gen 2520; MMI_gen 2521; MMI_gen 2522; MMI_gen 2523; MMI_gen 1097; MMI_gen 4256 (partly: Sinfo sound); MMI_gen 9516 (partly: lock screen function); MMI_gen 12025 (partly: lock screen function);
    /// 
    /// Scenario:
    /// Activate cabin A. Enter Driver ID and perform brake test.Enter and validate Train data. Then, enter train running number.Open Settings window. Then, verify the state of ‘Lock Screen’ button.Press ‘Lock Screen’ button. Then, verify the display information and sound.Open Main window. Then, press ‘Start’ button and acknowledge SR mode.Open Setting window and press ‘Lock Screen’ button.Drive the train forward. Then, verify the display information and sound.
    /// 
    /// Used files:
    /// N/A
    /// </summary>
    public class Lock_Screen : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // System is power on.

            // Call the TestCaseBase PreExecution
            base.PreExecution();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays  in SR mode, Level 1

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint


            /*
            Test Step 1
            Action: Perform the following procedure,Activate Cabin A.Enter Driver ID and perform brake test.Select and confirm Level 1.Press ‘Train data button.Enter and confirm all data. Then, press ‘Yes’ button.Press ‘Yes’ button and Confirm entered data by pressing an input field.Enter and confirm Train running numberPress ‘Close’ button
            Expected Result: DMI displays Default window in SB mode and Level 1
            */


            /*
            Test Step 2
            Action: The train is at standstill. Press ’Settings’ button
            Expected Result: The speed pointer is indicated to position 0 km/h.The Settings menu is displayed with enabled ’Lock Screen’ button
            Test Step Comment: MMI_gen 2520;
            */


            /*
            Test Step 3
            Action: Press ‘Lock Screen’ button
            Expected Result: Verify the following information,The ‘Lock Screen’ function is activated.The activation of this function is clearly visualised and displayed as countdown. This function has a maximum duration of 10s, The countdown is start from 10 to 0.Note: Text “Screen locked for X” disappears when X=0.DMI plays Sinfo sound when the countdown text is “Screen locked for 1”.DMI displays Settings window when the Lock Screen function is deactivated
            Test Step Comment: (1) MMI_gen 2520 (partly: train is at standstill);                                               (2) MMI_gen 2521 (partly: clrealy visualize);                        (3) MMI_gen 2521 (partly: maximum duration);                                     (4) MMI_gen 2522;                  MMI_gen 1097; MMI_gen 9516 (partly: deactivation of lock screen function); MMI_gen 12025 (partly: deactivation of lock screen function);                                    (5) MMI_gen2523;
            */


            /*
            Test Step 4
            Action: Press ‘Close’ button on Settings window
            Expected Result: DMI displays Default window
            */
            // Call generic Check Results Method
            DmiExpectedResults.DMI_displays_Default_window(this);


            /*
            Test Step 5
            Action: Press ‘Main’ button and select ‘Start’ button. Then, acknowledge SR mode
            Expected Result: DMI displays in SR mode and Level 1
            */


            /*
            Test Step 6
            Action: Press ‘Settings menu’ button and select ‘Lock Screen’ button
            Expected Result: The ‘Lock Screen’ is activated
            */


            /*
            Test Step 7
            Action: Drive the train forward with speed = 40km/h
            Expected Result: Verify the following information,Verify that DMI displays the default window after 1 second from the speed digital increased.The sound ‘Sinfo’ is played
            Test Step Comment: (1) MMI_gen 2520 (partly: train starts moving);     (2) MMI_gen 2520 (partly: sound Sinfo); MMI_gen 4256 (partly: Sinfo sound); MMI_gen 9516 (partly: activation of lock screen function); MMI_gen 12025 (partly: activation of lock screen function);    
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
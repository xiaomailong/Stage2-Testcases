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
    /// 22.9.6 Hide PA Function is configured ‘TIMER’ with reboot DMI
    /// TC-ID: 17.9.5
    /// 
    /// This test case verifies that if the Hide PA Function is configured as ‘Timer’ and then reboot the Dmi. The Hide PA button shall be enable.  The ‘Timer’ configured shall comply with condition of  [MMI-ETCS-gen].
    /// 
    /// Tested Requirements:
    /// MMI_gen 7341; MMI_gen 2996 (partly: Timer);
    /// 
    /// Scenario:
    /// Set HIDE PA FUNCTION configuration as Timer Set HIDE PA TIMER configuration as 20sActivate cabin A. Driver enters the Driver ID and performs brake test. Then the driver selects level 1, Train data, and validate the train data. After that driver enter Train running number and confirm SR mode. At 100 m, pass BG1 with pkt 12, pkt 21 and pkt 
    /// 27.Mode changes to FS modeTurn off/on  DMI. The Hide PA button is appeared from the area D of DMISet HIDE PA TIMER configuration as 30s, 40s, 50s, 60s, 70s and 80s. Repeat test step 1-6 and verify the ‘Timer’ function of the Hide PA Function.
    /// 
    /// Used files:
    /// 17_9_5.tdg
    /// </summary>
    public class Hide_PA_Function_is_configured_TIMER_with_reboot_DMI : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // System is power off .Configure HIDE_PA_FUNCTION to 3 (Timer)Configure HIDE_PA_TIMER to 20s., See the instruction in Appendix 1

            // Call the TestCaseBase PreExecution
            base.PreExecution();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // ATP is in FS mode

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint


            /*
            Test Step 1
            Action: Power On the system.
            Expected Result: DMI displays the default window.
            */

            /*
            Test Step 2
            Action: Activate cabin A and Perform SoM to SR mode, Level 1.
            Expected Result: DMI displays in SR mode, level 1.
            */

            /*
            Test Step 3
            Action: Drive the train forward with speed = 40 km/h pass BG1
            Expected Result: DMI shows “Entering FS” message.DMI displays the Planning area. The Hide PA button is appeared on  the area D of the DMI.
            */

            /*
            Test Step 4
            Action: Press Hide PA button.
            Expected Result: The Planning area is disappeared and hidden from main area D for 20s.After 20s the planning area is displayed.Verify that the Hide PA button is displayed on the planning area.
            Test Step Comment: MMI_gen 7341;
            */

            /*
            Test Step 5
            Action: Turn off power of DMI 
            Expected Result: DMI is power off.
            */

            /*
            Test Step 6
            Action: Turn on power of DMI 
            Expected Result: DMI is power on DMI displays the Planning area The Hide PA button is appeared on  the main area D of the DMI.
            Test Step Comment: MMI_gen 7341;  MMI_gen 2996 (partly: Timer); Hide PA icon
            */

            /*
            Test Step 7
            Action: Set HIDE PA TIMER configuration as 30s and repeat test step 1-6.
            Expected Result: The Planning area is disappeared and hidden from main area D for 30s.After 30s the planning area is displayed.Verify that the Hide PA button is displayed at sub-area D14 on the planning area.
            Test Step Comment: MMI_gen 7341;   MMI_gen 2996 (partly: Timer);
            */

            /*
            Test Step 8
            Action: Set HIDE PA TIMER configuration as 40s and repeat test step 1-6.
            Expected Result: The Planning area is disappeared and hidden from main area D for 40s.After 40s the planning area is displayed.Verify that the Hide PA button is displayed at sub-area D14 on the planning area.
            */

            /*
            Test Step 9
            Action: Set HIDE PA TIMER configuration as 50s and repeat test step 1-6.
            Expected Result: The Planning area is disappeared and hidden from main area D for 50s.After 50s the planning area is displayed.Verify that the Hide PA button is displayed at sub-area D14 on the planning area.
            */

            /*
            Test Step 10
            Action: Set HIDE PA TIMER configuration as 60s and repeat test step 1-6.
            Expected Result: The Planning area is disappeared and hidden from main area D for 60s.After 60s the planning area is displayed.Verify that the Hide PA button is displayed at sub-area D14 on the planning area.
            */

            /*
            Test Step 11
            Action: Set HIDE PA TIMER configuration as 70s and repeat test step 1-6.
            Expected Result: The Planning area is disappeared and hidden from main area D for 60s.After 60s the planning area is displayed.Verify that the Hide PA button is displayed at sub-area D14 on the planning area.
            */

            /*
            Test Step 12
            Action: Set HIDE PA TIMER configuration as 80s and repeat test step 1-6.
            Expected Result: The Planning area is disappeared and hidden from main area D for 60s.After 60s the planning area is displayed.Verify that the Hide PA button is displayed at sub-area D14 on the planning area.
            */

            /*
            Test Step 13
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
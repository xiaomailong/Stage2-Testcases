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
    /// 23.2 Local Time: Sub-Area G13
    /// TC-ID: 18.2
    /// 
    /// This test case verifies the display of the local time on DMI that shall comply with [ERA-ERTMS] standard and [MMI-ETCS-gen].
    /// 
    /// Tested Requirements:
    /// MMI_gen 176; MMI_gen 3613; MMI_gen 3852-1 (THR)(partly: flashing colons);
    /// 
    /// Scenario:
    /// 1.Test system is powered on
    /// 2.Local time data is verified
    /// 3.SoM is completed in SR mode, ETCS level 
    /// 14.Local time format is verified
    /// 
    /// Used files:
    /// N/A
    /// </summary>
    public class Local_Time_Sub_Area_G13 : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // Test system is powered off
            
            // Call the TestCaseBase PreExecution
            base.PreExecution();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in SR mode, Level 1.

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint

            
            /*
            Test Step 1
            Action: Power on the test system
            Expected Result: The driver message “Driver’s cab not active” displays on the DMI screen with timestamp on the left. Use the log file to confirm the timestamp is equal to MMI_T_UTC + MMI_T_ZONE_OFFSET from EVC-3
            Test Step Comment: (1) MMI_gen 176 (partly: derived time)
            */
            
            
            /*
            Test Step 2
            Action: Perform SoM to SR mode, ETCS level 1 and verified the presentation on the DMI screen
            Expected Result: DMI displays in SR mode, Level 1. The local time is displayed in format hh:mm:ss (24h) on sub-area G13.DMI displays the local time as a single line in grey colour. The background colour is dark-blue.The colon ‘:’ of local time flashes (shown and hide)
            Test Step Comment: (1) MMI_gen 176 (partly: format, sub-area G13)(2) MMI_gen 3613(3) MMI_gen 3852-1 (THR) (partly: flashing colons)
            */
            
            
            /*
            Test Step 3
            Action: End of test
            Expected Result: 
            */
            

            return GlobalTestResult;
        }
    }
}

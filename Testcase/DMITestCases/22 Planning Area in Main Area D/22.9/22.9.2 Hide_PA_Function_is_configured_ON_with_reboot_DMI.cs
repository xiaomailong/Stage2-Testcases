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
    /// 22.9.2 Hide PA Function is configured ‘ON’ with reboot DMI
    /// TC-ID: 17.9.2
    /// 
    /// This test case verifies that if the Hide PA Function is configured as “On” and then the DMI rebooted, the PA and Hide PA button shall be enable. The ‘ON’ configured shall comply with condition of  [MMI-ETCS-gen].
    /// 
    /// Tested Requirements:
    /// MMI_gen 7341; MMI_gen 2996 (partly: ON);
    /// 
    /// Scenario:
    /// Active cabin A. Perform SoM to SR mode, level 1Start driving the train forward and Pass BG
    /// 1.Mode changes to FS mode.Turn off and then turn on DMI. The PA and Hide PA button are appeared on the area D of the DMI.
    /// 
    /// Used files:
    /// 17_9_2.tdg
    /// </summary>
    public class Hide_PA_Function_is_configured_ON_with_reboot_DMI : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // Set the following tags name in configuration file (See the instruction in Appendix 1)HIDE_PA_FUNCTION = 0 (‘ON’ state)System is power ON.

            // Call the TestCaseBase PreExecution
            base.PreExecution();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in FS mode, Level 1.

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint


            /*
            Test Step 1
            Action: Activate cabin A and Perform SoM to SR mode, Level 1.
            Expected Result: DMI displays in SR mode, level 1.
            */

            /*
            Test Step 2
            Action: Drive the train forward with speed = 40 km/h pass BG1
            Expected Result: DMI shows “Entering FS” message and DMI displays the Planning area.The Hide PA button is appeared on  the area D of the DMI.
            */

            /*
            Test Step 3
            Action: Press Hide PA button.
            Expected Result: The Planning area is disappeared from the area D of DMI.
            Test Step Comment:  Hide PA button
            */

            /*
            Test Step 4
            Action: Turn off power of DMI 
            Expected Result: DMI is power off.
            */

            /*
            Test Step 5
            Action: Turn on power of DMI 
            Expected Result: DMI is power on DMI displays the Planning area The Hide PA button is appeared on the area D of the DMI.
            Test Step Comment: MMI_gen 7341;  MMI_gen 2996 (partly: ON); Hide PA icon
            */

            /*
            Test Step 6
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
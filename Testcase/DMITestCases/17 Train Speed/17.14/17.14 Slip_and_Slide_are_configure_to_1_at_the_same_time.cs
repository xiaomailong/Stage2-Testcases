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
    /// 17.14 Slip and Slide are configure to 1 at the same time
    /// TC-ID: 12.14
    /// 
    /// This test case verifies the display of the ‘Slip/Slide’ indication when both of indication are configured enabled with ETC speed.
    /// 
    /// Tested Requirements:
    /// MMI_gen 1693;
    /// 
    /// Scenario:
    /// SoM is completed in SR mode, Level 1 and  SLIP_SPEEDMETER & SLIDE_SPEEDMETER are configured to be 1 (display)At 100 m, pass BG1 with pkt 12, pkt 21 and pkt 
    /// 27.Mode changes to FS mode.The ‘Slip/Slide’ indication is verified by the following cases:ATP disable MMI_M_SLIP and MMI_M_SLIDEATP enable MMI_M_SLIP and MMI_M_SLIDEATP enable MMI_M_SLIP but disable MMI_M_SLIDEATP disable MMI_M_SLIP butenable MMI_M_SLIDEThe train is stopped.
    /// 
    /// Used files:
    /// 12_14.tdg, 12_14_a.xml, 12_14_b.xml, 12_14_c.xml
    /// </summary>
    public class Slip_and_Slide_are_configure_to_1_at_the_same_time : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // Set the following tags name in configuration file (See the instruction in Appendix 1)SLIP_SPEEDMETER = 1SLIDE_SPEEDMETER = 1Test system is powered onCabin is activeSoM is completed in SR mode, Level 1.

            // Call the TestCaseBase PreExecution
            base.PreExecution();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in FS mode, level 1.

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint


            /*
            Test Step 1
            Action: Driver the train forward
            Expected Result: DMI changes from SR mode to FS mode. 
            */

            /*
            Test Step 2
            Action: Drive the train forward with speed = 140 km/h 
            Expected Result: The speed pointer is displayed with speed =140.
            */

            /*
            Test Step 3
            Action: Use the test script file 12_14_a.xml to send EVC-1 with,MMI_M_SLIP = 1MMI_M_SLIDE = 0
            Expected Result: The Slip indication is displayed and shown as arrow pointing clockwise.
            */

            /*
            Test Step 4
            Action: Use the test script file 12_14_b.xml to send EVC-1 with,MMI_M_SLIP = 0MMI_M_SLIDE = 1
            Expected Result: The Slide indication is displayed and shown as arrow pointing counterclockwise.
            */

            /*
            Test Step 5
            Action: Use the test script file 12_14_c.xml to send EVC-1 with,MMI_M_SLIP = 1MMI_M_SLIDE = 1
            Expected Result: Verify the following information,The Slip indication is displayed and shown as arrow pointing clockwise.
            Test Step Comment: (1) MMI_gen 1693;
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
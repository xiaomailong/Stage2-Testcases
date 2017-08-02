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
    /// 17.3.1 Speed Pointer: Sub-Area B1
    /// TC-ID: 12.3
    /// 
    /// This test case verifies the presentation of speed pointer that displays in sub-area B1.
    /// The dimensions and colours of speed pointer shall comply with [ERA] standard.
    /// 
    /// Tested Requirements:
    /// MMI_gen 5965; MMI_gen 5968; 
    /// 
    /// Scenario:
    /// Drive the train forward. Then, verify the display of speed pointer.
    /// 
    /// Used files:
    /// N/A
    /// </summary>
    public class TC_12_3_Speed_Pointer : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // Test system is powered on.Cabin is activated.SoM is performed in SR mode, Level 1.

            // Call the TestCaseBase PreExecution
            base.PreExecution();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in SR mode, level 1

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint

            TraceInfo("This is a repeat of TC_12_1_Display_of_Speed_Pointer_and_Speed_Digital." +
                        "Please see results of this test case.");

            /*
            Test Step 1
            Action: Drive the train forward, speed up to 25 km/h
            Expected Result: Verify the following information,The speed pointer is displayed in sub-area B1. The speed pointer consists of a needle and a circular part centred in sub-area B1. Both parts are displayed in same colour. The dimension of the speed pointer is presented
            Test Step Comment: (1) MMI_gen 5965;   (2) MMI_gen 5968;   
            */

            /*
            Test Step 2
            Action: Stop the train
            Expected Result: The speed pointer is indicated to zero km/h
            */
            // Call generic Action Method

            /*
            Test Step 3
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
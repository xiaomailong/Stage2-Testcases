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
    /// 17.1 Display of Speed Pointer and Speed Digital
    /// TC-ID: 12.1
    /// 
    /// This test case verifies the presentation of speed pointer and speed digital which apply on train speed and display in sub-area B1 and B2. This shall comply with [ERA] standard and [MMI-ETCS-gen].
    /// 
    /// Tested Requirements:
    /// MMI_gen 1277; MMI_gen 5954; MMI_gen 11882;
    /// 
    /// Scenario:
    /// Driver performs SoM to SR mode, level 
    /// 1.Start driving the train forward with speed equal to 25 km/h.
    /// 
    /// Used files:
    /// N/A
    /// </summary>
    public class Display_of_Speed_Pointer_and_Speed_Digital : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // System is power on.Cabin is activated.SoM is performed in SR mode, Level 1.
            
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
            Action: Drive the train forward, speed up to 25 Km/h
            Expected Result: Verify the following information,(1)   The speed pointer and the speed digital are displayed in area B1 with constantly movement and indicated the train speed at 25km/h.(2)   Verify that the speed pointer in sub-area B1 and Circular Speed Gauge (CSG) in sub-area B2 are displayed as correlately.(3)   Use the log file to confirm that DMI received packet information EVC-1 with variable MMI_V_TRAIN = 694 (~25km/h)
            Test Step Comment: (1) MMI_gen 1277;(2) MMI_gen 5954;(3) MMI_gen 11822;
            */
            
            
            /*
            Test Step 2
            Action: Stop the train
            Expected Result: The train is at standstill
            */
            // Call generic Action Method
            DmiActions.Stop_the_train();
            // Call generic Check Results Method
            DmiExpectedResults.The_train_is_at_standstill();
            
            
            /*
            Test Step 3
            Action: End of test
            Expected Result: 
            */
            

            return GlobalTestResult;
        }
    }
}

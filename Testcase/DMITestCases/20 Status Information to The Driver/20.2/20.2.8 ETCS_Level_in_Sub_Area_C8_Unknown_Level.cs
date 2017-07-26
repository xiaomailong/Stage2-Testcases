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
    /// 20.2.8 ETCS Level in Sub-Area C8: Unknown Level
    /// TC-ID: 15.2.8
    /// 
    /// This test case verifies the appearance of ETCS Level symbols in sub-area C8 after received an invalid or unknown value from packet information EVC-7. The displays in sub-area C8 shall be blank.
    /// 
    /// Tested Requirements:
    /// MMI_gen 577 (partly: Unknown);
    /// 
    /// Scenario:
    /// 1.Use the test script file to send EVC-7 with an invalid and unknown value. Then, verify the blank information in sub-area C8.
    /// 
    /// Used files:
    /// 15_2_8_a.xml
    /// </summary>
    public class ETCS_Level_in_Sub_Area_C8_Unknown_Level : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // System is power on.Cabin is activated.SoM is performed until level 1 is selected and confirm.Main window is closed.

            // Call the TestCaseBase PreExecution
            base.PreExecution();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in SB mode, level 1.

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint


            /*
            Test Step 1
            Action: Use the test script file 15_2_8_a.xml to send EVC-7 with,OBU_TR_M_Level = 15
            Expected Result: Verify the following information,(1)   No symbol displays in sub-area C8.
            Test Step Comment: (1) MMI_gen 577 (partly: Unknown);
            */

            /*
            Test Step 2
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
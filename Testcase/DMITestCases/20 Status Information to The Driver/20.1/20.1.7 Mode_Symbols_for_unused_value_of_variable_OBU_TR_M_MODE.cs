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
    /// 20.1.7 Mode Symbols for unused value of variable OBU_TR_M_MODE
    /// TC-ID: 15.1.7
    /// 
    /// This test case verifies the blank display on sub-area B7 (mode symbol) when DMI receives the unused value in packet information EVC-7.
    /// 
    /// Tested Requirements:
    /// MMI_gen 580;
    /// 
    /// Scenario:
    /// Use the test script file to send EVC-7 with specific unused values. Then, verify the blank display on sub-area B7.
    /// 
    /// Used files:
    /// 15_1_7_a.xml, 15_1_7_b.xml, 15_1_7_c.xml, 15_1_7_d.xml, 15_1_7_e.xml, 15_1_7_f.xml, 15_1_7_g.xml, 15_1_7_h.xml
    /// </summary>
    public class Mode_Symbols_for_unused_value_of_variable_OBU_TR_M_MODE : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // Test system is powered onSoM is performed until level 1 is selected and confirmedMain window is closed

            // Call the TestCaseBase PreExecution
            base.PreExecution();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in SB mode, Level 1.

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint


            /*
            Test Step 1
            Action: Use the test script file 15_1_7_a.xml to send EVC-7 with, MMI_OBU_TR_M_MODE = 17 (“Not used”)
            Expected Result: Verify the following information,(1)   There is no symbol displayed on sub-area B7.
            Test Step Comment: (1) MMI_gen 580;
            */

            /*
            Test Step 2
            Action: Use the test script file 15_1_7_b.xml to send EVC-7 with, MMI_OBU_TR_M_MODE = 127 (“Not used”)
            Expected Result: Verify the following information,(1)   There is no symbol displayed on sub-area B7.
            Test Step Comment: (1) MMI_gen 580;
            */

            /*
            Test Step 3
            Action: Use the test script file 15_1_7_c.xml to send EVC-7 with, MMI_OBU_TR_M_MODE = 129 (“Not used”)
            Expected Result: Verify the following information,(1)   There is no symbol displayed on sub-area B7.
            Test Step Comment: (1) MMI_gen 580;
            */

            /*
            Test Step 4
            Action: Use the test script file 15_1_7_d.xml to send EVC-7 with, MMI_OBU_TR_M_MODE = 255 (“Not used”)
            Expected Result: Verify the following information,(1)   There is no symbol displayed on sub-area B7.
            Test Step Comment: (1) MMI_gen 580;
            */

            /*
            Test Step 5
            Action: Use the test script file 15_1_7_e.xml to send EVC-7 with, MMI_OBU_TR_M_MODE = 18 (“Not used”)
            Expected Result: Verify the following information,(1)   There is no symbol displayed on sub-area B7.
            Test Step Comment: (1) MMI_gen 580;
            */

            /*
            Test Step 6
            Action: Use the test script file 15_1_7_f.xml to send EVC-7 with, MMI_OBU_TR_M_MODE = 126 (“Not used”)
            Expected Result: Verify the following information,(1)   There is no symbol displayed on sub-area B7.
            Test Step Comment: (1) MMI_gen 580;
            */

            /*
            Test Step 7
            Action: Use the test script file 15_1_7_g.xml to send EVC-7 with, MMI_OBU_TR_M_MODE = 130 (“Not used”)
            Expected Result: Verify the following information,(1)   There is no symbol displayed on sub-area B7.
            Test Step Comment: (1) MMI_gen 580;
            */

            /*
            Test Step 8
            Action: Use the test script file 15_1_7_h.xml to send EVC-7 with, MMI_OBU_TR_M_MODE = 254 (“Not used”)
            Expected Result: Verify the following information,(1)   There is no symbol displayed on sub-area B7.
            Test Step Comment: (1) MMI_gen 580;
            */

            /*
            Test Step 9
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
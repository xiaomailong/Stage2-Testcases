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
    /// SH Symbol in Level 0 and Level 1
    /// TC-ID: 34.4.1
    /// 
    /// This test case verifies the display of symbol MO01 in ETCS level 0 and 1 when driver presses Shunting button.
    /// 
    /// Tested Requirements:
    /// MMI_gen 11914 (partly: when receive SH symbol); MMI_gen 11084 (partly: SH); MMI_gen 110 (partly: MO10);
    /// 
    /// Scenario:
    /// Enter SH mode, level 
    /// 0.Then, verify the display of symbol MO01.Restart test system.Enter SH mode, level 
    /// 1.Then, verify the display of symbol MO01.
    /// 
    /// Used files:
    /// N/A
    /// </summary>
    public class Symbol_in_Level_0_and_Level_1 : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // Test system is powered onCabin is activatedDriver ID is enteredLevel 0 is selected and confirmed

            // Call the TestCaseBase PreExecution
            base.PreExecution();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in SH mode, level 1

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint


            /*
            Test Step 1
            Action: Enter SH mode by performing the procedure below,Press and hold ‘Shunting’ button at least 2 secondsRelease ‘Shunting’ button
            Expected Result: Verify the following information,Use the log file to confirm that DMI receives EVC-7 with variable OBU_TR_M_MODE = 3 (SH – Shunting).The symbol MO01 is display in area B7.DMI closes Main window and returns to the Default window.
            Test Step Comment: (1) MMI_gen 11914 (partly: receives SH symbol); MMI_gen 11084 (partly: SH);(2) MMI_gen 11914 (partly: display the symbol when receive SH symbol); MMI_gen 110 (partly: MO10);(3) MMI_gen 11914 (partly: close main window and return to the default window);
            */

            /*
            Test Step 2
            Action: Re-validate the step1 by re-starting OTE Simulator and starting the precondition with ETCS level 1.
            Expected Result: See the expected results at Step 1. 
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
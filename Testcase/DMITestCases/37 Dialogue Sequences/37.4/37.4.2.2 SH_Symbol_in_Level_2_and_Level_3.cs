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
    /// 37.4.2.2 SH Symbol in Level 2 and Level 3
    /// TC-ID: 34.4.2.2
    /// 
    /// This test case verifies the display of symbol MO01 in ETCS level 2 and 3 when DMI receives message 28 from RBC after driver presses Shunting button.
    /// 
    /// Tested Requirements:
    /// MMI_gen 11914 (partly: when receive SH symbol); MMI_gen 11084 (partly: SH); MMI_gen 110 (partly: MO10);
    /// 
    /// Scenario:
    /// 1.Enter SH mode, level 
    /// 2.Then, verify the display of symbol MO01.2.Restart test system.3.Enter SH mode, level 
    /// 3.Then, verify the display of symbol MO01.
    /// 
    /// Used files:
    /// 34_4_2_2.utt
    /// </summary>
    public class SH_Symbol_in_Level_2_and_Level_3 : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // Test System is power onCabin is activatedSoM is performed in SR mode, level 2

            // Call the TestCaseBase PreExecution
            base.PreExecution();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in SH mode, level 3

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint


            /*
            Test Step 1
            Action: Enter SH mode by performing the procedure below,Press ‘Main’ buttonPress and hold ‘Shunting’ button at least 2 seconds.Release ‘Shunting’ button
            Expected Result: Verify the following information,Use the log file to confirm that DMI receives EVC-7 with variable OBU_TR_M_MODE = 3 (SH – Shunting).The symbol MO01 is display in area B7.DMI closes Main window and returns to the Default window.
            Test Step Comment: (1) MMI_gen 11914 (partly: receives SH symbol, Level 2/3); MMI_gen 11084 (partly: SH);(2) MMI_gen 11914 (partly: display the symbol when receive SH symbol); MMI_gen 110 (partly: MO10);(3) MMI_gen 11914 (partly: close main window and return to the default window);
            */

            /*
            Test Step 2
            Action: Re-validate the step1 by re-starting OTE Simulator and starting the precondition with ETCS level 3.
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
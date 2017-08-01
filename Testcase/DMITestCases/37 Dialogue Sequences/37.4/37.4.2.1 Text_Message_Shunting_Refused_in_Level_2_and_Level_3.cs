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
    /// 37.4.2.1 Text Message “Shunting Refused” in Level 2 and Level 3
    /// TC-ID: 34.4.2.1
    /// 
    /// This test case verifies the display of text message “SH refused” in ETCS level 2 and 3 when DMI received message 27 from radio connection.
    /// 
    /// Tested Requirements:
    /// MMI_gen 11915 (partly: SH refused); MMI_gen 134 (partly: E5);
    /// 
    /// Scenario:
    /// 1.Enter SH mode, level 
    /// 2.Then, verify the display of text message “SH refused”.2.Restart test system.
    /// 3.Enter SH mode, level 
    /// 3.Then, verify the display of text message “SH refused”.
    /// 
    /// Used files:
    /// 34_4_2_1.utt
    /// </summary>
    public class Text_Message_Shunting_Refused_in_Level_2_and_Level_3 : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // Test system is power onCabin is activatedSoM is performed in SR mode, level 2

            // Call the TestCaseBase PreExecution
            base.PreExecution();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in SR mode, level 3

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint


            /*
            Test Step 1
            Action: Enter SH mode by performing the procedure below,Press ‘Main’ buttonPress and hold ‘Shunting’ button at least 2 seconds.Release ‘Shunting’ button
            Expected Result: DMI displays Main window with text message ‘Shunting refused’ in sub-area E5
            Test Step Comment: MMI_gen 11915 (partly: SH refused); MMI_gen 134 (partly: E5);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(
                @"Enter SH mode by performing the procedure below,Press ‘Main’ buttonPress and hold ‘Shunting’ button at least 2 seconds.Release ‘Shunting’ button");


            /*
            Test Step 2
            Action: Re-validate the step1 by re-starting OTE Simulator and starting the precondition with ETCS level 3
            Expected Result: See the expected results at Step 1
            */
            // Call generic Action Method
            DmiActions
                .Re_validate_the_step1_by_re_starting_OTE_Simulator_and_starting_the_precondition_with_ETCS_level_3(this);
            // Call generic Check Results Method
            DmiExpectedResults.See_the_expected_results_at_Step_1(this);


            /*
            Test Step 3
            Action: End of test
            Expected Result: 
            */


            return GlobalTestResult;
        }
    }
}
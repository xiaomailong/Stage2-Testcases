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
    /// 37.4.2.3 Text Message “Shunting Request Failed” in Level 2 and Level 3
    /// TC-ID: 34.4.2.3
    /// 
    /// This test case verifies the display of text message “Shunting request failed” in ETCS level 2 and 3 when RBC doesn’t reply the “Shunting Authorised” to Onboard within the fixed waiting time from the last sending of “Request for Shunting”.
    /// 
    /// Tested Requirements:
    /// MMI_gen 11915 (partly: SH request failed); MMI_gen 134 (partly: E5); MMI_gen 9151;
    /// 
    /// Scenario:
    ///  1.Enter SH mode, level 
    /// 2.Then, verify the display of text message “Shunting request failed” in sub-area E5.2.Restart test system.
    /// 3.Enter SH mode, level 
    /// 3.Then, verify the display of text message “Shunting request failed” in sub-area E5.
    /// 
    /// Used files:
    /// 34_4_2_3.utt
    /// </summary>
    public class Text_Message_Shunting_Request_Failed_in_Level_2_and_Level_3 : TestcaseBase
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
            // DMI displays is in SH mode, level 3

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint

            
            /*
            Test Step 1
            Action: Enter SH mode by performing the procedure below,Press ‘Main’ buttonPress and hold ‘Shunting’ button at least 2 seconds.Release ‘Shunting’ button
            Expected Result: DMI displays Main window.(1)    While the Main window is display with hourglass symbol (ST05), the close button is disabled.(2)   The text message ‘Shunting request failed’ is display in sub-area E5 within 2 minutes
            Test Step Comment: Level 2:(1) MMI_gen 9151;(2) MMI_gen 11915 (partly: SH request failed); MMI_gen 134 (partly: E5);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(@"Enter SH mode by performing the procedure below,Press ‘Main’ buttonPress and hold ‘Shunting’ button at least 2 seconds.Release ‘Shunting’ button");
            
            
            /*
            Test Step 2
            Action: Re-validate the step1 by re-starting OTE Simulator and starting the precondition with ETCS level 3
            Expected Result: See the expected results at Step 1
            Test Step Comment: Level 3:(1) MMI_gen 9151;(2) MMI_gen 11915 (partly: SH request failed); MMI_gen 134 (partly: E5);
            */
            // Call generic Action Method
            DmiActions.Re_validate_the_step1_by_re_starting_OTE_Simulator_and_starting_the_precondition_with_ETCS_level_3();
            // Call generic Check Results Method
            DmiExpectedResults.See_the_expected_results_at_Step_1();
            
            
            /*
            Test Step 3
            Action: End of test
            Expected Result: 
            */
            

            return GlobalTestResult;
        }
    }
}

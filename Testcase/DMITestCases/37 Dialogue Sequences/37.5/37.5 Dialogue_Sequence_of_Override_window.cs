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
    /// 37.5 Dialogue Sequence of Override window
    /// TC-ID: 34.5
    /// 
    /// This test case verifies the ‘Close’ button enabling on the ‘Override’ window which complies with [ERA-ERTMS] standard and [MMI-ETCS-gen].
    /// 
    /// Tested Requirements:
    /// MMI_gen 9179; MMI_gen 8785 (partly: Override);
    /// 
    /// Scenario:
    /// Activate cabin A. Perform SoM to SR mode, level 1.
    /// 1.Press ‘Override’ button
    /// 2.Verify the Close button of the Override window is always enabled by pressing the Close button.Note: This test case is verifies only SR mode Level 
    /// 1.However, tester can use this scenario to verify test result in the following modes and Levels also,FS, LS, SR, OS, PT mode for Level 1,2,3UN mode for Level 0SN mode for Level NTCSH mode for All Level
    /// 
    /// Used files:
    /// N/A
    /// </summary>
    public class TC_34_5_Dialogue_Sequences : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // Test system is powered on Cabin is acticvated.SoM is performed in SR mode, level 1DMI displays the ‘Default’ window

            // Call the TestCaseBase PreExecution
            base.PreExecution();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in SR mode, Level 1

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint


            /*
            Test Step 1
            Action: Press ‘Override’ button
            Expected Result: The Override window is displayed. Verify that the Close button is always enabled
            Test Step Comment: MMI_gen 9179;
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press ‘Override’ button");


            /*
            Test Step 2
            Action: Press ‘Close’ button
            Expected Result: The Override window is closed. DMI displays the default window
            Test Step Comment: MMI_gen 9179; MMI_gen 8785 (partly: Override window); 
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press ‘Close’ button");


            /*
            Test Step 3
            Action: End of test
            Expected Result: 
            */


            return GlobalTestResult;
        }
    }
}
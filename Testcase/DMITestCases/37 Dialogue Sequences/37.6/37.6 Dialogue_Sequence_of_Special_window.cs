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
    /// 37.6 Dialogue Sequence of Special window
    /// TC-ID: 34.6
    /// 
    /// This test case verifies the ‘Close’ button enabling on every window under the ‘Special’ window and the ‘Special window which complies with [ERA-ERTMS] standard and [MMI-ETCS-gen].
    /// 
    /// Tested Requirements:
    /// MMI_gen 9199; MMI_gen 8785 (partly: Special, Adhesion, SR speed/distance);
    /// 
    /// Scenario:
    /// Activate cabin A. Perform SoM to SR mode, level 1.
    /// 1.Press ‘Special menu’ button on the default window.
    /// 2.Verify the Close button of the Special window is always enabled by pressing the Close button.
    /// 3.Press ‘Special menu’ button again.
    /// 4.Press ‘Adhesion’ button and press ‘Close’ button. 
    /// 5.Press ‘SR speed/distance’ button. Then press ‘Close’ button.Note: This test case is verifies only SR mode Level 
    /// 1.However, tester can use this scenario to verify test result in SR mode for Level 2 and 3 also.
    /// 
    /// Used files:
    /// N/A
    /// </summary>
    public class TC_34_6_Dialogue_Sequences : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // Use the ATP config editor to set the following parameters as follows (See the instruction in Appendix 2),Q_NVDRIVER_ADHES = 1Test system is powered on Activate Cabin A.Start of Mission is completed in SR mode, level 1DMI displays the ‘Default’ window

            // Call the TestCaseBase PreExecution
            base.PreExecution();

            DmiActions.Complete_SoM_L1_SR(this);
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in SR mode, Level 1
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in SR mode, Level 1.");

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint

            /*
            Test Step 1
            Action: Press ‘Spec’ button
            Expected Result: The Special window is displayed. Verify that the Close button is always enabled
            Test Step Comment: MMI_gen 9199;(partly: Special);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press the ‘Spec’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Special window." + Environment.NewLine +
                                "2. The ‘Close’ button is enabled");

            /*
            Test Step 2
            Action: Press ‘Close’ button
            Expected Result: The Special window is closed. DMI displays the default window
            Test Step Comment: MMI_gen 9199;(partly: Special); MMI_gen 8785 (partly: Special);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button in the Special Window");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI closes the Special window and displays the Default window.");

            /*
            Test Step 3
            Action: Press ‘Spec’ button again
            Expected Result: The Special window is displayed
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Spec’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Special window." + Environment.NewLine +
                                "2. The ‘Close’ button is enabled");

            /*
            Test Step 4
            Action: Press ‘Adhesion’ button
            Expected Result: The Adhesion window is displayed. Verify that the Close button is always enabled
            Test Step Comment: MMI_gen 9199 (partly: Adhesion);   
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press the ‘Adhesion’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Adhesion window." + Environment.NewLine +
                                "2. The ‘Close’ button is enabled");

            /*
            Test Step 5
            Action: Press ‘Close’ button
            Expected Result: Verify that the Special window is displayed
            Test Step Comment: MMI_gen 9199 (partly: Adhesion); MMI_gen 8785 (partly: Adhesion);
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button in the Adhesion Window");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI closes the Adhesion window and displays the Special window.");
            
            /*
            Test Step 6
            Action: Press ‘SR speed/distance’ button
            Expected Result: The SR speed/distance window is displayed. Verify that the Close button is always enabled
            Test Step Comment: MMI_gen 9199 (partly: SR speed/distance);   
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press the ‘SR speed/distance’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the SR speed/distance window." + Environment.NewLine +
                                "2. The ‘Close’ button is enabled");

            /*
            Test Step 7
            Action: Press ‘Close’ button
            Expected Result: DMI displays Special window
            Test Step Comment: MMI_gen 9199 (partly: SR speed/distance); MMI_gen 8785 (partly: SR speed/distance);
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button in the SR speed/distance Window");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI closes the SR speed/distance window and displays the Special window.");

            /*
            Test Step 8
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
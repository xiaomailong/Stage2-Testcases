using System;
using Testcase.Telegrams.EVCtoDMI;


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

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 0;
            // Testcase entrypoint
            TraceInfo("This test case requires an ATP configuration change - " +
                      "See Precondition requirements. If this is not done manually, the test may fail!");

            TraceHeader("Test Step 1");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Press ‘Spec’ button");
            TraceReport("Expected Result");
            TraceInfo("The Special window is displayed. Verify that the Close button is always enabled");
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

            TraceHeader("Test Step 2");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Press ‘Close’ button");
            TraceReport("Expected Result");
            TraceInfo("The Special window is closed. DMI displays the default window");
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

            TraceHeader("Test Step 3");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Press ‘Spec’ button again");
            TraceReport("Expected Result");
            TraceInfo("The Special window is displayed");
            /*
            Test Step 3
            Action: Press ‘Spec’ button again
            Expected Result: The Special window is displayed
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Spec’ button");

            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Special; // Special
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.Adhesion |
                                                               EVC30_MMIRequestEnable.EnabledRequests.SRSpeedDistance;
            EVC30_MMIRequestEnable.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Special window.");

            TraceHeader("Test Step 4");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Press ‘Adhesion’ button");
            TraceReport("Expected Result");
            TraceInfo("The Adhesion window is displayed. Verify that the Close button is always enabled");
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

            TraceHeader("Test Step 5");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Press ‘Close’ button");
            TraceReport("Expected Result");
            TraceInfo("Verify that the Special window is displayed");
            /*
            Test Step 5
            Action: Press ‘Close’ button
            Expected Result: Verify that the Special window is displayed
            Test Step Comment: MMI_gen 9199 (partly: Adhesion); MMI_gen 8785 (partly: Adhesion);
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button in the Adhesion Window");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI closes the Adhesion window and displays the Special window.");

            TraceHeader("Test Step 6");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Press ‘SR speed/distance’ button");
            TraceReport("Expected Result");
            TraceInfo("The SR speed/distance window is displayed. Verify that the Close button is always enabled");
            /*
            Test Step 6
            Action: Press ‘SR speed/distance’ button
            Expected Result: The SR speed/distance window is displayed. Verify that the Close button is always enabled
            Test Step Comment: MMI_gen 9199 (partly: SR speed/distance);   
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press the ‘SR speed/distance’ button");

            EVC11_MMICurrentSRRules.MMI_V_STFF = 40;
            EVC11_MMICurrentSRRules.MMI_L_STFF = 1000;
            EVC11_MMICurrentSRRules.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the SR speed/distance window." + Environment.NewLine +
                                "2. The ‘Close’ button is enabled");

            TraceHeader("Test Step 7");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Press ‘Close’ button");
            TraceReport("Expected Result");
            TraceInfo("DMI displays Special window");
            /*
            Test Step 7
            Action: Press ‘Close’ button
            Expected Result: DMI displays Special window
            Test Step Comment: MMI_gen 9199 (partly: SR speed/distance); MMI_gen 8785 (partly: SR speed/distance);
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button in the SR speed/distance Window");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI closes the SR speed/distance window and displays the Special window.");

            TraceHeader("Test Step 8");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("End of test");
            TraceReport("Expected Result");
            TraceInfo("");
            /*
            Test Step 8
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
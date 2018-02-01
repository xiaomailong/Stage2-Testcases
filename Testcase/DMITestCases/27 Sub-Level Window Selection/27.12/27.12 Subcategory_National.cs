using System;
using Testcase.Telegrams.EVCtoDMI;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 27.12 Subcategory ‘National’
    /// TC-ID: 22.12
    /// 
    /// This test case verifies the enabled button of subcategory National. The subcategory National shall enabled when ETCS Onboard is in SN or SE mode and if at least one of its subordinated function is enabled. Unless state otherwise, the subcategory National shall disabled.
    /// 
    /// Tested Requirements:
    /// MMI_gen 1547 (partly);
    /// 
    /// Scenario:
    /// Activate cabin A. 
    /// 1.Enter the Driver ID and perform brake test.
    /// 2.Perform SoM until SB mode, level 1.
    /// 
    /// Used files:
    /// N/A
    /// </summary>
    public class TC_22_12_Subcategory_National : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // System is power on.

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
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 0;
            // Testcase entrypoint


            TraceHeader("Test Step 1");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Activate cabin A. Driver performs SoM in SB mode, level 1");
            TraceReport("Expected Result");
            TraceInfo("DMI displays in SB mode, Level 1");
            /*
            Test Step 1
            Action: Activate cabin A. Driver performs SoM in SB mode, level 1
            Expected Result: DMI displays in SB mode, Level 1
            */
            DmiActions.Complete_SoM_L1_SB(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in SB mode, level 1.");

            TraceHeader("Test Step 2");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Press ‘Settings’ button");
            TraceReport("Expected Result");
            TraceInfo(
                "The Settings window is displayed all sub-menus.Verify that the button for subcategory National is disabled");
            /*
            Test Step 2
            Action: Press ‘Settings’ button
            Expected Result: The Settings window is displayed all sub-menus.Verify that the button for subcategory National is disabled
            Test Step Comment: MMI_gen 1547 (partly);
            */
            DmiActions.ShowInstruction(this, @"Press ‘Settings’ button");
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.NationalSystem;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Settings window with all sub-menus." + Environment.NewLine +
                                "2. The ‘National’ button is displayed disabled.");

            TraceHeader("Test Step 3");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("End of test");
            TraceReport("Expected Result");
            TraceInfo("");
            /*
            Test Step 3
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
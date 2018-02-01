using System;
using Testcase.Telegrams.EVCtoDMI;

namespace Testcase.DMITestCases
{
    /// <summary>
    /// 22.2.2 Planning Area-Layering: Display information when PA data is empty
    /// TC-ID: 17.2.2
    /// 
    /// This test case verify the display information of Planning Area when there is no Planning information is available.
    /// 
    /// Tested Requirements:
    /// MMI_gen 7109;
    /// 
    /// Scenario:
    /// Activate Cabin A.Perform SoM in SR mode, Level 
    /// 1.Then, verify that DMI displays only PA Distance Scale and PASP.
    /// 
    /// Used files:
    /// N/A
    /// </summary>
    public class TC_ID_17_2_2_Planning_Area : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // Set the following tags name in configuration file (See the instruction in Appendix 1)HIDE_PA_SR_MODE = 1System is power ON

            // Call the TestCaseBase PreExecution
            base.PreExecution();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in SR mode, Level 1.

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
            TraceInfo("Activate cabin A");
            TraceReport("Expected Result");
            TraceInfo("DMI displays Driver ID window");
            /*
            Test Step 1
            Action: Activate cabin A
            Expected Result: DMI displays Driver ID window
            */
            DmiActions.Activate_Cabin_1(this);

            EVC14_MMICurrentDriverID.MMI_X_DRIVER_ID = "1234";
            EVC14_MMICurrentDriverID.Send();

            DmiExpectedResults.Driver_ID_window_displayed(this);

            TraceHeader("Test Step 2");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Perform SoM in SR mode, Level 1");
            TraceReport("Expected Result");
            TraceInfo(
                "DMI displays in SR mode, level 1.Verify that there are only the following objects are displayed in PA,PA Distance Scale (0-4000m)PASP with PASP-dark-colour");
            /*
            Test Step 2
            Action: Perform SoM in SR mode, Level 1
            Expected Result: DMI displays in SR mode, level 1.Verify that there are only the following objects are displayed in PA,PA Distance Scale (0-4000m)PASP with PASP-dark-colour
            Test Step Comment: MMI_gen 7109;
            */
            DmiActions.Perform_SoM_in_SR_mode_Level_1(this);

            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.FullSupervision;

            WaitForVerification("Check that only the following objects are displayed:" + Environment.NewLine +
                                Environment.NewLine +
                                "1. PA Distance Scale." + Environment.NewLine +
                                "2. PASP (in the PASP-dark-colour");

            TraceHeader("Test Step 3");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("End of test");
            
            /*
            Test Step 3
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
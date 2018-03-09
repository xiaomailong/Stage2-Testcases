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
        public override bool TestcaseEntryPoint()
        {
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 23346;
            // Testcase entrypoint

            MakeTestStepHeader(1, UniqueIdentifier++, "Activate cabin A", "DMI displays Driver ID window");
            /*
            Test Step 1
            Action: Activate cabin A
            Expected Result: DMI displays Driver ID window
            */
            StartUp();

            EVC14_MMICurrentDriverID.MMI_X_DRIVER_ID = "1234";
            EVC14_MMICurrentDriverID.Send();

            DmiExpectedResults.Driver_ID_window_displayed(this);

            MakeTestStepHeader(2, UniqueIdentifier++, "Perform SoM in SR mode, Level 1",
                "DMI displays in SR mode, level 1.Verify that there are only the following objects are displayed in PA,PA Distance Scale (0-4000m)PASP with PASP-dark-colour");
            /*
            Test Step 2
            Action: Perform SoM in SR mode, Level 1
            Expected Result: DMI displays in SR mode, level 1.Verify that there are only the following objects are displayed in PA,PA Distance Scale (0-4000m)PASP with PASP-dark-colour
            Test Step Comment: MMI_gen 7109;
            */
            DmiActions.Complete_SoM_L1_SR(this);

            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.FullSupervision;

            WaitForVerification("Check that only the following objects are displayed:" + Environment.NewLine +
                                Environment.NewLine +
                                "1. PA Distance Scale." + Environment.NewLine +
                                "2. PASP (in the PASP-dark-colour");

            TraceHeader("End of test");

            /*
            Test Step 3
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
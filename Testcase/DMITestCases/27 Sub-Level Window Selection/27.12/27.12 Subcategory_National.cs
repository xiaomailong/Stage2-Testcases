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
        public override bool TestcaseEntryPoint()
        {
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 25505;
            // Testcase entrypoint


            MakeTestStepHeader(1, UniqueIdentifier++, "Activate cabin A. Driver performs SoM in SB mode, level 1",
                "DMI displays in SB mode, Level 1");
            /*
            Test Step 1
            Action: Activate cabin A. Driver performs SoM in SB mode, level 1
            Expected Result: DMI displays in SB mode, Level 1
            */
            StartUp();
            DmiActions.Complete_SoM_L1_SB(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in SB mode, level 1.");

            MakeTestStepHeader(2, UniqueIdentifier++, "Press ‘Settings’ button",
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
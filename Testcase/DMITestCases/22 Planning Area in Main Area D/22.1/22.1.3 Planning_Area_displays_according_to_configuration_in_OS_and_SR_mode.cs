using System;
using Testcase.Telegrams.EVCtoDMI;

namespace Testcase.DMITestCases
{
    /// <summary>
    /// 22.1.3 Planning Area displays according to configuration in OS and SR mode.
    /// TC-ID: 17.1.3
    /// 
    /// This test case verifies the presentation of planning area in area D in case of  driver configured the planning area to display in OS mode or SR mode. The planning area shall display and comply with conditions of [MMI-ETCS-gen].
    /// 
    /// Tested Requirements:
    /// MMI_gen 3063 (SR and OS mode); MMI_gen 7101; MMI_gen 7104;
    /// 
    /// Scenario:
    /// Activate Cabin A.Perform SoM in SR mode, Level 
    /// 1.Then, verify that PA is displayed.Drive train forward pass BG1 at 100m. Then, verify that PA is displayed.BG1: Packet 12, 21, 27 and 80
    /// 
    /// Used files:
    /// 17_1_3.tdg
    /// </summary>
    public class TC_ID_17_1_3_Planning_Area : TestcaseBase
    {
        public override bool TestcaseEntryPoint()
        {
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 23300;
            // Testcase entrypoint
            TraceInfo("This test case requires a DMI configuration change - " +
                      "See Precondition requirements. If this is not done manually, the test may fail!");

            MakeTestStepHeader(1, UniqueIdentifier++, "Activate cabin A", "DMI displays Driver ID window");
            /*
            Test Step 1
            Action: Activate cabin A
            Expected Result: DMI displays Driver ID window
            */
            // Call Generic Action Method
            StartUp();
            DmiActions.Set_Driver_ID(this, "1234");
            // Check Results
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Is the Driver ID window displayed.");

            MakeTestStepHeader(2, UniqueIdentifier++, "Driver performs SoM to SR mode, level 1",
                "DMI displays in SR mode, level 1.Verify that the Planning Area is displayed in area D.The Hide PA button is displayed on the planning area with ‘Scale up’ and ‘Scale Down’ buttons");
            /*
            Test Step 2
            Action: Driver performs SoM to SR mode, level 1
            Expected Result: DMI displays in SR mode, level 1.Verify that the Planning Area is displayed in area D.The Hide PA button is displayed on the planning area with ‘Scale up’ and ‘Scale Down’ buttons
            Test Step Comment: (1) MMI_gen 3063 (SR);   (1) MMI_gen 7101;   (2) MMI_gen 7104;
            */
            // Call Start of Mission Method
            DmiActions.Complete_SoM_L1_SR(this);
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Planning Area is displayed in Main Area D." + Environment.NewLine +
                                "2. The Hide PA button is displyed in sub area D14." + Environment.NewLine +
                                "3. The scale Up & Down buttons are displayed in sub area B.");

            MakeTestStepHeader(3, UniqueIdentifier++,
                "Drive the train forward passing BG1.Then, press an acknowledgement of OS mode symbol in area C1",
                "DMI displays in OS mode, level 1.Verify that the Planning Area is displayed the planning information in area D.The Hide PA button is displayed on the planning area with ‘Scale up’ and ‘Scale Down’ buttons");
            /*
            Test Step 3
            Action: Drive the train forward passing BG1.Then, press an acknowledgement of OS mode symbol in area C1
            Expected Result: DMI displays in OS mode, level 1.Verify that the Planning Area is displayed the planning information in area D.The Hide PA button is displayed on the planning area with ‘Scale up’ and ‘Scale Down’ buttons
            Test Step Comment: (1) MMI_gen 3063 (OS);   (1) MMI_gen 7101;   (2) MMI_gen 7104;
            */
            // Change to on sight Mode to emulate pasing BG1
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.OnSight;
            // Check Result
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Planning Area is displayed in Main Area D." + Environment.NewLine +
                                "2. On Sight Mode is displayed." + Environment.NewLine +
                                "3. The Hide PA button is displayed in sub area D14." + Environment.NewLine +
                                "4. The scale Up & Down buttons are displayed in sub area B.");

            MakeTestStepHeader(4, UniqueIdentifier++, "End of test", "");

            /*
            Test Step 4
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
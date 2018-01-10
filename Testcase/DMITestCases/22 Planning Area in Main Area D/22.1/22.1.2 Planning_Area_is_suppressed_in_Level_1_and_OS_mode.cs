using System;
using Testcase.Telegrams.EVCtoDMI;

namespace Testcase.DMITestCases
{
    /// <summary>
    /// 22.1.2 Planning Area is suppressed in Level 1 and OS mode
    /// TC-ID: 17.1.2
    /// 
    /// This test case verifies that the DMI allows to suppress the planning area in level 1 and On sight mode. The planning area configuration shall comply with conditions of [MMI-ETCS-gen]
    /// 
    /// Tested Requirements:
    /// MMI_gen 7102; MMI_gen 7101 (partly: disable OS);
    /// 
    /// Scenario:
    /// Activate Cabin A.Perform SoM in SR mode, Level 1.Drive train forward pass BG1 at 100m. Then, touch the main area D and verify that PA is not displayed on DMI.BG1: Packet 12, 21 and 27Drive train forward pass BG2 at 200m. Then, touch the main area D and verify that PA is not displayed on DMI.BG2: Packet 12, 21 ,27 and 80
    /// 
    /// Used files:
    /// 17_1_2.tdg
    /// </summary>
    public class TC_ID_17_1_2_Planning_Area : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // Set the following tags name in configuration file (See the instruction in Appendix 1)HIDE_PA_LEVEL_1 = 0 (Not show PA in the Level 1)HIDE_PA_OS_MODE = 0 (PA will not show in OS mode)HIDE_PA_FUNCTION = 0 (‘ON’ state)System is power ON.

            // Call the TestCaseBase PreExecution
            base.PreExecution();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in OS mode, level 1.

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint

            TraceInfo(
                "This test case requires a DMI configuration change; See Precondition requirements. If this is not done manually, the test may fail!");

            /*
            Test Step 1
            Action: Activate Cabin A
            Expected Result: DMI displays Driver ID window
            */
            // Call generic Action Method
            DmiActions.Activate_Cabin_1(this);
            DmiActions.Set_Driver_ID(this, "1234");
            // Call generic Check Results Method
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Is the Driver ID window displayed.");

            /*
            Test Step 2
            Action: Driver performs SoM to SR mode, level 1
            Expected Result: DMI displays in SR mode, level 1
            */
            // Call generic Action Method
            DmiActions.Complete_SoM_L1_SR(this);
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Does the DMI show Staff Responsible Mode.");


            // Test Step 3
            // Action: Drive train forward pass BG1
            // Expected Result: DMI change from SR mode to FS mode
            // Call Generic Method and Confirm result
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.FullSupervision;
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Does the DMI show Full Supervision Mode." + Environment.NewLine +
                                "2. Planning Area is displayed");


            // Test Step 4
            // Action: Touch main area D
            // Expected Result: Verify that the Planning Area is not displayed on DMI
            // Test Step Comment: MMI_gen 7102;
            // Call generic Check Results Method
            WaitForVerification("Touch main area D and please confirm:" + Environment.NewLine + Environment.NewLine +
                                "1. Planning area is NOT displayed.");

            /*
            Test Step 5
            Action: Drive train forward pass BG2.Then, press an acknowledgement of OS mode symbol in area C1
            Expected Result: DMI change from FS mode to OS mode
            */

            // Call Method to replace planning area on DMI
            DmiActions.Activate_Cabin_1(this);
            // Change mode to On Sight
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.OnSight;

            /*
            Test Step 6
            Action: Touch main area D
            Expected Result: Verify that the Planning Area is not displayed on DMI
            Test Step Comment: MMI_gen 7101 (partly: disable OS);
            */
            // Call generic Check Results Method
            WaitForVerification("Touch main area D and please confirm:" + Environment.NewLine + Environment.NewLine +
                                "1. Mode has changed to On Sight." + Environment.NewLine +
                                "2. Planning Area is NOT displayed");

            // Test Step 7
            // Action: End of test
            // Expected Result:

            return GlobalTestResult;
        }
    }
}
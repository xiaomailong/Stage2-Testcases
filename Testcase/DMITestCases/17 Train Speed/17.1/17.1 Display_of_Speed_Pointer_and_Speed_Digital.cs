using System;
using Testcase.Telegrams.EVCtoDMI;

namespace Testcase.DMITestCases
{
    /// <summary>
    /// 17.1 Display of Speed Pointer and Speed Digital
    /// TC-ID: 12.1
    /// 
    /// This test case verifies the presentation of speed pointer and speed digital which apply on train speed and display in sub-area B1 and B2. This shall comply with [ERA] standard and [MMI-ETCS-gen].
    /// 
    /// Tested Requirements:
    /// MMI_gen 1277; MMI_gen 5954; MMI_gen 11882;
    /// 
    /// Scenario:
    /// Driver performs SoM to SR mode, level 
    /// 1.Start driving the train forward with speed equal to 25 km/h.
    /// 
    /// Used files:
    /// N/A
    /// </summary>
    public class TC_12_1_Train_Speed : TestcaseBase
    {
        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in SR mode, Level 1.
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in SR mode, Level 1.");

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 0;
            // Testcase entrypoint
            StartUp();

            DmiActions.Complete_SoM_L1_SR(this);

            MakeTestStepHeader(1, UniqueIdentifier++, "Drive the train forward, speed up to 25 Km/h",
                "The train is at standstill");
            /*
            Test Step 1
            Action: Drive the train forward, speed up to 25 Km/h
            Expected Result:
            The speed pointer and the speed digital are displayed in area B1 with constantly movement and indicated the train speed at 25 km/h.
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 25;
            EVC1_MMIDynamic.MMI_V_PERMITTED = 30;

            WaitForVerification("1. Is the speed pointer showing 25 km/h?" + Environment.NewLine +
                                "2. Is the speed digital showing 25?" + Environment.NewLine +
                                "3. Are the speed pointer and speed digital both coloured light grey?");
            /*
            Test Step 2
            Action: Stop the train
            Expected Result: The train is at standstill
            */

            // Call generic Action Method
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 0;
            WaitForVerification("Are the speed pointer and speed digital showing 0?");

            MakeTestStepHeader(3, UniqueIdentifier++, "End of test", "");

            /*
            Test Step 3
            Action: End of test
            Expected Result: 
            */
            return GlobalTestResult;
        }
    }
}
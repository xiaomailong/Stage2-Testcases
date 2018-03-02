using Testcase.Telegrams.EVCtoDMI;
using System;

namespace Testcase.DMITestCases
{
    /// <summary>
    /// 6.8 Accleration/Decleration interval -4.0m/s2 to +4.0 m/s2
    /// TC-ID: 1.8
    /// 
    /// This test case is to verify ETCS MMI can handle accleration /decleration in the interval – 4m/s2 and +4m/s2 
    /// 
    /// Tested Requirements:
    /// MMI_gen 68;
    /// 
    /// Scenario:
    /// 1.Perform Start of Mission to L1, SR mode
    /// 2.Pass BG0 with MA and Track description
    /// 3.Mode changes to FS mode 
    /// 4.Increase the train speed with full acceleration until service barke is applied
    /// 5.Verify that the speedometer movement goes up and down smoothly
    /// 
    /// Used files:
    /// 1_8.tdg
    /// </summary>
    public class TC__ID_1_8_AcclerationDecleration : TestcaseBase
    {
        public override bool TestcaseEntryPoint()
        {
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 20422;
            // Testcase entrypoint

            MakeTestStepHeader(1, UniqueIdentifier++, "Power on the system and activate cabin",
                "DMI displays in SB mode");
            /*
            Test Step 1
            Action: Power on the system and activate cabin
            Expected Result: DMI displays in SB mode
            */


            MakeTestStepHeader(2, UniqueIdentifier++, "Perform Start of Mission to SR mode , level 1",
                "Mode changes to SR mode , Level 1");
            /*
            Test Step 2
            Action: Perform Start of Mission to SR mode , eevel 1
            Expected Result: Mode changes to SR mode , Level 1
            */


            MakeTestStepHeader(3, UniqueIdentifier++, "Pass BG0 with MA and Track description",
                "Mode changes to FS mode");
            /*
            Test Step 3
            Action: Pass BG0 with MA and Track description
            Expected Result: Mode changes to FS mode
            */

            DmiActions.Complete_SoM_L1_FS(this);

            DmiExpectedResults.FS_mode_displayed(this);

            MakeTestStepHeader(4, UniqueIdentifier++,
                "Full accelerate the traction (100%) until service brake is applied",
                "The speedometer movement goes up and down smoothly");

            for (short i = 0; i < 4020; i += 130)     // 4020 is just under 90 mph
                // Train speed increases by 130 cm per 300 ms when acceleration is 4 m/s/s
            {
                EVC1_MMIDynamic.MMI_V_TRAIN = i;
                Wait_Realtime(300);     // Cycle time of EVC-1 is 300 ms
            }

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Did the speed needle increase smoothly from 0 to approximately 90 mph?");

            for (short i = 4020; i > 0; i -= 130)     // 4020 is just under 90 mph
                // Train speed decreases by 130 cm per 300 ms when deceleration is 4 m/s/s
            {
                EVC1_MMIDynamic.MMI_V_TRAIN = i;
                Wait_Realtime(300);     // Cycle time of EVC-1 is 300 ms
            }

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Did the speed needle decrease smoothly from 90 to almost 0 mph?");

            /*
            Test Step 4
            Action: Full accelerate the traction (100%) until service brake is applied
            Expected Result: The speedometer movement goes up and down smoothly
            Test Step Comment: MMI_gen 68;
            */

            MakeTestStepHeader(5, UniqueIdentifier++, "End of Test", "");

            /*
            Test Step 5
            Action: End of Test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
using System;
using Testcase.Telegrams.EVCtoDMI;

namespace Testcase.DMITestCases
{
    /// <summary>
    /// Accleration/Decleration interval -4.0m/s2 to +4.0 m/s2
    /// TC-ID: 1.8
    /// Doors unique ID: TP-35750
    /// This test case is to verify ETCS MMI can handle accleration /decleration in the interval – 4m/s2 and +4m/s2 
    /// 
    /// Tested Requirements:
    /// MMI_gen 68;
    /// 
    /// Scenario:
    /// 1. Perform Start of Mission to L1, SR mode
    /// 2. Pass BG0 with MA and Track description
    /// 3. Mode changes to FS mode 
    /// 4. Increase the train speed with full acceleration until service barke is applied
    /// 5. Verify that the speedometer movement goes up and down smoothly
    /// 
    /// Used files:
    /// 1_8.tdg
    /// </summary>
    public class TC_ID_1_8_AccelerationAndDecelerationInterval : TestcaseBase
    {
        public override void PreExecution()
        {
            /* Pre-conditions from TestSpec
            	Power off test system
            	Set the following OTE configuration file (OteCfg_PC.cfg)
            	- tractionAcceleration 400- serviceBrakeDeceleration 400- emergencyBrakeDeceleration 400
            */

            TraceInfo("Pre-condition: " + "Power off test system" + Environment.NewLine +
                      "Set the following OTE configuration file (OteCfg_PC.cfg)" + Environment.NewLine +
                      "- tractionAcceleration 400- serviceBrakeDeceleration 400- emergencyBrakeDeceleration 400");

            base.PreExecution();
        }

        public override void PostExecution()
        {
            /* Post-conditions from TestSpec
            	DMI displays in FS mode.
            */

            TraceInfo("Post-condition: " + "DMI displays in FS mode.");

            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            /*
            Test Step 1
            Action:
            	Power on the system and activate cabin
            Expected Result:
            	DMI displays in SB mode
            */
            MakeTestStepHeader(1, 35765, "Power on the system and activate cabin", "DMI displays in SB mode");

            /*
            Test Step 2
            Action:
            	Perform Start of Mission to SR mode , eevel 1
            Expected Result:
            	Mode changes to SR mode , Level 1
            */
            MakeTestStepHeader(2, 35766, "Perform Start of Mission to SR mode , eevel 1",
                "Mode changes to SR mode , Level 1");

            /*
            Test Step 3
            Action:
            	Pass BG0 with MA and Track description
            Expected Result:
            	Mode changes to FS mode
            */
            MakeTestStepHeader(3, 35767, "Pass BG0 with MA and Track description", "Mode changes to FS mode");

            DmiActions.Complete_SoM_L1_FS(this);

            DmiExpectedResults.FS_mode_displayed(this);

            /*
            Test Step 4
            Action:
            	Full accelerate the traction (100%) until service brake is applied
            Expected Result:
            	The speedometer movement goes up and down smoothly
            Test Step Comment:
            	MMI_gen 68;
            */
            MakeTestStepHeader(4, 35768, "Full accelerate the traction (100%) until service brake is applied",
                "The speedometer movement goes up and down smoothly");

            for (short i = 0; i < 4020; i += 130) // 4020 is just under 90 mph
                // Train speed increases by 130 cm per 300 ms when acceleration is 4 m/s/s
            {
                EVC1_MMIDynamic.MMI_V_TRAIN = i;
                Wait_Realtime(300); // Cycle time of EVC-1 is 300 ms
            }

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Did the speed needle increase smoothly from 0 to approximately 90 mph?");

            for (short i = 4020; i > 0; i -= 130) // 4020 is just under 90 mph
                // Train speed decreases by 130 cm per 300 ms when deceleration is 4 m/s/s
            {
                EVC1_MMIDynamic.MMI_V_TRAIN = i;
                Wait_Realtime(300); // Cycle time of EVC-1 is 300 ms
            }

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Did the speed needle decrease smoothly from 90 to almost 0 mph?");

            /* End Of Test */
            TraceHeader("End Of Test");


            return GlobalTestResult;
        }
    }
}
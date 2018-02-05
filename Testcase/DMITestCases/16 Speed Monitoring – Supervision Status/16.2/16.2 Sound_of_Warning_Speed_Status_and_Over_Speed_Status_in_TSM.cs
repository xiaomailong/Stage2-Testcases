using System;
using Testcase.Telegrams.EVCtoDMI;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 16.2 Sound of Warning Speed Status and Over Speed Status in TSM
    /// TC-ID: 11.2
    /// 
    /// This test case verifes the sound played by DMI when current train speed is above the permitted speed for TSM supervision status. The sound S1 is played complied with [ERA] and the sound S2 is played when received MMI_M_WARNING = 5.
    /// 
    /// Tested Requirements:
    /// MMI_gen 5839; MMI_gen 5843; MMI_gen 4256 (partly: Sound S1 and S2 sound); MMI_gen 11921 (partly: MMI_M_WARNING = 5);
    /// 
    /// Scenario:
    /// 1.Drive the train forward pass BG1 at position 100m.      BG1: Packet 12, 21 and 27 (Entering FS)
    /// 2.Drive the train with specific speed. Then, verify the sound is playing refer to received packet EVC-1.
    /// 
    /// Used files:
    /// 11_2.tdg
    /// </summary>
    public class TC_ID_11_2_Speed_Monitoring : TestcaseBase
    {
        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in FS mode, Level 1.
            // Call generic Check Results Method
            DmiExpectedResults.FS_mode_displayed(this);

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 0;
            // Testcase entrypoint
            StartUp();

            MakeTestStepHeader(1, UniqueIdentifier++,
                "Drive the train forward with speed = 40km/h pass BG1 at position 100m",
                "DMI displays in FS mode, Level 1");
            /*
            Test Step 1
            Action: Drive the train forward with speed = 40km/h pass BG1 at position 100m
            Expected Result: DMI displays in FS mode, Level 1
            */
            // Call generic Check Results Method
            //?? what about permitted speed
            EVC1_MMIDynamic.MMI_V_PERMITTED_KMH = 40;
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 40;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.FullSupervision;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Is the speed pointer displaying 40 km/h?");

            MakeTestStepHeader(2, UniqueIdentifier++, "Drive the train with speed = 41 km/h",
                "Verify the following information,(1)     The sound ‘S1’ is played once.(2)     Use the log file to confirm that DMI received packet EVC-1 with variable MMI_M_WARNING = 9 (OvS and IndS, supervision = TSM)");
            /*
            Test Step 2
            Action: Drive the train with speed = 41 km/h
            Expected Result: Verify the following information,(1)     The sound ‘S1’ is played once.(2)     Use the log file to confirm that DMI received packet EVC-1 with variable MMI_M_WARNING = 9 (OvS and IndS, supervision = TSM)
            Test Step Comment: (1) MMI_gen 5839 (partly: play sound S1 once); MMI_gen 4256 (partly: Sound S1 sound);(2) MM MMI_gen 5839 (partly: MMI_M_WARNING);
            */
            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Overspeed_Status_Indication_Status_Target_Speed_Monitoring;
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 41; //  increase speed    
            // Send??          
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Is the speed pointer displaying 41 km/h?" + Environment.NewLine +
                                "2. Sound S1 is played once.");

            MakeTestStepHeader(3, UniqueIdentifier++,
                "Drive the train with speed = 45 km/hNote: dV_warning_max is defined in chapter 3 of [SUBSET-026]",
                "Verify the following information,(1)     The sound ‘S2’ is played continuously.(2)     Use the log file to confirm that DMI received packet EVC-1 with variable MMI_M_WARNING = 5 (WaS and IndS, supervision = TSM)");
            /*
            Test Step 3
            Action: Drive the train with speed = 45 km/hNote: dV_warning_max is defined in chapter 3 of [SUBSET-026]
            Expected Result: Verify the following information,(1)     The sound ‘S2’ is played continuously.(2)     Use the log file to confirm that DMI received packet EVC-1 with variable MMI_M_WARNING = 5 (WaS and IndS, supervision = TSM)
            Test Step Comment: (1) MMI_gen 5843 (partly: continuously play sound S2); MMI_gen 4256 (partly: Sound S2 sound); MMI_gen 11921 (partly: MMI_M_WARNING = 5);(2) MM MMI_gen 5843 (partly: MMI_M_WARNING);
            */
            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Warning_Status_Indication_Status_Target_Speed_Monitoring;
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 45; // increase speed  
            // Send??
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Is the speed pointer displaying 45 km/h?" + Environment.NewLine +
                                "2. Sound S2 is played continuously.");

            MakeTestStepHeader(4, UniqueIdentifier++, "Drive the train with spped = 40 km/h",
                "Verify the following information,(1)     The sound ‘S2’ is muted");
            /*
            Test Step 4
            Action: Drive the train with spped = 40 km/h
            Expected Result: Verify the following information,(1)     The sound ‘S2’ is muted
            Test Step Comment: (1) MMI_gen 11921 (partly: NEGATIVE, MMI_M_WARNING ≠ 5);
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 40; // decrease speed to 40 kmh  
            // Send??
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Is the speed pointer displaying 40 km/h?" + Environment.NewLine +
                                "2. Sound S2 is muted.");

            MakeTestStepHeader(5, UniqueIdentifier++, "End of test", "");

            /*
            Test Step 5
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
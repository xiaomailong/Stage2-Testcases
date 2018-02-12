using System;
using Testcase.Telegrams.EVCtoDMI;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 26.1.1 Sound S1 - Over Speed
    /// TC-ID: 21.1.1
    /// 
    /// This test case verifies that sound ‘S1_toofast.wav’ is played once when current train speed is exceeded permitted supervision limit in TSM supervision.
    /// 
    /// Tested Requirements:
    /// MMI_gen 11919;
    /// 
    /// Scenario:
    /// 1. Perform SoM to Level 1 in SR mode.
    /// 2. Drive the train forward with speed at 40 km/h.
    /// 3. Train runs pass BG1 at position 100 m.that contained pkt 12, pkt 21 and pkt 27 to enter FS mode.
    /// 4. Stop the train.
    /// 5. Use XML script to send EVC-1 to the DMI.
    /// 
    /// Used files:
    /// 21_1_1.tdg and 21_1_1.xml
    /// </summary>
    public class TC_ID_21_1_1_Sound_S1_Over_Speed : TestcaseBase
    {
        public override bool TestcaseEntryPoint()
        {
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 24650;
            // Testcase entrypoint

            MakeTestStepHeader(1, UniqueIdentifier++, "Perform SoM to Level 1 in SR mode.",
                "ETCS OB enters SR mode in Level 1.");
            /*
            Test Step 1
            Action: Perform SoM to Level 1 in SR mode.
            Expected Result: ETCS OB enters SR mode in Level 1.
            Test Step Comment: 
            */
            StartUp();
            DmiActions.Complete_SoM_L1_SR(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in SR mode, Level 1.");

            MakeTestStepHeader(2, UniqueIdentifier++, "Drive the train forward with constant speed at 40 km/h.",
                "The train can drive forward and all brakes are not applied.");
            /*
            Test Step 2
            Action: Drive the train forward with constant speed at 40 km/h.
            Expected Result: The train can drive forward and all brakes are not applied.
            Test Step Comment: 
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 40;
            EVC1_MMIDynamic.MMI_V_PERMITTED_KMH = 50;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI does not displays the Brake Intervention symbol, ST01.");

            MakeTestStepHeader(3, UniqueIdentifier++, "Train runs pass BG1 and keep train speed at 40 km/h.",
                "ETCS OB enters FS mode in Level 1.");
            /*
            Test Step 3
            Action: Train runs pass BG1 and keep train speed at 40 km/h.
            Expected Result: ETCS OB enters FS mode in Level 1.
            */
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.FullSupervision;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in FS mode, Level 1.");

            MakeTestStepHeader(4, UniqueIdentifier++,
                "Train enters TSM supervision and the permitted speed is gradually reduced until below the current train speed.",
                "Sound ‘S1_toofast.wav’ is played once when over-speed status in TSM supervision is active as figure below.	Use log file to verify that train speed is exceeded permitted supervision limit in TSM when DMI receives EVC-1 with variable [MMI_M_WARNING = 9]");
            /*
            Test Step 4
            Action: Train enters TSM supervision and the permitted speed is gradually reduced until below the current train speed.
            Expected Result: Sound ‘S1_toofast.wav’ is played once when over-speed status in TSM supervision is active as figure below.	Use log file to verify that train speed is exceeded permitted supervision limit in TSM when DMI receives EVC-1 with variable [MMI_M_WARNING = 9]
            Test Step Comment: MMI_gen 11919 (tested with TDG file)Note Sound file is stored in DMI_ERTMS_BL3 product in database path:/proj/ccmbkk3/mmi_v.
            */
            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Overspeed_Status_Indication_Status_Target_Speed_Monitoring;
            EVC1_MMIDynamic.MMI_V_PERMITTED_KMH = 37; // below current speed
            EVC1_MMIDynamic.MMI_V_TARGET_KMH = 0;
            EVC1_MMIDynamic.MMI_V_INTERVENTION_KMH = 45;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the current speed, 40 km/h." + Environment.NewLine +
                                "2. The speed pointer is orange." + Environment.NewLine +
                                "3. The circular speed gauge is yellow from 0 to 37 km/h." + Environment.NewLine +
                                "4. The circular speed gauge is orange from 37 to 45 km/h.");

            MakeTestStepHeader(5, UniqueIdentifier++, "Stop the train.", "The train is at standstill.");
            /*
            Test Step 5
            Action: Stop the train.
            Expected Result: The train is at standstill.
            Test Step Comment: 
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 0;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The speed displayed is 0 km/h.");

            MakeTestStepHeader(6, UniqueIdentifier++,
                "SUse test script 21_1_1.xml to send dynamic information via EVC-1 with: MMI_M_WARNING = 9 MMI_V_TRAIN = 1107 MMI_V_PERMITTED = 1101 MMI_V_INTERVENTION = 1242",
                "Sound ‘S1_toofast.wav’ is played once");
            /*
            Test Step 6
            Action: SUse test script 21_1_1.xml to send dynamic information via EVC-1 with: MMI_M_WARNING = 9 MMI_V_TRAIN = 1107 MMI_V_PERMITTED = 1101 MMI_V_INTERVENTION = 1242
            Expected Result: Sound ‘S1_toofast.wav’ is played once
            Test Step Comment: MMI_gen 11919 (tested with XML script)
            */

            #region Send_XML_21_1_1_DMI_Test_Specification

            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Overspeed_Status_Indication_Status_Target_Speed_Monitoring;
            EVC1_MMIDynamic.MMI_V_TRAIN = 1107;
            EVC1_MMIDynamic.MMI_V_PERMITTED = 1101; // below current speed
            EVC1_MMIDynamic.MMI_V_TARGET_KMH = 0;
            EVC1_MMIDynamic.MMI_V_INTERVENTION = 1242;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Overspeed’sound (‘S1_toofast.wav’) is played once.");

            #endregion

            MakeTestStepHeader(7, UniqueIdentifier++, "End of test", "");

            /*
            Test Step 7
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
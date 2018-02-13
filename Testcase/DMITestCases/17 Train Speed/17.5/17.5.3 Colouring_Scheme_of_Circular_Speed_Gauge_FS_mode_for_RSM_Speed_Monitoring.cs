using System;
using Testcase.Telegrams.EVCtoDMI;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 17.5.3 Colouring Scheme of Circular Speed Gauge (FS mode for RSM Speed Monitoring)
    /// TC-ID: 12.5.3
    /// 
    /// This test case verifies the display of CSG according to received packet information EVC-1 and EVC-7 including with sound played when Warning Status is active.
    /// 
    /// Tested Requirements:
    /// MMI_gen 972 (partly: OBU_TR_M_MODE , MMI_V_RELEASE, RSM); MMI_gen 6310 (partly: mode, release speed); MMI_gen 5902; MMI_gen 1182 (partly: Vrelease);
    /// 
    /// Scenario:
    /// 1.Drive the train forward with specified speed. Then verify the display of CSG refer to received packet information EVC-7 and EVC-1.
    /// 
    /// Used files:
    /// 12_5_3.tdg
    /// </summary>
    public class TC_12_5_3_Train_Speed : TestcaseBase
    {
        public override bool TestcaseEntryPoint()
        {
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 21659;
            // Testcase entrypoint
            StartUp();

            DmiActions.Complete_SoM_L1_SR(this);

            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.FullSupervision;

            MakeTestStepHeader(1, UniqueIdentifier++, "Drive the train forward pass BG1 with speed = 30km/h",
                "DMI displays in FS mode, Level 1.Verify the following information,(1)    Use the log file to confirm that DMI received packet EVC-7 with variable OBU_TR_M_MODE = 0 (Full Supervision mode).(2)   Use the log file to confirm that DMI received packet EVC-1 with following variables, MMI_M_WARNING = 3 (Status=IndS, Supervision=RSM).MMI_V_RELEASE =  1388 (50 km/h)(3)   All section of CSG is yellow colour");
            /*
            Test Step 1
            Action: Drive the train forward pass BG1 with speed = 30km/h
            Expected Result: DMI displays in FS mode, Level 1.Verify the following information,(1)    Use the log file to confirm that DMI received packet EVC-7 with variable OBU_TR_M_MODE = 0 (Full Supervision mode).(2)   Use the log file to confirm that DMI received packet EVC-1 with following variables, MMI_M_WARNING = 3 (Status=IndS, Supervision=RSM).MMI_V_RELEASE =  1388 (50 km/h)(3)   All section of CSG is yellow colour
            Test Step Comment: (1) MMI_gen 972 (partly: OBU_TR_M_MODE); MMI_gen 6310 (partly: mode);(2) MMI_gen 972 (partly: MMI_V_RELEASE); MMI_gen 6310 (partly: release speed); MMI_gen 5902 (partly: MMI_M_WARNING = 3);(3) MMI_gen 972 (partly: FS mode, RSM, IndS,  Vtarget <= CSG <= Vperm); MMI_gen 1182 (partly: Vrelease);
            */
            // Doc says TARGET to PERM is yellow in this mode but medium-grey from 0 to release! Bad test?
            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Indication_Status_Release_Speed_Monitoring;
            EVC1_MMIDynamic.MMI_V_TARGET_KMH = 20;
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 30;
            EVC1_MMIDynamic.MMI_V_RELEASE = 1388;
            EVC1_MMIDynamic.MMI_V_PERMITTED_KMH = 40;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in FS mode, level 1." + Environment.NewLine +
                                "2. All sections of CSG are in yellow");

            MakeTestStepHeader(2, UniqueIdentifier++, "Continue to drive the train forward with speed = 51 km/h",
                "Verify the following information,(1)   Use the log file to confirm that DMI received packet EVC-1 with following variables, MMI_M_WARNING = 15 (Status=IntS and Inds, Supervision=RSM).(2)   All section of CSG is yellow colour");
            /*
            Test Step 2
            Action: Continue to drive the train forward with speed = 51 km/h
            Expected Result: Verify the following information,(1)   Use the log file to confirm that DMI received packet EVC-1 with following variables, MMI_M_WARNING = 15 (Status=IntS and Inds, Supervision=RSM).(2)   All section of CSG is yellow colour
            Test Step Comment: (1) MMI_gen 972 (partly: MMI_V_RELEASE); MMI_gen 6310 (partly: release speed); MMI_gen 5902 (partly: MMI_M_WARNING = 15);(2) MMI_gen 972 (partly: FS mode, RSM, IntS, Vtarget <= CSG <= Vperm); MMI_gen 1182 (partly: Vrelease);
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 51;
            EVC1_MMIDynamic.MMI_M_WARNING =
                MMI_M_WARNING.Intervention_Status_Indication_Status_Release_Speed_Monitoring;
            // ?? Send
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. All sections of CSG are in yellow");

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
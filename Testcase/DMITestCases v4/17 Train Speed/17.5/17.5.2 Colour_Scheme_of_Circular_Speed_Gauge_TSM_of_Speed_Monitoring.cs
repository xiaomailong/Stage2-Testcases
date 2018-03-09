using System;
using Testcase.Telegrams.EVCtoDMI;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 17.5.2 Colour Scheme of Circular Speed Gauge (TSM of Speed Monitoring)
    /// TC-ID: 12.5.2
    /// 
    /// This test case verifies the display of CSG according to received packet information EVC-1 and EVC-7.
    /// 
    /// Tested Requirements:
    /// MMI_gen 972 (partly: OBU_TR_M_MODE , MMI_V_TARGET, TSM); MMI_gen 6310 (partly: mode, target speed); MMI_gen 1182 (partly: Vtarget);
    /// 
    /// Scenario:
    /// 1.Drive the train forward with specified speed. Then verify the display of CSG refer to received packet information EVC-7 and EVC-1.
    /// 
    /// Used files:
    /// 12_5_2.tdg
    /// </summary>
    public class TC_12_5_2_Train_Speed : TestcaseBase
    {
        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in FS mode, Level 1.
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in FS mode, Level 1.");

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 21647;
            // Testcase entrypoint
            StartUp();
            DmiActions.Complete_SoM_L1_SR(this);

            MakeTestStepHeader(1, UniqueIdentifier++, "Drive the train forward pass BG1 with speed = 30km/h",
                "DMI displays in FS mode, Level 1.Verify the following information," +
                "(1) Use the log file to confirm that DMI received packet EVC-7 with variable OBU_TR_M_MODE = 0 (Full Supervision mode)." +
                "(2) Use the log file to confirm that DMI received packet EVC-1 with following variables, MMI_M_WARNING = 11 (Status=Nos, Supervision=TSM). MMI_V_TARGET =  278 (10 km/h)" +
                "(3) At range 0-10 km/h, CSG is dark-grey colour." +
                "(4) At range above 11 km/h, CSG is white colour");
            /*
            Test Step 1
            Action: Drive the train forward pass BG1 with speed = 30 km/h
            Expected Result: DMI displays in FS mode, Level 1.Verify the following information,
            (1) Use the log file to confirm that DMI received packet EVC-7 with variable OBU_TR_M_MODE = 0 (Full Supervision mode).
            (2) Use the log file to confirm that DMI received packet EVC-1 with following variables, MMI_M_WARNING = 11 (Status=Nos, Supervision=TSM). MMI_V_TARGET =  278 (10 km/h)
            (3) At range 0-10 km/h, CSG is dark-grey colour.
            (4) At range above 11 km/h, CSG is white colour
            Test Step Comment: (1) MMI_gen 972 (partly: OBU_TR_M_MODE); MMI_gen 6310 (partly: mode);(2) MMI_gen 972 (partly: MMI_V_TARGET); MMI_gen 6310 (partly: target speed); (3) MMI_gen 972 (partly: FS mode, TSM, NoS,  0 <= CSG <= Vtarget); MMI_gen 1182 (partly: Vrelease);(3) MMI_gen 972 (partly: FS mode, TSM, NoS,  Vtarget <= CSG <= Vpermi);
            */
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.FullSupervision;
            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Normal_Status_Target_Speed_Monitoring;
            EVC1_MMIDynamic.MMI_V_TARGET_KMH = 10;
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 30;
            EVC1_MMIDynamic.MMI_V_PERMITTED_KMH = 40;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in FS mode, Level 1." + Environment.NewLine +
                                "2. Between 0 - 10 km/h, CSG is dark-grey in colour." + Environment.NewLine +
                                "3. Above 11 km/h, CSG is white in colour.");

            TraceHeader("End of test");

            /*
            Test Step 2
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
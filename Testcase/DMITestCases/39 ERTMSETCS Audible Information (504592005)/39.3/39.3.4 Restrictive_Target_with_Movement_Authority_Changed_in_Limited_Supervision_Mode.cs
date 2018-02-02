using System;
using Testcase.Telegrams.EVCtoDMI;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 39.3.4 Restrictive Target with Movement Authority Changed in Limited Supervision Mode
    /// TC-ID: 36.3.4
    /// 
    /// This test case verifies that sound ‘S info’ is not played when train is in LS mode even if distance to target or target speed is changed.
    /// 
    /// Tested Requirements:
    /// MMI_gen 12043 (partly: MA changed in LS mode);
    /// 
    /// Scenario:
    /// 1.Perform SoM to Level 1 in SR mode.
    /// 2.Drive the train forward with constant speed at 20 km/h.
    /// 3.Train runs pass BG1 at position 100 m. that contained pkt 12, pkt 21, pkt 27 and pkt 80 to enter LS mode.
    /// 4.Train runs pass BG2 at position 200 m. that contained pkt 12, pkt 21, pkt 27 and pkt 80 with reduction of target speed.
    /// 5.Stop the train.
    /// 
    /// Used files:
    /// 36_3_4.tdg
    /// </summary>
    public class
        TC_ID_36_3_4_Restrictive_Target_with_Movement_Authority_Changed_in_Limited_Supervision_Mode : TestcaseBase
    {

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint


            /*
            Test Step 1
            Action: Perform SoM to Level 1 in SR mode
            Expected Result: ETCS OB enters SR mode in Level 1
            */
            // Steps tested elsewhere: force SoM
            DmiActions.Complete_SoM_L1_SR(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in SR mode, Level 1.");

            /*
            Test Step 2
            Action: Drive the train forward with constant speed at 20 km/h
            Expected Result: The train can drive forward and all brakes are not applied
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 20;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI does not display the ‘Emergency brake’ symbol, ST01, in sub-area C9.");

            /*
            Test Step 3
            Action: Train runs pass BG1
            Expected Result: Sound Sinfo is not played when train pass BG1 with verification as follows:-Log FileUse log file to verify that after train pass BG1, train enters LS mode and restrictive target doesn’t exist in LS mode when the DMI receives the following:-- EVC-7 with variable [MMI_OBU_TR_M_Mode = 12].- EVC-1 with variable [MMI_V_TARGET = -1] all the time.- EVC-1 with variable [MMI_O_BRAKETARGET = -1] all the time
            Test Step Comment: MMI_gen 12043 (partly: MA changed in LS mode);
            */
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN = 10000;
            EVC1_MMIDynamic.MMI_V_TARGET = -1;
            EVC1_MMIDynamic.MMI_O_BRAKETARGET = -1;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode =
                EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.LimitedSupervision;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Sinfo’ sound is not played");

            /*
            Test Step 4
            Action: Train runs pass BG2
            Expected Result: Sound Sinfo is not played when train pass BG2 with verification as follows:-Log FileUse log file to verify that after train pass BG2, train is still in LS mode and restrictive target still doesn’t exist in LS mode when the DMI receives the following:-- EVC-7 with variable [MMI_OBU_TR_M_Mode = 12].- EVC-1 with variable [MMI_V_TARGET = -1] all the time.- EVC-1 with variable [MMI_O_BRAKETARGET = -1] all the time
            Test Step Comment: MMI_gen 12043 (partly: MA changed in LS mode);
            */
            // Call generic Action Method
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN = 20000;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Sinfo’ sound is (still) not played");

            /*
            Test Step 5
            Action: Stop the train
            Expected Result: The train is at standstill
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 0;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The speed pointer displays 0 km/h.");

            /*
            Test Step 6
            Action: End of test
            Expected Result: 
            */


            return GlobalTestResult;
        }
    }
}
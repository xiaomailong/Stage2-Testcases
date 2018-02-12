using System;
using Testcase.Telegrams.EVCtoDMI;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 39.3.3 Restrictive Target with Speed Monitoring in Limited Supervision Mode
    /// TC-ID: 36.3.3
    /// 
    /// This test case verifies that sound ‘S info’ is not played when train is in LS mode with speed monitoring changed
    /// 
    /// Tested Requirements:
    /// MMI_gen 12043 (partly: speed monitoring in LS mode);
    /// 
    /// Scenario:
    /// 1.Perform SoM to Level 1 in SR mode.
    /// 2.Drive the train forward with constant speed at 40 km/h.
    /// 3.Train runs pass BG1 at position 100 m. that contained pkt 12, pkt 21, pkt 27 and pkt 80 to enter LS mode.
    /// 4.Stop the train.
    /// 
    /// Used files:
    /// 36_3_3.tdg
    /// </summary>
    public class TC_ID_36_3_3_Restrictive_Target_with_Speed_Monitoring_in_Limited_Supervision_Mode : TestcaseBase
    {
        public override bool TestcaseEntryPoint()
        {
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 26898;
            // Testcase entrypoint

            MakeTestStepHeader(1, UniqueIdentifier++, "Perform SoM to Level 1 in SR mode",
                "ETCS OB enters SR mode in Level 1");
            /*
            Test Step 1
            Action: Perform SoM to Level 1 in SR mode
            Expected Result: ETCS OB enters SR mode in Level 1
            */
            // Steps tested elsewhere: force SoM
            StartUp();
            DmiActions.Complete_SoM_L1_SR(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in SR mode, Level 1.");

            MakeTestStepHeader(2, UniqueIdentifier++, "Drive the train forward with constant speed at 20 km/h",
                "The train can drive forward and all brakes are not applied");
            /*
            Test Step 2
            Action: Drive the train forward with constant speed at 20 km/h
            Expected Result: The train can drive forward and all brakes are not applied
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 20;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI does not display the ‘Emergency brake’ symbol, ST01, in sub-area C9.");

            MakeTestStepHeader(3, UniqueIdentifier++,
                "Drive the train forward pass BG1.Then, press an LS mode acknowledgement on sub-area C1",
                "ETCS OB enters LS mode in Level 1");
            /*
            Test Step 3
            Action: Drive the train forward pass BG1.Then, press an LS mode acknowledgement on sub-area C1
            Expected Result: ETCS OB enters LS mode in Level 1
            Test Step Comment: Note Sound ‘Sinfo’ is played once because driver’s acknowledgement is displayed, refers to MMI_gen 9393
            */
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 709;
            EVC8_MMIDriverMessage.Send();

            DmiActions.ShowInstruction(this, "Press in area C1 to acknowledge LS mode");

            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode =
                EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.LimitedSupervision;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in LS mode, Level 1." + Environment.NewLine +
                                "2. The ‘Sinfo’ sound is played once");

            MakeTestStepHeader(4, UniqueIdentifier++,
                "Continue to drive the train forward with constant speed at 20 km/h",
                "Sound ‘Sinfo’ is not played when train enters PIM, TSM and RSM with verification below:-Log FileUse log file to verify that when train enters PIM, TSM and RSM in LS mode, restrictive target doesn’t exist as follows:-- When train is in LS mode, the DMI receives EVC-7 with variable [MMI_OBU_TR_M_Mode = 12].- When train enters PIM, the DMI receives EVC-1 with variable [MMI_M_WARNING = 2].- When train enters TSM, the DMI receives EVC-1 with variable [MMI_M_WARNING = 11].- When train enters RSM, the DMI receives EVC-1 with variable [MMI_M_WARNING = 3 or 15].- The DMI receives EVC-1 with variable [MMI_V_TARGET = -1] all the time.- The DMI receives EVC-1 with variable [MMI_O_BRAKETARGET = -1] all the time");
            /*
            Test Step 4
            Action: Continue to drive the train forward with constant speed at 20 km/h
            Expected Result: Sound ‘Sinfo’ is not played when train enters PIM, TSM and RSM with verification below:-Log FileUse log file to verify that when train enters PIM, TSM and RSM in LS mode, restrictive target doesn’t exist as follows:-- When train is in LS mode, the DMI receives EVC-7 with variable [MMI_OBU_TR_M_Mode = 12].- When train enters PIM, the DMI receives EVC-1 with variable [MMI_M_WARNING = 2].- When train enters TSM, the DMI receives EVC-1 with variable [MMI_M_WARNING = 11].- When train enters RSM, the DMI receives EVC-1 with variable [MMI_M_WARNING = 3 or 15].- The DMI receives EVC-1 with variable [MMI_V_TARGET = -1] all the time.- The DMI receives EVC-1 with variable [MMI_O_BRAKETARGET = -1] all the time
            Test Step Comment: MMI_gen 12043 (partly: speed monitoring in LS mode)
            */
            EVC1_MMIDynamic.MMI_V_TARGET = -1;
            EVC1_MMIDynamic.MMI_O_BRAKETARGET = -1;
            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Normal_Status_PreIndication_Monitoring;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Sinfo’ sound is not played (again).");

            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Normal_Status_Target_Speed_Monitoring;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Sinfo’ sound is not played (again).");

            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Indication_Status_Release_Speed_Monitoring;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Sinfo’ sound is not played (again).");

            EVC1_MMIDynamic.MMI_M_WARNING =
                MMI_M_WARNING.Intervention_Status_Indication_Status_Release_Speed_Monitoring;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Sinfo’ sound is not played (again).");

            MakeTestStepHeader(5, UniqueIdentifier++, "Stop the train", "The train is at standstill");
            /*
            Test Step 5
            Action: Stop the train
            Expected Result: The train is at standstill
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 0;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The speed pointer displays 0 km/h.");

            TraceHeader("End of test");

            /*
            Test Step 6
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
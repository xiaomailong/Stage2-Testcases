using System;
using Testcase.Telegrams.EVCtoDMI;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 18.1.7.2 Distance to Target: Appearance of Distance to Target in OS mode
    /// TC-ID: 13.1.7.2
    /// 
    /// This test case verifies the display information of the distance to target bar and digital in OS mode. The display of distance to target bar and digital are comply with the received packet EVC-1 and EVC-7. 
    /// 
    /// Tested Requirements:
    /// MMI_gen 2567 (partly: Table 38, OS mode); MMI_gen 107 (partly: Table 37, OS mode); MMI_gen 6658; MMI_gen 6774;
    /// 
    /// Scenario:
    /// 1.Drive the train forward pass BG1 at 100m and confirm acknowledgement of OS mode. Then, verify the display of distance to target bar and digital with received packet information EVC-1 and EVC-
    /// 7.BG1: Packet 12, 21, 27 and 80 (Entering OS)
    /// 2.Continue to drive the train forward. Then, verify the display of distance to target bar and digital when the supervision status is changed.  Note: The consistency of information for the position in each test step and the location when the value of MMI_M_WARNING changed is able to verify in log file, EVC-7 variable OBU_TR_TRAIN.
    /// 
    /// Used files:
    /// 13_1_7_2.tdg
    /// </summary>
    public class TC_13_1_7_2_Brake : TestcaseBase
    {
        public override bool TestcaseEntryPoint()
        {
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 21968;
            // Testcase entrypoint
            StartUp();
            DmiActions.Complete_SoM_L1_SR(this);

            MakeTestStepHeader(1, UniqueIdentifier++,
                "Drive the train forward pass BG1. Then, press an acknowledgement in sub-area C1 and press on sub-area B to toggle the basic speed hook become visible",
                "DMI displays in OS mode, level 1Verify the following information(1)    Use the log file to confirm that DMI receives the following packets information with a specific value,  EVC-1: MMI_M_WARNING = 0 (Status = NoS, Supervision = CSM)MMI_O_BRAKETARGET = -1 (Default) EVC-7: OBU_TR_M_MODE = 1 (OS mode) (2)   The distance to target bar is not display in sub-area A3. (3)   The distance to target digital is not display in sub-area A2");
            /*
            Test Step 1
            Action: Drive the train forward pass BG1. Then, press an acknowledgement in sub-area C1 and press on sub-area B to toggle the basic speed hook become visible
            Expected Result: DMI displays in OS mode, level 1Verify the following information(1)    Use the log file to confirm that DMI receives the following packets information with a specific value,  EVC-1: MMI_M_WARNING = 0 (Status = NoS, Supervision = CSM)MMI_O_BRAKETARGET = -1 (Default) EVC-7: OBU_TR_M_MODE = 1 (OS mode) (2)   The distance to target bar is not display in sub-area A3. (3)   The distance to target digital is not display in sub-area A2
            Test Step Comment: (1) MMI_gen 107 (partly: MMI_M_WARNING, OBU_TR_M_MODE, OS mode CSM); MMI_gen 2567 (partly: MMI_M_WARNING, OBU_TR_M_MODE, OS mode CSM); MMI_gen 6658 (partly: MMI_O_BRAKETARGET is less than zero); MMI_gen 6774 (partly: MMI_O_BRAKETARGET is less than zero);(2) MMI_gen 6658 (partly: not be shown); MMI_gen 6674 (partly: not be shown); MMI_gen 107 (partly: Table 37, OS mode);(3) MMI_gen 2567 (partly: Table 38, OS mode CSM);
            */
            // Require a permitted and target speed for speed hook to be displayed

            EVC1_MMIDynamic.MMI_O_BRAKETARGET = -1;
            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Normal_Status_Ceiling_Speed_Monitoring;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN = 0; // just starting off


            EVC1_MMIDynamic.MMI_V_PERMITTED_KMH = 20;
            EVC1_MMIDynamic.MMI_V_TARGET_KMH = 15;
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 10;

            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN = 10000; // 100m
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 259;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.Send();

            DmiActions.ShowInstruction(this, "Press in sub-area C1 to acknowledge");

            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.OnSight;

            DmiActions.ShowInstruction(this,
                "Press on sub-area B to toggle the basic speed hook so that is is displayed");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Does the DMI delete the SR mode symbol (MO09) and replace it with the OS mode symbol (MO07) in area B7" +
                                Environment.NewLine +
                                "2. The distance to target bar is not displayed in sub-area A3." + Environment.NewLine +
                                "3. The digital distance to target is not displayed in sub-area A2.");

            MakeTestStepHeader(2, UniqueIdentifier++, "Continue to drive the train forward.Then, stop the train",
                "Verify the following information,(1)    Use the log file to confirm that DMI receives the packet information EVC-1 with following variables,MMI_M_WARNING = 2 (Status = NoS, Supervision = PIM)(2)    The distance to target bar is not display in sub-area A3.(3)   The distance to target digital is display in sub-area A2");
            /*
            Test Step 2
            Action: Continue to drive the train forward.Then, stop the train
            Expected Result: Verify the following information,(1)    Use the log file to confirm that DMI receives the packet information EVC-1 with following variables,MMI_M_WARNING = 2 (Status = NoS, Supervision = PIM)(2)    The distance to target bar is not display in sub-area A3.(3)   The distance to target digital is display in sub-area A2
            Test Step Comment: (1) MMI_gen 107 (partly: MMI_M_WARNING, OS mode); MMI_gen 2567 (partly: MMI_M_WARNING, OS mode PIM); (2) MMI_gen 107 (partly: Table 37, OS mode);(3) MMI_gen 2567 (partly: Table 38, OS mode PIM);
            */
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN = 81000; // 810m
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 0; // stop

            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Normal_Status_PreIndication_Monitoring;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The distance to target bar is not displayed in sub-area A3." + Environment.NewLine +
                                "2. The digital distance to target is not displayed in sub-area A2.");

            MakeTestStepHeader(3, UniqueIdentifier++, "Continue to drive the train forward.Then, stop the train",
                "Verify the following information,(1)    Use the log file to confirm that DMI receives the packet information EVC-1 with following variables,MMI_M_WARNING = 11 (Status = NoS, Supervision = TSM)(2)     The distance to target bar is not display in sub-area A3.(3)    The distance to target digital is display in sub-area A2");
            /*
            Test Step 3
            Action: Continue to drive the train forward.Then, stop the train
            Expected Result: Verify the following information,(1)    Use the log file to confirm that DMI receives the packet information EVC-1 with following variables,MMI_M_WARNING = 11 (Status = NoS, Supervision = TSM)(2)     The distance to target bar is not display in sub-area A3.(3)    The distance to target digital is display in sub-area A2
            Test Step Comment: (1) MMI_gen 107 (partly: MMI_M_WARNING, OS mode); MMI_gen 2567 (partly: MMI_M_WARNING, OS mode TSM);(2) MMI_gen 107 (partly: Table 37, OS mode);(3) MMI_gen 2567 (partly: Table 38, OS mode TSM);
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 10; // drive on
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN = 86000; // 860m
            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Normal_Status_Target_Speed_Monitoring;
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 0; // stop

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The distance to target bar is not displayed in sub-area A3." + Environment.NewLine +
                                "2. The digital distance to target is not displayed in sub-area A2.");

            MakeTestStepHeader(4, UniqueIdentifier++, "Continue to drive the train forward.Then, stop the train",
                "Verify the following information,(1)    Use the log file to confirm that DMI receives the packet information EVC-1 with following variables,MMI_M_WARNING = 3 (Status = Inds, Supervision = RSM)(2)     The distance to target bar is not display in sub-area A3.(3)    The distance to target digital is display in sub-area A2");
            /*
            Test Step 4
            Action: Continue to drive the train forward.Then, stop the train
            Expected Result: Verify the following information,(1)    Use the log file to confirm that DMI receives the packet information EVC-1 with following variables,MMI_M_WARNING = 3 (Status = Inds, Supervision = RSM)(2)     The distance to target bar is not display in sub-area A3.(3)    The distance to target digital is display in sub-area A2
            Test Step Comment: (1) MMI_gen 107 (partly: MMI_M_WARNING, OS mode); MMI_gen 2567 (partly: MMI_M_WARNING, OS mode RSM); (2) MMI_gen 107 (partly: Table 37, OS mode);(3) MMI_gen 2567 (partly: Table 38, OS mode RSM);
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 10; // drive on
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN = 106000; // 1060m                   // stop
            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Indication_Status_Release_Speed_Monitoring;
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 0; // stop

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The distance to target bar is not displayed in sub-area A3." + Environment.NewLine +
                                "2. The digital distance to target is not displayed in sub-area A2.");

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
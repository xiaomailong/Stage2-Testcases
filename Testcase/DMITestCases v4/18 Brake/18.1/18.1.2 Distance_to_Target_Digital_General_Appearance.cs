using System;
using Testcase.Telegrams.DMItoEVC;
using Testcase.Telegrams.EVCtoDMI;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 18.1.2 Distance to Target  Digital: General Appearance
    /// TC-ID: 13.1.2
    /// 
    /// This test case verifies the general appearance and properties of distance to target digital in sub-area A2.The distance to target digital shall comply with [ERA] standard and [MMI-ETCS-gen].
    /// 
    /// Tested Requirements:
    /// MMI_gen 6767; MMI_gen 6770; MMI_gen 6772; MMI_gen 6774; MMI_gen 2567 (partly: Table 38 'FS mode with PIM', 'TR mode', 'PT mode');
    /// 
    /// Scenario:
    /// Drive the train forward pass BG1 at 100m. Then, verify the display of distance to target digital in sub-area A2.BG1: Packet 12, 21 and 27 (Entering FS mode)Drive the train forward pass EOA (600m). Then, verify that distance to target digital is not shown in sub-area A2 and correspond to the received packet information EVC-1 and EVC-7.Acknowledge Trip mode. Then, verify that distance to target digital is not shown in sub-area A2 and correspond to the received packet information EVC-1 and EVC-7.
    /// 
    /// Used files:
    /// 13_1_2.tdg
    /// </summary>
    public class TC_ID_13_1_2_Brake : TestcaseBase
    {
        public override bool TestcaseEntryPoint()
        {
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 21884;
            // Testcase entrypoint
            StartUp();
            DmiActions.Complete_SoM_L1_SR(this);

            MakeTestStepHeader(1, UniqueIdentifier++, "Drive the train forward passing BG1 until entering FS mode",
                "Verify the following information,The distance to target digital is displayed in sub-area A2.The distance to target digital is vertically centered in sub-area A2.The distance to target digital is displayed in grey.Use the log file to confirm that DMI receives packet information EVC-1 and EVC-7 with following variables,OBU_TR_M_MODE (EVC-7) = 0 (Full Supervision)MMI_M_WARNING (EVC-1) = 2 (Status = NoS, Supervision = PIM)");
            /*
            Test Step 1
            Action: Drive the train forward passing BG1 until entering FS mode
            Expected Result: Verify the following information,The distance to target digital is displayed in sub-area A2.The distance to target digital is vertically centered in sub-area A2.The distance to target digital is displayed in grey.Use the log file to confirm that DMI receives packet information EVC-1 and EVC-7 with following variables,OBU_TR_M_MODE (EVC-7) = 0 (Full Supervision)MMI_M_WARNING (EVC-1) = 2 (Status = NoS, Supervision = PIM)
            Test Step Comment: (1) MMI_gen 6767;(2) MMI_gen 6770;(3) MMI_gen 6772;(4) MMI_gen 2567 (partly: Table 38 'FS mode with PIM');
            */
            // Set the permitted speed so the current speed is allowed
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 5;
            EVC1_MMIDynamic.MMI_V_PERMITTED_KMH = 10;

            // ?? Set an EOA so the DMI can display a target
            EVC1_MMIDynamic.MMI_O_BRAKETARGET = 60000; // 600 m
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN = 1000; // 100 m

            // Does the DMI automatically switch to FS mode??
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.FullSupervision;

            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Normal_Status_PreIndication_Monitoring;

            MakeTestStepHeader(2, UniqueIdentifier++,
                "Continue to drive the train forward pass EOA until entering TR mode",
                "Verify the following information,The distance to target digital is not shown in sub-area A2.Use the log file to confirm that DMI received packet information EVC-1 with vairable MMI_O_BRAKETARGET < 0.Use the log file to confirm that DMI received packet information EVC-7 with following variables,OBU_TR_M_MODE (EVC-7) = 7 (Trip)");
            /*
            Test Step 2
            Action: Continue to drive the train forward pass EOA until entering TR mode
            Expected Result: Verify the following information,The distance to target digital is not shown in sub-area A2.Use the log file to confirm that DMI received packet information EVC-1 with vairable MMI_O_BRAKETARGET < 0.Use the log file to confirm that DMI received packet information EVC-7 with following variables,OBU_TR_M_MODE (EVC-7) = 7 (Trip)
            Test Step Comment: (1) MMI_gen 6774 (partly: when MMI_O_BRAKETARGET less than zero);(2) MMI_gen 6774 (partly: received MMI_O_BRAKETARGET less than zero);(3) MMI_gen 2567 (partly: Table 38 ‘TR mode’);
            */
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN = 70000; // 700 m
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.Trip;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The digital distance to target is not displayed in sub-area A2.");

            MakeTestStepHeader(3, UniqueIdentifier++, "Stop the train. Then, acknowledge TR mode by press a sub-area C1",
                "DMI displays in PT mode, Level 1.Verify the following information,The distance to target digital is not shown in sub-area A2.Use the log file to confirm that DMI received packet information EVC-1 with vairable MMI_O_BRAKETARGET < 0.Use the log file to confirm that DMI received packet information EVC-7 with following variables,OBU_TR_M_MODE (EVC-7) = 8 (Post Trip)");
            /*
            Test Step 3
            Action: Stop the train.Then, acknowledge TR mode by press a sub-area C1
            Expected Result: DMI displays in PT mode, Level 1.Verify the following information,The distance to target digital is not shown in sub-area A2.Use the log file to confirm that DMI received packet information EVC-1 with vairable MMI_O_BRAKETARGET < 0.Use the log file to confirm that DMI received packet information EVC-7 with following variables,OBU_TR_M_MODE (EVC-7) = 8 (Post Trip)
            Test Step Comment: (1) MMI_gen 6774 (partly: when MMI_O_BRAKETARGET less than zero);(2) MMI_gen 6774 (partly: received MMI_O_BRAKETARGET is less than zero);(3) MMI_gen 2567 (partly: Table 38 'PT mode');
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 0;

            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 266;
            EVC8_MMIDriverMessage.Send();

            DmiActions.ShowInstruction(this, "Acknowledge Trip mode by pressing sub-area C1");

            EVC152_MMIDriverAction.Check_MMI_M_DRIVER_ACTION = EVC152_MMIDriverAction.MMI_M_DRIVER_ACTION.TrainTripAck;

            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.PostTrip;
            WaitForVerification(
                "Is the Trip mode symbol (M004)deleted and replaced by Post Trip mode symbol (MO06) in DMI area B7");

            TraceHeader("End of test");

            /*
            Test Step 4
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BT_Tools;
using BT_CSB_Tools;
using BT_CSB_Tools.Logging;
using BT_CSB_Tools.Utils.Xml;
using BT_CSB_Tools.SignalPoolGenerator.Signals;
using BT_CSB_Tools.SignalPoolGenerator.Signals.MwtSignal;
using BT_CSB_Tools.SignalPoolGenerator.Signals.MwtSignal.Misc;
using BT_CSB_Tools.SignalPoolGenerator.Signals.PdSignal;
using BT_CSB_Tools.SignalPoolGenerator.Signals.PdSignal.Misc;
using CL345;
using Testcase.Telegrams.EVCtoDMI;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 18.1.7.1 Distance to Target: Appearance of Distance to Target in FS mode
    /// TC-ID: 13.1.7.1
    /// 
    /// This test case verifies the display information of the distance to target bar and digital in FS mode. The display of distance to target is comply with the received packet EVC-1 and EVC-7. 
    /// 
    /// Tested Requirements:
    /// MMI_gen 6658; MMI_gen 6774; MMI_gen 107 (partly: Table 37, MMI_M_WARNING, OBU_TR_M_MODE, FS mode); MMI_gen 5817 (partly: MMI_M_WARNING = 2, sound Sinfo); MMI_gen 6758 ; MMI_gen 2567 (partly: Table 38, FS mode); MMI_gen 9516 (partly: PIM supervision); MMI_gen 12025 (partly: PIM supervision);
    /// 
    /// Scenario:
    /// 1.Drive the train forward pass BG1 at 100m. Then, verify the display of distance to target bar and digital with received packet information EVC-1 and EVC-
    /// 7.BG1: Packet 12, 21 and 27 (Entering FS)
    /// 2.Continue to drive the train forward. Then, verify the display of distance to target bar and digital when the supervision status is changed. 
    /// 3.Use the test script file to send an invalid value in EVC-1 and EVC-
    /// 7.Then, verify that the distance to target bar and digital are removed. 
    /// 4.Continue to drive the train forward. Then, verify the display of distance to target bar and digital when the supervision status is changed. Note: The consistency of information for the position in each test step and the location when the value of MMI_M_WARNING changed is able to verify in log file, EVC-7 variable OBU_TR_TRAIN.
    /// 
    /// Used files:
    /// 13_1_7_1.tdg, 13_1_7_1_a.xml, 13_1_7_1_b.xml
    /// </summary>
    public class TC_13_1_7_1_Brake : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:

            // Call the TestCaseBase PreExecution
            base.PreExecution();
            // System is powered on.Cabin is activated.SoM is performed in SR mode, level 1.
            DmiActions.Complete_SoM_L1_SR(this);
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec

            // Call the TestCaseBase PostExecution
            base.PostExecution();
            // DMI displays in FS mode, Level 1
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint

            /*
            Test Step 1
            Action: Drive the train forward pass BG1
            Expected Result: DMI displays in FS mode, level 1Verify the following information(1)    Use the log file to confirm that DMI receives the following packets information with a specific value,  EVC-1: MMI_M_WARNING = 0 (Status = NoS, Supervision = CSM)MMI_O_BRAKETARGET = -1 (Default) EVC-7: OBU_TR_M_MODE = 0 (FS mode) (2)   The distance to target bar is not display in sub-area A3. (3)   The distance to target digital is not display in sub-area A2
            Test Step Comment: (1) MMI_gen 107 (partly: MMI_M_WARNING, OBU_TR_M_MODE, FS mode CSM); MMI_gen 6658 (partly: MMI_O_BRAKETARGET is less than zero); MMI_gen 2567 (partly: MMI_M_WARNING, OBU_TR_M_MODE, FS mode CSM); MMI_gen 6774 (partly: MMI_O_BRAKETARGET is less than zero);(2) MMI_gen 6658 (partly: not be shown); MMI_gen 107 (partly: Table 37, FS mode, CSM);(3) MMI_gen 2567 (partly: Table 38, FS mode CSM); MMI_gen 6774 (partly: not be shown);
            */
            EVC1_MMIDynamic.MMI_O_BRAKETARGET = -1;
            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Normal_Status_Ceiling_Speed_Monitoring;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN = 0;       // just starting off

            // Set the permitted speed so the current speed is allowed
            EVC1_MMIDynamic.MMI_V_PERMITTED_KMH = 10;
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 5;

            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN = 10000;   // 100m
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.FullSupervision;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in FS mode, Level 1." + Environment.NewLine +
                                "2. The distance to target bar is not displayed in sub-area A3." + Environment.NewLine +
                                "3. The digital distance to target is not displayed in sub-area A2.");

            /*
            Test Step 2
            Action: Continue to drive the train forward.Then, stop the train
            Expected Result: Verify the following information,(1)    Use the log file to confirm that DMI receives the packet information EVC-1 with following variables,MMI_M_WARNING = 2 (Status = NoS, Supervision = PIM)MMI_O_BRAKETARGET > -1(2)    The distance to target bar is display in sub-area A3.(3)   The sound 'Sinfo' is played once.(4)    The distance to target digital is display in sub-area A2
            Test Step Comment: (1) MMI_gen 107 (partly: MMI_M_WARNING, FS mode PIM); MMI_gen 6658 (partly: NEGATIVE, MMI_O_BRAKETARGET is more than zero); MMI_gen 2567 (partly: MMI_M_WARNING, FS mode PIM);(2) MMI_gen 6658 (partly: NEGATIVE, shown); MMI_gen 107 (partly: Table 37, FS mode, PIM); MMI_gen 5817 (partly: MMI_M_WARNING = 2);(3) MMI_gen 5817 (partly: sound Sinfo); MMI_gen 9516 (partly: PIM supervision); MMI_gen 12025 (partly: PIM supervision);(4) MMI_gen 2567 (partly: Table 38, FS mode PIM);
            */
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN = 61000;   // 610m
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 0;
            EVC1_MMIDynamic.MMI_O_BRAKETARGET = 100000;
            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Normal_Status_PreIndication_Monitoring;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The distance to target bar is displayed in sub-area A3." + Environment.NewLine +
                                @"2. The sound 'Sinfo' is played once." + Environment.NewLine +
                                "3. The digital distance to target is displayed in sub-area A2.");

            /*
            Test Step 3
            Action: Continue to drive the train forward.Then, stop the train
            Expected Result: Verify the following information,(1)    Use the log file to confirm that DMI receives the packet information EVC-1 with following variables,MMI_M_WARNING = 11 (Status = NoS, Supervision = TSM)MMI_O_BRAKETARGET > -1(2)    The distance to target bar is display in sub-area A3.(3)    The distance to target digital is display in sub-area A2
            Test Step Comment: (1) MMI_gen 107 (partly: MMI_M_WARNING, FS mode TSM); MMI_gen 6658 (partly: NEGATIVE, MMI_O_BRAKETARGET is more than zero); MMI_gen 2567 (partly: MMI_M_WARNING, FS mode TSM);(2) MMI_gen 6658 (partly: NEGATIVE, shown); MMI_gen 107 (partly: Table 37, FS mode, TSM);(3) MMI_gen 2567 (partly: Table 38, FS mode TSM);
            */
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN = 90000;   // 910m
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 5;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN = 91000;   // 910m
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 0;
            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Normal_Status_Target_Speed_Monitoring;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The distance to target bar is displayed in sub-area A3." + Environment.NewLine +
                                "2. The digital distance to target is displayed in sub-area A2.");

            /*
            Test Step 4
            Action: Use the test script file 13_1_7_a.xml to send EVC-1 with,MMI_M_WARNING = 7
            Expected Result: Verify the following information,(1)   The distance to target bar and digital is removed from the DMI.Note: After test scipt file is executed, the distance to target bar and digital is re-appear refer to received packet EVC-1 from ETCS Onboard
            Test Step Comment: (1) MMI_gen 6758 (partly: MMI_M_WARNING is invalid);
            */
            XML_13_1_7(msgType.typea);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The distance to target bar is removed from sub-area A3." + Environment.NewLine +
                                "2. The digital distance to target is removed from sub-area A2.");

            this.Wait_Realtime(2000);
            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Normal_Status_Target_Speed_Monitoring;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The distance to target bar is re-displayed in sub-area A3 after 2s." + Environment.NewLine +
                                "2. The digital distance to target is re-displayed in sub-area A2 after 2s.");

            /*
            Test Step 5
            Action: Use the test script file 13_1_7_b.xml to send EVC-7 with,OBU_TR_M_MODE = 17
            Expected Result: Verify the following information,(1)   The distance to target bar and digital is removed from the DMI.Note: After test scipt file is executed, the distance to target bar and digital is re-appear refer to received packet EVC-1 from ETCS Onboard
            Test Step Comment: (1) MMI_gen 6758 (partly: OBU_TR_M_MODE is invalid);
            */
            XML_13_1_7(msgType.typeb);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The distance to target bar is removed from sub-area A3." + Environment.NewLine +
                                "2. The digital distance to target is removed from sub-area A2.");

            this.Wait_Realtime(2000);

            // Test spec says EVC1 signal would re-establish display but OBU_TR_M_MODE would still be invalid
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.FullSupervision;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The distance to target bar is re-displayed in sub-area A3 after 2s." + Environment.NewLine +
                                "2. The digital distance to target is re-displayed in sub-area A2 after 2s.");

            /*
            Test Step 6
            Action: Continue to drive the train forward.Then, stop the train
            Expected Result: Verify the following information,(1)    Use the log file to confirm that DMI receives the packet information EVC-1 with following variables,MMI_M_WARNING = 3 (Status = Inds, Supervision = RSM)(2)    The distance to target bar is display in sub-area A3.(3)    The distance to target digital is display in sub-area A2
            Test Step Comment: (1) MMI_gen 107 (partly: MMI_M_WARNING, OBU_TR_M_MODE, FS mode RSM); MMI_gen 6658 (partly: NEGATIVE, MMI_O_BRAKETARGET is more than zero); MMI_gen 2567 (partly: MMI_M_WARNING, OBU_TR_M_MODE, FS mode RSM);(2) MMI_gen 6658 (partly: NEGATIVE, shown); MMI_gen 107 (partly: Table 37, FS mode, RSM);(3) MMI_gen 2567 (partly: Table 38, FS mode RSM);
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 5;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN = 410000;   // 4.1 km
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 0;
            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Indication_Status_Release_Speed_Monitoring;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The distance to target bar is displayed in sub-area A3." + Environment.NewLine +
                                "2. The digital distance to target is displayed in sub-area A2.");
            /*
            Test Step 7
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
        #region Send_XML_13_1_7_DMI_Test_Specification
        enum msgType
        {
            typea,
            typeb
        }

        private void XML_13_1_7(msgType type)
        {
            switch (type)
            {

                case msgType.typea:

                    EVC1_MMIDynamic.MMI_M_SLIDE = 0;
                    EVC1_MMIDynamic.MMI_M_SLIP = 1;
                    // This is being ignored so set another 'invalid' warning mode so remove target bar
                    //EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Spare;   // 7
                    EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Intervention_Status_Ceiling_Speed_Monitoring;
                    EVC1_MMIDynamic.MMI_A_TRAIN = 0;
                    EVC1_MMIDynamic.MMI_V_TRAIN = 100;
                    EVC1_MMIDynamic.MMI_V_TARGET = 1111;
                    EVC1_MMIDynamic.MMI_V_PERMITTED = 833;
                    EVC1_MMIDynamic.MMI_V_RELEASE = 555;
                    EVC1_MMIDynamic.MMI_O_BRAKETARGET = 10002000;
                    EVC1_MMIDynamic.MMI_O_IML = 0;
                    EVC1_MMIDynamic.MMI_V_INTERVENTION = 0;

                    //SITR.ETCS1.Dynamic.EVC01Validity1.Value = 0x0;
                    //SITR.ETCS1.Dynamic.EVC01Validity2.Value = 0x0;

                    break;
                case msgType.typeb:
                    EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_EBTestInProgress = 0;
                    EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_EB_Status = 0;
                    EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_RadioStatus = 0;
                    EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_STM_HS_ENABLED = 0;
                    EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_STM_DA_ENABLED = 0;
                    EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_BrakeTest_Status =
                        EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_BRAKETEST_STATUS.BrakeTestNotInProgress;
                    EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Level = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_LEVEL.L0;
                    EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.Invalid;    // 17
                    EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_ADHESION = 0;
                    EVC7_MMIEtcsMiscOutSignals.OBU_TR_NID_STM_HS = 0;
                    EVC7_MMIEtcsMiscOutSignals.OBU_TR_NID_STM_DA = 0;
                    EVC7_MMIEtcsMiscOutSignals.BRAKE_TEST_TIMEOUT = 0;
                    EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN = 1000000000;

                    SITR.ETCS1.EtcsMiscOutSignals.EVC7Validity1.Value = 4096;         // bit 12 MMI_OBU_TR_M_Level
                    SITR.ETCS1.EtcsMiscOutSignals.EVC7Validity2.Value = 1;            // bit 0  MMI_OBU_TR_M_Mode


                    break;

            }
        }
        #endregion

    }
}
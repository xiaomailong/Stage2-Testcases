using System;
using Testcase.Telegrams.EVCtoDMI;

namespace Testcase.DMITestCases
{
    /// <summary>
    /// 17.6.1 Basic Speed Hook(s): General appearance
    /// TC-ID: 12.6.1
    /// 
    /// This test case verifies the general appearance of the Basic Speed Hook(s) in OS mode (for supervision is not CSM) and SH mode that displays around the speed dial. The general appearance of the Basic Speed Hook(s) shall comply with [ERA] standard and [MMI-ETCS-gen].
    /// 
    /// Tested Requirements:
    /// MMI_gen 6322; MMI_gen 6332 (partly: OBU_TR_M_MODE, MMI_V_PERMITTED, MMI_V_TARGET, MMI_M_WARNING, colour and appearance, OS mode not CSM, SH mode CSM); MMI_gen 6329 (partly: outer border of the speed dial); MMI_gen 6330 (partly: outer border of the speed dial); MMI_gen 9972; MMI_gen 6456 (partly: OS mode, Not CSM); MMI_gen 6452; MMI_gen 9516 (partly: toggling function of basic speed hooks with PS and TS); MMI_gen 12025 (partly: toggling function of basic speed hooks with PS and TS);
    /// 
    /// Scenario:
    /// Drive the train forward pass BG1 at position 100m. Then, verify the display information of all basic speed hooks refer to received packet information EVC1 and EVC-7.BG1: packet 12, 21, 27 and 80 (Entering OS)Continue to drive the train forward. Then, verify the display information of all basic speed hooks when its overlapping.Entering SH mode. Then, verify the display information of basic speed hook refer to received packet information EVC-1.Use the test script file to send an invalid value in EVC-1 and EVC-
    /// 7.Then, verify that basic speed hook is removed.
    /// 
    /// Used files:
    /// 12_6_1.tdg, 12_6_1_a.xml, 12_6_1_b.xml
    /// </summary>
    public class TC_12_6_1_Train_Speed : TestcaseBase
    {
        public override bool TestcaseEntryPoint()
        {
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 21691;

            StartUp();
            DmiActions.Complete_SoM_L1_SR(this);

            // Testcase entrypoint

            MakeTestStepHeader(1, UniqueIdentifier++,
                "Driver drives the train forward passing BG1.Then, stop the train and acknowledge OS mode by pressing sub-area C1",
                "DMI displays in OS mode, Level 1.Verify the following information," +
                "(1)   Use the log file to confirm that DMI received packet information EVC-7 with variable OBU_TR_M_MODE = 1 (On sight)." +
                "(2)   Use the log file to confirm that DMI received packet information EVC-1 with following variables, MMI_V_PERMITTED = 2777 (100 km/h) MMI_V_TARGET = 694 (25 km/h)MMI_M_WARNING not equal to 0,4,8,12 (Supervision is not CSM)" +
                "(3)   All basic speed hooks are displayed in sub-area B2." +
                "(4)   The first hook is displayed overlapping the outer border of the speed dial with white colour at 100 km/h." +
                "(5)   The second hook is displayed overlapping the outer border of the speed dial with Medium-grey colour at 25 km/h." +
                "(6)   Sound ‘Sinfo’ is played once");
            /*
            Test Step 1
            Action: Driver drives the train forward passing BG1.Then, stop the train and acknowledge OS mode by pressing sub-area C1
            Expected Result: DMI displays in OS mode, Level 1. Verify the following information,
            (1)   Use the log file to confirm that DMI received packet information EVC-7 with variable OBU_TR_M_MODE = 1 (On sight).
            (2)   Use the log file to confirm that DMI received packet information EVC-1 with following variables, MMI_V_PERMITTED = 2777 (100 km/h) MMI_V_TARGET = 694 (25 km/h)MMI_M_WARNING not equal to 0,4,8,12 (Supervision is not CSM)
            (3)   All basic speed hooks are displayed in sub-area B2.
            (4)   The first hook is displayed overlapping the outer border of the speed dial with white colour at 100 km/h.
            (5)   The second hook is displayed overlapping the outer border of the speed dial with Medium-grey colour at 25 km/h.
            (6)   Sound ‘Sinfo’ is played once
            Test Step Comment: (1) MMI_gen 6332 (partly: OBU_TR_M_MODE);(2) MMI_gen 6332 (partly: MMI_V_PERMITTED, MMI_V_TARGET, MMI_M_WARNING); MMI_gen 6456 (partly: Permitted Speed changes, Target Speed changes, OS mode, Not CSM);(3) MMI_gen 6322; MMI_gen 6456 (partly: toggle on);(4) MMI_gen 6332 (partly: colour and appearance, OS mode, not CSM); MMI_gen 6329 (partly: outer border of the speed dial);(5) MMI_gen 6332 (partly: colour and appearance, OS mode, not CSM); MMI_gen 6330 (partly: outer border of the speed dial);(6) MMI_gen 6456 (partly: sound Sinfo); MMI_gen 9516 (partly: toggling function of basic speed hooks with PS and TS); MMI_gen 12025 (partly: toggling function of basic speed hooks with PS and TS);
            */
            // V_TARGET, V_PERMITTED on
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.OnSight;

            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 5;
            EVC1_MMIDynamic.MMI_V_PERMITTED = 2777;     // 100 km/h
            EVC1_MMIDynamic.MMI_V_TARGET = 694;         // 25 km/h
            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Indication_Status_Target_Speed_Monitoring; // Not 0, 4, 8, 12


            WaitForVerification("Check  the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in OS mode, Level 1." + Environment.NewLine +
                                "2. All basic speed hooks are displayed in sub-area B2." + Environment.NewLine +
                                "3. The first hook is displayed overlapping the outer border of the speed dial in white colour at 100 km/h." +
                                Environment.NewLine +
                                "4. The second hook is displayed overlapping the outer border of the speed dial in medium-grey colour at 25 km/h." +
                                Environment.NewLine +
                                "5. Sound ‘Sinfo’ is played once.");

            MakeTestStepHeader(2, UniqueIdentifier++,
                "Continue to drive the train forward until basic speed hooks are overlapped",
                "Verify the following information,The Vperm hook (White colour) is overlay the Vtarget hook (Medium grery colour)");
            /*
            Test Step 2
            Action: Continue to drive the train forward until basic speed hooks are overlapped
            Expected Result: Verify the following information,The Vperm hook (White colour) is overlay the Vtarget hook (Medium grery colour)
            Test Step Comment: MMI_gen 9972;    
            */
            EVC1_MMIDynamic.MMI_V_TARGET = 2777;    // 100 km/h

            WaitForVerification("Wait until the basic speed hooks overlap, then check the following:" +
                                Environment.NewLine + Environment.NewLine +
                                "1. The Vperm hook (in white) overlays the Vtarget hook (medium-grey)");

            MakeTestStepHeader(3, UniqueIdentifier++,
                "Perform the following procedure,Press the 'Main' button.Press and hold 'Shunting' button at least 2 seconds. Release 'Shunting' button",
                "DMI displays in SH mode, level 1.Verify the following information," +
                "(1) Use the log file to confirm that DMI received packet information EVC-7 with variable OBU_TR_M_MODE = 3 (Shunting)." +
                "(2) Use the log file to confirm that DMI received packet information EVC-1 with following variables, MMI_V_PERMITTED = 833 (30 km/h) MMI_M_WARNING = 0 (NoS, Supervision = CSM)" +
                "(3) The first hook is displayed overlapping the outer border of the speed dial with white colour at 30 km/h");
            /*
            Test Step 3
            Action: Perform the following procedure,Press the 'Main' button.Press and hold 'Shunting' button at least 2 seconds.Release 'Shunting' button
            Expected Result: DMI displays in SH mode, level 1.Verify the following information,
            (1) Use the log file to confirm that DMI received packet information EVC-7 with variable OBU_TR_M_MODE = 3 (Shunting).
            (2) Use the log file to confirm that DMI received packet information EVC-1 with following variables, MMI_V_PERMITTED = 833 (30 km/h) MMI_M_WARNING = 0 (NoS, Supervision = CSM)
            (3) The first hook is displayed overlapping the outer border of the speed dial with white colour at 30 km/h
            Test Step Comment: (1) MMI_gen 6332 (partly: OBU_TR_M_MODE);(2) MMI_gen 6332 (partly: MMI_V_PERMITTED, MMI_M_WARNING);(3) MMI_gen 6322; MMI_gen 6332 (partly: colour and appearance, SH mode, CSM); MMI_gen 6329 (partly: outer border of the speed dial);
            */

            DmiActions.ShowInstruction(this,
                @"Press the ‘Main’ button. Press and hold 'Shunting' button for at least 2 seconds, then release the button.");

            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.Shunting;
            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Normal_Status_Ceiling_Speed_Monitoring;
            EVC1_MMIDynamic.MMI_V_PERMITTED = 833;
            EVC1_MMIDynamic.MMI_V_TARGET = 800;

            WaitForVerification("Check  the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in SH mode, level 1." + Environment.NewLine +
                                "2. The first hook is displayed overlapping the outer border of the speed dial coloured in white at 30 km/h.");

            MakeTestStepHeader(4, UniqueIdentifier++,
                "Use the test script file 12_6_1_a.xml to send EVC-1 with,MMI_M_WARNING = 7",
                "Verify the following information," +
                "(1) The basic speed hook is removed from the DMI." +
                "(2) After test scipt file is executed, the basic speed hook  re-appears refer to received packet EVC-1 from ETCS Onboard");
            /*
            Test Step 4
            Action: Use the test script file 12_6_1_a.xml to send EVC-1 with,MMI_M_WARNING = 7
            Expected Result: Verify the following information,(1)   The basic speed hook is removed from the DMI.(2)   After test scipt file is executed, the basic speed hook is re-appear refer to received packet EVC-1 from ETCS Onboard
            Test Step Comment: (1) MMI_gen 6452 (partly: MMI_M_WARNING is invalid);(2) MMI_gen 6452 (partly: toggle function is reset to default state);
            */
            XML_12_6_1(msgType.typea);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The basic speed hook is removed from the DMI.");

            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Indication_Status_Release_Speed_Monitoring;
            EVC1_MMIDynamic.MMI_A_TRAIN = 0;
            EVC1_MMIDynamic.MMI_V_TRAIN = 100;
            EVC1_MMIDynamic.MMI_V_TARGET = 1111;
            EVC1_MMIDynamic.MMI_V_PERMITTED = 833;
            EVC1_MMIDynamic.MMI_V_RELEASE = 555;

            // Reset validity bits
            SITR.ETCS1.Dynamic.EVC01Validity1.Value = 0xc800; // 51200 in decimal
            SITR.ETCS1.Dynamic.EVC01Validity2.Value = 0xff00; // 65280 in decimal

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The basic speed hook is re-displayed.");

            MakeTestStepHeader(5, UniqueIdentifier++,
                "Use the test script file 12_6_1_b.xml to send EVC-7 with,OBU_TR_M_MODE = 17",
                "Verify the following information," +
                "(1) The basic speed hook is removed from the DMI." +
                "(2) After test script file is executed, the basic speed hook is re-appear refer to received packet EVC-1 from ETCS Onboard");
            /*
            Test Step 5
            Action: Use the test script file 12_6_1_b.xml to send EVC-7 with,OBU_TR_M_MODE = 17
            Expected Result: Verify the following information,
            (1) The basic speed hook is removed from the DMI.
            (2) After test script file is executed, the basic speed hook is re-appear refer to received packet EVC-1 from ETCS Onboard
            Test Step Comment: (1) MMI_gen 6452 (partly: OBU_TR_M_MODE is invalid);(2) MMI_gen 6452 (partly: toggle function is reset to default state);
            */
            XML_12_6_1(msgType.typeb);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The basic speed hook is removed from the DMI.");

            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Indication_Status_Release_Speed_Monitoring;
            EVC1_MMIDynamic.MMI_A_TRAIN = 0;
            EVC1_MMIDynamic.MMI_V_TRAIN = 100;
            EVC1_MMIDynamic.MMI_V_TARGET = 1111;
            EVC1_MMIDynamic.MMI_V_PERMITTED = 833;
            EVC1_MMIDynamic.MMI_V_RELEASE = 555;
            EVC1_MMIDynamic.MMI_O_BRAKETARGET = 10002000;

            // Reset validity bits
            SITR.ETCS1.EtcsMiscOutSignals.EVC7Validity1.Value = 0x7c88; // All validity bits set
            SITR.ETCS1.EtcsMiscOutSignals.EVC7Validity2.Value = 0xfc00; // All validity bits set

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The basic speed hook is re-displayed.");

            TraceHeader("End of test");

            /*
            Test Step 6
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }

        #region Send_XML_12_6_1_DMI_Test_Specification

        enum msgType
        {
            typea,
            typeb
        }

        private void XML_12_6_1(msgType type)
        {
            switch (type)
            {
                case msgType.typea:
                    EVC1_MMIDynamic.MMI_M_SLIDE = 0;
                    EVC1_MMIDynamic.MMI_M_SLIP = 1;
                    EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Spare; // 7
                    EVC1_MMIDynamic.MMI_A_TRAIN = 0;
                    EVC1_MMIDynamic.MMI_V_TRAIN = 100;
                    EVC1_MMIDynamic.MMI_V_TARGET = 1111;
                    EVC1_MMIDynamic.MMI_V_PERMITTED = 833;
                    EVC1_MMIDynamic.MMI_V_RELEASE = 555;
                    EVC1_MMIDynamic.MMI_O_BRAKETARGET = 10002000;
                    EVC1_MMIDynamic.MMI_O_IML = 0;
                    EVC1_MMIDynamic.MMI_V_INTERVENTION = 0;

                    SITR.ETCS1.Dynamic.EVC01Validity1.Value = 0x0;
                    SITR.ETCS1.Dynamic.EVC01Validity2.Value = 0x0;
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
                    EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode =
                        EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.Invalid; // "Spare"
                    EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_ADHESION = 0;
                    EVC7_MMIEtcsMiscOutSignals.OBU_TR_NID_STM_HS = 0;
                    EVC7_MMIEtcsMiscOutSignals.OBU_TR_NID_STM_DA = 0;
                    EVC7_MMIEtcsMiscOutSignals.BRAKE_TEST_TIMEOUT = 0;
                    EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN = 10000000;

                    SITR.ETCS1.EtcsMiscOutSignals.EVC7Validity1.Value = 0x0008;     // Bit-inverse of 4096
                    SITR.ETCS1.EtcsMiscOutSignals.EVC7Validity2.Value = 0x8000;     // Bit inverse of 1
                    break;
            }
        }

        #endregion
    }
}
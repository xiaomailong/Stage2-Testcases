using System;
using Testcase.Telegrams.EVCtoDMI;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 17.5.4 Circular Speed Gauge removal when received an invalid value of EVC-1 and EVC-7
    /// TC-ID: 12.5.4
    /// 
    /// This test case verifies that the circular speed gauge is removed refer to received packet EVC-1 and EVC-7 with an invalid value in specific variable.
    /// 
    /// Tested Requirements:
    /// MMI_gen 977;
    /// 
    /// Scenario:
    /// 1.Drive the train forward pass BG1 at position 100m to enter FS mode.       BG1: Packet 12, 21 and 27 (Entering FS)
    /// 2.Use the test script file to send an invalid value in packet EVC-1 and EVC-
    /// 7.Then, verify that the circular speed gauge is removed.
    /// 
    /// Used files:
    /// 12_5_4.tdg, 12_5_4_a.xml, 12_5_4_b.xml, 12_5_4_c.xml, 12_5_4_d.xml, 12_5_4_e.xml, 12_5_4_f.xml
    /// </summary>
    public class TC_12_5_4_Train_Speed : TestcaseBase
    {
        public override bool TestcaseEntryPoint()
        {
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 21672;
            // Testcase entrypoint

            StartUp();
            DmiActions.Complete_SoM_L1_FS(this);

            MakeTestStepHeader(1, UniqueIdentifier++,
                "Drive the train forward pass BG1 with speed = 30 km/h.Then, stop the train",
                "DMI displays in FS mode, Level 1");
            /*
            Test Step 1
            Action: Drive the train forward pass BG1 with speed = 30 km/h. Then, stop the train
            Expected Result: DMI displays in FS mode, Level 1
            */
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in FS mode, Level 1.");

            // Set up initial MA parameters taken from .tdg file
            EVC1_MMIDynamic.MMI_V_TRAIN = 0;
            EVC1_MMIDynamic.MMI_V_PERMITTED_KMH = 160;
            EVC1_MMIDynamic.MMI_V_RELEASE_KMH = -1;
            EVC1_MMIDynamic.MMI_V_TARGET = -1;

            MakeTestStepHeader(2, UniqueIdentifier++,
                "Use the test script file 12_5_4_a.xml to send EVC-1 with MMI_M_WARNING = 7",
                "Verify the following information," +
                "(1) The Circular Speed Gauge is removed from sub-area B2." +
                "Note: The circular speed gauge re-appears when DMI received packet EVC-1 from ETCS onboard");
            /*
            Test Step 2
            Action: Use the test script file 12_5_4_a.xml to send EVC-1 with MMI_M_WARNING = 7
            Expected Result: Verify the following information,
            (1) The Circular Speed Gauge is removed from sub-area B2.
            Note: The circular speed gauge is re-appear when DMI received packet EVC-1 from ETCS onboard
            Test Step Comment: (1) MMI_gen 977 (partly: MMI_M_WARNING);
            */
            XML_12_5_4(msgType.typea);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1.The Circular Speed Gauge is removed from sub-area B2.");

            // correct the invalid item:
            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Normal_Status_Ceiling_Speed_Monitoring;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1.The Circular Speed Gauge re-appears in sub-area B2.");

            MakeTestStepHeader(3, UniqueIdentifier++,
                "Use the test script file 12_5_4_b.xml to send EVC-1 with,MMI_V_TARGET = 11112",
                "Verify the following information," +
                "(1) The Circular Speed Gauge is removed from sub-area B2." +
                "Note: The circular speed gauge re-appears when DMI received packet EVC-1 from ETCS onboard.");
            /*
            Test Step 3
            Action: Use the test script file 12_5_4_b.xml to send EVC-1 with,MMI_V_TARGET = 11112
            Expected Result: Verify the following information,
            (1) The Circular Speed Gauge is removed from sub-area B2.
            Note: The circular speed guage is re-appear when DMI received packet EVC-1 from ETCS onboard
            Test Step Comment: (1) MMI_gen 977 (partly: MMI_V_TARGET);
            */
            XML_12_5_4(msgType.typeb);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1.The Circular Speed Gauge is removed from sub-area B2.");

            // correct the invalid item:
            EVC1_MMIDynamic.MMI_V_TARGET = 1111;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1.The Circular Speed Gauge re-appears in sub-area B2.");

            MakeTestStepHeader(4, UniqueIdentifier++,
                "Use the test script file 12_5_4_c.xml to send EVC-1 with,MMI_V_PERMITTED = 11112",
                "Verify the following information," +
                "(1) The Circular Speed Gauge is removed from sub-area B2." +
                "Note: The circular speed gauge re-appears when DMI received packet EVC-1 from ETCS onboard");
            /*
            Test Step 4
            Action: Use the test script file 12_5_4_c.xml to send EVC-1 with,MMI_V_PERMITTED = 11112
            Expected Result: Verify the following information,
            (1) The Circular Speed Gauge is removed from sub-area B2.
            Note: The circular speed guage is re-appear when DMI received packet EVC-1 from ETCS onboard
            Test Step Comment: (1) MMI_gen 977 (partly: MMI_V_PERMITTED);
            */
            XML_12_5_4(msgType.typec);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1.The Circular Speed Gauge is removed from sub-area B2.");

            // correct the invalid item:
            EVC1_MMIDynamic.MMI_V_PERMITTED = 1111;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1.The Circular Speed Gauge re-appears in sub-area B2.");

            MakeTestStepHeader(5, UniqueIdentifier++,
                "Use the test script file 12_5_4_d.xml to send EVC-1 with,MMI_V_INTERVENTION = 11112",
                "Verify the following information," +
                "(1) The Circular Speed Gauge is removed from sub-area B2." +
                "Note: The circular speed gauge re-appears when DMI received packet EVC-1 from ETCS onboard");
            /*
            Test Step 5
            Action: Use the test script file 12_5_4_d.xml to send EVC-1 with,MMI_V_INTERVENTION = 11112
            Expected Result: Verify the following information,
            (1) The Circular Speed Gauge is removed from sub-area B2.
            Note: The circular speed guage is re-appear when DMI received packet EVC-1 from ETCS onboard
            Test Step Comment: (1) MMI_gen 977 (partly: MMI_V_INTERVENTION);
            */
            XML_12_5_4(msgType.typed);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1.The Circular Speed Gauge is removed from sub-area B2.");

            // correct the invalid item:
            EVC1_MMIDynamic.MMI_V_INTERVENTION = 4500;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1.The Circular Speed Gauge re-appears in sub-area B2.");

            MakeTestStepHeader(6, UniqueIdentifier++,
                "Use the test script file 12_5_4_e.xml to send EVC-1 with,MMI_V_RELEASE = 11112",
                "Verify the following information," +
                "(1) The Circular Speed Gauge is removed from sub-area B2." +
                "Note: The circular speed gauge re-appears when DMI received packet EVC-1 from ETCS onboard");
            /*
            Test Step 6
            Action: Use the test script file 12_5_4_e.xml to send EVC-1 with,MMI_V_RELEASE = 11112
            Expected Result: Verify the following information,
            (1) The Circular Speed Gauge is removed from sub-area B2.
            Note: The circular speed guage is re-appear when DMI received packet EVC-1 from ETCS onboard
            Test Step Comment: (1) MMI_gen 977 (partly: MMI_V_RELEASE);
            */
            XML_12_5_4(msgType.typee);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1.The Circular Speed Gauge is removed from sub-area B2.");


            // correct the invalid item:
            EVC1_MMIDynamic.MMI_V_RELEASE = 1111;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1.The Circular Speed Gauge re-appears in sub-area B2.");

            MakeTestStepHeader(7, UniqueIdentifier++,
                "Use the test script file 12_5_4_f.xml to send EVC-7 with,OBU_TR_M_MODE = 17",
                "Verify the following information," +
                "(1) The Circular Speed Gauge is removed from sub-area B2." +
                "Note: The circular speed gauge re-appears when DMI received packet EVC-1 from ETCS onboard");
            /*
            Test Step 7
            Action: Use the test script file 12_5_4_f.xml to send EVC-7 with,OBU_TR_M_MODE = 17
            Expected Result: Verify the following information,
            (1) The Circular Speed Gauge is removed from sub-area B2.
            Note: The circular speed guage is re-appear when DMI received packet EVC-7 from ETCS onboard
            Test Step Comment: (1) MMI_gen 977 (partly: OBU_TR_M_MODE);
            */
            XML_12_5_4(msgType.typef);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1.The Circular Speed Gauge is removed from sub-area B2.");

            // correct the invalid item:
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.FullSupervision;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1.The Circular Speed Gauge re-appears in sub-area B2.");

            TraceHeader("End of test");

            /*
            Test Step 8
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }

        #region Send_XML_12_5_4_DMI_Test_Specification

        enum msgType
        {
            typea,
            typeb,
            typec,
            typed,
            typee,
            typef
        }

        private void XML_12_5_4(msgType type)
        {
            if (type != msgType.typef)
            {
                EVC1_MMIDynamic.MMI_M_SLIDE = 0;
                EVC1_MMIDynamic.MMI_M_SLIP = 0;
                EVC1_MMIDynamic.MMI_A_TRAIN = 0;
                EVC1_MMIDynamic.MMI_V_TARGET = 0;
                EVC1_MMIDynamic.MMI_V_PERMITTED = 4444;
                EVC1_MMIDynamic.MMI_V_RELEASE = 0;
                EVC1_MMIDynamic.MMI_O_BRAKETARGET = 0x3ba1a259;     // 1000448601
                EVC1_MMIDynamic.MMI_O_IML = 0x3b9b4523;             // 1000031523
                EVC1_MMIDynamic.MMI_V_INTERVENTION = 4658;

                EVC1_MMIDynamic.SetValidityBits(true); 
            }

            switch (type)
            {
                case msgType.typea:
                    EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Spare; // 7
                    EVC1_MMIDynamic.MMI_V_TRAIN = 0;
                    break;

                case msgType.typeb:
                    EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Normal_Status_Ceiling_Speed_Monitoring; // 0
                    EVC1_MMIDynamic.MMI_V_TARGET = 11112; // invalid
                    EVC1_MMIDynamic.MMI_V_TRAIN = 0;
                    break;

                case msgType.typec:
                    EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Normal_Status_Ceiling_Speed_Monitoring; // 0
                    EVC1_MMIDynamic.MMI_V_PERMITTED = 11112; //invalid
                    EVC1_MMIDynamic.MMI_V_TRAIN = 0;
                    break;

                case msgType.typed:
                    EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Normal_Status_Ceiling_Speed_Monitoring; // 0
                    EVC1_MMIDynamic.MMI_V_INTERVENTION = 11112; // invalid
                    EVC1_MMIDynamic.MMI_V_TRAIN = 0;
                    break;

                case msgType.typee:
                    EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Normal_Status_Ceiling_Speed_Monitoring; // 0
                    EVC1_MMIDynamic.MMI_V_RELEASE = 11112; // invalid
                    EVC1_MMIDynamic.MMI_V_TRAIN = 0;
                    break;

                case msgType.typef:
                    EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_EBTestInProgress = 0;
                    EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_EB_Status = 0;
                    EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_RadioStatus = 0;
                    EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_STM_HS_ENABLED = 0;
                    EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_STM_DA_ENABLED = 0;
                    EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_BrakeTest_Status = EVC7_MMIEtcsMiscOutSignals
                        .MMI_OBU_TR_BRAKETEST_STATUS.BrakeTestNotInProgress;
                    EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Level = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_LEVEL.L1;
                    EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.Invalid;
                    EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_ADHESION = 100; // "Spare"
                    EVC7_MMIEtcsMiscOutSignals.OBU_TR_NID_STM_HS = 255;
                    EVC7_MMIEtcsMiscOutSignals.OBU_TR_NID_STM_DA = 255;
                    EVC7_MMIEtcsMiscOutSignals.BRAKE_TEST_TIMEOUT = 46;
                    EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN = 1000010000;

                    EVC7_MMIEtcsMiscOutSignals.SetValidityBits(true);

                    break;
            }
        }

        #endregion
    }
}
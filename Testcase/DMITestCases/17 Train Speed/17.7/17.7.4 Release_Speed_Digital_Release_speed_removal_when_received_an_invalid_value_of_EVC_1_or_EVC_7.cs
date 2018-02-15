using System;
using Testcase.Telegrams.EVCtoDMI;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 17.7.4 Release Speed Digital: Release speed removal when received an invalid value of EVC-1 or EVC-7
    /// TC-ID: 12.7.4
    /// 
    /// This test case is verifies the display of release speed which removed when DMI received an invalid value of EVC-1 or EVC-7.
    /// 
    /// Tested Requirements:
    /// MMI_gen 6587;
    /// 
    /// Scenario:
    /// 1.Drive the train forward pass BG1 at position 100m.BG1: Packet 12, 21, 27 (Entering FS)
    /// 2.Use the test script file to send an invalid value of EVC-1 and EVC-7.Then, verify that reease speed digital is removed.
    /// 
    /// Used files:
    /// 12_7_4.tdg, 12_7_4_a.xml, 12_7_4_b.xml
    /// </summary>
    public class TC_12_7_4_Train_Speed : TestcaseBase
    {
        public override bool TestcaseEntryPoint()
        {
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 21769;
            // Testcase entrypoint

            StartUp();
            DmiActions.Complete_SoM_L1_SR(this);

            MakeTestStepHeader(1, UniqueIdentifier++, "Drive the train forward pass BG1. Then, stop the train",
                "DMI displays in FS mode, level 1");
            /*
            Test Step 1
            Action: Drive the train forward pass BG1.Then, stop the train
            Expected Result: DMI displays in FS mode, level 1
            */

            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.FullSupervision;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in FS mode, Level 1.");

            MakeTestStepHeader(2, UniqueIdentifier++,
                "Use the test script file 12_7_4_a.xml to send EVC-1 with, MMI_M_WARNING = 7",
                "Verify the following information," +
                "(1) The release speed in sub-area B2 and B6 are removed from the DMI." +
                "(2) After test scipt file is executed, the release speed in sub-area B2 and B6 re-appear according to received packet EVC-1 from ETCS Onboard.");
            /*
            Test Step 2
            Action: Use the test script file 12_7_4_a.xml to send EVC-1 with, MMI_M_WARNING = 7
            Expected Result: Verify the following information,
            (1) The release speed in sub-area B2 and B6 are removed from the DMI.
            (2) After test scipt file is executed, the release speed in sub-area B2 and B6 re-appear according to received packet EVC-1 from ETCS Onboard
            Test Step Comment: (1) MMI_gen 6587 (partly: MMI_M_WARNING is invalid);(2) MMI_gen 6587 (partly: toggle function is reset to default state);
            */
            XML_12_7_4(msgType.typea);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The release speed in sub-area B2 and B6 are removed from the DMI.");

            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Indication_Status_Release_Speed_Monitoring;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The release speed in sub-area B2 and B6 are re-displayed");

            MakeTestStepHeader(3, UniqueIdentifier++,
                "Use the test script file 12_7_4_b.xml to send EVC-7 with, OBU_TR_M_MODE = 17",
                "Verify the following information," +
                "(1) The release speed are sub-area B2 and B6 is removed from the DMI." +
                "(2) After test scipt file is executed, the release speed in sub-area B2 and B6 re-appear according to received packet EVC-7 from ETCS Onboard.");
            /*
            Test Step 3
            Action: Use the test script file 12_7_4_b.xml to send EVC-7 with,OBU_TR_M_MODE = 17
            Expected Result: Verify the following information,
            (1) The release speed are sub-area B2 and B6 is removed from the DMI.
            (2) After test scipt file is executed, the release speed in sub-area B2 and B6 re-appear according to received packet EVC-7 from ETCS Onboard
            Test Step Comment: (1) MMI_gen 6587 (partly: OBU_TR_M_MODE is invalid);(2) MMI_gen 6587 (partly: toggle function is reset to default state);
            */
            XML_12_7_4(msgType.typeb);

            // Reset EVC-1 validity bits
            EVC1_MMIDynamic.SetValidityBits(true);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The release speeds in sub-area B2 and B6 are removed from the DMI.");

            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.FullSupervision;

            // Reset EVC-7 validity bits
            SITR.ETCS1.EtcsMiscOutSignals.EVC7Validity1.Value = 0x7c88;
            SITR.ETCS1.EtcsMiscOutSignals.EVC7Validity2.Value = 0xfc00;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The release speeds in sub-area B2 and B6 are re-displayed.");

            TraceHeader("End of test");

            /*
            Test Step 4
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }

        #region Send_XML_12_7_4_DMI_Test_Specification

        enum msgType
        {
            typea,
            typeb
        }

        private void XML_12_7_4(msgType type)
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

                    EVC1_MMIDynamic.SetValidityBits(false);

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
                    EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.Invalid;
                    EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_ADHESION = 100; // "Spare"
                    EVC7_MMIEtcsMiscOutSignals.OBU_TR_NID_STM_HS = 0;
                    EVC7_MMIEtcsMiscOutSignals.OBU_TR_NID_STM_DA = 0;
                    EVC7_MMIEtcsMiscOutSignals.BRAKE_TEST_TIMEOUT = 0;
                    EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN = 10000000;

                    SITR.ETCS1.EtcsMiscOutSignals.EVC7Validity1.Value = 0x0008;     // Bit-inverse of 4096
                    SITR.ETCS1.EtcsMiscOutSignals.EVC7Validity2.Value = 0x8000;     // Bit-inverse of 1

                    break;
            }
        }

        #endregion
    }
}
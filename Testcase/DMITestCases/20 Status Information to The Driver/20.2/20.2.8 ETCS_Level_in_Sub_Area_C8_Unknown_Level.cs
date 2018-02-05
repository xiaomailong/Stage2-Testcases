using System;
using Testcase.Telegrams.EVCtoDMI;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 20.2.8 ETCS Level in Sub-Area C8: Unknown Level
    /// TC-ID: 15.2.8
    /// 
    /// This test case verifies the appearance of ETCS Level symbols in sub-area C8 after received an invalid or unknown  from packet information EVC-7. The displays in sub-area C8 shall be blank.
    /// 
    /// Tested Requirements:
    /// EVC7_MMIEtcsMiscOutSignals.MMI_gen 577 (partly: Unknown);
    /// 
    /// Scenario:
    /// 1.Use the test script file to send EVC-7 with an invalid and unknown . Then, verify the blank information in sub-area C8.
    /// 
    /// Used files:
    /// 15_2_8_a.xml
    /// </summary>
    public class TC_ID_15_2_8_ETCS_Level : TestcaseBase
    {

        public override bool TestcaseEntryPoint()
        {
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 0;
            // Testcase entrypoint
            StartUp();
            DmiActions.Complete_SoM_L1_SB(this);

            MakeTestStepHeader(1, UniqueIdentifier++,
                "Use the test script file 15_2_8_a.xml to send EVC-7 with,OBU_TR_M_Level =  15",
                "Verify the following information,(1)   No symbol displays in sub-area C8");
            /*
            Test Step 1
            Action: Use the test script file 15_2_8_a.xml to send EVC-7 with,OBU_TR_M_Level =  15
            Expected Result: Verify the following information,(1)   No symbol displays in sub-area C8
            Test Step Comment: (1) EVC7_MMIEtcsMiscOutSignals.MMI_gen 577 (partly: Unknown);
            */

            #region Send_XML_15_2_8_DMI_Test_Specification

            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_EBTestInProgress = 0;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_EB_Status = 0;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_RadioStatus = 0;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_STM_HS_ENABLED = 0;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_STM_DA_ENABLED = 0;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_BrakeTest_Status =
                EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_BRAKETEST_STATUS.BrakeTestNotInProgress;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Level = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_LEVEL.Unknown;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.StandBy;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_ADHESION = 0;
            EVC7_MMIEtcsMiscOutSignals.BRAKE_TEST_TIMEOUT = 0;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN = 10000000;

            // XML has validity1 0x1000 (4096) which is bit 12 (MMI_OBU_TR_EBTestInProgress) and validity2 0x0001; (MMI_OBU_TR_M_Mode)
            this.SITR.ETCS1.EtcsMiscOutSignals.EVC7Validity1.Value = 0x0100;

            WaitForVerification("Check the following: + " + Environment.NewLine + Environment.NewLine +
                                "1. DMI does not display a symbol in sub-area C8.");

            #endregion

            MakeTestStepHeader(2, UniqueIdentifier++, "End of test", "");

            /*
            Test Step 2
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
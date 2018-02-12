using System;
using Testcase.Telegrams.EVCtoDMI;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 17.15 Slip and Slide are configure to 0 at the same time
    /// TC-ID: 12.15
    /// 
    /// This test case verifies the display of the ‘Slip/Slide’ indication when both of indication are configured disabled with ETC speed.
    /// 
    /// Tested Requirements:
    /// MMI_gen 1692 (partly: ETC speed, disabled);   
    /// 
    /// Scenario:
    /// SoM is completed in SR mode, Level 1 and  SLIP_SPEEDMETER & SLIDE_SPEEDMETER are configured to be 0 (not display)At 100 m, pass BG1 with pkt 12, pkt 21 and pkt 
    /// 27.Mode changes to FS mode.The ‘Slip/Slide’ indication is verified by the following cases:ATP disable MMI_M_SLIP and MMI_M_SLIDEATP enable MMI_M_SLIP and MMI_M_SLIDEATP enable MMI_M_SLIP but disable MMI_M_SLIDEATP disable MMI_M_SLIP butenable MMI_M_SLIDEThe train is stopped.
    /// 
    /// Used files:
    /// 12_15.tdg, 12_15_a.xml, 12_15_b.xml, 12_15_c.xml
    /// </summary>
    public class TC_ID_12_15_Train_Speed : TestcaseBase
    {
        public override bool TestcaseEntryPoint()
        {
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 21850;

            StartUp();

            DmiActions.Complete_SoM_L1_SR(this);

            // Testcase entrypoint
            TraceInfo("This test case requires an ATP configuration change - " +
                      "See Precondition requirements. If this is not done manually, the test may fail!");

            MakeTestStepHeader(1, UniqueIdentifier++, "Driver the train forward",
                "DMI changes from SR mode to FS mode");
            /*
            Test Step 1
            Action: Driver the train forward
            Expected Result: DMI changes from SR mode to FS mode
            */
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.FullSupervision;
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 5;

            // Call generic Check Results Method
            DmiExpectedResults.FS_mode_displayed(this);

            MakeTestStepHeader(2, UniqueIdentifier++, "Drive the train forward with speed = 140 km/h",
                "The speed pointer is displayed with speed =140");
            /*
            Test Step 2
            Action: Drive the train forward with speed = 140 km/h
            Expected Result: The speed pointer is displayed with speed =140
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 140;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The speed pointer is displayed with speed = 140 km/h.");

            MakeTestStepHeader(3, UniqueIdentifier++,
                "Use the test script file 12_15_a.xml to send EVC-1 with,MMI_M_SLIP = 1MMI_M_SLIDE =0",
                "Verify that Slip and Slide indicator are not display on DMI");
            /*
            Test Step 3
            Action: Use the test script file 12_15_a.xml to send EVC-1 with,MMI_M_SLIP = 1MMI_M_SLIDE =0
            Expected Result: Verify that Slip and Slide indicator are not display on DMI
            Test Step Comment: (1) MMI_gen 1692 (partly: ETC speed, disabled);   
            */
            XML_12_15(msgType.typea);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Slip and Slide indicator are not displayed on DMI.");

            MakeTestStepHeader(4, UniqueIdentifier++,
                "Use the test script file 12_15_b.xml to send EVC-1 with,MMI_M_SLIP = 0MMI_M_SLIDE = 1",
                "Verify that Slip and Slide indicator are not display on DMI");
            /*
            Test Step 4
            Action: Use the test script file 12_15_b.xml to send EVC-1 with,MMI_M_SLIP = 0MMI_M_SLIDE = 1
            Expected Result: Verify that Slip and Slide indicator are not display on DMI
            Test Step Comment: (1) MMI_gen 1692 (partly: ETC speed, disabled);   
            */
            XML_12_15(msgType.typeb);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Slip and Slide indicator are not displayed on DMI.");

            MakeTestStepHeader(5, UniqueIdentifier++,
                "Use the test script file 12_15_c.xml to send EVC-1 with,MMI_M_SLIP = 1MMI_M_SLIDE = 1",
                "Verify that Slip and Slide indicator are not display on DMI");
            /*
            Test Step 5
            Action: Use the test script file 12_15_c.xml to send EVC-1 with,MMI_M_SLIP = 1MMI_M_SLIDE = 1
            Expected Result: Verify that Slip and Slide indicator are not display on DMI
            Test Step Comment: (1) MMI_gen 1692 (partly: ETC speed, disabled);   
            */
            XML_12_15(msgType.typec);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Slip and Slide indicator are not displayed on DMI.");

            MakeTestStepHeader(6, UniqueIdentifier++, "End of test", "");

            /*
            Test Step 6
            Action: End of test
            Expected Result: 
            */


            return GlobalTestResult;
        }

        #region Send_XML_12_15_DMI_Test_Specification

        enum msgType
        {
            typea,
            typeb,
            typec
        }

        private void XML_12_15(msgType type)
        {
            //SITR.ETCS1.Dynamic.EVC01Validity1.Value = 0x0;
            //SITR.ETCS1.Dynamic.EVC01Validity2.Value = 0x0;

            if (type == msgType.typea)
            {
                EVC1_MMIDynamic.MMI_M_SLIDE = 0;
                EVC1_MMIDynamic.MMI_M_SLIP = 1;
                EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Normal_Status_Ceiling_Speed_Monitoring; // 0
                EVC1_MMIDynamic.MMI_A_TRAIN = 0;
                EVC1_MMIDynamic.MMI_V_TRAIN = 3888;
                EVC1_MMIDynamic.MMI_V_TARGET = 1111;
                EVC1_MMIDynamic.MMI_V_PERMITTED = 1111;
                EVC1_MMIDynamic.MMI_V_RELEASE = 555;
                EVC1_MMIDynamic.MMI_O_BRAKETARGET = 0;
                EVC1_MMIDynamic.MMI_O_IML = 0;
                EVC1_MMIDynamic.MMI_V_INTERVENTION = 0;
            }
            else if (type == msgType.typeb)
            {
                EVC1_MMIDynamic.MMI_M_SLIDE = 1;
                EVC1_MMIDynamic.MMI_M_SLIP = 0;
                EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Normal_Status_Ceiling_Speed_Monitoring; // 0
                EVC1_MMIDynamic.MMI_A_TRAIN = 0;
                EVC1_MMIDynamic.MMI_V_TRAIN = 3888;
                EVC1_MMIDynamic.MMI_V_TARGET = 1111;
                EVC1_MMIDynamic.MMI_V_PERMITTED = 1111;
                EVC1_MMIDynamic.MMI_V_RELEASE = 555;
                EVC1_MMIDynamic.MMI_O_BRAKETARGET = 0;
                EVC1_MMIDynamic.MMI_O_IML = 0;
                EVC1_MMIDynamic.MMI_V_INTERVENTION = 0;
            }
            else if (type == msgType.typec)
            {
                EVC1_MMIDynamic.MMI_M_SLIDE = 1;
                EVC1_MMIDynamic.MMI_M_SLIP = 1;
                EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Normal_Status_Ceiling_Speed_Monitoring; // 0
                EVC1_MMIDynamic.MMI_A_TRAIN = 0;
                EVC1_MMIDynamic.MMI_V_TRAIN = 3888;
                EVC1_MMIDynamic.MMI_V_TARGET = 1111;
                EVC1_MMIDynamic.MMI_V_PERMITTED = 1111;
                EVC1_MMIDynamic.MMI_V_RELEASE = 555;
                EVC1_MMIDynamic.MMI_O_BRAKETARGET = 0;
                EVC1_MMIDynamic.MMI_O_IML = 0;
                EVC1_MMIDynamic.MMI_V_INTERVENTION = 0;
            }
        }

        #endregion
    }
}
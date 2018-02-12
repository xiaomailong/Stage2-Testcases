using System;
using Testcase.Telegrams.EVCtoDMI;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 17.12 Slip Indication
    /// TC-ID: 12.12
    /// 
    /// This test case verifies the display of the ‘Slip’ indication which complies with conditions of  [MMI-ETCS-gen].
    /// 
    /// Tested Requirements:
    /// MMI_gen 1694; MMI_gen 1695; MMI_gen 1692 (partly: ETC speed, slip); MMI_gen 1079 (partly: slip); MMI_gen 7013 (partly: slip); MMI_gen 1696 (partly: slip); MMI_gen 7012 (partly: slip); MMI_gen 1693; MMI_gen 9516 (partly: slip indication); MMI_gen 12025 (partly: slip indication);
    /// 
    /// Scenario:
    /// SoM is completed in SR mode, Level 1 and SLIP_SPEEDMETER is configured to be 1 (display)At 100 m, pass BG1 with pkt 12, pkt 21 and pkt 
    /// 27.Mode changes to FS mode.The ‘Slip’Slide’ indication is verified by the following cases:ATP disable MMI_M_SLIP and MMI_M_SLIDEATP enable MMI_M_SLIP and MMI_M_SLIDEATP enable MMI_M_SLIP but disable MMI_M_SLIDEATP disable MMI_M_SLIP bu enable MMI_M_SLIDEThe train is stopped.
    /// 
    /// Used files:
    /// 12_12.tdg, 12_12_a.xml, 12_12_b.xml, 12_12_c.xml
    /// </summary>
    public class TC_ID_12_12_Train_Speed : TestcaseBase
    {
        public override bool TestcaseEntryPoint()
        {
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 21801;
            // Testcase entrypoint
            StartUp();

            DmiActions.Complete_SoM_L1_SR(this);

            TraceInfo("This test case requires an ATP configuration change - " +
                      "See Precondition requirements. If this is not done manually, the test may fail!");

            MakeTestStepHeader(1, UniqueIdentifier++, "Drive the train forward", "DMI changes from SR mode to FS mode");
            /*
            Test Step 1
            Action: Drive the train forward
            Expected Result: DMI changes from SR mode to FS mode
            */
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.FullSupervision;
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 5;

            // Call generic Check Results Method
            DmiExpectedResults.FS_mode_displayed(this);

            MakeTestStepHeader(2, UniqueIdentifier++, "Drive the train forward with speed = 140 km/h",
                "The speed pointer is displayed with speed =140.Verify the following information,Use the log file to confirm that DMI received packet EVC-1 with the following variables,MMI_M_SLIP = 0MMI_M_SLIDE = 0");
            /*
            Test Step 2
            Action: Drive the train forward with speed = 140 km/h
            Expected Result: The speed pointer is displayed with speed =140.Verify the following information,Use the log file to confirm that DMI received packet EVC-1 with the following variables,MMI_M_SLIP = 0MMI_M_SLIDE = 0
            Test Step Comment: (1) MMI_gen 1694(partly: slip is not set), MMI_gen 1695(partly: slide is not set), MMI_gen 1692 (partly: ETC speed, slip);   
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 140;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The speed pointer is displayed with speed = 140 km/h.");

            MakeTestStepHeader(3, UniqueIdentifier++,
                "Use the test script file 12_12_a.xml to send EVC-1 with,MMI_M_SLIP = 1MMI_M_SLIDE = 0",
                "Verify the following information,The Slip indication is displayed and shown as arrow pointing clockwise.The colour of Slip indication is displayed as same as speed digital colour. The Slip indication is displayed on speed hub of the speed pointer. DMI plays sound Sinfo once");
            /*
            Test Step 3
            Action: Use the test script file 12_12_a.xml to send EVC-1 with,MMI_M_SLIP = 1MMI_M_SLIDE = 0
            Expected Result: Verify the following information,The Slip indication is displayed and shown as arrow pointing clockwise.The colour of Slip indication is displayed as same as speed digital colour. The Slip indication is displayed on speed hub of the speed pointer. DMI plays sound Sinfo once
            Test Step Comment: (1) MMI_gen 1079 (partly: slip, presented),   MMI_gen 1694(partly: slip is set), MMI_gen 1695(partly: slide is not set), MMI_gen 1692 (partly: ETC speed, slip);   (2) MMI_gen 7013(partly: slip);(3) MMI_gen 1696 (partly:slip);(4) MMI_gen 7012 (partly: slip); MMI_gen 9516 (partly: slip indication); MMI_gen 12025 (partly: slip indication);
            */
            XML_12_12(msgType.typea);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The Slip indication is displayed and shown as arrow pointing clockwise." +
                                Environment.NewLine +
                                "2. The Slip indication and digital speed are displayed in the same colour." +
                                Environment.NewLine +
                                "3. The Slip indication is displayed on the speed hub of the speed pointer." +
                                Environment.NewLine +
                                "4. DMI plays sound Sinfo once.");

            MakeTestStepHeader(4, UniqueIdentifier++,
                "Use the test script file 12_12_b.xml to send EVC-1 with,MMI_M_SLIP = 0MMI_M_SLIDE =1",
                "Verify the following information,The ‘Slip/Slide’ indication is not displayed on the speed hub");
            /*
            Test Step 4
            Action: Use the test script file 12_12_b.xml to send EVC-1 with,MMI_M_SLIP = 0MMI_M_SLIDE =1
            Expected Result: Verify the following information,The ‘Slip/Slide’ indication is not displayed on the speed hub
            Test Step Comment: (1) MMI_gen 1079 (partly: slip, presented),   MMI_gen 1694(partly: slip is not set), MMI_gen 1695(partly: slide is set), MMI_gen 1692 (partly: ETC speed);   
            */
            XML_12_12(msgType.typeb);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Slip / Slide’ indication is not displayed on the speed hub.");

            MakeTestStepHeader(5, UniqueIdentifier++,
                "Use the test script file 12_12_c.xml to send EVC-1 with,MMI_M_SLIP = 1MMI_M_SLIDE =1",
                "Verify the following information,The ‘Slip’ indication is displayed on the speed hub as a clockwise arrow");
            /*
            Test Step 5
            Action: Use the test script file 12_12_c.xml to send EVC-1 with,MMI_M_SLIP = 1MMI_M_SLIDE =1
            Expected Result: Verify the following information,The ‘Slip’ indication is displayed on the speed hub as a clockwise arrow
            Test Step Comment: (1) MMI_gen 1079 (partly: slip, presented),   MMI_gen 1694(partly: slip is set), MMI_gen 1695(partly: slide is set), MMI_gen 1693, MMI_gen 1692 (partly: ETC speed, slip);
            */
            XML_12_12(msgType.typec);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Slip’ indication is displayed on the speed hub as a clockwise arrow.");

            MakeTestStepHeader(6, UniqueIdentifier++, "Stop the train", "Train is stand still");
            /*
            Test Step 6
            Action: Stop the train
            Expected Result: Train is stand still
            */
            EVC1_MMIDynamic.MMI_V_TRAIN = 0;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The speed is indicated as 0");

            TraceHeader("End of test");

            /*
            Test Step 7
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }

        #region Send_XML_12_12_DMI_Test_Specification

        enum msgType
        {
            typea,
            typeb,
            typec
        }

        private void XML_12_12(msgType type)
        {
            //SITR.ETCS1.Dynamic.EVC01Validity1.Value = 0x0;
            //SITR.ETCS1.Dynamic.EVC01Validity2.Value = 0x0;
            SITR.ETCS1.Dynamic.EVC01Validity1.Value = 0xc800;
            SITR.ETCS1.Dynamic.EVC01Validity2.Value = 0xff00;

            if (type == msgType.typea)
            {
                EVC1_MMIDynamic.MMI_M_SLIDE = 0;
                EVC1_MMIDynamic.MMI_M_SLIP = 1;
                EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Normal_Status_Ceiling_Speed_Monitoring; // 0
                EVC1_MMIDynamic.MMI_A_TRAIN = 0;
                EVC1_MMIDynamic.MMI_V_TRAIN = 100;
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
                EVC1_MMIDynamic.MMI_V_TRAIN = 100;
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
                EVC1_MMIDynamic.MMI_V_TRAIN = 100;
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
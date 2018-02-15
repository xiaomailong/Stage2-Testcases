using System;
using Testcase.Telegrams.EVCtoDMI;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 17.13 Slide Indication
    /// TC-ID: 12.13
    /// 
    /// This test case verifies the display of the ‘Slide’ indication with ETC speed which complies with conditions of  [MMI-ETCS-gen].
    /// 
    /// Tested Requirements:
    /// MMI_gen 1694; MMI_gen 1695; MMI_gen 1692 (partly: ETC speed, slide); MMI_gen 1079 (partly: slide); MMI_gen 7013 (partly: slide); MMI_gen 1696 (partly: slide); MMI_gen 7012 (partly: slide); MMI_gen 1693; MMI_gen 9516 (partly: slide indication); MMI_gen 12025 (partly: slide indication);
    /// 
    /// Scenario:
    /// SoM is completed in SR mode, Level 1 and SLIDE_SPEEDMETER is configured to be 1 (display)At 100 m, pass BG1 with pkt 12, pkt 21 and pkt 
    /// 27.Mode changes to FS mode.The ‘Slip/Slide’ indication is verified by the following cases:ATP disable MMI_M_SLIP and MMI_M_SLIDEATP enable MMI_M_SLIP and MMI_M_SLIDEATP enable MMI_M_SLIP but disable MMI_M_SLIDEATP disable MMI_M_SLIP bu enable MMI_M_SLIDEThe train is stopped.
    /// 
    /// Used files:
    /// 12_13.tdg, 12_3_a.xml, 12_3_b.xml, 12_3_c.xml
    /// </summary>
    public class TC_ID_12_13_Train_Speed : TestcaseBase
    {
        public override bool TestcaseEntryPoint()
        {
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 21818;

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
                "The speed pointer is displayed with speed =140.Verify the following information,Use the log file to confirm that DMI receives EVC-1 with following variables,MMI_M_SLIPE = 0MMI_M_SLIDE = 0");
            /*
            Test Step 2
            Action: Drive the train forward with speed = 140 km/h
            Expected Result: The speed pointer is displayed with speed =140.Verify the following information,Use the log file to confirm that DMI receives EVC-1 with following variables,MMI_M_SLIPE = 0MMI_M_SLIDE = 0
            Test Step Comment: (1) MMI_gen 1694 (partly: slip is not set), MMI_gen 1695 (partly: slide is not set), MMI_gen 1692 (partly: ETC speed, slide);
            */
            EVC1_MMIDynamic.MMI_M_SLIP = EVC1_MMIDynamic.MMI_M_SLIDE = 0;
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 140;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The speed pointer is displayed with speed = 140.");

            MakeTestStepHeader(3, UniqueIdentifier++,
                "Use the test script file 12_13_a.xml to send EVC-1 with,MMI_M_SLIP = 0MMI_M_SLIDE =1",
                "Verify the following information,The Slide indication is displayed and shown as arrow pointing counterclockwise.The colour of Slide indication is black as same as speed digital colour.The Slide indication is displayed on speed hub of the speed pointer.DMI plays sound Sinfo once");
            /*
            Test Step 3
            Action: Use the test script file 12_13_a.xml to send EVC-1 with,MMI_M_SLIP = 0MMI_M_SLIDE =1
            Expected Result: Verify the following information,The Slide indication is displayed and shown as arrow pointing counterclockwise.The colour of Slide indication is black as same as speed digital colour.The Slide indication is displayed on speed hub of the speed pointer.DMI plays sound Sinfo once
            Test Step Comment: (1) MMI_gen 1079 (partly: slide, presented),   MMI_gen 1695(partly: slide is set), MMI_gen 1694(partly: slip is not set), MMI_gen 1692 (partly: ETC speed, slide);   (2) MMI_gen 7013(partly: slide);(3) MMI_gen 1696(partly: slide);(4) MMI_gen 7012(partly: slide); MMI_gen 9516 (partly: slide indication); MMI_gen 12025 (partly: slide indication);
            */
            XML_12_13(msgType.typea);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The Slide indication is displayed and shown as arrow pointing counterclockwise." +
                                Environment.NewLine +
                                "2. The Slide indication and digital speed are in black." + Environment.NewLine +
                                "3. The Slide indication is displayed on the speed hub of the speed pointer." +
                                Environment.NewLine +
                                "4. DMI plays sound Sinfo once.");

            MakeTestStepHeader(4, UniqueIdentifier++,
                "Use the test script file 12_13_b.xml to send EVC-1 with,MMI_M_SLIP = 1MMI_M_SLIDE =0",
                "Verify the following information,The ‘Slip/Slide’ indication is not displayed on the speed hub. Sound Sinfo is not played");
            /*
            Test Step 4
            Action: Use the test script file 12_13_b.xml to send EVC-1 with,MMI_M_SLIP = 1MMI_M_SLIDE =0
            Expected Result: Verify the following information,The ‘Slip/Slide’ indication is not displayed on the speed hub. Sound Sinfo is not played
            Test Step Comment: (1) MMI_gen 1079 (partly: slide, presented),   MMI_gen 1694(partly: slip is set), MMI_gen 1695(partly: slide is not set), MMI_gen 1692 (partly: ETC speed);   (2) MMI_gen 7012(partly: no indication)
            */
            XML_12_13(msgType.typeb);
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Slip / Slide’ indication is not displayed on the speed hub." +
                                Environment.NewLine +
                                "2. Sound Sinfo is not played.");

            MakeTestStepHeader(5, UniqueIdentifier++,
                "Use the test script file 12_13_c.xml to send EVC-1 with,MMI_M_SLIP = 1MMI_M_SLIDE =1",
                "Verify the following information,The ‘Slip/Slide’ indication is not displayed on the speed hub. Sound Sinfo is not played");
            /*
            Test Step 5
            Action: Use the test script file 12_13_c.xml to send EVC-1 with,MMI_M_SLIP = 1MMI_M_SLIDE =1
            Expected Result: Verify the following information,The ‘Slip/Slide’ indication is not displayed on the speed hub. Sound Sinfo is not played
            Test Step Comment: (1) MMI_gen 1079 (partly: slide, presented),   MMI_gen 1694(partly: slip is set), MMI_gen 1695(partly: slide is set), MMI_gen 1693 (partly: under configuration), MMI_gen 1692 (partly: ETC speed);(2) MMI_gen 7012(partly: no indication);
            */
            XML_12_13(msgType.typec);

            // Test spec says this:
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Slip / Slide’ indication is not displayed on the speed hub." +
                                Environment.NewLine +
                                "2. Sound Sinfo is not played.");

            // ?? according to reference RS_ETC_R4 conditions are met for displaying slide indication and sounding Sinfo because slide is displayed

            TraceHeader("End of test");

            /*
            Test Step 6
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }

        #region Send_XML_12_13_DMI_Test_Specification

        enum msgType
        {
            typea,
            typeb,
            typec
        }

        private void XML_12_13(msgType type)
        {
            //EVC1_MMIDynamic.SetValidityBits(false);

            if (type == msgType.typea)
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
            else if (type == msgType.typeb)
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
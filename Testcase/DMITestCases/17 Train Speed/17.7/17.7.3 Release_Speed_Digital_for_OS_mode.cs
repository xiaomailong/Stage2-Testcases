using System;
using Testcase.Telegrams.EVCtoDMI;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 17.7.3 Release Speed Digital for OS mode
    /// TC-ID: 12.7.3
    /// 
    /// This test case verifies that the display of release speed digital including the toggling function and provided that the release speed is changed by received packet EVC-1.
    /// 
    /// Tested Requirements:
    /// MMI_gen 6586; MMI_gen 6468 (partly: OS); MMI_gen 6467; MMI_gen 9516 (partly: toggling function of release speed digital); MMI_gen 12025 (partly: toggling function of release speed digital);
    /// 
    /// Scenario:
    /// 1.Drive the train forward pass BG1 at position 100m. Then, acknowledge the OS mode acknowledgement.BG1: Packet 12, 21, 27 and 80 (Entering OS)
    /// 2.Continue to drive the train forward. Then, verify the toggle of release speed digital refer to received packet EVC-1 and EVC-7.
    /// 3.Use the test script file to send EVC-1 with an outside range value of MMI_V_RELEASE. Then, verify that release speed digital is removed.
    /// 
    /// Used files:
    /// 12_7_3.tdg, 12_7_3_a.xml
    /// </summary>
    public class TC_12_7_3_Train_Speed : TestcaseBase
    {
        public override bool TestcaseEntryPoint()
        {
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 21755;
            // Testcase entrypoint
            StartUp();
            DmiActions.Complete_SoM_L1_SR(this);

            MakeTestStepHeader(1, UniqueIdentifier++,
                "Drive the train forward pass BG1.Then, press an acknowledgement of OS mode in sub-area C1",
                "DMI displays in OS mode, level 1");
            /*
            Test Step 1
            Action: Drive the train forward pass BG1.Then, press an acknowledgement of OS mode in sub-area C1
            Expected Result: DMI displays in OS mode, level 1
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 5;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 259;
            EVC8_MMIDriverMessage.Send();

            DmiActions.ShowInstruction(this, "Acknowledge OS mode in sub-area C1");
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.OnSight;

            WaitForVerification("Acknowledge OS mode in sub-area C1 and check the following:" + Environment.NewLine +
                                Environment.NewLine +
                                "1. DMI displays in OS mode, Level 1.");


            MakeTestStepHeader(2, UniqueIdentifier++, "Driving the train with speed equal to 30 km/h",
                "When a Release speed exists, verify the following information,(1)   DMI displays the release speed digital in sub-area B6.(2)   Sound 'Sinfo' is played once.(3)   Use the log file to confirm that the appearance of the release speed digital is controlled by data packet from ETCS Onboard as follows,EVC-7: OBU_TR_M_MODE = 1 (OS Mode) EVC-1: MMI_V_RELEASE = 1111 (~40 km/h) EVC-1: MMI_M_WARNING != 0, 4, 8, 12 (Not CSM)");
            /*
            Test Step 2
            Action: Driving the train with speed equal to 30 km/h
            Expected Result: When a Release speed exists, verify the following information,(1)   DMI displays the release speed digital in sub-area B6.(2)   Sound 'Sinfo' is played once.(3)   Use the log file to confirm that the appearance of the release speed digital is controlled by data packet from ETCS Onboard as follows,EVC-7: OBU_TR_M_MODE = 1 (OS Mode) EVC-1: MMI_V_RELEASE = 1111 (~40 km/h) EVC-1: MMI_M_WARNING != 0, 4, 8, 12 (Not CSM)
            Test Step Comment: (1) MMI_gen 6586 (partly: toggle on);(2) MMI_gen 6586 (partly: sound Sinfo); MMI_gen 9516 (partly: toggling function of release speed digital); MMI_gen 12025 (partly: toggling function of release speed digital);(3) MMI_gen 6586 (partly: Release speed changes); MMI_gen 6468 (partly: OS);
            */
            // EVC7_MMIEtcsMiscOutSignals Send
            EVC1_MMIDynamic.MMI_V_RELEASE = 1111;
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 30;
            EVC1_MMIDynamic.MMI_M_WARNING =
                MMI_M_WARNING.Indication_Status_Target_Speed_Monitoring; // not 0, 4, 8, 12(Not CSM)
            // ?? Send

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the digital release speed in sub-area B6." + Environment.NewLine +
                                "2. Sound 'Sinfo' is played once.");

            MakeTestStepHeader(3, UniqueIdentifier++,
                "Use the test script file 12_7_3_a.xml to send EVC-1 with,MMI_V_RELEASE = 11112",
                "Verify the following information,(1)   Tthe release speed digital in sub-area B6 is removed");
            /*
            Test Step 3
            Action: Use the test script file 12_7_3_a.xml to send EVC-1 with,MMI_V_RELEASE = 11112
            Expected Result: Verify the following information,(1)   Tthe release speed digital in sub-area B6 is removed
            Test Step Comment: (1) MMI_gen 11112;
            */
            XML_12_7_3();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The digital release speed in sub-area B6 is removed.");

            TraceHeader("End of test");

            /*
            Test Step 4
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }

        #region Send_XML_12_7_3_DMI_Test_Specification

        private void XML_12_7_3()
        {
            EVC1_MMIDynamic.MMI_M_SLIDE = 0;
            EVC1_MMIDynamic.MMI_M_SLIP = 1;
            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Spare; // 7
            EVC1_MMIDynamic.MMI_A_TRAIN = 0;
            EVC1_MMIDynamic.MMI_V_TRAIN = 100;
            EVC1_MMIDynamic.MMI_V_TARGET = 1111;
            EVC1_MMIDynamic.MMI_V_PERMITTED = 833;
            EVC1_MMIDynamic.MMI_V_RELEASE = 11112;
            EVC1_MMIDynamic.MMI_O_BRAKETARGET = 10002000;
            EVC1_MMIDynamic.MMI_O_IML = 0;
            EVC1_MMIDynamic.MMI_V_INTERVENTION = 0;

            SITR.ETCS1.Dynamic.EVC01Validity1.Value = 0x0;
            SITR.ETCS1.Dynamic.EVC01Validity2.Value = 0x0;
            //SITR.ETCS1.EtcsMiscOutSignals.EVC7Validity1.Value = 4415; // All validity bits set
            //SITR.ETCS1.EtcsMiscOutSignals.EVC7Validity2.Value = 63;   // All validity bits set
        }

        #endregion
    }
}
using System;
using Testcase.Telegrams.EVCtoDMI;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 18.1.8 Distance to Target Bar: Maximum digit of Distance to Target Digital
    /// TC-ID: 13.1.8
    /// 
    /// This test case verifies the display of distance to target digital when the received packet information EVC-1 is greater than maximum value, the distance to target is able to display up to 5 digit with no leading zero.
    /// 
    /// Tested Requirements:
    /// MMI_gen 104 (partly: no leading zero, able to show up to 5 digits, right aligned); MMI_gen 6776 (partly: Greater distance); MMI_gen 105 (partly: equation, MMI_O_BRAKETARGET – OBU_TR_O_TRAIN); MMI_gen 6771; MMI_gen 6877;
    /// 
    /// Scenario:
    /// 1.Drive the train forward pass BG1 at 100m. Then, verify the display of distance to target bar with received packet information EVC-1 and EVC-
    /// 7.BG1: Packet 12, 21 and 27 (Entering FS)
    /// 2.Use the test script file to send an invalid value in EVC-1 and EVC-
    /// 7.Then, verify that distance to target bar and scale is removed. 
    /// 
    /// Used files:
    /// 13_1_8.tdg, 13_1_8_a.xml, 13_1_8_b.xml, 13_1_8_c.xml
    /// </summary>
    public class TC_13_1_8_Brake : TestcaseBase
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

            // DMI displays in FS mode, Level 1
            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 0;
            // Testcase entrypoint

            TraceHeader("Test Step 1");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Drive the train forward pass BG1.Then, stop the train");
            TraceReport("Expected Result");
            TraceInfo(
                "DMI displays in FS mode, level 1.Verify the following information,(1)   Use the log file to confirm that the distance to target (bar and digital) is calculated from the received packet information EVC-7 and EVC -1 as follows,(EVC-1) MMI_O_BRAKETARGET  – (EVC-7) OBU_TR_O_TRAIN Example: The observation point of the distance target is 4480. [EVC-1.MMI_O_BRAKETARGET = 1000498078] – [EVC-7.OBU_TR_O_TRAIN = 1000050121] = 447,957 cm (4479.57m)(2)   The first digit of distance to target digital in sub-area A2 is not zero. (3)   The distane to target digital is right aligned");
            /*
            Test Step 1
            Action: Drive the train forward pass BG1.Then, stop the train
            Expected Result: DMI displays in FS mode, level 1.Verify the following information,(1)   Use the log file to confirm that the distance to target (bar and digital) is calculated from the received packet information EVC-7 and EVC -1 as follows,(EVC-1) MMI_O_BRAKETARGET  – (EVC-7) OBU_TR_O_TRAIN Example: The observation point of the distance target is 4480. [EVC-1.MMI_O_BRAKETARGET = 1000498078] – [EVC-7.OBU_TR_O_TRAIN = 1000050121] = 447,957 cm (4479.57m)(2)   The first digit of distance to target digital in sub-area A2 is not zero. (3)   The distane to target digital is right aligned
            Test Step Comment: (1) MMI_gen 105 (partly: equation, MMI_O_BRAKETARGET – OBU_TR_O_TRAIN); MMI_gen 104 (partly: able to show); MMI_gen 6771;(2) MMI_gen 104 (partly: no leading zero);(3) MMI_gen 104 (partly: right aligned);
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 10;
            EVC1_MMIDynamic.MMI_O_BRAKETARGET = 30000; // EOA 300m
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN = 10000; // at 100,
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 0;


            this.Wait_Realtime(2000);
            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Intervention_Status_PreIndication_Monitoring;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.FullSupervision;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Does the DMI display the SR mode symbol (MO09) and then replace it with the FS mode symbol (MO11) in area B7 after 2s?" +
                                Environment.NewLine +
                                "2. The first digit of digital distance to target in sub-area A2 is non-zero." +
                                Environment.NewLine +
                                "3. The digital distance to target  is right aligned.");

            TraceHeader("Test Step 2");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Use the test script file 13_1_8_a.xml to send EVC-1 with,MMI_O_BRAKETARGET = 1010500000");
            TraceReport("Expected Result");
            TraceInfo("Verify the following information,(1)   DMI display the distance to target digital as ‘99999’");
            /*
            Test Step 2
            Action: Use the test script file 13_1_8_a.xml to send EVC-1 with,MMI_O_BRAKETARGET = 1010500000
            Expected Result: Verify the following information,(1)   DMI display the distance to target digital as ‘99999’
            Test Step Comment: (1) MMI_gen 6776 (partly: Greater distance); MMI_gen 104 (partly: able to show up to 5 digits);
            */
            XML_13_1_8(msgType.typea);
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the digital distance to target  as ‘99999’.");

            TraceHeader("Test Step 3");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Use the test script file 13_1_8_b.xml to send EVC-1 with,MMI_M_WARNING = 7");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following information,(1)   The distance to target bar and digital is removed from the DMI.        After test scipt file is executed, the distance to target bar and digital is re-appear refer to received packet EVC-1 from ETCS Onboard");
            /*
            Test Step 3
            Action: Use the test script file 13_1_8_b.xml to send EVC-1 with,MMI_M_WARNING = 7
            Expected Result: Verify the following information,(1)   The distance to target bar and digital is removed from the DMI.        After test scipt file is executed, the distance to target bar and digital is re-appear refer to received packet EVC-1 from ETCS Onboard
            Test Step Comment: (1) MMI_gen 6877 (partly: MMI_M_WARNING is invalid); 
            */
            XML_13_1_8(msgType.typeb);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI stops displaying the distance to target bar and digital distance to target.");

            // Simulate the effect of the ETCS sending another 'valid' packet. Leave a gap so the DMI screen can be seen changing
            this.Wait_Realtime(2000);

            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Intervention_Status_PreIndication_Monitoring;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI re-displays the distance to target bar and digital distance to target after 2s.");

            TraceHeader("Test Step 4");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Use the test script file 13_1_8_c.xml to send EVC-7 with,OBU_TR_M_MODE = 17");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following information,(1)   The distance to target bar and digital is removed from the DMI.        After test scipt file is executed, the distance to target bar and digital is re-appear refer to received packet EVC-1 from ETCS Onboard");
            /*
            Test Step 4
            Action: Use the test script file 13_1_8_c.xml to send EVC-7 with,OBU_TR_M_MODE = 17
            Expected Result: Verify the following information,(1)   The distance to target bar and digital is removed from the DMI.        After test scipt file is executed, the distance to target bar and digital is re-appear refer to received packet EVC-1 from ETCS Onboard
            Test Step Comment: (1) MMI_gen 6877 (partly: OBU_TR_M_MODE is invalid); 
            */
            XML_13_1_8(msgType.typec);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI stops displaying the distance to target bar and digital distance to target.");

            // Simulate the effect of the ETCS sending another 'valid' packet. Leave a gap so the DMI screen can be seen changing
            this.Wait_Realtime(2000);

            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.FullSupervision;
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI re-displays the distance to target bar and digital distance to target after 2s.");

            TraceHeader("Test Step 5");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("End of test");
            TraceReport("Expected Result");
            TraceInfo("");
            /*
            Test Step 5
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }

        #region Send_XML_13_1_8_DMI_Test_Specification

        enum msgType
        {
            typea,
            typeb,
            typec
        }

        private void XML_13_1_8(msgType type)
        {
            switch (type)
            {
                case msgType.typea:
                    //SITR.ETCS1.Dynamic.EVC01Validity1.Value = 0x0;
                    //SITR.ETCS1.Dynamic.EVC01Validity2.Value = 0x0;

                    EVC1_MMIDynamic.MMI_M_SLIDE = 0;
                    EVC1_MMIDynamic.MMI_M_SLIP = 1;
                    EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Normal_Status_Target_Speed_Monitoring; // 11
                    EVC1_MMIDynamic.MMI_A_TRAIN = 0;
                    EVC1_MMIDynamic.MMI_V_TRAIN = 0;
                    EVC1_MMIDynamic.MMI_V_TARGET = -1; // 0xff would be 65535 as unsigned short
                    EVC1_MMIDynamic.MMI_V_PERMITTED = 2222;
                    EVC1_MMIDynamic.MMI_V_RELEASE = 0;
                    EVC1_MMIDynamic.MMI_O_BRAKETARGET = 1010500000;
                    EVC1_MMIDynamic.MMI_O_IML = 0;
                    EVC1_MMIDynamic.MMI_V_INTERVENTION = 0;
                    break;

                case msgType.typeb:
                    //SITR.ETCS1.Dynamic.EVC01Validity1.Value = 0x0;
                    //SITR.ETCS1.Dynamic.EVC01Validity2.Value = 0x0;
                    EVC1_MMIDynamic.MMI_M_SLIDE = 0;
                    EVC1_MMIDynamic.MMI_M_SLIP = 1;
                    // DMI not responsive to this so chose another mode that removes distance to target items
                    //EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Spare;   // 7
                    EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Intervention_Status_Ceiling_Speed_Monitoring;
                    EVC1_MMIDynamic.MMI_A_TRAIN = 0;
                    EVC1_MMIDynamic.MMI_V_TRAIN = 0;
                    EVC1_MMIDynamic.MMI_V_TARGET = -1; // 0xff would be 65535 as unsigned short
                    EVC1_MMIDynamic.MMI_V_PERMITTED = 2222;
                    EVC1_MMIDynamic.MMI_V_RELEASE = 0;
                    EVC1_MMIDynamic.MMI_O_BRAKETARGET = 1010500000;
                    EVC1_MMIDynamic.MMI_O_IML = 0;
                    EVC1_MMIDynamic.MMI_V_INTERVENTION = 0;
                    break;

                case msgType.typec:
                    //SITR.ETCS1.EtcsMiscOutSignals.EVC7Validity1.Value = 4096;         // bit 12 MMI_OBU_TR_M_Level
                    //SITR.ETCS1.EtcsMiscOutSignals.EVC7Validity2.Value = 1;            // bit 0  MMI_OBU_TR_M_Mode
                    EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_EBTestInProgress = 0;
                    EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_EB_Status = 0;
                    EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_RadioStatus = 0;
                    EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_STM_HS_ENABLED = 0;
                    EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_STM_DA_ENABLED = 0;
                    EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_BrakeTest_Status =
                        EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_BRAKETEST_STATUS.BrakeTestNotInProgress;
                    EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Level = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_LEVEL.L0;
                    EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode =
                        EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.Invalid; // 17
                    EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_ADHESION = 0;
                    EVC7_MMIEtcsMiscOutSignals.OBU_TR_NID_STM_HS = 0;
                    EVC7_MMIEtcsMiscOutSignals.OBU_TR_NID_STM_DA = 0;
                    EVC7_MMIEtcsMiscOutSignals.BRAKE_TEST_TIMEOUT = 0;
                    EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN = 10000000;
                    break;
            }
        }

        #endregion
    }
}
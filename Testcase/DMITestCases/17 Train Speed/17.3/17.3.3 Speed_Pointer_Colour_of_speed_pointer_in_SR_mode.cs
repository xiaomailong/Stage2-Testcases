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
using Testcase.XML;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 17.3.3 Speed Pointer: Colour of speed pointer in SR mode
    /// TC-ID: 12.3.3
    /// 
    /// This test case verifies the colour of speed pointer which display refer to received packet EVC-1 and EVC-7 for SR mode.
    /// 
    /// Tested Requirements:
    /// MMI_gen 6299 (partly: SR mode);
    /// 
    /// Scenario:
    /// 1.Drive the train forward with specify speed. Then, verify the colour of speed pointer refer to received packet EVC-1 and EVC-7.
    /// 2.Use the test script file to send EVC-1 and EVC-7 with specify value. Then, verify the colour of speed pointer.Note: Tester need to execute script file repeatly due to the packet will be interrupted by dynamic packet EVC-1 and EVC-7 which send from ETCS onboard.
    /// 
    /// Used files:
    /// 12_3_3_a.xml, 12_3_3_b.xml, 12_3_3_c.xml, 12_3_3_d.xml, 12_3_3_e.xml, 12_3_3_f.xml, 12_3_3_g.xml, 12_3_3_h.xml, 12_3_3_i.xml, 12_3_3_j.xml, 12_3_3_k.xml, 12_3_3_l.xml, 12_3_3_m.xml, 12_3_3_n.xml, 12_3_3_o.xml
    /// </summary>
    public class TC_12_3_3_Train_Speed : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // Test system is powered on.Cabin is activated.SoM is performed in SR mode, Level 1.

            // Call the TestCaseBase PreExecution
            base.PreExecution();
            DmiActions.Complete_SoM_L1_SR(this);
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in SR mode, level 1
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in SR mode, Level 1.");

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint
                        
            /*
            Test Step 1
            Action: Drive the train forward with speed = 40 km/h
            Expected Result: DMI displays in SR mode, level 1.Verify the following information,(1)   Use the log file to confirm that DMI received the packet information EVC-1 and EVC-7 with following variables,(EVC-7) OBU_TR_M_MODE = 2 (Staff Responsible)(EVC-1) MMI_M_WARNING = 0 (Status = NoS, Supervision = CSM)(EVC-1) MMI_V_PERMITTED = 1111 (40km/h)(2)   The speed pointer display in grey colour
            Test Step Comment: (1) MMI_gen 6299 (partly: OBU_TR_M_MODE, MMI_M_WARNING, train speed in relation to permitted speed MMI_V_PERMITTED, SR mode in CSM supervision);(2) MMI_gen 6299 (partly: colour of speed pointer, SR mode in CSM supervision);
            */
            EVC1_MMIDynamic.MMI_V_PERMITTED = 1111;
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 40;
            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Normal_Status_Ceiling_Speed_Monitoring;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in SR mode, level 1." + Environment.NewLine + 
                                "2. Is the speed pointer grey?");

            /*
            Test Step 2
            Action: Increase the train speed to 41 km/h
            Expected Result: Verify the following information,(1)   Use the log file to confirm that DMI received the packet information EVC-1 with the following condition,MMI_M_WARNING = 8 (Status = OvS, Supervision = CSM) while the value of MMI_V_TRAIN = 1139 (41 km/h) which greater than MMI_V_PERMITTED(2)   The speed pointer display in orange colour
            Test Step Comment: (1) MMI_gen 6299 (partly: MMI_M_WARNING, train speed in relation to permitted speed MMI_V_PERMITTED, SR mode in CSM supervision);(2) MMI_gen 6299 (partly: colour of speed pointer, SR mode in CSM supervision);
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 41;
            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Overspeed_Status_Ceiling_Speed_Monitoring;
            //?? Send
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Is the speed pointer orange?");

            /*
            Test Step 3
            Action: Increase the train speed to 45 km/h.Note: dV_warning_max is defined in chapter 3 of [SUBSET-026]
            Expected Result: Verify the following information,(1)   Use the log file to confirm that DMI received the packet information EVC-1 with the following condition,MMI_M_WARNING = 4 (Status = WaS, Supervision = CSM) while the value of MMI_V_TRAIN = 1250 (45 km/h) which greater than MMI_V_PERMITTED but lower than MMI_V_INTERVENTION(2)   The speed pointer display in orange colour
            Test Step Comment: (1) MMI_gen 6299 (partly: MMI_M_WARNING, train speed in relation to permitted speed MMI_V_PERMITTED, SR mode in CSM supervision);(2) MMI_gen 6299 (partly: colour of speed pointer, SR mode in CSM supervision);
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 45;
            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Warning_Status_Ceiling_Speed_Monitoring;
            //?? Send
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Is the speed pointer orange?");

            /*
            Test Step 4
            Action: Increase the train speed to 46 km/h
            Expected Result: The train speed is force to decrease because of emergency brake is applied by ETCS onboard.Verify the following information,Before train speed is decreased(1)   Use the log file to confirm that DMI received the packet information EVC-1 with the following condition,MMI_M_WARNING = 12 (Status = IntS, Supervision = CSM) while the value of MMI_V_TRAIN = 1278 (46 km/h) which greater than MMI_V_INTERVENTION(2)   The speed pointer display in red colourAfter train speed is decreased(3)   Use the log file to confirm that DMI received the packet information EVC-1 with the following condition,MMI_M_WARNING = 12 (Status = IntS, Supervision = CSM) while the value of MMI_V_TRAIN is lower than MMI_V_INTERVENTION(4)   The speed pointer display in grey colour
            Test Step Comment: (1) MMI_gen 6299 (partly: MMI_M_WARNING, train speed in relation to permitted speed MMI_V_PERMITTED, SR mode in CSM supervision);(2) MMI_gen 6299 (partly: colour of speed pointer, SR mode in CSM supervision);(3) MMI_gen 6299 (partly: MMI_M_WARNING, SR mode in CSM supervision);(4) MMI_gen 6299 (partly: colour of speed pointer, SR mode in CSM supervision);
            */
            // Call generic Action Method
            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Intervention_Status_Ceiling_Speed_Monitoring;
            EVC1_MMIDynamic.MMI_V_INTERVENTION_KMH = 45;
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 46;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Is the speed pointer red?");

            DmiActions.Apply_Brakes(this);
            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Intervention_Status_Ceiling_Speed_Monitoring;
            EVC1_MMIDynamic.MMI_V_INTERVENTION_KMH = 45;
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 40;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Has the speed reduced to 40 km/h?" + Environment.NewLine +
                                "2. Is the speed pointer grey?");
            /*
            Test Step 5
            Action: Stop the train.Then, use the test script file 12_3_3_a.xml to send the following packets,EVC-1MMI_M_WARNING = 2MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 972EVC-7OBU_TR_M_MODE = 2
            Expected Result: DMI displays in SR mode, level 1.Verify the following information,(1)   The speed pointer display in grey colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, SR mode in PIM supervision);
            */
            XML_12_3_3(msgType.typea);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in SR mode, level 1" + Environment.NewLine +
                                "2. Is the speed pointer grey?");

            /*
            Test Step 6
            Action: Use the test script file 12_3_3_b.xml to send the following packets,EVC-1MMI_M_WARNING = 2MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1111EVC-7OBU_TR_M_MODE = 2
            Expected Result: DMI displays in SR mode, level 1.Verify the following information,(1)   The speed pointer display in white colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, SR mode in PIM supervision);
            */
            XML_12_3_3(msgType.typeb);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in SR mode, level 1" + Environment.NewLine +
                                "2. Is the speed pointer white?");

            /*
            Test Step 7
            Action: Use the test script file 12_3_3_c.xml to send the following packets,EVC-1MMI_M_WARNING = 10MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1139EVC-7OBU_TR_M_MODE = 2
            Expected Result: DMI displays in SR mode, level 1.Verify the following information,(1)   The speed pointer display in orange colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, SR mode in PIM supervision);
            */
            XML_12_3_3(msgType.typec);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in SR mode, level 1" + Environment.NewLine +
                                "2. Is the speed pointer orange?");

            /*
            Test Step 8
            Action: Use the test script file 12_3_3_d.xml to send the following packets,EVC-1MMI_M_WARNING = 6MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1250EVC-7OBU_TR_M_MODE = 2
            Expected Result: DMI displays in SR mode, level 1.Verify the following information,(1)   The speed pointer display in orange colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, SR mode in PIM supervision);
            */
            XML_12_3_3(msgType.typed);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in SR mode, level 1" + Environment.NewLine +
                                "2. Is the speed pointer orange?");

            /*
            Test Step 9
            Action: Use the test script file 12_3_3_e.xml to send the following packets,EVC-1MMI_M_WARNING = 14MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1277EVC-7OBU_TR_M_MODE = 2
            Expected Result: DMI displays in SR mode, level 1.Verify the following information,(1)   The speed pointer display in red colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, SR mode in PIM supervision);
            */
            XML_12_3_3(msgType.typee);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in SR mode, level 1" + Environment.NewLine +
                                "2. Is the speed pointer red?");

            /*
            Test Step 10
            Action: Use the test script file 12_3_3_f.xml to send the following packets,EVC-1MMI_M_WARNING = 14MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1111EVC-7OBU_TR_M_MODE = 2
            Expected Result: DMI displays in SR mode, level 1.Verify the following information,(1)   The speed pointer display in white colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, SR mode in PIM supervision);
            */
            XML_12_3_3(msgType.typef);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in SR mode, level 1" + Environment.NewLine +
                                "2. Is the speed pointer white?");

            /*
            Test Step 11
            Action: Use the test script file 12_3_3_g.xml to send the following packets,EVC-1MMI_M_WARNING = 14MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1000EVC-7OBU_TR_M_MODE = 2
            Expected Result: DMI displays in SR mode, level 1.Verify the following information,(1)   The speed pointer display in grey colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, SR mode in PIM supervision);
            */
            XML_12_3_3(msgType.typeg);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in SR mode, level 1" + Environment.NewLine +
                                "2. Is the speed pointer grey?");

            /*
            Test Step 12
            Action: Use the test script file 12_3_3_h.xml to send the following packets,EVC-1MMI_M_WARNING = 11MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1000EVC-7OBU_TR_M_MODE = 2
            Expected Result: DMI displays in SR mode, level 1.Verify the following information,(1)   The speed pointer display in grey colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, SR mode in TSM supervision);
            */
            XML_12_3_3(msgType.typeh);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in SR mode, level 1" + Environment.NewLine +
                                "2. Is the speed pointer grey?");

            /*
            Test Step 13
            Action: Use the test script file 12_3_3_i.xml to send the following packets,EVC-1MMI_M_WARNING = 11MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1111EVC-7OBU_TR_M_MODE = 2
            Expected Result: DMI displays in SR mode, level 1.Verify the following information,(1)   The speed pointer display in white colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, SR mode in TSM supervision);
            */
            XML_12_3_3(msgType.typei);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in SR mode, level 1" + Environment.NewLine +
                                "2. Is the speed pointer white?");

            /*
            Test Step 14
            Action: Use the test script file 12_3_3_j.xml to send the following packets,EVC-1MMI_M_WARNING = 1MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1111EVC-7OBU_TR_M_MODE = 2
            Expected Result: DMI displays in SR mode, level 1.Verify the following information,(1)   The speed pointer display in yellow colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, SR mode in TSM supervision);
            */
            XML_12_3_3(msgType.typej);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in SR mode, level 1" + Environment.NewLine +
                                "2. Is the speed pointer yellow?");

            /*
            Test Step 15
            Action: Use the test script file 12_3_3_k.xml to send the following packets,EVC-1MMI_M_WARNING = 9MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1139EVC-7OBU_TR_M_MODE = 2
            Expected Result: DMI displays in SR mode, level 1.Verify the following information,(1)   The speed pointer display in orange colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, SR mode in TSM supervision);
            */
            XML_12_3_3(msgType.typek);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in SR mode, level 1" + Environment.NewLine +
                                "2. Is the speed pointer orange?");

            /*
            Test Step 16
            Action: Use the test script file 12_3_3_l.xml to send the following packets,EVC-1MMI_M_WARNING = 5MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1250EVC-7OBU_TR_M_MODE = 2
            Expected Result: DMI displays in SR mode, level 1.Verify the following information,(1)   The speed pointer display in orange colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, SR mode in TSM supervision);
            */
            XML_12_3_3(msgType.typel);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in SR mode, level 1" + Environment.NewLine +
                                "2. Is the speed pointer orange?");

            /*
            Test Step 17
            Action: Use the test script file 12_3_3_m.xml to send the following packets,EVC-1MMI_M_WARNING = 13MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1277EVC-7OBU_TR_M_MODE = 2
            Expected Result: DMI displays in SR mode, level 1.Verify the following information,(1)   The speed pointer display in red colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, SR mode in TSM supervision);
            */
            XML_12_3_3(msgType.typem);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in SR mode, level 1" + Environment.NewLine +
                                "2. Is the speed pointer red?");


            /*
            Test Step 18
            Action: Use the test script file 12_3_3_n.xml to send the following packets,EVC-1MMI_M_WARNING = 13MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1111EVC-7OBU_TR_M_MODE = 2
            Expected Result: DMI displays in SR mode, level 1.Verify the following information,(1)   The speed pointer display in yellow colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, SR mode in TSM supervision);
            */
            XML_12_3_3(msgType.typen);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in SR mode, level 1" + Environment.NewLine +
                                "2. Is the speed pointer yellow?");

            /*
            Test Step 19
            Action: Use the test script file 12_3_3_o.xml to send the following packets,EVC-1MMI_M_WARNING = 13MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1083EVC-7OBU_TR_M_MODE = 2
            Expected Result: DMI displays in SR mode, level 1.Verify the following information,(1)   The speed pointer display in grey colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, SR mode in TSM supervision);
            */
            XML_12_3_3(msgType.typeo);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in SR mode, level 1" + Environment.NewLine +
                                "2. Is the speed pointer grey?");

            /*
            Test Step 20
            Action: End of test
            Expected Result: 
            */


            return GlobalTestResult;
        }
        #region Send_XML_12_3_3_DMI_Test_Specification
        enum msgType
        {
            typea,
            typeb,
            typec,
            typed,
            typee,
            typef,
            typeg,
            typeh,
            typei,
            typej,
            typek,
            typel,
            typem,
            typen,
            typeo
        }

        private void XML_12_3_3(msgType type)
        {
            EVC1_MMIDynamic.MMI_M_SLIDE = 0;
            EVC1_MMIDynamic.MMI_M_SLIP = 0;
            EVC1_MMIDynamic.MMI_O_BRAKETARGET = 0;
            EVC1_MMIDynamic.MMI_O_IML = 0;
            EVC1_MMIDynamic.MMI_V_INTERVENTION = 1250;
            EVC1_MMIDynamic.MMI_A_TRAIN = 0;
            EVC1_MMIDynamic.MMI_V_PERMITTED = 1111;
            EVC1_MMIDynamic.MMI_V_TARGET = 1083;

            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_EBTestInProgress = 0;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_EB_Status = 0;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_RadioStatus = 0;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_STM_HS_ENABLED = 0;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_STM_DA_ENABLED = 0;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_BrakeTest_Status =
                EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_BRAKETEST_STATUS.BrakeTestNotInProgress;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Level = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_LEVEL.L1;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode =
                EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.StaffResponsible;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_ADHESION = 100; // "Spare"
            EVC7_MMIEtcsMiscOutSignals.OBU_TR_NID_STM_HS = 255;
            EVC7_MMIEtcsMiscOutSignals.OBU_TR_NID_STM_DA = 255;
            EVC7_MMIEtcsMiscOutSignals.BRAKE_TEST_TIMEOUT = 46;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN = 1000000000;
            //SITR.ETCS1.EtcsMiscOutSignals.EVC7Validity1.Value = 4415; // All validity bits set
            //SITR.ETCS1.EtcsMiscOutSignals.EVC7Validity2.Value = 63;   // All validity bits set


            switch (type)
            {
                case msgType.typea:

                    EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Normal_Status_PreIndication_Monitoring; // 2
                    EVC1_MMIDynamic.MMI_V_TRAIN = 972;

                    break;
                case msgType.typeb :

                    EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Normal_Status_PreIndication_Monitoring; // 2
                    EVC1_MMIDynamic.MMI_V_TRAIN = 1111;

                    break;
                case msgType.typec:

                    EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Overspeed_Status_PreIndication_Monitoring; // 10
                    EVC1_MMIDynamic.MMI_V_TRAIN = 1139;

                    break;
                case msgType.typed:

                    EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Warning_Status_PreIndication_Monitoring; // 6
                    EVC1_MMIDynamic.MMI_V_TRAIN = 1250;

                    break;
                case msgType.typee:

                    EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Intervention_Status_PreIndication_Monitoring; // 14
                    EVC1_MMIDynamic.MMI_V_TRAIN = 1277;

                    break;
                case msgType.typef:

                    EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Intervention_Status_PreIndication_Monitoring; // 14
                    EVC1_MMIDynamic.MMI_V_TRAIN = 1111;

                    break;
                case msgType.typeg:

                    EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Intervention_Status_PreIndication_Monitoring; // 14
                    EVC1_MMIDynamic.MMI_V_TRAIN = 1000;

                    break;
                case msgType.typeh:

                    EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Normal_Status_Target_Speed_Monitoring; // 11
                    EVC1_MMIDynamic.MMI_V_TRAIN = 1000;

                    break;
                case msgType.typei:

                    EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Normal_Status_Target_Speed_Monitoring; // 11
                    EVC1_MMIDynamic.MMI_V_TRAIN = 1111;

                    break;
                case msgType.typej:

                    EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Indication_Status_Target_Speed_Monitoring; // 1
                    EVC1_MMIDynamic.MMI_V_TRAIN = 1111;

                    break;
                case msgType.typek:

                    EVC1_MMIDynamic.MMI_M_WARNING =
                        MMI_M_WARNING.Overspeed_Status_Indication_Status_Target_Speed_Monitoring; // 9
                    EVC1_MMIDynamic.MMI_V_TRAIN = 1139;

                    break;
                case msgType.typel:

                    EVC1_MMIDynamic.MMI_M_WARNING =
                        MMI_M_WARNING.Warning_Status_Indication_Status_Target_Speed_Monitoring; // 5
                    EVC1_MMIDynamic.MMI_V_TRAIN = 1250;

                    break;
                case msgType.typem:
                    EVC1_MMIDynamic.MMI_M_WARNING =
                        MMI_M_WARNING.Intervention_Status_Indication_Status_Target_Speed_Monitoring; // 13
                    EVC1_MMIDynamic.MMI_V_TRAIN = 1277;

                    break;
                case msgType.typen:

                    EVC1_MMIDynamic.MMI_M_WARNING =
                        MMI_M_WARNING.Intervention_Status_Indication_Status_Target_Speed_Monitoring; // 13
                    EVC1_MMIDynamic.MMI_V_TRAIN = 1111;

                    break;
                case msgType.typeo:

                    EVC1_MMIDynamic.MMI_M_WARNING =
                        MMI_M_WARNING.Intervention_Status_Indication_Status_Target_Speed_Monitoring; // 13
                    EVC1_MMIDynamic.MMI_V_TRAIN = 1083;

                    break;
            }
        }
        #endregion

    }
}
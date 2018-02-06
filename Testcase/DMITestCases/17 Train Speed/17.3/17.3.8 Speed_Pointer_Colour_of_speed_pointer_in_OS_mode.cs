using System;
using Testcase.Telegrams.EVCtoDMI;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 17.3.8 Speed Pointer: Colour of speed pointer in OS mode
    /// TC-ID: 12.3.8
    /// 
    /// This test case verifies the colour of speed pointer which display refer to received packet EVC-1 and EVC-7 for OS mode.
    /// 
    /// Tested Requirements:
    /// MMI_gen 6299 (partly: OS mode);
    /// 
    /// Scenario:
    /// 1.Drive the train forward pass BG1 at position 100mBG1: packet 12, 21, 27 and 80 (Entering OS mode)
    /// 2.Enter OS mode, level 
    /// 1.Then, drive the train with specify speed and verify the display of speed pointer refer to received packet EVC-1.
    /// 3.Use the test script file to send EVC-1 and EVC-7 with specify value. Then, verify the colour of speed pointer.
    /// 
    /// Used files:
    /// 12_3_8.tdg, 12_3_8_a.xml, 12_3_8_b.xml, 12_3_8_c.xml, 12_3_8_d.xml, 12_3_8_e.xml, 12_3_8_f.xml, 12_3_8_g.xml, 12_3_8_h.xml, 12_3_8_i.xml, 12_3_8_j.xml, 12_3_8_k.xml, 12_3_8_l.xml, 12_3_8_m.xml, 12_3_8_n.xml, 12_3_8_o.xml, 12_3_8_p.xml, 12_3_8_q.xml, 12_3_8_r.xml, 12_3_8_s.xml
    /// </summary>
    public class TC_12_3_8_Train_Speed : TestcaseBase
    {
        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in OS mode, level 1
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in OS mode, Level 1.");

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 0;
            // Testcase entrypoint

            StartUp();

            DmiActions.Complete_SoM_L1_SR(this);

            MakeTestStepHeader(1, UniqueIdentifier++,
                "Drive the train forward pass BG1. Then, press an acknowledgement of OS mode in sub-area C1",
                "DMI displays in OS mode, level 1");
            /*
            Test Step 1
            Action: Drive the train forward pass BG1. Then, press an acknowledgement of OS mode in sub-area C1
            Expected Result: DMI displays in OS mode, level 1
            */
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.OnSight;
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 5;

            DmiActions.Send_OS_Mode_Ack(this);
            DmiExpectedResults.OS_Mode_Ack_pressed_and_released(this);

            DmiActions.Send_OS_Mode(this);
            DmiExpectedResults.OS_Mode_displayed(this);

            MakeTestStepHeader(2, UniqueIdentifier++, "Drive the train forward with speed = 30 km/h",
                "DMI displays in OS mode, level 1.Verify the following information,");
            /*
            Test Step 2
            Action: Drive the train forward with speed = 30 km/h
            Expected Result: DMI displays in OS mode, level 1.Verify the following information,
            (1)   Use the log file to confirm that DMI received the packet information EVC-1 and EVC-7 with following variables,
                (EVC-7) OBU_TR_M_MODE = 1 (On-sight)
                (EVC-1) MMI_M_WARNING = 0 (Status = NoS, Supervision = CSM)
                (EVC-1) MMI_V_PERMITTED = 833 (30km/h)
            (2)   The speed pointer display in grey colour
            Test Step Comment: (1) MMI_gen 6299 (partly: OBU_TR_M_MODE, MMI_M_WARNING, train speed in relation to permitted speed MMI_V_PERMITTED, OS mode in CSM supervision);(2) MMI_gen 6299 (partly: colour of speed pointer, OS mode in CSM supervision);
            */
            EVC1_MMIDynamic.MMI_V_PERMITTED = 833;
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 30;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in OS mode, level 1." + Environment.NewLine +
                                "2. Is the speed pointer grey?");

            MakeTestStepHeader(3, UniqueIdentifier++, "Increase the train speed to 31 km/h",
                "Verify the following information,");
            /*
            Test Step 3
            Action: Increase the train speed to 31 km/h
            Expected Result: Verify the following information,
            (1)   Use the log file to confirm that DMI received the packet information EVC-1 with the following condition,
                MMI_M_WARNING = 8 (Status = OvS, Supervision = CSM) while the value of MMI_V_TRAIN = 861 (31 km/h) which greater than MMI_V_PERMITTED
            (2)   The speed pointer display in orange colour
            Test Step Comment: (1) MMI_gen 6299 (partly: MMI_M_WARNING, train speed in relation to permitted speed MMI_V_PERMITTED, OS mode in CSM supervision);(2) MMI_gen 6299 (partly: colour of speed pointer, OS mode in CSM supervision);
            */
            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Overspeed_Status_Ceiling_Speed_Monitoring;
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 31;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Is the speed pointer orange?");
            MakeTestStepHeader(4, UniqueIdentifier++,
                "Increase the train speed to 35 km/h.Note: dV_warning_max is defined in chapter 3 of [SUBSET-026]",
                "Verify the following information,");
            /*
            Test Step 4
            Action: Increase the train speed to 35 km/h.Note: dV_warning_max is defined in chapter 3 of [SUBSET-026]
            Expected Result: Verify the following information,
            (1)   Use the log file to confirm that DMI received the packet information EVC-1 with the following condition,
                MMI_M_WARNING = 4 (Status = WaS, Supervision = CSM) while the value of MMI_V_TRAIN = 972 (35 km/h) which greater than MMI_V_PERMITTED but lower than MMI_V_INTERVENTION
            (2)   The speed pointer display in orange colour
            Test Step Comment: (1) MMI_gen 6299 (partly: MMI_M_WARNING, train speed in relation to permitted speed MMI_V_PERMITTED, OS mode in CSM supervision);(2) MMI_gen 6299 (partly: colour of speed pointer, OS mode in CSM supervision);
            */
            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Warning_Status_Ceiling_Speed_Monitoring;
            EVC1_MMIDynamic.MMI_V_INTERVENTION_KMH = 35;
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 35;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Is the speed pointer orange?");

            MakeTestStepHeader(5, UniqueIdentifier++, "Increase the train speed to 36 km/h",
                "The train speed is force to decrease because of emergency brake is applied by ETCS onboard.Verify the following information,Before train speed is decreased(1)   Use the log file to confirm that DMI received the packet information EVC-1 with the following condition,MMI_M_WARNING = 12 (Status = IntS, Supervision = CSM) while the value of MMI_V_TRAIN = 1000 (36 km/h) which greater than MMI_V_INTERVENTION(2)   The speed pointer display in red colourAfter train speed is decreased(3)   Use the log file to confirm that DMI received the packet information EVC-1 with the following condition,MMI_M_WARNING = 12 (Status = IntS, Supervision = CSM) while the value of MMI_V_TRAIN is lower than MMI_V_INTERVENTION(4)   The speed pointer display in grey colour");
            /*
            Test Step 5
            Action: Increase the train speed to 36 km/h
            Expected Result: The train speed is force to decrease because of emergency brake is applied by ETCS onboard.Verify the following information,Before train speed is decreased(1)   Use the log file to confirm that DMI received the packet information EVC-1 with the following condition,MMI_M_WARNING = 12 (Status = IntS, Supervision = CSM) while the value of MMI_V_TRAIN = 1000 (36 km/h) which greater than MMI_V_INTERVENTION(2)   The speed pointer display in red colourAfter train speed is decreased(3)   Use the log file to confirm that DMI received the packet information EVC-1 with the following condition,MMI_M_WARNING = 12 (Status = IntS, Supervision = CSM) while the value of MMI_V_TRAIN is lower than MMI_V_INTERVENTION(4)   The speed pointer display in grey colour
            Test Step Comment: (1) MMI_gen 6299 (partly: MMI_M_WARNING, train speed in relation to permitted speed MMI_V_PERMITTED, OS mode in CSM supervision);(2) MMI_gen 6299 (partly: colour of speed pointer, OS mode in CSM supervision);(3) MMI_gen 6299 (partly: MMI_M_WARNING, OS mode in CSM supervision);(4) MMI_gen 6299 (partly: colour of speed pointer, OS mode in CSM supervision);
            */
            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Intervention_Status_Ceiling_Speed_Monitoring;
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 36;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Is the speed pointer red?");

            DmiActions.Apply_Brakes(this);
            MakeTestStepHeader(6, UniqueIdentifier++,
                "Stop the train.Then, use the test script file 12_3_8_a.xml to send the following packets,EVC-1MMI_M_WARNING = 2MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 972EVC-7OBU_TR_M_MODE = 1",
                "DMI displays in OS mode, level 1.Verify the following information,(1)   The speed pointer display in grey colour");
            /*
            Test Step 6
            Action: Stop the train.Then, use the test script file 12_3_8_a.xml to send the following packets,EVC-1MMI_M_WARNING = 2MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 972EVC-7OBU_TR_M_MODE = 1
            Expected Result: DMI displays in OS mode, level 1.Verify the following information,(1)   The speed pointer display in grey colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, OS mode in PIM supervision);
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 0;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in OS mode, level 1.");

            XML_12_3_8(msgType.typea);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Is the speed pointer grey?");

            MakeTestStepHeader(7, UniqueIdentifier++,
                "Use the test script file 12_3_8_b.xml to send the following packets,EVC-1MMI_M_WARNING = 2MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1111EVC-7OBU_TR_M_MODE = 1",
                "DMI displays in OS mode, level 1.Verify the following information,(1)   The speed pointer display in white colour");
            /*
            Test Step 7
            Action: Use the test script file 12_3_8_b.xml to send the following packets,EVC-1MMI_M_WARNING = 2MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1111EVC-7OBU_TR_M_MODE = 1
            Expected Result: DMI displays in OS mode, level 1.Verify the following information,(1)   The speed pointer display in white colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, OS mode in PIM supervision);
            */
            XML_12_3_8(msgType.typeb);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Is the speed pointer white?");

            MakeTestStepHeader(8, UniqueIdentifier++,
                "Use the test script file 12_3_8_c.xml to send the following packets,EVC-1MMI_M_WARNING = 10MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1139EVC-7OBU_TR_M_MODE = 1",
                "DMI displays in OS mode, level 1.Verify the following information,(1)   The speed pointer display in orange colour");
            /*
            Test Step 8
            Action: Use the test script file 12_3_8_c.xml to send the following packets,EVC-1MMI_M_WARNING = 10MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1139EVC-7OBU_TR_M_MODE = 1
            Expected Result: DMI displays in OS mode, level 1.Verify the following information,(1)   The speed pointer display in orange colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, OS mode in PIM supervision);
            */
            XML_12_3_8(msgType.typec);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Is the speed pointer orange?");

            MakeTestStepHeader(9, UniqueIdentifier++,
                "Use the test script file 12_3_8_d.xml to send the following packets,EVC-1MMI_M_WARNING = 6MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1250EVC-7OBU_TR_M_MODE = 1",
                "DMI displays in OS mode, level 1.Verify the following information,(1)   The speed pointer display in orange colour");
            /*
            Test Step 9
            Action: Use the test script file 12_3_8_d.xml to send the following packets,EVC-1MMI_M_WARNING = 6MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1250EVC-7OBU_TR_M_MODE = 1
            Expected Result: DMI displays in OS mode, level 1.Verify the following information,(1)   The speed pointer display in orange colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, OS mode in PIM supervision);
            */
            XML_12_3_8(msgType.typed);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Is the speed pointer orange?");

            MakeTestStepHeader(10, UniqueIdentifier++,
                "Use the test script file 12_3_8_e.xml to send the following packets,EVC-1MMI_M_WARNING = 14MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1277EVC-7OBU_TR_M_MODE = 1",
                "DMI displays in OS mode, level 1.Verify the following information,(1)   The speed pointer display in red colour");
            /*
            Test Step 10
            Action: Use the test script file 12_3_8_e.xml to send the following packets,EVC-1MMI_M_WARNING = 14MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1277EVC-7OBU_TR_M_MODE = 1
            Expected Result: DMI displays in OS mode, level 1.Verify the following information,(1)   The speed pointer display in red colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, OS mode in PIM supervision);
            */
            XML_12_3_8(msgType.typee);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Is the speed pointer red?");

            MakeTestStepHeader(11, UniqueIdentifier++,
                "Use the test script file 12_3_8_f.xml to send the following packets,EVC-1MMI_M_WARNING = 14MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1111EVC-7OBU_TR_M_MODE = 1",
                "DMI displays in OS mode, level 1.Verify the following information,(1)   The speed pointer display in white colour");
            /*
            Test Step 11
            Action: Use the test script file 12_3_8_f.xml to send the following packets,EVC-1MMI_M_WARNING = 14MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1111EVC-7OBU_TR_M_MODE = 1
            Expected Result: DMI displays in OS mode, level 1.Verify the following information,(1)   The speed pointer display in white colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, OS mode in PIM supervision);
            */
            XML_12_3_8(msgType.typef);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Is the speed pointer white?");

            MakeTestStepHeader(12, UniqueIdentifier++,
                "Use the test script file 12_3_8_g.xml to send the following packets,EVC-1MMI_M_WARNING = 14MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1000EVC-7OBU_TR_M_MODE = 1",
                "DMI displays in OS mode, level 1.Verify the following information,(1)   The speed pointer display in grey colour");
            /*
            Test Step 12
            Action: Use the test script file 12_3_8_g.xml to send the following packets,EVC-1MMI_M_WARNING = 14MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1000EVC-7OBU_TR_M_MODE = 1
            Expected Result: DMI displays in OS mode, level 1.Verify the following information,(1)   The speed pointer display in grey colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, OS mode in PIM supervision);
            */
            XML_12_3_8(msgType.typeg);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Is the speed pointer grey?");

            MakeTestStepHeader(13, UniqueIdentifier++,
                "Use the test script file 12_3_8_h.xml to send the following packets,EVC-1MMI_M_WARNING = 11MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1000EVC-7OBU_TR_M_MODE = 1",
                "DMI displays in OS mode, level 1.Verify the following information,(1)   The speed pointer display in grey colour");
            /*
            Test Step 13
            Action: Use the test script file 12_3_8_h.xml to send the following packets,EVC-1MMI_M_WARNING = 11MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1000EVC-7OBU_TR_M_MODE = 1
            Expected Result: DMI displays in OS mode, level 1.Verify the following information,(1)   The speed pointer display in grey colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, OS mode in TSM supervision);
            */
            XML_12_3_8(msgType.typeh);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Is the speed pointer grey?");

            MakeTestStepHeader(14, UniqueIdentifier++,
                "Use the test script file 12_3_8_i.xml to send the following packets,EVC-1MMI_M_WARNING = 11MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1111EVC-7OBU_TR_M_MODE = 1",
                "DMI displays in OS mode, level 1.Verify the following information,(1)   The speed pointer display in white colour");
            /*
            Test Step 14
            Action: Use the test script file 12_3_8_i.xml to send the following packets,EVC-1MMI_M_WARNING = 11MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1111EVC-7OBU_TR_M_MODE = 1
            Expected Result: DMI displays in OS mode, level 1.Verify the following information,(1)   The speed pointer display in white colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, OS mode in TSM supervision);
            */
            XML_12_3_8(msgType.typei);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Is the speed pointer white?");

            MakeTestStepHeader(15, UniqueIdentifier++,
                "Use the test script file 12_3_8_j.xml to send the following packets,EVC-1MMI_M_WARNING = 1MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1111EVC-7OBU_TR_M_MODE = 1",
                "DMI displays in OS mode, level 1.Verify the following information,(1)   The speed pointer display in yellow colour");
            /*
            Test Step 15
            Action: Use the test script file 12_3_8_j.xml to send the following packets,EVC-1MMI_M_WARNING = 1MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1111EVC-7OBU_TR_M_MODE = 1
            Expected Result: DMI displays in OS mode, level 1.Verify the following information,(1)   The speed pointer display in yellow colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, OS mode in TSM supervision);
            */
            XML_12_3_8(msgType.typej);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Is the speed pointer yellow?");

            MakeTestStepHeader(16, UniqueIdentifier++,
                "Use the test script file 12_3_8_k.xml to send the following packets,EVC-1MMI_M_WARNING = 1MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1083EVC-7OBU_TR_M_MODE = 1",
                "DMI displays in OS mode, level 1.Verify the following information,(1)   The speed pointer display in Grey colour");
            /*
            Test Step 16
            Action: Use the test script file 12_3_8_k.xml to send the following packets,EVC-1MMI_M_WARNING = 1MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1083EVC-7OBU_TR_M_MODE = 1
            Expected Result: DMI displays in OS mode, level 1.Verify the following information,(1)   The speed pointer display in Grey colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, OS mode in TSM supervision);
            */
            XML_12_3_8(msgType.typek);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Is the speed pointer grey?");

            MakeTestStepHeader(17, UniqueIdentifier++,
                "Use the test script file 12_3_8_l.xml to send the following packets,EVC-1MMI_M_WARNING = 9MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1139EVC-7OBU_TR_M_MODE = 1",
                "DMI displays in OS mode, level 1.Verify the following information,(1)   The speed pointer display in orange colour");
            /*
            Test Step 17
            Action: Use the test script file 12_3_8_l.xml to send the following packets,EVC-1MMI_M_WARNING = 9MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1139EVC-7OBU_TR_M_MODE = 1
            Expected Result: DMI displays in OS mode, level 1.Verify the following information,(1)   The speed pointer display in orange colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, OS mode in TSM supervision);
            */
            XML_12_3_8(msgType.typel);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Is the speed pointer orange?");

            MakeTestStepHeader(18, UniqueIdentifier++,
                "Use the test script file 12_3_8_m.xml to send the following packets,EVC-1MMI_M_WARNING = 5MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1250EVC-7OBU_TR_M_MODE = 1",
                "DMI displays in OS mode, level 1.Verify the following information,(1)   The speed pointer display in orange colour");
            /*
            Test Step 18
            Action: Use the test script file 12_3_8_m.xml to send the following packets,EVC-1MMI_M_WARNING = 5MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1250EVC-7OBU_TR_M_MODE = 1
            Expected Result: DMI displays in OS mode, level 1.Verify the following information,(1)   The speed pointer display in orange colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, OS mode in TSM supervision);
            */
            XML_12_3_8(msgType.typem);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Is the speed pointer orange?");

            MakeTestStepHeader(19, UniqueIdentifier++,
                "Use the test script file 12_3_8_n.xml to send the following packets,EVC-1MMI_M_WARNING = 13MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1277EVC-7OBU_TR_M_MODE = 1",
                "DMI displays in OS mode, level 1.Verify the following information,(1)   The speed pointer display in red colour");
            /*
            Test Step 19
            Action: Use the test script file 12_3_8_n.xml to send the following packets,EVC-1MMI_M_WARNING = 13MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1277EVC-7OBU_TR_M_MODE = 1
            Expected Result: DMI displays in OS mode, level 1.Verify the following information,(1)   The speed pointer display in red colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, OS mode in TSM supervision);
            */
            XML_12_3_8(msgType.typen);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Is the speed pointer red?");

            MakeTestStepHeader(20, UniqueIdentifier++,
                "Use the test script file 12_3_8_o.xml to send the following packets,EVC-1MMI_M_WARNING = 13MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1111EVC-7OBU_TR_M_MODE = 1",
                "DMI displays in OS mode, level 1.Verify the following information,(1)   The speed pointer display in yellow colour");
            /*
            Test Step 20
            Action: Use the test script file 12_3_8_o.xml to send the following packets,EVC-1MMI_M_WARNING = 13MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1111EVC-7OBU_TR_M_MODE = 1
            Expected Result: DMI displays in OS mode, level 1.Verify the following information,(1)   The speed pointer display in yellow colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, OS mode in TSM supervision);
            */
            XML_12_3_8(msgType.typeo);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Is the speed pointer yellow?");

            MakeTestStepHeader(21, UniqueIdentifier++,
                "Use the test script file 12_3_8_p.xml to send the following packets,EVC-1MMI_M_WARNING = 13MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1083EVC-7OBU_TR_M_MODE = 1",
                "DMI displays in OS mode, level 1.Verify the following information,(1)   The speed pointer display in grey colour");
            /*
            Test Step 21
            Action: Use the test script file 12_3_8_p.xml to send the following packets,EVC-1MMI_M_WARNING = 13MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1083EVC-7OBU_TR_M_MODE = 1
            Expected Result: DMI displays in OS mode, level 1.Verify the following information,(1)   The speed pointer display in grey colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, OS mode in TSM supervision);
            */
            XML_12_3_8(msgType.typep);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Is the speed pointer grey?");

            MakeTestStepHeader(22, UniqueIdentifier++,
                "Use the test script file 12_3_8_q.xml to send the following packets,EVC-1MMI_M_WARNING = 3MMI_V_PERMITTED = 1111MMI_V_RELEASE = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 0EVC-7OBU_TR_M_MODE = 1",
                "DMI displays in OS mode, level 1.Verify the following information,(1)   The speed pointer display in yellow colour");
            /*
            Test Step 22
            Action: Use the test script file 12_3_8_q.xml to send the following packets,EVC-1MMI_M_WARNING = 3MMI_V_PERMITTED = 1111MMI_V_RELEASE = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 0EVC-7OBU_TR_M_MODE = 1
            Expected Result: DMI displays in OS mode, level 1.Verify the following information,(1)   The speed pointer display in yellow colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, OS mode in RSM supervision);
            */
            XML_12_3_8(msgType.typeq);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Is the speed pointer yellow?");

            MakeTestStepHeader(23, UniqueIdentifier++,
                "Use the test script file 12_3_8_r.xml to send the following packets,EVC-1MMI_M_WARNING = 15MMI_V_PERMITTED = 1111MMI_V_RELEASE = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 0EVC-7OBU_TR_M_MODE = 1",
                "DMI displays in OS mode, level 1.Verify the following information,(1)   The speed pointer display in yellow colour");
            /*
            Test Step 23
            Action: Use the test script file 12_3_8_r.xml to send the following packets,EVC-1MMI_M_WARNING = 15MMI_V_PERMITTED = 1111MMI_V_RELEASE = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 0EVC-7OBU_TR_M_MODE = 1
            Expected Result: DMI displays in OS mode, level 1.Verify the following information,(1)   The speed pointer display in yellow colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, OS mode in RSM supervision);
            */
            XML_12_3_8(msgType.typer);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Is the speed pointer yellow?");

            MakeTestStepHeader(24, UniqueIdentifier++,
                "Use the test script file 12_3_8_s.xml to send the following packets,EVC-1MMI_M_WARNING = 15MMI_V_PERMITTED = 1111MMI_V_ RELEASE = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1111EVC-7OBU_TR_M_MODE = 1",
                "DMI displays in OS mode, level 1.Verify the following information,(1)   The speed pointer display in red colour");
            /*
            Test Step 24
            Action: Use the test script file 12_3_8_s.xml to send the following packets,EVC-1MMI_M_WARNING = 15MMI_V_PERMITTED = 1111MMI_V_ RELEASE = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1111EVC-7OBU_TR_M_MODE = 1
            Expected Result: DMI displays in OS mode, level 1.Verify the following information,(1)   The speed pointer display in red colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, OS mode in RSM supervision);
            */
            XML_12_3_8(msgType.types);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Is the speed pointer red?");

            MakeTestStepHeader(25, UniqueIdentifier++, "End of test", "");

            /*
            Test Step 25
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }

        #region Send_XML_12_3_8_DMI_Test_Specification

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
            typeo,
            typep,
            typeq,
            typer,
            types
        }

        private void XML_12_3_8(msgType type)
        {
            EVC1_MMIDynamic.MMI_M_SLIDE = 0;
            EVC1_MMIDynamic.MMI_M_SLIP = 0;
            EVC1_MMIDynamic.MMI_A_TRAIN = 0;
            EVC1_MMIDynamic.MMI_V_TARGET = 1083;
            EVC1_MMIDynamic.MMI_V_PERMITTED = 1111;
            EVC1_MMIDynamic.MMI_V_RELEASE = 0;
            EVC1_MMIDynamic.MMI_O_BRAKETARGET = 0;
            EVC1_MMIDynamic.MMI_O_IML = 0;
            EVC1_MMIDynamic.MMI_V_INTERVENTION = 1250;

            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_EBTestInProgress = 0;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_EB_Status = 0;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_RadioStatus = 0;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_STM_HS_ENABLED = 0;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_STM_DA_ENABLED = 0;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_BrakeTest_Status =
                EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_BRAKETEST_STATUS.BrakeTestNotInProgress;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Level = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_LEVEL.L1;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.OnSight;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_ADHESION = 100; // "Spare"
            EVC7_MMIEtcsMiscOutSignals.OBU_TR_NID_STM_HS = 255;
            EVC7_MMIEtcsMiscOutSignals.OBU_TR_NID_STM_DA = 255;
            EVC7_MMIEtcsMiscOutSignals.BRAKE_TEST_TIMEOUT = 46;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN = 1000000000;
            //_pool.SITR.ETCS1.EtcsMiscOutSignals.EVC7Validity1.Value = 4415; // All validity bits set
            //_pool.SITR.ETCS1.EtcsMiscOutSignals.EVC7Validity2.Value = 63;   // All validity bits set

            switch (type)
            {
                case msgType.typea:
                    EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Normal_Status_PreIndication_Monitoring; // 2
                    EVC1_MMIDynamic.MMI_V_TRAIN = 972;

                    break;
                case msgType.typeb:
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
                    EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Indication_Status_Target_Speed_Monitoring; // 1
                    EVC1_MMIDynamic.MMI_V_TRAIN = 1083;

                    break;
                case msgType.typel:
                    EVC1_MMIDynamic.MMI_M_WARNING =
                        MMI_M_WARNING.Overspeed_Status_Indication_Status_Target_Speed_Monitoring; // 9
                    EVC1_MMIDynamic.MMI_V_TRAIN = 1139;

                    break;
                case msgType.typem:
                    EVC1_MMIDynamic.MMI_M_WARNING =
                        MMI_M_WARNING.Warning_Status_Indication_Status_Target_Speed_Monitoring; // 5
                    EVC1_MMIDynamic.MMI_V_TRAIN = 1250;

                    break;
                case msgType.typen:
                    EVC1_MMIDynamic.MMI_M_WARNING =
                        MMI_M_WARNING.Intervention_Status_Indication_Status_Target_Speed_Monitoring; // 13
                    EVC1_MMIDynamic.MMI_V_TRAIN = 1277;

                    break;
                case msgType.typeo:
                    EVC1_MMIDynamic.MMI_M_WARNING =
                        MMI_M_WARNING.Intervention_Status_Indication_Status_Target_Speed_Monitoring; // 13
                    EVC1_MMIDynamic.MMI_V_TRAIN = 1111;

                    break;
                case msgType.typep:
                    EVC1_MMIDynamic.MMI_M_WARNING =
                        MMI_M_WARNING.Intervention_Status_Indication_Status_Target_Speed_Monitoring; // 13
                    EVC1_MMIDynamic.MMI_V_TRAIN = 1083;

                    break;
                case msgType.typeq:
                    EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Indication_Status_Release_Speed_Monitoring; // 3
                    EVC1_MMIDynamic.MMI_V_TRAIN = 0;

                    break;
                case msgType.typer:
                    EVC1_MMIDynamic.MMI_M_WARNING =
                        MMI_M_WARNING.Intervention_Status_Indication_Status_Release_Speed_Monitoring; // 15
                    EVC1_MMIDynamic.MMI_V_TRAIN = 0;

                    break;
                case msgType.types:
                    EVC1_MMIDynamic.MMI_M_WARNING =
                        MMI_M_WARNING.Intervention_Status_Indication_Status_Release_Speed_Monitoring; // 15
                    EVC1_MMIDynamic.MMI_V_TRAIN = 1111;

                    break;
            }
        }

        #endregion
    }
}
using System;
using Testcase.Telegrams.EVCtoDMI;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 17.3.7 Speed Pointer: Colour of speed pointer in LS mode
    /// TC-ID: 12.3.7
    /// 
    /// This test case verifies the colour of speed pointer which display refer to received packet EVC-1 while the train is running in each supervision status and speed monitoring for LS mode.
    /// 
    /// Tested Requirements:
    /// MMI_gen 6299 (partly: LS mode); 
    /// 
    /// Scenario:
    /// 1.Drive the train forward pass BG1 at position 100m. Then, enter LS mode by confirm an acknowledgement.BG1: Packet 12, 21, 27 and 80 (Entering LS)
    /// 2.Continue to drive the train with specify speed and verify the display of speed pointer refer to received packet EVC-1.
    /// 
    /// Used files:
    /// 12_3_7.tdg, 12_3_7_a.xml, 12_3_7_b.xml, 12_3_7_c.xml, 12_3_7_d.xml, 12_3_7_e.xml, 12_3_7_f.xml, 12_3_7_g.xml, 12_3_7_h.xml, 12_3_7_i.xml, 12_3_7_j.xml, 12_3_7_k.xml, 12_3_7_l.xml, 12_3_7_m.xml, 12_3_7_n.xml, 12_3_7_o.xml, 12_3_7_p.xml, 12_3_7_q.xml, 12_3_7_r.xml, 12_3_7_s.xml
    /// </summary>
    public class TC_12_3_7_Train_Speed : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // Test system is power on.Cabin is activated.SoM is performed in SR mode, level 1.

            // Call the TestCaseBase PreExecution
            base.PreExecution();
            DmiActions.Complete_SoM_L1_SR(this);
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in LS mode, level 1
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in LS mode, Level 1.");

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 0;
            // Testcase entrypoint


            EVC1_MMIDynamic.Initialise(this);

            TraceHeader("Test Step 1");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Drive the train forward pass BG1. Then, press an acknowledgement of LS mode in sub-area C1");
            TraceReport("Expected Result");
            TraceInfo("DMI displays in LS mode, level 1");
            /*
            Test Step 1
            Action: Drive the train forward pass BG1. Then, press an acknowledgement of LS mode in sub-area C1
            Expected Result: DMI displays in LS mode, level 1
            */
            EVC1_MMIDynamic.MMI_V_PERMITTED = 2778;
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 5;

            DmiActions.Send_LS_Mode_Ack(this);
            DmiExpectedResults.LS_Mode_Ack_pressed_and_released(this);

            DmiActions.Send_LS_Mode(this);
            DmiExpectedResults.LS_Mode_displayed(this);


            TraceHeader("Test Step 2");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Drive the train forward with speed = 100 km/h");
            TraceReport("Expected Result");
            TraceInfo("DMI displays in LS mode, level 1. Verify the following information,");
            /*
            Test Step 2
            Action: Drive the train forward with speed = 100 km/h
            Expected Result: DMI displays in LS mode, level 1. Verify the following information,
                (1)   Use the log file to confirm that DMI received the packet information EVC-1 and EVC-7 with following variables,
                    (EVC-7) OBU_TR_M_MODE = 12 (Limited Supervision)
                    (EVC-1) MMI_M_WARNING = 0 (Status = NoS, Supervision = CSM)
                    (EVC-1) MMI_V_PERMITTED = 2778 (100km/h)
                (2)   The speed pointer display in grey colour
            Test Step Comment: (1) MMI_gen 6299 (partly: OBU_TR_M_MODE, MMI_M_WARNING, train speed in relation to permitted speed MMI_V_PERMITTED, LS mode in CSM supervision);(2) MMI_gen 6299 (partly: colour of speed pointer, LS mode in CSM supervision);
            */
            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Normal_Status_Ceiling_Speed_Monitoring;
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 100;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in LS mode, level 1." + Environment.NewLine +
                                "2. Is the speed pointer grey?");

            TraceHeader("Test Step 3");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Increase the train speed to 101 km/h");
            TraceReport("Expected Result");
            TraceInfo("Verify the following information,");
            /*
            Test Step 3
            Action: Increase the train speed to 101 km/h
            Expected Result: Verify the following information,
                (1)   Use the log file to confirm that DMI received the packet information EVC-1 with the following condition,
                    MMI_M_WARNING = 8 (Status = OvS, Supervision = CSM) while the value of MMI_V_TRAIN = 2806 (101 km/h) which greater than MMI_V_PERMITTED
                (2)   The speed pointer display in orange colour
            Test Step Comment: (1) MMI_gen 6299 (partly: MMI_M_WARNING, train speed in relation to permitted speed MMI_V_PERMITTED, LS mode in CSM supervision);(2) MMI_gen 6299 (partly: colour of speed pointer, LS mode in CSM supervision);
            */
            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Overspeed_Status_Ceiling_Speed_Monitoring;
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 101;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Is the speed pointer orange?");

            TraceHeader("Test Step 4");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Increase the train speed to 105 km/h.Note: dV_warning_max is defined in chapter 3 of [SUBSET-026]");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following information,(1)   Use the log file to confirm that DMI received the packet information EVC-1 with the following condition,MMI_M_WARNING = 4 (Status = WaS, Supervision = CSM) while the value of MMI_V_TRAIN = 2917 (105 km/h) which greater than MMI_V_PERMITTED but lower than MMI_V_INTERVENTION(2)   The speed pointer display in orange colour");
            /*
            Test Step 4
            Action: Increase the train speed to 105 km/h.Note: dV_warning_max is defined in chapter 3 of [SUBSET-026]
            Expected Result: Verify the following information,(1)   Use the log file to confirm that DMI received the packet information EVC-1 with the following condition,MMI_M_WARNING = 4 (Status = WaS, Supervision = CSM) while the value of MMI_V_TRAIN = 2917 (105 km/h) which greater than MMI_V_PERMITTED but lower than MMI_V_INTERVENTION(2)   The speed pointer display in orange colour
            Test Step Comment: (1) MMI_gen 6299 (partly: MMI_M_WARNING, train speed in relation to permitted speed MMI_V_PERMITTED, LS mode in CSM supervision);(2) MMI_gen 6299 (partly: colour of speed pointer, LS mode in CSM supervision);
            */
            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Warning_Status_Ceiling_Speed_Monitoring;
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 105;
            EVC1_MMIDynamic.MMI_V_INTERVENTION_KMH = 105;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Is the speed pointer orange?");

            TraceHeader("Test Step 5");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Increase the train speed to 106 km/h");
            TraceReport("Expected Result");
            TraceInfo(
                "The train speed is force to decrease because of emergency brake is applied by ETCS onboard.Verify the following information,Before train speed is decreased");
            /*
            Test Step 5 
            Action: Increase the train speed to 106 km/h
            Expected Result: The train speed is force to decrease because of emergency brake is applied by ETCS onboard.Verify the following information,Before train speed is decreased
                (1)   Use the log file to confirm that DMI received the packet information EVC-1 with the following condition,
                    MMI_M_WARNING = 12 (Status = IntS, Supervision = CSM) while the value of MMI_V_TRAIN = 2944 (106 km/h) which greater than MMI_V_INTERVENTION
                (2)   The speed pointer display in red colourAfter train speed is decreased
                (3)   Use the log file to confirm that DMI received the packet information EVC-1 with the following condition,
                    MMI_M_WARNING = 12 (Status = IntS, Supervision = CSM) while the value of MMI_V_TRAIN is lower than MMI_V_INTERVENTION
                (4)   The speed pointer display in grey colour
            Test Step Comment: (1) MMI_gen 6299 (partly: MMI_M_WARNING, train speed in relation to permitted speed MMI_V_PERMITTED, LS mode in CSM supervision);(2) MMI_gen 6299 (partly: colour of speed pointer, LS mode in CSM supervision);(3) MMI_gen 6299 (partly: MMI_M_WARNING, LS mode in CSM supervision);(4) MMI_gen 6299 (partly: colour of speed pointer, LS mode in CSM supervision);
            */
            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Intervention_Status_Ceiling_Speed_Monitoring;
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 106;
            EVC1_MMIDynamic.MMI_V_INTERVENTION_KMH = 105;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Is the speed pointer red?");

            DmiActions.Apply_Brakes(this);
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 99;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Has the speed decreased to 99 km/h?" + Environment.NewLine +
                                "2. Is the speed pointer grey?");
            TraceHeader("Test Step 6");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Stop the train.Then, use the test script file 12_3_7_a.xml to send the following packets,");
            TraceReport("Expected Result");
            TraceInfo("DMI displays in LS mode, level 1.");
            /*
            Test Step 6 indicated also as 5
            Action: Stop the train.Then, use the test script file 12_3_7_a.xml to send the following packets,
            EVC-1
            MMI_M_WARNING = 2
            MMI_V_PERMITTED = 1111
            MMI_V_TARGET = 1083
            MMI_V_INTERVENTION = 1250
            MMI_V_TRAIN = 972
            EVC-7
            OBU_TR_M_MODE = 12
            Expected Result: DMI displays in LS mode, level 1.
            Verify the following information,
            (1)   The speed pointer display in grey colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, LS mode in PIM supervision);
            */
            EVC1_MMIDynamic.MMI_V_TRAIN = 0;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in LS mode, level 1.");

            XML_12_3_7(msgType.typea);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in LS mode, level 1." + Environment.NewLine +
                                "2. Is the speed pointer grey?");

            TraceHeader("Test Step 7");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Use the test script file 12_3_7_b.xml to send the following packets,EVC-1MMI_M_WARNING = 2MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1111EVC-7OBU_TR_M_MODE = 12");
            TraceReport("Expected Result");
            TraceInfo(
                "DMI displays in LS mode, level 1.Verify the following information,(1)   The speed pointer display in grey colour");
            /*
            Test Step 7 indicated as 6
            Action: Use the test script file 12_3_7_b.xml to send the following packets,EVC-1MMI_M_WARNING = 2MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1111EVC-7OBU_TR_M_MODE = 12
            Expected Result: DMI displays in LS mode, level 1.Verify the following information,(1)   The speed pointer display in grey colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, LS mode in PIM supervision);
            */
            XML_12_3_7(msgType.typeb);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in LS mode, level 1." + Environment.NewLine +
                                "2. Is the speed pointer grey?");

            TraceHeader("Test Step 8");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Use the test script file 12_3_7_c.xml to send the following packets,EVC-1MMI_M_WARNING = 10MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1139EVC-7OBU_TR_M_MODE = 12");
            TraceReport("Expected Result");
            TraceInfo(
                "DMI displays in LS mode, level 1.Verify the following information,(1)   The speed pointer display in orange colour");
            /*
            Test Step 8 indicated as 7
            Action: Use the test script file 12_3_7_c.xml to send the following packets,EVC-1MMI_M_WARNING = 10MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1139EVC-7OBU_TR_M_MODE = 12
            Expected Result: DMI displays in LS mode, level 1.Verify the following information,(1)   The speed pointer display in orange colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, LS mode in PIM supervision);
            */
            XML_12_3_7(msgType.typec);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in LS mode, level 1." + Environment.NewLine +
                                "2. Is the speed pointer orange?");

            TraceHeader("Test Step 9");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Use the test script file 12_3_7_d.xml to send the following packets,EVC-1MMI_M_WARNING = 6MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1250EVC-7OBU_TR_M_MODE = 12");
            TraceReport("Expected Result");
            TraceInfo(
                "DMI displays in LS mode, level 1.Verify the following information,(1)   The speed pointer display in orange colour");
            /*
            Test Step 9 indicated as 8
            Action: Use the test script file 12_3_7_d.xml to send the following packets,EVC-1MMI_M_WARNING = 6MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1250EVC-7OBU_TR_M_MODE = 12
            Expected Result: DMI displays in LS mode, level 1.Verify the following information,(1)   The speed pointer display in orange colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, LS mode in PIM supervision);
            */
            XML_12_3_7(msgType.typed);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in LS mode, level 1." + Environment.NewLine +
                                "2. Is the speed pointer orange?");

            TraceHeader("Test Step 10");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Use the test script file 12_3_7_e.xml to send the following packets,EVC-1MMI_M_WARNING = 14MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1277EVC-7OBU_TR_M_MODE = 12");
            TraceReport("Expected Result");
            TraceInfo(
                "DMI displays in LS mode, level 1.Verify the following information,(1)   The speed pointer display in red colour");
            /*
            Test Step 10 indicated as 9
            Action: Use the test script file 12_3_7_e.xml to send the following packets,EVC-1MMI_M_WARNING = 14MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1277EVC-7OBU_TR_M_MODE = 12
            Expected Result: DMI displays in LS mode, level 1.Verify the following information,(1)   The speed pointer display in red colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, LS mode in PIM supervision);
            */
            XML_12_3_7(msgType.typee);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in LS mode, level 1." + Environment.NewLine +
                                "2. Is the speed pointer red?");

            TraceHeader("Test Step 11");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Use the test script file 12_3_7_f.xml to send the following packets,EVC-1MMI_M_WARNING = 14MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1111EVC-7OBU_TR_M_MODE = 12");
            TraceReport("Expected Result");
            TraceInfo(
                "DMI displays in LS mode, level 1.Verify the following information,(1)   The speed pointer display in grey colour");
            /*
            Test Step 11 indicated as 10
            Action: Use the test script file 12_3_7_f.xml to send the following packets,EVC-1MMI_M_WARNING = 14MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1111EVC-7OBU_TR_M_MODE = 12
            Expected Result: DMI displays in LS mode, level 1.Verify the following information,(1)   The speed pointer display in grey colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, LS mode in PIM supervision);
            */
            XML_12_3_7(msgType.typef);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in LS mode, level 1." + Environment.NewLine +
                                "2. Is the speed pointer grey?");


            TraceHeader("Test Step 12");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Use the test script file 12_3_7_g.xml to send the following packets,EVC-1MMI_M_WARNING = 14MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1000EVC-7OBU_TR_M_MODE = 12");
            TraceReport("Expected Result");
            TraceInfo(
                "DMI displays in LS mode, level 1.Verify the following information,(1)   The speed pointer display in grey colour");
            /*
            Test Step 12 indicated as 11
            Action: Use the test script file 12_3_7_g.xml to send the following packets,EVC-1MMI_M_WARNING = 14MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1000EVC-7OBU_TR_M_MODE = 12
            Expected Result: DMI displays in LS mode, level 1.Verify the following information,(1)   The speed pointer display in grey colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, LS mode in PIM supervision);
            */
            XML_12_3_7(msgType.typeg);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in LS mode, level 1." + Environment.NewLine +
                                "2. Is the speed pointer grey?");

            TraceHeader("Test Step 13");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Use the test script file 12_3_7_h.xml to send the following packets,EVC-1MMI_M_WARNING = 11MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1000EVC-7OBU_TR_M_MODE = 12");
            TraceReport("Expected Result");
            TraceInfo(
                "DMI displays in LS mode, level 1.Verify the following information,(1)   The speed pointer display in grey colour");
            /*
            Test Step 13 indicated as 12
            Action: Use the test script file 12_3_7_h.xml to send the following packets,EVC-1MMI_M_WARNING = 11MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1000EVC-7OBU_TR_M_MODE = 12
            Expected Result: DMI displays in LS mode, level 1.Verify the following information,(1)   The speed pointer display in grey colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, LS mode in TSM supervision);
            */
            XML_12_3_7(msgType.typeh);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in LS mode, level 1." + Environment.NewLine +
                                "2. Is the speed pointer grey?");

            TraceHeader("Test Step 14");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Use the test script file 12_3_7_i.xml to send the following packets,EVC-1MMI_M_WARNING = 11MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1111EVC-7OBU_TR_M_MODE = 12");
            TraceReport("Expected Result");
            TraceInfo(
                "DMI displays in LS mode, level 1.Verify the following information,(1)   The speed pointer display in grey colour");
            /*
            Test Step 14 indicated as 13
            Action: Use the test script file 12_3_7_i.xml to send the following packets,EVC-1MMI_M_WARNING = 11MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1111EVC-7OBU_TR_M_MODE = 12
            Expected Result: DMI displays in LS mode, level 1.Verify the following information,(1)   The speed pointer display in grey colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, LS mode in TSM supervision);
            */
            XML_12_3_7(msgType.typei);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in LS mode, level 1." + Environment.NewLine +
                                "2. Is the speed pointer grey?");

            TraceHeader("Test Step 15");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Use the test script file 12_3_7_j.xml to send the following packets,EVC-1MMI_M_WARNING = 1MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1111EVC-7OBU_TR_M_MODE = 12");
            TraceReport("Expected Result");
            TraceInfo(
                "DMI displays in LS mode, level 1.Verify the following information,(1)   The speed pointer display in grey colour");
            /*
            Test Step 15 indicated as 14
            Action: Use the test script file 12_3_7_j.xml to send the following packets,EVC-1MMI_M_WARNING = 1MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1111EVC-7OBU_TR_M_MODE = 12
            Expected Result: DMI displays in LS mode, level 1.Verify the following information,(1)   The speed pointer display in grey colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, LS mode in TSM supervision);
            */
            XML_12_3_7(msgType.typej);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in LS mode, level 1." + Environment.NewLine +
                                "2. Is the speed pointer grey?");

            TraceHeader("Test Step 16");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Use the test script file 12_3_7_k.xml to send the following packets,EVC-1MMI_M_WARNING = 9MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1139EVC-7OBU_TR_M_MODE = 12");
            TraceReport("Expected Result");
            TraceInfo(
                "DMI displays in LS mode, level 1.Verify the following information,(1)   The speed pointer display in orange colour");
            /*
            Test Step 16 indicated as 15
            Action: Use the test script file 12_3_7_k.xml to send the following packets,EVC-1MMI_M_WARNING = 9MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1139EVC-7OBU_TR_M_MODE = 12
            Expected Result: DMI displays in LS mode, level 1.Verify the following information,(1)   The speed pointer display in orange colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, LS mode in TSM supervision);
            */
            XML_12_3_7(msgType.typek);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in LS mode, level 1." + Environment.NewLine +
                                "2. Is the speed pointer orange?");

            TraceHeader("Test Step 17");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Use the test script file 12_3_7_l.xml to send the following packets,EVC-1MMI_M_WARNING = 5MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1250EVC-7OBU_TR_M_MODE = 12");
            TraceReport("Expected Result");
            TraceInfo(
                "DMI displays in LS mode, level 1.Verify the following information,(1)   The speed pointer display in orange colour");
            /*
            Test Step 17 indicated as 16
            Action: Use the test script file 12_3_7_l.xml to send the following packets,EVC-1MMI_M_WARNING = 5MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1250EVC-7OBU_TR_M_MODE = 12
            Expected Result: DMI displays in LS mode, level 1.Verify the following information,(1)   The speed pointer display in orange colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, LS mode in TSM supervision);
            */
            XML_12_3_7(msgType.typel);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in LS mode, level 1." + Environment.NewLine +
                                "2. Is the speed pointer orange?");

            TraceHeader("Test Step 18");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Use the test script file 12_3_7_m.xml to send the following packets,EVC-1MMI_M_WARNING = 13MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1277EVC-7OBU_TR_M_MODE = 12");
            TraceReport("Expected Result");
            TraceInfo(
                "DMI displays in LS mode, level 1.Verify the following information,(1)   The speed pointer display in red colour");
            /*
            Test Step 18 indicated as 17
            Action: Use the test script file 12_3_7_m.xml to send the following packets,EVC-1MMI_M_WARNING = 13MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1277EVC-7OBU_TR_M_MODE = 12
            Expected Result: DMI displays in LS mode, level 1.Verify the following information,(1)   The speed pointer display in red colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, LS mode in TSM supervision);
            */
            XML_12_3_7(msgType.typem);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in LS mode, level 1." + Environment.NewLine +
                                "2. Is the speed pointer red?");

            TraceHeader("Test Step 19");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Use the test script file 12_3_7_n.xml to send the following packets,EVC-1MMI_M_WARNING = 13MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1111EVC-7OBU_TR_M_MODE = 12");
            TraceReport("Expected Result");
            TraceInfo(
                "DMI displays in LS mode, level 1.Verify the following information,(1)   The speed pointer display in grey colour");
            /*
            Test Step 19 indicated as 18
            Action: Use the test script file 12_3_7_n.xml to send the following packets,EVC-1MMI_M_WARNING = 13MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1111EVC-7OBU_TR_M_MODE = 12
            Expected Result: DMI displays in LS mode, level 1.Verify the following information,(1)   The speed pointer display in grey colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, LS mode in TSM supervision);
            */
            XML_12_3_7(msgType.typen);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in LS mode, level 1." + Environment.NewLine +
                                "2. Is the speed pointer grey?");

            TraceHeader("Test Step 20");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Use the test script file 12_3_7_o.xml to send the following packets,EVC-1MMI_M_WARNING = 13MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1083EVC-7OBU_TR_M_MODE = 12");
            TraceReport("Expected Result");
            TraceInfo(
                "DMI displays in LS mode, level 1.Verify the following information,(1)   The speed pointer display in grey colour");
            /*
            Test Step 20 indicated as 19
            Action: Use the test script file 12_3_7_o.xml to send the following packets,EVC-1MMI_M_WARNING = 13MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1083EVC-7OBU_TR_M_MODE = 12
            Expected Result: DMI displays in LS mode, level 1.Verify the following information,(1)   The speed pointer display in grey colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, LS mode in TSM supervision);
            */
            XML_12_3_7(msgType.typeo);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in LS mode, level 1." + Environment.NewLine +
                                "2. Is the speed pointer grey?");

            TraceHeader("Test Step 21");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Use the test script file 12_3_7_p.xml to send the following packets,EVC-1MMI_M_WARNING = 3MMI_V_PERMITTED = 1111MMI_V_RELEASE = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 0EVC-7OBU_TR_M_MODE = 12");
            TraceReport("Expected Result");
            TraceInfo(
                "DMI displays in LS mode, level 1.Verify the following information,(1)   The speed pointer display in yellow colour");
            /*
            Test Step 21 indicated as 20
            Action: Use the test script file 12_3_7_p.xml to send the following packets,EVC-1MMI_M_WARNING = 3MMI_V_PERMITTED = 1111MMI_V_RELEASE = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 0EVC-7OBU_TR_M_MODE = 12
            Expected Result: DMI displays in LS mode, level 1.Verify the following information,(1)   The speed pointer display in yellow colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, LS mode in RSM supervision);
            */
            XML_12_3_7(msgType.typep);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in LS mode, level 1." + Environment.NewLine +
                                "2. Is the speed pointer yellow?");

            TraceHeader("Test Step 22");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Use the test script file 12_3_7_q.xml to send the following packets,EVC-1MMI_M_WARNING = 3MMI_V_PERMITTED = 1111MMI_V_RELEASE = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1083EVC-7OBU_TR_M_MODE = 12");
            TraceReport("Expected Result");
            TraceInfo(
                "DMI displays in LS mode, level 1.Verify the following information,(1)   The speed pointer display in yellow colour");
            /*
            Test Step 22 indicated as 21
            Action: Use the test script file 12_3_7_q.xml to send the following packets,EVC-1MMI_M_WARNING = 3MMI_V_PERMITTED = 1111MMI_V_RELEASE = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1083EVC-7OBU_TR_M_MODE = 12
            Expected Result: DMI displays in LS mode, level 1.Verify the following information,(1)   The speed pointer display in yellow colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, LS mode in RSM supervision);
            */
            XML_12_3_7(msgType.typeq);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in LS mode, level 1." + Environment.NewLine +
                                "2. Is the speed pointer yellow?");

            TraceHeader("Test Step 23");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Use the test script file 12_3_7_r.xml to send the following packets,EVC-1MMI_M_WARNING = 15MMI_V_PERMITTED = 1111MMI_V_RELEASE = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 0EVC-7OBU_TR_M_MODE = 12");
            TraceReport("Expected Result");
            TraceInfo(
                "DMI displays in LS mode, level 1.Verify the following information,(1)   The speed pointer display in yellow colour");
            /*
            Test Step 23 indicated as 22
            Action: Use the test script file 12_3_7_r.xml to send the following packets,EVC-1MMI_M_WARNING = 15MMI_V_PERMITTED = 1111MMI_V_RELEASE = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 0EVC-7OBU_TR_M_MODE = 12
            Expected Result: DMI displays in LS mode, level 1.Verify the following information,(1)   The speed pointer display in yellow colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, LS mode in RSM supervision);
            */
            XML_12_3_7(msgType.typer);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in LS mode, level 1." + Environment.NewLine +
                                "2. Is the speed pointer yellow?");

            TraceHeader("Test Step 24");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Use the test script file 12_3_7_s.xml to send the following packets,EVC-1MMI_M_WARNING = 15MMI_V_PERMITTED = 1111MMI_V_RELEASE = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1111EVC-7OBU_TR_M_MODE = 12");
            TraceReport("Expected Result");
            TraceInfo(
                "DMI displays in LS mode, level 1.Verify the following information,(1)   The speed pointer display in red colour");
            /*
            Test Step 24 indicated as 23
            Action: Use the test script file 12_3_7_s.xml to send the following packets,EVC-1MMI_M_WARNING = 15MMI_V_PERMITTED = 1111MMI_V_RELEASE = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1111EVC-7OBU_TR_M_MODE = 12
            Expected Result: DMI displays in LS mode, level 1.Verify the following information,(1)   The speed pointer display in red colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, LS mode in RSM supervision);
            */
            XML_12_3_7(msgType.types);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in LS mode, level 1." + Environment.NewLine +
                                "2. Is the speed pointer red?");

            TraceHeader("Test Step 25");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("End of test");
            
            /*
            Test Step 25
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }

        #region Send_XML_12_3_7_DMI_Test_Specification

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

        private void XML_12_3_7(msgType type)
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
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode =
                EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.LimitedSupervision;
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
                    EVC1_MMIDynamic.MMI_M_WARNING =
                        MMI_M_WARNING.Intervention_Status_PreIndication_Monitoring; // 14    
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
                case msgType.typep:
                    EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Indication_Status_Release_Speed_Monitoring; // 3
                    EVC1_MMIDynamic.MMI_V_TRAIN = 0;

                    break;
                case msgType.typeq:
                    EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Indication_Status_Release_Speed_Monitoring; // 3
                    EVC1_MMIDynamic.MMI_V_TRAIN = 1083;

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
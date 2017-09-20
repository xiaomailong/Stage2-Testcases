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
            // Testcase entrypoint
            

            EVC1_MMIDynamic.Initialise(this);

            /*
            Test Step 1
            Action: Drive the train forward pass BG1. Then, press an acknowledgement of LS mode in sub-area C1
            Expected Result: DMI displays in LS mode, level 1
            */
            EVC1_MMIDynamic.MMI_V_PERMITTED = 2778;
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 5;

            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 709;
            EVC8_MMIDriverMessage.Send();

            DmiActions.ShowInstruction(this, "Acknowledgement of LS mode is requested. Press button to accept");

            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.LimitedSupervision;
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in LS mode, level 1.");

            /*
            Test Step 2
            Action: Drive the train forward with speed = 100 km/h
            Expected Result: DMI displays in LS mode, level 1.Verify the following information,(1)   Use the log file to confirm that DMI received the packet information EVC-1 and EVC-7 with following variables,(EVC-7) OBU_TR_M_MODE = 12 (Limited Supervision)(EVC-1) MMI_M_WARNING = 0 (Status = NoS, Supervision = CSM)(EVC-1) MMI_V_PERMITTED = 2778 (100km/h)(2)   The speed pointer display in grey colour
            Test Step Comment: (1) MMI_gen 6299 (partly: OBU_TR_M_MODE, MMI_M_WARNING, train speed in relation to permitted speed MMI_V_PERMITTED, LS mode in CSM supervision);(2) MMI_gen 6299 (partly: colour of speed pointer, LS mode in CSM supervision);
            */
            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Normal_Status_Ceiling_Speed_Monitoring;
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 100;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                 "1. DMI displays in LS mode, level 1." + Environment.NewLine + 
                                 "2. Is the speed pointer grey?");

            /*
            Test Step 3
            Action: Increase the train speed to 101 km/h
            Expected Result: Verify the following information,(1)   Use the log file to confirm that DMI received the packet information EVC-1 with the following condition,MMI_M_WARNING = 8 (Status = OvS, Supervision = CSM) while the value of MMI_V_TRAIN = 2806 (101 km/h) which greater than MMI_V_PERMITTED(2)   The speed pointer display in orange colour
            Test Step Comment: (1) MMI_gen 6299 (partly: MMI_M_WARNING, train speed in relation to permitted speed MMI_V_PERMITTED, LS mode in CSM supervision);(2) MMI_gen 6299 (partly: colour of speed pointer, LS mode in CSM supervision);
            */
            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Overspeed_Status_Ceiling_Speed_Monitoring;
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 101;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                 "1. Is the speed pointer orange?");

            /*
            Test Step 4
            Action: Increase the train speed to 105 km/h.Note: dV_warning_max is defined in chapter 3 of [SUBSET-026]
            Expected Result: Verify the following information,(1)   Use the log file to confirm that DMI received the packet information EVC-1 with the following condition,MMI_M_WARNING = 4 (Status = WaS, Supervision = CSM) while the value of MMI_V_TRAIN = 2917 (105 km/h) which greater than MMI_V_PERMITTED but lower than MMI_V_INTERVENTION(2)   The speed pointer display in orange colour
            Test Step Comment: (1) MMI_gen 6299 (partly: MMI_M_WARNING, train speed in relation to permitted speed MMI_V_PERMITTED, LS mode in CSM supervision);(2) MMI_gen 6299 (partly: colour of speed pointer, LS mode in CSM supervision);
            */
            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Warning_Status_Ceiling_Speed_Monitoring;
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 105;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                 "1. Is the speed pointer orange?");
            
            /*
            Test Step 5 
            Action: Increase the train speed to 106 km/h
            Expected Result: The train speed is force to decrease because of emergency brake is applied by ETCS onboard.Verify the following information,Before train speed is decreased(1)   Use the log file to confirm that DMI received the packet information EVC-1 with the following condition,MMI_M_WARNING = 12 (Status = IntS, Supervision = CSM) while the value of MMI_V_TRAIN = 2944 (106 km/h) which greater than MMI_V_INTERVENTION(2)   The speed pointer display in red colourAfter train speed is decreased(3)   Use the log file to confirm that DMI received the packet information EVC-1 with the following condition,MMI_M_WARNING = 12 (Status = IntS, Supervision = CSM) while the value of MMI_V_TRAIN is lower than MMI_V_INTERVENTION(4)   The speed pointer display in grey colour
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
            /*
            Test Step 6 indicated also as 5
            Action: Stop the train.Then, use the test script file 12_3_7_a.xml to send the following packets,EVC-1MMI_M_WARNING = 2MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 972EVC-7OBU_TR_M_MODE = 12
            Expected Result: DMI displays in LS mode, level 1.Verify the following information,(1)   The speed pointer display in grey colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, LS mode in PIM supervision);
            */
            EVC1_MMIDynamic.MMI_V_TRAIN = 0;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                 "1. DMI displays in LS mode, level 1.");

            XML_12_3_7_a.Send(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                 "1. DMI displays in LS mode, level 1." + Environment.NewLine +
                                 "2. Is the speed pointer grey?");

            /*
            Test Step 7 indicated as 6
            Action: Use the test script file 12_3_7_b.xml to send the following packets,EVC-1MMI_M_WARNING = 2MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1111EVC-7OBU_TR_M_MODE = 12
            Expected Result: DMI displays in LS mode, level 1.Verify the following information,(1)   The speed pointer display in grey colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, LS mode in PIM supervision);
            */
            XML_12_3_7_b.Send(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                 "1. DMI displays in LS mode, level 1." + Environment.NewLine +
                                 "2. Is the speed pointer grey?");

            /*
            Test Step 8 indicated as 7
            Action: Use the test script file 12_3_7_c.xml to send the following packets,EVC-1MMI_M_WARNING = 10MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1139EVC-7OBU_TR_M_MODE = 12
            Expected Result: DMI displays in LS mode, level 1.Verify the following information,(1)   The speed pointer display in orange colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, LS mode in PIM supervision);
            */
            XML_12_3_7_c.Send(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                 "1. DMI displays in LS mode, level 1." + Environment.NewLine +
                                 "2. Is the speed pointer orange?");

            /*
            Test Step 9 indicated as 8
            Action: Use the test script file 12_3_7_d.xml to send the following packets,EVC-1MMI_M_WARNING = 6MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1250EVC-7OBU_TR_M_MODE = 12
            Expected Result: DMI displays in LS mode, level 1.Verify the following information,(1)   The speed pointer display in orange colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, LS mode in PIM supervision);
            */
            XML_12_3_7_d.Send(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                 "1. DMI displays in LS mode, level 1." + Environment.NewLine +
                                 "2. Is the speed pointer orange?");

            /*
            Test Step 10 indicated as 9
            Action: Use the test script file 12_3_7_e.xml to send the following packets,EVC-1MMI_M_WARNING = 14MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1277EVC-7OBU_TR_M_MODE = 12
            Expected Result: DMI displays in LS mode, level 1.Verify the following information,(1)   The speed pointer display in red colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, LS mode in PIM supervision);
            */
            XML_12_3_7_e.Send(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                 "1. DMI displays in LS mode, level 1." + Environment.NewLine +
                                 "2. Is the speed pointer red?");

            /*
            Test Step 11 indicated as 10
            Action: Use the test script file 12_3_7_f.xml to send the following packets,EVC-1MMI_M_WARNING = 14MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1111EVC-7OBU_TR_M_MODE = 12
            Expected Result: DMI displays in LS mode, level 1.Verify the following information,(1)   The speed pointer display in grey colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, LS mode in PIM supervision);
            */
            XML_12_3_7_f.Send(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                 "1. DMI displays in LS mode, level 1." + Environment.NewLine +
                                 "2. Is the speed pointer grey?");


            /*
            Test Step 12 indicated as 11
            Action: Use the test script file 12_3_7_g.xml to send the following packets,EVC-1MMI_M_WARNING = 14MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1000EVC-7OBU_TR_M_MODE = 12
            Expected Result: DMI displays in LS mode, level 1.Verify the following information,(1)   The speed pointer display in grey colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, LS mode in PIM supervision);
            */
            XML_12_3_7_g.Send(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                 "1. DMI displays in LS mode, level 1." + Environment.NewLine +
                                 "2. Is the speed pointer grey?");

            /*
            Test Step 13 indicated as 12
            Action: Use the test script file 12_3_7_h.xml to send the following packets,EVC-1MMI_M_WARNING = 11MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1000EVC-7OBU_TR_M_MODE = 12
            Expected Result: DMI displays in LS mode, level 1.Verify the following information,(1)   The speed pointer display in grey colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, LS mode in TSM supervision);
            */
            XML_12_3_7_h.Send(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                 "1. DMI displays in LS mode, level 1." + Environment.NewLine +
                                 "2. Is the speed pointer grey?");

            /*
            Test Step 14 indicated as 13
            Action: Use the test script file 12_3_7_i.xml to send the following packets,EVC-1MMI_M_WARNING = 11MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1111EVC-7OBU_TR_M_MODE = 12
            Expected Result: DMI displays in LS mode, level 1.Verify the following information,(1)   The speed pointer display in grey colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, LS mode in TSM supervision);
            */
            XML_12_3_7_i.Send(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                 "1. DMI displays in LS mode, level 1." + Environment.NewLine +
                                 "2. Is the speed pointer grey?");

            /*
            Test Step 15 indicated as 14
            Action: Use the test script file 12_3_7_j.xml to send the following packets,EVC-1MMI_M_WARNING = 1MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1111EVC-7OBU_TR_M_MODE = 12
            Expected Result: DMI displays in LS mode, level 1.Verify the following information,(1)   The speed pointer display in grey colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, LS mode in TSM supervision);
            */
            XML_12_3_7_j.Send(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                 "1. DMI displays in LS mode, level 1." + Environment.NewLine +
                                 "2. Is the speed pointer grey?");

            /*
            Test Step 16 indicated as 15
            Action: Use the test script file 12_3_7_k.xml to send the following packets,EVC-1MMI_M_WARNING = 9MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1139EVC-7OBU_TR_M_MODE = 12
            Expected Result: DMI displays in LS mode, level 1.Verify the following information,(1)   The speed pointer display in orange colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, LS mode in TSM supervision);
            */
            XML_12_3_7_k.Send(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                 "1. DMI displays in LS mode, level 1." + Environment.NewLine +
                                 "2. Is the speed pointer orange?");

            /*
            Test Step 17 indicated as 16
            Action: Use the test script file 12_3_7_l.xml to send the following packets,EVC-1MMI_M_WARNING = 5MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1250EVC-7OBU_TR_M_MODE = 12
            Expected Result: DMI displays in LS mode, level 1.Verify the following information,(1)   The speed pointer display in orange colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, LS mode in TSM supervision);
            */
            XML_12_3_7_l.Send(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                 "1. DMI displays in LS mode, level 1." + Environment.NewLine +
                                 "2. Is the speed pointer orange?");

            /*
            Test Step 18 indicated as 17
            Action: Use the test script file 12_3_7_m.xml to send the following packets,EVC-1MMI_M_WARNING = 13MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1277EVC-7OBU_TR_M_MODE = 12
            Expected Result: DMI displays in LS mode, level 1.Verify the following information,(1)   The speed pointer display in red colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, LS mode in TSM supervision);
            */
            XML_12_3_7_m.Send(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                 "1. DMI displays in LS mode, level 1." + Environment.NewLine +
                                 "2. Is the speed pointer red?");

            /*
            Test Step 19 indicated as 18
            Action: Use the test script file 12_3_7_n.xml to send the following packets,EVC-1MMI_M_WARNING = 13MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1111EVC-7OBU_TR_M_MODE = 12
            Expected Result: DMI displays in LS mode, level 1.Verify the following information,(1)   The speed pointer display in grey colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, LS mode in TSM supervision);
            */
            XML_12_3_7_n.Send(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                 "1. DMI displays in LS mode, level 1." + Environment.NewLine +
                                 "2. Is the speed pointer grey?");

            /*
            Test Step 20 indicated as 19
            Action: Use the test script file 12_3_7_o.xml to send the following packets,EVC-1MMI_M_WARNING = 13MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1083EVC-7OBU_TR_M_MODE = 12
            Expected Result: DMI displays in LS mode, level 1.Verify the following information,(1)   The speed pointer display in grey colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, LS mode in TSM supervision);
            */
            XML_12_3_7_o.Send(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                 "1. DMI displays in LS mode, level 1." + Environment.NewLine +
                                 "2. Is the speed pointer grey?");

            /*
            Test Step 21 indicated as 20
            Action: Use the test script file 12_3_7_p.xml to send the following packets,EVC-1MMI_M_WARNING = 3MMI_V_PERMITTED = 1111MMI_V_RELEASE = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 0EVC-7OBU_TR_M_MODE = 12
            Expected Result: DMI displays in LS mode, level 1.Verify the following information,(1)   The speed pointer display in yellow colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, LS mode in RSM supervision);
            */
            XML_12_3_7_p.Send(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                 "1. DMI displays in LS mode, level 1." + Environment.NewLine +
                                 "2. Is the speed pointer yellow?");

            /*
            Test Step 22 indicated as 21
            Action: Use the test script file 12_3_7_q.xml to send the following packets,EVC-1MMI_M_WARNING = 3MMI_V_PERMITTED = 1111MMI_V_RELEASE = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1083EVC-7OBU_TR_M_MODE = 12
            Expected Result: DMI displays in LS mode, level 1.Verify the following information,(1)   The speed pointer display in yellow colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, LS mode in RSM supervision);
            */
            XML_12_3_7_q.Send(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                 "1. DMI displays in LS mode, level 1." + Environment.NewLine +
                                 "2. Is the speed pointer yellow?");

            /*
            Test Step 23 indicated as 22
            Action: Use the test script file 12_3_7_r.xml to send the following packets,EVC-1MMI_M_WARNING = 15MMI_V_PERMITTED = 1111MMI_V_RELEASE = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 0EVC-7OBU_TR_M_MODE = 12
            Expected Result: DMI displays in LS mode, level 1.Verify the following information,(1)   The speed pointer display in yellow colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, LS mode in RSM supervision);
            */
            XML_12_3_7_r.Send(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                 "1. DMI displays in LS mode, level 1." + Environment.NewLine +
                                 "2. Is the speed pointer yellow?");

            /*
            Test Step 24 indicated as 23
            Action: Use the test script file 12_3_7_s.xml to send the following packets,EVC-1MMI_M_WARNING = 15MMI_V_PERMITTED = 1111MMI_V_RELEASE = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1111EVC-7OBU_TR_M_MODE = 12
            Expected Result: DMI displays in LS mode, level 1.Verify the following information,(1)   The speed pointer display in red colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, LS mode in RSM supervision);
            */
            XML_12_3_7_s.Send(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                 "1. DMI displays in LS mode, level 1." + Environment.NewLine +
                                 "2. Is the speed pointer red?");

            /*
            Test Step 25
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
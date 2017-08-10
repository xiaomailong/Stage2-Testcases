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
    /// 17.3.3 Speed Pointer: Colour of speed pointer in UN mode
    /// TC-ID: 12.3.4
    /// 
    /// This test case verifies the colour of speed pointer which display refer to received packet EVC-1 and EVC-7 for UN mode.
    /// 
    /// Tested Requirements:
    /// MMI_gen 6299 (partly: UN mode);
    /// 
    /// Scenario:
    /// 1.Drive the train forward with specify speed. Then, verify the colour of speed pointer refer to received packet EVC-1 and EVC-7.
    /// 2.Use the test script file to send EVC-1 and EVC-7 with specify value. Then, verify the colour of speed pointer.Note: Tester need to execute script file repeatly due to the packet will be interrupted by dynamic packet EVC-1 and EVC-7 which send from ETCS onboard.
    /// 
    /// Used files:
    /// 12_3_4_a.xml, 12_3_4_b.xml, 12_3_4_c.xml, 12_3_4_d.xml, 12_3_4_e.xml, 12_3_4_f.xml, 12_3_4_g.xml, 12_3_4_h.xml, 12_3_4_i.xml, 12_3_4_j.xml, 12_3_4_k.xml, 12_3_4_l.xml, 12_3_4_m.xml, 12_3_4_n.xml
    /// </summary>
    public class Speed_Pointer_Colour_of_speed_pointer_in_UN_mode : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // Test system is powered on.Cabin is activated.SoM is performed in UN mode, Level 0.

            EVC0_MMIStartATP.Evc0Type = EVC0_MMIStartATP.EVC0Type.GoToIdle;
            EVC0_MMIStartATP.Send();

            // Set train running number, cab 1 active, and other defaults
            DmiActions.Activate_Cabin_1(this);

            // Set driver ID
            DmiActions.Set_Driver_ID(this, "1234");

            // Set to level 1 and SR mode
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Level = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_LEVEL.L0;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.Unfitted;

            // Enable standard buttons including Start, and display Default window.
            DmiActions.FinishedSoM_Default_Window(this);

            // Call the TestCaseBase PreExecution
            base.PreExecution();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in UN mode, level 0

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint
            
            /*
            Test Step 1
            Action: Drive the train forward with speed = 100 km/h
            Expected Result: DMI displays in UN mode, level 0.Verify the following information,(1)   Use the log file to confirm that DMI received the packet information EVC-1 and EVC-7 with following variables,(EVC-7) OBU_TR_M_MODE = 4 (Unfitted)(EVC-1) MMI_M_WARNING = 0 (Status = NoS, Supervision = CSM)(EVC-1) MMI_V_PERMITTED = 2778 (100km/h)(2)   The speed pointer display in grey colour
            Test Step Comment: (1) MMI_gen 6299 (partly: OBU_TR_M_MODE, MMI_M_WARNING, train speed in relation to permitted speed MMI_V_PERMITTED, UN mode in CSM supervision);(2) MMI_gen 6299 (partly: colour of speed pointer, UN mode in CSM supervision);
            */
            EVC1_MMIDynamic.MMI_V_PERMITTED = 2778;
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 100;
            EVC1_MMIDynamic.MMI_V_INTERVENTION_KMH = 105;      
            
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in UN mode, level 0." + Environment.NewLine +
                                "2. Is the speed pointer grey?");

            /*
            Test Step 2
            Action: Increase the train speed to 101 km/h
            Expected Result: Verify the following information,(1)   Use the log file to confirm that DMI received the packet information EVC-1 with the following condition,MMI_M_WARNING = 8 (Status = OvS, Supervision = CSM) while the value of MMI_V_TRAIN = 2806 (101 km/h) which greater than MMI_V_PERMITTED(2)   The speed pointer display in orange colour
            Test Step Comment: (1) MMI_gen 6299 (partly: MMI_M_WARNING, train speed in relation to permitted speed MMI_V_PERMITTED, UN mode in CSM supervision);(2) MMI_gen 6299 (partly: colour of speed pointer, UN mode in CSM supervision);
            */

            // Call generic Action Method
            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Overspeed_Status_Ceiling_Speed_Monitoring;
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 101;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Is the speed pointer orange?");

            /*
            Test Step 3
            Action: Increase the train speed to 105 km/h.Note: dV_warning_max is defined in chapter 3 of [SUBSET-026]
            Expected Result: Verify the following information,(1)   Use the log file to confirm that DMI received the packet information EVC-1 with the following condition,MMI_M_WARNING = 4 (Status = WaS, Supervision = CSM) while the value of MMI_V_TRAIN = 2917 (105 km/h) which greater than MMI_V_PERMITTED but lower than MMI_V_INTERVENTION(2)   The speed pointer display in orange colour
            Test Step Comment: (1) MMI_gen 6299 (partly: MMI_M_WARNING, train speed in relation to permitted speed MMI_V_PERMITTED, UN mode in CSM supervision);(2) MMI_gen 6299 (partly: colour of speed pointer, UN mode in CSM supervision);
            */
            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Warning_Status_Ceiling_Speed_Monitoring;
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 105;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Is the speed pointer orange?");

            /*
            Test Step 4
            Action: Increase the train speed to 106 km/h
            Expected Result: The train speed is force to decrease because of emergency brake is applied by ETCS onboard.Verify the following information,Before train speed is decreased(1)   Use the log file to confirm that DMI received the packet information EVC-1 with the following condition,MMI_M_WARNING = 12 (Status = IntS, Supervision = CSM) while the value of MMI_V_TRAIN = 2944 (106 km/h) which greater than MMI_V_INTERVENTION(2)   The speed pointer display in red colourAfter train speed is decreased(3)   Use the log file to confirm that DMI received the packet information EVC-1 with the following condition,MMI_M_WARNING = 12 (Status = IntS, Supervision = CSM) while the value of MMI_V_TRAIN is lower than MMI_V_INTERVENTION(4)   The speed pointer display in grey colour
            Test Step Comment: (1) MMI_gen 6299 (partly: MMI_M_WARNING, train speed in relation to permitted speed MMI_V_PERMITTED, UN mode in CSM supervision);(2) MMI_gen 6299 (partly: colour of speed pointer, UN mode in CSM supervision);(3) MMI_gen 6299 (partly: MMI_M_WARNING, UN mode in CSM supervision);(4) MMI_gen 6299 (partly: colour of speed pointer, UN mode in CSM supervision);
            */
            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Intervention_Status_Ceiling_Speed_Monitoring;
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 106;
            DmiActions.Apply_Brakes(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Is the speed pointer red?");

            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 100;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Is the speed pointer grey?");
            /*
            Test Step 5
            Action: Stop the train.Then, use the test script file 12_3_4_a.xml to send the following packets,EVC-1MMI_M_WARNING = 2MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 972EVC-7OBU_TR_M_MODE = 4
            Expected Result: DMI displays in UN mode, level 0.Verify the following information,(1)   The speed pointer display in grey colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, UN mode in PIM supervision);
            */
            XML_12_3_4_a.Send(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in UN mode, level 0." + Environment.NewLine +
                                "2. Is the speed pointer grey?");
            /*
            Test Step 6
            Action: Use the test script file 12_3_4_b.xml to send the following packets,EVC-1MMI_M_WARNING = 2MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1111EVC-7OBU_TR_M_MODE = 4
            Expected Result: DMI displays in UN mode, level 0.Verify the following information,(1)   The speed pointer display in white colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, UN mode in PIM supervision);
            */
            XML_12_3_4_b.Send(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in UN mode, level 0." + Environment.NewLine +
                                "2. Is the speed pointer white?");

            /*
            Test Step 7
            Action: Use the test script file 12_3_4_c.xml to send the following packets,EVC-1MMI_M_WARNING = 10MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1139EVC-7OBU_TR_M_MODE = 4
            Expected Result: DMI displays in UN mode, level 0.Verify the following information,(1)   The speed pointer display in orange colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, UN mode in PIM supervision);
            */
            XML_12_3_4_c.Send(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in UN mode, level 0." + Environment.NewLine +
                                "2. Is the speed pointer orange?");

            /*
            Test Step 8
            Action: Use the test script file 12_3_4_d.xml to send the following packets,EVC-1MMI_M_WARNING = 6MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1250EVC-7OBU_TR_M_MODE = 4
            Expected Result: DMI displays in UN mode, level 0.Verify the following information,(1)   The speed pointer display in orange colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, UN mode in PIM supervision);
            */
            XML_12_3_4_d.Send(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in UN mode, level 0." + Environment.NewLine +
                                "2. Is the speed pointer orange?");

            /*
            Test Step 9
            Action: Use the test script file 12_3_4_e.xml to send the following packets,EVC-1MMI_M_WARNING = 14MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1277EVC-7OBU_TR_M_MODE = 4
            Expected Result: DMI displays in UN mode, level 0.Verify the following information,(1)   The speed pointer display in red colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, UN mode in PIM supervision);
            */
            XML_12_3_4_e.Send(this);
            DmiActions.Apply_Brakes(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in UN mode, level 0." + Environment.NewLine +
                                "2. Is the speed pointer red?");

            /*
            Test Step 10
            Action: Use the test script file 12_3_4_f.xml to send the following packets,EVC-1MMI_M_WARNING = 14MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1111EVC-7OBU_TR_M_MODE = 4
            Expected Result: DMI displays in UN mode, level 0.Verify the following information,(1)   The speed pointer display in white colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, UN mode in PIM supervision);
            */
            XML_12_3_4_f.Send(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in UN mode, level 0." + Environment.NewLine +
                                "2. Is the speed pointer white?");

            /*
            Test Step 11
            Action: Use the test script file 12_3_4_g.xml to send the following packets,EVC-1MMI_M_WARNING = 14MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1000EVC-7OBU_TR_M_MODE = 4
            Expected Result: DMI displays in UN mode, level 0.Verify the following information,(1)   The speed pointer display in grey colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, UN mode in PIM supervision);
            */
            XML_12_3_4_g.Send(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in UN mode, level 0." + Environment.NewLine +
                                "2. Is the speed pointer grey?");

            /*
            Test Step 12
            Action: Use the test script file 12_3_4_h.xml to send the following packets,EVC-1MMI_M_WARNING = 11MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1000EVC-7OBU_TR_M_MODE = 4
            Expected Result: DMI displays in UN mode, level 0.Verify the following information,(1)   The speed pointer display in grey colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, UN mode in TSM supervision);
            */
            XML_12_3_4_h.Send(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in UN mode, level 0." + Environment.NewLine +
                                "2. Is the speed pointer grey?");
            /*
            Test Step 13
            Action: Use the test script file 12_3_4_i.xml to send the following packets,EVC-1MMI_M_WARNING = 11MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1111EVC-7OBU_TR_M_MODE = 4
            Expected Result: DMI displays in UN mode, level 0.Verify the following information,(1)   The speed pointer display in white colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, UN mode in TSM supervision);
            */
            XML_12_3_4_i.Send(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in UN mode, level 0." + Environment.NewLine +
                                "2. Is the speed pointer white?");

            /*
            Test Step 14
            Action: Use the test script file 12_3_4_j.xml to send the following packets,EVC-1MMI_M_WARNING = 1MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1111EVC-7OBU_TR_M_MODE = 4
            Expected Result: DMI displays in UN mode, level 0.Verify the following information,(1)   The speed pointer display in yellow colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, UN mode in TSM supervision);
            */
            XML_12_3_4_j.Send(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in UN mode, level 0." + Environment.NewLine +
                                "2. Is the speed pointer yellow?");

            /*
            Test Step 15
            Action: Use the test script file 12_3_4_k.xml to send the following packets,EVC-1MMI_M_WARNING = 9MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1139EVC-7OBU_TR_M_MODE = 4
            Expected Result: DMI displays in UN mode, level 0.Verify the following information,(1)   The speed pointer display in orange colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, UN mode in TSM supervision);
            */
            XML_12_3_4_k.Send(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in UN mode, level 0." + Environment.NewLine +
                                "2. Is the speed pointer orange?");

            /*
            Test Step 16
            Action: Use the test script file 12_3_4_l.xml to send the following packets,EVC-1MMI_M_WARNING = 5MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1250EVC-7OBU_TR_M_MODE = 4
            Expected Result: DMI displays in UN mode, level 0.Verify the following information,(1)   The speed pointer display in orange colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, UN mode in TSM supervision);
            */
            XML_12_3_4_l.Send(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in UN mode, level 0." + Environment.NewLine +
                                "2. Is the speed pointer orange?");

            /*
            Test Step 17
            Action: Use the test script file 12_3_4_m.xml to send the following packets,EVC-1MMI_M_WARNING = 13MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1277EVC-7OBU_TR_M_MODE = 4
            Expected Result: DMI displays in UN mode, level 0.Verify the following information,(1)   The speed pointer display in red colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, UN mode in TSM supervision);
            */
            XML_12_3_4_m.Send(this);
            DmiActions.Apply_Brakes(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in UN mode, level 0." + Environment.NewLine +
                                "2. Is the speed pointer red?");

            /*
            Test Step 18
            Action: Use the test script file 12_3_4_n.xml to send the following packets,EVC-1MMI_M_WARNING = 13MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1111EVC-7OBU_TR_M_MODE = 4
            Expected Result: DMI displays in UN mode, level 0.Verify the following information,(1)   The speed pointer display in yellow colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, UN mode in TSM supervision);
            */
            XML_12_3_4_n.Send(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in UN mode, level 0." + Environment.NewLine +
                                "2. Is the speed pointer yellow?");

            /*
            Test Step 19
            Action: Use the test script file 12_3_4_o.xml to send the following packets,EVC-1MMI_M_WARNING = 13MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1083EVC-7OBU_TR_M_MODE = 4
            Expected Result: DMI displays in UN mode, level 0.Verify the following information,(1)   The speed pointer display in grey colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, UN mode in TSM supervision);
            */
            XML_12_3_4_o.Send(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in UN mode, level 0." + Environment.NewLine +
                                "2. Is the speed pointer grey?");

            /*
            Test Step 20
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
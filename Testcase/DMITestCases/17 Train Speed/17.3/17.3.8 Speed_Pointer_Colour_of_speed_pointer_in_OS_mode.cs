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
    public class Speed_Pointer_Colour_of_speed_pointer_in_OS_mode : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // Test system is powered on.Cabin is activated.SoM is performed in SR mode, Level 1.

            // Call the TestCaseBase PreExecution
            base.PreExecution();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in OS mode, level 1

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint


            /*
            Test Step 1
            Action: Drive the train forward pass BG1. Then, press an acknowledgement of OS mode in sub-area C1.
            Expected Result: DMI displays in OS mode, level 1.
            */

            /*
            Test Step 2
            Action: Drive the train forward with speed = 30 km/h
            Expected Result: DMI displays in OS mode, level 1.Verify the following information,(1)   Use the log file to confirm that DMI received the packet information EVC-1 and EVC-7 with following variables,(EVC-7) OBU_TR_M_MODE = 1 (On-sight)(EVC-1) MMI_M_WARNING = 0 (Status = NoS, Supervision = CSM)(EVC-1) MMI_V_PERMITTED = 833 (30km/h)(2)   The speed pointer display in grey colour
            Test Step Comment: (1) MMI_gen 6299 (partly: OBU_TR_M_MODE, MMI_M_WARNING, train speed in relation to permitted speed MMI_V_PERMITTED, OS mode in CSM supervision);(2) MMI_gen 6299 (partly: colour of speed pointer, OS mode in CSM supervision);
            */

            /*
            Test Step 3
            Action: Increase the train speed to 31 km/h
            Expected Result: Verify the following information,(1)   Use the log file to confirm that DMI received the packet information EVC-1 with the following condition,MMI_M_WARNING = 8 (Status = OvS, Supervision = CSM) while the value of MMI_V_TRAIN = 861 (31 km/h) which greater than MMI_V_PERMITTED(2)   The speed pointer display in orange colour
            Test Step Comment: (1) MMI_gen 6299 (partly: MMI_M_WARNING, train speed in relation to permitted speed MMI_V_PERMITTED, OS mode in CSM supervision);(2) MMI_gen 6299 (partly: colour of speed pointer, OS mode in CSM supervision);
            */

            /*
            Test Step 4
            Action: Increase the train speed to 35 km/h.Note: dV_warning_max is defined in chapter 3 of [SUBSET-026]
            Expected Result: Verify the following information,(1)   Use the log file to confirm that DMI received the packet information EVC-1 with the following condition,MMI_M_WARNING = 4 (Status = WaS, Supervision = CSM) while the value of MMI_V_TRAIN = 972 (35 km/h) which greater than MMI_V_PERMITTED but lower than MMI_V_INTERVENTION(2)   The speed pointer display in orange colour
            Test Step Comment: (1) MMI_gen 6299 (partly: MMI_M_WARNING, train speed in relation to permitted speed MMI_V_PERMITTED, OS mode in CSM supervision);(2) MMI_gen 6299 (partly: colour of speed pointer, OS mode in CSM supervision);
            */

            /*
            Test Step 5
            Action: Increase the train speed to 36 km/h.
            Expected Result: The train speed is force to decrease because of emergency brake is applied by ETCS onboard.Verify the following information,Before train speed is decreased(1)   Use the log file to confirm that DMI received the packet information EVC-1 with the following condition,MMI_M_WARNING = 12 (Status = IntS, Supervision = CSM) while the value of MMI_V_TRAIN = 1000 (36 km/h) which greater than MMI_V_INTERVENTION(2)   The speed pointer display in red colourAfter train speed is decreased(3)   Use the log file to confirm that DMI received the packet information EVC-1 with the following condition,MMI_M_WARNING = 12 (Status = IntS, Supervision = CSM) while the value of MMI_V_TRAIN is lower than MMI_V_INTERVENTION(4)   The speed pointer display in grey colour
            Test Step Comment: (1) MMI_gen 6299 (partly: MMI_M_WARNING, train speed in relation to permitted speed MMI_V_PERMITTED, OS mode in CSM supervision);(2) MMI_gen 6299 (partly: colour of speed pointer, OS mode in CSM supervision);(3) MMI_gen 6299 (partly: MMI_M_WARNING, OS mode in CSM supervision);(4) MMI_gen 6299 (partly: colour of speed pointer, OS mode in CSM supervision);
            */

            /*
            Test Step 6
            Action: Stop the train.Then, use the test script file 12_3_8_a.xml to send the following packets,EVC-1MMI_M_WARNING = 2MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 972EVC-7OBU_TR_M_MODE = 1
            Expected Result: DMI displays in OS mode, level 1.Verify the following information,(1)   The speed pointer display in grey colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, OS mode in PIM supervision);
            */

            /*
            Test Step 7
            Action: Use the test script file 12_3_8_b.xml to send the following packets,EVC-1MMI_M_WARNING = 2MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1111EVC-7OBU_TR_M_MODE = 1
            Expected Result: DMI displays in OS mode, level 1.Verify the following information,(1)   The speed pointer display in white colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, OS mode in PIM supervision);
            */

            /*
            Test Step 8
            Action: Use the test script file 12_3_8_c.xml to send the following packets,EVC-1MMI_M_WARNING = 10MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1139EVC-7OBU_TR_M_MODE = 1
            Expected Result: DMI displays in OS mode, level 1.Verify the following information,(1)   The speed pointer display in orange colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, OS mode in PIM supervision);
            */

            /*
            Test Step 9
            Action: Use the test script file 12_3_8_d.xml to send the following packets,EVC-1MMI_M_WARNING = 6MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1250EVC-7OBU_TR_M_MODE = 1
            Expected Result: DMI displays in OS mode, level 1.Verify the following information,(1)   The speed pointer display in orange colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, OS mode in PIM supervision);
            */

            /*
            Test Step 10
            Action: Use the test script file 12_3_8_e.xml to send the following packets,EVC-1MMI_M_WARNING = 14MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1277EVC-7OBU_TR_M_MODE = 1
            Expected Result: DMI displays in OS mode, level 1.Verify the following information,(1)   The speed pointer display in red colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, OS mode in PIM supervision);
            */

            /*
            Test Step 11
            Action: Use the test script file 12_3_8_f.xml to send the following packets,EVC-1MMI_M_WARNING = 14MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1111EVC-7OBU_TR_M_MODE = 1
            Expected Result: DMI displays in OS mode, level 1.Verify the following information,(1)   The speed pointer display in white colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, OS mode in PIM supervision);
            */

            /*
            Test Step 12
            Action: Use the test script file 12_3_8_g.xml to send the following packets,EVC-1MMI_M_WARNING = 14MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1000EVC-7OBU_TR_M_MODE = 1
            Expected Result: DMI displays in OS mode, level 1.Verify the following information,(1)   The speed pointer display in grey colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, OS mode in PIM supervision);
            */

            /*
            Test Step 13
            Action: Use the test script file 12_3_8_h.xml to send the following packets,EVC-1MMI_M_WARNING = 11MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1000EVC-7OBU_TR_M_MODE = 1
            Expected Result: DMI displays in OS mode, level 1.Verify the following information,(1)   The speed pointer display in grey colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, OS mode in TSM supervision);
            */

            /*
            Test Step 14
            Action: Use the test script file 12_3_8_i.xml to send the following packets,EVC-1MMI_M_WARNING = 11MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1111EVC-7OBU_TR_M_MODE = 1
            Expected Result: DMI displays in OS mode, level 1.Verify the following information,(1)   The speed pointer display in white colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, OS mode in TSM supervision);
            */

            /*
            Test Step 15
            Action: Use the test script file 12_3_8_j.xml to send the following packets,EVC-1MMI_M_WARNING = 1MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1111EVC-7OBU_TR_M_MODE = 1
            Expected Result: DMI displays in OS mode, level 1.Verify the following information,(1)   The speed pointer display in yellow colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, OS mode in TSM supervision);
            */

            /*
            Test Step 16
            Action: Use the test script file 12_3_8_k.xml to send the following packets,EVC-1MMI_M_WARNING = 1MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1083EVC-7OBU_TR_M_MODE = 1
            Expected Result: DMI displays in OS mode, level 1.Verify the following information,(1)   The speed pointer display in Grey colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, OS mode in TSM supervision);
            */

            /*
            Test Step 17
            Action: Use the test script file 12_3_8_l.xml to send the following packets,EVC-1MMI_M_WARNING = 9MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1139EVC-7OBU_TR_M_MODE = 1
            Expected Result: DMI displays in OS mode, level 1.Verify the following information,(1)   The speed pointer display in orange colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, OS mode in TSM supervision);
            */

            /*
            Test Step 18
            Action: Use the test script file 12_3_8_m.xml to send the following packets,EVC-1MMI_M_WARNING = 5MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1250EVC-7OBU_TR_M_MODE = 1
            Expected Result: DMI displays in OS mode, level 1.Verify the following information,(1)   The speed pointer display in orange colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, OS mode in TSM supervision);
            */

            /*
            Test Step 19
            Action: Use the test script file 12_3_8_n.xml to send the following packets,EVC-1MMI_M_WARNING = 13MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1277EVC-7OBU_TR_M_MODE = 1
            Expected Result: DMI displays in OS mode, level 1.Verify the following information,(1)   The speed pointer display in red colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, OS mode in TSM supervision);
            */

            /*
            Test Step 20
            Action: Use the test script file 12_3_8_o.xml to send the following packets,EVC-1MMI_M_WARNING = 13MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1111EVC-7OBU_TR_M_MODE = 1
            Expected Result: DMI displays in OS mode, level 1.Verify the following information,(1)   The speed pointer display in yellow colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, OS mode in TSM supervision);
            */

            /*
            Test Step 21
            Action: Use the test script file 12_3_8_p.xml to send the following packets,EVC-1MMI_M_WARNING = 13MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1083EVC-7OBU_TR_M_MODE = 1
            Expected Result: DMI displays in OS mode, level 1.Verify the following information,(1)   The speed pointer display in grey colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, OS mode in TSM supervision);
            */

            /*
            Test Step 22
            Action: Use the test script file 12_3_8_q.xml to send the following packets,EVC-1MMI_M_WARNING = 3MMI_V_PERMITTED = 1111MMI_V_RELEASE = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 0EVC-7OBU_TR_M_MODE = 1
            Expected Result: DMI displays in OS mode, level 1.Verify the following information,(1)   The speed pointer display in yellow colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, OS mode in RSM supervision);
            */

            /*
            Test Step 23
            Action: Use the test script file 12_3_8_r.xml to send the following packets,EVC-1MMI_M_WARNING = 15MMI_V_PERMITTED = 1111MMI_V_RELEASE = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 0EVC-7OBU_TR_M_MODE = 1
            Expected Result: DMI displays in OS mode, level 1.Verify the following information,(1)   The speed pointer display in yellow colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, OS mode in RSM supervision);
            */

            /*
            Test Step 24
            Action: Use the test script file 12_3_8_s.xml to send the following packets,EVC-1MMI_M_WARNING = 15MMI_V_PERMITTED = 1111MMI_V_ RELEASE = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1111EVC-7OBU_TR_M_MODE = 1
            Expected Result: DMI displays in OS mode, level 1.Verify the following information,(1)   The speed pointer display in red colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, OS mode in RSM supervision);
            */

            /*
            Test Step 25
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
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
    public class Speed_Pointer_Colour_of_speed_pointer_in_SR_mode : TestcaseBase
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
            // DMI displays in SR mode, level 1

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


            /*
            Test Step 2
            Action: Increase the train speed to 41 km/h
            Expected Result: Verify the following information,(1)   Use the log file to confirm that DMI received the packet information EVC-1 with the following condition,MMI_M_WARNING = 8 (Status = OvS, Supervision = CSM) while the value of MMI_V_TRAIN = 1139 (41 km/h) which greater than MMI_V_PERMITTED(2)   The speed pointer display in orange colour
            Test Step Comment: (1) MMI_gen 6299 (partly: MMI_M_WARNING, train speed in relation to permitted speed MMI_V_PERMITTED, SR mode in CSM supervision);(2) MMI_gen 6299 (partly: colour of speed pointer, SR mode in CSM supervision);
            */

            /*
            Test Step 3
            Action: Increase the train speed to 45 km/h.Note: dV_warning_max is defined in chapter 3 of [SUBSET-026]
            Expected Result: Verify the following information,(1)   Use the log file to confirm that DMI received the packet information EVC-1 with the following condition,MMI_M_WARNING = 4 (Status = WaS, Supervision = CSM) while the value of MMI_V_TRAIN = 1250 (45 km/h) which greater than MMI_V_PERMITTED but lower than MMI_V_INTERVENTION(2)   The speed pointer display in orange colour
            Test Step Comment: (1) MMI_gen 6299 (partly: MMI_M_WARNING, train speed in relation to permitted speed MMI_V_PERMITTED, SR mode in CSM supervision);(2) MMI_gen 6299 (partly: colour of speed pointer, SR mode in CSM supervision);
            */

            /*
            Test Step 4
            Action: Increase the train speed to 46 km/h
            Expected Result: The train speed is force to decrease because of emergency brake is applied by ETCS onboard.Verify the following information,Before train speed is decreased(1)   Use the log file to confirm that DMI received the packet information EVC-1 with the following condition,MMI_M_WARNING = 12 (Status = IntS, Supervision = CSM) while the value of MMI_V_TRAIN = 1278 (46 km/h) which greater than MMI_V_INTERVENTION(2)   The speed pointer display in red colourAfter train speed is decreased(3)   Use the log file to confirm that DMI received the packet information EVC-1 with the following condition,MMI_M_WARNING = 12 (Status = IntS, Supervision = CSM) while the value of MMI_V_TRAIN is lower than MMI_V_INTERVENTION(4)   The speed pointer display in grey colour
            Test Step Comment: (1) MMI_gen 6299 (partly: MMI_M_WARNING, train speed in relation to permitted speed MMI_V_PERMITTED, SR mode in CSM supervision);(2) MMI_gen 6299 (partly: colour of speed pointer, SR mode in CSM supervision);(3) MMI_gen 6299 (partly: MMI_M_WARNING, SR mode in CSM supervision);(4) MMI_gen 6299 (partly: colour of speed pointer, SR mode in CSM supervision);
            */
            // Call generic Action Method
            DmiActions.Increase_the_train_speed_to_46_kmh(this);
            // Call generic Check Results Method
            DmiExpectedResults
                .The_train_speed_is_force_to_decrease_because_of_emergency_brake_is_applied_by_ETCS_onboard_Verify_the_following_information_Before_train_speed_is_decreased1_Use_the_log_file_to_confirm_that_DMI_received_the_packet_information_EVC_1_with_the_following_condition_MMI_M_WARNING_12_Status_IntS_Supervision_CSM_while_the_value_of_MMI_V_TRAIN_1278_46_kmh_which_greater_than_MMI_V_INTERVENTION2_The_speed_pointer_display_in_red_colourAfter_train_speed_is_decreased3_Use_the_log_file_to_confirm_that_DMI_received_the_packet_information_EVC_1_with_the_following_condition_MMI_M_WARNING_12_Status_IntS_Supervision_CSM_while_the_value_of_MMI_V_TRAIN_is_lower_than_MMI_V_INTERVENTION4_The_speed_pointer_display_in_grey_colour(this);


            /*
            Test Step 5
            Action: Stop the train.Then, use the test script file 12_3_3_a.xml to send the following packets,EVC-1MMI_M_WARNING = 2MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 972EVC-7OBU_TR_M_MODE = 2
            Expected Result: DMI displays in SR mode, level 1.Verify the following information,(1)   The speed pointer display in grey colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, SR mode in PIM supervision);
            */
            // Call generic Check Results Method
            DmiExpectedResults
                .DMI_displays_in_SR_mode_level_1_Verify_the_following_information_1_The_speed_pointer_display_in_grey_colour(this);


            /*
            Test Step 6
            Action: Use the test script file 12_3_3_b.xml to send the following packets,EVC-1MMI_M_WARNING = 2MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1111EVC-7OBU_TR_M_MODE = 2
            Expected Result: DMI displays in SR mode, level 1.Verify the following information,(1)   The speed pointer display in white colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, SR mode in PIM supervision);
            */
            // Call generic Check Results Method
            DmiExpectedResults
                .DMI_displays_in_SR_mode_level_1_Verify_the_following_information_1_The_speed_pointer_display_in_white_colour(this);


            /*
            Test Step 7
            Action: Use the test script file 12_3_3_c.xml to send the following packets,EVC-1MMI_M_WARNING = 10MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1139EVC-7OBU_TR_M_MODE = 2
            Expected Result: DMI displays in SR mode, level 1.Verify the following information,(1)   The speed pointer display in orange colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, SR mode in PIM supervision);
            */
            // Call generic Check Results Method
            DmiExpectedResults
                .DMI_displays_in_SR_mode_level_1_Verify_the_following_information_1_The_speed_pointer_display_in_orange_colour(this);


            /*
            Test Step 8
            Action: Use the test script file 12_3_3_d.xml to send the following packets,EVC-1MMI_M_WARNING = 6MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1250EVC-7OBU_TR_M_MODE = 2
            Expected Result: DMI displays in SR mode, level 1.Verify the following information,(1)   The speed pointer display in orange colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, SR mode in PIM supervision);
            */
            // Call generic Check Results Method
            DmiExpectedResults
                .DMI_displays_in_SR_mode_level_1_Verify_the_following_information_1_The_speed_pointer_display_in_orange_colour(this);


            /*
            Test Step 9
            Action: Use the test script file 12_3_3_e.xml to send the following packets,EVC-1MMI_M_WARNING = 14MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1277EVC-7OBU_TR_M_MODE = 2
            Expected Result: DMI displays in SR mode, level 1.Verify the following information,(1)   The speed pointer display in red colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, SR mode in PIM supervision);
            */
            // Call generic Check Results Method
            DmiExpectedResults
                .DMI_displays_in_SR_mode_level_1_Verify_the_following_information_1_The_speed_pointer_display_in_red_colour(this);


            /*
            Test Step 10
            Action: Use the test script file 12_3_3_f.xml to send the following packets,EVC-1MMI_M_WARNING = 14MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1111EVC-7OBU_TR_M_MODE = 2
            Expected Result: DMI displays in SR mode, level 1.Verify the following information,(1)   The speed pointer display in white colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, SR mode in PIM supervision);
            */
            // Call generic Check Results Method
            DmiExpectedResults
                .DMI_displays_in_SR_mode_level_1_Verify_the_following_information_1_The_speed_pointer_display_in_white_colour(this);


            /*
            Test Step 11
            Action: Use the test script file 12_3_3_g.xml to send the following packets,EVC-1MMI_M_WARNING = 14MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1000EVC-7OBU_TR_M_MODE = 2
            Expected Result: DMI displays in SR mode, level 1.Verify the following information,(1)   The speed pointer display in grey colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, SR mode in PIM supervision);
            */
            // Call generic Check Results Method
            DmiExpectedResults
                .DMI_displays_in_SR_mode_level_1_Verify_the_following_information_1_The_speed_pointer_display_in_grey_colour(this);


            /*
            Test Step 12
            Action: Use the test script file 12_3_3_h.xml to send the following packets,EVC-1MMI_M_WARNING = 11MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1000EVC-7OBU_TR_M_MODE = 2
            Expected Result: DMI displays in SR mode, level 1.Verify the following information,(1)   The speed pointer display in grey colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, SR mode in TSM supervision);
            */
            // Call generic Check Results Method
            DmiExpectedResults
                .DMI_displays_in_SR_mode_level_1_Verify_the_following_information_1_The_speed_pointer_display_in_grey_colour(this);


            /*
            Test Step 13
            Action: Use the test script file 12_3_3_i.xml to send the following packets,EVC-1MMI_M_WARNING = 11MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1111EVC-7OBU_TR_M_MODE = 2
            Expected Result: DMI displays in SR mode, level 1.Verify the following information,(1)   The speed pointer display in white colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, SR mode in TSM supervision);
            */
            // Call generic Check Results Method
            DmiExpectedResults
                .DMI_displays_in_SR_mode_level_1_Verify_the_following_information_1_The_speed_pointer_display_in_white_colour(this);


            /*
            Test Step 14
            Action: Use the test script file 12_3_3_j.xml to send the following packets,EVC-1MMI_M_WARNING = 1MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1111EVC-7OBU_TR_M_MODE = 2
            Expected Result: DMI displays in SR mode, level 1.Verify the following information,(1)   The speed pointer display in yellow colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, SR mode in TSM supervision);
            */
            // Call generic Check Results Method
            DmiExpectedResults
                .DMI_displays_in_SR_mode_level_1_Verify_the_following_information_1_The_speed_pointer_display_in_yellow_colour(this);


            /*
            Test Step 15
            Action: Use the test script file 12_3_3_k.xml to send the following packets,EVC-1MMI_M_WARNING = 9MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1139EVC-7OBU_TR_M_MODE = 2
            Expected Result: DMI displays in SR mode, level 1.Verify the following information,(1)   The speed pointer display in orange colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, SR mode in TSM supervision);
            */
            // Call generic Check Results Method
            DmiExpectedResults
                .DMI_displays_in_SR_mode_level_1_Verify_the_following_information_1_The_speed_pointer_display_in_orange_colour(this);


            /*
            Test Step 16
            Action: Use the test script file 12_3_3_l.xml to send the following packets,EVC-1MMI_M_WARNING = 5MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1250EVC-7OBU_TR_M_MODE = 2
            Expected Result: DMI displays in SR mode, level 1.Verify the following information,(1)   The speed pointer display in orange colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, SR mode in TSM supervision);
            */
            // Call generic Check Results Method
            DmiExpectedResults
                .DMI_displays_in_SR_mode_level_1_Verify_the_following_information_1_The_speed_pointer_display_in_orange_colour(this);


            /*
            Test Step 17
            Action: Use the test script file 12_3_3_m.xml to send the following packets,EVC-1MMI_M_WARNING = 13MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1277EVC-7OBU_TR_M_MODE = 2
            Expected Result: DMI displays in SR mode, level 1.Verify the following information,(1)   The speed pointer display in red colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, SR mode in TSM supervision);
            */
            // Call generic Check Results Method
            DmiExpectedResults
                .DMI_displays_in_SR_mode_level_1_Verify_the_following_information_1_The_speed_pointer_display_in_red_colour(this);


            /*
            Test Step 18
            Action: Use the test script file 12_3_3_n.xml to send the following packets,EVC-1MMI_M_WARNING = 13MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1111EVC-7OBU_TR_M_MODE = 2
            Expected Result: DMI displays in SR mode, level 1.Verify the following information,(1)   The speed pointer display in yellow colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, SR mode in TSM supervision);
            */
            // Call generic Check Results Method
            DmiExpectedResults
                .DMI_displays_in_SR_mode_level_1_Verify_the_following_information_1_The_speed_pointer_display_in_yellow_colour(this);


            /*
            Test Step 19
            Action: Use the test script file 12_3_3_o.xml to send the following packets,EVC-1MMI_M_WARNING = 13MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1083EVC-7OBU_TR_M_MODE = 2
            Expected Result: DMI displays in SR mode, level 1.Verify the following information,(1)   The speed pointer display in grey colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, SR mode in TSM supervision);
            */
            // Call generic Check Results Method
            DmiExpectedResults
                .DMI_displays_in_SR_mode_level_1_Verify_the_following_information_1_The_speed_pointer_display_in_grey_colour(this);


            /*
            Test Step 20
            Action: End of test
            Expected Result: 
            */


            return GlobalTestResult;
        }
    }
}
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
    /// 17.3.4 Speed Pointer: Colour of speed pointer in UN mode
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
            // Call generic Action Method
            DmiActions.Drive_the_train_forward_with_speed_100_kmh(this);


            /*
            Test Step 2
            Action: Increase the train speed to 101 km/h
            Expected Result: Verify the following information,(1)   Use the log file to confirm that DMI received the packet information EVC-1 with the following condition,MMI_M_WARNING = 8 (Status = OvS, Supervision = CSM) while the value of MMI_V_TRAIN = 2806 (101 km/h) which greater than MMI_V_PERMITTED(2)   The speed pointer display in orange colour
            Test Step Comment: (1) MMI_gen 6299 (partly: MMI_M_WARNING, train speed in relation to permitted speed MMI_V_PERMITTED, UN mode in CSM supervision);(2) MMI_gen 6299 (partly: colour of speed pointer, UN mode in CSM supervision);
            */
            // Call generic Action Method
            DmiActions.Increase_the_train_speed_to_101_kmh(this);
            // Call generic Check Results Method
            DmiExpectedResults
                .Verify_the_following_information_1_Use_the_log_file_to_confirm_that_DMI_received_the_packet_information_EVC_1_with_the_following_condition_MMI_M_WARNING_8_Status_OvS_Supervision_CSM_while_the_value_of_MMI_V_TRAIN_2806_101_kmh_which_greater_than_MMI_V_PERMITTED2_The_speed_pointer_display_in_orange_colour(this);


            /*
            Test Step 3
            Action: Increase the train speed to 105 km/h.Note: dV_warning_max is defined in chapter 3 of [SUBSET-026]
            Expected Result: Verify the following information,(1)   Use the log file to confirm that DMI received the packet information EVC-1 with the following condition,MMI_M_WARNING = 4 (Status = WaS, Supervision = CSM) while the value of MMI_V_TRAIN = 2917 (105 km/h) which greater than MMI_V_PERMITTED but lower than MMI_V_INTERVENTION(2)   The speed pointer display in orange colour
            Test Step Comment: (1) MMI_gen 6299 (partly: MMI_M_WARNING, train speed in relation to permitted speed MMI_V_PERMITTED, UN mode in CSM supervision);(2) MMI_gen 6299 (partly: colour of speed pointer, UN mode in CSM supervision);
            */
            // Call generic Action Method
            DmiActions.Increase_the_train_speed_to_105_kmh_Note_dV_warning_max_is_defined_in_chapter_3_of_SUBSET_026(this);
            // Call generic Check Results Method
            DmiExpectedResults
                .Verify_the_following_information_1_Use_the_log_file_to_confirm_that_DMI_received_the_packet_information_EVC_1_with_the_following_condition_MMI_M_WARNING_4_Status_WaS_Supervision_CSM_while_the_value_of_MMI_V_TRAIN_2917_105_kmh_which_greater_than_MMI_V_PERMITTED_but_lower_than_MMI_V_INTERVENTION2_The_speed_pointer_display_in_orange_colour(this);


            /*
            Test Step 4
            Action: Increase the train speed to 106 km/h
            Expected Result: The train speed is force to decrease because of emergency brake is applied by ETCS onboard.Verify the following information,Before train speed is decreased(1)   Use the log file to confirm that DMI received the packet information EVC-1 with the following condition,MMI_M_WARNING = 12 (Status = IntS, Supervision = CSM) while the value of MMI_V_TRAIN = 2944 (106 km/h) which greater than MMI_V_INTERVENTION(2)   The speed pointer display in red colourAfter train speed is decreased(3)   Use the log file to confirm that DMI received the packet information EVC-1 with the following condition,MMI_M_WARNING = 12 (Status = IntS, Supervision = CSM) while the value of MMI_V_TRAIN is lower than MMI_V_INTERVENTION(4)   The speed pointer display in grey colour
            Test Step Comment: (1) MMI_gen 6299 (partly: MMI_M_WARNING, train speed in relation to permitted speed MMI_V_PERMITTED, UN mode in CSM supervision);(2) MMI_gen 6299 (partly: colour of speed pointer, UN mode in CSM supervision);(3) MMI_gen 6299 (partly: MMI_M_WARNING, UN mode in CSM supervision);(4) MMI_gen 6299 (partly: colour of speed pointer, UN mode in CSM supervision);
            */
            // Call generic Action Method
            DmiActions.Increase_the_train_speed_to_106_kmh(this);
            // Call generic Check Results Method
            DmiExpectedResults
                .The_train_speed_is_force_to_decrease_because_of_emergency_brake_is_applied_by_ETCS_onboard_Verify_the_following_information_Before_train_speed_is_decreased1_Use_the_log_file_to_confirm_that_DMI_received_the_packet_information_EVC_1_with_the_following_condition_MMI_M_WARNING_12_Status_IntS_Supervision_CSM_while_the_value_of_MMI_V_TRAIN_2944_106_kmh_which_greater_than_MMI_V_INTERVENTION2_The_speed_pointer_display_in_red_colourAfter_train_speed_is_decreased3_Use_the_log_file_to_confirm_that_DMI_received_the_packet_information_EVC_1_with_the_following_condition_MMI_M_WARNING_12_Status_IntS_Supervision_CSM_while_the_value_of_MMI_V_TRAIN_is_lower_than_MMI_V_INTERVENTION4_The_speed_pointer_display_in_grey_colour(this);


            /*
            Test Step 5
            Action: Stop the train.Then, use the test script file 12_3_4_a.xml to send the following packets,EVC-1MMI_M_WARNING = 2MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 972EVC-7OBU_TR_M_MODE = 4
            Expected Result: DMI displays in UN mode, level 0.Verify the following information,(1)   The speed pointer display in grey colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, UN mode in PIM supervision);
            */
            // Call generic Check Results Method
            DmiExpectedResults
                .DMI_displays_in_UN_mode_level_0_Verify_the_following_information_1_The_speed_pointer_display_in_grey_colour(this);


            /*
            Test Step 6
            Action: Use the test script file 12_3_4_b.xml to send the following packets,EVC-1MMI_M_WARNING = 2MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1111EVC-7OBU_TR_M_MODE = 4
            Expected Result: DMI displays in UN mode, level 0.Verify the following information,(1)   The speed pointer display in white colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, UN mode in PIM supervision);
            */
            // Call generic Check Results Method
            DmiExpectedResults
                .DMI_displays_in_UN_mode_level_0_Verify_the_following_information_1_The_speed_pointer_display_in_white_colour(this);


            /*
            Test Step 7
            Action: Use the test script file 12_3_4_c.xml to send the following packets,EVC-1MMI_M_WARNING = 10MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1139EVC-7OBU_TR_M_MODE = 4
            Expected Result: DMI displays in UN mode, level 0.Verify the following information,(1)   The speed pointer display in orange colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, UN mode in PIM supervision);
            */
            // Call generic Check Results Method
            DmiExpectedResults
                .DMI_displays_in_UN_mode_level_0_Verify_the_following_information_1_The_speed_pointer_display_in_orange_colour(this);


            /*
            Test Step 8
            Action: Use the test script file 12_3_4_d.xml to send the following packets,EVC-1MMI_M_WARNING = 6MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1250EVC-7OBU_TR_M_MODE = 4
            Expected Result: DMI displays in UN mode, level 0.Verify the following information,(1)   The speed pointer display in orange colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, UN mode in PIM supervision);
            */
            // Call generic Check Results Method
            DmiExpectedResults
                .DMI_displays_in_UN_mode_level_0_Verify_the_following_information_1_The_speed_pointer_display_in_orange_colour(this);


            /*
            Test Step 9
            Action: Use the test script file 12_3_4_e.xml to send the following packets,EVC-1MMI_M_WARNING = 14MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1277EVC-7OBU_TR_M_MODE = 4
            Expected Result: DMI displays in UN mode, level 0.Verify the following information,(1)   The speed pointer display in red colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, UN mode in PIM supervision);
            */
            // Call generic Check Results Method
            DmiExpectedResults
                .DMI_displays_in_UN_mode_level_0_Verify_the_following_information_1_The_speed_pointer_display_in_red_colour(this);


            /*
            Test Step 10
            Action: Use the test script file 12_3_4_f.xml to send the following packets,EVC-1MMI_M_WARNING = 14MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1111EVC-7OBU_TR_M_MODE = 4
            Expected Result: DMI displays in UN mode, level 0.Verify the following information,(1)   The speed pointer display in white colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, UN mode in PIM supervision);
            */
            // Call generic Check Results Method
            DmiExpectedResults
                .DMI_displays_in_UN_mode_level_0_Verify_the_following_information_1_The_speed_pointer_display_in_white_colour(this);


            /*
            Test Step 11
            Action: Use the test script file 12_3_4_g.xml to send the following packets,EVC-1MMI_M_WARNING = 14MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1000EVC-7OBU_TR_M_MODE = 4
            Expected Result: DMI displays in UN mode, level 0.Verify the following information,(1)   The speed pointer display in grey colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, UN mode in PIM supervision);
            */
            // Call generic Check Results Method
            DmiExpectedResults
                .DMI_displays_in_UN_mode_level_0_Verify_the_following_information_1_The_speed_pointer_display_in_grey_colour(this);


            /*
            Test Step 12
            Action: Use the test script file 12_3_4_h.xml to send the following packets,EVC-1MMI_M_WARNING = 11MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1000EVC-7OBU_TR_M_MODE = 4
            Expected Result: DMI displays in UN mode, level 0.Verify the following information,(1)   The speed pointer display in grey colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, UN mode in TSM supervision);
            */
            // Call generic Check Results Method
            DmiExpectedResults
                .DMI_displays_in_UN_mode_level_0_Verify_the_following_information_1_The_speed_pointer_display_in_grey_colour(this);


            /*
            Test Step 13
            Action: Use the test script file 12_3_4_i.xml to send the following packets,EVC-1MMI_M_WARNING = 11MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1111EVC-7OBU_TR_M_MODE = 4
            Expected Result: DMI displays in UN mode, level 0.Verify the following information,(1)   The speed pointer display in white colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, UN mode in TSM supervision);
            */
            // Call generic Check Results Method
            DmiExpectedResults
                .DMI_displays_in_UN_mode_level_0_Verify_the_following_information_1_The_speed_pointer_display_in_white_colour(this);


            /*
            Test Step 14
            Action: Use the test script file 12_3_4_j.xml to send the following packets,EVC-1MMI_M_WARNING = 1MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1111EVC-7OBU_TR_M_MODE = 4
            Expected Result: DMI displays in UN mode, level 0.Verify the following information,(1)   The speed pointer display in yellow colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, UN mode in TSM supervision);
            */
            // Call generic Check Results Method
            DmiExpectedResults
                .DMI_displays_in_UN_mode_level_0_Verify_the_following_information_1_The_speed_pointer_display_in_yellow_colour(this);


            /*
            Test Step 15
            Action: Use the test script file 12_3_4_k.xml to send the following packets,EVC-1MMI_M_WARNING = 9MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1139EVC-7OBU_TR_M_MODE = 4
            Expected Result: DMI displays in UN mode, level 0.Verify the following information,(1)   The speed pointer display in orange colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, UN mode in TSM supervision);
            */
            // Call generic Check Results Method
            DmiExpectedResults
                .DMI_displays_in_UN_mode_level_0_Verify_the_following_information_1_The_speed_pointer_display_in_orange_colour(this);


            /*
            Test Step 16
            Action: Use the test script file 12_3_4_l.xml to send the following packets,EVC-1MMI_M_WARNING = 5MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1250EVC-7OBU_TR_M_MODE = 4
            Expected Result: DMI displays in UN mode, level 0.Verify the following information,(1)   The speed pointer display in orange colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, UN mode in TSM supervision);
            */
            // Call generic Check Results Method
            DmiExpectedResults
                .DMI_displays_in_UN_mode_level_0_Verify_the_following_information_1_The_speed_pointer_display_in_orange_colour(this);


            /*
            Test Step 17
            Action: Use the test script file 12_3_4_m.xml to send the following packets,EVC-1MMI_M_WARNING = 13MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1277EVC-7OBU_TR_M_MODE = 4
            Expected Result: DMI displays in UN mode, level 0.Verify the following information,(1)   The speed pointer display in red colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, UN mode in TSM supervision);
            */
            // Call generic Check Results Method
            DmiExpectedResults
                .DMI_displays_in_UN_mode_level_0_Verify_the_following_information_1_The_speed_pointer_display_in_red_colour(this);


            /*
            Test Step 18
            Action: Use the test script file 12_3_4_n.xml to send the following packets,EVC-1MMI_M_WARNING = 13MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1111EVC-7OBU_TR_M_MODE = 4
            Expected Result: DMI displays in UN mode, level 0.Verify the following information,(1)   The speed pointer display in yellow colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, UN mode in TSM supervision);
            */
            // Call generic Check Results Method
            DmiExpectedResults
                .DMI_displays_in_UN_mode_level_0_Verify_the_following_information_1_The_speed_pointer_display_in_yellow_colour(this);


            /*
            Test Step 19
            Action: Use the test script file 12_3_4_o.xml to send the following packets,EVC-1MMI_M_WARNING = 13MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1083EVC-7OBU_TR_M_MODE = 4
            Expected Result: DMI displays in UN mode, level 0.Verify the following information,(1)   The speed pointer display in grey colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, UN mode in TSM supervision);
            */
            // Call generic Check Results Method
            DmiExpectedResults
                .DMI_displays_in_UN_mode_level_0_Verify_the_following_information_1_The_speed_pointer_display_in_grey_colour(this);


            /*
            Test Step 20
            Action: End of test
            Expected Result: 
            */


            return GlobalTestResult;
        }
    }
}
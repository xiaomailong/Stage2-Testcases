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
    /// 17.3.2 Speed Pointer: Colour of speed pointer in FS mode
    /// TC-ID: 12.3.2
    /// 
    /// This test case verifies the colour of speed pointer which display refer to received packet EVC-1 while the train is running in each supervision status and speed monitoring for FS mode and verifies the sound S2 which played when received MMI_M_WARNING = 6.
    /// 
    /// Tested Requirements:
    /// MMI_gen 6299 (partly: FS mode); MMI_gen 11921 (partly: MMI_M_WARNING = 6);
    /// 
    /// Scenario:
    /// 1.Drive the train forward pass BG1 at position 100m. Then, verify the display of speed pointer refer to received packet EVC-1.BG1: Packet 12, 21 and 27 (Entering FS)
    /// 2.Continue to drive the train forward with the specify speed. Then, verify the display of speed pointer refer to received packet EVC-1.
    /// 
    /// Used files:
    /// 12_3_2.tdg, 12_3_2_a.xml, 12_3_2_b.xml, 12_3_2_c.xml, 12_3_2_d.xml, 12_3_2_e.xml, 12_3_2_f.xml
    /// </summary>
    public class Speed_Pointer_Colour_of_speed_pointer_in_FS_mode : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // Test system is power on.Cabin is activated.SoM is performed in SR mode, Level 1.
            
            // Call the TestCaseBase PreExecution
            base.PreExecution();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in FS mode, level 1

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint

            
            /*
            Test Step 1
            Action: Drive the train forward with speed = 40km/h pass BG1
            Expected Result: DMI displays in FS mode, level 1.Verify the following information,(1)   Use the log file to confirm that DMI received the packet information EVC-1 and EVC-7 with following variables,(EVC-7) OBU_TR_M_MODE = 0 (Full Supervision)(EVC-1) MMI_M_WARNING = 0 (Status = NoS, Supervision = CSM)(EVC-1) MMI_V_PERMITTED = 1111 (40km/h)(2)   The speed pointer display in grey colour
            Test Step Comment: (1) MMI_gen 6299 (partly: OBU_TR_M_MODE, MMI_M_WARNING, train speed in relation to permitted speed MMI_V_PERMITTED, FS mode in CSM supervision);(2) MMI_gen 6299 (partly: colour of speed pointer, FS mode in CSM supervision);
            */
            
            
            /*
            Test Step 2
            Action: Increase the train speed to 41 km/h
            Expected Result: Verify the following information,(1)   Use the log file to confirm that DMI received the packet information EVC-1 with the following condition,MMI_M_WARNING = 8 (Status = OvS, Supervision = CSM) while the value of MMI_V_TRAIN = 1139 (41 km/h) which greater than MMI_V_PERMITTED(2)   The speed pointer display in orange colour
            Test Step Comment: (1) MMI_gen 6299 (partly: MMI_M_WARNING, train speed in relation to permitted speed MMI_V_PERMITTED, FS mode in CSM supervision);(2) MMI_gen 6299 (partly: colour of speed pointer, FS mode in CSM supervision);
            */
            // Call generic Action Method
            DmiActions.Increase_the_train_speed_to_41_kmh();
            // Call generic Check Results Method
            DmiExpectedResults.Verify_the_following_information_1_Use_the_log_file_to_confirm_that_DMI_received_the_packet_information_EVC_1_with_the_following_condition_MMI_M_WARNING_8_Status_OvS_Supervision_CSM_while_the_value_of_MMI_V_TRAIN_1139_41_kmh_which_greater_than_MMI_V_PERMITTED2_The_speed_pointer_display_in_orange_colour();
            
            
            /*
            Test Step 3
            Action: Increase the train speed to 45 km/h.Note: dV_warning_max is defined in chapter 3 of [SUBSET-026]
            Expected Result: Verify the following information,(1)   Use the log file to confirm that DMI received the packet information EVC-1 with the following condition,MMI_M_WARNING = 4 (Status = WaS, Supervision = CSM) while the value of MMI_V_TRAIN = 1250 (45 km/h) which greater than MMI_V_PERMITTED but lower than MMI_V_INTERVENTION(2)   The speed pointer display in orange colour
            Test Step Comment: (1) MMI_gen 6299 (partly: MMI_M_WARNING, train speed in relation to permitted speed MMI_V_PERMITTED, FS mode in CSM supervision);(2) MMI_gen 6299 (partly: colour of speed pointer, FS mode in CSM supervision);
            */
            // Call generic Action Method
            DmiActions.Increase_the_train_speed_to_45_kmh_Note_dV_warning_max_is_defined_in_chapter_3_of_SUBSET_026();
            // Call generic Check Results Method
            DmiExpectedResults.Verify_the_following_information_1_Use_the_log_file_to_confirm_that_DMI_received_the_packet_information_EVC_1_with_the_following_condition_MMI_M_WARNING_4_Status_WaS_Supervision_CSM_while_the_value_of_MMI_V_TRAIN_1250_45_kmh_which_greater_than_MMI_V_PERMITTED_but_lower_than_MMI_V_INTERVENTION2_The_speed_pointer_display_in_orange_colour();
            
            
            /*
            Test Step 4
            Action: Increase the train speed to 46 km/h
            Expected Result: The train speed is force to decrease because of emergency brake is applied by ETCS onboard.Verify the following information,Before train speed is decreased(1)   Use the log file to confirm that DMI received the packet information EVC-1 with the following condition,MMI_M_WARNING = 12 (Status = IntS, Supervision = CSM) while the value of MMI_V_TRAIN = 1278 (46 km/h) which greater than MMI_V_INTERVENTION(2)   The speed pointer display in red colourAfter train speed is decreased(3)   Use the log file to confirm that DMI received the packet information EVC-1 with the following condition,MMI_M_WARNING = 12 (Status = IntS, Supervision = CSM) while the value of MMI_V_TRAIN is lower than MMI_V_INTERVENTION(4)   The speed pointer display in grey colour
            Test Step Comment: (1) MMI_gen 6299 (partly: MMI_M_WARNING, train speed in relation to permitted speed MMI_V_PERMITTED, FS mode in CSM supervision);(2) MMI_gen 6299 (partly: colour of speed pointer, FS mode in CSM supervision);(3) MMI_gen 6299 (partly: MMI_M_WARNING, FS mode in CSM supervision);(4) MMI_gen 6299 (partly: colour of speed pointer, FS mode in CSM supervision);
            */
            // Call generic Action Method
            DmiActions.Increase_the_train_speed_to_46_kmh();
            // Call generic Check Results Method
            DmiExpectedResults.The_train_speed_is_force_to_decrease_because_of_emergency_brake_is_applied_by_ETCS_onboard_Verify_the_following_information_Before_train_speed_is_decreased1_Use_the_log_file_to_confirm_that_DMI_received_the_packet_information_EVC_1_with_the_following_condition_MMI_M_WARNING_12_Status_IntS_Supervision_CSM_while_the_value_of_MMI_V_TRAIN_1278_46_kmh_which_greater_than_MMI_V_INTERVENTION2_The_speed_pointer_display_in_red_colourAfter_train_speed_is_decreased3_Use_the_log_file_to_confirm_that_DMI_received_the_packet_information_EVC_1_with_the_following_condition_MMI_M_WARNING_12_Status_IntS_Supervision_CSM_while_the_value_of_MMI_V_TRAIN_is_lower_than_MMI_V_INTERVENTION4_The_speed_pointer_display_in_grey_colour();
            
            
            /*
            Test Step 5
            Action: Continue to drive the train forward with speed = 30 km/h.Then, stop the train
            Expected Result: Verify the following information,While the train is moving(1)   Use the log file to confirm that DMI received the packet information EVC-1 with the following condition,MMI_M_WARNING = 11 (Status = NoS, Supervision = TSM) while the value of MMI_V_TRAIN is greater than MMI_V_TARGET(2)    The speed pointer display in white colourWhen the train is stopped(3)    Use the log file to confirm that DMI received the packet information EVC-1 with the following condition,MMI_M_WARNING = 11 (Status = NoS, Supervision = TSM) while the value of MMI_V_TRAIN is lower than or same as MMI_V_TARGET(4)   The speed pointer display in grey colour
            Test Step Comment: (1) MMI_gen 6299 (partly: MMI_M_WARNING, train speed in relation to target speed MMI_V_TARGET, FS mode in TSM supervision);(2) MMI_gen 6299 (partly: colour of speed pointer, FS mode in TSM supervision);(3) MMI_gen 6299 (partly: MMI_M_WARNING, train speed in relation to target speed MMI_V_TARGET, FS mode in TSM supervision);(4) MMI_gen 6299 (partly: colour of speed pointer, FS mode in TSM supervision);
            */
            
            
            /*
            Test Step 6
            Action: Continue the drive the train forward with speed = 30 km/h
            Expected Result: The permitted speed is decreased continuously, driver can observe at the circular speed gauge.Verify the following information,While the train speed < permitted speed(1)   Use the log file to confirm that DMI received the packet information EVC-1 with the following condition,MMI_M_WARNING = 1 (Status = IndS, Supervision = TSM) while the value of MMI_V_TRAIN is lower than MMI_V_PERMITTED and MMI_V_TRAIN is greater than MMI_V_TARGET(2)    The speed pointer display in yellow colourWhile the train speed > permitted speed (3)   Use the log file to confirm that DMI received the packet information EVC-1 with the following condition,MMI_M_WARNING = 9 (Status = OvS and IndS, Supervision = TSM) while the value of MMI_V_TRAIN is greater than MMI_V_PERMITTEDMMI_M_WARNING = 5 (Status = WaS and IndS, Supervision = TSM) while the value of MMI_V_TRAIN is greater than MMI_V_PERMITTED(4)   The speed pointer display in orange colourWhile the train speed is force to decrease by emergency brake applied from ETCS onboard(5)   Use the log file to confirm that DMI received the packet information EVC-1 with the following condition,MMI_M_WARNING = 13 (Status = IntS and IndS, Supervision = TSM) while the value of MMI_V_TRAIN is greater than MMI_V_INTERVENTION(4)   The speed pointer display in red colour
            Test Step Comment: (1) MMI_gen 6299 (partly: MMI_M_WARNING, train speed in relation to permitted speed MMI_V_PERMITTED, FS mode in TSM supervision);(2) MMI_gen 6299 (partly: colour of speed pointer, FS mode in TSM supervision);(3) MMI_gen 6299 (partly: MMI_M_WARNING, FS mode in TSM supervision);(4) MMI_gen 6299 (partly: colour of speed pointer, FS mode in TSM supervision);(5) MMI_gen 6299 (partly: MMI_M_WARNING, FS mode in TSM supervision);(6) MMI_gen 6299 (partly: colour of speed pointer, FS mode in TSM supervision);
            */
            
            
            /*
            Test Step 7
            Action: Drive the train with speed = 5 km/h
            Expected Result: Verify the following information,(1)   Use the log file to confirm that DMI received the packet information EVC-1 with the following condition,MMI_M_WARNING = 3 (Status = IndS, Supervision = RSM) while the value of MMI_V_TRAIN is lower than or same as MMI_V_RELEASE(2)   The speed pointer display in yellow colour
            Test Step Comment: (1) MMI_gen 6299 (partly: MMI_M_WARNING, train speed in relation to release speed MMI_V_RELEASE, FS mode in RSM supervision);(2) MMI_gen 6299 (partly: colour of speed pointer, FS mode in RSM supervision);
            */
            // Call generic Action Method
            DmiActions.Drive_the_train_with_speed_5_kmh();
            
            
            /*
            Test Step 8
            Action: Drive the train with speed = 6 km/h
            Expected Result: Verify the following information,(1)   Use the log file to confirm that DMI received the packet information EVC-1 with the following condition,MMI_M_WARNING = 15 (Status = IntS and IndS, Supervision = RSM) while the value of MMI_V_TRAIN is greater than MMI_V_RELEASE(2)   The speed pointer display in yellow colour
            Test Step Comment: (1) MMI_gen 6299 (partly: MMI_M_WARNING, train speed in relation to release speed MMI_V_RELEASE, FS mode in RSM supervision);(2) MMI_gen 6299 (partly: colour of speed pointer, FS mode in CSM supervision);
            */
            
            
            /*
            Test Step 9
            Action: Stop the train.Then, use the test script file 12_3_2_a.xml to send the following packets,EVC-1MMI_M_WARNING = 2MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 972EVC-7OBU_TR_M_MODE = 0
            Expected Result: DMI displays in FS mode, level 1.Verify the following information,(1)   The speed pointer display in grey colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, FS mode in PIM supervision);
            */
            // Call generic Check Results Method
            DmiExpectedResults.DMI_displays_in_FS_mode_level_1_Verify_the_following_information_1_The_speed_pointer_display_in_grey_colour();
            
            
            /*
            Test Step 10
            Action: Use the test script file 12_3_2_b.xml to send the following packets,EVC-1MMI_M_WARNING = 2MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1111EVC-7OBU_TR_M_MODE = 0
            Expected Result: DMI displays in FS mode, level 1.Verify the following information,(1)   The speed pointer display in white colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, FS mode in PIM supervision);
            */
            
            
            /*
            Test Step 11
            Action: Use the test script file 12_3_2_c.xml to send the following packets,EVC-1MMI_M_WARNING = 10MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1139EVC-7OBU_TR_M_MODE = 0
            Expected Result: DMI displays in FS mode, level 1.Verify the following information,(1)   The speed pointer display in orange colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, FS mode in PIM supervision);
            */
            
            
            /*
            Test Step 12
            Action: Use the test script file 12_3_2_d.xml to send the following packets,EVC-1MMI_M_WARNING = 6MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1250EVC-7OBU_TR_M_MODE = 0
            Expected Result: DMI displays in FS mode, level 1.Verify the following information,(1)   The speed pointer display in orange colour(2)   Sound S2 is played while the Warning Status is active
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, FS mode in PIM supervision);(2) MMI_gen 11921 (partly: MMI_M_WARNING = 6);
            */
            
            
            /*
            Test Step 13
            Action: Use the test script file 12_3_2_e.xml to send the following packets,EVC-1MMI_M_WARNING = 14MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1277EVC-7OBU_TR_M_MODE = 0
            Expected Result: DMI displays in FS mode, level 1.Verify the following information,(1)   The speed pointer display in red colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, FS mode in PIM supervision);
            */
            
            
            /*
            Test Step 14
            Action: Use the test script file 12_3_2_f.xml to send the following packets,EVC-1MMI_M_WARNING = 13MMI_V_PERMITTED = 1111MMI_V_TARGET = 0MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 0EVC-7OBU_TR_M_MODE = 0
            Expected Result: DMI displays in FS mode, level 1.Verify the following information,(1)   The speed pointer display in grey colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, FS mode in TSM supervision);
            */
            // Call generic Check Results Method
            DmiExpectedResults.DMI_displays_in_FS_mode_level_1_Verify_the_following_information_1_The_speed_pointer_display_in_grey_colour();
            
            
            /*
            Test Step 15
            Action: End of test
            Expected Result: 
            */
            

            return GlobalTestResult;
        }
    }
}

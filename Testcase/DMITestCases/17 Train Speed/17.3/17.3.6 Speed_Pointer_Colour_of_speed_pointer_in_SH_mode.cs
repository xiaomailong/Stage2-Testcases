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

namespace Testcase.DMITestCases
{
    /// <summary>
    /// 17.3.6 Speed Pointer: Colour of speed pointer in SH mode
    /// TC-ID: 12.3.6
    /// 
    /// This test case verifies the colour of speed pointer which display refer to received packet EVC-1 while the train is running in each supervision status and speed monitoring for SH mode.
    /// 
    /// Tested Requirements:
    /// MMI_gen 6299 (partly: SH mode); 
    /// 
    /// Scenario:
    /// 1.Enter SH mode, level 
    /// 1.Then, drive the train with specify speed and verify the display of speed pointer refer to received packet EVC-1.
    /// 
    /// Used files:
    /// N/A
    /// </summary>
    public class Speed_Pointer_Colour_of_speed_pointer_in_SH_mode : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // Test system is power on.Cabin is activated.Drive ID is entered and brake test in performed.Level 1 is selected and confirmed.

            // Call the TestCaseBase PreExecution
            base.PreExecution();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in SH mode, level 1

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint

            EVC7_MMIEtcsMiscOutSignals.Initialise(this);

            EVC1_MMIDynamic.Initialise(this);
            EVC1_MMIDynamic.MMI_V_PERMITTED = 1111;
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 40;
            //?? Send

            /*
            Test Step 1
            Action: Perform the following procedure,Press and hold ‘Shunting’ button at least 2 seconds.Release the pressed button
            Expected Result: DMI displays in SH mode, level 1
            */
            WaitForVerification("Press and hold the ‘Shunting’ button for at least 2 seconds. Release the pressed button." + Environment.NewLine +
                                "Then check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in SH mode, level 1.");

            /*
            Test Step 2
            Action: Drive the train with speed = 30 km/h
            Expected Result: Verify the following information,(1)   Use the log file to confirm that DMI received the packet information EVC-1 and EVC-7 with following variables,(EVC-7) OBU_TR_M_MODE = 3 (Shunting)(EVC-1) MMI_M_WARNING = 0 (Status = NoS, Supervision = CSM)(EVC-1) MMI_V_PERMITTED = 833 (30 km/h)(2)   The speed pointer display in grey colour
            Test Step Comment: (1) MMI_gen 6299 (partly: OBU_TR_M_MODE, MMI_M_WARNING, train speed in relation to permitted speed MMI_V_PERMITTED, SH mode in CSM supervision);(2) MMI_gen 6299 (partly: colour of speed pointer, SH mode in CSM supervision);
            */
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.Shunting;
            // ?? Send
            EVC1_MMIDynamic.MMI_V_PERMITTED = 833;
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 30;
            // ?? Send
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Is the speed pointer grey?");

            /*
            Test Step 3
            Action: Increase the train speed to 31 km/h
            Expected Result: Verify the following information,(1)   Use the log file to confirm that DMI received the packet information EVC-1 with the following condition,MMI_M_WARNING = 8 (Status = OvS, Supervision = CSM) while the value of MMI_V_TRAIN = 861 (31 km/h) which greater than MMI_V_PERMITTED(2)   The speed pointer display in orange colour
            Test Step Comment: (1) MMI_gen 6299 (partly: MMI_M_WARNING, train speed in relation to permitted speed MMI_V_PERMITTED, SH mode in CSM supervision);(2) MMI_gen 6299 (partly: colour of speed pointer, SH mode in CSM supervision);
            */
            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Overspeed_Status_Ceiling_Speed_Monitoring;
            // ?? Send
            // Call generic Action Method
            DmiActions.Increase_the_train_speed_to_31_kmh(this);
            // Call generic Check Results Method
            DmiExpectedResults.Verify_the_following_information_1_Use_the_log_file_to_confirm_that_DMI_received_the_packet_information_EVC_1_with_the_following_condition_MMI_M_WARNING_8_Status_OvS_Supervision_CSM_while_the_value_of_MMI_V_TRAIN_861_31_kmh_which_greater_than_MMI_V_PERMITTED2_The_speed_pointer_display_in_orange_colour(this);

            /*
            Test Step 4
            Action: Increase the train speed to 35 km/h.Note: dV_warning_max is defined in chapter 3 of [SUBSET-026]
            Expected Result: Verify the following information,(1)   Use the log file to confirm that DMI received the packet information EVC-1 with the following condition,MMI_M_WARNING = 4 (Status = WaS, Supervision = CSM) while the value of MMI_V_TRAIN = 972 (35  km/h) which greater than MMI_V_PERMITTED but lower than MMI_V_INTERVENTION(2)   The speed pointer display in orange colour
            Test Step Comment: (1) MMI_gen 6299 (partly: MMI_M_WARNING, train speed in relation to permitted speed MMI_V_PERMITTED, SH mode in CSM supervision);(2) MMI_gen 6299 (partly: colour of speed pointer, SH mode in CSM supervision);
            */
            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Warning_Status_Ceiling_Speed_Monitoring;
            // ?? Send

            // Call generic Action Method
            DmiActions.Increase_the_train_speed_to_35_kmh_Note_dV_warning_max_is_defined_in_chapter_3_of_SUBSET_026(this);
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Is the speed pointer orange?");

            /*
            Test Step 5
            Action: Increase the train speed to 36 km/h
            Expected Result: The train speed is force to decrease because of emergency brake is applied by ETCS onboard.Verify the following information,Before train speed is decreased(1)   Use the log file to confirm that DMI received the packet information EVC-1 with the following condition,MMI_M_WARNING = 12 (Status = IntS, Supervision = CSM) while the value of MMI_V_TRAIN = 1000 (36 km/h) which greater than MMI_V_INTERVENTION(2)   The speed pointer display in red colourAfter train speed is decreased(3)   Use the log file to confirm that DMI received the packet information EVC-1 with the following condition,MMI_M_WARNING = 12 (Status = IntS, Supervision = CSM) while the value of MMI_V_TRAIN is lower than MMI_V_INTERVENTION(4)   The speed pointer display in grey colour
            Test Step Comment: (1) MMI_gen 6299 (partly: MMI_M_WARNING, train speed in relation to permitted speed MMI_V_PERMITTED, SH mode in CSM supervision);(2) MMI_gen 6299 (partly: colour of speed pointer, SH mode in CSM supervision);(3) MMI_gen 6299 (partly: MMI_M_WARNING, SH mode in CSM supervision);(4) MMI_gen 6299 (partly: colour of speed pointer, SH mode in CSM supervision);
            */
            // Call generic Action Method
            DmiActions.Increase_the_train_speed_to_36_kmh(this);
            // Call generic Check Results Method
            DmiExpectedResults.The_train_speed_is_force_to_decrease_because_of_emergency_brake_is_applied_by_ETCS_onboard_Verify_the_following_information_Before_train_speed_is_decreased1_Use_the_log_file_to_confirm_that_DMI_received_the_packet_information_EVC_1_with_the_following_condition_MMI_M_WARNING_12_Status_IntS_Supervision_CSM_while_the_value_of_MMI_V_TRAIN_1000_36_kmh_which_greater_than_MMI_V_INTERVENTION2_The_speed_pointer_display_in_red_colourAfter_train_speed_is_decreased3_Use_the_log_file_to_confirm_that_DMI_received_the_packet_information_EVC_1_with_the_following_condition_MMI_M_WARNING_12_Status_IntS_Supervision_CSM_while_the_value_of_MMI_V_TRAIN_is_lower_than_MMI_V_INTERVENTION4_The_speed_pointer_display_in_grey_colour(this);

            /*
            Test Step 6
            Action: End of test
            Expected Result: 
            */


            return GlobalTestResult;
        }
    }
}
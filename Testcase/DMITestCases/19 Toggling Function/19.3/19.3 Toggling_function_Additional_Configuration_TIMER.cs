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
    /// 19.3 Toggling function: Additional Configuration ‘TIMER’
    /// TC-ID: 14.3
    /// 
    /// This case verifies the toggling function of the basic speed hooks, release speed ditial and distance to target (digital) which is configured “TIMER” on DMI in toggling-affected mode SR/OS/SH/ and non-toggling-affected mode UN/FS/TR/PT/RV. The Toggling function shall comply with [MMI-ETCS-gen].
    /// 
    /// Tested Requirements:
    /// MMI_gen 6890; MMI_gen 6892 (partly: RV mode, UN mode, FS mode, TR mode, LS mode, PT mode, Table 34, Table 38, Table 35); MMI_gen 11868; MMI_gen 6894; MMI_gen 6896; MMI_gen 6450 (partly: 3rd bullet); MMI_gen 6898 (partly: configuration ‘TIMER”); Table 34, Table 38, Information (paragraph 1) under MMI_gen 6898 (inoperable), Information (paragraph 2) under MMI_gen 6898 (re-establish)
    /// 
    /// Scenario:
    /// Drive the train forward pass BG1 at position 200mBG1: packet 12, 21, 27, 138 and 139 (Entering FS mode and reversing allowance area)Enter RV mode, Level 
    /// 1.Then, Perform the procedure in note below to verify that toggling function is disabled in RV mode.De-activate Cabin A and Activate Cabin A again.Perform SoM in SR mode, Level 1.Enter and confirm specified value of SR speed and SR distance. Then, verify that Basic speed Hooks and Distance to target (digital) are no displayed on DMI.Perform the procedure in note below to verify that toggling function is enabled in SR mode.Drive the train forward with speed below permitted pass BG2 at position 300mBG2: packet 41 (Level 0 Trainsition)Stop the train. Then, Perform the procedure in note below to verify that toggling function is disalbed in UN mode.Drive the train forward pass BG3 at position 500mBG3: packet 12, 21 and 27 (Entering FS mode)Stop the train. Then, Perform the procedure in note below to verify that toggling function is disalbed in FS mode.Drive the train forward pass BG4 at position 700mBG4: packet 12, 21, 27 and 80 (Entering OS mode)Stop the train at position 800m. Then, Perform the procedure in note below to verify that toggling function is enabled in OS mode.Drive the train forward pass BG5 at position 1000mBG5: packet 12, 21, 27 and 80 (Entering LS mode)Note: The train wiill return to FS mode at postion 900m before entering LS mode.Stop the train at position 1100m. Then, Perform the procedure in note below to verify that toggling function is disabled in LS mode.Drive the train pass EOA (over 1300m), Then, Perform the procedure in note below to verify that toggling function is disabled in TR mode.Acknowledge TR mode. Then, Perform the procedure in note below to verify that toggling function is disabled in PT mode.Force the train enter SH mode, Then, Perform the procedure in note below to verify that toggling function is enabled in SH mode.Note: Procedure for toggling function verification,Press on area A1-A4 respectively.Waiting for 10 second and verify that specifix objects are reappear.Press around areas B.Waiting for 10 second and verify that specifix objects are reappear.
    /// 
    /// Used files:
    /// 14_3.tdg
    /// </summary>
    public class Toggling_function_Additional_Configuration_TIMER1 : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // The DMI is configured TOGGLE_FUNCTION = 2 (‘TIMER’) and TOGGLE_TIMER = 10 (10 seconds), see Appendix 1.System is power on.SoM is performed in SR mode, Level1.
            
            // Call the TestCaseBase PreExecution
            base.PreExecution();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in PT mode, Level 1.

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint

            
            /*
            Test Step 1
            Action: Drive the train forward pass BG1.Then stop the train
            Expected Result: DMI displays in FS mode, Level 1 with the ST06 symbol at sub-area C6
            */
            // Call generic Action Method
            DmiActions.Drive_the_train_forward_pass_BG1_Then_stop_the_train();
            // Call generic Check Results Method
            DmiExpectedResults.DMI_displays_in_FS_mode_Level_1_with_the_ST06_symbol_at_sub_area_C6();
            
            
            /*
            Test Step 2
            Action: Perform the following procedure,Chage the train direction to reversePress the symbol in sub-area C1
            Expected Result: DMI displays in RV mode, Level 1.Verify the following information,The objects below are displayed on DMI,White Basic speed HookDistance to target (digital)The objects below are not displayed on DMI,Medium-grey basic speed hookRelease Speed Digital
            Test Step Comment: (1) MMI_gen 6892 (partly: RV mode, Table 34 (CSM), Table 38 (CSM))(2) MMI_gen 6890 (partly: RV mode, unidentified mode, un-concerned object), Table 34 (CSM), Table 35 (CSM)
            */
            // Call generic Action Method
            DmiActions.Perform_the_following_procedure_Chage_the_train_direction_to_reversePress_the_symbol_in_sub_area_C1();
            // Call generic Check Results Method
            DmiExpectedResults.DMI_displays_in_RV_mode_Level_1_Verify_the_following_information_The_objects_below_are_displayed_on_DMI_White_Basic_speed_HookDistance_to_target_digitalThe_objects_below_are_not_displayed_on_DMI_Medium_grey_basic_speed_hookRelease_Speed_Digital();
            
            
            /*
            Test Step 3
            Action: Press, at least twice, on area A1-A4, and area B respectively
            Expected Result: Verify the following information,The objects below are not toggled visible/invisible, (always remain the same as the previous step)White Basic speed HookMedium-grey basic speed hookDistance to target (digital)Release Speed Digital
            Test Step Comment: (1) MMI_gen 6892 (partly: Area A and B, RV mode) MMI_gen 6890 (partly: RV mode, unidentified mode, un-concerned object);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(@"Press, at least twice, on area A1-A4, and area B respectively");
            // Call generic Check Results Method
            DmiExpectedResults.Verify_the_following_information_The_objects_below_are_not_toggled_visibleinvisible_always_remain_the_same_as_the_previous_stepWhite_Basic_speed_HookMedium_grey_basic_speed_hookDistance_to_target_digitalRelease_Speed_Digital();
            
            
            /*
            Test Step 4
            Action: Perform the following procedure,De-activate Cabin AActivate Cabin A
            Expected Result: DMI displays in SB mode, Level 1
            */
            // Call generic Action Method
            DmiActions.Perform_the_following_procedure_De_activate_Cabin_AActivate_Cabin_A();
            // Call generic Check Results Method
            DmiExpectedResults.DMI_displays_in_SB_mode_Level_1();
            
            
            /*
            Test Step 5
            Action: Perform SoM in SR mode, Level 1.Note: Stopwatch is required for accuracy of test result
            Expected Result: DMI displays in SR mode, Level 1.The objects below are displayed on DMI for 10 secondsWhite basic speed hookDistance to target (digital)(2) The release speed digital is not displayed
            Test Step Comment: (1) MMI_gen 11868 (partly: SR mode), Table 34 (CSM), Table 38 (CSM), MMI_gen 6450 (partly: 3rd bullet, SR mode), MMI_gen 6898 (partly: configuration ‘TIMER’, SR mode);(2) MMI_gen 6890 (partly: SR mode, un-concerned object), Table 35 (CSM)
            */
            
            
            /*
            Test Step 6
            Action: Perform the following procedure,Press ‘Spec’ button.Press ‘SR speed/distance’ button.Enter and confirm the following data,SR speed = 40 km/hSR distance = 300 mPress the speedometer onceNote: Stopwatch is required for accuracy of test result
            Expected Result: Verify the following information,The objects below are displayed on DMI for 10 secondsWhite basic speed hookMedium-grey basic speed hookDistance to target (digital)The release speed digital is not displayed
            Test Step Comment: (1) MMI_gen 11868 (partly: SR mode);                    MMI_gen 6450 (partly: 3rd bullet, SR mode), Table 34 (not CSM), Table 38 (not CSM), MMI_gen 6898 (partly: configuration ‘TIMER’);(2) MMI_gen 6890 (partly: SR mode, un-concerned object), Table 35 (not CSM)
            */
            
            
            /*
            Test Step 7
            Action: Press the speedometer onceNote: Stopwatch is required for accuracy of test result
            Expected Result: Verify the following information,The objects below are displays for 10 seconds. Then, disappear.White basic speed hookMedium-grey basic speed hookDistance to target (digital)The release speed digital is still displayed
            Test Step Comment: (1) MMI_gen 6890 (partly: Areas A, SR mode, toggle off), MMI_gen 6896 (partly: configuration ‘TIMER’, SR mode, toggle invisible), MMI_gen 6894 (partly: SR mode);    (2) MMI_gen 6890 (partly: SR mode, un-concerned object, toggle on) , Table 35 (not CSM)
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(@"Press the speedometer onceNote: Stopwatch is required for accuracy of test result");
            
            
            /*
            Test Step 8
            Action: Press, only once, on area A1-A4, and area B respectively.Then, continue to drive the train forward after expected result verified.Note: Stopwatch is required for accuracy of test result
            Expected Result: Verify the following information,(1)   The objects below are displays for 10 seconds. Then, disappear.White basic speed hookMedium-grey basic speed hookDistance to target (digital)(2)   The release speed digital remains the same
            Test Step Comment: (1) MMI_gen 6890 (partly: Areas A, Area B, SR mode);                                  MMI_gen 6896 (partly: configuration ‘TIMER’, SR mode); MMI_gen 6894 (partly: SR mode); (2) MMI_gen 6890 (partly: SR mode, un-concerned object, toggle on) , Table 35 (not CSM) 
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(@"Press, only once, on area A1-A4, and area B respectively.Then, continue to drive the train forward after expected result verified.Note: Stopwatch is required for accuracy of test result");
            
            
            /*
            Test Step 9
            Action: Drive the train forward pass BG1 with speed = 20km/h (or below permitted speed).Then, press an area C1 for acknowledgement
            Expected Result: DMI displays in UN mode, Level 0.Verify the following information,The objects below are not displayed on DMI,White Basic speed HookMedium-grey basic speed hookDistance to target (digital)Release Speed Digital
            Test Step Comment: MMI_gen 6892 (partly: UN mode, Table 34, Table 38, Table 35), MMI_gen 6890 (partly: FS mode, unidentified mode, un-concerned object)
            */
            
            
            /*
            Test Step 10
            Action: Stop the train.Press, at least twice, on area A1-A4, and area B respectively.Then, continue to drive the train forward after expected result verified
            Expected Result: Verify the following information,The objects below are not toggled visible/invisible, (always remain the same as the previous step)White basic speed hookMedium-grey basic speed hookDistance to target (digital)Release Speed Digital
            Test Step Comment: (1) MMI_gen 6892 (partly: Areas A and B for UN mode), MMI_gen 6890 (partly: UN mode, unidentified mode, un-concerned object);
            */
            // Call generic Action Method
            DmiActions.Stop_the_train_Press_at_least_twice_on_area_A1_A4_and_area_B_respectively_Then_continue_to_drive_the_train_forward_after_expected_result_verified();
            // Call generic Check Results Method
            DmiExpectedResults.Verify_the_following_information_The_objects_below_are_not_toggled_visibleinvisible_always_remain_the_same_as_the_previous_stepWhite_basic_speed_hookMedium_grey_basic_speed_hookDistance_to_target_digitalRelease_Speed_Digital();
            
            
            /*
            Test Step 11
            Action: Drive the train forward pass BG2.Then, press an area C1 for acknowledgement
            Expected Result: DMI displays in FS mode, Level 1.Verify the following information,The objects below are displayed on DMI,Distance to target (digital)Release Speed DigitalThe objects below are not displayed on DMI,White Basic speed HookMedium-grey basic speed hook
            Test Step Comment: (1) MMI_gen 6892 (partly: FS mode, Table 38 (not CSM), Table 35 (not CSM))(2) MMI_gen 6890 (partly: FS mode, unidentified mode, un-concerned object), Table 34 (not CSM)
            */
            // Call generic Check Results Method
            DmiExpectedResults.DMI_displays_in_FS_mode_Level_1_Verify_the_following_information_The_objects_below_are_displayed_on_DMI_Distance_to_target_digitalRelease_Speed_DigitalThe_objects_below_are_not_displayed_on_DMI_White_Basic_speed_HookMedium_grey_basic_speed_hook();
            
            
            /*
            Test Step 12
            Action: Stop the train.Then, press, at least twice,  on area A1-A4, and area B respectively.Then, continue to drive the train forward after expected result verified
            Expected Result: Verify the following information,The objects below are not toggled visible/invisible, (always remain the same as the previous step)White Basic speed HookMedium-grey basic speed hookDistance to target (digital)Release Speed Digital
            Test Step Comment: (1) MMI_gen 6892 (partly: Areas A and B for FS mode) MMI_gen 6890 (partly: FS mode, unidentified mode, un-concerned object); 
            */
            // Call generic Action Method
            DmiActions.Stop_the_train_Then_press_at_least_twice_on_area_A1_A4_and_area_B_respectively_Then_continue_to_drive_the_train_forward_after_expected_result_verified();
            // Call generic Check Results Method
            DmiExpectedResults.Verify_the_following_information_The_objects_below_are_not_toggled_visibleinvisible_always_remain_the_same_as_the_previous_stepWhite_Basic_speed_HookMedium_grey_basic_speed_hookDistance_to_target_digitalRelease_Speed_Digital();
            
            
            /*
            Test Step 13
            Action: Drive the train forward pass BG3. Then, acknowledge OS mode by press a sub-area C1
            Expected Result: DMI displays in OS mode, Level 1.Verify the following information,The objects below are displayed on DMI for 10 secondsBasic speed Hook(s)Distance to target (digital)Release speed digital
            Test Step Comment: (1) MMI_gen 11868 (partly: OS mode), Table 34 (not CSM), Table 35 (not CSM), Table 38 (not CSM), MMI_gen 6450 (partly: 3rd bullet, OS mode), MMI_gen 6898 (configuration ‘TIMER’, OS mode);
            */
            
            
            /*
            Test Step 14
            Action: Stop the train.Press the speedometer onceNote: Stopwatch is required for accuracy of test result
            Expected Result: Verify the following information,The objects below are displays for 10 seconds (toggled off). Then, disappear.White Basic speed HookMedium-grey basic speed hookDistance to target (digital)Release Speed Digital
            Test Step Comment: (1) MMI_gen 6890 (partly: OS mode, toggle on), MMI_gen 6896 (partly: configuration ‘TIMER’, OS mode, toggle visible), MMI_gen 6894 (partly: OS mode); 
            */
            
            
            /*
            Test Step 15
            Action: Press, only once, on area A1-A4, and area B respectively.Then, continue to drive the train forward after expected result verified.Note: Stopwatch is required for accuracy of test result
            Expected Result: Verify the following information,(1)   The objects below are displays for 10 seconds. Then, disappear.White basic speed hookMedium-grey basic speed hookDistance to target (digital)Release Speed Digital
            Test Step Comment: (1) MMI_gen 6890 (partly: Areas A, Area B, OS mode);                                  MMI_gen 6896 (partly: configuration ‘TIMER’, OS mode);                                      MMI_gen 6894 (partly: OS mode);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(@"Press, only once, on area A1-A4, and area B respectively.Then, continue to drive the train forward after expected result verified.Note: Stopwatch is required for accuracy of test result");
            
            
            /*
            Test Step 16
            Action: Drive the train forward pass BG5. Then, acknowledge LS mode by press a sub-area C1
            Expected Result: DMI displays in LS mode, Level 1.Verify the following information,The objects below are displayed on DMI,Distance to target (digital)Release Speed DigitalThe objects below are not displayed on DMI,White Basic speed HookMedium-grey basic speed hook
            Test Step Comment: (1) MMI_gen 6892 (partly: LS mode, Table 35 (not CSM), Table 38 (not CSM))(2) MMI_gen 6890 (partly: LS mode, unidentified mode, un-concerned object), Table 34 (not CSM)
            */
            // Call generic Action Method
            DmiActions.Drive_the_train_forward_pass_BG5_Then_acknowledge_LS_mode_by_press_a_sub_area_C1();
            // Call generic Check Results Method
            DmiExpectedResults.DMI_displays_in_LS_mode_Level_1_Verify_the_following_information_The_objects_below_are_displayed_on_DMI_Distance_to_target_digitalRelease_Speed_DigitalThe_objects_below_are_not_displayed_on_DMI_White_Basic_speed_HookMedium_grey_basic_speed_hook();
            
            
            /*
            Test Step 17
            Action: Stop the train.Press, at least twice, on area A1-A4, and area B respectively.Then, continue to drive the train forward after expected result verified
            Expected Result: Verify the following information,The objects below are not toggled visible/invisible, (always remain the same as the previous step)White Basic speed HookMedium-grey basic speed hookDistance to target (digital)Release Speed Digital
            Test Step Comment: (1) MMI_gen 6892 (partly: Area A and B, LS mode) MMI_gen 6890 (partly: LS mode, unidentified mode, un-concerned object);
            */
            // Call generic Action Method
            DmiActions.Stop_the_train_Press_at_least_twice_on_area_A1_A4_and_area_B_respectively_Then_continue_to_drive_the_train_forward_after_expected_result_verified();
            // Call generic Check Results Method
            DmiExpectedResults.Verify_the_following_information_The_objects_below_are_not_toggled_visibleinvisible_always_remain_the_same_as_the_previous_stepWhite_Basic_speed_HookMedium_grey_basic_speed_hookDistance_to_target_digitalRelease_Speed_Digital();
            
            
            /*
            Test Step 18
            Action: Drive the train pass through EOA
            Expected Result: DMI displays in TR mode, Level 1.Verify the following information,The objects below are not displayed on DMI,White Basic speed HookMedium-grey basic speed hookDistance to target (digital)Release Speed Digital
            Test Step Comment: (1) MMI_gen 6892 (partly: TR mode, Table 34, Table 38, Table 35), MMI_gen 6890 (partly: TR mode, unidentified mode, un-concerned object)
            */
            // Call generic Action Method
            DmiActions.Drive_the_train_pass_through_EOA();
            
            
            /*
            Test Step 19
            Action: Stop the train.Press, at least twice, on area A1-A4, and area B respectively.Then, continue to drive the train forward after expected result verified
            Expected Result: Verify the following information,The objects below are not toggled visible/invisible, (always remain the same as the previous step)White basic speed hookMedium-grey basic speed hookDistance to target (digital)Release Speed Digital
            Test Step Comment: (1) MMI_gen 6892 (partly: Areas A and B for TR mode), MMI_gen 6890 (partly: TR mode, unidentified mode, un-concerned object);
            */
            // Call generic Action Method
            DmiActions.Stop_the_train_Press_at_least_twice_on_area_A1_A4_and_area_B_respectively_Then_continue_to_drive_the_train_forward_after_expected_result_verified();
            // Call generic Check Results Method
            DmiExpectedResults.Verify_the_following_information_The_objects_below_are_not_toggled_visibleinvisible_always_remain_the_same_as_the_previous_stepWhite_basic_speed_hookMedium_grey_basic_speed_hookDistance_to_target_digitalRelease_Speed_Digital();
            
            
            /*
            Test Step 20
            Action: Acknowledge TR mode by press a sub-area C1
            Expected Result: DMI displays in PT mode, Level 1. Verify the following information,The objects below are not displayed on DMI,White Basic speed HookMedium-grey basic speed hookDistance to target (digital)Release Speed Digital
            Test Step Comment: (1) MMI_gen 6892 (partly: PT mode, Table 34, Table 38, Table 35), MMI_gen 6890 (partly: PT mode, unidentified mode, un-concerned object)
            */
            // Call generic Action Method
            DmiActions.Acknowledge_TR_mode_by_press_a_sub_area_C1();
            // Call generic Check Results Method
            DmiExpectedResults.DMI_displays_in_PT_mode_Level_1_Verify_the_following_information_The_objects_below_are_not_displayed_on_DMI_White_Basic_speed_HookMedium_grey_basic_speed_hookDistance_to_target_digitalRelease_Speed_Digital();
            
            
            /*
            Test Step 21
            Action: Stop the train.Press, at least twice, on area A1-A4, and area B respectively.Then, continue to drive the train forward after expected result verified
            Expected Result: Verify the following information,The objects below are not toggled visible/invisible, (always remain the same as the previous step)White basic speed hookMedium-grey basic speed hookDistance to target (digital)Release Speed Digital
            Test Step Comment: (1) MMI_gen 6892 (partly: Areas A and B for PT mode), MMI_gen 6890 (partly: PT mode, unidentified mode, un-concerned object);
            */
            // Call generic Action Method
            DmiActions.Stop_the_train_Press_at_least_twice_on_area_A1_A4_and_area_B_respectively_Then_continue_to_drive_the_train_forward_after_expected_result_verified();
            // Call generic Check Results Method
            DmiExpectedResults.Verify_the_following_information_The_objects_below_are_not_toggled_visibleinvisible_always_remain_the_same_as_the_previous_stepWhite_basic_speed_hookMedium_grey_basic_speed_hookDistance_to_target_digitalRelease_Speed_Digital();
            
            
            /*
            Test Step 22
            Action: Perform the following procedure,Press ‘Main’ button.Press and hold ‘Shunting’ button up to 2 second.Release ‘Shunting’ button.Note: Stopwatch is required for accuracy of test result
            Expected Result: DMI displays in SH mode, Level 1.Verify the following information,The white basic speed hook is displayed on DMI for 10 secondsThe objects below are not displayed on DMI,Medium-grey basic speed hookDistance to target (digital)Release Speed Digital
            Test Step Comment: (1) MMI_gen 11868 (partly: SH mode);                    MMI_gen 6450 (partly: 3rd bullet, SH mode) , Table 34 (CSM), MMI_gen 6898 (partly: configuration ‘TIMER’);(2) MMI_gen 6890 (partly: SH mode, un-concerned object), Table 34 (CSM), Table 38 (CSM), Table 35 (CSM)
            */
            
            
            /*
            Test Step 23
            Action: Press the speedometer onceNote: Stopwatch is required for accuracy of test result
            Expected Result: Verify the following information,The white basic speed hook displays for 10 seconds (toggled off). Then, disappears.The objects below are still not displayed on DMI,Medium-grey basic speed hookDistance to target (digital)Release Speed Digital
            Test Step Comment: (1) MMI_gen 6890 (partly: SH mode, toggle off), MMI_gen 6896 (partly: configuration ‘TIMER’, SH mode, toggle invisible), MMI_gen 6894 (partly: SH mode);(2) MMI_gen 6890 (partly: SH mode, un-concerned object), Table 34 (CSM), Table 38 (CSM), Table 35 (CSM)
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(@"Press the speedometer onceNote: Stopwatch is required for accuracy of test result");
            
            
            /*
            Test Step 24
            Action: Press, only once, on area A1-A4, and area B respectively.Then, continue to drive the train forward after expected result verified.Note: Stopwatch is required for accuracy of test result
            Expected Result: Verify the following information,The white basic speed hook is displays for 10 seconds. Then, disappear.The objects below are not toggled visible/invisible, (always remain the same as the previous step),Medium-grey basic speed hookDistance to target (digital)Release Speed Digital
            Test Step Comment: (1) MMI_gen 6890 (partly: Areas A, Area B, SH mode);                                  MMI_gen 6896 (partly: configuration ‘TIMER’, SH mode);                                      MMI_gen 6894 (partly: SH mode); (2) MMI_gen 6890 (partly: SH mode, un-concerned object), Table 34 (CSM), Table 38 (CSM), Table 35 (CSM)
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(@"Press, only once, on area A1-A4, and area B respectively.Then, continue to drive the train forward after expected result verified.Note: Stopwatch is required for accuracy of test result");
            
            
            /*
            Test Step 25
            Action: Press a sensitivity area (areas A1-A4 or B) to make a Basic Speed Hook appear.Then simulate loss-communication between ETCS onboard and DMI (1 second).Note: Stopwatch is required for accuracy of test result
            Expected Result: DMI displays the  message “ATP Down Alarm” with sound alarm.Verify the following information,The objects below are not displayed on DMI,White Basic speed HookMedium-grey basic speed hookDistance to target (digital)Release Speed Digital
            Test Step Comment: (1) Information (paragraph 1) under MMI_gen 6898 (inoperable)
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(@"Press a sensitivity area (areas A1-A4 or B) to make a Basic Speed Hook appear.Then simulate loss-communication between ETCS onboard and DMI (1 second).Note: Stopwatch is required for accuracy of test result");
            // Call generic Check Results Method
            DmiExpectedResults.DMI_displays_the_message_ATP_Down_Alarm_with_sound_alarm_Verify_the_following_information_The_objects_below_are_not_displayed_on_DMI_White_Basic_speed_HookMedium_grey_basic_speed_hookDistance_to_target_digitalRelease_Speed_Digital();
            
            
            /*
            Test Step 26
            Action: Re-establish communication between ETCS onboard and DMI (in 1 second).Note: Stopwatch is required for accuracy of test result
            Expected Result: Verify the following information,(1)   The white basic speed hook displays for 10 seconds (toggled off). Then, disappears.(2)   The objects below are still not displayed on DMI,Medium-grey basic speed hookDistance to target (digital)Release Speed Digital
            Test Step Comment: (1) MMI_gen 6890 (partly: SH mode, toggle off), MMI_gen 6896 (partly: configuration ‘TIMER’, SH mode, toggle invisible), MMI_gen 6894 (partly: SH mode);(2) MMI_gen 6890 (partly: SH mode, un-concerned object), Table 34 (CSM), Table 38 (CSM), Table 35 (CSM)
            */
            // Call generic Action Method
            DmiActions.Re_establish_communication_between_ETCS_onboard_and_DMI_in_1_second_Note_Stopwatch_is_required_for_accuracy_of_test_result();
            
            
            /*
            Test Step 27
            Action: End of test
            Expected Result: 
            */
            

            return GlobalTestResult;
        }
    }
}

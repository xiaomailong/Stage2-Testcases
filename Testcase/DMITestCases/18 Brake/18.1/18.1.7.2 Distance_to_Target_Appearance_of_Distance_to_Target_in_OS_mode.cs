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
    /// 18.1.7.2 Distance to Target: Appearance of Distance to Target in OS mode
    /// TC-ID: 13.1.7.2
    /// 
    /// This test case verifies the display information of the distance to target bar and digital in OS mode. The display of distance to target bar and digital are comply with the received packet EVC-1 and EVC-7. 
    /// 
    /// Tested Requirements:
    /// MMI_gen 2567 (partly: Table 38, OS mode); MMI_gen 107 (partly: Table 37, OS mode); MMI_gen 6658; MMI_gen 6774;
    /// 
    /// Scenario:
    /// 1.Drive the train forward pass BG1 at 100m and confirm acknowledgement of OS mode. Then, verify the display of distance to target bar and digital with received packet information EVC-1 and EVC-
    /// 7.BG1: Packet 12, 21, 27 and 80 (Entering OS)
    /// 2.Continue to drive the train forward. Then, verify the display of distance to target bar and digital when the supervision status is changed.  Note: The consistency of information for the position in each test step and the location when the value of MMI_M_WARNING changed is able to verify in log file, EVC-7 variable OBU_TR_TRAIN.
    /// 
    /// Used files:
    /// 13_1_7_2.tdg
    /// </summary>
    public class Distance_to_Target_Appearance_of_Distance_to_Target_in_OS_mode : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // System is powered on.Cabin is activated.SoM is performed in SR mode, level 1.
            
            // Call the TestCaseBase PreExecution
            base.PreExecution();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in OS mode, Level 1

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint

            
            /*
            Test Step 1
            Action: Drive the train forward pass BG1. Then, press an acknowledgement in sub-area C1 and press on sub-area B to toggle the basic speed hook become visible
            Expected Result: DMI displays in OS mode, level 1Verify the following information(1)    Use the log file to confirm that DMI receives the following packets information with a specific value,  EVC-1: MMI_M_WARNING = 0 (Status = NoS, Supervision = CSM)MMI_O_BRAKETARGET = -1 (Default) EVC-7: OBU_TR_M_MODE = 1 (OS mode) (2)   The distance to target bar is not display in sub-area A3. (3)   The distance to target digital is not display in sub-area A2
            Test Step Comment: (1) MMI_gen 107 (partly: MMI_M_WARNING, OBU_TR_M_MODE, OS mode CSM); MMI_gen 2567 (partly: MMI_M_WARNING, OBU_TR_M_MODE, OS mode CSM); MMI_gen 6658 (partly: MMI_O_BRAKETARGET is less than zero); MMI_gen 6774 (partly: MMI_O_BRAKETARGET is less than zero);(2) MMI_gen 6658 (partly: not be shown); MMI_gen 6674 (partly: not be shown); MMI_gen 107 (partly: Table 37, OS mode);(3) MMI_gen 2567 (partly: Table 38, OS mode CSM);
            */
            
            
            /*
            Test Step 2
            Action: Continue to drive the train forward.Then, stop the train
            Expected Result: Verify the following information,(1)    Use the log file to confirm that DMI receives the packet information EVC-1 with following variables,MMI_M_WARNING = 2 (Status = NoS, Supervision = PIM)(2)    The distance to target bar is not display in sub-area A3.(3)   The distance to target digital is display in sub-area A2
            Test Step Comment: (1) MMI_gen 107 (partly: MMI_M_WARNING, OS mode); MMI_gen 2567 (partly: MMI_M_WARNING, OS mode PIM); (2) MMI_gen 107 (partly: Table 37, OS mode);(3) MMI_gen 2567 (partly: Table 38, OS mode PIM);
            */
            // Call generic Action Method
            DmiActions.Continue_to_drive_the_train_forward_Then_stop_the_train();
            // Call generic Check Results Method
            DmiExpectedResults.Verify_the_following_information_1_Use_the_log_file_to_confirm_that_DMI_receives_the_packet_information_EVC_1_with_following_variables_MMI_M_WARNING_2_Status_NoS_Supervision_PIM2_The_distance_to_target_bar_is_not_display_in_sub_area_A3_3_The_distance_to_target_digital_is_display_in_sub_area_A2();
            
            
            /*
            Test Step 3
            Action: Continue to drive the train forward.Then, stop the train
            Expected Result: Verify the following information,(1)    Use the log file to confirm that DMI receives the packet information EVC-1 with following variables,MMI_M_WARNING = 11 (Status = NoS, Supervision = TSM)(2)     The distance to target bar is not display in sub-area A3.(3)    The distance to target digital is display in sub-area A2
            Test Step Comment: (1) MMI_gen 107 (partly: MMI_M_WARNING, OS mode); MMI_gen 2567 (partly: MMI_M_WARNING, OS mode TSM);(2) MMI_gen 107 (partly: Table 37, OS mode);(3) MMI_gen 2567 (partly: Table 38, OS mode TSM);
            */
            // Call generic Action Method
            DmiActions.Continue_to_drive_the_train_forward_Then_stop_the_train();
            
            
            /*
            Test Step 4
            Action: Continue to drive the train forward.Then, stop the train
            Expected Result: Verify the following information,(1)    Use the log file to confirm that DMI receives the packet information EVC-1 with following variables,MMI_M_WARNING = 3 (Status = Inds, Supervision = RSM)(2)     The distance to target bar is not display in sub-area A3.(3)    The distance to target digital is display in sub-area A2
            Test Step Comment: (1) MMI_gen 107 (partly: MMI_M_WARNING, OS mode); MMI_gen 2567 (partly: MMI_M_WARNING, OS mode RSM); (2) MMI_gen 107 (partly: Table 37, OS mode);(3) MMI_gen 2567 (partly: Table 38, OS mode RSM);
            */
            // Call generic Action Method
            DmiActions.Continue_to_drive_the_train_forward_Then_stop_the_train();
            
            
            /*
            Test Step 5
            Action: End of test
            Expected Result: 
            */
            

            return GlobalTestResult;
        }
    }
}

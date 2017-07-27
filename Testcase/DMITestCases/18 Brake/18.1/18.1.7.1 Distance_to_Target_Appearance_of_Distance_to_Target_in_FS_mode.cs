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
    /// 18.1.7.1 Distance to Target: Appearance of Distance to Target in FS mode
    /// TC-ID: 13.1.7.1
    /// 
    /// This test case verifies the display information of the distance to target bar and digital in FS mode. The display of distance to target is comply with the received packet EVC-1 and EVC-7. 
    /// 
    /// Tested Requirements:
    /// MMI_gen 6658; MMI_gen 6774; MMI_gen 107 (partly: Table 37, MMI_M_WARNING, OBU_TR_M_MODE, FS mode); MMI_gen 5817 (partly: MMI_M_WARNING = 2, sound Sinfo); MMI_gen 6758 ; MMI_gen 2567 (partly: Table 38, FS mode); MMI_gen 9516 (partly: PIM supervision); MMI_gen 12025 (partly: PIM supervision);
    /// 
    /// Scenario:
    /// 1.Drive the train forward pass BG1 at 100m. Then, verify the display of distance to target bar and digital with received packet information EVC-1 and EVC-
    /// 7.BG1: Packet 12, 21 and 27 (Entering FS)
    /// 2.Continue to drive the train forward. Then, verify the display of distance to target bar and digital when the supervision status is changed. 
    /// 3.Use the test script file to send an invalid value in EVC-1 and EVC-
    /// 7.Then, verify that the distance to target bar and digital are removed. 
    /// 4.Continue to drive the train forward. Then, verify the display of distance to target bar and digital when the supervision status is changed. Note: The consistency of information for the position in each test step and the location when the value of MMI_M_WARNING changed is able to verify in log file, EVC-7 variable OBU_TR_TRAIN.
    /// 
    /// Used files:
    /// 13_1_7_1.tdg, 13_1_7_1_a.xml, 13_1_7_1_b.xml
    /// </summary>
    public class Distance_to_Target_Appearance_of_Distance_to_Target_in_FS_mode : TestcaseBase
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
            // DMI displays in FS mode, Level 1

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint

            
            /*
            Test Step 1
            Action: Drive the train forward pass BG1
            Expected Result: DMI displays in FS mode, level 1Verify the following information(1)    Use the log file to confirm that DMI receives the following packets information with a specific value,  EVC-1: MMI_M_WARNING = 0 (Status = NoS, Supervision = CSM)MMI_O_BRAKETARGET = -1 (Default) EVC-7: OBU_TR_M_MODE = 0 (FS mode) (2)   The distance to target bar is not display in sub-area A3. (3)   The distance to target digital is not display in sub-area A2
            Test Step Comment: (1) MMI_gen 107 (partly: MMI_M_WARNING, OBU_TR_M_MODE, FS mode CSM); MMI_gen 6658 (partly: MMI_O_BRAKETARGET is less than zero); MMI_gen 2567 (partly: MMI_M_WARNING, OBU_TR_M_MODE, FS mode CSM); MMI_gen 6774 (partly: MMI_O_BRAKETARGET is less than zero);(2) MMI_gen 6658 (partly: not be shown); MMI_gen 107 (partly: Table 37, FS mode, CSM);(3) MMI_gen 2567 (partly: Table 38, FS mode CSM); MMI_gen 6774 (partly: not be shown);
            */
            // Call generic Action Method
            DmiActions.Drive_the_train_forward_pass_BG1();
            
            
            /*
            Test Step 2
            Action: Continue to drive the train forward.Then, stop the train
            Expected Result: Verify the following information,(1)    Use the log file to confirm that DMI receives the packet information EVC-1 with following variables,MMI_M_WARNING = 2 (Status = NoS, Supervision = PIM)MMI_O_BRAKETARGET > -1(2)    The distance to target bar is display in sub-area A3.(3)   The sound 'Sinfo' is played once.(4)    The distance to target digital is display in sub-area A2
            Test Step Comment: (1) MMI_gen 107 (partly: MMI_M_WARNING, FS mode PIM); MMI_gen 6658 (partly: NEGATIVE, MMI_O_BRAKETARGET is more than zero); MMI_gen 2567 (partly: MMI_M_WARNING, FS mode PIM);(2) MMI_gen 6658 (partly: NEGATIVE, shown); MMI_gen 107 (partly: Table 37, FS mode, PIM); MMI_gen 5817 (partly: MMI_M_WARNING = 2);(3) MMI_gen 5817 (partly: sound Sinfo); MMI_gen 9516 (partly: PIM supervision); MMI_gen 12025 (partly: PIM supervision);(4) MMI_gen 2567 (partly: Table 38, FS mode PIM);
            */
            // Call generic Action Method
            DmiActions.Continue_to_drive_the_train_forward_Then_stop_the_train();
            
            
            /*
            Test Step 3
            Action: Continue to drive the train forward.Then, stop the train
            Expected Result: Verify the following information,(1)    Use the log file to confirm that DMI receives the packet information EVC-1 with following variables,MMI_M_WARNING = 11 (Status = NoS, Supervision = TSM)MMI_O_BRAKETARGET > -1(2)    The distance to target bar is display in sub-area A3.(3)    The distance to target digital is display in sub-area A2
            Test Step Comment: (1) MMI_gen 107 (partly: MMI_M_WARNING, FS mode TSM); MMI_gen 6658 (partly: NEGATIVE, MMI_O_BRAKETARGET is more than zero); MMI_gen 2567 (partly: MMI_M_WARNING, FS mode TSM);(2) MMI_gen 6658 (partly: NEGATIVE, shown); MMI_gen 107 (partly: Table 37, FS mode, TSM);(3) MMI_gen 2567 (partly: Table 38, FS mode TSM);
            */
            // Call generic Action Method
            DmiActions.Continue_to_drive_the_train_forward_Then_stop_the_train();
            
            
            /*
            Test Step 4
            Action: Use the test script file 13_1_7_a.xml to send EVC-1 with,MMI_M_WARNING = 7
            Expected Result: Verify the following information,(1)   The distance to target bar and digital is removed from the DMI.Note: After test scipt file is executed, the distance to target bar and digital is re-appear refer to received packet EVC-1 from ETCS Onboard
            Test Step Comment: (1) MMI_gen 6758 (partly: MMI_M_WARNING is invalid);
            */
            // Call generic Check Results Method
            DmiExpectedResults.Verify_the_following_information_1_The_distance_to_target_bar_and_digital_is_removed_from_the_DMI_Note_After_test_scipt_file_is_executed_the_distance_to_target_bar_and_digital_is_re_appear_refer_to_received_packet_EVC_1_from_ETCS_Onboard();
            
            
            /*
            Test Step 5
            Action: Use the test script file 13_1_7_b.xml to send EVC-7 with,OBU_TR_M_MODE = 17
            Expected Result: Verify the following information,(1)   The distance to target bar and digital is removed from the DMI.Note: After test scipt file is executed, the distance to target bar and digital is re-appear refer to received packet EVC-1 from ETCS Onboard
            Test Step Comment: (1) MMI_gen 6758 (partly: OBU_TR_M_MODE is invalid);
            */
            // Call generic Check Results Method
            DmiExpectedResults.Verify_the_following_information_1_The_distance_to_target_bar_and_digital_is_removed_from_the_DMI_Note_After_test_scipt_file_is_executed_the_distance_to_target_bar_and_digital_is_re_appear_refer_to_received_packet_EVC_1_from_ETCS_Onboard();
            
            
            /*
            Test Step 6
            Action: Continue to drive the train forward.Then, stop the train
            Expected Result: Verify the following information,(1)    Use the log file to confirm that DMI receives the packet information EVC-1 with following variables,MMI_M_WARNING = 3 (Status = Inds, Supervision = RSM)(2)    The distance to target bar is display in sub-area A3.(3)    The distance to target digital is display in sub-area A2
            Test Step Comment: (1) MMI_gen 107 (partly: MMI_M_WARNING, OBU_TR_M_MODE, FS mode RSM); MMI_gen 6658 (partly: NEGATIVE, MMI_O_BRAKETARGET is more than zero); MMI_gen 2567 (partly: MMI_M_WARNING, OBU_TR_M_MODE, FS mode RSM);(2) MMI_gen 6658 (partly: NEGATIVE, shown); MMI_gen 107 (partly: Table 37, FS mode, RSM);(3) MMI_gen 2567 (partly: Table 38, FS mode RSM);
            */
            // Call generic Action Method
            DmiActions.Continue_to_drive_the_train_forward_Then_stop_the_train();
            
            
            /*
            Test Step 7
            Action: End of test
            Expected Result: 
            */
            

            return GlobalTestResult;
        }
    }
}

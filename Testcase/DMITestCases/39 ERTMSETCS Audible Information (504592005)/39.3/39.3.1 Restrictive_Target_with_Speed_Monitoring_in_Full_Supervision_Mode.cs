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
    /// 39.3.1 Restrictive Target with Speed Monitoring in Full Supervision Mode
    /// TC-ID: 36.3.1
    /// 
    /// This test case verifies that sound ‘S info’ is played once when restrictive target becomes applicable in FS mode.
    /// 
    /// Tested Requirements:
    /// MMI_gen 12070; MMI_gen 12043 (partly: speed monitoring in FS mode); MMI_gen 9516 (partly: restrictive target changed); MMI_gen 12025 (partly: restrictive target changed);
    /// 
    /// Scenario:
    /// 1.Perform SoM to Level 1 in SR mode.
    /// 2.Drive the train forward with constant speed at 40 km/h.
    /// 3.Train runs pass BG1 at position 100 m. that contained pkt 12, pkt 21 and pkt 27 to enter FS mode.
    /// 4.Stop the train.
    /// 
    /// Used files:
    /// 36_3_1.tdg
    /// </summary>
    public class Restrictive_Target_with_Speed_Monitoring_in_Full_Supervision_Mode : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // System is power on.

            // Call the TestCaseBase PreExecution
            base.PreExecution();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in FS mode, Level 1.

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint


            /*
            Test Step 1
            Action: Perform SoM to Level 1 in SR mode
            Expected Result: ETCS OB enters SR mode in Level 1
            */
            // Call generic Action Method
            DmiActions.Perform_SoM_to_Level_1_in_SR_mode();
            // Call generic Check Results Method
            DmiExpectedResults.ETCS_OB_enters_SR_mode_in_Level_1();


            /*
            Test Step 2
            Action: Drive the train forward with constant speed at 40 km/h
            Expected Result: The train can drive forward and all brakes are not applied
            */
            // Call generic Action Method
            DmiActions.Drive_the_train_forward_with_constant_speed_at_40_kmh();
            // Call generic Check Results Method
            DmiExpectedResults.The_train_can_drive_forward_and_all_brakes_are_not_applied();


            /*
            Test Step 3
            Action: Train runs pass BG1
            Expected Result: ETCS OB enters FS mode in Level 1
            Test Step Comment: Note Sound ‘Sinfo’ is played once because new text message is displayed, refers to MMI_gen 11455
            */
            // Call generic Action Method
            DmiActions.Train_runs_pass_BG1();
            // Call generic Check Results Method
            DmiExpectedResults.ETCS_OB_enters_FS_mode_in_Level_1();


            /*
            Test Step 4
            Action: Continue to drive the train forward and wait until train enters PIM supervision
            Expected Result: (1) Sound Sinfo is played once when train enters PIM in FS mode with verification as follows:-DMI Appearance- The DMI displays white colour on CSG from Vtarget to Vperm.Log FileUse log file to verify that the DMI receives EVC-1 with variable [MMI_M_WARNING = 2].(2) Use log file to verify that restrictive target becomes applicable when DMI enters PIM in FS mode by verification below:-- In packet EVC-1 that variable ‘MMI_M_WARNING’ is changed to 2, variable ‘MMI_O_BRAKETARGET’ and ‘MMI_V_TARGET’ are changed value from -1 to any value
            Test Step Comment: (1) MMI_gen 12070; MMI_gen 12043 (partly: speed monitoring in FS mode); MMI_gen 9516 (partly: restrictive target changed); MMI_gen 12025 (partly: restrictive target changed);(2) MMI_gen 12043 (partly: restrictive target becomes applicable by speed monitoring);Note DMI appearance refers to Table 33: Conditions for display and colour of the CSG in [MMI-ETCS-gen]. 
            */


            /*
            Test Step 5
            Action: Continue to drive the train forward and wait until train enters TSM
            Expected Result: (1) Sound Sinfo is not played when train enters TSM in FS mode with verification as follows:-- Use log file to verify that the DMI receives EVC-1 with variable [MMI_M_WARNING = 11].(2) Use log file to verify that restrictive target doesn’t exist when DMI enters TSM in FS mode by verification below:-- In packet EVC-1 that variable ‘MMI_M_WARNING’ is changed to 11, variable ‘MMI_O_BRAKETARGET’ and ‘MMI_V_TARGET’ are not changed value
            Test Step Comment: (1) MMI_gen 12070 (partly: negative test, no more restrictive target exists);(2) MMI_gen 12043 (partly: negative test no more restrictive target exists);Note DMI appearance refers to Table 33: Conditions for display and colour of the CSG in [MMI-ETCS-gen].
            */


            /*
            Test Step 6
            Action: Continue to drive the train forward and wait until train enters RSM
            Expected Result: (1) Sound Sinfo is not played when train enters RSM in FS mode with verification as follows:-- Use log file to verify that the DMI receives EVC-1 with variable [MMI_M_WARNING = 3].(2) Use log file to verify that restrictive target doesn’t exist when DMI enters RSM by verification below:-- In packet EVC-1 that variable ‘MMI_M_WARNING’ is changed to 3, variable ‘MMI_O_BRAKETARGET’ and ‘MMI_V_TARGET’ are not changed value
            Test Step Comment: (1) MMI_gen 12070 (partly: negative test, no more restrictive target exists);(2) MMI_gen 12043 (partly: negative test, no more restrictive target exists);Note DMI appearance refers to Table 33: Conditions for display and colour of the CSG in [MMI-ETCS-gen]. 
            */


            /*
            Test Step 7
            Action: Stop the train
            Expected Result: The train is at standstill
            */
            // Call generic Action Method
            DmiActions.Stop_the_train();
            // Call generic Check Results Method
            DmiExpectedResults.The_train_is_at_standstill();


            /*
            Test Step 8
            Action: End of test
            Expected Result: 
            */


            return GlobalTestResult;
        }
    }
}
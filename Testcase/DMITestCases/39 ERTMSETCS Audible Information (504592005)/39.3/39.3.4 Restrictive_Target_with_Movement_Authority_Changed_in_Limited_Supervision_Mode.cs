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
    /// 39.3.4 Restrictive Target with Movement Authority Changed in Limited Supervision Mode
    /// TC-ID: 36.3.4
    /// 
    /// This test case verifies that sound ‘S info’ is not played when train is in LS mode even if distance to target or target speed is changed.
    /// 
    /// Tested Requirements:
    /// MMI_gen 12043 (partly: MA changed in LS mode);
    /// 
    /// Scenario:
    /// 1.Perform SoM to Level 1 in SR mode.
    /// 2.Drive the train forward with constant speed at 20 km/h.
    /// 3.Train runs pass BG1 at position 100 m. that contained pkt 12, pkt 21, pkt 27 and pkt 80 to enter LS mode.
    /// 4.Train runs pass BG2 at position 200 m. that contained pkt 12, pkt 21, pkt 27 and pkt 80 with reduction of target speed.
    /// 5.Stop the train.
    /// 
    /// Used files:
    /// 36_3_4.tdg
    /// </summary>
    public class Restrictive_Target_with_Movement_Authority_Changed_in_Limited_Supervision_Mode : TestcaseBase
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
            // DMI displays in LS mode, Level 1.

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
            Action: Drive the train forward with constant speed at 20 km/h
            Expected Result: The train can drive forward and all brakes are not applied
            */
            // Call generic Action Method
            DmiActions.Drive_the_train_forward_with_constant_speed_at_20_kmh();
            // Call generic Check Results Method
            DmiExpectedResults.The_train_can_drive_forward_and_all_brakes_are_not_applied();


            /*
            Test Step 3
            Action: Train runs pass BG1
            Expected Result: Sound Sinfo is not played when train pass BG1 with verification as follows:-Log FileUse log file to verify that after train pass BG1, train enters LS mode and restrictive target doesn’t exist in LS mode when the DMI receives the following:-- EVC-7 with variable [MMI_OBU_TR_M_Mode = 12].- EVC-1 with variable [MMI_V_TARGET = -1] all the time.- EVC-1 with variable [MMI_O_BRAKETARGET = -1] all the time
            Test Step Comment: MMI_gen 12043 (partly: MA changed in LS mode);
            */
            // Call generic Action Method
            DmiActions.Train_runs_pass_BG1();


            /*
            Test Step 4
            Action: Train runs pass BG2
            Expected Result: Sound Sinfo is not played when train pass BG2 with verification as follows:-Log FileUse log file to verify that after train pass BG2, train is still in LS mode and restrictive target still doesn’t exist in LS mode when the DMI receives the following:-- EVC-7 with variable [MMI_OBU_TR_M_Mode = 12].- EVC-1 with variable [MMI_V_TARGET = -1] all the time.- EVC-1 with variable [MMI_O_BRAKETARGET = -1] all the time
            Test Step Comment: MMI_gen 12043 (partly: MA changed in LS mode);
            */
            // Call generic Action Method
            DmiActions.Train_runs_pass_BG2();


            /*
            Test Step 5
            Action: Stop the train
            Expected Result: The train is at standstill
            */
            // Call generic Action Method
            DmiActions.Stop_the_train();
            // Call generic Check Results Method
            DmiExpectedResults.The_train_is_at_standstill();


            /*
            Test Step 6
            Action: End of test
            Expected Result: 
            */


            return GlobalTestResult;
        }
    }
}
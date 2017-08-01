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
    /// 39.3.2 Restrictive Target with Movement Authority Changed in Full Supervision Mode
    /// TC-ID: 36.3.2
    /// 
    /// This test case verifies that sound ‘S info’ is played once when more restrictive target exits in FS mode with variable [EVC-1.MMI_O_BRAKETARGET] or [EVC-1.MMI_V_TARGET] is changed to lower value.
    /// 
    /// Tested Requirements:
    /// MMI_gen 12043 (partly: MA changed in FS mode);
    /// 
    /// Scenario:
    /// 1.Perform SoM to Level 1 in SR mode.
    /// 2.Drive the train forward with constant speed at 20 km/h.
    /// 3.Train runs pass BG1 at position 100 m. that contained pkt 12, pkt 21 and pkt 27 to enter FS mode.
    /// 4.Train runs pass BG2 at position 200 m. that contained pkt 12, pkt 21 and pkt 27 with reduction of target speed.
    /// 5.Stop the train.
    /// 
    /// Used files:
    /// 36_3_2.tdg
    /// </summary>
    public class Restrictive_Target_with_Movement_Authority_Changed_in_Full_Supervision_Mode : TestcaseBase
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
            DmiActions.Perform_SoM_to_Level_1_in_SR_mode(this);
            // Call generic Check Results Method
            DmiExpectedResults.ETCS_OB_enters_SR_mode_in_Level_1(this);


            /*
            Test Step 2
            Action: Drive the train forward with constant speed at 20 km/h
            Expected Result: The train can drive forward and all brakes are not applied
            */
            // Call generic Action Method
            DmiActions.Drive_the_train_forward_with_constant_speed_at_20_kmh(this);
            // Call generic Check Results Method
            DmiExpectedResults.The_train_can_drive_forward_and_all_brakes_are_not_applied(this);


            /*
            Test Step 3
            Action: Train runs pass BG1
            Expected Result: Sound Sinfo is played continuously after train runs pass BG1 with verification as follows:-Log FileUse log file to verify that after train pass BG1, train enters FS mode and restrictive target exists all the time when the DMI receives the following:-- EVC-7 with variable [MMI_OBU_TR_M_Mode = 0].- EVC-1 with variable ‘MMI_V_TARGET’ is changed from -1 to 1666 once.- EVC-1 with variable ‘MMI_O_BRAKETARGET’ is changed from -1 to any value and keep changing to lower value all the time
            Test Step Comment: MMI_gen 12043 (partly: restrictive target becomes applicable by MA changed)
            */
            // Call generic Action Method
            DmiActions.Train_runs_pass_BG1(this);


            /*
            Test Step 4
            Action: Train runs pass BG2
            Expected Result: Sound Sinfo is still played continuously after train runs pass BG2 with verification as follows:-Log FileUse log file to verify that after train pass BG2, train is still in FS mode and restrictive target still exists all the time when the DMI receives the following:-- EVC-7 with variable [MMI_OBU_TR_M_Mode = 0].- EVC-1 with variable ‘MMI_V_TARGET’ is changed from 1666 to 416 once.- EVC-1 with variable ‘MMI_O_BRAKETARGET’ is changed to lower value and keep changing to lower value all the time
            Test Step Comment: MMI_gen 12043 (partly: more restrictive target exists by MA changed)Note When variable [EVC-1. MMI_V_TARGET] is changed from 1666 to 416, variable [EVC-1. MMI_O_BRAKETARGET] is changed to upper value. So at that time sound Sinfo is played because of the changing of variable [EVC-1. MMI_V_TARGET]. 
            */
            // Call generic Action Method
            DmiActions.Train_runs_pass_BG2(this);


            /*
            Test Step 5
            Action: Stop the train
            Expected Result: The train is at standstill
            */
            // Call generic Action Method
            DmiActions.Stop_the_train(this);
            // Call generic Check Results Method
            DmiExpectedResults.The_train_is_at_standstill(this);


            /*
            Test Step 6
            Action: End of test
            Expected Result: 
            */


            return GlobalTestResult;
        }
    }
}
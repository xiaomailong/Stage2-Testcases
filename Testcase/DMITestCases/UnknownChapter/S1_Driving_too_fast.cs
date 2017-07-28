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
    /// Sound S1 - Driving too fast
    /// TC-ID: 36.1
    /// 
    /// This test case verifies that sound ‘S1_toofast.wav’ is played once when current train speed is exceeded permitted speed in CSM and PIM supervision.
    /// 
    /// Tested Requirements:
    /// MMI_gen 12029; MMI_gen 12060;
    /// 
    /// Scenario:
    /// 1.Perform SoM to Level 1 in SR mode.
    /// 2.Drive the train forward with speed at 40 km/h.
    /// 3.Train runs pass BG1 at position 100 m. that contained pkt 12, pkt 21 and pkt 27 to enter FS mode.
    /// 4.Accelerate the train with max acceleration (100% throttle) above permitted speed.
    /// 5.Stop the train.
    /// 6.Use XML script to send EVC-1 to the DMI.
    /// 7.Deactivate cabin A and power off the system.
    /// 8.Power on the system and perform SoM to Level 1 in SR mode.
    /// 9.Drive the train forward with speed at 40 km/h.
    /// 10.Train runs pass BG1 at position 100 m. that contained pkt 12, pkt 21 and pkt 27 to enter FS mode.
    /// 11.Accelerate the train with max acceleration (100% throttle) until speed at 95 km/h.
    /// 12.Wait until train enters PIM supervision and then increase train speed above permitted speed.
    /// 13.Stop the train.
    /// 14.Use XML script to send EVC-1 to the DMI.
    /// 
    /// Used files:
    /// 36_1.tdg, 36_1_a.xml and 36_1_b.xml
    /// </summary>
    public class S1_Driving_too_fast : TestcaseBase
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
            Action: Drive the train forward with speed at 40 km/h
            Expected Result: The train can drive forward and all brakes are not applied
            */
            // Call generic Action Method
            DmiActions.Drive_the_train_forward_with_speed_at_40_kmh();
            // Call generic Check Results Method
            DmiExpectedResults.The_train_can_drive_forward_and_all_brakes_are_not_applied();


            /*
            Test Step 3
            Action: Train runs pass BG1
            Expected Result: ETCS OB enters FS mode in Level 1
            */
            // Call generic Action Method
            DmiActions.Train_runs_pass_BG1();
            // Call generic Check Results Method
            DmiExpectedResults.ETCS_OB_enters_FS_mode_in_Level_1();


            /*
            Test Step 4
            Action: Accelerate the train with max acceleration (100% throttle) above permitted speed
            Expected Result: (1) Sound ‘S1_toofast.wav’ is played once when over-speed status in CSM supervision is active as figure below.(2) Use log file to verify that train speed is exceeded permitted supervision limit in CSM when DMI receives EVC-1 with variable [MMI_M_WARNING = 8]
            Test Step Comment: (1) MMI_gen 12029 (partly: MMI_M_WARNING = 8 with TDG file)(2) MMI_gen 12060 (partly: MMI_M_WARNING = 8 with TDG file)Note Sound file is stored in DMI_ERTMS_BL3 product in database path:/proj/ccmbkk3/mmi_v.
            */


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
            Action: Use test script 36_1_a.xml to send dynamic information via EVC-1 with:-- MMI_M_WARNING = 8- MMI_V_TRAIN = 2880- MMI_V_PERMITTED = 2777- MMI_V_INTERVENTION = 2929
            Expected Result: Sound ‘S1_toofast.wav’ is played once
            Test Step Comment: MMI_gen 12060 (partly: MMI_M_WARNING = 8 with XML script)
            */
            // Call generic Check Results Method
            DmiExpectedResults.Sound_S1_toofast_wav_is_played_once();


            /*
            Test Step 7
            Action: Deactivate cabin A and power off the system
            Expected Result: System is power off and DMI displays ‘No contact with ATP’
            */
            // Call generic Action Method
            DmiActions.Deactivate_cabin_A_and_power_off_the_system();
            // Call generic Check Results Method
            DmiExpectedResults.System_is_power_off_and_DMI_displays_No_contact_with_ATP();


            /*
            Test Step 8
            Action: Power on the system and perform SoM to Level 1 in SR mode
            Expected Result: ETCS OB enters SR mode in Level 1
            */
            // Call generic Action Method
            DmiActions.Power_on_the_system_and_perform_SoM_to_Level_1_in_SR_mode();
            // Call generic Check Results Method
            DmiExpectedResults.ETCS_OB_enters_SR_mode_in_Level_1();


            /*
            Test Step 9
            Action: Drive the train forward with speed at 40 km/h
            Expected Result: The train can drive forward and all brakes are not applied
            */
            // Call generic Action Method
            DmiActions.Drive_the_train_forward_with_speed_at_40_kmh();
            // Call generic Check Results Method
            DmiExpectedResults.The_train_can_drive_forward_and_all_brakes_are_not_applied();


            /*
            Test Step 10
            Action: Train runs pass BG1
            Expected Result: ETCS OB enters FS mode in Level 1
            */
            // Call generic Action Method
            DmiActions.Train_runs_pass_BG1();
            // Call generic Check Results Method
            DmiExpectedResults.ETCS_OB_enters_FS_mode_in_Level_1();


            /*
            Test Step 11
            Action: Accelerate the train with max acceleration (100% throttle) until speed at 95 km/h.Wait until train enters PIM supervision and then increase train speed above permitted speed
            Expected Result: (1) Sound ‘S1_toofast.wav’ is played once when over-speed status in PIM supervision is active as figure below.(2) Use log file to verify that train speed is exceeded permitted supervision limit in PIM when DMI receives EVC-1 with variable [MMI_M_WARNING = 10]
            Test Step Comment: (1) MMI_gen 12029 (partly: MMI_M_WARNING = 10 with TDG file)(2) MMI_gen 12060 (partly: MMI_M_WARNING = 10 with TDG file)
            */


            /*
            Test Step 12
            Action: Stop the train
            Expected Result: The train is at standstill
            */
            // Call generic Action Method
            DmiActions.Stop_the_train();
            // Call generic Check Results Method
            DmiExpectedResults.The_train_is_at_standstill();


            /*
            Test Step 13
            Action: Use test script 36_1_b.xml to send dynamic information via EVC-1 with:-- MMI_M_WARNING = 10- MMI_V_TRAIN = 2806- MMI_V_PERMITTED = 2687- MMI_V_INTERVENTION = 2882
            Expected Result: Sound ‘S1_toofast.wav’ is played once
            Test Step Comment: MMI_gen 12060 (partly: MMI_M_WARNING = 10 with XML script)
            */
            // Call generic Check Results Method
            DmiExpectedResults.Sound_S1_toofast_wav_is_played_once();


            /*
            Test Step 14
            Action: End of test
            Expected Result: 
            */


            return GlobalTestResult;
        }
    }
}
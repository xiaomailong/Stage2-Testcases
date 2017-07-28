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
    /// Sound S3 - End of Intervention
    /// TC-ID: 36.2
    /// 
    /// This test case verifies that sound ‘S3_end_of_intervention.wav’ is played once when intervention status is no more active if variable [EVC-1.MMI_M_WARNING] is changed from value 12 or 13 to other value.
    /// 
    /// Tested Requirements:
    /// MMI_gen 12038; MMI_gen 12039;
    /// 
    /// Scenario:
    /// 1.Perform SoM to Level 1 in SR mode.
    /// 2.Drive the train forward with speed at 40 km/h.
    /// 3.Train runs pass BG1 at position 100 m. that contained pkt 12, pkt 21 and pkt 27 to enter FS mode.
    /// 4.Accelerate the train with max acceleration (100% throttle) until train speed at 107 km/h. 
    /// 5.Stop the train.
    /// 6.Deactivate cabin A and power off the system.
    /// 7.Power on the system and perform SoM to Level 1 in SR mode.
    /// 8.Drive the train forward with speed at 40 km/h.
    /// 9.Train runs pass BG1 at position 100 m. that contained pkt 12, pkt 21 and pkt 27 to enter FS mode.
    /// 10.Accelerate the train with max acceleration (100% throttle) until train speed at 80 km/h.
    /// 11.Wait until current train speed above permitted speed and intervention status in TSM is active.
    /// 12.Stop the train.
    /// 
    /// Used files:
    /// 36_2.tdg
    /// </summary>
    public class S3_End_of_Intervention : TestcaseBase
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
            Action: Accelerate the train with max acceleration (100% throttle) until train speed at 107 km/h
            Expected Result: (1) Use the DMI appearance and log file to verify that train enters intervention status in CSM.DMI Appearance- The DMI displays dark grey colour on CSG from 0 km/h to Vperm.- The DMI displays red colour on CSG from Vperm to Vtrain.Log FileUse log file to verify that the DMI receives EVC-1 with the change value of variable ‘MMI_M_WARNING’ from value 12 to other value.(2) Sound ‘S3_end_of_intervention.wav’ is played once after emergency brake is deactivated with disappearance of symbol ST01.Note The intervention status information is deactivated as soon as there is no more emergency brake command from the speed and monitoring function, refers to section 6.5.7.2.4 in [MMI-ETCS-gen]
            Test Step Comment: (1) MMI_gen 12039 (partly: MMI_M_WARNING = 12);(2) MMI_gen 12038 (partly: MMI_M_WARNING = 12 is no more active); MMI_gen 12039 (partly: MMI_M_WARNING = 12 is no more active);Note DMI appearance refers to Table 33: Conditions for display and colour of the CSG in [MMI-ETCS-gen].
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
            Action: Deactivate cabin A and power off the system
            Expected Result: System is power off and DMI displays ‘No contact with ATP’
            */
            // Call generic Action Method
            DmiActions.Deactivate_cabin_A_and_power_off_the_system();
            // Call generic Check Results Method
            DmiExpectedResults.System_is_power_off_and_DMI_displays_No_contact_with_ATP();


            /*
            Test Step 7
            Action: Power on the system and perform SoM to Level 1 in SR mode
            Expected Result: ETCS OB enters SR mode in Level 1
            */
            // Call generic Action Method
            DmiActions.Power_on_the_system_and_perform_SoM_to_Level_1_in_SR_mode();
            // Call generic Check Results Method
            DmiExpectedResults.ETCS_OB_enters_SR_mode_in_Level_1();


            /*
            Test Step 8
            Action: Drive the train forward with speed at 40 km/h
            Expected Result: The train can drive forward and all brakes are not applied
            */
            // Call generic Action Method
            DmiActions.Drive_the_train_forward_with_speed_at_40_kmh();
            // Call generic Check Results Method
            DmiExpectedResults.The_train_can_drive_forward_and_all_brakes_are_not_applied();


            /*
            Test Step 9
            Action: Train runs pass BG1
            Expected Result: ETCS OB enters FS mode in Level 1
            */
            // Call generic Action Method
            DmiActions.Train_runs_pass_BG1();
            // Call generic Check Results Method
            DmiExpectedResults.ETCS_OB_enters_FS_mode_in_Level_1();


            /*
            Test Step 10
            Action: Accelerate the train with max acceleration (100% throttle) until train speed at 80 km/h.  And wait until emergency brake is applied from ETCS onboard
            Expected Result: (1) Use the DMI appearance and log file to verify that train enters intervention status in TSM.DMI Appearance- The DMI displays yellow colour on CSG from 0 to Vperm.- The DMI displays red colour on CSG from Vperm to Vtrain.Log FileUse log file to verify that the DMI receives EVC-1 with the change value of variable ‘MMI_M_WARNING’ from value 13 to other value.(2)  Sound ‘S3_end_of_intervention.wav’ is played once after emergency brake is deactivated with disappearance of symbol ST01. Note The intervention status information is deactivated as soon as there is no more emergency brake command from the speed and monitoring function, refers to section 6.5.7.5.2 in [MMI-ETCS-gen]
            Test Step Comment: (1) MMI_gen 12039 (partly: MMI_M_WARNING = 13); (2) MMI_gen 12038 (partly: MMI_M_WARNING = 13 is no more active); MMI_gen 12039 (partly: MMI_M_WARNING = 13 is no more active);Note DMI appearance refers to Table 33: Conditions for display and colour of the CSG in [MMI-ETCS-gen].
            */


            /*
            Test Step 11
            Action: Stop the train
            Expected Result: The train is at standstill
            */
            // Call generic Action Method
            DmiActions.Stop_the_train();
            // Call generic Check Results Method
            DmiExpectedResults.The_train_is_at_standstill();


            /*
            Test Step 12
            Action: End of test
            Expected Result: 
            */


            return GlobalTestResult;
        }
    }
}
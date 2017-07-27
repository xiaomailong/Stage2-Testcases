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
    /// 29.1 UTC time and offset time(by driver)
    /// TC-ID: 29.1
    /// 
    /// This test case is to verify the UTC time/ Offset time changed by driver. It shall comply with [ERA] standard and [MMI-ETCS-gen].
    /// 
    /// Tested Requirements:
    /// MMI_gen 2432; MMI_gen 2421 (partly: source by Driver);
    /// 
    /// Scenario:
    /// 1.Power on test system and activate cabin.
    /// 2.Perform Start of Mission to L1, SR mode
    /// 3.Select ‘Settings’ button then ‘Settings’ window is opened.
    /// 4.Select ‘Set Clock’ button  then chage Date and time and verify the changes shall be transmited to ETCS OB by packet EVC-109 to ETCS OB.
    /// 
    /// Used files:
    /// N/A
    /// </summary>
    public class UTC_time_and_offset_timeby_driver : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // Power off test systemSet the following tags name in configuration file (See the instruction in Appendix 1)CLOCK_TIME_SOURCE = 1 (by driver)
            
            // Call the TestCaseBase PreExecution
            base.PreExecution();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays SR mode, Level 1

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint

            
            /*
            Test Step 1
            Action: Power on the system and activate cabin
            Expected Result: DMI displays SB mode
            */
            // Call generic Action Method
            DmiActions.Power_on_the_system_and_activate_cabin();
            // Call generic Check Results Method
            DmiExpectedResults.DMI_displays_SB_mode();
            
            
            /*
            Test Step 2
            Action: Perform SoM to L1, SR mode
            Expected Result: Mode changes to SR mode , L1
            */
            // Call generic Action Method
            DmiActions.Perform_SoM_to_L1_SR_mode();
            // Call generic Check Results Method
            DmiExpectedResults.Mode_changes_to_SR_mode_L1();
            
            
            /*
            Test Step 3
            Action: Select ‘Settings’ button and press ‘Set clock’ button
            Expected Result: The set clock window is opened
            */
            // Call generic Action Method
            DmiActions.Select_Settings_button_and_press_Set_clock_button();
            
            
            /*
            Test Step 4
            Action: Change Data and Time to be different  from current values for example:Year = current year +1Month = current month +2Day = current day +3Hour = current hour +4Minute = current minute +5Second =current second +6Offset = current offset + 12
            Expected Result: Verify the following information,The new UTC time and offset time are changed and displayed according to the entered data from the driver.Use the log file to verify that DMI sends out packet EVC-109 to ETCS OB correctly
            Test Step Comment: MMI_gen 2432;MMI_gen 2421 (partly: source by Driver);
            */
            // Call generic Check Results Method
            DmiExpectedResults.Verify_the_following_information_The_new_UTC_time_and_offset_time_are_changed_and_displayed_according_to_the_entered_data_from_the_driver_Use_the_log_file_to_verify_that_DMI_sends_out_packet_EVC_109_to_ETCS_OB_correctly();
            
            
            /*
            Test Step 5
            Action: Change Data and Time to be different  from current values for example:Year = curtrent year +1Month = current month +2Day = current day +3Hour = current hour +4Minute = current minute +5Second =current second +6Offset = current offset - 12
            Expected Result: Verify the following information,The new UTC time and offset time are changed and displayed according to the entered data from the driver.Use the log file to verify that DMI sends out packet EVC-109 to ETCS OB correctly
            Test Step Comment: MMI_gen 2432;MMI_gen 2421 (partly: source by Driver);
            */
            // Call generic Check Results Method
            DmiExpectedResults.Verify_the_following_information_The_new_UTC_time_and_offset_time_are_changed_and_displayed_according_to_the_entered_data_from_the_driver_Use_the_log_file_to_verify_that_DMI_sends_out_packet_EVC_109_to_ETCS_OB_correctly();
            
            
            /*
            Test Step 6
            Action: End of test
            Expected Result: 
            */
            

            return GlobalTestResult;
        }
    }
}

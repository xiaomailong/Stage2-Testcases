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
    /// 29.3 UTC time and offset time(By VAP acting as NTP server)
    /// TC-ID: 29.3
    /// 
    /// This test case is to verify the UTC time/Offset time changed by time source: VAP acting as NTP server and it complies with [ERA] standard and [MMI-ETCS-gen].
    /// 
    /// Tested Requirements:
    /// MMI_gen 12062; MMI_gen 2421 (partly: VAP acting as NTP server); MMI_gen 11284; MMI_gen 58;
    /// 
    /// Scenario:
    /// 1.Power on test system and activate cabin A (MMI 1)
    /// 2.Perform Start of Mission to L1, SR mode.
    /// 3.Use test script file to send data packet of UTC time/Offset time to DMI.
    /// 4.Verify that DMI time is updated
    /// 5.Select ‘Settings’ button then ‘Settings’ window is opened.
    /// 6.Select ‘Set Clock’ button and verify date and time that display on DMI.
    /// 7.Restart OTE and retest step 1-6 with Cabin B (MMI 2)
    /// 
    /// Used files:
    /// 29_3_1.xml, 29_3_2.xml.
    /// </summary>
    public class UTC_time_and_offset_timeBy_VAP_acting_as_NTP_server : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // 1.  Power off test system 2.  Set the following tags name in configuration file (See the instruction in Appendix 1)CLOCK_TIME_SOURCE =  5 (by VAP)

            // Call the TestCaseBase PreExecution
            base.PreExecution();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays SR mode.

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint


            /*
            Test Step 1
            Action: Power on test system and activate the cabin A (MMI 1)
            Expected Result: DMI displays SB mode
            */
            // Call generic Check Results Method
            DmiExpectedResults.SB_mode_displayed(this);


            /*
            Test Step 2
            Action: Perform SoM to L1, SR mode
            Expected Result: Mode changes to SR mode
            */
            // Call generic Action Method
            DmiActions.Perform_SoM_to_L1_SR_mode(this);


            /*
            Test Step 3
            Action: Use the test script file 29_3_1.xml to send packet EVC-TBD with,MMI_T_UTC = 946728000(2000-01-01,12:00:00)MMI_T_Zone_OFFSET =+5
            Expected Result: Verify the following information:(1) DMI time is updated only its offset time
            Test Step Comment: (1) MMI_gen 12062; MMI_gen 2421 (partly: VAP acting as NTP server);MMI_gen 11284;Note: ‘EVC-TBD’ will be provided by VSIS in order to support VAP interface 
            */
            // Call generic Check Results Method
            DmiExpectedResults.Verify_the_following_information1_DMI_time_is_updated_only_its_offset_time(this);


            /*
            Test Step 4
            Action: Select ‘Settings’ button and press ‘Set clock’ button
            Expected Result: Verify the following information:The set clock window is opened(1) The DMI date (yyyy-mm-dd) shall not be changed by test script
            Test Step Comment: (1) MMI_gen 12062; MMI_gen 11284;
            */
            // Call generic Action Method
            DmiActions.Select_Settings_button_and_press_Set_clock_button(this);


            /*
            Test Step 5
            Action: Use the test script file 29_3_2.xml to send packet EVC-TBD with,MMI_T_UTC = 946771200(2000-01-02,12:00:00)MMI_T_Zone_OFFSET =-5
            Expected Result: Verify the following information:(1) DMI time is updated only its offset time
            Test Step Comment: (1) MMI_gen 12062; MMI_gen 2421 (partly: VAP acting as NTP server);MMI_gen 11284;Note: ‘EVC-TBD’ will be provided by VSIS in order to support VAP interface
            */
            // Call generic Check Results Method
            DmiExpectedResults.Verify_the_following_information1_DMI_time_is_updated_only_its_offset_time(this);


            /*
            Test Step 6
            Action: Select ‘Settings’ button and press ‘Set clock’ button.Then, observe information that will display on the Set clock window
            Expected Result: Verify the following information:The set clock window is opened(1) The DMI date (yyyy-mm-dd) is not be changed by test script
            Test Step Comment: (1) MMI_gen 12062; MMI_gen 11284;
            */


            /*
            Test Step 7
            Action: Restart OTE and DMI then retest step1 to to 6 with cabin B (MMI 2)
            Expected Result: Same as the test result in step 1 to 6
            Test Step Comment: MMI_gen 58;
            */


            /*
            Test Step 8
            Action: End of test
            Expected Result: 
            */


            return GlobalTestResult;
        }
    }
}
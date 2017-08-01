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
    /// 17.13 Slide Indication
    /// TC-ID: 12.13
    /// 
    /// This test case verifies the display of the ‘Slide’ indication with ETC speed which complies with conditions of  [MMI-ETCS-gen].
    /// 
    /// Tested Requirements:
    /// MMI_gen 1694; MMI_gen 1695; MMI_gen 1692 (partly: ETC speed, slide); MMI_gen 1079 (partly: slide); MMI_gen 7013 (partly: slide); MMI_gen 1696 (partly: slide); MMI_gen 7012 (partly: slide); MMI_gen 1693; MMI_gen 9516 (partly: slide indication); MMI_gen 12025 (partly: slide indication);
    /// 
    /// Scenario:
    /// SoM is completed in SR mode, Level 1 and SLIDE_SPEEDMETER is configured to be 1 (display)At 100 m, pass BG1 with pkt 12, pkt 21 and pkt 
    /// 27.Mode changes to FS mode.The ‘Slip/Slide’ indication is verified by the following cases:ATP disable MMI_M_SLIP and MMI_M_SLIDEATP enable MMI_M_SLIP and MMI_M_SLIDEATP enable MMI_M_SLIP but disable MMI_M_SLIDEATP disable MMI_M_SLIP bu enable MMI_M_SLIDEThe train is stopped.
    /// 
    /// Used files:
    /// 12_13.tdg, 12_3_a.xml, 12_3_b.xml, 12_3_c.xml
    /// </summary>
    public class Slide_Indication : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // Set the following tags name in configuration file (See the instruction in Appendix 1)SLIP_SPEEDMETER = 0SLIDE_SPEEDMETER = 1Test system is powered onCabin is activeSoM is completed in SR mode, Level 1

            // Call the TestCaseBase PreExecution
            base.PreExecution();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in FS mode, level 1.

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint


            /*
            Test Step 1
            Action: Driver the train forward
            Expected Result: DMI changes from SR mode to FS mode
            */
            // Call generic Action Method
            DmiActions.Driver_the_train_forward(this);
            // Call generic Check Results Method
            DmiExpectedResults.DMI_changes_from_SR_mode_to_FS_mode(this);


            /*
            Test Step 2
            Action: Drive the train forward with speed = 140 km/h
            Expected Result: The speed pointer is displayed with speed =140.Verify the following information,Use the log file to confirm that DMI receives EVC-1 with following variables,MMI_M_SLIPE = 0MMI_M_SLIDE = 0
            Test Step Comment: (1) MMI_gen 1694 (partly: slip is not set), MMI_gen 1695 (partly: slide is not set), MMI_gen 1692 (partly: ETC speed, slide);
            */
            // Call generic Action Method
            DmiActions.Drive_the_train_forward_with_speed_140_kmh(this);


            /*
            Test Step 3
            Action: Use the test script file 12_13_a.xml to send EVC-1 with,MMI_M_SLIP = 0MMI_M_SLIDE =1
            Expected Result: Verify the following information,The Slide indication is displayed and shown as arrow pointing counterclockwise.The colour of Slide indication is black as same as speed digital colour.The Slide indication is displayed on speed hub of the speed pointer.DMI plays sound Sinfo once
            Test Step Comment: (1) MMI_gen 1079 (partly: slide, presented),   MMI_gen 1695(partly: slide is set), MMI_gen 1694(partly: slip is not set), MMI_gen 1692 (partly: ETC speed, slide);   (2) MMI_gen 7013(partly: slide);(3) MMI_gen 1696(partly: slide);(4) MMI_gen 7012(partly: slide); MMI_gen 9516 (partly: slide indication); MMI_gen 12025 (partly: slide indication);
            */


            /*
            Test Step 4
            Action: Use the test script file 12_13_b.xml to send EVC-1 with,MMI_M_SLIP = 1MMI_M_SLIDE =0
            Expected Result: Verify the following information,The ‘Slip/Slide’ indication is not displayed on the speed hub. Sound Sinfo is not played
            Test Step Comment: (1) MMI_gen 1079 (partly: slide, presented),   MMI_gen 1694(partly: slip is set), MMI_gen 1695(partly: slide is not set), MMI_gen 1692 (partly: ETC speed);   (2) MMI_gen 7012(partly: no indication)
            */
            // Call generic Check Results Method
            DmiExpectedResults
                .Verify_the_following_information_The_SlipSlide_indication_is_not_displayed_on_the_speed_hub_Sound_Sinfo_is_not_played(this);


            /*
            Test Step 5
            Action: Use the test script file 12_13_c.xml to send EVC-1 with,MMI_M_SLIP = 1MMI_M_SLIDE =1
            Expected Result: Verify the following information,The ‘Slip/Slide’ indication is not displayed on the speed hub. Sound Sinfo is not played
            Test Step Comment: (1) MMI_gen 1079 (partly: slide, presented),   MMI_gen 1694(partly: slip is set), MMI_gen 1695(partly: slide is set), MMI_gen 1693 (partly: under configuration), MMI_gen 1692 (partly: ETC speed);(2) MMI_gen 7012(partly: no indication);
            */
            // Call generic Check Results Method
            DmiExpectedResults
                .Verify_the_following_information_The_SlipSlide_indication_is_not_displayed_on_the_speed_hub_Sound_Sinfo_is_not_played(this);


            /*
            Test Step 6
            Action: End of test
            Expected Result: 
            */


            return GlobalTestResult;
        }
    }
}
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
using Testcase.Telegrams.EVCtoDMI;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 17.12 Slip Indication
    /// TC-ID: 12.12
    /// 
    /// This test case verifies the display of the ‘Slip’ indication which complies with conditions of  [MMI-ETCS-gen].
    /// 
    /// Tested Requirements:
    /// MMI_gen 1694; MMI_gen 1695; MMI_gen 1692 (partly: ETC speed, slip); MMI_gen 1079 (partly: slip); MMI_gen 7013 (partly: slip); MMI_gen 1696 (partly: slip); MMI_gen 7012 (partly: slip); MMI_gen 1693; MMI_gen 9516 (partly: slip indication); MMI_gen 12025 (partly: slip indication);
    /// 
    /// Scenario:
    /// SoM is completed in SR mode, Level 1 and SLIP_SPEEDMETER is configured to be 1 (display)At 100 m, pass BG1 with pkt 12, pkt 21 and pkt 
    /// 27.Mode changes to FS mode.The ‘Slip’Slide’ indication is verified by the following cases:ATP disable MMI_M_SLIP and MMI_M_SLIDEATP enable MMI_M_SLIP and MMI_M_SLIDEATP enable MMI_M_SLIP but disable MMI_M_SLIDEATP disable MMI_M_SLIP bu enable MMI_M_SLIDEThe train is stopped.
    /// 
    /// Used files:
    /// 12_12.tdg, 12_12_a.xml, 12_12_b.xml, 12_12_c.xml
    /// </summary>
    public class Slip_Indication : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // Set the following tags name in configuration file (See the instruction in Appendix 1)SLIP_SPEEDMETER = 1SLIDE_SPEEDMETER = 0Test system is powered onCabin is activeSoM is completed in SR mode, Level 1.

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

            EVC1_MMIDynamic.Initialise(this);

            /*
            Test Step 1
            Action: Drive the train forward
            Expected Result: DMI changes from SR mode to FS mode
            */
            // Call generic Action Method
            DmiActions.Drive_the_train_forward(this);

            // Call generic Check Results Method
            DmiExpectedResults.DMI_changes_from_SR_mode_to_FS_mode(this);
            
            /*
            Test Step 2
            Action: Drive the train forward with speed = 140 km/h
            Expected Result: The speed pointer is displayed with speed =140.Verify the following information,Use the log file to confirm that DMI received packet EVC-1 with the following variables,MMI_M_SLIP = 0MMI_M_SLIDE = 0
            Test Step Comment: (1) MMI_gen 1694(partly: slip is not set), MMI_gen 1695(partly: slide is not set), MMI_gen 1692 (partly: ETC speed, slip);   
            */
            // Call generic Action Method
            DmiActions.Drive_the_train_forward_with_speed_140_kmh(this);

            /*
            Test Step 3
            Action: Use the test script file 12_12_a.xml to send EVC-1 with,MMI_M_SLIP = 1MMI_M_SLIDE = 0
            Expected Result: Verify the following information,The Slip indication is displayed and shown as arrow pointing clockwise.The colour of Slip indication is displayed as same as speed digital colour. The Slip indication is displayed on speed hub of the speed pointer. DMI plays sound Sinfo once
            Test Step Comment: (1) MMI_gen 1079 (partly: slip, presented),   MMI_gen 1694(partly: slip is set), MMI_gen 1695(partly: slide is not set), MMI_gen 1692 (partly: ETC speed, slip);   (2) MMI_gen 7013(partly: slip);(3) MMI_gen 1696 (partly:slip);(4) MMI_gen 7012 (partly: slip); MMI_gen 9516 (partly: slip indication); MMI_gen 12025 (partly: slip indication);
            */
            EVC1_MMIDynamic.MMI_M_SLIP = 1;
            // ?? Send

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The Slip indication is displayed and shown as arrow pointing clockwise." + Environment.NewLine +
                                "2. The colour of Slip indication is displayed the same as speed digital colour." + Environment.NewLine +
                                "3. The Slip indication is displayed on the speed hub of the speed pointer." + Environment.NewLine +
                                "4. DMI plays sound Sinfo once.");

            /*
            Test Step 4
            Action: Use the test script file 12_12_b.xml to send EVC-1 with,MMI_M_SLIP = 0MMI_M_SLIDE =1
            Expected Result: Verify the following information,The ‘Slip/Slide’ indication is not displayed on the speed hub
            Test Step Comment: (1) MMI_gen 1079 (partly: slip, presented),   MMI_gen 1694(partly: slip is not set), MMI_gen 1695(partly: slide is set), MMI_gen 1692 (partly: ETC speed);   
            */
            EVC1_MMIDynamic.MMI_M_SLIP = 0;
            EVC1_MMIDynamic.MMI_M_SLIDE = 1;
            // ?? Send

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Slip / Slide’ indication is not displayed on the speed hub.");

            /*
            Test Step 5
            Action: Use the test script file 12_12_c.xml to send EVC-1 with,MMI_M_SLIP = 1MMI_M_SLIDE =1
            Expected Result: Verify the following information,The ‘Slip’ indication is displayed on the speed hub as a clockwise arrow
            Test Step Comment: (1) MMI_gen 1079 (partly: slip, presented),   MMI_gen 1694(partly: slip is set), MMI_gen 1695(partly: slide is set), MMI_gen 1693, MMI_gen 1692 (partly: ETC speed, slip);
            */
            EVC1_MMIDynamic.MMI_M_SLIP = 1;
            EVC1_MMIDynamic.MMI_M_SLIDE = 1;
            // ?? Send

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Slip’ indication is displayed on the speed hub as a clockwise arrow.");
            
            /*
            Test Step 6
            Action: Stop the train
            Expected Result: Train is stand still
            */
            // Call generic Action Method
            DmiActions.Stop_the_train(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The speed is indicated as 0");

            /*
            Test Step 7
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
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
    /// 17.14 Slip and Slide are configure to 1 at the same time
    /// TC-ID: 12.14
    /// 
    /// This test case verifies the display of the ‘Slip/Slide’ indication when both of indication are configured enabled with ETC speed.
    /// 
    /// Tested Requirements:
    /// MMI_gen 1693;
    /// 
    /// Scenario:
    /// SoM is completed in SR mode, Level 1 and  SLIP_SPEEDMETER & SLIDE_SPEEDMETER are configured to be 1 (display)At 100 m, pass BG1 with pkt 12, pkt 21 and pkt 
    /// 27.Mode changes to FS mode.The ‘Slip/Slide’ indication is verified by the following cases:ATP disable MMI_M_SLIP and MMI_M_SLIDEATP enable MMI_M_SLIP and MMI_M_SLIDEATP enable MMI_M_SLIP but disable MMI_M_SLIDEATP disable MMI_M_SLIP butenable MMI_M_SLIDEThe train is stopped.
    /// 
    /// Used files:
    /// 12_14.tdg, 12_14_a.xml, 12_14_b.xml, 12_14_c.xml
    /// </summary>
    public class Slip_and_Slide_are_configure_to_1_at_the_same_time : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // Set the following tags name in configuration file (See the instruction in Appendix 1)SLIP_SPEEDMETER = 1SLIDE_SPEEDMETER = 1Test system is powered onCabin is activeSoM is completed in SR mode, Level 1.

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

            EVC7_MMIEtcsMiscOutSignals.Initialise(this);
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.FullSupervision;

            EVC1_MMIDynamic.Initialise(this);

            DmiActions.Complete_SoM_L1_SR(this);

            /*
            Test Step 1
            Action: Driver the train forward
            Expected Result: DMI changes from SR mode to FS mode
            */
            // Call generic Action Method
            DmiActions.Driver_the_train_forward(this);

            // EVC7_MMIEtcsMiscOutSignals Send

            // Call generic Check Results Method
            DmiExpectedResults.DMI_changes_from_SR_to_FS_mode(this);

            /*
            Test Step 2
            Action: Drive the train forward with speed = 140 km/h
            Expected Result: The speed pointer is displayed with speed =140
            */
            // Call generic Action Method
            DmiActions.Drive_the_train_forward_with_speed_140_kmh(this);

            // Call generic Check Results Method
            DmiExpectedResults.The_speed_pointer_is_displayed_with_speed_140(this);

            /*
            Test Step 3
            Action: Use the test script file 12_14_a.xml to send EVC-1 with,MMI_M_SLIP = 1MMI_M_SLIDE = 0
            Expected Result: The Slip indication is displayed and shown as arrow pointing clockwise
            */
            EVC1_MMIDynamic.MMI_M_SLIP = 1;
            EVC1_MMIDynamic.MMI_M_SLIDE = 0;
            // ?? Send

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The Slip indication is displayed and shown as arrow pointing clockwise.");

            /*
            Test Step 4
            Action: Use the test script file 12_14_b.xml to send EVC-1 with,MMI_M_SLIP = 0MMI_M_SLIDE = 1
            Expected Result: The Slide indication is displayed and shown as arrow pointing counterclockwise
            */
            EVC1_MMIDynamic.MMI_M_SLIP = 0;
            EVC1_MMIDynamic.MMI_M_SLIDE = 1;
            // ?? Send

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The Slip indication is displayed and shown as arrow pointing clockwise.");

            /*
            Test Step 5
            Action: Use the test script file 12_14_c.xml to send EVC-1 with,MMI_M_SLIP = 1MMI_M_SLIDE = 1
            Expected Result: Verify the following information,The Slip indication is displayed and shown as arrow pointing clockwise
            Test Step Comment: (1) MMI_gen 1693;
            */
            EVC1_MMIDynamic.MMI_M_SLIP = 1;
            EVC1_MMIDynamic.MMI_M_SLIDE = 1;
            // ?? Send

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. (1)	The Slip indication is displayed and shown as arrow pointing clockwise.");

            /*
            Test Step 6
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
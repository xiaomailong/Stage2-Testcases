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
    /// 18.1.2 Distance to Target  Digital: General Appearance
    /// TC-ID: 13.1.2
    /// 
    /// This test case verifies the general appearance and properties of distance to target digital in sub-area A2.The distance to target digital shall comply with [ERA] standard and [MMI-ETCS-gen].
    /// 
    /// Tested Requirements:
    /// MMI_gen 6767; MMI_gen 6770; MMI_gen 6772; MMI_gen 6774; MMI_gen 2567 (partly: Table 38 'FS mode with PIM', 'TR mode', 'PT mode');
    /// 
    /// Scenario:
    /// Drive the train forward pass BG1 at 100m. Then, verify the display of distance to target digital in sub-area A2.BG1: Packet 12, 21 and 27 (Entering FS mode)Drive the train forward pass EOA (600m). Then, verify that distance to target digital is not shown in sub-area A2 and correspond to the received packet information EVC-1 and EVC-7.Acknowledge Trip mode. Then, verify that distance to target digital is not shown in sub-area A2 and correspond to the received packet information EVC-1 and EVC-7.
    /// 
    /// Used files:
    /// 13_1_2.tdg
    /// </summary>
    public class Distance_to_Target__Digital_General_Appearance : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // System is powered on.Cabin is activatedSoM is performed in SR mode, level 1.

            // Call the TestCaseBase PreExecution
            base.PreExecution();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in PT mode, level 1.

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint


            /*
            Test Step 1
            Action: Drive the train forward passing BG1 until entering FS mode.
            Expected Result: Verify the following information,The distance to target digital is displayed in sub-area A2.The distance to target digital is vertically centered in sub-area A2.The distance to target digital is displayed in grey.Use the log file to confirm that DMI receives packet information EVC-1 and EVC-7 with following variables,OBU_TR_M_MODE (EVC-7) = 0 (Full Supervision)MMI_M_WARNING (EVC-1) = 2 (Status = NoS, Supervision = PIM)
            Test Step Comment: (1) MMI_gen 6767;(2) MMI_gen 6770;(3) MMI_gen 6772;(4) MMI_gen 2567 (partly: Table 38 'FS mode with PIM');
            */

            /*
            Test Step 2
            Action: Continue to drive the train forward pass EOA until entering TR mode.
            Expected Result: Verify the following information,The distance to target digital is not shown in sub-area A2.Use the log file to confirm that DMI received packet information EVC-1 with vairable MMI_O_BRAKETARGET < 0.Use the log file to confirm that DMI received packet information EVC-7 with following variables,OBU_TR_M_MODE (EVC-7) = 7 (Trip)
            Test Step Comment: (1) MMI_gen 6774 (partly: when MMI_O_BRAKETARGET less than zero);(2) MMI_gen 6774 (partly: received MMI_O_BRAKETARGET less than zero);(3) MMI_gen 2567 (partly: Table 38 ‘TR mode’);
            */

            /*
            Test Step 3
            Action: Stop the train.Then, acknowledge TR mode by press a sub-area C1.
            Expected Result: DMI displays in PT mode, Level 1.Verify the following information,The distance to target digital is not shown in sub-area A2.Use the log file to confirm that DMI received packet information EVC-1 with vairable MMI_O_BRAKETARGET < 0.Use the log file to confirm that DMI received packet information EVC-7 with following variables,OBU_TR_M_MODE (EVC-7) = 8 (Post Trip)
            Test Step Comment: (1) MMI_gen 6774 (partly: when MMI_O_BRAKETARGET less than zero);(2) MMI_gen 6774 (partly: received MMI_O_BRAKETARGET is less than zero);(3) MMI_gen 2567 (partly: Table 38 'PT mode');
            */

            /*
            Test Step 4
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
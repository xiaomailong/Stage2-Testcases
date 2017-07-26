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
    /// 20.2.5 ETCS Level: ETCS Level Transitions by receiving data packet from ETCS Onboard (L2->L3, L3->L2)
    /// TC-ID: 15.2.5
    /// 
    /// This test case verifes a symbol which should be display immediately after passed the level transition area.
    /// 
    /// Tested Requirements:
    /// MMI_gen 7025 (partly: 2nd bullet, #4); MMI_gen 9430 (partly: LE12, LE14);
    /// 
    /// Scenario:
    /// Drive the train forward pass BG1 and receives MA with transition order from RBC. Then, verifie the display information.BG1: No packet, RBC: Message 3 (with optional packet 15, 21, 27 and 41)Drive the train forward pass BG2 and receives MA with transition order from RBC. Then, verifie the display information.BG2: No packet, RBC: Message 3 (with optional packet 15, 21, 27 and 41)
    /// 
    /// Used files:
    /// 15_2_5.tdg, 15_2_5.utt
    /// </summary>
    public class
        ETCS_Level_ETCS_Level_Transitions_by_receiving_data_packet_from_ETCS_Onboard_L2_L3__L3_L2 : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // -     System is power ON.-     SoM is performed in SR mode, Level 2

            // Call the TestCaseBase PreExecution
            base.PreExecution();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in FS mode, Level 2

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint


            /*
            Test Step 1
            Action: Drive the train forward pass BG1.
            Expected Result: Verify the following information,(1)     DMI display symbol LE14 in sub-area C1.(2)    Use the log file to confirm that DMI receives packet information EVC-8 with the following variables,MMI_Q_TEXT = 276MMI_Q_TEXT_CRITIRIA = 3MMI_N_TEXT = 1MMI_X_TEXT = 3
            Test Step Comment: (1) MMI_gen 9430 (partly: LE14);(2) MMI_gen 7025 (partly: 2nd bullet, #4, non-Ack Level 3 transition);
            */

            /*
            Test Step 2
            Action: Drive the train forward pass a distance to level transition.
            Expected Result: Level transition from Level 2 to Level 3.
            */

            /*
            Test Step 3
            Action: Drive the train forward pass BG2.
            Expected Result: Verify the following information,(1)     DMI display symbol LE12 in sub-area C1.(2)    Use the log file to confirm that DMI receives packet information EVC-8 with the following variables,MMI_Q_TEXT = 276MMI_Q_TEXT_CRITIRIA = 3MMI_N_TEXT = 1MMI_X_TEXT = 2
            Test Step Comment: (1) MMI_gen 9430 (partly: LE12);(2) MMI_gen 7025 (partly: 2nd bullet, #4, non-Ack Level 2 transition);
            */

            /*
            Test Step 4
            Action: Drive the train forward pass a distance to level transition.
            Expected Result: Level transition from Level 3 to Level 2.
            */

            /*
            Test Step 5
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
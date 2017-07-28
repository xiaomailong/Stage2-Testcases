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
    /// 20.2.10 ETCS Level: ETCS Level Transitions by receiving data packet from ETCS Onboard (L0->LNTC)
    /// TC-ID: 15.2.10
    /// 
    /// This test case verifies the symbol that displays after passed the level transition announcement from Level 0 to level NTC and then acknowledgement by driver 
    /// 
    /// Tested Requirements:
    /// MMI_gen 9430 (partly: LE08); MMI_gen 9431 (partly: LE08 and LE09);
    /// 
    /// Scenario:
    /// 1.Perform start of mission to Unfitted mode, level 
    /// 02.Drive the train forward pass BG0 at position 10m with Pkt 41: level transition announcement to ATB STM. Verify that LE08 symbol displays in sub-area C1.
    /// 3.Pass the acknowledgement area. Then, verify that LE09 symbol is displayed in sub-area C
    /// 14.Acknowledge the level transition. Then, verify that LE08 is displayed in sub-area C1 
    /// 5.Pass BG1 at position 400m which is the level transition border then mode changes to ATB STM, Level NTC
    /// 
    /// Used files:
    /// 15_2_10.tdg
    /// </summary>
    public class ETCS_Level_ETCS_Level_Transitions_by_receiving_data_packet_from_ETCS_Onboard_L0_LNTC : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // System is power OFF.Configure atpcu configuration file as following (See the instruction in Appendix 2)M_InstalledLevels = 31NID_NTC_Installed_0 = 1 (ATB)

            // Call the TestCaseBase PreExecution
            base.PreExecution();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in ATB STM mode, Level NTC

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint


            /*
            Test Step 1
            Action: Perform the following action:         Power on the systemActivate the cabin Perform start of mission to Unfitted mode , Level 0
            Expected Result: DMI displays Unfitted mode, Level 0
            */


            /*
            Test Step 2
            Action: Drive the train forward with 30 km/h then pass BG0 with level transition announcement
            Expected Result: DMI displays LE08 symbol in sub-area C1
            Test Step Comment: MMI_gen 9430 (partly:Negative LE08); 
            */
            // Call generic Action Method
            DmiActions.Drive_the_train_forward_with_30_kmh_then_pass_BG0_with_level_transition_announcement();


            /*
            Test Step 3
            Action: Pass the level transition acknowledgement area
            Expected Result: DMI displays LE09 symbol in sub-area C1
            Test Step Comment: MMI_gen 9431 (partly: LE09); 
            */
            // Call generic Action Method
            DmiActions.Pass_the_level_transition_acknowledgement_area();


            /*
            Test Step 4
            Action: Press acknowledgement LE09 symbol in sub-area C1
            Expected Result: DMI replaces LE09 symbol with LE08 in sub-area C1
            Test Step Comment: MMI_gen 9431 (partly: LE08);
            */


            /*
            Test Step 5
            Action: Pass BG1 at level transition border
            Expected Result: Mode changes to ATB STM mode, Level NTC
            */
            // Call generic Action Method
            DmiActions.Pass_BG1_at_level_transition_border();


            /*
            Test Step 6
            Action: End of test
            Expected Result: 
            */


            return GlobalTestResult;
        }
    }
}
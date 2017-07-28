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
    /// 20.2.12 ETCS Level: ETCS Level Transitions by receiving data packet from ETCS Onboard (LNTC ->L2)
    /// TC-ID: 15.2.12
    /// 
    /// This test case verifies the symbol that displays after passed the level transition announcement for level NTC to level 2 and acknowledgement by driver 
    /// 
    /// Tested Requirements:
    /// MMI_gen 9430 (partly: LE12); MMI_gen 9431 (partly: LE12 and LE13); MMI_gen 11470 (partly: Bit #8);
    /// 
    /// Scenario:
    /// 1.Perform start of mission to ATB STM mode, level NTC
    /// 2.Drive the train forward pass BG0 with Pkt 41: level transition announcement to Level 
    /// 2.Verify that LE12 symbol displays in sub-area C1.
    /// 3.Pass the acknowledgement area. Then, verify that LE13 symbol is displayed in sub-area C
    /// 14.Acknowledge the level transition and then verify that LE12 is displayed in sub-area C1 
    /// 5.Pass BG1 at position 400m which is level transition border. Then, mode changes to FS mode, Level 2
    /// 
    /// Used files:
    /// 15_2_12.tdg, 15_2_12.utt 
    /// </summary>
    public class ETCS_Level_ETCS_Level_Transitions_by_receiving_data_packet_from_ETCS_Onboard_LNTC_L2 : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // System is power OFF.Configure atpcu configuration file as following (See the instruction in Appendix 2)M_InstalledLevels = 31NID_NTC_Installe_0 = 1 (ATB) 

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
            Action: Perform the following action:         Power on the systemActivate the cabin Perform start of mission to ATB STM mode , Level NTC
            Expected Result: DMI displays in ATB STM mode, Level NTC
            */
            // Call generic Action Method
            DmiActions
                .Perform_the_following_action_Power_on_the_systemActivate_the_cabin_Perform_start_of_mission_to_ATB_STM_mode_Level_NTC();
            // Call generic Check Results Method
            DmiExpectedResults.DMI_displays_in_ATB_STM_mode_Level_NTC();


            /*
            Test Step 2
            Action: Drive the train forward with 30 km/h and then pass BG0 with level transition announcement
            Expected Result: DMI displays LE12 symbol in sub-area C1
            Test Step Comment: MMI_gen 9430 (partly:Negative LE12); 
            */
            // Call generic Action Method
            DmiActions.Drive_the_train_forward_with_30_kmh_and_then_pass_BG0_with_level_transition_announcement();


            /*
            Test Step 3
            Action: Pass the level transition acknowledgement area
            Expected Result: DMI displays LE13 symbol in sub-area C1
            Test Step Comment: MMI_gen 9431 (partly: LE13); 
            */
            // Call generic Action Method
            DmiActions.Pass_the_level_transition_acknowledgement_area();


            /*
            Test Step 4
            Action: Press acknowledgement LE13 symbol in sub-area C1
            Expected Result: Verify the following information,(1)    DMI replaces LE13 symbol with LE12 in sub-area C1.(2)     Use the log file to confirm that DMI sends out packet [MMI_DRIVER_ACTION (EVC-152)] with the value of variable MMI_M_DRIVER_ACTION refer to sequence below,a)   MMI_M_DRIVER_ACTION = 8 (Ack level 2)
            Test Step Comment: (1) MMI_gen 9431 (partly: LE12);(2) MMI_gen 11470 (partly: Bit #8);
            */


            /*
            Test Step 5
            Action: Pass BG1 at level transition border
            Expected Result: Mode changes to FS mode, Level 2
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
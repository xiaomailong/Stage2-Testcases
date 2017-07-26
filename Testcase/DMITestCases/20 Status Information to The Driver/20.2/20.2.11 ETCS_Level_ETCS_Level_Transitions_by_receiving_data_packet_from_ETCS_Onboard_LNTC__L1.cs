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
    /// 20.2.11 ETCS Level: ETCS Level Transitions by receiving data packet from ETCS Onboard (LNTC ->L1)
    /// TC-ID: 15.2.11
    /// 
    /// This test case verifies the symbol that displays after passed the level transition announcement form level NTC to L1 and then acknowledgement by driver 
    /// 
    /// Tested Requirements:
    /// MMI_gen 9430 (partly: LE10); MMI_gen 9431 (partly: LE10 and LE11);
    /// 
    /// Scenario:
    /// 1.Perform start of mission to ATB STM mode, level NTC
    /// 2.Drive the train forward pass BG0 at position 10m with Pkt 41: level transition announcement to Level 
    /// 1.Verify that LE10 symbol displays in sub-area C1.
    /// 3.Pass the acknowledgement area. Then, verify that LE11 symbol is displayed in sub-area C
    /// 14.Acknowledge the level transition and then verify that LE10 is displayed in sub-area C1 
    /// 5.Pass BG1 at position 400m which is level transition border. Then, mode changes to FS mode, Level 1
    /// 
    /// Used files:
    /// 15_2_11.tdg
    /// </summary>
    public class ETCS_Level_ETCS_Level_Transitions_by_receiving_data_packet_from_ETCS_Onboard_LNTC__L1 : TestcaseBase
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
            // DMI displays in FS mode, Level 1

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

            /*
            Test Step 2
            Action: Drive the train forward with 30 km/h then pass BG0 with level transition announcement
            Expected Result: DMI displays LE10 symbol in sub-area C1.
            Test Step Comment: MMI_gen 9430 (partly:Negative LE10); 
            */

            /*
            Test Step 3
            Action: Pass the level transition acknowledgement area
            Expected Result: DMI displays LE11 symbol in sub-area C1.
            Test Step Comment: MMI_gen 9431 (partly: LE11); 
            */

            /*
            Test Step 4
            Action: Press acknowledgement LE11 symbol in sub-area C1
            Expected Result: DMI replaces LE10 symbol with LE11 in sub-area C1.
            Test Step Comment: MMI_gen 9431 (partly: LE10);
            */

            /*
            Test Step 5
            Action: Pass BG1 at level transition border
            Expected Result: Mode changes to FS mode, Level 1
            */

            /*
            Test Step 6
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
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
    /// 6.8 Accleration/Decleration interval -4.0m/s2 to +4.0 m/s2
    /// TC-ID: 1.8
    /// 
    /// This test case is to verify ETCS MMI can handle accleration /decleration in the interval – 4m/s2 and +4m/s2 
    /// 
    /// Tested Requirements:
    /// MMI_gen 68;
    /// 
    /// Scenario:
    /// 1.Perform Start of Mission to L1, SR mode
    /// 2.Pass BG0 with MA and Track description
    /// 3.Mode changes to FS mode 
    /// 4.Increase the train speed with full acceleration until service barke is applied
    /// 5.Verify that the speedometer movement goes up and down smoothly
    /// 
    /// Used files:
    /// 1_8.tdg
    /// </summary>
    public class AcclerationDecleration_interval_4_0ms2_to_4_0_ms2 : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // Power off test systemSet the following OTE configuration file (OteCfg_PC.cfg)- tractionAcceleration 400- serviceBrakeDeceleration 400- emergencyBrakeDeceleration 400
            
            // Call the TestCaseBase PreExecution
            base.PreExecution();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in FS mode.

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint

            
            /*
            Test Step 1
            Action: Power on the system and activate cabin
            Expected Result: DMI displays in SB mode
            */
            // Call generic Action Method
            DmiActions.Power_on_the_system_and_activate_cabin();
            // Call generic Check Results Method
            DmiExpectedResults.DMI_displays_in_SB_mode();
            
            
            /*
            Test Step 2
            Action: Perform Start of Mission to SR mode , eevel 1
            Expected Result: Mode changes to SR mode , Level 1
            */
            
            
            /*
            Test Step 3
            Action: Pass BG0 with MA and Track description
            Expected Result: Mode changes to FS mode
            */
            
            
            /*
            Test Step 4
            Action: Full accelerate the traction (100%) until service brake is applied
            Expected Result: The speedometer movement goes up and down smoothly
            Test Step Comment: MMI_gen 68;
            */
            
            
            /*
            Test Step 5
            Action: End of Test
            Expected Result: 
            */
            // Call generic Action Method
            DmiActions.End_of_Test();
            

            return GlobalTestResult;
        }
    }
}

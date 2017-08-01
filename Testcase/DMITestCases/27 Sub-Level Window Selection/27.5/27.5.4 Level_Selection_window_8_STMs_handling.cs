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
    /// 27.5.4 Level Selection window: 8 STMs handling
    /// TC-ID: 22.5.4 
    /// 
    /// This test case is to verify that DMI can handle up to 8 STMs. 
    /// 
    /// Tested Requirements:
    /// MMI_gen 1077;
    /// 
    /// Scenario:
    /// 1.Power on the system and activate cabin.
    /// 2.Enter Driver ID and skip the brake test.
    /// 3.Verify that 8 STMs are available on the level selection window.
    /// 
    /// Used files:
    /// N/A
    /// </summary>
    public class Level_Selection_window_8_STMs_handling : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // Power off the system Configure atpcu configuration file as following (See the instruction in Appendix 2)M_InstalledLevels = 4082NID_NTC_Installe_0 = 1NID_NTC_Installe_1 = 20NID_NTC_Installe_2 = 28NID_NTC_Installe_3 = 9NID_NTC_Installe_4 = 6NID_NTC_Installe_5 = 10NID_NTC_Installe_6 = 22NID_NTC_Installe_7 = 0

            // Call the TestCaseBase PreExecution
            base.PreExecution();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            //  DMI Displays in SB mode.

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint


            /*
            Test Step 1
            Action: Power on the system and activate the cabin
            Expected Result: DMI displays Driver ID window in SB mode
            */
            // Call generic Action Method
            DmiActions.Power_on_the_system_and_activate_the_cabin(this);
            // Call generic Check Results Method
            DmiExpectedResults.DMI_displays_Driver_ID_window_in_SB_mode(this);


            /*
            Test Step 2
            Action: Enetr Driver ID and skip brake test
            Expected Result: Verify the following information,The level selection window displays 8 folllowing STMs accroding to configuration settingATBTPWS/AWSTBL1+PZB/LZBPZBLZBATC2ASFA
            Test Step Comment: MMI_gen 1077;
            */


            /*
            Test Step 3
            Action: End of test
            Expected Result: 
            */


            return GlobalTestResult;
        }
    }
}
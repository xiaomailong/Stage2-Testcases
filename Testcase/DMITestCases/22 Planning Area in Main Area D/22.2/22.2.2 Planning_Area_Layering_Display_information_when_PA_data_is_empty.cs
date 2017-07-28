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
    /// 22.2.2 Planning Area-Layering: Display information when PA data is empty
    /// TC-ID: 17.2.2
    /// 
    /// This test case verify the display information of Planning Area when there is no Planning information is available.
    /// 
    /// Tested Requirements:
    /// MMI_gen 7109;
    /// 
    /// Scenario:
    /// Activate Cabin A.Perform SoM in SR mode, Level 
    /// 1.Then, verify that DMI displays only PA Distance Scale and PASP.
    /// 
    /// Used files:
    /// N/A
    /// </summary>
    public class Planning_Area_Layering_Display_information_when_PA_data_is_empty : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // Set the following tags name in configuration file (See the instruction in Appendix 1)HIDE_PA_SR_MODE = 1System is power ON

            // Call the TestCaseBase PreExecution
            base.PreExecution();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in SR mode, Level 1.

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint


            /*
            Test Step 1
            Action: Activate cabin A
            Expected Result: DMI displays Driver ID window
            */
            // Call generic Action Method
            DmiActions.Activate_cabin_A();
            // Call generic Check Results Method
            DmiExpectedResults.DMI_displays_Driver_ID_window();


            /*
            Test Step 2
            Action: Perform SoM in SR mode, Level 1
            Expected Result: DMI displays in SR mode, level 1.Verify that there are only the following objects are displayed in PA,PA Distance Scale (0-4000m)PASP with PASP-dark-colour
            Test Step Comment: MMI_gen 7109;
            */
            // Call generic Action Method
            DmiActions.Perform_SoM_in_SR_mode_Level_1();


            /*
            Test Step 3
            Action: End of test
            Expected Result: 
            */


            return GlobalTestResult;
        }
    }
}
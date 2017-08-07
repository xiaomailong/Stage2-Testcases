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
    /// 22.1.3 Planning Area displays according to configuration in OS and SR mode.
    /// TC-ID: 17.1.3
    /// 
    /// This test case verifies the presentation of planning area in area D in case of  driver configured the planning area to display in OS mode or SR mode. The planning area shall display and comply with conditions of [MMI-ETCS-gen].
    /// 
    /// Tested Requirements:
    /// MMI_gen 3063 (SR and OS mode); MMI_gen 7101; MMI_gen 7104;
    /// 
    /// Scenario:
    /// Activate Cabin A.Perform SoM in SR mode, Level 
    /// 1.Then, verify that PA is displayed.Drive train forward pass BG1 at 100m. Then, verify that PA is displayed.BG1: Packet 12, 21, 27 and 80
    /// 
    /// Used files:
    /// 17_1_3.tdg
    /// </summary>
    public class Planning_Area_displays_according_to_configuration_in_OS_and_SR_mode : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // Set the following tags name in configuration file (See the instruction in Appendix 1)HIDE_PA_SR_MODE = 1 (PA will show in SR mode)HIDE_PA_OS_MODE = 1 (PA will show in OS mode)HIDE_PA_LEVEL_1 = 1 (Show PA in the Level 1)HIDE_PA_FUNCTION = 0 (‘ON’ state)System is power ON.

            // Call the TestCaseBase PreExecution
            base.PreExecution();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays OS mode, level 1.

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
            DmiActions.Activate_Cabin_1(this);
            // Call generic Check Results Method
            DmiExpectedResults.Driver_ID_window_displayed(this);


            /*
            Test Step 2
            Action: Driver performs SoM to SR mode, level 1
            Expected Result: DMI displays in SR mode, level 1.Verify that the Planning Area is displayed in area D.The Hide PA button is displayed on the planning area with ‘Scale up’ and ‘Scale Down’ buttons
            Test Step Comment: (1) MMI_gen 3063 (SR);   (1) MMI_gen 7101;   (2) MMI_gen 7104;
            */
            // Call generic Action Method
            DmiActions.Driver_performs_SoM_to_SR_mode_level_1(this);


            /*
            Test Step 3
            Action: Drive the train forward passing BG1.Then, press an acknowledgement of OS mode symbol in area C1
            Expected Result: DMI displays in OS mode, level 1.Verify that the Planning Area is displayed the planning information in area D.The Hide PA button is displayed on the planning area with ‘Scale up’ and ‘Scale Down’ buttons
            Test Step Comment: (1) MMI_gen 3063 (OS);   (1) MMI_gen 7101;   (2) MMI_gen 7104;
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
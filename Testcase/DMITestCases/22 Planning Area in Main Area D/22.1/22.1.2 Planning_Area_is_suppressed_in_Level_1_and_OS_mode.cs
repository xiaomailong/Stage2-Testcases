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
    /// 22.1.2 Planning Area is suppressed in Level 1 and OS mode
    /// TC-ID: 17.1.2
    /// 
    /// This test case verifies that the DMI allows to suppress the planning area in level 1 and On sight mode. The planning area configuration shall comply with conditions of [MMI-ETCS-gen]
    /// 
    /// Tested Requirements:
    /// MMI_gen 7102; MMI_gen 7101 (partly: disable OS);
    /// 
    /// Scenario:
    /// Activate Cabin A.Perform SoM in SR mode, Level 1.Drive train forward pass BG1 at 100m. Then, touch the main area D and verify that PA is not displayed on DMI.BG1: Packet 12, 21 and 27Drive train forward pass BG2 at 200m. Then, touch the main area D and verify that PA is not displayed on DMI.BG2: Packet 12, 21 ,27 and 80
    /// 
    /// Used files:
    /// 17_1_2.tdg
    /// </summary>
    public class Planning_Area_is_suppressed_in_Level_1_and_OS_mode : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // Set the following tags name in configuration file (See the instruction in Appendix 1)HIDE_PA_LEVEL_1 = 0 (Not show PA in the Level 1)HIDE_PA_OS_MODE = 0 (PA will not show in OS mode)HIDE_PA_FUNCTION = 0 (‘ON’ state)System is power ON.

            // Call the TestCaseBase PreExecution
            base.PreExecution();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in OS mode, level 1.

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint


            /*
            Test Step 1
            Action: Activate Cabin A
            Expected Result: DMI displays Driver ID window
            */
            // Call generic Action Method
            DmiActions.Activate_Cabin_A(this);
            // Call generic Check Results Method
            DmiExpectedResults.DMI_displays_Driver_ID_window(this);


            /*
            Test Step 2
            Action: Driver performs SoM to SR mode, level 1
            Expected Result: DMI displays in SR mode, level 1
            */
            // Call generic Action Method
            DmiActions.Driver_performs_SoM_to_SR_mode_level_1(this);
            // Call generic Check Results Method
            DmiExpectedResults.DMI_displays_in_SR_mode_level_1(this);


            /*
            Test Step 3
            Action: Drive train forward pass BG1
            Expected Result: DMI change from SR mode to FS mode
            */


            /*
            Test Step 4
            Action: Touch main area D
            Expected Result: Verify that the Planning Area is not displayed on DMI
            Test Step Comment: MMI_gen 7102;
            */
            // Call generic Action Method
            DmiActions.Touch_main_area_D(this);
            // Call generic Check Results Method
            DmiExpectedResults.Verify_that_the_Planning_Area_is_not_displayed_on_DMI(this);


            /*
            Test Step 5
            Action: Drive train forward pass BG2.Then, press an acknowledgement of OS mode symbol in area C1
            Expected Result: DMI change from FS mode to OS mode
            */


            /*
            Test Step 6
            Action: Touch main area D
            Expected Result: Verify that the Planning Area is not displayed on DMI
            Test Step Comment: MMI_gen 7101 (partly: disable OS);
            */
            // Call generic Action Method
            DmiActions.Touch_main_area_D(this);
            // Call generic Check Results Method
            DmiExpectedResults.Verify_that_the_Planning_Area_is_not_displayed_on_DMI(this);


            /*
            Test Step 7
            Action: End of test
            Expected Result: 
            */


            return GlobalTestResult;
        }
    }
}
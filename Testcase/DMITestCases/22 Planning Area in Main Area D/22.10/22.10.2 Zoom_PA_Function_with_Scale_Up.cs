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
    /// 22.10.2 Zoom PA Function with Scale Up
    /// TC-ID: 17.10.2
    /// 
    /// This test case verifies the properties of ‘Scale Up’ button of Zoom PA function. When driver presses the NA03 symbol to zoom in the planning area information. The scale up button shall comply with [ERA-ERTMS] standard.
    /// 
    /// Tested Requirements:
    /// MMI_gen 7378; MMI_gen 7386; MMI_gen 7387; MMI_gen 630; MMI_gen 7373 (partly: no visible);
    /// 
    /// Scenario:
    /// Activate cabin A. Perform SoM to SR mode, level 1.Drive the train pass BG1 at 100m. Then, verify that the Zoom PA function is enabled.Press ‘Scale Up’ button until the distance scale is the lowest value. Then, verify that the scale up button is disabled.Press ‘Scale Up’ button. Then, verify that PA’s distance scale is not change. Press ‘Scale Down’ button. Then, verify that scale up button is enabled.Press ‘Hide’ button. Then, verify that the scale up button is hidden.
    /// 
    /// Used files:
    /// 17_10_2.tdg
    /// </summary>
    public class Zoom_PA_Function_with_Scale_Up : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // System is power on.

            // Call the TestCaseBase PreExecution
            base.PreExecution();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in FS mode, level 1.

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint


            /*
            Test Step 1
            Action: Activate cabin A
            Expected Result: DMI displays in SB mode. The Driver ID window is displayed
            */
            // Call generic Action Method
            DmiActions.Activate_Cabin_1(this);
            // Call generic Check Results Method
            DmiExpectedResults.Driver_ID_window_displayed_in_SB_mode(this);


            /*
            Test Step 2
            Action: Driver performs SoM to SR mode, level 1
            Expected Result: DMI displays in SR mode, level 1
            */
            // Call generic Action Method
            DmiActions.Driver_performs_SoM_to_SR_mode_level_1(this);
            // Call generic Check Results Method
            DmiExpectedResults.SR_Mode_displayed(this);


            /*
            Test Step 3
            Action: Driver drives the train forward with speed = 30km/h and pass BG1
            Expected Result: DMI changes from SR mode to FS mode, Level 1 with PA in area D.Verify the following information,The symbol NA03 is displayed at sub-area D9
            Test Step Comment: (1) MMI_gen 7386 (partly: NA03);
            */


            /*
            Test Step 4
            Action: Presses ‘Scale Up’ button
            Expected Result: DMI remains displays in FS mode.Verify the following information,The PA’s distance range is changed to [0…2000]
            Test Step Comment: (1) MMI_gen 7386 (partly: operable zoom PA);
            */


            /*
            Test Step 5
            Action: Press ‘Scale Up’ button until the distance range is [0…1000]
            Expected Result: Verify the following information,Verify that the Zoom PA function is switched the PA’s distance range to the next lower range and the visualisation of the PA are updated accordingly.When the distance range is [0…1000], the symbol NA05 is displayed in sub-area D9
            Test Step Comment: (1) MMI_gen 630; (2) MMI_gen 7387;
            */


            /*
            Test Step 6
            Action: Press ‘Scale Up’ button
            Expected Result: Verify the following information,Verify that The ‘Scale Up’ button of the operable Zoom PA function is disabled, visualisation of the PA are not change
            Test Step Comment: (1) MMI_gen 7378 (partly: disable);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press ‘Scale Up’ button");


            /*
            Test Step 7
            Action: Press ‘Scale Down’ button
            Expected Result: Verify the following information,The PA’s distance range is changed to [0…2000] and the symbol NA03 is displayed in sub-area D9
            Test Step Comment: (1) MMI_gen 7378 (partly: opposite case); 
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press ‘Scale Down’ button");


            /*
            Test Step 8
            Action: Driver presses ‘Hide’ button at position top-right of planning area in sub-area D14
            Expected Result: The Planning information area is  disappeared from DMI.Verify that the ‘Scale Up’ and ‘Scale Down’ buttons are removed
            Test Step Comment: (1) MMI_gen 7373 (partly: no visible);
            */
            // Call generic Action Method
            DmiActions.Driver_presses_Hide_button_at_position_top_right_of_planning_area_in_sub_area_D14(this);


            /*
            Test Step 9
            Action: End of test
            Expected Result: 
            */


            return GlobalTestResult;
        }
    }
}
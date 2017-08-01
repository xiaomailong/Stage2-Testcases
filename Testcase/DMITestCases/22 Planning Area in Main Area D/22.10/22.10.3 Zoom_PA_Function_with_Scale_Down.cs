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
    /// 22.10.3 Zoom PA Function with Scale Down
    /// TC-ID: 17.10.3
    /// 
    /// This test case verifies the properties of Scale Down button in Zoom PA function. When driver pressed the NA04 symbol to zoom out the planning area information, the scale range on the planning area shall change. The Scale down button shall comply with [ERA-ERTMS] standard.
    /// 
    /// Tested Requirements:
    /// MMI_gen 7379; MMI_gen 7384; MMI_gen 7385; MMI_gen 7388; MMI_gen 7373;
    /// 
    /// Scenario:
    /// Activate cabin A. Perform SoM to SR mode, level 1.Drive the train pass BG1 at 100m.Driver the train follow with permitted speed. Then, verify that the Scale Down button is enabled.Press ‘Scale Down’ button until the highest distance range. Then, verify that the scale down button is disabled.Press ‘Scale Down’ button. Then, verify that PA’s distance scale is not change. Press ‘Scale Up’ button. Then, verify that scale down button is enabled.Press ‘Hide’ button. Then, verify that the scale down button is hidden.
    /// 
    /// Used files:
    /// 17_10_3.tdg
    /// </summary>
    public class Zoom_PA_Function_with_Scale_Down : TestcaseBase
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
            Expected Result: DMI displays the default window. The Driver ID window is displayed
            */
            // Call generic Action Method
            DmiActions.Activate_cabin_A(this);
            // Call generic Check Results Method
            DmiExpectedResults.DMI_displays_the_default_window_The_Driver_ID_window_is_displayed(this);


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
            Action: Drive the train forward pass BG1
            Expected Result: DMI changes from SR mode to FS mode, Level 1 with PA in area D.Verify the following information,The symbol NA04 is displayed at sub-area D12
            Test Step Comment: (1) MMI_gen 7384 (partly: NA04);
            */
            // Call generic Action Method
            DmiActions.Drive_the_train_forward_pass_BG1(this);


            /*
            Test Step 4
            Action: Presses ‘Scale Down’ button
            Expected Result: DMI remains displays in FS mode.Verify the following information,The PA’s distance range is changed to [0…8000]
            Test Step Comment: (1) MMI_gen 7384 (partly: operable zoom PA);
            */


            /*
            Test Step 5
            Action: Presses ‘Scale Down’ button until the distance range is [0…32000]
            Expected Result: Verify the following information,Verify that the Zoom PA function is switched the PA’s distance range to the next higher range and the visualisation of the PA are updated accordingly.When the distance range is [0…32000], the symbol NA06 is displayed in sub-area D12
            Test Step Comment: (1) MMI_gen 7388;   (2) MMI_gen 7385;
            */


            /*
            Test Step 6
            Action: Press ‘Scale Down’ button
            Expected Result: Verify the following information,Verify that The ‘Scale Down’ button of the operable Zoom PA function is disabled, visualisation of the PA are not change
            Test Step Comment: (1) MMI_gen 7379 (partly: disable);  
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(@"Press ‘Scale Down’ button");


            /*
            Test Step 7
            Action: Press ‘Scale Up’ button
            Expected Result: Verify the following information,The PA’s distance range is changed to [0…16000] and the symbol NA04 is displayed in sub-area D12
            Test Step Comment: (1) MMI_gen 7379 (partly: opposite case); 
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(@"Press ‘Scale Up’ button");


            /*
            Test Step 8
            Action: Driver presses ‘Hide’ button at position top-right of planning area in sub-area D14
            Expected Result: Verify the following information,The Planning information area and the ‘Scale Down’ buttons are disappeared from DMI
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
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
    /// 22.10.4 Zoom PA Function with the communication loss between ETCS Onboard and DMI
    /// TC-ID: 17.10.4
    /// 
    /// This test case verifies the Zoom PA function of planning area when communication between ETCS Onboard and DMI is lost.
    /// 
    /// Tested Requirements:
    /// MMI_gen 7393; MMI_gen 7394;
    /// 
    /// Scenario:
    /// Activate cabin A. Perform SoM to SR mode, level 1.Drive the train pass BG1 at 100m.Press ‘Scale Up’ button and simulate the communication loss between ETCS Onboard and DMI. Then, verify that the Zoom PA function is disabled.Re-establish the communication again. Then, verify that the Zoom PA function is not changed.Press ‘Scale Up’ and ‘Scale Down’ button. Then, verify that Zoom PA function is operable when communication is re-established.
    /// 
    /// Used files:
    /// 17_10_4.tdg
    /// </summary>
    public class Zoom_PA_Function_with_the_communication_loss_between_ETCS_Onboard_and_DMI : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // System is power on.The default configuration of PA distance scale is not set as [0…1000] (variable DEFAULT_PAGE_DISPLAY in etcs_planningArea.xml is not equal to 0)

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
            DmiActions.Activate_Cabin_1(this);
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
            Expected Result: DMI changes from SR mode to FS mode with PA in area D
            */
            // Call generic Action Method
            DmiActions.Drive_the_train_forward_pass_BG1(this);


            /*
            Test Step 4
            Action: Driver presses ‘Scale up’ button for selects distance range to [0…1000]
            Expected Result: The distance scale range of PA is presented with the range [0…1000]
            */


            /*
            Test Step 5
            Action: Simulate the communication loss between ETCS onboard and DMI.ExamplePause OTE/ATPRemove connection between DMI and PC (MVB or TCP-IP)
            Expected Result: DMI displays message “ATP Down Alarm” with sound alarm.Verify that the planning area is removed from DMI
            Test Step Comment: (1) MMI_gen 7393 (partly: symbols);
            */


            /*
            Test Step 6
            Action: Perform the following procedure,Press at sub-area D9 twice.Press at sub-area D12.Re-establish the communication  between ETCS onboard and DMI.ExampleStart OTE/ATPConnect DMI to PC (MVB or TCP-IP)
            Expected Result: DMI displays in FS mode again. The Planning Area information and Zoom PA function are resumed. The distance scale range is displayed with the range [0…1000]
            Test Step Comment: (1) MMI_gen 7394 (partly: symbols);(2) MMI_gen 7393 (partly: sensitive areas, inoperable);         
            */


            /*
            Test Step 7
            Action: Press ‘Scale Down’ button
            Expected Result: Verify the following information,The distance scale range of PA is presented with the range [0…2000]
            Test Step Comment: (1) MMI_gen 7394 (partly: operable);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(@"Press ‘Scale Down’ button");


            /*
            Test Step 8
            Action: Press ‘Scale Up’ button
            Expected Result: Verify the following information,The distance scale range of PA is presented with the range [0…1000]
            Test Step Comment: (1) MMI_gen 7394 (partly: operable);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(@"Press ‘Scale Up’ button");


            /*
            Test Step 9
            Action: End of test
            Expected Result: 
            */


            return GlobalTestResult;
        }
    }
}
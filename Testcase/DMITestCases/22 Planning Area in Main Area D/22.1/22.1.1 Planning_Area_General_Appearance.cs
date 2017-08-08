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
    /// 22.1.1 Planning Area: General Appearance
    /// TC-ID: 17.1.1
    /// 
    /// This test case verifies the presentation of planning area in area D when DMI changes to FS mode. The planning area shall display the planning information and all objects to driver. The presentation of the planning area shall comply with [ERA-ERTMS] standard and [MMI-ETCS-gen].
    /// 
    /// Tested Requirements:
    /// MMI_gen 3063 (partly: FS mode); MMI_gen 7101 (partly: default configuration);                      MMI_gen 7102 (partly: default configuration); MMI_gen 9937;
    /// 
    /// Scenario:
    /// Activate Cabin A.Perform SoM in SR mode, Level 1.Drive train forward pass BG1 at 100m. Then, verify that PA is not displayed in the SR mode.BG1: Packet 21 and 27Drive train forward pass BG2 at 150m. Then, verify that all objects in MMI_gen 9937 are displayed BG2: Packet 12 and 68
    /// 
    /// Used files:
    /// 17_1_1.tdg
    /// </summary>
    public class Planning_Area_General_Appearance : TestcaseBase
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
            Expected Result: DMI displays Driver ID window
            */
            // Call generic Action Method
            DmiActions.Activate_Cabin_1(this);
            // Call generic Check Results Method
            DmiExpectedResults.Driver_ID_window_displayed(this);


            /*
            Test Step 2
            Action: Perform SoM to SR mode, level 1
            Expected Result: DMI displays in SR mode, level 1
            */
            // Call generic Action Method
            DmiActions.Perform_SoM_to_SR_mode_level_1(this);
            // Call generic Check Results Method
            DmiExpectedResults.SR_Mode_displayed(this);


            /*
            Test Step 3
            Action: Drive the train forward passing BG1
            Expected Result: DMI remain displays in SR mode, level 1.Verify that the Planning area is not displayed in main area D
            Test Step Comment: MMI_gen 7101 (partly: default  configuration);              
            */
            // Call generic Action Method
            DmiActions.Drive_train_forward_passing_BG1(this);


            /*
            Test Step 4
            Action: Drive the train forward passing BG2
            Expected Result: DMI changes from SR mode to FS mode.The Planning area is displayed the planning information in main area D.The planning area is displayed the information following:Distance scaleOrder and announcement of track conditionGradient profileSpeed profile discontinuitiesPASPIndication markerHide and show planning informationZoom function(see the example in ‘Comment’ column)
            Test Step Comment: (1) MMI_gen 3063       (partly: FS mode);MMI_gen 7102       (partly: default  configuration);  (2) MMI_gen 9937;   
            */
            // Call generic Action Method
            DmiActions.Drive_train_forward_passing_BG2(this);


            /*
            Test Step 5
            Action: End of test
            Expected Result: 
            */


            return GlobalTestResult;
        }
    }
}
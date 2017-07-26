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
    /// 22.9.11 Hide PA Function configured ‘STORED’ with re-activate cabin
    /// TC-ID: 17.9.11
    /// 
    /// This test case  is to verify Hide PA function after re-activate the cabin.The Hide PA function state shall store and present similar as before deactivation. The Hide PA function shall comply with conditions of  [MMI-ETCS-gen].
    /// 
    /// Tested Requirements:
    /// MMI_gen 9948;
    /// 
    /// Scenario:
    /// 1.Active cabin A. Perform SoM to SR mode, level 
    /// 12.Press Hide PA button and verify that the Planning area is disappeared 
    /// 3.Deactivare and Activate the cabin A and verify that the Planning area is disappeared
    /// 4.Press sensitive area at main area D then the Palnning area is reappeared
    /// 5.Retest with FS and OS mode. Verify that the Planning area area is disappeared after reactivate the cabin
    /// 
    /// Used files:
    /// 17_9_11.tdg
    /// </summary>
    public class Hide_PA_Function_configured_STORED_with_re_activate_cabin : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // System is power OFFSet the following tags name in configuration file (See the instruction in Appendix 1)HIDE_PA_FUNCTION = 2 (‘STORE’ state)HIDE_PA_OS_MODE = 1 (shown)HIDE_PA_SR_MODE = 1 (shown)

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
            Action: Power on the system Activate cabin A.
            Expected Result: DMI displays Driver ID window.
            */

            /*
            Test Step 2
            Action: Perform SoM to SR mode, level 1.
            Expected Result: DMI displays in SR mode, level 1The Planning area is appeared on DMI.
            */

            /*
            Test Step 3
            Action: Press Hide PA button
            Expected Result: The Planning area is disappeared from DMI
            */

            /*
            Test Step 4
            Action: Deactive and reacitvate the cabin A
            Expected Result: DMI displays Driver ID window.
            */

            /*
            Test Step 5
            Action: Perform SoM to SR mode, level 1.
            Expected Result: DMI displays in SR mode, level 1The Planning area is not appear on DMI
            Test Step Comment: MMI_gen 9948 (partly:SR);
            */

            /*
            Test Step 6
            Action: Press at sensitive area in main area D.
            Expected Result: The planning area is reappeared by this activation.
            */

            /*
            Test Step 7
            Action: Drive the train forward with speed = 30 km/h pass BG1.
            Expected Result: Mode changes to FS modeThe Planning area is appeared on DMI.
            */

            /*
            Test Step 8
            Action: Stop the train and then press Hide PA button
            Expected Result: Train is at standstillThe Planning area is disappeared from DMI
            */

            /*
            Test Step 9
            Action: Deactive and reacitvate the cabin A
            Expected Result: DMI displays Driver ID window.
            */

            /*
            Test Step 10
            Action: Perform SoM to SR mode, level 1.
            Expected Result: DMI displays in SR mode, leve1
            */

            /*
            Test Step 11
            Action: Drive the train forward with speed = 30 km/h pass BG2.
            Expected Result: DMI displays in FS modeThe Planning area is not appear on DMI.
            Test Step Comment: MMI_gen 9948 (partly:FS);
            */

            /*
            Test Step 12
            Action: Press at sensitive area in main area D.
            Expected Result: The planning area is reappeared by this activation.
            */

            /*
            Test Step 13
            Action: Pass BG3 and the comfirm OS mode
            Expected Result: Mode chages to OS modeThe Planning area is appeared on DMI
            */

            /*
            Test Step 14
            Action: Stop the train and then press Hide PA button
            Expected Result: Train is at standstillThe Planning area is disappeared from DMI
            */

            /*
            Test Step 15
            Action: Deactive and reacitvate the cabin A
            Expected Result: DMI displays Driver ID window.
            */

            /*
            Test Step 16
            Action: Perform SoM to SR mode, level 1.
            Expected Result: DMI displays in SR mode, leve1
            */

            /*
            Test Step 17
            Action: Drive the train forward with speed = 30 km/h pass BG4.
            Expected Result: OS mode Acknowledgment is requested
            */

            /*
            Test Step 18
            Action: Acknowledge OS mode
            Expected Result: Mode chages to OS modeThe Planning area is not appear on DMI.
            Test Step Comment: MMI_gen 9948 (partly:OS);
            */

            /*
            Test Step 19
            Action: Press at sensitive area in main area D.
            Expected Result: The planning area is reappeared by this activation.
            */

            /*
            Test Step 20
            Action: End of test.
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
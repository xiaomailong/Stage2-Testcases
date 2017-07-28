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
    /// 25.2 Driver’s Action: Override window and Data view window
    /// TC-ID: 20.2
    /// 
    /// This test case verify that DMI sends values of [MMI_DRIVER_REQUEST (EVC-101).MMI_M_REQUEST] correctly when a driver presses on each button in Override window and Data view window.
    /// 
    /// Tested Requirements:
    /// MMI_gen 151 (partly: MMI_M_REQUEST = 7, 21); MMI_gen 11470 (partly: Bit #14);
    /// 
    /// Scenario:
    /// 1.At Override window, press the ‘EOA’ button. Then, verify the value in packet EVC-101 refer to each action.
    /// 2.Press the ‘Data view’ button. Then, verify the value in packet EVC-101 when button is pressed.
    /// 
    /// Used files:
    /// N/A
    /// </summary>
    public class Drivers_Action_Override_window_and_Data_view_window : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // Test system is powered on.Cabin is activated.SoM is performed in SR mode, Level 1.Override window is opened.

            // Call the TestCaseBase PreExecution
            base.PreExecution();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in SR mode, Level 1

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint


            /*
            Test Step 1
            Action: Press the ‘EOA’ button
            Expected Result: DMI displays Default window in SR mode, Level 1 with MO03 symbol in sub-area C7.Verify the following information,(1)    Use the log file to confirm that DMI sends out packet [MMI_DRIVER_REQUEST (EVC-101)] with variable MMI_M_REQUEST = 7 (Start Override EOA (Pass stop))(2)   The Override window is closed, DMI displays Default window.(3)    Use the log file to confirm that DMI sends out packet [MMI_DRIVER_ACTION (EVC-152)] with the value of variable MMI_M_DRIVER_ACTION refer to sequence below,a)    MMI_M_DRIVER_ACTION = 14 (Override selected)
            Test Step Comment: (1) MMI_gen 151 (partly: MMI_M_REQUEST = 7);(2) MMI_gen 151 (partly: close opened menu);(3) MMI_gen 11470 (partly: Bit #14);
            */


            /*
            Test Step 2
            Action: Press the ‘Data view’ button
            Expected Result: Verify the following information,(1)    Use the log file to confirm that DMI sends out packet [MMI_DRIVER_REQUEST (EVC-101)] with variable MMI_M_REQUEST = 21 (Start Train Data Veiw)(2)   The Default window is closed, DMI displays Data view window
            Test Step Comment: (1) MMI_gen 151 (partly: MMI_M_REQUEST = 21);(2) MMI_gen 151 (partly: close opened menu);
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
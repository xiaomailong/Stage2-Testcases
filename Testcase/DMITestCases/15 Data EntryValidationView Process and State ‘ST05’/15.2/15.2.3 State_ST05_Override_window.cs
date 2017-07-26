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
    /// 15.2.3 State 'ST05': Override window
    /// TC-ID: 10.2.3
    /// 
    /// This test case verifies the buttons of Override window when entry and exit on state of 'ST05'.
    /// 
    /// Tested Requirements:
    /// MMI_gen 12018 (partly: Override window); MMI_gen 168 (partly: disabled buttons, Override window); MMI_gen 4395 (partly: close button, disabled, Override window); MMI_gen 4396 (partly: close, NA11, NA12, Override window); MMI_gen 5646 (partly: always enable, State 'ST05' button is disabled, Override window); MMI_gen 5728 (partly: removal, EVC, restore after ST05, Override window); MMI_gen 8859 (partly: Override window);
    /// 
    /// Scenario:
    /// 1.The ‘Override’ menu window is displayed.
    /// 2.Use the test script files to send packets in order to verify state ‘ST05’ in a menu window. 
    /// 
    /// Used files:
    /// 10_2_3_a.xml
    /// </summary>
    public class State_ST05_Override_window : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // Test system is powered onCabin is activePerform SoM in SR mode, Level 1.

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
            Action: Perform the following procedure;Press ‘close’ button (Main menu window)Press ‘Override’ button
            Expected Result: Verify the following information;(1)   Verify DMI still displays Default window until Override window is displayed.(2)   Verify the close button is always enable.
            Test Step Comment: (1) MMI_gen 8859 (partly: Override window);(2) MMI_gen 5646 (partly: always enable, Override window);
            */

            /*
            Test Step 2
            Action: Use the test script file 10_2_3_a.xml to disable and enable button via EVC-8 withPacket 1 (Entry state of ‘ST05’)MMI_Q_TEXT_CRITERIA = 3 MMI_Q_TEXT = 716Packet 2 (Exit state of ‘ST05’)MMI_Q_TEXT_CRITERIA = 4MMI_Q_TEXT = 716Note: Stopwatch is required for accuracy of test result.
            Expected Result: Verify the following information;DMI in the entry state of ‘ST05’The hourglass symbol ST05 is displayed.(1)   Verify all buttons and the close button is disable.(2)   The disabled Close button NA12 is display in area G.10 seconds laterDMI in the exit state of ‘ST05’(3)   The hourglass symbol ST05 is removed.(4)   The state of all buttons is restored according to the last status before script is sent.(5)   The enabled Close button NA11 is display in area G.
            Test Step Comment: (1) MMI_gen 12018 (partly: Override window);(2) MMI_gen 168 (partly: disabled buttons, Override window); MMI_gen 5646 (partly: State 'ST05' button is disabled, Override window); MMI_gen 4395 (partly: close button, disabled, Override window); MMI_gen 4396 (partly: close, NA12, Override window);(3) MMI_gen 5728 (partly: removal, EVC, Override window);(4) MMI_gen 5728 (partly: restore after ST05, Override window);(5) MMI_gen 4396 (partly: close, NA11, Override window);
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
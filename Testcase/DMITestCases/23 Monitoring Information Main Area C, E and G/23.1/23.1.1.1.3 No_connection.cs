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
    /// 23.1.1.1.3 No connection
    /// TC-ID: 18.1.1.1.3
    /// 
    /// This test case is verifies the display information of safe radio connection symbols when there is no connection. Note: The safe radio connection is released when communication session is terminated by performing end of mission (see more detail in chapter 5 of [SUBSET026])
    /// 
    /// Tested Requirements:
    /// MMI_gen 11441;
    /// 
    /// Scenario:
    /// 1.Perform SoM in SR mode, level 
    /// 2.Then, keep the record of received packets EVC-8 and verify the display information in sub-area E1.
    /// 2.Enter SH mode by pressing ‘Shunting’ button. Then, verify the display information removing by received packets EVC-8 after passed 60 seconds (fail to establish a safe radio connection 3 times).
    /// 
    /// Used files:
    /// 18_1_1_1_3.utt
    /// </summary>
    public class No_connection : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // Setup a verbose visualisation for the radio connection status in configuration file (RADIO_STATUS_VISUAL= 1).System is powered ON.Cabin is activated

            // Call the TestCaseBase PreExecution
            base.PreExecution();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in SH mode, level 2

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint


            /*
            Test Step 1
            Action: Perform SoM in SR mode, level 2.
            Expected Result: DMI displays in SR mode, Level 2.DMI display the radio connection symbols in sub-area E1 refer to received packet of EVC-8 with unique value of MMI_I_TEXT in each packet and follolwing value of MMI_Q_TEXT,MMI_Q_TEXT = 568 (Connection established) or 613 (Connection Up)  or  MMI_Q_TEXT = 609 (Network registed via one modem) or MMI_Q_TEXT = 610 (Network registred via two modems).
            */

            /*
            Test Step 2
            Action: Press and hold ‘Shunting’ button at least 2 second.Then, release the pressed button.Note: Stopwatch is required.
            Expected Result: DMI displays in SH mode, level 2.When the time is passed 60 seconds ,Verify the following information,(1)    Use the log file to confirm that DMI received multiple packets of EVC-8 with variable MMI_Q_TEXT_CRITERIA = 4 with the same value of MMI_I_TEXT in expected No.1 of test step 1.(2)     No symbol display in sub-area E1.
            Test Step Comment: (1) MMI_gen 11441 (partly: removing all related text messages);(2) MMI_gen 11441 (partly: no symbols shall be displayed);
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
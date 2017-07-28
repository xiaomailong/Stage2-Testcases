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
    /// 20.1.4 Mode acknowledgement subtitution
    /// TC-ID: 15.1.4
    /// 
    /// This test case verify a subtitution of mode acknowledgement symbol when mode acknowledgement symbol is display after level announcement symbol.
    /// 
    /// Tested Requirements:
    /// MMI_gen 11234;
    /// 
    /// Scenario:
    /// Drive the train forward pass BG1 and press an acknowledgement on sub-area C1.BG1: packet 41 (Transition to Level 0).Drive the forward pass BG
    /// 2.Then, verify the mode acknowledgment subtitution on sub-area C1.BG2: packet 12, 21, 27 and 80 (Entering FS with acknowledgement of OS mode)Press an acknowledgement on sub-area C
    /// 1.Then, verify that the Level annoucement wihtout acknownledgement symbol is displays on sub-area C1 after mode acknowledgement symbol is disappearred.
    /// 
    /// Used files:
    /// 15_1_4.tdg
    /// </summary>
    public class Mode_acknowledgement_subtitution : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // System is power ON.SoM is perform in SR mode, Level 1.

            // Call the TestCaseBase PreExecution
            base.PreExecution();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in OS mode, Level 1.

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint


            /*
            Test Step 1
            Action: Drive the train forward passing BG1.Then, press an area C1 for acknowledgement
            Expected Result: DMI displays LE07 symbol in sub-area C1
            */


            /*
            Test Step 2
            Action: Continue to drive the train forward pass BG2.Then, stop the train
            Expected Result: DMI displays in FS mode, Level 1.Verify the following information,(1)   The symbol MO08 is displayed for On sight acknowledegement in sub-area C1
            Test Step Comment: (1) MMI_gen 11234 (partly: subtituted);
            */


            /*
            Test Step 3
            Action: Press an area C1 for acknowledgement
            Expected Result: Verify the following information,(1)   The symbol MO08 is disappear and DMI displays LE07 symbol instead
            Test Step Comment: (1) MMI_gen 11234 (partly: driver acknowledge);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(@"Press an area C1 for acknowledgement");


            /*
            Test Step 4
            Action: End of test
            Expected Result: 
            */


            return GlobalTestResult;
        }
    }
}
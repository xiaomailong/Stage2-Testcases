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
    /// 27.17.2 Layers
    /// TC-ID: 8.2.1
    /// 
    /// This test case verifies the presentation of Driver ID window that appears in area D, F and G cover the Half-Grid array each object (excluding Keyboards) shall comply with [ERA-ERTMS] standard.
    /// 
    /// Tested Requirements:
    /// MMI_gen 8033 (partly: MMI_gen 4722 (partly: Half-Grid Array), MMI_gen 5189 (partly: touch screen));
    /// 
    /// Scenario:
    /// Activate cabin A.  
    /// 1.Driver enters the Driver ID.
    /// 2.Perform brake test. Observe and verify that the presentation of the Driver ID window layout is displayed in Half-Grid array covering on Main-area D, F and G.
    /// 
    /// Used files:
    /// N/A
    /// </summary>
    public class Layers : TestcaseBase
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
            // DMI displays in SB mode.

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint


            /*
            Test Step 1
            Action: Activate cabin A
            Expected Result: The Driver ID window is displayed cover  the half-grid array in area D, F and G
            */
            // Call generic Action Method
            DmiActions.Activate_cabin_A();


            /*
            Test Step 2
            Action: Driver enters the Driver ID
            Expected Result: Verify that the layers on half grid array are displayed as 1. Layer 0: Main area D, F and G2. Layer -1: Area A1, (A2+A3)*, A4, B*, C1, (C2+C3+C4)*, C5, C6, C7, C8, C9, E1, E2, E3, E4, (E5-E9)*.3. Layer -2: Area B3, B4, B5, B6 and B7.4. Each object are follow the dimension and position as example picture in comment.Note: ‘*’ symbol is mean that specified area are drawn as one area
            Test Step Comment: MMI_gen 8033 (partly: MMI_gen 4722 (partly: Half-Grid Array),  MMI_gen 5189 (partly: touch screen))
            */


            /*
            Test Step 3
            Action: Confirm the Driver ID and perform brake test
            Expected Result: DMI displays the message ‘Brake test in progress’
            */


            /*
            Test Step 4
            Action: End of test
            Expected Result: 
            */


            return GlobalTestResult;
        }
    }
}
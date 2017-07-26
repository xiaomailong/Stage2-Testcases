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
    /// 10.2 Screen Layout: Layers
    /// TC-ID: 5.2
    /// 
    /// This test case verify the appearance of each layer that appears on DMI layout or displays on the default window. The layer shall comply with [ERA] standard.
    /// 
    /// Tested Requirements:
    /// MMI_gen 4213; MMI_gen 4215; MMI_gen 4216; MMI_gen 4217; MMI_gen 4218; MMI_gen 5944;
    /// 
    /// Scenario:
    /// Perform SoM and enter to SR mode, Level 
    /// 1.Then, verify the displays information in Default window.
    /// 
    /// Used files:
    /// N/A
    /// </summary>
    public class Screen_Layout_Layers : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // Test system is powered on

            // Call the TestCaseBase PreExecution
            base.PreExecution();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in SR mode, level 1.

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint


            /*
            Test Step 1
            Action: Perform SoM in SR mode, Level 1.
            Expected Result: DMI displays Default window in SR mode, Level 1.Verify the following information,LayerAreas displayed with the same impression of depth shall form a layer as picture below,The level of layers in each area of window as follows,Layer 0: Area E10, E11, F, G1, G2, G3, G4, G5, G6, G7, G8, G9, G10, Z, YLayer 0 areas have no border. Layer -1: Area A1, (A2+A3)*, A4, B*, D, C1, (C2+C3+C4)*, C5, C6, C7, C8, C9, E1, E2, E3, E4, (E5-E9)*.Layer -1 areas are inside layer 0 and have a border. Layer -2: Area B3, B4, B5, B6 and B7.Layer -2 areas are located inside Layer -1 and have a border.Note: ‘*’ symbol is mean that specified area are drawn as one area.
            Test Step Comment: (1) MMI_gen 4213;   (2) MMI_gen 5944; MMI_gen 4215; MMI_gen 4216; MMI_gen 4217; MMI_gen 4218;
            */

            /*
            Test Step 2
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
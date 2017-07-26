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
    /// 20.2.1 ETCS Level in Sub-Area C8: Level 0 and Level 1 by driver selection
    /// TC-ID: 15.2.1
    /// 
    /// This test case verifies the appearance of ETCS Level symbols in sub-area C8 after driver confirmed level selection. The ETCS Level symbols shall comply with [MMI-ETCS-gen], [GenVSIS] and [ERA] standard.
    /// 
    /// Tested Requirements:
    /// MMI_gen 577 (partly: Level 0 and Level 1);
    /// 
    /// Scenario:
    /// Select and confirm Level 
    /// 0.Then, verify the display information.Open Main window.Select and confirm Level 
    /// 1.Then, verify the display information.
    /// 
    /// Used files:
    /// N/A
    /// </summary>
    public class ETCS_Level_in_Sub_Area_C8_Level_0_and_Level_1_by_driver_selection : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // System is power on.Cabin is activated.Driver ID is entered.Brake test is performed.

            // Call the TestCaseBase PreExecution
            base.PreExecution();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in SB mode, level 1.

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint


            /*
            Test Step 1
            Action: Select and confirm Level 0.
            Expected Result: DMI displays Main window in SB mode, Level 0.Verify the following information, The symbol LE01 is displayed in sub-area C8.Use the log file to confirm that DMI received packet EVC-7 with variable OBU_TR_M_LEVEL = 0 (Level 0).
            Test Step Comment: (1) MMI_gen 577 (partly: Level is valid and equal to 0, symbol LE01, displayed in area C8);(2) MMI_gen 577 (partly: Level is valid, Derived from variable,Level 0);
            */

            /*
            Test Step 2
            Action: Press ‘Level’ button.Then, select and confirm Level 1.
            Expected Result: DMI displays Main window in SB mode, Level 1.Verify the following information, The symbol LE03 is displayed in sub-area C8.Use the log file to confirm that DMI received packet EVC-7 with variable OBU_TR_M_LEVEL = 2 (Level 1).
            Test Step Comment: (1) MMI_gen 577 (partly: Level is valid and equal to 1, symbol LE03, displayed in area C8);(2) MMI_gen 577 (partly: Level is valid, Derived from variable,Level 1);
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
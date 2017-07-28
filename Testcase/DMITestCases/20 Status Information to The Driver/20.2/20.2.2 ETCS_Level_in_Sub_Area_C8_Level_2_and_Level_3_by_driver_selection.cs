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
    /// 20.2.2 ETCS Level in Sub-Area C8: Level 2 and Level 3 by driver selection
    /// TC-ID: 15.2.2
    /// 
    /// This test case verifies the appearance of ETCS Level symbol shall display in sub-area C8. The ETCS Level symbols shall comply with [MMI-ETCS-gen], [GenVSIS] and [ERA] standard.
    /// 
    /// Tested Requirements:
    /// MMI_gen 577 (partly: Level 2 and Level 3); MMI_gen 11470 (partly: Bit # 36 and 37);
    /// 
    /// Scenario:
    /// Perform SoM to SR mode , Level 
    /// 2.Then, verify that the level 2 ”LE04” symbol displays at sub-area C
    /// 8.Repeat the test steps with Level 
    /// 3.Then, verify that the level 3 ”LE05” symbol displays at sub-area C8.
    /// 
    /// Used files:
    /// 15_2_2.utt
    /// </summary>
    public class ETCS_Level_in_Sub_Area_C8_Level_2_and_Level_3_by_driver_selection : TestcaseBase
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
            // DMI displays in SB mode, level 3.

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint


            /*
            Test Step 1
            Action: Select and confirm Level 2
            Expected Result: DMI displays RBC contact window.Verify the following information, The symbol LE04 is displayed in sub-area C8.Use the log file to confirm that DMI received packet EVC-7 with variable OBU_TR_M_LEVEL = 3 (Level 2).Use the log file to confirm that DMI sends out packet [MMI_DRIVER_ACTION (EVC-152)] with the value of variable MMI_M_DRIVER_ACTION refer to sequence below,a)   MMI_M_DRIVER_ACTION = 36 (level 2 selected)
            Test Step Comment: (1) MMI_gen 577 (partly: Level is valid and equal to 2, symbol LE04, displayed in area C8);(2) MMI_gen 577 (partly: Level is valid, Derived from variable,Level 2);(3) MMI_gen 11470 (partly: Bit # 36);
            */


            /*
            Test Step 2
            Action: Restart OTE and RBC Simulator.Repeat action step 1 with Level 3
            Expected Result: DMI displays RBC contact window.Verify the following information, The symbol LE05 is displayed in sub-area C8.Use the log file to confirm that DMI received packet EVC-7 with variable OBU_TR_M_LEVEL = 4 (Level 3).Use the log file to confirm that DMI sends out packet [MMI_DRIVER_ACTION (EVC-152)] with the value of variable MMI_M_DRIVER_ACTION refer to sequence below,a)   MMI_M_DRIVER_ACTION = 37 (level 3 selected)
            Test Step Comment: (1) MMI_gen 577 (partly: Level is valid and equal to 3, symbol LE05, displayed in area C8);(2) MMI_gen 577 (partly: Level is valid, Derived from variable,Level 3);(3) MMI_gen 11470 (partly: Bit # 37);
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
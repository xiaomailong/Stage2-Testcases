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
using Testcase.DMITestCases;
using Testcase.Telegrams.DMItoEVC;
using Testcase.Telegrams.EVCtoDMI;
using static Testcase.Telegrams.EVCtoDMI.Variables;
using Testcase.TemporaryFunctions;

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
    public class TC_15_2_1_ETCS_Level : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // System is power on.Cabin is activated.Driver ID is entered.Brake test is performed.

            base.PreExecution();

            EVC0_MMIStartATP.Evc0Type = EVC0_MMIStartATP.EVC0Type.GoToIdle;
            EVC0_MMIStartATP.Send();

            DmiActions.Activate_Cabin_1(this);

            DmiActions.Display_Driver_ID_Window(this);
            DmiActions.Set_Driver_ID(this, "1234");
            DmiActions.Send_SB_Mode(this);
            DmiActions.ShowInstruction(this, @"Perform the following action within 3 seconds after pressing OK : " +
                                             Environment.NewLine + Environment.NewLine +
                                             "1. Enter and confirm Driver ID on the DMI.");

            DmiActions.Request_Brake_Test(this);
            DmiActions.ShowInstruction(this, @"Perform the following action within 3 seconds after pressing OK : " +
                                             Environment.NewLine + Environment.NewLine +
                                             "1. Perform Brake Test");

            DmiActions.Display_Level_Window(this);
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

            #region Test Step 1

            /*
            Test Step 1
            Action: Select and confirm Level 0
            Expected Result: DMI displays Main window in SB mode, Level 0.
            Verify the following information, 
            The symbol LE01 is displayed in sub-area C8.
            Use the log file to confirm that DMI received packet EVC-7 
            with variable OBU_TR_M_LEVEL = 0 (Level 0)
            Test Step Comment: (1) MMI_gen 577 (partly: Level is valid and equal to 0, symbol LE01, displayed in area C8);
                               (2) MMI_gen 577 (partly: Level is valid, Derived from variable,Level 0);
            */

            DmiActions.ShowInstruction(this, @"Perform the following action within 3 seconds after pressing OK : " +
                                             Environment.NewLine + Environment.NewLine +
                                             "1. Select and enter Level 0");

            DmiExpectedResults.Level_0_Selected(this);

            DmiActions.Display_Main_Window_with_Start_button_not_enabled(this);
            DmiActions.Send_L0(this);
            DmiActions.Send_SB_Mode(this);

            DmiExpectedResults.Main_Window_displayed(this, true);
            DmiExpectedResults.Driver_symbol_displayed(this, "Level 0", "LE01", "C8", true);
            DmiExpectedResults.SB_Mode_displayed(this);

            #endregion

            #region Test Step 2

            /*
            Test Step 2
            Action: Press ‘Level’ button.Then, select and confirm Level 1
            Expected Result: DMI displays Main window in SB mode, Level 1.
            Verify the following information, The symbol LE03 is displayed in sub-area C8.
            Use the log file to confirm that DMI received packet EVC-7 
            with variable OBU_TR_M_LEVEL = 2 (Level 1)
            Test Step Comment: (1) MMI_gen 577 (partly: Level is valid and equal to 1, symbol LE03, displayed in area C8);
                               (2) MMI_gen 577 (partly: Level is valid, Derived from variable,Level 1);
            */

            DmiActions.ShowInstruction(this, @"Perform the following action: " + Environment.NewLine +
                                             Environment.NewLine +
                                             "1. Press 'Level' button.");
            DmiActions.Display_Level_Window(this);
            DmiActions.ShowInstruction(this, @"Perform the following action within 3 seconds after pressing OK : " +
                                             Environment.NewLine + Environment.NewLine +
                                             "1. Select and enter Level 1");

            DmiExpectedResults.Level_1_Selected(this);

            DmiActions.Display_Main_Window_with_Start_button_not_enabled(this);
            DmiActions.Send_L1(this);
            DmiActions.Send_SB_Mode(this);

            DmiExpectedResults.Main_Window_displayed(this, true);
            DmiExpectedResults.Driver_symbol_displayed(this, "Level 1", "LE03", "C8", true);
            DmiExpectedResults.SB_Mode_displayed(this);

            #endregion

            #region Test Step 3

            /*
            Test Step 3
            Action: End of test
            Expected Result: 
            */

            #endregion

            return GlobalTestResult;
        }
    }
}
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
using Testcase.Telegrams.EVCtoDMI;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 27.1.2 ETCS Specfic submenus and SN sub menus
    /// TC-ID: 22.1.2
    /// 
    /// This test case verifies that DMI shall hanlder the ETCS Specfic submenus and SN submenu in SN mode 
    /// 
    /// Tested Requirements:
    /// MMI_gen 1319;
    /// 
    /// Scenario:
    /// 1.Power on the system
    /// 2.Perform SoM to PLZB STM 3 Verfy that Close button is avaiable on Main ,Override, Data View, Special and Settings windows
    /// 4.Verify that the National button is availble and work as a part of ETCS Specific submenus
    /// 
    /// Used files:
    /// N/A
    /// </summary>
    public class TC_ID_22_1_2_Sub_Level_Window : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:

            // Call the TestCaseBase PreExecution
            base.PreExecution();

            // The system is powered ON
            DmiActions.Start_ATP();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in Level STM PLZB, SN mode.

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint

            /*
            Test Step 1
            Action: Activate cabin and perform start of mission to PLZB STM
            Expected Result: DMI displays in PLZB STM mode
            */
            DmiActions.Activate_Cabin_1(this);
            DmiActions.Set_Driver_ID(this, "1234");            
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Level = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_LEVEL.LNTC;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.NationalSystem;
            DmiActions.FinishedSoM_Default_Window(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in PLZB_STM_mode");

            /*
            Test Step 2
            Action: Select Main menu
            Expected Result: DMI displays Main window The Close buton is enable
            Test Step Comment: MMI_gen 1319 (partly:bullet1 and bullet2);
            */
            DmiActions.ShowInstruction(this, "Select the Main menu item");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Main window." + Environment.NewLine +
                                "2. The �Start�, �Maintain Shunting� and �Non-leading� buttons are displayed disabled." + Environment.NewLine +
                                "3. The �Train data�, �Level�, �Shunting�, and �Train running number� buttons are displayed enabled." + Environment.NewLine +
                                "4. The �Close� button is displayed enabled.");

            /*
            Test Step 3
            Action: Press Close button
            Expected Result: DMI displays the default window
            Test Step Comment: MMI_gen 1319 (partly:bullet3);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press the �Close� button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Default window.");

            /*
            Test Step 4
            Action: Select EOA menu
            Expected Result: DMI displays EOA window The Close buton is enable
            Test Step Comment: MMI_gen 1319 (partly:bullet1 and bullet2);
            */
            DmiActions.ShowInstruction(this, "Select the EOA menu item");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the EOA window with the title �Override�." + Environment.NewLine +
                                "2. The �Close� button is displayed enabled.");
            
            /*
            Test Step 5
            Action: Press Close button
            Expected Result: DMI displays the default window
            Test Step Comment: MMI_gen 1319 (partly:bullet3);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press the �Close� button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Default window.");

            /*
            Test Step 6
            Action: Select Data view menu
            Expected Result: DMI displays Data view window The Close buton is enable
            Test Step Comment: MMI_gen 1319 (partly:bullet1 and bullet2);
            */
            DmiActions.ShowInstruction(this, "Select the Data view menu item");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Data view window with the title �Data view (1/2)�." + Environment.NewLine +
                                "2. The �Close� button is displayed enabled.");
            
            /*
            Test Step 7
            Action: Press Close button
            Expected Result: DMI displays the default window
            Test Step Comment: MMI_gen 1319 (partly:bullet3);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press the �Close� button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Default window.");

            /*
            Test Step 8
            Action: Select Special menu
            Expected Result: DMI displays Special window The Close buton is enable
            Test Step Comment: MMI_gen 1319 (partly:bullet1 and bullet2);
            */
            DmiActions.ShowInstruction(this, "Select the Special menu item");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Special window with the title �Special�." + Environment.NewLine +
                                "2. The �Close� button is displayed enabled." + Environment.NewLine +
                                "3. The �Adhesion� and �SR speed distance� buttons are displayed disabled." + Environment.NewLine +
                                "2. The �Train Integrity� button is displayed enabled.");

            /*
            Test Step 9
            Action: Press Close button
            Expected Result: DMI displays the default window
            */
            DmiActions.ShowInstruction(this, @"Press the Close button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Default window.");

            /*
            Test Step 10
            Action: Select Settings menu
            Expected Result: DMI displays Settings window The Close buton is enable
            Test Step Comment: MMI_gen 1319 (partly:bullet1 and bullet2);
            */
            DmiActions.ShowInstruction(this, "Select the Settings menu item");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Settings window with the title �Settings�." + Environment.NewLine +
                                "2. The �Language� button is displayed enabled with symbol SE03." + Environment.NewLine +
                                "3. The �Brightness� button is displayed enabled with symbol SE01." + Environment.NewLine +
                                "4. The �Set VBC� button is displayed disabled." + Environment.NewLine +
                                "5. The �Lock screen for cleaning� button is displayed enabled." + Environment.NewLine + 
                                "6. The �Brake� button is displayed enabled." + Environment.NewLine +
                                "7. The �System info� button is displayed enabled." + Environment.NewLine +
                                "8. The �Volume� button is displayed enabled with symbol SE02." + Environment.NewLine +
                                "9. The �System version� button is displayed enabled." + Environment.NewLine +
                                "10. The �Remove VBC� button is displayed disabled." + Environment.NewLine +
                                "11. The �Time� button is displayed enabled with a clock symbol." + Environment.NewLine +
                                "12. The �Maintenance� button is displayed disabled." + Environment.NewLine +
                                "13. The �National� button is displayed enabled." + Environment.NewLine +
                                "14. The �Close� button is displayed enabled.");

            /*
            Test Step 11
            Action: Press National button
            Expected Result: DMI displays National sub-window The Close buton is enable
            Test Step Comment: MMI_gen 1319 (partly:bullet1 and bullet2);
            */
            DmiActions.ShowInstruction(this, @"Press the �National� button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the National window with the title �National�." + Environment.NewLine +
                                "2. The �F�llstand� button is displayed disabled." + Environment.NewLine +
                                "3. The �Pr�fen� button is displayed enabled." + Environment.NewLine +
                                "4. The �Close� button is displayed enabled.");

            /*
            Test Step 12
            Action: Press Close button
            Expected Result: DMI displays the setting window
            Test Step Comment: MMI_gen 1319 (partly:bullet1 and bullet2);
            */
            DmiActions.ShowInstruction(this, @"Press the Close button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Settings window.");

            /*
            Test Step 13
            Action: Press Close button
            Expected Result: DMI displays the default window
            Test Step Comment: MMI_gen 1319 (partly:bullet3);
            */
            DmiActions.ShowInstruction(this, @"Press the Close button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Default window.");
            
            /*
            Test Step 14
            Action: End of test
            Expected Result: 
            */
            
            return GlobalTestResult;
        }
    }
}
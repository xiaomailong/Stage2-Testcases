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
    public class ETCS_Specfic_submenus_and_SN_sub_menus : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // The system is powered ON

            // Call the TestCaseBase PreExecution
            base.PreExecution();
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
            // Call generic Check Results Method
            DmiExpectedResults.DMI_displays_in_PLZB_STM_mode();


            /*
            Test Step 2
            Action: Select  Main menu
            Expected Result: DMI displays Main window The Close buton is enable
            Test Step Comment: MMI_gen 1319 (partly:bullet1 and bullet2);
            */


            /*
            Test Step 3
            Action: Press Close button
            Expected Result: DMI displays the default window
            Test Step Comment: MMI_gen 1319 (partly:bullet3);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(@"Press Close button");
            // Call generic Check Results Method
            DmiExpectedResults.DMI_displays_the_default_window();


            /*
            Test Step 4
            Action: Select EOA menu
            Expected Result: DMI displays EOA window The Close buton is enable
            Test Step Comment: MMI_gen 1319 (partly:bullet1 and bullet2);
            */


            /*
            Test Step 5
            Action: Press Close button
            Expected Result: DMI displays the default window
            Test Step Comment: MMI_gen 1319 (partly:bullet3);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(@"Press Close button");
            // Call generic Check Results Method
            DmiExpectedResults.DMI_displays_the_default_window();


            /*
            Test Step 6
            Action: Select Data view menu
            Expected Result: DMI displays Data view window The Close buton is enable
            Test Step Comment: MMI_gen 1319 (partly:bullet1 and bullet2);
            */


            /*
            Test Step 7
            Action: Press Close button
            Expected Result: DMI displays the default window
            Test Step Comment: MMI_gen 1319 (partly:bullet3);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(@"Press Close button");
            // Call generic Check Results Method
            DmiExpectedResults.DMI_displays_the_default_window();


            /*
            Test Step 8
            Action: Select Special menu
            Expected Result: DMI displays Special window The Close buton is enable
            Test Step Comment: MMI_gen 1319 (partly:bullet1 and bullet2);
            */


            /*
            Test Step 9
            Action: Press Close button
            Expected Result: DMI displays the default window
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(@"Press Close button");
            // Call generic Check Results Method
            DmiExpectedResults.DMI_displays_the_default_window();


            /*
            Test Step 10
            Action: Select Settings menu
            Expected Result: DMI displays Settings window The Close buton is enable
            Test Step Comment: MMI_gen 1319 (partly:bullet1 and bullet2);
            */


            /*
            Test Step 11
            Action: Press National button
            Expected Result: DMI displays National sub-window The Close buton is enable
            Test Step Comment: MMI_gen 1319 (partly:bullet1 and bullet2);
            */


            /*
            Test Step 12
            Action: Press Close button
            Expected Result: DMI displays the setting window
            Test Step Comment: MMI_gen 1319 (partly:bullet1 and bullet2);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(@"Press Close button");


            /*
            Test Step 13
            Action: Press Close button
            Expected Result: DMI displays the default window
            Test Step Comment: MMI_gen 1319 (partly:bullet3);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(@"Press Close button");
            // Call generic Check Results Method
            DmiExpectedResults.DMI_displays_the_default_window();


            /*
            Test Step 14
            Action: End of test
            Expected Result: 
            */


            return GlobalTestResult;
        }
    }
}
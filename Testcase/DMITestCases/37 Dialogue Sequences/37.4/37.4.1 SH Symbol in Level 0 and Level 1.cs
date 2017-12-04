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
    /// 37.4.1 SH Symbol in Level 0 and Level 1
    /// TC-ID: 34.4.1
    /// 
    /// This test case verifies the display of symbol MO01 in ETCS level 2 and 3 when DMI receives message 28 from RBC after driver presses Shunting button.
    /// 
    /// Tested Requirements:
    /// MMI_gen 11914 (partly: when receive SH symbol); MMI_gen 11084 (partly: SH); MMI_gen 110 (partly: MO10);
    /// 
    /// Scenario:
    /// 1.Enter SH mode, level 0. Then, verify the display of symbol MO01.
    /// 2.Restart test system
    /// 3.Enter SH mode, level 1. Then, verify the display of symbol MO01.
    /// 
    /// Used files:
    /// 34_4_1.utt
    /// </summary>
    public class TC_34_4_1_Dialogue_Sequences : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // Call the TestCaseBase PreExecution
            base.PreExecution();
            // Test system is power onCabin is activatedDriver ID is enteredLevel 0 is selected and confirmed
            DmiActions.Start_ATP();

            // Set train running number, cab 1 active, and other defaults
            DmiActions.Activate_Cabin_1(this);

            // Set driver ID
            DmiActions.Set_Driver_ID(this, "1234");

            // Enable standard buttons including Start, and display Main window.
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.Start | Variables.standardFlags;
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Main;      // Main window
            EVC30_MMIRequestEnable.Send();

        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in SH mode, level 1

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint

            /*
            Test Step 1
            Action: Enter SH mode by performing the procedure below,Press and hold ‘Shunting’ button at least 2 secondsRelease ‘Shunting’ button

            Expected Result: Use the log file to confirm that DMI receives EVC-7 with variable OBU_TR_M_MODE = 3 (SH – Shunting).The symbol MO01 is display in area B7DMI closes Main window and returns to the Default window.

            Test Step Comment: MMI_gen 11914 (partly: receives SH symbol); MMI_gen 11084 (partly: SH);MMI_gen 11914 (partly: display the symbol when receive SH symbol); MMI_gen 110 (partly: MO10);MMI_gen 11914 (partly: close main window and return to the default window);
            */

            DmiActions.ShowInstruction(this, @"Press and hold the ‘Shunting’ button for at least 2 seconds, then release the button");

            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Level = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_LEVEL.L0;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.Shunting;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the symbol M001 in area B7." + Environment.NewLine +
                                "2. DMI closes Main window and displays the Default window.");

            /*
            Test Step 2
            Action: Re-validate the step 1 by re-starting OTE Simulator and starting the precondition with ETCS level 1.
            Expected Result: See the expected results at Step 1
            */
            DmiActions.ShowInstruction(this, @"Power down the system, wait 10s and then power up the system");

            DmiActions.Start_ATP();

            // Set train running number, cab 1 active, and other defaults
            DmiActions.Activate_Cabin_1(this);

            // Set driver ID
            DmiActions.Set_Driver_ID(this, "1234");

            // Set to level 1
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Level = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_LEVEL.L1;
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.Shunting;
            EVC30_MMIRequestEnable.Send();

            DmiActions.ShowInstruction(this, @"Press and hold ‘Shunting’ button for at least 2 seconds. Release ‘Shunting’ button");
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.Shunting;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the symbol M001 in area B7." + Environment.NewLine +
                                "2. DMI closes Main window and displays the Default window.");
            /*
            Test Step 3
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
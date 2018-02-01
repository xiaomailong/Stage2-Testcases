using System;
using Testcase.Telegrams.EVCtoDMI;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 37.4.2.2 SH Symbol in Level 2 and Level 3
    /// TC-ID: 34.4.2.2
    /// 
    /// This test case verifies the display of symbol MO01 in ETCS level 2 and 3 when DMI receives message 28 from RBC after driver presses Shunting button.
    /// 
    /// Tested Requirements:
    /// MMI_gen 11914 (partly: when receive SH symbol); MMI_gen 11084 (partly: SH); MMI_gen 110 (partly: MO10);
    /// 
    /// Scenario:
    /// 1.Enter SH mode, level 
    /// 2.Then, verify the display of symbol MO01.2.Restart test system.3.Enter SH mode, level 
    /// 3.Then, verify the display of symbol MO01.
    /// 
    /// Used files:
    /// 34_4_2_2.utt
    /// </summary>
    public class TC_34_4_2_2_Dialogue_Sequences : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:

            // Call the TestCaseBase PreExecution
            base.PreExecution();
            // Test System is power onCabin is activatedSoM is performed in SR mode, level 2
            DmiActions.Start_ATP();

            // Set train running number, cab 1 active, and other defaults
            DmiActions.Activate_Cabin_1(this);

            // Set driver ID
            DmiActions.Set_Driver_ID(this, "1234");

            // Set to level 2
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Level = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_LEVEL.L2;

            // Enable standard buttons including Start, and display Default window.
            DmiActions.Finished_SoM_Default_Window(this);
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in SH mode, level 3
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in SH mode, Level 3.");

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 0;
            // Testcase entrypoint


            TraceHeader("Test Step 1");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Enter SH mode by performing the procedure below,Press ‘Main’ buttonPress and hold ‘Shunting’ button at least 2 seconds.Release ‘Shunting’ button");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following information,Use the log file to confirm that DMI receives EVC-7 with variable OBU_TR_M_MODE = 3 (SH – Shunting).The symbol MO01 is display in area B7.DMI closes Main window and returns to the Default window");
            /*
            Test Step 1
            Action: Enter SH mode by performing the procedure below,Press ‘Main’ buttonPress and hold ‘Shunting’ button at least 2 seconds.Release ‘Shunting’ button
            Expected Result: Verify the following information,Use the log file to confirm that DMI receives EVC-7 with variable OBU_TR_M_MODE = 3 (SH – Shunting).The symbol MO01 is display in area B7.DMI closes Main window and returns to the Default window
            Test Step Comment: (1) MMI_gen 11914 (partly: receives SH symbol, Level 2/3); MMI_gen 11084 (partly: SH);(2) MMI_gen 11914 (partly: display the symbol when receive SH symbol); MMI_gen 110 (partly: MO10);(3) MMI_gen 11914 (partly: close main window and return to the default window);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this,
                @"Press ‘Main’ button, then press and hold ‘Shunting’ button at least 2 seconds. Release ‘Shunting’ button");

            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.Shunting;
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the symbol M001 in area B7." + Environment.NewLine +
                                "2. DMI closes Main window and displays the Default window.");

            TraceHeader("Test Step 2");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Re-validate the step1 by re-starting OTE Simulator and starting the precondition with ETCS level 3");
            TraceReport("Expected Result");
            TraceInfo("See the expected results at Step 1");
            /*
            Test Step 2
            Action: Re-validate the step1 by re-starting OTE Simulator and starting the precondition with ETCS level 3
            Expected Result: See the expected results at Step 1
            */
            // Restart
            DmiActions.ShowInstruction(this, "Power down the system, wait 10s, then power up the system");
            DmiActions.Start_ATP();

            // Set train running number, cab 1 active, and other defaults
            DmiActions.Activate_Cabin_1(this);

            // Set driver ID
            DmiActions.Set_Driver_ID(this, "1234");

            // Set to level 1 and SH mode
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Level = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_LEVEL.L3;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.Shunting;

            DmiActions.ShowInstruction(this,
                @"Press and hold ‘Shunting’ button for at least 2 seconds. Release ‘Shunting’ button");
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the symbol M001 in area B7." + Environment.NewLine +
                                "2. DMI closes Main window and displays the Default window.");
            TraceHeader("Test Step 3");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("End of test");
            
            /*
            Test Step 3
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
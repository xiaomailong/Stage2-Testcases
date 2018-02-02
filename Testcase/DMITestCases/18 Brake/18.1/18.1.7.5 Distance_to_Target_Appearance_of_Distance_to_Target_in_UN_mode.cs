using System;
using Testcase.Telegrams.EVCtoDMI;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 18.1.7.5 Distance to Target: Appearance of Distance to Target in UN mode
    /// TC-ID: 13.1.7.5
    /// 
    /// This test case verifies the display information of the distance to target bar and digital in UN mode. The display of distance to target bar and digital are comply with the received packet EVC-1 and EVC-7.  
    /// 
    /// Tested Requirements:
    /// MMI_gen 2567 (partly: Table 38, UN mode); MMI_gen 107 (partly: Table 37, UN mode); MMI_gen 6658; MMI_gen 6774;
    /// 
    /// Scenario:
    /// 1.Perform SoM in UN mode, level 
    /// 0.Then, verify the display of distance to target bar and digital with received packet EVC-1 and EVC-7.
    /// 
    /// Used files:
    /// N/A
    /// </summary>
    public class TC_13_1_7_5_Brake : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:

            // Call the TestCaseBase PreExecution
            base.PreExecution();
            // System is powered on.Cabin is activated. 
            DmiActions.Start_ATP();

            // Set train running number, cab 1 active, and other defaults
            DmiActions.Activate_Cabin_1(this);
        }

        public override bool TestcaseEntryPoint()
        {
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 0;
            // Testcase entrypoint

            TraceHeader("Test Step 1");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Perform SoM in UN mode, level 0");
            TraceReport("Expected Result");
            TraceInfo(
                "DMI displays in UN mode, level 0Verify the following information(1)    Use the log file to confirm that DMI receives the following packets information with a specific value,  EVC-7: OBU_TR_M_MODE = 4 (UN mode) (2)   The distance to target bar is not display in sub-area A3. (3)   The distance to target digital is not display in sub-area A2.(4)   Use the log file to confirm that DMI receives the packet EVC-1 with variable MMI_O_BRAKETARGET = -1 (Default)");
            /*
            Test Step 1
            Action: Perform SoM in UN mode, level 0
            Expected Result: DMI displays in UN mode, level 0Verify the following information(1)    Use the log file to confirm that DMI receives the following packets information with a specific value,  EVC-7: OBU_TR_M_MODE = 4 (UN mode) (2)   The distance to target bar is not display in sub-area A3. (3)   The distance to target digital is not display in sub-area A2.(4)   Use the log file to confirm that DMI receives the packet EVC-1 with variable MMI_O_BRAKETARGET = -1 (Default)
            Test Step Comment: (1) MMI_gen 107 (partly: MMI_M_WARNING, OBU_TR_M_MODE, UN mode); MMI_gen 2567 (partly: MMI_M_WARNING, OBU_TR_M_MODE, UN mode);(2) MMI_gen 6658 (partly: not be shown); MMI_gen 107 (partly: Table 37, UN mode);(3) MMI_gen 2567 (partly: Table 38, UN mode); MMI_gen 6774 (partly: not be shown);(4) MMI_gen 6658 (partly: MMI_O_BRAKETARGET is less than zero); MMI_gen 6774 (partly: MMI_O_BRAKETARGET is less than zero);
            */
            DmiActions.ShowInstruction(this, "Perform SoM in UN mode, level 0");

            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.Unfitted;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Level = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_LEVEL.L0;
            DmiActions.Finished_SoM_Default_Window(this);
            EVC1_MMIDynamic.MMI_O_BRAKETARGET = -1;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in UN mode, level 0 (Symbol MO16 is displayed in area B7)." +
                                Environment.NewLine +
                                "2. The distance to target bar is not displayed in sub-area A3." + Environment.NewLine +
                                "3. The digital distance to target is not displayed in sub-area A2.");

            TraceHeader("Test Step 2");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("End of test");
            
            /*
            Test Step 2
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
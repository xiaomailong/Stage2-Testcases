using System;
using Testcase.Telegrams.EVCtoDMI;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 18.1.7.6 Distance to Target: Appearance of Distance to Target in NL mode
    /// TC-ID: 13.1.7.6
    /// 
    /// This test case verifies the display information of the distance to target bar and digital in NL mode. The display of distance to target bar and digital is comply with the received packet EVC-1 and EVC-7.  
    /// 
    /// Tested Requirements:
    /// MMI_gen 2567 (partly: Table 38, NL mode); MMI_gen 107 (partly: Table 37, NL mode); MMI_gen 6658; MMI_gen 6774;
    /// 
    /// Scenario:
    /// 1.Enter NL mode. Then, verify the display of distance to target bar and digital when received packet EVC-1 and EVC-7.
    /// 
    /// Used files:
    /// N/A
    /// </summary>
    public class TC_13_1_7_6_Brake : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:

            // Call the TestCaseBase PreExecution
            base.PreExecution();

            // System is powered on.Cabin is activated.Driver ID is entered.Level 1 is selected and confirmed.
            DmiActions.Start_ATP();

            // Set train running number, cab 1 active, and other defaults
            DmiActions.Activate_Cabin_1(this);

            // Set driver ID
            DmiActions.Set_Driver_ID(this, "1234");

            // Set to level 1
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Level = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_LEVEL.L1;
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint


            /*
            Test Step 1
            Action: Perform the following procedure, Force the simulation to ‘Non-leadingPress and hold ‘Non-leading’ button at least 2 second.Release the pressed button
            Expected Result: DMI displays in NL mode, level 0Verify the following information(1)    Use the log file to confirm that DMI receives the following packets information with a specific value,  EVC-7: OBU_TR_M_MODE = 11 (NL mode) (2)   The distance to target bar is not display in sub-area A3. (3)   The distance to target digital is not display in sub-area A2.(4)   Use the log file to confirm that DMI receives the packet EVC-1 with variable MMI_O_BRAKETARGET = -1 (Default)
            Test Step Comment: (1) MMI_gen 107 (partly: OBU_TR_M_MODE, NL mode); MMI_gen 2567 (partly: OBU_TR_M_MODE, NL mode);(2) MMI_gen 6658 (partly: not be shown); MMI_gen 107 (partly: Table 37, NL mode);(3) MMI_gen 2567 (partly: Table 38, NL mode); MMI_gen 6774 (partly: not be shown);(4) MMI_gen 6658 (partly: MMI_O_BRAKETARGET is less than zero); MMI_gen 6774 (partly: MMI_O_BRAKETARGET is less than zero);
            */
            DmiActions.ShowInstruction(this,
                "Force the simulation to ‘Non-leading’. Press and hold ‘Non-leading’ button at least 2s. Release the ‘Non-leading’ button.");

            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.NonLeading;
            EVC1_MMIDynamic.MMI_O_BRAKETARGET = -1;

            // Test spec says level 0 but pre-condition says level 1
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in NL mode, level 1 (Symbol MO12 is displayed in area B7)." +
                                Environment.NewLine +
                                "2. The distance to target bar is not displayed in sub-area A3." + Environment.NewLine +
                                "3. The digital distance to target is not displayed in sub-area A2.");

            /*
            Test Step 2
            Action: End of test
            Expected Result: 
            */


            return GlobalTestResult;
        }
    }
}
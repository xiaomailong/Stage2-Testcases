using System;
using Testcase.Telegrams.EVCtoDMI;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 22.9.12 Hide PA Function with the occupied by system outside ETCS
    /// TC-ID: 17.9.12
    /// 
    /// This test case verifies the removal of ‘Show’ and ‘Hide’ button if at least one of the sub areas in Main-area D is occupied by STM system (system outside ETCS).
    /// 
    /// Tested Requirements:
    /// MMI_gen 7354;
    /// 
    /// Scenario:
    /// 1.Drive the train forward pass BG1 at position 100m. Then, stop the train.       BG1: Packet 12, 21 and 27 (Entering FS)
    /// 2.Select and confirm Level ‘ATB”. Then, verify that all objects in Main-area D are removed.
    /// 
    /// Used files:
    /// 17_9_12.tdg
    /// </summary>
    public class TC_ID_17_9_12_Hide_PA_Function_with_the_occupied_by_system_outside_ETCS : TestcaseBase
    {

        public override bool TestcaseEntryPoint()
        {
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 0;

            // Test system is power on.Cabin is activated.SoM is performed in SR mode, level 1
            StartUp();
            DmiActions.Complete_SoM_L1_SR(this);

            // Testcase entrypoint
            TraceInfo("This test case requires an ATP configuration change - " +
                      "See Precondition requirements. If this is not done manually, the test may fail!");

            MakeTestStepHeader(1, UniqueIdentifier++, "Drive the train forward pass BG1.Then, stop the train",
                "DMI displays in FS mode, Level 1 with PA in Main-area D");
            /*
            Test Step 1
            Action: Drive the train forward pass BG1.Then, stop the train
            Expected Result: DMI displays in FS mode, Level 1 with PA in Main-area D
            */
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.FullSupervision;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in FS mode, Level 1." + Environment.NewLine +
                                "2. The Planning Area is displayed in area D.");

            MakeTestStepHeader(2, UniqueIdentifier++,
                "Perform the following procedure,Press ‘Main’ buttonPress ‘Level’ buttonSelect and confirm  level ‘ATB’At Main window, press ‘Close’ button",
                "DMI displays in SN mode, Level ATB.Verify the following information,(1)    All objects of PA in Main-area D are removed");
            /*
            Test Step 2
            Action: Perform the following procedure,Press ‘Main’ buttonPress ‘Level’ buttonSelect and confirm  level ‘ATB’At Main window, press ‘Close’ button
            Expected Result: DMI displays in SN mode, Level ATB.Verify the following information,(1)    All objects of PA in Main-area D are removed
            Test Step Comment: (1) MMI_gen 7354;
            */
            DmiActions.ShowInstruction(this, "Press the ‘Main’ button, then press the ‘Level’ button");

            EVC20_MMISelectLevel.MMI_Q_CLOSE_ENABLE = Variables.MMI_Q_CLOSE_ENABLE.Disabled;
            EVC20_MMISelectLevel.MMI_Q_LEVEL_NTC_ID = new Variables.MMI_Q_LEVEL_NTC_ID[]
                {Variables.MMI_Q_LEVEL_NTC_ID.ETCS_Level};
            EVC20_MMISelectLevel.MMI_M_CURRENT_LEVEL = new Variables.MMI_M_CURRENT_LEVEL[]
                {Variables.MMI_M_CURRENT_LEVEL.NotLastUsedLevel};
            EVC20_MMISelectLevel.MMI_M_LEVEL_FLAG = new Variables.MMI_M_LEVEL_FLAG[]
                {Variables.MMI_M_LEVEL_FLAG.MarkedLevel};
            EVC20_MMISelectLevel.MMI_M_INHIBITED_LEVEL = new Variables.MMI_M_INHIBITED_LEVEL[]
                {Variables.MMI_M_INHIBITED_LEVEL.NotInhibited};
            EVC20_MMISelectLevel.MMI_M_INHIBIT_ENABLE = new Variables.MMI_M_INHIBIT_ENABLE[]
                {Variables.MMI_M_INHIBIT_ENABLE.AllowedForInhibiting};
            EVC20_MMISelectLevel.MMI_M_LEVEL_NTC_ID = new Variables.MMI_M_LEVEL_NTC_ID[]
                {Variables.MMI_M_LEVEL_NTC_ID.AWS_TPWS};
            EVC20_MMISelectLevel.Send();
            DmiActions.ShowInstruction(this, "Select level AWS TPWS");

            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.NationalSystem;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Level = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_LEVEL.LNTC;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in SN mode, Level AWS TPWS." + Environment.NewLine +
                                "2. The Planning Area is removed from area D.");

            MakeTestStepHeader(3, UniqueIdentifier++, "End of test", "");

            /*
            Test Step 3
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
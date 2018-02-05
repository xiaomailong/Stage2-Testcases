using System;
using Testcase.Telegrams.EVCtoDMI;

namespace Testcase.DMITestCases
{
    /// <summary>
    /// 20.2.5 ETCS Level: ETCS Level Transitions by receiving data packet from ETCS Onboard (L2->L3, L3->L2)
    /// TC-ID: 15.2.5
    /// 
    /// This test case verifes a symbol which should be display immediately after passed the level transition area.
    /// 
    /// Tested Requirements:
    /// MMI_gen 7025 (partly: 2nd bullet, #4); MMI_gen 9430 (partly: LE12, LE14);
    /// 
    /// Scenario:
    /// Drive the train forward pass BG1 and receives MA with transition order from RBC. Then, verifie the display information.BG1: No packet, RBC: Message 3 (with optional packet 15, 21, 27 and 41)Drive the train forward pass BG2 and receives MA with transition order from RBC. Then, verifie the display information.BG2: No packet, RBC: Message 3 (with optional packet 15, 21, 27 and 41)
    /// 
    /// Used files:
    /// 15_2_5.tdg, 15_2_5.utt
    /// </summary>
    public class TC_ID_15_2_5_ETCS_Level : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:

            // Call the TestCaseBase PreExecution
            base.PreExecution();

            // -     System is power ON.-     SoM is performed in SR mode, Level 2
            DmiActions.Complete_SoM_L1_SR(this);
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Level = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_LEVEL.L2;
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in FS mode, Level 2

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 0;
            // Testcase entrypoint

            MakeTestStepHeader(1, UniqueIdentifier++, "Drive the train forward pass BG1",
                "Verify the following information,(1)     DMI display symbol LE14 in sub-area C1.(2)    Use the log file to confirm that DMI receives packet information EVC-8 with the following variables,MMI_Q_TEXT = 276MMI_Q_TEXT_CRITIRIA = 3MMI_N_TEXT = 1MMI_X_TEXT = 3");
            /*
            Test Step 1
            Action: Drive the train forward pass BG1
            Expected Result: Verify the following information,(1)     DMI display symbol LE14 in sub-area C1.(2)    Use the log file to confirm that DMI receives packet information EVC-8 with the following variables,MMI_Q_TEXT = 276MMI_Q_TEXT_CRITIRIA = 3MMI_N_TEXT = 1MMI_X_TEXT = 3
            Test Step Comment: (1) MMI_gen 9430 (partly: LE14);(2) MMI_gen 7025 (partly: 2nd bullet, #4, non-Ack Level 3 transition);
            */
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.PlainTextMessage = "3";
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 276;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Level transition symbol, LE14, in sub-area C1.");

            MakeTestStepHeader(2, UniqueIdentifier++, "Drive the train forward pass a distance to level transition",
                "Level transition from Level 2 to Level 3");
            /*
            Test Step 2
            Action: Drive the train forward pass a distance to level transition
            Expected Result: Level transition from Level 2 to Level 3
            */
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Level = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_LEVEL.L3;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in Level 3.");

            MakeTestStepHeader(3, UniqueIdentifier++, "Drive the train forward pass BG2",
                "Verify the following information,(1)     DMI display symbol LE12 in sub-area C1.(2)    Use the log file to confirm that DMI receives packet information EVC-8 with the following variables,MMI_Q_TEXT = 276MMI_Q_TEXT_CRITIRIA = 3MMI_N_TEXT = 1MMI_X_TEXT = 2");
            /*
            Test Step 3
            Action: Drive the train forward pass BG2
            Expected Result: Verify the following information,(1)     DMI display symbol LE12 in sub-area C1.(2)    Use the log file to confirm that DMI receives packet information EVC-8 with the following variables,MMI_Q_TEXT = 276MMI_Q_TEXT_CRITIRIA = 3MMI_N_TEXT = 1MMI_X_TEXT = 2
            Test Step Comment: (1) MMI_gen 9430 (partly: LE12);(2) MMI_gen 7025 (partly: 2nd bullet, #4, non-Ack Level 2 transition);
            */
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.PlainTextMessage = "2";
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 276;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Level transition symbol, LE12, in sub-area C1.");

            MakeTestStepHeader(4, UniqueIdentifier++, "Drive the train forward pass a distance to level transition",
                "Level transition from Level 3 to Level 2");
            /*
            Test Step 4
            Action: Drive the train forward pass a distance to level transition
            Expected Result: Level transition from Level 3 to Level 2
            */
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Level = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_LEVEL.L2;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in Level 2.");

            MakeTestStepHeader(5, UniqueIdentifier++, "End of test", "");

            /*
            Test Step 5
            Action: End of test
            Expected Result: 
            */


            return GlobalTestResult;
        }
    }
}
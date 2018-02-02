using System;
using Testcase.Telegrams.EVCtoDMI;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 20.1.4 Mode acknowledgement subtitution
    /// TC-ID: 15.1.4
    /// 
    /// This test case verify a subtitution of mode acknowledgement symbol when mode acknowledgement symbol is display after level announcement symbol.
    /// 
    /// Tested Requirements:
    /// MMI_gen 11234;
    /// 
    /// Scenario:
    /// Drive the train forward pass BG1 and press an acknowledgement on sub-area C1.BG1: packet 41 (Transition to Level 0).Drive the forward pass BG
    /// 2.Then, verify the mode acknowledgment subtitution on sub-area C1.BG2: packet 12, 21, 27 and 80 (Entering FS with acknowledgement of OS mode)Press an acknowledgement on sub-area C
    /// 1.Then, verify that the Level annoucement wihtout acknownledgement symbol is displays on sub-area C1 after mode acknowledgement symbol is disappearred.
    /// 
    /// Used files:
    /// 15_1_4.tdg
    /// </summary>
    public class TC_15_1_4_ETCS_Mode_Symbols : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // System is power ON.SoM is perform in SR mode, Level 1.

            // Call the TestCaseBase PreExecution

            base.PreExecution();
            DmiActions.Complete_SoM_L1_SR(this);
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint

            #region Test Step 1

            /*
            Test Step 1
            Action: Drive the train forward passing BG1.
            Then, press an area C1 for acknowledgement
            Expected Result: DMI displays LE07 symbol in sub-area C1
            */

            DmiActions.Drive_train_forward_passing_BG1(this);

            DmiActions.Send_L0_Announcement_Ack(this);
            DmiExpectedResults.L0_Announcement_Ack_Requested(this);

            DmiActions.ShowInstruction(this, @"Perform the following actions on the DMI: " + Environment.NewLine +
                                             Environment.NewLine +
                                             "1. Press DMI Sub Area C1." + Environment.NewLine +
                                             "2. Press OK on THIS window within 3 seconds.");

            // Spec displays LE06... (following ack of LE07")
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Level = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_LEVEL.L0;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMi displays symbol LE06 in sub-area C1.");

            #endregion

            #region Test Step 2

            /*
            Test Step 2
            Action: Continue to drive the train forward pass BG2.Then, stop the train
            Expected Result: DMI displays in FS mode, Level 1.
            Verify the following information,
            (1)   The symbol MO08 is displayed for On sight acknowledegement in sub-area C1
            Test Step Comment: (1) MMI_gen 11234 (partly: subtituted);
            */

            DmiActions.Drive_train_forward_passing_BG2(this);

            DmiActions.Send_OS_Mode_Ack(this);
            DmiExpectedResults.OS_Mode_Ack_Requested(this);
            WaitForVerification("Has the LE06 symbol disappeared and been replaced with MO08 symbol in sub-area C1?");

            #endregion

            #region Test Step 3

            /*
            Test Step 3
            Action: Press an area C1 for acknowledgement
            Expected Result: Verify the following information,
            (1)   The symbol MO08 is disappear and DMI displays LE07 symbol instead
            Test Step Comment: (1) MMI_gen 11234 (partly: driver acknowledge);
            */
            DmiActions.ShowInstruction(this, @"Perform the following actions on the DMI: " + Environment.NewLine +
                                             Environment.NewLine +
                                             "1. Press DMI Sub Area C1." + Environment.NewLine +
                                             "2. Press OK on THIS window within 3 seconds.");

            WaitForVerification("Check the following: " + Environment.NewLine +
                                "1. Has the MO08 symbol disappeared from sub-area C1?" + Environment.NewLine +
                                "2. Is the LE06 symbol displayed in sub-area C1?");

            #endregion

            #region Test Step 4

            /*
            Test Step 4
            Action: End of test
            Expected Result: 
            */

            #endregion

            return GlobalTestResult;
        }
    }
}
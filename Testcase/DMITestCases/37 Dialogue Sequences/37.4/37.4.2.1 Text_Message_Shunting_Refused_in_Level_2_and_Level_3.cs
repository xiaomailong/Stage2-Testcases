using System;
using Testcase.Telegrams.EVCtoDMI;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 37.4.2.1 Text Message “Shunting Refused” in Level 0 and Level 
    /// TC-ID: 34.4.2.1
    /// 
    /// This test case verifies the display of text message “SH refused” in ETCS level 2 and 3 when DMI received message 27 from radio connection.
    /// 
    /// Tested Requirements:
    /// MMI_gen 11915 (partly: SH refused); MMI_gen 134 (partly: E5);
    /// 
    /// Scenario:
    /// 1.Enter SH mode, level 
    /// 2.Then, verify the display of text message “SH refused”.2.Restart test system.
    /// 3.Enter SH mode, level 
    /// 3.Then, verify the display of text message “SH refused”.
    /// 
    /// Used files:
    /// 34_4_2_1.utt
    /// </summary>
    public class TC_34_4_2_1_Dialogue_Sequences : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:

            // Call the TestCaseBase PreExecution
            base.PreExecution();

            // Set train running number, cab 1 active, and other defaults
            DmiActions.Activate_Cabin_1(this);

            // Set driver ID
            DmiActions.Set_Driver_ID(this, "1234");

            // Set to level 1
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Level = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_LEVEL.L2;

            // Enable standard buttons including Start, and display Default window.
            DmiActions.Finished_SoM_Default_Window(this);
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in SR mode, level 3

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 0;
            // Testcase entrypoint

            MakeTestStepHeader(1, UniqueIdentifier++,
                "Enter SH mode by performing the procedure below,Press ‘Main’ buttonPress and hold ‘Shunting’ button at least 2 seconds.Release ‘Shunting’ button",
                "DMI displays Main window with text message ‘Shunting refused’ in sub-area E5");
            /*
            Test Step 1
            Action: Enter SH mode by performing the procedure below,Press ‘Main’ buttonPress and hold ‘Shunting’ button at least 2 seconds.Release ‘Shunting’ button
            Expected Result: DMI displays Main window with text message ‘Shunting refused’ in sub-area E5
            Test Step Comment: MMI_gen 11915 (partly: SH refused); MMI_gen 134 (partly: E5);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this,
                @"Press ‘Main’ button, then press and hold ‘Shunting’ button for at least 2 seconds. Release the ‘Shunting’ button");

            EVC8_MMIDriverMessage.MMI_Q_TEXT = 290;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the main window with text message ‘Shunting refused’ in sub-area E5.");
            MakeTestStepHeader(2, UniqueIdentifier++,
                "Re-validate the step1 by re-starting OTE Simulator and starting the precondition with ETCS level 3",
                "See the expected results at Step 1");
            /*
            Test Step 2
            Action: Re-validate the step1 by re-starting OTE Simulator and starting the precondition with ETCS level 3
            Expected Result: See the expected results at Step 1
            */
            // Restart
            DmiActions.ShowInstruction(this, "Power down the system, wait 10s then power up the system");
            DmiActions.Start_ATP();
            // Set train running number, cab 1 active, and other defaults
            DmiActions.Activate_Cabin_1(this);
            // Set driver ID
            DmiActions.Set_Driver_ID(this, "1234");
            // Set to level 3 and SH mode
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Level = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_LEVEL.L3;

            DmiActions.ShowInstruction(this,
                @"Press and hold ‘Shunting’ button for at least 2 seconds. Release ‘Shunting’ button");

            EVC8_MMIDriverMessage.MMI_Q_TEXT = 290;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;

            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the main window with text message ‘Shunting refused’ in sub-area E5.");

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
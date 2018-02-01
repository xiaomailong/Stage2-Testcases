using System;
using Testcase.Telegrams.EVCtoDMI;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 27.5.4 Level Selection window: 8 STMs handling
    /// TC-ID: 22.5.4 
    /// 
    /// This test case is to verify that DMI can handle up to 8 STMs. 
    /// 
    /// Tested Requirements:
    /// MMI_gen 1077;
    /// 
    /// Scenario:
    /// 1.Power on the system and activate cabin.
    /// 2.Enter Driver ID and skip the brake test.
    /// 3.Verify that 8 STMs are available on the level selection window.
    /// 
    /// Used files:
    /// N/A
    /// </summary>
    public class TC_22_5_4_Level_Window : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // Power off the system Configure atpcu configuration file as following (See the instruction in Appendix 2)M_InstalledLevels = 4082NID_NTC_Installe_0 = 1NID_NTC_Installe_1 = 20NID_NTC_Installe_2 = 28NID_NTC_Installe_3 = 9NID_NTC_Installe_4 = 6NID_NTC_Installe_5 = 10NID_NTC_Installe_6 = 22NID_NTC_Installe_7 = 0

            // Call the TestCaseBase PreExecution
            base.PreExecution();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            //  DMI Displays in SB mode.

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 0;
            // Testcase entrypoint
            TraceInfo("This test case requires an ATP configuration change - " +
                      "See Precondition requirements. If this is not done manually, the test may fail!");
            // M_InstalledLevels = 4082NID_NTC_Installe_0 = 1NID_NTC_Installe_1 = 20NID_NTC_Installe_2 = 28NID_NTC_Installe_3 = 9
            // NID_NTC_Installe_4 = 6NID_NTC_Installe_5 = 10NID_NTC_Installe_6 = 22NID_NTC_Installe_7 = 0

            TraceHeader("Test Step 1");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Power on the system and activate the cabin");
            TraceReport("Expected Result");
            TraceInfo("DMI displays Driver ID window in SB mode");
            /*
            Test Step 1
            Action: Power on the system and activate the cabin
            Expected Result: DMI displays Driver ID window in SB mode
            */
            DmiActions.Start_ATP();
            DmiActions.Activate_Cabin_1(this);

            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.StandBy;
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.DriverID;
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Main;
            EVC30_MMIRequestEnable.Send();
            DmiActions.Set_Driver_ID(this, "1234");

            DmiExpectedResults.Driver_ID_window_displayed_in_SB_mode(this);

            TraceHeader("Test Step 2");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Enetr Driver ID and skip brake test");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following information,The level selection window displays 8 folllowing STMs accroding to configuration settingATBTPWS/AWSTBL1+PZB/LZBPZBLZBATC2ASFA");
            /*
            Test Step 2
            Action: Enetr Driver ID and skip brake test
            Expected Result: Verify the following information,The level selection window displays 8 folllowing STMs accroding to configuration settingATBTPWS/AWSTBL1+PZB/LZBPZBLZBATC2ASFA
            Test Step Comment: MMI_gen 1077;
            */
            DmiActions.ShowInstruction(this, "Enter Driver ID and confirm.");


            EVC20_MMISelectLevel.MMI_Q_CLOSE_ENABLE = Variables.MMI_Q_CLOSE_ENABLE.Disabled;

            EVC20_MMISelectLevel.MMI_Q_LEVEL_NTC_ID = new Variables.MMI_Q_LEVEL_NTC_ID[]
                {Variables.MMI_Q_LEVEL_NTC_ID.ETCS_Level, Variables.MMI_Q_LEVEL_NTC_ID.ETCS_Level};

            EVC20_MMISelectLevel.MMI_M_CURRENT_LEVEL = new Variables.MMI_M_CURRENT_LEVEL[]
                {Variables.MMI_M_CURRENT_LEVEL.NotLastUsedLevel, Variables.MMI_M_CURRENT_LEVEL.NotLastUsedLevel};

            EVC20_MMISelectLevel.MMI_M_LEVEL_FLAG = new Variables.MMI_M_LEVEL_FLAG[]
                {Variables.MMI_M_LEVEL_FLAG.MarkedLevel, Variables.MMI_M_LEVEL_FLAG.MarkedLevel};

            EVC20_MMISelectLevel.MMI_M_INHIBITED_LEVEL = new Variables.MMI_M_INHIBITED_LEVEL[]
                {Variables.MMI_M_INHIBITED_LEVEL.NotInhibited, Variables.MMI_M_INHIBITED_LEVEL.NotInhibited};

            EVC20_MMISelectLevel.MMI_M_INHIBIT_ENABLE = new Variables.MMI_M_INHIBIT_ENABLE[]
            {
                Variables.MMI_M_INHIBIT_ENABLE.AllowedForInhibiting, Variables.MMI_M_INHIBIT_ENABLE.AllowedForInhibiting
            };

            EVC20_MMISelectLevel.MMI_M_LEVEL_NTC_ID = new Variables.MMI_M_LEVEL_NTC_ID[]
                {Variables.MMI_M_LEVEL_NTC_ID.AWS_TPWS, Variables.MMI_M_LEVEL_NTC_ID.CBTC};
            EVC20_MMISelectLevel.Send();

            // The STMs will probably differ from these: check and alter!
            TraceInfo("The STMs displayed depend on the configuration - this test should not be automatically failed.");

            WaitForVerification("Check that the following STMs are displayed:" + Environment.NewLine +
                                Environment.NewLine +
                                "1. TPWS/AWS" + Environment.NewLine +
                                "2. CBTC");

            TraceHeader("Test Step 3");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("End of test");
            TraceReport("Expected Result");
            TraceInfo("");
            /*
            Test Step 3
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
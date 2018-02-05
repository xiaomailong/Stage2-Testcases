using System;
using Testcase.Telegrams.EVCtoDMI;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 17.2.2 Speed Dial: Display Train maxinum speed
    /// TC-ID: 12.2.2
    /// 
    /// This test case is to verify that DMI provide the full service train speed up to 550 Km/h and possible to configure lower values lfor specific project requirements. 
    /// 
    /// Tested Requirements:
    /// MMI_gen 67;
    /// 
    /// Scenario:
    /// 1.Power on the system and activate cabin.
    /// 2.Perform SoM in SR mode, level 1.
    /// 3.Verify the maximum speed display on DMI is align with the configuration setting.
    /// 
    /// Used files:
    /// N/A
    /// </summary>
    public class TC_ID_12_2_2_Train_Speed : TestcaseBase
    {
        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays SR mode.
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in SR mode, Level 1.");

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

            MakeTestStepHeader(1, UniqueIdentifier++, "Power on the system and activate the cabin",
                "DMI displays SB mode");
            /*
            Test Step 1
            Action: Power on the system and activate the cabin
            Expected Result: DMI displays SB mode
            */
            DmiActions.Start_ATP();

            // Set train running number, cab 1 active, and other defaults
            DmiActions.Activate_Cabin_1(this);

            // Set driver ID
            DmiActions.Set_Driver_ID(this, "1234");

            // Set to level 1 and SR mode
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Level = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_LEVEL.L1;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.StandBy;

            // Call generic Check Results Method
            DmiExpectedResults.SB_Mode_displayed(this);

            MakeTestStepHeader(2, UniqueIdentifier++, "Perform SoM to  SR mode, level 1",
                "Mode changes to SR mode , level 1Verify the following information:The speed dial displays 550 km/h as a mixminum speed");
            /*
            Test Step 2
            Action: Perform SoM to  SR mode, level 1
            Expected Result: Mode changes to SR mode , level 1Verify the following information:The speed dial displays 550 km/h as a mixminum speed
            Test Step Comment: MMI_gen 67 (partly:550 km/h);
            */
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Level = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_LEVEL.L1;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode =
                EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.StaffResponsible;
            DmiActions.Finished_SoM_Default_Window(this);

            WaitForVerification("Check the following" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in SR mode, level 1." + Environment.NewLine +
                                "2. The speed dial displays 550 km/h maximum speed");

            MakeTestStepHeader(3, UniqueIdentifier++,
                "Change the configuration: SPEED_DIAL_V_MAX  to 200, 300 and 400 then retest with step 1 to 2",
                "Verify the following information:The speed dial displays the maxinum speed accroding to configuration setting");
            /*
            Test Step 3
            Action: Change the configuration: SPEED_DIAL_V_MAX  to 200, 300 and 400 then retest with step 1 to 2
            Expected Result: Verify the following information:The speed dial displays the maxinum speed accroding to configuration setting
            Test Step Comment: MMI_gen 67 (partly: configure lower values);
            */
            DmiActions.ShowInstruction(this,
                "Power down the system and reconfigure speed dial properties, then power up:" + Environment.NewLine +
                "SPEED_DIAL_V_MAX = 200");

            DmiActions.Start_ATP();

            // Set train running number, cab 1 active, and other defaults
            DmiActions.Activate_Cabin_1(this);

            // Set driver ID
            DmiActions.Set_Driver_ID(this, "1234");

            // Set to level 1 and SB mode
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Level = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_LEVEL.L1;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.StandBy;

            // Call generic Check Results Method
            DmiExpectedResults.SB_Mode_displayed(this);

            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode =
                EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.StaffResponsible;
            DmiActions.Finished_SoM_Default_Window(this);

            WaitForVerification("Check the following" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in SR mode, level 1." + Environment.NewLine +
                                "2. The speed dial displays 200 km/h maximum speed");

            DmiActions.ShowInstruction(this,
                "Power down the system and reconfigure speed dial properties, then power up:" + Environment.NewLine +
                "SPEED_DIAL_V_MAX = 300");

            DmiActions.Start_ATP();

            // Set train running number, cab 1 active, and other defaults
            DmiActions.Activate_Cabin_1(this);

            // Set driver ID
            DmiActions.Set_Driver_ID(this, "1234");

            // Set to level 1 and SR mode
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Level = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_LEVEL.L1;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.StandBy;

            // Call generic Check Results Method
            DmiExpectedResults.SB_Mode_displayed(this);

            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode =
                EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.StaffResponsible;
            DmiActions.Finished_SoM_Default_Window(this);

            WaitForVerification("Check the following" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in SR mode, level 1." + Environment.NewLine +
                                "2. The speed dial displays 300 km/h maximum speed");

            DmiActions.ShowInstruction(this,
                "Power down the system and reconfigure speed dial properties, then power up:" + Environment.NewLine +
                "SPEED_DIAL_V_MAX = 400");

            DmiActions.Start_ATP();

            // Set train running number, cab 1 active, and other defaults
            DmiActions.Activate_Cabin_1(this);

            // Set driver ID
            DmiActions.Set_Driver_ID(this, "1234");

            // Set to level 1 and SR mode
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Level = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_LEVEL.L1;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.StandBy;

            // Call generic Check Results Method
            DmiExpectedResults.SB_Mode_displayed(this);

            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode =
                EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.StaffResponsible;
            DmiActions.Finished_SoM_Default_Window(this);

            WaitForVerification("Check the following" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in SR mode, level 1." + Environment.NewLine +
                                "2. The speed dial displays 400 km/h maximum speed");
            MakeTestStepHeader(4, UniqueIdentifier++, "End of test", "");

            /*
            Test Step 4
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
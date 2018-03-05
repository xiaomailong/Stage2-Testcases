using System;
using Testcase.Telegrams.EVCtoDMI;

namespace Testcase.DMITestCases
{
    /// <summary>
    /// 20.2.2 ETCS Level in Sub-Area C8: Level 2 and Level 3 by driver selection
    /// TC-ID: 15.2.2
    /// 
    /// This test case verifies the appearance of ETCS Level symbol shall display in sub-area C8. The ETCS Level symbols shall comply with [MMI-ETCS-gen], [GenVSIS] and [ERA] standard.
    /// 
    /// Tested Requirements:
    /// MMI_gen 577 (partly: Level 2 and Level 3); MMI_gen 11470 (partly: Bit # 36 and 37);
    /// 
    /// Scenario:
    /// Perform SoM to SR mode , Level 
    /// 2.Then, verify that the level 2 ”LE04” symbol displays at sub-area C
    /// 8.Repeat the test steps with Level 
    /// 3.Then, verify that the level 3 ”LE05” symbol displays at sub-area C8.
    /// 
    /// Used files:
    /// 15_2_2.utt
    /// </summary>
    public class TC_15_2_2_ETCS_Level : TestcaseBase
    {
        public override bool TestcaseEntryPoint()
        {
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 22380;
            // Testcase entrypoint
            StartUp();

            DmiActions.Display_Driver_ID_Window(this);
            DmiActions.Display_Driver_ID_Window(this, "1234");
            DmiActions.Send_SB_Mode(this);
            DmiActions.ShowInstruction(this, @"Perform the following action within 3 seconds after pressing OK : " +
                                             Environment.NewLine + Environment.NewLine +
                                             "1. Enter and confirm Driver ID on the DMI.");

            DmiActions.Request_Brake_Test(this);
            DmiActions.ShowInstruction(this, @"Perform the following action within 3 seconds after pressing OK : " +
                                             Environment.NewLine + Environment.NewLine +
                                             "1. Perform Brake Test");

            DmiActions.Display_Level_Window(this);

            #region Test step 1

            MakeTestStepHeader(1, UniqueIdentifier++, "Select and confirm Level 2", "DMI displays RBC contact window.");
            /*
            Test Step 1
            Action: Select and confirm Level 2
            Expected Result: DMI displays RBC contact window.
            Verify the following information, The symbol LE04 is displayed in sub-area C8.
            Use the log file to confirm that DMI received packet EVC-7 with variable OBU_TR_M_LEVEL = 3 (Level 2).
            Use the log file to confirm that DMI sends out packet [MMI_DRIVER_ACTION (EVC-152)] with the value of variable MMI_M_DRIVER_ACTION refer to sequence below,
            a)   MMI_M_DRIVER_ACTION = 36 (level 2 selected)
            Test Step Comment: (1) MMI_gen 577 (partly: Level is valid and equal to 2, symbol LE04, displayed in area C8);
                               (2) MMI_gen 577 (partly: Level is valid, Derived from variable,Level 2);
                               (3) MMI_gen 11470 (partly: Bit # 36);
            */

            DmiActions.ShowInstruction(this, @"Perform the following action within 3 seconds after pressing OK : " +
                                             Environment.NewLine + Environment.NewLine +
                                             "1. Select and enter Level 2");

            DmiExpectedResults.Level_2_Selected(this);

            DmiActions.Display_RBC_Contact_Window_Data_Unknown(this);
            DmiActions.Send_L2(this);

            DmiExpectedResults.RBC_Contact_Window_displayed(this);
            DmiExpectedResults.Driver_symbol_displayed(this, "Level 2", "LE04", "C8", true);

            #endregion

            #region Test Step 2

            MakeTestStepHeader(2, UniqueIdentifier++, "Restart OTE and RBC Simulator.",
                "DMI displays RBC contact window.");
            /*
            Test Step 2
            Action: Restart OTE and RBC Simulator.
            Repeat action step 1 with Level 3
            Expected Result: DMI displays RBC contact window.
            Verify the following information, The symbol LE05 is displayed in sub-area C8.
            Use the log file to confirm that DMI received packet EVC-7 with variable OBU_TR_M_LEVEL = 4 (Level 3).
            Use the log file to confirm that DMI sends out packet [MMI_DRIVER_ACTION (EVC-152)] 
            with the value of variable MMI_M_DRIVER_ACTION refer to sequence below,
            a)   MMI_M_DRIVER_ACTION = 37 (level 3 selected)
            Test Step Comment: (1) MMI_gen 577 (partly: Level is valid and equal to 3, symbol LE05, displayed in area C8);
                               (2) MMI_gen 577 (partly: Level is valid, Derived from variable,Level 3);
                               (3) MMI_gen 11470 (partly: Bit # 37);
            */

            Trace("Test Step skipped because Level 3 is out of scope.");

            #endregion

            #region Test Step 3

            TraceHeader("End of test");

            /*
            Test Step 3
            Action: End of test
            Expected Result: 
            */

            #endregion

            return GlobalTestResult;
        }
    }
}
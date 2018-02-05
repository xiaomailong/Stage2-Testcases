using System;
using Testcase.Telegrams.EVCtoDMI;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 20.2.10 ETCS Level: ETCS Level Transitions by receiving data packet from ETCS Onboard (L0->LNTC)
    /// TC-ID: 15.2.10
    /// 
    /// This test case verifies the symbol that displays after passed the level transition announcement from Level 0 to level NTC and then acknowledgement by driver 
    /// 
    /// Tested Requirements:
    /// MMI_gen 9430 (partly: LE08); MMI_gen 9431 (partly: LE08 and LE09);
    /// 
    /// Scenario:
    /// 1.Perform start of mission to Unfitted mode, level 
    /// 02.Drive the train forward pass BG0 at position 10m with Pkt 41: level transition announcement to ATB STM. Verify that LE08 symbol displays in sub-area C1.
    /// 3.Pass the acknowledgement area. Then, verify that LE09 symbol is displayed in sub-area C
    /// 14.Acknowledge the level transition. Then, verify that LE08 is displayed in sub-area C1 
    /// 5.Pass BG1 at position 400m which is the level transition border then mode changes to ATB STM, Level NTC
    /// 
    /// Used files:
    /// 15_2_10.tdg
    /// </summary>
    public class TC_15_2_10_ETCS_Level : TestcaseBase
    {

        public override bool TestcaseEntryPoint()
        {
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 0;
            // Testcase entrypoint
            TraceInfo("This test case requires an ATP configuration change - " +
                      "See Precondition requirements. If this is not done manually, the test may fail!");

            MakeTestStepHeader(1, UniqueIdentifier++,
                "Perform the following action:         Power on the systemActivate the cabin Perform start of mission to Unfitted mode , Level 0",
                "DMI displays Unfitted mode, Level 0");
            /*
            Test Step 1
            Action: Perform the following action:         Power on the systemActivate the cabin Perform start of mission to Unfitted mode , Level 0
            Expected Result: DMI displays Unfitted mode, Level 0
            */
            
            StartUp();
            DmiActions.Set_Driver_ID(this, "1234");
            // Skip brake test...
            // Set to level 0 and UN mode
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Level = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_LEVEL.L0;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.Unfitted;
            DmiActions.Finished_SoM_Default_Window(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in UN mode, Level 0.");

            MakeTestStepHeader(2, UniqueIdentifier++,
                "Drive the train forward with 30 km/h then pass BG0 with level transition announcement",
                "DMI displays LE08 symbol in sub-area C1");
            /*
            Test Step 2
            Action: Drive the train forward with 30 km/h then pass BG0 with level transition announcement
            Expected Result: DMI displays LE08 symbol in sub-area C1
            Test Step Comment: MMI_gen 9430 (partly:Negative LE08); 
            */
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 277;
            EVC8_MMIDriverMessage.PlainTextMessage = "0";
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays symbol LE08 in sub-area C1");

            MakeTestStepHeader(3, UniqueIdentifier++, "Pass the level transition acknowledgement area",
                "DMI displays LE09 symbol in sub-area C1");
            /*
            Test Step 3
            Action: Pass the level transition acknowledgement area
            Expected Result: DMI displays LE09 symbol in sub-area C1
            Test Step Comment: MMI_gen 9431 (partly: LE09); 
            */
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 258;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays symbol LE09 in sub-area C1");

            MakeTestStepHeader(4, UniqueIdentifier++, "Press acknowledgement LE09 symbol in sub-area C1",
                "DMI replaces LE09 symbol with LE08 in sub-area C1");
            /*
            Test Step 4
            Action: Press acknowledgement LE09 symbol in sub-area C1
            Expected Result: DMI replaces LE09 symbol with LE08 in sub-area C1
            Test Step Comment: MMI_gen 9431 (partly: LE08);
            */
            DmiActions.ShowInstruction(this, "Press in sub-area C1 to confirm the level");

            EVC8_MMIDriverMessage.MMI_Q_TEXT = 277;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays symbol LE08 in sub-area C1");

            MakeTestStepHeader(5, UniqueIdentifier++, "Pass BG1 at level transition border",
                "Mode changes to ATB STM mode, Level NTC");
            /*
            Test Step 5
            Action: Pass BG1 at level transition border
            Expected Result: Mode changes to ATB STM mode, Level NTC
            */
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 4;
            EVC8_MMIDriverMessage.Send();
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Level = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_LEVEL.LNTC;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in SN mode, Level NTC.");

            MakeTestStepHeader(6, UniqueIdentifier++, "End of test", "");

            /*
            Test Step 6
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
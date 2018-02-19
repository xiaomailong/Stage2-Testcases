using System;
using Testcase.Telegrams.DMItoEVC;
using Testcase.Telegrams.EVCtoDMI;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 20.2.13 ETCS Level: ETCS Level Transitions by receiving data packet from ETCS Onboard (LNTC ->L3)
    /// TC-ID: 15.2.13
    /// 
    /// This test case verifies the symbol that displays after passed the level transition announcement for level NTC to level 3 and acknowledgement by driver 
    /// 
    /// Tested Requirements:
    /// MMI_gen 9430 (partly: LE14); MMI_gen 9431 (partly: LE14 and LE15); MMI_gen 11470 (partly: Bit #9);
    /// 
    /// Scenario:
    /// 1.Perform start of mission to ATB STM mode, level NTC
    /// 2.Drive the train forward pass BG0 at position 10m with Pkt 41: level transition announcement to Level 
    /// 3.Verify that LE14 symbol displays in sub-area C1.
    /// 3.Pass the acknowledgement area. Then, verify that LE15 symbol is displayed in sub-area C1.
    /// 4.Acknowledge the level transition and then verify that LE14 is displayed in sub-area C1 
    /// 5.Pass BG1 at position 400m which is level transition border then mode changes to FS mode, Level 3
    /// 
    /// Used files:
    /// 15_2_13.tdg, 15_2_13.utt
    /// </summary>
    public class TC_ID_15_2_13_ETCS_Level : TestcaseBase
    {
        public override bool TestcaseEntryPoint()
        {
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 22543;
            // Testcase entrypoint
            TraceInfo("This test case requires an ATP configuration change - " +
                      "See Precondition requirements. If this is not done manually, the test may fail!");

            MakeTestStepHeader(1, UniqueIdentifier++,
                "Perform the following action:         Power on the systemActivate the cabin Perform start of mission to ATB STM mode , Level NTC",
                "DMI displays in ATB STM mode, Level NTC");
            /*
            Test Step 1
            Action: Perform the following action:         Power on the systemActivate the cabin Perform start of mission to ATB STM mode , Level NTC
            Expected Result: DMI displays in ATB STM mode, Level NTC
            */
            // Call generic Action Method
            StartUp();
            DmiActions.Set_Driver_ID(this, "1234");
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.NationalSystem;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Level = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_LEVEL.LNTC;
            DmiActions.Finished_SoM_Default_Window(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in SN mode, Level NTC");

            MakeTestStepHeader(2, UniqueIdentifier++,
                "Drive the train forward with 30 km/h and then pass BG0 with level transition announcement",
                "DMI displays LE14 symbol in sub-area C1");
            /*
            Test Step 2
            Action: Drive the train forward with 30 km/h and then pass BG0 with level transition announcement
            Expected Result: DMI displays LE14 symbol in sub-area C1
            Test Step Comment: MMI_gen 9430 (partly:Negative LE14); 
            */
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.PlainTextMessage = "3";
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 276;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Level transition symbol, LE14, in sub-area C1.");

            MakeTestStepHeader(3, UniqueIdentifier++, "Pass the level transition acknowledgement area",
                "DMI displays LE15 symbol in sub-area C1");
            /*
            Test Step 3
            Action: Pass the level transition acknowledgement area
            Expected Result: DMI displays LE15 symbol in sub-area C1
            Test Step Comment: MMI_gen 9431 (partly: LE15); 
            */
            EVC8_MMIDriverMessage.MMI_I_TEXT = 2;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 257;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Level transition symbol, LE15, in sub-area C1.");

            MakeTestStepHeader(4, UniqueIdentifier++, "Press acknowledgement LE15 symbol in sub-area C1",
                "Verify the following information," +
                "(1) DMI replaces LE15 symbol with LE14 in sub-area C1." +
                "(2) Use the log file to confirm that DMI sends out packet [MMI_DRIVER_ACTION (EVC-152)] with the value of variable MMI_M_DRIVER_ACTION refer to sequence below,a)   MMI_M_DRIVER_ACTION = 9 (Ack level 3)");
            /*
            Test Step 4
            Action: Press acknowledgement LE15 symbol in sub-area C1
            Expected Result: Verify the following information,
            (1) DMI replaces LE15 symbol with LE14 in sub-area C1.
            (2) Use the log file to confirm that DMI sends out packet [MMI_DRIVER_ACTION (EVC-152)] with the value of variable MMI_M_DRIVER_ACTION refer to sequence below,a)   MMI_M_DRIVER_ACTION = 9 (Ack level 3)
            Test Step Comment: (1) MMI_gen 9431 (partly: LE14);(2) MMI_gen 11470 (partly: Bit #9);
            */
            DmiActions.ShowInstruction(this, "Press in sub-area C1 to acknowledge the Level transition symbol, LE15");

            EVC152_MMIDriverAction.Check_MMI_M_DRIVER_ACTION = EVC152_MMIDriverAction.MMI_M_DRIVER_ACTION.Level3Ack;

            EVC8_MMIDriverMessage.MMI_I_TEXT = 3;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 276;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI removes symbol LE15 and displays the Level transition symbol, LE14, in sub-area C1.");

            MakeTestStepHeader(5, UniqueIdentifier++, "Pass BG1 at level transition border",
                "Mode changes to FS mode, Level 3");
            /*
            Test Step 5
            Action: Pass BG1 at level transition border
            Expected Result: Mode changes to FS mode, Level 3
            */
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Level = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_LEVEL.L3;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.FullSupervision;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in FS mode, Level 3.");

            TraceHeader("End of test");

            /*
            Test Step 6
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
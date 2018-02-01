using System;
using Testcase.Telegrams.EVCtoDMI;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 20.2.11 ETCS Level: ETCS Level Transitions by receiving data packet from ETCS Onboard (LNTC ->L1)
    /// TC-ID: 15.2.11
    /// 
    /// This test case verifies the symbol that displays after passed the level transition announcement form level NTC to L1 and then acknowledgement by driver 
    /// 
    /// Tested Requirements:
    /// MMI_gen 9430 (partly: LE10); MMI_gen 9431 (partly: LE10 and LE11);
    /// 
    /// Scenario:
    /// 1.Perform start of mission to ATB STM mode, level NTC
    /// 2.Drive the train forward pass BG0 at position 10m with Pkt 41: level transition announcement to Level 
    /// 1.Verify that LE10 symbol displays in sub-area C1.
    /// 3.Pass the acknowledgement area. Then, verify that LE11 symbol is displayed in sub-area C
    /// 14.Acknowledge the level transition and then verify that LE10 is displayed in sub-area C1 
    /// 5.Pass BG1 at position 400m which is level transition border. Then, mode changes to FS mode, Level 1
    /// 
    /// Used files:
    /// 15_2_11.tdg
    /// </summary>
    public class TC_ID_15_2_11_ETCS_Level : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // System is power OFF.Configure atpcu configuration file as following (See the instruction in Appendix 2)
            // M_InstalledLevels = 31NID_NTC_Installe_0 = 1 (ATB) 

            // Call the TestCaseBase PreExecution
            base.PreExecution();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in FS mode, Level 1

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

            TraceHeader("Test Step 1");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Perform the following action:         Power on the systemActivate the cabin Perform start of mission to ATB STM mode , Level NTC");
            TraceReport("Expected Result");
            TraceInfo("DMI displays in ATB STM mode, Level NTC");
            /*
            Test Step 1
            Action: Perform the following action:         Power on the systemActivate the cabin Perform start of mission to ATB STM mode , Level NTC
            Expected Result: DMI displays in ATB STM mode, Level NTC
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, "Power on the system");

            DmiActions.Start_ATP();
            DmiActions.Activate_Cabin_1(this);
            DmiActions.Set_Driver_ID(this, "1234");
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Level = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_LEVEL.LNTC;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.NationalSystem;
            DmiActions.Finished_SoM_Default_Window(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in SN mode, Level NTC.");

            TraceHeader("Test Step 2");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Drive the train forward with 30 km/h then pass BG0 with level transition announcement");
            TraceReport("Expected Result");
            TraceInfo("DMI displays LE10 symbol in sub-area C1");
            /*
            Test Step 2
            Action: Drive the train forward with 30 km/h then pass BG0 with level transition announcement
            Expected Result: DMI displays LE10 symbol in sub-area C1
            Test Step Comment: MMI_gen 9430 (partly:Negative LE10); 
            */
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.PlainTextMessage = "0";
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 276;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays symbol LE10 in sub-area C1.");

            TraceHeader("Test Step 3");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Pass the level transition acknowledgement area");
            TraceReport("Expected Result");
            TraceInfo("DMI displays LE11 symbol in sub-area C1");
            /*
            Test Step 3
            Action: Pass the level transition acknowledgement area
            Expected Result: DMI displays LE11 symbol in sub-area C1
            Test Step Comment: MMI_gen 9431 (partly: LE11); 
            */
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 257;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays symbol LE11 in sub-area C1.");

            TraceHeader("Test Step 4");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Press acknowledgement LE11 symbol in sub-area C1");
            TraceReport("Expected Result");
            TraceInfo("DMI replaces LE10 symbol with LE11 in sub-area C1");
            /*
            Test Step 4
            Action: Press acknowledgement LE11 symbol in sub-area C1
            Expected Result: DMI replaces LE10 symbol with LE11 in sub-area C1
            Test Step Comment: MMI_gen 9431 (partly: LE10);
            */
            DmiActions.ShowInstruction(this, "Press in sub-area C1 to confirm transition to Level");

            EVC8_MMIDriverMessage.MMI_Q_TEXT = 276;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays symbol LE10 in sub-area C1.");

            TraceHeader("Test Step 5");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Pass BG1 at level transition border");
            TraceReport("Expected Result");
            TraceInfo("Mode changes to FS mode, Level 1");
            /*
            Test Step 5
            Action: Pass BG1 at level transition border
            Expected Result: Mode changes to FS mode, Level 1
            */
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Level = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_LEVEL.L1;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.FullSupervision;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in FS mode, Level 1.");

            TraceHeader("Test Step 6");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("End of test");
            TraceReport("Expected Result");
            TraceInfo("");
            /*
            Test Step 6
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
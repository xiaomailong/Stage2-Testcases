using System;
using Testcase.Telegrams.EVCtoDMI;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 20.2.9 NTC Level :Announcement symbol in Sub-Area C1.
    /// TC-ID: 15.2.9
    /// 
    /// This test case verifies that the level announcement symbol is not presented if the mode acknowledgement symbol for NTC (MO20) still displays in sub-area C1 
    /// 
    /// Tested Requirements:
    /// MMI_gen 9429 (partly: NTC);
    /// 
    /// Scenario:
    /// 1.Perform Start of Misson to ATB STM until train running number entry 
    /// 2.Press the 'Start' button. 
    /// 3.Send packet EVC-8 with level announcement and verify that level announcement symbol is not presented as long as mode acknowledgement symbol still displays.
    /// 
    /// Used files:
    /// 15_2_9_a.xml
    /// </summary>
    public class TC_ID_15_2_9_NTC_Level_Announcement_ : TestcaseBase
    {
        public override bool TestcaseEntryPoint()
        {
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 22480;
            // Testcase entrypoint
            TraceInfo("This test case requires an ATP configuration change - " +
                      "See Precondition requirements. If this is not done manually, the test may fail!");

            MakeTestStepHeader(1, UniqueIdentifier++,
                "Peform following action:Power ON the systemActivate cabin A  perform Start of Mission to ATB STM until train running number entry",
                "DMI displays main window");
            /*
            Test Step 1
            Action: Peform following action:Power ON the systemActivate cabin A  perform Start of Mission to ATB STM until train running number entry
            Expected Result: DMI displays main window
            */
            DmiActions.ShowInstruction(this, "Power on the system");

            StartUp();
            DmiActions.Set_Driver_ID(this, "1234");
            // Skip brake test, train data entry
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.NationalSystem;
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Main;
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.Start;
            EVC30_MMIRequestEnable.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Main window");

            MakeTestStepHeader(2, UniqueIdentifier++, "Press the ‘Start’ button",
                "The acknowledgement for NTC symbol (MO20) is displayed in area C1");
            /*
            Test Step 2
            Action: Press the ‘Start’ button
            Expected Result: The acknowledgement for NTC symbol (MO20) is displayed in area C1
            */
            DmiActions.ShowInstruction(this, "Press the ‘Start’ button");

            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 555;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the ‘Acknowledgement for NTC’ symbol, MO20, in sub-area C1");

            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.NationalSystem;


            MakeTestStepHeader(3, UniqueIdentifier++,
                "Use the test script file 15_2_9_a.xml to send EVC-8 with,MMI_Q_TEXT = 257MMI_Q_TEXT_CRITERIA = 1MMI_N_TEXT = 1MMI_X_TEXT = 0",
                "Verify the following information,The displays in sub-area C1 is not changed, DMI still displays MO20 symbol");
            /*
            Test Step 3
            Action: Use the test script file 15_2_9_a.xml to send EVC-8 with,MMI_Q_TEXT = 257MMI_Q_TEXT_CRITERIA = 1MMI_N_TEXT = 1MMI_X_TEXT = 0
            Expected Result: Verify the following information,The displays in sub-area C1 is not changed, DMI still displays MO20 symbol
            Test Step Comment: MMI_gen 9429 (partly, NTC);
            */

            #region Send_XML_15_2_9_DMI_Test_Specification

            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 257;
            EVC8_MMIDriverMessage.PlainTextMessage = "0";
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following: + " + Environment.NewLine + Environment.NewLine +
                                "1. DMI still displays symbol M020 in sub-area C1.");

            #endregion

            MakeTestStepHeader(4, UniqueIdentifier++, "Confirm NTC", "DMI displays in ATB STM mode, Level NTC");
            /*
            Test Step 4
            Action: Confirm NTC
            Expected Result: DMI displays in ATB STM mode, Level NTC
            */
            DmiActions.ShowInstruction(this, "Confirm NTC Level");

            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Level = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_LEVEL.LNTC;

            WaitForVerification("Check the following: + " + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in SN mode, Level NTC");

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
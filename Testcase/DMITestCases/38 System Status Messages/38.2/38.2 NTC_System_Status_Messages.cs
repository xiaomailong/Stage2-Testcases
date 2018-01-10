using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BT_Tools;
using BT_CSB_Tools;
using BT_CSB_Tools.Logging;
using BT_CSB_Tools.Utils.Xml;
using BT_CSB_Tools.SignalPoolGenerator.Signals;
using BT_CSB_Tools.SignalPoolGenerator.Signals.MwtSignal;
using BT_CSB_Tools.SignalPoolGenerator.Signals.MwtSignal.Misc;
using BT_CSB_Tools.SignalPoolGenerator.Signals.PdSignal;
using BT_CSB_Tools.SignalPoolGenerator.Signals.PdSignal.Misc;
using CL345;
using Testcase.Telegrams.EVCtoDMI;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 38.2 NTC System Status Messages
    /// TC-ID: 35.2
    /// 
    /// This test case verifies the display of NTC system status messages.
    /// 
    /// Tested Requirements:
    /// MMI_gen 9522 (partly: table 77);
    /// 
    /// Scenario:
    /// Perform Start of Mission to ATB STM until train data entry Driver the train forward then verify that the text message “Runaway movement” displays in sub-area E5Turn off STM Simulator and complete start of mission then verify that the text message “ATB failed” and “ATB is not available” display in sub-area E5Restart OTE and STM SimulatorPerform start of mission to PLZB STM until train data entry completely. Verify that PLZB specific train data is requested 
    /// 
    /// Used files:
    /// N/A
    /// </summary>
    public class TC_ID_35_2_NTC_System_Status_Messages : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // Configure atpcu configuration file as following (See the instruction in Appendix 2)
            // M_InstalledLevels = 63
            // NID_NTC_Installe_0 = 1 
            // (ATB)NID_NTC_Installe_1 = 9 
            // (PLZB)Q_CustomConfig = 3

            // Call the TestCaseBase PreExecution
            base.PreExecution();

            // System is power off
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in PLZB STM mode

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint
            TraceInfo("This test case requires an ATP configuration change - " +
                      "See Precondition requirements. If this is not done manually, the test may fail!");

            /*
            Test Step 1
            Action: Power on the system and activate the cabin
            Expected Result: DMI displays Driver ID window in SB mode
            */
            DmiActions.ShowInstruction(this, "Power on the system");
            DmiActions.Activate_Cabin_1(this);
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.StandBy;
            EVC14_MMICurrentDriverID.MMI_Q_CLOSE_ENABLE = Variables.MMI_Q_CLOSE_ENABLE.Disabled;
            EVC14_MMICurrentDriverID.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Driver ID window in SB mode.");

            /*
            Test Step 2
            Action: Perform the following action:Enter Driver IDskip the brake testSelect and comfirm ATB STMSelect Train data
            Expected Result: DMI displays Train data entry window
            */
            DmiActions.ShowInstruction(this, "Enter the Driver ID and confirm");

            EVC20_MMISelectLevel.MMI_Q_CLOSE_ENABLE = Variables.MMI_Q_CLOSE_ENABLE.Disabled;
            EVC20_MMISelectLevel.MMI_Q_LEVEL_NTC_ID = new Variables.MMI_Q_LEVEL_NTC_ID[]
                {Variables.MMI_Q_LEVEL_NTC_ID.ETCS_Level};
            EVC20_MMISelectLevel.MMI_M_CURRENT_LEVEL = new Variables.MMI_M_CURRENT_LEVEL[]
                {Variables.MMI_M_CURRENT_LEVEL.LastUsedLevel};
            EVC20_MMISelectLevel.MMI_M_LEVEL_FLAG = new Variables.MMI_M_LEVEL_FLAG[]
                {Variables.MMI_M_LEVEL_FLAG.MarkedLevel};
            EVC20_MMISelectLevel.MMI_M_INHIBITED_LEVEL = new Variables.MMI_M_INHIBITED_LEVEL[]
                {Variables.MMI_M_INHIBITED_LEVEL.NotInhibited};
            EVC20_MMISelectLevel.MMI_M_INHIBIT_ENABLE = new Variables.MMI_M_INHIBIT_ENABLE[]
                {Variables.MMI_M_INHIBIT_ENABLE.AllowedForInhibiting};
            EVC20_MMISelectLevel.MMI_M_LEVEL_NTC_ID = new Variables.MMI_M_LEVEL_NTC_ID[]
                {Variables.MMI_M_LEVEL_NTC_ID.AWS_TPWS};
            EVC20_MMISelectLevel.Send();

            DmiActions.ShowInstruction(this, "Select and confirm AWS TPWS");

            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Main;
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.TrainData |
                                                               EVC30_MMIRequestEnable.EnabledRequests
                                                                   .TrainRunningNumber;

            DmiActions.ShowInstruction(this, @"Press the ‘Train data’ button.");

            DmiActions.Send_EVC6_MMICurrentTrainData_FixedDataEntry(this, Variables.paramEvc6FixedTrainsetCaptions, 1);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Train data window.");

            /*
            Test Step 3
            Action: Drive the train forward
            Expected Result: Service brake is applied.The text message “Runaway movement” displays in sub-area E5
            Test Step Comment: MMI_gen 9522 (partly: table 77, Runaway movement);
            */
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 269;
            EVC8_MMIDriverMessage.Send();

            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 2;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 260;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the message ‘Runaway movement’ in sub-area E5." + Environment.NewLine +
                                "2. DMI displays the ‘Brake intervention symbol’, ST01 in sub-area C9.");

            /*
            Test Step 4
            Action: Release service brake and complete train data enrty
            Expected Result: DMI displays train running number window
            */
            DmiActions.ShowInstruction(this,
                "Acknowledge brake intervention, complete train data entry, then press the ‘Train running number’ button");

            // Remove runaway message
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 4;
            EVC8_MMIDriverMessage.Send();

            EVC16_CurrentTrainNumber.TrainRunningNumber = 1;
            EVC16_CurrentTrainNumber.Send();

            DmiExpectedResults.TRN_window_displayed(this);

            /*
            Test Step 5
            Action: Turn off STM Simulator and then complete start of mission
            Expected Result: The text message “ATB failed” and “ATB is not avaliable “display in sub-area E5
            Test Step Comment: MMI_gen 9522 (partly: table 77, failed and is not available);
            */
            DmiActions.Simulate_communication_loss_EVC_DMI(this);

            // EVC8? Q_TEXT 543 (#2 failed), 704 (#2 is not available) needed?
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the messages ‘AWS TPWS failed’ and ‘AWS TPWS is not available’ in sub-area E5.");

            /*
            Test Step 6
            Action: Restart OTE and STM Simulator
            Expected Result: OTE and STM Simulator are started
            */
            DmiActions.Re_establish_communication_EVC_DMI(this);

            /*
            Test Step 7
            Action: Start up PZLB STM and then ATPCU
            Expected Result: DMI displays Driver ID window in SB mode
            */
            DmiActions.ShowInstruction(this, "Start up PZLB STM and ATPCU");

            EVC14_MMICurrentDriverID.MMI_Q_CLOSE_ENABLE = Variables.MMI_Q_CLOSE_ENABLE.Disabled;
            EVC14_MMICurrentDriverID.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Driver ID window in SB mode.");

            /*
            Test Step 8
            Action: Perform start of mission to PLZB STM until train data entry completely
            Expected Result: DMI display PLZB specific data window
            Test Step Comment: MMI_gen 9522 (partly: table 77, needed data);
            */
            DmiActions.ShowInstruction(this, "Enter and confirm the Driver ID");

            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Main;
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.TrainData |
                                                               EVC30_MMIRequestEnable.EnabledRequests
                                                                   .TrainRunningNumber;
            EVC30_MMIRequestEnable.Send();

            DmiActions.ShowInstruction(this, @"Press the ‘Train data’ button.");

            DmiActions.Send_EVC6_MMICurrentTrainData_FixedDataEntry(this, Variables.paramEvc6FixedTrainsetCaptions, 1);

            DmiActions.ShowInstruction(this, @"Enter and confirm the Train data.");

            DmiActions.Send_EVC10_MMIEchoedTrainData_FixedDataEntry(this, Variables.paramEvc6FixedTrainsetCaptions);

            DmiActions.ShowInstruction(this, @"Press the ‘Yes’ button to confirm the data.");

            /*
            Test Step 9
            Action: Enter and confirm PLZB specific data
            Expected Result: DMI displays train running number window
            */
            // Enter and confirm PLZB specific data ??

            EVC16_CurrentTrainNumber.TrainRunningNumber = 1;
            EVC16_CurrentTrainNumber.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Train running number window.");

            /*
            Test Step 10
            Action: Complete start of mission
            Expected Result: DMI displays in PLZB STM mode
            */
            DmiActions.Display_Main_Window_with_Start_button_enabled(this);
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.NationalSystem;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in NTC mode, Level AWS TPWS.");

            /*
            Test Step 11
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
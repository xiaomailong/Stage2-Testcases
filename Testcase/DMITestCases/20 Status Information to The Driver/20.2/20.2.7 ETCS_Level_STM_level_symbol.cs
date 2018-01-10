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
using Testcase.Telegrams.DMItoEVC;
using Testcase.Telegrams.EVCtoDMI;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 20.2.7 ETCS Level: STM level symbol
    /// TC-ID: 15.2.6
    /// 
    /// This test case verifies the visualisation of mode symbol and level symbol of STM level. The mode and level symbol of STM level are displayed in sub-area B7 and C8. The visualisation of mode symbol and level synbol of STM level shall comply with [ERA] standard and [MMI-ETCS-gen].
    /// 
    /// Tested Requirements:
    /// MMI_gen 110 (partly: display ETCS Mode, MO19); MMI_gen 1227 (partly: STM); MMI_gen 1088 (partly: Bit #24);  MMI_gen 11470 (partly: Bit #10,28 and 38);
    /// 
    /// Scenario:
    /// Activate cabin A. Enter the Driver ID. Then select and confirm STM (TPWS) level Verify the mode and level of TPWS STM Symbol then continue to complete Start of Mission. 
    /// 
    /// Used files:
    /// N/A
    /// </summary>
    public class TC_ID_15_2_6_ETCS_Level : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:

            // Call the TestCaseBase PreExecution
            base.PreExecution();

            // Configure atpcu configuration file as following (See the instruction in Appendix 2)
            // M_InstalledLevels = 31 NID_NTC_Installe_0 = 20 (TPWS) System is power on.
            DmiActions.Start_ATP();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in SN mode, level STM (TPWS)

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
            Action: Activate cabin A
            Expected Result: DMI displays the default window. The Driver ID window is displayed
            */
            DmiActions.Activate_Cabin_1(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Default window.");

            DmiActions.Set_Driver_ID(this, "1234");

            DmiExpectedResults.Driver_ID_window_displayed(this);

            DmiActions.ShowInstruction(this, "Confirm the Driver ID");

            /*
            Test Step 2
            Action: Select ‘TPWS (STM)’ level
            Expected Result: Verify the following information:(1)    The confirmation announcement symbol of SN mode is displayed at sub-area C1 and the driver is required to acknowledge. (Please check MO20 symbol in ‘Comment’ column.)(2)     Use the log file to confirm that DMI sends out packet [MMI_DRIVER_ACTION (EVC-152)] with the value of variable MMI_M_DRIVER_ACTION refer to sequence below,a)   MMI_M_DRIVER_ACTION = 38 (Level NTC selected)
            Test Step Comment: (1) MMI_gen 1227 (Partly: STM);    (2) MMI_gen 11470 (partly: Bit #38);
            */
            EVC20_MMISelectLevel.MMI_Q_CLOSE_ENABLE = Variables.MMI_Q_CLOSE_ENABLE.Disabled;
            EVC20_MMISelectLevel.MMI_Q_LEVEL_NTC_ID = new Variables.MMI_Q_LEVEL_NTC_ID[]
                {Variables.MMI_Q_LEVEL_NTC_ID.ETCS_Level};
            EVC20_MMISelectLevel.MMI_M_CURRENT_LEVEL = new Variables.MMI_M_CURRENT_LEVEL[]
                {Variables.MMI_M_CURRENT_LEVEL.NotLastUsedLevel};
            EVC20_MMISelectLevel.MMI_M_LEVEL_FLAG = new Variables.MMI_M_LEVEL_FLAG[]
                {Variables.MMI_M_LEVEL_FLAG.MarkedLevel};
            EVC20_MMISelectLevel.MMI_M_INHIBITED_LEVEL = new Variables.MMI_M_INHIBITED_LEVEL[]
                {Variables.MMI_M_INHIBITED_LEVEL.NotInhibited};
            EVC20_MMISelectLevel.MMI_M_INHIBIT_ENABLE = new Variables.MMI_M_INHIBIT_ENABLE[]
                {Variables.MMI_M_INHIBIT_ENABLE.AllowedForInhibiting};
            EVC20_MMISelectLevel.MMI_M_LEVEL_NTC_ID = new Variables.MMI_M_LEVEL_NTC_ID[]
                {Variables.MMI_M_LEVEL_NTC_ID.AWS_TPWS};
            EVC20_MMISelectLevel.Send();

            DmiActions.ShowInstruction(this, "Select and confirm TPWS Level");

            EVC152_MMIDriverAction.Check_MMI_M_DRIVER_ACTION =
                EVC152_MMIDriverAction.MMI_M_DRIVER_ACTION.LevelNTCSelected;

            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.PlainTextMessage = "";
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 555;
            EVC8_MMIDriverMessage.Send();

            /*
            Test Step 3
            Action: Confirm SN mode
            Expected Result: Verify the following information:(1)    DMI displays the symbol of TPWS STM level in sub-area C8.        The symbol of SN mode is displayed in sub-area B7. (see the example in ‘Comment’ column)(2)   Use the log file to confirm that DMI sends out packet [MMI_DRIVER_ACTION (EVC-152)] with the value of variable MMI_M_DRIVER_ACTION refer to sequence below,a)   MMI_M_DRIVER_ACTION = 10 (Ack level NTC)
            Test Step Comment: (1) MMI_gen 110 (partly: ETCS Mode, MO19); MMI_gen 1227 (partly: STM);  (2) MMI_gen 11470 (partly: Bit #10);
            */
            DmiActions.ShowInstruction(this, "Confirm SN Mode");

            // This is indicated in Step 5 but would occur here...
            EVC152_MMIDriverAction.Check_MMI_M_DRIVER_ACTION = EVC152_MMIDriverAction.MMI_M_DRIVER_ACTION.SNModeAck;

            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.NationalSystem;

            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.PlainTextMessage = "";
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 277;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays symbol MO19 (SN mode) in sub-area B7." + Environment.NewLine +
                                "2. DMI displays symbol LE08 (TPWS level) in sub-area C8.");

            /*
            Test Step 4
            Action: Complete Start of Mission
            Expected Result: DMI displays in SN mode, level STM (TPWS)(1)     Use the log file to confirm that DMI receives packet EVC-30 with the value of following bit in variable MMI_Q_REQUEST_ENABLE_64,Bit #24 = 1 (End of data entry)(2)     Use the log file to confirm that DMI sends out packet [MMI_DRIVER_ACTION (EVC-152)] with the value of variable MMI_M_DRIVER_ACTION refer to sequence below,a) MMI_M_DRIVER_ACTION = 28 (Ack of SN mode)
            Test Step Comment: (1) MMI_gen 1088 (partly: Bit #24);  (2) MMI_gen 11470 (partly: Bit #28);
            */
            // Note SN ack is before this
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.Start |
                                                               EVC30_MMIRequestEnable.EnabledRequests.EndOfDataEntryNTC;
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Default;
            EVC30_MMIRequestEnable.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in SN mode, Level STM (TPWS)");

            /*
            Test Step 5
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
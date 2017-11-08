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


// TODO This test case requires config file changes and other languages. Suggest to leave until later.

namespace Testcase.DMITestCases
{
    /// <summary>
    /// 20.5.2 Building Texts: Brake test in Progress!
    /// TC-ID: 15.4.2
    /// 
    /// This test case verifies that display texts shall be selected from one of character code and align with
    /// the currently active language
    /// 
    /// Tested Requirements:
    /// MMI_gen 3722;
    /// 
    /// Scenario:
    /// 1. Power on the system 
    /// 2. Verify the text message “Brake Test in Progress” shall not display when the character code language
    ///     does not match with the language selection
    /// 
    /// Used files:
    /// N/A
    /// </summary>
    public class TC_15_4_2_Adhesion_Factor : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // The system is powered OFF.
            // The default language is English
            // Set the following tags name in configuration file
            // (see the instruction in Appendix 1 and change configuration file to Language_mgr.xml)
            // Replace English text with Russian text
            // <ENG>Brake Test in Progress</ENG> to <ENG>Выполнение опробования тормозов</ENG>

            // Call the TestCaseBase PreExecution
            base.PreExecution();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in SN mode, Level STM-ATB.

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint

            TraceInfo("This test case requires a DMI configuration change - " +
                      "See Precondition requirements. If this is not done manually, the test may fail!");

            /*
            Test Step 1
            Action: Power on the system and activate cabin
            Expected Result: DMI displays the Driver ID window
            */
            DmiActions.Start_ATP();

            // Set train running number, cab 1 active, and other defaults
            DmiActions.Activate_Cabin_1(this);

            // In SoM, ERA_ERTMS says the driver ID button is pressed to display the window 
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Main;      // Main
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.DriverID |
                                                               EVC30_MMIRequestEnable.EnabledRequests.Level |
                                                               EVC30_MMIRequestEnable.EnabledRequests.StartBrakeTest;
            EVC30_MMIRequestEnable.Send();

            DmiActions.ShowInstruction(this, "Press the ‘Driver ID’ button");

            EVC14_MMICurrentDriverID.MMI_X_DRIVER_ID = "1234";
            EVC14_MMICurrentDriverID.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Driver ID window.");

            /*
            Test Step 2
            Action: Enter and confirm Driver ID. Then perform brake test
            Expected Result: DMI shall not display text message “Brake Test in Progress!” in any other languages
                            since the text is replaced with Russian character code language
            Test Step Comment: MMI_gen 3722 (partly:ETCS)
            */
            DmiActions.ShowInstruction(this, "Enter and confirm the Driver ID. Perform brake test");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI does not display the message ‘Brake Test in Progress’ because " +
                                "   the display text is Russian and English is the current language.");
            /*
            Test Step 3
            Action: Select ATB STM and complete Start of Mission
            Expected Result: DMI displays in SN mode, Level STM-ATB
            */
            EVC20_MMISelectLevel.MMI_Q_CLOSE_ENABLE = Variables.MMI_Q_CLOSE_ENABLE.Disabled;
            EVC20_MMISelectLevel.MMI_Q_LEVEL_NTC_ID = new Variables.MMI_Q_LEVEL_NTC_ID[] { Variables.MMI_Q_LEVEL_NTC_ID.ETCS_Level };
            EVC20_MMISelectLevel.MMI_M_CURRENT_LEVEL = new Variables.MMI_M_CURRENT_LEVEL[] { Variables.MMI_M_CURRENT_LEVEL.NotLastUsedLevel };
            EVC20_MMISelectLevel.MMI_M_LEVEL_FLAG = new Variables.MMI_M_LEVEL_FLAG[] { Variables.MMI_M_LEVEL_FLAG.MarkedLevel };
            EVC20_MMISelectLevel.MMI_M_INHIBITED_LEVEL = new Variables.MMI_M_INHIBITED_LEVEL[] { Variables.MMI_M_INHIBITED_LEVEL.NotInhibited };
            EVC20_MMISelectLevel.MMI_M_INHIBIT_ENABLE = new Variables.MMI_M_INHIBIT_ENABLE[] { Variables.MMI_M_INHIBIT_ENABLE.AllowedForInhibiting };
            EVC20_MMISelectLevel.MMI_M_LEVEL_NTC_ID = new Variables.MMI_M_LEVEL_NTC_ID[] { Variables.MMI_M_LEVEL_NTC_ID.AWS_TPWS };
            EVC20_MMISelectLevel.Send();

            DmiActions.ShowInstruction(this, "Select level AWS/TPWS STM and complete Start of Mission");

            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Level = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_LEVEL.LNTC;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.NationalSystem;

            /*
            Test Step 4
            Action: Press settings menu and start to perform brake test by pressing ‘Test’ button in the Brake window
            Expected Result: DMI shall not display text message “Brake Test in Progress!” in any other languages
                            since the text is replaced with Russian character code language
            Test Step Comment: MMI_gen 3722 (partly:NTC)
            */
            DmiActions.ShowInstruction(this, "Press settings menu then press the ‘Test’ button in the Brake " +
                                             "window to start the brake test.");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI does not display the message ‘Brake Test in Progress’ because " +
                                "the display text is Russian and English is the current language.");
            
            /*
            Test Step 5
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
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
using Testcase.DMITestCases;
using Testcase.Telegrams.DMItoEVC;
using Testcase.Telegrams.EVCtoDMI;
using static Testcase.Telegrams.EVCtoDMI.Variables;
using Testcase.TemporaryFunctions;
using CL345;

namespace Testcase.DMITestCases
{
    /// <summary>
    /// 20.2.3 ETCS Level :Announcement symbol in Sub-Area C1.
    /// TC-ID: 15.2.3
    /// 
    /// This test case verifies the display information of level announcement symbols refer to recived packet information EVC-8. The level announcement symbol is unable to display if the mode acknowledgement symbol still displays on DMI.
    /// 
    /// Tested Requirements:
    /// MMI_gen 1310; MMI_gen 9429;
    /// 
    /// Scenario:
    /// Press the 'Start' button. Then, use the test script file to send packet information EVC-8 and verify that level announcement symbols are not display while mode acknowledgement symbol still display.Acknowledge the symbol on sub-area C
    /// 1.Then, use the test script files to send packet information EVC-8 and verify the display information.Note: Each step of test script file in executed continuously, Tester need to confirm expected result within specific time (3 second).
    /// 
    /// Used files:
    /// 15_2_3_a.xml, 15_2_3_b.xml
    /// </summary>
    public class TC_15_2_3_ETCS_Level : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // System is power ON.Cabin is activated.SoM is perform until train running number is entered.

            // Call the TestCaseBase PreExecution
            base.PreExecution();
            DmiActions.Start_ATP();

            // Set train running number, cab 1 active, and other defaults
            DmiActions.Activate_Cabin_1(this);

            // Set driver ID
            DmiActions.Set_Driver_ID(this, "1234");

            // Set to level 1 and SR mode
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Level = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_LEVEL.L1;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.StaffResponsible;

            DmiActions.Display_Main_Window_with_Start_button_enabled(this);
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in SR mode, Level 1.

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint

            #region Test Step 1
            /*
            Test Step 1
            Action: Press the 'Start' button
            Expected Result: The acknowledgement for Staff Responsible symbol (MO10) is displayed in sub-area C1
            */

            DmiExpectedResults.Main_Window_displayed(this, true);

            DmiActions.ShowInstruction(this, @"Perform the following actions on the DMI: " + Environment.NewLine + Environment.NewLine +
                                "1. Press ‘Start’ button." + Environment.NewLine +
                                "2. Press OK on THIS window within 3 seconds.");
            DmiExpectedResults.Start_Button_pressed_and_released(this);

            DmiActions.Send_SR_Mode_Ack(this);
            DmiExpectedResults.SR_Mode_Ack_requested(this);

            #endregion

            #region Test Step 2
            /*
            Test Step 2
            Action: Use the test script file 15_2_3_a.xml to send EVC-8 with,
            MMI_Q_TEXT = 257
            MMI_Q_TEXT_CRITERIA = 1
            MMI_N_TEXT = 1
            MMI_X_TEXT = 0
            Expected Result: Verify the following information,
            (1)    The displays in sub-area C1 is not changed, DMI still displays MO10 symbol
            Test Step Comment: (1) MMI_gen 9429;
            */

            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 257;
            EVC8_MMIDriverMessage.PlainTextMessage = "0";
            EVC8_MMIDriverMessage.Send();

            DmiExpectedResults.Driver_symbol_displayed(this, "Acknowledgement for Staff Responsible mode", "MO10", "C1", true);

            #endregion

            /*
            Test Step 3
            Action: Perform the following procedure,
            Deactivate Cabin
            Activate Cabin
            Enter Driver ID and skip brake test
            Select and confirm Level 1.
            Press ‘Close’ button
            Expected Result: DMI displays in SB mode, level 1
            */
            // Call generic Check Results Method

            DmiActions.Deactivate_Cabin(this);

            Wait_Realtime(5000);

            DmiActions.Activate_Cabin_1(this);

            DmiActions.Set_Driver_ID(this, "1234");
            DmiActions.Send_SB_Mode(this);
            DmiExpectedResults.Driver_ID_window_displayed_in_SB_mode(this);

            DmiExpectedResults.Driver_ID_entered(this);

            DmiActions.Request_Brake_Test(this);
            DmiExpectedResults.Brake_Test_Perform_Order(this, false);

            DmiActions.Display_Level_Window(this,true);
            DmiExpectedResults.Level_window_displayed(this);

            DmiExpectedResults.Level_1_Selected(this);
            DmiExpectedResults.Close_Button_Level_Window_pressed_and_released(this);


            /*
            Test Step 4
            Action: Use the test script file 15_2_3_b.xml to send EVC-8 with,MMI_Q_TEXT = 257MMI_Q_TEXT_CRITERIA = 1MMI_N_TEXT = 1MMI_X_TEXT = 0
            Expected Result: Verify the following information,(1)  DMI displays the LE07 symbol with yellow flashing frame in sub-area C1
            Test Step Comment: (1) MMI_gen 1310 (partly:LE07);     
            */


            /*
            Test Step 5
            Action: (Continue from step 4) Send EVC-8 with,MMI_Q_TEXT = 257MMI_Q_TEXT_CRITERIA = 1MMI_N_TEXT = 1MMI_X_TEXT = 1
            Expected Result: Verify the following information,(1)  DMI displays the LE11 symbol with yellow flashing frame in sub-area C1
            Test Step Comment: (1) MMI_gen 1310 (partly:LE11);     
            */


            /*
            Test Step 6
            Action: (Continue from step 5) Send EVC-8 with,MMI_Q_TEXT = 257MMI_Q_TEXT_CRITERIA = 1MMI_N_TEXT = 1MMI_X_TEXT = 2
            Expected Result: Verify the following information,(1)  DMI displays the LE13 symbol with yellow flashing frame in sub-area C1
            Test Step Comment: (1) MMI_gen 1310 (partly:LE13);     
            */


            /*
            Test Step 7
            Action: (Continue from step 6)Send EVC-8 with,MMI_Q_TEXT = 257MMI_Q_TEXT_CRITERIA = 1MMI_N_TEXT = 1MMI_X_TEXT = 3
            Expected Result: Verify the following information,(1)  DMI displays the LE15 symbol with yellow flashing frame in sub-area C1
            Test Step Comment: (1) MMI_gen 1310 (partly:LE15);     
            */


            /*
            Test Step 8
            Action: (Continue from step 7) Send EVC-8 with,MMI_Q_TEXT = 258MMI_Q_TEXT_CRITERIA = 1MMI_N_TEXT = 1MMI_X_TEXT = 9
            Expected Result: Verify the following information,(1)  DMI displays the LE09a symbol with yellow flashing frame in sub-area C1
            Test Step Comment: (1) MMI_gen 1310 (partly:LE09a);     
            */


            /*
            Test Step 9
            Action: (Continue from step 8) Send EVC-8 with,MMI_Q_TEXT = 258MMI_Q_TEXT_CRITERIA = 1MMI_N_TEXT = 1MMI_X_TEXT = 4
            Expected Result: Verify the following information,(1)  DMI displays the LE09 symbol with yellow flashing frame in sub-area C1
            Test Step Comment: (1) MMI_gen 1310 (partly:LE09);     
            */


            /*
            Test Step 10
            Action: (Continue from step 9) Send EVC-8 with,MMI_Q_TEXT = 276MMI_Q_TEXT_CRITERIA = 3MMI_N_TEXT = 1MMI_X_TEXT = 0
            Expected Result: Verify the following information,(1)  DMI displays the LE06 symbol without yellow flashing frame in sub-area C1
            Test Step Comment: (1) MMI_gen 1310 (partly:LE06);     
            */


            /*
            Test Step 11
            Action: (Continue from step 10)Send EVC-8 with,MMI_Q_TEXT = 276MMI_Q_TEXT_CRITERIA = 3MMI_N_TEXT = 1MMI_X_TEXT = 1
            Expected Result: Verify the following information,(1)  DMI displays the LE10 symbol without yellow flashing frame in sub-area C1
            Test Step Comment: (1) MMI_gen 1310 (partly:LE10);     
            */


            /*
            Test Step 12
            Action: (Continue from step 11)Send EVC-8 with,MMI_Q_TEXT = 276MMI_Q_TEXT_CRITERIA = 3MMI_N_TEXT = 1MMI_X_TEXT = 2
            Expected Result: Verify the following information,(1)  DMI displays the LE12 symbol without yellow flashing frame in sub-area C1
            Test Step Comment: (1) MMI_gen 1310 (partly:LE12);     
            */


            /*
            Test Step 13
            Action: (Continue from step 12)Send EVC-8 with,MMI_Q_TEXT = 276MMI_Q_TEXT_CRITERIA = 3MMI_N_TEXT = 1MMI_X_TEXT = 3
            Expected Result: Verify the following information,(1)  DMI displays the LE14 symbol without yellow flashing frame in sub-area C1
            Test Step Comment: (1) MMI_gen 1310 (partly:LE14);     
            */


            /*
            Test Step 14
            Action: (Continue from step 13)Send EVC-8 with,MMI_Q_TEXT = 277MMI_Q_TEXT_CRITERIA = 3MMI_N_TEXT = 1MMI_X_TEXT = 9
            Expected Result: Verify the following information,(1)  DMI displays the LE08a symbol without yellow flashing frame in sub-area C1
            Test Step Comment: (1) MMI_gen 1310 (partly:LE08a);     
            */


            /*
            Test Step 15
            Action: (Continue from step 14)Send EVC-8 with,MMI_Q_TEXT = 277MMI_Q_TEXT_CRITERIA = 3MMI_N_TEXT = 1MMI_X_TEXT = 4
            Expected Result: Verify the following information,(1)  DMI displays the LE08 symbol without yellow flashing frame in sub-area C1
            Test Step Comment: (1) MMI_gen 1310 (partly:LE08);     
            */


            /*
            Test Step 16
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }       
    }
}
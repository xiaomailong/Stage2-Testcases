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
using Testcase.DMITestCases;
using Testcase.Telegrams.DMItoEVC;
using Testcase.Telegrams.EVCtoDMI;
using static Testcase.Telegrams.EVCtoDMI.Variables;
using Testcase.TemporaryFunctions;

namespace Testcase.DMITestCases
{
    /// <summary>
    /// 20.1.1 Mode Symbols in Sub-Area B7 for SB, SR, FS, TR, PT, SH, NL and SF mode
    /// TC-ID: 15.1.1
    /// 
    /// This test case verifies  a display of ETCS Mode symbol (SB, SR, FS, TR, PT, SH, NL and SF) in area B7 and ETCS-Object ETCS Mode symbol (acknowledgement for SR, TR and SH) in area C1. The symbol of each ETCS Mode shall comply with [ERA] standard.
    /// 
    /// Tested Requirements:
    /// MMI_gen 11084 (partly: current ETCS mode); MMI_gen 110 (partly: MO13, MO09, MO11, MO04, MO06, MO01,MO12, MO18); MMI_gen 1227 (partly:MO10, MO05); MMI_gen 9474; MMI_gen 11233; MMI_gen 3375; MMI_gen 12161;
    /// 
    /// Scenario:
    /// Activate cabin. Then, verify a display of SB symbol.Perform the Start of Mission in level1 until the ‘Start’ button is pressed. Then, verify a display of acknowledgement for SR symbol and SR symbol after acknowledgement.Drive the train pass BG
    /// 1.Then, verify a display of FS symbol.Drive the train pass the EOA. Then, verify a display of TR symbol and acknowledgement for TR symbol after train standstill.Acknowledge the acknowledgement for TR symbol. Then, verify a display of PT symbol.Force the train to Staff responsibility mode.Force the train to Shunting mode. Then, verify a display of acknowledgement for SH symbol and SH symbol after acknowledgement.Force the train to Non-leading. Then, verify a display of NL symbol.De-activate Cabin A.Force the train to System failure. Then, verify a display of SF symbol.
    /// 
    /// Used files:
    /// 15_1_1.tdg
    /// </summary>
    public class TC_15_1_1_ETCS_Mode_Symbols : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // - Test system is powered on- Cabin is active

            // Call the TestCaseBase PreExecution
            base.PreExecution();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in SF mode.

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint

            #region Test Step 1
            /*           
            Action: Verify mode symbol in sub-area B7
            Expected Result: Verify the following information,Use the log file to verify that DMI received the EVC-7 with [MMI_ETCS_MISC_OUT_SIGNALS.OBU_TR_M_MODE] = 6 
            in order to display the Stand By symbol.The Stand By symbol (MO13) is displayed in area B7
            Test Step Comment: (1) MMI_gen 11084 (par tly: current ETCS mode);
                               (2) MMI_gen 110 (partly: MO13);
            */

            EVC0_MMIStartATP.Evc0Type = EVC0_MMIStartATP.EVC0Type.GoToIdle;
            EVC0_MMIStartATP.Send();

            DmiActions.Activate_Cabin_1(this);

            DmiActions.Send_SB_Mode(this);
            DmiExpectedResults.SB_Mode_displayed(this);

            #endregion

            #region Test Step 2
            /*
            Action: Perform SoM in Level1 until the ‘Start’ button is pressed
            Expected Result: Verify the following information,The acknowledgement for Staff Responsible symbol (MO10) is displayed in area C1.
            Use the log file to confirm that DMI received the EVC-8 with [MMI_DRIVER_MESSAGE (EVC-8).MMI_Q_TEXT] = 263 
            in order to display the acknowledgement for Staff Responsible symbol
            Test Step Comment: (1) MMI_gen 1227 (partly:MO10);                                
                               (2) MMI_gen 11233;
            */

            DmiActions.Send_SR_Mode_Ack(this);

            DmiExpectedResults.SR_Mode_Ack_requested(this);

            #endregion

            #region Test Step 3
            /*
            Action: Press the symbol MO10 in sub-area C1
            Expected Result: Verify the following information,The symbol MO10 is disappear from sub-area C1 and re-appear again.
            Use the log file to confirm that DMI sends EVC-111 twice with different value of MMI_T_BUTTONEVENT and MMI_Q_BUTTON (1 = pressed, 0 = released).
            Note: DMI still display in SB mode, Level 1
            Test Step Comment: (1) MMI_gen 9474;
                               (2) MMI_gen 9474; MMI_gen 3375;
            */

            DmiActions.ShowInstruction(this, "Press DMI sub area C1.");
            DmiExpectedResults.SR_Mode_Ack_pressed_and_released(this);        
            
            DmiExpectedResults.SR_Mode_Ack_requested(this);
            DmiExpectedResults.SB_Mode_displayed(this);

            #endregion

            #region Test Step 4
            /*
            Action: Press the symbol MO10 in sub-area C1 for 2 second or upper.Then, release the pressed area
            Expected Result: Verify the following information,While the MO10 is pressed, the opacity of the symbol is decreased to 50%
            The symbol ‘MO10’ is displayed as a Safe Delay-Type button
            DMI received the EVC-7 with [MMI_ETCS_MISC_OUT_SIGNALS.OBU_TR_M_MODE] = 2 in order to display the Staff Responsible symbol.
            The Staff Responsible symbol (MO9) is displayed in area B7
            Test Step Comment: (1) MMI_gen 12161;
                               (2) MMI_gen 9474;
                               (3) MMI_gen 11084 (partly: current ETCS mode);
                               (4) MMI_gen 110     (partly: MO09);
            */

            DmiActions.ShowInstruction(this, "Press and hold DMI sub area C1.");
            DmiExpectedResults.SR_Mode_Ack_pressed_and_hold(this);

            DmiActions.Send_SR_Mode(this);           
            DmiExpectedResults.SR_Mode_displayed(this);

            DmiActions.FinishedSoM_Default_Window(this);

            #endregion

            #region Test Step 5
            /*
            Action: Force the train into FS mode by moving the train forward passing BG1
            Expected Result: Verify the following information, 
            Use the log file to confirm that DMI received the EVC-7 with [MMI_ETCS_MISC_OUT_SIGNALS.OBU_TR_M_MODE] = 0 in order to display the Full Supervision symbol.
            The Full Supervision symbol (MO11) is displayed in area B7
            Test Step Comment: (1) MMI_gen 11084 (partly: current ETCS mode);                                                 
                               (2) MMI_gen 110 (partly: MO11);
            */

            DmiActions.Drive_train_forward_passing_BG1(this);

            DmiActions.Send_FS_Mode(this);
            DmiExpectedResults.FS_mode_displayed(this);

            #endregion

            #region Test Step 6
            /*
            Action: Force the train into TR mode by moving the train forward to position of EOA
            Expected Result: Verify the following information,
            Use the log file to confirm that DMI received the EVC-7 with [MMI_ETCS_MISC_OUT_SIGNALS.OBU_TR_M_MODE] = 7 in order to display the Trip symbol.
            The Trip symbol (MO04) is displayed in area B7
            Test Step Comment: (1) MMI_gen 11084 (partly: current ETCS mode);                            
                               (2) MMI_gen 110 (partly: MO04);
            */
            
            DmiActions.Force_train_forward_overpassing_EOA(this);          
            DmiActions.Apply_Brakes(this);
            DmiActions.Send_EB_Intervention(this);

            DmiActions.Send_TR_Mode(this);
            DmiExpectedResults.TR_Mode_displayed(this);

            #endregion

            #region Test Step 7
            /*
            Action: Perform the following procedure,
            Wait until the train is stopped. Stop the train (set speed to 0 and set direction to neutral)
            Press at sub-area C9
            Expected Result: Verify the following information,
            Use the log file to confirm that DMI received the EVC-8 with [MMI_DRIVER_MESSAGE (EVC-8).MMI_Q_TEXT] = 266 in order to display the acknowledgement for Trip symbol.
            The acknowledgement for Trip symbol (MO05) is displayed in area C1
            Test Step Comment: (1) MMI_gen 11233;                             
                               (2) MMI_gen 1227 (partly: MO05);
            */

            DmiActions.Stop_the_train(this);
            DmiActions.Send_TR_Mode_Ack(this);
            DmiExpectedResults.TR_Mode_Ack_requested(this);
            DmiExpectedResults.TR_Mode_displayed(this);

            DmiActions.ShowInstruction(this, "Press DMI sub area C9 to acknowledge the Emergency Brake.");
            DmiExpectedResults.Brake_Intervention_symbol_pressed_and_released(this);

            #endregion

            #region Test Step 8
            /*
            Action: Press the symbol ‘MO05’ in sub-area C1
            Expected Result: Verify the following information,
            Use the log file to confirm that DMI received the EVC-7 with [MMI_ETCS_MISC_OUT_SIGNALS.OBU_TR_M_MODE] = 8 in order to display the Post Trip symbol.
            The Post trip symbol (MO06) is displayed in area B7
            Test Step Comment: (1) MMI_gen 11084 (partly: current ETCS mode);
                               (2) MMI_gen 110 (partly: MO06);
            */

            DmiActions.ShowInstruction(this, "Press DMI sub area C1.");
            DmiExpectedResults.TR_Mode_Ack_pressed_and_released(this);

            DmiActions.Send_PT_Mode(this);
            DmiExpectedResults.PT_Mode_displayed(this);

            #endregion

            #region Test Step 9
            /*
            Action: Force the train into SR mode by the steps below:
            Press ‘Main’ button.      
            Press ‘Start’ button ie. EVC-152 [MMI_DRIVER_ACTION.MMI_M_DRIVER_ACTION] = 19 ("Start selected") is received.
            Acknowledge SR mode
            */

            DmiActions.ShowInstruction(this, "Press \"Main\" button.");
            DmiActions.Display_Main_Window_with_Start_button_enabled(this);
            DmiExpectedResults.Main_Window_displayed_with_Start_button_enabled(this);

            DmiActions.ShowInstruction(this, "Press \"Start\" button.");
            DmiExpectedResults.Start_Button_pressed_and_released(this);

            DmiActions.Send_SR_Mode_Ack(this);
            DmiExpectedResults.SR_Mode_Ack_requested(this);

            DmiActions.ShowInstruction(this, "Acknowledge SR Mode.");
            DmiExpectedResults.SR_Mode_Ack_pressed_and_hold(this);

            DmiActions.Send_SR_Mode(this);
            DmiExpectedResults.SR_Mode_displayed(this);

            #endregion

            #region Test Step 10
            /*
            Test Step 10
            Action: Force the train into SH mode by the steps below:
            Press ‘Main’ button.
            Press and hold ‘Shunting’ button for 2 second or upper.
            Release the pressed area
            Expected Result: Verify the following information,
            Use the log file to confirm that DMI received the EVC-7 with [MMI_ETCS_MISC_OUT_SIGNALS.OBU_TR_M_MODE] = 3 in order to display the Shunting symbol.
            The Shunting symbol (MO01) is displayed in area B7
            Test Step Comment: (1) MMI_gen 11084 (partly: current ETCS mode);                                    
                               (2) MMI_gen 110 (partly: MO01);
            */

            DmiActions.ShowInstruction(this, "Press \"Main\" button");
            DmiActions.Display_Main_Window_with_Start_button_enabled(this);
            DmiExpectedResults.Main_Window_displayed_with_Start_button_enabled(this);

            DmiActions.ShowInstruction(this, "Press and hold \"Shunting\" button for at least 2 seconds.");
            DmiExpectedResults.Shunting_button_pressed_and_hold(this);

            DmiActions.Send_SH_Mode(this);
            DmiExpectedResults.SH_Mode_displayed(this);

            #endregion

            #region Test Step 11
            /*
            Test Step 11
            Action: Force the train into NL mode by the steps below:
            Press ‘Main’ button. 
            Press and hold ‘Exit Shunting’ button for 2 second or upper.
            Release the pressed area.
            Enter the Driver ID with no performing brake test when Driver ID window is displayed.
            If the Level window is display, select and confirm Level1. 
            Then, enter the train data and Train running number.
            Force the simulation to ‘Non-leading’. 
            Press and hold ‘Non-leading’ button for 2 second or upper.
            Release the pressed area
            Expected Result: Verify the following information,
            Use the log file to confirm that DMI received the EVC-7 with [MMI_ETCS_MISC_OUT_SIGNALS.OBU_TR_M_MODE] = 11 in order to display the Non-leading.
            The Non-leading symbol (MO12) is displayed in area B7
            Test Step Comment: (1) MMI_gen 11084 (partly: current ETCS mode);                                   
                               (2) MMI_gen 110 (partly: MO12);
            */

            DmiActions.ShowInstruction(this, "Press \"Main\" button");
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = Variables.standardFlags |
                EVC30_MMIRequestEnable.EnabledRequests.Start | EVC30_MMIRequestEnable.EnabledRequests.ExitShunting;
            EVC30_MMIRequestEnable.Send();
            WaitForVerification("Is the Main windows displayed, with \"Exit Shunting\" button available?");

            DmiActions.ShowInstruction(this, "Press and hold \"Exit Shunting\" button for at least 2 seconds.");
            DmiExpectedResults.Shunting_button_pressed_and_hold(this);

            DmiActions.Display_Driver_ID_Window(this);
            DmiExpectedResults.Driver_ID_window_displayed(this);
            DmiActions.ShowInstruction(this, "Enter Driver ID");
            //DmiActions.Set_Driver_ID(this, "1234");

            DmiActions.ShowInstruction(this, "Press and hold \"Non-leading\" button for at least 2 seconds.");
            DmiExpectedResults.Non_leading_button_pressed_and_hold(this);

            DmiActions.Send_NL_Mode(this);
            DmiExpectedResults.NL_Mode_displayed(this);

            #endregion

            #region Test Step 12
            /*
            Test Step 12
            Action: Force the train into SF mode by the steps below:
            Unforce the simulation of ‘Non-leading’.
            De-activate cabin A.
            Activate cabin B.
            Activate cabin A
            Expected Result: Verify the following information,
            Use the log file to confirm that DMI received the EVC-7 with [MMI_ETCS_MISC_OUT_SIGNALS.OBU_TR_M_MODE] = 9 in order to display the System failure symbol.
            The System failure symbol (MO18) is displayed in area B7
            Test Step Comment: (1) MMI_gen 11084 (partly: current ETCS mode);                                
                               (2) MMI_gen 110 (partly: MO18);
            */

            DmiActions.ShowInstruction(this, "Unforce the simulation of \"Non-leading\".");

            DmiActions.Deactivate_Cabin(this);
            DmiExpectedResults.Cab_deactivated(this);

            DmiActions.Activate_Cabin_2(this);
            DmiExpectedResults.Cabin_B_is_activated(this);

            DmiActions.Activate_Cabin_1(this);
            DmiActions.Send_SF_Mode(this);
            DmiExpectedResults.SF_Mode_displayed(this);

            #endregion

            #region Test Step 13
            /*
            Action: End of test
            Expected Result: 
            */

            #endregion

            return GlobalTestResult;
        }
    }
}
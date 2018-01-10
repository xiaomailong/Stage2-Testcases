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
    /// 20.1.3 Mode Symbols in Sub-Area B7 for OS, UN mode
    /// TC-ID: 15.1.3
    /// 
    /// This test case verifies the presentation of ETCS Mode displays as a symbol in sub-area C1 and B7 (for UN and OS mode) and a symbol in sub-area C7 (for EOA) . The symbol of each ETCS Mode shall comply with [ERA] standard.
    /// 
    /// Tested Requirements:
    /// MMI_gen 1227 (partly: MO08, MO17); MMI_gen 110 (partly: MO07, MO16); MMI_gen 11084 (partly: current ETCS mode); MMI_gen 11231; MMI_gen 11233 (partly: MO08 and MO 17); MMI_gen 11470 (partly: Bit # 0,3,4 and 34);
    /// 
    /// Scenario:
    /// Activate Cabin APerform SoM in UN mode Level 0 and verify the display information during mode acknowledgement.De-activate Cabin A. Then, activate Cabin A again and perform SoM in SR mode Level 1.Driver the train forward.Verify the display information when drive the train forward pass BG1 at 50m.BG1: packet 12, packet 21, packet 27 and packet 80.Acknowldege OS mode and verify the displays information.Stop the train.Open Override window.Press EOA button. Then, verify the displays information.
    /// 
    /// Used files:
    /// 15_1_3.tdg
    /// </summary>
    public class TC_15_1_3_ETCS_Mode_Symbols : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // System is power on.

            // Call the TestCaseBase PreExecution
            base.PreExecution();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in SR mode, Level 1

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint

            #region Test Step 1

            /*           
            Action: Activate cabin A
            Expected Result: DMI displays in SB mode. The Driver ID window is displayed
            */

            EVC0_MMIStartATP.Evc0Type = EVC0_MMIStartATP.EVC0Type.GoToIdle;
            EVC0_MMIStartATP.Send();

            DmiActions.Activate_Cabin_1(this);
            DmiExpectedResults.Cabin_A_is_activated(this);

            DmiActions.Set_Driver_ID(this, "1234");
            DmiActions.Send_SB_Mode(this);
            DmiExpectedResults.Driver_ID_window_displayed_in_SB_mode(this);

            #endregion

            #region Test Step 2

            /*           
            Action: Enter Driver ID and perform brake test
            Expected Result: DMI displays Level window
            */

            DmiExpectedResults.Driver_ID_entered(this);

            DmiActions.Request_Brake_Test(this, 1);
            DmiExpectedResults.Brake_Test_Perform_Order(this, true);

            DmiActions.Perform_Brake_Test(this, 2);

            Wait_Realtime(5000);

            DmiActions.Display_Brake_Test_Successful(this, 3);

            DmiActions.Display_Level_Window(this);
            DmiExpectedResults.Level_window_displayed(this);

            DmiActions.Delete_Brake_Test_Successful(this, 3);

            #endregion

            #region Test Step 3

            /*
            Action: Select and confirm Level 0
            Expected Result: DMI displays Main window.
            (1)   Use the log file to confirm that DMI sends out packet [MMI_DRIVER_ACTION (EVC-152)] with the value of variable MMI_M_DRIVER_ACTION refer to sequence below,
            a)   MMI_M_DRIVER_ACTION = 34 (Level 0 selected)
            Test Step Comment: (1) MMI_gen 11470 (partly: Bit # 34);
            */

            DmiExpectedResults.Level_0_Selected(this);

            DmiActions.Display_Main_Window_with_Start_button_not_enabled(this);

            #endregion

            #region Test Step 4

            /*
            Test Step 4
            Action: Press ‘Train data’ button
            Expected Result: DMI displays Train data window
            */

            DmiExpectedResults.Train_Data_Button_pressed_and_released(this);

            DmiActions.Display_Fixed_Train_Data_Window(this);
            DmiExpectedResults.Train_data_window_displayed(this);

            #endregion

            #region Test Step 5

            /*
            Test Step 5
            Action: Enter and confirm value in each input field.Then, press ‘Yes’ button
            Expected Result: DMI displays Train data validation window
            */

            DmiExpectedResults.Fixed_Train_Data_entered(this, Fixed_Trainset_Captions.FLU);

            DmiActions.Enable_Fixed_Train_Data_Validation(this, Fixed_Trainset_Captions.FLU);
            DmiExpectedResults.Fixed_Train_Data_validated(this, Fixed_Trainset_Captions.FLU);

            DmiActions.Complete_Fixed_Train_Data_Entry(this, Fixed_Trainset_Captions.FLU);

            Wait_Realtime(1000);

            DmiActions.Display_Train_data_validation_Window(this);
            DmiExpectedResults.Train_data_validation_window_displayed(this);

            #endregion

            #region Test Step 6

            /*
            Test Step 6
            Action: Press ‘Yes’ button.Then, confirmed selected value by pressing an input field
            Expected Result: DMI displays Train Running Number window
            */

            DmiExpectedResults.Train_Data_validation_completed(this);

            Wait_Realtime(5000);

            DmiActions.Display_TRN_Window(this);
            DmiExpectedResults.TRN_window_displayed(this);

            #endregion

            #region Test Step 7

            /*
            Test Step 7
            Action: Enter and confirm Train running number
            Expected Result: DMI displays Main window
            */

            DmiExpectedResults.TRN_entered(this);

            DmiActions.Display_Main_Window_with_Start_button_enabled(this);
            DmiExpectedResults.Main_Window_displayed(this, true);

            #endregion

            #region Test Step 8

            /*
            Test Step 8
            Action: Press ‘Start’ button
            Expected Result: Verify the following information,
            The symbol MO17 is displayed for Unfitted mode acknowledegement in sub-area C1. 
            Use the log file to confirm that DMI receives packet information EVC-8 with the following value,
            MMI_Q_TEXT = 264
            MMI_Q_TEXT_CRITERIA = 1
            Test Step Comment: (1) MMI_gen 1227 (partly: MO17);                                          
                               (2) MMI_gen 11233 (partly: MO17);
            */

            DmiExpectedResults.Start_Button_pressed_and_released(this);

            DmiActions.Display_Default_Window(this);

            DmiActions.Send_UN_Mode_Ack(this);
            DmiExpectedResults.UN_Mode_Ack_requested(this);

            #endregion

            #region Test Step 9

            /*
            Test Step 9
            Action: Acknowledge UN mode
            Expected Result: Verify the following information,
            (1)    The symbol MO16 is displayed in sub-area B7. 
            (2)    Use the log file to confirm that DMI received the EVC-7 
            with [MMI_ETCS_MISC_OUT_SIGNALS.OBU_TR_M_MODE] = 4 in order to display the Unfitted symbol.
            (3)    Use the log file to confirm that DMI sends out packet [MMI_DRIVER_ACTION (EVC-152)] 
            with the value of variable MMI_M_DRIVER_ACTION refer to sequence below,          
            a)   MMI_M_DRIVER_ACTION = 4 (Ack of Unfitted mode)
            Test Step Comment: (1) MMI_gen 110 (partly: MO16);  
                               (2) MMI_gen 11084 (partly: ETCS mode UN); 
                               (3) MMI_gen 11470 (partly: Bit # 4);                                               
            */

            DmiActions.ShowInstruction(this, "Press DMI Sub Area C1");
            DmiExpectedResults.UN_Mode_Ack_pressed_and_released(this);

            DmiActions.Send_UN_Mode(this);
            DmiActions.Send_L0(this);

            DmiExpectedResults.UN_Mode_displayed(this);
            DmiExpectedResults.Driver_symbol_displayed(this, "Level 0", "LE01", "C8", true);

            #endregion

            #region Test Step 10

            /*
            Test Step 10
            Action: Perform the following procedure:
            De-activate Cabin A.
            Activate Cabin A.
            Perform SoM in SR mode, Level 1
            Expected Result: DMI displays Default window in SR mode, Level 1.
            (1)   Use the log file to confirm that DMI sends out packet [MMI_DRIVER_ACTION (EVC-152)] 
            with the value of variable MMI_M_DRIVER_ACTION refer to sequence below,      
            a)    MMI_M_DRIVER_ACTION = 3 (Ack of Staff Responsible mode)
            Test Step Comment: (1) MMI_gen 11470 (partly: Bit #3);                     
            */

            DmiActions.Deactivate_Cabin(this);

            Wait_Realtime(5000);

            DmiActions.Activate_Cabin_1(this);

            DmiActions.Display_Driver_ID_Window(this);
            DmiActions.Set_Driver_ID(this, "1234");
            DmiActions.Send_SB_Mode(this);
            DmiActions.ShowInstruction(this, @"Perform the following actions on the DMI: " + Environment.NewLine +
                                             Environment.NewLine +
                                             "1. Enter and confirm Driver ID." + Environment.NewLine +
                                             "2. Press OK on THIS window.");

            DmiActions.Request_Brake_Test(this);
            DmiActions.ShowInstruction(this, @"Perform the following actions on the DMI: " + Environment.NewLine +
                                             Environment.NewLine +
                                             "1. Perform Brake Test" + Environment.NewLine +
                                             "2. Press OK on THIS window.");
            DmiActions.Perform_Brake_Test(this, 2);
            Wait_Realtime(5000);
            DmiActions.Display_Brake_Test_Successful(this, 3);

            DmiActions.Display_Level_Window(this);
            DmiActions.Delete_Brake_Test_Successful(this, 3);
            DmiActions.ShowInstruction(this, @"Perform the following actions on the DMI: " + Environment.NewLine +
                                             Environment.NewLine +
                                             "1. Select and enter Level 1" + Environment.NewLine +
                                             "2. Press OK on THIS window.");

            DmiActions.Display_Main_Window_with_Start_button_not_enabled(this);
            DmiActions.ShowInstruction(this, @"Perform the following actions on the DMI: " + Environment.NewLine +
                                             Environment.NewLine +
                                             "1. Press ‘Train data’ button." + Environment.NewLine +
                                             "2. Press OK on THIS window.");

            DmiActions.Display_Fixed_Train_Data_Window(this);
            DmiActions.ShowInstruction(this, @"Perform the following actions on the DMI: " + Environment.NewLine +
                                             Environment.NewLine +
                                             "1. Enter FLU and confirm value in each input field." +
                                             Environment.NewLine +
                                             "2. Press OK on THIS window.");

            DmiActions.Enable_Fixed_Train_Data_Validation(this, Fixed_Trainset_Captions.FLU);
            DmiActions.ShowInstruction(this, @"Perform the following actions on the DMI: " + Environment.NewLine +
                                             Environment.NewLine +
                                             "1. Press ‘Yes’ button." + Environment.NewLine +
                                             "2. Press OK on THIS window.");

            DmiActions.Complete_Fixed_Train_Data_Entry(this, Fixed_Trainset_Captions.FLU);
            DmiActions.Display_Train_data_validation_Window(this);
            DmiActions.ShowInstruction(this, @"Perform the following actions on the DMI: " + Environment.NewLine +
                                             Environment.NewLine +
                                             "1. Press ‘Yes’ button." + Environment.NewLine +
                                             "2. Confirmed the selected value by pressing the input field." +
                                             Environment.NewLine +
                                             "3. Press OK on THIS window.");

            DmiActions.Display_TRN_Window(this);
            DmiActions.ShowInstruction(this, @"Perform the following actions on the DMI: " + Environment.NewLine +
                                             Environment.NewLine +
                                             "1. Enter and confirm Train Running Number." + Environment.NewLine +
                                             "2. Press OK on THIS window.");

            DmiActions.Display_Main_Window_with_Start_button_enabled(this);
            DmiActions.ShowInstruction(this, @"Perform the following actions on the DMI: " + Environment.NewLine +
                                             Environment.NewLine +
                                             "1. Press ‘Start’ button." + Environment.NewLine +
                                             "2. Press OK on THIS window.");

            DmiActions.Send_SR_Mode_Ack(this);
            DmiActions.ShowInstruction(this, @"Perform the following action after pressing OK: " + Environment.NewLine +
                                             Environment.NewLine +
                                             "1. Press DMI Sub Area C1.");
            DmiExpectedResults.SR_Mode_Ack_pressed_and_hold(this);

            DmiActions.Send_SR_Mode(this);
            DmiActions.Send_L1(this);
            DmiActions.Finished_SoM_Default_Window(this);
            DmiExpectedResults.SR_Mode_displayed(this);
            DmiExpectedResults.Driver_symbol_displayed(this, "Level 1", "LE03", "C8", true);

            #endregion

            #region Test Step 11

            /*
            Test Step 11
            Action: Drive the train forward passing BG1
            Expected Result: Verify the following information,
            The symbol MO08 is displayed for On sight acknowledegement in sub-area C1. 
            Use the log file to confirm that DMI is receive packet information EVC-8 with the following value,
            MMI_Q_TEXT = 259
            MMI_Q_TEXT_CRITERIA = 1
            (3)    Use the log file to confirm that DMI sends out packet [MMI_DRIVER_ACTION (EVC-152)] 
            with the value of variable MMI_M_DRIVER_ACTION refer to sequence below,
            a)   MMI_M_DRIVER_ACTION = 0 (Ack of On sight mode)
            Test Step Comment: (1) MMI_gen 1227 (partly: MO08);        
                               (2) MMI_gen 11233 (partly: MO17ไ);
                               (3) MMI_gen 11470 (partly: Bit # 0);                     
            */

            DmiActions.Drive_train_forward_passing_BG1(this);

            DmiActions.Send_OS_Mode_Ack(this);
            DmiExpectedResults.OS_Mode_Ack_Requested(this);

            #endregion

            #region Test Step 12

            /*
            Test Step 12
            Action: Acknowledge OS mode
            Expected Result: Verify the following information,
            (1)    The symbol MO07 is displayed in sub-area B7. 
            (2)    Use the log file to confirm that DMI received the EVC-7 
            with [MMI_ETCS_MISC_OUT_SIGNALS.OBU_TR_M_MODE] = 1 in order to display the On-sight symbol
            Test Step Comment: (1) MMI_gen 110 (partly:MO07);  
                               (2) MMI_gen 11084 (partly: ETCS mode OS);                           
            */

            DmiActions.ShowInstruction(this, "Press DMI Sub Area C1");
            DmiExpectedResults.OS_Mode_Ack_pressed_and_released(this);

            DmiActions.Send_OS_Mode(this);
            DmiExpectedResults.OS_Mode_displayed(this);

            #endregion

            #region Test Step 13

            /*
            Test Step 13
            Action: Stop the train.Then, press ‘Over-ride’ button
            Expected Result: When the train is stopped, EOA button is enabled
            */

            DmiActions.Stop_the_train(this);

            Wait_Realtime(5000);

            DmiActions.ShowInstruction(this, @"Perform the following actions on the DMI: " + Environment.NewLine +
                                             Environment.NewLine +
                                             "1. Press the \"Override\" Button on Default Window Area F2." +
                                             Environment.NewLine +
                                             "2. Press OK on THIS window.");
            DmiActions.Display_Override_Window(this);
            DmiExpectedResults.Override_window_displayed(this);

            #endregion

            #region Test Step 14

            /*
            Test Step 14
            Action: Press ‘EOA’ button
            Expected Result: Verify the following information, 
            (1)   The symbol MO03 is displayed for Override EOA symbol in sub-area C7.
            (2) Use the log DMI received packet information EVC-2 with variable MMI_M_OVERRIDE_EOA = 1
            Test Step Comment: (1) MMI_gen 11231 (partly: MO03);
                               (2) MMI_gen 11231 (partly: EVC-2);
            */

            DmiActions.ShowInstruction(this, @"Perform the following action after pressing OK: " + Environment.NewLine +
                                             Environment.NewLine +
                                             "1. Press ‘EOA’ button.");

            DmiExpectedResults.EOA_Button_pressed(this);

            DmiActions.Set_Override(this);
            DmiExpectedResults.Default_Window_with_Override_Symbol(this);

            #endregion

            #region Test Step 15

            /*
            Test Step 15
            Action: End of test
            Expected Result: 
            */

            #endregion

            return GlobalTestResult;
        }
    }
}
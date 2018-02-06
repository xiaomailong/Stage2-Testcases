#region usings

using System;
using Testcase.Telegrams.EVCtoDMI;

#endregion

namespace Testcase.DMITestCases
{
    /// <summary>
    /// TCMS_DMI_SoM_L1
    /// 
    /// This test case verifies the presentation and the functionality of the DMI during a Start of Mission in Level 1 
    /// NOTE: This test case is relevant only if both real ETCS and TCMS DMI are part of the simulation environment   
    /// 
    /// Scenario:
    /// Activate Cabin A
    /// Enter Driver ID
    /// Perform Brake Test
    /// Select Level 1
    /// Proceed to Train Data entry and validation
    /// Enter TRN
    /// Press 'Start' button and acknowledge SR mode
    /// De-activate Cabin A.
    /// 
    /// </summary>
    public class TCMS_DMI_SoM_L1 : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-test configuration.
            base.PreExecution();
            DmiActions.ShowInstruction(this, @"Perform the following actions: " + Environment.NewLine +
                                             Environment.NewLine +
                                             "1. Start the ATP system." + Environment.NewLine +
                                             "2. Press OK on THIS window.");
        }

        public override void PostExecution()
        {
            // Post-test cleanup.
            DmiActions.ShowInstruction(this, @"Perform the following actions: " + Environment.NewLine +
                                             Environment.NewLine +
                                             "Press OK on THIS window to deactivate cab and finish test.");
            RigControl.SetMCSState(this, 1, RigControl.CabState.Shutdown);
        }

        public override bool TestcaseEntryPoint()
        {
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 0;
            // Test case entry point. 

            #region Cab Activation

            DmiActions.ShowInstruction(this, @"Perform the following actions: " + Environment.NewLine +
                                             Environment.NewLine +
                                             "Press OK on THIS window to activate cab and start test.");
            RigControl.SetMCSState(this, 1, RigControl.CabState.Forward);

            DmiExpectedResults.Cabin_A_is_activated(this);
            DmiExpectedResults.Driver_ID_window_displayed_in_SB_mode(this);

            #endregion

            #region Driver ID entry

            DmiExpectedResults.Driver_ID_entered(this);

            #endregion

            #region Brake Test

            DmiExpectedResults.Brake_Test_Perform_Order(this, true);
            Wait_Realtime(5000);

            DmiExpectedResults.Level_window_displayed(this);

            #endregion

            #region Level entry

            DmiExpectedResults.Level_1_Selected(this);
            DmiExpectedResults.Main_Window_displayed(this, false);

            #endregion

            #region Train Data entry

            DmiExpectedResults.Train_Data_Button_pressed_and_released(this);
            DmiExpectedResults.Train_data_window_displayed(this);

            DmiExpectedResults.Fixed_Train_Data_entered(this, Variables.Fixed_Trainset_Captions.FLU);
            DmiExpectedResults.Fixed_Train_Data_validated(this, Variables.Fixed_Trainset_Captions.FLU);
            Wait_Realtime(1000);

            DmiExpectedResults.Train_data_validation_window_displayed(this);

            #endregion

            #region Train Data validation

            DmiExpectedResults.Train_Data_validation_completed(this);
            Wait_Realtime(5000);

            DmiExpectedResults.TRN_window_displayed(this);

            #endregion

            #region TRN entry

            DmiExpectedResults.TRN_entered(this);

            DmiExpectedResults.Main_Window_displayed(this, true);

            #endregion

            #region Start

            DmiExpectedResults.Start_Button_pressed_and_released(this);
            DmiExpectedResults.Default_Window_Displayed(this);
            DmiExpectedResults.SR_Mode_Ack_requested(this);

            #endregion

            #region SR mode acknowledgement

            DmiActions.ShowInstruction(this, @"Perform the following action after pressing OK: " + Environment.NewLine +
                                             Environment.NewLine +
                                             "1. Press DMI Sub Area C1.");
            DmiExpectedResults.SR_Mode_Ack_pressed_and_hold(this);

            DmiExpectedResults.SR_Mode_displayed(this);
            DmiExpectedResults.Driver_symbol_displayed(this, "Level 1", "LE03", "C8", true);

            #endregion


            return GlobalTestResult;
        }
    }
}
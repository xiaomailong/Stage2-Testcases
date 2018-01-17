#region usings

using System;
using BT_CSB_Tools.SignalPoolGenerator.Signals.PdSignal.Misc;
using CL345;
using Testcase.Telegrams.EVCtoDMI;
using Testcase.Telegrams.DMItoEVC;
using static Testcase.Telegrams.EVCtoDMI.Variables;

#endregion

namespace Testcase.DMITestCases
{
    /// <summary>
    /// These are the generic methods used to check expected results on the DMI
    /// </summary>
    public static partial class DmiExpectedResults
    {
        /// <summary>
        /// Use when a DMI-EVC telegram is not received by RTSim due to the SMDStat flag not being set
        /// </summary>
        /// <param name="pool">The SignalPool</param>
        /// <param name="telegramString"></param>
        public static void DMItoEVC_Telegram_Not_Received(SignalPool pool, string telegramString)
        {
            pool.TraceError($"{telegramString} telegram was NOT received by RTSim.");
        }

        /// <summary>
        /// Used when TC is not needed since it tests the same interfaces as another test case.
        /// </summary>
        /// <param name="pool">The SignalPool</param>
        /// <param name="testcaseId">Testcase ID as per specification</param>
        /// <param name="sectionNumber">Section number as per specification</param>
        public static void Testcase_not_required(SignalPool pool, string testcaseId, string sectionNumber)
        {
            pool.TraceInfo($"This test case is not required since it tests the same interfaces as TC {testcaseId}" +
                           $" in section {sectionNumber} of the specification.");
        } 

        /// <summary>
        /// Prompt for verification of symbol displayed on the DMI.
        /// </summary>
        /// <param name="pool">The SignalPool</param>
        /// <param name="symbolName">Symbol name as described in ERA_ERTMS_015560 v3.4</param>
        /// <param name="symbolNumber">Symbol number as described in ERA_ERTMS_015560 v3.4</param>
        /// <param name="symbolArea">Area of the DMI where the symbol should be displayed</param>
        /// <param name="yellowBorder">Boolean of whether the symbol should have a yellow border</param>
        public static void Driver_symbol_displayed(SignalPool pool, string symbolName, string symbolNumber,
            string symbolArea,
            bool yellowBorder)
        {
            if (yellowBorder)
                pool.WaitForVerification($"Is the {symbolName} symbol ({symbolNumber}) " +
                                         $"displayed with a yellow border in area {symbolArea}?");
            else
                pool.WaitForVerification($"Is the {symbolName} symbol ({symbolNumber}) " +
                                         $"displayed in area {symbolArea}?");
        }

        public static void Driver_symbol_deleted(SignalPool pool, string symbolName, string symbolNumber,
           string symbolArea)
        {

                pool.WaitForVerification($"Is the {symbolName} symbol ({symbolNumber}) " +
                                         $"removed from area {symbolArea}?");
        }

        public static void TAF_ack_pressed_and_released(SignalPool pool)
        {
            EVC111_MMIDriverMessageAck.MMI_Q_ACK = MMI_Q_ACK.AcknowledgeYES;
            EVC111_MMIDriverMessageAck.MMI_Q_BUTTON = MMI_Q_BUTTON.Pressed;
            EVC111_MMIDriverMessageAck.MMI_Q_BUTTON = MMI_Q_BUTTON.Released;
            EVC152_MMIDriverAction.Check_MMI_M_DRIVER_ACTION = EVC152_MMIDriverAction.MMI_M_DRIVER_ACTION.TrackAheadFreeConfirmation;
        }

        /// <summary>
        /// Description: Level 0 acknowledgement is requested on DMI area C1
        /// Used in:
        ///     Step 1 in TC-ID: 15.1.4 in 20.1.4
        /// </summary>
        /// <param name="pool">The SignalPool</param>
        public static void L0_Announcement_Ack_Requested(SignalPool pool)
        {
            Driver_symbol_displayed(pool, "Acknowledgement for Level 0 announcement", "LE07", "C1", true);
        }

        /// <summary>
        /// Description: Level 0 Acknowledgement symbol on DMI area C1 is pressed and released.
        /// Used in:
        ///     Step 1 in TC-ID: 15.1.4 in 20.1.4
        ///     Step 4 in TC-ID: 15.2.4 in 20.2.4
        /// </summary>
        public static void L0_Announcement_Ack_pressed_and_released(SignalPool pool)
        {
            EVC111_MMIDriverMessageAck.MMI_Q_ACK = MMI_Q_ACK.AcknowledgeYES;
            EVC111_MMIDriverMessageAck.MMI_Q_BUTTON = MMI_Q_BUTTON.Pressed;
            EVC111_MMIDriverMessageAck.MMI_Q_BUTTON = MMI_Q_BUTTON.Released;
            EVC152_MMIDriverAction.Check_MMI_M_DRIVER_ACTION = EVC152_MMIDriverAction.MMI_M_DRIVER_ACTION.Level0Ack;
            pool.WaitForVerification(
                "Has the LE07 symbol disappeared and been replaced with LE06 symbol in sub-area C1?");
        }

        /// <summary>
        /// Description: Level 1 acknowledgement is requested on DMI area C1
        /// Used in:
        ///     Step 1 in TC-ID: 15.1.4 in 20.1.4
        /// </summary>
        /// <param name="pool">The SignalPool</param>
        public static void L1_Announcement_Ack_Requested(SignalPool pool)
        {
            Driver_symbol_displayed(pool, "Acknowledgement for Level 1 announcement", "LE11", "C1", true);
        }

        /// <summary>
        /// Description: Level 1 Acknowledgement symbol on DMI area C1 is pressed and released.
        /// Used in:
        ///     Step 1 in TC-ID: 15.1.4 in 20.1.4
        ///     Step 4 in TC-ID: 15.2.4 in 20.2.4
        /// </summary>
        public static void L1_Announcement_Ack_pressed_and_released(SignalPool pool)
        {
            EVC111_MMIDriverMessageAck.MMI_Q_ACK = MMI_Q_ACK.AcknowledgeYES;
            EVC111_MMIDriverMessageAck.MMI_Q_BUTTON = MMI_Q_BUTTON.Pressed;
            EVC111_MMIDriverMessageAck.MMI_Q_BUTTON = MMI_Q_BUTTON.Released;
            EVC152_MMIDriverAction.Check_MMI_M_DRIVER_ACTION = EVC152_MMIDriverAction.MMI_M_DRIVER_ACTION.Level1Ack;
            pool.WaitForVerification(
                "Has the LE11 symbol disappeared and been replaced with LE10 symbol in sub-area C1?");
        }

        /// <summary>
        /// Description: LS Mode acknowledgement is requested on DMI area C1
        /// Used in:
        ///     Step 1 in TC-ID: 15.1.6 in 20.1.6
        /// </summary>
        public static void LS_Mode_Ack_Requested(SignalPool pool)
        {
            Driver_symbol_displayed(pool, "Acknowledgement for Limited Supervision mode", "MO22", "C1", true);
        }

        /// <summary>
        /// Description: LS mode Acknowledgement symbol on DMI area C1 is pressed and released.
        /// Used in:
        ///     Step 2 in TC-ID: 15.1.6 in 20.1.6
        /// </summary>
        public static void LS_Mode_Ack_pressed_and_released(SignalPool pool)
        {
            EVC111_MMIDriverMessageAck.MMI_Q_ACK = MMI_Q_ACK.AcknowledgeYES;
            EVC111_MMIDriverMessageAck.MMI_Q_BUTTON = MMI_Q_BUTTON.Pressed;
            EVC111_MMIDriverMessageAck.MMI_Q_BUTTON = MMI_Q_BUTTON.Released;
            EVC152_MMIDriverAction.Check_MMI_M_DRIVER_ACTION =
                EVC152_MMIDriverAction.MMI_M_DRIVER_ACTION.LimitedSupervisionModeAck;
            pool.WaitForVerification("Has the MO22 symbol disappeared from sub-area C1?");
        }

        /// <summary>
        /// Description: DMI displays LS mode
        /// Used in:
        ///     Step 2 in TC-ID: 15.1.6 in 20.1.6
        /// </summary>
        public static void LS_Mode_displayed(SignalPool pool)
        {
            EVC102_MMIStatusReport.Check_MMI_M_MODE_READBACK =
                EVC102_MMIStatusReport.MMI_M_MODE_READBACK.LimitedSupervision;
            Driver_symbol_displayed(pool, "Limited Supervision mode", "MO21", "B7", false);
        }

        /// <summary>
        /// Description: DMI displays LSSMA in sub-area A1
        /// Used in:
        ///     Step 2 in TC-ID: 15.1.6 in 20.1.6
        /// </summary>
        /// <param name="pool">The SignalPool</param>
        public static void LSSMA_displayed(SignalPool pool)
        {
            Driver_symbol_displayed(pool, "LSSMA", "LS01", "A1", false);
            pool.WaitForVerification(
                "Is the the number of LSSMA displayed vertically and horizontally centered in sub-area A1?");
            pool.WaitForVerification("Does the number of LSSMA overlays the LS01 symbol?");
            pool.WaitForVerification("Is the colour of LSSMA grey?");
        }

        /// <summary>
        /// Description: OS Mode acknowledgement is requested on DMI area C1
        /// Used in:
        ///     Step 11 in TC-ID: 15.1.3 in 20.1.3
        /// </summary>
        public static void OS_Mode_Ack_Requested(SignalPool pool)
        {
            Driver_symbol_displayed(pool, "Acknowledgement for On Sight mode", "MO08", "C1", true);
        }

        /// <summary>
        /// Description: OS mode Acknowledgement symbol on DMI area C1 is pressed and released.
        /// Used in:
        ///     Step 12 in TC-ID: 15.1.3 in 20.1.3
        /// </summary>
        public static void OS_Mode_Ack_pressed_and_released(SignalPool pool)
        {
            EVC111_MMIDriverMessageAck.MMI_Q_ACK = MMI_Q_ACK.AcknowledgeYES;
            EVC111_MMIDriverMessageAck.MMI_Q_BUTTON = MMI_Q_BUTTON.Pressed;
            EVC111_MMIDriverMessageAck.MMI_Q_BUTTON = MMI_Q_BUTTON.Released;
            EVC152_MMIDriverAction.Check_MMI_M_DRIVER_ACTION =
                EVC152_MMIDriverAction.MMI_M_DRIVER_ACTION.OnSightModeAck;
            pool.WaitForVerification("Has the MO08 symbol disappeared from sub-area C1?");
        }

        /// <summary>
        /// Description: SR Mode acknowledgement is requested on DMI area C1
        /// Used in:
        ///     Step 2 in TC-ID: 15.1.1 in 20.1.1
        /// </summary>
        public static void SR_Mode_Ack_requested(SignalPool pool)
        {
            Driver_symbol_displayed(pool, "Acknowledgement for Staff Responsible mode", "MO10", "C1", true);
        }

        /// <summary>
        /// Description: SR mode Acknowledgement symbol on DMI area C1 is pressed and hold
        /// Used in:
        ///     Step 4 in TC-ID: 15.1.1 in 20.1.1
        /// </summary>
        public static void SR_Mode_Ack_pressed_and_hold(SignalPool pool)
        {
            EVC152_MMIDriverAction.Check_MMI_M_DRIVER_ACTION =
                EVC152_MMIDriverAction.MMI_M_DRIVER_ACTION.StaffResponsibleModeAck;
            pool.WaitForVerification(
                "Has the MO10 (Acknowledgement for Staff Responsible mode) symbol disappeared from sub-area C1?");
        }

        /// <summary>
        /// Description: UN Mode acknowledgement is requested on DMI area C1
        /// Used in:
        ///     Step 8 in TC-ID: 15.1.3 in 20.1.3
        /// </summary>
        public static void UN_Mode_Ack_requested(SignalPool pool)
        {
            Driver_symbol_displayed(pool, "Acknowledgement for Unfitted mode", "MO17", "C1", true);
        }

        /// <summary>
        /// Description: OS Mode acknowledgement is requested on DMI area C1
        /// Used in:
        ///     Step 13 in TC-ID: 14.1 in 19.1
        /// </summary>
        public static void OS_Mode_Ack_requested(SignalPool pool)
        {
            Driver_symbol_displayed(pool, "Acknowledgement for On-Sight mode", "MO08", "C1", true);
        }

        /// <summary>
        /// Description: LS Mode acknowledgement is requested on DMI area C1
        /// Used in:
        ///     Step 16 in TC-ID: 14.1 in 19.1
        /// </summary>
        public static void LS_Mode_Ack_requested(SignalPool pool)
        {
            Driver_symbol_displayed(pool, "Acknowledgement for Limited Supervision mode", "MO22", "C1", true);
        }

        /// <summary>
        /// Description: UN mode Acknowledgement symbol on DMI area C1 is pressed and released.
        /// Used in:
        ///     Step 9 in TC-ID: 15.1.3 in 20.1.3
        /// </summary>
        public static void UN_Mode_Ack_pressed_and_released(SignalPool pool)
        {
            EVC111_MMIDriverMessageAck.MMI_Q_ACK = MMI_Q_ACK.AcknowledgeYES;
            EVC111_MMIDriverMessageAck.MMI_Q_BUTTON = MMI_Q_BUTTON.Pressed;
            EVC111_MMIDriverMessageAck.MMI_Q_BUTTON = MMI_Q_BUTTON.Released;
            EVC152_MMIDriverAction.Check_MMI_M_DRIVER_ACTION =
                EVC152_MMIDriverAction.MMI_M_DRIVER_ACTION.UnfittedModeAck;
            pool.WaitForVerification(
                "Has the MO17 (Acknowledgement for Unfitted mode) symbol disappeared from sub-area C1?");
        }

        /// <summary>
        /// Description: DMI displays TR mode
        /// Used in:
        ///     Step 6 in TC-ID: 15.1.1 in 20.1.1
        /// </summary>
        public static void TR_Mode_displayed(SignalPool pool)
        {
            EVC102_MMIStatusReport.Check_MMI_M_MODE_READBACK = EVC102_MMIStatusReport.MMI_M_MODE_READBACK.Trip;
            Driver_symbol_displayed(pool, "Trip mode", "MO04", "B7", false);
        }

        /// <summary>
        /// Description: TR Mode acknowledgement is requested on DMI area C1
        /// Used in:
        ///     Step 2 in TC-ID: 15.1.1 in 20.1.1
        /// </summary>
        public static void TR_Mode_Ack_requested(SignalPool pool)
        {
            Driver_symbol_displayed(pool, "Acknowledgement for Train Trip", "MO05", "C1", true);
        }

        /// <summary>
        /// Description: Brake intervention symbol on DMI area C9 is pressed and released.
        /// Used in:
        ///     Step 7 in TC-ID: 15.1.1 in 20.1.1
        /// </summary>
        public static void Brake_Intervention_symbol_pressed_and_released(SignalPool pool)
        {
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_EB_Status = 0;
        }

        /// <summary>
        /// Description: TR mode Acknowledgement symbol on DMI area C1 is pressed and released.
        /// Used in:
        ///     Step 8 in TC-ID: 15.1.1 in 20.1.1
        /// </summary>
        public static void TR_Mode_Ack_pressed_and_released(SignalPool pool)
        {
            EVC111_MMIDriverMessageAck.MMI_Q_ACK = MMI_Q_ACK.AcknowledgeYES;
            EVC111_MMIDriverMessageAck.MMI_Q_BUTTON = MMI_Q_BUTTON.Pressed;
            EVC111_MMIDriverMessageAck.MMI_Q_BUTTON = MMI_Q_BUTTON.Released;
            EVC152_MMIDriverAction.Check_MMI_M_DRIVER_ACTION = EVC152_MMIDriverAction.MMI_M_DRIVER_ACTION.TrainTripAck;
            pool.WaitForVerification("Has the MO05 (Acknowledgement for Trip) symbol disappeared from sub-area C1?");
        }

        /// <summary>
        /// Description: DMI displays PT mode
        /// Used in:
        ///     Step 8 in TC-ID: 15.1.1 in 20.1.1
        /// </summary>
        public static void PT_Mode_displayed(SignalPool pool)
        {
            EVC102_MMIStatusReport.Check_MMI_M_MODE_READBACK = EVC102_MMIStatusReport.MMI_M_MODE_READBACK.PostTrip;
            Driver_symbol_displayed(pool, "Post Trip mode", "MO06", "B7", false);
        }

        /// <summary>
        /// Description: DMI displays RV mode
        /// Used in:
        ///     Step 4 in TC-ID: 15.1.2 in 20.1.2
        /// </summary>
        public static void RV_Mode_displayed(SignalPool pool)
        {
            EVC102_MMIStatusReport.Check_MMI_M_MODE_READBACK = EVC102_MMIStatusReport.MMI_M_MODE_READBACK.Reversing;
            Driver_symbol_displayed(pool, "Reversing mode", "MO14", "B7", false);
        }

        /// <summary>
        /// Description: DMI displays RV mode
        /// Used in:
        ///     Step 9 in TC-ID: 15.1.3 in 20.1.3
        /// </summary>
        public static void UN_Mode_displayed(SignalPool pool)
        {
            EVC102_MMIStatusReport.Check_MMI_M_MODE_READBACK = EVC102_MMIStatusReport.MMI_M_MODE_READBACK.Unfitted;
            Driver_symbol_displayed(pool, "Unfitted mode", "MO16", "B7", false);
        }

        /// <summary>
        /// Description: DMI displays Main window with enabled ‘Start’ button
        /// Used in:
        ///     Step 1 in TC-ID: 5.10 in 10.10 Screen Layout: Button States
        ///     Step 3 in TC-ID: 22.20.2 in 27.20.2 Override window in SB mode
        ///     Step 9 in TC-ID: 15.1.1 in 20.1.1
        ///     Step 1 in TC-ID: 15.2.1 in 20.2.1
        /// </summary>
        public static void Main_Window_displayed(SignalPool pool, bool startButtonEnabled)
        {
            if (startButtonEnabled)
            {
                pool.WaitForVerification("Is the Main window displayed on the DMI, with the Start button enabled?");
            }
            else
            {
                pool.WaitForVerification("Is the Main window displayed on the DMI?");
            }
        }

        /// <summary>
        /// Description: Start button is pressed and released
        /// Used in:
        ///     Step 1 in TC-ID: 5.10 in 10.10 Screen Layout: Button States
        ///     Step 3 in TC-ID: 22.20.2 in 27.20.2 Override window in SB mode
        ///     Step 9 in TC-ID: 15.1.1 in 20.1.1
        ///     Step 8 in TC-ID: 15.1.3 in 20.1.3
        /// </summary>
        public static void Start_Button_pressed_and_released(SignalPool pool)
        {
            DmiActions.ShowInstruction(pool, "Perform the following action after pressing OK:" + Environment.NewLine +
                                             Environment.NewLine +
                                             "1. Press ‘Start’ button.");
            //EVC101_MMIDriverRequest.CheckMRequestPressed = Variables.MMI_M_REQUEST.Start;
            EVC101_MMIDriverRequest.CheckMRequestReleased = MMI_M_REQUEST.Start;
            EVC152_MMIDriverAction.Check_MMI_M_DRIVER_ACTION = EVC152_MMIDriverAction.MMI_M_DRIVER_ACTION.StartSelected;
        }

        /// <summary>
        /// Description: Close button on Level selection window is pressed
        /// Used in:
        ///     Step 3 in TC-ID: 15.2.3 in 20.2.3 
        /// </summary>
        public static void Close_Button_Level_Window_pressed_and_released(SignalPool pool)
        {
            DmiActions.ShowInstruction(pool, "Perform the following action after pressing OK:" + Environment.NewLine +
                                             Environment.NewLine +
                                             "1. Press close button.");
            EVC101_MMIDriverRequest.CheckMRequestPressed = MMI_M_REQUEST.ExitChangeLevel;
            EVC101_MMIDriverRequest.CheckMRequestReleased = MMI_M_REQUEST.ExitChangeLevel;
        }

        /// <summary>
        /// Description: Shunting button is pressed and hold
        /// Used in:
        ///     Step 10 in TC-ID: 15.1.1 in 20.1.1     
        /// </summary>
        public static void Shunting_button_pressed_and_hold(SignalPool pool)
        {
            EVC101_MMIDriverRequest.CheckMRequestPressed = MMI_M_REQUEST.StartShunting;
            pool.Wait_Realtime(2000);
            EVC101_MMIDriverRequest.CheckMRequestReleased = MMI_M_REQUEST.StartShunting;
            EVC152_MMIDriverAction.Check_MMI_M_DRIVER_ACTION =
                EVC152_MMIDriverAction.MMI_M_DRIVER_ACTION.ShuntingSelected;
        }

        /// <summary>
        /// Description: Set VBC button is pressed and released
        /// Used in:
        ///     Step 1 in TC-ID: 22.27.1 in 27.27.1
        /// </summary>
        public static void Set_VBC_Button_pressed_and_released(SignalPool pool)
        {
            DmiActions.ShowInstruction(pool, "Perform the following action after pressing OK:" + Environment.NewLine +
                                             Environment.NewLine +
                                             "1. Press Set VBC button.");
            EVC101_MMIDriverRequest.CheckMRequestPressed = MMI_M_REQUEST.StartSetVBC;
            EVC101_MMIDriverRequest.CheckMRequestReleased = MMI_M_REQUEST.StartSetVBC;
        }

        /// <summary>
        /// Description: Data View button is pressed and released
        /// Used in:
        ///     
        /// </summary>
        public static void Data_View_Button_pressed_and_released(SignalPool pool)
        {
            DmiActions.ShowInstruction(pool, "Perform the following action after pressing OK:" + Environment.NewLine +
                                             Environment.NewLine +
                                             "1. Press ‘Data view’ button.");
            EVC101_MMIDriverRequest.CheckMRequestPressed = MMI_M_REQUEST.StartTrainDataView;
            EVC101_MMIDriverRequest.CheckMRequestReleased = MMI_M_REQUEST.StartTrainDataView;
        }

        /// <summary>
        /// Description: Settings button is pressed and released
        /// Used in:
        ///     Step 1 in TC-ID: 22.27.1 in 27.27.1
        /// </summary>
        public static void Settings_Button_pressed_and_released(SignalPool pool)
        {
            DmiActions.ShowInstruction(pool, "Perform the following action after pressing OK:" + Environment.NewLine +
                                             Environment.NewLine +
                                             "1. Press ‘Settings’ button.");
            EVC101_MMIDriverRequest.CheckMRequestPressed = MMI_M_REQUEST.Settings;
            EVC101_MMIDriverRequest.CheckMRequestReleased = MMI_M_REQUEST.Settings;
        }

        /// <summary>
        /// Description: System Version button is pressed and released
        /// Used in:
        ///     Step 1 in TC-ID: 22.27.1 in 27.27.1
        /// </summary>
        public static void System_Version_Button_pressed_and_released(SignalPool pool)
        {
            DmiActions.ShowInstruction(pool, "Perform the following action after pressing OK:" + Environment.NewLine +
                                             Environment.NewLine +
                                             "1. Press ‘System Version’ button.");
            EVC101_MMIDriverRequest.CheckMRequestPressed = MMI_M_REQUEST.SystemVersionRequest;
            EVC101_MMIDriverRequest.CheckMRequestReleased = MMI_M_REQUEST.SystemVersionRequest;
        }

        /// <summary>
        /// Description: DMI displays SH mode
        /// Used in:
        ///     Step 8 in TC-ID: 15.1.1 in 20.1.1
        /// </summary>
        public static void SH_Mode_displayed(SignalPool pool)
        {
            EVC102_MMIStatusReport.Check_MMI_M_MODE_READBACK = EVC102_MMIStatusReport.MMI_M_MODE_READBACK.Shunting;
            Driver_symbol_displayed(pool, "Shunting mode", "MO01", "B7", false);
        }

        /// <summary>
        /// Description: The Driver ID window is displayed
        /// Used in:
        ///     Step 4 in TC-ID: 22.17 in 27.17.1 Driver ID window: General Display
        ///     Step 1 in TC-ID: 33.1 in 36.1 The relationship between parent and child windows (1)
        ///     Step 5 in TC-ID: 1.1 in 6.1 Properties of each Display Unit’s Screen
        ///     Step 1 in TC-ID: 1.6 in 6.6 Adjustment of Sound Volume
        ///     Step 1 in TC-ID: 10.4.1.1 in 15.4.1.1 State ‘ST05’: Abort the pending Data Process in Main window
        ///     Step 1 in TC-ID: 15.2.4 in 20.2.4 ETCS Level: ETCS Level Transitions by receiving data packet from ETCS Onboard (L1->L0, L0->L1)
        ///     Step 1 in TC-ID: 17.1.1 in 22.1.1 Planning Area: General Appearance
        ///     Step 1 in TC-ID: 17.1.2 in 22.1.2 Planning Area is suppressed in Level 1 and OS mode
        ///     Step 1 in TC-ID: 17.1.3 in 22.1.3 Planning Area displays according to configuration in OS and SR mode.
        ///     Step 1 in TC-ID: 17.2.1 in 22.2.1 Planning Area-Layering: PASP and PA Distance scale
        ///     Step 1 in TC-ID: 17.2.2 in 22.2.2 Planning Area-Layering: Display information when PA data is empty
        ///     Step 1 in TC-ID: 17.9.1 in 22.9.1 Hide PA Function: General appearance
        ///     Step 1 in TC-ID: 17.9.11 in 22.9.11 Hide PA Function configured ‘STORED’ with re-activate cabin
        ///     Step 4 in TC-ID: 17.9.11 in 22.9.11 Hide PA Function configured ‘STORED’ with re-activate cabin
        ///     Step 9 in TC-ID: 17.9.11 in 22.9.11 Hide PA Function configured ‘STORED’ with re-activate cabin
        ///     Step 15 in TC-ID: 17.9.11 in 22.9.11 Hide PA Function configured ‘STORED’ with re-activate cabin
        ///     Step 1 in TC-ID: 18.1.1.1.1 in 23.1.1.1.1 Concise Visualization
        ///     Step 2 in TC-ID: 22.5.2 in 27.5.2 Level Selection window: Packet sending/receiving
        ///     Step 2 in TC-ID: 22.8.2.1 in 27.8.2.1 Radio Network ID window: General appearance
        ///     Step 1 in TC-ID: 22.26 in 27.26 System info window
        ///     Step 1 in TC-ID: 34.1.4 in 37.1.4.1.1 Data entry/validation process when enabling conditions not fullfilled: Level 1
        ///     Step 11 in TC-ID: 15.1.1 in 20.1.1
        ///     Step 5 in TC-ID: 1.1 in 6.1 Properties of each Display Unit’s Screen
        ///     Step 1 in TC-ID: 1.6 in 6.6 Adjustment of Sound Volume
        ///     Step 1 in TC-ID: 10.4.1.1 in 15.4.1.1 State ‘ST05’: Abort the pending Data Process in Main window
        ///     Step 1 in TC-ID: 15.2.4 in 20.2.4 ETCS Level: ETCS Level Transitions by receiving data packet from ETCS Onboard (L1->L0, L0->L1)
        ///     Step 1 in TC-ID: 17.1.1 in 22.1.1 Planning Area: General Appearance
        ///     Step 1 in TC-ID: 17.1.2 in 22.1.2 Planning Area is suppressed in Level 1 and OS mode
        ///     Step 1 in TC-ID: 17.1.3 in 22.1.3 Planning Area displays according to configuration in OS and SR mode.
        ///     Step 1 in TC-ID: 17.2.1 in 22.2.1 Planning Area-Layering: PASP and PA Distance scale
        ///     Step 1 in TC-ID: 17.2.2 in 22.2.2 Planning Area-Layering: Display information when PA data is empty
        ///     Step 1 in TC-ID: 17.9.1 in 22.9.1 Hide PA Function: General appearance
        ///     Step 1 in TC-ID: 17.9.11 in 22.9.11 Hide PA Function configured ‘STORED’ with re-activate cabin
        ///     Step 4 in TC-ID: 17.9.11 in 22.9.11 Hide PA Function configured ‘STORED’ with re-activate cabin
        ///     Step 9 in TC-ID: 17.9.11 in 22.9.11 Hide PA Function configured ‘STORED’ with re-activate cabin
        ///     Step 15 in TC-ID: 17.9.11 in 22.9.11 Hide PA Function configured ‘STORED’ with re-activate cabin
        ///     Step 1 in TC-ID: 18.1.1.1.1 in 23.1.1.1.1 Concise Visualization
        ///     Step 2 in TC-ID: 22.5.2 in 27.5.2 Level Selection window: Packet sending/receiving
        ///     Step 2 in TC-ID: 22.8.2.1 in 27.8.2.1 Radio Network ID window: General appearance
        ///     Step 1 in TC-ID: 22.26 in 27.26 System info window
        ///     Step 1 in TC-ID: 34.1.4 in 37.1.4.1.1 Data entry/validation process when enabling conditions not fullfilled: Level 1
        /// </summary>
        public static void Driver_ID_window_displayed(SignalPool pool)
        {
            pool.WaitForVerification("Is the Driver ID window displayed?");
        }

        /// <summary>
        /// Description: DMI displays Driver ID window in SB mode
        /// Used in:
        ///     Step 1 in TC-ID: 22.5.4  in 27.5.4 Level Selection window: 8 STMs handling
        ///     Step 1 in TC-ID: 35.2 in 38.2 NTC System Status Messages
        ///     Step 7 in TC-ID: 35.2 in 38.2 NTC System Status Messages
        ///     Step 1 in TC-ID: 17.1.3 in 20.1.3 
        /// </summary>
        public static void Driver_ID_window_displayed_in_SB_mode(SignalPool pool)
        {
            pool.WaitForVerification("Is the Driver Id window displayed?");
            Driver_symbol_displayed(pool, "Stand By Mode", "MO13", "B7", false);
            EVC102_MMIStatusReport.Check_MMI_M_MODE_READBACK = EVC102_MMIStatusReport.MMI_M_MODE_READBACK.StandBy;
        }

        /// <summary>
        /// Description: Default Window is displayed with Override Symbol in area C7.
        /// Used in:
        ///     Step 14 in TC-ID: 17.1.3 in 20.1.3
        /// </summary>
        /// <param name="pool"></param>
        public static void Default_Window_with_Override_Symbol(SignalPool pool)
        {
            pool.WaitForVerification("Is the Default window displayed?");
            Driver_symbol_displayed(pool, "Override EOA", "MO03", "C7", false);
        }

        /// <summary>
        /// Description: Non-leading button is pressed and hold
        /// Used in:
        ///     Step 11 in TC-ID: 15.1.1 in 20.1.1     
        /// </summary>
        public static void Non_leading_button_pressed_and_hold(SignalPool pool)
        {
            EVC101_MMIDriverRequest.CheckMRequestPressed = MMI_M_REQUEST.StartNonLeading;
            pool.Wait_Realtime(2000);
            EVC101_MMIDriverRequest.CheckMRequestReleased = MMI_M_REQUEST.StartNonLeading;
            EVC152_MMIDriverAction.Check_MMI_M_DRIVER_ACTION =
                EVC152_MMIDriverAction.MMI_M_DRIVER_ACTION.NonLeadingSelected;
        }

        /// <summary>
        /// Description: DMI displays NL mode
        /// Used in:
        ///     Step 11 in TC-ID: 15.1.1 in 20.1.1
        /// </summary>
        public static void NL_Mode_displayed(SignalPool pool)
        {
            EVC102_MMIStatusReport.Check_MMI_M_MODE_READBACK = EVC102_MMIStatusReport.MMI_M_MODE_READBACK.NonLeading;
            Driver_symbol_displayed(pool, "Non-leading mode", "MO12", "B7", false);
        }

        /// <summary>
        /// Description: DMI displays SF mode
        /// Used in:
        ///     Step 12 in TC-ID: 15.1.1 in 20.1.1
        /// </summary>
        public static void SF_Mode_displayed(SignalPool pool)
        {
            EVC102_MMIStatusReport.Check_MMI_M_MODE_READBACK = EVC102_MMIStatusReport.MMI_M_MODE_READBACK.SystemFailure;
            Driver_symbol_displayed(pool, "System failure mode", "MO18", "B7", false);
        }

        /// <summary>
        /// Description: DMI displays RV permitted symbol
        /// Used in:
        ///     Step 2 in TC-ID: 15.1.2 in 20.1.2
        /// </summary>
        public static void RV_Permitted_Symbol_displayed(SignalPool pool)
        {
            Driver_symbol_displayed(pool, "RV permitted", "ST06", "C6", false);
        }

        /// <summary>
        /// Description: DMI displays RV permitted symbol
        /// Used in:
        ///     Step 3 in TC-ID: 15.1.2 in 20.1.2
        /// </summary>
        public static void RV_Mode_Ack_requested(SignalPool pool)
        {
            Driver_symbol_displayed(pool, "Acknowledgement for Reversing mode", "MO15", "C1", true);
        }

        /// <summary>
        /// Description: Driver ID is entered
        /// Used in:
        ///     Step 2 in TC-ID: 15.1.3 in 20.1.3
        /// </summary>
        /// <param name="pool">The SignalPool</param>
        public static void Driver_ID_entered(SignalPool pool)
        {
            DmiActions.ShowInstruction(pool, "Perform the following action after pressing OK:" + Environment.NewLine +
                                             Environment.NewLine +
                                             "1. Enter and validate Driver ID");

            string driverIdInput = EVC104_MMINewDriverData.Get_X_DRIVER_ID;
            pool.WaitForVerification($"Is \"{driverIdInput}\" the Driver ID you entered?");
        }

        /// <summary>
        /// Description: Train running Number is entered
        /// Used in:
        ///     Step 7 in TC-ID: 15.1.3 in 20.1.3
        /// </summary>
        /// <param name="pool">The SignalPool</param>
        public static void TRN_entered(SignalPool pool)
        {
            DmiActions.ShowInstruction(pool, "Perform the following action after pressing OK:" + Environment.NewLine +
                                             Environment.NewLine +
                                             "1. Enter and validate Train Running Number");
            uint trnInput = EVC116_MMINewTrainNumber.Get_NID_OPERATION;
            pool.WaitForVerification($"Is {trnInput} the Train running number entered?");
        }

        /// <summary>
        /// Description: 
        /// Used in:
        ///     Step 2 in TC-ID: 15.1.3 in 20.1.3
        /// </summary>
        /// <param name="pool">The SignalPool</param>
        /// <param name="order">Indicates if Driver allows Brake Test to be performed</param>
        public static void Brake_Test_Perform_Order(SignalPool pool, bool order)
        {
            var sOrder = order ? "Yes" : "No";

            DmiActions.ShowInstruction(pool, "Perform the following action after pressing OK:" + Environment.NewLine +
                                             Environment.NewLine +
                                             "1. Press \"" + sOrder + "\" on DMI in area E.");

            EVC111_MMIDriverMessageAck.MMI_I_TEXT = 1;

            if (order)
            {
                EVC111_MMIDriverMessageAck.MMI_Q_ACK = MMI_Q_ACK.AcknowledgeYES;
                EVC111_MMIDriverMessageAck.MMI_Q_BUTTON = MMI_Q_BUTTON.Released;
            }

            else
            {
                EVC111_MMIDriverMessageAck.MMI_Q_ACK = MMI_Q_ACK.NotAcknowledgeNO;
                EVC111_MMIDriverMessageAck.MMI_Q_BUTTON = MMI_Q_BUTTON.Released;
            }
        }

        /// <summary>
        /// Description: VBC code is entered
        /// Used in:
        ///     Step 10 in TC-ID: 22.27.1 in 27.27.1
        /// </summary>
        /// <param name="pool">The SignalPool</param>
        public static void VBC_code_entered(SignalPool pool)
        {
            DmiActions.ShowInstruction(pool, "Perform the following action after pressing OK:" + Environment.NewLine +
                                             Environment.NewLine +
                                             "1. Enter and validate VBC Code");

            uint vbcCode = EVC118_MMINewSetVbc.Get_M_VBC_CODE;
            pool.WaitForVerification($"Is \"{vbcCode}\" the VBC Code you entered?");
        }

        /// <summary>
        /// Description:
        /// Used in:
        ///     Step 4 in TC-ID: 15.1.3 in 20.1.3     
        /// </summary>
        public static void Train_Data_Button_pressed_and_released(SignalPool pool)
        {
            DmiActions.ShowInstruction(pool, "Perform the following action after pressing OK:" + Environment.NewLine +
                                             Environment.NewLine +
                                             "1. Press \"Train Data\".");
            EVC101_MMIDriverRequest.CheckMRequestReleased = MMI_M_REQUEST.StartTrainDataEntry;
            EVC152_MMIDriverAction.Check_MMI_M_DRIVER_ACTION =
                EVC152_MMIDriverAction.MMI_M_DRIVER_ACTION.TrainDataEntryRequested;
        }

        /// <summary>
        /// Description: Level 0 is selected
        /// Used in:
        ///     Step 3 in TC-ID: 15.1.3 in 20.1.3
        ///     Step 1 in TC-ID: 15.2.1 in 20.2.1
        /// </summary>
        /// <param name="pool">The SignalPool</param>
        public static void Level_0_Selected(SignalPool pool)
        {
            DmiActions.ShowInstruction(pool, "Perform the following action after pressing OK:" + Environment.NewLine +
                                             Environment.NewLine +
                                             "1. Select and enter Level 0");
            EVC121_MMINewLevel.LevelSelected = MMI_M_LEVEL_NTC_ID.L0;
            EVC152_MMIDriverAction.Check_MMI_M_DRIVER_ACTION =
                EVC152_MMIDriverAction.MMI_M_DRIVER_ACTION.Level0Selected;
        }

        /// <summary>
        /// Description: Level 1 is selected
        /// Used in:
        ///     Step 2 in TC-ID: 15.2.1 in 20.2.1
        /// </summary>
        /// <param name="pool">The SignalPool</param>
        public static void Level_1_Selected(SignalPool pool)
        {
            DmiActions.ShowInstruction(pool, "Perform the following action after pressing OK:" + Environment.NewLine +
                                             Environment.NewLine +
                                             "1. Select and enter Level 1");
            EVC121_MMINewLevel.LevelSelected = MMI_M_LEVEL_NTC_ID.L1;
            EVC152_MMIDriverAction.Check_MMI_M_DRIVER_ACTION =
                EVC152_MMIDriverAction.MMI_M_DRIVER_ACTION.Level1Selected;
        }

        /// <summary>
        /// Description: Level 2 is selected
        /// Used in:
        ///     Step 1 in TC-ID: 15.2.2 in 20.2.2
        /// </summary>
        /// <param name="pool">The SignalPool</param>
        public static void Level_2_Selected(SignalPool pool)
        {
            DmiActions.ShowInstruction(pool, "Perform the following action after pressing OK:" + Environment.NewLine +
                                             Environment.NewLine +
                                             "1. Select and enter Level 2");

            EVC121_MMINewLevel.LevelSelected = MMI_M_LEVEL_NTC_ID.L2;
            EVC152_MMIDriverAction.Check_MMI_M_DRIVER_ACTION =
                EVC152_MMIDriverAction.MMI_M_DRIVER_ACTION.Level2Selected;
        }

        /// <summary>
        /// Description: EOA button is pressed in Override window on DMI
        /// Used in:
        ///     Step 14 in TC-ID: 15.1.3 in 20.1.3
        /// </summary>
        /// <param name="pool">The SignalPool</param>
        public static void EOA_Button_pressed(SignalPool pool)
        {
            EVC101_MMIDriverRequest.CheckMRequestPressed = MMI_M_REQUEST.StartOverrideEOA;
            EVC152_MMIDriverAction.Check_MMI_M_DRIVER_ACTION =
                EVC152_MMIDriverAction.MMI_M_DRIVER_ACTION.OverrideSelected;
        }

        /// <summary>
        /// Description: Cabin A is deactivated
        /// Used in:
        ///     Step 6 in TC-ID: 1.2 in 6.2 Internal Components
        ///     Step 8 in TC-ID: 1.6 in 6.6 Adjustment of Sound Volume
        ///     Step 12 in TC-ID: 15.1.1 in 20.1.1
        /// </summary>
        public static void Cabin_A_is_activated(SignalPool pool)
        {
            EVC102_MMIStatusReport.Check_MMI_M_ACTIVE_CABIN = MMI_M_ACTIVE_CABIN.Cabin1Active;
        }

        /// <summary>
        /// Description: Cabin B is deactivated
        /// Used in:     
        ///     Step 12 in TC-ID: 15.1.1 in 20.1.1
        /// </summary>
        public static void Cabin_B_is_activated(SignalPool pool)
        {
            EVC102_MMIStatusReport.Check_MMI_M_ACTIVE_CABIN = MMI_M_ACTIVE_CABIN.Cabin2Active;
        }

        /// <summary>
        /// Description: Cabin B is deactivated
        /// Used in:     
        ///     Step 12 in TC-ID: 15.1.1 in 20.1.1
        /// </summary>
        public static void Cab_deactivated(SignalPool pool)
        {
            EVC102_MMIStatusReport.Check_MMI_M_ACTIVE_CABIN = MMI_M_ACTIVE_CABIN.NoCabinActive;
        }

        /// <summary>
        /// Description: The Train data window is displayed
        /// Used in:
        ///     Step 3 in TC-ID: 5.3 in 10.3 Screen Layout: Frames
        ///     Step 12 in TC-ID: 33.1 in 36.1 The relationship between parent and child windows (1)
        ///     Step 3 in TC-ID: 9.1 in Data Validation Window for Flexible train data entry window
        ///     Step 3 in TC-ID: 9.2 in 14.2 Data Validation Window for Fixed train data entry window
        ///     Step 39 in TC-ID: 22.29.1 in 27.29.1 Flexible Train data window: General appearances
        ///     Step 18 in TC-ID: 22.29.2 in 27.29.2 Fixed Train data window: General appearances
        /// </summary>
        public static void Train_data_window_displayed(SignalPool pool)
        {
            pool.WaitForVerification("Is the Train Data window displayed on the DMI?");
        }

        /// <summary>
        /// Description: DMI displays Train data validation window
        /// Used in:
        ///     Step 4 in TC-ID: 9.1 in Data Validation Window for Flexible train data entry window
        ///     Step 6 in TC-ID: 9.1 in Data Validation Window for Flexible train data entry window
        ///     Step 4 in TC-ID: 9.2 in 14.2 Data Validation Window for Fixed train data entry window
        ///     Step 6 in TC-ID: 9.2 in 14.2 Data Validation Window for Fixed train data entry window
        ///     Step 7 in TC-ID: 10.4.1.1 in 15.4.1.1 State ‘ST05’: Abort the pending Data Process in Main window
        ///     Step 5 in TC-ID: 15.1.3 in 20.1.3 Mode Symbols in Sub-Area B7 for OS, UN mode
        ///     Step 5 in TC-ID: 34.1.1 in 37.1.1 Fixed Train data entry
        ///     Step 5 in TC-ID: 34.1.2 in 37.1.2 Flexible Train data entry
        ///     Step 7 in TC-ID: 34.1.2 in 37.1.2 Flexible Train data entry
        ///     Step 7 in TC-ID: 34.1.4 in 37.1.4.1.1 Data entry/validation process when enabling conditions not fullfilled: Level 1
        /// </summary>
        public static void Train_data_validation_window_displayed(SignalPool pool)
        {
            pool.WaitForVerification("Is the Train Data Validation window displayed on the DMI?");
        }

        /// <summary>
        /// Description: DMI displays RBC contact window
        /// Used in:
        ///     Step 16 in TC-ID: 22.8.2.1 in 27.8.2.1 Radio Network ID window: General appearance
        ///     Step 14 in TC-ID: 22.8.3.1 in 27.8.3.1 RBC Contact window: General appearance
        ///     Step 2 in TC-ID: 33.3 in 36.2 The relationship between parent and child windows (2)
        ///     Step 4 in TC-ID: 33.3 in 36.2 The relationship between parent and child windows (2)
        ///     Step 6 in TC-ID: 33.3 in 36.2 The relationship between parent and child windows (2)
        ///     Step 19 in TC-ID: 22.8.3.1 in 27.8.3.1 RBC Contact window: General appearance
        ///     Step 21 in TC-ID: 22.8.3.1 in 27.8.3.1 RBC Contact window: General appearance
        ///     Step 2 in TC-ID: 15.2.2 in 20.2.2
        ///     Step 3 in TC-ID: 15.2.2 in 20.2.2
        /// </summary>
        public static void RBC_Contact_Window_displayed(SignalPool pool)
        {
            pool.WaitForVerification("Is the RBC Contact window displayed on the DMI?");
        }

        /// <summary>
        /// Description: DMI displays Train Running Number window.Verify the following information,The Train data validation is closed.Use the log file to confirm that DMI sends out the packet [MMI_CONFIRMED_TRAIN DATA (EVC-110)] with variable based on confirmed data
        /// Used in:
        ///     Step 8 in TC-ID: 9.1 in Data Validation Window for Flexible train data entry window
        ///     Step 8 in TC-ID: 9.2 in 14.2 Data Validation Window for Fixed train data entry window
        ///     Step 11 in TC-ID: 10.2 in 15.2.1 State 'ST05': General Appearance
        ///     Step 6 in TC-ID: 15.1.3 in 20.1.3 Mode Symbols in Sub-Area B7 for OS, UN mode
        ///     Step 9 in TC-ID: 34.1.4 in 37.1.4.1.1 Data entry/validation process when enabling conditions not fullfilled: Level 1
        ///     Step 1 in TC-ID: 18.5 in 23.5 Train Running Number
        ///     Step 17 in TC-ID: 22.18 in Train Running Number window
        ///     Step 4 in TC-ID: 33.1 in 36.1 The relationship between parent and child windows (1)
        ///     Step 4 in TC-ID: 35.2 in 38.2 NTC System Status Messages
        ///     Step 9 in TC-ID: 35.2 in 38.2 NTC System Status Messages
        /// </summary>
        public static void TRN_window_displayed(SignalPool pool)
        {
            pool.WaitForVerification("Is the Train Running Number window displayed on the DMI?");
        }

        /// <summary>
        /// Description: DMI displays Train Running Number window.Verify the following information,The Train data validation is closed.Use the log file to confirm that DMI sends out the packet [MMI_CONFIRMED_TRAIN DATA (EVC-110)] with variable based on confirmed data
        /// Used in:
        ///     Step 13 in TC-ID: 15.1.3 in 20.1.3
        /// </summary>
        /// <param name="pool">The SignalPool</param>
        public static void Override_window_displayed(SignalPool pool)
        {
            pool.WaitForVerification("Is the Override window displayed on the DMI?");
        }

        /// <summary>
        /// Description: DMI displays SR mode
        /// Used in:
        ///     Step 9 in TC-ID: 6.1 in 11.1 Acknowledgements: General
        ///     Step 4 in TC-ID: 15.1.1 in 20.1.1 Mode Symbols in Sub-Area B7 for SB, SR, FS, TR, PT, SH, NL and SF mode
        ///     Step 9 in TC-ID: 15.1.1 in 20.1.1 Mode Symbols in Sub-Area B7 for SB, SR, FS, TR, PT, SH, NL and SF mode
        ///     Step 6 in TC-ID: 17.9.8 in 22.9.8 Hide PA Function is configured ‘STORED’ with reactivated Cabin A
        ///     Step 12 in TC-ID: 18.4.1 in 23.4.1 Geographical Position: General presentation
        ///     Step 3 in TC-ID: 18.4.3 in 23.4.3 Geographical Position: Additional requirements
        ///     Step 3 in TC-ID: 20.1 in 25.1 Driver’s Action: Main window
        ///     Step 1 in TC-ID: 26.1 in 1 Introduction
        ///     Step 11 in TC-ID: 5.3 in 10.3 Screen Layout: Frames
        ///     Step 13 in TC-ID: 10.2.6 in 15.2.6 State 'ST05': Settings window and windows in setting menu
        ///     Step 2 in TC-ID: 13.1.1 in 18.1.1 Distance to Target  Bar: General Appearance
        ///     Step 2 in TC-ID: 13.1.4 in 18.1.4 Distance to Target Digital when the communication between ETCS  Onboard and DMI is lost
        ///     Step 2 in TC-ID: 13.1.5 in 18.1.5 Distance to Target in RV mode
        ///     Step 2 in TC-ID: 17.1.1 in 22.1.1 Planning Area: General Appearance
        ///     Step 2 in TC-ID: 17.1.2 in 22.1.2 Planning Area is suppressed in Level 1 and OS mode
        ///     Step 2 in TC-ID: 17.2.1 in 22.2.1 Planning Area-Layering: PASP and PA Distance scale
        ///     Step 1 in TC-ID: 17.3 in 22.3 Planning Area: PA Distance Scale
        ///     Step 1 in TC-ID: 17.5.1 in 22.5.1 PA Gradient Profile:  General appearance
        ///     Step 1 in TC-ID: 17.7.2 in 22.7.2 PA Speed Profile (PASP): Information updating
        ///     Step 2 in TC-ID: 17.9.1 in 22.9.1 Hide PA Function: General appearance
        ///     Step 1 in TC-ID: 17.9.2 in 22.9.2 Hide PA Function is configured ‘ON’ with reboot DMI
        ///     Step 2 in TC-ID: 17.9.5 in 22.9.6 Hide PA Function is configured ‘TIMER’ with reboot DMI
        ///     Step 1 in TC-ID: 17.9.10 (Default Configuration) in 22.9.10 Hide PA Function with the communication loss between ETCS Onboard and DMI
        ///     Step 2 in TC-ID: 17.10.2 in 22.10.2 Zoom PA Function with Scale Up
        ///     Step 2 in TC-ID: 17.10.3 in 22.10.3 Zoom PA Function with Scale Down
        ///     Step 2 in TC-ID: 17.10.4 in 22.10.4 Zoom PA Function with the communication loss between ETCS Onboard and DMI
        ///     Step 1 in TC-ID: 18.4.1 in 23.4.1 Geographical Position: General presentation
        ///     Step 2 in TC-ID: 12.7.1 in 17.7.1 Release Speed: At Sub-area B2 and B6
        ///     Step 2 in TC-ID: 17.11 in 22.11 Handle at least 31 PA Speed Profile Segments
        ///     Step 2 in TC-ID: 17.12 in Handle at least 31 PA Gradient Profile Segments
        ///     Step 2 in TC-ID: 17.4.17 in 22.4.17 PA Track Condition: First symbol prevails over the next coming symbol
        ///     Step 2 in TC-ID: 29.1 in 29.1 UTC time and offset time(by driver)
        ///     Step 2 in TC-ID: 29.2 in 29.2 UTC time and offset time(by using EVC-3)
        ///     Step 1 in TC-ID: 17.5.2 in 22.5.2 PA Gradient Profile:  Display of many PA Gradient Profile
        ///     Step 1 in TC-ID: 17.5.3 in 22.5.3 PA Gradient Profile:  Information updating
        ///     Step 1 in TC-ID: 17.5.4 in 22.5.4 PA Gradient Profile:  Invalid Information Ignoring
        ///     Step 2 in TC-ID: 17.9.3 in 22.9.3 Hide PA Function is configured ‘OFF’ with reboot DMI
        ///     Step 2 in TC-ID: 17.9.4 in 22.9.4 Hide PA Function is configured ‘STORED’ with reboot DMI
        ///     Step 2 in TC-ID: 17.9.6 in 22.9.5 Hide PA Function is configured ‘ON’ with reactivated Cabin A
        ///     Step 2 in TC-ID: 17.9.7 in 22.9.7 Hide PA Function is configured ‘OFF’ with reactivated Cabin A
        ///     Step 6 in TC-ID: 17.9.7 in 22.9.7 Hide PA Function is configured ‘OFF’ with reactivated Cabin A
        ///     Step 2 in TC-ID: 17.9.9 in 22.9.9 Hide PA Function is configured ‘TIMER’ with reactivated Cabin A
        ///     Step 6 in TC-ID: 17.9.9 in 22.9.9 Hide PA Function is configured ‘TIMER’ with reactivated Cabin A
        ///     Step 10 in TC-ID: 17.9.11 in 22.9.11 Hide PA Function configured ‘STORED’ with re-activate cabin
        ///     Step 16 in TC-ID: 17.9.11 in 22.9.11 Hide PA Function configured ‘STORED’ with re-activate cabin
        /// </summary>
        public static void SR_Mode_displayed(SignalPool pool)
        {
            EVC102_MMIStatusReport.Check_MMI_M_MODE_READBACK =
                EVC102_MMIStatusReport.MMI_M_MODE_READBACK.StaffResponsible;
            Driver_symbol_displayed(pool, "Staff Responsible mode", "MO9", "B7", false);
        }

        /// <summary>
        /// Description: FS mode is displayed
        /// Used in:
        ///     Step 3 in TC-ID: 13.1.1 in 18.1.1 Distance to Target  Bar: General Appearance
        ///     Step 3 in TC-ID: 13.1.5 in 18.1.5 Distance to Target in RV mode
        ///     Step 1 in TC-ID: 12.12 in 17.12 Slip Indication
        ///     Step 1 in TC-ID: 12.13 in 17.13 Slide Indication
        ///     Step 1 in TC-ID: 12.14 in 17.14 Slip and Slide are configure to 1 at the same time
        ///     Step 1 in TC-ID: 12.15 in 17.15 Slip and Slide are configure to 0 at the same time
        ///     Step 2 in TC-ID: 17.3 in 22.3 Planning Area: PA Distance Scale
        ///     Step 5 in TC-ID: 15.1.1 in 20.1.1
        ///     Step 2 in TC-ID: 15.1.2 in 20.1.2
        /// </summary>
        public static void FS_mode_displayed(SignalPool pool)
        {
            EVC102_MMIStatusReport.Check_MMI_M_MODE_READBACK =
                EVC102_MMIStatusReport.MMI_M_MODE_READBACK.FullSupervision;
            Driver_symbol_displayed(pool, "FS mode", "MO11", "B7", false);
        }

        /// <summary>
        /// Description: Os mode is displayed
        /// Used in:
        ///     Step 12 in TC-ID: 15.1.3 in 20.1.3
        /// </summary>
        public static void OS_Mode_displayed(SignalPool pool)
        {
            EVC102_MMIStatusReport.Check_MMI_M_MODE_READBACK = EVC102_MMIStatusReport.MMI_M_MODE_READBACK.OnSight;
            Driver_symbol_displayed(pool, "OS mode", "MO07", "B7", false);
        }

        /// <summary>
        /// Description: Driver enters and confirms Fixed Train Data
        /// Used in:
        ///     Step 5 in 15.1.3
        /// </summary>
        /// <param name="pool">The SignalPool</param>
        /// <param name="trainsetSelected">Number of the fixed trainset selected</param>
        public static void Fixed_Train_Data_entered(SignalPool pool, Fixed_Trainset_Captions trainsetSelected)
        {
            DmiActions.ShowInstruction(pool, "Perform the following action after pressing OK:" + Environment.NewLine +
                                             Environment.NewLine +
                                             $"1. Select and enter \"{trainsetSelected.ToString()}\".");

            EVC107_MMINewTrainData.MMI_M_BUTTONS = MMI_M_BUTTONS_TRAIN_DATA.BTN_ENTER;
            EVC107_MMINewTrainData.TrainsetSelected = trainsetSelected;
        }

        /// <summary>
        /// Description: Driver validates Fixed Train Data
        /// Used in:
        ///     Step 5 in 15.1.3
        /// </summary>
        /// <param name="pool">The SignalPool</param>
        /// <param name="trainsetSelected">Number of the fixed trainset selected</param>
        public static void Fixed_Train_Data_validated(SignalPool pool, Fixed_Trainset_Captions trainsetSelected)
        {
            DmiActions.ShowInstruction(pool, "Perform the following action after pressing OK:" + Environment.NewLine +
                                             Environment.NewLine +
                                             "1. Press \"Yes\".");

            EVC107_MMINewTrainData.MMI_M_BUTTONS = MMI_M_BUTTONS_TRAIN_DATA.BTN_YES_DATA_ENTRY_COMPLETE;
            EVC107_MMINewTrainData.TrainsetSelected = trainsetSelected;
        }

        /// <summary>
        /// Description: Driver completes Train Data validation
        /// Used in:
        ///     Step 6 in 15.1.3
        /// </summary>
        /// <param name="pool">The SignalPool</param>
        public static void Train_Data_validation_completed(SignalPool pool)
        {
            DmiActions.ShowInstruction(pool, "Perform the following action after pressing OK: " + Environment.NewLine +
                                             Environment.NewLine +
                                             "1. Press ‘Yes’ button." + Environment.NewLine +
                                             "2. Confirmed the selected value by pressing the input field.");
            EVC110_MMIConfimedTrainData.CheckConfirmedTrainData();
            EVC152_MMIDriverAction.Check_MMI_M_DRIVER_ACTION =
                EVC152_MMIDriverAction.MMI_M_DRIVER_ACTION.TrainDataValidation;
        }

        /// <summary>
        /// Description: Driver enters and confirms Fixed Train Data
        /// Used in:
        ///     Step 5 in 15.1.3
        /// </summary>
        /// <param name="pool">The SignalPool</param>
        /// <param name="trainsetSelected">Number of the fixed trainset selected</param>
        public static void SR_speed_distance_entered(SignalPool pool, uint lStff, ushort vStff)
        {
            DmiActions.ShowInstruction(pool,
                $"Enter and confirm the following data, SR speed = {vStff}, SR distance = {lStff}");

            EVC106_MMINewSrRules.MMI_L_STFF = lStff;
            EVC106_MMINewSrRules.MMI_V_STFF = vStff;
            EVC106_MMINewSrRules.MMI_M_BUTTONS = MMI_M_BUTTONS_SR_RULES.BTN_ENTER;
        }

        /// <summary>
        /// Description: DMI displays Special window with enabled Adhesion button
        /// Used in:
        ///     Step 8 in TC-ID: 10.2.5 in 15.2.5 State 'ST05': Special window and windows in the special menu
        ///     Step 3 in TC-ID: 20.3 in 25.3 Driver’s Action: Special window
        /// </summary>
        public static void DMI_displays_Special_window_with_enabled_Adhesion_button(SignalPool pool)
        {
            pool.WaitForVerification("Is the Special window displayed with the Adhesion button enabled?");
        }

        /// <summary>
        /// Description: DMI displays SB mode
        /// Used in:
        ///     Step 1 in TC-ID: 12.2.2 in 17.2.2 Speed Dial: Display Train maxinum speed
        ///     Step 1 in TC-ID: 15.1.1
        ///     Step 1 in TC-ID: 29.1 in 29.1 UTC time and offset time(by driver)
        ///     Step 1 in TC-ID: 29.2 in 29.2 UTC time and offset time(by using EVC-3)
        ///     Step 1 in TC-ID: 29.3 in 29.3 UTC time and offset time(By VAP acting as NTP server)
        ///     Step 6 in TC-ID: 5.10 in 10.10 Screen Layout: Button States
        ///     Step 8 in TC-ID: 5.10 in 10.10 Screen Layout: Button States
        ///     Step 11 in TC-ID: 5.10 in 10.10 Screen Layout: Button States
        ///     Step 17 in TC-ID: 5.10 in 10.10 Screen Layout: Button States
        ///     Step 19 in TC-ID: 5.10 in 10.10 Screen Layout: Button States
        ///     Step 1 in TC-ID: 1.8 in 6.8 Accleration/Decleration interval -4.0m/s2 to +4.0 m/s2
        ///     Step 21 in TC-ID: 5.10 in 10.10 Screen Layout: Button States
        ///     Step 1 in TC-ID: 17.4.17 in 22.4.17 PA Track Condition: First symbol prevails over the next coming symbol
        /// </summary>
        public static void SB_Mode_displayed(SignalPool pool)
        {
            EVC102_MMIStatusReport.Check_MMI_M_MODE_READBACK = EVC102_MMIStatusReport.MMI_M_MODE_READBACK.StandBy;
            Driver_symbol_displayed(pool, "Stand By mode", "MO13", "B7", false);
        }


        /// <summary>
        /// Description: DMI displays Level window
        /// Used in:
        ///     Step 2 in TC-ID: 15.1.3 in 20.1.3 Mode Symbols in Sub-Area B7 for OS, UN mode
        ///     Step 3 in TC-ID: 34.1.4 in 37.1.4.1.1 Data entry/validation process when enabling conditions not fullfilled: Level 1
        /// </summary>
        public static void Level_window_displayed(SignalPool pool)
        {
            pool.WaitForVerification("Is the Level window displayed?");
        }

        /// <summary>
        /// Description: DMI displays Settings window
        /// Used in:
        ///     Step 1 in TC-ID: 1.1 in 6.1 Properties of each Display Unit’s Screen
        ///     Step 4 in TC-ID: 1.1 in 6.1 Properties of each Display Unit’s Screen
        ///     Step 6 in TC-ID: 22.21 in 27.21 Settings Window
        ///     Step 11 in TC-ID: 22.21 in 27.21 Settings Window
        ///     Step 8 in TC-ID: 22.26 in 27.26 System info window
        ///     Step 3 in TC-ID: 34.7 in 37.7 Dialogue Sequence of Settings window
        ///     Step 5 in TC-ID: 34.7 in 37.7 Dialogue Sequence of Settings window
        ///     Step 9 in TC-ID: 34.7 in 37.7 Dialogue Sequence of Settings window
        ///     Step 11 in TC-ID: 34.7 in 37.7 Dialogue Sequence of Settings window
        ///     Step 13 in TC-ID: 34.7 in 37.7 Dialogue Sequence of Settings window
        ///     Step 15 in TC-ID: 34.7 in 37.7 Dialogue Sequence of Settings window
        ///     Step 17 in TC-ID: 34.7 in 37.7 Dialogue Sequence of Settings window
        ///     Step 19 in TC-ID: 34.7 in 37.7 Dialogue Sequence of Settings window
        ///     Step 21 in TC-ID: 34.7 in 37.7 Dialogue Sequence of Settings window
        ///     Step 23 in TC-ID: 34.7 in 37.7 Dialogue Sequence of Settings window
        ///     Step 25 in TC-ID: 34.7 in 37.7 Dialogue Sequence of Settings window
        ///     Step 27 in TC-ID: 34.7 in 37.7 Dialogue Sequence of Settings window
        ///     Step 29 in TC-ID: 34.7 in 37.7 Dialogue Sequence of Settings window
        ///     Step 33 in TC-ID: 34.7 in 37.7 Dialogue Sequence of Settings window
        ///     Step 35 in TC-ID: 34.7 in 37.7 Dialogue Sequence of Settings window
        ///     Step 39 in TC-ID: 34.7 in 37.7 Dialogue Sequence of Settings window
        ///     Step 41 in TC-ID: 34.7 in 37.7 Dialogue Sequence of Settings window
        ///     Step 43 in TC-ID: 34.7 in 37.7 Dialogue Sequence of Settings window
        ///     Step 47 in TC-ID: 34.7 in 37.7 Dialogue Sequence of Settings window
        ///     Step 49 in TC-ID: 34.7 in 37.7 Dialogue Sequence of Settings window
        ///     Step 51 in TC-ID: 34.7 in 37.7 Dialogue Sequence of Settings window
        /// </summary>
        public static void DMI_displays_Settings_window(SignalPool pool)
        {
            pool.WaitForVerification("Is the Settings window displayed?");
        }

        /// <summary>
        /// Description: DMI displays Special window
        /// Used in:
        ///     Step 2 in TC-ID: 14.6 in 19.6 Toggling function: Additional Configuration ‘TIMER’
        ///     Step 8 in TC-ID: 22.10 in 27.10 Special window
        ///     Step 16 in TC-ID: 22.10 in 27.10 Special window
        ///     Step 7 in TC-ID: 34.6 in 37.6 Dialogue Sequence of Special window
        /// </summary>
        public static void DMI_displays_Special_window(SignalPool pool)
        {
            pool.WaitForVerification("Is the Special window displayed?");
        }

        /// <summary>
        /// Description: DMI displays SR speed/distance window
        /// Used in:
        ///     Step 1 in TC-ID: 10.4.1.3 in 15.4.1.3 State ‘ST05’: Abort the pending Data Process in Special window
        ///     Step 24 in TC-ID: 22.9.1 in 27.9.1 SR Speed/Distance window: General appearance
        /// </summary>
        public static void DMI_displays_SR_speed_distance_window(SignalPool pool)
        {
            pool.WaitForVerification("Is the SR Speed/Distance window displayed?");
        }

        /// <summary>
        /// Description: DMI displays in SB mode, level 1
        /// Used in:
        ///     Step 2 in TC-ID: 1.2 in 6.2 Internal Components
        ///     Step 8 in TC-ID: 1.2 in 6.2 Internal Components
        ///     Step 3 in TC-ID: 15.2.3 in 20.2.3 ETCS Level :Announcement symbol in Sub-Area C1.
        /// </summary>
        public static void DMI_displays_in_SB_mode_level_1(SignalPool pool)
        {
            Driver_symbol_displayed(pool, "Level 1", "LE03", "C8", true);
            SB_Mode_displayed(pool);
        }

        public static void Level_1_displayed(SignalPool pool)
        {
            Driver_symbol_displayed(pool, "Level 1", "LE03", "C8", true);
        }

        public static void UTC_time_changed(SignalPool pool)
        {
            DmiActions.ShowInstruction(pool,
                @"Enter the new value of Offset time. Then presses ‘Yes’ and closes the window");
            //TODO
            // Need to implement a flexible way to check for the time entered by the user.
            // Use the below method with some clever trick.
                //  EVC109_MMISetTimeMMI.Check_MMI_Set_Time(DateTime.Now,1);
        }

        /// <summary>
        /// Description: ETCS OB enters SR mode in Level 1
        /// Used in:
        ///     Step 1 in TC-ID: 1.11 in 6.11 Response Time with Up-Type Button
        ///     Step 1 in TC-ID: 21.1.1 in Sound S1 - Over Speed
        ///     Step 1 in TC-ID: 36.1 in Sound S1 - Driving too fast
        ///     Step 8 in TC-ID: 36.1 in Sound S1 - Driving too fast
        ///     Step 1 in TC-ID: 36.2 in Sound S3 - End of Intervention
        ///     Step 7 in TC-ID: 36.2 in Sound S3 - End of Intervention
        ///     Step 1 in TC-ID: 36.3.1 in 39.3.1 Restrictive Target with Speed Monitoring in Full Supervision Mode
        ///     Step 1 in TC-ID: 36.3.2 in 39.3.2 Restrictive Target with Movement Authority Changed in Full Supervision Mode
        ///     Step 1 in TC-ID: 36.3.3 in 39.3.3 Restrictive Target with Speed Monitoring in Limited Supervision Mode
        ///     Step 1 in TC-ID: 36.3.4 in 39.3.4 Restrictive Target with Movement Authority Changed in Limited Supervision Mode
        /// </summary>
        public static void ETCS_OB_enters_SR_mode_in_Level_1(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: DMI displays the default window
        /// Used in:
        ///     Step 11 in TC-ID: 3.1 in 8.1 DMI language selection: Configurable at least eight langauges
        ///     Step 10 in TC-ID: 5.8 in 10.8 Screen Layout: Windows
        ///     Step 15 in TC-ID: 5.8 in 10.8 Screen Layout: Windows
        ///     Step 1 in TC-ID: 17.9.4 in 22.9.4 Hide PA Function is configured ‘STORED’ with reboot DMI
        ///     Step 1 in TC-ID: 17.9.5 in 22.9.6 Hide PA Function is configured ‘TIMER’ with reboot DMI
        ///     Step 3 in TC-ID: 22.1.2 in 27.1.2 ETCS Specfic submenus and SN sub menus
        ///     Step 5 in TC-ID: 22.1.2 in 27.1.2 ETCS Specfic submenus and SN sub menus
        ///     Step 7 in TC-ID: 22.1.2 in 27.1.2 ETCS Specfic submenus and SN sub menus
        ///     Step 9 in TC-ID: 22.1.2 in 27.1.2 ETCS Specfic submenus and SN sub menus
        ///     Step 13 in TC-ID: 22.1.2 in 27.1.2 ETCS Specfic submenus and SN sub menus
        ///     Step 4 in TC-ID: 10.2 in 15.2.1 State 'ST05': General Appearance
        ///     Step 4 in TC-ID: 15.1.3 in 20.1.3 Mode Symbols in Sub-Area B7 for OS, UN mode
        ///     Step 31 in TC-ID: 22.29.1 in 27.29.1 Flexible Train data window: General appearances
        ///     Step 10 in TC-ID: 22.29.2 in 27.29.2 Fixed Train data window: General appearances
        ///     Step 2 in TC-ID: 34.1.1 in 37.1.1 Fixed Train data entry
        ///     Step 5 in TC-ID: 34.1.4 in 37.1.4.1.1 Data entry/validation process when enabling conditions not fullfilled: Level 1
        /// </summary>
        public static void DMI_displays_the_default_window(SignalPool pool)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Description: DMI displays Main window
        /// Used in:
        ///     Step 17 in TC-ID: 5.12.2 in 10.12.2 Close, Next, Previous and Yes Buttons
        ///     Step 7 in TC-ID: 15.1.3 in 20.1.3 Mode Symbols in Sub-Area B7 for OS, UN mode
        ///     Step 2 in TC-ID: 20.5 in 25.5 Driver’s Action: RBC Contact windows
        ///     Step 3 in TC-ID: 22.1.1 in 27.1.1 Sub-Level Window: General appearances
        ///     Step 15 in TC-ID: 22.8.3.1 in 27.8.3.1 RBC Contact window: General appearance
        ///     Step 14 in TC-ID: 22.18 in Train Running Number window
        ///     Step 2 in TC-ID: 22.20.1 in 27.20.1 Override window: General appearance
        ///     Step 2 in TC-ID: 22.20.2 in 27.20.2 Override window in SB mode
        ///     Step 19 in TC-ID: 22.29.2 in 27.29.2 Fixed Train data window: General appearances
        ///     Step 5 in TC-ID: 33.3 in 36.2 The relationship between parent and child windows (2)
        ///     Step 7 in TC-ID: 33.3 in 36.2 The relationship between parent and child windows (2)
        ///     Step 1 in TC-ID: 34.1.1 in 37.1.1 Fixed Train data entry
        ///     Step 6 in TC-ID: 34.1.1 in 37.1.1 Fixed Train data entry
        ///     Step 8 in TC-ID: 34.1.1 in 37.1.1 Fixed Train data entry
        ///     Step 1 in TC-ID: 34.1.2 in 37.1.2 Flexible Train data entry
        ///     Step 8 in TC-ID: 34.1.2 in 37.1.2 Flexible Train data entry
        ///     Step 10 in TC-ID: 34.1.2 in 37.1.2 Flexible Train data entry
        ///     Step 11 in TC-ID: 34.1.4 in 37.1.4.1.1 Data entry/validation process when enabling conditions not fullfilled: Level 1
        /// </summary>
        public static void DMI_displays_Main_window(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: The border of the button is shown (state ‘Enabled’) without a sound
        /// Used in:
        ///     Step 4 in TC-ID: 6.1 in 11.1 Acknowledgements: General
        ///     Step 3 in TC-ID: 6.4 in Acknowledgements: NACK/ACK Buttons
        ///     Step 3 in TC-ID: 7.1 in 27.2 Main window
        ///     Step 11 in TC-ID: 22.6.2 in 27.6.2 Maintenance window
        ///     Step 4 in TC-ID: 22.8.3.1 in 27.8.3.1 RBC Contact window: General appearance
        ///     Step 10 in TC-ID: 22.10 in 27.10 Special window
        ///     Step 6 in TC-ID: 22.20.1 in 27.20.1 Override window: General appearance
        ///     Step 3 in TC-ID: 22.21 in 27.21 Settings Window
        ///     Step 13 in TC-ID: 22.22.1 in 27.22.1 Brake window
        ///     Step 3 in TC-ID: 22.22.2  in 27.22.2 Brake test window
        /// </summary>
        public static void The_border_of_the_button_is_shown_state_Enabled_without_a_sound(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: The button is back to state ‘Pressed’ without a sound
        /// Used in:
        ///     Step 5 in TC-ID: 6.1 in 11.1 Acknowledgements: General
        ///     Step 4 in TC-ID: 6.4 in Acknowledgements: NACK/ACK Buttons
        ///     Step 4 in TC-ID: 7.1 in 27.2 Main window
        ///     Step 12 in TC-ID: 22.6.2 in 27.6.2 Maintenance window
        ///     Step 5 in TC-ID: 22.8.3.1 in 27.8.3.1 RBC Contact window: General appearance
        ///     Step 11 in TC-ID: 22.10 in 27.10 Special window
        ///     Step 7 in TC-ID: 22.20.1 in 27.20.1 Override window: General appearance
        ///     Step 4 in TC-ID: 22.21 in 27.21 Settings Window
        ///     Step 14 in TC-ID: 22.22.1 in 27.22.1 Brake window
        ///     Step 4 in TC-ID: 22.22.2  in 27.22.2 Brake test window
        /// </summary>
        public static void The_button_is_back_to_state_Pressed_without_a_sound(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: DMI displays Default window with the  message “ATP Down Alarm” and sound alarm
        /// Used in:
        ///     Step 21 in TC-ID: 6.3 in 11.3 Acknowledgements: Priority of new incoming acknowledgements
        ///     Step 9 in TC-ID: 9.1 in Data Validation Window for Flexible train data entry window
        ///     Step 9 in TC-ID: 9.2 in 14.2 Data Validation Window for Fixed train data entry window
        ///     Step 12 in TC-ID: 22.6.4.1 in 27.6.4.1
        ///     Step 12 in TC-ID: 22.6.6.1 in 27.6.6.1
        ///     Step 9 in TC-ID: 22.22.4  in 27.22.4 Brake percentage validation window
        ///     Step 12 in TC-ID: 22.28.2 in 27.28.2 ‘Remove VBC’ Validation Window
        /// </summary>
        public static void DMI_displays_Default_window_with_the_message_ATP_Down_Alarm_and_sound_alarm(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: The value of input field is changed refer to selected button
        /// Used in:
        ///     Step 2 in TC-ID: 9.1 in Data Validation Window for Flexible train data entry window
        ///     Step 7 in TC-ID: 9.1 in Data Validation Window for Flexible train data entry window
        ///     Step 2 in TC-ID: 9.2 in 14.2 Data Validation Window for Fixed train data entry window
        ///     Step 7 in TC-ID: 9.2 in 14.2 Data Validation Window for Fixed train data entry window
        ///     Step 2 in TC-ID: 22.6.4.1 in 27.6.4.1
        ///     Step 10 in TC-ID: 22.6.4.1 in 27.6.4.1
        ///     Step 2 in TC-ID: 22.6.6.1 in 27.6.6.1
        ///     Step 10 in TC-ID: 22.6.6.1 in 27.6.6.1
        ///     Step 2 in TC-ID: 22.22.4  in 27.22.4 Brake percentage validation window
        ///     Step 7 in TC-ID: 22.22.4  in 27.22.4 Brake percentage validation window
        ///     Step 2 in TC-ID: 22.27.2 in 27.27.2 ‘Set VBC’ Validation Window
        ///     Step 10 in TC-ID: 22.27.2 in 27.27.2 ‘Set VBC’ Validation Window
        ///     Step 2 in TC-ID: 22.28.2 in 27.28.2 ‘Remove VBC’ Validation Window
        ///     Step 7 in TC-ID: 22.28.2 in 27.28.2 ‘Remove VBC’ Validation Window
        /// </summary>
        public static void The_value_of_input_field_is_changed_refer_to_selected_button(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Verify the following information,DMI displays Main window.Use the log file to confirm that DMI sends out the packet [MMI_DRIVER_REQUEST (EVC-101)] with variable [MMI_DRIVER_REQUEST (EVC-101).MMI_M_REQUEST] = 4 (Exit Train data)
        /// Used in:
        ///     Step 5 in TC-ID: 9.1 in Data Validation Window for Flexible train data entry window
        ///     Step 5 in TC-ID: 9.2 in 14.2 Data Validation Window for Fixed train data entry window
        /// </summary>
        public static void
            Verify_the_following_information_DMI_displays_Main_window_Use_the_log_file_to_confirm_that_DMI_sends_out_the_packet_MMI_DRIVER_REQUEST_EVC_101_with_variable_MMI_DRIVER_REQUEST_EVC_101_MMI_M_REQUEST_4_Exit_Train_data(
                SignalPool pool)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Description: The train is at standstill
        /// Used in:
        ///     Step 2 in TC-ID: 12.1 in 17.1 Display of Speed Pointer and Speed Digital
        ///     Step 10 in TC-ID: 17.10.1 in 22.10.1 Zoom PA Function: General appearance
        ///     Step 5 in TC-ID: 21.1.1 in Sound S1 - Over Speed
        ///     Step 7 in TC-ID: 26.1 in 1 Introduction
        ///     Step 5 in TC-ID: 36.1 in Sound S1 - Driving too fast
        ///     Step 12 in TC-ID: 36.1 in Sound S1 - Driving too fast
        ///     Step 5 in TC-ID: 36.2 in Sound S3 - End of Intervention
        ///     Step 11 in TC-ID: 36.2 in Sound S3 - End of Intervention
        ///     Step 7 in TC-ID: 36.3.1 in 39.3.1 Restrictive Target with Speed Monitoring in Full Supervision Mode
        ///     Step 5 in TC-ID: 36.3.2 in 39.3.2 Restrictive Target with Movement Authority Changed in Full Supervision Mode
        ///     Step 5 in TC-ID: 36.3.3 in 39.3.3 Restrictive Target with Speed Monitoring in Limited Supervision Mode
        ///     Step 5 in TC-ID: 36.3.4 in 39.3.4 Restrictive Target with Movement Authority Changed in Limited Supervision Mode
        /// </summary>
        public static void The_train_is_at_standstill(SignalPool pool)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Description: The speed pointer is indicated as 20  km/h
        /// Used in:
        ///     Step 1 in TC-ID: 17.4.1 in 22.4.1 PA Track Condition: Non stopping area in Sub-Area D2 and B3
        ///     Step 6 in TC-ID: 17.4.1 in 22.4.1 PA Track Condition: Non stopping area in Sub-Area D2 and B3
        ///     Step 8 in TC-ID: 17.4.1 in 22.4.1 PA Track Condition: Non stopping area in Sub-Area D2 and B3
        ///     Step 1 in TC-ID: 17.4.2 in 22.4.2 PA Track Condition: Sound Horn in Sub-Area D2 and B3
        ///     Step 6 in TC-ID: 17.4.2 in 22.4.2 PA Track Condition: Sound Horn in Sub-Area D2 and B3
        ///     Step 8 in TC-ID: 17.4.2 in 22.4.2 PA Track Condition: Sound Horn in Sub-Area D2 and B3
        ///     Step 1 in TC-ID: 17.4.3 in 22.4.3 PA Track Condition:  Lower Pantograph in Sub-Area D2 and B3
        ///     Step 6 in TC-ID: 17.4.3 in 22.4.3 PA Track Condition:  Lower Pantograph in Sub-Area D2 and B3
        ///     Step 8 in TC-ID: 17.4.3 in 22.4.3 PA Track Condition:  Lower Pantograph in Sub-Area D2 and B3
        ///     Step 11 in TC-ID: 17.4.3 in 22.4.3 PA Track Condition:  Lower Pantograph in Sub-Area D2 and B3
        ///     Step 13 in TC-ID: 17.4.3 in 22.4.3 PA Track Condition:  Lower Pantograph in Sub-Area D2 and B3
        ///     Step 1 in TC-ID: 17.4.4 in 22.4.4 PA Track Condition: Radio Hole in Sub-Area D2 and B3
        ///     Step 6 in TC-ID: 17.4.4 in 22.4.4 PA Track Condition: Radio Hole in Sub-Area D2 and B3
        ///     Step 8 in TC-ID: 17.4.4 in 22.4.4 PA Track Condition: Radio Hole in Sub-Area D2 and B3
        ///     Step 1 in TC-ID: 17.4.5 in 22.4.5 PA Track Condition: Air Tightness in Sub-Area D2 and B3
        ///     Step 6 in TC-ID: 17.4.5 in 22.4.5 PA Track Condition: Air Tightness in Sub-Area D2 and B3
        ///     Step 8 in TC-ID: 17.4.5 in 22.4.5 PA Track Condition: Air Tightness in Sub-Area D2 and B3
        ///     Step 13 in TC-ID: 17.4.5 in 22.4.5 PA Track Condition: Air Tightness in Sub-Area D2 and B3
        ///     Step 1 in TC-ID: 17.4.6 in 22.4.6 PA Track Condition: Switch off regenerative brake in Sub-Area D2 and B3
        ///     Step 6 in TC-ID: 17.4.6 in 22.4.6 PA Track Condition: Switch off regenerative brake in Sub-Area D2 and B3
        ///     Step 1 in TC-ID: 17.4.7 in PA Track Condition: Switch off eddy current brake in Sub-Area D2 and B3
        ///     Step 6 in TC-ID: 17.4.7 in PA Track Condition: Switch off eddy current brake in Sub-Area D2 and B3
        ///     Step 1 in TC-ID: 17.4.8 in 22.4.8 PA Track Condition: Switch off magnetic shoe brake in Sub-Area D2 and B3
        ///     Step 6 in TC-ID: 17.4.8 in 22.4.8 PA Track Condition: Switch off magnetic shoe brake in Sub-Area D2 and B3
        ///     Step 8 in TC-ID: 17.4.8 in 22.4.8 PA Track Condition: Switch off magnetic shoe brake in Sub-Area D2 and B3
        ///     Step 1 in TC-ID: 17.4.9 in 22.4.9 PA Track Condition: Switch off main power switch in Sub-Area D2 and B3
        ///     Step 6 in TC-ID: 17.4.9 in 22.4.9 PA Track Condition: Switch off main power switch in Sub-Area D2 and B3
        ///     Step 9 in TC-ID: 17.4.9 in 22.4.9 PA Track Condition: Switch off main power switch in Sub-Area D2 and B3
        ///     Step 11 in TC-ID: 17.4.9 in 22.4.9 PA Track Condition: Switch off main power switch in Sub-Area D2 and B3
        ///     Step 1 in TC-ID: 17.4.10 in 22.4.10 PA Track Condition: Change of traction system, not fitted Sub-Area D2 and B3
        ///     Step 6 in TC-ID: 17.4.10 in 22.4.10 PA Track Condition: Change of traction system, not fitted Sub-Area D2 and B3
        ///     Step 8 in TC-ID: 17.4.10 in 22.4.10 PA Track Condition: Change of traction system, not fitted Sub-Area D2 and B3
        ///     Step 1 in TC-ID: 17.4.11 in 22.4.11 PA Track Condition: Change of traction system, AC 25 KV 50 Hz Sub-Area D2 and B3
        ///     Step 6 in TC-ID: 17.4.11 in 22.4.11 PA Track Condition: Change of traction system, AC 25 KV 50 Hz Sub-Area D2 and B3
        ///     Step 8 in TC-ID: 17.4.11 in 22.4.11 PA Track Condition: Change of traction system, AC 25 KV 50 Hz Sub-Area D2 and B3
        ///     Step 1 in TC-ID: 17.4.12 in 22.4.12 PA Track Condition: Change of traction system, AC 15 KV 16.7 Hz Sub-Area D2 and B3
        ///     Step 6 in TC-ID: 17.4.12 in 22.4.12 PA Track Condition: Change of traction system, AC 15 KV 16.7 Hz Sub-Area D2 and B3
        ///     Step 8 in TC-ID: 17.4.12 in 22.4.12 PA Track Condition: Change of traction system, AC 15 KV 16.7 Hz Sub-Area D2 and B3
        ///     Step 1 in TC-ID: 17.4.13 in 22.4.13 PA Track Condition: Change of traction system, DC 3kV Sub-Area D2 and B3
        ///     Step 6 in TC-ID: 17.4.13 in 22.4.13 PA Track Condition: Change of traction system, DC 3kV Sub-Area D2 and B3
        ///     Step 8 in TC-ID: 17.4.13 in 22.4.13 PA Track Condition: Change of traction system, DC 3kV Sub-Area D2 and B3
        ///     Step 1 in TC-ID: 17.4.14 in 22.4.14 PA Track Condition: Change of traction system, DC 1.5 kV Sub-Area D2 and B3
        ///     Step 6 in TC-ID: 17.4.14 in 22.4.14 PA Track Condition: Change of traction system, DC 1.5 kV Sub-Area D2 and B3
        ///     Step 8 in TC-ID: 17.4.14 in 22.4.14 PA Track Condition: Change of traction system, DC 1.5 kV Sub-Area D2 and B3
        ///     Step 1 in TC-ID: 17.4.15 in 22.4.15 PA Track Condition: Change of traction system, DC 600/750 V Sub-Area D2 and B3
        ///     Step 6 in TC-ID: 17.4.15 in 22.4.15 PA Track Condition: Change of traction system, DC 600/750 V Sub-Area D2 and B3
        ///     Step 8 in TC-ID: 17.4.15 in 22.4.15 PA Track Condition: Change of traction system, DC 600/750 V Sub-Area D2 and B3
        ///     Step 1 in TC-ID: 17.4.16 in 22.4.16 PA Track Condition: 30 PA Track Conditions
        ///     Step 3 in TC-ID: 17.4.17 in 22.4.17 PA Track Condition: First symbol prevails over the next coming symbol
        /// </summary>
        public static void The_speed_pointer_is_indicated_as_20_kmh(SignalPool pool)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Description: Mode changes to FS mode , L1
        /// Used in:
        ///     Step 2 in TC-ID: 17.4.2 in 22.4.2 PA Track Condition: Sound Horn in Sub-Area D2 and B3
        ///     Step 2 in TC-ID: 17.4.3 in 22.4.3 PA Track Condition:  Lower Pantograph in Sub-Area D2 and B3
        ///     Step 2 in TC-ID: 17.4.4 in 22.4.4 PA Track Condition: Radio Hole in Sub-Area D2 and B3
        ///     Step 2 in TC-ID: 17.4.5 in 22.4.5 PA Track Condition: Air Tightness in Sub-Area D2 and B3
        ///     Step 2 in TC-ID: 17.4.6 in 22.4.6 PA Track Condition: Switch off regenerative brake in Sub-Area D2 and B3
        ///     Step 2 in TC-ID: 17.4.7 in PA Track Condition: Switch off eddy current brake in Sub-Area D2 and B3
        ///     Step 2 in TC-ID: 17.4.8 in 22.4.8 PA Track Condition: Switch off magnetic shoe brake in Sub-Area D2 and B3
        ///     Step 2 in TC-ID: 17.4.9 in 22.4.9 PA Track Condition: Switch off main power switch in Sub-Area D2 and B3
        ///     Step 2 in TC-ID: 17.4.10 in 22.4.10 PA Track Condition: Change of traction system, not fitted Sub-Area D2 and B3
        ///     Step 2 in TC-ID: 17.4.11 in 22.4.11 PA Track Condition: Change of traction system, AC 25 KV 50 Hz Sub-Area D2 and B3
        ///     Step 2 in TC-ID: 17.4.12 in 22.4.12 PA Track Condition: Change of traction system, AC 15 KV 16.7 Hz Sub-Area D2 and B3
        ///     Step 2 in TC-ID: 17.4.13 in 22.4.13 PA Track Condition: Change of traction system, DC 3kV Sub-Area D2 and B3
        ///     Step 2 in TC-ID: 17.4.14 in 22.4.14 PA Track Condition: Change of traction system, DC 1.5 kV Sub-Area D2 and B3
        ///     Step 2 in TC-ID: 17.4.15 in 22.4.15 PA Track Condition: Change of traction system, DC 600/750 V Sub-Area D2 and B3
        ///     Step 2 in TC-ID: 17.4.16 in 22.4.16 PA Track Condition: 30 PA Track Conditions
        /// </summary>
        public static void Mode_changes_to_FS_mode_L1(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Verify the following information(1)   Use the log file to confirm that DMI received packet information MMI_TRACK_CONDITIONS (EVC-32) with the following variables,MMI_Q_TRACKCOND_STEP = 4MMI_NID_TRACKCOND = Same value with expected result No.2 of step 7
        /// Used in:
        ///     Step 9 in TC-ID: 17.4.4 in 22.4.4 PA Track Condition: Radio Hole in Sub-Area D2 and B3
        ///     Step 9 in TC-ID: 17.4.8 in 22.4.8 PA Track Condition: Switch off magnetic shoe brake in Sub-Area D2 and B3
        ///     Step 9 in TC-ID: 17.4.6 in 22.4.6 PA Track Condition: Switch off regenerative brake in Sub-Area D2 and B3
        ///     Step 9 in TC-ID: 17.4.7 in PA Track Condition: Switch off eddy current brake in Sub-Area D2 and B3
        ///     Step 9 in TC-ID: 17.4.10 in 22.4.10 PA Track Condition: Change of traction system, not fitted Sub-Area D2 and B3
        ///     Step 9 in TC-ID: 17.4.11 in 22.4.11 PA Track Condition: Change of traction system, AC 25 KV 50 Hz Sub-Area D2 and B3
        ///     Step 9 in TC-ID: 17.4.12 in 22.4.12 PA Track Condition: Change of traction system, AC 15 KV 16.7 Hz Sub-Area D2 and B3
        ///     Step 9 in TC-ID: 17.4.13 in 22.4.13 PA Track Condition: Change of traction system, DC 3kV Sub-Area D2 and B3
        ///     Step 9 in TC-ID: 17.4.14 in 22.4.14 PA Track Condition: Change of traction system, DC 1.5 kV Sub-Area D2 and B3
        ///     Step 9 in TC-ID: 17.4.15 in 22.4.15 PA Track Condition: Change of traction system, DC 600/750 V Sub-Area D2 and B3
        /// </summary>
        public static void
            Verify_the_following_information1_Use_the_log_file_to_confirm_that_DMI_received_packet_information_MMI_TRACK_CONDITIONS_EVC_32_with_the_following_variables_MMI_Q_TRACKCOND_STEP_4MMI_NID_TRACKCOND_Same_value_with_expected_result_No_2_of_step_7(
                SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Mode remians in FS mode
        /// Used in:
        ///     Step 3 in TC-ID: 17.4.6 in 22.4.6 PA Track Condition: Switch off regenerative brake in Sub-Area D2 and B3
        ///     Step 3 in TC-ID: 17.4.7 in PA Track Condition: Switch off eddy current brake in Sub-Area D2 and B3
        ///     Step 3 in TC-ID: 17.4.8 in 22.4.8 PA Track Condition: Switch off magnetic shoe brake in Sub-Area D2 and B3
        ///     Step 3 in TC-ID: 17.4.9 in 22.4.9 PA Track Condition: Switch off main power switch in Sub-Area D2 and B3
        ///     Step 3 in TC-ID: 17.4.10 in 22.4.10 PA Track Condition: Change of traction system, not fitted Sub-Area D2 and B3
        ///     Step 3 in TC-ID: 17.4.11 in 22.4.11 PA Track Condition: Change of traction system, AC 25 KV 50 Hz Sub-Area D2 and B3
        ///     Step 3 in TC-ID: 17.4.12 in 22.4.12 PA Track Condition: Change of traction system, AC 15 KV 16.7 Hz Sub-Area D2 and B3
        ///     Step 3 in TC-ID: 17.4.13 in 22.4.13 PA Track Condition: Change of traction system, DC 3kV Sub-Area D2 and B3
        ///     Step 3 in TC-ID: 17.4.14 in 22.4.14 PA Track Condition: Change of traction system, DC 1.5 kV Sub-Area D2 and B3
        ///     Step 3 in TC-ID: 17.4.15 in 22.4.15 PA Track Condition: Change of traction system, DC 600/750 V Sub-Area D2 and B3
        /// </summary>
        public static void Mode_remians_in_FS_mode(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: The speed pointer is indicated as 40  km/h
        /// Used in:
        ///     Step 8 in TC-ID: 17.4.6 in 22.4.6 PA Track Condition: Switch off regenerative brake in Sub-Area D2 and B3
        ///     Step 8 in TC-ID: 17.4.7 in PA Track Condition: Switch off eddy current brake in Sub-Area D2 and B3
        /// </summary>
        public static void The_speed_pointer_is_indicated_as_40_kmh(SignalPool pool)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Description: The train can drive forward and all brakes are not applied
        /// Used in:
        ///     Step 2 in TC-ID: 21.1.1 in Sound S1 - Over Speed
        ///     Step 2 in TC-ID: 36.1 in Sound S1 - Driving too fast
        ///     Step 9 in TC-ID: 36.1 in Sound S1 - Driving too fast
        ///     Step 2 in TC-ID: 36.2 in Sound S3 - End of Intervention
        ///     Step 8 in TC-ID: 36.2 in Sound S3 - End of Intervention
        ///     Step 2 in TC-ID: 36.3.1 in 39.3.1 Restrictive Target with Speed Monitoring in Full Supervision Mode
        ///     Step 2 in TC-ID: 36.3.2 in 39.3.2 Restrictive Target with Movement Authority Changed in Full Supervision Mode
        ///     Step 2 in TC-ID: 36.3.3 in 39.3.3 Restrictive Target with Speed Monitoring in Limited Supervision Mode
        ///     Step 2 in TC-ID: 36.3.4 in 39.3.4 Restrictive Target with Movement Authority Changed in Limited Supervision Mode
        /// </summary>
        public static void The_train_can_drive_forward_and_all_brakes_are_not_applied(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: ETCS OB enters FS mode in Level 1
        /// Used in:
        ///     Step 3 in TC-ID: 21.1.1 in Sound S1 - Over Speed
        ///     Step 3 in TC-ID: 36.1 in Sound S1 - Driving too fast
        ///     Step 10 in TC-ID: 36.1 in Sound S1 - Driving too fast
        ///     Step 3 in TC-ID: 36.2 in Sound S3 - End of Intervention
        ///     Step 9 in TC-ID: 36.2 in Sound S3 - End of Intervention
        ///     Step 3 in TC-ID: 36.3.1 in 39.3.1 Restrictive Target with Speed Monitoring in Full Supervision Mode
        /// </summary>
        public static void ETCS_OB_enters_FS_mode_in_Level_1(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Sound ‘S1_toofast.wav’ is played once
        /// Used in:
        ///     Step 6 in TC-ID: 21.1.1 in Sound S1 - Over Speed
        ///     Step 6 in TC-ID: 36.1 in Sound S1 - Driving too fast
        ///     Step 13 in TC-ID: 36.1 in Sound S1 - Driving too fast
        /// </summary>
        public static void Sound_S1_toofast_wav_is_played_once(SignalPool pool)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Description: Verify the following information,While press and hold button less than 1.5 secSound ‘Click’ is played once.The state of button is changed to ‘Pressed’ and immediately back to ‘Enabled’ state.The last character is removed from an input field after pressing the button.While press and hold button over 1.5 secThe state ‘pressed’ and ‘released’ are switched repeatly while button is pressed and the characters are removed from an input field repeatly refer to pressed state.The sound ‘Click’ is played repeatly while button is pressed
        /// Used in:
        ///     Step 4 in TC-ID: 22.6.1 in 27.6.1 Password window
        ///     Step 5 in TC-ID: 22.6.3.1 in 27.6.3.1 Wheel diameter window: General apearance
        ///     Step 5 in TC-ID: 22.6.5.1 in 27.6.5.1 Radar window: General appearance
        ///     Step 6 in TC-ID: 22.8.1.1 in 27.8.1.1
        ///     Step 6 in TC-ID: 22.9.1 in 27.9.1 SR Speed/Distance window: General appearance
        ///     Step 5 in TC-ID: 7.3.2 in 27.17.3 Entering Characters
        ///     Step 5 in TC-ID: 22.18 in Train Running Number window
        ///     Step 6 in TC-ID: 22.22.3  in 27.22.3 Brake percentage window
        ///     Step 5 in TC-ID: 22.27.1 in 27.27.1 ‘Set VBC’ Data Entry Window
        ///     Step 5 in TC-ID: 22.28.1 in 27.28.1 ‘Remove VBC’ Data Entry Window
        ///     Step 12 in TC-ID: 22.29.1 in 27.29.1 Flexible Train data window: General appearances
        /// </summary>
        public static void
            Verify_the_following_information_While_press_and_hold_button_less_than_1_5_secSound_Click_is_played_once_The_state_of_button_is_changed_to_Pressed_and_immediately_back_to_Enabled_state_The_last_character_is_removed_from_an_input_field_after_pressing_the_button_While_press_and_hold_button_over_1_5_secThe_state_pressed_and_released_are_switched_repeatly_while_button_is_pressed_and_the_characters_are_removed_from_an_input_field_repeatly_refer_to_pressed_state_The_sound_Click_is_played_repeatly_while_button_is_pressed(
                SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Verify the following information, The character is stop removing
        /// Used in:
        ///     Step 5 in TC-ID: 22.6.1 in 27.6.1 Password window
        ///     Step 6 in TC-ID: 22.6.3.1 in 27.6.3.1 Wheel diameter window: General apearance
        ///     Step 6 in TC-ID: 22.6.5.1 in 27.6.5.1 Radar window: General appearance
        ///     Step 7 in TC-ID: 22.8.1.1 in 27.8.1.1
        ///     Step 7 in TC-ID: 22.9.1 in 27.9.1 SR Speed/Distance window: General appearance
        ///     Step 6 in TC-ID: 7.3.2 in 27.17.3 Entering Characters
        ///     Step 6 in TC-ID: 22.18 in Train Running Number window
        ///     Step 7 in TC-ID: 22.22.3  in 27.22.3 Brake percentage window
        ///     Step 6 in TC-ID: 22.27.1 in 27.27.1 ‘Set VBC’ Data Entry Window
        ///     Step 6 in TC-ID: 22.28.1 in 27.28.1 ‘Remove VBC’ Data Entry Window
        ///     Step 13 in TC-ID: 22.29.1 in 27.29.1 Flexible Train data window: General appearances
        /// </summary>
        public static void Verify_the_following_information_The_character_is_stop_removing(SignalPool pool)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Description: Verify the following information,(1)    The state of an input field is changed to ‘Enabled, the border of button is shown without a sound
        /// Used in:
        ///     Step 11 in TC-ID: 22.6.1 in 27.6.1 Password window
        ///     Step 4 in TC-ID: 22.6.4.1 in 27.6.4.1
        ///     Step 4 in TC-ID: 22.6.6.1 in 27.6.6.1
        ///     Step 8 in TC-ID: 22.8.2.1 in 27.8.2.1 Radio Network ID window: General appearance
        ///     Step 11 in TC-ID: 22.17 in 27.17.1 Driver ID window: General Display
        ///     Step 11 in TC-ID: 22.18 in Train Running Number window
        ///     Step 5 in TC-ID: 22.19 in 27.19 Language Window
        ///     Step 7 in TC-ID: 22.24 in 27.24 Brightness window
        ///     Step 7 in TC-ID: 22.25 in 27.25 Volume window
        ///     Step 13 in TC-ID: 22.22.3  in 27.22.3 Brake percentage window
        ///     Step 4 in TC-ID: 22.27.2 in 27.27.2 ‘Set VBC’ Validation Window
        ///     Step 9 in TC-ID: 22.28.2 in 27.28.2 ‘Remove VBC’ Validation Window
        /// </summary>
        public static void
            Verify_the_following_information_1_The_state_of_an_input_field_is_changed_to_Enabled_the_border_of_button_is_shown_without_a_sound(
                SignalPool pool)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Description: Verify the following information,The state of released button is changed to enabled
        /// Used in:
        ///     Step 3 in TC-ID: 22.6.3.1 in 27.6.3.1 Wheel diameter window: General apearance
        ///     Step 3 in TC-ID: 22.6.5.1 in 27.6.5.1 Radar window: General appearance
        ///     Step 4 in TC-ID: 22.8.1.1 in 27.8.1.1
        ///     Step 4 in TC-ID: 22.9.1 in 27.9.1 SR Speed/Distance window: General appearance
        ///     Step 6 in TC-ID: 22.13.1 in 27.13.1 Set Clock function: General appearance
        ///     Step 3 in TC-ID: 22.27.1 in 27.27.1 ‘Set VBC’ Data Entry Window
        ///     Step 3 in TC-ID: 22.28.1 in 27.28.1 ‘Remove VBC’ Data Entry Window
        ///     Step 8 in TC-ID: 22.29.1 in 27.29.1 Flexible Train data window: General appearances
        /// </summary>
        public static void Verify_the_following_information_The_state_of_released_button_is_changed_to_enabled(
            SignalPool pool)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Description: Verify the following information,The state of button is changed to ‘Pressed’, the border of button is removed.The sound ‘Click’ is played once
        /// Used in:
        ///     Step 14 in TC-ID: 22.6.3.1 in 27.6.3.1 Wheel diameter window: General apearance
        ///     Step 12 in TC-ID: 22.6.5.1 in 27.6.5.1 Radar window: General appearance
        ///     Step 19 in TC-ID: 22.8.1.1 in 27.8.1.1
        ///     Step 18 in TC-ID: 22.9.1 in 27.9.1 SR Speed/Distance window: General appearance
        ///     Step 3 in TC-ID: 22.29.1 in 27.29.1 Flexible Train data window: General appearances
        /// </summary>
        public static void
            Verify_the_following_information_The_state_of_button_is_changed_to_Pressed_the_border_of_button_is_removed_The_sound_Click_is_played_once(
                SignalPool pool)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Description: Verify the following information,The button is back to state ‘Pressed’ without a sound
        /// Used in:
        ///     Step 16 in TC-ID: 22.6.3.1 in 27.6.3.1 Wheel diameter window: General apearance
        ///     Step 14 in TC-ID: 22.6.5.1 in 27.6.5.1 Radar window: General appearance
        ///     Step 21 in TC-ID: 22.8.1.1 in 27.8.1.1
        ///     Step 20 in TC-ID: 22.9.1 in 27.9.1 SR Speed/Distance window: General appearance
        ///     Step 5 in TC-ID: 22.29.1 in 27.29.1 Flexible Train data window: General appearances
        /// </summary>
        public static void Verify_the_following_information_The_button_is_back_to_state_Pressed_without_a_sound(
            SignalPool pool)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Description: Verify the following information,The state of an input field is changed to ‘selected’ when release the pressed area at the Data area of input field
        /// Used in:
        ///     Step 24 in TC-ID: 22.6.5.1 in 27.6.5.1 Radar window: General appearance
        ///     Step 30 in TC-ID: 22.9.1 in 27.9.1 SR Speed/Distance window: General appearance
        ///     Step 37 in TC-ID: 22.29.1 in 27.29.1 Flexible Train data window: General appearances
        /// </summary>
        public static void
            Verify_the_following_information_The_state_of_an_input_field_is_changed_to_selected_when_release_the_pressed_area_at_the_Data_area_of_input_field(
                SignalPool pool)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Description: Verify the following information,The border of the button is shown (state ‘Enabled’) without a sound
        /// Used in:
        ///     Step 20 in TC-ID: 22.8.1.1 in 27.8.1.1
        ///     Step 19 in TC-ID: 22.9.1 in 27.9.1 SR Speed/Distance window: General appearance
        ///     Step 4 in TC-ID: 22.29.1 in 27.29.1 Flexible Train data window: General appearances
        /// </summary>
        public static void
            Verify_the_following_information_The_border_of_the_button_is_shown_state_Enabled_without_a_sound(
                SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Verify the following information,The state of an input field is changed to ‘selected’ when release the pressed area at the Label area of input field
        /// Used in:
        ///     Step 30 in TC-ID: 22.8.1.1 in 27.8.1.1
        ///     Step 29 in TC-ID: 22.9.1 in 27.9.1 SR Speed/Distance window: General appearance
        ///     Step 36 in TC-ID: 22.29.1 in 27.29.1 Flexible Train data window: General appearances
        /// </summary>
        public static void
            Verify_the_following_information_The_state_of_an_input_field_is_changed_to_selected_when_release_the_pressed_area_at_the_Label_area_of_input_field(
                SignalPool pool)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Description: Input Field(1) The eventually displayed data value in the data area of the input field is replaced by “1” (character or value corresponding to the activated data key - state ‘Selected IF/value of pressed key(s)’)
        /// Used in:
        ///     Step 4 in TC-ID: 22.9.9 in 27.9.9 ‘SR speed / distance’ Data Checks: Technical Range Checks by Data Validity
        ///     Step 4 in TC-ID: 22.27.3 in 27.27.3 ‘Set VBC’ Data Checks: Technical Range Checks by Data Validity
        ///     Step 4 in TC-ID: 22.28.3 in 27.28.3 ‘Remove VBC’ Data Checks: Technical Range Checks by Data Validity
        ///     Step 4 in TC-ID: 22.29.3 in 27.29.3 ‘Train data’ (Flexible) Data Checks: Technical Range Checks by Data Validity
        /// </summary>
        public static void
            Input_Field1_The_eventually_displayed_data_value_in_the_data_area_of_the_input_field_is_replaced_by_1_character_or_value_corresponding_to_the_activated_data_key_state_Selected_IFvalue_of_pressed_keys(
                SignalPool pool)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Description: The state of ‘Yes’ button is changed to enabled
        /// Used in:
        ///     Step 33 in TC-ID: 22.13.1 in 27.13.1 Set Clock function: General appearance
        ///     Step 29 in TC-ID: 22.29.1 in 27.29.1 Flexible Train data window: General appearances
        /// </summary>
        public static void The_state_of_Yes_button_is_changed_to_enabled(SignalPool pool)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Description: Verify the following information,The fifth character is shown after a gap of fourth character, separated as 2 groups (e.g. 1234 56)
        /// Used in:
        ///     Step 14 in TC-ID: 7.3.2 in 27.17.3 Entering Characters
        ///     Step 9 in TC-ID: 22.18 in Train Running Number window
        ///     Step 8 in TC-ID: 22.27.1 in 27.27.1 ‘Set VBC’ Data Entry Window
        ///     Step 8 in TC-ID: 22.28.1 in 27.28.1 ‘Remove VBC’ Data Entry Window
        /// </summary>
        public static void
            Verify_the_following_information_The_fifth_character_is_shown_after_a_gap_of_fourth_character_separated_as_2_groups_e_g_1234_56(
                SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Verify the following information, The state of button is changed to ‘Enabled’
        /// Used in:
        ///     Step 4 in TC-ID: 22.18 in Train Running Number window
        ///     Step 3 in TC-ID: 22.19 in 27.19 Language Window
        ///     Step 4 in TC-ID: 22.22.3  in 27.22.3 Brake percentage window
        /// </summary>
        public static void Verify_the_following_information_The_state_of_button_is_changed_to_Enabled(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: No number is displayed on the Input Field
        /// Used in:
        ///     Step 7 in TC-ID: 22.18 in Train Running Number window
        ///     Step 8 in TC-ID: 22.22.3  in 27.22.3 Brake percentage window
        /// </summary>
        public static void No_number_is_displayed_on_the_Input_Field(SignalPool pool)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Description: Verify the following information,(1)    The state of an input field is changed to ‘Pressed’, the border of button is removed
        /// Used in:
        ///     Step 10 in TC-ID: 22.6.1 in 27.6.1 Password window
        ///     Step 12 in TC-ID: 22.6.1 in 27.6.1 Password window
        ///     Step 3 in TC-ID: 22.6.4.1 in 27.6.4.1
        ///     Step 5 in TC-ID: 22.6.4.1 in 27.6.4.1
        ///     Step 3 in TC-ID: 22.6.6.1 in 27.6.6.1
        ///     Step 5 in TC-ID: 22.6.6.1 in 27.6.6.1
        ///     Step 7 in TC-ID: 22.8.2.1 in 27.8.2.1 Radio Network ID window: General appearance
        ///     Step 9 in TC-ID: 22.8.2.1 in 27.8.2.1 Radio Network ID window: General appearance
        ///     Step 10 in TC-ID: 22.17 in 27.17.1 Driver ID window: General Display
        ///     Step 12 in TC-ID: 22.17 in 27.17.1 Driver ID window: General Display
        ///     Step 10 in TC-ID: 22.18 in Train Running Number window
        ///     Step 12 in TC-ID: 22.18 in Train Running Number window
        ///     Step 5 in TC-ID: 22.27.2 in 27.27.2 ‘Set VBC’ Validation Window
        ///     Step 10 in TC-ID: 22.28.2 in 27.28.2 ‘Remove VBC’ Validation Window
        ///     Step 4 in TC-ID: 22.19 in 27.19 Language Window
        ///     Step 6 in TC-ID: 22.19 in 27.19 Language Window
        ///     Step 12 in TC-ID: 22.22.3  in 27.22.3 Brake percentage window
        ///     Step 14 in TC-ID: 22.22.3  in 27.22.3 Brake percentage window
        ///     Step 6 in TC-ID: 22.24 in 27.24 Brightness window
        ///     Step 8 in TC-ID: 22.24 in 27.24 Brightness window
        ///     Step 6 in TC-ID: 22.25 in 27.25 Volume window
        ///     Step 8 in TC-ID: 22.25 in 27.25 Volume window
        ///     Step 3 in TC-ID: 22.27.2 in 27.27.2 ‘Set VBC’ Validation Window
        ///     Step 8 in TC-ID: 22.28.2 in 27.28.2 ‘Remove VBC’ Validation Window
        /// </summary>
        public static void
            Verify_the_following_information_1_The_state_of_an_input_field_is_changed_to_Pressed_the_border_of_button_is_removed(
                SignalPool pool)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Description: DMI displays Set VBC window
        /// Used in:
        ///     Step 13 in TC-ID: 22.27.1 in 27.27.1 ‘Set VBC’ Data Entry Window
        ///     Step 30 in TC-ID: 34.7 in 37.7 Dialogue Sequence of Settings window
        /// </summary>
        public static void DMI_displays_Set_VBC_window(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: The state of ‘VBC Code’ input field is changed to ‘accepted’
        /// Used in:
        ///     Step 14 in TC-ID: 22.27.1 in 27.27.1 ‘Set VBC’ Data Entry Window
        ///     Step 14 in TC-ID: 22.28.1 in 27.28.1 ‘Remove VBC’ Data Entry Window
        /// </summary>
        public static void The_state_of_VBC_Code_input_field_is_changed_to_accepted(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Verify the following information,The state of an input field is changed to ‘accepted’ when release the pressed area at the Data area of input field
        /// Used in:
        ///     Step 19 in TC-ID: 22.27.1 in 27.27.1 ‘Set VBC’ Data Entry Window
        ///     Step 19 in TC-ID: 22.28.1 in 27.28.1 ‘Remove VBC’ Data Entry Window
        /// </summary>
        public static void
            Verify_the_following_information_The_state_of_an_input_field_is_changed_to_accepted_when_release_the_pressed_area_at_the_Data_area_of_input_field(
                SignalPool pool)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Description: See the expected results of Step 7 – Step 8
        /// Used in:
        ///     Step 9 in TC-ID: 22.29.1 in 27.29.1 Flexible Train data window: General appearances
        ///     Step 20 in TC-ID: 22.29.1 in 27.29.1 Flexible Train data window: General appearances
        ///     Step 22 in TC-ID: 22.29.1 in 27.29.1 Flexible Train data window: General appearances
        ///     Step 24 in TC-ID: 22.29.1 in 27.29.1 Flexible Train data window: General appearances
        /// </summary>
        public static void See_the_expected_results_of_Step_7_Step_8(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: See the expected results of Step 11 – Step 13
        /// Used in:
        ///     Step 15 in TC-ID: 22.29.1 in 27.29.1 Flexible Train data window: General appearances
        ///     Step 17 in TC-ID: 22.29.1 in 27.29.1 Flexible Train data window: General appearances
        /// </summary>
        public static void See_the_expected_results_of_Step_11_Step_13(SignalPool pool)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Description: Verify the following information,Use the log file to confirm that DMI receives EVC-7 with variable OBU_TR_M_MODE = 3 (SH – Shunting).The symbol MO01 is display in area B7.DMI closes Main window and returns to the Default window
        /// Used in:
        ///     Step 1 in TC-ID: 34.4.1 in SH Symbol in Level 0 and Level 1
        ///     Step 1 in TC-ID: 34.4.2.2 in 37.4.2.2 SH Symbol in Level 2 and Level 3
        /// </summary>
        public static void
            Verify_the_following_information_Use_the_log_file_to_confirm_that_DMI_receives_EVC_7_with_variable_OBU_TR_M_MODE_3_SH_Shunting_The_symbol_MO01_is_display_in_area_B7_DMI_closes_Main_window_and_returns_to_the_Default_window(
                SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: See the expected results at Step 1
        /// Used in:
        ///     Step 2 in TC-ID: 34.4.1 in SH Symbol in Level 0 and Level 1
        ///     Step 2 in TC-ID: 34.4.2.1 in 37.4.2.1 Text Message “Shunting Refused” in Level 2 and Level 3
        ///     Step 2 in TC-ID: 34.4.2.2 in 37.4.2.2 SH Symbol in Level 2 and Level 3
        ///     Step 2 in TC-ID: 34.4.2.3 in 37.4.2.3 Text Message “Shunting Request Failed” in Level 2 and Level 3
        /// </summary>
        public static void See_the_expected_results_at_Step_1(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: System is power off and DMI displays ‘No contact with ATP’
        /// Used in:
        ///     Step 7 in TC-ID: 36.1 in Sound S1 - Driving too fast
        ///     Step 6 in TC-ID: 36.2 in Sound S3 - End of Intervention
        /// </summary>
        public static void System_is_power_off_and_DMI_displays_No_contact_with_ATP(SignalPool pool)
        {
            throw new NotImplementedException();
        }
    }
}
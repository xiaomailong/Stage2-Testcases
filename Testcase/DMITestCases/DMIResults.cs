using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
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
using Testcase.Telegrams;
using Testcase.Telegrams.EVCtoDMI;
using Testcase.Telegrams.DMItoEVC;
using static Testcase.Telegrams.EVCtoDMI.Variables;
using Testcase.TemporaryFunctions;

namespace Testcase.DMITestCases
{
    /// <summary>
    /// These are the generic methods used to check expected results on the DMI
    /// </summary>
    public static class DmiExpectedResults
    {
        /// <summary>
        /// Used when TC is not needed since it tests the same interfaces as another test case.
        /// </summary>
        /// <param name="pool">Signal pool</param>
        /// <param name="TestcaseID">Testcase ID as per specification</param>
        /// <param name="SectionNumber">Section number as per specification</param>
        public static void Testcase_not_required(SignalPool pool, string TestcaseID, string SectionNumber)
        {
            pool.TraceInfo($"This test case is not required since it tests the same interfaces as TC {TestcaseID}" +
                            $" in section {SectionNumber} of the specification.");
        }
        
        /// <summary>
        /// Prompt for verification of symbol displayed on the DMI.
        /// </summary>
        /// <param name="pool">Signal Pool</param>
        /// <param name="SymbolName">Symbol name as described in ERA_ERTMS_015560 v3.4</param>
        /// <param name="SymbolNumber">Symbol number as described in ERA_ERTMS_015560 v3.4</param>
        /// <param name="SymbolArea">Area of the DMI where the symbol should be displayed</param>
        /// <param name="YellowBorder">Boolean of whether the symbol should have a yellow border</param>
        public static void Driver_symbol_displayed(SignalPool pool, string SymbolName, string SymbolNumber, string SymbolArea,
                                                    bool YellowBorder)
        {
            if (YellowBorder)
                pool.WaitForVerification($"Is the {SymbolName} symbol ({SymbolNumber}) " +
                    $"displayed with a yellow border in area {SymbolArea}?");
            else
                pool.WaitForVerification($"Is the {SymbolName} symbol ({SymbolNumber}) " +
                    $"displayed in area {SymbolArea}?");
        }

        /// <summary>
        /// Description: SR Mode acknowledgement is requested on DMI area C1
        /// Used in:
        ///     Step 2 in TC-ID: 15.1.1 in 20.1.1
        /// </summary>
        public static void SR_Mode_Ack_requested(SignalPool pool)
        {
            Driver_symbol_displayed(pool, "Acknowledgement for Staff Responsible", "MO10", "C1", true);
        }

        /// <summary>
        /// Description: SR mode Acknowledgement symbol on DMI area C1 is pressed and released.
        /// Used in:
        ///     Step 3 in TC-ID: 15.1.1 in 20.1.1
        /// </summary>
        public static void SR_Mode_Ack_pressed_and_released(SignalPool pool)
        {
            EVC111_MMIDriverMessageAck.Check_MMI_Q_BUTTON = Variables.MMI_Q_BUTTON.Pressed;
            EVC111_MMIDriverMessageAck.Check_MMI_Q_BUTTON = Variables.MMI_Q_BUTTON.Released;
            pool.WaitForVerification("Has the MO10 symbol disappeared from sub-area C1 and re-appeared again?");
        }

        /// <summary>
        /// Description: SR mode Acknowledgement symbol on DMI area C1 is pressed and hold
        /// Used in:
        ///     Step 4 in TC-ID: 15.1.1 in 20.1.1
        /// </summary>
        public static void SR_Mode_Ack_pressed_and_hold(SignalPool pool)
        {
            EVC111_MMIDriverMessageAck.Check_MMI_Q_BUTTON = Variables.MMI_Q_BUTTON.Pressed;
            pool.Wait_Realtime(2000);
            EVC111_MMIDriverMessageAck.Check_MMI_Q_BUTTON = Variables.MMI_Q_BUTTON.Released;
            EVC152_MMIDriverAction.Check_MMI_M_DRIVER_ACTION = EVC152_MMIDriverAction.MMI_M_DRIVER_ACTION.StaffResponsibleModeAck;
            pool.WaitForVerification("Has the MO10 symbol opacity decreased to 50%?");
            
        }

        /// <summary>
        /// Description:
        /// Used in:
        ///     Step 2 in TC-ID: 12.7.2 in 17.7.2 
        /// </summary>
        /// <param name="pool"></param>
        public static void DMI_displays_in_SR_mode_level_1(SignalPool pool)
        {
            throw new NotImplementedException();
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
            EVC111_MMIDriverMessageAck.Check_MMI_Q_BUTTON = Variables.MMI_Q_BUTTON.Pressed;
            EVC111_MMIDriverMessageAck.Check_MMI_Q_BUTTON = Variables.MMI_Q_BUTTON.Released;
        }

        /// <summary>
        /// Description: TR mode Acknowledgement symbol on DMI area C1 is pressed and released.
        /// Used in:
        ///     Step 8 in TC-ID: 15.1.1 in 20.1.1
        /// </summary>
        public static void TR_Mode_Ack_pressed_and_released(SignalPool pool)
        {
            EVC111_MMIDriverMessageAck.Check_MMI_Q_BUTTON = Variables.MMI_Q_BUTTON.Pressed;
            EVC111_MMIDriverMessageAck.Check_MMI_Q_BUTTON = Variables.MMI_Q_BUTTON.Released;
            EVC152_MMIDriverAction.Check_MMI_M_DRIVER_ACTION = EVC152_MMIDriverAction.MMI_M_DRIVER_ACTION.TrainTripAck;
            pool.WaitForVerification("Has the MO05 symbol disappeared from sub-area C1?");
        }

        /// <summary>
        /// Description: RV mode Acknowledgement symbol on DMI area C1 is pressed and released.
        /// Used in:
        ///     Step 4 in TC-ID: 15.1.2 in 20.1.2
        /// </summary>
        public static void RV_Mode_Ack_pressed_and_released(SignalPool pool)
        {
            EVC111_MMIDriverMessageAck.Check_MMI_Q_BUTTON = Variables.MMI_Q_BUTTON.Pressed;
            EVC111_MMIDriverMessageAck.Check_MMI_Q_BUTTON = Variables.MMI_Q_BUTTON.Released;
            EVC152_MMIDriverAction.Check_MMI_M_DRIVER_ACTION = EVC152_MMIDriverAction.MMI_M_DRIVER_ACTION.ReversingModeAck;
            pool.WaitForVerification("Has the MO15 symbol disappeared from sub-area C1?");
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
        /// Description: DMI does NOT display SL mode despite EVC-7 with [MMI_ETCS_MISC_OUT_SIGNALS.OBU_TR_M_MODE] = 5 received
        /// Used in:
        ///     Step 5 in TC-ID: 15.1.2 in 20.1.2
        /// </summary>
        public static void SL_Mode_NOT_displayed(SignalPool pool)
        {
            EVC102_MMIStatusReport.Check_MMI_M_MODE_READBACK = EVC102_MMIStatusReport.MMI_M_MODE_READBACK.Sleeping;
            pool.WaitForVerification("Is there NO symbol displayed in area B7?");
        }

        /// <summary>
        /// Description: DMI displays Main window with enabled ‘Start’ button
        /// Used in:
        ///     Step 1 in TC-ID: 5.10 in 10.10 Screen Layout: Button States
        ///     Step 3 in TC-ID: 22.20.2 in 27.20.2 Override window in SB mode
        ///     Step 9 in TC-ID: 15.1.1 in 20.1.1
        /// </summary>
        public static void Main_Window_displayed_with_Start_button_enabled(SignalPool pool)
        {
            pool.WaitForVerification("Is the Main window displayed on the DMI, with the Start button enabled?");
        }

        /// <summary>
        /// Description: DMI displays Main window with enabled ‘Start’ button
        /// Used in:
        ///     Step 1 in TC-ID: 5.10 in 10.10 Screen Layout: Button States
        ///     Step 3 in TC-ID: 22.20.2 in 27.20.2 Override window in SB mode
        ///     Step 9 in TC-ID: 15.1.1 in 20.1.1
        /// </summary>
        public static void Start_Button_pressed_and_released(SignalPool pool)
        {
            EVC101_MMIDriverRequest.CheckMRequestPressed = Variables.MMI_M_REQUEST.Start;
            EVC101_MMIDriverRequest.CheckMRequestReleased = Variables.MMI_M_REQUEST.Start;
            EVC152_MMIDriverAction.Check_MMI_M_DRIVER_ACTION = EVC152_MMIDriverAction.MMI_M_DRIVER_ACTION.StartSelected;
        }

        /// <summary>
        /// Description: Shunting button is pressed and hold
        /// Used in:
        ///     Step 10 in TC-ID: 15.1.1 in 20.1.1     
        /// </summary>
        public static void Shunting_button_pressed_and_hold(SignalPool pool)
        {
            EVC101_MMIDriverRequest.CheckMRequestPressed = Variables.MMI_M_REQUEST.StartShunting;
            pool.Wait_Realtime(2000);
            EVC101_MMIDriverRequest.CheckMRequestReleased = Variables.MMI_M_REQUEST.StartShunting;
            EVC152_MMIDriverAction.Check_MMI_M_DRIVER_ACTION = EVC152_MMIDriverAction.MMI_M_DRIVER_ACTION.ShuntingSelected;
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
        /// Description: Exit Shunting button is pressed and hold
        /// Used in:
        ///     Step 11 in TC-ID: 15.1.1 in 20.1.1     
        /// </summary>
        public static void Exit_Shunting_button_pressed_and_hold(SignalPool pool)
        {
            EVC101_MMIDriverRequest.CheckMRequestPressed = Variables.MMI_M_REQUEST.ExitShunting;
            pool.Wait_Realtime(2000);
            EVC101_MMIDriverRequest.CheckMRequestReleased = Variables.MMI_M_REQUEST.ExitShunting;
            EVC152_MMIDriverAction.Check_MMI_M_DRIVER_ACTION = EVC152_MMIDriverAction.MMI_M_DRIVER_ACTION.ExitShuntingSelected;
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
        /// Description: Non-leading button is pressed and hold
        /// Used in:
        ///     Step 11 in TC-ID: 15.1.1 in 20.1.1     
        /// </summary>
        public static void Non_leading_button_pressed_and_hold(SignalPool pool)
        {
            EVC101_MMIDriverRequest.CheckMRequestPressed = Variables.MMI_M_REQUEST.StartNonLeading;
            pool.Wait_Realtime(2000);
            EVC101_MMIDriverRequest.CheckMRequestReleased = Variables.MMI_M_REQUEST.StartNonLeading;
            EVC152_MMIDriverAction.Check_MMI_M_DRIVER_ACTION = EVC152_MMIDriverAction.MMI_M_DRIVER_ACTION.NonLeadingSelected;
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
        /// Description: DMI displays the text "Driver's cab not active" in area E5
        /// Used in:
        ///     Step 5 in TC-ID: 15.1.2 in 20.1.2
        /// </summary>
        /// <param name="pool"></param>
        public static void Driver_s_cab_not_active_msg_displayed(SignalPool pool)
        {
            pool.WaitForVerification("Is the text \"Driver's cab not active\" displayed in area E5?");
        }

        /// <summary>
        /// Description: Driver ID is entered
        /// Used in:
        ///     Step 2 in TC-ID: 15.1.3 in 20.1.3
        /// </summary>
        /// <param name="pool"></param>
        public static void Driver_ID_entered(SignalPool pool)
        {
            string driverIDInput = DmiActions.ShowDialog("Please enter Driver ID", "Driver ID");
            EVC104_MMINewDriverData.Check_X_DRIVER_ID = driverIDInput;
        }

        /// <summary>
        /// Description: 
        /// Used in:
        ///     Step 2 in TC-ID: 15.1.3 in 20.1.3
        /// </summary>
        /// <param name="pool"></param>
        /// <param name="order">Indicates if Driver allows Brake Test to be performed</param>
        public static void Brake_Test_Perform_Order(SignalPool pool, bool order)
        {
            string _sOrder;
            if (order) { _sOrder = "Yes"; } else { _sOrder = "No"; }

            DmiActions.ShowInstruction(pool, "Press \"" + _sOrder + "\" on DMI in area E.");

            if (order) { EVC111_MMIDriverMessageAck.Check_MMI_Q_ACK = EVC111_MMIDriverMessageAck.MMI_Q_ACK.AcknowledgeYES; }
            else { EVC111_MMIDriverMessageAck.Check_MMI_Q_ACK = EVC111_MMIDriverMessageAck.MMI_Q_ACK.NotAcknowledgeNO; }
        }

        /// <summary>
        /// Description:
        /// Used in:
        ///     Step 4 in TC-ID: 15.1.3 in 20.1.3     
        /// </summary>
        public static void Train_Data_Button_pressed_and_released(SignalPool pool)
        {
            EVC101_MMIDriverRequest.CheckMRequestPressed = Variables.MMI_M_REQUEST.StartTrainDataEntry;
            pool.Wait_Realtime(100);
            EVC101_MMIDriverRequest.CheckMRequestReleased = Variables.MMI_M_REQUEST.StartTrainDataEntry;
            EVC152_MMIDriverAction.Check_MMI_M_DRIVER_ACTION = EVC152_MMIDriverAction.MMI_M_DRIVER_ACTION.TrainDataEntryRequested;
        }

        /// <summary>
        /// Description: Level 0 is selected
        /// Used in:
        ///     Step 3 in TC-ID: 15.1.3 in 20.1.3
        /// </summary>
        /// <param name="pool"></param>
        public static void Level_0_Selected(SignalPool pool)
        {
            EVC121_MMINewLevel.Check_MMI_M_LEVEL_NTC_ID = MMI_M_LEVEL_NTC_ID.L0;
            EVC121_MMINewLevel.Check_MMI_Q_LEVEL_NTC_ID = MMI_Q_LEVEL_NTC_ID.ETCS_Level;
            EVC121_MMINewLevel.Check_MMI_M_LEVEL_FLAG = MMI_M_LEVEL_FLAG.MarkedLevel;
            EVC121_MMINewLevel.Check_MMI_M_INHIBITED_LEVEL = MMI_M_INHIBITED_LEVEL.NotInhibited;
            EVC121_MMINewLevel.Check_MMI_M_INHIBIT_ENABLE = MMI_M_INHIBIT_ENABLE.NotAllowedForInhibiting;
            EVC152_MMIDriverAction.Check_MMI_M_DRIVER_ACTION = EVC152_MMIDriverAction.MMI_M_DRIVER_ACTION.Level0Selected;
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
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: DMI displays the default window. The Driver ID window is displayed
        /// Used in:
        ///     Step 1 in TC-ID: 1.2 in 6.2 Internal Components
        ///     Step 7 in TC-ID: 1.2 in 6.2 Internal Components
        ///     Step 1 in TC-ID: 5.3 in 10.3 Screen Layout: Frames
        ///     Step 1 in TC-ID: 15.2.6 in 20.2.7 ETCS Level: STM level symbol
        ///     Step 1 in TC-ID: 17.10.3 in 22.10.3 Zoom PA Function with Scale Down
        ///     Step 1 in TC-ID: 17.10.4 in 22.10.4 Zoom PA Function with the communication loss between ETCS Onboard and DMI
        ///     Step 1 in TC-ID: 17.11 in 22.11 Handle at least 31 PA Speed Profile Segments
        ///     Step 1 in TC-ID: 17.12 in Handle at least 31 PA Gradient Profile Segments
        /// </summary>
        public static void DMI_displays_the_default_window_The_Driver_ID_window_is_displayed(SignalPool pool)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: The Settings window is presented with all sub-menus
        /// Used in:
        ///     Step 3 in TC-ID: 1.2 in 6.2 Internal Components
        ///     Step 2 in TC-ID: 1.6 in 6.6 Adjustment of Sound Volume
        /// </summary>
        public static void The_Settings_window_is_presented_with_all_sub_menus(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: The Settings window is displayed
        /// Used in:
        ///     Step 5 in TC-ID: 1.2 in 6.2 Internal Components
        ///     Step 2 in TC-ID: 33.1 in 36.1 The relationship between parent and child windows (1)
        /// </summary>
        public static void The_Settings_window_is_displayed(SignalPool pool)
        {
            throw new NotImplementedException();
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
            EVC102_MMIStatusReport.Check_MMI_M_ACTIVE_CABIN = Variables.MMI_M_ACTIVE_CABIN.Cabin1Active;           
        }

        /// <summary>
        /// Description: Cabin B is deactivated
        /// Used in:     
        ///     Step 12 in TC-ID: 15.1.1 in 20.1.1
        /// </summary>
        public static void Cabin_B_is_activated(SignalPool pool)
        {
            EVC102_MMIStatusReport.Check_MMI_M_ACTIVE_CABIN = Variables.MMI_M_ACTIVE_CABIN.Cabin2Active;
        }

        /// <summary>
        /// Description: Cabin B is deactivated
        /// Used in:     
        ///     Step 12 in TC-ID: 15.1.1 in 20.1.1
        /// </summary>
        public static void Cab_deactivated(SignalPool pool)
        {
            EVC102_MMIStatusReport.Check_MMI_M_ACTIVE_CABIN = Variables.MMI_M_ACTIVE_CABIN.NoCabinActive;
        }

        /// <summary>
        /// Description: RCI logs the concerned activities as specified in the precondition
        /// Used in:
        ///     Step 1 in TC-ID: 1.9 in 6.9 Performance of ETCS-DMI: Data handling
        ///     Step 1 in TC-ID: 1.10 in 6.10 Performance of ETCS-DMI Data Processing
        /// </summary>
        public static void RCI_logs_the_concerned_activities_as_specified_in_the_precondition(SignalPool pool)
        {
            throw new NotImplementedException();
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
        /// Description: The Default window is displayed.Verify the following information,The local time is displayed in form of ‘hh:mm:ss’ with flashing colons at sub-area G13
        /// Used in:
        ///     Step 2 in TC-ID: 2.3 in 7.3 Frozen Display(s)
        ///     Step 3 in TC-ID: 2.3 in 7.3 Frozen Display(s)
        ///     Step 4 in TC-ID: 2.3 in 7.3 Frozen Display(s)
        /// </summary>
        public static void
            The_Default_window_is_displayed_Verify_the_following_information_The_local_time_is_displayed_in_form_of_hhmmss_with_flashing_colons_at_sub_area_G13(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: DMI displays Settings window.Verify the following information,(1)    Use the log file to confirm that DMI sent out packet EVC-141 with variable based on confirmed data to ETCS Onboard
        /// Used in:
        ///     Step 3 in TC-ID: 2.6 in 7.6 Safety related Data Entry
        ///     Step 6 in TC-ID: 2.6 in 7.6 Safety related Data Entry
        /// </summary>
        public static void
            DMI_displays_Settings_window_Verify_the_following_information_1_Use_the_log_file_to_confirm_that_DMI_sent_out_packet_EVC_141_with_variable_based_on_confirmed_data_to_ETCS_Onboard(SignalPool pool)
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
            pool.WaitForVerification("Is the Train Data window displayed?");
        }

        /// <summary>
        /// Description: DMI displays the Main window
        /// Used in:
        ///     Step 6 in TC-ID: 5.3 in 10.3 Screen Layout: Frames
        ///     Step 5 in TC-ID: 5.12.2 in 10.12.2 Close, Next, Previous and Yes Buttons
        /// </summary>
        public static void DMI_displays_the_Main_window(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: The distance range on the Planning area is not changed.The sound ‘click’ is not played
        /// Used in:
        ///     Step 3 in TC-ID: 5.12.1 in 10.12.1 Navigation Button on the Planning Area
        ///     Step 4 in TC-ID: 5.12.1 in 10.12.1 Navigation Button on the Planning Area
        /// </summary>
        public static void The_distance_range_on_the_Planning_area_is_not_changed_The_sound_click_is_not_played(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: The distance range on the Planning range is not changed.The sound ‘click’ is not played
        /// Used in:
        ///     Step 8 in TC-ID: 5.12.1 in 10.12.1 Navigation Button on the Planning Area
        ///     Step 9 in TC-ID: 5.12.1 in 10.12.1 Navigation Button on the Planning Area
        /// </summary>
        public static void The_distance_range_on_the_Planning_range_is_not_changed_The_sound_click_is_not_played(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: DMI still displays the planning area.The sound ‘click’ is not played
        /// Used in:
        ///     Step 14 in TC-ID: 5.12.1 in 10.12.1 Navigation Button on the Planning Area
        ///     Step 15 in TC-ID: 5.12.1 in 10.12.1 Navigation Button on the Planning Area
        /// </summary>
        public static void DMI_still_displays_the_planning_area_The_sound_click_is_not_played(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: The planning area still hidden.The sound ‘click’ is not played
        /// Used in:
        ///     Step 18 in TC-ID: 5.12.1 in 10.12.1 Navigation Button on the Planning Area
        ///     Step 19 in TC-ID: 5.12.1 in 10.12.1 Navigation Button on the Planning Area
        /// </summary>
        public static void The_planning_area_still_hidden_The_sound_click_is_not_played(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Verify the following information,The state of pressed button is changed to ‘Enabled’ state
        /// Used in:
        ///     Step 4 in TC-ID: 5.12.2 in 10.12.2 Close, Next, Previous and Yes Buttons
        ///     Step 3 in TC-ID: 22.5.1 in 27.5.1 Level Selection Window: General appearance
        /// </summary>
        public static void Verify_the_following_information_The_state_of_pressed_button_is_changed_to_Enabled_state(SignalPool pool)
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
            EVC102_MMIStatusReport.Check_MMI_M_MODE_READBACK = EVC102_MMIStatusReport.MMI_M_MODE_READBACK.StaffResponsible;
            Driver_symbol_displayed(pool, "Staff Responsible", "MO9", "B7?", false);
        }

        /// <summary>
        /// Description: The display information on DMI still not change, ST01 symbol is displayed on sub-area C9
        /// Used in:
        ///     Step 8 in TC-ID: 6.3 in 11.3 Acknowledgements: Priority of new incoming acknowledgements
        ///     Step 9 in TC-ID: 6.3 in 11.3 Acknowledgements: Priority of new incoming acknowledgements
        ///     Step 10 in TC-ID: 6.3 in 11.3 Acknowledgements: Priority of new incoming acknowledgements
        ///     Step 11 in TC-ID: 6.3 in 11.3 Acknowledgements: Priority of new incoming acknowledgements
        /// </summary>
        public static void The_display_information_on_DMI_still_not_change_ST01_symbol_is_displayed_on_sub_area_C9(SignalPool pool)
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
        public static void DMI_displays_Train_data_validation_window(SignalPool pool)
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
            Verify_the_following_information_DMI_displays_Main_window_Use_the_log_file_to_confirm_that_DMI_sends_out_the_packet_MMI_DRIVER_REQUEST_EVC_101_with_variable_MMI_DRIVER_REQUEST_EVC_101_MMI_M_REQUEST_4_Exit_Train_data(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: DMI displays Train Running Number window.Verify the following information,The Train data validation is closed.Use the log file to confirm that DMI sends out the packet [MMI_CONFIRMED_TRAIN DATA (EVC-110)] with variable based on confirmed data
        /// Used in:
        ///     Step 8 in TC-ID: 9.1 in Data Validation Window for Flexible train data entry window
        ///     Step 8 in TC-ID: 9.2 in 14.2 Data Validation Window for Fixed train data entry window
        /// </summary>
        public static void
            DMI_displays_Train_Running_Number_window_Verify_the_following_information_The_Train_data_validation_is_closed_Use_the_log_file_to_confirm_that_DMI_sends_out_the_packet_MMI_CONFIRMED_TRAIN_DATA_EVC_110_with_variable_based_on_confirmed_data(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: (1)   The Default window is displayed
        /// Used in:
        ///     Step 5 in TC-ID: 10.1 in 15.1 Data Entry/Validation/View process
        ///     Step 7 in TC-ID: 10.1 in 15.1 Data Entry/Validation/View process
        ///     Step 9 in TC-ID: 10.1 in 15.1 Data Entry/Validation/View process
        ///     Step 14 in TC-ID: 10.1 in 15.1 Data Entry/Validation/View process
        /// </summary>
        public static void The_Default_window_is_displayed(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: DMI displays Train Running Number window
        /// Used in:
        ///     Step 11 in TC-ID: 10.2 in 15.2.1 State 'ST05': General Appearance
        ///     Step 6 in TC-ID: 15.1.3 in 20.1.3 Mode Symbols in Sub-Area B7 for OS, UN mode
        ///     Step 9 in TC-ID: 34.1.4 in 37.1.4.1.1 Data entry/validation process when enabling conditions not fullfilled: Level 1
        /// </summary>
        public static void DMI_displays_Train_Running_Number_window(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: See the expectation in step 4
        /// Used in:
        ///     Step 15 in TC-ID: 10.2.2 in 15.2.2 State 'ST05': Main window and windows in main menu.
        ///     Step 18 in TC-ID: 10.2.2 in 15.2.2 State 'ST05': Main window and windows in main menu.
        ///     Step 20 in TC-ID: 10.2.2 in 15.2.2 State 'ST05': Main window and windows in main menu.
        ///     Step 35 in TC-ID: 10.2.6 in 15.2.6 State 'ST05': Settings window and windows in setting menu
        ///     Step 37 in TC-ID: 10.2.6 in 15.2.6 State 'ST05': Settings window and windows in setting menu
        /// </summary>
        public static void See_the_expectation_in_step_4(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Verify the following information;DMI in the entry state of ‘ST05’(1)   The hourglass symbol ST05 is displayed.(2)   Verify all buttons and the close button is disable.(3)   The disabled Close button NA12 is display in area G.10 seconds laterDMI in the exit state of ‘ST05’(4)   The hourglass symbol ST05 is removed.(5)   The state of all buttons is restored according to the last status before script is sent.(6)   The enabled Close button NA11 is display in area G
        /// Used in:
        ///     Step 2 in TC-ID: 10.2.4 in 15.2.4 State 'ST05': Data view window
        ///     Step 4 in TC-ID: 10.2.6 in 15.2.6 State 'ST05': Settings window and windows in setting menu
        /// </summary>
        public static void
            Verify_the_following_informationDMI_in_the_entry_state_of_ST051_The_hourglass_symbol_ST05_is_displayed_2_Verify_all_buttons_and_the_close_button_is_disable_3_The_disabled_Close_button_NA12_is_display_in_area_G_10_seconds_laterDMI_in_the_exit_state_of_ST054_The_hourglass_symbol_ST05_is_removed_5_The_state_of_all_buttons_is_restored_according_to_the_last_status_before_script_is_sent_6_The_enabled_Close_button_NA11_is_display_in_area_G(SignalPool pool)
        {
            throw new NotImplementedException();
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
        /// Description: Verify the following information;DMI in the entry state of ‘ST05’(1)   The hourglass symbol ST05 is displayed.(2)   Verify all buttons and the close button is disable.(3)   The disabled Close button NA12 is display in area G.(4)   The Input Field is deselected.10 seconds laterDMI in the exit state of ‘ST05’(5)   The hourglass symbol ST05 is removed.(6)   The state of all buttons is restored according to the last status before script is sent.(7)   The enabled Close button NA11 is display in area G.(8)   The input field is in the ‘Selected’ state
        /// Used in:
        ///     Step 2 in TC-ID: 10.2.6 in 15.2.6 State 'ST05': Settings window and windows in setting menu
        ///     Step 8 in TC-ID: 10.2.6 in 15.2.6 State 'ST05': Settings window and windows in setting menu
        /// </summary>
        public static void
            Verify_the_following_informationDMI_in_the_entry_state_of_ST051_The_hourglass_symbol_ST05_is_displayed_2_Verify_all_buttons_and_the_close_button_is_disable_3_The_disabled_Close_button_NA12_is_display_in_area_G_4_The_Input_Field_is_deselected_10_seconds_laterDMI_in_the_exit_state_of_ST055_The_hourglass_symbol_ST05_is_removed_6_The_state_of_all_buttons_is_restored_according_to_the_last_status_before_script_is_sent_7_The_enabled_Close_button_NA11_is_display_in_area_G_8_The_input_field_is_in_the_Selected_state(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: See the expectation in step 2
        /// Used in:
        ///     Step 10 in TC-ID: 10.2.6 in 15.2.6 State 'ST05': Settings window and windows in setting menu
        ///     Step 17 in TC-ID: 10.2.6 in 15.2.6 State 'ST05': Settings window and windows in setting menu
        ///     Step 19 in TC-ID: 10.2.6 in 15.2.6 State 'ST05': Settings window and windows in setting menu
        ///     Step 21 in TC-ID: 10.2.6 in 15.2.6 State 'ST05': Settings window and windows in setting menu
        ///     Step 25 in TC-ID: 10.2.6 in 15.2.6 State 'ST05': Settings window and windows in setting menu
        ///     Step 29 in TC-ID: 10.2.6 in 15.2.6 State 'ST05': Settings window and windows in setting menu
        ///     Step 39 in TC-ID: 10.2.6 in 15.2.6 State 'ST05': Settings window and windows in setting menu
        /// </summary>
        public static void See_the_expectation_in_step_2(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: See the expectation in step 8
        /// Used in:
        ///     Step 12 in TC-ID: 10.2.6 in 15.2.6 State 'ST05': Settings window and windows in setting menu
        ///     Step 27 in TC-ID: 10.2.6 in 15.2.6 State 'ST05': Settings window and windows in setting menu
        ///     Step 31 in TC-ID: 10.2.6 in 15.2.6 State 'ST05': Settings window and windows in setting menu
        ///     Step 41 in TC-ID: 10.2.6 in 15.2.6 State 'ST05': Settings window and windows in setting menu
        /// </summary>
        public static void See_the_expectation_in_step_8(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: The hourglass symbol ST05 is displayed at window title area
        /// Used in:
        ///     Step 2 in TC-ID: 10.4.1.1 in 15.4.1.1 State ‘ST05’: Abort the pending Data Process in Main window
        ///     Step 2 in TC-ID: 10.4.1.2 in 15.4.1.2 State ‘ST05’: Abort the pending Data Process in Data View window
        ///     Step 2 in TC-ID: 10.4.1.3 in 15.4.1.3 State ‘ST05’: Abort the pending Data Process in Special window
        ///     Step 1 in TC-ID: 10.4.1.4 in 15.4.1.4 State ‘ST05’: Abort the pending Data Process in Settings window
        ///     Step 2 in TC-ID: 10.4.1.5 in 15.4.1.5 State ‘ST05’: Abort the pending Data Process in RBC Contact menu
        /// </summary>
        public static void The_hourglass_symbol_ST05_is_displayed_at_window_title_area(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: DMI displays SR speed/distance window
        /// Used in:
        ///     Step 1 in TC-ID: 10.4.1.3 in 15.4.1.3 State ‘ST05’: Abort the pending Data Process in Special window
        ///     Step 24 in TC-ID: 22.9.1 in 27.9.1 SR Speed/Distance window: General appearance
        /// </summary>
        public static void DMI_displays_SR_speeddistance_window(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: DMI displays RBC Data window
        /// Used in:
        ///     Step 1 in TC-ID: 10.4.1.5 in 15.4.1.5 State ‘ST05’: Abort the pending Data Process in RBC Contact menu
        ///     Step 1 in TC-ID: 22.20.2 in 27.20.2 Override window in SB mode
        /// </summary>
        public static void DMI_displays_RBC_Data_window(SignalPool pool)
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
        /// Description: The train speed is force to decrease because of emergency brake is applied by ETCS onboard.Verify the following information,Before train speed is decreased(1)   Use the log file to confirm that DMI received the packet information EVC-1 with the following condition,MMI_M_WARNING = 12 (Status = IntS, Supervision = CSM) while the value of MMI_V_TRAIN = 1278 (46 km/h) which greater than MMI_V_INTERVENTION(2)   The speed pointer display in red colourAfter train speed is decreased(3)   Use the log file to confirm that DMI received the packet information EVC-1 with the following condition,MMI_M_WARNING = 12 (Status = IntS, Supervision = CSM) while the value of MMI_V_TRAIN is lower than MMI_V_INTERVENTION(4)   The speed pointer display in grey colour
        /// Used in:
        ///     Step 4 in TC-ID: 12.3.2 in 17.3.2 Speed Pointer: Colour of speed pointer in FS mode
        ///     Step 4 in TC-ID: 12.3.3 in 17.3.3 Speed Pointer: Colour of speed pointer in SR mode
        /// </summary>
        public static void
            The_train_speed_is_force_to_decrease_because_of_emergency_brake_is_applied_by_ETCS_onboard_Verify_the_following_information_Before_train_speed_is_decreased1_Use_the_log_file_to_confirm_that_DMI_received_the_packet_information_EVC_1_with_the_following_condition_MMI_M_WARNING_12_Status_IntS_Supervision_CSM_while_the_value_of_MMI_V_TRAIN_1278_46_kmh_which_greater_than_MMI_V_INTERVENTION2_The_speed_pointer_display_in_red_colourAfter_train_speed_is_decreased3_Use_the_log_file_to_confirm_that_DMI_received_the_packet_information_EVC_1_with_the_following_condition_MMI_M_WARNING_12_Status_IntS_Supervision_CSM_while_the_value_of_MMI_V_TRAIN_is_lower_than_MMI_V_INTERVENTION4_The_speed_pointer_display_in_grey_colour(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: DMI displays in FS mode, level 1.Verify the following information,(1)   The speed pointer display in grey colour
        /// Used in:
        ///     Step 9 in TC-ID: 12.3.2 in 17.3.2 Speed Pointer: Colour of speed pointer in FS mode
        ///     Step 14 in TC-ID: 12.3.2 in 17.3.2 Speed Pointer: Colour of speed pointer in FS mode
        /// </summary>
        public static void
            DMI_displays_in_FS_mode_level_1_Verify_the_following_information_1_The_speed_pointer_display_in_grey_colour(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: DMI displays in SR mode, level 1.Verify the following information,(1)   The speed pointer display in grey colour
        /// Used in:
        ///     Step 5 in TC-ID: 12.3.3 in 17.3.3 Speed Pointer: Colour of speed pointer in SR mode
        ///     Step 11 in TC-ID: 12.3.3 in 17.3.3 Speed Pointer: Colour of speed pointer in SR mode
        ///     Step 12 in TC-ID: 12.3.3 in 17.3.3 Speed Pointer: Colour of speed pointer in SR mode
        ///     Step 19 in TC-ID: 12.3.3 in 17.3.3 Speed Pointer: Colour of speed pointer in SR mode
        /// </summary>
        public static void
            DMI_displays_in_SR_mode_level_1_Verify_the_following_information_1_The_speed_pointer_display_in_grey_colour(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: DMI displays in SR mode, level 1.Verify the following information,(1)   The speed pointer display in white colour
        /// Used in:
        ///     Step 6 in TC-ID: 12.3.3 in 17.3.3 Speed Pointer: Colour of speed pointer in SR mode
        ///     Step 10 in TC-ID: 12.3.3 in 17.3.3 Speed Pointer: Colour of speed pointer in SR mode
        ///     Step 13 in TC-ID: 12.3.3 in 17.3.3 Speed Pointer: Colour of speed pointer in SR mode
        /// </summary>
        public static void
            DMI_displays_in_SR_mode_level_1_Verify_the_following_information_1_The_speed_pointer_display_in_white_colour(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: DMI displays in SR mode, level 1.Verify the following information,(1)   The speed pointer display in orange colour
        /// Used in:
        ///     Step 7 in TC-ID: 12.3.3 in 17.3.3 Speed Pointer: Colour of speed pointer in SR mode
        ///     Step 8 in TC-ID: 12.3.3 in 17.3.3 Speed Pointer: Colour of speed pointer in SR mode
        ///     Step 15 in TC-ID: 12.3.3 in 17.3.3 Speed Pointer: Colour of speed pointer in SR mode
        ///     Step 16 in TC-ID: 12.3.3 in 17.3.3 Speed Pointer: Colour of speed pointer in SR mode
        /// </summary>
        public static void
            DMI_displays_in_SR_mode_level_1_Verify_the_following_information_1_The_speed_pointer_display_in_orange_colour(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: DMI displays in SR mode, level 1.Verify the following information,(1)   The speed pointer display in red colour
        /// Used in:
        ///     Step 9 in TC-ID: 12.3.3 in 17.3.3 Speed Pointer: Colour of speed pointer in SR mode
        ///     Step 17 in TC-ID: 12.3.3 in 17.3.3 Speed Pointer: Colour of speed pointer in SR mode
        /// </summary>
        public static void
            DMI_displays_in_SR_mode_level_1_Verify_the_following_information_1_The_speed_pointer_display_in_red_colour(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: DMI displays in SR mode, level 1.Verify the following information,(1)   The speed pointer display in yellow colour
        /// Used in:
        ///     Step 14 in TC-ID: 12.3.3 in 17.3.3 Speed Pointer: Colour of speed pointer in SR mode
        ///     Step 18 in TC-ID: 12.3.3 in 17.3.3 Speed Pointer: Colour of speed pointer in SR mode
        /// </summary>
        public static void
            DMI_displays_in_SR_mode_level_1_Verify_the_following_information_1_The_speed_pointer_display_in_yellow_colour(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Verify the following information,(1)   Use the log file to confirm that DMI received the packet information EVC-1 with the following condition,MMI_M_WARNING = 8 (Status = OvS, Supervision = CSM) while the value of MMI_V_TRAIN = 2806 (101 km/h) which greater than MMI_V_PERMITTED(2)   The speed pointer display in orange colour
        /// Used in:
        ///     Step 2 in TC-ID: 12.3.4 in 17.3.4 Speed Pointer: Colour of speed pointer in UN mode
        ///     Step 3 in TC-ID: 12.3.7 in 17.3.7 Speed Pointer: Colour of speed pointer in LS mode
        /// </summary>
        public static void
            Verify_the_following_information_1_Use_the_log_file_to_confirm_that_DMI_received_the_packet_information_EVC_1_with_the_following_condition_MMI_M_WARNING_8_Status_OvS_Supervision_CSM_while_the_value_of_MMI_V_TRAIN_2806_101_kmh_which_greater_than_MMI_V_PERMITTED2_The_speed_pointer_display_in_orange_colour(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Verify the following information,(1)   Use the log file to confirm that DMI received the packet information EVC-1 with the following condition,MMI_M_WARNING = 4 (Status = WaS, Supervision = CSM) while the value of MMI_V_TRAIN = 2917 (105 km/h) which greater than MMI_V_PERMITTED but lower than MMI_V_INTERVENTION(2)   The speed pointer display in orange colour
        /// Used in:
        ///     Step 3 in TC-ID: 12.3.4 in 17.3.4 Speed Pointer: Colour of speed pointer in UN mode
        ///     Step 4 in TC-ID: 12.3.7 in 17.3.7 Speed Pointer: Colour of speed pointer in LS mode
        /// </summary>
        public static void
            Verify_the_following_information_1_Use_the_log_file_to_confirm_that_DMI_received_the_packet_information_EVC_1_with_the_following_condition_MMI_M_WARNING_4_Status_WaS_Supervision_CSM_while_the_value_of_MMI_V_TRAIN_2917_105_kmh_which_greater_than_MMI_V_PERMITTED_but_lower_than_MMI_V_INTERVENTION2_The_speed_pointer_display_in_orange_colour(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: The train speed is force to decrease because of emergency brake is applied by ETCS onboard.Verify the following information,Before train speed is decreased(1)   Use the log file to confirm that DMI received the packet information EVC-1 with the following condition,MMI_M_WARNING = 12 (Status = IntS, Supervision = CSM) while the value of MMI_V_TRAIN = 2944 (106 km/h) which greater than MMI_V_INTERVENTION(2)   The speed pointer display in red colourAfter train speed is decreased(3)   Use the log file to confirm that DMI received the packet information EVC-1 with the following condition,MMI_M_WARNING = 12 (Status = IntS, Supervision = CSM) while the value of MMI_V_TRAIN is lower than MMI_V_INTERVENTION(4)   The speed pointer display in grey colour
        /// Used in:
        ///     Step 4 in TC-ID: 12.3.4 in 17.3.4 Speed Pointer: Colour of speed pointer in UN mode
        ///     Step 5 in TC-ID: 12.3.7 in 17.3.7 Speed Pointer: Colour of speed pointer in LS mode
        /// </summary>
        public static void
            The_train_speed_is_force_to_decrease_because_of_emergency_brake_is_applied_by_ETCS_onboard_Verify_the_following_information_Before_train_speed_is_decreased1_Use_the_log_file_to_confirm_that_DMI_received_the_packet_information_EVC_1_with_the_following_condition_MMI_M_WARNING_12_Status_IntS_Supervision_CSM_while_the_value_of_MMI_V_TRAIN_2944_106_kmh_which_greater_than_MMI_V_INTERVENTION2_The_speed_pointer_display_in_red_colourAfter_train_speed_is_decreased3_Use_the_log_file_to_confirm_that_DMI_received_the_packet_information_EVC_1_with_the_following_condition_MMI_M_WARNING_12_Status_IntS_Supervision_CSM_while_the_value_of_MMI_V_TRAIN_is_lower_than_MMI_V_INTERVENTION4_The_speed_pointer_display_in_grey_colour(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: DMI displays in UN mode, level 0.Verify the following information,(1)   The speed pointer display in grey colour
        /// Used in:
        ///     Step 5 in TC-ID: 12.3.4 in 17.3.4 Speed Pointer: Colour of speed pointer in UN mode
        ///     Step 11 in TC-ID: 12.3.4 in 17.3.4 Speed Pointer: Colour of speed pointer in UN mode
        ///     Step 12 in TC-ID: 12.3.4 in 17.3.4 Speed Pointer: Colour of speed pointer in UN mode
        ///     Step 19 in TC-ID: 12.3.4 in 17.3.4 Speed Pointer: Colour of speed pointer in UN mode
        /// </summary>
        public static void
            DMI_displays_in_UN_mode_level_0_Verify_the_following_information_1_The_speed_pointer_display_in_grey_colour(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: DMI displays in UN mode, level 0.Verify the following information,(1)   The speed pointer display in white colour
        /// Used in:
        ///     Step 6 in TC-ID: 12.3.4 in 17.3.4 Speed Pointer: Colour of speed pointer in UN mode
        ///     Step 10 in TC-ID: 12.3.4 in 17.3.4 Speed Pointer: Colour of speed pointer in UN mode
        ///     Step 13 in TC-ID: 12.3.4 in 17.3.4 Speed Pointer: Colour of speed pointer in UN mode
        /// </summary>
        public static void
            DMI_displays_in_UN_mode_level_0_Verify_the_following_information_1_The_speed_pointer_display_in_white_colour(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: DMI displays in UN mode, level 0.Verify the following information,(1)   The speed pointer display in orange colour
        /// Used in:
        ///     Step 7 in TC-ID: 12.3.4 in 17.3.4 Speed Pointer: Colour of speed pointer in UN mode
        ///     Step 8 in TC-ID: 12.3.4 in 17.3.4 Speed Pointer: Colour of speed pointer in UN mode
        ///     Step 15 in TC-ID: 12.3.4 in 17.3.4 Speed Pointer: Colour of speed pointer in UN mode
        ///     Step 16 in TC-ID: 12.3.4 in 17.3.4 Speed Pointer: Colour of speed pointer in UN mode
        /// </summary>
        public static void
            DMI_displays_in_UN_mode_level_0_Verify_the_following_information_1_The_speed_pointer_display_in_orange_colour(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: DMI displays in UN mode, level 0.Verify the following information,(1)   The speed pointer display in red colour
        /// Used in:
        ///     Step 9 in TC-ID: 12.3.4 in 17.3.4 Speed Pointer: Colour of speed pointer in UN mode
        ///     Step 17 in TC-ID: 12.3.4 in 17.3.4 Speed Pointer: Colour of speed pointer in UN mode
        /// </summary>
        public static void
            DMI_displays_in_UN_mode_level_0_Verify_the_following_information_1_The_speed_pointer_display_in_red_colour(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: DMI displays in UN mode, level 0.Verify the following information,(1)   The speed pointer display in yellow colour
        /// Used in:
        ///     Step 14 in TC-ID: 12.3.4 in 17.3.4 Speed Pointer: Colour of speed pointer in UN mode
        ///     Step 18 in TC-ID: 12.3.4 in 17.3.4 Speed Pointer: Colour of speed pointer in UN mode
        /// </summary>
        public static void
            DMI_displays_in_UN_mode_level_0_Verify_the_following_information_1_The_speed_pointer_display_in_yellow_colour(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: DMI displays in FS mode, Level 1 with the ST06 symbol at sub-area C6
        /// Used in:
        ///     Step 1 in TC-ID: 12.3.5 in 17.3.5 Speed Pointer: Colour of speed pointer in RV mode
        ///     Step 1 in TC-ID: 14.1 in 19.1 Toggling function: Additional Configuration ‘OFF’ (Default)
        ///     Step 1 in TC-ID: 14.2 in 19.2 Toggling function: Additional Configuration ‘ON’
        ///     Step 1 in TC-ID: 14.3 in 19.3 Toggling function: Additional Configuration ‘TIMER’
        /// </summary>
        public static void DMI_displays_in_FS_mode_Level_1_with_the_ST06_symbol_at_sub_area_C6(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Verify the following information,(1)   Use the log file to confirm that DMI received the packet information EVC-1 with the following condition,MMI_M_WARNING = 8 (Status = OvS, Supervision = CSM) while the value of MMI_V_TRAIN = 861 (31 km/h) which greater than MMI_V_PERMITTED(2)   The speed pointer display in orange colour
        /// Used in:
        ///     Step 3 in TC-ID: 12.3.6 in 17.3.6 Speed Pointer: Colour of speed pointer in SH mode
        ///     Step 3 in TC-ID: 12.3.8 in 17.3.8 Speed Pointer: Colour of speed pointer in OS mode
        /// </summary>
        public static void
            Verify_the_following_information_1_Use_the_log_file_to_confirm_that_DMI_received_the_packet_information_EVC_1_with_the_following_condition_MMI_M_WARNING_8_Status_OvS_Supervision_CSM_while_the_value_of_MMI_V_TRAIN_861_31_kmh_which_greater_than_MMI_V_PERMITTED2_The_speed_pointer_display_in_orange_colour(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: The train speed is force to decrease because of emergency brake is applied by ETCS onboard.Verify the following information,Before train speed is decreased(1)   Use the log file to confirm that DMI received the packet information EVC-1 with the following condition,MMI_M_WARNING = 12 (Status = IntS, Supervision = CSM) while the value of MMI_V_TRAIN = 1000 (36 km/h) which greater than MMI_V_INTERVENTION(2)   The speed pointer display in red colourAfter train speed is decreased(3)   Use the log file to confirm that DMI received the packet information EVC-1 with the following condition,MMI_M_WARNING = 12 (Status = IntS, Supervision = CSM) while the value of MMI_V_TRAIN is lower than MMI_V_INTERVENTION(4)   The speed pointer display in grey colour
        /// Used in:
        ///     Step 5 in TC-ID: 12.3.6 in 17.3.6 Speed Pointer: Colour of speed pointer in SH mode
        ///     Step 5 in TC-ID: 12.3.8 in 17.3.8 Speed Pointer: Colour of speed pointer in OS mode
        /// </summary>
        public static void
            The_train_speed_is_force_to_decrease_because_of_emergency_brake_is_applied_by_ETCS_onboard_Verify_the_following_information_Before_train_speed_is_decreased1_Use_the_log_file_to_confirm_that_DMI_received_the_packet_information_EVC_1_with_the_following_condition_MMI_M_WARNING_12_Status_IntS_Supervision_CSM_while_the_value_of_MMI_V_TRAIN_1000_36_kmh_which_greater_than_MMI_V_INTERVENTION2_The_speed_pointer_display_in_red_colourAfter_train_speed_is_decreased3_Use_the_log_file_to_confirm_that_DMI_received_the_packet_information_EVC_1_with_the_following_condition_MMI_M_WARNING_12_Status_IntS_Supervision_CSM_while_the_value_of_MMI_V_TRAIN_is_lower_than_MMI_V_INTERVENTION4_The_speed_pointer_display_in_grey_colour(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: DMI displays in LS mode, level 1.Verify the following information,(1)   The speed pointer display in grey colour
        /// Used in:
        ///     Step 6 in TC-ID: 12.3.7 in 17.3.7 Speed Pointer: Colour of speed pointer in LS mode
        ///     Step 7 in TC-ID: 12.3.7 in 17.3.7 Speed Pointer: Colour of speed pointer in LS mode
        ///     Step 11 in TC-ID: 12.3.7 in 17.3.7 Speed Pointer: Colour of speed pointer in LS mode
        ///     Step 12 in TC-ID: 12.3.7 in 17.3.7 Speed Pointer: Colour of speed pointer in LS mode
        ///     Step 13 in TC-ID: 12.3.7 in 17.3.7 Speed Pointer: Colour of speed pointer in LS mode
        ///     Step 14 in TC-ID: 12.3.7 in 17.3.7 Speed Pointer: Colour of speed pointer in LS mode
        ///     Step 15 in TC-ID: 12.3.7 in 17.3.7 Speed Pointer: Colour of speed pointer in LS mode
        ///     Step 19 in TC-ID: 12.3.7 in 17.3.7 Speed Pointer: Colour of speed pointer in LS mode
        ///     Step 20 in TC-ID: 12.3.7 in 17.3.7 Speed Pointer: Colour of speed pointer in LS mode
        /// </summary>
        public static void
            DMI_displays_in_LS_mode_level_1_Verify_the_following_information_1_The_speed_pointer_display_in_grey_colour(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: DMI displays in LS mode, level 1.Verify the following information,(1)   The speed pointer display in orange colour
        /// Used in:
        ///     Step 8 in TC-ID: 12.3.7 in 17.3.7 Speed Pointer: Colour of speed pointer in LS mode
        ///     Step 9 in TC-ID: 12.3.7 in 17.3.7 Speed Pointer: Colour of speed pointer in LS mode
        ///     Step 16 in TC-ID: 12.3.7 in 17.3.7 Speed Pointer: Colour of speed pointer in LS mode
        ///     Step 17 in TC-ID: 12.3.7 in 17.3.7 Speed Pointer: Colour of speed pointer in LS mode
        /// </summary>
        public static void
            DMI_displays_in_LS_mode_level_1_Verify_the_following_information_1_The_speed_pointer_display_in_orange_colour(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: DMI displays in LS mode, level 1.Verify the following information,(1)   The speed pointer display in red colour
        /// Used in:
        ///     Step 10 in TC-ID: 12.3.7 in 17.3.7 Speed Pointer: Colour of speed pointer in LS mode
        ///     Step 18 in TC-ID: 12.3.7 in 17.3.7 Speed Pointer: Colour of speed pointer in LS mode
        ///     Step 24 in TC-ID: 12.3.7 in 17.3.7 Speed Pointer: Colour of speed pointer in LS mode
        /// </summary>
        public static void
            DMI_displays_in_LS_mode_level_1_Verify_the_following_information_1_The_speed_pointer_display_in_red_colour(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: DMI displays in LS mode, level 1.Verify the following information,(1)   The speed pointer display in yellow colour
        /// Used in:
        ///     Step 21 in TC-ID: 12.3.7 in 17.3.7 Speed Pointer: Colour of speed pointer in LS mode
        ///     Step 22 in TC-ID: 12.3.7 in 17.3.7 Speed Pointer: Colour of speed pointer in LS mode
        ///     Step 23 in TC-ID: 12.3.7 in 17.3.7 Speed Pointer: Colour of speed pointer in LS mode
        /// </summary>
        public static void
            DMI_displays_in_LS_mode_level_1_Verify_the_following_information_1_The_speed_pointer_display_in_yellow_colour(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: DMI displays in OS mode, level 1
        /// Used in:
        ///     Step 1 in TC-ID: 12.3.8 in 17.3.8 Speed Pointer: Colour of speed pointer in OS mode
        ///     Step 1 in TC-ID: 12.7.3 in 17.7.3 Release Speed Digital for OS mode
        /// </summary>
        public static void DMI_displays_in_OS_mode_level_1(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: DMI displays in OS mode, level 1.Verify the following information,(1)   The speed pointer display in grey colour
        /// Used in:
        ///     Step 6 in TC-ID: 12.3.8 in 17.3.8 Speed Pointer: Colour of speed pointer in OS mode
        ///     Step 12 in TC-ID: 12.3.8 in 17.3.8 Speed Pointer: Colour of speed pointer in OS mode
        ///     Step 13 in TC-ID: 12.3.8 in 17.3.8 Speed Pointer: Colour of speed pointer in OS mode
        ///     Step 21 in TC-ID: 12.3.8 in 17.3.8 Speed Pointer: Colour of speed pointer in OS mode
        /// </summary>
        public static void
            DMI_displays_in_OS_mode_level_1_Verify_the_following_information_1_The_speed_pointer_display_in_grey_colour(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: DMI displays in OS mode, level 1.Verify the following information,(1)   The speed pointer display in white colour
        /// Used in:
        ///     Step 7 in TC-ID: 12.3.8 in 17.3.8 Speed Pointer: Colour of speed pointer in OS mode
        ///     Step 11 in TC-ID: 12.3.8 in 17.3.8 Speed Pointer: Colour of speed pointer in OS mode
        ///     Step 14 in TC-ID: 12.3.8 in 17.3.8 Speed Pointer: Colour of speed pointer in OS mode
        /// </summary>
        public static void
            DMI_displays_in_OS_mode_level_1_Verify_the_following_information_1_The_speed_pointer_display_in_white_colour(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: DMI displays in OS mode, level 1.Verify the following information,(1)   The speed pointer display in orange colour
        /// Used in:
        ///     Step 8 in TC-ID: 12.3.8 in 17.3.8 Speed Pointer: Colour of speed pointer in OS mode
        ///     Step 9 in TC-ID: 12.3.8 in 17.3.8 Speed Pointer: Colour of speed pointer in OS mode
        ///     Step 17 in TC-ID: 12.3.8 in 17.3.8 Speed Pointer: Colour of speed pointer in OS mode
        ///     Step 18 in TC-ID: 12.3.8 in 17.3.8 Speed Pointer: Colour of speed pointer in OS mode
        /// </summary>
        public static void
            DMI_displays_in_OS_mode_level_1_Verify_the_following_information_1_The_speed_pointer_display_in_orange_colour(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: DMI displays in OS mode, level 1.Verify the following information,(1)   The speed pointer display in red colour
        /// Used in:
        ///     Step 10 in TC-ID: 12.3.8 in 17.3.8 Speed Pointer: Colour of speed pointer in OS mode
        ///     Step 19 in TC-ID: 12.3.8 in 17.3.8 Speed Pointer: Colour of speed pointer in OS mode
        ///     Step 24 in TC-ID: 12.3.8 in 17.3.8 Speed Pointer: Colour of speed pointer in OS mode
        /// </summary>
        public static void
            DMI_displays_in_OS_mode_level_1_Verify_the_following_information_1_The_speed_pointer_display_in_red_colour(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: DMI displays in OS mode, level 1.Verify the following information,(1)   The speed pointer display in yellow colour
        /// Used in:
        ///     Step 15 in TC-ID: 12.3.8 in 17.3.8 Speed Pointer: Colour of speed pointer in OS mode
        ///     Step 20 in TC-ID: 12.3.8 in 17.3.8 Speed Pointer: Colour of speed pointer in OS mode
        ///     Step 22 in TC-ID: 12.3.8 in 17.3.8 Speed Pointer: Colour of speed pointer in OS mode
        ///     Step 23 in TC-ID: 12.3.8 in 17.3.8 Speed Pointer: Colour of speed pointer in OS mode
        /// </summary>
        public static void
            DMI_displays_in_OS_mode_level_1_Verify_the_following_information_1_The_speed_pointer_display_in_yellow_colour(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: DMI displays in FS mode, level 1
        /// Used in:
        ///     Step 1 in TC-ID: 12.3.10 in 17.3.10 Speed Pointer: Colour of speed pointer in TR mode and PT mode
        ///     Step 1 in TC-ID: 12.7.4 in 17.7.4 Release Speed Digital: Release speed removal when received an invalid value of EVC-1 or EVC-7
        ///     Step 2 in TC-ID: 18.4.1 in 23.4.1 Geographical Position: General presentation
        ///     Step 1 in TC-ID: 11.2 in 16.2 Sound of Warning Speed Status and Over Speed Status in TSM
        ///     Step 1 in TC-ID: 12.5.4 in 17.5.4 Circular Speed Gauge removal when received an invalid value of EVC-1 and EVC-7
        ///     Step 1 in TC-ID: 14.4 in 19.4 Toggling function: Default state reset for Configuration ‘ON’ when communication loss
        ///     Step 1 in TC-ID: 14.5 in 19.5 Toggling function: Default state reset for Configuration ‘OFF’ when communication loss
        ///     Step 1 in TC-ID: 18.7 in 23.7 Tunnel stopping area track condition
        /// </summary>
        public static void DMI_displays_in_FS_mode_level_1(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Verify the following information,(1)   The Circular Speed Gauge is removed from sub-area B2.Note: The ciruclar speed guage is re-appear when DMI received packet EVC-1 from ETCS onboard
        /// Used in:
        ///     Step 2 in TC-ID: 12.5.4 in 17.5.4 Circular Speed Gauge removal when received an invalid value of EVC-1 and EVC-7
        ///     Step 3 in TC-ID: 12.5.4 in 17.5.4 Circular Speed Gauge removal when received an invalid value of EVC-1 and EVC-7
        ///     Step 4 in TC-ID: 12.5.4 in 17.5.4 Circular Speed Gauge removal when received an invalid value of EVC-1 and EVC-7
        ///     Step 5 in TC-ID: 12.5.4 in 17.5.4 Circular Speed Gauge removal when received an invalid value of EVC-1 and EVC-7
        ///     Step 6 in TC-ID: 12.5.4 in 17.5.4 Circular Speed Gauge removal when received an invalid value of EVC-1 and EVC-7
        /// </summary>
        public static void
            Verify_the_following_information_1_The_Circular_Speed_Gauge_is_removed_from_sub_area_B2_Note_The_ciruclar_speed_guage_is_re_appear_when_DMI_received_packet_EVC_1_from_ETCS_onboard(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: DMI displays in SB mode, level 1. The Driver ID window is displayed
        /// Used in:
        ///     Step 1 in TC-ID: 12.7.1 in 17.7.1 Release Speed: At Sub-area B2 and B6
        ///     Step 1 in TC-ID: 13.1.1 in 18.1.1 Distance to Target  Bar: General Appearance
        /// </summary>
        public static void DMI_displays_in_SB_mode_level_1_The_Driver_ID_window_is_displayed(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Train is standstill
        /// Used in:
        ///     Step 5 in TC-ID: 12.7.1 in 17.7.1 Release Speed: At Sub-area B2 and B6
        ///     Step 9 in TC-ID: 17.8 in 22.8 PA Indication Marker: Sub-Area D7
        /// </summary>
        public static void Train_is_standstill(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Verify the following information,The ‘Slip/Slide’ indication is not displayed on the speed hub. Sound Sinfo is not played
        /// Used in:
        ///     Step 4 in TC-ID: 12.13 in 17.13 Slide Indication
        ///     Step 5 in TC-ID: 12.13 in 17.13 Slide Indication
        /// </summary>
        public static void
            Verify_the_following_information_The_SlipSlide_indication_is_not_displayed_on_the_speed_hub_Sound_Sinfo_is_not_played(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: The speed pointer is displayed with speed =140
        /// Used in:
        ///     Step 2 in TC-ID: 12.14 in 17.14 Slip and Slide are configure to 1 at the same time
        ///     Step 2 in TC-ID: 12.15 in 17.15 Slip and Slide are configure to 0 at the same time
        /// </summary>
        public static void The_speed_pointer_is_displayed_with_speed_140(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Verify that Slip and Slide indicator are not display on DMI
        /// Used in:
        ///     Step 3 in TC-ID: 12.15 in 17.15 Slip and Slide are configure to 0 at the same time
        ///     Step 4 in TC-ID: 12.15 in 17.15 Slip and Slide are configure to 0 at the same time
        ///     Step 5 in TC-ID: 12.15 in 17.15 Slip and Slide are configure to 0 at the same time
        /// </summary>
        public static void Verify_that_Slip_and_Slide_indicator_are_not_display_on_DMI(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: DMI changes from SR to FS mode
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
            EVC102_MMIStatusReport.Check_MMI_M_MODE_READBACK = EVC102_MMIStatusReport.MMI_M_MODE_READBACK.FullSupervision;
            Driver_symbol_displayed(pool, "FS mode", "MO11", "B7", false);
        }

        /// <summary>
        /// Description: DMI displays in SB mode. The Driver ID window is displayed
        /// Used in:
        ///     Step 1 in TC-ID: 13.1.4 in 18.1.4 Distance to Target Digital when the communication between ETCS  Onboard and DMI is lost
        ///     Step 1 in TC-ID: 13.1.5 in 18.1.5 Distance to Target in RV mode
        ///     Step 1 in TC-ID: 15.1.3 in 20.1.3 Mode Symbols in Sub-Area B7 for OS, UN mode
        ///     Step 1 in TC-ID: 17.10.2 in 22.10.2 Zoom PA Function with Scale Up
        /// </summary>
        public static void DMI_displays_in_SB_mode_The_Driver_ID_window_is_displayed(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: DMI remains displays in FS mode
        /// Used in:
        ///     Step 4 in TC-ID: 13.1.4 in 18.1.4 Distance to Target Digital when the communication between ETCS  Onboard and DMI is lost
        ///     Step 5 in TC-ID: 13.1.5 in 18.1.5 Distance to Target in RV mode
        /// </summary>
        public static void DMI_remains_displays_in_FS_mode(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Verify the following information,(1)   The distance to target bar and digital is removed from the DMI.Note: After test scipt file is executed, the distance to target bar and digital is re-appear refer to received packet EVC-1 from ETCS Onboard
        /// Used in:
        ///     Step 4 in TC-ID: 13.1.7.1 in 18.1.7.1 Distance to Target: Appearance of Distance to Target in FS mode
        ///     Step 5 in TC-ID: 13.1.7.1 in 18.1.7.1 Distance to Target: Appearance of Distance to Target in FS mode
        /// </summary>
        public static void
            Verify_the_following_information_1_The_distance_to_target_bar_and_digital_is_removed_from_the_DMI_Note_After_test_scipt_file_is_executed_the_distance_to_target_bar_and_digital_is_re_appear_refer_to_received_packet_EVC_1_from_ETCS_Onboard(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Verify the following information,(1)    Use the log file to confirm that DMI receives the packet information EVC-1 with following variables,MMI_M_WARNING = 2 (Status = NoS, Supervision = PIM)(2)    The distance to target bar is not display in sub-area A3.(3)   The distance to target digital is display in sub-area A2
        /// Used in:
        ///     Step 2 in TC-ID: 13.1.7.2 in 18.1.7.2 Distance to Target: Appearance of Distance to Target in OS mode
        ///     Step 3 in TC-ID: 13.1.7.3 in 18.1.7.3 Distance to Target: Appearance of Distance to Target in SB, SR and SH mode
        /// </summary>
        public static void
            Verify_the_following_information_1_Use_the_log_file_to_confirm_that_DMI_receives_the_packet_information_EVC_1_with_following_variables_MMI_M_WARNING_2_Status_NoS_Supervision_PIM2_The_distance_to_target_bar_is_not_display_in_sub_area_A3_3_The_distance_to_target_digital_is_display_in_sub_area_A2(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Verify the following information,(1)   The distance to target bar and digital is removed from the DMI.        After test scipt file is executed, the distance to target bar and digital is re-appear refer to received packet EVC-1 from ETCS Onboard
        /// Used in:
        ///     Step 3 in TC-ID: 13.1.8 in 18.1.8 Distance to Target Bar: Maximum digit of Distance to Target Digital
        ///     Step 4 in TC-ID: 13.1.8 in 18.1.8 Distance to Target Bar: Maximum digit of Distance to Target Digital
        /// </summary>
        public static void
            Verify_the_following_information_1_The_distance_to_target_bar_and_digital_is_removed_from_the_DMI_After_test_scipt_file_is_executed_the_distance_to_target_bar_and_digital_is_re_appear_refer_to_received_packet_EVC_1_from_ETCS_Onboard(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: DMI displays in RV mode, Level 1.Verify the following information,The objects below are displayed on DMI,White Basic speed HookDistance to target (digital)The objects below are not displayed on DMI,Medium-grey basic speed hookRelease Speed Digital
        /// Used in:
        ///     Step 2 in TC-ID: 14.1 in 19.1 Toggling function: Additional Configuration ‘OFF’ (Default)
        ///     Step 2 in TC-ID: 14.2 in 19.2 Toggling function: Additional Configuration ‘ON’
        ///     Step 2 in TC-ID: 14.3 in 19.3 Toggling function: Additional Configuration ‘TIMER’
        /// </summary>
        public static void
            DMI_displays_in_RV_mode_Level_1_Verify_the_following_information_The_objects_below_are_displayed_on_DMI_White_Basic_speed_HookDistance_to_target_digitalThe_objects_below_are_not_displayed_on_DMI_Medium_grey_basic_speed_hookRelease_Speed_Digital(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Verify the following information,The objects below are not toggled visible/invisible, (always remain the same as the previous step)White Basic speed HookMedium-grey basic speed hookDistance to target (digital)Release Speed Digital
        /// Used in:
        ///     Step 3 in TC-ID: 14.1 in 19.1 Toggling function: Additional Configuration ‘OFF’ (Default)
        ///     Step 10 in TC-ID: 14.1 in 19.1 Toggling function: Additional Configuration ‘OFF’ (Default)
        ///     Step 12 in TC-ID: 14.1 in 19.1 Toggling function: Additional Configuration ‘OFF’ (Default)
        ///     Step 17 in TC-ID: 14.1 in 19.1 Toggling function: Additional Configuration ‘OFF’ (Default)
        ///     Step 19 in TC-ID: 14.1 in 19.1 Toggling function: Additional Configuration ‘OFF’ (Default)
        ///     Step 21 in TC-ID: 14.1 in 19.1 Toggling function: Additional Configuration ‘OFF’ (Default)
        ///     Step 3 in TC-ID: 14.2 in 19.2 Toggling function: Additional Configuration ‘ON’
        ///     Step 12 in TC-ID: 14.2 in 19.2 Toggling function: Additional Configuration ‘ON’
        ///     Step 17 in TC-ID: 14.2 in 19.2 Toggling function: Additional Configuration ‘ON’
        ///     Step 3 in TC-ID: 14.3 in 19.3 Toggling function: Additional Configuration ‘TIMER’
        ///     Step 12 in TC-ID: 14.3 in 19.3 Toggling function: Additional Configuration ‘TIMER’
        ///     Step 17 in TC-ID: 14.3 in 19.3 Toggling function: Additional Configuration ‘TIMER’
        /// </summary>
        public static void
            Verify_the_following_information_The_objects_below_are_not_toggled_visibleinvisible_always_remain_the_same_as_the_previous_stepWhite_Basic_speed_HookMedium_grey_basic_speed_hookDistance_to_target_digitalRelease_Speed_Digital(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: DMI displays in SB mode, Level 1
        /// Used in:
        ///     Step 4 in TC-ID: 14.1 in 19.1 Toggling function: Additional Configuration ‘OFF’ (Default)
        ///     Step 4 in TC-ID: 14.2 in 19.2 Toggling function: Additional Configuration ‘ON’
        ///     Step 4 in TC-ID: 14.3 in 19.3 Toggling function: Additional Configuration ‘TIMER’
        ///     Step 1 in TC-ID: 22.12 in 27.12 Subcategory ‘National’
        /// </summary>
        public static void DMI_displays_in_SB_mode_Level_1(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Verify the following information,The objects below are toggled visible (the same as the previous step)/invisible,White basic speed hookMedium-grey basic speed hookDistance to target (digital)Release Speed Digital
        /// Used in:
        ///     Step 15 in TC-ID: 14.1 in 19.1 Toggling function: Additional Configuration ‘OFF’ (Default)
        ///     Step 15 in TC-ID: 14.2 in 19.2 Toggling function: Additional Configuration ‘ON’
        /// </summary>
        public static void
            Verify_the_following_information_The_objects_below_are_toggled_visible_the_same_as_the_previous_stepinvisible_White_basic_speed_hookMedium_grey_basic_speed_hookDistance_to_target_digitalRelease_Speed_Digital(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: DMI displays in LS mode, Level 1.Verify the following information,The objects below are displayed on DMI,Distance to target (digital)Release Speed DigitalThe objects below are not displayed on DMI,White Basic speed HookMedium-grey basic speed hook
        /// Used in:
        ///     Step 16 in TC-ID: 14.1 in 19.1 Toggling function: Additional Configuration ‘OFF’ (Default)
        ///     Step 16 in TC-ID: 14.3 in 19.3 Toggling function: Additional Configuration ‘TIMER’
        /// </summary>
        public static void
            DMI_displays_in_LS_mode_Level_1_Verify_the_following_information_The_objects_below_are_displayed_on_DMI_Distance_to_target_digitalRelease_Speed_DigitalThe_objects_below_are_not_displayed_on_DMI_White_Basic_speed_HookMedium_grey_basic_speed_hook(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Verify the following information,The white basic speed hook is toggled visible (the same as the visible step)/invisibleThe objects below are not toggled visible/invisible, (always remain the same as the previous step),Medium-grey basic speed hookDistance to target (digital)Release Speed Digital
        /// Used in:
        ///     Step 24 in TC-ID: 14.1 in 19.1 Toggling function: Additional Configuration ‘OFF’ (Default)
        ///     Step 24 in TC-ID: 14.2 in 19.2 Toggling function: Additional Configuration ‘ON’
        /// </summary>
        public static void
            Verify_the_following_information_The_white_basic_speed_hook_is_toggled_visible_the_same_as_the_visible_stepinvisibleThe_objects_below_are_not_toggled_visibleinvisible_always_remain_the_same_as_the_previous_step_Medium_grey_basic_speed_hookDistance_to_target_digitalRelease_Speed_Digital(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Verify the following information,The objects below are not toggled visible/invisible, (always remain the same as the previous step)White basic speed hookMedium-grey basic speed hookDistance to target (digital)Release Speed Digital
        /// Used in:
        ///     Step 10 in TC-ID: 14.2 in 19.2 Toggling function: Additional Configuration ‘ON’
        ///     Step 19 in TC-ID: 14.2 in 19.2 Toggling function: Additional Configuration ‘ON’
        ///     Step 21 in TC-ID: 14.2 in 19.2 Toggling function: Additional Configuration ‘ON’
        ///     Step 10 in TC-ID: 14.3 in 19.3 Toggling function: Additional Configuration ‘TIMER’
        ///     Step 19 in TC-ID: 14.3 in 19.3 Toggling function: Additional Configuration ‘TIMER’
        ///     Step 21 in TC-ID: 14.3 in 19.3 Toggling function: Additional Configuration ‘TIMER’
        /// </summary>
        public static void
            Verify_the_following_information_The_objects_below_are_not_toggled_visibleinvisible_always_remain_the_same_as_the_previous_stepWhite_basic_speed_hookMedium_grey_basic_speed_hookDistance_to_target_digitalRelease_Speed_Digital(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: DMI displays in FS mode, Level 1.Verify the following information,The objects below are displayed on DMI,Distance to target (digital)Release Speed DigitalThe objects below are not displayed on DMI,White Basic speed HookMedium-grey basic speed hook
        /// Used in:
        ///     Step 11 in TC-ID: 14.2 in 19.2 Toggling function: Additional Configuration ‘ON’
        ///     Step 11 in TC-ID: 14.3 in 19.3 Toggling function: Additional Configuration ‘TIMER’
        /// </summary>
        public static void
            DMI_displays_in_FS_mode_Level_1_Verify_the_following_information_The_objects_below_are_displayed_on_DMI_Distance_to_target_digitalRelease_Speed_DigitalThe_objects_below_are_not_displayed_on_DMI_White_Basic_speed_HookMedium_grey_basic_speed_hook(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: DMI displays in PT mode, Level 1. Verify the following information,The objects below are not displayed on DMI,White Basic speed HookMedium-grey basic speed hookDistance to target (digital)Release Speed Digital
        /// Used in:
        ///     Step 20 in TC-ID: 14.2 in 19.2 Toggling function: Additional Configuration ‘ON’
        ///     Step 20 in TC-ID: 14.3 in 19.3 Toggling function: Additional Configuration ‘TIMER’
        /// </summary>
        public static void
            DMI_displays_in_PT_mode_Level_1_Verify_the_following_information_The_objects_below_are_not_displayed_on_DMI_White_Basic_speed_HookMedium_grey_basic_speed_hookDistance_to_target_digitalRelease_Speed_Digital(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: DMI displays the  message “ATP Down Alarm” with sound alarm.Verify the following information,The objects below are not displayed on DMI,White Basic speed HookMedium-grey basic speed hookDistance to target (digital)Release Speed Digital
        /// Used in:
        ///     Step 25 in TC-ID: 14.3 in 19.3 Toggling function: Additional Configuration ‘TIMER’
        ///     Step 3 in TC-ID: 14.4 in 19.4 Toggling function: Default state reset for Configuration ‘ON’ when communication loss
        ///     Step 3 in TC-ID: 14.5 in 19.5 Toggling function: Default state reset for Configuration ‘OFF’ when communication loss
        /// </summary>
        public static void
            DMI_displays_the_message_ATP_Down_Alarm_with_sound_alarm_Verify_the_following_information_The_objects_below_are_not_displayed_on_DMI_White_Basic_speed_HookMedium_grey_basic_speed_hookDistance_to_target_digitalRelease_Speed_Digital(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: DMI displays in OS mode, Level 1
        /// Used in:
        ///     Step 2 in TC-ID: 14.4 in 19.4 Toggling function: Default state reset for Configuration ‘ON’ when communication loss
        ///     Step 2 in TC-ID: 14.5 in 19.5 Toggling function: Default state reset for Configuration ‘OFF’ when communication loss
        /// </summary>
        public static void DMI_displays_in_OS_mode_Level_1(SignalPool pool)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
        /// Description: Verify the following information,(1)   There is no symbol displayed on sub-area B7
        /// Used in:
        ///     Step 1 in TC-ID: 15.1.7 in 20.1.7 Mode Symbols for unused value of variable OBU_TR_M_MODE
        ///     Step 2 in TC-ID: 15.1.7 in 20.1.7 Mode Symbols for unused value of variable OBU_TR_M_MODE
        ///     Step 3 in TC-ID: 15.1.7 in 20.1.7 Mode Symbols for unused value of variable OBU_TR_M_MODE
        ///     Step 4 in TC-ID: 15.1.7 in 20.1.7 Mode Symbols for unused value of variable OBU_TR_M_MODE
        ///     Step 5 in TC-ID: 15.1.7 in 20.1.7 Mode Symbols for unused value of variable OBU_TR_M_MODE
        ///     Step 6 in TC-ID: 15.1.7 in 20.1.7 Mode Symbols for unused value of variable OBU_TR_M_MODE
        ///     Step 7 in TC-ID: 15.1.7 in 20.1.7 Mode Symbols for unused value of variable OBU_TR_M_MODE
        ///     Step 8 in TC-ID: 15.1.7 in 20.1.7 Mode Symbols for unused value of variable OBU_TR_M_MODE
        /// </summary>
        public static void Verify_the_following_information_1_There_is_no_symbol_displayed_on_sub_area_B7(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: DMI displays in ATB STM mode, Level NTC
        /// Used in:
        ///     Step 4 in TC-ID: 15.2.9 in 20.2.9 NTC Level :Announcement symbol in Sub-Area C1.
        ///     Step 1 in TC-ID: 15.2.11 in 20.2.11 ETCS Level: ETCS Level Transitions by receiving data packet from ETCS Onboard (LNTC ->L1)
        ///     Step 1 in TC-ID: 15.2.12 in 20.2.12 ETCS Level: ETCS Level Transitions by receiving data packet from ETCS Onboard (LNTC ->L2)
        ///     Step 1 in TC-ID: 15.2.13 in 20.2.13 ETCS Level: ETCS Level Transitions by receiving data packet from ETCS Onboard (LNTC ->L3)
        /// </summary>
        public static void DMI_displays_in_ATB_STM_mode_Level_NTC(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Verify the following information,(1)   The display information in sub-area E5-E9 are not changed
        /// Used in:
        ///     Step 10 in TC-ID: 15.3.2 in 20.3.2 Driver Messages: Processing of incoming Driver Messages
        ///     Step 13 in TC-ID: 15.3.2 in 20.3.2 Driver Messages: Processing of incoming Driver Messages
        /// </summary>
        public static void
            Verify_the_following_information_1_The_display_information_in_sub_area_E5_E9_are_not_changed(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: DMI displays the message “ATP Down Alarm” with sound alarm.Verify the following information,(1)   The non-acknowledgeable message list is flushed, no driver message display in area E5-E9
        /// Used in:
        ///     Step 7 in TC-ID: 15.3.3 in 20.3.3 Driver Messages: Maximum of non-acknowledgeable Text Messages
        ///     Step 9 in TC-ID: 15.3.3 in 20.3.3 Driver Messages: Maximum of non-acknowledgeable Text Messages
        /// </summary>
        public static void
            DMI_displays_the_message_ATP_Down_Alarm_with_sound_alarm_Verify_the_following_information_1_The_non_acknowledgeable_message_list_is_flushed_no_driver_message_display_in_area_E5_E9(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Verify the following information,There is no symbol display in sub-area B3-B5
        /// Used in:
        ///     Step 1 in TC-ID: 15.6.2 in 20.6.2 Level Crossing “not protected” Indication: Packet Handling
        ///     Step 2 in TC-ID: 15.6.2 in 20.6.2 Level Crossing “not protected” Indication: Packet Handling
        /// </summary>
        public static void Verify_the_following_information_There_is_no_symbol_display_in_sub_area_B3_B5(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: DMI displays in SR mode, level 2
        /// Used in:
        ///     Step 1 in TC-ID: 16.1 in 21.1 TAF Question Box
        ///     Step 1 in TC-ID: 16.2 in 21.2 TAF Question Box: Display of TAF Question box instead of the planning area information
        ///     Step 1 in TC-ID: 17.1.4 in 22.1.4 Planning Area forced into background by TAF Question box
        /// </summary>
        public static void DMI_displays_in_SR_mode_level_2(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: DMI changes from SR mode to FS mode, level 2
        /// Used in:
        ///     Step 2 in TC-ID: 16.1 in 21.1 TAF Question Box
        ///     Step 2 in TC-ID: 16.2 in 21.2 TAF Question Box: Display of TAF Question box instead of the planning area information
        ///     Step 2 in TC-ID: 17.1.4 in 22.1.4 Planning Area forced into background by TAF Question box
        ///     Step 2 in TC-ID: 17.10.1 in 22.10.1 Zoom PA Function: General appearance
        /// </summary>
        public static void DMI_changes_from_SR_mode_to_FS_mode_level_2(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: DMI changes from FS mode to OS mode, level 2
        /// Used in:
        ///     Step 3 in TC-ID: 16.1 in 21.1 TAF Question Box
        ///     Step 3 in TC-ID: 16.2 in 21.2 TAF Question Box: Display of TAF Question box instead of the planning area information
        ///     Step 3 in TC-ID: 17.1.4 in 22.1.4 Planning Area forced into background by TAF Question box
        ///     Step 3 in TC-ID: 17.10.1 in 22.10.1 Zoom PA Function: General appearance
        /// </summary>
        public static void DMI_changes_from_FS_mode_to_OS_mode_level_2(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: The Main window is displayed
        /// Used in:
        ///     Step 4 in TC-ID: 16.1 in 21.1 TAF Question Box
        ///     Step 7 in TC-ID: 33.1 in 36.1 The relationship between parent and child windows (1)
        /// </summary>
        public static void The_Main_window_is_displayed(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Verify that the Planning Area is not displayed on DMI
        /// Used in:
        ///     Step 4 in TC-ID: 17.1.2 in 22.1.2 Planning Area is suppressed in Level 1 and OS mode
        ///     Step 6 in TC-ID: 17.1.2 in 22.1.2 Planning Area is suppressed in Level 1 and OS mode
        /// </summary>
        public static void Verify_that_the_Planning_Area_is_not_displayed_on_DMI(SignalPool pool)
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
        /// Description: Verify the following information(1)   Use the log file to confirm that DMI received packet information MMI_TRACK_CONDITIONS (EVC-32) with the following variables,MMI_Q_TRACKCOND_STEP = 4MMI_NID_TRACKCOND = same value with expected result No.2 of step 7
        /// Used in:
        ///     Step 9 in TC-ID: 17.4.1 in 22.4.1 PA Track Condition: Non stopping area in Sub-Area D2 and B3
        ///     Step 9 in TC-ID: 17.4.2 in 22.4.2 PA Track Condition: Sound Horn in Sub-Area D2 and B3
        /// </summary>
        public static void
            Verify_the_following_information1_Use_the_log_file_to_confirm_that_DMI_received_packet_information_MMI_TRACK_CONDITIONS_EVC_32_with_the_following_variables_MMI_Q_TRACKCOND_STEP_4MMI_NID_TRACKCOND_same_value_with_expected_result_No_2_of_step_7(SignalPool pool)
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
        /// Description: Mode remins in FS mode
        /// Used in:
        ///     Step 3 in TC-ID: 17.4.2 in 22.4.2 PA Track Condition: Sound Horn in Sub-Area D2 and B3
        ///     Step 3 in TC-ID: 17.4.3 in 22.4.3 PA Track Condition:  Lower Pantograph in Sub-Area D2 and B3
        ///     Step 3 in TC-ID: 17.4.4 in 22.4.4 PA Track Condition: Radio Hole in Sub-Area D2 and B3
        ///     Step 3 in TC-ID: 17.4.5 in 22.4.5 PA Track Condition: Air Tightness in Sub-Area D2 and B3
        ///     Step 5 in TC-ID: 17.4.17 in 22.4.17 PA Track Condition: First symbol prevails over the next coming symbol
        /// </summary>
        public static void Mode_remins_in_FS_mode(SignalPool pool)
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
            Verify_the_following_information1_Use_the_log_file_to_confirm_that_DMI_received_packet_information_MMI_TRACK_CONDITIONS_EVC_32_with_the_following_variables_MMI_Q_TRACKCOND_STEP_4MMI_NID_TRACKCOND_Same_value_with_expected_result_No_2_of_step_7(SignalPool pool)
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
        /// Description: Verify that the value of PA Gradient Profile is not change, still display PA Gradient Profiles value = 11 and 22
        /// Used in:
        ///     Step 10 in TC-ID: 17.5.4 in 22.5.4 PA Gradient Profile:  Invalid Information Ignoring
        ///     Step 11 in TC-ID: 17.5.4 in 22.5.4 PA Gradient Profile:  Invalid Information Ignoring
        ///     Step 12 in TC-ID: 17.5.4 in 22.5.4 PA Gradient Profile:  Invalid Information Ignoring
        ///     Step 13 in TC-ID: 17.5.4 in 22.5.4 PA Gradient Profile:  Invalid Information Ignoring
        /// </summary>
        public static void
            Verify_that_the_value_of_PA_Gradient_Profile_is_not_change_still_display_PA_Gradient_Profiles_value_11_and_22(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: The PA Speed Profile segments are reappeared
        /// Used in:
        ///     Step 9 in TC-ID: 17.6.1 in 22.6.1 PA Speed Profile Discontinuity: Display in sub-area D6 and D7
        ///     Step 10 in TC-ID: 17.7.1 in 22.7.1 PA Speed Profile (PASP): Display in sub-area D7 and D8
        /// </summary>
        public static void The_PA_Speed_Profile_segments_are_reappeared(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: DMI changes from SR to FS mode. The Planning Area is displayed
        /// Used in:
        ///     Step 1 in TC-ID: 17.6.2 in 22.6.2 PA Speed Profile Discontinuity: Information updating
        ///     Step 1 in TC-ID: 17.7.1 in 22.7.1 PA Speed Profile (PASP): Display in sub-area D7 and D8
        /// </summary>
        public static void DMI_changes_from_SR_to_FS_mode_The_Planning_Area_is_displayed(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Verify the following information, An information of PA in area D are not updated
        /// Used in:
        ///     Step 4 in TC-ID: 17.6.2 in 22.6.2 PA Speed Profile Discontinuity: Information updating
        ///     Step 5 in TC-ID: 17.6.2 in 22.6.2 PA Speed Profile Discontinuity: Information updating
        ///     Step 6 in TC-ID: 17.6.2 in 22.6.2 PA Speed Profile Discontinuity: Information updating
        /// </summary>
        public static void Verify_the_following_information_An_information_of_PA_in_area_D_are_not_updated(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Verify the indication marker shall not be shown in area D7
        /// Used in:
        ///     Step 2 in TC-ID: 17.8 in 22.8 PA Indication Marker: Sub-Area D7
        ///     Step 6 in TC-ID: 17.8 in 22.8 PA Indication Marker: Sub-Area D7
        ///     Step 8 in TC-ID: 17.8 in 22.8 PA Indication Marker: Sub-Area D7
        /// </summary>
        public static void Verify_the_indication_marker_shall_not_be_shown_in_area_D7(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: DMI is power off
        /// Used in:
        ///     Step 4 in TC-ID: 17.9.2 in 22.9.2 Hide PA Function is configured ‘ON’ with reboot DMI
        ///     Step 7 in TC-ID: 17.9.3 in 22.9.3 Hide PA Function is configured ‘OFF’ with reboot DMI
        ///     Step 5 in TC-ID: 17.9.4 in 22.9.4 Hide PA Function is configured ‘STORED’ with reboot DMI
        ///     Step 5 in TC-ID: 17.9.5 in 22.9.6 Hide PA Function is configured ‘TIMER’ with reboot DMI
        /// </summary>
        public static void DMI_is_power_off(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: DMI displays the Default window
        /// Used in:
        ///     Step 1 in TC-ID: 17.9.3 in 22.9.3 Hide PA Function is configured ‘OFF’ with reboot DMI
        ///     Step 8 in TC-ID: 34.7 in 37.7 Dialogue Sequence of Settings window
        /// </summary>
        public static void DMI_displays_the_Default_window(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: The DMI displays the default window
        /// Used in:
        ///     Step 1 in TC-ID: 17.9.6 in 22.9.5 Hide PA Function is configured ‘ON’ with reactivated Cabin A
        ///     Step 1 in TC-ID: 17.9.7 in 22.9.7 Hide PA Function is configured ‘OFF’ with reactivated Cabin A
        ///     Step 1 in TC-ID: 17.9.8 in 22.9.8 Hide PA Function is configured ‘STORED’ with reactivated Cabin A
        ///     Step 1 in TC-ID: 17.9.9 in 22.9.9 Hide PA Function is configured ‘TIMER’ with reactivated Cabin A
        /// </summary>
        public static void The_DMI_displays_the_default_window(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: The Planning area is disappeared from the main area D of the DMI
        /// Used in:
        ///     Step 4 in TC-ID: 17.9.6 in 22.9.5 Hide PA Function is configured ‘ON’ with reactivated Cabin A
        ///     Step 8 in TC-ID: 17.9.6 in 22.9.5 Hide PA Function is configured ‘ON’ with reactivated Cabin A
        /// </summary>
        public static void The_Planning_area_is_disappeared_from_the_main_area_D_of_the_DMI(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: The train is at standstill.DMI is displays in SB mode
        /// Used in:
        ///     Step 5 in TC-ID: 17.9.6 in 22.9.5 Hide PA Function is configured ‘ON’ with reactivated Cabin A
        ///     Step 5 in TC-ID: 17.9.7 in 22.9.7 Hide PA Function is configured ‘OFF’ with reactivated Cabin A
        ///     Step 5 in TC-ID: 17.9.8 in 22.9.8 Hide PA Function is configured ‘STORED’ with reactivated Cabin A
        ///     Step 5 in TC-ID: 17.9.9 in 22.9.9 Hide PA Function is configured ‘TIMER’ with reactivated Cabin A
        /// </summary>
        public static void The_train_is_at_standstill_DMI_is_displays_in_SB_mode(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: The Planning area is disappeared and hidden from main area D for 60s.After 60s the planning area is displayed.Verify that the Hide PA button is displayed at sub-area D14 on the planning area
        /// Used in:
        ///     Step 10 in TC-ID: 17.9.5 in 22.9.6 Hide PA Function is configured ‘TIMER’ with reboot DMI
        ///     Step 11 in TC-ID: 17.9.5 in 22.9.6 Hide PA Function is configured ‘TIMER’ with reboot DMI
        ///     Step 12 in TC-ID: 17.9.5 in 22.9.6 Hide PA Function is configured ‘TIMER’ with reboot DMI
        /// </summary>
        public static void
            The_Planning_area_is_disappeared_and_hidden_from_main_area_D_for_60s_After_60s_the_planning_area_is_displayed_Verify_that_the_Hide_PA_button_is_displayed_at_sub_area_D14_on_the_planning_area(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: DMI displays in FS mode, Level 1.There is no PA display on DMI
        /// Used in:
        ///     Step 3 in TC-ID: 17.9.7 in 22.9.7 Hide PA Function is configured ‘OFF’ with reactivated Cabin A
        ///     Step 7 in TC-ID: 17.9.8 in 22.9.8 Hide PA Function is configured ‘STORED’ with reactivated Cabin A
        /// </summary>
        public static void DMI_displays_in_FS_mode_Level_1_There_is_no_PA_display_on_DMI(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: The Planning area is disappeared from the area D of the DMI
        /// Used in:
        ///     Step 4 in TC-ID: 17.9.8 in 22.9.8 Hide PA Function is configured ‘STORED’ with reactivated Cabin A
        ///     Step 4 in TC-ID: 17.9.9 in 22.9.9 Hide PA Function is configured ‘TIMER’ with reactivated Cabin A
        ///     Step 8 in TC-ID: 17.9.9 in 22.9.9 Hide PA Function is configured ‘TIMER’ with reactivated Cabin A
        /// </summary>
        public static void The_Planning_area_is_disappeared_from_the_area_D_of_the_DMI(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: The Planning Information is disappeared from main area D
        /// Used in:
        ///     Step 6 in TC-ID: 17.9.10 (Default Configuration) in 22.9.10 Hide PA Function with the communication loss between ETCS Onboard and DMI
        ///     Step 8 in TC-ID: 17.9.10 (Default Configuration) in 22.9.10 Hide PA Function with the communication loss between ETCS Onboard and DMI
        /// </summary>
        public static void The_Planning_Information_is_disappeared_from_main_area_D(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: The planning area is reappeared by this activation
        /// Used in:
        ///     Step 6 in TC-ID: 17.9.11 in 22.9.11 Hide PA Function configured ‘STORED’ with re-activate cabin
        ///     Step 12 in TC-ID: 17.9.11 in 22.9.11 Hide PA Function configured ‘STORED’ with re-activate cabin
        ///     Step 19 in TC-ID: 17.9.11 in 22.9.11 Hide PA Function configured ‘STORED’ with re-activate cabin
        /// </summary>
        public static void The_planning_area_is_reappeared_by_this_activation(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Train is at standstillThe Planning area is disappeared from DMI
        /// Used in:
        ///     Step 8 in TC-ID: 17.9.11 in 22.9.11 Hide PA Function configured ‘STORED’ with re-activate cabin
        ///     Step 14 in TC-ID: 17.9.11 in 22.9.11 Hide PA Function configured ‘STORED’ with re-activate cabin
        /// </summary>
        public static void Train_is_at_standstillThe_Planning_area_is_disappeared_from_DMI(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: The train is moving forward, position is increase.The speed pointer displays the current speed
        /// Used in:
        ///     Step 4 in TC-ID: 18.1.1.1.1 in 23.1.1.1.1 Concise Visualization
        ///     Step 3 in TC-ID: 18.1.1.1.2 in 23.1.1.1.2 Verbose Visualization
        /// </summary>
        public static void
            The_train_is_moving_forward_position_is_increase_The_speed_pointer_displays_the_current_speed(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: DMI displays in FS mode, Level 2
        /// Used in:
        ///     Step 5 in TC-ID: 18.1.1.1.1 in 23.1.1.1.1 Concise Visualization
        ///     Step 4 in TC-ID: 18.1.1.1.2 in 23.1.1.1.2 Verbose Visualization
        /// </summary>
        public static void DMI_displays_in_FS_mode_Level_2(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: No symbol display in sub area E1
        /// Used in:
        ///     Step 9 in TC-ID: 18.1.1.1.1 in 23.1.1.1.1 Concise Visualization
        ///     Step 10 in TC-ID: 18.1.1.1.1 in 23.1.1.1.1 Concise Visualization
        /// </summary>
        public static void No_symbol_display_in_sub_area_E1(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: The symbol ‘DR03’ displays in sub-area G12
        /// Used in:
        ///     Step 13 in TC-ID: 18.4.1 in 23.4.1 Geographical Position: General presentation
        ///     Step 1 in TC-ID: 18.4.3 in 23.4.3 Geographical Position: Additional requirements
        ///     Step 4 in TC-ID: 18.4.3 in 23.4.3 Geographical Position: Additional requirements
        ///     Step 9 in TC-ID: 18.4.3 in 23.4.3 Geographical Position: Additional requirements
        /// </summary>
        public static void The_symbol_DR03_displays_in_sub_area_G12(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: The Train Running Number window is displayed
        /// Used in:
        ///     Step 1 in TC-ID: 18.5 in 23.5 Train Running Number
        ///     Step 17 in TC-ID: 22.18 in Train Running Number window
        ///     Step 4 in TC-ID: 33.1 in 36.1 The relationship between parent and child windows (1)
        /// </summary>
        public static void The_Train_Running_Number_window_is_displayed(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Verify the following information,(1)   There is no symbol display in sub-area B3-B5
        /// Used in:
        ///     Step 1 in TC-ID: 18.6.1 in 23.6.1 Visualise of the Track Conditions Symbols
        ///     Step 2 in TC-ID: 18.6.1 in 23.6.1 Visualise of the Track Conditions Symbols
        /// </summary>
        public static void Verify_the_following_information_1_There_is_no_symbol_display_in_sub_area_B3_B5(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Verify the following information,(1)    The display in sub-area B3-B5 still not change because of all areas are already displaying symbols
        /// Used in:
        ///     Step 6 in TC-ID: 18.6.1 in 23.6.1 Visualise of the Track Conditions Symbols
        ///     Step 8 in TC-ID: 18.6.1 in 23.6.1 Visualise of the Track Conditions Symbols
        /// </summary>
        public static void
            Verify_the_following_information_1_The_display_in_sub_area_B3_B5_still_not_change_because_of_all_areas_are_already_displaying_symbols(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Received packet information is stored to internal memory, the symbol in sub-area B3-B5 still not changed
        /// Used in:
        ///     Step 2 in TC-ID: 18.6.2 in 23.6.2 Maximum of Track Conditions in internal memory
        ///     Step 3 in TC-ID: 18.6.2 in 23.6.2 Maximum of Track Conditions in internal memory
        ///     Step 4 in TC-ID: 18.6.2 in 23.6.2 Maximum of Track Conditions in internal memory
        ///     Step 5 in TC-ID: 18.6.2 in 23.6.2 Maximum of Track Conditions in internal memory
        ///     Step 6 in TC-ID: 18.6.2 in 23.6.2 Maximum of Track Conditions in internal memory
        ///     Step 7 in TC-ID: 18.6.2 in 23.6.2 Maximum of Track Conditions in internal memory
        ///     Step 8 in TC-ID: 18.6.2 in 23.6.2 Maximum of Track Conditions in internal memory
        ///     Step 9 in TC-ID: 18.6.2 in 23.6.2 Maximum of Track Conditions in internal memory
        ///     Step 11 in TC-ID: 18.6.2 in 23.6.2 Maximum of Track Conditions in internal memory
        /// </summary>
        public static void
            Received_packet_information_is_stored_to_internal_memory_the_symbol_in_sub_area_B3_B5_still_not_changed(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Verify the following information,(1)  The symbol TC03 is re-appear if ETCS Onboard is re-transmit EVC-32 to DMI
        /// Used in:
        ///     Step 43 in TC-ID: 18.6.2 in 23.6.2 Maximum of Track Conditions in internal memory
        ///     Step 45 in TC-ID: 18.6.2 in 23.6.2 Maximum of Track Conditions in internal memory
        /// </summary>
        public static void
            Verify_the_following_information_1_The_symbol_TC03_is_re_appear_if_ETCS_Onboard_is_re_transmit_EVC_32_to_DMI(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Verify the following information,(1)  The symbol TC37 is removed from DMI
        /// Used in:
        ///     Step 11 in TC-ID: 18.7 in 23.7 Tunnel stopping area track condition
        ///     Step 13 in TC-ID: 18.7 in 23.7 Tunnel stopping area track condition
        /// </summary>
        public static void Verify_the_following_information_1_The_symbol_TC37_is_removed_from_DMI(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Verify the following information,(1)  The symbol TC37 is resume to display on DMI
        /// Used in:
        ///     Step 12 in TC-ID: 18.7 in 23.7 Tunnel stopping area track condition
        ///     Step 14 in TC-ID: 18.7 in 23.7 Tunnel stopping area track condition
        /// </summary>
        public static void Verify_the_following_information_1_The_symbol_TC37_is_resume_to_display_on_DMI(SignalPool pool)
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
        /// Description: Verify the following information,The ‘Main’, ‘Override’, Data View’ and ‘Special’ buttons are invisible.The ‘Settings’ button is visible
        /// Used in:
        ///     Step 1 in TC-ID: 22.1.1 in 27.1.1 Sub-Level Window: General appearances
        ///     Step 2 in TC-ID: 22.1.1 in 27.1.1 Sub-Level Window: General appearances
        /// </summary>
        public static void
            Verify_the_following_information_The_Main_Override_Data_View_and_Special_buttons_are_invisible_The_Settings_button_is_visible(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Verify the following information,DMI displays Main window
        /// Used in:
        ///     Step 8 in TC-ID: 22.1.1 in 27.1.1 Sub-Level Window: General appearances
        ///     Step 17 in TC-ID: 22.5.1 in 27.5.1 Level Selection Window: General appearance
        /// </summary>
        public static void Verify_the_following_information_DMI_displays_Main_window(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: DMI displays Default window
        /// Used in:
        ///     Step 9 in TC-ID: 22.1.1 in 27.1.1 Sub-Level Window: General appearances
        ///     Step 14 in TC-ID: 22.1.1 in 27.1.1 Sub-Level Window: General appearances
        ///     Step 19 in TC-ID: 22.1.1 in 27.1.1 Sub-Level Window: General appearances
        ///     Step 24 in TC-ID: 22.1.1 in 27.1.1 Sub-Level Window: General appearances
        ///     Step 4 in TC-ID: 22.20.2 in 27.20.2 Override window in SB mode
        ///     Step 4 in TC-ID: 32.1 in 35.1 Lock Screen
        ///     Step 9 in TC-ID: 33.3 in 36.2 The relationship between parent and child windows (2)
        ///     Step 6 in TC-ID: 34.7 in 37.7 Dialogue Sequence of Settings window
        /// </summary>
        public static void DMI_displays_Default_window(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: DMI displays Language window
        /// Used in:
        ///     Step 29 in TC-ID: 22.1.1 in 27.1.1 Sub-Level Window: General appearances
        ///     Step 5 in TC-ID: 22.21 in 27.21 Settings Window
        ///     Step 12 in TC-ID: 34.1.4 in 37.1.4.1.1 Data entry/validation process when enabling conditions not fullfilled: Level 1
        ///     Step 14 in TC-ID: 34.7 in 37.7 Dialogue Sequence of Settings window
        /// </summary>
        public static void DMI_displays_Language_window(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Verify the following information,The buttons ‘Main’, ‘Override’, Data View’ and ‘Special’ buttons are invisibled in any other state.The ‘Settings’ button is still visible
        /// Used in:
        ///     Step 34 in TC-ID: 22.1.1 in 27.1.1 Sub-Level Window: General appearances
        ///     Step 35 in TC-ID: 22.1.1 in 27.1.1 Sub-Level Window: General appearances
        /// </summary>
        public static void
            Verify_the_following_information_The_buttons_Main_Override_Data_View_and_Special_buttons_are_invisibled_in_any_other_state_The_Settings_button_is_still_visible(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: DMI displays in PLZB STM mode
        /// Used in:
        ///     Step 1 in TC-ID: 22.1.2 in 27.1.2 ETCS Specfic submenus and SN sub menus
        ///     Step 10 in TC-ID: 35.2 in 38.2 NTC System Status Messages
        /// </summary>
        public static void DMI_displays_in_PLZB_STM_mode(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Verify the following information,While press and hold button less than 2 secThe ‘Click’ sound is played once.The state of button is changed to ‘Pressed’.The state ‘pressed’ and ‘enabled’ are switched repeatly while button is pressed. Use the log file to confirm that DMI sends EVC-101 with variable MMI_T_BUTTONEVENT and MMI_Q_BUTTON = 1 (pressed).While press and hold button over 2 secThe state of button is changed to ‘Pressed’ and without toggle
        /// Used in:
        ///     Step 9 in TC-ID: 7.1 in 27.2 Main window
        ///     Step 3 in TC-ID: 22.10 in 27.10 Special window
        /// </summary>
        public static void
            Verify_the_following_information_While_press_and_hold_button_less_than_2_secThe_Click_sound_is_played_once_The_state_of_button_is_changed_to_Pressed_The_state_pressed_and_enabled_are_switched_repeatly_while_button_is_pressed_Use_the_log_file_to_confirm_that_DMI_sends_EVC_101_with_variable_MMI_T_BUTTONEVENT_and_MMI_Q_BUTTON_1_pressed_While_press_and_hold_button_over_2_secThe_state_of_button_is_changed_to_Pressed_and_without_toggle(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Verify the following information,The ‘Shunting’ button turns to the ‘Enabled’ state without a sound
        /// Used in:
        ///     Step 10 in TC-ID: 7.1 in 27.2 Main window
        ///     Step 11 in TC-ID: 7.1 in 27.2 Main window
        /// </summary>
        public static void
            Verify_the_following_information_The_Shunting_button_turns_to_the_Enabled_state_without_a_sound(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: While press and hold button less than 2 secThe state ‘pressed’ and ‘enabled’ are switched repeatly while button is pressed without a sound. While press and hold button over 2 secThe state of button is changed to ‘Pressed’ and without toggle
        /// Used in:
        ///     Step 12 in TC-ID: 7.1 in 27.2 Main window
        ///     Step 12 in TC-ID: 22.8.3.1 in 27.8.3.1 RBC Contact window: General appearance
        ///     Step 6 in TC-ID: 22.10 in 27.10 Special window
        /// </summary>
        public static void
            While_press_and_hold_button_less_than_2_secThe_state_pressed_and_enabled_are_switched_repeatly_while_button_is_pressed_without_a_sound_While_press_and_hold_button_over_2_secThe_state_of_button_is_changed_to_Pressed_and_without_toggle(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: DMI still displays Level window.No sound ‘Click’ is played
        /// Used in:
        ///     Step 5 in TC-ID: 22.5.1 in 27.5.1 Level Selection Window: General appearance
        ///     Step 6 in TC-ID: 22.5.1 in 27.5.1 Level Selection Window: General appearance
        /// </summary>
        public static void DMI_still_displays_Level_window_No_sound_Click_is_played(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: DMI displays Driver ID window in SB mode
        /// Used in:
        ///     Step 1 in TC-ID: 22.5.4  in 27.5.4 Level Selection window: 8 STMs handling
        ///     Step 1 in TC-ID: 35.2 in 38.2 NTC System Status Messages
        ///     Step 7 in TC-ID: 35.2 in 38.2 NTC System Status Messages
        ///     Step 1 in TC-ID: 17.1.3 in 20.1.3 
        /// </summary>
        public static void DMI_displays_Driver_ID_window_in_SB_mode(SignalPool pool)
        {
            pool.WaitForVerification("Is the Driver Id window displayed in SB mode?");
            EVC102_MMIStatusReport.Check_MMI_M_MODE_READBACK = EVC102_MMIStatusReport.MMI_M_MODE_READBACK.StandBy;
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
            Verify_the_following_information_While_press_and_hold_button_less_than_1_5_secSound_Click_is_played_once_The_state_of_button_is_changed_to_Pressed_and_immediately_back_to_Enabled_state_The_last_character_is_removed_from_an_input_field_after_pressing_the_button_While_press_and_hold_button_over_1_5_secThe_state_pressed_and_released_are_switched_repeatly_while_button_is_pressed_and_the_characters_are_removed_from_an_input_field_repeatly_refer_to_pressed_state_The_sound_Click_is_played_repeatly_while_button_is_pressed(SignalPool pool)
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
            Verify_the_following_information_1_The_state_of_an_input_field_is_changed_to_Enabled_the_border_of_button_is_shown_without_a_sound(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: DMI displays the Maintenance window
        /// Used in:
        ///     Step 14 in TC-ID: 22.6.1 in 27.6.1 Password window
        ///     Step 15 in TC-ID: 22.6.1 in 27.6.1 Password window
        ///     Step 14 in TC-ID: 22.6.2 in 27.6.2 Maintenance window
        ///     Step 18 in TC-ID: 22.6.3.1 in 27.6.3.1 Wheel diameter window: General apearance
        ///     Step 16 in TC-ID: 22.6.5.1 in 27.6.5.1 Radar window: General appearance
        /// </summary>
        public static void DMI_displays_the_Maintenance_window(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: DMI displays the Settings window
        /// Used in:
        ///     Step 16 in TC-ID: 22.6.1 in 27.6.1 Password window
        ///     Step 17 in TC-ID: 22.6.1 in 27.6.1 Password window
        /// </summary>
        public static void DMI_displays_the_Settings_window(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: The ‘Maintenance’ button is disabled
        /// Used in:
        ///     Step 4 in TC-ID: 22.6.2 in 27.6.2 Maintenance window
        ///     Step 7 in TC-ID: 22.6.2 in 27.6.2 Maintenance window
        /// </summary>
        public static void The_Maintenance_button_is_disabled(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Verify the following information, (1)   DMI displays Setting window
        /// Used in:
        ///     Step 16 in TC-ID: 22.6.2 in 27.6.2 Maintenance window
        ///     Step 5 in TC-ID: 22.14 in 27.14 System Version window
        /// </summary>
        public static void Verify_the_following_information_1_DMI_displays_Setting_window(SignalPool pool)
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
        public static void Verify_the_following_information_The_state_of_released_button_is_changed_to_enabled(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: See the expected results of Step 2 – Step 3 and the following additional information,The pressed key is added in an input field immediately. The cursor is jumped to next position after entered the character immediately
        /// Used in:
        ///     Step 4 in TC-ID: 22.6.3.1 in 27.6.3.1 Wheel diameter window: General apearance
        ///     Step 4 in TC-ID: 22.6.5.1 in 27.6.5.1 Radar window: General appearance
        /// </summary>
        public static void
            See_the_expected_results_of_Step_2_Step_3_and_the_following_additional_information_The_pressed_key_is_added_in_an_input_field_immediately_The_cursor_is_jumped_to_next_position_after_entered_the_character_immediately(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: See the expected results of Step 2 – Step 6 and the following additional information,The pressed key is added in an input field immediately. The cursor is jumped to next position after entered the character immediately
        /// Used in:
        ///     Step 8 in TC-ID: 22.6.3.1 in 27.6.3.1 Wheel diameter window: General apearance
        ///     Step 10 in TC-ID: 22.6.3.1 in 27.6.3.1 Wheel diameter window: General apearance
        ///     Step 8 in TC-ID: 22.6.5.1 in 27.6.5.1 Radar window: General appearance
        /// </summary>
        public static void
            See_the_expected_results_of_Step_2_Step_6_and_the_following_additional_information_The_pressed_key_is_added_in_an_input_field_immediately_The_cursor_is_jumped_to_next_position_after_entered_the_character_immediately(SignalPool pool)
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
            Verify_the_following_information_The_state_of_button_is_changed_to_Pressed_the_border_of_button_is_removed_The_sound_Click_is_played_once(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Verify the following information,The border of the input field is shown (state ‘Enabled’) without a sound
        /// Used in:
        ///     Step 15 in TC-ID: 22.6.3.1 in 27.6.3.1 Wheel diameter window: General apearance
        ///     Step 13 in TC-ID: 22.6.5.1 in 27.6.5.1 Radar window: General appearance
        /// </summary>
        public static void
            Verify_the_following_information_The_border_of_the_input_field_is_shown_state_Enabled_without_a_sound(SignalPool pool)
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
        public static void Verify_the_following_information_The_button_is_back_to_state_Pressed_without_a_sound(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Verify the following information,The state of an input field is changed to ‘selected’ when release the pressed area at the Label part of input field
        /// Used in:
        ///     Step 25 in TC-ID: 22.6.3.1 in 27.6.3.1 Wheel diameter window: General apearance
        ///     Step 23 in TC-ID: 22.6.5.1 in 27.6.5.1 Radar window: General appearance
        /// </summary>
        public static void
            Verify_the_following_information_The_state_of_an_input_field_is_changed_to_selected_when_release_the_pressed_area_at_the_Label_part_of_input_field(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Verify the following information,The state of an input field is changed to ‘selected’ when release the pressed area at the Data part of input field
        /// Used in:
        ///     Step 26 in TC-ID: 22.6.3.1 in 27.6.3.1 Wheel diameter window: General apearance
        ///     Step 31 in TC-ID: 22.8.1.1 in 27.8.1.1
        /// </summary>
        public static void
            Verify_the_following_information_The_state_of_an_input_field_is_changed_to_selected_when_release_the_pressed_area_at_the_Data_part_of_input_field(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Verify the following information,Use the log file to confirm that DMI sent out packet [MMI_DRIVER_REQUEST (EVC-101)] with variable MMI_M_REQUEST = 54 (Exit Maintenance).The window is closed and the Maintenance window is displayed
        /// Used in:
        ///     Step 27 in TC-ID: 22.6.3.1 in 27.6.3.1 Wheel diameter window: General apearance
        ///     Step 25 in TC-ID: 22.6.5.1 in 27.6.5.1 Radar window: General appearance
        /// </summary>
        public static void
            Verify_the_following_information_Use_the_log_file_to_confirm_that_DMI_sent_out_packet_MMI_DRIVER_REQUEST_EVC_101_with_variable_MMI_M_REQUEST_54_Exit_Maintenance_The_window_is_closed_and_the_Maintenance_window_is_displayed(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: The ‘Wheel diameter’ data entry window appears on ETCS-DMI screen instead of the ‘Settings’ menu window
        /// Used in:
        ///     Step 1 in TC-ID: 22.6.3.2.3.2  in 1 Introduction
        ///     Step 1 in TC-ID: 22.6.3.2.5 in 27.6.3.2.5 ‘Wheel diameter’ Data Checks: Technical Range Checks by Variable Range
        /// </summary>
        public static void
            The_Wheel_diameter_data_entry_window_appears_on_ETCS_DMI_screen_instead_of_the_Settings_menu_window(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Input Field (All)(1) The ‘Enter’ button associated to the data area of the input field displays the previously entered value.Echo Texts (All)(2) The data part of the echo text displays “++++”
        /// Used in:
        ///     Step 5 in TC-ID: 22.6.3.2.3.2  in 1 Introduction
        ///     Step 5 in TC-ID: 22.6.5.2.3.2  in 1 Introduction
        /// </summary>
        public static void
            Input_Field_All1_The_Enter_button_associated_to_the_data_area_of_the_input_field_displays_the_previously_entered_value_Echo_Texts_All2_The_data_part_of_the_echo_text_displays(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Verify the following information,DMI displays Maintenance window.Use the log file to confirm that DMI sends out the packet [MMI_DRIVER_REQUEST (EVC-101)] with variable ;[MMI_DRIVER_REQUEST (EVC-101).MMI_M_REQUEST] = 54 (Exit Maintenance)
        /// Used in:
        ///     Step 6 in TC-ID: 22.6.4.1 in 27.6.4.1
        ///     Step 8 in TC-ID: 22.6.4.1 in 27.6.4.1
        ///     Step 8 in TC-ID: 22.6.6.1 in 27.6.6.1
        /// </summary>
        public static void
            Verify_the_following_information_DMI_displays_Maintenance_window_Use_the_log_file_to_confirm_that_DMI_sends_out_the_packet_MMI_DRIVER_REQUEST_EVC_101_with_variable_MMI_DRIVER_REQUEST_EVC_101_MMI_M_REQUEST_54_Exit_Maintenance(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: DMI displays Wheel diameter validation window
        /// Used in:
        ///     Step 7 in TC-ID: 22.6.4.1 in 27.6.4.1
        ///     Step 9 in TC-ID: 22.6.4.1 in 27.6.4.1
        /// </summary>
        public static void DMI_displays_Wheel_diameter_validation_window(SignalPool pool)
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
            Verify_the_following_information_The_state_of_an_input_field_is_changed_to_selected_when_release_the_pressed_area_at_the_Data_area_of_input_field(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: The ‘Radar’ data entry window appears on ETCS-DMI screen instead of the ‘Settings’ menu window
        /// Used in:
        ///     Step 1 in TC-ID: 22.6.5.2.3.2  in 1 Introduction
        ///     Step 1 in TC-ID: 22.6.5.2.5 in 27.6.5.2.5 ‘Radar’ Data Checks: Technical Range Checks by Variable Range
        /// </summary>
        public static void The_Radar_data_entry_window_appears_on_ETCS_DMI_screen_instead_of_the_Settings_menu_window(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: DMI displays Radar validation window
        /// Used in:
        ///     Step 7 in TC-ID: 22.6.6.1 in 27.6.6.1
        ///     Step 9 in TC-ID: 22.6.6.1 in 27.6.6.1
        /// </summary>
        public static void DMI_displays_Radar_validation_window(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Verify the following information,(1)   The state of button is changed to ‘Pressed’, the border of button is removed.(2)   The sound ‘Click’ is played once
        /// Used in:
        ///     Step 2 in TC-ID: 22.7.1 in 27.7.1 Data view window for Flexible Train data entry
        ///     Step 2 in TC-ID: 22.7.2 in 27.7.2 Data view window for Fixed Train data entry
        ///     Step 3 in TC-ID: 22.26 in 27.26 System info window
        ///     Step 6 in TC-ID: 22.29.2 in 27.29.2 Fixed Train data window: General appearances
        /// </summary>
        public static void
            Verify_the_following_information_1_The_state_of_button_is_changed_to_Pressed_the_border_of_button_is_removed_2_The_sound_Click_is_played_once(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Verify the following information,(1)   The border of the button is shown (state ‘Enabled’) without a sound
        /// Used in:
        ///     Step 3 in TC-ID: 22.7.1 in 27.7.1 Data view window for Flexible Train data entry
        ///     Step 3 in TC-ID: 22.7.2 in 27.7.2 Data view window for Fixed Train data entry
        ///     Step 4 in TC-ID: 22.26 in 27.26 System info window
        ///     Step 7 in TC-ID: 22.29.2 in 27.29.2 Fixed Train data window: General appearances
        /// </summary>
        public static void
            Verify_the_following_information_1_The_border_of_the_button_is_shown_state_Enabled_without_a_sound(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Verify the following information,(1)   The button is back to state ‘Pressed’ without a sound
        /// Used in:
        ///     Step 4 in TC-ID: 22.7.1 in 27.7.1 Data view window for Flexible Train data entry
        ///     Step 4 in TC-ID: 22.7.2 in 27.7.2 Data view window for Fixed Train data entry
        ///     Step 5 in TC-ID: 22.26 in 27.26 System info window
        ///     Step 8 in TC-ID: 22.29.2 in 27.29.2 Fixed Train data window: General appearances
        /// </summary>
        public static void Verify_the_following_information_1_The_button_is_back_to_state_Pressed_without_a_sound(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Verify that the Data view is displayed the next page of the train data.The window title of the next page is displayed with text ‘Data view (2/2)’. Data View ItemsThe data view items are displayed correctly refer to following items,RBC IDRBC phone numberVBC set code (if any)The data part of RBC phone number is displayed as 2 lines.Navigation buttonsThe state of ‘Previous’ and ‘Next’ button are displayed as follows,  ‘Next’ button is disabled, displays as symbol NA18.2  ‘Previous’ button is enabled, displays as symbol NA18
        /// Used in:
        ///     Step 5 in TC-ID: 22.7.1 in 27.7.1 Data view window for Flexible Train data entry
        ///     Step 5 in TC-ID: 22.7.2 in 27.7.2 Data view window for Fixed Train data entry
        /// </summary>
        public static void
            Verify_that_the_Data_view_is_displayed_the_next_page_of_the_train_data_The_window_title_of_the_next_page_is_displayed_with_text_Data_view_22_Data_View_ItemsThe_data_view_items_are_displayed_correctly_refer_to_following_items_RBC_IDRBC_phone_numberVBC_set_code_if_anyThe_data_part_of_RBC_phone_number_is_displayed_as_2_lines_Navigation_buttonsThe_state_of_Previous_and_Next_button_are_displayed_as_follows_Next_button_is_disabled_displays_as_symbol_NA18_2_Previous_button_is_enabled_displays_as_symbol_NA18(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: See the expected result of step 2-5 and the following points,(1)   The state of ‘Previous’ and ‘Next’ button are displayed as follows,‘Next’ button is enabled, displays as symbol NA17  ‘Previous’ button is enabled, displays as symbol NA19
        /// Used in:
        ///     Step 6 in TC-ID: 22.7.1 in 27.7.1 Data view window for Flexible Train data entry
        ///     Step 6 in TC-ID: 22.7.2 in 27.7.2 Data view window for Fixed Train data entry
        /// </summary>
        public static void
            See_the_expected_result_of_step_2_5_and_the_following_points_1_The_state_of_Previous_and_Next_button_are_displayed_as_follows_Next_button_is_enabled_displays_as_symbol_NA17_Previous_button_is_enabled_displays_as_symbol_NA19(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Verify the following information,(1)   The data part of following information are automatically insert a line brake at the end of first line, represented as 2 lines.Page 1:Driver IDPage 2:Radio Network IDRBC Phone Number
        /// Used in:
        ///     Step 8 in TC-ID: 22.7.1 in 27.7.1 Data view window for Flexible Train data entry
        ///     Step 8 in TC-ID: 22.7.2 in 27.7.2 Data view window for Fixed Train data entry
        /// </summary>
        public static void
            Verify_the_following_information_1_The_data_part_of_following_information_are_automatically_insert_a_line_brake_at_the_end_of_first_line_represented_as_2_lines_Page_1Driver_IDPage_2Radio_Network_IDRBC_Phone_Number(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Verify the following information,(1)   DMI displays Default window
        /// Used in:
        ///     Step 9 in TC-ID: 22.7.1 in 27.7.1 Data view window for Flexible Train data entry
        ///     Step 12 in TC-ID: 22.20.1 in 27.20.1 Override window: General appearance
        /// </summary>
        public static void Verify_the_following_information_1_DMI_displays_Default_window(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: See the expected results of Step 3 – Step 4 and the following additional information,The pressed key is added in an input field immediately. The cursor is jumped to next position after entered the character immediately
        /// Used in:
        ///     Step 5 in TC-ID: 22.8.1.1 in 27.8.1.1
        ///     Step 5 in TC-ID: 22.9.1 in 27.9.1 SR Speed/Distance window: General appearance
        ///     Step 5 in TC-ID: 22.22.3  in 27.22.3 Brake percentage window
        ///     Step 4 in TC-ID: 22.27.1 in 27.27.1 ‘Set VBC’ Data Entry Window
        ///     Step 4 in TC-ID: 22.28.1 in 27.28.1 ‘Remove VBC’ Data Entry Window
        /// </summary>
        public static void
            See_the_expected_results_of_Step_3_Step_4_and_the_following_additional_information_The_pressed_key_is_added_in_an_input_field_immediately_The_cursor_is_jumped_to_next_position_after_entered_the_character_immediately(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Verify the following information,The 5 characters are added on an input field as one group. (e.g. ‘10000’)
        /// Used in:
        ///     Step 8 in TC-ID: 22.8.1.1 in 27.8.1.1
        ///     Step 13 in TC-ID: 22.8.1.1 in 27.8.1.1
        ///     Step 12 in TC-ID: 22.9.1 in 27.9.1 SR Speed/Distance window: General appearance
        /// </summary>
        public static void
            Verify_the_following_information_The_5_characters_are_added_on_an_input_field_as_one_group_e_g_10000(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Verify the following information,The fifth character is shown after a gap of fourth character, separated as 2 groups (e.g. 1000 00)
        /// Used in:
        ///     Step 9 in TC-ID: 22.8.1.1 in 27.8.1.1
        ///     Step 14 in TC-ID: 22.8.1.1 in 27.8.1.1
        ///     Step 13 in TC-ID: 22.9.1 in 27.9.1 SR Speed/Distance window: General appearance
        /// </summary>
        public static void
            Verify_the_following_information_The_fifth_character_is_shown_after_a_gap_of_fourth_character_separated_as_2_groups_e_g_1000_00(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Verify the following information,The data value is separated as 2 lines. In each line is displayed only 8 characters
        /// Used in:
        ///     Step 10 in TC-ID: 22.8.1.1 in 27.8.1.1
        ///     Step 15 in TC-ID: 22.8.1.1 in 27.8.1.1
        ///     Step 15 in TC-ID: 7.3.2 in 27.17.3 Entering Characters
        /// </summary>
        public static void
            Verify_the_following_information_The_data_value_is_separated_as_2_lines_In_each_line_is_displayed_only_8_characters(SignalPool pool)
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
            Verify_the_following_information_The_border_of_the_button_is_shown_state_Enabled_without_a_sound(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: DMI displays RBC data window
        /// Used in:
        ///     Step 23 in TC-ID: 22.8.1.1 in 27.8.1.1
        ///     Step 25 in TC-ID: 22.8.1.1 in 27.8.1.1
        ///     Step 3 in TC-ID: 33.3 in 36.2 The relationship between parent and child windows (2)
        ///     Step 1 in TC-ID: 34.1.4.2 in 37.1.4.1.2 Data entry/validation process when enabling conditions not fullfilled: Level 2
        /// </summary>
        public static void DMI_displays_RBC_data_window(SignalPool pool)
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
            Verify_the_following_information_The_state_of_an_input_field_is_changed_to_selected_when_release_the_pressed_area_at_the_Label_area_of_input_field(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: The ‘Yes’ button is enabled
        /// Used in:
        ///     Step 1 in TC-ID: 22.8.1.4 in 27.8.1.4 ‘RBC data’ Data Checks: Technical Range Checks by Data Validity
        ///     Step 3 in TC-ID: 22.8.1.4 in 27.8.1.4 ‘RBC data’ Data Checks: Technical Range Checks by Data Validity
        ///     Step 5 in TC-ID: 22.8.1.4 in 27.8.1.4 ‘RBC data’ Data Checks: Technical Range Checks by Data Validity
        ///     Step 7 in TC-ID: 22.8.1.4 in 27.8.1.4 ‘RBC data’ Data Checks: Technical Range Checks by Data Validity
        /// </summary>
        public static void The_Yes_button_is_enabled(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: EVC-22(1) Use the log file to verify that DMI receives packet EVC-22 with variable MMI_M_BUTTONS = 255 (no button) and the 'Yes' button is disabled
        /// Used in:
        ///     Step 2 in TC-ID: 22.8.1.4 in 27.8.1.4 ‘RBC data’ Data Checks: Technical Range Checks by Data Validity
        ///     Step 4 in TC-ID: 22.8.1.4 in 27.8.1.4 ‘RBC data’ Data Checks: Technical Range Checks by Data Validity
        /// </summary>
        public static void
            EVC_221_Use_the_log_file_to_verify_that_DMI_receives_packet_EVC_22_with_variable_MMI_M_BUTTONS_255_no_button_and_the_Yes_button_is_disabled(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Verify the following information,DMI closes the Radio Network ID window.Use the log file to confirm that DMI sends EVC-112 with the following variables,MMI_M_BUTTONS = 254MMI_N_DATA_ELEMENTS = 1MMI_M_NID_DATA = 3MMI_NID_MN = index of selected network ID (refer to EVC-22 from previous step, the 1st index is start with 0)
        /// Used in:
        ///     Step 10 in TC-ID: 22.8.2.1 in 27.8.2.1 Radio Network ID window: General appearance
        ///     Step 12 in TC-ID: 22.8.2.1 in 27.8.2.1 Radio Network ID window: General appearance
        /// </summary>
        public static void
            Verify_the_following_information_DMI_closes_the_Radio_Network_ID_window_Use_the_log_file_to_confirm_that_DMI_sends_EVC_112_with_the_following_variables_MMI_M_BUTTONS_254MMI_N_DATA_ELEMENTS_1MMI_M_NID_DATA_3MMI_NID_MN_index_of_selected_network_ID_refer_to_EVC_22_from_previous_step_the_1st_index_is_start_with_0(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: DMI displays RBC contact window
        /// Used in:
        ///     Step 16 in TC-ID: 22.8.2.1 in 27.8.2.1 Radio Network ID window: General appearance
        ///     Step 14 in TC-ID: 22.8.3.1 in 27.8.3.1 RBC Contact window: General appearance
        ///     Step 2 in TC-ID: 33.3 in 36.2 The relationship between parent and child windows (2)
        ///     Step 4 in TC-ID: 33.3 in 36.2 The relationship between parent and child windows (2)
        ///     Step 6 in TC-ID: 33.3 in 36.2 The relationship between parent and child windows (2)
        /// </summary>
        public static void DMI_displays_RBC_contact_window(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: DMI displays RBC Contact window
        /// Used in:
        ///     Step 19 in TC-ID: 22.8.3.1 in 27.8.3.1 RBC Contact window: General appearance
        ///     Step 21 in TC-ID: 22.8.3.1 in 27.8.3.1 RBC Contact window: General appearance
        /// </summary>
        public static void DMI_displays_RBC_Contact_window(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Verify the following information,The value pressed key is not added into an input field
        /// Used in:
        ///     Step 9 in TC-ID: 22.9.1 in 27.9.1 SR Speed/Distance window: General appearance
        ///     Step 14 in TC-ID: 22.9.1 in 27.9.1 SR Speed/Distance window: General appearance
        /// </summary>
        public static void Verify_the_following_information_The_value_pressed_key_is_not_added_into_an_input_field(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: The ‘SR speed / distance’ data entry window appears on ETCS-DMI screen instead of the ‘Special’ menu window
        /// Used in:
        ///     Step 1 in TC-ID: 22.9.9 in 27.9.9 ‘SR speed / distance’ Data Checks: Technical Range Checks by Data Validity
        ///     Step 1 in TC-ID: 22.9.10 in 27.9.10 ‘SR speed / distance’ Data Checks: Technical Range Checks by Variable Range
        /// </summary>
        public static void
            The_SR_speed_distance_data_entry_window_appears_on_ETCS_DMI_screen_instead_of_the_Special_menu_window(SignalPool pool)
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
            Input_Field1_The_eventually_displayed_data_value_in_the_data_area_of_the_input_field_is_replaced_by_1_character_or_value_corresponding_to_the_activated_data_key_state_Selected_IFvalue_of_pressed_keys(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Input Field(1) The eventually displayed data value in the data area of the input field is replaced by “40” (character or value corresponding to the activated data key - state ‘Selected IF/value of pressed key(s)’)
        /// Used in:
        ///     Step 7 in TC-ID: 22.9.9 in 27.9.9 ‘SR speed / distance’ Data Checks: Technical Range Checks by Data Validity
        ///     Step 7 in TC-ID: 22.22.5 in 27.22.5 ‘Brake percentage’ Data Checks: Technical Range Checks by Data Validity
        /// </summary>
        public static void
            Input_Field1_The_eventually_displayed_data_value_in_the_data_area_of_the_input_field_is_replaced_by_40_character_or_value_corresponding_to_the_activated_data_key_state_Selected_IFvalue_of_pressed_keys(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: The Adhesion window is displayed
        /// Used in:
        ///     Step 6 in TC-ID: 22.11 in 27.11 Adhesion Window
        ///     Step 8 in TC-ID: 22.11 in 27.11 Adhesion Window
        /// </summary>
        public static void The_Adhesion_window_is_displayed(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Verify the following information,No sound ‘Click’ is played
        /// Used in:
        ///     Step 10 in TC-ID: 22.11 in 27.11 Adhesion Window
        ///     Step 11 in TC-ID: 22.11 in 27.11 Adhesion Window
        /// </summary>
        public static void Verify_the_following_information_No_sound_Click_is_played(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: The Set clock button is disabled
        /// Used in:
        ///     Step 1 in TC-ID: 22.13.1 in 27.13.1 Set Clock function: General appearance
        ///     Step 44 in TC-ID: 22.13.1 in 27.13.1 Set Clock function: General appearance
        /// </summary>
        public static void The_Set_clock_button_is_disabled(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: The Set clock button is enabled
        /// Used in:
        ///     Step 3 in TC-ID: 22.13.1 in 27.13.1 Set Clock function: General appearance
        ///     Step 45 in TC-ID: 22.13.1 in 27.13.1 Set Clock function: General appearance
        ///     Step 48 in TC-ID: 22.13.1 in 27.13.1 Set Clock function: General appearance
        /// </summary>
        public static void The_Set_clock_button_is_enabled(SignalPool pool)
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
        /// Description: Verify the following information,(1)   DMI displays Settings window
        /// Used in:
        ///     Step 50 in TC-ID: 22.13.1 in 27.13.1 Set Clock function: General appearance
        ///     Step 10 in TC-ID: 22.19 in 27.19 Language Window
        /// </summary>
        public static void Verify_the_following_information_1_DMI_displays_Settings_window(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Verify the following information,The 5 characters are added on an input field as one group. (e.g. ‘12345')
        /// Used in:
        ///     Step 13 in TC-ID: 7.3.2 in 27.17.3 Entering Characters
        ///     Step 7 in TC-ID: 22.27.1 in 27.27.1 ‘Set VBC’ Data Entry Window
        ///     Step 7 in TC-ID: 22.28.1 in 27.28.1 ‘Remove VBC’ Data Entry Window
        /// </summary>
        public static void
            Verify_the_following_information_The_5_characters_are_added_on_an_input_field_as_one_group_e_g_12345(SignalPool pool)
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
            Verify_the_following_information_The_fifth_character_is_shown_after_a_gap_of_fourth_character_separated_as_2_groups_e_g_1234_56(SignalPool pool)
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
            Verify_the_following_information_1_The_state_of_an_input_field_is_changed_to_Pressed_the_border_of_button_is_removed(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Verify the following information,The ‘EOA’ button is in disable state.Use the log file to confirm that DMI receives EVC-30 with with bit No.9 of variable MMI_Q_REQUEST_ENABLE_64 = 0 (Disable Start Override EOA)
        /// Used in:
        ///     Step 1 in TC-ID: 22.20.1 in 27.20.1 Override window: General appearance
        ///     Step 11 in TC-ID: 22.20.1 in 27.20.1 Override window: General appearance
        ///     Step 6 in TC-ID: 22.20.2 in 27.20.2 Override window in SB mode
        /// </summary>
        public static void
            Verify_the_following_information_The_EOA_button_is_in_disable_state_Use_the_log_file_to_confirm_that_DMI_receives_EVC_30_with_with_bit_No_9_of_variable_MMI_Q_REQUEST_ENABLE_64_0_Disable_Start_Override_EOA(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: The ‘Set Clock’ button is disabled
        /// Used in:
        ///     Step 14 in TC-ID: 22.21 in 27.21 Settings Window
        ///     Step 16 in TC-ID: 22.21 in 27.21 Settings Window
        ///     Step 18 in TC-ID: 22.21 in 27.21 Settings Window
        /// </summary>
        public static void The_Set_Clock_button_is_disabled(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: The ‘Set Clock’ button is enabled
        /// Used in:
        ///     Step 15 in TC-ID: 22.21 in 27.21 Settings Window
        ///     Step 17 in TC-ID: 22.21 in 27.21 Settings Window
        ///     Step 19 in TC-ID: 22.21 in 27.21 Settings Window
        /// </summary>
        public static void The_Set_Clock_button_is_enabled(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: DMI displays Settings button
        /// Used in:
        ///     Step 4 in TC-ID: 22.22.1 in 27.22.1 Brake window
        ///     Step 8 in TC-ID: 22.22.1 in 27.22.1 Brake window
        /// </summary>
        public static void DMI_displays_Settings_button(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: The ‘Brake’ button is disabled
        /// Used in:
        ///     Step 5 in TC-ID: 22.22.1 in 27.22.1 Brake window
        ///     Step 9 in TC-ID: 22.22.1 in 27.22.1 Brake window
        /// </summary>
        public static void The_Brake_button_is_disabled(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: DMI displays Brake window
        /// Used in:
        ///     Step 16 in TC-ID: 22.22.1 in 27.22.1 Brake window
        ///     Step 5 in TC-ID: 22.22.2  in 27.22.2 Brake test window
        /// </summary>
        public static void DMI_displays_Brake_window(SignalPool pool)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Description: Verify the following information,DMI displays Brake window.Use the log file to confirm that DMI sends out the packet [MMI_DRIVER_REQUEST (EVC-101)] with variable [MMI_DRIVER_REQUEST (EVC-101).MMI_M_REQUEST] = 60 (Exit brake percentage)
        /// Used in:
        ///     Step 3 in TC-ID: 22.22.4  in 27.22.4 Brake percentage validation window
        ///     Step 5 in TC-ID: 22.22.4  in 27.22.4 Brake percentage validation window
        /// </summary>
        public static void
            Verify_the_following_information_DMI_displays_Brake_window_Use_the_log_file_to_confirm_that_DMI_sends_out_the_packet_MMI_DRIVER_REQUEST_EVC_101_with_variable_MMI_DRIVER_REQUEST_EVC_101_MMI_M_REQUEST_60_Exit_brake_percentage(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: DMI displays Brake percentage validation window
        /// Used in:
        ///     Step 4 in TC-ID: 22.22.4  in 27.22.4 Brake percentage validation window
        ///     Step 6 in TC-ID: 22.22.4  in 27.22.4 Brake percentage validation window
        /// </summary>
        public static void DMI_displays_Brake_percentage_validation_window(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: The ‘Brake percentage’ data entry window appears on ETCS-DMI screen instead of the ‘Special’ menu window
        /// Used in:
        ///     Step 1 in TC-ID: 22.22.5 in 27.22.5 ‘Brake percentage’ Data Checks: Technical Range Checks by Data Validity
        ///     Step 1 in TC-ID: 22.22.6 in 27.22.6 ‘Brake percentage’ Data Checks: Technical Range Checks by Variable Range
        /// </summary>
        public static void
            The_Brake_percentage_data_entry_window_appears_on_ETCS_DMI_screen_instead_of_the_Special_menu_window(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Verify the following information,The input field is stop decreasing
        /// Used in:
        ///     Step 3 in TC-ID: 22.24 in 27.24 Brightness window
        ///     Step 3 in TC-ID: 22.25 in 27.25 Volume window
        /// </summary>
        public static void Verify_the_following_information_The_input_field_is_stop_decreasing(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Verify the following information,The input field is stop increasing
        /// Used in:
        ///     Step 5 in TC-ID: 22.24 in 27.24 Brightness window
        ///     Step 5 in TC-ID: 22.25 in 27.25 Volume window
        /// </summary>
        public static void Verify_the_following_information_The_input_field_is_stop_increasing(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Verify the following information,(1)   DMI displays the Settings window
        /// Used in:
        ///     Step 12 in TC-ID: 22.24 in 27.24 Brightness window
        ///     Step 12 in TC-ID: 22.25 in 27.25 Volume window
        /// </summary>
        public static void Verify_the_following_information_1_DMI_displays_the_Settings_window(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Verify the following information,The state of button is changed to ‘Pressed’ and immediately back to ‘Enabled’ state.The sound ‘Click’ is played once.The Input Field displays the value associated to the data key according to the pressings in state ‘Pressed’.The cursor is displayed as horizontal line below the value of the numeric-keyboard data key in the input field.The input field is used to enter the VBC code.The colour of data value is black.An echo text is composed of Label Part and Data Part.The Data part of echo text is left aligned
        /// Used in:
        ///     Step 2 in TC-ID: 22.27.1 in 27.27.1 ‘Set VBC’ Data Entry Window
        ///     Step 2 in TC-ID: 22.28.1 in 27.28.1 ‘Remove VBC’ Data Entry Window
        /// </summary>
        public static void
            Verify_the_following_information_The_state_of_button_is_changed_to_Pressed_and_immediately_back_to_Enabled_state_The_sound_Click_is_played_once_The_Input_Field_displays_the_value_associated_to_the_data_key_according_to_the_pressings_in_state_Pressed_The_cursor_is_displayed_as_horizontal_line_below_the_value_of_the_numeric_keyboard_data_key_in_the_input_field_The_input_field_is_used_to_enter_the_VBC_code_The_colour_of_data_value_is_black_An_echo_text_is_composed_of_Label_Part_and_Data_Part_The_Data_part_of_echo_text_is_left_aligned(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Verify the following information,The data value is separated into 2 lines. In each line is displayed only 8 characters
        /// Used in:
        ///     Step 9 in TC-ID: 22.27.1 in 27.27.1 ‘Set VBC’ Data Entry Window
        ///     Step 9 in TC-ID: 22.28.1 in 27.28.1 ‘Remove VBC’ Data Entry Window
        /// </summary>
        public static void
            Verify_the_following_information_The_data_value_is_separated_into_2_lines_In_each_line_is_displayed_only_8_characters(SignalPool pool)
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
            Verify_the_following_information_The_state_of_an_input_field_is_changed_to_accepted_when_release_the_pressed_area_at_the_Data_area_of_input_field(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: DMI displays Set VBC Validation window
        /// Used in:
        ///     Step 7 in TC-ID: 22.27.2 in 27.27.2 ‘Set VBC’ Validation Window
        ///     Step 9 in TC-ID: 22.27.2 in 27.27.2 ‘Set VBC’ Validation Window
        /// </summary>
        public static void DMI_displays_Set_VBC_Validation_window(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: The ‘Set VBC’ data entry window appears on ETCS-DMI screen instead of the ‘Settings’ menu window
        /// Used in:
        ///     Step 1 in TC-ID: 22.27.3 in 27.27.3 ‘Set VBC’ Data Checks: Technical Range Checks by Data Validity
        ///     Step 1 in TC-ID: 22.27.9 in 27.27.9 ‘Set VBC’ Data Checks: Technical Range Checks by Variable Range
        /// </summary>
        public static void
            The_Set_VBC_data_entry_window_appears_on_ETCS_DMI_screen_instead_of_the_Settings_menu_window(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Input Field(1) The eventually displayed data value in the data area of the input field is replaced by “65536” (character or value corresponding to the activated data key - state ‘Selected IF/value of pressed key(s)’)
        /// Used in:
        ///     Step 7 in TC-ID: 22.27.3 in 27.27.3 ‘Set VBC’ Data Checks: Technical Range Checks by Data Validity
        ///     Step 7 in TC-ID: 22.28.3 in 27.28.3 ‘Remove VBC’ Data Checks: Technical Range Checks by Data Validity
        /// </summary>
        public static void
            Input_Field1_The_eventually_displayed_data_value_in_the_data_area_of_the_input_field_is_replaced_by_65536_character_or_value_corresponding_to_the_activated_data_key_state_Selected_IFvalue_of_pressed_keys(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: 1. After pressing the ‘Yes’ button, the data validation window (‘Validate Set VBC’) appears instead of the ‘Set VBC’ data entry window. The data part of echo text displays “65536” in white.2. After the data area of the input field containing “Yes” is pressed, the data validation window disappears and returns to the parent window (‘Settings’ window) of ‘Set VBC’ window with enabled ‘Set VBC’ button
        /// Used in:
        ///     Step 9 in TC-ID: 22.27.3 in 27.27.3 ‘Set VBC’ Data Checks: Technical Range Checks by Data Validity
        ///     Step 5 in TC-ID: 22.27.9 in 27.27.9 ‘Set VBC’ Data Checks: Technical Range Checks by Variable Range
        /// </summary>
        public static void
            After_pressing_the_Yes_button_the_data_validation_window_Validate_Set_VBC_appears_instead_of_the_Set_VBC_data_entry_window_The_data_part_of_echo_text_displays_65536_in_white_2_After_the_data_area_of_the_input_field_containing_Yes_is_pressed_the_data_validation_window_disappears_and_returns_to_the_parent_window_Settings_window_of_Set_VBC_window_with_enabled_Set_VBC_button(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: DMI displays Remove VBC window
        /// Used in:
        ///     Step 13 in TC-ID: 22.28.1 in 27.28.1 ‘Remove VBC’ Data Entry Window
        ///     Step 36 in TC-ID: 34.7 in 37.7 Dialogue Sequence of Settings window
        /// </summary>
        public static void DMI_displays_Remove_VBC_window(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: DMI displays Remove VBC Validation window
        /// Used in:
        ///     Step 4 in TC-ID: 22.28.2 in 27.28.2 ‘Remove VBC’ Validation Window
        ///     Step 6 in TC-ID: 22.28.2 in 27.28.2 ‘Remove VBC’ Validation Window
        /// </summary>
        public static void DMI_displays_Remove_VBC_Validation_window(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: The ‘Remove VBC’ data entry window appears on ETCS-DMI screen instead of the ‘Settings’ menu window
        /// Used in:
        ///     Step 1 in TC-ID: 22.28.3 in 27.28.3 ‘Remove VBC’ Data Checks: Technical Range Checks by Data Validity
        ///     Step 1 in TC-ID: 22.28.9 in 27.28.9 ‘Remove VBC’ Data Checks: Technical Range Checks by Variable Range
        /// </summary>
        public static void
            The_Remove_VBC_data_entry_window_appears_on_ETCS_DMI_screen_instead_of_the_Settings_menu_window(SignalPool pool)
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
        /// Description: Verify the following information,The new UTC time and offset time are changed and displayed according to the entered data from the driver.Use the log file to verify that DMI sends out packet EVC-109 to ETCS OB correctly
        /// Used in:
        ///     Step 4 in TC-ID: 29.1 in 29.1 UTC time and offset time(by driver)
        ///     Step 5 in TC-ID: 29.1 in 29.1 UTC time and offset time(by driver)
        /// </summary>
        public static void
            Verify_the_following_information_The_new_UTC_time_and_offset_time_are_changed_and_displayed_according_to_the_entered_data_from_the_driver_Use_the_log_file_to_verify_that_DMI_sends_out_packet_EVC_109_to_ETCS_OB_correctly(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Verify the following information:(1) DMI time is updated only its offset time
        /// Used in:
        ///     Step 3 in TC-ID: 29.3 in 29.3 UTC time and offset time(By VAP acting as NTP server)
        ///     Step 5 in TC-ID: 29.3 in 29.3 UTC time and offset time(By VAP acting as NTP server)
        /// </summary>
        public static void Verify_the_following_information1_DMI_time_is_updated_only_its_offset_time(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: DMI displays Volume window
        /// Used in:
        ///     Step 16 in TC-ID: 34.1.4 in 37.1.4.1.1 Data entry/validation process when enabling conditions not fullfilled: Level 1
        ///     Step 18 in TC-ID: 34.7 in 37.7 Dialogue Sequence of Settings window
        /// </summary>
        public static void DMI_displays_Volume_window(SignalPool pool)
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
            Verify_the_following_information_Use_the_log_file_to_confirm_that_DMI_receives_EVC_7_with_variable_OBU_TR_M_MODE_3_SH_Shunting_The_symbol_MO01_is_display_in_area_B7_DMI_closes_Main_window_and_returns_to_the_Default_window(SignalPool pool)
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
        /// Description: DMI displays Settings window.Verify the following information,The ‘Close’ button is enabled
        /// Used in:
        ///     Step 1 in TC-ID: 34.7 in 37.7 Dialogue Sequence of Settings window
        ///     Step 7 in TC-ID: 34.7 in 37.7 Dialogue Sequence of Settings window
        /// </summary>
        public static void DMI_displays_Settings_window_Verify_the_following_information_The_Close_button_is_enabled(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: DMI displays train running number window
        /// Used in:
        ///     Step 4 in TC-ID: 35.2 in 38.2 NTC System Status Messages
        ///     Step 9 in TC-ID: 35.2 in 38.2 NTC System Status Messages
        /// </summary>
        public static void DMI_displays_train_running_number_window(SignalPool pool)
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
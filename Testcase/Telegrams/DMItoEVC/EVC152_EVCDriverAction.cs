﻿using System;
using CL345;
using Testcase.DMITestCases;
using BT_CSB_Tools.SignalPoolGenerator.Signals.PdSignal.Misc;

namespace Testcase.Telegrams.DMItoEVC
{
    /// <summary>
    /// This packet shall be sent when the corresponding driver action is  performed.
    ///The data is used by ETC to record the driver actions in JRU.
    /// </summary>
    public static class EVC152_MMIDriverAction
    {
        private static SignalPool _pool;
        private static MMI_M_DRIVER_ACTION _driverAction;
        private static bool _checkResult;
        const string baseString = "DMI->ETCS: EVC-152 [MMI_DRIVER_ACTION] - MMI_M_DRIVER_ACTION";

        /// <summary>
        /// Initialise EVC-152 MMI_Driver_Action telegram.
        /// </summary>
        /// <param name="pool"></param>
        public static void Initialise(SignalPool pool)
        {
            _pool = pool;
            _pool.SITR.SMDCtrl.CCUO.ETCS1DriverAction.Value = 0x0001;
            _pool.SITR.SMDStat.CCUO.ETCS1DriverAction.Value = 0x00;
        }

        private static void CheckDriverAction(MMI_M_DRIVER_ACTION driverAction)
        {
            // Reset telegram received flag in RTSim
            _pool.SITR.SMDStat.CCUO.ETCS1DriverAction.Value = 0x00;

            // Wait 10 seconds for SMDStat to become set
            if (_pool.SITR.SMDStat.CCUO.ETCS1DriverAction.WaitForCondition(Is.Equal, 0x01, 10000, 100))
            {
                _checkResult = _pool.SITR.CCUO.ETCS1DriverAction.MmiMDriverAction.Value.Equals((byte) driverAction);

                // If check passes
                if (_checkResult)
                {
                    _pool.TraceReport($"{baseString} = {(byte) driverAction} \"{driverAction}\"" + Environment.NewLine +
                                      "Result: PASSED.");
                }
                // Else display the real value extracted from EVC-152
                else
                {
                    _pool.TraceError($"{baseString} = {_pool.SITR.CCUO.ETCS1DriverAction.MmiMDriverAction.Value} - \"" +
                                     Enum.GetName(typeof(MMI_M_DRIVER_ACTION),
                                         _pool.SITR.CCUO.ETCS1DriverAction.MmiMDriverAction.Value) + "\"" +
                                     Environment.NewLine + "Result: FAILED.");
                }
            }
            // Show generic DMI -> EVC telegram failure
            else
            {
                DmiExpectedResults.DMItoEVC_Telegram_Not_Received(_pool, baseString);
            }
        }

        /// <summary>
        /// Contains the performed driver action
        /// 
        /// Values:
        /// 0  = "Ack of On Sight mode"
        /// 1  = "Ack of Shunting mode"
        /// 2  = "Ack of Train Trip"
        /// 3  = "Ack of Staff Responsible mode"
        /// 4  = "Ack of Unfitted mode"
        /// 5  = "Ack of Reversing mode"
        /// 6  = "Ack level 0"
        /// 7  = "Ack level 1"
        /// 8  = "Ack level 2"
        /// 9  = "Ack level 3"
        /// 10 = "Ack level NTC"
        /// 11 = "Shunting selected"
        /// 12 = "Non Leading selected"
        /// 13 = "Ack of Limited Supervision mode"
        /// 14 = "Override selected"
        /// 15 = "“Continue Shunting on desk closure” selected"
        /// 16 = "Brake release acknowledgement"
        /// 17 = "Exit of Shunting selected"
        /// 18 = "Isolation selected"
        /// 19 = "Start selected"
        /// 20 = "Train Data Entry requested"
        /// 21 = "Validation of train data"
        /// 22 = "Confirmation of Track Ahead Free"
        /// 23 = "Ack of Plain Text information"
        /// 24 = "Ack of Fixed Text information"
        /// 25 = "Request to hide supervision limits"
        /// 26 = "Train integrity confirmation"
        /// 27 = "Request to show supervision limits"
        /// 28 = "Ack of SN mode"
        /// 29 = "Selection of Language"
        /// 30 = "Request to show geographical position"
        /// 31 = "spare"
        /// 32 = "spare"
        /// 33 = "Request to hide geographical position"
        /// 34 = "Level 0 selected"
        /// 35 = "Level 1 selected"
        /// 36 = "Level 2 selected"
        /// 37 = "Level 3 selected"
        /// 38 = "Level NTC selected"
        /// 39 = "Request to show tunnel stopping area information"
        /// 40 = "Request to hide tunnel stopping area information"
        /// 41 = "Scroll up button activated"
        /// 42 = "Scroll down button activated"
        /// 43..255 = "spare"
        /// </summary>
        public static MMI_M_DRIVER_ACTION Check_MMI_M_DRIVER_ACTION
        {
            set
            {
                _driverAction = value;
                CheckDriverAction(_driverAction);
            }
        }

        /// <summary>
        /// MMI_M_DRIVER_ACTION enum
        /// 
        /// Values:
        /// 0  = "Ack of On Sight mode"
        /// 1  = "Ack of Shunting mode"
        /// 2  = "Ack of Train Trip"
        /// 3  = "Ack of Staff Responsible mode"
        /// 4  = "Ack of Unfitted mode"
        /// 5  = "Ack of Reversing mode"
        /// 6  = "Ack level 0"
        /// 7  = "Ack level 1"
        /// 8  = "Ack level 2"
        /// 9  = "Ack level 3"
        /// 10 = "Ack level NTC"
        /// 11 = "Shunting selected"
        /// 12 = "Non Leading selected"
        /// 13 = "Ack of Limited Supervision mode"
        /// 14 = "Override selected"
        /// 15 = "“Continue Shunting on desk closure” selected"
        /// 16 = "Brake release acknowledgement"
        /// 17 = "Exit of Shunting selected"
        /// 18 = "Isolation selected"
        /// 19 = "Start selected"
        /// 20 = "Train Data Entry requested"
        /// 21 = "Validation of train data"
        /// 22 = "Confirmation of Track Ahead Free"
        /// 23 = "Ack of Plain Text information"
        /// 24 = "Ack of Fixed Text information"
        /// 25 = "Request to hide supervision limits"
        /// 26 = "Train integrity confirmation"
        /// 27 = "Request to show supervision limits"
        /// 28 = "Ack of SN mode"
        /// 29 = "Selection of Language"
        /// 30 = "Request to show geographical position"
        /// 31 = "spare"
        /// 32 = "spare"
        /// 33 = "Request to hide geographical position"
        /// 34 = "Level 0 selected"
        /// 35 = "Level 1 selected"
        /// 36 = "Level 2 selected"
        /// 37 = "Level 3 selected"
        /// 38 = "Level NTC selected"
        /// 39 = "Request to show tunnel stopping area information"
        /// 40 = "Request to hide tunnel stopping area information"
        /// 41 = "Scroll up button activated"
        /// 42 = "Scroll down button activated"
        /// 43..255 = "spare"
        /// </summary>
        public enum MMI_M_DRIVER_ACTION : byte
        {
            OnSightModeAck = 0,
            ShuntingModeAck = 1,
            TrainTripAck = 2,
            StaffResponsibleModeAck = 3,
            UnfittedModeAck = 4,
            ReversingModeAck = 5,
            Level0Ack = 6,
            Level1Ack = 7,
            Level2Ack = 8,
            Level3Ack = 9,
            LevelNTCAck = 10,
            ShuntingSelected = 11,
            NonLeadingSelected = 12,
            LimitedSupervisionModeAck = 13,
            OverrideSelected = 14,
            ContinueShuntingOnDeskClosureSelected = 15,
            BrakeReleaseAck = 16,
            ExitShuntingSelected = 17,
            IsolationSelected = 18,
            StartSelected = 19,
            TrainDataEntryRequested = 20,
            TrainDataValidation = 21,
            TrackAheadFreeConfirmation = 22,
            PlainTextInformationAck = 23,
            FixedTextInformationAck = 24,
            RequestToHideSupervisionLimits = 25,
            TrainIntegrityConfirmation = 26,
            RequestToShowSupervisionLimits = 27,
            SNModeAck = 28,
            LanguageSelection = 29,
            RequestToShowGeographicalPosition = 30,
            RequestToHideGeographicalPosition = 33,
            Level0Selected = 34,
            Level1Selected = 35,
            Level2Selected = 36,
            Level3Selected = 37,
            LevelNTCSelected = 38,
            RequestToShowTunnelStoppingAreaInformation = 39,
            RequestToHideTunnelStoppingAreaInformation = 40,
            ScrollUpButtonActivated = 41,
            ScrollDownButtonActivated = 42
        }
    }
}
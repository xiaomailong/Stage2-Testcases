using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CL345;
using System.Windows.Forms;

namespace Testcase.Telegrams.EVCtoDMI
{
    public static class Variables
    {
        /// <summary>
        /// This populates the Data Elements of EVC-6, 11, and 22
        /// 
        /// Note: EVC-22 captions must be limited to 10 chars compared to the 16 allowed in the VSIS.
        /// </summary>
        /// <param name="baseString">The base RTSIM signal name to use</param>
        /// <param name="totalSizeCounter">Reference counter for total size of telegram</param>
        /// <param name="_pool">The SignalPool</param>
        /// <returns></returns>
        public static ushort PopulateDataElements(string baseString, ushort totalSizeCounter,
            List<DataElement> dataElements, SignalPool _pool)
        {
            // Populate the data elements array
            for (var tdeIndex = 0; tdeIndex < dataElements.Count; tdeIndex++)
            {
                var trainDataElement = dataElements[tdeIndex];

                var varNamestring = baseString + tdeIndex + "_";
                var charArray = trainDataElement.EchoText.ToCharArray();
                if (charArray.Length > 10)
                    throw new ArgumentOutOfRangeException(charArray.ToString(), "Too many characters in the caption string!");

                // Set identifier
                _pool.SITR.Client.Write(varNamestring + "MmiNidData", (byte)trainDataElement.Identifier);

                // Set data check result
                _pool.SITR.Client.Write(varNamestring + "MmiQDataCheck", (byte)trainDataElement.QDataCheck);

                // Set number of chars
                _pool.SITR.Client.Write(varNamestring + "MmiNText", (ushort) charArray.Length);

                totalSizeCounter += 32;

                // Populate the array
                for (var charIndex = 0; charIndex < charArray.Length; charIndex++)
                {
                    var character = charArray[charIndex];

                    if (charIndex < 10)
                    {
                        _pool.SITR.Client.Write(varNamestring + baseString.Substring( baseString.LastIndexOf('_') + 1 ) + $"10{charIndex}_MmiXText", character);
                    }
                    else
                    {
                        _pool.SITR.Client.Write(varNamestring + baseString.Substring( baseString.LastIndexOf('_') + 1 ) + $"1{charIndex}_MmiXText", character);
                    }

                    totalSizeCounter += 8;
                }
            }
            return totalSizeCounter;
        }

        /// <summary>
        /// Data to be checked and verified by the EVC.
        /// </summary>
        public class DataElement
        {
            public ushort Identifier { get; set; }      // Identifier of a data set
            public ushort QDataCheck { get; set; }      // Result of data check
            public string EchoText { get; set; }        // Echo text of data
        }

        /// <summary>
        /// Identifier of MMI Buttons.
        /// 
        /// Values:
        /// 0 = "BTN_MAIN"
        /// 1 = "BTN_OVERRIDE"
        /// 2 = "BTN_DATA_VIEW"
        /// 3 = "BTN_SPECIAL"
        /// 4 = "BTN_SETTINGS"
        /// 5 = "BTN_START"
        /// 6 = "BTN_DRIVER_ID"
        /// 7 = "BTN_TRAIN_DATA"
        /// 8 = "BTN_LEVEL"
        /// 9 = "BTN_TRAIN_RUNNING_NUMBER"
        /// 10 = "BTN_SHUNTING"
        /// 11 = "BTN_EXIT_SHUNTING"
        /// 12 = "BTN_NON_LEADING"
        /// 13 = "BTN_MAINTAIN_SHUNTING"
        /// 14 = "BTN_OVERRIDE_EOA"
        /// 15 = "BTN_ADHESION"
        /// 16 = "BTN_SR_SPEED_DISTANCE"
        /// 17 = "BTN_TRAIN_INTEGRITY"
        /// 18 = "BTN_SYSTEM_VERSION"
        /// 19 = "BTN_SET_VBC"
        /// 20 = "BTN_REMOVE_VBC"
        /// 21 = "BTN_CONTACT_LAST_RBC"
        /// 22 = "BTN_USE_SHORT_NUMBER"
        /// 23 = "BTN_ENTER_RBC_DATA"
        /// 24 = "BTN_RADIO_NETWORK_ID"
        /// 25 = "BTN_DRIVERID_TRAIN_RUNNING_NUMBER "
        /// 26 = "BTN_DRIVERID_SETTINGS"
        /// 27 = "BTN_SWITCH_FIXED_TRAIN_DATA_ENTRY"
        /// 28 = "BTN_SWITCH_FLEXIBLE_TRAIN_DATA_ENTRY"
        /// 29 = "BTN_TOGGLE_TUNNELSTOP_AREA"
        /// 30 = "BTN_TOGGLE_SPEED_DISTANCE_INFO"
        /// 31 = "BTN_YES_TRACK_AHEAD_FREE"
        /// 32 = "BTN_TOGGLE_GEOPOS"
        /// 33 = "BTN_CLOSE"
        /// 34 = "BTN_SCROLL_UP"
        /// 35 = "BTN_SCROLL_DOWN"
        /// 36 = "BTN_YES_DATA_ENTRY_COMPLETE"
        /// 37 = "BTN_YES_DATA_ENTRY_COMPLETE_DELAY_TYPE"
        /// 38 = "BTN_STM_DATA_ENTRY_SELECTION_POS1"
        /// 39 = "BTN_STM_DATA_ENTRY_SELECTION_POS2"
        /// 40 = "BTN_STM_DATA_ENTRY_SELECTION_POS3"
        /// 41 = "BTN_STM_DATA_ENTRY_SELECTION_POS4"
        /// 42 = "BTN_STM_DATA_ENTRY_SELECTION_POS5"
        /// 43 = "BTN_STM_DATA_ENTRY_SELECTION_POS6"
        /// 44 = "BTN_STM_DATA_ENTRY_SELECTION_POS7"
        /// 45 = "BTN_STM_DATA_ENTRY_SELECTION_POS8"
        /// 46 = "BTN_STM_END_OF_DATA_ENTRY"
        /// 47..252 = "Spare"
        /// 253 = "BTN_ENTER_DELAY_TYPE"
        /// 254 = "BTN_ENTER"
        /// 255 = "no button"
        /// </summary>
        public enum MMI_M_BUTTONS : ushort
        {
            BTN_MAIN = 0,
            BTN_OVERRIDE = 1,
            BTN_DATA_VIEW = 2,
            BTN_SPECIAL = 3,
            BTN_SETTINGS = 4,
            BTN_START = 5,
            BTN_DRIVER_ID = 6,
            BTN_TRAIN_DATA = 7,
            BTN_LEVEL = 8,
            BTN_TRAIN_RUNNING_NUMBER = 9,
            BTN_SHUNTING = 10,
            BTN_EXIT_SHUNTING = 11,
            BTN_NON_LEADING = 12,
            BTN_MAINTAIN_SHUNTING = 13,
            BTN_OVERRIDE_EOA = 14,
            BTN_ADHESION = 15,
            BTN_SR_SPEED_DISTANCE = 16,
            BTN_TRAIN_INTEGRITY = 17,
            BTN_SYSTEM_VERSION = 18,
            BTN_SET_VBC = 19,
            BTN_REMOVE_VBC = 20,
            BTN_CONTACT_LAST_RBC = 21,
            BTN_USE_SHORT_NUMBER = 22,
            BTN_ENTER_RBC_DATA = 23,
            BTN_RADIO_NETWORK_ID = 24,
            BTN_DRIVERID_TRAIN_RUNNING_NUMBER = 25,
            BTN_DRIVERID_SETTINGS = 26,
            BTN_SWITCH_FIXED_TRAIN_DATA_ENTRY = 27,
            BTN_SWITCH_FLEXIBLE_TRAIN_DATA_ENTRY = 28,
            BTN_TOGGLE_TUNNELSTOP_AREA = 29,
            BTN_TOGGLE_SPEED_DISTANCE_INFO = 30,
            BTN_YES_TRACK_AHEAD_FREE = 31,
            BTN_TOGGLE_GEOPOS = 32,
            BTN_CLOSE = 33,
            BTN_SCROLL_UP = 34,
            BTN_SCROLL_DOWN = 35,
            BTN_YES_DATA_ENTRY_COMPLETE = 36,
            BTN_YES_DATA_ENTRY_COMPLETE_DELAY_TYPE = 37,
            BTN_STM_DATA_ENTRY_SELECTION_POS1 = 38,
            BTN_STM_DATA_ENTRY_SELECTION_POS2 = 39,
            BTN_STM_DATA_ENTRY_SELECTION_POS3 = 40,
            BTN_STM_DATA_ENTRY_SELECTION_POS4 = 41,
            BTN_STM_DATA_ENTRY_SELECTION_POS5 = 42,
            BTN_STM_DATA_ENTRY_SELECTION_POS6 = 43,
            BTN_STM_DATA_ENTRY_SELECTION_POS7 = 44,
            BTN_STM_DATA_ENTRY_SELECTION_POS8 = 45,
            BTN_STM_END_OF_DATA_ENTRY = 46,
            BTN_ENTER_DELAY_TYPE = 253,
            BTN_ENTER = 254,
            No_Button = 255
        }

        /// <summary>
        /// MMI_M_Buttons for EVC-18 and EVC-19 enum
        /// </summary>
        public enum MMI_M_BUTTONS_VBC : byte
        {
            BTN_SETTINGS = 4,
            BTN_YES_DATA_ENTRY_COMPLETE = 36,
            BTN_YES_DATA_ENTRY_COMPLETE_DELAY_TYPE = 37,
            NoButton = 255
        }

        /// <summary>
        /// MMI_M_Buttons for EVC-6 and EVC-107 enum
        /// </summary>
        public enum MMI_M_BUTTONS_TRAIN_DATA : byte
        {
            BTN_YES_DATA_ENTRY_COMPLETE = 36,
            BTN_YES_DATA_ENTRY_COMPLETE_DELAY_TYPE = 37,
            BTN_ENTER_DELAY_TYPE = 253,
            BTN_ENTER = 254,
            NoButton = 255
        }

        /// <summary>
        /// MMI_NID_RBC value specifying to contact last known RBC
        /// </summary>
        public const uint ContactLastRBC = 16383;

        /// <summary>
        /// NID_C as allocated by ERA for Great Western line
        /// </summary>
        public const uint NidC = 3;

        /// <summary>
        /// Conversion factor for cm/s to km/h
        /// </summary>
        public const double CmSToKmH = 0.036;

        /// <summary>
        /// Conversion factor for cm/s to mph
        /// </summary>
        public const double CmSToMph = 0.02237;

        /// <summary>
        /// Driver Request enum
        /// 
        /// Values:
        /// 0 = "Spare"
        /// 1 = "Start Shunting"
        /// 2 = "Exit Shunting"
        /// 3 = "Start Train Data Entry"
        /// 4 = "Exit Train Data Entry"
        /// 5 = "Start Non-Leading"
        /// 6 = "Exit Non-Leading"
        /// 7 = "Start Override EOA (Pass stop)"
        /// 8 = "Geographical position request"
        /// 9 = "Start"
        /// 10 = "Restore adhesion coefficient to 'non-slippery rail'"
        /// 11 = "Set adhesion coefficient to 'slippery rail'"
        /// 12 = "Exit Change SR rules"
        /// 13 = "Change SR rules"
        /// 14 = "Continue shunting on desk closure"
        /// 15 = "Spare"
        /// 16 = "Spare"
        /// 17 = "Spare"
        /// 18 = "Spare"
        /// 19 = "Spare"
        /// 20 = "Change Driver identity"
        /// 21 = "Start Train Data View"
        /// 22 = "Start Brake Test"
        /// 23 = "Start Set VBC"
        /// 24 = "Start Remove VBC"
        /// 25 = "Exit Set VBC"
        /// 26 = "Exit Remove VBC"
        /// 27 = "Change Level (or inhibit status)"
        /// 28 = "Start RBC Data Entry"
        /// 29 = "System Info request"
        /// 30 = "Change Train Running Number"
        /// 31 = "Exit Change Train Running Number"
        /// 32 = "Exit Change Level (or inhibit status)"
        /// 33 = "Exit RBC Data Entry"
        /// 34 = "Exit Driver Data Entry"
        /// 35 = "Spare"
        /// 36 = "Spare"
        /// 37 = "Spare"
        /// 38 = "Start procedure 'Train Integrity'"
        /// 39 = "Exit RBC contact"
        /// 40 = "Level entered"
        /// 41 = "start NTC 1 data entry"
        /// 42 = "start NTC 2 data entry"
        /// 43 = "start NTC 3 data entry"
        /// 44 = "start NTC 4 data entry"
        /// 45 = "start NTC 5 data entry"
        /// 46 = "start NTC 6 data entry"
        /// 47 = "start NTC 7 data entry"
        /// 48 = "start NTC 8 data entry"
        /// 49 = "Exit NTC data entry"
        /// 50 = "Exit NTC data entry selection"
        /// 51 = "Change Brake Percentage"
        /// 52 = "Change Doppler"
        /// 53 = "Change Wheel Diameter"
        /// 54 = "Exit maintenance"
        /// 55 = "System Version request"
        /// 56 = "Start Network ID"
        /// 57 = "Contact last RBC"
        /// 58 = "Settings"
        /// 59 = "Switch"
        /// 60 = "Exit brake percentage"
        /// 61 = "Exit RBC Network ID"
        /// 62..255 = "Spare"
        /// 
        /// Note 1: Values 3 and 4 also apply to customised Train Data Entry (packets EVC-60, EVC-61, EVC-160, EVC-161).
        /// Note 2: The number of the NTC x in 'start NTC x data entry' will match the sequence number of the related NTC in the list provided with EVC-31.
        /// </summary>
        public enum MMI_M_REQUEST : byte
        {
            Spare = 0,
            StartShunting = 1,
            ExitShunting = 2,
            StartTrainDataEntry = 3,
            ExitTrainDataEntry = 4,
            StartNonLeading = 5,
            ExitNonLeading = 6,
            StartOverrideEOA = 7,
            GeographicalPositionRequest = 8,
            Start = 9,
            RestoreAdhesionCoefficientToNonSlipperyRail = 10,
            SetAdhesionCoefficientToSlipperyRail = 11,
            ExitChangeSRrules = 12,
            ChangeSRrules = 13,
            ContinueShuntingOnDeskClosure = 14,
            ChangeDriverIdentity = 20,
            StartTrainDataView = 21,
            StartBrakeTest = 22,
            StartSetVBC = 23,
            StartRemoveVBC = 24,
            ExitSetVBC = 25,
            ExitRemoveVBC = 26,
            ChangeLevel = 27,
            StartRBCdataEntry = 28,
            SystemInfoRequest = 29,
            ChangeTrainRunningNumber = 30,
            ExitChangeTrainRunningNumber = 31,
            ExitChangeLevel = 32,
            ExitRBCdataEntry = 33,
            ExitDriverDataEntry = 34,
            StartProcedureTrainIntegrity = 38,
            ExitRBCcontact = 39,
            LevelEntered = 40,
            StartNTC1DataEntry = 41,
            StartNTC2DataEntry = 42,
            StartNTC3DataEntry = 43,
            StartNTC4DataEntry = 44,
            StartNTC5DataEntry = 45,
            StartNTC6DataEntry = 46,
            StartNTC7DataEntry = 47,
            StartNTC8DataEntry = 48,
            ExitNTCDataEntry = 49,
            ExitNTCDataEntrySelection = 50,
            ChangeBrakePercentage = 51,
            ChangeDoppler = 52,
            ChangeWheelDiameter = 53,
            ExitMaintenance = 54,
            SystemVersionRequest = 55,
            StartNetworkID = 56,
            ContactLastRBC = 57,
            Settings = 58,
            Switch = 59,
            ExitBrakePercentage = 60,
            ExitRBCNetworkID = 61
        }

        /// <summary>
        /// Button Event (pressed or released)
        /// 
        /// Values:
        /// 0 = "released"
        /// 1 = "pressed"
        /// </summary>
        public enum MMI_Q_BUTTON : byte
        {
            Released = 0,
            Pressed = 1
        }

        /// <summary>
        /// Enabling close button in EVC-14, EVC-20 and EVC-22.
        /// 
        /// Bits:
        /// 0 = "disable/enable close button"
        /// 1..7 = "Spare"
        /// 
        /// Note: Bit0 = 0 -> disable close button, Bit0 = 1 -> enable close button
        /// </summary>
        public enum MMI_Q_CLOSE_ENABLE : byte
        {
            Disabled = 0x00,
            Enabled = 0x80
        }

        /// <summary>
        /// This universal data check result variable provides control information
        /// how to display the related train data element in Echo Text/Data Entry Fields.
        /// Affected Echo Text/Data Entry Fields are indicated by MMI_NID_DATA
        /// 
        /// Values:
        /// 0 = "All checks have passed"
        /// 1 = "Technical Range Check failed"
        /// 2 = "Technical Resolution Check failed"
        /// 3 = "Technical Cross Check failed"
        /// 4 = "Operational Range Check failed"
        /// 5 = "Operational Cross Check failed"
        /// </summary>
        public enum Q_DATA_CHECK : byte
        {
            All_checks_passed = 0,
            Technical_Range_Check_failed = 1,
            Technical_Resolution_Check_failed = 2,
            Technical_Cross_Check_failed = 3,
            Operational_Range_Check_failed = 4,
            Operational_Cross_Check_failed = 5
        }

        /// <summary>
        /// EVC20 and EVC121 parameters. This configuration will display all levels used for 
        /// Crossrail project (+ Level3)
        /// </summary>
        #region EVC20 and EVC121 parameters

        public static MMI_Q_LEVEL_NTC_ID[] paramEvc20MmiQLevelNtcId =
                { MMI_Q_LEVEL_NTC_ID.ETCS_Level,
                MMI_Q_LEVEL_NTC_ID.ETCS_Level,
                MMI_Q_LEVEL_NTC_ID.ETCS_Level,
                MMI_Q_LEVEL_NTC_ID.ETCS_Level,
                MMI_Q_LEVEL_NTC_ID.STM_ID,
                MMI_Q_LEVEL_NTC_ID.STM_ID };

        public static MMI_M_CURRENT_LEVEL[] paramEvc20MmiMCurrentLevel =
                { MMI_M_CURRENT_LEVEL.NotLastUsedLevel,
                MMI_M_CURRENT_LEVEL.NotLastUsedLevel,
                MMI_M_CURRENT_LEVEL.NotLastUsedLevel,
                MMI_M_CURRENT_LEVEL.NotLastUsedLevel,
                MMI_M_CURRENT_LEVEL.NotLastUsedLevel,
                MMI_M_CURRENT_LEVEL.NotLastUsedLevel };

        public static MMI_M_LEVEL_FLAG[] paramEvc20MmiMLevelFlag =
                { MMI_M_LEVEL_FLAG.MarkedLevel,
                MMI_M_LEVEL_FLAG.MarkedLevel,
                MMI_M_LEVEL_FLAG.MarkedLevel,
                MMI_M_LEVEL_FLAG.MarkedLevel,
                MMI_M_LEVEL_FLAG.MarkedLevel,
                MMI_M_LEVEL_FLAG.MarkedLevel };

        public static MMI_M_INHIBITED_LEVEL[] paramEvc20MmiMInhibitedLevel =
                { MMI_M_INHIBITED_LEVEL.NotInhibited,
                MMI_M_INHIBITED_LEVEL.NotInhibited,
                MMI_M_INHIBITED_LEVEL.NotInhibited,
                MMI_M_INHIBITED_LEVEL.NotInhibited,
                MMI_M_INHIBITED_LEVEL.NotInhibited,
                MMI_M_INHIBITED_LEVEL.NotInhibited };

        public static MMI_M_INHIBIT_ENABLE[] paramEvc20MmiMInhibitEnable =
                { MMI_M_INHIBIT_ENABLE.AllowedForInhibiting,
                MMI_M_INHIBIT_ENABLE.AllowedForInhibiting,
                MMI_M_INHIBIT_ENABLE.AllowedForInhibiting,
                MMI_M_INHIBIT_ENABLE.AllowedForInhibiting,
                MMI_M_INHIBIT_ENABLE.AllowedForInhibiting,
                MMI_M_INHIBIT_ENABLE.AllowedForInhibiting };

        public static MMI_M_LEVEL_NTC_ID[] paramEvc20MmiMLevelNtcId =
                { MMI_M_LEVEL_NTC_ID.L0,
                MMI_M_LEVEL_NTC_ID.L1,
                MMI_M_LEVEL_NTC_ID.L2,
                MMI_M_LEVEL_NTC_ID.L3,
                MMI_M_LEVEL_NTC_ID.CBTC,
                MMI_M_LEVEL_NTC_ID.AWS_TPWS };
        #endregion


        #region EVC6 parameters

        public static string[] paramEvc6FixedTrainsetCaptions = new string[] { Enum.GetName(typeof(Fixed_Trainset_Captions), 1),
                                                                               Enum.GetName(typeof(Fixed_Trainset_Captions), 2),
                                                                               Enum.GetName(typeof(Fixed_Trainset_Captions), 3)};  
        
        #endregion

        /// <summary>
        /// Collection of flags used to enable standard buttons on DMI.
        /// </summary>
        public static EVC30_MMIRequestEnable.EnabledRequests standardFlags = 
            EVC30_MMIRequestEnable.EnabledRequests.EnableDoppler |
            EVC30_MMIRequestEnable.EnabledRequests.EnableWheelDiameter |
            EVC30_MMIRequestEnable.EnabledRequests.StartBrakeTest |
            EVC30_MMIRequestEnable.EnabledRequests.SetLocalTimeDateAndOffset |
            EVC30_MMIRequestEnable.EnabledRequests.RemoveVBC |
            EVC30_MMIRequestEnable.EnabledRequests.SetVBC |
            EVC30_MMIRequestEnable.EnabledRequests.SystemVersion |
            EVC30_MMIRequestEnable.EnabledRequests.Brightness |
            EVC30_MMIRequestEnable.EnabledRequests.Volume |
            EVC30_MMIRequestEnable.EnabledRequests.NonLeading |        
            EVC30_MMIRequestEnable.EnabledRequests.Shunting |
            EVC30_MMIRequestEnable.EnabledRequests.TrainData |
            EVC30_MMIRequestEnable.EnabledRequests.TrainRunningNumber |
            EVC30_MMIRequestEnable.EnabledRequests.Level |
            EVC30_MMIRequestEnable.EnabledRequests.ContactLastRBC |
            EVC30_MMIRequestEnable.EnabledRequests.EnterRBCData |
            EVC30_MMIRequestEnable.EnabledRequests.RadioNetworkID |
            EVC30_MMIRequestEnable.EnabledRequests.UseShortNumber |
            EVC30_MMIRequestEnable.EnabledRequests.DriverID |
            EVC30_MMIRequestEnable.EnabledRequests.Language |
            EVC30_MMIRequestEnable.EnabledRequests.SRSpeedDistance;

        /// <summary>
        /// Defines the identity of the activated cabin
        /// 
        /// Values:
        /// 0 = No active cabin
        /// 1 = Cabin 1 active
        /// 2 = Cabin 2 active
        /// </summary>
        public enum MMI_M_ACTIVE_CABIN : byte
        {
            NoCabinActive = 0,
            Cabin1Active = 1,
            Cabin2Active = 2
        }

        /// <summary>
        /// Track condition type.
        /// 
        /// Values:
        /// 0 = "Non Stopping Area"
        /// 1 = "Tunnel Stopping Area"
        /// 2 = "Sound Horn"
        /// 3 = "Panto"
        /// 4 = "Radio hole"
        /// 5 = "Air tightness"
        /// 6 = "Magnetic Shoe Brakes"
        /// 7 = "Eddy Current Brakes"
        /// 8 = "Regenerative Brakes"
        /// 9 = "Main power switch/Neutral Section"
        /// 10 = "Change of traction system, not fitted"
        /// 11 = "Change of traction system, AC 25 kV 50 Hz"
        /// 12 = "Change of traction system, AC 15 kV 16.7 Hz"
        /// 13 = "Change of traction system, DC 3 kV"
        /// 14 = "Change of traction system, DC 1.5 kV"
        /// 15 = "Change of traction system, DC 600/750 V"
        /// 16 = "Level Crossing"
        /// 17..63 = "reserved"
        /// </summary>
        public enum MMI_M_TRACKCOND_TYPE : byte
        {
            Non_Stopping_Area = 0,
            Tunnel_Stopping_Area = 1,
            Sound_Horn = 2,
            Pantograph = 3,
            Radio_hole = 4,
            Air_tightness = 5,
            Magnetic_Shoe_Brakes = 6,
            Eddy_Current_Brakes = 7,
            Regenerative_Brakes = 8,
            Main_power_switch_Neutral_Section = 9,
            Change_traction_not_fitted = 10,
            Change_traction_AC_25_kV_50_Hz = 11,
            Change_traction_AC_15_kV_16_7_Hz = 12,
            Change_traction_DC_3_kV = 13,
            Change_traction_DC_1_5_kV = 14,
            Change_traction_DC_600_750 = 15,
            Level_Crossing = 16,
            Invalid = 64
        }

        /// <summary>
        /// Required driver action
        /// 
        /// Values:
        /// 0 = With driver action (manual)
        /// 1 = Without driver action (automatic)
        /// </summary>
        public enum MMI_Q_TRACKCOND_ACTION : byte
        {
            WithDriverAction = 0,       // Manual
            WithoutDriverAction = 1     // Automatic
        }

        /// <summary>
        /// Variable describing step of the track condition.
        /// 
        /// Values:
        /// 0 = "Approaching area"
        /// 1 = "Announce area"
        /// 2 = "Inside area/active"
        /// 3 = "Leave area"
        /// 4 = "Remove TC"
        /// 5..15 = "Spare"
        /// </summary>
        public enum MMI_Q_TRACKCOND_STEP : byte
        {
            ApproachingArea = 0,
            AnnounceArea = 1,
            InsideArea_Active = 2,
            LeaveArea = 3,
            RemoveTC = 4,
            Spare5 = 5,
            Spare6 = 6,
            Spare7 = 7,
            Spare8 = 8,
            Spare9 = 9,
            Spare10 = 10,
            Spare11 = 11,
            Spare12 = 12,
            Spare13 = 13,
            Spare14 = 14,
            Spare15 = 15
        }

        /// <summary>
        /// Qualifier for the variable MMI_M_LEVEL_NTC_ID
        /// Used in EVC packets 20 and 121
        /// 
        /// Values:
        /// 0 = "MMI_M_LEVEL_NTC_ID contains an STM ID (0-255)"
        /// 1 = "MMI_M_LEVEL_NTC_ID contains a level number (0-3)"
        /// </summary>
        public enum MMI_Q_LEVEL_NTC_ID : byte
        {
            STM_ID = 0,
            ETCS_Level = 1
        }

        /// <summary>
        /// Last level used enum
        /// 
        /// Values:
        /// 0 = "MMI_M_LEVEL_STM_ID is not the latest used level"
        /// 1 ="MMI_M_LEVEL_STM_ID is the latest used level"
        /// </summary>
        public enum MMI_M_CURRENT_LEVEL : byte
        {
            NotLastUsedLevel = 0,
            LastUsedLevel = 1
        }

        /// <summary>
        /// Indicator for marked MMI_M_LEVEL_NTC_ID enum
        /// Used in EVC packets 20 and 121
        /// 
        /// Values:
        /// 0 = "MMI_M_LEVEL_NTC_ID is 'not marked'"
        /// 1 = "MMI_M_LEVEL_NTC_ID is 'marked'"
        /// </summary>
        public enum MMI_M_LEVEL_FLAG : byte
        {
            NotMarkedLevel = 0,
            MarkedLevel = 1
        }

        /// <summary>
        /// Inhibit status enum
        /// Used in EVC packets 20 and 121
        /// 
        /// Values:
        /// 0 = "MMI_M_LEVEL_NTC_ID is not inhibited"
        /// 1 = "MMI_M_LEVEL_NTC_ID is inhibited"
        /// </summary>
        public enum MMI_M_INHIBITED_LEVEL : byte
        {
            NotInhibited = 0,
            Inhibited = 1
        }

        /// <summary>
        /// Inhibit enabled enum.
        /// Used in EVC packets 20 and 121
        /// 
        /// Values:
        /// 0 = "MMI_M_LEVEL_NTC_ID is not allowed for inhibiting"
        /// 1 = "MMI_M_LEVEL_NTC_ID is allowed for inhibiting"
        /// </summary>
        public enum MMI_M_INHIBIT_ENABLE : byte
        {
            NotAllowedForInhibiting = 0,
            AllowedForInhibiting = 1
        }

        /// <summary>
        /// Level ID enum
        /// Note: In order to set ETCS level, the corresponding MMI_Q_LEVEL_NTC_ID needs to be TRUE.
        /// Used in EVC packets 20 and 121
        /// </summary>
        public enum MMI_M_LEVEL_NTC_ID : byte
        {
            L0 = 0,
            L1 = 1,
            L2 = 2,
            L3 = 3,
            AWS_TPWS = 20,
            CBTC = 50
        }

        /// <summary>
        /// A bit mask that, for each variable, tells if a data value is enabled (e.g. for 'edit' in EVC-6). 1== 'enabled'.
        /// 
        /// The variable supports the following use cases:
        /// 1.) Controls edit ability of related data object during TDE procedure(EVC-6, no data view).
        /// 2.) In case of a Train Data View procedure this variable controls visibility of data items
        ///     (ERA_ERTMS_015560, v3.4.0, chapter 11.5.1.5).
        /// 3.) In packet EVC-10 this variable controls highlighting of changed data items
        ///     (ERA_ERTMS_015560, v3.4.0, chapter 11.4.1.4, 10.3.3.5).
        /// </summary>
        [Flags]
        public enum MMI_M_DATA_ENABLE : ushort
        {
            NONE = 0,
            TrainSetID = 0x8000,
            TrainCategory = 0x4000,
            TrainLength = 0x2000,
            BrakePercentage = 0x1000,
            MaxTrainSpeed = 0x800,
            AxleLoadCategory = 0x400,
            Airtightness = 0x200,
            LoadingGauge = 0x100
        }

        /// <summary>
        /// Identifier of an MMI keyboard key name.
        /// 
        /// Values:
        /// 0 = "No dedicated key"
        /// 1 = "No"
        /// 2 = "Yes"
        /// 3 = "PASS1"
        /// 4 = "PASS2"
        /// 5 = "PASS3"
        /// 6 = "TILT1"
        /// 7 = "TILT2"
        /// 8 = "TILT3"
        /// 9 = "TILT4"
        /// 10 = "TILT5"
        /// 11 = "TILT6"
        /// 12 = "TILT7"
        /// 13 = "FP1"
        /// 14 = "FP2"
        /// 15 = "FP3"
        /// 16 = "FP4"
        /// 17 = "FG1"
        /// 18 = "FG2"
        /// 19 = "FG3"
        /// 20 = "FG4"
        /// 21 = "CAT A"
        /// 22 = "CAT HS17"
        /// 23 = "CAT B1"
        /// 24 = "CAT B2"
        /// 25 = "CAT C2"
        /// 26 = "CAT C3"
        /// 27 = "CAT C4"
        /// 28 = "CAT D2"
        /// 29 = "CAT D3"
        /// 30 = "CAT D4"
        /// 31 = "CAT D4XL"
        /// 32 = "CAT E4"
        /// 33 = "CAT E5"
        /// 34 = "G1"
        /// 35 = "GA"
        /// 36 = "GB"
        /// 37 = "GC"
        /// 38 = "Out of GC"
        /// 39 = "Non slippery rail"
        /// 40 = "Slippery rail"
        /// 41 = "Level 1"
        /// 42 = "Level 2"
        /// 43 = "Level 3"
        /// 44 = "Level 0"
        /// 45..255 = "Spare"
        /// 
        /// Note: the definition is according to preliminary SubSet-121 'NID_KEY' definition.
        /// </summary>
        public enum MMI_NID_KEY : byte
        {
            NoDedicatedKey = 0,
            No = 1,
            Yes = 2,
            PASS1 = 3,
            PASS2 = 4,
            PASS3 = 5,
            TILT1 = 6,
            TILT2 = 7,
            TILT3 = 8,
            TILT4 = 9,
            TILT5 = 10,
            TILT6 = 11,
            TILT7 = 12,

            FP1 = 13,
            FP2 = 14,
            FP3 = 15,
            FP4 = 16,
            FG1 = 17,
            FG2 = 18,
            FG3 = 19,
            FG4 = 20,
            CATA = 21,
            CATHS17 = 22,
            CATB1 = 23,
            CATB2 = 24,
            CATC2 = 25,
            CATC3 = 26,
            CATC4 = 27,
            CATD2 = 28,
            CATD3 = 29,
            CATD4 = 30,
            CATD4XL = 31,
            CATE4 = 32,
            CATE5 = 33,
            G1 = 34,
            GA = 35,
            GB = 36,
            GC = 37,
            OutofGC = 38,
            NonSlipperyRail = 39,
            SlipperyRail = 40,
            Level1 = 41,
            Level2 = 42,
            Level3 = 43,
            Level0 = 44
        }

        /// <summary>
        /// This is a maintenance parameter for Doppler radars 1 and 2. It gives the number of pulses per km-travelled distance.
        /// 
        /// Values:
        /// 0 = "No Radar 1 on board"
        /// 1..20000 = "Reserved"
        /// 85535..4294967289 = "Reserved"
        /// 4294967290 = "Technical Range Check failed"
        /// 4294967291 = "Technical Resolution Check failed"
        /// 4294967292 = "Technical Cross Check failed"
        /// 4294967293 = "Operational Range Check failed"
        /// 4294967294 = "Operational Cross Check failed"
        /// 4294967295 = "Reserved"
        /// 
        /// Note: All special values concerning cross/range checks are only used in packets EVC-40 and EVC-41.
        /// </summary>
        public enum MMI_M_PULSE_PER_KM : uint
        {
            NoRadarOnBoard = 0,
            TechnicalRangeCheckFailed = 4294967290,
            TechnicalResolutionCheckFailed = 4294967291,
            TechnicalCrossCheckFailed = 4294967292,
            OperationalRangeCheckFailed = 4294967293,
            OperationalCrossCheckFailed = 4294967294,
            Reserved = 4294967295
        }

        /// <summary>
        /// Wheel diameter for tachos 1 and 2
        /// 
        /// Values:
        /// 0..499 = "Reserved"
        /// 1501..65529 = "Reserved"
        /// 65530 = "Technical Range Check failed"
        /// 65531 = "Technical Resolution Check failed"
        /// 65532 = "Technical Cross Check failed"
        /// 65533 = "Operational Range Check failed"
        /// 65534 = "Operational Cross Check failed"
        /// 65535 = "Reserved"
        /// 
        /// Note: All special values concerning cross/range checks are only used in packets EVC-40 and EVC-41.
        /// </summary>
        public enum MMI_M_SDU_WHEEL_SIZE : ushort
        {
            TechnicalRangeCheckFailed = 65530,
            TechnicalResolutionCheckFailed = 65531,
            TechnicalCrossCheckFailed = 65532,
            OperationalRangeCheckFailed = 65533,
            OperationalCrossCheckFailed = 65534,
            Reserved = 65535
        }

        /// <summary>
        /// Accuracy of wheel diameter
        /// 
        /// Values:
        /// 33..249 = "Reserved"
        /// 250 = "Technical Range Check failed"
        /// 251 = "Technical Resolution Check failed"
        /// 252 = "Technical Cross Check failed"
        /// 253 = "Operational Range Check failed"
        /// 254 = "Operational Cross Check failed"
        /// 255 = "Reserved"
        /// 
        /// Note: All special values concerning cross/range checks are only used in packets EVC-40 and EVC-41.
        /// </summary>
        public enum MMI_M_WHEEL_SIZE_ERR : byte
        {
            TechnicalRangeCheckFailed = 250,
            TechnicalResolutionCheckFailed = 251,
            TechnicalCrossCheckFailed = 252,
            OperationalRangeCheckFailed = 253,
            OperationalCrossCheckFailed = 254,
            Reserved = 255
        }

        /// <summary>
        /// Indicates the content of the maintenance telegram
        /// 
        /// Values:
        /// 0 = "Wheel diameter"
        /// 1 = "Doppler"
        /// </summary>
        public enum MMI_Q_MD_DATASET : byte
        {
            WheelDiameter = 0,
            Doppler = 1
        }

        /// <summary>
        /// Fixed trainset captions used for Crossrail
        /// 
        /// FLU = "Fixed length unit" -> 9 cars
        /// RLU = "Reduced length unit" -> 7 cars
        /// Rescue = "Rescue" -> 18 cars
        /// </summary>
        public enum Fixed_Trainset_Captions : byte
        {
            FLU = 1,
            RLU = 2,
            Rescue = 3           
        }

        /// <summary>
        /// Bit-reverses a 32-bit number
        /// </summary>
        /// <param name="intToBeReversed"></param>
        /// <returns>Reversed 32-bit uint</returns>
        public static uint BitReverser32(uint intToBeReversed)
        {
            uint y = 0;

            for (int i = 0; i < 32; i++)
            {
                y <<= 1;
                y |= intToBeReversed & 1;
                intToBeReversed >>= 1;
            }

            return y;
        }

        /// <summary>
        /// Bit-reverses a 16-bit number
        /// </summary>
        /// <param name="intToBeReversed"></param>
        /// <returns>Reversed 16-bit uint</returns>
        public static ushort BitReverser16(ushort intToBeReversed)
        {
            int y = 0;

            for (int i = 0; i < 16; i++)
            {
                y <<= 1;
                y |= intToBeReversed & 1;
                intToBeReversed >>= 1;
            }

            ushort reversedInt = Convert.ToUInt16(y);
            return reversedInt;
        }

        /// <summary>
        /// Bit-reverses a 8-bit number
        /// </summary>
        /// <param name="byteToBeReversed"></param>
        /// <returns>Reversed byte</returns>
        public static byte BitReverser8(byte byteToBeReversed)
        {
            int y = 0;

            for (int i = 0; i < 8; i++)
            {
                y <<= 1;
                y |= byteToBeReversed & 1;
                byteToBeReversed >>= 1;
            }

            byte reversedByte = Convert.ToByte(y);
            return reversedByte;
        }
    }
}
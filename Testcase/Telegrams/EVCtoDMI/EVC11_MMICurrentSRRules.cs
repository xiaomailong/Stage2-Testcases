#region usings
using System;
using System.Collections.Generic;
using CL345;
using static Testcase.Telegrams.EVCtoDMI.Variables;
#endregion

namespace Testcase.Telegrams.EVCtoDMI
{
    /// <summary>
    /// This packet is sent sporadically by ETC and is intended to support the following use cases:
    /// 1.) Display current SR speed / distance data when entering 'SR speed / distance' window
    /// 2.) Display/change echo text after data checks have been performed by ETC; this as well includes control over the allowed driver actions in case some data check has failed
    /// 
    /// It also gives the ETC the ability to control the status/type of the "Yes" button, if specified by functional requirements for ETC and DMI.
    /// 
    /// Note: Parameter 'MMI_NID_DATA_ELEMENTS' distinguishes between use case 1.) and 2.)
    /// </summary>
    public static class EVC11_MMICurrentSRRules
    {
        private static SignalPool _pool;

        /// <summary>
        /// Initialise EVC-6 MMI Current Train Data telegram.
        /// </summary>
        /// <param name="pool">The SignalPool</param>
        public static void Initialise(SignalPool pool)
        {
            _pool = pool;
            DataElements = new List<DataElement>();

            // Set as dynamic
            _pool.SITR.SMDCtrl.ETCS1.CurrentSrRules.Value = 0x0008;

            // Set default values
            _pool.SITR.ETCS1.CurrentSrRules.MmiMPacket.Value = 11; // Packet ID
        }

        /// <summary>
        /// Send EVC-11 MMI Current SR Rules telegram.
        /// </summary>
        public static void Send()
        {
            if (DataElements.Count > 3)
                throw new ArgumentOutOfRangeException();
            
            ushort totalSizeCounter = 96;
        
            // Set number of train data elements
            _pool.SITR.ETCS1.CurrentSrRules.MmiNDataElements.Value = (ushort) DataElements.Count;

            totalSizeCounter = PopulateDataElements($"ETCS1_CurrentSrRules_EVC11CurrentSrRulesSub1",
                totalSizeCounter, DataElements, _pool);

            // Set the total length of the packet (adding MMI_M_BUTTONS length)
            _pool.SITR.ETCS1.CurrentSrRules.MmiLPacket.Value = Convert.ToUInt16((int)totalSizeCounter + 8);

            _pool.SITR.SMDCtrl.ETCS1.CurrentSrRules.Value = 0x09;
        }

        /// <summary>
        /// Distance on which the train is allowed to run in Staff Responsible mode.
        /// 
        /// Values:
        /// 0..100000 = "Distance in Staff Responsible Mode"
        /// 100001..4294967295  = "Not Used"
        /// </summary>
        public static uint MMI_L_STFF
        {
            get => _pool.SITR.ETCS1.CurrentSrRules.MmiLStff.Value;
            set => _pool.SITR.ETCS1.CurrentSrRules.MmiLStff.Value = value;
        }

        /// <summary>
        /// Speed value to override the default max Staff Responsible speed in the system
        /// 
        /// Values:
        /// 0..600 = "Speed Value"
        /// 601..65535 = "Reserved"
        /// 
        /// Note:
        /// According[SS026] Ch. 3, A.3.11 and Ch. 7.5.1, all speed resolution have 5 km/h.
        /// </summary>
        public static ushort MMI_V_STFF
        {
            get => _pool.SITR.ETCS1.CurrentSrRules.MmiVStff.Value;
            set => _pool.SITR.ETCS1.CurrentSrRules.MmiVStff.Value = value;
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
        /// Note: the definition is according to preliminary SubSet-121 'M_BUTTONS' definition.
        /// </summary>
        public static Variables.MMI_M_BUTTONS MMI_M_BUTTONS
        {
            set => _pool.SITR.ETCS1.CurrentSrRules.MmiMButtons.Value = (byte) value;
        }

        public static List<DataElement> DataElements { get; set; }
    }
}
using System;
using CL345;

namespace Testcase.Telegrams.EVCtoDMI
{
    /// <summary>
    /// The packet is used by the ETC to dynamically
    /// enable/disable generic and customised (i.e. project
    /// specific) procedures on the MMI.
    /// Note: The customisable contents of the packet
    /// (variables MMI_Q_EVC_PROJECT,
    /// MMI_M_CUST_PROC_ID,
    /// MMI_Q_CUST_REQUEST_ENABLE) may be
    /// customised by projects. This has to be specified in
    /// the project's documentation.
    /// </summary>
    static class EVC30_MMIRequestEnable
    {
        private static SignalPool _pool;

        public static void Initialise(SignalPool pool)
        {
            _pool = pool;

            // set default values
            _pool.SITR.ETCS1.EnableRequest.MmiMPacket.Value = 30;
            _pool.SITR.ETCS1.EnableRequest.MmiLPacket.Value = 128;
        }

        public static void Send()
        {
            _pool.SITR.SMDCtrl.ETCS1.EnableRequest.Value = 1;
        }

        public static void SendBlank()
        {
            _pool.SITR.ETCS1.EnableRequest.MmiNidWindow.Value = 255;
            _pool.SITR.Client.Write("ETCS1_EnableRequest_MmiQRequestEnable", new uint[2] { 0x00000000, 0x00000000 });
            _pool.SITR.SMDCtrl.ETCS1.EnableRequest.Value = 1;
        }

        /// <summary>
        /// Identifier of currently active ETCS windows. Not all
        /// possible windows are controlled/used by this
        /// variable in the current packet. The relevant
        /// windows are specified by related requirements.
        /// 
        /// Values:
        /// 0 = "Default"
        /// 1 = "Main"
        /// 2 = "Override"
        /// 3 = "Special"
        /// 4 = "Settings"
        /// 5 = "RBC contact" - Only accessed via
        /// 6 = "Train running number"
        /// 7 = "Level"
        /// 8 = "Driver ID"
        /// 9 = "radio network ID"
        /// 10 = "RBC data"
        /// 11 = "Train data"
        /// 12 = "SR speed/distance"
        /// 13 = "Adhesion"
        /// 14 = "Set VBC"
        /// 15 = "Remove VBC"
        /// 16 = "Train data validation"
        /// 17 = "Set VBC validation"
        /// 18 = "Remove VBC validation"
        /// 19 = "Data View"
        /// 20 = "System version"
        /// 21 = "NTC data entry selection"
        /// 22 = "NTC X data"
        /// 23 = "NTC X data validation"
        /// 24 = "NTC X data view"
        /// 25..252 = "Spare"
        /// 253 = "Language"
        /// 254 = "close current window, return to parent"
        /// 255 = "no window specified"
        /// Note1: The definition is according to preliminary SubSet-121 'NID_WINDOW' definition + some
        /// extension for BT specific design.
        /// Note2: The variable is already prepared for future use and contains all known possible windows; it
        /// is assumed that there are use cases where the variable is contained in a sent package, but there is
        /// no intention to do a statement about a specific window. Then the special value 'no window
        /// specified' can be used.
        /// </summary>
        public static ushort MMI_NID_WINDOW
        {
            set => _pool.SITR.ETCS1.EnableRequest.MmiNidWindow.Value = (byte)value;
        }


        /// <summary>
        /// This variable controls which generic driver requests shall be accessible by the driver
        /// 
        /// Note:
        /// Bit-using as following:
        /// 0 = "Start"
        /// 1 = "Driver ID"
        /// 2 = "Train data"
        /// 3 = "Level"
        /// 4 = "Train running number"
        /// 5 = "Shunting"
        /// 6 = "Exit Shunting"
        /// 7 = "Non-Leading"
        /// 8 = "Maintain Shunting"
        /// 9 = "EOA"
        /// 10 = "Adhesion"
        /// 11 = "SR speed / distance"
        /// 12 = "Train integrity"
        /// 13 = "Language"
        /// 14 = "Volume"
        /// 15 = "Brightness"
        /// 16 = "System version"
        /// 17 = "Set VBC"
        /// 18 = "Remove VBC"
        /// 19 = "Contact last RBC"
        /// 20 = "Use short number"
        /// 21 = "Enter RBC data"
        /// 22 = "Radio Network ID"
        /// 23 = "Geographical position"
        /// 24 = "End of data entry (NTC)"
        /// 25 = "Set local time, date and offset"
        /// 26 = "Set local offset"
        /// 27 = "Reserved"
        /// 28 = "Start Brake Test"
        /// 29 = "Enable wheel diameter"
        /// 30 = "Enable Doppler"
        /// 31 = "Enable brake percentage"
        /// </summary>
        public static EnabledRequests MMI_Q_REQUEST_ENABLE_HIGH
        {
            set
            {
                _pool.SITR.Client.Write("ETCS1_EnableRequest_MmiQRequestEnable", new uint[2] { Convert.ToUInt32 (value), 0x80000000 });
            }
                
        }

        // TODO To implement properly at some point
        ///// <summary>
        ///// This variable controls which generic driver requests shall be accessible by the driver
        ///// True = System Info enabled
        ///// 
        ///// Note:
        ///// Bit-using as following:
        ///// 0 = "System info"
        ///// 1..31 = not used, set to zero
        ///// </summary>
        //public static bool MMI_Q_REQUEST_ENABLE_LOW
        //{
        //    set
        //    {
        //        if (value)
        //            _pool.SITR.ETCS1.EnableRequest.MmiQRequestEnable.Value[1] = 0x80000000;
        //        else
        //            _pool.SITR.ETCS1.EnableRequest.MmiQRequestEnable.Value[1] = 0x00000000;
        //    }
        //}

        [Flags]
        public enum EnabledRequests : uint
        {
            None = 0,
            Start = 0x80000000,
            DriverID = 1 << 30,
            TrainData = 1 << 29,
            Level = 1 << 28,
            TrainRunningNumber = 1 << 27,
            Shunting = 1 << 26,
            ExitShunting = 1 << 25,
            NonLeading = 1 << 24,
            MaintainShunting = 1 << 23,
            EOA = 1 << 22,
            Adhesion = 1 << 21,
            SRSpeedDistance = 1 << 20,
            TrainIntegrity = 1 << 19,
            Language = 1 << 18,
            Volume = 1 << 17,
            Brightness = 1 << 16,
            SystemVersion = 1 << 15,
            SetVBC = 1 << 14,
            RemoveVBC = 1 << 13,
            ContactLastRBC = 1 << 12,
            UseShortNumber = 1 << 11,
            EnterRBCData = 1 << 10,
            RadioNetworkID = 1 << 9,
            GeographicalPosition = 1 << 8,
            EndOfDataEntryNTC = 1 << 7,
            SetLocalTimeDateAndOffset = 1 << 6,
            SetLocalOffset = 1 << 5,
            Reserved = 1 << 4,
            StartBrakeTest = 1 << 3,
            EnableWheelDiameter = 1 << 2,
            EnableDoppler = 1 << 1,
            EnableBrakePercentage = 1 << 0
        }
    }
}
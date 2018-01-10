#region usings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CL345;
using static Testcase.Telegrams.EVCtoDMI.Variables;

#endregion

namespace Testcase.Telegrams.EVCtoDMI
{
    /// <summary>
    /// This packet shall be sent when the driver is intended to enter/validate/view driver identity number
    /// </summary>
    public static class EVC14_MMICurrentDriverID
    {
        private static SignalPool _pool;

        /// <summary>
        /// Initialise EVC-14 MMI Current Driver ID telegram.
        /// </summary>
        /// <param name="pool">The SignalPool</param>
        public static void Initialise(SignalPool pool)
        {
            _pool = pool;

            // Set default values
            _pool.SITR.ETCS1.CurrentDriverId.MmiMPacket.Value = 14;
            _pool.SITR.ETCS1.CurrentDriverId.MmiLPacket.Value = 176;
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
        public static MMI_Q_CLOSE_ENABLE MMI_Q_CLOSE_ENABLE
        {
            set => _pool.SITR.ETCS1.CurrentDriverId.MmiQCloseEnable.Value = (byte) value;
        }

        /// <summary>
        /// Enable additional buttons in Driver ID window packet EVC-14
        /// 
        /// Bits:
        /// 0 = "TRN"
        /// 1 = "Settings"
        /// 2..7 = "Spare"
        /// </summary>
        public static MMI_Q_ADD_ENABLE_BUTTONS MMI_Q_ADD_ENABLE
        {
            set => _pool.SITR.ETCS1.CurrentDriverId.MmiQAddEnable.Value = (byte) value;
        }

        [Flags]
        public enum MMI_Q_ADD_ENABLE_BUTTONS : byte
        {
            Settings = 0x40,
            TRN = 0x80
        }

        /// <summary>
        /// Current driver identity
        /// 
        /// Values:
        /// 0 = "Empty string (null character)"
        /// 
        /// Note: 16 alphanumeric characters (ISO 8859-1, also known as Latin Alphabet #1).
        /// Note 1: If the value is unknown the table will be filled with null characters (0, not '0').
        /// Note 2: If Driver ID is shorter than 16 characters the free charcters will be filled with null characters.
        /// Note 3: If Driver ID is 16 characters there will be no null character in the string.
        /// </summary>
        public static string MMI_X_DRIVER_ID
        {
            set
            {
                if (value.Length > 16)
                    throw new ArgumentOutOfRangeException("Too many characters in Driver ID!");

                _pool.SITR.ETCS1.CurrentDriverId.MmiXDriverId.Value = value;
            }
        }

        /// <summary>
        /// Send EVC-14 MMI Current Driver ID telegram.
        /// </summary>
        public static void Send()
        {
            _pool.SITR.SMDCtrl.ETCS1.CurrentDriverId.Value = 0x0001;
        }
    }
}
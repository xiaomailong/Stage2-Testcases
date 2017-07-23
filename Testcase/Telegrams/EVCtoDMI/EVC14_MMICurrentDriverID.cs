using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CL345;

namespace Testcase.Telegrams
{
    /// <summary>
    /// This packet shall be sent when the driver is intended to enter/validate /view driver identity number
    /// </summary>
    static class EVC14_MMICurrentDriverID
    {
        private static SignalPool _pool;

        public static void Initialise(SignalPool pool)
        {
            _pool = pool;

            // set default values
            _pool.SITR.ETCS1.CurrentDriverId.MmiMPacket.Value = 14;
            _pool.SITR.ETCS1.CurrentDriverId.MmiLPacket.Value = 172;
        }

        /// <summary>
        /// Enabling close button in EVC-14, EVC-20 and EVC-22.
        /// True = enabled
        /// </summary>
        public static bool MMI_Q_CLOSE_ENABLE
        {
            set => _pool.SITR.ETCS1.CurrentDriverId.MmiQCloseEnable.Value = (byte) (value ? 0x80 : 0x00);
        }

        /// <summary>
        /// Enable additional buttons in Driver ID Window packet EVC-14
        /// Bits:
        /// 0 = "TRN"
        /// 1 = "Settings"
        /// 2..7 = "Spare"
        /// </summary>
        public static MMIQADDENABLEBUTTONS MMI_Q_ADD_ENABLE
        {
            set => _pool.SITR.ETCS1.CurrentDriverId.MmiQAddEnable.Value = (byte) value;
        }

        [Flags]
        public enum MMIQADDENABLEBUTTONS : byte
        {
            TRN = 0x80,
            Settings = 0x40
        }

        /// <summary>
        /// Current driver identity
        /// Values:
        /// 0 = "Empty string (null character)"
        /// Note: 16 alphanumeric characters (ISO 8859-1, also known as Latin Alphabet #1).
        /// Note 1: If the value is unknown the table will be filled with null characters (0, not '0').
        /// Note 2: If Driver ID is shorter than 16 characters the free charcters will be filled with null
        /// characters.
        /// Note 3: If Driver ID is 16 characters there will be no null character in the string.
        /// </summary>
        public static string MMI_X_DRIVER_ID
        {
            set
            {
                if (value.Length > 16)
                    throw new ArgumentOutOfRangeException();
                _pool.SITR.ETCS1.CurrentDriverId.MmiXDriverId.Value = value;
            }
        }

        public static void Send()
        {
            _pool.SITR.SMDCtrl.ETCS1.CurrentDriverId.Value = 1;
        }
    }
}
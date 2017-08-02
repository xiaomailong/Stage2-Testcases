using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CL345;

namespace Testcase.Telegrams.EVCtoDMI
{
    /// <summary>
    /// Packet containing operated system version.
    /// </summary>
    static class EVC34_MMISystemVersion
    {
        private static SignalPool _pool;
        private static byte _systemVersionX = 0;
        private static byte _systemVersionY = 0;

        /// <summary>
        /// Initialise an instance of EVC-34 telegram.
        /// </summary>
        /// <param name="pool"></param>
        public static void Initialise(SignalPool pool)
        {
            _pool = pool;

            // Set default values
            _pool.SITR.ETCS1.SystemVersion.MmiMPacket.Value = 34; // Packet ID
            _pool.SITR.ETCS1.SystemVersion.MmiLPacket.Value = 48; // Packet length
        }

        private static void SetOperatedSystemVersion()
        {
            _pool.SITR.ETCS1.SystemVersion.MmiMOperatedSystemVersion.Value = (ushort) (_systemVersionX << 8 | _systemVersionY);
        }

        /// <summary>
        /// Operated system version according to SS026 (X.Y - first byte is X).
        /// 
        /// Bits:
        /// 0..7 = "X : UNSIGNED8"
        /// Note: Version "X.Y"
        /// </summary>
        public static byte SYSTEM_VERSION_X
        {
            set
            {
                _systemVersionX = value;
                SetOperatedSystemVersion();
            }
        }

        /// <summary>
        /// Operated system version according to SS026 (X.Y - first byte is X).
        /// 
        /// Bits:
        /// 8..15 = "Y : UNSIGNED8"
        /// Note: Version "X.Y"
        /// </summary>
        public static byte SYSTEM_VERSION_Y
        {
            set
            {
                _systemVersionY = value;
                SetOperatedSystemVersion();
            }
        }

        /// <summary>
        /// Send EVC-34 MMI System Version telegram.
        /// </summary>
        public static void Send()
        {
            _pool.SITR.SMDCtrl.ETCS1.SystemVersion.Value = 1;
        }
    }
}
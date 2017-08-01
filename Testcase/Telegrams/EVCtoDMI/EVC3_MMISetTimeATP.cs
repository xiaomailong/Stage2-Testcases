using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CL345;

namespace Testcase.Telegrams.EVCtoDMI
{
    /// <summary>
    /// This packet shall be sent whenever the time is changed in the ETC clock function.
    /// Note that there is a corresponding message from MMI to ETC. The issue of the message depends on which of the units are selected as “clock master”.
    /// This may vary from system to system, but in a specific system, only the clock master is allowed to initiate the message.
    /// </summary>
    class EVC3_MMISetTimeATP
    {
        private static SignalPool _pool;

        /// <summary>
        /// Initialises an instance of telegram EVC-3 telegram.
        /// </summary>
        /// <param name="pool"></param>
        public static void Initialise(SignalPool pool)
        {
            _pool = pool;

            // Set default values
            _pool.SITR.ETCS1.SetTimeATP.MmiMPacket.Value = 3; // Packet ID
            _pool.SITR.ETCS1.SetTimeATP.MmiLPacket.Value = 72; // Packet length
        }

        /// <summary>
        /// UTC time as seconds since 01.01.1970, 00:00:00
        /// 
        /// Values:
        /// 0..4294967295
        /// </summary>
        public static uint MMI_T_UTC
        {
            set => _pool.SITR.ETCS1.SetTimeATP.MmiTUtc.Value = value;
            get => _pool.SITR.ETCS1.SetTimeATP.MmiTUtc.Value;
        }

        /// <summary>
        /// Time zone offset
        /// 
        /// Values:
        /// -48..+48 (resolution = 0.25 h)
        /// </summary>
        public static sbyte MMI_T_ZONE_OFFSET
        {
            set => _pool.SITR.ETCS1.SetTimeATP.MmiTZoneOffset.Value = value;
            get => _pool.SITR.ETCS1.SetTimeATP.MmiTZoneOffset.Value;
        }

        /// <summary>
        /// Send EVC-3 Set Time ATP telegram.
        /// </summary>
        public static void Send()
        {
            _pool.TraceInfo("ETCS->DMI: EVC-3 (MMI_SET_TIME_ATP) UTC Time = {0}, Time offset = {1}", MMI_T_UTC,
                MMI_T_ZONE_OFFSET);
            _pool.SITR.SMDCtrl.ETCS1.SetTimeATP.Value = 1;
        }
    }
}
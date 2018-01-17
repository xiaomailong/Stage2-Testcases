﻿#region usings

using System;
using CL345;

#endregion

namespace Testcase.Telegrams.EVCtoDMI
{
    /// <summary>
    /// This packet shall be sent whenever the time is changed in the ETC clock function.
    /// Note that there is a corresponding message from MMI to ETC.
    /// The issue of the message depends on which of the units are selected as “clock master”.
    /// This may vary from system to system, but in a specific system, only the clock master is allowed to initiate the message.
    /// </summary>
    public static class EVC3_MMISetTimeATP
    {
        private static SignalPool _pool;

        /// <summary>
        /// Initialise EVC-3 MMI Set Time ATP telegram.
        /// </summary>
        /// <param name="pool">The SignalPool</param>
        public static void Initialise(SignalPool pool)
        {
            _pool = pool;

            // Set default values
            _pool.SITR.ETCS1.SetTimeATP.MmiMPacket.Value = 3;
            _pool.SITR.ETCS1.SetTimeATP.MmiLPacket.Value = 72;
        }

        /// <summary>
        /// UTC time as seconds since 01.01.1970, 00:00:00
        /// 
        /// Values:
        /// 0..4294967295
        /// </summary>
        public static uint MMI_T_UTC
        {
            set { _pool.SITR.ETCS1.SetTimeATP.MmiTUtc.Value = value; }
            get { return _pool.SITR.ETCS1.SetTimeATP.MmiTUtc.Value; }
        }

        /// <summary>
        /// Time zone offset
        /// 
        /// Values:
        /// -48..+48 (resolution = 0.25 h)
        /// </summary>
        public static sbyte MMI_T_ZONE_OFFSET
        {
            set { _pool.SITR.ETCS1.SetTimeATP.MmiTZoneOffset.Value = value; }
            get { return _pool.SITR.ETCS1.SetTimeATP.MmiTZoneOffset.Value; }
        }

        /// <summary>
        /// Send EVC-3 Set Time ATP telegram.
        /// </summary>
        public static void Send()
        {
            _pool.TraceInfo(
                string.Format("ETCS->DMI: EVC-3 (MMI_SET_TIME_ATP) UTC Time = {0}, Time offset = {1}", MMI_T_UTC,
                    MMI_T_ZONE_OFFSET));
            _pool.SITR.SMDCtrl.ETCS1.SetTimeATP.Value = 0x0001;
        }
    }
}
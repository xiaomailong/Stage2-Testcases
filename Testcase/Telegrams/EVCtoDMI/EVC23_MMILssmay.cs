﻿#region usings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CL345;

#endregion

namespace Testcase.Telegrams.EVCtoDMI
{
    /// <summary>
    /// This packet is sent sporadically from ETC when LSSMA display shall be started/updated/stopped.
    /// </summary>
    public static class EVC23_MMILssma
    {
        private static SignalPool _pool;

        /// <summary>
        /// Initialse EVC-23 MMI_LSSMA telegram.
        /// </summary>
        /// <param name="pool"></param>
        public static void Initialise(SignalPool pool)
        {
            _pool = pool;

            // set default values
            _pool.SITR.ETCS1.CurrentTrainNumber.MmiMPacket.Value = 23;
            _pool.SITR.ETCS1.CurrentTrainNumber.MmiLPacket.Value = 48;
        }

        /// <summary>
        /// LSSMA speed
        /// Values:
        /// 0..600 = "Speed Value"
        /// 601..65534 = "Reserved"
        /// 65535 = "no LSSMA display"
        /// </summary>
        public static ushort MMI_V_LSSMA
        {
            set => _pool.SITR.ETCS1.Lssma.MmiVLssma.Value = value;
        }

        /// <summary>
        /// Send EVC-23 telegram.
        /// </summary>
        public static void Send()
        {
            _pool.SITR.SMDCtrl.ETCS1.Lssma.Value = 1;
        }
    }
}
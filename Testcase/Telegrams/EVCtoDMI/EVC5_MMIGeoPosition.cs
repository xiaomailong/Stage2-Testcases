using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CL345;

namespace Testcase.Telegrams.EVCtoDMI
{
    /// <summary>
    /// This packet contains the geographical position to be presented on request by the driver.
    /// </summary>
    public static class EVC5_MMIGeoPosition
    {
        private static SignalPool _pool;

        /// <summary>
        /// Initialise an instance of telegram EVC-5 telegram.
        /// </summary>
        /// <param name="pool"></param>
        public static void Initialise(SignalPool pool)
        {
            _pool = pool;

            // Set default values
            _pool.SITR.ETCS1.GeoPosition.MmiMPacket.Value = 5; // Packet ID
            _pool.SITR.ETCS1.GeoPosition.MmiLPacket.Value = 80; // Packet length
        }

        /// <summary>
        /// This is the train’s current geographical position, in absolute co-ordinates as defined by trackside.
        /// In case of single balises it indicates the absolute position of the last passed balise.
        /// Values:
        /// 0..8388607 (resolution = 1 m)
        /// -1 = "No more geo position report after this."
        /// </summary>
        public static int MMI_M_ABSOLUTPOS
        {
            set => _pool.SITR.ETCS1.GeoPosition.MmiMAbsolutpos.Value = value;
            get => _pool.SITR.ETCS1.GeoPosition.MmiMAbsolutpos.Value;
        }

        /// <summary>
        /// This is the train’s current geographical position given as an offset from last passed balise.
        /// Values:
        /// 0..32767 (resolution = 1 m)
        /// -1 = "Not applicable (i.e. MMI shall display _ABSOLUTPOS only)"
        /// </summary>
        public static short MMI_M_RELATIVPOS
        {
            set => _pool.SITR.ETCS1.GeoPosition.MmiMRelativpos.Value = value;
            get => _pool.SITR.ETCS1.GeoPosition.MmiMRelativpos.Value;
        }

        /// <summary>
        /// Send EVC-5 Geo Position telegram.
        /// </summary>
        public static void Send()
        {
            _pool.TraceInfo("ETCS->DMI: EVC-5 (MMI_GEO_POSITION) Absolute Postion = {0}, Relative Position = {1}",
                MMI_M_ABSOLUTPOS, MMI_M_RELATIVPOS);
            _pool.SITR.SMDCtrl.ETCS1.GeoPosition.Value = 1;
        }
    }
}
using CL345;
using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BT_CSB_Tools;
using BT_CSB_Tools.Logging;
using BT_CSB_Tools.Utils.Xml;
using BT_CSB_Tools.SignalPoolGenerator.Signals;
using BT_CSB_Tools.SignalPoolGenerator.Signals.MwtSignal;
using BT_CSB_Tools.SignalPoolGenerator.Signals.PdSignal.Misc;
using System.Windows.Forms;
using Testcase.Telegrams;
using Testcase.Telegrams.EVCtoDMI;
using Testcase.TemporaryFunctions;

namespace Testcase.Telegrams.EVCtoDMI
{
    /// <summary>
    /// This packet contains status information for the driver and shall be sent to the MMI when­ever any of the status has changed.
    /// </summary>
    static class EVC2_MMIStatus
    {
        private static SignalPool _pool;
        private static byte _mmiMAdhesion; // Adhesion can only be set by trackside for Crossrail project
        private static Variables.MMI_M_ACTIVE_CABIN _mmiMActiveCabin;
        private static bool _mmiMOverrideEOA;

        /// <summary>
        /// Initialise EVC-2 MMI_STATUS telegram
        /// </summary>
        /// <param name="pool"></param>
        public static void Initialise(SignalPool pool)
        {
            _pool = pool;

            // Set default values
            _pool.SITR.ETCS1.Status.MmiMPacket.Value = 2;
            _pool.SITR.ETCS1.Status.MmiLPacket.Value = 72;
        }

        /// <summary>
        /// Train Running Number stored in EVC
        /// 
        /// Values:
        /// Note: Definition according to Subset-026, 7.5.1.92.
        /// Binary Coded Decimal
        /// For each digit: 
        /// Values 0-9 Digit value
        /// Values A-E Not used, spare
        /// Value F No digit (used for shorter numbers or when not applicable) or special value (see below)
        /// E.g. “1234567” is coded as 0x1234567F
        /// Special values:
        /// 0xFFFFFFFF	'Unknown Train Running Number'
        /// </summary>
        public static uint TrainRunningNumber
        {
            get => _pool.SITR.ETCS1.Status.MmiNidOperation.Value;
            set => _pool.SITR.ETCS1.Status.MmiNidOperation.Value = value;
        }

        /// <summary>
        /// Adhesion status
        /// 
        /// Values:
        /// 0x0 = "No low adhesion"
        /// 0x1 = "Low Adhesion by Driver"
        /// 0x2 = "Low Adhesion from Trackside"
        /// </summary>
        public static byte MMI_M_ADHESION
        {
            get => _mmiMAdhesion;

            set
            {
                _mmiMAdhesion = value;
                SetAlias();
            }
        }

        public static Variables.MMI_M_ACTIVE_CABIN MMI_M_ACTIVE_CABIN
        {
            set
            {
                _mmiMActiveCabin = value;
                SetAlias();
            }
        }

        /// <summary>
        /// Variable informing the driver that the EOA is currently being overridden.
        /// 
        /// Values:
        /// False = "No Override EOA function active"
        /// True = "Override EOA function is active (e.g. passing a stop signal)"
        /// </summary>
        public static bool MMI_M_OVERRIDE_EOA
        {
            get => _mmiMOverrideEOA;

            set
            {
                _mmiMOverrideEOA = value;
                SetAlias();
            }
        }

        private static void SetAlias()
        {
            var mmiMActiveCabin = (byte) _mmiMActiveCabin;
            var mmiMOverrideEOA = Convert.ToByte(_mmiMOverrideEOA);

            _pool.SITR.ETCS1.Status.EVC2alias1.Value =
                (byte) (_mmiMAdhesion << 6 | mmiMActiveCabin << 4 | mmiMOverrideEOA << 3);
        }

        /// <summary>
        /// Send EVC-2 telegram
        /// </summary>
        public static void Send()
        {
            _pool.TraceInfo("ETCS->DMI: EVC-2 (MMI_STATUS)");
            _pool.SITR.SMDCtrl.ETCS1.Status.Value = 1;
        }
    }
}
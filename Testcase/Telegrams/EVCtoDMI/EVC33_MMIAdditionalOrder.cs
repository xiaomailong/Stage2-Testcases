using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CL345;

namespace Testcase.Telegrams.EVCtoDMI
{
    /// <summary>
    /// Packet replacing old ATP-3 packet. This packet is the additional order or announcement.
    /// </summary>
    public static class EVC33_MMIAdditionalOrder
    {
        private static SignalPool _pool;
        private static MMI_Q_TRACKCOND_ACTION _mmiQTrackCondAction;
        private static byte _mmiQTrackCondStep;

        public static void Initialise(SignalPool pool)
        {
            _pool = pool;

            // Set default values
            _pool.SITR.ETCS1.AdditionalOrder.MmiMPacket.Value = 33; // Packet ID
            _pool.SITR.ETCS1.AdditionalOrder.MmiLPacket.Value = 56; // Packet length
        }

        /// <summary>
        /// Sets EVC33_alias_1
        /// 
        /// Bits:
        /// 0 = evc33_spare3
        /// 1 = evc33_spare2
        /// 2 = evc33_spare1
        /// 3 = MMI_Q_TRACKCOND_ACTION -> 0 (manual) or 1 (automatic)
        /// 4..7 = MMI_Q_TRACKCOND_STEP -> 0 = "Approaching area", 1 = "Announce area", 2 = "Inside area/active", 3 = "Leave area", 4 = "Remove TC"
        /// </summary>
        private static void SetAlias()
        {
            _pool.SITR.ETCS1.AdditionalOrder.EVC33alias1.Value =
                (byte) (_mmiQTrackCondStep << 4 | (byte) _mmiQTrackCondAction << 3);
        }

        /// <summary>
        /// Send EVC33 Additional Order telegram.
        /// </summary>
        public static void Send()
        {
            _pool.TraceInfo("ETCS->DMI: EVC-33 (MMI_ADDITIONAL_ORDER)");
            _pool.TraceInfo("EVC-33: MMI_NID_TRACKCOND = {0}", MMI_NID_TRACKCOND);
            _pool.TraceInfo("EVC-33: MMI_M_TRACKCOND_TYPE = {0}", MMI_M_TRACKCOND_TYPE);
            _pool.TraceInfo("EVC-33: MMI_Q_TRACKCOND_STEP = {0}", _mmiQTrackCondStep);
            _pool.TraceInfo("EVC-33: MMI_Q_TRACKCOND_ACTION = {0}", _mmiQTrackCondAction);
            _pool.SITR.SMDCtrl.ETCS1.AdditionalOrder.Value = 1;
        }

        /// <summary>
        /// NID assigned to Track Condition.
        /// 
        /// Values:
        /// 0..31 = "NID assigned to Track Condition.
        /// 32.255 = "spare"
        /// </summary>
        public static byte MMI_NID_TRACKCOND
        {
            set => _pool.SITR.ETCS1.AdditionalOrder.MmiNidTrackcond.Value = value;
            get => _pool.SITR.ETCS1.AdditionalOrder.MmiNidTrackcond.Value;
        }

        /// <summary>
        /// Sets the track condition type.
        /// 
        /// Values:
        /// 0 = "Non Stopping Area"
        /// 1 = "Tunnel Stopping Area"
        /// 2 = "Sound Horn"
        /// 3 = "Panto"
        /// 4 = "Radio hole"
        /// 5 = "Air tightness"
        /// 6 = "Magnetic Shoe Brakes"
        /// 7 = "Eddy Current Brakes"
        /// 8 = "Regenerative Brakes"
        /// 9 = "Main power switch/Neutral Section"
        /// 10 = "Change of traction system, not fitted"
        /// 11 = "Change of traction system, AC 25 kV 50 Hz"
        /// 12 = "Change of traction system, AC 15 kV 16.7 Hz"
        /// 13 = "Change of traction system, DC 3 kV"
        /// 14 = "Change of traction system, DC 1.5 kV"
        /// 15 = "Change of traction system, DC 600/750 V"
        /// 16 = "Level Crossing"
        /// 17..63 = "reserved"
        /// </summary>
        public static Variables.MMI_M_TRACKCOND_TYPE MMI_M_TRACKCOND_TYPE
        {
            set => _pool.SITR.ETCS1.AdditionalOrder.MmiMTrackcondType.Value = (byte)value;
            get => (Variables.MMI_M_TRACKCOND_TYPE)_pool.SITR.ETCS1.AdditionalOrder.MmiMTrackcondType.Value;
        }

        /// <summary>
        /// Variable describing step of the track condition.
        /// 
        /// Values:
        /// 0 = "Approaching area"
        /// 1 = "Announce area"
        /// 2 = "Inside area/active"
        /// 3 = "Leave area"
        /// 4 = "Remove TC"
        /// 5..15 = "Spare"
        /// </summary>
        public static byte MMI_Q_TRACKCOND_STEP
        {
            set
            {
                _mmiQTrackCondStep = value;
                SetAlias();
            }
        }

        /// <summary>
        /// Required driver action
        /// 
        /// Values:
        /// 0 = "With driver action (manual)"
        /// 1 = "Without driver action (automatic)"
        /// </summary>
        public static MMI_Q_TRACKCOND_ACTION MMI_Q_TRACKCOND_ACTION
        {
            set
            {
                _mmiQTrackCondAction = value;
                SetAlias();
            }
        }
    }

    /// <summary>
    /// Required driver action
    /// 
    /// Values:
    /// 0 = With driver action (manual)
    /// 1 = Without driver action (automatic)
    /// </summary>
    public enum MMI_Q_TRACKCOND_ACTION : byte
    {
        WithDriverAction = 0, // manual
        WithoutDriverAction = 1 // automatic
    }
}
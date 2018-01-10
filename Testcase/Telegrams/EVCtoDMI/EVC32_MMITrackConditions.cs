using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CL345;

namespace Testcase.Telegrams.EVCtoDMI
{
    /// <summary>
    /// Packet replacing old ATP-2 packet. This packet transmits track condition information.
    /// </summary>
    public static class EVC32_MMITrackConditions
    {
        private static SignalPool _pool;
        private static byte _evc32Alias1;

        /// <summary>
        /// Initialise EVC-32 MMI_Track_Conditions telegram.
        /// </summary>
        /// <param name="pool"></param>
        public static void Initialise(SignalPool pool)
        {
            _pool = pool;

            // Set default values
            _pool.SITR.ETCS1.TrackConditions.MmiMPacket.Value = 32; // Packet ID
            _pool.SITR.SMDCtrl.ETCS1.TrackConditions.Value = 0x8;
            TrackConditions = new List<TrackCondition>();
        }

        /// <summary>
        /// Send EVC-32 Track Conditions telegram.
        /// </summary>
        public static void Send()
        {
            ushort numberOfTrackConditions = (ushort) TrackConditions.Count;

            // Only 31 track conditions allowed in telegram
            if (numberOfTrackConditions > 31)
                throw new ArgumentOutOfRangeException("Too many track conditions.");

            // Set number of track conditions
            _pool.SITR.ETCS1.TrackConditions.MmiNTrackconditions.Value = numberOfTrackConditions;

            // Initial packet size
            ushort totalSizeCounter = 64;

            // Base string used by SignalPool
            var baseString = "ETCS1_TrackConditions_EVC32TrackConditionSub";

            // For all track conditions
            for (int k = 0; k < numberOfTrackConditions; k++)
            {
                // Write SignalPool signals
                if (k < 10)
                {
                    _pool.SITR.Client.Write($"{baseString}0{k}_MmiOTrackcondAnnounce",
                        TrackConditions[k].MMI_O_TRACKCOND_ANNOUNCE);
                    _pool.SITR.Client.Write($"{baseString}0{k}_MmiOTrackcondStart",
                        TrackConditions[k].MMI_O_TRACKCOND_START);
                    _pool.SITR.Client.Write($"{baseString}0{k}_MmiOTrackcondEnd",
                        TrackConditions[k].MMI_O_TRACKCOND_END);
                    _pool.SITR.Client.Write($"{baseString}0{k}_MmiNidTrackcond", TrackConditions[k].MMI_NID_TRACKCOND);
                    _pool.SITR.Client.Write($"{baseString}0{k}_MmiMTrackcondType",
                        TrackConditions[k].MMI_M_TRACKCOND_TYPE);
                    _pool.SITR.Client.Write($"{baseString}0{k}_EVC32alias2", TrackConditions[k].EVC_32_ALIAS_2);
                }

                else
                {
                    _pool.SITR.Client.Write($"{baseString}{k}_MmiOTrackcondAnnounce",
                        TrackConditions[k].MMI_O_TRACKCOND_ANNOUNCE);
                    _pool.SITR.Client.Write($"{baseString}{k}_MmiOTrackcondStart",
                        TrackConditions[k].MMI_O_TRACKCOND_START);
                    _pool.SITR.Client.Write($"{baseString}{k}_MmiOTrackcondEnd",
                        TrackConditions[k].MMI_O_TRACKCOND_END);
                    _pool.SITR.Client.Write($"{baseString}{k}_MmiNidTrackcond", TrackConditions[k].MMI_NID_TRACKCOND);
                    _pool.SITR.Client.Write($"{baseString}{k}_MmiMTrackcondType",
                        TrackConditions[k].MMI_M_TRACKCOND_TYPE);
                    _pool.SITR.Client.Write($"{baseString}{k}_EVC32alias2", TrackConditions[k].EVC_32_ALIAS_2);
                }

                totalSizeCounter += 128;
            }

            _pool.SITR.ETCS1.TrackConditions.MmiLPacket.Value = totalSizeCounter;

            _pool.TraceInfo("ETCS->DMI: EVC-32 (MMI_TRACK_CONDITIONS)");
            _pool.TraceInfo($"EVC-32: MMI_Q_TRACKCOND_UPDATE = {MMI_Q_TRACKCOND_UPDATE}");
            _pool.TraceInfo($"EVC-32: Number of track conditions = {numberOfTrackConditions}");
            _pool.SITR.SMDCtrl.ETCS1.TrackConditions.Value = 0x9;
        }

        /// <summary>
        /// Sets EVC32_alias_1
        /// 
        /// Bits:
        /// 0-3 = evc32_spare2
        /// 4-5 = evc32_spare1
        /// 6 = evc32_spare0
        /// 7 = MMI_Q_TRACKCOND_UPDATE
        /// </summary>
        private static void SetAlias1()
        {
            _pool.SITR.ETCS1.TrackConditions.EVC32alias1.Value =
                (byte) (_evc32Alias1 << 7);
        }

        /// <summary>
        /// Specifies if stored data should be updated.
        /// 
        /// Values:
        /// 0 = "The data in this packet shall replace all stored data"
        /// 1 = "The data in this packet shall update already stored data"
        /// </summary>
        public static byte MMI_Q_TRACKCOND_UPDATE
        {
            get
            {
                byte evc32Alias1 = _pool.SITR.ETCS1.TrackConditions.EVC32alias1.Value;
                return (byte) (evc32Alias1 >> 7);
            }

            set
            {
                _evc32Alias1 = value;
                SetAlias1();
            }
        }

        /// <summary>
        /// List of track conditions.
        /// </summary>
        public static List<TrackCondition> TrackConditions { get; set; }
    }

    /// <summary>
    /// Track condition information.
    /// </summary>
    public class TrackCondition
    {
        private static int _mmiOTrackcondAnnounce;
        private static int _mmiOTrackcondStart;
        private static int _mmiOTrackcondEnd;
        private static byte _mmiNidTrackcond;
        private static byte _mmiMTrackcondType;

        private static byte _evc32Alias2;
        private static byte _mmiQTrackcondStep;
        private static byte _mmiQTrackcondActionStart;
        private static byte _mmiQTrackcondActionEnd;

        private static void SetAlias2()
        {
            _evc32Alias2 = (byte) (_mmiQTrackcondStep << 4 | _mmiQTrackcondActionStart << 3 |
                                   _mmiQTrackcondActionEnd << 2);
        }

        /// <summary>
        /// Track condition announcement. This position can be adjusted depending on supervision.
        /// 
        /// Values:
        /// -2147483647 = "No announcement location exist or is already passed"
        /// -1..2147483647 = "Position of the announcement location. Range and units like OBU_TR_O_TRAIN"
        /// </summary>
        public int MMI_O_TRACKCOND_ANNOUNCE
        {
            set => _mmiOTrackcondAnnounce = value;
            get => _mmiOTrackcondAnnounce;
        }

        /// <summary>
        /// Start location of track condition. This position can be adjusted depending on supervision.
        /// 
        /// Values:
        /// -2147483647 = "Start location already passed"
        /// -1..2147483647 = "Position of the start location. Range and units like OBU_TR_O_TRAIN"
        /// </summary>
        public int MMI_O_TRACKCOND_START
        {
            set => _mmiOTrackcondStart = value;
            get => _mmiOTrackcondStart;
        }

        /// <summary>
        /// End location of track condition. This position can be adjusted depending on supervision.
        /// 
        /// Values:
        /// -2147483647 = "End location already passed"
        /// -1..2147483647 = "Position of the end location. Range and units like OBU_TR_O_TRAIN"
        /// </summary>
        public int MMI_O_TRACKCOND_END
        {
            set => _mmiOTrackcondEnd = value;
            get => _mmiOTrackcondEnd;
        }

        /// <summary>
        /// NID assigned to track condition.
        /// 
        /// Values:
        /// 0..31 = "NID assigned to TC"
        /// 32..255 = "spare"
        /// </summary>
        public byte MMI_NID_TRACKCOND
        {
            set => _mmiNidTrackcond = value;
            get => _mmiNidTrackcond;
        }

        /// <summary>
        /// Type of track condition.
        /// 
        /// Values:
        /// 0 = "Non Stopping Area"
        /// 1 = "Tunnel Stopping Area"
        /// 2 = "Sound Horn"
        /// 3 = "Pantograph"
        /// 4 = "Radio hole"
        /// 5 = "Air tightness"
        /// 6 = "Magnetic Shoe Brakes"
        /// 7 = "Eddy Current Brakes"
        /// 8 = "Regenerative Brakes"
        /// 9 = "Main power switch/Neutral Section"
        /// 10 = "Change of traction system, not fitted"
        /// 11 = "Change of traction system, AC 25 kV 50 Hz"
        /// 12 = "Change of traction system, AC 15 kV 16.7 Hz"
        /// 13 = "Change of traction system, DC 3 kV"\
        /// 14 = "Change of traction system, DC 1.5 kV"
        /// 15 = "Change of traction system, DC 600/750 V"
        /// 16 = "Level Crossing"
        /// 17..63 = "reserved"
        /// </summary>
        public Variables.MMI_M_TRACKCOND_TYPE MMI_M_TRACKCOND_TYPE
        {
            set => _mmiMTrackcondType = (byte) value;
            get => (Variables.MMI_M_TRACKCOND_TYPE) _mmiMTrackcondType;
        }

        /// <summary>
        /// Sets EVC32_alias_2
        /// 
        /// Bits:
        /// 0-1 = evc32_spare4
        /// 2 = "MMI_Q_TRACKCOND_ACTION_END"
        /// 3 = "MMI_Q_TRACKCOND_ACTION_START"
        /// 4..7 = MMI_Q_TRACKCOND_STEP ->
        ///     0 = "Approaching area", 1 = "Announce area", 2 = "Inside area/active", 3 = "Leave area", 4 = "Remove TC"
        /// </summary>
        public byte EVC_32_ALIAS_2
        {
            get => _evc32Alias2;
            set => _evc32Alias2 = value;
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
        public Variables.MMI_Q_TRACKCOND_STEP MMI_Q_TRACKCOND_STEP
        {
            get { return (Variables.MMI_Q_TRACKCOND_STEP) _mmiQTrackcondStep; }

            set
            {
                _mmiQTrackcondStep = (byte) value;
                SetAlias2();
            }
        }

        /// <summary>
        /// Type of action at start location.
        /// 
        /// Values:
        /// 0 = "With driver action (manual)"
        /// 1 = "Without driver action (automatic)"
        /// </summary>
        public Variables.MMI_Q_TRACKCOND_ACTION MMI_Q_TRACKCOND_ACTION_START
        {
            get { return (Variables.MMI_Q_TRACKCOND_ACTION) _mmiQTrackcondActionStart; }

            set
            {
                _mmiQTrackcondActionStart = (byte) value;
                SetAlias2();
            }
        }

        /// <summary>
        /// Type of action at end location.
        /// 
        /// Values:
        /// 0 = "With driver action (manual)"
        /// 1 = "Without driver action (automatic)"
        /// </summary>
        public Variables.MMI_Q_TRACKCOND_ACTION MMI_Q_TRACKCOND_ACTION_END
        {
            get { return (Variables.MMI_Q_TRACKCOND_ACTION) _mmiQTrackcondActionEnd; }

            set
            {
                _mmiQTrackcondActionEnd = (byte) value;
                SetAlias2();
            }
        }
    }
}
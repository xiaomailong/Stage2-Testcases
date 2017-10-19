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
    /// This packet shall be sent on request from the driver. The packet contains misc. system info for operational and initial maintenance purpose.
    /// </summary>
    public static class EVC24_MMISystemInfo
    {
        private static SignalPool _pool;
        const string BaseString = "ETCS1_SystemInfo_EVC24SystemInfoSub";

        /// <summary>
        /// Initialse EVC-24 MMI_System_Info telegram.
        /// </summary>
        /// <param name="pool"></param>
        public static void Initialise(SignalPool pool)
        {
            _pool = pool;
            MMI_NID_NTC = new List<byte>();
            MMI_NID_STMSTATE = new List<byte>();

            // set default values
            _pool.SITR.SMDCtrl.ETCS1.SystemInfo.Value = 0x0008;
            _pool.SITR.ETCS1.SystemInfo.MmiMPacket.Value = 24;
        }

        /// <summary>
        /// Send EVC-24 telegram.
        /// </summary>
        public static void Send()
        {
            if (MMI_NID_NTC.Count > 10)
                throw new ArgumentOutOfRangeException();
            if (MMI_NID_STMSTATE.Count > 10)
                throw new ArgumentOutOfRangeException();
            if (MMI_NID_NTC.Count != MMI_NID_STMSTATE.Count)
                throw new Exception("Number of MMI_NID_NTC and MMI_NID_STMSTATE do not match!");

            _pool.SITR.ETCS1.SystemInfo.MmiNNidntc.Value = (ushort)MMI_NID_NTC.Count;

            // for each element of the list
            for (var nidNtcIndex = 0; nidNtcIndex < MMI_NID_NTC.Count; nidNtcIndex++)
            {
                // Get the NIDNTC info
                var _nidNtc = MMI_NID_NTC[nidNtcIndex];
                var _nidStmState = MMI_NID_STMSTATE[nidNtcIndex];

                // Set the value on EVC-24 packet according to the NIDNTC index
                if (nidNtcIndex < 10)
                {
                    _pool.SITR.Client.Write($"{BaseString}10{nidNtcIndex}_MmiNidNtc", _nidNtc);
                    _pool.SITR.Client.Write($"{BaseString}10{nidNtcIndex}_MmiNidStmstate", _nidStmState);
                }
                else
                {
                    _pool.SITR.Client.Write($"{BaseString}1{nidNtcIndex}_MmiNidNtc", _nidNtc);
                    _pool.SITR.Client.Write($"{BaseString}1{nidNtcIndex}_MmiNidStmstate", _nidStmState);
                }
            }

            // Set the total length of the packet
            _pool.SITR.ETCS1.SystemInfo.MmiLPacket.Value = (ushort)(240 + MMI_NID_NTC.Count * 16);

            // Send telegram
            _pool.SITR.SMDCtrl.ETCS1.SystemInfo.Value = 0x0009;
        }

        /// <summary>
        /// Vehicle identity number provided by ETCS Onboard on request of the ETCS-MMI
        /// Values:
        /// 0 = "'Unknown'"
        /// </summary>
        public static uint MMI_NID_ENGINE_1
        {
            get => _pool.SITR.ETCS1.SystemInfo.MmiNidEngine1.Value;
            set => _pool.SITR.ETCS1.SystemInfo.MmiNidEngine1.Value = value;
        }

        /// <summary>
        /// Next timeout for brake test in MM-DD-HH-MM as seconds since 01.01.1970, 00:00:00
        /// </summary>
        public static uint MMI_T_TIMEOUT_BRAKE
        {
            get => _pool.SITR.ETCS1.SystemInfo.MmiTTimeoutBrake.Value;
            set => _pool.SITR.ETCS1.SystemInfo.MmiTTimeoutBrake.Value = value;
        }

        /// <summary>
        /// Next timeout for BTM test in MM-DD-HH-MM as seconds since 01.01.1970, 00:00:00
        /// </summary>
        public static uint MMI_T_TIMEOUT_BTM
        {
            get => _pool.SITR.ETCS1.SystemInfo.MmiTTimeoutBtm.Value;
            set => _pool.SITR.ETCS1.SystemInfo.MmiTTimeoutBtm.Value = value;
        }

        /// <summary>
        /// Next timeout for TBSW test in MM-DD-HH-MM as seconds since 01.01.1970, 00:00:00
        /// </summary>
        public static uint MMI_T_TIMEOUT_TBSW
        {
            get => _pool.SITR.ETCS1.SystemInfo.MmiTTimeoutTbsw.Value;
            set => _pool.SITR.ETCS1.SystemInfo.MmiTTimeoutTbsw.Value = value;
        }

        /// <summary>
        /// This variable contains ETC version X.Y.Z (first byte is X)
        /// </summary>
        public static uint MMI_M_ETC_VER
        {
            get => _pool.SITR.ETCS1.SystemInfo.MmiMEtcVer.Value;
            set => _pool.SITR.ETCS1.SystemInfo.MmiMEtcVer.Value = value;
        }

        /// <summary>
        /// Current HW configuration
        /// 
        /// Bits:
        /// 0 = "Not used (MMI 1 always installed)"
        /// 1 = "Normal MMI 2 installed"
        /// 2 = "Redundant MMI 1 installed"
        /// 3 = "Redundant MMI 2 installed"
        /// 4 = "BTM antenna 1 installed"
        /// 5 = "BTM antenna 2 installed"
        /// 6 = "Radio modem 1 installed"
        /// 7 = "Radio modem 2 installed"
        /// 8 = "Spare"
        /// 9 = "DRU installed"
        /// 10= "Euroloop BTM(s) installed"
        /// 11..15 = "Spare" 
        /// </summary>
        public static ushort MMI_M_AVAIL_SERVICES
        {
            get => _pool.SITR.ETCS1.SystemInfo.MmiMAvailServices.Value;
            set => _pool.SITR.ETCS1.SystemInfo.MmiMAvailServices.Value = value;
        }

        /// <summary>
        /// Current brake configuration
        /// Bits:
        /// 0 = "SB available"
        /// 1 = "Spare"
        /// 2 = "0 = EB as RTW, 1 = SB as RTW"
        /// 3 = "0 = Release TCO on brake release, 1 = Release TCO when coasting"
        /// 4 = "TCO feedback available"
        /// 5 = "Soft isolation allowed"
        /// 6 = "Monitoring of EB1 cut-off enabled"
        /// 7 = "Monitoring of EB2 cut-off enabled"
        /// </summary>
        public static byte MMI_M_BRAKE_CONFIG
        {
            get => _pool.SITR.ETCS1.SystemInfo.MmiMBrakeConfig.Value;
            set => _pool.SITR.ETCS1.SystemInfo.MmiMBrakeConfig.Value = value;
        }

        /// <summary>
        /// Installed levels
        /// Bits:
        /// 0 = "Level 0 installed"
        /// 1 = "Level NTC installed"
        /// 2 = "Level 1 installed"
        /// 3 = "Level 2 installed"
        /// 4 = "Level 3 installed"
        /// 5..7 = "Spare"
        /// </summary>
        public static byte MMI_M_LEVEL_INST
        {
            get => _pool.SITR.ETCS1.SystemInfo.MmiMLevelInst.Value;
            set => _pool.SITR.ETCS1.SystemInfo.MmiMLevelInst.Value = value;
        }

        /// <summary>
        /// NTC Identity. This variable identifies the non-ETCS track equipment on a given section of line for which the train requires NTC support (via e.g. STM or standalone system).
        /// (The definition of this variable is done by ERA ref [ETCS_VARIABLES])
        /// 
        /// Note: Refer to[ETCS_VARIABLES].
        /// Values not yet assigned to a dedicated NTC shall be handled as Not Defined.
        /// In case of an insertion of text instead of values of MMI_NID_NTC (e.g.text messages) undefined values shall lead to textstring ‘<Unknown>’.
        /// 
        /// Note 1: Value 255 is used in packets EVC-25 and EVC-26 to indicate termination.
        /// </summary>
        public static List<byte> MMI_NID_NTC { get; set; }

        /// <summary>
        /// This variable contains the current state of a STM.
        /// Values:
        /// 0 = "Reserved (mapped to NP for consistency)"
        /// 1 = "Power On (PO)"
        /// 2 = "Configuration (CO)"
        /// 3 = "Data Entry (DE)"
        /// 4 = "Cold Standby (CS)"
        /// 5 = "Reserved (mapped to CS for consistency)"
        /// 6 = "Hot Standby"
        /// 7 = "Data Available (DA)"
        /// 8 = "Failure (FA)"
        /// 9..255 = "not used"
        /// </summary>
        public static List<byte> MMI_NID_STMSTATE { get; set; }
    }
}
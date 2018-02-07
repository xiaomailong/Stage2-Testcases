#region usings

using System;
using CL345;

#endregion

namespace Testcase.Telegrams.EVCtoDMI
{
    /// <summary>
    /// This packet shall be sent when the ETC requests the driver to select level.
    /// The packet contains a list of ETCS and NTC levels and related additional status information.
    /// Possible use cases are:
    ///     display of 'default level list',
    ///     display of 'trackside supported level list',
    ///     display of 'inhibit level list'.
    /// </summary>
    public static class EVC31_MMINTCDeSelect
    {
        private static TestcaseBase _pool;
        private static byte[] _nidNtc;
        private static ushort _nNidNtc;

        /// <summary>
        /// Initialise an instance of EVC-31 MMI Ntc DeSelect telegram.
        /// </summary>
        /// <param name="pool"></param>
        public static void Initialise(TestcaseBase pool)
        {
            _pool = pool;

            // Set as dynamic
            _pool.SITR.SMDCtrl.ETCS1.NtcDeSelect.Value = 0x0008;

            // Set default values
            _pool.SITR.ETCS1.NtcDeSelect.MmiMPacket.Value = 31; // Packet ID
        }

        private static void SetNtcInfoK()
        {
            // Check that every array has the same length
            if (_nidNtc.Length > 11)
            {
                throw new ArgumentOutOfRangeException();
            }

            // Determine how many Nid_Ntc will be sent
            _nNidNtc = (ushort) _nidNtc.Length;

            // Populate telegram with MMI_N_NIDNTC
            _pool.SITR.ETCS1.NtcDeSelect.MmiNNidntc.Value = _nNidNtc;

            for (var k = 0; k < _nNidNtc; k++)
            {
                // Populate telegram with dynamic fields
                if (k < 10)
                {
                    _pool.SITR.Client.Write("ETCS1_NtcDeSelect_EVC31NtcDeSelectSub0" + k + "_MmiNidNtc", _nidNtc[k]);
                }
                else
                {
                    _pool.SITR.Client.Write("ETCS1_NtcDeSelect_EVC31NtcDeSelectSub" + k + "_MmiNidNtc", _nidNtc[k]);
                }
            }
        }

        /// <summary>
        /// Send EVC-31 telegram to the DMI.
        /// </summary>
        public static void Send()
        {
            _pool.SITR.ETCS1.NtcDeSelect.MmiLPacket.Value = (ushort) (56 + _nNidNtc * 8);
            _pool.SITR.SMDCtrl.ETCS1.NtcDeSelect.Value = 0x000B;
            _pool.WaitForAck(_pool.SITR.SMDStat.ETCS1.NtcDeSelect);
        }

        /// <summary>
        /// NTC Identity
        /// 
        /// Values not yet assigned to a dedicated NTC shall be handled as Not Defined.
        /// In case of an insertion of text instead of values of MMI_NID_NTC(e.g.text messages) 
        /// undefined values shall lead to textstring ‘<Unknown>’.
        /// Note 1: Value 255 is used in packets EVC-25 and EVC-26 to indicate termination. 
        /// </summary>
        public static byte[] MMI_NID_NTC
        {
            set
            {
                _nidNtc = value;
                SetNtcInfoK();
            }
        }

        /// <summary>
        /// Enable bit mask for up to 8 NTCs.
        /// 
        /// Note:
        /// Bit-Mask used as following:
        /// 'xxxxxxx1' = NTC 1
        /// 'xxxxxx1x' = NTC 2
        /// 'xxxxx1xx' = NTC 3
        /// 'xxxx1xxx' = NTC 4
        /// 'xxx1xxxx' = NTC 5
        /// 'xx1xxxxx' = NTC 6
        /// 'x1xxxxxx' = NTC 7
        /// '1xxxxxxx' = NTC 8
        /// </summary>
        public static MMI_Q_NTC_ENABLE Mmi_Q_Ntc_Enable
        {
            set { _pool.SITR.ETCS1.NtcDeSelect.MmiQNtcEnable.Value = (byte) value; }
        }

        [Flags]
        public enum MMI_Q_NTC_ENABLE : byte
        {
            NTC1 = 1 << 0,
            NTC2 = 1 << 1,
            NTC3 = 1 << 2,
            NTC4 = 1 << 3,
            NTC5 = 1 << 4,
            NTC6 = 1 << 5,
            NTC7 = 1 << 6,
            NTC8 = 1 << 7
        }
    }
}
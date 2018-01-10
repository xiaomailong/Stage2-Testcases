#region usings

using CL345;

#endregion

namespace Testcase.Telegrams.EVCtoDMI
{
    public static class EVC28_MMIEchoedSetVBCData
    {
        private static SignalPool _pool;

        /// <summary>
        /// Initialise EVC-28 MMI Echoed Set VBC Data telegram.
        /// (VBC = Virtual Balise Cover)
        /// </summary>
        /// <param name="pool">The SignalPool</param>
        public static void Initialise(SignalPool pool)
        {
            _pool = pool;

            // Set default values
            _pool.SITR.ETCS1.EchoedSetVbcData.MmiMPacket.Value = 28;
            _pool.SITR.ETCS1.EchoedSetVbcData.MmiLPacket.Value = 64;

            MMI_M_VBC_CODE_ = 0xff;
        }

        /// <summary>
        /// Send EVC-28 MMI Echoed Set VBC Data telegram.
        /// </summary>
        public static void Send()
        {
            _pool.SITR.SMDCtrl.ETCS1.EchoedRemoveVbcData.Value = 0x0001;
        }

        /// <summary>
        /// VBC Code
        /// Enter the value needed and it will be automatically inverted
        /// 
        /// Values:
        /// 0..9 = "NID_C"
        /// 10..15 = "NID_VBCMK"
        /// 16..23 = "T_VBC"
        /// 24..31 = "spare"
        /// </summary>
        public static uint MMI_M_VBC_CODE_
        {
            set
            {
                uint vbcCode_ = value;

                _pool.SITR.ETCS1.EchoedRemoveVbcData.MmiMVbcCodeR.Value = ~vbcCode_;
            }
        }
    }
}
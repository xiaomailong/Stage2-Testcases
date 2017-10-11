#region usings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CL345;

#endregion

namespace Testcase.Telegrams.EVCtoDMI
{
    /// <summary>
    /// This packet is sent sporadically from ETC when the 'Remove VBC' procedure
    /// is ongoing and is intended to support the following use cases:
    /// 1. Prompt the driver to enter a VBC code
    /// 2. Display/change echo text after data checks have been performed by EVC;
    ///     this also includes control over the allowed driver actions in case some data check has failed
    /// It also gives the ETC the ability to control the status/type of the "Yes" button,
    ///     if specified by functional requirements for ETC and DMI.
    /// 
    /// Note: Parameter 'MMI_N_VBC' distinguishes between use case 1 and 2.
    /// </summary>
    public static class EVC29_MMIEchoedRemoveVBCData
    {
        private static SignalPool _pool;

        /// <summary>
        /// Initialise EVC-29 MMI_Echoed_Remove_VBC_Data telegram
        /// (VBC = Virtual Balise Cover)
        /// </summary>
        /// <param name="pool"></param>
        public static void Initialise(SignalPool pool)
        {
            _pool = pool;

            // Set default values
            _pool.SITR.ETCS1.EchoedRemoveVbcData.MmiMPacket.Value = 29; // Packet ID
            _pool.SITR.ETCS1.EchoedRemoveVbcData.MmiLPacket.Value = 64;

            MMI_M_VBC_CODE_ = 0xff;
        }

        /// <summary>
        /// Send EVC-29 MMI_Echoed_Remove_VBC_Data telegram
        /// </summary>
        public static void Send()
        {
            _pool.SITR.SMDCtrl.ETCS1.EchoedRemoveVbcData.Value = 0x0001;
        }

        /// <summary>
        /// VBC Code
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

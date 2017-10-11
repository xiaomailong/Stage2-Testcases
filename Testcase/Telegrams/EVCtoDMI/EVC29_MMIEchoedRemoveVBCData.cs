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
    /// This packet shall be sent from ETC to MMI when the driver has finished the 'Remove VBC' data entry 
    /// by pressing the "Yes" button and all checks have passed. The packet starts the 'Remove VBC' Data Validation window / procedure
    /// at MMI.  the "Yes" button, if specified by functional requirements for ETC and DMI.
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

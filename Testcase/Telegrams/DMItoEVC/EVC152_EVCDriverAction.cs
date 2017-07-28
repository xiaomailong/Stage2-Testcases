using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using CL345;

namespace Testcase.Telegrams
{
    /// <summary>
    /// This packet shall be sent when the corresponding driver action is  performed.
    ///The data is used by ETC to record the driver actions in JRU.
    /// </summary>
    static class EVC152_MMIDriverAction
    {
        private static SignalPool _pool;

        public static void Receive(byte mmiMDriverAction)
        {
            bool bResult = false;

            // Checking packet id
            _pool.SITR.CCUO.ETCS1DriverAction.MmiMPacket.Value.Equals(152);
            // Checking packet length
            _pool.SITR.CCUO.ETCS1DriverAction.MmiLPacket.Value.Equals(40);
            // Checking MMI_M_DRIVER_ACTION 
            bResult = _pool.SITR.CCUO.ETCS1DriverAction.MmiMDriverAction.Value.Equals(mmiMDriverAction);
            if (bResult)
            {
                _pool.TraceInfo("EVC-101 received: MMI_M_DRIVER_ACTION = {0}", mmiMDriverAction);
            }
        }
    }
}
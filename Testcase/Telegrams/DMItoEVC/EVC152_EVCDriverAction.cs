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

        public static void Receive(byte mmiDriverAction)
        {
            // Checking packet id
            _pool.SITR.CCUO.ETCS1DriverAction.MmiMPacket.Equals(152);
            // Checking packet length
            _pool.SITR.CCUO.ETCS1DriverAction.MmiLPacket.Equals(40);
            // Checking MMI_M_DRIVER_ACTION 
            _pool.SITR.CCUO.ETCS1DriverAction.MmiMDriverAction.Equals(mmiDriverAction);
        }
    }
}
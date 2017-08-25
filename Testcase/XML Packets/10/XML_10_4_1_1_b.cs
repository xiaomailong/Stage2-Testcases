#region usings

using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BT_Tools;
using BT_CSB_Tools;
using BT_CSB_Tools.Logging;
using BT_CSB_Tools.Utils.Xml;
using BT_CSB_Tools.SignalPoolGenerator.Signals;
using BT_CSB_Tools.SignalPoolGenerator.Signals.MwtSignal;
using BT_CSB_Tools.SignalPoolGenerator.Signals.MwtSignal.Misc;
using BT_CSB_Tools.SignalPoolGenerator.Signals.PdSignal;
using BT_CSB_Tools.SignalPoolGenerator.Signals.PdSignal.Misc;
using CL345;
using Testcase.Telegrams.EVCtoDMI;

#endregion

namespace Testcase.XML
{
    /// <summary>
    /// Values of 10.4.1.1.b.xml file
    /// </summary>
    static class XML_10_4_1_1_b
    {
        private static SignalPool _pool;

        public static void Send(SignalPool pool)
        {
            _pool = pool;

            //EVC24MMISystemInfo.MMI_NID_ENGINE_1 = 1234;
            //EVC24MMISystemInfo.MMI_T_TIMEOUT_BRAKES = 0x5695224c;         // 1452614220
            //EVC24MMISystemInfo.MMI_T_TIMEOUT_BTM = 0x54b3eecc;            // 1421078220
            //EVC24MMISystemInfo.MMI_T_TIMEOUT_TBSW = 0x538b4d4c;           // 1401638220
            //EVC24MMISystemInfo.MMI_ETC_VER = 0xffaa0f;                    // 16755215
            //EVC24MMISystemInfo.MMI_M_AVAIL_SERVICES = 0xffff;             // 65535 

            // Discrepancy betwee spec (config = 55)
            //EVC24MMISystemInfo.MMI_M_BRAKE_CONFIG = 55;                   // 236 in xml
            //EVC24MMISystemInfo.MMI_M_LEVEL_INSTALLED = 248;

            //EVC24MMISystemInfo.Send();
        }
    }
}
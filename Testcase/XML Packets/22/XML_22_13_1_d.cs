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
    /// Values of 22.13.1.d.xml file
    /// </summary>
    static class XML_22_13_1_d
    {
        private static SignalPool _pool;

        public static void Send(SignalPool pool)
        {
            _pool = pool;

            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.None;
            EVC30_MMIRequestEnable.Send();

            EVC30_MMIRequestEnable.MMI_NID_WINDOW = 4;      // Settings
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.SetLocalTimeDateAndOffset |
                                                               EVC30_MMIRequestEnable.EnabledRequests.SetLocalOffset;

            EVC30_MMIRequestEnable.Send();            

        }
    }
}
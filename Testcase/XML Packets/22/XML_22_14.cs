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
    /// Values of 22.14.xml file
    /// </summary>
    static class XML_22_14
    {
        private static SignalPool _pool;

        public static void Send(SignalPool pool)
        {
            _pool = pool;

            EVC34_MMISystemVersion.SYSTEM_VERSION_X = 0xff;
            EVC34_MMISystemVersion.SYSTEM_VERSION_Y = 0xff;
            EVC34_MMISystemVersion.Send();

            /*
            // Test spec does not say anything about closing the window as in xml (the NID value should be 254 for that)
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.None;
            EVC30_MMIRequestEnable.Send();

            EVC30_MMIRequestEnable.MMI_NID_WINDOW = 4;  // Settings
            EVC30_MMIRequestEnable.Send();
            */
        }
    }
}
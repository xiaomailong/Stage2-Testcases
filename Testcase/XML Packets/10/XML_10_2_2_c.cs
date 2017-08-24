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
    /// Values of 10.2.2.c.xml file
    /// </summary>
    static class XML_10_2_2_c
    {
        private static SignalPool _pool;

        public static void Send(SignalPool pool)
        {
            _pool = pool;

            // EVC20_MMISelectLevel.MMI_Q_CLOSE_ENABLE = 0;
            // EVC20_MMISelectLevel.Send();

            _pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                      "1. ‘Close’ button in Level window is disabled.");

            System.Threading.Thread.Sleep(10000);

            // EVC20_MMISelectLevel.MMI_Q_CLOSE_ENABLE = 1;
            //EVC20_MMISelectLevel.Send();

            _pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                      "1. ‘Close’ button in Level window is enabled.");

        }
    }
}
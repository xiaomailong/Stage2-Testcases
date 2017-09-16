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
using Testcase.Telegrams.DMItoEVC;

#endregion

namespace Testcase.XML
{
    /// <summary>
    /// Values of 18.5.xml file
    /// </summary>
    static class XML_18_5
    {
        private static SignalPool _pool;

        public static void Send(SignalPool pool)
        {
            _pool = pool;

            EVC2_MMIStatus.TrainRunningNumber = 0xa12f;         // an invalid number
            EVC2_MMIStatus.MMI_M_ADHESION = 0;
            EVC2_MMIStatus.MMI_M_ACTIVE_CABIN = Variables.MMI_M_ACTIVE_CABIN.Cabin1Active;
            EVC2_MMIStatus.MMI_M_OVERRIDE_EOA = false;

            EVC2_MMIStatus.Send();

            _pool.Wait_Realtime(500);
            EVC2_MMIStatus.Send();

            _pool.Wait_Realtime(500);
            EVC2_MMIStatus.Send();

            _pool.Wait_Realtime(500);
            EVC2_MMIStatus.Send();

            _pool.Wait_Realtime(500);
            EVC2_MMIStatus.Send();
        }
    }
}
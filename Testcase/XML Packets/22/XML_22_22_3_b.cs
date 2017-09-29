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
    /// Values of 22.22.3.b.xml file
    /// </summary>
    static class XML_22_22_3_b
    {
        private static SignalPool _pool;

        public static void Send(SignalPool pool)
        {
            _pool = pool;

            EVC50_MMICurrentBrakePercentage.MMI_M_M_BP_ORIG = 50;
            EVC50_MMICurrentBrakePercentage.MMI_M_BP_MEASURED = 254;
            EVC50_MMICurrentBrakePercentage.MMI_M_BP_CURRENT = 255;
            EVC50_MMICurrentBrakePercentage.Send();            

        }
    }
}
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
    /// Values of 22.5.1.b.xml file
    /// </summary>
    static class XML_22_5_1_b
    {
        private static SignalPool _pool;

        public static void Send(SignalPool pool)
        {
            _pool = pool;

            EVC20_MMISelectLevel.MMI_Q_CLOSE_ENABLE = Variables.MMI_Q_CLOSE_ENABLE.Disabled;

            EVC20_MMISelectLevel.MMI_Q_LEVEL_NTC_ID = null;
            EVC20_MMISelectLevel.MMI_M_CURRENT_LEVEL = null;
            EVC20_MMISelectLevel.MMI_M_LEVEL_FLAG = null;
            EVC20_MMISelectLevel.MMI_M_INHIBITED_LEVEL = null;
            EVC20_MMISelectLevel.MMI_M_INHIBIT_ENABLE = null;
            EVC20_MMISelectLevel.MMI_M_LEVEL_NTC_ID = null;

            EVC20_MMISelectLevel.Send();            

        }
    }
}
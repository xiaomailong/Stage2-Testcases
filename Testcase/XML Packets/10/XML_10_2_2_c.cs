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

            EVC20_MMISelectLevel.MMI_Q_CLOSE_ENABLE = Variables.MMI_Q_CLOSE_ENABLE.Disabled;
            EVC20_MMISelectLevel.Send();

            _pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                      "1. ‘Close’ button in Level window is disabled.");

            _pool.Wait_Realtime(10000);

            EVC20_MMISelectLevel.MMI_Q_CLOSE_ENABLE = Variables.MMI_Q_CLOSE_ENABLE.Enabled;
            EVC20_MMISelectLevel.MMI_Q_LEVEL_NTC_ID = new Variables.MMI_Q_LEVEL_NTC_ID[] { Variables.MMI_Q_LEVEL_NTC_ID.ETCS_Level };
            EVC20_MMISelectLevel.MMI_M_CURRENT_LEVEL = new Variables.MMI_M_CURRENT_LEVEL[] { Variables.MMI_M_CURRENT_LEVEL.LastUsedLevel };
            EVC20_MMISelectLevel.MMI_M_LEVEL_FLAG = new Variables.MMI_M_LEVEL_FLAG[] { Variables.MMI_M_LEVEL_FLAG.MarkedLevel };
            EVC20_MMISelectLevel.MMI_M_INHIBITED_LEVEL = new Variables.MMI_M_INHIBITED_LEVEL[] { Variables.MMI_M_INHIBITED_LEVEL.NotInhibited };
            EVC20_MMISelectLevel.MMI_M_INHIBIT_ENABLE = new Variables.MMI_M_INHIBIT_ENABLE[] { Variables.MMI_M_INHIBIT_ENABLE.AllowedForInhibiting };
            EVC20_MMISelectLevel.MMI_M_LEVEL_NTC_ID = new Variables.MMI_M_LEVEL_NTC_ID[] { Variables.MMI_M_LEVEL_NTC_ID.L1 };
            EVC20_MMISelectLevel.Send();

            _pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                      "1. ‘Close’ button in Level window is enabled.");

        }
    }
}
﻿#region usings

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
using static Testcase.Telegrams.EVCtoDMI.Variables;

#endregion

namespace Testcase.XML
{
    /// <summary>
    /// Values of 22.5.3.b.xml file
    /// </summary>
    static class XML_22_5_3_b
    {
        private static SignalPool _pool;

        public static void Send(SignalPool pool)
        {
            _pool = pool;

            EVC20_MMISelectLevel.MMI_Q_CLOSE_ENABLE = MMI_Q_CLOSE_ENABLE.Disabled;
            
            EVC20_MMISelectLevel.MMI_Q_LEVEL_NTC_ID = new Variables.MMI_Q_LEVEL_NTC_ID[]
                                                          { Variables.MMI_Q_LEVEL_NTC_ID.ETCS_Level,
                                                            Variables.MMI_Q_LEVEL_NTC_ID.ETCS_Level };
            EVC20_MMISelectLevel.MMI_M_CURRENT_LEVEL = new Variables.MMI_M_CURRENT_LEVEL[]
                                                           { Variables.MMI_M_CURRENT_LEVEL.NotLastUsedLevel,
                                                             Variables.MMI_M_CURRENT_LEVEL.NotLastUsedLevel };
            EVC20_MMISelectLevel.MMI_M_LEVEL_FLAG = new Variables.MMI_M_LEVEL_FLAG[]
                                                        { Variables.MMI_M_LEVEL_FLAG.MarkedLevel,
                                                          Variables.MMI_M_LEVEL_FLAG.MarkedLevel };
            EVC20_MMISelectLevel.MMI_M_INHIBITED_LEVEL = new Variables.MMI_M_INHIBITED_LEVEL[]
                                                             { Variables.MMI_M_INHIBITED_LEVEL.NotInhibited,
                                                               Variables.MMI_M_INHIBITED_LEVEL.Inhibited };
            EVC20_MMISelectLevel.MMI_M_INHIBIT_ENABLE = new Variables.MMI_M_INHIBIT_ENABLE[]
                                                            { Variables.MMI_M_INHIBIT_ENABLE.AllowedForInhibiting,
                                                              Variables.MMI_M_INHIBIT_ENABLE.AllowedForInhibiting };
            EVC20_MMISelectLevel.MMI_M_LEVEL_NTC_ID = new Variables.MMI_M_LEVEL_NTC_ID[]
                                                          { Variables.MMI_M_LEVEL_NTC_ID.L0,
                                                            Variables.MMI_M_LEVEL_NTC_ID.L2 };

            EVC20_MMISelectLevel.Send();

        }
    }
}
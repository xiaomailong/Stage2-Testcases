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
using static Testcase.Telegrams.EVCtoDMI.Variables;

#endregion

namespace Testcase.XML
{
    /// <summary>
    /// Values of 22.5.2.c.xml file
    /// </summary>
    static class XML_22_5_2_c
    {
        private static SignalPool _pool;

        public static void Send(SignalPool pool)
        {
            _pool = pool;

            EVC20_MMISelectLevel.MMI_Q_CLOSE_ENABLE = MMI_Q_CLOSE_ENABLE.Disabled;

            Variables.MMI_Q_LEVEL_NTC_ID[] paramEvc20MmiQLevelNtcId =
                { MMI_Q_LEVEL_NTC_ID.ETCS_Level,
                MMI_Q_LEVEL_NTC_ID.ETCS_Level,
                MMI_Q_LEVEL_NTC_ID.ETCS_Level };
            Variables.MMI_M_CURRENT_LEVEL[] paramEvc20MmiMCurrentLevel =
                { MMI_M_CURRENT_LEVEL.NotLastUsedLevel,
                MMI_M_CURRENT_LEVEL.NotLastUsedLevel,
                MMI_M_CURRENT_LEVEL.NotLastUsedLevel };
            Variables.MMI_M_LEVEL_FLAG[] paramEvc20MmiMLevelFlag =
                { MMI_M_LEVEL_FLAG.MarkedLevel,
                MMI_M_LEVEL_FLAG.MarkedLevel,
                MMI_M_LEVEL_FLAG.MarkedLevel };
            Variables.MMI_M_INHIBITED_LEVEL[] paramEvc20MmiMInhibitedLevel =
                { MMI_M_INHIBITED_LEVEL.NotInhibited,
                MMI_M_INHIBITED_LEVEL.NotInhibited,
                MMI_M_INHIBITED_LEVEL.NotInhibited };
            Variables.MMI_M_INHIBIT_ENABLE[] paramEvc20MmiMInhibitEnable =
                { MMI_M_INHIBIT_ENABLE.AllowedForInhibiting,
                MMI_M_INHIBIT_ENABLE.AllowedForInhibiting,
                MMI_M_INHIBIT_ENABLE.AllowedForInhibiting };
            Variables.MMI_M_LEVEL_NTC_ID[] paramEvc20MmiMLevelNtcId =
                { MMI_M_LEVEL_NTC_ID.L0,
                MMI_M_LEVEL_NTC_ID.L2,
                MMI_M_LEVEL_NTC_ID.AWS_TPWS };

            EVC20_MMISelectLevel.MMI_Q_LEVEL_NTC_ID = paramEvc20MmiQLevelNtcId;
            EVC20_MMISelectLevel.MMI_M_CURRENT_LEVEL = paramEvc20MmiMCurrentLevel;
            EVC20_MMISelectLevel.MMI_M_LEVEL_FLAG = paramEvc20MmiMLevelFlag;
            EVC20_MMISelectLevel.MMI_M_INHIBITED_LEVEL = paramEvc20MmiMInhibitedLevel;
            EVC20_MMISelectLevel.MMI_M_INHIBIT_ENABLE = paramEvc20MmiMInhibitEnable;
            EVC20_MMISelectLevel.MMI_M_LEVEL_NTC_ID = paramEvc20MmiMLevelNtcId;

            EVC20_MMISelectLevel.Send();

        }
    }
}
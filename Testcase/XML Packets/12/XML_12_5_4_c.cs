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
    /// Values of 12.5.4.c.xml file
    /// </summary>
    static class XML_12_5_4_c
    {
        private static SignalPool _pool;

        public static void Send(SignalPool pool)
        {
            _pool = pool;

            EVC1_MMIDynamic.MMI_M_SLIDE = 0;
            EVC1_MMIDynamic.MMI_M_SLIP = 0;
            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Normal_Status_Ceiling_Speed_Monitoring;   // 0
            EVC1_MMIDynamic.MMI_A_TRAIN = 0;
            EVC1_MMIDynamic.MMI_V_TRAIN = 0;
            EVC1_MMIDynamic.MMI_V_TARGET = 0;
            EVC1_MMIDynamic.MMI_V_PERMITTED = 11112;
            EVC1_MMIDynamic.MMI_V_RELEASE = 0;
            EVC1_MMIDynamic.MMI_O_BRAKETARGET = 0x3ba1a259;
            EVC1_MMIDynamic.MMI_O_IML = 0x3b9b4523;
            EVC1_MMIDynamic.MMI_V_INTERVENTION = 4658;

            _pool.SITR.ETCS1.Dynamic.EVC01Validity1.Value = 0x13;
            _pool.SITR.ETCS1.Dynamic.EVC01Validity2.Value = 0xff;        
            //_pool.SITR.ETCS1.EtcsMiscOutSignals.EVC7Validity1.Value = 4415; // All validity bits set
            //_pool.SITR.ETCS1.EtcsMiscOutSignals.EVC7Validity2.Value = 63;   // All validity bits set
        }
    }
}
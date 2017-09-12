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
    /// Values of 22.7.2.b.xml file
    /// </summary>
    static class XML_22_7_2_b
    {
        private static SignalPool _pool;

        public static void Send(SignalPool pool)
        {
            _pool = pool;

            // values taken from xml not spec. where different
            //EVC13.MMI_X_DRIVER_ID[0] = 825373492;
            //EVC13.MMI_X_DRIVER_ID[1] = 909588537;
            //EVC13.MMI_X_DRIVER_ID[2] = 825373492;
            //EVC13.MMI_X_DRIVER_ID[3] = 909588537;

            //EVC13.MMI_M_DATA_ENABLE = 0xff00;               // 65280
            //EVC13.MMI_N_CAPTION_TRAINSET = "ABCDEFGHIJKL";  
            //EVC13.MMI_X_CAPTION_NETWORK = "ABCDEFGHIJKLMNOP";

            //EVC13.MMI_NID_RADIO[0] = 0x99999999;          // 2576980377
            //EVC13.MMI_NID_RADIO[1] = 0x99999999;          // 2576980377

            //EVC13.Send();

        }
    }
}
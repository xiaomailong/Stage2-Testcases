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
    /// Values of 22.7.1.a.xml file
    /// </summary>
    static class XML_22_7_1_a
    {
        private static SignalPool _pool;

        public static void Send(SignalPool pool)
        {
            _pool = pool;

            //values taken from spec not xml file where different
            //EVC13.MMI_X_DRIVER_ID[0] = 0;
            //EVC13.MMI_X_DRIVER_ID[1] = 0;
            //EVC13.MMI_X_DRIVER_ID[2] = 0;
            //EVC13.MMI_X_DRIVER_ID[3] = 0;

            //EVC13.MMI_NID_OPERATION = 0xffffffff;         // 4294967295
            //EVC13.MMI_M_DATA_ENABLE = 0x80;               // 128

            //EVC13.MMI_L_TRAIN = 0x1000;                   // 4096
            //EVC13.MMI_V_MAXTRAIN = 601;
            //EVC13.MMI_M_BRAKE_PERC = 9;
            //EVC13.MMI_NID_KEY_AXLE_LOAD = 20;
            //EVC13.MMI_NID_RADIO[0] = 0xffffffff;          // 4294967295
            //EVC13.MMI_NID_RADIO[1] = 0xffffffff;          // 4294967295
            //EVC13.MMI_NID_RBC = 0;                        // 8388608 in xml
            //EVC13.MMI_M_AIRTIGHT = 3;     
            //EVC13.MMI_NID_KEY_LOAD_GAUGE = 33;

            //EVC13.Send();

        }
    }
}
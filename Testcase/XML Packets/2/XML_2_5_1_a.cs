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

#endregion

namespace Testcase.XML
{
    /// <summary>
    /// Values of 2.5.1.a.xml file
    /// </summary>
    static class XML_2_5_1_a
    {
        private static SignalPool _pool;

        public static void Send(SignalPool pool)
        {
            _pool = pool;

            // XML indicates MMI_M_START_REQ property with value of 10
            //EVC0_MMIStartATP.MMI_M_START_REQ = 10;
            //EVC0_MMIStartATP.Evc0Type = EVC0_MMIStartATP.EVC0Type.GoToIdle;
            Testcase.DMITestCases.DmiActions.Start_ATP();
        }
    }
}
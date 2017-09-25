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
    /// Values of 22.9.9.a.xml file
    /// </summary>
    static class XML_22_9_9_a
    {
        private static SignalPool _pool;

        public static void Send(SignalPool pool)
        {
            _pool = pool;

            //some values taken from xml file not spec where different
            EVC11_MMICurrentSRRules.DataElements = new List<Variables.DataElement> { new Variables.DataElement { Identifier = 16, EchoText = "", QDataCheck = 1 } };
            EVC11_MMICurrentSRRules.MMI_M_BUTTONS = Variables.MMI_M_BUTTONS.No_Button;

            EVC11_MMICurrentSRRules.Send();

        }
    }
}
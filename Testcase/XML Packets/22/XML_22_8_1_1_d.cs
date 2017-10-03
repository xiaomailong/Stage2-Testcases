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
    /// Values of 22.8.1.1.d.xml file
    /// </summary>
    static class XML_22_8_1_1_d
    {
        private static SignalPool _pool;

        public static void Send(SignalPool pool)
        {
            _pool = pool;

            EVC22_MMICurrentRBC.MMI_NID_WINDOW = 10;
            EVC22_MMICurrentRBC.NID_RBC = 5648;
            // spec says 5678ef....
            EVC22_MMICurrentRBC.MMI_NID_RADIO = 0x5678ffffffffffff;
            EVC22_MMICurrentRBC.MMI_Q_CLOSE_ENABLE = Variables.MMI_Q_CLOSE_ENABLE.Enabled;
            EVC22_MMICurrentRBC.MMI_M_BUTTONS = EVC22_MMICurrentRBC.EVC22BUTTONS.NoButton;
            EVC22_MMICurrentRBC.DataElements = new List<DataElement> { new DataElement { Identifier = 0, QDataCheck = 4, EchoText = "\0x1\0x30" },
                                                                       new DataElement { Identifier = 1, QDataCheck = 5, EchoText = "\0x1\0x30"} };
            
            EVC22_MMICurrentRBC.Send();            

        }
    }
}
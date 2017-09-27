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
    /// Values of 22.17.b.xml file
    /// </summary>
    static class XML_22_17_b
    {
        private static SignalPool _pool;

        public static void Send(SignalPool pool)
        {
            _pool = pool;
            
            EVC14_MMICurrentDriverID.MMI_X_DRIVER_ID = "";
            EVC14_MMICurrentDriverID.MMI_Q_ADD_ENABLE = EVC14_MMICurrentDriverID.MMI_Q_ADD_ENABLE_BUTTONS.Settings |
                                                        EVC14_MMICurrentDriverID.MMI_Q_ADD_ENABLE_BUTTONS.TRN;
            EVC14_MMICurrentDriverID.MMI_Q_CLOSE_ENABLE = Variables.MMI_Q_CLOSE_ENABLE.Enabled;
            
            EVC14_MMICurrentDriverID.Send();

        }
    }
}
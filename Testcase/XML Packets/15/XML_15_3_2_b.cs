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
using Testcase.DMITestCases;

#endregion

namespace Testcase.XML
{
    /// <summary>
    /// Values of 15.3.2_b.xml file
    /// </summary>
    static class XML_15_3_2_b
    {
        private static SignalPool _pool;

        public static void Send(SignalPool pool)
        {
            _pool = pool;

            // Step 1
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.AuxiliaryInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 4;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 2;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 0;

            EVC8_MMIDriverMessage.Send();

            pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                     "1. The fixed text message ‘Doppler error’ is removed." + Environment.NewLine +
                                     "2. The text message ‘Wheel data settings were successfully changed’ at the lowest position is moved up to close the gap, displayed in sub-area E9." + Environment.NewLine +
                                     "3. The navigation button <Down> at sub-area E11 is disabled, displayed as symbol NA16.");
        }
    }
}
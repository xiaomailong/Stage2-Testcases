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
    /// Values of 15.3.2_c.xml file
    /// </summary>
    static class XML_15_3_2_c
    {
        private static SignalPool _pool;

        public static void Send(SignalPool pool)
        {
            _pool = pool;

            // A series of EVC-8 messages are sent (see 15_3_2_c.xml) to successively remove a line of text

            // Step 14/1
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 4;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 0;

            EVC8_MMIDriverMessage.Send();

            pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                     "1. The text message ‘Tachometer error’ is removed.");

            // Step 14/2
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 4;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 3;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 0;

            EVC8_MMIDriverMessage.Send();

            pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                     "1. The text message ‘*+’ is removed.");

            // Step 14/3
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 4;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 5;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 0;

            EVC8_MMIDriverMessage.Send();

            pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                     "1. The text message ‘Wheel data settings were successfully changed’ is removed.");

            // Step 14/4
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 4;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 6;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 0;

            EVC8_MMIDriverMessage.Send();

            pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                     "1. The text message ‘Level crossing not protected’ is removed.");
        }
    }
}
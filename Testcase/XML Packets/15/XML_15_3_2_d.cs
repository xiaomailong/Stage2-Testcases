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
    /// Values of 15.3.2_d.xml file
    /// </summary>
    static class XML_15_3_2_d
    {
        private static SignalPool _pool;

        public static void Send(SignalPool pool)
        {
            _pool = pool;

            // XML file has these 4 steps as step 14

            // Step 15
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.AuxiliaryInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 261;

            EVC8_MMIDriverMessage.Send();

            pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                     "1. The new incoming message “Fixed Text Message 261” is displayed in sub-area E6.");

            // Step 16
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 3;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 270;

            EVC8_MMIDriverMessage.Send();

            pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                     "1. The new incoming message “Fixed Text Message 270” is display in sub-area E5.");

            // Step 17
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 5;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 271;

            EVC8_MMIDriverMessage.Send();

            pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                     "1. The new incoming message “Fixed Text Message 271” is display in sub-area E5.");

            // Step 18
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.AuxiliaryInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 6;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 272;

            EVC8_MMIDriverMessage.Send();

            pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                     "1. The new incoming message “Fixed Text Message 272” is display in sub-area E9.");

        }
    }
}
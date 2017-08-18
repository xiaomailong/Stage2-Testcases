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
    /// Values of 15.3.2_e.xml file
    /// </summary>
    static class XML_15_3_2_e
    {
        private static SignalPool _pool;

        public static void Send(SignalPool pool)
        {
            _pool = pool;

            // XML file has these 2 steps as step 14
            // The spec has Q_TEXT_CLASS = 4 while Q_TEXT_CRITERIA = 1. These should be reversed - XML file is correct

            // Step 19
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.AuxiliaryInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 4;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 261;

            EVC8_MMIDriverMessage.Send();

            pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                     "1. The new incoming message “Fixed Text Message 261” is removed from sub-area E9.");

            // Step 20
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 4;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 5;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 271;

            EVC8_MMIDriverMessage.Send();

            pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                     "1. The new incoming message “Fixed Text Message 271” is removed from sub-area E5.");
            
        }
    }
}
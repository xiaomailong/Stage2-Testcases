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
    /// Values of 10.2.6.a.xml file
    /// </summary>
    static class XML_10_2_6_a
    {
        private static SignalPool _pool;

        public static void Send(SignalPool pool)
        {
            _pool = pool;

            // Step 2/1
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 716;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;

            EVC8_MMIDriverMessage.Send();

            _pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                      "1. DMI is in the entry state of ‘ST05’." + Environment.NewLine + 
                                      "2. The hourglass symbol ST05 is displayed." + Environment.NewLine +
                                      "3. All buttons and the ‘Close’ button are disabled." + Environment.NewLine +
                                      "4. ‘Close’ button NA12 is displayed disabled in area G." + Environment.NewLine + 
                                      "5. The Input Field is not selected.");

            System.Threading.Thread.Sleep(10000);

            // Step 2/2
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 4;

            EVC8_MMIDriverMessage.Send();

            _pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                      "1. DMI is in the exit state of ‘ST05’." + Environment.NewLine +
                                      "2. The hourglass symbol ST05 is removed." + Environment.NewLine +
                                      "3. All buttons are enabled." + Environment.NewLine +
                                      "4. ‘Close’ button NA11 is displayed enabled in area G." + Environment.NewLine +
                                      "5. The Input Field is selected.");
        }
    }
}
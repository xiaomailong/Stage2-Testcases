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
    /// Values of 15.4.c.xml file
    /// </summary>
    static class XML_15_4_c
    {
        private static SignalPool _pool;

        public static void Send(SignalPool pool)
        {
            _pool = pool;
                        
            // Step 8/1
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 5;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 256;
            EVC8_MMIDriverMessage.PlainTextMessage = "ABCDEFGHIJ BCDEFGHIJ";
            EVC8_MMIDriverMessage.Send();            

            System.Threading.Thread.Sleep(9000);

            // Step 8/2            
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.PlainTextMessage = " XXXXXXXXX";
            EVC8_MMIDriverMessage.Send();
       }
    }
}
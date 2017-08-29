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
    /// Values of 15.4.h.xml file
    /// </summary>
    static class XML_15_4_h
    {
        private static SignalPool _pool;

        public static void Send(SignalPool pool)
        {
            _pool = pool;
                        
            // Step 13/1
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 533;
            EVC8_MMIDriverMessage.PlainTextMessage = "1";
            EVC8_MMIDriverMessage.Send();

            _pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                      @"1. DMI displays BTM Test Timeout’ in sub-area E5.");           

            // Step 13/2         
            // spec says I_TEXT = 1 (fixed) but xml 'incremented' in next message
            // assume xml is correct   
            EVC8_MMIDriverMessage.MMI_I_TEXT = 2;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 260;
            EVC8_MMIDriverMessage.Send();

            // Spec says ST02 but the symbol displayed is ST01!
            _pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                       "1. DMI displays ST01 symbol in area C9.");
            
        }
    }
}
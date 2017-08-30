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
    /// Values of 15.4.d.xml file
    /// </summary>
    static class XML_15_4_d
    {
        private static SignalPool _pool;

        public static void Send(SignalPool pool)
        {
            _pool = pool;
                        
            // Step 9/1
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 5;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 543;
            EVC8_MMIDriverMessage.PlainTextMessage = "\0x14";
            EVC8_MMIDriverMessage.Send();

            _pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                      @"1. DMI displays ‘TPWS Failed’ in areas E5-E9.");           

            // Step 9/2         
            // spec says I_TEXT = 1 (fixed) but xml 'increments' in each message
            // assume xml is correct   
            EVC8_MMIDriverMessage.MMI_I_TEXT = 2;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 565;
            EVC8_MMIDriverMessage.Send();

            _pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                       @"1. DMI displays ‘Confirm change of inhibit STM TPWS’ in areas E5-E9.");

            // Step 9/3
            EVC8_MMIDriverMessage.MMI_I_TEXT = 3;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 570;
            EVC8_MMIDriverMessage.Send();

            _pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                       @"1. DMI displays ‘Shunting rejected due to TPWS Trip’ in areas E5-E9.");

            // Step 9/4
            EVC8_MMIDriverMessage.MMI_I_TEXT = 4;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 573;
            EVC8_MMIDriverMessage.Send();

            _pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                       @"1. DMI displays ‘TPWS needs data’ in areas E5-E9.");

            // Step 9/5
            EVC8_MMIDriverMessage.MMI_I_TEXT = 5;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 700;
            EVC8_MMIDriverMessage.Send();

            _pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                       @"1. DMI displays ‘TPWS brake demand’ in areas E5-E9.");

            // Step 9/6
            EVC8_MMIDriverMessage.MMI_I_TEXT = 6;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 704;
            EVC8_MMIDriverMessage.Send();

            _pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                       @"1. DMI displays ‘TPWS is not available’ in areas E5-E9.");

            _pool.WaitForVerification(@"Using the <Up> and <Down> buttons, check that the order of all messages top to bottom is:" + Environment.NewLine + Environment.NewLine +
                                      @"1. DMI displays ‘TPWS is not available’ in areas E5-E9." + Environment.NewLine +
                                      @"2. DMI displays ‘TPWS brake demand’ in areas E5-E9." + Environment.NewLine +
                                      @"3. DMI displays ‘TPWS needs data’ in areas E5-E9." + Environment.NewLine +
                                      @"4. DMI displays ‘Shunting rejected due to TPWS Trip’ in areas E5-E9." + Environment.NewLine +
                                      @"5. DMI displays ‘Confirm change of inhibit STM TPWS’ in areas E5-E9." + Environment.NewLine +
                                      @"6. DMI displays ‘TPWS Failed’ in areas E5 - E9.");
        }
    }
}
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
    /// Values of 15.4.g.xml file
    /// </summary>
    static class XML_15_4_g
    {
        private static SignalPool _pool;

        public static void Send(SignalPool pool)
        {
            _pool = pool;
                        
            // Step 12/1
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 522;
            EVC8_MMIDriverMessage.PlainTextMessage = "\0x00";
            EVC8_MMIDriverMessage.Send();

            _pool.WaitForVerification("Check the following where [@] is a solid white rectangular symbol:" + Environment.NewLine + Environment.NewLine +
                                      @"1. DMI displays ‘Restriction [@] km/h in Release Speed Area’ in areas E5-E9.");           

            // Step 12/2         
            // spec says I_TEXT = 1 (fixed) but xml 'increments' in each message
            // assume xml is correct   
            EVC8_MMIDriverMessage.MMI_I_TEXT = 2;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 525;
            EVC8_MMIDriverMessage.Send();

            _pool.WaitForVerification("Check the following where [@] is a solid white rectangular symbol:" + Environment.NewLine + Environment.NewLine +
                                       @"1. DMI displays ‘Brake Test timeout in [@] Hours’ in areas E5-E9.");

            // Step 12/3
            EVC8_MMIDriverMessage.MMI_I_TEXT = 3;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 534;
            EVC8_MMIDriverMessage.Send();

            _pool.WaitForVerification("Check the following where [@] is a solid white rectangular symbol:" + Environment.NewLine + Environment.NewLine +
                                       @"1. DMI displays ‘BTM Test Timeout in [@] hours’ in areas E5-E9.");

            // Step 12/4
            EVC8_MMIDriverMessage.MMI_I_TEXT = 4;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 535;
            EVC8_MMIDriverMessage.Send();

            _pool.WaitForVerification("Check the following where [@] is a solid white rectangular symbol:" + Environment.NewLine + Environment.NewLine +
                                       @"1. DMI displays ‘ATP Restart required in [@] Hours’ in areas E5-E9.");

            // Step 12/5
            EVC8_MMIDriverMessage.MMI_I_TEXT = 5;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 564;
            EVC8_MMIDriverMessage.Send();

            _pool.WaitForVerification("Check the following where [@] is a solid white rectangular symbol:" + Environment.NewLine + Environment.NewLine +
                                       @"1. DMI displays ‘Confirm change of inhibit Level [@]’ in areas E5-E9.");

            // Step 12/6
            EVC8_MMIDriverMessage.MMI_I_TEXT = 6;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 574;
            EVC8_MMIDriverMessage.Send();

            _pool.WaitForVerification("Check the following where [@] is a solid white rectangular symbol:" + Environment.NewLine + Environment.NewLine +
                                       @"1. DMI displays ‘Cabin Reactivation required in [@] hours’ in areas E5-E9."); 
            
            // Step 12/7
            EVC8_MMIDriverMessage.MMI_I_TEXT = 7;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 705;
            EVC8_MMIDriverMessage.Send();

            _pool.WaitForVerification("Check the following where [@] is a solid white rectangular symbol:" + Environment.NewLine + Environment.NewLine +
                                       @"1. DMI displays ‘New power-up required in [@] hours’ in areas E5-E9.");

            _pool.WaitForVerification(@"Using the <Up> and <Down> buttons, check that the order of all messages top to bottom is:" + Environment.NewLine + Environment.NewLine +
                                      @"1. DMI displays ‘New power-up required in [@] hours’ in areas E5-E9." + Environment.NewLine +
                                      @"2. DMI displays Cabin Reactivation required in [@] hours’ in areas E5-E9." + Environment.NewLine +
                                      @"3. DMI displays ‘Confirm change of inhibit Level [@]’ in areas E5-E9." + Environment.NewLine +
                                      @"4. DMI displays ‘ATP Restart required in [@] Hours’ in areas E5-E9." + Environment.NewLine +
                                      @"5. DMI displays ‘BTM Test Timeout in [@] hours’ in areas E5-E9." + Environment.NewLine +
                                      @"6. DMI displays ‘Brake Test timeout in [@] Hours’ in areas E5 - E9." + Environment.NewLine +
                                      @"7. DMI displays ‘Restriction [@] km/h in Release Speed Area’ in areas E5 - E9.");
        }
    }
}
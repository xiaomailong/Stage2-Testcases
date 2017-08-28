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
    /// Values of 5.12.3.a.xml file
    /// </summary>
    static class XML_5_12_3_a
    {
        private static SignalPool _pool;

        public static void Send(SignalPool pool)
        {
            _pool = pool;

            // Step 1/1
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 267;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;

            EVC8_MMIDriverMessage.Send();

            _pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                      @"1. DMI displays the message ‘Balise read error’ in sub-areas E5-E9." + Environment.NewLine +
                                      @"2. The ‘Up’ button NA15 symbol is displayed disabled in sub-area E10" + Environment.NewLine +
                                      @"3. The ‘Down’ button  NA14 symbol is displayed enabled in sub-area E11.");

            // Step 1/2
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 560;
            EVC8_MMIDriverMessage.Send();

            _pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                      @"1. DMI displays the message ‘Trackside malfunction’ in sub-areas E5." + Environment.NewLine +
                                      "2. The older text message is moved down." + Environment.NewLine +
                                      @"3. The ‘Up’ button NA15 symbol is displayed disabled in sub-area E10" + Environment.NewLine +
                                      @"4. The ‘Down’ button  NA14 symbol is displayed enabled in sub-area E11.");

            // Step 1/3
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 268;
            EVC8_MMIDriverMessage.Send();

            _pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                      @"1. DMI displays the message ‘Communication error’ in sub-areas E5." + Environment.NewLine +
                                      "2. The older text messages are moved down." + Environment.NewLine +
                                      @"3. The ‘Up’ button NA15 symbol is displayed disabled in sub-area E10" + Environment.NewLine +
                                      @"4. The ‘Down’ button  NA14 symbol is displayed enabled in sub-area E11.");

            // Step 1/4
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 274;
            EVC8_MMIDriverMessage.Send();

            _pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                      @"1. DMI displays the message ‘Entering FS’ in sub-areas E5." + Environment.NewLine +
                                      "2. The older text messages are moved down." + Environment.NewLine +
                                      @"3. The ‘Up’ button NA15 symbol is displayed disabled in sub-area E10" + Environment.NewLine +
                                      @"4. The ‘Down’ button  NA14 symbol is displayed enabled in sub-area E11.");

            // Step 1/5
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 275;
            EVC8_MMIDriverMessage.Send();

            _pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                      @"1. DMI displays the message ‘Entering OS’ in sub-areas E5." + Environment.NewLine +
                                      "2. The older text messages are moved down." + Environment.NewLine +
                                      @"3. The ‘Up’ button NA15 symbol is displayed disabled in sub-area E10" + Environment.NewLine +
                                      @"4. The ‘Down’ button  NA14 symbol is displayed enabled in sub-area E11.");

            // Step 1/6
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 290;
            EVC8_MMIDriverMessage.Send();

            _pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                      @"1. DMI displays the message ‘SH refused’ in sub-areas E5." + Environment.NewLine +
                                      "2. The older text messages are moved down." + Environment.NewLine +
                                      @"3. The ‘Up’ button NA15 symbol is displayed disabled in sub-area E10" + Environment.NewLine +
                                      @"4. The ‘Down’ button  NA14 symbol is displayed enabled in sub-area E11.");

            // Step 1/7
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 292;
            EVC8_MMIDriverMessage.Send();

            _pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                      @"1. DMI displays the message ‘SH request failed’ in sub-areas E5." + Environment.NewLine +
                                      "2. The older text messages are moved down." + Environment.NewLine +
                                      @"3. The ‘Up’ button NA15 symbol is displayed disabled in sub-area E10" + Environment.NewLine +
                                      @"4. The ‘Down’ button  NA14 symbol is displayed enabled in sub-area E11.");

            // Step 1/8
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 296;
            EVC8_MMIDriverMessage.Send();

            _pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                      @"1. DMI displays the message ‘Trackside not compatible’ in sub-areas E5." + Environment.NewLine +
                                      "2. The older text messages are moved down." + Environment.NewLine +
                                      @"3. The ‘Up’ button NA15 symbol is displayed disabled in sub-area E10" + Environment.NewLine +
                                      @"4. The ‘Down’ button  NA14 symbol is displayed enabled in sub-area E11.");

            // Step 1/9
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 310;
            EVC8_MMIDriverMessage.Send();

            _pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                      @"1. DMI displays the message ‘Train data changed’ in sub-areas E5." + Environment.NewLine +
                                      "2. The older text messages are moved down." + Environment.NewLine +
                                      @"3. The ‘Up’ button NA15 symbol is displayed disabled in sub-area E10" + Environment.NewLine +
                                      @"4. The ‘Down’ button  NA14 symbol is displayed enabled in sub-area E11.");

            // Step 1/10
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 299;
            EVC8_MMIDriverMessage.Send();

            _pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                      @"1. DMI displays the message ‘Train is rejected’ in sub-areas E5." + Environment.NewLine +
                                      "2. The older text messages are moved down." + Environment.NewLine +
                                      @"3. The ‘Up’ button NA15 symbol is displayed disabled in sub-area E10" + Environment.NewLine +
                                      @"4. The ‘Down’ button  NA14 symbol is displayed enabled in sub-area E11.");
        }
    }
}
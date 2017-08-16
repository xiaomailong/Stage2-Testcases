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
    /// Values of 15.3.2_a.xml file
    /// </summary>
    static class XML_15_3_2_a
    {
        private static SignalPool _pool;

        public static void Send(SignalPool pool)
        {
            _pool = pool;

            // Step 1
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 256;
            EVC8_MMIDriverMessage.PlainTextMessage = "*+";

            EVC8_MMIDriverMessage.Send();

            pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                     "1. The plain text message ‘*+’ is displayed sub-area E5 without a yellow flashing frame." + Environment.NewLine +
                                     "2. The text message is presented with characters in bold style." + Environment.NewLine +
                                     "3. Sound Sinfo is played.");

            // Step 2
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.AuxiliaryInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 2;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 266;
            EVC8_MMIDriverMessage.PlainTextMessage = "**";

            EVC8_MMIDriverMessage.Send();

            pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                     "1. The plain text message ‘**’ is displayed sub-area E5 without a yellow flashing frame." + Environment.NewLine +
                                     "2. The text message is presented with characters in regular style." + Environment.NewLine +
                                     "3. No sound is played." + Environment.NewLine +
                                     "4. There is no gap between the new text message and older message from the previous step.");

            // Step 3
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.AuxiliaryInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 3;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 0;
            EVC8_MMIDriverMessage.Send();

            pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                     "1. Text message ‘Level crossing not protected’ is displayed in sub-area E6 without a yellow flashing frame." + Environment.NewLine +
                                     "2. The bold text message is still displayed above the regular messages." + Environment.NewLine +
                                     "3. No sound is played." + Environment.NewLine +
                                     "4. The old text message ‘**’ is moved to sub - area E7.");

            // Step 4
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 4;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 273;
            EVC8_MMIDriverMessage.Send();

            pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                     "1. Text message ‘Unauthorized passing of EOA/LOA’ is displayed in sub-area E5-E6 without yellow flashing frame." + Environment.NewLine +
                                     "2. Sound Sinfo is played." + Environment.NewLine +
                                     "3. The old text messages are moved to sub - area E7 - E9 respectively." + Environment.NewLine +
                                     "4. The navigation buttons < Up > and < Down > at sub - area E10 - E11 are disabled." + Environment.NewLine +
                                     "5. DMI displays symbol NA15 at sub - area E10." + Environment.NewLine +
                                     "6. DMI displays symbol NA16 at sub - area E11.");

            // Step 5
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 5;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 625;
            EVC8_MMIDriverMessage.Send();

            pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                     "1. Text message ‘Tachometer error’ is displayed in sub - area E5 without yellow flashing frame." + Environment.NewLine +
                                     "2. Sound Sinfo is played." + Environment.NewLine +
                                     "3. The old text messages are moved to sub - area E6 - E9 respectively." + Environment.NewLine +
                                     "4. The navigation buttons <Down>is disabled." + Environment.NewLine +
                                     "5. DMI displays symbol NA14 at sub - area E11.");
            
            // Step 6
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.AuxiliaryInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 6;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 712;
            EVC8_MMIDriverMessage.Send();

            pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                     "1. Truncated text message ‘Wheel data settings were successfully changed’ is displayed in sub-area E9 without yellow flashing frame." + Environment.NewLine +
                                     "2. No sound is played.");

            // Step 7
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.AuxiliaryInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 2;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 583;

            pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Text message ‘Doppler error’ is displayed in sub - area E9 without yellow flashing frame." + Environment.NewLine +
                                "2. No sound is played.");            
            
        }
    }
}
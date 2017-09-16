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
    /// Values of 6.7.xml file
    /// </summary>
    static class XML_6_7
    {
        private static SignalPool _pool;

        public static void Send(SignalPool pool)
        {
            _pool = pool;

            // STM-38 parts??

            /*              
            MMI_STM_NID_PACKET = 38;
            MMI_STM_L_PACKET = 56;
            MMI_STM_NID_XMESSAGE = 1;
            MMI_STM_M_XATTRIBUTE = 513;
            MMI_STM_Q_ACK = 1;
            MMI_STM_L_TEXT = 1;
            //?? MMI_STM_X_TEXT = "\0x2";
            // ...Send();
            */

            _pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                      "1. DMI displays the message ‘2 - Brakes are not operated’ in sub-area E5 with a yellow flashing frame surrounding sub-areas (E5+E6+E7+E8+E9)." + Environment.NewLine +
                                      "2. ‘Sinfo’ sound is not played.");

            _pool.Wait_Realtime(5000);
                
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.AuxiliaryInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 268;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 2;
            EVC8_MMIDriverMessage.PlainTextMessage = "";
            EVC8_MMIDriverMessage.Send();

            _pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                      "1. After 5s, DMI displays the message ‘Communication error’ in sub-area E5 with a yellow flashing frame surrounding sub-areas (E5+E6+E7+E8+E9)." + Environment.NewLine +
                                      "2. ‘Sinfo’ sound is played once.");

            _pool.Wait_Realtime(5000);

            /*              
            MMI_STM_NID_PACKET = 38;
            MMI_STM_L_PACKET = 56;
            MMI_STM_NID_XMESSAGE = 3;
            MMI_STM_M_XATTRIBUTE = 513;
            MMI_STM_Q_ACK = 1;
            MMI_STM_L_TEXT = 1;
            //?? MMI_STM_X_TEXT = "\0x4";
            // ...Send();
            */

            _pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                      "1. After 5s, DMI displays the message ‘4 - Brake feedback fault’ in sub-area E5 with a yellow flashing frame surrounding sub-areas (E5+E6+E7+E8+E9)." + Environment.NewLine +
                                      "2. ‘Sinfo’ sound is not played.");

            _pool.Wait_Realtime(5000);

            EVC8_MMIDriverMessage.MMI_Q_TEXT = 305;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 4;

            _pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                      "1. After 5s, DMI displays the message ‘Train divided’ in sub-area E5 with a yellow flashing frame surrounding sub-areas (E5+E6+E7+E8+E9)." + Environment.NewLine +
                                      "2. ‘Sinfo’ sound is played once.");
        }
    }
}
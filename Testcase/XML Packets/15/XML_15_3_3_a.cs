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
    /// Values of 15.3.3.a.xml file
    /// </summary>
    static class XML_15_3_3_a
    {
        private static SignalPool _pool;

        public static void Send(SignalPool pool)
        {
            _pool = pool;
                        
            // Step 1/1
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 0;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 0;
            EVC8_MMIDriverMessage.Send();

            pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                     "1. The text message ‘Level crossing not protected’ is displayed in area E5." + Environment.NewLine +
                                     "2. Sound Sinfo is played." + Environment.NewLine +
                                     "3. All older messages are moved down one line");


            // Step 1/2
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 1;
            EVC8_MMIDriverMessage.Send();

            pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                     "1. The text message ‘Acknowledgement’ is displayed in area E5." + Environment.NewLine +
                                     "2. Sound Sinfo is played.");

            // Step 1/3
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 2;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 267;
            EVC8_MMIDriverMessage.Send();

            pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                     "1. The text message ‘Balise read error’ is displayed in area E5." + Environment.NewLine +
                                     "2. Sound Sinfo is played." + Environment.NewLine +
                                     "3. All older messages are moved down one line");

            // Step 1/4
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 3;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 268;
            EVC8_MMIDriverMessage.Send();

            pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                     "1. The text message ‘Communication error’ is displayed in area E5." + Environment.NewLine +
                                     "2. Sound Sinfo is played." + Environment.NewLine +
                                     "3. All older messages are moved down one line");

            // Step 1/5
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 4;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 269;
            EVC8_MMIDriverMessage.Send();

            pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                     "1. The text message ‘Runaway movement’ is displayed in area E5." + Environment.NewLine +
                                     "2. Sound Sinfo is played." + Environment.NewLine +
                                     "3. All older messages are moved down one line");

            // Step 1/6
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 5;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 274;
            EVC8_MMIDriverMessage.Send();

            pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                     "1. The text message ‘Entering FS’ is displayed in area E5." + Environment.NewLine +
                                     "2. Sound Sinfo is played.");

            // Step 1/7
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 6;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 275;
            EVC8_MMIDriverMessage.Send();

            pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                     "1. The text message ‘Entering OS’ is displayed in area E5." + Environment.NewLine +
                                     "2. Sound Sinfo is played." + Environment.NewLine +
                                     "3. All older messages are moved down one line");

            // Step 1/8
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 7;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 280;
            EVC8_MMIDriverMessage.Send();

            pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                     "1. The text message ‘Emergency stop’ is displayed in area E5." + Environment.NewLine +
                                     "2. Sound Sinfo is played." + Environment.NewLine +
                                     "3. All older messages are moved down one line");

            // Step 1/9
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 8;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 290;
            EVC8_MMIDriverMessage.Send();

            pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                     "1. The text message ‘SH refused’ is displayed in area E5." + Environment.NewLine +
                                     "2. Sound Sinfo is played." + Environment.NewLine +
                                     "3. All older messages are moved down one line");

            // Step 1/10
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 9;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 292;
            EVC8_MMIDriverMessage.Send();

            pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                     "1. The text message ‘SH request failed’ is displayed in area E5." + Environment.NewLine +
                                     "2. Sound Sinfo is played." + Environment.NewLine +
                                     "3. All older messages are moved down one line");

            // Step 1/11
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 10;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 296;
            EVC8_MMIDriverMessage.Send();

            pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                     "1. The text message ‘Trackside not compatible’ is displayed in area E5." + Environment.NewLine +
                                     "2. Sound Sinfo is played." + Environment.NewLine +
                                     "3. All older messages are moved down one line");

            // Step 1/12
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 11;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 299;
            EVC8_MMIDriverMessage.Send();

            pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                     "1. The text message ‘Train is rejected’ is displayed in area E5." + Environment.NewLine +
                                     "2. Sound Sinfo is played." + Environment.NewLine +
                                     "3. All older messages are moved down one line");

            // Step 1/13
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 12;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 305;
            EVC8_MMIDriverMessage.Send();

            pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                     "1. The text message ‘Train divided’ is displayed in area E5." + Environment.NewLine +
                                     "2. Sound Sinfo is played.");

            // Step 1/14
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 13;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 310;
            EVC8_MMIDriverMessage.Send();

            pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                     "1. The text message ‘Train data changed’ is displayed in area E5." + Environment.NewLine +
                                     "2. Sound Sinfo is played." + Environment.NewLine +
                                     "3. All older messages are moved down one line");

            // Step 1/15
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 14;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 315;
            EVC8_MMIDriverMessage.Send();

            pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                     "1. The text message ‘SR Distance exceeded’ is displayed in area E5." + Environment.NewLine +
                                     "2. Sound Sinfo is played." + Environment.NewLine +
                                     "3. All older messages are moved down one line");

            // Step 1/16
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 15;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 316;
            EVC8_MMIDriverMessage.Send();

            pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                     "1. The text message ‘SR stop order’ is displayed in area E5." + Environment.NewLine +
                                     "2. Sound Sinfo is played.");

            // Step 1/17
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 16;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 320;
            EVC8_MMIDriverMessage.Send();

            pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                     "1. The text message ‘RV distance exceeded’ is displayed in area E5." + Environment.NewLine +
                                     "2. Sound Sinfo is played." + Environment.NewLine +
                                     "3. All older messages are moved down one line");

            // Step 1/18
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 17;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 321;
            EVC8_MMIDriverMessage.Send();

            pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                     "1. The text message ‘ETCS Isolated’ is displayed in area E5." + Environment.NewLine +
                                     "2. Sound Sinfo is played.");

            // Step 1/19
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 18;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 514;
            EVC8_MMIDriverMessage.Send();

            pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                     "1. The text message ‘Perform Brake Test!’ is displayed in area E5." + Environment.NewLine +
                                     "2. Sound Sinfo is played." + Environment.NewLine +
                                     "3. All older messages are moved down one line");

            // Step 1/20
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 19;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 515;
            EVC8_MMIDriverMessage.Send();

            pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                     "1. The text message ‘Unable to start Brake Test’ is displayed in area E5." + Environment.NewLine +
                                     "2. Sound Sinfo is played." + Environment.NewLine +
                                     "3. All older messages are moved down one line");

            // Step 1/21
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 20;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 516;
            EVC8_MMIDriverMessage.Send();

            pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                     "1. The text message ‘Brake Test in Progress’ is displayed in area E5." + Environment.NewLine +
                                     "2. Sound Sinfo is played.");

            // Step 1/22
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 21;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 520;
            EVC8_MMIDriverMessage.Send();

            pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                     "1. The text message ‘LZB Partial Block Mode’ is displayed in area E5." + Environment.NewLine +
                                     "2. Sound Sinfo is played." + Environment.NewLine +
                                     "3. All older messages are moved down one line");

            // Step 1/23
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 22;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 521;
            EVC8_MMIDriverMessage.Send();

            pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                     "1. The text message ‘Override LZB Partial Block Mode’ is displayed in area E5." + Environment.NewLine +
                                     "2. Sound Sinfo is played." + Environment.NewLine +
                                     "3. All older messages are moved down one line");

            // Step 1/24
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 23;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 524;
            EVC8_MMIDriverMessage.Send();

            pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                     "1. The text message ‘Brake Test successful’ is displayed in area E5." + Environment.NewLine +
                                     "2. Sound Sinfo is played." + Environment.NewLine +
                                     "3. All older messages are moved down one line");

            // Step 1/25
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 24;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 526;
            EVC8_MMIDriverMessage.Send();

            pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                     "1. The text message ‘Brake Test Timeout’ is displayed in area E5." + Environment.NewLine +
                                     "2. Sound Sinfo is played." + Environment.NewLine +
                                     "3. All older messages are moved down one line");

            // Step 1/26
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 25;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 527;
            EVC8_MMIDriverMessage.Send();

            pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                     "1. The text message ‘Brake Test aborted, perform new Test?’ is displayed in area E5." + Environment.NewLine +
                                     "2. Sound Sinfo is played." + Environment.NewLine +
                                     "3. All older messages are moved down one line");

            // Step 1/27
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 26;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 531;
            EVC8_MMIDriverMessage.Send();

            pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                     "1. The text message ‘BTM Test in Progress’ is displayed in area E5." + Environment.NewLine +
                                     "2. Sound Sinfo is played." + Environment.NewLine +
                                     "3. All older messages are moved down one line");

            // Step 1/28
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 27;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 532;
            EVC8_MMIDriverMessage.Send();

            pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                     "1. The text message ‘BTM Test Failure’ is displayed in area E5." + Environment.NewLine +
                                     "2. Sound Sinfo is played." + Environment.NewLine +
                                     "3. All older messages are moved down one line");

            // Step 1/29
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 28;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 533;
            EVC8_MMIDriverMessage.Send();

            pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                     "1. The text message ‘BTM Test Timeout’ is displayed in area E5." + Environment.NewLine +
                                     "2. Sound Sinfo is played." + Environment.NewLine +
                                     "3. All older messages are moved down one line");

            // Step 1/30
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 29;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 536;
            EVC8_MMIDriverMessage.Send();

            pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                     "1. The text message ‘Restart ATP!’ is displayed in area E5." + Environment.NewLine +
                                     "2. Sound Sinfo is played." + Environment.NewLine +
                                     "3. All older messages are moved down one line");

            // Step 1/31
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 30;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 540;
            EVC8_MMIDriverMessage.Send();

            pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                     "1. The text message ‘No Level available Onboard’ is displayed in area E5." + Environment.NewLine +
                                     "2. Sound Sinfo is played." + Environment.NewLine +
                                     "3. All older messages are moved down one line");

            // Step 1/32
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 31;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 552;
            EVC8_MMIDriverMessage.Send();

            pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                     "1. The text message ‘Announced levels(s) not supported Onboard’ is displayed in area E5." + Environment.NewLine +
                                     "2. Sound Sinfo is played." + Environment.NewLine +
                                     "3. All older messages are moved down one line");

            // Step 1/33
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 32;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 554;
            EVC8_MMIDriverMessage.Send();

            pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                     "1. The text message ‘Reactivate the Cabin!’ is displayed in area E5." + Environment.NewLine +
                                     "2. Sound Sinfo is played." + Environment.NewLine +
                                     "3. All older messages are moved down one line");

            // Step 1/34
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 33;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 560;
            EVC8_MMIDriverMessage.Send();

            pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                     "1. The text message ‘Trackside malfunction’ is displayed in area E5." + Environment.NewLine +
                                     "2. Sound Sinfo is played." + Environment.NewLine +
                                     "3. All older messages are moved down one line");

            // Step 1/35
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 34;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 563;
            EVC8_MMIDriverMessage.Send();

            pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                     "1. The text message ‘Trackside Level(s) not supported Onboard’ is displayed in area E5." + Environment.NewLine +
                                     "2. Sound Sinfo is played." + Environment.NewLine +
                                     "3. All older messages are moved down one line");

            // Step 1/36
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 35;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 572;
            EVC8_MMIDriverMessage.Send();

            pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                     "1. The text message ‘No Track Description’ is displayed in area E5." + Environment.NewLine +
                                     "2. Sound Sinfo is played." + Environment.NewLine +
                                     "3. All older messages are moved down one line");

            // Step 1/37
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 36;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 606;
            EVC8_MMIDriverMessage.Send();

            pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                     "1. The text message ‘SH Stop Order’ is displayed in area E5." + Environment.NewLine +
                                     "2. Sound Sinfo is played." + Environment.NewLine +
                                     "3. All older messages are moved down one line");

            // Step 1/38
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 37;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 580;
            EVC8_MMIDriverMessage.Send();

            pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                     "1. The text message ‘Procedure Brake Percentage Entry terminated by ATP’ is displayed in area E5." + Environment.NewLine +
                                     "2. Sound Sinfo is played." + Environment.NewLine +
                                     "3. All older messages are moved down one line");

            // Step 1/39
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 38;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 581;
            EVC8_MMIDriverMessage.Send();

            pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                     "1. The text message ‘Procedure Wheel Diameter Entry terminated by ATP’ is displayed in area E5." + Environment.NewLine +
                                     "2. Sound Sinfo is played." + Environment.NewLine +
                                     "3. All older messages are moved down one line");

            // Step 1/40
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 39;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 582;
            EVC8_MMIDriverMessage.Send();

            pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                     "1. The text message ‘Procedure Doppler Radar Entry terminated by ATP’ is displayed in area E5." + Environment.NewLine +
                                     "2. Sound Sinfo is played." + Environment.NewLine +
                                     "3. All older messages are moved down one line");

            // Step 1/41
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 40;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 621;
            EVC8_MMIDriverMessage.Send();

            pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                     "1. The text message ‘Unable to start Brake Test, vehicle not ready’ is displayed in area E5." + Environment.NewLine +
                                     "2. Sound Sinfo is played." + Environment.NewLine +
                                     "3. All older messages are moved down one line");

            // Step 1/42
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 41;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 622;
            EVC8_MMIDriverMessage.Send();

            pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                     "1. The text message ‘Unblock EB’ is displayed in area E5." + Environment.NewLine +
                                     "2. Sound Sinfo is played." + Environment.NewLine +
                                     "3. All older messages are moved down one line");

            // Step 1/43
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 42;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 701;
            EVC8_MMIDriverMessage.Send();

            pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                     "1. The text message ‘Route unsuitable – axle load category’ is displayed in area E5." + Environment.NewLine +
                                     "2. Sound Sinfo is played." + Environment.NewLine +
                                     "3. All older messages are moved down one line");
            // Step 1/44
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 43;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 702;
            EVC8_MMIDriverMessage.Send();

            pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                     "1. The text message ‘Route unsuitable – loading gauge’ is displayed in area E5." + Environment.NewLine +
                                     "2. Sound Sinfo is played." + Environment.NewLine +
                                     "3. All older messages are moved down one line");
            // Step 1/45
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 44;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 703;
            EVC8_MMIDriverMessage.Send();

            pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                     "1. The text message ‘Route unsuitable – traction system’ is displayed in area E5." + Environment.NewLine +
                                     "2. Sound Sinfo is played." + Environment.NewLine +
                                     "3. All older messages are moved down one line");
            // Step 1/46
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 45;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 706;
            EVC8_MMIDriverMessage.Send();

            pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                     "1. The text message ‘No valid authentication key’ is displayed in area E5." + Environment.NewLine +
                                     "2. Sound Sinfo is played." + Environment.NewLine +
                                     "3. All older messages are moved down one line");
            // Step 1/47
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 46;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 711;
            EVC8_MMIDriverMessage.Send();

            pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                     "1. The text message ‘NL-input signal is withdrawn’ is displayed in area E5." + Environment.NewLine +
                                     "2. Sound Sinfo is played." + Environment.NewLine +
                                     "3. All older messages are moved down one line");
            // Step 1/48
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 47;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 712;
            EVC8_MMIDriverMessage.Send();

            pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                     "1. The text message ‘Wheel data settings were successfully changed’ is displayed in area E5." + Environment.NewLine +
                                     "2. Sound Sinfo is played." + Environment.NewLine +
                                     "3. All older messages are moved down one line");
            // Step 1/49
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 48;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 713;
            EVC8_MMIDriverMessage.Send();

            pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                     "1. The text message ‘Doppler radar settings were successfully changed’ is displayed in area E5." + Environment.NewLine +
                                     "2. Sound Sinfo is played." + Environment.NewLine +
                                     "3. All older messages are moved down one line");
            // Step 1/50
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 49;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 714;
            EVC8_MMIDriverMessage.Send();

            pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                     "1. The text message ‘Brake percentage was successfully changed’ is displayed in area E5." + Environment.NewLine +
                                     "2. Sound Sinfo is played." + Environment.NewLine + 
                                     "3. All older messages are moved down one line");
        }
    }
}
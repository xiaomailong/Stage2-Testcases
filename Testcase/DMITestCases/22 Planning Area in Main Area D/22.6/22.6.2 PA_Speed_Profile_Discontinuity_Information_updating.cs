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


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 22.6.2 PA Speed Profile Discontinuity: Information updating
    /// TC-ID: 17.6.2
    /// 
    /// This test case verify the display information of PA Speed Profile Discontinuity refer to received packets information EVC-4 which contain an aempty array of PA Speed Profile segments (MMI_N_MRSP =0) and the special value of speed profile refer to [MMI-ETCS-gen].
    /// 
    /// Tested Requirements:
    /// MMI_gen 7290; MMI_gen 2600 (partly: 1st bullet, result of calculation, 3rd bullet, PL22 symbol, 4th bullet, PL21 symbol, 5th bullet, Not place any Speed discontinuity symbol); MMI_gen 7291;
    /// 
    /// Scenario:
    /// Drive the train forward pass BG1 at position 50m. BG1: Packet 12, 21 and 27Use the test script file to send packet information EVC-
    /// 4.Then, verify that all PASP segments are updated correctly.
    /// 
    /// Used files:
    /// 17_6_2.tdg, 17_6_2_a.xml, 17_6_2_b.xml, 17_6_2_c.xml, 17_6_2_d.xml, 17_6_2_e.xml
    /// </summary>
    public class TC_ID_17_6_2_PA_Speed_Profile_Discontinuity_Information_updating : TestcaseBase
    {
        static List<TrackDescription> TrackDescriptions;

        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:

            // Call the TestCaseBase PreExecution
            base.PreExecution();

            // Test system is power on.Cabin is activatedSoM is perform in SR mode, Leve 1.
            DmiActions.Complete_SoM_L1_SR(this);
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in FS mode, level 1

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint

            /*
            Test Step 1
            Action: Drive the train forward pass BG1.Then, Stop the train
            Expected Result: DMI changes from SR to FS mode. The Planning Area is displayed
            */
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.FullSupervision;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in FS mode, Level 1." + Environment.NewLine + Environment.NewLine +
                                "2. DMI displays the Planning Area.");
            /*
            Test Step 2
            Action: Use the test script file  17_6_2_a.xml to send EVC-4 with,MMI_N_MRSP = 0
            Expected Result: Verify the following information, All PA Speed Profile Discontinuities symbol are removed from areas D6-D7
            Test Step Comment: (1) MMI_gen 7290;
            */
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN = 5500;
            TrackDescriptions = new List<TrackDescription>();
            EVC4_MMITrackDescription.TrackDescriptions = TrackDescriptions;

            XML_17_6_2(msgType.typeA);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays no speed discontinuites in the Planning Area.");

            /*
            Test Step 3
            Action: Use the test script file  17_6_2_b.xml to send EVC-4 with,MMI_N_MRSP = 3MMI_V_MRSP_CURR[0] = -1MMI_O_MRSP[0] = 1,000,050,000MMI_V_MRSP_CURR[0] = -2MMI_O_MRSP[0] = 1,000,100,000MMI_V_MRSP_CURR[0] = -3MMI_O_MRSP[0] = 1,000,200,000
            Expected Result: Verify the following information, The symbol PL22 is displayed at position 500m.The symbol PL21 is displayed at position 1000m.There is no symbol display at position 2000m
            Test Step Comment: (1) MMI_gen 2600 (partly: 1st bullet, result of calculation, 3rd bullet, PL22 symbol);(2) MMI_gen 2600 (partly: 1st bullet, result of calculation, 4th bullet, PL21 symbol);(3) MMI_gen 2600 (partly: 1st bullet, result of calculation, 5th bullet, Not place any Speed discontinuity symbol);
            */
            XML_17_6_2(msgType.typeB);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The speed decrease symbol, PL22 is displayed at position 500m." +
                                Environment.NewLine +
                                "2. The speed increase symbol, PL21 is displayed at position 1000m." +
                                Environment.NewLine +
                                "3. No symbol is displayed at position 2000m.");
            /*
            Test Step 4
            Action: Use the test script file  17_6_2_c.xml to send EVC-4 with,MMI_N_MRSP = 1MMI_V_MRSP_CURR[0] = 11112MMI_O_MRSP[0] = 1,000,050,000
            Expected Result: Verify the following information, An information of PA in area D are not updated
            Test Step Comment: (1) MMI_gen 7291 (partly: 1st bullet, MMI_V_MRSP has an invalid value);
            */
            XML_17_6_2(msgType.typeC);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. PA information in area D is not updated.");

            /*
            Test Step 5
            Action: Use the test script file  17_6_2_d.xml to send EVC-4 with,MMI_N_MRSP = 1MMI_V_MRSP_CURR[0] = 11111MMI_O_MRSP[0] = 2,147,483,648
            Expected Result: Verify the following information, An information of PA in area D are not updated
            Test Step Comment: (1) MMI_gen 7291 (partly: 2nd bullet, MMI_O_MRSP has an invalid value);
            */
            XML_17_6_2(msgType.typeD);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. PA information in area D is not updated.");

            /*
            Test Step 6
            Action: Use the test script file  17_6_2_e.xml to send EVC-4 with,MMI_N_MRSP = 1MMI_V_MRSP_CURR[0] = 11111MMI_O_MRSP[0] = 0
            Expected Result: Verify the following information, An information of PA in area D are not updated
            Test Step Comment: (1) MMI_gen 7291 (partly: 3rd bullet, value is not positive);
            */
            XML_17_6_2(msgType.typeE);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. PA information in area D is not updated.");

            /*
            Test Step 7
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }

        #region Send_XML_17_6_2_DMI_Test_Specification

        private enum msgType : byte
        {
            typeA,
            typeB,
            typeC,
            typeD,
            typeE
        }

        // Test spec is at odds with EVC4 definitions: values should be checked
        private void XML_17_6_2(msgType packetSelector)
        {
            switch (packetSelector)
            {
                case msgType.typeA:
                    EVC4_MMITrackDescription.MMI_V_MRSP_CURR = 2777;
                    EVC4_MMITrackDescription.MMI_G_GRADIENT_CURR = 0;
                    break;
                case msgType.typeB:
                    // OBU_TR_O_TRAIN was at 5500 (55m) O_MRSP - O_TRAIN => desired position
                    TrackDescriptions.Add(new TrackDescription {MMI_O_MRSP = 55500, MMI_V_MRSP = -1});
                    TrackDescriptions.Add(new TrackDescription {MMI_O_MRSP = 105500, MMI_V_MRSP = -2});
                    TrackDescriptions.Add(new TrackDescription {MMI_O_MRSP = 205500, MMI_V_MRSP = -3});
                    break;
                case msgType.typeC:
                    TrackDescriptions.Clear();
                    EVC4_MMITrackDescription.MMI_V_MRSP_CURR = 11112;
                    TrackDescriptions.Add(new TrackDescription {MMI_O_MRSP = 55500, MMI_V_MRSP = 1});
                    break;
                case msgType.typeD:
                    EVC4_MMITrackDescription.MMI_V_MRSP_CURR = 11111;
                    TrackDescriptions[0].MMI_O_MRSP =
                        -1; // 1 bigger than the largest signed int: VSIS refers to uint not int
                    break;
                case msgType.typeE:
                    TrackDescriptions[0].MMI_O_MRSP = 0;
                    TrackDescriptions[0].MMI_V_MRSP = 11111; // both valid
                    break;
            }

            EVC4_MMITrackDescription.Send();
        }

        #endregion
    }
}
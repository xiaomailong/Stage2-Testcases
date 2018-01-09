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
    /// 22.7.1 PA Speed Profile (PASP): Display in sub-area D7 and D8
    /// TC-ID: 17.7.1
    /// 
    /// This test case verifies  the presentation of PA speed profile that is displayed on the planning area in sub-area D7 and D8. The PA Speed Profile shall comply with [MMI-ETCS-gen] and [ERA-ERTMS] standard.
    /// 
    /// Tested Requirements:
    /// MMI_gen 9943; MMI_gen 7315 (partly: 1st bullet); MMI_gen 7323 (partly: MMI_gen 644); MMI_gen 648 (partly: width of each PASP segment); MMI_gen 644 (partly: 1st bullet, 3rd bullet, 4th bullet); MMI_gen 9944 (partly: shown in D7); MMI_gen 7316 (partly: up to 3 speed discontinuities); MMI_gen 9946; MMI_gen 7318 (partly: update the current PASP); MMI_gen 7325; MMI_gen 3027 (partly: 1st bullet, 3rd bullet, 4th bullet); MMI_gen 7321; MMI_gen 9945;
    /// 
    /// Scenario:
    /// Drive the train forward pass BG1 at position 50m. Then, verify that DMI displays the planning area correctly.BG1: Packet 12, 21 and 27Drive the train and then stop at each position that defined in each test step.Verify that PASP is updated correctly.Simulate the communication loss between ETCS Onboard and DMI. Then, verify that DMI hides the planning area correctly.Re-establishes the communication. Then, verify that DMI displays the planning area again.
    /// 
    /// Used files:
    /// 17_7_1.tdg
    /// </summary>
    public class TC_ID_17_7_1_PA_Speed_Profile_PASP_Display_in_sub_area_D7_and_D8 : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // System is power on.Complete to perform SoM in SR mode, Level 1.
            // The default configuration of PA distance scale is set as [0…4000] 
            // (variable DEFAULT_PAGE_DISPLAY in etcs_planningArea.xml is equal to 2)

            // Call the TestCaseBase PreExecution
            base.PreExecution();
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
            TraceInfo("This test case may require an ATP configuration change - " +
                      "See Precondition requirements. If this is not done manually, the test may fail!");

            /*
            Test Step 1
            Action: Drive the train forward and pass BG1.Then, slow down the train to make it stop at position 100m
            Expected Result: DMI changes from SR to FS mode. The Planning Area is displayed
            */
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.FullSupervision;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN = 10000;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI changes display from SR to FS mode, Level 1." + Environment.NewLine + Environment.NewLine +
                                "2. DMI displays the Planning Area.");

            /*
            Test Step 2
            Action: Stop the train
            Expected Result: DMI still displays the planning area. Verify that the PA Speed Profile is displayed is sub-area D7.All PASP segments are displayed with a resolution of ¼ the width of sub-area D7.Each PA Speed Profile segment is displayed with PASP-Light colour. At position 0-500m, the whole width of sub-area D7 is displayed in PASP-Light colour.The permitted speed is 100 km/h
            Test Step Comment: (1) MMI_gen 9943 (partly: area D7);(2) MMI_gen 7315  (partly: resolution ¼ of the width); (3) MMI_gen 7323 (partly: colour PASP-Light); (4) MMI_gen 7315 (partly: 1st bullet); MMI_gen 9944 (partly: the width of D7);   (5) MMI_gen 9944 (partly: represent the ceiling permitted speed);   
            */
            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Normal_Status_PreIndication_Monitoring;
            //EVC1_MMIDynamic.MMI_O_BRAKETARGET = 100000;       // ??
            EVC1_MMIDynamic.MMI_V_TARGET_KMH = 75;
            EVC1_MMIDynamic.MMI_V_PERMITTED_KMH = 100;
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 0;

            EVC4_MMITrackDescription.MMI_G_GRADIENT_CURR = 0;
            EVC4_MMITrackDescription.MMI_V_MRSP_CURR_KMH = 100;
            List<TrackDescription> trackDescriptions = new List<TrackDescription>
            {
                new TrackDescription {MMI_O_MRSP = 50000, MMI_V_MRSP_KMH = 95},
                new TrackDescription {MMI_O_MRSP = 100000, MMI_V_MRSP_KMH = 75},
                new TrackDescription {MMI_O_MRSP = 200000, MMI_V_MRSP_KMH = 60}
            };
            EVC4_MMITrackDescription.TrackDescriptions = trackDescriptions;
            EVC4_MMITrackDescription.Send();


            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI still displays the Planning Area." + Environment.NewLine +
                                "2. The PA Speed Profile (PASP) is displayed in sub-area D7." + Environment.NewLine +
                                "3. The resolution of all PASP segments is 1/4 of the width of sub-area D7." + Environment.NewLine +
                                "4. All PASP segments are displayed in the PASP-Light colour." + Environment.NewLine +
                                "5. The whole width of sub-area D7 is displayed in the PASP-Light colour at position 500m." + Environment.NewLine +
                                "6. The permitted speed = 100 km/h." + Environment.NewLine +
                                "7. Three speed discontinuities are displayed in sub-area D7:");

            /*
            Test Step 3
            Action: Press <Scale Down> button
            Expected Result: Verify the following information,There are only 3 speed discontinuities displayed at sub-area D7 and The width of each PA Speed Profile segments are displayed correctly refer as follows,0-500m: The width is covered all of sub-area D7.501-1000m: The width is covered only ¾ of sub-area D7.1001-2000m: The width is covered half of sub-area D7.2001-8000m: The width is covered only ¼ of sub-area D7.Use the log file to confirm that DMI receives packet information [MMI_TRACK_DESCRIPTION (EVC-4)] with variable MMI_V_MRSP_CURR = 2777 (approximately 100km/h).Use the log file to confirm the start position for each segment of PA speed profile from the differentiate of variable [MMI_TRACK_DESCRIPTION (EVC-4).MMI_O_MRSP] and [MMI_ETCS_MISC_OUT_SIGNALS (EVC-7).OBU_TR_O_TRAIN] as follows,[MMI_TRACK_DESCRIPTION (EVC-4).MMI_O_MRSP[0]] – [MMI_ETCS_MISC_OUT_SIGNALS (EVC-7).OBU_TR_O_TRAIN] is approximately to 60000 (600m)[MMI_TRACK_DESCRIPTION (EVC-4).MMI_O_MRSP[1]] – [MMI_ETCS_MISC_OUT_SIGNALS (EVC-7).OBU_TR_O_TRAIN] is approximately to 110000 (1100m)[MMI_TRACK_DESCRIPTION (EVC-4).MMI_O_MRSP[2]] – [MMI_ETCS_MISC_OUT_SIGNALS (EVC-7).OBU_TR_O_TRAIN] is approximately to 210000 (2100m)[MMI_TRACK_DESCRIPTION (EVC-4).MMI_O_MRSP[3]] – [MMI_ETCS_MISC_OUT_SIGNALS (EVC-7).OBU_TR_O_TRAIN] is approximately to 410000 (4100m)[MMI_TRACK_DESCRIPTION (EVC-4).MMI_O_MRSP[4]] – [MMI_ETCS_MISC_OUT_SIGNALS (EVC-7).OBU_TR_O_TRAIN] is approximately to 610000 (6100m).The symbol PL21 is displayed in sub area D6-D7 at position 6000m.   PL21Use the log file to confirm the value for each index of variable MMI_V_MRSP in received packet [MMI_TRACK_DESCRIPTION (EVC-4)] as follows,MMI_V_MRSP[4] > MMI_V_MRSP[3]Note: The first index is MMI_V_MRSP[0]The PASP is displayed within 8000m according to movement authorize (MA) and up to the first zero target speed.At the fourth speed restriction (position 2001-8000m), there is PL23 symbol displays at position 8000m
            Test Step Comment: (1) MMI_gen 648    (partly: width of each PASP segment);      MMI_gen 644          (partly: 3rd bullet, 4th bullet);                                         MMI_gen 9944         (partly: shown in D7);                   MMI_gen 7323 (partly: PASP width, MMI_gen 644); MMI_gen 7316 (partly: up to 3 speed discontinuities);      MMI_gen 7318 (partly: update the current PASP); MMI_gen 644 (partly: 1st bullet);(2) MMI_gen 9946 (partly: PL21);      (3) MMI_gen 9946 (partly: 1st bullet);(4) MMI_gen 9943 (partly: within movement authority and up to the first target at zero speed);(5) MMI_gen 9945;
            */
            DmiActions.ShowInstruction(this, "Press the ‘Scale Down’ button");

            trackDescriptions.Clear();
            trackDescriptions.Add(new TrackDescription { MMI_O_MRSP = 70000, MMI_V_MRSP_KMH = 95 });
            trackDescriptions.Add(new TrackDescription { MMI_O_MRSP = 120000, MMI_V_MRSP_KMH = 75 });
            trackDescriptions.Add(new TrackDescription { MMI_O_MRSP = 210000, MMI_V_MRSP_KMH = 60 });
            trackDescriptions.Add(new TrackDescription { MMI_O_MRSP = 420000, MMI_V_MRSP_KMH = 25 });
            trackDescriptions.Add(new TrackDescription { MMI_O_MRSP = 610000, MMI_V_MRSP_KMH = 35 });            
            EVC4_MMITrackDescription.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI still displays the Planning Area." + Environment.NewLine + 
                                "2. The PA Speed Profile (PASP) is displayed in sub-area D7." + Environment.NewLine +
                                "3. The resolution of all PASP segments is 1/4 of the width of sub-area D7." + Environment.NewLine +
                                "4. All PASP segments are displayed in the PASP-Light colour." + Environment.NewLine +
                                "5. The whole width of sub-area D7 is displayed in the PASP-Light colour at position 500m." + Environment.NewLine +
                                "6. The permitted speed = 100 km/h." + Environment.NewLine + 
                                "7. Three speed discontinuities are displayed in sub-area D7:" + Environment.NewLine +
                                "8. From 0-500m the whole width of sub-area D7 is included;" + Environment.NewLine +
                                "9. From 501-1000m, 3/4 of the width of sub-area D7 is included;" + Environment.NewLine +
                                "10. From 1001-2000m, 1/2 of the width of sub-area D7 is included;" + Environment.NewLine +
                                "11. From 2000-8000m, 1/4 of the width of sub-area D7 is included." + Environment.NewLine +
                                "12. Speed decrease symbols (PL22) are displayed at positions ~600m, ~1100m, ~2100m and ~4100m." + Environment.NewLine +
                                "13. The speed increase symbol (PL21) is displayed at position 6000m." + Environment.NewLine +
                                "14. The speed decrease to zero target symbol (PL23) is displayed at position 8000m.");
            /*
            Test Step 4
            Action: Drive the train forward
            Expected Result: Verify the following information,While the train is running forward, each segment of PA Speed Profile are moving down to the zero line
            Test Step Comment: (1) MMI_gen 3027 (partly: 1st bullet, 3rd bullet);
            */
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN = 40000;
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 30;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Each PASP segment is moved towards the zero line.");


            /*
            Test Step 5
            Action: Continue to drive the train forward
            Expected Result: Verify the following information,The 1st segment of PA Speed Profile is removed from sub-area D7 after passed the zero line.After the 1st segment is removed, There are only 3 speed discontinuities displayed at sub-area D7 and The width of each PA Speed Profile segments are displayed correctly refer as follows,The 1st segment (Red frame in picture above) of PA Speed Profile is covered full width of sub-area D7.The 2nd segment (upper red frame in picture above) of PA Speed Profile is covered ¾ width of sub-area D7.The 3rd segment (below yellow frame in picture above) of PA Speed Profile is covered half width of sub-area D7.The last segment Yellow frame in picture above) is covered only ¼ width of sub-area D7.Use the log file to confirm that DMI receives packet information [MMI_TRACK_DESCRIPTION (EVC-4)] with variable MMI_V_MRSP_CURR = 2083 (approximately 75 km/h)
            Test Step Comment: (1) MMI_gen 3027 (partly: 4th bullet);                 (2) MMI_gen 7321; MMI_gen 7315 (partly: resolution ¼ of the width); MMI_gen 648 (partly: width of each PASP segment); MMI_gen 7316 (partly: up to 3 speed discontinuities);             
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 75;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN = 65500;
            EVC4_MMITrackDescription.MMI_V_MRSP_CURR_KMH = 75;
            EVC4_MMITrackDescription.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The first (nearest to the zero line) PASP segment is removed from sub-area D7 (past zero line)." + Environment.NewLine +
                                "2. Four discontinuities are displayed in sub-area D7 with the following proportions:" + Environment.NewLine +
                                "3. Segment #1 has the full width of sub-area D7;" + Environment.NewLine +
                                "4. Segment #2 has 3/4 of the width of sub-area D7;" + Environment.NewLine +
                                "5. Segment #3 has 1/2 of the width of sub-area D7;" + Environment.NewLine +
                                "6. The last segment has 1/4 of the width of sub-area D7.");

            /*
            Test Step 6
            Action: Continue to drive the train forward
            Expected Result: Verify the following information,The 1st segment of PA Speed Profile is removed from sub-area D7 after passed the zero line.After the 1st segment is removed, There are only 3 speed discontinuities displayed at sub-area D7 and The width of each PA Speed Profile segments are displayed correctly refer as follows,The 1st segment (Red frame in picture above) of PA Speed Profile is covered full width of sub-area D7. The 2nd segment (upper red frame in picture above) of PA Speed Profile is covered ¾ width of sub-area D7.The last segment (Yellow frame in picture above) is covered half width of sub-area D7.Use the log file to confirm that DMI receives packet information [MMI_TRACK_DESCRIPTION (EVC-4)] with variable MMI_V_MRSP_CURR = 1666 (approximately 60 km/h)
            Test Step Comment: (1) MMI_gen 3027 (partly: 4th bullet);                 (2) MMI_gen 7321; MMI_gen 7315 (partly: resolution ¼ of the width); MMI_gen 648 (partly: width of each PASP segment); MMI_gen 7316 (partly: up to 3 speed discontinuities);                  
            */
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN = 115000;
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 60;
            EVC4_MMITrackDescription.MMI_V_MRSP_CURR_KMH = 60;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The first (nearest to the zero line) PASP segment is removed from sub-area D7 (past zero line)." + Environment.NewLine +
                                "2. Three discontinuities are displayed in sub-area D7 with the following proportions:" + Environment.NewLine +
                                "3. Segment #1 has the full width of sub-area D7;" + Environment.NewLine +
                                "4. Segment #2 has 3/4 of the width of sub-area D7;" + Environment.NewLine +
                                "5. Segment #3 has 1/2 of the width of sub-area D7.");

            /*
            Test Step 7
            Action: Continue to drive the train forward
            Expected Result: Verify the following information,The 1st segment of PA Speed Profile is removed from sub-area D7 after passed the zero line.After the 1st segment is removed, There are only 3 speed discontinuities displayed at sub-area D7 and The width of each PA Speed Profile segments are displayed correctly refer as follows,The 1st segment (position 0-2000m refer to PA distance scale) of PA Speed Profile is covered full width of sub-area D7.The last segment (position 2001m-6000m refer to PA distance scale) is covered ¾  of sub-area D7.Use the log file to confirm that DMI receives packet information [MMI_TRACK_DESCRIPTION (EVC-4)] with variable MMI_V_MRSP_CURR = 1250 (approximately 45 km/h)
            Test Step Comment: (1) MMI_gen 3027 (partly: 4th bullet);                 (2) MMI_gen 7321; MMI_gen 7315 (partly: resolution ¼ of the width); MMI_gen 648 (partly: width of each PASP segment); MMI_gen 7316 (partly: up to 3 speed discontinuities);                   
            */
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN = 215000;
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 45;
            EVC4_MMITrackDescription.MMI_V_MRSP_CURR_KMH = 45;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The first (nearest to the zero line) PASP segment is removed from sub-area D7 (past zero line)." + Environment.NewLine +
                                "2. Two discontinuities are displayed in sub-area D7 with the following proportions:" + Environment.NewLine +
                                "3. Segment #1 has the full width of sub-area D7;" + Environment.NewLine +
                                "4. Segment #2 has 3/4 of the width of sub-area D7.");

            /*
            Test Step 8
            Action: Continue to drive the train forward
            Expected Result: Verify the following information,The 1st segment of PA Speed Profile is removed from sub-area D7 after passed the zero line.After the 1st segment is removed, There are only 3 speed discontinuities displayed at sub-area D7 and The width of each PA Speed Profile segments are displayed correctly refer as follows,The remaining distance until EOA of PA Speed Profile is covered full width of sub-area D7.Use the log file to confirm that DMI receives packet information [MMI_TRACK_DESCRIPTION (EVC-4)] with variable MMI_V_MRSP_CURR = 972 (approximately 35 km/h).There is no symbol PL22 displayed between PL21 and PL23 symbol.    PL22   PL23Use the log file to confirm the value for each index of variable MMI_V_MRSP in received packet [MMI_TRACK_DESCRIPTION (EVC-4)] as follows,MMI_V_MRSP[0] > MMI_V_MRSP_CURRNote: The first index is MMI_V_MRSP[0]The symbol PL21 is displayed in sub area D6-D7 at position 2000m.   PL21
            Test Step Comment: (1) MMI_gen 3027 (partly: 4th bullet);                 (2) MMI_gen 7321; MMI_gen 7315 (partly: resolution ¼ of the width); MMI_gen 648 (partly: width of each PASP segment); MMI_gen 7316 (partly: up to 3 speed discontinuities);             (3) MMI_gen 9946 (partly: speed decrease to the first target at zero speed);(4) MMI_gen 9946 (partly: 2nd  bullet);(5) MMI_gen 9946 (partly: PL21);      
            */
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN = 415000;
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 35;
            EVC4_MMITrackDescription.MMI_V_MRSP_CURR_KMH = 35;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The first (nearest to the zero line) PASP segment is removed from sub-area D7 (past zero line)." + Environment.NewLine +
                                "2. One discontinuity is displayed in sub-area D7 with the following proportions:" + Environment.NewLine +
                                "3. The segment has the full width of sub-area D7." + Environment.NewLine +
                                "4. No speed decrease symbol (PL22) is displayed between symbols PL21 and PL23." + Environment.NewLine +
                                "4. The Speed increase symbol (PL21) is displayed at position 2000m.");

            /*
            Test Step 9
            Action: Simulate the communication loss between ETCS Onboard and DMI
            Expected Result: DMI displays the  message “ATP Down Alarm” with sound.Verify that the PA Speed Profile segments are removed from DMI
            Test Step Comment: (1) MMI_gen 7325;
            */
            // Call generic Action Method
            DmiActions.Simulate_communication_loss_EVC_DMI(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the message ‘ATP Down Alarm’." + Environment.NewLine +
                                "2. The ‘Alarm’ sound is displayed." + Environment.NewLine +
                                "3. The PASP segments are not displayed.");

            /*
            Test Step 10
            Action: Re-establish the communication between ETCS onboard and DMI
            Expected Result: The PA Speed Profile segments are reappeared
            */
            // Call generic Action Method
            DmiActions.Re_establish_communication_EVC_DMI(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI re-displays the PASP segments.");

            /*
            Test Step 11
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
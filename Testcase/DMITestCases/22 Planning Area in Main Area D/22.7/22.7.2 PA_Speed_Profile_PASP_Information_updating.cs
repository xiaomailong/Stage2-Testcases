using System;
using System.Collections.Generic;
using Testcase.Telegrams.EVCtoDMI;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 22.7.2 PA Speed Profile (PASP): Information updating
    /// TC-ID: 17.7.2
    /// 
    /// This test case verify the display information of PA Speed Profile segment refer to received packets information EVC-4 which contain zero speed profile (MMI_V_MRSP =0) and the empty area of PA Speed Profile segment (MMI_N_MRSP = 0)
    /// 
    /// Tested Requirements:
    /// MMI_gen 7313; MMI_gen 2897; MMI_gen 2599 (partly: value 0, 1st bullet, 3rd bullet); MMI_gen 7323 (partly: MMI_gen 2599);
    /// 
    /// Scenario:
    /// Activate Cabin A.Perform SoM in SR mode, Level 1.Drive the train forward pass BG1 at position 50m. BG1: Packet 12, 21 and 27Use the test script file to send packet information EVC-
    /// 4.Then, verify that all PASP segments are updated correctly.
    /// 
    /// Used files:
    /// 17_7_2.tdg, 17_7_2.xml
    /// </summary>
    public class TC_ID_17_7_2_PA_Speed_Profile_PASP_Information_updating : TestcaseBase
    {
        static List<TrackDescription> TrackDescriptions;

        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // System is power on.

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
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 0;
            // Testcase entrypoint

            TraceHeader("Test Step 1");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Activate cabin A then  perform SoM to SR mode, selects level 1");
            TraceReport("Expected Result");
            TraceInfo("DMI displays in SR mode, level 1");
            /*
            Test Step 1
            Action: Activate cabin A then  perform SoM to SR mode, selects level 1
            Expected Result: DMI displays in SR mode, level 1
            */
            DmiActions.Complete_SoM_L1_SR(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in SR Mode, Level 1.");

            TraceHeader("Test Step 2");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Drive the train forward pass BG1.Then, Stop the train");
            TraceReport("Expected Result");
            TraceInfo(
                "DMI changes from SR to FS mode. The Planning Area is displayed.Use the log file to confirm that DMI received packet information EVC-4 with variable MMI_V_MRSP[1] = 0Use the log file to confirm the start position for the segment of PA speed profile which have a value of [MMI_TRACK_DESCRIPTION (EVC-4).MMI_V_MRSP] =0 from the differentiate of variable [MMI_TRACK_DESCRIPTION (EVC-4).MMI_O_MRSP] and [MMI_ETCS_MISC_OUT_SIGNALS (EVC-7).OBU_TR_O_TRAIN] as follows,[MMI_TRACK_DESCRIPTION (EVC-4).MMI_O_MRSP[1]] – [MMI_ETCS_MISC_OUT_SIGNALS (EVC-7).OBU_TR_O_TRAIN] is approximately to 200000 (2000m)The width of each PA Speed Profile segments are displayed correctly as follows,0-1000m: The width is covered all of sub-area D7.1001-2000m: The width is covered only ¼ of sub-area D7.At position beyond 2000m, the whole width of sub-area D7 is displayed in PASP-Dark colour. (There is no PA Speed Profile segment drawn)");
            /*
            Test Step 2
            Action: Drive the train forward pass BG1.Then, Stop the train
            Expected Result: DMI changes from SR to FS mode. The Planning Area is displayed.Use the log file to confirm that DMI received packet information EVC-4 with variable MMI_V_MRSP[1] = 0Use the log file to confirm the start position for the segment of PA speed profile which have a value of [MMI_TRACK_DESCRIPTION (EVC-4).MMI_V_MRSP] =0 from the differentiate of variable [MMI_TRACK_DESCRIPTION (EVC-4).MMI_O_MRSP] and [MMI_ETCS_MISC_OUT_SIGNALS (EVC-7).OBU_TR_O_TRAIN] as follows,[MMI_TRACK_DESCRIPTION (EVC-4).MMI_O_MRSP[1]] – [MMI_ETCS_MISC_OUT_SIGNALS (EVC-7).OBU_TR_O_TRAIN] is approximately to 200000 (2000m)The width of each PA Speed Profile segments are displayed correctly as follows,0-1000m: The width is covered all of sub-area D7.1001-2000m: The width is covered only ¼ of sub-area D7.At position beyond 2000m, the whole width of sub-area D7 is displayed in PASP-Dark colour. (There is no PA Speed Profile segment drawn)
            Test Step Comment: (1) MMI_gen 2599 (partly: value 0); MMI_gen 7323 (partly: MMI_gen 2599);(2) MMI_gen 2599 (partly: 1st bullet);(3) MMI_gen 2599 (partly: 3rd bullet);
            */
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.FullSupervision;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN = 5000;
            EVC4_MMITrackDescription.MMI_G_GRADIENT_CURR = 0;
            EVC4_MMITrackDescription.MMI_V_MRSP_CURR = 833; // 30 km/h

            TrackDescriptions = new List<TrackDescription>
            {
                new TrackDescription {MMI_O_MRSP = 105000},
                new TrackDescription {MMI_V_MRSP = 0, MMI_O_MRSP = 205000}
            };
            EVC4_MMITrackDescription.TrackDescriptions = TrackDescriptions;
            EVC4_MMITrackDescription.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in FS Mode, Level 1." + Environment.NewLine +
                                "2. DMI displays the Planning Area." + Environment.NewLine +
                                "3. From the zero line to position 1000m the whole width of sub-area D7 is in PASP-light." +
                                Environment.NewLine +
                                "4. From 1001m to position 2000m the PASP segment covers 1/4 of the width of sub-area D7." +
                                Environment.NewLine +
                                "5. Beyond 2000m the whole width of sub-area D7 is in PASP-dark." +
                                Environment.NewLine +
                                "6. (No PASP segments are displayed beyond 2000m.)");

            TraceHeader("Test Step 3");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Use the test script file  17_7_2.xml to send EVC-4 with,MMI_N_MRSP = 0MMI_V_MRSP_CURR = 2777");
            TraceReport("Expected Result");
            TraceInfo(
                "The value of PA Gradient Profile is changed to 20.The previous PASP segment from step 2 is removed from DMI.The current PASP Segment is end up in infinity (see picture in comment)");
            /*
            Test Step 3
            Action: Use the test script file  17_7_2.xml to send EVC-4 with,MMI_N_MRSP = 0MMI_V_MRSP_CURR = 2777
            Expected Result: The value of PA Gradient Profile is changed to 20.The previous PASP segment from step 2 is removed from DMI.The current PASP Segment is end up in infinity (see picture in comment)
            Test Step Comment: (1) MMI_gen 7313 (partly: Delete all PASP segments);(2) MMI_gen 7313       (partly: 2nd  bullet);           
            */
            XML_17_7_2(msgPart.part1);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The PASP segment covering 1/4 of the width of sub-area D7 is removed." +
                                Environment.NewLine +
                                "2. The PA Gradient Profile value displayed is ‘20’." + Environment.NewLine +
                                "3. The current PASP segment ends at infinity (displayed from 0 to beyond 4000m).");

            TraceHeader("Test Step 4");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("(Continue from step 3)Send EVC-4 with,MMI_N_MRSP = 0MMI_V_MRSP_CURR = 0");
            TraceReport("Expected Result");
            TraceInfo(
                "The value of PA Gradient Profile is changed to 10.Verify the following information,The current PASP Segments are deleted from area D7.The background colour of area D7 and D8 is PASP-Dark colour (see picture in comment)");
            /*
            Test Step 4
            Action: (Continue from step 3)Send EVC-4 with,MMI_N_MRSP = 0MMI_V_MRSP_CURR = 0
            Expected Result: The value of PA Gradient Profile is changed to 10.Verify the following information,The current PASP Segments are deleted from area D7.The background colour of area D7 and D8 is PASP-Dark colour (see picture in comment)
            Test Step Comment: (1) MMI_gen 7313       (partly: 1st bullet);           (2) MMI_gen 2897;
            */
            XML_17_7_2(msgPart.part2);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The PASP segment is removed." + Environment.NewLine +
                                "2. The PA Gradient Profile value displayed is ‘10’." + Environment.NewLine +
                                "3. Sub-areas D7 and D8 are displayed with a PASP-dark background.");

            TraceHeader("Test Step 5");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("End of test");
            TraceReport("Expected Result");
            TraceInfo("");
            /*
            Test Step 5
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }

        #region Send_XML_17_7_2_DMI_Test_Specification

        private enum msgPart : byte
        {
            part1,
            part2
        }

        private void XML_17_7_2(msgPart packetSelector)
        {
            switch (packetSelector)
            {
                case msgPart.part1:
                    TrackDescriptions.Clear();
                    EVC4_MMITrackDescription.MMI_G_GRADIENT_CURR = 20;
                    EVC4_MMITrackDescription.MMI_V_MRSP_CURR = 2777;
                    EVC4_MMITrackDescription.Send();
                    break;
                case msgPart.part2:
                    EVC4_MMITrackDescription.MMI_V_MRSP_CURR = 0;
                    EVC4_MMITrackDescription.MMI_G_GRADIENT_CURR = 10;
                    EVC4_MMITrackDescription.Send();
                    break;
            }
        }

        #endregion
    }
}
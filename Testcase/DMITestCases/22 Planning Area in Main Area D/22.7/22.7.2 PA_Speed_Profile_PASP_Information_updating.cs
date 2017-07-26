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
    public class PA_Speed_Profile_PASP_Information_updating : TestcaseBase
    {
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
            // Testcase entrypoint


            /*
            Test Step 1
            Action: Activate cabin A then  perform SoM to SR mode, selects level 1.
            Expected Result: DMI displays in SR mode, level 1.
            */

            /*
            Test Step 2
            Action: Drive the train forward pass BG1.Then, Stop the train.
            Expected Result: DMI changes from SR to FS mode. The Planning Area is displayed.Use the log file to confirm that DMI received packet information EVC-4 with variable MMI_V_MRSP[1] = 0Use the log file to confirm the start position for the segment of PA speed profile which have a value of [MMI_TRACK_DESCRIPTION (EVC-4).MMI_V_MRSP] =0 from the differentiate of variable [MMI_TRACK_DESCRIPTION (EVC-4).MMI_O_MRSP] and [MMI_ETCS_MISC_OUT_SIGNALS (EVC-7).OBU_TR_O_TRAIN] as follows,[MMI_TRACK_DESCRIPTION (EVC-4).MMI_O_MRSP[1]] – [MMI_ETCS_MISC_OUT_SIGNALS (EVC-7).OBU_TR_O_TRAIN] is approximately to 200000 (2000m)The width of each PA Speed Profile segments are displayed correctly as follows,0-1000m: The width is covered all of sub-area D7.1001-2000m: The width is covered only ¼ of sub-area D7.At position beyond 2000m, the whole width of sub-area D7 is displayed in PASP-Dark colour. (There is no PA Speed Profile segment drawn).
            Test Step Comment: (1) MMI_gen 2599 (partly: value 0); MMI_gen 7323 (partly: MMI_gen 2599);(2) MMI_gen 2599 (partly: 1st bullet);(3) MMI_gen 2599 (partly: 3rd bullet);
            */

            /*
            Test Step 3
            Action: Use the test script file  17_7_2.xml to send EVC-4 with,MMI_N_MRSP = 0MMI_V_MRSP_CURR = 2777
            Expected Result: The value of PA Gradient Profile is changed to 20.The previous PASP segment from step 2 is removed from DMI.The current PASP Segment is end up in infinity (see picture in comment).
            Test Step Comment: (1) MMI_gen 7313 (partly: Delete all PASP segments);(2) MMI_gen 7313       (partly: 2nd  bullet);           
            */

            /*
            Test Step 4
            Action: (Continue from step 3)Send EVC-4 with,MMI_N_MRSP = 0MMI_V_MRSP_CURR = 0
            Expected Result: The value of PA Gradient Profile is changed to 10.Verify the following information,The current PASP Segments are deleted from area D7.The background colour of area D7 and D8 is PASP-Dark colour (see picture in comment).
            Test Step Comment: (1) MMI_gen 7313       (partly: 1st bullet);           (2) MMI_gen 2897;
            */

            /*
            Test Step 5
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
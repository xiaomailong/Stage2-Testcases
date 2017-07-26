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
    /// 23.7 Tunnel stopping area track condition
    /// TC-ID: 18.7
    /// 
    /// This test case verifies a correctness of display information for Tunnel stopping area symbol and Tunnel stopping area announcement symbol with the remaining dist refer to received packet EVC-32.
    /// 
    /// Tested Requirements:
    /// MMI_gen 9481; MMI_gen 9487; MMI_gen 10474; MMI_gen 9488 (partly: touch screen); MMI_gen 9489; MMI_gen 9491; MMI_gen 9492; MMI_gen 10476; MMI_gen 9493; MMI_gen 9494; MMI_gen 10473 (partly: MMI_gen 662, MMI_gen 10465, MMI_gen 10469 (partly: no update), MMI_gen 10470, MMI_gen 663, MMI_gen 664 (partly: Flashing yellow frame), MMI_gen 668, Note under MMI_gen 668); MMI_gen 9516 (partly: symbol requires driver's action, non-acknowledgable); MMI_gen 12025 (partly: symbol requires driver's action, non-acknowledgable);
    /// 
    /// Scenario:
    /// Drive the train forward pass BG1 at position 100m. Then, press the sensitive area C2-C4 and verify that no symbol is display. BG1: Packet 12, 21 and 27 (Entering FS).Continue to drive the train forward pass BG2 at position 200m. Then, stop the train and verify the display information of tunnel stopping symbol TC37 refer to received packet EVC-32.BG2: Packet 68 (Track condition)Press the sensitive area C2-C4 and verify the display of tunnel stopping symbols.Drive the train forward until the remaining distance on DMI is zero. Then, verify the display information of tunnel stopping symbol TC36 refer to received packet EVC-32.Continue to drive the train forward pass BG3 at position 400m. Then, stop the train and verify the display information of tunnel stopping symbol TC37 with remaining distance refer to received packet EVC-32.BG3: Packet 68 (Track condition)Continue to drive the train forward pass BG4 at position 600m. Then, stop the train and verify the display information of tunnel stopping symbol TC37 without remaining distance.BG3: Packet 68 (2 Track condition)Simulate the loss-communication between DMI and ETCS onboard. Then, verify that all tunnel stopping information is removed.Re-establish communication between DMI and ETCS onboard. Then, verify that all tunnel stopping information is resume to display.De-activate cabin and simulate the loss-communication between DMI and ETCS onboard. Then, verify that all tunnel stopping information is removed.Activate cabin and re-establish communication between DMI and ETCS onboard. Then, verify that all tunnel stopping information is resume to display.
    /// 
    /// Used files:
    /// 18_7.tdg
    /// </summary>
    public class Tunnel_stopping_area_track_condition : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // System is power on.SoM is performed in SR mode, Level 1.

            // Call the TestCaseBase PreExecution
            base.PreExecution();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in FS mode, Level 1.

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint


            /*
            Test Step 1
            Action: Drive the train forward pass BG1.
            Expected Result: DMI displays in FS mode, Level 1.
            */

            /*
            Test Step 2
            Action: Press at sub-area C2, C3 and C4.
            Expected Result: Verify the following information,(1)   The sub-areas C2, C3 and C4 are not sensitive, no symbol display at sub-area C2, C3 and C4.
            Test Step Comment: (1) MMI_gen 9494;     MMI_gen 10474      (partly: not known);
            */

            /*
            Test Step 3
            Action: Continue to drive the train forward pass BG2.Then, Stop the train.
            Expected Result: Verify the following information,(1)  Use the log file to confirm that DMI received packet EVC-32 with following variables,MMI_Q_TRACKCOND_UPDATE = 0MMI_M_TRACKCOND_TYPE = 1MMI_Q_TRACKCOND_STEP = 1MMI_Q_TRACKCOND_ACTION_START = 0 (2)  The Tunnel stopping area announcement symbol TC37 is display in sub-area C2 with yellow flashing frame.(3)  The remaining distance to tunnel stopping area is display in sub-area C4.(4)  The display of remaining distance is consistent with the differentiation of following variables in equation below,MMI_TRACK_CONDITIONS (EVC-32). MMI_O_TRACKCOND_START[0] – MMI_ETCS_MISC_OUT_SIGNAL (EVC-7). OBU_TR_O_TRAIN(5)  Sound ‘Sinfo’ is played.
            Test Step Comment: (1) MMI_gen 10474 (partly: known TC37); MMI_gen 10473 (partly: MMI_gen 662, MMI_gen 10465, MMI_gen 10470, MMI_gen 10469 (partly: no update));(2) MMI_gen 9481 (partly: C2); MMI_gen 9491 (partly: TC37); MMI_gen 10473 (partly: MMI_gen 664 (partly: Flashing yellow frame));(3) MMI_gen 9492; MMI_gen 9481 (partly: C4);(4) MMI_gen 10476 (partly: calculate);(5) MMI_gen 10473 (partly: MMI_gen 663); MMI_gen 9516 (partly: symbol requires driver's action, non-acknowledgable); MMI_gen 12025 (partly: symbol requires driver's action, non-acknowledgable);
            */

            /*
            Test Step 4
            Action: Press at sub-area C2.
            Expected Result: Verify the following information,(1)  The symbol DR05 is display in sub-area C3.
            Test Step Comment: (1) MMI_gen 9489 (partly: C2);  MMI_gen 9488 (partly:toggle off, C3); MMI_gen 9487; MMI_gen 9481 (partly: C3);
            */

            /*
            Test Step 5
            Action: Press at sub-area C2.
            Expected Result: Verify the following information,(1)  DMI displays symbol TC37 with the remaining distance in sub-area C2 and C4 same as expected result in step 2.
            Test Step Comment: (1) MMI_gen 9488 (partly:toggle on, C2);
            */

            /*
            Test Step 6
            Action: Perform action step4-5 for sub-area C3 and C4.
            Expected Result: See the expected result of step 4-5.
            Test Step Comment: MMI_gen 9488 (partly: toggle, C3, C4);MMI_gen 9489 (partly: C3,C4);
            */

            /*
            Test Step 7
            Action: Continue to drive the train forward until the remaining distance in sub-area C4 is become 0.Then, stop the train.
            Expected Result: Verify the following information,(1)  Use the log file to confirm that DMI received packet EVC-32 with following variables,MMI_M_TRACKCOND_TYPE = 1MMI_Q_TRACKCOND_STEP = 2(2)  The Tunnel stopping area announcement symbol TC36 is display in sub-area C2.(3)  Use the log file to confirm that the result of differentiation for the following variables is less than zero,MMI_TRACK_CONDITIONS (EVC-32). MMI_O_TRACKCOND_START[0] – MMI_ETCS_MISC_OUT_SIGNAL (EVC-7). OBU_TR_O_TRAIN(4)  There is no information of the remaining distance display on DMI.
            Test Step Comment: (1) MMI_gen 10474 (partly: known TC36);(2) MMI_gen 9491 (partly: TC36);(3) MMI_gen 10476 (partly: remaining distance is negative;(4) MMI_gen 10476 (partly: no distance be displayed); 
            */

            /*
            Test Step 8
            Action: Repeat action step 4-6.
            Expected Result: Verify the following information,(1)  The display information is toggled between the 2 following symbols refer to each pressing,The symbol DR05 is display in sub-area C3.The Tunnel stopping area announcement symbol TC36 is display in sub-area C2.
            Test Step Comment: (1) MMI_gen 9491 (partly: TC37);
            */

            /*
            Test Step 9
            Action: Continue to drive the train forward pass BG3.
            Expected Result: DMI display symbol TC36 with the remaining distance to tunnel stopping area is display in sub-area C4.Verify the following information,(1)   The remaining distance is show up to 5 digits with resolution 1m. Note: Use the equation in expected result no.4 of step 3 to verify the resolution.(2)  The colour of remaining distance is grey. (3)  The remaining distance is right aligned and vertically center.
            Test Step Comment: (1) MMI_gen 9493 (partly: 5 digits with resolution 1m);(2) MMI_gen 9493 (partly: grey);(3) MMI_gen 9493 (partly: right aligned and vertically center);
            */

            /*
            Test Step 10
            Action: Continue to drive the train forward pass BG4.
            Expected Result: DMI display symbol TC37 with yellow flashing frame.Verify the following information,(1)    There is no information of the remaining distance display on DMI.
            Test Step Comment: (1) MMI_gen 10476 (partly: more track condition with symbol TC37); 
            */

            /*
            Test Step 11
            Action: Simulate loss-communication between ETCS onboard and DMI
            Expected Result: Verify the following information,(1)  The symbol TC37 is removed from DMI.
            Test Step Comment: (1) MMI_gen 10473 (partly: MMI_gen 668 (partly: MMI_gen 244);
            */

            /*
            Test Step 12
            Action: Re-establish communication between ETCS onboard and DMI.
            Expected Result: Verify the following information,(1)  The symbol TC37 is resume to display on DMI.
            Test Step Comment: (1) MMI_gen 10473 (partly: Note under MMI_gen 668);
            */

            /*
            Test Step 13
            Action: Deactivate cabin.Then, simulate loss-communication between ETCS onboard and DMI
            Expected Result: Verify the following information,(1)  The symbol TC37 is removed from DMI.
            Test Step Comment: (1) MMI_gen 10473 (partly: MMI_gen 668 (partly: MMI_gen 240);
            */

            /*
            Test Step 14
            Action: Activate cabin.Then, re-establish communication between ETCS onboard and DMI.
            Expected Result: Verify the following information,(1)  The symbol TC37 is resume to display on DMI.
            Test Step Comment: (1) MMI_gen 10473 (partly: Note under MMI_gen 668);
            */

            /*
            Test Step 15
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
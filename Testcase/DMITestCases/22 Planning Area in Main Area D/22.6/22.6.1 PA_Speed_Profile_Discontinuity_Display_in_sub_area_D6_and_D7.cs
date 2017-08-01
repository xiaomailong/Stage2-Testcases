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
    /// 22.6.1 PA Speed Profile Discontinuity: Display in sub-area D6 and D7
    /// TC-ID: 17.6.1
    /// 
    /// This test case verifies the appearance of Speed Profile Discontinuity that displays on the Planning Area in sub-area D6 and D7. The condition of Speed Profile Discontinuity shall comply with [MMI-ETCS-gen] and [ERA-ERTMS] standard.
    /// 
    /// Tested Requirements:
    /// MMI_gen 9941; MMI_gen 7293; MMI_gen 9942 (partly: EOA, LOA, the symbol PL21, PL22. PL23); MMI_gen 641 (partly: 1st bullet, result of calculation, 4th bullet, result of comparision to display PL21 symbol, result of comparision to display PL22 symbol, result of comparision to display PL23 symbol); MMI_gen 643 (partly: 1st bullet, changes during train movement, 3rd bullet, move to calculated Y-coordinate, 4th bullet, deleted); MMI_gen 622; MMI_gen 1418; MMI_gen 6956; MMI_gen 7295;
    /// 
    /// Scenario:
    /// Drive the train forward pass BG1 at position 50m. Then, verifty the displays of PA Speed Profile Discontinuities symbols refer to received packet information EVC-4.BG1: packet 12, 21 and 27 (Entering FS)Drive the train forward pas BG2 at position 600m. The verify the display information for PA Speed Profile Discontinuities symbol when it’s overlapping.BG2: packet 12, 21 and 27 (Entering FS)Drive the train forward pas BG3 at position 1000m. The verify the display information for PA Speed Profile Discontinuities symbol when it’s overlapping.BG2: packet 12, 21 and 27 (Entering FS)Simulate the communication loss between ETCS Onboard and DMI. Then, verify that DMI hides the planning area correctly.Re-establishes the communication. Then, verify that DMI displays the planning area again.
    /// 
    /// Used files:
    /// 17_6_1.tdg
    /// </summary>
    public class PA_Speed_Profile_Discontinuity_Display_in_sub_area_D6_and_D7 : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // Test system is power on.Cabin is activated.SoM is performed in SR mode, Level1.

            // Call the TestCaseBase PreExecution
            base.PreExecution();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // Cabin is inactive, DMI displays the message “Driver’s cab not active”

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint


            /*
            Test Step 1
            Action: Drive the train forward and pass BG1.Then, stop the train
            Expected Result: DMI displays in FS mode, Level 1.Verify the following information,The PA Speed Profile Discontinuities symbol are displayed in area D6-D7 and the right half of PA Speed Profile Discontinuities symbol are extended into area D7, a remaining part are covering the right side of area D6.Use the log file to confirm the result of comparision in packet EVC-4 for displaying each PA Speed Profile Discontinuities symbol at sub-area D6-D7 and position for each position of PA Speed Profile Discontinuities from the differentiate of variable [MMI_TRACK_DESCRIPTION (EVC-4).MMI_O_MRSP] and [MMI_ETCS_MISC_OUT_SIGNALS (EVC-7).OBU_TR_O_TRAIN] refer as follows,Speed Profile DiscontinuitiesMMI_V_MRSP[0] < MMI_V_MRSP_CURRPosition [MMI_TRACK_DESCRIPTION (EVC-4).MMI_O_MRSP[0]] – [MMI_ETCS_MISC_OUT_SIGNALS (EVC-7).OBU_TR_O_TRAIN] is approximately to 50000 (500m)DMI displays symbol PL22 (a speed decrease) at position 500m. Speed Profile DiscontinuitiesMMI_V_MRSP[0] < MMI_V_MRSP[1]Position[MMI_TRACK_DESCRIPTION (EVC-4).MMI_O_MRSP[1]] – [MMI_ETCS_MISC_OUT_SIGNALS (EVC-7).OBU_TR_O_TRAIN] is approximately to 100000 (1000m)DMI displays symbol PL21 (a speed increase) At position 1000m.Speed Profile DiscontinuitiesMMI_V_MRSP[2] = 0 Position[MMI_TRACK_DESCRIPTION (EVC-4).MMI_O_MRSP[2]] – [MMI_ETCS_MISC_OUT_SIGNALS (EVC-7).OBU_TR_O_TRAIN] is approximately to 200000 (2000m)DMI displays symbol PL23 (a speed decrease to a target at zero speed).There is no PA Speed Profile Discontinuity symbol displayed beyond position 2000m.(4)   The bottom of horizontal line of each symbol are located at specific location in expected result No.2
            Test Step Comment: (1) MMI_gen 9941 (partly: D6-D7 areas); MMI_gen 622;(2) MMI_gen 7293; MMI_gen 641 (partly: 1st bullet, result of calculation, 4th bullet, result of comparision to display PL21 symbol, result of comparision to display PL22 symbol, result of comparision to display PL23 symbol); MMI_gen 9942 (partly: LOA location, symbol PL21, PL22, PL23);(3) MMI_gen 9941 (partly: up to the first target at zero speed);(4) MMI_gen 9941 (partly: bottom of the horizontal line); MMI_gen 7295;
            */


            /*
            Test Step 2
            Action: Drive the train forward
            Expected Result: Verify the following information,While the train is running forward, each symbol of PA Speed Profile Discontinuities are moving down to the zero line
            Test Step Comment: (1) MMI_gen 643 (partly: 1st bullet, changes during train movement, 3rd bullet, move to calculated Y-coordinate);
            */
            // Call generic Action Method
            DmiActions.Drive_the_train_forward(this);


            /*
            Test Step 3
            Action: Continue to drive the train forward
            Expected Result: Verify the following information,The 1st symbol of PA Speed Profile Discontinuity is removed from are D6-D7 after passed the zero line
            Test Step Comment: (1) MMI_gen 643 (partly: 4th bullet, deleted);
            */
            // Call generic Action Method
            DmiActions.Continue_to_drive_the_train_forward(this);


            /*
            Test Step 4
            Action: Drive the train forward and pass BG2
            Expected Result: Verify the following information,(1)   DMI displays symbol PL23 at position 600m and there is no another PA Speed Profile Discontinuity symbol displayed at the position beyond 600m.(2)   A position 200-210m, the 2 symbols of PL22 are drawn. The closest one to the train's current location (0m in distance scale) shall be drawn on top of the other symbols
            Test Step Comment: (1) MMI_gen 9941 (partly: within MA); MMI_gen 9942 (partly: EOA);(2) MMI_gen 1418 (partly: 3rd bullet, drawn on top of the other symbols of the same type, PL22);
            */


            /*
            Test Step 5
            Action: Stop the train at position around 950m
            Expected Result: Verify the following information,(1)   The symbol PL23 is drawn over the nearest symbol PL22 below.Note: Press ‘Scale up’ or ‘Scale down’ button for easier to verify an expected result
            Test Step Comment: (1) MMI_gen 1418 (partly: 1st bullet, PL23 on top of PL22);
            */


            /*
            Test Step 6
            Action: Continue to drive the train forward and pass BG3
            Expected Result: Verify the following information,At position 200-210m, The symbol PL 22 is drawn on top of symbol PL 21.A position 1000-1010m, the 2 symbols of PL21 are drawn. The closest one to the train's current location (0m in distance scale) shall be drawn on top of the other symbols.Note: Press ‘Scale up’ or ‘Scale down’ button for easier to verify an expected result
            Test Step Comment: (1) MMI_gen 1418 (partly: 2nd bullet, PL22 on top of PL21);(2) MMI_gen 1418 (partly: 3rd bullet, drawn on top of the other symbols of the same type, PL21);
            */


            /*
            Test Step 7
            Action: Stop the train
            Expected Result: Verify the following information,(1)   The symbol PL23 is drawn over the nearest symbol PL21 below
            Test Step Comment: (1) MMI_gen 1418 (partly: 1st bullet, PL23 on top of PL22 and PL21);
            */
            // Call generic Action Method
            DmiActions.Stop_the_train(this);


            /*
            Test Step 8
            Action: Simulate the communication loss between ETCS Onboard and DMI
            Expected Result: DMI displays the  message “ATP Down Alarm” with sound.Verify that the PA Speed Profile segments including their Speed Discontinuity Symbols are removed from DMI
            Test Step Comment: (1) MMI_gen 6956 (partly: MMI_gen 244);
            */
            // Call generic Action Method
            DmiActions.Simulate_the_communication_loss_between_ETCS_Onboard_and_DMI(this);


            /*
            Test Step 9
            Action: Re-establish the communication between ETCS onboard and DMI
            Expected Result: The PA Speed Profile segments are reappeared
            Test Step Comment: Note under MMI_gen 6956;
            */
            // Call generic Action Method
            DmiActions.Re_establish_the_communication_between_ETCS_onboard_and_DMI(this);
            // Call generic Check Results Method
            DmiExpectedResults.The_PA_Speed_Profile_segments_are_reappeared(this);


            /*
            Test Step 10
            Action: Deactive the cabin
            Expected Result: DMI is entered “Idle” state and displays the message “Driver’s cab not active”
            */


            /*
            Test Step 11
            Action: Simulate the communication loss between ETCS Onboard and DMI
            Expected Result: DMI displays the  message “No connection to the ATP”Verify that the PA Speed Profile segments including their Speed Discontinuity Symbols are removed from DMI
            Test Step Comment: (1) MMI_gen 6956 (partly: MMI_gen 240);
            */
            // Call generic Action Method
            DmiActions.Simulate_the_communication_loss_between_ETCS_Onboard_and_DMI(this);


            /*
            Test Step 12
            Action: Re-establish the communication between ETCS onboard and DMI
            Expected Result: DMI displays the message “Driver’s cab not active”
            Test Step Comment: Note under MMI_gen 6956;
            */
            // Call generic Action Method
            DmiActions.Re_establish_the_communication_between_ETCS_onboard_and_DMI(this);


            /*
            Test Step 13
            Action: End of test
            Expected Result: 
            */


            return GlobalTestResult;
        }
    }
}
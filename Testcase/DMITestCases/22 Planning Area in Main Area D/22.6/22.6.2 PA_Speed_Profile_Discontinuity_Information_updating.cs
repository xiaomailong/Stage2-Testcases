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
    public class PA_Speed_Profile_Discontinuity_Information_updating : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // Test system is power on.Cabin is activatedSoM is perform in SR mode, Leve 1.

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
            Action: Drive the train forward pass BG1.Then, Stop the train
            Expected Result: DMI changes from SR to FS mode. The Planning Area is displayed
            */
            // Call generic Action Method
            DmiActions.Drive_the_train_forward_pass_BG1_Then_Stop_the_train(this);
            // Call generic Check Results Method
            DmiExpectedResults.DMI_changes_from_SR_to_FS_mode_The_Planning_Area_is_displayed(this);


            /*
            Test Step 2
            Action: Use the test script file  17_6_2_a.xml to send EVC-4 with,MMI_N_MRSP = 0
            Expected Result: Verify the following information, All PA Speed Profile Discontinuities symbol are removed from areas D6-D7
            Test Step Comment: (1) MMI_gen 7290;
            */


            /*
            Test Step 3
            Action: Use the test script file  17_6_2_b.xml to send EVC-4 with,MMI_N_MRSP = 3MMI_V_MRSP_CURR[0] = -1MMI_O_MRSP[0] = 1,000,050,000MMI_V_MRSP_CURR[0] = -2MMI_O_MRSP[0] = 1,000,100,000MMI_V_MRSP_CURR[0] = -3MMI_O_MRSP[0] = 1,000,200,000
            Expected Result: Verify the following information, The symbol PL22 is displayed at position 500m.The symbol PL21 is displayed at position 1000m.There is no symbol display at position 2000m
            Test Step Comment: (1) MMI_gen 2600 (partly: 1st bullet, result of calculation, 3rd bullet, PL22 symbol);(2) MMI_gen 2600 (partly: 1st bullet, result of calculation, 4th bullet, PL21 symbol);(3) MMI_gen 2600 (partly: 1st bullet, result of calculation, 5th bullet, Not place any Speed discontinuity symbol);
            */


            /*
            Test Step 4
            Action: Use the test script file  17_6_2_c.xml to send EVC-4 with,MMI_N_MRSP = 1MMI_V_MRSP_CURR[0] = 11112MMI_O_MRSP[0] = 1,000,050,000
            Expected Result: Verify the following information, An information of PA in area D are not updated
            Test Step Comment: (1) MMI_gen 7291 (partly: 1st bullet, MMI_V_MRSP has an invalid value);
            */
            // Call generic Check Results Method
            DmiExpectedResults.Verify_the_following_information_An_information_of_PA_in_area_D_are_not_updated(this);


            /*
            Test Step 5
            Action: Use the test script file  17_6_2_d.xml to send EVC-4 with,MMI_N_MRSP = 1MMI_V_MRSP_CURR[0] = 11111MMI_O_MRSP[0] = 2,147,483,648
            Expected Result: Verify the following information, An information of PA in area D are not updated
            Test Step Comment: (1) MMI_gen 7291 (partly: 2nd bullet, MMI_O_MRSP has an invalid value);
            */
            // Call generic Check Results Method
            DmiExpectedResults.Verify_the_following_information_An_information_of_PA_in_area_D_are_not_updated(this);


            /*
            Test Step 6
            Action: Use the test script file  17_6_2_e.xml to send EVC-4 with,MMI_N_MRSP = 1MMI_V_MRSP_CURR[0] = 11111MMI_O_MRSP[0] = 0
            Expected Result: Verify the following information, An information of PA in area D are not updated
            Test Step Comment: (1) MMI_gen 7291 (partly: 3rd bullet, value is not positive);
            */
            // Call generic Check Results Method
            DmiExpectedResults.Verify_the_following_information_An_information_of_PA_in_area_D_are_not_updated(this);


            /*
            Test Step 7
            Action: End of test
            Expected Result: 
            */


            return GlobalTestResult;
        }
    }
}
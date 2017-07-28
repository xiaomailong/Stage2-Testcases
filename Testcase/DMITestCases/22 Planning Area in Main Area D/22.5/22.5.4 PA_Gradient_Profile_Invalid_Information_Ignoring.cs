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
    /// 22.5.4 PA Gradient Profile:  Invalid Information Ignoring
    /// TC-ID: 17.5.4
    /// 
    /// This test case verifies an information updating of PA Gradient Profile and Speed Profile refer to information from received packet EVC-4 which will be ignored when the value in packet is invalid.
    /// 
    /// Tested Requirements:
    /// MMI_gen 7260; MMI_gen 7261; MMI_gen 7257 (partly: Special value);
    /// 
    /// Scenario:
    /// Activate cabin A. Perform SoM to SR mode, level 1.Drive the train forward pass BG1 at position 100.BG1 giving pkt12, pkt 21 and pkt27.Q_GDIR = 1, G_A = 1Stop the train.Use the test script file to send packet information EVC-
    /// 4.Then, verify that DMI ignores the invalid EVC-4 packet correctly.Note: Each step of test script file in executed continuously, Tester need to verify expected result within specific time (5 second).
    /// 
    /// Used files:
    /// 17_5_4.tdg, 17_5_4_a.xml, 17_5_4_b.xml, 17_5_4_c.xml, 17_5_4_d.xml, 17_5_4_e.xml
    /// </summary>
    public class PA_Gradient_Profile_Invalid_Information_Ignoring : TestcaseBase
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
            // DMI displays FS mode, level 1.

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint


            /*
            Test Step 1
            Action: Activate cabin A. Then  perform SoM to SR mode, level 1
            Expected Result: DMI displays SR mode, level 1
            */
            // Call generic Action Method
            DmiActions.Activate_cabin_A_Then_perform_SoM_to_SR_mode_level_1();
            // Call generic Check Results Method
            DmiExpectedResults.DMI_displays_SR_mode_level_1();


            /*
            Test Step 2
            Action: Drive the train forward pass BG1.Then, stop the train
            Expected Result: DMI changes from SR to FS mode. The Planning Area is displayed in area D with PA Gradient Profile value = 1 and PA Gradient Profile colour is grey
            */
            // Call generic Action Method
            DmiActions.Drive_the_train_forward_pass_BG1_Then_stop_the_train();


            /*
            Test Step 3
            Action: Use the test script file 17_5_4_a.xml to send EVC-4 with,MMI_V_MRSP_CURR = 11111MMI_N_MRSP = 0MMI_G_GRADIENT_CURR = 254MMI_N_GRADIENT = 0
            Expected Result: Verify that the value of PA Gradient Profile is change to 254
            */


            /*
            Test Step 4
            Action: Send EVC-4 with,MMI_V_MRSP_CURR = 11111MMI_N_MRSP = 0MMI_G_GRADIENT_CURR = 255MMI_N_GRADIENT = 0
            Expected Result: Verify that the value of PA Gradient Profile is not change, still display as 254
            Test Step Comment: MMI_gen 7260 (partly: 4th bullet);
            */


            /*
            Test Step 5
            Action: Send EVC-4 with,MMI_V_MRSP_CURR = 11111MMI_N_MRSP = 0MMI_G_GRADIENT_CURR = -254MMI_N_GRADIENT = 0
            Expected Result: Verify that the value of PA Gradient Profile is change to 254 and PA Gradient Profile colour is change to dark-grey colour
            */


            /*
            Test Step 6
            Action: Send EVC-4 with,MMI_V_MRSP_CURR = 11111MMI_N_MRSP = 0MMI_G_GRADIENT_CURR = -255MMI_N_GRADIENT = 0
            Expected Result: Verify that PA Gradient Profile is removed from area D
            Test Step Comment: MMI_gen 7257 (partly: Special value);      
            */


            /*
            Test Step 7
            Action: Use the test script file 17_5_4_b.xml to send EVC-4 with,MMI_V_MRSP_CURR = 11111MMI_N_MRSP = 0MMI_G_GRADIENT_CURR = 10MMI_N_GRADIENT = 0
            Expected Result: The Planning Area is displayed in area D with PA Gradient Profile value = 10 and PA Gradient Profile colour is grey
            */


            /*
            Test Step 8
            Action: Send EVC-4 with,MMI_V_MRSP_CURR = 11112MMI_N_MRSP = 0MMI_G_GRADIENT_CURR = 20MMI_N_GRADIENT = 0
            Expected Result: Verify that the value of PA Gradient Profile is not change, still display as 10
            Test Step Comment: MMI_gen 7260 (partly: 3rd  bullet);
            */


            /*
            Test Step 9
            Action: Use the test script file 17_5_4_c.xml to send EVC-4 with,MMI_V_MRSP_CURR = 11111MMI_N_MRSP = 0MMI_G_GRADIENT_CURR = 22MMI_N_GRADIENT = 1MMI_G_GRADIENT = 11MMI_O_GRADIENT_2 = 15259MMI_O_GRADIENT_1 = 16176
            Expected Result: The 2 PA Gradient profiles (value = 11 and 22) are displayed in area D5
            */


            /*
            Test Step 10
            Action: Send EVC-4 with,MMI_V_MRSP_CURR = 11111MMI_N_MRSP = 0MMI_G_GRADIENT_CURR = 10MMI_N_GRADIENT = 32MMI_G_GRADIENT = 1MMI_O_GRADIENT_2 = 15259MMI_O_GRADIENT_1 = 16176
            Expected Result: Verify that the value of PA Gradient Profile is not change, still display PA Gradient Profiles value = 11 and 22
            Test Step Comment: MMI_gen 7260 (partly: 1st   bullet);                      
            */
            // Call generic Check Results Method
            DmiExpectedResults
                .Verify_that_the_value_of_PA_Gradient_Profile_is_not_change_still_display_PA_Gradient_Profiles_value_11_and_22();


            /*
            Test Step 11
            Action: Use the test script file 17_5_4_d.xml to send EVC-4 with,MMI_V_MRSP_CURR = 11111MMI_N_MRSP = 0MMI_G_GRADIENT_CURR = 20MMI_N_GRADIENT = 1MMI_G_GRADIENT = 1MMI_O_GRADIENT_2 = 15258MMI_O_GRADIENT_1 = 51712
            Expected Result: Verify that the value of PA Gradient Profile is not change, still display PA Gradient Profiles value = 11 and 22
            Test Step Comment: MMI_gen 7261 (partly: 3rd bullet);
            */
            // Call generic Check Results Method
            DmiExpectedResults
                .Verify_that_the_value_of_PA_Gradient_Profile_is_not_change_still_display_PA_Gradient_Profiles_value_11_and_22();


            /*
            Test Step 12
            Action: Send EVC-4 with,MMI_V_MRSP_CURR = 11111MMI_N_MRSP = 0MMI_G_GRADIENT_CURR = 20MMI_N_GRADIENT = 1MMI_G_GRADIENT = 255MMI_O_GRADIENT_2 = 15259MMI_O_GRADIENT_1 = 16176
            Expected Result: Verify that the value of PA Gradient Profile is not change, still display PA Gradient Profiles value = 11 and 22
            Test Step Comment: MMI_gen 7261 (partly: 1st  bullet);
            */
            // Call generic Check Results Method
            DmiExpectedResults
                .Verify_that_the_value_of_PA_Gradient_Profile_is_not_change_still_display_PA_Gradient_Profiles_value_11_and_22();


            /*
            Test Step 13
            Action: Send EVC-4 with,MMI_V_MRSP_CURR = 11111MMI_N_MRSP = 0MMI_G_GRADIENT_CURR = 20MMI_N_GRADIENT = 1MMI_G_GRADIENT = 2MMI_O_GRADIENT_2 = 32768MMI_O_GRADIENT_1 = 0
            Expected Result: Verify that the value of PA Gradient Profile is not change, still display PA Gradient Profiles value = 11 and 22
            Test Step Comment: MMI_gen 7261 (partly: 2nd   bullet);
            */
            // Call generic Check Results Method
            DmiExpectedResults
                .Verify_that_the_value_of_PA_Gradient_Profile_is_not_change_still_display_PA_Gradient_Profiles_value_11_and_22();


            /*
            Test Step 14
            Action: Use the test script file 17_5_4_e.xml to send EVC-4 with,MMI_V_MRSP_CURR = 11111MMI_N_MRSP = 1MMI_V_MRSP = 3000MMI_O_MRSP_2 = 15259MMI_O_MRSP_1 = 16176MMI_G_GRADIENT_CURR = 20MMI_N_GRADIENT = 0
            Expected Result: Verify that only one PA Gradient value is display as value = 20 with speed profile updated as picture in comment
            */


            /*
            Test Step 15
            Action: Send EVC-4 with,MMI_V_MRSP_CURR = 11111MMI_N_MRSP = 32MMI_V_MRSP = 3000MMI_O_MRSP_2 = 15259MMI_O_MRSP_1 = 16176MMI_G_GRADIENT_CURR = 2MMI_N_GRADIENT = 0
            Expected Result: Verify that PA Gradient Profile and speed profile is not update, result is still same as step 14
            Test Step Comment: MMI_gen 7260 (partly: 2nd   bullet);    
            */


            /*
            Test Step 16
            Action: End of test
            Expected Result: 
            */


            return GlobalTestResult;
        }
    }
}
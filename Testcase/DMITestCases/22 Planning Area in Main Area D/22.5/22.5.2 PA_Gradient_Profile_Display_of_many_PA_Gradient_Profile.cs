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
    /// 22.5.2 PA Gradient Profile:  Display of many PA Gradient Profile
    /// TC-ID: 17.5.2
    /// 
    /// This test case verifies the PA Gradient Profile displays on sub-area D5. The Gradient Profile shall display PA Gradient Profile segments. The condition to display the gradient profiles shall comply with [MMI-ETCS-gen].
    /// 
    /// Tested Requirements:
    /// MMI_gen 7262; MMI_gen 7264 (partly: 2nd – 5th bullet); MMI_gen 7266 (partly: 2nd – 4th bullet);
    /// 
    /// Scenario:
    /// Activate cabin A. Perform SoM to SR mode, level 
    /// 1.Drive the train forward pass BG1 at position 
    /// 100.Then, verify that PA is displayed in area D.BG1 giving pkt12, pkt 21 and pkt
    /// 27.Press <Scale Down> button 3 times. Then, verify that the gradient segments are update accordingly.Press <Scale Up> button 3 times.Continue to drive train forward. Then, verify that the gradient segments are update accordingly.
    /// 
    /// Used files:
    /// 17_5_2.tdg
    /// </summary>
    public class PA_Gradient_Profile_Display_of_many_PA_Gradient_Profile : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // System is power on.The default configuration of PA distance scale is set as [0…4000]

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
            DmiActions.Activate_cabin_A_Then_perform_SoM_to_SR_mode_level_1(this);
            // Call generic Check Results Method
            DmiExpectedResults.DMI_displays_SR_mode_level_1(this);


            /*
            Test Step 2
            Action: Drive the train forward pass BG1.Then, stop the train
            Expected Result: DMI changes from SR to FS mode. The Planning Area is displayed in area D.The order of PA Gradient Profile segments are displayed correctly refer to received EVC-4 packet (see figure in comment),0-250m: Gradient Profile value = 2 (grey colour)251-500m: Gradient Profile value = 5 (grey colour)501-1000m: Gradient Profile value = 20 (grey colour)1001-2000m: Gradient Profile value = 16 (dark-grey colour)2001-4000m: Gradient Profile value = 10 (grey colour)The lower boundary of each PA Gradient are placed in sub-area D5 at following position,0 m250 m500 m1000 m2000 mUse the log file to confirm that DMI receives EVC-4 packet with the following variables,MMI_G_GRADIENT_CURR = 2MMI_N_GRADIENT = 4MMI_G_GRADIENT[0] = 5MMI_G_GRADIENT[1] = 20MMI_G_GRADIENT[2] = -16MMI_G_GRADIENT[3] = 10Note: The first index of parameter is the topmost position in packet EVC-4
            Test Step Comment: (1) MMI_gen 7262 (partly: process all valid entries); (2) MMI_gen 7264 (partly: 2nd  bullet, result of calculation);    MMI_gen 7264 (partly: 3rd bullet, lower boundary); (3) MMI_gen 7262 (partly: reception of packet EVC-4); 
            */
            // Call generic Action Method
            DmiActions.Drive_the_train_forward_pass_BG1_Then_stop_the_train(this);


            /*
            Test Step 3
            Action: Press <Scale Down> button 3 times
            Expected Result: The distance scale is changed to 0-32000 m.The PA Gradient Profiles are displayed within MA (0-8000m), the upper boundary of PA Gradient profile is displayed at the same position of target zero speed (8000m)
            Test Step Comment: (1) MMI_gen 7264 (partly: 5th bullet);
            */


            /*
            Test Step 4
            Action: Press <Scale Up> button 3 times.Then, drive the train forward
            Expected Result: The PA Gradient Profile segment bars are continuously updated.At the lowest PA Gradient Profile, the lower boundary is stuck to the zero line.The PA Gradient Profile bar is become shorter accordingly
            Test Step Comment: (1) MMI_gen 7266 (partly: 4th bullet, lower boundary);          MMI_gen 7266 (partly: 2nd bullet, result of calculation);                      (2) MMI_gen 7266 (partly: 3rd bar become shorter);             MMI_gen 7264 (partly: 4th  bullet, shortened);                   
            */


            /*
            Test Step 5
            Action: Continue to drive the train forward
            Expected Result: The PA Gradient Profile segment bars are continuously updated.Verify the following information,PA Gradient Profile Segment is moved down to the Zero Line then its change to the next value
            Test Step Comment: (1) MMI_gen 7266 (partly: 2nd bullet, result of calculation);
            */
            // Call generic Action Method
            DmiActions.Continue_to_drive_the_train_forward(this);


            /*
            Test Step 6
            Action: End of test
            Expected Result: 
            */


            return GlobalTestResult;
        }
    }
}
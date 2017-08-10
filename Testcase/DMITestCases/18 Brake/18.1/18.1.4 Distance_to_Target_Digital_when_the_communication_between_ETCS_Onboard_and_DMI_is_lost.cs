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
    /// 18.1.4 Distance to Target Digital when the communication between ETCS  Onboard and DMI is lost
    /// TC-ID: 13.1.4
    /// 
    /// This test case verifies  the properties of the distance to target digital when the communication between ETCS Onboard and DMI is lost. 
    /// 
    /// Tested Requirements:
    /// MMI_gen 6878 (partly: Distance to target Digital removal); MMI_gen 6879 (partly: Distance to target Digital removal);
    /// 
    /// Scenario:
    /// Active cabin A. Perform SoM in SR mode, level 
    /// 1.Then drive the train forward.Pass BG1 at 250m: giving pkt 12, pkt 21 and pkt 27Pass BG2 at 600m: V_MAIN=50km/hSimulate the communication loss between ETCS Onboard and DMI. Then re-establish the communication again.
    /// 
    /// Used files:
    /// 13_1_4.tdg
    /// </summary>
    public class Distance_to_Target_Digital_when_the_communication_between_ETCS_Onboard_and_DMI_is_lost : TestcaseBase
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
            // DMI displays in FS mode, level 1.

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint


            /*
            Test Step 1
            Action: Activate cabin A
            Expected Result: DMI displays in SB mode. The Driver ID window is displayed
            */
            // Call generic Action Method
            DmiActions.Activate_Cabin_1(this);
            // Call generic Check Results Method
            DmiExpectedResults.DMI_displays_Driver_ID_window_in_SB_mode(this);


            /*
            Test Step 2
            Action: Driver performs SoM to SR mode, level 1
            Expected Result: DMI displays in SR mode, level 1
            */
            // Call generic Action Method
            DmiActions.Driver_performs_SoM_to_SR_mode_level_1(this);
            // Call generic Check Results Method
            DmiExpectedResults.SR_Mode_displayed(this);


            /*
            Test Step 3
            Action: Drive the train forward passing BG1Then drive the train forward with speed = 60 km/h in FS mode
            Expected Result: DMI changes from SR to FS mode.Verify that the distance to target bar is displayed in sub-area A2.The distance to target digital is displayed as numeric in Metric units
            */


            /*
            Test Step 4
            Action: Drive the train forward passing BG2
            Expected Result: DMI remains displays in FS mode
            */
            // Call generic Action Method
            DmiActions.Drive_train_forward_passing_BG2(this);
            // Call generic Check Results Method
            DmiExpectedResults.DMI_remains_displays_in_FS_mode(this);


            /*
            Test Step 5
            Action: Simulate a communication loss between ETCS Onboard and DMI
            Expected Result: DMI displays the  message “ATP Down Alarm” with sound alarm.Verify that the distance to target digital is removed from DMI’s screen. The toggling function is disabled
            Test Step Comment: MMI_gen 6878 (partly: Distance to target Digital removal);
            */


            /*
            Test Step 6
            Action: Re-establish the communication between ETCS onboard and DMI
            Expected Result: DMI displays in FS mode. Verify that the distance to target digital is resumed
            Test Step Comment: MMI_gen 6879 (partly: Distance to target Digital re-appeared);
            */
            // Call generic Action Method
            DmiActions.Re_establish_communication_EVC_DMI(this);


            /*
            Test Step 7
            Action: End of test
            Expected Result: 
            */


            return GlobalTestResult;
        }
    }
}
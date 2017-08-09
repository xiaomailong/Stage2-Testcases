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
    /// 22.9.10 Hide PA Function with the communication loss between ETCS Onboard and DMI
    /// TC-ID: 17.9.10 (Default Configuration)
    /// 
    /// This test case verifies that the properties of  Hide PA function when the communication between ETCS Onboard and DMI loss. In this state, it shall not affect to the state of the  Hide PA function. The Hide PA function shall comply with conditions of  [MMI-ETCS-gen].
    /// 
    /// Tested Requirements:
    /// MMI_gen 7357; MMI_gen 7358;
    /// 
    /// Scenario:
    /// Active cabin A. Perform start of mission to SR mode, Level 
    /// 1.At 100m ,pass BG1 with pkt12, pkt21 and pkt
    /// 27.Mode changes to FS mode. Then simulate the communication loss between ETCS onboard and DMI is lost. The DMI displays ATP Down Alarm. After that the communication re-establishes again. DMI displays the planning area with enabled Hide PA function 
    /// 
    /// Used files:
    /// 17_9_10.tdg
    /// </summary>
    public class Hide_PA_Function_with_the_communication_loss_between_ETCS_Onboard_and_DMI : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // System is power on

            // Call the TestCaseBase PreExecution
            base.PreExecution();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in FS mode,  

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint


            /*
            Test Step 1
            Action: Activate cabin A. Driver performs SoM to SR mode, level 1
            Expected Result: DMI displays in SR mode, level 1
            */
            // Call generic Action Method
            DmiActions.Activate_cabin_A_Driver_performs_SoM_to_SR_mode_level_1(this);
            // Call generic Check Results Method
            DmiExpectedResults.SR_Mode_displayed(this);


            /*
            Test Step 2
            Action: Drive the train forward with speed = 40 km/h pass BG1
            Expected Result: DMI displays the Planning area The “Entering FS” message is shown. The Hide PA function is enabled and locate at Main area D
            */
            // Call generic Action Method
            DmiActions.Drive_the_train_forward_with_speed_40_kmh_pass_BG1(this);


            /*
            Test Step 3
            Action: Simulate the communication loss between ETCS onboard and DMI
            Expected Result: DMI displays “ATP Down Alarm” message with sound alarm.Verify that the planning area and Hide PA function is disappeared
            Test Step Comment: (1) MMI_gen 7357;
            */
            // Call generic Action Method
            DmiActions.Simulate_communication_loss_EVC_DMI(this);


            /*
            Test Step 4
            Action: Press at Main area D
            Expected Result: Verify the following information,PA is not be resumed to display on DMI
            Test Step Comment: (1) MMI_gen 7357 (partly: sensitive areas, inoperable);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press at Main area D");


            /*
            Test Step 5
            Action: Re-establish the communication between ETCS onboard and DMI
            Expected Result: DMI displays in FS mode, level 1.Verify that DMI displays the planning area and Hide PA button is resumed
            Test Step Comment: (1) MMI_gen 7358;
            */
            // Call generic Action Method
            DmiActions.Re_establish_communication_EVC_DMI(this);


            /*
            Test Step 6
            Action: Press Hide PA button
            Expected Result: The Planning Information is disappeared from main area D
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press Hide PA button");
            // Call generic Check Results Method
            DmiExpectedResults.The_Planning_Information_is_disappeared_from_main_area_D(this);


            /*
            Test Step 7
            Action: Press at Main area D
            Expected Result: Verify the following information,PA is resumed to display at main area D
            Test Step Comment: (1) MMI_gen 7358 (partly: sensitive areas, operable);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press at Main area D");


            /*
            Test Step 8
            Action: Press Hide PA button
            Expected Result: The Planning Information is disappeared from main area D
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press Hide PA button");
            // Call generic Check Results Method
            DmiExpectedResults.The_Planning_Information_is_disappeared_from_main_area_D(this);


            /*
            Test Step 9
            Action: Simulate the communication loss between ETCS onboard and DMI
            Expected Result: DMI displays “ATP Down Alarm” message with sound alarm and Hide PA function is disappeared
            */
            // Call generic Action Method
            DmiActions.Simulate_communication_loss_EVC_DMI(this);


            /*
            Test Step 10
            Action: Re-establish the communication between ETCS onboard and DMI
            Expected Result: Verify the following information,Verify that DMI is not displays the planning area and Hide PA button because state of Hide PA is no affected
            Test Step Comment: (1) MMI_gen 7358 (partly: state of Hide PA);
            */
            // Call generic Action Method
            DmiActions.Re_establish_communication_EVC_DMI(this);


            /*
            Test Step 11
            Action: End of test
            Expected Result: 
            */


            return GlobalTestResult;
        }
    }
}
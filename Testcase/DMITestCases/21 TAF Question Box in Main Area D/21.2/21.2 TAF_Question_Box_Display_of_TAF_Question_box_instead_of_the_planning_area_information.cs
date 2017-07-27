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
    /// 21.2 TAF Question Box: Display of TAF Question box instead of the planning area information
    /// TC-ID: 16.2
    /// 
    /// This test case verifies that planning area information is forced into the backgound while TAF Question box is display.
    /// 
    /// Tested Requirements:
    /// MMI_gen 7096; 
    /// 
    /// Scenario:
    /// Perform SoM to SR mode, level 
    /// 2.Then, verify that PA, Scale up button and Scale down button are not displayed .Drive the train forward to receive information from RBC at 70m.Message 3: Packet 15,21,27 and 80 (Entering FS and get OS mode acknowledgement area)Continue to drive the train forward and acknowledge OS mode at position 250m.Drive the train forward to receive Track ahead free request from RBC at position 350m. Then, verify that Planning area with specified buttons are removed from area D.
    /// 
    /// Used files:
    /// 16_2.tdg, 16_2.utt
    /// </summary>
    public class TAF_Question_Box_Display_of_TAF_Question_box_instead_of_the_planning_area_information : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // Set the following tags name in configuration file (See the instruction in Appendix 1)HIDE_PA_OS_MODE = 1 (PA will show in OS mode)System is power on.Cabin is activate.
            
            // Call the TestCaseBase PreExecution
            base.PreExecution();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in OS mode, level 2.

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint

            
            /*
            Test Step 1
            Action: Perform SoM to SR mode, level 2.Then, drive the train forward with speed = 30km/h
            Expected Result: DMI displays in SR mode, level 2
            */
            // Call generic Action Method
            DmiActions.Perform_SoM_to_SR_mode_level_2_Then_drive_the_train_forward_with_speed_30kmh();
            // Call generic Check Results Method
            DmiExpectedResults.DMI_displays_in_SR_mode_level_2();
            
            
            /*
            Test Step 2
            Action: Received information from RBC
            Expected Result: DMI changes from SR mode to FS mode, level 2
            */
            // Call generic Action Method
            DmiActions.Received_information_from_RBC();
            // Call generic Check Results Method
            DmiExpectedResults.DMI_changes_from_SR_mode_to_FS_mode_level_2();
            
            
            /*
            Test Step 3
            Action: Acknowledge OS mode by press at area C1
            Expected Result: DMI changes from FS mode to OS mode, level 2
            */
            // Call generic Action Method
            DmiActions.Acknowledge_OS_mode_by_press_at_area_C1();
            // Call generic Check Results Method
            DmiExpectedResults.DMI_changes_from_FS_mode_to_OS_mode_level_2();
            
            
            /*
            Test Step 4
            Action: Received information from RBC.Then, stop the train
            Expected Result: Verify the following information,TAF Question box is displayed in area D and force PA information into background.The area D is displayed only TAF Question box.The following buttons are removed from area D,Scale Up button (sub-area D9)Scale Down button (sub-area D12).Hide button (sub-area D14)
            Test Step Comment: (1) MMI_gen 7096 (partly: Placed in Main-Area D);(2) MMI_gen 7096 (partly: 1st bullet);(3) MMI_gen 7096 (partly: 3rd bullet, 4th bullet);
            */
            // Call generic Action Method
            DmiActions.Received_information_from_RBC_Then_stop_the_train();
            
            
            /*
            Test Step 5
            Action: Press at any location in area D (except ‘Yes’ button in TAF Question box)
            Expected Result: Verify the following information,PA information is not displayed even pressed in any point of area D
            Test Step Comment: (1) MMI_gen 7096 (partly: 2nd bullet);
            */
            
            
            /*
            Test Step 6
            Action: End of test
            Expected Result: 
            */
            

            return GlobalTestResult;
        }
    }
}

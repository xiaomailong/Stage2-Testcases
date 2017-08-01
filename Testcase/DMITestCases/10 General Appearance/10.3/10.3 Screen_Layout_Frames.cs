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
    /// 10.3 Screen Layout: Frames
    /// TC-ID: 5.3
    /// 
    /// This test case verifies the appearance of all ETCS objects on DMI that related to the control information from ETCS Onboard i.e. display of mode symbols, supervision status or the release speed. 
    /// 
    /// Tested Requirements:
    /// MMI_gen 4222 (partly: frame is displayed with yellow); MMI_gen 6468 (partly: UN mode, supervision status and the release speed are not displayed); MMI_gen 11470 (partly: Bit #2);   
    /// 
    /// Scenario:
    /// Activate cabin A and enter the Driver ID.
    /// 1.Driver selects and confirms level 
    /// 0.DMI displays in UN mode.
    /// 2.Start driving the train forward with train speed = 15 km/h. Then, stop the train at position 100m.
    /// 3.Select level and confirm level 
    /// 14.Driver acknowledges the train trip, after that the driver presses the start button.
    /// 5.DMI changes to SR mode.
    /// 
    /// Used files:
    /// N/A
    /// </summary>
    public class Screen_Layout_Frames : TestcaseBase
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
            // DMI displays in SR mode, level 1

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint


            /*
            Test Step 1
            Action: Activate cabin A
            Expected Result: DMI displays the default window. The Driver ID window is displayed
            */
            // Call generic Action Method
            DmiActions.Activate_cabin_A(this);
            // Call generic Check Results Method
            DmiExpectedResults.DMI_displays_the_default_window_The_Driver_ID_window_is_displayed(this);


            /*
            Test Step 2
            Action: Enter the Driver ID. Perform brake test and then select Level 0
            Expected Result: ATP enters level 0.DMI displays the symbol of Level 0 in sub-area C8
            */


            /*
            Test Step 3
            Action: Select ‘Train data’ button
            Expected Result: The Train data window is displayed
            */
            // Call generic Check Results Method
            DmiExpectedResults.The_Train_data_window_is_displayed(this);


            /*
            Test Step 4
            Action: Enter and confirm the train data
            Expected Result: The Train data validation window is displayed
            */


            /*
            Test Step 5
            Action: Driver validates the train data
            Expected Result: DMI displays the Train running window
            */


            /*
            Test Step 6
            Action: Enter and confirm the Train running number
            Expected Result: DMI displays the Main window
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(@"Enter and confirm the Train running number");
            // Call generic Check Results Method
            DmiExpectedResults.DMI_displays_the_Main_window(this);


            /*
            Test Step 7
            Action: Press ‘Start’ button and confirm UN mode
            Expected Result: DMI displays in UN mode, level 0
            */


            /*
            Test Step 8
            Action: Drive the train forward and observe all objects on DMI’s screen
            Expected Result: Verify that when DMI displays in UN mode, the supervision status is not presented to the driver and there is no release speed on DMI
            Test Step Comment: MMI_gen 6468 (partly: UN mode, supervision status and the release speed are not displayed);   
            */


            /*
            Test Step 9
            Action: Stop at position 100m. Then, select level 1
            Expected Result: DMI displays the symbol of level 1 in sub-area C8 instead of level 0.DMI displays in level 1 with train trip announcement symbol which requires the driver’s action. The train trip symbol is displayed with yellow flashing frame
            Test Step Comment: MMI_gen 4222 (partly: frame is displayed with yellow flashing);
            */


            /*
            Test Step 10
            Action: Driver acknowledges train trip
            Expected Result: DMI displays in PT mode.Use the log file to confirm that DMI sends out packet [MMI_DRIVER_ACTION (EVC-152)] with the value of variable MMI_M_DRIVER_ACTION refer to sequence below,a)   MMI_M_DRIVER_ACTION = 2 (Ack of Train Trip)
            Test Step Comment: MMI_gen 11470 (partly: Bit #2);   
            */


            /*
            Test Step 11
            Action: Press ‘Start’ button and confirm SR mode
            Expected Result: DMI displays in SR mode, level 1
            */
            // Call generic Check Results Method
            DmiExpectedResults.DMI_displays_in_SR_mode_level_1(this);


            /*
            Test Step 12
            Action: End of test
            Expected Result: 
            */


            return GlobalTestResult;
        }
    }
}
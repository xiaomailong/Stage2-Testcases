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
    /// 6.11 Response Time with Up-Type Button
    /// TC-ID: 1.11
    /// 
    /// This test case verifies that response time of DMI starting from up-type button is released until related message is available to the GPP is not exceeded 130 ms.
    /// 
    /// Tested Requirements:
    /// MMI_gen 65;
    /// 
    /// Scenario:
    /// 1.Perform SoM to Level 1 in SR mode.
    /// 2.Verify response time of DMI on actuation of ‘Train data’ button in ‘Main’ window.
    /// 3.Verify response time of DMI on actuation of ‘Start’ button in ‘Main’ window.
    /// 4.Press ‘Data view’ button in sub area F3 and then press ‘Close’ button in ‘Data view’ window.
    /// 5.Press SE04 symbol in sub area F5 and then press ‘System Version’ button in ‘Settings’ window.
    /// 
    /// Used files:
    /// N/A
    /// </summary>
    public class Response_Time_with_Up_Type_Button : TestcaseBase
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
            // DMI displays in SR mode, Level 1.

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint


            /*
            Test Step 1
            Action: Perform SoM to Level 1 in SR mode
            Expected Result: ETCS OB enters SR mode in Level 1
            */
            // Call generic Action Method
            DmiActions.Perform_SoM_to_Level_1_in_SR_mode();
            // Call generic Check Results Method
            DmiExpectedResults.ETCS_OB_enters_SR_mode_in_Level_1();


            /*
            Test Step 2
            Action: Verify response time of DMI on actuation of ‘Train data’ button in ‘Main’ window
            Expected Result: Use log file to verify that response time of DMI on actuation of the ‘Train data’ buttons is not exceeded 130 ms when DMI sends EVC-101 with verification below:-- In packet EVC-101 that variable [MMI_M_REQUEST = 3] and [MMI_Q_BUTTON = 0], the different time between time when ODL log receives this packet and timestamp in variable ‘MMI_T_BUTTONEVENT’ is not exceeded 130 ms. Note Use epoch & unix timestamp converter to convert timestamp recorded in variable ‘MMI_T_BUTTONEVENT’
            Test Step Comment: MMI_gen 65 (partly: ‘Train data’ button);Note All buttons of a menu window (TS) shall be up-type buttons, refers to MMI_gen 4557
            */


            /*
            Test Step 3
            Action: Verify response time of DMI on actuation of ‘Start’ button in ‘Main’ window
            Expected Result: Use log file to verify that response time of DMI on actuation of the ‘Start’ buttons is not exceeded 130 ms when DMI sends EVC-101 with verification below:-- In packet EVC-101 that variable [MMI_M_REQUEST = 9] and [MMI_Q_BUTTON = 0], the different time between time when ODL log receives this packet and timestamp in variable ‘MMI_T_BUTTONEVENT’ is not exceeded 130 ms. Note Use epoch & unix timestamp converter to convert timestamp recorded in variable ‘MMI_T_BUTTONEVENT’
            Test Step Comment: MMI_gen 65 (partly: ‘Start’ button);
            */


            /*
            Test Step 4
            Action: Press ‘Data view’ button in sub area F3 and then press ‘Close’ button in ‘Data view’ window
            Expected Result: Use log file to verify that response time of DMI on actuation of the ‘Data view’ buttons is not exceeded 130 ms when DMI sends EVC-101 with verification below:-- In packet EVC-101 that variable [MMI_M_REQUEST = 21] and [MMI_Q_BUTTON = 0], the different time between time when ODL log receives this packet and timestamp in variable ‘MMI_T_BUTTONEVENT’ is not exceeded 130 ms. Note Use epoch & unix timestamp converter to convert timestamp recorded in variable ‘MMI_T_BUTTONEVENT’
            Test Step Comment: MMI_gen 65 (partly: ‘Data view’ button);
            */


            /*
            Test Step 5
            Action: Press SE04 symbol in sub area F5. Then press ‘System Version’ button in ‘Settings’ window
            Expected Result: Use log file to verify that response time of DMI on actuation of the ‘System Version’ buttons is not exceeded 130 ms when DMI sends EVC-101 with verification below:-- In packet EVC-101 that variable [MMI_M_REQUEST = 55] and [MMI_Q_BUTTON = 0], the different time between time when ODL log receives this packet and timestamp in variable ‘MMI_T_BUTTONEVENT’ is not exceeded 130 ms. Note Use epoch & unix timestamp converter to convert timestamp recorded in variable ‘MMI_T_BUTTONEVENT’
            Test Step Comment: MMI_gen 65 (partly: ‘System Version’ button);
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
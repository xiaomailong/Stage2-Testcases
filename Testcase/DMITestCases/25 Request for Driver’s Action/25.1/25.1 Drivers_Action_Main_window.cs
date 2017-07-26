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
    /// 25.1 Driver’s Action: Main window
    /// TC-ID: 20.1
    /// 
    /// This test case verifes that DMI sends values of [MMI_DRIVER_REQUEST (EVC-101).MMI_M_REQUEST] correctly when a driver presses on each button in Main window.
    /// 
    /// Tested Requirements:
    /// MMI_gen 151 (partly: MMI_M_REQUEST = 40, 20, 34, 3, 4, 27, 32, 30, 31, 1, 2, 5, 9, 14); MMI_gen 1088 (partly: Bit #6); MMI_gen 11470 (partly: Bit #1,7,11,12,15, 17,19,20,21 and 35);
    /// 
    /// Scenario:
    /// 1.Perform the specified action (e.g. open/close window, press an acknowledgement). Then, verify the value in packet EVC-101 refer to each action.
    /// 2.Perform SoM in SR mode, Level 
    /// 1.Then, verify the value in packet EVC-101 when start button is pressed.
    /// 3.Entering SH mode, Level 
    /// 1.Then, verify the value in packet EVC-101 when Shunting button is pressed.
    /// 4.Perform action to Maintain Shunting. Then, verify the value in packet EVC-101 when Maintain Shunting button is pressed.
    /// 5.Exit SH mode. Then, verify the value in packet EVC-101 when Exit Shunting button is pressed.
    /// 6.Simulate non-leading signal. Then, verify the value in packet EVC-101 when Non-leading button is pressed.
    /// 
    /// Used files:
    /// N/A
    /// </summary>
    public class Drivers_Action_Main_window : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // Test system is powered on.Cabin is activated.Driver ID is entered and the brake test is performed.

            // Call the TestCaseBase PreExecution
            base.PreExecution();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in NL mode, Level 1

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint


            /*
            Test Step 1
            Action: Perform the following procedure,Select and confirm Level 1.Press the 'Driver ID' button.Press 'Close' button. Press the 'Train data' button.At the Train data window, press 'Close' button.Press 'Level' button.At the Level window, press 'Close' button.Press 'Train running number' button.At the Train running number window, press 'Close' button.
            Expected Result: Verify the following information(1)   Use the log file to confirm that DMI sends out packet [MMI_DRIVER_REQUEST (EVC-101)] with the value of variable MMI_M_REQUEST refer to sequence below,a)   MMI_M_REQUEST = 40 (Level entered)b)   MMI_M_REQUEST = 20 (Change Driver Identity)c)   MMI_M_REQUEST = 34 (Exit Driver ID Data entry)d)   MMI_M_REQUEST = 3 (Start Train Data Entry)e)   MMI_M_REQUEST = 4 (Exit Train Data Entry)f)   MMI_M_REQUEST = 27 (Change Level)g)   MMI_M_REQUEST = 32 (Exit Change Level)h)   MMI_M_REQUEST = 30 (Change Train Running Number)i)   MMI_M_REQUEST = 31 (Exit Change Train Running Number)Note: The sequence of MMI_M_REQUEST value are consistent with step of each action.(2)   When the button is pressed in each action, the window of pressed button is closed.(3)   Use the log file to confirm that DMI sends out packet [MMI_DRIVER_ACTION (EVC-152)] with the value of variable MMI_M_DRIVER_ACTION refer to sequence below,a)   MMI_M_DRIVER_ACTION = 7 (ACK Level 1)b)   MMI_M_DRIVER_ACTION = 35 (Level 1 selected)
            Test Step Comment: (1) MMI_gen 151 (partly: MMI_M_REQUEST = 40, 20, 34, 3, 4, 27, 32, 30, 31);(2) MMI_gen 151 (partly: close opened menu);(3) MMI_gen 11470 (partly: Bit # 7,35);
            */

            /*
            Test Step 2
            Action: Perform the following procedure,Press the 'Train data' button.Enter and validate all train data.Enter the train running number.Press the 'Start' button.
            Expected Result: Verify the following information,(1)   Use the log file to confirm that DMI sends out packet [MMI_DRIVER_REQUEST (EVC-101)] with variable MMI_M_REQUEST = 9 (Start)(2)   The Main window is closed, DMI displays Default window.(3)   Use the log file to confirm that DMI sends out packet [MMI_DRIVER_ACTION (EVC-152)] with the value of variable MMI_M_DRIVER_ACTION refer to sequence below,a)   MMI_M_DRIVER_ACTION = 19 (Start selected)b)   MMI_M_DRIVER_ACTION = 20 (Train Data Entry requested)c)   MMI_M_DRIVER_ACTION = 21 (Validation of train data)
            Test Step Comment: (1) MMI_gen 151 (partly: MMI_M_REQUEST = 9);(2) MMI_gen 151 (partly: close opened menu);(3) MMI_gen 11470 (partly: Bit # 19, 20, 21);
            */

            /*
            Test Step 3
            Action: Perform the following procedure,Press and hold sub-area C1 at least 2 seconds.Release the pressed area.
            Expected Result: DMI displays in SR mode, Level 1.
            */

            /*
            Test Step 4
            Action: Perform the following procedure,Press the 'Main' button.Press and hold 'Shunting' button at least 2 seconds.Released the pressed button.
            Expected Result: DMI displays Default window in SH mode, Level 1.Verify the following information,(1)    Use the log file to confirm that DMI sends out packet [MMI_DRIVER_REQUEST (EVC-101)] with variable MMI_M_REQUEST = 1 (Start Shunting)(2)   The Main window is closed, DMI displays Default window.(3)   Use the log file to confirm that DMI sends out packet [MMI_DRIVER_ACTION (EVC-152)] with the value of variable MMI_M_DRIVER_ACTION refer to sequence below,a)   MMI_M_DRIVER_ACTION = 1 (ACK of shunting mode)b)   MMI_M_DRIVER_ACTION = 11 (Shunting selected)
            Test Step Comment: (1) MMI_gen 151 (partly: MMI_M_REQUEST = 1);(2) MMI_gen 151 (partly: close opened menu);(3) MMI_gen 11470 (partly: Bit # 1,and 11);
            */

            /*
            Test Step 5
            Action: Perform the following procedure,Press the 'Main' button.Simulate the ‘Passive-Shunting’ signal by activating the ‘Passive-Shunting’ checkbox on OTE.Press and hold 'Maintain Shunting' button at least 2 seconds.Released the pressed button.
            Expected Result: DMI displays Default window in SH mode, Level 1.Verify the following information,(1)    Use the log file to confirm that DMI sends out packet [MMI_DRIVER_REQUEST (EVC-101)] with variable MMI_M_REQUEST = 14 (Continue shunting on desk closure)(2)   The Main window is closed, DMI displays Default window.(3)   Use the log file to confirm that DMI sends out packet [MMI_DRIVER_ACTION (EVC-152)] with the value of variable MMI_M_DRIVER_ACTION refer to sequence below,a)     MMI_M_DRIVER_ACTION = 15 (Continue Shunting on desk closure selected)
            Test Step Comment: (1) MMI_gen 151 (partly: MMI_M_REQUEST = 14);(2) MMI_gen 151 (partly: close opened menu);(3) MMI_gen 11470 (partly: Bit # 15);
            */

            /*
            Test Step 6
            Action: Perform action follow step 4 for the ‘Exit Shunting’ button.
            Expected Result: DMI displays Default window in SH mode, Level 1.Verify the following information,(1)    Use the log file to confirm that DMI sends out packet [MMI_DRIVER_REQUEST (EVC-101)] with variable MMI_M_REQUEST = 2 (Exit Shunting)(2)   The Main window is closed, DMI displays Default window.(3)    Use the log file to verify that DMI receives EVC-30 with bit No.6 of variable: MMI_Q_REQUEST_ENABLE_64 = 1 (Exit shunting).(3)   Use the log file to confirm that DMI sends out packet [MMI_DRIVER_ACTION (EVC-152)] with the value of variable MMI_M_DRIVER_ACTION refer to sequence below,a)    MMI_M_DRIVER_ACTION = 17 (Exit of Shunting selected)
            Test Step Comment: (1) MMI_gen 151 (partly: MMI_M_REQUEST = 2);(2) MMI_gen 151 (partly: close opened menu);(3) MMI_gen 1088 (partly: Bit #6);(4) MMI_gen 11470 (partly: Bit # 17);
            */

            /*
            Test Step 7
            Action: Perform the following procedure,Enter Driver IDSelect and confirm Level 1. Note: If Level window is display.
            Expected Result: DMI displays Main window in SB mode, Level 1.
            */

            /*
            Test Step 8
            Action: Simulate the ‘Non-leading’ signal by activating the ‘Non-leading’ checkbox on OTE.
            Expected Result: The state of ‘Non-leading’ button is changed to enabled.
            */

            /*
            Test Step 9
            Action: Perform action follow step 4 for the ‘Non-leading’ button.
            Expected Result: DMI displays Default window in NL mode, Level 1.Verify the following information,(1)    Use the log file to confirm that DMI sends out packet [MMI_DRIVER_REQUEST (EVC-101)] with variable MMI_M_REQUEST = 5 (Start Non-leading)(2)   Use the log file to confirm that DMI sends out packet [MMI_DRIVER_ACTION (EVC-152)] with the value of variable MMI_M_DRIVER_ACTION refer to sequence below,a)     MMI_M_DRIVER_ACTION = 12 (Non Leading selected) 
            Test Step Comment: (1) MMI_gen 151 (partly: MMI_M_REQUEST = 5);(2) MMI_gen 11470 (partly: Bit # 12);
            */

            /*
            Test Step 10
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
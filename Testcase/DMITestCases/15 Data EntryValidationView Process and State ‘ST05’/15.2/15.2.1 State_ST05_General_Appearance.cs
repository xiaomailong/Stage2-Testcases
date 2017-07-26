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
    /// 15.2.1 State 'ST05': General Appearance
    /// TC-ID: 10.2
    /// 
    /// This test case verifies the visualization of ST05 symbol when DMI received packet EVC-8.
    /// 
    /// Tested Requirements:
    /// MMI_gen 12018; MMI_gen 168 (partly: menu window ‘Main’, data-entry window ‘Train data’, data-validation window ‘Train data’, data-view window); MMI_gen 5728 (partly: menu window ‘Main’, data-entry window ‘Train data’, data-validation window ‘Train data’, data-view window, button ‘Close’, ‘Start’, ‘Driver ID’, Train data’, ‘Level’, Train running number’ and ‘Shunting’), Note (partly: navigation buttons and buttons of numeric/dedicated keypad in the ‘Train data’ data entry and validation windows) under MMI_gen 5728; MMI_gen 5731 (partly: window ‘Data view’, closure, to window ‘Default’, on expiration of 45 seconds); MMI_gen 5732 (partly: window ‘Main’, communication lost, closure, to window ‘Default’); MMI_gen 8355 (partly: vertically centered, move to the right every second, no more possible to display, EVC-8); MMI_gen 5464 (partly: state ST05, 4th bullet); MMI_gen 4395 (partly: close button, disabled); MMI_gen 4396 (partly: close button, NA11, NA12); MMI_gen 1088 (partly: Bit #1 to #9 and #28, #32);
    /// 
    /// Scenario:
    /// The ‘Main’ menu window is displayed.Use the test script files to send packets in order to verify state ‘ST05’ in a menu window.Open the ‘Train data’ window and accept all values.Use the test script files to send packets in order to verify state ‘ST05’ in a data entry window.Accept and confirm the train data again.Use the test script files to send packets in order to verify state ‘ST05’ in a data validation window.Validate the train data and complete entering the train running number. The ‘Main’ menu window is displayed.Use the test script files to send packets in order to verify state ‘ST05’ when the communication is lost.Open the data view window.Use the test script files to send packets in order to verify state ‘ST05’ in a data view window.Use the test script files to send packets in order to verify state ‘ST05’ when the state stays longer than 45 seconds. Open Data view window.
    /// 
    /// Used files:
    /// 10_2_a.xml, 10_2_b.xml
    /// </summary>
    public class State_ST05_General_Appearance : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // Test system is powered on Activate Cabin AEnter the Driver ID and perform brake testSelect and confirm Level 1.

            // Call the TestCaseBase PreExecution
            base.PreExecution();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in SB mode, level 1.

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint


            /*
            Test Step 1
            Action: Perform the following procedure,Activate Cabin A.Enter Driver ID and perform break test.Select and confirm Level 1.
            Expected Result: DMI displays Main window.Verify the following information,Use the log file to confirm that DMI receives packet EVC-30 with the value of following bit in variable MMI_Q_REQUEST_ENABLE_64,Bit #0 = 0 (Start)Bit #1 = 1 (Driver ID)Bit #2 = 1 (Train Data)Bit #3 = 1 (Level )Bit #4 = 1 (Train running number)Bit #5 = 1 (Shunting)Bit #7 = 0 (Non-Leading)Bit #8 = 0 (Maintain Shunting)Bit #9 = 0 (EOA)Bit #28 =1 (Start Brake Test)Bit #32 = 1 (System info)And displays the buttons which have the bit value is 1.
            Test Step Comment: (1) MMI_gen 5728 (partly: ‘Main’ window, menu window, EVC-30, before ST05 state); MMI_gen 1088 (partly: Bit #1 to #9 and #28, #32);
            */

            /*
            Test Step 2
            Action: Use the test script file 10_2_a.xml to send EVC-8 withMMI_Q_TEXT_CRITERIA = 3 MMI_Q_TEXT = 716Note: Stopwatch is required for accuracy of test result.
            Expected Result: Verify the following information,The hourglass symbol ST05 is displayed at window title area.The hourglass symbol ST05 is vertically aligned center of the window title area.All buttons and the ‘Close’ button are disabled.The disabled Close button NA12 is display in area G.The symbol ST05 is move to the right every second.After symbol ST05 is moved to the end of the window title area, the symbol comes back to the first position and keeps moving to the right.
            Test Step Comment: (1) MMI_gen 12018, MMI_gen 8355 (partly: EVC-8);(2) MMI_gen 8355 (partly: vertically centered)(3) MMI_gen 168 (partly: disabled buttons, ‘Main’ window, menu window); MMI_gen 5464 (partly: state ST05, 4th bullet); MMI_gen 4395 (partly: close button, disabled); (4) MMI_gen 4396 (partly: close, NA12);(5) MMI_gen 8355 (partly: Move to the right every second);(6) MMI_gen 8355 (partly: no more possible to display);
            */

            /*
            Test Step 3
            Action: Use the test script file 10_2_b.xml to send EVC-8 withMMI_Q_TEXT_CRITERIA = 4MMI_Q_TEXT = 716
            Expected Result: Verify the following information,The hourglass symbol ST05 is removed.The state of all buttons is restored according to the last received EVC-30, see Step 3:Button Start is disabledButton Driver ID is enabledButton Train Data is enabledButton Level is enabledButton Train running number is enabledButton Shunting is enabledButton Non-Leading is disabledButton Maintain Shunting is disabledThe enabled Close button NA11 is display in area G.
            Test Step Comment: (1) MMI_gen 5728 (partly: removal, EVC);(2) MMI_gen 5728 (partly: restore (after ST05), ‘Main’ window, menu window);(3) MMI_gen 4396 (partly: close, NA11);
            */

            /*
            Test Step 4
            Action: Press ‘Train data’ button.
            Expected Result: DMI displays Train data window.
            */

            /*
            Test Step 5
            Action: Confirm the value of input field refer to specified type of Train data window below,Fixed Train data window: Confirm the value of Train typeFlexible Train data window: Confirm the value of Train length.
            Expected Result: Verify the following information,The ‘Selected’ state (medium-grey background with black text) of the input fields are changed to the ‘Accepted’ state (dark-grey background with white text).The buttons are stated as follows:The keypad is enabled.The ‘Close’ button is enabled.The ‘Yes’ button is enabled.
            Test Step Comment: (1) MMI_gen 5728 (partly: ‘Train data’ window, data entry window, before ST05 state)(2) MMI_gen 5728 (partly: ‘Train data’ window, data entry window, before ST05 state, state of buttons)
            */

            /*
            Test Step 6
            Action: Use the test script file 10_2_a.xml to send EVC-8 withMMI_Q_TEXT_CRITERIA = 3 MMI_Q_TEXT = 716
            Expected Result: Verify the following information,The hourglass symbol ST05 is displayed.The buttons are disabled as follows:The ‘Close’ button is disabled.The ‘Yes’ button is disabled.The keypad is still enabled.All input field is in the ‘Not Selected’ state (dark-grey background with grey text).
            Test Step Comment: (1) MMI_gen 12018(2) MMI_gen 168 (partly: disabled button, ‘Train data’ window, data entry window);(3) Note under MMI_gen 5728;(4) MMI_gen 168 (partly: deselect input field);
            */

            /*
            Test Step 7
            Action: Use the test script file 10_2_b.xml to send EVC-8 withMMI_Q_TEXT_CRITERIA = 4MMI_Q_TEXT = 716
            Expected Result: Verify the following information,The hourglass symbol ST05 is removed.The button state is resumed as follows:The ‘Close’ button is enabled.The ‘Yes’ button is enabled.The input field is stated as follows:The first input field is in the ‘Selected’ state.The rests are in the ‘Not selected’ state
            Test Step Comment: (1) MMI_gen 5728 (partly: removal, EVC);(2) MMI_gen 5728 (partly: restore (after ST05), ‘Train data’ window, data entry window);(3) MMI_gen 5728 (partly: MMI_gen 5211, MMI_gen 4683 (partly: data entry))
            */

            /*
            Test Step 8
            Action: Perform the following procedure,Confirm all value in Train data window.Press ‘Yes’ button.Press ‘Yes’ button (on keypad).
            Expected Result: DMI displays Train data validation window with “Yes” in the input field.Verify the following information,The input field is in ‘Selected’ state (medium-grey background with black text). The buttons are stated as follows:The keypad is enabled.The ‘Close’ button is enabled.
            Test Step Comment: (1) MMI_gen 5728 (partly: ‘Train data’ validation window, data validation window, before ST05 state)(2) MMI_gen 5728 (partly: ‘Train data’ validation window, data validation window, before ST05 state, state of buttons)
            */

            /*
            Test Step 9
            Action: Use the test script file 10_2_a.xml to send EVC-8 withMMI_Q_TEXT_CRITERIA = 3 MMI_Q_TEXT = 716
            Expected Result: Verifies the following points,The hourglass symbol ST05 is displayed.The ‘Close’ button is disabled.The keypad is still enabled.The input field is in the ‘Not Selected’ state (dark-grey background with grey text).
            Test Step Comment: (1) MMI_gen 12018;(2) MMI_gen 168 (partly: disabled button, ‘Train data’ validation window, data validation window);(3) Note under MMI_gen 5728; MMI_gen 5719 (partly: exception, state ST05);(4) MMI_gen 168 (partly: deselect input field);
            */

            /*
            Test Step 10
            Action: Use the test script file 10_2_b.xml to send EVC-8 withMMI_Q_TEXT_CRITERIA = 4MMI_Q_TEXT = 716
            Expected Result: Verifies the following points,The hourglass symbol ST05 is removed.The ‘Close’ button is enabled.The input field is in the ‘Selected’ state.
            Test Step Comment: (1) MMI_gen 5728 (partly: removal, EVC);(2) MMI_gen 5728 (partly: restore (after ST05), ‘Train data’ validation window, data validation window);(3) MMI_gen 5728 (partly: MMI_gen 5211, MMI_gen 4683 (partly: data validation))
            */

            /*
            Test Step 11
            Action: Confirm entered data by pressing an input field.
            Expected Result: DMI displays Train Running Number window.
            */

            /*
            Test Step 12
            Action: Enter and confirm Train running number.
            Expected Result: DMI displays Main window.Verify the following information,Use the log file to confirm that DMI receives packet EVC-30 with the value of following bit in variable MMI_Q_REQUEST_ENABLE_64,Bit #0 = 1 (Start)Bit #1 = 1 (Driver ID)Bit #2 = 1 (Train Data)Bit #3 = 1 (Level )Bit #4 = 1 (Train running number)Bit #5 = 1 (Shunting)Bit #7 = 0 (Non-Leading)Bit #8 = 0 (Maintain Shunting)And displays the buttons which have the bit value is 1.
            Test Step Comment: (1) MMI_gen 5728 (partly: default window, EVC-30, before ST05 state)
            */

            /*
            Test Step 13
            Action: Use the test script file 10_2_a.xml to send EVC-8 withMMI_Q_TEXT_CRITERIA = 3 MMI_Q_TEXT = 716
            Expected Result: DMI displays the  message “ATP Down Alarm” with sound alarm.Verify the following information,The hourglass symbol ST05 is displayed.All buttons and the ‘Close’ button are disabled.
            Test Step Comment: (1) MMI_gen 12018, MMI_gen 5732 (partly: before communication lost, current window ‘Main’);(2) MMI_gen 168 (partly: disabled buttons, ‘Main’ window, menu window);
            */

            /*
            Test Step 14
            Action: Then, simulate the communication loss between ETCS Onboard and DMI and re-establish the communication between ETCS onboard and DMI.
            Expected Result: Verify the following information,The hourglass symbol ST05 is removed.The ‘Main’ window is closed and DMI returns to the default window.The state of all buttons is restored according to the last received EVC-30, see Step 12:Button Start is enabledButton Driver ID is enabledButton Train Data is enabledButton Level is enabledButton Train running number is enabledButton Shunting is enabledButton Non-Leading is disabledButton Maintain Shunting is disabled
            Test Step Comment: (1) MMI_gen 5728 (partly: removal);(2) MMI_gen 5732 (partly: close the ‘Main’ window, switch back the default window);(3) MMI_gen 5728 (partly: restore (after ST05), default window);
            */

            /*
            Test Step 15
            Action: Press ‘Data view’ button.
            Expected Result: DMI displays Data view window.Verify the following information,The buttons are stated as follows:The navigation buttons are enabled.The ‘Close’ button is enabled.
            Test Step Comment: (1) MMI_gen 5728 (partly: data view window, before ST05 state, state of buttons);
            */

            /*
            Test Step 16
            Action: Use the test script file 10_2_a.xml to send EVC-8 withMMI_Q_TEXT_CRITERIA = 3 MMI_Q_TEXT = 716
            Expected Result: Verify the following information,The hourglass symbol ST05 is displayed.The ‘Close’ button is disabled.The navigation buttons are still enabled.
            Test Step Comment: (1) MMI_gen 12018;(2) MMI_gen 168 (partly: disabled button, data view window);(3) Note under MMI_gen 5728;
            */

            /*
            Test Step 17
            Action: Use the test script file 10_2_b.xml to send EVC-8 withMMI_Q_TEXT_CRITERIA = 4MMI_Q_TEXT = 716
            Expected Result: Verify the following information,The hourglass symbol ST05 is removed.The ‘Close’ button is enabled.
            Test Step Comment: (1) MMI_gen 5728 (partly: removal, EVC);(2) MMI_gen 5728 (partly: restore (after ST05), data view window);
            */

            /*
            Test Step 18
            Action: Use the test script file 10_2_a.xml to send EVC-8 withMMI_Q_TEXT_CRITERIA = 3 MMI_Q_TEXT = 716Then, start the stopwatch (to verify expected result of the next step).Note: Stopwatch is required for accuracy of test result.
            Expected Result: Verify the following information,The hourglass symbol ST05 is displayed.
            Test Step Comment: (1) MMI_gen 5731 (partly: before the expiration of 45 seconds, current window ‘Data view’);
            */

            /*
            Test Step 19
            Action: Wait for 46 second.Note: Stopwatch is required for accuracy of test result.
            Expected Result: Verify the following information,The hourglass symbol ST05 is removed.The ‘Data view’ window is closed and DMI returns to the default window.The state of all buttons is restored according to the last received EVC-30, see Step 12
            Test Step Comment: (1) MMI_gen 5728 (partly: removal);(2) MMI_gen 5731 (partly: close the ‘Data view’ window, switch back the default window);(3) MMI_gen 5728 (partly: restore (after ST05), default window);
            */

            /*
            Test Step 20
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
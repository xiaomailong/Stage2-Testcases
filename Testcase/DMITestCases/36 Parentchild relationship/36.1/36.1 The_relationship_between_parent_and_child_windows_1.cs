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
    /// 36.1 The relationship between parent and child windows (1)
    /// TC-ID: 33.1
    /// 
    /// This test case verifies the relationship between parent and child window when the driver presses ‘Close’ button in each window. The relationship between parent and child windows shall comply with [ERA-ERTMS] standard and [MMI-ETCS-gen].
    /// 
    /// Tested Requirements:
    /// MMI_gen 8785 (partly: Driver ID window, Train data window, Train data validation window, Level window, Train running number window, Main window, Language window, Volume window, Brightness window, System version window, Settings window); MMI_gen 8787;
    /// 
    /// Scenario:
    /// Activate Cabin A.Press ‘Settings’ button.Close the Settings window to verify that the Settings window is closed and displayed the Driver ID window.Press ‘TRN’ button.Close the Train Running Number window to verify that the Train Running Number window is closed and displayed the Driver ID window.Enter Driver ID and perform brake test.Select and confirm Level 1.Press ‘Driver ID’ button.Close the Driver ID window to verify that the Driver ID window is closed and displayed the Main window.Press ‘Train data’ button. Close the train data window to verify that the Train data window is closed and the Main window is displayed.Press ‘Train data’ button.Enter and confirm all valid train data.Press ‘Yes’ button.Close the Train data validation window. Verify that the Train data validation window is closed and the Main window is displayed.Press ‘Level’ button.Close the Level window. Verify the Level window is closed and the Main window is displayed.Press ‘Train running number’ button.Close the Train Running Number window. Verify the train running number is closed and the Main window is displayed.Close the Main window. Verify that the Main window is closed and the Default window is displayed. 
    /// 
    /// Used files:
    /// N/A
    /// </summary>
    public class The_relationship_between_parent_and_child_windows_1 : TestcaseBase
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
            // DMI displays  in SB mode, Level 1

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint


            /*
            Test Step 1
            Action: Activate cabin A.
            Expected Result: The Driver ID window is displayed. 
            */

            /*
            Test Step 2
            Action: Press ‘Settings’ button.
            Expected Result: The Settings window is displayed.
            */

            /*
            Test Step 3
            Action: Press ‘Close’ button.
            Expected Result: Verify that the Settings window is closed. The Driver ID window is displayed.
            Test Step Comment: MMI_gen 8787 (partly: Close the Settings window);
            */

            /*
            Test Step 4
            Action: Press ‘TRN’ button.
            Expected Result: The Train Running Number window is displayed.
            */

            /*
            Test Step 5
            Action: Press ‘Close’ button.
            Expected Result: Verify that the Train Running Number window is closed. The Driver ID window is displayed.
            Test Step Comment: MMI_gen 8787 (partly: Close the Train Running Number window);
            */

            /*
            Test Step 6
            Action: Enter Driver ID and perform brake test.
            Expected Result: The Level window is displayed.
            */

            /*
            Test Step 7
            Action: Select and confirm Level 1.
            Expected Result: The Main window is displayed.
            */

            /*
            Test Step 8
            Action: Press ‘Driver ID’ button.
            Expected Result: The Driver ID window is displayed. The ‘Close’ button is presented as enabled state.
            */

            /*
            Test Step 9
            Action: Close the Driver ID window.
            Expected Result: Verify that the Driver ID window is closed. The Main window is displayed.
            Test Step Comment: MMI_gen 8785 (partly: Close the Driver ID window);
            */

            /*
            Test Step 10
            Action: Press ‘Train data’ button
            Expected Result: The Train data window is displayed. The ‘Close’ button is presented as enabled state.
            */

            /*
            Test Step 11
            Action: Close the Train data window.
            Expected Result: Verify that the Train data window is closed. The Main window is displayed.
            Test Step Comment: MMI_gen 8785 (partly: Close the Train data window);
            */

            /*
            Test Step 12
            Action: Press ‘Train data’ button
            Expected Result: The Train data window is displayed.
            */

            /*
            Test Step 13
            Action: If the train data is fixed, re-select the train type and then press ‘Yes’ button.If the train data is flexible, re-entry all train data and then press ‘Yes’ button
            Expected Result: The Train data validation window is displayed.The ‘Close’ button is presented as enabled state.
            */

            /*
            Test Step 14
            Action: Press ‘Close’ button.
            Expected Result: Verify that the Train data validation window is closed. The Main window is displayed.
            Test Step Comment: MMI_gen 8785 (partly: Close the Train data validation window);
            */

            /*
            Test Step 15
            Action: Press ‘Level’ button.
            Expected Result: The Level window is displayed. The ‘Close’ button is presented as enabled state.
            */

            /*
            Test Step 16
            Action: Close the Level window.
            Expected Result: Verify that the Level window is closed. The Main window is displayed.
            Test Step Comment: MMI_gen 8785 (partly: Close the Level window);
            */

            /*
            Test Step 17
            Action: Press ‘Train running number’ button.
            Expected Result: The Train Running Number window is displayed. The ‘Close’ button is presented as enabled state.
            */

            /*
            Test Step 18
            Action: Close the Train Running Number window.
            Expected Result: Verify that the Train Running Number window is closed. The Main window is displayed.
            Test Step Comment: MMI_gen 8785 (partly: Close the Train Running Number window);
            */

            /*
            Test Step 19
            Action: Press ‘Close’ button.
            Expected Result: Verify that the Main window is closed. The Default window is displayed
            Test Step Comment: MMI_gen 8785 (partly: Close the Main window);
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
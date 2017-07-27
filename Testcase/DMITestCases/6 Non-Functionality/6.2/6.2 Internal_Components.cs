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
    /// 6.2 Internal Components
    /// TC-ID: 1.2
    /// 
    /// This test case verifies that DMI shall keep time zone offset at least 48 hrs. The real time clock function shall comply with [MMI-ETCS-gen].
    /// 
    /// Tested Requirements:
    /// MMI_gen 2446;
    /// 
    /// Scenario:
    /// Start up the system. Activate cabin A. Driver performs SoM in SB mode and selects level 1.
    /// 1.Press the icon of ‘Settings menu’ button.
    /// 2.Select the icon ‘Set Clock’ button. 
    /// 3.Enter new date, offset time and UTC time.
    /// 4.Close the Set Clock window and leave from Settings window.
    /// 5.Deactivate cabin A for 48 hrs.
    /// 6.Activate cabin A.
    /// 7.Perform SoM and select level 1.
    /// 8.Verify that DMI keep the new time zone offset for 48 hrs
    /// 
    /// Used files:
    /// N/A
    /// </summary>
    public class Internal_Components : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // System is power off.
            
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
            Action: Power on DMI and start up the system.Activate cabin A
            Expected Result: DMI displays the default window. The Driver ID window is displayed
            */
            // Call generic Check Results Method
            DmiExpectedResults.DMI_displays_the_default_window_The_Driver_ID_window_is_displayed();
            
            
            /*
            Test Step 2
            Action: Enter Driver ID and  selects level 1
            Expected Result: DMI displays in SB mode, level 1
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(@"Enter Driver ID and  selects level 1");
            // Call generic Check Results Method
            DmiExpectedResults.DMI_displays_in_SB_mode_level_1();
            
            
            /*
            Test Step 3
            Action: Press ‘Close’ button and select ‘Settings menu’
            Expected Result: The Settings window is presented with all sub-menus
            */
            // Call generic Check Results Method
            DmiExpectedResults.The_Settings_window_is_presented_with_all_sub_menus();
            
            
            /*
            Test Step 4
            Action: Select the icon of ‘Set Clock’ button
            Expected Result: The Set Clock window is presented to the driver.Verify that the year, month, day, hour, minute, second are displayed as real time update
            */
            
            
            /*
            Test Step 5
            Action: Enter the new value of Offset time. Then presses ‘Yes’ and closes the window
            Expected Result: The Settings window is displayed
            */
            // Call generic Check Results Method
            DmiExpectedResults.The_Settings_window_is_displayed();
            
            
            /*
            Test Step 6
            Action: Close the Settings window and deactivate cabin A
            Expected Result: Cabin A is deactivated
            */
            // Call generic Check Results Method
            DmiExpectedResults.Cabin_A_is_deactivated();
            
            
            /*
            Test Step 7
            Action: Power off DMI for 48 hrs. Then, activate Cabin A
            Expected Result: DMI displays the default window. The Driver ID window is displayed
            */
            // Call generic Check Results Method
            DmiExpectedResults.DMI_displays_the_default_window_The_Driver_ID_window_is_displayed();
            
            
            /*
            Test Step 8
            Action: Enter Driver ID and  selects level 1
            Expected Result: DMI displays in SB mode, level 1
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(@"Enter Driver ID and  selects level 1");
            // Call generic Check Results Method
            DmiExpectedResults.DMI_displays_in_SB_mode_level_1();
            
            
            /*
            Test Step 9
            Action: Perform the following procedure,Press ‘Close’ buttonSelect ‘Settings menu’Press ‘Set Clock’ button
            Expected Result: The Set Clock window is displayed.Verify that the new offset time has been stored on DMI for 48 hrs
            Test Step Comment: MMI_gen 2446;
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

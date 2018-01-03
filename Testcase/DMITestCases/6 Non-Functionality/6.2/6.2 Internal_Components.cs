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
using Testcase.Telegrams.EVCtoDMI;

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
    public class TC_1_2_Internal_Components : TestcaseBase
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
            Action: Power on DMI and start up the system.
            Activate cabin A
            Expected Result: DMI displays the default window. The Driver ID window is displayed
            */

            DmiActions.Start_ATP();
            DmiActions.Activate_Cabin_1(this);
            DmiActions.Set_Driver_ID(this, "1234");
            
            DmiExpectedResults.Driver_ID_window_displayed(this);


            /*
            Test Step 2
            Action: Enter Driver ID and  selects level 1
            Expected Result: DMI displays in SB mode, level 1
            */

            DmiActions.Send_SB_Mode(this);
            DmiExpectedResults.Driver_ID_entered(this);

            DmiActions.Display_Level_Window(this);
            DmiExpectedResults.Level_window_displayed(this);

            DmiExpectedResults.Level_1_Selected(this);

            DmiExpectedResults.DMI_displays_in_SB_mode_level_1(this);


            /*
            Test Step 3
            Action: Press ‘Close’ button and select ‘Settings menu’
            Expected Result: The Settings window is presented with all sub-menus
            */

            DmiActions.ShowInstruction(this, @"Press ‘Close’ button and select ‘Settings menu’");
            DmiActions.Open_the_Settings_window(this);

            DmiExpectedResults.DMI_displays_Settings_window(this);


            /*
            Test Step 4
            Action: Select the icon of ‘Set Clock’ button
            Expected Result: The Set Clock window is presented to the driver.
            Verify that the year, month, day, hour, minute, second are displayed as real time update
            */

            DmiActions.ShowInstruction(this, @"Select the icon of ‘Set Clock’ button");
            WaitForVerification("The Set Clock window is presented to the driver." + Environment.NewLine +
                                "Verify that the year, month, day, hour, minute, second are displayed as real time update.");

            /*
            Test Step 5
            Action: Enter the new value of Offset time. Then presses ‘Yes’ and closes the window
            Expected Result: The Settings window is displayed
            */

            DmiExpectedResults.UTC_time_changed(this);

            // Possible send EVC-3 MMI_SET_TIME_ATP packet
            EVC3_MMISetTimeATP.MMI_T_UTC = (uint)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
            EVC3_MMISetTimeATP.MMI_T_ZONE_OFFSET = 0;
            EVC3_MMISetTimeATP.Send();

            DmiActions.Open_the_Settings_window(this);
            DmiExpectedResults.DMI_displays_Settings_window(this);


            /*
            Test Step 6
            Action: Close the Settings window and deactivate cabin A
            Expected Result: Cabin A is deactivated
            */

            DmiActions.ShowInstruction(this, @"Press ‘Close’ button");
            DmiExpectedResults.Cab_deactivated(this);


            /*
            Test Step 7
            Action: Power off DMI for 48 hrs. Then, activate Cabin A
            Expected Result: DMI displays the default window. The Driver ID window is displayed
            */

            DmiActions.Activate_Cabin_1(this);
            DmiActions.Set_Driver_ID(this, "1234");

            DmiExpectedResults.Driver_ID_window_displayed(this);


            /*
            Test Step 8
            Action: Enter Driver ID and  selects level 1
            Expected Result: DMI displays in SB mode, level 1
            */

            DmiActions.Send_SB_Mode(this);
            DmiExpectedResults.Driver_ID_entered(this);

            DmiActions.Display_Level_Window(this);
            DmiExpectedResults.Level_window_displayed(this);

            DmiExpectedResults.Level_1_Selected(this);

            DmiExpectedResults.DMI_displays_in_SB_mode_level_1(this);


            /*
            Test Step 9
            Action: Perform the following procedure,
                    Press ‘Close’ button
                    Select ‘Settings menu’
                    Press ‘Set Clock’ button
            Expected Result: The Set Clock window is displayed.
                             Verify that the new offset time has been stored on DMI for 48 hrs
            Test Step Comment: MMI_gen 2446;
            */

            DmiActions.ShowInstruction(this, @"Press ‘Close’ button and select ‘Settings menu’");
            DmiActions.Open_the_Settings_window(this);
            DmiExpectedResults.DMI_displays_Settings_window(this);

            DmiActions.ShowInstruction(this, @"Select the icon of ‘Set Clock’ button");
            WaitForVerification("The Set Clock window is displayed." + Environment.NewLine +
                                "Verify that the new offset time has been stored on DMI for 48 hrs.");
            /*
            Test Step 10
            Action: End of test
            Expected Result: 
            */


            return GlobalTestResult;
        }
    }
}
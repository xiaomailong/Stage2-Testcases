using System;
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

        public override bool TestcaseEntryPoint()
        {
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 0;
            // Testcase entrypoint


            TraceHeader("Test Step 1");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Power on DMI and start up the system.");
            TraceReport("Expected Result");
            TraceInfo("DMI displays the default window. The Driver ID window is displayed");
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


            TraceHeader("Test Step 2");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Enter Driver ID and  selects level 1");
            TraceReport("Expected Result");
            TraceInfo("DMI displays in SB mode, level 1");
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


            TraceHeader("Test Step 3");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Press ‘Close’ button and select ‘Settings menu’");
            TraceReport("Expected Result");
            TraceInfo("The Settings window is presented with all sub-menus");
            /*
            Test Step 3
            Action: Press ‘Close’ button and select ‘Settings menu’
            Expected Result: The Settings window is presented with all sub-menus
            */

            DmiActions.ShowInstruction(this, @"Press ‘Close’ button and select ‘Settings menu’");
            DmiActions.Open_the_Settings_window(this);

            DmiExpectedResults.DMI_displays_Settings_window(this);


            TraceHeader("Test Step 4");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Select the icon of ‘Set Clock’ button");
            TraceReport("Expected Result");
            TraceInfo("The Set Clock window is presented to the driver.");
            /*
            Test Step 4
            Action: Select the icon of ‘Set Clock’ button
            Expected Result: The Set Clock window is presented to the driver.
            Verify that the year, month, day, hour, minute, second are displayed as real time update
            */

            DmiActions.ShowInstruction(this, @"Select the icon of ‘Set Clock’ button");
            WaitForVerification("The Set Clock window is presented to the driver." + Environment.NewLine +
                                "Verify that the year, month, day, hour, minute, second are displayed as real time update.");

            TraceHeader("Test Step 5");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Enter the new value of Offset time. Then presses ‘Yes’ and closes the window");
            TraceReport("Expected Result");
            TraceInfo("The Settings window is displayed");
            /*
            Test Step 5
            Action: Enter the new value of Offset time. Then presses ‘Yes’ and closes the window
            Expected Result: The Settings window is displayed
            */

            DmiExpectedResults.UTC_time_changed(this);

            // Possible send EVC-3 MMI_SET_TIME_ATP packet
            EVC3_MMISetTimeATP.MMI_T_UTC = (uint) (DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
            EVC3_MMISetTimeATP.MMI_T_ZONE_OFFSET = 0;
            EVC3_MMISetTimeATP.Send();

            DmiActions.Open_the_Settings_window(this);
            DmiExpectedResults.DMI_displays_Settings_window(this);


            TraceHeader("Test Step 6");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Close the Settings window and deactivate cabin A");
            TraceReport("Expected Result");
            TraceInfo("Cabin A is deactivated");
            /*
            Test Step 6
            Action: Close the Settings window and deactivate cabin A
            Expected Result: Cabin A is deactivated
            */

            DmiActions.ShowInstruction(this, @"Press ‘Close’ button");
            DmiExpectedResults.Cab_deactivated(this);


            TraceHeader("Test Step 7");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Power off DMI for 48 hrs. Then, activate Cabin A");
            TraceReport("Expected Result");
            TraceInfo("DMI displays the default window. The Driver ID window is displayed");
            /*
            Test Step 7
            Action: Power off DMI for 48 hrs. Then, activate Cabin A
            Expected Result: DMI displays the default window. The Driver ID window is displayed
            */

            DmiActions.Activate_Cabin_1(this);
            DmiActions.Set_Driver_ID(this, "1234");

            DmiExpectedResults.Driver_ID_window_displayed(this);


            TraceHeader("Test Step 8");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Enter Driver ID and  selects level 1");
            TraceReport("Expected Result");
            TraceInfo("DMI displays in SB mode, level 1");
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


            TraceHeader("Test Step 9");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Perform the following procedure,");
            TraceReport("Expected Result");
            TraceInfo("The Set Clock window is displayed.");
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
            TraceHeader("Test Step 10");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("End of test");
            
            /*
            Test Step 10
            Action: End of test
            Expected Result: 
            */


            return GlobalTestResult;
        }
    }
}
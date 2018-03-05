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
            UniqueIdentifier = 20357;
            // Testcase entrypoint


            MakeTestStepHeader(1, UniqueIdentifier++, "Power on DMI and start up the system.",
                "DMI displays the default window. The Driver ID window is displayed");
            /*
            Test Step 1
            Action: Power on DMI and start up the system.
            Activate cabin A
            Expected Result: DMI displays the default window. The Driver ID window is displayed
            */

            StartUp();
            DmiActions.Display_Driver_ID_Window(this, "1234");

            DmiExpectedResults.Driver_ID_window_displayed(this);


            MakeTestStepHeader(2, UniqueIdentifier++, "Enter Driver ID and  selects level 1",
                "DMI displays in SB mode, level 1");
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


            MakeTestStepHeader(3, UniqueIdentifier++, "Press ‘Close’ button and select ‘Settings menu’",
                "The Settings window is presented with all sub-menus");
            /*
            Test Step 3
            Action: Press ‘Close’ button and select ‘Settings menu’
            Expected Result: The Settings window is presented with all sub-menus
            */

            DmiActions.ShowInstruction(this, @"Press ‘Close’ button and select ‘Settings menu’");
            DmiActions.Open_the_Settings_window(this);

            DmiExpectedResults.DMI_displays_Settings_window(this);


            MakeTestStepHeader(4, UniqueIdentifier++, "Select the icon of ‘Set Clock’ button",
                "The Set Clock window is presented to the driver.");
            /*
            Test Step 4
            Action: Select the icon of ‘Set Clock’ button
            Expected Result: The Set Clock window is presented to the driver.
            Verify that the year, month, day, hour, minute, second are displayed as real time update
            */

            DmiActions.ShowInstruction(this, @"Select the icon of ‘Set Clock’ button");
            WaitForVerification("The Set Clock window is presented to the driver." + Environment.NewLine +
                                "Verify that the year, month, day, hour, minute, second are displayed as real time update.");

            MakeTestStepHeader(5, UniqueIdentifier++,
                "Enter the new value of Offset time. Then presses ‘Yes’ and closes the window",
                "The Settings window is displayed");
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


            MakeTestStepHeader(6, UniqueIdentifier++, "Close the Settings window and deactivate cabin A",
                "Cabin A is deactivated");
            /*
            Test Step 6
            Action: Close the Settings window and deactivate cabin A
            Expected Result: Cabin A is deactivated
            */

            DmiActions.ShowInstruction(this, @"Press ‘Close’ button");
            DmiExpectedResults.Cab_deactivated(this);


            MakeTestStepHeader(7, UniqueIdentifier++, "Power off DMI for 48 hrs. Then, activate Cabin A",
                "DMI displays the default window. The Driver ID window is displayed");
            /*
            Test Step 7
            Action: Power off DMI for 48 hrs. Then, activate Cabin A
            Expected Result: DMI displays the default window. The Driver ID window is displayed
            */

            DmiActions.Activate_Cabin_1(this);
            DmiActions.Display_Driver_ID_Window(this, "1234");

            DmiExpectedResults.Driver_ID_window_displayed(this);


            MakeTestStepHeader(8, UniqueIdentifier++, "Enter Driver ID and  selects level 1",
                "DMI displays in SB mode, level 1");
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


            MakeTestStepHeader(9, UniqueIdentifier++, "Perform the following procedure,",
                "The Set Clock window is displayed.");
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
            TraceHeader("End of test");

            /*
            Test Step 10
            Action: End of test
            Expected Result: 
            */


            return GlobalTestResult;
        }
    }
}
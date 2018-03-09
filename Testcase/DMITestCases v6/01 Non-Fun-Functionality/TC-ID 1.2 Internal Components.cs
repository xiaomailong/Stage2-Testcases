using System;
using BT_CSB_Tools;
using Testcase.Telegrams.EVCtoDMI;

namespace Testcase.DMITestCases
{
    /// <summary>
    /// Internal Components
    /// TC-ID: 1.2
    /// Doors unique ID: TP-35660
    /// This test case verifies that DMI shall keep time zone offset at least 48 hrs. The real time clock function shall comply with [MMI-ETCS-gen].
    /// 
    /// Tested Requirements:
    /// MMI_gen 2446;
    /// 
    /// Scenario:
    /// Start up the system. Activate cabin A. Driver performs SoM in SB mode and selects level 1.
    /// 1. Press the icon of ‘Settings menu’ button.
    /// 2. Select the icon ‘Set Clock’ button. 
    /// 3. Enter new date, offset time and UTC time.
    /// 4. Close the Set Clock window and leave from Settings window.
    /// 5. Deactivate cabin A for 48 hrs.
    /// 6. Activate cabin A.
    /// 7. Perform SoM and select level 1.
    /// 8. Verify that DMI keep the new time zone offset for 48 hrs
    /// 
    /// Used files:
    /// N/A
    /// </summary>
    public class TC_ID_1_2_Internal_Components : TestcaseBase
    {
        public override void PreExecution()
        {
            /* Pre-conditions from TestSpec
            	-  Set the tag name CLOCK_TIME_SOURCE = 0 in configuration file (See the instruction in Appendix 1)
            	-  System is power off.
            */

            TraceInfo("Pre-condition: " +
                      "-  Set the tag name CLOCK_TIME_SOURCE = 0 in configuration file (See the instruction in Appendix 1)" +
                      Environment.NewLine + "-  System is power off.");

            base.PreExecution();
        }

        public override void PostExecution()
        {
            /* Post-conditions from TestSpec
            	DMI displays in SB mode, level 1.
            */

            TraceInfo("Post-condition: " + "DMI displays in SB mode, level 1.");

            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            throw new TestcaseException("This testcase may not be relevant, to be reviewed");
            StartUp();

            /*
            Test Step 1
            Action:
            	Power on DMI and start up the system.
            	Activate cabin A
            Expected Result:
            	DMI displays the default window. The Driver ID window is displayed
            */
            MakeTestStepHeader(1, 35678,
                "Power on DMI and start up the system." + Environment.NewLine + "Activate cabin A",
                "DMI displays the default window. The Driver ID window is displayed");

            DmiActions.Display_Driver_ID_Window(this, "1234");

            DmiExpectedResults.Driver_ID_window_displayed(this);

            /*
            Test Step 2
            Action:
            	Enter Driver ID and  selects level 1
            Expected Result:
            	DMI displays in SB mode, level 1
            */
            MakeTestStepHeader(2, 35679, "Enter Driver ID and  selects level 1", "DMI displays in SB mode, level 1");

            DmiActions.Send_SB_Mode(this);
            DmiExpectedResults.Driver_ID_entered(this);

            DmiActions.Display_Level_Window(this);
            DmiExpectedResults.Level_window_displayed(this);

            DmiExpectedResults.Level_1_Selected(this);

            DmiExpectedResults.DMI_displays_in_SB_mode_level_1(this);

            /*
            Test Step 3
            Action:
            	Press ‘Close’ button and select ‘Settings menu’
            Expected Result:
            	The Settings window is presented with all sub-menus
            */
            MakeTestStepHeader(3, 35680, "Press ‘Close’ button and select ‘Settings menu’",
                "The Settings window is presented with all sub-menus");

            DmiActions.ShowInstruction(this, @"Press ‘Close’ button and select ‘Settings menu’");
            DmiActions.Open_the_Settings_window(this);

            DmiExpectedResults.DMI_displays_Settings_window(this);

            /*
            Test Step 4
            Action:
            	Select the icon of ‘Set Clock’ button
            Expected Result:
            	The Set Clock window is presented to the driver.
            	Verify that the year, month, day, hour, minute, second are displayed as real time update
            */
            MakeTestStepHeader(4, 35681, "Select the icon of ‘Set Clock’ button",
                "The Set Clock window is presented to the driver." + Environment.NewLine +
                "Verify that the year, month, day, hour, minute, second are displayed as real time update");

            DmiActions.ShowInstruction(this, @"Select the icon of ‘Set Clock’ button");
            WaitForVerification("The Set Clock window is presented to the driver." + Environment.NewLine +
                                "Verify that the year, month, day, hour, minute, second are displayed as real time update.");

            /*
            Test Step 5
            Action:
            	Enter the new value of Offset time. Then presses ‘Yes’ and closes the window
            Expected Result:
            	The Settings window is displayed
            */
            MakeTestStepHeader(5, 35682, "Enter the new value of Offset time. Then presses ‘Yes’ and closes the window",
                "The Settings window is displayed");

            DmiExpectedResults.UTC_time_changed(this);

            // Possible send EVC-3 MMI_SET_TIME_ATP packet
            EVC3_MMISetTimeATP.MMI_T_UTC = (uint) (DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
            EVC3_MMISetTimeATP.MMI_T_ZONE_OFFSET = 0;
            EVC3_MMISetTimeATP.Send();

            DmiActions.Open_the_Settings_window(this);
            DmiExpectedResults.DMI_displays_Settings_window(this);

            /*
            Test Step 6
            Action:
            	Close the Settings window and deactivate cabin A
            Expected Result:
            	Cabin A is deactivated
            */
            MakeTestStepHeader(6, 35683, "Close the Settings window and deactivate cabin A", "Cabin A is deactivated");

            DmiActions.ShowInstruction(this, @"Press ‘Close’ button");
            DmiExpectedResults.Cab_deactivated(this);

            /*
            Test Step 7
            Action:
            	Power off DMI for 48 hrs. Then, activate Cabin A
            Expected Result:
            	DMI displays the default window. The Driver ID window is displayed
            */
            MakeTestStepHeader(7, 35684, "Power off DMI for 48 hrs. Then, activate Cabin A",
                "DMI displays the default window. The Driver ID window is displayed");

            DmiActions.Activate_Cabin_1(this);
            DmiActions.Display_Driver_ID_Window(this, "1234");

            DmiExpectedResults.Driver_ID_window_displayed(this);

            /*
            Test Step 8
            Action:
            	Enter Driver ID and  selects level 1
            Expected Result:
            	DMI displays in SB mode, level 1
            */
            MakeTestStepHeader(8, 35685, "Enter Driver ID and  selects level 1", "DMI displays in SB mode, level 1");

            DmiActions.Send_SB_Mode(this);
            DmiExpectedResults.Driver_ID_entered(this);

            DmiActions.Display_Level_Window(this);
            DmiExpectedResults.Level_window_displayed(this);

            DmiExpectedResults.Level_1_Selected(this);

            DmiExpectedResults.DMI_displays_in_SB_mode_level_1(this);

            /*
            Test Step 9
            Action:
            	Perform the following procedure,
            	Press ‘Close’ button
            	Select ‘Settings menu’
            	Press ‘Set Clock’ button
            Expected Result:
            	The Set Clock window is displayed.
            	Verify that the new offset time has been stored on DMI for 48 hrs
            Test Step Comment:
            	MMI_gen 2446;
            */
            MakeTestStepHeader(9, 35686,
                "Perform the following procedure," + Environment.NewLine + "Press ‘Close’ button" +
                Environment.NewLine + "Select ‘Settings menu’" + Environment.NewLine + "Press ‘Set Clock’ button",
                "The Set Clock window is displayed." + Environment.NewLine +
                "Verify that the new offset time has been stored on DMI for 48 hrs");

            DmiActions.ShowInstruction(this, @"Press ‘Close’ button and select ‘Settings menu’");
            DmiActions.Open_the_Settings_window(this);
            DmiExpectedResults.DMI_displays_Settings_window(this);

            DmiActions.ShowInstruction(this, @"Select the icon of ‘Set Clock’ button");
            WaitForVerification("The Set Clock window is displayed." + Environment.NewLine +
                                "Verify that the new offset time has been stored on DMI for 48 hrs.");

            /* End Of Test */
            TraceHeader("End Of Test");


            return GlobalTestResult;
        }
    }
}
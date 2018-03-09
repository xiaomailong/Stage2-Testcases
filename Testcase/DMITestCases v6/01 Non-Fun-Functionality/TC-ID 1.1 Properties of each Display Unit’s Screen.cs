using System;

namespace Testcase.DMITestCases
{
    /// <summary>
    /// Properties of each Display Unit’s Screen
    /// TC-ID: 1.1
    /// Doors unique ID: TP-35628
    /// This test case verifies the luminance property of DMI are displayed properly. The Brightness window shall display as half-grid array on DMI’s screen. DMI shall support driver’s adjustment of the brightness of the display and possible to adjust the brightness to a defined minimum level. The properties and the presentation of the Brightness window shall comply with [ERA] standard and [MMI-ETCS-gen].
    /// 
    /// Tested Requirements:
    /// MMI_gen 3091; MMI_gen 258 (partly);
    /// 
    /// Scenario:
    /// Open Settings window.
    /// Press icon of ‘Brightness’ button. 
    /// And then, press the button to respectively decrease the luminance.
    /// The luminance will be decreased until minimum level.
    /// Adjust again by pressing the button to increase the luminance to maximum level.
    /// Deactivate and activate cabin again. Then, open Brightness window and verifes display information.
    /// Enter and confirm maximum value of luminance.
    /// Deactivate and activate cabin again. Then, open Brightness window and verifes display information.
    /// 
    /// Used files:
    /// N/A
    /// </summary>
    public class TC_ID_1_1_Properties_of_each_Display_Units_Screen : TestcaseBase
    {
        public override void PreExecution()
        {
            /* Pre-conditions from TestSpec
            	Set the following tag names (See the instruction in Appendix 1)
            	MIN_BRIGHT = 10
            	MAX_BRIGHT = 100
            	Test system is powered ON
            	Cabin A is activated
            */

            TraceInfo("Pre-condition: " + "Set the following tag names (See the instruction in Appendix 1)" +
                      Environment.NewLine + "MIN_BRIGHT = 10" + Environment.NewLine + "MAX_BRIGHT = 100" +
                      Environment.NewLine + "Test system is powered ON" + Environment.NewLine + "Cabin A is activated");

            base.PreExecution();
        }

        public override void PostExecution()
        {
            /* Post-conditions from TestSpec
            	DMI displays in SB mode.
            */

            TraceInfo("Post-condition: " + "DMI displays in SB mode.");

            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            StartUp();

            DmiActions.Complete_SoM_L1_SB(this);

            /*
            Test Step 1
            Action:
            	Press ‘Settings’ button
            Expected Result:
            	DMI displays Settings window
            */
            MakeTestStepHeader(1, 35648, "Press ‘Settings’ button", "DMI displays Settings window");

            DmiExpectedResults.Settings_Button_Pressed(this);

            DmiActions.Open_the_Settings_window(this);

            DmiExpectedResults.DMI_displays_Settings_window(this);

            /*
            Test Step 2
            Action:
            	Press ‘Brightness’ button
            Expected Result:
            	DMI displays Brightness window.
            	Verify the following information,
            	The value of an input field is 55 (median value between 10 an 100)
            Test Step Comment:
            	(1) MMI_gen 3091 (partly: default luminance);
            */
            MakeTestStepHeader(2, 35649, "Press ‘Brightness’ button",
                "DMI displays Brightness window." + Environment.NewLine + "Verify the following information," +
                Environment.NewLine + "The value of an input field is 55 (median value between 10 an 100)");

            DmiActions.ShowInstruction(this, @"Press ‘Brightness’ button");

            WaitForVerification(@"Is the Brightness set at median value (= 55)?");

            /*
            Test Step 3
            Action:
            	Press and hold ‘-‘ button in order to decreasing brightness to defined minimum level
            Expected Result:
            	The adjust luminance is used by DMI.
            	Verify the following information
            	Verify that value of an input field is decreasing while button is pressed and the brightness is dimmer than before pressing button.
            	Verify that the minimum level of bightness is defined as 10
            Test Step Comment:
            	(1) MMI_gen 258 (partly: adjustment of the brightness);
            	(2) MMI_gen 258 (partly: defined minimum level);  
            */
            MakeTestStepHeader(3, 35650,
                "Press and hold ‘-‘ button in order to decreasing brightness to defined minimum level",
                "The adjust luminance is used by DMI." + Environment.NewLine + "Verify the following information" +
                Environment.NewLine +
                "Verify that value of an input field is decreasing while button is pressed and the brightness is dimmer than before pressing button." +
                Environment.NewLine + "Verify that the minimum level of bightness is defined as 10");

            DmiActions.ShowInstruction(this,
                @"Press and hold ‘-‘ button in order to decreasing brightness to defined minimum level");

            WaitForVerification(
                "Verify that value of an input field is decreasing while button is pressed and the brightness is dimmer than before pressing button." +
                Environment.NewLine +
                "Verify that the minimum level of bightness is defined as 10.");

            /*
            Test Step 4
            Action:
            	Press ‘Close’ button
            Expected Result:
            	DMI displays Settings window
            */
            MakeTestStepHeader(4, 35651, "Press ‘Close’ button", "DMI displays Settings window");

            DmiActions.ShowInstruction(this, @"Press ‘Close’ button");

            DmiExpectedResults.DMI_displays_Settings_window(this);

            /*
            Test Step 5
            Action:
            	Press ‘Close’ button
            Expected Result:
            	DMI displays Driver ID window
            */
            MakeTestStepHeader(5, 35652, "Press ‘Close’ button", "DMI displays Driver ID window");

            DmiActions.ShowInstruction(this, @"Press ‘Close’ button");

            DmiExpectedResults.Driver_ID_window_displayed(this);

            /*
            Test Step 6
            Action:
            	Perform the following procedure,
            	Press ‘Settings’ button.
            	Press ‘Brightness’ button
            Expected Result:
            	DMI displays Brightness window.
            	The value of an input field is restored to 55 and the brightness is not effected from setting of step 3
            */
            MakeTestStepHeader(6, 35653,
                "Perform the following procedure," + Environment.NewLine + "Press ‘Settings’ button." +
                Environment.NewLine + "Press ‘Brightness’ button",
                "DMI displays Brightness window." + Environment.NewLine +
                "The value of an input field is restored to 55 and the brightness is not effected from setting of step 3");

            DmiActions.ShowInstruction(this, @"Press ‘Settings’ button, followed by the Brightness button.");

            WaitForVerification(@"Is the Brightness set at median value (= 55)?");

            /*
            Test Step 7
            Action:
            	Press and hold ‘+‘ button in order to increasing brightness to defined maximum level
            Expected Result:
            	The value of an input field is increasing while button is pressed and the brightness is brighter than before pressing button.
            	The maximum level of bightness is defined as 100
            */
            MakeTestStepHeader(7, 35654,
                "Press and hold ‘+‘ button in order to increasing brightness to defined maximum level",
                "The value of an input field is increasing while button is pressed and the brightness is brighter than before pressing button." +
                Environment.NewLine + "The maximum level of bightness is defined as 100");

            DmiActions.ShowInstruction(this,
                @"Press and hold ‘+‘ button in order to increasing brightness to defined maximum level");

            WaitForVerification("Verify the following:" + Environment.NewLine + Environment.NewLine +
                                "- The value of an input field is increasing while button is pressed and the brightness is brighter than before pressing button." +
                                Environment.NewLine +
                                "- The maximum level of bightness is defined as 100.");

            /*
            Test Step 8
            Action:
            	Perform the following procedure,
            	Decrease the brightness to minimum value.
            	De-activate Cabin
            	Activate Cabin
            	Press ‘Settings’ button.
            	Press ‘Brightness’ button
            Expected Result:
            	The brightness is increased from the minimum and the value of and input field is 55 (median value between 10 an 100)
            Test Step Comment:
            	MMI_gen 3091 (partly: In case no luminance is stored);
            */
            MakeTestStepHeader(8, 35655,
                "Perform the following procedure," + Environment.NewLine + "Decrease the brightness to minimum value." +
                Environment.NewLine + "De-activate Cabin" + Environment.NewLine + "Activate Cabin" +
                Environment.NewLine + "Press ‘Settings’ button." + Environment.NewLine + "Press ‘Brightness’ button",
                "The brightness is increased from the minimum and the value of and input field is 55 (median value between 10 an 100)");

            DmiActions.ShowInstruction(this, @"Decrease the brightness to minimum value.");

            DmiActions.ShowInstruction(this, @"Rebooting Cab...");
            DmiActions.Deactivate_and_activate_cabin(this);

            DmiActions.ShowInstruction(this, @"Press ‘Settings’ button, followed by the Brightness button.");

            WaitForVerification(@"Is the Brightness set at median value (= 55)?");

            /*
            Test Step 9
            Action:
            	Repeat action Step 7.
            	Then, confirm entered data by pressing an input fied
            Expected Result:
            	DMI displays Settings window with luminance increased refer to entered data
            */
            MakeTestStepHeader(9, 35656,
                "Repeat action Step 7." + Environment.NewLine + "Then, confirm entered data by pressing an input fied",
                "DMI displays Settings window with luminance increased refer to entered data");

            DmiActions.ShowInstruction(this,
                "Press and hold ‘+‘ button in order to increasing brightness to defined maximum level." +
                Environment.NewLine +
                "Then, confirm entered data by pressing an input field.");

            WaitForVerification(
                @"Confirm that the DMI Settings window is brighter according to the set value in the previous step.");

            /*
            Test Step 10
            Action:
            	Press ‘Brightness’ button.
            	Then, repeat action step 8
            Expected Result:
            	The ‘Brightness’ window is come up with maximum value of the luminance range
            Test Step Comment:
            	MMI_gen 3091 (partly: The last stored luminance shall be used when opening the desk);
            */
            MakeTestStepHeader(10, 35657,
                "Press ‘Brightness’ button." + Environment.NewLine + "Then, repeat action step 8",
                "The ‘Brightness’ window is come up with maximum value of the luminance range");

            DmiActions.ShowInstruction(this, @"Decrease the brightness to minimum value.");

            DmiActions.ShowInstruction(this, @"Rebooting Cab...");
            DmiActions.Deactivate_and_activate_cabin(this);

            DmiActions.ShowInstruction(this, @"Press ‘Settings’ button, followed by the Brightness button.");

            WaitForVerification(@"Is the Brightness set at its maximum value?");

            /* End Of Test */
            TraceHeader("End Of Test");


            return GlobalTestResult;
        }
    }
}
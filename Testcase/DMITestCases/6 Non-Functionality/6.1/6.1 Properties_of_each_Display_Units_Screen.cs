using System;

namespace Testcase.DMITestCases
{
    /// <summary>
    /// 6.1 Properties of each Display Unit’s Screen
    /// TC-ID: 1.1
    /// 
    /// This test case verifies the luminance property of DMI are displayed properly. The Brightness window shall display as half-grid array on DMI’s screen. DMI shall support driver’s adjustment of the brightness of the display and possible to adjust the brightness to a defined minimum level. The properties and the presentation of the Brightness window shall comply with [ERA] standard and [MMI-ETCS-gen].
    /// 
    /// Tested Requirements:
    /// MMI_gen 3091; MMI_gen 258 (partly);
    /// 
    /// Scenario:
    /// Open Settings window.Press icon of ‘Brightness’ button. And then, press the button to respectively decrease the luminance.The luminance will be decreased until minimum level.Adjust again by pressing the button to increase the luminance to maximum level.Deactivate and activate cabin again. Then, open Brightness window and verifes display information.Enter and confirm maximum value of luminance.Deactivate and activate cabin again. Then, open Brightness window and verifes display information.
    /// 
    /// Used files:
    /// N/A
    /// </summary>
    public class TC_1_1_Properties_of_each_Display_Units_Screen : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // Set the following tag names (See the instruction in Appendix 1)MIN_BRIGHT = 10MAX_BRIGHT = 100Test system is powered ONCabin A is activated

            // Call the TestCaseBase PreExecution
            base.PreExecution();

            DmiActions.Start_ATP();
            DmiActions.Activate_Cabin_1(this);
            DmiActions.Set_Driver_ID(this, "1234");
            DmiActions.Send_SB_Mode(this);
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in SB mode.

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 0;
            // Testcase entrypoint


            TraceHeader("Test Step 1");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Press ‘Settings’ button");
            TraceReport("Expected Result");
            TraceInfo("DMI displays Settings window");
            /*
            Test Step 1
            Action: Press ‘Settings’ button
            Expected Result: DMI displays Settings window
            */

            DmiActions.ShowInstruction(this, @"Press ‘Settings’ button");
            DmiActions.Open_the_Settings_window(this);

            DmiExpectedResults.DMI_displays_Settings_window(this);

            TraceHeader("Test Step 2");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Press ‘Brightness’ button");
            TraceReport("Expected Result");
            TraceInfo("DMI displays Brightness window.");
            /*
            Test Step 2
            Action: Press ‘Brightness’ button
            Expected Result: DMI displays Brightness window.
            Verify the following information,
            The value of and input field is 55 (median value between 10 an 100)
            Test Step Comment: (1) MMI_gen 3091 (partly: default luminance);
            */

            DmiActions.ShowInstruction(this, @"Press ‘Brightness’ button");

            WaitForVerification(@"Is the Brightness set at median value (= 55)?");

            TraceHeader("Test Step 3");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Press and hold ‘-‘ button in order to decreasing brightness to defined minimum level");
            TraceReport("Expected Result");
            TraceInfo("The adjust luminance is used by DMI.");
            /*
            Test Step 3
            Action: Press and hold ‘-‘ button in order to decreasing brightness to defined minimum level
            Expected Result: The adjust luminance is used by DMI.
            Verify the following information
            Verify that value of an input field is decreasing while button is pressed and the brightness is dimmer than before pressing button.
            Verify that the minimum level of bightness is defined as 10
            Test Step Comment: (1) MMI_gen 258 (partly: adjustment of the brightness);
                               (2) MMI_gen 258 (partly: defined minimum level);  
            */

            DmiActions.ShowInstruction(this,
                @"Press and hold ‘-‘ button in order to decreasing brightness to defined minimum level");

            WaitForVerification(
                "Verify that value of an input field is decreasing while button is pressed and the brightness is dimmer than before pressing button." +
                Environment.NewLine +
                "Verify that the minimum level of bightness is defined as 10.");

            TraceHeader("Test Step 4");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Press ‘Close’ button");
            TraceReport("Expected Result");
            TraceInfo("DMI displays Settings window");
            /*
            Test Step 4
            Action: Press ‘Close’ button
            Expected Result: DMI displays Settings window
            */

            DmiActions.ShowInstruction(this, @"Press ‘Close’ button");

            DmiExpectedResults.DMI_displays_Settings_window(this);

            TraceHeader("Test Step 5");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Press ‘Close’ button");
            TraceReport("Expected Result");
            TraceInfo("DMI displays Driver ID window");
            /*
            Test Step 5
            Action: Press ‘Close’ button
            Expected Result: DMI displays Driver ID window
            */

            DmiActions.ShowInstruction(this, @"Press ‘Close’ button");

            DmiExpectedResults.Driver_ID_window_displayed(this);

            TraceHeader("Test Step 6");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Perform the following procedure,");
            TraceReport("Expected Result");
            TraceInfo("DMI displays Brightness window.");
            /*
            Test Step 6
            Action: Perform the following procedure,
            Press ‘Settings’ button.
            Press ‘Brightness’ button
            Expected Result: DMI displays Brightness window.
            The value of an input field is restored to 55 and the brightness is not effected from setting of step 3
            */

            DmiActions.ShowInstruction(this, @"Press ‘Settings’ button");

            DmiActions.ShowInstruction(this, @"Press ‘Brightness’ button");
            WaitForVerification(@"Is the Brightness set at median value (= 55)?");

            TraceHeader("Test Step 7");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Press and hold ‘+‘ button in order to increasing brightness to defined maximum level");
            TraceReport("Expected Result");
            TraceInfo(
                "The value of an input field is increasing while button is pressed and the brightness is brighter than before pressing button.");
            /*
            Test Step 7
            Action: Press and hold ‘+‘ button in order to increasing brightness to defined maximum level
            Expected Result: The value of an input field is increasing while button is pressed and the brightness is brighter than before pressing button.
            The maximum level of bightness is defined as 100
            */

            DmiActions.ShowInstruction(this,
                @"Press and hold ‘+‘ button in order to increasing brightness to defined maximum level");

            WaitForVerification("Verify the following:" + Environment.NewLine + Environment.NewLine +
                                "- The value of an input field is increasing while button is pressed and the brightness is brighter than before pressing button." +
                                Environment.NewLine +
                                "- The maximum level of bightness is defined as 100.");

            TraceHeader("Test Step 8");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Perform the following procedure,");
            TraceReport("Expected Result");
            TraceInfo(
                "The brightness is increased from the minimum and the value of and input field is 55 (median value between 10 an 100)");
            /*
            Test Step 8
            Action: Perform the following procedure,
            Decrease the brightness to minimum value.
            De-activate Cabin
            Activate Cabin
            Press ‘Settings’ button.
            Press ‘Brightness’ button
            Expected Result: The brightness is increased from the minimum and the value of and input field is 55 (median value between 10 an 100)
            Test Step Comment: MMI_gen 3091 (partly: In case no luminance is stored);
            */

            DmiActions.ShowInstruction(this, @"Decrease the brightness to minimum value.");

            DmiActions.ShowInstruction(this, @"Rebooting Cab...");
            DmiActions.Deactivate_and_activate_cabin(this);

            DmiActions.ShowInstruction(this, @"Press ‘Settings’ button");

            DmiActions.ShowInstruction(this, @"Press ‘Brightness’ button");
            WaitForVerification(@"Is the Brightness set at median value (= 55)?");

            TraceHeader("Test Step 9");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Repeat action Step 7.Then, confirm entered data by pressing an input fied");
            TraceReport("Expected Result");
            TraceInfo("DMI displays Settings window with luminance increased refer to entered data");
            /*
            Test Step 9
            Action: Repeat action Step 7.Then, confirm entered data by pressing an input fied
            Expected Result: DMI displays Settings window with luminance increased refer to entered data
            */

            DmiActions.ShowInstruction(this,
                "Press and hold ‘+‘ button in order to increasing brightness to defined maximum level." +
                Environment.NewLine +
                "Then, confirm entered data by pressing an input field");

            WaitForVerification(
                @"Confirm that DMI displays Settings window with luminance increased refer to entered data");

            TraceHeader("Test Step 10");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Press ‘Brightness’ button.");
            TraceReport("Expected Result");
            TraceInfo("The ‘Brightness’ window is come up with maximum value of the luminance range");
            /*
            Test Step 10
            Action: Press ‘Brightness’ button.
            Then, repeat action step 8
            Expected Result: The ‘Brightness’ window is come up with maximum value of the luminance range
            Test Step Comment: MMI_gen 3091 (partly: The last stored luminance shall be used when opening the desk);
            */

            DmiActions.ShowInstruction(this, @"Decrease the brightness to minimum value.");

            DmiActions.ShowInstruction(this, @"Rebooting Cab...");
            DmiActions.Deactivate_and_activate_cabin(this);

            DmiActions.ShowInstruction(this, @"Press ‘Settings’ button");

            DmiActions.ShowInstruction(this, @"Press ‘Brightness’ button");

            WaitForVerification(@"Is the Brightness set at its maximum value?");

            TraceHeader("Test Step 11");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("End of test");
            TraceReport("Expected Result");
            TraceInfo("");
            /*
            Test Step 11
            Action: End of test
            Expected Result: 
            */


            return GlobalTestResult;
        }
    }
}
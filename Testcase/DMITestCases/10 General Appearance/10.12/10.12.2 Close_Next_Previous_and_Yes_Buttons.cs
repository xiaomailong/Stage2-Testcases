using System;
using Testcase.Telegrams.EVCtoDMI;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 10.12.2 Close, Next, Previous and Yes Buttons
    /// TC-ID: 5.12.2
    /// 
    /// This test case verifies the navigation buttons including close, next, previous, and delete buttons are available on data view window and train data entry windows. The symbol, type of button, states, sound, and button activation shall comply [MMI-ETCS-gen]
    /// 
    /// Tested Requirements:
    /// MMI_gen 4393 (partly: delete, MMI_gen 4384); MMI_gen 4440 (partly: other navigation buttons);  MMI_gen 4395 (partly: close button, disabled);
    /// 
    /// Scenario:
    /// Activate cabin A. Enter the Driver ID. Verify the repeat function within the delete button.  Next perform brake test. Then select and confirm level 
    /// 1.Select ‘Train data’ button.Enter the train data.Verify the state, sound and type of next and previous button . Close the Train data window. 
    /// 
    /// Used files:
    /// N/A
    /// </summary>
    public class TC_ID_5_12_2_Navigation_Buttons : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:

            // Call the TestCaseBase PreExecution
            base.PreExecution();

            // Test system is powered on.
            DmiActions.Start_ATP();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in SB mode, Level 1.

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
            TraceInfo("Activate cabin A");
            TraceReport("Expected Result");
            TraceInfo("DMI displays Driver ID window.");
            /*
            Test Step 1
            Action: Activate cabin A
            Expected Result: DMI displays Driver ID window.
            Verify the following information,
            Delete button NA21 is shown as enabled state in area G.
            The disabled Close button NA12 is display in area G
            Test Step Comment: (1) MMI_gen 4392 (partly: bullet e, delete NA21); MMI_gen 4440 (partly: delete, enabled);(2) MMI_gen 4440 (partly: other navigation buttons); MMI_gen 4396 (partly: close, NA12); MMI_gen 4392 (partly: bullet a, close button);  MMI_gen 4395 (partly: close button, disabled);
            */
            // Call generic Action Method
            DmiActions.Activate_Cabin_1(this);

            DmiActions.Set_Driver_ID(this, "1234");
            DmiActions.Send_SB_Mode(this);
            DmiExpectedResults.Driver_ID_window_displayed_in_SB_mode(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Driver ID window." + Environment.NewLine +
                                @"2. The ‘Delete’ button NA21 is displayed enabled in area G." + Environment.NewLine +
                                @"3. The ‘Close’ button NA12 is displayed disabled in area G.");

            TraceHeader("Test Step 2");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Enter the Driver ID ‘12345678’");
            TraceReport("Expected Result");
            TraceInfo("The value of an input field is displayed correspond with entered data");
            /*
            Test Step 2
            Action: Enter the Driver ID ‘12345678’
            Expected Result: The value of an input field is displayed correspond with entered data
            */
            DmiActions.ShowInstruction(this, "Enter the Driver ID ‘12345678’.");
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The Input Field is displayed with the value ‘1234 5678’");

            TraceHeader("Test Step 3");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Press and hold ‘Delete’ button up to 2 second.");
            TraceReport("Expected Result");
            TraceInfo("Verify the following information,");
            /*
            Test Step 3
            Action: Press and hold ‘Delete’ button up to 2 second.
            Note:Stopwatch is required
            Expected Result: Verify the following information,
            While press and hold button less than 1.5 sec
                Sound ‘Click’ is played once.
                The state of button is changed to ‘Pressed’ and immediately back to ‘Enabled’ state.
                The last character of Driver ID is  deleted from the input field.
            While press and hold button over 1.5 sec
                The state ‘pressed’ and ‘released’ are switched repeatly while button is pressed and the characters are removed from an input field repeatly refer to pressed state.
                The sound ‘Click’ is played repeatly while button is pressed
            Test Step Comment: (1) MMI_gen 4393 (partly: delete, MMI_gen 4384 (partly: sound ‘Click’));(2) MMI_gen 4393 (partly: MMI_gen 4384 (partly: Change to state ‘Pressed’ and immediately back to state ‘Enabled’));(3) MMI_gen 4393 (partly: delete, MMI_gen 4384 (partly: ETCS-MMI’s function associated to the button));(4) MMI_gen 4393 (partly: delete, MMI_gen 4386 (partly: visual of repeat function));(5) MMI_gen 4393 (partly: delete, MMI_gen 4386 (partly: audible of repeat function));
            */
            DmiActions.ShowInstruction(this, @"Press and hold the ‘Delete’ button then release the button within 1.5s");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘click’ sound is played once." + Environment.NewLine +
                                @"2. The ‘Delete’ button is displayed pressed and immediately changed to enabled." +
                                Environment.NewLine +
                                @"3. The last character (‘8’) of the Driver ID is deleted from the Input field");


            DmiActions.ShowInstruction(this, @"Press and hold the ‘Delete’ button for 2s");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘Delete’ button is displayed pressed and immediately changed to enabled repeatedly." +
                                Environment.NewLine +
                                "2. Characters are repeatedly deleted from the end of the Input field" +
                                Environment.NewLine +
                                @"3. The ‘click’ sound is played repeatedly while the ‘Delete’ button is pressed.");

            TraceHeader("Test Step 4");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Release the pressed button");
            TraceReport("Expected Result");
            TraceInfo("Verify the following information,");
            /*
            Test Step 4
            Action: Release the pressed button
            Expected Result: Verify the following information,
            The state of pressed button is changed to ‘Enabled’ state
            Test Step Comment: (1) MMI_gen 4393 (partly: delete, MMI_gen 4384 (partly: ETCS-MMI’s function associated to the button));
            */
            DmiActions.ShowInstruction(this, "Release the pressed button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘Delete’ button is displayed enabled.");

            TraceHeader("Test Step 5");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Perform the following procedure,");
            TraceReport("Expected Result");
            TraceInfo("DMI displays the Main window");
            /*
            Test Step 5
            Action: Perform the following procedure,
            Enter and confirm Driver ID
            Perform brake test
            Select and confirm Level 1
            Expected Result: DMI displays the Main window
            */

            DmiExpectedResults.Driver_ID_entered(this);

            DmiActions.Request_Brake_Test(this);
            DmiActions.ShowInstruction(this, "Perform Brake Test");
            DmiActions.Perform_Brake_Test(this, 2);
            Wait_Realtime(5000);
            DmiActions.Display_Brake_Test_Successful(this, 3);

            DmiActions.Display_Level_Window(this);
            DmiActions.Delete_Brake_Test_Successful(this, 3);

            DmiActions.ShowInstruction(this, "Select and enter Level 1");

            DmiActions.Display_Main_Window_with_Start_button_not_enabled(this);
            DmiExpectedResults.Main_Window_displayed(this, false);

            TraceHeader("Test Step 6");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Press ‘Driver ID’ button");
            TraceReport("Expected Result");
            TraceInfo("DMI displays the Driver ID window.");
            /*
            Test Step 6
            Action: Press ‘Driver ID’ button
            Expected Result: DMI displays the Driver ID window.
            The enabled Close button NA11 is display in area G
            Test Step Comment: (1) MMI_gen 4392 (partly: bullet a, close button, NA11); MMI_gen 4396 (partly: close, NA11); 
            */
            DmiActions.ShowInstruction(this, "Press the ‘Driver ID’ button");

            DmiActions.Set_Driver_ID(this, Telegrams.DMItoEVC.EVC104_MMINewDriverData.Get_X_DRIVER_ID);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Driver ID  window." + Environment.NewLine +
                                @"2. The ‘Close’ button NA11 is displayed enabled in area G.");
            TraceHeader("Test Step 7");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Perform the following procedure,");
            TraceReport("Expected Result");
            TraceInfo("DMI displays the first page of  the  train data entry.");
            /*
            Test Step 7
            Action: Perform the following procedure,
            Press ‘Close’ button.
            Press ‘Train data’ button
            Expected Result: DMI displays the first page of  the  train data entry.
            The enabled Close button NA 11 is display in area G.
            The enabled Next button NA17 is display in area G.
            The disabled Previous button NA19 is display in area G
            Test Step Comment: (1) MMI_gen 4392 (partly: bullet a, close button, NA11);(2) MMI_gen 4392 (partly: bullet c, next button, NA17);(3) MMI_gen 4394 (partly: previous, disabled); MMI_gen 4396 (partly: previous, NA19); MMI_gen 4392 (partly: bullet d, previous button);
            */

            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button.");

            DmiActions.Display_Main_Window_with_Start_button_not_enabled(this);
            DmiActions.ShowInstruction(this, @"Press ‘Train data’ button");

            DmiActions.Display_Fixed_Train_Data_Window(this);
            DmiExpectedResults.Train_data_window_displayed(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the first page of Train data entry." + Environment.NewLine +
                                @"2. The ‘Close’ button NA11 is displayed enabled in area G." + Environment.NewLine +
                                @"3. The ‘Next’ button NA17 is displayed enabled in area G." + Environment.NewLine +
                                @"3. The ‘Previous’ button NA19 is displayed disabled in area G.");
            TraceHeader("Test Step 8");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Press and hold Next button");
            TraceReport("Expected Result");
            TraceInfo("Verify the following information,");
            /*
            Test Step 8
            Action: Press and hold Next button
            Expected Result: Verify the following information,
            The Next button is shown as pressed state.
            The sound ‘click’ is played once
            Test Step Comment: (1) MMI_gen 9391 (partly: Next, MMI_gen 4381 (partly: change to state ‘Pressed’ as long as remain actuated));
            */
            DmiActions.ShowInstruction(this, @"Press and hold the ‘Next’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘Next’ button NA17 is displayed pressed." + Environment.NewLine +
                                "2. The ‘click’ sound is played once.");

            TraceHeader("Test Step 9");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Slide out Next button");
            TraceReport("Expected Result");
            TraceInfo("Verify the following information,");
            /*
            Test Step 9
            Action: Slide out Next button
            Expected Result: Verify the following information,
            The Next button becomes the ‘Enabled’ state without a sound
            Test Step Comment: (1) MMI_gen 9391 (partly: Next, MMI_gen 4382 (partly: state ‘Enabled’ when slide out with force applied, no sound));
            */
            DmiActions.ShowInstruction(this, @"Whilst keeping the ‘Next’ button pressed, drag outside its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘Next’ button NA17 is displayed enabled." + Environment.NewLine +
                                "2. No sound is played.");

            TraceHeader("Test Step 10");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Slide back into Next button");
            TraceReport("Expected Result");
            TraceInfo("Verify the following information,");
            /*
            Test Step 10
            Action: Slide back into Next button
            Expected Result: Verify the following information,
            The Next button is shown as pressed state and no sound ‘Click’ is played
            Test Step Comment: (1) MMI_gen 9391 (partly: Next, MMI_gen 4382 (partly: state ‘Pressed’ when slide back, no sound));
            */
            DmiActions.ShowInstruction(this, @"Whilst keeping the ‘Next’ button pressed, drag it back inside its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Next’ button NA17 is displayed pressed." + Environment.NewLine +
                                "2. No sound is played.");

            TraceHeader("Test Step 11");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Release Next button");
            TraceReport("Expected Result");
            TraceInfo("Verify the following information,");
            /*
            Test Step 11
            Action: Release Next button
            Expected Result: Verify the following information,
            DMI display the next page of Train data window
            Test Step Comment: (1) MMI_gen 9391 (partly: Next, MMI_gen 4381 (partly: exit state ‘Pressed’, execute function associated to the button));
            */
            DmiActions.ShowInstruction(this, @"Release the ‘Next’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the next page of the Train data window.");

            TraceHeader("Test Step 12");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Press the next button until the last page of Train data window");
            TraceReport("Expected Result");
            TraceInfo("DMI displays the last page of Train data window.");
            /*
            Test Step 12
            Action: Press the next button until the last page of Train data window
            Expected Result: DMI displays the last page of Train data window.
            Verify the following information,The disabled Next button NA18.2 is display in area G. 
            The enalbed Previous button NA18 is display in area G
            Test Step Comment: (1) MMI_gen 4394 (partly: next, disabled); MMI_gen 4396 (partly: next, NA18.2);(2) MMI_gen 4392 (partly: bullet d, previous button, NA18); MMI_gen 4396 (partly: previous, NA18);
            */
            DmiActions.ShowInstruction(this,
                @"Press the ‘Next’ button repeatedly until the Train data window does not change");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the last page of the Train data window." + Environment.NewLine +
                                @"2. The ‘Next’ button NA18.2 is displayed disabled in area G" + Environment.NewLine +
                                @"2. The ‘Previous’ button NA18 is displayed enabled in area G");

            TraceHeader("Test Step 13");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Press and hold Previous button");
            TraceReport("Expected Result");
            TraceInfo("Verify the following information,");
            /*
            Test Step 13
            Action: Press and hold Previous button
            Expected Result: Verify the following information,
            The Previous button is shown as pressed state.
            The sound ‘click’ is played once
            Test Step Comment: (1) MMI_gen 9391 (partly: Next, MMI_gen 4381 (partly: change to state ‘Pressed’ as long as remain actuated));
            */
            DmiActions.ShowInstruction(this, @"Press and hold the ‘Previous’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘Previous’ button is displayed pressed." + Environment.NewLine +
                                @"2. The ‘click’ sound is played once.");

            TraceHeader("Test Step 14");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Slide out Previous button");
            TraceReport("Expected Result");
            TraceInfo("Verify the following information,");
            /*
            Test Step 14
            Action: Slide out Previous button
            Expected Result: Verify the following information,
            The Previous button becomes the ‘Enabled’ state without a sound
            Test Step Comment: (1) MMI_gen 9391 (partly: Previous, MMI_gen 4382 (partly: state ‘Enabled’ when slide out with force applied, no sound));
            */
            DmiActions.ShowInstruction(this, @"Whilst keeping the ‘Previous’ button pressed, drag outside its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘Previous’ button is displayed enabled." + Environment.NewLine +
                                "2. No sound is played.");

            TraceHeader("Test Step 15");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Slide back into Previous button");
            TraceReport("Expected Result");
            TraceInfo("Verify the following information,");
            /*
            Test Step 15
            Action: Slide back into Previous button
            Expected Result: Verify the following information,
            The Previous button is shown as pressed state and no sound ‘Click’ is played
            Test Step Comment: (1) MMI_gen 9391 (partly: Previous, MMI_gen 4382 (partly: state ‘Pressed’ when slide back, no sound));
            */
            DmiActions.ShowInstruction(this,
                @"Whilst keeping the ‘Previous’ button pressed, drag it back inside its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Previous’ button is displayed pressed." + Environment.NewLine +
                                "2. No sound is played.");

            TraceHeader("Test Step 16");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Release Previous button");
            TraceReport("Expected Result");
            TraceInfo("Verify the following information,");
            /*
            Test Step 16
            Action: Release Previous button
            Expected Result: Verify the following information,
            DMI display the previous page of Train data window.
            The enabled Next button NA17 is display in area G
            Test Step Comment: (1) MMI_gen 9391 (partly: Previous, MMI_gen 4381 (partly: exit state ‘Pressed’, execute function associated to the button));(2) MMI_gen 4396 (partly: next, NA17);
            */
            DmiActions.ShowInstruction(this, @"Release the ‘Previous’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the previous page of the Train data window." + Environment.NewLine +
                                @"2. The ‘Next’ button NA17 is displayed enabled in area G.");

            TraceHeader("Test Step 17");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Press Close button");
            TraceReport("Expected Result");
            TraceInfo("DMI displays Main window");
            /*
            Test Step 17
            Action: Press Close button
            Expected Result: DMI displays Main window
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button");
            DmiActions.Display_Main_Window_with_Start_button_not_enabled(this);
            DmiExpectedResults.Main_Window_displayed(this, true);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Main window.");

            TraceHeader("Test Step 18");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("End of test");
            TraceReport("Expected Result");
            TraceInfo("");
            /*
            Test Step 18
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
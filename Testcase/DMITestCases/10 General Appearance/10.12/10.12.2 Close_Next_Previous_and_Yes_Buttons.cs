using System;
using Testcase.Telegrams.EVCtoDMI;
using Testcase.Telegrams.DMItoEVC;


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
        public override bool TestcaseEntryPoint()
        {
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 20732;
            // Testcase entrypoint

            MakeTestStepHeader(1, UniqueIdentifier++, "Activate cabin A", "DMI displays Driver ID window.");

            StartUp();

            /*
            Test Step 1
            Action: Activate cabin A
            Expected Result: DMI displays Driver ID window.
            Verify the following information,
            Delete button NA21 is shown as enabled state in area G.
            The disabled Close button NA12 is display in area G
            Test Step Comment: (1) MMI_gen 4392 (partly: bullet e, delete NA21); MMI_gen 4440 (partly: delete, enabled);(2) MMI_gen 4440 (partly: other navigation buttons); MMI_gen 4396 (partly: close, NA12); MMI_gen 4392 (partly: bullet a, close button);  MMI_gen 4395 (partly: close button, disabled);
            */

            DmiActions.Display_Driver_ID_Window(this);
            DmiActions.Send_SB_Mode(this);
            DmiExpectedResults.Driver_ID_window_displayed_in_SB_mode(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Driver ID window." + Environment.NewLine +
                                @"2. The ‘Delete’ button NA21 is displayed enabled in area G." + Environment.NewLine +
                                @"3. The ‘Close’ button NA12 is displayed disabled in area G.");

            MakeTestStepHeader(2, UniqueIdentifier++, "Enter the Driver ID ‘12345678’",
                "The value of an input field is displayed correspond with entered data");
            /*
            Test Step 2
            Action: Enter the Driver ID ‘12345678’
            Expected Result: The value of an input field is displayed correspond with entered data
            */
            DmiActions.ShowInstruction(this, "Enter the Driver ID ‘12345678’.");
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The Input Field is displayed with the value ‘1234 5678’");

            MakeTestStepHeader(3, UniqueIdentifier++, "Press and hold ‘Delete’ button up to 2 second.",
                "Verify the following information,");
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
            DmiActions.ShowInstruction(this, @"Press the ‘Delete’ button.");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘click’ sound is played once." + Environment.NewLine +
                                @"2. The ‘Delete’ button is displayed pressed and immediately changed to enabled." +
                                Environment.NewLine +
                                @"3. The last character (‘8’) of the Driver ID is deleted from the Input field");


            DmiActions.ShowInstruction(this, @"Press and hold the ‘Delete’ button for at least 2 seconds.");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘Delete’ button is displayed pressed and immediately changed to enabled, repeatedly." +
                                Environment.NewLine +
                                "2. Characters are repeatedly deleted from the end of the Input field." +
                                Environment.NewLine +
                                @"3. The ‘click’ sound is played repeatedly while the ‘Delete’ button is held.");

            MakeTestStepHeader(4, UniqueIdentifier++, "Release the pressed button", "Verify the following information,");
            /*
            Test Step 4
            Action: Release the pressed button
            Expected Result: Verify the following information,
            The state of pressed button is changed to ‘Enabled’ state
            Test Step Comment: (1) MMI_gen 4393 (partly: delete, MMI_gen 4384 (partly: ETCS-MMI’s function associated to the button));
            */
            DmiActions.ShowInstruction(this, "Release the 'Delete' button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘Delete’ button is displayed enabled.");

            MakeTestStepHeader(5, UniqueIdentifier++, "Perform the following procedure,", "DMI displays the Main window");
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

            MakeTestStepHeader(6, UniqueIdentifier++, "Press ‘Driver ID’ button", "DMI displays the Driver ID window.");
            /*
            Test Step 6
            Action: Press ‘Driver ID’ button
            Expected Result: DMI displays the Driver ID window.
            The enabled Close button NA11 is display in area G
            Test Step Comment: (1) MMI_gen 4392 (partly: bullet a, close button, NA11); MMI_gen 4396 (partly: close, NA11); 
            */
            DmiActions.ShowInstruction(this, "Press the ‘Driver ID’ button");

            EVC101_MMIDriverRequest.CheckMRequestPressed = Variables.MMI_M_REQUEST.ChangeDriverIdentity;

            DmiActions.Display_Driver_ID_Window(this, EVC14_MMICurrentDriverID.MMI_X_DRIVER_ID);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Driver ID window." + Environment.NewLine +
                                @"2. The ‘Close’ button NA11 is displayed enabled in area G.");
            MakeTestStepHeader(7, UniqueIdentifier++, "Perform the following procedure,",
                "DMI displays the first page of  the  train data entry.");
            /*
            Test Step 7
            Action: Perform the following procedure,
            Press ‘Close’ button.
            Press ‘Train data’ button
            Expected Result: DMI displays the first page of the train data entry.
            The enabled Close button NA 11 is display in area G.
            The enabled Next button NA17 is display in area G.
            The disabled Previous button NA19 is display in area G
            Test Step Comment: (1) MMI_gen 4392 (partly: bullet a, close button, NA11);(2) MMI_gen 4392 (partly: bullet c, next button, NA17);(3) MMI_gen 4394 (partly: previous, disabled); MMI_gen 4396 (partly: previous, NA19); MMI_gen 4392 (partly: bullet d, previous button);
            */

            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button.");

            DmiActions.Display_Main_Window_with_Start_button_not_enabled(this);
            DmiActions.ShowInstruction(this, @"Press ‘Train data’ button");

            DmiActions.Display_Flexible_Train_Data_Window(this);

            DmiExpectedResults.Train_data_window_displayed(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the first page of Train data entry." + Environment.NewLine +
                                @"2. The ‘Close’ button NA11 is displayed enabled in area G." + Environment.NewLine +
                                @"3. The ‘Next’ button NA17 is displayed enabled in area G." + Environment.NewLine +
                                @"3. The ‘Previous’ button NA19 is displayed disabled in area G.");
            MakeTestStepHeader(8, UniqueIdentifier++, "Press and hold Next button",
                "Verify the following information,");
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

            MakeTestStepHeader(9, UniqueIdentifier++, "Slide out Next button", "Verify the following information,");
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

            MakeTestStepHeader(10, UniqueIdentifier++, "Slide back into Next button",
                "Verify the following information,");
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

            MakeTestStepHeader(11, UniqueIdentifier++, "Release Next button", "Verify the following information,");
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

            MakeTestStepHeader(12, UniqueIdentifier++, "Press the next button until the last page of Train data window",
                "DMI displays the last page of Train data window.");
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

            MakeTestStepHeader(13, UniqueIdentifier++, "Press and hold Previous button",
                "Verify the following information,");
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

            MakeTestStepHeader(14, UniqueIdentifier++, "Slide out Previous button",
                "Verify the following information,");
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

            MakeTestStepHeader(15, UniqueIdentifier++, "Slide back into Previous button",
                "Verify the following information,");
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

            MakeTestStepHeader(16, UniqueIdentifier++, "Release Previous button", "Verify the following information,");
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

            MakeTestStepHeader(17, UniqueIdentifier++, "Press Close button", "DMI displays Main window");
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

            TraceHeader("End of test");
            /*
            Test Step 18
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
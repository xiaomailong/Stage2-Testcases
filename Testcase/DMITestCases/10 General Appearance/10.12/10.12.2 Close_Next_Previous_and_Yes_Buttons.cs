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
            EVC0_MMIStartATP.Evc0Type = EVC0_MMIStartATP.EVC0Type.GoToIdle;
            EVC0_MMIStartATP.Send();
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
            // Testcase entrypoint

            /*
            Test Step 1
            Action: Activate cabin A
            Expected Result: DMI displays Driver ID window.Verify the following information,Delete button NA21 is shown as enabled state in area G.The disabled Close button NA12 is display in area G
            Test Step Comment: (1) MMI_gen 4392 (partly: bullet e, delete NA21); MMI_gen 4440 (partly: delete, enabled);(2) MMI_gen 4440 (partly: other navigation buttons); MMI_gen 4396 (partly: close, NA12); MMI_gen 4392 (partly: bullet a, close button);  MMI_gen 4395 (partly: close button, disabled);
            */
            // Call generic Action Method
            DmiActions.Activate_Cabin_1(this);
            EVC14_MMICurrentDriverID.MMI_Q_ADD_ENABLE = EVC14_MMICurrentDriverID.MMI_Q_ADD_ENABLE_BUTTONS.Settings;
            EVC14_MMICurrentDriverID.MMI_Q_CLOSE_ENABLE = Variables.MMI_Q_CLOSE_ENABLE.Enabled;
            EVC14_MMICurrentDriverID.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Driver ID window." + Environment.NewLine +
                                @"2. The ‘Delete’ button NA21 is displayed enabled in area G." + Environment.NewLine +
                                @"3. The ‘Close’ button NA12 is displayed disabled in area G.");

            /*
            Test Step 2
            Action: Enter the Driver ID ‘12345678’
            Expected Result: The value of an input field is displayed correspond with entered data
            */
            DmiActions.ShowInstruction(this, @"Enter the Driver ID ‘12345678’");
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The Input Field is displayed with the value ‘1234 5678’");

            /*
            Test Step 3
            Action: Press and hold ‘Delete’ button up to 2 second.Note:Stopwatch is required
            Expected Result: Verify the following information,While press and hold button less than 1.5 secSound ‘Click’ is played once.The state of button is changed to ‘Pressed’ and immediately back to ‘Enabled’ state.The last character of Driver ID is  deleted from the input field.While press and hold button over 1.5 secThe state ‘pressed’ and ‘released’ are switched repeatly while button is pressed and the characters are removed from an input field repeatly refer to pressed state.The sound ‘Click’ is played repeatly while button is pressed
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

            /*
            Test Step 4
            Action: Release the pressed button
            Expected Result: Verify the following information,The state of pressed button is changed to ‘Enabled’ state
            Test Step Comment: (1) MMI_gen 4393 (partly: delete, MMI_gen 4384 (partly: ETCS-MMI’s function associated to the button));
            */
            DmiActions.ShowInstruction(this, "Release the pressed button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘Delete’ button is displayed enabled.");

            /*
            Test Step 5
            Action: Perform the following procedure,Enter and confirm Driver IDPerform brake testSelect and confirm Level 1
            Expected Result: DMI displays the Main window
            */
            DmiActions.ShowInstruction(this, "Enter and accept Driver ID");

            EVC20_MMISelectLevel.MMI_Q_CLOSE_ENABLE = Variables.MMI_Q_CLOSE_ENABLE.Disabled;
            EVC20_MMISelectLevel.MMI_Q_LEVEL_NTC_ID = new Variables.MMI_Q_LEVEL_NTC_ID[]
                {Variables.MMI_Q_LEVEL_NTC_ID.ETCS_Level};
            EVC20_MMISelectLevel.MMI_M_CURRENT_LEVEL = new Variables.MMI_M_CURRENT_LEVEL[]
                {Variables.MMI_M_CURRENT_LEVEL.NotLastUsedLevel};
            EVC20_MMISelectLevel.MMI_M_LEVEL_FLAG = new Variables.MMI_M_LEVEL_FLAG[]
                {Variables.MMI_M_LEVEL_FLAG.MarkedLevel};
            EVC20_MMISelectLevel.MMI_M_INHIBITED_LEVEL = new Variables.MMI_M_INHIBITED_LEVEL[]
                {Variables.MMI_M_INHIBITED_LEVEL.NotInhibited};
            EVC20_MMISelectLevel.MMI_M_INHIBIT_ENABLE = new Variables.MMI_M_INHIBIT_ENABLE[]
                {Variables.MMI_M_INHIBIT_ENABLE.AllowedForInhibiting};
            EVC20_MMISelectLevel.MMI_M_LEVEL_NTC_ID = new Variables.MMI_M_LEVEL_NTC_ID[]
                {Variables.MMI_M_LEVEL_NTC_ID.L1};
            EVC20_MMISelectLevel.Send();

            DmiActions.ShowInstruction(this, " Select and accept Level 1");

            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Main;
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH =
                EVC30_MMIRequestEnable.EnabledRequests.Start | Variables.standardFlags;
            EVC30_MMIRequestEnable.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Main window.");

            /*
            Test Step 6
            Action: Press ‘Driver ID’ button
            Expected Result: DMI displays the Driver ID window.The enabled Close button NA11 is display in area G
            Test Step Comment: (1) MMI_gen 4392 (partly: bullet a, close button, NA11); MMI_gen 4396 (partly: close, NA11); 
            */
            DmiActions.ShowInstruction(this, "Press the ‘Driver ID’ button");

            EVC14_MMICurrentDriverID.MMI_Q_ADD_ENABLE = EVC14_MMICurrentDriverID.MMI_Q_ADD_ENABLE_BUTTONS.Settings;
            EVC14_MMICurrentDriverID.MMI_Q_CLOSE_ENABLE = Variables.MMI_Q_CLOSE_ENABLE.Enabled;
            EVC14_MMICurrentDriverID.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Driver ID  window." + Environment.NewLine +
                                @"2. The ‘Close’ button NA11 is displayed enabled in area G.");
            /*
            Test Step 7
            Action: Perform the following procedure,Press ‘Close’ button.Press ‘Train data’ button
            Expected Result: DMI displays the first page of  the  train data entry.The enabled Close button NA 11 is display in area G.The enabled Next button NA17 is display in area G.The disabled Previous button NA19 is display in area G
            Test Step Comment: (1) MMI_gen 4392 (partly: bullet a, close button, NA11);(2) MMI_gen 4392 (partly: bullet c, next button, NA17);(3) MMI_gen 4394 (partly: previous, disabled); MMI_gen 4396 (partly: previous, NA19); MMI_gen 4392 (partly: bullet d, previous button);
            */
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Main;
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.TrainData;
            EVC30_MMIRequestEnable.Send();

            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button. Press the ‘Train data’ button");

            DmiActions.Send_EVC6_MMICurrentTrainData_FixedDataEntry(this, new[] {"FLU", "RLU", "Rescue"}, 2);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the first page of Train data entry." + Environment.NewLine +
                                @"2. The ‘Close’ button NA11 is displayed enabled in area G." + Environment.NewLine +
                                @"3. The ‘Next’ button NA17 is displayed enabled in area G." + Environment.NewLine +
                                @"3. The ‘Previous’ button NA19 is displayed disabled in area G.");
            /*
            Test Step 8
            Action: Press and hold Next button
            Expected Result: Verify the following information,The Next button is shown as pressed state.The sound ‘click’ is played once
            Test Step Comment: (1) MMI_gen 9391 (partly: Next, MMI_gen 4381 (partly: change to state ‘Pressed’ as long as remain actuated));
            */
            DmiActions.ShowInstruction(this, @"Press and hold the ‘Next’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘Next’ button NA17 is displayed pressed." + Environment.NewLine +
                                "2. The ‘click’ sound is played once.");

            /*
            Test Step 9
            Action: Slide out Next button
            Expected Result: Verify the following information,The Next button becomes the ‘Enabled’ state without a sound
            Test Step Comment: (1) MMI_gen 9391 (partly: Next, MMI_gen 4382 (partly: state ‘Enabled’ when slide out with force applied, no sound));
            */
            DmiActions.ShowInstruction(this, @"Whilst keeping the ‘Next’ button pressed, drag outside its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘Next’ button NA17 is displayed enabled." + Environment.NewLine +
                                "2. No sound is played.");

            /*
            Test Step 10
            Action: Slide back into Next button
            Expected Result: Verify the following information,The Next button is shown as pressed state and no sound ‘Click’ is played
            Test Step Comment: (1) MMI_gen 9391 (partly: Next, MMI_gen 4382 (partly: state ‘Pressed’ when slide back, no sound));
            */
            DmiActions.ShowInstruction(this, @"Whilst keeping the ‘Next’ button pressed, drag it back inside its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Next’ button NA17 is displayed pressed." + Environment.NewLine +
                                "2. No sound is played.");

            /*
            Test Step 11
            Action: Release Next button
            Expected Result: Verify the following information,DMI display the next page of Train data window
            Test Step Comment: (1) MMI_gen 9391 (partly: Next, MMI_gen 4381 (partly: exit state ‘Pressed’, execute function associated to the button));
            */
            DmiActions.ShowInstruction(this, @"Release the ‘Next’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the next page of the Train data window.");

            /*
            Test Step 12
            Action: Press the next button until the last page of Train data window
            Expected Result: DMI displays the last page of Train data window.Verify the following information,The disabled Next button NA18.2 is display in area G. The enalbed Previous button NA18 is display in area G
            Test Step Comment: (1) MMI_gen 4394 (partly: next, disabled); MMI_gen 4396 (partly: next, NA18.2);(2) MMI_gen 4392 (partly: bullet d, previous button, NA18); MMI_gen 4396 (partly: previous, NA18);
            */
            DmiActions.ShowInstruction(this,
                @"Press the ‘Next’ button repeatedly until the Train data window does not change");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the last page of the Train data window." + Environment.NewLine +
                                @"2. The ‘Next’ button NA18.2 is displayed disabled in area G" + Environment.NewLine +
                                @"2. The ‘Previous’ button NA18 is displayed enabled in area G");

            /*
            Test Step 13
            Action: Press and hold Previous button
            Expected Result: Verify the following information,The Previous button is shown as pressed state.The sound ‘click’ is played once
            Test Step Comment: (1) MMI_gen 9391 (partly: Next, MMI_gen 4381 (partly: change to state ‘Pressed’ as long as remain actuated));
            */
            DmiActions.ShowInstruction(this, @"Press and hold the ‘Previous’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘Previous’ button is displayed pressed." + Environment.NewLine +
                                @"2. The ‘click’ sound is played once.");

            /*
            Test Step 14
            Action: Slide out Previous button
            Expected Result: Verify the following information,The Previous button becomes the ‘Enabled’ state without a sound
            Test Step Comment: (1) MMI_gen 9391 (partly: Previous, MMI_gen 4382 (partly: state ‘Enabled’ when slide out with force applied, no sound));
            */
            DmiActions.ShowInstruction(this, @"Whilst keeping the ‘Previous’ button pressed, drag outside its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘Previous’ button is displayed enabled." + Environment.NewLine +
                                "2. No sound is played.");

            /*
            Test Step 15
            Action: Slide back into Previous button
            Expected Result: Verify the following information,The Previous button is shown as pressed state and no sound ‘Click’ is played
            Test Step Comment: (1) MMI_gen 9391 (partly: Previous, MMI_gen 4382 (partly: state ‘Pressed’ when slide back, no sound));
            */
            DmiActions.ShowInstruction(this,
                @"Whilst keeping the ‘Previous’ button pressed, drag it back inside its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Previous’ button is displayed pressed." + Environment.NewLine +
                                "2. No sound is played.");

            /*
            Test Step 16
            Action: Release Previous button
            Expected Result: Verify the following information,DMI display the previous page of Train data window.The enabled Next button NA17 is display in area G
            Test Step Comment: (1) MMI_gen 9391 (partly: Previous, MMI_gen 4381 (partly: exit state ‘Pressed’, execute function associated to the button));(2) MMI_gen 4396 (partly: next, NA17);
            */
            DmiActions.ShowInstruction(this, @"Release the ‘Previous’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the previous page of the Train data window." + Environment.NewLine +
                                @"2. The ‘Next’ button NA17 is displayed enabled in area G.");

            /*
            Test Step 17
            Action: Press Close button
            Expected Result: DMI displays Main window
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Main window.");

            /*
            Test Step 18
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
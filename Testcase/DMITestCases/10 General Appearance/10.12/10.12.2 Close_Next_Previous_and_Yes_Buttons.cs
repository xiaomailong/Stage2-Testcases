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
    public class Close_Next_Previous_and_Yes_Buttons : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // Test system is powered on.

            // Call the TestCaseBase PreExecution
            base.PreExecution();
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
            DmiActions.Activate_cabin_A(this);


            /*
            Test Step 2
            Action: Enter the Driver ID ‘12345678’
            Expected Result: The value of an input field is displayed correspond with entered data
            */


            /*
            Test Step 3
            Action: Press and hold ‘Delete’ button up to 2 second.Note:Stopwatch is required
            Expected Result: Verify the following information,While press and hold button less than 1.5 secSound ‘Click’ is played once.The state of button is changed to ‘Pressed’ and immediately back to ‘Enabled’ state.The last character of Driver ID is  deleted from the input field.While press and hold button over 1.5 secThe state ‘pressed’ and ‘released’ are switched repeatly while button is pressed and the characters are removed from an input field repeatly refer to pressed state.The sound ‘Click’ is played repeatly while button is pressed
            Test Step Comment: (1) MMI_gen 4393 (partly: delete, MMI_gen 4384 (partly: sound ‘Click’));(2) MMI_gen 4393 (partly: MMI_gen 4384 (partly: Change to state ‘Pressed’ and immediately back to state ‘Enabled’));(3) MMI_gen 4393 (partly: delete, MMI_gen 4384 (partly: ETCS-MMI’s function associated to the button));(4) MMI_gen 4393 (partly: delete, MMI_gen 4386 (partly: visual of repeat function));(5) MMI_gen 4393 (partly: delete, MMI_gen 4386 (partly: audible of repeat function));
            */


            /*
            Test Step 4
            Action: Release the pressed button
            Expected Result: Verify the following information,The state of pressed button is changed to ‘Enabled’ state
            Test Step Comment: (1) MMI_gen 4393 (partly: delete, MMI_gen 4384 (partly: ETCS-MMI’s function associated to the button));
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(@"Release the pressed button");
            // Call generic Check Results Method
            DmiExpectedResults
                .Verify_the_following_information_The_state_of_pressed_button_is_changed_to_Enabled_state(this);


            /*
            Test Step 5
            Action: Perform the following procedure,Enter and confirm Driver IDPerform brake testSelect and confirm Level 1
            Expected Result: DMI displays the Main window
            */
            // Call generic Check Results Method
            DmiExpectedResults.DMI_displays_the_Main_window(this);


            /*
            Test Step 6
            Action: Press ‘Driver ID’ button
            Expected Result: DMI displays the Driver ID window.The enabled Close button NA11 is display in area G
            Test Step Comment: (1) MMI_gen 4392 (partly: bullet a, close button, NA11); MMI_gen 4396 (partly: close, NA11); 
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(@"Press ‘Driver ID’ button");


            /*
            Test Step 7
            Action: Perform the following procedure,Press ‘Close’ button.Press ‘Train data’ button
            Expected Result: DMI displays the first page of  the  train data entry.The enabled Close button NA 11 is display in area G.The enabled Next button NA17 is display in area G.The disabled Previous button NA19 is display in area G
            Test Step Comment: (1) MMI_gen 4392 (partly: bullet a, close button, NA11);(2) MMI_gen 4392 (partly: bullet c, next button, NA17);(3) MMI_gen 4394 (partly: previous, disabled); MMI_gen 4396 (partly: previous, NA19); MMI_gen 4392 (partly: bullet d, previous button);
            */


            /*
            Test Step 8
            Action: Press and hold Next button
            Expected Result: Verify the following information,The Next button is shown as pressed state.The sound ‘click’ is played once
            Test Step Comment: (1) MMI_gen 9391 (partly: Next, MMI_gen 4381 (partly: change to state ‘Pressed’ as long as remain actuated));
            */


            /*
            Test Step 9
            Action: Slide out Next button
            Expected Result: Verify the following information,The Next button becomes the ‘Enabled’ state without a sound
            Test Step Comment: (1) MMI_gen 9391 (partly: Next, MMI_gen 4382 (partly: state ‘Enabled’ when slide out with force applied, no sound));
            */


            /*
            Test Step 10
            Action: Slide back into Next button
            Expected Result: Verify the following information,The Next button is shown as pressed state and no sound ‘Click’ is played
            Test Step Comment: (1) MMI_gen 9391 (partly: Next, MMI_gen 4382 (partly: state ‘Pressed’ when slide back, no sound));
            */


            /*
            Test Step 11
            Action: Release Next button
            Expected Result: Verify the following information,DMI display the next page of Train data window
            Test Step Comment: (1) MMI_gen 9391 (partly: Next, MMI_gen 4381 (partly: exit state ‘Pressed’, execute function associated to the button));
            */


            /*
            Test Step 12
            Action: Press the next button until the last page of Train data window
            Expected Result: DMI displays the last page of Train data window.Verify the following information,The disabled Next button NA18.2 is display in area G. The enalbed Previous button NA18 is display in area G
            Test Step Comment: (1) MMI_gen 4394 (partly: next, disabled); MMI_gen 4396 (partly: next, NA18.2);(2) MMI_gen 4392 (partly: bullet d, previous button, NA18); MMI_gen 4396 (partly: previous, NA18);
            */


            /*
            Test Step 13
            Action: Press and hold Previous button
            Expected Result: Verify the following information,The Previous button is shown as pressed state.The sound ‘click’ is played once
            Test Step Comment: (1) MMI_gen 9391 (partly: Next, MMI_gen 4381 (partly: change to state ‘Pressed’ as long as remain actuated));
            */


            /*
            Test Step 14
            Action: Slide out Previous button
            Expected Result: Verify the following information,The Previous button becomes the ‘Enabled’ state without a sound
            Test Step Comment: (1) MMI_gen 9391 (partly: Previous, MMI_gen 4382 (partly: state ‘Enabled’ when slide out with force applied, no sound));
            */


            /*
            Test Step 15
            Action: Slide back into Previous button
            Expected Result: Verify the following information,The Previous button is shown as pressed state and no sound ‘Click’ is played
            Test Step Comment: (1) MMI_gen 9391 (partly: Previous, MMI_gen 4382 (partly: state ‘Pressed’ when slide back, no sound));
            */


            /*
            Test Step 16
            Action: Release Previous button
            Expected Result: Verify the following information,DMI display the previous page of Train data window.The enabled Next button NA17 is display in area G
            Test Step Comment: (1) MMI_gen 9391 (partly: Previous, MMI_gen 4381 (partly: exit state ‘Pressed’, execute function associated to the button));(2) MMI_gen 4396 (partly: next, NA17);
            */


            /*
            Test Step 17
            Action: Press Close button
            Expected Result: DMI displays Main window
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(@"Press Close button");
            // Call generic Check Results Method
            DmiExpectedResults.DMI_displays_Main_window(this);


            /*
            Test Step 18
            Action: End of test
            Expected Result: 
            */


            return GlobalTestResult;
        }
    }
}
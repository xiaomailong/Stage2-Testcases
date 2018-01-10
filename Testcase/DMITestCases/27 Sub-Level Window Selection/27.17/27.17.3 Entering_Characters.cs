using System;
using Testcase.Telegrams.EVCtoDMI;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 27.17.3 Entering Characters
    /// TC-ID: 7.3.2
    /// 
    /// This test case verifies the property of entering characters of the Driver ID window. The property of entering characters shall comply with [ERA-ERTMS] standard.
    /// 
    /// Tested Requirements:
    /// MMI_gen 8033 (partly: MMI_gen 4689, MMI_gen 4690, MMI_gen 4691 (partly: flashing), MMI_gen 4692, MMI_gen 4693, MMI_gen 4694 (partly: MMI_gen 4246, MMI_gen 4247), MMI_gen 4913 (partly: MMI_gen 4384, MMI_gen 4386 (partly: except 0.3s))); MMI_gen 4393 (partly: [Delete]);
    /// 
    /// Scenario:
    /// The test system is powered on and the cabin is activated.The properties of entering characters in Driver ID window is verified.The Down-type button on keypad is verified.
    /// 
    /// Used files:
    /// N/A
    /// </summary>
    public class TC_ID_7_3_2_Entering_Characters : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:

            // Call the TestCaseBase PreExecution
            base.PreExecution();

            // System is power on.
            DmiActions.Start_ATP();
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
            // Testcase entrypoint

            /*
            Test Step 1
            Action: Activate cabin A
            Expected Result: DMI displays Driver ID window.Verify the following information,The flashing horizontal-line cursor is always flashed in the next position of the echoed entered-data key in the ‘Selected IF/value of pressed key(s)’ data input field when selected the next character it will be inserted cursor position
            Test Step Comment: (1) MMI_gen 8033 (partly: MMI_gen 4689, MMI_gen 4690, MMI_gen 4691 (partly: flashing));
            */
            DmiActions.Activate_Cabin_1(this);

            EVC14_MMICurrentDriverID.MMI_X_DRIVER_ID = "";
            EVC14_MMICurrentDriverID.MMI_Q_ADD_ENABLE = (EVC14_MMICurrentDriverID.MMI_Q_ADD_ENABLE_BUTTONS) 0;
            EVC14_MMICurrentDriverID.MMI_Q_CLOSE_ENABLE = Variables.MMI_Q_CLOSE_ENABLE.Disabled;
            EVC14_MMICurrentDriverID.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Driver ID window." + Environment.NewLine +
                                "2. A flashing underscore is displayed as a cursor in the data input field (at the left side).");

            /*
            Test Step 2
            Action: Press the data key which have only single character once.Note: This step is for testing ‘1’ and ‘0’ button
            Expected Result: Verify the following information,The pressed key is added in an input field immediately.The cursor is jumped to next position after entered the character immediately
            Test Step Comment: (1) MMI_gen 8033 (partly: MMI_gen 4693 (a));  (2) MMI_gen 8033 (partly: MMI_gen 4692);  
            */
            DmiActions.ShowInstruction(this, "Press the <1> key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The data input field immediately displays ‘1’." + Environment.NewLine +
                                "2. A flashing underscore is displayed after the ‘1’ in the data input field.");

            DmiActions.ShowInstruction(this, "Press the <0> key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The data input field immediately displays ‘10’." + Environment.NewLine +
                                "2. A flashing underscore is displayed after the ‘0’ in the data input field.");

            /*
            Test Step 3
            Action: Press and hold the data key which have only single character.Note: This step is for testing ‘1’ and ‘0’ button.Press the ‘Del’ button to delete an information when entered data is out of input field range is acceptable
            Expected Result: Verify the following information,Sound ‘Click’ is played once.The state of button is changed to ‘Pressed’ and immediately back to ‘Enabled’ state.Pressed key is added in an input field after pressing the button
            Test Step Comment: (1) MMI_gen 8033 (partly: MMI_gen 4913 (partly: MMI_gen 4384 (partly: sound ‘Click’)));(2) MMI_gen 8033 (partly: MMI_gen 4913 (partly: MMI_gen 4384 (partly: Change to state ‘Pressed’ and immediately back to state ‘Enabled’)));   (3) MMI_gen 8033 (partly: MMI_gen 4913 (partly: MMI_gen 4384 (partly: ETCS-MMI’s function associated to the button)));
            */
            DmiActions.ShowInstruction(this, "Press and hold the <1> key for more than 1.5s");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The key is displayed pressed and immediately re-displayed enabled." +
                                Environment.NewLine +
                                "2. The ‘Click’ sound is played once." + Environment.NewLine +
                                "3. The data input field displays ‘101’." + Environment.NewLine +
                                "4. After 1.5s, the data input field accepts another ‘1’ repeatedly, displaying ‘1011’, then 1011 1 and so on." +
                                Environment.NewLine +
                                "5. When the data input field has displayed ‘1011 1111’ and ‘1111 1111’ (over 2 lines) no more characters are accepted.");

            DmiActions.ShowInstruction(this, "Press the <Del> key until the input field is blank");

            /*
            Test Step 4
            Action: Released the pressed button
            Expected Result: Verify the following information, The character is stop adding
            Test Step Comment: (1) MMI_gen 8033 (partly: MMI_gen 4913 (partly: MMI_gen 4384 (partly: ETCS-MMI’s function associated to the button)));
            */
            DmiActions.ShowInstruction(this, @"Press and hold the <0> key for 3s and then release the key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The data input field displays a series of ‘0’ characters." + Environment.NewLine +
                                "2. After releasing the key, no more ‘0’ characters are added to the data input field.");

            /*
            Test Step 5
            Action: Press and hold ‘Del’ button.Note: Stopwatch is required
            Expected Result: Verify the following information,While press and hold button less than 1.5 secSound ‘Click’ is played once.The state of button is changed to ‘Pressed’ and immediately back to ‘Enabled’ state.The last character is removed from an input field after pressing the button.While press and hold button over 1.5 secThe state ‘pressed’ and ‘released’ are switched repeatly while button is pressed and the characters are removed from an input field repeatly refer to pressed state.The sound ‘Click’ is played repeatly while button is pressed
            Test Step Comment: (1) MMI_gen 8033 (partly: MMI_gen 4913 (partly: MMI_gen 4384 (partly: sound ‘Click)));(2) MMI_gen 8033 (partly: MMI_gen 4913 (partly: MMI_gen 4384 (partly: Change to state ‘Pressed’ and immediately back to state ‘Enabled’))); MMI_gen 4393 (partly: [Delete]);(3) MMI_gen 8033 (partly: MMI_gen 4913 (partly:MMI_gen 4384 (partly: ETCS-MMI’s function associated to the button)));(4) MMI_gen 8033 (partly: MMI_gen 4913 (partly:  MMI_gen 4386 (partly: visual of repeat function)));(5) MMI_gen 8033 (partly: MMI_gen 4913 (partly: MMI_gen 4386 (partly: audible of repeat function)));
            */
            DmiActions.ShowInstruction(this,
                @"Press and hold the ‘Del’ key for more than 1.5s. Note: Stopwatch is required");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Click’ sound is played once." + Environment.NewLine +
                                "2. The key is displayed pressed then immediately re-displayed enabled." +
                                Environment.NewLine +
                                "3. The last character is deleted from the data input field." + Environment.NewLine +
                                "4. After the key has been pressed for more than 1.5 s, the key is repeatedly displayed pressed and re-displayed enabled;" +
                                Environment.NewLine +
                                "5. The ‘Click’ sound is played repeatedly while the key is pressed and characters are deleted repeatedly from the end of the data input field.");

            /*
            Test Step 6
            Action: Release ‘Del’ button
            Expected Result: Verify the following information, The character is stop removing
            Test Step Comment: (1) MMI_gen 8033 (partly: MMI_gen 4913 (partly: MMI_gen 4384 (partly: ETCS-MMI’s function associated to the button)));
            */
            DmiActions.ShowInstruction(this, @"Release the ‘Del’ key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Characters stop being deleted from the end of the data input field.");

            /*
            Test Step 7
            Action: Press the data key which have various characters once.Note: This step is for testing ‘2’-‘9’ button.Stopwatch is required
            Expected Result: Verify the following information,The first character of the data key is added on an input field immediately. The cursor is jumped to next position after entered the character after 2 seconds
            Test Step Comment: (1) MMI_gen 8033 (parly: MMI_gen 4693 (a));(2) MMI_gen 8033 (parly: MMI_gen 4693 (b));
            */
            DmiActions.ShowInstruction(this,
                "Delete the value in the data input field, then press (and release) the <2> key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The data input field immediately displays ‘2’." + Environment.NewLine +
                                @"2. After 2s the cursor is re-displayed after the ‘2’");
            DmiActions.ShowInstruction(this,
                @"Delete the value in the data input field, then press (and release) the <2> key");

            // Repeat for the <3> key
            DmiActions.ShowInstruction(this, "Press (and release) the <3> key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The data input field immediately displays ‘23’." + Environment.NewLine +
                                @"2. After 2s the cursor is re-displayed after the ‘3’");

            // Repeat for the <4> key
            DmiActions.ShowInstruction(this, "Press (and release) the <4> key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The data input field immediately displays ‘234’." + Environment.NewLine +
                                @"2. After 2s the cursor is re-displayed after the ‘4’");

            // Repeat for the <5> key
            DmiActions.ShowInstruction(this, "Press (and release) the <5> key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The data input field immediately displays ‘2345’." + Environment.NewLine +
                                @"2. After 2s the cursor is re-displayed after the ‘5’");

            // Repeat for the <6> key
            DmiActions.ShowInstruction(this, "Press (and release) the <6> key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The data input field immediately displays ‘23456’." + Environment.NewLine +
                                @"2. After 2s the cursor is re-displayed after the ‘6’");

            // Repeat for the <7> key
            DmiActions.ShowInstruction(this, "Press (and release) the <7> key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The data input field immediately displays ‘2345 67’." + Environment.NewLine +
                                @"2. After 2s the cursor is re-displayed after the ‘7’");

            // Repeat for the <8> key
            DmiActions.ShowInstruction(this, "Press (and release) the <8> key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The data input field immediately displays ‘2345 678’." + Environment.NewLine +
                                @"2. After 2s the cursor is re-displayed after the ‘8’");

            // Repeat for the <9> key
            DmiActions.ShowInstruction(this, "Press (and release) the <9> key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The data input field immediately displays ‘2345 6789’." + Environment.NewLine +
                                @"2. After 2s the cursor is re-displayed after the ‘9’");

            /*
            Test Step 8
            Action: Press the same data key repeatly within these 2 seconds.Then, verify the displayed information at an input field while pressing button
            Expected Result: Verify the following information,The next character of the same data key is selected and displayed on an input field refer to the label of pressed button and wrap-around.For example, the single character is displayed at input field with following step ‘2’ -> ‘a’ -> ‘b’ -> ‘c’ -> ‘2’
            Test Step Comment: (1) MMI_gen 8033 (partly: MMI_gen 4693 (partly: (c), pressing the same data key));
            */
            DmiActions.ShowInstruction(this,
                "Delete the value in the data input field, then press the <2> key and press it again repeatedly within 2s");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Click’ sound is played once." + Environment.NewLine +
                                "2. The key is displayed pressed then immediately re-displayed enabled." +
                                Environment.NewLine +
                                "3. The data input field displays ‘2’, ‘a’, ‘b’, ‘c’, then ‘2’, ‘a’, and so on, as the key is re-pressed.");

            // Repeat for the <3> key
            DmiActions.ShowInstruction(this,
                "Delete the value in the data input field, then press the <3> key and press it again repeatedly within 2s");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Click’ sound is played once." + Environment.NewLine +
                                "2. The key is displayed pressed then immediately re-displayed enabled." +
                                Environment.NewLine +
                                "3. The data input field displays ‘3’, ‘d’, ‘e’, ‘f’, then ‘3’, ‘d’, and so on, as the key is re-pressed.");

            // Repeat for the <4> key
            DmiActions.ShowInstruction(this,
                "Delete the value in the data input field, then press the <4> key and press it again repeatedly within 2s");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Click’ sound is played once." + Environment.NewLine +
                                "2. The key is displayed pressed then immediately re-displayed enabled." +
                                Environment.NewLine +
                                "3. The data input field displays ‘4’, ‘g’, ‘h’, ‘i’, then ‘4’, ‘g’, and so on, as the key is re-pressed.");

            // Repeat for the <5> key
            DmiActions.ShowInstruction(this,
                "Delete the value in the data input field, then press the <5> key and press it again repeatedly within 2s");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Click’ sound is played once." + Environment.NewLine +
                                "2. The key is displayed pressed then immediately re-displayed enabled." +
                                Environment.NewLine +
                                "3. The data input field displays ‘5’, ‘j’, ‘k’, ‘l’, then ‘5’, ‘j’, and so on, as the key is re-pressed.");

            // Repeat for the <6> key
            DmiActions.ShowInstruction(this,
                "Delete the value in the data input field, then press the <6> key and press it again repeatedly within 2s");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Click’ sound is played once." + Environment.NewLine +
                                "2. The key is displayed pressed then immediately re-displayed enabled." +
                                Environment.NewLine +
                                "3. The data input field displays ‘6’, ‘m’, ‘n’, ‘o’, then ‘6’, ‘m’, and so on, as the key is re-pressed.");

            // Repeat for the <7> key
            DmiActions.ShowInstruction(this,
                "Delete the value in the data input field, then press the <7> key and press it again repeatedly within 2s");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Click’ sound is played once." + Environment.NewLine +
                                "2. The key is displayed pressed then immediately re-displayed enabled." +
                                Environment.NewLine +
                                "3. The data input field displays ‘7’, ‘p’, ‘q’, ‘r’, ‘s’,then ‘7’, ‘p’, and so on, as the key is re-pressed.");

            // Repeat for the <8> key
            DmiActions.ShowInstruction(this,
                "Delete the value in the data input field, then press the <8> key and press it again repeatedly within 2s");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Click’ sound is played once." + Environment.NewLine +
                                "2. The key is displayed pressed then immediately re-displayed enabled." +
                                Environment.NewLine +
                                "3. The data input field displays ‘8’, ‘t’, ‘u’, ‘v’, then ‘8’, ‘t’, and so on, as the key is re-pressed.");

            // Repeat for the <9> key
            DmiActions.ShowInstruction(this,
                "Delete the value in the data input field, then press the <9> key and press it again repeatedly within 2s");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Click’ sound is played once." + Environment.NewLine +
                                "2. The key is displayed pressed then immediately re-displayed enabled." +
                                Environment.NewLine +
                                "3. The data input field displays ‘9’, ‘w’, ‘x’, ‘y’, ‘z’, then ‘9’, ‘w’, and so on, as the key is re-pressed.");

            /*
            Test Step 9
            Action: Press and hold the same data key.Note: This step is for testing ‘’2’-‘9’ button.Stopwatch is required.Press the ‘Del’ button to delete an information when entered data is out of input field range is acceptable
            Expected Result: Verify the following information,While press and hold button less than 1.5 secSound ‘Click’ is played once.The state of button is changed to ‘Pressed’ and immediately back to ‘Enabled’ state.Pressed key is added in an input field after pressing the button.While press and hold button over 1.5 secThe next character of the same data key is selected and displayed on an input field refer to the label ot pressed button and wrap-around.For example, the single character is displayed at input field with following step ‘2’ -> ‘a’ -> ‘b’ -> ‘c’ -> ‘2’
            Test Step Comment: (1) MMI_gen 8033  (partly: MMI_gen 4913 (partly: MMI_gen 4384 (partly: sound ‘Click)));(2) MMI_gen 8033 (partly: MMI_gen 4913 (partly: MMI_gen 4384 (partly: Change to state ‘Pressed’ and immediately back to state ‘Enabled’)));   (3) MMI_gen 8033 (partly: MMI_gen 4913 (partly: MMI_gen 4384 (partly: ETCS-MMI’s function associated to the button)));(4) MMI_gen 8033 (partly: MMI_gen 4693 (partly: (c), hoding for some time));
            */
            DmiActions.ShowInstruction(this,
                "Delete the value in the data input field, then press the <2> key and hold it for more than 1.5s");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Click’ sound is played once." + Environment.NewLine +
                                "2. The key is displayed pressed then immediately re-displayed enabled." +
                                Environment.NewLine +
                                "3. The data input field immediately displays ‘2’." + Environment.NewLine +
                                "4. After 1.5s with the key still pressed the data input field displays ‘2’, ‘a’, ‘b’, ‘c’, then ‘2’, ‘a’, and so on, as the key is held.");

            // Repeat for the <3> key
            DmiActions.ShowInstruction(this,
                "Delete the value in the data input field, then press the <3> key and hold it for more than 1.5s");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Click’ sound is played once." + Environment.NewLine +
                                "2. The key is displayed pressed then immediately re-displayed enabled." +
                                Environment.NewLine +
                                "3. The data input field immediately displays ‘3’." + Environment.NewLine +
                                "4. After 1.5s with the key still pressed the data input field displays ‘3’, ‘d’, ‘e’, ‘f’, then ‘3’, ‘d’, and so on, as the key is held.");

            // Repeat for the <4> key
            DmiActions.ShowInstruction(this,
                "Delete the value in the data input field, then press the <4> key and hold it for more than 1.5s");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Click’ sound is played once." + Environment.NewLine +
                                "2. The key is displayed pressed then immediately re-displayed enabled." +
                                Environment.NewLine +
                                "3. The data input field immediately displays ‘4’." + Environment.NewLine +
                                "4. After 1.5s with the key still pressed the data input field displays ‘4’, ‘g’, ‘h’, ‘i’, then ‘4’, ‘g’, and so on, as the key is held.");

            // Repeat for the <5> key
            DmiActions.ShowInstruction(this,
                "Delete the value in the data input field, then press the <5> key and hold it for more than 1.5s");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Click’ sound is played once." + Environment.NewLine +
                                "2. The key is displayed pressed then immediately re-displayed enabled." +
                                Environment.NewLine +
                                "3. The data input field displays ‘5’, ‘j’, ‘k’, ‘l’, then ‘5’, ‘j’, and so on, as the key is re-pressed.");

            // Repeat for the <6> key
            DmiActions.ShowInstruction(this,
                "Delete the value in the data input field, then press the <6> key and hold it for more than 1.5s");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Click’ sound is played once." + Environment.NewLine +
                                "2. The key is displayed pressed then immediately re-displayed enabled." +
                                Environment.NewLine +
                                "3. The data input field displays ‘6’, ‘m’, ‘n’, ‘o’, then ‘6’, ‘m’, and so on, as the key is re-pressed.");

            // Repeat for the <7> key
            DmiActions.ShowInstruction(this,
                "Delete the value in the data input field, then press the <7> key and hold it for more than 1.5s");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Click’ sound is played once." + Environment.NewLine +
                                "2. The key is displayed pressed then immediately re-displayed enabled." +
                                Environment.NewLine +
                                "3. The data input field displays ‘7’, ‘p’, ‘q’, ‘r’, ‘s’,then ‘7’, ‘p’, and so on, as the key is re-pressed.");

            // Repeat for the <8> key
            DmiActions.ShowInstruction(this,
                "Delete the value in the data input field, then press the <8> key and hold it for more than 1.5s");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Click’ sound is played once." + Environment.NewLine +
                                "2. The key is displayed pressed then immediately re-displayed enabled." +
                                Environment.NewLine +
                                "3. The data input field displays ‘8’, ‘t’, ‘u’, ‘v’, then ‘8’, ‘t’, and so on, as the key is re-pressed.");

            // Repeat for the <9> key
            DmiActions.ShowInstruction(this,
                "Delete the value in the data input field, then press the <9> key and hold it for more than 1.5s");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Click’ sound is played once." + Environment.NewLine +
                                "2. The key is displayed pressed then immediately re-displayed enabled." +
                                Environment.NewLine +
                                "3. The data input field displays ‘9’, ‘w’, ‘x’, ‘y’, ‘z’, then ‘9’, ‘w’, and so on, as the key is re-pressed.");

            /*
            Test Step 10
            Action: Released the pressed button
            Expected Result: Verify the following information, The character is stop changing
            Test Step Comment: (1) MMI_gen 8033 (partly: MMI_gen 4913 (partly:  MMI_gen 4384 (partly: ETCS-MMI’s function associated to the button)));
            */
            DmiActions.ShowInstruction(this, @"Released the pressed key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The data input field stops changing the value for the <9> key in the data input field.");

            /*
            Test Step 11
            Action: Press the data key which have various characters once.After passed 2 seconds, press the same data key.Note: This step is for testing ‘’2’-‘9’ button.Stopwatch is required
            Expected Result: Verify the following information, An entered data are separated as 2 characters, for example ‘22’
            Test Step Comment: (1) MMI_gen 8033 (partly: MMI_gen 4693 (partly: (c), NEGATIVE, exceed 2 second delay time));
            */
            DmiActions.ShowInstruction(this,
                "Delete the value in the data input field, then press the <2> key. After 2s, press the key again");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The data input field displays ‘22’.");

            // Repeat for the <3> key
            DmiActions.ShowInstruction(this,
                "Delete the value in the data input field, then press the <3> key. After 2s, press the key again");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The data input field displays ‘33’.");

            // Repeat for the <4> key
            DmiActions.ShowInstruction(this,
                "Delete the value in the data input field, then press the <4> key. After 2s, press the key again");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The data input field displays ‘44’.");

            // Repeat for the <5> key
            DmiActions.ShowInstruction(this,
                "Delete the value in the data input field, then press the <5> key. After 2s, press the key again");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The data input field displays ‘55’.");

            // Repeat for the <6> key
            DmiActions.ShowInstruction(this,
                "Delete the value in the data input field, then press the <6> key. After 2s, press the key again");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The data input field displays ‘66’.");

            // Repeat for the <7> key
            DmiActions.ShowInstruction(this,
                "Delete the value in the data input field, then press the <7> key. After 2s, press the key again");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The data input field displays ‘77’.");

            // Repeat for the <8> key
            DmiActions.ShowInstruction(this,
                "Delete the value in the data input field, then press the <8> key. After 2s, press the key again");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The data input field displays ‘88’.");

            // Repeat for the <9> key
            DmiActions.ShowInstruction(this,
                "Delete the value in the data input field, then press the <9> key. After 2s, press the key again");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The data input field displays ‘99’.");

            /*
            Test Step 12
            Action: Press data key and then another data key within 2 seconds, for example 2 then 3Note: This step is for testing ‘’2’-‘9’ button.Stopwatch is required
            Expected Result: Verify the following information, The selected characters are added on an input field.The horizontal line cursor is forced to jump to next position directly
            Test Step Comment: (1) MMI_gen 8033 (partly: MMI_gen 4693 (a));(2) MMI_gen 8033 (partly: MMI_gen 4693 (d));
            */
            DmiActions.ShowInstruction(this,
                "Delete the value in the data input field, then press the <2> key. After less than 2s press the <4> key again");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The data input field displays ‘24’." + Environment.NewLine +
                                "2. The cursor moves to the right of the character just entered immediately after the key is pressed.");

            /*
            Test Step 13
            Action: Enter the data value with 5 characters
            Expected Result: Verify the following information,The 5 characters are added on an input field as one group. (e.g. ‘12345')
            Test Step Comment: (1) MMI_gen 8033 (partly: MMI_gen 4694 (partly: NEGATIVE, 6th character));
            */
            DmiActions.ShowInstruction(this,
                "Delete the value in the data input field, then enter ‘12345’ for the Driver ID");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The data input field displays ‘12345’.");

            /*
            Test Step 14
            Action: Continue to enter the 6th character
            Expected Result: Verify the following information,The fifth character is shown after a gap of fourth character, separated as 2 groups (e.g. 1234 56)
            Test Step Comment: (1) MMI_gen 8033 (partly: MMI_gen 4694 (partly: MMI_gen 4246));
            */
            DmiActions.ShowInstruction(this, "Press the <6> key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The data input field displays ‘1234 56’.");

            /*
            Test Step 15
            Action: Continue to enter the new value more than 8 characters
            Expected Result: Verify the following information,The data value is separated as 2 lines. In each line is displayed only 8 characters
            Test Step Comment: (1) MMI_gen 8033 (partly: MMI_gen 4694 (partly: MMI_gen 4247));
            */
            DmiActions.ShowInstruction(this, "Enter ‘123456’");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The data input field displays ‘1234 5612’ and ‘3456’ (over 2 lines).");

            /*
            Test Step 16
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
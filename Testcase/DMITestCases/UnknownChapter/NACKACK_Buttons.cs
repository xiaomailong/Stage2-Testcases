namespace Testcase.DMITestCases
{
    /// <summary>
    /// Acknowledgements: NACK/ACK Buttons
    /// TC-ID: 6.4
    /// 
    /// This test case verifies the display information of acknowledgement buttons refer to configured and the operation of safe up-type in each acknowledgement button.
    /// 
    /// Tested Requirements:
    /// MMI_gen 4507; MMI_gen 4470 (partly: MMI_gen 4381, MMI_gen 4382, MMI_gen 11387 (partly: send events of Pressed independently to ETCS), MMI_gen 11907 (partly: EVC-101, timestamp)); MMI_gen 4374; MMI_gen 4375; MMI_gen 4468; MMI_gen 4499 (partly: text message step back as non-acknowledgementable); MMI_gen 3375; MMI_gen 3200 (partly: NACK button, ACK button, Text acknowledgement); MMI_gen 3374 (partly: NACK button, ACK button, Text acknowledgement); MMI_gen 4256 (partly: Click sound);
    /// 
    /// Scenario:
    /// 1.Use the test script file to send EVC-
    /// 8.Then, verify the display of acknowledgement message with buttons.
    /// 2.Verify the safe up-type button of each acknowledgement button.
    /// 3.Use the test script file to send EVC-
    /// 8.Then, verify that configured colour is not effect when there is no acknowledgement button display.
    /// 4.Verify the safe up-type button of text acknowledgement. Then, verify the display of text message. 
    /// 
    /// Used files:
    /// 6_4_a.xml , 6_4_b.xml
    /// </summary>
    public class NACKACK_Buttons : TestcaseBase
    {
        public override bool TestcaseEntryPoint()
        {
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 0;
            // Testcase entrypoint


            MakeTestStepHeader(1, UniqueIdentifier++,
                "Use the test script file 6_4_a.xml to send EVC-8 with,MMI_I_TEXT = 1MMI_Q_TEXT = 527MMI_Q_TEXT_CRITERIA = 2MMI_Q_TEXT_CLASS = 1",
                "DMI displays the text message ‘Brake test aborted, perform new Test?’ with an acknowledgement option in sub-area E5-E9.Verify the following information,(1)  The ‘ACK’ button is labelled with text ‘ACK in yellow colour.(2)  The 'NACK' button is labelled with text 'NACK' in yellow colour.(3)   The background colour of both button 'ACK' and 'NACK' are red");
            /*
            Test Step 1
            Action: Use the test script file 6_4_a.xml to send EVC-8 with,MMI_I_TEXT = 1MMI_Q_TEXT = 527MMI_Q_TEXT_CRITERIA = 2MMI_Q_TEXT_CLASS = 1
            Expected Result: DMI displays the text message ‘Brake test aborted, perform new Test?’ with an acknowledgement option in sub-area E5-E9.Verify the following information,(1)  The ‘ACK’ button is labelled with text ‘ACK in yellow colour.(2)  The 'NACK' button is labelled with text 'NACK' in yellow colour.(3)   The background colour of both button 'ACK' and 'NACK' are red
            Test Step Comment: (1) MMI_gen 4468 (partly: ‘ACK’ in yellow);(2) MMI_gen 4507 (partly: 'NACK' in yellow colour);(3) MMI_gen 4468 (partly: configured background colour); MMI_gen 4507 (partly: configured background colour);
            */


            MakeTestStepHeader(2, UniqueIdentifier++, "Press and hold 'NACK' button",
                "Verify the following information,(1)   The sound ‘Click’ played once.(2)   The 'NACK' button is shown as pressed state, the border of button is removed.(3)   Use the log file to confirm that DMI send EVC-111 with variable MMI_Q_BUTTON = 1 (Pressed) and MMI_T_BUTTONEVENT is not blank");
            /*
            Test Step 2
            Action: Press and hold 'NACK' button
            Expected Result: Verify the following information,(1)   The sound ‘Click’ played once.(2)   The 'NACK' button is shown as pressed state, the border of button is removed.(3)   Use the log file to confirm that DMI send EVC-111 with variable MMI_Q_BUTTON = 1 (Pressed) and MMI_T_BUTTONEVENT is not blank
            Test Step Comment: (1) MMI_gen 4470 (partly: 'NACK', MMI_gen 4381 (partly: the sound for Up-Type button)); MMI_gen 4256 (partly: Click sound);(2) MMI_gen 4470 (partly: 'NACK', MMI_gen 4381 (partly: change to state ‘Pressed’ as long as remain actuated));(3) MMI_gen 4470 (partly: MMI_gen 11387 (partly: send events of Pressed independently to ETCS), MMI_gen 11907 (partly: EVC-111, timestamp)); MMI_gen 3375; MMI_gen 3200 (partly: NACK button, pressed);
            */


            MakeTestStepHeader(3, UniqueIdentifier++, "Slide out 'NACK' button",
                "The border of the button is shown (state ‘Enabled’) without a sound");
            /*
            Test Step 3
            Action: Slide out 'NACK' button
            Expected Result: The border of the button is shown (state ‘Enabled’) without a sound
            Test Step Comment: MMI_gen 4470 (partly: 'NACK', MMI_gen 4382 (partly: state ‘Enabled’ when slide out with force applied, no sound))); MMI_gen 4374;
            */
            // Call generic Check Results Method
            DmiExpectedResults.The_border_of_the_button_is_shown_state_Enabled_without_a_sound(this);


            MakeTestStepHeader(4, UniqueIdentifier++, "Slide back into 'NACK' button",
                "The button is back to state ‘Pressed’ without a sound");
            /*
            Test Step 4
            Action: Slide back into 'NACK' button
            Expected Result: The button is back to state ‘Pressed’ without a sound
            Test Step Comment: MMMI_gen 4470 (partly: 'NACK', MMI_gen 4382 (partly: state ‘Pressed’ when slide back, no sound))); MMI_gen 4375;
            */
            // Call generic Check Results Method
            DmiExpectedResults.The_button_is_back_to_state_Pressed_without_a_sound(this);


            MakeTestStepHeader(5, UniqueIdentifier++, "Release the 'NACK' button",
                "Verify the following information,(1)    The Ack-buttons with yellow flashing frame and text message is disappear.(2)   Use the log file to confirm that DMI send EVC-111 with variable MMI_Q_BUTTON = 0 (Released) and MMI_T_BUTTONEVENT is not blank");
            /*
            Test Step 5
            Action: Release the 'NACK' button
            Expected Result: Verify the following information,(1)    The Ack-buttons with yellow flashing frame and text message is disappear.(2)   Use the log file to confirm that DMI send EVC-111 with variable MMI_Q_BUTTON = 0 (Released) and MMI_T_BUTTONEVENT is not blank
            Test Step Comment: (1) MMI_gen 4470 (partly: 'NACK', MMI_gen 4381 (partly: exit state ‘Pressed’, execute function associated to the button)));(2) MMI_gen 4470 (partly: MMI_gen 11387 (partly: send events of Pressed independently to ETCS), MMI_gen 11907 (partly: EVC-111, timestamp)); MMI_gen 3375; MMI_gen 3200 (partly: NACK button, released);
            */


            MakeTestStepHeader(6, UniqueIdentifier++,
                "Use the test script file 6_4_a.xml to send EVC-8.Then, perform action step 2-5 for 'ACK' button",
                "See the expected results of Step 2 – Step 5");
            /*
            Test Step 6
            Action: Use the test script file 6_4_a.xml to send EVC-8.Then, perform action step 2-5 for 'ACK' button
            Expected Result: See the expected results of Step 2 – Step 5
            Test Step Comment: MMI_gen 4470 (partly: 'ACK'); MMI_gen 3375; MMI_gen 3200 (partly: ACK button);
            */


            MakeTestStepHeader(7, UniqueIdentifier++,
                "Use the test script file 6_4_b.xml to send EVC-8 with,MMI_I_TEXT = 1MMI_Q_TEXT = 527MMI_Q_TEXT_CRITERIA = 0MMI_Q_TEXT_CLASS = 1",
                "Verify the following information,(1)   DMI displays the text message ‘Brake test aborted, perform new Test?’ with a yellow flashing frame around sub-area E5-E9");
            /*
            Test Step 7
            Action: Use the test script file 6_4_b.xml to send EVC-8 with,MMI_I_TEXT = 1MMI_Q_TEXT = 527MMI_Q_TEXT_CRITERIA = 0MMI_Q_TEXT_CLASS = 1
            Expected Result: Verify the following information,(1)   DMI displays the text message ‘Brake test aborted, perform new Test?’ with a yellow flashing frame around sub-area E5-E9
            Test Step Comment: (1) MMI_gen 4468 (partly: Note); MMI_gen 3374 (partly: Text acknowledgement, visible, not faulty);
            */


            MakeTestStepHeader(8, UniqueIdentifier++, "Press and hold sub-area E5",
                "Verify the following information,(1)   The sound ‘Click’ played once.(2)   The sub-area E5 is shown as pressed state, the border of button is removed.(3)   Use the log file to confirm that DMI send EVC-101 with variable MMI_Q_BUTTON = 1 (Pressed) and MMI_T_BUTTONEVENT is not blank");
            /*
            Test Step 8
            Action: Press and hold sub-area E5
            Expected Result: Verify the following information,(1)   The sound ‘Click’ played once.(2)   The sub-area E5 is shown as pressed state, the border of button is removed.(3)   Use the log file to confirm that DMI send EVC-101 with variable MMI_Q_BUTTON = 1 (Pressed) and MMI_T_BUTTONEVENT is not blank
            Test Step Comment: (1) MMI_gen 3200 (partly: Text acknowledgement, MMI_gen 4381 (partly: the sound for Up-Type button));(2) MMI_gen 3200 (partly: Text acknowledgement, MMI_gen 4381 (partly: change to state ‘Pressed’ as long as remain actuated));(3) MMI_gen 3200 (partly: Text acknowledgement, pressed, MMI_gen 11387 (partly: send events of Pressed independently to ETCS), MMI_gen 11907 (partly: EVC-101, timestamp)); 
            */


            MakeTestStepHeader(9, UniqueIdentifier++, "Slide out sub-area E5",
                "The border of the sub-area E5 is shown (state ‘Enabled’) without a sound");
            /*
            Test Step 9
            Action: Slide out sub-area E5
            Expected Result: The border of the sub-area E5 is shown (state ‘Enabled’) without a sound
            Test Step Comment: MMI_gen 3200 (partly: Text acknowledgement, MMI_gen 4382 (partly: state ‘Enabled’ when slide out with force applied, no sound))); 
            */


            MakeTestStepHeader(10, UniqueIdentifier++, "Slide back into sub-area E5",
                "The sub-area E5 is back to state ‘Pressed’ without a sound");
            /*
            Test Step 10
            Action: Slide back into sub-area E5
            Expected Result: The sub-area E5 is back to state ‘Pressed’ without a sound
            Test Step Comment: MMMI_gen 3200 (partly: Text acknowledgement, MMI_gen 4382 (partly: state ‘Pressed’ when slide back, no sound))); 
            */


            MakeTestStepHeader(11, UniqueIdentifier++, "Release the pressed area",
                "Verify the following information,(1)   The yellow flashing frame is removed, DMI still display text message 'Brake test aborted, perform new Test?' in sub-area E5.(2)   Use the log file to confirm that DMI send EVC-101 with variable MMI_Q_BUTTON = 0 (Released) and MMI_T_BUTTONEVENT is not blank");
            /*
            Test Step 11
            Action: Release the pressed area
            Expected Result: Verify the following information,(1)   The yellow flashing frame is removed, DMI still display text message 'Brake test aborted, perform new Test?' in sub-area E5.(2)   Use the log file to confirm that DMI send EVC-101 with variable MMI_Q_BUTTON = 0 (Released) and MMI_T_BUTTONEVENT is not blank
            Test Step Comment: (1) MMI_gen 4499 (partly: text message step back as non-acknowledgementable);(2) MMI_gen 3200 (partly: Text acknowledgement, released, MMI_gen 11387 (partly: send events of Released independently to ETCS), MMI_gen 11907 (partly: EVC-101, timestamp));
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Release the pressed area");


            MakeTestStepHeader(12, UniqueIdentifier++, "End of test", "");

            /*
            Test Step 12
            Action: End of test
            Expected Result: 
            */


            return GlobalTestResult;
        }
    }
}
using System;
using Testcase.Telegrams.EVCtoDMI;

namespace Testcase.DMITestCases
{
    /// <summary>
    /// 20.3.2 Driver Messages: Processing of incoming Driver Messages
    /// TC-ID: 15.3.2
    /// 
    /// This test case verifies the DMI driver messages handling when received packet information. Driver message should displayed/removed correctly refer to specified important/auxiliary message.
    /// 
    /// Tested Requirements:
    /// MMI_gen 11455 (partly: touch screen); MMI_gen 1699 (partly: non-acknowledgeable, fixed text message, plain text message, system status message, The second group, the first group); MMI_gen 135 (partly: Added to Message list according to the first/second group, The topmost, The most recent one, Chronological order); MMI_gen 138 (partly: Place at the topmost position in its group, not full, first group, Sound Sinfo, NEGATIVE, no sound for text belong to second group); MMI_gen 136; MMI_gen 164; MMI_gen 1048 (partly: touch screen); MMI_gen 137; MMI_gen 140; MMI_gen 1314; MMI_gen 143; MMI_gen 144; MMI_gen 147 (partly: Remove the Driver message with the same index, Add the new Driver message to the message list); MMI_gen 148; MMI_gen 134 (partly: touch screen); MMI_gen 7050; MMI_gen 9516 (partly: text message in first group); MMI_gen 12025 (partly: text message in first group);
    /// 
    /// Scenario:
    /// Use the test script file to send EVC-8 to DMI. Then, verify the display information.The first group of non-acknowledgeable system plain/fixed/status text messageThe second group of non-acknowledgeable plain/fixed text messageThe text message with repeatedly message index numberNote: Each step of test script file in executed continuously, Tester need to confirm expected result within specific time (5 second).Press ‘Up’ and ‘Down’ button. Then, verify the display information.Use the test script file to send EVC-8 to DMI to remove a text message. Then, verify the display information.
    /// 
    /// Used files:
    /// 15_3_2_a.xml, 15_3_2_b.xml, 15_3_2_c.xml, 15_3_2_d.xml, 15_3_2_e.xml
    /// </summary>
    public class TC_15_3_2_Driver_Messages : TestcaseBase
    {
        public override bool TestcaseEntryPoint()
        {
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 0;
            // Testcase entrypoint
            StartUp();
            DmiActions.Complete_SoM_L1_SB(this);

            XML_15_3_2(msgType.typea);
            // Steps 1 to 7 are carried out in XML_15_3_2_a.cs
            MakeTestStepHeader(1, UniqueIdentifier++,
                "Use the test script file 15_3_2_a.xml to send EVC-8 with,MMI_Q_TEXT = 256MMI_Q_TEXT_CRITERIA = 3MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1MMI_N_TEXT = 2MMI_X_TEXT[0] = 42MMI_X_TEXT[1] = 43",
                "Verify the following information,(1)   The plain text message ‘*+’ is display in sub-area E5 without yellow flashing frame.(2)   The text message is presented with characters in bold style.(3)   Sound Sinfo is played.E.g.*+");
            /*
            Test Step 1
            Action: Use the test script file 15_3_2_a.xml to send EVC-8 with,MMI_Q_TEXT = 256MMI_Q_TEXT_CRITERIA = 3MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1MMI_N_TEXT = 2MMI_X_TEXT[0] = 42MMI_X_TEXT[1] = 43
            Expected Result: Verify the following information,(1)   The plain text message ‘*+’ is display in sub-area E5 without yellow flashing frame.(2)   The text message is presented with characters in bold style.(3)   Sound Sinfo is played.E.g.*+
            Test Step Comment: (1) MMI_gen 134 (partly: Touch screen, E5); MMI_gen 1699 (partly: non-acknowledgeable, important plain text message); (2) MMI_gen 11455 (partly: plain text message, bold style, Q_TEXT_CLASS = 1, the first group);(3) MMI_gen 11455 (partly: sound Sinfo); MMI_gen 9516 (partly: text message in first group); MMI_gen 12025 (partly: text message in first group); 
            */


            MakeTestStepHeader(2, UniqueIdentifier++,
                "(Continue from step 1)Send EVC-8 with,MMI_Q_TEXT = 580MMI_Q_TEXT_CRITERIA = 3MMI_Q_TEXT_CLASS = 0MMI_I_TEXT = 2MMI_N_TEXT = 2MMI_X_TEXT[0] = 42MMI_X_TEXT[1] = 42",
                "Verify the following information,(1)   The plain text message ‘**’ is display in sub-are E6 without yellow flashing frame.(2)   The text message is presented with characters in regular style.(3)   There is no sound played.(4)   There is no gap between the new text message and older message from previous step.E.g.*+**");
            /*
            Test Step 2
            Action: (Continue from step 1)Send EVC-8 with,MMI_Q_TEXT = 580MMI_Q_TEXT_CRITERIA = 3MMI_Q_TEXT_CLASS = 0MMI_I_TEXT = 2MMI_N_TEXT = 2MMI_X_TEXT[0] = 42MMI_X_TEXT[1] = 42
            Expected Result: Verify the following information,(1)   The plain text message ‘**’ is display in sub-are E6 without yellow flashing frame.(2)   The text message is presented with characters in regular style.(3)   There is no sound played.(4)   There is no gap between the new text message and older message from previous step.E.g.*+**
            Test Step Comment: (1) MMI_gen 134 (partly: Touch screen, E6); MMI_gen 1699 (partly: non-acknowledgeable, auxiliary plain text message);(2) MMI_gen 11455 (partly: plain text message, regular style, Q_TEXT_CLASS = 0, the second group); MMI_gen 1699 (partly: The second group);(3) MMI_gen 11455 (partly: No sound used);(4) MMI_gen 136;  MMI_gen 134 (partly: E6); MMI_gen 135 (partly: The most recent one);     
            */


            MakeTestStepHeader(3, UniqueIdentifier++,
                "(Continue from step 2)Send EVC-8 with,MMI_Q_TEXT = 0MMI_Q_TEXT_CRITERIA = 3MMI_Q_TEXT_CLASS = 0MMI_I_TEXT = 3",
                "Verify the following information,(1)   Text message ‘Level crossing not protected’ is displayed in sub-area E6 without yellow flashing frame.(2)   The bold text message is still displayed above the regular messages. (3)  There is no sound played.(4)  The old text message ‘**’ is moved to sub-area E7. E.g.*+Level crossing not protected**");
            /*
            Test Step 3
            Action: (Continue from step 2)Send EVC-8 with,MMI_Q_TEXT = 0MMI_Q_TEXT_CRITERIA = 3MMI_Q_TEXT_CLASS = 0MMI_I_TEXT = 3
            Expected Result: Verify the following information,(1)   Text message ‘Level crossing not protected’ is displayed in sub-area E6 without yellow flashing frame.(2)   The bold text message is still displayed above the regular messages. (3)  There is no sound played.(4)  The old text message ‘**’ is moved to sub-area E7. E.g.*+Level crossing not protected**
            Test Step Comment: (1) MMI_gen 11455 (partly:  classified the second group in chronological way); MMI_gen 135 (partly: added, the second group, chronological order); MMI_gen 1699 (partly: non-acknowledgeable, auxiliary fixed text message); (2) MMI_gen 11455 (partly: divided, fixed text message, the first group above the second group); MMI_gen 1699 (partly: The second group);      (3) MMI_gen 11455 (partly: No sound used); MMI_gen 138 (partly: NEGATIVE, no sound for text belong to second group);(4) MMI_gen 134 (partly: Touch screen, E7);
            */


            MakeTestStepHeader(4, UniqueIdentifier++,
                "(Continue from step 3)Send EVC-8 with,MMI_Q_TEXT = 273MMI_Q_TEXT_CRITERIA = 3MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 4",
                "Verify the following information,(1)   Text message ‘Unauthorize passing of EOA/LOA’ is displayed in sub-area E5-E6 without yellow flashing frame.(2)   Sound Sinfo is played.(3)   The old text messages are moved to sub-area E7-E9 respectively.(4)   The navigation buttons <Up> and <Down> at sub-area E10-E11 are disabled.(5)   DMI displays symbol NA15 at sub-area E10.(6)   DMI displays symbol NA16 at sub-area E11.E.g. Unauthorize passing of EOA/LOA  *+  Level crossing not protected  **");
            /*
            Test Step 4
            Action: (Continue from step 3)Send EVC-8 with,MMI_Q_TEXT = 273MMI_Q_TEXT_CRITERIA = 3MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 4
            Expected Result: Verify the following information,(1)   Text message ‘Unauthorize passing of EOA/LOA’ is displayed in sub-area E5-E6 without yellow flashing frame.(2)   Sound Sinfo is played.(3)   The old text messages are moved to sub-area E7-E9 respectively.(4)   The navigation buttons <Up> and <Down> at sub-area E10-E11 are disabled.(5)   DMI displays symbol NA15 at sub-area E10.(6)   DMI displays symbol NA16 at sub-area E11.E.g. Unauthorize passing of EOA/LOA  *+  Level crossing not protected  **
            Test Step Comment: (1) MMI_gen 135 (partly: The most recent one); MMI_gen 1699 (partly: non-acknowledgeable, important system status message); MMI_gen 138 (partly: Place at the topmost position in its group); (2) MMI_gen 138 (partly: Sound Sinfo); MMI_gen 9516 (partly: text message in first group); MMI_gen 12025 (partly: text message in first group);(3) MMI_gen 134 (partly: Touch screen, E8);(4) MMI_gen 1048 (partly: Not more than 5 lines, touch screen); MMI_gen 137 (partly: opposite case);(5) MMI_gen 1048 (partly: NA15);(6) MMI_gen 1048 (partly: NA16);
            */


            MakeTestStepHeader(5, UniqueIdentifier++,
                "(Continue from step 4)Send EVC-8 with,MMI_Q_TEXT = 625MMI_Q_TEXT_CRITERIA = 3MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 5",
                "Verify the following information,(1)   Text message ‘Tachometer error’ is displayed in sub-area E5 without yellow flashing frame.(2)   Sound Sinfo is played.(3)   The old text messages are moved to sub-area E6-E9 respectively.(4)   The navigation button <Down> is enabled.(5)   DMI displays symbol NA14 at sub-area E11.E.g.Shown messages:Tachometer error Unauthorize passing of EOA/LOA *+ Level crossing not protectedHidden messages:**");
            /*
            Test Step 5
            Action: (Continue from step 4)Send EVC-8 with,MMI_Q_TEXT = 625MMI_Q_TEXT_CRITERIA = 3MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 5
            Expected Result: Verify the following information,(1)   Text message ‘Tachometer error’ is displayed in sub-area E5 without yellow flashing frame.(2)   Sound Sinfo is played.(3)   The old text messages are moved to sub-area E6-E9 respectively.(4)   The navigation button <Down> is enabled.(5)   DMI displays symbol NA14 at sub-area E11.E.g.Shown messages:Tachometer error Unauthorize passing of EOA/LOA *+ Level crossing not protectedHidden messages:**
            Test Step Comment: (1) MMI_gen 135 (partly: The most recent one, topmost); MMI_gen 1699 (partly: non-acknowledgeable, important fixed text message); MMI_gen 138 (partly: not full, first group));(2) MMI_gen 138 (partly: Sound Sinfo); MMI_gen 9516 (partly: text message in first group); MMI_gen 12025 (partly: text message in first group);(3) MMI_gen 134 (partly: Touch screen, E9);(4) MMI_gen 140 (partly: enabled);(5) MMI_gen 140 (partly: NA14);
            */


            MakeTestStepHeader(6, UniqueIdentifier++,
                "(Continue from step 5)Send EVC-8 with,MMI_Q_TEXT = 712MMI_Q_TEXT_CRITERIA = 3MMI_Q_TEXT_CLASS = 0MMI_I_TEXT = 6",
                "Verify the following information,(1)   Text message ‘Wheel data settings were successfully changed’ is displayed in sub-area E9 without yellow flashing frame.Note: The text message is too long for 1 line. So, the message is not completely displayed in the visibility window.(2)   There is no sound played.E.g.Shown messages: Tachometer error  Unauthorize passing of EOA/LOA  *+  Wheel data settings were Hidden messages: successfully changed  Level crossing not protected  **");
            /*
            Test Step 6
            Action: (Continue from step 5)Send EVC-8 with,MMI_Q_TEXT = 712MMI_Q_TEXT_CRITERIA = 3MMI_Q_TEXT_CLASS = 0MMI_I_TEXT = 6
            Expected Result: Verify the following information,(1)   Text message ‘Wheel data settings were successfully changed’ is displayed in sub-area E9 without yellow flashing frame.Note: The text message is too long for 1 line. So, the message is not completely displayed in the visibility window.(2)   There is no sound played.E.g.Shown messages: Tachometer error  Unauthorize passing of EOA/LOA  *+  Wheel data settings were Hidden messages: successfully changed  Level crossing not protected  **
            Test Step Comment: (1) MMI_gen 138 (partly: Place at the topmost position in its group);(2) MMI_gen 11455 (partly: No sound used); MMI_gen 138 (partly: NEGATIVE, no sound for text belong to second group);
            */


            MakeTestStepHeader(7, UniqueIdentifier++,
                "(Continue from step 6)Send EVC-8 with,MMI_Q_TEXT = 583MMI_Q_TEXT_CRITERIA = 3MMI_Q_TEXT_CLASS = 0MMI_I_TEXT = 2",
                "Verify the following information,(1)   Text message ‘Doppler error’ is displayed in sub-area E9 without yellow flashing frame.(2)    There is no sound played.E.g.Shown messages: Tachometer error  Unauthorize passing of EOA/LOA  *+  Doppler error Hidden messages: Wheel data settings were successfully changed  Level crossing not protected");
            /*
            Test Step 7
            Action: (Continue from step 6)Send EVC-8 with,MMI_Q_TEXT = 583MMI_Q_TEXT_CRITERIA = 3MMI_Q_TEXT_CLASS = 0MMI_I_TEXT = 2
            Expected Result: Verify the following information,(1)   Text message ‘Doppler error’ is displayed in sub-area E9 without yellow flashing frame.(2)    There is no sound played.E.g.Shown messages: Tachometer error  Unauthorize passing of EOA/LOA  *+  Doppler error Hidden messages: Wheel data settings were successfully changed  Level crossing not protected
            Test Step Comment: (1) MMI_gen 148 (partly: add new text driver message, MMI_gen 138 (partly: topmost position));(2) MMI_gen 147 (partly: MMI_gen 138 (partly: NEGATIVE of moving the visibility window, no sound for text belong to second group));
            */


            MakeTestStepHeader(8, UniqueIdentifier++, "Press ‘Down’ button at sub-area E11",
                "Verify the following information,(1)   The navigation button <Up> is enabled.(2)   DMI displays symbol NA13 at sub-area E10.(3)   The visibility window is moves down to the 1st line of the next lower text message, text message ‘Wheel data settings were successfully changed' is displayed in sub-area E8-E9.E.g.Hidden messages (above): Tachometer error  Unauthorize passing ofShown messages: EOA/LOA  *+  Doppler error  Wheel data settings were successfully changedHidden messages (below): Level crossing not protected");
            /*
            Test Step 8
            Action: Press ‘Down’ button at sub-area E11
            Expected Result: Verify the following information,(1)   The navigation button <Up> is enabled.(2)   DMI displays symbol NA13 at sub-area E10.(3)   The visibility window is moves down to the 1st line of the next lower text message, text message ‘Wheel data settings were successfully changed' is displayed in sub-area E8-E9.E.g.Hidden messages (above): Tachometer error  Unauthorize passing ofShown messages: EOA/LOA  *+  Doppler error  Wheel data settings were successfully changedHidden messages (below): Level crossing not protected
            Test Step Comment: (1) MMI_gen 137 (partly: enabled);(2) MMI_gen 137 (partly: NA13);(3) MMI_gen 11455 (partly: Down button, area E11); MMI_gen 164 (partly: Down, scroll through); MMI_gen 134 (partly: visibility window); MMI_gen 143 (partly: 1st line of the next lower text message is not yet visible);
            */
            WaitForVerification("Press ‘Down’ button at sub-area E11 and check the following:" + Environment.NewLine +
                                Environment.NewLine +
                                "1. The navigation button <Up> is enabled." + Environment.NewLine +
                                "2. DMI displays symbol NA13 at sub-area E10." + Environment.NewLine +
                                "3. The visibility window moves down to the 1st line of the next lower text message" +
                                Environment.NewLine +
                                "4. Text message ‘Wheel data settings were successfully changed' is displayed in sub-area E8-E9.");

            MakeTestStepHeader(9, UniqueIdentifier++, "Press ‘Down’ button at sub-area E11",
                "Verify the following information,(1)   The visibility window is moves one line down, text message ‘Level crossing not protected’ is displayed at sub-area E9.(2)   The navigation buttons <Down> at sub-area E11 is disabled, display as symbol NA16");
            /*
            Test Step 9
            Action: Press ‘Down’ button at sub-area E11
            Expected Result: Verify the following information,(1)   The visibility window is moves one line down, text message ‘Level crossing not protected’ is displayed at sub-area E9.(2)   The navigation buttons <Down> at sub-area E11 is disabled, display as symbol NA16
            Test Step Comment: (1) MMI_gen 143 (partly: opposite case, moves one line down); (2) MMI_gen 140 (partly: disabled);
            */
            WaitForVerification("Press ‘Down’ button at sub-area E11 and check the following:" + Environment.NewLine +
                                Environment.NewLine +
                                "1. The visibility window moves down to the 1st line of the next lower text message" +
                                Environment.NewLine +
                                "2. The navigation button <Down> at sub-area E11 is disabled, displayed as symbol NA16.");

            MakeTestStepHeader(10, UniqueIdentifier++, "Press ‘Down’ button at sub-area E11",
                "Verify the following information,(1)   The display information in sub-area E5-E9 are not changed");
            /*
            Test Step 10
            Action: Press ‘Down’ button at sub-area E11
            Expected Result: Verify the following information,(1)   The display information in sub-area E5-E9 are not changed
            Test Step Comment: (1) MMI_gen 164 (partly: Down, No wrap around); MMI_gen 11455 (partly: not be circular); MMI_gen 147 (partly:remove text driver message, MMI_I_TEXT = 2);
            */
            WaitForVerification("Press ‘Down’ button at sub-area E11 and check the following:" + Environment.NewLine +
                                Environment.NewLine +
                                "1. The display information in sub-area E5-E9 are not changed.");

            MakeTestStepHeader(11, UniqueIdentifier++,
                "Use the test script file 15_3_2_b.xml to send EVC-8 with to remove a text message, MMI_Q_TEXT = 0MMI_Q_TEXT_CRITERIA = 4MMI_Q_TEXT_CLASS = 0MMI_I_TEXT = 2",
                "Verify the following information,The fixed text message ‘Doppler error’ is removed.The text message ‘Wheel data settings were successfully changed’ at the lowest position is move up to close the gap, display in sub-area E9.The navigation buttons <Down> at sub-area E11 is disabled, display as symbol NA16. E.g.Hidden messages (above): Tachometer error  Unauthorize passing ofShown messages: EOA/LOA  *+  Wheel data settings were successfully changed  Level crossing not protected");
            /*
            Test Step 11
            Action: Use the test script file 15_3_2_b.xml to send EVC-8 with to remove a text message, MMI_Q_TEXT = 0MMI_Q_TEXT_CRITERIA = 4MMI_Q_TEXT_CLASS = 0MMI_I_TEXT = 2
            Expected Result: Verify the following information,The fixed text message ‘Doppler error’ is removed.The text message ‘Wheel data settings were successfully changed’ at the lowest position is move up to close the gap, display in sub-area E9.The navigation buttons <Down> at sub-area E11 is disabled, display as symbol NA16. E.g.Hidden messages (above): Tachometer error  Unauthorize passing ofShown messages: EOA/LOA  *+  Wheel data settings were successfully changed  Level crossing not protected
            Test Step Comment: (1) MMI_gen 144 (partly: MMI_I_TEXT);(2) MMI_gen 144 (partly: move up to close the gap);(3) MMI_gen 140 (partly: disabled);
            */
            XML_15_3_2(msgType.typeb);

            MakeTestStepHeader(12, UniqueIdentifier++, "Press ‘Up’ button at sub-area E10 3 times",
                "Verify the following information,(1)   The navigation buttons <Up> at sub-area E10 is disabled, display as symbol NA15.(2)   The visibility window is moved up to the next 1st line of a text message when the button is pressed.E.g.Hidden messages (above):“Shown messages:Tachometer error   Unauthorize passing of EOA/LOA  *+   Wheel data settings were Hidden messages (below): successfully changed  Level crossing not protected");
            /*
            Test Step 12
            Action: Press ‘Up’ button at sub-area E10 3 times
            Expected Result: Verify the following information,(1)   The navigation buttons <Up> at sub-area E10 is disabled, display as symbol NA15.(2)   The visibility window is moved up to the next 1st line of a text message when the button is pressed.E.g.Hidden messages (above):“Shown messages:Tachometer error   Unauthorize passing of EOA/LOA  *+   Wheel data settings were Hidden messages (below): successfully changed  Level crossing not protected
            Test Step Comment: (1) MMI_gen 137 (partly: opposite case);(2) MMI_gen 1314; MMI_gen 11455 (partly: Up button, area E10); MMI_gen 164 (partly: Up, scroll through); MMI_gen 7050 (partly: E10);
            */
            WaitForVerification("Press ‘Up’ button at sub-area E10 3 times and check the following:" +
                                Environment.NewLine + Environment.NewLine +
                                "1. The navigation buttons <Up> at sub-area E10 is disabled, display as symbol NA15." +
                                Environment.NewLine +
                                "2. The visibility window is moved up to the next 1st line of a text message each time the button is pressed.");

            MakeTestStepHeader(13, UniqueIdentifier++, "Press ‘Up’ button at sub-area E10",
                "Verify the following information,(1)   The display information in sub-area E5-E9 are not changed");
            /*
            Test Step 13
            Action: Press ‘Up’ button at sub-area E10
            Expected Result: Verify the following information,(1)   The display information in sub-area E5-E9 are not changed
            Test Step Comment: (1) MMI_gen 164 (partly: Up, No wrap around); MMI_gen 11455 (partly: not be circular);
            */
            WaitForVerification("Press ‘Up’ button at sub-area E10 and check the following:" + Environment.NewLine +
                                Environment.NewLine +
                                "1. The display information in sub-area E5-E9 is not changed.");

            MakeTestStepHeader(14, UniqueIdentifier++,
                "Use the test script file 15_3_2_c.xml to send a multiple packet of EVC-8 to remove a text message,Common variables MMI_Q_TEXT = 0MMI_Q_TEXT_CRITERIA = 4MMI_Q_TEXT_CLASS = 0The order of MMI_I_TEXT value in each packetMMI_I_TEXT = 1MMI_I_TEXT = 3MMI_I_TEXT = 5MMI_I_TEXT = 6",
                "The text messages of “Tachometer error”, “*+”, “Wheel data settings were successfully changed” and “Level crossing not protected” disappear.E.g.Shown messages: Unauthorize passing of EOA/LOA");
            /*
            Test Step 14
            Action: Use the test script file 15_3_2_c.xml to send a multiple packet of EVC-8 to remove a text message,Common variables MMI_Q_TEXT = 0MMI_Q_TEXT_CRITERIA = 4MMI_Q_TEXT_CLASS = 0The order of MMI_I_TEXT value in each packetMMI_I_TEXT = 1MMI_I_TEXT = 3MMI_I_TEXT = 5MMI_I_TEXT = 6
            Expected Result: The text messages of “Tachometer error”, “*+”, “Wheel data settings were successfully changed” and “Level crossing not protected” disappear.E.g.Shown messages: Unauthorize passing of EOA/LOA
            */
            XML_15_3_2(msgType.typec);

            MakeTestStepHeader(15, UniqueIdentifier++,
                "Use the test script file 15_3_2_d.xml to send EVC-8 with,MMI_Q_TEXT = 261MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 0MMI_I_TEXT = 1",
                "Verify the following information,(1)    The new incoming message “Fixed Text Message 261” is display in sub-area E6.E.g.Shown messages: Unauthorize passing of EOA/LOA  Fixed Text Message 261");
            /*
            Test Step 15
            Action: Use the test script file 15_3_2_d.xml to send EVC-8 with,MMI_Q_TEXT = 261MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 0MMI_I_TEXT = 1
            Expected Result: Verify the following information,(1)    The new incoming message “Fixed Text Message 261” is display in sub-area E6.E.g.Shown messages: Unauthorize passing of EOA/LOA  Fixed Text Message 261
            Test Step Comment: (1) MMI_gen 148 (indexing, auxiliary);
            */
            XML_15_3_2(msgType.typed);
            // Steps 15 to 18 are carried out in XML_15_3_2_d.cs

            MakeTestStepHeader(16, UniqueIdentifier++,
                "(Continue from step 15) Send EVC-8 withMMI_Q_TEXT = 270MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 3",
                "Verify the following information,(1)    The new incoming message “Fixed Text Message 270” is display in sub-area E5.(2)    Sound Sinfo is played.E.g.Shown messages:  Fixed Text Message 270  Unauthorize passing of EOA/LOA  Fixed Text Message 261");
            /*
            Test Step 16
            Action: (Continue from step 15) Send EVC-8 withMMI_Q_TEXT = 270MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 3
            Expected Result: Verify the following information,(1)    The new incoming message “Fixed Text Message 270” is display in sub-area E5.(2)    Sound Sinfo is played.E.g.Shown messages:  Fixed Text Message 270  Unauthorize passing of EOA/LOA  Fixed Text Message 261
            Test Step Comment: (1) MMI_gen 148 (indexing, important); (2) MMI_gen 11455 (partly: sound); MMI_gen 138 (partly: sound); MMI_gen 9516 (partly: text message in first group); MMI_gen 12025 (partly: text message in first group);
            */


            MakeTestStepHeader(17, UniqueIdentifier++,
                "(Continue from step 16) Send EVC-8 withMMI_Q_TEXT = 271MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 5",
                "Verify the following information,(1)    The new incoming message “Fixed Text Message 271” is display in sub-area E5. (2)    Sound Sinfo is played.E.g.Shown messages:  Fixed Text Message 271  Fixed Text Message 270  Unauthorize passing of EOA/LOA  Fixed Text Message 261");
            /*
            Test Step 17
            Action: (Continue from step 16) Send EVC-8 withMMI_Q_TEXT = 271MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 5
            Expected Result: Verify the following information,(1)    The new incoming message “Fixed Text Message 271” is display in sub-area E5. (2)    Sound Sinfo is played.E.g.Shown messages:  Fixed Text Message 271  Fixed Text Message 270  Unauthorize passing of EOA/LOA  Fixed Text Message 261
            Test Step Comment: (1) MMI_gen 148 (indexing, important);(2) MMI_gen 11455 (partly: sound); MMI_gen 138 (partly: sound); MMI_gen 9516 (partly: text message in first group); MMI_gen 12025 (partly: text message in first group);
            */


            MakeTestStepHeader(18, UniqueIdentifier++,
                "(Continue from step 17) Send EVC-8 withMMI_Q_TEXT = 272MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 0MMI_I_TEXT = 6",
                "Verify the following information,(1)    The new incoming message “Fixed Text Message 272” is display in sub-area E9.E.g.Shown messages:  Fixed Text Message 271  Fixed Text Message 270  Unauthorize passing of EOA/LOAFixed Text Message 272Hidden messages: Fixed Text Message 261");
            /*
            Test Step 18
            Action: (Continue from step 17) Send EVC-8 withMMI_Q_TEXT = 272MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 0MMI_I_TEXT = 6
            Expected Result: Verify the following information,(1)    The new incoming message “Fixed Text Message 272” is display in sub-area E9.E.g.Shown messages:  Fixed Text Message 271  Fixed Text Message 270  Unauthorize passing of EOA/LOAFixed Text Message 272Hidden messages: Fixed Text Message 261
            Test Step Comment: (1) MMI_gen 148 (indexing, auxiliary);
            */


            MakeTestStepHeader(19, UniqueIdentifier++,
                "Press ‘Down’ button.Then, use the test script file 15_3_2_e.xml to send EVC-8 to remove text message,MMI_Q_TEXT = 261MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 4MMI_I_TEXT = 1",
                "Verify the following information,(1)    The new incoming message “Fixed Text Message 261” is removed from sub-area E9.E.g.Shown messages:  Fixed Text Message 271  Fixed Text Message 270  Unauthorize passing of EOA/LOAFixed Text Message 272");
            /*
            Test Step 19
            Action: Press ‘Down’ button.Then, use the test script file 15_3_2_e.xml to send EVC-8 to remove text message,MMI_Q_TEXT = 261MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 4MMI_I_TEXT = 1
            Expected Result: Verify the following information,(1)    The new incoming message “Fixed Text Message 261” is removed from sub-area E9.E.g.Shown messages:  Fixed Text Message 271  Fixed Text Message 270  Unauthorize passing of EOA/LOAFixed Text Message 272
            Test Step Comment: (1) MMI_gen 148 (deleting, auxiliary);
            */
            XML_15_3_2(msgType.typee);
            // Steps 19 and 20 are carried out in XML_15_3_2_e.cs
            DmiActions.ShowInstruction(this, @"Press <Down> button");

            WaitForVerification("Press ‘Down’ button at sub-area E10 and check the following:" + Environment.NewLine +
                                Environment.NewLine +
                                "1. The new incoming message “Fixed Text Message 261” is removed from sub-area E9.");

            MakeTestStepHeader(20, UniqueIdentifier++,
                "(Continue from step 19)Send EVC-8 with,MMI_Q_TEXT = 271MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 4MMI_I_TEXT = 5",
                "Verify the following information,(1)    The new incoming message “Fixed Text Message 271” is removed from sub-area E5.E.g.Shown messages:  Fixed Text Message 270  Unauthorize passing of EOA/LOA  Fixed Text Message 272");
            /*
            Test Step 20
            Action: (Continue from step 19)Send EVC-8 with,MMI_Q_TEXT = 271MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 4MMI_I_TEXT = 5
            Expected Result: Verify the following information,(1)    The new incoming message “Fixed Text Message 271” is removed from sub-area E5.E.g.Shown messages:  Fixed Text Message 270  Unauthorize passing of EOA/LOA  Fixed Text Message 272
            Test Step Comment: (1) MMI_gen 148 (deleting, important);
            */


            MakeTestStepHeader(21, UniqueIdentifier++, "End of test", "");

            /*
            Test Step 21
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }

        #region Send_XML_15_3_2_DMI_Test_Specification

        enum msgType
        {
            typea,
            typeb,
            typec,
            typed,
            typee
        }

        private void XML_15_3_2(msgType type)
        {
            switch (type)
            {
                case msgType.typea:
                    // Step 1
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
                    EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT = 256;
                    EVC8_MMIDriverMessage.PlainTextMessage = "*+";

                    EVC8_MMIDriverMessage.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        "1. The plain text message ‘*+’ is displayed sub-area E5 without a yellow flashing frame." +
                                        Environment.NewLine +
                                        "2. The text message is presented with characters in bold style." +
                                        Environment.NewLine +
                                        "3. Sound Sinfo is played.");

                    // Step 2
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.AuxiliaryInformation;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
                    EVC8_MMIDriverMessage.MMI_I_TEXT = 2;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT = 256;
                    EVC8_MMIDriverMessage.PlainTextMessage = "**";

                    EVC8_MMIDriverMessage.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        "1. The plain text message ‘**’ is displayed sub-area E5 without a yellow flashing frame." +
                                        Environment.NewLine +
                                        "2. The text message is presented with characters in regular style." +
                                        Environment.NewLine +
                                        "3. No sound is played." + Environment.NewLine +
                                        "4. There is no gap between the new text message and older message from the previous step.");

                    // Step 3
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.AuxiliaryInformation;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
                    EVC8_MMIDriverMessage.MMI_I_TEXT = 3;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT = 0;
                    EVC8_MMIDriverMessage.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        "1. Text message ‘Level crossing not protected’ is displayed in sub-area E6 without a yellow flashing frame." +
                                        Environment.NewLine +
                                        "2. The bold text message is still displayed above the regular messages." +
                                        Environment.NewLine +
                                        "3. No sound is played." + Environment.NewLine +
                                        "4. The old text message ‘**’ is moved to sub-area E7.");

                    // Step 4
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
                    EVC8_MMIDriverMessage.MMI_I_TEXT = 4;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT = 273;
                    EVC8_MMIDriverMessage.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        "1. Text message ‘Unauthorized passing of EOA/LOA’ is displayed in sub-area E5-E6 without yellow flashing frame." +
                                        Environment.NewLine +
                                        "2. Sound Sinfo is played." + Environment.NewLine +
                                        "3. The old text messages are moved to sub-areas E7 - E9 respectively." +
                                        Environment.NewLine +
                                        "4. The navigation buttons < Up > and < Down > at sub-areas E10 - E11 are disabled." +
                                        Environment.NewLine +
                                        "5. DMI displays symbol NA15 at sub-area E10." + Environment.NewLine +
                                        "6. DMI displays symbol NA16 at sub-area E11.");

                    // Step 5
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
                    EVC8_MMIDriverMessage.MMI_I_TEXT = 5;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT = 625;
                    EVC8_MMIDriverMessage.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        "1. Text message ‘Tachometer error’ is displayed in sub-area E5 without yellow flashing frame." +
                                        Environment.NewLine +
                                        "2. Sound Sinfo is played." + Environment.NewLine +
                                        "3. The old text messages are moved to sub-area E6 - E9 respectively." +
                                        Environment.NewLine +
                                        "4. The navigation button <Down> is enabled." + Environment.NewLine +
                                        "5. DMI displays symbol NA14 at sub-area E11.");

                    // Step 6
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.AuxiliaryInformation;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
                    EVC8_MMIDriverMessage.MMI_I_TEXT = 6;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT = 712;
                    EVC8_MMIDriverMessage.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        "1. Truncated text message ‘Wheel data settings were successfully changed’ is displayed in sub-area E9 without yellow flashing frame." +
                                        Environment.NewLine +
                                        "2. No sound is played.");

                    // Step 7
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.AuxiliaryInformation;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
                    EVC8_MMIDriverMessage.MMI_I_TEXT = 7;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT = 583;

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        "1. Text message ‘Doppler error’ is displayed in sub - area E9 without yellow flashing frame." +
                                        Environment.NewLine +
                                        "2. No sound is played.");


                    break;
                case msgType.typeb:
                    // Step 1
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.AuxiliaryInformation;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 4;
                    EVC8_MMIDriverMessage.MMI_I_TEXT = 2;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT = 0;

                    EVC8_MMIDriverMessage.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        "1. The fixed text message ‘Doppler error’ is removed." + Environment.NewLine +
                                        "2. The text message ‘Wheel data settings were successfully changed’ at the lowest position is moved up to close the gap, displayed in sub-area E9." +
                                        Environment.NewLine +
                                        "3. The navigation button <Down> at sub-area E11 is disabled, displayed as symbol NA16.");

                    break;
                case msgType.typec:
                    // A series of EVC-8 messages are sent (see 15_3_2_c.xml) to successively remove a line of text

                    // Step 14/1
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 4;
                    EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT = 0;

                    EVC8_MMIDriverMessage.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        "1. The text message ‘Tachometer error’ is removed.");

                    // Step 14/2
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 4;
                    EVC8_MMIDriverMessage.MMI_I_TEXT = 3;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT = 0;

                    EVC8_MMIDriverMessage.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        "1. The text message ‘*+’ is removed.");

                    // Step 14/3
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 4;
                    EVC8_MMIDriverMessage.MMI_I_TEXT = 5;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT = 0;

                    EVC8_MMIDriverMessage.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        "1. The text message ‘Wheel data settings were successfully changed’ is removed.");

                    // Step 14/4
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 4;
                    EVC8_MMIDriverMessage.MMI_I_TEXT = 6;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT = 0;

                    EVC8_MMIDriverMessage.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        "1. The text message ‘Level crossing not protected’ is removed.");


                    break;
                case msgType.typed:
                    // XML file has these 4 steps as step 14

                    // Step 15
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.AuxiliaryInformation;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
                    EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT = 261;

                    EVC8_MMIDriverMessage.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        "1. The new incoming message “Fixed Text Message 261” is displayed in sub-area E6.");

                    // Step 16
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
                    EVC8_MMIDriverMessage.MMI_I_TEXT = 3;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT = 270;

                    EVC8_MMIDriverMessage.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        "1. The new incoming message “Fixed Text Message 270” is display in sub-area E5.");

                    // Step 17
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
                    EVC8_MMIDriverMessage.MMI_I_TEXT = 5;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT = 271;

                    EVC8_MMIDriverMessage.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        "1. The new incoming message “Fixed Text Message 271” is display in sub-area E5.");

                    // Step 18
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.AuxiliaryInformation;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
                    EVC8_MMIDriverMessage.MMI_I_TEXT = 6;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT = 272;

                    EVC8_MMIDriverMessage.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        "1. The new incoming message “Fixed Text Message 272” is display in sub-area E9.");


                    break;
                case msgType.typee:
                    // XML file has these 2 steps as step 14
                    // The spec has Q_TEXT_CLASS = 4 while Q_TEXT_CRITERIA = 1. These should be reversed - XML file is correct

                    // Step 19
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.AuxiliaryInformation;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 4;
                    EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT = 261;

                    EVC8_MMIDriverMessage.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        "1. The new incoming message “Fixed Text Message 261” is removed from sub-area E9.");

                    // Step 20
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 4;
                    EVC8_MMIDriverMessage.MMI_I_TEXT = 5;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT = 271;

                    EVC8_MMIDriverMessage.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        "1. The new incoming message “Fixed Text Message 271” is removed from sub-area E5.");


                    break;
            }
        }

        #endregion
    }
}
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
    public class Driver_Messages_Processing_of_incoming_Driver_Messages : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // System is power on.SoM is performed until Level 1 is selected.Main window is closed.

            // Call the TestCaseBase PreExecution
            base.PreExecution();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in SB mode, level 1

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint


            /*
            Test Step 1
            Action: Use the test script file 15_3_2_a.xml to send EVC-8 with,MMI_Q_TEXT = 256MMI_Q_TEXT_CRITERIA = 3MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1MMI_N_TEXT = 2MMI_X_TEXT[0] = 42MMI_X_TEXT[1] = 43
            Expected Result: Verify the following information,(1)   The plain text message ‘*+’ is display in sub-area E5 without yellow flashing frame.(2)   The text message is presented with characters in bold style.(3)   Sound Sinfo is played.E.g.*+
            Test Step Comment: (1) MMI_gen 134 (partly: Touch screen, E5); MMI_gen 1699 (partly: non-acknowledgeable, important plain text message); (2) MMI_gen 11455 (partly: plain text message, bold style, Q_TEXT_CLASS = 1, the first group);(3) MMI_gen 11455 (partly: sound Sinfo); MMI_gen 9516 (partly: text message in first group); MMI_gen 12025 (partly: text message in first group); 
            */


            /*
            Test Step 2
            Action: (Continue from step 1)Send EVC-8 with,MMI_Q_TEXT = 580MMI_Q_TEXT_CRITERIA = 3MMI_Q_TEXT_CLASS = 0MMI_I_TEXT = 2MMI_N_TEXT = 2MMI_X_TEXT[0] = 42MMI_X_TEXT[1] = 42
            Expected Result: Verify the following information,(1)   The plain text message ‘**’ is display in sub-are E6 without yellow flashing frame.(2)   The text message is presented with characters in regular style.(3)   There is no sound played.(4)   There is no gap between the new text message and older message from previous step.E.g.*+**
            Test Step Comment: (1) MMI_gen 134 (partly: Touch screen, E6); MMI_gen 1699 (partly: non-acknowledgeable, auxiliary plain text message);(2) MMI_gen 11455 (partly: plain text message, regular style, Q_TEXT_CLASS = 0, the second group); MMI_gen 1699 (partly: The second group);(3) MMI_gen 11455 (partly: No sound used);(4) MMI_gen 136;  MMI_gen 134 (partly: E6); MMI_gen 135 (partly: The most recent one);     
            */


            /*
            Test Step 3
            Action: (Continue from step 2)Send EVC-8 with,MMI_Q_TEXT = 0MMI_Q_TEXT_CRITERIA = 3MMI_Q_TEXT_CLASS = 0MMI_I_TEXT = 3
            Expected Result: Verify the following information,(1)   Text message ‘Level crossing not protected’ is displayed in sub-area E6 without yellow flashing frame.(2)   The bold text message is still displayed above the regular messages. (3)  There is no sound played.(4)  The old text message ‘**’ is moved to sub-area E7. E.g.*+Level crossing not protected**
            Test Step Comment: (1) MMI_gen 11455 (partly:  classified the second group in chronological way); MMI_gen 135 (partly: added, the second group, chronological order); MMI_gen 1699 (partly: non-acknowledgeable, auxiliary fixed text message); (2) MMI_gen 11455 (partly: divided, fixed text message, the first group above the second group); MMI_gen 1699 (partly: The second group);      (3) MMI_gen 11455 (partly: No sound used); MMI_gen 138 (partly: NEGATIVE, no sound for text belong to second group);(4) MMI_gen 134 (partly: Touch screen, E7);
            */


            /*
            Test Step 4
            Action: (Continue from step 3)Send EVC-8 with,MMI_Q_TEXT = 273MMI_Q_TEXT_CRITERIA = 3MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 4
            Expected Result: Verify the following information,(1)   Text message ‘Unauthorize passing of EOA/LOA’ is displayed in sub-area E5-E6 without yellow flashing frame.(2)   Sound Sinfo is played.(3)   The old text messages are moved to sub-area E7-E9 respectively.(4)   The navigation buttons <Up> and <Down> at sub-area E10-E11 are disabled.(5)   DMI displays symbol NA15 at sub-area E10.(6)   DMI displays symbol NA16 at sub-area E11.E.g. Unauthorize passing of EOA/LOA  *+  Level crossing not protected  **
            Test Step Comment: (1) MMI_gen 135 (partly: The most recent one); MMI_gen 1699 (partly: non-acknowledgeable, important system status message); MMI_gen 138 (partly: Place at the topmost position in its group); (2) MMI_gen 138 (partly: Sound Sinfo); MMI_gen 9516 (partly: text message in first group); MMI_gen 12025 (partly: text message in first group);(3) MMI_gen 134 (partly: Touch screen, E8);(4) MMI_gen 1048 (partly: Not more than 5 lines, touch screen); MMI_gen 137 (partly: opposite case);(5) MMI_gen 1048 (partly: NA15);(6) MMI_gen 1048 (partly: NA16);
            */


            /*
            Test Step 5
            Action: (Continue from step 4)Send EVC-8 with,MMI_Q_TEXT = 625MMI_Q_TEXT_CRITERIA = 3MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 5
            Expected Result: Verify the following information,(1)   Text message ‘Tachometer error’ is displayed in sub-area E5 without yellow flashing frame.(2)   Sound Sinfo is played.(3)   The old text messages are moved to sub-area E6-E9 respectively.(4)   The navigation button <Down> is enabled.(5)   DMI displays symbol NA14 at sub-area E11.E.g.Shown messages:Tachometer error Unauthorize passing of EOA/LOA *+ Level crossing not protectedHidden messages:**
            Test Step Comment: (1) MMI_gen 135 (partly: The most recent one, topmost); MMI_gen 1699 (partly: non-acknowledgeable, important fixed text message); MMI_gen 138 (partly: not full, first group));(2) MMI_gen 138 (partly: Sound Sinfo); MMI_gen 9516 (partly: text message in first group); MMI_gen 12025 (partly: text message in first group);(3) MMI_gen 134 (partly: Touch screen, E9);(4) MMI_gen 140 (partly: enabled);(5) MMI_gen 140 (partly: NA14);
            */


            /*
            Test Step 6
            Action: (Continue from step 5)Send EVC-8 with,MMI_Q_TEXT = 712MMI_Q_TEXT_CRITERIA = 3MMI_Q_TEXT_CLASS = 0MMI_I_TEXT = 6
            Expected Result: Verify the following information,(1)   Text message ‘Wheel data settings were successfully changed’ is displayed in sub-area E9 without yellow flashing frame.Note: The text message is too long for 1 line. So, the message is not completely displayed in the visibility window.(2)   There is no sound played.E.g.Shown messages: Tachometer error  Unauthorize passing of EOA/LOA  *+  Wheel data settings were Hidden messages: successfully changed  Level crossing not protected  **
            Test Step Comment: (1) MMI_gen 138 (partly: Place at the topmost position in its group);(2) MMI_gen 11455 (partly: No sound used); MMI_gen 138 (partly: NEGATIVE, no sound for text belong to second group);
            */


            /*
            Test Step 7
            Action: (Continue from step 6)Send EVC-8 with,MMI_Q_TEXT = 583MMI_Q_TEXT_CRITERIA = 3MMI_Q_TEXT_CLASS = 0MMI_I_TEXT = 2
            Expected Result: Verify the following information,(1)   Text message ‘Doppler error’ is displayed in sub-area E9 without yellow flashing frame.(2)    There is no sound played.E.g.Shown messages: Tachometer error  Unauthorize passing of EOA/LOA  *+  Doppler error Hidden messages: Wheel data settings were successfully changed  Level crossing not protected
            Test Step Comment: (1) MMI_gen 148 (partly: add new text driver message, MMI_gen 138 (partly: topmost position));(2) MMI_gen 147 (partly: MMI_gen 138 (partly: NEGATIVE of moving the visibility window, no sound for text belong to second group));
            */


            /*
            Test Step 8
            Action: Press ‘Down’ button at sub-area E11
            Expected Result: Verify the following information,(1)   The navigation button <Up> is enabled.(2)   DMI displays symbol NA13 at sub-area E10.(3)   The visibility window is moves down to the 1st line of the next lower text message, text message ‘Wheel data settings were successfully changed' is displayed in sub-area E8-E9.E.g.Hidden messages (above): Tachometer error  Unauthorize passing ofShown messages: EOA/LOA  *+  Doppler error  Wheel data settings were successfully changedHidden messages (below): Level crossing not protected
            Test Step Comment: (1) MMI_gen 137 (partly: enabled);(2) MMI_gen 137 (partly: NA13);(3) MMI_gen 11455 (partly: Down button, area E11); MMI_gen 164 (partly: Down, scroll through); MMI_gen 134 (partly: visibility window); MMI_gen 143 (partly: 1st line of the next lower text message is not yet visible);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(@"Press ‘Down’ button at sub-area E11");


            /*
            Test Step 9
            Action: Press ‘Down’ button at sub-area E11
            Expected Result: Verify the following information,(1)   The visibility window is moves one line down, text message ‘Level crossing not protected’ is displayed at sub-area E9.(2)   The navigation buttons <Down> at sub-area E11 is disabled, display as symbol NA16
            Test Step Comment: (1) MMI_gen 143 (partly: opposite case, moves one line down); (2) MMI_gen 140 (partly: disabled);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(@"Press ‘Down’ button at sub-area E11");


            /*
            Test Step 10
            Action: Press ‘Down’ button at sub-area E11
            Expected Result: Verify the following information,(1)   The display information in sub-area E5-E9 are not changed
            Test Step Comment: (1) MMI_gen 164 (partly: Down, No wrap around); MMI_gen 11455 (partly: not be circular); MMI_gen 147 (partly:remove text driver message, MMI_I_TEXT = 2);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(@"Press ‘Down’ button at sub-area E11");
            // Call generic Check Results Method
            DmiExpectedResults
                .Verify_the_following_information_1_The_display_information_in_sub_area_E5_E9_are_not_changed();


            /*
            Test Step 11
            Action: Use the test script file 15_3_2_b.xml to send EVC-8 with to remove a text message, MMI_Q_TEXT = 0MMI_Q_TEXT_CRITERIA = 4MMI_Q_TEXT_CLASS = 0MMI_I_TEXT = 2
            Expected Result: Verify the following information,The fixed text message ‘Doppler error’ is removed.The text message ‘Wheel data settings were successfully changed’ at the lowest position is move up to close the gap, display in sub-area E9.The navigation buttons <Down> at sub-area E11 is disabled, display as symbol NA16. E.g.Hidden messages (above): Tachometer error  Unauthorize passing ofShown messages: EOA/LOA  *+  Wheel data settings were successfully changed  Level crossing not protected
            Test Step Comment: (1) MMI_gen 144 (partly: MMI_I_TEXT);(2) MMI_gen 144 (partly: move up to close the gap);(3) MMI_gen 140 (partly: disabled);
            */


            /*
            Test Step 12
            Action: Press ‘Up’ button at sub-area E10 3 times
            Expected Result: Verify the following information,(1)   The navigation buttons <Up> at sub-area E10 is disabled, display as symbol NA15.(2)   The visibility window is moved up to the next 1st line of a text message when the button is pressed.E.g.Hidden messages (above):“Shown messages:Tachometer error   Unauthorize passing of EOA/LOA  *+   Wheel data settings were Hidden messages (below): successfully changed  Level crossing not protected
            Test Step Comment: (1) MMI_gen 137 (partly: opposite case);(2) MMI_gen 1314; MMI_gen 11455 (partly: Up button, area E10); MMI_gen 164 (partly: Up, scroll through); MMI_gen 7050 (partly: E10);
            */


            /*
            Test Step 13
            Action: Press ‘Up’ button at sub-area E10
            Expected Result: Verify the following information,(1)   The display information in sub-area E5-E9 are not changed
            Test Step Comment: (1) MMI_gen 164 (partly: Up, No wrap around); MMI_gen 11455 (partly: not be circular);
            */
            // Call generic Check Results Method
            DmiExpectedResults
                .Verify_the_following_information_1_The_display_information_in_sub_area_E5_E9_are_not_changed();


            /*
            Test Step 14
            Action: Use the test script file 15_3_2_c.xml to send a multiple packet of EVC-8 to remove a text message,Common variables MMI_Q_TEXT = 0MMI_Q_TEXT_CRITERIA = 4MMI_Q_TEXT_CLASS = 0The order of MMI_I_TEXT value in each packetMMI_I_TEXT = 1MMI_I_TEXT = 3MMI_I_TEXT = 5MMI_I_TEXT = 6
            Expected Result: The text messages of “Tachometer error”, “*+”, “Wheel data settings were successfully changed” and “Level crossing not protected” disappear.E.g.Shown messages: Unauthorize passing of EOA/LOA
            */


            /*
            Test Step 15
            Action: Use the test script file 15_3_2_d.xml to send EVC-8 with,MMI_Q_TEXT = 261MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 0MMI_I_TEXT = 1
            Expected Result: Verify the following information,(1)    The new incoming message “Fixed Text Message 261” is display in sub-area E6.E.g.Shown messages: Unauthorize passing of EOA/LOA  Fixed Text Message 261
            Test Step Comment: (1) MMI_gen 148 (indexing, auxiliary);
            */


            /*
            Test Step 16
            Action: (Continue from step 15) Send EVC-8 withMMI_Q_TEXT = 270MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 3
            Expected Result: Verify the following information,(1)    The new incoming message “Fixed Text Message 270” is display in sub-area E5.(2)    Sound Sinfo is played.E.g.Shown messages:  Fixed Text Message 270  Unauthorize passing of EOA/LOA  Fixed Text Message 261
            Test Step Comment: (1) MMI_gen 148 (indexing, important); (2) MMI_gen 11455 (partly: sound); MMI_gen 138 (partly: sound); MMI_gen 9516 (partly: text message in first group); MMI_gen 12025 (partly: text message in first group);
            */


            /*
            Test Step 17
            Action: (Continue from step 16) Send EVC-8 withMMI_Q_TEXT = 271MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 5
            Expected Result: Verify the following information,(1)    The new incoming message “Fixed Text Message 271” is display in sub-area E5. (2)    Sound Sinfo is played.E.g.Shown messages:  Fixed Text Message 271  Fixed Text Message 270  Unauthorize passing of EOA/LOA  Fixed Text Message 261
            Test Step Comment: (1) MMI_gen 148 (indexing, important);(2) MMI_gen 11455 (partly: sound); MMI_gen 138 (partly: sound); MMI_gen 9516 (partly: text message in first group); MMI_gen 12025 (partly: text message in first group);
            */


            /*
            Test Step 18
            Action: (Continue from step 17) Send EVC-8 withMMI_Q_TEXT = 272MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 0MMI_I_TEXT = 6
            Expected Result: Verify the following information,(1)    The new incoming message “Fixed Text Message 272” is display in sub-area E9.E.g.Shown messages:  Fixed Text Message 271  Fixed Text Message 270  Unauthorize passing of EOA/LOAFixed Text Message 272Hidden messages: Fixed Text Message 261
            Test Step Comment: (1) MMI_gen 148 (indexing, auxiliary);
            */


            /*
            Test Step 19
            Action: Press ‘Down’ button.Then, use the test script file 15_3_2_e.xml to send EVC-8 to remove text message,MMI_Q_TEXT = 261MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 4MMI_I_TEXT = 1
            Expected Result: Verify the following information,(1)    The new incoming message “Fixed Text Message 261” is removed from sub-area E9.E.g.Shown messages:  Fixed Text Message 271  Fixed Text Message 270  Unauthorize passing of EOA/LOAFixed Text Message 272
            Test Step Comment: (1) MMI_gen 148 (deleting, auxiliary);
            */


            /*
            Test Step 20
            Action: (Continue from step 19)Send EVC-8 with,MMI_Q_TEXT = 271MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 4MMI_I_TEXT = 5
            Expected Result: Verify the following information,(1)    The new incoming message “Fixed Text Message 271” is removed from sub-area E5.E.g.Shown messages:  Fixed Text Message 270  Unauthorize passing of EOA/LOA  Fixed Text Message 272
            Test Step Comment: (1) MMI_gen 148 (deleting, important);
            */


            /*
            Test Step 21
            Action: End of test
            Expected Result: 
            */


            return GlobalTestResult;
        }
    }
}
using System;
using Testcase.Telegrams.EVCtoDMI;
using Testcase.Telegrams.DMItoEVC;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 27.10 Special window
    /// TC-ID: 22.10
    /// 
    /// This test case verifies the display of the ‘Special’ window that shall comply with [ERA-ERTMS] standard and [MMI-ETCS-gen]
    /// 
    /// Tested Requirements:
    /// MMI_gen 8436 (partly: touch screen); MMI_gen 8435; MMI_gen 600; MMI_gen 8434 (partly MMI_gen 7909, MMI_gen 4557, MMI_gen 4556 (partly: Close button, Window Title), MMI_gen 4630 (partly: MMI gen 5944 (partly: touch screen))); MMI_gen 8438-1 (THR); MMI_gen 11450-1 (THR); MMI_gen 4388; MMI_gen 11907; MMI_gen 1779; MMI_gen 9512, MMI_gen 968; MMI_gen 4381; MMI_gen 4382; MMI_gen 12138 (partly:adhesion window); MMI_gen 4360 (partly: window title); MMI_gen 4355 (partly: Buttons, Close button); MMI_gen 4392 (partly: [Close] NA11, returning to the parent window); MMI_gen 1706; MMI_gen 3375; MMI_gen 1088 (partly, Bit #10 to #12); MMI_gen 4350; MMI_gen 4351; MMI_gen 4353; MMI_gen 4354;
    /// 
    /// Scenario:
    /// The concerned buttons in the ‘Special’ window are verified by the following actions:Press the button once (for the Delay-Type button)Press the button and holdSlide the button out with force appliedSlide the button back with force appliedRelease the buttonThe ‘Special window is opened and verified.The Safe Delay-Type ‘Train Integrity’ button is verified.The Up-Type ‘SR speed/distance’ button is verified.Drive train forward to pass BG
    /// 1.Then, verify the state of Adhesion button.Packet number 3 with variable Q_NVDRIVER_AHES = 0 to reset the enabling condition. (Initial BG at position 50m)Packet number 3 with variable Q_NVDRIVER_AHES = 1 to enable modification of the adhesion factor.Drive the train forward while Special window is opened, Then, verify the state of SR speed/distance and Train integrity button.Stop the train. Then, verify the state of SR speed/distance and Train integrity button.
    /// 
    /// Used files:
    /// 22_10.tdg
    /// </summary>
    public class TC_ID_22_10_Special_window : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // Call the TestCaseBase PreExecution

            base.PreExecution();

            // Test system is powered on Cabin A is activatedSoM is completed in SR mode, level 1
            DmiActions.Complete_SoM_L1_SR(this);
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint


            /*
            Test Step 1
            Action: Press ‘Spec’ button
            Expected Result: DMI displays Special window.Verify the following information,Menu window(1)   TheSpecial window is displayed in main area D/F/G.(2)   The window title is ‘Special’.(3)   The following objects are displayed in Main window, Enabled Close button (NA11)Window TitleButton 1 with label ‘Adhesion’Button 2 with label ‘SR speed /distance’Button 3 with label ‘Train integrity’ Note: See the position of buttons in picture below,(4)   The state of each button in Special window are displayed correctly as follows ,SR speed/distance = EnableAdhesion = DisableTrain integrity = Enable(5)   Use the log file to confirm that DMI receives packet EVC-30 with the value of following bit of MMI_Q_REQUEST_ENABLE_64Bit #10  = 0 (Adhesion disabled)Bit #11 = 1 (SR speed/distance enabled)Bit #12 = 1 (Train Integrity enabled)Layers(6)   The level of layers in each area of window as follows,Layer 0: Area D, F, G, E10, E11, Y, and ZLayer -1: Area A1, (A2+A3)*, A4, B*, C1, (C2+C3+C4)*, C5, C6, C7, C8, C9, E1, E2, E3, E4, (E5-E9)*.Layer -2: Area B3, B4, B5, B6 and B7.Note: ‘*’ symbol is mean that specified area are drawn as one area.General property of window(7)   The Special window is presented with objects and buttons which is the one of several levels and allocated to areas of DMI.(8)   All objects, text messages and buttons are presented within the same layer.(9)   The Default window is not displayed and covered the current window.(10)   Sub-level window covers partially depending on the size of the Sub-Level window. There is no other window is displayed and activated at the same time
            Test Step Comment: (1) MMI_gen 8434 (partly MMI_gen 7909);   (2) MMI_gen 8435; MMI_gen 4360 (partly: window title);(3) MMI_gen 8434 (partly: MMI_gen 4556 (partly: Close button, Window Title)); MMI_gen 4355 (partly: Buttons, Close button); MMI_gen 8436 (partly: touch screen, button with label, Adhesion, SR speed/distance, Train integrity); MMI_gen 4392 (partly: [Close] NA11);(4) MMI_gen 600 (partly: enabling #11 and #12, disabling #10);(5) MMI_gen 600 (partly: EVC-30, enabling #11 and #12, disabling #10); MMI_gen 1088 (partly, Bit #10 to #12)(6) MMI_gen 8434 (partly: MMI_gen 4630, MMI gen 5944 (partly: touch screen));(7) MMI_gen 4350;(8) MMI_gen 4351;(9) MMI_gen 4353;(10) MMI_gen 4354;
            */
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Default;
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.None;
            EVC30_MMIRequestEnable.Send(); // Just to make sure that all are disabled
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = (EVC30_MMIRequestEnable.EnabledRequests.SRSpeedDistance |
                                                                EVC30_MMIRequestEnable.EnabledRequests.TrainIntegrity) &
                                                               ~EVC30_MMIRequestEnable.EnabledRequests.Adhesion;
            EVC30_MMIRequestEnable.Send();

            DmiActions.ShowInstruction(this, @"Press ‘Spec’ button");

            // Spec says some objects are displayed in the Main window. Doh!
            WaitForVerification("Check the following  (* indicates sub-areas drawn as one area):" +
                                Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Special window with the title ‘Special’." + Environment.NewLine +
                                "2. The Special window is displayed in areas D, F and G with 3 layers." +
                                Environment.NewLine +
                                "3. The window displays an enabled ‘Close’ button, symbol NA11, and three buttons (in two lines)." +
                                Environment.NewLine +
                                "4. Button #1 (disabled) is labeled ‘Adhesion’" + Environment.NewLine +
                                "5. Button #2 (enabled) is labeled ‘SR speed / distance’" + Environment.NewLine +
                                "6. Button #3 (enabled) is labeled ‘Train integrity’" + Environment.NewLine +
                                "7. Layer 0 comprises areas D, F, G, E10, E11, Y, and Z" + Environment.NewLine +
                                "8. Layer 1 comprises area A1, (A2 + A3)*, A4, B*, C1, (C2 + C3 + C4)*, C5, C6, C7, C8, C9, E1, E2, E3, E4, (E5 - E9)*." +
                                Environment.NewLine +
                                "9. Layer 2 comprises areas B3, B4, B5, B6 and B7." + Environment.NewLine +
                                "10. Objects, text messages and buttons can be displayed in several levels. Within a level they are allocated to areas." +
                                Environment.NewLine +
                                "11. Objects, text messages and buttons in a layer form a window." +
                                Environment.NewLine +
                                "12. The Default window does not cover the current window." + Environment.NewLine +
                                "13. A sub-level window can partially cover another window, depending on its size. Another window cannot be displayed and activated at the same time.");

            /*
            Test Step 2
            Action: Press ‘Train integrity’ button
            Expected Result: Verify the following information,The ‘Train Integrity’ button becomes state ‘Pressed’, then state ‘Enabled’ once the button is immediately releasedThe Special window is still displayed.The ‘Click’ sound is played once.Use the log file to confirm that DMI sends EVC-101 twice with different value of MMI_T_BUTTONEVENT and MMI_Q_BUTTON (1 = pressed, 0 = released)
            Test Step Comment: (1) MMI_gen 8438-1 (THR) (partly: button ‘Train integrity’, MMI_gen 11450-1 (THR) (partly: Delay-Type button, MMI_gen 4388 (partly: less than the 2 seconds, return to state ‘Enabled’)));(2) MMI_gen 8438-1 (THR) (partly: button ‘Train integrity’, MMI_gen 11450-1 (THR) (partly: Delay-Type button, MMI_gen 4388 (partly: less than the 2 seconds, no valid button activation considered by onboard)));(3) MMI_gen 8438-1 (THR) (partly: button ‘Train integrity’, MMI_gen 11450-1 (THR) (partly: Delay-Type button, MMI_gen 4388 (partly: the sound for button Delay-Type))); MMI_gen 9512, MMI_gen 968;(4) MMI_gen 8438-1 (THR) (partly: button ‘Train integrity’, MMI_gen 11450-1 (THR) (partly: send events of Pressed and Released independently to ETCS), MMI_gen 11907 (partly: EVC-101, timestamp)); MMI_gen 3375;
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Train integrity’ button and immediately release it");

            EVC101_MMIDriverRequest.CheckMRequestReleased = Variables.MMI_M_REQUEST.StartProcedureTrainIntegrity;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The button is displayed pressed and immediately re-displayed enabled." +
                                Environment.NewLine +
                                "2. DMI still displays the Special window." + Environment.NewLine +
                                "3. The ‘Click’ sound is played once." + Environment.NewLine);

            /*
            Test Step 3
            Action: Press and hold ‘Train integrity’ button for 2s.Note: Stopwatch is required for accuracy of test result
            Expected Result: Verify the following information,While press and hold button less than 2 secThe ‘Click’ sound is played once.The state of button is changed to ‘Pressed’.The state ‘pressed’ and ‘enabled’ are switched repeatly while button is pressed. Use the log file to confirm that DMI sends EVC-101 with variable MMI_T_BUTTONEVENT and MMI_Q_BUTTON = 1 (pressed).While press and hold button over 2 secThe state of button is changed to ‘Pressed’ and without toggle
            Test Step Comment: (1)  MMI_gen 8438-1 (THR) (partly: button ‘Train integrity’, MMI_gen 11450-1 (THR) (partly: Delay-Type button, MMI_gen 4388 (partly: the sound for button Delay-Type))); MMI_gen 9512; MMI_gen 968;(2) MMI_gen 8438-1 (THR) (partly: button ‘Train integrity’, MMI_gen 11450-1 (THR) (partly: Delay-Type button, MMI_gen 4388 (partly: change to pressed state)));(3) MMI_gen 8438-1 (THR) (partly: button ‘Train integrity’, MMI_gen 11450-1 (THR) (partly: Delay-Type button, MMI_gen 4388 (partly: toggle between the “pressed” and “enabled” states as long as the button remains pressed by the driver)));(4) MMI_gen 8438-1 (THR) (partly: button ‘Train integrity’, MMI_gen 11450-1 (THR) (partly: send events of Pressed independently to ETCS), MMI_gen 11907 (partly: EVC-101, timestamp))); MMI_gen 3375;(5) MMI_gen 8438-1 (THR) (partly: button ‘Train integrity’, MMI_gen 11450-1 (THR) (partly: Delay-Type button, MMI_gen 4388 (partly: after 2 seconds, the button is change again to the state ‘Pressed’)));
            */
            DmiActions.ShowInstruction(this, @"Press and hold the ‘Train integrity’ button for more than 2s");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Whilst the button has been pressed for less than 2s, the button is repeatedly displayed pressed then immediately re-displayed enabled;" +
                                Environment.NewLine +
                                "2. The ‘Click’ sound is played once." + Environment.NewLine +
                                "3. When the button has been pressed for more than 2s, it is displayed pressed (without toggling).");

            /*
            Test Step 4
            Action: Slide out from the “Train Integrity” button
            Expected Result: Verify the following information,The ‘Train Integrity’ button turns to the ‘Enabled’ state without a sound
            Test Step Comment: (1) MMI_gen 8438-1 (THR) (partly: button ‘Train Integrity’, MMI_gen 11450-1 (THR) (partly: Delay-Type button, MMI_gen 4389 (partly: state ‘Enabled’ when slide out with force applied (stop toggling state ‘Pressed’ and ‘Enabled’), no sound)))
            */
            DmiActions.ShowInstruction(this,
                @"Whilst keeping the ‘Train Integrity’ button pressed, drag it out of its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘Train Integrity’ button is displayed enabled." + Environment.NewLine +
                                "2. No sound is played.");

            /*
            Test Step 5
            Action: Slide back to the “Train integrity’ button and hold it for 1 seconds. Then, slide out again.Note: Stopwatch is required for accuracy of test result
            Expected Result: Verify the following information, (1)  The ‘Train Integrity’ button turns to the ‘Enabled’ state without a sound
            Test Step Comment: (1) MMI_gen 8438-1 (THR) (partly: button ‘Train nintegrity’, MMI_gen 11450-1 (THR) (partly: Delay-Type button, MMI_gen 4388 (partly: to reset toggling state ‘Pressed’ and ‘Enabled’, no sound), MMI_gen 4389 (partly: to reset toggling state ‘Pressed’ and ‘Enabled’, no sound)));
            */
            DmiActions.ShowInstruction(this,
                @"Whilst keeping the ‘Train Integrity’ button pressed, drag it back inside its area for 1s, then drag it outside again");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘Train Integrity’ button is displayed enabled." + Environment.NewLine +
                                "2. No sound is played.");

            /*
            Test Step 6
            Action: Slide back to the “Train Integrity” button and hold it for 2 seconds.Note: Stopwatch is required for accuracy of test result
            Expected Result: While press and hold button less than 2 secThe state ‘pressed’ and ‘enabled’ are switched repeatly while button is pressed without a sound. While press and hold button over 2 secThe state of button is changed to ‘Pressed’ and without toggle
            Test Step Comment: (1) MMI_gen 8438-1 (THR) (partly: button ‘Train Integrity’, MMI_gen 11450-1 (THR) (partly: Delay-Type button, MMI_gen 4389 (partly: start toggling state ‘Pressed’ and ‘Enabled’ when slide back, no sound)));(2) MMI_gen 8438-1 (THR) (partly: button ‘Train Integrity’, MMI_gen 11450-1 (THR) (partly: Delay-Type button, MMI_gen 4388 (partly: after 2 seconds, the button is change again to the state ‘Pressed’)));
            */
            DmiActions.ShowInstruction(this,
                @"Whilst keeping the ‘Train Integrity’ button pressed, drag it back inside its area for more than 2s");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. Whilst The ‘Train Integrity’ button has been held pressed in its area for less than 2s it is repeatedly displayed pressed then immediately re-displayed enabled;" +
                                Environment.NewLine +
                                "2. No sound is played." + Environment.NewLine +
                                "3. When the button has been pressed for more than 2s, it is displayed pressed (without toggling).");

            /*
            Test Step 7
            Action: Release ‘Train integrity’ button
            Expected Result: DMI displays Default window.Verify the following information,Use the log file to confirm that DMI sends EVC-101 with the variable MMI_M_REQUEST = 38 (Start procedure ‘Train Integrity’)
            Test Step Comment: (1) MMI_gen 8438-1 (THR) (partly: button ‘Train Integrity’, MMI_gen 11450-1 (THR) (partly: send events of Released to ETCS, MMI_gen 4388 (partly: after 2 seconds button Up-Type is followed, button Delay-Type, MMI_gen 4381 (partly: exit state ‘Pressed’, execute function associated to the button))), MMI_gen 11907 (partly: EVC-101, timestamp)), MMI_gen 1779; MMI_gen 3375;
            */
            DmiActions.ShowInstruction(this, @"Release the ‘Train integrity’ button");

            EVC101_MMIDriverRequest.CheckMRequestReleased = Variables.MMI_M_REQUEST.StartProcedureTrainIntegrity;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Default window");

            /*
            Test Step 8
            Action: Press ‘Spec’ button
            Expected Result: DMI displays Special window
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Spec’ button");

            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Settings;
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.None;
            EVC30_MMIRequestEnable.Send(); // Just to make sure that all are disabled
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = (EVC30_MMIRequestEnable.EnabledRequests.SRSpeedDistance |
                                                                EVC30_MMIRequestEnable.EnabledRequests.TrainIntegrity) &
                                                               ~EVC30_MMIRequestEnable.EnabledRequests.Adhesion;
            EVC30_MMIRequestEnable.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Special window");

            /*
            Test Step 9
            Action: Press and hold ‘SR speed/distance’ button
            Expected Result: Verify the following information,The sound ‘Click’ is played once.The ‘SR speed/distance’ button is shown as pressed state, the border of button is removed
            Test Step Comment: (1) MMI_gen 8434 (partly: MMI_gen 4557 (partly: SR speed/distance),  MMI_gen 4381 (partly: the sound for Up-Type button))); MMI_gen 9512; MMI_gen 968;(2) MMI_gen 8434 (partly: MMI_gen 4557 (partly: SR speed/distance), MMI_gen 4381 (partly: change to state ‘Pressed’ as long as remain actuated))); MMI_gen 4375;
            */
            DmiActions.ShowInstruction(this, @"Press and hold the ‘SR speed/distance’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The button is displayed pressed, without a border" + Environment.NewLine +
                                "2. No sound is played.");

            /*
            Test Step 10
            Action: Slide out of ‘SR speed/distance’ button
            Expected Result: The border of the button is shown (state ‘Enabled’) without a sound
            Test Step Comment: MMI_gen 8434 (partly: MMI_gen 4557 (partly: SR speed/distance), MMI_gen 4382 (partly: state ‘Enabled’ when slide out with force applied, no sound))); MMI_gen 4374;
            */
            DmiActions.ShowInstruction(this,
                @"Whilst keeping the ‘SR speed/distance’ button pressed, drag it out of its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘SR speed/distance’ button is displayed enabled, with a border." +
                                Environment.NewLine +
                                "2. The ‘Click’ sound is played once.");

            /*
            Test Step 11
            Action: Slide back into ‘SR speed/distance’ button
            Expected Result: The button is back to state ‘Pressed’ without a sound
            Test Step Comment: MMI_gen 8434 (partly: MMI_gen 4557 (partly: SR speed/distance), MMI_gen 4382 (partly: state ‘Pressed’ when slide back, no sound))); MMI_gen 4375;
            */
            DmiActions.ShowInstruction(this,
                @"Whilst keeping the ‘SR speed/distance’ button pressed, drag it back inside its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘SR speed/distance’ button is displayed pressed." + Environment.NewLine +
                                "2. No sound is played.");

            /*
            Test Step 12
            Action: Release ‘SR speed/distance’ button
            Expected Result: Verify the following information,DMI displays SR speed/distance window.Use the log file to confirm that DMI sends out the packet [MMI_DRIVER_REQUEST (EVC-101)] with variable [MMI_DRIVER_REQUEST (EVC-101).MMI_M_REQUEST = 13 (Change SR rules)
            Test Step Comment: (1) MMI_gen 8434 (partly: MMI_gen 4557 (partly: SR speed/distance), MMI_gen 4381 (partly: exit state ‘Pressed’, execute function associated to the button))); (2) MMI_gen 1706;
            */
            DmiActions.ShowInstruction(this, @"Release the ‘SR speed/distance’ button");

            EVC101_MMIDriverRequest.CheckMRequestReleased = Variables.MMI_M_REQUEST.ChangeSRrules;

            EVC11_MMICurrentSRRules.MMI_M_BUTTONS = Variables.MMI_M_BUTTONS.BTN_YES_DATA_ENTRY_COMPLETE;
            EVC11_MMICurrentSRRules.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the SR speed/distance window");

            /*
            Test Step 13
            Action: Follow action step 9 – step 12 for the ‘Close’ button
            Expected Result: See the expected results of Step 9 – Step 12 and the following additional information, (1) DMI displays Special window refer to released button
            Test Step Comment: (1) MMI_gen 8434 (partly: MMI_gen 4557 (partly: button ‘Close’));
            */
            // Repeat Step 9 for the Close button
            DmiActions.ShowInstruction(this, @"Press and hold the ‘Close’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The button is displayed pressed, without a border" + Environment.NewLine +
                                "2. No sound is played.");

            // Repeat Step 10 for the Close button
            DmiActions.ShowInstruction(this, @"Whilst keeping the ‘Close’ button pressed, drag it out of its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘Close’ button is displayed enabled, with a border." + Environment.NewLine +
                                "2. The ‘Click’ sound is played once.");

            // Repeat Step 11 for the Close button
            DmiActions.ShowInstruction(this,
                @"Whilst keeping the ‘Close’ button pressed, drag it back inside its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘Close’ button is displayed pressed." + Environment.NewLine +
                                "2. No sound is played.");

            // Repeat Step 12 for the Close button
            DmiActions.ShowInstruction(this, @"Release the ‘Close’ button");

            EVC101_MMIDriverRequest.CheckMRequestReleased = Variables.MMI_M_REQUEST.ExitChangeSRrules;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Special window");

            /*
            Test Step 14
            Action: Drive the train forward pass BG1
            Expected Result: Verify the following information,Adhesion button is enabled.Use the log file to confirm that DMI receives packet EVC-30 with variable MMI_Q_REQUEST_ENABLE_64Bit #10 = 1 (Adhesion Enabled)
            Test Step Comment: (1) MMI_gen 600 (partly: enabling #10);(2) MMI_gen 600 (partly: enabling #10, EVC-30);
            */
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.No_window_specified;
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.None;
            EVC30_MMIRequestEnable.Send(); // Just to make sure that all are disabled
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.SRSpeedDistance |
                                                               EVC30_MMIRequestEnable.EnabledRequests.TrainIntegrity |
                                                               EVC30_MMIRequestEnable.EnabledRequests.Adhesion;
            EVC30_MMIRequestEnable.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Adhesion’ button is displayed enabled");

            /*
            Test Step 15
            Action: Stop the train.Then, Follow action step 9 – step 12 for the ‘Adhesion’ button
            Expected Result: See the expected results of Step 9 – Step 12 and the following additional information, DMI displays Adhesion window refer to released button
            Test Step Comment: (1) MMI_gen 8434 (partly: MMI_gen 4557 (partly: Adhesion button) MMI_gen 4381 (partly: exit state ‘Pressed’, execute function associated to the button))); MMI_gen 12138;
            */
            // Repeat Step 9 for the Adhesion button
            DmiActions.ShowInstruction(this, @"Press and hold the ‘Adhesion’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The button is displayed pressed, without a border" + Environment.NewLine +
                                "2. No sound is played.");

            // Repeat Step 10 for the Adhesion button
            DmiActions.ShowInstruction(this, @"Whilst keeping the ‘Adhesion’ button pressed, drag it out of its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘Adhesion’ button is displayed enabled, with a border." + Environment.NewLine +
                                "2. The ‘Click’ sound is played once.");

            // Repeat Step 11 for the Adhesion button
            DmiActions.ShowInstruction(this,
                @"Whilst keeping the ‘Adhesion’ button pressed, drag it back inside its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘Adhesion’ button is displayed pressed." + Environment.NewLine +
                                "2. No sound is played.");

            // Repeat Step 12 for the Adhesion button
            DmiActions.ShowInstruction(this, @"Release the ‘Adhesion’ button");

            // DMI_RS_ETCS does not specify a msg when adhesion button is pressed/released
            //EVC101_MMIDriverRequest.CheckMRequestReleased = Variables.MMI_M_REQUEST.ExitChangeSRrules;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Adhesion window");

            /*
            Test Step 16
            Action: Press ‘Close’ button
            Expected Result: DMI displays Special window
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Special window");

            /*
            Test Step 17
            Action: Drive the train forward
            Expected Result: Verify the following information,The following buttons are disabled,SR speed/distanceTrain integrityUse the log file to confirm that DMI receives EVC-30 with following value in each bit of variable MMI_Q__REQUEST_ENABLE_64,Bit #11 = 0 (SR speed/distance disabled)Bit #12 = 0 (Train integrity disabled)
            Test Step Comment: (1) MMI_gen 600 (partly: Disabling, #11, #12);(2) MMI_gen 600 (partly: Disabling, EVC-30, #11, #12);
            */
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Settings;
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.None;
            EVC30_MMIRequestEnable.Send(); // Just to make sure that all are disabled
            // Spec not clear whether adhesion is enabled
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.Adhesion;
            EVC30_MMIRequestEnable.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘SR speed/distance’ button is displayed disabled" + Environment.NewLine +
                                "2. The ‘Train Integrity’ button is displayed disabled");

            /*
            Test Step 18
            Action: Stop the train
            Expected Result: Verify the following information,The following buttons are enabled,SR speed/distanceTrain integrityUse the log file to confirm that DMI receives EVC-30 with following value in each bit of variable MMI_Q__REQUEST_ENABLE_64,Bit #11 = 1 (SR speed/distance enabled)Bit #12 = 1 (Train integrity enabled)
            Test Step Comment: (1) MMI_gen 600 (partly: Enabling, #11, #12);(2) MMI_gen 600 (partly: Enabling, EVC-30, #11, #12);
            */
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Settings;
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.None;
            EVC30_MMIRequestEnable.Send(); // Just to make sure that all are disabled
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.Adhesion |
                                                               EVC30_MMIRequestEnable.EnabledRequests.SRSpeedDistance |
                                                               EVC30_MMIRequestEnable.EnabledRequests.TrainIntegrity;
            EVC30_MMIRequestEnable.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘SR speed/distance’ button is displayed enabled" + Environment.NewLine +
                                "2. The ‘Train Integrity’ button is displayed enabled");

            /*
            Test Step 19
            Action: Press the ‘Close’ button
            Expected Result: Verify the following information,(1)   DMI dipslays Default window
            Test Step Comment: (1) MMI_gen 4392 (partly: returning to the parent window);
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button");

            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Main;
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.None;
            EVC30_MMIRequestEnable.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Default window");

            /*
            Test Step 20
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
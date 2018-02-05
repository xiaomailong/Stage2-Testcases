using System;
using Testcase.Telegrams.EVCtoDMI;
using Testcase.Telegrams.DMItoEVC;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 27.2 Main window
    /// TC-ID: 7.1
    /// 
    /// This test case verifies the display of the menu buttons in ‘Main’ window for touch screen technology which complies with [ERA-ERTMS] standard and [MMI-ETCS-gen].
    /// 
    /// Tested Requirements:
    /// MMI_gen 8349 (partly: MMI_gen 7909, MMI_gen 4557, MMI_gen 4630 (partly: MMI_gen 5944 (partly: touch screen)), MMI_gen 4556 (partly: Close button, Window Title));MMI_gen 8350; MMI_gen 1205; MMI_gen 2490; MMI_gen 11908; MMI_gen 11909; MMI_gen 11910; MMI_gen 9954; MMI_gen 1423; MMI_gen 8354-1 (THR); MMI_gen 8351 (partly: touch screen); MMI_gen 2479; MMI_gen 11236; MMI_gen 11450-1 (THR); MMI_gen 591; MMI_gen 4355 (partly: Buttons, Close button); MMI_gen 4360 (partly: window title); MMI_gen 4392 (partly: [Close] NA11, returning to the parent window); MMI_gen 11911; MMI_gen 4389 (partly: Main window); MMI_gen 4350; MMI_gen 4351;MMI_gen 4353; MMI_gen 4354; MMI_gen 4377 (partly: shown);
    /// 
    /// Scenario:
    /// The concerned buttons in the ‘Main’ window are verified by the following actions:Press the button once (for the Delay-Type button)Press the button and holdSlide the button out with force appliedSlide the button back with force appliedRelease the buttonSoM is performed until the train running number is entered.The Up-Type ‘Start’ is verified.The ‘Main’ menu window is opened and verified.The Up-Type ‘Driver ID’, ‘Level’, ‘Train data’, and ‘Train running number’ are verified.The Safe Delay-Type ‘Non-leading’, ‘Shunting’, ‘Shunting Exit’, ‘Maintain Shunting’ buttons are verified.Note: ‘Non-leading’ and ‘Maintain Shunting’ signals is required to be simulated by OTE simulation.The enabling/disabling buttons are verified after preconditions, the ‘Non-leading’, ‘Shunting’, ‘Shunting Exit’, and ‘Maintain Shunting’ button actuated.
    /// 
    /// Used files:
    /// 7_1_a.xml, 7_1_b.xml
    /// </summary>
    public class TC_ID_7_1_Main_window : TestcaseBase
    {

        public override bool TestcaseEntryPoint()
        {
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 0;
            // Testcase entrypoint
            // Test system is powered onCabin is activeSoM is performed until the train data validated.
            StartUp();
            DmiActions.Set_Driver_ID(this, "1234");
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Level = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_LEVEL.L1;
            DmiActions.Display_Main_Window_with_Start_button_enabled(this);

            MakeTestStepHeader(1, UniqueIdentifier++, "Enter and confirm the Train running number",
                "DMI displays Main window.Use the log file to confirm that DMI received packet EVC-30 with the variable MMI_Q_REQUEST_ENABLE_64 (#0) = 1 (Enable Start) and the ‘Start’ button is enabled.The Main window is presented with objects and buttons which is the one of several levels and allocated to areas of DMI. All objects, text messages and buttons in Main window are presented within the same layer.The Default window is not displayed and covered the current window.Sub-level window covers partially depending on the size of the Sub-Level window. There is no other window is displayed and activated at the same time");
            /*
            Test Step 1
            Action: Enter and confirm the Train running number
            Expected Result: DMI displays Main window.Use the log file to confirm that DMI received packet EVC-30 with the variable MMI_Q_REQUEST_ENABLE_64 (#0) = 1 (Enable Start) and the ‘Start’ button is enabled.The Main window is presented with objects and buttons which is the one of several levels and allocated to areas of DMI. All objects, text messages and buttons in Main window are presented within the same layer.The Default window is not displayed and covered the current window.Sub-level window covers partially depending on the size of the Sub-Level window. There is no other window is displayed and activated at the same time
            Test Step Comment: (1) MMI_gen 8351 (partly: touch screen, enabling condition of ‘Start’ button); MMI_gen 591 (partly: EVC-30, enabling, start);(2) MMI_gen 4350;(3) MMI_gen 4351;(4) MMI_gen 4353;(5) MMI_gen 4354;
            */
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Main;
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH =
                EVC30_MMIRequestEnable.EnabledRequests.TrainRunningNumber |
                EVC30_MMIRequestEnable.EnabledRequests.Start;
            EVC30_MMIRequestEnable.Send();
            EVC16_CurrentTrainNumber.Send();
            DmiActions.ShowInstruction(this, @"Enter and confirm the Train running number");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Main window." + Environment.NewLine +
                                "2. Objects, text messages and buttons can be displayed in several levels. Within a level they are allocated to areas." +
                                Environment.NewLine +
                                "3. Objects, text messages and buttons in a layer form a window." +
                                Environment.NewLine +
                                "4. The Default window does not cover the current window." + Environment.NewLine +
                                "5. A sub-level window can partially cover another window, depending on its size. Another window cannot be displayed and activated at the same time.");

            MakeTestStepHeader(2, UniqueIdentifier++, "Press and hold ‘Start’ button",
                "Verify the following points,The sound ‘Click’ played once.The ‘Start’ button is shown as ‘Pressed’ state, the border of button is removed");
            /*
            Test Step 2
            Action: Press and hold ‘Start’ button
            Expected Result: Verify the following points,The sound ‘Click’ played once.The ‘Start’ button is shown as ‘Pressed’ state, the border of button is removed
            Test Step Comment: (1) MMI_gen 8349 (partly: MMI_gen 4557 (partly: button ‘Start’, MMI_gen 4381 (partly: the sound for Up-Type button))), MMI_gen 9512, MMI_gen 968;(2) MMI_gen 8349 (partly: MMI_gen 4557 (partly: button ‘Start’, MMI_gen 4381 (partly: change to state ‘Pressed’ as long as remain actuated))); MMI_gen 4375;
            */
            DmiActions.ShowInstruction(this, @"Press and hold the ‘Start’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the ‘Start’ button pressed, without a border." + Environment.NewLine +
                                "2. The ‘Click’ sound is played once.");

            MakeTestStepHeader(3, UniqueIdentifier++, "Slide out of ‘Start’ button",
                "The border of the button is shown (state ‘Enabled’) without a sound");
            /*
            Test Step 3
            Action: Slide out of ‘Start’ button
            Expected Result: The border of the button is shown (state ‘Enabled’) without a sound
            Test Step Comment: MMI_gen 8349 (partly: MMI_gen 4557 (partly: button ‘Start’, MMI_gen 4382 (partly: state ‘Enabled’ when slide out with force applied, no sound))); MMI_gen 4374;
            */
            DmiActions.ShowInstruction(this, @"Whilst keeping the ‘Start’ button pressed, drag outside its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the ‘Start’ button enabled, with a border." + Environment.NewLine +
                                "2. No sound is played.");

            MakeTestStepHeader(4, UniqueIdentifier++, "Slide back into ‘Start’ button",
                "The button is back to state ‘Pressed’ without a sound");
            /*
            Test Step 4
            Action: Slide back into ‘Start’ button
            Expected Result: The button is back to state ‘Pressed’ without a sound
            Test Step Comment: MMI_gen 8349 (partly: MMI_gen 4557 (partly: button ‘Start’, MMI_gen 4382 (partly: state ‘Pressed’ when slide back, no sound))); MMI_gen 4375;
            */
            DmiActions.ShowInstruction(this,
                @"Whilst keeping the ‘Start’ button pressed, drag it back inside its area");


            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the ‘Start’ button pressed." + Environment.NewLine +
                                "2. No sound is played.");
            MakeTestStepHeader(5, UniqueIdentifier++, "Release ‘Start’ button",
                "Verify the following points,DMI displays Default window.Use the log file to confirm that DMI sends out the packet [MMI_DRIVER_REQUEST (EVC-101)] with variable [MMI_DRIVER_REQUEST (EVC-101).MMI_M_REQUEST] = 9 (Start)");
            /*
            Test Step 5
            Action: Release ‘Start’ button
            Expected Result: Verify the following points,DMI displays Default window.Use the log file to confirm that DMI sends out the packet [MMI_DRIVER_REQUEST (EVC-101)] with variable [MMI_DRIVER_REQUEST (EVC-101).MMI_M_REQUEST] = 9 (Start)
            Test Step Comment: (1) MMI_gen 8349 (partly: MMI_gen 4557 (partly: button ‘Start’, MMI_gen 4381 (partly: exit state ‘Pressed’, execute function associated to the button)));(2) MMI_gen 11908;
            */
            DmiActions.ShowInstruction(this, @"Release the ‘Start’ button");

            EVC101_MMIDriverRequest.CheckMRequestReleased = Variables.MMI_M_REQUEST.Start;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Default window.");

            MakeTestStepHeader(6, UniqueIdentifier++, "Acknowledge ‘SR’ mode.Then, press ‘Main’ button",
                "DMI displays Main window.Verify the following points,Menu windowThe Main window is displayed in main area D/F/G.The window title is ‘Main’.The following objects are display in Main window, Enabled Close button (NA11)Window TitleButton 1 with label ‘Start’Button 2 with label ‘Driver ID’Button 3 with label ‘Train data’ Button 5 with label ‘Level’Button 6 with label ‘Train running number’Button 7 with label ‘Shunting’Button 8 with label ‘Non-Leading’Button 9 with label ‘Maintain shunting’Note: See the position of buttons in picture below,LayersThe level of layers in each area of window as follows,Layer 0: Area D, F, G, E10, E11, Y, and ZLayer -1: Area A1, (A2+A3)*, A4, B*, C1, (C2+C3+C4)*, C5, C6, C7, C8, C9, E1, E2, E3, E4, (E5-E9)*.Layer -2: Area B3, B4, B5, B6 and B7.Note: ‘*’ symbol is mean that specified area are drawn as one area");
            /*
            Test Step 6
            Action: Acknowledge ‘SR’ mode.Then, press ‘Main’ button
            Expected Result: DMI displays Main window.Verify the following points,Menu windowThe Main window is displayed in main area D/F/G.The window title is ‘Main’.The following objects are display in Main window, Enabled Close button (NA11)Window TitleButton 1 with label ‘Start’Button 2 with label ‘Driver ID’Button 3 with label ‘Train data’ Button 5 with label ‘Level’Button 6 with label ‘Train running number’Button 7 with label ‘Shunting’Button 8 with label ‘Non-Leading’Button 9 with label ‘Maintain shunting’Note: See the position of buttons in picture below,LayersThe level of layers in each area of window as follows,Layer 0: Area D, F, G, E10, E11, Y, and ZLayer -1: Area A1, (A2+A3)*, A4, B*, C1, (C2+C3+C4)*, C5, C6, C7, C8, C9, E1, E2, E3, E4, (E5-E9)*.Layer -2: Area B3, B4, B5, B6 and B7.Note: ‘*’ symbol is mean that specified area are drawn as one area
            Test Step Comment: (1) MMI_gen 8349 (partly: MMI_gen 7909);(2) MMI_gen 8350; MMI_gen 4360 (partly: window title);(3) MMI_gen 8349 (partly: MMI_gen 4556 (partly: Close button, Window Title)); MMI_gen 4355 (partly: Buttons, Close button); MMI_gen 8351 (partly: touch screen, button with label, Driver ID, Train data, Level, Train Running Number, Shunting, Non-Leading, Maintain shunting); MMI_gen 4392 (partly: [Close] NA11);(4) MMI_gen 8349 (partly: MMI_gen 4630, MMI gen 5944 (partly: touch screen));
            */
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 263;
            EVC8_MMIDriverMessage.Send();

            DmiActions.ShowInstruction(this, "Acknowledge SR mode, then press the ‘Main’ button");

            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Main;
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.Start |
                                                               EVC30_MMIRequestEnable.EnabledRequests.DriverID |
                                                               EVC30_MMIRequestEnable.EnabledRequests.TrainData |
                                                               EVC30_MMIRequestEnable.EnabledRequests.Level |
                                                               EVC30_MMIRequestEnable.EnabledRequests
                                                                   .TrainRunningNumber |
                                                               EVC30_MMIRequestEnable.EnabledRequests.Shunting |
                                                               EVC30_MMIRequestEnable.EnabledRequests.NonLeading |
                                                               EVC30_MMIRequestEnable.EnabledRequests.MaintainShunting;
            EVC30_MMIRequestEnable.Send();

            WaitForVerification("Check the following (* indicates sub-areas drawn as one area):" + Environment.NewLine +
                                Environment.NewLine +
                                "1. DMI displays the Main window across areas D, F and G." + Environment.NewLine +
                                "2. The window title is ‘Main’." + Environment.NewLine +
                                "3. DMI displays the ‘Close’ button NA11 enabled (symbol NA11)." + Environment.NewLine +
                                "4. DMI displays the ‘Start’ button (#1)." + Environment.NewLine +
                                "5. DMI displays the ‘Driver ID’ button (#2)." + Environment.NewLine +
                                "6. DMI displays the ‘Train data’ button (#3)." + Environment.NewLine +
                                "7. DMI displays the ‘Level’ button (#5)." + Environment.NewLine +
                                "8. DMI displays the ‘Train running number’ button (#6)." + Environment.NewLine +
                                "9. DMI displays the ‘Shunting’ button (#7)." + Environment.NewLine +
                                "10. DMI displays the ‘Non-Leading’ button (#8)." + Environment.NewLine +
                                "11. DMI displays the ‘Maintain shunting’ button (#9)." + Environment.NewLine +
                                "15. The following screen areas are in Layer 0: D, F, G, E10, E11, Z and Y." +
                                Environment.NewLine +
                                "16. The following screen areas are in Layer 1: A1, (A2 + A3)*, A4, C1, (C2 + C3 + C4)*, C5, C6, C7, C8, C9, E1, E2, E3, E4, (E5-E9)." +
                                Environment.NewLine +
                                "17. The following screen areas are in Layer 2: B3, B4, B5, B6, B7");

            MakeTestStepHeader(7, UniqueIdentifier++,
                "Follow action step 2 – step 5. Then, close an opened window respectively for the following button.‘Driver ID’ button.‘Level’ button.‘Train data’ button.‘Train running number’ button",
                "See the expected results of Step 2 – Step 5 and the following additional information,DMI displays corresponding window refer to released button.Use the log file to confirm that DMI sends out the packet EVC-101 with variable according to the actuated buttons,Driver ID buttonMMI_M_REQUEST= 20 (Change Driver Identity)Level buttonMMI_M_REQUEST = 27 (Change Level or Inhibit status)Train data buttonMMI_M_REQUEST = 3 (Start Train data entry)Train running number buttonMMI_M_REQUEST = 30 (Change Train running Number)");
            /*
            Test Step 7
            Action: Follow action step 2 – step 5. Then, close an opened window respectively for the following button.‘Driver ID’ button.‘Level’ button.‘Train data’ button.‘Train running number’ button
            Expected Result: See the expected results of Step 2 – Step 5 and the following additional information,DMI displays corresponding window refer to released button.Use the log file to confirm that DMI sends out the packet EVC-101 with variable according to the actuated buttons,Driver ID buttonMMI_M_REQUEST= 20 (Change Driver Identity)Level buttonMMI_M_REQUEST = 27 (Change Level or Inhibit status)Train data buttonMMI_M_REQUEST = 3 (Start Train data entry)Train running number buttonMMI_M_REQUEST = 30 (Change Train running Number)
            Test Step Comment: (1) MMI_gen 8349 (partly: MMI_gen 4557 (partly: button ‘Driver ID’, ‘Level’, ‘Train data’, ‘Train running number’));(2) MMI_gen 2479; MMI_gen 1205; MMI_gen 2490; MMI_gen 9954; 
            */
            // Driver ID button
            DmiActions.ShowInstruction(this, @"Press and hold the ‘Driver ID’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the ‘Driver ID’ button pressed, without a border." +
                                Environment.NewLine +
                                "2. The ‘Click’ sound is played once.");

            DmiActions.ShowInstruction(this, @"Whilst keeping the ‘Driver ID’ button pressed, drag outside its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the ‘Driver ID’ button enabled, with a border." + Environment.NewLine +
                                "2. No sound is played.");

            DmiActions.ShowInstruction(this,
                @"Whilst keeping the ‘Driver ID’ button pressed, drag it back inside its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the ‘Driver ID’ button pressed." + Environment.NewLine +
                                "2. No sound is played.");

            DmiActions.ShowInstruction(this, @"Release the ‘Driver ID’ button");

            EVC101_MMIDriverRequest.CheckMRequestReleased = Variables.MMI_M_REQUEST.ChangeDriverIdentity;

            EVC14_MMICurrentDriverID.MMI_X_DRIVER_ID = "1234";
            EVC14_MMICurrentDriverID.MMI_Q_CLOSE_ENABLE = Variables.MMI_Q_CLOSE_ENABLE.Enabled;
            EVC14_MMICurrentDriverID.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Driver ID window.");

            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Main window.");

            // Level button
            DmiActions.ShowInstruction(this, @"Press and hold the ‘Level’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the ‘Level’ button pressed, without a border." + Environment.NewLine +
                                "2. The ‘Click’ sound is played once.");

            DmiActions.ShowInstruction(this, @"Whilst keeping the ‘Level’ button pressed, drag outside its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the ‘Level’ button enabled, with a border." + Environment.NewLine +
                                "2. No sound is played.");

            DmiActions.ShowInstruction(this,
                @"Whilst keeping the ‘Level’ button pressed, drag it back inside its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the ‘Level’ button pressed." + Environment.NewLine +
                                "2. No sound is played.");

            DmiActions.ShowInstruction(this, @"Release the ‘Level’ button");

            EVC101_MMIDriverRequest.CheckMRequestReleased = Variables.MMI_M_REQUEST.ChangeLevel;

            EVC20_MMISelectLevel.MMI_Q_CLOSE_ENABLE = Variables.MMI_Q_CLOSE_ENABLE.Enabled;

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

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Level window.");

            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Main window.");

            // Train data button
            DmiActions.ShowInstruction(this, @"Press and hold the ‘Train data’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the ‘Train data’ button pressed, without a border." +
                                Environment.NewLine +
                                "2. The ‘Click’ sound is played once.");

            DmiActions.ShowInstruction(this, @"Whilst keeping the ‘Train data’ button pressed, drag outside its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the ‘Train data’ button enabled, with a border." +
                                Environment.NewLine +
                                "2. No sound is played.");

            DmiActions.ShowInstruction(this,
                @"Whilst keeping the ‘Train data’ button pressed, drag it back inside its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the ‘Train data’ button pressed." + Environment.NewLine +
                                "2. No sound is played.");

            DmiActions.ShowInstruction(this, @"Release the ‘Train data’ button");

            DmiActions.Send_EVC6_MMICurrentTrainData_FixedDataEntry(this, new[] {"FLU"}, 1);
            EVC101_MMIDriverRequest.CheckMRequestReleased = Variables.MMI_M_REQUEST.StartTrainDataEntry;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Train data window.");

            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Main window.");

            // Train running number button
            DmiActions.ShowInstruction(this, @"Press and hold the ‘Train running number’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the ‘Train running number’ button pressed, without a border." +
                                Environment.NewLine +
                                "2. The ‘Click’ sound is played once.");

            DmiActions.ShowInstruction(this,
                @"Whilst keeping the ‘Train running number’ button pressed, drag outside its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the ‘Train running number’ button enabled, with a border." +
                                Environment.NewLine +
                                "2. No sound is played.");

            DmiActions.ShowInstruction(this,
                @"Whilst keeping the ‘Train running number’ button pressed, drag it back inside its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the ‘Train running number’ button pressed." + Environment.NewLine +
                                "2. No sound is played.");

            DmiActions.ShowInstruction(this, @"Release the ‘Train running number’ button");

            EVC16_CurrentTrainNumber.TrainRunningNumber = 1;
            EVC16_CurrentTrainNumber.Send();
            EVC101_MMIDriverRequest.CheckMRequestReleased = Variables.MMI_M_REQUEST.ChangeTrainRunningNumber;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Train running number window.");

            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Main window.");
            MakeTestStepHeader(8, UniqueIdentifier++, "Press ‘Shunting’ button",
                "Verify the following information,The ‘Shunting’ button becomes state ‘Pressed’, then state ‘Enabled’ once the button is immediately released.DMI still displays the Main window.The ‘Click’ sound is played once.Use the log file to confirm that DMI sends EVC-101 twice with different value of MMI_T_BUTTONEVENT and MMI_Q_BUTTON (1 = pressed, 0 = released)");
            /*
            Test Step 8
            Action: Press ‘Shunting’ button
            Expected Result: Verify the following information,The ‘Shunting’ button becomes state ‘Pressed’, then state ‘Enabled’ once the button is immediately released.DMI still displays the Main window.The ‘Click’ sound is played once.Use the log file to confirm that DMI sends EVC-101 twice with different value of MMI_T_BUTTONEVENT and MMI_Q_BUTTON (1 = pressed, 0 = released)
            Test Step Comment: (1) MMI_gen 8354-1 (THR) (partly: button ‘Shunting’, MMI_gen 11450-1 (THR) (partly: Delay-Type button, MMI_gen 4388 (partly: less than the 2 seconds, return to state ‘Enabled’)));(2) MMI_gen 8354-1 (THR) (partly: button ‘Shunting’, MMI_gen 11450-1 (THR) (partly: Delay-Type button, MMI_gen 4388 (partly: less than the 2 seconds, no valid button activation considered by onboard)));(3) MMI_gen 8354-1 (THR) (partly: button ‘Shunting’, MMI_gen 11450-1 (THR) (partly: Delay-Type button, MMI_gen 4388 (partly: the sound for button Delay-Type))); MMI_gen 9512, MMI_gen 968;(4) MMI_gen 8354-1 (THR) (partly: button ‘Shunting’, MMI_gen 11450-1 (THR) (partly: send events of Pressed and Released independently to ETCS), MMI_gen 11907 (partly: EVC-101, timestamp)); MMI_gen 3375;
            */
            DmiActions.ShowInstruction(this, @"Press and immediately release the ‘Shunting’ button");

            // This will not now: DMI only sends the packet when the button has been released
            //EVC101_MMIDriverRequest.CheckMRequestPressed = Variables.MMI_M_REQUEST.StartShunting;

            EVC101_MMIDriverRequest.CheckMRequestReleased = Variables.MMI_M_REQUEST.StartShunting;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI still displays the Main window." + Environment.NewLine +
                                "2. The ‘Shunting’ button is displayed pressed and then re-displayed enabled." +
                                Environment.NewLine +
                                "3. The ‘Click’ sound is played once.");

            MakeTestStepHeader(9, UniqueIdentifier++,
                "Press and hold ‘Shunting’ button for 2s.Note: Stopwatch is required for accuracy of test result",
                "Verify the following information,While press and hold button less than 2 secThe ‘Click’ sound is played once.The state of button is changed to ‘Pressed’.The state ‘pressed’ and ‘enabled’ are switched repeatly while button is pressed. Use the log file to confirm that DMI sends EVC-101 with variable MMI_T_BUTTONEVENT and MMI_Q_BUTTON = 1 (pressed).While press and hold button over 2 secThe state of button is changed to ‘Pressed’ and without toggle");
            /*
            Test Step 9
            Action: Press and hold ‘Shunting’ button for 2s.Note: Stopwatch is required for accuracy of test result
            Expected Result: Verify the following information,While press and hold button less than 2 secThe ‘Click’ sound is played once.The state of button is changed to ‘Pressed’.The state ‘pressed’ and ‘enabled’ are switched repeatly while button is pressed. Use the log file to confirm that DMI sends EVC-101 with variable MMI_T_BUTTONEVENT and MMI_Q_BUTTON = 1 (pressed).While press and hold button over 2 secThe state of button is changed to ‘Pressed’ and without toggle
            Test Step Comment: (1)  MMI_gen 8354-1 (THR) (partly: button ‘Shunting’, MMI_gen 11450-1 (THR) (partly: Delay-Type button, MMI_gen 4388 (partly: the sound for button Delay-Type))); MMI_gen 9512; MMI_gen 968;(2) MMI_gen 8354-1 (THR) (partly: button ‘Shunting’, MMI_gen 11450-1 (THR) (partly: Delay-Type button, MMI_gen 4388 (partly: change to pressed state)));(3) MMI_gen 8354-1 (THR) (partly: button ‘Shunting’, MMI_gen 11450-1 (THR) (partly: Delay-Type button, MMI_gen 4388 (partly: toggle between the “pressed” and “enabled” states as long as the button remains pressed by the driver)));(4) MMI_gen 8354-1 (THR) (partly: button ‘Shunting’, MMI_gen 11450-1 (THR) (partly: send events of Pressed independently to ETCS), MMI_gen 11907 (partly: EVC-101, timestamp))); MMI_gen 3375;(5) MMI_gen 8354-1 (THR) (partly: button ‘Shunting’, MMI_gen 11450-1 (THR) (partly: Delay-Type button, MMI_gen 4388 (partly: after 2 seconds, the button is change again to the state ‘Pressed’)));
            */
            DmiActions.ShowInstruction(this, @"Press and hold the ‘Shunting’ button.");

            // This will not now: DMI only sends the packet when the button has been released
            //EVC101_MMIDriverRequest.CheckMRequestPressed = Variables.MMI_M_REQUEST.StartShunting;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Shunting’ button is displayed pressed and then re-displayed enabled, repeatedly, while the button is held." +
                                Environment.NewLine +
                                "2. The ‘Click’ sound is played once." + Environment.NewLine +
                                "3. After 2s, the ‘Shunting’ button is displayed pressed with no toggling.");

            MakeTestStepHeader(10, UniqueIdentifier++, "Slide out from the “Shunting” button",
                "Verify the following information,The ‘Shunting’ button turns to the ‘Enabled’ state without a sound");
            /*
            Test Step 10
            Action: Slide out from the “Shunting” button
            Expected Result: Verify the following information,The ‘Shunting’ button turns to the ‘Enabled’ state without a sound
            Test Step Comment: (1) MMI_gen 8354-1 (THR) (partly: button ‘Shunting’, MMI_gen 11450-1 (THR) (partly: Delay-Type button, MMI_gen 4389 (partly: state ‘Enabled’ when slide out with force applied, stop toggling state ‘Pressed’ and ‘Enabled’, no sound))); 
            */
            DmiActions.ShowInstruction(this, @"Whilst keeping the ‘Shunting’ button pressed, drag outside its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the ‘Shunting’ button enabled." + Environment.NewLine +
                                "2. No sound is played.");

            MakeTestStepHeader(11, UniqueIdentifier++,
                "Slide back to the “Shunting” button and hold it for 1 seconds. Then, slide out again.Note: Stopwatch is required for accuracy of test result",
                "Verify the following information,The ‘Shunting’ button turns to the ‘Enabled’ state without a sound");
            /*
            Test Step 11
            Action: Slide back to the “Shunting” button and hold it for 1 seconds. Then, slide out again.Note: Stopwatch is required for accuracy of test result
            Expected Result: Verify the following information,The ‘Shunting’ button turns to the ‘Enabled’ state without a sound
            Test Step Comment: (1) MMI_gen 8354-1 (THR) (partly: button ‘Shunting’, MMI_gen 11450-1 (THR) (partly: Delay-Type button, MMI_gen 4388 (partly: to reset toggling state ‘Pressed’ and ‘Enabled’, no sound), MMI_gen 4389 (partly: to reset toggling state ‘Pressed’ and ‘Enabled’, no sound, Slide back)));
            */
            DmiActions.ShowInstruction(this,
                @"Whilst keeping the ‘Shunting’ button pressed, drag it back inside its area and hold it for 1s," +
                Environment.NewLine +
                "then drag outside its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the ‘Train Shunting’ button enabled." + Environment.NewLine +
                                "2. No sound is played.");

            MakeTestStepHeader(12, UniqueIdentifier++,
                "Slide back to the “Shunting” button and hold it for 2 seconds.Note: Stopwatch is required for accuracy of test result",
                "While press and hold button less than 2 secThe state ‘pressed’ and ‘enabled’ are switched repeatly while button is pressed without a sound. While press and hold button over 2 secThe state of button is changed to ‘Pressed’ and without toggle");
            /*
            Test Step 12
            Action: Slide back to the “Shunting” button and hold it for 2 seconds.Note: Stopwatch is required for accuracy of test result
            Expected Result: While press and hold button less than 2 secThe state ‘pressed’ and ‘enabled’ are switched repeatly while button is pressed without a sound. While press and hold button over 2 secThe state of button is changed to ‘Pressed’ and without toggle
            Test Step Comment: (1) MMI_gen 8354-1 (THR) (partly: button ‘Shunting’, MMI_gen 11450-1 (THR) (partly: Delay-Type button, MMI_gen 4389 (partly: start toggling state ‘Pressed’ and ‘Enabled’ when slide back, no sound)));(2) MMI_gen 8354-1 (THR) (partly: button ‘Shunting’, MMI_gen 11450-1 (THR) (partly: Delay-Type button, MMI_gen 4388 (partly: after 2 seconds, the button is change again to the state ‘Pressed’))); MMI_gen 4389 (partly:Main Window, 2 seconds timer);
            */
            DmiActions.ShowInstruction(this,
                @"Whilst keeping the ‘Shunting’ button pressed, drag it back inside its area and hold it for at least 2s");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Shunting’ button is displayed pressed and then re-displayed enabled, repeatedly, while the button is held." +
                                Environment.NewLine +
                                "2. The ‘Click’ sound is played once." + Environment.NewLine +
                                "3. After 2s, the ‘Shunting’ button is displayed pressed with no toggling.");

            MakeTestStepHeader(13, UniqueIdentifier++, "Release ‘Shunting’ button",
                "DMI displays Default window in SH mode, Level 1.Verify the following information,Use the log file to confirm that DMI sends EVC-101 with the following variable,MMI_Q_BUTTON = 0 (Released) MMI_M_REQUEST = 1 (Start Shunting) MMI_T_BUTTONEVENT is not blank.Use the log file to confirm that DMI receives EVC-7 with variable OBU_TR_M_MODE = 3 (SH – Shunting)");
            /*
            Test Step 13
            Action: Release ‘Shunting’ button
            Expected Result: DMI displays Default window in SH mode, Level 1.Verify the following information,Use the log file to confirm that DMI sends EVC-101 with the following variable,MMI_Q_BUTTON = 0 (Released) MMI_M_REQUEST = 1 (Start Shunting) MMI_T_BUTTONEVENT is not blank.Use the log file to confirm that DMI receives EVC-7 with variable OBU_TR_M_MODE = 3 (SH – Shunting)
            Test Step Comment: (1) MMI_gen 8354-1 (THR) (partly: button ‘Shunting’, MMI_gen 11450-1 (THR) (partly: send events of Released to ETCS, MMI_gen 4388 (partly: after 2 seconds button Up-Type is followed, button Delay-Type, MMI_gen 4381 (partly: exit state ‘Pressed’, execute function associated to the button)))); MMI_gen 11907 (partly: EVC-101, timestamp)); MMI_gen 11909; MMI_gen 3375;(2) MMI_gen 1423 (partly: DMI equipped with TS, current mode SH) ;
            */
            DmiActions.ShowInstruction(this, @"Release the ‘Shunting’ button");

            EVC101_MMIDriverRequest.CheckMRequestReleased = Variables.MMI_M_REQUEST.StartShunting;

            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.Shunting;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Default window in SH mode, Level 1.");

            MakeTestStepHeader(14, UniqueIdentifier++, "Press ‘Main’ button",
                "Verify the following information,The ‘Shunting’ button is replaced with the ‘Exit Shunting’ button.Use the log file to confirm that DMI receives EVC-30 with the following value in variable MMI_Q_REQUEST_ENABLE_64MMI_Q_REQUEST_ENABLE_64 (#0) = 0 (Start)MMI_Q_REQUEST_ENABLE_64 (#1) = 1 (Driver ID)MMI_Q_REQUEST_ENABLE_64 (#2) = 0 (Train Data)MMI_Q_REQUEST_ENABL15E_64 (#3) = 0 (Level)MMI_Q_REQUEST_ENABLE_64 (#4) = 0 (Train running number)MMI_Q_REQUEST_ENABLE_64 (#5) = 0 (Shunting)MMI_Q_REQUEST_ENABLE_64 (#6) = 1 (Exit Shunting)MMI_Q_REQUEST_ENABLE_64 (#7) = 0 (Non-Leading)MMI_Q_REQUEST_ENABLE_64 (#8) = 0 (Maintain Shunting)The following buttons are shown with a border and its text is coloured Dark-Grey:The ‘Start’ buttonThe ‘Train data’ buttonThe ‘Level’ buttonThe ‘Train running number’ buttonThe ‘Non-Leading’ buttonThe ‘Maintain Shunting’ button");
            /*
            Test Step 14
            Action: Press ‘Main’ button
            Expected Result: Verify the following information,The ‘Shunting’ button is replaced with the ‘Exit Shunting’ button.Use the log file to confirm that DMI receives EVC-30 with the following value in variable MMI_Q_REQUEST_ENABLE_64MMI_Q_REQUEST_ENABLE_64 (#0) = 0 (Start)MMI_Q_REQUEST_ENABLE_64 (#1) = 1 (Driver ID)MMI_Q_REQUEST_ENABLE_64 (#2) = 0 (Train Data)MMI_Q_REQUEST_ENABL15E_64 (#3) = 0 (Level)MMI_Q_REQUEST_ENABLE_64 (#4) = 0 (Train running number)MMI_Q_REQUEST_ENABLE_64 (#5) = 0 (Shunting)MMI_Q_REQUEST_ENABLE_64 (#6) = 1 (Exit Shunting)MMI_Q_REQUEST_ENABLE_64 (#7) = 0 (Non-Leading)MMI_Q_REQUEST_ENABLE_64 (#8) = 0 (Maintain Shunting)The following buttons are shown with a border and its text is coloured Dark-Grey:The ‘Start’ buttonThe ‘Train data’ buttonThe ‘Level’ buttonThe ‘Train running number’ buttonThe ‘Non-Leading’ buttonThe ‘Maintain Shunting’ button
            Test Step Comment: (1) MMI_gen 1423 (partly: DMI equipped with TS, replace)(2) MMI_gen 591 (partly: enabling condition of ‘Exit Shunting’ button, disabling condition of ‘Start’, ‘Train data’, ‘Level’, ‘Train running number’ and ‘Shunting’ buttons); (3) MMI_gen 591 (partly: enabling condition of ‘Exit Shunting’ button, disabling condition of ‘Start’, ‘Train data’, ‘Level’, ‘Train running number’ and ‘Shunting’ buttons”); MMI_gen 4377 (partly: shown);
            */

            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.DriverID |
                                                               EVC30_MMIRequestEnable.EnabledRequests.ExitShunting;
            EVC30_MMIRequestEnable.Send();

            DmiActions.ShowInstruction(this, @"Press the ‘Main’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI removes the ‘Shunting’ button and replaces it with the ‘Exit Shunting’ button" +
                                Environment.NewLine +
                                "2. DMI displays the ‘Start’, ‘Train data’, ‘Level’, ‘Train running number’, ‘Shunting’ and ‘Maintain shunting’ buttons" +
                                Environment.NewLine +
                                "   with a border and Dark-grey text");

            MakeTestStepHeader(15, UniqueIdentifier++,
                "Simulate the ‘Passive-Shunting’ signal by activating the ‘Passive-Shunting’ checkbox on OTE",
                "Verify the following information,The state of ‘Maintain Shunting’ button is changed to enabled.Use the log file to confirm that DMI receives EVC-30 with the MMI_Q_REQUEST_ENABLE_64 (#8) = 1 (Maintain Shunting)");
            /*
            Test Step 15
            Action: Simulate the ‘Passive-Shunting’ signal by activating the ‘Passive-Shunting’ checkbox on OTE
            Expected Result: Verify the following information,The state of ‘Maintain Shunting’ button is changed to enabled.Use the log file to confirm that DMI receives EVC-30 with the MMI_Q_REQUEST_ENABLE_64 (#8) = 1 (Maintain Shunting)
            Test Step Comment: (1) MMI_gen 8351 (partly: enabling condition of ‘Maintain Shunting’ button);(2) MMI_gen 591 (partly: enabling condition of ‘Maintain Shunting’ button);
            */
            DmiActions.ShowInstruction(this,
                @"Activate the ‘Passive-Shunting’ checkbox on OTE to simulate the ‘Passive-Shunting’ signal");

            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.PassiveShunting;
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Main;
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.DriverID |
                                                               EVC30_MMIRequestEnable.EnabledRequests.ExitShunting |
                                                               EVC30_MMIRequestEnable.EnabledRequests.MaintainShunting;
            EVC30_MMIRequestEnable.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the ‘Maintain shunting’ button enabled.");

            MakeTestStepHeader(16, UniqueIdentifier++,
                "Perform action follow step 8 – step 14 for the ‘Maintain Shunting’ button",
                "See the expected results of Step 8 – Step 14 and the following additional information,DMI displays Default window after button is released from action step 13.Use the log file to confirm that DMI sends EVC-101 with the following variable,MMI_Q_BUTTON = 0 (Released) MMI_M_REQUEST = 14 (Continue shunting on desk closure) MMI_T_BUTTONEVENT is not blank");
            /*
            Test Step 16
            Action: Perform action follow step 8 – step 14 for the ‘Maintain Shunting’ button
            Expected Result: See the expected results of Step 8 – Step 14 and the following additional information,DMI displays Default window after button is released from action step 13.Use the log file to confirm that DMI sends EVC-101 with the following variable,MMI_Q_BUTTON = 0 (Released) MMI_M_REQUEST = 14 (Continue shunting on desk closure) MMI_T_BUTTONEVENT is not blank
            Test Step Comment: (1) MMI_gen 8354-1 (THR) (partly: button’ Maintain Shunting’); MMI_gen 11236 (partly: Additionally);(2) MMI_gen 11907 (partly: EVC-101, Maintain Shunting); MMI_gen 11236 (partly: released, pressed, MMI_Q_BUTTON, EVC-101); MMI_gen 3375;
            */
            // Repeat Step 8
            DmiActions.ShowInstruction(this, @"Press and immediately release the ‘Maintain shunting’ button");

            // This will not now: DMI only sends the packet when the button has been released
            //EVC101_MMIDriverRequest.CheckMRequestPressed = Variables.MMI_M_REQUEST.ContinueShuntingOnDeskClosure;

            EVC101_MMIDriverRequest.CheckMRequestReleased = Variables.MMI_M_REQUEST.ContinueShuntingOnDeskClosure;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI still displays the Main window." + Environment.NewLine +
                                "2. The ‘Maintain shunting’ button is displayed pressed and then re-displayed enabled." +
                                Environment.NewLine +
                                "3. The ‘Click’ sound is played once.");

            // Repeat Step 9
            DmiActions.ShowInstruction(this, @"Press and hold the ‘Maintain shunting’ button.");

            // This will not now: DMI only sends the packet when the button has been released
            //EVC101_MMIDriverRequest.CheckMRequestPressed = Variables.MMI_M_REQUEST.ContinueShuntingOnDeskClosure;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Maintain shunting’ button is displayed pressed and then re-displayed enabled, repeatedly, while the button is held." +
                                Environment.NewLine +
                                "2. The ‘Click’ sound is played once." + Environment.NewLine +
                                "3. After 2s, the ‘Maintain shunting’ button is displayed pressed with no toggling.");

            // Repeat Step 10
            DmiActions.ShowInstruction(this,
                @"Whilst keeping the ‘Maintain shunting’ button pressed, drag outside its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the ‘Maintain shunting’ button enabled." + Environment.NewLine +
                                "2. No sound is played.");

            // Repeat Step 11
            DmiActions.ShowInstruction(this,
                @"Whilst keeping the ‘Maintain shunting’ button pressed, drag it back inside its area and hold it for 1s," +
                Environment.NewLine +
                "then drag outside its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the ‘Maintain shunting’ button enabled." + Environment.NewLine +
                                "2. No sound is played.");

            // Repeat Step 12
            DmiActions.ShowInstruction(this,
                @"Whilst keeping the ‘Maintain shunting’ button pressed, drag it back inside its area and hold it for at least 2s");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Maintain shunting’ button is displayed pressed and then re-displayed enabled, repeatedly, while the button is held." +
                                Environment.NewLine +
                                "2. The ‘Click’ sound is played once." + Environment.NewLine +
                                "3. After 2s, the ‘Maintain shunting’ button is displayed pressed with no toggling.");

            // Repeat Step 13
            DmiActions.ShowInstruction(this, @"Release the ‘Maintain shunting’ button");

            EVC101_MMIDriverRequest.CheckMRequestReleased = Variables.MMI_M_REQUEST.ContinueShuntingOnDeskClosure;

            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.Shunting;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Default window in SH mode, Level 1.");

            // Repeat Step 14
            DmiActions.ShowInstruction(this, @"Press the ‘Main’ button");

            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = (EVC30_MMIRequestEnable.EnabledRequests.DriverID |
                                                                EVC30_MMIRequestEnable.EnabledRequests.ExitShunting) &
                                                               ~(EVC30_MMIRequestEnable.EnabledRequests.Start |
                                                                 EVC30_MMIRequestEnable.EnabledRequests.TrainData |
                                                                 EVC30_MMIRequestEnable.EnabledRequests.Level |
                                                                 EVC30_MMIRequestEnable.EnabledRequests
                                                                     .TrainRunningNumber |
                                                                 EVC30_MMIRequestEnable.EnabledRequests.Shunting |
                                                                 EVC30_MMIRequestEnable.EnabledRequests.NonLeading |
                                                                 EVC30_MMIRequestEnable.EnabledRequests
                                                                     .MaintainShunting);
            EVC30_MMIRequestEnable.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI removes the ‘Shunting’ button and replaces it with the ‘Exit Shunting’ button" +
                                Environment.NewLine +
                                "2. DMI displays the ‘Start’, ‘Train data’, ‘Level’, ‘Train running number’, ‘Shunting’ and ‘Maintain shunting’ buttons" +
                                Environment.NewLine +
                                "   with a border and Dark-grey text");
            MakeTestStepHeader(17, UniqueIdentifier++,
                "Perform action follow step 8 – step 13 for the ‘Exit Shunting’ button",
                "See the expected results of Step 8 – Step 13 and the following additional information,DMI displays Driver ID window in SB mode, Level 1.Use the log file to confirm that DMI sends EVC-101 with the following variable,MMI_Q_BUTTON = 0 (Released) MMI_M_REQUEST = 2 (Exit Shunting)MMI_T_BUTTONEVENT is not blank");
            /*
            Test Step 17
            Action: Perform action follow step 8 – step 13 for the ‘Exit Shunting’ button
            Expected Result: See the expected results of Step 8 – Step 13 and the following additional information,DMI displays Driver ID window in SB mode, Level 1.Use the log file to confirm that DMI sends EVC-101 with the following variable,MMI_Q_BUTTON = 0 (Released) MMI_M_REQUEST = 2 (Exit Shunting)MMI_T_BUTTONEVENT is not blank
            Test Step Comment: (1) MMI_gen 8354-1 (THR) (partly: button’ Exit Shunting’);(2) MMI_gen 11910 (partly: EVC-101, Exit Shunting); MMI_gen 3375;
            */
            // Repeat Step 8
            DmiActions.ShowInstruction(this, @"Press and immediately release the ‘Exit shunting’ button");

            // This will not work: DMI only sends the packet when the button has been released
            //EVC101_MMIDriverRequest.CheckMRequestPressed = Variables.MMI_M_REQUEST.ExitShunting;

            EVC101_MMIDriverRequest.CheckMRequestReleased = Variables.MMI_M_REQUEST.ExitShunting;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI still displays the Main window." + Environment.NewLine +
                                "2. The ‘Exit shunting’ button is displayed pressed and then re-displayed enabled." +
                                Environment.NewLine +
                                "3. The ‘Click’ sound is played once.");

            // Repeat Step 9
            DmiActions.ShowInstruction(this, @"Press and hold the ‘Exit shunting’ button.");

            // This will not work: DMI only sends the packet when the button has been released
            //EVC101_MMIDriverRequest.CheckMRequestPressed = Variables.MMI_M_REQUEST.ExitShunting;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Exit shunting’ button is displayed pressed and then re-displayed enabled, repeatedly, while the button is held." +
                                Environment.NewLine +
                                "2. The ‘Click’ sound is played once." + Environment.NewLine +
                                "3. After 2s, the ‘Exit shunting’ button is displayed pressed with no toggling.");

            // Repeat Step 10
            DmiActions.ShowInstruction(this,
                @"Whilst keeping the ‘Exit shunting’ button pressed, drag outside its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the ‘Exit shunting’ button enabled." + Environment.NewLine +
                                "2. No sound is played.");

            // Repeat Step 11
            DmiActions.ShowInstruction(this,
                @"Whilst keeping the ‘Exit shunting’ button pressed, drag it back inside its area and hold it for 1s," +
                Environment.NewLine +
                "then drag outside its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the ‘Exit shunting’ button enabled." + Environment.NewLine +
                                "2. No sound is played.");

            // Repeat Step 12
            DmiActions.ShowInstruction(this,
                @"Whilst keeping the ‘Exit shunting’ button pressed, drag it back inside its area and hold it for at least 2s");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Exit shunting’ button is displayed pressed and then re-displayed enabled, repeatedly, while the button is held." +
                                Environment.NewLine +
                                "2. The ‘Click’ sound is played once." + Environment.NewLine +
                                "3. After 2s, the ‘Exit shunting’ button is displayed pressed with no toggling.");

            // Repeat Step 13
            DmiActions.ShowInstruction(this, @"Release the ‘Exit shunting’ button");

            EVC101_MMIDriverRequest.CheckMRequestReleased = Variables.MMI_M_REQUEST.ExitShunting;

            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.Shunting;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Default window in SH mode, Level 1.");

            MakeTestStepHeader(18, UniqueIdentifier++,
                "Perform the following procedure,Enter Driver IDSelect and confirm Level 1. Note: If Level window is display",
                "DMI displays Main window.Verify the following information,Use the log file to confirm that DMI receives EVC-30 with the following value in variable MMI_Q_REQUEST_ENABLE_64MMI_Q_REQUEST_ENABLE_64 (#0) = 0 (Start)MMI_Q_REQUEST_ENABLE_64 (#1) = 1 (Driver ID)MMI_Q_REQUEST_ENABLE_64 (#2) = 1 (Train data)MMI_Q_REQUEST_ENABLE_64 (#3) = 1 (Level)MMI_Q_REQUEST_ENABLE_64 (#4) = 1 (Train running number)MMI_Q_REQUEST_ENABLE_64 (#5) = 1 (Shunting)MMI_Q_REQUEST_ENABLE_64 (#6) = 0 (Exit Shunting)MMI_Q_REQUEST_ENABLE_64 (#7) = 0 (Non-Leading)MMI_Q_REQUEST_ENABLE_64 (#8) = 0 (Maintain Shunting)");
            /*
            Test Step 18
            Action: Perform the following procedure,Enter Driver IDSelect and confirm Level 1. Note: If Level window is display
            Expected Result: DMI displays Main window.Verify the following information,Use the log file to confirm that DMI receives EVC-30 with the following value in variable MMI_Q_REQUEST_ENABLE_64MMI_Q_REQUEST_ENABLE_64 (#0) = 0 (Start)MMI_Q_REQUEST_ENABLE_64 (#1) = 1 (Driver ID)MMI_Q_REQUEST_ENABLE_64 (#2) = 1 (Train data)MMI_Q_REQUEST_ENABLE_64 (#3) = 1 (Level)MMI_Q_REQUEST_ENABLE_64 (#4) = 1 (Train running number)MMI_Q_REQUEST_ENABLE_64 (#5) = 1 (Shunting)MMI_Q_REQUEST_ENABLE_64 (#6) = 0 (Exit Shunting)MMI_Q_REQUEST_ENABLE_64 (#7) = 0 (Non-Leading)MMI_Q_REQUEST_ENABLE_64 (#8) = 0 (Maintain Shunting)
            Test Step Comment: (1) MMI_gen 591 (partly: EVC-30, disabling condition of ‘Exit Shunting’ button, enabling condition of ‘Train data’, ‘Level’, ‘Train running number’ and ‘Shunting’ buttons);
            */
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Special; // Level window
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = (EVC30_MMIRequestEnable.EnabledRequests.DriverID |
                                                                EVC30_MMIRequestEnable.EnabledRequests.TrainData |
                                                                EVC30_MMIRequestEnable.EnabledRequests.Level |
                                                                EVC30_MMIRequestEnable.EnabledRequests
                                                                    .TrainRunningNumber |
                                                                EVC30_MMIRequestEnable.EnabledRequests.Shunting) &
                                                               ~(EVC30_MMIRequestEnable.EnabledRequests.Start |
                                                                 EVC30_MMIRequestEnable.EnabledRequests.ExitShunting |
                                                                 EVC30_MMIRequestEnable.EnabledRequests.NonLeading |
                                                                 EVC30_MMIRequestEnable.EnabledRequests
                                                                     .MaintainShunting);

            EVC30_MMIRequestEnable.Send();

            EVC14_MMICurrentDriverID.Send();

            DmiActions.ShowInstruction(this, @"Enter the Driver ID and confirm");

            EVC20_MMISelectLevel.MMI_Q_CLOSE_ENABLE = Variables.MMI_Q_CLOSE_ENABLE.Disabled;
            EVC20_MMISelectLevel.MMI_Q_LEVEL_NTC_ID = new Variables.MMI_Q_LEVEL_NTC_ID[]
                {Variables.MMI_Q_LEVEL_NTC_ID.ETCS_Level};
            EVC20_MMISelectLevel.MMI_M_CURRENT_LEVEL = new Variables.MMI_M_CURRENT_LEVEL[]
                {Variables.MMI_M_CURRENT_LEVEL.LastUsedLevel};
            EVC20_MMISelectLevel.MMI_M_LEVEL_FLAG = new Variables.MMI_M_LEVEL_FLAG[]
                {Variables.MMI_M_LEVEL_FLAG.MarkedLevel};
            EVC20_MMISelectLevel.MMI_M_INHIBITED_LEVEL = new Variables.MMI_M_INHIBITED_LEVEL[]
                {Variables.MMI_M_INHIBITED_LEVEL.NotInhibited};
            EVC20_MMISelectLevel.MMI_M_INHIBIT_ENABLE = new Variables.MMI_M_INHIBIT_ENABLE[]
                {Variables.MMI_M_INHIBIT_ENABLE.AllowedForInhibiting};
            EVC20_MMISelectLevel.MMI_M_LEVEL_NTC_ID = new Variables.MMI_M_LEVEL_NTC_ID[]
                {Variables.MMI_M_LEVEL_NTC_ID.L1};
            EVC20_MMISelectLevel.Send();

            DmiActions.ShowInstruction(this, @"Select and confirm Level 1.");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Main window.");

            MakeTestStepHeader(19, UniqueIdentifier++,
                "Simulates the ‘Non-leading’ signal by activating the ‘Non-leading’ checkbox on OTE",
                "Verify the following information,The state of ‘Non-leading’ button is changed to enabled.Use the log file to confirm that DMI receives EVC-30 with the MMI_Q_REQUEST_ENABLE_64 (#7) = 1 (Non-Leading)");
            /*
            Test Step 19
            Action: Simulates the ‘Non-leading’ signal by activating the ‘Non-leading’ checkbox on OTE
            Expected Result: Verify the following information,The state of ‘Non-leading’ button is changed to enabled.Use the log file to confirm that DMI receives EVC-30 with the MMI_Q_REQUEST_ENABLE_64 (#7) = 1 (Non-Leading)
            Test Step Comment: (1) MMI_gen 8351 (partly: enabling condition of ‘Non-leading’ button);(2) MMI_gen 591 (partly: enabling condition of ‘Non-leading’ button);
            */
            DmiActions.ShowInstruction(this,
                @"Activate the ‘Non-leading’ checkbox on OTE to simulate the ‘Non-leading’ signal");

            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = (EVC30_MMIRequestEnable.EnabledRequests.DriverID |
                                                                EVC30_MMIRequestEnable.EnabledRequests.TrainData |
                                                                EVC30_MMIRequestEnable.EnabledRequests.Level |
                                                                EVC30_MMIRequestEnable.EnabledRequests.NonLeading |
                                                                EVC30_MMIRequestEnable.EnabledRequests
                                                                    .TrainRunningNumber |
                                                                EVC30_MMIRequestEnable.EnabledRequests.Shunting) &
                                                               ~(EVC30_MMIRequestEnable.EnabledRequests.Start |
                                                                 EVC30_MMIRequestEnable.EnabledRequests.ExitShunting |
                                                                 EVC30_MMIRequestEnable.EnabledRequests
                                                                     .MaintainShunting);
            EVC30_MMIRequestEnable.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the ‘Non-leading’ button enabled.");

            MakeTestStepHeader(20, UniqueIdentifier++,
                "Perform action follow step 8 – step 14 for the ‘Non-leading’ button",
                "See the expected results of Step 8 – Step 14 and the following additional information,DMI displays Default window after button is released from action step 13.Use the log file to confirm that DMI sends EVC-101 with the following variable,MMI_Q_BUTTON = 0 (Released) MMI_M_REQUEST = 5 (Start Non-Leading) MMI_T_BUTTONEVENT is not blank");
            /*
            Test Step 20
            Action: Perform action follow step 8 – step 14 for the ‘Non-leading’ button
            Expected Result: See the expected results of Step 8 – Step 14 and the following additional information,DMI displays Default window after button is released from action step 13.Use the log file to confirm that DMI sends EVC-101 with the following variable,MMI_Q_BUTTON = 0 (Released) MMI_M_REQUEST = 5 (Start Non-Leading) MMI_T_BUTTONEVENT is not blank
            Test Step Comment: (1) MMI_gen 8354-1 (THR) (partly: button’ Non Leading);(2) MMI_gen 11911 (partly: EVC-101, Non Leading); MMI_gen 3375;
            */
            // Repeat Step 8
            DmiActions.ShowInstruction(this, @"Press and immediately release the ‘Non-leading’ button");

            // This will not work: DMI only sends the packet when the button has been released
            //EVC101_MMIDriverRequest.CheckMRequestPressed = Variables.MMI_M_REQUEST.StartNonLeading;

            EVC101_MMIDriverRequest.CheckMRequestReleased = Variables.MMI_M_REQUEST.StartNonLeading;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI still displays the Main window." + Environment.NewLine +
                                "2. The ‘Non-leading’ button is displayed pressed and then re-displayed enabled." +
                                Environment.NewLine +
                                "3. The ‘Click’ sound is played once.");

            // Repeat Step 9
            DmiActions.ShowInstruction(this, @"Press and hold the ‘Non-leading’ button.");

            EVC101_MMIDriverRequest.CheckMRequestPressed = Variables.MMI_M_REQUEST.StartNonLeading;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Non-leading’ button is displayed pressed and then re-displayed enabled, repeatedly, while the button is held." +
                                Environment.NewLine +
                                "2. The ‘Click’ sound is played once." + Environment.NewLine +
                                "3. After 2s, the ‘Non-leading’ button is displayed pressed with no toggling.");

            // Repeat Step 10
            DmiActions.ShowInstruction(this, @"Whilst keeping the ‘Non-leading’ button pressed, drag outside its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the ‘Non-leading’ button enabled." + Environment.NewLine +
                                "2. No sound is played.");

            // Repeat Step 11
            DmiActions.ShowInstruction(this,
                @"Whilst keeping the ‘Non-leading’ button pressed, drag it back inside its area and hold it for 1s," +
                Environment.NewLine +
                "then drag outside its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the ‘Train Non-leading’ button enabled." + Environment.NewLine +
                                "2. No sound is played.");

            // Repeat Step 12
            DmiActions.ShowInstruction(this,
                @"Whilst keeping the ‘Non-leading’ button pressed, drag it back inside its area and hold it for at least 2s");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Non-leading’ button is displayed pressed and then re-displayed enabled, repeatedly, while the button is held." +
                                Environment.NewLine +
                                "2. The ‘Click’ sound is played once." + Environment.NewLine +
                                "3. After 2s, the ‘Non-leading’ button is displayed pressed with no toggling.");

            // Repeat Step 13
            DmiActions.ShowInstruction(this, @"Release the ‘Non-leading’ button");

            EVC101_MMIDriverRequest.CheckMRequestReleased = Variables.MMI_M_REQUEST.StartNonLeading;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.NonLeading;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Default window in NL mode, Level 1.");

            // Spec says steps 8-14, but 14 is superfluous 
            /*
             * 
            // Repeat Step 14

            DmiActions.ShowInstruction(this, @"Press the ‘Main’ button");

            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = (EVC30_MMIRequestEnable.EnabledRequests.DriverID |
                                                                EVC30_MMIRequestEnable.EnabledRequests.ExitShunting) &
                                                               ~(EVC30_MMIRequestEnable.EnabledRequests.Start |
                                                                 EVC30_MMIRequestEnable.EnabledRequests.TrainData |
                                                                 EVC30_MMIRequestEnable.EnabledRequests.Level |
                                                                 EVC30_MMIRequestEnable.EnabledRequests.TrainRunningNumber |
                                                                 EVC30_MMIRequestEnable.EnabledRequests.Shunting |
                                                                 EVC30_MMIRequestEnable.EnabledRequests.NonLeading |
                                                                 EVC30_MMIRequestEnable.EnabledRequests.MaintainShunting);
            EVC30_MMIRequestEnable.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI removes the ‘Shunting’ button and replaces it with the ‘Exit Shunting’ button" + Environment.NewLine +
                                "2. DMI displays the ‘Start’, ‘Train data’, ‘Level’, ‘Train running number’, ‘Shunting’ and ‘Non-leading’ buttons" + Environment.NewLine +
                                "   with a border and Dark-grey text");
            */

            MakeTestStepHeader(21, UniqueIdentifier++,
                "Remove the ‘Non-leading’ signal by de-activating the ‘Non-leading’ checkbox on OTE.Then, perform the following procedure,Enter Driver IDSelect and confirm Level 1. Note: If Level window is display",
                "DMI displays Main window.Verify the following information,Use the log file to confirm that DMI receives EVC-30 with the MMI_Q_REQUEST_ENABLE_64 (#7) = 0 (Non-Leading)The state of ‘Non-leading’ button is disabled");
            /*
            Test Step 21
            Action: Remove the ‘Non-leading’ signal by de-activating the ‘Non-leading’ checkbox on OTE.Then, perform the following procedure,Enter Driver IDSelect and confirm Level 1. Note: If Level window is display
            Expected Result: DMI displays Main window.Verify the following information,Use the log file to confirm that DMI receives EVC-30 with the MMI_Q_REQUEST_ENABLE_64 (#7) = 0 (Non-Leading)The state of ‘Non-leading’ button is disabled
            Test Step Comment: (1) MMI_gen 591 (partly: disabling condition of ‘Non-leading’ button);(2) MMI_gen 591 (partly: disabling condition of ‘Non-leading’ button);
            */
            DmiActions.ShowInstruction(this,
                @"De-activate the ‘Non-leading’ checkbox on OTE to remove the ‘Non-leading’ signal");

            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Level;
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = (EVC30_MMIRequestEnable.EnabledRequests.DriverID |
                                                                EVC30_MMIRequestEnable.EnabledRequests.Level) &
                                                               ~(EVC30_MMIRequestEnable.EnabledRequests.Start |
                                                                 EVC30_MMIRequestEnable.EnabledRequests.TrainData |
                                                                 EVC30_MMIRequestEnable.EnabledRequests
                                                                     .TrainRunningNumber |
                                                                 EVC30_MMIRequestEnable.EnabledRequests.Shunting |
                                                                 EVC30_MMIRequestEnable.EnabledRequests.ExitShunting |
                                                                 EVC30_MMIRequestEnable.EnabledRequests.NonLeading |
                                                                 EVC30_MMIRequestEnable.EnabledRequests
                                                                     .MaintainShunting);
            EVC30_MMIRequestEnable.Send();

            EVC14_MMICurrentDriverID.Send();

            DmiActions.ShowInstruction(this, "Enter and confirm Driver ID");

            EVC20_MMISelectLevel.MMI_Q_CLOSE_ENABLE = Variables.MMI_Q_CLOSE_ENABLE.Disabled;
            EVC20_MMISelectLevel.MMI_Q_LEVEL_NTC_ID = new Variables.MMI_Q_LEVEL_NTC_ID[]
                {Variables.MMI_Q_LEVEL_NTC_ID.ETCS_Level};
            EVC20_MMISelectLevel.MMI_M_CURRENT_LEVEL = new Variables.MMI_M_CURRENT_LEVEL[]
                {Variables.MMI_M_CURRENT_LEVEL.LastUsedLevel};
            EVC20_MMISelectLevel.MMI_M_LEVEL_FLAG = new Variables.MMI_M_LEVEL_FLAG[]
                {Variables.MMI_M_LEVEL_FLAG.MarkedLevel};
            EVC20_MMISelectLevel.MMI_M_INHIBITED_LEVEL = new Variables.MMI_M_INHIBITED_LEVEL[]
                {Variables.MMI_M_INHIBITED_LEVEL.NotInhibited};
            EVC20_MMISelectLevel.MMI_M_INHIBIT_ENABLE = new Variables.MMI_M_INHIBIT_ENABLE[]
                {Variables.MMI_M_INHIBIT_ENABLE.AllowedForInhibiting};
            EVC20_MMISelectLevel.MMI_M_LEVEL_NTC_ID = new Variables.MMI_M_LEVEL_NTC_ID[]
                {Variables.MMI_M_LEVEL_NTC_ID.L1};
            EVC20_MMISelectLevel.Send();

            DmiActions.ShowInstruction(this, "Select and confirm Level 1.");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Main window" +
                                "2. The ‘Non-leading’ button is disabled.");
            MakeTestStepHeader(22, UniqueIdentifier++,
                "Use the test script file 7_1_a.xml to send EVC-30 with,MMI_Q_REQUEST_ENABLE_64 (#1) = 0MMI_NID_WINDOW = 1",
                "Verify that the ‘Drive ID’ button is disabled");
            /*
            Test Step 22
            Action: Use the test script file 7_1_a.xml to send EVC-30 with,MMI_Q_REQUEST_ENABLE_64 (#1) = 0MMI_NID_WINDOW = 1
            Expected Result: Verify that the ‘Drive ID’ button is disabled
            Test Step Comment: MMI_gen 591 (partly: disabling condition of ‘Driver ID’ button);
            */
            XML_7_1(msgType.typea);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Driver ID’ button is disabled.");

            MakeTestStepHeader(23, UniqueIdentifier++,
                "Use the test script file 7_1_b.xml to send EVC-30 with,MMI_Q_REQUEST_ENABLE_64 (#1) = 1MMI_NID_WINDOW = 1",
                "Verify that the ‘Drive ID’ button is enabled");
            /*
            Test Step 23
            Action: Use the test script file 7_1_b.xml to send EVC-30 with,MMI_Q_REQUEST_ENABLE_64 (#1) = 1MMI_NID_WINDOW = 1
            Expected Result: Verify that the ‘Drive ID’ button is enabled
            Test Step Comment: MMI_gen 591 (partly: enabling condition of ‘Driver ID’ button);
            */
            XML_7_1(msgType.typeb);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Driver ID’ button is enabled.");

            MakeTestStepHeader(24, UniqueIdentifier++, "Follow action step 2 – step 5 for the ‘Close’ button",
                "See the expected results of Step 2 – Step 5 and the following additional information,  (1) DMI displays Default window refer to released button");
            /*
            Test Step 24
            Action: Follow action step 2 – step 5 for the ‘Close’ button
            Expected Result: See the expected results of Step 2 – Step 5 and the following additional information,  (1) DMI displays Default window refer to released button
            Test Step Comment: (1) MMI_gen 8349 (partly: MMI_gen 4557 (partly: button ‘Close’)); MMI_gen 4392 (partly: returning to the parent window);
            */
            // Repeat Step 2
            DmiActions.ShowInstruction(this, @"Press and hold the ‘Close’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the ‘Close’ button pressed, without a border." + Environment.NewLine +
                                "2. The ‘Click’ sound is played once.");

            // Repeat Step 3
            DmiActions.ShowInstruction(this, @"Whilst keeping the ‘Close’ button pressed, drag outside its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the ‘Close’ button enabled, with a border." + Environment.NewLine +
                                "2. No sound is played.");

            // Repeat Step 4
            DmiActions.ShowInstruction(this,
                @"Whilst keeping the ‘Close’ button pressed, drag it back inside its area");


            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the ‘Close’ button pressed." + Environment.NewLine +
                                "2. No sound is played.");

            // Repeat Step 5
            DmiActions.ShowInstruction(this, @"Release the ‘Close’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Default window.");

            MakeTestStepHeader(25, UniqueIdentifier++, "End of test", "");

            /*
            Test Step 25
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }

        #region Send_XML_7_1_DMI_Test_Specification

        enum msgType
        {
            typea,
            typeb
        }

        private void XML_7_1(msgType type)
        {
            EVC30_MMIRequestEnable.SendBlank();
            switch (type)
            {
                case msgType.typea:
                    EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH =
                        (EVC30_MMIRequestEnable.EnabledRequests.Language |
                         EVC30_MMIRequestEnable.EnabledRequests.Volume |
                         EVC30_MMIRequestEnable.EnabledRequests.Brightness |
                         EVC30_MMIRequestEnable.EnabledRequests.SystemVersion) &
                        ~(EVC30_MMIRequestEnable.EnabledRequests.Start |
                          EVC30_MMIRequestEnable.EnabledRequests.DriverID |
                          EVC30_MMIRequestEnable.EnabledRequests.TrainData |
                          EVC30_MMIRequestEnable.EnabledRequests.Level |
                          EVC30_MMIRequestEnable.EnabledRequests.TrainRunningNumber |
                          EVC30_MMIRequestEnable.EnabledRequests.Shunting |
                          EVC30_MMIRequestEnable.EnabledRequests.ExitShunting |
                          EVC30_MMIRequestEnable.EnabledRequests.NonLeading |
                          EVC30_MMIRequestEnable.EnabledRequests.MaintainShunting |
                          EVC30_MMIRequestEnable.EnabledRequests.EOA |
                          EVC30_MMIRequestEnable.EnabledRequests.Adhesion |
                          EVC30_MMIRequestEnable.EnabledRequests.SRSpeedDistance |
                          EVC30_MMIRequestEnable.EnabledRequests.TrainIntegrity |
                          EVC30_MMIRequestEnable.EnabledRequests.SetVBC |
                          EVC30_MMIRequestEnable.EnabledRequests.RemoveVBC |
                          EVC30_MMIRequestEnable.EnabledRequests.ContactLastRBC |
                          EVC30_MMIRequestEnable.EnabledRequests.UseShortNumber |
                          EVC30_MMIRequestEnable.EnabledRequests.EnterRBCData |
                          EVC30_MMIRequestEnable.EnabledRequests.RadioNetworkID |
                          EVC30_MMIRequestEnable.EnabledRequests.GeographicalPosition |
                          EVC30_MMIRequestEnable.EnabledRequests.EndOfDataEntryNTC |
                          EVC30_MMIRequestEnable.EnabledRequests.SetLocalTimeDateAndOffset |
                          EVC30_MMIRequestEnable.EnabledRequests.SetLocalOffset |
                          EVC30_MMIRequestEnable.EnabledRequests.Reserved |
                          EVC30_MMIRequestEnable.EnabledRequests.StartBrakeTest |
                          EVC30_MMIRequestEnable.EnabledRequests.EnableWheelDiameter |
                          EVC30_MMIRequestEnable.EnabledRequests.EnableDoppler |
                          EVC30_MMIRequestEnable.EnabledRequests.EnableBrakePercentage);
                    break;
                case msgType.typeb:
                    EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH =
                        (EVC30_MMIRequestEnable.EnabledRequests.DriverID |
                         EVC30_MMIRequestEnable.EnabledRequests.Language |
                         EVC30_MMIRequestEnable.EnabledRequests.Volume |
                         EVC30_MMIRequestEnable.EnabledRequests.Brightness |
                         EVC30_MMIRequestEnable.EnabledRequests.SystemVersion) &
                        ~(EVC30_MMIRequestEnable.EnabledRequests.Start |
                          EVC30_MMIRequestEnable.EnabledRequests.TrainData |
                          EVC30_MMIRequestEnable.EnabledRequests.Level |
                          EVC30_MMIRequestEnable.EnabledRequests.TrainRunningNumber |
                          EVC30_MMIRequestEnable.EnabledRequests.Shunting |
                          EVC30_MMIRequestEnable.EnabledRequests.ExitShunting |
                          EVC30_MMIRequestEnable.EnabledRequests.NonLeading |
                          EVC30_MMIRequestEnable.EnabledRequests.MaintainShunting |
                          EVC30_MMIRequestEnable.EnabledRequests.EOA |
                          EVC30_MMIRequestEnable.EnabledRequests.Adhesion |
                          EVC30_MMIRequestEnable.EnabledRequests.SRSpeedDistance |
                          EVC30_MMIRequestEnable.EnabledRequests.TrainIntegrity |
                          EVC30_MMIRequestEnable.EnabledRequests.SetVBC |
                          EVC30_MMIRequestEnable.EnabledRequests.RemoveVBC |
                          EVC30_MMIRequestEnable.EnabledRequests.ContactLastRBC |
                          EVC30_MMIRequestEnable.EnabledRequests.UseShortNumber |
                          EVC30_MMIRequestEnable.EnabledRequests.EnterRBCData |
                          EVC30_MMIRequestEnable.EnabledRequests.RadioNetworkID |
                          EVC30_MMIRequestEnable.EnabledRequests.GeographicalPosition |
                          EVC30_MMIRequestEnable.EnabledRequests.EndOfDataEntryNTC |
                          EVC30_MMIRequestEnable.EnabledRequests.SetLocalTimeDateAndOffset |
                          EVC30_MMIRequestEnable.EnabledRequests.SetLocalOffset |
                          EVC30_MMIRequestEnable.EnabledRequests.Reserved |
                          EVC30_MMIRequestEnable.EnabledRequests.StartBrakeTest |
                          EVC30_MMIRequestEnable.EnabledRequests.EnableWheelDiameter |
                          EVC30_MMIRequestEnable.EnabledRequests.EnableDoppler |
                          EVC30_MMIRequestEnable.EnabledRequests.EnableBrakePercentage);
                    break;
            }

            EVC30_MMIRequestEnable.Send();
        }

        #endregion
    }
}
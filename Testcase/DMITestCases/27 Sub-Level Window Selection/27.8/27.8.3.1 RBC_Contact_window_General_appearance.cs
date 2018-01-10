using System;
using System.Collections.Generic;
using Testcase.Telegrams.DMItoEVC;
using Testcase.Telegrams.EVCtoDMI;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 27.8.3.1 RBC Contact window: General appearance
    /// TC-ID: 22.8.3.1
    /// 
    /// This test case verifies the display of the menu buttons in ‘RBC Contact’ window for touch screen technology that shall comply with [ERA-ERTMS] standard and [MMI-ETCS-gen].
    /// 
    /// Tested Requirements:
    /// MMI_gen 9446; MMI_gen 11241; MMI_gen 8518 (partly: touch screen); MMI_gen 8518; MMI_gen 11928; MMI_gen 9450; Note under MMI_gen 9450; MMI_gen 11239; MMI_gen 9507; MMI_gen 8516 (partly: MMI_gen 7909 (partly: area D and F), MMI_gen 4556 (partly: Close button, Window Title), MMI_gen 4630; MMI_gen 4557 (partly: MMI_gen 4381, MMI_gen 4382); MMI_gen 8520; MMI_gen 9512; MMI_gen 968; MMI_gen 8521 (partly: hour glass symbol, vertically centered, move to the right every second, no more display); MMI_gen 4360 (partly: window title); MMI_gen 4374; MMI_gen 4375; MMI_gen 4392 (partly: [Close] NA11, returning to the parent window); MMI_gen 8517; MMI_gen 1088 (partly, Bit #19 to #22), MMI_gen 4389 (partly: RBC contact window); MMI_gen 4350; MMI_gen 4351; MMI_gen 4353; MMI_gen 4396 (partly: Close, NA12); MMI_gen 4395 (partly: Close);
    /// 
    /// Scenario:
    /// The concerned buttons in the ‘RBC Contact’ window are verified by the following actions:Press the button once (for the Delay-Type button)Press the button and holdSlide the button out with force appliedSlide the button back with force appliedRelease the buttonThe test system is powered on and the cabin is inactive.RBC Contact window in cabin inactive state is verified.After SoM is perform until Level 2 is selected and confirmed, the RBC Contact window appearance is verified.The ‘Enter RBC data’, ‘Radio Network ID’ buttons are verified by the action specified above.After RBC ID and RBC Phone number are entered, ‘Contact last RBC’ and ‘Close’ button are verified by the action specified above.After used the test script file, the state of RBC Contact window is verified refer to received packet EVC-22.Note: It’s necessary to re-start OTE and RBC simulator and perform SoM until level 2 is selected and confirmed for before execute step 4-6.
    /// 
    /// Used files:
    /// 22_8_3_1.utt, 22_8_3_1_a.xml, 22_8_3_1_b.xml
    /// </summary>
    public class TC_ID_22_8_3_1_RBC_Contact_window : TestcaseBase
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
            // DMI displays in SB mode, Level 2

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint

            /*
            Test Step 1
            Action: Use the test script file 22_8_3_1_a.xml to send EVC-22 with,MMI_NID_WINDOW = 5
            Expected Result: Verify the following information,DMI does not display RBC Contact window
            Test Step Comment: (1) MMI_gen 9446 (partly: NEGATIVE, inactive);
            */
            // Make sure that cabin is not active
            DmiActions.Start_ATP();
            XML_22_8_3_1(msgType.typea);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI does not display the RBC contact ID window");

            /*
            Test Step 2
            Action: Perform the following procedure,Activate Cabin AEnter Driver ID and perform brake testSelect and confirm Level 2
            Expected Result: Verify the following information,Hour glass symbolThe hour glass symbol ST05 is displayed vertically centered in window title area and move to the right every second and come back to its first position and continue moving when there is no more possible to display inside window title area.Note: stopwatch is required.Menu windowThe RBC Contact window is displayed in main area D/F/G.The window title is ‘RBC contact’.The following objects are displayed in RBC Contact window,  Disabled Close button (NA12)Window TitleButton 1 with label ‘Contact last RBC’Button 2 with label ‘Use short number’Button 3 with label ‘Enter RBC data’Button 4 with label ‘Radio Network ID’Note: See the position of buttons in picture below,The state of each button in RBC Contact window are displayed correctly as follows,Contact last RBC = DisableUse short number = DisableEnter RBC data = EnableRadio Network ID = EnableLayersThe level of layers in each area of window as follows,Layer 0: Area D, F, G, E10, E11, Y, and ZLayer -1: Area A1, (A2+A3)*, A4, B*, C1, (C2+C3+C4)*, C5, C6, C7, C8, C9, E1, E2, E3, E4, (E5-E9)*.Layer -2: Area B3, B4, B5, B6 and B7.Note: ‘*’ symbol is mean that specified area are drawn as one area.Packet receivingUse the log file to confirm that DMI received pacekt EVC-22 with variable MMI_NID_WINDOW = 5.DMI displays RBC Contact window.Use the log file to confirm that DMI received packet EVC-30 with the value in each bit of variable MMI_Q_REQUEST_ENABLE_64 as follows,Bit #19 = 0 (Disable Contact last RBC)Bit #20 = 0 (Disable Use short number)Bit #21  = 1 (Enable Start RBC Data Entry)Bit #22 = 1 (Enable Radio Network ID)General property of windowThe RBC Contact window is presented with objects, text messages and buttons which is the one of several levels and allocated to areas of DMI. All objects, text messages and buttons are presented within the same layer.The Default window is not displayed and covered the current window
            Test Step Comment: (1) MMI_gen 8521 (partly: hour glass symbol, vertically centered, move to the right every second, no more display);(2) MMI_gen 8516 (partly: MMI_gen 7909);(3) MMI_gen 8517; MMI_gen 4360 (partly: window title);(4) MMI_gen 8516 (partly: MMI_gen 4556 (partly: Close button, Window Title)); MMI_gen 4355 (partly: Buttons, Close button); MMI_gen 8518 (partly: touch screen, button with label, Contact last RBC, Use short number, Enter RBC data, Radio Network ID); MMI_gen 4396 (partly: Close, NA12); MMI_gen 4395 (partly: Close);(5) MMI_gen 9507 (partly: EVC-30, enabling #21 and #22, disabling #19 and #20); Note under MMI_gen 9450;(6) MMI_gen 8516 (partly: MMI_gen 4630, MMI gen 5944 (partly: touch screen));(7) MMI_gen 9446 (partly: EVC-22);(8) MMI_gen 9446 (partly: Open RBC Contact window); (9) MMI_gen 9507 (partly: Disabled Contact last RBC, Enabled Enter RBC data, Enabled Radio Network ID); Note under MMI_gen 9450; MMI_gen 1088 (partly, Bit #19 to #22);(10) MMI_gen 4350;(11) MMI_gen 4351;(12) MMI_gen 4353;
            */
            // No point test all this...
            DmiActions.Complete_SoM_L1_SB(this);
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Level = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_LEVEL.L2;

            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Main;
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.EnterRBCData |
                                                               EVC30_MMIRequestEnable.EnabledRequests.RadioNetworkID;
            EVC30_MMIRequestEnable.Send();
            EVC22_MMICurrentRBC.MMI_NID_WINDOW = 5;
            EVC22_MMICurrentRBC.MMI_Q_CLOSE_ENABLE = Variables.MMI_Q_CLOSE_ENABLE.Enabled;
            EVC22_MMICurrentRBC.Send();

            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 716;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following (* indicates sub-areas drawn as one area):" + Environment.NewLine +
                                Environment.NewLine +
                                "1. DMI displays the RBC Contact window in areas D, F and G, with 3 layers, with the title ‘RBC Contact’." +
                                Environment.NewLine +
                                "2. The hour-glass, symbol ST05, is displayed vertically-centred in the window title, moving to the right each second and " +
                                Environment.NewLine +
                                "   returning to its original position after reaching the end of the window title area then continuing to move to the right." +
                                Environment.NewLine +
                                "3. Layer 0 comprises areas D, F, G, Y and Z." + Environment.NewLine +
                                "4. Layer 1 comprises areas (A1 + A2 + A3)*, A4, B*, C1, (C2 + C3 + C4)*, C5, C6, C7, C8, C9, E1, E2, E3, E4, (E5 - E9)*." +
                                Environment.NewLine +
                                "5. Layer 2 comprises areas B3, B4, B5, B6, B7." + Environment.NewLine +
                                "6. The window displays a disabled ‘Close’ button (symbol NA12) and 4 buttons on a keypad in a 2 x 2 grid." +
                                Environment.NewLine +
                                @"7. Button #1 (disabled) has the label ‘Contact last RBC’, Button #2 (disabled) has the label ‘Use short number,’" +
                                Environment.NewLine +
                                @"   Button #3 (enabled) has the label ‘Enter RBC data’, Button #4 has the label ‘Radio Network ID’" +
                                Environment.NewLine +
                                "8. All objects, text messages and buttons are displayed in the same layer." +
                                Environment.NewLine +
                                "9. The Default window is not covering the current window.");

            /*
            Test Step 3
            Action: Press and hold ‘Enter RBC data’ button
            Expected Result: Verify the following information,The sound ‘Click’ is played once.The ‘Enter RBC data’ button is shown as pressed state, the border of button is removed
            Test Step Comment: (1) MMI_gen 8516 (partly: MMI_gen 4557 (partly: button ‘Enter RBC data’), MMI_gen 4381 (partly: the sound for Up-Type button)); MMI_gen 9512; MMI_gen 968; (2) MMI_gen 8516 (partly: MMI_gen 4557 (partly: button ‘Enter RBC data’), MMI_gen 4381 (partly: change to state ‘Pressed’ as long as remain actuated)); MMI_gen 4375;
            */
            // Remove hourglass
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 4;
            EVC8_MMIDriverMessage.Send();

            DmiActions.ShowInstruction(this, "Press and hold the ‘Enter RBC data’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘Enter RBC data’ button is displayed pressed, without a border." +
                                Environment.NewLine +
                                "2. The ‘Click’ sound is played once.");

            /*
            Test Step 4
            Action: Slide out of ‘Enter RBC data’ button
            Expected Result: The border of the button is shown (state ‘Enabled’) without a sound
            Test Step Comment: MMI_gen 8516 (partly: MMI_gen 4557 (partly: button ‘Enter RBC data’, MMI_gen 4382 (partly: state ‘Enabled’ when slide out with force applied, no sound))); MMI_gen 4374;
            */
            DmiActions.ShowInstruction(this,
                @"Whilst keeping the ‘Enter RBC data’ button pressed, drag it out of its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘Enter RBC data’ button field is displayed enabled." + Environment.NewLine +
                                "2. No sound is played.");

            /*
            Test Step 5
            Action: Slide back into ‘Enter RBC data’ button
            Expected Result: The button is back to state ‘Pressed’ without a sound
            Test Step Comment: MMI_gen 8516 (partly: MMI_gen 4557 (partly: button ‘Enter RBC data’, MMI_gen 4382 (partly: state ‘Pressed’ when slide back, no sound))); MMI_gen 4375;
            */
            DmiActions.ShowInstruction(this,
                @"Whilst keeping the ‘Enter RBC data’ button pressed, drag it back inside its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘Enter RBC data’ button is displayed pressed." + Environment.NewLine +
                                "2. No sound is played.");

            /*
            Test Step 6
            Action: Release ‘Enter RBC data’ button
            Expected Result: Verify the following points,DMI displays RBC Data window.Use the log file to confirm that DMI sends out the packet [MMI_NEW_RBC_DATA (EVC-112)] with variable [MMI_NEW_RBC_DATA (EVC-112). MMI_M_BUTTONS] = 23 (BTN_ENTER_RBC_DATA) and [MMI_NEW_RBC_DATA (EVC-112). MMI_N_DATA_ELEMENTS] = 0
            Test Step Comment: (1) MMI_gen 8516 (partly: MMI_gen 4557 (partly: button ‘Enter RBC Data’, MMI_gen 4381 (partly: exit state ‘Pressed’, execute function associated to the button)));(2) MMI_gen 9450 (partly: Enter RBC data);
            */
            DmiActions.ShowInstruction(this, @"Release the ‘Enter RBC data’ button");

            // Spec says button type = 23 
            EVC112_MMINewRbcData.MMI_NID_DATA = new List<byte>();
            EVC112_MMINewRbcData.MMI_M_BUTTONS = Variables.MMI_M_BUTTONS_RBC_DATA.BTN_ENTER;
            EVC112_MMINewRbcData.CheckPacketContent();

            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.No_window_specified;
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = (EVC30_MMIRequestEnable.EnabledRequests.EnterRBCData |
                                                                EVC30_MMIRequestEnable.EnabledRequests.RadioNetworkID) &
                                                               ~(EVC30_MMIRequestEnable.EnabledRequests.ContactLastRBC |
                                                                 EVC30_MMIRequestEnable.EnabledRequests.UseShortNumber);
            EVC30_MMIRequestEnable.Send();
            EVC22_MMICurrentRBC.MMI_NID_WINDOW = 10;
            EVC22_MMICurrentRBC.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the RBC data window");

            /*
            Test Step 7
            Action: Press ‘Close’ button
            Expected Result: Verify the following information,(1)   DMI displays RBC Contact window
            Test Step Comment: (1) MMI_gen 4392 (partly: returning to the parent window);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the RBC contact window");

            /*
            Test Step 8
            Action: Press ‘Radio Network ID ‘ button
            Expected Result: Verify the following information,The ‘Radio Network ID’ button becomes state ‘Pressed’, then state ‘Enabled’ once the button is immediately released.DMI still displays the RBC Contact window.The ‘Click’ sound is played once
            Test Step Comment: (1) MMI_gen 8520 (partly: button ‘Radio Network ID’, MMI_gen 4388 (partly: less than the 2 seconds, return to state ‘Enabled’));(2) MMI_gen 8520  (partly: button ‘Radio Network ID’, MMI_gen 4388 (partly: less than the 2 seconds, no valid button activation considered by onboard));(3) MMI_gen 8520  (partly: button ‘Radio Network ID’, MMI_gen 4388 (partly: the sound for button Delay-Type)); MMI_gen 9512, MMI_gen 968;
            */
            DmiActions.ShowInstruction(this, "Press and immediately release the ‘Radio Network ID’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘Radio Network ID’ button is displayed pressed, then enabled when released." +
                                Environment.NewLine +
                                @"2. The ‘Click’ sound is played once." + Environment.NewLine +
                                "3. DMI still displays the RBC contact window");

            /*
            Test Step 9
            Action: Press and hold ‘Radio Network ID’ button for 2s.Note: Stopwatch is required for accuracy of test result
            Expected Result: Verify the following information,While press and hold button less than 2 secThe ‘Click’ sound is played once.The state of button is changed to ‘Pressed’.The state ‘pressed’ and ‘enabled’ are switched repeatly while button is pressed. While press and hold button over 2 secThe state of button is changed to ‘Pressed’ and without toggle
            Test Step Comment: (1)  MMI_gen 8520 (partly: button ‘Radio Network ID’, MMI_gen 4388 (partly: the sound for button Delay-Type)); MMI_gen 9512; MMI_gen 968;(2) MMI_gen 8520 (partly: button ‘Radio Network ID’, MMI_gen 4388 (partly: change to pressed state));(3) MMI_gen 8520 (partly: button ‘Radio Network ID’, MMI_gen 4388 (partly: toggle every between the “pressed” and “enabled” states as long as the button remains pressed by the driver));(4) MMI_gen 8520 (partly: button ‘Radio Network ID’, MMI_gen 11450-1 (THR) (partly: Delay-Type button, MMI_gen 4388 (partly: after 2 seconds, the button is change again to the state ‘Pressed’));
            */
            DmiActions.ShowInstruction(this, "Press and hold the ‘Radio Network ID’ button for more than 2s");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. While the button has been pressed for less than 2s it repeatedly changes between being displayed pressed and enabled;" +
                                @"2. The ‘Click’ sound is played once." + Environment.NewLine +
                                "3. When the button has been pressed for more than 2s it is displayed pressed and does not change further.");

            /*
            Test Step 10
            Action: Slide out from the “Radio Network ID” button
            Expected Result: Verify the following information,The ‘‘Radio Network ID’ button turns to the ‘Enabled’ state without a sound
            Test Step Comment: (1) MMI_gen 8520 (partly: button ‘Radio Network ID’, MMI_gen 4389 (partly: state ‘Enabled’ when slide out with force applied, stop toggling state ‘Pressed’ and ‘Enabled’, no sound)));
            */
            DmiActions.ShowInstruction(this,
                "Whilst keeping the ‘Radio Network ID’ button pressed, drag it out of its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘Radio Network ID’ button is displayed enabled." + Environment.NewLine +
                                "2. No sound is played.");

            /*
            Test Step 11
            Action: Slide back to the “Radio Network ID” button and hold it for 1 seconds.Then, slide out again.Note: Stopwatch is required for accuracy of test result
            Expected Result: Verify the following information,  (1)  The ‘Radio Network ID’ button turns to the ‘Enabled’ state without a sound
            Test Step Comment: (1) MMI_gen 8520 (partly: button ‘Radio Network ID’, MMI_gen 4388 (partly: to reset toggling state ‘Pressed’ and ‘Enabled’, no sound), MMI_gen 4389 (partly: to reset toggling state ‘Pressed’ and ‘Enabled’, no sound, Slide back));
            */
            DmiActions.ShowInstruction(this,
                @"Whilst keeping the ‘Radio Network ID’ button pressed, drag it back inside its area for 1s, then drag it outside again");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Radio Network ID’ button is displayed enabled." + Environment.NewLine +
                                "2. No sound is played.");

            /*
            Test Step 12
            Action: Slide back to the “Radio Network ID” button and hold it for 2 seconds.Note: Stopwatch is required for accuracy of test result
            Expected Result: While press and hold button less than 2 secThe state ‘pressed’ and ‘enabled’ are switched repeatly while button is pressed without a sound. While press and hold button over 2 secThe state of button is changed to ‘Pressed’ and without toggle
            Test Step Comment: (1) MMI_gen 8520 (partly: button ‘Radio Network ID’, MMI_gen 4389 (partly: start toggling state ‘Pressed’ and ‘Enabled’ when slide back, no sound));(2) MMI_gen 8520 (partly: button ‘Radio Network ID’, MMI_gen 4388 (partly: after 2 seconds, the button is change again to the state ‘Pressed’)); MMI_gen 4389 (partly: RBC contact window, 2 seconds timer);
            */
            DmiActions.ShowInstruction(this,
                @"Whilst keeping the ‘Radio Network ID’ button pressed, drag it back inside its area for more than 2s");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. While the button has been pressed for less than 2s it repeatedly changes between being displayed pressed and enabled;" +
                                "2. The ‘Click’ sound is not played." + Environment.NewLine +
                                "3. When the button has been pressed for more than 2s it is displayed pressed and does not change further.");

            /*
            Test Step 13
            Action: Release ‘Radio Network ID’ button
            Expected Result: Verify the following information,DMI displays Radio Network ID window.Use the log file to confirm that DMI sends out the packet [MMI_NEW_RBC_DATA (EVC-112)] with variable [MMI_NEW_RBC_DATA (EVC-112). MMI_M_BUTTONS] = 24 (BTN_RADIO_NETWORK_ID) and [MMI_NEW_RBC_DATA (EVC-112). MMI_N_DATA_ELEMENTS] = 0
            Test Step Comment: (1) MMI_gen 8520 (partly: button ‘Radio Network ID’, MMI_gen 4388 (partly: after 2 seconds button Up-Type is followed, button Delay-Type, MMI_gen 4381 (partly: exit state ‘Pressed’, execute function associated to the button)));(2) MMI_gen 9450 (partly: Radio Network ID);
            */
            DmiActions.ShowInstruction(this, @"Release the ‘Radio Network ID’ button");

            // Is this correct: no RBC data entered
            //EVC112_MMINewRbcData.MMI_NID_DATA = new List<byte>();
            EVC112_MMINewRbcData.MMI_M_BUTTONS = Variables.MMI_M_BUTTONS_RBC_DATA.BTN_YES_DATA_ENTRY_COMPLETE;
            EVC112_MMINewRbcData.CheckPacketContent();

            EVC22_MMICurrentRBC.MMI_NID_WINDOW = 9;
            EVC22_MMICurrentRBC.NetworkCaptions = new List<string> {"GSMR-A", "GSMR-B"};
            EVC22_MMICurrentRBC.MMI_Q_CLOSE_ENABLE = Variables.MMI_Q_CLOSE_ENABLE.Enabled;
            EVC22_MMICurrentRBC.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Radio Network ID window");

            /*
            Test Step 14
            Action: Press Close button
            Expected Result: DMI displays RBC contact window
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Close’  button");

            DmiExpectedResults.RBC_Contact_Window_displayed(this);

            /*
            Test Step 15
            Action: Press ‘Enter RBC data’ button.Then, enter and confirm all data in RBC data window as follows,RBC ID= 6996969RBC Phone number = 0031840880100
            Expected Result: DMI displays Main window
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Enter RBC data’ button");
            EVC22_MMICurrentRBC.MMI_NID_WINDOW = 10;
            EVC22_MMICurrentRBC.MMI_M_BUTTONS = EVC22_MMICurrentRBC.EVC22BUTTONS.BTN_YES_DATA_ENTRY_COMPLETE;
            EVC22_MMICurrentRBC.Send();

            DmiActions.ShowInstruction(this,
                "Enter ‘6996969RBC’ for RBC ID and confirm it and ‘0031840880100’ for Phone number and confirm it");

            EVC22_MMICurrentRBC.MMI_NID_WINDOW = 9;
            EVC22_MMICurrentRBC.NetworkCaptions.Clear();
            EVC22_MMICurrentRBC.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Main window");

            /*
            Test Step 16
            Action: Restart OTE and RBC simulator.Then, perform SoM until Level 2 is selected and confirmed
            Expected Result: Verify the following information,Use the log file to confirm that DMI received packet EVC-30 with the value in each bit of variable MMI_Q_REQUEST_ENABLE_64 as follows,Bit 19 = 1 (Enable Contact last RBC)
            Test Step Comment: (1) MMI_gen 9507 (partly: Enabled Contact last RBC);
            */
            DmiActions.ShowInstruction(this, "Power down the system, wait 10s then power up the system");

            DmiActions.Complete_SoM_L1_SB(this);
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Level = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_LEVEL.L2;

            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Main;
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.ContactLastRBC;
            EVC30_MMIRequestEnable.Send();
            EVC22_MMICurrentRBC.MMI_NID_WINDOW = 5;
            EVC22_MMICurrentRBC.Send();

            /*
            Test Step 17
            Action: Follow action step 2 – step 6 for ‘Contact last RBC’ button
            Expected Result: See the expected results of Step 2 – Step 6 and the following additional information,DMI close the RBC Contact window.Use the log file to confirm that DMI sends out the packet [MMI_NEW_RBC_DATA (EVC-112)] with variable [MMI_NEW_RBC_DATA (EVC-112). MMI_M_BUTTONS] = 21 (BTN_CONTACT_LAST_RBC) and [MMI_NEW_RBC_DATA (EVC-112). MMI_N_DATA_ELEMENTS] = 0.Use the log file to confirm that DMI sends out the packet [MMI_DRIVER_REQUEST (EVC-101)] with variable [MMI_DRIVER_REQUEST (EVC-101).MMI_M_REQUEST] = 57 (Contact last RBC)
            Test Step Comment: (1) MMI_gen 8516 (partly: MMI_gen 4557 (partly: button ‘Contact last RBC’)); MMI_gen 11928 (partly: close the Radio contact window);(2) MMI_gen 9450 (partly: Contact last RBC);(3) MMI_gen 11928 (partly: EVC-101);
            */
            WaitForVerification("Check the following (* indicates sub-areas drawn as one area):" + Environment.NewLine +
                                Environment.NewLine +
                                "1. DMI displays the RBC Contact window in areas D, F and G, with 3 layers, with the title ‘RBC Contact’." +
                                Environment.NewLine +
                                "2. The hour-glass, symbol ST05, is displayed vertically-centred in the window title, moving to the right each second and " +
                                Environment.NewLine +
                                "   returning to its original position after reaching the end of the window title area then continuing to move to the right." +
                                Environment.NewLine +
                                "3. Layer 0 comprises areas D, F, G, Y and Z." + Environment.NewLine +
                                "4. Layer 1 comprises areas (A1 + A2 + A3)*, A4, B*, C1, (C2 + C3 + C4)*, C5, C6, C7, C8, C9, E1, E2, E3, E4, (E5 - E9)*." +
                                Environment.NewLine +
                                "5. Layer 2 comprises areas B3, B4, B5, B6, B7." + Environment.NewLine +
                                "6. The window displays a disabled ‘Close’ button (symbol NA12) and 1 buttons on a keypad." +
                                Environment.NewLine +
                                @"7. Button #1 (enabled) has the label ‘Contact last RBC’" + Environment.NewLine +
                                "8. All objects, text messages and buttons are displayed in the same layer." +
                                Environment.NewLine +
                                "9. The Default window is not covering the current window.");

            DmiActions.ShowInstruction(this, "Press and hold the ‘Contact last RBC’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘Contact last RBC’ button is displayed pressed, without a border." +
                                Environment.NewLine +
                                "2. The ‘Click’ sound is played once.");

            DmiActions.ShowInstruction(this,
                @"Whilst keeping the ‘Contact last RBC’ button pressed, drag it out of its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘Contact last RBC’ button field is displayed enabled, with a border." +
                                Environment.NewLine +
                                "2. No sound is played.");

            DmiActions.ShowInstruction(this,
                @"Whilst keeping the ‘Contact last RBC’ button pressed, drag it back inside its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘Contact last RBC’ button is displayed pressed, without a border." +
                                Environment.NewLine +
                                "2. No sound is played.");

            DmiActions.ShowInstruction(this, @"Release the ‘Contact last RBC’ button");

            EVC112_MMINewRbcData.MMI_NID_DATA = new List<byte>();
            EVC112_MMINewRbcData.MMI_M_BUTTONS = Variables.MMI_M_BUTTONS_RBC_DATA.BTN_ENTER;
            EVC112_MMINewRbcData.CheckPacketContent();

            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Main;
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.ContactLastRBC;
            EVC30_MMIRequestEnable.Send();
            EVC22_MMICurrentRBC.MMI_NID_WINDOW = 5;
            EVC22_MMICurrentRBC.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the RBC Contact window");

            /*
            Test Step 18
            Action: Press ‘Level’ button.Then, select and confirm ‘Level 2’
            Expected Result: DMI displays RBC Contact window.(1)   The enabled Close button NA11 is display in area G
            Test Step Comment: (1) MMI_gen 4392 (partly: [Close] NA11);
            */
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Main;
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.Level;
            EVC30_MMIRequestEnable.Send();

            DmiActions.ShowInstruction(this, @"Press the ‘Level’ button");

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
                {Variables.MMI_M_LEVEL_NTC_ID.L2};
            EVC20_MMISelectLevel.Send();

            DmiActions.ShowInstruction(this, @"Select and confirm ‘Level 2’");

            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Level = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_LEVEL.L2;
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Main;
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.ContactLastRBC;
            EVC30_MMIRequestEnable.Send();
            EVC22_MMICurrentRBC.MMI_NID_WINDOW = 5;
            EVC22_MMICurrentRBC.MMI_Q_CLOSE_ENABLE = Variables.MMI_Q_CLOSE_ENABLE.Enabled;
            EVC22_MMICurrentRBC.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the RBC Contact window with an enabled close button, symbol NA211, in area G");

            /*
            Test Step 19
            Action: Restart OTE and RBC simulator.Then, perform SoM until Level 2 is selected and confirmed
            Expected Result: DMI displays RBC Contact window
            */
            DmiActions.ShowInstruction(this, "Power down the system, wait 10s then power up the system");

            DmiActions.Complete_SoM_L1_SB(this);
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Level = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_LEVEL.L2;

            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Main;
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.ContactLastRBC;
            EVC30_MMIRequestEnable.Send();
            EVC22_MMICurrentRBC.MMI_NID_WINDOW = 5;
            EVC22_MMICurrentRBC.MMI_Q_CLOSE_ENABLE = Variables.MMI_Q_CLOSE_ENABLE.Enabled;
            EVC22_MMICurrentRBC.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the RBC Contact window");

            /*
            Test Step 20
            Action: Follow action step 2 – step 6 for ‘Close’ button
            Expected Result: See the expected results of Step 2 – Step 6 and the following additional information,The RBC Contact window is closed, DMI displays Main window.Use the log file to confirm that DMI sends out the packet [MMI_DRIVER_REQUEST (EVC-101)])  with variable MMI_M_REQUEST] = 39 (Exit RBC contact)
            Test Step Comment: (1) MMI_gen 11239 (partly: close the window);(2) MMI_gen 8516 (partly: EVC-101, MMI_gen 4381 (partly: exit state ‘Pressed’, execute function associated to the button)); MMI_gen 11239 (partly: EVC-101);
            */
            WaitForVerification("Check the following (* indicates sub-areas drawn as one area):" + Environment.NewLine +
                                Environment.NewLine +
                                "1. DMI displays the RBC Contact window in areas D, F and G, with 3 layers, with the title ‘RBC Contact’." +
                                Environment.NewLine +
                                "2. The hour-glass, symbol ST05, is displayed vertically-centred in the window title, moving to the right each second and " +
                                Environment.NewLine +
                                "   returning to its original position after reaching the end of the window title area then continuing to move to the right." +
                                Environment.NewLine +
                                "3. Layer 0 comprises areas D, F, G, Y and Z." + Environment.NewLine +
                                "4. Layer 1 comprises areas (A1 + A2 + A3)*, A4, B*, C1, (C2 + C3 + C4)*, C5, C6, C7, C8, C9, E1, E2, E3, E4, (E5 - E9)*." +
                                Environment.NewLine +
                                "5. Layer 2 comprises areas B3, B4, B5, B6, B7." + Environment.NewLine +
                                "6. The window displays an enabled ‘Close’ button (symbol NA12) and 1 buttons on a keypad." +
                                Environment.NewLine +
                                @"7. Button #1 (enabled) has the label ‘Contact last RBC’" + Environment.NewLine +
                                "8. All objects, text messages and buttons are displayed in the same layer." +
                                Environment.NewLine +
                                "9. The Default window is not covering the current window.");

            DmiActions.ShowInstruction(this, "Press and hold the ‘Close’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘Close’ button is displayed pressed, without a border." + Environment.NewLine +
                                "2. The ‘Click’ sound is played once.");

            DmiActions.ShowInstruction(this, @"Whilst keeping the ‘Close’ button pressed, drag it out of its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘Close’ button field is displayed enabled, with a border." +
                                Environment.NewLine +
                                "2. No sound is played.");

            DmiActions.ShowInstruction(this,
                @"Whilst keeping the ‘Close’ button pressed, drag it back inside its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘Close’ button is displayed pressed, without a border." + Environment.NewLine +
                                "2. No sound is played.");

            DmiActions.ShowInstruction(this, @"Release the ‘Close’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Main window");

            /*
            Test Step 21
            Action: Restart OTE and RBC simulator.Then, perform SoM until Level 2 is selected and confirmed
            Expected Result: DMI displays RBC Contact window
            */
            DmiActions.ShowInstruction(this, "Power down the system, wait 10s then power up the system");

            DmiActions.Complete_SoM_L1_SB(this);
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Level = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_LEVEL.L2;

            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Main;
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.ContactLastRBC;
            EVC30_MMIRequestEnable.Send();
            EVC22_MMICurrentRBC.MMI_NID_WINDOW = 5;
            EVC22_MMICurrentRBC.MMI_Q_CLOSE_ENABLE = Variables.MMI_Q_CLOSE_ENABLE.Enabled;
            EVC22_MMICurrentRBC.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the RBC Contact window");

            /*
            Test Step 22
            Action: Use the test script file 22_8_3_1_b.xml to send EVC-22 with,MMI_NID_WINDOW  = 9MMI_N_NETWORKS = 0
            Expected Result: The RBC Contact window is closed, DMI displays Main window
            Test Step Comment: MMI_gen 11241;
            */
            XML_22_8_3_1(msgType.typeb);

            // DMI seems to have trouble sometimes with remembering the window stack after a re-boot (this was causing the default window to be the parent
            // of the RBC contact window even though Main was displayed and was the parent)
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Main;
            EVC30_MMIRequestEnable.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI closes the RBC Contact window and displays the Main window.");

            /*
            Test Step 23
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }

        #region Send_XML_22_8_3_1_DMI_Test_Specification

        enum msgType
        {
            typea,
            typeb
        }

        private void XML_22_8_3_1(msgType type)
        {
            EVC22_MMICurrentRBC.NID_RBC = 0;
            EVC22_MMICurrentRBC.MMI_NID_RADIO = 0;
            EVC22_MMICurrentRBC.MMI_Q_CLOSE_ENABLE = Variables.MMI_Q_CLOSE_ENABLE.Disabled;
            EVC22_MMICurrentRBC.MMI_M_BUTTONS = EVC22_MMICurrentRBC.EVC22BUTTONS.NoButton;
            switch (type)
            {
                case msgType.typea:
                    EVC22_MMICurrentRBC.MMI_NID_WINDOW = 5;
                    break;
                case msgType.typeb:
                    EVC22_MMICurrentRBC.MMI_NID_WINDOW = 9;
                    EVC22_MMICurrentRBC.NetworkCaptions =
                        new List<string>(); // empty list so count now 0 to close window
                    break;
            }
            EVC22_MMICurrentRBC.Send();
        }

        #endregion
    }
}
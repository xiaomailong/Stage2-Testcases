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
using Testcase.Telegrams.EVCtoDMI;
using Testcase.Telegrams.DMItoEVC;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 27.21 Settings Window
    /// TC-ID: 22.21
    /// 
    /// This test case verifies the display of the ‘Settings’ window shall comply with [ERA-ERTMS] standard and [MMI-ETCS-gen].
    /// 
    /// Tested Requirements:
    /// MMI_gen 8466; MMI_gen 8469; MMI_gen 8465 (partly: MMI_gen 7909, MMI_gen 4557 (partly: Language button, Volume button, Brightness button, System Version button, Set VBC button, Remove VBC button, Brake button, System Info button, Set Clock button, Maintenance button, Close button), MMI_gen 4381, MMI_gen 4382, MMI_gen 4556 (partly: Close button, Window Title), MMI_gen 4630); MMI_gen 8467 (partly: touch screen); MMI_gen 11545; MMI_gen 9512; MMI_gen 968; MMI_gen 12057; MMI_gen 4360 (partly: window title); MMI_gen 4375; MMI_gen 4374; MMI_gen 4392 (partly: [Close] NA11, returning to the parent window); MMI_gen 1088 (Partly, Bit #13 to #18 and #25 to #26); MMI_gen 4350; MMI_gen 4351; MMI_gen 4353; MMI_gen 4354;
    /// 
    /// Scenario:
    /// The concerned buttons in the ‘Settings’ window are verified by the following actions:Press the button and holdSlide the button out with force appliedSlide the button back with force appliedRelease the buttonThe ‘Settings’ menu window is opened and verified.The Up-Type ‘Language’, ‘Volume’, ‘Brightness’, ‘System version’, ‘Set VBC’, ‘Brake’, ‘System info’, ‘Maintenance’, ‘Remove VBC’ buttons are verified.The Safe Delay-Type ‘Non-leading’, ‘Shunting’, ‘Shunting Exit’, ‘Maintain Shunting’ buttons are verified.The enabling/disabling buttons are verified.Use the test script file to send EVC-30 packet information. Then, verify the state of Set-Clock button.The Up-Type ‘Close’’ button is verified.
    /// 
    /// Used files:
    /// 22_21_a.xml, 22_21_b.xml, 22_21_c.xml, 22_21_d.xml
    /// </summary>
    public class TC_ID_22_21_Settings_Window : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:

            // Call the TestCaseBase PreExecution
            base.PreExecution();

            // Test system is powered on Cabin A is activated.
            DmiActions.Start_ATP();
            DmiActions.Activate_Cabin_1(this);
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in SB mode

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint

            TraceInfo("This test case may require a DMI configuration change - " +
                      "System info may not be disabled/enabled by text). If this is not done manually, the test may fail!");
            /*
            Test Step 1
            Action: Press ‘Settings’ button
            Expected Result: DMI displays Settings window.Verify the following points,Menu windowsThe Settings window is displayed in main area D/F/G.The window title is ‘Settings’.The following objects are displayed Main window, Enabled Close button (NA11)Window TitleButton 1 with Symbol SE03 for Language Button 2 with Symbol SE02 for VolumeButton 3 with SE01 for BrightnessButton 4 with label ‘System version’Button 5 with label ‘Set VBC’Button 6 with label ‘Remove VBC’Note: See the position of buttons in picture below,The state of each button in Special window are displayed correctly as follows,Language = EnableVolume = EnableBrightness = EnableSystem version = EnableSet VBC = EnableRemove VBC = DisableSet clock = EnableThe other additional buttons from list above also display in this window (e.g. Maintenance, Brake, System Info, etc.).LayersThe level of layers in each area of window as follows,Layer 0: Area D, F, G, E10, E11, Y, and ZLayer -1: Area A1, (A2+A3)*, A4, B*, C1, (C2+C3+C4)*, C5, C6, C7, C8, C9, E1, E2, E3, E4, (E5-E9)*.Layer -2: Area B3, B4, B5, B6 and B7.Note: ‘*’ symbol is mean that specified area are drawn as one area.Packet transmissionUse the log file to confirm that DMI receives EVC-30 with following value in each bit of variable MMI_Q__REQUEST_ENABLE_64,Bit #13 = 1 (Language)Bit #14 = 1 (Volume)Bit #15 = 1 (Brightness)Bit #16 = 1 (System version)Bit #17 = 1 (Set VBC)Bit #18 = 0 (Remove VBC)Bit #25 or Bit #26 = 1 (Set Clock)And the buttons are enabled according to bit value = 1. General property of windowThe Settings window is presented with objects and buttons which is the one of several levels and allocated to areas of DMI..All objects, text messages and buttons are presented within the same layer.The Default window is not displayed and covered the current window.Sub-level window covers partially depending on the size of the Sub-Level window. There is no other window is displayed and activated at the same time
            Test Step Comment: (1) MMI_gen 8465 (partly: MMI_gen 7909);(2) MMI_gen 8466; MMI_gen 4360 (partly: window title);(3) MMI_gen 8645 (partly: MMI_gen 4556 (partly: Close button, Window Title));    MMI_gen 8467 (partly: touch screen, button with label, Language, Volume, Brightness, System version, Set VBC, Remove VBC); MMI_gen 4392 (partly: [Close] NA11);                   (4) MMI_gen 11545 (partly: EVC-30, enabling #13, #14, #15, #16, #17, #25 or #26, disabling #18); (5) MMI_gen 8469; (6) MMI_gen 8645 (partly: MMI_gen 4630, MMI gen 5944 (partly: touch screen));(7) MMI_gen11545 (partly: enabling buttons, disabling ‘remove vbc’ button, EVC-30); MMI_gen 1088 (Partly, Bit #13 to #18 and #25 to #26)            (8) MMI_gen 4350;(9) MMI_gen 4351;(10) MMI_gen 4353;(11) MMI_gen 4354;
            */
/* This may be required
            DmiActions.Set_Driver_ID(this, "1234");
            DmiActions.ShowInstruction(this, "Confirm the Driver ID");
*/
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.StandBy;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Level = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_LEVEL.L1;
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.EVC30WindowID.Default;      // Default window
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_LOW = true;
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = (EVC30_MMIRequestEnable.EnabledRequests.Language |
                                                                EVC30_MMIRequestEnable.EnabledRequests.Volume |
                                                                EVC30_MMIRequestEnable.EnabledRequests.Brightness |
                                                                EVC30_MMIRequestEnable.EnabledRequests.SystemVersion |
                                                                EVC30_MMIRequestEnable.EnabledRequests.SetVBC |
                                                                EVC30_MMIRequestEnable.EnabledRequests.SetLocalTimeDateAndOffset |
                                                                EVC30_MMIRequestEnable.EnabledRequests.EnableBrakePercentage |
                                                                EVC30_MMIRequestEnable.EnabledRequests.EnableWheelDiameter) &
                                                               ~EVC30_MMIRequestEnable.EnabledRequests.RemoveVBC;
            EVC30_MMIRequestEnable.Send();

            DmiActions.ShowInstruction(this, @"Press the ‘Settings’ button");

            WaitForVerification("Check the following (* indicates sub-areas drawn as one area):" + Environment.NewLine + Environment.NewLine +
                                @"1. DMI displays the Settings window with 3 layers, with the title ‘Settings’." + Environment.NewLine +
                                "2. The Settings window is displayed in areas D, F, G." + Environment.NewLine +
                                "3. Layer 0 comprises areas D, F, G, E10, E11, Y and Z." + Environment.NewLine +
                                "4. Layer 1 comprises areas A1, (A2+A3)*, A4, B, C1, (C2+C3+c4)*, C5, C6, C7, C8, C9, E1, E2, E3, E4, (E5-E9)*." + Environment.NewLine +
                                "5. Layer 2 comprises areas B3, B4, B5, B6 and B7." + Environment.NewLine +
                                @"6. The Override window displays Settings buttons (as described) and an ‘Enabled Close’ button (symbol NA11)." + Environment.NewLine +
                                "7. The first row of buttons displayed is: ‘Language’ (enabled), ‘Volume’ (enabled);" + Environment.NewLine +
                                "8. The second row of buttons displayed is: ‘Brightness’ (enabled), ‘System version’ (enabled);" + Environment.NewLine +
                                "9. The third row of buttons displayed is: ‘Set VBC’ (enabled), ‘Remove VBC’ (disabled);" + Environment.NewLine +
                                "10. The first buttons in the next row displayed is: ‘Language’ (enabled), ‘Volume’ (enabled);" + Environment.NewLine +
                                "11. Other buttons such as ‘Maintenance’, ‘Brake’ are also displayed." + Environment.NewLine +
                                "12. Objects, text messages and buttons can be displayed in several levels. Within a level they are allocated to areas." + Environment.NewLine +
                                "13. Objects, text messages and buttons in a layer form a window." + Environment.NewLine +
                                "14. The Default window does not cover the current window." + Environment.NewLine +
                                "15. A sub-level window can partially cover another window, depending on its size.Another window cannot be displayed and activated at the same time.");

            /*
            Test Step 2
            Action: Press and hold ‘Language’ button
            Expected Result: Verify the following information,The sound ‘Click’ is played once.The ‘Language’ button is shown as pressed state, the border of button is removed
            Test Step Comment: (1) MMI_gen 8465 (partly: MMI_gen 4557 (partly: Language button),  MMI_gen 4381 (partly: the sound for Up-Type button))); MMI_gen 9512; MMI_gen 968; (2) MMI_gen 8465 (partly: MMI_gen 4557 (partly: Language button) , MMI_gen 4381 (partly: change to state ‘Pressed’ as long as remain actuated))); MMI_gen 4375;
            */
            DmiActions.ShowInstruction(this, @"Press and hold the ‘Language’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Language’ button is displayed pressed, without a border." + Environment.NewLine +
                                "2. The ‘Click’ sound is played once.");

            /*
            Test Step 3
            Action: Slide out of ‘Language’ button
            Expected Result: The border of the button is shown (state ‘Enabled’) without a sound
            Test Step Comment: (1) MMI_gen 8465 (partly: MMI_gen 4557 (partly: Language button, MMI_gen 4382 (partly: state ‘Enabled’ when slide out with force applied, no sound)));
            */
            DmiActions.ShowInstruction(this, @"Whilst keeping the ‘Language’ button pressed, drag it out of its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘Language’ button is displayed enabled, with a border." + Environment.NewLine +
                                "2. No sound is played.");

            /*
            Test Step 4
            Action: Slide back into ‘Language’ button
            Expected Result: The button is back to state ‘Pressed’ without a sound
            Test Step Comment: (1) MMI_gen 8465 (partly: MMI_gen 4557 (partly: Language button, MMI_gen 4382 (partly: state ‘Pressed’ when slide back, no sound)));  
            */
            DmiActions.ShowInstruction(this, @"Whilst keeping the ‘Language’ button pressed, drag it back inside its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘Language’ button is displayed pressed." + Environment.NewLine +
                                "2. No sound is played.");

            /*
            Test Step 5
            Action: Release the ‘Language’ button
            Expected Result: DMI displays Language window
            Test Step Comment: MMI_gen 8465 (partly: MMI_gen 4557 (partly: Language button),  MMI_gen 4381 (partly: exit state ‘Pressed’, execute function associated to the button));  
            */
            DmiActions.ShowInstruction(this, @"Release the ‘Language’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Language window.");

            /*
            Test Step 6
            Action: Press ‘Close’ button
            Expected Result: DMI displays Settings window
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Settings window.");

            /*
            Test Step 7
            Action: Follow action step 2 – step 6 respectively for the following button.‘Volume’ button.‘Brightness’ button.‘System version’’ button.‘Set VBC’ button.‘Brake’ button.‘System info’ button.‘Set Clock’ button.‘Maintenance’ button
            Expected Result: See the expected results of Step 2 – Step 5 and the following additional information,DMI displays corresponding window refer to released button from action step 5. When, released ‘System version’ button, use the log file to confirm that DMI sends out the packet [MMI_DRIVER_REQUEST (EVC-101)] with variable [MMI_DRIVER_REQUEST (EVC-101).MMI_M_REQUEST] = 55 (System version request)
            Test Step Comment: (1) MMI_gen 8465 (partly: MMI_gen 4557 (partly: Volume button, Brightness button, System version button, Set VBC button, Additional DMI technical functions));(2) MMI_gen 12057;
            */
            // Repeat Step 2 for Volume
            DmiActions.ShowInstruction(this, @"Press and hold the ‘Volume’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Volume’ button is displayed pressed, without a border." + Environment.NewLine +
                                "2. The ‘Click’ sound is played once.");

            // Repeat Step 3 for Volume
            DmiActions.ShowInstruction(this, @"Whilst keeping the ‘Volume’ button pressed, drag it out of its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘Volume’ button is displayed enabled, with a border." + Environment.NewLine +
                                "2. No sound is played.");

            // Repeat Step 4 for Volume
            DmiActions.ShowInstruction(this, @"Whilst keeping the ‘Volume’ button pressed, drag it back inside its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘Volume’ button is displayed pressed." + Environment.NewLine +
                                "2. No sound is played.");

            // Repeat Step 5 for Volume
            DmiActions.ShowInstruction(this, @"Release the ‘Volume’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Volume window.");

            // Repeat Step 6
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Settings window.");

            // Repeat Step 2 for Brightness
            DmiActions.ShowInstruction(this, @"Press and hold the ‘Brightness’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Brightness’ button is displayed pressed, without a border." + Environment.NewLine +
                                "2. The ‘Click’ sound is played once.");

            // Repeat Step 3 for Brightness
            DmiActions.ShowInstruction(this, @"Whilst keeping the ‘Brightness’ button pressed, drag it out of its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘Brightness’ button is displayed enabled, with a border." + Environment.NewLine +
                                "2. No sound is played.");

            // Repeat Step 4 for Brightness
            DmiActions.ShowInstruction(this, @"Whilst keeping the ‘Brightness’ button pressed, drag it back inside its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘Brightness’ button is displayed pressed." + Environment.NewLine +
                                "2. No sound is played.");

            // Repeat Step 5 for Brightness
            DmiActions.ShowInstruction(this, @"Release the ‘Brightness’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Brightness window.");

            // Repeat Step 6
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Settings window.");

            // Repeat Step 2 for System version
            DmiActions.ShowInstruction(this, @"Press and hold the ‘System version’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘System version’ button is displayed pressed, without a border." + Environment.NewLine +
                                "2. The ‘Click’ sound is played once.");

            // Repeat Step 3 for System version
            DmiActions.ShowInstruction(this, @"Whilst keeping the ‘System version’ button pressed, drag it out of its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘System version’ button is displayed enabled, with a border." + Environment.NewLine +
                                "2. No sound is played.");

            // Repeat Step 4 for System version
            DmiActions.ShowInstruction(this, @"Whilst keeping the ‘System version’ button pressed, drag it back inside its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘System version’ button is displayed pressed." + Environment.NewLine +
                                "2. No sound is played.");

            // Repeat Step 5 for System version
            DmiActions.ShowInstruction(this, @"Release the ‘System version’ button");

            EVC34_MMISystemVersion.Send();
            EVC101_MMIDriverRequest.CheckMRequestReleased = Variables.MMI_M_REQUEST.SystemVersionRequest;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the System version window.");

            // Repeat Step 6
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Settings window.");

            // Repeat Step 2 for Set VBC
            DmiActions.ShowInstruction(this, @"Press and hold the ‘Set VBC’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Set VBC’ button is displayed pressed, without a border." + Environment.NewLine +
                                "2. The ‘Click’ sound is played once.");

            // Repeat Step 3 for Set VBC
            DmiActions.ShowInstruction(this, @"Whilst keeping the ‘Set VBC’ button pressed, drag it out of its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘Set VBC’ button is displayed enabled, with a border." + Environment.NewLine +
                                "2. No sound is played.");

            // Repeat Step 4 for Set VBC
            DmiActions.ShowInstruction(this, @"Whilst keeping the ‘Set VBC’ button pressed, drag it back inside its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘Set VBC’ button is displayed pressed." + Environment.NewLine +
                                "2. No sound is played.");

            // Repeat Step 5 for Set VBC
            DmiActions.ShowInstruction(this, @"Release the ‘Set VBC’ button");

            EVC18_MMISetVBC.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Set VBC window.");

            // Repeat Step 6
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Settings window.");

            // Repeat Step 2 for Brake button
            DmiActions.ShowInstruction(this, @"Press and hold the ‘Brake’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Brake’ button is displayed pressed, without a border." + Environment.NewLine +
                                "2. The ‘Click’ sound is played once.");

            // Repeat Step 3 for Brake button
            DmiActions.ShowInstruction(this, @"Whilst keeping the ‘Brake’ button pressed, drag it out of its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘Brake’ button is displayed enabled, with a border." + Environment.NewLine +
                                "2. No sound is played.");

            // Repeat Step 4 for Brake button
            DmiActions.ShowInstruction(this, @"Whilst keeping the ‘Brake’ button pressed, drag it back inside its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘Brake button’ button is displayed pressed." + Environment.NewLine +
                                "2. No sound is played.");

            // Repeat Step 5 for Brake button
            DmiActions.ShowInstruction(this, @"Release the ‘Brake’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Brake window.");

            // Repeat Step 6
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Settings window.");

            // Repeat Step 2 for System info
            TraceInfo("If the System info button has not been configured correctly the test will fail - Ignore and pass the test");

            DmiActions.ShowInstruction(this, @"Press and hold the ‘System info’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘System info’ button is displayed pressed, without a border." + Environment.NewLine +
                                "2. The ‘Click’ sound is played once.");

            // Repeat Step 3 for System info
            DmiActions.ShowInstruction(this, @"Whilst keeping the ‘System info’ button pressed, drag it out of its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘System info’ button is displayed enabled, with a border." + Environment.NewLine +
                                "2. No sound is played.");

            // Repeat Step 4 for System info
            DmiActions.ShowInstruction(this, @"Whilst keeping the ‘System info’ button pressed, drag it back inside its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘System info’ button is displayed pressed." + Environment.NewLine +
                                "2. No sound is played.");

            // Repeat Step 5 for System info
            DmiActions.ShowInstruction(this, @"Release the ‘System info’ button");

            EVC24_MMISystemInfo.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the System info window.");

            // Repeat Step 6
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Settings window.");

            // Repeat Step 2 for Set Clock
            DmiActions.ShowInstruction(this, @"Press and hold the ‘Set Clock’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Set Clock’ button is displayed pressed, without a border." + Environment.NewLine +
                                "2. The ‘Click’ sound is played once.");

            // Repeat Step 3 for Set Clock
            DmiActions.ShowInstruction(this, @"Whilst keeping the ‘Set Clock’ button pressed, drag it out of its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘Set Clock’ button is displayed enabled, with a border." + Environment.NewLine +
                                "2. No sound is played.");

            // Repeat Step 4 for Set Clock
            DmiActions.ShowInstruction(this, @"Whilst keeping the ‘Set Clock’ button pressed, drag it back inside its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘Set Clock’ button is displayed pressed." + Environment.NewLine +
                                "2. No sound is played.");

            // Repeat Step 5 for Set Clock
            DmiActions.ShowInstruction(this, @"Release the ‘Set Clock’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Set Clock window.");

            // Repeat Step 6
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Settings window.");

            // Repeat Step 2 for Maintenance
            DmiActions.ShowInstruction(this, @"Press and hold the ‘Maintenance’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Maintenance’ button is displayed pressed, without a border." + Environment.NewLine +
                                "2. The ‘Click’ sound is played once.");

            // Repeat Step 3 for Maintenance
            DmiActions.ShowInstruction(this, @"Whilst keeping the ‘Maintenance’ button pressed, drag it out of its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘Maintenance’ button is displayed enabled, with a border." + Environment.NewLine +
                                "2. No sound is played.");

            // Repeat Step 4 for Maintenance
            DmiActions.ShowInstruction(this, @"Whilst keeping the ‘Maintenance’ button pressed, drag it back inside its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘Maintenance’ button is displayed pressed." + Environment.NewLine +
                                "2. No sound is played.");

            // Repeat Step 5 for Maintenance
            DmiActions.ShowInstruction(this, @"Release the ‘Maintenance’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Maintenance window.");

            // Repeat Step 6
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Settings window.");
            /*
            Test Step 8
            Action: Perform the following procedure,Enter and confirm the specify value VBC code = 65536Press ‘Yes’ button
            Expected Result: DMI displays Set VBC data validation window
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Set VBC’ button");

            EVC18_MMISetVBC.MMI_M_BUTTONS = Variables.MMI_M_BUTTONS_VBC.BTN_YES_DATA_ENTRY_COMPLETE;
            EVC18_MMISetVBC.Send();

            DmiActions.ShowInstruction(this, @"Enter ‘65536’ for the VBC code and confirm the value, then press the ‘Yes’ button");

            EVC28_MMIEchoedSetVBCData.MMI_M_VBC_CODE_ = 65536;     // 65536 bit-inverted
            EVC28_MMIEchoedSetVBCData.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Set VBC data validation window.");

            /*
            Test Step 9
            Action: Press ‘Yes’ button. Then, confirm selected value by pressing an input field
            Expected Result: DMI displays Settings window.Verify the following information,Use the log file to confirm that DMI receives EVC-30 with variable MMI_Q_REQUEST_ENABLE_64 (#18) = 1 and the ‘Remove VBC’ button is enabled
            Test Step Comment: (1) MMI_gen 11545 (partly: enable #18, EVC-30)
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Yes’ button and confirm the value by pressing a data input field");

            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.EVC30WindowID.Default;      // Main window
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.Language |
                                                               EVC30_MMIRequestEnable.EnabledRequests.Volume |
                                                               EVC30_MMIRequestEnable.EnabledRequests.Brightness |
                                                               EVC30_MMIRequestEnable.EnabledRequests.SystemVersion |
                                                               EVC30_MMIRequestEnable.EnabledRequests.RemoveVBC | 
                                                               EVC30_MMIRequestEnable.EnabledRequests.SetLocalTimeDateAndOffset |
                                                               EVC30_MMIRequestEnable.EnabledRequests.StartBrakeTest |
                                                               EVC30_MMIRequestEnable.EnabledRequests.SetVBC;
            EVC30_MMIRequestEnable.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Settings window with the ‘Remove VBC’ button enabled.");

            /*
            Test Step 10
            Action: Follow action step 2 – step 6 respectively for ‘Remove VBC’ button
            Expected Result: See the expected results of Step 2 – Step 5 and the following additional information,DMI displays Remove VBC window
            Test Step Comment: (1) MMI_gen 8465 (partly: MMI_gen 4557 (partly: Remove VBC button));
            */
            // Repeat Step 2 for Remove VBC
            DmiActions.ShowInstruction(this, @"Press and hold the ‘Remove VBC’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Remove VBC’ button is displayed pressed, without a border." + Environment.NewLine +
                                "2. The ‘Click’ sound is played once.");

            // Repeat Step 3 for Remove VBC
            DmiActions.ShowInstruction(this, @"Whilst keeping the ‘Remove VBC’ button pressed, drag it out of its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘Remove VBC’ button is displayed enabled, with a border." + Environment.NewLine +
                                "2. No sound is played.");

            // Repeat Step 4 for Remove VBC
            DmiActions.ShowInstruction(this, @"Whilst keeping the ‘Remove VBC’ button pressed, drag it back inside its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘Remove VBC’ button is displayed pressed." + Environment.NewLine +
                                "2. No sound is played.");

            // Repeat Step 5 for Remove VBC
            DmiActions.ShowInstruction(this, @"Release the ‘Remove VBC’ button");

            EVC19_MMIRemoveVBC.MMI_N_VBC = 0;
            EVC19_MMIRemoveVBC.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Remove VBC window.");

            // No need to repeat Step 6: that is Step 11

            /*
            Test Step 11
            Action: Press ‘Close’ button
            Expected Result: DMI displays Settings window
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button");

            EVC19_MMIRemoveVBC.MMI_N_VBC = 1;
            EVC19_MMIRemoveVBC.MMI_Q_DATA_CHECK = Variables.Q_DATA_CHECK.All_checks_passed;
            EVC19_MMIRemoveVBC.Send(); 

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Settings window.");

            /*
            Test Step 12
            Action: Perform the following procedure,Drive the train forward until the brake is appliedStop driving the train
            Expected Result: Verify the following information,The following buttons are disabled,LanguageVolumeBrightnessSystem VersionSet VBCRemove VBCUse the log file to confirm that DMI receives EVC-30 with following value in each bit of variable MMI_Q_REQUEST_ENABLE_64Bit #13 = 0 (Language)Bit #14 = 0 (Volume)Bit #15 = 0 (Brightness)Bit #16 = 0 (System version)Bit #17 = 0 (Set VBC)Bit #18 = 0 (Remove VBC)
            Test Step Comment: (1) MMI_gen 11545 (partly: Disabling, #13, #14, #15, #16, #17, #18);(2) MMI_gen 11545 (partly: EVC-30, Disabling, #13, #14, #15, #16, #17, #18);
            */
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.EVC30WindowID.Settings;      // Settings window
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.None;
            EVC30_MMIRequestEnable.Send();
            
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 260;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The following buttons are disabled ‘Language’, ‘Volume’, ‘Brightness’, ‘System Version’, ‘Set VBC’, ‘Remove VBC’.");

            /*
            Test Step 13
            Action: Acknowledge the ‘Brake intervention’ symbol by pressing area E1
            Expected Result: Verify the following information,The following buttons are disabled,LanguageVolumeBrightnessSystem VersionSet VBCRemove VBCUse the log file to confirm that DMI receives EVC-30 with following value in each bit of variable MMI_Q_REQUEST_ENABLE_64Bit #13 = 1 (Language)Bit #14 = 1 (Volume)Bit #15 = 1 (Brightness)Bit #16 = 1 (System version)Bit #17 = 1 (Set VBC)Bit #18 = 1 (Remove VBC)
            Test Step Comment: (1) MMI_gen 11545 (partly: Enabling, #13, #14, #15, #16, #17, #18);(2) MMI_gen 11545 (partly: EVC-30, Enabling, #13, #14, #15, #16, #17, #18);
            */
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 260;
            EVC8_MMIDriverMessage.Send();

            DmiActions.ShowInstruction(this, @"Acknowledge the ‘Brake intervention’ symbol by pressing in area E1");

            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 4;
            EVC8_MMIDriverMessage.Send();

            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.EVC30WindowID.Settings;      // Settings window
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.Language |
                                                               EVC30_MMIRequestEnable.EnabledRequests.Volume |
                                                               EVC30_MMIRequestEnable.EnabledRequests.Brightness |
                                                               EVC30_MMIRequestEnable.EnabledRequests.SystemVersion |
                                                               EVC30_MMIRequestEnable.EnabledRequests.RemoveVBC |
                                                               EVC30_MMIRequestEnable.EnabledRequests.SetLocalTimeDateAndOffset |
                                                               EVC30_MMIRequestEnable.EnabledRequests.StartBrakeTest |
                                                               EVC30_MMIRequestEnable.EnabledRequests.SetVBC;
            EVC30_MMIRequestEnable.Send();

            // Spec says buttons are enabled but indicates enabled bits
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The following buttons are enabled ‘Language’, ‘Volume’, ‘Brightness’, ‘System Version’, ‘Set VBC’, ‘Remove VBC’.");

            /*
            Test Step 14
            Action: Use the test script file 22_21_a.xml to send EVC-30 with,MMI_Q_REQUEST_ENABLE_64 _#25 = 0MMI_Q_REQUEST_ENABLE_64 _#26 = 0
            Expected Result: The ‘Set Clock’ button is disabled
            Test Step Comment: MMI_gen 11545 (partly: EVC-30, Disabling, #25, #26);
            */
            XML_22_21(msgType.typea);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                          "1. The following ‘Set Clock’ button is disabled.");

            /*
            Test Step 15
            Action: Use the test script file 22_21_b.xml to send EVC-30 with,MMI_Q_REQUEST_ENABLE_64 _#25 = 1MMI_Q_REQUEST_ENABLE_64 _#26 = 0
            Expected Result: The ‘Set Clock’ button is enabled
            Test Step Comment: MMI_gen 11545 (partly: EVC-30, Enabling, #25);
            */
            XML_22_21(msgType.typeb);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                          "1. The following ‘Set Clock’ button is enabled.");

            /*
            Test Step 16
            Action: Use the test script file 22_21_a.xml to send EVC-30 with,MMI_Q_REQUEST_ENABLE_64 _#25 = 0MMI_Q_REQUEST_ENABLE_64 _#26 = 0
            Expected Result: The ‘Set Clock’ button is disabled
            */
            XML_22_21(msgType.typea);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                          "1. The following ‘Set Clock’ button is disabled.");

            /*
            Test Step 17
            Action: Use the test script file 22_21_c.xml to send EVC-30 with,MMI_Q_REQUEST_ENABLE_64 _#25 = 1MMI_Q_REQUEST_ENABLE_64 _#26 = 1
            Expected Result: The ‘Set Clock’ button is enabled
            Test Step Comment: MMI_gen 11545 (partly: EVC-30, Enabling, #25, #26);
            */
            XML_22_21(msgType.typec);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                          "1. The following ‘Set Clock’ button is enabled.");

            /*
            Test Step 18
            Action: Use the test script file 22_21_a.xml to send EVC-30 with,MMI_Q_REQUEST_ENABLE_64 _#25 = 0MMI_Q_REQUEST_ENABLE_64 _#26 = 0
            Expected Result: The ‘Set Clock’ button is disabled
            */
            XML_22_21(msgType.typea);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                          "1. The following ‘Set Clock’ button is disabled.");

            /*
            Test Step 19
            Action: Use the test script file 22_21_d.xml to send EVC-30 with,MMI_Q_REQUEST_ENABLE_64 _#25 = 0MMI_Q_REQUEST_ENABLE_64 _#26 = 1
            Expected Result: The ‘Set Clock’ button is enabled
            Test Step Comment: MMI_gen 11545 (partly: EVC-30, Enabling, #26);
            */
            XML_22_21(msgType.typed);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                          "1. The following ‘Set Clock’ button is enabled.");

            /*
            Test Step 20
            Action: Follow action step 2 – step 6 respectively for ‘Close’ button
            Expected Result: See the expected results of Step 2 – Step 6 and the following additional information,  (1)    DMI displays Default window
            Test Step Comment: (1) MMI_gen 8465 (partly: MMI_gen 4557 (partly: Close button)); MMI_gen 4392 (partly:returning to the parent window);
            */
            // Repeat Step 2 for Close
            DmiActions.ShowInstruction(this, @"Press and hold the ‘Close’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Close’ button is displayed pressed, without a border." + Environment.NewLine +
                                "2. The ‘Click’ sound is played once.");

            // Repeat Step 3 for Close
            DmiActions.ShowInstruction(this, @"Whilst keeping the ‘Close’ button pressed, drag it out of its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘Close’ button is displayed enabled, with a border." + Environment.NewLine +
                                "2. No sound is played.");

            // Repeat Step 4 for Close
            DmiActions.ShowInstruction(this, @"Whilst keeping the ‘Close’ button pressed, drag it back inside its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘Close’ button is displayed pressed." + Environment.NewLine +
                                "2. No sound is played.");

            // Repeat Step 5 for Close
            DmiActions.ShowInstruction(this, @"Release the ‘Close’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Settings window.");

            // Repeat Step 6
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Default window.");

            /*
            Test Step 21
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }

        #region Send_XML_22_21_DMI_Test_Specification
        enum msgType
        {
            typea,
            typeb,
            typec,
            typed
        }

        private void XML_22_21(msgType type)
        {
            EVC30_MMIRequestEnable.SendBlank();
            switch (type)
            {
                case msgType.typea:
                    EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.Language |
                                                                       EVC30_MMIRequestEnable.EnabledRequests.Volume |
                                                                       EVC30_MMIRequestEnable.EnabledRequests.Brightness |
                                                                       EVC30_MMIRequestEnable.EnabledRequests.SystemVersion |
                                                                       EVC30_MMIRequestEnable.EnabledRequests.SetVBC;
                    break;
                case msgType.typeb:
                    EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.Language |
                                                                       EVC30_MMIRequestEnable.EnabledRequests.Volume |
                                                                       EVC30_MMIRequestEnable.EnabledRequests.Brightness |
                                                                       EVC30_MMIRequestEnable.EnabledRequests.SystemVersion |
                                                                       EVC30_MMIRequestEnable.EnabledRequests.SetVBC |
                                                                       EVC30_MMIRequestEnable.EnabledRequests.SetLocalTimeDateAndOffset;
                    break;
                case msgType.typec:
                    EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.Language |
                                                                       EVC30_MMIRequestEnable.EnabledRequests.Volume |
                                                                       EVC30_MMIRequestEnable.EnabledRequests.Brightness |
                                                                       EVC30_MMIRequestEnable.EnabledRequests.SystemVersion |
                                                                       EVC30_MMIRequestEnable.EnabledRequests.SetVBC |
                                                                       EVC30_MMIRequestEnable.EnabledRequests.SetLocalTimeDateAndOffset |
                                                                       EVC30_MMIRequestEnable.EnabledRequests.SetLocalOffset;
                    break;
                case msgType.typed:
                    EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.Language |
                                                                       EVC30_MMIRequestEnable.EnabledRequests.Volume |
                                                                       EVC30_MMIRequestEnable.EnabledRequests.Brightness |
                                                                       EVC30_MMIRequestEnable.EnabledRequests.SystemVersion |
                                                                       EVC30_MMIRequestEnable.EnabledRequests.SetVBC |
                                                                       EVC30_MMIRequestEnable.EnabledRequests.SetLocalOffset;
                    break;
            }
            EVC20_MMISelectLevel.Send();
        }
        #endregion

    }
}
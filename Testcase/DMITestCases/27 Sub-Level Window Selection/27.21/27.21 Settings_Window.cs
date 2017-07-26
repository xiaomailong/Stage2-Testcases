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
    public class Settings_Window : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // Test system is powered on Cabin A is activated.

            // Call the TestCaseBase PreExecution
            base.PreExecution();
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


            /*
            Test Step 1
            Action: Press ‘Settings’  button.
            Expected Result: DMI displays Settings window.Verify the following points,Menu windowsThe Settings window is displayed in main area D/F/G.The window title is ‘Settings’.The following objects are displayed Main window, Enabled Close button (NA11)Window TitleButton 1 with Symbol SE03 for Language Button 2 with Symbol SE02 for VolumeButton 3 with SE01 for BrightnessButton 4 with label ‘System version’Button 5 with label ‘Set VBC’Button 6 with label ‘Remove VBC’Note: See the position of buttons in picture below,The state of each button in Special window are displayed correctly as follows,Language = EnableVolume = EnableBrightness = EnableSystem version = EnableSet VBC = EnableRemove VBC = DisableSet clock = EnableThe other additional buttons from list above also display in this window (e.g. Maintenance, Brake, System Info, etc.).LayersThe level of layers in each area of window as follows,Layer 0: Area D, F, G, E10, E11, Y, and ZLayer -1: Area A1, (A2+A3)*, A4, B*, C1, (C2+C3+C4)*, C5, C6, C7, C8, C9, E1, E2, E3, E4, (E5-E9)*.Layer -2: Area B3, B4, B5, B6 and B7.Note: ‘*’ symbol is mean that specified area are drawn as one area.Packet transmissionUse the log file to confirm that DMI receives EVC-30 with following value in each bit of variable MMI_Q__REQUEST_ENABLE_64,Bit #13 = 1 (Language)Bit #14 = 1 (Volume)Bit #15 = 1 (Brightness)Bit #16 = 1 (System version)Bit #17 = 1 (Set VBC)Bit #18 = 0 (Remove VBC)Bit #25 or Bit #26 = 1 (Set Clock)And the buttons are enabled according to bit value = 1. General property of windowThe Settings window is presented with objects and buttons which is the one of several levels and allocated to areas of DMI..All objects, text messages and buttons are presented within the same layer.The Default window is not displayed and covered the current window.Sub-level window covers partially depending on the size of the Sub-Level window. There is no other window is displayed and activated at the same time.
            Test Step Comment: (1) MMI_gen 8465 (partly: MMI_gen 7909);(2) MMI_gen 8466; MMI_gen 4360 (partly: window title);(3) MMI_gen 8645 (partly: MMI_gen 4556 (partly: Close button, Window Title));    MMI_gen 8467 (partly: touch screen, button with label, Language, Volume, Brightness, System version, Set VBC, Remove VBC); MMI_gen 4392 (partly: [Close] NA11);                   (4) MMI_gen 11545 (partly: EVC-30, enabling #13, #14, #15, #16, #17, #25 or #26, disabling #18); (5) MMI_gen 8469; (6) MMI_gen 8645 (partly: MMI_gen 4630, MMI gen 5944 (partly: touch screen));(7) MMI_gen11545 (partly: enabling buttons, disabling ‘remove vbc’ button, EVC-30); MMI_gen 1088 (Partly, Bit #13 to #18 and #25 to #26)            (8) MMI_gen 4350;(9) MMI_gen 4351;(10) MMI_gen 4353;(11) MMI_gen 4354;
            */

            /*
            Test Step 2
            Action: Press and hold ‘Language’ button.
            Expected Result: Verify the following information,The sound ‘Click’ is played once.The ‘Language’ button is shown as pressed state, the border of button is removed.
            Test Step Comment: (1) MMI_gen 8465 (partly: MMI_gen 4557 (partly: Language button),  MMI_gen 4381 (partly: the sound for Up-Type button))); MMI_gen 9512; MMI_gen 968; (2) MMI_gen 8465 (partly: MMI_gen 4557 (partly: Language button) , MMI_gen 4381 (partly: change to state ‘Pressed’ as long as remain actuated))); MMI_gen 4375;
            */

            /*
            Test Step 3
            Action: Slide out of ‘Language’ button.
            Expected Result: The border of the button is shown (state ‘Enabled’) without a sound.
            Test Step Comment: (1) MMI_gen 8465 (partly: MMI_gen 4557 (partly: Language button, MMI_gen 4382 (partly: state ‘Enabled’ when slide out with force applied, no sound)));
            */

            /*
            Test Step 4
            Action: Slide back into ‘Language’ button.
            Expected Result: The button is back to state ‘Pressed’ without a sound.
            Test Step Comment: (1) MMI_gen 8465 (partly: MMI_gen 4557 (partly: Language button, MMI_gen 4382 (partly: state ‘Pressed’ when slide back, no sound)));  
            */

            /*
            Test Step 5
            Action: Release the ‘Language’ button.
            Expected Result: DMI displays Language window.
            Test Step Comment: MMI_gen 8465 (partly: MMI_gen 4557 (partly: Language button),  MMI_gen 4381 (partly: exit state ‘Pressed’, execute function associated to the button));  
            */

            /*
            Test Step 6
            Action: Press ‘Close’ button.
            Expected Result: DMI displays Settings window.
            */

            /*
            Test Step 7
            Action: Follow action step 2 – step 6 respectively for the following button.‘Volume’ button.‘Brightness’ button.‘System version’’ button.‘Set VBC’ button.‘Brake’ button.‘System info’ button.‘Set Clock’ button.‘Maintenance’ button.
            Expected Result: See the expected results of Step 2 – Step 5 and the following additional information,DMI displays corresponding window refer to released button from action step 5. When, released ‘System version’ button, use the log file to confirm that DMI sends out the packet [MMI_DRIVER_REQUEST (EVC-101)] with variable [MMI_DRIVER_REQUEST (EVC-101).MMI_M_REQUEST] = 55 (System version request).
            Test Step Comment: (1) MMI_gen 8465 (partly: MMI_gen 4557 (partly: Volume button, Brightness button, System version button, Set VBC button, Additional DMI technical functions));(2) MMI_gen 12057;
            */

            /*
            Test Step 8
            Action: Perform the following procedure,Enter and confirm the specify value VBC code = 65536Press ‘Yes’ button.
            Expected Result: DMI displays Set VBC data validation window.
            */

            /*
            Test Step 9
            Action: Press ‘Yes’ button. Then, confirm selected value by pressing an input field.
            Expected Result: DMI displays Settings window.Verify the following information,Use the log file to confirm that DMI receives EVC-30 with variable MMI_Q_REQUEST_ENABLE_64 (#18) = 1 and the ‘Remove VBC’ button is enabled.
            Test Step Comment: (1) MMI_gen 11545 (partly: enable #18, EVC-30)
            */

            /*
            Test Step 10
            Action: Follow action step 2 – step 6 respectively for ‘Remove VBC’ button.
            Expected Result: See the expected results of Step 2 – Step 5 and the following additional information,DMI displays Remove VBC window.
            Test Step Comment: (1) MMI_gen 8465 (partly: MMI_gen 4557 (partly: Remove VBC button));
            */

            /*
            Test Step 11
            Action: Press ‘Close’ button.
            Expected Result: DMI displays Settings window.
            */

            /*
            Test Step 12
            Action: Perform the following procedure,Drive the train forward until the brake is appliedStop driving the train
            Expected Result: Verify the following information,The following buttons are disabled,LanguageVolumeBrightnessSystem VersionSet VBCRemove VBCUse the log file to confirm that DMI receives EVC-30 with following value in each bit of variable MMI_Q_REQUEST_ENABLE_64Bit #13 = 0 (Language)Bit #14 = 0 (Volume)Bit #15 = 0 (Brightness)Bit #16 = 0 (System version)Bit #17 = 0 (Set VBC)Bit #18 = 0 (Remove VBC)
            Test Step Comment: (1) MMI_gen 11545 (partly: Disabling, #13, #14, #15, #16, #17, #18);(2) MMI_gen 11545 (partly: EVC-30, Disabling, #13, #14, #15, #16, #17, #18);
            */

            /*
            Test Step 13
            Action: Acknowledge the ‘Brake intervention’ symbol by pressing area E1.
            Expected Result: Verify the following information,The following buttons are disabled,LanguageVolumeBrightnessSystem VersionSet VBCRemove VBCUse the log file to confirm that DMI receives EVC-30 with following value in each bit of variable MMI_Q_REQUEST_ENABLE_64Bit #13 = 1 (Language)Bit #14 = 1 (Volume)Bit #15 = 1 (Brightness)Bit #16 = 1 (System version)Bit #17 = 1 (Set VBC)Bit #18 = 1 (Remove VBC)
            Test Step Comment: (1) MMI_gen 11545 (partly: Enabling, #13, #14, #15, #16, #17, #18);(2) MMI_gen 11545 (partly: EVC-30, Enabling, #13, #14, #15, #16, #17, #18);
            */

            /*
            Test Step 14
            Action: Use the test script file 22_21_a.xml to send EVC-30 with,MMI_Q_REQUEST_ENABLE_64 _#25 = 0MMI_Q_REQUEST_ENABLE_64 _#26 = 0
            Expected Result: The ‘Set Clock’ button is disabled.
            Test Step Comment: MMI_gen 11545 (partly: EVC-30, Disabling, #25, #26);
            */

            /*
            Test Step 15
            Action: Use the test script file 22_21_b.xml to send EVC-30 with,MMI_Q_REQUEST_ENABLE_64 _#25 = 1MMI_Q_REQUEST_ENABLE_64 _#26 = 0
            Expected Result: The ‘Set Clock’ button is enabled.
            Test Step Comment: MMI_gen 11545 (partly: EVC-30, Enabling, #25);
            */

            /*
            Test Step 16
            Action: Use the test script file 22_21_a.xml to send EVC-30 with,MMI_Q_REQUEST_ENABLE_64 _#25 = 0MMI_Q_REQUEST_ENABLE_64 _#26 = 0
            Expected Result: The ‘Set Clock’ button is disabled.
            */

            /*
            Test Step 17
            Action: Use the test script file 22_21_c.xml to send EVC-30 with,MMI_Q_REQUEST_ENABLE_64 _#25 = 1MMI_Q_REQUEST_ENABLE_64 _#26 = 1
            Expected Result: The ‘Set Clock’ button is enabled.
            Test Step Comment: MMI_gen 11545 (partly: EVC-30, Enabling, #25, #26);
            */

            /*
            Test Step 18
            Action: Use the test script file 22_21_a.xml to send EVC-30 with,MMI_Q_REQUEST_ENABLE_64 _#25 = 0MMI_Q_REQUEST_ENABLE_64 _#26 = 0
            Expected Result: The ‘Set Clock’ button is disabled.
            */

            /*
            Test Step 19
            Action: Use the test script file 22_21_d.xml to send EVC-30 with,MMI_Q_REQUEST_ENABLE_64 _#25 = 0MMI_Q_REQUEST_ENABLE_64 _#26 = 1
            Expected Result: The ‘Set Clock’ button is enabled.
            Test Step Comment: MMI_gen 11545 (partly: EVC-30, Enabling, #26);
            */

            /*
            Test Step 20
            Action: Follow action step 2 – step 6 respectively for ‘Close’ button.
            Expected Result: See the expected results of Step 2 – Step 6 and the following additional information,  (1)    DMI displays Default window.
            Test Step Comment: (1) MMI_gen 8465 (partly: MMI_gen 4557 (partly: Close button)); MMI_gen 4392 (partly:returning to the parent window);
            */

            /*
            Test Step 21
            Action: End of test.
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
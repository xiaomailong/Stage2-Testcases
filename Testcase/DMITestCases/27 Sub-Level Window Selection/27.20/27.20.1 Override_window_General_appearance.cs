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
    /// 27.20.1 Override window: General appearance
    /// TC-ID: 22.20.1
    /// 
    /// This test case verifies the display of the ‘Override’ window on DMI that shall comply with [ERA-ERTMS] standard and [MMI-ETCS-gen].
    /// 
    /// Tested Requirements:
    /// MMI_gen 8415 (partly: touch screen, label “EOA”); MMI_gen 11225; MMI_gen 8413 (partly: MMI_gen 7909, MMI_gen 4630, MMI gen 5944 (partly: touch screen), MMI_gen 4556 (partly: Close button, Window Title)); MMI_gen 8414; MMI_gen 11415 (partly: MMI_gen 11387 (partly: button Up-Type, send events of Pressed independently to ETCS), MMI_gen 4381, MMI_gen 4382); MMI_gen 9512; MMI_gen 968; MMI_gen 11907 (partly: EVC-101, timestamp), MMI_gen 11226; MMI_gen 11231; MMI_gen 4360 (partly: window title); MMI_gen 4355 (partly: Buttons, Close button); MMI_gen 4392 (partly: [Close] NA11, returning to the parent window); MMI_gen 3375; MMI_gen 4350; MMI_gen 4351; MMI_gen 4353; MMI_gen 4354;
    /// 
    /// Scenario:
    /// The concerned buttons in the ‘Override window are verified by the following actions:Press the button and holdSlide the button out with force appliedSlide the button back with force appliedRelease the buttonSoM is performed until Train Running number is confirmed and, then, the ‘Main’ window is closed.The disabled state of  ‘EOA’ button is verified.SoM is completed in mode SR, level 
    /// 1.Then, the ‘Main’ window is closed.The ‘Override’ window is opened and verified.The Safe-up-Type ‘EOA’ button is verified.Perform action for making DMI entered mode SB, level 
    /// 1.Then, the ‘EOA’ button is verified.
    /// 
    /// Used files:
    /// N/A
    /// </summary>
    public class TC_22_20_1_Override_window : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:

            // Call the TestCaseBase PreExecution
            base.PreExecution();

            // Test system is powered on.The cabin is activated.SoM is performed until Train Running number is confirmed.
            DmiActions.Start_ATP();
            DmiActions.Activate_Cabin_1(this);

            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.StandBy;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Level = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_LEVEL.L1;
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
            Action: Perform the following procedure,Press ‘Close’ button.Press ‘Override’ button
            Expected Result: Verify the following information,The ‘EOA’ button is in disable state.Use the log file to confirm that DMI receives EVC-30 with with bit No.9 of variable MMI_Q_REQUEST_ENABLE_64 = 0 (Disable Start Override EOA)
            Test Step Comment: (1) MMI_gen 8415 (partly: touch screen, label “EOA”);              MMI_gen 11225 (partly: EVC-30, disabled);(2) MMI_gen 11225 (partly: disalbed);
            */
            DmiActions.Set_Driver_ID(this, "1234");

            DmiActions.ShowInstruction(this, "Confirm the Driver ID");

            EVC16_CurrentTrainNumber.TrainRunningNumber = 1234;
            EVC16_CurrentTrainNumber.Send();

            DmiActions.ShowInstruction(this, "Confirm the Train running number");

            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.None;
            EVC30_MMIRequestEnable.Send();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Default; // Default window
            EVC30_MMIRequestEnable.Send();

            // The test says Close a window (presumably the Main window was displayed so closing it returns to the Default window
            // Just display the default window (which has the override button) so disabling options stops EOA being available

            DmiActions.ShowInstruction(this, "Press the ‘Override’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘EOA’ button is disabled");

            /*
            Test Step 2
            Action: Perform the following procedure,Press ‘Close’ button.Press ‘Main’ button
            Expected Result: DMI displays Main window
            */
            DmiActions.ShowInstruction(this, "Press the ‘Close’ button");

            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Main;
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH =
                EVC30_MMIRequestEnable.EnabledRequests.Start | Variables.standardFlags;
            EVC30_MMIRequestEnable.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Main window");

            /*
            Test Step 3
            Action: Press ‘Start’ button.Then, acknowledge ‘SR’ mode
            Expected Result: DMI displays Default window in SR mode, Level 1.Use the log file to confirm that DMI receives EVC-30 with with bit No.9 of variable MMI_Q_REQUEST_ENABLE_64 = 1 (Enable Start Override EOA)
            Test Step Comment: (1) MMI_gen 11225 (partly: EVC-30, enabled);
            */
            DmiActions.ShowInstruction(this, "Press the ‘Start’ button");

            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 263; // Ack SR
            EVC8_MMIDriverMessage.Send();

            DmiActions.ShowInstruction(this, "Acknowledge SR mode");

            // Remove symbol
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 4;
            EVC8_MMIDriverMessage.Send();

            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode =
                EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.StaffResponsible;

            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.EOA;
            EVC30_MMIRequestEnable.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Default window in SR mode, Level 1.");

            /*
            Test Step 4
            Action: Press ‘Override’ button
            Expected Result: DMI displays Override windowVerify the following points,Menu windowThe Override window is displayed in main area D/F/G. The window title is ‘Override’.The following objects are displayed in Main window, Enabled Close button (NA11).Window TitleButton 1 with label ‘EOA’Note: See the position of buttons in picture below,The ‘EOA’ button is in enable state.LayersThe level of layers in each area of window as follows,Layer 0: Area D, F, G, E10, E11, Y, and ZLayer -1: Area A1, (A2+A3)*, A4, B*, C1, (C2+C3+C4)*, C5, C6, C7, C8, C9, E1, E2, E3, E4, (E5-E9)*.Layer -2: Area B3, B4, B5, B6 and B7.Note: ‘*’ symbol is mean that specified areas are drawn as one area.General property of windowThe Override window is presented with objects and buttons which is the one of several levels and allocated to areas of DMI.All objects, text messages and buttons are presented within the same layer.The Default window is not displayed and covered the current window.Sub-level window covers partially depending on the size of the Sub-Level window. There is no other window is displayed and activated at the same time
            Test Step Comment: (1) MMI_gen 8413 (partly: MMI_gen 7909);    (2) MMI_gen 8414;  MMI_gen 4360 (partly: window title);(3) MMI_gen 8413 (partly: MMI_gen 4556 (partly: Close button, Window Title)); MMI_gen 8415 (partly: touch screen); MMI_gen 4392 (partly: [Close] NA11); MMI_gen 4355 (partly: Buttons, Close button);(4) MMI_gen 11225 (partly: enabled);(5) MMI_gen 8413 (partly: MMI_gen 4630, MMI gen 5944 (partly: touch screen));   (6) MMI_gen 4350;(7) MMI_gen 4351;(8) MMI_gen 4353;(9) MMI_gen 4354;
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Override’ button");

            WaitForVerification("Check the following (* indicates sub-areas drawn as one area):" + Environment.NewLine +
                                Environment.NewLine +
                                @"1. DMI displays the Override window with 3 layers, with the title ‘Override’." +
                                Environment.NewLine +
                                "2. The Override window is displayed in areas D, F, G." + Environment.NewLine +
                                "3. Layer 0 comprises areas D, F, G, E10, E11, Y and Z." + Environment.NewLine +
                                "4. Layer 1 comprises areas A1, (A2+A3)*, A4, B, C1, (C2+C3+c4)*, C5, C6, C7, C8, C9, E1, E2, E3, E4, (E5-E9)*." +
                                Environment.NewLine +
                                "5. Layer 2 comprises areas B3, B4, B5, B6 and B7." + Environment.NewLine +
                                @"6. The Override window displays an enabled button labelled ‘EOA’ and an ‘Enabled Close’ button (symbol NA11)." +
                                Environment.NewLine +
                                "7. Objects, text messages and buttons can be displayed in several levels. Within a level they are allocated to areas." +
                                Environment.NewLine +
                                "8. Objects, text messages and buttons in a layer form a window." +
                                Environment.NewLine +
                                "9. The Default window does not cover the current window." + Environment.NewLine +
                                "10. A sub-level window can partially cover another window, depending on its size.Another window cannot be displayed and activated at the same time.");

            /*
            Test Step 5
            Action: Press and hold ‘EOA’ button
            Expected Result: DMI displays the Override window.The sound ‘Click’ is played once.The ‘EOA’ button is shown as the ‘Pressed’ state, the border of button is removed. Use the log file to confirm that DMI sends EVC-101 with variable MMI_M_REQUEST = 7 (Start Override EOA (Pass stop) and MMI_T_BUTTONEVENT is not blank
            Test Step Comment: (1) MMI_gen 11415 (partly: MMI_gen 11387 (partly: button Up-Type, MMI_gen 4381 (partly: the sound for Up-Type button))), MMI_gen 9512, MMI_gen 968;(2) MMI_gen 11415 (partly: MMI_gen 11387 (partly: button Up-Type, MMI_gen 4381 (partly: change to state ‘Pressed’ as long as remain actuated))); MMI_gen 4375;(3) MMI_gen 11415 (partly: MMI_gen 11387 (partly: send events of Pressed independently to ETCS), MMI_gen 11907 (partly: EVC-101, timestamp)), MMI_gen 11226 (partly: EVC-101); MMI_gen 3375;
            */
            DmiActions.ShowInstruction(this, @"Press and hold the ‘EOA’ button");

            EVC101_MMIDriverRequest.CheckMRequestReleased = Variables.MMI_M_REQUEST.StartOverrideEOA;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The data input field is displayed pressed, without a border." +
                                Environment.NewLine +
                                "2. The ‘Click’ sound is played once.");

            /*
            Test Step 6
            Action: Slide out of ‘EOA’ button
            Expected Result: The border of the button is shown (state ‘Enabled’) without a sound
            Test Step Comment: MMI_gen 11415 (partly: MMI_gen 11387 (partly: button Up-Type, MMI_gen 4382 (partly: state ‘Enabled’ when slide out with force applied, no sound))); MMI_gen 4374;
            */
            DmiActions.ShowInstruction(this, @"Whilst keeping the ‘EOA’ button pressed, drag it out of its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘EOA’ button is displayed enabled, with a border." + Environment.NewLine +
                                "2. No sound is played.");

            /*
            Test Step 7
            Action: Slide back into ‘EOA’ button
            Expected Result: The button is back to state ‘Pressed’ without a sound
            Test Step Comment: MMI_gen 11415 (partly: MMI_gen 11387 (partly: button Up-Type, MMI_gen 4382 (partly: state ‘Pressed’ when slide back, no sound))); MMI_gen 4375;
            */
            DmiActions.ShowInstruction(this, @"Whilst keeping the ‘EOA’ button pressed, drag it back inside its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘EOA’ button is displayed pressed, without a border." + Environment.NewLine +
                                "2.No sound is played.");

            /*
            Test Step 8
            Action: Release ‘EOA’ button
            Expected Result: Verify the following information,DMI displays the ‘Default’ window.Use the log file to confirm that DMI sends EVC-101 with variable MMI_M_REQUEST = 7 (Start Override EOA (Pass stop)) and MMI_T_BUTTONEVENT is not blank.Use the log file to confirm that DMI receives EVC-2 with variable MMI_M_OVERRIDE_EOA = 1 (function is active)  and DMI displays symbol ‘Override’ MO03 in sub-area C7
            Test Step Comment: (1) MMI_gen 11415 (partly: MMI_gen 11387 (partly: button Up-Type, MMI_gen 4381 (partly: exit state ‘Pressed’, execute function associated to the button))), MMI_gen 11226 (partly: closure, parent window);(2) MMI_gen 11415 (partly: MMI_gen 11387 (partly: send events of Released independently to ETCS), MMI_gen 11907 (partly: EVC-101, timestamp)), MMI_gen 11226 (partly: EVC-101); MMI_gen 3375;(3) MMI_gen 11231;
            */
            DmiActions.ShowInstruction(this, @"Release the ‘EOA’ button");

            EVC101_MMIDriverRequest.CheckMRequestReleased = Variables.MMI_M_REQUEST.StartOverrideEOA;
            EVC2_MMIStatus.MMI_M_OVERRIDE_EOA = true;
            EVC2_MMIStatus.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Default window" + Environment.NewLine +
                                "2. DMI displays the ‘Override’ symbol (MO03) in sub-area C7.");

            /*
            Test Step 9
            Action: Perform the following procedure, Press ‘Main’ buttonPress and hold ‘Shunting’ button up to 2 secondRelease ‘Shunting’ button
            Expected Result: DMI displays Default window in SH mode, Level 1
            */
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.None;
            EVC30_MMIRequestEnable.Send();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Main;
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.DriverID |
                                                               EVC30_MMIRequestEnable.EnabledRequests.Shunting |
                                                               EVC30_MMIRequestEnable.EnabledRequests.MaintainShunting |
                                                               EVC30_MMIRequestEnable.EnabledRequests.ExitShunting;
            EVC30_MMIRequestEnable.Send();

            DmiActions.ShowInstruction(this,
                @"Press the ‘Main’ button. In the main window press and hold the ‘Shunting’ button for 2 second, then release the button");

            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.Shunting;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Default window in SH mode, Level 1.");

            /*
            Test Step 10
            Action: Perform the following procedure,Press ‘Main’ buttonPress and hold ‘Exit Shunting’ button up to 2 secondRelease ‘Exit Shunting’ buttonEnter Driver IDClose the ‘Main’ window
            Expected Result: DMI displays Default window in SB mode, Level 1
            */
            DmiActions.ShowInstruction(this,
                @"Press the ‘Main’ button. In the main window press and hold the ‘Exit Shunting’ button for 2 second, then release the button." +
                "Enter the Driver ID, then close the Main window");

            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.StandBy;
            EVC2_MMIStatus.MMI_M_OVERRIDE_EOA = false; // Remove the override symbol in the default window
            EVC2_MMIStatus.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Default window in SB mode, Level 1.");

            /*
            Test Step 11
            Action: Press ‘Override’ button
            Expected Result: Verify the following information,The ‘EOA’ button is in disable state.Use the log file to confirm that DMI receives EVC-30 with with bit No.9 of variable MMI_Q_REQUEST_ENABLE_64 = 0 (Disable Start Override EOA)
            Test Step Comment: (1) MMI_gen 8415 (partly: touch screen, label “EOA”);              MMI_gen 11225 (partly: EVC-30, disabled);(2) MMI_gen 11225 (partly: disabled);
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Override’ button");

            // Make sure that the EOA button is disabled
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.None;
            EVC30_MMIRequestEnable.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Override window with the ‘EOA’ button displayed disabled.");

            /*
            Test Step 12
            Action: Press ‘Close’ button
            Expected Result: Verify the following information,(1)   DMI displays Default window
            Test Step Comment: (1) MMI_gen 4392 (partly: returning to the parent window);
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Default window.");

            /*
            Test Step 13
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
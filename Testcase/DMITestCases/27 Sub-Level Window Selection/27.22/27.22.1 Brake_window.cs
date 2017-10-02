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


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 27.22.1 Brake window
    /// TC-ID: 22.22.1
    /// 
    /// This test case verifies the display of the ‘Brake’ menu window and its buttons that shall comply with [ERA-ERTMS] standard and [MMI-ETCS-gen].
    /// 
    /// Tested Requirements:
    /// MMI_gen 11804; MMI_gen 11805; MMI_gen 11818; MMI_gen 11817; MMI_gen 11938; MMI_gen 11819; MMI_gen 11820; MMI_gen 11816 (partly: MMI_gen 4556 (partly: Button 1, Button2, Close, Window Title), MMI_gen 7909, MMI_gen 4557, MMI_gen 4381, MMI_gen 4382, MMI_gen 9512, MMI_gen 968, MMI_gen 4630, MMI gen 5944 (partly: touch screen)); MMI_gen 11810; MMI_gen 4360 (partly: window title); MMI_gen 4375; MMI_gen 4374; MMI_gen 4392 (partly: [Close] NA11, returning to the parent window); MMI_gen 4350; MMI_gen 4351; MMI_gen 4353;
    /// 
    /// Scenario:
    /// The concerned buttons in the ‘Brake’ window are verified by the following actions:Press the button and holdSlide the button out with force appliedSlide the button back with force appliedRelease the buttonUse different script files to force different situations of sending of EVC-
    /// 30.Then, verify the effect on ‘Brake’, ‘Test’ and ‘Percentage’ button.The Brake window appearance is verified.The Up-Type button of ‘Test’, ‘Percentage’ and ‘Close’ buttons are verified.
    /// 
    /// Used files:
    /// 22_22_1_a.xml, 22_22_1_b.xml, 22_22_1_c.xml, 22_22_1_d.xml
    /// </summary>
    public class TC_ID_22_22_1_Brake_window : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:

            // Call the TestCaseBase PreExecution
            base.PreExecution();

            // Test system is powered on.Cabin is activatedSettings window is opened.
            DmiActions.Start_ATP();
            DmiActions.Activate_Cabin_1(this);

            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.StandBy;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Level = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_LEVEL.L1;
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = 4;      // Settings window: no buttons enabled
            EVC30_MMIRequestEnable.Send();
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
            Action: Use the test script file 22_22_1_a.xml to send EVC-30 with,MMI_NID_WINDOW = 4MMI_Q_REQUEST_ENABLE_64 (#31) = 0MMI_Q_REQUEST_ENABLE_64 (#28) = 0
            Expected Result: Verify the following information,Use the log file to verify that DMI receives the EVC-30 with [MMI_ENABLE_REQUEST (EVC-30).MMI_Q_REQUEST_ENABLE_64] (#31) = 0 (Disable Brake Percentage)Use the log file to verify that DMI receives the EVC-30 with [MMI_ENABLE_REQUEST (EVC-30).MMI_Q_REQUEST_ENABLE_64] (#28) = 0 (Disable to Start Brake Test)The ‘Brake button is disabled
            Test Step Comment: (1) MMI_gen 11938 (partly: disable);(2) MMI_gen 11938 (partly: disable, MMI_gen 11810);                                     MMI_gen 11810 (partly: disable);(3) MMI_gen 11805 (partly: disable);
            */
            XML.XML_22_21_1_a.Send(this);
            
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Brake’ button is displayed disabled.");

            /*
            Test Step 2
            Action: Use the test script file 22_22_1_b.xml to send EVC-30 with,MMI_NID_WINDOW = 4MMI_Q_REQUEST_ENABLE_64 (#31) = 1MMI_Q_REQUEST_ENABLE_64 (#28) = 0
            Expected Result: Verify the following information,Use the log file to verify that DMI receives the EVC-30 with [MMI_ENABLE_REQUEST (EVC-30).MMI_Q_REQUEST_ENABLE_64] (#31) = 1 (Enable Brake Percentage)Use the log file to verify that DMI receives the EVC-30 with [MMI_ENABLE_REQUEST (EVC-30).MMI_Q_REQUEST_ENABLE_64] (#28) = 0 (Disable to Start Brake Test)The ‘Brake’ button is enabled
            Test Step Comment: (1) MMI_gen 11938 (partly: enable);(2) MMI_gen 11938 (partly: disable, MMI_gen 11810);                                     MMI_gen 11810 (partly: disable);(3) MMI_gen 11805 (partly: disable);
            */
            XML.XML_22_21_1_b.Send(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Brake’ button is displayed enabled.");

            /*
            Test Step 3
            Action: Press ‘Brake’ button
            Expected Result: Verify the following information,The ‘Test’ button is disabled.The ‘Percentage’ button is enabled
            Test Step Comment: (1) MMI_gen 11938 (partly: disable, MMI_gen 11810);                                     MMI_gen 11810 (partly: disable);(2) MMI_gen 11938 (partly: enable);
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Brake’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Test’ button is displayed disabled." + Environment.NewLine +
                                "2. The ‘Percentage’ button is displayed enabled.");

            /*
            Test Step 4
            Action: Press ‘Close’ button
            Expected Result: DMI displays Settings button
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button");

            // Test says Settings button. Doh!
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Settings window.");

            /*
            Test Step 5
            Action: Use the test script file 22_22_1_a.xml to send EVC-30 with,MMI_NID_WINDOW = 4MMI_Q_REQUEST_ENABLE_64 (#31) = 0MMI_Q_REQUEST_ENABLE_64 (#28) = 0
            Expected Result: The ‘Brake’ button is disabled
            */
            XML.XML_22_21_1_a.Send(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Brake’ button is displayed disabled.");

            /*
            Test Step 6
            Action: Use the test script file 22_22_1_c.xml to send EVC-30 with,MMI_NID_WINDOW = 4MMI_Q_REQUEST_ENABLE_64 (#31) = 0MMI_Q_REQUEST_ENABLE_64 (#28) = 1
            Expected Result: Verify the following information,Use the log file to verify that DMI receives the EVC-30 with [MMI_ENABLE_REQUEST (EVC-30).MMI_Q_REQUEST_ENABLE_64] (#31) = 0 (Disable Brake Percentage)Use the log file to verify that DMI receives the EVC-30 with [MMI_ENABLE_REQUEST (EVC-30).MMI_Q_REQUEST_ENABLE_64] (#28) = 1 (Enable to Start Brake Test)The ‘Brake’ button is enabled
            Test Step Comment: (1) MMI_gen 11938 (partly: disable);(2) MMI_gen 11938 (partly: enable, MMI_gen 11810);                                     MMI_gen 11810 (partly: enable);(3) MMI_gen 11805 (partly: enable);
            */
            XML.XML_22_21_1_c.Send(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Brake’ button is displayed enabled.");
            
            /*
            Test Step 7
            Action: Press ‘Brake’ button
            Expected Result: Verify the following information,The ‘Test’ button is enabled.The ‘Percentage’ button is disabled
            Test Step Comment: (1) MMI_gen 11938 (partly: enable, MMI_gen 11810);                                     MMI_gen 11810 (partly: enable);(2) MMI_gen 11938 (partly: disable);
            */
            DmiActions.ShowInstruction(this, @"Press ‘Brake’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Test’ button is displayed enabled." + Environment.NewLine +
                                "2. The ‘Percentage’ button is displayed disbled.");

            /*
            Test Step 8
            Action: Press ‘Close’ button
            Expected Result: DMI displays Settings button
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button");

            // Test says Settings button. Doh!
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Settings window.");

            /*
            Test Step 9
            Action: Use the test script file 22_22_1_a.xml to send EVC-30 with,MMI_NID_WINDOW = 4MMI_Q_REQUEST_ENABLE_64 (#31) = 0MMI_Q_REQUEST_ENABLE_64 (#28) = 0
            Expected Result: The ‘Brake’ button is disabled
            */
            XML.XML_22_21_1_a.Send(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Brake’ button is displayed disabled.");

            /*
            Test Step 10
            Action: Use the test script file 22_22_1_d.xml to send EVC-30 with,MMI_NID_WINDOW = 4MMI_Q_REQUEST_ENABLE_64 (#31) = 1MMI_Q_REQUEST_ENABLE_64 (#28) = 1
            Expected Result: Verify the following information,Use the log file to verify that DMI receives the EVC-30 with [MMI_ENABLE_REQUEST (EVC-30).MMI_Q_REQUEST_ENABLE_64] (#31) = 1 (Enable Brake Percentage)Use the log file to verify that DMI receives the EVC-30 with [MMI_ENABLE_REQUEST (EVC-30).MMI_Q_REQUEST_ENABLE_64] (#28) = 1 (Enable to Start Brake Test)The ‘Brake’ button is enabled
            Test Step Comment: (1) MMI_gen 11938 (partly: enable);(2) MMI_gen 11938 (partly: enable, MMI_gen 11810);                                     MMI_gen 11810 (partly: enable);(3) MMI_gen 11805 (partly: enable);
            */
            XML.XML_22_21_1_d.Send(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Brake’ button is displayed enabled.");

            /*
            Test Step 11
            Action: Press ‘Brake’ button
            Expected Result: Verify the following points,Menu windowDMI displays Brake window.The window title is ‘Brake’.The Brake window is displayed in main area D/F/G.The following objects are display in Brake window. Enabled Close button (NA11)Window TitleButton 1 with label ‘Test’Button 2 with label ‘PercentageNote: See the position of buttons in picture below,The state of each button in Brake window are displayed correctly as follows,Test = EnablePercentage = EnableLayersThe level of layers in each area of window as follows,Layer 0: Area D, F, G, E10, E11, Y, and ZLayer -1: Area A1, (A2+A3)*, A4, B*, C1, (C2+C3+C4)*, C5, C6, C7, C8, C9, E1, E2, E3, E4, (E5-E9)*.Layer -2: Area B3, B4, B5, B6 and B7.Note: ‘*’ symbol is mean that specified area are drawn as one area.General property of windowThe Brake window is presented with objects and buttons which is the one of several levels and allocated to areas of DMI. All objects, text messages and buttons are presented within the same layer.The Default window is not displayed and covered the current window
            Test Step Comment: (1) MMI_gen 11804;(2) MMI_gen 11818; MMI_gen 4360 (partly: window title);(3) MMI_gen 11816 (partly: MMI_gen 7909);(4) MMI_gen 11816 (MMI_gen 4556 (partly: Close button, Window Title, Button 1, Button2); MMI_gen 11817; MMI_gen 4392 (partly: [Close] NA11);         (5) MMI_gen 11938 (partly: enable, MMI_gen 11810);                                     MMI_gen 11810 (partly: enable); (6) MMI_gen 11816 (partly: MMI_gen 4630, MMI gen 5944 (partly: touch screen));(7) MMI_gen 4350;(8) MMI_gen 4351;(9) MMI_gen 4353;
            */
            DmiActions.ShowInstruction(this, @"Press ‘Brake’ button");

            WaitForVerification("Check the following (* indicates sub-areas drawn as one area):" + Environment.NewLine + Environment.NewLine +
                                @"1. DMI displays the Brake window with 3 layers in a half-grid array with the title ‘Brake’." + Environment.NewLine +
                                "2. The Brake window is displayed in areas D, F and G." + Environment.NewLine +
                                "3. Layer 0 comprises areas D, F, G, Y and Z." + Environment.NewLine +
                                "4. Layer 1 comprises areas A1, (A2+A3)*, A4, B, C1, (C2+C3+c4)*, C5, C6, C7, C8, C9, E1, E2, E3, E4, (E5-E9)*." + Environment.NewLine +
                                "5. Layer 2 comprises areas B3, B4, B5, B6 and B7." + Environment.NewLine +
                                @"6. The Brake window displays two buttons in areas D, F and G and an ‘Enabled Close’ button (symbol NA11)." + Environment.NewLine +
                                "7. The two buttons are displayed enabled and are labelled ‘Test’ and ‘Percentage’, respectively." + Environment.NewLine +
                                "8. Objects, text messages and buttons can be displayed in several levels. Within a level they are allocated to areas." + Environment.NewLine +
                                "9. Objects, text messages and buttons in a layer form a window." + Environment.NewLine +
                                "10. The Default window does not cover the current window.");

            /*
            Test Step 12
            Action: Press and hold ‘Test’ button
            Expected Result: Verify the following information,The sound ‘Click’ played once.The ‘Test’ button is shown as pressed state, the border of button is removed
            Test Step Comment: (1) MMI_gen 11816 (partly: MMI_gen 4557 (partly: button ‘Test), MMI_gen 4381 (partly: the sound for Up-Type button)); MMI_gen 9512; MMI_gen 968;(2) MMI_gen 11816 (partly: MMI_gen 4557 (partly: button ‘Test’), MMI_gen 4381 (partly: change to state ‘Pressed’ as long as remain actuated)); MMI_gen 4375;
            */
            DmiActions.ShowInstruction(this, @"Press in and hold the ‘Test’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Test’ button is displayed pressed, without a border." + Environment.NewLine +
                                "2. The ‘Click’ sound is played once.");

            /*
            Test Step 13
            Action: Slide out of ‘Test’ button
            Expected Result: The border of the button is shown (state ‘Enabled’) without a sound
            Test Step Comment: MMI_gen 11816 (partly: MMI_gen 4557 (partly: button ‘Test’, MMI_gen 4382 (partly: state ‘Enabled’ when slide out with force applied, no sound))); MMI_gen 4374;
            */
            DmiActions.ShowInstruction(this, @"Whilst keeping the ‘Test’ button pressed, drag it out of its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘Test’ button is displayed enabled, with a border." + Environment.NewLine +
                                "2. No sound is played.");


            /*
            Test Step 14
            Action: Slide back into ‘Test’ button
            Expected Result: The button is back to state ‘Pressed’ without a sound
            Test Step Comment: MMI_gen 11816 (partly: MMI_gen 4557 (partly: button ‘Test’, MMI_gen 4382 (partly: state ‘Pressed’ when slide back, no sound))); MMI_gen 4375;
            */
            DmiActions.ShowInstruction(this, @"Whilst keeping the ‘Test’ button pressed, drag it back inside its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘Test’ button is displayed pressed." + Environment.NewLine +
                                "2. No sound is played.");

            /*
            Test Step 15
            Action: Release the ‘Test’ button
            Expected Result: DMI displays Brake Test window
            Test Step Comment: MMI_gen 11819;   MMI_gen 11816 (partly: MMI_gen 4557 (partly: button ‘Test’, MMI_gen 4381 (partly: exit state ‘Pressed’, execute function associated to the button)));
            */
            DmiActions.ShowInstruction(this, @"Release the ‘Test’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. DMI displays the Brake test window");

            /*
            Test Step 16
            Action: Press ‘Close’ button
            Expected Result: DMI displays Brake window
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button");

            WaitForVerification("Check the following (* indicates sub-areas drawn as one area):" + Environment.NewLine + Environment.NewLine +
                                @"1. DMI displays the Brake window");


            /*
            Test Step 17
            Action: Follow action step 12 – step 16 for ‘Percentage’ button
            Expected Result: See the expected results of Step 12 – Step 16 and the following additional information,DMI displays Brake Percentage window when button is released refer to action step 15.Use the log file to confirm that DMI sends out the packet [MMI_DRIVER_REQUEST (EVC-101)] with variable [MMI_DRIVER_REQUEST (EVC-101).MMI_M_REQUEST] = 51 (Change Brake Percentage)
            Test Step Comment: (1) MMI_gen 11816 (partly: MMI_gen 4557 (partly: button ‘Percentage’));(2) MMI_gen 11820;
            */
            // Repeat Step 12 for the ‘Percentage’ button
            DmiActions.ShowInstruction(this, @"Press in and hold the ‘Percentage’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Percentage’ button is displayed pressed, without a border." + Environment.NewLine +
                                "2. The ‘Click’ sound is played once.");

            // Repeat Step 13 for the ‘Percentage’ button
            DmiActions.ShowInstruction(this, @"Whilst keeping the ‘Percentage’ button pressed, drag it out of its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘Percentage’ button is displayed enabled, with a border." + Environment.NewLine +
                                "2. No sound is played.");

            // Repeat Step 14 for the ‘Percentage’ button
            DmiActions.ShowInstruction(this, @"Whilst keeping the ‘Percentage’ button pressed, drag it back inside its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘Percentage’ button is displayed pressed." + Environment.NewLine +
                                "2. No sound is played.");

            // Repeat Step 15 for the ‘Percentage’ button
            DmiActions.ShowInstruction(this, @"Release the ‘Percentage’ button");

            Telegrams.DMItoEVC.EVC101_MMIDriverRequest.CheckMRequestReleased = Variables.MMI_M_REQUEST.ChangeBrakePercentage;
           
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. DMI displays the Brake percentage window");

            /*
            Test Step 18
            Action: Follow action step 12 – step 16 for ‘Close’ button
            Expected Result: See the expected results of Step 12 – Step 16 and the following additional information,DMI displays Settings window
            Test Step Comment: (1) MMI_gen 11816 (partly: MMI_gen 4557 (partly: button ‘Close’)); MMI_gen 4392 (partly: returning to the parent window);
            */
            // Repeat Step 12 for the ‘Close’ button
            DmiActions.ShowInstruction(this, @"Press in and hold the ‘Close’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Close’ button is displayed pressed, without a border." + Environment.NewLine +
                                "2. The ‘Click’ sound is played once.");

            // Repeat Step 13 for the ‘Close’ button
            DmiActions.ShowInstruction(this, @"Whilst keeping the ‘Close’ button pressed, drag it out of its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘Close’ button is displayed enabled, with a border." + Environment.NewLine +
                                "2. No sound is played.");

            // Repeat Step 14 for the ‘Close’ button
            DmiActions.ShowInstruction(this, @"Whilst keeping the ‘Close’ button pressed, drag it back inside its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘Close’ button is displayed pressed." + Environment.NewLine +
                                "2. No sound is played.");

            // Repeat Step 15 for the ‘Close’ button
            DmiActions.ShowInstruction(this, @"Release the ‘Close’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. DMI displays the Settings window");
            
            /*
            Test Step 19
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
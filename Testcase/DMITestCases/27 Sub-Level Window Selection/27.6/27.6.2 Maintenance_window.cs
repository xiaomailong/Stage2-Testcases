using System;
using Testcase.Telegrams.DMItoEVC;
using Testcase.Telegrams.EVCtoDMI;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 27.6.2 Maintenance window
    /// TC-ID: 22.6.2
    /// 
    /// This test case verifies a display of Maintenance window which related to ‘Wheel diameter’ and ‘Radar’ button.
    /// 
    /// Tested Requirements:
    /// MMI_gen 11724; MMI_gen 11743 (partly: MMI_gen 7909, MMI_gen 4556, MMI_gen 4557, MMI_gen 4381, MMI_gen 4382, MMI_gen 4630 (partly: MMI gen 5944 (partly: touch screen))); MMI_gen 11744; MMI_gen 11745; MMI_gen 11746; MMI_gen 11747; MMI_gen 968; MMI_gen 9512; MMI_gen 4360 (partly: window title); MMI_gen 4392 (partly: [Close] NA11, returning to the parent window); MMI_gen 4350; MMI_gen 4351; MMI_gen 4353;
    /// 
    /// Scenario:
    /// The concerned buttons in the ‘Maintenance’ window are verified by the following actions:Press the button and holdSlide the button out with force appliedSlide the button back with force appliedRelease the buttonUse different script files to force different situations of sending of EVC-
    /// 30.Then, verify the effect on ‘Maintenance’, ‘Wheel diameter’ and ‘Radar’ button.The Maintenance window appearance is verified.The Up-Type button of ‘Wheel diameter’, ‘Radar’ and ‘Close’ buttons are verified.
    /// 
    /// Used files:
    /// 22_6_2_a.xml, 22_6_2_b.xml, 22_6_2_c.xml, 22_6_2_d.xml
    /// </summary>
    public class TC_ID_22_6_2_Maintenance_window : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // Test system is powered onCabin is active‘Settings’ button is pressed after cabin activation.

            // Call the TestCaseBase PreExecution
            base.PreExecution();
            DmiActions.Activate_Cabin_1(this);
        }

        public override bool TestcaseEntryPoint()
        {
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 0;
            // Testcase entrypoint


            TraceHeader("Test Step 1");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Use test script file 22_6_2_a.xml to disable wheel diameter and doppler by sending EVC-30 with,MMI_NID_WINDOW = 4MMI_Q_REQUEST_ENABLE_64 (#29) = 0MMI_Q_REQUEST_ENABLE_64 (#30) = 0");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following information,DMI received the EVC-30 with [MMI_ENABLE_REQUEST (EVC-30).MMI_Q_REQUEST_ENABLE_64] (#29) = 0 in order to disable wheel diameter.DMI received the EVC-30 with [MMI_ENABLE_REQUEST (EVC-30).MMI_Q_REQUEST_ENABLE_64] (#30) = 0 in order to disable doppler.The ‘Maintenance’ button is disabled");
            /*
            Test Step 1
            Action: Use test script file 22_6_2_a.xml to disable wheel diameter and doppler by sending EVC-30 with,MMI_NID_WINDOW = 4MMI_Q_REQUEST_ENABLE_64 (#29) = 0MMI_Q_REQUEST_ENABLE_64 (#30) = 0
            Expected Result: Verify the following information,DMI received the EVC-30 with [MMI_ENABLE_REQUEST (EVC-30).MMI_Q_REQUEST_ENABLE_64] (#29) = 0 in order to disable wheel diameter.DMI received the EVC-30 with [MMI_ENABLE_REQUEST (EVC-30).MMI_Q_REQUEST_ENABLE_64] (#30) = 0 in order to disable doppler.The ‘Maintenance’ button is disabled
            Test Step Comment: (1) MMI_gen 11746 (partly: disable wheel diameter);(2) MMI_gen 11746 (partly: disable doppler);(3) MMI_gen 11724;
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Settings’ button");

            XML_22_6_2(msgType.typea);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Maintenance’ button is disabled");

            TraceHeader("Test Step 2");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Use test script file 22_6_2_b.xml to enable wheel diameter by sending EVC-30 with,MMI_NID_WINDOW = 4MMI_Q_REQUEST_ENABLE_64 (#29) = 1MMI_Q_REQUEST_ENABLE_64 (#30) = 0");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following information, DMI received the EVC-30 with [MMI_ENABLE_REQUEST (EVC-30).MMI_Q_REQUEST_ENABLE_64] (#29) = 1 in order to enable wheel diameter.DMI received the EVC-30 with [MMI_ENABLE_REQUEST (EVC-30).MMI_Q_REQUEST_ENABLE_64] (#30) = 0 in order to disable doppler.The ‘Maintenance’ button is enabled");
            /*
            Test Step 2
            Action: Use test script file 22_6_2_b.xml to enable wheel diameter by sending EVC-30 with,MMI_NID_WINDOW = 4MMI_Q_REQUEST_ENABLE_64 (#29) = 1MMI_Q_REQUEST_ENABLE_64 (#30) = 0
            Expected Result: Verify the following information, DMI received the EVC-30 with [MMI_ENABLE_REQUEST (EVC-30).MMI_Q_REQUEST_ENABLE_64] (#29) = 1 in order to enable wheel diameter.DMI received the EVC-30 with [MMI_ENABLE_REQUEST (EVC-30).MMI_Q_REQUEST_ENABLE_64] (#30) = 0 in order to disable doppler.The ‘Maintenance’ button is enabled
            Test Step Comment: (1) MMI_gen 11746 (partly: enable wheel diameter);(2) MMI_gen 11746 (partly: disable doppler);(3) MMI_gen 11724;
            */
            XML_22_6_2(msgType.typeb);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Maintenance’ button is enabled");

            TraceHeader("Test Step 3");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Perform the following procedure,Press ‘Maintenance’ button.Enter the Maintenance window by entering the password same as a value in tag ‘PASS_CODE_MTN’ of the configuration file and confirming the password");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following information,The Wheel diameter button is enabled.The Radar button is disabled");
            /*
            Test Step 3
            Action: Perform the following procedure,Press ‘Maintenance’ button.Enter the Maintenance window by entering the password same as a value in tag ‘PASS_CODE_MTN’ of the configuration file and confirming the password
            Expected Result: Verify the following information,The Wheel diameter button is enabled.The Radar button is disabled
            Test Step Comment: (1) MMI_gen 11746 (partly: enable wheel diameter);(2) MMI_gen 11746 (partly: disable doppler);
            */
            DmiActions.ShowInstruction(this,
                @"Press the ‘Maintenance’ button, enter the password from the PASS_CODE_MTN tag in the configuration and confirm the password");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Wheel diameter’ button is enabled" + Environment.NewLine +
                                "2. The ‘Radar’ button is disabled");

            TraceHeader("Test Step 4");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Perform the following procedure,Return to the Setting window by pressing Close’ button.Use test script file 22_6_2_a.xml to disable wheel diameter and doppler by sending EVC-30 with,MMI_NID_WINDOW = 4MMI_Q_REQUEST_ENABLE_64 (#29) = 0MMI_Q_REQUEST_ENABLE_64 (#30) = 0");
            TraceReport("Expected Result");
            TraceInfo("The ‘Maintenance’ button is disabled");
            /*
            Test Step 4
            Action: Perform the following procedure,Return to the Setting window by pressing Close’ button.Use test script file 22_6_2_a.xml to disable wheel diameter and doppler by sending EVC-30 with,MMI_NID_WINDOW = 4MMI_Q_REQUEST_ENABLE_64 (#29) = 0MMI_Q_REQUEST_ENABLE_64 (#30) = 0
            Expected Result: The ‘Maintenance’ button is disabled
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button to return to the Settings window");

            XML_22_6_2(msgType.typea);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Maintenance’ button is disabled");

            TraceHeader("Test Step 5");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Use test script file 22_6_2_c.xml to enable doppler by sendingSend EVC-30 with,MMI_NID_WINDOW = 4MMI_Q_REQUEST_ENABLE_64 (#29) = 0MMI_Q_REQUEST_ENABLE_64 (#30) = 1");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following information,DMI received the EVC-30 with [MMI_ENABLE_REQUEST (EVC-30).MMI_Q_REQUEST_ENABLE_64] (#29) = 0 in order to disable wheel diameter.DMI received the EVC-30 with [MMI_ENABLE_REQUEST (EVC-30).MMI_Q_REQUEST_ENABLE_64] (#30) = 1 in order to enable doppler.The ‘Maintenance’ button is enabled");
            /*
            Test Step 5
            Action: Use test script file 22_6_2_c.xml to enable doppler by sendingSend EVC-30 with,MMI_NID_WINDOW = 4MMI_Q_REQUEST_ENABLE_64 (#29) = 0MMI_Q_REQUEST_ENABLE_64 (#30) = 1
            Expected Result: Verify the following information,DMI received the EVC-30 with [MMI_ENABLE_REQUEST (EVC-30).MMI_Q_REQUEST_ENABLE_64] (#29) = 0 in order to disable wheel diameter.DMI received the EVC-30 with [MMI_ENABLE_REQUEST (EVC-30).MMI_Q_REQUEST_ENABLE_64] (#30) = 1 in order to enable doppler.The ‘Maintenance’ button is enabled
            Test Step Comment: (1) MMI_gen 11746 (partly: disable wheel diameter);(2) MMI_gen 11746 (partly: enable doppler);(3) MMI_gen 11724;
            */
            XML_22_6_2(msgType.typec);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Maintenance’ button is enabled");

            TraceHeader("Test Step 6");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Perform the following procedure,Press ‘Maintenance’ button.Enter the Maintenance window by entering the password same as a value in tag ‘PASS_CODE_MTN’ of the configuration file and confirming the password");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following information,The Wheel diameter button is disabled.The Radar button is enabled");
            /*
            Test Step 6
            Action: Perform the following procedure,Press ‘Maintenance’ button.Enter the Maintenance window by entering the password same as a value in tag ‘PASS_CODE_MTN’ of the configuration file and confirming the password
            Expected Result: Verify the following information,The Wheel diameter button is disabled.The Radar button is enabled
            Test Step Comment: (1) MMI_gen 11746 (partly: disable wheel diameter);(2) MMI_gen 11746 (partly: enable doppler);          
            */
            DmiActions.ShowInstruction(this,
                @"Press the ‘Maintenance’ button, enter the password from the PASS_CODE_MTN tag in the configuration and confirm the password");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Wheel diameter’ button is disabled" + Environment.NewLine +
                                "2. The ‘Radar’ button is enabled");

            TraceHeader("Test Step 7");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Perform the following procedure,Return to the Setting window by pressing Close’ button.Use test script file 22_6_2_a.xml to disable wheel diameter and doppler by sending EVC-30 with,MMI_NID_WINDOW = 4MMI_Q_REQUEST_ENABLE_64 (#29) = 0MMI_Q_REQUEST_ENABLE_64 (#30) = 0");
            TraceReport("Expected Result");
            TraceInfo("The ‘Maintenance’ button is disabled");
            /*
            Test Step 7
            Action: Perform the following procedure,Return to the Setting window by pressing Close’ button.Use test script file 22_6_2_a.xml to disable wheel diameter and doppler by sending EVC-30 with,MMI_NID_WINDOW = 4MMI_Q_REQUEST_ENABLE_64 (#29) = 0MMI_Q_REQUEST_ENABLE_64 (#30) = 0
            Expected Result: The ‘Maintenance’ button is disabled
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button to return to the Settings window");

            XML_22_6_2(msgType.typea);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Maintenance’ button is disabled");

            TraceHeader("Test Step 8");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Use test script file 22_6_2_d.xml to enable wheel diameter and doppler by sending EVC-30 with,MMI_NID_WINDOW = 4MMI_Q_REQUEST_ENABLE_64 (#29) = 1MMI_Q_REQUEST_ENABLE_64 (#30) = 1");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following information,DMI received the EVC-30 with [MMI_ENABLE_REQUEST (EVC-30).MMI_Q_REQUEST_ENABLE_64] (#29) = 1 in order to enable wheel diameter.DMI received the EVC-30 with [MMI_ENABLE_REQUEST (EVC-30).MMI_Q_REQUEST_ENABLE_64] (#30) = 1 in order to enabled doppler.The ‘Maintenance’ button is enabled");
            /*
            Test Step 8
            Action: Use test script file 22_6_2_d.xml to enable wheel diameter and doppler by sending EVC-30 with,MMI_NID_WINDOW = 4MMI_Q_REQUEST_ENABLE_64 (#29) = 1MMI_Q_REQUEST_ENABLE_64 (#30) = 1
            Expected Result: Verify the following information,DMI received the EVC-30 with [MMI_ENABLE_REQUEST (EVC-30).MMI_Q_REQUEST_ENABLE_64] (#29) = 1 in order to enable wheel diameter.DMI received the EVC-30 with [MMI_ENABLE_REQUEST (EVC-30).MMI_Q_REQUEST_ENABLE_64] (#30) = 1 in order to enabled doppler.The ‘Maintenance’ button is enabled
            Test Step Comment: (1) MMI_gen 11746 (partly: enable wheel diameter);(2) MMI_gen 11746 (partly: enable doppler);(3) MMI_gen 11724;
            */
            XML_22_6_2(msgType.typed);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Maintenance’ button is enabled");

            TraceHeader("Test Step 9");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Perform the following procedures,Press ‘Maintenance’ button.Enter the Maintenance window by entering the password same as a value in tag ‘PASS_CODE_MTN’ of the configuration file and confirming the password");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following information,Menu windowThe window title is ‘Maintenance’.The Maintenance window is in the D, F and G area.The following objects are display in Maintenance window. Enabled Close button (NA11)Window TitleButton 1 with label ‘Wheel diameter’Button 2 with label ‘Radar’Note: See the position of buttons in picture below,The state of each button in Brake window are displayed correctly as follows,Wheel diameter = EnableRadar = EnableLayersThe level of layers in each area of window as follows,Layer 0: Area D, F, G, E10, E11, Y, and ZLayer -1: Area A1, (A2+A3)*, A4, B*, C1, (C2+C3+C4)*, C5, C6, C7, C8, C9, E1, E2, E3, E4, (E5-E9)*.Layer -2: Area B3, B4, B5, B6 and B7.Note: ‘*’ symbol is mean that specified area are drawn as one area.General property of windowThe Maintenance window is presented with objects and buttons which is the one of several levels and allocated to areas of DMI. All objects, text messages and buttons are presented within the same layer.The Default window is not displayed and covered the current window");
            /*
            Test Step 9
            Action: Perform the following procedures,Press ‘Maintenance’ button.Enter the Maintenance window by entering the password same as a value in tag ‘PASS_CODE_MTN’ of the configuration file and confirming the password
            Expected Result: Verify the following information,Menu windowThe window title is ‘Maintenance’.The Maintenance window is in the D, F and G area.The following objects are display in Maintenance window. Enabled Close button (NA11)Window TitleButton 1 with label ‘Wheel diameter’Button 2 with label ‘Radar’Note: See the position of buttons in picture below,The state of each button in Brake window are displayed correctly as follows,Wheel diameter = EnableRadar = EnableLayersThe level of layers in each area of window as follows,Layer 0: Area D, F, G, E10, E11, Y, and ZLayer -1: Area A1, (A2+A3)*, A4, B*, C1, (C2+C3+C4)*, C5, C6, C7, C8, C9, E1, E2, E3, E4, (E5-E9)*.Layer -2: Area B3, B4, B5, B6 and B7.Note: ‘*’ symbol is mean that specified area are drawn as one area.General property of windowThe Maintenance window is presented with objects and buttons which is the one of several levels and allocated to areas of DMI. All objects, text messages and buttons are presented within the same layer.The Default window is not displayed and covered the current window
            Test Step Comment: (1) MMI_gen 11744; MMI_gen 4360 (partly: window title);(2) MMI_gen 11743 (partly: MMI_gen 7909);(3) MMI_gen 11743 (MMI_gen 4556 (partly: Close button, Window Title, Button 1, Button 2)); MMI_gen 11745; MMI_gen 4392 (partly: [Close] NA11);(4) MMI_gen 11746;(5) MMI_gen 11743 (partly: MMI_gen 4630, MMI gen 5944 (partly: touch screen));        (6) MMI_gen 4350;(7) MMI_gen 4351;(8) MMI_gen 4353;     
            */
            DmiActions.ShowInstruction(this,
                @"Press the ‘Maintenance’ button, enter the password from the PASS_CODE_MTN tag in the configuration and confirm the password");

            WaitForVerification("Check the following (* indicates sub-areas drawn as one area):" + Environment.NewLine +
                                Environment.NewLine +
                                "1. The window title is ‘Maintenance’." + Environment.NewLine +
                                "2. The Maintenance window is in areas D, F and G." + Environment.NewLine +
                                "3. The following screen areas are in Layer 0: D, F, G, E10, E11, Z and Y." +
                                Environment.NewLine +
                                "4. The following screen areas are in Layer 1: A1, (A2 + A3)*, A4, B, C1, (C2 + C3 + C4)*, C5, C6, C7, C8, C9, E1, E2, E3, E4, (E5-E9)*." +
                                Environment.NewLine +
                                "5. The following screen areas are in Layer 2: B3, B4, B5, B6, B7." +
                                Environment.NewLine +
                                "8. 2 buttons, ‘Wheel diameter’, ‘Radar’ are displayed enabled on the keypad." +
                                Environment.NewLine +
                                "9. The ‘Close’ button, symbol NA11, is displayed enabled." + Environment.NewLine +
                                "10. Objects and buttons can be displayed in several levels. Within a level they are allocated to areas." +
                                Environment.NewLine +
                                "11. All objects, text messages and buttons are displayed in the same layer." +
                                Environment.NewLine +
                                "12. The Default window is not displayed covering the Maintenance window.");

            TraceHeader("Test Step 10");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Press and hold ‘Wheel diameter’ button");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following information,The sound ‘Click’ played once.The ‘Wheel diameter’ button is shown as pressed state, the border of button is removed");
            /*
            Test Step 10
            Action: Press and hold ‘Wheel diameter’ button
            Expected Result: Verify the following information,The sound ‘Click’ played once.The ‘Wheel diameter’ button is shown as pressed state, the border of button is removed
            Test Step Comment: (1) MMI_gen 11743 (partly: MMI_gen 4381 (partly: the sound for Up-Type button)); MMI_gen 9512; MMI_gen 968;(2) MMI_gen 11743 (partly: partly: MMI_gen 4557 (partly: button ‘Wheel diameter’), MMI_gen 4381 (partly: change to state ‘Pressed’ as long as remain actuated)); MMI_gen 4375;
            */
            DmiActions.ShowInstruction(this, @"Press and hold the ‘Wheel diameter’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Wheel diameter’ button is displayed pressed, without a border." +
                                Environment.NewLine +
                                "2. The ‘Click’ sound is played once.");

            TraceHeader("Test Step 11");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Slide out ‘Wheel diameter’ button");
            TraceReport("Expected Result");
            TraceInfo("The border of the button is shown (state ‘Enabled’) without a sound");
            /*
            Test Step 11
            Action: Slide out ‘Wheel diameter’ button
            Expected Result: The border of the button is shown (state ‘Enabled’) without a sound
            Test Step Comment: MMI_gen 11743 (partly: MMI_gen 4557 (partly: button ‘Wheel diameter’, MMI_gen 4382 (partly: state ‘Enabled’ when slide out with force applied, no sound))); MMI_gen 4374;
            */
            DmiActions.ShowInstruction(this,
                @"Drag the ‘Wheel diameter’ button outside of its area, keeping the button pressed");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Wheel diameter’ button is displayed enabled." + Environment.NewLine +
                                "2. No sound is played.");

            TraceHeader("Test Step 12");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Slide back into ‘Wheel diameter’ button");
            TraceReport("Expected Result");
            TraceInfo("The button is back to state ‘Pressed’ without a sound");
            /*
            Test Step 12
            Action: Slide back into ‘Wheel diameter’ button
            Expected Result: The button is back to state ‘Pressed’ without a sound
            Test Step Comment: MMI_gen 11743 (partly: MMI_gen 4557 (partly: button ‘Wheel diameter’, MMI_gen 4382 (partly: state ‘Pressed’ when slide back, no sound))); MMI_gen 4375;
            */
            DmiActions.ShowInstruction(this,
                @"Whilst keeping the ‘Wheel diameter’ button pressed, drag it back inside its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Wheel diameter’ button is displayed pressed." + Environment.NewLine +
                                "2. No sound is played.");

            TraceHeader("Test Step 13");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Release ‘Wheel diameter’ button");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following information,DMI displays Wheel diameter windowUse the log file to confirm that DMI sends out the packet [MMI_DRIVER_REQUEST (EVC-101)] with variable [MMI_DRIVER_REQUEST (EVC-101).MMI_M_REQUEST] = 53 (Change Wheel diameter)");
            /*
            Test Step 13
            Action: Release ‘Wheel diameter’ button
            Expected Result: Verify the following information,DMI displays Wheel diameter windowUse the log file to confirm that DMI sends out the packet [MMI_DRIVER_REQUEST (EVC-101)] with variable [MMI_DRIVER_REQUEST (EVC-101).MMI_M_REQUEST] = 53 (Change Wheel diameter)
            Test Step Comment: (1) MMI_gen 11743 (partly: MMI_gen 4557 (partly: button ‘Wheel diameter’, MMI_gen 4381 (partly: exit state ‘Pressed’, execute function associated to the button)));(2) MMI_gen 11747 (partly: wheel diameter);
            */
            DmiActions.ShowInstruction(this, @"Release the ‘Wheel diameter’ button");

            EVC101_MMIDriverRequest.CheckMRequestReleased =
                Telegrams.EVCtoDMI.Variables.MMI_M_REQUEST.ChangeWheelDiameter;

            EVC40_MMICurrentMaintenanceData.MMI_Q_MD_DATASET = Variables.MMI_Q_MD_DATASET.WheelDiameter;
            EVC40_MMICurrentMaintenanceData.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Wheel diameter window.");

            TraceHeader("Test Step 14");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Press ‘Close’ button");
            TraceReport("Expected Result");
            TraceInfo("DMI displays the Maintenance window");
            /*
            Test Step 14
            Action: Press ‘Close’ button
            Expected Result: DMI displays the Maintenance window
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Maintenance window.");

            TraceHeader("Test Step 15");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Follow action step 10 – step 14 for ‘Radar’ button");
            TraceReport("Expected Result");
            TraceInfo(
                "See the expected results of Step 10 – Step 14 and the following additional information,DMI displays Radar window refer to released button from action step 13.Use the log file to confirm that DMI sends out the packet [MMI_DRIVER_REQUEST (EVC-101)] with variable [MMI_DRIVER_REQUEST (EVC-101).MMI_M_REQUEST] = 52 (Change Radar)");
            /*
            Test Step 15
            Action: Follow action step 10 – step 14 for ‘Radar’ button
            Expected Result: See the expected results of Step 10 – Step 14 and the following additional information,DMI displays Radar window refer to released button from action step 13.Use the log file to confirm that DMI sends out the packet [MMI_DRIVER_REQUEST (EVC-101)] with variable [MMI_DRIVER_REQUEST (EVC-101).MMI_M_REQUEST] = 52 (Change Radar)
            Test Step Comment: (1) MMI_gen 11743 (partly: MMI_gen 4557 (partly: button ‘Radar’));(2) MMI_gen 11747 (partly: Radar);
            */
            // Repeat Step 10 for Radar button
            DmiActions.ShowInstruction(this, @"Press and hold the ‘Radar’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Radar’ button is displayed pressed, without a border." + Environment.NewLine +
                                "2. The ‘Click’ sound is played once.");

            // Repeat Step 11 for Radar button
            DmiActions.ShowInstruction(this,
                @"Drag the ‘Radar’ button outside of its area, keeping the button pressed");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Radar’ button is displayed enabled." + Environment.NewLine +
                                "2. No sound is played.");

            // Repeat Step 12 for Radar button
            DmiActions.ShowInstruction(this,
                @"Whilst keeping the ‘Radar’ button pressed, drag it back inside its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Radar’ button is displayed pressed." + Environment.NewLine +
                                "2. No sound is played.");

            // Repeat Step 13 for Radar button
            DmiActions.ShowInstruction(this, @"Release the ‘Radar’ button");

            EVC101_MMIDriverRequest.CheckMRequestReleased = Telegrams.EVCtoDMI.Variables.MMI_M_REQUEST.ChangeDoppler;

            EVC40_MMICurrentMaintenanceData.MMI_Q_MD_DATASET = Variables.MMI_Q_MD_DATASET.Doppler;
            EVC40_MMICurrentMaintenanceData.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Radar window.");

            // Repeat Step 14 for Radar button
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Maintenance window.");

            TraceHeader("Test Step 16");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Press ‘Close’ button");
            TraceReport("Expected Result");
            TraceInfo("Verify the following information, (1)   DMI displays Setting window");
            /*
            Test Step 16
            Action: Press ‘Close’ button
            Expected Result: Verify the following information, (1)   DMI displays Setting window
            Test Step Comment: (1) MMI_gen 4392 (partly: returning to the parent window);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Settings window.");

            TraceHeader("Test Step 17");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Press the ‘Maintenance’ window.Then, follow action step 10 – step 13 ‘Close’ button");
            TraceReport("Expected Result");
            TraceInfo(
                "See the expected results of Step 10 – Step 13 and the following additional information,DMI displays Settings window");
            /*
            Test Step 17
            Action: Press the ‘Maintenance’ window.Then, follow action step 10 – step 13 ‘Close’ button
            Expected Result: See the expected results of Step 10 – Step 13 and the following additional information,DMI displays Settings window
            Test Step Comment: (1) MMI_gen 11743 (partly: MMI_gen 4557 (partly: button ‘Close’));
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Maintenance’ button");

            // Repeat Step 10 for ‘Close’ button 
            DmiActions.ShowInstruction(this, @"Press and hold the ‘Close’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Close’ button is displayed pressed, without a border." + Environment.NewLine +
                                "2. The ‘Click’ sound is played once.");

            // Repeat Step 11 for ‘Close’ button
            DmiActions.ShowInstruction(this,
                @"Drag the ‘Close’ button outside of its area, keeping the button pressed");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Close’ button is displayed enabled." + Environment.NewLine +
                                "2. No sound is played.");

            // Repeat Step 12 for ‘Close’ button
            DmiActions.ShowInstruction(this,
                @"Whilst keeping the ‘Close’ button pressed, drag it back inside its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Close’ button is displayed pressed." + Environment.NewLine +
                                "2. No sound is played.");

            // Repeat Step 13 for ‘Close’ button
            DmiActions.ShowInstruction(this, @"Release the ‘Close’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Settings window.");
            TraceHeader("Test Step 18");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("End of test");
            
            /*
            Test Step 18
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }

        #region Send_XML_22_6_2_DMI_Test_Specification

        enum msgType
        {
            typea,
            typeb,
            typec,
            typed
        }

        private void XML_22_6_2(msgType type)
        {
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Settings;
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.None;
            EVC30_MMIRequestEnable.Send();
            switch (type)
            {
                case msgType.typea:
                    // This step just wants doppler and wheel diameter disabled: nothing is said about the others so they might as well be disabled
                    /*
                           EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.Start |
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
                                                                       EVC30_MMIRequestEnable.EnabledRequests.Language |
                                                                       EVC30_MMIRequestEnable.EnabledRequests.Volume |
                                                                       EVC30_MMIRequestEnable.EnabledRequests.Brightness |
                                                                       EVC30_MMIRequestEnable.EnabledRequests.SystemVersion |
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
                                                                       EVC30_MMIRequestEnable.EnabledRequests.EnableBrakePercentage;
                    */
                    break;
                case msgType.typeb:
                    // The state here should be that all flags are disabled
                    EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH =
                        EVC30_MMIRequestEnable.EnabledRequests.EnableWheelDiameter;

                    // This step just wants wheel diameter enabled: nothing is said about the others so they might as well be disabled
                    /*
                           EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.Start |
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
                                                                       EVC30_MMIRequestEnable.EnabledRequests.Language |
                                                                       EVC30_MMIRequestEnable.EnabledRequests.Volume |
                                                                       EVC30_MMIRequestEnable.EnabledRequests.Brightness |
                                                                       EVC30_MMIRequestEnable.EnabledRequests.SystemVersion |
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
                                                                       EVC30_MMIRequestEnable.EnabledRequests.EnableBrakePercentage;
                    */
                    break;
                case msgType.typec:
                    // The state here should be that all flags are disabled
                    EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH =
                        EVC30_MMIRequestEnable.EnabledRequests.EnableDoppler;

                    // This step just wants wheel diameter enabled: nothing is said about the others so they might as well be disabled
                    /*
                           EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.Start |
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
                                                                       EVC30_MMIRequestEnable.EnabledRequests.Language |
                                                                       EVC30_MMIRequestEnable.EnabledRequests.Volume |
                                                                       EVC30_MMIRequestEnable.EnabledRequests.Brightness |
                                                                       EVC30_MMIRequestEnable.EnabledRequests.SystemVersion |
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
                                                                       EVC30_MMIRequestEnable.EnabledRequests.EnableBrakePercentage;
                    */
                    break;
                case msgType.typed:
                    // The state here should be that all flags are disabled
                    EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH =
                        EVC30_MMIRequestEnable.EnabledRequests.EnableWheelDiameter |
                        EVC30_MMIRequestEnable.EnabledRequests.EnableDoppler;

                    // This step just wants wheel diameter enabled: nothing is said about the others so they might as well be disabled
                    /*
                           EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.Start |
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
                                                                       EVC30_MMIRequestEnable.EnabledRequests.Language |
                                                                       EVC30_MMIRequestEnable.EnabledRequests.Volume |
                                                                       EVC30_MMIRequestEnable.EnabledRequests.Brightness |
                                                                       EVC30_MMIRequestEnable.EnabledRequests.SystemVersion |
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
                                                                       EVC30_MMIRequestEnable.EnabledRequests.EnableBrakePercentage;
                    */
                    break;
            }

            EVC30_MMIRequestEnable.Send();
        }

        #endregion
    }
}
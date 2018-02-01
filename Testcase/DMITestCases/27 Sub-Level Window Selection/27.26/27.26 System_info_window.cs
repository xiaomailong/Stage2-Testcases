using System;
using System.Collections.Generic;
using Testcase.Telegrams.EVCtoDMI;
using Testcase.Telegrams.DMItoEVC;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 27.26 System info window
    /// TC-ID: 22.26
    /// 
    /// This test case verifies the display information of System info window refer to chapter 6.5.6.4 of requirement specification document and verifies the value of displayed information is correct refer to received data from packet sending EVC-24.
    /// 
    /// Tested Requirements:
    /// MMI_gen 11902 (partly:MMI_gen 5338, MMI_gen 5306 (partly: Object in table 21),  MMI_gen 5383 (partly: MMI_gen 5944 (partly: touchscreen)), MMI_gen 5337, MMI_gen 5340 (partly: right aligned), MMI_gen 5342 (partly: left aligned), MMI_gen 5336 (partly: valid), MMI_gen 5335, MMI_gen 7510); MMI_gen 11903; MMI_gen 1552; MMI_gen 1551; MMI_gen 2463; MMI_gen 11695; MMI_gen 4392 (partly: [Previous : NA19], [Next: NA17], [Close] NA11, returning to the parent window); MMI_gen 4394 (partly: next, previous); MMI_gen 4396 (party: next, previous); MMI_gen 4350; MMI_gen 4351; MMI_gen 4353; MMI_gen 4358; MMI_gen 4360 (partly: total number of window); MMI_gen 9391 (partly: [Next], MMI_gen 4381 (partly: change to state ‘Pressed’ as long as remain actuated));
    /// 
    /// Scenario:
    /// Activate Cabin A.Open Settings window.Open System info window and verify the display information.Close System info window.Use the test scrip file to send data packet and verify the display information.Verify the functionality of Up-type button for the ‘Next’ and ‘Previouse’ buttons.
    /// 
    /// Used files:
    /// 22_26_a.xml, 22_26_b.xml
    /// </summary>
    public class TC_ID_22_26_System_info_window : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:

            // Call the TestCaseBase PreExecution
            base.PreExecution();

            // System is powered ON.
            DmiActions.Start_ATP();
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
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 0;
            // Testcase entrypoint
            TraceInfo("This test may not work without changing the configuration to enable System Info");

            TraceHeader("Test Step 1");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Activate Cabin A");
            TraceReport("Expected Result");
            TraceInfo("DMI displays Driver ID window");
            /*
            Test Step 1
            Action: Activate Cabin A
            Expected Result: DMI displays Driver ID window
            */
            DmiActions.Activate_Cabin_1(this);
            DmiActions.Set_Driver_ID(this, "1234");

            DmiExpectedResults.Driver_ID_window_displayed(this);

            TraceHeader("Test Step 2");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Press Settings button icon. Then, press ‘System info’ button");
            TraceReport("Expected Result");
            TraceInfo(
                "DMI displays System info window.Verify the following points below,Data View WindowsThe window is displayed in main area D, F and G.The text colour of Data View is grey.The following objects are displayed on DMI (see example picture in comment column), Enabled Close button (NA11) Enabled Next button (NA17) Disabled Previous button (NA19)Window titleLabelsThe Label part are consisted with an information in Data part.The labels of Data View is right aligned.The data of Data View is left aligned.If the values are valid, the Data Part is displayed the values.The window title is ‘System info’.LayersThe level of layers in each area of window as follows,Layer 0: Area D, F, G, E10, E11, Z, YLayer -1: Area A1, (A2+A3)*, A4, B*, C1, (C2+C3+C4)*, C5, C6, C7, C8, C9, E1, E2, E3, E4, (E5-E9)*.Layer -2: Area B3, B4, B5, B6 and B7.Note: ‘*’ symbol is mean that specified area are drawn as one area.Packet SendingUse the log file to verify that DMI send information EVC-101 with the following variable,MMI_M_REQUEST = 29General property of windowThe System info window is presented with objects, text messages and buttons which is the one of several levels and allocated to areas of DMI.All objects, text messages and buttons are presented within the same layer.The Default window is not displayed and covered the current window.The text label on window title is included with sequence number of the current window (e.g. ‘(1/2)’)");
            /*
            Test Step 2
            Action: Press Settings button icon. Then, press ‘System info’ button
            Expected Result: DMI displays System info window.Verify the following points below,Data View WindowsThe window is displayed in main area D, F and G.The text colour of Data View is grey.The following objects are displayed on DMI (see example picture in comment column), Enabled Close button (NA11) Enabled Next button (NA17) Disabled Previous button (NA19)Window titleLabelsThe Label part are consisted with an information in Data part.The labels of Data View is right aligned.The data of Data View is left aligned.If the values are valid, the Data Part is displayed the values.The window title is ‘System info’.LayersThe level of layers in each area of window as follows,Layer 0: Area D, F, G, E10, E11, Z, YLayer -1: Area A1, (A2+A3)*, A4, B*, C1, (C2+C3+C4)*, C5, C6, C7, C8, C9, E1, E2, E3, E4, (E5-E9)*.Layer -2: Area B3, B4, B5, B6 and B7.Note: ‘*’ symbol is mean that specified area are drawn as one area.Packet SendingUse the log file to verify that DMI send information EVC-101 with the following variable,MMI_M_REQUEST = 29General property of windowThe System info window is presented with objects, text messages and buttons which is the one of several levels and allocated to areas of DMI.All objects, text messages and buttons are presented within the same layer.The Default window is not displayed and covered the current window.The text label on window title is included with sequence number of the current window (e.g. ‘(1/2)’)
            Test Step Comment: (1) MMI_gen 11902 (partly: MMI_gen 5338); (2) MMI_gen 11902 (partly: MMI_gen 5337);   (3) MMI_gen 11902 (partly: MMI_gen 5306 (partly: Objects in table 21); MMI_gen 4392 (partly: [Previous : NA19], [Next: NA17], [Close] NA11); MMI_gen 4355 (partly: Buttons, Close button); MMI_gen 4396 (partly: Previous, NA19); MMI_gen 4394 (partly: disabled [previous]); (4) MMI_gen 11902 (partly: MMI_gen 5335);  (5) MMI_gen 11902 (partly: MMI_gen 5340 (partly: right aligned));  (6) MMI_gen 11902 (partly: MMI_gen 5342 (partly: left aligned));     (7) MMI_gen 11902 (partly: MMI_gen 5336);  (8) MMI_gen 11903;     (9) MMI_gen 11902 (partly: MMI_gen 5383 (MMI_gen 5944 (partly: touchscreen)); (10) MMI_gen 1552;(11) MMI_gen 4350;(12) MMI_gen 4351;(13) MMI_gen 4353;(14) MMI_gen 4360 (partly: partly: total number of window);
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Settings’ button");

            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Settings; // Settings window
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_LOW = true;
            EVC30_MMIRequestEnable.Send();

            DmiActions.ShowInstruction(this, @"Press the ‘System info’ button");

            EVC101_MMIDriverRequest.CheckMRequestReleased = Variables.MMI_M_REQUEST.SystemInfoRequest;

            EVC24_MMISystemInfo.MMI_NID_ENGINE_1 = 1234;
            EVC24_MMISystemInfo.MMI_T_TIMEOUT_BRAKE = 1452614220;
            EVC24_MMISystemInfo.MMI_T_TIMEOUT_BTM = 1421078220;
            EVC24_MMISystemInfo.MMI_T_TIMEOUT_TBSW = 1401638220;
            EVC24_MMISystemInfo.MMI_M_ETC_VER = 16755215;
            EVC24_MMISystemInfo.MMI_M_BRAKE_CONFIG = 236;
            EVC24_MMISystemInfo.MMI_M_AVAIL_SERVICES = 65535; // ?? undefined
            EVC24_MMISystemInfo.MMI_M_LEVEL_INST = 248; // ?? undefined
            EVC24_MMISystemInfo.MMI_NID_NTC = new List<byte> {20};
            EVC24_MMISystemInfo.MMI_NID_STMSTATE = new List<byte> {7}; // Data Available (DA)

            EVC24_MMISystemInfo.Send();

            WaitForVerification("Check the following (* indicates sub-areas drawn as one area):" + Environment.NewLine +
                                Environment.NewLine +
                                @"1. DMI displays the System info window with 3 layers, with the title ‘System info (1/2)’ where (1/2) shows the current window page." +
                                Environment.NewLine +
                                "2. The System info window is displayed in areas D, F and G." + Environment.NewLine +
                                "3. Layer 0 comprises areas D, F, G, E10, E11, Y and Z." + Environment.NewLine +
                                "4. Layer 1 comprises areas A1, (A2+A3)*, A4, B, C1, (C2+C3+c4)*, C5, C6, C7, C8, C9, E1, E2, E3, E4, (E5-E9)*." +
                                Environment.NewLine +
                                "5. Layer 2 comprises areas B3, B4, B5, B6 and B7." + Environment.NewLine +
                                @"6. An ‘Enabled Close’ button (symbol NA11), an ‘Enabled Next’ button (symbol NA17) and a ‘Disbled Previous’ button (symbol NA 19) are displayed." +
                                Environment.NewLine +
                                "7. Data fields, with a label part (right-aligned) and a data part (left-aligned), with grey text, are displayed." +
                                Environment.NewLine +
                                "8. The data parts of data fields are displayed if their values are valid." +
                                Environment.NewLine +
                                "9. Objects, text messages and buttons can be displayed in several levels. Within a level they are allocated to areas." +
                                Environment.NewLine +
                                "10. Objects, text messages and buttons in a layer form a window." +
                                Environment.NewLine +
                                "11. The Default window does not cover the current window.");

            TraceHeader("Test Step 3");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Press and hold ‘Next’ button");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following information,(1)   The state of button is changed to ‘Pressed’, the border of button is removed.(2)   The sound ‘Click’ is played once");
            /*
            Test Step 3
            Action: Press and hold ‘Next’ button
            Expected Result: Verify the following information,(1)   The state of button is changed to ‘Pressed’, the border of button is removed.(2)   The sound ‘Click’ is played once
            Test Step Comment: (1) MMI_gen 9391 (partly: [Next], MMI_gen 4381 (partly: change to state ‘Pressed’ as long as remain actuated));(2) MMI_gen 9391 (partly: [Next], MMI_gen 4381 (partly: sound ‘Click’));
            */
            DmiActions.ShowInstruction(this, @"Press and hold the ‘Next’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Next’ button is displayed pressed, without a border.");

            TraceHeader("Test Step 4");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Slide out the ‘Next’ button");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following information,(1)   The border of the button is shown (state ‘Enabled’) without a sound");
            /*
            Test Step 4
            Action: Slide out the ‘Next’ button
            Expected Result: Verify the following information,(1)   The border of the button is shown (state ‘Enabled’) without a sound
            Test Step Comment: (1) MMI_gen 9391 (partly: [Next], MMI_gen 4382 (partly: state ‘Enabled’ when slide out with force applied, no sound));
            */
            DmiActions.ShowInstruction(this, "Whilst keeping the ‘Next’ button pressed, drag it out of its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Next’ button is displayed enabled, with a border." + Environment.NewLine +
                                "2. No sound is played.");


            TraceHeader("Test Step 5");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Slide back into the ‘Next’ button");
            TraceReport("Expected Result");
            TraceInfo("Verify the following information,(1)   The button is back to state ‘Pressed’ without a sound");
            /*
            Test Step 5
            Action: Slide back into the ‘Next’ button
            Expected Result: Verify the following information,(1)   The button is back to state ‘Pressed’ without a sound
            Test Step Comment: (1) MMI_gen 9391 (partly: [Next], MMI_gen 4382 (partly: state ‘Enabled’ when slide out with force applied, no sound));
            */
            DmiActions.ShowInstruction(this,
                "Whilst keeping the the data input field pressed, drag it back inside its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Next’ button is displayed pressed." + Environment.NewLine +
                                "2. No sound is played.");

            TraceHeader("Test Step 6");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Release ‘Next’ button");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following information,(1)   The state of ‘Previous’ and ‘Next’ button are displayed as follows,   ‘Next’ button is disabled, displays as symbol NA18.2‘Previous’ button is enabled, displays as symbol NA18(2)   The window title is displayed as ‘System window (2/2)’ The scrolling between various windows is not displayed as circular");
            /*
            Test Step 6
            Action: Release ‘Next’ button
            Expected Result: Verify the following information,(1)   The state of ‘Previous’ and ‘Next’ button are displayed as follows,   ‘Next’ button is disabled, displays as symbol NA18.2‘Previous’ button is enabled, displays as symbol NA18(2)   The window title is displayed as ‘System window (2/2)’ The scrolling between various windows is not displayed as circular
            Test Step Comment: (1) MMI_gen 4394 (partly: enabled [previous], disabled [next]); MMI_gen 4396 (partly: Next, NA18.2, Previous, NA18);(2) MMI_gen 4358;
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Release the ‘Next’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Next’ button is displayed disabled (symbol NA 18.2)" + Environment.NewLine +
                                "2. The ‘Previous’ button is displayed enabled (symbol NA 18)." + Environment.NewLine +
                                "3. The window title changes to ‘System info (2/2)’.");

            TraceHeader("Test Step 7");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Perform action step 3-6 for ‘Previous’ button");
            TraceReport("Expected Result");
            TraceInfo(
                "See the expected result of step 3-6 and the following points,(1)   The state of ‘Previous’ and ‘Next’ button are displayed as follows,‘Next’ button is enabled, displays as symbol NA17 ‘Previous’ button is enabled, displays as symbol NA19(2)    The window title is displayed as ‘System window (1/2)’ The scrolling between various windows is not displayed as circular");
            /*
            Test Step 7
            Action: Perform action step 3-6 for ‘Previous’ button
            Expected Result: See the expected result of step 3-6 and the following points,(1)   The state of ‘Previous’ and ‘Next’ button are displayed as follows,‘Next’ button is enabled, displays as symbol NA17 ‘Previous’ button is enabled, displays as symbol NA19(2)    The window title is displayed as ‘System window (1/2)’ The scrolling between various windows is not displayed as circular
            Test Step Comment: (1) MMI_gen 4394 (partly: enabled [next], disabled [previous]); MMI_gen 4396 (partly: Next, NA17, Previous, NA19);(2) MMI_gen 4358;
            */
            // Repeat Step 3 for the ‘Previous’ button
            DmiActions.ShowInstruction(this, @"Press and hold the ‘Previous’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Previous’ button is displayed pressed, without a border.");

            // Repeat Step 4 for the ‘Previous’ button
            DmiActions.ShowInstruction(this, "Whilst keeping the ‘Previous’ button pressed, drag it out of its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Previous’ button is displayed enabled, with a border." + Environment.NewLine +
                                "2. No sound is played.");

            // Repeat Step 5 for the ‘Previous’ button
            DmiActions.ShowInstruction(this,
                "Whilst keeping the the data input field pressed, drag it back inside its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Previous’ button is displayed pressed." + Environment.NewLine +
                                "2. No sound is played.");

            // Repeat Step 6 for the ‘Previous’ button
            DmiActions.ShowInstruction(this, @"Release the ‘Previous’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Next’ button is displayed evabled (symbol NA 17)" + Environment.NewLine +
                                "2. The ‘Previous’ button is displayed disabled (symbol NA 19)." + Environment.NewLine +
                                "3. The window title changes to ‘System info (1/2)’.");

            TraceHeader("Test Step 8");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Press ‘Close’ button");
            TraceReport("Expected Result");
            TraceInfo("DMI displays Settings window");
            /*
            Test Step 8
            Action: Press ‘Close’ button
            Expected Result: DMI displays Settings window
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Settings window");

            TraceHeader("Test Step 9");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Use the test script file 22_26_a.xml to send EVC-30 withMMI_Q_REQUEST_ENABLE_64 (#32) = 0");
            TraceReport("Expected Result");
            TraceInfo("Verify that ‘System info’ button is disabled");
            /*
            Test Step 9
            Action: Use the test script file 22_26_a.xml to send EVC-30 withMMI_Q_REQUEST_ENABLE_64 (#32) = 0
            Expected Result: Verify that ‘System info’ button is disabled
            Test Step Comment: MMI_gen 1551 (partly: disabling);
            */
            XML_22_26(msgType.typea);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘System info’ button is disabled.");

            TraceHeader("Test Step 10");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Press ‘System info’ button");
            TraceReport("Expected Result");
            TraceInfo(
                "Use the log file to verify that there is no packet information EVC-101 with MMI_M_REQUEST = 29 sent from DMI to ETCS");
            /*
            Test Step 10
            Action: Press ‘System info’ button
            Expected Result: Use the log file to verify that there is no packet information EVC-101 with MMI_M_REQUEST = 29 sent from DMI to ETCS
            Test Step Comment: MMI_gen 1552 (partly:pressed disabled button);
            */
            DmiActions.ShowInstruction(this, @"Press the ‘System info’ button");

            TraceHeader("Test Step 11");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Use the test script file 22_26_b.xml to send EVC-30 withMMI_Q_REQUEST_ENABLE_64 (#32) = 1Then, send EVC-24 with MMI_NID_ENGINE_1 = 1234MMI_M_BRAKE_CONFIG = 55MMI_M_AVAIL_SERVICES = 65535MMI_M_ETC_VER = 16755215");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the state of ‘System info’ button is enabled.DMI displays System info window after received packet EVC-24.The Data part of Data view text is able to display multiples value in same label (see the value of hardware configuration are splitted to be a multiple lines).Verify the display information are displayed correctly refer to received packet as follows,MMI Product version = 255.170.15Vehicle ID = 1234Brake configuration is display only the following items,SB availableSB as RTWRelease TCO @BRTCO feedback OKSoft isolation allowedHardware configuration is display only the following items,MMI 1MMI 2Redundant MMI 1Redundant MMI 2BTM antenna 1BTM antenna 2Radio modem 1Radio modem 2DRUEuroloop BTM(s)");
            /*
            Test Step 11
            Action: Use the test script file 22_26_b.xml to send EVC-30 withMMI_Q_REQUEST_ENABLE_64 (#32) = 1Then, send EVC-24 with MMI_NID_ENGINE_1 = 1234MMI_M_BRAKE_CONFIG = 55MMI_M_AVAIL_SERVICES = 65535MMI_M_ETC_VER = 16755215
            Expected Result: Verify the state of ‘System info’ button is enabled.DMI displays System info window after received packet EVC-24.The Data part of Data view text is able to display multiples value in same label (see the value of hardware configuration are splitted to be a multiple lines).Verify the display information are displayed correctly refer to received packet as follows,MMI Product version = 255.170.15Vehicle ID = 1234Brake configuration is display only the following items,SB availableSB as RTWRelease TCO @BRTCO feedback OKSoft isolation allowedHardware configuration is display only the following items,MMI 1MMI 2Redundant MMI 1Redundant MMI 2BTM antenna 1BTM antenna 2Radio modem 1Radio modem 2DRUEuroloop BTM(s)
            Test Step Comment: (1) MMI_gen 1551 (partly: enabling); (2) MMI_gen 2463 (partly: open System info window);(3) MMI_gen 11902 (partly:MMI_gen 7510);      (4) MMI_gen 2463 (partly: presentation of data); MMI_gen 11695; MMI_gen 11902 (partly: MMI_gen 5336 (partly: valid));
            */
            XML_22_26(msgType.typeb);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘System info’ button is enabled.");

            EVC24_MMISystemInfo.MMI_NID_ENGINE_1 = 1234;
            EVC24_MMISystemInfo.MMI_T_TIMEOUT_BRAKE = 1452614220;
            EVC24_MMISystemInfo.MMI_T_TIMEOUT_BTM = 1421078220;
            EVC24_MMISystemInfo.MMI_T_TIMEOUT_TBSW = 1401638220;
            EVC24_MMISystemInfo.MMI_M_ETC_VER = 16755215;
            EVC24_MMISystemInfo.MMI_M_BRAKE_CONFIG = 236;
            EVC24_MMISystemInfo.MMI_M_AVAIL_SERVICES = 65535; // ?? undefined
            EVC24_MMISystemInfo.MMI_M_LEVEL_INST = 248; // ?? undefined
            EVC24_MMISystemInfo.MMI_NID_NTC = new List<byte> {20};
            EVC24_MMISystemInfo.MMI_NID_STMSTATE = new List<byte> {7}; // Data Available (DA)

            EVC24_MMISystemInfo.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the System info window." + Environment.NewLine +
                                "2. The data parts of data fields may be displayed across 2 lines." +
                                Environment.NewLine +
                                "3. The value displayed for ‘MMI Product version’ is ‘255.170.15’." +
                                Environment.NewLine +
                                "4. The value displayed for ‘Vehicle ID’ is ‘1234’." + Environment.NewLine +
                                "5. Brake configuration displays only the following:" + Environment.NewLine +
                                "   ‘SB available; ‘SR as RTW’; ‘Release TCO@BR’; ‘TCO feedback OK’; ‘Soft isolation allowed’." +
                                Environment.NewLine +
                                "6. Hardware configuration displays only the following:" + Environment.NewLine +
                                "   ‘MMI 1’; ‘MMI 2’; ‘Redundant MMI 1’; ‘Redundant MMI 2’; ‘BTM antenna 1’; ‘BTM antenna 2’;" +
                                Environment.NewLine +
                                "   ‘Radio modem 1’; ‘Radio modem 2’ ‘DRU’; ‘Euroloop BTM(s)’.");

            TraceHeader("Test Step 12");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("End of test");
            TraceReport("Expected Result");
            TraceInfo("");
            /*
            Test Step 12
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }

        #region Send_XML_22_26_DMI_Test_Specification

        enum msgType
        {
            typea,
            typeb
        }

        private void XML_22_26(msgType type)
        {
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Settings;
            switch (type)
            {
                case msgType.typea:
                    EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.Language |
                                                                       EVC30_MMIRequestEnable.EnabledRequests.Volume |
                                                                       EVC30_MMIRequestEnable.EnabledRequests
                                                                           .Brightness |
                                                                       EVC30_MMIRequestEnable.EnabledRequests
                                                                           .SystemVersion |
                                                                       EVC30_MMIRequestEnable.EnabledRequests.SetVBC;
                    break;
                case msgType.typeb:
                    EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.Language |
                                                                       EVC30_MMIRequestEnable.EnabledRequests.Volume |
                                                                       EVC30_MMIRequestEnable.EnabledRequests
                                                                           .Brightness |
                                                                       EVC30_MMIRequestEnable.EnabledRequests
                                                                           .SystemVersion |
                                                                       EVC30_MMIRequestEnable.EnabledRequests.SetVBC |
                                                                       EVC30_MMIRequestEnable.EnabledRequests
                                                                           .EnableWheelDiameter;
                    break;
            }

            EVC30_MMIRequestEnable.Send();
        }

        #endregion
    }
}
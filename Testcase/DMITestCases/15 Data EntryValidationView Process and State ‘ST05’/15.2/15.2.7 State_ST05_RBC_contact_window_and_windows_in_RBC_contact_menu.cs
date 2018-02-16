using System;
using System.Collections.Generic;
using Testcase.Telegrams.EVCtoDMI;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// Updated to DMI Test Spec 4.4 by JS at 2018-02-16
    /// 
    /// 15.2.7 State 'ST05': RBC contact window and windows in RBC contact menu
    /// TC-ID: 10.2.7
    /// 
    /// This test case verifies the buttons of RBC contact window and windows in RBC contact menu state when entry and exit on state of 'ST05'.
    /// 
    /// Tested Requirements:
    /// MMI_gen 12018 (partly: RBC contact window and windows in RBC contact menu); MMI_gen 168 (partly: deselect input field, disabled buttons, RBC contact window and windows in RBC contact menu); MMI_gen 4395 (partly: close button, disabled, RBC contact window and windows in RBC contact menu); MMI_gen 4396 (partly: close, NA11, NA12, RBC contact window and windows in RBC contact menu); MMI_gen 5646 (partly: always enable, Exceptions of RBC contact window (disable, enable), State ‘ST05’ button is disabled, RBC contact window and windows in RBC contact menu); MMI_gen 5728 (partly: input field, removal, EVC, restore after ST05, RBC contact window and windows in RBC contact menu); MMI_gen 8521 (partly: Move to the right every second, no more possible to display, vertically centered, RBC contact window and windows in RBC contact menu); MMI_gen 8859 (partly: RBC contact window and windows in RBC contact menu);
    /// 
    /// Scenario:
    /// 1.The ‘RBC contact’ menu window is displayed.
    /// 2.Use the test script files to send packets in order to verify state ‘ST05’ in a menu window. 
    /// 3.Use the test script files to send packets in order to verify ‘close’ button control by ATP. 
    /// 4.Open the ‘Radio network ID’ window and use the test script files to send packets in order to verify state ‘ST05’.
    /// 5.Open the ‘Enter RBC data’ window and use the test script files to send packets in order to verify state ‘ST05’.                                                                                                                                                 
    /// 
    /// Used files:
    /// 10_2_7_a.xml, 10_2_7_b.xml,10_2_7.utt
    /// </summary>
    public class TC_ID_10_2_7_State_ST05 : TestcaseBase
    {
        public override bool TestcaseEntryPoint()
        {
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 21204;
            // Testcase entrypoint

            StartUp();

            MakeTestStepHeader(1, UniqueIdentifier++, "Perform SoM until select and confirm Level 2",
                "Verify the following information;(1)   Verify DMI still displays Level window until RBC contact window is displayed");
            /*
            Test Step 1
            Action: Perform SoM until select and confirm Level 2
            Expected Result: Verify the following information;(1)   Verify DMI still displays Level window until RBC contact window is displayed
            Test Step Comment: (1) MMI_gen 8859 (partly: RBC contact window);
            */

            // Set driver ID
            DmiActions.Set_Driver_ID(this, "1234");

            EVC20_MMISelectLevel.MMI_Q_CLOSE_ENABLE = Variables.MMI_Q_CLOSE_ENABLE.Disabled;

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
                {Variables.MMI_M_LEVEL_NTC_ID.L2};
            EVC20_MMISelectLevel.Send();

            DmiActions.ShowInstruction(this, "Confirm Level 2");

            // Set to level 2 and SR mode 
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Level = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_LEVEL.L2;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.StandBy;

            // Enable standard buttons including Start, and display Default window.
            DmiActions.Finished_SoM_Default_Window(this);

            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.No_window_specified;
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.ContactLastRBC |
                                                               EVC30_MMIRequestEnable.EnabledRequests.EnterRBCData |
                                                               EVC30_MMIRequestEnable.EnabledRequests.RadioNetworkID |
                                                               EVC30_MMIRequestEnable.EnabledRequests.UseShortNumber;
            EVC30_MMIRequestEnable.Send();

            EVC22_MMICurrentRBC.MMI_NID_WINDOW = 5;
            EVC22_MMICurrentRBC.MMI_M_BUTTONS = EVC22_MMICurrentRBC.EVC22BUTTONS.BTN_YES_DATA_ENTRY_COMPLETE;
            EVC22_MMICurrentRBC.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the RBC contact window.");

            MakeTestStepHeader(2, UniqueIdentifier++,
                "Use the test script file 10_2_7_a.xml to disable and enable button of the RBC contact window via EVC-8 withPacket 1 (Entry state of ‘ST05’)MMI_Q_TEXT_CRITERIA = 3 MMI_Q_TEXT = 716Packet 2 (Exit state of ‘ST05’)MMI_Q_TEXT_CRITERIA = 4MMI_Q_TEXT = 716",
                "Verify the following information;DMI in the entry state of ‘ST05’(1)    The hourglass symbol ST05 is displayed at window title area.(2)    The hourglass symbol ST05 is vertically aligned center of the window title area.(3)    The symbol ST05 is move to the right every second.(4)    After symbol ST05 is moved to the end of the window title area, the symbol comes back to the first position and keeps moving to the right. (5)    Verify all buttons and the close button is disable.(6)    The disabled Close button NA12 is display in area G.10 seconds laterDMI in the exit state of ‘ST05’(7)   The hourglass symbol ST05 is removed.(8)   The state of all buttons is restored according to the last status before script is sent.(9)   The enabled Close button NA11 is display in area G");
            /*
            Test Step 2
            Action: Use the test script file 10_2_7_a.xml to disable and enable button of the RBC contact window via EVC-8 withPacket 1 (Entry state of ‘ST05’)MMI_Q_TEXT_CRITERIA = 3 MMI_Q_TEXT = 716Packet 2 (Exit state of ‘ST05’)MMI_Q_TEXT_CRITERIA = 4MMI_Q_TEXT = 716
            Expected Result: Verify the following information;DMI in the entry state of ‘ST05’(1)    The hourglass symbol ST05 is displayed at window title area.(2)    The hourglass symbol ST05 is vertically aligned center of the window title area.(3)    The symbol ST05 is move to the right every second.(4)    After symbol ST05 is moved to the end of the window title area, the symbol comes back to the first position and keeps moving to the right. (5)    Verify all buttons and the close button is disable.(6)    The disabled Close button NA12 is display in area G.10 seconds laterDMI in the exit state of ‘ST05’(7)   The hourglass symbol ST05 is removed.(8)   The state of all buttons is restored according to the last status before script is sent.(9)   The enabled Close button NA11 is display in area G
            Test Step Comment: (1) MMI_gen 12018 (partly: RBC contact window);(2) MMI_gen 8521 (partly: vertically centered, RBC contact window);(3) MMI_gen 8521 (partly: Move to the right every second, RBC contact window);(4) MMI_gen 8521 (partly: no more possible to display, RBC contact window);(5) MMI_gen 168 (partly: disabled buttons, RBC contact window); MMI_gen 5646 (partly: State ‘ST05’ button is disabled, RBC contact window); MMI_gen 4395 (partly: close button, disabled, RBC contact window);(6) MMI_gen 4396 (partly: close, NA12, RBC contact window);(7) MMI_gen 5728 (partly: removal, EVC, RBC contact window);(8) MMI_gen 5728 (partly: restore after ST05, RBC contact window);(9) MMI_gen 4396 (partly: close, NA11, RBC contact window);
            */
            // Step 2/1
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 716;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;

            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI is in the entry state of ‘ST05’." + Environment.NewLine +
                                "2. The hourglass symbol ST05 is displayed vertically aligned in the center of the window title area." +
                                Environment.NewLine +
                                "3. The hourglass symbol ST05 moves to the right every second." +
                                Environment.NewLine +
                                "4. When the hourglass symbol ST05 has reached the edge of the window title area it is re-displayed on the lefthand side of the window title area and continues to move to the right." +
                                Environment.NewLine +
                                "5. All buttons and the ‘Close’ button are disabled." + Environment.NewLine +
                                "6. ‘Close’ button NA12 is displayed disabled in area G.");

            Wait_Realtime(10000);

            // Step 2/2
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 4;

            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI is in the exit state of ‘ST05’." + Environment.NewLine +
                                "2. The hourglass symbol ST05 is removed." + Environment.NewLine +
                                "3. All buttons are enabled." + Environment.NewLine +
                                "4. ‘Close’ button NA11 is displayed enabled in area G.");

            MakeTestStepHeader(3, UniqueIdentifier++,
                "Use the test script file 10_2_7_b.xml to disable and enable button via EVC-22 withPacket 1 (disable all button)MMI_Q_CLOSE_ENABLE (#0) = 1Packet 2 (enable all button)MMI_Q_CLOSE_ENABLE (#0) = 0Note: Stopwatch is required for accuracy of test result",
                "Verify the following information;(1)   ‘close’ buttons in RBC contact window is enable.10 seconds later(2)   ‘close’ buttons in Level window is disable");
            /*
            Test Step 3
            Action: Use the test script file 10_2_7_b.xml to disable and enable button via EVC-22 withPacket 1 (disable all button)MMI_Q_CLOSE_ENABLE (#0) = 1Packet 2 (enable all button)MMI_Q_CLOSE_ENABLE (#0) = 0Note: Stopwatch is required for accuracy of test result
            Expected Result: Verify the following information;(1)   ‘close’ buttons in RBC contact window is enable.10 seconds later(2)   ‘close’ buttons in Level window is disable
            Test Step Comment: (1) MMI_gen 5646 (partly: Exceptions of RBC contact window, enable);(2) MMI_gen 5646 (partly: Exceptions of RBC contact windows, disable);
            */
            EVC22_MMICurrentRBC.MMI_Q_CLOSE_ENABLE = Variables.MMI_Q_CLOSE_ENABLE.Enabled;
            EVC22_MMICurrentRBC.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. ‘Close’ button in RBC contact window is enabled.");

            Wait_Realtime(10000);

            EVC22_MMICurrentRBC.MMI_Q_CLOSE_ENABLE = Variables.MMI_Q_CLOSE_ENABLE.Disabled;
            EVC22_MMICurrentRBC.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. ‘Close’ button in RBC contact window is disabled.");

            MakeTestStepHeader(4, UniqueIdentifier++,
                "Perform the following procedure;Press and hold ‘Radio network ID’ button at least 2 secondsRelease ‘Radio network ID’ button",
                "Verify the following information;(1)   Verify DMI still displays RBC contact window until Radio network ID window is displayed.(2)   Verify the close button is always enable");
            /*
            Test Step 4
            Action: Perform the following procedure;Press and hold ‘Radio network ID’ button at least 2 secondsRelease ‘Radio network ID’ button
            Expected Result: Verify the following information;(1)   Verify DMI still displays RBC contact window until Radio network ID window is displayed.(2)   Verify the close button is always enable
            Test Step Comment: (1) MMI_gen 8859 (partly: windows in RBC contact menu);(2) MMI_gen 5646 (partly: always enable, windows in RBC contact menu);
            */
            DmiActions.ShowInstruction(this,
                @"Press and hold the ‘Radio Network ID’ button for at least two seconds. Release ‘Radio network ID’ button");

            EVC22_MMICurrentRBC.MMI_NID_WINDOW = 9;
            EVC22_MMICurrentRBC.MMI_Q_CLOSE_ENABLE = Variables.MMI_Q_CLOSE_ENABLE.Enabled;
            EVC22_MMICurrentRBC.MMI_M_BUTTONS = EVC22_MMICurrentRBC.EVC22BUTTONS.BTN_YES_DATA_ENTRY_COMPLETE;
            EVC22_MMICurrentRBC.NetworkCaptions = new List<string> {"GSMR-A", "GSMR-B"};
            EVC22_MMICurrentRBC.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the RBC contact window until the Radio network ID window is displayed." +
                                Environment.NewLine +
                                @"2. ‘Close’ button is always enabled.");

            MakeTestStepHeader(5, UniqueIdentifier++, "Repeat action step 2 with Radio network ID window",
                "Verify the following information;DMI entry on state of 'ST05'(1)   The hourglass symbol ST05 is displayed.(2)   Verify all buttons and the close button is disable.(3)   The disabled Close button NA12 is display in area G.(4)   The Input Field is deselected.10 seconds laterDMI exit on state of 'ST05'(5)   The hourglass symbol ST05 is removed.(6)   The state of all buttons is restored according to the last status before script is sent.(7)   The enabled Close button NA11 is display in area G.(8)   The input field is in the ‘Selected’ state");
            /*
            Test Step 5
            Action: Repeat action step 2 with Radio network ID window
            Expected Result: Verify the following information;DMI entry on state of 'ST05'(1)   The hourglass symbol ST05 is displayed.(2)   Verify all buttons and the close button is disable.(3)   The disabled Close button NA12 is display in area G.(4)   The Input Field is deselected.10 seconds laterDMI exit on state of 'ST05'(5)   The hourglass symbol ST05 is removed.(6)   The state of all buttons is restored according to the last status before script is sent.(7)   The enabled Close button NA11 is display in area G.(8)   The input field is in the ‘Selected’ state
            Test Step Comment: (1) MMI_gen 12018 (partly: windows in RBC contact menu);(2) MMI_gen 168 (partly: disabled buttons, windows in RBC contact menu); MMI_gen 5646 (partly: State ‘ST05’ button is disabled, windows in RBC contact menu); MMI_gen 4395 (partly: close button, disabled, windows in RBC contact menu);(3) MMI_gen 4396 (partly: close, NA12, windows in RBC contact menu);(4) MMI_gen 168 (partly: deselect input field, windows in RBC contact menu);(5) MMI_gen 5728 (partly: removal, EVC, windows in RBC contact menu);(6) MMI_gen 5728 (partly: restore after ST05, windows in RBC contact menu);(7) MMI_gen 4396 (partly: close, NA11, windows in RBC contact menu);(8) MMI_gen 5728 (partly: input field, windows in RBC contact menu);
            */
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 716;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI is in the entry state of ‘ST05’." + Environment.NewLine +
                                "2. The hourglass symbol ST05 is displayed." + Environment.NewLine +
                                "3. All buttons and the ‘Close’ button are disabled." + Environment.NewLine +
                                "4. ‘Close’ button NA12 is displayed disabled in area G." + Environment.NewLine +
                                "5. The Input Field is not selected.");

            this.Wait_Realtime(10000);

            // Step 2/2
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 4;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI is in the exit state of ‘ST05’." + Environment.NewLine +
                                "2. The hourglass symbol ST05 is removed." + Environment.NewLine +
                                "3. All buttons are enabled." + Environment.NewLine +
                                "4. ‘Close’ button NA11 is displayed enabled in area G." + Environment.NewLine +
                                "5. The Input Field is selected.");

            MakeTestStepHeader(6, UniqueIdentifier++,
                "Perform the following procedure;Press ‘close’ button (Radio network ID window) Press ‘Enter RBC data’ button",
                "Verify the following information;(1)   Verify DMI still displays RBC contact window until RBC data window is displayed.(2)   Verify the close button is always enable");
            /*
            Test Step 6
            Action: Perform the following procedure;Press ‘close’ button (Radio network ID window) Press ‘Enter RBC data’ button
            Expected Result: Verify the following information;(1)   Verify DMI still displays RBC contact window until RBC data window is displayed.(2)   Verify the close button is always enable
            Test Step Comment: (1) MMI_gen 8859 (partly: windows in RBC contact menu);(2) MMI_gen 5646 (partly: always enable, windows in RBC contact menu);
            */
            DmiActions.ShowInstruction(this,
                @"Press ‘Close’ button in the Radio network ID window. Press ‘Enter RBC data’ button");

            EVC22_MMICurrentRBC.MMI_M_BUTTONS = EVC22_MMICurrentRBC.EVC22BUTTONS.BTN_YES_DATA_ENTRY_COMPLETE;
            EVC22_MMICurrentRBC.MMI_NID_WINDOW = 10;
            EVC22_MMICurrentRBC.MMI_Q_CLOSE_ENABLE = Variables.MMI_Q_CLOSE_ENABLE.Enabled;
            EVC22_MMICurrentRBC.NID_RBC = 0;
            EVC22_MMICurrentRBC.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the RBC contact window until the RBC data window is displayed." +
                                Environment.NewLine +
                                @"2. ‘Close’ button is always enabled.");

            MakeTestStepHeader(7, UniqueIdentifier++, "Repeat action step 2 with RBC data window",
                "Verify the following information;DMI entry on state of 'ST05'(1)   The hourglass symbol ST05 is displayed.(2)   Verify all buttons and the close button is disable.(3)   The disabled Close button NA12 is display in area G.(4)   All Input Field are deselected.10 seconds laterDMI exit on state of 'ST05'(5)   The hourglass symbol ST05 is removed.(6)   The state of all buttons is restored according to the last status before script is sent.(7)   The enabled Close button NA11 is display in area G.(8)   The input field is stated as follows:The first input field is in the ‘Selected’ state.The all others are in the ‘Not selected’ state");
            /*
            Test Step 7
            Action: Repeat action step 2 with RBC data window
            Expected Result: Verify the following information;DMI entry on state of 'ST05'(1)   The hourglass symbol ST05 is displayed.(2)   Verify all buttons and the close button is disable.(3)   The disabled Close button NA12 is display in area G.(4)   All Input Field are deselected.10 seconds laterDMI exit on state of 'ST05'(5)   The hourglass symbol ST05 is removed.(6)   The state of all buttons is restored according to the last status before script is sent.(7)   The enabled Close button NA11 is display in area G.(8)   The input field is stated as follows:The first input field is in the ‘Selected’ state.The all others are in the ‘Not selected’ state
            Test Step Comment: (1) MMI_gen 12018 (partly: windows in RBC contact menu);(2) MMI_gen 168 (partly: disabled buttons, windows in RBC contact menu); MMI_gen 5646 (partly: State ‘ST05’ button is disabled, windows in RBC contact menu); MMI_gen 4395 (partly: close button, disabled, windows in RBC contact menu);(3) MMI_gen 4396 (partly: close, NA12, windows in RBC contact menu);(4) MMI_gen 168 (partly: deselect input field, windows in RBC contact menu);(5) MMI_gen 5728 (partly: removal, EVC, windows in RBC contact menu);(6) MMI_gen 5728 (partly: restore after ST05, windows in RBC contact menu);(7) MMI_gen 4396 (partly: close, NA11, windows in RBC contact menu);(8) MMI_gen 5728 (partly: input field, windows in RBC contact menu);
            */
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 716;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI is in the entry state of ‘ST05’." + Environment.NewLine +
                                "2. The hourglass symbol ST05 is displayed." + Environment.NewLine +
                                "3. All buttons and the ‘Close’ button are disabled." + Environment.NewLine +
                                "4. ‘Close’ button NA12 is displayed disabled in area G." + Environment.NewLine +
                                "5. All Input Fields are not selected.");

            this.Wait_Realtime(10000);

            // Step 2/2
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 4;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI is in the exit state of ‘ST05’." + Environment.NewLine +
                                "2. The hourglass symbol ST05 is removed." + Environment.NewLine +
                                "3. All buttons are enabled." + Environment.NewLine +
                                "4. ‘Close’ button NA11 is displayed enabled in area G." + Environment.NewLine +
                                "5. The first Input Field is selected." + Environment.NewLine +
                                "6. All other Input Fields are not selected");

            TraceHeader("End of test");

            /*
            Test Step 8
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
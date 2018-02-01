using System;
using Testcase.Telegrams.EVCtoDMI;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 15.2.2 State 'ST05': Main window and windows in main menu.
    /// TC-ID: 10.2.2
    /// 
    /// This test case verifies the buttons of Main window and windows in main menu state when entry and exit on state of 'ST05'.
    /// 
    /// Tested Requirements:
    /// MMI_gen 12018 (partly: Main window and windows in main menu); MMI_gen 168 (partly: deselect input field, disabled buttons, Main window and windows in main menu); MMI_gen 4395 (partly: close button, disabled, Main window and windows in main menu); MMI_gen 4396 (partly: close, NA11, NA12, Main window and windows in main menu); MMI_gen 5646 (partly: always enable, Exceptions of Driver ID windows (disable, enable), Exceptions of Level windows (disable, enable), State ‘ST05’ button is disabled, Main window and windows in main menu); MMI_gen 5719 (partly: always enable, State ‘ST05’ button is disabled, Main window and windows in main menu);MMI_gen 5728 (partly: input field, removal, EVC, restore after ST05, Main window and windows in main menu); MMI_gen 8355 (partly: EVC-8, Move to the right every second, no more possible to display, vertically centered, Main window and windows in main menu); MMI_gen 8859 (partly: Main window and windows in main menu); Note under the MMI_gen 5728;
    /// 
    /// Scenario:
    /// 1.The ‘Main’ menu window is displayed.
    /// 2.Use the test script files to send packets in order to verify state ‘ST05’ in a menu window. 
    /// 3.Open the ‘Driver ID’ window and use the test script files to send packets in order to verify state ‘ST05’. 
    /// 4.Use the test script files to send packets in order to verify ‘close’ button control by ATP. 
    /// 5.Open the ‘Train data (fixed)’ window and use the test script files to send packets in order to verify state ‘ST05’. 
    /// 6.Open the ‘Validate Train Data’ window and use the test script files to send packets in order to verify state ‘ST05’. 
    /// 7.Open the ‘Train data (flexible)’ window and use the test script files to send packets in order to verify state ‘ST05’. 
    /// 8.Open the ‘Validate Train Data’ window and use the test script files to send packets in order to verify state ‘ST05’. 
    /// 9.Open the ‘Level’ window and use the test script files to send packets in order to verify state ‘ST05’. 
    /// 10.Use the test script files to send packets in order to verify ‘close’ button control by ATP. 
    /// 11.Open the ‘Train running number’ window and use the test script files to send packets in order to verify state ‘ST05’.
    /// 
    /// Used files:
    /// 10_2_2_a.xml, 10_2_2_b.xml, 10_2_2_c.xml
    /// </summary>
    public class TC_ID_10_2_2_State_ST05 : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // Open defaultValues_default.xml in OTE then set all value of parameter "  TR_OBU_TrainType" to 3Test system is powered onCabin is active

            // Call the TestCaseBase PreExecution
            base.PreExecution();
            DmiActions.Complete_SoM_L1_SB(this);
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in SB mode
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in SB mode, Level 1.");

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 0;
            // Testcase entrypoint
            TraceInfo("This test case requires an ATP configuration change - " +
                      "See Precondition requirements. If this is not done manually, the test may fail!");

            TraceHeader("Test Step 1");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Perform SoM until select and confirm Level 1");
            TraceReport("Expected Result");
            TraceInfo("DMI displays Main window.(1)   Verify the close button is always enable");
            /*
            Test Step 1
            Action: Perform SoM until select and confirm Level 1
            Expected Result: DMI displays Main window.(1)   Verify the close button is always enable
            Test Step Comment: (1) MMI_gen 5646 (partly: always enable, Main window);
            */
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH =
                EVC30_MMIRequestEnable.EnabledRequests.Start | Variables.standardFlags;
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Main;
            EVC30_MMIRequestEnable.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays Main Window." + Environment.NewLine +
                                "2. ‘Close’ button is enabled.");

            TraceHeader("Test Step 2");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Use the test script file 10_2_2_a.xml to disable and enable button of the Main window via EVC-8 withPacket 1 (Entry state of ‘ST05’)MMI_Q_TEXT_CRITERIA = 3 MMI_Q_TEXT = 716Packet 2 (Exit state of ‘ST05’)MMI_Q_TEXT_CRITERIA = 4MMI_Q_TEXT = 716Note: Stopwatch is required for accuracy of test result");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following information;DMI in the entry state of ‘ST05’.(1)   The hourglass symbol ST05 is displayed at window title area.(2)   The hourglass symbol ST05 is vertically aligned center of the window title area.(3)   The symbol ST05 is move to the right every second.(4)   After symbol ST05 is moved to the end of the window title area, the symbol comes back to the first position and keeps moving to the right. (5)   Verify all buttons and the close button are disable.(6)   The disabled Close button NA12 is display in area G.10 seconds later(7)   DMI in the exit state of ‘ST05’.(8)   The hourglass symbol ST05 is removed.(9)   The state of all buttons is restored according to the last status before script is sent.(10) The enabled Close button NA11 is display in area G");
            /*
            Test Step 2
            Action: Use the test script file 10_2_2_a.xml to disable and enable button of the Main window via EVC-8 withPacket 1 (Entry state of ‘ST05’)MMI_Q_TEXT_CRITERIA = 3 MMI_Q_TEXT = 716Packet 2 (Exit state of ‘ST05’)MMI_Q_TEXT_CRITERIA = 4MMI_Q_TEXT = 716Note: Stopwatch is required for accuracy of test result
            Expected Result: Verify the following information;DMI in the entry state of ‘ST05’.(1)   The hourglass symbol ST05 is displayed at window title area.(2)   The hourglass symbol ST05 is vertically aligned center of the window title area.(3)   The symbol ST05 is move to the right every second.(4)   After symbol ST05 is moved to the end of the window title area, the symbol comes back to the first position and keeps moving to the right. (5)   Verify all buttons and the close button are disable.(6)   The disabled Close button NA12 is display in area G.10 seconds later(7)   DMI in the exit state of ‘ST05’.(8)   The hourglass symbol ST05 is removed.(9)   The state of all buttons is restored according to the last status before script is sent.(10) The enabled Close button NA11 is display in area G
            Test Step Comment: (1) MMI_gen 12018 (partly: Main window); MMI_gen 8355 (partly: EVC-8, Main window);(2) MMI_gen 8355 (partly: vertically centered, Main window);(3) MMI_gen 8355 (partly: Move to the right every second, Main window);(4) MMI_gen 8355 (partly: no more possible to display, Main window);(5) MMI_gen 168 (partly: disabled buttons, Main window); MMI_gen 5646 (partly: State ‘ST05’ button is disabled, Main window); MMI_gen 4395 (partly: close button, disabled, Main window); (6) MMI_gen 4396 (partly: close, NA12, Main window);(7) MMI_gen 5728 (partly: removal, EVC, Main window);(8) MMI_gen 5728 (partly: restore after ST05, Main window);(9) MMI_gen 4396 (partly: close, NA11, Main window);
            */
            XML_10_2_2_a();

            TraceHeader("Test Step 3");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Press ‘Driver ID’ button");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following information;(1)   Verify DMI still displays Main window until Driver ID window is displayed.(2)   Verify the close button is always enable");
            /*
            Test Step 3
            Action: Press ‘Driver ID’ button
            Expected Result: Verify the following information;(1)   Verify DMI still displays Main window until Driver ID window is displayed.(2)   Verify the close button is always enable
            Test Step Comment: (1) MMI_gen 8859 (partly: windows in main menu);(2) MMI_gen 5646 (partly: always enable, windows in main menu);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press ‘Driver ID’ button");

            EVC14_MMICurrentDriverID.MMI_X_DRIVER_ID = "1234";
            EVC14_MMICurrentDriverID.MMI_Q_CLOSE_ENABLE = Variables.MMI_Q_CLOSE_ENABLE.Enabled;
            EVC14_MMICurrentDriverID.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays Main window until Driver ID window is displayed." +
                                Environment.NewLine +
                                "2. ‘Close’ button is always enabled.");

            TraceHeader("Test Step 4");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Repeat action step 2 with Driver ID window");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following information;DMI in the entry state of ‘ST05’.(1)   The hourglass symbol ST05 is displayed.(2)   Verify all buttons and the close button are disable.(3)   The disabled Close button NA12 is display in area G.(4)   The Input Field is deselected.10 seconds laterDMI in the exit state of ‘ST05’.(5)   The hourglass symbol ST05 is removed.(6)  The state of all buttons is restored according to the last status before script is sent.(7)  The enabled Close button NA11 is display in area G.(8)   The input field is in the ‘Selected’ state");
            /*
            Test Step 4
            Action: Repeat action step 2 with Driver ID window
            Expected Result: Verify the following information;DMI in the entry state of ‘ST05’.(1)   The hourglass symbol ST05 is displayed.(2)   Verify all buttons and the close button are disable.(3)   The disabled Close button NA12 is display in area G.(4)   The Input Field is deselected.10 seconds laterDMI in the exit state of ‘ST05’.(5)   The hourglass symbol ST05 is removed.(6)  The state of all buttons is restored according to the last status before script is sent.(7)  The enabled Close button NA11 is display in area G.(8)   The input field is in the ‘Selected’ state
            Test Step Comment: (1) MMI_gen 12018 (partly: windows in main menu);(2) MMI_gen 168 (partly: disabled buttons, windows in main menu); MMI_gen 5646 (partly: State ‘ST05’ button is disabled, windows in main menu); MMI_gen 4395 (partly: close button, disabled, windows in main menu);(3) MMI_gen 4396 (partly: close, NA12, windows in main menu);(4) MMI_gen 168 (partly: deselect input field, windows in main menu);(5) MMI_gen 5728 (partly: removal, EVC, windows in main menu);(6) MMI_gen 5728 (partly: restore after ST05, windows in main menu);(7) MMI_gen 4396 (partly: close, NA11, windows in main menu);(8) MMI_gen 5728 (partly: input field, windows in main menu)
            */
            XML_10_2_2_a(true);

            TraceHeader("Test Step 5");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Use the test script file 10_2_2_b.xml to disable and enable button via EVC-14 withPacket 1 (disable all button)MMI_Q_CLOSE_ENABLE (#0) = 0Packet 2 (enable all button)MMI_Q_CLOSE_ENABLE (#0) = 1");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following information;(1)   ‘close’ buttons in Driver ID window is disable.10 seconds later(2)   ‘close’ buttons in Driver ID window is enable");
            /*
            Test Step 5
            Action: Use the test script file 10_2_2_b.xml to disable and enable button via EVC-14 withPacket 1 (disable all button)MMI_Q_CLOSE_ENABLE (#0) = 0Packet 2 (enable all button)MMI_Q_CLOSE_ENABLE (#0) = 1
            Expected Result: Verify the following information;(1)   ‘close’ buttons in Driver ID window is disable.10 seconds later(2)   ‘close’ buttons in Driver ID window is enable
            Test Step Comment: (1) MMI_gen 5646 (partly: Exceptions of Driver ID windows, disable);(2) MMI_gen 5646 (partly: Exceptions of Driver ID windows, enable);
            */
            XML_10_2_2_b_and_c(msgType.typeb);

            TraceHeader("Test Step 6");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Perform the following procedure;Press ‘close’ button (Driver ID window).Press ‘Train Data’ button");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following information;(1)   Verify DMI still displays Main window until Train Data window is displayed.(2)   Verify the close button is always enable");
            /*
            Test Step 6
            Action: Perform the following procedure;Press ‘close’ button (Driver ID window).Press ‘Train Data’ button
            Expected Result: Verify the following information;(1)   Verify DMI still displays Main window until Train Data window is displayed.(2)   Verify the close button is always enable
            Test Step Comment: (1) MMI_gen 8859 (partly: windows in main menu);(2) MMI_gen 5646 (partly: always enable, windows in main menu);
            */
            DmiActions.ShowInstruction(this, @"Press ‘Close’ button in Driver ID window. Press ‘Train Data’ button");

            DmiActions.Send_EVC6_MMICurrentTrainData_FixedDataEntry(this, new[] {"FLU", "RLU", "Rescue"}, 1);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays Main window until Train Data window is displayed." +
                                Environment.NewLine +
                                "2. ‘Close’ button is enabled.");

            TraceHeader("Test Step 7");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Repeat action step 2 with fixed Train Data window");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following information;DMI in the entry state of ‘ST05’.(1)  The hourglass symbol ST05 is displayed.(2)  Verify all buttons and the close button are disable.(3)  The disabled Close button NA12 is display in area G.10 seconds laterDMI in the exit state of ‘ST05’.(4)  The hourglass symbol ST05 is removed.(5)  The state of all buttons is restored according to the last status before script is sent.(6)  The enabled Close button NA11 is display in area G");
            /*
            Test Step 7
            Action: Repeat action step 2 with fixed Train Data window
            Expected Result: Verify the following information;DMI in the entry state of ‘ST05’.(1)  The hourglass symbol ST05 is displayed.(2)  Verify all buttons and the close button are disable.(3)  The disabled Close button NA12 is display in area G.10 seconds laterDMI in the exit state of ‘ST05’.(4)  The hourglass symbol ST05 is removed.(5)  The state of all buttons is restored according to the last status before script is sent.(6)  The enabled Close button NA11 is display in area G
            Test Step Comment: (1) MMI_gen 12018 (partly: windows in main menu);(2) MMI_gen 168 (partly: disabled buttons, windows in main menu); MMI_gen 5646 (partly: State ‘ST05’ button is disabled, windows in main menu); MMI_gen 4395 (partly: close button, disabled, windows in main menu);(3) MMI_gen 4396 (partly: close, NA12, windows in main menu);(4) MMI_gen 5728 (partly: removal, EVC, windows in main menu);(5) MMI_gen 5728 (partly: restore after ST05, windows in main menu);(6) MMI_gen 4396 (partly: close, NA11, windows in main menu);
            */
            XML_10_2_2_a();

            TraceHeader("Test Step 8");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Perform the following procedure;Confirm Train type in Train data window.Press ‘Yes’ button.Press ‘Yes’ button (on keypad)");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following information;(1)   Verify DMI still displays Train data window until Validate Train data window is displayed.(2)  Verify the close button is always enable. (3)   Verify the <Yes> button is always enable");
            /*
            Test Step 8
            Action: Perform the following procedure;Confirm Train type in Train data window.Press ‘Yes’ button.Press ‘Yes’ button (on keypad)
            Expected Result: Verify the following information;(1)   Verify DMI still displays Train data window until Validate Train data window is displayed.(2)  Verify the close button is always enable. (3)   Verify the <Yes> button is always enable
            Test Step Comment: (1) MMI_gen 8859 (partly: windows in main menu);(2) MMI_gen 5646 (partly: always enable, windows in main menu);(3) MMI_gen 5719 (partly: always enable, windows in main menu);
            */
            DmiActions.ShowInstruction(this, "Accept train type in the Train data window");

            DmiActions.ShowInstruction(this, @"Press the ‘Yes’ key");

            DmiActions.Send_EVC10_MMIEchoedTrainData_FixedDataEntry(this, new[] {"FLU", "RLU", "Rescue"});

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Train data window until the Validate Train data window is displayed.");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘Close’ button is enabled." + Environment.NewLine +
                                @"2. The <Yes> button is enabled");

            DmiActions.ShowInstruction(this, @"Press the ‘Yes’ key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘Close’ button is enabled." + Environment.NewLine +
                                @"2. The <Yes> key is enabled");

            TraceHeader("Test Step 9");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Repeat action step 2 with Validate Train Data window");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following information;DMI entry on state of 'ST05'.(1)   The hourglass symbol ST05 is displayed.(2)   Verify all buttons and the close button are disable.(3)  The disabled Close button NA12 is display in area G.(4)  The Input Field is deselected.10 seconds laterDMI in the exit state of ‘ST05’.(5)  The hourglass symbol ST05 is removed.(6)  The state of all buttons is restored according to the last status before script is sent.(7)  The enabled Close button NA11 is display in area G.(8)  The input field is in the ‘Selected’ state");
            /*
            Test Step 9
            Action: Repeat action step 2 with Validate Train Data window
            Expected Result: Verify the following information;DMI entry on state of 'ST05'.(1)   The hourglass symbol ST05 is displayed.(2)   Verify all buttons and the close button are disable.(3)  The disabled Close button NA12 is display in area G.(4)  The Input Field is deselected.10 seconds laterDMI in the exit state of ‘ST05’.(5)  The hourglass symbol ST05 is removed.(6)  The state of all buttons is restored according to the last status before script is sent.(7)  The enabled Close button NA11 is display in area G.(8)  The input field is in the ‘Selected’ state
            Test Step Comment: (1) MMI_gen 12018 (partly: windows in main menu);(2) MMI_gen 168 (partly: disabled buttons, windows in main menu); MMI_gen 5646 (partly: State ‘ST05’ button is disabled, windows in main menu); MMI_gen 5719 (partly: State ‘ST05’ button is disabled, windows in main menu); MMI_gen 4395 (partly: close button, disabled, windows in main menu);(3) MMI_gen 4396 (partly: close, NA12, windows in main menu);(4) MMI_gen 168 (partly: deselect input field, windows in main menu);(5) MMI_gen 5728 (partly: removal, EVC, windows in main menu);(6) MMI_gen 5728 (partly: restore after ST05, windows in main menu);(7) MMI_gen 4396 (partly: close, NA11, windows in main menu);(8) MMI_gen 5728 (partly: input field, windows in main menu)
            */
            XML_10_2_2_a(true);

            TraceHeader("Test Step 10");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Perform the following procedure;Press ‘close’ button (Validate Train Data window).Press ‘Train Data’ button.Press ‘Enter data’ button");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following information;(1)   Verify DMI still displays fixed TDE in Train data window until the window is changed to flexible TDE in Train data window.(2)   Verify the close button is always enable");
            /*
            Test Step 10
            Action: Perform the following procedure;Press ‘close’ button (Validate Train Data window).Press ‘Train Data’ button.Press ‘Enter data’ button
            Expected Result: Verify the following information;(1)   Verify DMI still displays fixed TDE in Train data window until the window is changed to flexible TDE in Train data window.(2)   Verify the close button is always enable
            Test Step Comment: (1) MMI_gen 8859 (partly: windows in main menu);(2) MMI_gen 5646 (partly: always enable, windows in main menu);
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button in the Validate Train data window");

            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Close_current_return_to_parent;
            EVC30_MMIRequestEnable.Send();

            DmiActions.ShowInstruction(this, @"Press the ‘Train Data’ button.");

            DmiActions.Send_EVC6_MMICurrentTrainData(Variables.MMI_M_DATA_ENABLE.TrainLength,
                100,
                120,
                Variables.MMI_NID_KEY.PASS1,
                90,
                Variables.MMI_NID_KEY.CATA,
                0,
                Variables.MMI_NID_KEY_Load_Gauge.G1,
                EVC6_MMICurrentTrainData.MMI_M_BUTTONS_CURRENT_TRAIN_DATA.BTN_YES_DATA_ENTRY_COMPLETE,
                0, 0, new[] {"FLU", "RLU", "Rescue"},
                new Variables.DataElement[] { });

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays fixed TDE in Train data window until the window is changed to flexible TDE in the Train data window." +
                                Environment.NewLine +
                                "2. The ‘Close’ button is enabled.");

            TraceHeader("Test Step 11");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Repeat action step 2 with flexible Train Data window");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following information;DMI in the entry state of ‘ST05’.(1)   The hourglass symbol ST05 is displayed.(2)   Verify all buttons and the close button are disable. (except ‘Navigator’ button)(3)   The disabled Close button NA12 is display in area G.(4)   All Input Field are deselected.10 seconds laterDMI in the exit state of ‘ST05’.(5)   The hourglass symbol ST05 is removed.(6)   The state of all buttons is restored according to the last status before script is sent.(7)   The enabled Close button NA11 is display in area G.(8)   The input field is stated as follows:The first input field is in the ‘Selected’ state.The all others are in the ‘Not selected’ state");
            /*
            Test Step 11
            Action: Repeat action step 2 with flexible Train Data window
            Expected Result: Verify the following information;DMI in the entry state of ‘ST05’.(1)   The hourglass symbol ST05 is displayed.(2)   Verify all buttons and the close button are disable. (except ‘Navigator’ button)(3)   The disabled Close button NA12 is display in area G.(4)   All Input Field are deselected.10 seconds laterDMI in the exit state of ‘ST05’.(5)   The hourglass symbol ST05 is removed.(6)   The state of all buttons is restored according to the last status before script is sent.(7)   The enabled Close button NA11 is display in area G.(8)   The input field is stated as follows:The first input field is in the ‘Selected’ state.The all others are in the ‘Not selected’ state
            Test Step Comment: (1) MMI_gen 12018 (partly: windows in main menu);(2) MMI_gen 168 (partly: disabled buttons, windows in main menu); MMI_gen 5646 (partly: State ‘ST05’ button is disabled, windows in main menu); MMI_gen 4395 (partly: close button, disabled, windows in main menu); Note under the MMI_gen 5728;(3) MMI_gen 4396 (partly: close, NA12, windows in main menu);(4) MMI_gen 168 (partly: deselect input field, windows in main menu);(5) MMI_gen 5728 (partly: removal, EVC, windows in main menu);(6) MMI_gen 5728 (partly: restore after ST05, windows in main menu);(7) MMI_gen 4396 (partly: close, NA11, windows in main menu);(8) MMI_gen 5728 (partly: input field, windows in main menu)
            */
            XML_10_2_2_a();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The first data input field is ‘Selected’." + Environment.NewLine +
                                "2. All other data input fields are ‘Not Selected’.");

            TraceHeader("Test Step 12");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Perform the following procedure;Confirm all value in Train data window.Press ‘Yes’ button.Press ‘Yes’ button (on keypad)");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following information;(1)   Verify DMI still displays Train data window until Validate Train data window is displayed.(2)   Verify the close button is always enable. (3)   Verify the <Yes> button is always enable");
            /*
            Test Step 12
            Action: Perform the following procedure;Confirm all value in Train data window.Press ‘Yes’ button.Press ‘Yes’ button (on keypad)
            Expected Result: Verify the following information;(1)   Verify DMI still displays Train data window until Validate Train data window is displayed.(2)   Verify the close button is always enable. (3)   Verify the <Yes> button is always enable
            Test Step Comment: (1) MMI_gen 8859 (partly: windows in main menu);(2) MMI_gen 5646 (partly: always enable, windows in main menu); (3) MMI_gen 5719 (partly: always enable, windows in main menu);
            */
            DmiActions.ShowInstruction(this,
                @"Accept all values in the Train data window, then press the ‘Yes’ button");

            // This sends the inverted bit map of current state of EVC6 so should work
            DmiActions.Send_EVC10_MMIEchoedTrainData(this, Variables.MMI_M_DATA_ENABLE.TrainLength,
                100,
                120,
                Variables.MMI_NID_KEY.PASS1,
                90,
                Variables.MMI_NID_KEY.CATA,
                0,
                Variables.MMI_NID_KEY.G1,
                new[] {"FLU", "RLU", "Rescue"});

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Train data window until the Validate Train data window is displayed.");

            DmiActions.ShowInstruction(this, @"Press the ‘Yes’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. ‘Close’ button is enabled." + Environment.NewLine +
                                @"2. <Yes> button is enabled.");
            TraceHeader("Test Step 13");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Repeat action step 2 with Validate Train Data window");
            TraceReport("Expected Result");
            TraceInfo("See the expectation in step 9");
            /*
            Test Step 13
            Action: Repeat action step 2 with Validate Train Data window
            Expected Result: See the expectation in step 9
            Test Step Comment: See step 9 for the Validate Train Data window in the Main menu
            */
            XML_10_2_2_a(true);

            // Steps 14 and 15 missing in spec
            TraceHeader("Test Step 16");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Perform the following procedure;Confirm entered data by pressing an input field.Press ‘Level’ button");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following information;(1)   Verify DMI still displays Main window until Level window is displayed.(2)   Verify the close button is always enable");
            /*
            Test Step 16
            Action: Perform the following procedure;Confirm entered data by pressing an input field.Press ‘Level’ button
            Expected Result: Verify the following information;(1)   Verify DMI still displays Main window until Level window is displayed.(2)   Verify the close button is always enable
            Test Step Comment: (1) MMI_gen 8859 (partly: windows in main menu);(2) MMI_gen 5646 (partly: always enable, windows in main menu);
            */
            DmiActions.ShowInstruction(this,
                "Accept entered data by pressing an input field, the press the ‘Level’ button");

            EVC20_MMISelectLevel.MMI_Q_CLOSE_ENABLE = Variables.MMI_Q_CLOSE_ENABLE.Enabled;
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

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Main window until the Level window is displayed." +
                                Environment.NewLine +
                                "2. ‘Close’ button is still enabled.");

            TraceHeader("Test Step 17");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Repeat action step 2 with Level window");
            TraceReport("Expected Result");
            TraceInfo("See the expectation in step 4");
            /*
            Test Step 17
            Action: Repeat action step 2 with Level window
            Expected Result: See the expectation in step 4
            Test Step Comment: See step 4 for the Level window in the Main menu
            */
            XML_10_2_2_a(true);

            TraceHeader("Test Step 18");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Use the test script file 10_2_2_c.xml to disable and enable button via EVC-20 withPacket 1 (disable all button)MMI_Q_CLOSE_ENABLE (#0) = 0Packet 2 (enable all button)MMI_Q_CLOSE_ENABLE (#0) = 1");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following information;(1)   ‘close’ buttons in Level window is disable.10 seconds later(2)   ‘close’ buttons in Level window is enable");
            /*
            Test Step 18
            Action: Use the test script file 10_2_2_c.xml to disable and enable button via EVC-20 withPacket 1 (disable all button)MMI_Q_CLOSE_ENABLE (#0) = 0Packet 2 (enable all button)MMI_Q_CLOSE_ENABLE (#0) = 1
            Expected Result: Verify the following information;(1)   ‘close’ buttons in Level window is disable.10 seconds later(2)   ‘close’ buttons in Level window is enable
            Test Step Comment: (1) MMI_gen 5646 (partly: Exceptions of Level windows, disable);(2) MMI_gen 5646 (partly: Exceptions of Level windows, enable);
            */
            XML_10_2_2_b_and_c(msgType.typec);

            TraceHeader("Test Step 19");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Press ‘L inh’ button");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following information;(1)   Verify DMI still displays Level window until Level inhibition window is displayed.(2)   Verify the close button is always enable");
            /*
            Test Step 19
            Action: Press ‘L inh’ button
            Expected Result: Verify the following information;(1)   Verify DMI still displays Level window until Level inhibition window is displayed.(2)   Verify the close button is always enable
            Test Step Comment: (1) MMI_gen 8859 (partly: windows in main menu);(2) MMI_gen 5646 (partly: always enable, windows in main menu);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press ‘L inh’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Level window until the Level inhibition window is displayed." +
                                Environment.NewLine +
                                "2. ‘Close’ button is enabled.");

            TraceHeader("Test Step 20");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Repeat action step 2 with Level inhibition window");
            TraceReport("Expected Result");
            TraceInfo("See the expectation in step 4");
            /*
            Test Step 20
            Action: Repeat action step 2 with Level inhibition window
            Expected Result: See the expectation in step 4
            Test Step Comment: See step 4 for Train running number window in the Main menu
            */
            XML_10_2_2_a(true);

            TraceHeader("Test Step 21");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Perform the following procedure;Press ‘close’ button (Level window).Press ‘Train running number’ button");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following information;(1)   Verify DMI still displays Main window until Train running number window is displayed.(2)   Verify the close button is always enable");
            /*
            Test Step 21 Action: Perform the following procedure;Press ‘close’ button (Level window).Press ‘Train running number’ button
            Expected Result: Verify the following information;(1)   Verify DMI still displays Main window until Train running number window is displayed.(2)   Verify the close button is always enable
            Test Step Comment: (1) MMI_gen 8859 (partly: windows in main menu);(2) MMI_gen 5646 (partly: always enable, windows in main menu);        
            */
            DmiActions.ShowInstruction(this,
                @"Confirm the data in the Level inhibition window, then press the ‘Close’ button in the Level window." +
                Environment.NewLine +
                @"Press the ‘Train running number’ button");

            EVC16_CurrentTrainNumber.TrainRunningNumber = 1;
            EVC16_CurrentTrainNumber.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Main window until the Train running number window is displayed.");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. ‘Close’ button is enabled.");

            TraceHeader("Test Step 22");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Repeat action step 2 with Train running number window");
            TraceReport("Expected Result");
            TraceInfo("See the expectation in step 4");
            /*
            Test Step 22
            Action: Repeat action step 2 with Train running number window
            Expected Result: See the expectation in step 4
            Test Step Comment: See step 4 for Train running number window in the Main menu
            */
            XML_10_2_2_a(true);

            TraceHeader("Test Step 23");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("End of test");
            TraceReport("Expected Result");
            TraceInfo("");
            /*
            Test Step 23
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }

        #region Send_XML_10_2_2_a_DMI_Test_Specification

        private void XML_10_2_2_a(bool dataInputSelected = false)
        {
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
                                "3. The hourglass symbol ST05 moves to the right every second." + Environment.NewLine +
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
                                "4. ‘Close’ button NA11 is displayed enabled in area G." +
                                (dataInputSelected
                                    ? Environment.NewLine + "5. The data input field is ‘Selected’."
                                    : ""));
        }

        #endregion

        #region Send_XML_10_2_2_b_and_c_DMI_Test_Specification

        private enum msgType
        {
            typeb,
            typec
        };

        private void XML_10_2_2_b_and_c(msgType msgtype)
        {
            if (msgtype == msgType.typeb)
            {
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

                WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                    "1. ‘Close’ button in Level window is disabled.");

                Wait_Realtime(1000);

                EVC20_MMISelectLevel.MMI_Q_CLOSE_ENABLE = Variables.MMI_Q_CLOSE_ENABLE.Enabled;
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

                WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                    "1. ‘Close’ button in Level window is enabled.");
            }
            else if (msgtype == msgType.typec)
            {
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

                WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                    "1. ‘Close’ button in Level window is disabled.");

                Wait_Realtime(1000);

                EVC20_MMISelectLevel.MMI_Q_CLOSE_ENABLE = Variables.MMI_Q_CLOSE_ENABLE.Enabled;
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

                WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                    "1. ‘Close’ button in Level window is enabled.");
            }
        }

        #endregion
    }
}
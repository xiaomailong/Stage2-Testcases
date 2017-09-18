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
            // Testcase entrypoint


            /*
            Test Step 1
            Action: Perform SoM until select and confirm Level 1
            Expected Result: DMI displays Main window.(1)   Verify the close button is always enable
            Test Step Comment: (1) MMI_gen 5646 (partly: always enable, Main window);
            */
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays Main Window." + Environment.NewLine +
                                "2. ‘Close’ button is enabled.");

            /*
            Test Step 2
            Action: Use the test script file 10_2_2_a.xml to disable and enable button of the Main window via EVC-8 withPacket 1 (Entry state of ‘ST05’)MMI_Q_TEXT_CRITERIA = 3 MMI_Q_TEXT = 716Packet 2 (Exit state of ‘ST05’)MMI_Q_TEXT_CRITERIA = 4MMI_Q_TEXT = 716Note: Stopwatch is required for accuracy of test result
            Expected Result: Verify the following information;DMI in the entry state of ‘ST05’.(1)   The hourglass symbol ST05 is displayed at window title area.(2)   The hourglass symbol ST05 is vertically aligned center of the window title area.(3)   The symbol ST05 is move to the right every second.(4)   After symbol ST05 is moved to the end of the window title area, the symbol comes back to the first position and keeps moving to the right. (5)   Verify all buttons and the close button are disable.(6)   The disabled Close button NA12 is display in area G.10 seconds later(7)   DMI in the exit state of ‘ST05’.(8)   The hourglass symbol ST05 is removed.(9)   The state of all buttons is restored according to the last status before script is sent.(10) The enabled Close button NA11 is display in area G
            Test Step Comment: (1) MMI_gen 12018 (partly: Main window); MMI_gen 8355 (partly: EVC-8, Main window);(2) MMI_gen 8355 (partly: vertically centered, Main window);(3) MMI_gen 8355 (partly: Move to the right every second, Main window);(4) MMI_gen 8355 (partly: no more possible to display, Main window);(5) MMI_gen 168 (partly: disabled buttons, Main window); MMI_gen 5646 (partly: State ‘ST05’ button is disabled, Main window); MMI_gen 4395 (partly: close button, disabled, Main window); (6) MMI_gen 4396 (partly: close, NA12, Main window);(7) MMI_gen 5728 (partly: removal, EVC, Main window);(8) MMI_gen 5728 (partly: restore after ST05, Main window);(9) MMI_gen 4396 (partly: close, NA11, Main window);
            */
            XML.XML_10_2_2_a.Send(this);

            /*
            Test Step 3
            Action: Press ‘Driver ID’ button
            Expected Result: Verify the following information;(1)   Verify DMI still displays Main window until Driver ID window is displayed.(2)   Verify the close button is always enable
            Test Step Comment: (1) MMI_gen 8859 (partly: windows in main menu);(2) MMI_gen 5646 (partly: always enable, windows in main menu);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press ‘Driver ID’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays Main window until Driver ID window is displayed." + Environment.NewLine +
                                "2. ‘Close’ button is always enabled.");

            /*
            Test Step 4
            Action: Repeat action step 2 with Driver ID window
            Expected Result: Verify the following information;DMI in the entry state of ‘ST05’.(1)   The hourglass symbol ST05 is displayed.(2)   Verify all buttons and the close button are disable.(3)   The disabled Close button NA12 is display in area G.(4)   The Input Field is deselected.10 seconds laterDMI in the exit state of ‘ST05’.(5)   The hourglass symbol ST05 is removed.(6)  The state of all buttons is restored according to the last status before script is sent.(7)  The enabled Close button NA11 is display in area G.(8)   The input field is in the ‘Selected’ state
            Test Step Comment: (1) MMI_gen 12018 (partly: windows in main menu);(2) MMI_gen 168 (partly: disabled buttons, windows in main menu); MMI_gen 5646 (partly: State ‘ST05’ button is disabled, windows in main menu); MMI_gen 4395 (partly: close button, disabled, windows in main menu);(3) MMI_gen 4396 (partly: close, NA12, windows in main menu);(4) MMI_gen 168 (partly: deselect input field, windows in main menu);(5) MMI_gen 5728 (partly: removal, EVC, windows in main menu);(6) MMI_gen 5728 (partly: restore after ST05, windows in main menu);(7) MMI_gen 4396 (partly: close, NA11, windows in main menu);(8) MMI_gen 5728 (partly: input field, windows in main menu)
            */
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;

            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                      "1. DMI is in the entry state of ‘ST05’." + Environment.NewLine +
                                      "2. The hourglass symbol ST05 is displayed" + Environment.NewLine +
                                      "3. All buttons and the ‘Close’ button are disabled." + Environment.NewLine +
                                      "4. ‘Close’ button NA12 is displayed disabled in area G." + Environment.NewLine +
                                      "5. The Input Field is not selected.");

            this.Wait_Realtime(10000);
            
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 4;

            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI is in the exit state of ‘ST05’." + Environment.NewLine +
                                "2. The hourglass symbol ST05 is removed." + Environment.NewLine +
                                "3. All buttons are enabled." + Environment.NewLine +
                                "4. ‘Close’ button NA11 is displayed enabled in area G." + Environment.NewLine +
                                "5. The Input Field is selected.");

            /*
            Test Step 5
            Action: Use the test script file 10_2_2_b.xml to disable and enable button via EVC-14 withPacket 1 (disable all button)MMI_Q_CLOSE_ENABLE (#0) = 0Packet 2 (enable all button)MMI_Q_CLOSE_ENABLE (#0) = 1
            Expected Result: Verify the following information;(1)   ‘close’ buttons in Driver ID window is disable.10 seconds later(2)   ‘close’ buttons in Driver ID window is enable
            Test Step Comment: (1) MMI_gen 5646 (partly: Exceptions of Driver ID windows, disable);(2) MMI_gen 5646 (partly: Exceptions of Driver ID windows, enable);
            */
            XML.XML_10_2_2_b.Send(this);

            /*
            Test Step 6
            Action: Perform the following procedure;Press ‘close’ button (Driver ID window).Press ‘Train Data’ button
            Expected Result: Verify the following information;(1)   Verify DMI still displays Main window until Train Data window is displayed.(2)   Verify the close button is always enable
            Test Step Comment: (1) MMI_gen 8859 (partly: windows in main menu);(2) MMI_gen 5646 (partly: always enable, windows in main menu);
            */
            DmiActions.ShowInstruction(this, @"Press ‘Close’ button in Driver ID window. Press ‘Train Data’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays Main window until Train Data window is displayed." + Environment.NewLine +
                                "2. ‘Close’ button is enabled.");

            /*
            Test Step 7
            Action: Repeat action step 2 with fixed Train Data window
            Expected Result: Verify the following information;DMI in the entry state of ‘ST05’.(1)  The hourglass symbol ST05 is displayed.(2)  Verify all buttons and the close button are disable.(3)  The disabled Close button NA12 is display in area G.10 seconds laterDMI in the exit state of ‘ST05’.(4)  The hourglass symbol ST05 is removed.(5)  The state of all buttons is restored according to the last status before script is sent.(6)  The enabled Close button NA11 is display in area G
            Test Step Comment: (1) MMI_gen 12018 (partly: windows in main menu);(2) MMI_gen 168 (partly: disabled buttons, windows in main menu); MMI_gen 5646 (partly: State ‘ST05’ button is disabled, windows in main menu); MMI_gen 4395 (partly: close button, disabled, windows in main menu);(3) MMI_gen 4396 (partly: close, NA12, windows in main menu);(4) MMI_gen 5728 (partly: removal, EVC, windows in main menu);(5) MMI_gen 5728 (partly: restore after ST05, windows in main menu);(6) MMI_gen 4396 (partly: close, NA11, windows in main menu);
            */
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;

            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                      "1. DMI is in the entry state of ‘ST05’." + Environment.NewLine +
                                      "2. The hourglass symbol ST05 is displayed" + Environment.NewLine +
                                      "3. All buttons and the ‘Close’ button are disabled." + Environment.NewLine +
                                      "4. ‘Close’ button NA12 is displayed disabled in area G.");

            this.Wait_Realtime(10000);

            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 4;

            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI is in the exit state of ‘ST05’." + Environment.NewLine +
                                "2. The hourglass symbol ST05 is removed." + Environment.NewLine +
                                "3. All buttons are enabled." + Environment.NewLine +
                                "4. ‘Close’ button NA11 is displayed enabled in area G.");

            /*
            Test Step 8
            Action: Perform the following procedure;Confirm Train type in Train data window.Press ‘Yes’ button.Press ‘Yes’ button (on keypad)
            Expected Result: Verify the following information;(1)   Verify DMI still displays Train data window until Validate Train data window is displayed.(2)  Verify the close button is always enable. (3)   Verify the <Yes> button is always enable
            Test Step Comment: (1) MMI_gen 8859 (partly: windows in main menu);(2) MMI_gen 5646 (partly: always enable, windows in main menu);(3) MMI_gen 5719 (partly: always enable, windows in main menu);
            */
            DmiActions.ShowInstruction(this, "Accept train type in the Train data window");
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Train data window until the Validate Train data window is displayed.");

            DmiActions.ShowInstruction(this, @"Press the ‘Yes’ button");
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘Close’ button is enabled." + Environment.NewLine +
                                @"2. The <Yes> button is enabled");

            DmiActions.ShowInstruction(this, @"Press the ‘Yes’ button on the keypad");
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘Close’ button is enabled." + Environment.NewLine +
                                @"2. The <Yes> button is enabled");

            /*
            Test Step 9
            Action: Repeat action step 2 with Validate Train Data window
            Expected Result: Verify the following information;DMI entry on state of 'ST05'.(1)   The hourglass symbol ST05 is displayed.(2)   Verify all buttons and the close button are disable.(3)  The disabled Close button NA12 is display in area G.(4)  The Input Field is deselected.10 seconds laterDMI in the exit state of ‘ST05’.(5)  The hourglass symbol ST05 is removed.(6)  The state of all buttons is restored according to the last status before script is sent.(7)  The enabled Close button NA11 is display in area G.(8)  The input field is in the ‘Selected’ state
            Test Step Comment: (1) MMI_gen 12018 (partly: windows in main menu);(2) MMI_gen 168 (partly: disabled buttons, windows in main menu); MMI_gen 5646 (partly: State ‘ST05’ button is disabled, windows in main menu); MMI_gen 5719 (partly: State ‘ST05’ button is disabled, windows in main menu); MMI_gen 4395 (partly: close button, disabled, windows in main menu);(3) MMI_gen 4396 (partly: close, NA12, windows in main menu);(4) MMI_gen 168 (partly: deselect input field, windows in main menu);(5) MMI_gen 5728 (partly: removal, EVC, windows in main menu);(6) MMI_gen 5728 (partly: restore after ST05, windows in main menu);(7) MMI_gen 4396 (partly: close, NA11, windows in main menu);(8) MMI_gen 5728 (partly: input field, windows in main menu)
            */
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;

            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI is in the entry state of ‘ST05’." + Environment.NewLine +
                                "2. The hourglass symbol ST05 is displayed" + Environment.NewLine +
                                "3. All buttons and the ‘Close’ button are disabled." + Environment.NewLine +
                                "4. ‘Close’ button NA12 is displayed disabled in area G." + Environment.NewLine +
                                "5. The input field is not selected.");

            this.Wait_Realtime(10000);

            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 4;

            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI is in the exit state of ‘ST05’." + Environment.NewLine +
                                "2. The hourglass symbol ST05 is removed." + Environment.NewLine +
                                "3. All buttons are enabled." + Environment.NewLine +
                                "4. ‘Close’ button NA11 is displayed enabled in area G." + Environment.NewLine +
                                "5. The input field is selected.");

            /*
            Test Step 10
            Action: Perform the following procedure;Press ‘close’ button (Validate Train Data window).Press ‘Train Data’ button.Press ‘Enter data’ button
            Expected Result: Verify the following information;(1)   Verify DMI still displays fixed TDE in Train data window until the window is changed to flexible TDE in Train data window.(2)   Verify the close button is always enable
            Test Step Comment: (1) MMI_gen 8859 (partly: windows in main menu);(2) MMI_gen 5646 (partly: always enable, windows in main menu);
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ but     ton in the Validate Train data window");
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays fixed TDE in Train data window until the window is changed to flexible TDE in Train data window.");       

            DmiActions.ShowInstruction(this, @"Press the ‘Train Data’ button.");
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Close’ button is enabled.");

            DmiActions.ShowInstruction(this, @"Press the ‘Enter Data’ button.");
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Close’ button is enabled.");

            /*
            Test Step 11
            Action: Repeat action step 2 with flexible Train Data window
            Expected Result: Verify the following information;DMI in the entry state of ‘ST05’.(1)   The hourglass symbol ST05 is displayed.(2)   Verify all buttons and the close button are disable. (except ‘Navigator’ button)(3)   The disabled Close button NA12 is display in area G.(4)   All Input Field are deselected.10 seconds laterDMI in the exit state of ‘ST05’.(5)   The hourglass symbol ST05 is removed.(6)   The state of all buttons is restored according to the last status before script is sent.(7)   The enabled Close button NA11 is display in area G.(8)   The input field is stated as follows:The first input field is in the ‘Selected’ state.The all others are in the ‘Not selected’ state
            Test Step Comment: (1) MMI_gen 12018 (partly: windows in main menu);(2) MMI_gen 168 (partly: disabled buttons, windows in main menu); MMI_gen 5646 (partly: State ‘ST05’ button is disabled, windows in main menu); MMI_gen 4395 (partly: close button, disabled, windows in main menu); Note under the MMI_gen 5728;(3) MMI_gen 4396 (partly: close, NA12, windows in main menu);(4) MMI_gen 168 (partly: deselect input field, windows in main menu);(5) MMI_gen 5728 (partly: removal, EVC, windows in main menu);(6) MMI_gen 5728 (partly: restore after ST05, windows in main menu);(7) MMI_gen 4396 (partly: close, NA11, windows in main menu);(8) MMI_gen 5728 (partly: input field, windows in main menu)
            */
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;

            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                      "1. DMI is in the entry state of ‘ST05’." + Environment.NewLine +
                                      "2. The hourglass symbol ST05 is displayed" + Environment.NewLine +
                                      "3. All buttons and the ‘Close’ button are disabled except the ‘Navigator’ button ." + Environment.NewLine +
                                      "4. ‘Close’ button NA12 is displayed disabled in area G." + Environment.NewLine +
                                      "5. All Input Fields are not selected");

            this.Wait_Realtime(10000);

            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 4;

            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI is in the exit state of ‘ST05’." + Environment.NewLine +
                                "2. The hourglass symbol ST05 is removed." + Environment.NewLine +
                                "3. All buttons are enabled." + Environment.NewLine +
                                "4. ‘Close’ button NA11 is displayed enabled in area G." + Environment.NewLine +
                                "5. The first Input Field is selected." + Environment.NewLine +
                                "6. All other Input Fields are not selected.");

            /*
            Test Step 12
            Action: Perform the following procedure;Confirm all value in Train data window.Press ‘Yes’ button.Press ‘Yes’ button (on keypad)
            Expected Result: Verify the following information;(1)   Verify DMI still displays Train data window until Validate Train data window is displayed.(2)   Verify the close button is always enable. (3)   Verify the <Yes> button is always enable
            Test Step Comment: (1) MMI_gen 8859 (partly: windows in main menu);(2) MMI_gen 5646 (partly: always enable, windows in main menu); (3) MMI_gen 5719 (partly: always enable, windows in main menu);
            */
            DmiActions.ShowInstruction(this, @"Accept all values in the Train data window");
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Train data window until the Validate Train data window is displayed.");

            DmiActions.ShowInstruction(this, @"Press the ‘Yes’ button");
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. ‘Close’ button is enabled." + Environment.NewLine +
                                @"2. <Yes> button is enabled.");
            /*
            Test Step 13
            Action: Repeat action step 2 with Validate Train Data window
            Expected Result: See the expectation in step 9
            Test Step Comment: See step 9 for the Validate Train Data window in the Main menu
            */
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;

            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI is in the entry state of ‘ST05’." + Environment.NewLine +
                                "2. The hourglass symbol ST05 is displayed" + Environment.NewLine +
                                "3. All buttons and the ‘Close’ button are disabled." + Environment.NewLine +
                                "4. ‘Close’ button NA12 is displayed disabled in area G.");

            this.Wait_Realtime(10000);

            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 4;

            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI is in the exit state of ‘ST05’." + Environment.NewLine +
                                "2. The hourglass symbol ST05 is removed." + Environment.NewLine +
                                "3. All buttons are enabled." + Environment.NewLine +
                                "4. ‘Close’ button NA11 is displayed enabled in area G." + Environment.NewLine +
                                "5. The input field is selected.");

            // Steps 14 and 15 missing in spec
            /*
            Test Step 16
            Action: Perform the following procedure;Confirm entered data by pressing an input field.Press ‘Level’ button
            Expected Result: Verify the following information;(1)   Verify DMI still displays Main window until Level window is displayed.(2)   Verify the close button is always enable
            Test Step Comment: (1) MMI_gen 8859 (partly: windows in main menu);(2) MMI_gen 5646 (partly: always enable, windows in main menu);
            */
            DmiActions.ShowInstruction(this, "Accept entered data by pressing an input field");
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Main window until the Level window is displayed.");

            DmiActions.ShowInstruction(this, @"Press ‘Level’ button");
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. ‘Close’ button is still enabled.");

            /*
            Test Step 17
            Action: Repeat action step 2 with Level window
            Expected Result: See the expectation in step 4
            Test Step Comment: See step 4 for the Level window in the Main menu
            */
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;

            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                      "1. DMI is in the entry state of ‘ST05’." + Environment.NewLine +
                                      "2. The hourglass symbol ST05 is displayed" + Environment.NewLine +
                                      "3. All buttons and the ‘Close’ button are disabled." + Environment.NewLine +
                                      "4. ‘Close’ button NA12 is displayed disabled in area G." + Environment.NewLine +
                                      "5. The Input Field is not selected.");

            this.Wait_Realtime(10000);

            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 4;

            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI is in the exit state of ‘ST05’." + Environment.NewLine +
                                "2. The hourglass symbol ST05 is removed." + Environment.NewLine +
                                "3. All buttons are enabled." + Environment.NewLine +
                                "4. ‘Close’ button NA11 is displayed enabled in area G." + Environment.NewLine +
                                "5. The Input Field is selected.");

            /*
            Test Step 18
            Action: Use the test script file 10_2_2_c.xml to disable and enable button via EVC-20 withPacket 1 (disable all button)MMI_Q_CLOSE_ENABLE (#0) = 0Packet 2 (enable all button)MMI_Q_CLOSE_ENABLE (#0) = 1
            Expected Result: Verify the following information;(1)   ‘close’ buttons in Level window is disable.10 seconds later(2)   ‘close’ buttons in Level window is enable
            Test Step Comment: (1) MMI_gen 5646 (partly: Exceptions of Level windows, disable);(2) MMI_gen 5646 (partly: Exceptions of Level windows, enable);
            */
            XML.XML_10_2_2_c.Send(this);

            /*
            Test Step 19
            Action: Press ‘L inh’ button
            Expected Result: Verify the following information;(1)   Verify DMI still displays Level window until Level inhibition window is displayed.(2)   Verify the close button is always enable
            Test Step Comment: (1) MMI_gen 8859 (partly: windows in main menu);(2) MMI_gen 5646 (partly: always enable, windows in main menu);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press ‘L inh’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Level window until the Level inhibition window is displayed." + Environment.NewLine +
                                "2. ‘Close’ button is enabled.");  

            /*
            Test Step 20
            Action: Repeat action step 2 with Train running number window
            Expected Result: See the expectation in step 4
            Test Step Comment: See step 4 for Train running number window in the Main menu
            */
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;

            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                      "1. DMI is in the entry state of ‘ST05’." + Environment.NewLine +
                                      "2. The hourglass symbol ST05 is displayed" + Environment.NewLine +
                                      "3. All buttons and the ‘Close’ button are disabled." + Environment.NewLine +
                                      "4. ‘Close’ button NA12 is displayed disabled in area G." + Environment.NewLine +
                                      "5. The Input Field is not selected.");

            this.Wait_Realtime(10000);

            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 4;

            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI is in the exit state of ‘ST05’." + Environment.NewLine +
                                "2. The hourglass symbol ST05 is removed." + Environment.NewLine +
                                "3. All buttons are enabled." + Environment.NewLine +
                                "4. ‘Close’ button NA11 is displayed enabled in area G." + Environment.NewLine +
                                "5. The Input Field is selected.");


            DmiActions.ShowInstruction(this, @"Press ‘L inh’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Level window until the Level inhibition window is displayed." + Environment.NewLine +
                                "2. ‘Close’ button is enabled.");

            /*
            Test Step 21 Action: Perform the following procedure;Press ‘close’ button (Level window).Press ‘Train running number’ button
            Expected Result: Verify the following information;(1)   Verify DMI still displays Main window until Train running number window is displayed.(2)   Verify the close button is always enable
            Test Step Comment: (1) MMI_gen 8859 (partly: windows in main menu);(2) MMI_gen 5646 (partly: always enable, windows in main menu);        
            */
            DmiActions.ShowInstruction(this, @"Press ‘Close’ button in the Level window");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Main window until the Train running number window is displayed.");

            DmiActions.ShowInstruction(this, @"Press ‘Train running number’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. ‘Close’ button is enabled.");

            /*
            Test Step 22
            Action: Repeat action step 2 with Train running number window
            Expected Result: See the expectation in step 4
            Test Step Comment: See step 4 for Train running number window in the Main menu
            */
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;

            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                      "1. DMI is in the entry state of ‘ST05’." + Environment.NewLine +
                                      "2. The hourglass symbol ST05 is displayed" + Environment.NewLine +
                                      "3. All buttons and the ‘Close’ button are disabled." + Environment.NewLine +
                                      "4. ‘Close’ button NA12 is displayed disabled in area G." + Environment.NewLine +
                                      "5. The Input Field is not selected.");

            this.Wait_Realtime(10000);

            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 4;

            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI is in the exit state of ‘ST05’." + Environment.NewLine +
                                "2. The hourglass symbol ST05 is removed." + Environment.NewLine +
                                "3. All buttons are enabled." + Environment.NewLine +
                                "4. ‘Close’ button NA11 is displayed enabled in area G." + Environment.NewLine +
                                "5. The Input Field is selected.");

            /*
            Test Step 23
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
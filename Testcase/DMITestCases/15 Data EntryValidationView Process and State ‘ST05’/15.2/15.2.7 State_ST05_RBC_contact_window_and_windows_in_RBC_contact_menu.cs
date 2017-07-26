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
    public class State_ST05_RBC_contact_window_and_windows_in_RBC_contact_menu : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // Test system is powered onCabin is active

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
            Action: Perform SoM until select and confirm Level 2.
            Expected Result: Verify the following information;(1)   Verify DMI still displays Level window until RBC contact window is displayed.
            Test Step Comment: (1) MMI_gen 8859 (partly: RBC contact window);
            */

            /*
            Test Step 2
            Action: Use the test script file 10_2_7_a.xml to disable and enable button of the RBC contact window via EVC-8 withPacket 1 (Entry state of ‘ST05’)MMI_Q_TEXT_CRITERIA = 3 MMI_Q_TEXT = 716Packet 2 (Exit state of ‘ST05’)MMI_Q_TEXT_CRITERIA = 4MMI_Q_TEXT = 716
            Expected Result: Verify the following information;DMI in the entry state of ‘ST05’(1)    The hourglass symbol ST05 is displayed at window title area.(2)    The hourglass symbol ST05 is vertically aligned center of the window title area.(3)    The symbol ST05 is move to the right every second.(4)    After symbol ST05 is moved to the end of the window title area, the symbol comes back to the first position and keeps moving to the right. (5)    Verify all buttons and the close button is disable.(6)    The disabled Close button NA12 is display in area G.10 seconds laterDMI in the exit state of ‘ST05’(7)   The hourglass symbol ST05 is removed.(8)   The state of all buttons is restored according to the last status before script is sent.(9)   The enabled Close button NA11 is display in area G.
            Test Step Comment: (1) MMI_gen 12018 (partly: RBC contact window);(2) MMI_gen 8521 (partly: vertically centered, RBC contact window);(3) MMI_gen 8521 (partly: Move to the right every second, RBC contact window);(4) MMI_gen 8521 (partly: no more possible to display, RBC contact window);(5) MMI_gen 168 (partly: disabled buttons, RBC contact window); MMI_gen 5646 (partly: State ‘ST05’ button is disabled, RBC contact window); MMI_gen 4395 (partly: close button, disabled, RBC contact window);(6) MMI_gen 4396 (partly: close, NA12, RBC contact window);(7) MMI_gen 5728 (partly: removal, EVC, RBC contact window);(8) MMI_gen 5728 (partly: restore after ST05, RBC contact window);(9) MMI_gen 4396 (partly: close, NA11, RBC contact window);
            */

            /*
            Test Step 3
            Action: Use the test script file 10_2_7_b.xml to disable and enable button via EVC-22 withPacket 1 (disable all button)MMI_Q_CLOSE_ENABLE (#0) = 1Packet 2 (enable all button)MMI_Q_CLOSE_ENABLE (#0) = 0Note: Stopwatch is required for accuracy of test result.
            Expected Result: Verify the following information;(1)   ‘close’ buttons in RBC contact window is enable.10 seconds later(2)   ‘close’ buttons in Level window is disable.
            Test Step Comment: (1) MMI_gen 5646 (partly: Exceptions of RBC contact window, enable);(2) MMI_gen 5646 (partly: Exceptions of RBC contact windows, disable);
            */

            /*
            Test Step 4
            Action: Perform the following procedure;Press and hold ‘Radio network ID’ button at least 2 secondsRelease ‘Radio network ID’ button 
            Expected Result: Verify the following information;(1)   Verify DMI still displays RBC contact window until Radio network ID window is displayed.(2)   Verify the close button is always enable.
            Test Step Comment: (1) MMI_gen 8859 (partly: windows in RBC contact menu);(2) MMI_gen 5646 (partly: always enable, windows in RBC contact menu);
            */

            /*
            Test Step 5
            Action: Repeat action step 2 with Radio network ID window.
            Expected Result: Verify the following information;DMI entry on state of 'ST05'(1)   The hourglass symbol ST05 is displayed.(2)   Verify all buttons and the close button is disable.(3)   The disabled Close button NA12 is display in area G.(4)   The Input Field is deselected.10 seconds laterDMI exit on state of 'ST05'(5)   The hourglass symbol ST05 is removed.(6)   The state of all buttons is restored according to the last status before script is sent.(7)   The enabled Close button NA11 is display in area G.(8)   The input field is in the ‘Selected’ state.
            Test Step Comment: (1) MMI_gen 12018 (partly: windows in RBC contact menu);(2) MMI_gen 168 (partly: disabled buttons, windows in RBC contact menu); MMI_gen 5646 (partly: State ‘ST05’ button is disabled, windows in RBC contact menu); MMI_gen 4395 (partly: close button, disabled, windows in RBC contact menu);(3) MMI_gen 4396 (partly: close, NA12, windows in RBC contact menu);(4) MMI_gen 168 (partly: deselect input field, windows in RBC contact menu);(5) MMI_gen 5728 (partly: removal, EVC, windows in RBC contact menu);(6) MMI_gen 5728 (partly: restore after ST05, windows in RBC contact menu);(7) MMI_gen 4396 (partly: close, NA11, windows in RBC contact menu);(8) MMI_gen 5728 (partly: input field, windows in RBC contact menu);
            */

            /*
            Test Step 6
            Action: Perform the following procedure;Press ‘close’ button (Radio network ID window) Press ‘Enter RBC data’ button
            Expected Result: Verify the following information;(1)   Verify DMI still displays RBC contact window until RBC data window is displayed.(2)   Verify the close button is always enable.
            Test Step Comment: (1) MMI_gen 8859 (partly: windows in RBC contact menu);(2) MMI_gen 5646 (partly: always enable, windows in RBC contact menu);
            */

            /*
            Test Step 7
            Action: Repeat action step 2 with RBC data window.
            Expected Result: Verify the following information;DMI entry on state of 'ST05'(1)   The hourglass symbol ST05 is displayed.(2)   Verify all buttons and the close button is disable.(3)   The disabled Close button NA12 is display in area G.(4)   All Input Field are deselected.10 seconds laterDMI exit on state of 'ST05'(5)   The hourglass symbol ST05 is removed.(6)   The state of all buttons is restored according to the last status before script is sent.(7)   The enabled Close button NA11 is display in area G.(8)   The input field is stated as follows:The first input field is in the ‘Selected’ state.The all others are in the ‘Not selected’ state
            Test Step Comment: (1) MMI_gen 12018 (partly: windows in RBC contact menu);(2) MMI_gen 168 (partly: disabled buttons, windows in RBC contact menu); MMI_gen 5646 (partly: State ‘ST05’ button is disabled, windows in RBC contact menu); MMI_gen 4395 (partly: close button, disabled, windows in RBC contact menu);(3) MMI_gen 4396 (partly: close, NA12, windows in RBC contact menu);(4) MMI_gen 168 (partly: deselect input field, windows in RBC contact menu);(5) MMI_gen 5728 (partly: removal, EVC, windows in RBC contact menu);(6) MMI_gen 5728 (partly: restore after ST05, windows in RBC contact menu);(7) MMI_gen 4396 (partly: close, NA11, windows in RBC contact menu);(8) MMI_gen 5728 (partly: input field, windows in RBC contact menu);
            */

            /*
            Test Step 8
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
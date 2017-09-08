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
    /// 15.2.4 State 'ST05': Data view window
    /// TC-ID: 10.2.4
    /// 
    /// This test case verifies the buttons of Data view window when entry and exit on state of 'ST05'.
    /// 
    /// Tested Requirements:
    /// MMI_gen 12018 (partly: Data view window); MMI_gen 168 (partly: disabled buttons, Data view window); MMI_gen 4395 (partly: close button, disabled, Data view window); MMI_gen 4396 (partly: close, NA11, NA12, Data view window); MMI_gen 5646 (partly: always enable, State 'ST05' button is disabled, Data view window); MMI_gen 5728 (partly: restore after ST05, removal, EVC, Data view window); MMI_gen 8859 (partly: Data view window);
    /// 
    /// Scenario:
    /// 1.The ‘Data view’ window is displayed.
    /// 2.Use the test script files to send packets in order to verify state ‘ST05’ in a menu window. 
    /// 
    /// Used files:
    /// 10_2_4_a.xml
    /// </summary>
    public class TC_ID_10_2_4_State : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:

            // Call the TestCaseBase PreExecution
            base.PreExecution();
            // Test system is powered onCabin is activePerform SoM until select and confirm Level 1.
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
            Action: Perform the following procedure;Press ‘close’ button (Data view window) Press ‘Data View’ button
            Expected Result: Verify the following information;(1)   Verify DMI still displays Default window until Data View window is displayed.(2)   Verify the close button is always enable
            Test Step Comment: (1) MMI_gen 8859 (partly: Data view window);(2) MMI_gen 5646 (partly: always enable, Data view window)
            */
            DmiActions.ShowInstruction(this, @"Press ‘Close’ button in the Data View window. Press ‘Data View’ button.");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays Default window until Data View window is displayed." + Environment.NewLine +
                                "2. ‘Close’ button is always enabled.");

            /*
            Test Step 2
            Action: Use the test script file 10_2_4_a.xml to disable and enable button via EVC-8 withPacket 1 (Entry state of ‘ST05’)MMI_Q_TEXT_CRITERIA = 3 MMI_Q_TEXT = 716Packet 2 (Exit state of ‘ST05’)MMI_Q_TEXT_CRITERIA = 4MMI_Q_TEXT = 716
            Expected Result: Verify the following information;DMI in the entry state of ‘ST05’(1)   The hourglass symbol ST05 is displayed.(2)   Verify all buttons and the close button is disable.(3)   The disabled Close button NA12 is display in area G.10 seconds laterDMI in the exit state of ‘ST05’(4)   The hourglass symbol ST05 is removed.(5)   The state of all buttons is restored according to the last status before script is sent.(6)   The enabled Close button NA11 is display in area G
            Test Step Comment: (1) MMI_gen 12018 (partly: Data view window);(2) MMI_gen 168 (partly: disabled buttons, Data view window); MMI_gen 5646 (partly: State 'ST05' button is disabled, Data view window); MMI_gen 4395 (partly: close button, disabled, Data view window);(3) MMI_gen 4396 (partly: close, NA12, Data view window);(4) MMI_gen 5728(partly: removal, EVC, Data view window);(5) MMI_gen 5728 (partly: restore after ST05, Data view window);(6) MMI_gen 4396 (partly: close, NA11, Data view window);
            */
            // Call generic Check Results Method
            XML.XML_10_2_4_a.Send(this);

            /*
            Test Step 3
            Action: End of test
            Expected Result: 
            */


            return GlobalTestResult;
        }
    }
}
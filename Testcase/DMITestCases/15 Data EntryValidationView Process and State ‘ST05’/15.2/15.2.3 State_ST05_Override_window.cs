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
    /// 15.2.3 State 'ST05': Override window
    /// TC-ID: 10.2.3
    /// 
    /// This test case verifies the buttons of Override window when entry and exit on state of 'ST05'.
    /// 
    /// Tested Requirements:
    /// MMI_gen 12018 (partly: Override window); MMI_gen 168 (partly: disabled buttons, Override window); MMI_gen 4395 (partly: close button, disabled, Override window); MMI_gen 4396 (partly: close, NA11, NA12, Override window); MMI_gen 5646 (partly: always enable, State 'ST05' button is disabled, Override window); MMI_gen 5728 (partly: removal, EVC, restore after ST05, Override window); MMI_gen 8859 (partly: Override window);
    /// 
    /// Scenario:
    /// 1.The ‘Override’ menu window is displayed.
    /// 2.Use the test script files to send packets in order to verify state ‘ST05’ in a menu window. 
    /// 
    /// Used files:
    /// 10_2_3_a.xml
    /// </summary>
    public class TC_ID_10_2_3_State_ST05 : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:

            // Call the TestCaseBase PreExecution
            base.PreExecution();
            // Test system is powered onCabin is activePerform SoM in SR mode, Level 1.
            DmiActions.Complete_SoM_L1_SR(this);
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in SR mode, Level 1

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint


            /*
            Test Step 1
            Action: Perform the following procedure;Press ‘close’ button (Main menu window)Press ‘Override’ button
            Expected Result: Verify the following information;(1)   Verify DMI still displays Default window until Override window is displayed.(2)   Verify the close button is always enable
            Test Step Comment: (1) MMI_gen 8859 (partly: Override window);(2) MMI_gen 5646 (partly: always enable, Override window);
            */
            DmiActions.ShowInstruction(this, @"Press ‘Close’ button in the Main window. Press ‘Override’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays Default window until Override window is displayed." +
                                Environment.NewLine +
                                "2. ‘Close’ button is always enabled.");

            /*
            Test Step 2
            Action: Use the test script file 10_2_3_a.xml to disable and enable button via EVC-8 withPacket 1 (Entry state of ‘ST05’)MMI_Q_TEXT_CRITERIA = 3 MMI_Q_TEXT = 716Packet 2 (Exit state of ‘ST05’)MMI_Q_TEXT_CRITERIA = 4MMI_Q_TEXT = 716Note: Stopwatch is required for accuracy of test result
            Expected Result: Verify the following information;DMI in the entry state of ‘ST05’The hourglass symbol ST05 is displayed.(1)   Verify all buttons and the close button is disable.(2)   The disabled Close button NA12 is display in area G.10 seconds laterDMI in the exit state of ‘ST05’(3)   The hourglass symbol ST05 is removed.(4)   The state of all buttons is restored according to the last status before script is sent.(5)   The enabled Close button NA11 is display in area G
            Test Step Comment: (1) MMI_gen 12018 (partly: Override window);(2) MMI_gen 168 (partly: disabled buttons, Override window); MMI_gen 5646 (partly: State 'ST05' button is disabled, Override window); MMI_gen 4395 (partly: close button, disabled, Override window); MMI_gen 4396 (partly: close, NA12, Override window);(3) MMI_gen 5728 (partly: removal, EVC, Override window);(4) MMI_gen 5728 (partly: restore after ST05, Override window);(5) MMI_gen 4396 (partly: close, NA11, Override window);
            */
            XML_10_2_3_a();

            /*
            Test Step 3
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }

        #region Send_XML_10_2_3_a_DMI_Test_Specification

        private void XML_10_2_3_a()
        {
            // Step 2/1
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 716;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;

            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI is in the entry state of ‘ST05’." + Environment.NewLine +
                                "2. The hourglass symbol ST05 is displayed." + Environment.NewLine +
                                "3. All buttons and the ‘Close’ button are disabled." + Environment.NewLine +
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
        }

        #endregion
    }
}
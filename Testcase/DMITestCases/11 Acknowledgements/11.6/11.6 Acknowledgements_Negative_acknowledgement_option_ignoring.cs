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
    /// 11.6 Acknowledgements: Negative acknowledgement option ignoring
    /// TC-ID: 6.6
    /// 
    /// This test case verifies that DMI is still display a symbol with acknowledgement even received packet EVC-8 with negative acknowledgement option.
    /// 
    /// Tested Requirements:
    /// MMI_gen 4504;
    /// 
    /// Scenario:
    /// 1.At the default window, Use the test script file to send Driver message to DMI. Then, verifies the display information.
    /// 2.Press an acknowledgement area. Then use the test script file to send Driver message to DMI and verifies the display information.
    /// 
    /// Used files:
    /// 6_6_a.xml, 6_6_b.xml, 6_6_c.xml
    /// </summary>
    public class Acknowledgements_Negative_acknowledgement_option_ignoring : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:

            // Call the TestCaseBase PreExecution
            base.PreExecution();
            DmiActions.Complete_SoM_L1_SB(this);
            // System is power on.Cabin is activated.SoM is performed until Level 1 is selected and confirmed.Main window is closed.
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in SB mode, Level 1
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
            Action: Use the test script file 6_6_a.xml to send EVC-8 with,MMI_Q_TEXT = 260MMI_Q_TEXT_CRITERIA = 2MMI_I_TEXT = 1
            Expected Result: Verify the following information,(1)    DMI displays ST01 symbol with yellow flashing frame in sub-area C9
            Test Step Comment: (1) MMI_gen 4504 (partly: symbols);
            */
            XML.XML_6_6_a.Send(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the ST01 symbol with a yellow flashing frame in sub-area C9.");

            /*
            Test Step 2
            Action: Press an acknowledgement on sub-area C9.Then, use the test script file 6_6_b.xml to send EVC-8 with,MMI_Q_TEXT = 298MMI_Q_TEXT_CRITERIA = 2MMI_I_TEXT = 1
            Expected Result: Verify the following information,(1)    DMI displays DR02 symbol with yes button in area D
            Test Step Comment: (1) MMI_gen 4504 (partly: TAF);
            */
            DmiActions.ShowInstruction(this, "Acknowledge by pressing in sub-area C9");

            XML.XML_6_6_b.Send(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the DR02 symbol and a ‘Yes’ button (to the right) in area D.");

            /*
            Test Step 3
            Action: Press ‘Yes’ button on area D.Then, use the test script file 6_6_c.xml to send EVC-8 with,MMI_Q_TEXT = 263MMI_Q_TEXT_CRITERIA = 2MMI_I_TEXT = 1
            Expected Result: Verify the following information,(1)    DMI displays MO10 symbol with yellow flashing frame in sub-area C1
            Test Step Comment: (1) MMI_gen 4504 (partly: symbols);
            */
            DmiActions.ShowInstruction(this, "Press the ‘Yes’ button (to the right) in area D.");

            XML.XML_6_6_c.Send(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the MO10 symbol with a yellow flashing frame in sub-area C1.");

            /*
            Test Step 4
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
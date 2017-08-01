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
    /// 30.2 Start-up error with MMI_M_START_REQ = 2, 3 and 4
    /// TC-ID: 25.2
    /// 
    /// To verify that the DMI is working properly while DMI start-up is error.
    /// 
    /// Tested Requirements:
    /// MMI_gen 236;
    /// 
    /// Scenario:
    /// Use test scripts to simulate mistakes during DMI start-up and verify that DMI displays each text message correctly.
    /// 
    /// Used files:
    /// 25_2_a.xml25_2_b.xml25_2_c.xml
    /// </summary>
    public class Start_up_error_with_MMI_M_START_REQ_2_3_and_4 : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // Power on test system and start OTE.

            // Call the TestCaseBase PreExecution
            base.PreExecution();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // Cabin is inactive and DMI displays the latest message “Incompatible SW versions” in area E5.

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint


            /*
            Test Step 1
            Action: Use 25_2_a.xml script to simulate the [MMI_START_ATP(EVC-0).MMI_M_START_REQ] = 2
            Expected Result: (1)    DMI displays the message “MMI type not supported” instead of “starting up” in area E5
            Test Step Comment: (1) MMI_gen 236 (partly: MMI_M_START_REQ = 2)
            */


            /*
            Test Step 2
            Action: Restart OTE and ATP again until the message “starting up” is displayed in area E5
            Expected Result: 
            */
            // Call generic Action Method
            DmiActions.Restart_OTE_and_ATP_again_until_the_message_starting_up_is_displayed_in_area_E5(this);


            /*
            Test Step 3
            Action: Use 25_2_b.xml script to simulate the [MMI_START_ATP(EVC-0).MMI_M_START_REQ] = 3
            Expected Result: (1)    DMI displays the message “Incompatible IF versions” instead of “starting up” in area E5
            Test Step Comment: (1) MMI_gen 236 (partly: MMI_M_START_REQ = 3)
            */


            /*
            Test Step 4
            Action: Restart OTE and ATP again until the message “starting up” is displayed in area E5
            Expected Result: 
            */
            // Call generic Action Method
            DmiActions.Restart_OTE_and_ATP_again_until_the_message_starting_up_is_displayed_in_area_E5(this);


            /*
            Test Step 5
            Action: Use 25_2_c.xml script to simulate the [MMI_START_ATP(EVC-0).MMI_M_START_REQ] = 4
            Expected Result: (1)    DMI displays the message “Incompatible SW versions” instead of “starting up” in area E5
            Test Step Comment: (1) MMI_gen 236 (partly: MMI_M_START_REQ = 4)
            */


            /*
            Test Step 6
            Action: End of test
            Expected Result: 
            */


            return GlobalTestResult;
        }
    }
}
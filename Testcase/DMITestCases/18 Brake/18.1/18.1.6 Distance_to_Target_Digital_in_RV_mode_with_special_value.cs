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
    /// 18.1.6 Distance to Target Digital in RV mode with special value
    /// TC-ID: 13.1.6
    /// 
    /// This test case verifies the presentation of the distance to target digital in RV mode when received the special value.
    /// 
    /// Tested Requirements:
    /// MMI_gen 6777; 
    /// 
    /// Scenario:
    /// 1.Drive the train forward pass BG1 at position 50m       BG1: packet 12, 21 and 27 (Entering FS)
    /// 2.Drive the train forward pass BG2 at position 200m.        BG2 packet138: D_STARTREVERSE 100, L_REVERSEAREA 400               packet139: D_REVERSE 32767, V_REVERSE 30      
    /// 3.Stop the train at position 700m.
    /// 4.Select and confirm reversing mode.
    /// 5.Drive the train backward and verify the display of distance to target on DMI.
    /// 
    /// Used files:
    /// 13_1_6.tdg
    /// </summary>
    public class Distance_to_Target_Digital_in_RV_mode_with_special_value : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // Test system is power on.Cabin is activatedStart of Mission is completed in SR mode, level1 (set train length = 100m)

            // Call the TestCaseBase PreExecution
            base.PreExecution();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in RV mode, level 1.

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint


            /*
            Test Step 1
            Action: Drive the train forward passing BG1 with speed = 40 km/h until entering FS mode.
            Expected Result: 
            */

            /*
            Test Step 2
            Action: Continue drive the train forward passing BG2.
            Expected Result: 
            */

            /*
            Test Step 3
            Action: The train is in reversing area.
            Expected Result: 
            */

            /*
            Test Step 4
            Action: Stop the train.
            Expected Result: The train is standstill.Driver is informed that reversing is possible.
            */

            /*
            Test Step 5
            Action: Change the direction of train to reverse. Then select and confirm RV mode.
            Expected Result: DMI displays in RV mode, level 1.Verify the following information,(1)    Use the log file to confirm that DMI received packet EVC-1 with variable MMI_O_BRAKETARGET = 2147483647(2)    The symbol infinity '∞' is displayed for distance to target digital in sub-area A2.(3)    The symbol is be horizontally and vertically centered in Sub-Area A2.
            Test Step Comment: (1) MMI_gen 6777  (partly: receive MMI_O_BRAKETARGET  equal special value);                   (2) MMI_gen 6777 (partly: when MMI_O_BRAKETARGET  equal special value); (3) MMI_gen 6777 (partly: horizontally and vertically centered);
            */

            /*
            Test Step 6
            Action: End of test.
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
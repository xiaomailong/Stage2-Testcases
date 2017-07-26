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
    /// 18.1.8 Distance to Target Bar: Maximum digit of Distance to Target Digital
    /// TC-ID: 13.1.8
    /// 
    /// This test case verifies the display of distance to target digital when the received packet information EVC-1 is greater than maximum value, the distance to target is able to display up to 5 digit with no leading zero.
    /// 
    /// Tested Requirements:
    /// MMI_gen 104 (partly: no leading zero, able to show up to 5 digits, right aligned); MMI_gen 6776 (partly: Greater distance); MMI_gen 105 (partly: equation, MMI_O_BRAKETARGET – OBU_TR_O_TRAIN); MMI_gen 6771; MMI_gen 6877;
    /// 
    /// Scenario:
    /// 1.Drive the train forward pass BG1 at 100m. Then, verify the display of distance to target bar with received packet information EVC-1 and EVC-
    /// 7.BG1: Packet 12, 21 and 27 (Entering FS)
    /// 2.Use the test script file to send an invalid value in EVC-1 and EVC-
    /// 7.Then, verify that distance to target bar and scale is removed. 
    /// 
    /// Used files:
    /// 13_1_8.tdg, 13_1_8_a.xml, 13_1_8_b.xml, 13_1_8_c.xml
    /// </summary>
    public class Distance_to_Target_Bar_Maximum_digit_of_Distance_to_Target_Digital : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // System is powered on.Cabin is activated.SoM is performed in SR mode, level 1.

            // Call the TestCaseBase PreExecution
            base.PreExecution();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in FS mode, Level 1

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint


            /*
            Test Step 1
            Action: Drive the train forward pass BG1.Then, stop the train.
            Expected Result: DMI displays in FS mode, level 1.Verify the following information,(1)   Use the log file to confirm that the distance to target (bar and digital) is calculated from the received packet information EVC-7 and EVC -1 as follows,(EVC-1) MMI_O_BRAKETARGET  – (EVC-7) OBU_TR_O_TRAIN Example: The observation point of the distance target is 4480. [EVC-1.MMI_O_BRAKETARGET = 1000498078] – [EVC-7.OBU_TR_O_TRAIN = 1000050121] = 447,957 cm (4479.57m)(2)   The first digit of distance to target digital in sub-area A2 is not zero. (3)   The distane to target digital is right aligned.
            Test Step Comment: (1) MMI_gen 105 (partly: equation, MMI_O_BRAKETARGET – OBU_TR_O_TRAIN); MMI_gen 104 (partly: able to show); MMI_gen 6771;(2) MMI_gen 104 (partly: no leading zero);(3) MMI_gen 104 (partly: right aligned);
            */

            /*
            Test Step 2
            Action: Use the test script file 13_1_8_a.xml to send EVC-1 with,MMI_O_BRAKETARGET = 1010500000
            Expected Result: Verify the following information,(1)   DMI display the distance to target digital as ‘99999’.
            Test Step Comment: (1) MMI_gen 6776 (partly: Greater distance); MMI_gen 104 (partly: able to show up to 5 digits);
            */

            /*
            Test Step 3
            Action: Use the test script file 13_1_8_b.xml to send EVC-1 with,MMI_M_WARNING = 7
            Expected Result: Verify the following information,(1)   The distance to target bar and digital is removed from the DMI.        After test scipt file is executed, the distance to target bar and digital is re-appear refer to received packet EVC-1 from ETCS Onboard.
            Test Step Comment: (1) MMI_gen 6877 (partly: MMI_M_WARNING is invalid); 
            */

            /*
            Test Step 4
            Action: Use the test script file 13_1_8_c.xml to send EVC-7 with,OBU_TR_M_MODE = 17
            Expected Result: Verify the following information,(1)   The distance to target bar and digital is removed from the DMI.        After test scipt file is executed, the distance to target bar and digital is re-appear refer to received packet EVC-1 from ETCS Onboard.
            Test Step Comment: (1) MMI_gen 6877 (partly: OBU_TR_M_MODE is invalid); 
            */

            /*
            Test Step 5
            Action: End of test.
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
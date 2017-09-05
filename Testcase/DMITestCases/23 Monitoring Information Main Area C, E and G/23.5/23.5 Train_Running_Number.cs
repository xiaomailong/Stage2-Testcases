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
    /// 23.5 Train Running Number
    /// TC-ID: 18.5
    /// 
    /// This test case verifies the validity and display of Train Running number refer to received packet information
    /// 
    /// Tested Requirements:
    /// MMI_gen 1043; MMI_gen 1044; MMI_gen 11905 (partly: text is grey);
    /// 
    /// Scenario:
    /// Enter and confirm the train running number. Then, verify the display information of train runnning number in sub-area G11.Use the test script file to send EVC-2 with an invalid value. Then, verify the display information of train running number in sub-area G11.
    /// 
    /// Used files:
    /// 18_5.xml
    /// </summary>
    public class Train_Running_Number : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // Test system is power on.Cabin is activateSoM is performed until the ‘Train running number’ window is opened in SB mode, Level 1.

            // Call the TestCaseBase PreExecution
            base.PreExecution();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in SB mode, Level 1.

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint


            /*
            Test Step 1
            Action: Perform the following procedure, Enter Driver ID and perform brake test.Select and confirm Level 1.Press ‘Train data’ button.Enter an confirm the train data.Validate the Train data
            Expected Result: The Train Running Number window is displayed
            */
            // Call generic Check Results Method
            DmiExpectedResults.TRN_window_displayed(this);


            /*
            Test Step 2
            Action: Enter and confirm the train running number.Then, press ‘Close’ button at Main window
            Expected Result: DMI displays Default window.Verify the following information,DMI displays the train running number in sub-area G11.The displayed train running number is taken from [MMI_STATUS (EVC-2).MMI_NID_OPERATION].Note: The hexadecimal value ‘F’ is mean ‘No digit’.(i.e. ‘123456’ = 0x123456FF).The text of displayed train running number is coloured grey
            Test Step Comment: (1) MMI_gen 1043 (partly: valid, G11);   (2) MMI_gen 1044;  (3)MMI_gen 11905 (partly: text is grey);
            */


            /*
            Test Step 3
            Action: Use the test script file 18_5.xml to send EVC-2 with,MMI_NID_OPERATION = 173069631487 (0xA12FFFFF)Note: It’s necessary to execute the test script file repeatly to verify the test result
            Expected Result: Verify the following information,Verify that the train running number is disappear from sub-area G11.Note: The result will be appear only short time because it’s interrupted by ATP-CU packet information
            Test Step Comment: (1) MMI_gen 1043 (partly: invalid, G11);
            */


            /*
            Test Step 4
            Action: End of test
            Expected Result: 
            */


            return GlobalTestResult;
        }
    }
}
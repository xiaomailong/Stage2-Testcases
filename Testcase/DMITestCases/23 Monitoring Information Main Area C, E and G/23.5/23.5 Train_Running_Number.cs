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
    public class TC_ID_18_5_Train_Running_Number : TestcaseBase
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

            EVC0_MMIStartATP.Evc0Type = EVC0_MMIStartATP.EVC0Type.GoToIdle;
            EVC0_MMIStartATP.Send();
            
            DmiActions.Activate_Cabin_1(this);
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint

            /*
            Test Step 1
            Action: Perform the following procedure, Enter Driver ID and perform brake test.Select and confirm Level 1.Press ‘Train data’ button.Enter an confirm the train data.Validate the Train data
            Expected Result: The Train Running Number window is displayed
            */
            DmiActions.ShowInstruction(this, "Enter the Driver ID and perform the brake test. Select and confirm Level 1." + Environment.NewLine +
                                             "Press the ‘Train data’ button. Enter, confirm and validate the train data.");

            DmiExpectedResults.TRN_window_displayed(this);

            /*
            Test Step 2
            Action: Enter and confirm the train running number.Then, press ‘Close’ button at Main window
            Expected Result: DMI displays Default window.Verify the following information,DMI displays the train running number in sub-area G11.The displayed train running number is taken from [MMI_STATUS (EVC-2).MMI_NID_OPERATION].Note: The hexadecimal value ‘F’ is mean ‘No digit’.(i.e. ‘123456’ = 0x123456FF).The text of displayed train running number is coloured grey
            Test Step Comment: (1) MMI_gen 1043 (partly: valid, G11);   (2) MMI_gen 1044;  (3)MMI_gen 11905 (partly: text is grey);
            */
            DmiActions.ShowInstruction(this, "Enter and confirm the train running number. Then, press the ‘Close’ button in the Main window");

            EVC2_MMIStatus.TrainRunningNumber = 0x234FFFFF;
            EVC2_MMIStatus.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Default window." + Environment.NewLine +
                                "2. DMI displays the Train Running number as ‘234’ in grey in sub-area G11.");

            /*
            Test Step 3
            Action: Use the test script file 18_5.xml to send EVC-2 with,MMI_NID_OPERATION = 173069631487 (0xA12FFFFF)Note: It’s necessary to execute the test script file repeatly to verify the test result
            Expected Result: Verify the following information,Verify that the train running number is disappear from sub-area G11.Note: The result will be appear only short time because it’s interrupted by ATP-CU packet information
            Test Step Comment: (1) MMI_gen 1043 (partly: invalid, G11);
            */
            XML.XML_18_5.Send(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI removes the train running number.");

            /*
            Test Step 4
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
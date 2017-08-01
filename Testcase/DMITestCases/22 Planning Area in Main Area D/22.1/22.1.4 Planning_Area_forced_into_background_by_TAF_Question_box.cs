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
    /// 22.1.4 Planning Area forced into background by TAF Question box
    /// TC-ID: 17.1.4
    /// 
    /// This test case verifies that planning area is forced into the background when TAF Question Box is opened but still updating information continuously.
    /// 
    /// Tested Requirements:
    /// MMI_gen 7097; MMI_gen 11470 (partly: Bit # 22);
    /// 
    /// Scenario:
    /// Perform SoM to SR mode, level 2.Drive the train forward to receive information from RBC at 70m.Message 3: Packet 15,21, 27 and 80 (Entering FS and get OS mode acknowledgement area)Continue to drive the train forward and acknowledge OS mode at position 250m.Drive the train forward to receive Track ahead free request from RBC at position 350m. Then, verify that PA is not display.Acknowledge Track ahead free. Then, verify that each objects of PA is updated refer to position of the train.
    /// 
    /// Used files:
    /// 17_1_4.tdg, 17_1_4.utt
    /// </summary>
    public class Planning_Area_forced_into_background_by_TAF_Question_box : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // Set the following tags name in configuration file (See the instruction in Appendix 1)HIDE_PA_OS_MODE = 1 (PA will show in OS mode)System is power on.Cabin is activate.

            // Call the TestCaseBase PreExecution
            base.PreExecution();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays OS mode, Level 2.

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint


            /*
            Test Step 1
            Action: Perform SoM to SR mode, level 2
            Expected Result: DMI displays in SR mode, level 2
            */
            // Call generic Check Results Method
            DmiExpectedResults.DMI_displays_in_SR_mode_level_2(this);


            /*
            Test Step 2
            Action: Receive information from RBC
            Expected Result: DMI changes from SR mode to FS mode, level 2
            */
            // Call generic Check Results Method
            DmiExpectedResults.DMI_changes_from_SR_mode_to_FS_mode_level_2(this);


            /*
            Test Step 3
            Action: Acknowledge OS mode by pressing at area C1
            Expected Result: DMI changes from FS mode to OS mode, level 2
            */
            // Call generic Check Results Method
            DmiExpectedResults.DMI_changes_from_FS_mode_to_OS_mode_level_2(this);


            /*
            Test Step 4
            Action: Received information from RBC
            Expected Result: DMI displays symbol DR02 (Confirm Track Ahead Free) in Main area D.Verify that Planning area is forced into background, and it is not display in Main area D
            Test Step Comment: (1) MMI_gen 7097 (partly: force into the background);
            */
            // Call generic Action Method
            DmiActions.Received_information_from_RBC(this);


            /*
            Test Step 5
            Action: Drive the train forward
            Expected Result: The symbol DR02 is still displayed in Main area D
            */
            // Call generic Action Method
            DmiActions.Drive_the_train_forward(this);


            /*
            Test Step 6
            Action: Press ‘Yes’ button in Main area D
            Expected Result: DMI displays PA in Main area D again.Verify that the following object is moving down to the bottom of area D.PASPUse the log file to confirm that DMI sends out packet [MMI_DRIVER_ACTION (EVC-152)] with the value of variable MMI_M_DRIVER_ACTION refer to sequence below,a)   MMI_M_DRIVER_ACTION = 22 (Confirmation of Track Ahead Free)
            Test Step Comment: (1) MMI_gen 7097 (partly: Update information in background);(2) MMI_gen 11470 (partly: Bit # 22);
            */


            /*
            Test Step 7
            Action: End of test
            Expected Result: 
            */


            return GlobalTestResult;
        }
    }
}
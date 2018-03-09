using System;
using BT_CSB_Tools;

namespace Testcase.DMITestCases
{
    /// <summary>
    /// FailureStatus on LED
    /// TC-ID: 1.12
    /// Doors unique ID: TP-35836
    /// This test case verifies the presentation of LED status and error message while the DMI configuration file error.
    /// 
    /// Tested Requirements:
    /// MMI_gen 12214 (partly: configuration errors);
    /// 
    /// Scenario:
    /// 1. Power on system and verify the LED status and error message on DMI.
    /// 
    /// Used files:
    /// N/A
    /// </summary>
    public class TC_ID_1_12_FailureStatus_on_LED : TestcaseBase
    {
        public override void PreExecution()
        {
            /* Pre-conditions from TestSpec
            	Remove all content in configuration file. See an instruction in Appendix 1
            */

            TraceInfo("Pre-condition: " + "Remove all content in configuration file. See an instruction in Appendix 1");

            base.PreExecution();
        }

        public override void PostExecution()
        {
            /* Post-conditions from TestSpec
            	DMI display with LED status and error message.
            */

            TraceInfo("Post-condition: " + "DMI display with LED status and error message.");

            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            throw new TestcaseException("The HMI411S is not compatible with this test, review requirements");
            /*
            Test Step 1
            Action:
            	Power on DMI and start up the system
            Expected Result:
            	Verify the following information,
            	(1)   The red LED status is on.
            	(2)   There is error message display on DMI.Note: The expected of error message is an optional
            Test Step Comment:
            	(1) MMI_gen 12214 (partly: status on LED, configuration error);
            	(2) MMI_gen 12214 (partly: error message, configuration error);
            */
            MakeTestStepHeader(1, 35845, "Power on DMI and start up the system",
                "Verify the following information," + Environment.NewLine + "(1)   The red LED status is on." +
                Environment.NewLine +
                "(2)   There is error message display on DMI.Note: The expected of error message is an optional");

            /* End Of Test */
            TraceHeader("End Of Test");


            return GlobalTestResult;
        }
    }
}
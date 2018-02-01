using System;
using Testcase.Telegrams.EVCtoDMI;

namespace Testcase.DMITestCases
{
    /// <summary>
    /// 7.5.1 DMI Failure Reaction: Reboot function (SIL0 DMI)
    /// TC-ID: 2.5.1
    /// 
    /// This test case verifies that DMI is reboot or stop according to the received packet EVC-0 with specific value.
    /// 
    /// Tested Requirements:
    /// MMI_gen 11439;
    /// 
    /// Scenario:
    /// 1.Use the test script file to send EVC-
    /// 0.Then, verify that DMI is reboot.
    /// 
    /// Used files:
    /// 2_5_1_a.xml
    /// </summary>
    public class TC_ID_2_5_1_Failure_Reaction : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:

            // Call the TestCaseBase PreExecution
            base.PreExecution();
            // -   System is power on.-   Cabin is activated.-   SoM is perform until level 1 is selected and confirmed.
            DmiActions.Complete_SoM_L1_SB(this);
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI is rebooted.

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint


            TraceHeader("Test Step 1");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Use the test script file 2_5_1_a.xml to send EVC-0 with,MMI_M_START_REQ = 10 (DMI reboot, Indication error)");
            TraceReport("Expected Result");
            TraceInfo("Verify the following information,(1)   DMI is rebooted");
            /*
            Test Step 1
            Action: Use the test script file 2_5_1_a.xml to send EVC-0 with,MMI_M_START_REQ = 10 (DMI reboot, Indication error)
            Expected Result: Verify the following information,(1)   DMI is rebooted
            Test Step Comment: (1) MMI_gen 11439;
            */
            DmiActions.ShowInstruction(this, @"Press ‘Ok’ to reboot DMI");

            #region Send_XML_5_5_1_10_DMI_Test_Specification

            // XML indicates MMI_M_START_REQ property with value of 10
            Testcase.Telegrams.EVCtoDMI.EVC0_MMIStartATP.Evc0Type = EVC0_MMIStartATP.EVC0Type.DMIRebootIndicationError;
            Testcase.Telegrams.EVCtoDMI.EVC0_MMIStartATP.Send();

            #endregion

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI is rebooted.");


            TraceHeader("Test Step 2");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("End of test");
            TraceReport("Expected Result");
            TraceInfo("");
            /*
            Test Step 2
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
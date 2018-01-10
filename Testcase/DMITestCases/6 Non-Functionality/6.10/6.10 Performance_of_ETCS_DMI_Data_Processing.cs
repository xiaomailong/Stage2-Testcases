namespace Testcase.DMITestCases
{
    /// <summary>
    /// 6.10 Performance of ETCS-DMI Data Processing
    /// TC-ID: 1.10
    /// 
    /// This test case verifies the performance of ETCS data processing within ETCS-DMI. The function designs comply with the conditions in [MMI-ETCS-gen]. The data range and interface comply with the data information in [VSIS_gen].
    /// 
    /// Tested Requirements:
    /// MMI_gen 3216; MMI_gen 88 (partly: ETCS BA);
    /// 
    /// Scenario:
    /// 1.Connect RCI and start RCI logging
    /// 2.Activate the cabin.
    /// 3.Verify and calculate the time responses of SoM until the ‘Staff Responsible’ mode, level 2, is confirmed. 
    /// 
    /// Used files:
    /// 1_10.utt
    /// </summary>
    public class Performance_of_ETCS_DMI_Data_Processing : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // 1. The test environment is powered on.2. The RCI client is connected to ETCS-DMI with the concerned ETCS-DMI IP address via port 15001 (Raw connection). 3. The RCI is commanded to start logging the following data:- The incoming data received by MVB port.- The concerned data for ETCS-DMI screen update.- The ETCS-DMI screen update according to the incoming data.4. The cabin is activated.

            // Call the TestCaseBase PreExecution
            base.PreExecution();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in SR mode, level 2.

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint


            /*
            Test Step 1
            Action: Perform SoM in SR mode, Level 2
            Expected Result: RCI logs the concerned activities as specified in the precondition
            */
            // Call generic Action Method
            DmiActions.Perform_SoM_in_SR_mode_Level_2(this);
            // Call generic Check Results Method
            DmiExpectedResults.RCI_logs_the_concerned_activities_as_specified_in_the_precondition(this);


            /*
            Test Step 2
            Action: Observe the timestamps in RCI log and calculate the average differentiation of the response time of the incoming data and ETCS-DMI update in:- The MVB port- The ETCS-DMI screen
            Expected Result: (1) Use the RCI log to confirm the  (average) response time differentiation of the incoming data (message) and when related visualization is updated on ETCS-DMI screen (tupdatedDMI – tinMVB) is less than 130 ms
            Test Step Comment: (1) MMI_gen 3216; MMI_gen 88 (partly: ETCS BA);
            */


            /*
            Test Step 3
            Action: End of test
            Expected Result: 
            */


            return GlobalTestResult;
        }
    }
}
namespace Testcase.DMITestCases
{
    /// <summary>
    /// 6.10 Performance of ETCS-DMI Data Processing
    /// TC-ID: 1.10
    /// 
    /// This test case verifies the performance of ETCS data processing within ETCS-DMI. 
    /// The function designs comply with the conditions in [MMI-ETCS-gen]. 
    /// The data range and interface comply with the data information in [VSIS_gen].
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
    public class TC_1_10_Performance_of_ETCS_DMI_Data_Processing : TestcaseBase
    {
        public override bool TestcaseEntryPoint()
        {
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 20453;
            // Testcase entrypoint
            DmiActions.ShowInstruction(this, "THIS TESCASE TO BE SKIPPED??");
            StartUp();

            MakeTestStepHeader(1, UniqueIdentifier++, "Perform SoM in SR mode, Level 2",
                "RCI logs the concerned activities as specified in the precondition");
            /*
            Test Step 1
            Action: Perform SoM in SR mode, Level 2
            Expected Result: RCI logs the concerned activities as specified in the precondition
            */

            DmiActions.Perform_SoM_in_SR_mode_Level_2(this);


            MakeTestStepHeader(2, UniqueIdentifier++,
                "Observe the timestamps in RCI log and calculate the average differentiation of the response time of the incoming data and ETCS-DMI update in:- The MVB port- The ETCS-DMI screen",
                "(1) Use the RCI log to confirm the  (average) response time differentiation of the incoming data (message) and when related visualization is updated on ETCS-DMI screen (tupdatedDMI – tinMVB) is less than 130 ms");
            /*
            Test Step 2
            Action: Observe the timestamps in RCI log and calculate the average differentiation of the response time of the incoming data and ETCS-DMI update in:- The MVB port- The ETCS-DMI screen
            Expected Result: (1) Use the RCI log to confirm the  (average) response time differentiation of the incoming data (message) and when related visualization is updated on ETCS-DMI screen (tupdatedDMI – tinMVB) is less than 130 ms
            Test Step Comment: (1) MMI_gen 3216; MMI_gen 88 (partly: ETCS BA);
            */


            MakeTestStepHeader(3, UniqueIdentifier++, "End of test", "");

            /*
            Test Step 3
            Action: End of test
            Expected Result: 
            */


            return GlobalTestResult;
        }
    }
}
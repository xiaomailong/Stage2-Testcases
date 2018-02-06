namespace Testcase.DMITestCases
{
    /// <summary>
    /// 6.9 Performance of ETCS-DMI: Data handling
    /// TC-ID: 1.9
    /// 
    /// This test case verifies the performance of ETCS-DMI data handling before the data processing in ETCS-DMI. 
    /// The function designs comply with the conditions in [MMI-ETCS-gen]. 
    /// The data range and interface comply with the data information in [VSIS_gen].
    /// 
    /// Tested Requirements:
    /// MMI_gen 89; MMI_gen 87;
    /// 
    /// Scenario:
    /// Connect RCI and start RCI logging
    /// Activate the cabin.
    /// Verify and calculate the time responses of the following events:
    /// a. Perform SoM until the ‘Staff Responsible’ mode, level 2, is confirmed.        
    /// b. Send data of ~200 bytes (EVC-8) 
    /// 
    /// Used files:
    /// 1_9.utt, 1_9_a.xml
    /// </summary>
    public class TC_1_8_Performance_of_ETCS_DMI_Data_handling : TestcaseBase
    {
        public override bool TestcaseEntryPoint()
        {
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 0;
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
                "Observe the timestamps in RCI log and calculate the average differentiation of the response time of the incoming data in:- The MVB port - The GPP component",
                "(1) Use the RCI log to confirm the (average) response time differentiation of the incoming data (message) in the GPP component and MVB port (tinGPP – tinMVB) is less than 128 ms");
            /*
            Test Step 2
            Action: Observe the timestamps in RCI log and calculate the average differentiation of the response time of the incoming data in:- The MVB port - The GPP component
            Expected Result: (1) Use the RCI log to confirm the (average) response time differentiation of the incoming data (message) in the GPP component and MVB port (tinGPP – tinMVB) is less than 128 ms
            Test Step Comment: (1) MMI_gen 87 (partly: read);
            */


            MakeTestStepHeader(3, UniqueIdentifier++,
                "Follow step 2 to calculate the average differentiation of the response time of the outgoing data",
                "(1) Use the RCI log to confirm the (average) response time differentiation of the outgoing data (message) in the MVB port and GPP component (toutGPP - toutMVB) is less than 128 ms");
            /*
            Test Step 3
            Action: Follow step 2 to calculate the average differentiation of the response time of the outgoing data
            Expected Result: (1) Use the RCI log to confirm the (average) response time differentiation of the outgoing data (message) in the MVB port and GPP component (toutGPP - toutMVB) is less than 128 ms
            Test Step Comment: (1) MMI_gen 87 (partly: write);
            */


            MakeTestStepHeader(4, UniqueIdentifier++, "Follow step 2 to calculate the data throughput",
                "(1) Use the RCI log to confirm the (average) response time differentiation of every incoming or outgoing EVC data (extracted EVC packets) with the size of 50 Bytes in GPP component and MVB port (tEVCinGPP – tinMVB or tEVCoutGPP - toutMVB)is less than 250 ms");
            /*
            Test Step 4
            Action: Follow step 2 to calculate the data throughput
            Expected Result: (1) Use the RCI log to confirm the (average) response time differentiation of every incoming or outgoing EVC data (extracted EVC packets) with the size of 50 Bytes in GPP component and MVB port (tEVCinGPP – tinMVB or tEVCoutGPP - toutMVB)is less than 250 ms
            Test Step Comment: (1) MMI_gen 89 (partly: throughput);
            */


            MakeTestStepHeader(5, UniqueIdentifier++, "Send the data of EVC-8 with size of 200 Bytes by 1_9_a.xml",
                "The big size of data can be transferred to ETCS-DMI screen and the text message of “ABC … BC17” displayed in area E5 – E9.Note: Each group of the text message is identified with number 2 – 17, except the first group");
            /*
            Test Step 5
            Action: Send the data of EVC-8 with size of 200 Bytes by 1_9_a.xml
            Expected Result: The big size of data can be transferred to ETCS-DMI screen and the text message of “ABC … BC17” displayed in area E5 – E9.Note: Each group of the text message is identified with number 2 – 17, except the first group
            Test Step Comment: (1) MMI_gen 89 (partly: extra in one shot);
            */


            MakeTestStepHeader(6, UniqueIdentifier++, "End of test", "");

            /*
            Test Step 6
            Action: End of test
            Expected Result: 
            */


            return GlobalTestResult;
        }
    }
}
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
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // 1. The test environment is powered on.
            // 2. The RCI client is connected to ETCS-DMI with the concerned ETCS-DMI IP address via port 15001 (Raw connection).
            // 3. The RCI is commanded to start logging the following data:
            //  - The incoming data message from the MVB port to GPP component.
            //  - The outgoing data message from the GPP component to MVB port.
            //  - The concerned data in the GPP component.
            // 4. The cabin is activated.

            // Call the TestCaseBase PreExecution
            base.PreExecution();
            DmiActions.ShowInstruction(this, "THIS TESCASE TO BE SKIPPED??");
            DmiActions.Start_ATP();
            DmiActions.Activate_Cabin_1(this);
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
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 0;
            // Testcase entrypoint


            TraceHeader("Test Step 1");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Perform SoM in SR mode, Level 2");
            TraceReport("Expected Result");
            TraceInfo("RCI logs the concerned activities as specified in the precondition");
            /*
            Test Step 1
            Action: Perform SoM in SR mode, Level 2
            Expected Result: RCI logs the concerned activities as specified in the precondition
            */

            DmiActions.Perform_SoM_in_SR_mode_Level_2(this);


            TraceHeader("Test Step 2");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Observe the timestamps in RCI log and calculate the average differentiation of the response time of the incoming data in:- The MVB port - The GPP component");
            TraceReport("Expected Result");
            TraceInfo(
                "(1) Use the RCI log to confirm the (average) response time differentiation of the incoming data (message) in the GPP component and MVB port (tinGPP – tinMVB) is less than 128 ms");
            /*
            Test Step 2
            Action: Observe the timestamps in RCI log and calculate the average differentiation of the response time of the incoming data in:- The MVB port - The GPP component
            Expected Result: (1) Use the RCI log to confirm the (average) response time differentiation of the incoming data (message) in the GPP component and MVB port (tinGPP – tinMVB) is less than 128 ms
            Test Step Comment: (1) MMI_gen 87 (partly: read);
            */


            TraceHeader("Test Step 3");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Follow step 2 to calculate the average differentiation of the response time of the outgoing data");
            TraceReport("Expected Result");
            TraceInfo(
                "(1) Use the RCI log to confirm the (average) response time differentiation of the outgoing data (message) in the MVB port and GPP component (toutGPP - toutMVB) is less than 128 ms");
            /*
            Test Step 3
            Action: Follow step 2 to calculate the average differentiation of the response time of the outgoing data
            Expected Result: (1) Use the RCI log to confirm the (average) response time differentiation of the outgoing data (message) in the MVB port and GPP component (toutGPP - toutMVB) is less than 128 ms
            Test Step Comment: (1) MMI_gen 87 (partly: write);
            */


            TraceHeader("Test Step 4");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Follow step 2 to calculate the data throughput");
            TraceReport("Expected Result");
            TraceInfo(
                "(1) Use the RCI log to confirm the (average) response time differentiation of every incoming or outgoing EVC data (extracted EVC packets) with the size of 50 Bytes in GPP component and MVB port (tEVCinGPP – tinMVB or tEVCoutGPP - toutMVB)is less than 250 ms");
            /*
            Test Step 4
            Action: Follow step 2 to calculate the data throughput
            Expected Result: (1) Use the RCI log to confirm the (average) response time differentiation of every incoming or outgoing EVC data (extracted EVC packets) with the size of 50 Bytes in GPP component and MVB port (tEVCinGPP – tinMVB or tEVCoutGPP - toutMVB)is less than 250 ms
            Test Step Comment: (1) MMI_gen 89 (partly: throughput);
            */


            TraceHeader("Test Step 5");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Send the data of EVC-8 with size of 200 Bytes by 1_9_a.xml");
            TraceReport("Expected Result");
            TraceInfo(
                "The big size of data can be transferred to ETCS-DMI screen and the text message of “ABC … BC17” displayed in area E5 – E9.Note: Each group of the text message is identified with number 2 – 17, except the first group");
            /*
            Test Step 5
            Action: Send the data of EVC-8 with size of 200 Bytes by 1_9_a.xml
            Expected Result: The big size of data can be transferred to ETCS-DMI screen and the text message of “ABC … BC17” displayed in area E5 – E9.Note: Each group of the text message is identified with number 2 – 17, except the first group
            Test Step Comment: (1) MMI_gen 89 (partly: extra in one shot);
            */


            TraceHeader("Test Step 6");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("End of test");
            TraceReport("Expected Result");
            TraceInfo("");
            /*
            Test Step 6
            Action: End of test
            Expected Result: 
            */


            return GlobalTestResult;
        }
    }
}
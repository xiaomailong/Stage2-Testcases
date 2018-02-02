using System;
using System.Collections.Generic;
using Testcase.Telegrams.EVCtoDMI;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 23.6.2 Maximum of Track Conditions in internal memory
    /// TC-ID: 18.6.2
    /// 
    /// This test case verifies the display information of Track conditions symbols, The symbols are display/remove correctly refer to requirement specification and verifies the maximum internal memory which stored 30 track conditions.
    /// 
    /// Tested Requirements:
    /// MMI_gen 10465; MMI_gen 10467; MMI_gen 668; MMI_gen 10470; MMI_gen 10475; MMI_gen 662; arn_043#3617; MMI_gen 667;
    /// 
    /// Scenario:
    /// Use the test script file to send a packet to DMI, Then, verifies the display information.Note: Each step of test script file in executed continuously, Tester need to confirm expected result within specific time (5 second).
    /// 
    /// Used files:
    /// 18_6_2_a.xml, 18_6_2_b.xml, 18_6_2_c.xml
    /// </summary>
    public class TC_ID_18_6_2_Track_Conditions : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:

            // Call the TestCaseBase PreExecution
            base.PreExecution();

            // Test system is powered on.Activate Cabin A.SoM is perform until level 1 is selected and confirmed.Main window is closed.
            DmiActions.Complete_SoM_L1_SB(this);
        }

        public override bool TestcaseEntryPoint()
        {
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 0;
            // Testcase entrypoint

            // Steps 1 to 11 are in XML_18_6_2_a.cs
            TraceHeader("Test Step 1");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Use a first test script file 18_6_2_a.xml to Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 0MMI_N_TRACKCONDITIONS = 3MMI_NID_TRACKCOND[0] = 0MMI_Q_TRACKCOND_STEP[0] = 1MMI_M_TRACKCOND_TYPE[0] = 3MMI_Q_TRACKCOND_ACTION_START[0] = 1MMI_Q_TRACKCOND_ACTION_END [0] = 0MMI_NID_TRACKCOND[1] = 1MMI_Q_TRACKCOND_STEP[1] = 2MMI_M_TRACKCOND_TYPE[1] = 3MMI_Q_TRACKCOND_ACTION_START[1] = 1MMI_Q_TRACKCOND_ACTION_END [1] = 0MMI_NID_TRACKCOND[2] = 2MMI_Q_TRACKCOND_STEP[2] = 3MMI_M_TRACKCOND_TYPE[2] = 3MMI_Q_TRACKCOND_ACTION_START[2] = 0MMI_Q_TRACKCOND_ACTION_END [2] = 1");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following information,(1)   The 3 symbols are correctly  displayed in the specified location below,The symbol TC02 is display in sub-area B3.The symbol TC01 is display in sub-area B4.The symbol TC04 is display in sub-area B5");
            /*
            Test Step 1
            Action: Use a first test script file 18_6_2_a.xml to Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 0MMI_N_TRACKCONDITIONS = 3MMI_NID_TRACKCOND[0] = 0MMI_Q_TRACKCOND_STEP[0] = 1MMI_M_TRACKCOND_TYPE[0] = 3MMI_Q_TRACKCOND_ACTION_START[0] = 1MMI_Q_TRACKCOND_ACTION_END [0] = 0MMI_NID_TRACKCOND[1] = 1MMI_Q_TRACKCOND_STEP[1] = 2MMI_M_TRACKCOND_TYPE[1] = 3MMI_Q_TRACKCOND_ACTION_START[1] = 1MMI_Q_TRACKCOND_ACTION_END [1] = 0MMI_NID_TRACKCOND[2] = 2MMI_Q_TRACKCOND_STEP[2] = 3MMI_M_TRACKCOND_TYPE[2] = 3MMI_Q_TRACKCOND_ACTION_START[2] = 0MMI_Q_TRACKCOND_ACTION_END [2] = 1
            Expected Result: Verify the following information,(1)   The 3 symbols are correctly  displayed in the specified location below,The symbol TC02 is display in sub-area B3.The symbol TC01 is display in sub-area B4.The symbol TC04 is display in sub-area B5
            Test Step Comment: (1)   MMI_gen 10470; MMI_gen 10475; MMI_gen 662 (partly: TC02, TC01, TC04); MMI_gen 10465 (partly: TC02, TC01, TC04);
            */
            DmiActions.ShowInstruction(this, "Close the Main window");

            XML_18_6_2(msgType.typea);

            TraceHeader("Test Step 2");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 1MMI_N_TRACKCONDITIONS = 3MMI_NID_TRACKCOND[0] = 3MMI_Q_TRACKCOND_STEP[0] = 1MMI_M_TRACKCOND_TYPE[0] = 9MMI_Q_TRACKCOND_ACTION_START[0] = 1MMI_Q_TRACKCOND_ACTION_END [0] = 0MMI_NID_TRACKCOND[1] = 4MMI_Q_TRACKCOND_STEP[1] = 2MMI_M_TRACKCOND_TYPE[1] = 9MMI_Q_TRACKCOND_ACTION_START[1] = 0MMI_Q_TRACKCOND_ACTION_END [1] = 0MMI_NID_TRACKCOND[2] = 5MMI_Q_TRACKCOND_STEP[2] = 3MMI_M_TRACKCOND_TYPE[2] = 9MMI_Q_TRACKCOND_ACTION_START[2] = 0MMI_Q_TRACKCOND_ACTION_END [2] = 1");
            TraceReport("Expected Result");
            TraceInfo(
                "Received packet information is stored to internal memory, the symbol in sub-area B3-B5 still not changed");
            /*
            Test Step 2
            Action: Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 1MMI_N_TRACKCONDITIONS = 3MMI_NID_TRACKCOND[0] = 3MMI_Q_TRACKCOND_STEP[0] = 1MMI_M_TRACKCOND_TYPE[0] = 9MMI_Q_TRACKCOND_ACTION_START[0] = 1MMI_Q_TRACKCOND_ACTION_END [0] = 0MMI_NID_TRACKCOND[1] = 4MMI_Q_TRACKCOND_STEP[1] = 2MMI_M_TRACKCOND_TYPE[1] = 9MMI_Q_TRACKCOND_ACTION_START[1] = 0MMI_Q_TRACKCOND_ACTION_END [1] = 0MMI_NID_TRACKCOND[2] = 5MMI_Q_TRACKCOND_STEP[2] = 3MMI_M_TRACKCOND_TYPE[2] = 9MMI_Q_TRACKCOND_ACTION_START[2] = 0MMI_Q_TRACKCOND_ACTION_END [2] = 1
            Expected Result: Received packet information is stored to internal memory, the symbol in sub-area B3-B5 still not changed
            */

            TraceHeader("Test Step 3");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 1MMI_N_TRACKCONDITIONS = 3MMI_NID_TRACKCOND[0] = 6MMI_Q_TRACKCOND_STEP[0] = 1MMI_M_TRACKCOND_TYPE[0] = 0MMI_Q_TRACKCOND_ACTION_START[0] = 1MMI_Q_TRACKCOND_ACTION_END [0] = 0MMI_NID_TRACKCOND[1] = 7MMI_Q_TRACKCOND_STEP[1] = 2MMI_M_TRACKCOND_TYPE[1] = 4MMI_Q_TRACKCOND_ACTION_START[1] = 1MMI_Q_TRACKCOND_ACTION_END [1] = 0MMI_NID_TRACKCOND[2] = 8MMI_Q_TRACKCOND_STEP[2] = 1MMI_M_TRACKCOND_TYPE[2] = 6MMI_Q_TRACKCOND_ACTION_START[2] = 1MMI_Q_TRACKCOND_ACTION_END [2] = 0");
            TraceReport("Expected Result");
            TraceInfo(
                "Received packet information is stored to internal memory, the symbol in sub-area B3-B5 still not changed");
            /*
            Test Step 3
            Action: Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 1MMI_N_TRACKCONDITIONS = 3MMI_NID_TRACKCOND[0] = 6MMI_Q_TRACKCOND_STEP[0] = 1MMI_M_TRACKCOND_TYPE[0] = 0MMI_Q_TRACKCOND_ACTION_START[0] = 1MMI_Q_TRACKCOND_ACTION_END [0] = 0MMI_NID_TRACKCOND[1] = 7MMI_Q_TRACKCOND_STEP[1] = 2MMI_M_TRACKCOND_TYPE[1] = 4MMI_Q_TRACKCOND_ACTION_START[1] = 1MMI_Q_TRACKCOND_ACTION_END [1] = 0MMI_NID_TRACKCOND[2] = 8MMI_Q_TRACKCOND_STEP[2] = 1MMI_M_TRACKCOND_TYPE[2] = 6MMI_Q_TRACKCOND_ACTION_START[2] = 1MMI_Q_TRACKCOND_ACTION_END [2] = 0
            Expected Result: Received packet information is stored to internal memory, the symbol in sub-area B3-B5 still not changed
            Test Step Comment: Note: The value of variables in EVC-32 for symbol TC12 is changed refer to NCR arn_043#3617.
            */

            TraceHeader("Test Step 4");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 1MMI_N_TRACKCONDITIONS = 3MMI_NID_TRACKCOND[0] = 9MMI_Q_TRACKCOND_STEP[0] = 2MMI_M_TRACKCOND_TYPE[0] = 6MMI_Q_TRACKCOND_ACTION_START[0] = 0MMI_Q_TRACKCOND_ACTION_END [0] = 0MMI_NID_TRACKCOND[1] = 10MMI_Q_TRACKCOND_STEP[1] = 1MMI_M_TRACKCOND_TYPE[1] = 7MMI_Q_TRACKCOND_ACTION_START[1] = 1MMI_Q_TRACKCOND_ACTION_END [1] = 0MMI_NID_TRACKCOND[2] = 11MMI_Q_TRACKCOND_STEP[2] = 2MMI_M_TRACKCOND_TYPE[2] = 7MMI_Q_TRACKCOND_ACTION_START[2] = 0MMI_Q_TRACKCOND_ACTION_END [2] = 0");
            TraceReport("Expected Result");
            TraceInfo(
                "Received packet information is stored to internal memory, the symbol in sub-area B3-B5 still not changed");
            /*
            Test Step 4
            Action: Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 1MMI_N_TRACKCONDITIONS = 3MMI_NID_TRACKCOND[0] = 9MMI_Q_TRACKCOND_STEP[0] = 2MMI_M_TRACKCOND_TYPE[0] = 6MMI_Q_TRACKCOND_ACTION_START[0] = 0MMI_Q_TRACKCOND_ACTION_END [0] = 0MMI_NID_TRACKCOND[1] = 10MMI_Q_TRACKCOND_STEP[1] = 1MMI_M_TRACKCOND_TYPE[1] = 7MMI_Q_TRACKCOND_ACTION_START[1] = 1MMI_Q_TRACKCOND_ACTION_END [1] = 0MMI_NID_TRACKCOND[2] = 11MMI_Q_TRACKCOND_STEP[2] = 2MMI_M_TRACKCOND_TYPE[2] = 7MMI_Q_TRACKCOND_ACTION_START[2] = 0MMI_Q_TRACKCOND_ACTION_END [2] = 0
            Expected Result: Received packet information is stored to internal memory, the symbol in sub-area B3-B5 still not changed
            */

            TraceHeader("Test Step 5");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 1MMI_N_TRACKCONDITIONS = 3MMI_NID_TRACKCOND[0] = 12MMI_Q_TRACKCOND_STEP[0] = 1MMI_M_TRACKCOND_TYPE[0] = 8MMI_Q_TRACKCOND_ACTION_START[0] = 1MMI_Q_TRACKCOND_ACTION_END [0] = 0MMI_NID_TRACKCOND[1] = 13MMI_Q_TRACKCOND_STEP[1] = 2MMI_M_TRACKCOND_TYPE[1] = 8MMI_Q_TRACKCOND_ACTION_START[1] = 0MMI_Q_TRACKCOND_ACTION_END [1] = 0MMI_NID_TRACKCOND[2] = 14MMI_Q_TRACKCOND_STEP[2] = 1MMI_M_TRACKCOND_TYPE[2] = 5MMI_Q_TRACKCOND_ACTION_START[2] = 1MMI_Q_TRACKCOND_ACTION_END [2] = 0");
            TraceReport("Expected Result");
            TraceInfo(
                "Received packet information is stored to internal memory, the symbol in sub-area B3-B5 still not changed");
            /*
            Test Step 5
            Action: Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 1MMI_N_TRACKCONDITIONS = 3MMI_NID_TRACKCOND[0] = 12MMI_Q_TRACKCOND_STEP[0] = 1MMI_M_TRACKCOND_TYPE[0] = 8MMI_Q_TRACKCOND_ACTION_START[0] = 1MMI_Q_TRACKCOND_ACTION_END [0] = 0MMI_NID_TRACKCOND[1] = 13MMI_Q_TRACKCOND_STEP[1] = 2MMI_M_TRACKCOND_TYPE[1] = 8MMI_Q_TRACKCOND_ACTION_START[1] = 0MMI_Q_TRACKCOND_ACTION_END [1] = 0MMI_NID_TRACKCOND[2] = 14MMI_Q_TRACKCOND_STEP[2] = 1MMI_M_TRACKCOND_TYPE[2] = 5MMI_Q_TRACKCOND_ACTION_START[2] = 1MMI_Q_TRACKCOND_ACTION_END [2] = 0
            Expected Result: Received packet information is stored to internal memory, the symbol in sub-area B3-B5 still not changed
            */

            TraceHeader("Test Step 6");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 1MMI_N_TRACKCONDITIONS = 3MMI_NID_TRACKCOND[0] = 15MMI_Q_TRACKCOND_STEP[0] = 2MMI_M_TRACKCOND_TYPE[0] = 5MMI_Q_TRACKCOND_ACTION_START[0] = 0MMI_Q_TRACKCOND_ACTION_END [0] = 0MMI_NID_TRACKCOND[1] = 16MMI_Q_TRACKCOND_STEP[1] = 3MMI_M_TRACKCOND_TYPE[1] = 5MMI_Q_TRACKCOND_ACTION_START[1] = 0MMI_Q_TRACKCOND_ACTION_END [1] = 1MMI_NID_TRACKCOND[2] = 17MMI_Q_TRACKCOND_STEP[2] = 1MMI_M_TRACKCOND_TYPE[2] = 10MMI_Q_TRACKCOND_ACTION_START[2] = 1MMI_Q_TRACKCOND_ACTION_END [2] = 0");
            TraceReport("Expected Result");
            TraceInfo(
                "Received packet information is stored to internal memory, the symbol in sub-area B3-B5 still not changed");
            /*
            Test Step 6
            Action: Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 1MMI_N_TRACKCONDITIONS = 3MMI_NID_TRACKCOND[0] = 15MMI_Q_TRACKCOND_STEP[0] = 2MMI_M_TRACKCOND_TYPE[0] = 5MMI_Q_TRACKCOND_ACTION_START[0] = 0MMI_Q_TRACKCOND_ACTION_END [0] = 0MMI_NID_TRACKCOND[1] = 16MMI_Q_TRACKCOND_STEP[1] = 3MMI_M_TRACKCOND_TYPE[1] = 5MMI_Q_TRACKCOND_ACTION_START[1] = 0MMI_Q_TRACKCOND_ACTION_END [1] = 1MMI_NID_TRACKCOND[2] = 17MMI_Q_TRACKCOND_STEP[2] = 1MMI_M_TRACKCOND_TYPE[2] = 10MMI_Q_TRACKCOND_ACTION_START[2] = 1MMI_Q_TRACKCOND_ACTION_END [2] = 0
            Expected Result: Received packet information is stored to internal memory, the symbol in sub-area B3-B5 still not changed
            */

            TraceHeader("Test Step 7");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 1MMI_N_TRACKCONDITIONS = 3MMI_NID_TRACKCOND[0] = 18MMI_Q_TRACKCOND_STEP[0] = 2MMI_M_TRACKCOND_TYPE[0] = 10MMI_Q_TRACKCOND_ACTION_START[0] = 0MMI_Q_TRACKCOND_ACTION_END [0] = 0MMI_NID_TRACKCOND[1] = 19MMI_Q_TRACKCOND_STEP[1] = 1MMI_M_TRACKCOND_TYPE[1] = 11MMI_Q_TRACKCOND_ACTION_START[1] = 1MMI_Q_TRACKCOND_ACTION_END [1] = 0MMI_NID_TRACKCOND[2] = 20MMI_Q_TRACKCOND_STEP[2] = 2MMI_M_TRACKCOND_TYPE[2] = 11MMI_Q_TRACKCOND_ACTION_START[2] = 0MMI_Q_TRACKCOND_ACTION_END [2] = 0");
            TraceReport("Expected Result");
            TraceInfo(
                "Received packet information is stored to internal memory, the symbol in sub-area B3-B5 still not changed");
            /*
            Test Step 7
            Action: Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 1MMI_N_TRACKCONDITIONS = 3MMI_NID_TRACKCOND[0] = 18MMI_Q_TRACKCOND_STEP[0] = 2MMI_M_TRACKCOND_TYPE[0] = 10MMI_Q_TRACKCOND_ACTION_START[0] = 0MMI_Q_TRACKCOND_ACTION_END [0] = 0MMI_NID_TRACKCOND[1] = 19MMI_Q_TRACKCOND_STEP[1] = 1MMI_M_TRACKCOND_TYPE[1] = 11MMI_Q_TRACKCOND_ACTION_START[1] = 1MMI_Q_TRACKCOND_ACTION_END [1] = 0MMI_NID_TRACKCOND[2] = 20MMI_Q_TRACKCOND_STEP[2] = 2MMI_M_TRACKCOND_TYPE[2] = 11MMI_Q_TRACKCOND_ACTION_START[2] = 0MMI_Q_TRACKCOND_ACTION_END [2] = 0
            Expected Result: Received packet information is stored to internal memory, the symbol in sub-area B3-B5 still not changed
            */

            TraceHeader("Test Step 8");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 1MMI_N_TRACKCONDITIONS = 3MMI_NID_TRACKCOND[0] = 21MMI_Q_TRACKCOND_STEP[0] = 1MMI_M_TRACKCOND_TYPE[0] = 12MMI_Q_TRACKCOND_ACTION_START[0] = 1MMI_Q_TRACKCOND_ACTION_END [0] = 0MMI_NID_TRACKCOND[1] = 22MMI_Q_TRACKCOND_STEP[1] = 2MMI_M_TRACKCOND_TYPE[1] = 12MMI_Q_TRACKCOND_ACTION_START[1] = 0MMI_Q_TRACKCOND_ACTION_END [1] = 0MMI_NID_TRACKCOND[2] = 23MMI_Q_TRACKCOND_STEP[2] = 1MMI_M_TRACKCOND_TYPE[2] = 13MMI_Q_TRACKCOND_ACTION_START[2] = 1MMI_Q_TRACKCOND_ACTION_END [2] = 0");
            TraceReport("Expected Result");
            TraceInfo(
                "Received packet information is stored to internal memory, the symbol in sub-area B3-B5 still not changed");
            /*
            Test Step 8
            Action: Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 1MMI_N_TRACKCONDITIONS = 3MMI_NID_TRACKCOND[0] = 21MMI_Q_TRACKCOND_STEP[0] = 1MMI_M_TRACKCOND_TYPE[0] = 12MMI_Q_TRACKCOND_ACTION_START[0] = 1MMI_Q_TRACKCOND_ACTION_END [0] = 0MMI_NID_TRACKCOND[1] = 22MMI_Q_TRACKCOND_STEP[1] = 2MMI_M_TRACKCOND_TYPE[1] = 12MMI_Q_TRACKCOND_ACTION_START[1] = 0MMI_Q_TRACKCOND_ACTION_END [1] = 0MMI_NID_TRACKCOND[2] = 23MMI_Q_TRACKCOND_STEP[2] = 1MMI_M_TRACKCOND_TYPE[2] = 13MMI_Q_TRACKCOND_ACTION_START[2] = 1MMI_Q_TRACKCOND_ACTION_END [2] = 0
            Expected Result: Received packet information is stored to internal memory, the symbol in sub-area B3-B5 still not changed
            */

            TraceHeader("Test Step 9");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 1MMI_N_TRACKCONDITIONS = 3MMI_NID_TRACKCOND[0] = 24MMI_Q_TRACKCOND_STEP[0] = 2MMI_M_TRACKCOND_TYPE[0] = 13MMI_Q_TRACKCOND_ACTION_START[0] = 0MMI_Q_TRACKCOND_ACTION_END [0] = 0MMI_NID_TRACKCOND[1] = 25MMI_Q_TRACKCOND_STEP[1] = 1MMI_M_TRACKCOND_TYPE[1] = 14MMI_Q_TRACKCOND_ACTION_START[1] = 1MMI_Q_TRACKCOND_ACTION_END [1] = 0MMI_NID_TRACKCOND[2] = 26MMI_Q_TRACKCOND_STEP[2] = 2MMI_M_TRACKCOND_TYPE[2] = 14MMI_Q_TRACKCOND_ACTION_START[2] = 0MMI_Q_TRACKCOND_ACTION_END [2] = 0");
            TraceReport("Expected Result");
            TraceInfo(
                "Received packet information is stored to internal memory, the symbol in sub-area B3-B5 still not changed");
            /*
            Test Step 9
            Action: Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 1MMI_N_TRACKCONDITIONS = 3MMI_NID_TRACKCOND[0] = 24MMI_Q_TRACKCOND_STEP[0] = 2MMI_M_TRACKCOND_TYPE[0] = 13MMI_Q_TRACKCOND_ACTION_START[0] = 0MMI_Q_TRACKCOND_ACTION_END [0] = 0MMI_NID_TRACKCOND[1] = 25MMI_Q_TRACKCOND_STEP[1] = 1MMI_M_TRACKCOND_TYPE[1] = 14MMI_Q_TRACKCOND_ACTION_START[1] = 1MMI_Q_TRACKCOND_ACTION_END [1] = 0MMI_NID_TRACKCOND[2] = 26MMI_Q_TRACKCOND_STEP[2] = 2MMI_M_TRACKCOND_TYPE[2] = 14MMI_Q_TRACKCOND_ACTION_START[2] = 0MMI_Q_TRACKCOND_ACTION_END [2] = 0
            Expected Result: Received packet information is stored to internal memory, the symbol in sub-area B3-B5 still not changed
            */

            TraceHeader("Test Step 10");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 1MMI_N_TRACKCONDITIONS = 3MMI_NID_TRACKCOND[0] = 27MMI_Q_TRACKCOND_STEP[0] = 1MMI_M_TRACKCOND_TYPE[0] = 15MMI_Q_TRACKCOND_ACTION_START[0] = 1MMI_Q_TRACKCOND_ACTION_END [0] = 0MMI_NID_TRACKCOND[1] = 28MMI_Q_TRACKCOND_STEP[1] = 2MMI_M_TRACKCOND_TYPE[1] = 15MMI_Q_TRACKCOND_ACTION_START[1] = 0MMI_Q_TRACKCOND_ACTION_END [1] = 0MMI_NID_TRACKCOND[2] = 29MMI_Q_TRACKCOND_STEP[2] = 2MMI_M_TRACKCOND_TYPE[2] = 1MMI_Q_TRACKCOND_ACTION_START[2] = 0MMI_Q_TRACKCOND_ACTION_END [2] = 0");
            TraceReport("Expected Result");
            TraceInfo(
                "Received packet information is stored to internal memory, the symbol in sub-area B3-B5 still not changed.Verify the following information,(1)   The symbol TC36 is display in sub-area C2");
            /*
            Test Step 10
            Action: Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 1MMI_N_TRACKCONDITIONS = 3MMI_NID_TRACKCOND[0] = 27MMI_Q_TRACKCOND_STEP[0] = 1MMI_M_TRACKCOND_TYPE[0] = 15MMI_Q_TRACKCOND_ACTION_START[0] = 1MMI_Q_TRACKCOND_ACTION_END [0] = 0MMI_NID_TRACKCOND[1] = 28MMI_Q_TRACKCOND_STEP[1] = 2MMI_M_TRACKCOND_TYPE[1] = 15MMI_Q_TRACKCOND_ACTION_START[1] = 0MMI_Q_TRACKCOND_ACTION_END [1] = 0MMI_NID_TRACKCOND[2] = 29MMI_Q_TRACKCOND_STEP[2] = 2MMI_M_TRACKCOND_TYPE[2] = 1MMI_Q_TRACKCOND_ACTION_START[2] = 0MMI_Q_TRACKCOND_ACTION_END [2] = 0
            Expected Result: Received packet information is stored to internal memory, the symbol in sub-area B3-B5 still not changed.Verify the following information,(1)   The symbol TC36 is display in sub-area C2
            Test Step Comment: (1) MMI_gen 10470; MMI_gen 662 (partly: TC36); MMI_gen 10465 (partly: TC36);
            */

            TraceHeader("Test Step 11");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 1MMI_N_TRACKCONDITIONS = 1MMI_NID_TRACKCOND = 30MMI_Q_TRACKCOND_STEP = 1MMI_M_TRACKCOND_TYPE = 3MMI_Q_TRACKCOND_ACTION_START = 0MMI_Q_TRACKCOND_ACTION_END  = 0");
            TraceReport("Expected Result");
            TraceInfo(
                "Received packet information is stored to internal memory, the symbol in sub-area B3-B5 still not changed");
            /*
            Test Step 11
            Action: Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 1MMI_N_TRACKCONDITIONS = 1MMI_NID_TRACKCOND = 30MMI_Q_TRACKCOND_STEP = 1MMI_M_TRACKCOND_TYPE = 3MMI_Q_TRACKCOND_ACTION_START = 0MMI_Q_TRACKCOND_ACTION_END  = 0
            Expected Result: Received packet information is stored to internal memory, the symbol in sub-area B3-B5 still not changed
            */

            // Steps 12 to 41 are in XML_18_6_2_b.cs
            TraceHeader("Test Step 12");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Use a second test script file 18_6_2_b.xml to Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 1MMI_N_TRACKCONDITIONS = 1MMI_NID_TRACKCOND = 0MMI_Q_TRACKCOND_STEP = 4");
            TraceReport("Expected Result");
            TraceInfo(
                "The symbol TC02 is removed from sub-area B3.Verify the following information,(1)   The 3 symbols are correctly  displayed in the specified location below,The symbol TC01 is display in sub-area B3.The symbol TC04 is display in sub-area B4.The symbol TC06 is display in sub-area B5");
            /*
            Test Step 12
            Action: Use a second test script file 18_6_2_b.xml to Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 1MMI_N_TRACKCONDITIONS = 1MMI_NID_TRACKCOND = 0MMI_Q_TRACKCOND_STEP = 4
            Expected Result: The symbol TC02 is removed from sub-area B3.Verify the following information,(1)   The 3 symbols are correctly  displayed in the specified location below,The symbol TC01 is display in sub-area B3.The symbol TC04 is display in sub-area B4.The symbol TC06 is display in sub-area B5
            Test Step Comment: (1) MMI_gen 10470; MMI_gen 662 (partly: TC06); MMI_gen 667; MMI_gen 10465 (partly: TC06); MMI_gen 10467;
            */
            XML_18_6_2(msgType.typeb);

            TraceHeader("Test Step 13");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 1MMI_N_TRACKCONDITIONS = 1MMI_NID_TRACKCOND = 1MMI_Q_TRACKCOND_STEP = 4");
            TraceReport("Expected Result");
            TraceInfo(
                "The symbol TC01 is removed from sub-area B3.Verify the following information,(1)   The 3 symbols are correctly  displayed in the specified location below,The symbol TC04 is display in sub-area B3.The symbol TC06 is display in sub-area B4.The symbol TC06 is display in sub-area B5");
            /*
            Test Step 13
            Action: Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 1MMI_N_TRACKCONDITIONS = 1MMI_NID_TRACKCOND = 1MMI_Q_TRACKCOND_STEP = 4
            Expected Result: The symbol TC01 is removed from sub-area B3.Verify the following information,(1)   The 3 symbols are correctly  displayed in the specified location below,The symbol TC04 is display in sub-area B3.The symbol TC06 is display in sub-area B4.The symbol TC06 is display in sub-area B5
            Test Step Comment: (1) MMI_gen 10470; MMI_gen 662 (partly: TC06); MMI_gen 667; MMI_gen 10465 (partly: TC06); MMI_gen 10467;
            */


            TraceHeader("Test Step 14");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 1MMI_N_TRACKCONDITIONS = 1MMI_NID_TRACKCOND = 2MMI_Q_TRACKCOND_STEP = 4");
            TraceReport("Expected Result");
            TraceInfo(
                "The symbol TC04 is removed from sub-area B3.Verify the following information,(1)   The 3 symbols are correctly  displayed in the specified location below,The symbol TC06 is display in sub-area B3.The symbol TC06 is display in sub-area B4.The symbol TC08 is display in sub-area B5");
            /*
            Test Step 14
            Action: Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 1MMI_N_TRACKCONDITIONS = 1MMI_NID_TRACKCOND = 2MMI_Q_TRACKCOND_STEP = 4
            Expected Result: The symbol TC04 is removed from sub-area B3.Verify the following information,(1)   The 3 symbols are correctly  displayed in the specified location below,The symbol TC06 is display in sub-area B3.The symbol TC06 is display in sub-area B4.The symbol TC08 is display in sub-area B5
            Test Step Comment: (1) MMI_gen 10470; MMI_gen 662 (partly: TC08); MMI_gen 667; MMI_gen 10465 (partly: TC08); MMI_gen 10467;
            */


            TraceHeader("Test Step 15");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 1MMI_N_TRACKCONDITIONS = 1MMI_NID_TRACKCOND = 3MMI_Q_TRACKCOND_STEP = 4");
            TraceReport("Expected Result");
            TraceInfo(
                "The symbol TC06 is removed from sub-area B3.Verify the following information,(1)   The 3 symbols are correctly  displayed in the specified location below,The symbol TC06 is display in sub-area B3.The symbol TC08 is display in sub-area B4.The symbol TC10 is display in sub-area B5");
            /*
            Test Step 15
            Action: Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 1MMI_N_TRACKCONDITIONS = 1MMI_NID_TRACKCOND = 3MMI_Q_TRACKCOND_STEP = 4
            Expected Result: The symbol TC06 is removed from sub-area B3.Verify the following information,(1)   The 3 symbols are correctly  displayed in the specified location below,The symbol TC06 is display in sub-area B3.The symbol TC08 is display in sub-area B4.The symbol TC10 is display in sub-area B5
            Test Step Comment: (1) MMI_gen 10470; MMI_gen 662 (partly: TC10); MMI_gen 667; MMI_gen 10465 (partly: TC10); MMI_gen 10467;
            */

            TraceHeader("Test Step 16");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 1MMI_N_TRACKCONDITIONS = 1MMI_NID_TRACKCOND = 4MMI_Q_TRACKCOND_STEP = 4");
            TraceReport("Expected Result");
            TraceInfo(
                "The symbol TC06 is removed from sub-area B3.Verify the following information,(1)   The 3 symbols are correctly  displayed in the specified location below,The symbol TC08 is display in sub-area B3.The symbol TC10 is display in sub-area B4.The symbol TC12 is display in sub-area B5");
            /*
            Test Step 16
            Action: Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 1MMI_N_TRACKCONDITIONS = 1MMI_NID_TRACKCOND = 4MMI_Q_TRACKCOND_STEP = 4
            Expected Result: The symbol TC06 is removed from sub-area B3.Verify the following information,(1)   The 3 symbols are correctly  displayed in the specified location below,The symbol TC08 is display in sub-area B3.The symbol TC10 is display in sub-area B4.The symbol TC12 is display in sub-area B5
            Test Step Comment: (1) MMI_gen 10470; MMI_gen 662 (partly: TC12); MMI_gen 667; MMI_gen 10465 (partly: TC12); MMI_gen 10467;Note: The value of variables in EVC-32 for symbol TC12 is changed refer to NCR arn_043#3617.
            */

            TraceHeader("Test Step 17");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 1MMI_N_TRACKCONDITIONS = 1MMI_NID_TRACKCOND = 5MMI_Q_TRACKCOND_STEP = 4");
            TraceReport("Expected Result");
            TraceInfo(
                "The symbol TC08 is removed from sub-area B3.Verify the following information,(1)   The 3 symbols are correctly  displayed in the specified location below,The symbol TC10 is display in sub-area B3.The symbol TC12 is display in sub-area B4.The symbol TC13 is display in sub-area B5");
            /*
            Test Step 17
            Action: Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 1MMI_N_TRACKCONDITIONS = 1MMI_NID_TRACKCOND = 5MMI_Q_TRACKCOND_STEP = 4
            Expected Result: The symbol TC08 is removed from sub-area B3.Verify the following information,(1)   The 3 symbols are correctly  displayed in the specified location below,The symbol TC10 is display in sub-area B3.The symbol TC12 is display in sub-area B4.The symbol TC13 is display in sub-area B5
            Test Step Comment: (1) MMI_gen 10470; MMI_gen 662 (partly: TC13); MMI_gen 667; MMI_gen 10465 (partly: TC13); MMI_gen 10467;
            */

            TraceHeader("Test Step 18");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 1MMI_N_TRACKCONDITIONS = 1MMI_NID_TRACKCOND = 6MMI_Q_TRACKCOND_STEP = 4");
            TraceReport("Expected Result");
            TraceInfo(
                "The symbol TC10 is removed from sub-area B3.Verify the following information,(1)   The 3 symbols are correctly  displayed in the specified location below,The symbol TC12 is display in sub-area B3.The symbol TC13 is display in sub-area B4.The symbol TC13 is display in sub-area B5");
            /*
            Test Step 18
            Action: Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 1MMI_N_TRACKCONDITIONS = 1MMI_NID_TRACKCOND = 6MMI_Q_TRACKCOND_STEP = 4
            Expected Result: The symbol TC10 is removed from sub-area B3.Verify the following information,(1)   The 3 symbols are correctly  displayed in the specified location below,The symbol TC12 is display in sub-area B3.The symbol TC13 is display in sub-area B4.The symbol TC13 is display in sub-area B5
            Test Step Comment: (1) MMI_gen 10470; MMI_gen 662 (partly: TC13); MMI_gen 667; MMI_gen 10465 (partly: TC13); MMI_gen 10467;
            */

            TraceHeader("Test Step 19");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 1MMI_N_TRACKCONDITIONS = 1MMI_NID_TRACKCOND = 7MMI_Q_TRACKCOND_STEP = 4");
            TraceReport("Expected Result");
            TraceInfo(
                "The symbol TC12 is removed from sub-area B3.Verify the following information,(1)   The 3 symbols are correctly  displayed in the specified location below,The symbol TC13 is display in sub-area B3.The symbol TC13 is display in sub-area B4.The symbol TC15 is display in sub-area B5");
            /*
            Test Step 19
            Action: Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 1MMI_N_TRACKCONDITIONS = 1MMI_NID_TRACKCOND = 7MMI_Q_TRACKCOND_STEP = 4
            Expected Result: The symbol TC12 is removed from sub-area B3.Verify the following information,(1)   The 3 symbols are correctly  displayed in the specified location below,The symbol TC13 is display in sub-area B3.The symbol TC13 is display in sub-area B4.The symbol TC15 is display in sub-area B5
            Test Step Comment: (1) MMI_gen 10470; MMI_gen 662 (partly: TC15); MMI_gen 667; MMI_gen 10465 (partly: TC15); MMI_gen 10467;
            */

            TraceHeader("Test Step 20");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 1MMI_N_TRACKCONDITIONS = 1MMI_NID_TRACKCOND = 8MMI_Q_TRACKCOND_STEP = 4");
            TraceReport("Expected Result");
            TraceInfo(
                "The symbol TC13 is removed from sub-area B3.Verify the following information,(1)   The 3 symbols are correctly  displayed in the specified location below,The symbol TC13 is display in sub-area B3.The symbol TC15 is display in sub-area B4.The symbol TC15 is display in sub-area B5");
            /*
            Test Step 20
            Action: Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 1MMI_N_TRACKCONDITIONS = 1MMI_NID_TRACKCOND = 8MMI_Q_TRACKCOND_STEP = 4
            Expected Result: The symbol TC13 is removed from sub-area B3.Verify the following information,(1)   The 3 symbols are correctly  displayed in the specified location below,The symbol TC13 is display in sub-area B3.The symbol TC15 is display in sub-area B4.The symbol TC15 is display in sub-area B5
            Test Step Comment: (1) MMI_gen 10470; MMI_gen 662 (partly: TC15); MMI_gen 667; MMI_gen 10467;
            */

            TraceHeader("Test Step 21");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 1MMI_N_TRACKCONDITIONS = 1MMI_NID_TRACKCOND = 9MMI_Q_TRACKCOND_STEP = 4");
            TraceReport("Expected Result");
            TraceInfo(
                "The symbol TC13 is removed from sub-area B3.Verify the following information,(1)   The 3 symbols are correctly  displayed in the specified location below,The symbol TC15 is display in sub-area B3.The symbol TC15 is display in sub-area B4.The symbol TC17 is display in sub-area B5");
            /*
            Test Step 21
            Action: Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 1MMI_N_TRACKCONDITIONS = 1MMI_NID_TRACKCOND = 9MMI_Q_TRACKCOND_STEP = 4
            Expected Result: The symbol TC13 is removed from sub-area B3.Verify the following information,(1)   The 3 symbols are correctly  displayed in the specified location below,The symbol TC15 is display in sub-area B3.The symbol TC15 is display in sub-area B4.The symbol TC17 is display in sub-area B5
            Test Step Comment: (1) MMI_gen 10470; MMI_gen 662 (partly: TC17); MMI_gen 667; MMI_gen 10465 (partly: TC17); MMI_gen 10467;
            */

            TraceHeader("Test Step 22");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 1MMI_N_TRACKCONDITIONS = 1MMI_NID_TRACKCOND = 10MMI_Q_TRACKCOND_STEP = 4");
            TraceReport("Expected Result");
            TraceInfo(
                "The symbol TC15 is removed from sub-area B3.Verify the following information,(1)   The 3 symbols are correctly  displayed in the specified location below,The symbol TC15 is display in sub-area B3.The symbol TC17 is display in sub-area B4.The symbol TC17 is display in sub-area B5");
            /*
            Test Step 22
            Action: Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 1MMI_N_TRACKCONDITIONS = 1MMI_NID_TRACKCOND = 10MMI_Q_TRACKCOND_STEP = 4
            Expected Result: The symbol TC15 is removed from sub-area B3.Verify the following information,(1)   The 3 symbols are correctly  displayed in the specified location below,The symbol TC15 is display in sub-area B3.The symbol TC17 is display in sub-area B4.The symbol TC17 is display in sub-area B5
            Test Step Comment: (1) MMI_gen 10470; MMI_gen 662 (partly: TC17); MMI_gen 667; MMI_gen 10465 (partly: TC17); MMI_gen 10467;
            */

            TraceHeader("Test Step 23");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 1MMI_N_TRACKCONDITIONS = 1MMI_NID_TRACKCOND = 11MMI_Q_TRACKCOND_STEP = 4");
            TraceReport("Expected Result");
            TraceInfo(
                "The symbol TC15 is removed from sub-area B3.Verify the following information,(1)   The 3 symbols are correctly  displayed in the specified location below,The symbol TC17 is display in sub-area B3.The symbol TC17 is display in sub-area B4.The symbol TC19 is display in sub-area B5");
            /*
            Test Step 23
            Action: Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 1MMI_N_TRACKCONDITIONS = 1MMI_NID_TRACKCOND = 11MMI_Q_TRACKCOND_STEP = 4
            Expected Result: The symbol TC15 is removed from sub-area B3.Verify the following information,(1)   The 3 symbols are correctly  displayed in the specified location below,The symbol TC17 is display in sub-area B3.The symbol TC17 is display in sub-area B4.The symbol TC19 is display in sub-area B5
            Test Step Comment: (1) MMI_gen 10470; MMI_gen 662 (partly: TC19); MMI_gen 667; MMI_gen 10465 (partly: TC19); MMI_gen 10467;
            */

            TraceHeader("Test Step 24");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 1MMI_N_TRACKCONDITIONS = 1MMI_NID_TRACKCOND = 12MMI_Q_TRACKCOND_STEP = 4");
            TraceReport("Expected Result");
            TraceInfo(
                "The symbol TC17 is removed from sub-area B3.Verify the following information,(1)   The 3 symbols are correctly  displayed in the specified location below,The symbol TC17 is display in sub-area B3.The symbol TC19 is display in sub-area B4.The symbol TC19 is display in sub-area B5");
            /*
            Test Step 24
            Action: Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 1MMI_N_TRACKCONDITIONS = 1MMI_NID_TRACKCOND = 12MMI_Q_TRACKCOND_STEP = 4
            Expected Result: The symbol TC17 is removed from sub-area B3.Verify the following information,(1)   The 3 symbols are correctly  displayed in the specified location below,The symbol TC17 is display in sub-area B3.The symbol TC19 is display in sub-area B4.The symbol TC19 is display in sub-area B5
            Test Step Comment: (1) MMI_gen 10470; MMI_gen 662 (partly: TC19); MMI_gen 667; MMI_gen 10465 (partly: TC19); MMI_gen 10467;
            */

            TraceHeader("Test Step 25");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 1MMI_N_TRACKCONDITIONS = 1MMI_NID_TRACKCOND = 13MMI_Q_TRACKCOND_STEP = 4");
            TraceReport("Expected Result");
            TraceInfo(
                "The symbol TC17 is removed from sub-area B3.Verify the following information,(1)   The 3 symbols are correctly  displayed in the specified location below,The symbol TC19 is display in sub-area B3.The symbol TC19 is display in sub-area B4.The symbol TC20 is display in sub-area B5");
            /*
            Test Step 25
            Action: Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 1MMI_N_TRACKCONDITIONS = 1MMI_NID_TRACKCOND = 13MMI_Q_TRACKCOND_STEP = 4
            Expected Result: The symbol TC17 is removed from sub-area B3.Verify the following information,(1)   The 3 symbols are correctly  displayed in the specified location below,The symbol TC19 is display in sub-area B3.The symbol TC19 is display in sub-area B4.The symbol TC20 is display in sub-area B5
            Test Step Comment: (1) MMI_gen 10470; MMI_gen 662 (partly: TC20); MMI_gen 667; MMI_gen 10465 (partly: TC20); MMI_gen 10467;
            */

            TraceHeader("Test Step 26");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 1MMI_N_TRACKCONDITIONS = 1MMI_NID_TRACKCOND = 14MMI_Q_TRACKCOND_STEP = 4");
            TraceReport("Expected Result");
            TraceInfo(
                "The symbol TC19 is removed from sub-area B3.Verify the following information,(1)   The 3 symbols are correctly  displayed in the specified location below,The symbol TC19 is display in sub-area B3.The symbol TC20 is display in sub-area B4.The symbol TC23 is display in sub-area B5");
            /*
            Test Step 26
            Action: Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 1MMI_N_TRACKCONDITIONS = 1MMI_NID_TRACKCOND = 14MMI_Q_TRACKCOND_STEP = 4
            Expected Result: The symbol TC19 is removed from sub-area B3.Verify the following information,(1)   The 3 symbols are correctly  displayed in the specified location below,The symbol TC19 is display in sub-area B3.The symbol TC20 is display in sub-area B4.The symbol TC23 is display in sub-area B5
            Test Step Comment: (1) MMI_gen 10470; MMI_gen 662 (partly: TC23); MMI_gen 667; MMI_gen 10465 (partly: TC23); MMI_gen 10467;
            */

            TraceHeader("Test Step 27");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 1MMI_N_TRACKCONDITIONS = 1MMI_NID_TRACKCOND = 15MMI_Q_TRACKCOND_STEP = 4");
            TraceReport("Expected Result");
            TraceInfo(
                "The symbol TC19 is removed from sub-area B3.Verify the following information,(1)   The 3 symbols are correctly  displayed in the specified location below,The symbol TC20 is display in sub-area B3.The symbol TC23 is display in sub-area B4.The symbol TC23 is display in sub-area B5");
            /*
            Test Step 27
            Action: Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 1MMI_N_TRACKCONDITIONS = 1MMI_NID_TRACKCOND = 15MMI_Q_TRACKCOND_STEP = 4
            Expected Result: The symbol TC19 is removed from sub-area B3.Verify the following information,(1)   The 3 symbols are correctly  displayed in the specified location below,The symbol TC20 is display in sub-area B3.The symbol TC23 is display in sub-area B4.The symbol TC23 is display in sub-area B5
            Test Step Comment: (1) MMI_gen 10470; MMI_gen 662 (partly: TC23); MMI_gen 667; MMI_gen 10465 (partly: TC23); MMI_gen 10467;
            */

            TraceHeader("Test Step 28");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 1MMI_N_TRACKCONDITIONS = 1MMI_NID_TRACKCOND = 16MMI_Q_TRACKCOND_STEP = 4");
            TraceReport("Expected Result");
            TraceInfo(
                "The symbol TC20 is removed from sub-area B3.Verify the following information,(1)   The 3 symbols are correctly  displayed in the specified location below,The symbol TC23 is display in sub-area B3.The symbol TC23 is display in sub-area B4.The symbol TC25 is display in sub-area B5");
            /*
            Test Step 28
            Action: Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 1MMI_N_TRACKCONDITIONS = 1MMI_NID_TRACKCOND = 16MMI_Q_TRACKCOND_STEP = 4
            Expected Result: The symbol TC20 is removed from sub-area B3.Verify the following information,(1)   The 3 symbols are correctly  displayed in the specified location below,The symbol TC23 is display in sub-area B3.The symbol TC23 is display in sub-area B4.The symbol TC25 is display in sub-area B5
            Test Step Comment: (1) MMI_gen 10470; MMI_gen 662 (partly: TC25); MMI_gen 667; MMI_gen 10465 (partly: TC25); MMI_gen 10467;
            */

            TraceHeader("Test Step 29");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 1MMI_N_TRACKCONDITIONS = 1MMI_NID_TRACKCOND = 17MMI_Q_TRACKCOND_STEP = 4");
            TraceReport("Expected Result");
            TraceInfo(
                "The symbol TC23 is removed from sub-area B3.Verify the following information,(1)   The 3 symbols are correctly  displayed in the specified location below,The symbol TC23 is display in sub-area B3.The symbol TC25 is display in sub-area B4.The symbol TC25 is display in sub-area B5");
            /*
            Test Step 29
            Action: Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 1MMI_N_TRACKCONDITIONS = 1MMI_NID_TRACKCOND = 17MMI_Q_TRACKCOND_STEP = 4
            Expected Result: The symbol TC23 is removed from sub-area B3.Verify the following information,(1)   The 3 symbols are correctly  displayed in the specified location below,The symbol TC23 is display in sub-area B3.The symbol TC25 is display in sub-area B4.The symbol TC25 is display in sub-area B5
            Test Step Comment: (1) MMI_gen 10470; MMI_gen 662 (partly: TC25); MMI_gen 667; MMI_gen 10465 (partly: TC25); MMI_gen 10467;
            */

            TraceHeader("Test Step 30");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 1MMI_N_TRACKCONDITIONS = 1MMI_NID_TRACKCOND = 18MMI_Q_TRACKCOND_STEP = 4");
            TraceReport("Expected Result");
            TraceInfo(
                "The symbol TC23 is removed from sub-area B3.Verify the following information,(1)   The 3 symbols are correctly  displayed in the specified location below,The symbol TC25 is display in sub-area B3.The symbol TC25 is display in sub-area B4.The symbol TC27 is display in sub-area B5");
            /*
            Test Step 30
            Action: Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 1MMI_N_TRACKCONDITIONS = 1MMI_NID_TRACKCOND = 18MMI_Q_TRACKCOND_STEP = 4
            Expected Result: The symbol TC23 is removed from sub-area B3.Verify the following information,(1)   The 3 symbols are correctly  displayed in the specified location below,The symbol TC25 is display in sub-area B3.The symbol TC25 is display in sub-area B4.The symbol TC27 is display in sub-area B5
            Test Step Comment: (1) MMI_gen 10470; MMI_gen 662 (partly: TC27); MMI_gen 667; MMI_gen 10465 (partly: TC27); MMI_gen 10467;
            */

            TraceHeader("Test Step 31");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 1MMI_N_TRACKCONDITIONS = 1MMI_NID_TRACKCOND = 19MMI_Q_TRACKCOND_STEP = 4");
            TraceReport("Expected Result");
            TraceInfo(
                "The symbol TC25 is removed from sub-area B3.Verify the following information,(1)   The 3 symbols are correctly  displayed in the specified location below,The symbol TC25 is display in sub-area B3.The symbol TC27 is display in sub-area B4.The symbol TC27 is display in sub-area B5");
            /*
            Test Step 31
            Action: Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 1MMI_N_TRACKCONDITIONS = 1MMI_NID_TRACKCOND = 19MMI_Q_TRACKCOND_STEP = 4
            Expected Result: The symbol TC25 is removed from sub-area B3.Verify the following information,(1)   The 3 symbols are correctly  displayed in the specified location below,The symbol TC25 is display in sub-area B3.The symbol TC27 is display in sub-area B4.The symbol TC27 is display in sub-area B5
            Test Step Comment: (1) MMI_gen 10470; MMI_gen 662 (partly: TC27); MMI_gen 667; MMI_gen 10465 (partly: TC27); MMI_gen 10467;
            */

            TraceHeader("Test Step 32");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 1MMI_N_TRACKCONDITIONS = 1MMI_NID_TRACKCOND = 20MMI_Q_TRACKCOND_STEP = 4");
            TraceReport("Expected Result");
            TraceInfo(
                "The symbol TC25 is removed from sub-area B3.Verify the following information,(1)   The 3 symbols are correctly  displayed in the specified location below,The symbol TC27 is display in sub-area B3.The symbol TC27 is display in sub-area B4.The symbol TC29 is display in sub-area B5");
            /*
            Test Step 32
            Action: Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 1MMI_N_TRACKCONDITIONS = 1MMI_NID_TRACKCOND = 20MMI_Q_TRACKCOND_STEP = 4
            Expected Result: The symbol TC25 is removed from sub-area B3.Verify the following information,(1)   The 3 symbols are correctly  displayed in the specified location below,The symbol TC27 is display in sub-area B3.The symbol TC27 is display in sub-area B4.The symbol TC29 is display in sub-area B5
            Test Step Comment: (1) MMI_gen 10470; MMI_gen 662 (partly: TC29); MMI_gen 667; MMI_gen 10465 (partly: TC29); MMI_gen 10467;
            */

            TraceHeader("Test Step 33");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 1MMI_N_TRACKCONDITIONS = 1MMI_NID_TRACKCOND = 21MMI_Q_TRACKCOND_STEP = 4");
            TraceReport("Expected Result");
            TraceInfo(
                "The symbol TC27 is removed from sub-area B3.Verify the following information,(1)   The 3 symbols are correctly  displayed in the specified location below,The symbol TC27 is display in sub-area B3.The symbol TC29 is display in sub-area B4.The symbol TC29 is display in sub-area B5");
            /*
            Test Step 33
            Action: Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 1MMI_N_TRACKCONDITIONS = 1MMI_NID_TRACKCOND = 21MMI_Q_TRACKCOND_STEP = 4
            Expected Result: The symbol TC27 is removed from sub-area B3.Verify the following information,(1)   The 3 symbols are correctly  displayed in the specified location below,The symbol TC27 is display in sub-area B3.The symbol TC29 is display in sub-area B4.The symbol TC29 is display in sub-area B5
            Test Step Comment: (1) MMI_gen 10470; MMI_gen 662 (partly: TC29); MMI_gen 667; MMI_gen 10465 (partly: TC29); MMI_gen 10467;
            */

            TraceHeader("Test Step 34");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 1MMI_N_TRACKCONDITIONS = 1MMI_NID_TRACKCOND = 22MMI_Q_TRACKCOND_STEP = 4");
            TraceReport("Expected Result");
            TraceInfo(
                "The symbol TC27 is removed from sub-area B3.Verify the following information,(1)   The 3 symbols are correctly  displayed in the specified location below,The symbol TC29 is display in sub-area B3.The symbol TC29 is display in sub-area B4.The symbol TC31 is display in sub-area B5");
            /*
            Test Step 34
            Action: Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 1MMI_N_TRACKCONDITIONS = 1MMI_NID_TRACKCOND = 22MMI_Q_TRACKCOND_STEP = 4
            Expected Result: The symbol TC27 is removed from sub-area B3.Verify the following information,(1)   The 3 symbols are correctly  displayed in the specified location below,The symbol TC29 is display in sub-area B3.The symbol TC29 is display in sub-area B4.The symbol TC31 is display in sub-area B5
            Test Step Comment: (1) MMI_gen 10470; MMI_gen 662 (partly: TC31); MMI_gen 667; MMI_gen 10465 (partly: TC31); MMI_gen 10467;
            */

            TraceHeader("Test Step 35");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 1MMI_N_TRACKCONDITIONS = 1MMI_NID_TRACKCOND = 23MMI_Q_TRACKCOND_STEP = 4");
            TraceReport("Expected Result");
            TraceInfo(
                "The symbol TC29 is removed from sub-area B3.Verify the following information,(1)   The 3 symbols are correctly  displayed in the specified location below,The symbol TC29 is display in sub-area B3.The symbol TC31 is display in sub-area B4.The symbol TC31 is display in sub-area B5");
            /*
            Test Step 35
            Action: Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 1MMI_N_TRACKCONDITIONS = 1MMI_NID_TRACKCOND = 23MMI_Q_TRACKCOND_STEP = 4
            Expected Result: The symbol TC29 is removed from sub-area B3.Verify the following information,(1)   The 3 symbols are correctly  displayed in the specified location below,The symbol TC29 is display in sub-area B3.The symbol TC31 is display in sub-area B4.The symbol TC31 is display in sub-area B5
            Test Step Comment: (1) MMI_gen 10470; MMI_gen 662 (partly: TC31); MMI_gen 667; MMI_gen 10465 (partly: TC31); MMI_gen 10467;
            */

            TraceHeader("Test Step 36");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 1MMI_N_TRACKCONDITIONS = 1MMI_NID_TRACKCOND = 24MMI_Q_TRACKCOND_STEP = 4");
            TraceReport("Expected Result");
            TraceInfo(
                "The symbol TC29 is removed from sub-area B3.Verify the following information,(1)   The 3 symbols are correctly  displayed in the specified location below,The symbol TC31 is display in sub-area B3.The symbol TC31 is display in sub-area B4.The symbol TC33 is display in sub-area B5");
            /*
            Test Step 36
            Action: Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 1MMI_N_TRACKCONDITIONS = 1MMI_NID_TRACKCOND = 24MMI_Q_TRACKCOND_STEP = 4
            Expected Result: The symbol TC29 is removed from sub-area B3.Verify the following information,(1)   The 3 symbols are correctly  displayed in the specified location below,The symbol TC31 is display in sub-area B3.The symbol TC31 is display in sub-area B4.The symbol TC33 is display in sub-area B5
            Test Step Comment: (1) MMI_gen 10470; MMI_gen 662 (partly: TC33); MMI_gen 667; MMI_gen 10465 (partly: TC33); MMI_gen 10467;
            */

            TraceHeader("Test Step 37");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 1MMI_N_TRACKCONDITIONS = 1MMI_NID_TRACKCOND = 25MMI_Q_TRACKCOND_STEP = 4");
            TraceReport("Expected Result");
            TraceInfo(
                "The symbol T31 is removed from sub-area B3.Verify the following information,(1)   The 3 symbols are correctly  displayed in the specified location below,The symbol TC31 is display in sub-area B3.The symbol TC33 is display in sub-area B4.The symbol TC33 is display in sub-area B5");
            /*
            Test Step 37
            Action: Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 1MMI_N_TRACKCONDITIONS = 1MMI_NID_TRACKCOND = 25MMI_Q_TRACKCOND_STEP = 4
            Expected Result: The symbol T31 is removed from sub-area B3.Verify the following information,(1)   The 3 symbols are correctly  displayed in the specified location below,The symbol TC31 is display in sub-area B3.The symbol TC33 is display in sub-area B4.The symbol TC33 is display in sub-area B5
            Test Step Comment: (1) MMI_gen 10470; MMI_gen 662 (partly: TC33); MMI_gen 667; MMI_gen 10465 (partly: TC33); MMI_gen 10467;
            */

            TraceHeader("Test Step 38");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 1MMI_N_TRACKCONDITIONS = 1MMI_NID_TRACKCOND = 26MMI_Q_TRACKCOND_STEP = 4");
            TraceReport("Expected Result");
            TraceInfo(
                "The symbol T31 is removed from sub-area B3.Verify the following information,(1)   The 3 symbols are correctly  displayed in the specified location below,The symbol TC33 is display in sub-area B3.The symbol TC33 is display in sub-area B4.The symbol TC03 is display in sub-area B5 with yellow flashing frame");
            /*
            Test Step 38
            Action: Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 1MMI_N_TRACKCONDITIONS = 1MMI_NID_TRACKCOND = 26MMI_Q_TRACKCOND_STEP = 4
            Expected Result: The symbol T31 is removed from sub-area B3.Verify the following information,(1)   The 3 symbols are correctly  displayed in the specified location below,The symbol TC33 is display in sub-area B3.The symbol TC33 is display in sub-area B4.The symbol TC03 is display in sub-area B5 with yellow flashing frame
            Test Step Comment: (1) MMI_gen 10470; MMI_gen 662 (partly: TC03); MMI_gen 667; MMI_gen 10465 (partly: TC03); MMI_gen 10467;
            */

            TraceHeader("Test Step 39");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 1MMI_N_TRACKCONDITIONS = 1MMI_NID_TRACKCOND = 27MMI_Q_TRACKCOND_STEP = 4");
            TraceReport("Expected Result");
            TraceInfo(
                "The symbol T33 is removed from sub-area B3.Verify the following information,(1)   The 2 symbols are correctly  displayed in the specified location below,The symbol TC33 is display in sub-area B3.The symbol TC03 is display in sub-area B4 with yellow flashing frame.There is no symbol display in sub-area B5");
            /*
            Test Step 39
            Action: Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 1MMI_N_TRACKCONDITIONS = 1MMI_NID_TRACKCOND = 27MMI_Q_TRACKCOND_STEP = 4
            Expected Result: The symbol T33 is removed from sub-area B3.Verify the following information,(1)   The 2 symbols are correctly  displayed in the specified location below,The symbol TC33 is display in sub-area B3.The symbol TC03 is display in sub-area B4 with yellow flashing frame.There is no symbol display in sub-area B5
            Test Step Comment: (1) MMI_gen 10467;
            */

            TraceHeader("Test Step 40");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 1MMI_N_TRACKCONDITIONS = 1MMI_NID_TRACKCOND = 28MMI_Q_TRACKCOND_STEP = 4");
            TraceReport("Expected Result");
            TraceInfo(
                "The symbol T33 is removed from sub-area B3.Verify the following information,(1)   The symbol is correctly displayed in the specified location below,The symbol TC03 is display in sub-area B3 with yellow flashing frame.There is no symbol display in sub-area B4.There is no symbol display in sub-area B5");
            /*
            Test Step 40
            Action: Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 1MMI_N_TRACKCONDITIONS = 1MMI_NID_TRACKCOND = 28MMI_Q_TRACKCOND_STEP = 4
            Expected Result: The symbol T33 is removed from sub-area B3.Verify the following information,(1)   The symbol is correctly displayed in the specified location below,The symbol TC03 is display in sub-area B3 with yellow flashing frame.There is no symbol display in sub-area B4.There is no symbol display in sub-area B5
            Test Step Comment: (1) MMI_gen 10467;
            */

            TraceHeader("Test Step 41");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 1MMI_N_TRACKCONDITIONS = 1MMI_NID_TRACKCOND = 29MMI_Q_TRACKCOND_STEP = 4");
            TraceReport("Expected Result");
            TraceInfo("The symbol TC36 in sub-area C2 is removed");
            /*
            Test Step 41
            Action: Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 1MMI_N_TRACKCONDITIONS = 1MMI_NID_TRACKCOND = 29MMI_Q_TRACKCOND_STEP = 4
            Expected Result: The symbol TC36 in sub-area C2 is removed
            */

            TraceHeader("Test Step 42");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Driver simulates the communication loss between ETCS Onboard and DMI");
            TraceReport("Expected Result");
            TraceInfo("Verify the following information,(1)  The symbol TC03 is removed from DMI");
            /*
            Test Step 42
            Action: Driver simulates the communication loss between ETCS Onboard and DMI
            Expected Result: Verify the following information,(1)  The symbol TC03 is removed from DMI
            Test Step Comment: MMI_gen 668 (partly: MMI_gen 244);
            */
            DmiActions.Simulate_communication_loss_EVC_DMI(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI removes symbol TC03 from sub-area C1");

            TraceHeader("Test Step 43");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Re-establish the communication between ETCS Onboard and DMI");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following information,(1)  The symbol TC03 is re-appear if ETCS Onboard is re-transmit EVC-32 to DMI");
            /*
            Test Step 43
            Action: Re-establish the communication between ETCS Onboard and DMI
            Expected Result: Verify the following information,(1)  The symbol TC03 is re-appear if ETCS Onboard is re-transmit EVC-32 to DMI
            Test Step Comment: Note under MMI_gen 668;
            */
            DmiActions.Re_establish_communication_EVC_DMI(this);

            EVC32_MMITrackConditions.MMI_Q_TRACKCOND_UPDATE = 0;
            EVC32_MMITrackConditions.TrackConditions = new List<TrackCondition>
            {
                new TrackCondition
                {
                    MMI_O_TRACKCOND_ANNOUNCE = 0,
                    MMI_O_TRACKCOND_START = 0,
                    MMI_O_TRACKCOND_END = 0,
                    MMI_NID_TRACKCOND = 0,
                    MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Pantograph,
                    MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.AnnounceArea,
                    MMI_Q_TRACKCOND_ACTION_START = Variables.MMI_Q_TRACKCOND_ACTION.WithoutDriverAction,
                    MMI_Q_TRACKCOND_ACTION_END = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction
                }
            };
            EVC32_MMITrackConditions.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI re-displays symbol TC03.");

            TraceHeader("Test Step 44");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Deactivate cabin.Then, simulate loss-communication between ETCS onboard and DMI");
            TraceReport("Expected Result");
            TraceInfo("Verify the following information,(1)  All symbols in sub-area B3-B5 are removed");
            /*
            Test Step 44
            Action: Deactivate cabin.Then, simulate loss-communication between ETCS onboard and DMI
            Expected Result: Verify the following information,(1)  All symbols in sub-area B3-B5 are removed
            Test Step Comment: MMI_gen 668 (partly: MMI_gen 240);
            */
            DmiActions.Deactivate_Cabin(this);
            DmiActions.Simulate_communication_loss_EVC_DMI(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays no symbols in sub-areas B3-B5.");

            TraceHeader("Test Step 45");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Activate cabin.Then, re-establish communication between ETCS onboard and DMI");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following information,(1)  The symbol TC03 is re-appear if ETCS Onboard is re-transmit EVC-32 to DMI");
            /*
            Test Step 45
            Action: Activate cabin.Then, re-establish communication between ETCS onboard and DMI
            Expected Result: Verify the following information,(1)  The symbol TC03 is re-appear if ETCS Onboard is re-transmit EVC-32 to DMI
            Test Step Comment: Note under MMI_gen 668;
            */
            DmiActions.Activate_Cabin_1(this);
            DmiActions.Re_establish_communication_EVC_DMI(this);
            DmiActions.Set_Driver_ID(this, "1234");

            EVC32_MMITrackConditions.MMI_Q_TRACKCOND_UPDATE = 0;
            EVC32_MMITrackConditions.TrackConditions = new List<TrackCondition>
            {
                new TrackCondition
                {
                    MMI_O_TRACKCOND_ANNOUNCE = 0,
                    MMI_O_TRACKCOND_START = 0,
                    MMI_O_TRACKCOND_END = 0,
                    MMI_NID_TRACKCOND = 0,
                    MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Pantograph,
                    MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.AnnounceArea,
                    MMI_Q_TRACKCOND_ACTION_START = Variables.MMI_Q_TRACKCOND_ACTION.WithoutDriverAction,
                    MMI_Q_TRACKCOND_ACTION_END = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction
                }
            };
            EVC32_MMITrackConditions.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI re-displays symbol TC03.");

            // Steps 46 to end are in XML_18_6_2_c.cs
            TraceHeader("Test Step 46");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Use a second test script file 18_6_2_c.xml to Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 0MMI_N_TRACKCONDITIONS = 1MMI_NID_TRACKCOND = 0MMI_Q_TRACKCOND_STEP = 3MMI_M_TRACKCOND_TYPE = 3MMI_Q_TRACKCOND_ACTION_START = 0MMI_Q_TRACKCOND_ACTION_END = 0");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following information,(1)   The symbol TC05 is display in sub-area B3 with yellow flashing frame");
            /*
            Test Step 46
            Action: Use a second test script file 18_6_2_c.xml to Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 0MMI_N_TRACKCONDITIONS = 1MMI_NID_TRACKCOND = 0MMI_Q_TRACKCOND_STEP = 3MMI_M_TRACKCOND_TYPE = 3MMI_Q_TRACKCOND_ACTION_START = 0MMI_Q_TRACKCOND_ACTION_END = 0
            Expected Result: Verify the following information,(1)   The symbol TC05 is display in sub-area B3 with yellow flashing frame
            Test Step Comment: (1) MMI_gen 10465 (partly: TC05); MMI_gen 662 (partly: TC05);
            */
            XML_18_6_2(msgType.typec);

            TraceHeader("Test Step 47");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 0MMI_N_TRACKCONDITIONS = 1MMI_NID_TRACKCOND = 0MMI_Q_TRACKCOND_STEP = 1MMI_M_TRACKCOND_TYPE = 9MMI_Q_TRACKCOND_ACTION_START = 0MMI_Q_TRACKCOND_ACTION_END = 0");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following information,(1)   The symbol TC07 is display in sub-area B3 with yellow flashing frame");
            /*
            Test Step 47
            Action: Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 0MMI_N_TRACKCONDITIONS = 1MMI_NID_TRACKCOND = 0MMI_Q_TRACKCOND_STEP = 1MMI_M_TRACKCOND_TYPE = 9MMI_Q_TRACKCOND_ACTION_START = 0MMI_Q_TRACKCOND_ACTION_END = 0
            Expected Result: Verify the following information,(1)   The symbol TC07 is display in sub-area B3 with yellow flashing frame
            Test Step Comment: (1) MMI_gen 10465 (partly: TC07); MMI_gen 662 (partly: TC07);
            */

            TraceHeader("Test Step 48");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 0MMI_N_TRACKCONDITIONS = 1MMI_NID_TRACKCOND = 0MMI_Q_TRACKCOND_STEP = 3MMI_M_TRACKCOND_TYPE = 9MMI_Q_TRACKCOND_ACTION_START = 0MMI_Q_TRACKCOND_ACTION_END = 0");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following information,(1)   The symbol TC09 is display in sub-area B3 with yellow flashing frame");
            /*
            Test Step 48
            Action: Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 0MMI_N_TRACKCONDITIONS = 1MMI_NID_TRACKCOND = 0MMI_Q_TRACKCOND_STEP = 3MMI_M_TRACKCOND_TYPE = 9MMI_Q_TRACKCOND_ACTION_START = 0MMI_Q_TRACKCOND_ACTION_END = 0
            Expected Result: Verify the following information,(1)   The symbol TC09 is display in sub-area B3 with yellow flashing frame
            Test Step Comment: (1) MMI_gen 10465 (partly: TC09); MMI_gen 662 (partly: TC09);
            */

            TraceHeader("Test Step 49");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 0MMI_N_TRACKCONDITIONS = 1MMI_NID_TRACKCOND = 0MMI_Q_TRACKCOND_STEP = 1MMI_M_TRACKCOND_TYPE = 0MMI_Q_TRACKCOND_ACTION_START = 0MMI_Q_TRACKCOND_ACTION_END = 0");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following information,(1)   The symbol TC11 is display in sub-area B3 with yellow flashing frame");
            /*
            Test Step 49
            Action: Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 0MMI_N_TRACKCONDITIONS = 1MMI_NID_TRACKCOND = 0MMI_Q_TRACKCOND_STEP = 1MMI_M_TRACKCOND_TYPE = 0MMI_Q_TRACKCOND_ACTION_START = 0MMI_Q_TRACKCOND_ACTION_END = 0
            Expected Result: Verify the following information,(1)   The symbol TC11 is display in sub-area B3 with yellow flashing frame
            Test Step Comment: (1) MMI_gen 10465 (partly: TC11); MMI_gen 662 (partly: TC11);
            */

            TraceHeader("Test Step 50");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 0MMI_N_TRACKCONDITIONS = 1MMI_NID_TRACKCOND = 0MMI_Q_TRACKCOND_STEP = 1MMI_M_TRACKCOND_TYPE = 6MMI_Q_TRACKCOND_ACTION_START = 0MMI_Q_TRACKCOND_ACTION_END = 0");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following information,(1)   The symbol TC14 is display in sub-area B3 with yellow flashing frame");
            /*
            Test Step 50
            Action: Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 0MMI_N_TRACKCONDITIONS = 1MMI_NID_TRACKCOND = 0MMI_Q_TRACKCOND_STEP = 1MMI_M_TRACKCOND_TYPE = 6MMI_Q_TRACKCOND_ACTION_START = 0MMI_Q_TRACKCOND_ACTION_END = 0
            Expected Result: Verify the following information,(1)   The symbol TC14 is display in sub-area B3 with yellow flashing frame
            Test Step Comment: (1) MMI_gen 10465 (partly: TC14); MMI_gen 662 (partly: TC14);
            */

            TraceHeader("Test Step 51");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 0MMI_N_TRACKCONDITIONS = 1MMI_NID_TRACKCOND = 0MMI_Q_TRACKCOND_STEP = 1MMI_M_TRACKCOND_TYPE = 7MMI_Q_TRACKCOND_ACTION_START = 0MMI_Q_TRACKCOND_ACTION_END = 0");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following information,(1)   The symbol TC16 is display in sub-area B3 with yellow flashing frame");
            /*
            Test Step 51
            Action: Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 0MMI_N_TRACKCONDITIONS = 1MMI_NID_TRACKCOND = 0MMI_Q_TRACKCOND_STEP = 1MMI_M_TRACKCOND_TYPE = 7MMI_Q_TRACKCOND_ACTION_START = 0MMI_Q_TRACKCOND_ACTION_END = 0
            Expected Result: Verify the following information,(1)   The symbol TC16 is display in sub-area B3 with yellow flashing frame
            Test Step Comment: (1) MMI_gen 10465 (partly: TC16); MMI_gen 662 (partly: TC16);
            */

            TraceHeader("Test Step 52");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 0MMI_N_TRACKCONDITIONS = 1MMI_NID_TRACKCOND = 0MMI_Q_TRACKCOND_STEP = 1MMI_M_TRACKCOND_TYPE = 8MMI_Q_TRACKCOND_ACTION_START = 0MMI_Q_TRACKCOND_ACTION_END = 0");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following information,(1)   The symbol TC18 is display in sub-area B3 with yellow flashing frame");
            /*
            Test Step 52
            Action: Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 0MMI_N_TRACKCONDITIONS = 1MMI_NID_TRACKCOND = 0MMI_Q_TRACKCOND_STEP = 1MMI_M_TRACKCOND_TYPE = 8MMI_Q_TRACKCOND_ACTION_START = 0MMI_Q_TRACKCOND_ACTION_END = 0
            Expected Result: Verify the following information,(1)   The symbol TC18 is display in sub-area B3 with yellow flashing frame
            Test Step Comment: (1) MMI_gen 10465 (partly: TC18); MMI_gen 662 (partly: TC18); 
            */

            TraceHeader("Test Step 53");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 0MMI_N_TRACKCONDITIONS = 1MMI_NID_TRACKCOND = 0MMI_Q_TRACKCOND_STEP = 1MMI_M_TRACKCOND_TYPE = 5MMI_Q_TRACKCOND_ACTION_START = 0MMI_Q_TRACKCOND_ACTION_END = 0");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following information,(1)   The symbol TC21 is display in sub-area B3 with yellow flashing frame");
            /*
            Test Step 53
            Action: Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 0MMI_N_TRACKCONDITIONS = 1MMI_NID_TRACKCOND = 0MMI_Q_TRACKCOND_STEP = 1MMI_M_TRACKCOND_TYPE = 5MMI_Q_TRACKCOND_ACTION_START = 0MMI_Q_TRACKCOND_ACTION_END = 0
            Expected Result: Verify the following information,(1)   The symbol TC21 is display in sub-area B3 with yellow flashing frame
            Test Step Comment: (1) MMI_gen 10465 (partly: TC21); MMI_gen 662 (partly: TC21); 
            */

            TraceHeader("Test Step 54");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 0MMI_N_TRACKCONDITIONS = 1MMI_NID_TRACKCOND = 0MMI_Q_TRACKCOND_STEP = 3MMI_M_TRACKCOND_TYPE = 5MMI_Q_TRACKCOND_ACTION_START = 0MMI_Q_TRACKCOND_ACTION_END = 0");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following information,(1)   The symbol TC22 is display in sub-area B3 with yellow flashing frame");
            /*
            Test Step 54
            Action: Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 0MMI_N_TRACKCONDITIONS = 1MMI_NID_TRACKCOND = 0MMI_Q_TRACKCOND_STEP = 3MMI_M_TRACKCOND_TYPE = 5MMI_Q_TRACKCOND_ACTION_START = 0MMI_Q_TRACKCOND_ACTION_END = 0
            Expected Result: Verify the following information,(1)   The symbol TC22 is display in sub-area B3 with yellow flashing frame
            Test Step Comment: (1) MMI_gen 10465 (partly: TC22); MMI_gen 662 (partly: TC22);
            */

            TraceHeader("Test Step 55");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 0MMI_N_TRACKCONDITIONS = 1MMI_NID_TRACKCOND = 0MMI_Q_TRACKCOND_STEP = 1MMI_M_TRACKCOND_TYPE = 10MMI_Q_TRACKCOND_ACTION_START = 0MMI_Q_TRACKCOND_ACTION_END = 0");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following information,(1)   The symbol TC24 is display in sub-area B3 with yellow flashing frame");
            /*
            Test Step 55
            Action: Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 0MMI_N_TRACKCONDITIONS = 1MMI_NID_TRACKCOND = 0MMI_Q_TRACKCOND_STEP = 1MMI_M_TRACKCOND_TYPE = 10MMI_Q_TRACKCOND_ACTION_START = 0MMI_Q_TRACKCOND_ACTION_END = 0
            Expected Result: Verify the following information,(1)   The symbol TC24 is display in sub-area B3 with yellow flashing frame
            Test Step Comment: (1) MMI_gen 10465 (partly: TC24); MMI_gen 662 (partly: TC24);
            */

            TraceHeader("Test Step 56");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 0MMI_N_TRACKCONDITIONS = 1MMI_NID_TRACKCOND = 0MMI_Q_TRACKCOND_STEP = 1MMI_M_TRACKCOND_TYPE = 11MMI_Q_TRACKCOND_ACTION_START = 0MMI_Q_TRACKCOND_ACTION_END = 0");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following information,(1)   The symbol TC26 is display in sub-area B3 with yellow flashing frame");
            /*
            Test Step 56
            Action: Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 0MMI_N_TRACKCONDITIONS = 1MMI_NID_TRACKCOND = 0MMI_Q_TRACKCOND_STEP = 1MMI_M_TRACKCOND_TYPE = 11MMI_Q_TRACKCOND_ACTION_START = 0MMI_Q_TRACKCOND_ACTION_END = 0
            Expected Result: Verify the following information,(1)   The symbol TC26 is display in sub-area B3 with yellow flashing frame
            Test Step Comment: (1) MMI_gen 10465 (partly: TC26); MMI_gen 662 (partly: TC26);
            */

            TraceHeader("Test Step 57");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 0MMI_N_TRACKCONDITIONS = 1MMI_NID_TRACKCOND = 0MMI_Q_TRACKCOND_STEP = 1MMI_M_TRACKCOND_TYPE = 12MMI_Q_TRACKCOND_ACTION_START = 0MMI_Q_TRACKCOND_ACTION_END = 0");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following information,(1)   The symbol TC28 is display in sub-area B3 with yellow flashing frame");
            /*
            Test Step 57
            Action: Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 0MMI_N_TRACKCONDITIONS = 1MMI_NID_TRACKCOND = 0MMI_Q_TRACKCOND_STEP = 1MMI_M_TRACKCOND_TYPE = 12MMI_Q_TRACKCOND_ACTION_START = 0MMI_Q_TRACKCOND_ACTION_END = 0
            Expected Result: Verify the following information,(1)   The symbol TC28 is display in sub-area B3 with yellow flashing frame
            Test Step Comment: (1) MMI_gen 10465 (partly: TC28); MMI_gen 662 (partly: TC28);
            */

            TraceHeader("Test Step 58");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 0MMI_N_TRACKCONDITIONS = 1MMI_NID_TRACKCOND = 0MMI_Q_TRACKCOND_STEP = 1MMI_M_TRACKCOND_TYPE = 13MMI_Q_TRACKCOND_ACTION_START = 0MMI_Q_TRACKCOND_ACTION_END = 0");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following information,(1)   The symbol TC30 is display in sub-area B3 with yellow flashing frame");
            /*
            Test Step 58
            Action: Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 0MMI_N_TRACKCONDITIONS = 1MMI_NID_TRACKCOND = 0MMI_Q_TRACKCOND_STEP = 1MMI_M_TRACKCOND_TYPE = 13MMI_Q_TRACKCOND_ACTION_START = 0MMI_Q_TRACKCOND_ACTION_END = 0
            Expected Result: Verify the following information,(1)   The symbol TC30 is display in sub-area B3 with yellow flashing frame
            Test Step Comment: (1) MMI_gen 10465 (partly: TC30); MMI_gen 662 (partly: TC30);
            */

            TraceHeader("Test Step 59");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 0MMI_N_TRACKCONDITIONS = 1MMI_NID_TRACKCOND = 0MMI_Q_TRACKCOND_STEP = 1MMI_M_TRACKCOND_TYPE = 14MMI_Q_TRACKCOND_ACTION_START = 0MMI_Q_TRACKCOND_ACTION_END = 0");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following information,(1)   The symbol TC32 is display in sub-area B3 with yellow flashing frame");
            /*
            Test Step 59
            Action: Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 0MMI_N_TRACKCONDITIONS = 1MMI_NID_TRACKCOND = 0MMI_Q_TRACKCOND_STEP = 1MMI_M_TRACKCOND_TYPE = 14MMI_Q_TRACKCOND_ACTION_START = 0MMI_Q_TRACKCOND_ACTION_END = 0
            Expected Result: Verify the following information,(1)   The symbol TC32 is display in sub-area B3 with yellow flashing frame
            Test Step Comment: (1) MMI_gen 10465 (partly: TC32); MMI_gen 662 (partly: TC32);
            */

            TraceHeader("Test Step 60");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 0MMI_N_TRACKCONDITIONS = 1MMI_NID_TRACKCOND = 0MMI_Q_TRACKCOND_STEP = 1MMI_M_TRACKCOND_TYPE = 15MMI_Q_TRACKCOND_ACTION_START = 0MMI_Q_TRACKCOND_ACTION_END = 0");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following information,(1)   The symbol TC34 is display in sub-area B3 with yellow flashing frame");
            /*
            Test Step 60
            Action: Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 0MMI_N_TRACKCONDITIONS = 1MMI_NID_TRACKCOND = 0MMI_Q_TRACKCOND_STEP = 1MMI_M_TRACKCOND_TYPE = 15MMI_Q_TRACKCOND_ACTION_START = 0MMI_Q_TRACKCOND_ACTION_END = 0
            Expected Result: Verify the following information,(1)   The symbol TC34 is display in sub-area B3 with yellow flashing frame
            Test Step Comment: (1) MMI_gen 10465 (partly: TC34); MMI_gen 662 (partly: TC34);
            */

            TraceHeader("Test Step 61");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 0MMI_N_TRACKCONDITIONS = 1MMI_NID_TRACKCOND = 0MMI_Q_TRACKCOND_STEP = 1MMI_M_TRACKCOND_TYPE = 2MMI_Q_TRACKCOND_ACTION_START = 0MMI_Q_TRACKCOND_ACTION_END = 0");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following information,(1)   The symbol TC35 is display in sub-area B3 with yellow flashing frame");
            /*
Test Step 61
Action: Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 0MMI_N_TRACKCONDITIONS = 1MMI_NID_TRACKCOND = 0MMI_Q_TRACKCOND_STEP = 1MMI_M_TRACKCOND_TYPE = 2MMI_Q_TRACKCOND_ACTION_START = 0MMI_Q_TRACKCOND_ACTION_END = 0
Expected Result: Verify the following information,(1)   The symbol TC35 is display in sub-area B3 with yellow flashing frame
Test Step Comment: (1) MMI_gen 10465 (partly: TC35); MMI_gen 662 (partly: TC35);
*/

            TraceHeader("Test Step 62");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 0MMI_N_TRACKCONDITIONS = 1MMI_NID_TRACKCOND = 0MMI_Q_TRACKCOND_STEP = 1MMI_M_TRACKCOND_TYPE = 1MMI_Q_TRACKCOND_ACTION_START = 0MMI_Q_TRACKCOND_ACTION_END = 0");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following information,(1)   The symbol TC37 is display in sub-area C2 with yellow flashing frame");
            /*
            Test Step 62
            Action: Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 0MMI_N_TRACKCONDITIONS = 1MMI_NID_TRACKCOND = 0MMI_Q_TRACKCOND_STEP = 1MMI_M_TRACKCOND_TYPE = 1MMI_Q_TRACKCOND_ACTION_START = 0MMI_Q_TRACKCOND_ACTION_END = 0
            Expected Result: Verify the following information,(1)   The symbol TC37 is display in sub-area C2 with yellow flashing frame
            Test Step Comment: (1) MMI_gen 10465 (partly: TC37); MMI_gen 662 (partly: TC37);
            */

            TraceHeader("Test Step 63");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("End of test");
            
            /*
            Test Step 63
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }

        #region Send_XML_18_6_2_DMI_Test_Specification

        enum msgType
        {
            typea,
            typeb,
            typec
        }

        private void XML_18_6_2(msgType type)
        {
            switch (type)
            {
                case msgType.typea:
                    // Step 1
                    EVC32_MMITrackConditions.MMI_Q_TRACKCOND_UPDATE = 0;
                    EVC32_MMITrackConditions.Send();

                    TrackCondition trackCondition0 = new TrackCondition
                    {
                        MMI_O_TRACKCOND_ANNOUNCE = 0,
                        MMI_O_TRACKCOND_START = 0,
                        MMI_O_TRACKCOND_END = 0,
                        MMI_NID_TRACKCOND = 0,
                        MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Pantograph,
                        MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.AnnounceArea,
                        MMI_Q_TRACKCOND_ACTION_START = Variables.MMI_Q_TRACKCOND_ACTION.WithoutDriverAction
                    };
                    TrackCondition trackCondition1 = new TrackCondition
                    {
                        MMI_O_TRACKCOND_ANNOUNCE = 0,
                        MMI_O_TRACKCOND_START = 0,
                        MMI_O_TRACKCOND_END = 0,
                        MMI_NID_TRACKCOND = 1,
                        MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Pantograph,
                        MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.InsideArea_Active,
                        MMI_Q_TRACKCOND_ACTION_START = Variables.MMI_Q_TRACKCOND_ACTION.WithoutDriverAction
                    };
                    TrackCondition trackCondition2 = new TrackCondition
                    {
                        MMI_O_TRACKCOND_ANNOUNCE = 0,
                        MMI_O_TRACKCOND_START = 0,
                        MMI_O_TRACKCOND_END = 0,
                        MMI_NID_TRACKCOND = 2,
                        MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Pantograph,
                        MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.LeaveArea,
                        MMI_Q_TRACKCOND_ACTION_END = Variables.MMI_Q_TRACKCOND_ACTION.WithoutDriverAction
                    };

                    List<TrackCondition> trackConditionList = new List<TrackCondition> {trackCondition1};
                    //List<TrackCondition> trackConditionList = new List<TrackCondition> { trackCondition0, trackCondition1, trackCondition2 };
                    EVC32_MMITrackConditions.TrackConditions = trackConditionList;
                    EVC32_MMITrackConditions.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        "1. DMI displays symbol TC02 in sub-area B3." + Environment.NewLine +
                                        "2. DMI displays symbol TC01 in sub-area B4." + Environment.NewLine +
                                        "3. DMI displays symbol TC04 in sub-area B5.");

                    // Step 2
                    EVC32_MMITrackConditions.MMI_Q_TRACKCOND_UPDATE = 1;

                    trackCondition0.MMI_NID_TRACKCOND = 3;
                    trackCondition0.MMI_M_TRACKCOND_TYPE =
                        Variables.MMI_M_TRACKCOND_TYPE.Main_power_switch_Neutral_Section;
                    trackCondition0.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.AnnounceArea;
                    trackCondition0.MMI_Q_TRACKCOND_ACTION_START = Variables.MMI_Q_TRACKCOND_ACTION.WithoutDriverAction;
                    trackCondition0.MMI_Q_TRACKCOND_ACTION_END = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction;

                    trackCondition1.MMI_NID_TRACKCOND = 4;
                    trackCondition1.MMI_M_TRACKCOND_TYPE =
                        Variables.MMI_M_TRACKCOND_TYPE.Main_power_switch_Neutral_Section;
                    trackCondition0.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.InsideArea_Active;
                    trackCondition1.MMI_Q_TRACKCOND_ACTION_START = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction;
                    trackCondition1.MMI_Q_TRACKCOND_ACTION_END = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction;

                    trackCondition1.MMI_NID_TRACKCOND = 5;
                    trackCondition1.MMI_M_TRACKCOND_TYPE =
                        Variables.MMI_M_TRACKCOND_TYPE.Main_power_switch_Neutral_Section;
                    trackCondition0.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.LeaveArea;
                    trackCondition1.MMI_Q_TRACKCOND_ACTION_START = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction;
                    trackCondition1.MMI_Q_TRACKCOND_ACTION_END = Variables.MMI_Q_TRACKCOND_ACTION.WithoutDriverAction;

                    EVC32_MMITrackConditions.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        "1. DMI still displays symbol TC02 in sub-area B3." + Environment.NewLine +
                                        "2. DMI still displays symbol TC01 in sub-area B4." + Environment.NewLine +
                                        "3. DMI still displays symbol TC04 in sub-area B5.");

                    // Step 3
                    trackCondition0.MMI_NID_TRACKCOND = 6;

                    trackCondition0.MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Non_Stopping_Area;
                    trackCondition0.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.AnnounceArea;
                    trackCondition0.MMI_Q_TRACKCOND_ACTION_START = Variables.MMI_Q_TRACKCOND_ACTION.WithoutDriverAction;
                    trackCondition0.MMI_Q_TRACKCOND_ACTION_END = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction;

                    trackCondition1.MMI_NID_TRACKCOND = 7;
                    trackCondition1.MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Radio_hole;
                    trackCondition0.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.InsideArea_Active;
                    trackCondition1.MMI_Q_TRACKCOND_ACTION_START = Variables.MMI_Q_TRACKCOND_ACTION.WithoutDriverAction;
                    trackCondition1.MMI_Q_TRACKCOND_ACTION_END = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction;

                    trackCondition1.MMI_NID_TRACKCOND = 8;
                    trackCondition1.MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Magnetic_Shoe_Brakes;
                    trackCondition0.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.AnnounceArea;
                    trackCondition1.MMI_Q_TRACKCOND_ACTION_START = Variables.MMI_Q_TRACKCOND_ACTION.WithoutDriverAction;
                    trackCondition1.MMI_Q_TRACKCOND_ACTION_END = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction;

                    EVC32_MMITrackConditions.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        "1. DMI still displays symbol TC02 in sub-area B3." + Environment.NewLine +
                                        "2. DMI still displays symbol TC01 in sub-area B4." + Environment.NewLine +
                                        "3. DMI still displays symbol TC04 in sub-area B5.");

                    // Step 4            
                    trackCondition0.MMI_NID_TRACKCOND = 9;

                    trackCondition0.MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Magnetic_Shoe_Brakes;
                    trackCondition0.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.InsideArea_Active;
                    trackCondition0.MMI_Q_TRACKCOND_ACTION_START = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction;
                    trackCondition0.MMI_Q_TRACKCOND_ACTION_END = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction;

                    trackCondition1.MMI_NID_TRACKCOND = 10;
                    trackCondition1.MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Eddy_Current_Brakes;
                    trackCondition0.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.AnnounceArea;
                    trackCondition1.MMI_Q_TRACKCOND_ACTION_START = Variables.MMI_Q_TRACKCOND_ACTION.WithoutDriverAction;
                    trackCondition1.MMI_Q_TRACKCOND_ACTION_END = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction;

                    trackCondition1.MMI_NID_TRACKCOND = 11;
                    trackCondition1.MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Eddy_Current_Brakes;
                    trackCondition0.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.InsideArea_Active;
                    trackCondition1.MMI_Q_TRACKCOND_ACTION_START = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction;
                    trackCondition1.MMI_Q_TRACKCOND_ACTION_END = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction;

                    EVC32_MMITrackConditions.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        "1. DMI still displays symbol TC02 in sub-area B3." + Environment.NewLine +
                                        "2. DMI still displays symbol TC01 in sub-area B4." + Environment.NewLine +
                                        "3. DMI still displays symbol TC04 in sub-area B5.");

                    // Step 5           
                    trackCondition0.MMI_NID_TRACKCOND = 12;

                    trackCondition0.MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Regenerative_Brakes;
                    trackCondition0.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.AnnounceArea;
                    trackCondition0.MMI_Q_TRACKCOND_ACTION_START = Variables.MMI_Q_TRACKCOND_ACTION.WithoutDriverAction;
                    trackCondition0.MMI_Q_TRACKCOND_ACTION_END = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction;

                    trackCondition1.MMI_NID_TRACKCOND = 13;
                    trackCondition1.MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Regenerative_Brakes;
                    trackCondition0.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.InsideArea_Active;
                    trackCondition1.MMI_Q_TRACKCOND_ACTION_START = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction;
                    trackCondition1.MMI_Q_TRACKCOND_ACTION_END = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction;

                    trackCondition1.MMI_NID_TRACKCOND = 14;
                    trackCondition1.MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Air_tightness;
                    trackCondition0.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.AnnounceArea;
                    trackCondition1.MMI_Q_TRACKCOND_ACTION_START = Variables.MMI_Q_TRACKCOND_ACTION.WithoutDriverAction;
                    trackCondition1.MMI_Q_TRACKCOND_ACTION_END = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction;

                    EVC32_MMITrackConditions.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        "1. DMI still displays symbol TC02 in sub-area B3." + Environment.NewLine +
                                        "2. DMI still displays symbol TC01 in sub-area B4." + Environment.NewLine +
                                        "3. DMI still displays symbol TC04 in sub-area B5.");

                    // Step 6
                    trackCondition0.MMI_NID_TRACKCOND = 15;

                    trackCondition0.MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Air_tightness;
                    trackCondition0.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.InsideArea_Active;
                    trackCondition0.MMI_Q_TRACKCOND_ACTION_START = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction;
                    trackCondition0.MMI_Q_TRACKCOND_ACTION_END = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction;

                    trackCondition1.MMI_NID_TRACKCOND = 16;
                    trackCondition1.MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Air_tightness;
                    trackCondition0.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.LeaveArea;
                    trackCondition1.MMI_Q_TRACKCOND_ACTION_START = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction;
                    trackCondition1.MMI_Q_TRACKCOND_ACTION_END = Variables.MMI_Q_TRACKCOND_ACTION.WithoutDriverAction;

                    trackCondition1.MMI_NID_TRACKCOND = 17;
                    trackCondition1.MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Change_traction_not_fitted;
                    trackCondition0.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.AnnounceArea;
                    trackCondition1.MMI_Q_TRACKCOND_ACTION_START = Variables.MMI_Q_TRACKCOND_ACTION.WithoutDriverAction;
                    trackCondition1.MMI_Q_TRACKCOND_ACTION_END = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction;

                    EVC32_MMITrackConditions.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        "1. DMI still displays symbol TC02 in sub-area B3." + Environment.NewLine +
                                        "2. DMI still displays symbol TC01 in sub-area B4." + Environment.NewLine +
                                        "3. DMI still displays symbol TC04 in sub-area B5.");

                    // Step 7
                    trackCondition0.MMI_NID_TRACKCOND = 18;

                    trackCondition0.MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Change_traction_not_fitted;
                    trackCondition0.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.InsideArea_Active;
                    trackCondition0.MMI_Q_TRACKCOND_ACTION_START = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction;
                    trackCondition0.MMI_Q_TRACKCOND_ACTION_END = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction;

                    trackCondition1.MMI_NID_TRACKCOND = 19;
                    trackCondition1.MMI_M_TRACKCOND_TYPE =
                        Variables.MMI_M_TRACKCOND_TYPE.Change_traction_AC_25_kV_50_Hz;
                    trackCondition0.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.AnnounceArea;
                    trackCondition1.MMI_Q_TRACKCOND_ACTION_START = Variables.MMI_Q_TRACKCOND_ACTION.WithoutDriverAction;
                    trackCondition1.MMI_Q_TRACKCOND_ACTION_END = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction;

                    trackCondition1.MMI_NID_TRACKCOND = 20;
                    trackCondition1.MMI_M_TRACKCOND_TYPE =
                        Variables.MMI_M_TRACKCOND_TYPE.Change_traction_AC_25_kV_50_Hz;
                    trackCondition0.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.InsideArea_Active;
                    trackCondition1.MMI_Q_TRACKCOND_ACTION_START = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction;
                    trackCondition1.MMI_Q_TRACKCOND_ACTION_END = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction;

                    EVC32_MMITrackConditions.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        "1. DMI still displays symbol TC02 in sub-area B3." + Environment.NewLine +
                                        "2. DMI still displays symbol TC01 in sub-area B4." + Environment.NewLine +
                                        "3. DMI still displays symbol TC04 in sub-area B5.");

                    // Step 8
                    trackCondition0.MMI_NID_TRACKCOND = 21;

                    trackCondition0.MMI_M_TRACKCOND_TYPE =
                        Variables.MMI_M_TRACKCOND_TYPE.Change_traction_AC_15_kV_16_7_Hz;
                    trackCondition0.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.AnnounceArea;
                    trackCondition0.MMI_Q_TRACKCOND_ACTION_START = Variables.MMI_Q_TRACKCOND_ACTION.WithoutDriverAction;
                    trackCondition0.MMI_Q_TRACKCOND_ACTION_END = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction;

                    trackCondition1.MMI_NID_TRACKCOND = 22;
                    trackCondition1.MMI_M_TRACKCOND_TYPE =
                        Variables.MMI_M_TRACKCOND_TYPE.Change_traction_AC_15_kV_16_7_Hz;
                    trackCondition0.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.InsideArea_Active;
                    trackCondition1.MMI_Q_TRACKCOND_ACTION_START = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction;
                    trackCondition1.MMI_Q_TRACKCOND_ACTION_END = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction;

                    trackCondition1.MMI_NID_TRACKCOND = 23;
                    trackCondition1.MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Change_traction_DC_3_kV;
                    trackCondition0.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.AnnounceArea;
                    trackCondition1.MMI_Q_TRACKCOND_ACTION_START = Variables.MMI_Q_TRACKCOND_ACTION.WithoutDriverAction;
                    trackCondition1.MMI_Q_TRACKCOND_ACTION_END = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction;

                    EVC32_MMITrackConditions.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        "1. DMI still displays symbol TC02 in sub-area B3." + Environment.NewLine +
                                        "2. DMI still displays symbol TC01 in sub-area B4." + Environment.NewLine +
                                        "3. DMI still displays symbol TC04 in sub-area B5.");

                    // Step 9
                    trackCondition0.MMI_NID_TRACKCOND = 24;

                    trackCondition0.MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Change_traction_DC_3_kV;
                    trackCondition0.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.InsideArea_Active;
                    trackCondition0.MMI_Q_TRACKCOND_ACTION_START = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction;
                    trackCondition0.MMI_Q_TRACKCOND_ACTION_END = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction;

                    trackCondition1.MMI_NID_TRACKCOND = 25;
                    trackCondition1.MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Change_traction_DC_1_5_kV;
                    trackCondition0.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.AnnounceArea;
                    trackCondition1.MMI_Q_TRACKCOND_ACTION_START = Variables.MMI_Q_TRACKCOND_ACTION.WithoutDriverAction;
                    trackCondition1.MMI_Q_TRACKCOND_ACTION_END = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction;

                    trackCondition1.MMI_NID_TRACKCOND = 26;
                    trackCondition1.MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Change_traction_DC_1_5_kV;
                    trackCondition0.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.InsideArea_Active;
                    trackCondition1.MMI_Q_TRACKCOND_ACTION_START = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction;
                    trackCondition1.MMI_Q_TRACKCOND_ACTION_END = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction;

                    EVC32_MMITrackConditions.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        "1. DMI still displays symbol TC02 in sub-area B3." + Environment.NewLine +
                                        "2. DMI still displays symbol TC01 in sub-area B4." + Environment.NewLine +
                                        "3. DMI still displays symbol TC04 in sub-area B5.");

                    // Step 10
                    trackCondition0.MMI_NID_TRACKCOND = 27;
                    trackCondition0.MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Change_traction_DC_600_750;
                    trackCondition0.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.AnnounceArea;
                    trackCondition0.MMI_Q_TRACKCOND_ACTION_START = Variables.MMI_Q_TRACKCOND_ACTION.WithoutDriverAction;
                    trackCondition0.MMI_Q_TRACKCOND_ACTION_END = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction;

                    trackCondition1.MMI_NID_TRACKCOND = 28;
                    trackCondition1.MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Change_traction_DC_600_750;
                    trackCondition0.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.InsideArea_Active;
                    trackCondition1.MMI_Q_TRACKCOND_ACTION_START = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction;
                    trackCondition1.MMI_Q_TRACKCOND_ACTION_END = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction;

                    trackCondition1.MMI_NID_TRACKCOND = 29;
                    trackCondition1.MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Tunnel_Stopping_Area;
                    trackCondition0.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.InsideArea_Active;
                    trackCondition1.MMI_Q_TRACKCOND_ACTION_START = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction;
                    trackCondition1.MMI_Q_TRACKCOND_ACTION_END = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction;

                    EVC32_MMITrackConditions.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        "1. DMI still displays symbol TC02 in sub-area B3." + Environment.NewLine +
                                        "2. DMI still displays symbol TC01 in sub-area B4." + Environment.NewLine +
                                        "3. DMI still displays symbol TC04 in sub-area B5." + Environment.NewLine +
                                        "4. DMI displays symbol TC36 in sub-area C2.");

                    // Step 11   
                    trackCondition0.MMI_NID_TRACKCOND = 30;
                    trackCondition0.MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Pantograph;
                    trackCondition0.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.AnnounceArea;
                    trackCondition0.MMI_Q_TRACKCOND_ACTION_START = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction;
                    trackCondition0.MMI_Q_TRACKCOND_ACTION_END = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction;

                    trackConditionList.Remove(trackCondition1);
                    trackConditionList.Remove(trackCondition2);

                    EVC32_MMITrackConditions.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        "1. DMI still displays symbol TC02 in sub-area B3." + Environment.NewLine +
                                        "2. DMI still displays symbol TC01 in sub-area B4." + Environment.NewLine +
                                        "3. DMI still displays symbol TC04 in sub-area B5.");


                    break;
                case msgType.typeb:

                    // Step 12
                    EVC32_MMITrackConditions.MMI_Q_TRACKCOND_UPDATE = 1;

                    TrackCondition trackCondition = new TrackCondition
                    {
                        MMI_O_TRACKCOND_ANNOUNCE = 0,
                        MMI_O_TRACKCOND_START = 0,
                        MMI_O_TRACKCOND_END = 0,
                        MMI_NID_TRACKCOND = 0,
                        MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Non_Stopping_Area,
                        MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.RemoveTC,
                        MMI_Q_TRACKCOND_ACTION_START = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction,
                        MMI_Q_TRACKCOND_ACTION_END = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction
                    };

                    EVC32_MMITrackConditions.TrackConditions = new List<TrackCondition> {trackCondition};

                    EVC32_MMITrackConditions.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        "1. DMI removes symbol TC02 from sub-area B3." + Environment.NewLine +
                                        "2. DMI displays symbol TC01 in sub-area B3." + Environment.NewLine +
                                        "3. DMI displays symbol TC04 in sub-area B4." + Environment.NewLine +
                                        "4. DMI displays symbol TC06 in sub-area B5.");

                    // Step 13
                    trackCondition.MMI_NID_TRACKCOND = 1;
                    trackCondition.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.RemoveTC;
                    EVC32_MMITrackConditions.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        "1. DMI removes symbol TC01 from sub-area B3." + Environment.NewLine +
                                        "2. DMI displays symbol TC04 in sub-area B3." + Environment.NewLine +
                                        "3. DMI displays symbol TC06 in sub-area B4." + Environment.NewLine +
                                        "4. DMI displays symbol TC06 in sub-area B5.");


                    // Step 14
                    trackCondition.MMI_NID_TRACKCOND = 2;
                    trackCondition.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.RemoveTC;
                    EVC32_MMITrackConditions.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        "1. DMI removes symbol TC04 from sub-area B3." + Environment.NewLine +
                                        "2. DMI displays symbol TC06 in sub-area B3." + Environment.NewLine +
                                        "3. DMI displays symbol TC06 in sub-area B4." + Environment.NewLine +
                                        "4. DMI displays symbol TC08 in sub-area B5.");

                    // Step 15
                    trackCondition.MMI_NID_TRACKCOND = 3;
                    trackCondition.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.RemoveTC;
                    EVC32_MMITrackConditions.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        "1. DMI removes symbol TC06 from sub-area B3." + Environment.NewLine +
                                        "2. DMI displays symbol TC06 in sub-area B3." + Environment.NewLine +
                                        "3. DMI displays symbol TC08 in sub-area B4." + Environment.NewLine +
                                        "4. DMI displays symbol TC010 in sub-area B5.");

                    // Step 16
                    trackCondition.MMI_NID_TRACKCOND = 4;
                    trackCondition.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.RemoveTC;
                    EVC32_MMITrackConditions.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        "1. DMI removes symbol TC06 from sub-area B3." + Environment.NewLine +
                                        "2. DMI displays symbol TC08 in sub-area B3." + Environment.NewLine +
                                        "3. DMI displays symbol TC010 in sub-area B4." + Environment.NewLine +
                                        "4. DMI displays symbol TC012 in sub-area B5.");

                    // Step 17
                    trackCondition.MMI_NID_TRACKCOND = 5;
                    trackCondition.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.RemoveTC;
                    EVC32_MMITrackConditions.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        "1. DMI removes symbol TC08 from sub-area B3." + Environment.NewLine +
                                        "2. DMI displays symbol TC10 in sub-area B3." + Environment.NewLine +
                                        "3. DMI displays symbol TC12 in sub-area B4." + Environment.NewLine +
                                        "4. DMI displays symbol TC13 in sub-area B5.");

                    // Step 18
                    trackCondition.MMI_NID_TRACKCOND = 6;
                    trackCondition.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.RemoveTC;
                    EVC32_MMITrackConditions.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        "1. DMI removes symbol TC10 from sub-area B3." + Environment.NewLine +
                                        "2. DMI displays symbol TC12 in sub-area B3." + Environment.NewLine +
                                        "3. DMI displays symbol TC13 in sub-area B4." + Environment.NewLine +
                                        "4. DMI displays symbol TC13 in sub-area B5.");

                    // Step 19
                    trackCondition.MMI_NID_TRACKCOND = 7;
                    trackCondition.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.RemoveTC;
                    EVC32_MMITrackConditions.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        "1. DMI removes symbol TC12 from sub-area B3." + Environment.NewLine +
                                        "2. DMI displays symbol TC13 in sub-area B3." + Environment.NewLine +
                                        "3. DMI displays symbol TC13 in sub-area B4." + Environment.NewLine +
                                        "4. DMI displays symbol TC15 in sub-area B5.");

                    // Step 20
                    trackCondition.MMI_NID_TRACKCOND = 8;
                    trackCondition.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.RemoveTC;
                    EVC32_MMITrackConditions.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        "1. DMI removes symbol TC13 from sub-area B3." + Environment.NewLine +
                                        "2. DMI displays symbol TC13 in sub-area B3." + Environment.NewLine +
                                        "3. DMI displays symbol TC15 in sub-area B4." + Environment.NewLine +
                                        "4. DMI displays symbol TC15 in sub-area B5.");

                    // Step 21
                    trackCondition.MMI_NID_TRACKCOND = 9;
                    trackCondition.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.RemoveTC;
                    EVC32_MMITrackConditions.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        "1. DMI removes symbol TC13 from sub-area B3." + Environment.NewLine +
                                        "2. DMI displays symbol TC15 in sub-area B3." + Environment.NewLine +
                                        "3. DMI displays symbol TC15 in sub-area B4." + Environment.NewLine +
                                        "4. DMI displays symbol TC17 in sub-area B5.");

                    // Step 22
                    trackCondition.MMI_NID_TRACKCOND = 10;
                    trackCondition.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.RemoveTC;
                    EVC32_MMITrackConditions.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        "1. DMI removes symbol TC15 from sub-area B3." + Environment.NewLine +
                                        "2. DMI displays symbol TC15 in sub-area B3." + Environment.NewLine +
                                        "3. DMI displays symbol TC17 in sub-area B4." + Environment.NewLine +
                                        "4. DMI displays symbol TC17 in sub-area B5.");

                    // Step 23
                    trackCondition.MMI_NID_TRACKCOND = 11;
                    trackCondition.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.RemoveTC;
                    EVC32_MMITrackConditions.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        "1. DMI removes symbol TC15 from sub-area B3." + Environment.NewLine +
                                        "2. DMI displays symbol TC17 in sub-area B3." + Environment.NewLine +
                                        "3. DMI displays symbol TC17 in sub-area B4." + Environment.NewLine +
                                        "4. DMI displays symbol TC19 in sub-area B5.");

                    // Step 24
                    trackCondition.MMI_NID_TRACKCOND = 12;
                    trackCondition.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.RemoveTC;
                    EVC32_MMITrackConditions.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        "1. DMI removes symbol TC15 from sub-area B3." + Environment.NewLine +
                                        "2. DMI displays symbol TC17 in sub-area B3." + Environment.NewLine +
                                        "3. DMI displays symbol TC17 in sub-area B4." + Environment.NewLine +
                                        "4. DMI displays symbol TC19 in sub-area B5.");

                    // Step 25
                    trackCondition.MMI_NID_TRACKCOND = 13;
                    trackCondition.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.RemoveTC;
                    EVC32_MMITrackConditions.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        "1. DMI removes symbol TC17 from sub-area B3." + Environment.NewLine +
                                        "2. DMI displays symbol TC19 in sub-area B3." + Environment.NewLine +
                                        "3. DMI displays symbol TC19 in sub-area B4." + Environment.NewLine +
                                        "4. DMI displays symbol TC20 in sub-area B5.");

                    // Step 26
                    trackCondition.MMI_NID_TRACKCOND = 14;
                    trackCondition.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.RemoveTC;
                    EVC32_MMITrackConditions.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        "1. DMI removes symbol TC19 from sub-area B3." + Environment.NewLine +
                                        "2. DMI displays symbol TC19 in sub-area B3." + Environment.NewLine +
                                        "3. DMI displays symbol TC20 in sub-area B4." + Environment.NewLine +
                                        "4. DMI displays symbol TC23 in sub-area B5.");

                    // Step 27
                    trackCondition.MMI_NID_TRACKCOND = 15;
                    trackCondition.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.RemoveTC;
                    EVC32_MMITrackConditions.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        "1. DMI removes symbol TC19 from sub-area B3." + Environment.NewLine +
                                        "2. DMI displays symbol TC20 in sub-area B3." + Environment.NewLine +
                                        "3. DMI displays symbol TC23 in sub-area B4." + Environment.NewLine +
                                        "4. DMI displays symbol TC23 in sub-area B5.");

                    // Step 28
                    trackCondition.MMI_NID_TRACKCOND = 16;
                    trackCondition.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.RemoveTC;
                    EVC32_MMITrackConditions.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        "1. DMI removes symbol TC20 from sub-area B3." + Environment.NewLine +
                                        "2. DMI displays symbol TC23 in sub-area B3." + Environment.NewLine +
                                        "3. DMI displays symbol TC23 in sub-area B4." + Environment.NewLine +
                                        "4. DMI displays symbol TC25 in sub-area B5.");

                    // Step 29
                    trackCondition.MMI_NID_TRACKCOND = 17;
                    trackCondition.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.RemoveTC;
                    EVC32_MMITrackConditions.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        "1. DMI removes symbol TC23 from sub-area B3." + Environment.NewLine +
                                        "2. DMI displays symbol TC23 in sub-area B3." + Environment.NewLine +
                                        "3. DMI displays symbol TC25 in sub-area B4." + Environment.NewLine +
                                        "4. DMI displays symbol TC25 in sub-area B5.");

                    // Step 30
                    trackCondition.MMI_NID_TRACKCOND = 18;
                    trackCondition.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.RemoveTC;
                    EVC32_MMITrackConditions.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        "1. DMI removes symbol TC23 from sub-area B3" + Environment.NewLine +
                                        "2. DMI displays symbol TC25 in sub-area B3." + Environment.NewLine +
                                        "3. DMI displays symbol TC25 in sub-area B4." + Environment.NewLine +
                                        "4. DMI displays symbol TC27 in sub-area B5.");

                    // Step 31
                    trackCondition.MMI_NID_TRACKCOND = 19;
                    trackCondition.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.RemoveTC;
                    EVC32_MMITrackConditions.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        "1. DMI removes symbol TC25 from sub-area B3." + Environment.NewLine +
                                        "2. DMI displays symbol TC25 in sub-area B3." + Environment.NewLine +
                                        "3. DMI displays symbol TC27 in sub-area B4." + Environment.NewLine +
                                        "4. DMI displays symbol TC27 in sub-area B5.");

                    // Step 32
                    trackCondition.MMI_NID_TRACKCOND = 20;
                    trackCondition.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.RemoveTC;
                    EVC32_MMITrackConditions.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        "1. DMI removes symbol TC25 from sub-area B3." + Environment.NewLine +
                                        "2. DMI displays symbol TC27 in sub-area B3." + Environment.NewLine +
                                        "3. DMI displays symbol TC27 in sub-area B4." + Environment.NewLine +
                                        "4. DMI displays symbol TC29 in sub-area B5.");

                    // Step 33
                    trackCondition.MMI_NID_TRACKCOND = 21;
                    trackCondition.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.RemoveTC;
                    EVC32_MMITrackConditions.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        "1. DMI removes symbol TC27 from sub-area B3." + Environment.NewLine +
                                        "2. DMI displays symbol TC27 in sub-area B3." + Environment.NewLine +
                                        "3. DMI displays symbol TC29 in sub-area B4." + Environment.NewLine +
                                        "4. DMI displays symbol TC29 in sub-area B5.");

                    // Step 34
                    trackCondition.MMI_NID_TRACKCOND = 22;
                    trackCondition.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.RemoveTC;
                    EVC32_MMITrackConditions.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        "1. DMI removes symbol TC27 from sub-area B3." + Environment.NewLine +
                                        "2. DMI displays symbol TC29 in sub-area B3." + Environment.NewLine +
                                        "3. DMI displays symbol TC29 in sub-area B4." + Environment.NewLine +
                                        "4. DMI displays symbol TC31 in sub-area B5.");

                    // Step 35
                    trackCondition.MMI_NID_TRACKCOND = 23;
                    trackCondition.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.RemoveTC;
                    EVC32_MMITrackConditions.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        "1. DMI removes symbol TC29 from sub-area B3." + Environment.NewLine +
                                        "2. DMI displays symbol TC29 in sub-area B3." + Environment.NewLine +
                                        "3. DMI displays symbol TC31 in sub-area B4." + Environment.NewLine +
                                        "4. DMI displays symbol TC31 in sub-area B5.");

                    // Step 36
                    trackCondition.MMI_NID_TRACKCOND = 24;
                    trackCondition.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.RemoveTC;
                    EVC32_MMITrackConditions.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        "1. DMI removes symbol TC29 from sub-area B3." + Environment.NewLine +
                                        "2. DMI displays symbol TC31 in sub-area B3." + Environment.NewLine +
                                        "3. DMI displays symbol TC31 in sub-area B4." + Environment.NewLine +
                                        "4. DMI displays symbol TC33 in sub-area B5.");

                    // Step 37
                    trackCondition.MMI_NID_TRACKCOND = 25;
                    trackCondition.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.RemoveTC;
                    EVC32_MMITrackConditions.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        "1. DMI removes symbol TC31 from sub-area B3." + Environment.NewLine +
                                        "2. DMI displays symbol TC31 in sub-area B3." + Environment.NewLine +
                                        "3. DMI displays symbol TC33 in sub-area B4." + Environment.NewLine +
                                        "4. DMI displays symbol TC33 in sub-area B5.");

                    // Step 38
                    trackCondition.MMI_NID_TRACKCOND = 26;
                    trackCondition.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.RemoveTC;
                    EVC32_MMITrackConditions.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        "1. DMI removes symbol TC31 from sub-area B3." + Environment.NewLine +
                                        "2. DMI displays symbol TC33 in sub-area B3." + Environment.NewLine +
                                        "3. DMI displays symbol TC33 in sub-area B4." + Environment.NewLine +
                                        "4. DMI displays symbol TC03 in sub-area B5.");

                    // Step 39
                    trackCondition.MMI_NID_TRACKCOND = 27;
                    trackCondition.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.RemoveTC;
                    EVC32_MMITrackConditions.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        "1. DMI removes symbol TC33 from sub-area B3." + Environment.NewLine +
                                        "2. DMI displays symbol TC33 in sub-area B3." + Environment.NewLine +
                                        "3. DMI displays symbol TC03 in sub-area B4." + Environment.NewLine +
                                        "4. DMI displays no symbol in sub-area B5.");

                    // Step 40
                    trackCondition.MMI_NID_TRACKCOND = 28;
                    trackCondition.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.RemoveTC;
                    EVC32_MMITrackConditions.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        "1. DMI removes symbol TC33 from sub-area B3." + Environment.NewLine +
                                        "2. DMI displays symbol TC03 in sub-area B3 with a yellow flashing frame." +
                                        Environment.NewLine +
                                        "3. DMI displays no symbol in sub-area B4." + Environment.NewLine +
                                        "4. DMI displays no symbol in sub-area B5.");

                    // Step 41
                    trackCondition.MMI_NID_TRACKCOND = 29;
                    trackCondition.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.RemoveTC;
                    EVC32_MMITrackConditions.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        "1. DMI removes symbol TC36 from sub-area C1");

                    break;
                case msgType.typec:

                    // Step 46
                    EVC32_MMITrackConditions.MMI_Q_TRACKCOND_UPDATE = 0;

                    TrackCondition trackConditionC = new TrackCondition
                    {
                        MMI_O_TRACKCOND_ANNOUNCE = 0,
                        MMI_O_TRACKCOND_START = 0,
                        MMI_O_TRACKCOND_END = 0,
                        MMI_NID_TRACKCOND = 0,
                        MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Pantograph,
                        MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.LeaveArea,
                        MMI_Q_TRACKCOND_ACTION_START = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction,
                        MMI_Q_TRACKCOND_ACTION_END = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction
                    };

                    EVC32_MMITrackConditions.TrackConditions = new List<TrackCondition> {trackConditionC};

                    EVC32_MMITrackConditions.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        "1. DMI displays symbol TC05 in sub-area B3 with a yellow flashing frame.");

                    // Step 47
                    trackConditionC.MMI_M_TRACKCOND_TYPE =
                        Variables.MMI_M_TRACKCOND_TYPE.Main_power_switch_Neutral_Section;
                    trackConditionC.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.AnnounceArea;
                    EVC32_MMITrackConditions.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        "1. DMI displays symbol TC07 in sub-area B3 with a yellow flashing frame.");


                    // Step 48
                    trackConditionC.MMI_M_TRACKCOND_TYPE =
                        Variables.MMI_M_TRACKCOND_TYPE.Main_power_switch_Neutral_Section;
                    trackConditionC.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.LeaveArea;
                    EVC32_MMITrackConditions.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        "1. DMI displays symbol TC09 in sub-area B3 with a yellow flashing frame.");

                    // Step 49
                    trackConditionC.MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Non_Stopping_Area;
                    trackConditionC.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.AnnounceArea;
                    EVC32_MMITrackConditions.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        "1. DMI displays symbol TC11 in sub-area B3 with a yellow flashing frame.");

                    // Step 50
                    trackConditionC.MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Magnetic_Shoe_Brakes;
                    trackConditionC.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.AnnounceArea;
                    EVC32_MMITrackConditions.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        "1. DMI displays symbol TC14 in sub-area B3 with a yellow flashing frame.");

                    // Step 51
                    trackConditionC.MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Eddy_Current_Brakes;
                    trackConditionC.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.AnnounceArea;
                    EVC32_MMITrackConditions.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        "1. DMI displays symbol TC16 in sub-area B3 with a yellow flashing frame.");

                    // Step 52
                    trackConditionC.MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Regenerative_Brakes;
                    trackConditionC.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.AnnounceArea;
                    EVC32_MMITrackConditions.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        "1. DMI displays symbol TC18 in sub-area B3 with a yellow flashing frame.");

                    // Step 53
                    trackConditionC.MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Air_tightness;
                    trackConditionC.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.AnnounceArea;
                    EVC32_MMITrackConditions.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        "1. DMI displays symbol TC21 in sub-area B3 with a yellow flashing frame.");

                    // Step 54
                    trackConditionC.MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Air_tightness;
                    trackConditionC.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.LeaveArea;
                    EVC32_MMITrackConditions.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        "1. DMI displays symbol TC22 in sub-area B3 with a yellow flashing frame.");

                    // Step 55
                    trackConditionC.MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Change_traction_not_fitted;
                    trackConditionC.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.AnnounceArea;
                    EVC32_MMITrackConditions.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        "1. DMI displays symbol TC24 in sub-area B3 with a yellow flashing frame.");

                    // Step 56
                    trackConditionC.MMI_M_TRACKCOND_TYPE =
                        Variables.MMI_M_TRACKCOND_TYPE.Change_traction_AC_25_kV_50_Hz;
                    trackConditionC.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.AnnounceArea;
                    EVC32_MMITrackConditions.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        "1. DMI displays symbol TC26 in sub-area B3 with a yellow flashing frame.");

                    // Step 57
                    trackConditionC.MMI_M_TRACKCOND_TYPE =
                        Variables.MMI_M_TRACKCOND_TYPE.Change_traction_AC_15_kV_16_7_Hz;
                    trackConditionC.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.AnnounceArea;
                    EVC32_MMITrackConditions.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        "1. DMI displays symbol TC28 in sub-area B3 with a yellow flashing frame.");

                    // Step 58
                    trackConditionC.MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Change_traction_DC_3_kV;
                    trackConditionC.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.AnnounceArea;
                    EVC32_MMITrackConditions.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        "1. DMI displays symbol TC30 in sub-area B3 with a yellow flashing frame.");

                    // Step 59
                    trackConditionC.MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Change_traction_DC_1_5_kV;
                    trackConditionC.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.AnnounceArea;
                    EVC32_MMITrackConditions.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        "1. DMI displays symbol TC32 in sub-area B3 with a yellow flashing frame.");

                    // Step 60
                    trackConditionC.MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Change_traction_DC_600_750;
                    trackConditionC.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.AnnounceArea;
                    EVC32_MMITrackConditions.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        "1. DMI displays symbol TC34 in sub-area B3 with a yellow flashing frame.");

                    // Step 61
                    trackConditionC.MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Sound_Horn;
                    trackConditionC.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.AnnounceArea;
                    EVC32_MMITrackConditions.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        "1. DMI displays symbol TC35 in sub-area B3 with a yellow flashing frame.");

                    // Step 62
                    trackConditionC.MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Tunnel_Stopping_Area;
                    trackConditionC.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.AnnounceArea;
                    EVC32_MMITrackConditions.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        "1. DMI displays symbol TC37 in sub-area B3 with a yellow flashing frame.");

                    break;
            }
        }

        #endregion
    }
}
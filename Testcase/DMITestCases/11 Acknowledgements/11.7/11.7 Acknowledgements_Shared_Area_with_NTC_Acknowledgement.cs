using System;
using BT_CSB_Tools;
using Testcase.Telegrams.EVCtoDMI;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// Updated to DMI Test Spec 4.4 by JS at 2018-02-16
    /// 
    /// 11.7 Acknowledgements: Shared Area with NTC-Acknowledgement
    /// THIS TESTCASE HAS BEEN REMOVED IN 4.4
    /// </summary>
    public class TC_ID_6_7_Acknowledgements : TestcaseBase
    {
        public override bool TestcaseEntryPoint()
        {
            throw new TestcaseException("This TestCase has been removed from DMI Test Spec 4.4");
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 20896;
            // Testcase entrypoint


            MakeTestStepHeader(1, UniqueIdentifier++, "Perform SoM to Level STM-ATB in SN mode",
                "ETCS OB enters Level STM-ATB, SN mode");
            /*
            Test Step 1
            Action: Perform SoM to Level STM-ATB in SN mode
            Expected Result: ETCS OB enters Level STM-ATB, SN mode
            */
            
            MakeTestStepHeader(2, UniqueIdentifier++,
                "Use test script file 6_7.xml to send STM text message and ETCS text message to the DMI via STM-38 and EVC-8, respectively with:-Message 1: STM-38- MMI_STM_Q_ACK = 1- MMI_STM_X_TEXT = 2Message 2: EVC-8- MMI_Q_TEXT_CLASS = 0- MMI_Q_TEXT_CRITERIA = 1- MMI_Q_TEXT = 268Message 3: STM-38- MMI_STM_Q_ACK = 1- MMI_STM_X_TEXT = 4Message 4: EVC-8- MMI_Q_TEXT_CLASS = 0- MMI_Q_TEXT_CRITERIA = 1- MMI_Q_TEXT = 305",
                "Verify the following information:After DMI receives test script- Text message ‘2 - Brakes are not operated’ is displayed in sub-area E5 with yellow flashing frame surrounded area (E5+E6+E7+E8+E9).- Sound Sinfo is not played.5 seconds later- Text message ‘Communication error’ is displayed in sub-area E5 with yellow flashing frame surrounded area (E5+E6+E7+E8+E9).- Sound Sinfo is played once.5 seconds later- Text message ‘4 - Brake feedback fault’ is displayed in sub-area E5 with yellow flashing frame surrounded area (E5+E6+E7+E8+E9).- Sound Sinfo is not played.5 seconds later- Text message ‘Train divided’ is displayed in sub-area E5 with yellow flashing frame surrounded area (E5+E6+E7+E8+E9).- Sound Sinfo is played once");
            /*
            Test Step 2
            Action: Use test script file 6_7.xml to send STM text message and ETCS text message to the DMI via STM-38 and EVC-8, respectively with:-Message 1: STM-38- MMI_STM_Q_ACK = 1- MMI_STM_X_TEXT = 2Message 2: EVC-8- MMI_Q_TEXT_CLASS = 0- MMI_Q_TEXT_CRITERIA = 1- MMI_Q_TEXT = 268Message 3: STM-38- MMI_STM_Q_ACK = 1- MMI_STM_X_TEXT = 4Message 4: EVC-8- MMI_Q_TEXT_CLASS = 0- MMI_Q_TEXT_CRITERIA = 1- MMI_Q_TEXT = 305
            Expected Result: Verify the following information:After DMI receives test script- Text message ‘2 - Brakes are not operated’ is displayed in sub-area E5 with yellow flashing frame surrounded area (E5+E6+E7+E8+E9).- Sound Sinfo is not played.5 seconds later- Text message ‘Communication error’ is displayed in sub-area E5 with yellow flashing frame surrounded area (E5+E6+E7+E8+E9).- Sound Sinfo is played once.5 seconds later- Text message ‘4 - Brake feedback fault’ is displayed in sub-area E5 with yellow flashing frame surrounded area (E5+E6+E7+E8+E9).- Sound Sinfo is not played.5 seconds later- Text message ‘Train divided’ is displayed in sub-area E5 with yellow flashing frame surrounded area (E5+E6+E7+E8+E9).- Sound Sinfo is played once
            Test Step Comment: MMI_gen 4483 (partly: NTC);
            */
            
            MakeTestStepHeader(3, UniqueIdentifier++, "Press sub-area (E5+E6+E7+E8+E9) for acknowledgement",
                "Verify the following information:- Text message ‘Train divided’ is disappeared.- Text message ‘4 - Brake feedback fault’ is reappeared in sub-area E5 with yellow flashing frame surrounded area (E5+E6+E7+E8+E9)");
            /*
            Test Step 3
            Action: Press sub-area (E5+E6+E7+E8+E9) for acknowledgement
            Expected Result: Verify the following information:- Text message ‘Train divided’ is disappeared.- Text message ‘4 - Brake feedback fault’ is reappeared in sub-area E5 with yellow flashing frame surrounded area (E5+E6+E7+E8+E9)
            Test Step Comment: MMI_gen 4483 (partly: NTC);
            */
            
            MakeTestStepHeader(4, UniqueIdentifier++, "Press sub-area (E5+E6+E7+E8+E9) for acknowledgement",
                "Verify the following information:- Text message ‘4 - Brake feedback fault’ is disappeared.- Text message ‘Communication error’ is reappeared in sub-area E5 with yellow flashing frame surrounded area (E5+E6+E7+E8+E9)");
            /*
            Test Step 4
            Action: Press sub-area (E5+E6+E7+E8+E9) for acknowledgement
            Expected Result: Verify the following information:- Text message ‘4 - Brake feedback fault’ is disappeared.- Text message ‘Communication error’ is reappeared in sub-area E5 with yellow flashing frame surrounded area (E5+E6+E7+E8+E9)
            Test Step Comment: MMI_gen 4483 (partly: NTC);
            */
            
            MakeTestStepHeader(5, UniqueIdentifier++, "Press sub-area (E5+E6+E7+E8+E9) for acknowledgement",
                "Verify the following information:- Text message ‘Communication error’ is disappeared.- Text message ‘2 - Brakes are not operated’ is reappeared in sub-area E5 with yellow flashing frame surrounded area (E5+E6+E7+E8+E9)");
            /*
            Test Step 5
            Action: Press sub-area (E5+E6+E7+E8+E9) for acknowledgement
            Expected Result: Verify the following information:- Text message ‘Communication error’ is disappeared.- Text message ‘2 - Brakes are not operated’ is reappeared in sub-area E5 with yellow flashing frame surrounded area (E5+E6+E7+E8+E9)
            Test Step Comment: MMI_gen 4483 (partly: NTC);
            */
            
            MakeTestStepHeader(6, UniqueIdentifier++, "Press sub-area (E5+E6+E7+E8+E9) for acknowledgement",
                "- Text message ‘2 - Brakes are not operated’ is disappeared");
            /*
            Test Step 6
            Action: Press sub-area (E5+E6+E7+E8+E9) for acknowledgement
            Expected Result: - Text message ‘2 - Brakes are not operated’ is disappeared
            Test Step Comment: This test step is to clear expected result of the previous test step.
            */
            
            TraceHeader("End of test");

            /*
            Test Step 7
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
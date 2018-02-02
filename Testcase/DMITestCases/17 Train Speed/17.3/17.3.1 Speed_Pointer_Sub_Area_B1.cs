namespace Testcase.DMITestCases
{
    /// <summary>
    /// 17.3.1 Speed Pointer: Sub-Area B1
    /// TC-ID: 12.3
    /// 
    /// This test case verifies the presentation of speed pointer that displays in sub-area B1.
    /// The dimensions and colours of speed pointer shall comply with [ERA] standard.
    /// 
    /// Tested Requirements:
    /// MMI_gen 5965; MMI_gen 5968; 
    /// 
    /// Scenario:
    /// Drive the train forward. Then, verify the display of speed pointer.
    /// 
    /// Used files:
    /// N/A
    /// </summary>
    public class TC_12_3_Train_Speed : TestcaseBase
    {
        public override bool TestcaseEntryPoint()
        {
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 0;
            // Testcase entrypoint

            TraceInfo("This is a repeat of TC_12_1_Display_of_Speed_Pointer_and_Speed_Digital." +
                      "Please see results of this test case.");

            GlobalTestResult = true;

            MakeTestStepHeader(1, UniqueIdentifier++, "Drive the train forward, speed up to 25 km/h",
                "Verify the following information,The speed pointer is displayed in sub-area B1. The speed pointer consists of a needle and a circular part centred in sub-area B1. Both parts are displayed in same colour. The dimension of the speed pointer is presented");
            /*
            Test Step 1
            Action: Drive the train forward, speed up to 25 km/h
            Expected Result: Verify the following information,The speed pointer is displayed in sub-area B1. The speed pointer consists of a needle and a circular part centred in sub-area B1. Both parts are displayed in same colour. The dimension of the speed pointer is presented
            Test Step Comment: (1) MMI_gen 5965;   (2) MMI_gen 5968;   
            */

            MakeTestStepHeader(2, UniqueIdentifier++, "Stop the train", "The speed pointer is indicated to zero km/h");
            /*
            Test Step 2
            Action: Stop the train
            Expected Result: The speed pointer is indicated to zero km/h
            */
            // Call generic Action Method

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
namespace Testcase.DMITestCases
{
    /// <summary>
    /// 6.8 Accleration/Decleration interval -4.0m/s2 to +4.0 m/s2
    /// TC-ID: 1.8
    /// 
    /// This test case is to verify ETCS MMI can handle accleration /decleration in the interval – 4m/s2 and +4m/s2 
    /// 
    /// Tested Requirements:
    /// MMI_gen 68;
    /// 
    /// Scenario:
    /// 1.Perform Start of Mission to L1, SR mode
    /// 2.Pass BG0 with MA and Track description
    /// 3.Mode changes to FS mode 
    /// 4.Increase the train speed with full acceleration until service barke is applied
    /// 5.Verify that the speedometer movement goes up and down smoothly
    /// 
    /// Used files:
    /// 1_8.tdg
    /// </summary>
    public class TC_1_8_AcclerationDecleration : TestcaseBase
    {
        public override bool TestcaseEntryPoint()
        {
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 0;
            // Testcase entrypoint

            DmiActions.ShowInstruction(this, "THIS TESCASE NEEDS TO BE RUN WITH ETCS AND VSIM");

            MakeTestStepHeader(1, UniqueIdentifier++, "Power on the system and activate cabin",
                "DMI displays in SB mode");
            /*
            Test Step 1
            Action: Power on the system and activate cabin
            Expected Result: DMI displays in SB mode
            */


            MakeTestStepHeader(2, UniqueIdentifier++, "Perform Start of Mission to SR mode , eevel 1",
                "Mode changes to SR mode , Level 1");
            /*
            Test Step 2
            Action: Perform Start of Mission to SR mode , eevel 1
            Expected Result: Mode changes to SR mode , Level 1
            */


            MakeTestStepHeader(3, UniqueIdentifier++, "Pass BG0 with MA and Track description",
                "Mode changes to FS mode");
            /*
            Test Step 3
            Action: Pass BG0 with MA and Track description
            Expected Result: Mode changes to FS mode
            */


            MakeTestStepHeader(4, UniqueIdentifier++,
                "Full accelerate the traction (100%) until service brake is applied",
                "The speedometer movement goes up and down smoothly");
            /*
            Test Step 4
            Action: Full accelerate the traction (100%) until service brake is applied
            Expected Result: The speedometer movement goes up and down smoothly
            Test Step Comment: MMI_gen 68;
            */


            MakeTestStepHeader(5, UniqueIdentifier++, "End of Test", "");

            /*
            Test Step 5
            Action: End of Test
            Expected Result: 
            */


            return GlobalTestResult;
        }
    }
}
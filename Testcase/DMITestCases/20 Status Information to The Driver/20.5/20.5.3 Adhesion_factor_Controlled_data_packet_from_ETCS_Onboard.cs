namespace Testcase.DMITestCases
{
    /// <summary>
    /// 20.5.3 Adhesion factor: Controlled data packet from ETCS Onboard
    /// TC-ID: 15.5.2
    /// 
    /// This test case verifies the display of the adhesion factor indication on DMI when the factor is activated by both driver
    /// and Trackside together.
    /// 
    /// Tested Requirements:
    /// MMI_gen 7088; MMI_gen 600; MMI_gen 8435;
    /// 
    /// Scenario:
    /// Drive the train past BG0 at position 100 m: packet 3 Q_NVDRIVER_ADHES = 0 (to reset the value if it has been set)
    /// Drive the train past BG1 at position 250 m: packet 3 Q_NVDRIVER_ADHES = 1
    /// 
    /// The adhesion factor indication is verified with the following events:
    /// 1. (Redundant Case) Activate Slippery button at Adhesion window.
    /// The ‘Adhesion’ button is revoked when the train passes BG2 at position 600 m:
    ///     packet 71 D_ADHESION = 0, L_ADHESION = 200, M_ADHESION = 0 (Slippery)
    /// Drive the train past length of reduced adhesion at 800 m. Deactivates Slippery button.
    /// 
    /// Used files:
    /// 15_5_2.tdg
    /// </summary>
    public class TC_15_5_2_Adhesion_Factor : TestcaseBase
    {

        public override bool TestcaseEntryPoint()
        {
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 0;
            // Testcase entrypoint
            DmiExpectedResults.Testcase_not_required(this, "15.5.1", "20.5.1");

            MakeTestStepHeader(1, UniqueIdentifier++,
                "Driver drives the train forward passing BG1.Then, press ‘Special’ button",
                "DMI still displays in SR mode.Verify that ‘Adhesion’ button is enabled");
            /*
            Test Step 1
            Action: Driver drives the train forward passing BG1.Then, press ‘Special’ button
            Expected Result: DMI still displays in SR mode.Verify that ‘Adhesion’ button is enabled
            */

            MakeTestStepHeader(2, UniqueIdentifier++, "Press ‘Adhesion’ button.Then, press ‘Slippery rail’ button",
                "Verify the following information,Use the log file to confirm that DMI receives EVC-2 with variable MMI_M_ADHESION (#0) = 1, bit ‘Low Adhesion by Driver’ is set.DMI displays symbol ST02 in sub-area A4, by driver");
            /*
            Test Step 2
            Action: Press ‘Adhesion’ button.Then, press ‘Slippery rail’ button
            Expected Result: Verify the following information,Use the log file to confirm that DMI receives EVC-2 with variable MMI_M_ADHESION (#0) = 1, bit ‘Low Adhesion by Driver’ is set.DMI displays symbol ST02 in sub-area A4, by driver
            Test Step Comment: (1) MMI_gen 7088 (partly: EVC-2, ‘Low Adhesion by Driver’)(2) MMI_gen 111;     
            */

            MakeTestStepHeader(3, UniqueIdentifier++, "Drive the train forward passing BG2",
                "Verify the following information,Use the log file to confirm that DMI receives EVC-2 with variable MMI_M_ADHESION (#1) = 1, bit ‘Low Adhesion from Trackside’ is set.DMI displays symbol ST02 in sub-area A4");
            /*
            Test Step 3
            Action: Drive the train forward passing BG2
            Expected Result: Verify the following information,Use the log file to confirm that DMI receives EVC-2 with variable MMI_M_ADHESION (#1) = 1, bit ‘Low Adhesion from Trackside’ is set.DMI displays symbol ST02 in sub-area A4
            Test Step Comment: (1) MMI_gen 7088 (partly: EVC-2, ‘Low Adhesion from Trackside’, ‘Low Adhesion by Driver’);(2) MMI_gen 111;
            */

            MakeTestStepHeader(4, UniqueIdentifier++, "Drive the train forward",
                "Verify the following information,Use the log file to confirm that DMI receives EVC-2 with following variable,MMI_M_ADHESION (#1) = 0, bit ‘Low Adhesion from Trackside’ is not set.DMI displays symbol ST02 in sub-area A4, by driver");
            /*
            Test Step 4
            Action: Drive the train forward
            Expected Result: Verify the following information,Use the log file to confirm that DMI receives EVC-2 with following variable,MMI_M_ADHESION (#1) = 0, bit ‘Low Adhesion from Trackside’ is not set.DMI displays symbol ST02 in sub-area A4, by driver
            Test Step Comment: (1) MMI_gen 7088 (partly: EVC-2, ‘Low Adhesion by Driver’)(2) MMI_gen 111;     
            */

            MakeTestStepHeader(5, UniqueIdentifier++,
                "Perform the following procedure,Press ‘Special’ button.Press ‘Adhesion’ button.Select and confirm ‘Non slippery rail’ button",
                "No adhesion factor indication is displayed.Verify the following information,Use the log file to confirm that DMI receives EVC-2 with following variable,MMI_M_ADHESION (#0) = 0, bit ‘Low Adhesion by Driver’ is not set");
            /*
            Test Step 5
            Action: Perform the following procedure,Press ‘Special’ button.Press ‘Adhesion’ button.Select and confirm ‘Non slippery rail’ button
            Expected Result: No adhesion factor indication is displayed.Verify the following information,Use the log file to confirm that DMI receives EVC-2 with following variable,MMI_M_ADHESION (#0) = 0, bit ‘Low Adhesion by Driver’ is not set
            Test Step Comment: (1) MMI_gen 7088 (partly: No symbol displayed);    
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
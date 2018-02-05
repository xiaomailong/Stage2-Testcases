namespace Testcase.DMITestCases
{
    /// <summary>
    /// Sound S1 - Over Speed
    /// TC-ID: 21.1.1
    /// 
    /// This test case verifies that sound ‘S1_toofast.wav’ is played once when current train speed is exceeded permitted supervision limit in TSM supervision.
    /// 
    /// Tested Requirements:
    /// MMI_gen 11919;
    /// 
    /// Scenario:
    /// 1.Perform SoM to Level 1 in SR mode.
    /// 2.Drive the train forward with speed at 40 km/h.
    /// 3.Train runs pass BG1 at position 100 m. that contained pkt 12, pkt 21 and pkt 27 to enter FS mode.
    /// 4.Stop the train.
    /// 5.Use XML script to send EVC-1 to the DMI.
    /// 
    /// Used files:
    /// 21_1_1.tdg and 21_1_1.xml
    /// </summary>
    public class S1_Over_Speed : TestcaseBase
    {
        public override bool TestcaseEntryPoint()
        {
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 0;
            // Testcase entrypoint


            MakeTestStepHeader(1, UniqueIdentifier++, "Perform SoM to Level 1 in SR mode",
                "ETCS OB enters SR mode in Level 1");
            /*
            Test Step 1
            Action: Perform SoM to Level 1 in SR mode
            Expected Result: ETCS OB enters SR mode in Level 1
            */
            // Call generic Action Method
            StartUp();
            DmiActions.Perform_SoM_to_Level_1_in_SR_mode(this);
            // Call generic Check Results Method
            DmiExpectedResults.ETCS_OB_enters_SR_mode_in_Level_1(this);


            MakeTestStepHeader(2, UniqueIdentifier++, "Drive the train forward with constant speed at 40 km/h",
                "The train can drive forward and all brakes are not applied");
            /*
            Test Step 2
            Action: Drive the train forward with constant speed at 40 km/h
            Expected Result: The train can drive forward and all brakes are not applied
            */
            // Call generic Action Method
            DmiActions.Drive_the_train_forward_with_constant_speed_at_40_kmh(this);
            // Call generic Check Results Method
            DmiExpectedResults.The_train_can_drive_forward_and_all_brakes_are_not_applied(this);


            MakeTestStepHeader(3, UniqueIdentifier++, "Train runs pass BG1 and keep train speed at 40 km/h",
                "ETCS OB enters FS mode in Level 1");
            /*
            Test Step 3
            Action: Train runs pass BG1 and keep train speed at 40 km/h
            Expected Result: ETCS OB enters FS mode in Level 1
            */
            // Call generic Check Results Method
            DmiExpectedResults.ETCS_OB_enters_FS_mode_in_Level_1(this);


            MakeTestStepHeader(4, UniqueIdentifier++,
                "Train enters TSM supervision and the permitted speed is gradually reduced until below the current train speed",
                "Sound ‘S1_toofast.wav’ is played once when over-speed status in TSM supervision is active as figure below.Use log file to verify that train speed is exceeded permitted supervision limit in TSM when DMI receives EVC-1 with variable [MMI_M_WARNING = 9]");
            /*
            Test Step 4
            Action: Train enters TSM supervision and the permitted speed is gradually reduced until below the current train speed
            Expected Result: Sound ‘S1_toofast.wav’ is played once when over-speed status in TSM supervision is active as figure below.Use log file to verify that train speed is exceeded permitted supervision limit in TSM when DMI receives EVC-1 with variable [MMI_M_WARNING = 9]
            Test Step Comment: MMI_gen 11919 (tested with TDG file)Note Sound file is stored in DMI_ERTMS_BL3 product in database path:/proj/ccmbkk3/mmi_v.
            */


            MakeTestStepHeader(5, UniqueIdentifier++, "Stop the train", "The train is at standstill");
            /*
            Test Step 5
            Action: Stop the train
            Expected Result: The train is at standstill
            */
            // Call generic Action Method
            DmiActions.Stop_the_train(this);
            // Call generic Check Results Method
            DmiExpectedResults.The_train_is_at_standstill(this);


            MakeTestStepHeader(6, UniqueIdentifier++,
                "Use test script 21_1_1.xml to send dynamic information via EVC-1 with:-- MMI_M_WARNING = 9- MMI_V_TRAIN = 1107- MMI_V_PERMITTED = 1101- MMI_V_INTERVENTION = 1242",
                "Sound ‘S1_toofast.wav’ is played once");
            /*
            Test Step 6
            Action: Use test script 21_1_1.xml to send dynamic information via EVC-1 with:-- MMI_M_WARNING = 9- MMI_V_TRAIN = 1107- MMI_V_PERMITTED = 1101- MMI_V_INTERVENTION = 1242
            Expected Result: Sound ‘S1_toofast.wav’ is played once
            Test Step Comment: MMI_gen 11919 (tested with XML script)
            */
            // Call generic Check Results Method
            DmiExpectedResults.Sound_S1_toofast_wav_is_played_once(this);


            MakeTestStepHeader(7, UniqueIdentifier++, "End of test", "");

            /*
            Test Step 7
            Action: End of test
            Expected Result: 
            */


            return GlobalTestResult;
        }
    }
}
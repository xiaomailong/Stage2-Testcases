namespace Testcase.DMITestCases
{
    /// <summary>
    /// Sound S1 - Driving too fast
    /// TC-ID: 36.1
    /// 
    /// This test case verifies that sound ‘S1_toofast.wav’ is played once when current train speed is exceeded permitted speed in CSM and PIM supervision.
    /// 
    /// Tested Requirements:
    /// MMI_gen 12029; MMI_gen 12060;
    /// 
    /// Scenario:
    /// 1.Perform SoM to Level 1 in SR mode.
    /// 2.Drive the train forward with speed at 40 km/h.
    /// 3.Train runs pass BG1 at position 100 m. that contained pkt 12, pkt 21 and pkt 27 to enter FS mode.
    /// 4.Accelerate the train with max acceleration (100% throttle) above permitted speed.
    /// 5.Stop the train.
    /// 6.Use XML script to send EVC-1 to the DMI.
    /// 7.Deactivate cabin A and power off the system.
    /// 8.Power on the system and perform SoM to Level 1 in SR mode.
    /// 9.Drive the train forward with speed at 40 km/h.
    /// 10.Train runs pass BG1 at position 100 m. that contained pkt 12, pkt 21 and pkt 27 to enter FS mode.
    /// 11.Accelerate the train with max acceleration (100% throttle) until speed at 95 km/h.
    /// 12.Wait until train enters PIM supervision and then increase train speed above permitted speed.
    /// 13.Stop the train.
    /// 14.Use XML script to send EVC-1 to the DMI.
    /// 
    /// Used files:
    /// 36_1.tdg, 36_1_a.xml and 36_1_b.xml
    /// </summary>
    public class S1_Driving_too_fast : TestcaseBase
    {
        public override bool TestcaseEntryPoint()
        {
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 26817;
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


            MakeTestStepHeader(2, UniqueIdentifier++, "Drive the train forward with speed at 40 km/h",
                "The train can drive forward and all brakes are not applied");
            /*
            Test Step 2
            Action: Drive the train forward with speed at 40 km/h
            Expected Result: The train can drive forward and all brakes are not applied
            */
            // Call generic Action Method
            DmiActions.Drive_the_train_forward_with_speed_at_40_kmh(this);
            // Call generic Check Results Method
            DmiExpectedResults.The_train_can_drive_forward_and_all_brakes_are_not_applied(this);


            MakeTestStepHeader(3, UniqueIdentifier++, "Train runs pass BG1", "ETCS OB enters FS mode in Level 1");
            /*
            Test Step 3
            Action: Train runs pass BG1
            Expected Result: ETCS OB enters FS mode in Level 1
            */
            // Call generic Action Method
            DmiActions.Train_runs_pass_BG1(this);
            // Call generic Check Results Method
            DmiExpectedResults.ETCS_OB_enters_FS_mode_in_Level_1(this);


            MakeTestStepHeader(4, UniqueIdentifier++,
                "Accelerate the train with max acceleration (100% throttle) above permitted speed",
                "(1) Sound ‘S1_toofast.wav’ is played once when over-speed status in CSM supervision is active as figure below.(2) Use log file to verify that train speed is exceeded permitted supervision limit in CSM when DMI receives EVC-1 with variable [MMI_M_WARNING = 8]");
            /*
            Test Step 4
            Action: Accelerate the train with max acceleration (100% throttle) above permitted speed
            Expected Result: (1) Sound ‘S1_toofast.wav’ is played once when over-speed status in CSM supervision is active as figure below.(2) Use log file to verify that train speed is exceeded permitted supervision limit in CSM when DMI receives EVC-1 with variable [MMI_M_WARNING = 8]
            Test Step Comment: (1) MMI_gen 12029 (partly: MMI_M_WARNING = 8 with TDG file)(2) MMI_gen 12060 (partly: MMI_M_WARNING = 8 with TDG file)Note Sound file is stored in DMI_ERTMS_BL3 product in database path:/proj/ccmbkk3/mmi_v.
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
                "Use test script 36_1_a.xml to send dynamic information via EVC-1 with:-- MMI_M_WARNING = 8- MMI_V_TRAIN = 2880- MMI_V_PERMITTED = 2777- MMI_V_INTERVENTION = 2929",
                "Sound ‘S1_toofast.wav’ is played once");
            /*
            Test Step 6
            Action: Use test script 36_1_a.xml to send dynamic information via EVC-1 with:-- MMI_M_WARNING = 8- MMI_V_TRAIN = 2880- MMI_V_PERMITTED = 2777- MMI_V_INTERVENTION = 2929
            Expected Result: Sound ‘S1_toofast.wav’ is played once
            Test Step Comment: MMI_gen 12060 (partly: MMI_M_WARNING = 8 with XML script)
            */
            // Call generic Check Results Method
            DmiExpectedResults.Sound_S1_toofast_wav_is_played_once(this);


            MakeTestStepHeader(7, UniqueIdentifier++, "Deactivate cabin A and power off the system",
                "System is power off and DMI displays ‘No contact with ATP’");
            /*
            Test Step 7
            Action: Deactivate cabin A and power off the system
            Expected Result: System is power off and DMI displays ‘No contact with ATP’
            */
            // Call generic Action Method
            DmiActions.Deactivate_cabin_A_and_power_off_the_system(this);
            // Call generic Check Results Method
            DmiExpectedResults.System_is_power_off_and_DMI_displays_No_contact_with_ATP(this);


            MakeTestStepHeader(8, UniqueIdentifier++, "Power on the system and perform SoM to Level 1 in SR mode",
                "ETCS OB enters SR mode in Level 1");
            /*
            Test Step 8
            Action: Power on the system and perform SoM to Level 1 in SR mode
            Expected Result: ETCS OB enters SR mode in Level 1
            */
            // Call generic Action Method
            DmiActions.Power_on_the_system_and_perform_SoM_to_Level_1_in_SR_mode(this);
            // Call generic Check Results Method
            DmiExpectedResults.ETCS_OB_enters_SR_mode_in_Level_1(this);


            MakeTestStepHeader(9, UniqueIdentifier++, "Drive the train forward with speed at 40 km/h",
                "The train can drive forward and all brakes are not applied");
            /*
            Test Step 9
            Action: Drive the train forward with speed at 40 km/h
            Expected Result: The train can drive forward and all brakes are not applied
            */
            // Call generic Action Method
            DmiActions.Drive_the_train_forward_with_speed_at_40_kmh(this);
            // Call generic Check Results Method
            DmiExpectedResults.The_train_can_drive_forward_and_all_brakes_are_not_applied(this);


            MakeTestStepHeader(10, UniqueIdentifier++, "Train runs pass BG1", "ETCS OB enters FS mode in Level 1");
            /*
            Test Step 10
            Action: Train runs pass BG1
            Expected Result: ETCS OB enters FS mode in Level 1
            */
            // Call generic Action Method
            DmiActions.Train_runs_pass_BG1(this);
            // Call generic Check Results Method
            DmiExpectedResults.ETCS_OB_enters_FS_mode_in_Level_1(this);


            MakeTestStepHeader(11, UniqueIdentifier++,
                "Accelerate the train with max acceleration (100% throttle) until speed at 95 km/h.Wait until train enters PIM supervision and then increase train speed above permitted speed",
                "(1) Sound ‘S1_toofast.wav’ is played once when over-speed status in PIM supervision is active as figure below.(2) Use log file to verify that train speed is exceeded permitted supervision limit in PIM when DMI receives EVC-1 with variable [MMI_M_WARNING = 10]");
            /*
            Test Step 11
            Action: Accelerate the train with max acceleration (100% throttle) until speed at 95 km/h.Wait until train enters PIM supervision and then increase train speed above permitted speed
            Expected Result: (1) Sound ‘S1_toofast.wav’ is played once when over-speed status in PIM supervision is active as figure below.(2) Use log file to verify that train speed is exceeded permitted supervision limit in PIM when DMI receives EVC-1 with variable [MMI_M_WARNING = 10]
            Test Step Comment: (1) MMI_gen 12029 (partly: MMI_M_WARNING = 10 with TDG file)(2) MMI_gen 12060 (partly: MMI_M_WARNING = 10 with TDG file)
            */


            MakeTestStepHeader(12, UniqueIdentifier++, "Stop the train", "The train is at standstill");
            /*
            Test Step 12
            Action: Stop the train
            Expected Result: The train is at standstill
            */
            // Call generic Action Method
            DmiActions.Stop_the_train(this);
            // Call generic Check Results Method
            DmiExpectedResults.The_train_is_at_standstill(this);


            MakeTestStepHeader(13, UniqueIdentifier++,
                "Use test script 36_1_b.xml to send dynamic information via EVC-1 with:-- MMI_M_WARNING = 10- MMI_V_TRAIN = 2806- MMI_V_PERMITTED = 2687- MMI_V_INTERVENTION = 2882",
                "Sound ‘S1_toofast.wav’ is played once");
            /*
            Test Step 13
            Action: Use test script 36_1_b.xml to send dynamic information via EVC-1 with:-- MMI_M_WARNING = 10- MMI_V_TRAIN = 2806- MMI_V_PERMITTED = 2687- MMI_V_INTERVENTION = 2882
            Expected Result: Sound ‘S1_toofast.wav’ is played once
            Test Step Comment: MMI_gen 12060 (partly: MMI_M_WARNING = 10 with XML script)
            */
            // Call generic Check Results Method
            DmiExpectedResults.Sound_S1_toofast_wav_is_played_once(this);


            MakeTestStepHeader(14, UniqueIdentifier++, "End of test", "");

            /*
            Test Step 14
            Action: End of test
            Expected Result: 
            */


            return GlobalTestResult;
        }
    }
}
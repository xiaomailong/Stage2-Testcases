namespace Testcase.DMITestCases
{
    /// <summary>
    /// PA Track Condition: Switch off eddy current brake in Sub-Area D2 and B3
    /// TC-ID: 17.4.7
    /// 
    /// This test case is to verify PA Track Condition ”Switch off eddy current brake” on Sub-Area D2 and B3. The track condition shall comply with [ERA] standard and [MMI-ETCS-gen].
    /// 
    /// Tested Requirements:
    /// MMI_gen 619 (partly: PL13 or PL14); MMI_gen 9980 (partly: Table45(PL13 or PL14)); MMI_gen 9979 (partly: ANNOUNCE); MMI_gen 662 (partly: TC15 or TC16); MMI_gen 10465 (partly: Table40(TC15 or TC16));  MMI_gen 9965; MMI_gen 636 (partly: ANNOUNCE); MMI_gen 2604 (partly: bottom of the symbol, D2);
    /// 
    /// Scenario:
    /// 1.Drive the train forward pass BG0 at position 10m.BG0: pkt 12, 21 and 27 (Entering FS) 
    /// 2.Drive the train forward pass BG1 at position 100m. Then,verify the display of PA track condition based on received packet EVC-32.BG1: pkt 68 (M_TRACKCOND = 7) (Switch off eddy current brake)
    /// 3.Verify the Track condition symbol in sub-Area D2 and B3
    /// 
    /// Used files:
    /// 17_4_7.tdg
    /// </summary>
    public class Track_Condition_Switch_off_eddy_current_brake_in_Sub_Area_D2_and_B3 : TestcaseBase
    {
        public override bool TestcaseEntryPoint()
        {
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 0;
            // Testcase entrypoint


            MakeTestStepHeader(1, UniqueIdentifier++, "Drive the train forward with speed = 20 km/h",
                "The speed pointer is indicated as 20  km/h");
            /*
            Test Step 1
            Action: Drive the train forward with speed = 20 km/h
            Expected Result: The speed pointer is indicated as 20  km/h
            */
            // Call generic Action Method
            DmiActions.Drive_the_train_forward_with_speed_20_kmh(this);
            // Call generic Check Results Method
            DmiExpectedResults.The_speed_pointer_is_indicated_as_20_kmh(this);


            MakeTestStepHeader(2, UniqueIdentifier++,
                "Drive the train forward pass BG0 with MA and Track descriptionPkt 12,21 and 27",
                "Mode changes to FS mode , L1");
            /*
            Test Step 2
            Action: Drive the train forward pass BG0 with MA and Track descriptionPkt 12,21 and 27
            Expected Result: Mode changes to FS mode , L1
            */
            // Call generic Action Method
            DmiActions.Drive_the_train_forward_pass_BG0_with_MA_and_Track_descriptionPkt_12_21_and_27(this);
            // Call generic Check Results Method
            DmiExpectedResults.Mode_changes_to_FS_mode_L1(this);


            MakeTestStepHeader(3, UniqueIdentifier++,
                "Pass BG1 with Track conditionPkt 68:D_TRACKCOND = 500L_TRACKCOND = 200M_TRACKCOND = 7(Switch off eddy current brake)",
                "Mode remians in FS mode");
            /*
            Test Step 3
            Action: Pass BG1 with Track conditionPkt 68:D_TRACKCOND = 500L_TRACKCOND = 200M_TRACKCOND = 7(Switch off eddy current brake)
            Expected Result: Mode remians in FS mode
            */
            // Call generic Check Results Method
            DmiExpectedResults.Mode_remians_in_FS_mode(this);


            MakeTestStepHeader(4, UniqueIdentifier++,
                "Enter Anouncement of Track condition “Switch off eddy current brake”",
                "Verify the following information(1)   DMI displays PL13 or PL14 symbol in sub-area D2. (PL13) or  (PL14)");
            /*
            Test Step 4
            Action: Enter Anouncement of Track condition “Switch off eddy current brake”
            Expected Result: Verify the following information(1)   DMI displays PL13 or PL14 symbol in sub-area D2. (PL13) or  (PL14)
            Test Step Comment: (1) MMI_gen 619 (partly: PL13 or PL14);
            */


            MakeTestStepHeader(5, UniqueIdentifier++, "Stop the train",
                "Verify the following information(1)   Use the log file to confirm that DMI received packet information MMI_TRACK_CONDITIONS (EVC-32) and MMI_ETCS_MISC_OUT_SIGNALS (EVC-7) with the following variables,MMI_M_TRACkCOND_TYPE = 7MMI_Q_TRACKCOND_STEP = 0MMI_Q_TRACKCOND_ACTION_START = 1 (PL13) or 0 (PL14)MMI_O_TRACKCOND_ANNOUNCE - OBU_TR_O_TRAIN (EVC-7)   =  Remaining distance from PL13 or PL14 symbol on area D2 to the first distance scale line (zero line)(2)    The bottom of PL13 or PL14 symbol is displayed with the correct position in the PA distance scale refer to the result of calculation from expected result (1)");
            /*
            Test Step 5
            Action: Stop the train
            Expected Result: Verify the following information(1)   Use the log file to confirm that DMI received packet information MMI_TRACK_CONDITIONS (EVC-32) and MMI_ETCS_MISC_OUT_SIGNALS (EVC-7) with the following variables,MMI_M_TRACkCOND_TYPE = 7MMI_Q_TRACKCOND_STEP = 0MMI_Q_TRACKCOND_ACTION_START = 1 (PL13) or 0 (PL14)MMI_O_TRACKCOND_ANNOUNCE - OBU_TR_O_TRAIN (EVC-7)   =  Remaining distance from PL13 or PL14 symbol on area D2 to the first distance scale line (zero line)(2)    The bottom of PL13 or PL14 symbol is displayed with the correct position in the PA distance scale refer to the result of calculation from expected result (1)
            Test Step Comment: (1) MMI_gen 9980 (partly:Table45(PL13 or PL14));MMI_gen 9979 (partly: ANNOUNCE); MMI_gen 636 (partly: ANNOUNCE); (2) MMI_gen 2604 (partly: bottom of the symbol, D2);
            */
            // Call generic Action Method
            DmiActions.Stop_the_train(this);


            MakeTestStepHeader(6, UniqueIdentifier++, "Drive the train forward with speed = 20 km/h",
                "The speed pointer is indicated as 20  km/h");
            /*
            Test Step 6
            Action: Drive the train forward with speed = 20 km/h
            Expected Result: The speed pointer is indicated as 20  km/h
            */
            // Call generic Action Method
            DmiActions.Drive_the_train_forward_with_speed_20_kmh(this);
            // Call generic Check Results Method
            DmiExpectedResults.The_speed_pointer_is_indicated_as_20_kmh(this);


            MakeTestStepHeader(7, UniqueIdentifier++,
                "Stop the train when the TC15 or TC16 symbol displays in sub-area B3",
                "Verify the following information(1)   DMI displays TC15 or TC16 symbol in sub-area B3. (TC15) or  (TC16)(2)   Use the log file to confirm that DMI received packet information MMI_TRACK_CONDITIONS (EVC-32) with the following variables,MMI_M_TRACkCOND_TYPE = 7MMI_Q_TRACKCOND_STEP = 1(TC15 or TC16) or 2 (TC15)MMI_Q_TRACKCOND_ACTION_START = 1 (TC15) or 0 (TC16)");
            /*
            Test Step 7
            Action: Stop the train when the TC15 or TC16 symbol displays in sub-area B3
            Expected Result: Verify the following information(1)   DMI displays TC15 or TC16 symbol in sub-area B3. (TC15) or  (TC16)(2)   Use the log file to confirm that DMI received packet information MMI_TRACK_CONDITIONS (EVC-32) with the following variables,MMI_M_TRACkCOND_TYPE = 7MMI_Q_TRACKCOND_STEP = 1(TC15 or TC16) or 2 (TC15)MMI_Q_TRACKCOND_ACTION_START = 1 (TC15) or 0 (TC16)
            Test Step Comment: (1) MMI_gen 10465 (partly: Table40(TC15 or TC16));(2) MMI_gen 662(partly: TC15 or TC16);
            */


            MakeTestStepHeader(8, UniqueIdentifier++, "Driver the train forward with speed = 40 km/h",
                "The speed pointer is indicated as 40  km/h");
            /*
            Test Step 8
            Action: Driver the train forward with speed = 40 km/h
            Expected Result: The speed pointer is indicated as 40  km/h
            */
            // Call generic Action Method
            DmiActions.Driver_the_train_forward_with_speed_40_kmh(this);
            // Call generic Check Results Method
            DmiExpectedResults.The_speed_pointer_is_indicated_as_40_kmh(this);


            MakeTestStepHeader(9, UniqueIdentifier++,
                "Stop the train when the track condition symbol has been removed from sub-area B3",
                "Verify the following information(1)   Use the log file to confirm that DMI received packet information MMI_TRACK_CONDITIONS (EVC-32) with the following variables,MMI_Q_TRACKCOND_STEP = 4MMI_NID_TRACKCOND = Same value with expected result No.2 of step 7");
            /*
            Test Step 9
            Action: Stop the train when the track condition symbol has been removed from sub-area B3
            Expected Result: Verify the following information(1)   Use the log file to confirm that DMI received packet information MMI_TRACK_CONDITIONS (EVC-32) with the following variables,MMI_Q_TRACKCOND_STEP = 4MMI_NID_TRACKCOND = Same value with expected result No.2 of step 7
            Test Step Comment: (1) MMI_gen 9965;
            */
            // Call generic Action Method
            DmiActions.Stop_the_train_when_the_track_condition_symbol_has_been_removed_from_sub_area_B3(this);
            // Call generic Check Results Method
            DmiExpectedResults
                .Verify_the_following_information1_Use_the_log_file_to_confirm_that_DMI_received_packet_information_MMI_TRACK_CONDITIONS_EVC_32_with_the_following_variables_MMI_Q_TRACKCOND_STEP_4MMI_NID_TRACKCOND_Same_value_with_expected_result_No_2_of_step_7(
                    this);


            TraceHeader("End of test");

            /*
            Test Step 10
            Action: End of test
            Expected Result: 
            */


            return GlobalTestResult;
        }
    }
}
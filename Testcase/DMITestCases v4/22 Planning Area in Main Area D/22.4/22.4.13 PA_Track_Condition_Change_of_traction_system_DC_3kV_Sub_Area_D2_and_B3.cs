using System;
using System.Collections.Generic;
using Testcase.Telegrams.EVCtoDMI;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 22.4.13 PA Track Condition: Change of traction system, DC 3kV Sub-Area D2 and B3
    /// TC-ID: 17.4.13
    /// 
    /// This test case is to verify PA Track Condition ”Change of traction system, DC 3kV” on Sub-Area D2 and B3. The track condition shall comply with [ERA] standard and [MMI-ETCS-gen].
    /// 
    /// Tested Requirements:
    /// MMI_gen 619 (partly: PL31 or PL32); MMI_gen 9980 (partly:Table45(PL31 or PL32)); MMI_gen 9979 (partly: ANNOUNCE); MMI_gen 662 (partly: TC29 or TC30); MMI_gen 10465 (partly:Table40(TC29 or TC30)); MMI_gen 9965; MMI_gen 636 (partly: ANNOUNCE); MMI_gen 2604 (partly: bottom of the symbol, D2);
    /// 
    /// Scenario:
    /// 1.Drive the train forward pass BG0 at position 10m.BG0: pkt 12, 21 and 27 (Entering FS) 
    /// 2.Drive the train forward pass BG1 at position 100m. Then,verify the display of PA track condition based on received packet EVC-32.BG1: pkt 68 (M_TRACKCOND = 13) (Change of traction system, DC 3kV)
    /// 3.Verify the Track condition symbol in sub-Area D2 and B3
    /// 
    /// Used files:
    /// 17_4_13.tdg
    /// </summary>
    public class TC_ID_17_4_13_PA_Track_Condition_Change_of_traction_system_DC_3kV_Sub_Area_D2_and_B3 : TestcaseBase
    {
        public override bool TestcaseEntryPoint()
        {
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 23635;

            StartUp();
            DmiActions.Complete_SoM_L1_SR(this);

            // Testcase entrypoint
            TraceInfo("This test case requires an ATP configuration change - " +
                      "See Precondition requirements. If this is not done manually, the test may fail!");

            MakeTestStepHeader(1, UniqueIdentifier++, "Drive the train forward with speed = 20 km/h",
                "The speed pointer is indicated as 20  km/h");
            /*
            Test Step 1
            Action: Drive the train forward with speed = 20 km/h
            Expected Result: The speed pointer is indicated as 20  km/h
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 20;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays a speed of 20 km/h.");


            MakeTestStepHeader(2, UniqueIdentifier++,
                "Drive the train forward pass BG0 with MA and Track descriptionPkt 12,21 and 27",
                "Mode changes to FS mode , L1");
            /*
            Test Step 2
            Action: Drive the train forward pass BG0 with MA and Track descriptionPkt 12,21 and 27
            Expected Result: Mode changes to FS mode , L1
            */
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.FullSupervision;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in FS mode, Level 1.");

            MakeTestStepHeader(3, UniqueIdentifier++,
                "Pass BG1 with Track conditionPkt 68:D_TRACKCOND = 500L_TRACKCOND = 200M_TRACKCOND = 8(Switch off magnetic shoe brake)",
                "Mode remians in FS mode");
            /*
            Test Step 3
            Action: Pass BG1 with Track conditionPkt 68:D_TRACKCOND = 500L_TRACKCOND = 200M_TRACKCOND = 8(Switch off magnetic shoe brake)
            Expected Result: Mode remians in FS mode
            */

            MakeTestStepHeader(4, UniqueIdentifier++,
                "Enter Anouncement of Track condition “Change of traction system, DC 3 kV”",
                "Verify the following information(1)   DMI displays PL31 or PL32 symbol in sub-area D2. (PL31) or  (PL32)");
            /*
            Test Step 4
            Action: Enter Anouncement of Track condition “Change of traction system, DC 3 kV”
            Expected Result: Verify the following information(1)   DMI displays PL31 or PL32 symbol in sub-area D2. (PL31) or  (PL32)
            Test Step Comment: (1) MMI_gen 619(partly: PL31 or PL32);
            */
            EVC32_MMITrackConditions.MMI_Q_TRACKCOND_UPDATE = 0;
            TrackCondition trackCondition = new TrackCondition
            {
                MMI_O_TRACKCOND_ANNOUNCE = 30000,
                MMI_O_TRACKCOND_START = 0,
                MMI_O_TRACKCOND_END = 0,
                MMI_NID_TRACKCOND = 0,
                MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Change_traction_DC_3_kV,
                MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.AnnounceArea,
                MMI_Q_TRACKCOND_ACTION_START = Variables.MMI_Q_TRACKCOND_ACTION.WithoutDriverAction,
                MMI_Q_TRACKCOND_ACTION_END = 0
            };

            EVC32_MMITrackConditions.TrackConditions = new List<TrackCondition> {trackCondition};
            EVC32_MMITrackConditions.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays symbol PL31 in sub-area D2.");

            MakeTestStepHeader(5, UniqueIdentifier++, "Stop the train",
                "Verify the following information(1)   Use the log file to confirm that DMI received packet information MMI_TRACK_CONDITIONS (EVC-32) and MMI_ETCS_MISC_OUT_SIGNALS (EVC-7) with the following variables,MMI_M_TRACkCOND_TYPE = 13MMI_Q_TRACKCOND_STEP = 0MMI_Q_TRACKCOND_ACTION_START = 1 (PL31) or 0 (PL32)MMI_O_TRACKCOND_ANNOUNCE - OBU_TR_O_TRAIN (EVC-7)   =  Remaining distance from PL31 or PL32 symbol on area D2 to the first distance scale line (zero line)(2)    The bottom of PL31 or PL32 symbol is displayed with the correct position in the PA distance scale refer to the result of calculation from expected result (1)");
            /*
            Test Step 5
            Action: Stop the train
            Expected Result: Verify the following information(1)   Use the log file to confirm that DMI received packet information MMI_TRACK_CONDITIONS (EVC-32) and MMI_ETCS_MISC_OUT_SIGNALS (EVC-7) with the following variables,MMI_M_TRACkCOND_TYPE = 13MMI_Q_TRACKCOND_STEP = 0MMI_Q_TRACKCOND_ACTION_START = 1 (PL31) or 0 (PL32)MMI_O_TRACKCOND_ANNOUNCE - OBU_TR_O_TRAIN (EVC-7)   =  Remaining distance from PL31 or PL32 symbol on area D2 to the first distance scale line (zero line)(2)    The bottom of PL31 or PL32 symbol is displayed with the correct position in the PA distance scale refer to the result of calculation from expected result (1)
            Test Step Comment: (1) MMI_gen 9980 (partly:Table45(PL31 or PL32));MMI_gen 9979 (partly: ANNOUNCE); MMI_gen 636 (partly: ANNOUNCE); (2) MMI_gen 2604 (partly: bottom of the symbol, D2);
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 0;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN = 11000;
            // In diagram first scale line is at 125, bottom of symbol at ~60 (m)
            // O_TRACKCOND_ANNOUNCE - MMI_OBU_TR_O_TRAIN should == 6000

            trackCondition.MMI_O_TRACKCOND_ANNOUNCE = 17000;
            EVC32_MMITrackConditions.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The bottom of the PL31 symbol is displayed at ~60.");

            MakeTestStepHeader(6, UniqueIdentifier++, "Drive the train forward with speed = 20 km/h",
                "The speed pointer is indicated as 20  km/h");
            /*
            Test Step 6
            Action: Drive the train forward with speed = 20 km/h
            Expected Result: The speed pointer is indicated as 20  km/h
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 20;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The speed pointer is displayed with speed = 20 km/h.");

            MakeTestStepHeader(7, UniqueIdentifier++,
                "Stop the train when the TC29 or TC30 symbol displays in sub-area B3",
                "Verify the following information(1)   DMI displays TC29 or TC30 symbol in sub-area B3. (TC29) or  (TC30)(2)   Use the log file to confirm that DMI received packet information MMI_TRACK_CONDITIONS (EVC-32) with the following variables,MMI_M_TRACkCOND_TYPE = 13MMI_Q_TRACKCOND_STEP = 1(TC29 or TC30) or 2 (TC29)MMI_Q_TRACKCOND_ACTION_START = 1 (TC29) or 0 (TC30)");
            /*
            Test Step 7
            Action: Stop the train when the TC29 or TC30 symbol displays in sub-area B3
            Expected Result: Verify the following information(1)   DMI displays TC29 or TC30 symbol in sub-area B3. (TC29) or  (TC30)(2)   Use the log file to confirm that DMI received packet information MMI_TRACK_CONDITIONS (EVC-32) with the following variables,MMI_M_TRACkCOND_TYPE = 13MMI_Q_TRACKCOND_STEP = 1(TC29 or TC30) or 2 (TC29)MMI_Q_TRACKCOND_ACTION_START = 1 (TC29) or 0 (TC30)
            Test Step Comment: (1) MMI_gen 10465  (partly:Table40(TC29 or TC30));(2) MMI_gen 662 (partly: TC29 or TC30);
            */
            // Remove current track condition?
            trackCondition.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.RemoveTC;
            EVC32_MMITrackConditions.Send();

            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 0;
            trackCondition.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.InsideArea_Active;
            EVC32_MMITrackConditions.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays symbol TC29 in sub-area B3.");

            MakeTestStepHeader(8, UniqueIdentifier++, "Drive the train forward with speed = 20 km/h",
                "The speed pointer is indicated as 20  km/h");
            /*
            Test Step 8
            Action: Drive the train forward with speed = 20 km/h
            Expected Result: The speed pointer is indicated as 20  km/h
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 20;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The speed pointer is displayed with speed = 20 km/h.");

            MakeTestStepHeader(9, UniqueIdentifier++, "Stop the train when the track condition symbol has been removed",
                "Verify the following information(1)   Use the log file to confirm that DMI received packet information MMI_TRACK_CONDITIONS (EVC-32) with the following variables,MMI_Q_TRACKCOND_STEP = 4MMI_NID_TRACKCOND = Same value with expected result No.2 of step 7");
            /*
            Test Step 9
            Action: Stop the train when the track condition symbol has been removed
            Expected Result: Verify the following information(1)   Use the log file to confirm that DMI received packet information MMI_TRACK_CONDITIONS (EVC-32) with the following variables,MMI_Q_TRACKCOND_STEP = 4MMI_NID_TRACKCOND = Same value with expected result No.2 of step 7
            Test Step Comment: (1) MMI_gen 9965;
            */
            trackCondition.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.RemoveTC;
            EVC32_MMITrackConditions.Send();

            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 0;

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
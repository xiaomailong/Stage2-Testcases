using System;
using System.Collections.Generic;
using Testcase.Telegrams.EVCtoDMI;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 22.4.1 PA Track Condition: Non stopping area in Sub-Area D2 and B3
    /// TC-ID: 17.4.1
    /// 
    /// This test case is to verify PA Track Condition ”Non stopping area” on Sub-Area D2, and also B3,. The track condition shall comply with [ERA] standard and [MMI-ETCS-gen].
    /// 
    /// Tested Requirements:
    /// MMI_gen 619 (partly: PL09); MMI_gen 9980 (partly: Table 45(PL09)); MMI_gen 9979 (partly: ANNOUNCE); MMI_gen 662 (partly: TC10 or TC11); MMI_gen 10465 (partly: Table 40(TC10 or TC11)); MMI_gen 9965; MMI_gen 636 (partly: ANNOUNCE); MMI_gen 2604 (partly: bottom of the symbol, D2);
    /// 
    /// Scenario:
    /// 1.Drive the train forward pass BG0 at position 10m.BG0: pkt 12, 21 and 27 (Entering FS)
    /// 2.Drive the train forward pass BG1 at position 100m. Then,verify the display of PA track condition based on received packet EVC-32.BG1: pkt 68 (D_TRACKCOND = 200, L_TRACKCOND = 200, M_TRACKCOND = 0) (Track condition: Non stopping area)
    /// 3.Verify the Track condition symbol in sub-Area D2 and B3
    /// 
    /// Used files:
    /// 17_4_1.tdg
    /// </summary>
    public class TC_ID_17_4_1_PA_Track_Condition_Non_stopping_area_in_Sub_Area_D2_and_B3 : TestcaseBase
    {
        public override bool TestcaseEntryPoint()
        {
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 23382;
            // Testcase entrypoint
            TraceInfo("This test case requires an ATP configuration change - " +
                      "See Precondition requirements. If this is not done manually, the test may fail!");

            StartUp();
            DmiActions.Complete_SoM_L1_FS(this);

            MakeTestStepHeader(1, UniqueIdentifier++, "Drive the train forward with speed = 20 km/h",
                "The speed pointer is indicated as 20 km/h");
            /*
            Test Step 1
            Action: Drive the train forward with speed = 20 km/h
            Expected Result: The speed pointer is indicated as 20  km/h
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 20;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The speed pointer is displayed with speed = 20 km/h.");

            MakeTestStepHeader(2, UniqueIdentifier++,
                "Drive the train forward pass BG0 with MA and Track description Pkt 12, 21 and 27",
                "Mode changes to FS mode , level 1");
            /*
            Test Step 2
            Action: Drive the train forward pass BG0 with MA and Track descriptionPkt 12,21 and 27
            Expected Result: Mode changes to FS mode , level 1
            */

            MakeTestStepHeader(3, UniqueIdentifier++,
                "Continue to drive the train forward pass BG1 with Track conditionPkt 68:D_TRACKCOND = 200L_TRACKCOND = 200M_TRACKCOND = 0(Non stopping area)",
                "Mode remains in FS mode");
            /*
            Test Step 3
            Action: Continue to drive the train forward pass BG1 with Track conditionPkt 68: D_TRACKCOND = 200 L_TRACKCOND = 200 M_TRACKCOND = 0 (Non stopping area)
            Expected Result: Mode remains in FS mode
            */

            MakeTestStepHeader(4, UniqueIdentifier++, "Enter Anouncement of Track condition “Non stopping area”",
                "Verify the following information(1)  DMI displays PL09 symbol in sub-area D2");
            /*
            Test Step 4
            Action: Enter Anouncement of Track condition “Non stopping area”
            Expected Result: Verify the following information(1)   DMI displays PL09 symbol in sub-area D2
            Test Step Comment: (1) MMI_gen 619(Partly: PL09);
            */
            EVC1_MMIDynamic.MMI_O_BRAKETARGET_M = 5000;

            EVC4_MMITrackDescription.MMI_G_GRADIENT_CURR = 0;
            EVC4_MMITrackDescription.MMI_V_MRSP_CURR_KMH = 100;
            EVC4_MMITrackDescription.Send();

            EVC32_MMITrackConditions.MMI_Q_TRACKCOND_UPDATE = 0;
            TrackCondition trackCondition = new TrackCondition
            {
                MMI_O_TRACKCOND_ANNOUNCE = -2147483647,
                MMI_O_TRACKCOND_START = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN + 20000,
                MMI_O_TRACKCOND_END = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN + 40000,
                MMI_NID_TRACKCOND = 0,
                MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Non_Stopping_Area,
                MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.AnnounceArea,
                MMI_Q_TRACKCOND_ACTION_START = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction,
                MMI_Q_TRACKCOND_ACTION_END = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction
            };

            EVC32_MMITrackConditions.TrackConditions = new List<TrackCondition> {trackCondition};
            EVC32_MMITrackConditions.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays symbol PL09 in sub-area D2.");

            MakeTestStepHeader(5, UniqueIdentifier++, "Stop the train",
                "Verify the following information(1)    Use the log file to confirm that DMI recieved packet information MMI_DRIVER_MESSAGE_ACK (EVC-32) and MMI_ETCS_MISC_OUT_SIGNALS (EVC-7) with the following variables,MMI_M_TRACkCOND_TYPE = 0MMI_Q_TRACKCOND_STEP = 0MMI_O_TRACKCOND_ANNOUNCE - OBU_TR_O_TRAIN (EVC-7) =  Remaining distance from PL09 symbol on area D2 to the first distance scale line (zero line)(2)    The bottom of PL09 symbol is displayed with the correct position in the PA distance scale refer to the result of calculation from expected result (1)");
            /*
            Test Step 5
            Action: Stop the train
            Expected Result: Verify the following information(1)    Use the log file to confirm that DMI recieved packet information MMI_DRIVER_MESSAGE_ACK (EVC-32) and MMI_ETCS_MISC_OUT_SIGNALS (EVC-7) with the following variables, MMI_M_TRACkCOND_TYPE = 0 MMI_Q_TRACKCOND_STEP = 0 MMI_O_TRACKCOND_ANNOUNCE - OBU_TR_O_TRAIN (EVC-7) =  Remaining distance from PL09 symbol on area D2 to the first distance scale line (zero line)(2)    The bottom of PL09 symbol is displayed with the correct position in the PA distance scale refer to the result of calculation from expected result (1)
            Test Step Comment: (1) MMI_gen 9980 (partly: Table 45 (PL09));MMI_gen 9979 (partly: ANNOUNCE); MMI_gen 636 (partly: ANNOUNCE);(2) MMI_gen 2604 (partly: bottom of the symbol, D2);
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 0;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN = 11000;
            // In diagram first scale line is at 125, bottom of symbol at ~80 (m) == 12500 (cm): difference ~4500
            // O_TRACKCOND_ANNOUNCE - MMI_OBU_TR_O_TRAIN should == 4500
            trackCondition.MMI_O_TRACKCOND_ANNOUNCE = 15500;
            trackCondition.MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Non_Stopping_Area;
            trackCondition.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.ApproachingArea;
            EVC32_MMITrackConditions.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The bottom of the PL09 symbol is displayed at ~80.");

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
                "Stop the train when the TC10 or TC11 symbol displays in sub-area B3",
                "Verify the following information(1)   DMI displays TC10 or TC11 symbol in sub-area B3. (TC10) or (TC11)(2)   Use the log file to confirm that DMI recieved packet information MMI_DRIVER_MESSAGE_ACK (EVC-32) and MMI_ETCS_MISC_OUT_SIGNALS (EVC-7) with the following variables,MMI_M_TRACkCOND_TYPE = 0MMI_Q_TRACKCOND_STEP = 1MMI_Q_TRACKCOND_ACTION_START = 1 (TC10) or 0 (TC11)");
            /*
            Test Step 7
            Action: Stop the train when the TC10 or TC11 symbol displays in sub-area B3
            Expected Result: Verify the following information(1)   DMI displays TC10 or TC11 symbol in sub-area B3. (TC10) or (TC11)(2)   Use the log file to confirm that DMI recieved packet information MMI_DRIVER_MESSAGE_ACK (EVC-32) and MMI_ETCS_MISC_OUT_SIGNALS (EVC-7) with the following variables,MMI_M_TRACkCOND_TYPE = 0 MMI_Q_TRACKCOND_STEP = 1 MMI_Q_TRACKCOND_ACTION_START = 1 (TC10) or 0 (TC11)
            Test Step Comment: (1) MMI_gen 10465 (partly: Table 40(TC10 or TC11));(2) MMI_gen 662 (Partly: TC10 or TC11); 
            */
            // Remove current track condition?
            trackCondition.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.RemoveTC;
            EVC32_MMITrackConditions.Send();

            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 0;
            trackCondition.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.AnnounceArea;
            trackCondition.MMI_Q_TRACKCOND_ACTION_START = Variables.MMI_Q_TRACKCOND_ACTION.WithoutDriverAction;
            trackCondition.MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Non_Stopping_Area;
            EVC32_MMITrackConditions.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays symbol TC11 in sub-area B3.");

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

            MakeTestStepHeader(9, UniqueIdentifier++,
                "Stop the train when the track condition symbol has been removed from sub-area B3",
                "Verify the following information(1)   Use the log file to confirm that DMI received packet information MMI_TRACK_CONDITIONS (EVC-32) with the following variables,MMI_Q_TRACKCOND_STEP = 4MMI_NID_TRACKCOND = same value with expected result No.2 of step 7");
            /*
            Test Step 9
            Action: Stop the train when the track condition symbol has been removed from sub-area B3
            Expected Result: Verify the following information(1)   Use the log file to confirm that DMI received packet information MMI_TRACK_CONDITIONS (EVC-32) with the following variables,MMI_Q_TRACKCOND_STEP = 4MMI_NID_TRACKCOND = same value with expected result No.2 of step 7
            Test Step Comment: (1) MMI_gen 9965;
            */
            trackCondition.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.RemoveTC;
            EVC32_MMITrackConditions.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI removes symbol TC11 from sub-area B3.");

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
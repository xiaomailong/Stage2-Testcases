using System;
using System.Collections.Generic;
using Testcase.Telegrams.EVCtoDMI;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 22.4.3 PA Track Condition:  Lower Pantograph in Sub-Area D2 and B3
    /// TC-ID: 17.4.3
    /// 
    /// This test case is to verify PA Track Condition” Lower pantograph” in Sub-Area D2 and B3. 
    /// The track condition shall comply with [ERA] standard and [MMI-ETCS-gen].
    /// 
    /// Tested Requirements:
    /// MMI_gen 619 (partly: PL01 or PL02, PL03 or PL04); MMI_gen 9980 (partly: Table45(PL01 or PL02, PL03 or PL04)); MMI_gen 9979 (partly: START, END); MMI_gen 662 (partly: TC01, TC02 or TC03, TC04 or TC05); MMI_gen 10465 (partly:Table40(TC01, TC02 or TC03, TC04 or TC05)); MMI_gen 9965; MMI_gen 636 (partly: START, END); MMI_gen 2604 (partly: bottom of the symbol, D2);
    /// 
    /// Scenario:
    /// 1.Drive the train forward pass BG0 at position 10m.BG0: pkt 12, 21 and 27 (Entering FS) 
    /// 2.Drive the train forward pass BG1 at position 100m. Then,verify the display of PA track condition based on received packet EVC-32.BG1: pkt 68 (M_TRACKCOND = 3) (Lower pantograph)
    /// 3.Verify the Track condition symbol in sub-Area D2 and B3
    /// 
    /// Used files:
    /// 17_4_3.tdg
    /// </summary>
    public class TC_17_4_3_PA_Track_Condition_Lower_Pantograph_in_Sub_Area_D2_and_B3 : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // Configure atpcu configuration file as following:
            // TC_T_Panto_Down = 100
            // TC_T_MainSwitch_Off = 100
            // TC_T_Airtight_Close =100
            // TC_T_Inhib_RBBrake = 100
            // TC_T_ Inhib_ECBrake = 100
            // TC_T_ Inhib_MSBrake = 100
            // TC_T_Change_TractionSyst = 100
            // TC_T_Allowed_CurrentConsump = 100
            // TC_T_StationPlatform = 100

            // Call the TestCaseBase PreExecution
            base.PreExecution();

            // Test system is power on.SoM is performed in SR mode, level 1.
            DmiActions.Complete_SoM_L1_SR(this);
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays FS mode, level 1

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint
            TraceInfo("This test case requires an ATP configuration change - " +
                      "See Precondition requirements. If this is not done manually, the test may fail!");

            /*
            Test Step 1
            Action: Drive the train forward with speed = 20 km/h
            Expected Result: The speed pointer is indicated as 20  km/h
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 20;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The speed pointer is displayed with speed = 20 km/h.");


            /*
            Test Step 2
            Action: Drive the train forward pass BG0 with MA and Track description
            Pkt 12,21 and 27
            Expected Result: Mode changes to FS mode , L1
            */
            DmiActions.Send_FS_Mode(this);
            DmiExpectedResults.FS_mode_displayed(this);

            /*
            Test Step 3
            Action: Continue to drive the train forward pass BG1 with Track condition 
            Pkt 68:
            D_TRACKCOND = 200
            L_TRACKCOND = 200
            M_TRACKCOND = 3(Lower pantograph)
            Expected Result: Mode remins in FS mode
            */

            DmiExpectedResults.FS_mode_displayed(this);

            /*
            Test Step 4
            Action: Enter Anouncement of Track condition “Non stopping area: Lower Pantograph”
            Expected Result: Verify the following information
            (1)   DMI displays PL01 or PL02 symbol in sub-area D2.(PL01) or (PL02)
            Test Step Comment: (1) MMI_gen 619(partly: PL01 or PL02);
            */
            TrackCondition trackCondition = new TrackCondition
            {
                MMI_O_TRACKCOND_ANNOUNCE = 30000,
                MMI_O_TRACKCOND_START = 0,
                MMI_O_TRACKCOND_END = 0,
                MMI_NID_TRACKCOND = 0,
                MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Pantograph,
                MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.InsideArea_Active,
                MMI_Q_TRACKCOND_ACTION_START = Variables.MMI_Q_TRACKCOND_ACTION.WithoutDriverAction,
                MMI_Q_TRACKCOND_ACTION_END = 0
            };
            EVC32_MMITrackConditions.TrackConditions = new List<TrackCondition> {{trackCondition}};
            EVC32_MMITrackConditions.Send();

            DmiExpectedResults.Driver_symbol_displayed(this, "Lower Pantograph", "PL01", "D2", false);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays symbol PL01 in sub-area D2.");

            /*
            Test Step 5
            Action: Stop the train
            Expected Result: Verify the following information
            (1)   Use the log file to confirm that DMI recieved packet information 
            MMI_DRIVER_MESSAGE_ACK (EVC-32) and MMI_ETCS_MISC_OUT_SIGNALS (EVC-7) with the following variables,
            MMI_M_TRACkCOND_TYPE = 3
            MMI_Q_TRACKCOND_STEP = 0 or 1 (PL01 or PL02)
            MMI_Q_TRACKCOND_ACTION_START = 1 (PL01) or 0 (PL02)
            MMI_O_TRACKCOND_START - OBU_TR_O_TRAIN (EVC-7)   =  Remaining distance from PL01 or PL02 symbol in sub-area D2 to the first distance scale line (zero line)
            (2)    The bottom of PL01 or PL02 symbol is displayed with the correct position in the PA distance scale refer to the result of calculation from expected result (1)
            Test Step Comment: (1) MMI_gen 9980 (partly:Table45(PL01 or PL02));MMI_gen 9979 (partly: START); MMI_gen 636 (partly: START); (2) MMI_gen 2604 (partly: bottom of the symbol, D2);
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 0;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN = 11000;
            // DMI_RS_ETCS says type == 3 (Panto), step == 2, announce = 1, action == 1 to display PL01 step == 3 (Leave area), action == 0 to display PL02
            // In diagram first scale line is at 125, bottom of symbol at ~120 (m): difference ~500 (cm)
            // O_TRACKCOND_START - MMI_OBU_TR_O_TRAIN should == 500
            trackCondition.MMI_Q_TRACKCOND_ACTION_START = Variables.MMI_Q_TRACKCOND_ACTION.WithoutDriverAction;
            trackCondition.MMI_O_TRACKCOND_START = 11500;
            trackCondition.MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Pantograph;
            trackCondition.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.InsideArea_Active;
            EVC32_MMITrackConditions.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The bottom of the PL01 symbol is displayed at ~120.");

            /*
            Test Step 6
            Action: Drive the train forward with speed = 20 km/h
            Expected Result: The speed pointer is indicated as 20  km/h
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 20;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The speed pointer is displayed with speed = 20 km/h.");

            /*
            Test Step 7
            Action: Stop the train when the TC01, TC02 or TC03 symbol displays in sub-area B3
            Expected Result: Verify the following informationDMI displays TC01, TC02 or TC03 symbol in sub-area B3.
            (TC01) or (TC02) or (TC03)
            Use the log file to confirm that DMI recieved packet information 
            MMI_DRIVER_MESSAGE_ACK (EVC-32) and MMI_ETCS_MISC_OUT_SIGNALS (EVC-7) with the following variables,
            MMI_M_TRACkCOND_TYPE = 3
            MMI_Q_TRACKCOND_STEP = 1 (TC02 or TC03) or 2(TC01)
            MMI_Q_TRACKCOND_ACTION_START = 0 (TC03) or 1(TC02 or TC01)
            Test Step Comment: (1) MMI_gen 10465 (partly:Table40(TC01, TC02 or TC03));(2) MMI_gen 662 (partly: TC01, TC02 or TC03);
            */
            // Remove current track condition?
            trackCondition.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.RemoveTC;
            EVC32_MMITrackConditions.Send();

            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 0;
            trackCondition.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.InsideArea_Active;
            trackCondition.MMI_Q_TRACKCOND_ACTION_START = Variables.MMI_Q_TRACKCOND_ACTION.WithoutDriverAction;
            trackCondition.MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Pantograph;
            EVC32_MMITrackConditions.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays symbol TC01 in sub-area B3.");

            /*
            Test Step 8
            Action: Drive the train forward with speed = 20 km/h
            Expected Result: The speed pointer is indicated as 20  km/h
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 20;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The speed pointer is displayed with speed = 20 km/h.");

            /*
            Test Step 9
            Action: Enter Track condition “Non stopping area: Raise Pantograph”
            Expected Result: Verify the following information
            (1)   DMI displays PL03 or PL04 symbol in sub-area D2.(PL03) or (PL04)
            Test Step Comment: (1) MMI_gen 619(partly: PL03 or PL04);
            */
            trackCondition.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.LeaveArea;
            trackCondition.MMI_Q_TRACKCOND_ACTION_END = Variables.MMI_Q_TRACKCOND_ACTION.WithoutDriverAction;
            trackCondition.MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Pantograph;
            EVC32_MMITrackConditions.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays symbol PL03 in sub-area D2.");

            /*
            Test Step 10
            Action: Stop the train
            Expected Result: Verify the following information
            (1)   Use the log file to confirm that DMI recieved packet information 
            MMI_DRIVER_MESSAGE_ACK (EVC-32) and MMI_ETCS_MISC_OUT_SIGNALS (EVC-7) with the following variables,
            MMI_M_TRACkCOND_TYPE = 3
            MMI_Q_TRACKCOND_STEP = 0 or 1 or 2 (PL03 or PL04)
            MMI_Q_TRACKCOND_ACTION_START = 1 (PL03) or 0 (PL04)
            MMI_O_TRACKCOND_END - OBU_TR_O_TRAIN (EVC-7)   =  Remaining distance from PL03 or PL04 symbol in sub-area D2 to the first distance scale line (zero line)
            (2)     The bottom of PL03 or PL04 symbol is displayed with the correct position in the PA distance scale refer to the result of calculation from expected result (1)
            Test Step Comment: (1) MMI_gen 9980 (partly:Table45(PL03 or PL04));MMI_gen 9979 (partly: END); MMI_gen 636 (partly: END); (2) MMI_gen 2604 (partly: bottom of the symbol, D2);
            */
            // Spec does not agree with DMI_RS_ETCS doc. on values
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 0;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN = 13000;
            trackCondition.MMI_O_TRACKCOND_END = 13500;
            EVC32_MMITrackConditions.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The bottom of the PL03 symbol is displayed at ~100.");

            /*
            Test Step 11
            Action: Drive the train forward with speed = 20 km/h
            Expected Result: The speed pointer is indicated as 20  km/h
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 20;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The speed pointer is displayed with speed = 20 km/h.");

            /*
            Test Step 12
            Action: Stop the train when the TC04 or TC05 symbol displays in sub-area B3
            Expected Result: Verify the following information
            (1)   DMI displays TC04 or TC05 symbol in sub-area B3. (TC04) or  (TC05)
            (2)   Use the log file to confirm that DMI recieved packet information 
            MMI_DRIVER_MESSAGE_ACK (EVC-32) and MMI_ETCS_MISC_OUT_SIGNALS (EVC-7) with the following variables,
            MMI_M_TRACkCOND_TYPE = 3
            MMI_Q_TRACKCOND_STEP = 3
            MMI_Q_TRACKCOND_ACTION_END = 0 (TC05) or 1(TC04)
            Test Step Comment: (1) MMI_gen 10465 (partly:Table40(TC04 or TC05));(2) MMI_gen 662 (partly: TC04 or TC05);
            */
            trackCondition.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.RemoveTC;
            EVC32_MMITrackConditions.Send();

            trackCondition.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.LeaveArea;
            trackCondition.MMI_Q_TRACKCOND_ACTION_END = Variables.MMI_Q_TRACKCOND_ACTION.WithoutDriverAction;
            trackCondition.MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Pantograph;
            EVC32_MMITrackConditions.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays symbol TC04 in sub-area B3.");

            /*
            Test Step 13
            Action: Drive the train forward with speed = 20 km/h
            Expected Result: The speed pointer is indicated as 20  km/h
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 20;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The speed pointer is displayed with speed = 20 km/h.");

            /*
            Test Step 14
            Action: Stop the train when the track condition symbol has been removed from sub-area B3
            Expected Result: Verify the following information
            (1)   Use the log file to confirm that DMI received packet information MMI_TRACK_CONDITIONS (EVC-32) with the following variables,
            MMI_Q_TRACKCOND_STEP = 4
            MMI_NID_TRACKCOND = Same value with expected result No.2 of step 12
            Test Step Comment: (1) MMI_gen 9965;
            */
            trackCondition.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.RemoveTC;
            EVC32_MMITrackConditions.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI removes symbol TC04 from sub-area B3.");

            /*
            Test Step 15
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
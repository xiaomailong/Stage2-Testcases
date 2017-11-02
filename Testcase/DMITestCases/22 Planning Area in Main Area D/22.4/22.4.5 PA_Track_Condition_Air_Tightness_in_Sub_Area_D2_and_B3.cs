using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BT_Tools;
using BT_CSB_Tools;
using BT_CSB_Tools.Logging;
using BT_CSB_Tools.Utils.Xml;
using BT_CSB_Tools.SignalPoolGenerator.Signals;
using BT_CSB_Tools.SignalPoolGenerator.Signals.MwtSignal;
using BT_CSB_Tools.SignalPoolGenerator.Signals.MwtSignal.Misc;
using BT_CSB_Tools.SignalPoolGenerator.Signals.PdSignal;
using BT_CSB_Tools.SignalPoolGenerator.Signals.PdSignal.Misc;
using CL345;
using Testcase.Telegrams.EVCtoDMI;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 22.4.5 PA Track Condition: Air Tightness in Sub-Area D2 and B3
    /// TC-ID: 17.4.5
    /// 
    /// This test case is to verify PA Track Condition” Air Tightness” in Sub-Area D2 and B3. The track condition shall comply with [ERA] standard and [MMI-ETCS-gen].
    /// 
    /// Tested Requirements:
    /// MMI_gen 619 (partly: PL17 or PL19, PL18 or PL20); MMI_gen 9980 (partly:Table45(PL17 or PL19, PL18 or PL20)); MMI_gen 9979(partly: ANNOUNCE, END); MMI_gen 662 (partly: TC19 or TC21, TC20 or TC22);MMI_gen 10465 (partly:Table40(TC19 or TC21, TC20 or TC22)); MMI_gen 9965; MMI_gen 636 (partly: ANNOUNCE, END); MMI_gen 2604 (partly: bottom of the symbol, D2);
    /// 
    /// Scenario:
    /// 1.Drive the train forward pass BG0 at position 10m.BG0: pkt 12, 21 and 27 (Entering FS) 
    /// 2.Drive the train forward pass BG1 at position 100m. Then,verify the display of PA track condition based on received packet EVC-32.BG1: pkt 68 (M_TRACKCOND = 5) (Air tightness)
    /// 3.Verify the Track condition symbol in sub-Area D2 and B3
    /// 
    /// Used files:
    /// 17_4_5.tdg
    /// </summary>
    public class TC_ID_17_4_5_PA_Track_Condition_Air_Tightness_in_Sub_Area_D2_and_B3 : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // Configure atpcu configuration file as following:
            // 
            // T_Panto_Down = 100        
            // T_MainSwitch_Off = 100
            // T_Airtight_Close =100
            // T_Inhib_RBBrake = 100
            // T_ Inhib_ECBrake = 100
            // T_ Inhib_MSBrake = 100
            // T_Change_TractionSyst = 100
            // T_Allowed_CurrentConsump = 100 
            // T_StationPlatform = 100

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
            Action: Drive the train forward pass BG0 with MA and Track descriptionPkt 12,21 and 27
            Expected Result: Mode changes to FS mode , L1
            */
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.FullSupervision;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in FS mode, Level 1.");

            /*
            Test Step 3
            Action: Continue to drive the train forward pass BG1 with Track condition Pkt 68:D_TRACKCOND = 400L_TRACKCOND = 500M_TRACKCOND = 5(Air tightness)
            Expected Result: Mode remins in FS mode
            */

            /*
            Test Step 4
            Action: Enter Anouncement of Track condition “Air tightness”
            Expected Result: Verify the following information(1)   DMI displays PL17 or PL19 symbol in sub-area D2. PL17 or PL19
            Test Step Comment: (1) MMI_gen 619(partly: PL17 or PL19);
            */
            EVC32_MMITrackConditions.MMI_Q_TRACKCOND_UPDATE = 0;
            TrackCondition trackCondition = new TrackCondition
            {
                MMI_O_TRACKCOND_ANNOUNCE = 30000,
                MMI_O_TRACKCOND_START = 0,
                MMI_O_TRACKCOND_END = 0,
                MMI_NID_TRACKCOND = 0,
                MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Air_tightness,
                MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.AnnounceArea,
                MMI_Q_TRACKCOND_ACTION_START = Variables.MMI_Q_TRACKCOND_ACTION.WithoutDriverAction,
                MMI_Q_TRACKCOND_ACTION_END = 0
            };

            EVC32_MMITrackConditions.TrackConditions = new List<TrackCondition> { { trackCondition } };
            EVC32_MMITrackConditions.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays symbol PL17 in sub-area D2.");

            /*
            Test Step 5
            Action: Stop the train
            Expected Result: Verify the following information(1)   Use the log file to confirm that DMI received packet information MMI_TRACK_CONDITIONS (EVC-32) and MMI_ETCS_MISC_OUT_SIGNALS (EVC-7) with the following variables,MMI_M_TRACkCOND_TYPE = 5MMI_Q_TRACKCOND_STEP = 0 MMI_Q_TRACKCOND_ACTION_START = 1 (PL17) or 0 (PL19)MMI_O_TRACKCOND_ANNCOUNCE - OBU_TR_O_TRAIN (EVC-7) =  Remaining distance from PL17 or PL19 symbol in sub-area D2 to the first distance scale line (zero line)(2)    The bottom of PL17 or PL19 symbol is displayed with the correct position in the PA distance scale refer to the result of calculation from expected result (1)
            Test Step Comment: (1) MMI_gen 9980 (partly:Table45( PL17 or PL19));MMI_gen 9979(partly: ANNOUNCE); MMI_gen 636 (partly: ANNOUNCE); (2) MMI_gen 2604 (partly: bottom of the symbol, D2);
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 0;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN = 11000;
            // In diagram first scale line is at 125, bottom of symbol at ~2 (m): difference ~12300 (cm)
            // O_TRACKCOND_ANNOUNCE - MMI_OBU_TR_O_TRAIN should == 4500

            // Test spec says _STEP is 0, Table 45 says PL17 is _TYPE 5 (Air-tightness), _STEP 1, _ACTION_START 1
            trackCondition.MMI_O_TRACKCOND_ANNOUNCE = 23300;
            EVC22_MMICurrentRBC.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The bottom of the PL17 symbol is displayed at ~2.");

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
            Action: Stop the train when the TC19 or TC21 symbol displays in sub-area B3
            Expected Result: Verify the following information(1)   DMI displays TC19 or TC21 symbol in sub-area B3. (TC19) or  (TC21)(2)   Use the log file to confirm that DMI received packet information MMI_TRACK_CONDITIONS (EVC-32) with the following variables,MMI_M_TRACkCOND_TYPE = 5MMI_Q_TRACKCOND_STEP = 1 (TC19 or TC21) or 2(TC19)MMI_Q_TRACKCOND_ACTION_START = 0 (TC21) or 1 (TC19)
            Test Step Comment: (1) MMI_gen 10465 (partly:Table40(TC19 or TC21));(2) MMI_gen 662(partly: TC19 or TC21);
            */
            // Remove current track condition?
            trackCondition.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.RemoveTC;
            EVC32_MMITrackConditions.Send();

            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 0;
            trackCondition.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.AnnounceArea;
            trackCondition.MMI_Q_TRACKCOND_ACTION_START = Variables.MMI_Q_TRACKCOND_ACTION.WithoutDriverAction;
            EVC32_MMITrackConditions.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays symbol TC19 in sub-area B3.");

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
            Action: Enter Track condition “Air tightness”
            Expected Result: Verify the following information(1)   DMI displays PL03 or PL04 symbol in sub-area D2. (PL18) or  (PL20)
            Test Step Comment: (1) MMI_gen 619(partly: PL18 or PL20);
            */
            // Spec says PL03/04 which are pantograph signs -> PL18/20 are open air conditioning symbols
            trackCondition.MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Air_tightness;
            trackCondition.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.LeaveArea;
            trackCondition.MMI_Q_TRACKCOND_ACTION_END = Variables.MMI_Q_TRACKCOND_ACTION.WithoutDriverAction;
            EVC32_MMITrackConditions.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays symbol PL18 in sub-area D2.");

            /*
            Test Step 10
            Action: Stop the train
            Expected Result: Verify the following information(1)   Use the log file to confirm that DMI received packet information MMI_TRACK_CONDITIONS (EVC-32) and MMI_ETCS_MISC_OUT_SIGNALS (EVC-7) with the following variables,MMI_M_TRACkCOND_TYPE = 5MMI_Q_TRACKCOND_STEP = 0 or 1 or 2MMI_Q_TRACKCOND_ACTION_START = 1 (PL18) or 0 (PL20)MMI_O_TRACKCOND_END - OBU_TR_O_TRAIN (EVC-7)   =  Remaining distance from PL18 or PL20 symbol in sub-area D2 to the first distance scale line (zero line)(2)    The bottom of PL18 or PL20 symbol is displayed with the correct position in the PA distance scale refer to the result of calculation from expected result (1)
            Test Step Comment: (1) MMI_gen 9980 (partly: Table45(PL18 or PL20));MMI_gen 9979(partly: END); MMI_gen 636 (partly: END); (2) MMI_gen 2604 (partly: bottom of the symbol, D2);
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 0;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN = 55000;
            // 1st display line at 500m: symbol at this, MMI_O_TRACKCOND_END - (EVC7)OBU_TR_O_TRAIN => ~50000 (cm)
            trackCondition.MMI_O_TRACKCOND_END = 105000;
            EVC32_MMITrackConditions.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The bottom of the PL18 symbol is displayed at ~2.");

            /*
            Test Step 11
            Action: Driver the train forward with speed = 30km/h
            Expected Result: The speed pointer is indicated as 30  km/h
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 30;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The speed pointer is displayed with speed = 30 km/h.");

            /*
            Test Step 12
            Action: Stop the train when the TC20 or TC22 symbol displays in sub-area B3
            Expected Result: Verify the following information(1)   DMI displays TC20 or TC22 symbol in sub-area B3. (TC20) or  (TC22)(2)   Use the log file to confirm that DMI received packet information MMI_TRACK_CONDITIONS (EVC-32) with the following variables,MMI_M_TRACkCOND_TYPE = 5MMI_Q_TRACKCOND_STEP = 3MMI_Q_TRACKCOND_ACTION_END = 0 (TC22) or 1(TC20)
            Test Step Comment: (1) MMI_gen 10465 (partly:Table40(TC20 or TC22));(2) MMI_gen 662(partly: TC20 or TC22);
            */
            trackCondition.MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Air_tightness;
            trackCondition.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.LeaveArea;
            trackCondition.MMI_Q_TRACKCOND_ACTION_END = Variables.MMI_Q_TRACKCOND_ACTION.WithoutDriverAction;
            EVC32_MMITrackConditions.Send();

            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 0;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays symbol PL18 in sub-area B3.");


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
            Action: Stop the train when the track condition symbol has been removed
            Expected Result: Verify the following information(1)    Use the log file to confirm that DMI received packet information MMI_TRACK_CONDITIONS (EVC-32) with the following variables,MMI_Q_TRACKCOND_STEP = 4MMI_NID_TRACKCOND = Same value with expected result No.2 of step 12
            Test Step Comment: (1) MMI_gen 9965;
            */
            trackCondition.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.RemoveTC;
            EVC32_MMITrackConditions.Send();

            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 0;

            /*
            Test Step 15
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
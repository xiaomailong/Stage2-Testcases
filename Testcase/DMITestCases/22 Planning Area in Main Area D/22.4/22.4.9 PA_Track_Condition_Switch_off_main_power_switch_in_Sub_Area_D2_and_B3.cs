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
    /// 22.4.9 PA Track Condition: Switch off main power switch in Sub-Area D2 and B3
    /// TC-ID: 17.4.9
    /// 
    /// This test case is to verify PA Track Condition ”Switch off  main power switch” on Sub-Area D2 and B3. The track condition shall comply with [ERA] standard and [MMI-ETCS-gen].
    /// 
    /// Tested Requirements:
    /// MMI_gen 619 (partly: PL05 or PL06, PL07 or PL08); MMI_gen 9980 (partly:Table45(PL05 or PL06, PL07 or PL08)); MMI_gen 9979 (partly: ANNOUNCE, END); MMI_gen 662 (partly: TC06, TC07, TC08 or TC09); MMI_gen 10465 (partly:Table40(TC06, TC07, TC08 or TC09)); MMI_gen 9965; MMI_gen 636 (partly: ANNOUNCE, END); MMI_gen 2604 (partly: bottom of the symbol, D2);
    /// 
    /// Scenario:
    /// 1.Drive the train forward pass BG0 at position 10m.BG0: pkt 12, 21 and 27 (Entering FS) 
    /// 2.Drive the train forward pass BG1 at position 100m. Then,verify the display of PA track condition based on received packet EVC-32.BG1: pkt 68 (M_TRACKCOND = 9) (Switch off main power switch)
    /// 3.Verify the Track condition symbol in sub-Area D2 and B3
    /// 
    /// Used files:
    /// 17_4_9.tdg
    /// </summary>
    public class TC_ID_17_4_9_PA_Track_Condition_Switch_off_main_power_switch_in_Sub_Area_D2_and_B3 : TestcaseBase
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
                                "1. DMI displays a speed of 20 km/h.");

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
            Action: Pass BG1 with Track conditionPkt 68:D_TRACKCOND = 500L_TRACKCOND = 200M_TRACKCOND = 9(Switch off main power switch)
            Expected Result: Mode remians in FS mode
            */

            /*
            Test Step 4
            Action: Enter Anouncement of Track condition “Swich off main power switch”
            Expected Result: Verify the following information(1)   DMI displays PL05 or PL06 symbol in sub-area D2. (PL05) or  (PL06)
            Test Step Comment: (1) MMI_gen 619 (partly: PL05 or PL06);
            */
            EVC32_MMITrackConditions.MMI_Q_TRACKCOND_UPDATE = 0;
            TrackCondition trackCondition = new TrackCondition
            {
                MMI_O_TRACKCOND_ANNOUNCE = 30000,
                MMI_O_TRACKCOND_START = 0,
                MMI_O_TRACKCOND_END = 0,
                MMI_NID_TRACKCOND = 0,
                MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Main_power_switch_Neutral_Section,
                MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.AnnounceArea,
                MMI_Q_TRACKCOND_ACTION_START = Variables.MMI_Q_TRACKCOND_ACTION.WithoutDriverAction,
                MMI_Q_TRACKCOND_ACTION_END = 0
            };

            EVC32_MMITrackConditions.TrackConditions = new List<TrackCondition> {trackCondition};
            EVC32_MMITrackConditions.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays symbol PL05 in sub-area D2.");

            /*
            Test Step 5
            Action: Stop the train
            Expected Result: Verify the following information(1)   Use the log file to confirm that DMI received packet information MMI_TRACK_CONDITIONS (EVC-32) and MMI_ETCS_MISC_OUT_SIGNALS (EVC-7) with the following variables,MMI_M_TRACkCOND_TYPE = 9MMI_Q_TRACKCOND_STEP = 0 MMI_Q_TRACKCOND_ACTION_START = 1 (PL05) or 0 (PL06)MMI_O_TRACKCOND_ANNOUNCE - OBU_TR_O_TRAIN (EVC-7)  =  Remaining distance from PL05 or PL06 symbol on area D2 to the first distance scale line (zero line)(2)    The bottom of PL05 or PL06 symbol is displayed with the correct position in the PA distance scale refer to the result of calculation from expected result (1)
            Test Step Comment: (1) MMI_gen 9980 (partly:Table45(PL05 or PL06));MMI_gen 9979 (partly: ANNOUNCE); MMI_gen 636 (partly: ANNOUNCE); (2) MMI_gen 2604 (partly: bottom of the symbol, D2);
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 0;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN = 11000;
            // In diagram first scale line is at 125, bottom of symbol at ~80 (m)
            // O_TRACKCOND_ANNOUNCE - MMI_OBU_TR_O_TRAIN should == 8000

            // Test spec says _STEP is 0, Table 45 says PL17 is _TYPE 5 (Air-tightness), _STEP 1, _ACTION_START 1
            trackCondition.MMI_O_TRACKCOND_ANNOUNCE = 19000;
            EVC32_MMITrackConditions.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The bottom of the PL05 symbol is displayed at ~80.");

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
            Action: Stop the train when the TC06 or TC07 symbol displays in sub-area B3
            Expected Result: Verify the following information(1)   DMI displays TC06 or TC07 symbol in sub-area B3. (TC06) or  (TC07)(2)   Use the log file to confirm that DMI received packet information MMI_TRACK_CONDITIONS (EVC-32) with the following variables,MMI_M_TRACkCOND_TYPE = 9MMI_Q_TRACKCOND_STEP = 1 (TC06 or TC07) or 2(TC06)MMI_Q_TRACKCOND_ACTION_START = 0 (TC07) or 1(TC06)
            Test Step Comment: (1) MMI_gen 10465 (partly:Table40(TC06, TC07));(2) MMI_gen 662 (partly: TC06, TC07);
            */
            // Remove current track condition?
            trackCondition.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.RemoveTC;
            EVC32_MMITrackConditions.Send();

            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 0;
            trackCondition.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.InsideArea_Active;
            trackCondition.MMI_Q_TRACKCOND_ACTION_START = Variables.MMI_Q_TRACKCOND_ACTION.WithoutDriverAction;
            EVC32_MMITrackConditions.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays symbol TC06 in sub-area B3.");

            /*
            Test Step 8
            Action: Enter Anouncement of Track condition “End of Swich off main power switch”
            Expected Result: Verify the following information(1)   DMI displays PL07 or PL08 symbol in sub-area D2. (PL07) or  (PL08)(2)   Use the log file to confirm that DMI received packet information MMI_TRACK_CONDITIONS (EVC-32) and MMI_ETCS_MISC_OUT_SIGNALS (EVC-7) with the following variables,MMI_M_TRACkCOND_TYPE = 9MMI_Q_TRACKCOND_STEP = 0 or 1 or 2 (PL07 or PL08)MMI_Q_TRACKCOND_ACTION_END = 1 (PL07) or 0 (PL08)MMI_O_TRACKCOND_END - OBU_TR_O_TRAIN (EVC-7)   =  Remaining distance from PL07 or PL08 symbol in sub-area D2 to the first distance scale line (zero line)(3)    The bottom of PL07 or PL08 symbol is displayed with the correct position in the PA distance scale refer to the result of calculation from expected result (2)
            Test Step Comment: (1) MMI_gen 619(partly: PL07 or PL08);(2) MMI_gen 9980 (partly:Table45(PL07 or PL08));MMI_gen 9979(partly: END); MMI_gen 636 (partly: END); (3) MMI_gen 2604 (partly: bottom of the symbol, D2);
            */
            // Remove current track condition?
            trackCondition.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.RemoveTC;
            EVC32_MMITrackConditions.Send();

            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 0;
            trackCondition.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.LeaveArea;
            trackCondition.MMI_Q_TRACKCOND_ACTION_END = Variables.MMI_Q_TRACKCOND_ACTION.WithoutDriverAction;
            EVC32_MMITrackConditions.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays symbol PL07 in sub-area D2.");

            /*
            Test Step 9
            Action: Drive the train forward with speed = 20 km/h
            Expected Result: The speed pointer is indicated as 20  km/h
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 20;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The speed pointer is displayed with speed = 20 km/h.");

            /*
            Test Step 10
            Action: Stop the train when the TC08 or TC09 symbol displays in sub-area B3
            Expected Result: Verify the following information(1)   DMI displays TC08 or TC09 symbol in sub-area B3.  (TC08) or  (TC09)(2)   Use the log file to confirm that DMI received packet information MMI_TRACK_CONDITIONS (EVC-32) with the following variables,MMI_M_TRACkCOND_TYPE = 9MMI_Q_TRACKCOND_STEP = 3MMI_Q_TRACKCOND_ACTION_END = 1 (TC08) or 0 (TC09)
            Test Step Comment: (1) MMI_gen 10465 (partly:Table40(TC08 or TC09));(2) MMI_gen 662 (partly: TC08 or TC09);
            */
            // Remove current track condition?
            trackCondition.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.RemoveTC;
            EVC32_MMITrackConditions.Send();

            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 0;
            trackCondition.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.LeaveArea;
            EVC32_MMITrackConditions.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays symbol TC08 in sub-area B3.");

            /*
            Test Step 11
            Action: Driver the train up to 40 km/h
            Expected Result: The speed pointer is indicated as 20  km/h
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 40;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The speed pointer is displayed with speed = 40 km/h.");

            /*
            Test Step 12
            Action: Stop the train when the track condition symbol has been removed from sub-area B3
            Expected Result: Verify the following information(1)   Use the log file to confirm that DMI received packet information MMI_TRACK_CONDITIONS (EVC-32) with the following variables,MMI_Q_TRACKCOND_STEP = 4MMI_NID_TRACKCOND = Same value with expected result No.2 of step 10
            Test Step Comment: (1) MMI_gen 9965;
            */
            trackCondition.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.RemoveTC;
            EVC32_MMITrackConditions.Send();

            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 0;

            /*
            Test Step 13
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
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
using Testcase.Telegrams.DMItoEVC;
using Testcase.Telegrams.EVCtoDMI;

namespace Testcase.DMITestCases
{
    /// <summary>
    /// 20.6.1 Level Crossing “not protected” Indication: General Display
    /// TC-ID: 15.6
    /// 
    /// This test case verifies the display of Level crossing ‘Not protected’ symbol (LX01).
    /// The symbol is displayed/removed correctly according to received packet information EVC-33.
    /// 
    /// Tested Requirements:
    /// MMI_gen 10481;  MMI_gen 9503; MMI_gen 10485; MMI_gen 10484; MMI_gen 10483; MMI_gen 10486; Note under MMI_gen 10486;
    /// 
    /// Scenario:
    /// Drive the train forward past BG
    /// 
    /// Verify the display information of LX01 symbol with received packet information EVC-33.
    ///     BG1: Packet 12, 21, 27 and 88 (Entering FS with Level crossing not protected with MA = 5000 m)
    ///     BG2: Packet 88 (Add Level crossing not protected)
    ///     BG3: Packet 88 (Add Level crossing not protected)
    /// 
    /// Continue to drive the train forward past position 400 m.
    /// Verify that LX01 symbol is removed according to received packet information EVC-33.
    /// 
    /// Used files:
    /// 15_6.tdg
    /// </summary>
    public class TC_ID_15_6_Level_Crossing_not_protected_Indication : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // Test system is powered on.
            // Activate Cabin A
            // SoM in performed in SR mode, Level 1.

            // Call the TestCaseBase PreExecution
            base.PreExecution();
            DmiActions.Complete_SoM_L1_SR(this);
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in FS mode, level 1.

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint

            /*
            Test Step 1
            Action: Drive the train forward past BG1
            Expected Result: DMI displays in FS mode, Level 1.
                Verify the following information:
                    MMI_M_TKCOND_TYPE = 16
                    DMI displays LX01 symbol in sub-area B3
            Test Step Comment: (1) MMI_gen 10481;  (2) MMI_gen 9503; MMI_gen 10485; 
            */
            DmiActions.Send_FS_Mode(this);

            EVC33_MMIAdditionalOrder.MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Level_Crossing;
            EVC33_MMIAdditionalOrder.MMI_NID_TRACKCOND = 1;
            EVC33_MMIAdditionalOrder.MMI_Q_TRACKCOND_ACTION = Variables.MMI_Q_TRACKCOND_ACTION.WithoutDriverAction;
            EVC33_MMIAdditionalOrder.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.AnnounceArea;
            EVC33_MMIAdditionalOrder.Send();

            DmiExpectedResults.Driver_symbol_displayed(this, "LX not protected", "LX01", "B3", false);

            /*
            Test Step 2
            Action: Drive the train forward past BG2
            Expected Result: DMI displays in FS mode, Level 1.
                Verify the following information:
                    MMI_Q_TRACKCOND_STEP = 1
                    MMI_M_TKCOND_TYPE = 16
                    DMI displays LX01 symbol in sub-area B4
            Test Step Comment: (1) MMI_gen 10481;  (2) MMI_gen 9503; MMI_gen 10485;
            */
            EVC33_MMIAdditionalOrder.MMI_NID_TRACKCOND = 2;
            EVC33_MMIAdditionalOrder.Send();
            DmiExpectedResults.Driver_symbol_displayed(this, "LX not protected", "LX01", "B4", false);

            /*
            Test Step 3
            Action: Drive the train forward past BG3. Then, stop the train.
            Expected Result: DMI displays in FS mode, Level 1.
                Verify the following information
                    MMI_Q_TRACKCOND_STEP = 1
                    MMI_M_TKCOND_TYPE = 16
                    DMI displays LX01 symbol in sub-area B5
            Test Step Comment: (1) MMI_gen 10481;  (2) MMI_gen 9503; MMI_gen 10485; MMI_gen 10483;
            */
            EVC33_MMIAdditionalOrder.MMI_NID_TRACKCOND = 3;
            EVC33_MMIAdditionalOrder.Send();
            DmiExpectedResults.Driver_symbol_displayed(this, "LX not protected", "LX01", "B5", false);

            /*
            Test Step 4
            Action: Simulate loss-communication between DMI and ETCS onboard
            Expected Result: All LX01 symbols are removed from sub-area B3-B5
            Test Step Comment: (1) MMI_gen 10486 (partly: criteria, MMI_gen 244);
            */
            DmiActions.Simulate_communication_loss_EVC_DMI(this);
            this.WaitForVerification("Have all three LX symbols disappeared from the DMI?");

            /*
            Test Step 5
            Action: Re-establish communication between DMI and ETCS onboard.
            Expected Result: All LX01 symbols have reappeared in sub-area B3-B5
            Test Step Comment: (1) Note under MMI_gen 10486;
            */
            DmiActions.Re_establish_communication_EVC_DMI(this);
            this.WaitForVerification("Have all three LX symbols re-appeared on the DMI?");

            /*
            Test Step 6
            Action: Continue to drive the train forward with speed below permitted speed
            Expected Result: The LX01 symbol is removed from sub-area B5.
                            DMI received packet information EVC-33 with following vairables:
                                MMI_Q_TRACKCOND_STEP = 4
                                MMI_NID_TRACKCOND is same value as received packet from step 3
            Test Step Comment: (1) MMI_gen 10484 (partly: removed stored LX);
                                (2) MMI_gen 10484 (partly: reception packet EVC-33, NID = MMI_NID_TRACKCOND);
            */
            EVC33_MMIAdditionalOrder.MMI_NID_TRACKCOND = 3;
            EVC33_MMIAdditionalOrder.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.RemoveTC;
            EVC33_MMIAdditionalOrder.Send();
            this.WaitForVerification("Has the LX symbol in area B5 disappeared from the DMI?");

            /*
            Test Step 7
            Action: Continue to drive the train forward with speed below permitted speed
            Expected Result: The LX01 symbol is removed from sub-area B4.
                            MMI_Q_TRACKCOND_STEP = 4
                            MMI_NID_TRACKCOND is same value as received packet from step 2
            Test Step Comment: (1) MMI_gen 10484 (partly: removed stored LX);
                                (2) MMI_gen 10484 (partly: reception packet EVC-33, NID = MMI_NID_TRACKCOND);
            */
            EVC33_MMIAdditionalOrder.MMI_NID_TRACKCOND = 2;
            EVC33_MMIAdditionalOrder.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.RemoveTC;
            EVC33_MMIAdditionalOrder.Send();
            this.WaitForVerification("Has the LX symbol in area B4 disappeared from the DMI?");


            /*
            Test Step 8
            Action: Continue to drive the train forward with speed below permitted speed
            Expected Result: The LX01 symbol is removed from sub-area B3.
                                MMI_Q_TRACKCOND_STEP = 4
                                MMI_NID_TRACKCOND is same value as received packet from step 1
            Test Step Comment: (1) MMI_gen 10484 (partly: removed stored LX);
                                (2) MMI_gen 10484 (partly: reception packet EVC-33, NID = MMI_NID_TRACKCOND);
            */
            EVC33_MMIAdditionalOrder.MMI_NID_TRACKCOND = 2;
            EVC33_MMIAdditionalOrder.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.RemoveTC;
            EVC33_MMIAdditionalOrder.Send();
            this.WaitForVerification("Has the LX symbol in area B3 disappeared from the DMI?");

            /*
            Test Step 9
            Action: Repeat action step 1-3
            Expected Result: The LX01 symbols are displayed in sub-area B3-B5
            */
            EVC33_MMIAdditionalOrder.MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Level_Crossing;
            EVC33_MMIAdditionalOrder.MMI_NID_TRACKCOND = 1;
            EVC33_MMIAdditionalOrder.MMI_Q_TRACKCOND_ACTION = Variables.MMI_Q_TRACKCOND_ACTION.WithoutDriverAction;
            EVC33_MMIAdditionalOrder.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.AnnounceArea;
            EVC33_MMIAdditionalOrder.Send();

            // Delay so that separate telegram can be sent
            this.Wait_Realtime(500);

            EVC33_MMIAdditionalOrder.MMI_NID_TRACKCOND = 2;
            EVC33_MMIAdditionalOrder.Send();

            // Delay so that separate telegram can be sent
            this.Wait_Realtime(500);

            EVC33_MMIAdditionalOrder.MMI_NID_TRACKCOND = 3;
            EVC33_MMIAdditionalOrder.Send();

            this.WaitForVerification("Have all three LX symbols re-appeared in areas B3-B5 on the DMI?");

            /*
            Test Step 10
            Action: Deactivate cabin. Then, simulate loss-communication between DMI and ETCS onboard
            Expected Result: 1. All LX01 symbols are removed from sub-area B3-B5
            Test Step Comment: (1) MMI_gen 10486 (partly: criteria, MMI_gen 240);
            */
            DmiActions.Deactivate_Cabin(this);
            DmiActions.Simulate_communication_loss_EVC_DMI(this);
            this.WaitForVerification("Have all three LX symbols disappeared from the DMI?");

            /*
            Test Step 11
            Action: Re-establish communication between DMI and ETCS onboard. Then, activate cabin
            Expected Result: 1. All LX01 symbols have re-appeared in sub-areas B3-B5
            Test Step Comment: (1) Note under MMI_gen 10486;
            */
            DmiActions.Re_establish_communication_EVC_DMI(this);
            DmiActions.Activate_Cabin_1(this);
            this.WaitForVerification("Have all three LX symbols re-appeared on the DMI?");

            /*
            Test Step 12
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
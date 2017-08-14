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
using Testcase.XML;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 17.6.1 Basic Speed Hook(s): General appearance
    /// TC-ID: 12.6.1
    /// 
    /// This test case verifies the general appearance of the Basic Speed Hook(s) in OS mode (for supervision is not CSM) and SH mode that displays around the speed dial. The general appearance of the Basic Speed Hook(s) shall comply with [ERA] standard and [MMI-ETCS-gen].
    /// 
    /// Tested Requirements:
    /// MMI_gen 6322; MMI_gen 6332 (partly: OBU_TR_M_MODE, MMI_V_PERMITTED, MMI_V_TARGET, MMI_M_WARNING, colour and appearance, OS mode not CSM, SH mode CSM); MMI_gen 6329 (partly: outer border of the speed dial); MMI_gen 6330 (partly: outer border of the speed dial); MMI_gen 9972; MMI_gen 6456 (partly: OS mode, Not CSM); MMI_gen 6452; MMI_gen 9516 (partly: toggling function of basic speed hooks with PS and TS); MMI_gen 12025 (partly: toggling function of basic speed hooks with PS and TS);
    /// 
    /// Scenario:
    /// Drive the train forward pass BG1 at position 100m. Then, verify the display information of all basic speed hooks refer to received packet information EVC1 and EVC-7.BG1: packet 12, 21, 27 and 80 (Entering OS)Continue to drive the train forward. Then, verify the display information of all basic speed hooks when its overlapping.Entering SH mode. Then, verify the display information of basic speed hook refer to received packet information EVC-1.Use the test script file to send an invalid value in EVC-1 and EVC-
    /// 7.Then, verify that basic speed hook is removed.
    /// 
    /// Used files:
    /// 12_6_1.tdg, 12_6_1_a.xml, 12_6_1_b.xml
    /// </summary>
    public class Basic_Speed_Hooks_General_appearance : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // The DMI default configuration has TOGGLE_FUNCTION = 0 (‘ON’).System is power on.Cabin is activated.SoM is performed in SR mode, Level 1.
            DmiActions.Complete_SoM_L1_SR(this);

            // Call the TestCaseBase PreExecution
            base.PreExecution();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in SH mode, level 1.
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in SH mode, Level 1.");

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint

            /*
            Test Step 1
            Action: Driver drives the train forward passing BG1.Then, stop the train and acknowledge OS mode by pressing sub-area C1
            Expected Result: DMI displays in OS mode, Level 1.Verify the following information,(1)   Use the log file to confirm that DMI received packet information EVC-7 with variable OBU_TR_M_MODE = 1 (On sight).(2)   Use the log file to confirm that DMI received packet information EVC-1 with following variables,MMI_V_PERMITTED = 2777 (100km/h)MMI_V_TARGET = 694 (25km/h)MMI_M_WARNING not equal to 0,4,8,12 (Supervision is not CSM)(3)   All basic speed hooks are displayed in sub-area B2.(4)   The first hook is displayed overlapping the outer border of the speed dial with white colour at 100 km/h.(5)   The second hook is displayed overlapping the outer border of the speed dial with Medium-grey colour at 25 km/h.(6)   Sound ‘Sinfo’ is played once
            Test Step Comment: (1) MMI_gen 6332 (partly: OBU_TR_M_MODE);(2) MMI_gen 6332 (partly: MMI_V_PERMITTED, MMI_V_TARGET, MMI_M_WARNING); MMI_gen 6456 (partly: Permitted Speed changes, Target Speed changes, OS mode, Not CSM);(3) MMI_gen 6322; MMI_gen 6456 (partly: toggle on);(4) MMI_gen 6332 (partly: colour and appearance, OS mode, not CSM); MMI_gen 6329 (partly: outer border of the speed dial);(5) MMI_gen 6332 (partly: colour and appearance, OS mode, not CSM); MMI_gen 6330 (partly: outer border of the speed dial);(6) MMI_gen 6456 (partly: sound Sinfo); MMI_gen 9516 (partly: toggling function of basic speed hooks with PS and TS); MMI_gen 12025 (partly: toggling function of basic speed hooks with PS and TS);
            */
            DmiActions.Drive_the_train_forward_pass_BG1_Then_press_an_acknowledgement_of_OS_mode_in_sub_area_C1(this);

            // EVC7_MMIEtcsMiscOutSignals Send
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 5;
            EVC1_MMIDynamic.MMI_V_PERMITTED = 2777;
            EVC1_MMIDynamic.MMI_V_TARGET = 694;
            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Indication_Status_Target_Speed_Monitoring;    // Not 0, 4, 8, 12
            // ?? Send

            WaitForVerification("Check  the following:" + Environment.NewLine + Environment.NewLine +
                                "1. All basic speed hooks are displayed in sub - area B2." + Environment.NewLine +
                                "2. The first hook is displayed overlapping the outer border of the speed dial in white colour at 100 km/h." + Environment.NewLine +
                                "3. The second hook is displayed overlapping the outer border of the speed dial in medium-grey colour at 25 km/h." + Environment.NewLine +
                                "4. Sound ‘Sinfo’ is played once.");

            /*
            Test Step 2
            Action: Continue to drive the train forward until basic speed hooks are overlapped
            Expected Result: Verify the following information,The Vperm hook (White colour) is overlay the Vtarget hook (Medium grery colour)
            Test Step Comment: MMI_gen 9972;    
            */
            WaitForVerification("Wait until the basic speed hooks overlap, then check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The Vperm hook (in white) overlays the Vtarget hook (medium-grey)");

            /*
            Test Step 3
            Action: Perform the following procedure,Press the 'Main' button.Press and hold 'Shunting' button at least 2 seconds.Release 'Shunting' button
            Expected Result: DMI displays in SH mode, level 1.Verify the following information,(1)   Use the log file to confirm that DMI received packet information EVC-7 with variable OBU_TR_M_MODE = 3 (Shunting).(2)   Use the log file to confirm that DMI received packet information EVC-1 with following variables,MMI_V_PERMITTED = 833 (30km/h)MMI_M_WARNING = 0 (NoS, Supervision = CSM)(3)   The first hook is displayed overlapping the outer border of the speed dial with white colour at 30 km/h
            Test Step Comment: (1) MMI_gen 6332 (partly: OBU_TR_M_MODE);(2) MMI_gen 6332 (partly: MMI_V_PERMITTED, MMI_M_WARNING);(3) MMI_gen 6322; MMI_gen 6332 (partly: colour and appearance, SH mode, CSM); MMI_gen 6329 (partly: outer border of the speed dial);
            */
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.Shunting;
            EVC1_MMIDynamic.MMI_V_PERMITTED = 883;
            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Normal_Status_Ceiling_Speed_Monitoring;

            WaitForVerification("Press the 'Main' button. Press and hold 'Shunting' button at least 2 seconds. Release 'Shunting' button." + Environment.NewLine +
                                "Check  the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in SH mode, level 1." + Environment.NewLine +
                                "2. The first hook is displayed overlapping the outer border of the speed dial coloured in white at 30 km/h.");

            /*
            Test Step 4
            Action: Use the test script file 12_6_1_a.xml to send EVC-1 with,MMI_M_WARNING = 7
            Expected Result: Verify the following information,(1)   The basic speed hook is removed from the DMI.(2)   After test scipt file is executed, the basic speed hook is re-appear refer to received packet EVC-1 from ETCS Onboard
            Test Step Comment: (1) MMI_gen 6452 (partly: MMI_M_WARNING is invalid);(2) MMI_gen 6452 (partly: toggle function is reset to default state);
            */
            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Spare;
            XML_12_6_1_a.Send(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The basic speed hook is removed from the DMI.");

            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Normal_Status_Ceiling_Speed_Monitoring;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The basic speed hook is re-displayed.");

            /*
            Test Step 5
            Action: Use the test script file 12_6_1_b.xml to send EVC-7 with,OBU_TR_M_MODE = 17
            Expected Result: Verify the following information,(1)   The basic speed hook is removed from the DMI.(2)   After test script file is executed, the basic speed hook is re-appear refer to received packet EVC-1 from ETCS Onboard
            Test Step Comment: (1) MMI_gen 6452 (partly: OBU_TR_M_MODE is invalid);(2) MMI_gen 6452 (partly: toggle function is reset to default state);
            */
            XML_12_6_1_b.Send(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The basic speed hook is removed from the DMI.");

            // Test says EVC-1 restores display but M_MODE is still invalid: should send valid EVC-7 state??
            //EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Normal_Status_Ceiling_Speed_Monitoring;

            //EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.FullSupervision;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The basic speed hook is re-displayed.");

            /*
            Test Step 6
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
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

namespace Testcase.DMITestCases
{
    /// <summary>
    /// 20.6.1 Level Crossing “not protected” Indication: General Display
    /// TC-ID: 15.6
    /// 
    /// This test case verifies the display of Level crossing ‘Not protected’ symbol (LX01), the symbol is display/remove correctly refer to received packet information EVC-33.
    /// 
    /// Tested Requirements:
    /// MMI_gen 10481;  MMI_gen 9503; MMI_gen 10485; MMI_gen 10484; MMI_gen 10483; MMI_gen 10486; Note under MMI_gen 10486;
    /// 
    /// Scenario:
    /// Drive the train forward pass BG
    /// 1.Then, verify the display information of LX01 symbol with received packet information EVC-33.BG1: packet 12, 21, 27 and 88 (Entering FS with Level crossing not protected with MA = 5000m)BG2: packet 88 (Add Level crossing not protected)BG3: packet 88 (Add Level crossing not protected)Continue to drive the train forward pass position 400m. Then, verify that LX01 symbol is removed refer to received packet information EVC-33.
    /// 
    /// Used files:
    /// 15_6.tdg
    /// </summary>
    public class Level_Crossing_not_protected_Indication_General_Display : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // Test system is powered on.Activate Cabin A.SoM in performed in SR mode, Level 1.

            // Call the TestCaseBase PreExecution
            base.PreExecution();
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
            Action: Drive the train forward pass BG1
            Expected Result: DMI displays in FS mode, Level 1.Verify the following information,Use the log file to confirm that DMI received packet information EVC-33 with following variables,MMI_Q_TRACKCOND_STEP = 1MMI_M_TKCOND_TYPE = 16DMI displays LX01 symbol in sub-area B3
            Test Step Comment: (1) MMI_gen 10481;  (2) MMI_gen 9503; MMI_gen 10485; 
            */
            // Call generic Action Method
            DmiActions.Drive_the_train_forward_pass_BG1(this);


            /*
            Test Step 2
            Action: Drive the train forward pass BG2
            Expected Result: DMI displays in FS mode, Level 1.Verify the following information,Use the log file to confirm that DMI received packet information EVC-33 with following variables,MMI_Q_TRACKCOND_STEP = 1MMI_M_TKCOND_TYPE = 16DMI displays LX01 symbol in sub-area B4
            Test Step Comment: (1) MMI_gen 10481;  (2) MMI_gen 9503; MMI_gen 10485;
            */
            // Call generic Action Method
            DmiActions.Drive_the_train_forward_pass_BG2(this);


            /*
            Test Step 3
            Action: Drive the train forward pass BG3.Then, Stop the Train
            Expected Result: DMI displays in FS mode, Level 1.Verify the following information,Use the log file to confirm that DMI received packet information EVC-33 with following variables,MMI_Q_TRACKCOND_STEP = 1MMI_M_TKCOND_TYPE = 16DMI displays LX01 symbol in sub-area B5
            Test Step Comment: (1) MMI_gen 10481;  (2) MMI_gen 9503; MMI_gen 10485; MMI_gen 10483;
            */


            /*
            Test Step 4
            Action: Simulate loss-communication between DMI and ETCS onboard
            Expected Result: Verify the following information,All LX01 symbols are removed from sub-area B3-B5
            Test Step Comment: (1) MMI_gen 10486 (partly: criteria, MMI_gen 244);
            */


            /*
            Test Step 5
            Action: Re-establish communication between DMI and ETCS onboard.Then, drive the train forward
            Expected Result: Verify the following information,All LX01 symbols are reappear in sub-area B3-B5
            Test Step Comment: (1) Note under MMI_gen 10486;
            */


            /*
            Test Step 6
            Action: Continue to drive the train forward with speed below permitted speed
            Expected Result: Verify the following information,The LX01 symbol is removed from sub-area B5.Use the log file to confirm that DMI received packet information EVC-33 with following vairables,MMI_Q_TRACKCOND_STEP = 4MMI_NID_TRACKCOND is same value as received packet from step 3
            Test Step Comment: (1) MMI_gen 10484 (partly: removed stored LX);(2) MMI_gen 10484 (partly: reception packet EVC-33, NID = MMI_NID_TRACKCOND);
            */
            // Call generic Action Method
            DmiActions.Continue_to_drive_the_train_forward_with_speed_below_permitted_speed(this);


            /*
            Test Step 7
            Action: Continue to drive the train forward with speed below permitted speed
            Expected Result: Verify the following information,The LX01 symbol is removed from sub-area B4.Use the log file to confirm that DMI received packet information EVC-33 with following vairables,MMI_Q_TRACKCOND_STEP = 4MMI_NID_TRACKCOND is same value as received packet from step 2
            Test Step Comment: (1) MMI_gen 10484 (partly: removed stored LX);(2) MMI_gen 10484 (partly: reception packet EVC-33, NID = MMI_NID_TRACKCOND);
            */
            // Call generic Action Method
            DmiActions.Continue_to_drive_the_train_forward_with_speed_below_permitted_speed(this);


            /*
            Test Step 8
            Action: Continue to drive the train forward with speed below permitted speed
            Expected Result: Verify the following information,The LX01 symbol is removed from sub-area B3.Use the log file to confirm that DMI received packet information EVC-33 with following vairables,MMI_Q_TRACKCOND_STEP = 4MMI_NID_TRACKCOND is same value as received packet from step 1
            Test Step Comment: (1) MMI_gen 10484 (partly: removed stored LX);(2) MMI_gen 10484 (partly: reception packet EVC-33, NID = MMI_NID_TRACKCOND);
            */
            // Call generic Action Method
            DmiActions.Continue_to_drive_the_train_forward_with_speed_below_permitted_speed(this);


            /*
            Test Step 9
            Action: Perform the following procedure,Restart OTE and DMI.Perform SoM in SR mode, Level 1.Repeat action step 1-3
            Expected Result: The LX01 symbols are displayed in sub-area B3-B5
            */


            /*
            Test Step 10
            Action: Deactivate cabin.Then, simulate loss-communication between DMI and ETCS onboard
            Expected Result: Verify the following information,(1)    All LX01 symbols are removed from sub-area B3-B5
            Test Step Comment: (1) MMI_gen 10486 (partly: criteria, MMI_gen 240);
            */


            /*
            Test Step 11
            Action: Re-establish communication between DMI and ETCS onboard.Then, activate cabin
            Expected Result: Verify the following information,(1)    All LX01 symbols are reappear in sub-area B3-B5
            Test Step Comment: (1) Note under MMI_gen 10486;
            */


            /*
            Test Step 12
            Action: End of test
            Expected Result: 
            */


            return GlobalTestResult;
        }
    }
}
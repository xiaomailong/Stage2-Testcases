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
using Testcase.Telegrams.DMItoEVC;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 31.1 MMI Error Handling
    /// TC-ID: 26.1
    /// 
    /// This test case verifies the appearance of DMI error handling when the communication between DMI and ETCS Onboard is lost. 
    /// DMI plays sound alarm and removes all information during the ATP down. DMI error handling shall comply with conditions in [MMI-ETCS-gen].
    /// 
    /// Tested Requirements:
    /// MI_gen 244 (partly: 1st bullet); MMI_gen 244 (partly: 2nd bullet); MMI_gen 244 (partly: 3rd bullet); MMI_gen 157 (partly: sound); MMI_gen 244 (partly: 4th bullet);                                                                 
    /// MMI_gen 245 (partly: 1st bullet, sound); MMI_gen 245 (partly: 1st bullet, confirm button); MMI_gen 245 (partly: 2nd bullet); MMI_gen 246 (partly: 1st bullet, text/symbol); 
    /// MMI_gen 246 (partly: 2nd bullet); MMI_gen 246 (partly: 3rd bullet);
    ///
    /// 
    /// Scenario:
    /// 1.Activate cabin A. 
    /// 2.Perform SoM in SR mode, Level 1.
    /// 3.Drive the train forward pass BG1
    /// 4.Simulate the communication loss between DMI and ETCS Onboard.Then, verify the display information and sound.
    /// 5.Acknowledge the message.Then, verify the display information and sound.
    /// 6.Re-establish the communication between DMI and ETCS Onboard. Then, verify the display information and sound.
    /// 7.Simulate the communication loss between DMI and ETCS Onboard and re-establish again. Then, verify the display information and sound.
    /// 
    /// Used files:
    /// 26_1.tdg
    /// </summary>
    public class MMI_Error_Handling : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:

            // Call the TestCaseBase PreExecution
            base.PreExecution();
            // System is powered on.
            EVC0_MMIStartATP.Evc0Type = EVC0_MMIStartATP.EVC0Type.GoToIdle;
            EVC0_MMIStartATP.Send();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in FS mode, Level 1
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in FS mode, Level 1.");

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint

            /*
            Test Step 1          	 	
            Action: Activate cabin A. Driver performs SoM to SR mode, level 1
            Expected Result: DMI displays in SR mode, Level 1
            Test Step Comment: 
            */
            DmiActions.Perform_SoM_in_SR_mode_Level_1(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in SR mode, Level 1.");

            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN = 0;      // at start

            /*
            Test Step 2
            Action: Driver drives the train forward passing BG1
            Expected Result: DMI changes from SR mode to FS mode, Level 1. The planning area is displayed
            Test Step Comment:
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 10;
            EVC1_MMIDynamic.MMI_O_BRAKETARGET = 50000;                  // EOA 500m
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN = 25000;      // at 250m
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.FullSupervision;
            

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI removes the SR mode symbol (MO09) and displays the FS symbol (MO11) in area B7." + Environment.NewLine +
                                "2. DMI displays the planning area");

            /*
            Test Step 3
            Action: Increase the train speed until reaching the warning margin
            Expected Result: The over speed warning sound is played
            Test Step Comment:
            */
            EVC1_MMIDynamic.MMI_V_PERMITTED_KMH = 15;
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 16;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN = 25500;      // at 255m

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The warning sound is played");

            /*
            Test Step 4
            Action: Simulate the communication loss between DMI and ETCS Onboard
            Expected Result: DMI enters ‘ATP-down’ state. Verify that all information on DMI’s screen is disappeared.The continuous 1000Hz sound is play.DMI displays message ‘ATP Down Alarm’ with yellow flashing frame.Use log file to confirm that DMI sends out [MMI_STATUS_REPORT (EVC-102).MMI_M_MMI_STATUS] = 5 only once.         
            Test Step Comment:MMI_gen 244 (partly: 1st bullet);MMI_gen 244 (partly: 2nd bullet);MMI_gen 244 (partly: 3rd bullet);MMI_gen 157 (partly: sound);MMI_gen 244 (partly: 4th bullet);
            */
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN = 40000;      // at 400m
            DmiActions.Simulate_communication_loss_EVC_DMI(this);
            
            // EVC102 check?? MMI_M_MMI_STATUS = 5

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI enters ‘ATP-down’ state, displaying the message ‘ATP Down Alarm’ in a yellow flashing frame." + Environment.NewLine +
                                "2. All information on the DMI screen is disappears." + Environment.NewLine +
                                "3. The 1000Hz sound is played continuously.");

            /*
            Test Step 5
            Action: Driver acknowledges ‘ATP Down Alarm’ message
            Expected Result: Verify the following information,	The ATP down alarm is removed.The yellow flashing frame is removed but the message ‘ATP Down Alarm’ is still displayed.Use log file to confirm that DMI sends out [MMI_STATUS_REPORT (EVC-102).MMI_M_MMI_STATUS] = 6 only once.
            Test Step Comment:MMI_gen 245 (partly: 1st bullet, sound);MMI_gen 245 (partly: 1st bullet, confirm button);MMI_gen 245 (partly: 2nd bullet);
            */
            DmiActions.ShowInstruction(this, "Acknowledge the ‘ATP Down Alarm’ message");

            // EVC102 check?? MMI_M_MMI_STATUS = 6

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI stops playing the 1000Hz sound." + Environment.NewLine +
                                "2. DMI still displays the message ‘ATP Down Alarm’, but without a yellow flashing frame.");

            /*
            Test Step 6
            Action: Re-establish the communication between DMI and ETCS Onboard
            Expected Result: Verify the following information,The message ‘ATP Down Alarm’ is removed.Use log file to confirm that DMI sends out [MMI_STATUS_REPORT (EVC-102).MMI_M_MMI_STATUS] = 3 every 250ms.The normal operation is resumed
            Test Step Comment: MMI_gen 246 (partly: 1st bullet, text/symbol);MMI_gen 246 (partly: 2nd bullet);MMI_gen 246 (partly: 3rd bullet);
            */
            DmiActions.Re_establish_communication_EVC_DMI(this);

            // EVC102 check??  MMI_M_MMI_STATUS = 3

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI stops displaying the message ‘ATP Down Alarm’." + Environment.NewLine +
                                "2. DMI displays as before in FS mode.");

            /*
            Test Step 7
            Action: Stop the train
            Expected Result: The train is at standstill
            Test Step Comment:
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 0;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays speed = 0 km/h");

            /*
            Test Step 8
            Action: Simulate the communication loss between DMI and ETCS Onboard
            Expected Result: DMI enters ‘ATP-down’ state with continuous 1000Hz sound
            Test Step Comment: (1) MMI_gen 100;
            */
            DmiActions.Simulate_communication_loss_EVC_DMI(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI enters ‘ATP-Down’ state." + Environment.NewLine +
                                "2. The 1000Hz sound is played continuously.");

            /*
            Test Step 9
            Action: Re-establish the communication between DMI and ETCS Onboard
            Expected Result: Verify that if ATP Down is not acknowledged yet, the sound alarm and confirmation button are cleared when the communication is recovered
            Test Step Comment: MMI_gen 246 (partly: before driver’s confirmation)
            */
            DmiActions.Re_establish_communication_EVC_DMI(this);

            WaitForVerification("When ‘ATP-Down’ has not been acknowledged yet, check the following :" + Environment.NewLine + Environment.NewLine +
                                "1. The confirmation button is cleared." + Environment.NewLine +
                                "2. DMI stops playing the 1000Hz sound.");

            /*
            Test Step 10
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
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
    /// 22.5.1 PA Gradient Profile:  General appearance
    /// TC-ID: 17.5.1
    /// 
    /// This test case verifies that when DMI receives the data packet of gradient profiles from ETCS Onboard, the PA Gradient Profile displays in sub-area D5 correctly for uphill, downhill and zero gradient. Also verify the situation of communication lost, PAGP shall be removed. The PA Gradient Profile shall comply with [ERA-ERTMS] standard.
    /// 
    /// Tested Requirements:
    /// MMI_gen 638; MMI_gen 7271; MMI_gen 640;  MMI_gen 7268; MMI_gen 639;  MMI_gen 2605; MMI_gen 9940; MMI_gen 3050 (partly: white line); MMI_gen 3034 (partly: grey line); MMI_gen 7270 (partly: black line); Note under MMI_gen 7271;
    /// 
    /// Scenario:
    /// Activate cabin A. Perform SoM to SR mode, level 
    /// 1.Drive the train forward pass BG1 at position 
    /// 100.BG1: Packet 12, 21 and 27D_GRADIENT = 0, Q_GDIR=1, G_A=2 (uphill gradient)  N_ITER = 4D_GRADIENT = 200, Q_GDIR=1, G_A =5 (uphill gradient) D_GRADIENT = 200, Q_GDIR=0, G_A =20 (downhill gradient)D_GRADIENT = 200, Q_GDIR=1, G_A =0 (zero gradient)D_GRADIENT = 200, Q_GDIR=1, G_A =10 (uphill gradient)Drive the train until position 900m. Verify the correctness of the gradient profile displayed on the Planning area.Then simulate the communication loss between ETCS Onboard and DMI. After that re-establish the communication again.
    /// 
    /// Used files:
    /// 17_5_1.tdg
    /// </summary>
    public class PA_Gradient_Profile_General_appearance : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // System is power on.

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
            Action: Activate cabin A. Driver performs SoM to SR mode, level 1
            Expected Result: DMI displays in SR mode, level 1
            */
            // Call generic Action Method
            DmiActions.Activate_cabin_A_Driver_performs_SoM_to_SR_mode_level_1();
            // Call generic Check Results Method
            DmiExpectedResults.DMI_displays_in_SR_mode_level_1();


            /*
            Test Step 2
            Action: Drive the train forward passing BG1
            Expected Result: DMI changes from SR mode to FS mode.The Planning Area is displayed in area D.Verify that the Gradient Profile is displayed in sub-area D5 and shown as a set of bars with different gradients. (see the second figure in ‘Comment’ column)The Gradient Profile segment bar is displayed two ‘+’ signs for uphill gradient, two ‘-‘ signs for downhill gradient, and no sign for zero gradient. The gradient value is displayed in the middle of the bar. (see the second figure in ‘Comment’ column)The Downhill PA Gradient Profile segment bars are displayed in dark-grey colour with the value and sign of gradient in white. The Uphill and zero PA Gradient Profile segment bars are displayed in grey colour with the value and sign of gradient in black.The Uphill and zero PA Gradient Profile have a white line on their upper and left boundary.The Downhill PA Gradient Profile have a grey line on their upper and left boundary.All PA gradient Profile have a black line on their lower boundary
            Test Step Comment: (1) MMI_gen 638;      MMI_gen 9940;    (2) MMI_gen 640;              MMI_gen 7268;                       (3) MMI_gen 639;             (4) MMI_gen 2605;       (5) MMI_gen 3050 (partly: white line);         (6) MMI_gen 3034 (partly: grey line);           (7) MMI_gen 7270   (partly: black line);
            */
            // Call generic Action Method
            DmiActions.Drive_the_train_forward_passing_BG1();


            /*
            Test Step 3
            Action: Simulate the communication loss between ETCS Onboard and DMI
            Expected Result: DMI displays the  message “ATP Down Alarm” with sound.Verify that the PA Gradient Profile is removed from DMI
            Test Step Comment: MMI_gen 7271;   
            */
            // Call generic Action Method
            DmiActions.Simulate_the_communication_loss_between_ETCS_Onboard_and_DMI();


            /*
            Test Step 4
            Action: Re-establish the communication between ETCS onboard and DMI
            Expected Result: DMI displays in FS mode, level 1. The PA Gradient Profile is resumed
            Test Step Comment: Note under MMI_gen 7271;
            */
            // Call generic Action Method
            DmiActions.Re_establish_the_communication_between_ETCS_onboard_and_DMI();


            /*
            Test Step 5
            Action: End of test
            Expected Result: 
            */


            return GlobalTestResult;
        }
    }
}
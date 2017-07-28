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
    /// 22.5.3 PA Gradient Profile:  Information updating
    /// TC-ID: 17.5.3
    /// 
    /// This test case verifies a display information of PA Gradient Profile when received an updating information from EVC-4.
    /// 
    /// Tested Requirements:
    /// MMI_gen 7257; MMI_gen 7317 (partly: updated according variable MMI_G_GRADIENT_CURR); MMI_gen 7263 (partly: 1st bullet, EVC-4, The gradient profile ends); 
    /// 
    /// Scenario:
    /// Activate cabin A. Perform SoM to SR mode, level 1.Drive the train forward pass BG1 at position 100.BG1 giving pkt12, pkt 21 and pkt27.Q_GDIR = 1, G_A = 10Drive the train forward pass BG2 at position 
    /// 200.Then, verify that the PA Gradient Profile segments are updated correctly.BG2 giving pkt 21 and pkt27.Q_GDIR = 1, G_A = 20, N_ITER = 2, D_GRADIENT[1] = 250, Q_GDIR[1] = 1, G_A[1] = 25, D_GRADIENT[2] = 250, Q_GDIR[2] = 0, G_A[2] = 255.Drive the train forward pass BG3 at position 
    /// 300.Then, verify that the PA Gradient Profile segments are removed.BG3 giving pkt 21 and pkt 27.Q_GRID = 0, G_A = 255
    /// 
    /// Used files:
    /// 17_5_3.tdg
    /// </summary>
    public class PA_Gradient_Profile_Information_updating : TestcaseBase
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
            // DMI displays TR mode, level 1.

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint


            /*
            Test Step 1
            Action: Activate cabin A. Then  perform SoM to SR mode, level 1
            Expected Result: DMI displays SR mode, level 1
            */
            // Call generic Action Method
            DmiActions.Activate_cabin_A_Then_perform_SoM_to_SR_mode_level_1();
            // Call generic Check Results Method
            DmiExpectedResults.DMI_displays_SR_mode_level_1();


            /*
            Test Step 2
            Action: Drive the train forward pass BG1
            Expected Result: DMI changes from SR to FS mode. The Planning Area is displayed in area D with PA Gradient profile value = 10 with grey colour (uphill).Verify the following information,After PA information is updated, use the log file to verify a value of each variables in first packet of EVC-4 at transition state,MMI_N_GRADIENT = 0MMI_G_GRADIENT_CURR = 10The upper boundary of PA Gradient Profile is extended to 2000m
            Test Step Comment: (1) MMI_gen 7257 (partly: 1st bullet (normal value));(2) MMI_gen 7257 (Partly: 2nd bullet);
            */
            // Call generic Action Method
            DmiActions.Drive_the_train_forward_pass_BG1();


            /*
            Test Step 3
            Action: Drive the train forward pass BG2
            Expected Result: Verify the following information,The order of PA Gradient Profile segments are updated correctly refer to received EVC-4 packet 0-250m: Gradient Profile value = 20 (grey colour)251-500m: Gradient Profile value = 25 (grey colour)501-2000m: No Gradient Profile display.Use the log file to confirm that DMI receives EVC-4 packet with the following variables,MMI_G_GRADIENT_CURR = 20MMI_N_GRADIENT = 2MMI_G_GRADIENT[0] = 25MMI_G_GRADIENT[1] = -255Note: The first index of parameter is the topmost position in packet EVC-4
            Test Step Comment: (1) MMI_gen 7317 (partly: updated according variable MMI_G_GRADIENT_CURR);                    MMI_gen 7263 (partly: 1st bullet);       (2) MMI_gen 7263 (partly:EVC-4, The gradient profile ends);
            */
            // Call generic Action Method
            DmiActions.Drive_the_train_forward_pass_BG2();


            /*
            Test Step 4
            Action: Drive the train forward pass BG3
            Expected Result: DMI changes from FS mode to TR mode because of PA gradient profile is removed.Note: There is text message ‘Balise read error’ displays in sub-area E5-E9.Verify the following information,After PA information is updated, use the log file to verify a value of each variables in first packet of EVC-4 at transition state,MMI_N_GRADIENT = 0MMI_G_GRADIENT_CURR = -255
            Test Step Comment: (1) MMI_gen 7257 (partly: delete all PA Gradient profile, 1st bullet (special value));
            */
            // Call generic Action Method
            DmiActions.Drive_the_train_forward_pass_BG3();


            /*
            Test Step 5
            Action: End of test
            Expected Result: 
            */


            return GlobalTestResult;
        }
    }
}
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
    /// 17.6.2 Basic Speed Hook(s): Speed hooks are outside Speed Dial’s range
    /// TC-ID: 12.6.2
    /// 
    /// This test case verifies the appearance of the Basic Speed Hook(s) in OS mode (for supervision is not CSM) when permitted speed and target speed are outside the range of speed dial, the Basic Speed Hook(s) are not display.
    /// 
    /// Tested Requirements:
    /// MMI_gen 6331;
    /// 
    /// Scenario:
    /// 1.Drive the train forward pass BG1 at position 100m. Then, verify the display information of all basic speed hooks refer to received packet information EVC1 and EVC-7.BG1: packet 12, 21, 27 and 80 (Entering OS)
    /// 2.Use the test script file to send EVC-1 with permitted speed and target speed is outside range of speed dial. Then, verify that Basic Speed Hook(s) are not display.
    /// 
    /// Used files:
    /// 12_6_2.tdg, 12_6_2_a.xml, 12_6_2_b.xml
    /// </summary>
    public class Basic_Speed_Hooks_Speed_hooks_are_outside_Speed_Dials_range : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // The speed dial properties below are configured for 140 km/h dial: True, False, 140, 0, 20, 0, 1, 0, 0, 0, -144, 0, 144 respectively: Speed in km/h (otherwise mph)Display speed unitMaximum Speed (upper boundary of the entire Speed Dial)Transition Speed (boundary between the two segments, if 0 – only segment 1 available)Speed interval between subsequent speed labels in segment 1Speed interval between subsequent speed labels in segment 2Number of short scale divisions between long scale divisions in segment 1Number of short scale divisions between long scale divisions in segment 2Number of long scale divisions between labels in segment 1Number of long scale divisions between labels in segment 2Position of Zero point (angle in grad, 0 grad at 12 o’clock, counting clockwise)Position of Transition Speed (angle, see above)Position of Maximum Speed (angle, see above)Test system is powered on.Cabin is activated.SoM is performed in SR mode, Level 1.

            // Call the TestCaseBase PreExecution
            base.PreExecution();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in OS mode, level 1.

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint

            EVC7_MMIEtcsMiscOutSignals.Initialise(this);
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.OnSight;

            EVC1_MMIDynamic.Initialise(this);

            // Set up from CCU to set range of speed dial ??

            /*
            Test Step 1
            Action: Driver drives the train forward passing BG1.Then, stop the train and acknowledge OS mode by pressing area C1
            Expected Result: DMI displays in OS mode, Level 1.Verify the following information,(1)   Use the log file to confirm that DMI received packet information EVC-1 with following variables,MMI_V_PERMITTED = 4166 (150km/h)MMI_V_TARGET = 4027 (145km/h)(2)   All basic speed hooks are not displays in sub-area B2
            Test Step Comment: (1) MMI_gen 6331 (partly: outside the Speed Dial’s maximum speed);(2) MMI_gen 6331 (partly: not to be shown);
            */

            // EVC7_MMIEtcsMiscOutSignals Send

            EVC1_MMIDynamic.MMI_V_PERMITTED = 2777;
            EVC1_MMIDynamic.MMI_V_TARGET = 694;
            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Indication_Status_Target_Speed_Monitoring;        // i.e. not 0, 4, 8, 12
            // ?? Send

            DmiActions.Drive_the_train_forward_pass_BG1_Then_press_an_acknowledgement_of_OS_mode_in_sub_area_C1(this);

            WaitForVerification("Acknowledgement of LS mode is requested. Press button to accept and then check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. All basic speed hooks are displayed in sub-area B2." + Environment.NewLine +
                                "2. The first hook is displayed overlapping the outer border of the speed dial in white  at 100 km/h." + Environment.NewLine +
                                "3. The second hook is displayed overlapping the outer border of the speed dial in medium-grey colour at 25 km/h." + Environment.NewLine +
                                "4. Sound ‘Sinfo’ is played once.");

            /*
            Test Step 2
            Action: Use the test script file 12_6_2_a.xml to send EVC-1 with,MMI_V_TARGET = 65535MMI_V_PERMITTED = 0
            Expected Result: Verify the following information,(1)   There is only white basic speed hook displays at 0 km/h
            Test Step Comment: (1) MMI_gen 6331 (partly: Target speed is outside the speed range determined by zero and the Speed Dial's maximum Speed, not to be shown);
            */
            // This value [65535] is outside the range of short
            EVC1_MMIDynamic.MMI_V_TARGET = 32767;   // largest +ve integer 
            // ?? Send

            WaitForVerification("Wait until the basic speed hooks overlap, then check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The Vperm hook (in white) overlays the Vtarget hook (medium-grey).");

            /*
            Test Step 3
            Action: Use the test script file 12_6_2_b.xml to send EVC-1 with,MMI_V_TARGET = 0MMI_V_PERMITTED = 65535
            Expected Result: Verify the following information,(1)   There is only medium grey basic speed hook displays at 0 km/h
            Test Step Comment: (1) MMI_gen 6331 (partly: Permitted speed is outside the speed range determined by zero and the Speed Dial's maximum Speed, not to be shown);
            */
            WaitForVerification("Press the 'Main' button. Press and hold 'Shunting' button at least 2 seconds." + Environment.NewLine +
                                "Release 'Shunting' button then check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in SH mode, level 1.");

            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.Shunting;
            // EVC7_MMIEtcsMiscOutSignals Send

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The first hook is displayed overlapping the outer border of the speed dial in white at 30 km/h.");

            /*
            Test Step 4
            Action: End of test
            Expected Result: 
            */


            return GlobalTestResult;
        }
    }
}
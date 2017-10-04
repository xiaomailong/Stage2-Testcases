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
    public class TC_12_6_2_Train_Speed : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // The speed dial properties below are configured for 140 km/h dial: True, False, 140, 0, 20, 0, 1, 0, 0, 0, -144, 0, 144 respectively: Speed in km/h (otherwise mph)Display speed unitMaximum Speed (upper boundary of the entire Speed Dial)Transition Speed (boundary between the two segments, if 0 – only segment 1 available)Speed interval between subsequent speed labels in segment 1Speed interval between subsequent speed labels in segment 2Number of short scale divisions between long scale divisions in segment 1Number of short scale divisions between long scale divisions in segment 2Number of long scale divisions between labels in segment 1Number of long scale divisions between labels in segment 2Position of Zero point (angle in grad, 0 grad at 12 o’clock, counting clockwise)Position of Transition Speed (angle, see above)Position of Maximum Speed (angle, see above)Test system is powered on.Cabin is activated.SoM is performed in SR mode, Level 1.
            // load config settings: TODO Check these
            // SPEED_UNIT_TYPE = 1
            // SPEED_UNIT_DISPLAY = 0
            // SPEED_DIAL_V_MAX = 140
            // SPEED_DIAL_V_TRANS = 0
            // SPEED_DIAL_V_NUMBER1 = 20
            // SPEED_DIAL_V_NUMBER2 = 0
            // SPEED_DIAL_N_SHORT_LINES1 = 1
            // SPEED_DIAL_N_SHORT_LINES2 = 0
            // SPEED_DIAL_N_LONG_LINES1 = 0
            // SPEED_DIAL_N_LONG_LINES2 = 0
            // SPEED_DIAL_ANGLE_V_0 = -144
            // SPEED_DIAL_ANGLE_V_TRANS = 0
            // SPEED_DIAL_ANGLE_V_MAX = 144

            // Call the TestCaseBase PreExecution
            base.PreExecution();
            DmiActions.Complete_SoM_L1_SR(this);
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

            // Set up from config to set range of speed dial ??

            /*
            Test Step 1
            Action: Driver drives the train forward passing BG1.Then, stop the train and acknowledge OS mode by pressing area C1
            Expected Result: DMI displays in OS mode, Level 1.Verify the following information,(1)   Use the log file to confirm that DMI received packet information EVC-1 with following variables,MMI_V_PERMITTED = 4166 (150km/h)MMI_V_TARGET = 4027 (145km/h)(2)   All basic speed hooks are not displays in sub-area B2
            Test Step Comment: (1) MMI_gen 6331 (partly: outside the Speed Dial’s maximum speed);(2) MMI_gen 6331 (partly: not to be shown);
            */

            EVC1_MMIDynamic.MMI_V_PERMITTED = 4166;
            EVC1_MMIDynamic.MMI_V_TARGET = 4027;            
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 0;

            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 259;
            EVC8_MMIDriverMessage.Send();

            DmiActions.ShowInstruction(this, "Acknowledge OS mode by pressing in area C1");

            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.OnSight;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in OS mode, Level 1." + Environment.NewLine +
                                "2. No basic speed hooks are displayed in sub-area B2.");

            /*
            Test Step 2
            Action: Use the test script file 12_6_2_a.xml to send EVC-1 with,MMI_V_TARGET = 65535MMI_V_PERMITTED = 0
            Expected Result: Verify the following information,(1)   There is only white basic speed hook displays at 0 km/h
            Test Step Comment: (1) MMI_gen 6331 (partly: Target speed is outside the speed range determined by zero and the Speed Dial's maximum Speed, not to be shown);
            */
            // These tests use speed values [65535] outside the range of short
            XML_12_6_2_a.Send(this);

            WaitForVerification("Acknowledgement of OS mode is requested. Press button to accept and then check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Only a white basic speed hook is displayed at 0 km/h.");

            /*
            Test Step 3
            Action: Use the test script file 12_6_2_b.xml to send EVC-1 with,MMI_V_TARGET = 0MMI_V_PERMITTED = 65535
            Expected Result: Verify the following information,(1)   There is only medium grey basic speed hook displays at 0 km/h
            Test Step Comment: (1) MMI_gen 6331 (partly: Permitted speed is outside the speed range determined by zero and the Speed Dial's maximum Speed, not to be shown);
            */
            XML_12_6_2_b.Send(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Only a medium-grey basic speed hook is displays at 0 km/h.");

            /*
            Test Step 4
            Action: End of test
            Expected Result: 
            */
            
            return GlobalTestResult;
        }
    }
}
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
    /// 17.3.10 Speed Pointer: Colour of speed pointer in TR mode and PT mode
    /// TC-ID: 12.3.10
    /// 
    /// This test case verifies the colour of speed pointer which display refer to received packet EVC-1 and EVC-7 for TR mode and PT mode.
    /// 
    /// Tested Requirements:
    /// MMI_gen 6299 (partly: TR mode, PT mode);
    /// 
    /// Scenario:
    /// 1.Drive the train forward pass BG1 at position 50m.BG1: Packet 12, 21 and 27 (Entering FS)
    /// 2.Continue to drive the train forward pass through the movement authority (300m) to entering TR mode.
    /// 3.Drive the train with specify speed. Then, verify that the colour of speed pointer is always red.
    /// 4.Ackownledge PT mode. Then, drive the train backward with specify speed and verify that the colour of speed pointer is always grey.
    /// 
    /// Used files:
    /// 12_3_10.tdg
    /// </summary>
    public class Speed_Pointer_Colour_of_speed_pointer_in_TR_mode_and_PT_mode : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // Test system is powered on.Cabin is activated.SoM is performed in SR mode, Level 1.

            // Call the TestCaseBase PreExecution
            base.PreExecution();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in PT mode, level 1

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint


            /*
            Test Step 1
            Action: Drive the train forward pass BG1
            Expected Result: DMI displays in FS mode, level 1
            */
            // Call generic Action Method
            DmiActions.Drive_the_train_forward_pass_BG1(this);
            // Call generic Check Results Method
            DmiExpectedResults.DMI_displays_in_FS_mode_level_1(this);


            /*
            Test Step 2
            Action: Force the train into TR mode by moving the train forward to position of EOA
            Expected Result: DMI displays in TR mode, level 1.The train is forced to stop, train speed is decreasing to 0 km/h.Verify the following information,(1)   Use the log file to confirm that DMI received the EVC-7 with variable OBU_TR_M_MODE = 7 (Trip)(2)   The speed pointer is always display in red colour
            Test Step Comment: (1) MMI_gen 6299 (partly: OBU_TR_M_MODE = 7);(2) MMI_gen 6299 (partly: colour of speed pointer, TR mode);
            */
            // Call generic Action Method
            DmiActions.Force_the_train_into_TR_mode_by_moving_the_train_forward_to_position_of_EOA(this);


            /*
            Test Step 3
            Action: Perform the following procedure,Press an acknowledgement in sub-area C1.Chage the train direction to ‘Reverse’Drive the train with speed = 40 km/h
            Expected Result: DMI displays in PT mode, level 1.Verify the following information,(1)   Use the log file to confirm that DMI received the EVC-7 with variable OBU_TR_M_MODE = 8 (Post trip)(2)   The speed pointer is always display in grey colour.Note: The train will be force to stop due to runaway movement is detect when the train moving back over 200m
            Test Step Comment: (1) MMI_gen 6299 (partly: OBU_TR_M_MODE = 8);(2) MMI_gen 6299 (partly: colour of speed pointer, PT mode);
            */


            /*
            Test Step 4
            Action: End of test
            Expected Result: 
            */


            return GlobalTestResult;
        }
    }
}
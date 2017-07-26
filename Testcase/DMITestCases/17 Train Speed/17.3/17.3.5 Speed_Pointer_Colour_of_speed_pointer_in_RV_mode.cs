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
    /// 17.3.5 Speed Pointer: Colour of speed pointer in RV mode
    /// TC-ID: 12.3.5
    /// 
    /// This test case verifies the colour of speed pointer which display refer to received packet EVC-1 while the train is running in each supervision status and speed monitoring for RV mode.
    /// 
    /// Tested Requirements:
    /// MMI_gen 6299 (partly: RV mode); 
    /// 
    /// Scenario:
    /// Drive the train forward pass BG1 at position 500mBG1: packet 12, 21, 27, 138 and 139 (Entering FS mode and reversing allowance area)
    /// 2.Enter RV mode, level 
    /// 1.Then, drive the train with specify speed and verify the display of speed pointer refer to received packet EVC-1.
    /// 
    /// Used files:
    /// 12_3_5.tdg
    /// </summary>
    public class Speed_Pointer_Colour_of_speed_pointer_in_RV_mode : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // Test system is power on.Cabin is activated.SoM is performed in SR mode, Level 1.

            // Call the TestCaseBase PreExecution
            base.PreExecution();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in RV mode, level 1

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint


            /*
            Test Step 1
            Action: Drive the train forward pass BG1.Then stop the train.
            Expected Result: DMI displays in FS mode, Level 1 with the ST06 symbol at sub-area C6.
            */

            /*
            Test Step 2
            Action: Perform the following procedure,Chage the train direction to reverseAcknowledge RV mode by pressing the symbol in sub-area C1.
            Expected Result: DMI displays in RV mode, Level 1.
            */

            /*
            Test Step 3
            Action: Drive the train with speed = 5 km/h.
            Expected Result: Verify the following information,(1)   Use the log file to confirm that DMI received the packet information EVC-1 and EVC-7 with following variables,(EVC-7) OBU_TR_M_MODE = 14 (Reversing)(EVC-1) MMI_M_WARNING = 0 (Status = NoS, Supervision = CSM)(EVC-1) MMI_V_PERMITTED = 139 (5km/h)(2)   The speed pointer display in grey colour
            Test Step Comment: (1) MMI_gen 6299 (partly: OBU_TR_M_MODE, MMI_M_WARNING, train speed in relation to permitted speed MMI_V_PERMITTED, RV mode in CSM supervision);(2) MMI_gen 6299 (partly: colour of speed pointer, RV mode in CSM supervision);
            */

            /*
            Test Step 4
            Action: Increase the train speed to 6 km/h
            Expected Result: Verify the following information,(1)   Use the log file to confirm that DMI received the packet information EVC-1 with the following condition,MMI_M_WARNING = 8 (Status = OvS, Supervision = CSM) while the value of MMI_V_TRAIN = 167 (6 km/h) which greater than MMI_V_PERMITTED(2)   The speed pointer display in orange colour
            Test Step Comment: (1) MMI_gen 6299 (partly: MMI_M_WARNING, train speed in relation to permitted speed MMI_V_PERMITTED, RV mode in CSM supervision);(2) MMI_gen 6299 (partly: colour of speed pointer, RV mode in CSM supervision);
            */

            /*
            Test Step 5
            Action: Increase the train speed to 10 km/h.Note: dV_warning_max is defined in chapter 3 of [SUBSET-026]
            Expected Result: Verify the following information,(1)   Use the log file to confirm that DMI received the packet information EVC-1 with the following condition,MMI_M_WARNING = 4 (Status = WaS, Supervision = CSM) while the value of MMI_V_TRAIN = 278 (10 km/h) which greater than MMI_V_PERMITTED but lower than MMI_V_INTERVENTION(2)   The speed pointer display in orange colour
            Test Step Comment: (1) MMI_gen 6299 (partly: MMI_M_WARNING, train speed in relation to permitted speed MMI_V_PERMITTED, RV mode in CSM supervision);(2) MMI_gen 6299 (partly: colour of speed pointer, RV mode in CSM supervision);
            */

            /*
            Test Step 6
            Action: Increase the train speed to 11 km/h.
            Expected Result: The train speed is force to decrease because of emergency brake is applied by ETCS onboard.Verify the following information,Before train speed is decreased(1)   Use the log file to confirm that DMI received the packet information EVC-1 with the following condition,MMI_M_WARNING = 12 (Status = IntS, Supervision = CSM) while the value of MMI_V_TRAIN = 306 (11 km/h) which greater than MMI_V_INTERVENTION(2)   The speed pointer display in red colourAfter train speed is decreased(3)   Use the log file to confirm that DMI received the packet information EVC-1 with the following condition,MMI_M_WARNING = 12 (Status = IntS, Supervision = CSM) while the value of MMI_V_TRAIN is lower than MMI_V_INTERVENTION(4)   The speed pointer display in grey colour
            Test Step Comment: (1) MMI_gen 6299 (partly: MMI_M_WARNING, train speed in relation to permitted speed MMI_V_PERMITTED, RV mode in CSM supervision);(2) MMI_gen 6299 (partly: colour of speed pointer, RV mode in CSM supervision);(3) MMI_gen 6299 (partly: MMI_M_WARNING, RV mode in CSM supervision);(4) MMI_gen 6299 (partly: colour of speed pointer, RV mode in CSM supervision);
            */

            /*
            Test Step 7
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
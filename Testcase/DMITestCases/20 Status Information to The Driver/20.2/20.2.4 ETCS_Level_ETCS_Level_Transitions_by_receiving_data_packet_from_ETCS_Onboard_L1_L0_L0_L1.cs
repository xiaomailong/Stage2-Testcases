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
    /// 20.2.4 ETCS Level: ETCS Level Transitions by receiving data packet from ETCS Onboard (L1->L0, L0->L1)
    /// TC-ID: 15.2.4
    /// 
    /// This test case verifes a replaced symbol which should be replaced immediately after driver acknowledged level transition acknowledgement symbol.
    /// 
    /// Tested Requirements:
    /// MMI_gen 9431 (partly: LE07 is used, LE06 is replace respectively LE07); MMI_gen 7025 (partly: 2nd bullet, #4); MMI_gen 1310 (partly:LE07);  MMI_gen 9430 (partly:LE06, LE10); MMI_gen 11470 (partly: Bit #6);
    /// 
    /// Scenario:
    /// Activate Cabin A.Perform SoM in SR mode, Level 1.Drive the train forward pass BG
    /// 1.Then, verifie the display information.BG1: Packet 41Press an acknowledment at area C
    /// 1.Then, verifie the display information.Drive the train forward pass BG
    /// 3.Then, verifie the display information.BG2: Pack 12, 21, 27 and 41
    /// 
    /// Used files:
    /// 15_2_4.tdg
    /// </summary>
    public class ETCS_Level_ETCS_Level_Transitions_by_receiving_data_packet_from_ETCS_Onboard_L1_L0_L0_L1 : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // System is power ON.

            // Call the TestCaseBase PreExecution
            base.PreExecution();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in FS mode, Level 1.

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint


            /*
            Test Step 1
            Action: Acivate cabin A
            Expected Result: DMI displays Driver ID window
            */
            // Call generic Check Results Method
            DmiExpectedResults.DMI_displays_Driver_ID_window(this);


            /*
            Test Step 2
            Action: Perform SoM in SR mode, Level 1
            Expected Result: DMI displays in SR mode Level 1
            */
            // Call generic Action Method
            DmiActions.Perform_SoM_in_SR_mode_Level_1(this);


            /*
            Test Step 3
            Action: Drive the train forward pass BG1
            Expected Result: DMI displays symbol LE07 in area C1 with flashing yellow frame.Verify the following information,(1)    Use the log file to confirm that DMI receives packet information EVC-8 with the following variables,MMI_Q_TEXT = 276MMI_Q_TEXT_CRITIRIA = 1MMI_N_TEXT = 1MMI_X_TEXT = 0(2)    Use the log file to confirm that DMI sends out packet [MMI_DRIVER_ACTION (EVC-152)] with the value of variable MMI_M_DRIVER_ACTION refer to sequence below,a)   MMI_M_DRIVER_ACTION = 6 (Ack level 0)
            Test Step Comment: (1) MMI_gen 7025 (partly: 2nd bullet, #4, Ack Level 0 transition); MMI_gen 1310 (partly:LE07); MMI_gen 9431 (partly: LE07);(2) MMI_gen 11470 (partly: Bit #6);
            */
            // Call generic Action Method
            DmiActions.Drive_the_train_forward_pass_BG1(this);


            /*
            Test Step 4
            Action: Press an area C1 for acknowledgement
            Expected Result: Verify the following information,The symbol LE06 is display in area C1 instead of symbol LE07 immediately.Use the log file to confirm that DMI receives packet information EVC-8 with the following variables,MMI_Q_TEXT = 276MMI_Q_TEXT_CRITIRIA = 3MMI_N_TEXT = 1MMI_X_TEXT = 0
            Test Step Comment: (1) MMI_gen 9431          (partly: The symbol LE06 is replace respectively LE07); MMI_gen 9430          (partly:LE06);(2) MMI_gen 7025 (partly: 2nd bullet, #4, non-Ack Level 0 transition);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(@"Press an area C1 for acknowledgement");


            /*
            Test Step 5
            Action: Drive the train pass a distance to level transition
            Expected Result: Level transition from Level 1 to Level 0.DMI displays symbol LE01 in area C8
            */
            // Call generic Action Method
            DmiActions.Drive_the_train_pass_a_distance_to_level_transition(this);


            /*
            Test Step 6
            Action: Drive the train forward pass BG3
            Expected Result: Verify the following information,The symbol LE10 is display in area C1.Use the log file to confirm that DMI receives packet information EVC-8 with the following variables,MMI_Q_TEXT = 276MMI_Q_TEXT_CRITIRIA = 3MMI_N_TEXT = 1MMI_X_TEXT = 1
            Test Step Comment: (1) MMI_gen 9430          (partly:LE10);(2) MMI_gen 7025 (partly: 2nd bullet, #4, non-Ack Level 1 transition);
            */
            // Call generic Action Method
            DmiActions.Drive_the_train_forward_pass_BG3(this);


            /*
            Test Step 7
            Action: Drive the train pass a distance to level transition
            Expected Result: Level transition from Level 0 to Level 1.DMI displays symbol LE03 in area C8
            */
            // Call generic Action Method
            DmiActions.Drive_the_train_pass_a_distance_to_level_transition(this);


            /*
            Test Step 8
            Action: End of test
            Expected Result: 
            */


            return GlobalTestResult;
        }
    }
}
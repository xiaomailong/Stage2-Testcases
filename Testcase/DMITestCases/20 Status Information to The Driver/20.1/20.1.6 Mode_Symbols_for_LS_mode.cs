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
    /// 20.1.6 Mode Symbols for LS mode
    /// TC-ID: 15.1.6
    /// 
    /// This test case verifies a display of ETCS Mode symbol (LS) in area B7 and LSSMA with LS01 symbol in area A1. Each symbol shall comply with [ERA] standard.
    /// 
    /// Tested Requirements:
    /// MMI_gen 11084 (partly: current ETCS mode); MMI_gen 110 (partly: MO21); MMI_gen 9436; MMI_gen 10489 (partly: LS mode, nevertheless evaluated, FS mode, OS mode); MMI_gen 9971; MMI_gen 9440; MMI_gen 9441; MMI_gen 9442; MMI_gen 1227 (partly: MO22); MMI_gen 11233 (partly: MO22); MMI_gen 11470 (partly: Bit # 13);
    /// 
    /// Scenario:
    /// Drive the train forward pass BG1 at position 100m.Then, verify the display information of MO22 symbol, MO21 symbol, LS01 symbol and the LSSMA.BG1: Packet 12, 21, 27 and 80 (Entering LS)Drive the train forward pass position 250m. After entered FS mode, verify that there is no LSSMA display on DMI.Drive the train forward pass BG2 at position 400m. Then, verify that  DMI updates LSSMA refer to the last received packet EVC-23 for OS mode.BG2: Packet 12, 21, 27 and 80 (Entering OS)
    /// 
    /// Used files:
    /// 15_1_6.tdg
    /// </summary>
    public class Mode_Symbols_for_LS_mode : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // Test system is powered onSoM is performed in SR mode, Level 1.

            // Call the TestCaseBase PreExecution
            base.PreExecution();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in OS mode, Level 1.

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint


            /*
            Test Step 1
            Action: Drive the train forward passing BG1
            Expected Result: Verify the following information,The symbol MO22 is displayed for Limited Supervision acknowledegement in sub-area C1.Use the log file to confirm that DMI receives packet information EVC-8 with the following value,MMI_Q_TEXT = 709MMI_Q_TEXT_CRITERIA = 1
            Test Step Comment: (1) MMI_gen 1227 (partly: MO22);                                          (2) MMI_gen 11233 (partly: MO22);
            */
            // Call generic Action Method
            DmiActions.Drive_the_train_forward_passing_BG1(this);


            /*
            Test Step 2
            Action: Press an acknowledgement of LS mode in sub-area C1 and stop the train
            Expected Result: DMI displays in LS mode, Level 1.Verify the following information,Use the log file to verify that DMI received the EVC-7 with [MMI_ETCS_MISC_OUT_SIGNALS.OBU_TR_M_MODE] = 12 in order to display the Limited Supervision symbol.The Limited Supervision symbol (MO21) is displayed in area B7.The symbol LS01 is displayed in sub-area A1.The number of LSSMA is displayed vertically and horizontally centered in sub-area A1.The number of LSSMA is overlay the LS01 symbol.The colour of LSSMA is grey.Use the log file to confirm that DMI received packet EVC-23 with variable MMI_V_LSSMA and the value of variable is same as displayed information in sub-area A1. Use the log file to confirm that DMI sends out packet [MMI_DRIVER_ACTION (EVC-152)] with the value of variable MMI_M_DRIVER_ACTION refer to sequence below,a)   MMI_M_DRIVER_ACTION = 13 (Ack of Limited Supervision mode)
            Test Step Comment: (1) MMI_gen 11084 (partly: current ETCS mode);                                 (2) MMI_gen 110 (partly: MO21);(3) MMI_gen 9441;(4) MMI_gen 9436; MMI_gen 9440 (partly: LSSMA shall be shown with a number, vertically and horizontally centered in A1);(5) MMI_gen 9442; MMI_gen 10489 (partly: LS mode);(6) MMI_gen 9440 (partly: grey);(7) MMI_gen 9440 (partly: packet EVC-23);(8) MMI_gen 11470 (partly: Bit # 13);
            */


            /*
            Test Step 3
            Action: Drive the train forward pass the position 250m.Then, stop the train
            Expected Result: DMI displays in FS mode, Level 1.Verify the following information,The LSSMA is not displayed in sub-area A1
            Test Step Comment: (1) MMI_gen 10489 (partly: FS);
            */


            /*
            Test Step 4
            Action: Drive the train forward pass BG2.Then, press an acknowledgement of OS mode in sub-area C1
            Expected Result: DMI displays in OS mode, Level 1.Verify the following information,Use the log file to confirm that DMI received packet EVC-23 with variable MMI_V_LSSMA and use it to display on sub-area A1.Note: The LSSMA is sub-area A1 will not display if MMI_V_LSSMA = 65535 (special value)
            Test Step Comment: (1) MMI_gen 9971; MMI_gen 10489 (partly: display, OS mode);
            */


            /*
            Test Step 5
            Action: End of test
            Expected Result: 
            */


            return GlobalTestResult;
        }
    }
}
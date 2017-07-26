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
    /// 20.1.3 Mode Symbols in Sub-Area B7 for OS, UN mode
    /// TC-ID: 15.1.3
    /// 
    /// This test case verifies the presentation of ETCS Mode displays as a symbol in sub-area C1 and B7 (for UN and OS mode) and a symbol in sub-area C7 (for EOA) . The symbol of each ETCS Mode shall comply with [ERA] standard.
    /// 
    /// Tested Requirements:
    /// MMI_gen 1227 (partly: MO08, MO17); MMI_gen 110 (partly: MO07, MO16); MMI_gen 11084 (partly: current ETCS mode); MMI_gen 11231; MMI_gen 11233 (partly: MO08 and MO 17); MMI_gen 11470 (partly: Bit # 0,3,4 and 34);
    /// 
    /// Scenario:
    /// Activate Cabin APerform SoM in UN mode Level 0 and verify the display information during mode acknowledgement.De-activate Cabin A. Then, activate Cabin A again and perform SoM in SR mode Level 1.Driver the train forward.Verify the display information when drive the train forward pass BG1 at 50m.BG1: packet 12, packet 21, packet 27 and packet 80.Acknowldege OS mode and verify the displays information.Stop the train.Open Override window.Press EOA button. Then, verify the displays information.
    /// 
    /// Used files:
    /// 15_1_3.tdg
    /// </summary>
    public class Mode_Symbols_in_Sub_Area_B7_for_OS__UN_mode : TestcaseBase
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
            // DMI displays in SR mode, Level 1

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint


            /*
            Test Step 1
            Action: Activate cabin A. 
            Expected Result: DMI displays in SB mode. The Driver ID window is displayed.
            */

            /*
            Test Step 2
            Action: Enter Driver ID and perform brake test.
            Expected Result: DMI displays Level window.
            */

            /*
            Test Step 3
            Action: Select and confirm Level 0.
            Expected Result: DMI displays Main window.(1)   Use the log file to confirm that DMI sends out packet [MMI_DRIVER_ACTION (EVC-152)] with the value of variable MMI_M_DRIVER_ACTION refer to sequence below,a)   MMI_M_DRIVER_ACTION = 34 (Level 0 selected)
            Test Step Comment: (1) MMI_gen 11470 (partly: Bit # 34);
            */

            /*
            Test Step 4
            Action: Press ‘Train data’ button.
            Expected Result: DMI displays Train data window.
            */

            /*
            Test Step 5
            Action: Enter and confirm value in each input field.Then, press ‘Yes’ button.
            Expected Result: DMI displays Train data validation window.
            */

            /*
            Test Step 6
            Action: Press ‘Yes’ button.Then, confirmed selected value by pressing an input field.
            Expected Result: DMI displays Train Running Number window.
            */

            /*
            Test Step 7
            Action: Enter and confirm Train running number.
            Expected Result: DMI displays Main window.
            */

            /*
            Test Step 8
            Action: Press ‘Start’ button.
            Expected Result: Verify the following information,The symbol MO17 is displayed for Unfitted mode acknowledegement in sub-area C1. Use the log file to confirm that DMI receives packet information EVC-8 with the following value,MMI_Q_TEXT = 264MMI_Q_TEXT_CRITERIA = 1
            Test Step Comment: (1) MMI_gen 1227 (partly: MO17);                                          (2) MMI_gen 11233 (partly: MO17);
            */

            /*
            Test Step 9
            Action: Acknowledge UN mode.
            Expected Result: Verify the following information,(1)    The symbol MO16 is displayed in sub-area B7. (2)     Use the log file to confirm that DMI received the EVC-7 with [MMI_ETCS_MISC_OUT_SIGNALS.OBU_TR_M_MODE] = 4 in order to display the Unfitted symbol.(3)     Use the log file to confirm that DMI sends out packet [MMI_DRIVER_ACTION (EVC-152)] with the value of variable MMI_M_DRIVER_ACTION refer to sequence below,          a)   MMI_M_DRIVER_ACTION = 4 (Ack of Unfitted mode)
            Test Step Comment: (1) MMI_gen 110 (partly: MO16);  (2) MMI_gen 11084 (partly: ETCS mode UN); (3) MMI_gen 11470 (partly: Bit # 4);                                               
            */

            /*
            Test Step 10
            Action: Perform the following procedure,De-activate Cabin A.Activate Cabin A.Perform SoM in SR mode, Level 1.
            Expected Result: DMI displays Default window in SR mode, Level 1.(1)   Use the log file to confirm that DMI sends out packet [MMI_DRIVER_ACTION (EVC-152)] with the value of variable MMI_M_DRIVER_ACTION refer to sequence below,      a)    MMI_M_DRIVER_ACTION = 3 (Ack of Staff Responsible mode)
            Test Step Comment: (1) MMI_gen 11470 (partly: Bit #3);                     
            */

            /*
            Test Step 11
            Action: Drive the train forward passing BG1.
            Expected Result: Verify the following information,The symbol MO08 is displayed for On sight acknowledegement in sub-area C1. Use the log file to confirm that DMI is receive packet information EVC-8 with the following value,MMI_Q_TEXT = 259MMI_Q_TEXT_CRITERIA = 1(3)    Use the log file to confirm that DMI sends out packet [MMI_DRIVER_ACTION (EVC-152)] with the value of variable MMI_M_DRIVER_ACTION refer to sequence below,a)   MMI_M_DRIVER_ACTION = 0 (Ack of On sight mode)
            Test Step Comment: (1) MMI_gen 1227 (partly: MO08);        (2) MMI_gen 11233 (partly: MO17ไ);(3) MMI_gen 11470 (partly: Bit # 0);                     
            */

            /*
            Test Step 12
            Action: Acknowledge OS mode.
            Expected Result: Verify the following information,(1)    The symbol MO07 is displayed in sub-area B7. (2)    Use the log file to confirm that DMI received the EVC-7 with [MMI_ETCS_MISC_OUT_SIGNALS.OBU_TR_M_MODE] = 1 in order to display the On-sight symbol.
            Test Step Comment: (1) MMI_gen 110 (partly:MO07);  (2) MMI_gen 11084 (partly: ETCS mode OS);                           
            */

            /*
            Test Step 13
            Action: Stop the train.Then, press ‘Over-ride’ button.
            Expected Result: When the train is stopped, EOA button is enabled.
            */

            /*
            Test Step 14
            Action: Press ‘EOA’ button.
            Expected Result: Verify the following information, (1)   The symbol MO03 is displayed for Override EOA symbol in sub-area C7.(2) Use the log DMI received packet information EVC-2 with variable MMI_M_OVERRIDE_EOA = 1.
            Test Step Comment: (1) MMI_gen 11231 (partly: MO03);(2) MMI_gen 11231 (partly: EVC-2);
            */

            /*
            Test Step 15
            Action: End of test.
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
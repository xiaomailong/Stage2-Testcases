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
    /// 23.4.3 Geographical Position: Additional requirements
    /// TC-ID: 18.4.3
    /// 
    /// 
    /// 
    /// Tested Requirements:
    /// MMI_gen 656; MMI_gen 2499;
    /// 
    /// Scenario:
    /// Drive the train forward pass BG
    /// 1.Then, press the symbol DR03 in sub-area G12 and verify the packet information EVC-101 with display information of geographical position.BG1: Packet 79 (Geographical Position Information)Note: Perform this scenario for every configuration cycle time.De-activate cabin A. Then, that state of sub-area G12 is not sensitive and no information display in this area.Activate cabin A again and verify the display of symbol DR03 in sub-area G12.Power off ATP-CU and verify the display of symbol DR03 in sub-area G12
    /// 
    /// Used files:
    /// 18_4_3.tdg
    /// </summary>
    public class Geographical_Position_Additional_requirements : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // Set the following tags name in configuration file (See the instruction in Appendix 1)GEOPOS_REQ_PERIOD = 0 (Send only one request via EVC-101)Test System is power on.SoM is performed in SR mode, Level 1. 

            // Call the TestCaseBase PreExecution
            base.PreExecution();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in SB mode, Level 1.

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint


            /*
            Test Step 1
            Action: Drive the train forward pass BG1
            Expected Result: The symbol ‘DR03’ displays in sub-area G12
            */
            // Call generic Action Method
            DmiActions.Drive_the_train_forward_pass_BG1(this);
            // Call generic Check Results Method
            DmiExpectedResults.The_symbol_DR03_displays_in_sub_area_G12(this);


            /*
            Test Step 2
            Action: Press ‘DR03’ symbol at sub-area G12
            Expected Result: Verify the following information,Use the log file to confirm that DMI sent out only one packet of EVC-101 with variable MMI_M_REQUEST = 8 (Geographical position request)
            Test Step Comment: (1) MMI_gen 656 (partly: press symbol DR03, cycle time is configured as 0);
            */


            /*
            Test Step 3
            Action: Perform the following procedure, Power off test systemSet the variable GEOPOS_REQ_PERIOD = 1 in configuration filePower on test systemPerform SoM in SR mode, Level 1
            Expected Result: DMI displays in SR mode, Level 1
            */
            // Call generic Check Results Method
            DmiExpectedResults.DMI_displays_in_SR_mode_Level_1(this);


            /*
            Test Step 4
            Action: Drive the train forward pass BG1
            Expected Result: The symbol ‘DR03’ displays in sub-area G12
            */
            // Call generic Action Method
            DmiActions.Drive_the_train_forward_pass_BG1(this);
            // Call generic Check Results Method
            DmiExpectedResults.The_symbol_DR03_displays_in_sub_area_G12(this);


            /*
            Test Step 5
            Action: Press  ‘DR03’ symbol at sub-area G12
            Expected Result: Verify the following information,Use the log file to confirm that DMI sent out packet of EVC-101 with variable MMI_M_REQUEST = 8 (Geographical position request) every 1 second.The display of geographical is update every 1 second refer to received packet information from ETCS.Note: Stopwatch is required
            Test Step Comment: (1) MMI_gen 656 (partly: cyclically transmit a request, configurable 1s);(2) Information under MMI_gen 656;
            */


            /*
            Test Step 6
            Action: Repeat action step 3-5 for the remaining configuration of cycle time (GEOPOS_REQ_PERIOD = 2 to 10)
            Expected Result: Verify the following information,Use the log file to confirm that DMI sent out packet of EVC-101 with variable MMI_M_REQUEST = 8 (Geographical position request) every X second.The display of geographical is update every X second refer to received packet informaion from ETCS.Note:X is configured cycle time refer to value of GEOPOS_REQ_PERIODStopwatch is required
            Test Step Comment: (1) MMI_gen 656 (partly: cyclically transmit a request, configurable 2-10s);(2) Information under MMI_gen 656;
            */


            /*
            Test Step 7
            Action: De-activate cabin A
            Expected Result: Verify the following information,The geographical position in sub-area G12 is removed
            Test Step Comment: (1) MMI_gen 2499 (partly: remove the function show geographical position, MMI_gen 242);
            */


            /*
            Test Step 8
            Action: Press at sub-area G12
            Expected Result: Verify the following information,The symbol DR03 is not display in sub-area G12.Use the log file to confirm that DMI did not sent out packet EVC-101
            Test Step Comment: (1) MMI_gen 2499 (partly: not display symbol DR03, MMI_gen 242);(2) MMI_gen 2499 (partly: not be sensitive, MMI_gen 242);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(@"Press at sub-area G12");


            /*
            Test Step 9
            Action: Activate cabin A
            Expected Result: The symbol ‘DR03’ displays in sub-area G12
            Test Step Comment: MMI_gen 2499 (partly: reset the default state of toggle to off, MMI_gen 242);
            */
            // Call generic Action Method
            DmiActions.Activate_Cabin_1(this);
            // Call generic Check Results Method
            DmiExpectedResults.The_symbol_DR03_displays_in_sub_area_G12(this);


            /*
            Test Step 10
            Action: Simulate loss-communication between ETCS onboard and DMI
            Expected Result: Verify the following information,(1)   The geographical position in sub-area G12 is removed
            Test Step Comment: (1) MMI_gen 2499 (partly: removal, MMI_gen 244);
            */
            // Call generic Action Method
            DmiActions.Simulate_loss_communication_between_ETCS_onboard_and_DMI(this);


            /*
            Test Step 11
            Action: Press at sub-area G12
            Expected Result: Verify the following information,(1)   The symbol DR03 is not display in sub-area G12.(2)   Use the log file to confirm that DMI did not sent out packet EVC-101
            Test Step Comment: (1) MMI_gen 2499 (partly: not display symbol DR03, MMI_gen 244);(2) MMI_gen 2499 (partly: not be sensitive, MMI_gen 244);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(@"Press at sub-area G12");


            /*
            Test Step 12
            Action: End of test
            Expected Result: 
            */


            return GlobalTestResult;
        }
    }
}
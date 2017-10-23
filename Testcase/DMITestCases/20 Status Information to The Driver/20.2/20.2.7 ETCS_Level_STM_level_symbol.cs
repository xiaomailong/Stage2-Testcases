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
    /// 20.2.7 ETCS Level: STM level symbol
    /// TC-ID: 15.2.6
    /// 
    /// This test case verifies the visualisation of mode symbol and level symbol of STM level. The mode and level symbol of STM level are displayed in sub-area B7 and C8. The visualisation of mode symbol and level synbol of STM level shall comply with [ERA] standard and [MMI-ETCS-gen].
    /// 
    /// Tested Requirements:
    /// MMI_gen 110 (partly: display ETCS Mode, MO19); MMI_gen 1227 (partly: STM); MMI_gen 1088 (partly: Bit #24);  MMI_gen 11470 (partly: Bit #10,28 and 38);
    /// 
    /// Scenario:
    /// Activate cabin A. Enter the Driver ID. Then select and confirm STM (TPWS) level Verify the mode and level of TPWS STM Symbol then continue to complete Start of Mission. 
    /// 
    /// Used files:
    /// N/A
    /// </summary>
    public class ETCS_Level_STM_level_symbol : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // Configure atpcu configuration file as following (See the instruction in Appendix 2)M_InstalledLevels = 31NID_NTC_Installe_0 = 20 (TPWS)System is power on.

            // Call the TestCaseBase PreExecution
            base.PreExecution();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in SN mode, level STM (TPWS)

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint


            /*
            Test Step 1
            Action: Activate cabin A
            Expected Result: DMI displays the default window. The Driver ID window is displayed
            */
            // Call generic Action Method
            DmiActions.Activate_Cabin_1(this);
            // Call generic Check Results Method
            DmiExpectedResults.Driver_ID_window_displayed(this);


            /*
            Test Step 2
            Action: Select ‘TPWS (STM)’ level
            Expected Result: Verify the following information:(1)    The confirmation announcement symbol of SN mode is displayed at sub-area C1 and the driver is required to acknowledge. (Please check MO20 symbol in ‘Comment’ column.)(2)     Use the log file to confirm that DMI sends out packet [MMI_DRIVER_ACTION (EVC-152)] with the value of variable MMI_M_DRIVER_ACTION refer to sequence below,a)   MMI_M_DRIVER_ACTION = 38 (Level NTC selected)
            Test Step Comment: (1) MMI_gen 1227 (Partly: STM);    (2) MMI_gen 11470 (partly: Bit #38);
            */


            /*
            Test Step 3
            Action: Confirm SN mode
            Expected Result: Verify the following information:(1)    DMI displays the symbol of TPWS STM level in sub-area C8.        The symbol of SN mode is displayed in sub-area B7. (see the example in ‘Comment’ column)(2)   Use the log file to confirm that DMI sends out packet [MMI_DRIVER_ACTION (EVC-152)] with the value of variable MMI_M_DRIVER_ACTION refer to sequence below,a)   MMI_M_DRIVER_ACTION = 10 (Ack level NTC)
            Test Step Comment: (1) MMI_gen 110 (partly: ETCS Mode, MO19); MMI_gen 1227 (partly: STM);  (2) MMI_gen 11470 (partly: Bit #10);
            */


            /*
            Test Step 4
            Action: Complete Start of Mission
            Expected Result: DMI displays in SN mode, level STM (TPWS)(1)     Use the log file to confirm that DMI receives packet EVC-30 with the value of following bit in variable MMI_Q_REQUEST_ENABLE_64,Bit #24 = 1 (End of data entry)(2)     Use the log file to confirm that DMI sends out packet [MMI_DRIVER_ACTION (EVC-152)] with the value of variable MMI_M_DRIVER_ACTION refer to sequence below,a) MMI_M_DRIVER_ACTION = 28 (Ack of SN mode)
            Test Step Comment: (1) MMI_gen 1088 (partly: Bit #24);  (2) MMI_gen 11470 (partly: Bit #28);
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
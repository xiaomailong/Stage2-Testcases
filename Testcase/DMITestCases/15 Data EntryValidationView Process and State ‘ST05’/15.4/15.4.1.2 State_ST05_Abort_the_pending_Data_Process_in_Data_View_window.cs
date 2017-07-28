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
    /// 15.4.1.2 State ‘ST05’: Abort the pending Data Process in Data View window
    /// TC-ID: 10.4.1.2
    /// 
    /// This test case verifies that the process of data view window in state ST05 is aborted by a received packet of different window type (i.e., data entry window) from ETCS onboard.
    /// 
    /// Tested Requirements:
    /// MMI_gen 5507 (partly: Data View window, abort an already pending data view process, received packet of different window from ETCS onboard);
    /// 
    /// Scenario:
    /// 1.Verify the display information when execute the test script files when open the windows below,Data view windowSystem Info windowSystem Version window
    /// 
    /// Used files:
    /// 10_4_1_2_a.xml, 10_4_1_2_b.xml
    /// </summary>
    public class State_ST05_Abort_the_pending_Data_Process_in_Data_View_window : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // Test system is powered onCabin is activeSoM is performed until level 1 is selected and confirmed.Main window is closed

            // Call the TestCaseBase PreExecution
            base.PreExecution();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in SB mode

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint


            /*
            Test Step 1
            Action: At the Default window, press ‘Data View’ button
            Expected Result: DMI displays Data View window
            */


            /*
            Test Step 2
            Action: Use the test script file 10_4_1_2_a.xml to send EVC-8 withMMI_Q_TEXT_CRITERIA = 3 MMI_Q_TEXT = 716
            Expected Result: The hourglass symbol ST05 is displayed at window title area
            */
            // Call generic Check Results Method
            DmiExpectedResults.The_hourglass_symbol_ST05_is_displayed_at_window_title_area();


            /*
            Test Step 3
            Action: Use the test script file 10_4_1_2_b.xml to send EVC-14 withMMI_X_DRIVER_ID = ‘4444’
            Expected Result: Verify the followin information,(1)     The Data View window is closed, DMI displays Driver ID window after received packet EVC-14
            Test Step Comment: (1) MMI_gen 5507 (partly: Data View window, abort an already pending data view process, received packet of different window from ETCS onboard);
            */


            /*
            Test Step 4
            Action: Perform the following procedure,At Driver ID window, press ‘close’ button.Open System Info windowRepeat action step 2-3
            Expected Result: Verify the followin information,(1)     The System Info window is closed, DMI displays Driver ID window after received packet EVC-14
            Test Step Comment: (1) MMI_gen 5507 (partly: System Info window, abort an already pending data view process, received packet of different window from ETCS onboard);
            */


            /*
            Test Step 5
            Action: Perform the following procedure,At Driver ID window, press ‘close’ button.Open System version windowRepeat action step 2-3
            Expected Result: Verify the followin information,(1)     The System version window is closed, DMI displays Driver ID window after received packet EVC-14
            Test Step Comment: (1) MMI_gen 5507 (partly: System version window, abort an already pending data view process, received packet of different window from ETCS onboard);
            */


            /*
            Test Step 6
            Action: End of test
            Expected Result: 
            */


            return GlobalTestResult;
        }
    }
}
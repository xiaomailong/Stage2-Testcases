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
    /// 15.4.1.1 State ‘ST05’: Abort the pending Data Process in Main window
    /// TC-ID: 10.4.1.1
    /// 
    /// This test case verifies that the process of data entry and validation window in state ST05 is aborted by a received packet of different window type (i.e., data view window) from ETCS onboard.
    /// 
    /// Tested Requirements:
    /// MMI_gen 5507 (partly: Main window, abort an already pending data entry and validation processes, received packet of different window from ETCS onboard);
    /// 
    /// Scenario:
    /// 1.Verify the display information when execute the test script files when open the windows below,Driver ID windowTrain running number windowLevel windowTrain Data windowTrain Data validation window
    /// 
    /// Used files:
    /// 10_4_1_1_a.xml, 10_4_1_1_b.xml
    /// </summary>
    public class State_ST05_Abort_the_pending_Data_Process_in_Main_window : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // Test system is powered onCabin is activeSoM is performed until level 1 is selected and confirmed.

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
            Action: At the Main window, press ‘Driver ID’ button
            Expected Result: DMI displays Driver ID window
            */
            // Call generic Check Results Method
            DmiExpectedResults.Driver_ID_window_displayed(this);


            /*
            Test Step 2
            Action: Use the test script file 10_4_1_1_a.xml to send EVC-8 withMMI_Q_TEXT_CRITERIA = 3 MMI_Q_TEXT = 716
            Expected Result: The hourglass symbol ST05 is displayed at window title area
            */
            // Call generic Check Results Method
            DmiExpectedResults.The_hourglass_symbol_ST05_is_displayed_at_window_title_area(this);


            /*
            Test Step 3
            Action: Use the test script file 10_4_1_1_b.xml to send EVC-24 withMMI_NID_ENGINE_1 = 1234MMI_M_BRAKE_CONFIG = 55MMI_M_AVAIL_SERVICES = 65535MMI_M_ETC_VER = 16755215
            Expected Result: Verify the followin information,(1)     The Driver ID window is closed, DMI displays System info window after received packet EVC-24
            Test Step Comment: (1) MMI_gen 5507 (partly: Driver ID window, abort an already pending data entry process, received packet of different window from ETCS onboard);
            */


            /*
            Test Step 4
            Action: Perform the following procedure,At System info window, press ‘close’ button.Open Train running number windowRepeat action step 2-3
            Expected Result: Verify the followin information,(1)    The Train Running Number window is closed, DMI displays System info window after received packet EVC-24
            Test Step Comment: (1) MMI_gen 5507 (partly: Train Running Number window, abort an already pending data entry process, received packet of different window from ETCS onboard);
            */


            /*
            Test Step 5
            Action: Perform the following procedure,At System info window, press ‘close’ button.Open Level windowRepeat action step 2-3
            Expected Result: Verify the followin information,(1)    The Level window is closed, DMI displays System info window after received packet EVC-24
            Test Step Comment: (1) MMI_gen 5507 (partly: Level  window, abort an already pending data entry process, received packet of different window from ETCS onboard);
            */


            /*
            Test Step 6
            Action: Perform the following procedure,At System info window, press ‘close’ button.Open Train data windowRepeat action step 2-3
            Expected Result: Verify the followin information,(1)    The Train data window is closed, DMI displays System info window after received packet EVC-24
            Test Step Comment: (1) MMI_gen 5507 (partly: Train Data Number window, abort an already pending data validation process, received packet of different window from ETCS onboard);
            */


            /*
            Test Step 7
            Action: Perform the following procedure,At System info window, press ‘close’ button.Open Train data windowEnter and confirm all value of an input fieldsPress on enabled ‘Yes’ button
            Expected Result: DMI displays Train data validation window
            */
            // Call generic Check Results Method
            DmiExpectedResults.DMI_displays_Train_data_validation_window(this);


            /*
            Test Step 8
            Action: Repeat action step 2-3
            Expected Result: Verify the followin information,(1)    The Train data validation window is closed, DMI displays System info window after received packet EVC-24
            Test Step Comment: (1) MMI_gen 5507 (partly: Train Data Number window, abort an already pending data entry process, received packet of different window from ETCS onboard);
            */


            /*
            Test Step 9
            Action: End of test
            Expected Result: 
            */


            return GlobalTestResult;
        }
    }
}
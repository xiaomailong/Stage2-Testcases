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
    /// 38.2 NTC System Status Messages
    /// TC-ID: 35.2
    /// 
    /// This test case verifies the display of NTC system status messages.
    /// 
    /// Tested Requirements:
    /// MMI_gen 9522 (partly: table 77);
    /// 
    /// Scenario:
    /// Perform Start of Mission to ATB STM until train data entry Driver the train forward then verify that the text message “Runaway movement” displays in sub-area E5Turn off STM Simulator and complete start of mission then verify that the text message “ATB failed” and “ATB is not available” display in sub-area E5Restart OTE and STM SimulatorPerform start of mission to PLZB STM until train data entry completely. Verify that PLZB specific train data is requested 
    /// 
    /// Used files:
    /// N/A
    /// </summary>
    public class NTC_System_Status_Messages : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // System is power offConfigure atpcu configuration file as following (See the instruction in Appendix 2)M_InstalledLevels = 63NID_NTC_Installe_0 = 1 (ATB)NID_NTC_Installe_1 = 9 (PLZB)Q_CustomConfig = 3
            
            // Call the TestCaseBase PreExecution
            base.PreExecution();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in PLZB STM mode

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint

            
            /*
            Test Step 1
            Action: Power on the system and activate the cabin
            Expected Result: DMI displays Driver ID window in SB mode
            */
            // Call generic Action Method
            DmiActions.Power_on_the_system_and_activate_the_cabin();
            // Call generic Check Results Method
            DmiExpectedResults.DMI_displays_Driver_ID_window_in_SB_mode();
            
            
            /*
            Test Step 2
            Action: Perform the following action:Enter Driver IDskip the brake testSelect and comfirm ATB STMSelect Train data
            Expected Result: DMI displays Train data entry window
            */
            
            
            /*
            Test Step 3
            Action: Drive the train forward
            Expected Result: Service brake is applied.The text message “Runaway movement” displays in sub-area E5
            Test Step Comment: MMI_gen 9522 (partly: table 77, Runaway movement);
            */
            // Call generic Action Method
            DmiActions.Drive_the_train_forward();
            
            
            /*
            Test Step 4
            Action: Release service brake and complete train data enrty
            Expected Result: DMI displays train running number window
            */
            // Call generic Check Results Method
            DmiExpectedResults.DMI_displays_train_running_number_window();
            
            
            /*
            Test Step 5
            Action: Turn off STM Simulator and then complete start of mission
            Expected Result: The text message “ATB failed” and “ATB is not avaliable “display in sub-area E5
            Test Step Comment: MMI_gen 9522 (partly: table 77, failed and is not available);
            */
            
            
            /*
            Test Step 6
            Action: Restart OTE and STM Simulator
            Expected Result: OTE and STM Simulator are started
            */
            
            
            /*
            Test Step 7
            Action: Start up PZLB STM and then ATPCU
            Expected Result: DMI displays Driver ID window in SB mode
            */
            // Call generic Check Results Method
            DmiExpectedResults.DMI_displays_Driver_ID_window_in_SB_mode();
            
            
            /*
            Test Step 8
            Action: Perform start of mission to PLZB STM until train data entry completely
            Expected Result: DMI display PLZB specific data window
            Test Step Comment: MMI_gen 9522 (partly: table 77, needed data);
            */
            
            
            /*
            Test Step 9
            Action: Enter and confirm PLZB specific data
            Expected Result: DMI displays train running number window
            */
            // Call generic Check Results Method
            DmiExpectedResults.DMI_displays_train_running_number_window();
            
            
            /*
            Test Step 10
            Action: Complete start of mission
            Expected Result: DMI displays in PLZB STM mode
            */
            // Call generic Action Method
            DmiActions.Complete_start_of_mission();
            // Call generic Check Results Method
            DmiExpectedResults.DMI_displays_in_PLZB_STM_mode();
            
            
            /*
            Test Step 11
            Action: End of test
            Expected Result: 
            */
            

            return GlobalTestResult;
        }
    }
}

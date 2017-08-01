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
    /// 17.5.3 Colouring Scheme of Circular Speed Gauge (FS mode for RSM Speed Monitoring)
    /// TC-ID: 12.5.3
    /// 
    /// This test case verifies the display of CSG according to received packet information EVC-1 and EVC-7 including with sound played when Warning Status is active.
    /// 
    /// Tested Requirements:
    /// MMI_gen 972 (partly: OBU_TR_M_MODE , MMI_V_RELEASE, RSM); MMI_gen 6310 (partly: mode, release speed); MMI_gen 5902; MMI_gen 1182 (partly: Vrelease);
    /// 
    /// Scenario:
    /// 1.Drive the train forward with specified speed. Then verify the display of CSG refer to received packet information EVC-7 and EVC-1.
    /// 
    /// Used files:
    /// 12_5_3.tdg
    /// </summary>
    public class Colouring_Scheme_of_Circular_Speed_Gauge_FS_mode_for_RSM_Speed_Monitoring : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // System is power on.Cabin is activated.SoM is performed in SR mode, Level 1.

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
            Action: Drive the train forward pass BG1 with speed = 30km/h
            Expected Result: DMI displays in FS mode, Level 1.Verify the following information,(1)    Use the log file to confirm that DMI received packet EVC-7 with variable OBU_TR_M_MODE = 0 (Full Supervision mode).(2)   Use the log file to confirm that DMI received packet EVC-1 with following variables, MMI_M_WARNING = 3 (Status=IndS, Supervision=RSM).MMI_V_RELEASE =  1388 (50 km/h)(3)   All section of CSG is yellow colour
            Test Step Comment: (1) MMI_gen 972 (partly: OBU_TR_M_MODE); MMI_gen 6310 (partly: mode);(2) MMI_gen 972 (partly: MMI_V_RELEASE); MMI_gen 6310 (partly: release speed); MMI_gen 5902 (partly: MMI_M_WARNING = 3);(3) MMI_gen 972 (partly: FS mode, RSM, IndS,  Vtarget <= CSG <= Vperm); MMI_gen 1182 (partly: Vrelease);
            */
            // Call generic Action Method
            DmiActions.Drive_the_train_forward_pass_BG1_with_speed_30kmh(this);


            /*
            Test Step 2
            Action: Continue to drive the train forward with speed = 51 km/h
            Expected Result: Verify the following information,(1)   Use the log file to confirm that DMI received packet EVC-1 with following variables, MMI_M_WARNING = 15 (Status=IntS and Inds, Supervision=RSM).(2)   All section of CSG is yellow colour
            Test Step Comment: (1) MMI_gen 972 (partly: MMI_V_RELEASE); MMI_gen 6310 (partly: release speed); MMI_gen 5902 (partly: MMI_M_WARNING = 15);(2) MMI_gen 972 (partly: FS mode, RSM, IntS, Vtarget <= CSG <= Vperm); MMI_gen 1182 (partly: Vrelease);
            */


            /*
            Test Step 3
            Action: End of test
            Expected Result: 
            */


            return GlobalTestResult;
        }
    }
}
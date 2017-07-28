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
    /// 17.5.2 Colour Scheme of Circular Speed Gauge (TSM of Speed Monitoring)
    /// TC-ID: 12.5.2
    /// 
    /// This test case verifies the display of CSG according to received packet information EVC-1 and EVC-7.
    /// 
    /// Tested Requirements:
    /// MMI_gen 972 (partly: OBU_TR_M_MODE , MMI_V_TARGET, TSM); MMI_gen 6310 (partly: mode, target speed); MMI_gen 1182 (partly: Vtarget);
    /// 
    /// Scenario:
    /// 1.Drive the train forward with specified speed. Then verify the display of CSG refer to received packet information EVC-7 and EVC-1.
    /// 
    /// Used files:
    /// 12_5_2.tdg
    /// </summary>
    public class Colour_Scheme_of_Circular_Speed_Gauge_TSM_of_Speed_Monitoring : TestcaseBase
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
            Expected Result: DMI displays in FS mode, Level 1.Verify the following information,(1)    Use the log file to confirm that DMI received packet EVC-7 with variable OBU_TR_M_MODE = 0 (Full Supervision mode).(2)   Use the log file to confirm that DMI received packet EVC-1 with following variables, MMI_M_WARNING = 11 (Status=Nos, Supervision=TSM).MMI_V_TARGET =  278 (10 km/h)(3)   At range 0-10km/h, CSG is dark-grey colour.(4)   at range above 11km/h, CSG is white colour
            Test Step Comment: (1) MMI_gen 972 (partly: OBU_TR_M_MODE); MMI_gen 6310 (partly: mode);(2) MMI_gen 972 (partly: MMI_V_TARGET); MMI_gen 6310 (partly: target speed); (3) MMI_gen 972 (partly: FS mode, TSM, NoS,  0 <= CSG <= Vtarget); MMI_gen 1182 (partly: Vrelease);(3) MMI_gen 972 (partly: FS mode, TSM, NoS,  Vtarget <= CSG <= Vpermi);
            */
            // Call generic Action Method
            DmiActions.Drive_the_train_forward_pass_BG1_with_speed_30kmh();


            /*
            Test Step 2
            Action: End of test
            Expected Result: 
            */


            return GlobalTestResult;
        }
    }
}
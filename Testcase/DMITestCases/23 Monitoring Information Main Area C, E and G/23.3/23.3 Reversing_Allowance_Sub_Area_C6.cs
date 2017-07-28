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
    /// 23.3 Reversing Allowance: Sub-Area C6
    /// TC-ID: 18.3
    /// 
    /// This test case verifies the display of ‘Reversing Allowance’ on DMI.
    /// 
    /// Tested Requirements:
    /// MMI_gen 7485; MMI_gen 11470 (partly: Bit # 5);
    /// 
    /// Scenario:
    /// 1.Activate Cabin A and perform SoM to SR mode Level 1.
    /// 2.Drive train forward    Pass BG1 at 100m:    DMI changes from SR mode to FS mode.         Packet 12: L_ENDSECTION = 3000 m         packet 21: G_A = 0         packet 27: V_STATIC =  160 km/h     Pass BG2 at 200m:         packet 138: D_STARTREVERSE = 0 m                             L_REVERSEAREA = 200 m         Packet 139: D_REVERSE = 200 m                             V_REVERSE = 20 km/h
    /// 3.Stop the train. Then, verify the display information.
    /// 4.Change train direction to ‘Reverse’. Then, verify the display information.
    /// 
    /// Used files:
    /// 18_3.tdg
    /// </summary>
    public class Reversing_Allowance_Sub_Area_C6 : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // Test system is powered on.Activate Cabin A.SoM is completed in SR mode, Level 1.

            // Call the TestCaseBase PreExecution
            base.PreExecution();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in RV mode, level 1.

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint


            /*
            Test Step 1
            Action: Drive the train forward passing BG1
            Expected Result: DMI changes from SR mode to FS mode, Level 1
            */
            // Call generic Action Method
            DmiActions.Drive_the_train_forward_passing_BG1();


            /*
            Test Step 2
            Action: Drive the train forward passing BG2.Then, stop the train
            Expected Result: Verify the following information,Use the log file to confirm that DMI receives packet information [MMI_DRIVER_MESSAGE (EVC-8)] with variable [MMI_DRIVER_MESSAGE (EVC-8)].MMI_Q_TEXT = 286The symbol ST06 is displayed in sub-area C6
            Test Step Comment: (1) MMI_gen 7485 (partly: received packet EVC-8);(2) MMI_gen 7485        (partly: ST06);
            */


            /*
            Test Step 3
            Action: Change the train direction to ‘Reverse’
            Expected Result: An acknowledgement of ‘Reversing’ mode is displayed in sub-area C1
            */


            /*
            Test Step 4
            Action: Acknowledges ‘Reversing’ mode by pressing at area C1
            Expected Result: DMI displays in RV mode.The symbol MO14 is displayed in sub-area B7.Use the log file to confirm that DMI sends out packet [MMI_DRIVER_ACTION (EVC-152)] with the value of variable MMI_M_DRIVER_ACTION refer to sequence below,a)   MMI_M_DRIVER_ACTION = 5 (Ack of Reversing mode)
            Test Step Comment: MMI_gen 11470 (partly: Bit # 5);
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
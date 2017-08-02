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
    /// 27.23 Switchable train data entry
    /// TC-ID: 22.23
    /// 
    /// This test case verifies the displays information of ‘switch’ button including with packet sending when the button is pressed. The switchable train data entry shall comply with [ERA-ERTMS] standard and [MMI-ETCS-gen].
    /// 
    /// Tested Requirements:
    /// MMI_gen 8095; MMI_gen 8098; MMI_gen 8099 (partly: bottom right corner); MMI_gen 8096; MMI_gen 8097; MMI_gen 9402 (partly:  MMI_M_ALT_DEM, switchable);
    /// 
    /// Scenario:
    /// Open Train data window. Then, verify the location and label of ‘switch’ button.Press ‘switch’ button. Then, verify that label of ‘switch’ button is changed and DMI send out packet information EVC-101.
    /// 
    /// Used files:
    /// N/A
    /// </summary>
    public class Switchable_train_data_entry : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // Test system is powered on.Cabin is activated.SoM is performed until Level 1 is selcted and confirmed.

            // Call the TestCaseBase PreExecution
            base.PreExecution();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in SB mode, level 1.

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint


            /*
            Test Step 1
            Action: Press ‘Train data’ button
            Expected Result: The Train data window is displayed as  Flexible train data entry or Fixed train data entry.Verify the following information,The Train data window is presented an enabled ‘switch’ button.The ‘switch’ button is located in the bottom right corner of D/F/G area.The label of ‘switch’ button is displayed correctly refer to the type of displayed window as follows,Flexible Train DataThe label of switch button is presented with text ‘Select type’. Fixed Train DataThe label of switch button is presented with text ‘Enter data’.Use the log file to confirm that DMI receives packet EVC-6 with variable MMI_M_ALT_DEM = 1 (switchable)
            Test Step Comment: (1) MMI_gen 8095;   (2) MMI_gen 8098; MMI_gen 8099 (partly: bottom right corner);(3) MMI_gen 8097; (4) MMI_gen 9402 (partly:  MMI_M_ALT_DEM, switchable);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press ‘Train data’ button");


            /*
            Test Step 2
            Action: Press switch button
            Expected Result: Verify the following information,Use the log file to confirm that DMI send out packet EVC-101 [MMI_DRIVER_REQUEST] with variable MMI_M_REQUEST = 59 (Switch).The label of ‘switch’ button is changed correctly refer to the type of displayed window as follows,Flexible Train DataThe label of switch button is presented with text ‘Select type’. Fixed Train DataThe label of switch button is presented with text ‘Enter data’
            Test Step Comment: (1) MMI_gen 8096;  (2) MMI_gen 8097;
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
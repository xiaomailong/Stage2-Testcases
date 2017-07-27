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
    /// 25.5 Driver’s Action: RBC Contact windows
    /// TC-ID: 20.5
    /// 
    /// This test case verify that DMI sends values of [MMI_DRIVER_REQUEST (EVC-101).MMI_M_REQUEST] correctly when a driver presses on each button in RBC Contact window.
    /// 
    /// Tested Requirements:
    /// MMI_gen 151 (partly: MMI_M_REQUEST = 56, 61, 28, 33, 39, 57);
    /// 
    /// Scenario:
    /// 1.Perform the specified action (e.g. open/close window, press an acknowledgement). Then, verify the value in packet EVC-101 refer to each action.
    /// 2.Open RBC Data window. Then, enter and confirm the value in each input field.
    /// 3.Open Level window to re-enter and confirm level 
    /// 2.Then, press the close button at RBC contact window and verify the value in packet EVC-101.
    /// 4.Re-open RBC contact window. Then, press the 'Contact last RBC' button. and verify the value in packet EVC-101.
    /// 
    /// Used files:
    /// 20_5.utt
    /// </summary>
    public class Drivers_Action_RBC_Contact_windows : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // Test system is powered on.Cabin is activated.SoM is performed until level 2 is selected and confirmed.
            
            // Call the TestCaseBase PreExecution
            base.PreExecution();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in SB mode, Level 2

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint

            
            /*
            Test Step 1
            Action: Perform the following procedure,a)   Press and hold the ‘Radio Network ID’ button at least 2 seconds. Then, release the pressed button.b)  Press the ‘Close’ button.c)   Press the ‘Enter RBC Data’ button.d)   Press the ‘Close’ button
            Expected Result: DMI displays RBC Contact window.Verify the following information(1)   Use the log file to confirm that DMI sends out packet [MMI_DRIVER_REQUEST (EVC-101)] with the value of variable MMI_M_REQUEST refer to sequence below,a)   MMI_M_REQUEST = 56 (Start Network ID)b)   MMI_M_REQUEST = 61 (Exit RBC Network ID)c)   MMI_M_REQUEST = 28 (Start RBC Data Entry)d)   MMI_M_REQUEST = 33 (Exit RBC Data Entry)Note: The sequence of MMI_M_REQUEST value are consistent with step of each action.(2)   When the button is pressed in each action, the window of pressed button is closed
            Test Step Comment: (1) MMI_gen 151 (partly: MMI_M_REQUEST = 56, 61, 28, 33) ;(2) MMI_gen 151 (partly: close opened menu);
            */
            
            
            /*
            Test Step 2
            Action: Perform the following procedure,Press the ‘Enter RBC Data’ button.Enter the value of an input fields as follows,RBC ID = 6996969RBC Phone = 0031840880100Press 'Yes' button
            Expected Result: DMI displays Main window
            */
            // Call generic Check Results Method
            DmiExpectedResults.DMI_displays_Main_window();
            
            
            /*
            Test Step 3
            Action: Perform the following procedure,Press the ‘Level’ button.Select and confirm ‘Level 2’At the RBC Contact window, press ‘Close’ button
            Expected Result: Verify the following information(1)   Use the log file to confirm that DMI sends out packet [MMI_DRIVER_REQUEST (EVC-101)] with the value of variable MMI_M_REQUEST = 39 (Exit RBC contact).(2)   The RBC Contact window is closed, DMI displays Main window
            Test Step Comment: (1) MMI_gen 151 (partly: MMI_M_REQUEST = 39);(2) MMI_gen 151 (partly: close opened menu);
            */
            
            
            /*
            Test Step 4
            Action: Perform the following procedure,Press the ‘Level’ button.Select and confirm ‘Level 2’At the RBC Contact window, press ‘Contact last RBC’ button
            Expected Result: DMI displays Main window.Verify the following information(1)   Use the log file to confirm that DMI sends out packet [MMI_DRIVER_REQUEST (EVC-101)] with the value of variable MMI_M_REQUEST = 57 (Contact last RBC).(2)   The RBC Contact window is closed, DMI displays Main window
            Test Step Comment: (1) MMI_gen 151 (partly: MMI_M_REQUEST = 57);(2) MMI_gen 151 (partly: close opened menu);
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

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
    /// 36.2 The relationship between parent and child windows (2)
    /// TC-ID: 33.3
    /// 
    /// This test case verifies the relationship between parent and child window when the driver presses ‘Close’ button in each window. The relationship between parent and child windows shall comply with [ERA-ERTMS] standard and [MMI-ETCS-gen].
    /// 
    /// Tested Requirements:
    /// MMI_gen 8785 (partly: Radio network ID window, RBC data window, RBC contact window, Data view window);
    /// 
    /// Scenario:
    /// Perform the following actions for each window in note below to verify the display of parent window. Open the specified window.Press ‘Close’ button to verify the display of parent window.Note: Perform the action for the following windows,Radio Network ID windowRBC data windowRBC contact windowData view window
    /// 
    /// Used files:
    /// 33_3.utt
    /// </summary>
    public class The_relationship_between_parent_and_child_windows_2 : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // Test System is power on.Cabin is activated.Driver ID is entered and Brake test is performed.Level 2 is selected and confirmed.
            
            // Call the TestCaseBase PreExecution
            base.PreExecution();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays  in SB mode, Level 2

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint

            
            /*
            Test Step 1
            Action: Perform the following procedure,Press and hold ‘Radio Network ID’ button at least 2 second.Release the pressed area
            Expected Result: DMI displays Radio Network ID window
            */
            
            
            /*
            Test Step 2
            Action: Press ‘Close’ button
            Expected Result: DMI displays RBC contact window
            Test Step Comment: MMI_gen 8785 (partly: Radio Network ID window);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(@"Press ‘Close’ button");
            // Call generic Check Results Method
            DmiExpectedResults.DMI_displays_RBC_contact_window();
            
            
            /*
            Test Step 3
            Action: Press ‘Enter RBC data’ button
            Expected Result: DMI displays RBC data window
            */
            // Call generic Check Results Method
            DmiExpectedResults.DMI_displays_RBC_data_window();
            
            
            /*
            Test Step 4
            Action: Press ‘Close’ button
            Expected Result: DMI displays RBC contact window
            Test Step Comment: MMI_gen 8785 (partly: RBC data window);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(@"Press ‘Close’ button");
            // Call generic Check Results Method
            DmiExpectedResults.DMI_displays_RBC_contact_window();
            
            
            /*
            Test Step 5
            Action: Perform the following procedure,Press ‘Enter RBC data’ button.Enter and confirm the following value,RBC ID = 6996969RBC Phone number = 0031840880100
            Expected Result: DMI displays Main window
            */
            // Call generic Check Results Method
            DmiExpectedResults.DMI_displays_Main_window();
            
            
            /*
            Test Step 6
            Action: Perform the following procedure,Press ‘Level’ button.Select and confirm Level 2
            Expected Result: DMI displays RBC contact window
            */
            // Call generic Check Results Method
            DmiExpectedResults.DMI_displays_RBC_contact_window();
            
            
            /*
            Test Step 7
            Action: Press ‘Close’ button
            Expected Result: DMI displays Main window
            Test Step Comment: MMI_gen 8785 (partly: RBC contact window);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(@"Press ‘Close’ button");
            // Call generic Check Results Method
            DmiExpectedResults.DMI_displays_Main_window();
            
            
            /*
            Test Step 8
            Action: Perform the following procedure,Press ‘Close’ button.Press ‘Data view’ button
            Expected Result: DMI displays Data view window
            */
            
            
            /*
            Test Step 9
            Action: Press ‘Close’ button
            Expected Result: DMI displays Default window
            Test Step Comment: MMI_gen 8785 (partly: Data view window);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(@"Press ‘Close’ button");
            // Call generic Check Results Method
            DmiExpectedResults.DMI_displays_Default_window();
            
            
            /*
            Test Step 10
            Action: End of test
            Expected Result: 
            */
            

            return GlobalTestResult;
        }
    }
}

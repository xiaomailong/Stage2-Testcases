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
    /// 37.1.2 Flexible Train data entry
    /// TC-ID: 34.1.2
    /// 
    /// This test case verifies the display information of window covering the total grid array for Flexible Train data windows when driver enter ‘valid’ value.Moreover, this test case also confirm result of value(s) stored onboard replacing informaiton which occur only when driver press the ‘Yes’ button at Train data validation window.
    /// 
    /// Tested Requirements:
    /// MMI_gen 8863 (partly: Exception); MMI_gen 8865 (partly: Exception 1);
    /// 
    /// Scenario:
    /// Activate Cabin AEnter Driver ID and perform brake test.Select and confirm Level 1.Open Train data window.Modifies and confirm each value of input field as specified value.Close and re-open Train data window again. Then, verify that all train data are not changed Modifies all input fields as specified value. Then, press ‘Yes’ button to enter Train data validation window.Select and confirm ‘No’ button at Train data validation window. Then, verify that all train data are not changed.Modifies all input fields as specified value. Then, press ‘Yes’ button to enter Train data validation window.Select and confirm ‘Yes’ button at Train data validation windowOpen Train data window and verify that all train data are changedClose Train data window.
    /// 
    /// Used files:
    /// N/A
    /// </summary>
    public class Flexible_Train_data_entry : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // System is power on

            // Call the TestCaseBase PreExecution
            base.PreExecution();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in SB mode, level 1

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint


            /*
            Test Step 1
            Action: Perform the following procedure,Activate Cabin AEnter Driver ID and perform brake testSelect and confirm Level 1
            Expected Result: DMI displays Main window
            */
            // Call generic Action Method
            DmiActions
                .Perform_the_following_procedure_Activate_Cabin_AEnter_Driver_ID_and_perform_brake_testSelect_and_confirm_Level_1();
            // Call generic Check Results Method
            DmiExpectedResults.DMI_displays_Main_window();


            /*
            Test Step 2
            Action: Press ‘Train data’ button
            Expected Result: DMI displays Train data window.Note: Please memo the value of each input field to be compare with action step 4
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(@"Press ‘Train data’ button");


            /*
            Test Step 3
            Action: Modify and confirm an valid value for each following input field,Train category = ‘TILT1’Train length = ‘4000’Brake percentage = ‘100’Maximum speed = ‘120’Axel load category = ‘B1’Airtight = ‘No’Loading gauge = ‘Out of GC’
            Expected Result: Verifies that the value of each input field are changed refer to specifies entered data
            */


            /*
            Test Step 4
            Action: Press ‘Close’ button.Then, select Train data button
            Expected Result: DMI displays Train data window.Verifies the displayed values of each input field are same as action step No.2
            Test Step Comment: MMI_gen 8865 (partly: Exception 1);
            */


            /*
            Test Step 5
            Action: Repeat action step 3.Then, press ‘Yes’ button
            Expected Result: DMI displays Train data validation window
            */
            // Call generic Action Method
            DmiActions.Repeat_action_step_3_Then_press_Yes_button();
            // Call generic Check Results Method
            DmiExpectedResults.DMI_displays_Train_data_validation_window();


            /*
            Test Step 6
            Action: Select and confirm ‘No’ button
            Expected Result: DMI displays Train data window.Verifies the displayed values of each input field are different from entered data in step 3
            Test Step Comment: MMI_gen 8865 (partly: Exception 1);
            */
            // Call generic Action Method
            DmiActions.Select_and_confirm_No_button();


            /*
            Test Step 7
            Action: Repeat action step 3.Then, press ‘Yes’ button
            Expected Result: DMI displays Train data validation window
            */
            // Call generic Action Method
            DmiActions.Repeat_action_step_3_Then_press_Yes_button();
            // Call generic Check Results Method
            DmiExpectedResults.DMI_displays_Train_data_validation_window();


            /*
            Test Step 8
            Action: Select and confirm ‘Yes’ button in Train data validation window
            Expected Result: DMI displays Main window
            */
            // Call generic Check Results Method
            DmiExpectedResults.DMI_displays_Main_window();


            /*
            Test Step 9
            Action: Select Train data button
            Expected Result: DMI displays Train data window.Verifies the displayed values of each input field are changed refer to entered data in step 3
            Test Step Comment: MMI_gen 8863 (partly: Exception);
            */
            // Call generic Action Method
            DmiActions.Select_Train_data_button();


            /*
            Test Step 10
            Action: Press ‘Close’ button
            Expected Result: DMI displays Main window
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(@"Press ‘Close’ button");
            // Call generic Check Results Method
            DmiExpectedResults.DMI_displays_Main_window();


            /*
            Test Step 11
            Action: End of test
            Expected Result: 
            */


            return GlobalTestResult;
        }
    }
}
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
    /// 22.9.1 Hide PA Function: General appearance
    /// TC-ID: 17.9.1
    /// 
    /// This test case verifies the general appearance of Hide PA function that operable in planning area. When driver presses NA01 symbol to activate show or hide the planning information function. The Hide PA function shall comply with [ERA-ERTMS] standard.
    /// 
    /// Tested Requirements:
    /// MMI_gen 7336; MMI_gen 7352 (partly: Set); MMI_gen 2996 (partly: 1st bullet, activation of ‘Hide’ and ‘Show’ button); MMI_gen 7339 (partly: Update); MMI_gen 7350 (partly: symbol NA01); MMI_gen 7353 (partly: Reset); MMI_gen 7349; MMI_gen 6962;      
    /// 
    /// Scenario:
    /// Active cabin A. Perform SoM to SR mode, level 1Start driving the train forward and Pass BG
    /// 1.Mode changes to FS mode
    /// 
    /// Used files:
    /// 17_9_1.tdg
    /// </summary>
    public class Hide_PA_Function_General_appearance : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // Set the following tags name in configuration file (See the instruction in Appendix 1)HIDE_PA_FUNCTION = 0 (‘ON’ state)System is power ON.

            // Call the TestCaseBase PreExecution
            base.PreExecution();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in FS mode, level 1.

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint


            /*
            Test Step 1
            Action: Activate cabin A
            Expected Result: DMI displays Driver ID window
            */
            // Call generic Action Method
            DmiActions.Activate_Cabin_1(this);
            // Call generic Check Results Method
            DmiExpectedResults.DMI_displays_Driver_ID_window(this);


            /*
            Test Step 2
            Action: Perform SoM to SR mode, level 1
            Expected Result: DMI displays in SR mode, level 1
            */
            // Call generic Action Method
            DmiActions.Perform_SoM_to_SR_mode_level_1(this);
            // Call generic Check Results Method
            DmiExpectedResults.SR_Mode_displayed(this);


            /*
            Test Step 3
            Action: Drive the train forward with speed = 40 km/h pass BG1
            Expected Result: DMI displays in FS mode, level 1. The Planning area is displayed the planning information in main area D
            */
            // Call generic Action Method
            DmiActions.Drive_the_train_forward_with_speed_40_kmh_pass_BG1(this);


            /*
            Test Step 4
            Action: Press the ‘NA01’ symbol in sub-area D14
            Expected Result: Verify the following information, The Planning area is disappeared from DMI.Use log file to verify tha DMI still received packet EVC-4 from ETCS Onboard
            Test Step Comment: (1) MMI_gen 7336 (partly: Hide); MMI_gen 7352 (partly: Set); MMI_gen 2996        (partly: 1st bullet, activation of ‘Hide’ button); MMI_gen 7339         (partly: Update);  (2) MMI_gen 6962 (partly: continuously updated objects in the background);         
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press the ‘NA01’ symbol in sub-area D14");


            /*
            Test Step 5
            Action: Press at sensitive area in main area D
            Expected Result: Verify the following information, When driver presses main area D in sensitive area. The planning area is reappeared by this activation.NA01 symbol is still display in sub-area D14.All objects on the Planning area is updated according to the packet that sent from ETCS OB.Use log file to verify tha DMI still received packet EVC-4 from ETCS Onboard
            Test Step Comment: (1) MMI_gen 7336 (partly: Show); MMI_gen 7353 (partly: Reset);     MMI_gen 7349;     MMI_gen 2996        (partly: 1st bullet, activation of ‘Show’ button);   (2) MMI_gen 7350 (partly: symbol NA01);(3) MMI_gen 6962 (partly: updated in the background);(4) MMI_gen 6962 (partly: continuously be updated);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press at sensitive area in main area D");


            /*
            Test Step 6
            Action: Press the ‘NA01’ symbol in sub-area D14
            Expected Result: Verify the following information, (1)   The Planning area is disappeared from DMI.(2)   Use log file to verify tha DMI still received packet EVC-4 from ETCS Onboard
            Test Step Comment: (1) MMI_gen 6962 (partly: hidden objects);(2) MMI_gen 6962 (partly: continuously updated objects in the background);         
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press the ‘NA01’ symbol in sub-area D14");


            /*
            Test Step 7
            Action: Press at sensitive area in main area D to display the Planning area
            Expected Result: The Planning area is reappeared in area D.Verify the following information, (1)   Use the log file to confirm that all objects on the Planning area are updated according to the received packet EVC-4 from ETCS Onboard
            Test Step Comment: (1) MMI_gen 6962;
            */


            /*
            Test Step 8
            Action: End of test
            Expected Result: 
            */


            return GlobalTestResult;
        }
    }
}
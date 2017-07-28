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
    /// 17.8 Evaluation of Train Speed
    /// TC-ID: 12.8
    /// 
    /// This test case verifies a display information of specified objects (e.g. Speed pointer, Speed digital, CSG, CSG-Extension, all hooks, Ditance to target bar and digital) refer to received packet EVC-1.
    /// 
    /// Tested Requirements:
    /// MMI_gen 1268; MMI_gen 1275; MMI_gen 1086; MMI_gen 1277;
    /// 
    /// Scenario:
    /// Perform the test scenarios below, and verify the display of all speed objects which are corresponded to the received packet EVC-1 with each scenario.    
    /// 1.Start ATP.    
    /// 2.Activate cabin and perform SoM in SR mode, level 
    /// 1.
    /// 3.Drive the train forward pass BG1 at position 100m.    
    /// 4.Use the test script file to send a special value of EVC-1.
    /// 
    /// Used files:
    /// 12_8.tdg, 12_8_a.xml
    /// </summary>
    public class Evaluation_of_Train_Speed : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // -    Test system is powered on.-    ATP is still not start.

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
            Action: Start ATP without cabin activation
            Expected Result: Verify the following information,(1)    Use the log file to confirm that DMI receives packet EVC-1 with variable MMI_V_TRAIN = -1.(2)   The following objects are not displayed on the DMI,Speed PointerSpeed DigitalCSGCSG-ExtensionAll hooksTarget Distance Bar
            Test Step Comment: (1) MMI_gen 1086 (partly: received MMI_V_TRAIN equal -1); MMI_gen 1268 (partly: received MMI_V_TRAIN equal -1); MMI_gen 1275 (partly: received invalid MMI_V_TRAIN);(2) MMI_gen 1086 (partly: when MMI_V_TRAIN equal -1);  MMI_gen 1268 (partly: when MMI_DYNAMIC not elder than 600ms and MMI_V_TRAIN equal -1); MMI_gen 1275 (partly: when MMI_V_TRAIN is invalid); 
            */


            /*
            Test Step 2
            Action: Activate cabin A and perform SoM in SR mode, Level 1
            Expected Result: Verify the following information,(1)   Use the log file to confirm that DMI receives packet EVC-1 with variable MMI_V_TRAIN = 0.(2)    The Speed pointer, Speed digital, CSG, CSG-Extension, all hooks, Target Distance Bar and Target Distance Digital are diplayed and correspond to the  received packet EVC-1
            Test Step Comment: (1) MMI_gen 1086 (partly: negative case - received MMI_V_TRAIN not equal -1); MMI_gen 1268 (partly: received MMI_V_TRAIN greater than -1); MMI_gen 1275 (partly: negative case - received valid MMI_V_TRAIN);(2) MMI_gen 1086 (partly: negative case - when MMI_V_TRAIN not equal -1); MMI_gen 1268 (partly: when MMI_DYNAMIC not elder than 600ms and MMI_V_TRAIN greater than -1); MMI_gen 1275 (partly: negative case - when MMI_V_TRAIN is valid);
            */


            /*
            Test Step 3
            Action: Drive the train forward pass BG1 with speed = 25 km/h
            Expected Result: Verify the following information,(1)   Use the log file to confirm that DMI received packet EVC-1 with variable MMI_V_TRAIN = 694.(2)    The Speed pointer and Speed digital are diplayed consist with received packet EVC-1.(3)   The Speed Pointer and Speed Digital on DMI screen are correspond with the current train speed
            Test Step Comment: (1) MMI_gen 1086 (partly: negative case - received MMI_V_TRAIN not equal -1); MMI_gen 1268 (partly: received MMI_V_TRAIN greater than -1); MMI_gen 1275 (partly: negative case - received valid MMI_V_TRAIN);(2) MMI_gen 1086 (partly: negative case - when MMI_V_TRAIN not equal -1); MMI_gen 1268 (partly: when MMI_DYNAMIC not elder than 600ms and MMI_V_TRAIN greater than -1); MMI_gen 1275 (partly: negative case - when MMI_V_TRAIN is valid);(3) MMI_gen 1277;
            */


            /*
            Test Step 4
            Action: Use the test script file 12_8_a.xml to send EVC-1 with, MMI_V_TRAIN = -2
            Expected Result: Verify the following information,(1)   The following objects are not display on DMI,Speed PointerSpeed DigitalCSGCSG-ExtensionAll hooksTarget Distance BarTarget Distance Digital
            Test Step Comment: (1) MMI_gen 1086 (partly: negative case - when MMI_V_TRAIN not equal -1); MMI_gen 1268 (partly: negative case - when MMI_DYNAMIC not elder than 600ms and MMI_V_TRAIN not greater than and equal -1); MMI_gen 1275;
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
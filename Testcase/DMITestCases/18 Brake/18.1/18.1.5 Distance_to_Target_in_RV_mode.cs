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
    /// 18.1.5 Distance to Target in RV mode
    /// TC-ID: 13.1.5
    /// 
    /// This test case verifies the presentation of the distance to target digital and the distance to target bar when DMI is displayed in RV mode.
    /// 
    /// Tested Requirements:
    /// MMI_gen 105 (partly: RV mode); MMI_gen 2567 (partly: RV mode); MMI_gen 107 (partly: ETCS mode, Table 37, RV mode);
    /// 
    /// Scenario:
    /// Activate cabin A. Performs SoM to SR mode, Level 1.Note: Set the train length to 100m during train data entry process.Drive the train forward pass BG1 at position 50mBG1: packet 12, 21 and 27Drive the train forward pass BG2 at position 200mBG2 Packet138: D_STARTREVERSE 100, L_REVERSEAREA 400        Packet139: D_REVERSE 200, V_REVERSE 30Stop the train at position 700m.Driver selects reversing mode and confirmmode change to RV mode.Drive the train backward and verify the display of distance to target on DMI.
    /// 
    /// Used files:
    /// 13_1_5.tdg
    /// </summary>
    public class Distance_to_Target_in_RV_mode : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // System is power on.
            
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
            Action: Activate cabin A
            Expected Result: DMI displays in SB mode. The Driver ID window is displayed
            */
            // Call generic Action Method
            DmiActions.Activate_cabin_A();
            // Call generic Check Results Method
            DmiExpectedResults.DMI_displays_in_SB_mode_The_Driver_ID_window_is_displayed();
            
            
            /*
            Test Step 2
            Action: Driver performs SoM to SR mode, Level 1.Note: Please set Train length = 100m during train data entry process
            Expected Result: DMI displays in SR mode, level 1
            */
            // Call generic Check Results Method
            DmiExpectedResults.DMI_displays_in_SR_mode_level_1();
            
            
            /*
            Test Step 3
            Action: Drive the train forward passing BG1Then drive the train forward  with speed = 40 km/h in FS mode
            Expected Result: DMI changes from SR to FS mode
            */
            // Call generic Check Results Method
            DmiExpectedResults.DMI_changes_from_SR_to_FS_mode();
            
            
            /*
            Test Step 4
            Action: Driving forward passing BG2
            Expected Result: 
            */
            
            
            /*
            Test Step 5
            Action: The train is in reversing area
            Expected Result: DMI remains displays in FS mode
            */
            // Call generic Action Method
            DmiActions.The_train_is_in_reversing_area();
            // Call generic Check Results Method
            DmiExpectedResults.DMI_remains_displays_in_FS_mode();
            
            
            /*
            Test Step 6
            Action: Stop the train
            Expected Result: The train is at standstill.Driver is informed that reversing is possible
            */
            // Call generic Action Method
            DmiActions.Stop_the_train();
            
            
            /*
            Test Step 7
            Action: Change the direction of train to reverse. Then select and confirm RV mode
            Expected Result: DMI displays in RV mode, level 1.Verify the following information,Use the log file to confirm that the distance to target (bar and digital) is calculated from the received packet information EVC-7 and EVC-1 as follows,(EVC-7) OBU_TR_O_TRAIN – (EVC-1) MMI_O_BRAKE_TARGETExample: The observation point of the distance target is 407. [EVC-7.OBU_TR_O_TRAIN = 1000080700] – [EVC-1.MMI_O_BRAKETARGET = 1000040036] = 40664 (406.64 m)Use the log file to confirm that the distance to target bar is display when DMI received packet information EVC-7 with, OBU_TR_M_MODE = 14
            Test Step Comment: (1)MMI_gen 105           (partly: RV mode);                        (2) MMI_gen 2567 (partly RV mode); MMI_gen 107 (partly: ETCS mode, Table 37, RV mode);
            */
            // Call generic Action Method
            DmiActions.Change_the_direction_of_train_to_reverse_Then_select_and_confirm_RV_mode();
            
            
            /*
            Test Step 8
            Action: End of test
            Expected Result: 
            */
            

            return GlobalTestResult;
        }
    }
}

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
    /// 22.4.17 PA Track Condition: First symbol prevails over the next coming symbol
    /// TC-ID: 17.4.17
    /// 
    /// This test case is to verify that if two or more PA Track condition symbols located closely. The first coming symbol shall display in the foreground of the next coming symbol.
    /// 
    /// Tested Requirements:
    /// MMI_gen 1417;
    /// 
    /// Scenario:
    /// 1.Power on test system and activate cabin.
    /// 2.Perform Start of Mission to L1, SR mode
    /// 3.Pass BG0 with MA and Track desciption
    /// 4.Mode changes to FS mode 
    /// 5.Pass BG1 with containing pkt68 (Track condtion)                 M_TRACKCOND = 0 (Non Stopping area)         M_TRACKCOND = 2 (Sound Horn)         M_TRACKCOND = 4 (Radio Hole)                  M_TRACKCOND = 0 (Non Stopping area)      
    /// 6.Verify the Track condition symbos display on area D2, D3 and D4
    /// 
    /// Used files:
    /// 17_4_17.tdg
    /// </summary>
    public class PA_Track_Condition_First_symbol_prevails_over_the_next_coming_symbol : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // Power off the  systemTrain length is 100 mConfigure atpcu configuration file as follwing:TC_T_Panto_Down = 100TC_T_MainSwitch_Off = 100TC_T_Airtight_Close =100TC_T_Inhib_RBBrake = 100TC_T_ Inhib_ECBrake = 100TC_T_ Inhib_MSBrake = 100TC_T_Change_TractionSyst = 100TC_T_Allowed_CurrentConsump = 100 TC_T_StationPlatform = 100
            
            // Call the TestCaseBase PreExecution
            base.PreExecution();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays FS mode.

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint

            
            /*
            Test Step 1
            Action: Power on the system and activate cabin
            Expected Result: DMI displays in SB mode
            */
            // Call generic Action Method
            DmiActions.Power_on_the_system_and_activate_cabin();
            // Call generic Check Results Method
            DmiExpectedResults.DMI_displays_in_SB_mode();
            
            
            /*
            Test Step 2
            Action: Perform SoM to L1, SR mode
            Expected Result: Mode changes to SR mode , L1
            */
            // Call generic Action Method
            DmiActions.Perform_SoM_to_L1_SR_mode();
            // Call generic Check Results Method
            DmiExpectedResults.Mode_changes_to_SR_mode_L1();
            
            
            /*
            Test Step 3
            Action: Drive the train up to 20 km/h
            Expected Result: The speed pointer is indicated as 20  km/h
            */
            // Call generic Action Method
            DmiActions.Drive_the_train_up_to_20_kmh();
            // Call generic Check Results Method
            DmiExpectedResults.The_speed_pointer_is_indicated_as_20_kmh();
            
            
            /*
            Test Step 4
            Action: Pass BG0 with MA and Track descriptionPkt 12,21 and 27
            Expected Result: Mode changes to FS mode , Level 1
            */
            // Call generic Action Method
            DmiActions.Pass_BG0_with_MA_and_Track_descriptionPkt_12_21_and_27();
            
            
            /*
            Test Step 5
            Action: Pass BG1 with 4Track conditions Pkt 68:D_TRACKCOND(1) = 400L_TRACKCOND(1) = 200M_TRACKCOND(1) = 0D_TRACKCOND(2) = 0L_TRACKCOND(2) = 200M_TRACKCOND(2) = 2D_TRACKCOND(3) = 5L_TRACKCOND(3) = 200M_TRACKCOND(3) = 4D_TRACKCOND(4) = 10L_TRACKCOND(4) = 200M_TRACKCOND(4) = 0
            Expected Result: Mode remins in FS mode
            */
            // Call generic Check Results Method
            DmiExpectedResults.Mode_remins_in_FS_mode();
            
            
            /*
            Test Step 6
            Action: Continue the train speed at 20 km/h
            Expected Result: Verify the following informationDMI displays Track condition symbol “ Non-stopping area” over “ Sound horn”
            Test Step Comment: MMI_gen 1417;
            */
            
            
            /*
            Test Step 7
            Action: End of test
            Expected Result: 
            */
            

            return GlobalTestResult;
        }
    }
}

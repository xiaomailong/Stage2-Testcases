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
    /// 23.1.1.1.2 Verbose Visualization
    /// TC-ID: 18.1.1.1.2
    /// 
    /// This test case verifies the display information refer to mapping table of Driver Message ID for verbose Radio connection status.
    /// 
    /// Tested Requirements:
    /// MMI_gen 7527 (partly: divided in two areas); MMI_gen 2576 (partly: verbose visualisation); MMI_gen 11459 (partly: verbose visualisation); MMI_gen 1855 (partly: connection established);  MMI_gen 11442 (partly: connection up with one RBC, connection up with two RBC, network registration via one mode, network registration via two modem); MMI_gen 7022 (partly: Radio connection symbols); MMI_gen 3005 (partly: Radio connection symbols);
    /// 
    /// Scenario:
    /// Activate Cabin A.Perform SoM to SR mode L2 and verify the radio connection established symbol.Drive the train forward pass BG1 at position 50m. Then receives FS MA from RBC.Drive the train forward pass BG2 at position 250m. Then receives RBC transition order from RBC and verify the connection up with two RBCs symbol and network registered via two modems symbol.Simulate RBC loss communication, verify the radio connection Lost/Set-Up failed symbol.Re-establish radio connection, verify the radio connection established symbol.
    /// 
    /// Used files:
    /// 18_1_1_1_2.utt, 18_1_1_1_2.tdg
    /// </summary>
    public class Verbose_Visualization : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // Setup a verbose visualisation for the radio connection status in configuration file (RADIO_STATUS_VISUAL= 1).System is powered ON.

            // Call the TestCaseBase PreExecution
            base.PreExecution();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in FS mode, level 2

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint


            /*
            Test Step 1
            Action: Activate Cabin A
            Expected Result: DMI displays Driver ID window.Verify that sub-area E1 is divided in two areas
            Test Step Comment: (1) MMI_gen 7527 (partly: divided in two areas);          MMI_gen 11459 (partly: verbose visualisation);
            */
            // Call generic Action Method
            DmiActions.Activate_Cabin_A(this);


            /*
            Test Step 2
            Action: Perform SoM in SR mode, Level 2
            Expected Result: DMI displays Connection established symbol (ST103) in the left part of sub-area E1.Use the log file to confirm that DMI receives packet information EVC-8 with variable MMI_DRIVER_MESSAGE.MMI_Q_TEXT = 568 (ST03 symbol)
            Test Step Comment: (1) MMI_gen 2576 (partly: verbose visualisation, connection established); MMI_gen 1855 (partly: connection established);  MMI_gen 11459 (partly: verbose visualisation); MMI_gen 7022 (partly: Radio connection symbols); MMI_gen 3005 (partly: Radio connection symbols);
            */
            // Call generic Action Method
            DmiActions.Perform_SoM_in_SR_mode_Level_2(this);


            /*
            Test Step 3
            Action: Drive the train forward with speed below the permitted speed
            Expected Result: The train is moving forward, position is increase.The speed pointer displays the current speed
            */
            // Call generic Action Method
            DmiActions.Drive_the_train_forward_with_speed_below_the_permitted_speed(this);
            // Call generic Check Results Method
            DmiExpectedResults
                .The_train_is_moving_forward_position_is_increase_The_speed_pointer_displays_the_current_speed(this);


            /*
            Test Step 4
            Action: Receives FS MA and track description from RBC
            Expected Result: DMI displays in FS mode, Level 2
            */
            // Call generic Action Method
            DmiActions.Receives_FS_MA_and_track_description_from_RBC(this);
            // Call generic Check Results Method
            DmiExpectedResults.DMI_displays_in_FS_mode_Level_2(this);


            /*
            Test Step 5
            Action: Receives RBC transition order from RBC
            Expected Result: DMI displays Connection Up with two RBCs symbol (ST03B) in the left part of sub-area E1.Use the log file to confirm that DMI receives packet information EVC-8 with variable MMI_DRIVER_MESSAGE.MMI_Q_TEXT = 614.DMI displays Network registered via two modems symbol (ST102) in the right part of sub-area E1.Use the log file to confirm that DMI receives packet information EVC-8 with variable MMI_DRIVER_MESSAGE.MMI_Q_TEXT = 610
            Test Step Comment: (1) MMI_gen 2576 (partly: verbose visualisation, Connection Up with two RBCs); MMI_gen 1855 (partly: connection established);  MMI_gen 11459 (partly: verbose visualisation); MMI_gen 11442 (partly: connection Up with wto RBC);(2) MMI_gen 2576 (partly: verbose visualisation, Network registered via two modems); MMI_gen 1855 (partly: connection established);  MMI_gen 11459 (partly: verbose visualisation); MMI_gen 11442 (partly: Network registration via two modems); MMI_gen 7022 (partly: Radio connection symbols); MMI_gen 3005 (partly: Radio connection symbols);
            */


            /*
            Test Step 6
            Action: Receive Terminate communication session from RBC
            Expected Result: DMI displays Connection Up symbol (ST103) in the left part of sub-area E1.Use the log file to confirm that DMI receives packet information EVC-8 with variable MMI_DRIVER_MESSAGE.MMI_Q_TEXT = 613 (ST03 symbol).DMI displays Network registered via one modem symbol (ST100) in the right part of sub-area E1.Use the log file to confirm that DMI receives packet information EVC-8 with variable MMI_DRIVER_MESSAGE.MMI_Q_TEXT = 609
            Test Step Comment: (1) MMI_gen 2576 (partly: verbose visualisation, Connection Up with two RBCs); MMI_gen 1855 (partly: connection up);  MMI_gen 11459 (partly: verbose visualisation); MMI_gen 11442 (partly: connection up with one RBC);(2) MMI_gen 2576 (partly: verbose visualisation, Network registered via one modem); MMI_gen 1855 (partly: connection established);  MMI_gen 11459 (partly: verbose visualisation); MMI_gen 11442 (partly: Network registration via one modem); MMI_gen 7022 (partly: Radio connection symbols); MMI_gen 3005 (partly: Radio connection symbols);
            */


            /*
            Test Step 7
            Action: Simulate RBC communication loss and wait for a few secondsNote: This simulation is performed automatically by UTT file
            Expected Result: DMI displays Connection Lost/Set-Up failed symbol (ST03C) in the left part of sub-area E1.Use the log file to confirm that DMI receives packet information EVC-8 with variable MMI_DRIVER_MESSAGE.MMI_Q_TEXT = 282 (ST04 symbol)
            Test Step Comment: (1) MMI_gen 2576 (partly: verbose visualisation, connection lost); MMI_gen 1855 (partly: connection established);  MMI_gen 11459 (partly: verbose visualisation); MMI_gen 7022 (partly: ST03C, MMI_Q_TEXT = 615); MMI_gen 3005 (partly: ST03C, MMI_Q_TEXT = 615);
            */


            /*
            Test Step 8
            Action: Re-establish the radio communication.Note: This simulation is performed automatically by UTT file
            Expected Result: DMI displays Connection established symbol (ST103) in the left part of sub-area E1.Use the log file to confirm that DMI receives packet information EVC-8 with variable MMI_DRIVER_MESSAGE.MMI_Q_TEXT = 613
            Test Step Comment: (1) MMI_gen 2576 (partly: verbose visualisation, connection up); MMI_gen 1855 (partly: connection established); MMI_gen 11459 (partly: verbose visualisation); MMI_gen 7022 (partly: Radio connection symbols); MMI_gen 3005 (partly: Radio connection symbols);
            */


            /*
            Test Step 9
            Action: End of test
            Expected Result: 
            */


            return GlobalTestResult;
        }
    }
}
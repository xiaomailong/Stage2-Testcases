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
    /// 23.1.1.1.1 Concise Visualization
    /// TC-ID: 18.1.1.1.1
    /// 
    /// This test case verifies the display information refer to mapping table of Driver Message ID for concise Radio connection status. 
    /// 
    /// Tested Requirements:
    /// MMI_gen 2576 (partly: concise visualisation); MMI_gen 11459 (partly: concise visualisation); MMI_gen 1855 (partly: connection established); MMI_gen 7022 (partly: Radio connection symbols); MMI_gen 3005 (partly: Radio connection symbols);
    /// 
    /// Scenario:
    /// Activate Cabin A.Enter Driver ID and verify that there is no packet information EVC-8 with ST03 and ST04 symbol.Perform SoM to SR mode L2 and verify the radio connection established symbol.Drive the train forward pass BG1 at position 50m. Then receives FS MA from RBC.Simulatae RBC loss communication and verify the radio connection Lost/Set-Up failed symbol.Re-establish radio connection and verify the radio connection established symbol.De-activate and activate Cabin A again.Use the test script file to send EVC-
    /// 8.Then, verify the display information in sub area-E1.
    /// 
    /// Used files:
    /// 18_1_1_1_1.tdg, 18_1_1_1_1.utt, 18_1_1_1_1_a.xml, 18_1_1_1_1_b.xml
    /// </summary>
    public class Concise_Visualization : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // Setup a concise visualisation for the radio connection status in configuration file (RADIO_STATUS_VISUAL= 0).System is powered ON.

            // Call the TestCaseBase PreExecution
            base.PreExecution();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in SB mode.

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint


            /*
            Test Step 1
            Action: Activate Cabin A.
            Expected Result: DMI displays Driver ID window.
            */

            /*
            Test Step 2
            Action: Enter Driver ID. 
            Expected Result: No symbol display in sub area E1.Use the log file to confirm that there is no packet information EVC-8 with variable MMI_Q_TEXT = 282 (ST04 symbol) and MMI_Q_TEXT = 568 (ST03 symbol) send to DMI.
            Test Step Comment: MMI_gen 2576 (partly: concise visualisation); MMI_gen 11459 (partly: concise visualisation);
            */

            /*
            Test Step 3
            Action: Perform SoM in SR mode, Level 2. 
            Expected Result: DMI displays Connection established symbol (ST03) in sub area E1.Use the log file to confirm that DMI receives packet information EVC-8 with variable MMI_DRIVER_MESSAGE.MMI_Q_TEXT = 568 (ST03 symbol).
            Test Step Comment: (1) MMI_gen 2576 (partly: concise visualisation, connection established); MMI_gen 1855 (partly: connection established);  MMI_gen 11459 (partly: concise visualisation); 
            */

            /*
            Test Step 4
            Action: Drive the train forward with speed below the permitted speed.
            Expected Result: The train is moving forward, position is increase.The speed pointer displays the current speed. 
            */

            /*
            Test Step 5
            Action: Receives FS MA and track description from RBC.
            Expected Result: DMI displays in FS mode, Level 2. 
            */

            /*
            Test Step 6
            Action: Simulate RBC communication loss and wait for a few secondsNote: This simulation is perform automatically by UTT file. 
            Expected Result: DMI displays Connection Lost/Set-Up failed symbol (ST04) in sub area E1.Use the log file to confirm that DMI receives packet information EVC-8 with variable MMI_DRIVER_MESSAGE.MMI_Q_TEXT = 282 (ST04 symbol).
            Test Step Comment: (1) MMI_gen 2576 (partly: concise visualisation, connection lost); MMI_gen 1855 (partly: connection established);  MMI_gen 11459 (partly: concise visualisation); MMI_gen 7022 (partly: Radio connection symbols); MMI_gen 3005 (partly: Radio connection symbols);
            */

            /*
            Test Step 7
            Action: Re-establish the radio communication.Note: This simulation is perform automatically by UTT file.
            Expected Result: DMI displays Connection established symbol (ST03) in sub area E1.Use the log file to confirm that DMI receives packet information EVC-8 with variable MMI_DRIVER_MESSAGE.MMI_Q_TEXT = 613.
            Test Step Comment: (1) MMI_gen 2576 (partly: concise visualisation, connection up); MMI_gen 1855 (partly: connection established);  MMI_gen 11459 (partly: concise visualisation); MMI_gen 7022 (partly: Radio connection symbols); MMI_gen 3005 (partly: Radio connection symbols);
            */

            /*
            Test Step 8
            Action: Perform the following procedure,Stop the trainDe-activate Cabin A.Activate Cabin A.
            Expected Result: The symbol in sub area E1 is removed.
            */

            /*
            Test Step 9
            Action: Use the test script file 18_1_1_1_1_a.xml to send EVC-8 with,MMI_Q_TEXT_CLASS = 1 MMI_Q_TEXT_CRITERIA = 3MMI_Q_TEXT = 610
            Expected Result: No symbol display in sub area E1.
            Test Step Comment: MMI_gen 2576 (partly: concise visualisation, Network registered via two modems); MMI_gen 1855 (partly: connection established);  MMI_gen 11459 (partly: concise visualisation);
            */

            /*
            Test Step 10
            Action: (Continue from step 9) Send EVC-8 with,MMI_Q_TEXT_CLASS = 1 MMI_Q_TEXT_CRITERIA = 3MMI_Q_TEXT = 609
            Expected Result: No symbol display in sub area E1.
            Test Step Comment: MMI_gen 2576 (partly: concise visualisation, Network registered via one modems); MMI_gen 1855 (partly: connection established);  MMI_gen 11459 (partly: concise visualisation); 
            */

            /*
            Test Step 11
            Action: Use the test script file 18_1_1_1_1_b.xml to send EVC-8 with,MMI_Q_TEXT_CLASS = 1 MMI_Q_TEXT_CRITERIA = 3MMI_Q_TEXT = 614
            Expected Result: DMI displays Connection established symbol (ST03) in sub area E1.
            Test Step Comment: MMI_gen 2576 (partly: concise visualisation, Connection up with two RBCs); MMI_gen 1855 (partly: connection established);  MMI_gen 11459 (partly: concise visualisation); MMI_gen 7022 (partly: Radio connection symbols); MMI_gen 3005 (partly: Radio connection symbols);
            */

            /*
            Test Step 12
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
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
using Testcase.Telegrams.EVCtoDMI;
using Testcase.Telegrams.DMItoEVC;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 10.10 Screen Layout: Button States
    /// TC-ID: 5.10
    /// 
    /// This test case verifies the display of the buttons in disable state.
    /// 
    /// Tested Requirements:
    /// MMI_gen 4377;
    /// 
    /// Scenario:
    /// 1.Perform SoM until train running number is entered.2      Send XML script (EVC-30) to disable the buttons on Main window, override window, Special window and Setting window
    /// 3.Verify that the disable buttons shall be shown as a enable button with text label in dark gray. 
    /// 
    /// Used files:
    /// 5_10_a.xml, 5_10.tdg, 5_10.utt
    /// </summary>
    public class Screen_Layout_Button_States : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:

            // Call the TestCaseBase PreExecution
            base.PreExecution();

            // Test system is powered on    -> Cabin is active: not in spec
            DmiActions.Start_ATP();

        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in SB mode
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Default window in SB mode, Level 1.");

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint


            /*
            Test Step 1
            Action: Perform SoM until train running number is entered
            Expected Result: DMI displays Main window with enabled ‘Start’ button
            */
            // Set train running number, cab 1 active, and other defaults
            DmiActions.Activate_Cabin_1(this);

            // Set driver ID
            DmiActions.Set_Driver_ID(this, "1234");

            // Set to level 1 and SR mode
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Level = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_LEVEL.L1;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.StandBy;

            EVC30_MMIRequestEnable.SendBlank();

            // Spec says Start button enabled
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.TrainRunningNumber;
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = 0;      // Start window
            EVC30_MMIRequestEnable.Send();

            DmiActions.ShowInstruction(this, "Enter Train running number");

            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.Start | Variables.standardFlags;
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = 1;      // Main window
            EVC30_MMIRequestEnable.Send();

            // Call generic Check Results Method
            DmiExpectedResults.Main_Window_displayed_with_Start_button_enabled(this);

            // Steps 2 to 22 are in XML_5_10_a.cs
            /*
            Test Step 2
            Action: Send the test script file 5_10_a.xml to send EVC-30 with,MMI_Q_REQUEST_ENABLE           (#0) = 0           (#1) = 0           (#2) = 0           (#3) = 0           (#4) = 0           (#5) = 0           (#6) = 0           (#7) = 0           (#8) = 0
            Expected Result: The following buttons are shown with a border and its text is coloured Dark-Grey:The ‘Start’ buttonThe ‘Driver ID’ buttonThe ‘Train data’ buttonThe ‘Level’ buttonThe ‘Train running number’ buttonThe ‘Shunting’ buttonThe ‘Non-Leading’ buttonThe ‘Maintain Shunting’ button
            Test Step Comment: MMI_gen 4377 (partly: Main window);
            */
            XML.XML_5_10_a.Send(this);

            /*
            Test Step 3
            Action: Press Exit button and select Override menu then run test script 5_10_a.xml to send EVC-30 with,MMI_Q_REQUEST_ENABLE         (#9) = 0
            Expected Result: The following button is shown with a border and its text is coloured Dark-Grey:The ‘EOA’ button
            Test Step Comment: MMI_gen 4377 (partly: Override window);
            */


            /*
            Test Step 4
            Action: Press Exit button and select Special menu then run test script 5_10_a.xml to send EVC-30 with,MMI_Q_REQUEST_ENABLE         (#10) = 0         (#11) = 0         (#12) = 0
            Expected Result: The following buttons are shown with a border and its text is coloured Dark-Grey:The ‘Adhesion’ buttonThe ‘SR speed/distance’ buttonThe ‘Train Integrity’ button
            Test Step Comment: MMI_gen 4377 (partly: Override window);
            */


            /*
            Test Step 5
            Action: Press Exit button and select Setting menu then run test script 5_10_a.xml to send EVC-30 with,MMI_Q_REQUEST_ENABLE         (#13) = 0         (#14) = 0         (#15) = 0         (#16) = 0         (#17) = 0         (#18) = 0         (#25) = 0         (#26) = 0         (#32) = 0
            Expected Result: The following buttons are shown with a border and its text is coloured Dark-Grey:The ‘Language’ buttonThe ‘Volume’ buttonThe ‘Brightness’ buttonThe ‘System version’ buttonThe ‘Set VBC’ buttonThe ‘Remove VBC’ buttonThe ‘Set Clock’ button The ‘System info’ button
            Test Step Comment: MMI_gen 4377 (partly: Setting window);
            */


            /*
            Test Step 6
            Action: Deativate and activate the cabin
            Expected Result: DMI disiplays in SB mode
            */

            /*
            Test Step 7
            Action: Press Setting menu and select Maintenace button and passward. The  run test script 5_10_a.xml to send EVC-30 with,MMI_Q_REQUEST_ENABLE         (#29) = 0         (#30) = 0
            Expected Result: The following buttons are shown with a border and its text is coloured Dark-Grey:The ‘Wheel diameter’ buttonThe ‘Radar’ button
            Test Step Comment: MMI_gen 4377 (partly: Mainteance window);
            */


            /*
            Test Step 8
            Action: Deativate and activate the cabin
            Expected Result: DMI disiplays in SB mode
            */


            /*
            Test Step 9
            Action: Enter Driver ID, skip brake test, select Level 1 then  shunting mode
            Expected Result: DMI disiplays in SH mode
            Test Step Comment: MMI_gen 4377 (partly: Exit Shunting);
            */


            /*
            Test Step 10
            Action: Run test script 5_10_a.xml to send EVC-30 with,MMI_Q_REQUEST_ENABLE         (#6) = 0
            Expected Result: The following button is shown with a border and its text is coloured Dark-Grey:The ‘Exit Shuntingr’ button
            */


            /*
            Test Step 11
            Action: Deativate and activate the cabin
            Expected Result: DMI disiplays in SB mode
            */
            // De-activate and activate cabin


            /*
            Test Step 12
            Action: Perform Start of Mission to SR mode , Level 1
            Expected Result: DMI disiplays in SR mode
            */  

            /*
            Test Step 13
            Action: Drive the train forward with 40 km/h then select Setting menu
            Expected Result: The following buttons are shown with a border and its text is coloured Dark-Grey:The ‘Lock screen for cleanning’ buttonThe ‘Brake’ buttonThe ‘National’ buttonThe ‘Maintenance’ button
            Test Step Comment: MMI_gen 4377 (partly: Setting window);
            */

            /*
            Test Step 14
            Action: Pass BG1 with Pkt 12,21 and 27
            Expected Result: DMI disiplays in FS mode
            */


            /*
            Test Step 15
            Action: Pass BG2 with pkt 79 Geographical position then Run test script 5_10_a.xml to send EVC-30 with,MMI_Q_REQUEST_ENABLE         (#23) = 0
            Expected Result: The following button is shown with a border and its text is coloured Dark-Grey:The ‘Geographical        position’ button
            Test Step Comment: MMI_gen 4377 (partly: Geographical position);
            */


            /*
            Test Step 16
            Action: Stop the train
            Expected Result: Train is at standstill
            */
            // Call generic Action Method


            /*
            Test Step 17
            Action: Deactivate and activate cabin
            Expected Result: DMI disiplays in SB mode
            */

            /*
            Test Step 18
            Action: Perform the following procedure,Activate Cabin AEnter Driver ID and perform brake testSelect and confirm Level 2Then run test script 5_10_a.xml to send EVC-30 with,MMI_Q_REQUEST_ENABLE         (#19) = 0         (#20) = 0         (#21) = 0         (#22) = 0
            Expected Result: The following buttons are shown with a border and its text is coloured Dark-Grey:The ‘Contract last window’ buttonThe ‘Use short number’ buttonThe ‘Enter RBC data’ buttonThe ‘Radio Network ID’ button
            Test Step Comment: MMI_gen 4377 (partly: RBC data window);
            */


            /*
            Test Step 19
            Action: Deactivate and activate cabin
            Expected Result: DMI disiplays in SB mode
            */

            /*
            Test Step 20
            Action: Perform the following procedure,Activate Cabin AEnter Driver ID and perform brake testSelect and confirm Level STM PLZBEnter train data entry and comfirm Then run test script 5_10_a.xml to send EVC-30 with,MMI_Q_REQUEST_ENABLE         (#24) = 0
            Expected Result: The following button is shown with a border and its text is coloured Dark-Grey:The ‘End of data entry’ button
            */


            /*
            Test Step 21
            Action: Deactivate and activate cabin cabin
            Expected Result: DMI displays in SB mode
            */


            /*
            Test Step 22
            Action: Perform the following procedure,Activate Cabin AEnter Driver ID and perform brake testAt the Main window, press ‘Close’ buttonPress ‘Settings’ buttonPress ‘Brake’ button.Then run test script 5_10_a.xml to send EVC-30 with,MMI_Q_REQUEST_ENABLE(#28) = 0
            Expected Result: The following button is shown with a border and its text is coloured Dark-Grey:The ‘Brake test’ button
            Test Step Comment: MMI_gen 4377 (partly: Start Brake Test button);
            */


            /*
            Test Step 23
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
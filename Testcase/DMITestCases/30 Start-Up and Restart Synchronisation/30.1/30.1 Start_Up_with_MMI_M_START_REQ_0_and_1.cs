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
    /// 30.1 Start-Up with MMI_M_START_REQ = 0 and 1
    /// TC-ID: 25.1
    /// 
    /// To verify that the DMI is working properly during DMI start-up.
    /// 
    /// Tested Requirements:
    /// MMI_gen 90; MMI_gen 100; MMI_gen 2517; MMI_gen 233; MMI_gen 235; MMI_gen 237; MMI_gen 238; MMI_gen 11280; MMI_gen 240; MMI_gen 241; MMI_gen 242; MMI_gen 3424; MMI_gen 11389-1 (THR); MMI_gen 11430; MMI_gen 11906; MMI_gen 1074;
    /// 
    /// Scenario:
    /// Perform the test scenarios below, and verify DMI’s state and display with each scenario.
    /// 1.Power on test system and start OTE.
    /// 2.Establish the communication between ETCS Onboard and DMI in ‘Sleeping’ mode.
    /// 3.Simulate ETCS Onboard to ‘Stand by’ mode.
    /// 4.Simulate communication loss between ETCS Onboard and DMI.
    /// 5.Re-establish the communication between ETCS Onboard and DMI.
    /// 6.Activate cabin.
    /// 7.Deactivate cabin.
    /// 
    /// Used files:
    /// N/A
    /// </summary>
    public class Start_Up_with_MMI_M_START_REQ_0_and_1 : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // Test system is powered off.

            // Call the TestCaseBase PreExecution
            base.PreExecution();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // Cabin is deactivated and DMI is in “idle” state.

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint


            /*
            Test Step 1
            Action: Power on test system and start OTE
            Expected Result: Verify the following information:(1)    DMI is in isolation state and keeps all specific ATP indications extinguished. The text “No contact with ATP” is presented in area E5
            Test Step Comment: (1) MMI_gen 233; MMI_gen 1074;
            */


            /*
            Test Step 2
            Action: Establish the communication between ETCS Onboard and DMI with start of ATP.Ensure that the ‘Sleeping’ is selected on OTE Simulator
            Expected Result: Verify the following information:(1)    Use the log file to verify packet that DMI receives and sends out as following:DMI receives packet EVC-0 with variable [MMI_M_START_REQ] = 0.No ATP related information on DMI screen, except the text “starting up” in area E5.DMI sends out [MMI_START_MMI (EVC-100)] with all corresponding data.(2)    Use the log file to verify packet that DMI receives and sends out with variables and value as following:DMI receives packet EVC-0 with variable [MMI_M_START_REQ] = 1.The text “starting up” in area E5 is removed.DMI sends out [MMI_STATUS_REPORT (EVC-102).MMI_M_MMI_STATUS] = 2 (Idle) to the ETC/ATP at least each 250ms regardless of the status of the cabin activation.No mode information in area B7.No ETCS related information on DMI screen, except the text “Driver's cab not active” in area E5.No button corresponds to driver’s request on DMI screen.(3)    When DMI receives MMI_M_START_REQ = 1,  use the log file to confirm that DMI receives packet EVC-1 with cylical update of variable SDT_SSC32 (Counter)
            Test Step Comment: (1) MMI_gen 235;(2) MMI_gen 237; MMI_gen 238 (partly: in sleeping mode, MMI_gen 11280); MMI_gen 3424; MMI_gen 11430;(3) MMI_gen 11906;
            */


            /*
            Test Step 3
            Action: Deselect the ‘Sleeping’ on the OTE Simulator
            Expected Result: Verify the following information:(1)    In “Idle” state, the following actions are carried out: DMI displays a current mode in area B7.No ETCS related information on DMI screen, except the text “Driver's cab not active” in area E5.No button corresponds to driver’s request on DMI screen except Settings button
            Test Step Comment: (1) MMI_gen 238 (partly: not in sleeping mode, MMI_gen 11280), MMI_gen 3424;
            */


            /*
            Test Step 4
            Action: Driver simulates the communication loss between ETCS Onboard and DMI by removing connection (MVB/Ethernet) from DMI hardware
            Expected Result: Verify the following information:(1)    In “Idle” state, the following actions are carried out: No ATP related information on DMI screen, except the text “No connection to the ATP” in area E5.Use the log file to confirm that DMI stops sending [MMI_STATUS_REPORT (EVC-102)] packet since no EVC-1 have been received during the latest second
            Test Step Comment: (1) MMI_gen 238 (partly:  MMI_gen 240);
            */


            /*
            Test Step 5
            Action: Re-establish the communication between ETCS Onboard and DMI with start of ATP
            Expected Result: Verify the following information:(1)    DMI remains in “idle” state. Use the log file to confirm packet that DMI sends out or updates variables and value below: DMI sends out [MMI_STATUS_REPORT (EVC-102).MMI_M_MMI_STATUS] = 2 (Idle) to the ETC/ATP at least each 250ms.DMI sends out and updated value [MMI_STATUS_REPORT (EVC-102). SDT_SSC32] to the ETC/ATP at least each 250ms
            Test Step Comment: (1) MMI_gen 242 (partly: state remain in “idle”); MMI_gen 90 (partly: in state idle);
            */


            /*
            Test Step 6
            Action: Activate cabin
            Expected Result: Verify the following information:(1)    In “active” state, the following actions are carried out by using the log file to confirm packet that DMI sends out below:DMI sends out [MMI_STATUS_REPORT (EVC-102).MMI_M_MMI_STATUS] = 3 (Active) to the ETC/ATP at least each 250ms.DMI sends out and updated value [MMI_STATUS_REPORT (EVC-102). SDT_SSC32] to the ETC/ATP at least each 250ms.All relevant ATP information is presented on DMI screen.(2)    Use the log file to confirm that DMI sends out ETCS mode and displays status via packet EVC-102 preriodically at least every 1.5s
            Test Step Comment: (1) MMI_gen 241; MMI_gen 90 (partly: in state active);(2) MMI_gen 11389-1 (THR);
            */


            /*
            Test Step 7
            Action: Deactivate cabin
            Expected Result: Verify the following information:(1)    DMI removes all current ETCS objects related to indications to the driver.(2)    DMI returns to “idle” state. Use the log file to confirm that DMI sends out [MMI_STATUS_REPORT (EVC-102).MMI_M_MMI_STATUS] = 2 (Idle) to the ETC/ATP at least each 250ms
            Test Step Comment: (1) MMI_gen 2517;(2) MMI_gen 242 (partly: state return to “idle”);
            */


            /*
            Test Step 8
            Action: Press or touch any area on DMI screen
            Expected Result: Verify the following information:(1)    No response/reaction from touching on the DMI screen
            Test Step Comment: (1) MMI_gen 100;
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
using System;
using Testcase.Telegrams.DMItoEVC;
using Testcase.Telegrams.EVCtoDMI;

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
    public class TC_ID_25_1_Start_Up_with_MMI_M_START_REQ_0_and_1 : TestcaseBase
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
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 0;
            // Testcase entrypoint


            TraceHeader("Test Step 1");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Power on test system and start OTE");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following information:(1)    DMI is in isolation state and keeps all specific ATP indications extinguished. The text “No contact with ATP” is presented in area E5");
            /*
            Test Step 1
            Action: Power on test system and start OTE
            Expected Result: Verify the following information:(1)    DMI is in isolation state and keeps all specific ATP indications extinguished. The text “No contact with ATP” is presented in area E5
            Test Step Comment: (1) MMI_gen 233; MMI_gen 1074;
            */
            DmiActions.ShowInstruction(this, "Power on the test system");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI is isolated and all ATP indications are off." + Environment.NewLine +
                                @"2. The message ‘No contact with ATP’ is displayed in area E5.");

            TraceHeader("Test Step 2");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Establish the communication between ETCS Onboard and DMI with start of ATP.Ensure that the ‘Sleeping’ is selected on OTE Simulator");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following information:(1)    Use the log file to verify packet that DMI receives and sends out as following:DMI receives packet EVC-0 with variable [MMI_M_START_REQ] = 0.No ATP related information on DMI screen, except the text “starting up” in area E5.DMI sends out [MMI_START_MMI (EVC-100)] with all corresponding data.(2)    Use the log file to verify packet that DMI receives and sends out with variables and value as following:DMI receives packet EVC-0 with variable [MMI_M_START_REQ] = 1.The text “starting up” in area E5 is removed.DMI sends out [MMI_STATUS_REPORT (EVC-102).MMI_M_MMI_STATUS] = 2 (Idle) to the ETC/ATP at least each 250ms regardless of the status of the cabin activation.No mode information in area B7.No ETCS related information on DMI screen, except the text “Driver's cab not active” in area E5.No button corresponds to driver’s request on DMI screen.(3)    When DMI receives MMI_M_START_REQ = 1,  use the log file to confirm that DMI receives packet EVC-1 with cylical update of variable SDT_SSC32 (Counter)");
            /*
            Test Step 2
            Action: Establish the communication between ETCS Onboard and DMI with start of ATP.Ensure that the ‘Sleeping’ is selected on OTE Simulator
            Expected Result: Verify the following information:(1)    Use the log file to verify packet that DMI receives and sends out as following:DMI receives packet EVC-0 with variable [MMI_M_START_REQ] = 0.No ATP related information on DMI screen, except the text “starting up” in area E5.DMI sends out [MMI_START_MMI (EVC-100)] with all corresponding data.(2)    Use the log file to verify packet that DMI receives and sends out with variables and value as following:DMI receives packet EVC-0 with variable [MMI_M_START_REQ] = 1.The text “starting up” in area E5 is removed.DMI sends out [MMI_STATUS_REPORT (EVC-102).MMI_M_MMI_STATUS] = 2 (Idle) to the ETC/ATP at least each 250ms regardless of the status of the cabin activation.No mode information in area B7.No ETCS related information on DMI screen, except the text “Driver's cab not active” in area E5.No button corresponds to driver’s request on DMI screen.(3)    When DMI receives MMI_M_START_REQ = 1,  use the log file to confirm that DMI receives packet EVC-1 with cylical update of variable SDT_SSC32 (Counter)
            Test Step Comment: (1) MMI_gen 235;(2) MMI_gen 237; MMI_gen 238 (partly: in sleeping mode, MMI_gen 11280); MMI_gen 3424; MMI_gen 11430;(3) MMI_gen 11906;
            */
            EVC0_MMIStartATP.Evc0Type = EVC0_MMIStartATP.EVC0Type.VersionInfo;
            EVC0_MMIStartATP.Send();

            DmiActions.Re_establish_communication_EVC_DMI(this);

            EVC100_MMIStartMmi.MMI_M_START_STATUS = 0; // OK

            // Checking the EVC100 packet is questionable: the i/f and system version may change. May need to add CheckStatus call

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the message ‘Starting up’ in area E5." + Environment.NewLine +
                                "2. DMI displays no other information relating to ATP.");

            // How to do this?
            DmiActions.ShowInstruction(this, "Select ‘Sleeping’ mode");

            /* This is what spec says, but send 0 first seems to be needed
            EVC0_MMIStartATP.Evc0Type = EVC0_MMIStartATP.EVC0Type.GoToIdle;
            EVC0_MMIStartATP.Send();
            */
            DmiActions.Start_ATP();

            // Spec says DMI sends this out every 250 ms so wait to ensure that packet is sent
            Wait_Realtime(250);
            EVC102_MMIStatusReport.Check_MMI_M_MMI_STATUS = EVC102_MMIStatusReport.MMI_M_MMI_STATUS.StatusIdle;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI removes the message ‘Starting up’ and displays ‘Driver's cab not active’ in area E5." +
                                Environment.NewLine +
                                "2. DMI displays no mode information in area B7." + Environment.NewLine +
                                "3. No other ETCS information or driver request buttons are displayed.");

            TraceHeader("Test Step 3");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Deselect the ‘Sleeping’ on the OTE Simulator");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following information:(1)    In “Idle” state, the following actions are carried out: DMI displays a current mode in area B7.No ETCS related information on DMI screen, except the text “Driver's cab not active” in area E5.No button corresponds to driver’s request on DMI screen except Settings button");
            /*
            Test Step 3
            Action: Deselect the ‘Sleeping’ on the OTE Simulator
            Expected Result: Verify the following information:(1)    In “Idle” state, the following actions are carried out: DMI displays a current mode in area B7.No ETCS related information on DMI screen, except the text “Driver's cab not active” in area E5.No button corresponds to driver’s request on DMI screen except Settings button
            Test Step Comment: (1) MMI_gen 238 (partly: not in sleeping mode, MMI_gen 11280), MMI_gen 3424;
            */
            DmiActions.ShowInstruction(this, "De-select ‘Sleeping’ mode");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI still displays ‘Driver's cab not active’ in area E5." + Environment.NewLine +
                                "2. DMI displays the current mode in area B7." + Environment.NewLine +
                                "3. No driver request buttons are displayed.");

            TraceHeader("Test Step 4");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Driver simulates the communication loss between ETCS Onboard and DMI by removing connection (MVB/Ethernet) from DMI hardware");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following information:(1)    In “Idle” state, the following actions are carried out: No ATP related information on DMI screen, except the text “No connection to the ATP” in area E5.Use the log file to confirm that DMI stops sending [MMI_STATUS_REPORT (EVC-102)] packet since no EVC-1 have been received during the latest second");
            /*
            Test Step 4
            Action: Driver simulates the communication loss between ETCS Onboard and DMI by removing connection (MVB/Ethernet) from DMI hardware
            Expected Result: Verify the following information:(1)    In “Idle” state, the following actions are carried out: No ATP related information on DMI screen, except the text “No connection to the ATP” in area E5.Use the log file to confirm that DMI stops sending [MMI_STATUS_REPORT (EVC-102)] packet since no EVC-1 have been received during the latest second
            Test Step Comment: (1) MMI_gen 238 (partly:  MMI_gen 240);
            */
            DmiActions.Simulate_communication_loss_EVC_DMI(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays ‘No connection to the ATP’ in area E5." + Environment.NewLine +
                                "2. No other ATP information is displayed.");
            TraceHeader("Test Step 5");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Re-establish the communication between ETCS Onboard and DMI with start of ATP");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following information:(1)    DMI remains in “idle” state. Use the log file to confirm packet that DMI sends out or updates variables and value below: DMI sends out [MMI_STATUS_REPORT (EVC-102).MMI_M_MMI_STATUS] = 2 (Idle) to the ETC/ATP at least each 250ms.DMI sends out and updated value [MMI_STATUS_REPORT (EVC-102). SDT_SSC32] to the ETC/ATP at least each 250ms");
            /*
            Test Step 5
            Action: Re-establish the communication between ETCS Onboard and DMI with start of ATP
            Expected Result: Verify the following information:(1)    DMI remains in “idle” state. Use the log file to confirm packet that DMI sends out or updates variables and value below: DMI sends out [MMI_STATUS_REPORT (EVC-102).MMI_M_MMI_STATUS] = 2 (Idle) to the ETC/ATP at least each 250ms.DMI sends out and updated value [MMI_STATUS_REPORT (EVC-102). SDT_SSC32] to the ETC/ATP at least each 250ms
            Test Step Comment: (1) MMI_gen 242 (partly: state remain in “idle”); MMI_gen 90 (partly: in state idle);
            */
            DmiActions.Start_ATP();
            DmiActions.Re_establish_communication_EVC_DMI(this);

            // Spec says DMI sends this out every 250 ms so wait to ensure that packet is sent
            Wait_Realtime(250);
            EVC102_MMIStatusReport.Check_MMI_M_MMI_STATUS = EVC102_MMIStatusReport.MMI_M_MMI_STATUS.StatusIdle;

            TraceHeader("Test Step 6");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Activate cabin");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following information:(1)    In “active” state, the following actions are carried out by using the log file to confirm packet that DMI sends out below:DMI sends out [MMI_STATUS_REPORT (EVC-102).MMI_M_MMI_STATUS] = 3 (Active) to the ETC/ATP at least each 250ms.DMI sends out and updated value [MMI_STATUS_REPORT (EVC-102). SDT_SSC32] to the ETC/ATP at least each 250ms.All relevant ATP information is presented on DMI screen.(2)    Use the log file to confirm that DMI sends out ETCS mode and displays status via packet EVC-102 preriodically at least every 1.5s");
            /*
            Test Step 6
            Action: Activate cabin
            Expected Result: Verify the following information:(1)    In “active” state, the following actions are carried out by using the log file to confirm packet that DMI sends out below:DMI sends out [MMI_STATUS_REPORT (EVC-102).MMI_M_MMI_STATUS] = 3 (Active) to the ETC/ATP at least each 250ms.DMI sends out and updated value [MMI_STATUS_REPORT (EVC-102). SDT_SSC32] to the ETC/ATP at least each 250ms.All relevant ATP information is presented on DMI screen.(2)    Use the log file to confirm that DMI sends out ETCS mode and displays status via packet EVC-102 preriodically at least every 1.5s
            Test Step Comment: (1) MMI_gen 241; MMI_gen 90 (partly: in state active);(2) MMI_gen 11389-1 (THR);
            */
            DmiActions.Activate_Cabin_1(this);

            // Spec says that details of DMI status are sent every 250 ms: so set SB mode and check that it is reported
            // Cabin should be active
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.StandBy;

            // Spec says DMI sends this out every 250 ms so wait to ensure that packet is sent
            Wait_Realtime(250);
            EVC102_MMIStatusReport.Check_MMI_M_MMI_STATUS = EVC102_MMIStatusReport.MMI_M_MMI_STATUS.StatusActive;

            // Spec says mode (and status: interpreted here as cabin status) are reported each 1.5s so allow another 1.25s
            Wait_Realtime(1250);
            EVC102_MMIStatusReport.Check_MMI_M_ACTIVE_CABIN = Variables.MMI_M_ACTIVE_CABIN.Cabin1Active;
            EVC102_MMIStatusReport.Check_MMI_M_MODE_READBACK = EVC102_MMIStatusReport.MMI_M_MODE_READBACK.StandBy;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays all ATP relevant information.");

            TraceHeader("Test Step 7");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Deactivate cabin");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following information:(1)    DMI removes all current ETCS objects related to indications to the driver.(2)    DMI returns to “idle” state. Use the log file to confirm that DMI sends out [MMI_STATUS_REPORT (EVC-102).MMI_M_MMI_STATUS] = 2 (Idle) to the ETC/ATP at least each 250ms");
            /*
            Test Step 7
            Action: Deactivate cabin
            Expected Result: Verify the following information:(1)    DMI removes all current ETCS objects related to indications to the driver.(2)    DMI returns to “idle” state. Use the log file to confirm that DMI sends out [MMI_STATUS_REPORT (EVC-102).MMI_M_MMI_STATUS] = 2 (Idle) to the ETC/ATP at least each 250ms
            Test Step Comment: (1) MMI_gen 2517;(2) MMI_gen 242 (partly: state return to “idle”);
            */
            DmiActions.Deactivate_Cabin(this);

            // Spec says DMI sends this out every 250 ms so wait 1s to ensure that packet is sent
            Wait_Realtime(250);
            EVC102_MMIStatusReport.Check_MMI_M_MMI_STATUS = EVC102_MMIStatusReport.MMI_M_MMI_STATUS.StatusIdle;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI removes all ETCS objects relevant to driver information.");

            TraceHeader("Test Step 8");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Press or touch any area on DMI screen");
            TraceReport("Expected Result");
            TraceInfo("Verify the following information:(1)    No response/reaction from touching on the DMI screen");
            /*
            Test Step 8
            Action: Press or touch any area on DMI screen
            Expected Result: Verify the following information:(1)    No response/reaction from touching on the DMI screen
            Test Step Comment: (1) MMI_gen 100;
            */
            DmiActions.ShowInstruction(this, "Press or touch any area on the DMI screen");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI does not respond to pressing or touching the screen.");

            TraceHeader("Test Step 9");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("End of test");
            
            /*
            Test Step 9
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
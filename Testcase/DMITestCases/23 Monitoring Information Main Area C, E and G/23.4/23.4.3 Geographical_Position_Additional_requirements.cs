using System;
using Testcase.Telegrams.EVCtoDMI;
using Testcase.Telegrams.DMItoEVC;

namespace Testcase.DMITestCases
{
    /// <summary>
    /// 23.4.3 Geographical Position: Additional requirements
    /// TC-ID: 18.4.3
    /// 
    /// 
    /// 
    /// Tested Requirements:
    /// MMI_gen 656; MMI_gen 2499;
    /// 
    /// Scenario:
    /// Drive the train forward past BG
    /// 1. Press the symbol DR03 in sub-area G12 and verify the packet information EVC-101 with display information of geographical position.
    /// BG1: Packet 79 (Geographical Position Information)
    /// Note: Perform this scenario for every configuration cycle time.
    /// De-activate cabin A. Then, that state of sub-area G12 is not sensitive and no information display in this area.
    /// Activate cabin A again and verify the display of symbol DR03 in sub-area G12.
    /// Power off ATP-CU and verify the display of symbol DR03 in sub-area G12
    /// 
    /// Used files:
    /// 18_4_3.tdg
    /// </summary>
    public class TC_18_4_3_Geographical_Position : TestcaseBase
    {
        public override bool TestcaseEntryPoint()
        {
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 24386;
            // Testcase entrypoint

            StartUp();
            DmiActions.Complete_SoM_L1_SR(this);

            TraceInfo("This test case requires a DMI configuration change;" + Environment.NewLine +
                      "GEOPOS_REQ_PERIOD = 0 (Send only one request via EVC-101) as opposed default of 1." +
                      Environment.NewLine +
                      "If this is not done manually, the test will fail!");

            #region Skipped due to repeated test steps of TC_18_4_1_Geographical_Position

            MakeTestStepHeader(1, UniqueIdentifier++, "Drive the train forward past BG1",
                "The symbol ‘DR03’ displays in sub-area G12");
            /*
            Test Step 1
            Action: Drive the train forward past BG1
            Expected Result: The symbol ‘DR03’ displays in sub-area G12
            */
            // Call generic Action Method

            // Call generic Check Results Method

            MakeTestStepHeader(2, UniqueIdentifier++, "Press ‘DR03’ symbol at sub-area G12",
                "Use the log file to confirm that DMI sent out only one packet of EVC-101 with variable MMI_M_REQUEST = 8 (Geographical position request)");
            /*
            Test Step 2
            Action: Press ‘DR03’ symbol at sub-area G12
            Expected Result: Use the log file to confirm that DMI sent out only one packet of EVC-101 with variable MMI_M_REQUEST = 8 (Geographical position request)
            Test Step Comment: (1) MMI_gen 656 (partly: press symbol DR03, cycle time is configured as 0);
            */

            #endregion

            MakeTestStepHeader(3, UniqueIdentifier++, "Perform the following procedure:",
                "DMI displays in SR mode, Level 1");
            /*
            Test Step 3
            Action: Perform the following procedure:
                    Power off test system
                    Set the variable GEOPOS_REQ_PERIOD = 1 in configuration file
                    Power on test system
                    Perform SoM in SR mode, Level 1
            Expected Result: DMI displays in SR mode, Level 1
            */
            // Call generic Check Results Method
            DmiExpectedResults.SR_Mode_displayed(this);

            MakeTestStepHeader(4, UniqueIdentifier++, "Drive the train forward pass BG1",
                "The symbol ‘DR03’ displays in sub-area G12");
            /*
            Test Step 4
            Action: Drive the train forward pass BG1
            Expected Result: The symbol ‘DR03’ displays in sub-area G12
            */
            // Call generic Action Method
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN = 1000000;
            EVC5_MMIGeoPosition.MMI_M_ABSOLUTPOS = 1000000;

            EVC30_MMIRequestEnable.SendBlank();

            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.No_window_specified;
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = Variables.standardFlags |
                                                               EVC30_MMIRequestEnable.EnabledRequests
                                                                   .GeographicalPosition;
            EVC30_MMIRequestEnable.Send();

            // Call generic Check Results Method
            DmiExpectedResults.Driver_symbol_displayed(this, "Geographical Position", "DR03", "G12", false);

            MakeTestStepHeader(5, UniqueIdentifier++, "Press ‘DR03’ symbol at sub-area G12",
                "Use the log file to confirm that DMI sent out packet of EVC-101 with variable MMI_M_REQUEST = 8 (Geographical position request) every 1 second.");
            /*
            Test Step 5
            Action: Press ‘DR03’ symbol at sub-area G12
            Expected Result: Use the log file to confirm that DMI sent out packet of EVC-101 with variable MMI_M_REQUEST = 8 (Geographical position request) every 1 second.
                The display of geographical is updated every 1 second according to received packet information from ETCS.
                Note: Stopwatch is required
            Test Step Comment: (1) MMI_gen 656 (partly: cyclically transmit a request, configurable 1 s);
                                (2) Information under MMI_gen 656;
            */
            DmiActions.ShowInstruction(this, @"Press on the ‘DR03’ symbol, on sub-area G12.");
            EVC101_MMIDriverRequest.CheckMRequestReleased = Variables.MMI_M_REQUEST.GeographicalPositionRequest;
            EVC5_MMIGeoPosition.Send();

            DmiActions.ShowInstruction(this,
                "Once you click ok, observe the Geographical Position indicator for approximately five seconds.");

            Wait_Realtime(1100);
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN = 9000000;

            Wait_Realtime(1100);
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN = 8000000;

            Wait_Realtime(1100);
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN = 7000000;

            Wait_Realtime(1100);
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN = 6000000;

            Wait_Realtime(1100);
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN = 5000000;

            Wait_Realtime(1100);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Did the Geographical Position value reduce by 1000000 approximately every second?");

            #region Skipped due to repeated cycle times not required

            MakeTestStepHeader(6, UniqueIdentifier++,
                "Repeat action step 3-5 for the remaining configuration of cycle time (GEOPOS_REQ_PERIOD = 2 to 10)",
                "Use the log file to confirm that DMI sent out packet of EVC-101 with variable MMI_M_REQUEST = 8 (Geographical position request) every X second.");
            /*
            Test Step 6
            Action: Repeat action step 3-5 for the remaining configuration of cycle time (GEOPOS_REQ_PERIOD = 2 to 10)
            Expected Result: Use the log file to confirm that DMI sent out packet of EVC-101 with variable MMI_M_REQUEST = 8 (Geographical position request) every X second.
                The display of geographical is updated every X second refer to received packet informaion from ETCS.
                Note:X is configured cycle time refer to value of GEOPOS_REQ_PERIOD
                Stopwatch is required
            Test Step Comment: (1) MMI_gen 656 (partly: cyclically transmit a request, configurable 2-10s);
                                (2) Information under MMI_gen 656;
            */

            #endregion

            MakeTestStepHeader(7, UniqueIdentifier++, "De-activate cabin A",
                "The geographical position in sub-area G12 is removed");
            /*
            Test Step 7
            Action: De-activate cabin A
            Expected Result: The geographical position in sub-area G12 is removed
            Test Step Comment: (1) MMI_gen 2499 (partly: remove the function show geographical position, MMI_gen 242);
            */
            DmiActions.Deactivate_Cabin(this);
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. There is no symbol displayed in area G12.");

            MakeTestStepHeader(8, UniqueIdentifier++, "Press at sub-area G12",
                "The symbol DR03 is not display in sub-area G12.");
            /*
            Test Step 8
            Action: Press at sub-area G12
            Expected Result: The symbol DR03 is not display in sub-area G12.
                Use the log file to confirm that DMI did not sent out packet EVC-101
            Test Step Comment: (1) MMI_gen 2499 (partly: not display symbol DR03, MMI_gen 242);
                                (2) MMI_gen 2499 (partly: not be sensitive, MMI_gen 242);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press at sub-area G12");
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. There is no symbol displayed in area G12.");

            MakeTestStepHeader(9, UniqueIdentifier++, "Activate cabin A", "The symbol ‘DR03’ displays in sub-area G12");
            /*
            Test Step 9
            Action: Activate cabin A
            Expected Result: The symbol ‘DR03’ displays in sub-area G12
            Test Step Comment: MMI_gen 2499 (partly: reset the default state of toggle to off, MMI_gen 242);
            */
            // Call generic Action Method
            DmiActions.Activate_Cabin_1(this);
            // Call generic Check Results Method
            DmiExpectedResults.Driver_symbol_displayed(this, "Geographical Position", "DR03", "G12", false);

            MakeTestStepHeader(10, UniqueIdentifier++, "Simulate loss-communication between ETCS onboard and DMI",
                "The geographical position in sub-area G12 is removed");
            /*
            Test Step 10
            Action: Simulate loss-communication between ETCS onboard and DMI
            Expected Result: The geographical position in sub-area G12 is removed
            Test Step Comment: (1) MMI_gen 2499 (partly: removal, MMI_gen 244);
            */
            // Call generic Action Method
            DmiActions.Simulate_communication_loss_EVC_DMI(this);
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. There is no symbol displayed in area G12.");

            MakeTestStepHeader(11, UniqueIdentifier++, "Press at sub-area G12",
                "The symbol DR03 is not display in sub-area G12.");
            /*
            Test Step 11
            Action: Press at sub-area G12
            Expected Result: The symbol DR03 is not display in sub-area G12.
                Use the log file to confirm that DMI did not sent out packet EVC-101
            Test Step Comment: (1) MMI_gen 2499 (partly: not display symbol DR03, MMI_gen 244);
                (2) MMI_gen 2499 (partly: not be sensitive, MMI_gen 244);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press at sub-area G12");
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. There is no symbol displayed in area G12.");

            MakeTestStepHeader(12, UniqueIdentifier++, "End of test", "");

            /*
            Test Step 12
            Action: End of test
            Expected Result: 
            */

            TraceInfo("Make sure to revert the configuration file of the DMI back to the default." +
                      Environment.NewLine +
                      "GEOPOS_REQ_PERIOD = 0 (Send only one request via EVC - 101.");

            return GlobalTestResult;
        }
    }
}
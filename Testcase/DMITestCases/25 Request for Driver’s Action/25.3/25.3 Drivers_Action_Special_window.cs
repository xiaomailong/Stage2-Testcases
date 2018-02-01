using System;
using System.Collections.Generic;
using Testcase.Telegrams.DMItoEVC;
using Testcase.Telegrams.EVCtoDMI;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 25.3 Driver’s Action: Special window
    /// TC-ID: 20.3
    /// 
    /// This test case verify that DMI sends values of [MMI_DRIVER_REQUEST (EVC-101).MMI_M_REQUEST] correctly when a driver presses on each button in Special window.
    /// 
    /// Tested Requirements:
    /// MMI_gen 151 (partly: MMI_M_REQUEST = 13, 12, 11, 10, 38);
    /// 
    /// Scenario:
    /// 1.At Special window, open and close the ‘SR speed/distance’ window. Then, verify the value in packet EVC-101 refer to each action.
    /// 2.Drive the train forward pass BG
    /// 1.Then, open the Adhesion window and verify the value in packet EVC-101 when the adhesion button is pressed.BG1: packet 3 (National value, Q_NVDRIVER_ADHES = 1)
    /// 3.Press and hold ‘Train integrity’ button at least 2 seconds. Then, verify the value in packet EVC-101 when released the button.
    /// 
    /// Used files:
    /// 20_3.tdg
    /// </summary>
    public class TC_ID_20_3_Drivers_Action_Special_window : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // Test system is powered on.Cabin is activated.SoM is performed in SR mode, Level 1.Special window is opened.

            // Call the TestCaseBase PreExecution
            base.PreExecution();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec

            // Call the TestCaseBase PostExecution
            base.PostExecution();

            // DMI displays in SR mode, Level 1
            DmiActions.Complete_SoM_L1_SR(this);
        }

        public override bool TestcaseEntryPoint()
        {
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 0;
            // Testcase entrypoint

            TraceHeader("Test Step 1");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Press the ‘SR speed/distance’ button");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following information,(1)    Use the log file to confirm that DMI sends out packet [MMI_DRIVER_REQUEST (EVC-101)] with variable MMI_M_REQUEST = 13 (Change SR Rules)(2)   The Special window is closed, DMI displays SR speed/distance window");
            /*
            Test Step 1
            Action: Press the ‘SR speed/distance’ button
            Expected Result: Verify the following information,(1)    Use the log file to confirm that DMI sends out packet [MMI_DRIVER_REQUEST (EVC-101)] with variable MMI_M_REQUEST = 13 (Change SR Rules)(2)   The Special window is closed, DMI displays SR speed/distance window
            Test Step Comment: (1) MMI_gen 151 (partly: MMI_M_REQUEST = 13);(2) MMI_gen 151 (partly: close opened menu);
            */
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Default;
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.SRSpeedDistance |
                                                               EVC30_MMIRequestEnable.EnabledRequests.Adhesion |
                                                               EVC30_MMIRequestEnable.EnabledRequests.TrainIntegrity;
            EVC30_MMIRequestEnable.Send();

            DmiActions.ShowInstruction(this, "Press the ‘Special’ button, then press the ‘SR speed/distance’ button");

            EVC101_MMIDriverRequest.CheckMRequestPressed = Variables.MMI_M_REQUEST.ChangeSRrules;
            EVC11_MMICurrentSRRules.MMI_M_BUTTONS = Variables.MMI_M_BUTTONS.BTN_CLOSE;
            EVC11_MMICurrentSRRules.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI closes the Special window and displays the SR speed/distance window.");

            TraceHeader("Test Step 2");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Press the ‘Close’ button");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following information,(1)    Use the log file to confirm that DMI sends out packet [MMI_DRIVER_REQUEST (EVC-101)] with variable MMI_M_REQUEST = 12 (Exit Change SR Rules)(2)   The SR speed/distance window is closed, DMI displays Special window");
            /*
            Test Step 2
            Action: Press the ‘Close’ button
            Expected Result: Verify the following information,(1)    Use the log file to confirm that DMI sends out packet [MMI_DRIVER_REQUEST (EVC-101)] with variable MMI_M_REQUEST = 12 (Exit Change SR Rules)(2)   The SR speed/distance window is closed, DMI displays Special window
            Test Step Comment: (1) MMI_gen 151 (partly: MMI_M_REQUEST = 12);(2) MMI_gen 151 (partly: close opened menu);
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button");

            EVC101_MMIDriverRequest.CheckMRequestPressed = Variables.MMI_M_REQUEST.ExitChangeSRrules;
            EVC11_MMICurrentSRRules.DataElements = new List<Variables.DataElement>
            {
                new Variables.DataElement {Identifier = 15, EchoText = "0", QDataCheck = 0},
                new Variables.DataElement {Identifier = 16, EchoText = "0", QDataCheck = 0}
            };
            EVC11_MMICurrentSRRules.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI closes the SR speed/distance window and displays the Special window.");

            TraceHeader("Test Step 3");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Drive the train forward pass BG1.Then, stop the train and press the ‘Special’ button");
            TraceReport("Expected Result");
            TraceInfo("DMI displays Special window with enabled Adhesion button");
            /*
            Test Step 3
            Action: Drive the train forward pass BG1.Then, stop the train and press the ‘Special’ button
            Expected Result: DMI displays Special window with enabled Adhesion button
            */
            // DMI_RS_ETCS says train must be at a standstill
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 0;

            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button, then press the ‘Special’ button");

            DmiExpectedResults.DMI_displays_Special_window_with_enabled_Adhesion_button(this);

            TraceHeader("Test Step 4");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Press the ‘Adhesion’ button.Then, select and confirm ‘Slippery rail’ button");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following information,(1)    Use the log file to confirm that DMI sends out packet [MMI_DRIVER_REQUEST (EVC-101)] with variable MMI_M_REQUEST = 11 (Set adhesion coefficient to ‘slippery rail’)(2)   The Adhesion window is closed, DMI displays Special window");
            /*
            Test Step 4
            Action: Press the ‘Adhesion’ button.Then, select and confirm ‘Slippery rail’ button
            Expected Result: Verify the following information,(1)    Use the log file to confirm that DMI sends out packet [MMI_DRIVER_REQUEST (EVC-101)] with variable MMI_M_REQUEST = 11 (Set adhesion coefficient to ‘slippery rail’)(2)   The Adhesion window is closed, DMI displays Special window
            Test Step Comment: (1) MMI_gen 151 (partly: MMI_M_REQUEST = 11);(2) MMI_gen 151 (partly: close opened menu);
            */
            // DMI_RS_ETCS says mode should be SB if National value does not allow other modes
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.StandBy;

            DmiActions.ShowInstruction(this,
                @"Press the ‘Adhesion’ button, then select and confirm the ‘Slippery rail’ button");

            EVC101_MMIDriverRequest.CheckMRequestPressed = Variables.MMI_M_REQUEST.SetAdhesionCoefficientToSlipperyRail;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI closes the Adhesion window and displays the Special window.");
            TraceHeader("Test Step 5");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Press the ‘Adhesion’ button.Then, select and confirm ‘Non slippery rail’ button");
            TraceReport("Expected Result");
            TraceInfo(
                "DMI displays Special window.Verify the following information,(1)    Use the log file to confirm that DMI sends out packet [MMI_DRIVER_REQUEST (EVC-101)] with variable MMI_M_REQUEST = 10 (Restore adhesion coefficient to ‘non-slippery rail’)(2)   The Adhesion window is closed, DMI displays Special window");
            /*
            Test Step 5
            Action: Press the ‘Adhesion’ button.Then, select and confirm ‘Non slippery rail’ button
            Expected Result: DMI displays Special window.Verify the following information,(1)    Use the log file to confirm that DMI sends out packet [MMI_DRIVER_REQUEST (EVC-101)] with variable MMI_M_REQUEST = 10 (Restore adhesion coefficient to ‘non-slippery rail’)(2)   The Adhesion window is closed, DMI displays Special window
            Test Step Comment: (1) MMI_gen 151 (partly: MMI_M_REQUEST = 10);(2) MMI_gen 151 (partly: close opened menu);
            */
            DmiActions.ShowInstruction(this,
                @"Press the ‘Adhesion’ button, then select and confirm the ‘Non slippery rail’ button");

            EVC101_MMIDriverRequest.CheckMRequestPressed =
                Variables.MMI_M_REQUEST.RestoreAdhesionCoefficientToNonSlipperyRail;
            ;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI closes the Adhesion window and displays the Special window.");

            TraceHeader("Test Step 6");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Perform the following procedure,Press and hold ‘Train integrity’ button at least 2 seconds.Release the pressed button");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following information,(1)    Use the log file to confirm that DMI sends out packet [MMI_DRIVER_REQUEST (EVC-101)] with variable MMI_M_REQUEST = 38 (Start procedure ‘Train Integrity’)(2)   The Special window is closed, DMI displays Default window");
            /*
            Test Step 6
            Action: Perform the following procedure,Press and hold ‘Train integrity’ button at least 2 seconds.Release the pressed button
            Expected Result: Verify the following information,(1)    Use the log file to confirm that DMI sends out packet [MMI_DRIVER_REQUEST (EVC-101)] with variable MMI_M_REQUEST = 38 (Start procedure ‘Train Integrity’)(2)   The Special window is closed, DMI displays Default window
            Test Step Comment: (1) MMI_gen 151 (partly: MMI_M_REQUEST = 38);(2) MMI_gen 151 (partly: close opened menu);
            */
            DmiActions.ShowInstruction(this,
                @"Press and hold the ‘Train integrity’ button for at least 2s, then release the pressed button");

            EVC101_MMIDriverRequest.CheckMRequestPressed = Variables.MMI_M_REQUEST.StartProcedureTrainIntegrity;

            // DMI_RS_ETCS says two packets for the separate button events need to be sent...
            Wait_Realtime(300);
            EVC101_MMIDriverRequest.CheckMRequestReleased = Variables.MMI_M_REQUEST.StartProcedureTrainIntegrity;

            TraceHeader("Test Step 7");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("End of test");
            TraceReport("Expected Result");
            TraceInfo("");
            /*
            Test Step 7
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
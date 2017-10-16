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
    public class TC_ID_18_1_1_1_2_Verbose_Visualization : TestcaseBase
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
            TraceInfo("This test case requires an ATP configuration change - " +
                      "See Precondition requirements. If this is not done manually, the test may fail!");

            /*
            Test Step 1
            Action: Activate Cabin A
            Expected Result: DMI displays Driver ID window.Verify that sub-area E1 is divided in two areas
            Test Step Comment: (1) MMI_gen 7527 (partly: divided in two areas);          MMI_gen 11459 (partly: verbose visualisation);
            */
            DmiActions.Activate_Cabin_1(this);

            EVC14_MMICurrentDriverID.MMI_X_DRIVER_ID = "1234";
            EVC14_MMICurrentDriverID.Send();

            WaitForVerification("Check the following:" + Environment.NewLine +
                                "1. DMI displays the Driver ID window." + Environment.NewLine +
                                "2. Sub-area E1 is displayed divided into two areas.");

            /*
            Test Step 2
            Action: Perform SoM in SR mode, Level 2
            Expected Result: DMI displays Connection established symbol (ST103) in the left part of sub-area E1.Use the log file to confirm that DMI receives packet information EVC-8 with variable MMI_DRIVER_MESSAGE.MMI_Q_TEXT = 568 (ST03 symbol)
            Test Step Comment: (1) MMI_gen 2576 (partly: verbose visualisation, connection established); MMI_gen 1855 (partly: connection established);  MMI_gen 11459 (partly: verbose visualisation); MMI_gen 7022 (partly: Radio connection symbols); MMI_gen 3005 (partly: Radio connection symbols);
            */
            DmiActions.Complete_SoM_L1_SR(this);
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Level = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_LEVEL.L2;

            // Spec says 568 Connection established ST103 but 568 is ST03
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 568;
            EVC8_MMIDriverMessage.PlainTextMessage = "";
            EVC8_MMIDriverMessage.Send();
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN = 0;

            WaitForVerification("Check the following:" + Environment.NewLine +
                                "1. DMI displays the ‘Connection established’ symbol (ST03) in the left part of sub-area E1.");

            /*
            Test Step 3
            Action: Drive the train forward with speed below the permitted speed
            Expected Result: The train is moving forward, position is increase.The speed pointer displays the current speed
            */
            EVC1_MMIDynamic.MMI_V_PERMITTED_KMH = 10;
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 5;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN = 50;

            WaitForVerification("Check the following:" + Environment.NewLine +
                                "1. The train has moved forward." + Environment.NewLine +
                                "2. The speed pointer displays the current speed.");

            /*
            Test Step 4
            Action: Receives FS MA and track description from RBC
            Expected Result: DMI displays in FS mode, Level 2
            */
            // No display changes noted so ignoring any update of track, et c.
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.FullSupervision;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Level = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_LEVEL.L2;

            WaitForVerification("Check the following:" + Environment.NewLine +
                                "1. DMI displays in FS mode, Level 2");

            /*
            Test Step 5
            Action: Receives RBC transition order from RBC
            Expected Result: DMI displays Connection Up with two RBCs symbol (ST03B) in the left part of sub-area E1.Use the log file to confirm that DMI receives packet information EVC-8 with variable MMI_DRIVER_MESSAGE.MMI_Q_TEXT = 614.DMI displays Network registered via two modems symbol (ST102) in the right part of sub-area E1.Use the log file to confirm that DMI receives packet information EVC-8 with variable MMI_DRIVER_MESSAGE.MMI_Q_TEXT = 610
            Test Step Comment: (1) MMI_gen 2576 (partly: verbose visualisation, Connection Up with two RBCs); MMI_gen 1855 (partly: connection established);  MMI_gen 11459 (partly: verbose visualisation); MMI_gen 11442 (partly: connection Up with wto RBC);(2) MMI_gen 2576 (partly: verbose visualisation, Network registered via two modems); MMI_gen 1855 (partly: connection established);  MMI_gen 11459 (partly: verbose visualisation); MMI_gen 11442 (partly: Network registration via two modems); MMI_gen 7022 (partly: Radio connection symbols); MMI_gen 3005 (partly: Radio connection symbols);
            */
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 614;
            EVC8_MMIDriverMessage.Send();

            EVC8_MMIDriverMessage.MMI_I_TEXT = 2;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 610;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine +
                                "1. DMI displays the ‘Connection Up with two RBCs’ symbol (ST03B) in the left part of sub-area E1." + Environment.NewLine +
                                "2. DMI displays the ‘Network registered via two modems’ symbol (ST03B) in the right part of sub-area E1.");

            /*
            Test Step 6
            Action: Receive Terminate communication session from RBC
            Expected Result: DMI displays Connection Up symbol (ST103) in the left part of sub-area E1.Use the log file to confirm that DMI receives packet information EVC-8 with variable MMI_DRIVER_MESSAGE.MMI_Q_TEXT = 613 (ST03 symbol).DMI displays Network registered via one modem symbol (ST100) in the right part of sub-area E1.Use the log file to confirm that DMI receives packet information EVC-8 with variable MMI_DRIVER_MESSAGE.MMI_Q_TEXT = 609
            Test Step Comment: (1) MMI_gen 2576 (partly: verbose visualisation, Connection Up with two RBCs); MMI_gen 1855 (partly: connection up);  MMI_gen 11459 (partly: verbose visualisation); MMI_gen 11442 (partly: connection up with one RBC);(2) MMI_gen 2576 (partly: verbose visualisation, Network registered via one modem); MMI_gen 1855 (partly: connection established);  MMI_gen 11459 (partly: verbose visualisation); MMI_gen 11442 (partly: Network registration via one modem); MMI_gen 7022 (partly: Radio connection symbols); MMI_gen 3005 (partly: Radio connection symbols);
            */
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 613;
            EVC8_MMIDriverMessage.Send();

            EVC8_MMIDriverMessage.MMI_I_TEXT = 2;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 609;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine +
                                "1. DMI displays the ‘Connection Up’ symbol (ST103) in the left part of sub-area E1." + Environment.NewLine +
                                "2. DMI displays the ‘Network registered via one modem’ symbol (ST100) in the right part of sub-area E1.");

            /*
            Test Step 7
            Action: Simulate RBC communication loss and wait for a few secondsNote: This simulation is performed automatically by UTT file
            Expected Result: DMI displays Connection Lost/Set-Up failed symbol (ST03C) in the left part of sub-area E1.Use the log file to confirm that DMI receives packet information EVC-8 with variable MMI_DRIVER_MESSAGE.MMI_Q_TEXT = 282 (ST04 symbol)
            Test Step Comment: (1) MMI_gen 2576 (partly: verbose visualisation, connection lost); MMI_gen 1855 (partly: connection established);  MMI_gen 11459 (partly: verbose visualisation); MMI_gen 7022 (partly: ST03C, MMI_Q_TEXT = 615); MMI_gen 3005 (partly: ST03C, MMI_Q_TEXT = 615);
            */
            DmiActions.Simulate_communication_loss_EVC_DMI(this);

            Wait_Realtime(2000);

            // Spec says MMI_Q_TEXT = 282 (ST04) as displayed: should sending ST03C (615) display ST04?
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 282;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine +
                                "1. DMI displays the ‘Connection Lost/Set-Up failed’ symbol (ST03C) in sub-area E1.");

            /*
            Test Step 8
            Action: Re-establish the radio communication.Note: This simulation is performed automatically by UTT file
            Expected Result: DMI displays Connection established symbol (ST103) in the left part of sub-area E1.Use the log file to confirm that DMI receives packet information EVC-8 with variable MMI_DRIVER_MESSAGE.MMI_Q_TEXT = 613
            Test Step Comment: (1) MMI_gen 2576 (partly: verbose visualisation, connection up); MMI_gen 1855 (partly: connection established); MMI_gen 11459 (partly: verbose visualisation); MMI_gen 7022 (partly: Radio connection symbols); MMI_gen 3005 (partly: Radio connection symbols);
            */
            DmiActions.Re_establish_communication_EVC_DMI(this);

            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 613;
            EVC8_MMIDriverMessage.Send();

            // Spec says ST03 symbol, Connection established but 613 is ST103 Connection Up: does this make a difference?
            WaitForVerification("Check the following:" + Environment.NewLine +
                                "1. DMI displays the ‘Connection Up’ symbol (ST103) in sub-area E1.");

            /*
            Test Step 9
            Action: End of test
            Expected Result: 
            */


            return GlobalTestResult;
        }
    }
}
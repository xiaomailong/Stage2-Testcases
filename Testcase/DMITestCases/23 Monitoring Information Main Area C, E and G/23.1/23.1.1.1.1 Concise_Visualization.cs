using System;
using Testcase.Telegrams.EVCtoDMI;


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
    public class TC_ID_18_1_1_1_1_Concise_Visualization : TestcaseBase
    {
        public override bool TestcaseEntryPoint()
        {
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 24280;
            // Testcase entrypoint
            TraceInfo("This test case requires an ATP configuration change - " +
                      "See Precondition requirements. If this is not done manually, the test may fail!");

            MakeTestStepHeader(1, UniqueIdentifier++, "Activate Cabin A", "DMI displays Driver ID window");
            /*
            Test Step 1
            Action: Activate Cabin A
            Expected Result: DMI displays Driver ID window
            */
            StartUp();

            EVC14_MMICurrentDriverID.MMI_X_DRIVER_ID = "1234";
            EVC14_MMICurrentDriverID.Send();

            DmiExpectedResults.Driver_ID_window_displayed(this);

            MakeTestStepHeader(2, UniqueIdentifier++, "Enter Driver ID",
                "No symbol display in sub area E1.Use the log file to confirm that there is no packet information EVC-8 with variable MMI_Q_TEXT = 282 (ST04 symbol) and MMI_Q_TEXT = 568 (ST03 symbol) send to DMI");
            /*
            Test Step 2
            Action: Enter Driver ID
            Expected Result: No symbol display in sub area E1.Use the log file to confirm that there is no packet information EVC-8 with variable MMI_Q_TEXT = 282 (ST04 symbol) and MMI_Q_TEXT = 568 (ST03 symbol) send to DMI
            Test Step Comment: MMI_gen 2576 (partly: concise visualisation); MMI_gen 11459 (partly: concise visualisation);
            */
            DmiActions.ShowInstruction(this, "Enter the Driver ID");

            WaitForVerification("Check the following:" + Environment.NewLine +
                                "1. No symbol is displayed in sub-area E1");

            MakeTestStepHeader(3, UniqueIdentifier++, "Perform SoM in SR mode, Level 2",
                "DMI displays Connection established symbol (ST03) in sub area E1.Use the log file to confirm that DMI receives packet information EVC-8 with variable MMI_DRIVER_MESSAGE.MMI_Q_TEXT = 568 (ST03 symbol)");
            /*
            Test Step 3
            Action: Perform SoM in SR mode, Level 2
            Expected Result: DMI displays Connection established symbol (ST03) in sub area E1.Use the log file to confirm that DMI receives packet information EVC-8 with variable MMI_DRIVER_MESSAGE.MMI_Q_TEXT = 568 (ST03 symbol)
            Test Step Comment: (1) MMI_gen 2576 (partly: concise visualisation, connection established); MMI_gen 1855 (partly: connection established);  MMI_gen 11459 (partly: concise visualisation); 
            */
            DmiActions.Complete_SoM_L1_SR(this);
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Level = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_LEVEL.L2;

            // or, if this is really needed:
            /*
            DmiActions.Display_Driver_ID_Window(this);
            DmiActions.Set_Driver_ID(this, "1234");
            DmiActions.Send_SB_Mode(this);
            DmiActions.ShowInstruction(this, "Enter and confirm Driver ID");

            DmiActions.Request_Brake_Test(this);
            DmiActions.ShowInstruction(this, "Perform Brake Test");

            DmiActions.Display_Level_Window(this);
            DmiActions.ShowInstruction(this, "Select and enter Level 2");
            DmiActions.Display_Main_Window_with_Start_button_not_enabled(this);
            DmiActions.ShowInstruction(this, @"Press ‘Train data’ button");
            DmiActions.Display_Train_Data_Window(this);
            DmiActions.ShowInstruction(this, @"Perform the following actions on the DMI: " + Environment.NewLine + Environment.NewLine +
                                  "1. Enter and confirm value in each input field." + Environment.NewLine +
                                  "2. Press ‘Yes’ button.");
            DmiActions.Display_Train_data_validation_Window(this);
            DmiActions.ShowInstruction(this, @"Perform the following actions on the DMI: " + Environment.NewLine + Environment.NewLine +
                                             @"1. Press ‘Yes’ button." + Environment.NewLine +
                                             "2. Confirmed the selected value by pressing the input field.");
            DmiActions.Display_TRN_Window(this);
            DmiActions.ShowInstruction(this, "Enter and confirm Train Running Number");
            DmiActions.Display_Main_Window_with_Start_button_enabled(this);
            DmiActions.ShowInstruction(this, @"Press ‘Start’ button");
            DmiActions.Send_SR_Mode_Ack(this);
            DmiActions.ShowInstruction(this, "Press and hold DMI Sub Area C1");
            DmiActions.Send_SR_Mode(this);
            DmiActions.Finished_SoM_Default_Window(this);
            */
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN = 0;

            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 568;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine +
                                "1. DMI displays the ‘Connection established’ symbol (ST03) in sub-area E1.");

            MakeTestStepHeader(4, UniqueIdentifier++, "Drive the train forward with speed below the permitted speed",
                "The train is moving forward, position is increase.The speed pointer displays the current speed");
            /*
            Test Step 4
            Action: Drive the train forward with speed below the permitted speed
            Expected Result: The train is moving forward, position is increase.The speed pointer displays the current speed
            */
            EVC1_MMIDynamic.MMI_V_PERMITTED_KMH = 10;
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 5;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN = 50;

            WaitForVerification("Check the following:" + Environment.NewLine +
                                "1. The train has moved forward." + Environment.NewLine +
                                "2. The speed pointer displays the current speed.");

            MakeTestStepHeader(5, UniqueIdentifier++, "Receives FS MA and track description from RBC",
                "DMI displays in FS mode, Level 2");
            /*
            Test Step 5
            Action: Receives FS MA and track description from RBC
            Expected Result: DMI displays in FS mode, Level 2
            */
            // No display changes noted so ignoring any update of track, et c.
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.FullSupervision;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Level = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_LEVEL.L2;

            WaitForVerification("Check the following:" + Environment.NewLine +
                                "1. DMI displays in FS mode, Level 2");

            MakeTestStepHeader(6, UniqueIdentifier++,
                "Simulate RBC communication loss and wait for a few secondsNote: This simulation is perform automatically by UTT file",
                "DMI displays Connection Lost/Set-Up failed symbol (ST04) in sub area E1.Use the log file to confirm that DMI receives packet information EVC-8 with variable MMI_DRIVER_MESSAGE.MMI_Q_TEXT = 282 (ST04 symbol)");
            /*
            Test Step 6
            Action: Simulate RBC communication loss and wait for a few secondsNote: This simulation is perform automatically by UTT file
            Expected Result: DMI displays Connection Lost/Set-Up failed symbol (ST04) in sub area E1.Use the log file to confirm that DMI receives packet information EVC-8 with variable MMI_DRIVER_MESSAGE.MMI_Q_TEXT = 282 (ST04 symbol)
            Test Step Comment: (1) MMI_gen 2576 (partly: concise visualisation, connection lost); MMI_gen 1855 (partly: connection established);  MMI_gen 11459 (partly: concise visualisation); MMI_gen 7022 (partly: Radio connection symbols); MMI_gen 3005 (partly: Radio connection symbols);
            */
            DmiActions.Simulate_communication_loss_EVC_DMI(this);

            Wait_Realtime(2000);

            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 282;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine +
                                "1. DMI displays the ‘Connection Lost/Set-Up failed’ symbol (ST04) in sub-area E1.");

            MakeTestStepHeader(7, UniqueIdentifier++,
                "Re-establish the radio communication.Note: This simulation is perform automatically by UTT file",
                "DMI displays Connection established symbol (ST03) in sub area E1.Use the log file to confirm that DMI receives packet information EVC-8 with variable MMI_DRIVER_MESSAGE.MMI_Q_TEXT = 613");
            /*
            Test Step 7
            Action: Re-establish the radio communication.Note: This simulation is perform automatically by UTT file
            Expected Result: DMI displays Connection established symbol (ST03) in sub area E1.Use the log file to confirm that DMI receives packet information EVC-8 with variable MMI_DRIVER_MESSAGE.MMI_Q_TEXT = 613
            Test Step Comment: (1) MMI_gen 2576 (partly: concise visualisation, connection up); MMI_gen 1855 (partly: connection established);  MMI_gen 11459 (partly: concise visualisation); MMI_gen 7022 (partly: Radio connection symbols); MMI_gen 3005 (partly: Radio connection symbols);
            */
            DmiActions.Re_establish_communication_EVC_DMI(this);

            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 613;
            EVC8_MMIDriverMessage.Send();

            // Spec says ST03 symbol, Connection established but 613 is ST103 Connection Up
            WaitForVerification("Check the following:" + Environment.NewLine +
                                "1. DMI displays the ‘Connection established’ symbol (ST03) in sub-area E1.");

            MakeTestStepHeader(8, UniqueIdentifier++,
                "Perform the following procedure,Stop the trainDe-activate Cabin A.Activate Cabin A",
                "The symbol in sub area E1 is removed");
            /*
            Test Step 8
            Action: Perform the following procedure,Stop the trainDe-activate Cabin A.Activate Cabin A
            Expected Result: The symbol in sub area E1 is removed
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 0;

            DmiActions.Deactivate_Cabin(this);
            DmiActions.Activate_Cabin_1(this);

            WaitForVerification("Check the following:" + Environment.NewLine +
                                "1. DMI does not display the ‘Connection established’ symbol (ST04) in sub-area E1.");

            MakeTestStepHeader(9, UniqueIdentifier++,
                "Use the test script file 18_1_1_1_1_a.xml to send EVC-8 with,MMI_Q_TEXT_CLASS = 1 MMI_Q_TEXT_CRITERIA = 3MMI_Q_TEXT = 610",
                "No symbol display in sub area E1");
            /*
            Test Step 9
            Action: Use the test script file 18_1_1_1_1_a.xml to send EVC-8 with,MMI_Q_TEXT_CLASS = 1 MMI_Q_TEXT_CRITERIA = 3MMI_Q_TEXT = 610
            Expected Result: No symbol display in sub area E1
            Test Step Comment: MMI_gen 2576 (partly: concise visualisation, Network registered via two modems); MMI_gen 1855 (partly: connection established);  MMI_gen 11459 (partly: concise visualisation);
            */
            XML_18_1_1_1_1_a();

            WaitForVerification("Check the following:" + Environment.NewLine +
                                "1. DMI does not display a symbol in sub-area E1.");

            MakeTestStepHeader(10, UniqueIdentifier++,
                "(Continue from step 9) Send EVC-8 with,MMI_Q_TEXT_CLASS = 1 MMI_Q_TEXT_CRITERIA = 3MMI_Q_TEXT = 609",
                "No symbol display in sub area E1");
            /*
            Test Step 10
            Action: (Continue from step 9) Send EVC-8 with,MMI_Q_TEXT_CLASS = 1 MMI_Q_TEXT_CRITERIA = 3MMI_Q_TEXT = 609
            Expected Result: No symbol display in sub area E1
            Test Step Comment: MMI_gen 2576 (partly: concise visualisation, Network registered via one modems); MMI_gen 1855 (partly: connection established);  MMI_gen 11459 (partly: concise visualisation); 
            */
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 609;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine +
                                "1. DMI does not display a symbol in sub-area E1.");

            MakeTestStepHeader(11, UniqueIdentifier++,
                "Use the test script file 18_1_1_1_1_b.xml to send EVC-8 with,MMI_Q_TEXT_CLASS = 1 MMI_Q_TEXT_CRITERIA = 3MMI_Q_TEXT = 614",
                "DMI displays Connection established symbol (ST03) in sub area E1");
            /*
            Test Step 11
            Action: Use the test script file 18_1_1_1_1_b.xml to send EVC-8 with,MMI_Q_TEXT_CLASS = 1 MMI_Q_TEXT_CRITERIA = 3MMI_Q_TEXT = 614
            Expected Result: DMI displays Connection established symbol (ST03) in sub area E1
            Test Step Comment: MMI_gen 2576 (partly: concise visualisation, Connection up with two RBCs); MMI_gen 1855 (partly: connection established);  MMI_gen 11459 (partly: concise visualisation); MMI_gen 7022 (partly: Radio connection symbols); MMI_gen 3005 (partly: Radio connection symbols);
            */
            XML_18_1_1_1_1_b();

            WaitForVerification("Check the following:" + Environment.NewLine +
                                "1. DMI displays the ‘Connection established with two RBCs’ symbol (ST03B) in sub-area E1.");

            MakeTestStepHeader(12, UniqueIdentifier++, "End of test", "");

            /*
            Test Step 12
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }


        #region Send_XML_18_1_1_1_1_DMI_Test_Specification

        private void XML_18_1_1_1_1_a()
        {
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 610;
            EVC8_MMIDriverMessage.PlainTextMessage = "";
            EVC8_MMIDriverMessage.Send();
        }

        private void XML_18_1_1_1_1_b()
        {
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 614;
            EVC8_MMIDriverMessage.Send();
        }

        #endregion
    }
}
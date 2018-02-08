using System;
using Testcase.Telegrams.EVCtoDMI;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 11.3 Acknowledgements: Priority of new incoming acknowledgements
    /// TC-ID: 6.3
    /// 
    /// This test case verifies the display of acknowledgements refer to group of priority which specified in [MMI-ETCS-gen]. The maximum of an amount for pending acknowledgements is 10, remove the oldest acknowledgement if the list is overflow.
    /// 
    /// Tested Requirements:
    /// MMI_gen 4484; MMI_gen 4482; MMI_gen 4486; MMI_gen 4498; MMI_gen 4485 (partly: ETCS Onboard); MMI_gen 6923;
    /// 
    /// Scenario:
    /// 1.Use the test script file to send a packet information EVC-8.Then, verify the display of acknowledgement on DMI.
    /// 2.Use the test script file to send a packet information EVC-8.Then, press an acknowledgement in specify area and verify the display of acknowledgement on DMI.
    /// 
    /// Used files:
    /// 6_3_a.xml, 6_3_b.xml, 6_3_c.xml, 6_3_d.xml
    /// </summary>
    public class TC_ID_6_3_Acknowledgements : TestcaseBase
    {
        public override bool TestcaseEntryPoint()
        {
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 20816;
            // Testcase entrypoint


            // System is powered onCabin is activatedPerform SoM until level 1 is selected and confirmedMain window is closed.
            StartUp();

            // System is powered on
            // Cabin is activated
            // Perform SoM until level 1 is selected and confirmed
            // Main window is closed.
            DmiActions.Complete_SoM_L1_SB(this);

            MakeTestStepHeader(1, UniqueIdentifier++, "Use the test script file 6_3_a.xml to send EVC-8 with,",
                "DMI displays the text message ‘Emergency stop’ in sub-area E5");
            /*
            Test Step 1
            Action: Use the test script file 6_3_a.xml to send EVC-8 with,
            MMI_Q_TEXT = 280
            MMI_Q_TEXT_CRITERIA = 1
            MMI_I_TEXT = 1
            Expected Result: DMI displays the text message ‘Emergency stop’ in sub-area E5
            */
            XML_6_3(msgType.typea);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the message ‘Emergency stop’ in sub-area E5.");


            MakeTestStepHeader(2, UniqueIdentifier++, "(Continue from step 1)Send EVC-8 with, ",
                "Verify the following information,");
            /*
            Test Step 2
            Action: (Continue from step 1)Send EVC-8 with, 
            MMI_Q_TEXT = 257
            MMI_Q_TEXT_CRITERIA = 1
            MMI_I_TEXT = 2
            MMI_N_TEXT = 1
            MMI_X_TEXT = 0
            Expected Result: Verify the following information,
            (1)   The text message in sub-area E5 is disappeared and DMI displays LE07 symbol with yellow flashing frame in sub-area C1 instead
            Test Step Comment: (1) MMI_gen 4484;
            */
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 280;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 4;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.Send();

            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 2;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 257;
            EVC8_MMIDriverMessage.PlainTextMessage = "0"; // MMI_N_TEXT (length) 1
            EVC8_MMIDriverMessage.Send();

            DmiExpectedResults.Driver_symbol_deleted(this, "Emergency stop", "text message", "E5");
            DmiExpectedResults.Driver_symbol_displayed(this, "L0 announcement", "LE07", "C1", true);


            MakeTestStepHeader(3, UniqueIdentifier++, "(Continue from step 2)Send EVC-8 with, ",
                "DMI displays LE11 symbol with yellow flashing frame in sub-area C1");
            /*
            Test Step 3
            Action: (Continue from step 2)Send EVC-8 with, 
            MMI_Q_TEXT = 257
            MMI_Q_TEXT_CRITERIA = 1
            MMI_I_TEXT = 3
            MMI_N_TEXT = 1
            MMI_X_TEXT = 1
            Expected Result: DMI displays LE11 symbol with yellow flashing frame in sub-area C1
            */
            // Remove LE07
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 4;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 2;
            EVC8_MMIDriverMessage.Send();

            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 3;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 257;
            EVC8_MMIDriverMessage.PlainTextMessage = "1"; // MMI_N_TEXT (length) 1
            EVC8_MMIDriverMessage.Send();

            DmiExpectedResults.Driver_symbol_deleted(this, "L0 announcement", "LE07", "C1");
            DmiExpectedResults.Driver_symbol_displayed(this, "L1 announcement", "LE11", "C1", true);


            MakeTestStepHeader(4, UniqueIdentifier++, "(Continue from step 3)Send EVC-8 with, ",
                "Verify the following information,");
            /*
            Test Step 4
            Action: (Continue from step 3)Send EVC-8 with, 
            MMI_Q_TEXT = 259
            MMI_Q_TEXT_CRITERIA = 1
            MMI_I_TEXT = 4
            Expected Result: Verify the following information,
            (1)   DMI displays MO08 symbol with yellow flashing frame in sub-area C1 instead of LE11 symbol
            Test Step Comment: (1) MMI_gen 4484;
            */
            // Remove LE11
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 4;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 3;
            EVC8_MMIDriverMessage.Send();

            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 4;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 259;
            EVC8_MMIDriverMessage.PlainTextMessage = "";
            EVC8_MMIDriverMessage.Send();

            DmiExpectedResults.Driver_symbol_deleted(this, "L0 announcement", "LE11", "C1");
            DmiExpectedResults.Driver_symbol_displayed(this, "Acknowledgement for On Sight mode", "MO08", "C1", true);


            MakeTestStepHeader(5, UniqueIdentifier++, "(Continue from step 4)Send EVC-8 with, ",
                "Verify the following information,");
            /*
            Test Step 5
            Action: (Continue from step 4)Send EVC-8 with, 
            MMI_Q_TEXT = 298
            MMI_Q_TEXT_CRITERIA = 1
            MMI_I_TEXT = 5
            Expected Result: Verify the following information,
            (1)   The symbol in sub-area C1 is disappeared and DMI displays the symbol DR02 in area D instead
            Test Step Comment: (1) MMI_gen 4484;
            */
            // Remove M008
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 4;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 4;
            EVC8_MMIDriverMessage.Send();

            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 5;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 298;
            EVC8_MMIDriverMessage.Send();

            DmiExpectedResults.Driver_symbol_deleted(this, "Acknowledgement for On Sight mode", "MO08", "C1");
            DmiExpectedResults.Driver_symbol_displayed(this, "Track Ahead Free", "DR02", "D", true);


            MakeTestStepHeader(6, UniqueIdentifier++, "(Continue from step 5)Send EVC-8 with, ",
                "Verify the following information,");
            /*
            Test Step 6
            Action: (Continue from step 5)Send EVC-8 with, 
            MMI_Q_TEXT = 260
            MMI_Q_TEXT_CRITERIA = 1
            MMI_I_TEXT = 6
            Expected Result: Verify the following information,
            (1)   The symbol in area D is disappeared and DMI displays the ST01 symbol on sub-area C9 instead
            Test Step Comment: (1) MMI_gen 4484;
            */
            // Remove DR02
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 4;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 5;
            EVC8_MMIDriverMessage.Send();

            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 6;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 260;
            EVC8_MMIDriverMessage.Send();

            DmiExpectedResults.Driver_symbol_deleted(this, "Track Ahead Free", "DR02", "D");
            DmiExpectedResults.Driver_symbol_displayed(this, "Emergency Brake Intervention", "ST01", "C9", true);


            MakeTestStepHeader(7, UniqueIdentifier++, "Use the test script file 6_3_b.xml to send EVC-8 with,",
                "Verify the following information,");
            /*
            Test Step 7
            Action: Use the test script file 6_3_b.xml to send EVC-8 with,
            MMI_Q_TEXT = 264
            MMI_Q_TEXT_CRITERIA = 1
            MMI_I_TEXT = 7
            Expected Result: Verify the following information,
            (1)   The display information on DMI still not change, ST01 symbol is displayed on sub-area C9
            Test Step Comment: (1) MMI_gen 4484 (partly: NEGATIVE, lower priority, focus not moved);
            */

            XML_6_3(msgType.typeb);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI display does not change and still displays symbol ST01 in sub-area C9.");

            MakeTestStepHeader(8, UniqueIdentifier++, "(Continue from step 7)Send EVC-8 with, ",
                "The display information on DMI still not change, ST01 symbol is displayed on sub-area C9");
            /*
            Test Step 8
            Action: (Continue from step 7)Send EVC-8 with, 
            MMI_Q_TEXT = 269
            MMI_Q_TEXT_CRITERIA = 1
            MMI_I_TEXT = 8
            Expected Result: The display information on DMI still not change, ST01 symbol is displayed on sub-area C9
            */
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 8;
            //EVC8_MMIDriverMessage.MMI_Q_TEXT = 269;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 265;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI display does not change and still displays symbol ST01 in sub-area C9.");

            MakeTestStepHeader(9, UniqueIdentifier++, "(Continue from step 8)Send EVC-8 with, ",
                "The display information on DMI still not change, ST01 symbol is displayed on sub-area C9");
            /*
            Test Step 9
            Action: (Continue from step 8)Send EVC-8 with, 
            MMI_Q_TEXT = 268
            MMI_Q_TEXT_CRITERIA = 1
            MMI_I_TEXT = 9
            Expected Result: The display information on DMI still not change, ST01 symbol is displayed on sub-area C9
            */
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 9;
            //EVC8_MMIDriverMessage.MMI_Q_TEXT = 268;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 266;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI display does not change and still displays symbol ST01 in sub-area C9.");

            MakeTestStepHeader(10, UniqueIdentifier++, "(Continue from step 9)Send EVC-8 with, ",
                "The display information on DMI still not change, ST01 symbol is displayed on sub-area C9");
            /*
            Test Step 10
            Action: (Continue from step 9)Send EVC-8 with, 
            MMI_Q_TEXT = 267
            MMI_Q_TEXT_CRITERIA = 1
            MMI_I_TEXT = 10
            Expected Result: The display information on DMI still not change, ST01 symbol is displayed on sub-area C9
            */
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 10;
            //EVC8_MMIDriverMessage.MMI_Q_TEXT = 267;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 2636;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI display does not change and still displays symbol ST01 in sub-area C9.");

            MakeTestStepHeader(11, UniqueIdentifier++, "Use the test script file 6_3_c.xml to send EVC-8 with,",
                "The display information on DMI still not change, ST01 symbol is displayed on sub-area C9");
            /*
            Test Step 11
            Action: Use the test script file 6_3_c.xml to send EVC-8 with,
            MMI_Q_TEXT = 554
            MMI_Q_TEXT_CRITERIA = 1
            MMI_I_TEXT = 11
            Expected Result: The display information on DMI still not change, ST01 symbol is displayed on sub-area C9
            */
            XML_6_3(msgType.typec);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI display does not change and still displays symbol ST01 in sub-area C9.");

            MakeTestStepHeader(12, UniqueIdentifier++, "Use the test script file 6_3_d.xml to send EVC-8 with,",
                "Verify the following information,");
            /*
            Test Step 12
            Action: Use the test script file 6_3_d.xml to send EVC-8 with,
            MMI_Q_TEXT_CRITERIA = 4
            MMI_I_TEXT = 6
            Then, press at sub-area C9
            Expected Result: Verify the following information,
            (1)   The symbol ST01 in sub-area C9 is removed.
            (2)   Use the log file to confirm that DMI is not send out packet EVC-111.
            (3)   After 1 second, the symbol DR02 is displayed on area D
            Test Step Comment: (1) MMI_gen 4485 (partly: ETCS Onboard); MMI_gen 4498 (partly: disappear);(2) MMI_gen 4498 (partly: sensitive area is removed);(3) MMI_gen 4498 (partly: reappear, next pending acknowledgement);
            */
            XML_6_3(msgType.typed);

            DmiActions.ShowInstruction(this, "Press in sub-area C9");

            DmiExpectedResults.Driver_symbol_deleted(this, "Emergency Brake Intervention", "ST01", "C9");

            // ?? Check for an 'empty' packet
            //Telegrams.DMItoEVC.EVC111_MMIDriverMessageAck.Check_MMI_Q_ACK = Telegrams.DMItoEVC.EVC111_MMIDriverMessageAck.MMI_Q_ACK.Spare;
            Wait_Realtime(1000);
            EVC8_MMIDriverMessage.MMI_I_TEXT = 12;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 298;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.Send();

            DmiExpectedResults.Driver_symbol_displayed(this, "Track Ahead Free", "DR02", "D", true);


            MakeTestStepHeader(13, UniqueIdentifier++, "Confirm an acknow.ledgement of TAF by pressing at area D",
                "Verify the following information,");
            /*
            Test Step 13
            Action: Confirm an acknow.ledgement of TAF by pressing at area D
            Expected Result: Verify the following information,
            (1)    The symbols are removed refer to pressed area. 
                   DMI displays the symbol MO08 in sub-area C1. (The oldest entry of the highest priority in the list)
            Test Step Comment: (1) MMI_gen 4486 (partly: mode acknowledgement); MMI_gen 4482 (moveable focus);
            */
            DmiActions.ShowInstruction(this, "Acknowledge TAF by pressing in area D");
            DmiExpectedResults.TAF_ack_pressed_and_released(this);

            EVC8_MMIDriverMessage.MMI_I_TEXT = 12;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 4;
            EVC8_MMIDriverMessage.Send();

            DmiExpectedResults.Driver_symbol_deleted(this, "Track Ahead Free", "DR02", "D");

            EVC8_MMIDriverMessage.MMI_Q_TEXT = 259;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.Send();

            DmiExpectedResults.Driver_symbol_displayed(this, "Acknowledgement for On Sight", "MO08", "C1", true);


            MakeTestStepHeader(14, UniqueIdentifier++, "Press an acknowledgement on sub-area C1",
                "DMI displays MO17 symbol on sub-area C1");
            /*`
            Test Step 14
            Action: Press an acknowledgement on sub-area C1
            Expected Result: DMI displays MO17 symbol on sub-area C1
            */
            DmiActions.ShowInstruction(this, @"Acknowledge by pressing in sub-area C1");
            DmiExpectedResults.OS_Mode_Ack_pressed_and_released(this);
            DmiActions.Send_OS_Mode(this);

            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 12;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 264;
            EVC8_MMIDriverMessage.Send();

            DmiExpectedResults.Driver_symbol_displayed(this, "Acknowledgement for Unfitted", "MO17", "C1", true);


            MakeTestStepHeader(15, UniqueIdentifier++, "Press an acknowledgement on sub-area C1",
                "Verify the following information,");
            /*
            Test Step 15
            Action: Press an acknowledgement on sub-area C1
            Expected Result: Verify the following information,
            (1)    DMI displays the symbol LE07 in sub-area C1. (The oldest entry of the highest priority in the list)
            Test Step Comment: (1) MMI_gen 4486 (partly: level acknowledgement); MMI_gen 4482 (partly: moveable focus);
            */
            DmiActions.ShowInstruction(this, @"Acknowledge by pressing in sub-area C1");
            DmiExpectedResults.UN_Mode_Ack_pressed_and_released(this);
            DmiActions.Send_UN_Mode(this);

            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 12;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 257;
            EVC8_MMIDriverMessage.PlainTextMessage = "0"; // MMI_N_TEXT (length) 1
            EVC8_MMIDriverMessage.Send();

            DmiExpectedResults.L0_Announcement_Ack_Requested(this);


            MakeTestStepHeader(16, UniqueIdentifier++, "Press an acknowledgement on sub-area C1",
                "Verify the following information,");
            /*
            Test Step 16
            Action: Press an acknowledgement on sub-area C1
            Expected Result: Verify the following information,
            (1)    The symbols is removed refer to pressed area. 
                   DMI displays the text message ‘Runaway movement’ (The oldest entry of the highest priority in the list).
            (2)   The text message ‘Runaway movement’ is displayed instead of ‘Emergency Stop’ from step 1
            Test Step Comment: (1) MMI_gen 4486 (partly: other acknowledgement);(2) MMI_gen 4482 (partly: overflow);
            */
            DmiActions.ShowInstruction(this, @"Acknowledge by pressing in sub-area C1");
            DmiExpectedResults.L0_Announcement_Ack_pressed_and_released(this);

            EVC8_MMIDriverMessage.MMI_Q_TEXT = 269;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.Send();

            DmiExpectedResults.Driver_symbol_deleted(this, "Acknowledgement for Level 0 announcement", "LE07", "C1");
            DmiExpectedResults.Driver_symbol_deleted(this, "Emergency Stop", "text message", "E5");
            DmiExpectedResults.Driver_symbol_displayed(this, "Runaway movement", "text message", "E5", true);


            MakeTestStepHeader(17, UniqueIdentifier++, "Press an acknowledgement on sub-area E5",
                "Verify the following information,");
            /*
            Test Step 17
            Action: Press an acknowledgement on sub-area E5
            Expected Result: Verify the following information,
            (1)    The text message ‘Runaway movement’ is removed refer to pressed area. 
                   DMI displays the text message ‘Communication error’ (The oldest entry of the highest priority in the list)
            Test Step Comment: (1) MMI_gen 4486 (partly: the oldest entry);
            */
            DmiActions.ShowInstruction(this, @"Acknowledge by pressing in sub-area E5");

            EVC8_MMIDriverMessage.MMI_Q_TEXT = 268;
            EVC8_MMIDriverMessage.Send();

            DmiExpectedResults.Driver_symbol_deleted(this, "Runaway movement", "text message", "E5");
            DmiExpectedResults.Driver_symbol_displayed(this, "Communication error", "text message", "E5", true);

            MakeTestStepHeader(18, UniqueIdentifier++, "Press an acknowledgement on sub-area E5",
                "Verify the following information,");
            /*
            Test Step 18
            Action: Press an acknowledgement on sub-area E5
            Expected Result: Verify the following information,
            (1)    The text message ‘Communication error’ is removed refer to pressed area. 
                   DMI displays the text message ‘Balise read error’ (The oldest entry of the highest priority in the list)
            Test Step Comment: (1) MMI_gen 4486 (partly: the oldest entry);
            */
            DmiActions.ShowInstruction(this, @"Acknowledge by pressing in sub-area E5");

            EVC8_MMIDriverMessage.MMI_Q_TEXT = 267;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.Send();

            DmiExpectedResults.Driver_symbol_deleted(this, "Communication error", "text message", "E5");
            DmiExpectedResults.Driver_symbol_displayed(this, "Balise read error", "text message", "E5", true);


            MakeTestStepHeader(19, UniqueIdentifier++, "Press an acknowledgement on sub-area E5",
                "Verify the following information,");
            /*
            Test Step 19
            Action: Press an acknowledgement on sub-area E5
            Expected Result: Verify the following information,
            (1)    The text message ‘Balise read error’ is removed refer to pressed area. 
                   DMI displays the text message ‘Reactivate the Cabin!’ (The oldest entry of the highest priority in the list)
            Test Step Comment: (1) MMI_gen 4486 (partly: the oldest entry); MMI_gen 4482 (partly: 10 pending acknowledgements);
            */
            DmiActions.ShowInstruction(this, @"Acknowledge by pressing in sub-area E5");

            EVC8_MMIDriverMessage.MMI_Q_TEXT = 554;
            EVC8_MMIDriverMessage.Send();

            DmiExpectedResults.Driver_symbol_deleted(this, "Balise read error", "text message", "E5");
            DmiExpectedResults.Driver_symbol_displayed(this, "Reactivate the Cabin!", "text message", "E5", true);


            MakeTestStepHeader(20, UniqueIdentifier++, "Use the test script file 6_3_a.xml",
                "See the expected result No.1-6");
            /*
            Test Step 20
            Action: Use the test script file 6_3_a.xml
            Expected Result: See the expected result No.1-6
            */
            // Should steps 2-6 be repeated also??
            XML_6_3(msgType.typea);

            DmiExpectedResults.Driver_symbol_displayed(this, "Emergency Stop", "text message", "E5", true);

            // ... ??
            // /// ??

            MakeTestStepHeader(21, UniqueIdentifier++, "Simulate loss-communication between ETCS and DMI",
                "DMI displays Default window with the  message “ATP Down Alarm” and sound alarm");
            /*
            Test Step 21
            Action: Simulate loss-communication between ETCS and DMI
            Expected Result: DMI displays Default window with the  message “ATP Down Alarm” and sound alarm
            Test Step Comment: MMI_gen 6923 (partly: MMI_gen 244);
            */
            DmiActions.Simulate_communication_loss_EVC_DMI(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Default window with the message ‘ATP Down Alarm’ in sub-area E5." +
                                Environment.NewLine +
                                "2. The ‘Alarm’ sound is played");

            MakeTestStepHeader(22, UniqueIdentifier++, "Press an acknowledgement on sub-area E5",
                "All entire acknowledgement lists is flushed, DMI displays text message ‘ATP Down Alarm’ without yellow flashing frame");
            /*
            Test Step 22
            Action: Press an acknowledgement on sub-area E5
            Expected Result: All entire acknowledgement lists is flushed, DMI displays text message ‘ATP Down Alarm’ without yellow flashing frame
            Test Step Comment: MMI_gen 6923 (partly: flush the entire acknowledgement list);
            */
            DmiActions.ShowInstruction(this, @"Acknowledge by pressing in sub-area E5");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. All acknowledgement messages are removed from the list." + Environment.NewLine +
                                "2. DMI displays the Default window with the message ‘ATP Down Alarm’ without a yellow flashing frame in sub-area E5.");

            MakeTestStepHeader(23, UniqueIdentifier++, "End of test", "");

            /*
            Test Step 23
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }

        #region Send_XML_6_3_DMI_Test_Specification

        enum msgType
        {
            typea,
            typeb,
            typec,
            typed
        }

        private void XML_6_3(msgType type)
        {
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.PlainTextMessage = "";
            switch (type)
            {
                case msgType.typea:
                    EVC8_MMIDriverMessage.MMI_Q_TEXT = 280;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
                    EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
                    break;
                case msgType.typeb:
                    EVC8_MMIDriverMessage.MMI_Q_TEXT = 264;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
                    EVC8_MMIDriverMessage.MMI_I_TEXT = 7;
                    break;
                case msgType.typec:
                    EVC8_MMIDriverMessage.MMI_Q_TEXT = 554;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
                    EVC8_MMIDriverMessage.MMI_I_TEXT = 11;
                    break;
                case msgType.typed:
                    EVC8_MMIDriverMessage.MMI_Q_TEXT = 0;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 4;
                    EVC8_MMIDriverMessage.MMI_I_TEXT = 6;
                    break;
            }

            EVC8_MMIDriverMessage.Send();
        }

        #endregion
    }
}
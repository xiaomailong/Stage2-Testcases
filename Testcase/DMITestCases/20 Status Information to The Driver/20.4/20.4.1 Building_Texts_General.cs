using System;
using Testcase.Telegrams.EVCtoDMI;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 20.4.1 Building Texts: General
    /// TC-ID: 15.4
    /// 
    /// This test case verifies the display of the building texts and driver messages with a placeholder symbol (#1,#2 and unknown) with specified characters which are complied with [GenVSIS].
    /// 
    /// Tested Requirements:
    /// MMI_gen 2557 (partly:first part, EVC-8.criteria = 5, incomplete sentence, 9 seconds, the same index, non-ack, ack, different index); MMI_gen 7045; MMI_gen 7046 (partly: concatenate, second packet, non-ack, ack, overwritten); MMI_gen 7025 (partly: packet EVC-8, 1st bullet, 2nd bullet, #1, #2, unknown); MMI_gen 1066; MMI_gen 1046; MMI_gen 11470 (partly: Bit #23); MMI_gen 9520 (partly: system status message in table 76 for NTC);
    /// 
    /// Scenario:
    /// Drive the train forward pass BG1 at position 100m. Then verify the text message which display in sub-area E5-E9 and compare with received packet information EVC-8.BG1: Packet 72 (Plain Text message)Stop the train and use the test script files to send packet information EVC-
    /// 8.Then, verify the display information.NoteSome step of test script files are executed continuously, Tester need to confirm expected results within specific time.
    /// 
    /// Used files:
    /// 15_4.tdg, 15_4_a.xml, 15_4_b.xml, 15_4_c.xml, 15_4_d.xml, 15_4_e.xml, 15_4_f.xml, 15_4_g.xml, 15_4_h.xml, 15_4_i.xml, 15_4_j.xml
    /// </summary>
    public class TC_15_4_Building_Texts : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:

            // Call the TestCaseBase PreExecution
            base.PreExecution();
            // System is power on.Cabin is activateSoM is perform in SR mode, Level 1.The active language is English.
            DmiActions.Complete_SoM_L1_SR(this);
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in SR mode, Level 1.

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
            TraceInfo("Drive the train forward pass BG1.Then, stop the train");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following information,Use the log file to confirm that DMI received packet EVC-8 with MMI_Q_TEXT = 256 (Plain Text Message).The plain text message ‘TEST 15.4’ is displayed on sub-area E5.Text messsage is display in white colour.The local time of appearance is attached in front of plain text messge at the first line.The local time is separated from the first character of displayed text by an indent.The format of local time is “hh:mm” with a 24 hours time reference");
            /*
            Test Step 1
            Action: Drive the train forward pass BG1.Then, stop the train
            Expected Result: Verify the following information,Use the log file to confirm that DMI received packet EVC-8 with MMI_Q_TEXT = 256 (Plain Text Message).The plain text message ‘TEST 15.4’ is displayed on sub-area E5.Text messsage is display in white colour.The local time of appearance is attached in front of plain text messge at the first line.The local time is separated from the first character of displayed text by an indent.The format of local time is “hh:mm” with a 24 hours time reference
            Test Step Comment: (1) MMI_gen 7025 (partly: packet EVC-8, 1st bullet);(2) MMI_gen 7025 (partly: 2nd bullet, #1);(3) MMI_gen 1046 (partly: bullet a, non-acknowledgeable Text message); (4) MMI_gen 1046 (b) (partly: attached to the first line);   (5) MMI_gen 1046 (b) (partly: separated from the first character by an indent);           (6) MMI_gen 1046 (b) (partly: format of local time);
            */
            // Don't understand this part of the test!
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 5;

            // ETCS must be supplying the time: DMI does not prepend it to the message

            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 256;
            EVC8_MMIDriverMessage.PlainTextMessage =
                string.Format("{0:HH:mm} TEST 15.4", DateTime.Now.ToLocalTime());
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.Send();
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 0;

            // spec says 'indent' is separator ??
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The plain text message ‘TEST 15.4’ is displayed in white on sub-area E5." +
                                Environment.NewLine +
                                "2. The local time of arrival of the message is displayed on the first line in front of the plain text message" +
                                Environment.NewLine +
                                "3. The local time is separated from the text message by a space" +
                                Environment.NewLine +
                                "4. The local time format is ‘hh:mm’ with hh in range 0..23");

            TraceHeader("Test Step 2");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Acknowledge the plain text message");
            TraceReport("Expected Result");
            TraceInfo(
                "Use the log file to confirm that DMI sends out packet [MMI_DRIVER_ACTION (EVC-152)] with the value of variable MMI_M_DRIVER_ACTION refer to sequence below,a)   MMI_M_DRIVER_ACTION = 23 (Ack of Plain Text message)");
            /*
            Test Step 2
            Action: Acknowledge the plain text message
            Expected Result: Use the log file to confirm that DMI sends out packet [MMI_DRIVER_ACTION (EVC-152)] with the value of variable MMI_M_DRIVER_ACTION refer to sequence below,a)   MMI_M_DRIVER_ACTION = 23 (Ack of Plain Text message)
            Test Step Comment: MMI_gen 11470 (partly: Bit #23);
            */
            DmiActions.ShowInstruction(this, "Acknowledge the plain test message");

            Telegrams.DMItoEVC.EVC152_MMIDriverAction.Check_MMI_M_DRIVER_ACTION = Telegrams.DMItoEVC
                .EVC152_MMIDriverAction.MMI_M_DRIVER_ACTION.PlainTextInformationAck;

            TraceHeader("Test Step 3");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Use the test script file 15_4_a.xml to send 2 packets information EVC-8 with,MMI_Q_TEXT_CRITERIA = 5MMI_I_TEXT = 1MMI_Q_TEXT = 256MMI_N_TEXT = 4MMI_X_TEXT[0] = 84MMI_X_TEXT[1] = 69MMI_X_TEXT[2] = 83MMI_X_TEXT[3] = 84Send the second packet after time pass 10 second.MMI_Q_TEXT_CRITERIA = 3MMI_I_TEXT = 1MMI_Q_TEXT = 256MMI_N_TEXT = 4MMI_X_TEXT[0] = 32MMI_X_TEXT[1] = 68MMI_X_TEXT[2] = 77MMI_X_TEXT[3] = 73Note: Stopwatch is required");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following information,Only “ DMI” from the second packet is displayed in area E5 due to the expiry of 10 seconds");
            /*
            Test Step 3
            Action: Use the test script file 15_4_a.xml to send 2 packets information EVC-8 with,MMI_Q_TEXT_CRITERIA = 5MMI_I_TEXT = 1MMI_Q_TEXT = 256MMI_N_TEXT = 4MMI_X_TEXT[0] = 84MMI_X_TEXT[1] = 69MMI_X_TEXT[2] = 83MMI_X_TEXT[3] = 84Send the second packet after time pass 10 second.MMI_Q_TEXT_CRITERIA = 3MMI_I_TEXT = 1MMI_Q_TEXT = 256MMI_N_TEXT = 4MMI_X_TEXT[0] = 32MMI_X_TEXT[1] = 68MMI_X_TEXT[2] = 77MMI_X_TEXT[3] = 73Note: Stopwatch is required
            Expected Result: Verify the following information,Only “ DMI” from the second packet is displayed in area E5 due to the expiry of 10 seconds
            Test Step Comment: (1) MMI_gen 7045; 
            */
            XML_15_4(msgType.typea);
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The plain text message ‘TEST’ does not appear." + Environment.NewLine +
                                @"2. ‘ DMI’ is displayed in sub-area E5.");

            TraceHeader("Test Step 4");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Use the test script file 15_4_b.xml to send 2 packets information EVC-8 with,MMI_Q_TEXT_CRITERIA = 5MMI_I_TEXT = 1MMI_Q_TEXT = 256MMI_N_TEXT = 4MMI_X_TEXT[0] = 84MMI_X_TEXT[1] = 69MMI_X_TEXT[2] = 83MMI_X_TEXT[3] = 84Send the second packet after time pass 9 seconds.MMI_Q_TEXT_CRITERIA = 3MMI_I_TEXT = 1MMI_Q_TEXT = 256MMI_N_TEXT = 4MMI_X_TEXT[0] = 32MMI_X_TEXT[1] = 68MMI_X_TEXT[2] = 77MMI_X_TEXT[3] = 73");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the display in sub-area E5-E9 as follows,Once the first message is sent, DMI does not show the message as the message is marked as an incomplete sentence.DMI displays “TEST DMI” in area E5 after the second message is sent");
            /*
            Test Step 4
            Action: Use the test script file 15_4_b.xml to send 2 packets information EVC-8 with,MMI_Q_TEXT_CRITERIA = 5MMI_I_TEXT = 1MMI_Q_TEXT = 256MMI_N_TEXT = 4MMI_X_TEXT[0] = 84MMI_X_TEXT[1] = 69MMI_X_TEXT[2] = 83MMI_X_TEXT[3] = 84Send the second packet after time pass 9 seconds.MMI_Q_TEXT_CRITERIA = 3MMI_I_TEXT = 1MMI_Q_TEXT = 256MMI_N_TEXT = 4MMI_X_TEXT[0] = 32MMI_X_TEXT[1] = 68MMI_X_TEXT[2] = 77MMI_X_TEXT[3] = 73
            Expected Result: Verify the display in sub-area E5-E9 as follows,Once the first message is sent, DMI does not show the message as the message is marked as an incomplete sentence.DMI displays “TEST DMI” in area E5 after the second message is sent
            Test Step Comment: (1) MMI_gen 2557 (partly: first part, EVC-8.criteria = 5, incomplete sentence)(2) MMI_gen 2557 (partly: 9 seconds, second part, the same index, non-ack);           MMI_gen 7046 (partly: concatenate, second packet, non-ack);      MMI_gen 7025 (partly: 2nd bullet, #1);
            */
            XML_15_4(msgType.typeb);
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The plain text message ‘TEST’ does not appear." + Environment.NewLine +
                                @"2. ‘TEST DMI’ is displayed in sub-area E5.");

            TraceHeader("Test Step 5");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Use the test script file 15_4_i.xml to send 2 packets information EVC-8 with,MMI_Q_TEXT_CRITERIA = 5MMI_I_TEXT = 1MMI_Q_TEXT = 256MMI_N_TEXT = 4MMI_X_TEXT[0] = 84MMI_X_TEXT[1] = 69MMI_X_TEXT[2] = 83MMI_X_TEXT[3] = 84Send the second packet after time pass 11 seconds.MMI_Q_TEXT_CRITERIA = 3MMI_I_TEXT = 1MMI_Q_TEXT = 256MMI_N_TEXT = 4MMI_X_TEXT[0] = 32MMI_X_TEXT[1] = 68MMI_X_TEXT[2] = 77MMI_X_TEXT[3] = 73Note: A stopwatch is required");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following information,(1) Only “ DMI” from the second packet is displayed in area E5 due to the expiry of 11 seconds");
            /*
            Test Step 5
            Action: Use the test script file 15_4_i.xml to send 2 packets information EVC-8 with,MMI_Q_TEXT_CRITERIA = 5MMI_I_TEXT = 1MMI_Q_TEXT = 256MMI_N_TEXT = 4MMI_X_TEXT[0] = 84MMI_X_TEXT[1] = 69MMI_X_TEXT[2] = 83MMI_X_TEXT[3] = 84Send the second packet after time pass 11 seconds.MMI_Q_TEXT_CRITERIA = 3MMI_I_TEXT = 1MMI_Q_TEXT = 256MMI_N_TEXT = 4MMI_X_TEXT[0] = 32MMI_X_TEXT[1] = 68MMI_X_TEXT[2] = 77MMI_X_TEXT[3] = 73Note: A stopwatch is required
            Expected Result: Verify the following information,(1) Only “ DMI” from the second packet is displayed in area E5 due to the expiry of 11 seconds
            Test Step Comment: (1) MMI_gen 2557 (partly: 11 seconds, second part, the same index, non-ack); MMI_gen 7045; 
            */
            XML_15_4(msgType.typei);
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. ‘ DMI’ is displayed in sub-area E5.");

            TraceHeader("Test Step 6");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Use the test script file 15_4_j.xml to send 3 packets information EVC-8 with,MMI_Q_TEXT_CRITERIA = 5MMI_I_TEXT = 1MMI_Q_TEXT = 256MMI_N_TEXT = 4MMI_X_TEXT[0] = 84MMI_X_TEXT[1] = 69MMI_X_TEXT[2] = 83MMI_X_TEXT[3] = 84Send the second packet after time pass 9 seconds.MMI_Q_TEXT_CRITERIA = 5MMI_I_TEXT = 1MMI_Q_TEXT = 256MMI_N_TEXT = 4MMI_X_TEXT[0] = 32MMI_X_TEXT[1] = 68MMI_X_TEXT[2] = 77MMI_X_TEXT[3] = 73Send the third packet after time pass 9 seconds.MMI_Q_TEXT_CRITERIA = 3MMI_I_TEXT = 1MMI_Q_TEXT = 256MMI_N_TEXT = 4MMI_X_TEXT[0] = 84MMI_X_TEXT[1] = 69MMI_X_TEXT[2] = 83MMI_X_TEXT[3] = 84A stopwatch is required");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following information,Once the first message is sent, DMI does not show the message as the message is marked as an incomplete sentence.Once the second message is sent, DMI does not show the message as the message is marked as an incomplete sentence.DMI displays “ DMITEST” in area E5 after the third message is sent.The first message is overwritten by the second message");
            /*
            Test Step 6
            Action: Use the test script file 15_4_j.xml to send 3 packets information EVC-8 with,MMI_Q_TEXT_CRITERIA = 5MMI_I_TEXT = 1MMI_Q_TEXT = 256MMI_N_TEXT = 4MMI_X_TEXT[0] = 84MMI_X_TEXT[1] = 69MMI_X_TEXT[2] = 83MMI_X_TEXT[3] = 84Send the second packet after time pass 9 seconds.MMI_Q_TEXT_CRITERIA = 5MMI_I_TEXT = 1MMI_Q_TEXT = 256MMI_N_TEXT = 4MMI_X_TEXT[0] = 32MMI_X_TEXT[1] = 68MMI_X_TEXT[2] = 77MMI_X_TEXT[3] = 73Send the third packet after time pass 9 seconds.MMI_Q_TEXT_CRITERIA = 3MMI_I_TEXT = 1MMI_Q_TEXT = 256MMI_N_TEXT = 4MMI_X_TEXT[0] = 84MMI_X_TEXT[1] = 69MMI_X_TEXT[2] = 83MMI_X_TEXT[3] = 84A stopwatch is required
            Expected Result: Verify the following information,Once the first message is sent, DMI does not show the message as the message is marked as an incomplete sentence.Once the second message is sent, DMI does not show the message as the message is marked as an incomplete sentence.DMI displays “ DMITEST” in area E5 after the third message is sent.The first message is overwritten by the second message
            Test Step Comment: (1) MMI_gen 2557 (partly: first part, EVC-8.criteria = 5, incomplete sentence);(2) MMI_gen 2557 (partly: EVC-8.criteria = 5, incomplete sentence);(3) MMI_gen 2557 (partly: 9 seconds, the same index, non-ack);           MMI_gen 7046 (partly: concatenate, non-ack);      MMI_gen 7025 (partly: 2nd bullet, #1);(4) MMI_gen 7046 (partly: overwritten);
            */
            XML_15_4(msgType.typej);
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The plain text message ‘TEST’ does not appear." + Environment.NewLine +
                                @"2. The plain text message ‘ DMI’ does not appear." + Environment.NewLine +
                                @"2. ‘ DMITEST’ is displayed in sub-area E5.");

            TraceHeader("Test Step 7");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Use the test script file 15_4_k.xml to send 3 packets information EVC-8 with,MMI_Q_TEXT_CRITERIA = 5MMI_I_TEXT = 1MMI_Q_TEXT = 256MMI_N_TEXT = 4MMI_X_TEXT[0] = 84MMI_X_TEXT[1] = 69MMI_X_TEXT[2] = 83MMI_X_TEXT[3] = 84Send the second packet after time pass 4 seconds.MMI_Q_TEXT_CRITERIA = 0MMI_I_TEXT = 2MMI_Q_TEXT = 256MMI_N_TEXT = 4MMI_X_TEXT[0] = 32MMI_X_TEXT[1] = 68MMI_X_TEXT[2] = 77MMI_X_TEXT[3] = 73Send the second packet after time pass 4 seconds.MMI_Q_TEXT_CRITERIA = 0MMI_I_TEXT = 1MMI_Q_TEXT = 256MMI_N_TEXT = 4MMI_X_TEXT[0] = 32MMI_X_TEXT[1] = 68MMI_X_TEXT[2] = 77MMI_X_TEXT[3] = 73Note: Stopwatch is required");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following information,“ DMI” from the second packet is displayed in area E5 with yellow flashing frame due to the different ID from the first packet.DMI displays “TEST DMI” in area E5 with yellow flashing frame after the third message is sent due to the same message ID as the first incomplete message");
            /*
            Test Step 7
            Action: Use the test script file 15_4_k.xml to send 3 packets information EVC-8 with,MMI_Q_TEXT_CRITERIA = 5MMI_I_TEXT = 1MMI_Q_TEXT = 256MMI_N_TEXT = 4MMI_X_TEXT[0] = 84MMI_X_TEXT[1] = 69MMI_X_TEXT[2] = 83MMI_X_TEXT[3] = 84Send the second packet after time pass 4 seconds.MMI_Q_TEXT_CRITERIA = 0MMI_I_TEXT = 2MMI_Q_TEXT = 256MMI_N_TEXT = 4MMI_X_TEXT[0] = 32MMI_X_TEXT[1] = 68MMI_X_TEXT[2] = 77MMI_X_TEXT[3] = 73Send the second packet after time pass 4 seconds.MMI_Q_TEXT_CRITERIA = 0MMI_I_TEXT = 1MMI_Q_TEXT = 256MMI_N_TEXT = 4MMI_X_TEXT[0] = 32MMI_X_TEXT[1] = 68MMI_X_TEXT[2] = 77MMI_X_TEXT[3] = 73Note: Stopwatch is required
            Expected Result: Verify the following information,“ DMI” from the second packet is displayed in area E5 with yellow flashing frame due to the different ID from the first packet.DMI displays “TEST DMI” in area E5 with yellow flashing frame after the third message is sent due to the same message ID as the first incomplete message
            Test Step Comment: (1) MMI_gen 2557 (partly: second part, different index);(2) MMI_gen 2557 (partly: 9 seconds, the same index, ack);           MMI_gen 7046 (partly: concatenate, ack);      MMI_gen 7025 (partly: 2nd bullet, #1);
            */
            XML_15_4(msgType.typek);

            TraceHeader("Test Step 8");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Perform the following procedure, De-activate Cabin.Activate Cabin.Then, Use the test script file 15_4_c.xml to send 2 packets information EVC-8 with,MMI_Q_TEXT_CRITERIA = 5MMI_I_TEXT = 1MMI_Q_TEXT = 256MMI_N_TEXT = 20MMI_X_TEXT[0] = 65MMI_X_TEXT[1] = 66MMI_X_TEXT[2] = 67MMI_X_TEXT[3] = 68MMI_X_TEXT[4] = 69MMI_X_TEXT[5] = 70MMI_X_TEXT[6] = 71MMI_X_TEXT[7] = 72MMI_X_TEXT[8] = 73MMI_X_TEXT[9] = 74MMI_X_TEXT[10] = 32MMI_X_TEXT[11] = 66MMI_X_TEXT[12] = 67MMI_X_TEXT[13] = 68MMI_X_TEXT[14] = 69MMI_X_TEXT[15] = 70MMI_X_TEXT[16] = 71MMI_X_TEXT[17] = 72MMI_X_TEXT[18] = 73MMI_X_TEXT[19] = 74Send the second packet after time pass 9 second.MMI_Q_TEXT_CRITERIA = 3MMI_I_TEXT = 1MMI_Q_TEXT = 256MMI_N_TEXT = 10MMI_X_TEXT[0] = 32MMI_X_TEXT[1] = 88MMI_X_TEXT[2] = 88MMI_X_TEXT[3] = 88MMI_X_TEXT[4] = 88MMI_X_TEXT[5] = 88MMI_X_TEXT[6] = 88MMI_X_TEXT[7] = 88MMI_X_TEXT[8] = 88MMI_X_TEXT[9] = 88Note: Stopwatch is required");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the display in sub-area E5-E9 as follows,The 2 plain text messages are concatenated and displayed as “ABCDEFGHIJ BCDEFGHIJ XXXXXXXXX.Text messsage is displayed in white colour.The text message is displayed as two lines in both of sub area E5 and E6.The message in sub area E6 is aligned with the with character of display text in sub area E5");
            /*
            Test Step 8
            Action: Perform the following procedure, De-activate Cabin.Activate Cabin.Then, Use the test script file 15_4_c.xml to send 2 packets information EVC-8 with,MMI_Q_TEXT_CRITERIA = 5MMI_I_TEXT = 1MMI_Q_TEXT = 256MMI_N_TEXT = 20MMI_X_TEXT[0] = 65MMI_X_TEXT[1] = 66MMI_X_TEXT[2] = 67MMI_X_TEXT[3] = 68MMI_X_TEXT[4] = 69MMI_X_TEXT[5] = 70MMI_X_TEXT[6] = 71MMI_X_TEXT[7] = 72MMI_X_TEXT[8] = 73MMI_X_TEXT[9] = 74MMI_X_TEXT[10] = 32MMI_X_TEXT[11] = 66MMI_X_TEXT[12] = 67MMI_X_TEXT[13] = 68MMI_X_TEXT[14] = 69MMI_X_TEXT[15] = 70MMI_X_TEXT[16] = 71MMI_X_TEXT[17] = 72MMI_X_TEXT[18] = 73MMI_X_TEXT[19] = 74Send the second packet after time pass 9 second.MMI_Q_TEXT_CRITERIA = 3MMI_I_TEXT = 1MMI_Q_TEXT = 256MMI_N_TEXT = 10MMI_X_TEXT[0] = 32MMI_X_TEXT[1] = 88MMI_X_TEXT[2] = 88MMI_X_TEXT[3] = 88MMI_X_TEXT[4] = 88MMI_X_TEXT[5] = 88MMI_X_TEXT[6] = 88MMI_X_TEXT[7] = 88MMI_X_TEXT[8] = 88MMI_X_TEXT[9] = 88Note: Stopwatch is required
            Expected Result: Verify the display in sub-area E5-E9 as follows,The 2 plain text messages are concatenated and displayed as “ABCDEFGHIJ BCDEFGHIJ XXXXXXXXX.Text messsage is displayed in white colour.The text message is displayed as two lines in both of sub area E5 and E6.The message in sub area E6 is aligned with the with character of display text in sub area E5
            Test Step Comment: (1) MMI_gen 2557 (partly: stored first part, same-index second part, 1st bullet);           MMI_gen 7046 (partly: concatenate, 1st bullet);      (2) MMI_gen 1046 (a) (partly: building text);  (3) MMI_gen 1046 (c) (partly: building text, continue inside the next Sub-area);(4) MMI_gen 1046 (c)  (partly: building text, Next line aligned with the first character of the display text);
            */
            EVC2_MMIStatus.MMI_M_ACTIVE_CABIN = Variables.MMI_M_ACTIVE_CABIN.NoCabinActive;
            EVC2_MMIStatus.Send();

            EVC2_MMIStatus.MMI_M_ACTIVE_CABIN = Variables.MMI_M_ACTIVE_CABIN.Cabin1Active;
            EVC2_MMIStatus.Send();

            DmiActions.Set_Driver_ID(this, "1234");

            XML_15_4(msgType.typec);
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. DMI displays ‘ABCDEFGHIJ BCDEFGHIJ XXXXXXXXX’ in white in sub-areas E5 and E6." +
                                Environment.NewLine +
                                "2. The text message is displayed in two lines in both sub-areas E5 and E6.");

            TraceHeader("Test Step 9");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Use the test script file 15_4_d.xml to send multiple packets EVC-8 with the following value,Common variableMMI_N_TEXT = 1MMI_X_TEXT[0]= 20The order of MMI_Q_TEXT value in each packetMMI_Q_TEXT = 543MMI_Q_TEXT = 565MMI_Q_TEXT = 570MMI_Q_TEXT = 573MMI_Q_TEXT = 700MMI_Q_TEXT = 704");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the display message in sub-area E5-E9 according to the order of MMI_Q_TEXT in the Action column,TPWS FailedConfirm change of inhibit STM TPWSShunting rejected due to TPWS TripTPWS needs dataTPWS brake demandTPWS is not availableNote: Use the <Up> and <Down> button to scroll the list of message in sub-area E5-E9");
            /*
            Test Step 9
            Action: Use the test script file 15_4_d.xml to send multiple packets EVC-8 with the following value,Common variableMMI_N_TEXT = 1MMI_X_TEXT[0]= 20The order of MMI_Q_TEXT value in each packetMMI_Q_TEXT = 543MMI_Q_TEXT = 565MMI_Q_TEXT = 570MMI_Q_TEXT = 573MMI_Q_TEXT = 700MMI_Q_TEXT = 704
            Expected Result: Verify the display message in sub-area E5-E9 according to the order of MMI_Q_TEXT in the Action column,TPWS FailedConfirm change of inhibit STM TPWSShunting rejected due to TPWS TripTPWS needs dataTPWS brake demandTPWS is not availableNote: Use the <Up> and <Down> button to scroll the list of message in sub-area E5-E9
            Test Step Comment: MMI_gen 7025 (partly:1st bullet, 2nd bullet, #2); MMI_gen 9520 (partly: system status message in table 76 for NTC);
            */
            XML_15_4(msgType.typed);

            TraceHeader("Test Step 10");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Use the test script file 15_4_e.xml to send multiple packets EVC-8 with the follow ing value,Common variableMMI_N_TEXT = 1MMI_X_TEXT[0]= 99The order of MMI_Q_TEXT value in each packetMMI_Q_TEXT = 543MMI_Q_TEXT = 565MMI_Q_TEXT = 570MMI_Q_TEXT = 573MMI_Q_TEXT = 700MMI_Q_TEXT = 704");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the display message in sub-area E5-E9 according to the order of MMI_Q_TEXT in the Action column,<Unknown>FailedConfirm change of inhibit STM <Unknown>Shunting rejected due to <Unknown>Trip<Unknown> needs data<Unknown> brake demand<Unknown> is not availableNote: Use the <Up> and <Down> button to scroll the list of message in sub-area E5-E9");
            /*
            Test Step 10
            Action: Use the test script file 15_4_e.xml to send multiple packets EVC-8 with the follow ing value,Common variableMMI_N_TEXT = 1MMI_X_TEXT[0]= 99The order of MMI_Q_TEXT value in each packetMMI_Q_TEXT = 543MMI_Q_TEXT = 565MMI_Q_TEXT = 570MMI_Q_TEXT = 573MMI_Q_TEXT = 700MMI_Q_TEXT = 704
            Expected Result: Verify the display message in sub-area E5-E9 according to the order of MMI_Q_TEXT in the Action column,<Unknown>FailedConfirm change of inhibit STM <Unknown>Shunting rejected due to <Unknown>Trip<Unknown> needs data<Unknown> brake demand<Unknown> is not availableNote: Use the <Up> and <Down> button to scroll the list of message in sub-area E5-E9
            Test Step Comment: MMI_gen 7025 (partly:1st bullet, 2nd bullet, #2, unknown); MMI_gen 9520 (partly: system status message in table 76 for NTC);
            */
            XML_15_4(msgType.typee);

            TraceHeader("Test Step 11");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Use the test script file 15_4_f.xml to send multiple packets EVC-8 with the following value,Common variableMMI_N_TEXT = 1MMI_X_TEXT[0]= 49The order of MMI_Q_TEXT value in each packetMMI_Q_TEXT = 522MMI_Q_TEXT = 525MMI_Q_TEXT = 534MMI_Q_TEXT = 535MMI_Q_TEXT = 564MMI_Q_TEXT = 574MMI_Q_TEXT = 705");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the display message in sub-area E5-E9 according to the order of MMI_Q_TEXT in the Action column,Restriction 1 km/h in Release Speed Area Brake Test timeout in 1 HoursBTM Test Timeout in 1 hoursATP Restart required in 1 HoursConfirm change of inhibit Level 1Cabin Reactivation required in 1 hoursNew power-up required in 1 hoursNote: Use the <Up> and <Down> button to scroll the list of message in sub-area E5-E9");
            /*
            Test Step 11
            Action: Use the test script file 15_4_f.xml to send multiple packets EVC-8 with the following value,Common variableMMI_N_TEXT = 1MMI_X_TEXT[0]= 49The order of MMI_Q_TEXT value in each packetMMI_Q_TEXT = 522MMI_Q_TEXT = 525MMI_Q_TEXT = 534MMI_Q_TEXT = 535MMI_Q_TEXT = 564MMI_Q_TEXT = 574MMI_Q_TEXT = 705
            Expected Result: Verify the display message in sub-area E5-E9 according to the order of MMI_Q_TEXT in the Action column,Restriction 1 km/h in Release Speed Area Brake Test timeout in 1 HoursBTM Test Timeout in 1 hoursATP Restart required in 1 HoursConfirm change of inhibit Level 1Cabin Reactivation required in 1 hoursNew power-up required in 1 hoursNote: Use the <Up> and <Down> button to scroll the list of message in sub-area E5-E9
            Test Step Comment: MMI_gen 7025 (partly:1st bullet, 2nd bullet, #1);
            */
            XML_15_4(msgType.typef);

            TraceHeader("Test Step 12");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Use the test script file 15_4_g.xml to send multiple packets EVC-8 with the following value,Common variableMMI_N_TEXT = 1MMI_X_TEXT[0]= 0The order of MMI_Q_TEXT value in each packetMMI_Q_TEXT = 522MMI_Q_TEXT = 525MMI_Q_TEXT = 534MMI_Q_TEXT = 535MMI_Q_TEXT = 564MMI_Q_TEXT = 574MMI_Q_TEXT = 705");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the display message in sub-area E5-E9 according to the order of MMI_Q_TEXT in the Action column,Restriction  km/h in Release Speed Area Brake Test timeout in  HoursBTM Test Timeout in  hoursATP Restart required in  HoursConfirm change of inhibit Level Cabin Reactivation required in  hoursNew power-up required in  hoursNote: Use the <Up> and <Down> button to scroll the list of message in sub-area E5-E9. is a solid rectangular symbol in white colour");
            /*
            Test Step 12
            Action: Use the test script file 15_4_g.xml to send multiple packets EVC-8 with the following value,Common variableMMI_N_TEXT = 1MMI_X_TEXT[0]= 0The order of MMI_Q_TEXT value in each packetMMI_Q_TEXT = 522MMI_Q_TEXT = 525MMI_Q_TEXT = 534MMI_Q_TEXT = 535MMI_Q_TEXT = 564MMI_Q_TEXT = 574MMI_Q_TEXT = 705
            Expected Result: Verify the display message in sub-area E5-E9 according to the order of MMI_Q_TEXT in the Action column,Restriction  km/h in Release Speed Area Brake Test timeout in  HoursBTM Test Timeout in  hoursATP Restart required in  HoursConfirm change of inhibit Level Cabin Reactivation required in  hoursNew power-up required in  hoursNote: Use the <Up> and <Down> button to scroll the list of message in sub-area E5-E9. is a solid rectangular symbol in white colour
            Test Step Comment: MMI_gen 7025 (partly:1st bullet, 2nd bullet, #1, unknown);
            */
            XML_15_4(msgType.typeg);

            TraceHeader("Test Step 13");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Use the test script file 15_4_h.xml to send multiple packets EVC-8 with the following value,Common variableMMI_N_TEXT = 1MMI_X_TEXT[0]= 49The order of MMI_Q_TEXT value in each packetMMI_Q_TEXT = 533MMI_Q_TEXT = 260");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following information,DMI displays driver message ‘BTM Test Timeout’ in sub-area E5. (MMI_X_TEXT is ignored)DMI displays symbol ST02 symbol  in sub-area C9. (MMI_X_TEXT is ignored)");
            /*
            Test Step 13
            Action: Use the test script file 15_4_h.xml to send multiple packets EVC-8 with the following value,Common variableMMI_N_TEXT = 1MMI_X_TEXT[0]= 49The order of MMI_Q_TEXT value in each packetMMI_Q_TEXT = 533MMI_Q_TEXT = 260
            Expected Result: Verify the following information,DMI displays driver message ‘BTM Test Timeout’ in sub-area E5. (MMI_X_TEXT is ignored)DMI displays symbol ST02 symbol  in sub-area C9. (MMI_X_TEXT is ignored)
            Test Step Comment: (1) MMI_gen 1066 (partly: #1, #2);(2) MMI_gen 1066 (partly: #4, ST02);
            */
            XML_15_4(msgType.typeh);

            TraceHeader("Test Step 14");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("End of test");
            TraceReport("Expected Result");
            TraceInfo("");
            /*
            Test Step 14
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }

        #region Send_XML_15_4_DMI_Test_Specification

        enum msgType
        {
            typea,
            typeb,
            typec,
            typed,
            typee,
            typef,
            typeg,
            typeh,
            typei,
            typej,
            typek
        }

        private void XML_15_4(msgType type)
        {
            switch (type)
            {
                case msgType.typea:
                    // Step 3/1
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 5;
                    EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT = 256;
                    EVC8_MMIDriverMessage.PlainTextMessage = "TEST";
                    EVC8_MMIDriverMessage.Send();

                    Wait_Realtime(10000);

                    // Step 3/2            
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
                    EVC8_MMIDriverMessage.PlainTextMessage = " DMI";
                    EVC8_MMIDriverMessage.Send();


                    break;
                case msgType.typeb:
                    // Step 4/1
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 5;
                    EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT = 256;
                    EVC8_MMIDriverMessage.PlainTextMessage = "TEST";
                    EVC8_MMIDriverMessage.Send();

                    Wait_Realtime(9000);

                    // Step 4/2            
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
                    EVC8_MMIDriverMessage.PlainTextMessage = " DMI";
                    EVC8_MMIDriverMessage.Send();


                    break;
                case msgType.typec:
                    // Step 8/1
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 5;
                    EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT = 256;
                    EVC8_MMIDriverMessage.PlainTextMessage = "ABCDEFGHIJ BCDEFGHIJ";
                    EVC8_MMIDriverMessage.Send();

                    Wait_Realtime(9000);

                    // Step 8/2            
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
                    EVC8_MMIDriverMessage.PlainTextMessage = " XXXXXXXXX";
                    EVC8_MMIDriverMessage.Send();


                    break;
                case msgType.typed:
                    // Step 9/1
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 5;
                    EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT = 543;
                    EVC8_MMIDriverMessage.PlainTextMessage = "20";
                    EVC8_MMIDriverMessage.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        @"1. DMI displays ‘TPWS Failed’ in areas E5-E9.");

                    // Step 9/2         
                    // spec says I_TEXT = 1 (fixed) but xml 'increments' in each message
                    // assume xml is correct   
                    EVC8_MMIDriverMessage.MMI_I_TEXT = 2;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT = 565;
                    EVC8_MMIDriverMessage.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        @"1. DMI displays ‘Confirm change of inhibit STM TPWS’ in areas E5-E9.");

                    // Step 9/3
                    EVC8_MMIDriverMessage.MMI_I_TEXT = 3;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT = 570;
                    EVC8_MMIDriverMessage.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        @"1. DMI displays ‘Shunting rejected due to TPWS Trip’ in areas E5-E9.");

                    // Step 9/4
                    EVC8_MMIDriverMessage.MMI_I_TEXT = 4;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT = 573;
                    EVC8_MMIDriverMessage.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        @"1. DMI displays ‘TPWS needs data’ in areas E5-E9.");

                    // Step 9/5
                    EVC8_MMIDriverMessage.MMI_I_TEXT = 5;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT = 700;
                    EVC8_MMIDriverMessage.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        @"1. DMI displays ‘TPWS brake demand’ in areas E5-E9.");

                    // Step 9/6
                    EVC8_MMIDriverMessage.MMI_I_TEXT = 6;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT = 704;
                    EVC8_MMIDriverMessage.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        @"1. DMI displays ‘TPWS is not available’ in areas E5-E9.");

                    WaitForVerification(
                        @"Using the <Up> and <Down> buttons, check that the order of all messages top to bottom is:" +
                        Environment.NewLine + Environment.NewLine +
                        @"1. DMI displays ‘TPWS is not available’ in areas E5-E9." + Environment.NewLine +
                        @"2. DMI displays ‘TPWS brake demand’ in areas E5-E9." + Environment.NewLine +
                        @"3. DMI displays ‘TPWS needs data’ in areas E5-E9." + Environment.NewLine +
                        @"4. DMI displays ‘Shunting rejected due to TPWS Trip’ in areas E5-E9." + Environment.NewLine +
                        @"5. DMI displays ‘Confirm change of inhibit STM TPWS’ in areas E5-E9." + Environment.NewLine +
                        @"6. DMI displays ‘TPWS Failed’ in areas E5 - E9.");


                    break;
                case msgType.typee:

                    // Step 10/1
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 5;
                    EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT = 543;
                    EVC8_MMIDriverMessage.PlainTextMessage = "99";
                    EVC8_MMIDriverMessage.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        @"1. DMI displays ‘<Unknown> Failed’ in areas E5-E9.");

                    // Step 10/2         
                    // spec says I_TEXT = 1 (fixed) but xml 'increments' in each message
                    // assume xml is correct   
                    EVC8_MMIDriverMessage.MMI_I_TEXT = 2;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT = 565;
                    EVC8_MMIDriverMessage.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        @"1. DMI displays ‘Confirm change of inhibit STM <Unknown>’ in areas E5-E9.");

                    // Step 10/3
                    EVC8_MMIDriverMessage.MMI_I_TEXT = 3;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT = 570;
                    EVC8_MMIDriverMessage.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        @"1. DMI displays ‘Shunting rejected due to <Unknown> Trip’ in areas E5-E9.");

                    // Step 10/4
                    EVC8_MMIDriverMessage.MMI_I_TEXT = 4;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT = 573;
                    EVC8_MMIDriverMessage.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        @"1. DMI displays ‘<Unknown> needs data’ in areas E5-E9.");

                    // Step 10/5
                    EVC8_MMIDriverMessage.MMI_I_TEXT = 5;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT = 700;
                    EVC8_MMIDriverMessage.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        @"1. DMI displays ‘<Unknown> brake demand’ in areas E5-E9.");

                    // Step 10/6
                    EVC8_MMIDriverMessage.MMI_I_TEXT = 6;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT = 704;
                    EVC8_MMIDriverMessage.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        @"1. DMI displays ‘<Unknown> is not available’ in areas E5-E9.");

                    WaitForVerification(
                        @"Using the <Up> and <Down> buttons, check that the order of all messages top to bottom is:" +
                        Environment.NewLine + Environment.NewLine +
                        @"1. DMI displays ‘<Unknown> is not available’ in areas E5-E9." + Environment.NewLine +
                        @"2. DMI displays ‘<Unknown> brake demand’ in areas E5-E9." + Environment.NewLine +
                        @"3. DMI displays ‘<Unknown> needs data’ in areas E5-E9." + Environment.NewLine +
                        @"4. DMI displays ‘Shunting rejected due to <Unknown> Trip’ in areas E5-E9." +
                        Environment.NewLine +
                        @"5. DMI displays ‘Confirm change of inhibit STM <Unknown>’ in areas E5-E9." +
                        Environment.NewLine +
                        @"6. DMI displays ‘<Unknown> Failed’ in areas E5 - E9.");

                    break;
                case msgType.typef:

                    // Step 11/1
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
                    EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT = 522;
                    EVC8_MMIDriverMessage.PlainTextMessage = "1";
                    EVC8_MMIDriverMessage.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        @"1. DMI displays ‘Restriction 1 km/h in Release Speed Area’ in areas E5-E9.");

                    // Step 11/2         
                    // spec says I_TEXT = 1 (fixed) but xml 'increments' in each message
                    // assume xml is correct   
                    EVC8_MMIDriverMessage.MMI_I_TEXT = 2;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT = 525;
                    EVC8_MMIDriverMessage.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        @"1. DMI displays ‘Brake Test timeout in 1 Hours’ in areas E5-E9.");

                    // Step 11/3
                    EVC8_MMIDriverMessage.MMI_I_TEXT = 3;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT = 534;
                    EVC8_MMIDriverMessage.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        @"1. DMI displays ‘BTM Test Timeout in 1 hours’ in areas E5-E9.");

                    // Step 11/4
                    EVC8_MMIDriverMessage.MMI_I_TEXT = 4;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT = 535;
                    EVC8_MMIDriverMessage.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        @"1. DMI displays ‘ATP Restart required in 1 Hours’ in areas E5-E9.");

                    // Step 11/5
                    EVC8_MMIDriverMessage.MMI_I_TEXT = 5;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT = 564;
                    EVC8_MMIDriverMessage.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        @"1. DMI displays ‘Confirm change of inhibit Level 1’ in areas E5-E9.");

                    // Step 11/6
                    EVC8_MMIDriverMessage.MMI_I_TEXT = 6;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT = 574;
                    EVC8_MMIDriverMessage.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        @"1. DMI displays ‘Cabin Reactivation required in 1 hours’ in areas E5-E9.");

                    // Step 11/7
                    EVC8_MMIDriverMessage.MMI_I_TEXT = 7;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT = 705;
                    EVC8_MMIDriverMessage.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        @"1. DMI displays ‘New power-up required in 1 hours’ in areas E5-E9.");

                    WaitForVerification(
                        @"Using the <Up> and <Down> buttons, check that the order of all messages top to bottom is:" +
                        Environment.NewLine + Environment.NewLine +
                        @"1. DMI displays ‘New power-up required in 1 hours’ in areas E5-E9." + Environment.NewLine +
                        @"2. DMI displays Cabin Reactivation required in 1 hours’ in areas E5-E9." +
                        Environment.NewLine +
                        @"3. DMI displays ‘Confirm change of inhibit Level 1’ in areas E5-E9." + Environment.NewLine +
                        @"4. DMI displays ‘ATP Restart required in 1 Hours’ in areas E5-E9." + Environment.NewLine +
                        @"5. DMI displays ‘BTM Test Timeout in 1 hours’ in areas E5-E9." + Environment.NewLine +
                        @"6. DMI displays ‘Brake Test timeout in 1 Hours’ in areas E5 - E9." + Environment.NewLine +
                        @"7. DMI displays ‘Restriction 1 km/h in Release Speed Area’ in areas E5 - E9.");

                    break;
                case msgType.typeg:

                    // Step 12/1
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
                    EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT = 522;
                    EVC8_MMIDriverMessage.PlainTextMessage = "0";
                    EVC8_MMIDriverMessage.Send();

                    WaitForVerification("Check the following where [@] is a solid white rectangular symbol:" +
                                        Environment.NewLine + Environment.NewLine +
                                        @"1. DMI displays ‘Restriction [@] km/h in Release Speed Area’ in areas E5-E9.");

                    // Step 12/2         
                    // spec says I_TEXT = 1 (fixed) but xml 'increments' in each message
                    // assume xml is correct   
                    EVC8_MMIDriverMessage.MMI_I_TEXT = 2;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT = 525;
                    EVC8_MMIDriverMessage.Send();

                    WaitForVerification("Check the following where [@] is a solid white rectangular symbol:" +
                                        Environment.NewLine + Environment.NewLine +
                                        @"1. DMI displays ‘Brake Test timeout in [@] Hours’ in areas E5-E9.");

                    // Step 12/3
                    EVC8_MMIDriverMessage.MMI_I_TEXT = 3;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT = 534;
                    EVC8_MMIDriverMessage.Send();

                    WaitForVerification("Check the following where [@] is a solid white rectangular symbol:" +
                                        Environment.NewLine + Environment.NewLine +
                                        @"1. DMI displays ‘BTM Test Timeout in [@] hours’ in areas E5-E9.");

                    // Step 12/4
                    EVC8_MMIDriverMessage.MMI_I_TEXT = 4;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT = 535;
                    EVC8_MMIDriverMessage.Send();

                    WaitForVerification("Check the following where [@] is a solid white rectangular symbol:" +
                                        Environment.NewLine + Environment.NewLine +
                                        @"1. DMI displays ‘ATP Restart required in [@] Hours’ in areas E5-E9.");

                    // Step 12/5
                    EVC8_MMIDriverMessage.MMI_I_TEXT = 5;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT = 564;
                    EVC8_MMIDriverMessage.Send();

                    WaitForVerification("Check the following where [@] is a solid white rectangular symbol:" +
                                        Environment.NewLine + Environment.NewLine +
                                        @"1. DMI displays ‘Confirm change of inhibit Level [@]’ in areas E5-E9.");

                    // Step 12/6
                    EVC8_MMIDriverMessage.MMI_I_TEXT = 6;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT = 574;
                    EVC8_MMIDriverMessage.Send();

                    WaitForVerification("Check the following where [@] is a solid white rectangular symbol:" +
                                        Environment.NewLine + Environment.NewLine +
                                        @"1. DMI displays ‘Cabin Reactivation required in [@] hours’ in areas E5-E9.");

                    // Step 12/7
                    EVC8_MMIDriverMessage.MMI_I_TEXT = 7;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT = 705;
                    EVC8_MMIDriverMessage.Send();

                    WaitForVerification("Check the following where [@] is a solid white rectangular symbol:" +
                                        Environment.NewLine + Environment.NewLine +
                                        @"1. DMI displays ‘New power-up required in [@] hours’ in areas E5-E9.");

                    WaitForVerification(
                        @"Using the <Up> and <Down> buttons, check that the order of all messages top to bottom is:" +
                        Environment.NewLine + Environment.NewLine +
                        @"1. DMI displays ‘New power-up required in [@] hours’ in areas E5-E9." + Environment.NewLine +
                        @"2. DMI displays Cabin Reactivation required in [@] hours’ in areas E5-E9." +
                        Environment.NewLine +
                        @"3. DMI displays ‘Confirm change of inhibit Level [@]’ in areas E5-E9." + Environment.NewLine +
                        @"4. DMI displays ‘ATP Restart required in [@] Hours’ in areas E5-E9." + Environment.NewLine +
                        @"5. DMI displays ‘BTM Test Timeout in [@] hours’ in areas E5-E9." + Environment.NewLine +
                        @"6. DMI displays ‘Brake Test timeout in [@] Hours’ in areas E5 - E9." + Environment.NewLine +
                        @"7. DMI displays ‘Restriction [@] km/h in Release Speed Area’ in areas E5 - E9.");

                    break;
                case msgType.typeh:

                    // Step 13/1
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
                    EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT = 533;
                    EVC8_MMIDriverMessage.PlainTextMessage = "1";
                    EVC8_MMIDriverMessage.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        @"1. DMI displays BTM Test Timeout’ in sub-area E5.");

                    // Step 13/2         
                    // spec says I_TEXT = 1 (fixed) but xml 'incremented' in next message
                    // assume xml is correct   
                    EVC8_MMIDriverMessage.MMI_I_TEXT = 2;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT = 260;
                    EVC8_MMIDriverMessage.Send();

                    // Spec says ST02 but the symbol displayed is ST01!
                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        "1. DMI displays ST01 symbol in area C9.");

                    break;
                case msgType.typei:
                    // Step 5/1
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 5;
                    EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT = 256;
                    EVC8_MMIDriverMessage.PlainTextMessage = "TEST";
                    EVC8_MMIDriverMessage.Send();

                    Wait_Realtime(11000);

                    // Step 5/2            
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
                    EVC8_MMIDriverMessage.PlainTextMessage = " DMI";
                    EVC8_MMIDriverMessage.Send();


                    break;
                case msgType.typej:
                    // Step 6/1
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 5;
                    EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT = 256;
                    EVC8_MMIDriverMessage.PlainTextMessage = "TEST";
                    EVC8_MMIDriverMessage.Send();

                    Wait_Realtime(9000);

                    // Step 6/2            
                    EVC8_MMIDriverMessage.PlainTextMessage = " DMI";
                    EVC8_MMIDriverMessage.Send();
                    Wait_Realtime(9000);

                    // Step 6/3         
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
                    EVC8_MMIDriverMessage.PlainTextMessage = "TEST";
                    EVC8_MMIDriverMessage.Send();


                    break;
                case msgType.typek:

                    // Step 7/1
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 5;
                    EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT = 256;
                    EVC8_MMIDriverMessage.PlainTextMessage = "TEST";
                    EVC8_MMIDriverMessage.Send();
                    Wait_Realtime(4000);


                    // Step 7/2            
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 0;
                    EVC8_MMIDriverMessage.MMI_I_TEXT = 2;
                    EVC8_MMIDriverMessage.PlainTextMessage = " DMI";
                    EVC8_MMIDriverMessage.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        @"1. After 4s DMI displays ‘ DMI’ in area E5 with a yellow flashing frame.");

                    Wait_Realtime(4000);

                    // Step 7/3                      
                    EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
                    EVC8_MMIDriverMessage.PlainTextMessage = " DMI";
                    EVC8_MMIDriverMessage.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        @"1. After a further 4s DMI displays ‘TEST DMI’ in area E5 with a yellow flashing frame.");
                    break;
            }
        }

        #endregion
    }
}
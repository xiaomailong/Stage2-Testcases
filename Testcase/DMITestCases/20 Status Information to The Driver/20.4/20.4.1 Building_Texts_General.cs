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
    public class Building_Texts_General : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // System is power on.Cabin is activateSoM is perform in SR mode, Level 1.The active language is English.

            // Call the TestCaseBase PreExecution
            base.PreExecution();
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
            // Testcase entrypoint


            /*
            Test Step 1
            Action: Drive the train forward pass BG1.Then, stop the train
            Expected Result: Verify the following information,Use the log file to confirm that DMI received packet EVC-8 with MMI_Q_TEXT = 256 (Plain Text Message).The plain text message ‘TEST 15.4’ is displayed on sub-area E5.Text messsage is display in white colour.The local time of appearance is attached in front of plain text messge at the first line.The local time is separated from the first character of displayed text by an indent.The format of local time is “hh:mm” with a 24 hours time reference
            Test Step Comment: (1) MMI_gen 7025 (partly: packet EVC-8, 1st bullet);(2) MMI_gen 7025 (partly: 2nd bullet, #1);(3) MMI_gen 1046 (partly: bullet a, non-acknowledgeable Text message); (4) MMI_gen 1046 (b) (partly: attached to the first line);   (5) MMI_gen 1046 (b) (partly: separated from the first character by an indent);           (6) MMI_gen 1046 (b) (partly: format of local time);
            */
            // Call generic Action Method
            DmiActions.Drive_the_train_forward_pass_BG1_Then_stop_the_train();


            /*
            Test Step 2
            Action: Acknowledge the plain text message
            Expected Result: Use the log file to confirm that DMI sends out packet [MMI_DRIVER_ACTION (EVC-152)] with the value of variable MMI_M_DRIVER_ACTION refer to sequence below,a)   MMI_M_DRIVER_ACTION = 23 (Ack of Plain Text message)
            Test Step Comment: MMI_gen 11470 (partly: Bit #23);
            */


            /*
            Test Step 3
            Action: Use the test script file 15_4_a.xml to send 2 packets information EVC-8 with,MMI_Q_TEXT_CRITERIA = 5MMI_I_TEXT = 1MMI_Q_TEXT = 256MMI_N_TEXT = 4MMI_X_TEXT[0] = 84MMI_X_TEXT[1] = 69MMI_X_TEXT[2] = 83MMI_X_TEXT[3] = 84Send the second packet after time pass 10 second.MMI_Q_TEXT_CRITERIA = 3MMI_I_TEXT = 1MMI_Q_TEXT = 256MMI_N_TEXT = 4MMI_X_TEXT[0] = 32MMI_X_TEXT[1] = 68MMI_X_TEXT[2] = 77MMI_X_TEXT[3] = 73Note: Stopwatch is required
            Expected Result: Verify the following information,Only “ DMI” from the second packet is displayed in area E5 due to the expiry of 10 seconds
            Test Step Comment: (1) MMI_gen 7045; 
            */


            /*
            Test Step 4
            Action: Use the test script file 15_4_b.xml to send 2 packets information EVC-8 with,MMI_Q_TEXT_CRITERIA = 5MMI_I_TEXT = 1MMI_Q_TEXT = 256MMI_N_TEXT = 4MMI_X_TEXT[0] = 84MMI_X_TEXT[1] = 69MMI_X_TEXT[2] = 83MMI_X_TEXT[3] = 84Send the second packet after time pass 9 seconds.MMI_Q_TEXT_CRITERIA = 3MMI_I_TEXT = 1MMI_Q_TEXT = 256MMI_N_TEXT = 4MMI_X_TEXT[0] = 32MMI_X_TEXT[1] = 68MMI_X_TEXT[2] = 77MMI_X_TEXT[3] = 73
            Expected Result: Verify the display in sub-area E5-E9 as follows,Once the first message is sent, DMI does not show the message as the message is marked as an incomplete sentence.DMI displays “TEST DMI” in area E5 after the second message is sent
            Test Step Comment: (1) MMI_gen 2557 (partly: first part, EVC-8.criteria = 5, incomplete sentence)(2) MMI_gen 2557 (partly: 9 seconds, second part, the same index, non-ack);           MMI_gen 7046 (partly: concatenate, second packet, non-ack);      MMI_gen 7025 (partly: 2nd bullet, #1);
            */


            /*
            Test Step 5
            Action: Use the test script file 15_4_i.xml to send 2 packets information EVC-8 with,MMI_Q_TEXT_CRITERIA = 5MMI_I_TEXT = 1MMI_Q_TEXT = 256MMI_N_TEXT = 4MMI_X_TEXT[0] = 84MMI_X_TEXT[1] = 69MMI_X_TEXT[2] = 83MMI_X_TEXT[3] = 84Send the second packet after time pass 11 seconds.MMI_Q_TEXT_CRITERIA = 3MMI_I_TEXT = 1MMI_Q_TEXT = 256MMI_N_TEXT = 4MMI_X_TEXT[0] = 32MMI_X_TEXT[1] = 68MMI_X_TEXT[2] = 77MMI_X_TEXT[3] = 73Note: A stopwatch is required
            Expected Result: Verify the following information,(1) Only “ DMI” from the second packet is displayed in area E5 due to the expiry of 11 seconds
            Test Step Comment: (1) MMI_gen 2557 (partly: 11 seconds, second part, the same index, non-ack); MMI_gen 7045; 
            */


            /*
            Test Step 6
            Action: Use the test script file 15_4_j.xml to send 3 packets information EVC-8 with,MMI_Q_TEXT_CRITERIA = 5MMI_I_TEXT = 1MMI_Q_TEXT = 256MMI_N_TEXT = 4MMI_X_TEXT[0] = 84MMI_X_TEXT[1] = 69MMI_X_TEXT[2] = 83MMI_X_TEXT[3] = 84Send the second packet after time pass 9 seconds.MMI_Q_TEXT_CRITERIA = 5MMI_I_TEXT = 1MMI_Q_TEXT = 256MMI_N_TEXT = 4MMI_X_TEXT[0] = 32MMI_X_TEXT[1] = 68MMI_X_TEXT[2] = 77MMI_X_TEXT[3] = 73Send the third packet after time pass 9 seconds.MMI_Q_TEXT_CRITERIA = 3MMI_I_TEXT = 1MMI_Q_TEXT = 256MMI_N_TEXT = 4MMI_X_TEXT[0] = 84MMI_X_TEXT[1] = 69MMI_X_TEXT[2] = 83MMI_X_TEXT[3] = 84A stopwatch is required
            Expected Result: Verify the following information,Once the first message is sent, DMI does not show the message as the message is marked as an incomplete sentence.Once the second message is sent, DMI does not show the message as the message is marked as an incomplete sentence.DMI displays “ DMITEST” in area E5 after the third message is sent.The first message is overwritten by the second message
            Test Step Comment: (1) MMI_gen 2557 (partly: first part, EVC-8.criteria = 5, incomplete sentence);(2) MMI_gen 2557 (partly: EVC-8.criteria = 5, incomplete sentence);(3) MMI_gen 2557 (partly: 9 seconds, the same index, non-ack);           MMI_gen 7046 (partly: concatenate, non-ack);      MMI_gen 7025 (partly: 2nd bullet, #1);(4) MMI_gen 7046 (partly: overwritten);
            */


            /*
            Test Step 7
            Action: Use the test script file 15_4_k.xml to send 3 packets information EVC-8 with,MMI_Q_TEXT_CRITERIA = 5MMI_I_TEXT = 1MMI_Q_TEXT = 256MMI_N_TEXT = 4MMI_X_TEXT[0] = 84MMI_X_TEXT[1] = 69MMI_X_TEXT[2] = 83MMI_X_TEXT[3] = 84Send the second packet after time pass 4 seconds.MMI_Q_TEXT_CRITERIA = 0MMI_I_TEXT = 2MMI_Q_TEXT = 256MMI_N_TEXT = 4MMI_X_TEXT[0] = 32MMI_X_TEXT[1] = 68MMI_X_TEXT[2] = 77MMI_X_TEXT[3] = 73Send the second packet after time pass 4 seconds.MMI_Q_TEXT_CRITERIA = 0MMI_I_TEXT = 1MMI_Q_TEXT = 256MMI_N_TEXT = 4MMI_X_TEXT[0] = 32MMI_X_TEXT[1] = 68MMI_X_TEXT[2] = 77MMI_X_TEXT[3] = 73Note: Stopwatch is required
            Expected Result: Verify the following information,“ DMI” from the second packet is displayed in area E5 with yellow flashing frame due to the different ID from the first packet.DMI displays “TEST DMI” in area E5 with yellow flashing frame after the third message is sent due to the same message ID as the first incomplete message
            Test Step Comment: (1) MMI_gen 2557 (partly: second part, different index);(2) MMI_gen 2557 (partly: 9 seconds, the same index, ack);           MMI_gen 7046 (partly: concatenate, ack);      MMI_gen 7025 (partly: 2nd bullet, #1);
            */


            /*
            Test Step 8
            Action: Perform the following procedure, De-activate Cabin.Activate Cabin.Then, Use the test script file 15_4_c.xml to send 2 packets information EVC-8 with,MMI_Q_TEXT_CRITERIA = 5MMI_I_TEXT = 1MMI_Q_TEXT = 256MMI_N_TEXT = 20MMI_X_TEXT[0] = 65MMI_X_TEXT[1] = 66MMI_X_TEXT[2] = 67MMI_X_TEXT[3] = 68MMI_X_TEXT[4] = 69MMI_X_TEXT[5] = 70MMI_X_TEXT[6] = 71MMI_X_TEXT[7] = 72MMI_X_TEXT[8] = 73MMI_X_TEXT[9] = 74MMI_X_TEXT[10] = 32MMI_X_TEXT[11] = 66MMI_X_TEXT[12] = 67MMI_X_TEXT[13] = 68MMI_X_TEXT[14] = 69MMI_X_TEXT[15] = 70MMI_X_TEXT[16] = 71MMI_X_TEXT[17] = 72MMI_X_TEXT[18] = 73MMI_X_TEXT[19] = 74Send the second packet after time pass 9 second.MMI_Q_TEXT_CRITERIA = 3MMI_I_TEXT = 1MMI_Q_TEXT = 256MMI_N_TEXT = 10MMI_X_TEXT[0] = 32MMI_X_TEXT[1] = 88MMI_X_TEXT[2] = 88MMI_X_TEXT[3] = 88MMI_X_TEXT[4] = 88MMI_X_TEXT[5] = 88MMI_X_TEXT[6] = 88MMI_X_TEXT[7] = 88MMI_X_TEXT[8] = 88MMI_X_TEXT[9] = 88Note: Stopwatch is required
            Expected Result: Verify the display in sub-area E5-E9 as follows,The 2 plain text messages are concatenated and displayed as “ABCDEFGHIJ BCDEFGHIJ XXXXXXXXX.Text messsage is displayed in white colour.The text message is displayed as two lines in both of sub area E5 and E6.The message in sub area E6 is aligned with the with character of display text in sub area E5
            Test Step Comment: (1) MMI_gen 2557 (partly: stored first part, same-index second part, 1st bullet);           MMI_gen 7046 (partly: concatenate, 1st bullet);      (2) MMI_gen 1046 (a) (partly: building text);  (3) MMI_gen 1046 (c) (partly: building text, continue inside the next Sub-area);(4) MMI_gen 1046 (c)  (partly: building text, Next line aligned with the first character of the display text);
            */


            /*
            Test Step 9
            Action: Use the test script file 15_4_d.xml to send multiple packets EVC-8 with the following value,Common variableMMI_N_TEXT = 1MMI_X_TEXT[0]= 20The order of MMI_Q_TEXT value in each packetMMI_Q_TEXT = 543MMI_Q_TEXT = 565MMI_Q_TEXT = 570MMI_Q_TEXT = 573MMI_Q_TEXT = 700MMI_Q_TEXT = 704
            Expected Result: Verify the display message in sub-area E5-E9 according to the order of MMI_Q_TEXT in the Action column,TPWS FailedConfirm change of inhibit STM TPWSShunting rejected due to TPWS TripTPWS needs dataTPWS brake demandTPWS is not availableNote: Use the <Up> and <Down> button to scroll the list of message in sub-area E5-E9
            Test Step Comment: MMI_gen 7025 (partly:1st bullet, 2nd bullet, #2); MMI_gen 9520 (partly: system status message in table 76 for NTC);
            */


            /*
            Test Step 10
            Action: Use the test script file 15_4_e.xml to send multiple packets EVC-8 with the follow ing value,Common variableMMI_N_TEXT = 1MMI_X_TEXT[0]= 99The order of MMI_Q_TEXT value in each packetMMI_Q_TEXT = 543MMI_Q_TEXT = 565MMI_Q_TEXT = 570MMI_Q_TEXT = 573MMI_Q_TEXT = 700MMI_Q_TEXT = 704
            Expected Result: Verify the display message in sub-area E5-E9 according to the order of MMI_Q_TEXT in the Action column,<Unknown>FailedConfirm change of inhibit STM <Unknown>Shunting rejected due to <Unknown>Trip<Unknown> needs data<Unknown> brake demand<Unknown> is not availableNote: Use the <Up> and <Down> button to scroll the list of message in sub-area E5-E9
            Test Step Comment: MMI_gen 7025 (partly:1st bullet, 2nd bullet, #2, unknown); MMI_gen 9520 (partly: system status message in table 76 for NTC);
            */


            /*
            Test Step 11
            Action: Use the test script file 15_4_f.xml to send multiple packets EVC-8 with the following value,Common variableMMI_N_TEXT = 1MMI_X_TEXT[0]= 49The order of MMI_Q_TEXT value in each packetMMI_Q_TEXT = 522MMI_Q_TEXT = 525MMI_Q_TEXT = 534MMI_Q_TEXT = 535MMI_Q_TEXT = 564MMI_Q_TEXT = 574MMI_Q_TEXT = 705
            Expected Result: Verify the display message in sub-area E5-E9 according to the order of MMI_Q_TEXT in the Action column,Restriction 1 km/h in Release Speed Area Brake Test timeout in 1 HoursBTM Test Timeout in 1 hoursATP Restart required in 1 HoursConfirm change of inhibit Level 1Cabin Reactivation required in 1 hoursNew power-up required in 1 hoursNote: Use the <Up> and <Down> button to scroll the list of message in sub-area E5-E9
            Test Step Comment: MMI_gen 7025 (partly:1st bullet, 2nd bullet, #1);
            */


            /*
            Test Step 12
            Action: Use the test script file 15_4_g.xml to send multiple packets EVC-8 with the following value,Common variableMMI_N_TEXT = 1MMI_X_TEXT[0]= 0The order of MMI_Q_TEXT value in each packetMMI_Q_TEXT = 522MMI_Q_TEXT = 525MMI_Q_TEXT = 534MMI_Q_TEXT = 535MMI_Q_TEXT = 564MMI_Q_TEXT = 574MMI_Q_TEXT = 705
            Expected Result: Verify the display message in sub-area E5-E9 according to the order of MMI_Q_TEXT in the Action column,Restriction  km/h in Release Speed Area Brake Test timeout in  HoursBTM Test Timeout in  hoursATP Restart required in  HoursConfirm change of inhibit Level Cabin Reactivation required in  hoursNew power-up required in  hoursNote: Use the <Up> and <Down> button to scroll the list of message in sub-area E5-E9. is a solid rectangular symbol in white colour
            Test Step Comment: MMI_gen 7025 (partly:1st bullet, 2nd bullet, #1, unknown);
            */


            /*
            Test Step 13
            Action: Use the test script file 15_4_h.xml to send multiple packets EVC-8 with the following value,Common variableMMI_N_TEXT = 1MMI_X_TEXT[0]= 49The order of MMI_Q_TEXT value in each packetMMI_Q_TEXT = 533MMI_Q_TEXT = 260
            Expected Result: Verify the following information,DMI displays driver message ‘BTM Test Timeout’ in sub-area E5. (MMI_X_TEXT is ignored)DMI displays symbol ST02 symbol  in sub-area C9. (MMI_X_TEXT is ignored)
            Test Step Comment: (1) MMI_gen 1066 (partly: #1, #2);(2) MMI_gen 1066 (partly: #4, ST02);
            */


            /*
            Test Step 14
            Action: End of test
            Expected Result: 
            */


            return GlobalTestResult;
        }
    }
}
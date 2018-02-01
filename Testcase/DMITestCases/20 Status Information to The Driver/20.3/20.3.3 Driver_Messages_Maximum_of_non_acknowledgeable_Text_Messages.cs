using System;
using Testcase.Telegrams.EVCtoDMI;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 20.3.3 Driver Messages: Maximum of non-acknowledgeable Text Messages
    /// TC-ID: 15.3.3
    /// 
    /// This test case verify a Driver message list handling for non-acknowledgeable text messages, order of message removing and maximum of message in list.
    /// 
    /// Tested Requirements:
    /// MMI_gen 135 (partly: 50 entries in total); MMI_gen 138 (partly: remove oldest important message, move the visibility window on top of the Message List, remove oldest auxiliary text message); MMI_gen 7048;
    /// 
    /// Scenario:
    /// Use the test script file to send EVC-8 to DMI. Then, verifies the display information.Press ‘Down’ button until it’s disabled. Then, verifies the display information.Use the test script file to send EVC-8 to DMI. Then, verifies the display information.Press ‘Down’ button until it’s disabled. Then, verifies the display information.Use the test script file to send EVC-8 to DMI. Then, verifies the display information.Simulate loss communication between DMI and ETCS.Re-establish communication. Then, verifies the display information.Note: Each step of test script file in executed continuously, Tester need to confim expected result within specific time (5 second).
    /// 
    /// Used files:
    /// 15_3_3_a.xml, 15_3_3_b.xml, 15_3_3_c.xml
    /// </summary>
    public class TC_15_3_3_Driver_Messages : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // Test system is power onPerform SoM until Level 1 is selected and confirmed.Main window is closed.

            // Call the TestCaseBase PreExecution
            base.PreExecution();
            // DMI is power on.Cabin A is activated.SoM is perform until Level 1 is selected and confirmed.Main window is closed.
            DmiActions.Complete_SoM_L1_SB(this);
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in SB mode, level 1

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
            TraceInfo(
                "Use the test script file 15_3_3_a.xml to send multiple packets EVC-8 with the following value,Common variablesMMI_Q_TEXT_CRITERIA = 3MMI_Q_TEXT_CLASS = 1The order of MMI_Q_TEXT value in each packetMMI_Q_TEXT = 0MMI_Q_TEXT = 1MMI_Q_TEXT = 267MMI_Q_TEXT = 268MMI_Q_TEXT = 269MMI_Q_TEXT = 274MMI_Q_TEXT = 275MMI_Q_TEXT = 280MMI_Q_TEXT = 290MMI_Q_TEXT = 292MMI_Q_TEXT = 296MMI_Q_TEXT = 299MMI_Q_TEXT = 305MMI_Q_TEXT = 310MMI_Q_TEXT = 315MMI_Q_TEXT = 316MMI_Q_TEXT = 320MMI_Q_TEXT = 321MMI_Q_TEXT = 514MMI_Q_TEXT = 515MMI_Q_TEXT = 516MMI_Q_TEXT = 520MMI_Q_TEXT = 521MMI_Q_TEXT = 524MMI_Q_TEXT = 526MMI_Q_TEXT = 527MMI_Q_TEXT = 531MMI_Q_TEXT = 532MMI_Q_TEXT = 533MMI_Q_TEXT = 536MMI_Q_TEXT = 540MMI_Q_TEXT = 552MMI_Q_TEXT = 554MMI_Q_TEXT = 560MMI_Q_TEXT = 563MMI_Q_TEXT = 572MMI_Q_TEXT = 606MMI_Q_TEXT = 580MMI_Q_TEXT = 581MMI_Q_TEXT = 582MMI_Q_TEXT = 621MMI_Q_TEXT = 622MMI_Q_TEXT = 701MMI_Q_TEXT = 702MMI_Q_TEXT = 703MMI_Q_TEXT = 706MMI_Q_TEXT = 711MMI_Q_TEXT = 712MMI_Q_TEXT = 713MMI_Q_TEXT = 714Note: MMI_I_TEXT is unique");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following information,(1)   The following text messages are displays on sub-area E5 respectively with sound Sinfo,Level crossing not protected AcknowledgementBalise read errorCommunication errorRunaway movementEntering FSEntering OSEmergency stopSH refusedSH request failedTrackside not compatibleTrain is rejectedTrain dividedTrain data changedSR Distance exceededSR stop orderRV distance exceededETCS IsolatedPerform Brake Test!Unable to start Brake TestBrake Test in ProgressLZB Partial Block ModeOverride LZB Partial Block ModeBrake Test successfulBrake Test TimeoutBrake Test aborted, perform new Test?BTM Test in ProgressBTM Test FailureBTM Test TimeoutRestart ATP!No Level available OnboardAnnounced levels(s) not supported OnboardReactivate the Cabin!Trackside malfunctionTrackside Level(s) not supported OnboardNo Track DescriptionSH Stop OrderProcedure Brake Percentage Entry terminated by ATPProcedure Wheel Diameter Entry terminated by ATPProcedure Doppler Radar Entry terminated by ATPUnable to start Brake Test, vehicle not readyUnblock EBRoute unsuitable – axle load categoryRoute unsuitable – loading gaugeRoute unsuitable – traction systemNo valid authentication keyNL-input signal is withdrawnWheel data settings were successfully changedDoppler radar settings were successfully changedBrake percentage was successfully changedNote: When the new message is added in sub-area E5, an every older message are moved down 1 line");
            /*
            Test Step 1
            Action: Use the test script file 15_3_3_a.xml to send multiple packets EVC-8 with the following value,Common variablesMMI_Q_TEXT_CRITERIA = 3MMI_Q_TEXT_CLASS = 1The order of MMI_Q_TEXT value in each packetMMI_Q_TEXT = 0MMI_Q_TEXT = 1MMI_Q_TEXT = 267MMI_Q_TEXT = 268MMI_Q_TEXT = 269MMI_Q_TEXT = 274MMI_Q_TEXT = 275MMI_Q_TEXT = 280MMI_Q_TEXT = 290MMI_Q_TEXT = 292MMI_Q_TEXT = 296MMI_Q_TEXT = 299MMI_Q_TEXT = 305MMI_Q_TEXT = 310MMI_Q_TEXT = 315MMI_Q_TEXT = 316MMI_Q_TEXT = 320MMI_Q_TEXT = 321MMI_Q_TEXT = 514MMI_Q_TEXT = 515MMI_Q_TEXT = 516MMI_Q_TEXT = 520MMI_Q_TEXT = 521MMI_Q_TEXT = 524MMI_Q_TEXT = 526MMI_Q_TEXT = 527MMI_Q_TEXT = 531MMI_Q_TEXT = 532MMI_Q_TEXT = 533MMI_Q_TEXT = 536MMI_Q_TEXT = 540MMI_Q_TEXT = 552MMI_Q_TEXT = 554MMI_Q_TEXT = 560MMI_Q_TEXT = 563MMI_Q_TEXT = 572MMI_Q_TEXT = 606MMI_Q_TEXT = 580MMI_Q_TEXT = 581MMI_Q_TEXT = 582MMI_Q_TEXT = 621MMI_Q_TEXT = 622MMI_Q_TEXT = 701MMI_Q_TEXT = 702MMI_Q_TEXT = 703MMI_Q_TEXT = 706MMI_Q_TEXT = 711MMI_Q_TEXT = 712MMI_Q_TEXT = 713MMI_Q_TEXT = 714Note: MMI_I_TEXT is unique
            Expected Result: Verify the following information,(1)   The following text messages are displays on sub-area E5 respectively with sound Sinfo,Level crossing not protected AcknowledgementBalise read errorCommunication errorRunaway movementEntering FSEntering OSEmergency stopSH refusedSH request failedTrackside not compatibleTrain is rejectedTrain dividedTrain data changedSR Distance exceededSR stop orderRV distance exceededETCS IsolatedPerform Brake Test!Unable to start Brake TestBrake Test in ProgressLZB Partial Block ModeOverride LZB Partial Block ModeBrake Test successfulBrake Test TimeoutBrake Test aborted, perform new Test?BTM Test in ProgressBTM Test FailureBTM Test TimeoutRestart ATP!No Level available OnboardAnnounced levels(s) not supported OnboardReactivate the Cabin!Trackside malfunctionTrackside Level(s) not supported OnboardNo Track DescriptionSH Stop OrderProcedure Brake Percentage Entry terminated by ATPProcedure Wheel Diameter Entry terminated by ATPProcedure Doppler Radar Entry terminated by ATPUnable to start Brake Test, vehicle not readyUnblock EBRoute unsuitable – axle load categoryRoute unsuitable – loading gaugeRoute unsuitable – traction systemNo valid authentication keyNL-input signal is withdrawnWheel data settings were successfully changedDoppler radar settings were successfully changedBrake percentage was successfully changedNote: When the new message is added in sub-area E5, an every older message are moved down 1 line
            Test Step Comment: (1) MMI_gen 135 (partly: 50 entries in total, first group); MMI_gen 138 (partly: first group, sound of the first group);
            */
            XML_15_3_3(msgType.typea);

            TraceHeader("Test Step 2");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Use the test script file 15_3_3_b.xml to send EVC-8 with, MMI_Q_TEXT = 715MMI_Q_TEXT_CRITERIA = 3MMI_Q_TEXT_CLASS = 0MMI_I_TEXT = 51");
            TraceReport("Expected Result");
            TraceInfo(
                "The text messages in area E5-E9 are not change.Verify the following information,(1)   There is no sound ‘Sinfo’");
            /*
            Test Step 2
            Action: Use the test script file 15_3_3_b.xml to send EVC-8 with, MMI_Q_TEXT = 715MMI_Q_TEXT_CRITERIA = 3MMI_Q_TEXT_CLASS = 0MMI_I_TEXT = 51
            Expected Result: The text messages in area E5-E9 are not change.Verify the following information,(1)   There is no sound ‘Sinfo’
            Test Step Comment: (1) MMI_gen 138 (partly: NEGATIVE, no sound of the second group);
            */
            XML_15_3_3(msgType.typeb);

            TraceHeader("Test Step 3");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Press <Down> button until it is disabled");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following information,(1)   The text message ‘Level crossing not protected’ is changed to ‘No Country Selection in LZB PB Mode’ with regular style in sub-area E9");
            /*
            Test Step 3
            Action: Press <Down> button until it is disabled
            Expected Result: Verify the following information,(1)   The text message ‘Level crossing not protected’ is changed to ‘No Country Selection in LZB PB Mode’ with regular style in sub-area E9
            Test Step Comment: (1) MMI_gen 138 (partly: remove oldest important message, new of the second group); MMI_gen 135 (partly: 50 entries in total, first group topmost);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press <Down> button until it is disabled");
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "Text message ‘Level crossing not protected’ changes to ‘No Country Selection in LZB PB Mode’ in normal style in sub-area E9");

            TraceHeader("Test Step 4");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Use the test script file 15_3_3_c.xml to send EVC-8 with, MMI_Q_TEXT = 278MMI_Q_TEXT_CRITERIA = 3MMI_Q_TEXT_CLASS = 0MMI_I_TEXT = 52");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following points,(1)   The visibility window is not moved.(2)   The text message ‘No Country Selection in LZB PB Mode’ is changed to ‘Emergency Brake Failure’.(3)   There is no sound ‘Sinfo’");
            /*
            Test Step 4
            Action: Use the test script file 15_3_3_c.xml to send EVC-8 with, MMI_Q_TEXT = 278MMI_Q_TEXT_CRITERIA = 3MMI_Q_TEXT_CLASS = 0MMI_I_TEXT = 52
            Expected Result: Verify the following points,(1)   The visibility window is not moved.(2)   The text message ‘No Country Selection in LZB PB Mode’ is changed to ‘Emergency Brake Failure’.(3)   There is no sound ‘Sinfo’
            Test Step Comment: (1) MMI_gen 138 (partly: the second group, not necessary moving the visibility window);(2) MMI_gen 138 (partly: remove oldest auxiliary message, new of the second group);(3) MMI_gen 138 (partly: negative, no sound of the second group);
            */
            XML_15_3_3(msgType.typec);

            TraceHeader("Test Step 5");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "(Continue from step 4) send EVC-8 with, MMI_Q_TEXT = 273MMI_Q_TEXT_CRITERIA = 3MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 53");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following points,The visibility window is moved on top of the message list, <Up> button is disabled and DMI display text message “Unauthorized passing of EOA / LOA” in sub-area E5");
            /*
            Test Step 5
            Action: (Continue from step 4) send EVC-8 with, MMI_Q_TEXT = 273MMI_Q_TEXT_CRITERIA = 3MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 53
            Expected Result: Verify the following points,The visibility window is moved on top of the message list, <Up> button is disabled and DMI display text message “Unauthorized passing of EOA / LOA” in sub-area E5
            Test Step Comment: (1)  MMI_gen 138 (partly: move the visibility window on top of the Message List);
            */

            TraceHeader("Test Step 6");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Press <Down> button until it is disabled");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following information,(1)  The text message ‘Emergency Brake Failure’ is removed from sub-are E9, DMI displays the text message ‘Acknowledgement’ instead");
            /*
            Test Step 6
            Action: Press <Down> button until it is disabled
            Expected Result: Verify the following information,(1)  The text message ‘Emergency Brake Failure’ is removed from sub-are E9, DMI displays the text message ‘Acknowledgement’ instead
            Test Step Comment: (1) MMI_gen 138 (partly: remove oldest auxiliary text message);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press <Down> button until it is disabled");
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Emergency Brake Failure’ is removed from sub-area E9." +
                                Environment.NewLine +
                                "2. DMI displays the text message ‘Acknowledgement’ instead.");

            TraceHeader("Test Step 7");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Simulate loss communication between DMI and ETCS");
            TraceReport("Expected Result");
            TraceInfo(
                "DMI displays the message “ATP Down Alarm” with sound alarm.Verify the following information,(1)   The non-acknowledgeable message list is flushed, no driver message display in area E5-E9");
            /*
            Test Step 7
            Action: Simulate loss communication between DMI and ETCS
            Expected Result: DMI displays the message “ATP Down Alarm” with sound alarm.Verify the following information,(1)   The non-acknowledgeable message list is flushed, no driver message display in area E5-E9
            Test Step Comment: (1) MMI_gen 7048 (partly: MMI_gen 240);
            */
            // Call generic Check Results Method
            DmiActions.Simulate_communication_loss_EVC_DMI(this);
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the text message ‘ATP Down Alarm’." + Environment.NewLine +
                                "2. A sound alarm is played." + Environment.NewLine +
                                "3. The non-acknowledgeable message list is flushed, no driver messages are displayed in areas E5-E9.");

            TraceHeader("Test Step 8");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Re-establish communication between DMI and ETCS");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following information,(1)  If ETCS Onboard re-transmits the driver messages, the messages re-appear");
            /*
            Test Step 8
            Action: Re-establish communication between DMI and ETCS
            Expected Result: Verify the following information,(1)  If ETCS Onboard re-transmits the driver messages, the messages re-appear
            Test Step Comment: Note under MMI_gen 7048;
            */
            DmiActions.Re_establish_communication_EVC_DMI(this);
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI re-displays driver messages in areas E5-E9.");

            TraceHeader("Test Step 9");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Use the test script file 15_3_3_b.xml to send EVC-8.Then, perform the following procedure, De-activate DMI cabin. Simulate loss communication between DMI and ETCS");
            TraceReport("Expected Result");
            TraceInfo(
                "DMI displays the message “ATP Down Alarm” with sound alarm.Verify the following information,(1)   The non-acknowledgeable message list is flushed, no driver message display in area E5-E9");
            /*
            Test Step 9
            Action: Use the test script file 15_3_3_b.xml to send EVC-8.Then, perform the following procedure, De-activate DMI cabin. Simulate loss communication between DMI and ETCS
            Expected Result: DMI displays the message “ATP Down Alarm” with sound alarm.Verify the following information,(1)   The non-acknowledgeable message list is flushed, no driver message display in area E5-E9
            Test Step Comment: (1) MMI_gen 7048 (partly: MMI_gen 244);
            */
            XML_15_3_3(msgType.typeb);
            DmiActions.Deactivate_Cabin(this);
            DmiActions.Simulate_communication_loss_EVC_DMI(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the text message ‘ATP Down Alarm’." + Environment.NewLine +
                                "2. A sound alarm is played." + Environment.NewLine +
                                "3. The non-acknowledgeable message list is flushed, no driver messages are displayed in areas E5-E9.");
            TraceHeader("Test Step 10");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("End of test");
            TraceReport("Expected Result");
            TraceInfo("");
            /*
            Test Step 10
            Action: End of test
            Expected Result: 
            */


            return GlobalTestResult;
        }


        #region Send_XML_15_3_3_DMI_Test_Specification

        enum msgType
        {
            typea,
            typeb,
            typec
        }

        private void XML_15_3_3(msgType type)
        {
            switch (type)
            {
                case msgType.typea:

                    // Step 1/1
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
                    EVC8_MMIDriverMessage.MMI_I_TEXT = 0;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT = 0;
                    EVC8_MMIDriverMessage.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        "1. The text message ‘Level crossing not protected’ is displayed in area E5." +
                                        Environment.NewLine +
                                        "2. Sound Sinfo is played." + Environment.NewLine +
                                        "3. All older messages are moved down one line");


                    // Step 1/2
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
                    EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT = 1;
                    EVC8_MMIDriverMessage.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        "1. The text message ‘Acknowledgement’ is displayed in area E5." +
                                        Environment.NewLine +
                                        "2. Sound Sinfo is played.");

                    // Step 1/3
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
                    EVC8_MMIDriverMessage.MMI_I_TEXT = 2;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT = 267;
                    EVC8_MMIDriverMessage.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        "1. The text message ‘Balise read error’ is displayed in area E5." +
                                        Environment.NewLine +
                                        "2. Sound Sinfo is played." + Environment.NewLine +
                                        "3. All older messages are moved down one line");

                    // Step 1/4
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
                    EVC8_MMIDriverMessage.MMI_I_TEXT = 3;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT = 268;
                    EVC8_MMIDriverMessage.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        "1. The text message ‘Communication error’ is displayed in area E5." +
                                        Environment.NewLine +
                                        "2. Sound Sinfo is played." + Environment.NewLine +
                                        "3. All older messages are moved down one line");

                    // Step 1/5
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
                    EVC8_MMIDriverMessage.MMI_I_TEXT = 4;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT = 269;
                    EVC8_MMIDriverMessage.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        "1. The text message ‘Runaway movement’ is displayed in area E5." +
                                        Environment.NewLine +
                                        "2. Sound Sinfo is played." + Environment.NewLine +
                                        "3. All older messages are moved down one line");

                    // Step 1/6
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
                    EVC8_MMIDriverMessage.MMI_I_TEXT = 5;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT = 274;
                    EVC8_MMIDriverMessage.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        "1. The text message ‘Entering FS’ is displayed in area E5." +
                                        Environment.NewLine +
                                        "2. Sound Sinfo is played.");

                    // Step 1/7
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
                    EVC8_MMIDriverMessage.MMI_I_TEXT = 6;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT = 275;
                    EVC8_MMIDriverMessage.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        "1. The text message ‘Entering OS’ is displayed in area E5." +
                                        Environment.NewLine +
                                        "2. Sound Sinfo is played." + Environment.NewLine +
                                        "3. All older messages are moved down one line");

                    // Step 1/8
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
                    EVC8_MMIDriverMessage.MMI_I_TEXT = 7;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT = 280;
                    EVC8_MMIDriverMessage.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        "1. The text message ‘Emergency stop’ is displayed in area E5." +
                                        Environment.NewLine +
                                        "2. Sound Sinfo is played." + Environment.NewLine +
                                        "3. All older messages are moved down one line");

                    // Step 1/9
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
                    EVC8_MMIDriverMessage.MMI_I_TEXT = 8;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT = 290;
                    EVC8_MMIDriverMessage.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        "1. The text message ‘SH refused’ is displayed in area E5." +
                                        Environment.NewLine +
                                        "2. Sound Sinfo is played." + Environment.NewLine +
                                        "3. All older messages are moved down one line");

                    // Step 1/10
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
                    EVC8_MMIDriverMessage.MMI_I_TEXT = 9;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT = 292;
                    EVC8_MMIDriverMessage.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        "1. The text message ‘SH request failed’ is displayed in area E5." +
                                        Environment.NewLine +
                                        "2. Sound Sinfo is played." + Environment.NewLine +
                                        "3. All older messages are moved down one line");

                    // Step 1/11
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
                    EVC8_MMIDriverMessage.MMI_I_TEXT = 10;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT = 296;
                    EVC8_MMIDriverMessage.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        "1. The text message ‘Trackside not compatible’ is displayed in area E5." +
                                        Environment.NewLine +
                                        "2. Sound Sinfo is played." + Environment.NewLine +
                                        "3. All older messages are moved down one line");

                    // Step 1/12
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
                    EVC8_MMIDriverMessage.MMI_I_TEXT = 11;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT = 299;
                    EVC8_MMIDriverMessage.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        "1. The text message ‘Train is rejected’ is displayed in area E5." +
                                        Environment.NewLine +
                                        "2. Sound Sinfo is played." + Environment.NewLine +
                                        "3. All older messages are moved down one line");

                    // Step 1/13
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
                    EVC8_MMIDriverMessage.MMI_I_TEXT = 12;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT = 305;
                    EVC8_MMIDriverMessage.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        "1. The text message ‘Train divided’ is displayed in area E5." +
                                        Environment.NewLine +
                                        "2. Sound Sinfo is played.");

                    // Step 1/14
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
                    EVC8_MMIDriverMessage.MMI_I_TEXT = 13;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT = 310;
                    EVC8_MMIDriverMessage.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        "1. The text message ‘Train data changed’ is displayed in area E5." +
                                        Environment.NewLine +
                                        "2. Sound Sinfo is played." + Environment.NewLine +
                                        "3. All older messages are moved down one line");

                    // Step 1/15
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
                    EVC8_MMIDriverMessage.MMI_I_TEXT = 14;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT = 315;
                    EVC8_MMIDriverMessage.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        "1. The text message ‘SR Distance exceeded’ is displayed in area E5." +
                                        Environment.NewLine +
                                        "2. Sound Sinfo is played." + Environment.NewLine +
                                        "3. All older messages are moved down one line");

                    // Step 1/16
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
                    EVC8_MMIDriverMessage.MMI_I_TEXT = 15;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT = 316;
                    EVC8_MMIDriverMessage.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        "1. The text message ‘SR stop order’ is displayed in area E5." +
                                        Environment.NewLine +
                                        "2. Sound Sinfo is played.");

                    // Step 1/17
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
                    EVC8_MMIDriverMessage.MMI_I_TEXT = 16;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT = 320;
                    EVC8_MMIDriverMessage.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        "1. The text message ‘RV distance exceeded’ is displayed in area E5." +
                                        Environment.NewLine +
                                        "2. Sound Sinfo is played." + Environment.NewLine +
                                        "3. All older messages are moved down one line");

                    // Step 1/18
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
                    EVC8_MMIDriverMessage.MMI_I_TEXT = 17;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT = 321;
                    EVC8_MMIDriverMessage.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        "1. The text message ‘ETCS Isolated’ is displayed in area E5." +
                                        Environment.NewLine +
                                        "2. Sound Sinfo is played.");

                    // Step 1/19
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
                    EVC8_MMIDriverMessage.MMI_I_TEXT = 18;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT = 514;
                    EVC8_MMIDriverMessage.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        "1. The text message ‘Perform Brake Test!’ is displayed in area E5." +
                                        Environment.NewLine +
                                        "2. Sound Sinfo is played." + Environment.NewLine +
                                        "3. All older messages are moved down one line");

                    // Step 1/20
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
                    EVC8_MMIDriverMessage.MMI_I_TEXT = 19;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT = 515;
                    EVC8_MMIDriverMessage.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        "1. The text message ‘Unable to start Brake Test’ is displayed in area E5." +
                                        Environment.NewLine +
                                        "2. Sound Sinfo is played." + Environment.NewLine +
                                        "3. All older messages are moved down one line");

                    // Step 1/21
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
                    EVC8_MMIDriverMessage.MMI_I_TEXT = 20;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT = 516;
                    EVC8_MMIDriverMessage.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        "1. The text message ‘Brake Test in Progress’ is displayed in area E5." +
                                        Environment.NewLine +
                                        "2. Sound Sinfo is played.");

                    // Step 1/22
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
                    EVC8_MMIDriverMessage.MMI_I_TEXT = 21;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT = 520;
                    EVC8_MMIDriverMessage.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        "1. The text message ‘LZB Partial Block Mode’ is displayed in area E5." +
                                        Environment.NewLine +
                                        "2. Sound Sinfo is played." + Environment.NewLine +
                                        "3. All older messages are moved down one line");

                    // Step 1/23
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
                    EVC8_MMIDriverMessage.MMI_I_TEXT = 22;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT = 521;
                    EVC8_MMIDriverMessage.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        "1. The text message ‘Override LZB Partial Block Mode’ is displayed in area E5." +
                                        Environment.NewLine +
                                        "2. Sound Sinfo is played." + Environment.NewLine +
                                        "3. All older messages are moved down one line");

                    // Step 1/24
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
                    EVC8_MMIDriverMessage.MMI_I_TEXT = 23;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT = 524;
                    EVC8_MMIDriverMessage.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        "1. The text message ‘Brake Test successful’ is displayed in area E5." +
                                        Environment.NewLine +
                                        "2. Sound Sinfo is played." + Environment.NewLine +
                                        "3. All older messages are moved down one line");

                    // Step 1/25
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
                    EVC8_MMIDriverMessage.MMI_I_TEXT = 24;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT = 526;
                    EVC8_MMIDriverMessage.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        "1. The text message ‘Brake Test Timeout’ is displayed in area E5." +
                                        Environment.NewLine +
                                        "2. Sound Sinfo is played." + Environment.NewLine +
                                        "3. All older messages are moved down one line");

                    // Step 1/26
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
                    EVC8_MMIDriverMessage.MMI_I_TEXT = 25;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT = 527;
                    EVC8_MMIDriverMessage.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        "1. The text message ‘Brake Test aborted, perform new Test?’ is displayed in area E5." +
                                        Environment.NewLine +
                                        "2. Sound Sinfo is played." + Environment.NewLine +
                                        "3. All older messages are moved down one line");

                    // Step 1/27
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
                    EVC8_MMIDriverMessage.MMI_I_TEXT = 26;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT = 531;
                    EVC8_MMIDriverMessage.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        "1. The text message ‘BTM Test in Progress’ is displayed in area E5." +
                                        Environment.NewLine +
                                        "2. Sound Sinfo is played." + Environment.NewLine +
                                        "3. All older messages are moved down one line");

                    // Step 1/28
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
                    EVC8_MMIDriverMessage.MMI_I_TEXT = 27;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT = 532;
                    EVC8_MMIDriverMessage.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        "1. The text message ‘BTM Test Failure’ is displayed in area E5." +
                                        Environment.NewLine +
                                        "2. Sound Sinfo is played." + Environment.NewLine +
                                        "3. All older messages are moved down one line");

                    // Step 1/29
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
                    EVC8_MMIDriverMessage.MMI_I_TEXT = 28;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT = 533;
                    EVC8_MMIDriverMessage.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        "1. The text message ‘BTM Test Timeout’ is displayed in area E5." +
                                        Environment.NewLine +
                                        "2. Sound Sinfo is played." + Environment.NewLine +
                                        "3. All older messages are moved down one line");

                    // Step 1/30
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
                    EVC8_MMIDriverMessage.MMI_I_TEXT = 29;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT = 536;
                    EVC8_MMIDriverMessage.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        "1. The text message ‘Restart ATP!’ is displayed in area E5." +
                                        Environment.NewLine +
                                        "2. Sound Sinfo is played." + Environment.NewLine +
                                        "3. All older messages are moved down one line");

                    // Step 1/31
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
                    EVC8_MMIDriverMessage.MMI_I_TEXT = 30;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT = 540;
                    EVC8_MMIDriverMessage.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        "1. The text message ‘No Level available Onboard’ is displayed in area E5." +
                                        Environment.NewLine +
                                        "2. Sound Sinfo is played." + Environment.NewLine +
                                        "3. All older messages are moved down one line");

                    // Step 1/32
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
                    EVC8_MMIDriverMessage.MMI_I_TEXT = 31;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT = 552;
                    EVC8_MMIDriverMessage.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        "1. The text message ‘Announced levels(s) not supported Onboard’ is displayed in area E5." +
                                        Environment.NewLine +
                                        "2. Sound Sinfo is played." + Environment.NewLine +
                                        "3. All older messages are moved down one line");

                    // Step 1/33
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
                    EVC8_MMIDriverMessage.MMI_I_TEXT = 32;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT = 554;
                    EVC8_MMIDriverMessage.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        "1. The text message ‘Reactivate the Cabin!’ is displayed in area E5." +
                                        Environment.NewLine +
                                        "2. Sound Sinfo is played." + Environment.NewLine +
                                        "3. All older messages are moved down one line");

                    // Step 1/34
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
                    EVC8_MMIDriverMessage.MMI_I_TEXT = 33;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT = 560;
                    EVC8_MMIDriverMessage.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        "1. The text message ‘Trackside malfunction’ is displayed in area E5." +
                                        Environment.NewLine +
                                        "2. Sound Sinfo is played." + Environment.NewLine +
                                        "3. All older messages are moved down one line");

                    // Step 1/35
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
                    EVC8_MMIDriverMessage.MMI_I_TEXT = 34;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT = 563;
                    EVC8_MMIDriverMessage.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        "1. The text message ‘Trackside Level(s) not supported Onboard’ is displayed in area E5." +
                                        Environment.NewLine +
                                        "2. Sound Sinfo is played." + Environment.NewLine +
                                        "3. All older messages are moved down one line");

                    // Step 1/36
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
                    EVC8_MMIDriverMessage.MMI_I_TEXT = 35;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT = 572;
                    EVC8_MMIDriverMessage.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        "1. The text message ‘No Track Description’ is displayed in area E5." +
                                        Environment.NewLine +
                                        "2. Sound Sinfo is played." + Environment.NewLine +
                                        "3. All older messages are moved down one line");

                    // Step 1/37
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
                    EVC8_MMIDriverMessage.MMI_I_TEXT = 36;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT = 606;
                    EVC8_MMIDriverMessage.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        "1. The text message ‘SH Stop Order’ is displayed in area E5." +
                                        Environment.NewLine +
                                        "2. Sound Sinfo is played." + Environment.NewLine +
                                        "3. All older messages are moved down one line");

                    // Step 1/38
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
                    EVC8_MMIDriverMessage.MMI_I_TEXT = 37;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT = 580;
                    EVC8_MMIDriverMessage.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        "1. The text message ‘Procedure Brake Percentage Entry terminated by ATP’ is displayed in area E5." +
                                        Environment.NewLine +
                                        "2. Sound Sinfo is played." + Environment.NewLine +
                                        "3. All older messages are moved down one line");

                    // Step 1/39
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
                    EVC8_MMIDriverMessage.MMI_I_TEXT = 38;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT = 581;
                    EVC8_MMIDriverMessage.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        "1. The text message ‘Procedure Wheel Diameter Entry terminated by ATP’ is displayed in area E5." +
                                        Environment.NewLine +
                                        "2. Sound Sinfo is played." + Environment.NewLine +
                                        "3. All older messages are moved down one line");

                    // Step 1/40
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
                    EVC8_MMIDriverMessage.MMI_I_TEXT = 39;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT = 582;
                    EVC8_MMIDriverMessage.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        "1. The text message ‘Procedure Doppler Radar Entry terminated by ATP’ is displayed in area E5." +
                                        Environment.NewLine +
                                        "2. Sound Sinfo is played." + Environment.NewLine +
                                        "3. All older messages are moved down one line");

                    // Step 1/41
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
                    EVC8_MMIDriverMessage.MMI_I_TEXT = 40;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT = 621;
                    EVC8_MMIDriverMessage.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        "1. The text message ‘Unable to start Brake Test, vehicle not ready’ is displayed in area E5." +
                                        Environment.NewLine +
                                        "2. Sound Sinfo is played." + Environment.NewLine +
                                        "3. All older messages are moved down one line");

                    // Step 1/42
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
                    EVC8_MMIDriverMessage.MMI_I_TEXT = 41;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT = 622;
                    EVC8_MMIDriverMessage.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        "1. The text message ‘Unblock EB’ is displayed in area E5." +
                                        Environment.NewLine +
                                        "2. Sound Sinfo is played." + Environment.NewLine +
                                        "3. All older messages are moved down one line");

                    // Step 1/43
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
                    EVC8_MMIDriverMessage.MMI_I_TEXT = 42;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT = 701;
                    EVC8_MMIDriverMessage.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        "1. The text message ‘Route unsuitable – axle load category’ is displayed in area E5." +
                                        Environment.NewLine +
                                        "2. Sound Sinfo is played." + Environment.NewLine +
                                        "3. All older messages are moved down one line");
                    // Step 1/44
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
                    EVC8_MMIDriverMessage.MMI_I_TEXT = 43;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT = 702;
                    EVC8_MMIDriverMessage.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        "1. The text message ‘Route unsuitable – loading gauge’ is displayed in area E5." +
                                        Environment.NewLine +
                                        "2. Sound Sinfo is played." + Environment.NewLine +
                                        "3. All older messages are moved down one line");
                    // Step 1/45
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
                    EVC8_MMIDriverMessage.MMI_I_TEXT = 44;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT = 703;
                    EVC8_MMIDriverMessage.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        "1. The text message ‘Route unsuitable – traction system’ is displayed in area E5." +
                                        Environment.NewLine +
                                        "2. Sound Sinfo is played." + Environment.NewLine +
                                        "3. All older messages are moved down one line");
                    // Step 1/46
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
                    EVC8_MMIDriverMessage.MMI_I_TEXT = 45;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT = 706;
                    EVC8_MMIDriverMessage.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        "1. The text message ‘No valid authentication key’ is displayed in area E5." +
                                        Environment.NewLine +
                                        "2. Sound Sinfo is played." + Environment.NewLine +
                                        "3. All older messages are moved down one line");
                    // Step 1/47
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
                    EVC8_MMIDriverMessage.MMI_I_TEXT = 46;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT = 711;
                    EVC8_MMIDriverMessage.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        "1. The text message ‘NL-input signal is withdrawn’ is displayed in area E5." +
                                        Environment.NewLine +
                                        "2. Sound Sinfo is played." + Environment.NewLine +
                                        "3. All older messages are moved down one line");
                    // Step 1/48
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
                    EVC8_MMIDriverMessage.MMI_I_TEXT = 47;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT = 712;
                    EVC8_MMIDriverMessage.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        "1. The text message ‘Wheel data settings were successfully changed’ is displayed in area E5." +
                                        Environment.NewLine +
                                        "2. Sound Sinfo is played." + Environment.NewLine +
                                        "3. All older messages are moved down one line");
                    // Step 1/49
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
                    EVC8_MMIDriverMessage.MMI_I_TEXT = 48;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT = 713;
                    EVC8_MMIDriverMessage.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        "1. The text message ‘Doppler radar settings were successfully changed’ is displayed in area E5." +
                                        Environment.NewLine +
                                        "2. Sound Sinfo is played." + Environment.NewLine +
                                        "3. All older messages are moved down one line");
                    // Step 1/50
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
                    EVC8_MMIDriverMessage.MMI_I_TEXT = 49;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT = 714;
                    EVC8_MMIDriverMessage.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        "1. The text message ‘Brake percentage was successfully changed’ is displayed in area E5." +
                                        Environment.NewLine +
                                        "2. Sound Sinfo is played." + Environment.NewLine +
                                        "3. All older messages are moved down one line");


                    break;
                case msgType.typeb:
                    // Step 2
                    // Discrepancy between xml and spec: spec has MMI_TEXT = 51, xml 50
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.AuxiliaryInformation;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
                    EVC8_MMIDriverMessage.MMI_I_TEXT = 50;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT = 715;
                    EVC8_MMIDriverMessage.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        "1. The text messages in areas E5-9 are unchanged." + Environment.NewLine +
                                        "2. No sound Sinfo is played." + Environment.NewLine +
                                        "3. All older messages are moved down one line");


                    break;
                case msgType.typec:

                    // Step 4
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.AuxiliaryInformation;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
                    EVC8_MMIDriverMessage.MMI_I_TEXT = 52;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT = 278;
                    EVC8_MMIDriverMessage.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        "1. The visibility window is not moved." + Environment.NewLine +
                                        "2. The text message ‘No Country Selection in LZB PB Mode’ is changed to ‘Emergency Brake Failure’." +
                                        Environment.NewLine +
                                        "3. No sound ‘Sinfo’ is played.");


                    // Step 5
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
                    EVC8_MMIDriverMessage.MMI_I_TEXT = 53;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT = 273;
                    EVC8_MMIDriverMessage.Send();

                    WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                        "1. The visibility window is moved on top of the message list." +
                                        Environment.NewLine +
                                        "2. <Up> button is disabled." + Environment.NewLine +
                                        "3. DMI displays text message ‘Unauthorized passing of EOA/LOA’ in sub-area E5.");

                    break;
            }
        }

        #endregion
    }
}
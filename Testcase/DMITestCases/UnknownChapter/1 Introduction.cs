namespace Testcase.DMITestCases
{
    /// <summary>
    /// 1 Introduction
    /// TC-ID: 26.1
    /// 
    /// This test case verifies the appearance of DMI error handling when the communication between DMI and ETCS Onboard is lost. DMI plays sound alarm and removes all information during the ATP down. DMI error handling shall comply with conditions in [MMI-ETCS-gen].
    /// 
    /// Tested Requirements:
    /// MMI_gen 244; MMI_gen 157; MMI_gen 245; MMI_gen 246; 
    /// 
    /// Scenario:
    /// Activate cabin A. Perform SoM in SR mode, Level 1.Drive the train forward pass BG1BG1: Packet 12, 21 and 27.Simulate the communication loss between DMI and ETCS Onboard. Then, verify the display information and sound.Acknowledge the message. Then, verify the display information and sound.Re-establish the communication between DMI and ETCS Onboard. Then, verify the display information and sound.Simulate the communication loss between DMI and ETCS Onboard and re-establish again. Then, verify the display information and sound.
    /// 
    /// Used files:
    /// N/A
    /// </summary>
    public class Introduction : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // System is power on.

            // Call the TestCaseBase PreExecution
            base.PreExecution();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in FS mode, level 1.

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
            TraceInfo("Activate cabin A. Driver performs SoM to SR mode, level 1");
            TraceReport("Expected Result");
            TraceInfo("DMI displays in SR mode, Level 1");
            /*
            Test Step 1
            Action: Activate cabin A. Driver performs SoM to SR mode, level 1
            Expected Result: DMI displays in SR mode, Level 1
            */
            // Call generic Action Method
            DmiActions.Activate_cabin_A_Driver_performs_SoM_to_SR_mode_level_1(this);
            // Call generic Check Results Method
            DmiExpectedResults.SR_Mode_displayed(this);


            TraceHeader("Test Step 2");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Driver drives the train forward passing BG1");
            TraceReport("Expected Result");
            TraceInfo("DMI changes from SR mode to FS mode, Level 1.The planning area is displayed");
            /*
            Test Step 2
            Action: Driver drives the train forward passing BG1
            Expected Result: DMI changes from SR mode to FS mode, Level 1.The planning area is displayed
            */


            TraceHeader("Test Step 3");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Increase the train speed until reaching the warning margin");
            TraceReport("Expected Result");
            TraceInfo("The over speed warning sound is played");
            /*
            Test Step 3
            Action: Increase the train speed until reaching the warning margin
            Expected Result: The over speed warning sound is played
            */


            TraceHeader("Test Step 4");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Simulate the communication loss between DMI and ETCS Onboard");
            TraceReport("Expected Result");
            TraceInfo(
                "DMI enters ‘ATP-down’ state.Verify that all information on DMI’s screen is disappeared. The continuous 1000Hz sound is play.DMI displays message ‘ATP Down Alarm’ with yellow flashing frame.Use log file to confirm that DMI sends out [MMI_STATUS_REPORT (EVC-102).MMI_M_MMI_STATUS] = 5 only once");
            /*
            Test Step 4
            Action: Simulate the communication loss between DMI and ETCS Onboard
            Expected Result: DMI enters ‘ATP-down’ state.Verify that all information on DMI’s screen is disappeared. The continuous 1000Hz sound is play.DMI displays message ‘ATP Down Alarm’ with yellow flashing frame.Use log file to confirm that DMI sends out [MMI_STATUS_REPORT (EVC-102).MMI_M_MMI_STATUS] = 5 only once
            Test Step Comment: (1) MMI_gen 244 (partly: 1st bullet);                                        (2) MMI_gen 244 (partly: 2nd bullet);                               (3) MMI_gen 244 (partly: 3rd bullet);                            MMI_gen 157 (partly: sound);                                             (4) MMI_gen 244 (partly: 4th bullet);                                                                 
            */
            // Call generic Action Method
            DmiActions.Simulate_the_communication_loss_between_DMI_and_ETCS_Onboard(this);


            TraceHeader("Test Step 5");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Driver acknowledges ‘ATP Down Alarm’ message");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following information,The ATP down alarm is removed.The yellow flashing frame is removed but the message ‘ATP Down Alarm’ is still displayed.Use log file to confirm that DMI sends out [MMI_STATUS_REPORT (EVC-102).MMI_M_MMI_STATUS] = 6 only once");
            /*
            Test Step 5
            Action: Driver acknowledges ‘ATP Down Alarm’ message
            Expected Result: Verify the following information,The ATP down alarm is removed.The yellow flashing frame is removed but the message ‘ATP Down Alarm’ is still displayed.Use log file to confirm that DMI sends out [MMI_STATUS_REPORT (EVC-102).MMI_M_MMI_STATUS] = 6 only once
            Test Step Comment: (1) MMI_gen 245 (partly: 1st bullet, sound);                    (2) MMI_gen 245 (partly: 1st bullet, confirm button);      (3) MMI_gen 245 (partly: 2nd bullet);
            */


            TraceHeader("Test Step 6");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Re-establish the communication between DMI and ETCS Onboard");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following information,The message ‘ATP Down Alarm’ is removed.Use log file to confirm that DMI sends out [MMI_STATUS_REPORT (EVC-102).MMI_M_MMI_STATUS] = 3 every 250ms.The normal operation is resumed");
            /*
            Test Step 6
            Action: Re-establish the communication between DMI and ETCS Onboard
            Expected Result: Verify the following information,The message ‘ATP Down Alarm’ is removed.Use log file to confirm that DMI sends out [MMI_STATUS_REPORT (EVC-102).MMI_M_MMI_STATUS] = 3 every 250ms.The normal operation is resumed
            Test Step Comment: (1) MMI_gen 246 (partly: 1st bullet, text/symbol);              (2) MMI_gen 246 (partly: 2nd bullet);                                 (3) MMI_gen 246 (partly: 3rd bullet);
            */
            // Call generic Action Method
            DmiActions.Re_establish_the_communication_between_DMI_and_ETCS_Onboard(this);


            TraceHeader("Test Step 7");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Stop the train");
            TraceReport("Expected Result");
            TraceInfo("The train is at standstill");
            /*
            Test Step 7
            Action: Stop the train
            Expected Result: The train is at standstill
            */
            // Call generic Action Method
            DmiActions.Stop_the_train(this);
            // Call generic Check Results Method
            DmiExpectedResults.The_train_is_at_standstill(this);


            TraceHeader("Test Step 8");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Simulate the communication loss between DMI and ETCS Onboard");
            TraceReport("Expected Result");
            TraceInfo("DMI enters ‘ATP-down’ state with continuous 1000Hz sound");
            /*
            Test Step 8
            Action: Simulate the communication loss between DMI and ETCS Onboard
            Expected Result: DMI enters ‘ATP-down’ state with continuous 1000Hz sound
            */
            // Call generic Action Method
            DmiActions.Simulate_the_communication_loss_between_DMI_and_ETCS_Onboard(this);


            TraceHeader("Test Step 9");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Re-establish the communication between DMI and ETCS Onboard");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify that if ATP Down is not acknowledged yet, the sound alarm and confirmation button are cleared when the communication is recovered");
            /*
            Test Step 9
            Action: Re-establish the communication between DMI and ETCS Onboard
            Expected Result: Verify that if ATP Down is not acknowledged yet, the sound alarm and confirmation button are cleared when the communication is recovered
            Test Step Comment: MMI_gen 246 (partly: before driver’s confirmation);
            */
            // Call generic Action Method
            DmiActions.Re_establish_the_communication_between_DMI_and_ETCS_Onboard(this);


            TraceHeader("Test Step 10");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("End of test");
            
            /*
            Test Step 10
            Action: End of test
            Expected Result: 
            */


            return GlobalTestResult;
        }
    }
}
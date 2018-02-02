using System;
using Testcase.Telegrams.EVCtoDMI;

namespace Testcase.DMITestCases
{
    /// <summary>
    /// 20.2.4 ETCS Level: ETCS Level Transitions by receiving data packet from ETCS Onboard (L1->L0, L0->L1)
    /// TC-ID: 15.2.4
    /// 
    /// This test case verifes a replaced symbol which should be replaced immediately after driver acknowledged level transition acknowledgement symbol.
    /// 
    /// Tested Requirements:
    /// MMI_gen 9431 (partly: LE07 is used, LE06 is replace respectively LE07); MMI_gen 7025 (partly: 2nd bullet, #4); MMI_gen 1310 (partly:LE07);  MMI_gen 9430 (partly:LE06, LE10); MMI_gen 11470 (partly: Bit #6);
    /// 
    /// Scenario:
    /// Activate Cabin A.Perform SoM in SR mode, Level 1.Drive the train forward pass BG
    /// 1.Then, verifie the display information.BG1: Packet 41Press an acknowledment at area C
    /// 1.Then, verifie the display information.Drive the train forward pass BG
    /// 3.Then, verifie the display information.BG2: Pack 12, 21, 27 and 41
    /// 
    /// Used files:
    /// 15_2_4.tdg
    /// </summary>
    public class TC_15_2_4_ETCS_Level : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // System is power ON.

            // Call the TestCaseBase PreExecution
            base.PreExecution();
            DmiActions.Start_ATP();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in FS mode, Level 1.

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
            TraceInfo("Acivate cabin A");
            TraceReport("Expected Result");
            TraceInfo("DMI displays Driver ID window");
            /*
            Test Step 1
            Action: Acivate cabin A
            Expected Result: DMI displays Driver ID window
            */

            DmiActions.Activate_Cabin_1(this);
            DmiExpectedResults.Cabin_A_is_activated(this);

            DmiActions.Set_Driver_ID(this, "1234");
            DmiActions.Send_SB_Mode(this);
            DmiExpectedResults.Driver_ID_window_displayed_in_SB_mode(this);


            TraceHeader("Test Step 2");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Perform SoM in SR mode, Level 1");
            TraceReport("Expected Result");
            TraceInfo("DMI displays in SR mode Level 1");
            /*
            Test Step 2
            Action: Perform SoM in SR mode, Level 1
            Expected Result: DMI displays in SR mode Level 1
            */

            DmiActions.Perform_SoM_in_SR_mode_Level_1(this);
            DmiExpectedResults.SR_Mode_displayed(this);
            DmiExpectedResults.Driver_symbol_displayed(this, "Level 1", "LE03", "C1", false);

            TraceHeader("Test Step 3");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Drive the train forward pass BG1");
            TraceReport("Expected Result");
            TraceInfo("DMI displays symbol LE07 in area C1 with flashing yellow frame.");
            /*
            Test Step 3
            Action: Drive the train forward pass BG1
            Expected Result: DMI displays symbol LE07 in area C1 with flashing yellow frame.
            Verify the following information,
            (1)    Use the log file to confirm that DMI receives packet information EVC-8 with the following variables,
            MMI_Q_TEXT = 276
            MMI_Q_TEXT_CRITIRIA = 1
            MMI_N_TEXT = 1
            MMI_X_TEXT = 0
            (2)    Use the log file to confirm that DMI sends out packet [MMI_DRIVER_ACTION (EVC-152)] with the value of variable MMI_M_DRIVER_ACTION refer to sequence below,
            a)   MMI_M_DRIVER_ACTION = 6 (Ack level 0)
            Test Step Comment: (1) MMI_gen 7025 (partly: 2nd bullet, #4, Ack Level 0 transition); 
                                   MMI_gen 1310 (partly:LE07); MMI_gen 9431 (partly: LE07);
                               (2) MMI_gen 11470 (partly: Bit #6);
            */

            DmiActions.Drive_the_train_forward_pass_BG1(this);

            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 276;
            EVC8_MMIDriverMessage.PlainTextMessage = "0";
            EVC8_MMIDriverMessage.Send();

            DmiExpectedResults.L0_Announcement_Ack_Requested(this);

            // Samson: I don't think that the DMI should send back EVC-152 at this point. 
            // It should wait the driver action ie. it should be sent at next step.

            TraceHeader("Test Step 4");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Press an area C1 for acknowledgement");
            TraceReport("Expected Result");
            TraceInfo("Verify the following information,");
            /*
            Test Step 4
            Action: Press an area C1 for acknowledgement
            Expected Result: Verify the following information,
            The symbol LE06 is display in area C1 instead of symbol LE07 immediately.
            Use the log file to confirm that DMI receives packet information EVC-8 with the following variables,
            MMI_Q_TEXT = 276
            MMI_Q_TEXT_CRITIRIA = 3
            MMI_N_TEXT = 1
            MMI_X_TEXT = 0
            Test Step Comment: (1) MMI_gen 9431 (partly: The symbol LE06 is replace respectively LE07); 
                                   MMI_gen 9430 (partly:LE06);
                               (2) MMI_gen 7025 (partly: 2nd bullet, #4, non-Ack Level 0 transition);
            */

            DmiActions.ShowInstruction(this, @"Perform the following action after pressing OK:" + Environment.NewLine +
                                             Environment.NewLine +
                                             "1. Acknowledge L0 annoucement by pressing DMI area C1");

            DmiExpectedResults.L0_Announcement_Ack_pressed_and_released(this);

            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 276;
            EVC8_MMIDriverMessage.PlainTextMessage = "0";
            EVC8_MMIDriverMessage.Send();

            DmiExpectedResults.Driver_symbol_displayed(this, "Announcement for Level 0", "LE06", "C1", false);

            TraceHeader("Test Step 5");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Drive the train pass a distance to level transition");
            TraceReport("Expected Result");
            TraceInfo("Level transition from Level 1 to Level 0.");
            /*
            Test Step 5
            Action: Drive the train pass a distance to level transition
            Expected Result: Level transition from Level 1 to Level 0.
            DMI displays symbol LE01 in area C8
            */
            DmiActions.Drive_the_train_pass_a_distance_to_level_transition(this);

            DmiActions.Send_L0(this);
            DmiExpectedResults.Driver_symbol_displayed(this, "Level 0", "LE01", "C8", false);

            TraceHeader("Test Step 6");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Drive the train forward pass BG3");
            TraceReport("Expected Result");
            TraceInfo("Verify the following information,The symbol LE10 is display in area C1.");
            /*
            Test Step 6
            Action: Drive the train forward pass BG3
            Expected Result: Verify the following information,The symbol LE10 is display in area C1.
            Use the log file to confirm that DMI receives packet information EVC-8 with the following variables,
            MMI_Q_TEXT = 276
            MMI_Q_TEXT_CRITIRIA = 3
            MMI_N_TEXT = 1
            MMI_X_TEXT = 1
            Test Step Comment: (1) MMI_gen 9430 (partly:LE10);
                               (2) MMI_gen 7025 (partly: 2nd bullet, #4, non-Ack Level 1 transition);
            */

            DmiActions.Drive_the_train_forward_pass_BG3(this);

            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 276;
            EVC8_MMIDriverMessage.PlainTextMessage = "1";
            EVC8_MMIDriverMessage.Send();

            DmiExpectedResults.Driver_symbol_displayed(this, "Announcement for Level 1", "LE10", "C1", false);

            TraceHeader("Test Step 7");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Drive the train pass a distance to level transition");
            TraceReport("Expected Result");
            TraceInfo("Level transition from Level 0 to Level 1.");
            /*
            Test Step 7
            Action: Drive the train pass a distance to level transition
            Expected Result: Level transition from Level 0 to Level 1.
            DMI displays symbol LE03 in area C8
            */

            DmiActions.Drive_the_train_pass_a_distance_to_level_transition(this);

            DmiActions.Send_L1(this);
            DmiExpectedResults.Driver_symbol_displayed(this, "Level 1", "LE03", "C8", false);

            TraceHeader("Test Step 8");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("End of test");
            
            /*
            Test Step 8
            Action: End of test
            Expected Result: 
            */


            return GlobalTestResult;
        }
    }
}
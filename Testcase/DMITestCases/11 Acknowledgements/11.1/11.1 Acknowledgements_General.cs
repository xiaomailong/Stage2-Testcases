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
using Testcase.Telegrams.DMItoEVC;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 11.1 Acknowledgements: General
    /// TC-ID: 6.1
    /// 
    /// This test case verifies the acknowledgement appearance of symbol, button, text message and packet EVC-111 which are corresponded to the received packet EVC-8 including DMI responses when driver takes action for acknowledgement.
    /// 
    /// Tested Requirements:
    /// MMI_gen 4483 (partly: received incoming acknowledgements, by default, sound ‘Sinfo’, sorted in descending priority order); MMI_gen 4495; MMI_gen 4469; MMI_gen 4485 (partly: action of the driver); MMI_gen 9394; MMI_gen 4499 (partly: driver's action 'ACK' or ‘NACK’, flashing frame and button disappear, the symbol shall be removed, sensitive area, text message area shall reappear, next pending acknowledgement pointed out); MMI_gen 146; MMI_gen 4466 (partly: offer an acknowledgement located on total image); MMI_gen 9393; MMI_gen 4505; MMI_gen 4471 (partly: symbol is surrounded by flashing yellow frame, 'ACK' and 'NACK' buttons are surrounded by yellow flashing frame, text message not be framed, text message only 'ACK'); MMI_gen 11232; MMI_gen 7509; MMI_gen 3374 (partly: Brake Intervention, Mode acknowledgement, Level acknowledgement); MMI_gen 3200 (partly: Brake Intervention, Mode acknowledgement, Level acknowledgement); MMI_gen 9516 (partly: acknowledgable information); MMI_gen 12025 (partly: acknowledgable information);
    /// 
    /// Scenario:
    /// Perform the test scenarios below, and verify the acknowledgement appearance of symbol, button, text message and packet EVC-111 which are corresponded to the received packet EVC-8 with each scenario.
    /// 1.Press ‘Start’ button.
    /// 2.Force the train roll away.
    /// 3.Stop the train and acknowledge the Brake Intervention symbol. 
    /// 4.Verify the sensitive area of area C9 after acknowledement symbol is removed.
    /// 5.Send EVC-8 by using test script files to show symbols / text messages. 
    /// 
    /// Used files:
    /// 6_1_a.xml, 6_1_b.xml, 6_1_c.xml
    /// </summary>
    public class TC_ID_6_1_Acknowledgements : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:

            // Call the TestCaseBase PreExecution
            base.PreExecution();

            // Test system is powered onCabin is activatedPerform SoM until train running number is entered
            EVC0_MMIStartATP.Evc0Type = EVC0_MMIStartATP.EVC0Type.GoToIdle;
            EVC0_MMIStartATP.Send();
            DmiActions.Activate_Cabin_1(this);
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in SR mode, level 1.
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in SR mode, Level 1.");

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint

            /*
            Test Step 1
            Action: Press ‘Start’ button
            Expected Result: Verify the following information,(1)   Use the log file to confirm that DMI received packet information EVC-8 with following variables,MMI_Q_TEXT = 263MMI_Q_TEXT_CRITERIA = 1(2)   The symbol ‘MO10’ is displayed in sub-area C1.(3)   A yellow flashing frame is surronded with the MO10 symbol.(4)   The sound ‘Sinfo’ is played once
            Test Step Comment: (1) MMI_gen 4483 (partly: received incoming acknowledgements); MMI_gen 11232 (partly: MMI_gen 4483 (partly: received incoming acknowledgement));(2) MMI_gen 4495; MMI_gen 4483 (partly: by default); MMI_gen 11232 (partly: MMI_gen 4483 (partly: by default), MMI_gen 4495));(3) MMI_gen 9393 (partly: flashing frame surround the object); MMI_gen 4471 (partly: symbol is surrounded by flashing yellow frame); MMI_gen 4466 (partly: offer an acknowledgement located on total image); MMI_gen 11232 (partly: MMI_gen 9393 (partly: flashing frame surround the object), MMI_gen 4471 (partly: symbol is surrounded by flashing yellow frame), MMI_gen 4466 (partly: offer an acknowledgement located on total image));(4) MMI_gen 9393 (partly: sound ‘Sinfo’); MMI_gen 4483 (partly: sound ‘Sinfo’); MMI_gen 11232 (partly: MMI_gen 9393 (partly: sound ‘Sinfo’), MMI_gen 4483 (partly: sound ‘Sinfo’)); MMI_gen 9516 (partly: acknowledgable information); MMI_gen 12025 (partly: acknowledgable information);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press ‘Start’ button");

            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 263;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the symbol MO10 in sub-area C1 in a yellow flashing frame." + Environment.NewLine +
                                "2. The ‘Sinfo’ sound is played once.");

            /*
            Test Step 2
            Action: Force the train roll away by moving of speed with ‘Neutral’ direction.Then, wait until a runaway movement is detected
            Expected Result: Verify the following information,(1)   The symbol ‘ST01’ is displayed in sub-area C9 with yellow flashing frame.(2)   The symbol MO10 with yellow flashing frame in sub-area C1  disappears.(3)   The sound ‘Sinfo’ is played once.(4)   Use the log file to confirm that DMI receives packet information EVC-8 with following variables,MMI_Q_TEXT = 260MMI_Q_TEXT_CRITERIA = 1
            Test Step Comment: (1) MMI_gen 4483 (partly: sorted in descending priority order); MMI_gen 4471 (partly: symbol is surrounded by flashing yellow frame); MMI_gen 4466 (partly: offer an acknowledgement located on total image); MMI_gen 3374 (partly: Brake Intervention, button is visible, not faulty);(2) MMI_gen 4469; MMI_gen 11232 (partly: MMI_gen 4469);(3) MMI_gen 9393 (partly: sound ‘Sinfo’); MMI_gen 4483 (partly: sound ‘Sinfo’); MMI_gen 9516 (partly: acknowledgable information); MMI_gen 12025 (partly: acknowledgable information);(4) MMI_gen 4483 (partly: received incoming acknowledgements); MMI_gen 3374 (partly: Brake Intervention, enabled by ETCS);
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 5;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 260;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the symbol ST01 in sub-area C9 in a yellow flashing frame." + Environment.NewLine +
                                "2. The ‘Sinfo’ sound is played once.");

            /*
            Test Step 3
            Action: Stop the train.Then, press and hold sub-area C9
            Expected Result: Verify the following information,(1)   Use the log file to confirm that DMI sends out packet information of EVC-111 with variables:MMI_I_TEXT = the same value related to the value of MMI_I_TEXT in EVC-8 (in test step2)MMI_Q_ACK = 1MMI_Q_BUTTON = 1 MMI_T_BUTTONEVENT is not blank(2)   The ‘ST01’ symbol is shown as pressed state, the border is removed.(3)   The sound ‘Click’ is played once
            Test Step Comment: (1) MMI_gen 146; MMI_gen 11232 (partly: MMI_gen 146 (partly: pressed)); MMI_gen 3200 (partly: Brake Intervention, pressed, MMI_gen 11387 (partly: send events of Pressed independently to ETCS), MMI_gen 11907 (partly: EVC-101, timestamp));(2) MMI_gen 3200 (partly: MMI_gen 4381 (partly: change to state ‘Pressed’ as long as remain actuated));(3) MMI_gen 3200 (partly: MMI_gen 4381 (partly: the sound for Up-Type button));
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 5;
            DmiActions.ShowInstruction(this, "Press and hold sub-area C9");

            EVC111_MMIDriverMessageAck.Check_MMI_Q_ACK = EVC111_MMIDriverMessageAck.MMI_Q_ACK.AcknowledgeYES;
            EVC111_MMIDriverMessageAck.Check_MMI_Q_BUTTON = Variables.MMI_Q_BUTTON.Pressed;
            
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the symbol ST01 pressed without the yellow flashing frame." + Environment.NewLine +
                                "2. The ‘Click’ sound is played once.");

            /*
            Test Step 4
            Action: Slide out sub-area C9
            Expected Result: The border of the button is shown (state ‘Enabled’) without a sound
            Test Step Comment: MMI_gen 3200 (partly: MMI_gen 4382 (partly: Brake Intervention, state ‘Enabled’ when slide out with force applied, no sound));
            */
            DmiActions.ShowInstruction(this, "Whilst keeping sub-area C9 pressed, drag outside the area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The border of sub-area C9 is displayed enabled." + Environment.NewLine +
                                "2. No sound is played.");

            /*
            Test Step 5
            Action: Slide back into sub-area C9
            Expected Result: The button is back to state ‘Pressed’ without a sound
            Test Step Comment: MMI_gen 3200 (partly: Brake Intervention, MMI_gen 4382 (partly: state ‘Pressed’ when slide back, no sound));
            */
            DmiActions.ShowInstruction(this, "Whilst keeping sub-area C9 pressed, drag back inside the area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The border of sub-area C9 is displayed pressed." + Environment.NewLine +
                                "2. No sound is played.");

            /*
            Test Step 6
            Action: Release the pressed area.Note: Stopwatch is required
            Expected Result: Verify the following information,(1)   Use the log file to confirm that DMI sends out packet information of EVC-111 with variables:MMI_I_TEXT = the same value related to the value of MMI_I_TEXT in EVC-8 (in test step2)MMI_Q_ACK = 1MMI_Q_BUTTON = 0MMI_T_BUTTONEVENT is not blank(2)   The symbol ST01 with yellow flashing frame is removed from sub-area C9.(3)   After 1 second, the symbol MO10 with yellow flashing frame re-appears in sub-area C1
            Test Step Comment: (1) MMI_gen 146; MMI_gen 11232 (partly: MMI_gen 146 (partly: released)); MMI_gen 3200 (partly: Brake Intervention, released);(2) MMI_gen 4499 (partly: driver's action 'ACK', flashing frame and button disappear, the symbol shall be removed); MMI_gen 9394 (partly: object); MMI_gen 4485 (partly: action of the driver); MMI_gen 11232 (partly: MMI_gen 4499 (partly: driver's action 'ACK', flashing frame and button disappear, the symbol shall be removed), MMI_gen 9394 (partly: object), MMI_gen 4485 (partly: action of the driver));(3) MMI_gen 4499 (partly: next pending acknowledgement pointed out); MMI_gen 11232 (partly: MMI_gen 4499 (partly: next pending acknowledgement pointed out));
            */
            DmiActions.ShowInstruction(this, "Release the pressed area");

            EVC111_MMIDriverMessageAck.Check_MMI_Q_ACK = EVC111_MMIDriverMessageAck.MMI_Q_ACK.AcknowledgeYES;
            EVC111_MMIDriverMessageAck.Check_MMI_Q_BUTTON = Variables.MMI_Q_BUTTON.Released;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI stops displaying the symbol ST01 with a yellow flashing frame in sub-area C1.");

            System.Threading.Thread.Sleep(1000);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI re-displays the symbol ST01 in sub-area C1 with a yellow flashing frame after 1s.");

            /*
            Test Step 7
            Action: Press on sub-area C9
            Expected Result: Verify the following information,(1)   Touch sensitive areas of the acknowledgement is removed.Note: DMI will not send the packet [MMI_DRIVER_MESSAGE_ACK (EVC-111)] when there is no detection of acknowledgement
            Test Step Comment: (1) MMI_gen4499 (partly: sensitive area); MMI_gen 11232 (partly: MMI_gen4499 (partly: sensitive area));
            */
            DmiActions.ShowInstruction(this, "Press in sub-area C9");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Sub-area C1 is not touch sensitive.");

            /*
            Test Step 8
            Action: Use the test script file 6_1_a.xml to send EVC-8 to show symbol ‘MO17’,MMI_Q_TEXT = 264MMI_Q_TEXT_CRITERIA = 1MMI_I_TEXT = 1
            Expected Result: Verify the following information,(1)   The symbol ‘MO17’ is displayed in sub-area C1
            Test Step Comment: (1) MMI_gen 4483 (partly: chronological reception order); MMI_gen 11232 (partly: MMI_gen 4483 (partly: chronological reception order));
            */
            XML.XML_6_1_a.Send(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the symbol MO17 (with a yellow flashing frame) in sub-area C1.");

            /*
            Test Step 9
            Action: Perform the following procedure,Press an acknowledgement on sub-area C1.Press and hold sub-area C1 at least 2 seconds.Release the pressed area
            Expected Result: DMI displays in SR mode, Level 1
            */
            DmiActions.ShowInstruction(this, "Acknowledge by pressing on sub-area C1. Press and hold sub-area C1 for at least 2s, then release the pressed area.");

            // ?? Would be in UN mode
            // EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.StaffResponsible;

            // Call generic Check Results Method
            DmiExpectedResults.SR_Mode_displayed(this);

            /*
            Test Step 10
            Action: Use the test script file 6_1_b.xml to send EVC-8 to show text message with ACK/NACK option,MMI_Q_TEXT = 527MMI_Q_TEXT_CRITERIA = 2MMI_I_TEXT = 2
            Expected Result: DMI displays text message 'Brake test aborted, perform new Test?' in sub-area E5 with ACK/NACK buttons.Verify the following information, (1)   The 'ACK' and 'NACK' buttons are placed in sub-area E5-E9.(2)   The buttons are cleary separated and placed below the text to be acknowledged.(3)   The yellow flashing frame are surrounded 'ACK' and 'NACK' button. The text message itself is not framed.(4)   The text message ‘Brake test aborted, perform new test?” is displayed as 2 lines refer to the manual line brake ‘\n’ which added in the definition of text message in configuration file.        Note: See the definition of text message in language_mgr.xml
            Test Step Comment: (1) MMI_gen 4505 (partly: composed by sub-area E5-E9);(2) MMI_gen 4505 (partly: cleary separated, beneath the text);(3) MMI_gen 4471 (partly: 'ACK' and 'NACK' buttons are surrounded by yellow flashing frame, text message not be framed);(4) MMI_gen 7509;
            */
            XML.XML_6_1_b.Send(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the message ‘Brake test aborted, perform new Test?’ in sub-area E5 with ‘Yes’/‘No’ buttons." + Environment.NewLine +
                                "2. The ‘Yes’/‘No’ buttons are displayed in sub-areas E5-E9." + Environment.NewLine +
                                "3. The ‘Yes’/‘No’ buttons are clearly separated and placed below the text to be acknowledged." + Environment.NewLine +
                                "4. The ‘Yes’/‘No’ buttons are displayed with yellow flashing frames" + Environment.NewLine +
                                "5. The message is displayed on two lines as ‘Brake test aborted,<New line>perform new Test?’.");

            /*
            Test Step 11
            Action: Intermittently press any area in E5-E9 except ‘ACK’ and ‘NACK’ buttons
            Expected Result: Verify the following information,(1)    The DMI’s display does not change. DMI still displays text message 'Brake test aborted, perform new Test?' in sub-area E5 with ‘ACK’ and ‘NACK’ buttons.Note: DMI will not send the packet [MMI_DRIVER_MESSAGE_ACK (EVC-111)] when there is no detection of acknowledgement
            Test Step Comment: (1) MMI_gen 4505 (partly: text message not form a button);
            */
            DmiActions.ShowInstruction(this, "Press several times on area E5 without touching the ‘Yes’ or ‘No’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The DMI display does not change" + Environment.NewLine +
                                "2. DMI still displays the message ‘Brake test aborted, perform new Test?’ in sub-areas E5-E9 with ‘Yes’/‘No’ buttons.");

            /*
            Test Step 12
            Action: Press the ‘NACK’ button
            Expected Result: Verify the following information,(1)   Use the log file to confirm that DMI sends out packet information of EVC-111 with variables:         When ‘NACK’ button has been pressedMMI_I_TEXT = the same value related to the value of MMI_I_TEXT in EVC-8 (in test step2)MMI_Q_ACK = 2MMI_Q_BUTTON = 1When ‘NACK’ button has been  releasedMMI_I_TEXT = the same value related to the value of MMI_I_TEXT in EVC-8 (in test step2)MMI_Q_ACK = 2MMI_Q_BUTTON = 0(2)   The acknowledgement and text message in sub-area E5-E9 are  removed.(3)   The text message area in sub-area E5-E9 is reappeared with text’ 'Brake test aborted, perform new Test?’
            Test Step Comment: (1) MMI_gen 146; MMI_gen 4499 (partly: Driver's action 'NACK');(2) MMI_gen 4485 (partly: action of the driver); MMI_gen 4499 (partly: text message shall be removed);(3) MMI_gen 4499 (partly: text message area shall reappear);
            */
            DmiActions.ShowInstruction(this, "Press and hold the ‘No’ button");

            EVC111_MMIDriverMessageAck.Check_MMI_Q_ACK = EVC111_MMIDriverMessageAck.MMI_Q_ACK.NotAcknowledgeNO;
            EVC111_MMIDriverMessageAck.Check_MMI_Q_BUTTON = Variables.MMI_Q_BUTTON.Pressed;
            this.Wait_Realtime(100);

            DmiActions.ShowInstruction(this, "Release the ‘No’ button and check the log file for packet EVC111 from DMI with MMI_I_TEXT = 1, MMI_Q_ACK = 2, MMI_Q_BUTTON = 0");

            EVC111_MMIDriverMessageAck.Check_MMI_Q_ACK = EVC111_MMIDriverMessageAck.MMI_Q_ACK.NotAcknowledgeNO;
            EVC111_MMIDriverMessageAck.Check_MMI_Q_BUTTON = Variables.MMI_Q_BUTTON.Released;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The message and ‘Yes’ and ‘No’ buttons in sub-areas E5-E9 are removed." + Environment.NewLine +
                                "2. DMI re-displays the message ‘Brake test aborted, perform new Test?’ in sub-areas E5-E9.");

            /*
            Test Step 13
            Action: Use the test script file 6_1_c.xml to send EVC-8 to show text message without 'NACK' option,MMI_Q_TEXT = 514MMI_Q_TEXT_CRITERIA = 1MMI_I_TEXT = 3
            Expected Result: Verify the following information,(1)   The sound 'Sinfo' is played once.(2)   The text message is surrounded by flashing yellow frame which drawn around sub-area E5-E9. No ‘NACK’ button
            Test Step Comment: (1) MMI_gen 9393 (partly: sound ‘Sinfo’); MMI_gen 9516 (partly: acknowledgable information); MMI_gen 12025 (partly: acknowledgable information);(2) MMI_gen 4471 (partly: text message only 'ACK'); MMI_gen 4505 (partly: without 'NACK' option, formed by sub-area E5-E9, No ‘NACK’ button); MMI_gen 9393 (partly: flashing frame surround text message);
            */
            XML.XML_6_1_c.Send(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the message ‘Perform Brake Test!’ with a flashing yellow frame around sub-areas E5-E9." + Environment.NewLine +
                                "2. Sound ‘Sinfo’ is played once." + Environment.NewLine +
                                "3. A ‘No’ button is not displayed.");

            /*
            Test Step 14
            Action: Press on sub-area E5
            Expected Result: Verify the following information,(1)   The acknowledgeable text that covers sub-areas E5 is touch sensitive.(2)   The text message and yellow flashing frame disappears
            Test Step Comment: (1) MMI_gen 4505 (partly: touch sensitive);(2) MMI_gen 9394 (partly: text message); 
            */
            DmiActions.ShowInstruction(this, "Press in sub-area E5");
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The acknowledgeable text in sub-area E5 is touch-sensitive" + Environment.NewLine +
                                "2. The text message and flashing yellow frame are removed.");

            /*
            Test Step 15
            Action: Verify the other sensitive area E6..E9 by performing action step 10-11
            Expected Result: Verify the following information,(1)   The acknowledgeable text that covers sub-areas E6..E9 is touch sensitive
            Test Step Comment: (1) MMI_gen 4505 (partly: touch sensitive);
            */
            // repeat 10 for sub-area E6
            XML.XML_6_1_b.Send(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the message ‘Brake test aborted, perform new Test?’ in sub-area E5 with ‘Yes’/‘No’ buttons." + Environment.NewLine +
                                "2. The ‘Yes’/‘No’ buttons are displayed in sub-areas E5-E9." + Environment.NewLine +
                                "3. The ‘Yes’/‘No’ buttons are clearly separated and placed below the text to be acknowledged." + Environment.NewLine +
                                "4. The ‘Yes’/‘No’ buttons are displayed with yellow flashing frames" + Environment.NewLine +
                                "5. The message is displayed on two lines as ‘Brake test aborted,<New line>perform new Test?’.");

            // repeat 11 for sub-area E6
            DmiActions.ShowInstruction(this, "Press several times on area E6 without touching the ‘Yes’ or ‘No’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The DMI display does not change" + Environment.NewLine +
                                "2. DMI still displays the message ‘Brake test aborted, perform new Test?’ in sub-areas E5-E9 with ‘Yes’/‘No’ buttons.");

            // repeat 10 for sub-area E7
            XML.XML_6_1_b.Send(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the message ‘Brake test aborted, perform new Test?’ in sub-area E5 with ‘Yes’/‘No’ buttons." + Environment.NewLine +
                                "2. The ‘Yes’/‘No’ buttons are displayed in sub-areas E5-E9." + Environment.NewLine +
                                "3. The ‘Yes’/‘No’ buttons are clearly separated and placed below the text to be acknowledged." + Environment.NewLine +
                                "4. The ‘Yes’/‘No’ buttons are displayed with yellow flashing frames" + Environment.NewLine +
                                "5. The message is displayed on two lines as ‘Brake test aborted,<New line>perform new Test?’.");

            // repeat 11 for sub - area E6
            DmiActions.ShowInstruction(this, "Press several times on area E7 without touching the ‘Yes’ or ‘No’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The DMI display does not change" + Environment.NewLine +
                                "2. DMI still displays the message ‘Brake test aborted, perform new Test?’ in sub-areas E5-E9 with ‘Yes’/‘No’ buttons.");

            // repeat 10 for sub - area E8
            XML.XML_6_1_b.Send(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the message ‘Brake test aborted, perform new Test?’ in sub-area E5 with ‘Yes’/‘No’ buttons." + Environment.NewLine +
                                "2. The ‘Yes’/‘No’ buttons are displayed in sub-areas E5-E9." + Environment.NewLine +
                                "3. The ‘Yes’/‘No’ buttons are clearly separated and placed below the text to be acknowledged." + Environment.NewLine +
                                "4. The ‘Yes’/‘No’ buttons are displayed with yellow flashing frames" + Environment.NewLine +
                                "5. The message is displayed on two lines as ‘Brake test aborted,<New line>perform new Test?’.");

            // repeat 11 for sub - area E8
            DmiActions.ShowInstruction(this, "Press several times on area E8 without touching the ‘Yes’ or ‘No’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The DMI display does not change" + Environment.NewLine +
                                "2. DMI still displays the message ‘Brake test aborted, perform new Test?’ in sub-areas E5-E9 with ‘Yes’/‘No’ buttons.");

            // repeat 10 for sub - area E9
            XML.XML_6_1_b.Send(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the message ‘Brake test aborted, perform new Test?’ in sub-area E5 with ‘Yes’/‘No’ buttons." + Environment.NewLine +
                                "2. The ‘Yes’/‘No’ buttons are displayed in sub-areas E5-E9." + Environment.NewLine +
                                "3. The ‘Yes’/‘No’ buttons are clearly separated and placed below the text to be acknowledged." + Environment.NewLine +
                                "4. The ‘Yes’/‘No’ buttons are displayed with yellow flashing frames" + Environment.NewLine +
                                "5. The message is displayed on two lines as ‘Brake test aborted,<New line>perform new Test?’.");

            // repeat 11 for sub - area E9
            DmiActions.ShowInstruction(this, "Press several times on area E9 without touching the ‘Yes’ or ‘No’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The DMI display does not change." + Environment.NewLine +
                                "2. DMI still displays the message ‘Brake test aborted, perform new Test?’ in sub-areas E5-E9 with ‘Yes’/‘No’ buttons.");

            /*
            Test Step 16
            Action: Use the test script file 6_1_a.xml to send EVC-8.After the symbol MO17 displayed on sub-area C1, press and hold sub-area C1
            Expected Result: Verify the following information,(1)   The sound ‘Click’ is played once.(2)   The sub-area C1 is shown as pressed state, the border of button is removed.(3)   Use the log file to confirm that DMI send EVC-101 with variable MMI_Q_BUTTON = 1 (Pressed) and MMI_T_BUTTONEVENT is not blank
            Test Step Comment: (1) MMI_gen 3200 (partly: Mode acknowledgement, MMI_gen 4381 (partly: the sound for Up-Type button));(2) MMI_gen 3200 (partly: Mode acknowledgement, MMI_gen 4381 (partly: change to state ‘Pressed’ as long as remain actuated));(3) MMI_gen 3200 (partly: Mode acknowledgement, pressed, MMI_gen 11387 (partly: send events of Pressed independently to ETCS), MMI_gen 11907 (partly: EVC-101, timestamp)); 
            */
            XML.XML_6_1_a.Send(this);

            DmiActions.ShowInstruction(this, "After symbol MO17 has been displayed, press in and hold sub-area C1");

            EVC111_MMIDriverMessageAck.Check_MMI_Q_BUTTON = Variables.MMI_Q_BUTTON.Pressed;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Sub-area C1 is displayed pressed." + Environment.NewLine +
                                "2. The ‘Click’ sound is played once.");

            /*
            Test Step 17
            Action: Slide out sub-area C1
            Expected Result: The border of the sub-area C1 is shown (state ‘Enabled’) without a sound
            Test Step Comment: MMI_gen 3200 (partly: Mode acknowledgement, MMI_gen 4382 (partly: state ‘Enabled’ when slide out with force applied, no sound))); 
            */
            DmiActions.ShowInstruction(this, "Whilst keeping sub-area C1 pressed, drag outside the area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The border of sub-area C1 is displayed enabled." + Environment.NewLine +
                                "2. No sound is played.");            

            /*
            Test Step 18
            Action: Slide back into sub-area C1
            Expected Result: The sub-area C1 is back to state ‘Pressed’ without a sound
            Test Step Comment: MMMI_gen 3200 (partly: Mode acknowledgement, MMI_gen 4382 (partly: state ‘Pressed’ when slide back, no sound))); 
            */
            DmiActions.ShowInstruction(this, "Whilst keeping sub-area C1 pressed, drag back inside the area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The border of sub-area C1 is displayed pressed." + Environment.NewLine +
                                "2. No sound is played.");

            /*
            Test Step 19
            Action: Release the pressed area
            Expected Result: Verify the following information,(1)   The symbol in sub-area C1 is removed.(2)   Use the log file to confirm that DMI send EVC-101 with variable MMI_Q_BUTTON = 0 (Released) and MMI_T_BUTTONEVENT is not blank
            Test Step Comment: (1) MMI_gen 4499 (partly: text message step back as non-acknowledgementable);(2) MMI_gen 3200 (partly: Mode acknowledgement, released, MMI_gen 11387 (partly: send events of Released independently to ETCS), MMI_gen 11907 (partly: EVC-101, timestamp));
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Release the pressed area");

            EVC111_MMIDriverMessageAck.Check_MMI_Q_BUTTON = Variables.MMI_Q_BUTTON.Released;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI stops displaying symbol MO17 in sub-area C1.");

            /*
            Test Step 20
            Action: Use the test script file 6_1_d.xml to send EVC-8 with,MMI_Q_TEXT = 257MMI_Q_TEXT_CRITIRIA = 1MMI_N_TEXT = 1MMI_X_TEXT = 0
            Expected Result: DMI displays symbol LE07 in area C1 with flashing yellow frame
            Test Step Comment: MMI_gen 3374 (partly: Level acknowledgement symbol, visible, not faulty);
            */
            XML.XML_6_1_d.Send(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays symbol LE07 in area C1 with a flashing yellow frame");

            /*
            Test Step 21
            Action: Perform action step 16-19 for verify the safe up-type of Level acknowledement symbol
            Expected Result: See expected result of step 16-19
            Test Step Comment: (1) MMI_gen 3200 (partly: Level acknowledgement symbol);
            */
            // Repeat 16
            XML.XML_6_1_a.Send(this);

            DmiActions.ShowInstruction(this, "After symbol MO17 has been displayed, press in and hold sub-area C1");

            EVC111_MMIDriverMessageAck.Check_MMI_Q_BUTTON = Variables.MMI_Q_BUTTON.Pressed;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Sub-area C1 is displayed pressed." + Environment.NewLine +
                                "2. The ‘Click’ sound is played once.");

            // Repeat 17
            DmiActions.ShowInstruction(this, "Whilst keeping sub-area C1 pressed, drag outside the area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The border of sub-area C1 is displayed enabled." + Environment.NewLine +
                                "2. No sound is played.");

            // Repeat 18
            DmiActions.ShowInstruction(this, "Whilst keeping sub-area C1 pressed, drag back inside the area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The border of sub-area C1 is displayed pressed." + Environment.NewLine +
                                "2. No sound is played.");

            // Repeat 19
            DmiActions.ShowInstruction(this, @"Release the pressed area and check the log file for packet EVC-101 from DMI with MMI_Q_BUTTON = 0 (Released) and MMI_T_BUTTONEVENT non-blank");

            EVC111_MMIDriverMessageAck.Check_MMI_Q_BUTTON = Variables.MMI_Q_BUTTON.Released;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI stops displaying symbol MO17 in sub-area C1.");

            /*
            Test Step 22
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
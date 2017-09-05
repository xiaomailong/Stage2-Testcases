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
    public class Acknowledgements_Priority_of_new_incoming_acknowledgements : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:

            // Call the TestCaseBase PreExecution
            base.PreExecution();

            // System is powered onCabin is activatedPerform SoM until level 1 is selected and confirmedMain window is closed.
            DmiActions.Complete_SoM_L1_SB(this);
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in SB mode, level 1
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in SB mode, Level 1.");

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint

            /*
            Test Step 1
            Action: Use the test script file 6_3_a.xml to send EVC-8 with,MMI_Q_TEXT = 280MMI_Q_TEXT_CRITERIA = 1MMI_I_TEXT = 1
            Expected Result: DMI displays the text message ‘Emergency stop’ in sub-area E5
            */
            XML.XML_6_3_a.Send(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the message ‘Emergency stop’ in sub-area E5.");

            /*
            Test Step 2
            Action: (Continue from step 1)Send EVC-8 with, MMI_Q_TEXT = 257MMI_Q_TEXT_CRITERIA = 1MMI_I_TEXT = 2MMI_N_TEXT = 1MMI_X_TEXT = 0
            Expected Result: Verify the following information,(1)   The text message in sub-area E5 is disappeared and DMI displays LE07 symbol with yellow flashing frame in sub-area C1 instead
            Test Step Comment: (1) MMI_gen 4484;
            */
            EVC8_MMIDriverMessage.MMI_I_TEXT = 2;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 257;
            EVC8_MMIDriverMessage.PlainTextMessage = "\0x0";        // MMI_N_TEXT (length) 1
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI stops displaying the message ‘Emergency stop’ in sub-area E5 and displays symbol LE07 in sub-area C1 with a flashing yellow frame.");

            /*
            Test Step 3
            Action: (Continue from step 2)Send EVC-8 with, MMI_Q_TEXT = 257MMI_Q_TEXT_CRITERIA = 1MMI_I_TEXT = 3MMI_N_TEXT = 1MMI_X_TEXT = 1
            Expected Result: DMI displays LE11 symbol with yellow flashing frame in sub-area C1
            */
            EVC8_MMIDriverMessage.MMI_I_TEXT = 3;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 257;
            EVC8_MMIDriverMessage.PlainTextMessage = "\0x1";        // MMI_N_TEXT (length) 1

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays symbol LE11 in sub-area C1 with a flashing yellow frame.");

            /*
            Test Step 4
            Action: (Continue from step 3)Send EVC-8 with, MMI_Q_TEXT = 259MMI_Q_TEXT_CRITERIA = 1MMI_I_TEXT = 4
            Expected Result: Verify the following information,(1)   DMI displays MO08 symbol with yellow flashing frame in sub-area C1 instead of LE11 symbol
            Test Step Comment: (1) MMI_gen 4484;
            */
            EVC8_MMIDriverMessage.MMI_I_TEXT = 4;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 259;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays symbol MO08 in sub-area C1 with a flashing yellow frame.");

            /*
            Test Step 5
            Action: (Continue from step 4)Send EVC-8 with, MMI_Q_TEXT = 298MMI_Q_TEXT_CRITERIA = 1MMI_I_TEXT = 5
            Expected Result: Verify the following information,(1)   The symbol in sub-area C1 is disappeared and DMI displays the symbol DR02 in area D instead
            Test Step Comment: (1) MMI_gen 4484;
            */
            EVC8_MMIDriverMessage.MMI_I_TEXT = 5;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 298;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI stops displaying symbol MO08 in sub-area C1 and displays symbol DR02 in area D.");

            /*
            Test Step 6
            Action: (Continue from step 5)Send EVC-8 with, MMI_Q_TEXT = 260MMI_Q_TEXT_CRITERIA = 1MMI_I_TEXT = 6
            Expected Result: Verify the following information,(1)   The symbol in area D is disappeared and DMI displays the ST01 symbol on sub-area C9 instead
            Test Step Comment: (1) MMI_gen 4484;
            */
            EVC8_MMIDriverMessage.MMI_I_TEXT = 6;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 260;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI stops displaying symbol DR02 in area D and displays symbol ST01 in sub-area C9.");

            /*
            Test Step 7
            Action: Use the test script file 6_3_b.xml to send EVC-8 with,MMI_Q_TEXT = 264MMI_Q_TEXT_CRITERIA = 1MMI_I_TEXT = 7
            Expected Result: Verify the following information,(1)   The display information on DMI still not change, ST01 symbol is displayed on sub-area C9
            Test Step Comment: (1) MMI_gen 4484 (partly: NEGATIVE, lower priority, focus not moved);
            */
            XML.XML_6_3_b.Send(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI display does not change and still displays symbol ST01 in sub-area C9.");

            /*
            Test Step 8
            Action: (Continue from step 7)Send EVC-8 with, MMI_Q_TEXT = 269MMI_Q_TEXT_CRITERIA = 1MMI_I_TEXT = 8
            Expected Result: The display information on DMI still not change, ST01 symbol is displayed on sub-area C9
            */
            EVC8_MMIDriverMessage.MMI_I_TEXT = 8;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 269;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI display does not change and still displays symbol ST01 in sub-area C9.");

            /*
            Test Step 9
            Action: (Continue from step 8)Send EVC-8 with, MMI_Q_TEXT = 268MMI_Q_TEXT_CRITERIA = 1MMI_I_TEXT = 9
            Expected Result: The display information on DMI still not change, ST01 symbol is displayed on sub-area C9
            */
            EVC8_MMIDriverMessage.MMI_I_TEXT = 9;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 268;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI display does not change and still displays symbol ST01 in sub-area C9.");

            /*
            Test Step 10
            Action: (Continue from step 9)Send EVC-8 with, MMI_Q_TEXT = 267MMI_Q_TEXT_CRITERIA = 1MMI_I_TEXT = 10
            Expected Result: The display information on DMI still not change, ST01 symbol is displayed on sub-area C9
            */
            EVC8_MMIDriverMessage.MMI_I_TEXT = 10;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 267;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI display does not change and still displays symbol ST01 in sub-area C9.");

            /*
            Test Step 11
            Action: Use the test script file 6_3_c.xml to send EVC-8 with,MMI_Q_TEXT = 554MMI_Q_TEXT_CRITERIA = 1MMI_I_TEXT = 11
            Expected Result: The display information on DMI still not change, ST01 symbol is displayed on sub-area C9
            */
            XML.XML_6_3_c.Send(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI display does not change and still displays symbol ST01 in sub-area C9.");

            /*
            Test Step 12
            Action: Use the test script file 6_3_d.xml to send EVC-8 with,MMI_Q_TEXT_CRITERIA = 4MMI_I_TEXT = 6Then, press at sub-area C9
            Expected Result: Verify the following information,(1)   The symbol ST01 in sub-area C9 is removed.(2)   Use the log file to confirm that DMI is not send out packet EVC-111.(3)   After 1 second, the symbol DR02 is displayed on area D
            Test Step Comment: (1) MMI_gen 4485 (partly: ETCS Onboard); MMI_gen 4498 (partly: disappear);(2) MMI_gen 4498 (partly: sensitive area is removed);(3) MMI_gen 4498 (partly: reappear, next pending acknowledgement);
            */
            XML.XML_6_3_d.Send(this);

            DmiActions.ShowInstruction(this, "Press in sub-area C9 and check in the log file that DMI does not send packet EVC-111");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI stops displaying symbol ST01 in sub-area C9." + Environment.NewLine +
                                "2. After 1s DMI displays symbol DR02 in area D.");

            /*
            Test Step 13
            Action: Confirm an acknowledgement of TAF by pressing at area D
            Expected Result: Verify the following information,(1)    The symbols are removed refer to pressed area. DMI displays the symbol MO08 in sub-area C1. (The oldest entry of the highest priority in the list)
            Test Step Comment: (1) MMI_gen 4486 (partly: mode acknowledgement); MMI_gen 4482 (moveable focus);
            */
            DmiActions.ShowInstruction(this, "Acknowledge TAF by pressing in area D and check in the log file that DMI does not send packet EVC-111");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI stops displaying symbol DR02 in area D and displays symbol MO08 in area C1.");

            /*
            Test Step 14
            Action: Press an acknowledgement on sub-area C1
            Expected Result: DMI displays MO17 symbol on sub-area C1
            */
            DmiActions.ShowInstruction(this, @"Acknowledge by pressing in sub-area C1");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays symbol MO17 in sub-area C1");
            /*
            Test Step 15
            Action: Press an acknowledgement on sub-area C1
            Expected Result: Verify the following information,(1)    DMI displays the symbol LE07 in sub-area C1. (The oldest entry of the highest priority in the list)
            Test Step Comment: (1) MMI_gen 4486 (partly: level acknowledgement); MMI_gen 4482 (partly: moveable focus);
            */
            DmiActions.ShowInstruction(this, @"Acknowledge by pressing in sub-area C1");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays symbol LE07 in sub-area C1");

            /*
            Test Step 16
            Action: Press an acknowledgement on sub-area C1
            Expected Result: Verify the following information,(1)    The symbols is removed refer to pressed area. DMI displays the text message ‘Runaway movement’ (The oldest entry of the highest priority in the list).(2)   The text message ‘Runaway movement’ is displayed instead of ‘Emergency Stop’ from step 1
            Test Step Comment: (1) MMI_gen 4486 (partly: other acknowledgement);(2) MMI_gen 4482 (partly: overflow);
            */
            DmiActions.ShowInstruction(this, @"Acknowledge by pressing in sub-area C1");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI stops displaying symbol LE07 in sub-area C1 and displays the message ‘Runaway movement’ in sub-area E5.");

            /*
            Test Step 17
            Action: Press an acknowledgement on sub-area E5
            Expected Result: Verify the following information,(1)    The text message ‘Runaway movement’ is removed refer to pressed area. DMI displays the text message ‘Communication error’ (The oldest entry of the highest priority in the list)
            Test Step Comment: (1) MMI_gen 4486 (partly: the oldest entry);
            */
            DmiActions.ShowInstruction(this, @"Acknowledge by pressing in sub-area E5");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI stops displaying the message ‘Runaway movement’ and displays the message ‘Communication error’in sub-area E5.");

            /*
            Test Step 18
            Action: Press an acknowledgement on sub-area E5
            Expected Result: Verify the following information,(1)    The text message ‘Communication error’ is removed refer to pressed area. DMI displays the text message ‘Balise read error’ (The oldest entry of the highest priority in the list)
            Test Step Comment: (1) MMI_gen 4486 (partly: the oldest entry);
            */
            DmiActions.ShowInstruction(this, @"Acknowledge by pressing in sub-area E5");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI stops displaying the message ‘Communication error’ and displays the message ‘Balise read error’ in sub-area E5.");

            /*
            Test Step 19
            Action: Press an acknowledgement on sub-area E5
            Expected Result: Verify the following information,(1)    The text message ‘Balise read error’ is removed refer to pressed area. DMI displays the text message ‘Reactivate the Cabin!’ (The oldest entry of the highest priority in the list)
            Test Step Comment: (1) MMI_gen 4486 (partly: the oldest entry); MMI_gen 4482 (partly: 10 pending acknowledgements);
            */
            DmiActions.ShowInstruction(this, @"Acknowledge by pressing in sub-area E5");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI stops displaying the message ‘Balise read error’ and displays the message ‘Reactivate the Cabin!’ in sub-area E5.");

            /*
            Test Step 20
            Action: Use the test script file 6_3_a.xml
            Expected Result: See the expected result No.1-6
            */
            // Should steps 2-6 be repeated also??
            XML.XML_6_3_a.Send(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the message ‘Emergency stop’ in sub-area E5.");
            // ... ??
            // /// ??

            /*
            Test Step 21
            Action: Simulate loss-communication between ETCS and DMI
            Expected Result: DMI displays Default window with the  message “ATP Down Alarm” and sound alarm
            Test Step Comment: MMI_gen 6923 (partly: MMI_gen 244);
            */
            DmiActions.Simulate_communication_loss_EVC_DMI(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Default window with the message ‘ATP Down Alarm’ in sub-area E5." + Environment.NewLine +
                                "2. The ‘Alarm’ sound is played");

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

            /*
            Test Step 23
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
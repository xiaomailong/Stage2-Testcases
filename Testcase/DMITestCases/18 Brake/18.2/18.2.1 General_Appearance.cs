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
    /// 18.2.1 General Appearance
    /// TC-ID: 13.2.1
    /// 
    /// This test case verifies the general appearance of Brake Intervention symbol (ST01) in sub-area C9 with different acknowledgements on the sensitive areas. The properties of Brake Intervention complies with [MMI-ETCS-gen]. The general appearance of Brake Intervention symbol shall comply with [ERA] standard.
    /// 
    /// Tested Requirements:
    /// MMI_gen 1378; MMI_gen 1400; MMI_gen 114; MMI_gen 1382 (partly: receives packet, MMI_gen 9393, MMI_gen4499 (partly: flashing frame, symbol after acknowledgement is given, sensitivity area), MMI_gen 146); MMI_gen 116; MMI_gen 11470 (partly: Bit #16); MMI_gen 9516 (partly: removal of the Brake Intervention); MMI_gen 12025 (partly: removal of the Brake Intervention);
    /// 
    /// Scenario:
    /// Force the train roll away and verify the Brake Intervention symbol in sub-area C9 with different acknowledgements on the sensitive areas (C8, C9 and  E1)Use the XML script file to send EVC-8 with [MMI_DRIVER_MESSAGE (EVC-8).MMI_Q_TEXT_CRITERIA]=0 and verify the Brake Intervention symbol in sub-area C9 with different acknowledgements on the sensitive areas (C8, C9 and  E1)”
    /// 
    /// Used files:
    /// 13_2_1_a.xml, 13_2_1_b.xml
    /// </summary>
    public class TC_ID_13_2_1_Brake : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:.

            // Call the TestCaseBase PreExecution
            base.PreExecution();

            // Test system is power on.Cabin is activeComplete the SoM in SR mode, Level 1
            DmiActions.Complete_SoM_L1_SR(this);
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in SB mode, Level 1.

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint

            /*
            Test Step 1
            Action: Force the train roll away by moving of speed with ‘Neutral’ direction throughout the test step 2, 3, 4 and 5
            Expected Result: 
            */

            /*
            Test Step 2
            Action: Wait until a runaway movement is detected
            Expected Result: Verify the following information,The symbol ‘ST01’ is displayed when packet [MMI_DRIVER_MESSAGE (EVC-8)] is received with variable [MMI_DRIVER_MESSAGE (EVC-8).MMI_Q_TEXT]= 260Note: Use the log file to verify a received packet EVC-8.The symbol ‘ST01’ is displayed in sub-area C9A flashing frame is only surrounded in the sub-area C9
            Test Step Comment: (1) MMI_gen 114;                      (2) MMI_gen 1378;              (3) MMI_gen 1400 (partly: flashing frame);
            */
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 260;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays symbol ST01 in sub-area C9 in a yellow flashing frame.");

            /*
            Test Step 3
            Action: Press sub-area E1 (below ST01 symbol)
            Expected Result: Verify the following information,The sub-area E1 can be acknowledged as sensitive areaThe symbol ‘ST01’ is removed from the sub-area C9.Use the log file to confirm that DMI sends out packet [MMI_DRIVER_ACTION (EVC-152)] with the value of variable MMI_M_DRIVER_ACTION refer to sequence below,a)   MMI_M_DRIVER_ACTION = 16 (Brake release acknowledgement)
            Test Step Comment: (1) MMI_gen 1400 (partly: sub-area E1);     (2) MMI_gen 144 (partly: symbol removal);      (3) MMI_gen 11470 (partly: Bit #16);                              
            */
            DmiActions.ShowInstruction(this, "Press in sub-area E1");

            EVC152_MMIDriverAction.Check_MMI_M_DRIVER_ACTION = EVC152_MMIDriverAction.MMI_M_DRIVER_ACTION.BrakeReleaseAck;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Sub-area E1 can be acknowledged as sensitive area." + Environment.NewLine +
                                "2. DMI stops displaying symbol ST01");

            /*
            Test Step 4
            Action: Wait until a runaway movement is detected, and the symbol ST01 is displayed again.Then, press sub-area C8 (above ST01 symbol)
            Expected Result: Verify the following information,The sub-area C8 can be acknowledged as sensitive areaThe symbol ‘ST01’ is removed from the sub-area C9
            Test Step Comment: (1) MMI_gen 1400 (partly: sub-area C8);                                        (2) MMI_gen 144 (partly: symbol removal);                   
            */
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 260;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.Send();

            DmiActions.ShowInstruction(this, "When the symbol ST01 is displayed again, press in sub-area C8 (above ST01 symbol)");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Sub-area C8 can be acknowledged as sensitive area." + Environment.NewLine +
                                "2. DMI stops displaying symbol ST01");

            /*
            Test Step 5
            Action: Wait until a runaway movement is detected, and the symbol ST01 is displayed again.Then, press sub-area C9 (ST01 symbol)
            Expected Result: Verify the following information,The sub-area C9 can be acknowledged as sensitive areaThe symbol ‘ST01’ is removed from the sub-area C9
            Test Step Comment: (1) MMI_gen 1400 (partly: sub-area C9);                                        (2) MMI_gen 144 (partly: symbol removal);                   
            */
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 260;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.Send();

            DmiActions.ShowInstruction(this, "When the symbol ST01 is displayed again, press in sub-area C9");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Sub-area C9 can be acknowledged as sensitive area." + Environment.NewLine +
                                "2. DMI stops displaying symbol ST01");

            /*
            Test Step 6
            Action: Stop the train from runway by dropping the train’s speed to zero
            Expected Result: 
            */

            /*
            Test Step 7
            Action: Use the test script file 13_2_1_a.xml to send EVC-8 with,MMI_Q_TEXT_CRITERIA = 0MMI_Q_TEXT = 260MMI_I_TEXT = 1
            Expected Result: Verifies that the following information,The symbol ST01 is displayed in sub-area C9 with yellow flashing frame.A yellow flashing frame is surrounded in the related object (sub-area C9) and sound Sinfo is played
            Test Step Comment: (1) MMI_gen 1382 (partly: receives packet);                 (2) MMI_gen 1382 (partly: MMI_gen 9393);
            */
            XML_13_2_1(msgType.typea);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the symbol ST01 in sub-area C9 surrounded by a yellow flashing frame." + Environment.NewLine +
                                "2. DMI plays sound ‘Sinfo’");

            /*
            Test Step 8
            Action: Press on sub-area E1 (below ST01 symbol)
            Expected Result: Verify the following information,The sub-area E1 can be acknowledged as sensitive areaThe flashing frame belonging to the acknowledgement is disappearedThe symbol ‘ST01’ is still displayed.When the driver carries out an acknowledgement, the DMI will send [MMI_DRIVER_MESSAGE_ACK (EVC-111)] for pressed and released event with [MMI_DRIVER_MESSAGE (EVC-8).MMI_I_TEXT] and [MMI_DRIVER_MESSAGE_ACK (EVC-111).MMI_Q_ACK]Note: For pressed and released event, the DMI will send variable [MMI_DRIVER_MESSAGE_ACK (EVC-111).MMI_Q_ACK] = 1 with  [MMI_DRIVER_MESSAGE_ACK (EVC-111).MMI_Q_BUTTON] = 1, and then send [MMI_DRIVER_MESSAGE_ACK (EVC-111).MMI_Q_ACK] = 1 with  [MMI_DRIVER_MESSAGE_ACK (EVC-111).MMI_Q_BUTTON] = 0
            Test Step Comment: (1) MMI_gen 1400 (partly: sub-area E1);                                        (2) MMI_gen 1382 (partly: MMI_gen4499 (partly: flashing frame));                    (3) MMI_gen 1382 (partly: MMI_gen4499 (partly: symbol after acknowledgement is given));                                    (4) MMI_gen 1382 (partly: MMI_gen146);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, "Press in sub-area E1 (below ST01 symbol)");

            EVC111_MMIDriverMessageAck.MMI_I_TEXT = 1;
            EVC111_MMIDriverMessageAck.MMI_Q_ACK = MMI_Q_ACK.AcknowledgeYES;
            EVC111_MMIDriverMessageAck.MMI_Q_BUTTON = Variables.MMI_Q_BUTTON.Pressed;

            this.Wait_Realtime(2000);

            EVC111_MMIDriverMessageAck.MMI_I_TEXT = 1;
            EVC111_MMIDriverMessageAck.MMI_Q_ACK = MMI_Q_ACK.AcknowledgeYES;
            EVC111_MMIDriverMessageAck.MMI_Q_BUTTON = Variables.MMI_Q_BUTTON.Released;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Sub-area E1 can be acknowledged as sensitive area." + Environment.NewLine +
                                "2. The yellow flashing frame surrounding the ST01 symbol is removed." + Environment.NewLine +
                                "3. DMI still displays symbol ST01");

            /*
            Test Step 9
            Action: Press on sub-area E1 (below ST01 symbol)
            Expected Result: Verify the following information,Touch sensitive areas of the acknowledgement is removed.Note: DMI will not send the packet [MMI_DRIVER_MESSAGE_ACK (EVC-111)] when there is no detection of acknowledgement
            Test Step Comment: (1) MMI_gen 1382 (partly: MMI_gen 4499 (partly: sensitive area));
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press on sub-area E1 (below ST01 symbol)");

            EVC111_MMIDriverMessageAck.MMI_I_TEXT = 1;
            EVC111_MMIDriverMessageAck.MMI_Q_ACK = MMI_Q_ACK.AcknowledgeYES;
            EVC111_MMIDriverMessageAck.MMI_Q_BUTTON = Variables.MMI_Q_BUTTON.Pressed;

            this.Wait_Realtime(2000);

            EVC111_MMIDriverMessageAck.MMI_I_TEXT = 1;
            EVC111_MMIDriverMessageAck.MMI_Q_ACK = MMI_Q_ACK.AcknowledgeYES;
            EVC111_MMIDriverMessageAck.MMI_Q_BUTTON = Variables.MMI_Q_BUTTON.Released;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The touch-sensitive area in sub-area E1 is removed.");

            /*
            Test Step 10
            Action: This step is to clear the symbol ‘ST01’ after verification of the previous step.Use the test script file 13_2_1_b.xml to send EVC-8 with,MMI_Q_TEXT_CRITERIA = 4MMI_I_TEXT = 1
            Expected Result: Verify the following information, (1)   Sound Sinfo is played
            Test Step Comment: (1) MMI_gen 116; MMI_gen 9516 (partly: removal of the Brake Intervention); MMI_gen 12025 (partly: removal of the Brake Intervention);
            */
            // Call generic Action Method
            XML_13_2_1(msgType.typeb);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI plays sound ‘Sinfo’");

            /*
            Test Step 11
            Action: Use the test script file 13_2_1_a.xml to send EVC-8 with,MMI_Q_TEXT_CRITERIA = 0MMI_Q_TEXT = 260MMI_I_TEXT = 1Then, press at sub-area C8 (above ST01 symbol)
            Expected Result: Verify the following information, The sub-area C8 can be acknowledged as sensitive areaThe flashing frame belonging to the acknowledgement is disappearedThe symbol ‘ST01’ is still displayed.When the driver carries out an acknowledgement, the DMI will send [MMI_DRIVER_MESSAGE_ACK (EVC-111)] for pressed and released event with [MMI_DRIVER_MESSAGE (EVC-8).MMI_I_TEXT] and [MMI_DRIVER_MESSAGE_ACK (EVC-111).MMI_Q_ACK]Note: For pressed and released event, the DMI will send variable [MMI_DRIVER_MESSAGE_ACK (EVC-111).MMI_Q_ACK] = 1 with  [MMI_DRIVER_MESSAGE_ACK (EVC-111).MMI_Q_BUTTON] = 1, and then send [MMI_DRIVER_MESSAGE_ACK (EVC-111).MMI_Q_ACK] = 1 with  [MMI_DRIVER_MESSAGE_ACK (EVC-111).MMI_Q_BUTTON] = 0
            Test Step Comment: (1) MMI_gen 1400 (partly: C8);                                         (2) MMI_gen 1382 (partly: MMI_gen 4499 (partly: flashing frame));                       (3) MMI_gen 1382 (partly: MMI_gen 4499 (partly: symbol after acknowledgement is given));(4) MMI_gen 1382 (partly: MMI_gen146);
            */
            XML_13_2_1(msgType.typea);

            DmiActions.ShowInstruction(this, "Press in sub-area C8 (below ST01 symbol)");

            EVC111_MMIDriverMessageAck.MMI_I_TEXT = 1;
            EVC111_MMIDriverMessageAck.MMI_Q_ACK = MMI_Q_ACK.AcknowledgeYES;
            EVC111_MMIDriverMessageAck.MMI_Q_BUTTON = Variables.MMI_Q_BUTTON.Pressed;

            this.Wait_Realtime(2000);

            EVC111_MMIDriverMessageAck.MMI_I_TEXT = 1;
            EVC111_MMIDriverMessageAck.MMI_Q_ACK = MMI_Q_ACK.AcknowledgeYES;
            EVC111_MMIDriverMessageAck.MMI_Q_BUTTON = Variables.MMI_Q_BUTTON.Released;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Sub-area C8 can be acknowledged as sensitive area." + Environment.NewLine +
                                "2. The yellow flashing frame surrounding the ST01 symbol is removed." + Environment.NewLine +
                                "3. DMI still displays symbol ST01");

            /*
            Test Step 12
            Action: This step is to clear the symbol ‘ST01’ after verification of the previous step.Use the test script file 13_2_1_b.xml to send EVC-8 with,MMI_Q_TEXT_CRITERIA = 4MMI_I_TEXT = 1
            Expected Result: 
            */
            XML_13_2_1(msgType.typeb);

            /*
            Test Step 13
            Action: Use the test script file 13_2_1_a.xml to send EVC-8 with,MMI_Q_TEXT_CRITERIA = 0MMI_Q_TEXT = 260MMI_I_TEXT = 1Then, press at sub-area C9 (ST01 symbol)
            Expected Result: Verify the following information,The sub-area C9 can be acknowledged as sensitive areaThe flashing frame belonging to the acknowledgement is disappearedThe symbol ‘ST01’ is still displayed.When the driver carries out an acknowledgement, the DMI will send [MMI_DRIVER_MESSAGE_ACK (EVC-111)] for pressed and released event with [MMI_DRIVER_MESSAGE (EVC-8).MMI_I_TEXT] and [MMI_DRIVER_MESSAGE_ACK (EVC-111).MMI_Q_ACK]Note: For pressed and released event, the DMI will send variable [MMI_DRIVER_MESSAGE_ACK (EVC-111).MMI_Q_ACK] = 1 with  [MMI_DRIVER_MESSAGE_ACK (EVC-111).MMI_Q_BUTTON] = 1, and then send [MMI_DRIVER_MESSAGE_ACK (EVC-111).MMI_Q_ACK] = 1 with  [MMI_DRIVER_MESSAGE_ACK (EVC-111).MMI_Q_BUTTON] = 0
            Test Step Comment: (1) MMI_gen 1400 (partly: sub-area C8);                                      (2) MMI_gen 1382 (partly: MMI_gen4499 (partly: flashing frame));            (3) MMI_gen 1382 (partly:  MMI_gen 4499 (partly: symbol after acknowledgement is given));                             (4) MMI_gen 1382 (partly: MMI_gen146);
            */
            XML_13_2_1(msgType.typea);

            DmiActions.ShowInstruction(this, "Press in sub-area C9 (below ST01 symbol)");

            EVC111_MMIDriverMessageAck.MMI_I_TEXT = 1;
            EVC111_MMIDriverMessageAck.MMI_Q_ACK = MMI_Q_ACK.AcknowledgeYES;
            EVC111_MMIDriverMessageAck.MMI_Q_BUTTON = Variables.MMI_Q_BUTTON.Pressed;

            this.Wait_Realtime(2000);

            EVC111_MMIDriverMessageAck.MMI_I_TEXT = 1;
            EVC111_MMIDriverMessageAck.MMI_Q_ACK = MMI_Q_ACK.AcknowledgeYES;
            EVC111_MMIDriverMessageAck.MMI_Q_BUTTON = Variables.MMI_Q_BUTTON.Released;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Sub-area C9 can be acknowledged as sensitive area." + Environment.NewLine +
                                "2. The yellow flashing frame surrounding the ST01 symbol is removed." + Environment.NewLine +
                                "3. DMI still displays symbol ST01");

            /*
            Test Step 14
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
        #region Send_XML_13_2_1_DMI_Test_Specification
        enum msgType
        {
            typea,
            typeb
        }

        private void XML_13_2_1(msgType type)
        {
            switch (type)
            {

                case msgType.typea:
                    // Step 2/1
                    EVC8_MMIDriverMessage.MMI_Q_TEXT = 260;
                    EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 0;

                    EVC8_MMIDriverMessage.Send();

                    break;
                case msgType.typeb:

                    EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
                    EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 4;

                    EVC8_MMIDriverMessage.Send();

                    break;

            }
        }
        #endregion
    }
}
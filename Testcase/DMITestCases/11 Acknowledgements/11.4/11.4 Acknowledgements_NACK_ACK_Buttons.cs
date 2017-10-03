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
    /// 11.$ Acknowledgements: NACK/ACK Buttons
    /// TC-ID: 6.4
    /// 
    /// This test case verifies the display information of acknowledgement buttons refer to configured and the operation of safe up-type in each acknowledgement button
    /// 
    /// Tested Requirements:
    /// MMI_gen 4507; MMI_gen 4470 (partly: MMI_gen 4381, MMI_gen 4382, MMI_gen 11387 (partly: send events of Pressed independently to ETCS), MMI_gen 11907 (partly: EVC-101, timestamp)); MMI_gen 4374; MMI_gen 4375; MMI_gen 4468; MMI_gen 4499 (partly: text message step back as non-acknowledgementable); MMI_gen 3375; MMI_gen 3200 (partly: NACK button, ACK button, Text acknowledgement); MMI_gen 3374 (partly: NACK button, ACK button, Text acknowledgement); MMI_gen 4256 (partly: Click sound)
    /// 
    /// Scenario:
    /// 1.Use the test script file to send EVC-8. Then, verify the display of acknowledgement message with buttons.
    /// 2.Verify the safe up-type button of each acknowledgement button.
    /// 3.Use the test script file to send EVC-8. Then, verify that configured colour is not effect when there is no acknowledgement button display.
    /// 4.Verify the safe up-type button of text acknowledgement.Then, verify the display of text message. 

    /// 
    /// Used files:
    /// 6_4_a.xml, 6_4_b.xml
    /// </summary>
    public class TC_ID_6_4_Acknowledgements : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:

            // Call the TestCaseBase PreExecution
            base.PreExecution();

            // Change the background-colour of the style below in gen_buttonStyle_type_4.xml to be #BF0002, STYLE class="normal", STYLE class="flash"

            // System is powered onCabin is activatedPerform SoM until level 1 is selected and confirmedMain window is closed.
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
            // Testcase entrypoint

            /*
            Test Step 1
            Action: Use the test script file 6_4_a.xml to send EVC-8 with MMI_Q_TEXT = 527MMI_Q_TEXT_CRITERIA = 2MMI_I_TEXT = 1MMI_Q_TEXT_CLASS = 1
            Expected Result: DMI displays the text message ‘Brake test aborted, perform new Test?’ with an acknowledgement option in sub-area E5-E9.Verify the following information,(1)  The ‘ACK’ button is labelled with text ‘ACK in yellow colour.(2)  The 'NACK' button is labelled with text 'NACK' in yellow colour.(3)   The background colour of both button 'ACK' and 'NACK' are red.
            
            Test Step Comment: (1) MMI_gen 4468 (partly: ‘ACK’ in yellow);(2) MMI_gen 4507 (partly: 'NACK' in yellow colour);(3) MMI_gen 4468 (partly: configured background colour); MMI_gen 4507 (partly: configured background colour);

            */
            XML.XML_6_4_a.Send(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the message ‘Brake test aborted, perform new Test?’ with acknowledgement buttons in sub-areas E5-E9." + Environment.NewLine +
                                "2. The ‘ACK’ button has text ‘ACK’ in yellow on a red background" + Environment.NewLine +
                                "3. The ‘NACK’ button has text ‘NACK’ in yellow on a red background");

            /*
            Test Step 2
            Action: Press and hold 'NACK' button.
            Expected Result: Verify the following information,(1)   The sound ‘Click’ played once.(2)   The 'NACK' button is shown as pressed state, the border of button is removed.(3)   Use the log file to confirm that DMI send EVC-111 with variable MMI_Q_BUTTON = 1 (Pressed) and MMI_T_BUTTONEVENT is not blank.

            Test Step Comment: (1) MMI_gen 4470 (partly: 'NACK', MMI_gen 4381 (partly: the sound for Up-Type button)); MMI_gen 4256 (partly: Click sound);(2) MMI_gen 4470 (partly: 'NACK', MMI_gen 4381 (partly: change to state ‘Pressed’ as long as remain actuated));(3) MMI_gen 4470 (partly: MMI_gen 11387 (partly: send events of Pressed independently to ETCS), MMI_gen 11907 (partly: EVC-111, timestamp)); MMI_gen 3375; MMI_gen 3200 (partly: NACK button, pressed);
            */
            DmiActions.ShowInstruction(this, "Press and hold the ‘NACK’ button");
            
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Click’ sound is played once." + Environment.NewLine +
                                "2. The ‘NACK’ button is displayed pressed, with the border removed.");

            /*
            Test Step 3
            Action: Slide out 'NACK' button.
            Expected Result: The border of the button is shown (state ‘Enabled’) without a sound.

            Test Step Comment: MMI_gen 4470 (partly: 'NACK', MMI_gen 4382 (partly: state ‘Enabled’ when slide out with force applied, no sound))); MMI_gen 4374;
            */
            DmiActions.ShowInstruction(this, "Whilst keeping the ‘NACK’ button pressed, drag outside the area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The button is displayed enabled." + Environment.NewLine +
                                "2. No sound is played.");

            /*
            Test Step 4
            Action: Slide back into 'NACK' button.
            Expected Result: The button is back to state ‘Pressed’ without a sound.
            
            Test Step Comment: MMMI_gen 4470 (partly: 'NACK', MMI_gen 4382 (partly: state ‘Pressed’ when slide back, no sound))); MMI_gen 4375;
            */
            DmiActions.ShowInstruction(this, "Whilst keeping ‘NACK’ button pressed, drag back inside the area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The button border is displayed pressed." + Environment.NewLine +
                                "2. No sound is played.");

            /*
            Test Step 5
            Action: Release the 'NACK' button.	
            Expected Result: Verify the following information,(1)    The Ack-buttons with yellow flashing frame and text message is disappear.(2)   Use the log file to confirm that DMI send EVC-111 with variable MMI_Q_BUTTON = 0 (Released) and MMI_T_BUTTONEVENT is not blank.	(1) MMI_gen 4470 (partly: 'NACK', MMI_gen 4381 (partly: exit state ‘Pressed’, execute function associated to the button)));
            
            Test Step Comment: (2) MMI_gen 4470 (partly: MMI_gen 11387 (partly: send events of Pressed independently to ETCS), MMI_gen 11907 (partly: EVC-111, timestamp)); MMI_gen 3375; MMI_gen 3200 (partly: NACK button, released);
            */
            DmiActions.ShowInstruction(this, "Release the ‘NACK’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI stops displaying the acknowledgement buttons.");

            /*
            Test Step 6
            Action: Use the test script file 6_4_a.xml to send EVC-8.Then, perform action step 2-5 for 'ACK' button.            
            Expected Result: See the expected results of Step 2 – Step 5.	

            Test Step Comment: MMI_gen 4470 (partly: 'ACK'); MMI_gen 3375; MMI_gen 3200 (partly: ACK button);
            */
            XML.XML_6_4_a.Send(this);
            
            // Repeat Step 2
            DmiActions.ShowInstruction(this, "Press and hold the ‘ACK’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Click’ sound is played once." + Environment.NewLine +
                                "2. The ‘ACK’ button is displayed pressed, with the border removed.");

            // Repeat Step 3
            DmiActions.ShowInstruction(this, "Whilst keeping the ‘ACK’ button pressed, drag outside the area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The button is displayed enabled." + Environment.NewLine +
                                "2. No sound is played.");

            // Repeat Step 4
            DmiActions.ShowInstruction(this, "Whilst keeping ‘NACK’ button pressed, drag back inside the area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The button border is displayed pressed." + Environment.NewLine +
                                "2. No sound is played.");

            // Repeat Step 5
            DmiActions.ShowInstruction(this, "Release the ‘NACK’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI stops displaying the acknowledgement buttons.");

            /*
            Test Step 7
            Action: Use the test script file 6_4_b.xml to send EVC-8 with,MMI_I_TEXT = 1MMI_Q_TEXT = 527MMI_Q_TEXT_CRITERIA = 0	MMI_Q_TEXT_CLASS = 1	
            Expected Result: Verify the following information,(1)   DMI displays the text message ‘Brake test aborted, perform new Test?’ with a yellow flashing frame around sub-area E5-E9.	

            Test Step Comment: (1) MMI_gen 4468 (partly: Note); MMI_gen 3374 (partly: Text acknowledgement, visible, not faulty);
            */
            XML.XML_6_4_b.Send(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displaying the message ‘Brake test aborted. Perform new Test?’ with a flashing yellow frame around sub-areas E5-E9.");

            /*
            Test Step 8
            Action: Press and hold sub-area E5.	
            Expected Result: Verify the following information,(1)   The sound ‘Click’ played once.(2)   The sub-area E5 is shown as pressed state, the border of button is removed.(3)   Use the log file to confirm that DMI send EVC-101 with variable MMI_Q_BUTTON = 1 (Pressed) and MMI_T_BUTTONEVENT is not blank.	(1) MMI_gen 3200 (partly: Text acknowledgement, MMI_gen 4381 (partly: the sound for Up-Type button));
            
            Test Step Comment: (2) MMI_gen 3200 (partly: Text acknowledgement, MMI_gen 4381 (partly: change to state ‘Pressed’ as long as remain actuated));(3) MMI_gen 3200 (partly: Text acknowledgement, pressed, MMI_gen 11387 (partly: send events of Pressed independently to ETCS), MMI_gen 11907 (partly: EVC-101, timestamp)); (Continue from step 7)Send EVC-8 with, MMI_Q_TEXT = 269MMI_Q_TEXT_CRITERIA = 1MMI_I_TEXT = 8The display information on DMI still not change, ST01 symbol is displayed on sub-area C9
            */
            DmiActions.ShowInstruction(this, "Press and hold sub-area E5");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Click’ sound is played once." + Environment.NewLine +
                                "2. The sub-area E5 is displayed pressed with no border.");

            /*
            Test Step 9
            Action: Slide out sub-area E5.	
            Expected Result: The border of the sub-area E5 is shown (state ‘Enabled’) without a sound.
            
            Test Step Comment: MMI_gen 3200 (partly: Text acknowledgement, MMI_gen 4382 (partly: state ‘Enabled’ when slide out with force applied, no sound))); 
            */
            DmiActions.ShowInstruction(this, "Whilst keeping sub-area E5 pressed, drag outside the area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The border of sub-area E5 is displayed enabled" + Environment.NewLine +
                                "2. No sound is played.");

            /*
            Test Step 10
            Action: Slide back into sub-area E5.	
            Expected Result: The sub-area E5 is back to state ‘Pressed’ without a sound. 
            
            Test Step Comment: MMMI_gen 3200 (partly: Text acknowledgement, MMI_gen 4382 (partly: state ‘Pressed’ when slide back, no sound)));
            */
            DmiActions.ShowInstruction(this, "Whilst keeping sub-area E5 pressed, drag back inside the area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Sub-area E5 is displayed pressed." + Environment.NewLine +
                                "2. No sound is played.");

            /*
            Test Step 11
            Action: Release the pressed area.	
            Expected Result: Verify the following information,(1)   The yellow flashing frame is removed, DMI still display text message 'Brake test aborted, perform new Test?' in sub-area E5.(2)   Use the log file to confirm that DMI send EVC-101 with variable MMI_Q_BUTTON = 0 (Released) and MMI_T_BUTTONEVENT is not blank.	(1) MMI_gen 4499 (partly: text message step back as non-acknowledgementable);
            
            Test Step Comment: (2) MMI_gen 3200 (partly: Text acknowledgement, released, MMI_gen 11387 (partly: send events of Released independently to ETCS), MMI_gen 11907 (partly: EVC-101, timestamp));        
            */
            DmiActions.ShowInstruction(this, "Release sub-area E5");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI still displays the message ‘Brake test aborted. Perform new Test?’" + Environment.NewLine +
                                "2. The flashing yellow frame around sub-areas E5-E9 is removed.");

            /*
            Test Step 12
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
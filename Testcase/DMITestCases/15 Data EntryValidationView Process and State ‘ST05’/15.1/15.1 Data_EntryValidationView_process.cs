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
    /// 15.1 Data Entry/Validation/View process
    /// TC-ID: 10.1
    /// 
    /// This test case verifies the buttons state in menu windows.
    /// 
    /// Tested Requirements:
    /// MMI_gen 1316 (partly: disabled state in Table 23 (Active state, Idle state), enable state in Table 23 (Active state, Idle state)); MMI_gen 5647;
    /// 
    /// Scenario:
    /// 1.The ‘Settings’ menu window is displayed.
    /// 2.Use the test script files to send packets in order to verify button state in a Setting menu window. 
    /// 3.Active the cabin and perform SoM until select and confirm Level 1.
    /// 4.Use the test script files to send packets in order to verify button state in a Main menu window. 
    /// 5.Open the ‘Override’ window and use the test script files to send packets in order to verify buttons state. 
    /// 6.Open the ‘Special’ window and use the test script files to send packets in order to verify buttons state. 
    /// 7.Open the ‘Setting’ window and use the test script files to send packets in order to verify buttons state.
    /// 8.Reactive the cabin and perform SoM until select and confirm Level 2.
    /// 9.Open the ‘RBC contact’ window and use the test script files to send packets in order to verify buttons state.
    /// 
    /// Used files:
    /// 10_1_a.xml, 10_1_b.xml, 10_1.utt
    /// </summary>
    public class TC_ID_10_1_Data_EntryValidationView_process : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:

            // Call the TestCaseBase PreExecution
            base.PreExecution();
            // Test system is powered onCabin is inactive
            DmiActions.Complete_SoM_L1_SB(this);
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in SB mode
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in SB mode.");

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint
            
            /*
            Test Step 1
            Action: Press ‘Settings menu’ button
            Expected Result: Settings menu window is displayed
            */
            DmiActions.ShowInstruction(this, "Press the ‘Settings menu’ button");

            // Can you tell this?
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Settings menu window.");

            /*
            Test Step 2
            Action: Use the test script file 10_1_a.xml to disable and enable button via EVC-30 withPacket 1 (disable all button)all bit of variable MMI_Q_REQUEST_ENABLE = ‘0’ and MMI_NID_WINDOW = 255Packet 2 (enable all button) Send EVC-30 with all bit of variable MMI_Q_REQUEST_ENABLE = ‘1’ and MMI_NID_WINDOW = 255
            Expected Result: Verify the following information,(1)   All buttons in Settings menu window are disabled, except ‘Lock screen for cleaning’.10 seconds later(2)   All buttons in Settings menu window are enabled, except ‘Lock screen for cleaning’.Note: Button ‘Lock screen for cleaning’ is not controlled by ETCS onboard
            Test Step Comment: (1) MMI_gen 1316 (partly: disabled state in Table 23, Idle state);(2) MMI_gen 1316 (partly: enable state in Table 23, Idle state); 
            */
            XML.XML_10_1_a.Send(this, "Settings menu");

            /*
            Test Step 3
            Action: Perform the following procedure,Close the Setting windowCabin A is activated.Perform SoM until select and confirm Level 1
            Expected Result: Main menu window is displayed
            */
            DmiActions.ShowInstruction(this, "Close the Setting window. Activate Cabin A. Perform SoM until select and confirm Level 1.");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Main menu window.");

            /*
            Test Step 4
            Action: Use the test script file 10_1_b.xml to disable and enable button via EVC-30 withPacket 1 (disable all button)all bit of variable MMI_Q_REQUEST_ENABLE = ‘0’ and MMI_NID_WINDOW = 1Packet 2 (enable all button) Send EVC-30 with all bit of variable MMI_Q_REQUEST_ENABLE = ‘1’ and MMI_NID_WINDOW = 1
            Expected Result: Verify the following information,(1)   All buttons in Main menu window are disabled.10 seconds later(2)   All buttons in Main menu window are enabled
            Test Step Comment: (1) MMI_gen 1316 (partly: disabled state in Table 23, Active state);(2) MMI_gen 1316 (partly: enable state in Table 23, Active state); 
            */
            XML.XML_10_1_b.Send(this);

            /*
            Test Step 5
            Action: Close the Main window
            Expected Result: (1)   The Default window is displayed
            Test Step Comment: (1) MMI_gen 5647
            */
            DmiActions.ShowInstruction(this, "Close the Main window");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Default window.");

            /*
            Test Step 6
            Action: Perform the following procedure;Press the Override button Repeat action step 2 with Override window
            Expected Result: Override menu window is displayed.Verify the following information,(1)   All buttons in Override menu window are disabled.10 seconds later(2)   All buttons in Override menu window are enabled
            Test Step Comment: (1) MMI_gen 1316 (partly: disabled state in Table 23, Active state);(2) MMI_gen 1316 (partly: enable state in Table 23, Active state); 
            */
            DmiActions.ShowInstruction(this, "Press the ‘Override’ button");
            XML.XML_10_1_a.Send(this, "Override menu", false);

            /*
            Test Step 7
            Action: Close the Override menu window
            Expected Result: (1)   The Default window is displayed
            Test Step Comment: (1) MMI_gen 5647
            */
            DmiActions.ShowInstruction(this, "Close the Override menu window");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Default window.");

            /*
            Test Step 8
            Action: Perform the following procedure;Press ‘Spec’ buttonRepeat action step 2 with Special window
            Expected Result: Special menu window is displayed.Verify the following information,(1)   All buttons in Special menu window are disabled.10 seconds later(2)   All buttons in Special menu window are enabled
            Test Step Comment: (1) MMI_gen 1316 (partly: disabled state in Table 23, Active state);(2) MMI_gen 1316 (partly: enable state in Table 23, Active state); 
            */
            DmiActions.ShowInstruction(this, "Press the ‘Spec’ button");
            XML.XML_10_1_a.Send(this, "Special menu", false);

            /*
            Test Step 9
            Action: Close the Special menu window
            Expected Result: (1)   The Default window is displayed
            Test Step Comment: (1) MMI_gen 5647
            */
            DmiActions.ShowInstruction(this, "Close the Special menu window");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Default window.");

            /*
            Test Step 10
            Action: Perform the following procedure;Press ‘Settings’ buttonRepeat action step 2 with Settings window
            Expected Result: Settings menu window is displayed.Verify the following information,(1)   All buttons in Settings menu window are disabled, except ‘Lock screen for cleaning’.10 seconds later(2)   All buttons in Settings menu window are enabled, except ‘Lock screen for cleaning’.Note: Button ‘Lock screen for cleaning’ is not controlled by ETCS onboard
            Test Step Comment: (1) MMI_gen 1316 (partly: disabled state in Table 23, Active state);(2) MMI_gen 1316 (partly: enable state in Table 23, Active state); 
            */
            DmiActions.ShowInstruction(this, "Press the ‘Settings’ button");
            XML.XML_10_1_a.Send(this, "Settings menu");

            /*
            Test Step 11
            Action: Perform the following procedure;Press ‘Brake’ buttonRepeat action step 2 with Brake window
            Expected Result: Brake menu window is displayed.Verify the following information,(1)   All buttons in Brake menu window are disabled.10 seconds later(2)   All buttons in Brake menu window are enabled
            Test Step Comment: (1) MMI_gen 1316 (partly: disabled state in Table 23, Active state);(2) MMI_gen 1316 (partly: enable state in Table 23, Active state); 
            */
            DmiActions.ShowInstruction(this, "Press the ‘Brake’ button");
            XML.XML_10_1_a.Send(this, "Brake menu", false);

            /*
            Test Step 12
            Action: Perform the following procedure;Press ‘Test’ buttonRepeat action step 2 with Brake Test window
            Expected Result: Brake Test menu window is displayed.Verify the following information,(1)   All buttons in Brake Test menu window are disabled.10 seconds later(2)   All buttons in Brake Test menu window are enabled
            Test Step Comment: (1) MMI_gen 1316 (partly: disabled state in Table 23, Active state);(2) MMI_gen 1316 (partly: enable state in Table 23, Active state); 
            */
            DmiActions.ShowInstruction(this, "Press the ‘Test’ button");
            XML.XML_10_1_a.Send(this, "Brake Test menu", false);

            /*
            Test Step 13
            Action: Close the Brake Test window and Brake window
            Expected Result: (1)   The Settings window is displayed
            Test Step Comment: (1) MMI_gen 5647
            */
            DmiActions.ShowInstruction(this, "Close the Brake Test and Brake window");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Settings window.");

            /*
            Test Step 14
            Action: Close the Settings menu window
            Expected Result: (1)   The Default window is displayed
            Test Step Comment: (1) MMI_gen 5647
            */
            DmiActions.ShowInstruction(this, "Close the Settings window");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Default window.");

            /*
            Test Step 15
            Action: Perform the following procedure,Press the Main button Press the Level buttonSelect and confirm Level 2Repeat action step 2 with RBC contact window
            Expected Result: RBC contact window is displayedVerify the following information,(1)   All buttons in RBC contact window are disabled.10 seconds later(2)   All buttons in RBC contact window are enabled
            Test Step Comment: (1) MMI_gen 1316 (partly: disabled state in Table 23, Active state);(2) MMI_gen 1316 (partly: enable state in Table 23, Active state); 
            */
            DmiActions.ShowInstruction(this, "Press the Main button. Press the Level button. Select and confirm Level 2");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the RBC contact window.");

            XML.XML_10_1_a.Send(this, "RBC Contact", false);

            /*
            Test Step 16
            Action: Close the RBC contact window
            Expected Result: (1)   The Main menu window is displayed
            Test Step Comment: (1) MMI_gen 5647
            */
            DmiActions.ShowInstruction(this, "Close the RBC contact window");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Main menu window.");

            /*
            Test Step 17
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
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
    /// 27.27.2 ‘Set VBC’ Validation Window
    /// TC-ID: 22.27.2
    /// 
    /// This test case verifies the general appearances of Set VBC validation window based on chapter 6.5.6.3 of requirement specification with packet information sending and receiving.
    /// 
    /// Tested Requirements:
    /// MMI_gen 9925; MMI_gen 9926 (partly: MMI_gen 5724, MMI_gen 5720, MMI_gen 2519 (partly: Set VBC validation window); MMI_gen 1426 (partly: Set VBC validation window)); MMI_gen 9927; MMI_gen 8564; MMI_gen 8565; MMI_gen 9928; MMI_gen 8563 (partly: MMI_gen 5215 (partly: Close button, Window title, Input field, No button, Yes button), MMI_gen 5216, MMI_gen 7943, window on half grid array, MMI_gen 5214, MMI_gen 5006,  MMI_gen 5484, MMI_gen 5263 (partly: MMI_gen 4696, MMI_gen 4697, MMI_gen 4698, MMI_gen 4700 (partly: data validation process), MMI_gen 4702 (partly: right aligned), MMI_gen 4704 (partly: left aligned), partly: MMI_gen 4701), MMI_gen 5303); MMI_gen 4392 (partly: [Close] NA11, returning to the parent window, [Enter], touch screen); MMI_gen 4350; MMI_gen 4351; MMI_gen 4353; MMI_gen 9390 (partly: Set VBC Validation window); MMI_gen 4377 (partly: shown);
    /// 
    /// Scenario:
    /// Enter and confirm all data in Set VBC window. Then, verify the display information and received packet data EVC-28.Press ‘No’ button and verify that the value of an input field is changed refer to pressed button.Confirm entered data by pressing an input field. Then, verify that DMI closes Set VBC Validation window and opens Set VBC window with sending out packet EVC-101.Open Set VBC window. Then, enter and confirm all data.Press ‘Close’ button. Then, verify that DMI close Set VBC Validation window and open Set VBC window with sending out packet EVC-101.Open Set VBC window. Then, enter and confirm all data.Press ‘Yes’ button and verify that the value of an input field is changed refer to pressed button.Confirm entered data by pressing an input field. Then, verify that DMI closes Set VBC Validation window and open Set VBC window with sending out packet EVC-128.Simulate loss-communication between ETCS and DMI. Then, re-establish communication and verify the state of buttons in Set VBC Validation window.
    /// 
    /// Used files:
    /// N/A
    /// </summary>
    public class TC_ID_22_27_2_Set_VBC_Validation_Window : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // The VBC code is not stored on ETCS. (See the information in the “Data View” menu)Settings window is opened.Set VBC window is opened.

            // Call the TestCaseBase PreExecution
            base.PreExecution();

            // System is powered ON.Cabin is activated.Perform SoM until level 1 is selected and confirmed.
            DmiActions.Complete_SoM_L1_SR(this);
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in SB mode, level 1
            // The VBC code “65536” is stored ETCS onboard.

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint

            /*
            Test Step 1
            Action: Perform the following procedure,Enter and confirm value ‘65536’ at input field.Press ‘Yes’ button
            Expected Result: Verify the following information,Use the log file to confirm that DMI received the packet MMI_ECHOED_SET_VBC_DATA (EVC-28). DMI displays Set VBC Validation window.The following objects are displayed in Set VBC Validation window. Enabled Close button (NA11)Window TitleInput fieldYes buttonNo buttonWindow TitleThe window title is ‘Validate set VBC’.The window title is right aligned.LayerThe window is displayed in main area A/B/C/D/E/F/G.All areas of Data validation window are displayed in Layer 0.Input fieldThe window is contains a single input field which has only data area.The value of input field is empty.KeyboardThe displayed keyboard type is dedicated keyboard which contain only ‘Yes’ and ‘No’ button.The key #7 is No button.The key #8 is Yes button.Echo TextEcho Text is composed of a Label part and Data part.The Label of echo text is right aligned.The Data part of echo text is left aligned.The order of echo texts are same as of the Set VBC window.The data part of echo texts are display the data value same as of the Set VBC window.The echo texts are located in Main area A,B,C and E.The echo texts colour is white.Use the log file to confirm that the following variable in packet EVC-28 is same as entered data and display in the data part of echo text,MMI_M_VBC_CODE = entered VBC codeGeneral property of windowThe Set VBC Validation window is presented with objects, text messages and buttons which is the one of several levels and allocated to areas of DMI. All objects, text messages and buttons are presented within the same layer.The Default window is not displayed and covered the current window
            Test Step Comment: (1) MMI_gen 9925 (partly: EVC-28);(2) MMI_gen 9925 (partly: open Set VBC Validation window, touch screen);(3) MMI_gen 8563 (partly: MMI_gen 5215 (partly: Close button, Window title, Input field, No button, Yes button)); MMI_gen 4392 (partly: [Close] NA11);(4) MMI_gen 8564;(5) MMI_gen 8563 (partly: MMI_gen 5216);(6) MMI_gen 8563 (partly: MMI_gen 7943);(7) MMI_gen 8563 (partly: MMI_gen 5303);(8) MMI_gen 8563 (partly: MMI_gen 5214 (partly: single input field));          (9) MMI_gen 8563 (partly: MMI_gen 5484 (partly: empty)); (10) MMI_gen 8563 (partly: MMI_gen 5214 (partly: dedicated keyboard, MMI_gen 5006), MMI_gen 5006);(11) MMI_gen 8563 (partly: MMI_gen 5263 (partly: MMI_gen 4696));(12) MMI_gen 8563 (partly: MMI_gen 5263 (partly: MMI_gen 4702 (partly: right aligned)));(13) MMI_gen 8563 (partly: MMI_gen 5263 (partly: MMI_gen 4704 (partly: left aligned)));(14) MMI_gen 8563 (partly: MMI_gen 5263 (partly: MMI_gen 4701 (partly: same order), MMI_gen 4697));(15) MMI_gen 8563 (partly: MMI_gen 5263 (partly: MMI_gen 4698));(16) MMI_gen 8563 (partly: MMI_gen 5263 (partly: MMI_gen 4701 (partly: Main area A, B, C and E));(17) MMI_gen 8563 (partly: MMI_gen 5263 (partly: MMI_gen 4700 (partly: data validation process)));(18) MMI_gen 9928; MMI_gen 8565;(19) MMI_gen 4350;(20) MMI_gen 4351;(21) MMI_gen 4353;
            */
            
            DmiActions.ShowInstruction(this, @"Press the ‘Settings’ button, then press the ‘Set VBC’ button.");

            EVC18_MMISetVBC.MMI_M_BUTTONS = Variables.MMI_M_BUTTONS_VBC.BTN_YES_DATA_ENTRY_COMPLETE;
            EVC18_MMISetVBC.MMI_N_VBC = 0;
            EVC18_MMISetVBC.Send();

            DmiActions.ShowInstruction(this, @"Enter and confirm the value ‘65536’, then press the ‘Yes’ button");

            EVC28_MMIEchoedSetVBCData.MMI_M_VBC_CODE_ = 65536;
            EVC28_MMIEchoedSetVBCData.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Set VBC validation window, with the title ‘Validate set VBC’, right-aligned." + Environment.NewLine +
                                "2. The window contains an enabled Close button (NA11), a data input field and ‘Yes’ and ‘No’ buttons." + Environment.NewLine +
                                "3. The window is displayed in areas A, B, C, D, E, F and G." + Environment.NewLine +
                                "4. All areas of the window are in Layer 0." + Environment.NewLine +
                                "5. The (single) data input field only has a data area, which has a blank value." + Environment.NewLine +
                                "6. The data input field has a dedicated keypad containing <Yes> (#8) and <No> (#7) keys." + Environment.NewLine +
                                "7. An echo text with Label, with right-aligned text, and Data, with left-aligned text, parts is displayed." + Environment.NewLine +
                                "8. The echo text displays the value, in white, as input in the Set VBC window." + Environment.NewLine +
                                "9. The echo text is in area A, B, C and E." + Environment.NewLine +
                                "10. Objects, text messages and buttons can be displayed in several levels.Within a level they are allocated to areas." + Environment.NewLine +
                                "11. All objects, text messages and buttons are displayed in the same layer." + Environment.NewLine +
                                "12. The Default window is not displayed covering the current window.");

            /*
            Test Step 2
            Action: Press ‘No’ button
            Expected Result: The value of input field is changed refer to selected button
            Test Step Comment: MMI_gen 8563 (partly: MMI_gen 5484 (partly: filled ‘No’));
            */
            DmiActions.ShowInstruction(this, @"Press the ‘No’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The data input field displays ‘No’.");

            /*
            Test Step 3
            Action: Press and hold an input field
            Expected Result: Verify the following information,(1)    The state of an input field is changed to ‘Pressed’, the border of button is removed
            Test Step Comment: (1) MMI_gen 9390 (partly: Set VBC Validation window);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press and hold the data input field");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The data input field is displayed pressed, without a border.");

            /*
            Test Step 4
            Action: Slide out an input field
            Expected Result: Verify the following information,(1)     The state of an input field is changed to ‘Enabled, the border of button is shown without a sound
            Test Step Comment: (1) MMI_gen 9390 (partly: Set VBC Validation window);
            */
            DmiActions.ShowInstruction(this, @"Whilst keeping the data input field pressed, drag outside the area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The data input field is displayed enabled, with a border." + Environment.NewLine +
                                "2. No sound is played.");

            /*
            Test Step 5
            Action: Slide back into an input field
            Expected Result: Verify the following information,(1)     The state of an input field is changed to ‘Pressed’, the border of button is removed
            Test Step Comment: (1) MMI_gen 9390 (partly: Set VBC Validation window);
            */
            DmiActions.ShowInstruction(this, @"Whilst keeping the data input field pressed, drag back inside the area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The data input field is displayed pressed, without a border.");

            /*
            Test Step 6
            Action: Released the pressed area
            Expected Result: Verify the following information,DMI displays Set VBC window.Use the log file to confirm that DMI sends out the packet [MMI_DRIVER_REQUEST (EVC-101)] with variable [MMI_DRIVER_REQUEST (EVC-101).MMI_M_REQUEST] = 25 (Exit Set VBC)
            Test Step Comment: (1) MMI_gen 9926 (partly: No button, open Set VBC window);(2) MMI_gen 9926 (partly: EVC-101, MMI_gen 5724); MMI_gen 4392 (partly: [Enter], touch screen); MMI_gen 9390 (partly: Set VBC Validation window);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, "Release the data input field");

            EVC101_MMIDriverRequest.CheckMRequestReleased = Variables.MMI_M_REQUEST.ExitSetVBC;
            
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Set VBC window.");

            /*
            Test Step 7
            Action: Perform the following procedure,Enter and confirm value ‘65536’ at input field.Press ‘Yes’ button
            Expected Result: DMI displays Set VBC Validation window
            */
            DmiActions.ShowInstruction(this, @"Enter and confirm the value ‘65536’, then press the ‘Yes’ button");

            EVC28_MMIEchoedSetVBCData.MMI_M_VBC_CODE_ = 65536;
            EVC28_MMIEchoedSetVBCData.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Set VBC validation window.");

            /*
            Test Step 8
            Action: Press ‘Close’ button
            Expected Result: Verify the following information,DMI displays Settings window.Use the log file to confirm that DMI sends out the packet [MMI_DRIVER_REQUEST (EVC-101)] with variable [MMI_DRIVER_REQUEST (EVC-101).MMI_M_REQUEST] = 25 (Exit Set VBC)
            Test Step Comment: (1) MMI_gen 9926 (partly: Close button, open Settings window); MMI_gen 4392 (partly: returning to the parent window);(2) MMI_gen 9926 (partly: EVC-101); 
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button, then press the ‘Close’ button again");

            EVC18_MMISetVBC.MMI_Q_DATA_CHECK = Variables.Q_DATA_CHECK.All_checks_passed;
            EVC18_MMISetVBC.MMI_M_BUTTONS = Variables.MMI_M_BUTTONS_VBC.BTN_SETTINGS;
            EVC18_MMISetVBC.Send();

            /*
            Test Step 9
            Action: Perform the following procedure,Press ‘Set VBC’ button.Enter and confirm value ‘65536’ at input field.Press ‘Yes’ button
            Expected Result: DMI displays Set VBC Validation window
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Set VBC’ button.");

            EVC18_MMISetVBC.MMI_M_BUTTONS = Variables.MMI_M_BUTTONS_VBC.BTN_YES_DATA_ENTRY_COMPLETE;
            EVC18_MMISetVBC.MMI_N_VBC = 0;
            EVC18_MMISetVBC.Send();

            DmiActions.ShowInstruction(this, @"Enter and confirm the value ‘65536’, then press the ‘Yes’ button");

            EVC18_MMISetVBC.MMI_M_BUTTONS = Variables.MMI_M_BUTTONS_VBC.BTN_YES_DATA_ENTRY_COMPLETE;
            EVC18_MMISetVBC.MMI_N_VBC = 1;
            EVC18_MMISetVBC.MMI_Q_DATA_CHECK = Variables.Q_DATA_CHECK.All_checks_passed;
            EVC18_MMISetVBC.ECHO_TEXT = "";
            EVC18_MMISetVBC.Send();
            EVC28_MMIEchoedSetVBCData.MMI_M_VBC_CODE_ = 65536;
            EVC28_MMIEchoedSetVBCData.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Set VBC validation window.");

            /*
            Test Step 10
            Action: Press ‘Yes’ button
            Expected Result: The value of input field is changed refer to selected button
            Test Step Comment: MMI_gen 8563 (partly: MMI_gen 5484 (partly: filled ‘Yes’));
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press the ‘Yes’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The data input field displays ‘Yes’ .");

            /*
            Test Step 11
            Action: Confirm entered data by pressing an input field
            Expected Result: DMI displays Settings window.Verify the following information,The Set VBC validation is closed.Use the log file to confirm that DMI sends out the packet [MMI_CONFIRMED_SET_VBC (EVC-128)] with variable based on confirmed data
            Test Step Comment: (1) MMI_gen 9926 (partly: MMI_gen 5720 (partly: closed));(2) MMI_gen 9927; MMI_gen 9926 (partly: MMI_gen 5720 (partly: ConfirmedData-Packet));
            */
            DmiActions.ShowInstruction(this, "Confirm the  value by pressing in the data input field");

            EVC128_MMIConfirmedSetVBC.Check_VBC_Code = 65536;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI closes the Set VBC validation window.");

            /*
            Test Step 12
            Action: Perform the following procedure,Press ‘Set VBC’ button.Enter and confirm value ‘65536’ at input field.Press ‘Yes’ button.Then, simulate loss-communication between ETCS onboard and DMI
            Expected Result: DMI displays Default window with the message “ATP Down Alarm” and sound alarm
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Set VBC’ button.");

            EVC18_MMISetVBC.MMI_M_BUTTONS = Variables.MMI_M_BUTTONS_VBC.BTN_YES_DATA_ENTRY_COMPLETE;
            EVC18_MMISetVBC.MMI_N_VBC = 1;
            EVC18_MMISetVBC.MMI_Q_DATA_CHECK = Variables.Q_DATA_CHECK.All_checks_passed;
            EVC18_MMISetVBC.ECHO_TEXT = "";
            EVC18_MMISetVBC.Send();

            DmiActions.ShowInstruction(this, @"Enter and confirm the value ‘65536’, then press the ‘Yes’ button");

            EVC28_MMIEchoedSetVBCData.MMI_M_VBC_CODE_ = 65536;
            EVC28_MMIEchoedSetVBCData.Send();

            DmiActions.Simulate_communication_loss_EVC_DMI(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the message ‘ATP Down Alarm’." + Environment.NewLine +
                                "2. The ‘Alarm’ sound is played.");

            /*
            Test Step 13
            Action: Re-establish communication between ETCS onboard and DMI
            Expected Result: Verify the following informaiton,All buttons except ‘No’ button are disabled.The state of ‘No’ button is enabled.The disabled buttons are shown as a button is state ‘Disabled‘ with text label in dark-grey
            Test Step Comment: (1) MMI_gen 9926 ( partly: MMI_gen 2519 (partly: Set VBC Validation window, All Request buttons except negative validations));(2) MMI_gen 9926 (partly: MMI_gen 2519 (partly: Set VBC Validation window, All negative validations));(3) MMI_gen 9926 ( partly: MMI_gen 1426 (partly: Set VBC Validation window)); MMI_gen 4377 (partly: shown);
            */
            // Call generic Action Method
            DmiActions.Re_establish_communication_EVC_DMI(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. All buttons, except the ‘No’ button are disabled." + Environment.NewLine +
                                "2. The disabled buttons are displayed with labels in Dark-grey." + Environment.NewLine +
                                "3. The ‘No’ button is enabled.");

            /*
            Test Step 14
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
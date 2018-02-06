using System;
using Testcase.Telegrams.EVCtoDMI;
using Testcase.Telegrams.DMItoEVC;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 27.28.2 ‘Remove VBC’ Validation Window
    /// TC-ID: 22.28.2
    /// 
    /// This test case verifies the general appearances of Remove VBC validation window based on chapter 6.5.6.3 of requirement specification with packet information sending and receiving.
    /// 
    /// Tested Requirements:
    /// MMI_gen 9932; MMI_gen 9933 (partly: MMI_gen 5724, MMI_gen 5720, MMI_gen 2519 (partly: Remove VBC validation window); MMI_gen 1426 (partly: Remove VBC validation window)); MMI_gen 9934; MMI_gen 8573; MMI_gen 8574; MMI_gen 9935; MMI_gen 8572 (partly: MMI_gen 5215 (partly: Close button, Window title, Input field, No button, Yes button), MMI_gen 5216, MMI_gen 7943, window on half grid array, MMI_gen 5214, MMI_gen 5006, MMI_gen 5484, MMI_gen 5263 (partly: MMI_gen 4696, MMI_gen 4697, MMI_gen 4698, MMI_gen 4700 (partly: data validation process), MMI_gen 4702 (partly: right aligned), MMI_gen 4704 (partly: left aligned), partly: MMI_gen 4701), MMI_gen 5303); MMI_gen 4392 (partly: [Close] NA11, returning to the parent window, [Enter], touch screen); MMI_gen 4350; MMI_gen 4351; MMI_gen 4353; MMI_gen 9390 (partly: Remove VBC Validation window); MMI_gen 4377 (partly: shown);
    /// 
    /// Scenario:
    /// Enter and confirm all data in Remove VBC window. Then, verify the display information and received packet data EVC-29.Press ‘No’ button and verify that the value of an input field is changed refer to pressed button.Confirm entered data by pressing an input field. Then, verify that DMI closes Remove VBC Validation window and opens Remove VBC window with sending out packet EVC-101.Open Remove VBC window. Then, enter and confirm all data.Press ‘Close’ button. Then, verify that DMI closes Remove VBC Validation window and opens Remove VBC window with sending out packet EVC-101.Open Remove VBC window. Then, enter and confirm all data.Press ‘Yes’ button and verify that the value of an input field is changed refer to pressed button.Confirm entered data by pressing an input field. Then, verify that DMI closes Remove VBC Validation window and opens Remove VBC window with sending out packet EVC-129.Simulate loss-communication between ETCS and DMI. Then, re-establish communication and verify the state of buttons in Remove VBC Validation window.
    /// 
    /// Used files:
    /// N/A
    /// </summary>
    public class TC_ID_22_28_2_Remove_VBC_Validation_Window : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:

            // Call the TestCaseBase PreExecution
            base.PreExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 0;
            // Testcase entrypoint

            StartUp();
            // System is powered ON.Cabin is activated.Perform SoM until Level 1 is selected and confirmed.
            // The VBC code “65536” is stored on ETCS. (See the information in the “Data View” menu)Settings window is opened.Remove VBC window is opened.
            DmiActions.Complete_SoM_L1_SB(this);

            MakeTestStepHeader(1, UniqueIdentifier++,
                "Perform the following procedure,Enter and confirm value ‘65536’ at input field.Press ‘Yes’ button",
                "Verify the following information,Use the log file to confirm that DMI received the packet MMI_ECHOED_REMOVE_VBC_DATA (EVC-29). DMI displays Remove VBC Validation window.The following objects are displayed in Remove VBC Validation window. Enabled Close button (NA11)Window TitleInput fieldYes buttonNo buttonWindow TitleThe window title is ‘Validate Remove VBC’.The window title is right aligned.LayerThe window is displayed in main area A/B/C/D/E/F/G.All areas of Data validation window are displayed in Layer 0.Input fieldThe window contains a single input field which has only data area.The value of input field is empty.KeyboardThe displayed keyboard type is dedicated keyboard which contain only ‘Yes’ and ‘No’ button.The key #7 is No button.The key #8 is Yes button.Echo TextEcho Text is composed of a Label part and Data part.The Label of echo text is right aligned.The Data part of echo text is left aligned.The order of echo texts are same as of the Remove VBC window.The data part of echo texts are displayed the data value same as of the Remove VBC window.The echo texts are located in Main area A,B,C and E.The echo texts colour is white.Use the log file to confirm that the following variable in packet EVC-29 is same as entered data and display in the data part of echo text,MMI_M_VBC_CODE = entered VBC codeGeneral property of windowThe Remove VBC Validation window is presented with objects, text messages and buttons which is the one of several levels and allocated to areas of DMIAll objects, text messages and buttons are presented within the same layer.The Default window is not displayed and covered the current window");
            /*
            Test Step 1
            Action: Perform the following procedure,Enter and confirm value ‘65536’ at input field.Press ‘Yes’ button
            Expected Result: Verify the following information,Use the log file to confirm that DMI received the packet MMI_ECHOED_REMOVE_VBC_DATA (EVC-29). DMI displays Remove VBC Validation window.The following objects are displayed in Remove VBC Validation window. Enabled Close button (NA11)Window TitleInput fieldYes buttonNo buttonWindow TitleThe window title is ‘Validate Remove VBC’.The window title is right aligned.LayerThe window is displayed in main area A/B/C/D/E/F/G.All areas of Data validation window are displayed in Layer 0.Input fieldThe window contains a single input field which has only data area.The value of input field is empty.KeyboardThe displayed keyboard type is dedicated keyboard which contain only ‘Yes’ and ‘No’ button.The key #7 is No button.The key #8 is Yes button.Echo TextEcho Text is composed of a Label part and Data part.The Label of echo text is right aligned.The Data part of echo text is left aligned.The order of echo texts are same as of the Remove VBC window.The data part of echo texts are displayed the data value same as of the Remove VBC window.The echo texts are located in Main area A,B,C and E.The echo texts colour is white.Use the log file to confirm that the following variable in packet EVC-29 is same as entered data and display in the data part of echo text,MMI_M_VBC_CODE = entered VBC codeGeneral property of windowThe Remove VBC Validation window is presented with objects, text messages and buttons which is the one of several levels and allocated to areas of DMIAll objects, text messages and buttons are presented within the same layer.The Default window is not displayed and covered the current window
            Test Step Comment: (1) MMI_gen 9932 (partly: EVC-28);(2) MMI_gen 9932 (partly: open Remove VBC Validation window, touch screen);(3) MMI_gen 8572 (partly: MMI_gen 5215 (partly: Close button, Window title, Input field, No button, Yes button)); MMI_gen 4392 (partly: [Close] NA11);(4) MMI_gen 8573;(5) MMI_gen 8572 (partly: MMI_gen 5216);(6) MMI_gen 8572 (partly: MMI_gen 7943);(7) MMI_gen 8572 (partly: MMI_gen 5303);(8) MMI_gen 8572 (partly: MMI_gen 5214 (partly: single input field));          (9) MMI_gen 8572 (partly: MMI_gen 5484 (partly: empty)); (10) MMI_gen 8572 (partly: MMI_gen 5214 (partly: dedicated keyboard, MMI_gen 5006), MMI_gen 5006);(11) MMI_gen 8572 (partly: MMI_gen 5263 (partly: MMI_gen 4696));(12) MMI_gen 8572 (partly: MMI_gen 5263 (partly: MMI_gen 4702 (partly: right aligned)));(13) MMI_gen 8572 (partly: MMI_gen 5263 (partly: MMI_gen 4704 (partly: left aligned)));(14) MMI_gen 8572 (partly: MMI_gen 5263 (partly: MMI_gen 4701 (partly: same order), MMI_gen 4697));(15) MMI_gen 8572 (partly: MMI_gen 5263 (partly: MMI_gen 4698));(16) MMI_gen 8572 (partly: MMI_gen 5263 (partly: MMI_gen 4701 (partly: Main area A, B, C and E));(17) MMI_gen 8572 (partly: MMI_gen 5263 (partly: MMI_gen 4700 (partly: data validation process)));(18) MMI_gen 9935; MMI_gen 8574;(19) MMI_gen 4350;(20) MMI_gen 4351;(21) MMI_gen 4353;
            */
            DmiActions.ShowInstruction(this, "Press the ‘Settings’ button");

            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Settings;
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.RemoveVBC |
                                                               EVC30_MMIRequestEnable.EnabledRequests.SetVBC;
            EVC30_MMIRequestEnable.Send();

            DmiActions.ShowInstruction(this, "Press the ‘Remove VBC’ button");

            EVC19_MMIRemoveVBC.MMI_M_BUTTONS = Variables.MMI_M_BUTTONS_VBC.BTN_ENTER;
            EVC19_MMIRemoveVBC.MMI_N_VBC = 0;
            EVC19_MMIRemoveVBC.Send();

            DmiActions.ShowInstruction(this, "Enter and confirm the value ‘65536’, then press the ‘Yes’ button");

            EVC29_MMIEchoedRemoveVBCData.MMI_M_VBC_CODE_ = 65536;
            EVC29_MMIEchoedRemoveVBCData.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Remove VBC validation window with the right-aligned title ‘Validate Remove VBC’." +
                                Environment.NewLine +
                                "2. The window contains a data input field, an enabled Close button, symbol NA11, and ‘Yes’ and ‘No’ buttons." +
                                Environment.NewLine +
                                "3. The window is displayed in areas A, B, C, D, E, F and G." + Environment.NewLine +
                                "4. The data input field has only a data part with a blank value." +
                                Environment.NewLine +
                                "5. A dedicated keypad is displayed with <Yes> (#7) and <No> (#8) keys." +
                                Environment.NewLine +
                                "6. An echo text is displayed with a label and a data part." + Environment.NewLine +
                                "7. The echo text label is right-aligned, the data part is left-aligned." +
                                Environment.NewLine +
                                "8. The data part of the echo text displays the same value as the data input field." +
                                Environment.NewLine +
                                "9. The echo text is in areas A, B, C and E." + Environment.NewLine +
                                "10. The echo text displays ‘65536’ in white." + Environment.NewLine +
                                "11. Objects, text messages and buttons can be displayed in several levels. Within a level they are allocated to areas." +
                                Environment.NewLine +
                                "12. Objects, text messages and buttons in a layer form a window." +
                                Environment.NewLine +
                                "13. The Default window does not cover the current window.");

            MakeTestStepHeader(2, UniqueIdentifier++, "Press ‘No’ button",
                "The value of input field is changed refer to selected button");
            /*
            Test Step 2
            Action: Press ‘No’ button
            Expected Result: The value of input field is changed refer to selected button
            Test Step Comment: MMI_gen 8572 (partly: MMI_gen 5484 (partly: filled ‘No’));
            */
            DmiActions.ShowInstruction(this, @"Press the <No> key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The data input field displays ‘No’");

            MakeTestStepHeader(3, UniqueIdentifier++, "Confirm entered data by pressing an input field",
                "Verify the following information,DMI displays Remove VBC window.Use the log file to confirm that DMI sends out the packet [MMI_DRIVER_REQUEST (EVC-101)] with variable [MMI_DRIVER_REQUEST (EVC-101).MMI_M_REQUEST] = 26 (Exit Remove VBC)");
            /*
            Test Step 3
            Action: Confirm entered data by pressing an input field
            Expected Result: Verify the following information,DMI displays Remove VBC window.Use the log file to confirm that DMI sends out the packet [MMI_DRIVER_REQUEST (EVC-101)] with variable [MMI_DRIVER_REQUEST (EVC-101).MMI_M_REQUEST] = 26 (Exit Remove VBC)
            Test Step Comment: (1) MMI_gen 9933 (partly: No button, open Remove VBC window);(2) MMI_gen 9933 (partly: EVC-101, MMI_gen 5724); MMI_gen 4392 (partly: [Enter], touch screen);
            */
            DmiActions.ShowInstruction(this, @"Confirm the data by pressing in the data input field");

            EVC101_MMIDriverRequest.CheckMRequestReleased = Variables.MMI_M_REQUEST.ExitRemoveVBC;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Remove VBC window");

            MakeTestStepHeader(4, UniqueIdentifier++,
                "Perform the following procedure,Enter and confirm value ‘65536’ at input field.Press ‘Yes’ button",
                "DMI displays Remove VBC Validation window");
            /*
            Test Step 4
            Action: Perform the following procedure,Enter and confirm value ‘65536’ at input field.Press ‘Yes’ button
            Expected Result: DMI displays Remove VBC Validation window
            */
            DmiActions.ShowInstruction(this, "Enter and confirm the value ‘65536’, then press the ‘Yes’ button");

            EVC29_MMIEchoedRemoveVBCData.MMI_M_VBC_CODE_ = 65536;
            EVC29_MMIEchoedRemoveVBCData.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Remove VBC validation window");

            MakeTestStepHeader(5, UniqueIdentifier++, "Press ‘Close’ button",
                "Verify the following information,DMI displays Settings window.Use the log file to confirm that DMI sends out the packet [MMI_DRIVER_REQUEST (EVC-101)] with variable [MMI_DRIVER_REQUEST (EVC-101).MMI_M_REQUEST] = 25 (Exit Remove VBC)");
            /*
            Test Step 5
            Action: Press ‘Close’ button
            Expected Result: Verify the following information,DMI displays Settings window.Use the log file to confirm that DMI sends out the packet [MMI_DRIVER_REQUEST (EVC-101)] with variable [MMI_DRIVER_REQUEST (EVC-101).MMI_M_REQUEST] = 25 (Exit Remove VBC)
            Test Step Comment: (1) MMI_gen 9933 (partly: Close button, open Remove VBC window); MMI_gen 4392 (partly: returning to the parent window);(2) MMI_gen 9933 (partly: EVC-101);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button");

            // Test spec says 25 (== Exit Set VBC)
            EVC101_MMIDriverRequest.CheckMRequestReleased = Variables.MMI_M_REQUEST.ExitRemoveVBC;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Settings window");

            MakeTestStepHeader(6, UniqueIdentifier++,
                "Perform the following procedure,Press ‘Remove VBC’ button.Enter and confirm value ‘65536’ at input field.Press ‘Yes’ button",
                "DMI displays Remove VBC Validation window");
            /*
            Test Step 6
            Action: Perform the following procedure,Press ‘Remove VBC’ button.Enter and confirm value ‘65536’ at input field.Press ‘Yes’ button
            Expected Result: DMI displays Remove VBC Validation window
            */
            DmiActions.ShowInstruction(this, "Press the ‘Remove VBC’ button");

            EVC19_MMIRemoveVBC.MMI_M_BUTTONS = Variables.MMI_M_BUTTONS_VBC.BTN_ENTER;
            EVC19_MMIRemoveVBC.MMI_N_VBC = 0;
            EVC19_MMIRemoveVBC.Send();

            DmiActions.ShowInstruction(this, "Enter and confirm the value ‘65536’");

            EVC19_MMIRemoveVBC.MMI_M_BUTTONS = Variables.MMI_M_BUTTONS_VBC.BTN_YES_DATA_ENTRY_COMPLETE;
            EVC19_MMIRemoveVBC.MMI_N_VBC = 1;
            EVC19_MMIRemoveVBC.MMI_Q_DATA_CHECK = Variables.Q_DATA_CHECK.All_checks_passed;
            EVC19_MMIRemoveVBC.ECHO_TEXT = "65536";
            EVC19_MMIRemoveVBC.Send();

            DmiActions.ShowInstruction(this, "Press the ‘Yes’ button");

            EVC29_MMIEchoedRemoveVBCData.MMI_M_VBC_CODE_ = 65536;
            EVC29_MMIEchoedRemoveVBCData.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Remove VBC validation window");

            MakeTestStepHeader(7, UniqueIdentifier++, "Press ‘Yes’ button",
                "The value of input field is changed refer to selected button");
            /*
            Test Step 7
            Action: Press ‘Yes’ button
            Expected Result: The value of input field is changed refer to selected button
            Test Step Comment: MMI_gen 8572 (partly: MMI_gen 5484 (partly: filled ‘Yes’));
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Yes’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The data input field displays ‘Yes’");

            MakeTestStepHeader(8, UniqueIdentifier++, "Press and hold an input field",
                "Verify the following information,(1)    The state of an input field is changed to ‘Pressed’, the border of button is removed");
            /*
            Test Step 8
            Action: Press and hold an input field
            Expected Result: Verify the following information,(1)    The state of an input field is changed to ‘Pressed’, the border of button is removed
            Test Step Comment: (1) MMI_gen 9390 (partly: Remove VBC Validation window);
            */
            DmiActions.ShowInstruction(this, @"Press and hold the data input field");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The data input field is displayed pressed, without a border.");

            MakeTestStepHeader(9, UniqueIdentifier++, "Slide out an input field",
                "Verify the following information,(1)     The state of an input field is changed to ‘Enabled, the border of button is shown without a sound");
            /*
            Test Step 9
            Action: Slide out an input field
            Expected Result: Verify the following information,(1)     The state of an input field is changed to ‘Enabled, the border of button is shown without a sound
            Test Step Comment: (1) MMI_gen 9390 (partly: Remove VBC Validation window);
            */
            DmiActions.ShowInstruction(this, "Whilst keeping the data input field pressed,  drag outside its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The data input field is displayed enabled, with a border." + Environment.NewLine +
                                "2. No sound is played.");

            MakeTestStepHeader(10, UniqueIdentifier++, "Slide back into an input field",
                "Verify the following information,(1)     The state of an input field is changed to ‘Pressed’, the border of button is removed");
            /*
            Test Step 10
            Action: Slide back into an input field
            Expected Result: Verify the following information,(1)     The state of an input field is changed to ‘Pressed’, the border of button is removed
            Test Step Comment: (1) MMI_gen 9390 (partly: Remove VBC Validation window);
            */
            DmiActions.ShowInstruction(this,
                @"Whilst keeping the data input field pressed, drag it back inside its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The data input field is displayed pressed, without a border.");

            MakeTestStepHeader(11, UniqueIdentifier++, "Release the pressed area",
                "DMI displays Settings window.Verify the following information,The Remove VBC validation is closed.Use the log file to confirm that DMI sends out the packet [MMI_CONFIRMED_REMOVE_VBC (EVC-129)] with variable based on confirmed data");
            /*
            Test Step 11
            Action: Release the pressed area
            Expected Result: DMI displays Settings window.Verify the following information,The Remove VBC validation is closed.Use the log file to confirm that DMI sends out the packet [MMI_CONFIRMED_REMOVE_VBC (EVC-129)] with variable based on confirmed data
            Test Step Comment: (1) MMI_gen 9933 (partly: MMI_gen 5720 (partly: closed));(2) MMI_gen 9934; MMI_gen 9933 (partly: MMI_gen 5720 (partly: ConfirmedData-Packet)); MMI_gen 9390 (partly: Remove VBC Validation window);
            */
            DmiActions.ShowInstruction(this, @"Release the data input field");

            EVC129_MMIConfirmedRemoveVBC.Check_VBC_Code = 65536;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI closes the Remove VBC validation window and displays the Settings window.");

            MakeTestStepHeader(12, UniqueIdentifier++,
                "Perform the following procedure,Press ‘Set VBC’ button.Enter and validate value ‘65536’Press ‘Remove VBC button.Enter and confirm value ‘65536’ at input field.Press ‘Yes’ button.Then, simulate loss-communication between ETCS onboard and DMI",
                "DMI displays Default window with the  message “ATP Down Alarm” and sound alarm");
            /*
            Test Step 12
            Action: Perform the following procedure,Press ‘Set VBC’ button.Enter and validate value ‘65536’Press ‘Remove VBC button.Enter and confirm value ‘65536’ at input field.Press ‘Yes’ button.Then, simulate loss-communication between ETCS onboard and DMI
            Expected Result: DMI displays Default window with the  message “ATP Down Alarm” and sound alarm
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Set VBC’ button");

            EVC18_MMISetVBC.MMI_N_VBC = 0;
            EVC18_MMISetVBC.MMI_M_BUTTONS = Variables.MMI_M_BUTTONS_VBC.NoButton;
            EVC18_MMISetVBC.Send();

            DmiActions.ShowInstruction(this, "Enter and confirm the value ‘65536’");

            EVC28_MMIEchoedSetVBCData.MMI_M_VBC_CODE_ = 65536;
            EVC28_MMIEchoedSetVBCData.Send();

            EVC18_MMISetVBC.MMI_M_BUTTONS = Variables.MMI_M_BUTTONS_VBC.BTN_YES_DATA_ENTRY_COMPLETE;
            EVC18_MMISetVBC.MMI_Q_DATA_CHECK = Variables.Q_DATA_CHECK.All_checks_passed;
            // EVC18_MMISetVBC.ECHO_TEXT = "65536";
            EVC18_MMISetVBC.Send();
            DmiActions.ShowInstruction(this, "Press the ‘Yes’ button, then press the ‘Remove VBC’ button");

            EVC19_MMIRemoveVBC.MMI_N_VBC = 0;
            EVC19_MMIRemoveVBC.MMI_M_BUTTONS = Variables.MMI_M_BUTTONS_VBC.BTN_ENTER;
            EVC19_MMIRemoveVBC.Send();

            DmiActions.ShowInstruction(this, "Enter and confirm the value ‘65536’, then press the ‘Yes’ button");

            DmiActions.Simulate_communication_loss_EVC_DMI(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the message ‘ATP Down Alarm’." + Environment.NewLine +
                                "2. The ‘Alarm’ sound is played.");

            MakeTestStepHeader(13, UniqueIdentifier++, "Re-establish communication between ETCS onboard and DMI",
                "Verify the following information,All buttons except ‘No’ button are disabled.The state of ‘No’ button is enabled.The disabled buttons are shown as a button is state ‘disabled’ with text label in dark-grey");
            /*
            Test Step 13
            Action: Re-establish communication between ETCS onboard and DMI
            Expected Result: Verify the following information,All buttons except ‘No’ button are disabled.The state of ‘No’ button is enabled.The disabled buttons are shown as a button is state ‘disabled’ with text label in dark-grey
            Test Step Comment: (1) MMI_gen 9934 (partly: MMI_gen 2519 (partly: Remove VBC Validation window, All Request buttons except negative validations));(2) MMI_gen 9934 (partly: MMI_gen 2519 (partly: Remove VBC Validation window, All negative validations));(3) MMI_gen 9934 (partly: MMI_gen 1426 (partly: Remove VBC Validation window)); MMI_gen 4377 (partly: shown);
            */
            DmiActions.Re_establish_communication_EVC_DMI(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. All buttons except the ‘No’ button are disabled." + Environment.NewLine +
                                "2. The disabled buttons are displayed with the labels in Dark-grey." +
                                Environment.NewLine +
                                "3. The ‘No’ button is displayed enabled.");

            MakeTestStepHeader(14, UniqueIdentifier++, "End of test", "");

            /*
            Test Step 14
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
using System;
using Testcase.Telegrams.EVCtoDMI;
using Testcase.Telegrams.DMItoEVC;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 27.22.4 Brake percentage validation window
    /// TC-ID: 22.22.4 
    /// 
    /// This test case verifies the general appearances of Brake percentage validation window based on chapter 6.5.6.3 of requirement specification with packet information sending and receiving.
    /// 
    /// Tested Requirements:
    /// MMI_gen 11834; MMI_gen 11835; MMI_gen 11836; MMI_gen 11837; MMI_gen 11838; MMI_gen 11839; MMI_gen 11833 (partly: MMI_gen 5215 (partly: Close button, Window title, Input field, No button, Yes button), MMI_gen 5216, MMI_gen 7943, window on half grid array, MMI_gen 5214, MMI_gen 5006,  MMI_gen 5484, MMI_gen 5263 (partly: MMI_gen 4696, MMI_gen 4697, MMI_gen 4698, MMI_gen 4700 (partly: data validation process), MMI_gen 4702 (partly: right aligned), MMI_gen 4704 (partly: left aligned), partly: MMI_gen 4701), MMI_gen 5303); MMI_gen 5724; MMI_gen 5720; MMI_gen 2519 (partly: Brake validation window); MMI_gen 1426 (partly: Brake validation window); MMI_gen 4392 (partly: [Close] NA11, returning to the parent window, [Enter], touch screen); MMI_gen 4350; MMI_gen 4351; MMI_gen 4353; MMI_gen 4377 (partly: shown);
    /// 
    /// Scenario:
    /// Enter and confirm all data in Brake Percentage window. Then, verify the display information and received packet data EVC-51.Press ‘No’ button and verify that the value of an input field is changed refer to pressed button.Confirm entered data by pressing an input field. Then, verify that DMI closes Brake Percentage Validation window and open Brake window with sending out packet EVC-101.Open Brake Percentage window. Then, enter and confirm all data.Press ‘Close’ button. Then, verify that DMI closes Brake Percentage Validation window and open Brake window with sending out packet EVC-101.Open Brake Percentage window. Then, enter and confirm all data.Press ‘Yes’ button and verify that the value of an input field is changed refer to pressed button.Confirm entered data by pressing an input field. Then, verify that DMI close Brake Percentage Validation window and open Brake window with sending out packet EVC-151.Simulate loss-communication between ETCS and DMI. Then, re-establish communication and verify the state of buttons in Brake Percentage Validation window.
    /// 
    /// Used files:
    /// N/A
    /// </summary>
    public class TC_ID_22_22_4_Brake_percentage_validation_window : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // Call the TestCaseBase PreExecution
            base.PreExecution();

            // Configure atpcu configuration file as following (See the instruction in Appendix 2)M_InstalledLevels = 31NID_NTC_Installe_0 = 22 (ATC-2)
            //Test system is powered on.Cabin is activated.Level ATC-2 is selected and confirmed.SoM is performed until train running number is entered.Settings window is opened.Brake button is enabled.Brake Percentage button is enabled.Brake Percentage window is opened.
            DmiActions.Activate_Cabin_1(this);

            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.StandBy;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Level = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_LEVEL.L2;

            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH =
                EVC30_MMIRequestEnable.EnabledRequests.EnableBrakePercentage;
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Settings; // Settings
            EVC30_MMIRequestEnable.Send();
        }

        public override bool TestcaseEntryPoint()
        {
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 0;
            // Testcase entrypoint
            TraceInfo("This test case requires an ATP configuration change - " +
                      "See Precondition requirements. If this is not done manually, the test may fail!");

            MakeTestStepHeader(1, UniqueIdentifier++, "Enter and confirm all data in Brake Percentage window",
                "Verify the following information,(1)   Use the log file to confirm that DMI received the packet MMI_ECHOED_BRAKE_PERCENTAGE (EVC-51). (2)   Use the log file to confirm that the following variables in packet EVC-51 are same as entered data,MMI_M_BP_CURRENT = entered brake percentageMMI_M_BP_MEASURED = entered Last measured BPMMI_M_BP_ORIG = entered Original BP(3)   DMI displays Brake Percentage Validation window.(4)   The following objects are displayed in Brake Percentage Validation window. Enabled Close button (NA11)Window TitleInput fieldYes buttonNo buttonWindow Title(5)   The window title is ‘Validate brake percentage’.(6)   The window title is right aligned.Layer(7)   The window is displayed in main area A/B/C/D/E/F/G.(8)   All areas of Data validation window are displayed in Layer 0.Input field(9)   The window contains a single input field which has only data area.(10)   The value of input field is empty.Keyboard(11)   The displayed keyboard type is dedicated keyboard which contain only ‘Yes’ and ‘No’ button.The key #7 is No button.The key #8 is Yes button.Echo Text(12)   Echo Text is composed of a Label part and Data part.(13)   The Label of echo text is right aligned.(14)   The Data part of echo text is left aligned.(15)   The order of echo texts are same as of the Brake Percentage window as follows,Original BPLast measured BPBrake percentage(16)   The data part of echo texts are display the data value same as of the Brake percentage window.(17)   The echo texts are located in Main area A,B,C and E.(18)   The echo texts colour is white.General property of window(19)   The Brake percentage validation window is presented with objects and buttons which is the one of several levels and allocated to areas of DMI(20)   All objects, text messages and buttons are presented within the same layer.(21)   The Default window is not displayed and covered the current window");
            /*
            Test Step 1
            Action: Enter and confirm all data in Brake Percentage window
            Expected Result: Verify the following information,(1)   Use the log file to confirm that DMI received the packet MMI_ECHOED_BRAKE_PERCENTAGE (EVC-51). (2)   Use the log file to confirm that the following variables in packet EVC-51 are same as entered data,MMI_M_BP_CURRENT = entered brake percentageMMI_M_BP_MEASURED = entered Last measured BPMMI_M_BP_ORIG = entered Original BP(3)   DMI displays Brake Percentage Validation window.(4)   The following objects are displayed in Brake Percentage Validation window. Enabled Close button (NA11)Window TitleInput fieldYes buttonNo buttonWindow Title(5)   The window title is ‘Validate brake percentage’.(6)   The window title is right aligned.Layer(7)   The window is displayed in main area A/B/C/D/E/F/G.(8)   All areas of Data validation window are displayed in Layer 0.Input field(9)   The window contains a single input field which has only data area.(10)   The value of input field is empty.Keyboard(11)   The displayed keyboard type is dedicated keyboard which contain only ‘Yes’ and ‘No’ button.The key #7 is No button.The key #8 is Yes button.Echo Text(12)   Echo Text is composed of a Label part and Data part.(13)   The Label of echo text is right aligned.(14)   The Data part of echo text is left aligned.(15)   The order of echo texts are same as of the Brake Percentage window as follows,Original BPLast measured BPBrake percentage(16)   The data part of echo texts are display the data value same as of the Brake percentage window.(17)   The echo texts are located in Main area A,B,C and E.(18)   The echo texts colour is white.General property of window(19)   The Brake percentage validation window is presented with objects and buttons which is the one of several levels and allocated to areas of DMI(20)   All objects, text messages and buttons are presented within the same layer.(21)   The Default window is not displayed and covered the current window
            Test Step Comment: (1) MMI_gen 11834 (partly: EVC-51);(2) MMI_gen 11839;(3) MMI_gen 11834 (partly: open Brake Percentage Validation window, touch screen);(4) MMI_gen 11833 (partly: MMI_gen 5215 (partly: Close button, Window title, Input field, No button, Yes button)); MMI_gen 4392 (partly: [Close] NA11);(5) MMI_gen 11837;(6) MMI_gen 11833 (partly: MMI_gen 5216);(7) MMI_gen 11833 (partly: MMI_gen 7943);(8) MMI_gen 11833 (partly: MMI_gen 5303);(9) MMI_gen 11833 (partly: MMI_gen 5214 (partly: single input field));          (10) MMI_gen 11833 (partly: MMI_gen 5484 (partly: empty)); (11) MMI_gen 11833 (partly: MMI_gen 5214 (partly: dedicated keyboard, MMI_gen 5006), MMI_gen 5006);(12) MMI_gen 11833 (partly: MMI_gen 5263 (partly: MMI_gen 4696));(13) MMI_gen 11833 (partly: MMI_gen 5263 (partly: MMI_gen 4702 (partly: right aligned)));(14) MMI_gen 11833 (partly: MMI_gen 5263 (partly: MMI_gen 4704 (partly: left aligned)));(15) MMI_gen 11838;                  MMI_gen 11833 (partly: MMI_gen 5263 (partly: MMI_gen 4701 (partly: same order), MMI_gen 4697));(16) MMI_gen 11833 (partly: MMI_gen 5263 (partly: MMI_gen 4698));(17) MMI_gen 11833 (partly: MMI_gen 5263 (partly: MMI_gen 4701 (partly: Main area A, B, C and E));(18) MMI_gen 11833 (partly: MMI_gen 5263 (partly: MMI_gen 4700 (partly: data validation process)));(19) MMI_gen 4350;(20) MMI_gen 4351;(21) MMI_gen 4353;
            */
            DmiActions.ShowInstruction(this,
                "Press the ‘Brake’ button in the Settings window, then the ‘Brake percentage’ button.");

            EVC50_MMICurrentBrakePercentage.Send();

            DmiActions.ShowInstruction(this, "Enter and confirm the value ‘90’ for ‘Brake percentage’");

            EVC51_MMIEchoedBrakePercentage.MMI_M_BP_ORIG_ = 99;
            EVC51_MMIEchoedBrakePercentage.MMI_M_BP_CURRENT_ = 90;
            EVC51_MMIEchoedBrakePercentage.MMI_M_BP_MEASURED_ = 95;
            EVC51_MMIEchoedBrakePercentage.Send();

            // Spec says echo texts are displayed in areas A, B, C and E but DMI_RS_ETCS shows them in area D ??
            WaitForVerification("Check the following (* indicates sub-areas drawn as one area):" + Environment.NewLine +
                                Environment.NewLine +
                                @"1. DMI displays the Brake percentage validation window with 3 layers in a half-grid array with the title ‘Validate brake percentage’, right-aligned." +
                                Environment.NewLine +
                                "2. The Brake percentage validation window is displayed in areas A, B, C, D, F and G in layer 0." +
                                Environment.NewLine +
                                @"3. The Brake percentage validation window displays one data input field (blank), an ‘Enabled Close’ button (symbol NA11)." +
                                Environment.NewLine +
                                "4. A dedicated keypad is displayed below the data input field, containing enabled  <No> and <Yes> keys." +
                                Environment.NewLine +
                                "7. 3 echo texts are displayed in areas A, B, C and E [????] with a Label Part (right-aligned) and a Data Part (left-aligned) with white text." +
                                Environment.NewLine +
                                "8. The echo texts in order (top to bottom) are labelled ‘Original BP’, ‘Last measured BP’ and ‘Brake percentage’." +
                                Environment.NewLine +
                                "9. The echo texts (in the same order) display ‘95’, ‘92’ and ‘90’, respectively." +
                                Environment.NewLine +
                                "8. Objects, text messages and buttons can be displayed in several levels. Within a level they are allocated to areas." +
                                Environment.NewLine +
                                "9. Objects, text messages and buttons in a layer form a window." +
                                Environment.NewLine +
                                "10. The Default window does not cover the current window.");

            MakeTestStepHeader(2, UniqueIdentifier++, "Press ‘No’ button",
                "The value of input field is changed refer to selected button");
            /*
            Test Step 2
            Action: Press ‘No’ button
            Expected Result: The value of input field is changed refer to selected button
            Test Step Comment: MMI_gen 11833 (partly: MMI_gen 5484 (partly: filled ‘No’));
            */
            DmiActions.ShowInstruction(this, @"Press the <No> key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The data input field displays ‘No’.");

            MakeTestStepHeader(3, UniqueIdentifier++, "Confirm entered data by pressing an input field",
                "Verify the following information,DMI displays Brake window.Use the log file to confirm that DMI sends out the packet [MMI_DRIVER_REQUEST (EVC-101)] with variable [MMI_DRIVER_REQUEST (EVC-101).MMI_M_REQUEST] = 60 (Exit brake percentage)");
            /*
            Test Step 3
            Action: Confirm entered data by pressing an input field
            Expected Result: Verify the following information,DMI displays Brake window.Use the log file to confirm that DMI sends out the packet [MMI_DRIVER_REQUEST (EVC-101)] with variable [MMI_DRIVER_REQUEST (EVC-101).MMI_M_REQUEST] = 60 (Exit brake percentage)
            Test Step Comment: (1) MMI_gen 11836 (partly: No button, open Brake window);(2) MMI_gen 11836 (partly: EVC-101); MMI_gen 5724; MMI_gen 4392 (partly: [Enter], touch screen);
            */
            DmiActions.ShowInstruction(this, "Press in the data input field to confirm the entered data");

            EVC101_MMIDriverRequest.CheckMRequestReleased = Variables.MMI_M_REQUEST.ExitBrakePercentage;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Brake window");

            MakeTestStepHeader(4, UniqueIdentifier++,
                "Perform the following procedure,Press ‘Brake’ percentage button.Enter and confirm all data in brake percentage window.Press ‘Yes’ button",
                "DMI displays Brake percentage validation window");
            /*
            Test Step 4
            Action: Perform the following procedure,Press ‘Brake’ percentage button.Enter and confirm all data in brake percentage window.Press ‘Yes’ button
            Expected Result: DMI displays Brake percentage validation window
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Brake percentage’ button.");

            EVC50_MMICurrentBrakePercentage.MMI_M_BP_ORIG = 99;
            EVC50_MMICurrentBrakePercentage.MMI_M_BP_MEASURED = 95;
            EVC50_MMICurrentBrakePercentage.MMI_M_BP_CURRENT = 90;
            EVC50_MMICurrentBrakePercentage.Send();

            DmiActions.ShowInstruction(this, "Enter and confirm the value ‘89’ for ‘Brake percentage’");

            EVC51_MMIEchoedBrakePercentage.MMI_M_BP_ORIG_ = 99; // (99 -> 0x63, bit-inverted)
            EVC51_MMIEchoedBrakePercentage.MMI_M_BP_CURRENT_ = 89;
            EVC51_MMIEchoedBrakePercentage.MMI_M_BP_MEASURED_ = 95;
            EVC51_MMIEchoedBrakePercentage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Brake percentage validation window");

            DmiActions.ShowInstruction(this, "Press the <Yes> key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The data input field displays ‘Yes’");

            MakeTestStepHeader(5, UniqueIdentifier++, "Press ‘Close’ button",
                "Verify the following information,DMI displays Brake window.Use the log file to confirm that DMI sends out the packet [MMI_DRIVER_REQUEST (EVC-101)] with variable [MMI_DRIVER_REQUEST (EVC-101).MMI_M_REQUEST] = 60 (Exit brake percentage)");
            /*
            Test Step 5
            Action: Press ‘Close’ button
            Expected Result: Verify the following information,DMI displays Brake window.Use the log file to confirm that DMI sends out the packet [MMI_DRIVER_REQUEST (EVC-101)] with variable [MMI_DRIVER_REQUEST (EVC-101).MMI_M_REQUEST] = 60 (Exit brake percentage)
            Test Step Comment: (1) MMI_gen 11836 (partly: Close button, open Brake window);(2) MMI_gen 11836 (partly: EVC-101); MMI_gen 4392 (partly: returning to the parent window);
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button");

            EVC101_MMIDriverRequest.CheckMRequestReleased = Variables.MMI_M_REQUEST.ExitBrakePercentage;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Brake window");

            MakeTestStepHeader(6, UniqueIdentifier++,
                "Perform the following procedure,Press ‘Brake’ percentage button.Enter and confirm all data in brake percentage window.Press ‘Yes’ button",
                "DMI displays Brake percentage validation window");
            /*
            Test Step 6
            Action: Perform the following procedure,Press ‘Brake’ percentage button.Enter and confirm all data in brake percentage window.Press ‘Yes’ button
            Expected Result: DMI displays Brake percentage validation window
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Brake percentage’ button.");

            EVC50_MMICurrentBrakePercentage.MMI_M_BP_ORIG = 99;
            EVC50_MMICurrentBrakePercentage.MMI_M_BP_MEASURED = 95;
            EVC50_MMICurrentBrakePercentage.MMI_M_BP_CURRENT = 89;
            EVC50_MMICurrentBrakePercentage.Send();

            DmiActions.ShowInstruction(this, "Enter and confirm the value ‘88’ for ‘Brake percentage’");

            EVC51_MMIEchoedBrakePercentage.MMI_M_BP_ORIG_ = 99;
            EVC51_MMIEchoedBrakePercentage.MMI_M_BP_CURRENT_ = 88;
            EVC51_MMIEchoedBrakePercentage.MMI_M_BP_MEASURED_ = 95;
            EVC51_MMIEchoedBrakePercentage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Brake percentage validation window");

            MakeTestStepHeader(7, UniqueIdentifier++, "Press ‘Yes’ button",
                "The value of input field is changed refer to selected button");
            /*
            Test Step 7
            Action: Press ‘Yes’ button
            Expected Result: The value of input field is changed refer to selected button
            Test Step Comment: MMI_gen 11833 (partly: MMI_gen 5484 (partly: filled ‘Yes’));
            */
            DmiActions.ShowInstruction(this, @"Press the <Yes> key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The data input field displays ‘Yes’");

            MakeTestStepHeader(8, UniqueIdentifier++, "Confirm entered data by pressing an input field",
                "Verify the following information,The Brake percentage validation is closed.DMI displays Brake window.Use the log file to confirm that DMI sends out the packet [MMI_CONFIRMED_BRAKE_PERCENTAGE (EVC-151)] with variable based on confirmed data");
            /*
            Test Step 8
            Action: Confirm entered data by pressing an input field
            Expected Result: Verify the following information,The Brake percentage validation is closed.DMI displays Brake window.Use the log file to confirm that DMI sends out the packet [MMI_CONFIRMED_BRAKE_PERCENTAGE (EVC-151)] with variable based on confirmed data
            Test Step Comment: (1) MMI_gen 11835 (partly: closure); MMI_gen 11833 (partly: MMI_gen 5720 (partly: closed));(2) MMI_gen 11835 (partly: open Brake window);(3) MMI_gen 11836 (partly: EVC-151); MMI_gen 11833 (partly: MMI_gen 5720 (partly: ConfirmedData-Packet));
            */
            DmiActions.ShowInstruction(this, "Press in the data input field to confirm the entered data");

            EVC50_MMICurrentBrakePercentage.MMI_M_BP_ORIG = 99;
            EVC50_MMICurrentBrakePercentage.MMI_M_BP_CURRENT = 88;
            EVC50_MMICurrentBrakePercentage.MMI_M_BP_MEASURED = 95;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Brake window");

            MakeTestStepHeader(9, UniqueIdentifier++,
                "Perform the following procedure,Press ‘Brake percentage’ button.Enter and confirm all data in brake percentage window.Press ‘Yes’ button.Then, Simulate loss-communication between ETCS onboard and DMI",
                "DMI displays Default window with the  message “ATP Down Alarm” and sound alarm");
            /*
            Test Step 9
            Action: Perform the following procedure,Press ‘Brake percentage’ button.Enter and confirm all data in brake percentage window.Press ‘Yes’ button.Then, Simulate loss-communication between ETCS onboard and DMI
            Expected Result: DMI displays Default window with the  message “ATP Down Alarm” and sound alarm
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Brake percentage’ button.");

            EVC50_MMICurrentBrakePercentage.MMI_M_BP_ORIG = 99;
            EVC50_MMICurrentBrakePercentage.MMI_M_BP_MEASURED = 95;
            EVC50_MMICurrentBrakePercentage.MMI_M_BP_CURRENT = 88;
            EVC50_MMICurrentBrakePercentage.Send();

            DmiActions.ShowInstruction(this, "Enter and confirm the value ‘87’ for ‘Brake percentage’");

            DmiActions.Simulate_communication_loss_EVC_DMI(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the message ‘ATP Down Alarm’." + Environment.NewLine +
                                "2.The ‘Alarm’ sound is played.");

            MakeTestStepHeader(10, UniqueIdentifier++, "Re-establish communication between ETCS onboard and DMI",
                "Verify the following informaiton,All buttons except ‘No’ button are disabled.The state of ‘No’ button is enabled.The disabled buttons are shown as a button in state ‘disabled‘ with text label in dark-grey");
            /*
            Test Step 10
            Action: Re-establish communication between ETCS onboard and DMI
            Expected Result: Verify the following informaiton,All buttons except ‘No’ button are disabled.The state of ‘No’ button is enabled.The disabled buttons are shown as a button in state ‘disabled‘ with text label in dark-grey
            Test Step Comment: (1) MMI_gen 2519 (partly: Brake percentage Validation window, All Request buttons except negative validations);(2) MMI_gen 2519 (partly: Brake percentage Validation window, All negative validations);(3) MMI_gen 1426 (partly: Brake percentage Validation window); MMI_gen 4377 (partly: shown);
            */
            DmiActions.Re_establish_communication_EVC_DMI(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. All buttons and keys except the <No> key are displayed disabled." +
                                Environment.NewLine +
                                "2. The <No> key is displayed enabled." + Environment.NewLine +
                                "3. The text of all disabled button and keys labels is in dark-grey.");

            MakeTestStepHeader(11, UniqueIdentifier++, "End of test", "");

            /*
            Test Step 11
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
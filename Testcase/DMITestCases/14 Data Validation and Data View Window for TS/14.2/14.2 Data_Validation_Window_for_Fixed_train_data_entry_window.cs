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
using Testcase.Telegrams.DMItoEVC;
using Testcase.Telegrams.EVCtoDMI;

namespace Testcase.DMITestCases
{
    /// <summary>
    /// 14.2 Data Validation Window for Fixed train data entry window
    /// TC-ID: 9.2
    /// 
    /// This test case verifies the general appearances of Train data validation window (for fixed train data entry) based on chapter 6.5.6.3 of requirement specification with packet information sending and receiving.
    /// 
    /// Tested Requirements:
    /// MMI_gen 9461 (partly: Fixed Train data); MMI_gen 9463 (partly: MMI_gen 5724, MMI_gen 5720, MMI_gen 2519 (partly: Fixed Train validation window); MMI_gen 1426 (partly: Fixed Train validation window))); MMI_gen 9464; MMI_gen 8556; MMI_gen 8557; MMI_gen 9462 (partly: Fixed Train data); MMI_gen 8555 (partly: MMI_gen 5215 (partly: Close button, Window title, Input field, No button, Yes button), MMI_gen 5216, MMI_gen 7943, window on half grid array, MMI_gen 5214, MMI_gen 5006,  MMI_gen 5484, MMI_gen 5263 (partly: MMI_gen 4696, MMI_gen 4697, MMI_gen 4698, MMI_gen 4700 (partly: data validation process), MMI_gen 4702 (partly: right aligned), MMI_gen 4704 (partly: left aligned), MMI_gen 4701), MMI_gen 5303); MMI_gen 4377 (partly: shown);
    /// 
    /// Scenario:
    /// Enter and confirm all data in Train data window. Then, verify the display information and received packet data EVC-10.Press ‘No’ button and verify that the value of an input field is changed refer to pressed button.Confirm entered data by pressing an input field. Then, verify that DMI close Train data Validation window and open Train data window with sending out packet EVC-101.Open Train data window. Then, enter and confirm all data.Press ‘Close’ button. Then, verify that DMI close Train data Validation window and open Train data window with sending out packet EVC-101.Open Train data window. Then, enter and confirm all data.Press ‘Yes’ button and verify that the value of an input field is changed refer to pressed button.Confirm entered data by pressing an input field. Then, verify that DMI close Train data Validation window and open Train data window with sending out packet EVC-110.Simulate loss-communication between ETCS and DMI. Then, re-establish communication and verify the state of buttons in Train data Validation window.
    /// 
    /// Used files:
    /// N/A
    /// </summary>
    public class TC_ID_9_2_Data_Validation_Window : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // Open defaultValues_default.xml in OTE  then set all value of parameter "  TR_OBU_TrainType" to 1 (Now is 2)System is powered ON.Cabin is activated.SoM is performed until Level 1 is selected and confirmed.Train data window is opened.

            // Call the TestCaseBase PreExecution
            base.PreExecution();

            DmiActions.Complete_SoM_L1_SB(this);
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
            TraceInfo("This test case requires an ATP configuration change - " +
                      "See Precondition requirements. If this is not done manually, the test may fail!");

            /*
            Test Step 1
            Action: Enter and confirm all data in Train data window
            Expected Result: Verify the following information,Use the log file to confirm that DMI received the packet MMI_ECHOED_TRAIN_DATA (EVC-10). DMI displays Train data Validation window.The following objects are display in Train data Validation window.Close buttonWindow TitleInput fieldYes buttonNo buttonWindow TitleThe window title is ‘Validate Train data’.The window title is right aligned.LayerThe window is displayed in main area A/B/C/D/E/F/G.All area of Data validation window are Layer 0.Input fieldThe window contains a single input field which have only data area.The value of input field is empty.KeyboardThe displayed keyboard type is dedicated keyboard which contain only ‘Yes’ and ‘No’ button.The key #7 is No button.The key #8 is Yes button.Echo TextEcho Text is composed of a Label part and Data part.The Label of echo text is right aligned.The Data part of echo text is left aligned.The order of echo texts are same as of the Train data window as follows,Train typeThe data part of echo texts are display the data value same as of the Train data window.The echo texts are located in Main area A,B,C and E.The echo texts colour is white.Use the log file to confirm that the following variable in packet EVC-10 is same as entered data and display in the data part of echo text,MMI_M_DATA_ENABLE =1 (Only bit #0, Train Set ID is set) MMI_M_TRAINSET_ID != 0 and 10-15 (Train data set is chosen)MMI_X_CAPTION_TRAINSET =  entered Train Set
            Test Step Comment: (1) MMI_gen 9461 (partly: EVC-10, Fixed Train data);(2) MMI_gen 9461 (partly: open Train data Validation window, touch screen);(3) MMI_gen 8555 (partly: MMI_gen 5215 (partly: Close button, Window title, Input field, No button, Yes button));(4) MMI_gen 8556;(5) MMI_gen 8555 (partly: MMI_gen 5216);(6) MMI_gen 8555 (partly: MMI_gen 7943);(7) MMI_gen 8555 (partly: MMI_gen 5303);(8) MMI_gen 8555 (partly: MMI_gen 5214 (partly: single input field));          (9) MMI_gen 8555 (partly: MMI_gen 5484 (partly: empty)); (10) MMI_gen 8555 (partly: MMI_gen 5214 (partly: dedicated keyboard, MMI_gen 5006), MMI_gen 5006);(11) MMI_gen 8555 (partly: MMI_gen 5263 (partly: MMI_gen 4696));(12) MMI_gen 8555 (partly: MMI_gen 5263 (partly: MMI_gen 4702 (partly: right aligned)));(13) MMI_gen 8555 (partly: MMI_gen 5263 (partly: MMI_gen 4704 (partly: left aligned)));(14) MMI_gen 8557; MMI_gen 8555 (partly: MMI_gen 5263 (partly: MMI_gen 4701 (partly: same order), MMI_gen 4697));(15) MMI_gen 8555 (partly: MMI_gen 5263 (partly: MMI_gen 4698));(16) MMI_gen 8555 (partly: MMI_gen 5263 (partly: MMI_gen 4701 (partly: Main area A, B, C and E));(17) MMI_gen 8555 (partly: MMI_gen 5263 (partly: MMI_gen 4700 (partly: data validation process)));(18) MMI_gen 9462 (partly: Fixed Train data);
            */
            DmiActions.Send_EVC6_MMICurrentTrainData_FixedDataEntry(this, new[] { "FLU", "RLU", "Rescue" }, 15);

            DmiActions.ShowInstruction(this, @"Enter and confirm all data in Train data window");

            DmiActions.Send_EVC10_MMIEchoedTrainData_FixedDataEntry(this, new [] { "FLU", "RLU", "Rescue" });

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. DMI displays the Train data validation window, with the title ‘Validate Train data, right-aligned’." + Environment.NewLine +
                                @"2. The window contains a single data input field, a ‘Close’ button and a dedicated keypad for the data input field." + Environment.NewLine +
                                "3. The window is in one layer in areas A, B, C, D, E, F and G" + Environment.NewLine +
                                "4. The data input field displays a blank value and has only a data area." + Environment.NewLine +
                                "5. The keypad contains <Yes> (#7)and <No> (#8) keys." + Environment.NewLine +
                                "6. A ‘Train type’ echo text is displayed, with same value as in the Train data window ." + Environment.NewLine +
                                "7. The echo text has a Label (right-aligned text) and a Data part (left-aligned text) in white." + Environment.NewLine +
                                "8. The echo text is displayed in areas A, B, C and E.");
            
            /*
            Test Step 2
            Action: Press ‘No’ button
            Expected Result: The value of input field is changed refer to selected button
            Test Step Comment: MMI_gen 8555 (partly: MMI_gen 5484 (partly: filled ‘No’));
            */
            DmiActions.ShowInstruction(this, @"Press the <No> key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The data input field displays ‘No’");

            /*
            Test Step 3
            Action: Confirm entered data by pressing an input field
            Expected Result: Verify the following information,DMI displays Train data window.Use the log file to confirm that DMI sends out the packet [MMI_DRIVER_REQUEST (EVC-101)] with variable [MMI_DRIVER_REQUEST (EVC-101).MMI_M_REQUEST] = 4 (Exit Train data)
            Test Step Comment: (1) MMI_gen 9463 (partly: No button, open Train data window);(2) MMI_gen 9463 (partly: EVC-101, MMI_gen 5724);
            */
            DmiActions.ShowInstruction(this, "Confirm the value by pressing in the input field");

            EVC101_MMIDriverRequest.CheckMRequestReleased = Variables.MMI_M_REQUEST.ExitTrainDataEntry;
            DmiActions.Send_EVC6_MMICurrentTrainData_FixedDataEntry(this, new[] { "FLU", "RLU", "Rescue" }, 15);

            DmiExpectedResults.Train_data_window_displayed(this);

            /*
            Test Step 4
            Action: Perform the following procedure,Enter and confirm all data in Train data window.Press ‘Yes’ button
            Expected Result: DMI displays Train data validation window
            */
            DmiActions.ShowInstruction(this, "Enter and confirm all data in the Train data window, then press the ‘Yes’ button");

            DmiActions.Send_EVC10_MMIEchoedTrainData_FixedDataEntry(this, new[] { "FLU", "RLU", "Rescue" });

            DmiExpectedResults.Train_data_validation_window_displayed(this);

            /*
            Test Step 5
            Action: Press ‘Close’ button
            Expected Result: Verify the following information,DMI displays Main window.Use the log file to confirm that DMI sends out the packet [MMI_DRIVER_REQUEST (EVC-101)] with variable [MMI_DRIVER_REQUEST (EVC-101).MMI_M_REQUEST] = 4 (Exit Train data)
            Test Step Comment: (1) MMI_gen 9463 (partly: Close button, open Main window);(2) MMI_gen 9463 (partly: EVC-101);
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button");

            EVC101_MMIDriverRequest.CheckMRequestReleased = Variables.MMI_M_REQUEST.ExitTrainDataEntry;

            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.EVC30WindowID.Main;
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.TrainData;
            EVC30_MMIRequestEnable.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. DMI displays the Main window");
            /*
            Test Step 6
            Action: Perform the following procedure,Press ‘Train data’ button.Enter and confirm all data in Train data window.Press ‘Yes’ button
            Expected Result: DMI displays Train data validation window
            */
            DmiActions.ShowInstruction(this, "Press the ‘Train data’ button");

            DmiActions.Send_EVC6_MMICurrentTrainData_FixedDataEntry(this, new[] { "FLU", "RLU", "Rescue" }, 15);

            DmiActions.ShowInstruction(this, "Enter and confirm all data in the Train data window, then press the ‘Yes’ button");

            DmiActions.Send_EVC10_MMIEchoedTrainData_FixedDataEntry(this, new[] { "FLU", "RLU", "Rescue" });

            DmiExpectedResults.Train_data_validation_window_displayed(this);

            /*
            Test Step 7
            Action: Press ‘Yes’ button
            Expected Result: The value of input field is changed refer to selected button
            Test Step Comment: MMI_gen 8555 (partly: MMI_gen 5484 (partly: filled ‘Yes’));
            */
            DmiActions.ShowInstruction(this, @"Press the <Yes> key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The data input field displays ‘Yes’.");

            /*
            Test Step 8
            Action: Confirm entered data by pressing an input field
            Expected Result: DMI displays Train Running Number window.Verify the following information,The Train data validation is closed.Use the log file to confirm that DMI sends out the packet [MMI_CONFIRMED_TRAIN DATA (EVC-110)] with variable based on confirmed data
            Test Step Comment: (1) MMI_gen 9463 (partly: MMI_gen 5720 (partly: closed));(2) MMI_gen 9464; MMI_gen 9463 (partly: MMI_gen 5720 (partly: ConfirmedData-Packet));
            */
            DmiActions.ShowInstruction(this, "Confirm by pressing the data input field");

            EVC110_MMIConfimedTrainData.CheckConfirmedTrainData();

            EVC16_CurrentTrainNumber.TrainRunningNumber = 1;
            EVC16_CurrentTrainNumber.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. DMI closes the Train validation window and displays the Train running number window.");

            DmiActions.ShowInstruction(this, "Confirm the data");

            /*
            Test Step 9
            Action: Perform the following procedure,Press ‘Train data’ button.Enter and confirm all data in Train data window.Press ‘Yes’ button.Then, Simulate loss-communication between ETCS onboard and DMI
            Expected Result: DMI displays Default window with the  message “ATP Down Alarm” and sound alarm
            */
            // Already in train data window...

            DmiActions.ShowInstruction(this, "Enter and confirm all data in Train data window, then press the ‘Yes’ button");

            DmiActions.Send_EVC10_MMIEchoedTrainData_FixedDataEntry(this, new[] { "FLU", "RLU", "Rescue" });

            DmiActions.Simulate_communication_loss_EVC_DMI(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. DMI displays the Default window with the message ‘ATP Down Alarm’." + Environment.NewLine +
                                @"2. The ‘Alarm’ sound is played.");

            /*
            Test Step 10
            Action: Re-establish communication between ETCS onboard and DMI
            Expected Result: Verify the following informaiton,All buttons except ‘No’ button are disabled.The state of ‘No’ button is enabled.The disabled button are shown as a button is state ‘enabled’ with text label in dark-grey
            Test Step Comment: (1) MMI_gen 9463 ( partly: MMI_gen 2519 (partly: Train data Validation window, All Request buttons except negative validations));(2) MMI_gen 9463 ( partly: MMI_gen 2519 (partly: Train data Validation window, All negative validations));(3) MMI_gen 9463 ( partly: MMI_gen 1426 (partly: Train data Validation window)); MMI_gen 4377 (partly: shown);
            */
            DmiActions.Re_establish_communication_EVC_DMI(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. All buttons are disabled, with dark-grey labels." + Environment.NewLine +
                                @"2.  The <No> key is enabled.");

            /*
            Test Step 11
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
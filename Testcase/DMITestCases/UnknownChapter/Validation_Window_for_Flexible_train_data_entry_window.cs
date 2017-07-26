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

namespace Testcase.DMITestCases
{
    /// <summary>
    /// Data Validation Window for Flexible train data entry window
    /// TC-ID: 9.1
    /// 
    /// This test case verifies the general appearances of Train data validation window (for Flexible Train data entry) based on chapter 6.5.6.3 of requirement specification with packet information sending and receiving.
    /// 
    /// Tested Requirements:
    /// MMI_gen 9461 (partly: Flexible Train data); MMI_gen 9463 (partly: MMI_gen 5724, MMI_gen 5720, MMI_gen 2519 (partly: Flexible Train validation window); MMI_gen 1426 (partly: Flexible Train validation window)); MMI_gen 9464; MMI_gen 8556; MMI_gen 8557; MMI_gen 9462 (partly: Flexible Train data); MMI_gen 8555 (partly: MMI_gen 5215 (partly: Close button, Window title, Input field, No button, Yes button), MMI_gen 5216, MMI_gen 7943, window on half grid array, MMI_gen 5214, MMI_gen 5006,  MMI_gen 5484, MMI_gen 5263 (partly: MMI_gen 4696, MMI_gen 4697, MMI_gen 4698, MMI_gen 4700 (partly: data validation process), MMI_gen 4702 (partly: right aligned), MMI_gen 4704 (partly: left aligned),  MMI_gen 4701), MMI_gen 5303); MMI_gen 4377 (partly: shown);
    /// 
    /// Scenario:
    /// Enter and confirm all data in Train data window. Then, verify the display information and received packet data EVC-10.Press ‘No’ button and verify that the value of an input field is changed refer to pressed button.Confirm entered data by pressing an input field. Then, verify that DMI close Train data Validation window and open Train data window with sending out packet EVC-101.Open Train data window. Then, enter and confirm all data.Press ‘Close’ button. Then, verify that DMI close Train data Validation window and open Train data window with sending out packet EVC-101.Open Train data window. Then, enter and confirm all data.Press ‘Yes’ button and verify that the value of an input field is changed refer to pressed button.Confirm entered data by pressing an input field. Then, verify that DMI close Train data Validation window and open Train data window with sending out packet EVC-110.Simulate loss-communication between ETCS and DMI. Then, re-establish communication and verify the state of buttons in Train data Validation window.
    /// 
    /// Used files:
    /// N/A
    /// </summary>
    public class Validation_Window_for_Flexible_train_data_entry_window : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // Open defaultValues_default.xml in OTE  then set all value of parameter "  TR_OBU_TrainType" to 2System is powered ON.Cabin is activated.SoM is performed until Level 1 is selected and confirmed.Train data window is opened.

            // Call the TestCaseBase PreExecution
            base.PreExecution();
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
            Action: Enter and confirm all data in Train data window.
            Expected Result: Verify the following information,Use the log file to confirm that DMI received the packet MMI_ECHOED_TRAIN_DATA (EVC-10). DMI displays Train data Validation window.The following objects are display in Train data Validation window.Close buttonWindow TitleInput fieldYes buttonNo buttonWindow TitleThe window title is ‘Validate train data’.The window title is right aligned.LayerThe window is displayed in main area A/B/C/D/E/F/G.All area of Data validation window are Layer 0.Input fieldThe window contains a single input field which have only data area.The value of input field is empty.KeyboardThe displayed keyboard type is dedicated keyboard which contain only ‘Yes’ and ‘No’ button.The key #7 is No button.The key #8 is Yes button.Echo TextEcho Text is composed of a Label part and Data part.The Label of echo text is right aligned.The Data part of echo text is left aligned.The order of echo texts are same as of the Train data window as follows,Train categoryLengthBrake percentageMaximum speedAxle load categoryAirtightLoading gaugeThe data part of echo texts are display the data value same as of the Train data window.The echo texts are located in Main area A,B,C and E.The echo texts colour is white.Use the log file to confirm that the following variable in packet EVC-10 is same as entered data and display in the data part of echo text,MMI_M_DATA_ENABLE =32512 (Only bit #0, Train Set ID is not set) MMI_M_TRAINSET_ID = 0 (Use Flexible TDE)MMI_NID_KEY_LOAD_GAUGE =  entered Loading GaugeMMI_M_AIRTIGHT = entered AirtightMMI_NID_KEY_AXLE_LOAD = entered Axle loadMMI_V_MAXTRAIN = entered Max train speedMMI_M_BRAKE_PERC = entered Brake percentageMMI_L_TRAIN = entered LengthMMI_NID_KEY_TRAIN_CAT = entered Train category
            Test Step Comment: (1) MMI_gen 9461 (partly: EVC-10, Flexible Train data);(2) MMI_gen 9461 (partly: open Train data Validation window, touch screen);(3) MMI_gen 8555 (partly: MMI_gen 5215 (partly: Close button, Window title, Input field, No button, Yes button));(4) MMI_gen 8556;(5) MMI_gen 8555 (partly: MMI_gen 5216);(6) MMI_gen 8555 (partly: MMI_gen 7943);(7) MMI_gen 8555 (partly: MMI_gen 5303);(8) MMI_gen 8555 (partly: MMI_gen 5214 (partly: single input field));          (9) MMI_gen 8555 (partly: MMI_gen 5484 (partly: empty)); (10) MMI_gen 8555 (partly: MMI_gen 5214 (partly: dedicated keyboard, MMI_gen 5006), MMI_gen 5006);(11) MMI_gen 8555 (partly: MMI_gen 5263 (partly: MMI_gen 4696));(12) MMI_gen 8555 (partly: MMI_gen 5263 (partly: MMI_gen 4702 (partly: right aligned)));(13) MMI_gen 8555 (partly: MMI_gen 5263 (partly: MMI_gen 4704 (partly: left aligned)));(14) MMI_gen 8557; MMI_gen 8555 (partly: MMI_gen 5263 (partly: MMI_gen 4701 (partly: same order), MMI_gen 4697));(15) MMI_gen 8555 (partly: MMI_gen 5263 (partly: MMI_gen 4698));(16) MMI_gen 8555 (partly: MMI_gen 5263 (partly: MMI_gen 4701 (partly: Main area A, B, C and E));(17) MMI_gen 8555 (partly: MMI_gen 5263 (partly: MMI_gen 4700 (partly: data validation process)));(18) MMI_gen 9462 (partly: Flexible Train data);
            */

            /*
            Test Step 2
            Action: Press ‘No’ button.
            Expected Result: The value of input field is changed refer to selected button.
            Test Step Comment: MMI_gen 8555 (partly: MMI_gen 5484 (partly: filled ‘No’));
            */

            /*
            Test Step 3
            Action: Confirm entered data by pressing an input field.
            Expected Result: Verify the following information,DMI displays Train data window.Use the log file to confirm that DMI sends out the packet [MMI_DRIVER_REQUEST (EVC-101)] with variable [MMI_DRIVER_REQUEST (EVC-101).MMI_M_REQUEST] = 4 (Exit Train data).
            Test Step Comment: (1) MMI_gen 9463 (partly: No button, open Train data window);(2) MMI_gen 9463 (partly: EVC-101, MMI_gen 5724);
            */

            /*
            Test Step 4
            Action: Perform the following procedure,Enter and confirm all data in Train data window.Press ‘Yes’ button.
            Expected Result: DMI displays Train data validation window.
            */

            /*
            Test Step 5
            Action: Press ‘Close’ button.
            Expected Result: Verify the following information,DMI displays Main window.Use the log file to confirm that DMI sends out the packet [MMI_DRIVER_REQUEST (EVC-101)] with variable [MMI_DRIVER_REQUEST (EVC-101).MMI_M_REQUEST] = 4 (Exit Train data).
            Test Step Comment: (1) MMI_gen 9463 (partly: Close button, open Main window);(2) MMI_gen 9463 (partly: EVC-101);
            */

            /*
            Test Step 6
            Action: Perform the following procedure,Press ‘Train data’ button.Enter and confirm all data in Train data window.Press ‘Yes’ button.
            Expected Result: DMI displays Train data validation window.
            */

            /*
            Test Step 7
            Action: Press ‘Yes’ button.
            Expected Result: The value of input field is changed refer to selected button.
            Test Step Comment: MMI_gen 8555 (partly: MMI_gen 5484 (partly: filled ‘Yes’));
            */

            /*
            Test Step 8
            Action: Confirm entered data by pressing an input field.
            Expected Result: DMI displays Train Running Number window.Verify the following information,The Train data validation is closed.Use the log file to confirm that DMI sends out the packet [MMI_CONFIRMED_TRAIN DATA (EVC-110)] with variable based on confirmed data.
            Test Step Comment: (1) MMI_gen 9463 (partly: MMI_gen 5720 (partly: closed));(2) MMI_gen 9464; MMI_gen 9463 (partly: MMI_gen 5720 (partly: ConfirmedData-Packet));
            */

            /*
            Test Step 9
            Action: Perform the following procedure,Enter and confirm ‘Train running number’Press ‘Train data’ button.Enter and confirm all data in Train data window.Press ‘Yes’ button.Then, Simulate loss-communication between ETCS onboard and DMI
            Expected Result: DMI displays Default window with the  message “ATP Down Alarm” and sound alarm.
            */

            /*
            Test Step 10
            Action: Re-establish communication between ETCS onboard and DMI.
            Expected Result: DMI displays Train data validation window.Verify the following informaiton,All buttons except ‘No’ button are disabled.The state of ‘No’ button is enabled.The disabled button are shown as a button is state ‘enabled’ with text label in dark-grey.
            Test Step Comment: (1) MMI_gen 9463 ( partly: MMI_gen 2519 (partly: Train data Validation window, All Request buttons except negative validations));(2) MMI_gen 9463 ( partly: MMI_gen 2519 (partly: Train data Validation window, All negative validations));(3) MMI_gen 9463 ( partly: MMI_gen 1426 (partly: Train data Validation window)); MMI_gen 4377 (partly: shown);
            */

            /*
            Test Step 11
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
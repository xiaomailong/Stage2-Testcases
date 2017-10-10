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
    /// 27.6.6.1
    /// TC-ID: 22.6.6.1
    /// 
    /// This test case verifies the general appearances of Radar validation window based on chapter 6.5.6.3 of requirement specification with packet information sending and receiving.
    /// 
    /// Tested Requirements:
    /// MMI_gen 11792; MMI_gen 11793; MMI_gen 11794 (partly: MMI_gen 5724, MMI_gen 5720; MMI_gen 2519 (partly: Radar validation window); MMI_gen 1426 (partly: Radar validation window)); MMI_gen 11795; MMI_gen 11796; MMI_gen 11797; MMI_gen 11791 (partly: MMI_gen 5215 (partly: Close button, Window title, Input field, No button, Yes button), MMI_gen 5216, MMI_gen 7943, window on half grid array, MMI_gen 5214, MMI_gen 5006, MMI_gen 5484, MMI_gen 5263 (partly: MMI_gen 4696, MMI_gen 4697, MMI_gen 4698, MMI_gen 4700 (partly: data validation process), MMI_gen 4702 (partly: right aligned), MMI_gen 4704 (partly: left aligned), partly: MMI_gen 4701), MMI_gen 5303); MMI_gen 4392 (partly: [Close] NA11, returning to the parent window, [Enter], touch screen); MMI_gen 4350; MMI_gen 4351; MMI_gen 4353; MMI_gen 9390 (partly: Radar Validation window); MMI_gen 4377 (partly: shown);
    /// 
    /// Scenario:
    /// Enter and confirm all data in Radar window. Then, verify the display information and received packet data EVC-41.Press ‘No’ button and verify that the value of an input field is changed refer to pressed button.Confirm entered data by pressing an input field. Then, verify that DMI closes Radar Validation window and openss Radar window with sending out packet EVC-101.Open Radar window. Then, enter and confirm all data.Press ‘Close’ button. Then, verify that DMI closes Radar Validation window and opens Brake window with sending out packet EVC-101.Open Radar window. Then, enter and confirm all data.Press ‘Yes’ button and verify that the value of an input field is changed refer to pressed button.Confirm entered data by pressing an input field. Then, verify that DMI closes Radar Validation window and open Brake window with sending out packet EVC-141.Simulate loss-communication between ETCS and DMI. Then, re-establish communication and verify the state of buttons in Radar Validation window.
    /// 
    /// Used files:
    /// N/A
    /// </summary>
    public class TC_ID_27_6_6_1_Radar_validation_window : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:

            // Call the TestCaseBase PreExecution
            base.PreExecution();

            // The Maintenance password in tag name ‘PASS_CODE_MTN’ of the configuration file is set correctly refer to MMI_gen 11722Test system is power on.Cabin is activated.Settings window is opened. 
            // ?? Maintenance window is opened.Radar window is opened.
            DmiActions.Start_ATP();
            DmiActions.Activate_Cabin_1(this);
            DmiActions.Set_Driver_ID(this, "1234");
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = 4;  // settings window
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.EnableDoppler;
            EVC30_MMIRequestEnable.Send();
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
            Action: Enter and confirm all data in Radar window
            Expected Result: Verify the following information,Use the log file to confirm that DMI received the packet MMI_ECHOED_MAINTENANCE_DATA (EVC-41) with variable MMI_Q_MD_DATASET = 1. Use the log file to confirm that the following variables in packet EVC-41 are same as the entered data,MMI_M_PULSE_PER_KM_1 = entered radar 1DMI displays Radar Validation window.The following objects are displayed in Radar Validation window. Enabled Close button (NA11)Window TitleInput fieldYes buttonNo buttonWindow TitleThe window title is ‘Validate radar’.The window title is right aligned.LayerThe window is displayed in main area A/B/C/D/E/F/G.All areas of Data validation window are Layer 0.Input fieldThe window contains a single input field which have only data area.The value of input field is empty.KeyboardThe displayed keyboard type is dedicated keyboard which contain only ‘Yes’ and ‘No’ button.The key #7 is No button.The key #8 is Yes button.Echo TextEcho Text is composed of a Label part and Data part.The Label of echo text is right aligned.The Data part of echo text is left aligned.The order of echo texts are same as of the Radar window as follows,Radar 1 (mm)Radar 2 (mm)The data part of echo texts is displayed the data value same as of the Radar window.The echo texts are located in Main area A,B,C and E.The colour of echo texts is white.General property of windowThe Radar Validation window is presented with objects, text messages and buttons which is the one of several levels and allocated to areas of DMI. All objects, text messages and buttons are presented within the same layer.The Default window is not displayed and covered the current window
            Test Step Comment: (1) MMI_gen 11792 (partly: EVC-41);(2) MMI_gen 11797;(3) MMI_gen 11792 (partly: open Radar Validation window, touch screen);(4) MMI_gen 11791 (partly: MMI_gen 5215 (partly: Close button, Window title, Input field, No button, Yes button)); MMI_gen 4392 (partly: [Close] NA11);(5) MMI_gen 11795;(6) MMI_gen 11791 (partly: MMI_gen 5216);(7) MMI_gen 11791 (partly: MMI_gen 7943);(8) MMI_gen 11791 (partly: MMI_gen 5303);(9) MMI_gen 11791 (partly: MMI_gen 5214 (partly: single input field));          (10) MMI_gen 11791 (partly: MMI_gen 5484 (partly: empty)); (11) MMI_gen 11791 (partly: MMI_gen 5214 (partly: dedicated keyboard, MMI_gen 5006), MMI_gen 5006);(12) MMI_gen 11791 (partly: MMI_gen 5263 (partly: MMI_gen 4696));(13) MMI_gen 11791 (partly: MMI_gen 5263 (partly: MMI_gen 4702 (partly: right aligned)));(14) MMI_gen 11791 (partly: MMI_gen 5263 (partly: MMI_gen 4704 (partly: left aligned)));(15) MMI_gen 11796;                  MMI_gen 11791 (partly: MMI_gen 5263 (partly: MMI_gen 4701 (partly: same order), MMI_gen 4697));(16) MMI_gen 11791 (partly: MMI_gen 5263 (partly: MMI_gen 4698));(17) MMI_gen 11791 (partly: MMI_gen 5263 (partly: MMI_gen 4701 (partly: Main area A, B, C and E));(18) MMI_gen 11791 (partly: MMI_gen 5263 (partly: MMI_gen 4700 (partly: data validation process)));(19) MMI_gen 4350;(20) MMI_gen 4351;(21) MMI_gen 4353;
            */
            DmiActions.ShowInstruction(this, "Press the ‘Radar’ button to open the Radar window, then enter and confirm all data");

            EVC40_MMICurrentMaintenanceData.MMI_Q_MD_DATASET = Variables.MMI_Q_MD_DATASET.Doppler;
            EVC40_MMICurrentMaintenanceData.MMI_M_PULSE_PER_KM_1 = (Variables.MMI_M_PULSE_PER_KM)20001;
            EVC40_MMICurrentMaintenanceData.MMI_M_PULSE_PER_KM_2 = (Variables.MMI_M_PULSE_PER_KM)20001;
            EVC40_MMICurrentMaintenanceData.Send();
            
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. DMI displays the Radar validation window with the title ‘Validate radar’, rightaligned." + Environment.NewLine +
                                @"2. The Radar Validation window displays an enabled Close button(symbol NA11), a data input field, a ‘Yes’ button and a ‘No’ button." + Environment.NewLine +
                                "3. The window is displayed in areas A, B, C, D, E, F and G." + Environment.NewLine +
                                "4. All areas of the data validation window are Layer 0." + Environment.NewLine +                              
                                "5. The data input field only has a data area." + Environment.NewLine +
                                "6. The data input field is blank." + Environment.NewLine +
                                "7. The window displays a dedicated keypad type which contain only a ‘Yes’ button (key #7) and a ‘No’ button (key #8)." + Environment.NewLine +
                                "8. Echo texts are displayed in white and comprise a Label part, right-aligned and a Data part, left-aligned." + Environment.NewLine +
                                "9. Echo texts are in the same order as in the Radar window (Radar 1(mm), Radar 2(mm))." + Environment.NewLine +
                                "10. Echo texts data parts displays the same values as in the Radar window." + Environment.NewLine +
                                "11. Echo texts are displayed in areas A, B, C and E." + Environment.NewLine +
                                "12. Objects and buttons can be displayed in several levels. Within a level they are allocated to areas." + Environment.NewLine +
                                "13. All objects, text messages and buttons are presented within the same layer" + Environment.NewLine +
                                "14. The Default window is not displayed covering the current window.");
            
            /*
            Test Step 2
            Action: Press ‘No’ button
            Expected Result: The value of input field is changed refer to selected button
            Test Step Comment: MMI_gen 11791 (partly: MMI_gen 5484 (partly: filled ‘No’));
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press the ‘No’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The data input field displays ‘No’.");

            /*
            Test Step 3
            Action: Press and hold an input field
            Expected Result: Verify the following information,(1)    The state of an input field is changed to ‘Pressed’, the border of button is removed
            Test Step Comment: (1) MMI_gen 9390 (partly: Radar Validation window);
            */
            DmiActions.ShowInstruction(this, @"Press and hold the data input field");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The data input field is displayed pressed, without a border.");

            /*
            Test Step 4
            Action: Slide out an input field
            Expected Result: Verify the following information,(1)    The state of an input field is changed to ‘Enabled, the border of button is shown without a sound
            Test Step Comment: (1) MMI_gen 9390 (partly: Radar Validation window);
            */
            DmiActions.ShowInstruction(this, "Whilst keeping the data input field pressed, drag it out of its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The data input field is displayed enabled, with a border." + Environment.NewLine +
                                "2. No sound is played.");

            /*
            Test Step 5
            Action: Slide back into an input field
            Expected Result: Verify the following information,(1)    The state of an input field is changed to ‘Pressed’, the border of button is removed
            Test Step Comment: (1) MMI_gen 9390 (partly: Radar Validation window);
            */
            DmiActions.ShowInstruction(this, "Whilst keeping the data input field pressed, drag it back inside its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The data input field is displayed pressed, without a border.");
            
            /*
            Test Step 6
            Action: Release the pressed area
            Expected Result: Verify the following information,DMI displays Maintenance window.Use the log file to confirm that DMI sends out the packet [MMI_DRIVER_REQUEST (EVC-101)] with variable [MMI_DRIVER_REQUEST (EVC-101).MMI_M_REQUEST] = 54 (Exit Maintenance)
            Test Step Comment: (1) MMI_gen 11794 (partly: No button, open Maintenance window);(2) MMI_gen 11794 (partly: EVC-101, MMI_gen 5724);  MMI_gen 4392 (partly: [Enter], touch screen); MMI_gen 9390 (partly: Radar Validation window);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Release the data input field");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Maintenance window");

            /*
            Test Step 7
            Action: Perform the following procedure,Press ‘Radar’ button.Enter and confirm all data in Radar window.Press ‘Yes’ button
            Expected Result: DMI displays Radar validation window
            */
            DmiActions.ShowInstruction(this, "Press the ‘Radar’ button");

            EVC40_MMICurrentMaintenanceData.MMI_Q_MD_DATASET = Variables.MMI_Q_MD_DATASET.Doppler;
            EVC40_MMICurrentMaintenanceData.MMI_M_PULSE_PER_KM_1 = (Variables.MMI_M_PULSE_PER_KM)20001;
            EVC40_MMICurrentMaintenanceData.MMI_M_PULSE_PER_KM_2 = (Variables.MMI_M_PULSE_PER_KM)20001;
            EVC40_MMICurrentMaintenanceData.Send();

            DmiActions.ShowInstruction(this, "Confirm all data, then press the ‘Yes’ button");

            // Open the Radar validation window
            EVC41_MMIEchoedMaintenanceData.MMI_Q_MD_DATASET_ = Variables.MMI_Q_MD_DATASET.Doppler;
            EVC41_MMIEchoedMaintenanceData.MMI_M_PULSE_PER_KM_1_ = (Variables.MMI_M_PULSE_PER_KM)20001;
            EVC41_MMIEchoedMaintenanceData.MMI_M_PULSE_PER_KM_2_ = (Variables.MMI_M_PULSE_PER_KM)20001;
            EVC41_MMIEchoedMaintenanceData.Send();
            EVC41_MMIEchoedMaintenanceData.MMI_Q_MD_DATASET_ = Variables.MMI_Q_MD_DATASET.Doppler;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. DMI displays the Radar validation window");

            /*
            Test Step 8
            Action: Press ‘Close’ button
            Expected Result: Verify the following information,DMI displays Maintenance window.Use the log file to confirm that DMI sends out the packet [MMI_DRIVER_REQUEST (EVC-101)] with variable ;[MMI_DRIVER_REQUEST (EVC-101).MMI_M_REQUEST] = 54 (Exit Maintenance)
            Test Step Comment: (1) MMI_gen 11794 (partly: Close button, open Maintenance window); MMI_gen 4392 (partly: returning to the parent window);(2) MMI_gen 11794 (partly: EVC-101); 
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button");

            EVC101_MMIDriverRequest.CheckMRequestReleased = Variables.MMI_M_REQUEST.ExitMaintenance;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. DMI displays the Maintenance window");

            /*
            Test Step 9
            Action: Perform the following procedure,Press ‘Radar’ button.Enter and confirm all data in Radar window.Press ‘Yes’ button
            Expected Result: DMI displays Radar validation window
            */
            DmiActions.ShowInstruction(this, "Press the ‘Radar’ button");

            EVC40_MMICurrentMaintenanceData.MMI_Q_MD_DATASET = Variables.MMI_Q_MD_DATASET.Doppler;
            EVC40_MMICurrentMaintenanceData.MMI_M_PULSE_PER_KM_1 = (Variables.MMI_M_PULSE_PER_KM)20001;
            EVC40_MMICurrentMaintenanceData.MMI_M_PULSE_PER_KM_2 = (Variables.MMI_M_PULSE_PER_KM)20001;
            EVC40_MMICurrentMaintenanceData.Send();

            DmiActions.ShowInstruction(this, "Confirm all data, then press the ‘Yes’ button");

            EVC41_MMIEchoedMaintenanceData.MMI_Q_MD_DATASET_ = Variables.MMI_Q_MD_DATASET.Doppler;
            EVC41_MMIEchoedMaintenanceData.MMI_M_PULSE_PER_KM_1_ = (Variables.MMI_M_PULSE_PER_KM)20001;
            EVC41_MMIEchoedMaintenanceData.MMI_M_PULSE_PER_KM_2_ = (Variables.MMI_M_PULSE_PER_KM)20001;
            EVC41_MMIEchoedMaintenanceData.Send();
            EVC41_MMIEchoedMaintenanceData.MMI_Q_MD_DATASET_ = Variables.MMI_Q_MD_DATASET.Doppler;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. DMI displays the Radar validation window");

            /*
            Test Step 10
            Action: Press ‘Yes’ button
            Expected Result: The value of input field is changed refer to selected button
            Test Step Comment: MMI_gen 11791 (partly: MMI_gen 5484 (partly: filled ‘Yes’));
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Yes’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The data input field displays ‘Yes’.");

            /*
            Test Step 11
            Action: Confirm entered data by pressing an input field
            Expected Result: Verify the following information,The Radar validation is closed.DMI displays Maintenance window.Use the log file to confirm that DMI sends out the packet [MMI_CONFIRMED_MAINTENANCE_DATA (EVC-141)] with variable based on confirmed data
            Test Step Comment: (1) MMI_gen 11793 (partly: closure); MMI_gen 11794 (partly: MMI_gen 5720 (partly: closed));(2) MMI_gen 11793 (partly: open Maintenance window);(3) MMI_gen 11793 (partly: EVC-141); MMI_gen 11794 (partly: MMI_gen 5720 (partly: ConfirmedData-Packet));
            */
            DmiActions.ShowInstruction(this, @"Press the data input field to confirm the data entered");

            //EVC141_MMI_NewMaintenanceData.CheckQMdDataset = 1;
            //EVC141_MMI_NewMaintenanceData.CheckMPulsePerKm1 = (Variables.MMI_M_PULSE_PER_KM)20001;
            //EVC141_MMI_NewMaintenanceData.CheckMPulsePerKm2 = (Variables.MMI_M_PULSE_PER_KM)20001;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI closes the data validation window and displays the Maintenance window.");
            /*
            Test Step 12
            Action: Perform the following procedure,Press ‘Radar’ button.Enter and confirm all data in Radar window.Press ‘Yes’ button.Then, Simulate loss-communication between ETCS onboard and DMI
            Expected Result: DMI displays Default window with the  message “ATP Down Alarm” and sound alarm
            */
            DmiActions.ShowInstruction(this, "Press the ‘Radar’ button");

            EVC40_MMICurrentMaintenanceData.MMI_Q_MD_DATASET = Variables.MMI_Q_MD_DATASET.Doppler;
            EVC40_MMICurrentMaintenanceData.MMI_M_PULSE_PER_KM_1 = (Variables.MMI_M_PULSE_PER_KM)20001;
            EVC40_MMICurrentMaintenanceData.MMI_M_PULSE_PER_KM_2 = (Variables.MMI_M_PULSE_PER_KM)20001;
            EVC40_MMICurrentMaintenanceData.Send();

            DmiActions.ShowInstruction(this, "Confirm all data, then press the ‘Yes’ button");

            EVC41_MMIEchoedMaintenanceData.MMI_Q_MD_DATASET_ = Variables.MMI_Q_MD_DATASET.Doppler;
            EVC41_MMIEchoedMaintenanceData.MMI_M_PULSE_PER_KM_1_ = (Variables.MMI_M_PULSE_PER_KM)20001;
            EVC41_MMIEchoedMaintenanceData.MMI_M_PULSE_PER_KM_2_ = (Variables.MMI_M_PULSE_PER_KM)20001;
            EVC41_MMIEchoedMaintenanceData.Send();
            EVC41_MMIEchoedMaintenanceData.MMI_Q_MD_DATASET_ = Variables.MMI_Q_MD_DATASET.Doppler;

            DmiActions.Simulate_communication_loss_EVC_DMI(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. DMI displays the message ‘ATP Down Alarm’." + Environment.NewLine +
                                @"2. The ‘Alarm’ sound is played.");

            /*
            Test Step 13
            Action: Re-establish communication between ETCS onboard and DMI
            Expected Result: Verify the following informaiton,All buttons except ‘No’ button are disabled.The state of ‘No’ button is enabled.The disabled button are shown as a button in state ‘disabled’ with text label in dark-grey
            Test Step Comment: (1) MMI_gen 11794 (partly: MMI_gen 2519 (partly: Radar Validation window, All Request buttons except negative validations));(2) MMI_gen 11794 (partly: MMI_gen 2519 (partly: Radar Validation window, All negative validations));(3) MMI_gen 11794 (partly: MMI_gen 1426 (partly: Radar Validation window)); MMI_gen 4377 (partly: shown);
            */
            // Call generic Action Method
            DmiActions.Re_establish_communication_EVC_DMI(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. All buttons are displayed disabled, with the label text in Dark-grey, except the ‘No’ button." + Environment.NewLine +
                                @"2. The ‘No’ button is displayed enabled.");

            /*
            Test Step 14
            Action: End of test
            Expected Result: 
            */


            return GlobalTestResult;
        }
    }
}
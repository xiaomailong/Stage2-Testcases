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
    /// 27.6.4.1
    /// TC-ID: 22.6.4.1
    /// 
    /// This test case verifies the general appearances of Wheel diameter validation window based on chapter 6.5.6.3 of requirement specification with packet information sending and receiving.
    /// 
    /// Tested Requirements:
    /// MMI_gen 11761; MMI_gen 11762; MMI_gen 11768; MMI_gen 11701; MMI_gen 11764; MMI_gen 11763; MMI_gen 11760 (partly: MMI_gen 5215 (partly: Close button, Window title, Input field, No button, Yes button), MMI_gen 5216, MMI_gen 7943, window on half grid array, MMI_gen 5214, MMI_gen 5006, MMI_gen 5484, MMI_gen 5263 (partly: MMI_gen 4696, MMI_gen 4697, MMI_gen 4698, MMI_gen 4700 (partly: data validation process), MMI_gen 4702 (partly: right aligned), MMI_gen 4704 (partly: left aligned), partly: MMI_gen 4701), MMI_gen 5303); MMI_gen 5724; MMI_gen 5720; MMI_gen 2519 (partly: Wheel diameter validation window); MMI_gen 1426 (partly: Wheel diameter validation window); MMI_gen 4392 (partly: [Close] NA11, returning to the parent window, [Enter], touch screen); MMI_gen 4350; MMI_gen 4351; MMI_gen 4353; MMI_gen 9390 (partly: Wheel Diameter Validation window); MMI_gen 4377 (partly: shown);
    /// 
    /// Scenario:
    /// Enter and confirm all data in Wheel diameter window. Then, verify the display information and received packet data EVC-41.Press ‘No’ button and verify that the value of an input field is changed refer to pressed button.Confirm entered data by pressing an input field. Then, verify that DMI closess Wheel diameter Validation window and opens Wheel diameter window with sending out packet EVC-101.Open Wheel diameter window. Then, enter and confirm all data.Press ‘Close’ button. Then, verify that DMI closes Wheel diameter Validation window and opens Brake window with sending out packet EVC-101.Open Wheel diameter window. Then, enter and confirm all data.Press ‘Yes’ button and verify that the value of an input field is changed refer to pressed button.Confirm entered data by pressing an input field. Then, verify that DMI closes Wheel diameter Validation window and opens Brake window with sending out packet EVC-141.Simulate loss-communication between ETCS and DMI. Then, re-establish communication and verify the state of buttons in Wheel diameter Validation window.
    /// 
    /// Used files:
    /// N/A
    /// </summary>
    public class TC_ID_22_6_4_1_Wheel_diameter : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // Call the TestCaseBase PreExecution
            base.PreExecution();

            // The Maintenance password in tag name ‘PASS_CODE_MTN’ of the configuration file is set correctly refer to MMI_gen 11722Test system is power on.Cabin is activated.Settings window is opened.Maintenance window is opened.Wheel diameter window is opened.
            DmiActions.Start_ATP();
            DmiActions.Activate_Cabin_1(this);
            DmiActions.Set_Driver_ID(this, "1234");
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = 4;  // settings window
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.EnableWheelDiameter;
            EVC30_MMIRequestEnable.Send();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in SB mode

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint

            /*
            Test Step 1
            Action: Enter and confirm all data in Wheel diameter window
            Expected Result: Verify the following information,Use the log file to confirm that DMI received the packet MMI_ECHOED_MAINTENANCE_DATA (EVC-41) with variable MMI_Q_MD_DATASET = 0. Use the log file to confirm that the following variables in packet EVC-41 are same as entered data,MMI_M_SDU_WHEEL_SIZE_1 = entered Wheel diameter1MMI_M_SDU_WHEEL_SIZE_2 = entered Wheel diameter2MMI_M_WHEEL_SIZE_ERR = entered AccuracyDMI displays Wheel diameter Validation window.The following objects are displayed in Wheel diameter Validation window. Enabled Close button (NA11)Window TitleInput fieldYes buttonNo buttonWindow TitleThe window title is ‘Validate Wheel diameter’.The window title is right aligned.LayerThe window is displayed in main area A/B/C/D/E/F/G.All areas of Data validation window are Layer 0.Input fieldThe window contains a single input field which have only data area.The value of input field is empty.KeyboardThe displayed keyboard type is dedicated keyboard which contain only ‘Yes’ and ‘No’ button.The key #7 is No button.The key #8 is Yes button.Echo TextEcho Text is composed of a Label part and Data part.The Label of echo text is right aligned.The Data part of echo text is left aligned.The order of echo texts are same as of the Wheel diameter window as follows,Wheel diameter 1 (mm)Wheel diameter 2 (mm)Accuracy (mm)The data part of echo texts are display the data value same as of the Wheel diameter window.The echo texts are located in Main area A,B,C and E.The colour of echo texts is white.General property of windowThe Wheel diameter Validation window is presented with objects, text messages and buttons which is the one of several levels and allocated to areas of DMI. All objects, text messages and buttons are presented within the same layer.The Default window is not displayed and covered the current window
            Test Step Comment: (1) MMI_gen 11761 (partly: EVC-41);(2) MMI_gen 11763;(3) MMI_gen 11761 (partly: open Wheel diameter Validation window, touch screen);(4) MMI_gen 11760 (partly: MMI_gen 5215 (partly: Close button, Window title, Input field, No button, Yes button)); MMI_gen 4392 (partly: [Close] NA11);(5) MMI_gen 11701;(6) MMI_gen 11760 (partly: MMI_gen 5216);(7) MMI_gen 11760 (partly: MMI_gen 7943);(8) MMI_gen 11760 (partly: MMI_gen 5303);(9) MMI_gen 11760 (partly: MMI_gen 5214 (partly: single input field));          (10) MMI_gen 11760 (partly: MMI_gen 5484 (partly: empty)); (11) MMI_gen 11760 (partly: MMI_gen 5214 (partly: dedicated keyboard, MMI_gen 5006), MMI_gen 5006);(12) MMI_gen 11760 (partly: MMI_gen 5263 (partly: MMI_gen 4696));(13) MMI_gen 11760 (partly: MMI_gen 5263 (partly: MMI_gen 4702 (partly: right aligned)));(14) MMI_gen 11760 (partly: MMI_gen 5263 (partly: MMI_gen 4704 (partly: left aligned)));(15) MMI_gen 11764;                  MMI_gen 11760 (partly: MMI_gen 5263 (partly: MMI_gen 4701 (partly: same order), MMI_gen 4697));(16) MMI_gen 11760 (partly: MMI_gen 5263 (partly: MMI_gen 4698));(17) MMI_gen 11760 (partly: MMI_gen 5263 (partly: MMI_gen 4701 (partly: Main area A, B, C and E));(18) MMI_gen 11760 (partly: MMI_gen 5263 (partly: MMI_gen 4700 (partly: data validation process)));(19) MMI_gen 4350;(20) MMI_gen 4351;(21) MMI_gen 4353;
            */
            DmiActions.ShowInstruction(this, "Press the ‘Maintenance’ button and enter the password ‘26728290’ in the Password Maintenance window, then press the ‘Wheel Diameter’ button");

            // spec says this but EVC40 opens the Wheel diameter window
            EVC40_MMICurrentMaintenanceData.MMI_Q_MD_DATASET = Variables.MMI_Q_MD_DATASET.WheelDiameter;
            EVC40_MMICurrentMaintenanceData.Send();

            DmiActions.ShowInstruction(this, "Using the numeric keypad enter a value for Wheel Diameter 1 and press its data input field" + Environment.NewLine +
                                             "Enter a value for Wheel Diameter 2 and press its data input field" + Environment.NewLine +
                                             "Enter a value for Accuracy and press its data input field");

            // The spec says this:
            // (2) Use the log file to confirm that the following variables in packet EVC-41 are same as entered data,
            //	MMI_M_SDU_WHEEL_SIZE_1 = entered Wheel diameter1
            //	MMI_M_SDU_WHEEL_SIZE_2 = entered Wheel diameter2
            //	MMI_M_WHEEL_SIZE_ERR = entered Accuracy
            // EVC-140 is sent out when the data are accepted. How do you know what the values are unless they were specified to be entered?

            // Open the wheel diameter validation window
            EVC41_MMIEchoedMaintenanceData.MMI_Q_MD_DATASET_ = Variables.MMI_Q_MD_DATASET.WheelDiameter;
            EVC41_MMIEchoedMaintenanceData.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Wheel Diameter Validation window comprising:" + Environment.NewLine +
                                @"   an enabled ‘Close’ button, symbol NA11, a title, an input field, a ‘Yes’ button and a ‘No’ button." +
                                @"2. The window title is ‘Validate Wheel diameter, right-aligned’." + Environment.NewLine +
                                "3. The window is displayed in areas A, B, C, D, E, F and G." + Environment.NewLine +
                                "4. All data validation areas are in Layer 0." + Environment.NewLine +
                                "5. The window displays one data input field which only has a (blank) data part." + Environment.NewLine +
                                @"6. The window has an associated keypad displaying a ‘Yes’ button (at key #7) and a ‘No’ button (at key #8)." + Environment.NewLine +
                                "7. Echo texts are displayed with values in white, corresponding to the (three) values previously entered (Wheel diameter 1, Wheel diameter 2 and Accuracy), and in the same order." + Environment.NewLine +
                                "8. Each echo text has a label, right-aligned and a data part, left-aligned." + Environment.NewLine +
                                "9. Echo texts are displayed in areas A, B, C and E." + Environment.NewLine +
                                "10. Objects, text messages and buttons can be displayed in several levels. Within a level they are allocated to areas." + Environment.NewLine +
                                "11. Objects, text messages and buttons in a layer form a window." + Environment.NewLine +
                                "12. The Default window is not displayed covering the current window.");
            
            /*
            Test Step 2
            Action: Press ‘No’ button
            Expected Result: The value of input field is changed refer to selected button
            Test Step Comment: MMI_gen 11760 (partly: MMI_gen 5484 (partly: filled ‘No’));
            */
            DmiActions.ShowInstruction(this, @"Press the ‘No’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The data input field displays the value of the (currently) selected echo text");

            /*
            Test Step 3
            Action: Press and hold an input field
            Expected Result: Verify the following information,(1)    The state of an input field is changed to ‘Pressed’, the border of button is removed
            Test Step Comment: (1) MMI_gen 9390 (partly: Wheel Diameter Validation window);
            */
            DmiActions.ShowInstruction(this, "Press and hold the data input field");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The data input field is displayed pressed, without a border.");

            /*
            Test Step 4
            Action: Slide out an input field
            Expected Result: Verify the following information,(1)    The state of an input field is changed to ‘Enabled, the border of button is shown without a sound
            Test Step Comment: (1) MMI_gen 9390 (partly: Wheel Diameter Validation window);
            */
            DmiActions.ShowInstruction(this, "Whilst keeping the data input field pressed, drag it out of its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The data input field is displayed enabled, with a border." + Environment.NewLine +
                                "2. No sound is played.");

            /*
            Test Step 5
            Action: Slide back into an input field
            Expected Result: Verify the following information,(1)    The state of an input field is changed to ‘Pressed’, the border of button is removed
            Test Step Comment: (1) MMI_gen 9390 (partly: Wheel Diameter Validation window);
            */
            DmiActions.ShowInstruction(this, "Whilst keeping the data input field pressed, drag it back inside its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The data input field is displayed pressed, without a border.");

            /*
            Test Step 6
            Action: Release the pressed area
            Expected Result: Verify the following information,DMI displays Maintenance window.Use the log file to confirm that DMI sends out the packet [MMI_DRIVER_REQUEST (EVC-101)] with variable ;[MMI_DRIVER_REQUEST (EVC-101).MMI_M_REQUEST] = 54 (Exit Maintenance)
            Test Step Comment: (1) MMI_gen 11768 (partly: No button, open Maintenance window);(2) MMI_gen 11768 (partly: EVC-101); MMI_gen 5724; MMI_gen 4392 (partly: [Enter], touch screen); MMI_gen 9390 (partly: Wheel Diameter Validation window);
            */
            DmiActions.ShowInstruction(this, "Release the data input field");

            EVC101_MMIDriverRequest.CheckMRequestReleased = Variables.MMI_M_REQUEST.ExitMaintenance;

            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = 4;  // settings window
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.EnableWheelDiameter;
            EVC30_MMIRequestEnable.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Maintenance window.");


            /*
            Test Step 7
            Action: Perform the following procedure,Press ‘Wheel diameter’ button.Enter and confirm all data in Wheel diameter window.Press ‘Yes’ button
            Expected Result: DMI displays Wheel diameter validation window
            */
            DmiActions.ShowInstruction(this, "Press the ‘Maintenance’ button and enter the password ‘26728290’ in the Password Maintenance window, then press the ‘Wheel Diameter’ button");

            // spec says this but EVC40 opens the Wheel diameter window
            EVC40_MMICurrentMaintenanceData.MMI_Q_MD_DATASET = Variables.MMI_Q_MD_DATASET.WheelDiameter;
            EVC40_MMICurrentMaintenanceData.Send();

            DmiActions.ShowInstruction(this, "Using the numeric keypad enter a value for Wheel Diameter 1 and press its data input field" + Environment.NewLine +
                                             "Enter a value for Wheel Diameter 2 and press its data input field" + Environment.NewLine +
                                             "Enter a value for Accuracy and press its data input field" + Environment.NewLine +
                                             @"Press the ‘Yes’ button");
            
            EVC41_MMIEchoedMaintenanceData.MMI_Q_MD_DATASET_ = Variables.MMI_Q_MD_DATASET.WheelDiameter;
            EVC41_MMIEchoedMaintenanceData.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Wheel diameter validation window.");

            /*
            Test Step 8
            Action: Press ‘Close’ button
            Expected Result: Verify the following information,DMI displays Maintenance window.Use the log file to confirm that DMI sends out the packet [MMI_DRIVER_REQUEST (EVC-101)] with variable ;[MMI_DRIVER_REQUEST (EVC-101).MMI_M_REQUEST] = 54 (Exit Maintenance)
            Test Step Comment: (1) MMI_gen 11768 (partly: Close button, open Maintenance window); MMI_gen 4392 (partly: returning to the parent window);(2) MMI_gen 11768 (partly: EVC-101); 
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button");

            EVC101_MMIDriverRequest.CheckMRequestReleased = Variables.MMI_M_REQUEST.ExitMaintenance;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Maintenance window.");

            /*
            Test Step 9
            Action: Perform the following procedure,Press ‘Wheel diameter’ button.Enter and confirm all data in Wheel diameter window.Press ‘Yes’ button
            Expected Result: DMI displays Wheel diameter validation window
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Wheel diameter’ button. Enter and confirm all data in the Wheel diameter window, then press the ‘Yes’ button");

            // Open the wheel diameter validation window
            EVC41_MMIEchoedMaintenanceData.MMI_Q_MD_DATASET_ = Variables.MMI_Q_MD_DATASET.WheelDiameter;
            EVC41_MMIEchoedMaintenanceData.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Wheel diameter validation window.");
            
            /*
            Test Step 10
            Action: Press ‘Yes’ button
            Expected Result: The value of input field is changed refer to selected button
            Test Step Comment: MMI_gen 11760 (partly: MMI_gen 5484 (partly: filled ‘Yes’));
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press the ‘Yes’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The data input field displays the value of the (currently) selected echo text");

            /*
            Test Step 11
            Action: Confirm entered data by pressing an input field
            Expected Result: Verify the following information,The Wheel diameter validation is closed.DMI displays Maintenance window.Use the log file to confirm that DMI sends out the packet [MMI_CONFIRMED_MAINTENANCE_DATA (EVC-141)] with variable based on confirmed data
            Test Step Comment: (1) MMI_gen 11762 (partly: closure); MMI_gen 5720 (partly: closed);(2) MMI_gen 11762 (partly: open Maintenance window);(3) MMI_gen 11762 (partly: EVC-141); MMI_gen 5720 (partly: ConfirmedData-Packet);
            */
            DmiActions.ShowInstruction(this, @"Press on the data input field to confirm the entered data");

            //EVC141_MMIConfirmedMaintenanceData.CheckMRequestReleased = Variables.MMI_M_REQUEST.ExitMaintenance;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI closes the Wheel diameter validation window and displays the Maintenance window.");

            /*
            Test Step 12
            Action: Perform the following procedure,Press ‘Wheel diameter’ button.Enter and confirm all data in Wheel diameter window.Press ‘Yes’ button.Then, Simulate loss-communication between ETCS onboard and DMI
            Expected Result: DMI displays Default window with the  message “ATP Down Alarm” and sound alarm
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Wheel diameter’ button. Enter and confirm all data in the Wheel diameter window, then press the ‘Yes’ button.");

            DmiActions.Simulate_communication_loss_EVC_DMI(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. DMI displays the Default window with the message ‘ATPDown Alarm’." + Environment.NewLine +
                                "2. An alarm sound is played.");

            /*
            Test Step 13
            Action: Re-establish communication between ETCS onboard and DMI
            Expected Result: Verify the following information,All buttons except ‘No’ button are disabled.The state of ‘No’ button is enabled.The disabled button are shown as a button is state ‘disabled’ with text label in dark-grey
            Test Step Comment: (1) MMI_gen 2519 (partly: Wheel diameter Validation window, All Request buttons except negative validations);(2) MMI_gen 2519 (partly: Wheel diameter Validation window, All negative validations);(3) MMI_gen 1426 (partly: Wheel diameter Validation window); MMI_gen 4377 (partly: shown);
            */
            DmiActions.Re_establish_communication_EVC_DMI(this);

            // Open the wheel diameter validation window
            EVC41_MMIEchoedMaintenanceData.MMI_Q_MD_DATASET_ = Variables.MMI_Q_MD_DATASET.WheelDiameter;
            EVC41_MMIEchoedMaintenanceData.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. All buttons except the ‘No’ button are disabled." + Environment.NewLine +
                                "2. The ‘No’ button is displayed enabled." + Environment.NewLine +
                                "3. Disabled buttons have Dark-grey labels.");

            /*
            Test Step 14
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
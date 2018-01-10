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
    /// 25.4 Driver’s Action: Settings window
    /// TC-ID: 20.4
    /// 
    /// This test case verify that DMI sends values of [MMI_DRIVER_REQUEST (EVC-101).MMI_M_REQUEST] correctly when a driver presses on each button in Settings window.
    /// 
    /// Tested Requirements:
    /// MMI_gen 151 (partly: MMI_M_REQUEST = 58, 55, 29, 22, 23, 25, 24, 26, 53, 54, 52, 51, 60); MMI_gen 1088 ( partly, Bit # 29 to 31);
    /// 
    /// Scenario:
    /// 1.Perform the specified action (e.g. open/close window, press an acknowledgement). Then, verify the value in packet EVC-101 refer to each action.
    /// 2.Enter VBC code to enable ‘Remove VBC’ button.
    /// 3.Open and Close the Remove VBC window. Then, verify the value in packet EVC-101 refer to each action.
    /// 4.Open the Maintenance window. Then, perform the specified action and verfiy the value in packet EVC-101 refer to each action.
    /// 5.Use the test script file to enable 'Brake percentage' button. Then, open and close the Brake percentage window to verify the value in packet EVC-101.
    /// 
    /// Used files:
    /// 20_4_a.xml
    /// </summary>
    public class TC_ID_20_4_Drivers_Action_Settings_window : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:

            // Call the TestCaseBase PreExecution
            base.PreExecution();

            // Test system is powered on.Cabin is activated.
            DmiActions.Start_ATP();
            DmiActions.Activate_Cabin_1(this);
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
            Action: Perform the following procedure,a)   Press the ‘Settings’ button.b)  Press the ‘System version’ button.c)   Press the ‘Close’ button. Then, press the ‘System info’ button.d)   Press the ‘Close’ button. Then, press the ‘Brake’ button and ‘Brake test’ button respectively.e)   Press the ‘Close’ button. Then, press the ‘Set VBC’ button.f)    Press the ‘Close’ button
            Expected Result: Verify the following information(1)   Use the log file to confirm that DMI sends out packet [MMI_DRIVER_REQUEST (EVC-101)] with the value of variable MMI_M_REQUEST refer to sequence below,a)   MMI_M_REQUEST = 58 (Settings)b)   MMI_M_REQUEST = 55 (System version request)c)   MMI_M_REQUEST = 29 (System Info request)d)   MMI_M_REQUEST = 22 (Start Brake Test)e)   MMI_M_REQUEST = 23 (Start Set VBC)f)   MMI_M_REQUEST = 25 (Exit Set VBC)Note: The sequence of MMI_M_REQUEST value are consistent with step of each action.(2)   When the button is pressed in each action, the window of pressed button is closed
            Test Step Comment: (1) MMI_gen 151 (partly: MMI_M_REQUEST = 58, 55, 29, 22, 23, 25) ;(2) MMI_gen 151 (partly: close opened menu);
            */
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Default;
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_LOW = true; // System info enabled
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.SystemVersion |
                                                               EVC30_MMIRequestEnable.EnabledRequests.SetVBC |
                                                               EVC30_MMIRequestEnable.EnabledRequests.StartBrakeTest |
                                                               EVC30_MMIRequestEnable.EnabledRequests
                                                                   .EnableBrakePercentage |
                                                               EVC30_MMIRequestEnable.EnabledRequests.RemoveVBC |
                                                               EVC30_MMIRequestEnable.EnabledRequests
                                                                   .EnableWheelDiameter |
                                                               EVC30_MMIRequestEnable.EnabledRequests.EnableDoppler;
            EVC30_MMIRequestEnable.Send();

            DmiActions.ShowInstruction(this, "Press the ‘Settings’ button");

            EVC101_MMIDriverRequest.CheckMRequestPressed = Variables.MMI_M_REQUEST.Settings;

            DmiActions.ShowInstruction(this, "Press the ‘System version’ button");

            EVC101_MMIDriverRequest.CheckMRequestPressed = Variables.MMI_M_REQUEST.SystemVersionRequest;

            DmiActions.ShowInstruction(this, "Press the ‘Close’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI closes the System version window.");

            DmiActions.ShowInstruction(this, "Press the ‘System info’ button");
            EVC101_MMIDriverRequest.CheckMRequestPressed = Variables.MMI_M_REQUEST.SystemInfoRequest;

            EVC24_MMISystemInfo.MMI_NID_ENGINE_1 = 1234;
            EVC24_MMISystemInfo.MMI_T_TIMEOUT_BRAKE = 0x5695224c; // 1452614220
            EVC24_MMISystemInfo.MMI_T_TIMEOUT_BTM = 0x54b3eecc; // 1421078220
            EVC24_MMISystemInfo.MMI_T_TIMEOUT_TBSW = 0x538b4d4c; // 1401638220
            EVC24_MMISystemInfo.MMI_M_ETC_VER = 0xffaa0f; // 16755215
            EVC24_MMISystemInfo.MMI_M_AVAIL_SERVICES = 0xffff; // 65535 
            EVC24_MMISystemInfo.MMI_M_BRAKE_CONFIG = 55;
            EVC24_MMISystemInfo.MMI_M_LEVEL_INST = 248;
            EVC24_MMISystemInfo.Send();

            DmiActions.ShowInstruction(this, "Press the ‘Close’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI closes the System info window.");

            DmiActions.ShowInstruction(this, "Press the ‘Brake’ button, then press the  ‘Brake test’ button");

            EVC101_MMIDriverRequest.CheckMRequestPressed = Variables.MMI_M_REQUEST.StartBrakeTest;

            DmiActions.ShowInstruction(this, "Press the ‘Close’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI closes the Brake test window.");

            DmiActions.ShowInstruction(this, "Press the ‘Set VBC’ button");

            EVC101_MMIDriverRequest.CheckMRequestPressed = Variables.MMI_M_REQUEST.StartSetVBC;

            EVC18_MMISetVBC.Send();

            DmiActions.ShowInstruction(this, "Press the ‘Close’ button");


            EVC101_MMIDriverRequest.CheckMRequestPressed = Variables.MMI_M_REQUEST.ExitSetVBC;

            EVC18_MMISetVBC.MMI_N_VBC = 1;
            EVC18_MMISetVBC.MMI_Q_DATA_CHECK = Variables.Q_DATA_CHECK.All_checks_passed;
            EVC18_MMISetVBC.ECHO_TEXT = "";
            EVC18_MMISetVBC.MMI_M_BUTTONS = Variables.MMI_M_BUTTONS_VBC.BTN_SETTINGS;
            EVC18_MMISetVBC.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI closes the Set VBC window.");

            /*
            Test Step 2
            Action: Perform the following procedure,Press the ‘Set VBC’ button.Enter and confirm the value ‘65536’ at the input field.Press ‘Yes’ button.Select and confirm ‘Yes’ button at Set VBC validation window.Press the ‘Remove VBC’ button
            Expected Result: Verify the following information(1)   Use the log file to confirm that DMI sends out packet [MMI_DRIVER_REQUEST (EVC-101)] with the value of variable MMI_M_REQUEST = 24 (Start Remove VBC)(2)   The Settings window is closed, DMI displays Remove VBC window
            Test Step Comment: (1) MMI_gen 151 (partly: MMI_M_REQUEST = 24);(2) MMI_gen 151 (partly: close opened menu);
            */
            DmiActions.ShowInstruction(this, "Press the ‘Set VBC’ button");

            EVC18_MMISetVBC.MMI_M_BUTTONS = Variables.MMI_M_BUTTONS_VBC.BTN_YES_DATA_ENTRY_COMPLETE;
            EVC18_MMISetVBC.Send();

            DmiActions.ShowInstruction(this,
                "Enter and confirm the value ‘65536’ in the data input field, then press the ‘Yes’ button");

            EVC28_MMIEchoedSetVBCData.MMI_M_VBC_CODE_ = 65536;
            EVC28_MMIEchoedSetVBCData.Send();

            DmiActions.ShowInstruction(this, "Select and confirm the ‘Yes’ button in the Set VBC validation window");
            EVC18_MMISetVBC.ECHO_TEXT = "";
            EVC18_MMISetVBC.MMI_M_BUTTONS = Variables.MMI_M_BUTTONS_VBC.BTN_SETTINGS;
            EVC18_MMISetVBC.MMI_N_VBC = 1;
            EVC18_MMISetVBC.MMI_Q_DATA_CHECK = Variables.Q_DATA_CHECK.All_checks_passed;
            EVC18_MMISetVBC.Send();

            DmiActions.ShowInstruction(this, "Press the ‘Remove VBC’ button");
            EVC19_MMIRemoveVBC.MMI_N_VBC = 0;
            EVC19_MMIRemoveVBC.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Remove VBC window.");

            /*
            Test Step 3
            Action: Press the ‘Close’ button
            Expected Result: Verify the following information(1)   Use the log file to confirm that DMI sends out packet [MMI_DRIVER_REQUEST (EVC-101)] with the value of variable MMI_M_REQUEST = 26 (Exit  Remove VBC)(2)   The Remove VBC window is closed, DMI displays Settings  window
            Test Step Comment: (1) MMI_gen 151 (partly: MMI_M_REQUEST = 26);(2) MMI_gen 151 (partly: close opened menu);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button");

            EVC101_MMIDriverRequest.CheckMRequestPressed = Variables.MMI_M_REQUEST.ExitRemoveVBC;

            EVC19_MMIRemoveVBC.MMI_M_BUTTONS = Variables.MMI_M_BUTTONS_VBC.BTN_SETTINGS;
            EVC19_MMIRemoveVBC.MMI_N_VBC = 1;
            EVC19_MMIRemoveVBC.MMI_Q_DATA_CHECK = Variables.Q_DATA_CHECK.All_checks_passed;
            EVC19_MMIRemoveVBC.ECHO_TEXT = "";
            EVC19_MMIRemoveVBC.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Settings window.");

            /*
            Test Step 4
            Action: Perform the following procedure,Press ‘Maintenance’ button.Enter the Maintenance window by entering the password same as a value in tag ‘PASS_CODE_MTN’ of the configuration file and confirming the password
            Expected Result: DMI displays Maintenance window
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Maintenance’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Maintenance window.");

            DmiActions.ShowInstruction(this,
                @"Enter the password (as in the ‘PASS_CODE_MTN’ tag of the configuration file), then confirm");

            /*
            Test Step 5
            Action: Perform the following procedure,a)   Press the ‘Wheel diameter’ button.b)   Press the ‘Close’ button.c)   Press the ‘Radar’d)   Press the ‘Close’ button
            Expected Result: Verify the following information(1)   Use the log file to confirm that DMI sends out packet [MMI_DRIVER_REQUEST (EVC-101)] with the value of variable MMI_M_REQUEST refer to sequence below,a)   MMI_M_REQUEST = 53 (Change Wheel Diameter)b)   MMI_M_REQUEST = 54 (Exit Maintenance)c)   MMI_M_REQUEST = 52 (Change Doppler)d)   MMI_M_REQUEST = 54 (Exit Maintenance)Note: The sequence of MMI_M_REQUEST value are consistent with step of each action.(2)   When the button is pressed in each action, the window of pressed button is closed.(3)   Use the log file to confirm that DMI receives packet EVC-30 with the value of following bit of MMI_Q_REQUEST_ENABLE_64Bit #29  = 1 (Enable wheel diameter)Bit #30 = 1 (Enable doppler) Bit #31 = 1 (Enable brake percentage)
            Test Step Comment: (1) MMI_gen 151 (partly: MMI_M_REQUEST = 53, 54, 52);(2) MMI_gen 151 (partly: close opened menu);(3) MMI_gen 1088 (partly, Bit # 29 to 31);Note: The MMI_M_REQUEST = 54 is send according to the MMI_gen 11758 and MMI_gen 11786 which also verified in another test cases.
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Wheel diameter’ button");

            EVC101_MMIDriverRequest.CheckMRequestPressed = Variables.MMI_M_REQUEST.ChangeWheelDiameter;

            EVC40_MMICurrentMaintenanceData.MMI_Q_MD_DATASET = Variables.MMI_Q_MD_DATASET.WheelDiameter;
            EVC40_MMICurrentMaintenanceData.Send();

            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button");

            EVC101_MMIDriverRequest.CheckMRequestPressed = Variables.MMI_M_REQUEST.ExitMaintenance;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI closes the Wheel diameter window.");

            DmiActions.ShowInstruction(this, @"Press the ‘Radar’ button");

            EVC101_MMIDriverRequest.CheckMRequestPressed = Variables.MMI_M_REQUEST.ChangeDoppler;

            EVC40_MMICurrentMaintenanceData.MMI_Q_MD_DATASET = Variables.MMI_Q_MD_DATASET.Doppler;
            EVC40_MMICurrentMaintenanceData.Send();

            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button");

            EVC101_MMIDriverRequest.CheckMRequestPressed = Variables.MMI_M_REQUEST.ExitMaintenance;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI closes the Radar window.");

            /*
            Test Step 6
            Action: Perform the following procedure,Press the 'Brake' button.Use the test script file 20_4_a.xml to send EVC-30 with MMI_NID_WINDOW = 4 and MMI_Q_REQUEST_ENABLE_64 (#31) =1Press the enabled 'Brake percentage' button
            Expected Result: Verify the following information(1)   Use the log file to confirm that DMI sends out packet [MMI_DRIVER_REQUEST (EVC-101)] with the value of variable MMI_M_REQUEST = 51 (Change Brake Percentage)(2)   The Brake window is closed, DMI displays Brake percentage window
            Test Step Comment: (1) MMI_gen 151 (partly: MMI_M_REQUEST = 51);(2) MMI_gen 151 (partly: close opened menu);
            */
            DmiActions.ShowInstruction(this, "Press the ‘Brake’ button");

            EVC101_MMIDriverRequest.CheckMRequestPressed = Variables.MMI_M_REQUEST.StartBrakeTest;

            XML_20_4_a();

            DmiActions.ShowInstruction(this, "Press the ‘Brake percentage’ button");

            EVC50_MMICurrentBrakePercentage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Brake percentage window.");

            /*
            Test Step 7
            Action: Press the ‘Close’ button
            Expected Result: Verify the following information(1)   Use the log file to confirm that DMI sends out packet [MMI_DRIVER_REQUEST (EVC-101)] with the value of variable MMI_M_REQUEST = 60 (Exit Brake Percentage)(2)   The Brake percentage window is closed, DMI displays Brake window
            Test Step Comment: (1) MMI_gen 151 (partly: MMI_M_REQUEST = 60);(2) MMI_gen 151 (partly: close opened menu);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button");

            EVC101_MMIDriverRequest.CheckMRequestPressed = Variables.MMI_M_REQUEST.ExitBrakePercentage;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Brake window.");

            /*
            Test Step 8
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }


        #region Send_XML_20_4_DMI_Test_Specification

        private void XML_20_4_a()
        {
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Settings;
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH =
                EVC30_MMIRequestEnable.EnabledRequests.EnableBrakePercentage;
            EVC30_MMIRequestEnable.Send();
        }

        #endregion
    }
}
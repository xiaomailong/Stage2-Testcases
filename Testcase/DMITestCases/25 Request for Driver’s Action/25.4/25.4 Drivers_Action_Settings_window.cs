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
    public class Drivers_Action_Settings_window : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // Test system is powered on.Cabin is activated.
            
            // Call the TestCaseBase PreExecution
            base.PreExecution();
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
            
            
            /*
            Test Step 2
            Action: Perform the following procedure,Press the ‘Set VBC’ button.Enter and confirm the value ‘65536’ at the input field.Press ‘Yes’ button.Select and confirm ‘Yes’ button at Set VBC validation window.Press the ‘Remove VBC’ button
            Expected Result: Verify the following information(1)   Use the log file to confirm that DMI sends out packet [MMI_DRIVER_REQUEST (EVC-101)] with the value of variable MMI_M_REQUEST = 24 (Start Remove VBC)(2)   The Settings window is closed, DMI displays Remove VBC window
            Test Step Comment: (1) MMI_gen 151 (partly: MMI_M_REQUEST = 24);(2) MMI_gen 151 (partly: close opened menu);
            */
            
            
            /*
            Test Step 3
            Action: Press the ‘Close’ button
            Expected Result: Verify the following information(1)   Use the log file to confirm that DMI sends out packet [MMI_DRIVER_REQUEST (EVC-101)] with the value of variable MMI_M_REQUEST = 26 (Exit  Remove VBC)(2)   The Remove VBC window is closed, DMI displays Settings  window
            Test Step Comment: (1) MMI_gen 151 (partly: MMI_M_REQUEST = 26);(2) MMI_gen 151 (partly: close opened menu);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(@"Press the ‘Close’ button");
            
            
            /*
            Test Step 4
            Action: Perform the following procedure,Press ‘Maintenance’ button.Enter the Maintenance window by entering the password same as a value in tag ‘PASS_CODE_MTN’ of the configuration file and confirming the password
            Expected Result: DMI displays Maintenance window
            */
            // Call generic Action Method
            DmiActions.Perform_the_following_procedure_Press_Maintenance_button_Enter_the_Maintenance_window_by_entering_the_password_same_as_a_value_in_tag_PASS_CODE_MTN_of_the_configuration_file_and_confirming_the_password();
            
            
            /*
            Test Step 5
            Action: Perform the following procedure,a)   Press the ‘Wheel diameter’ button.b)   Press the ‘Close’ button.c)   Press the ‘Radar’d)   Press the ‘Close’ button
            Expected Result: Verify the following information(1)   Use the log file to confirm that DMI sends out packet [MMI_DRIVER_REQUEST (EVC-101)] with the value of variable MMI_M_REQUEST refer to sequence below,a)   MMI_M_REQUEST = 53 (Change Wheel Diameter)b)   MMI_M_REQUEST = 54 (Exit Maintenance)c)   MMI_M_REQUEST = 52 (Change Doppler)d)   MMI_M_REQUEST = 54 (Exit Maintenance)Note: The sequence of MMI_M_REQUEST value are consistent with step of each action.(2)   When the button is pressed in each action, the window of pressed button is closed.(3)   Use the log file to confirm that DMI receives packet EVC-30 with the value of following bit of MMI_Q_REQUEST_ENABLE_64Bit #29  = 1 (Enable wheel diameter)Bit #30 = 1 (Enable doppler) Bit #31 = 1 (Enable brake percentage)
            Test Step Comment: (1) MMI_gen 151 (partly: MMI_M_REQUEST = 53, 54, 52);(2) MMI_gen 151 (partly: close opened menu);(3) MMI_gen 1088 (partly, Bit # 29 to 31);Note: The MMI_M_REQUEST = 54 is send according to the MMI_gen 11758 and MMI_gen 11786 which also verified in another test cases.
            */
            
            
            /*
            Test Step 6
            Action: Perform the following procedure,Press the 'Brake' button.Use the test script file 20_4_a.xml to send EVC-30 with MMI_NID_WINDOW = 4 and MMI_Q_REQUEST_ENABLE_64 (#31) =1Press the enabled 'Brake percentage' button
            Expected Result: Verify the following information(1)   Use the log file to confirm that DMI sends out packet [MMI_DRIVER_REQUEST (EVC-101)] with the value of variable MMI_M_REQUEST = 51 (Change Brake Percentage)(2)   The Brake window is closed, DMI displays Brake percentage window
            Test Step Comment: (1) MMI_gen 151 (partly: MMI_M_REQUEST = 51);(2) MMI_gen 151 (partly: close opened menu);
            */
            
            
            /*
            Test Step 7
            Action: Press the ‘Close’ button
            Expected Result: Verify the following information(1)   Use the log file to confirm that DMI sends out packet [MMI_DRIVER_REQUEST (EVC-101)] with the value of variable MMI_M_REQUEST = 60 (Exit Brake Percentage)(2)   The Brake percentage window is closed, DMI displays Brake window
            Test Step Comment: (1) MMI_gen 151 (partly: MMI_M_REQUEST = 60);(2) MMI_gen 151 (partly: close opened menu);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(@"Press the ‘Close’ button");
            
            
            /*
            Test Step 8
            Action: End of test
            Expected Result: 
            */
            

            return GlobalTestResult;
        }
    }
}

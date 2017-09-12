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
    /// 37.7 Dialogue Sequence of Settings window
    /// TC-ID: 34.7
    /// 
    /// This test case verifies the dialogue sequence of the Settings window. The dialogue sequence shows the interaction with the driver when presses the ‘Settings’ button on the default window. The dialogue sequence of the Settings window shall comply with [ERA-ERTMS] standard and [MMI-ETCS-gen].
    /// 
    /// Tested Requirements:
    /// MMI_gen 9231; MMI_gen 11992; MMI_gen 11993; MMI_gen 11994; MMI_gen 11995; MMI_gen 8785 (partly: Settings, Language, Volumne, Brightness, System version, Set VBC, Set VBC validation, Remove VBC, Remove VBC validation, additional DMI technical function, Maintenance password, Maintenance, Brake, System Info, Set clock);
    /// 
    /// Scenario:
    /// Verifies the state of ‘Close’ button in the following windows,Maintenance password windowMaintenance windowSettings WindowSystem version WindowLanguage WindowVolume WindowBrightness WindowSet VBC WindowValidate Set VBC WindowRemove VBC WindowValidate Remove VBC WindowBrake windowSystem info windowSet Clock windowPress the ‘Close’ button in each window according to scenarion No.1 to verifiy an operation of enabled button and dialogue sequence of Settings window.Simulate Loss-communication between ETCS and DMI. Then, verify the state of the following buttons,SettingsLanguageBrightnessVolumePress on each button according to scenario No. 3 to verify that DMI does not send out packet EVC-101 and EVC-122.Note: This test case is verifies only SB mode Level 
    /// 1.However, tester can use this scenario to verify test result in SB mode for Level 2 and 3 also.
    /// 
    /// Used files:
    /// N/A
    /// </summary>
    public class TC_34_7_Dialogue_Sequences : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:

            // Call the TestCaseBase PreExecution
            base.PreExecution();

            // Test system is power on.Cabin is activated.
            
            EVC0_MMIStartATP.Evc0Type = EVC0_MMIStartATP.EVC0Type.GoToIdle;
            EVC0_MMIStartATP.Send();

            // Set train running number, cab 1 active, and other defaults
            DmiActions.Activate_Cabin_1(this);
            
            
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in SB mode, Level 1
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in SB mode, Level 1.");

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint

            /*
            Test Step 1
            Action: Press ‘Settings’ button
            Expected Result: DMI displays Settings window.Verify the following information,The ‘Close’ button is enabled
            Test Step Comment: (1) MMI_gen 9231 (partly: Settings window);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press the ‘Settings’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Settings window." + Environment.NewLine +
                                "2. The ‘Close’ button is enabled");            

            /*
            Test Step 2
            Action: Press ‘Maintenance’ button
            Expected Result: DMI displays Maintenance password window.Verify the following information,The ‘Close’ button is enabled
            Test Step Comment: (1) MMI_gen 9231 (partly: Maintenance password);   
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press the ‘Maintenance’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Maintenance password window." + Environment.NewLine +
                                "2. The ‘Close’ button is enabled");

            /*
            Test Step 3
            Action: Press ‘Close’ button
            Expected Result: DMI displays Settings window
            Test Step Comment: MMI_gen 8785 (partly: additional DMI technical function, Maintenance password);
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button in the Maintenance Window");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI closes the Maintenance password window and displays the Settings window.");

            /*
            Test Step 4
            Action: Perform the following procedure,Press ‘Maintenance’ button.Enter the Maintenance window by entering the password same as a value in tag ‘PASS_CODE_MTN’ of the configuration file and confirming the password
            Expected Result: DMI displays Maintenance window.Verify the following information,The ‘Close’ button is enabled
            Test Step Comment: (1) MMI_gen 9231 (partly: Maintenance window);   
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Maintenance’ button. Enter the value from the configuration file PASS_CDE_MTN tag and confirm the password");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Maintenance window." + Environment.NewLine +
                                "2. The ‘Close’ button is enabled");

            /*
            Test Step 5
            Action: Press ‘Close’ button
            Expected Result: DMI displays Settings window
            Test Step Comment: MMI_gen 8785 (partly: additional DMI technical function, Maintenance);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button in the Maintenance window");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Settings window.");

            /*
            Test Step 6
            Action: Perform the following procedure, Press ‘Close’ button.Enter Driver ID and perform brake test.Select and confirm Level 1.Press ‘Close’ button
            Expected Result: DMI displays Default window
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button. Enter the Driver ID and perform a brake test. Select and confirm Level 1.	Press the ‘Close’ button.");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Default window.");

            /*
            Test Step 7
            Action: Press ‘Settings’ button
            Expected Result: DMI displays Settings window.Verify the following information,The ‘Close’ button is enabled
            Test Step Comment: (1) MMI_gen 9231 (partly: Settings window);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press the ‘Settings’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Settings window." + Environment.NewLine +
                                "2. The ‘Close’ button is enabled");

            /*
            Test Step 8
            Action: Press ‘Close’ button
            Expected Result: DMI displays the Default window
            Test Step Comment: MMI_gen 8785 (partly: Settings);
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button in the Settings window");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Default window.");

            /*
            Test Step 9
            Action: Press ‘Settings’ button
            Expected Result: DMI displays Settings window
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press the ‘Settings’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Settings window.");

            /*
            Test Step 10
            Action: Press ‘System version’ button
            Expected Result: DMI displays System version window
            */
            DmiActions.ShowInstruction(this, @"Press the ‘System version’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the System version window.");

            /*
            Test Step 11
            Action: Press ‘Close’ button
            Expected Result: DMI displays Settings window
            Test Step Comment: MMI_gen 8785 (partly: System version);
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button in the System version window");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Settings window.");

            /*
            Test Step 12
            Action: Press ‘Language’ button
            Expected Result: Verify the following information,DMI displays Language window.The ‘Close’ button is enabled
            Test Step Comment: (1) MMI_gen 11992;(2) MMI_gen 9231 (partly: Language window);   
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Language’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Language window." + Environment.NewLine +
                                "2. The ‘Close’ button is enabled");

            /*
            Test Step 13
            Action: Press ‘Close’ button
            Expected Result: DMI displays Settings window
            Test Step Comment: MMI_gen 8785 (partly: Language);
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button in the Language window");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Settings window.");

            /*
            Test Step 14
            Action: Press ‘Language’ button
            Expected Result: DMI displays Language window
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Language’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Language window.");

            /*
            Test Step 15
            Action: Confirm entered data by pressing input field
            Expected Result: DMI displays Settings window
            Test Step Comment: Table 71 (Partly: step S2 (Language window));
            */
            DmiActions.ShowInstruction(this, @"Press the Input Field to accept the data");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Settings window.");

            /*
            Test Step 16
            Action: Press ‘Volume’ button
            Expected Result: Verify the following information,DMI displays Volume window.The ‘Close’ button is enabled
            Test Step Comment: (1) MMI_gen 11994; (2) MMI_gen 9231 (partly: Volume window);   
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press the ‘Volume’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Volume window." + Environment.NewLine +
                                "2. The ‘Close’ button is enabled");

            /*
            Test Step 17
            Action: Press ‘Close’ button
            Expected Result: DMI displays Settings window
            Test Step Comment: MMI_gen 8785 (partly: Volume);
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button in the Volume window");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Settings window.");

            /*
            Test Step 18
            Action: Press ‘Volume’ button
            Expected Result: DMI displays Volume window
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press the ‘Volume’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Volume window.");

            /*
            Test Step 19
            Action: Confirm entered data by pressing input field
            Expected Result: DMI displays Settings window
            Test Step Comment: Table 71 (Partly: step S3 (Volume window));
            */
            DmiActions.ShowInstruction(this, @"Press the Input Field to accept the data");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Settings window.");

            /*
            Test Step 20
            Action: Press ‘Brightness’ button
            Expected Result: Verify the following information,DMI displays Brightness window.The ‘Close’ button is enabled
            Test Step Comment: (1) MMI_gen 11993; (2) MMI_gen 9231 (partly: Volume window);   
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Brightness’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Brightness window." + Environment.NewLine +
                                "2. The ‘Close’ button is enabled");

            /*
            Test Step 21
            Action: Press ‘Close’ button
            Expected Result: DMI displays Settings window
            Test Step Comment: MMI_gen 8785 (partly: Brightness);
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button in the Brightness window");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Settings window.");

            /*
            Test Step 22
            Action: Press ‘Brightness’ button
            Expected Result: DMI displays Brightness window
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Brightness’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Brightness window.");

            /*
            Test Step 23
            Action: Confirm entered data by pressing input field
            Expected Result: DMI displays Settings window
            Test Step Comment: Table 71 (Partly: step S4 (Brightness window));
            */
            DmiActions.ShowInstruction(this, @"Press the Input Field to accept the data");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Settings window.");

            /*
            Test Step 24
            Action: Press ‘System version’ button
            Expected Result: DMI displays System version window.Verify the following information,The ‘Close’ button is enabled
            Test Step Comment: (1) MMI_gen 9231 (partly: System version window);    
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press the ‘System version’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the System version window." + Environment.NewLine +
                                "2. The ‘Close’ button is enabled");

            /*
            Test Step 25
            Action: Press ‘Close’ button
            Expected Result: DMI displays Settings window
            Test Step Comment: MMI_gen 8785 (partly: System version);Table 71 (Partly: step S5 (System version window));
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button in the System version window");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Settings window.");

            /*
            Test Step 26
            Action: Press ‘Set VBC’ button
            Expected Result: DMI displays Set VBC window.Verify the following information,The ‘Close’ button is enabled
            Test Step Comment: (1) MMI_gen 9231 (partly: Set VBC window);    
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press the  ‘Set VBC’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Set VBC window." + Environment.NewLine +
                                "2. The ‘Close’ button is enabled");

            /*
            Test Step 27
            Action: Press ‘Close’ button
            Expected Result: DMI displays Settings window
            Test Step Comment: MMI_gen 8785 (partly: Set VBC);
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button in the Set VBC window");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Settings window.");

            /*
            Test Step 28
            Action: Perforrm the following procedure,Press ‘Set VBC’ button. Enter the value 65536 and confirm by pressing an input field.Press ‘Yes’ button
            Expected Result: DMI displays Validate Set VBC window. Verify the following information,The ‘Close’ button is enabled
            Test Step Comment: (1) MMI_gen 9231 (partly: Validate Set VBC window);    Table 71 (Partly: step S6-1 (Set VBC window));
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Set VBC’ button. Enter the value 65536 and press an input field to confirm. Press the ‘Yes’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Set VBC validation window." + Environment.NewLine +
                                "2. The ‘Close’ button is enabled");

            /*
            Test Step 29
            Action: Press ‘Close’ button
            Expected Result: DMI displays Settings window
            Test Step Comment: MMI_gen 8785 (partly: Set VBC validation);
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button in the Set VBC validation window");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Settings window.");

            /*
            Test Step 30
            Action: Perform a following procedure,Press ‘Set VBC’ buttonEnter and confirm value 65536At Validate set VBC window, press ‘No’ button and press an input field
            Expected Result: DMI displays Set VBC window
            Test Step Comment: Table 71 (Partly: step S6-2 (Set VBC validation window));
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Set VBC’ button. Enter the value 65536. Press the ‘No’ button in the Set VBC validation window and press an input field");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Set VBC window");

            /*
            Test Step 31
            Action: Perform a following procedure,Enter and confirm value 65536At Validate set VBC window, press ‘Yes’ button and press an input field
            Expected Result: DMI displays Settings window.The ‘Remove VBC’ button is enabled
            Test Step Comment: Table 71 (Partly: step S6-2 (Set VBC validation window));
            */
            DmiActions.ShowInstruction(this, @"Enter and confirm the value 65536. Press the ‘Yes’ button in the Set VBC validation window and press an input field");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Settings window." + Environment.NewLine +
                                "2. The ‘Remove VBC’ button is enabled.");

            /*
            Test Step 32
            Action: Press ‘Remove VBC’ button
            Expected Result: DMI displays Remove VBC window.Verify the following information,The ‘Close’ button is enabled
            Test Step Comment: (1) MMI_gen 9231 (partly: Remove VBC window);    
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press the ‘Remove VBC’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Remove VBC window." + Environment.NewLine +
                                "2. The ‘Close’ button is enabled.");

            /*
            Test Step 33
            Action: Press ‘Close’ button
            Expected Result: DMI displays Settings window
            Test Step Comment: MMI_gen 8785 (partly: Remove VBC);
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button in the Remove VBC window");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Settings window.");

            /*
            Test Step 34
            Action: Perforrm the following procedure,Enter the value 65536 and confirm by pressing an input field.Press ‘Yes’ button
            Expected Result: DMI displays Validate Remove VBC window. Verify the following information,The ‘Close’ button is enabled
            Test Step Comment: (1) MMI_gen 9231 (partly: Validate Remove VBC window);    
            */
            DmiActions.ShowInstruction(this, @"Enter the value 65536 and press an input field to confirm. Press the ‘Yes’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Remove VBC validation window." + Environment.NewLine +
                                "2. The ‘Close’ button is enabled.");

            /*
            Test Step 35
            Action: Press ‘Close’ button
            Expected Result: DMI displays Settings window
            Test Step Comment: MMI_gen 8785 (partly: Remove VBC validation);
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button in the Remove VBC validation window");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Settings window.");

            /*
            Test Step 36
            Action: Perforrm the following procedure,Press ‘Remove VBC’ button. Enter the value 65536 and confirm by pressing an input field.At the Validate remove VBC window, press ‘No’ button and press an input field
            Expected Result: DMI displays Remove VBC window
            Test Step Comment: Table 71 (Partly: step 76-2 (Remove VBC validation window));
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Remove VBC’ button. Enter the value 65536 and confirm by pressing an input field. Press the ‘No’ button in the Remove VBC validation window and press an input field");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Remove VBC window.");

            /*
            Test Step 37
            Action: Perform a following procedure,Enter and confirm value 65536At the Validate remove VBC window, press ‘Yes’ button and press an input field
            Expected Result: DMI displays Settings window.The ‘Remove VBC’ button is disabled
            Test Step Comment: Table 71 (Partly: step 76-2 (Remove VBC validation window));
            */
            DmiActions.ShowInstruction(this, @"Enter the value 65536 and confirm by pressing an input field. Press the ‘Yes’ button in the Remove VBC validation window and press an input field");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Settings window." + Environment.NewLine +
                                "2. The ‘Remove VBC’ button is disabled.");
            
            /*
            Test Step 38
            Action: Press ‘Brake’ button
            Expected Result: DMI displays Brake window.Verify the following information,The ‘Close’ button is enabled
            Test Step Comment: (1) MMI_gen 9231 (partly: Brake window);    
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press the ‘Brake’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Brake window." + Environment.NewLine +
                                "2. The ‘Close’ button is enabled.");

            /*
            Test Step 39
            Action: Press ‘Close’ button
            Expected Result: DMI displays Settings window
            Test Step Comment: MMI_gen 8785 (partly: additional DMI technical function, Brake);
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button in the Brake validation window");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Settings window.");

            /*
            Test Step 40
            Action: Press ‘System Info’ button
            Expected Result: DMI displays System Info window.Verify the following information,The ‘Close’ button is enabled
            Test Step Comment: (1) MMI_gen 9231 (partly: System Info window);    
            */
            DmiActions.ShowInstruction(this, @"Press the ‘System Info’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the System Info window." + Environment.NewLine +
                                "2. The ‘Close’ button is enabled.");

            /*
            Test Step 41
            Action: Press ‘Close’ button
            Expected Result: DMI displays Settings window
            Test Step Comment: MMI_gen 8785 (partly: additional DMI technical function, System Info);
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button in the System Info window");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Settings window.");

            /*
            Test Step 42
            Action: Press ‘Set Clock’ button
            Expected Result: DMI displays Set clock window.Verify the following information,The ‘Close’ button is enabled
            Test Step Comment: (1) MMI_gen 9231 (partly: Set Clock window);    
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Set Clock’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Set Clock window." + Environment.NewLine +
                                "2. The ‘Close’ button is enabled.");

            /*
            Test Step 43
            Action: Press ‘Close’ button
            Expected Result: DMI displays Settings window
            Test Step Comment: MMI_gen 8785 (partly: additional DMI technical function, Set Clock);
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button in the Set Clock  window");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Settings window.");

            /*
            Test Step 44
            Action: Press ‘Close’ button.Then, simulate loss-communication between ETCS onboard and DMI
            Expected Result: DMI displays Default window with the  message “ATP Down Alarm” and sound alarm.Verify the following information,The ‘Settings’ button is displays as enabled state in sub-area F5
            Test Step Comment: (1) MMI_gen 11995 (partly: enable settings);
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button");
            DmiActions.Simulate_communication_loss_EVC_DMI(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Default window with the message ‘ATP-Down Alarm’." + Environment.NewLine +
                                "2. DMI plays the ‘Alarm’ sound");

            /*
            Test Step 45
            Action: Press ‘Settings’ button
            Expected Result: DMI displays Settings window.Verify the following information,The buttons below are in enabled state,LanguageBrightnessVolumeUse the log file to confirm that DMI does not send out packet EVC-101 and EVC122
            Test Step Comment: (1) MMI_gen 11995 (partly: enable language, brightnesss and volume);(2) MMI_gen 11995 (partly: no request send to ETC, settings button is pressed);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press the ‘Settings’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Settings window." + Environment.NewLine +
                                "2. The ‘Language’ button is enabled." + Environment.NewLine +
                                "3. The ‘Brightness’ button is enabled." + Environment.NewLine +
                                "4. The ‘Volume’ button is enabled.");

            /*
            Test Step 46
            Action: Press ‘Language’ button
            Expected Result: Verify the following information,DMI displays Language window.The ‘Close’ button is enabled.Use the log file to confirm that DMI does not send out packet EVC-101 and EVC122
            Test Step Comment: (1) MMI_gen 11992;(2) MMI_gen 9231 (partly: Language window);   (3) MMI_gen 11995 (partly: no request send to ETC, language button is pressed);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press the ‘Language’ button");

            //EVC101_MMIDriverRequest. ?? empty packet
            //EVC122  ?? empty packet

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Language window." + Environment.NewLine +
                                "2. The ‘Close’ button is enabled.");

            /*
            Test Step 47
            Action: Press ‘Close’ button
            Expected Result: DMI displays Settings window
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button in the Language window");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Settings window.");
            
            /*
            Test Step 48
            Action: Press ‘Brightness’ button
            Expected Result: Verify the following information,DMI displays Brightness window.The ‘Close’ button is enabled.Use the log file to confirm that DMI does not send out packet EVC-101 and EVC122
            Test Step Comment: (1) MMI_gen 11993;(2) MMI_gen 9231 (partly: Brightness window);   (3) MMI_gen 11995 (partly: no request send to ETC, Brightness button is pressed);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press ‘Brightness’ button");

            //EVC101_MMIDriverRequest. ?? empty packet
            //EVC122  ?? empty packet

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Brightness window." + Environment.NewLine +
                                "2. The ‘Close’ button is enabled.");

            /*
            Test Step 49
            Action: Press ‘Close’ button
            Expected Result: DMI displays Settings window
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button in the Brightness window");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Settings window.");

            /*
            Test Step 50
            Action: Press ‘Volume’ button
            Expected Result: Verify the following information,DMI displays Volume window.The ‘Close’ button is enabled.Use the log file to confirm that DMI does not send out packet EVC-101 and EVC122
            Test Step Comment: (1) MMI_gen 11994;(2) MMI_gen 9231 (partly: Volume window);   (3) MMI_gen 11995 (partly: no request send to ETC, Volume button is pressed);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press ‘Volume’ button");

            //EVC101_MMIDriverRequest. ?? empty packet
            //EVC122  ?? empty packet

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Volume window." + Environment.NewLine +
                                "2. The ‘Close’ button is enabled.");

            /*
            Test Step 51
            Action: Press ‘Close’ button
            Expected Result: DMI displays Settings window
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button in the Volume window");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Settings window.");
            
            /*
            Test Step 52
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
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
    /// 7.6 Safety related Data Entry
    /// TC-ID: 2.6
    /// 
    /// This test case verifies the different format of presentation including with position of confirm buttons and the format of packet sending/receiving between Data entry windows and Data validation windows.
    /// 
    /// Tested Requirements:
    /// MMI_gen 3203; MMI_gen 3226; MMI_gen 3205; MMI_gen 3390; MMI_gen 3391;
    /// 
    /// Scenario:
    /// 1.The format of presentation and packet sending/receiving in Wheel diameter window, Wheel diameter validaiton window, Radar window and Radar validation window are verified (Maintenance Data Entry and Maintenance Data Validation).
    /// 2.The format of presentation and packet sending/receiving in Set VBC window, Set VBC validaiton window, Remove VBC window and Remove VBC validation window are verified (Set VBC Entry, Set VBC Validation, Remove VBC Entry, Remove VBC Validtion).
    /// 3.The format of presentation and packet sending/receiving in Train data window and Train data validation window are verified (Train Data Entry and Train Data Validation).
    /// 4.The format of presentation and packet sending/receiving in Brake percentage window and Brake percentage validation window are verified (Brake percentage Entry and Brake percentage Validation).
    /// 
    /// Used files:
    /// 2_6_a.xml
    /// </summary>
    public class TC_ID_2_6_Safety_related_Data_Entry : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
        
            // Call the TestCaseBase PreExecution
            base.PreExecution();
            // System is power on.Cabin is activated.Settings window is opened.Maintenance password window is opened.The correct password is entered, the Maintenance window is opened.
            DmiActions.Start_ATP();

            // Set train running number, cab 1 active, and other defaults
            DmiActions.Activate_Cabin_1(this);

            // force the window
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Settings;      // Settings window
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.EnableWheelDiameter;
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
            Action: Press ‘Wheel diameter’ button
            Expected Result: DMI displays Wheel diameter window.Verify the following information,(1)    Use the log file to confirm that DMI received packet EVC-40 with variable MMI_Q_MD_DATASET = 0 from ETCS Onboard.(2)   The format of presentation in Wheel diameter window is presented as an Input Fields
            Test Step Comment: (1) MMI_gen 3226 (partly: Maintenance Data Entry);(2)  MMI_gen 3390 (partly: Maintenance Data entry);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press the ‘Maintenance’ button, enter and confirm the maintenance password (as in the configuration file)," + Environment.NewLine +
                                             @"then press the ‘Wheel diameter’ button in the Maintenance window");
                                          
            EVC40_MMICurrentMaintenanceData.MMI_Q_MD_DATASET = Variables.MMI_Q_MD_DATASET.WheelDiameter;
            EVC40_MMICurrentMaintenanceData.MMI_M_SDU_WHEEL_SIZE_1 = (Variables.MMI_M_SDU_WHEEL_SIZE)20001;
            EVC40_MMICurrentMaintenanceData.MMI_M_SDU_WHEEL_SIZE_2 = (Variables.MMI_M_SDU_WHEEL_SIZE)20001;
            EVC40_MMICurrentMaintenanceData.MMI_M_WHEEL_SIZE_ERR = (Variables.MMI_M_WHEEL_SIZE_ERR)200;
            EVC40_MMICurrentMaintenanceData.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                               "1. DMI displays the Wheel Diameter window." + Environment.NewLine +
                               "2. The Wheel diameter present its contents as Input Fields.");

            /*
            Test Step 2
            Action: Confirm all value of each Input Field.Then, press ‘Yes’ button
            Expected Result: DMI displays Wheel diameter validation window.Verify the following information,(1)    Use the log file to confirm that DMI sent out packet EVC-140 variable based on confirmed data and MMI_Q_MD_DATASET = 0 to ETCS Onboard.(2)   Use the log file to confirm that DMI received packet EVC-41 with variable MMI_Q_MD_DATASET = 0 from ETCS Onboard.(3)   The position of ‘Yes’ button on Wheel diameter validation window is located at the different location of ‘Yes’ button on Wheel diameter window.(4)   The format of presentation in Wheel diameter validation window is difference from Wheel diameter window as follows,           -   The data pending for confirmation of Wheel diameter validation window is presented as echo texts.(5)   The presentation of echo text in Wheel diameter validation window is located at the difference location of an Input Fields in Wheel diameter window
            Test Step Comment: (1) MMI_gen 3203 (Maintenance Data Entry);(2) MMI_gen 3226 (partly: Maintenance Data Validation);(3) MMI_gen 3205 (partly: Maintenance Data Entry and Validation);(4) MMI_gen 3390 (partly: Maintenance Validation);(5) MMI_gen 3391 (partly: Maintenance Data Entry and Validation);
            */
            DmiActions.ShowInstruction(this, @"Accept the values of all Input Fields as shown. Press the ‘Yes’ button");

            EVC140_MMINewMaintenanceData.MMI_Q_MD_DATASET = Variables.MMI_Q_MD_DATASET.WheelDiameter;
            EVC140_MMINewMaintenanceData.MMI_M_SDU_WHEEL_SIZE_1 = (Variables.MMI_M_SDU_WHEEL_SIZE)1000;
            EVC140_MMINewMaintenanceData.MMI_M_SDU_WHEEL_SIZE_2 = (Variables.MMI_M_SDU_WHEEL_SIZE)100;
            EVC140_MMINewMaintenanceData.MMI_M_WHEEL_SIZE_ERR = (Variables.MMI_M_WHEEL_SIZE_ERR) 30;
            EVC140_MMINewMaintenanceData.CheckTelegram();

            EVC41_MMIEchoedMaintenanceData.MMI_Q_MD_DATASET_ = Variables.MMI_Q_MD_DATASET.WheelDiameter;
            EVC41_MMIEchoedMaintenanceData.MMI_M_SDU_WHEEL_SIZE_1_ = (Variables.MMI_M_SDU_WHEEL_SIZE)1000;
            EVC41_MMIEchoedMaintenanceData.MMI_M_SDU_WHEEL_SIZE_2_ = (Variables.MMI_M_SDU_WHEEL_SIZE)100;
            EVC41_MMIEchoedMaintenanceData.MMI_M_WHEEL_SIZE_ERR_ = (Variables.MMI_M_WHEEL_SIZE_ERR)30;
            EVC41_MMIEchoedMaintenanceData.Send(); 
            
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                               "1. DMI displays the Wheel Diameter validation window." + Environment.NewLine +
                               @"2. The ‘Yes’ button in the Wheel diameter validation window is at a different location from the ‘Yes’ button in the Wheel diameter window." + Environment.NewLine +
                               "3. Data pending confirmation in the Wheel diameter validation window is presented as echo text." + Environment.NewLine +
                               "4. The echo text in the Wheel diameter validation window is placed at a different location from the Input Fields in the Wheel diameter window.");

            /*
            Test Step 3
            Action: Press ‘Yes’ button.Then, confirm an entered data by pressing at an Input Field
            Expected Result: DMI displays Settings window.Verify the following information,(1)    Use the log file to confirm that DMI sent out packet EVC-141 with variable based on confirmed data to ETCS Onboard
            Test Step Comment: (1) MMI_gen 3203 (partly: Maintenance Data Validation);
            */
            // EVC141_MMIConfirmedMaintenanceData.MMI_Q_MD_DATASET_ ?? 
            DmiActions.ShowInstruction(this, "@Press the ‘Yes’ button. Accept entered data by pressing an Input Field and check the log file for packet EVC-141 from DMI with variables reflecting the accepted data");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                               "1. DMI displays the Settings window.");

            /*
            Test Step 4
            Action: Press ‘Radar’ button
            Expected Result: DMI displays Radar window.Verify the following information,(1)    Use the log file to confirm that DMI received packet EVC-40 with variable MMI_Q_MD_DATASET = 1 from ETCS Onboard.(2)   The format of presentation in Radar window is presented as an Input Fields
            Test Step Comment: (1) MMI_gen 3226 (partly: Maintenance Data Entry);(2)  MMI_gen 3390 (partly: Maintenance Data entry);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press ‘Radar’ button");

            EVC40_MMICurrentMaintenanceData.MMI_Q_MD_DATASET = Variables.MMI_Q_MD_DATASET.Doppler;
            EVC40_MMICurrentMaintenanceData.MMI_M_PULSE_PER_KM_1 = (Variables.MMI_M_PULSE_PER_KM) 20001;
            EVC40_MMICurrentMaintenanceData.MMI_M_PULSE_PER_KM_2 = (Variables.MMI_M_PULSE_PER_KM)20001;
            EVC40_MMICurrentMaintenanceData.Send();
            
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                               "1. DMI displays the Radar window." + Environment.NewLine +
                               "2. The Radar window displays its contents as Input Fields.");

            /*
            Test Step 5
            Action: Confirm all value of each Input Field.Then, press ‘Yes’ button
            Expected Result: DMI displays Radar validation window.Verify the following information,(1)    Use the log file to confirm that DMI sent out packet EVC-140 variable based on confirmed data and MMI_Q_MD_DATASET = 1 to ETCS Onboard.(2)   Use the log file to confirm that DMI received packet EVC-41 with variable MMI_Q_MD_DATASET = 1 from ETCS Onboard.(3)   The position of ‘Yes’ button on Radar validation window is located at the different location of ‘Yes’ button on Radar window.(4)   The format of presentation in Radar validation window is difference from Radar window as follows,           -   The data pending for confirmation of Radar validation window is presented as echo texts.(5)   The presentation of echo text in Radar validation window is located at the difference location of an Input Fields in Radar window
            Test Step Comment: (1) MMI_gen 3203 (Maintenance Data Entry);(2) MMI_gen 3226 (partly: Maintenance Data Validation);(3) MMI_gen 3205 (partly: Maintenance Data Entry and Validation);(4) MMI_gen 3390 (partly: Maintenance Validation);(5) MMI_gen 3391 (partly: Maintenance Data Entry and Validation);
            */
            DmiActions.ShowInstruction(this, @"Accept the values of all Input Fields as shown. Press the ‘Yes’ button");

            EVC140_MMINewMaintenanceData.MMI_Q_MD_DATASET = Variables.MMI_Q_MD_DATASET.Doppler;
            EVC140_MMINewMaintenanceData.MMI_M_PULSE_PER_KM_1 = (Variables.MMI_M_PULSE_PER_KM)20001;
            EVC140_MMINewMaintenanceData.MMI_M_PULSE_PER_KM_2 = (Variables.MMI_M_PULSE_PER_KM)20001;
            EVC140_MMINewMaintenanceData.CheckTelegram();

            EVC41_MMIEchoedMaintenanceData.MMI_Q_MD_DATASET_ = Variables.MMI_Q_MD_DATASET.Doppler;
            EVC41_MMIEchoedMaintenanceData.MMI_M_PULSE_PER_KM_1_ = (Variables.MMI_M_PULSE_PER_KM)20001;
            EVC41_MMIEchoedMaintenanceData.MMI_M_PULSE_PER_KM_2_ = (Variables.MMI_M_PULSE_PER_KM)20001;
            EVC41_MMIEchoedMaintenanceData.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                               "1. DMI displays the Radar validation window." + Environment.NewLine +
                               @"2. The ‘Yes’ button in the Radar validation window is at a different location from the ‘Yes’ button in the Radar window." + Environment.NewLine +
                               "3. Data pending confirmation in the Radar validation window is presented as echo text." + Environment.NewLine +
                               "4. The echo text in the Radar validation window is at a different location from the Input Fields in the Radar window.");

            /*
            Test Step 6
            Action: Perform the following procedure,Press ‘Yes’ button.Confirm an entered data by pressing at an Input Field.At ‘Maintenance’ window, press ‘Close’ button
            Expected Result: DMI displays Settings window.Verify the following information,(1)    Use the log file to confirm that DMI sent out packet EVC-141 with variable based on confirmed data to ETCS Onboard
            Test Step Comment: (1) MMI_gen 3203 (partly: Maintenance Data Validation);
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Yes’ button. Accept entered data by pressing an Input Field" + Environment.NewLine +
                                             @"Press the ‘Yes’ button in the Maintenance windows and check the log file for packet EVC-141 from DMI with variables reflecting the accepted data");
            
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                               "1. DMI displays the Settings window.");

            /*
            Test Step 7
            Action: Press ‘Set VBC’ button
            Expected Result: DMI displays Set VBC window.Verify the following information,(1)    Use the log file to confirm that DMI received packet EVC-18 from ETCS Onboard.(2)   The format of presentation in Set VBC window is presented as an Input Field
            Test Step Comment: (1) MMI_gen 3226 (partly: Set VBC Data Entry);(2)  MMI_gen 3390 (partly: Set VBC Data entry);
            */
            // Call generic Action Method
            EVC18_MMISetVBC.MMI_N_VBC = 0;
            EVC18_MMISetVBC.Send();

            DmiActions.ShowInstruction(this, @"Press ‘Set VBC’ button");
            
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                               "1. DMI displays the Set VBC window." + Environment.NewLine +
                               "2. The Set VBC window displays its contents as Input Fields.");

            /*
            Test Step 8
            Action: Enter and confirm the value ‘65536’ at an Input Field.Then, press ‘Yes’ button
            Expected Result: DMI displays Radar validation window.Verify the following information,(1)    Use the log file to confirm that DMI sent out packet EVC-118 to ETCS Onboard.(2)    Use the log file to confirm that DMI received packet EVC-28 from ETCS Onboard.(3)   The position of ‘Yes’ button on Set VBC validation window is located at the different location of ‘Yes’ button on Set VBC window.(4)   The format of presentation in Set VBC validation window is difference from Set VBC window as follows,           -   The data pending for confirmation of Set VBC validation window is presented as echo texts.(5)   The presentation of echo text in Set VBC validation window is located at the difference location of an Input Fields in Set VBC window
            Test Step Comment: (1) MMI_gen 3203 (Set VBC Data Entry);(2) MMI_gen 3226 (partly: Set VBC Data Validation);(3) MMI_gen 3205 (partly: Set VBC Data Entry and Validation);(4) MMI_gen 3390 (partly: Set VBC Validation);(5) MMI_gen 3391 (partly: Set VBC Data Entry and Validation);
            */

            // Test spec says Radar validation window but message means that Set RBC Validation window would be displayed...
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Enter and confirm the value ‘65536’ in an Input Field, then press the ‘Yes’ button");
            
            EVC28_MMIEchoedSetVBCData.MMI_M_VBC_CODE_ = 65536;
            EVC28_MMIEchoedSetVBCData.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Set VBC validation window." + Environment.NewLine +
                                @"2. The ‘Yes’ button in the Set VBC validation validation window is at a different location from the ‘Yes’ button in the Set VBC window." + Environment.NewLine +
                                "3. Data pending confirmation in the Set VBC validation window is presented as echo text." + Environment.NewLine +
                                "4. The echo text in the Set VBC validation window is at a different location from the Input Fields in the Set VBC window.");

            /*
            Test Step 9
            Action: Press ‘Yes’ button.Then, confirm an entered value by pressing at an Input Field
            Expected Result: DMI displays Settings window.Verify the following information,(1)    Use the log file to confirm that DMI sent out packet EVC-128 with variable based on confirmed data to ETCS Onboard
            Test Step Comment: (1) MMI_gen 3203 (partly: Set VBC Data Validation);
            */
            // Call generic Action Method           
            DmiActions.ShowInstruction(this, @"Press the ‘Yes’ button. Accept an Input Field value by pressing it");

            EVC128_MMIConfirmedSetVBC.Check_VBC_Code = 65536;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                               "1. DMI displays the Settings window.");

            /*
            Test Step 10
            Action: Press ‘Remove VBC’ window
            Expected Result: DMI displays Remove VBC window.Verify the following information,(1)    Use the log file to confirm that DMI received packet EVC-19 from ETCS Onboard.(2)   The format of presentation in Remove VBC window is presented as an Input Field
            Test Step Comment: (1) MMI_gen 3226 (partly: Remove VBC Data Entry);(2)  MMI_gen 3390 (partly: Remove VBC Data entry);
            */
            DmiActions.ShowInstruction(this, @"Press ‘Remove VBC’ window");

            EVC19_MMIRemoveVBC.MMI_M_BUTTONS = Variables.MMI_M_BUTTONS_VBC.BTN_SETTINGS;
            EVC19_MMIRemoveVBC.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                               "1. DMI displays the Remove VBC window." + Environment.NewLine +
                               "2. The Remove VBC window displays its contents as Input Fields."); 

            /*
            Test Step 11
            Action: Enter and confirm the value ‘65536’ at an Input Field.Then, press ‘Yes’ button
            Expected Result: DMI displays Radar validation window.Verify the following information,(1)   Use the log file to confirm that DMI sent out packet EVC-119 to ETCS Onboard. (2)   Use the log file to confirm that DMI received packet EVC-29 from ETCS Onboard.(3)   The position of ‘Yes’ button on Remove VBC validation window is located at the different location of ‘Yes’ button on Remove VBC window.(4)   The format of presentation in Remove VBC validation window is difference from Remove VBC window as follows,           -   The data pending for confirmation of Remove VBC validation window is presented as echo texts.(5)   The presentation of echo text in Remove VBC validation window is located at the difference location of an Input Fields in Remove VBC window
            Test Step Comment: (1) MMI_gen 3203 (Remove VBC Data Entry);(2) MMI_gen 3226 (partly: Remove VBC Data Validation);(3) MMI_gen 3205 (partly: Remove VBC Data Entry and Validation);(4) MMI_gen 3390 (partly: Remove VBC Validation);(5) MMI_gen 3391 (partly: Remove VBC Data Entry and Validation);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Enter and confirm the value ‘65536’ at an Input Field.Then, press ‘Yes’ button and check the log file for packet EVC-119 from DMI");

            EVC29_MMIEchoedRemoveVBCData.MMI_M_VBC_CODE_ = 66536;
            EVC29_MMIEchoedRemoveVBCData.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                               "1. DMI displays the Remove VBC validation window." + Environment.NewLine +
                                @"2. The ‘Yes’ button in the Remove VBC validation  window is at a different location from the ‘Yes’ button in the Remove VBC window." + Environment.NewLine +
                                "3. Data pending confirmation in the Set VBC validation window is presented as echo text." + Environment.NewLine +
                                "4. The echo text in the Remove VBC validation window is at a different location from the Input Fields in the Remove VBC window.");

            /*
            Test Step 12
            Action: Press ‘Yes’ button.Then, confirm an entered value by pressing at an Input Field
            Expected Result: DMI displays Settings window.Verify the following information,(1)    Use the log file to confirm that DMI sent out packet EVC-129 with variable based on confirmed data to ETCS Onboard
            Test Step Comment: (1) MMI_gen 3203 (partly: Remove VBC Data Validation);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press ‘Yes’ button.Then, confirm an entered value by pressing  an Input Field");

            EVC129_MMIConfirmedRemoveVBC.Check_VBC_Code = 65536;
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                               "1. DMI displays the Settings window.");

            /*
            Test Step 13
            Action: Perform the following procedure,Press ‘Close’ buttonEnter Driver ID and skip brake test.Select and confirm ‘Level 1’.Press ‘Train data’ button
            Expected Result: DMI displays Train data window.Verify the following information,(1)    Use the log file to confirm that DMI received packet EVC-6 from ETCS Onboard.(2)   The format of presentation in Train data window is presented as an Input Fields
            Test Step Comment: (1) MMI_gen 3226 (partly: Train Data Entry);(2)  MMI_gen 3390 (partly: Train Data entry);
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button. Enter Driver ID and skip the brake test. Select and confirm ‘Level 1’.	Press the ‘Train data’ button");

            DmiActions.Send_EVC6_MMICurrentTrainData_FixedDataEntry(this, new[] { "FLU" }, 2);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                               "1. DMI displays the Train data window." + Environment.NewLine +
                               "2. The Train data window displays its contents as Input Fields.");

            /*
            Test Step 14
            Action: Confirm all value of each Input Field.Then, press ‘Yes’ button
            Expected Result: DMI displays Train data validation window.Verify the following information,(1)    Use the log file to confirm that DMI sent out packet EVC-107 with variable based on confirmed data to ETCS Onboard.(2)   Use the log file to confirm that DMI received packet EVC-10 from ETCS Onboard.(3)   The position of ‘Yes’ button on Train Data validation window is located at the different location of ‘Yes’ button on Train data window.(4)   The format of presentation in Train data validation window is difference from Train data window as follows,           -   The data pending for confirmation of Train data validation window is presented as echo texts.(5)   The presentation of echo text in Train data validation window is located at the difference location of an Input Fields in Train data window
            Test Step Comment: (1) MMI_gen 3203 (partly: Train Data Entry);(2) MMI_gen 3226 (partly: Train Data Validation);(3) MMI_gen 3205 (partly: Train Data Entry and Validation);(4) MMI_gen 3390 (partly: Train Validation);(5) MMI_gen 3391 (partly: Train Data Entry and Validation);
            */
            DmiActions.ShowInstruction(this, "@Accept the values of each Input Field");

            EVC107_MMINewTrainData.TrainsetSelected = Variables.Fixed_Trainset_Captions.FLU;

            DmiActions.Send_EVC10_MMIEchoedTrainData_FixedDataEntry(this, new[] { "FLU" });

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Train Data validation window." + Environment.NewLine +
                                @"2. The ‘Yes’ button in the Train Data validation validation window is at a different location from the ‘Yes’ button in the Train Data window." + Environment.NewLine +
                                "3. Data pending confirmation in the Train Data validation window is presented as echo text." + Environment.NewLine +
                                "4. The echo text in the Train Data validation window is at a different location from the Input Fields in the Train Data window.");

            /*
            Test Step 15
            Action: Press ‘Yes’ button.Then, confirm an entered value by pressing at an Input Field
            Expected Result: DMI displays Train running number window.Verify the following information,(1)    Use the log file to confirm that DMI sent out packet EVC-110 with variable based on confirmed data to ETCS Onboard
            Test Step Comment: (1) MMI_gen 3203 (partly: Train Data Validation);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press ‘Yes’ button.Then, confirm an entered value by pressing an Input Field and check the log file for packet EVC-110 from DMI with variables reflecting the accepted data");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                               "1. DMI displays the Train Running number window.");

            /*
            Test Step 16
            Action: Perform the following procedure,Enter and confirm the train running numberPress ‘Close’ button.Press ‘Settings’ button.Press ‘Brake’ button.Use the test script file 2_6_a.xml to send EVC-30 with MMI_NID_WINDOW = 4 and MMI_Q_REQUEST_ENABLE_64 (#31) =1Press the enabled 'Brake percentage' button
            Expected Result: DMI displays Brake percentage window.Verify the following information,(1)    Use the log file to confirm that DMI received packet EVC-50 from ETCS Onboard.(2)   The format of presentation in Brake Percentage window is presented as an Input Field
            Test Step Comment: (1) MMI_gen 3226 (partly: Brake Percentage Data Entry);(2)  MMI_gen 3390 (partly: Brake Percentage Data Entry);
            */
            DmiActions.ShowInstruction(this, @"Enter and confirm the train running number. Press ‘Close’ button. Press ‘Settings’ button. Press ‘Brake’ button");

            #region Send_XML_2_6_DMI_Test_Specification
            // Does this display Train Running window (expect Brake Percentage)
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Settings;
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.EnableBrakePercentage;
            EVC30_MMIRequestEnable.Send();
            #endregion

            // Need to send set of data for the input values ??
            EVC50_MMICurrentBrakePercentage.MMI_M_BP_ORIG = 100;
            EVC50_MMICurrentBrakePercentage.MMI_M_BP_MEASURED = 93;
            EVC50_MMICurrentBrakePercentage.MMI_M_BP_CURRENT = 92;
            EVC50_MMICurrentBrakePercentage.Send();

            DmiActions.ShowInstruction(this, @"Press the enabled ‘Brake Percentage’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                               "1. DMI displays the Brake Percentage window." + Environment.NewLine +
                               "2. The Brake Percentage window displays its contents as an Input Field.");

            /*
            Test Step 17
            Action: Confirm the current value of brake percentage by pressing at an Input Field
            Expected Result: DMI displays Brake percentage validation window.Verify the following information,(1)    Use the log file to confirm that DMI sent out packet EVC-150 with variable based on confirmed data to ETCS Onboard.(2)    Use the log file to confirm that DMI received packet EVC-51 from ETCS Onboard.(3)   The position of ‘Yes’ button on Brake percentage validation window is located at the different location of ‘Yes’ button on Brake percentage window.(4)   The format of presentation in Brake percentage validation window is difference from Brake percentage window as follows,           -   The data pending for confirmation of Brake percentage validation window is presented as echo texts.(5)   The presentation of echo text in Brake percentage validation window is located at the difference location of an Input Fields in Brake percentage window
            Test Step Comment: (1) MMI_gen 3203 (partly: Brake Percentage Data Entry);(2)MMI_gen 3226 (partly: Brake percentage Validation);(3) MMI_gen 3205 (partly: Brake percentage Entry and Validation);(4) MMI_gen 3390 (partly: Brake percentage Validation);(5) MMI_gen 3391 (partly: Brake percentage Data Entry and Validation);
            */
            DmiActions.ShowInstruction(this, @"Confirm the brake percentage value by pressing an Input Field and check the log file for packet EVC-150 from DMI with variables reflecting the accepted data");

            // Need to send set of data for the input values ??
            EVC51_MMIEchoedBrakePercentage.MMI_M_BP_CURRENT_ = 92;
            EVC51_MMIEchoedBrakePercentage.Send();
            
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Brake percentage validation window." + Environment.NewLine +
                                @"2. The ‘Yes’ button in the Brake percentage validation validation window is at a different location from the ‘Yes’ button in the Brake percentage window." + Environment.NewLine +
                                "3. Data pending confirmation in the Brake percentage validation window is presented as echo text." + Environment.NewLine +
                                "4. The echo text in the Brake percentage validation window is at a different location from the Input Field in the Brake percentage window.");
            
            /*
            Test Step 18
            Action: Press ‘Yes’ button.Then, confirm an entered value by pressing at an Input Field
            Expected Result: DMI displays Brake window.Verify the following information,(1)    Use the log file to confirm that DMI sent out packet EVC-151 with variable based on confirmed data to ETCS Onboard
            Test Step Comment: (1) MMI_gen 3203 (partly: Brake percentage Validation);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press ‘Yes’ button. Confirm the entered value by pressing an Input Field and check the log file for packet EVC-151 from DMI with variables reflecting the accepted data");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                               "1. DMI displays the Brake window.");

            /*
            Test Step 19
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
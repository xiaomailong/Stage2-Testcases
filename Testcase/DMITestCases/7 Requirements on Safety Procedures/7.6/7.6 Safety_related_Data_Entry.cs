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
    public class Safety_related_Data_Entry : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // System is power on.Cabin is activated.Settings window is opened.Maintenance password window is opened.The correct password is entered, the Maintenance window is opened.

            // Call the TestCaseBase PreExecution
            base.PreExecution();
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
            Action: Press ‘Wheel diameter’ button.
            Expected Result: DMI displays Wheel diameter window.Verify the following information,(1)    Use the log file to confirm that DMI received packet EVC-40 with variable MMI_Q_MD_DATASET = 0 from ETCS Onboard.(2)   The format of presentation in Wheel diameter window is presented as an input fields.
            Test Step Comment: (1) MMI_gen 3226 (partly: Maintenance Data Entry);(2)  MMI_gen 3390 (partly: Maintenance Data entry);
            */

            /*
            Test Step 2
            Action: Confirm all value of each input field.Then, press ‘Yes’ button.
            Expected Result: DMI displays Wheel diameter validation window.Verify the following information,(1)    Use the log file to confirm that DMI sent out packet EVC-140 variable based on confirmed data and MMI_Q_MD_DATASET = 0 to ETCS Onboard.(2)   Use the log file to confirm that DMI received packet EVC-41 with variable MMI_Q_MD_DATASET = 0 from ETCS Onboard.(3)   The position of ‘Yes’ button on Wheel diameter validation window is located at the different location of ‘Yes’ button on Wheel diameter window.(4)   The format of presentation in Wheel diameter validation window is difference from Wheel diameter window as follows,           -   The data pending for confirmation of Wheel diameter validation window is presented as echo texts.(5)   The presentation of echo text in Wheel diameter validation window is located at the difference location of an input fields in Wheel diameter window.
            Test Step Comment: (1) MMI_gen 3203 (Maintenance Data Entry);(2) MMI_gen 3226 (partly: Maintenance Data Validation);(3) MMI_gen 3205 (partly: Maintenance Data Entry and Validation);(4) MMI_gen 3390 (partly: Maintenance Validation);(5) MMI_gen 3391 (partly: Maintenance Data Entry and Validation);
            */

            /*
            Test Step 3
            Action: Press ‘Yes’ button.Then, confirm an entered data by pressing at an input field.
            Expected Result: DMI displays Settings window.Verify the following information,(1)    Use the log file to confirm that DMI sent out packet EVC-141 with variable based on confirmed data to ETCS Onboard.
            Test Step Comment: (1) MMI_gen 3203 (partly: Maintenance Data Validation);
            */

            /*
            Test Step 4
            Action: Press ‘Radar’ button.
            Expected Result: DMI displays Radar window.Verify the following information,(1)    Use the log file to confirm that DMI received packet EVC-40 with variable MMI_Q_MD_DATASET = 1 from ETCS Onboard.(2)   The format of presentation in Radar window is presented as an input fields.
            Test Step Comment: (1) MMI_gen 3226 (partly: Maintenance Data Entry);(2)  MMI_gen 3390 (partly: Maintenance Data entry);
            */

            /*
            Test Step 5
            Action: Confirm all value of each input field.Then, press ‘Yes’ button.
            Expected Result: DMI displays Radar validation window.Verify the following information,(1)    Use the log file to confirm that DMI sent out packet EVC-140 variable based on confirmed data and MMI_Q_MD_DATASET = 1 to ETCS Onboard.(2)   Use the log file to confirm that DMI received packet EVC-41 with variable MMI_Q_MD_DATASET = 1 from ETCS Onboard.(3)   The position of ‘Yes’ button on Radar validation window is located at the different location of ‘Yes’ button on Radar window.(4)   The format of presentation in Radar validation window is difference from Radar window as follows,           -   The data pending for confirmation of Radar validation window is presented as echo texts.(5)   The presentation of echo text in Radar validation window is located at the difference location of an input fields in Radar window.
            Test Step Comment: (1) MMI_gen 3203 (Maintenance Data Entry);(2) MMI_gen 3226 (partly: Maintenance Data Validation);(3) MMI_gen 3205 (partly: Maintenance Data Entry and Validation);(4) MMI_gen 3390 (partly: Maintenance Validation);(5) MMI_gen 3391 (partly: Maintenance Data Entry and Validation);
            */

            /*
            Test Step 6
            Action: Perform the following procedure,Press ‘Yes’ button.Confirm an entered data by pressing at an input field.At ‘Maintenance’ window, press ‘Close’ button.
            Expected Result: DMI displays Settings window.Verify the following information,(1)    Use the log file to confirm that DMI sent out packet EVC-141 with variable based on confirmed data to ETCS Onboard.
            Test Step Comment: (1) MMI_gen 3203 (partly: Maintenance Data Validation);
            */

            /*
            Test Step 7
            Action: Press ‘Set VBC’ button.
            Expected Result: DMI displays Set VBC window.Verify the following information,(1)    Use the log file to confirm that DMI received packet EVC-18 from ETCS Onboard.(2)   The format of presentation in Set VBC window is presented as an input field.
            Test Step Comment: (1) MMI_gen 3226 (partly: Set VBC Data Entry);(2)  MMI_gen 3390 (partly: Set VBC Data entry);
            */

            /*
            Test Step 8
            Action: Enter and confirm the value ‘65536’ at an input field.Then, press ‘Yes’ button.
            Expected Result: DMI displays Radar validation window.Verify the following information,(1)    Use the log file to confirm that DMI sent out packet EVC-118 to ETCS Onboard.(2)    Use the log file to confirm that DMI received packet EVC-28 from ETCS Onboard.(3)   The position of ‘Yes’ button on Set VBC validation window is located at the different location of ‘Yes’ button on Set VBC window.(4)   The format of presentation in Set VBC validation window is difference from Set VBC window as follows,           -   The data pending for confirmation of Set VBC validation window is presented as echo texts.(5)   The presentation of echo text in Set VBC validation window is located at the difference location of an input fields in Set VBC window.
            Test Step Comment: (1) MMI_gen 3203 (Set VBC Data Entry);(2) MMI_gen 3226 (partly: Set VBC Data Validation);(3) MMI_gen 3205 (partly: Set VBC Data Entry and Validation);(4) MMI_gen 3390 (partly: Set VBC Validation);(5) MMI_gen 3391 (partly: Set VBC Data Entry and Validation);
            */

            /*
            Test Step 9
            Action: Press ‘Yes’ button.Then, confirm an entered value by pressing at an input field.
            Expected Result: DMI displays Settings window.Verify the following information,(1)    Use the log file to confirm that DMI sent out packet EVC-128 with variable based on confirmed data to ETCS Onboard.
            Test Step Comment: (1) MMI_gen 3203 (partly: Set VBC Data Validation);
            */

            /*
            Test Step 10
            Action: Press ‘Remove VBC’ window.
            Expected Result: DMI displays Remove VBC window.Verify the following information,(1)    Use the log file to confirm that DMI received packet EVC-19 from ETCS Onboard.(2)   The format of presentation in Remove VBC window is presented as an input field.
            Test Step Comment: (1) MMI_gen 3226 (partly: Remove VBC Data Entry);(2)  MMI_gen 3390 (partly: Remove VBC Data entry);
            */

            /*
            Test Step 11
            Action: Enter and confirm the value ‘65536’ at an input field.Then, press ‘Yes’ button.
            Expected Result: DMI displays Radar validation window.Verify the following information,(1)   Use the log file to confirm that DMI sent out packet EVC-119 to ETCS Onboard. (2)   Use the log file to confirm that DMI received packet EVC-29 from ETCS Onboard.(3)   The position of ‘Yes’ button on Remove VBC validation window is located at the different location of ‘Yes’ button on Remove VBC window.(4)   The format of presentation in Remove VBC validation window is difference from Remove VBC window as follows,           -   The data pending for confirmation of Remove VBC validation window is presented as echo texts.(5)   The presentation of echo text in Remove VBC validation window is located at the difference location of an input fields in Remove VBC window.
            Test Step Comment: (1) MMI_gen 3203 (Remove VBC Data Entry);(2) MMI_gen 3226 (partly: Remove VBC Data Validation);(3) MMI_gen 3205 (partly: Remove VBC Data Entry and Validation);(4) MMI_gen 3390 (partly: Remove VBC Validation);(5) MMI_gen 3391 (partly: Remove VBC Data Entry and Validation);
            */

            /*
            Test Step 12
            Action: Press ‘Yes’ button.Then, confirm an entered value by pressing at an input field.
            Expected Result: DMI displays Settings window.Verify the following information,(1)    Use the log file to confirm that DMI sent out packet EVC-129 with variable based on confirmed data to ETCS Onboard.
            Test Step Comment: (1) MMI_gen 3203 (partly: Remove VBC Data Validation);
            */

            /*
            Test Step 13
            Action: Perform the following procedure,Press ‘Close’ buttonEnter Driver ID and skip brake test.Select and confirm ‘Level 1’.Press ‘Train data’ button.
            Expected Result: DMI displays Train data window.Verify the following information,(1)    Use the log file to confirm that DMI received packet EVC-6 from ETCS Onboard.(2)   The format of presentation in Train data window is presented as an input fields.
            Test Step Comment: (1) MMI_gen 3226 (partly: Train Data Entry);(2)  MMI_gen 3390 (partly: Train Data entry);
            */

            /*
            Test Step 14
            Action: Confirm all value of each input field.Then, press ‘Yes’ button.
            Expected Result: DMI displays Train data validation window.Verify the following information,(1)    Use the log file to confirm that DMI sent out packet EVC-107 with variable based on confirmed data to ETCS Onboard.(2)   Use the log file to confirm that DMI received packet EVC-10 from ETCS Onboard.(3)   The position of ‘Yes’ button on Train Data validation window is located at the different location of ‘Yes’ button on Train data window.(4)   The format of presentation in Train data validation window is difference from Train data window as follows,           -   The data pending for confirmation of Train data validation window is presented as echo texts.(5)   The presentation of echo text in Train data validation window is located at the difference location of an input fields in Train data window.
            Test Step Comment: (1) MMI_gen 3203 (partly: Train Data Entry);(2) MMI_gen 3226 (partly: Train Data Validation);(3) MMI_gen 3205 (partly: Train Data Entry and Validation);(4) MMI_gen 3390 (partly: Train Validation);(5) MMI_gen 3391 (partly: Train Data Entry and Validation);
            */

            /*
            Test Step 15
            Action: Press ‘Yes’ button.Then, confirm an entered value by pressing at an input field.
            Expected Result: DMI displays Train running number window.Verify the following information,(1)    Use the log file to confirm that DMI sent out packet EVC-110 with variable based on confirmed data to ETCS Onboard.
            Test Step Comment: (1) MMI_gen 3203 (partly: Train Data Validation);
            */

            /*
            Test Step 16
            Action: Perform the following procedure,Enter and confirm the train running numberPress ‘Close’ button.Press ‘Settings’ button.Press ‘Brake’ button.Use the test script file 2_6_a.xml to send EVC-30 with MMI_NID_WINDOW = 4 and MMI_Q_REQUEST_ENABLE_64 (#31) =1Press the enabled 'Brake percentage' button.
            Expected Result: DMI displays Brake percentage window.Verify the following information,(1)    Use the log file to confirm that DMI received packet EVC-50 from ETCS Onboard.(2)   The format of presentation in Brake Percentage window is presented as an input field.
            Test Step Comment: (1) MMI_gen 3226 (partly: Brake Percentage Data Entry);(2)  MMI_gen 3390 (partly: Brake Percentage Data Entry);
            */

            /*
            Test Step 17
            Action: Confirm the current value of brake percentage by pressing at an input field.
            Expected Result: DMI displays Brake percentage validation window.Verify the following information,(1)    Use the log file to confirm that DMI sent out packet EVC-150 with variable based on confirmed data to ETCS Onboard.(2)    Use the log file to confirm that DMI received packet EVC-51 from ETCS Onboard.(3)   The position of ‘Yes’ button on Brake percentage validation window is located at the different location of ‘Yes’ button on Brake percentage window.(4)   The format of presentation in Brake percentage validation window is difference from Brake percentage window as follows,           -   The data pending for confirmation of Brake percentage validation window is presented as echo texts.(5)   The presentation of echo text in Brake percentage validation window is located at the difference location of an input fields in Brake percentage window.
            Test Step Comment: (1) MMI_gen 3203 (partly: Brake Percentage Data Entry);(2)MMI_gen 3226 (partly: Brake percentage Validation);(3) MMI_gen 3205 (partly: Brake percentage Entry and Validation);(4) MMI_gen 3390 (partly: Brake percentage Validation);(5) MMI_gen 3391 (partly: Brake percentage Data Entry and Validation);
            */

            /*
            Test Step 18
            Action: Press ‘Yes’ button.Then, confirm an entered value by pressing at an input field.
            Expected Result: DMI displays Brake window.Verify the following information,(1)    Use the log file to confirm that DMI sent out packet EVC-151 with variable based on confirmed data to ETCS Onboard.
            Test Step Comment: (1) MMI_gen 3203 (partly: Brake percentage Validation);
            */

            /*
            Test Step 19
            Action: End of test.
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
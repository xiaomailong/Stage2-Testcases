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
    /// 37.1.4.1.1 Data entry/validation process when enabling conditions not fullfilled: Level 1
    /// TC-ID: 34.1.4
    /// 
    /// This test case verifies the dialogue sequences when the button-enabling conditions of the concerned windows are not fulfilled and the display of the ‘Close’ button on the ‘Override’, ‘Special’, ‘Adhesion’ and ‘SR speed/distance’ windows.
    /// 
    /// Tested Requirements:
    /// MMI_gen 8868 (partly: Driver ID, Level, Train data, Train data validaion, Train running number, Language, Brightness, Volume, Adhesion, SR speed/distance); MMI_gen 11283 (partly: Driver ID, Level, Train data, Train data validaion, Train running number, Language, Brightness, Volume, Adhesion, SR speed/distance); MMI_gen 9199 (partly: Adhesion, Special, SR speed/distance); MMI_gen 9179; MMI_gen 3374 (partly: NEGATIVE, close by ETCS OB);
    /// 
    /// Scenario:
    /// SoM is performed until level 1 (mode SB) is confirmed. (Start-up Dialogue Sequence)Level 1 (mode SB) is entered and confirmed again. (After Start-up Dialogue Sequence)A concerned window is opened. Then, the driver drives the train forward without movement authorities.The concerned window is verified.SoM is completed in mode SR, level 1.The ‘SR speed/distance’ window is verified.Note: Please see more detail of enabling condition in chapter 8.2 of requirement specificaiton document. The following windows are concerned:Driver ID windowLevel windowTrain data windowTrain data validation windowLanguage windowBrightness windowVolumn windowSR speed/distance windowAdhesion window
    /// 
    /// Used files:
    /// N/A
    /// </summary>
    public class Data_entryvalidation_process_when_enabling_conditions_not_fullfilled_Level_1 : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // Test system is powered on Cabin A is activated.Enter Driver ID and perform brake test.Level 1 is entered and confirmed.
            
            // Call the TestCaseBase PreExecution
            base.PreExecution();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in SR mode, level 1

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint

            
            /*
            Test Step 1
            Action: Press ‘Driver ID’ button
            Expected Result: DMI displays Driver ID window
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(@"Press ‘Driver ID’ button");
            // Call generic Check Results Method
            DmiExpectedResults.DMI_displays_Driver_ID_window();
            
            
            /*
            Test Step 2
            Action: Perform the following procedure,Drive the train forward until the brake is appliedStop driving the trainAcknowledge the ‘Brake intervention’ symbol by pressing area E1
            Expected Result: Verify the following information,DMI closes the Drive ID window and displays Main window instead.Use the log file to confirm that DMI receives packet information [MMI_ENABLE_REQUEST (EVC-30)] with variable MMI_Q_REQUEST_ENABLE_64 (#0) = 0
            Test Step Comment: (1) MMI_gen 8868 (partly: Driver ID);(2) MMI_gen 11283 (partly: Driver ID);
            */
            // Call generic Action Method
            DmiActions.Perform_the_following_procedure_Drive_the_train_forward_until_the_brake_is_appliedStop_driving_the_trainAcknowledge_the_Brake_intervention_symbol_by_pressing_area_E1();
            
            
            /*
            Test Step 3
            Action: Press ‘Level’ button
            Expected Result: DMI displays Level window
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(@"Press ‘Level’ button");
            // Call generic Check Results Method
            DmiExpectedResults.DMI_displays_Level_window();
            
            
            /*
            Test Step 4
            Action: Perform the following procedure,Drive the train forward until the brake is appliedStop driving the trainAcknowledge the ‘Brake intervention’ symbol by pressing area E1
            Expected Result: Verify the following information,DMI closes the Level window and displays Main window instead.Use the log file to confirm that DMI receives packet information [MMI_ENABLE_REQUEST (EVC-30)] with variable MMI_Q_REQUEST_ENABLE_64 (#3) = 0
            Test Step Comment: (1) MMI_gen 8868 (partly: Level);(2) MMI_gen 11283 (partly: Level);
            */
            // Call generic Action Method
            DmiActions.Perform_the_following_procedure_Drive_the_train_forward_until_the_brake_is_appliedStop_driving_the_trainAcknowledge_the_Brake_intervention_symbol_by_pressing_area_E1();
            
            
            /*
            Test Step 5
            Action: Press ‘Train data’ button
            Expected Result: DMI displays Train data window
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(@"Press ‘Train data’ button");
            // Call generic Check Results Method
            DmiExpectedResults.DMI_displays_Train_data_window();
            
            
            /*
            Test Step 6
            Action: Perform the following procedure,Drive the train forward until the brake is appliedStop driving the trainAcknowledge the ‘Brake intervention’ symbol by pressing area E1
            Expected Result: Verify the following information,DMI closes the Train data window and displays Main window instead.Use the log file to confirm that DMI receives packet information [MMI_ENABLE_REQUEST (EVC-30)] with variable MMI_Q_REQUEST_ENABLE_64 (#2) = 0
            Test Step Comment: (1) MMI_gen 8868 (partly: Train data); (2) MMI_gen 11283 (partly: train data); MMI_gen 3374 (partly: NEGATIVE, close by ETCS OB);
            */
            // Call generic Action Method
            DmiActions.Perform_the_following_procedure_Drive_the_train_forward_until_the_brake_is_appliedStop_driving_the_trainAcknowledge_the_Brake_intervention_symbol_by_pressing_area_E1();
            
            
            /*
            Test Step 7
            Action: Perform the following procedure,Press ‘Train data’ button.Enter and confirm all value of train data window.Press ‘Yes’ button
            Expected Result: DMI displays Train data validation window
            */
            // Call generic Check Results Method
            DmiExpectedResults.DMI_displays_Train_data_validation_window();
            
            
            /*
            Test Step 8
            Action: Perform the following procedure,Drive the train forward until the brake is appliedStop driving the trainAcknowledge the ‘Brake intervention’ symbol by pressing area E1
            Expected Result: Verify the following information,DMI closes the Train data validation window and displays Main window instead.Use the log file to confirm that DMI receives packet information [MMI_ENABLE_REQUEST (EVC-30)] with variable MMI_Q_REQUEST_ENABLE_64 (#2) = 0
            Test Step Comment: (1) MMI_gen 8868 (partly: Train data validation);(2) MMI_gen 11283 (partly: train data validation);
            */
            // Call generic Action Method
            DmiActions.Perform_the_following_procedure_Drive_the_train_forward_until_the_brake_is_appliedStop_driving_the_trainAcknowledge_the_Brake_intervention_symbol_by_pressing_area_E1();
            
            
            /*
            Test Step 9
            Action: Perform the following procedure,Press ‘Train data’ button.Enter and confirm all value of train data window.Press ‘Yes’ button.At the train data validation window, press ‘Yes’ button.Confirm entered data by pressing an input field
            Expected Result: DMI displays Train Running Number window
            */
            // Call generic Check Results Method
            DmiExpectedResults.DMI_displays_Train_Running_Number_window();
            
            
            /*
            Test Step 10
            Action: Perform the following procedure,Drive the train forward until the brake is appliedStop driving the trainAcknowledge the ‘Brake intervention’ symbol by pressing area E1
            Expected Result: Verify the following information,DMI closes the Train Running Number window and displays Main window instead.Use the log file to confirm that DMI receives packet information [MMI_ENABLE_REQUEST (EVC-30)] with variable MMI_Q_REQUEST_ENABLE_64 (#4) = 0
            Test Step Comment: (1) MMI_gen 8868 (partly: Train running number);(2) MMI_gen 11283 (partly: train running number);
            */
            // Call generic Action Method
            DmiActions.Perform_the_following_procedure_Drive_the_train_forward_until_the_brake_is_appliedStop_driving_the_trainAcknowledge_the_Brake_intervention_symbol_by_pressing_area_E1();
            
            
            /*
            Test Step 11
            Action: Press ‘Train running number’ button. Then, enter and confirm Train running number
            Expected Result: DMI displays Main window
            */
            // Call generic Check Results Method
            DmiExpectedResults.DMI_displays_Main_window();
            
            
            /*
            Test Step 12
            Action: Perform the following procedure, Press ‘Close’ buttonPress ‘Settings’ buttonPress ‘Language’ button
            Expected Result: DMI displays Language window
            */
            // Call generic Check Results Method
            DmiExpectedResults.DMI_displays_Language_window();
            
            
            /*
            Test Step 13
            Action: Perform the following procedure,Drive the train forward until the brake is appliedStop driving the trainAcknowledge the ‘Brake intervention’ symbol by pressing area E1
            Expected Result: Verify the following information,DMI closes the Language window and displays Settings window instead.Use the log file to confirm that DMI receives packet information [MMI_ENABLE_REQUEST (EVC-30)] with variable MMI_Q_REQUEST_ENABLE_64 (#13) = 0
            Test Step Comment: (1) MMI_gen 8868 (partly: Language);(2) MMI_gen 11283 (partly: Language);
            */
            // Call generic Action Method
            DmiActions.Perform_the_following_procedure_Drive_the_train_forward_until_the_brake_is_appliedStop_driving_the_trainAcknowledge_the_Brake_intervention_symbol_by_pressing_area_E1();
            
            
            /*
            Test Step 14
            Action: Press ‘Brightness’ button
            Expected Result: DMI displays Brighness window
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(@"Press ‘Brightness’ button");
            
            
            /*
            Test Step 15
            Action: Perform the following procedure,Drive the train forward until the brake is appliedStop driving the trainAcknowledge the ‘Brake intervention’ symbol by pressing area E1
            Expected Result: Verify the following information,DMI closes the Brightness window and displays Settings window instead.Use the log file to confirm that DMI receives packet information [MMI_ENABLE_REQUEST (EVC-30)] with variable MMI_Q_REQUEST_ENABLE_64 (#15) = 0
            Test Step Comment: (1) MMI_gen 8868 (partly: Brightness);(2) MMI_gen 11283 (partly: Brightness);
            */
            // Call generic Action Method
            DmiActions.Perform_the_following_procedure_Drive_the_train_forward_until_the_brake_is_appliedStop_driving_the_trainAcknowledge_the_Brake_intervention_symbol_by_pressing_area_E1();
            
            
            /*
            Test Step 16
            Action: Press ‘Volume’ button
            Expected Result: DMI displays Volume window
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(@"Press ‘Volume’ button");
            // Call generic Check Results Method
            DmiExpectedResults.DMI_displays_Volume_window();
            
            
            /*
            Test Step 17
            Action: Perform the following procedure,Drive the train forward until the brake is appliedStop driving the trainAcknowledge the ‘Brake intervention’ symbol by pressing area E1
            Expected Result: Verify the following information,DMI closes the Volume window and displays Settings window instead.Use the log file to confirm that DMI receives packet information [MMI_ENABLE_REQUEST (EVC-30)] with variable MMI_Q_REQUEST_ENABLE_64 (#14) = 0
            Test Step Comment: (1) MMI_gen 8868 (partly: volume);(2) MMI_gen 11283 (partly: volume);
            */
            // Call generic Action Method
            DmiActions.Perform_the_following_procedure_Drive_the_train_forward_until_the_brake_is_appliedStop_driving_the_trainAcknowledge_the_Brake_intervention_symbol_by_pressing_area_E1();
            
            
            /*
            Test Step 18
            Action: Perform the following procedure,Press ‘Close’ buttonPress ‘Special’ button.Press ‘Adhesion’ button
            Expected Result: DMI displays Adhesion window.Verify the Close button on this window is enabling
            Test Step Comment: MMI_gen 9199 (partly: Adhesion);
            */
            
            
            /*
            Test Step 19
            Action: Perform the following procedure,Drive the train forward until the brake is appliedStop driving the trainAcknowledge the ‘Brake intervention’ symbol by pressing area E1
            Expected Result: Verify the following information,DMI closes the Adhesion window and displays Special window instead.Use the log file to confirm that DMI receives packet information [MMI_ENABLE_REQUEST (EVC-30)] with variable MMI_Q_REQUEST_ENABLE_64 (#10) = 0
            Test Step Comment: (1) MMI_gen 8868 (partly: Adhesion);(2) MMI_gen 11283 (partly: Adhesion);
            */
            // Call generic Action Method
            DmiActions.Perform_the_following_procedure_Drive_the_train_forward_until_the_brake_is_appliedStop_driving_the_trainAcknowledge_the_Brake_intervention_symbol_by_pressing_area_E1();
            
            
            /*
            Test Step 20
            Action: Perform the following procedure,Press ‘Close’ buttonPress ‘Main’ button.Press ‘Start’ button.Acknowledge SR mode.Press ‘Special’ button
            Expected Result: DMI displays Special window.Verify the Close button on this window is enabling
            Test Step Comment: MMI_gen 9199 (partly: special);
            */
            
            
            /*
            Test Step 21
            Action: Press ‘SR speed/distance’ button
            Expected Result: DMI displays SR speed/distance window.Verify the Close button on this window is enabling
            Test Step Comment: MMI_gen 9199 (partly: SR speed/distance);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(@"Press ‘SR speed/distance’ button");
            
            
            /*
            Test Step 22
            Action: Drive the train forward.Then, stop the train
            Expected Result: Verify the following information,DMI closes the SR speed/distance window and displays Special window instead.Use the log file to confirm that DMI receives packet information [MMI_ENABLE_REQUEST (EVC-30)] with variable MMI_Q_REQUEST_ENABLE_64 (#11) = 0
            Test Step Comment: (1) MMI_gen 8868 (partly: SR speed/distance);(2) MMI_gen 11283 (partly: SR speed/distance);
            */
            
            
            /*
            Test Step 23
            Action: Press ‘Close’ button.Then, press ‘Override’ button
            Expected Result: DMI displays Override button.Verify the Close button on this window is enabling
            Test Step Comment: MMI_gen 9179;
            */
            
            
            /*
            Test Step 24
            Action: End of test
            Expected Result: 
            */
            

            return GlobalTestResult;
        }
    }
}

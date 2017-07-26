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
    /// 27.7.2 Data view window for Fixed Train data entry
    /// TC-ID: 22.7.2
    /// 
    /// This test case verifies the display of the ‘Data View’ window with ‘Fixed Train Data’ information which complies with [ERA-ERTMS] and [MMI-ETCS-gen].
    /// 
    /// Tested Requirements:
    /// MMI_gen 2482; MMI_gen 230; MMI_gen 8586; MMI_gen 9428; MMI_gen 8582 (partly: MMI_gen 5338, MMI_gen 5383 (partly: MMI_gen 5944 (partly: touchscreen)), MMI_gen 5337, MMI_gen 5339, MMI_gen 5336 (partly: valid, NEGATIVE, display only display only Driver ID/Train runnung number/ Radio Network ID/ RBC ID/ RBC Phone number), MMI_gen 7510, MMI_gen 5306 (partly: Close button, Previous button, Next button, Window title)); MMI_gen 8583; MMI_gen 8584 (partly: ETCS); MMI_gen 8585 (partly: Fixed train data); MMI_gen 4392 (partly: [Previous : NA19], [Next: NA17], [Close] NA11, returning to the parent window); MMI_gen 4394 (partly: next, previous); MMI_gen 4396 (party: next, previous); MMI_gen 4350; MMI_gen 4351; MMI_gen 4353; MMI_gen 4354; MMI_gen 4358; MMI_gen 7514;
    /// 
    /// Scenario:
    /// Parameter ‘TR_OBU_TrainType’ is set to 1 (Fixed Train Data).Start of Mission is completed in SR mode, level 1The ‘Data View’ window is opened.The ‘Data View’ window is verified.The up-type buttons ‘Next’ and ‘Previous’ are verified.
    /// 
    /// Used files:
    /// 22_7_2_a.xml, 22_7_2_b.xml
    /// </summary>
    public class Data_view_window_for_Fixed_Train_data_entry : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // All value of Parameter ‘TR_OBU_TrainType’ is set to 1 (Fixed Train Data) in defaultValues_default.xml in OTE.Test system is powered on SoM is performed in SR mode, Level 1.

            // Call the TestCaseBase PreExecution
            base.PreExecution();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in SR mode, level 1.

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint


            /*
            Test Step 1
            Action: Press ‘Data view’ button.
            Expected Result: DMI displays Data view window.Verify the following information,Use the log file to confirm that DMI send packet EVC-101 with variable MMI_M_REQUEST = 21 (Start Train Data View).The Data View window is displayed.Data View WindowThe Data view window is covered in main area D, F and G.LayersThe level of layers in each area of window as follows,Layer 0: Area D, F, G, E10, E11, Z, YLayer -1: Area A1, (A2+A3)*, A4, B*, C1, (C2+C3+C4)*, C5, C6, C7, C8, C9, E1, E2, E3, E4, (E5-E9)*.Layer -2: Area B3, B4, B5, B6 and B7.LabelThe data view text is composed of a Label Part and Data Part. The labels of data view items are right aligned.The data of data view items are left aligned.Data view text colour is grey.The different topics (i.e., Train running number and Train data entry) are separated by one empty line.The Data Part are display only a valid value.The window title is displayed with text ‘Data view(1/2)’.Data View ItemsUse the log file to confirm that DMI received packet EVC-13 and displays the following information respectively:Driver IDTrain running numberTrain typeTrain categoryLength (m)Brake percentageMaximum speed (km/h)Axle load categoryAirtightLoading gaugeNote: The display information of Data View items (except Driver ID) are display when the specified bit value of variable MMI_M_DATA_ENABLE is set refer to received packet EVC-13 as follows,MMI_M_DATA_ENABLE (#0) = display of the label of ‘Train Set ID’MMI_M_DATA_ENABLE (#1) = display of label of ‘Train Category’MMI_M_DATA_ENABLE (#2) = display of label of ‘Train Length’MMI_M_DATA_ENABLE (#3) = display of label of ‘Brake Percentage’MMI_M_DATA_ENABLE (#4) = display of label of ‘Max. Train Speed’MMI_M_DATA_ENABLE (#5) = display of label of ‘Axle Load CategoryMMI_M_DATA_ENABLE (#6) = display of label of ‘Airtightness’MMI_M_DATA_ENABLE (#7) = display of label of ‘Loading Guage’The following objects are display in Data View window Enabled Close button (NA11)   Disabled Previous button (NA19)   Enabled Next button (NA17)General property of windowThe Data view window is presented with objects, text messages and which is the one of several levels and allocated to areas of DMI. All objects, text messages and buttons are presented within the same layer.The Default window is not covering the current window.Sub-level window covers partially depending on the size of the Sub-Level window. There is no other window is displayed and activated at the same time.
            Test Step Comment: (1) MMI_gen 2482;(2) MMI_gen 230 (partly: open);(3) MMI_gen 8582 (partly: MMI_gen 5338);  (4) MMI_gen 8582 (partly: MMI_gen 5383 (partly: MMI_gen 5944 (partly: touchscreen)));(5) MMI_gen 8582 (partly: MMI_gen 5335);  (6) MMI_gen 8582 (partly: MMI_gen 5340 (partly: right aligned));  (7) MMI_gen 8582 (partly: MMI_gen 5342 (partly: left aligned));  (8) MMI_gen 8582 (partly: MMI_gen 5337);   (9) MMI_gen 8582 (partly: MMI_gen 5339);  (10) MMI_gen 8582 (partly: MMI_gen 5336 (partly: valid));         (11) MMI_gen 8583; (12) MMI_gen 8584 (partly: ETCS); MMI_gen 8585 (partly:Fixed train data, window #1); MMI_gen 8586 (partly: modify by the driver); MMI_gen 9428; MMI_gen 230 (partly: EVC-13);(13) MMI_gen 8582 (partly: MMI_gen 5306 (partly: Close button, Previous button, Next button, Window title)); MMI_gen 4392 (partly: [Previous : NA19], [Next: NA17], [Close] NA11); MMI_gen 4396 (partly: Previous, NA19, Next, NA17); MMI_gen 4394 (partly: disabled [previous]);(14) MMI_gen 4350;(15) MMI_gen 4351;(16) MMI_gen 4353;(17) MMI_gen 4354;
            */

            /*
            Test Step 2
            Action: Press and hold ‘Next’ button.
            Expected Result: Verify the following information,(1)   The state of button is changed to ‘Pressed’, the border of button is removed.(2)   The sound ‘Click’ is played once.
            Test Step Comment: (1) MMI_gen 9391 (partly: [Next], MMI_gen 4381 (partly: change to state ‘Pressed’ as long as remain actuated));(2) MMI_gen 9391 (partly: [Next], MMI_gen 4381 (partly: sound ‘Click’));
            */

            /*
            Test Step 3
            Action: Slide out the ‘Next’ button
            Expected Result: Verify the following information,(1)   The border of the button is shown (state ‘Enabled’) without a sound.
            Test Step Comment: (1) MMI_gen 9391 (partly: [Next], MMI_gen 4382 (partly: state ‘Enabled’ when slide out with force applied, no sound));
            */

            /*
            Test Step 4
            Action: Slide back into the ‘Next’ button
            Expected Result: Verify the following information,(1)   The button is back to state ‘Pressed’ without a sound.
            Test Step Comment: (1) MMI_gen 9391 (partly: [Next], Train category, MMI_gen 4382 (partly: state ‘Pressed’ when slide back, no sound));
            */

            /*
            Test Step 5
            Action: Release ‘Next’ button.
            Expected Result: Verify that the Data view is displayed the next page of the train data.The window title of the next page is displayed with text ‘Data view (2/2)’. Data View ItemsThe data view items are displayed correctly refer to following items,RBC IDRBC phone numberVBC set code (if any)The data part of RBC phone number is displayed as 2 lines.Navigation buttonsThe state of ‘Previous’ and ‘Next’ button are displayed as follows,  ‘Next’ button is disabled, displays as symbol NA18.2  ‘Previous’ button is enabled, displays as symbol NA18
            Test Step Comment: (1) MMI_gen 8584 (partly: ETCS), MMI_gen 8585 (partly: Fixed train, window #2);       (2) MMI_gen 8585 (partly: Fixed train, window #2); MMI_gen 8582 (partly: MMI_gen 5336 (partly: valid));(3) MMI_gen 8582 (partly: MMI_gen 7510);(4) MMI_gen 4394 (partly: enabled [previous], disabled [next]); MMI_gen 4396 (partly: Next, NA18.2, Previous, NA18); MMI_gen 4358;          
            */

            /*
            Test Step 6
            Action: Perform action step 2-5 for ‘Previous’ button.
            Expected Result: See the expected result of step 2-5 and the following points,(1)   The state of ‘Previous’ and ‘Next’ button are displayed as follows,‘Next’ button is enabled, displays as symbol NA17  ‘Previous’ button is enabled, displays as symbol NA19
            Test Step Comment: (1) MMI_gen 4394 (partly: enabled [next], disabled [previous]); MMI_gen 4396 (partly: Next, NA17, Previous, NA19); MMI_gen 4358; 
            */

            /*
            Test Step 7
            Action: Use the test script file 22_7_2_a.xml to send EVC-13 with,MMI_X_DRIVER_ID =0MMI_NID_OPERATION = 4294967295MMI_M_DATA_ENABLE = 256MMI_N_CAPTION_TRAINSET = 13MMI_X_CAPTION_TRAINSET =0 (Note: All index of this variable are same)MMI_NID_KEY_TRAIN_CAT = 21MMI_L_TRAIN = 4096MMI_M_BRAKE_PERC = 9MMI_V_MAXTRAIN = 601MMI_NID_KEY_AXLE_LOAD = 20MMI_M_AIRTIGHT = 3MMI_NID_KEY_LOAD_GAUGE = 33MMI_N_CAPTION_NETWORK = 17MMI_X_CAPTION_NETWORK = 0(Note: All index of this variable are same)MMI_NID_RBC =8388608MMI_NID_RADIO = 0xFFFFFFFFFFFFFFFFMMI_N_VBC = 0
            Expected Result: Verify the following information,DMI displays the following information respectively with blank value:Page 1:Driver IDTrain running numberPage 2:Radio Network IDRBC IDRBC Phone Number
            Test Step Comment: (1) MMI_gen 8586 (partly: modify by other ETCS external source); MMI_gen 8582 (partly: MMI_gen 5336 (partly: NEGATIVE, display only Driver ID/Train running number/ Radio Network ID/ RBC ID/ RBC Phone number));
            */

            /*
            Test Step 8
            Action: Use the test script file 22_7_2_b.xml to send EVC-13 with,MMI_X_DRIVER_ID = 0x31323334363738393132333436373839MMI_M_DATA_ENABLE = 255MMI_N_CAPTION_NETWORK = 16MMI_X_CAPTION_TRAINSET[0] = 65MMI_X_CAPTION_TRAINSET[1] = 66MMI_X_CAPTION_TRAINSET[2] = 67MMI_X_CAPTION_TRAINSET[3] = 68MMI_X_CAPTION_TRAINSET[4] = 69MMI_X_CAPTION_TRAINSET[5] = 70MMI_X_CAPTION_TRAINSET[6] = 71MMI_X_CAPTION_TRAINSET[7] = 72MMI_X_CAPTION_TRAINSET[8] = 73MMI_X_CAPTION_TRAINSET[9] = 74MMI_X_CAPTION_TRAINSET[10] = 75MMI_X_CAPTION_TRAINSET[11] = 76MMI_X_CAPTION_TRAINSET[12] =77MMI_X_CAPTION_TRAINSET[13] = 78MMI_X_CAPTION_TRAINSET[14] = 79MMI_X_CAPTION_TRAINSET[15] = 80MMI_NID_RADIO = 0x9999999999999999
            Expected Result: Verify the following information,(1)   The data part of following information are automatically insert a line brake at the end of first line, represented as 2 lines.Page 1:Driver IDPage 2:Radio Network IDRBC Phone Number
            Test Step Comment: (1) MMI_gen 7514;
            */

            /*
            Test Step 9
            Action: End of test.
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
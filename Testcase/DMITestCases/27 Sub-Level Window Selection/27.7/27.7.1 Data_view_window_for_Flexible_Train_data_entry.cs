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
    /// 27.7.1 Data view window for Flexible Train data entry
    /// TC-ID: 22.7.1
    /// 
    /// This test case verifies the display of the ‘Data View’ window that displays information of ‘Flexible Train Data’. The display of the Data view window shall comply with [ERA-ERTMS] and [MMI-ETCS-gen].
    /// 
    /// Tested Requirements:
    /// MMI_gen 2482; MMI_gen 230; MMI_gen 8586; MMI_gen 9428; MMI_gen 8582 (partly: MMI_gen 5338, MMI_gen 5383 (partly: MMI_gen 5944 (partly: touchscreen)), MMI_gen 5337, MMI_gen 5339, MMI_gen 5336 (partly: valid, NEGATIVE, display only Driver ID/Train runnung number/ Radio Network ID/ RBC Phone number), MMI_gen 7510, MMI_gen 5306 (partly: Close button, Previous button, Next button, Window title)); MMI_gen 8583; MMI_gen 8584 (partly: ETCS); MMI_gen 8585 (partly: Flexible train); MMI_gen 4392 (partly: [Previous : NA19], [Next: NA17], [Close] NA11, returning to the parent window); MMI_gen 4394 (partly: next, previous); MMI_gen 4396 (party: next, previous); MMI_gen 4350; MMI_gen 4351; MMI_gen 4353; MMI_gen 4354; MMI_gen 4358; MMI_gen 7514; MMI_gen 9391 (partly: [Next]);
    /// 
    /// Scenario:
    /// Parameter ‘TR_OBU_TrainType’ is set to 2 (Flexible Train Data)Start of Mission is completed in SR mode, level 1The ‘Data View’ window is opened.The ‘Data View’ window is verified.The up-type buttons ‘Next’ and ‘Previous’ are verified.
    /// 
    /// Used files:
    /// 22_7_1_a.xml, 22_7_1_b.xml
    /// </summary>
    public class TC_ID_22_7_1_Sub_Level_Window : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // All value of Parameter ‘TR_OBU_TrainType’ is set to 2 (Flexible Train Data) in defaultValues_default.xml in OTE.Test system is powered on SoM is performed in SR mode, Level 1.

            // Call the TestCaseBase PreExecution
            base.PreExecution();

            DmiActions.Complete_SoM_L1_SR(this);
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in SR mode, level 1.
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in SR mode, Level 1.");

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint
            TraceInfo("This test case requires an ATP configuration change - " +
                      "See Precondition requirements. If this is not done manually, the test may fail!");

            /*
            Test Step 1
            Action: Press ‘Data view’ button
            Expected Result: DMI displays Data view window.Verify the following information,Use the log file to confirm that DMI sends EVC-101 with variable MMI_MREQUEST = 21 (Start Train Data View)The ‘Data View’ window is displayed.DMI receives EVC-13 with variable:MMI_M_DATA_ENABLE (#0) = 0 to disable the label of ‘Train Set ID’MMI_M_DATA_ENABLE (#1) = 1 to display the label of ‘Train Category’MMI_M_DATA_ENABLE (#2) = 1 to display the label of ‘Train Length’MMI_M_DATA_ENABLE (#3) = 1 to display the label of ‘Brake Percentage’MMI_M_DATA_ENABLE (#4) = 1 to display the label of ‘Max. Train Speed’MMI_M_DATA_ENABLE (#5) = 1 to display the label of ‘Axle Load CategoryMMI_M_DATA_ENABLE (#6) = 1 to display the label of ‘Airtightness’MMI_M_DATA_ENABLE (#7) = 1 to display the label of ‘Loading Guage’Data View WindowThe Data view window is covered in main area D, F and G LayersThe level of layers in each area of window as follows,Layer 0: Area D, F, G, E10, E11, Z, YLayer -1: Area A1, (A2+A3)*, A4, B*, C1, (C2+C3+C4)*, C5, C6, C7, C8, C9, E1, E2, E3, E4, (E5-E9)*.Layer -2: Area B3, B4, B5, B6 and B7.LabelThe data view text is composed of a Label Part and Data Part The labels of data view items are right aligned.The data of data view items are left aligned.Data view text colour is grey.The different topics (i.e., Train running number and Train data entry) are separated by one empty line.The Data Part is displayed only a valid value.The window title is displayed with text ‘Data view(1/2)’.Data View ItemsDMI displays the following information respectively:Driver IDTrain running numberTrain categoryLength (m)Brake percentageMaximum speed (km/h)Axle load categoryAirtightLoading gaugeThe following objects are displayed in Data View window   Enabled Close button (NA11)   Disabled Previous button (NA19)   Enabled Next button (NA17)Window titleGeneral property of windowThe Data view window is  presented with objects, text messages and which is the one of several levels and allocated to areas of DMI. All objects, text messages and buttons are presented within the same layer.The Default window is not covering the current window.Sub-level window covers partially depending on the size of the Sub-Level window. There is no other window is displayed and activated at the same time
            Test Step Comment: (1) MMI_gen 2482;(2) MMI_gen 230 (partly: open);          (3) MMI_gen 8586 (partly: modify by the driver); MMI_gen 9428; MMI_gen 230 (partly: EVC-13);(4) MMI_gen 8582 (partly: MMI_gen 5338);  (5) MMI_gen 8582 (partly: MMI_gen 5383 (partly: MMI_gen 5944 (partly: touchscreen)));(6) MMI_gen 8582 (partly: MMI_gen 5335);  (7) MMI_gen 8582 (partly: MMI_gen 5340 (partly: right aligned));  (8) MMI_gen 8582 (partly: MMI_gen 5342 (partly: left aligned));  (9) MMI_gen 8582 (partly: MMI_gen 5337);   (10) MMI_gen 8582 (partly: MMI_gen 5339);  (11) MMI_gen 8582 (partly: MMI_gen 5336 (partly: valid));         (12) MMI_gen 8583;    (13) MMI_gen 8584 (partly: ETCS); MMI_gen 8585 (partly: Flexible train, window #1);    (14) MMI_gen 8582 (partly: MMI_gen 5306 (partly: Close button, Previous button, Next button, Window title)); MMI_gen 4392 (partly: [Previous : NA19], [Next: NA17], [Close] NA11); MMI_gen 4396 (partly: Previous, NA19, Next, NA17); MMI_gen 4394 (partly: disabled [previous]);(15) MMI_gen 4350;(16) MMI_gen 4351;(17) MMI_gen 4353;(18) MMI_gen 4354;
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press the ‘Data view’ button");

            EVC101_MMIDriverRequest.CheckMRequestReleased = Variables.MMI_M_REQUEST.StartTrainDataView;

            EVC13_MMIDataView.MMI_M_DATA_ENABLE = MMI_M_DATA_ENABLE.TrainCategory |
                                                  MMI_M_DATA_ENABLE.TrainLength |
                                                  MMI_M_DATA_ENABLE.BrakePercentage |
                                                  MMI_M_DATA_ENABLE.MaxTrainSpeed |
                                                  MMI_M_DATA_ENABLE.AxleLoadCategory |
                                                  MMI_M_DATA_ENABLE.AirTightness |
                                                  MMI_M_DATA_ENABLE.LoadGauge;
            //                       &  ~MMI_M_DATA_ENABLE.TrainSetID;
            EVC13_MMIDataView.MMI_X_DRIVER_ID = "1";
            EVC13_MMIDataView.MMI_NID_OPERATION = 0;
            EVC13_MMIDataView.MMI_NID_KEY_TRAIN_CAT = Variables.MMI_NID_KEY.PASS1;
            EVC13_MMIDataView.MMI_L_TRAIN = 100;
            EVC13_MMIDataView.MMI_M_BRAKE_PERC = 70;
            EVC13_MMIDataView.MMI_V_MAXTRAIN = 160;
            EVC13_MMIDataView.MMI_NID_KEY_AXLE_LOAD = Variables.MMI_NID_KEY.CATA;
            EVC13_MMIDataView.MMI_M_AIRTIGHT = 0;
            EVC13_MMIDataView.MMI_NID_KEY_LOAD_GAUGE = Variables.MMI_NID_KEY.OutofGC;
            EVC13_MMIDataView.Send();

            // The window title is displayed with text ‘Data view(1 / 2)’
            WaitForVerification("Check the following (* indicates sub-areas drawn as one area):" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Data View window." + Environment.NewLine +
                                "2. The Data View window covers areas D, F and G." + Environment.NewLine +
                                "3. Layer 0 comprises areas D, F, G, E10, E11, Z and Y." + Environment.NewLine +
                                "4. Layer 1 comprises areas A1, (A2+A3)*, A4, B, C1, (C2+C3+C4)*, C5, C6, C7, C8, C9, E1, E2, E3, E4, (E5-E9)*." + Environment.NewLine +
                                "5. Layer 2 comprises areas B3, B4, B5, B6 and B7." + Environment.NewLine +
                                "6. Text in the data view has a Label Part, right-aligned, and a Data Part, left-aligned." + Environment.NewLine +
                                "7. Data View text is grey" + Environment.NewLine +
                                "8. Different topics such as Train running number and Train data entry are separated by an empty line." + Environment.NewLine +
                                "9. Data Parts only display valid values." +
                                "10. The window title is labelled with text ‘Data view(1/2)’." + Environment.NewLine +
                                "11. DMI displays the information about the following: DriverID, Train running number, Train category, Length(m), Brake percentage, " + Environment.NewLine +
                                "12. Maximum speed(km/h), Axle load category, Airtight" + Environment.NewLine +
                                "13. DMI displays the ‘Enabled Close’ button (symbol NA11), the ‘Disabled Previous’ button (symbol NA19) and the ‘Enabled Next’ button (symbol NA17)." + Environment.NewLine +
                                "14. Objects, text messages and buttons can be displayed in several levels. Within a level they are allocated to areas." + Environment.NewLine +
                                "15. Objects, text messages and buttons in a layer form a window." + Environment.NewLine +
                                "16. The Default window does not cover the current window." + Environment.NewLine +
                                "17. A sub-level window can partially cover another window, depending on its size. Another window cannot be displayed and activated at the same time.");
            
            /*
            Test Step 2
            Action: Press and hold ‘Next’ button
            Expected Result: Verify the following information,(1)   The state of button is changed to ‘Pressed’, the border of button is removed.(2)   The sound ‘Click’ is played once
            Test Step Comment: (1) MMI_gen 9391 (partly: [Next], MMI_gen 4381 (partly: change to state ‘Pressed’ as long as remain actuated));(2) MMI_gen 9391 (partly: [Next], MMI_gen 4381 (partly: sound ‘Click’));
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press and hold the ‘Next’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the ‘Next’ button pressed with no border." + Environment.NewLine +
                                "2. The ‘Click’ sound is played once.");

            /*
            Test Step 3
            Action: Slide out the ‘Next’ button
            Expected Result: Verify the following information,(1)   The border of the button is shown (state ‘Enabled’) without a sound
            Test Step Comment: (1) MMI_gen 9391 (partly: [Next], MMI_gen 4382 (partly: state ‘Enabled’ when slide out with force applied, no sound));
            */
            DmiActions.ShowInstruction(this, @"Whilst keeping the ‘Next’ button pressed, drag outside its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘Next’ button is displayed enabled." + Environment.NewLine +
                                "2. No sound is played.");

            /*
            Test Step 4
            Action: Slide back into the ‘Next’ button
            Expected Result: Verify the following information,(1)   The button is back to state ‘Pressed’ without a sound
            Test Step Comment: (1) MMI_gen 9391 (partly: [Next], Train category, MMI_gen 4382 (partly: state ‘Pressed’ when slide back, no sound));
            */
            DmiActions.ShowInstruction(this, @"Whilst keeping the ‘Next’ button pressed, drag it back inside its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Next’ button is displayed pressed." + Environment.NewLine +
                                "2. No sound is played.");

            /*
            Test Step 5
            Action: Release ‘Next’ button
            Expected Result: Verify that the Data view is displayed the next page of the train data.The window title of the next page is displayed with text ‘Data view (2/2)’. Data View ItemsThe data view items are displayed correctly refer to following items,RBC IDRBC phone numberVBC set code (if any)The data part of RBC phone number is displayed as 2 lines.Navigation buttonsThe state of ‘Previous’ and ‘Next’ button are displayed as follows,  ‘Next’ button is disabled, displays as symbol NA18.2  ‘Previous’ button is enabled, displays as symbol NA18
            Test Step Comment: (1) MMI_gen 8584 (partly: ETCS), MMI_gen 8585 (partly: Flexible train, window #2); (2) MMI_gen 8585 (partly: Flexible train, window #2); MMI_gen 8582 (partly: MMI_gen 5336 (partly: valid)); MMI_gen 9391 (partly: [Next], MMI_gen 4381 (partly: exit state ‘pressed’));(3) MMI_gen 8582 (partly: MMI_gen 7510);(4) MMI_gen 4394 (partly: enabled [previous], disabled [next]); MMI_gen 4396 (partly: Next, NA18.2, Previous, NA18); MMI_gen 4358;
            */
            DmiActions.ShowInstruction(this, @"Release the ‘Next’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. DMI displays the next page of the Train data with the window title being displayed with the text ‘Data view (2/2)’." + Environment.NewLine +
                                @"2. The data view items ‘RBC ID’, ‘RBC phone number’, ‘VBC set code’ (if any) are displayed correctly." + Environment.NewLine +
                                @"3. The data part of the RBC phone number is displayed on two lines." + Environment.NewLine +
                                @"5. The ‘Next’ button is disabled (DMI displays symbol NA18.2)." + Environment.NewLine +
                                @"4. The ‘Previous’ button is enabled (DMI displays symbol NA18).");

            /*
            Test Step 6
            Action: Perform action step 2-5 for ‘Previous’ button
            Expected Result: See the expected result of step 2-5 and the following points,(1)   The state of ‘Previous’ and ‘Next’ button are displayed as follows,‘Next’ button is enabled, displays as symbol NA17  ‘Previous’ button is enabled, displays as symbol NA19
            Test Step Comment: (1) MMI_gen 4394 (partly: enabled [next], disabled [previous]); MMI_gen 4396 (partly: Next, NA17, Previous, NA19); MMI_gen 4358;
            */
            DmiActions.ShowInstruction(this, @"Press and hold the ‘Previous’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the ‘Previous’ button pressed with no border." + Environment.NewLine +
                                "2. The ‘Click’ sound is played once.");

            DmiActions.ShowInstruction(this, @"Whilst keeping the ‘Previous’ button pressed, drag outside its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘Previous’ button is displayed enabled." + Environment.NewLine +
                                "2. No sound is played.");

            DmiActions.ShowInstruction(this, @"Release the ‘Previous’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the next page of the Train data." + Environment.NewLine +
                                "2. The data view items ‘RBC ID’, ‘RBC phone number’, ‘VBC set code’ (if any) are displayed correctly." + Environment.NewLine +
                                @"3. The ‘Previous’ button is disabled (DMI displays symbol NA18.2)." + Environment.NewLine +
                                @"4. The ‘Next’ button is enabled (DMI displays symbol NA18).");

            /*
            Test Step 7
            Action: Use the test script file 22_7_1_a.xml to send EVC-13 with,MMI_X_DRIVER_ID =0MMI_NID_OPERATION = 4294967295MMI_M_DATA_ENABLE = 256MMI_N_CAPTION_TRAINSET = 13MMI_X_CAPTION_TRAINSET =0 (Note: All index of this variable are same)MMI_NID_KEY_TRAIN_CAT = 21MMI_L_TRAIN = 4096MMI_M_BRAKE_PERC = 9MMI_V_MAXTRAIN = 601MMI_NID_KEY_AXLE_LOAD = 20MMI_M_AIRTIGHT = 3MMI_NID_KEY_LOAD_GAUGE = 33MMI_N_CAPTION_NETWORK = 17MMI_X_CAPTION_NETWORK = 0(Note: All index of this variable are same)MMI_NID_RBC = 0MMI_NID_RADIO = 0xFFFFFFFFFFFFFFFFMMI_N_VBC = 0
            Expected Result: Verify the following information,DMI displays the following information respectively with blank value:Page 1:Driver IDTrain running numberPage 2:Radio Network IDRBC Phone Number
            Test Step Comment: (1) MMI_gen 8586 (partly: modify by other ETCS external source); MMI_gen 8582 (partly: MMI_gen 5336 (partly: NEGATIVE, display only Driver ID/Train running number/ Radio Network ID/ RBC Phone number);
            */
            XML_22_7_1(msgType.typea);

            WaitForVerification("Check the following (scrolling the window to see both pages):" + Environment.NewLine + Environment.NewLine +
                                "1. On page 1, DMI displays information on Driver ID and Train running number with blank values." + Environment.NewLine +
                                "2. On page 2, DMI displays information on Radio Network ID and RBC Phone Number with blank values.");
            /*
            Test Step 8
            Action: Use the test script file 22_7_1_b.xml to send EVC-13 with,MMI_X_DRIVER_ID = 0x31323334363738393132333436373839MMI_M_DATA_ENABLE = 254MMI_N_CAPTION_NETWORK = 16MMI_X_CAPTION_TRAINSET[0] = 65MMI_X_CAPTION_TRAINSET[1] = 66MMI_X_CAPTION_TRAINSET[2] = 67MMI_X_CAPTION_TRAINSET[3] = 68MMI_X_CAPTION_TRAINSET[4] = 69MMI_X_CAPTION_TRAINSET[5] = 70MMI_X_CAPTION_TRAINSET[6] = 71MMI_X_CAPTION_TRAINSET[7] = 72MMI_X_CAPTION_TRAINSET[8] = 73MMI_X_CAPTION_TRAINSET[9] = 74MMI_X_CAPTION_TRAINSET[10] = 75MMI_X_CAPTION_TRAINSET[11] = 76MMI_X_CAPTION_TRAINSET[12] =77MMI_X_CAPTION_TRAINSET[13] = 78MMI_X_CAPTION_TRAINSET[14] = 79MMI_X_CAPTION_TRAINSET[15] = 80MMI_NID_RADIO = 0x9999999999999999
            Expected Result: Verify the following information,(1)   The data part of following information are automatically insert a line brake at the end of first line, represented as 2 lines.Page 1:Driver IDPage 2:Radio Network IDRBC Phone Number
            Test Step Comment: (1) MMI_gen 7514;
            */
            XML_22_7_1(msgType.typeb);

            WaitForVerification("Check the following information is displayed with a line break inserted after the first line so that the data" + Environment.NewLine +
                                "are displayed over two lines (scrolling the window to see both pages):" + Environment.NewLine + Environment.NewLine +
                                "1. On page 1, DMI displays information on Driver ID." + Environment.NewLine +
                                "2. On page 2, DMI displays information on Radio Network ID and RBC Phone Number.");
            
            /*
            Test Step 9
            Action: Press the ‘Close’ button
            Expected Result: Verify the following information,(1)   DMI displays Default window
            Test Step Comment: (1) MMI_gen 4392 (partly: returning to the parent window);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "DMI displays the Default window.");

            /*
            Test Step 10
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
        #region Send_XML_22_7_1_DMI_Test_Specification
        enum msgType
        {
            typea,
            typeb
        }

        private void XML_22_7_1(msgType type)
        {
            switch (type)
            {
                case msgType.typea:
                    // values taken from xml not spec. where different
                    EVC13_MMIDataView.MMI_X_DRIVER_ID = "";
                    EVC13_MMIDataView.MMI_NID_OPERATION = 0xffffffff;
                    EVC13_MMIDataView.MMI_M_DATA_ENABLE = (Variables.MMI_M_DATA_ENABLE)0x0080;     // 128
                    EVC13_MMIDataView.MMI_L_TRAIN = 4096;
                    EVC13_MMIDataView.MMI_V_MAXTRAIN = 601;
                    EVC13_MMIDataView.MMI_M_BRAKE_PERC = 9;
                    EVC13_MMIDataView.MMI_NID_KEY_AXLE_LOAD = Variables.MMI_NID_KEY.FG4;       // 20
                    EVC13_MMIDataView.MMI_NID_RADIO = 0xffffffffffffffff;          // 4294967295 (= 0xffffffff) hi, 4294967295 (= 0xffffffff) lo
                    EVC13_MMIDataView.MMI_M_AIRTIGHT = 3;
                    EVC13_MMIDataView.MMI_NID_KEY_LOAD_GAUGE = Variables.MMI_NID_KEY.CATE5;
                    EVC13_MMIDataView.Trainset_Caption = "\0x00\0x00\0x00\0x00\0x00\0x00\0x00\0x00\0x00\0x00\0x00\0x00\0x00";
                    EVC13_MMIDataView.Network_Caption = "\0x00\0x00\0x00\0x00\0x00\0x00\0x00\0x00\0x00\0x00\0x00\0x00\0x00\0x00\0x00\0x00\0x00";
                    EVC13_MMIDataView.MMI_NID_KEY_TRAIN_CAT = Variables.MMI_NID_KEY.CATA;  // 21

                    break;
                case msgType.typeb:
                    // values taken from xml not spec. where different
                    EVC13_MMIDataView.MMI_X_DRIVER_ID = "12346789123426789";
                    EVC13_MMIDataView.MMI_NID_OPERATION = 0x9999999;
                    EVC13_MMIDataView.MMI_M_DATA_ENABLE = (Variables.MMI_M_DATA_ENABLE)0x7f00;     // 32512
                    EVC13_MMIDataView.MMI_L_TRAIN = 4095;
                    EVC13_MMIDataView.MMI_V_MAXTRAIN = 600;
                    EVC13_MMIDataView.MMI_M_BRAKE_PERC = 250;
                    EVC13_MMIDataView.MMI_NID_KEY_AXLE_LOAD = Variables.MMI_NID_KEY.CATA;       // 21
                    EVC13_MMIDataView.MMI_NID_RADIO = 0x9999999999999999;          // 2576980377 (= 0x9999999) hi, 2576980377 (= 0x9999999) lo
                    EVC13_MMIDataView.MMI_M_AIRTIGHT = 0;
                    EVC13_MMIDataView.MMI_NID_KEY_LOAD_GAUGE = Variables.MMI_NID_KEY.G1;
                    EVC13_MMIDataView.Trainset_Caption = "ABCDEFGHIJKL";         
                    EVC13_MMIDataView.Network_Caption = "ABCDEFGHIJKLMNOP";
                    EVC13_MMIDataView.MMI_NID_KEY_TRAIN_CAT = Variables.MMI_NID_KEY.PASS1;  // 3
                    break;
            }

            EVC13_MMIDataView.Send();
        }
        #endregion
    }
}
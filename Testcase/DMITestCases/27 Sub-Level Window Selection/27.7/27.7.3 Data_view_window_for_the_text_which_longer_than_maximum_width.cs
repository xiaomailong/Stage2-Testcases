using System;
using Testcase.Telegrams.EVCtoDMI;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 27.7.3 Data view window for the text which longer than maximum width
    /// TC-ID: 22.7.3
    /// 
    /// This test case verifies the display of Data View Texts when the text is longer than the maximum width.
    /// 
    /// Tested Requirements:
    /// MMI_gen 7512; MMI_gen 7514;
    /// 
    /// Scenario:
    /// The ‘Data View’ window is opened and verify the display information.
    /// 
    /// Used files:
    /// N/A
    /// </summary>
    public class TC_ID_22_7_3_Sub_Level_Window : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // All value of Parameter ‘TR_OBU_TrainType’ is set to 2 (Flexible Train Data) in defaultValues_default.xml in OTE.Set the following information in language_mgr.xmlRevise wording from ‘PASS1’ to be ‘For Test Data View truncated by long text’Revise wording from ‘Train category’ to be ‘For Test Data View truncated by long text’SoM is performed in SR mode, Level 1.

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
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 0;
            // Testcase entrypoint
            TraceInfo("This test case requires a DMI configuration change; See Precondition requirements. " +
                      "If this is not done manually, the test may fail!");

            TraceHeader("Test Step 1");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Press ‘Data view’ button");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following information,(1)   The label part of Data View item No.3 is changed to ‘For Test Data View truncated by long text’.  The text label which longer than the maximum width of label part is not display (truncated).(2)   The data part of Data View item No.3 is changed to ‘For Test Data View truncated by long text’.The data part is displayed as 2 lines.At the 2nd line, the text which longer than the maximum width of data part is not display (truncated)");
            /*
            Test Step 1
            Action: Press ‘Data view’ button
            Expected Result: Verify the following information,(1)   The label part of Data View item No.3 is changed to ‘For Test Data View truncated by long text’.  The text label which longer than the maximum width of label part is not display (truncated).(2)   The data part of Data View item No.3 is changed to ‘For Test Data View truncated by long text’.The data part is displayed as 2 lines.At the 2nd line, the text which longer than the maximum width of data part is not display (truncated)
            Test Step Comment: (1) MMI_gen 7512;(2) MMI_gen 7514;
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press the ‘Data view’ button");

            EVC13_MMIDataView.MMI_M_DATA_ENABLE = Variables.MMI_M_DATA_ENABLE.TrainCategory |
                                                  Variables.MMI_M_DATA_ENABLE.TrainLength |
                                                  Variables.MMI_M_DATA_ENABLE.BrakePercentage |
                                                  Variables.MMI_M_DATA_ENABLE.MaxTrainSpeed |
                                                  Variables.MMI_M_DATA_ENABLE.AxleLoadCategory |
                                                  Variables.MMI_M_DATA_ENABLE.Airtightness |
                                                  Variables.MMI_M_DATA_ENABLE.LoadingGauge;
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
            EVC13_MMIDataView.Network_Caption = "";
            EVC13_MMIDataView.Trainset_Caption = "";
            EVC13_MMIDataView.Send();


            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The label of Data view item #3 is ‘For Test Data View truncated by long text’, truncated at the maximum width of the label part." +
                                Environment.NewLine +
                                "2. The data part of Data view item #3 is ‘For Test Data View truncated by long text’, displayed on two lines" +
                                Environment.NewLine +
                                "   with the second line truncated at the maximum width of the data part.");

            TraceHeader("Test Step 2");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("End of test");
            
            /*
            Test Step 2
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
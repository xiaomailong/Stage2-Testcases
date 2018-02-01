using System;
using Testcase.Telegrams.EVCtoDMI;
using Testcase.Telegrams.DMItoEVC;

namespace Testcase.DMITestCases
{
    /// <summary>
    /// 22.1.4 Planning Area forced into background by TAF Question box
    /// TC-ID: 17.1.4
    /// 
    /// This test case verifies that planning area is forced into the background when TAF Question Box is opened but still updating information continuously.
    /// 
    /// Tested Requirements:
    /// MMI_gen 7097; MMI_gen 11470 (partly: Bit # 22);
    /// 
    /// Scenario:
    /// Perform SoM to SR mode, level 2.Drive the train forward to receive information from RBC at 70m.Message 3: Packet 15,21, 27 and 80 (Entering FS and get OS mode acknowledgement area)Continue to drive the train forward and acknowledge OS mode at position 250m.Drive the train forward to receive Track ahead free request from RBC at position 350m. Then, verify that PA is not display.Acknowledge Track ahead free. Then, verify that each objects of PA is updated refer to position of the train.
    /// 
    /// Used files:
    /// 17_1_4.tdg, 17_1_4.utt
    /// </summary>
    public class TC_ID_17_1_4_Planning_Area : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // Set the following tags name in configuration file (See the instruction in Appendix 1)HIDE_PA_OS_MODE = 1 (PA will show in OS mode)System is power on.Cabin is activate.

            // Call the TestCaseBase PreExecution
            base.PreExecution();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays OS mode, Level 2.

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 0;
            // Testcase entrypoint
            TraceInfo("This test case requires a DMI configuration change - " +
                      "See Precondition requirements. If this is not done manually, the test may fail!");

            TraceHeader("Test Step 1");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Perform SoM to SR mode, level 2");
            TraceReport("Expected Result");
            TraceInfo("DMI displays in SR mode, level 2");
            /*
            Test Step 1
            Action: Perform SoM to SR mode, level 2
            Expected Result: DMI displays in SR mode, level 2
            */
            // Call generic Check Results Method
            DmiActions.Complete_SoM_L1_SR(this);
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Level = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_LEVEL.L2;
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI is displaying Staff Responsible Mode." + Environment.NewLine +
                                "2. DMI shows that the ATP is in Level 2.");

            TraceHeader("Test Step 2");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Receive information from RBC");
            TraceReport("Expected Result");
            TraceInfo("DMI changes from SR mode to FS mode, level 2");
            /*
            Test Step 2
            Action: Receive information from RBC
            Expected Result: DMI changes from SR mode to FS mode, level 2
            */
            // Call generic Check Results Method
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.FullSupervision;
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI is displying Full Supervision Mode." + Environment.NewLine +
                                "2. DMI shows that the ATP is in Level 2.");

            TraceHeader("Test Step 3");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Acknowledge OS mode by pressing at area C1");
            TraceReport("Expected Result");
            TraceInfo("DMI changes from FS mode to OS mode, level 2");
            /*
            Test Step 3
            Action: Acknowledge OS mode by pressing at area C1
            Expected Result: DMI changes from FS mode to OS mode, level 2
            */
            // Call generic Check Results Method
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.OnSight;
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI is displying On Sight Mode." + Environment.NewLine +
                                "2. DMI shows that the ATP is in Level 2.");


            TraceHeader("Test Step 4");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Received information from RBC");
            TraceReport("Expected Result");
            TraceInfo(
                "DMI displays symbol DR02 (Confirm Track Ahead Free) in Main area D.Verify that Planning area is forced into background, and it is not display in Main area D");
            /*
            Test Step 4
            Action: Received information from RBC
            Expected Result: DMI displays symbol DR02 (Confirm Track Ahead Free) in Main area D.Verify that Planning area is forced into background, and it is not display in Main area D
            Test Step Comment: (1) MMI_gen 7097 (partly: force into the background);
            */
            // Call generic Action Method

            EVC8_MMIDriverMessage.MMI_Q_TEXT = 298;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.Send();
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Planning Area has been forced into the background." + Environment.NewLine +
                                "2. The Track Ahead Free Symbol (DR02) has been displayed." + Environment.NewLine +
                                "3. An acknowledgement is requested");


            TraceHeader("Test Step 5");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Drive the train forward");
            TraceReport("Expected Result");
            TraceInfo("The symbol DR02 is still displayed in Main area D");
            /*
            Test Step 5
            Action: Drive the train forward
            Expected Result: The symbol DR02 is still displayed in Main area D
            */
            // Call generic Action Method
            WaitForVerification("Please press the DMI button to acknowledge that the track ahead is free.");


            TraceHeader("Test Step 6");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Press ‘Yes’ button in Main area D");
            TraceReport("Expected Result");
            TraceInfo(
                "DMI displays PA in Main area D again.Verify that the following object is moving down to the bottom of area D.PASPUse the log file to confirm that DMI sends out packet [MMI_DRIVER_ACTION (EVC-152)] with the value of variable MMI_M_DRIVER_ACTION refer to sequence below,a)   MMI_M_DRIVER_ACTION = 22 (Confirmation of Track Ahead Free)");
            /*
            Test Step 6
            Action: Press ‘Yes’ button in Main area D
            Expected Result: DMI displays PA in Main area D again.Verify that the following object is moving down to the bottom of area D.PASPUse the log file to confirm that DMI sends out packet [MMI_DRIVER_ACTION (EVC-152)] with the value of variable MMI_M_DRIVER_ACTION refer to sequence below,a)   MMI_M_DRIVER_ACTION = 22 (Confirmation of Track Ahead Free)
            Test Step Comment: (1) MMI_gen 7097 (partly: Update information in background);(2) MMI_gen 11470 (partly: Bit # 22);
            */
            // Check DMI --> EVC telegrams 
            //Check MMI_DRIVER_ACTION (EVC 152)
            // Check Results
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI is displaying Planning Area.");

            EVC152_MMIDriverAction.Check_MMI_M_DRIVER_ACTION =
                EVC152_MMIDriverAction.MMI_M_DRIVER_ACTION.TrackAheadFreeConfirmation;

            //{TraceInfo("The DMI driver action to confirm TAF is CORRECT");}

            //{TraceInfo("The DMI driver action to confirm TAF is INCORRECT");}

            TraceHeader("Test Step 7");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("End of test");
            TraceReport("Expected Result");
            TraceInfo("");
            /*
            Test Step 7
            Action: End of test
            Expected Result: 
            */


            return GlobalTestResult;
        }
    }
}
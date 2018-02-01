using System;
using Testcase.Telegrams.EVCtoDMI;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 17.3.10 Speed Pointer: Colour of speed pointer in TR mode and PT mode
    /// TC-ID: 12.3.10
    /// 
    /// This test case verifies the colour of speed pointer which display refer to received packet EVC-1 and EVC-7 for TR mode and PT mode.
    /// 
    /// Tested Requirements:
    /// MMI_gen 6299 (partly: TR mode, PT mode);
    /// 
    /// Scenario:
    /// 1.Drive the train forward pass BG1 at position 50m.BG1: Packet 12, 21 and 27 (Entering FS)
    /// 2.Continue to drive the train forward pass through the movement authority (300m) to entering TR mode.
    /// 3.Drive the train with specify speed. Then, verify that the colour of speed pointer is always red.
    /// 4.Ackownledge PT mode. Then, drive the train backward with specify speed and verify that the colour of speed pointer is always grey.
    /// 
    /// Used files:
    /// 12_3_10.tdg
    /// </summary>
    public class TC_12_3_10_Train_Speed : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // Test system is powered on.Cabin is activated.SoM is performed in SR mode, Level 1.

            // Call the TestCaseBase PreExecution
            base.PreExecution();
            DmiActions.Complete_SoM_L1_SR(this);
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in PT mode, level 1

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 0;
            // Testcase entrypoint            

            TraceHeader("Test Step 1");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Drive the train forward pass BG1");
            TraceReport("Expected Result");
            TraceInfo("DMI displays in FS mode, level 1");
            /*
            Test Step 1
            Action: Drive the train forward pass BG1
            Expected Result: DMI displays in FS mode, level 1
            */
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.FullSupervision;
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 5;
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in FS mode, level 1.");

            TraceHeader("Test Step 2");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Force the train into TR mode by moving the train forward to position of EOA");
            TraceReport("Expected Result");
            TraceInfo(
                "DMI displays in TR mode, level 1.The train is forced to stop, train speed is decreasing to 0 km/h.Verify the following information,(1)   Use the log file to confirm that DMI received the EVC-7 with variable OBU_TR_M_MODE = 7 (Trip)(2)   The speed pointer is always display in red colour");
            /*
            Test Step 2
            Action: Force the train into TR mode by moving the train forward to position of EOA
            Expected Result: DMI displays in TR mode, level 1.The train is forced to stop, train speed is decreasing to 0 km/h.Verify the following information,(1)   Use the log file to confirm that DMI received the EVC-7 with variable OBU_TR_M_MODE = 7 (Trip)(2)   The speed pointer is always display in red colour
            Test Step Comment: (1) MMI_gen 6299 (partly: OBU_TR_M_MODE = 7);(2) MMI_gen 6299 (partly: colour of speed pointer, TR mode);
            */
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.Trip;
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 1;

            EVC1_MMIDynamic.MMI_O_BRAKETARGET = 0;
            // set target distance to 0 to simulate passing EOA??

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in TR mode, level 1." + Environment.NewLine +
                                "2. Is the speed pointer red?");

            TraceHeader("Test Step 3");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Perform the following procedure,Press an acknowledgement in sub-area C1.Chage the train direction to ‘Reverse’Drive the train with speed = 40 km/h");
            TraceReport("Expected Result");
            TraceInfo(
                "DMI displays in PT mode, level 1.Verify the following information,(1)   Use the log file to confirm that DMI received the EVC-7 with variable OBU_TR_M_MODE = 8 (Post trip)(2)   The speed pointer is always display in grey colour.Note: The train will be force to stop due to runaway movement is detect when the train moving back over 200m");
            /*
            Test Step 3
            Action: Perform the following procedure,Press an acknowledgement in sub-area C1.Chage the train direction to ‘Reverse’Drive the train with speed = 40 km/h
            Expected Result: DMI displays in PT mode, level 1.Verify the following information,(1)   Use the log file to confirm that DMI received the EVC-7 with variable OBU_TR_M_MODE = 8 (Post trip)(2)   The speed pointer is always display in grey colour.Note: The train will be force to stop due to runaway movement is detect when the train moving back over 200m
            Test Step Comment: (1) MMI_gen 6299 (partly: OBU_TR_M_MODE = 8);(2) MMI_gen 6299 (partly: colour of speed pointer, PT mode);
            */
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 266;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Press an acknowledgement in sub-area C1. Change the train direction to ‘Reverse’");

            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.PostTrip;
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 40;
            EVC1_MMIDynamic.MMI_V_PERMITTED_KMH = 45;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in PT mode, level 1." + Environment.NewLine +
                                "2. Is the speed pointer grey?");

            TraceHeader("Test Step 4");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("End of test");
            TraceReport("Expected Result");
            TraceInfo("");
            /*
            Test Step 4
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
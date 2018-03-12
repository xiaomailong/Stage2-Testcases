using System;
using Testcase.Telegrams.DMItoEVC;
using Testcase.Telegrams.EVCtoDMI;

namespace Testcase.DMITestCases
{
    /// <summary>
    /// 17.3.5 Speed Pointer: Colour of speed pointer in RV mode
    /// TC-ID: 12.3.5
    /// 
    /// This test case verifies the colour of speed pointer which display refer to received packet EVC-1 while the train is running in each supervision status and speed monitoring for RV mode.
    /// 
    /// Tested Requirements:
    /// MMI_gen 6299 (partly: RV mode); 
    /// 
    /// Scenario:
    /// Drive the train forward pass BG1 at position 500mBG1: packet 12, 21, 27, 138 and 139 (Entering FS mode and reversing allowance area)
    /// 2.Enter RV mode, level 
    /// 1.Then, drive the train with specify speed and verify the display of speed pointer refer to received packet EVC-1.
    /// 
    /// Used files:
    /// 12_3_5.tdg
    /// </summary>
    public class TC_12_3_5_Train_Speed : TestcaseBase
    {
        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in RV mode, level 1
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in RV mode, Level 1.");

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 21478;

            StartUp();
            DmiActions.Complete_SoM_L1_FS(this);

            // No intervention speed described here: comment is that it is less than 11, presumably 10...
            EVC1_MMIDynamic.MMI_V_INTERVENTION_KMH = 10;

            MakeTestStepHeader(1, UniqueIdentifier++, "Drive the train forward pass BG1. Then stop the train",
                "DMI displays in FS mode, Level 1 with the ST06 symbol at sub-area C6");
            /*
            Test Step 1
            Action: Drive the train forward past BG1. Then stop the train
            Expected Result: DMI displays in FS mode, Level 1 with the ST06 symbol at sub-area C6
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 5;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 286;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.AuxiliaryInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in FS mode, Level 1 with the ST06 symbol at sub-area C6.");

            EVC1_MMIDynamic.MMI_V_TRAIN = 0; // Set speed to zero


            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Speed is displayed at 0 km/h.");

            MakeTestStepHeader(2, UniqueIdentifier++,
                "Perform the following procedure, Change the train direction to reverse. Acknowledge RV mode by pressing the symbol in sub-area C1",
                "DMI displays in RV mode, Level 1");
            /*
            Test Step 2
            Action: Perform the following procedure, Change the train direction to reverse Acknowledge RV mode by pressing the symbol in sub-area C1
            Expected Result: DMI displays in RV mode, Level 1
            */
            EVC8_MMIDriverMessage.MMI_I_TEXT = 2;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 262;
            EVC8_MMIDriverMessage.Send();

            EVC152_MMIDriverAction.Check_MMI_M_DRIVER_ACTION = EVC152_MMIDriverAction.MMI_M_DRIVER_ACTION.ReversingModeAck;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.Reversing;

            WaitForVerification(
                "Change the train direction to reverse and acknowledge RV mode by pressing the symbol in sub-area C1." +
                Environment.NewLine +
                "Then check the following:" + Environment.NewLine + Environment.NewLine +
                "1. DMI displays in RV mode, Level 1.");

            MakeTestStepHeader(3, UniqueIdentifier++, "Drive the train with speed = 5 km/h",
                "Verify the following information,(1)   Use the log file to confirm that DMI received the packet information EVC-1 and EVC-7 with following variables,(EVC-7) OBU_TR_M_MODE = 14 (Reversing)(EVC-1) MMI_M_WARNING = 0 (Status = NoS, Supervision = CSM)(EVC-1) MMI_V_PERMITTED = 139 (5km/h)(2)   The speed pointer display in grey colour");
            /*
            Test Step 3
            Action: Drive the train with speed = 5 km/h
            Expected Result: Verify the following information,(1)   Use the log file to confirm that DMI received the packet information EVC-1 and EVC-7 with following variables,(EVC-7) OBU_TR_M_MODE = 14 (Reversing)(EVC-1) MMI_M_WARNING = 0 (Status = NoS, Supervision = CSM)(EVC-1) MMI_V_PERMITTED = 139 (5km/h)(2)   The speed pointer display in grey colour
            Test Step Comment: (1) MMI_gen 6299 (partly: OBU_TR_M_MODE, MMI_M_WARNING, train speed in relation to permitted speed MMI_V_PERMITTED, RV mode in CSM supervision);(2) MMI_gen 6299 (partly: colour of speed pointer, RV mode in CSM supervision);
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 5;
            EVC1_MMIDynamic.MMI_V_PERMITTED_KMH = 5;
            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Normal_Status_Ceiling_Speed_Monitoring;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Is the speed pointer grey?");

            MakeTestStepHeader(4, UniqueIdentifier++, "Increase the train speed to 6 km/h",
                "Verify the following information,(1)   Use the log file to confirm that DMI received the packet information EVC-1 with the following condition,MMI_M_WARNING = 8 (Status = OvS, Supervision = CSM) while the value of MMI_V_TRAIN = 167 (6 km/h) which greater than MMI_V_PERMITTED(2)   The speed pointer display in orange colour");
            /*
            Test Step 4
            Action: Increase the train speed to 6 km/h
            Expected Result: Verify the following information,(1)   Use the log file to confirm that DMI received the packet information EVC-1 with the following condition,MMI_M_WARNING = 8 (Status = OvS, Supervision = CSM) while the value of MMI_V_TRAIN = 167 (6 km/h) which greater than MMI_V_PERMITTED(2)   The speed pointer display in orange colour
            Test Step Comment: (1) MMI_gen 6299 (partly: MMI_M_WARNING, train speed in relation to permitted speed MMI_V_PERMITTED, RV mode in CSM supervision);(2) MMI_gen 6299 (partly: colour of speed pointer, RV mode in CSM supervision);
            */
            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Overspeed_Status_Ceiling_Speed_Monitoring;
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 6;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Is the speed pointer orange?");

            MakeTestStepHeader(5, UniqueIdentifier++,
                "Increase the train speed to 10 km/h. Note: dV_warning_max is defined in chapter 3 of [SUBSET-026]",
                "Verify the following information,(1)   Use the log file to confirm that DMI received the packet information EVC-1 with the following condition,MMI_M_WARNING = 4 (Status = WaS, Supervision = CSM) while the value of MMI_V_TRAIN = 278 (10 km/h) which greater than MMI_V_PERMITTED but lower than MMI_V_INTERVENTION(2)   The speed pointer display in orange colour");
            /*
            Test Step 5
            Action: Increase the train speed to 10 km/h.Note: dV_warning_max is defined in chapter 3 of [SUBSET-026]
            Expected Result: Verify the following information,(1)   Use the log file to confirm that DMI received the packet information EVC-1 with the following condition,MMI_M_WARNING = 4 (Status = WaS, Supervision = CSM) while the value of MMI_V_TRAIN = 278 (10 km/h) which greater than MMI_V_PERMITTED but lower than MMI_V_INTERVENTION(2)   The speed pointer display in orange colour
            Test Step Comment: (1) MMI_gen 6299 (partly: MMI_M_WARNING, train speed in relation to permitted speed MMI_V_PERMITTED, RV mode in CSM supervision);(2) MMI_gen 6299 (partly: colour of speed pointer, RV mode in CSM supervision);
            */
            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Warning_Status_Ceiling_Speed_Monitoring;
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 10;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Is the speed pointer orange?");

            MakeTestStepHeader(6, UniqueIdentifier++, "Increase the train speed to 11 km/h",
                "The train speed is force to decrease because of emergency brake is applied by ETCS onboard.Verify the following information,Before train speed is decreased(1)   Use the log file to confirm that DMI received the packet information EVC-1 with the following condition,MMI_M_WARNING = 12 (Status = IntS, Supervision = CSM) while the value of MMI_V_TRAIN = 306 (11 km/h) which greater than MMI_V_INTERVENTION(2)   The speed pointer display in red colourAfter train speed is decreased(3)   Use the log file to confirm that DMI received the packet information EVC-1 with the following condition,MMI_M_WARNING = 12 (Status = IntS, Supervision = CSM) while the value of MMI_V_TRAIN is lower than MMI_V_INTERVENTION(4)   The speed pointer display in grey colour");
            /*
            Test Step 6
            Action: Increase the train speed to 11 km/h
            Expected Result: The train speed is force to decrease because of emergency brake is applied by ETCS onboard.Verify the following information,Before train speed is decreased(1)   Use the log file to confirm that DMI received the packet information EVC-1 with the following condition,MMI_M_WARNING = 12 (Status = IntS, Supervision = CSM) while the value of MMI_V_TRAIN = 306 (11 km/h) which greater than MMI_V_INTERVENTION
              (2)   The speed pointer display in red colour after train speed is decreased
              (3)   Use the log file to confirm that DMI received the packet information EVC-1 with the following condition,
                    MMI_M_WARNING = 12 (Status = IntS, Supervision = CSM) while the value of MMI_V_TRAIN is lower than MMI_V_INTERVENTION
              (4)   The speed pointer display in grey colour
            Test Step Comment: (1) MMI_gen 6299 (partly: MMI_M_WARNING, train speed in relation to permitted speed MMI_V_PERMITTED, RV mode in CSM supervision);(2) MMI_gen 6299 (partly: colour of speed pointer, RV mode in CSM supervision);(3) MMI_gen 6299 (partly: MMI_M_WARNING, RV mode in CSM supervision);(4) MMI_gen 6299 (partly: colour of speed pointer, RV mode in CSM supervision);
            */
            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Intervention_Status_Ceiling_Speed_Monitoring;
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 11;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Is the speed pointer red?");

            // ETCS will decrease speed
            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Intervention_Status_Ceiling_Speed_Monitoring;
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 4;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Is the speed pointer grey?");

            TraceHeader("End of test");

            /*
            Test Step 7
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
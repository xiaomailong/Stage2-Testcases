using System;
using Testcase.Telegrams.EVCtoDMI;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 18.1.5 Distance to Target in RV mode
    /// TC-ID: 13.1.5
    /// 
    /// This test case verifies the presentation of the distance to target digital and the distance to target bar when DMI is displayed in RV mode.
    /// 
    /// Tested Requirements:
    /// MMI_gen 105 (partly: RV mode); MMI_gen 2567 (partly: RV mode); MMI_gen 107 (partly: ETCS mode, Table 37, RV mode);
    /// 
    /// Scenario:
    /// Activate cabin A. Performs SoM to SR mode, Level 1.Note: Set the train length to 100m during train data entry process.Drive the train forward pass BG1 at position 50mBG1: packet 12, 21 and 27Drive the train forward pass BG2 at position 200mBG2 Packet138: D_STARTREVERSE 100, L_REVERSEAREA 400        Packet139: D_REVERSE 200, V_REVERSE 30Stop the train at position 700m.Driver selects reversing mode and confirmmode change to RV mode.Drive the train backward and verify the display of distance to target on DMI.
    /// 
    /// Used files:
    /// 13_1_5.tdg
    /// </summary>
    public class TC_13_1_5_Brake : TestcaseBase
    {
        public override bool TestcaseEntryPoint()
        {
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 21916;
            // Testcase entrypoint

            StartUp();

            MakeTestStepHeader(1, UniqueIdentifier++, "Activate cabin A",
                "DMI displays in SB mode. The Driver ID window is displayed");
            /*
            Test Step 1
            Action: Activate cabin A
            Expected Result: DMI displays in SB mode. The Driver ID window is displayed
            */

            DmiActions.Set_Driver_ID(this, "1234");
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.StandBy;

            // Call generic Check Results Method
            DmiExpectedResults.Driver_ID_window_displayed_in_SB_mode(this);

            MakeTestStepHeader(2, UniqueIdentifier++,
                "Driver performs SoM to SR mode, Level 1.Note: Please set Train length = 100m during train data entry process",
                "DMI displays in SR mode, level 1");
            /*
            Test Step 2
            Action: Driver performs SoM to SR mode, Level 1.Note: Please set Train length = 100m during train data entry process
            Expected Result: DMI displays in SR mode, level 1
            */
            DmiActions.ShowInstruction(this,
                "Perform SoM to SR mode, level 1: setting Train Length = 100m in train data entry");
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Level = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_LEVEL.L1;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode =
                EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.StaffResponsible;
            DmiActions.Finished_SoM_Default_Window(this);

            // Call generic Check Results Method
            DmiExpectedResults.SR_Mode_displayed(this);

            MakeTestStepHeader(3, UniqueIdentifier++,
                "Drive the train forward passing BG1Then drive the train forward  with speed = 40 km/h in FS mode",
                "DMI changes from SR to FS mode");
            /*
            Test Step 3
            Action: Drive the train forward passing BG1Then drive the train forward  with speed = 40 km/h in FS mode
            Expected Result: DMI changes from SR to FS mode
            */

            // ?? Set an EOA so the DMI can display a target
            EVC1_MMIDynamic.MMI_O_BRAKETARGET =
                300000; // 3 km: will cause the target display to show a white arrow on top
            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Intervention_Status_PreIndication_Monitoring;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN = 0; // just starting off

            // Set the permitted speed so the current speed is allowed
            EVC1_MMIDynamic.MMI_V_PERMITTED_KMH = 10;
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 5;

            EVC1_MMIDynamic.MMI_V_PERMITTED_KMH = 70;
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 40;

            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN = 5000; // 50m
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.FullSupervision;

            // Call generic Check Results Method
            DmiExpectedResults.FS_mode_displayed(this);

            MakeTestStepHeader(4, UniqueIdentifier++, "Driving forward passing BG2", "");

            /*
            Test Step 4
            Action: Driving forward passing BG2
            Expected Result: 
            */
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN = 20000; // 200m

            // ??? No observation: still in FS mode maybe

            MakeTestStepHeader(5, UniqueIdentifier++, "The train is in reversing area",
                "DMI remains displays in FS mode");
            /*
            Test Step 5
            Action: The train is in reversing area
            Expected Result: DMI remains displays in FS mode
            */
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN = 30000; // 300m
            EVC1_MMIDynamic.MMI_O_BRAKETARGET = 70000; // in reversing area can travel 400m further ??

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI still displays the FS mode symbol (MO11) in area B7");

            MakeTestStepHeader(6, UniqueIdentifier++, "Stop the train",
                "The train is at standstill.Driver is informed that reversing is possible");
            /*
            Test Step 6
            Action: Stop the train
            Expected Result: The train is at standstill.Driver is informed that reversing is possible
            */
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 5;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 286; // Reversing possible
            EVC8_MMIDriverMessage.Send();

            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 0;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI indicates speed = 0 km/h" + Environment.NewLine +
                                "2. DMI displays message that reversing is possible and displays symbol ST06 in sub-area C6");

            MakeTestStepHeader(7, UniqueIdentifier++,
                "Change the direction of train to reverse. Then select and confirm RV mode",
                "DMI displays in RV mode, level 1.Verify the following information,Use the log file to confirm that the distance to target (bar and digital) is calculated from the received packet information EVC-7 and EVC-1 as follows,(EVC-7) OBU_TR_O_TRAIN – (EVC-1) MMI_O_BRAKE_TARGETExample: The observation point of the distance target is 407. [EVC-7.OBU_TR_O_TRAIN = 1000080700] – [EVC-1.MMI_O_BRAKETARGET = 1000040036] = 40664 (406.64 m)Use the log file to confirm that the distance to target bar is display when DMI received packet information EVC-7 with, OBU_TR_M_MODE = 14");
            /*
            Test Step 7
            Action: Change the direction of train to reverse. Then select and confirm RV mode
            Expected Result: DMI displays in RV mode, level 1.Verify the following information,Use the log file to confirm that the distance to target (bar and digital) is calculated from the received packet information EVC-7 and EVC-1 as follows,(EVC-7) OBU_TR_O_TRAIN – (EVC-1) MMI_O_BRAKE_TARGETExample: The observation point of the distance target is 407. [EVC-7.OBU_TR_O_TRAIN = 1000080700] – [EVC-1.MMI_O_BRAKETARGET = 1000040036] = 40664 (406.64 m)Use the log file to confirm that the distance to target bar is display when DMI received packet information EVC-7 with, OBU_TR_M_MODE = 14
            Test Step Comment: (1)MMI_gen 105           (partly: RV mode);                        (2) MMI_gen 2567 (partly RV mode); MMI_gen 107 (partly: ETCS mode, Table 37, RV mode);
            */
            DmiActions.ShowInstruction(this, "Change the direction of train to reverse. Select and confirm RV mode.");

            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 4;
            EVC8_MMIDriverMessage.Send();
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.Reversing;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in RV mode, level 1.");

            MakeTestStepHeader(8, UniqueIdentifier++, "End of test", "");

            /*
            Test Step 8
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
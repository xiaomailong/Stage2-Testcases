using System;
using Testcase.Telegrams.DMItoEVC;
using Testcase.Telegrams.EVCtoDMI;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 17.3.9 Speed Pointer: Colour of speed pointer in SB mode and NL mode
    /// TC-ID: 12.3.9
    /// 
    /// This test case verifies the colour of speed pointer which display refer to received packet EVC-1 and EVC-7 for SB mode and NL mode.
    /// 
    /// Tested Requirements:
    /// MMI_gen 6299 (partly: SB mode, NL mode);
    /// 
    /// Scenario:
    /// 1.Force the train roll away in SB mode. Then, verify that the colour of speed pointer is always grey.
    /// 2.Enter NL mode, level 
    /// 1.Then, drive the train with maximum speed and verify that the colour of speed pointer is always grey.
    /// 
    /// Used files:
    /// N/A
    /// </summary>
    public class TC_12_3_9_Train_Speed : TestcaseBase
    {
        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in NL mode, level 1

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in NL mode, Level 1.");

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 21581;
            // Testcase entrypoint
            StartUp();

            // Set driver ID
            DmiActions.Display_Driver_ID_Window(this, "1234");

            // Set to level 1 and SR mode
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Level = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_LEVEL.L1;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.StandBy;

            // Do we need to close any open windows?
            // EVC30_MMIRequestEnable.SendBlank();

            MakeTestStepHeader(1, UniqueIdentifier++, "Drive the train forward with speed = 10 km/h",
                "Verify the following information," +
                "(1) The speed pointer is always display in grey colour even runaway movement is detected." +
                "(2) Use the log file to confirm that DMI received packet EVC-7 with variable OBU_TR_M_MODE = 6 (Standby)");
            /*
            Test Step 1
            Action: Drive the train forward with speed = 10 km/h
            Expected Result: Verify the following information,(1)   The speed pointer is always display in grey colour even runaway movement is detected.(2)   Use the log file to confirm that DMI received packet EVC-7 with variable OBU_TR_M_MODE = 6 (Standby)
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, SB mode);(2) MMI_gen 6299 (partly: OBU_TR_M_MODE);
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 10;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Is the speed pointer grey?");

            MakeTestStepHeader(2, UniqueIdentifier++,
                "Stop the train. Press ‘Main’ button." +
                "Press and hold ‘Non-Leading’ button for at least 2 second and then release.",
                "DMI displays in NL mode, level 1.");
            /*
            Test Step 2
            Action: Stop the train.Then, perform the following procedure,Press on sub-area C9.Press ‘Main’ buttonForce the simulation to ‘Non-leading’Press and hold ‘Non-Leading’ button at least 2 second.Release the pressed button
            Expected Result: DMI displays in NL mode, level 1
            */
            EVC1_MMIDynamic.MMI_V_TRAIN = 0;

            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Main;
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.NonLeading;
            EVC30_MMIRequestEnable.Send();

            DmiActions.ShowInstruction(this,
                "Press on sub-area B7. Press ‘Main’ button." + Environment.NewLine +
                "Press and hold ‘Non - Leading’ button at least 2 seconds and then release.");

            EVC152_MMIDriverAction.Check_MMI_M_DRIVER_ACTION =
                EVC152_MMIDriverAction.MMI_M_DRIVER_ACTION.NonLeadingSelected;

            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.NonLeading;
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in NL mode, level 1.");

            MakeTestStepHeader(3, UniqueIdentifier++,
                "Drive the train with speed = 110 mph (Maximum speed range of speed dial)",
                "Verify the following information," +
                "(1) The speed pointer is always display in grey colour." +
                "(2) Use the log file to confirm that DMI received packet EVC-7 with variable OBU_TR_M_MODE = 11 (Non-leading)");
            /*
            Test Step 3
            Action: Drive the train with speed = 400 km/h (Maximum speed range of speed dial)
            Expected Result: Verify the following information,(1)   The speed pointer is always display in grey colour..(2)   Use the log file to confirm that DMI received packet EVC-7 with variable OBU_TR_M_MODE = 11 (Non-leading)
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, NL mode);(2) MMI_gen 6299 (partly: OBU_TR_M_MODE);
            */

            EVC1_MMIDynamic.MMI_V_TRAIN_MPH = 110;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Is the speed pointer grey?");

            TraceHeader("End of test");

            /*
            Test Step 4
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
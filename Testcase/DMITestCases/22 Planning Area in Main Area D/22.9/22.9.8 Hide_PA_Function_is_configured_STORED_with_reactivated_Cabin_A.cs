using System;
using Testcase.Telegrams.EVCtoDMI;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 22.9.8 Hide PA Function is configured ‘STORED’ with reactivated Cabin A
    /// TC-ID: 17.9.8
    /// 
    /// This test case verifies that the default state of Planning area is shown/hidden refer to ‘STORED’ configuration of Hide PA function included with correctness of displayed Planning area when activate ‘Hide’/’Show’ buttons after cabin re-activation.
    /// 
    /// Tested Requirements:
    /// MMI_gen 7340; MMI_gen 2996 (partly: Stored);
    /// 
    /// Scenario:
    /// Activate Cabin A.Perform SoM in SR mode, Level 1.Drive the train forward pass BG1 at position 100m.BG1: Packet 12, 21 and 27 (mode changes to FS)Press ‘Hide PA’ button.Stop the train at position 300m.De-activate cabin A and activate cabin A again.Drive the train forward pass BG2 at position 600m. Then, verify that PA is hidden by stored configuration.BG2: packet 12, 21 and 27 (mode changes to FS)
    /// 
    /// Used files:
    /// 17_9_8.tdg
    /// </summary>
    public class TC_ID_17_9_8_Hide_PA_Function_is_configured_STORED_with_reactivated_Cabin_A : TestcaseBase
    {
        public override bool TestcaseEntryPoint()
        {
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 24032;
            // Testcase entrypoint
            TraceInfo("This test case requires an ATP configuration change - " +
                      "See Precondition requirements. If this is not done manually, the test may fail!");

            MakeTestStepHeader(1, UniqueIdentifier++, "Power On the system", "The DMI displays the default window");
            /*
            Test Step 1
            Action: Power On the system
            Expected Result: The DMI displays the default window
            */
            StartUp();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Default window.");

            MakeTestStepHeader(2, UniqueIdentifier++, "Activate cabin A and Perform SoM to SR mode, Level 1",
                "The DMI displays in SR mode,  level 1");
            /*
            Test Step 2
            Action: Activate cabin A and Perform SoM to SR mode, Level 1
            Expected Result: The DMI displays in SR mode,  level 1
            */
            DmiActions.Complete_SoM_L1_SR(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in SR mode, Level 1.");

            MakeTestStepHeader(3, UniqueIdentifier++, "Drive the train forward with speed = 40 km/h pass BG1",
                "DMI displays in FS mode, Level 1 with PA in area D");
            /*
            Test Step 3
            Action: Drive the train forward with speed = 40 km/h pass BG1
            Expected Result: DMI displays in FS mode, Level 1 with PA in area D
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 40;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN = 19000;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.FullSupervision;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in FS mode, Level 1." + Environment.NewLine +
                                "2. The Planning Area is displayed in area D.");

            MakeTestStepHeader(4, UniqueIdentifier++, "Press Hide PA button",
                "The Planning area is disappeared from the area D of the DMI");
            /*
            Test Step 4
            Action: Press Hide PA button
            Expected Result: The Planning area is disappeared from the area D of the DMI
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Hide PA’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The Planning Area is removed from area D.");

            MakeTestStepHeader(5, UniqueIdentifier++, "Stop the train. Then, deactivate cabin A",
                "The train is at standstill.DMI is displays in SB mode");
            /*
            Test Step 5
            Action: Stop the train. Then, deactivate cabin A
            Expected Result: The train is at standstill.DMI is displays in SB mode
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 0;

            DmiActions.Deactivate_Cabin(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in SB mode.");

            MakeTestStepHeader(6, UniqueIdentifier++, "Activate cabin A and Perform SoM to SR mode, Level 1",
                "DMI displays in SR mode, Level 1");
            /*
            Test Step 6
            Action: Activate cabin A and Perform SoM to SR mode, Level 1
            Expected Result: DMI displays in SR mode, Level 1
            */
            DmiActions.Activate_Cabin_1(this);
            DmiActions.Set_Driver_ID(this, "1234");
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Level = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_LEVEL.L1;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode =
                EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.StaffResponsible;
            DmiActions.Finished_SoM_Default_Window(this);
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN = 36000;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in SR mode, Level 1.");

            MakeTestStepHeader(7, UniqueIdentifier++, "Drive the train forward with speed = 40 km/h pass BG2",
                "DMI displays in FS mode, Level 1.There is no PA display on DMI");
            /*
            Test Step 7
            Action: Drive the train forward with speed = 40 km/h pass BG2
            Expected Result: DMI displays in FS mode, Level 1.There is no PA display on DMI
            Test Step Comment: MMI_gen 7340;MMI_gen 2996 (partly: Stored);
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 40;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.FullSupervision;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in FS mode, Level 1." + Environment.NewLine +
                                "2. The Planning Area is not displayed in area D.");
            MakeTestStepHeader(8, UniqueIdentifier++, "Press the main area D",
                "The Hide PA button is appeared on  the area D of the DMI");
            /*
            Test Step 8
            Action: Press the main area D
            Expected Result: The Hide PA button is appeared on  the area D of the DMI
            */
            DmiActions.ShowInstruction(this, @"Press in main area D");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Hide PA’ button is displayed in area D.");

            TraceHeader("End of test");

            /*
            Test Step 9
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
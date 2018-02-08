using System;
using Testcase.Telegrams.EVCtoDMI;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 22.10.2 Zoom PA Function with Scale Up
    /// TC-ID: 17.10.2
    /// 
    /// This test case verifies the properties of ‘Scale Up’ button of Zoom PA function. When driver presses the NA03 symbol to zoom in the planning area information. The scale up button shall comply with [ERA-ERTMS] standard.
    /// 
    /// Tested Requirements:
    /// MMI_gen 7378; MMI_gen 7386; MMI_gen 7387; MMI_gen 630; MMI_gen 7373 (partly: no visible);
    /// 
    /// Scenario:
    /// Activate cabin A. Perform SoM to SR mode, level 1.Drive the train pass BG1 at 100m. Then, verify that the Zoom PA function is enabled.Press ‘Scale Up’ button until the distance scale is the lowest value. Then, verify that the scale up button is disabled.Press ‘Scale Up’ button. Then, verify that PA’s distance scale is not change. Press ‘Scale Down’ button. Then, verify that scale up button is enabled.Press ‘Hide’ button. Then, verify that the scale up button is hidden.
    /// 
    /// Used files:
    /// 17_10_2.tdg
    /// </summary>
    public class TC_ID_17_10_2_Zoom_PA_Function_with_Scale_Up : TestcaseBase
    {
        public override bool TestcaseEntryPoint()
        {
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 24157;
            // Testcase entrypoint

            MakeTestStepHeader(1, UniqueIdentifier++, "Activate cabin A",
                "DMI displays in SB mode. The Driver ID window is displayed");
            /*
            Test Step 1
            Action: Activate cabin A
            Expected Result: DMI displays in SB mode. The Driver ID window is displayed
            */
            StartUp();
            DmiActions.Set_Driver_ID(this, "1234");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in SB mode." + Environment.NewLine +
                                "2. The Driver ID window is displayed.");
            MakeTestStepHeader(2, UniqueIdentifier++, "Driver performs SoM to SR mode, level 1",
                "DMI displays in SR mode, level 1");
            /*
            Test Step 2
            Action: Driver performs SoM to SR mode, level 1
            Expected Result: DMI displays in SR mode, level 1
            */
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Level = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_LEVEL.L1;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode =
                EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.StaffResponsible;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in SR mode, Level 1.");

            MakeTestStepHeader(3, UniqueIdentifier++,
                "Driver drives the train forward with speed = 30km/h and pass BG1",
                "DMI changes from SR mode to FS mode, Level 1 with PA in area D.Verify the following information,The symbol NA03 is displayed at sub-area D9");
            /*
            Test Step 3
            Action: Driver drives the train forward with speed = 30km/h and pass BG1
            Expected Result: DMI changes from SR mode to FS mode, Level 1 with PA in area D.Verify the following information,The symbol NA03 is displayed at sub-area D9
            Test Step Comment: (1) MMI_gen 7386 (partly: NA03);
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 30;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.FullSupervision;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN = 10000;
            EVC4_MMITrackDescription.MMI_G_GRADIENT_CURR = 35;
            EVC4_MMITrackDescription.MMI_V_MRSP_CURR_KMH = 30;
            EVC4_MMITrackDescription.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DM changes from SR mode to FS mode, Level 1." + Environment.NewLine +
                                "2. The Planning Area is displayed in area D." + Environment.NewLine +
                                "3. The ‘Scale Up’ button, symbol NA03, is displayed in sub-area D9.");

            MakeTestStepHeader(4, UniqueIdentifier++, "Presses ‘Scale Up’ button",
                "DMI remains displays in FS mode.Verify the following information,The PA’s distance range is changed to [0…2000]");
            /*
            Test Step 4
            Action: Presses ‘Scale Up’ button
            Expected Result: DMI remains displays in FS mode.Verify the following information,The PA’s distance range is changed to [0…2000]
            Test Step Comment: (1) MMI_gen 7386 (partly: operable zoom PA);
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Scale Up’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DM still displays in FS mode." + Environment.NewLine +
                                "2. The Planning Area distance range changes to 0..2000.");

            MakeTestStepHeader(5, UniqueIdentifier++, "Press ‘Scale Up’ button until the distance range is [0…1000]",
                "Verify the following information,Verify that the Zoom PA function is switched the PA’s distance range to the next lower range and the visualisation of the PA are updated accordingly.When the distance range is [0…1000], the symbol NA05 is displayed in sub-area D9");
            /*
            Test Step 5
            Action: Press ‘Scale Up’ button until the distance range is [0…1000]
            Expected Result: Verify the following information,Verify that the Zoom PA function is switched the PA’s distance range to the next lower range and the visualisation of the PA are updated accordingly.When the distance range is [0…1000], the symbol NA05 is displayed in sub-area D9
            Test Step Comment: (1) MMI_gen 630; (2) MMI_gen 7387;
            */
            DmiActions.ShowInstruction(this,
                @"Press the ‘Scale Up’ button until the distance range changes to 0..1000.");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The Planning Area scale displays according to the range 0..1000." +
                                Environment.NewLine +
                                "2. The (enabled) ‘Scale Up’ symbol, NA03, is removed and the disabled ‘Scale Up’ symbol, NA05, is displayed in sub-area D9.");

            MakeTestStepHeader(6, UniqueIdentifier++, "Press ‘Scale Up’ button",
                "Verify the following information,Verify that The ‘Scale Up’ button of the operable Zoom PA function is disabled, visualisation of the PA are not change");
            /*
            Test Step 6
            Action: Press ‘Scale Up’ button
            Expected Result: Verify the following information,Verify that The ‘Scale Up’ button of the operable Zoom PA function is disabled, visualisation of the PA are not change
            Test Step Comment: (1) MMI_gen 7378 (partly: disable);
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Scale Up’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The Planning Area scale does not change." + Environment.NewLine +
                                "2. The Zoom PA function is disabled.");

            MakeTestStepHeader(7, UniqueIdentifier++, "Press ‘Scale Down’ button",
                "Verify the following information,The PA’s distance range is changed to [0…2000] and the symbol NA03 is displayed in sub-area D9");
            /*
            Test Step 7
            Action: Press ‘Scale Down’ button
            Expected Result: Verify the following information,The PA’s distance range is changed to [0…2000] and the symbol NA03 is displayed in sub-area D9
            Test Step Comment: (1) MMI_gen 7378 (partly: opposite case); 
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press the ‘Scale Down’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The Planning Area scale displays according to the range 0..2000." +
                                Environment.NewLine +
                                "2. The (disabled) ‘Scale Up’ symbol, NA05, is removed and the (enabled) ‘Scale Up’ symbol, NA03, is displayed in sub-area D9.");

            MakeTestStepHeader(8, UniqueIdentifier++,
                "Driver presses ‘Hide’ button at position top-right of planning area in sub-area D14",
                "The Planning information area is  disappeared from DMI.Verify that the ‘Scale Up’ and ‘Scale Down’ buttons are removed");
            /*
            Test Step 8
            Action: Driver presses ‘Hide’ button at position top-right of planning area in sub-area D14
            Expected Result: The Planning information area is  disappeared from DMI.Verify that the ‘Scale Up’ and ‘Scale Down’ buttons are removed
            Test Step Comment: (1) MMI_gen 7373 (partly: no visible);
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Hide’ button in the top right of sub-area D14");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Planning Information is removed." + Environment.NewLine +
                                "2. The ‘Scale Up’ and ‘Scale Down’ buttons are removed.");

            MakeTestStepHeader(9, UniqueIdentifier++, "End of test", "");

            /*
            Test Step 9
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
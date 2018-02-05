using System;
using Testcase.Telegrams.EVCtoDMI;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 22.10.3 Zoom PA Function with Scale Down
    /// TC-ID: 17.10.3
    /// 
    /// This test case verifies the properties of Scale Down button in Zoom PA function. When driver pressed the NA04 symbol to zoom out the planning area information, the scale range on the planning area shall change. The Scale down button shall comply with [ERA-ERTMS] standard.
    /// 
    /// Tested Requirements:
    /// MMI_gen 7379; MMI_gen 7384; MMI_gen 7385; MMI_gen 7388; MMI_gen 7373;
    /// 
    /// Scenario:
    /// Activate cabin A. Perform SoM to SR mode, level 1.Drive the train pass BG1 at 100m.Driver the train follow with permitted speed. Then, verify that the Scale Down button is enabled.Press ‘Scale Down’ button until the highest distance range. Then, verify that the scale down button is disabled.Press ‘Scale Down’ button. Then, verify that PA’s distance scale is not change. Press ‘Scale Up’ button. Then, verify that scale down button is enabled.Press ‘Hide’ button. Then, verify that the scale down button is hidden.
    /// 
    /// Used files:
    /// 17_10_3.tdg
    /// </summary>
    public class TC_ID_17_10_3_Zoom_PA_Function_with_Scale_Down : TestcaseBase
    {

        public override bool TestcaseEntryPoint()
        {
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 0;
            // Testcase entrypoint

            MakeTestStepHeader(1, UniqueIdentifier++, "Activate cabin A",
                "DMI displays the default window. The Driver ID window is displayed");
            /*
            Test Step 1
            Action: Activate cabin A
            Expected Result: DMI displays the default window. The Driver ID window is displayed
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
            DmiActions.Finished_SoM_Default_Window(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in SR mode, Level 1.");

            MakeTestStepHeader(3, UniqueIdentifier++, "Drive the train forward pass BG1",
                "DMI changes from SR mode to FS mode, Level 1 with PA in area D.Verify the following information,The symbol NA04 is displayed at sub-area D12");
            /*
            Test Step 3
            Action: Drive the train forward pass BG1
            Expected Result: DMI changes from SR mode to FS mode, Level 1 with PA in area D.Verify the following information,The symbol NA04 is displayed at sub-area D12
            Test Step Comment: (1) MMI_gen 7384 (partly: NA04);
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 30;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.FullSupervision;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN = 10000;
            EVC4_MMITrackDescription.MMI_G_GRADIENT_CURR = 0;
            EVC4_MMITrackDescription.MMI_V_MRSP_CURR_KMH = 30;
            EVC4_MMITrackDescription.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI changes from SR to FS mode, Level 1." + Environment.NewLine +
                                "2. The Planning Area is displayed in area D." + Environment.NewLine +
                                "3. The ‘Scale Down’ button is displayed with symbol NA04 in sub-area D12.");

            MakeTestStepHeader(4, UniqueIdentifier++, "Presses ‘Scale Down’ button",
                "DMI remains displays in FS mode.Verify the following information,The PA’s distance range is changed to [0…8000]");
            /*
            Test Step 4
            Action: Presses ‘Scale Down’ button
            Expected Result: DMI remains displays in FS mode.Verify the following information,The PA’s distance range is changed to [0…8000]
            Test Step Comment: (1) MMI_gen 7384 (partly: operable zoom PA);
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Scale Down’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DM still displays in FS mode." + Environment.NewLine +
                                "2. The Planning Area distance range changes to 0..8000.");

            MakeTestStepHeader(5, UniqueIdentifier++,
                "Presses ‘Scale Down’ button until the distance range is [0…32000]",
                "Verify the following information,Verify that the Zoom PA function is switched the PA’s distance range to the next higher range and the visualisation of the PA are updated accordingly.When the distance range is [0…32000], the symbol NA06 is displayed in sub-area D12");
            /*
            Test Step 5
            Action: Presses ‘Scale Down’ button until the distance range is [0…32000]
            Expected Result: Verify the following information,Verify that the Zoom PA function is switched the PA’s distance range to the next higher range and the visualisation of the PA are updated accordingly.When the distance range is [0…32000], the symbol NA06 is displayed in sub-area D12
            Test Step Comment: (1) MMI_gen 7388;   (2) MMI_gen 7385;
            */
            DmiActions.ShowInstruction(this,
                @"Press the ‘Scale Down’ button until the distance range changes to 0..32000");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The Planning Area scale is displayed according to the range 0..32000." +
                                Environment.NewLine +
                                "2. The (enabled) ‘Scale Down’ symbol, NA04, is removed and the disabled ‘Scale Down’ symbol, NA06, is displayed in sub-area D12.");

            MakeTestStepHeader(6, UniqueIdentifier++, "Press ‘Scale Down’ button",
                "Verify the following information,Verify that The ‘Scale Down’ button of the operable Zoom PA function is disabled, visualisation of the PA are not change");
            /*
            Test Step 6
            Action: Press ‘Scale Down’ button
            Expected Result: Verify the following information,Verify that The ‘Scale Down’ button of the operable Zoom PA function is disabled, visualisation of the PA are not change
            Test Step Comment: (1) MMI_gen 7379 (partly: disable);  
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Scale Down’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The Planning Area scale does not change." + Environment.NewLine +
                                "2. The Zoom PA function is disabled.");

            MakeTestStepHeader(7, UniqueIdentifier++, "Press ‘Scale Up’ button",
                "Verify the following information,The PA’s distance range is changed to [0…16000] and the symbol NA04 is displayed in sub-area D12");
            /*
            Test Step 7
            Action: Press ‘Scale Up’ button
            Expected Result: Verify the following information,The PA’s distance range is changed to [0…16000] and the symbol NA04 is displayed in sub-area D12
            Test Step Comment: (1) MMI_gen 7379 (partly: opposite case); 
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Scale Up’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The Planning Area scale is displayed according to the range 0..16000." +
                                Environment.NewLine +
                                "2. The (disabled) ‘Scale Down’ symbol, NA06, is removed and the enabled ‘Scale Down’ symbol, NA04, is displayed in sub-area D12.");

            MakeTestStepHeader(8, UniqueIdentifier++,
                "Driver presses ‘Hide’ button at position top-right of planning area in sub-area D14",
                "Verify the following information,The Planning information area and the ‘Scale Down’ buttons are disappeared from DMI");
            /*
            Test Step 8
            Action: Driver presses ‘Hide’ button at position top-right of planning area in sub-area D14
            Expected Result: Verify the following information,The Planning information area and the ‘Scale Down’ buttons are disappeared from DMI
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
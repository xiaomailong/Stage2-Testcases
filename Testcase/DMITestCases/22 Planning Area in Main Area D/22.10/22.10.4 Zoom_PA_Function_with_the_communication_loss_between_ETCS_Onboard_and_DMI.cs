using System;
using Testcase.Telegrams.EVCtoDMI;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 22.10.4 Zoom PA Function with the communication loss between ETCS Onboard and DMI
    /// TC-ID: 17.10.4
    /// 
    /// This test case verifies the Zoom PA function of planning area when communication between ETCS Onboard and DMI is lost.
    /// 
    /// Tested Requirements:
    /// MMI_gen 7393; MMI_gen 7394;
    /// 
    /// Scenario:
    /// Activate cabin A. Perform SoM to SR mode, level 1.Drive the train pass BG1 at 100m.Press ‘Scale Up’ button and simulate the communication loss between ETCS Onboard and DMI. Then, verify that the Zoom PA function is disabled.Re-establish the communication again. Then, verify that the Zoom PA function is not changed.Press ‘Scale Up’ and ‘Scale Down’ button. Then, verify that Zoom PA function is operable when communication is re-established.
    /// 
    /// Used files:
    /// 17_10_4.tdg
    /// </summary>
    public class TC_ID_17_10_4_Zoom_PA_Function_with_the_communication_loss_between_ETCS_Onboard_and_DMI : TestcaseBase
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
                "DMI changes from SR mode to FS mode with PA in area D");
            /*
            Test Step 3
            Action: Drive the train forward pass BG1
            Expected Result: DMI changes from SR mode to FS mode with PA in area D
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 30;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.FullSupervision;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN = 10000;
            EVC4_MMITrackDescription.MMI_G_GRADIENT_CURR = 0;
            EVC4_MMITrackDescription.MMI_V_MRSP_CURR_KMH = 30;
            EVC4_MMITrackDescription.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI changes from SR to FS mode, Level 1." + Environment.NewLine +
                                "2. The Planning Area is displayed in area D.");

            MakeTestStepHeader(4, UniqueIdentifier++,
                "Driver presses ‘Scale up’ button for selects distance range to [0…1000]",
                "The distance scale range of PA is presented with the range [0…1000]");
            /*
            Test Step 4
            Action: Driver presses ‘Scale up’ button for selects distance range to [0…1000]
            Expected Result: The distance scale range of PA is presented with the range [0…1000]
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Scale Up’ button until the distance range is 0..1000");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The Planning Area scale displays according to the range 0..1000.");

            MakeTestStepHeader(5, UniqueIdentifier++,
                "Simulate the communication loss between ETCS onboard and DMI.ExamplePause OTE/ATPRemove connection between DMI and PC (MVB or TCP-IP)",
                "DMI displays message “ATP Down Alarm” with sound alarm.Verify that the planning area is removed from DMI");
            /*
            Test Step 5
            Action: Simulate the communication loss between ETCS onboard and DMI.ExamplePause OTE/ATPRemove connection between DMI and PC (MVB or TCP-IP)
            Expected Result: DMI displays message “ATP Down Alarm” with sound alarm.Verify that the planning area is removed from DMI
            Test Step Comment: (1) MMI_gen 7393 (partly: symbols);
            */
            DmiActions.Simulate_communication_loss_EVC_DMI(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. DMI displays the message ‘ATP Down Alarm’." + Environment.NewLine +
                                @"2. The ‘Alarm’ sound is played." + Environment.NewLine +
                                "3. The Planning Area is removed.");

            MakeTestStepHeader(6, UniqueIdentifier++,
                "Perform the following procedure,Press at sub-area D9 twice.Press at sub-area D12.Re-establish the communication  between ETCS onboard and DMI.ExampleStart OTE/ATPConnect DMI to PC (MVB or TCP-IP)",
                "DMI displays in FS mode again. The Planning Area information and Zoom PA function are resumed. The distance scale range is displayed with the range [0…1000]");
            /*
            Test Step 6
            Action: Perform the following procedure,Press at sub-area D9 twice.Press at sub-area D12.Re-establish the communication  between ETCS onboard and DMI.ExampleStart OTE/ATPConnect DMI to PC (MVB or TCP-IP)
            Expected Result: DMI displays in FS mode again. The Planning Area information and Zoom PA function are resumed. The distance scale range is displayed with the range [0…1000]
            Test Step Comment: (1) MMI_gen 7394 (partly: symbols);(2) MMI_gen 7393 (partly: sensitive areas, inoperable);         
            */
            DmiActions.ShowInstruction(this, @"Press twice in sub-areas D9 and D12");
            DmiActions.Re_establish_communication_EVC_DMI(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The Planning Area is re-displayed." + Environment.NewLine +
                                "2. The distance scale is 0..1000.");

            MakeTestStepHeader(7, UniqueIdentifier++, "Press ‘Scale Down’ button",
                "Verify the following information,The distance scale range of PA is presented with the range [0…2000]");
            /*
            Test Step 7
            Action: Press ‘Scale Down’ button
            Expected Result: Verify the following information,The distance scale range of PA is presented with the range [0…2000]
            Test Step Comment: (1) MMI_gen 7394 (partly: operable);
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Scale Down’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The Planning Area distance scale changes to 0..2000.");

            MakeTestStepHeader(8, UniqueIdentifier++, "Press ‘Scale Up’ button",
                "Verify the following information,The distance scale range of PA is presented with the range [0…1000]");
            /*
            Test Step 8
            Action: Press ‘Scale Up’ button
            Expected Result: Verify the following information,The distance scale range of PA is presented with the range [0…1000]
            Test Step Comment: (1) MMI_gen 7394 (partly: operable);
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Scale Up’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The Planning Area distance scale changes to 0..1000.");

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
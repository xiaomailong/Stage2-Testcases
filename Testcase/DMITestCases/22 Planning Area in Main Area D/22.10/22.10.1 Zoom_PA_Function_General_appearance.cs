using System;
using Testcase.Telegrams.EVCtoDMI;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 22.10.1 Zoom PA Function: General appearance
    /// TC-ID: 17.10.1
    /// 
    /// This test case verifies the Zoom PA function of planning area when driver presses the NA03 or NA04 symbol to zoom in/zoom out the planning area information.PA, Scale up button and Scale down button are removed from the DMI when the Main-Area D is used by TAF.
    /// 
    /// Tested Requirements:
    /// MMI_gen 7370; MMI_gen 7372; MMI_gen 7373; MMI_gen 7390;
    /// 
    /// Scenario:
    /// Perform SoM to SR mode, level 
    /// 2.Then, verify that PA, Scale up button and Scale down button are not displayed .Drive the train forward to receive information from RBC at 70m.Message 3: Packet 15,21,27 and 80 (Entering FS and get OS mode acknowledgement area)Continue to drive the train forward and acknowledge OS mode at position 250m.Stop the train and perform the following steps to verify that ‘Scale Up’ and ‘Scale Down’ button are operable as up-type button,- Press and hold button.- Slide out the button.- Slide back into the button.- Release the pressed area.Drive the train forward to receive Track ahead free request from RBC at position 350m. Then, verify that ‘Scale Up’ and ‘Scale Down’ button are not display.Acknowledge Track ahead free, The PA is reappear with ‘Scale Up’ and ‘Scale Down’ button.Driver the train forward to receive information from RBC at 500m. Then, verify that ‘Scale Up’ and ‘Scale Down’ button are not display.Message 3: Packet 15,21 27 (Entering TR mode)Stop the train.
    /// 
    /// Used files:
    /// 17_10_1.tdg, 17_10_1.utt
    /// </summary>
    public class TC_ID_17_10_1_Zoom_PA_Function_General_appearance : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // Set the following tags name in configuration file (See the instruction in Appendix 1)
            // HIDE_PA_OS_MODE = 1 (PA will show in OS mode)

            // Call the TestCaseBase PreExecution
            base.PreExecution();

            // System is power on.Cabin is activate.
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in TR mode, Level 2

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint
            TraceInfo("This test case requires an ATP configuration change - " +
                      "See Precondition requirements. If this is not done manually, the test may fail!");

            /*
            Test Step 1
            Action: Perform SoM to SR mode, level 2.Then, drive the train forward
            Expected Result: DMI displays in SR mode, level 2.Verify that the Zoom PA function is not enabled when DMI displays in SR mode
            Test Step Comment: (1) MMI_gen 7373;
            */
            // tested elsewhere: force SoM
            DmiActions.Complete_SoM_L1_SR(this);
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Level = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_LEVEL.L2;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in SR mode, Level 1." + Environment.NewLine +
                                "2. The Zoom PA function is not enabled.");

            /*
            Test Step 2
            Action: Received information from RBC
            Expected Result: DMI changes from SR mode to FS mode, level 2
            */
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.FullSupervision;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in FS mode, Level 2." + Environment.NewLine +
                                "2. Planning Information is displayed." + Environment.NewLine +
                                "3. The Zoom PA function is enabled.");

            /*
            Test Step 3
            Action: Acknowledge OS mode by press at area C1
            Expected Result: DMI changes from FS mode to OS mode, level 2
            */
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 275;
            EVC8_MMIDriverMessage.Send();

            DmiActions.ShowInstruction(this, "Acknowledge OS mode by pressing in sub-area C1");
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.OnSight;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in OS mode, Level 2." + Environment.NewLine +
                                "2. Planning Information is still displayed." + Environment.NewLine +
                                "3. The Zoom PA function is still enabled.");

            /*
            Test Step 4
            Action: Stop the train.Then, perform the following procedurePress and hold ‘Scale Up’ button.Slide out ‘Scale Up’ button.Slide back into ‘Scale Up’ button.Release the pressed area
            Expected Result: Verify the following information,The PA distance scale is not change until driver release at the ‘Scale Up’ button area
            Test Step Comment: (1) MMI_gen 7372;           MMI_gen 7370 (partly: Scale Up);
            */
            DmiActions.ShowInstruction(this, @"Press and hold the ‘Scale Up’ button.");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The PA distance scale does not change.");

            DmiActions.ShowInstruction(this, @"Whilst keeping the ‘Scale Up’ button pressed, drag it out of its area.");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The PA distance scale does not change.");

            DmiActions.ShowInstruction(this,
                @"Whilst keeping the ‘Scale Up’ button pressed, drag it back inside its area.");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The PA distance scale does not change.");

            DmiActions.ShowInstruction(this, @"Release the ‘Scale Up’ button.");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The PA distance scale changes.");

            /*
            Test Step 5
            Action: Perform the following procedurePress and hold ‘Scale Down’ button.Slide out ‘Scale Down’ button.Slide back into ‘Scale Down’ button.Release the pressed area
            Expected Result: Verify the following information,The PA distance scale is not change until driver release at the ‘Scale Down’ button area
            Test Step Comment: (1) MMI_gen 7372;           MMI_gen 7370 (partly: Scale Down);
            */
            DmiActions.ShowInstruction(this, @"Press and hold the ‘Scale Down’ button.");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The PA distance scale does not change.");

            DmiActions.ShowInstruction(this,
                @"Whilst keeping the ‘Scale Down’ button pressed, drag it out of its area.");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The PA distance scale does not change.");

            DmiActions.ShowInstruction(this,
                @"Whilst keeping the ‘Scale Down’ button pressed, drag it back inside its area.");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The PA distance scale does not change.");

            DmiActions.ShowInstruction(this, @"Release the ‘Scale Down’ button.");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The PA distance scale changes.");

            /*
            Test Step 6
            Action: Drive the train forward
            Expected Result: DMI still displays as OS mode, Level 2
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 10;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI still displays in OS mode, Level 2.");

            /*
            Test Step 7
            Action: Received information from RBC
            Expected Result: DMI display symbol DR02 (Confirm Track Ahead Free) in Main area D.Verify that ‘Scale Up’ and ‘Scale Down’ button at sub-area D9 and D12 are removed, not display on DMI
            Test Step Comment: (1) MMI_gen 7390;
            */
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 298;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the ‘Track Ahead Free’ symbol, DR02, in area D." +
                                Environment.NewLine +
                                "2. The ‘Scale Up’ and ‘Scale Down’ buttons in sub-areas D9 and D12 are not displayed.");

            /*
            Test Step 8
            Action: Acknowledge Track Ahead Free by press ‘Yes’ button in Main area D
            Expected Result: The PA is reappear in Main area D
            */
            DmiActions.ShowInstruction(this, @"Acknowledge Track Ahead Free by pressing the ‘Yes’ button in area D");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Planning Information is re-displayed." + Environment.NewLine +
                                "3. The Zoom PA function is enabled.");

            /*
            Test Step 9
            Action: Drive the train pass over EOA
            Expected Result: The train is tripped. DMI displays the symbol of TR and driver is required to acknowledge TR mode to PT mode.SB &EB applied.The Planning area which including ‘Scale Up’ and ‘Scale Down’ buttons is removed from DMI
            Test Step Comment: (1) MMI_gen 7373;
            */
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.Trip;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_EB_Status = 1;
            // Show ST01
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 260;
            EVC8_MMIDriverMessage.Send();

            // Ack for TR mode
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 2;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 266;
            EVC8_MMIDriverMessage.Send();

            DmiActions.ShowInstruction(this, @"Acknowledge TR mode");

            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.PostTrip;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in PT mode." + Environment.NewLine +
                                "2. The Brake Intervention symbol, ST01 is displayed in sub-area C9." +
                                Environment.NewLine +
                                "3. Planning Information is removed." + Environment.NewLine +
                                "4. The Zoom PA function is disabled.");

            // Remove Brake Intervention symbol
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 4;
            EVC8_MMIDriverMessage.Send();

            /*
            Test Step 10
            Action: Stop the train
            Expected Result: The train is at standstill
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 10;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The speed displayed is 0 km/h.");

            /*
            Test Step 11
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
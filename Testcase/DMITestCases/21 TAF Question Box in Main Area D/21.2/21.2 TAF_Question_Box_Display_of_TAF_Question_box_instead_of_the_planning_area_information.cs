using System;
using Testcase.Telegrams.EVCtoDMI;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 21.2 TAF Question Box: Display of TAF Question box instead of the planning area information
    /// TC-ID: 16.2
    /// 
    /// This test case verifies that planning area information is forced into the backgound while TAF Question box is display.
    /// 
    /// Tested Requirements:
    /// MMI_gen 7096; 
    /// 
    /// Scenario:
    /// Perform SoM to SR mode, level 
    /// 2.Then, verify that PA, Scale up button and Scale down button are not displayed .Drive the train forward to receive information from RBC at 70m.Message 3: Packet 15,21,27 and 80 (Entering FS and get OS mode acknowledgement area)Continue to drive the train forward and acknowledge OS mode at position 250m.Drive the train forward to receive Track ahead free request from RBC at position 350m. Then, verify that Planning area with specified buttons are removed from area D.
    /// 
    /// Used files:
    /// 16_2.tdg, 16_2.utt
    /// </summary>
    public class TC_ID_16_2_TAF_Question_Box : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // Set the following tags name in configuration file (See the instruction in Appendix 1)HIDE_PA_OS_MODE = 1 (PA will show in OS mode)System is power on.Cabin is activate.

            // Call the TestCaseBase PreExecution
            base.PreExecution();

            DmiActions.Start_ATP();

            DmiActions.Activate_Cabin_1(this);
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint

            /*
            Test Step 1
            Action: Perform SoM to SR mode, level 2.Then, drive the train forward with speed = 30km/h
            Expected Result: DMI displays in SR mode, level 2
            */
            // The above steps are done in 21.1 so not repeating set in SoM level 2
            DmiActions.Set_Driver_ID(this, "1234");

            // Set to level 1 and SR mode
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Level = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_LEVEL.L2;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode =
                EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.StaffResponsible;

            // Enable standard buttons including Start, and display Default window.
            DmiActions.Finished_SoM_Default_Window(this);

            /*
            Test Step 2
            Action: Received information from RBC
            Expected Result: DMI changes from SR mode to FS mode, level 2
            */
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.FullSupervision;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in FS mode, Level 2.");

            /*
            Test Step 3
            Action: Acknowledge OS mode by press at area C1
            Expected Result: DMI changes from FS mode to OS mode, level 2
            */
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.OnSight;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 259;
            EVC8_MMIDriverMessage.Send();

            DmiActions.ShowInstruction(this, "Acknowledge OS mode by pressing in area C1");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in OS mode, Level 2.");

            /*
            Test Step 4
            Action: Received information from RBC.Then, stop the train
            Expected Result: Verify the following information,TAF Question box is displayed in area D and force PA information into background.The area D is displayed only TAF Question box.The following buttons are removed from area D,Scale Up button (sub-area D9)Scale Down button (sub-area D12).Hide button (sub-area D14)
            Test Step Comment: (1) MMI_gen 7096 (partly: Placed in Main-Area D);(2) MMI_gen 7096 (partly: 1st bullet);(3) MMI_gen 7096 (partly: 3rd bullet, 4th bullet);
            */
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 298;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the TAF Question box in area D, with PA information in the background." +
                                Environment.NewLine +
                                "2. DMI displays only the TAF Question box in Area D." + Environment.NewLine +
                                "3. DMI does not display the ‘Scale Up’ button in sub-area D9." + Environment.NewLine +
                                "4. DMI does not display the ‘Scale Down’ button in sub-area D12." +
                                Environment.NewLine +
                                "5. DMI does not display the ‘Hide’ button in sub-area D14.");

            /*
            Test Step 5
            Action: Press at any location in area D (except ‘Yes’ button in TAF Question box)
            Expected Result: Verify the following information,PA information is not displayed even pressed in any point of area D
            Test Step Comment: (1) MMI_gen 7096 (partly: 2nd bullet);
            */
            DmiActions.ShowInstruction(this, "Press in area D outside the ‘Yes’ button in the TAF Question box");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI does not display PA information whatever part of area D is pressed.");
            /*
            Test Step 6
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
using System;
using Testcase.Telegrams.EVCtoDMI;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 17.7.2 Release Speed Digital is removed when communication between ETCS Onboard and DMI is lost
    /// TC-ID: 12.7.2
    /// 
    /// This test case verifies the appearance of the Release Speed digital while the communication between ETCS Onboard and DMI is lost.
    /// After the communication is re-established, the Release Speed digital shall display on DMI according to [MMI-ETCS-gen]
    /// 
    /// Tested Requirements:
    /// MMI_gen 6588 (partly: Release speed removal); MMI_gen 6589 (partly: Release speed re-appeared);
    /// 
    /// Scenario:
    /// Activate cabin A. 
    /// 1.Driver enters the Driver ID and performs brake test. 
    /// 2.Then select level 1, enter and validate the train data. 
    /// 3.Enter the Train running number, select and confirm SR mode. 
    /// 4.Start driving the train forward and pass BG1 at position 250m, BG1: pkt 12, pkt 21 and pkt 
    /// 275.Mode change to FS mode and then continue until release speed supervision is entered
    /// 6.Driver simulates the communication loss between ETCS Onboard and DMI. Then re-establishes the communication again.
    /// 
    /// Used files:
    /// 12_7_2.tdg
    /// </summary>
    public class TC_12_7_2_Train_Speed : TestcaseBase
    {
        public override bool TestcaseEntryPoint()
        {
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 21738;
            // Testcase entrypoint

            MakeTestStepHeader(1, UniqueIdentifier++, "Activate cabin A", "ATP is in SB mode.DMI displays in SB mode");
            /*
            Test Step 1
            Action: Activate cabin A
            Expected Result: ATP is in SB mode.DMI displays in SB mode
            */
            // Call generic Action Method
            StartUp();

            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.StandBy;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. ATP is in SB mode." + Environment.NewLine +
                                "2. DMI displays in SB mode.");

            MakeTestStepHeader(2, UniqueIdentifier++, "Driver performs SoM to SR mode, Level 1",
                "ATP enters SR mode, Level 1.DMI displays in SR mode");
            /*
            Test Step 2
            Action: Driver performs SoM to SR mode, Level 1
            Expected Result: ATP enters SR mode, Level 1.DMI displays in SR mode
            */
            DmiActions.Complete_SoM_L1_SR(this);
            
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. ATP enters SR mode, Level 1." + Environment.NewLine +
                                "2. DMI displays in SR mode.");

            MakeTestStepHeader(3, UniqueIdentifier++, "Drive the train forward passing BG1",
                "DMI changes mode from SR to FS");
            /*
            Test Step 3
            Action: Drive the train forward passing BG1
            Expected Result: DMI changes mode from SR to FS
            */
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.FullSupervision;
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 10;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI changes mode from SR to FS.");

            MakeTestStepHeader(4, UniqueIdentifier++, "When the supervision status is RSM",
                "The Release Speed digital is displayed at sub-area B6");
            /*
            Test Step 4
            Action: When the supervision status is RSM
            Expected Result: The Release Speed digital is displayed at sub-area B6
            */
            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Indication_Status_Release_Speed_Monitoring;
            EVC1_MMIDynamic.MMI_V_RELEASE_MPH = 20;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The digital Release Speed (20 mph) is displayed at sub-area B6.");

            MakeTestStepHeader(5, UniqueIdentifier++,
                "Stop the train and simulate the communication loss between ETCS Onboard and DMI",
                "DMI displays the message “ATP Down Alarm” with sound alarm. Verify that the release speed digital is removed from DMI’s screen. The toggling function is reset to default state");
            /*
            Test Step 5
            Action: Stop the train and simulate the communication loss between ETCS Onboard and DMI
            Expected Result: DMI displays the message “ATP Down Alarm” with sound alarm.Verify that the release speed digital is removed from DMI’s screen. The toggling function is reset to default state
            Test Step Comment: MMI_gen 6588 (partly: Release speed removal);
            */
            EVC1_MMIDynamic.MMI_V_TRAIN = 0;
            DmiActions.Simulate_communication_loss_EVC_DMI(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the  message “ATP Down Alarm” with sound alarm." + Environment.NewLine +
                                "2. Digital Release Speed is not displayed. " +
                                "3. FOR SOFT KEY SCREEN TYPE ONLY: The toggling function is inactive (symbol DR01 is NOT displayed in area F7.)");

            MakeTestStepHeader(6, UniqueIdentifier++, "Re-establish the communication between ETCS onboard and DMI",
                "DMI displays in FS mode and the release speed digital re-appear. The toggling function is applied");
            /*
            Test Step 6
            Action: Re-establish the communication between ETCS onboard and DMI
            Expected Result: DMI displays in FS mode and the release speed digital is re-appear. The toggling function is applied
            Test Step Comment: MMI_gen 6589 (partly: Release speed re-appear);    
            */
            // Call generic Action Method
            DmiActions.Re_establish_communication_EVC_DMI(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in FS mode." + Environment.NewLine +
                                "2. Digital Release Speed is re-displayed" + Environment.NewLine +
                                "3. FOR SOFT KEY SCREEN TYPE ONLY: Toggling function is active (symbol DR01 is displayed in area F7.)");

            DmiActions.ShowInstruction(this, "Press in area F7 to activate the toggling function");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Digital Release Speed is not displayed.");

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
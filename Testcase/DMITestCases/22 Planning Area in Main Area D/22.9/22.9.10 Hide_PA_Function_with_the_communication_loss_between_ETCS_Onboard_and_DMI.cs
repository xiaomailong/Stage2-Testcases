using System;
using Testcase.Telegrams.EVCtoDMI;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 22.9.10 Hide PA Function with the communication loss between ETCS Onboard and DMI
    /// TC-ID: 17.9.10 (Default Configuration)
    /// 
    /// This test case verifies that the properties of  Hide PA function when the communication between ETCS Onboard and DMI loss. In this state, it shall not affect to the state of the  Hide PA function. The Hide PA function shall comply with conditions of  [MMI-ETCS-gen].
    /// 
    /// Tested Requirements:
    /// MMI_gen 7357; MMI_gen 7358;
    /// 
    /// Scenario:
    /// Active cabin A. Perform start of mission to SR mode, Level 
    /// At 100m ,pass BG1 with pkt12, pkt21 and pkt 27.
    /// Mode changes to FS mode. Then simulate the communication loss between ETCS onboard and DMI is lost. The DMI displays ATP Down Alarm. 
    /// After that the communication re-establishes again. DMI displays the planning area with enabled Hide PA function 
    /// 
    /// Used files:
    /// 17_9_10.tdg
    /// </summary>
    public class TC_ID_17_9_10_Hide_PA_Function_with_the_communication_loss_between_ETCS_Onboard_and_DMI : TestcaseBase
    {
        public override bool TestcaseEntryPoint()
        {
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 0;
            // Testcase entrypoint

            MakeTestStepHeader(1, UniqueIdentifier++, "Activate cabin A. Driver performs SoM to SR mode, level 1",
                "DMI displays in SR mode, level 1");
            /*
            Test Step 1
            Action: Activate cabin A. Driver performs SoM to SR mode, level 1
            Expected Result: DMI displays in SR mode, level 1
            */
            // Tested elsewhere: force SoM
            StartUp();
            DmiActions.Complete_SoM_L1_SR(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in SR mode, Level 1.");

            MakeTestStepHeader(2, UniqueIdentifier++, "Drive the train forward with speed = 40 km/h pass BG1",
                "DMI displays the Planning area The “Entering FS” message is shown. The Hide PA function is enabled and locate at Main area D");
            /*
            Test Step 2
            Action: Drive the train forward with speed = 40 km/h pass BG1
            Expected Result: DMI displays the Planning area The “Entering FS” message is shown. The Hide PA function is enabled and locate at Main area D
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 40;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN = 10000;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.FullSupervision;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 274;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the message ‘Entering FS’." + Environment.NewLine +
                                "2. The Planning Area is displayed in area D." + Environment.NewLine +
                                "3. The ‘Hide PA’ button (enabled) is displayed in area D.");

            // Remove the message
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 4;
            EVC8_MMIDriverMessage.Send();

            MakeTestStepHeader(3, UniqueIdentifier++, "Simulate the communication loss between ETCS onboard and DMI",
                "DMI displays “ATP Down Alarm” message with sound alarm.Verify that the planning area and Hide PA function is disappeared");
            /*
            Test Step 3
            Action: Simulate the communication loss between ETCS onboard and DMI
            Expected Result: DMI displays “ATP Down Alarm” message with sound alarm.Verify that the planning area and Hide PA function is disappeared
            Test Step Comment: (1) MMI_gen 7357;
            */
            DmiActions.Simulate_communication_loss_EVC_DMI(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the message ‘ATP Down Alarm’." + Environment.NewLine +
                                "2. The Planning Area is removed from area D." + Environment.NewLine +
                                "3. The ‘Hide PA’ button is not displayed in area D.");

            MakeTestStepHeader(4, UniqueIdentifier++, "Press at Main area D",
                "Verify the following information,PA is not be resumed to display on DMI");
            /*
            Test Step 4
            Action: Press at Main area D
            Expected Result: Verify the following information,PA is not be resumed to display on DMI
            Test Step Comment: (1) MMI_gen 7357 (partly: sensitive areas, inoperable);
            */
            DmiActions.ShowInstruction(this, @"Press in Main area D");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The Planning Area is not re-displayed in area D.");

            MakeTestStepHeader(5, UniqueIdentifier++, "Re-establish the communication between ETCS onboard and DMI",
                "DMI displays in FS mode, level 1.Verify that DMI displays the planning area and Hide PA button is resumed");
            /*
            Test Step 5
            Action: Re-establish the communication between ETCS onboard and DMI
            Expected Result: DMI displays in FS mode, level 1.Verify that DMI displays the planning area and Hide PA button is resumed
            Test Step Comment: (1) MMI_gen 7358;
            */
            DmiActions.Re_establish_communication_EVC_DMI(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in FS mode, Level 1." + Environment.NewLine +
                                "2. The Planning Area is re-displayed in area D." + Environment.NewLine +
                                "3. The ‘Hide PA’ button (enabled) is re-displayed in area D.");

            MakeTestStepHeader(6, UniqueIdentifier++, "Press Hide PA button",
                "The Planning Information is disappeared from main area D");
            /*
            Test Step 6
            Action: Press Hide PA button
            Expected Result: The Planning Information is disappeared from main area D
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Hide PA’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The Planning Area is removed from area D.");

            MakeTestStepHeader(7, UniqueIdentifier++, "Press at Main area D",
                "Verify the following information,PA is resumed to display at main area D");
            /*
            Test Step 7
            Action: Press at Main area D
            Expected Result: Verify the following information,PA is resumed to display at main area D
            Test Step Comment: (1) MMI_gen 7358 (partly: sensitive areas, operable);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press in Main area D");

            MakeTestStepHeader(8, UniqueIdentifier++, "Press Hide PA button",
                "The Planning Information is disappeared from main area D");
            /*
            Test Step 8
            Action: Press Hide PA button
            Expected Result: The Planning Information is disappeared from main area D
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press Hide PA button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The Planning Area is not displayed in area D.");

            MakeTestStepHeader(9, UniqueIdentifier++, "Simulate the communication loss between ETCS onboard and DMI",
                "DMI displays “ATP Down Alarm” message with sound alarm and Hide PA function is disappeared");
            /*
            Test Step 9
            Action: Simulate the communication loss between ETCS onboard and DMI
            Expected Result: DMI displays “ATP Down Alarm” message with sound alarm and Hide PA function is disappeared
            */
            DmiActions.Simulate_communication_loss_EVC_DMI(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the message ‘ATP Down Alarm’." + Environment.NewLine +
                                "2. The Planning Area is not displayed in area D." + Environment.NewLine +
                                "3. The ‘Hide PA’ button is not displayed in area D." + Environment.NewLine +
                                "4. The ‘Alarm’ sound is played.");

            MakeTestStepHeader(10, UniqueIdentifier++, "Re-establish the communication between ETCS onboard and DMI",
                "Verify the following information,Verify that DMI is not displays the planning area and Hide PA button because state of Hide PA is no affected");
            /*
            Test Step 10
            Action: Re-establish the communication between ETCS onboard and DMI
            Expected Result: Verify the following information,Verify that DMI is not displays the planning area and Hide PA button because state of Hide PA is no affected
            Test Step Comment: (1) MMI_gen 7358 (partly: state of Hide PA);
            */
            DmiActions.Re_establish_communication_EVC_DMI(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The Planning Area is not displayed in area D." + Environment.NewLine +
                                "2. The ‘Hide PA’ button is not displayed in area D.");

            MakeTestStepHeader(11, UniqueIdentifier++, "End of test", "");

            /*
            Test Step 11
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
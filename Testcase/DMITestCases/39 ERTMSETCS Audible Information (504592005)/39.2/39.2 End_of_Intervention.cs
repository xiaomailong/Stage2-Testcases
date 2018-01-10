using System;
using Testcase.Telegrams.EVCtoDMI;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 39.2 End_of_Intervention
    /// TC-ID: 36.2
    /// 
    /// This test case verifies that sound ‘S3_end_of_intervention.wav’ is played once when intervention status is no more active if variable [EVC-1.MMI_M_WARNING] is changed from value 12 or 13 to other value.
    /// 
    /// Tested Requirements:
    /// MMI_gen 12038; MMI_gen 12039
    /// 
    /// Scenario:
    /// 1. Perform SoM to Level 1 in SR mode.
    /// 2. Drive the train forward with speed at 40 km/h.
    /// 3. Train runs pass BG1 at position 100 m.that contained pkt 12, pkt 21 and pkt 27 to enter FS mode.
    /// 4. Accelerate the train with max acceleration (100% throttle) until train speed at 107 km/h.
    /// 5. Stop the train.
    /// 6. Deactivate cabin A and power off the system.
    /// 7. Power on the system and perform SoM to Level 1 in SR mode.
    /// 8. Drive the train forward with speed at 40 km/h.
    /// 9. Train runs pass BG1 at position 100 m.that contained pkt 12, pkt 21 and pkt 27 to enter FS mode.
    /// 10. Accelerate the train with max acceleration (100% throttle) until train speed at 80 km/h.
    /// 11. Wait until current train speed above permitted speed and intervention status in TSM is active.
    /// 12. Stop the train.
    /// 
    /// Used files:
    /// 36_2.tdg
    /// </summary>
    public class TC_ID_36_2_End_of_Intervention : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // System is power on.

            // Call the TestCaseBase PreExecution
            base.PreExecution();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in FS mode, Level 1.

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint

            /*
            Test Step 1
            Action: Perform SoM to Level 1 in SR mode
            Expected Result: ETCS OB enters SR mode in Level 1
            */
            // tested elsewhere force SoM
            DmiActions.Complete_SoM_L1_SR(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in SR mode, Level 1.");

            /*
            Test Step 2
            Action: Drive the train forward with speed at 40 km/h
            Expected Result: The train can drive forward and all brakes are not applied
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 40;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI does not display the ‘Emergency brake’ symbol, ST01, in sub-area C9.");

            /*
            Test Step 3
            Action: Train runs pass BG1
            Expected Result: ETCS OB enters FS mode in Level 1            
            */
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.FullSupervision;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in FS mode, Level 1.");

            /*
            Test Step 4
            Action: Accelerate the train with max acceleration (100% throttle) until train speed at 107 km/h. 
            Expected Result: (1) Use the DMI appearance and log file to verify that train enters intervention status in CSM.DMI Appearance- The DMI displays dark grey colour on CSG from 0 km/h to Vperm.- The DMI displays red colour on CSG from Vperm to Vtrain. Log FileUse log file to verify that the DMI receives EVC-1 with the change value of variable ‘MMI_M_WARNING’ from value 12 to other value.(2) Sound ‘S3_end_of_intervention.wav’ is played once after emergency brake is deactivated with disappearance of symbol ST01. Note The intervention status information is deactivated as soon as there is no more emergency brake command from the speed and monitoring function, refers to section 6.5.7.2.4 in [MMI-ETCS-gen].            
            Test Step Comment: 
            */
            EVC1_MMIDynamic.MMI_V_PERMITTED = 100;
            EVC1_MMIDynamic.MMI_V_INTERVENTION_KMH = 107;
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 107;
            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Intervention_Status_Ceiling_Speed_Monitoring;

            // Display ST01
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 260;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The permitted speed hook is displayed on the CSG at from 100 to 107 km/h in red." +
                                Environment.NewLine +
                                "2. The CSG is coloured Dark-grey up to the permitted speed (100 km/h)" +
                                Environment.NewLine +
                                "3. The speed pointer displays 107 km/h and is coloured red." + Environment.NewLine +
                                "4. DMI displays the ‘Emergency brake’ symbol, ST01, in sub-area C9.");

            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 90;
            // Value other than 12 (Intervention_Status_Ceiling_Speed_Monitoring) or 13 (Intervention_Status_Indication_Status_Target_Speed_Monitoring)
            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Warning_Status_Indication_Status_Target_Speed_Monitoring;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 4;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI plays the ‘S3_end_of_intervention.wav’ sound once." + Environment.NewLine +
                                "2. DMI removes symbol ST01 from sub-area C9.");

            /*
            Test Step 5
            Action: Stop the train.
            Expected Result: The train is at standstill.
            Test Step Comment:
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 0;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The speed pointer displays 0 km/h.");

            /*
            Test Step 6
            Action: Deactivate cabin A and power off the system.
            Expected Result: System is power off and DMI displays ‘No contact with ATP’.
            */
            DmiActions.Deactivate_Cabin(this);

            DmiActions.ShowInstruction(this, "Power off the system");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the message ‘No contact with ATP’");

            /*
            Test Step 7
            Action: Power on the system and perform SoM to Level 1 in SR mode.
            Expected Result: ETCS OB enters SR mode in Level 1.
            */
            DmiActions.ShowInstruction(this,
                "Wait for at least 10s after powering off the system, then power on the system");

            DmiActions.Complete_SoM_L1_SR(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in SR mode, Level 1");

            /*
            Test Step 8
            Action: Drive the train forward with speed at 40 km/h.
            Expected Result: The train can drive forward and all brakes are not applied.
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 40;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI does not display the ‘Emergency brake’ symbol, ST01, in sub-area C9.");
            /*
            Test Step 9
            Action: Train runs pass BG1.
            Expected Result: ETCS OB enters FS mode in Level 1.
            */
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.FullSupervision;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in FS mode, Level 1");

            /*
            Test Step 10
            Action: Accelerate the train with max acceleration (100% throttle) until speed at 80 km/h.And wait until emergency brake is applied from ETCS onboard.
            Expected Result: (1) Use the DMI appearance and log file to verify that train enters intervention status in TSM.DMI Appearance- The DMI displays yellow colour on CSG from 0 to Vperm.- The DMI displays red colour on CSG from Vperm to Vtrain. Log FileUse log file to verify that the DMI receives EVC-1 with the change value of variable ‘MMI_M_WARNING’ from value 13 to other value.(2)  Sound ‘S3_end_of_intervention.wav’ is played once after emergency brake is deactivated with disappearance of symbol ST01.  Note The intervention status information is deactivated as soon as there is no more emergency brake command from the speed and monitoring function, refers to section 6.5.7.5.2 in [MMI-ETCS-gen].
            Test Step Comment: (1) MMI_gen 12039 (partly: MMI_M_WARNING = 13); (2) MMI_gen 12038 (partly: MMI_M_WARNING = 13 is no more active); MMI_gen 12039 (partly: MMI_M_WARNING = 13 is no more active);Note DMI appearance refers to Table 33: Conditions for display and colour of the CSG in [MMI-ETCS-gen].
            */
            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Intervention_Status_Indication_Status_Target_Speed_Monitoring;
            EVC1_MMIDynamic.MMI_V_PERMITTED_KMH = 70;
            EVC1_MMIDynamic.MMI_V_INTERVENTION_KMH = 80;
            EVC1_MMIDynamic.MMI_V_TARGET_KMH = 70;
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 80;
            // In Intervention_Status_Indication_Status_Target_Speed_Monitoring

            // Display ST01
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 260;
            EVC8_MMIDriverMessage.Send();

            // Value other than 12 (Intervention_Status_Ceiling_Speed_Monitoring) or 13 (Intervention_Status_Indication_Status_Target_Speed_Monitoring)
            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Warning_Status_Indication_Status_Target_Speed_Monitoring;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The permitted speed hook is displayed on the CSG from 70 to 80 km/h in red." +
                                Environment.NewLine +
                                "2. The CSG is coloured yellow up to the speed hook 70 km/h" + Environment.NewLine +
                                "3. The speed pointer displays 80 km/h and is coloured red." + Environment.NewLine +
                                "4. DMI displays the ‘Emergency brake’ symbol, ST01, in sub-area C9.");

            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 65;
            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Intervention_Status_Indication_Status_Target_Speed_Monitoring;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 4;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI plays the ‘S3_end_of_intervention.wav’ sound once." + Environment.NewLine +
                                "2. DMI removes symbol ST01 from sub-area C9.");
            /*
            Test Step 11
            Action: Stop the train.
            Expected Result: The train is at standstill.
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 0;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The speed pointer displays 0 km/h.");

            /*
            Test Step 12
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
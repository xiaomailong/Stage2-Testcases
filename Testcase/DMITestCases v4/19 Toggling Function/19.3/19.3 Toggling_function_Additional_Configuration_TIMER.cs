using System;
using System.Collections.Generic;
using Testcase.Telegrams.EVCtoDMI;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 19.3 Toggling function: Additional Configuration ‘TIMER’
    /// TC-ID: 14.3
    /// 
    /// This case verifies the toggling function of the basic speed hooks, release speed ditial and distance to target (digital) which is configured “TIMER” on DMI in toggling-affected mode SR/OS/SH/ and non-toggling-affected mode UN/FS/TR/PT/RV. The Toggling function shall comply with [MMI-ETCS-gen].
    /// 
    /// Tested Requirements:
    /// MMI_gen 6890; MMI_gen 6892 (partly: RV mode, UN mode, FS mode, TR mode, LS mode, PT mode, Table 34, Table 38, Table 35); MMI_gen 11868; MMI_gen 6894; MMI_gen 6896; MMI_gen 6450 (partly: 3rd bullet); MMI_gen 6898 (partly: configuration ‘TIMER”); Table 34, Table 38, Information (paragraph 1) under MMI_gen 6898 (inoperable), Information (paragraph 2) under MMI_gen 6898 (re-establish)
    /// 
    /// Scenario:
    /// Drive the train forward pass BG1 at position 200mBG1: packet 12, 21, 27, 138 and 139 (Entering FS mode and reversing allowance area)Enter RV mode, Level 
    /// 1.Then, Perform the procedure in note below to verify that toggling function is disabled in RV mode.De-activate Cabin A and Activate Cabin A again.Perform SoM in SR mode, Level 1.Enter and confirm specified value of SR speed and SR distance. Then, verify that Basic speed Hooks and Distance to target (digital) are no displayed on DMI.Perform the procedure in note below to verify that toggling function is enabled in SR mode.Drive the train forward with speed below permitted pass BG2 at position 300mBG2: packet 41 (Level 0 Trainsition)Stop the train. Then, Perform the procedure in note below to verify that toggling function is disalbed in UN mode.Drive the train forward pass BG3 at position 500mBG3: packet 12, 21 and 27 (Entering FS mode)Stop the train. Then, Perform the procedure in note below to verify that toggling function is disalbed in FS mode.Drive the train forward pass BG4 at position 700mBG4: packet 12, 21, 27 and 80 (Entering OS mode)Stop the train at position 800m. Then, Perform the procedure in note below to verify that toggling function is enabled in OS mode.Drive the train forward pass BG5 at position 1000mBG5: packet 12, 21, 27 and 80 (Entering LS mode)Note: The train wiill return to FS mode at postion 900m before entering LS mode.Stop the train at position 1100m. Then, Perform the procedure in note below to verify that toggling function is disabled in LS mode.Drive the train pass EOA (over 1300m), Then, Perform the procedure in note below to verify that toggling function is disabled in TR mode.Acknowledge TR mode. Then, Perform the procedure in note below to verify that toggling function is disabled in PT mode.Force the train enter SH mode, Then, Perform the procedure in note below to verify that toggling function is enabled in SH mode.Note: Procedure for toggling function verification,Press on area A1-A4 respectively.Waiting for 10 second and verify that specifix objects are reappear.Press around areas B.Waiting for 10 second and verify that specifix objects are reappear.
    /// 
    /// Used files:
    /// 14_3.tdg
    /// </summary>
    public class TC_14_3_Toggling_Function : TestcaseBase
    {
        public override bool TestcaseEntryPoint()
        {
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 22162;
            // Testcase entrypoint
            StartUp();

            DmiActions.Complete_SoM_L1_SR(this);

            MakeTestStepHeader(1, UniqueIdentifier++, "Drive the train forward pass BG1.Then stop the train",
                "DMI displays in FS mode, Level 1 with the ST06 symbol at sub-area C6");
            /*
            Test Step 1
            Action: Drive the train forward pass BG1.Then stop the train
            Expected Result: DMI displays in FS mode, Level 1 with the ST06 symbol at sub-area C6
            */

            DmiActions.Send_FS_Mode(this);
            DmiActions.Send_RV_Permitted_Symbol(this);

            DmiExpectedResults.FS_mode_displayed(this);
            DmiExpectedResults.Driver_symbol_displayed(this, "Level 1", "LE03", "C8", true);
            DmiExpectedResults.RV_Permitted_Symbol_displayed(this);

            MakeTestStepHeader(2, UniqueIdentifier++,
                "Perform the following procedure,Chage the train direction to reversePress the symbol in sub-area C1",
                "DMI displays in RV mode, Level 1.Verify the following information,The objects below are displayed on DMI,White Basic speed HookDistance to target (digital)The objects below are not displayed on DMI,Medium-grey basic speed hookRelease Speed Digital");
            /*
            Test Step 2
            Action: Perform the following procedure,Chage the train direction to reversePress the symbol in sub-area C1
            Expected Result: DMI displays in RV mode, Level 1.Verify the following information,The objects below are displayed on DMI,White Basic speed HookDistance to target (digital)The objects below are not displayed on DMI,Medium-grey basic speed hookRelease Speed Digital
            Test Step Comment: (1) MMI_gen 6892 (partly: RV mode, Table 34 (CSM), Table 38 (CSM))(2) MMI_gen 6890 (partly: RV mode, unidentified mode, un-concerned object), Table 34 (CSM), Table 35 (CSM)
            */

            DmiActions.ShowInstruction(this, "Change the train direction to reverse");

            DmiActions.Send_RV_Mode_Ack(this);
            DmiExpectedResults.RV_Mode_Ack_requested(this);

            DmiActions.ShowInstruction(this, "Press the symbol in sub-area C1");
            DmiExpectedResults.RV_Mode_Ack_Pressed(this);

            DmiActions.Send_L1(this);
            DmiActions.Send_RV_Mode(this);

            DmiExpectedResults.RV_Mode_displayed(this);
            DmiExpectedResults.Driver_symbol_displayed(this, "Level 1", "LE03", "C8", true);

            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 4;
            EVC8_MMIDriverMessage.Send();


            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in RV mode, Level 1." + Environment.NewLine +
                                "2. DMI displays the White basic speed hook." + Environment.NewLine +
                                "3.	DMI displays the Digital distance to target." + Environment.NewLine +
                                "4. DMI does not display the Medium-grey basic speed hook." + Environment.NewLine +
                                "5. DMI does not display the Digital release speed.");

            MakeTestStepHeader(3, UniqueIdentifier++, "Press, at least twice, on area A1-A4, and area B respectively",
                "Verify the following information,The objects below are not toggled visible/invisible, (always remain the same as the previous step)White Basic speed HookMedium-grey basic speed hookDistance to target (digital)Release Speed Digital");
            /*
            Test Step 3
            Action: Press, at least twice, on area A1-A4, and area B respectively
            Expected Result: Verify the following information,The objects below are not toggled visible/invisible, (always remain the same as the previous step)White Basic speed HookMedium-grey basic speed hookDistance to target (digital)Release Speed Digital
            Test Step Comment: (1) MMI_gen 6892 (partly: Area A and B, RV mode) MMI_gen 6890 (partly: RV mode, unidentified mode, un-concerned object);
            */
            DmiActions.ShowInstruction(this, "Press on area A1-A4, and area B, respectively, at least twice");

            WaitForVerification(
                "Check that the following objects do not toggle and remain the same as in the previous step:" +
                Environment.NewLine + Environment.NewLine +
                "1. White basic speed hook." + Environment.NewLine +
                "2.	Medium-grey basic speed hook." + Environment.NewLine +
                "3. Digital distance to target." + Environment.NewLine +
                "4. Digital	release speed.");

            MakeTestStepHeader(4, UniqueIdentifier++,
                "Perform the following procedure,De-activate Cabin AActivate Cabin A",
                "DMI displays in SB mode, Level 1");
            /*
            Test Step 4
            Action: Perform the following procedure,De-activate Cabin AActivate Cabin A
            Expected Result: DMI displays in SB mode, Level 1
            */

            DmiActions.Deactivate_Cabin(this);
            Wait_Realtime(5000);
            DmiActions.Activate_Cabin_1(this);

            DmiActions.Display_Driver_ID_Window(this, "1234");

            DmiActions.Send_SB_Mode(this);
            DmiExpectedResults.SB_Mode_displayed(this);

            MakeTestStepHeader(5, UniqueIdentifier++,
                "Perform SoM in SR mode, Level 1.Note: Stopwatch is required for accuracy of test result",
                "DMI displays in SR mode, Level 1.The objects below are displayed on DMI for 10 secondsWhite basic speed hookDistance to target (digital)(2) The release speed digital is not displayed");
            /*
            Test Step 5
            Action: Perform SoM in SR mode, Level 1.Note: Stopwatch is required for accuracy of test result
            Expected Result: DMI displays in SR mode, Level 1.The objects below are displayed on DMI for 10 secondsWhite basic speed hookDistance to target (digital)(2) The release speed digital is not displayed
            Test Step Comment: (1) MMI_gen 11868 (partly: SR mode), Table 34 (CSM), Table 38 (CSM), MMI_gen 6450 (partly: 3rd bullet, SR mode), MMI_gen 6898 (partly: configuration ‘TIMER’, SR mode);(2) MMI_gen 6890 (partly: SR mode, un-concerned object), Table 35 (CSM)
            */

            DmiActions.Send_SR_Mode(this);
            DmiActions.Send_L1(this);
            DmiActions.Finished_SoM_Default_Window(this);

            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Indication_Status_Target_Speed_Monitoring;
            EVC1_MMIDynamic.MMI_O_BRAKETARGET = 200000;

            // ?? Medium-grey speed hook not mentioned
            Wait_Realtime(10000);
            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Intervention_Status_Ceiling_Speed_Monitoring;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the White Basic speed hook for 10s then removes it." +
                                Environment.NewLine +
                                "3.	DMI displays the Digital distance to target for 10s then removes it." +
                                Environment.NewLine +
                                "4. DMI does not display the Digital release speed.");

            MakeTestStepHeader(6, UniqueIdentifier++,
                "Perform the following procedure,Press ‘Spec’ button.Press ‘SR speed/distance’ button.Enter and confirm the following data,SR speed = 40 km/hSR distance = 300 mPress the speedometer onceNote: Stopwatch is required for accuracy of test result",
                "Verify the following information,The objects below are displayed on DMI for 10 secondsWhite basic speed hookMedium-grey basic speed hookDistance to target (digital)The release speed digital is not displayed");
            /*
            Test Step 6
            Action: Perform the following procedure,Press ‘Spec’ button.Press ‘SR speed/distance’ button.Enter and confirm the following data,SR speed = 40 km/hSR distance = 300 mPress the speedometer onceNote: Stopwatch is required for accuracy of test result
            Expected Result: Verify the following information,The objects below are displayed on DMI for 10 secondsWhite basic speed hookMedium-grey basic speed hookDistance to target (digital)The release speed digital is not displayed
            Test Step Comment: (1) MMI_gen 11868 (partly: SR mode);                    MMI_gen 6450 (partly: 3rd bullet, SR mode), Table 34 (not CSM), Table 38 (not CSM), MMI_gen 6898 (partly: configuration ‘TIMER’);(2) MMI_gen 6890 (partly: SR mode, un-concerned object), Table 35 (not CSM)
            */

            DmiActions.ShowInstruction(this, @"Press ‘Spec’ button");
            DmiActions.Open_the_Special_window(this);

            DmiExpectedResults.DMI_displays_Special_window(this);

            DmiActions.ShowInstruction(this, @"Press the ‘SR speed/distance’ button");
            DmiActions.Display_SR_speed_distance_window(this, 0, 0);

            DmiExpectedResults.DMI_displays_SR_speed_distance_window(this);

            DmiExpectedResults.SR_speed_distance_entered(this, 400, 30);

            EVC11_MMICurrentSRRules.MMI_L_STFF = 0;
            EVC11_MMICurrentSRRules.MMI_V_STFF = 0;
            EVC11_MMICurrentSRRules.MMI_M_BUTTONS = Variables.MMI_M_BUTTONS.BTN_YES_DATA_ENTRY_COMPLETE;
            EVC11_MMICurrentSRRules.DataElements = new List<Variables.DataElement>
            {
                new Variables.DataElement {Identifier = Variables.MMI_NID_DATA.SR_Speed, EchoText = "40", QDataCheck = Variables.Q_DATA_CHECK.All_checks_passed},
                new Variables.DataElement {Identifier = Variables.MMI_NID_DATA.SR_Distance, EchoText = "300", QDataCheck = Variables.Q_DATA_CHECK.All_checks_passed}
            };
            EVC11_MMICurrentSRRules.Send();

            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Indication_Status_Target_Speed_Monitoring;
            Wait_Realtime(10000);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the White Basic speed hook for 10s then removes it." +
                                Environment.NewLine +
                                "2.	DMI displays the Medium-grey basic speed hook for 10s then removes it." +
                                Environment.NewLine +
                                "3. DMI displays the Digital distance to target for 10s then removes it." +
                                Environment.NewLine +
                                "4. DMI does not display the Digital release speed.");

            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Intervention_Status_Ceiling_Speed_Monitoring;


            MakeTestStepHeader(7, UniqueIdentifier++,
                "Press the speedometer onceNote: Stopwatch is required for accuracy of test result",
                "Verify the following information,The objects below are displays for 10 seconds. Then, disappear.White basic speed hookMedium-grey basic speed hookDistance to target (digital)The release speed digital is still displayed");
            /*
            Test Step 7
            Action: Press the speedometer onceNote: Stopwatch is required for accuracy of test result
            Expected Result: Verify the following information,The objects below are displays for 10 seconds. Then, disappear.White basic speed hookMedium-grey basic speed hookDistance to target (digital)The release speed digital is still displayed
            Test Step Comment: (1) MMI_gen 6890 (partly: Areas A, SR mode, toggle off), MMI_gen 6896 (partly: configuration ‘TIMER’, SR mode, toggle invisible), MMI_gen 6894 (partly: SR mode);    (2) MMI_gen 6890 (partly: SR mode, un-concerned object, toggle on) , Table 35 (not CSM)
            */
            DmiActions.ShowInstruction(this, @"Press the speedometer once");

            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Indication_Status_Target_Speed_Monitoring;
            EVC1_MMIDynamic.MMI_V_RELEASE_KMH = 15;
            Wait_Realtime(10000);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the White Basic speed hook for 10s then removes it." +
                                Environment.NewLine +
                                "2.	DMI displays the Medium-grey basic speed hook for 10s then removes it." +
                                Environment.NewLine +
                                "3. DMI displays the Digital distance to target for 10s then removes it." +
                                Environment.NewLine +
                                "4. DMI displays the Digital release speed which remains visible.");

            MakeTestStepHeader(8, UniqueIdentifier++,
                "Press, only once, on area A1-A4, and area B respectively.Then, continue to drive the train forward after expected result verified.Note: Stopwatch is required for accuracy of test result",
                "Verify the following information,(1)   The objects below are displays for 10 seconds. Then, disappear.White basic speed hookMedium-grey basic speed hookDistance to target (digital)(2)   The release speed digital remains the same");
            /*
            Test Step 8
            Action: Press, only once, on area A1-A4, and area B respectively.Then, continue to drive the train forward after expected result verified.Note: Stopwatch is required for accuracy of test result
            Expected Result: Verify the following information,(1)   The objects below are displays for 10 seconds. Then, disappear.White basic speed hookMedium-grey basic speed hookDistance to target (digital)(2)   The release speed digital remains the same
            Test Step Comment: (1) MMI_gen 6890 (partly: Areas A, Area B, SR mode);                                  MMI_gen 6896 (partly: configuration ‘TIMER’, SR mode); MMI_gen 6894 (partly: SR mode); (2) MMI_gen 6890 (partly: SR mode, un-concerned object, toggle on) , Table 35 (not CSM) 
            */
            DmiActions.ShowInstruction(this, @"Press once on area A1-A4 and area B, respectively");

            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Indication_Status_Target_Speed_Monitoring;
            EVC1_MMIDynamic.MMI_V_RELEASE_KMH = 15;
            Wait_Realtime(10000);

            WaitForVerification("Check the following: " + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the White Basic speed hook for 10s then removes it." +
                                Environment.NewLine +
                                "2.	DMI displays the Medium-grey basic speed hook for 10s then removes it." +
                                Environment.NewLine +
                                "3. DMI displays the Digital distance to target for 10s then removes it." +
                                Environment.NewLine +
                                "4. Digital release speed does not change (remains visible).");

            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Intervention_Status_Ceiling_Speed_Monitoring;
            EVC1_MMIDynamic.MMI_V_RELEASE_KMH = 15;


            MakeTestStepHeader(9, UniqueIdentifier++,
                "Drive the train forward pass BG1 with speed = 20km/h (or below permitted speed).Then, press an area C1 for acknowledgement",
                "DMI displays in UN mode, Level 0.Verify the following information,The objects below are not displayed on DMI,White Basic speed HookMedium-grey basic speed hookDistance to target (digital)Release Speed Digital");
            /*
            Test Step 9
            Action: Drive the train forward pass BG1 with speed = 20km/h (or below permitted speed).Then, press an area C1 for acknowledgement
            Expected Result: DMI displays in UN mode, Level 0.Verify the following information,The objects below are not displayed on DMI,White Basic speed HookMedium-grey basic speed hookDistance to target (digital)Release Speed Digital
            Test Step Comment: MMI_gen 6892 (partly: UN mode, Table 34, Table 38, Table 35), MMI_gen 6890 (partly: FS mode, unidentified mode, un-concerned object)
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 20;

            DmiActions.Send_UN_Mode_Ack(this);
            DmiExpectedResults.UN_Mode_Ack_requested(this);

            DmiActions.ShowInstruction(this, @"Acknowledge by pressing on area C1");
            DmiExpectedResults.UN_Mode_Ack_Pressed(this);

            DmiActions.Send_L1(this);
            DmiActions.Send_UN_Mode(this);

            DmiExpectedResults.UN_Mode_displayed(this);
            DmiExpectedResults.Driver_symbol_displayed(this, "Level 1", "LE03", "C8", true);

            WaitForVerification("Check the following :" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in UN mode, Level 0." + Environment.NewLine +
                                "2. DMI does not display the White basic speed hook." + Environment.NewLine +
                                "3. DMI does not display the Medium-grey basic speed hook." + Environment.NewLine +
                                "4. DMI does not display the Digital distance to target." + Environment.NewLine +
                                "5. DMI does not display the Digital release speed.");

            MakeTestStepHeader(10, UniqueIdentifier++,
                "Stop the train.Press, at least twice, on area A1-A4, and area B respectively.Then, continue to drive the train forward after expected result verified",
                "Verify the following information,The objects below are not toggled visible/invisible, (always remain the same as the previous step)White basic speed hookMedium-grey basic speed hookDistance to target (digital)Release Speed Digital");
            /*
            Test Step 10
            Action: Stop the train.Press, at least twice, on area A1-A4, and area B respectively.Then, continue to drive the train forward after expected result verified
            Expected Result: Verify the following information,The objects below are not toggled visible/invisible, (always remain the same as the previous step)White basic speed hookMedium-grey basic speed hookDistance to target (digital)Release Speed Digital
            Test Step Comment: (1) MMI_gen 6892 (partly: Areas A and B for UN mode), MMI_gen 6890 (partly: UN mode, unidentified mode, un-concerned object);
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 0;

            DmiActions.ShowInstruction(this, "Press on area A1-A4, and area B, respectively, at least twice");

            WaitForVerification(
                "Check that the following objects do not toggle on (visible) or off (invisible) as the respective area is pressed:" +
                Environment.NewLine + Environment.NewLine +
                "1. White Basic speed hook (remains invisible)." + Environment.NewLine +
                "2.	Medium-grey basic speed hook (remains invisible)." + Environment.NewLine +
                "3. Digital distance to target (remains invisible)." + Environment.NewLine +
                "4. Digital release speed (remains invisible).");

            MakeTestStepHeader(11, UniqueIdentifier++,
                "Drive the train forward pass BG2.Then, press an area C1 for acknowledgement",
                "DMI displays in FS mode, Level 1.Verify the following information,The objects below are displayed on DMI,Distance to target (digital)Release Speed DigitalThe objects below are not displayed on DMI,White Basic speed HookMedium-grey basic speed hook");
            /*
            Test Step 11
            Action: Drive the train forward pass BG2.Then, press an area C1 for acknowledgement
            Expected Result: DMI displays in FS mode, Level 1.Verify the following information,The objects below are displayed on DMI,Distance to target (digital)Release Speed DigitalThe objects below are not displayed on DMI,White Basic speed HookMedium-grey basic speed hook
            Test Step Comment: (1) MMI_gen 6892 (partly: FS mode, Table 38 (not CSM), Table 35 (not CSM))(2) MMI_gen 6890 (partly: FS mode, unidentified mode, un-concerned object), Table 34 (not CSM)
            */
            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Intervention_Status_PreIndication_Monitoring;
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 20;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.FullSupervision;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Level = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_LEVEL.L1;
            EVC1_MMIDynamic.MMI_V_TARGET_KMH = -1;
            EVC1_MMIDynamic.MMI_V_RELEASE_KMH = 15;
            EVC1_MMIDynamic.MMI_O_BRAKETARGET = 150000;

            WaitForVerification("Check the following :" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in FS mode, Level 1." + Environment.NewLine +
                                "2.	DMI displays the Digital distance to target." + Environment.NewLine +
                                "3. DMI displays the Digital release speed." + Environment.NewLine +
                                "4. DMI does not display the White basic speed hook." + Environment.NewLine +
                                "5. DMI does not display the Medium-grey basic speed hook.");

            MakeTestStepHeader(12, UniqueIdentifier++,
                "Stop the train.Then, press, at least twice,  on area A1-A4, and area B respectively.Then, continue to drive the train forward after expected result verified",
                "Verify the following information,The objects below are not toggled visible/invisible, (always remain the same as the previous step)White Basic speed HookMedium-grey basic speed hookDistance to target (digital)Release Speed Digital");
            /*
            Test Step 12
            Action: Stop the train.Then, press, at least twice,  on area A1-A4, and area B respectively.Then, continue to drive the train forward after expected result verified
            Expected Result: Verify the following information,The objects below are not toggled visible/invisible, (always remain the same as the previous step)White Basic speed HookMedium-grey basic speed hookDistance to target (digital)Release Speed Digital
            Test Step Comment: (1) MMI_gen 6892 (partly: Areas A and B for FS mode) MMI_gen 6890 (partly: FS mode, unidentified mode, un-concerned object); 
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 0;

            DmiActions.ShowInstruction(this, "Press on area A1-A4, and area B, respectively, at least twice");

            WaitForVerification("Check that the following objects do not toggle as the respective area is pressed:" +
                                Environment.NewLine + Environment.NewLine +
                                "1. White Basic speed hook (remains invisible)." + Environment.NewLine +
                                "2.	Medium-grey basic speed hook (remains invisible)." + Environment.NewLine +
                                "3. Digital distance to target (remains visible)." + Environment.NewLine +
                                "4. Digital release speed (remains visible).");

            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 20;

            MakeTestStepHeader(13, UniqueIdentifier++,
                "Drive the train forward pass BG3. Then, acknowledge OS mode by press a sub-area C1",
                "DMI displays in OS mode, Level 1.Verify the following information,The objects below are displayed on DMI for 10 secondsBasic speed Hook(s)Distance to target (digital)Release speed digital");
            /*
            Test Step 13
            Action: Drive the train forward pass BG3. Then, acknowledge OS mode by press a sub-area C1
            Expected Result: DMI displays in OS mode, Level 1.Verify the following information,The objects below are displayed on DMI for 10 secondsBasic speed Hook(s)Distance to target (digital)Release speed digital
            Test Step Comment: (1) MMI_gen 11868 (partly: OS mode), Table 34 (not CSM), Table 35 (not CSM), Table 38 (not CSM), MMI_gen 6450 (partly: 3rd bullet, OS mode), MMI_gen 6898 (configuration ‘TIMER’, OS mode);
            */

            DmiActions.Send_OS_Mode_Ack(this);
            DmiExpectedResults.OS_Mode_Ack_requested(this);

            DmiActions.ShowInstruction(this, @"Acknowledge by pressing on area C1");
            DmiExpectedResults.OS_Mode_Ack_Pressed(this);

            DmiActions.Send_L1(this);
            DmiActions.Send_OS_Mode(this);

            DmiExpectedResults.OS_Mode_displayed(this);
            DmiExpectedResults.Driver_symbol_displayed(this, "Level 1", "LE03", "C8", true);

            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Intervention_Status_PreIndication_Monitoring;
            EVC1_MMIDynamic.MMI_V_RELEASE_KMH = 15;
            Wait_Realtime(10000);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in OS mode, Level 1." + Environment.NewLine +
                                "2. DMI displays (both) Basic speed hook for 10s then removes them." +
                                Environment.NewLine +
                                "3.	DMI displays the Digital distance to target for 10s then removes it." +
                                Environment.NewLine +
                                "4. DMI displays the Digital release speed for 10s then removes it.");

            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Normal_Status_Ceiling_Speed_Monitoring;

            MakeTestStepHeader(14, UniqueIdentifier++,
                "Stop the train.Press the speedometer onceNote: Stopwatch is required for accuracy of test result",
                "Verify the following information,The objects below are displays for 10 seconds (toggled off). Then, disappear.White Basic speed HookMedium-grey basic speed hookDistance to target (digital)Release Speed Digital");
            /*
            Test Step 14
            Action: Stop the train.Press the speedometer onceNote: Stopwatch is required for accuracy of test result
            Expected Result: Verify the following information,The objects below are displays for 10 seconds (toggled off). Then, disappear.White Basic speed HookMedium-grey basic speed hookDistance to target (digital)Release Speed Digital
            Test Step Comment: (1) MMI_gen 6890 (partly: OS mode, toggle on), MMI_gen 6896 (partly: configuration ‘TIMER’, OS mode, toggle visible), MMI_gen 6894 (partly: OS mode); 
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 0;

            DmiActions.ShowInstruction(this, "Press the speedometer once");

            WaitForVerification(
                "Check that the following objects are displayed by the DMI for 10s (toggled off) and then removed:" +
                Environment.NewLine + Environment.NewLine +
                "1. White Basic speed hook." + Environment.NewLine +
                "2.	Medium-grey basic speed hook." + Environment.NewLine +
                "3. Digital distance to target." + Environment.NewLine +
                "4. Digital release speed.");

            MakeTestStepHeader(15, UniqueIdentifier++,
                "Press, only once, on area A1-A4, and area B respectively.Then, continue to drive the train forward after expected result verified.Note: Stopwatch is required for accuracy of test result",
                "Verify the following information,(1)   The objects below are displays for 10 seconds. Then, disappear.White basic speed hookMedium-grey basic speed hookDistance to target (digital)Release Speed Digital");
            /*
            Test Step 15
            Action: Press, only once, on area A1-A4, and area B respectively.Then, continue to drive the train forward after expected result verified.Note: Stopwatch is required for accuracy of test result
            Expected Result: Verify the following information,(1)   The objects below are displays for 10 seconds. Then, disappear.White basic speed hookMedium-grey basic speed hookDistance to target (digital)Release Speed Digital
            Test Step Comment: (1) MMI_gen 6890 (partly: Areas A, Area B, OS mode);                                  MMI_gen 6896 (partly: configuration ‘TIMER’, OS mode);                                      MMI_gen 6894 (partly: OS mode);
            */
            DmiActions.ShowInstruction(this, "Press on area A1-A4, and area B, respectively, at least twice");

            WaitForVerification("Check that the following objects are displayed by the DMI for 10s and then removed:" +
                                Environment.NewLine + Environment.NewLine +
                                "1. White Basic speed hook." + Environment.NewLine +
                                "2.	Medium-grey basic speed hook." + Environment.NewLine +
                                "3. Digital distance to target." + Environment.NewLine +
                                "4. Digital release speed.");

            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 20;

            MakeTestStepHeader(16, UniqueIdentifier++,
                "Drive the train forward pass BG5. Then, acknowledge LS mode by press a sub-area C1",
                "DMI displays in LS mode, Level 1.Verify the following information,The objects below are displayed on DMI,Distance to target (digital)Release Speed DigitalThe objects below are not displayed on DMI,White Basic speed HookMedium-grey basic speed hook");
            /*
            Test Step 16
            Action: Drive the train forward pass BG5. Then, acknowledge LS mode by press a sub-area C1
            Expected Result: DMI displays in LS mode, Level 1.Verify the following information,The objects below are displayed on DMI,Distance to target (digital)Release Speed DigitalThe objects below are not displayed on DMI,White Basic speed HookMedium-grey basic speed hook
            Test Step Comment: (1) MMI_gen 6892 (partly: LS mode, Table 35 (not CSM), Table 38 (not CSM))(2) MMI_gen 6890 (partly: LS mode, unidentified mode, un-concerned object), Table 34 (not CSM)
            */

            DmiActions.Send_LS_Mode_Ack(this);
            DmiExpectedResults.LS_Mode_Ack_requested(this);

            DmiActions.ShowInstruction(this, @"Acknowledge by pressing on area C1");
            DmiExpectedResults.LS_Mode_Ack_Pressed(this);

            DmiActions.Send_L1(this);
            DmiActions.Send_LS_Mode(this);

            DmiExpectedResults.LS_Mode_displayed(this);
            DmiExpectedResults.Driver_symbol_displayed(this, "Level 1", "LE03", "C8", true);

            // Wrong LS mode turns off Digital distance to target!
            WaitForVerification("Check the following :" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in LS mode, Level 1." + Environment.NewLine +
                                "2.	DMI displays the Digital distance to target." + Environment.NewLine +
                                "3. DMI displays the Digital release speed." + Environment.NewLine +
                                "4. DMI does not display the White basic speed hook." + Environment.NewLine +
                                "5. DMI does not display the Medium-grey basic speed hook.");

            MakeTestStepHeader(17, UniqueIdentifier++,
                "Stop the train.Press, at least twice, on area A1-A4, and area B respectively.Then, continue to drive the train forward after expected result verified",
                "Verify the following information,The objects below are not toggled visible/invisible, (always remain the same as the previous step)White Basic speed HookMedium-grey basic speed hookDistance to target (digital)Release Speed Digital");
            /*
            Test Step 17
            Action: Stop the train.Press, at least twice, on area A1-A4, and area B respectively.Then, continue to drive the train forward after expected result verified
            Expected Result: Verify the following information,The objects below are not toggled visible/invisible, (always remain the same as the previous step)White Basic speed HookMedium-grey basic speed hookDistance to target (digital)Release Speed Digital
            Test Step Comment: (1) MMI_gen 6892 (partly: Area A and B, LS mode) MMI_gen 6890 (partly: LS mode, unidentified mode, un-concerned object);
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 0;
            DmiActions.ShowInstruction(this, "Press on area A1-A4, and area B, respectively, at least twice");

            WaitForVerification("Check that the following objects do not toggle as the respective area is pressed:" +
                                Environment.NewLine + Environment.NewLine +
                                "1. White Basic speed hook (remains invisible)." + Environment.NewLine +
                                "2.	Medium-grey basic speed hook (remains invisible)." + Environment.NewLine +
                                "3. Digital distance to target (remains visible)." + Environment.NewLine +
                                "4. Digital release speed (remains visible).");

            MakeTestStepHeader(18, UniqueIdentifier++, "Drive the train pass through EOA",
                "DMI displays in TR mode, Level 1.Verify the following information,The objects below are not displayed on DMI,White Basic speed HookMedium-grey basic speed hookDistance to target (digital)Release Speed Digital");
            /*
            Test Step 18
            Action: Drive the train pass through EOA
            Expected Result: DMI displays in TR mode, Level 1.Verify the following information,The objects below are not displayed on DMI,White Basic speed HookMedium-grey basic speed hookDistance to target (digital)Release Speed Digital
            Test Step Comment: (1) MMI_gen 6892 (partly: TR mode, Table 34, Table 38, Table 35), MMI_gen 6890 (partly: TR mode, unidentified mode, un-concerned object)
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 20;
            EVC1_MMIDynamic.MMI_O_BRAKETARGET = 150000;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN = 151000;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.Trip;

            WaitForVerification("Check the following :" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in TR mode, Level 1." + Environment.NewLine +
                                "2. DMI does not display the White basic speed hook." + Environment.NewLine +
                                "3. DMI does not display the Medium-grey basic speed hook." + Environment.NewLine +
                                "4.	DMI does not display the Digital distance to target." + Environment.NewLine +
                                "5. DMI does not display the Digital release speed.");

            MakeTestStepHeader(19, UniqueIdentifier++,
                "Stop the train.Press, at least twice, on area A1-A4, and area B respectively.Then, continue to drive the train forward after expected result verified",
                "Verify the following information,The objects below are not toggled visible/invisible, (always remain the same as the previous step)White basic speed hookMedium-grey basic speed hookDistance to target (digital)Release Speed Digital");
            /*
            Test Step 19
            Action: Stop the train.Press, at least twice, on area A1-A4, and area B respectively.Then, continue to drive the train forward after expected result verified
            Expected Result: Verify the following information,The objects below are not toggled visible/invisible, (always remain the same as the previous step)White basic speed hookMedium-grey basic speed hookDistance to target (digital)Release Speed Digital
            Test Step Comment: (1) MMI_gen 6892 (partly: Areas A and B for TR mode), MMI_gen 6890 (partly: TR mode, unidentified mode, un-concerned object);
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 0;

            DmiActions.ShowInstruction(this, "Press on area A1-A4, and area B, respectively, at least twice");

            WaitForVerification(
                "Check that the following objects do not toggle on (visible) or off (invisible) as the respective area is pressed:" +
                Environment.NewLine + Environment.NewLine +
                "1. White Basic speed hook (remains invisible)." + Environment.NewLine +
                "2.	Medium-grey basic speed hook (remains invisible)." + Environment.NewLine +
                "3. Digital distance to target (remains invisible)." + Environment.NewLine +
                "4. Digital release speed (remains invisible).");

            MakeTestStepHeader(20, UniqueIdentifier++, "Acknowledge TR mode by press a sub-area C1",
                "DMI displays in PT mode, Level 1. Verify the following information,The objects below are not displayed on DMI,White Basic speed HookMedium-grey basic speed hookDistance to target (digital)Release Speed Digital");
            /*
            Test Step 20
            Action: Acknowledge TR mode by press a sub-area C1
            Expected Result: DMI displays in PT mode, Level 1. Verify the following information,The objects below are not displayed on DMI,White Basic speed HookMedium-grey basic speed hookDistance to target (digital)Release Speed Digital
            Test Step Comment: (1) MMI_gen 6892 (partly: PT mode, Table 34, Table 38, Table 35), MMI_gen 6890 (partly: PT mode, unidentified mode, un-concerned object)
            */

            DmiActions.Send_TR_Mode_Ack(this);
            DmiExpectedResults.TR_Mode_Ack_requested(this);

            DmiActions.ShowInstruction(this, @"Acknowledge by pressing on area C1");
            DmiExpectedResults.TR_Mode_Ack_Pressed(this);

            DmiActions.Send_L1(this);
            DmiActions.Send_PT_Mode(this);

            DmiExpectedResults.PT_Mode_displayed(this);
            DmiExpectedResults.Driver_symbol_displayed(this, "Level 1", "LE03", "C8", true);

            WaitForVerification("Check the following :" + Environment.NewLine + Environment.NewLine +
                                "1.	DMI displays in PT mode, Level 1." + Environment.NewLine +
                                "2.	DMI does not display the Digital distance to target." + Environment.NewLine +
                                "3. DMI does not display the Digital release speed." + Environment.NewLine +
                                "4. DMI does not display the White basic speed hook." + Environment.NewLine +
                                "5. DMI does not display the Medium-grey basic speed hook.");

            MakeTestStepHeader(21, UniqueIdentifier++,
                "Stop the train.Press, at least twice, on area A1-A4, and area B respectively.Then, continue to drive the train forward after expected result verified",
                "Verify the following information,The objects below are not toggled visible/invisible, (always remain the same as the previous step)White basic speed hookMedium-grey basic speed hookDistance to target (digital)Release Speed Digital");
            /*
            Test Step 21
            Action: Stop the train.Press, at least twice, on area A1-A4, and area B respectively.Then, continue to drive the train forward after expected result verified
            Expected Result: Verify the following information,The objects below are not toggled visible/invisible, (always remain the same as the previous step)White basic speed hookMedium-grey basic speed hookDistance to target (digital)Release Speed Digital
            Test Step Comment: (1) MMI_gen 6892 (partly: Areas A and B for PT mode), MMI_gen 6890 (partly: PT mode, unidentified mode, un-concerned object);
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 0;

            DmiActions.ShowInstruction(this, "Press on area A1-A4, and area B, respectively, at least twice");

            WaitForVerification(
                "Check that the following objects do not toggle on (visible) or on (invisible) as the respective area is pressed:" +
                Environment.NewLine + Environment.NewLine +
                "1. White Basic speed hook (remains invisible)." + Environment.NewLine +
                "2.	Medium-grey basic speed hook (remains invisible)." + Environment.NewLine +
                "3. Digital distance to target (remains invisible)." + Environment.NewLine +
                "4. Digital release speed does not change (remains invisible).");

            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 20;

            MakeTestStepHeader(22, UniqueIdentifier++,
                "Perform the following procedure,Press ‘Main’ button.Press and hold ‘Shunting’ button up to 2 second.Release ‘Shunting’ button.Note: Stopwatch is required for accuracy of test result",
                "DMI displays in SH mode, Level 1.Verify the following information,The white basic speed hook is displayed on DMI for 10 secondsThe objects below are not displayed on DMI,Medium-grey basic speed hookDistance to target (digital)Release Speed Digital");
            /*
            Test Step 22
            Action: Perform the following procedure,Press ‘Main’ button.Press and hold ‘Shunting’ button up to 2 second.Release ‘Shunting’ button.Note: Stopwatch is required for accuracy of test result
            Expected Result: DMI displays in SH mode, Level 1.Verify the following information,The white basic speed hook is displayed on DMI for 10 secondsThe objects below are not displayed on DMI,Medium-grey basic speed hookDistance to target (digital)Release Speed Digital
            Test Step Comment: (1) MMI_gen 11868 (partly: SH mode);                    MMI_gen 6450 (partly: 3rd bullet, SH mode) , Table 34 (CSM), MMI_gen 6898 (partly: configuration ‘TIMER’);(2) MMI_gen 6890 (partly: SH mode, un-concerned object), Table 34 (CSM), Table 38 (CSM), Table 35 (CSM)
            */
            DmiActions.ShowInstruction(this, "Press the ‘Main’ button.");
            DmiActions.Display_Main_Window_with_Start_button_enabled(this);

            DmiActions.ShowInstruction(this,
                "Press and hold ‘Shunting’ button for up to 2s then release the ‘Shunting’ button");
            DmiExpectedResults.Shunting_button_pressed_and_hold(this);

            DmiActions.Send_SH_Mode(this);
            DmiActions.Send_L1(this);

            DmiExpectedResults.SH_Mode_displayed(this);
            DmiExpectedResults.Driver_symbol_displayed(this, "Level 1", "LE03", "C8", true);

            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Normal_Status_Ceiling_Speed_Monitoring;
            Wait_Realtime(10000);
            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Intervention_Status_PreIndication_Monitoring;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in SH mode, Level 1." + Environment.NewLine +
                                "2. DMI displays the White basic speed hook for 10s then removes it." +
                                Environment.NewLine +
                                "3. DMI does not display the Medium-grey basic speed hook." + Environment.NewLine +
                                "4. DMI does not display the Digital distance to target." + Environment.NewLine +
                                "5. DMI does not display the Digital release speed.");

            MakeTestStepHeader(23, UniqueIdentifier++,
                "Press the speedometer onceNote: Stopwatch is required for accuracy of test result",
                "Verify the following information,The white basic speed hook displays for 10 seconds (toggled off). Then, disappears.The objects below are still not displayed on DMI,Medium-grey basic speed hookDistance to target (digital)Release Speed Digital");
            /*
            Test Step 23
            Action: Press the speedometer onceNote: Stopwatch is required for accuracy of test result
            Expected Result: Verify the following information,The white basic speed hook displays for 10 seconds (toggled off). Then, disappears.The objects below are still not displayed on DMI,Medium-grey basic speed hookDistance to target (digital)Release Speed Digital
            Test Step Comment: (1) MMI_gen 6890 (partly: SH mode, toggle off), MMI_gen 6896 (partly: configuration ‘TIMER’, SH mode, toggle invisible), MMI_gen 6894 (partly: SH mode);(2) MMI_gen 6890 (partly: SH mode, un-concerned object), Table 34 (CSM), Table 38 (CSM), Table 35 (CSM)
            */
            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Normal_Status_Ceiling_Speed_Monitoring;
            DmiActions.ShowInstruction(this, @"Press the speedometer once");

            Wait_Realtime(10000);
            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Intervention_Status_PreIndication_Monitoring;
            EVC1_MMIDynamic.MMI_V_PERMITTED_KMH = 10;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the White basic speed hook for 10s then removes it." +
                                Environment.NewLine +
                                "2. DMI still does not display the Medium-grey basic speed hook." +
                                Environment.NewLine +
                                "3. DMI still does not display the Digital distance to target." + Environment.NewLine +
                                "4. DMI still does not display the Digital release speed.");

            MakeTestStepHeader(24, UniqueIdentifier++,
                "Press, only once, on area A1-A4, and area B respectively.Then, continue to drive the train forward after expected result verified.Note: Stopwatch is required for accuracy of test result",
                "Verify the following information,The white basic speed hook is displays for 10 seconds. Then, disappear.The objects below are not toggled visible/invisible, (always remain the same as the previous step),Medium-grey basic speed hookDistance to target (digital)Release Speed Digital");
            /*
            Test Step 24
            Action: Press, only once, on area A1-A4, and area B respectively.Then, continue to drive the train forward after expected result verified.Note: Stopwatch is required for accuracy of test result
            Expected Result: Verify the following information,The white basic speed hook is displays for 10 seconds. Then, disappear.The objects below are not toggled visible/invisible, (always remain the same as the previous step),Medium-grey basic speed hookDistance to target (digital)Release Speed Digital
            Test Step Comment: (1) MMI_gen 6890 (partly: Areas A, Area B, SH mode);                                  MMI_gen 6896 (partly: configuration ‘TIMER’, SH mode);                                      MMI_gen 6894 (partly: SH mode); (2) MMI_gen 6890 (partly: SH mode, un-concerned object), Table 34 (CSM), Table 38 (CSM), Table 35 (CSM)
            */
            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Normal_Status_Ceiling_Speed_Monitoring;

            DmiActions.ShowInstruction(this, "Press on area A1-A4, and area B, respectively, at least twice");

            Wait_Realtime(10000);
            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Intervention_Status_PreIndication_Monitoring;
            EVC1_MMIDynamic.MMI_V_PERMITTED_KMH = 10;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the White basic speed hook for 10s then removes it." +
                                Environment.NewLine +
                                "2. The Medium-grey basic speed hook does not change (stays invisible)." +
                                Environment.NewLine +
                                "3. The Digital distance to target does not change (stays invisible)." +
                                Environment.NewLine +
                                "4. The Digital release speed does not change (stays invisible)");

            MakeTestStepHeader(25, UniqueIdentifier++,
                "Press a sensitivity area (areas A1-A4 or B) to make a Basic Speed Hook appear.Then simulate loss-communication between ETCS onboard and DMI (1 second).Note: Stopwatch is required for accuracy of test result",
                "DMI displays the  message “ATP Down Alarm” with sound alarm.Verify the following information,The objects below are not displayed on DMI,White Basic speed HookMedium-grey basic speed hookDistance to target (digital)Release Speed Digital");
            /*
            Test Step 25
            Action: Press a sensitivity area (areas A1-A4 or B) to make a Basic Speed Hook appear.Then simulate loss-communication between ETCS onboard and DMI (1 second).Note: Stopwatch is required for accuracy of test result
            Expected Result: DMI displays the  message “ATP Down Alarm” with sound alarm.Verify the following information,The objects below are not displayed on DMI,White Basic speed HookMedium-grey basic speed hookDistance to target (digital)Release Speed Digital
            Test Step Comment: (1) Information (paragraph 1) under MMI_gen 6898 (inoperable)
            */
            // Call generic Action Method
            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Normal_Status_Ceiling_Speed_Monitoring;

            DmiActions.ShowInstruction(this,
                "Press in a sensitivity area (areas A1-A4 or B) to make a Basic Speed Hook appear");

            this.Wait_Realtime(1000);
            DmiActions.Simulate_communication_loss_EVC_DMI(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the message ‘ATP Down Alarm’ and plays a sound alarm." +
                                Environment.NewLine +
                                "2. DMI does not display the White basic speed hook." + Environment.NewLine +
                                "3. DMI does not display the Medium-grey basic speed hook." + Environment.NewLine +
                                "4. DMI does not display the Digital distance to target." + Environment.NewLine +
                                "5. DMI does not display the Digital release speed.");

            MakeTestStepHeader(26, UniqueIdentifier++,
                "Re-establish communication between ETCS onboard and DMI (in 1 second).Note: Stopwatch is required for accuracy of test result",
                "Verify the following information,(1)   The white basic speed hook displays for 10 seconds (toggled off). Then, disappears.(2)   The objects below are still not displayed on DMI,Medium-grey basic speed hookDistance to target (digital)Release Speed Digital");
            /*
            Test Step 26
            Action: Re-establish communication between ETCS onboard and DMI (in 1 second).Note: Stopwatch is required for accuracy of test result
            Expected Result: Verify the following information,(1)   The white basic speed hook displays for 10 seconds (toggled off). Then, disappears.(2)   The objects below are still not displayed on DMI,Medium-grey basic speed hookDistance to target (digital)Release Speed Digital
            Test Step Comment: (1) MMI_gen 6890 (partly: SH mode, toggle off), MMI_gen 6896 (partly: configuration ‘TIMER’, SH mode, toggle invisible), MMI_gen 6894 (partly: SH mode);(2) MMI_gen 6890 (partly: SH mode, un-concerned object), Table 34 (CSM), Table 38 (CSM), Table 35 (CSM)
            */
            this.Wait_Realtime(1000);
            DmiActions.Re_establish_communication_EVC_DMI(this);
            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Normal_Status_Ceiling_Speed_Monitoring;
            Wait_Realtime(10000);
            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Intervention_Status_PreIndication_Monitoring;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the White basic speed hook for 10s (toggled off) then removes it." +
                                Environment.NewLine +
                                "2. DMI does not display the Medium-grey basic speed hook." + Environment.NewLine +
                                "3. DMI does not display the Digital distance to target." + Environment.NewLine +
                                "4. DMI does not display the Digital release speed.");

            TraceHeader("End of test");

            /*
            Test Step 27
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
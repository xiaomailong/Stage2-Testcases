using System;
using System.Collections.Generic;
using Testcase.Telegrams.EVCtoDMI;
using static Testcase.Telegrams.EVCtoDMI.Variables;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 19.1 Toggling function: Additional Configuration ‘OFF’ (Default)
    /// TC-ID: 14.1
    /// 
    /// This case verifies the toggling function of the basic speed hooks, release speed ditial and distance to target (digital) which is configured “OFF” by default on DMI in toggling-affected mode SR/OS/SH/ and non-toggling-affected mode UN/FS/TR/PT/RV/LS. The Toggling function shall comply with [MMI-ETCS-gen].
    /// 
    /// Tested Requirements:
    /// MMI_gen 6890; MMI_gen 6892 (partly: RV mode, UN mode, FS mode, TR mode, LS mode, PT mode, Table 34, Table 38, Table 35); MMI_gen 11868; MMI_gen 6894; MMI_gen 6896; MMI_gen 6450 (partly: 2nd bullet); MMI_gen 6898 (partly: configuration ‘OFF”); Table 34, Table 38, Information (paragraph 1) under MMI_gen 6898 (inoperable), Information (paragraph 2) under MMI_gen 6898 (re-establish)
    /// 
    /// Scenario:
    /// 1.Drive the train forward pass BG1 at position 200m      BG1: packet 12, 21, 27, 138 and 139 (Entering FS mode and reversing allowance area)
    /// 2.Enter RV mode, Level 
    /// 1.Then, Perform the procedure in note below to verify that toggling function is disabled in RV mode.
    /// 3.De-activate Cabin A and Activate Cabin A again.
    /// 4.Perform SoM in SR mode, Level 1.
    /// 5.Enter and confirm specified value of SR speed and SR distance. Then, verify that Basic speed Hooks and Distance to target (digital) are no displayed on DMI.
    /// 6.Perform the procedure in note below to verify that toggling function is enabled in SR mode.
    /// 7.Drive the train forward with speed below permitted pass BG2 at position 300m      BG2: packet 41 (Level 0 Trainsition)
    /// 8.Stop the train. Then, Perform the procedure in note below to verify that toggling function is disalbed in UN mode.
    /// 9.Drive the train forward pass BG3 at position 500m      BG3: packet 12, 21 and 27 (Entering FS mode)
    /// 10.Stop the train. Then, Perform the procedure in note below to verify that toggling function is disalbed in FS mode.
    /// 11.Drive the train forward pass BG4 at position 700m        BG4: packet 12, 21, 27 and 80 (Entering OS mode)
    /// 12.Stop the train at position 800m. Then, Perform the procedure in note below to verify that toggling function is enabled in OS mode.
    /// 13.Drive the train forward pass BG5 at position 1000m        BG5: packet 12, 21, 27 and 80 (Entering LS mode)        Note: The train wiill return to FS mode at postion 900m before entering LS mode.
    /// 14.Stop the train at position 1100m. Then, Perform the procedure in note below to verify that toggling function is disabled in LS mode.
    /// 15.Drive the train pass EOA (over 1500m), Then, Perform the procedure in note below to verify that toggling function is disabled in TR mode.
    /// 16.Acknowledge TR mode. Then, Perform the procedure in note below to verify that toggling function is disabled in PT mode.
    /// 17.Force the train enter SH mode, Then, Perform the procedure in note below to verify that toggling function is enabled in SH mode.Note: Procedure for toggling function verification,Press on area A1-A4 respectively.Press around areas B.
    /// 
    /// Used files:
    /// 14_1.tdg
    /// </summary>
    public class TC_14_1_Toggling_Function : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // The DMI default configuration has TOGGLE_FUNCTION = 1 (‘OFF’).System is power on.SoM is performed in SR mode, Level1.

            // Call the TestCaseBase PreExecution
            base.PreExecution();

            DmiActions.Complete_SoM_L1_SR(this);
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in PT mode, Level 1.

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint

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

            /*
            Test Step 2
            Action: Perform the following procedure,
            Chage the train direction to reverse
            Press the symbol in sub-area C1
            Expected Result: DMI displays in RV mode, Level 1.
            Verify the following information,
                The objects below are displayed on DMI,
                    White Basic speed Hook
                    Distance to target (digital)
                The objects below are not displayed on DMI,
                    Medium-grey basic speed hook
                    Release Speed Digital
            Test Step Comment: (1) MMI_gen 6892 (partly: RV mode, Table 34 (CSM), Table 38 (CSM))(2) MMI_gen 6890 (partly: RV mode, unidentified mode, un-concerned object), Table 34 (CSM), Table 35 (CSM)
            */

            DmiActions.ShowInstruction(this, "Change the train direction to reverse");

            DmiActions.Send_RV_Mode_Ack(this);
            DmiExpectedResults.RV_Mode_Ack_requested(this);

            DmiActions.ShowInstruction(this, "Press the symbol in sub-area C1");
            DmiExpectedResults.RV_Mode_Ack_pressed_and_released(this);

            // Remove RV ACK (MO15)
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 4;
            EVC8_MMIDriverMessage.Send();
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.Reversing;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN = 100000;
            EVC1_MMIDynamic.MMI_O_BRAKETARGET = 150000;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in RV mode, Level 1." + Environment.NewLine +
                                "2. DMI displays the White basic speed hook." + Environment.NewLine +
                                "3.	DMI displays the Digital distance to target." + Environment.NewLine +
                                "4. DMI does not display the Medium-grey basic speed hook." + Environment.NewLine +
                                "5. DMI does not display the Digital release speed.");

           
            /*
            Test Step 3
            Action: Press, at least twice, on area A1-A4, and area B respectively
            Expected Result: Verify the following information,The objects below are not toggled visible/invisible, (always remain the same as the previous step)White Basic speed HookMedium-grey basic speed hookDistance to target (digital)Release Speed Digital
            Test Step Comment: (1) MMI_gen 6892 (partly: Area A and B, RV mode) MMI_gen 6890 (partly: RV mode, unidentified mode, un-concerned object);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, "Press on area A1-A4, and area B, respectively, at least twice");

            WaitForVerification(
                "Check that the following objects do not toggle  and remain the same as in the previous step:" +
                Environment.NewLine + Environment.NewLine +
                "1. White basic speed hook (remains visible)." + Environment.NewLine +
                "2.	Medium-grey basic speed hook (remains invisible)." + Environment.NewLine +
                "3. Digital distance to target (remains visible)." + Environment.NewLine +
                "4. Digital	release speed (remains invisible).");

            /*
            Test Step 4
            Action: Perform the following procedure,De-activate Cabin AActivate Cabin A
            Expected Result: DMI displays in SB mode, Level 1
            */

            DmiActions.Deactivate_Cabin(this);
            Wait_Realtime(5000);
            DmiActions.Activate_Cabin_1(this);

            DmiActions.Set_Driver_ID(this, "1234");

            DmiActions.Send_SB_Mode(this);
            DmiExpectedResults.SB_Mode_displayed(this);

            /*
            Test Step 5
            Action: Perform SoM in SR mode, Level 1
            Expected Result: DMI displays in SR mode, Level 1.The objects below not displayed on DMI, (toggle off)White basic speed hookDistance to target (digital)The release speed digital is not displayed
            Test Step Comment: (1) MMI_gen 11868 (partly: SR mode), Table 34 (CSM), Table 38 (CSM), MMI_gen 6450 (partly: 2nd bullet, SR mode), MMI_gen 6898 (partly: configuration ‘OFF’, SR mode);(2) MMI_gen 6890 (partly: SR mode, un-concerned object), Table 35 (CSM)
            */
            // Tested ad nauseam, force...
            // Call generic Action Method

            DmiActions.Send_SR_Mode(this);
            DmiActions.Send_L1(this);
            DmiActions.Finished_SoM_Default_Window(this);

            WaitForVerification("Check the mode and that the following objects are not displayed:" +
                                Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in SR mode, Level 1." + Environment.NewLine +
                                "2. White Basic speed hook." + Environment.NewLine +
                                "3.	Medium-grey basic speed hook." + Environment.NewLine +
                                "4. Digital	release speed.");

            /*
            Test Step 6
            Action: Perform the following procedure,
            Press ‘Spec’ button.
            Press ‘SR speed/disาtance’ button.
            Enter and confirm the following data,
            SR speed = 40 km/hSR distance = 300 m
            Expected Result: Verify the following information,
            The objects below still not displayed on DMI, (toggle off)
            White basic speed hook
            Medium-grey basic speed hook
            Distance to target (digital)
            The release speed digital is not displayed
            Test Step Comment: (1) MMI_gen 11868 (partly: SR mode);                    
            MMI_gen 6450 (partly: 2nd bullet, SR mode), Table 34 (not CSM), Table 38 (not CSM), MMI_gen 6898 (partly: configuration ‘OFF’);(2) MMI_gen 6890 (partly: SR mode, un-concerned object), Table 35 (not CSM)
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
            EVC11_MMICurrentSRRules.MMI_M_BUTTONS = MMI_M_BUTTONS.BTN_YES_DATA_ENTRY_COMPLETE;
            EVC11_MMICurrentSRRules.DataElements = new List<Variables.DataElement>
            {
                new DataElement {Identifier = 15, EchoText = "40", QDataCheck = 0},
                new DataElement {Identifier = 16, EchoText = "300", QDataCheck = 0}
            };
            EVC11_MMICurrentSRRules.Send();

            WaitForVerification("Check that the following objects are still not displayed:" + Environment.NewLine +
                                Environment.NewLine +
                                "1. White Basic speed hook." + Environment.NewLine +
                                "2. Medium-grey basic speed hook." + Environment.NewLine +
                                "3. Digital distance to target." + Environment.NewLine +
                                "4. Digital release speed.");

            /*
            Test Step 7
            Action: Press the speedometer once
            Expected Result: The objects below are displayed on DMI, (toggled on)White basic speed hookMedium-grey basic speed hookDistance to target (digital)The release speed digital is still displayed
            Test Step Comment: (1) MMI_gen 6890 (partly: Areas A, SR mode, toggle on), MMI_gen 6896 (partly: configuration ‘OFF’, SR mode, toggle visible), MMI_gen 6894 (partly: SR mode);    (2) MMI_gen 6890 (partly: SR mode, un-concerned object, toggle on) , Table 35 (not CSM)
            */
            // Call generic Action Method

            DmiActions.ShowInstruction(this, @"Press the speedometer once");
            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Indication_Status_Target_Speed_Monitoring;
            EVC1_MMIDynamic.MMI_V_TARGET_KMH = 10;
            EVC1_MMIDynamic.MMI_V_RELEASE_KMH = 15;
            EVC1_MMIDynamic.MMI_V_PERMITTED_KMH = 20;

            WaitForVerification("Check that the following objects are displayed (toggled on):" + Environment.NewLine +
                                Environment.NewLine +
                                "1. White Basic speed hook." + Environment.NewLine +
                                "2. Medium-grey basic speed hook." + Environment.NewLine +
                                "3. Digital distance to target." + Environment.NewLine +
                                "4. Digital release speed.");

            /*
            Test Step 8
            Action: Press, at least twice, on area A1-A4, and area B respectively.Then, continue to drive the train forward after expected result verified
            Expected Result: Verify the following information,The objects below are toggled visible (the same as the previous step)/invisible,White basic speed hookMedium-grey basic speed hookDistance to target (digital) The release speed digital remains the same
            Test Step Comment: (1) MMI_gen 6890 (partly: Areas A, Area B, SR mode);                                  MMI_gen 6896 (partly: configuration ‘OFF’, SR mode);                                      MMI_gen 6894 (partly: SR mode);(2) MMI_gen 6890 (partly: SR mode, un-concerned object, toggle on) , Table 35 (not CSM)
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press on area A1-A4, and area B respectively, at least twice");

            WaitForVerification(
                "Check that the following objects toggle off (invisible) or on (visible) as the respective area is pressed:" +
                Environment.NewLine + Environment.NewLine +
                "1. White Basic speed hook." + Environment.NewLine +
                "2.	Medium-grey basic speed hook." + Environment.NewLine +
                "3. Digital distance to target." + Environment.NewLine +
                "4. Digital release speed does not change (remains visible).");

            /*
            Test Step 9
            Action: Drive the train forward pass BG2 with speed = 20km/h (or below permitted speed).Then, press an area C1 for acknowledgement
            Expected Result: DMI displays in UN mode, Level 0.The objects below are not displayed on DMI,White Basic speed HookMedium-grey basic speed hookDistance to target (digital)Release Speed Digital
            Test Step Comment: (1) MMI_gen 6892 (partly: UN mode, Table 34, Table 38, Table 35), MMI_gen 6890 (partly: FS mode, unidentified mode, un-concerned object)
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 20;

            DmiActions.Send_UN_Mode_Ack(this);
            DmiExpectedResults.UN_Mode_Ack_requested(this);

            DmiActions.ShowInstruction(this, @"Acknowledge by pressing on area C1");
            DmiExpectedResults.UN_Mode_Ack_pressed_and_released(this);

            DmiActions.Send_L1(this);
            DmiActions.Send_UN_Mode(this);

            DmiExpectedResults.UN_Mode_displayed(this);
            DmiExpectedResults.Driver_symbol_displayed(this, "Level 1", "LE03", "C8", true);

            WaitForVerification("Check the following :" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in UN mode, Level 1." + Environment.NewLine +
                                "2. DMI does not display the White basic speed hook." + Environment.NewLine +
                                "3. DMI does not display the Medium-grey basic speed hook." + Environment.NewLine +
                                "4. DMI does not display the Digital distance to target." + Environment.NewLine +
                                "5. DMI does not display the Digital release speed.");

            /*
            Test Step 10
            Action: Stop the train.Press, at least twice, on area A1-A4, and area B respectively.Then, continue to drive the train forward after expected result verified
            Expected Result: Verify the following information,The objects below are not toggled visible/invisible, (always remain the same as the previous step)White Basic speed HookMedium-grey basic speed hookDistance to target (digital)Release Speed Digital
            Test Step Comment: (1) MMI_gen 6892 (partly: Areas A and B for UN mode), MMI_gen 6890 (partly: UN mode, unidentified mode, un-concerned object);
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 0;

            DmiActions.ShowInstruction(this, "Press on area A1-A4, and area B, respectively, at least twice");

            WaitForVerification(
                "Check that the following objects do not toggle on (visible) or off (invisible) as the respective area is pressed:" +
                Environment.NewLine + Environment.NewLine +
                "1. White Basic speed hook." + Environment.NewLine +
                "2.	Medium-grey basic speed hook." + Environment.NewLine +
                "3. Digital distance to target." + Environment.NewLine +
                "4. Digital release speed.");

            /*
            Test Step 11
            Action: Drive the train forward pass BG3
            Expected Result: DMI displays in FS mode, Level 1.The objects below are displayed on DMI,Distance to target (digital)Release Speed DigitalThe objects below are not displayed on DMI,White Basic speed HookMedium-grey basic speed hook
            Test Step Comment: (1) MMI_gen 6892 (partly: FS mode, Table 38 (not CSM), Table 35 (not CSM))(2) MMI_gen 6890 (partly: FS mode, unidentified mode, un-concerned object), Table 34 (not CSM)
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 20;

            DmiActions.Send_FS_Mode(this);
            DmiExpectedResults.FS_mode_displayed(this);
            DmiExpectedResults.Driver_symbol_displayed(this, "Level 1", "LE03", "C8", true);

            WaitForVerification("Check the following :" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in FS mode, Level 1." + Environment.NewLine +
                                "2.	DMI displays the Digital distance to target." + Environment.NewLine +
                                "3. DMI displays the Digital release speed." + Environment.NewLine +
                                "4. DMI does not display the White basic speed hook." + Environment.NewLine +
                                "5. DMI does not display the Medium-grey basic speed hook.");

            /*
            Test Step 12
            Action: Stop the train.Then, press, at least twice, on area A1-A4, and area B respectively.Then, continue to drive the train forward after expected result verified
            Expected Result: Verify the following information,The objects below are not toggled visible/invisible, (always remain the same as the previous step)White Basic speed HookMedium-grey basic speed hookDistance to target (digital)Release Speed Digital
            Test Step Comment: (1) MMI_gen 6892 (partly: Areas A and B for FS mode) MMI_gen 6890 (partly: FS mode, unidentified mode, un-concerned object);
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 0;

            DmiActions.ShowInstruction(this, "Press on area A1-A4, and area B, respectively, at least twice");

            WaitForVerification(
                "Check that the following objects do not toggle off (invisible) / on (visible) as the respective area is pressed:" +
                Environment.NewLine + Environment.NewLine +
                "1. White Basic speed hook (remains invisible)." + Environment.NewLine +
                "2.	Medium-grey basic speed hook (remains invisible)." + Environment.NewLine +
                "3. Digital distance to target (remains visible)." + Environment.NewLine +
                "4. Digital release speed does not change (remains visible).");

            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 20;

            /*
            Test Step 13
            Action: Drive the train forward pass BG4. Then, acknowledge OS mode by press a sub-area C1
            Expected Result: DMI displays in OS mode, Level 1.Verify the following information,The objects below are not displays on DMI, (toggle off)Basic speed Hook(s)Distance to target (digital)Release speed digital
            Test Step Comment: (1) MMI_gen 11868 (partly: OS mode), Table 34 (not CSM), Table 35 (not CSM), Table 38 (not CSM), MMI_gen 6450 (partly: 2nd bullet, OS mode), MMI_gen 6898 (configuration ‘OFF’, OS mode);
            */

            DmiActions.Send_OS_Mode_Ack(this);
            DmiExpectedResults.OS_Mode_Ack_requested(this);

            DmiActions.ShowInstruction(this, @"Acknowledge by pressing on area C1");
            DmiExpectedResults.OS_Mode_Ack_pressed_and_released(this);

            DmiActions.Send_L1(this);
            DmiActions.Send_OS_Mode(this);

            DmiExpectedResults.OS_Mode_displayed(this);
            DmiExpectedResults.Driver_symbol_displayed(this, "Level 1", "LE03", "C8", true);

            WaitForVerification("Check that the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in OS mode, Level 1." + Environment.NewLine +
                                "2. DMI does not display (either) Basic speed hook(s)." + Environment.NewLine +
                                "3.	DMI does not display the Digital distance to target." + Environment.NewLine +
                                "4. DMI does not display the Digital release speed.");

            /*
            Test Step 14
            Action: Stop the train.Press the speedometer once
            Expected Result: Verify the following information,The objects below are displayed on DMI, (toggle on)White Basic speed HookMedium-grey basic speed hookDistance to target (digital)Release Speed Digital
            Test Step Comment: (1) MMI_gen 6890 (partly: OS mode, toggle on), MMI_gen 6896 (partly: configuration ‘OFF’, OS mode, toggle visible), MMI_gen 6894 (partly: OS mode); 
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 0;

            DmiActions.ShowInstruction(this, "Press the speedometer once");

            WaitForVerification("Check the following :" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the White basic speed hook." + Environment.NewLine +
                                "2. DMI displays the Medium-grey basic speed hook." + Environment.NewLine +
                                "3.	DMI displays the Digital distance to target." + Environment.NewLine +
                                "4. DMI displays the Digital release speed.");

            /*
            Test Step 15
            Action: Press, at least twice, on area A1-A4, and area B respectively.Then, continue to drive the train forward after expected result verified
            Expected Result: Verify the following information,The objects below are toggled visible (the same as the previous step)/invisible,White basic speed hookMedium-grey basic speed hookDistance to target (digital)Release Speed Digital
            Test Step Comment: (1) MMI_gen 6890 (partly: Areas A, Area B, OS mode);                                  MMI_gen 6896 (partly: configuration ‘ON’, OS mode);                                      MMI_gen 6894 (partly: OS mode);
            */
            DmiActions.ShowInstruction(this, "Press on area A1-A4, and area B, respectively, at least twice");

            WaitForVerification(
                "Check that the following objects toggle off (invisible) and on (visible) as the respective area is pressed:" +
                Environment.NewLine + Environment.NewLine +
                "1. White Basic speed hook." + Environment.NewLine +
                "2.	Medium-grey basic speed hook." + Environment.NewLine +
                "3. Digital distance to target." + Environment.NewLine +
                "4. Digital release speed.");

            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 20;

            /*
            Test Step 16
            Action: Drive the train forward pass BG5. Then, acknowledge LS mode by press a sub-area C1
            Expected Result: DMI displays in LS mode, Level 1.Verify the following information,The objects below are displayed on DMI,Distance to target (digital)Release Speed DigitalThe objects below are not displayed on DMI,White Basic speed HookMedium-grey basic speed hook
            Test Step Comment: (1) MMI_gen 6892 (partly: LS mode, Table 35 (not CSM), Table 38 (not CSM))(2) MMI_gen 6890 (partly: LS mode, unidentified mode, un-concerned object), Table 34 (not CSM)
            */

            DmiActions.Send_LS_Mode_Ack(this);
            DmiExpectedResults.LS_Mode_Ack_requested(this);

            DmiActions.ShowInstruction(this, @"Acknowledge by pressing on area C1");
            DmiExpectedResults.LS_Mode_Ack_pressed_and_released(this);

            DmiActions.Send_L1(this);
            DmiActions.Send_LS_Mode(this);

            DmiExpectedResults.LS_Mode_displayed(this);
            DmiExpectedResults.Driver_symbol_displayed(this, "Level 1", "LE03", "C8", true);

            WaitForVerification("Check the following :" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in LS mode, Level 1." + Environment.NewLine +
                                "2.	DMI displays the Digital distance to target." + Environment.NewLine +
                                "3. DMI displays the Digital release speed." + Environment.NewLine +
                                "4. DMI does not display the White basic speed hook." + Environment.NewLine +
                                "5. DMI does not display the Medium-grey basic speed hook.");
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
                                "4. Digital release speed does not change (remains visible).");

            /*
            Test Step 18
            Action: Drive the train pass through EOA
            Expected Result: DMI displays in TR mode, Level 1. The objects below are not displayed on DMI,White Basic speed HookMedium-grey basic speed hookDistance to target (digital)Release Speed Digital
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

            /*
            Test Step 19
            Action: Stop the train.Then, press, at least twice, on area A1-A4, and area B respectively.Then, continue to drive the train forward after expected result verified
            Expected Result: Verify the following information,The objects below are not toggled visible/invisible, (always remain the same as the previous step)White Basic speed HookMedium-grey basic speed hookDistance to target (digital)Release Speed Digital
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

            /*
            Test Step 20
            Action: Acknowledge TR mode by press a sub-area C1
            Expected Result: DMI displays in PT mode, Level 1. The objects below are not displayed on DMI,White Basic speed HookMedium-grey basic speed hookDistance to target (digital)Release Speed Digital
            Test Step Comment: (1) MMI_gen 6892 (partly: PT mode, Table 34, Table 38, Table 35), MMI_gen 6890 (partly: PT mode, unidentified mode, un-concerned object)
            */
            // Spec may be inconsistent here: says TR but expects PT to result

            DmiActions.Send_TR_Mode_Ack(this);
            DmiExpectedResults.TR_Mode_Ack_requested(this);

            DmiActions.ShowInstruction(this, @"Acknowledge by pressing on area C1");
            DmiExpectedResults.TR_Mode_Ack_pressed_and_released(this);

            DmiActions.Send_L1(this);
            DmiActions.Send_PT_Mode(this);

            DmiExpectedResults.PT_Mode_displayed(this);
            DmiExpectedResults.Driver_symbol_displayed(this, "Level 1", "LE03", "C8", true);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1.	DMI displays in PT mode, Level 1." + Environment.NewLine +
                                "2.	DMI does not display the Digital distance to target." + Environment.NewLine +
                                "3. DMI does not display the Digital release speed." + Environment.NewLine +
                                "4. DMI does not display the White basic speed hook." + Environment.NewLine +
                                "5. DMI does not display the Medium-grey basic speed hook.");

            /*
            Test Step 21
            Action: Stop the train.Then, press, at least twice, on area A1-A4, and area B respectively.Then, continue to drive the train forward after expected result verified
            Expected Result: Verify the following information,The objects below are not toggled visible/invisible, (always remain the same as the previous step)White Basic speed HookMedium-grey basic speed hookDistance to target (digital)Release Speed Digital
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

            /*
            Test Step 22
            Action: Perform the following procedure,Press ‘Main’ button.Press and hold ‘Shunting’ button up to 2 second.Release ‘Shunting’ button
            Expected Result: DMI displays in SH mode, Level 1.Verify the following information,The white basic speed hook is not displayed on DMI (toggle off).The objects below are not displayed on DMI,Medium-grey basic speed hookDistance to target (digital)Release Speed Digital
            Test Step Comment: (1) MMI_gen 11868 (partly: SH mode);                    MMI_gen 6450 (partly: 2nd bullet, SH mode) , Table 34 (CSM), MMI_gen 6898 (partly: configuration ‘OFF’);(2) MMI_gen 6890 (partly: SH mode, un-concerned object), Table 34 (CSM), Table 38 (CSM), Table 35 (CSM)
            */

            DmiActions.ShowInstruction(this, $"Press the ‘Main’ button.");
            DmiActions.Display_Main_Window_with_Start_button_enabled(this);

            DmiActions.ShowInstruction(this, $"Press and hold ‘Shunting’ button for up to 2s then release the ‘Shunting’ button");
            DmiExpectedResults.Shunting_button_pressed_and_hold(this);

            DmiActions.Send_SH_Mode(this);
            DmiActions.Send_L1(this);

            DmiExpectedResults.SH_Mode_displayed(this);
            DmiExpectedResults.Driver_symbol_displayed(this, "Level 1", "LE03", "C8", true);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in SH mode, Level 1." + Environment.NewLine +
                                "2. DMI does not display the White basic speed hook (toggled off)." +
                                Environment.NewLine +
                                "3. DMI does not display the Medium-grey basic speed hook." + Environment.NewLine +
                                "4. DMI does not display the Digital distance to target." + Environment.NewLine +
                                "5. DMI does not display the Digital release speed.");

            /*
            Test Step 23
            Action: Press the speedometer once
            Expected Result: Verify the following information,The white basic speed hook is displayed on DMI (toggle on).The objects below are still not displayed on DMI,Medium-grey basic speed hookDistance to target (digital)Release Speed Digital
            Test Step Comment: (1) MMI_gen 6890 (partly: SH mode, toggle on), MMI_gen 6896 (partly: configuration ‘OFF’, SH mode, toggle visible), MMI_gen 6894 (partly: SH mode);(2) MMI_gen 6890 (partly: SH mode, un-concerned object), Table 34 (CSM), Table 38 (CSM), Table 35 (CSM)
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press the speedometer once");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the White basic speed hook (toggled on)." + Environment.NewLine +
                                "2. DMI does not display the Medium-grey basic speed hook." + Environment.NewLine +
                                "3. DMI does not display the Digital distance to target." + Environment.NewLine +
                                "4. DMI does not display the Digital release speed.");

            /*
            Test Step 24
            Action: Press, at least twice, on area A1-A4, and area B respectively.Then, continue to drive the train forward after expected result verified
            Expected Result: Verify the following information,The white basic speed hook is toggled visible (the same as the visible step)/invisibleThe objects below are not toggled visible/invisible, (always remain the same as the previous step),Medium-grey basic speed hookDistance to target (digital)Release Speed Digital
            Test Step Comment: (1) MMI_gen 6890 (partly: Areas A, Area B, SH mode);                                  MMI_gen 6896 (partly: configuration ‘ON’, SH mode);                                      MMI_gen 6894 (partly: SH mode); (2) MMI_gen 6890 (partly: SH mode, un-concerned object), Table 34 (CSM), Table 38 (CSM), Table 35 (CSM)
            */
            DmiActions.ShowInstruction(this, "Press on area A1-A4, and area B, respectively, at least twice");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The White basic speed hook is toggled off (invisible) and on (visible) as it is pressed." +
                                Environment.NewLine +
                                "2. DMI does not display the Medium-grey basic speed hook." + Environment.NewLine +
                                "3. DMI does not display the Digital distance to target." + Environment.NewLine +
                                "4. DMI does not display the Digital release speed");

            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 20;

            /*
            Test Step 25
            Action: Press the speedometer once
            Expected Result: Verify the following information,The objects below is displayed on DMI, (toggle on)White Basic speed HookThe objects below are still not displayed on DMI,Medium-grey basic speed hookDistance to target (digital)Release Speed Digital
            Test Step Comment: (1) MMI_gen 6890 (partly: Areas A, SH mode, toggle on), MMI_gen 6896 (partly: configuration ‘OFF’, SH mode, toggle visible), MMI_gen 6894 (partly: SH mode), Information (paragraph 2) under MMI_gen 6898 (re-establish, operable);(2) MMI_gen 6890 (partly: SH mode, un-concerned object), Table 34 (CSM), Table 38 (CSM), Table 35 (CSM)
            */
            DmiActions.ShowInstruction(this, @"Press the speedometer once");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the White basic speed hook (toggled on)." + Environment.NewLine +
                                "2. DMI does not display the Medium-grey basic speed hook." + Environment.NewLine +
                                "3. DMI does not display the Digital distance to target." + Environment.NewLine +
                                "4. DMI does not display the Digital release speed.");

            /*
            Test Step 26
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
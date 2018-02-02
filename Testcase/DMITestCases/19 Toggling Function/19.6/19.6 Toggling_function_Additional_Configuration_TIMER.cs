using System;
using System.Collections.Generic;
using Testcase.Telegrams.EVCtoDMI;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 19.6 Toggling function: Additional Configuration ‘TIMER’
    /// TC-ID: 14.6
    /// 
    /// This test case verifies the display of related objects, basic speed hook(s), release speed digital and distance to target digital when driver activate the toggle function in cluding with retriggerable function, driver activate toggle function while the related objects still displaying.
    /// 
    /// Tested Requirements:
    /// MMI_gen 6897;
    /// 
    /// Scenario:
    /// 1.Perform SoM in SR mode, level 
    /// 1.Then, set the SR speed and SR distance.
    /// 2.Press on the specifc area to verify the toggle function.
    /// 3.Drive the train forward pass BG1 at position 100m. Then, acknowledge the OS mode and stop the train.BG1: packet 12, 21, 27 and 80 (Entering OS)
    /// 4.Press on the specifc area to verify the toggle function.
    /// 5.Perform the procedure to enter SH mode. Then, press on the specifc area to verify the toggle function.
    /// 
    /// Used files:
    /// 14_6.tdg
    /// </summary>
    public class TC_14_6_Toggling_Function : TestcaseBase
    {
        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint

            /*
            Test Step 1
            Action: Perform SoM in SR mode, level 1
            Expected Result: DMI displays Default window in SR mode, level 1.Note: The basic speed hook is appear for 10 seconds
            */
            // tested elsewhere: set SR mode, level 1
            //DmiActions.ShowInstruction(this, "Perform SoM in SR mode, level 1.");
            DmiActions.Complete_SoM_L1_SR(this);

            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Indication_Status_Target_Speed_Monitoring;
            EVC1_MMIDynamic.MMI_V_PERMITTED_KMH = 20;
            Wait_Realtime(10000);
            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Intervention_Status_Ceiling_Speed_Monitoring;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays Default window in SR mode, level 1." + Environment.NewLine +
                                "2. The Basic speed hook is displayed for 10s.");

            /*
            Test Step 2
            Action: Perform the following procedure, Press ‘Spec’ buttonPress ‘SR speed/distance’ buttonEnter and confirm the following data,SR speed = 40 km/hSR distance = 300 m
            Expected Result: DMI displays Special window
            */
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.No_window_specified;
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.SRSpeedDistance;
            EVC30_MMIRequestEnable.Send();

            DmiActions.ShowInstruction(this, "Press the ‘Spec’ button. Press the ‘SR speed/distance’ button");

            EVC11_MMICurrentSRRules.MMI_M_BUTTONS = Variables.MMI_M_BUTTONS.BTN_YES_DATA_ENTRY_COMPLETE;
            EVC11_MMICurrentSRRules.Send();

            DmiActions.ShowInstruction(this,
                "Enter and confirm the following data, SR speed = 40 km/h, SR distance = 300 m");

            EVC11_MMICurrentSRRules.DataElements = new List<Variables.DataElement>
            {
                new Variables.DataElement {Identifier = 15, EchoText = "40", QDataCheck = 0},
                new Variables.DataElement {Identifier = 16, EchoText = "300", QDataCheck = 0}
            };
            EVC11_MMICurrentSRRules.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Special window.");

            /*
            Test Step 3
            Action: Perform the following procedure, Press ‘Close’ buttonPress on sub-area A1.Note: Stopwatch is required
            Expected Result: Verify the following information,(1)   The following objects are displays for 10 seconds before disappear.White basic speed hookMedium-grey basic speed hookDistance to target (digital)
            Test Step Comment: (1) MMI_gen 6897 (partly: switch on the affected ETCS-objects for the configured duration);
            */
            DmiActions.ShowInstruction(this, "Press the ‘Close’ button. Press in sub-area A1");

            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Indication_Status_Target_Speed_Monitoring;
            EVC1_MMIDynamic.MMI_V_PERMITTED_KMH = 20;
            EVC1_MMIDynamic.MMI_V_TARGET_KMH = 15;
            EVC1_MMIDynamic.MMI_O_BRAKETARGET = 200000;
            Wait_Realtime(10000);
            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Intervention_Status_Ceiling_Speed_Monitoring;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the White basic speed hook for 10s then removes it." +
                                Environment.NewLine +
                                "2. DMI displays the Medium-grey basic speed hook for 10s then removes it." +
                                Environment.NewLine +
                                "3. DMI displays the Digital distance to target for 10s then removes it.");

            /*
            Test Step 4
            Action: Repeat action step 3 for sub-area A2-A4 and area B respectively
            Expected Result: See expected result of step 3
            */
            DmiActions.ShowInstruction(this, "Press the ‘Close’ button. Press in sub-area A2");

            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Indication_Status_Target_Speed_Monitoring;
            EVC1_MMIDynamic.MMI_V_PERMITTED_KMH = 20;
            Wait_Realtime(10000);
            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Intervention_Status_Ceiling_Speed_Monitoring;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the White basic speed hook for 10s then removes it." +
                                Environment.NewLine +
                                "2. DMI displays the Medium-grey basic speed hook for 10s then removes it." +
                                Environment.NewLine +
                                "3. DMI displays the Digital distance to target for 10s then removes it.");

            DmiActions.ShowInstruction(this, "Press the ‘Close’ button. Press in sub-area A3");

            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Indication_Status_Target_Speed_Monitoring;
            EVC1_MMIDynamic.MMI_V_PERMITTED_KMH = 20;
            EVC1_MMIDynamic.MMI_V_TARGET_KMH = 15;
            EVC1_MMIDynamic.MMI_O_BRAKETARGET = 200000;
            Wait_Realtime(10000);
            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Intervention_Status_Ceiling_Speed_Monitoring;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the White basic speed hook for 10s then removes it." +
                                Environment.NewLine +
                                "2. DMI displays the Medium-grey basic speed hook for 10s then removes it." +
                                Environment.NewLine +
                                "3. DMI displays the Digital distance to target for 10s then removes it.");

            DmiActions.ShowInstruction(this, "Press the ‘Close’ button. Press in sub-area A4");

            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Indication_Status_Target_Speed_Monitoring;
            EVC1_MMIDynamic.MMI_V_PERMITTED_KMH = 20;
            Wait_Realtime(10000);
            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Intervention_Status_Ceiling_Speed_Monitoring;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the White basic speed hook for 10s then removes it." +
                                Environment.NewLine +
                                "2. DMI displays the Medium-grey basic speed hook for 10s then removes it." +
                                Environment.NewLine +
                                "3. DMI displays the Digital distance to target for 10s then removes it.");

            DmiActions.ShowInstruction(this, "Press the ‘Close’ button. Press in area B");

            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Indication_Status_Target_Speed_Monitoring;
            EVC1_MMIDynamic.MMI_V_PERMITTED_KMH = 20;
            EVC1_MMIDynamic.MMI_V_TARGET_KMH = 15;
            EVC1_MMIDynamic.MMI_O_BRAKETARGET = 200000;
            Wait_Realtime(10000);
            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Intervention_Status_Ceiling_Speed_Monitoring;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the White basic speed hook for 10s then removes it." +
                                Environment.NewLine +
                                "2. DMI displays the Medium-grey basic speed hook for 10s then removes it." +
                                Environment.NewLine +
                                "3. DMI displays the Digital distance to target for 10s then removes it.");

            /*
            Test Step 5
            Action: Perform the following procedure, Press on sub-area A1.Wait for 5 secondsPress on sub-area A1 again
            Expected Result: Verify the following information,(1)    The following objects are displays for 10 seconds before disappear.White basic speed hookMedium-grey basic speed hookDistance to target (digital)
            Test Step Comment: (1) MMI_gen 6897 (partly: retriggerable);
            */
            DmiActions.ShowInstruction(this, "Press in sub-area A1. Wait 5s, then press in sub-area A1 again");

            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Indication_Status_Target_Speed_Monitoring;
            EVC1_MMIDynamic.MMI_V_PERMITTED_KMH = 20;
            EVC1_MMIDynamic.MMI_V_TARGET_KMH = 15;
            EVC1_MMIDynamic.MMI_O_BRAKETARGET = 200000;
            Wait_Realtime(10000);
            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Intervention_Status_Ceiling_Speed_Monitoring;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the White basic speed hook for 10s then removes it." +
                                Environment.NewLine +
                                "2. DMI displays the Medium-grey basic speed hook for 10s then removes it." +
                                Environment.NewLine +
                                "3. DMI displays the Digital distance to target for 10s then removes it.");

            /*
            Test Step 6
            Action: Repeat action step 5 for sub-area A2-A4 and area B respectively
            Expected Result: See expected result of step 5
            */
            DmiActions.ShowInstruction(this, "Press in sub-area A2. Wait 5s, then press in sub-area A2 again");

            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Indication_Status_Target_Speed_Monitoring;
            EVC1_MMIDynamic.MMI_V_PERMITTED_KMH = 20;
            EVC1_MMIDynamic.MMI_V_TARGET_KMH = 15;
            EVC1_MMIDynamic.MMI_O_BRAKETARGET = 200000;
            Wait_Realtime(10000);
            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Intervention_Status_Ceiling_Speed_Monitoring;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the White basic speed hook for 10s then removes it." +
                                Environment.NewLine +
                                "2. DMI displays the Medium-grey basic speed hook for 10s then removes it." +
                                Environment.NewLine +
                                "3. DMI displays the Digital distance to target for 10s then removes it.");

            DmiActions.ShowInstruction(this, "Press in sub-area A3. Wait 5s, then press in sub-area A3 again");

            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Indication_Status_Target_Speed_Monitoring;
            EVC1_MMIDynamic.MMI_V_PERMITTED_KMH = 20;
            EVC1_MMIDynamic.MMI_V_TARGET_KMH = 15;
            EVC1_MMIDynamic.MMI_O_BRAKETARGET = 200000;
            Wait_Realtime(10000);
            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Intervention_Status_Ceiling_Speed_Monitoring;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the White basic speed hook for 10s then removes it." +
                                Environment.NewLine +
                                "2. DMI displays the Medium-grey basic speed hook for 10s then removes it." +
                                Environment.NewLine +
                                "3. DMI displays the Digital distance to target for 10s then removes it.");

            DmiActions.ShowInstruction(this, "Press in sub-area A4. Wait 5s, then press in sub-area A4 again");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the White basic speed hook for 10s then removes it." +
                                Environment.NewLine +
                                "2. DMI displays the Medium-grey basic speed hook for 10s then removes it." +
                                Environment.NewLine +
                                "3. DMI displays the Digital distance to target for 10s then removes it.");

            DmiActions.ShowInstruction(this, "Press in area B. Wait 5s, then press in area B again");

            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Indication_Status_Target_Speed_Monitoring;
            EVC1_MMIDynamic.MMI_V_PERMITTED_KMH = 20;
            EVC1_MMIDynamic.MMI_V_TARGET_KMH = 15;
            EVC1_MMIDynamic.MMI_O_BRAKETARGET = 200000;
            Wait_Realtime(10000);
            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Intervention_Status_Ceiling_Speed_Monitoring;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the White basic speed hook for 10s then removes it." +
                                Environment.NewLine +
                                "2. DMI displays the Medium-grey basic speed hook for 10s then removes it." +
                                Environment.NewLine +
                                "3. DMI displays the Digital distance to target for 10s then removes it.");

            /*
            Test Step 7
            Action: Drive the train forward with speed = 10 km/h
            Expected Result: Train is moving forward
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 10;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays speed = 10 km/h.");

            /*
            Test Step 8
            Action: Drive the train forward pass BG1. Then, press an acknowledgement of OS mode in sub-area C1
            Expected Result: DMI displays in OS mode, level 1.Note: The basic speed hook is appear for 10 seconds
            */
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.OnSight;

            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Indication_Status_Target_Speed_Monitoring;
            EVC1_MMIDynamic.MMI_V_PERMITTED_KMH = 20;
            EVC1_MMIDynamic.MMI_O_BRAKETARGET = 200000;
            Wait_Realtime(10000);
            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Intervention_Status_Ceiling_Speed_Monitoring;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in OS mode, level 1." + Environment.NewLine +
                                "2. DMI displays the White basic speed hook for 10s then removes it.");

            /*
            Test Step 9
            Action: Stop the train.Then, repeat action step 3-5
            Expected Result: See expected result of step 3-5 for the following objects,White basic speed hookMedium-grey basic speed hookDistance to target (digital)Release speed digital
            Test Step Comment: (1) MMI_gen 6897;
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 0;

            DmiActions.ShowInstruction(this, "Press the ‘Close’ button. Press in sub-area A1");

            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Indication_Status_Target_Speed_Monitoring;
            EVC1_MMIDynamic.MMI_V_PERMITTED_KMH = 20;
            EVC1_MMIDynamic.MMI_V_TARGET_KMH = 30;
            EVC1_MMIDynamic.MMI_O_BRAKETARGET = 200000;
            Wait_Realtime(10000);
            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Intervention_Status_Ceiling_Speed_Monitoring;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the White basic speed hook for 10s then removes it." +
                                Environment.NewLine +
                                "2. DMI displays the Medium-grey basic speed hook for 10s then removes it." +
                                Environment.NewLine +
                                "3. DMI displays the Digital distance to target for 10s then removes it." +
                                Environment.NewLine +
                                "4. DMI does not display the Digital release speed");

            DmiActions.ShowInstruction(this, "Press the ‘Close’ button. Press in sub-area A2");

            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Indication_Status_Target_Speed_Monitoring;
            EVC1_MMIDynamic.MMI_V_PERMITTED_KMH = 20;
            EVC1_MMIDynamic.MMI_V_TARGET_KMH = 30;
            EVC1_MMIDynamic.MMI_O_BRAKETARGET = 200000;
            Wait_Realtime(10000);
            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Intervention_Status_Ceiling_Speed_Monitoring;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the White basic speed hook for 10s then removes it." +
                                Environment.NewLine +
                                "2. DMI displays the Medium-grey basic speed hook for 10s then removes it." +
                                Environment.NewLine +
                                "3. DMI displays the Digital distance to target for 10s then removes it." +
                                Environment.NewLine +
                                "4. DMI does not display the Digital release speed");

            DmiActions.ShowInstruction(this, "Press the ‘Close’ button. Press in sub-area A3");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the White basic speed hook for 10s then removes it." +
                                Environment.NewLine +
                                "2. DMI displays the Medium-grey basic speed hook for 10s then removes it." +
                                Environment.NewLine +
                                "3. DMI displays the Digital distance to target for 10s then removes it." +
                                Environment.NewLine +
                                "4. DMI does not display the Digital release speed");

            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Indication_Status_Target_Speed_Monitoring;
            EVC1_MMIDynamic.MMI_V_PERMITTED_KMH = 20;
            EVC1_MMIDynamic.MMI_V_TARGET_KMH = 30;
            EVC1_MMIDynamic.MMI_O_BRAKETARGET = 200000;
            Wait_Realtime(10000);
            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Intervention_Status_Ceiling_Speed_Monitoring;

            DmiActions.ShowInstruction(this, "Press the ‘Close’ button. Press in sub-area A4");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the White basic speed hook for 10s then removes it." +
                                Environment.NewLine +
                                "2. DMI displays the Medium-grey basic speed hook for 10s then removes it." +
                                Environment.NewLine +
                                "3. DMI displays the Digital distance to target for 10s then removes it." +
                                Environment.NewLine +
                                "4. DMI does not display the Digital release speed");

            DmiActions.ShowInstruction(this, "Press the ‘Close’ button. Press in area B");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the White basic speed hook for 10s then removes it." +
                                Environment.NewLine +
                                "2. DMI displays the Medium-grey basic speed hook for 10s then removes it." +
                                Environment.NewLine +
                                "3. DMI displays the Digital distance to target for 10s then removes it." +
                                Environment.NewLine +
                                "4. DMI does not display the Digital release speed");

            DmiActions.ShowInstruction(this, "Press in sub-area A1. Wait 5s, then press in sub-area A1 again");

            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Indication_Status_Target_Speed_Monitoring;
            EVC1_MMIDynamic.MMI_V_PERMITTED_KMH = 20;
            EVC1_MMIDynamic.MMI_V_TARGET_KMH = 30;
            EVC1_MMIDynamic.MMI_O_BRAKETARGET = 200000;
            Wait_Realtime(10000);
            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Intervention_Status_Ceiling_Speed_Monitoring;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the White basic speed hook for 10s then removes it." +
                                Environment.NewLine +
                                "2. DMI displays the Medium-grey basic speed hook for 10s then removes it." +
                                Environment.NewLine +
                                "3. DMI displays the Digital distance to target for 10s then removes it." +
                                Environment.NewLine +
                                "4. DMI does not display the Digital release speed");

            /*
            Test Step 10
            Action: Perform the following procedure,Press ‘Main’ button.Press and hold ‘Shunting’ button at least 2 seconds.Release the pressed button
            Expected Result: DMI displays in SH mode, level 1.Note: The basic speed hook is appear for 10 seconds
            */
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.No_window_specified;
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.Shunting;
            EVC30_MMIRequestEnable.Send();

            DmiActions.ShowInstruction(this,
                "Press the ‘Main’ button. Press and hold the ‘Shunting’ button at least 2 seconds. Release the ‘Shunting’ button");

            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.Shunting;

            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Intervention_Status_Ceiling_Speed_Monitoring;
            EVC1_MMIDynamic.MMI_V_PERMITTED_KMH = 20;
            EVC1_MMIDynamic.MMI_V_TARGET_KMH = -1;
            EVC1_MMIDynamic.MMI_O_BRAKETARGET = 200000;
            Wait_Realtime(10000);

            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Indication_Status_Target_Speed_Monitoring;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in SH mode, level 1" + Environment.NewLine +
                                "2. DMI displays the Basic speed hook for 10s then removes it.");

            /*
            Test Step 11
            Action: Repeat action step 3-5
            Expected Result: See expected result of step 3-5 for the following objects,White basic speed hookMedium-grey basic speed hook (if any)
            Test Step Comment: (1) MMI_gen 6897;
            */
            DmiActions.ShowInstruction(this, "Press the ‘Close’ button. Press in sub-area A1");

            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Intervention_Status_Ceiling_Speed_Monitoring;
            EVC1_MMIDynamic.MMI_V_PERMITTED_KMH = 20;
            EVC1_MMIDynamic.MMI_V_TARGET_KMH = 10;
            Wait_Realtime(10000);
            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Indication_Status_Target_Speed_Monitoring;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the White basic speed hook for 10s then removes it." +
                                Environment.NewLine +
                                "2. DMI displays the Medium-grey basic speed hook for 10s then removes it.");

            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Intervention_Status_Ceiling_Speed_Monitoring;
            EVC1_MMIDynamic.MMI_V_PERMITTED_KMH = 20;
            Wait_Realtime(10000);
            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Indication_Status_Target_Speed_Monitoring;

            DmiActions.ShowInstruction(this, "Press the ‘Close’ button. Press in sub-area A2");

            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Intervention_Status_Ceiling_Speed_Monitoring;
            EVC1_MMIDynamic.MMI_V_PERMITTED_KMH = 20;
            EVC1_MMIDynamic.MMI_V_TARGET_KMH = 10;
            Wait_Realtime(10000);
            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Indication_Status_Target_Speed_Monitoring;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the White basic speed hook for 10s then removes it." +
                                Environment.NewLine +
                                "2. DMI displays the Medium-grey basic speed hook for 10s then removes it.");

            DmiActions.ShowInstruction(this, "Press the ‘Close’ button. Press in sub-area A3");

            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Intervention_Status_Ceiling_Speed_Monitoring;
            EVC1_MMIDynamic.MMI_V_PERMITTED_KMH = 20;
            EVC1_MMIDynamic.MMI_V_TARGET_KMH = 10;
            Wait_Realtime(10000);
            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Indication_Status_Target_Speed_Monitoring;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the White basic speed hook for 10s then removes it." +
                                Environment.NewLine +
                                "2. DMI displays the Medium-grey basic speed hook for 10s then removes it.");

            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Intervention_Status_Ceiling_Speed_Monitoring;
            EVC1_MMIDynamic.MMI_V_PERMITTED_KMH = 20;
            EVC1_MMIDynamic.MMI_V_TARGET_KMH = 10;
            Wait_Realtime(10000);

            DmiActions.ShowInstruction(this, "Press the ‘Close’ button. Press in sub-area A4");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the White basic speed hook for 10s then removes it." +
                                Environment.NewLine +
                                "2. DMI displays the Medium-grey basic speed hook for 10s then removes it.");

            DmiActions.ShowInstruction(this, "Press the ‘Close’ button. Press in area B");

            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Intervention_Status_Ceiling_Speed_Monitoring;
            EVC1_MMIDynamic.MMI_V_PERMITTED_KMH = 20;
            EVC1_MMIDynamic.MMI_V_TARGET_KMH = 10;
            Wait_Realtime(10000);
            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Indication_Status_Target_Speed_Monitoring;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the White basic speed hook for 10s then removes it." +
                                Environment.NewLine +
                                "2. DMI displays the Medium-grey basic speed hook for 10s then removes it.");

            DmiActions.ShowInstruction(this, "Press in sub-area A1. Wait 5s, then press in sub-area A1 again");

            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Intervention_Status_Ceiling_Speed_Monitoring;
            EVC1_MMIDynamic.MMI_V_PERMITTED_KMH = 20;
            EVC1_MMIDynamic.MMI_V_TARGET_KMH = 10;
            Wait_Realtime(10000);
            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Indication_Status_Target_Speed_Monitoring;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the White basic speed hook for 10s then removes it." +
                                Environment.NewLine +
                                "2. DMI displays the Medium-grey basic speed hook for 10s then removes it.");

            /*
            Test Step 12
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
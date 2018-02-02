using System;
using Testcase.Telegrams.EVCtoDMI;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 22.9.11 Hide PA Function configured ‘STORED’ with re-activate cabin
    /// TC-ID: 17.9.11
    /// 
    /// This test case  is to verify Hide PA function after re-activate the cabin.The Hide PA function state shall store and present similar as before deactivation. The Hide PA function shall comply with conditions of  [MMI-ETCS-gen].
    /// 
    /// Tested Requirements:
    /// MMI_gen 9948;
    /// 
    /// Scenario:
    /// 1.Active cabin A. Perform SoM to SR mode, level 
    /// 12.Press Hide PA button and verify that the Planning area is disappeared 
    /// 3.Deactivare and Activate the cabin A and verify that the Planning area is disappeared
    /// 4.Press sensitive area at main area D then the Palnning area is reappeared
    /// 5.Retest with FS and OS mode. Verify that the Planning area area is disappeared after reactivate the cabin
    /// 
    /// Used files:
    /// 17_9_11.tdg
    /// </summary>
    public class TC_ID_17_9_11_Hide_PA_Function_configured_STORED_with_re_activate_cabin : TestcaseBase
    {

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint
            TraceInfo("This test case requires an ATP configuration change - " +
                      "See Precondition requirements. If this is not done manually, the test may fail!");

            /*
            Test Step 1
            Action: Power on the system Activate cabin A
            Expected Result: DMI displays Driver ID window
            */
            DmiActions.Start_ATP();
            DmiActions.Activate_Cabin_1(this);
            DmiActions.Set_Driver_ID(this, "1234");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Driver ID window.");

            /*
            Test Step 2
            Action: Perform SoM to SR mode, level 1
            Expected Result: DMI displays in SR mode, level 1The Planning area is appeared on DMI
            */
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Level = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_LEVEL.L1;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode =
                EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.StaffResponsible;
            DmiActions.Finished_SoM_Default_Window(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in SR mode, Level 1." + Environment.NewLine +
                                "2. The Planning Area is displayed.");

            /*
            Test Step 3
            Action: Press Hide PA button
            Expected Result: The Planning area is disappeared from DMI
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Hide PA’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The Planning Area is removed from area D.");

            /*
            Test Step 4
            Action: Deactive and reacitvate the cabin A
            Expected Result: DMI displays Driver ID window
            */
            DmiActions.Deactivate_Cabin(this);
            DmiActions.Activate_Cabin_1(this);
            DmiActions.Set_Driver_ID(this, "1234");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Driver ID window.");

            /*
            Test Step 5
            Action: Perform SoM to SR mode, level 1
            Expected Result: DMI displays in SR mode, level 1The Planning area is not appear on DMI
            Test Step Comment: MMI_gen 9948 (partly:SR);
            */
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Level = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_LEVEL.L1;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode =
                EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.StaffResponsible;
            DmiActions.Finished_SoM_Default_Window(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in SR mode, Level 1." + Environment.NewLine +
                                "2. The Planning Area is not displayed.");

            /*
            Test Step 6
            Action: Press at sensitive area in main area D
            Expected Result: The planning area is reappeared by this activation
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press in the sensitive area in area D");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The Planning Area is displayed.");

            /*
            Test Step 7
            Action: Drive the train forward with speed = 30 km/h pass BG1
            Expected Result: Mode changes to FS modeThe Planning area is appeared on DMI
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 30;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.FullSupervision;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The Planning Area is still displayed.");

            /*
            Test Step 8
            Action: Stop the train and then press Hide PA button
            Expected Result: Train is at standstillThe Planning area is disappeared from DMI
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 30;
            DmiActions.ShowInstruction(this, @"Press the ‘Hide PA’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The Planning Area is removed from area D." + Environment.NewLine +
                                "2. The speed displayed is 30 km/h.");

            /*
            Test Step 9
            Action: Deactive and reacitvate the cabin A
            Expected Result: DMI displays Driver ID window
            */
            DmiActions.Deactivate_Cabin(this);
            DmiActions.Activate_Cabin_1(this);
            DmiActions.Set_Driver_ID(this, "1234");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Driver ID window.");

            /*
            Test Step 10
            Action: Perform SoM to SR mode, level 1
            Expected Result: DMI displays in SR mode, leve1
            */
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Level = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_LEVEL.L1;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode =
                EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.StaffResponsible;
            DmiActions.Finished_SoM_Default_Window(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in SR mode, Level 1.");

            /*
            Test Step 11
            Action: Drive the train forward with speed = 30 km/h pass BG2
            Expected Result: DMI displays in FS modeThe Planning area is not appear on DMI
            Test Step Comment: MMI_gen 9948 (partly:FS);
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 0;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.FullSupervision;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in FS mode." + Environment.NewLine +
                                "2. The Planning Area is not displayed.");

            /*
            Test Step 12
            Action: Press at sensitive area in main area D
            Expected Result: The planning area is reappeared by this activation
            */
            DmiActions.ShowInstruction(this, @"Press in the sensitive area in main area D");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The Planning Area is re-displayed.");

            /*
            Test Step 13
            Action: Pass BG3 and the comfirm OS mode
            Expected Result: Mode chages to OS modeThe Planning area is appeared on DMI
            */
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 275;
            EVC8_MMIDriverMessage.Send();

            DmiActions.ShowInstruction(this, "Acknowledge OS mode");
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.OnSight;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in OS mode." + Environment.NewLine +
                                "2. The Planning Area is displayed.");

            /*
            Test Step 14
            Action: Stop the train and then press Hide PA button
            Expected Result: Train is at standstillThe Planning area is disappeared from DMI
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 0;

            DmiActions.ShowInstruction(this, @"Press the ‘Hide PA’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The Planning Area is removed from area D." + Environment.NewLine +
                                "2. The speed displayed is 0 km/h.");

            /*
            Test Step 15
            Action: Deactive and reacitvate the cabin A
            Expected Result: DMI displays Driver ID window
            */
            DmiActions.Deactivate_Cabin(this);
            DmiActions.Activate_Cabin_1(this);
            DmiActions.Set_Driver_ID(this, "1234");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Driver ID window.");

            /*
            Test Step 16
            Action: Perform SoM to SR mode, level 1
            Expected Result: DMI displays in SR mode, leve1
            */
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Level = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_LEVEL.L1;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode =
                EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.StaffResponsible;
            DmiActions.Finished_SoM_Default_Window(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in SR mode, Level 1.");

            /*
            Test Step 17
            Action: Drive the train forward with speed = 30 km/h pass BG4
            Expected Result: OS mode Acknowledgment is requested
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 0;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 275;
            EVC8_MMIDriverMessage.Send();


            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Acknowledgement of OS mode is requested.");

            /*
            Test Step 18
            Action: Acknowledge OS mode
            Expected Result: Mode chages to OS modeThe Planning area is not appear on DMI
            Test Step Comment: MMI_gen 9948 (partly:OS);
            */
            DmiActions.ShowInstruction(this, "Acknowledge OS mode");
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.OnSight;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in OS mode." + Environment.NewLine +
                                "2. The Planning Area is displayed.");

            /*
            Test Step 19
            Action: Press at sensitive area in main area D
            Expected Result: The planning area is reappeared by this activation
            */
            DmiActions.ShowInstruction(this, @"Press in the sensitive area in main area D");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The Planning Area is re-displayed.");

            /*
            Test Step 20
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
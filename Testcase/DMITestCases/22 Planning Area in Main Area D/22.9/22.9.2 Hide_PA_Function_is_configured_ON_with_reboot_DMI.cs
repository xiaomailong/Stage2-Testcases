using System;
using Testcase.Telegrams.EVCtoDMI;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 22.9.2 Hide PA Function is configured ‘ON’ with reboot DMI
    /// TC-ID: 17.9.2
    /// 
    /// This test case verifies that if the Hide PA Function is configured as “On” and then the DMI rebooted, the PA and Hide PA button shall be enable. The ‘ON’ configured shall comply with condition of  [MMI-ETCS-gen].
    /// 
    /// Tested Requirements:
    /// MMI_gen 7341; MMI_gen 2996 (partly: ON);
    /// 
    /// Scenario:
    /// Active cabin A. Perform SoM to SR mode, level 1Start driving the train forward and Pass BG
    /// 1.Mode changes to FS mode.Turn off and then turn on DMI. The PA and Hide PA button are appeared on the area D of the DMI.
    /// 
    /// Used files:
    /// 17_9_2.tdg
    /// </summary>
    public class TC_ID_17_9_2_Hide_PA_Function_is_configured_ON_with_reboot_DMI : TestcaseBase
    {

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint
            TraceInfo("This test case requires an ATP configuration change - " +
                      "See Precondition requirements. If this is not done manually, the test may fail!");

            /*
            Test Step 1
            Action: Activate cabin A and Perform SoM to SR mode, Level 1
            Expected Result: DMI displays in SR mode, level 1
            */
            DmiActions.Complete_SoM_L1_SR(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in SR mode, Level 1.");

            /*
            Test Step 2
            Action: Drive the train forward with speed = 40 km/h pass BG1
            Expected Result: DMI shows “Entering FS” message and DMI displays the Planning area.The Hide PA button is appeared on  the area D of the DMI
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
                                "3. The ‘Hide PA’ button is displayed in area D.");

            // Remove the message
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 4;
            EVC8_MMIDriverMessage.Send();

            /*
            Test Step 3
            Action: Press Hide PA button
            Expected Result: The Planning area is disappeared from the area D of DMI
            Test Step Comment:  Hide PA button
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Hide PA’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The Planning Area is removed from area D.");

            /*
            Test Step 4
            Action: Turn off power of DMI
            Expected Result: DMI is power off
            */
            DmiActions.ShowInstruction(this, @"Power down the system");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI is blank.");

            /*
            Test Step 5
            Action: Turn on power of DMI
            Expected Result: DMI is power on DMI displays the Planning area The Hide PA button is appeared on the area D of the DMI
            Test Step Comment: MMI_gen 7341;  MMI_gen 2996 (partly: ON); Hide PA icon
            */
            DmiActions.ShowInstruction(this, @"Wait 10s and power up the system");
            DmiActions.Complete_SoM_L1_FS(this);
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 40;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN = 10000;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Planning Area." + Environment.NewLine +
                                "2. The ‘Hide PA’ button is re-displayed in area D.");

            /*
            Test Step 6
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
using System;
using Testcase.Telegrams.EVCtoDMI;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 22.9.6 Hide PA Function is configured ‘TIMER’ with reboot DMI
    /// TC-ID: 17.9.5
    /// 
    /// This test case verifies that if the Hide PA Function is configured as ‘Timer’ and then reboot the Dmi. The Hide PA button shall be enable.  The ‘Timer’ configured shall comply with condition of  [MMI-ETCS-gen].
    /// 
    /// Tested Requirements:
    /// MMI_gen 7341; MMI_gen 2996 (partly: Timer);
    /// 
    /// Scenario:
    /// Set HIDE PA FUNCTION configuration as Timer Set HIDE PA TIMER configuration as 20s
    /// Activate cabin A. Driver enters the Driver ID and performs brake test. Then the driver selects level 1, Train data, and validate the train data. After that driver enter Train running number and confirm SR mode. At 100 m, pass BG1 with pkt 12, pkt 21 and pkt 
    /// 27.Mode changes to FS modeTurn off/on  DMI. The Hide PA button is appeared from the area D of DMISet HIDE PA TIMER configuration as 30s, 40s, 50s, 60s, 70s and 80s. Repeat test step 1-6 and verify the ‘Timer’ function of the Hide PA Function.
    /// 
    /// Used files:
    /// 17_9_5.tdg
    /// </summary>
    public class TC_ID_17_9_5_TC_ID_Hide_PA_Function_is_configured_TIMER_with_reboot_DMI : TestcaseBase
    {
        public override bool TestcaseEntryPoint()
        {
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 23990;
            // Testcase entrypoint
            TraceInfo("This test case requires an ATP configuration change - " +
                      "See Precondition requirements. If this is not done manually, the test may fail!");

            MakeTestStepHeader(1, UniqueIdentifier++, "Power On the system", "DMI displays the default window");
            /*
            Test Step 1
            Action: Power On the system
            Expected Result: DMI displays the default window
            */
            StartUp();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Default window.");

            MakeTestStepHeader(2, UniqueIdentifier++, "Activate cabin A and Perform SoM to SR mode, Level 1",
                "DMI displays in SR mode, level 1");
            /*
            Test Step 2
            Action: Activate cabin A and Perform SoM to SR mode, Level 1
            Expected Result: DMI displays in SR mode, level 1
            */
            // Tested elsewhere, force SoM
            DmiActions.Complete_SoM_L1_SR(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in SR mode, Level 1.");

            MakeTestStepHeader(3, UniqueIdentifier++, "Drive the train forward with speed = 40 km/h pass BG1",
                "DMI shows “Entering FS” message.DMI displays the Planning area. The Hide PA button is appeared on  the area D of the DMI");
            /*
            Test Step 3
            Action: Drive the train forward with speed = 40 km/h pass BG1
            Expected Result: DMI shows “Entering FS” message.DMI displays the Planning area. The Hide PA button is appeared on  the area D of the DMI
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

            MakeTestStepHeader(4, UniqueIdentifier++, "Press Hide PA button",
                "The Planning area is disappeared and hidden from main area D for 20s.After 20s the planning area is displayed.Verify that the Hide PA button is displayed on the planning area");
            /*
            Test Step 4
            Action: Press Hide PA button
            Expected Result: The Planning area is disappeared and hidden from main area D for 20s.After 20s the planning area is displayed.Verify that the Hide PA button is displayed on the planning area
            Test Step Comment: MMI_gen 7341;
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press the ‘Hide PA’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The Planning Area is hidden for 20s and then re-displayed in area D." +
                                Environment.NewLine +
                                "3. The ‘Hide PA’ button is displayed in area D.");

            MakeTestStepHeader(5, UniqueIdentifier++, "Turn off power of DMI", "DMI is power off");
            /*
            Test Step 5
            Action: Turn off power of DMI
            Expected Result: DMI is power off
            */
            DmiActions.ShowInstruction(this, @"Power down the system");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI is blank.");

            MakeTestStepHeader(6, UniqueIdentifier++, "Turn on power of DMI",
                "DMI is power on DMI displays the Planning area The Hide PA button is appeared on  the main area D of the DMI");
            /*
            Test Step 6
            Action: Turn on power of DMI
            Expected Result: DMI is power on DMI displays the Planning area The Hide PA button is appeared on  the main area D of the DMI
            Test Step Comment: MMI_gen 7341;  MMI_gen 2996 (partly: Timer); Hide PA icon
            */
            DmiActions.ShowInstruction(this, @"Wait 10s and power up the system");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The Planning Area is re-displayed in area D." + Environment.NewLine +
                                "2. The ‘Hide PA’ button is re-displayed in area D.");

            MakeTestStepHeader(7, UniqueIdentifier++, "Set HIDE PA TIMER configuration as 30s and repeat test step 1-6",
                "The Planning area is disappeared and hidden from main area D for 30s.After 30s the planning area is displayed.Verify that the Hide PA button is displayed at sub-area D14 on the planning area");
            /*
            Test Step 7
            Action: Set HIDE PA TIMER configuration as 30s and repeat test step 1-6
            Expected Result: The Planning area is disappeared and hidden from main area D for 30s.After 30s the planning area is displayed.Verify that the Hide PA button is displayed at sub-area D14 on the planning area
            Test Step Comment: MMI_gen 7341;   MMI_gen 2996 (partly: Timer);
            */
            DmiActions.ShowInstruction(this,
                @"Power down the system and wait 10s. Set the configuration HIDE_PA_TIMER = 30 and power up the system");

            // Repeat Step 1
            DmiActions.Start_ATP();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Default window.");

            // Repeat Step 2
            DmiActions.Complete_SoM_L1_SR(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in SR mode, Level 1.");

            // Repeat Step 3
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

            // Repeat Step 4
            DmiActions.ShowInstruction(this, @"Press the ‘Hide PA’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The Planning Area is hidden for 30s and then re-displayed in area D." +
                                Environment.NewLine +
                                "3. The ‘Hide PA’ button is displayed in sub-area D14.");

            // Repeat Step 5
            DmiActions.ShowInstruction(this, @"Power down the system");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI is blank.");

            // Repeat Step 6
            DmiActions.ShowInstruction(this, @"Wait 10s and power up the system");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The Planning Area is re-displayed in area D." + Environment.NewLine +
                                "2. The ‘Hide PA’ button is re-displayed in area D14.");

            MakeTestStepHeader(8, UniqueIdentifier++, "Set HIDE PA TIMER configuration as 40s and repeat test step 1-6",
                "The Planning area is disappeared and hidden from main area D for 40s.After 40s the planning area is displayed.Verify that the Hide PA button is displayed at sub-area D14 on the planning area");
            /*
            Test Step 8
            Action: Set HIDE PA TIMER configuration as 40s and repeat test step 1-6
            Expected Result: The Planning area is disappeared and hidden from main area D for 40s.After 40s the planning area is displayed.Verify that the Hide PA button is displayed at sub-area D14 on the planning area
            */
            DmiActions.ShowInstruction(this,
                @"Power down the system and wait 10s. Set the configuration HIDE_PA_TIMER = 40 and power up the system");

            // Repeat Step 1
            DmiActions.Start_ATP();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Default window.");

            // Repeat Step 2
            DmiActions.Complete_SoM_L1_SR(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in SR mode, Level 1.");

            // Repeat Step 3
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

            // Repeat Step 4
            DmiActions.ShowInstruction(this, @"Press the ‘Hide PA’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The Planning Area is hidden for 40s and then re-displayed in area D." +
                                Environment.NewLine +
                                "3. The ‘Hide PA’ button is displayed in sub-area D14.");

            // Repeat Step 5
            DmiActions.ShowInstruction(this, @"Power down the system");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI is blank.");

            // Repeat Step 6
            DmiActions.ShowInstruction(this, @"Wait 10s and power up the system");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The Planning Area is re-displayed in area D." + Environment.NewLine +
                                "2. The ‘Hide PA’ button is re-displayed in area D14.");

            MakeTestStepHeader(9, UniqueIdentifier++, "Set HIDE PA TIMER configuration as 50s and repeat test step 1-6",
                "The Planning area is disappeared and hidden from main area D for 50s.After 50s the planning area is displayed.Verify that the Hide PA button is displayed at sub-area D14 on the planning area");
            /*
            Test Step 9
            Action: Set HIDE PA TIMER configuration as 50s and repeat test step 1-6
            Expected Result: The Planning area is disappeared and hidden from main area D for 50s.After 50s the planning area is displayed.Verify that the Hide PA button is displayed at sub-area D14 on the planning area
            */
            DmiActions.ShowInstruction(this,
                @"Power down the system and wait 10s. Set the configuration HIDE_PA_TIMER = 50 and power up the system");

            // Repeat Step 1
            DmiActions.Start_ATP();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Default window.");

            // Repeat Step 2
            DmiActions.Complete_SoM_L1_SR(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in SR mode, Level 1.");

            // Repeat Step 3
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

            // Repeat Step 4
            DmiActions.ShowInstruction(this, @"Press the ‘Hide PA’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The Planning Area is hidden for 50s and then re-displayed in area D." +
                                Environment.NewLine +
                                "3. The ‘Hide PA’ button is displayed in sub-area D14.");

            // Repeat Step 5
            DmiActions.ShowInstruction(this, @"Power down the system");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI is blank.");

            // Repeat Step 6
            DmiActions.ShowInstruction(this, @"Wait 10s and power up the system");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The Planning Area is re-displayed in area D." + Environment.NewLine +
                                "2. The ‘Hide PA’ button is re-displayed in area D14.");

            MakeTestStepHeader(10, UniqueIdentifier++,
                "Set HIDE PA TIMER configuration as 60s and repeat test step 1-6",
                "The Planning area is disappeared and hidden from main area D for 60s.After 60s the planning area is displayed.Verify that the Hide PA button is displayed at sub-area D14 on the planning area");
            /*
            Test Step 10
            Action: Set HIDE PA TIMER configuration as 60s and repeat test step 1-6
            Expected Result: The Planning area is disappeared and hidden from main area D for 60s.After 60s the planning area is displayed.Verify that the Hide PA button is displayed at sub-area D14 on the planning area
            */
            DmiActions.ShowInstruction(this,
                @"Power down the system and wait 10s. Set the configuration HIDE_PA_TIMER = 60 and power up the system");

            // Repeat Step 1
            DmiActions.Start_ATP();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Default window.");

            // Repeat Step 2
            DmiActions.Complete_SoM_L1_SR(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in SR mode, Level 1.");

            // Repeat Step 3
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

            // Repeat Step 4
            DmiActions.ShowInstruction(this, @"Press the ‘Hide PA’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The Planning Area is hidden for 60s and then re-displayed in area D." +
                                Environment.NewLine +
                                "3. The ‘Hide PA’ button is displayed in sub-area D14.");

            // Repeat Step 5
            DmiActions.ShowInstruction(this, @"Power down the system");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI is blank.");

            // Repeat Step 6
            DmiActions.ShowInstruction(this, @"Wait 10s and power up the system");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The Planning Area is re-displayed in area D." + Environment.NewLine +
                                "2. The ‘Hide PA’ button is re-displayed in area D14.");

            MakeTestStepHeader(11, UniqueIdentifier++,
                "Set HIDE PA TIMER configuration as 70s and repeat test step 1-6",
                "The Planning area is disappeared and hidden from main area D for 60s.After 60s the planning area is displayed.Verify that the Hide PA button is displayed at sub-area D14 on the planning area");
            /*
            Test Step 11
            Action: Set HIDE PA TIMER configuration as 70s and repeat test step 1-6
            Expected Result: The Planning area is disappeared and hidden from main area D for 60s.After 60s the planning area is displayed.Verify that the Hide PA button is displayed at sub-area D14 on the planning area
            */
            DmiActions.ShowInstruction(this,
                @"Power down the system and wait 10s. Set the configuration HIDE_PA_TIMER = 70 and power up the system");

            // Repeat Step 1
            DmiActions.Start_ATP();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Default window.");

            // Repeat Step 2
            DmiActions.Complete_SoM_L1_SR(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in SR mode, Level 1.");

            // Repeat Step 3
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

            // Repeat Step 4
            DmiActions.ShowInstruction(this, @"Press the ‘Hide PA’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The Planning Area is hidden for 70s and then re-displayed in area D." +
                                Environment.NewLine +
                                "3. The ‘Hide PA’ button is displayed in sub-area D14.");

            // Repeat Step 5
            DmiActions.ShowInstruction(this, @"Power down the system");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI is blank.");

            // Repeat Step 6
            DmiActions.ShowInstruction(this, @"Wait 10s and power up the system");
            DmiActions.Complete_SoM_L1_FS(this);
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 40;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN = 10000;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The Planning Area is re-displayed in area D." + Environment.NewLine +
                                "2. The ‘Hide PA’ button is re-displayed in area D14.");

            MakeTestStepHeader(12, UniqueIdentifier++,
                "Set HIDE PA TIMER configuration as 80s and repeat test step 1-6",
                "The Planning area is disappeared and hidden from main area D for 60s.After 60s the planning area is displayed.Verify that the Hide PA button is displayed at sub-area D14 on the planning area");
            /*
            Test Step 12
            Action: Set HIDE PA TIMER configuration as 80s and repeat test step 1-6
            Expected Result: The Planning area is disappeared and hidden from main area D for 60s.After 60s the planning area is displayed.Verify that the Hide PA button is displayed at sub-area D14 on the planning area
            */
            DmiActions.ShowInstruction(this,
                @"Power down the system and wait 10s. Set the configuration HIDE_PA_TIMER = 80 and power up the system");

            // Repeat Step 1
            DmiActions.Start_ATP();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Default window.");

            // Repeat Step 2
            DmiActions.Complete_SoM_L1_SR(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in SR mode, Level 1.");

            // Repeat Step 3
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

            // Repeat Step 4
            DmiActions.ShowInstruction(this, @"Press the ‘Hide PA’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The Planning Area is hidden for 80s and then re-displayed in area D." +
                                Environment.NewLine +
                                "3. The ‘Hide PA’ button is displayed in sub-area D14.");

            // Repeat Step 5
            DmiActions.ShowInstruction(this, @"Power down the system");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI is blank.");

            // Repeat Step 6
            DmiActions.ShowInstruction(this, @"Wait 10s and power up the system");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The Planning Area is re-displayed in area D." + Environment.NewLine +
                                "2. The ‘Hide PA’ button is re-displayed in area D14.");

            MakeTestStepHeader(13, UniqueIdentifier++, "End of test", "");

            /*
            Test Step 13
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
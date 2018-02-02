using System;
using Testcase.Telegrams.EVCtoDMI;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 22.9.5 Hide PA Function is configured ‘ON’ with reactivated Cabin A
    /// TC-ID: 17.9.6
    /// 
    /// This test case verifies that the default state of Planning area is shown/hidden refer to ‘ON’ configuration of Hide PA function included with correctness of displayed Planning area when activate ‘Hide’/’Show’ buttons after cabin re-activation.
    /// 
    /// Tested Requirements:
    /// MMI_gen 7340; MMI_gen 2996 (partly: ON); MMI_gen 7346;
    /// 
    /// Scenario:
    /// Activate Cabin A.Perform SoM in SR mode, Level 1.Drive the train forward pass BG1 at position 100m.BG1: Packet 12, 21 and 27 (mode changes to FS)Stop the train at position 300m.De-activate cabin A and activate cabin A again.Drive the train forward pass BG2 at position 600m. Then, verify that PA is hidden by default.BG2: packet 12, 21 and 27 (mode changes to FS)
    /// 
    /// Used files:
    /// 17_9_6.tdg
    /// </summary>
    public class TC_ID_17_9_6_Hide_PA_Function_is_configured_ON_with_reactivated_Cabin_A : TestcaseBase
    {

        public override bool TestcaseEntryPoint()
        {
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 0;
            // Testcase entrypoint
            TraceInfo("This test case requires an ATP configuration change - " +
                      "See Precondition requirements. If this is not done manually, the test may fail!");

            TraceHeader("Test Step 1");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Power On the system");
            TraceReport("Expected Result");
            TraceInfo("The DMI displays the default window");
            /*
            Test Step 1
            Action: Power On the system
            Expected Result: The DMI displays the default window
            */
            DmiActions.Start_ATP();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Default window.");

            TraceHeader("Test Step 2");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Activate cabin A and Perform SoM to SR mode, Level 1");
            TraceReport("Expected Result");
            TraceInfo("The DMI displays in SR mode, level 1");
            /*
            Test Step 2
            Action: Activate cabin A and Perform SoM to SR mode, Level 1
            Expected Result: The DMI displays in SR mode, level 1
            */
            // Tested elsewhere, force SoM
            DmiActions.Activate_Cabin_1(this);
            DmiActions.Set_Driver_ID(this, "1234");
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Level = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_LEVEL.L1;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode =
                EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.StaffResponsible;
            DmiActions.Finished_SoM_Default_Window(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in SR mode, Level 1.");

            TraceHeader("Test Step 3");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Drive the train forward with speed = 40 km/h pass BG1");
            TraceReport("Expected Result");
            TraceInfo(
                "The DMI shows “Entering FS” message.The DMI displays the Planning area.The Hide PA button is appeared on  the main area D of the DMI");
            /*
            Test Step 3
            Action: Drive the train forward with speed = 40 km/h pass BG1
            Expected Result: The DMI shows “Entering FS” message.The DMI displays the Planning area.The Hide PA button is appeared on  the main area D of the DMI
            Test Step Comment: (1) MMI_gen 2996 (partly: ON);
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

            TraceHeader("Test Step 4");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Press Hide PA button");
            TraceReport("Expected Result");
            TraceInfo("The Planning area is disappeared from the main area D of the DMI");
            /*
            Test Step 4
            Action: Press Hide PA button
            Expected Result: The Planning area is disappeared from the main area D of the DMI
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Hide PA’ button");


            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The Planning Area is removed from area D.");

            TraceHeader("Test Step 5");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Stop the train. Then, deactivate cabin A");
            TraceReport("Expected Result");
            TraceInfo("The train is at standstill.DMI is displays in SB mode");
            /*
            Test Step 5
            Action: Stop the train. Then, deactivate cabin A
            Expected Result: The train is at standstill.DMI is displays in SB mode
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 0;
            DmiActions.Deactivate_Cabin(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in SB mode.");

            TraceHeader("Test Step 6");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Activate cabin A and Perform SoM to SR mode, Level 1");
            TraceReport("Expected Result");
            TraceInfo(
                "The DMI displays in SR mode,  level 1.Verify that the planning area is not displayed, the Hide PA function is inoperable. No visible buttons of Hide PA function are displayed on DMI");
            /*
            Test Step 6
            Action: Activate cabin A and Perform SoM to SR mode, Level 1
            Expected Result: The DMI displays in SR mode,  level 1.Verify that the planning area is not displayed, the Hide PA function is inoperable. No visible buttons of Hide PA function are displayed on DMI
            Test Step Comment: MMI_gen 7346;
            */
            DmiActions.Activate_Cabin_1(this);
            DmiActions.Set_Driver_ID(this, "1234");
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Level = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_LEVEL.L1;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode =
                EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.StaffResponsible;
            DmiActions.Finished_SoM_Default_Window(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in SR mode, Level 1." + Environment.NewLine +
                                "2. The Planning area is not displayed." + Environment.NewLine +
                                "3. No ‘Hide PA’ or ‘Show PA’ buttons are displayed.");

            TraceHeader("Test Step 7");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Drive the train forward with speed = 40 km/h pass BG2");
            TraceReport("Expected Result");
            TraceInfo(
                "The DMI shows “Entering FS” message.The DMI displays the Planning area. The Hide PA button is appeared on  the main area D of the DMI");
            /*
            Test Step 7
            Action: Drive the train forward with speed = 40 km/h pass BG2
            Expected Result: The DMI shows “Entering FS” message.The DMI displays the Planning area. The Hide PA button is appeared on  the main area D of the DMI
            Test Step Comment: (1) MMI_gen 2996 (partly: ON); MMI_gen 7340;
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 40;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.FullSupervision;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN = 600000;

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
            TraceHeader("Test Step 8");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Press Hide PA button");
            TraceReport("Expected Result");
            TraceInfo("The Planning area is disappeared from the main area D of the DMI");
            /*
            Test Step 8
            Action: Press Hide PA button
            Expected Result: The Planning area is disappeared from the main area D of the DMI
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press the ‘Hide PA’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The Planning area is not displayed.");

            TraceHeader("Test Step 9");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("End of test");
            
            /*
            Test Step 9
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
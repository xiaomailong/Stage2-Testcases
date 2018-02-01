using System;
using System.Collections.Generic;
using Testcase.Telegrams.EVCtoDMI;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 18.1.7.3 Distance to Target: Appearance of Distance to Target in SB, SR and SH mode
    /// TC-ID: 13.1.7.3
    /// 
    /// This test case verifies the display information of the distance to target bar and digital in SB, SR and SH mode. The display of distance to target is comply with the received packet EVC-1 and EVC-7. 
    /// 
    /// Tested Requirements:
    /// MMI_gen 2567 (partly: SB mode, SR mode, SH mode); MMI_gen 107 (partly: Table 37, SB mode, SR mode, SH mode); MMI_gen 6658; MMI_gen 6774;
    /// 
    /// Scenario:
    /// 1.Enter SB mode, Then, verify the display of distance to target.
    /// 2.Enter SR mode and set the SR speed and SR distance with specify value and press the toggle of basic speed hook to become visible. Then, verify the display of distance to target bar and digital.
    /// 3.Drive the train forward. Then, verify the display of distance to target bar and digital when the supervision status is changed.  
    /// 4.Enter SH mode and verify the display of distance to target bar and digital.Note: The consistency of information for the position in each test step and the location when the value of MMI_M_WARNING changed is able to verify in log file, EVC-7 variable OBU_TR_O_TRAIN.
    /// 
    /// Used files:
    /// N/A
    /// </summary>
    public class TC_13_1_7_3_Brake : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:

            // Call the TestCaseBase PreExecution
            base.PreExecution();
            // System is powered on.Cabin is activated.
            DmiActions.Start_ATP();

            // Set train running number, cab 1 active, and other defaults
            DmiActions.Activate_Cabin_1(this);
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in SH mode, Level 1

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 0;
            // Testcase entrypoint

            TraceHeader("Test Step 1");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Perform the following procedure,Enter Driver ID and skip break testSelect and confirm ‘Level 1’");
            TraceReport("Expected Result");
            TraceInfo(
                "DMI displays in SB mode, level 1.Verify the following information(1)    Use the log file to confirm that DMI receives the following packets information with a specific value,  EVC-7: OBU_TR_M_MODE = 6 (SB mode) (2)   The distance to target bar is not display in sub-area A3. (3)   The distance to target digital is not display in sub-area A2.(4)   Use the log file to confirm that DMI receives the packet EVC-1 with variable MMI_O_BRAKETARGET = -1 (Default)");
            /*
            Test Step 1
            Action: Perform the following procedure,Enter Driver ID and skip break testSelect and confirm ‘Level 1’
            Expected Result: DMI displays in SB mode, level 1.Verify the following information(1)    Use the log file to confirm that DMI receives the following packets information with a specific value,  EVC-7: OBU_TR_M_MODE = 6 (SB mode) (2)   The distance to target bar is not display in sub-area A3. (3)   The distance to target digital is not display in sub-area A2.(4)   Use the log file to confirm that DMI receives the packet EVC-1 with variable MMI_O_BRAKETARGET = -1 (Default)
            Test Step Comment: (1) MMI_gen 107 (partly: MMI_M_WARNING, OBU_TR_M_MODE, SB mode); MMI_gen 2567 (partly: MMI_M_WARNING, OBU_TR_M_MODE, SB mode);(2) MMI_gen 6658 (partly: not be shown); MMI_gen 107 (partly: Table 37, SB mode);(3) MMI_gen 2567 (partly: Table 38, SB mode); MMI_gen 6774 (partly: not be shown);(4) MMI_gen 6658 (partly: MMI_O_BRAKETARGET is less than zero); MMI_gen 6774 (partly: MMI_O_BRAKETARGET is less than zero);
            */
            EVC14_MMICurrentDriverID.MMI_Q_CLOSE_ENABLE = Variables.MMI_Q_CLOSE_ENABLE.Disabled;
            EVC14_MMICurrentDriverID.MMI_X_DRIVER_ID = "1234";
            EVC14_MMICurrentDriverID.Send();

            DmiActions.ShowInstruction(this, "Enter Driver ID. Skip brake test");

            EVC20_MMISelectLevel.MMI_Q_CLOSE_ENABLE = Variables.MMI_Q_CLOSE_ENABLE.Disabled;
            EVC20_MMISelectLevel.MMI_Q_LEVEL_NTC_ID = new Variables.MMI_Q_LEVEL_NTC_ID[]
                {Variables.MMI_Q_LEVEL_NTC_ID.ETCS_Level};
            EVC20_MMISelectLevel.MMI_M_CURRENT_LEVEL = new Variables.MMI_M_CURRENT_LEVEL[]
                {Variables.MMI_M_CURRENT_LEVEL.NotLastUsedLevel};
            EVC20_MMISelectLevel.MMI_M_LEVEL_FLAG = new Variables.MMI_M_LEVEL_FLAG[]
                {Variables.MMI_M_LEVEL_FLAG.MarkedLevel};
            EVC20_MMISelectLevel.MMI_M_INHIBITED_LEVEL = new Variables.MMI_M_INHIBITED_LEVEL[]
                {Variables.MMI_M_INHIBITED_LEVEL.NotInhibited};
            EVC20_MMISelectLevel.MMI_M_INHIBIT_ENABLE = new Variables.MMI_M_INHIBIT_ENABLE[]
                {Variables.MMI_M_INHIBIT_ENABLE.AllowedForInhibiting};
            EVC20_MMISelectLevel.MMI_M_LEVEL_NTC_ID = new Variables.MMI_M_LEVEL_NTC_ID[]
                {Variables.MMI_M_LEVEL_NTC_ID.L1};
            EVC20_MMISelectLevel.Send();

            DmiActions.ShowInstruction(this, "Select and confirm ‘Level 1’");

            EVC1_MMIDynamic.MMI_O_BRAKETARGET = -1;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.StandBy;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in SB mode, level 1." + Environment.NewLine +
                                "2. The distance to target bar is not displayed in sub-area A3." + Environment.NewLine +
                                "3. The digital distance to target is not displayed in sub-area A2.");

            TraceHeader("Test Step 2");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Perform the following procedure,Perform SoM in SR mode, level1.Press ‘Special’ button.Press ‘SR speed/distance’ button.Enter the value of SR speed = 40 km/h and SR distance = 500mPress ‘Yes’ button.Press on sub-area B to toggle the basic speed hook become visible");
            TraceReport("Expected Result");
            TraceInfo(
                "DMI displays in SR mode, level 1Verify the following information(1)    Use the log file to confirm that DMI receives the following packets information with a specific value,  EVC-1: MMI_M_WARNING = 0 (Status = NoS, Supervision = CSM) EVC-7: OBU_TR_M_MODE = 2 (SR mode) (2)   The distance to target bar is not display in sub-area A3. (3)   The distance to target digital is not display in sub-area A2.(4)   Use the log file to confirm that DMI receives the packet EVC-1 with variable MMI_O_BRAKETARGET = -1 (Default)");
            /*
            Test Step 2
            Action: Perform the following procedure,Perform SoM in SR mode, level1.Press ‘Special’ button.Press ‘SR speed/distance’ button.Enter the value of SR speed = 40 km/h and SR distance = 500mPress ‘Yes’ button.Press on sub-area B to toggle the basic speed hook become visible
            Expected Result: DMI displays in SR mode, level 1Verify the following information(1)    Use the log file to confirm that DMI receives the following packets information with a specific value,  EVC-1: MMI_M_WARNING = 0 (Status = NoS, Supervision = CSM) EVC-7: OBU_TR_M_MODE = 2 (SR mode) (2)   The distance to target bar is not display in sub-area A3. (3)   The distance to target digital is not display in sub-area A2.(4)   Use the log file to confirm that DMI receives the packet EVC-1 with variable MMI_O_BRAKETARGET = -1 (Default)
            Test Step Comment: (1) MMI_gen 107 (partly: MMI_M_WARNING, OBU_TR_M_MODE, SR mode CSM); MMI_gen 2567 (partly: MMI_M_WARNING, OBU_TR_M_MODE, SR mode CSM);(2) MMI_gen 6658 (partly: not be shown); MMI_gen 107 (partly: Table 37, SR mode);(3) MMI_gen 2567 (partly: Table 38, SR mode CSM); MMI_gen 6774 (partly: not be shown);(4) MMI_gen 6658 (partly: MMI_O_BRAKETARGET is less than zero); MMI_gen 6774 (partly: MMI_O_BRAKETARGET is less than zero);
            */
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN = 0; // 0m

            // SoM is tested elsewhere: set SR mode
            /*
            DmiActions.ShowInstruction(this, "Perform SoM in SR mode, level 1. Press ‘Special’ button. Press ‘SR speed / distance’ button. Enter the value of SR speed = 40 km/h " + Environment.NewLine +
                                             "and SR distance = 500m. Press ‘Yes’ button. Press on sub-area B to toggle the basic speed hook so that is is displayed");
            */
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode =
                EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.StaffResponsible;

            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Close_current_return_to_parent;
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.SRSpeedDistance;

            EVC30_MMIRequestEnable.Send();

            DmiActions.ShowInstruction(this, "Press the ‘Special’ button, then press the ‘SR speed / distance’ button");

            EVC11_MMICurrentSRRules.MMI_M_BUTTONS = Variables.MMI_M_BUTTONS.BTN_YES_DATA_ENTRY_COMPLETE;
            EVC11_MMICurrentSRRules.Send();

            DmiActions.ShowInstruction(this,
                "Enter the value of SR speed = 40 km/h and SR distance = 500m. Press the ‘Yes’ button");

            EVC11_MMICurrentSRRules.DataElements = new List<Variables.DataElement>
            {
                new Variables.DataElement {Identifier = 15, EchoText = "40", QDataCheck = 0},
                new Variables.DataElement {Identifier = 16, EchoText = "500", QDataCheck = 0}
            };
            EVC11_MMICurrentSRRules.Send();

            DmiActions.ShowInstruction(this,
                "Press on sub-area B to toggle the basic speed hook so that is is displayed");

            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Normal_Status_Ceiling_Speed_Monitoring;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode =
                EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.StaffResponsible;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in SR mode, level 1." + Environment.NewLine +
                                "2. The distance to target bar is not displayed in sub-area A3." + Environment.NewLine +
                                "3. The digital distance to target is not displayed in sub-area A2.");

            TraceHeader("Test Step 3");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Continue to drive the train forward.Then, stop the train");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following information,(1)    Use the log file to confirm that DMI receives the packet information EVC-1 with following variables,MMI_M_WARNING = 2 (Status = NoS, Supervision = PIM)(2)    The distance to target bar is not display in sub-area A3.(3)   The distance to target digital is display in sub-area A2");
            /*
            Test Step 3
            Action: Continue to drive the train forward.Then, stop the train
            Expected Result: Verify the following information,(1)    Use the log file to confirm that DMI receives the packet information EVC-1 with following variables,MMI_M_WARNING = 2 (Status = NoS, Supervision = PIM)(2)    The distance to target bar is not display in sub-area A3.(3)   The distance to target digital is display in sub-area A2
            Test Step Comment: (1) MMI_gen 107 (partly: MMI_M_WARNING, OBU_TR_M_MODE, SR mode); MMI_gen 2567 (partly: MMI_M_WARNING, OBU_TR_M_MODE, SR mode PIM);(2) MMI_gen 107 (partly: Table 37, SR mode);(3) MMI_gen 2567 (partly: Table 38, SR mode PIM);
            */
            EVC1_MMIDynamic.MMI_V_TARGET_KMH = 10; // drive on
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN = 2500; // 25m
            EVC1_MMIDynamic.MMI_V_TARGET_KMH = 0; // stop
            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Normal_Status_PreIndication_Monitoring;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The distance to target bar is not displayed in sub-area A3." + Environment.NewLine +
                                "2. The digital distance to target is not displayed in sub-area A2.");

            TraceHeader("Test Step 4");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Continue to drive the train forward.Then, stop the train");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following information,(1)    Use the log file to confirm that DMI receives the packet information EVC-1 with following variables,MMI_M_WARNING = 11 (Status = NoS, Supervision = TSM)(2)    The distance to target bar is not display in sub-area A3.(3)    The distance to target digital is display in sub-area A2");
            /*
            Test Step 4
            Action: Continue to drive the train forward.Then, stop the train
            Expected Result: Verify the following information,(1)    Use the log file to confirm that DMI receives the packet information EVC-1 with following variables,MMI_M_WARNING = 11 (Status = NoS, Supervision = TSM)(2)    The distance to target bar is not display in sub-area A3.(3)    The distance to target digital is display in sub-area A2
            Test Step Comment: (1) MMI_gen 107 (partly: MMI_M_WARNING, OBU_TR_M_MODE, SR mode); MMI_gen 2567 (partly: MMI_M_WARNING, OBU_TR_M_MODE, SR mode TSM);(2) MMI_gen 107 (partly: Table 37, SR mode);(3) MMI_gen 2567 (partly: Table 38, SR mode TSM);
            */
            EVC1_MMIDynamic.MMI_V_TARGET_KMH = 10; // drive on
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN = 10500; // 105m
            EVC1_MMIDynamic.MMI_V_TARGET_KMH = 0; // stop
            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Normal_Status_Target_Speed_Monitoring;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The distance to target bar is not displayed in sub-area A3." + Environment.NewLine +
                                "2. The digital distance to target is not displayed in sub-area A2.");

            TraceHeader("Test Step 5");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Perform the following procedure,Press ‘Main’ button.Press and hold ‘Shunting’ button at least 2 second.Release the pressed button");
            TraceReport("Expected Result");
            TraceInfo(
                "DMI displays in SH mode, level 1.Verify the following information,(1)    Use the log file to confirm that DMI receives the following packets information with a specific value, EVC-7: OBU_TR_M_MODE = 3 (SH mode) (2)    The distance to target bar is not display in sub-area A3.(3)    The distance to target digital is not display in sub-area A2.(4)   Use the log file to confirm that DMI receives the packet EVC-1 with variable MMI_O_BRAKETARGET = -1 (Default)");
            /*
            Test Step 5
            Action: Perform the following procedure,Press ‘Main’ button.Press and hold ‘Shunting’ button at least 2 second.Release the pressed button
            Expected Result: DMI displays in SH mode, level 1.Verify the following information,(1)    Use the log file to confirm that DMI receives the following packets information with a specific value, EVC-7: OBU_TR_M_MODE = 3 (SH mode) (2)    The distance to target bar is not display in sub-area A3.(3)    The distance to target digital is not display in sub-area A2.(4)   Use the log file to confirm that DMI receives the packet EVC-1 with variable MMI_O_BRAKETARGET = -1 (Default)
            Test Step Comment: (1) MMI_gen 107 (partly: MMI_M_WARNING, OBU_TR_M_MODE, SH mode); MMI_gen 2567 (partly: MMI_M_WARNING, OBU_TR_M_MODE, SH mode);(2) MMI_gen 6658 (partly: not be shown); MMI_gen 107 (partly: Table 37, SH mode);(3) MMI_gen 2567 (partly: Table 38, SH mode); MMI_gen 6774 (partly: not be shown);(4) MMI_gen 6658 (partly: MMI_O_BRAKETARGET is less than zero); MMI_gen 6774 (partly: MMI_O_BRAKETARGET is less than zero);
            */
            // Need to close the special window
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Close_current_return_to_parent;
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.Shunting;
            EVC30_MMIRequestEnable.Send();

            DmiActions.ShowInstruction(this,
                "Press ‘Main’ button.	Press and hold ‘Shunting’ button for at least 2s. Release the ‘Shunting’ button.");

            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.Shunting;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Does the DMI delete the SR mode symbol (MO09) and replace it with the SH mode symbol (MO01) in area B7" +
                                Environment.NewLine +
                                "2. The distance to target bar is not displayed in sub-area A3." + Environment.NewLine +
                                "3. The digital distance to target is not displayed in sub-area A2.");

            TraceHeader("Test Step 6");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("End of test");
            TraceReport("Expected Result");
            TraceInfo("");
            /*
            Test Step 6
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
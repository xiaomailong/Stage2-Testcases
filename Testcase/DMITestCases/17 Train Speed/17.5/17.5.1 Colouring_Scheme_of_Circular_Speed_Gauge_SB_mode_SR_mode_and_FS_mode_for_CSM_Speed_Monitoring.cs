using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BT_Tools;
using BT_CSB_Tools;
using BT_CSB_Tools.Logging;
using BT_CSB_Tools.Utils.Xml;
using BT_CSB_Tools.SignalPoolGenerator.Signals;
using BT_CSB_Tools.SignalPoolGenerator.Signals.MwtSignal;
using BT_CSB_Tools.SignalPoolGenerator.Signals.MwtSignal.Misc;
using BT_CSB_Tools.SignalPoolGenerator.Signals.PdSignal;
using BT_CSB_Tools.SignalPoolGenerator.Signals.PdSignal.Misc;
using CL345;
using Testcase.Telegrams.EVCtoDMI;
using static Testcase.Telegrams.EVCtoDMI.Variables;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 17.5.1 Colouring Scheme of Circular Speed Gauge (SB mode, SR mode and FS mode for CSM Speed Monitoring)
    /// TC-ID: 12.5.1
    /// 
    /// This test case verifies the display of CSG according to received packet information EVC-1 and EVC-7 including with sound played when Warning Status is active.
    /// 
    /// Tested Requirements:
    /// MMI_gen 2327; MMI_gen 1155; MMI_gen 1154 (partly: Outer border of the Speed Dial, Placed at Permitted Speed); MMI_gen 6310 (partly: supervision status, mode, permitted speed, intervention speed); MMI_gen 972 (partly: OBU_TR_M_MODE, CSG not display in SB and SR mode, MMI_M_WARNING = CSM Supervision status, MMI_V_PERMITTED, MMI_V_INTERVENTION); MMI_gen 5774; MMI_gen 11921 (partly: MMI_M_WARNING = 4); MMI_gen 1182 (partly: speed dial, Vperm, Vsbi);
    /// 
    /// Scenario:
    /// 1.Perform SoM in SB mode, Level 
    /// 1.Then, verify that the CSG is not displayed on DMI refer to received packet information EVC-7.
    /// 2.Perform SoM in SR mode, Level 
    /// 1.Then, verify that the CSG is not displayed on DMI refer to received packet information EVC-7.
    /// 3.Drive the train forward pass BG1 at position 100m with specified speed. Then verify the display of CSG refer to received packet information EVC-7 and EVC-1.BG1: packet 12, 21 and 27
    /// 
    /// Used files:
    /// 12_5_1.tdg
    /// </summary>
    public class TC_12_5_1_Train_Speed : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // System is power on. Cabin is activated.Perform SoM until Level 1 is selected and confirmed.

            // Call the TestCaseBase PreExecution
            base.PreExecution();
            DmiActions.Complete_SoM_L1_SB(this);
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
            Action: Perform the following procedure,Press ‘Train data’ button.Enter and validate all train data.Enter the train running number
            Expected Result: DMI displays Main window in SB mode, Level 1.Verify the following information,(1)    Use the log file to confirm that DMI received packet EVC-7 with variable OBU_TR_M_MODE = 6 (Standby mode).(2)    The CSG is still not displayed on DMI
            Test Step Comment: (1) MMI_gen 972 (partly: OBU_TR_M_MODE, table 33, mode SB); MMI_gen 6310 (partly: mode);(2) MMI_gen 972 (partly: CSG shall not displayed, table 33, mode SB);
            */
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Main;
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.TrainData |
                                                               EVC30_MMIRequestEnable.EnabledRequests.TrainRunningNumber |
                                                               EVC30_MMIRequestEnable.EnabledRequests.Start;
            EVC30_MMIRequestEnable.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays Main window in SB mode, Level 1.");

            DmiActions.ShowInstruction(this, @"Press the ‘Train data’ button");
          
            MMI_M_DATA_ENABLE enableOption = MMI_M_DATA_ENABLE.Airtightness | MMI_M_DATA_ENABLE.AxleLoadCategory | MMI_M_DATA_ENABLE.BrakePercentage |
                                             MMI_M_DATA_ENABLE.LoadingGauge | MMI_M_DATA_ENABLE.MaxTrainSpeed | MMI_M_DATA_ENABLE.TrainCategory |
                                             MMI_M_DATA_ENABLE.TrainLength | MMI_M_DATA_ENABLE.TrainSetID;
            string[] trainSetCaptions = new string[3] {"FLU", "RLU", "Rescue" };

            DmiActions.Send_EVC6_MMICurrentTrainData(enableOption,100, 120, MMI_NID_KEY.TILT1, 120, MMI_NID_KEY.G1, 1, MMI_NID_KEY.CATA, 
                                                     EVC6_MMICurrentTrainData.MMI_M_BUTTONS_CURRENT_TRAIN_DATA.BTN_YES_DATA_ENTRY_COMPLETE,
                                                     1, 1, 
                                                     trainSetCaptions,
                                                     new DataElement[0]);
            
            DmiActions.ShowInstruction(this, "Enter and validate all train data");
            
            DmiActions.Send_EVC10_MMIEchoedTrainData(this, enableOption, 100, 120, MMI_NID_KEY.TILT1, 120, MMI_NID_KEY.G1, 1, MMI_NID_KEY.CATA, 
                                                           trainSetCaptions);
            
            DmiActions.ShowInstruction(this, @"Press the ‘Train running number’ button");
            
            EVC16_CurrentTrainNumber.Send();

            DmiActions.ShowInstruction(this, "Enter the train running number");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1, The CSG is still not displayed on DMI.");

            /*
            Test Step 2
            Action: Perform the following procedure,Press ‘Start’ button.Press and hold sub-area C1 up to 2 second.Release the pressed area
            Expected Result: DMI displays Default window in SR mode, Level 1.Verify the following information,(1)     Use the log file to confirm that DMI received packet EVC-7 with variable OBU_TR_M_MODE = 2 (Staff Responsible mode)(2)    The CSG is still not displayed on DMI
            Test Step Comment: (1) MMI_gen 972 (partly: OBU_TR_M_MODE, table 33, mode SR); MMI_gen 6310 (partly: mode);(2) MMI_gen 972 (partly: CSG shall not displayed, table 33, mode SR);
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Start’ button.");

            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.StaffResponsible;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays Default window in SR mode, Level 1.");

            DmiActions.ShowInstruction(this, "Press and hold sub-area C1 for up to 2s, then release the pressed area.");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "The CSG is still not displayed on DMI.");

            /*
            Test Step 3
            Action: Drive the train forward pass BG1 with speed = 30km/h
            Expected Result: DMI displays in FS mode, Level 1.Verify the following information,(1)    The CSG is displays in sub-area B2.(2)    Use the log file to confirm that DMI received packet EVC-7 with variable OBU_TR_M_MODE = 0 (Full Supervision mode).(3)   Use the log file to confirm that DMI received packet EVC-1 with variable MMI_V_PERMITTED = 4166 (150 km/h)(4)   All section of CSG (0km/h – 150 km/h) is dark-grey colour.(5)   At Permitted speed, The CSG is display a Hook covering the outer border of Speed Dial and The upper limit of Hook is placed at 150 km/h
            Test Step Comment: (1) MMI_gen 2327;(2) MMI_gen 972 (partly: OBU_TR_M_MODE, table 33, mode FS); MMI_gen 6310 (partly: mode);(3) MMI_gen 972 (partly: MMI_V_PERMITTED); MMI_gen 6310 (partly: permitted speed);(4) MMI_gen 972 (partly: FS mode, CSM, 0km/h <= CSG <= Vperm);(5) MMI_gen 1154 (partly: Outer border of the Speed Dial, Placed at Permitted Speed); MMI_gen 1182 (partly: speed dial, Vperm);
            */
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.FullSupervision;
            EVC1_MMIDynamic.MMI_V_PERMITTED = 4166;
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 30;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in FS mode, Level 1." + Environment.NewLine +
                                "2. The CSG is displayed in sub-area B2." + Environment.NewLine + 
                                "3. All sections of CSG(0 km/h – 150 km/h) are dark-grey in colour." + Environment.NewLine +
                                "4. The CSG displays a Hook covering the outer border of Speed Dial and the upper limit of the Hook is placed at 150 km/h.");

            /*
            Test Step 4
            Action: Continue to drive the train forward with speed = 151 km/h
            Expected Result: Verify the following information,(1)   Use the log file to confirm that DMI received packet EVC-1 with following variables, MMI_M_WARNING = 8 (Status=OvS, Supervision=CSM).MMI_V_INTERVENTION > 4166 (150 km/h)(2)   The CSG at 0-150 km/h is dark-grey colour.(3)   The CSG at beyond 150km/h is orange colour.(4)   The CSG between the hook (Vperm = 150 km/h) and Vsbi is have a same width with hook
            Test Step Comment: (1) MMI_gen 972 (partly: MMI_M_WARNING, MMI_V_INTERVEN); MMI_gen 6310 (partly: supervision status, intervention speed);(2) MMI_gen 972 (partly: FS mode, CSM, 0km/h <= CSG <= Vperm);(3) MMI_gen 972 (partly: FS mode, CSM, Vperm <= CSG <= Vsbi );(4) MMI_gen 1155 (partly: Over-speed); MMI_gen 1182 (partly: Vsbi);
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 151;
            EVC1_MMIDynamic.MMI_V_INTERVENTION_KMH = 155;
            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Overspeed_Status_Ceiling_Speed_Monitoring;
            // ?? Send
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The CSG between 0 and 150 km/h is dark-grey in colour." + Environment.NewLine +
                                "2. The CSG beyond 150km/h is orange in colour." + Environment.NewLine +
                                "3. The CSG between the Hook (Vperm = 150 km/h) and Vsbi has a hook of the same width.");

            /*
            Test Step 5
            Action: Continue to drive the train forward with speed = 155 km/h.Note: dV_warning_max is defined in chapter 3 of [SUBSET-026]
            Expected Result: Verify the following information,(1)   Use the log file to confirm that DMI received packet EVC-1 with following variables, MMI_M_WARNING = 4 (Status=WaS, Supervision=CSM).(2)   The CSG at 0-150 km/h is dark-grey colour.(3)   The CSG at beyond 150km/h is orange colour.(4)   The CSG between the hook (Vperm = 150 km/h) and Vsbi is have a same width with hook.(5)   Sound S2 is played continuously while the Warning Status is active
            Test Step Comment: (1) MMI_gen 972 (partly: MMI_M_WARNING, MMI_V_INTERVEN); MMI_gen 6310 (partly: supervision status, intervention speed);(2) MMI_gen 972 (partly: FS mode, CSM, 0km/h <= CSG <= Vperm);(3) MMI_gen 972 (partly: FS mode, CSM, Vperm <= CSG <= Vsbi );(4) MMI_gen 1155 (partly: Warning);(5)   MMI_gen 5774; MMI_gen 11921 (partly: MMI_M_WARNING = 4);
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 155;

            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Warning_Status_Ceiling_Speed_Monitoring;
            // ?? Send

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The CSG at 0-150 km/h is dark-grey in colour." + Environment.NewLine +
                                "2. The CSG beyond 150 km/h is orange in colour." + Environment.NewLine +
                                "3. The CSG between the hook (Vperm = 150 km/h) and Vsbi has a hook of the same width." + Environment.NewLine +
                                "4. Sound S2 is played continuously while the Warning Status is active.");

            /*
            Test Step 6
            Action: Drive the train forward with speed greater than MMI_V_INTERVENTION from step 4
            Expected Result: Verify the following information,(1)   Use the log file to confirm that DMI received packet EVC-1 with following variables, MMI_M_WARNING = 12 (Status=IntS, Supervision=CSM).(2)   The CSG at 0-150 km/h is dark-grey colour.(3)   The CSG at beyond 150km/h is red colour.(4)   The CSG between the hook (Vperm = 150 km/h) and Vsbi is have a same width with hook.(5) Sound S2 is muted because of Warning Stauts is deactive
            Test Step Comment: (1) MMI_gen 972 (partly: MMI_M_WARNING, MMI_V_INTERVEN); MMI_gen 6310 (partly: supervision status);(2) MMI_gen 972 (partly: FS mode, CSM, 0km/h <= CSG <= Vperm);(3) MMI_gen 972 (partly: FS mode, CSM, Vperm <= CSG <= Vsbi );(4) MMI_gen 1155 (partly: Intervention);(5) MMI_gen 11921 (partly: NEGATIVE, MMI_M_WARNING = 4);
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 157;
            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Intervention_Status_Ceiling_Speed_Monitoring;
            // ?? Send
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                               "1. The CSG at 0-150 km/h is dark-grey in colour." + Environment.NewLine +
                               "2. The CSG beyond 150 km/h is red in colour." + Environment.NewLine +
                               "3. The CSG between the hook (Vperm = 150 km/h) and Vsbi has a hook of the same width." + Environment.NewLine +
                               "4. Sound S2 is muted because Warning Status is deactivated.");

            /*
            Test Step 7
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
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
using Testcase.Telegrams.DMItoEVC;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 10.3 Screen Layout: Frames
    /// TC-ID: 5.3
    /// 
    /// This test case verifies the appearance of all ETCS objects on DMI that related to the control information from ETCS Onboard i.e. display of mode symbols, supervision status or the release speed. 
    /// 
    /// Tested Requirements:
    /// MMI_gen 4222 (partly: frame is displayed with yellow); MMI_gen 6468 (partly: UN mode, supervision status and the release speed are not displayed); MMI_gen 11470 (partly: Bit #2);   
    /// 
    /// Scenario:
    /// Activate cabin A and enter the Driver ID.
    /// 1.Driver selects and confirms level 
    /// 0.DMI displays in UN mode.
    /// 2.Start driving the train forward with train speed = 15 km/h. Then, stop the train at position 100m.
    /// 3.Select level and confirm level 
    /// 14.Driver acknowledges the train trip, after that the driver presses the start button.
    /// 5.DMI changes to SR mode.
    /// 
    /// Used files:
    /// N/A
    /// </summary>
    public class TC_ID_5_3_Screen_Layout_Frames : TestcaseBase
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
            // DMI displays in SR mode, level 1
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Default window in SR mode, Level 1.");

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint

            /*
            Test Step 1
            Action: Activate cabin A
            Expected Result: DMI displays the default window. The Driver ID window is displayed
            */
            // Call generic Action Method
            DmiActions.Start_ATP();
            DmiActions.Activate_Cabin_1(this);
            DmiActions.Finished_SoM_Default_Window(pool);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Default window.");

            DmiActions.Set_Driver_ID(this, "1234");

            // Call generic Check Results Method
            DmiExpectedResults.Driver_ID_window_displayed(this);

            /*
            Test Step 2
            Action: Enter the Driver ID. Perform brake test and then select Level 0
            Expected Result: ATP enters level 0.DMI displays the symbol of Level 0 in sub-area C8
            */
            DmiActions.ShowInstruction(this, "Confirm the Driver ID");
            
            EVC20_MMISelectLevel.MMI_Q_CLOSE_ENABLE = Variables.MMI_Q_CLOSE_ENABLE.Disabled;

            EVC20_MMISelectLevel.MMI_Q_LEVEL_NTC_ID = new Variables.MMI_Q_LEVEL_NTC_ID[] { Variables.MMI_Q_LEVEL_NTC_ID.ETCS_Level };
            EVC20_MMISelectLevel.MMI_M_CURRENT_LEVEL = new Variables.MMI_M_CURRENT_LEVEL[] { Variables.MMI_M_CURRENT_LEVEL.NotLastUsedLevel };
            EVC20_MMISelectLevel.MMI_M_LEVEL_FLAG = new Variables.MMI_M_LEVEL_FLAG[] { Variables.MMI_M_LEVEL_FLAG.MarkedLevel };
            EVC20_MMISelectLevel.MMI_M_INHIBITED_LEVEL = new Variables.MMI_M_INHIBITED_LEVEL[] { Variables.MMI_M_INHIBITED_LEVEL.NotInhibited };
            EVC20_MMISelectLevel.MMI_M_INHIBIT_ENABLE = new Variables.MMI_M_INHIBIT_ENABLE[] { Variables.MMI_M_INHIBIT_ENABLE.AllowedForInhibiting };
            EVC20_MMISelectLevel.MMI_M_LEVEL_NTC_ID = new Variables.MMI_M_LEVEL_NTC_ID[] { Variables.MMI_M_LEVEL_NTC_ID.L0 };
            EVC20_MMISelectLevel.Send();

            DmiActions.ShowInstruction(this, "Select Level 0");

            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Level = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_LEVEL.L0;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. ATP enters level 0." + Environment.NewLine + 
                                "2. DMI displays the Level 0 symbol in sub-area C8.");

            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = 1;
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.TrainData |
                                                               EVC30_MMIRequestEnable.EnabledRequests.Start;
            EVC30_MMIRequestEnable.Send();

            /*
            Test Step 3
            Action: Select ‘Train data’ button
            Expected Result: The Train data window is displayed
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Train data’ button");

            DmiActions.Send_EVC6_MMICurrentTrainData_FixedDataEntry(this, new[] { "FLU", "RLU", "Rescue" }, 2);

            // Call generic Check Results Method
            DmiExpectedResults.Train_data_window_displayed(this);

            /*
            Test Step 4
            Action: Enter and confirm the train data
            Expected Result: The Train data validation window is displayed
            */
            DmiActions.ShowInstruction(this, "Enter and accept the Train data");

            DmiActions.Send_EVC10_MMIEchoedTrainData_FixedDataEntry(this, Variables.paramEvc6FixedTrainsetCaptions);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Train data validation window.");

            /*
            Test Step 5
            Action: Driver validates the train data
            Expected Result: DMI displays the Train running window
            */
            DmiActions.ShowInstruction(this, "Accept validation of the Train data");
            
            EVC16_CurrentTrainNumber.TrainRunningNumber = 1;
            EVC16_CurrentTrainNumber.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Train running number window.");

            /*
            Test Step 6
            Action: Enter and confirm the Train running number
            Expected Result: DMI displays the Main window
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, "Enter and accept the Train running number");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Main window.");

            /*
            Test Step 7
            Action: Press ‘Start’ button and confirm UN mode
            Expected Result: DMI displays in UN mode, level 0
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Start’ button");

            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 264;
            EVC8_MMIDriverMessage.Send();

            DmiActions.ShowInstruction(this, "Confirm UN mode");

            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.Unfitted;
            
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Is the Unfitted mode symbol (MO16) displayed in area B7?");

            /*
            Test Step 8
            Action: Drive the train forward and observe all objects on DMI’s screen
            Expected Result: Verify that when DMI displays in UN mode, the supervision status is not presented to the driver and there is no release speed on DMI
            Test Step Comment: MMI_gen 6468 (partly: UN mode, supervision status and the release speed are not displayed);   
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 5;

            WaitForVerification("Observe all the objects on the DMI screen and check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Supervision status is not presented to the driver." + Environment.NewLine +
                                "2. DMI does not display a release speed");

            /*
            Test Step 9
            Action: Stop at position 100m. Then, select level 1
            Expected Result: DMI displays the symbol of level 1 in sub-area C8 instead of level 0.DMI displays in level 1 with train trip announcement symbol which requires the driver’s action. The train trip symbol is displayed with yellow flashing frame
            Test Step Comment: MMI_gen 4222 (partly: frame is displayed with yellow flashing);
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 0;

            //DmiActions.ShowInstruction(this, @"Accept level 1");
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Level = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_LEVEL.L1;

            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 266;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI removes the level 0 symbol from sub-area C8 and displays the level 1 symbol." + Environment.NewLine +
                                "2. DMI displays the train trip announcement symbol requiring driver action." + Environment.NewLine +
                                "3. The train trip symbol is displayed with a yellow flashing frame.");

            /*
            Test Step 10
            Action: Driver acknowledges train trip
            Expected Result: DMI displays in PT mode.Use the log file to confirm that DMI sends out packet [MMI_DRIVER_ACTION (EVC-152)] with the value of variable MMI_M_DRIVER_ACTION refer to sequence below,a)   MMI_M_DRIVER_ACTION = 2 (Ack of Train Trip)
            Test Step Comment: MMI_gen 11470 (partly: Bit #2);   
            */

            DmiActions.ShowInstruction(this, @"Acknowledge train trip");

            EVC152_MMIDriverAction.Check_MMI_M_DRIVER_ACTION = EVC152_MMIDriverAction.MMI_M_DRIVER_ACTION.TrainTripAck;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Is the Post trip mode symbol (MO6) displayed in area B7?");

            /*
            Test Step 11
            Action: Press ‘Start’ button and confirm SR mode
            Expected Result: DMI displays in SR mode, level 1
            */
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.StaffResponsible;

            DmiActions.ShowInstruction(this, @"Press ‘Start’ button and confirm SR mode.");

            // Call generic Check Results Method
            DmiExpectedResults.SR_Mode_displayed(this);

            /*
            Test Step 12
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
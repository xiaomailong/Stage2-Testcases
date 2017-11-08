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


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 36.2 The relationship between parent and child windows (2)
    /// TC-ID: 33.3
    /// 
    /// This test case verifies the relationship between parent and child window when the driver presses ‘Close’ button in each window. The relationship between parent and child windows shall comply with [ERA-ERTMS] standard and [MMI-ETCS-gen].
    /// 
    /// Tested Requirements:
    /// MMI_gen 8785 (partly: Radio network ID window, RBC data window, RBC contact window, Data view window);
    /// 
    /// Scenario:
    /// Perform the following actions for each window in note below to verify the display of parent window. Open the specified window.Press ‘Close’ button to verify the display of parent window.Note: Perform the action for the following windows,Radio Network ID windowRBC data windowRBC contact windowData view window
    /// 
    /// Used files:
    /// 33_3.utt
    /// </summary>
    public class TC_ID_33_3_Parent_Child_Relationship : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // Call the TestCaseBase PreExecution
            base.PreExecution();


            // Test System is power on.Cabin is activated.Driver ID is entered and Brake test is performed.Level 2 is selected and confirmed.
            DmiActions.Start_ATP();

            // Set train running number, cab 1 active, and other defaults
            DmiActions.Activate_Cabin_1(this);

            // Set driver ID
            DmiActions.Set_Driver_ID(this, "1234");

            // Set to level 2(?) and SB mode
            //EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Level = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_LEVEL.L2;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.StandBy;
            DmiActions.Finished_SoM_Default_Window(this);
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays  in SB mode, Level 2

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint

            /*
            Test Step 1
            Action: Perform the following procedure,Press and hold ‘Radio Network ID’ button at least 2 second.Release the pressed area
            Expected Result: DMI displays Radio Network ID window
            */
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.EVC30WindowID.Main;      // display main window
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.RadioNetworkID |
                                                               EVC30_MMIRequestEnable.EnabledRequests.EnterRBCData |
                                                               EVC30_MMIRequestEnable.EnabledRequests.Level;
            EVC30_MMIRequestEnable.Send();

            DmiActions.ShowInstruction(this, @"Press and hold the ‘Radio Network ID’ button for at least 2 second, then release the ‘Radio Network ID’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                             "1. DMI displays the Radio Network ID window.");

            /*
            Test Step 2
            Action: Press ‘Close’ button
            Expected Result: DMI displays RBC contact window
            Test Step Comment: MMI_gen 8785 (partly: Radio Network ID window);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button in the Radio Network ID window");

            EVC22_MMICurrentRBC.MMI_NID_WINDOW = 5;
            EVC22_MMICurrentRBC.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                             "1. DMI displays the RBC Contact window.");

            /*
            Test Step 3
            Action: Press ‘Enter RBC data’ button
            Expected Result: DMI displays RBC data window
            */
            DmiActions.ShowInstruction(this, @"Press the ‘RBC data’ button");

            EVC22_MMICurrentRBC.MMI_NID_WINDOW = 10;
            EVC22_MMICurrentRBC.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                             "1. DMI displays the RBC data window.");

            /*
            Test Step 4
            Action: Press ‘Close’ button
            Expected Result: DMI displays RBC contact window
            Test Step Comment: MMI_gen 8785 (partly: RBC data window);
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button in the RBC data window");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                             "1. DMI displays the RBC Contact window.");

            /*
            Test Step 5
            Action: Perform the following procedure,Press ‘Enter RBC data’ button.Enter and confirm the following value,RBC ID = 6996969RBC Phone number = 0031840880100
            Expected Result: DMI displays Main window
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Enter RBC data’ button");

            EVC22_MMICurrentRBC.MMI_NID_WINDOW = 10;
            EVC22_MMICurrentRBC.Send();

            DmiActions.ShowInstruction(this, "Enter and confirm the following values: RBC ID = 6996969, RBC Phone number = 0031840880100");

            // Need to force RBC contact window to close
            EVC22_MMICurrentRBC.MMI_NID_WINDOW = 9;
            EVC22_MMICurrentRBC.DataElements = null;
            EVC22_MMICurrentRBC.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                             "1. DMI displays the Main window.");

            /*
            Test Step 6
            Action: Perform the following procedure,Press ‘Level’ button.Select and confirm Level 2
            Expected Result: DMI displays RBC contact window
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Level’ button");

            EVC20_MMISelectLevel.MMI_Q_CLOSE_ENABLE = Variables.MMI_Q_CLOSE_ENABLE.Disabled;
            EVC20_MMISelectLevel.MMI_Q_LEVEL_NTC_ID = new Variables.MMI_Q_LEVEL_NTC_ID[] { Variables.MMI_Q_LEVEL_NTC_ID.ETCS_Level };
            EVC20_MMISelectLevel.MMI_M_CURRENT_LEVEL = new Variables.MMI_M_CURRENT_LEVEL[] { Variables.MMI_M_CURRENT_LEVEL.NotLastUsedLevel };
            EVC20_MMISelectLevel.MMI_M_LEVEL_FLAG = new Variables.MMI_M_LEVEL_FLAG[] { Variables.MMI_M_LEVEL_FLAG.MarkedLevel };
            EVC20_MMISelectLevel.MMI_M_INHIBITED_LEVEL = new Variables.MMI_M_INHIBITED_LEVEL[] { Variables.MMI_M_INHIBITED_LEVEL.NotInhibited };
            EVC20_MMISelectLevel.MMI_M_INHIBIT_ENABLE = new Variables.MMI_M_INHIBIT_ENABLE[] { Variables.MMI_M_INHIBIT_ENABLE.AllowedForInhibiting };
            EVC20_MMISelectLevel.MMI_M_LEVEL_NTC_ID = new Variables.MMI_M_LEVEL_NTC_ID[] { Variables.MMI_M_LEVEL_NTC_ID.L2 };
            EVC20_MMISelectLevel.Send();

            DmiActions.ShowInstruction(this, "Select and confirm Level 2");

            EVC22_MMICurrentRBC.MMI_NID_WINDOW = 5;
            EVC22_MMICurrentRBC.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                             "1. DMI displays the RBC Contact window.");

            /*
            Test Step 7
            Action: Press ‘Close’ button
            Expected Result: DMI displays Main window
            Test Step Comment: MMI_gen 8785 (partly: RBC contact window);
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button in the RBC Contact window");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                             "1. DMI displays the Main window.");

            /*
            Test Step 8
            Action: Perform the following procedure,Press ‘Close’ button.Press ‘Data view’ button
            Expected Result: DMI displays Data view window
            */
            // Does this mean Train data?
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button in the Main window. Press the ‘Data view’ button");

            EVC13_MMIDataView.MMI_X_DRIVER_ID = "1234";
            EVC13_MMIDataView.MMI_NID_OPERATION = 1;
            EVC13_MMIDataView.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                             "1. DMI displays the Data view window.");

            /*
            Test Step 9
            Action: Press ‘Close’ button
            Expected Result: DMI displays Default window
            Test Step Comment: MMI_gen 8785 (partly: Data view window);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button in the Data view window");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                             "1. DMI displays the Default window.");

            /*
            Test Step 10
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
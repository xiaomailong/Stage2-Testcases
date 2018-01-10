using System;
using System.Collections.Generic;
using Testcase.Telegrams.DMItoEVC;
using Testcase.Telegrams.EVCtoDMI;
using static Testcase.Telegrams.EVCtoDMI.Variables;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 25.5 Driver’s Action: RBC Contact windows
    /// TC-ID: 20.5
    /// 
    /// This test case verify that DMI sends values of [MMI_DRIVER_REQUEST (EVC-101).MMI_M_REQUEST] correctly when a driver presses on each button in RBC Contact window.
    /// 
    /// Tested Requirements:
    /// MMI_gen 151 (partly: MMI_M_REQUEST = 56, 61, 28, 33, 39, 57);
    /// 
    /// Scenario:
    /// 1.Perform the specified action (e.g. open/close window, press an acknowledgement). Then, verify the value in packet EVC-101 refer to each action.
    /// 2.Open RBC Data window. Then, enter and confirm the value in each input field.
    /// 3.Open Level window to re-enter and confirm level 
    /// 2.Then, press the close button at RBC contact window and verify the value in packet EVC-101.
    /// 4.Re-open RBC contact window. Then, press the 'Contact last RBC' button. and verify the value in packet EVC-101.
    /// 
    /// Used files:
    /// 20_5.utt
    /// </summary>
    public class TC_ID_25_5_Drivers_Action_RBC_Contact_windows : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // Test system is powered on.Cabin is activated.SoM is performed until level 2 is selected and confirmed.

            // Call the TestCaseBase PreExecution
            base.PreExecution();

            // Test system is powered on.Cabin is activated.SoM is performed until level 2 is selected and confirmed.
            DmiActions.Complete_SoM_L1_SB(this);
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Level = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_LEVEL.L2;
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in SB mode, Level 2

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint

            /*
            Test Step 1
            Action: Perform the following procedure,a)   Press and hold the ‘Radio Network ID’ button at least 2 seconds. Then, release the pressed button.b)  Press the ‘Close’ button.c)   Press the ‘Enter RBC Data’ button.d)   Press the ‘Close’ button
            Expected Result: DMI displays RBC Contact window.Verify the following information(1)   Use the log file to confirm that DMI sends out packet [MMI_DRIVER_REQUEST (EVC-101)] with the value of variable MMI_M_REQUEST refer to sequence below,a)   MMI_M_REQUEST = 56 (Start Network ID)b)   MMI_M_REQUEST = 61 (Exit RBC Network ID)c)   MMI_M_REQUEST = 28 (Start RBC Data Entry)d)   MMI_M_REQUEST = 33 (Exit RBC Data Entry)Note: The sequence of MMI_M_REQUEST value are consistent with step of each action.(2)   When the button is pressed in each action, the window of pressed button is closed
            Test Step Comment: (1) MMI_gen 151 (partly: MMI_M_REQUEST = 56, 61, 28, 33) ;(2) MMI_gen 151 (partly: close opened menu);
            */
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.No_window_specified;
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.EnterRBCData |
                                                               EVC30_MMIRequestEnable.EnabledRequests.RadioNetworkID |
                                                               EVC30_MMIRequestEnable.EnabledRequests.ContactLastRBC |
                                                               EVC30_MMIRequestEnable.EnabledRequests.Level;
            EVC30_MMIRequestEnable.Send();

            EVC22_MMICurrentRBC.MMI_NID_WINDOW = 5;
            EVC22_MMICurrentRBC.Send();

            DmiActions.ShowInstruction(this,
                @"Press and hold the ‘Radio Network ID’ button for at least 2s, then release the button");

            EVC101_MMIDriverRequest.CheckMRequestPressed = MMI_M_REQUEST.StartNetworkID;

            EVC22_MMICurrentRBC.MMI_NID_WINDOW = 9;
            EVC22_MMICurrentRBC.NetworkCaptions = new List<string> {"GSMR-A", "GSMR-B"};
            EVC22_MMICurrentRBC.DataElements = new List<DataElement>
            {
                new DataElement {Identifier = 0, QDataCheck = 23, EchoText = ""},
                new DataElement {Identifier = 1, QDataCheck = 24, EchoText = ""}
            };
            EVC22_MMICurrentRBC.Send();

            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button");

            EVC101_MMIDriverRequest.CheckMRequestPressed = MMI_M_REQUEST.ExitRBCNetworkID;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI closes the Radio network ID window.");

            DmiActions.ShowInstruction(this, @"Press the ‘Enter RBC data’ button");

            EVC101_MMIDriverRequest.CheckMRequestPressed = MMI_M_REQUEST.StartRBCdataEntry;

            EVC22_MMICurrentRBC.MMI_NID_WINDOW = 10;
            EVC22_MMICurrentRBC.Send();

            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button");

            EVC101_MMIDriverRequest.CheckMRequestPressed = MMI_M_REQUEST.ExitRBCdataEntry;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI closes the RBC data window.");

            /*
            Test Step 2
            Action: Perform the following procedure,Press the ‘Enter RBC Data’ button.Enter the value of an input fields as follows,RBC ID = 6996969RBC Phone = 0031840880100Press 'Yes' button
            Expected Result: DMI displays Main window
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Enter RBC data’ button");

            EVC22_MMICurrentRBC.MMI_NID_WINDOW = 10;
            EVC22_MMICurrentRBC.MMI_M_BUTTONS = EVC22_MMICurrentRBC.EVC22BUTTONS.BTN_YES_DATA_ENTRY_COMPLETE;
            EVC22_MMICurrentRBC.Send();

            DmiActions.ShowInstruction(this,
                @"Enter ‘6996969’ for RBC ID, ‘0031840880100’ for RBC Phone and press the 'Yes' button");

            // DMI_RS_ETCS says if 0 network items this packet closes RBC Contact window
            EVC22_MMICurrentRBC.MMI_NID_WINDOW = 9;
            EVC22_MMICurrentRBC.NetworkCaptions.Clear();
            EVC22_MMICurrentRBC.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Main window.");

            /*
            Test Step 3
            Action: Perform the following procedure,Press the ‘Level’ button.Select and confirm ‘Level 2’At the RBC Contact window, press ‘Close’ button
            Expected Result: Verify the following information(1)   Use the log file to confirm that DMI sends out packet [MMI_DRIVER_REQUEST (EVC-101)] with the value of variable MMI_M_REQUEST = 39 (Exit RBC contact).(2)   The RBC Contact window is closed, DMI displays Main window
            Test Step Comment: (1) MMI_gen 151 (partly: MMI_M_REQUEST = 39);(2) MMI_gen 151 (partly: close opened menu);
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Level’ button");

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
                {Variables.MMI_M_LEVEL_NTC_ID.L2};
            EVC20_MMISelectLevel.Send();

            DmiActions.ShowInstruction(this, @"Select and confirm Level 2");

            EVC22_MMICurrentRBC.MMI_NID_WINDOW = 5;
            EVC22_MMICurrentRBC.Send();

            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button in the RBC Contact window");

            EVC101_MMIDriverRequest.CheckMRequestPressed = MMI_M_REQUEST.ExitRBCcontact;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI closes the RBC Contact window and displays the Main window.");

            /*
            Test Step 4
            Action: Perform the following procedure,Press the ‘Level’ button.Select and confirm ‘Level 2’At the RBC Contact window, press ‘Contact last RBC’ button
            Expected Result: DMI displays Main window.Verify the following information(1)   Use the log file to confirm that DMI sends out packet [MMI_DRIVER_REQUEST (EVC-101)] with the value of variable MMI_M_REQUEST = 57 (Contact last RBC).(2)   The RBC Contact window is closed, DMI displays Main window
            Test Step Comment: (1) MMI_gen 151 (partly: MMI_M_REQUEST = 57);(2) MMI_gen 151 (partly: close opened menu);
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Level’ button");

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
                {Variables.MMI_M_LEVEL_NTC_ID.L2};
            EVC20_MMISelectLevel.Send();

            DmiActions.ShowInstruction(this, @"Select and confirm Level 2");

            EVC22_MMICurrentRBC.MMI_NID_WINDOW = 5;
            EVC22_MMICurrentRBC.Send();

            DmiActions.ShowInstruction(this, @"Press the ‘Contact last RBC’ button in the RBC Contact window");

            EVC101_MMIDriverRequest.CheckMRequestPressed = MMI_M_REQUEST.ContactLastRBC;

            // Force the RBC Contact window to close
            EVC22_MMICurrentRBC.MMI_NID_WINDOW = 9;
            EVC22_MMICurrentRBC.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI closes the RBC Contact window and displays the Main window.");

            /*
            Test Step 5
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
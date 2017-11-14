
using System;
using Testcase.Telegrams.EVCtoDMI;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 15.4.1.1 State ‘ST05’: Abort the pending Data Process in Main window
    /// TC-ID: 10.4.1.1
    /// 
    /// This test case verifies that the process of data entry and validation window in state ST05 is aborted by a received packet of different window type (i.e., data view window) from ETCS onboard.
    /// 
    /// Tested Requirements:
    /// MMI_gen 5507 (partly: Main window, abort an already pending data entry and validation processes, received packet of different window from ETCS onboard);
    /// 
    /// Scenario:
    /// 1.Verify the display information when execute the test script files when open the windows below,Driver ID windowTrain running number windowLevel windowTrain Data windowTrain Data validation window
    /// 
    /// Used files:
    /// 10_4_1_1_a.xml, 10_4_1_1_b.xml
    /// </summary>
    public class TC_ID_10_4_1_1_State_ST05 : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:

            // Call the TestCaseBase PreExecution
            base.PreExecution();
            // Test system is powered onCabin is activeSoM is performed until level 1 is selected and confirmed.
            DmiActions.Complete_SoM_L1_SB(this);
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in SB mode

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint

            /*
            Test Step 1
            Action: At the Main window, press ‘Driver ID’ button
            Expected Result: DMI displays Driver ID window
            */
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Main;       // Main window
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.TrainData |
                                                               EVC30_MMIRequestEnable.EnabledRequests.DriverID |
                                                               EVC30_MMIRequestEnable.EnabledRequests.Level |
                                                               EVC30_MMIRequestEnable.EnabledRequests.TrainRunningNumber;
            EVC30_MMIRequestEnable.Send();
          
            DmiActions.ShowInstruction(this, @"Press ‘Driver ID’ button");
            DmiActions.Set_Driver_ID(this, "1234");
            
            DmiExpectedResults.Driver_ID_window_displayed(this);

            /*
            Test Step 2
            Action: Use the test script file 10_4_1_1_a.xml to send EVC-8 withMMI_Q_TEXT_CRITERIA = 3 MMI_Q_TEXT = 716
            Expected Result: The hourglass symbol ST05 is displayed at window title area
            */
            XML_10_4_1_1_a_b(msgType.typea);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The hourglass symbol ST05 is displayed in the window title area.");

            /*
            Test Step 3
            Action: Use the test script file 10_4_1_1_b.xml to send EVC-24 withMMI_NID_ENGINE_1 = 1234MMI_M_BRAKE_CONFIG = 55MMI_M_AVAIL_SERVICES = 65535MMI_M_ETC_VER = 16755215
            Expected Result: Verify the followin information,(1)     The Driver ID window is closed, DMI displays System info window after received packet EVC-24
            Test Step Comment: (1) MMI_gen 5507 (partly: Driver ID window, abort an already pending data entry process, received packet of different window from ETCS onboard);
            */
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Close_current_return_to_parent;    // Close window
            EVC30_MMIRequestEnable.Send();
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 4;
            EVC8_MMIDriverMessage.Send();

            XML_10_4_1_1_a_b(msgType.typeb);
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The Driver ID window is closed");

            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.No_window_specified;
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.TrainData |
                                                               EVC30_MMIRequestEnable.EnabledRequests.DriverID |
                                                               EVC30_MMIRequestEnable.EnabledRequests.Level |
                                                               EVC30_MMIRequestEnable.EnabledRequests.TrainRunningNumber;
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_LOW = true;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the System info window.");

            /*
            Test Step 4
            Action: Perform the following procedure,At System info window, press ‘close’ button.Open Train running number windowRepeat action step 2-3
            Expected Result: Verify the followin information,(1)    The Train Running Number window is closed, DMI displays System info window after received packet EVC-24
            Test Step Comment: (1) MMI_gen 5507 (partly: Train Running Number window, abort an already pending data entry process, received packet of different window from ETCS onboard);
            */
            EVC30_MMIRequestEnable.Send();

            DmiActions.ShowInstruction(this, @"Press the  ‘Close’ button in the System info window");

            // Re-display the main window with buttons enabled
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Main;       // Main window
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.TrainData |
                                                               EVC30_MMIRequestEnable.EnabledRequests.DriverID |
                                                               EVC30_MMIRequestEnable.EnabledRequests.Level |
                                                               EVC30_MMIRequestEnable.EnabledRequests.TrainRunningNumber;
            EVC30_MMIRequestEnable.Send();

            DmiActions.ShowInstruction(this, @"Open the Train Running number window");

            EVC16_CurrentTrainNumber.TrainRunningNumber = 1;
            EVC16_CurrentTrainNumber.Send();

            XML_10_4_1_1_a_b(msgType.typea);
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The hourglass symbol ST05 is displayed in the window title area.");

            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Close_current_return_to_parent;    // Close window
            EVC30_MMIRequestEnable.Send();
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 4;
            EVC8_MMIDriverMessage.Send();

            XML_10_4_1_1_a_b(msgType.typeb);
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The Driver ID window is closed and DMI displays the System info window.");
            
            /*
            Test Step 5
            Action: Perform the following procedure,At System info window, press ‘close’ button.Open Level windowRepeat action step 2-3
            Expected Result: Verify the followin information,(1)    The Level window is closed, DMI displays System info window after received packet EVC-24
            Test Step Comment: (1) MMI_gen 5507 (partly: Level  window, abort an already pending data entry process, received packet of different window from ETCS onboard);
            */
            DmiActions.ShowInstruction(this, @"Press the  ‘Close’ button in the System info window");

            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Main;       // Main window
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.TrainData |
                                                               EVC30_MMIRequestEnable.EnabledRequests.DriverID |
                                                               EVC30_MMIRequestEnable.EnabledRequests.Level |
                                                               EVC30_MMIRequestEnable.EnabledRequests.TrainRunningNumber;
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_LOW = true;
            EVC30_MMIRequestEnable.Send();


            DmiActions.ShowInstruction(this, @"Open the Level window.");

            EVC20_MMISelectLevel.MMI_Q_CLOSE_ENABLE = Variables.MMI_Q_CLOSE_ENABLE.Enabled;
            EVC20_MMISelectLevel.MMI_Q_LEVEL_NTC_ID = new Variables.MMI_Q_LEVEL_NTC_ID[] { Variables.MMI_Q_LEVEL_NTC_ID.ETCS_Level };
            EVC20_MMISelectLevel.MMI_M_CURRENT_LEVEL = new Variables.MMI_M_CURRENT_LEVEL[] { Variables.MMI_M_CURRENT_LEVEL.NotLastUsedLevel };
            EVC20_MMISelectLevel.MMI_M_LEVEL_FLAG = new Variables.MMI_M_LEVEL_FLAG[] { Variables.MMI_M_LEVEL_FLAG.MarkedLevel };
            EVC20_MMISelectLevel.MMI_M_INHIBITED_LEVEL = new Variables.MMI_M_INHIBITED_LEVEL[] { Variables.MMI_M_INHIBITED_LEVEL.NotInhibited };
            EVC20_MMISelectLevel.MMI_M_INHIBIT_ENABLE = new Variables.MMI_M_INHIBIT_ENABLE[] { Variables.MMI_M_INHIBIT_ENABLE.AllowedForInhibiting };
            EVC20_MMISelectLevel.MMI_M_LEVEL_NTC_ID = new Variables.MMI_M_LEVEL_NTC_ID[] { Variables.MMI_M_LEVEL_NTC_ID.L1 };
            EVC20_MMISelectLevel.Send();

            XML_10_4_1_1_a_b(msgType.typea);
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The hourglass symbol ST05 is displayed in the window title area.");
            
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 4;
            EVC8_MMIDriverMessage.Send();

            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Close_current_return_to_parent;    // Close window
            EVC30_MMIRequestEnable.Send();

            EVC24_MMISystemInfo.MMI_NID_ENGINE_1 = 1234;
            EVC24_MMISystemInfo.MMI_T_TIMEOUT_BRAKE = 0x5695224c;         // 1452614220
            EVC24_MMISystemInfo.MMI_T_TIMEOUT_BTM = 0x54b3eecc;            // 1421078220
            EVC24_MMISystemInfo.MMI_T_TIMEOUT_TBSW = 0x538b4d4c;           // 1401638220
            EVC24_MMISystemInfo.MMI_M_ETC_VER = 0xffaa0f;                    // 16755215
            EVC24_MMISystemInfo.MMI_M_AVAIL_SERVICES = 0xffff;             // 65535 

            // Discrepancy betwee spec (config = 55)
            EVC24_MMISystemInfo.MMI_M_BRAKE_CONFIG = 55;                   // 236 in xml
            EVC24_MMISystemInfo.MMI_M_LEVEL_INST = 248;
            EVC24_MMISystemInfo.Send();

            XML_10_4_1_1_a_b(msgType.typeb);
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The Level window is closed and DMI displays the System info window.");

            /*
            Test Step 6
            Action: Perform the following procedure,At System info window, press ‘close’ button.Open Train data windowRepeat action step 2-3
            Expected Result: Verify the followin information,(1)    The Train data window is closed, DMI displays System info window after received packet EVC-24
            Test Step Comment: (1) MMI_gen 5507 (partly: Train Data Number window, abort an already pending data validation process, received packet of different window from ETCS onboard);
            */
            DmiActions.ShowInstruction(this, @"Press the  ‘Close’ button in the System info window");

            // Re-display the main window with buttons enabled
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Main;       // Main window
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.TrainData |
                                                               EVC30_MMIRequestEnable.EnabledRequests.DriverID |
                                                               EVC30_MMIRequestEnable.EnabledRequests.Level |
                                                               EVC30_MMIRequestEnable.EnabledRequests.TrainRunningNumber;
            EVC30_MMIRequestEnable.Send();

            DmiActions.ShowInstruction(this, @"Open the Train data window");

            DmiActions.Send_EVC6_MMICurrentTrainData_FixedDataEntry(this, new[] { "FLU", "RLU", "Rescue" }, 2);

            XML_10_4_1_1_a_b(msgType.typea);
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The hourglass symbol ST05 is displayed in the window title area.");

            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Close_current_return_to_parent;    // Close window
            EVC30_MMIRequestEnable.Send();
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 4;
            EVC8_MMIDriverMessage.Send();

            XML_10_4_1_1_a_b(msgType.typeb);
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The Train data window is closed and DMI displays the System info window.");

            /*
            Test Step 7
            Action: Perform the following procedure,At System info window, press ‘close’ button.Open Train data windowEnter and confirm all value of an input fieldsPress on enabled ‘Yes’ button
            Expected Result: DMI displays Train data validation window
            */
            DmiActions.ShowInstruction(this, @"Press the  ‘Close’ button in the System info window");

            // Re-display the main window with buttons enabled
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Main;       // Main window
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.TrainData |
                                                               EVC30_MMIRequestEnable.EnabledRequests.DriverID |
                                                               EVC30_MMIRequestEnable.EnabledRequests.Level |
                                                               EVC30_MMIRequestEnable.EnabledRequests.TrainRunningNumber;

            DmiActions.ShowInstruction(this, @"Open the Train data window");

            DmiActions.Send_EVC6_MMICurrentTrainData_FixedDataEntry(this, new[] { "FLU", "RLU", "Rescue" }, 2);                                                                                                                                                                                                                                                     

            DmiActions.ShowInstruction(this, @"Enter and accept the values of all Input Fields. Press on the enabled ‘Yes’ button");

            DmiActions.Send_EVC10_MMIEchoedTrainData_FixedDataEntry(this, Variables.paramEvc6FixedTrainsetCaptions);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Train validation window.");

            /*
            Test Step 8
            Action: Repeat action step 2-3
            Expected Result: Verify the followin information,(1)    The Train data validation window is closed, DMI displays System info window after received packet EVC-24
            Test Step Comment: (1) MMI_gen 5507 (partly: Train Data Number window, abort an already pending data entry process, received packet of different window from ETCS onboard);
            */
            XML_10_4_1_1_a_b(msgType.typea);
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The hourglass symbol ST05 is displayed in the window title area.");                                             

            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Close_current_return_to_parent;    // Close window
            EVC30_MMIRequestEnable.Send();

            XML_10_4_1_1_a_b(msgType.typeb);
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The Train Data validation window is closed and DMI displays the System info window.");

            /*
            Test Step 9
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }

        #region Send_XML_10_4_1_1_a_b_DMI_Test_Specification
        enum msgType
        {
            typea,
            typeb
        }

        private void XML_10_4_1_1_a_b(msgType type)
        {
            if (type == msgType.typea)
            {
                EVC8_MMIDriverMessage.MMI_Q_TEXT = 716;
                EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
                EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
                EVC8_MMIDriverMessage.MMI_I_TEXT = 1;

                EVC8_MMIDriverMessage.Send();
            }
            else if (type == msgType.typeb)
            {
                EVC24_MMISystemInfo.MMI_NID_ENGINE_1 = 1234;
                EVC24_MMISystemInfo.MMI_T_TIMEOUT_BRAKE = 0x5695224c;         // 1452614220
                EVC24_MMISystemInfo.MMI_T_TIMEOUT_BTM = 0x54b3eecc;            // 1421078220
                EVC24_MMISystemInfo.MMI_T_TIMEOUT_TBSW = 0x538b4d4c;           // 1401638220
                EVC24_MMISystemInfo.MMI_M_ETC_VER = 0xffaa0f;                    // 16755215
                EVC24_MMISystemInfo.MMI_M_AVAIL_SERVICES = 0xffff;             // 65535 

                // Discrepancy betwee spec (config = 55)
                EVC24_MMISystemInfo.MMI_M_BRAKE_CONFIG = 55;                   // 236 in xml
                EVC24_MMISystemInfo.MMI_M_LEVEL_INST = 248;

                EVC24_MMISystemInfo.Send();
            }
        }
        #endregion


    }
}
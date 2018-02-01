using System;
using Testcase.Telegrams.EVCtoDMI;

namespace Testcase.DMITestCases
{
    /// <summary>
    /// 15.4.1.2 State ‘ST05’: Abort the pending Data Process in Data View window
    /// TC-ID: 10.4.1.2
    /// 
    /// This test case verifies that the process of data view window in state ST05 is aborted by a received packet of different window type (i.e., data entry window) from ETCS onboard.
    /// 
    /// Tested Requirements:
    /// MMI_gen 5507 (partly: Data View window, abort an already pending data view process, received packet of different window from ETCS onboard);
    /// 
    /// Scenario:
    /// 1.Verify the display information when execute the test script files when open the windows below,Data view windowSystem Info windowSystem Version window
    /// 
    /// Used files:
    /// 10_4_1_2_a.xml, 10_4_1_2_b.xml
    /// </summary>
    public class TC_ID_10_4_1_2_State_ST05 : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:

            // Call the TestCaseBase PreExecution
            base.PreExecution();
            // Test system is powered onCabin is activeSoM is performed until level 1 is selected and confirmed.Main window is closed
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
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 0;
            // Testcase entrypoint

            TraceHeader("Test Step 1");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("At the Default window, press ‘Data View’ button");
            TraceReport("Expected Result");
            TraceInfo("DMI displays Data View window");
            /*
            Test Step 1
            Action: At the Default window, press ‘Data View’ button
            Expected Result: DMI displays Data View window
            */
            DmiActions.ShowInstruction(this, @"Press ‘Data View’ button");

            EVC13_MMIDataView.MMI_X_DRIVER_ID = "";
            EVC13_MMIDataView.MMI_NID_OPERATION = 0xffffffff;
            EVC13_MMIDataView.MMI_M_DATA_ENABLE = (Variables.MMI_M_DATA_ENABLE) 0x0080; // 128
            EVC13_MMIDataView.MMI_L_TRAIN = 4096;
            EVC13_MMIDataView.MMI_V_MAXTRAIN = 601;
            EVC13_MMIDataView.MMI_M_BRAKE_PERC = 9;
            EVC13_MMIDataView.MMI_NID_KEY_AXLE_LOAD = Variables.MMI_NID_KEY.FG4; // 20
            EVC13_MMIDataView.MMI_NID_RADIO =
                0xffffffffffffffff; // 4294967295 (= 0xffffffff) hi, 4294967295 (= 0xffffffff) lo
            EVC13_MMIDataView.MMI_M_AIRTIGHT = 3;
            EVC13_MMIDataView.MMI_NID_KEY_LOAD_GAUGE = Variables.MMI_NID_KEY.CATE5;
            EVC13_MMIDataView.Trainset_Caption = "";
            EVC13_MMIDataView.Network_Caption = "";
            EVC13_MMIDataView.MMI_NID_KEY_TRAIN_CAT = Variables.MMI_NID_KEY.CATA; // 21
            EVC13_MMIDataView.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Data View window.");

            TraceHeader("Test Step 2");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Use the test script file 10_4_1_2_a.xml to send EVC-8 withMMI_Q_TEXT_CRITERIA = 3 MMI_Q_TEXT = 716");
            TraceReport("Expected Result");
            TraceInfo("The hourglass symbol ST05 is displayed at window title area");
            /*
            Test Step 2
            Action: Use the test script file 10_4_1_2_a.xml to send EVC-8 withMMI_Q_TEXT_CRITERIA = 3 MMI_Q_TEXT = 716
            Expected Result: The hourglass symbol ST05 is displayed at window title area
            */
            XML_10_4_1_2_a_b(msgType.typea);
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The hourglass symbol ST05 is displayed in the window title area.");

            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 4;
            EVC8_MMIDriverMessage.Send();

            TraceHeader("Test Step 3");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Use the test script file 10_4_1_2_b.xml to send EVC-14 withMMI_X_DRIVER_ID = ‘4444’");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the followin information,(1)     The Data View window is closed, DMI displays Driver ID window after received packet EVC-14");
            /*
            Test Step 3
            Action: Use the test script file 10_4_1_2_b.xml to send EVC-14 withMMI_X_DRIVER_ID = ‘4444’
            Expected Result: Verify the followin information,(1)     The Data View window is closed, DMI displays Driver ID window after received packet EVC-14
            Test Step Comment: (1) MMI_gen 5507 (partly: Data View window, abort an already pending data view process, received packet of different window from ETCS onboard);
            */
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Close_current_return_to_parent;
            EVC30_MMIRequestEnable.Send();
            XML_10_4_1_2_a_b(msgType.typeb);
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The Data View window is closed and DMI displays the Driver ID window");

            TraceHeader("Test Step 4");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Perform the following procedure,At Driver ID window, press ‘close’ button.Open System Info windowRepeat action step 2-3");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the followin information,(1)     The System Info window is closed, DMI displays Driver ID window after received packet EVC-14");
            /*
            Test Step 4
            Action: Perform the following procedure,At Driver ID window, press ‘close’ button.Open System Info windowRepeat action step 2-3
            Expected Result: Verify the followin information,(1)     The System Info window is closed, DMI displays Driver ID window after received packet EVC-14
            Test Step Comment: (1) MMI_gen 5507 (partly: System Info window, abort an already pending data view process, received packet of different window from ETCS onboard);
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button in the Driver ID window");

            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_LOW = true;
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Default;
            EVC30_MMIRequestEnable.Send();

            DmiActions.ShowInstruction(this, @"Press the ‘Settings’ button, then open the System info window");

            EVC24_MMISystemInfo.Send();

            XML_10_4_1_2_a_b(msgType.typea);
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The hourglass symbol ST05 is displayed in the window title area.");

            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 4;
            EVC8_MMIDriverMessage.Send();
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Close_current_return_to_parent;
            EVC30_MMIRequestEnable.Send();
            EVC30_MMIRequestEnable.Send();

            XML_10_4_1_2_a_b(msgType.typeb);
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The System info window is closed and DMI displays the Driver ID window");

            TraceHeader("Test Step 5");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Perform the following procedure,At Driver ID window, press ‘close’ button.Open System version windowRepeat action step 2-3");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the followin information,(1)     The System version window is closed, DMI displays Driver ID window after received packet EVC-14");
            /*
            Test Step 5
            Action: Perform the following procedure,At Driver ID window, press ‘close’ button.Open System version windowRepeat action step 2-3
            Expected Result: Verify the followin information,(1)     The System version window is closed, DMI displays Driver ID window after received packet EVC-14
            Test Step Comment: (1) MMI_gen 5507 (partly: System version window, abort an already pending data view process, received packet of different window from ETCS onboard);
            */

            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button in the Driver ID window");

            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Main;
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.SystemVersion;
            EVC30_MMIRequestEnable.Send();

            DmiActions.ShowInstruction(this, "Open the System version window");

            EVC34_MMISystemVersion.SYSTEM_VERSION_X = 123;
            EVC34_MMISystemVersion.SYSTEM_VERSION_Y = 225;
            EVC34_MMISystemVersion.Send();

            XML_10_4_1_2_a_b(msgType.typea);
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The hourglass symbol ST05 is displayed in the window title area.");

            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Close_current_return_to_parent;
            EVC30_MMIRequestEnable.Send();

            XML_10_4_1_2_a_b(msgType.typeb);
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The System version window is closed and DMI displays the Driver ID window");

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

        #region Send_XML_10_4_1_2_a_b_DMI_Test_Specification

        enum msgType
        {
            typea,
            typeb
        }

        private void XML_10_4_1_2_a_b(msgType type)
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
                EVC14_MMICurrentDriverID.MMI_X_DRIVER_ID = "4444";
                EVC14_MMICurrentDriverID.Send();
            }
        }

        #endregion
    }
}
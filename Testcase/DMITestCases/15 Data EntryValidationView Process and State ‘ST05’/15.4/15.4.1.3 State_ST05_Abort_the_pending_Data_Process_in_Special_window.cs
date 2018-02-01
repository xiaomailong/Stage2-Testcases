using System;
using System.Collections.Generic;
using Testcase.Telegrams.EVCtoDMI;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 15.4.1.3 State ‘ST05’: Abort the pending Data Process in Special window
    /// TC-ID: 10.4.1.3
    /// 
    /// This test case verifies that the process of data entry window in state ST05 is aborted by a received packet of different window type (i.e., data view window) from ETCS onboard.
    /// 
    /// Tested Requirements:
    /// MMI_gen 5507 (partly: Special window, abort an already pending data processes, received packet of different window from ETCS onboard);
    /// 
    /// Scenario:
    /// 1.Verify the display information when execute the test script files when open the windows below,SR Speed/Distance windowAdhesion window
    /// 
    /// Used files:
    /// 10_4_1_3_a.xml, 10_4_1_3_b.xml
    /// </summary>
    public class TC_ID_10_4_1_3_State_ST05 : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // Use the ATP config editor to set the parameter Q_NVDRIVER_ADHES = 1 (See the instruction in Appendix 2).Test system is powered onCabin is activeSoM is performed until level 1 is selected and confirmed.Main window is closedSpecial window is opened

            // Call the TestCaseBase PreExecution
            base.PreExecution();
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
            TraceInfo("At the Special window, press ‘SR speed/distance’’ button");
            TraceReport("Expected Result");
            TraceInfo("DMI displays SR speed/distance window");
            /*
            Test Step 1
            Action: At the Special window, press ‘SR speed/distance’’ button
            Expected Result: DMI displays SR speed/distance window
            */
            DmiActions.ShowInstruction(this, @"Open the Special window, then press the ‘SR speed/distance’ button");

            EVC11_MMICurrentSRRules.MMI_M_BUTTONS = Variables.MMI_M_BUTTONS.BTN_ENTER;
            EVC11_MMICurrentSRRules.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the SR/speed distance window.");

            TraceHeader("Test Step 2");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Use the test script file 10_4_1_3_a.xml to send EVC-8 withMMI_Q_TEXT_CRITERIA = 3 MMI_Q_TEXT = 716");
            TraceReport("Expected Result");
            TraceInfo("The hourglass symbol ST05 is displayed at window title area");
            /*
            Test Step 2
            Action: Use the test script file 10_4_1_3_a.xml to send EVC-8 withMMI_Q_TEXT_CRITERIA = 3 MMI_Q_TEXT = 716
            Expected Result: The hourglass symbol ST05 is displayed at window title area
            */
            XML_10_4_1_3_a_b(msgType.typea);
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The hourglass symbol ST05 is displayed in the window title area.");

            TraceHeader("Test Step 3");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Use the test script file 10_4_1_3_b.xml to send EVC-24 withMMI_NID_ENGINE_1 = 1234MMI_M_BRAKE_CONFIG = 55MMI_M_AVAIL_SERVICES = 65535MMI_M_ETC_VER = 16755215");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the followin information,(1)     The SR speed/distance window is closed, DMI displays System info window after received packet EVC-24");
            /*
            Test Step 3
            Action: Use the test script file 10_4_1_3_b.xml to send EVC-24 withMMI_NID_ENGINE_1 = 1234MMI_M_BRAKE_CONFIG = 55MMI_M_AVAIL_SERVICES = 65535MMI_M_ETC_VER = 16755215
            Expected Result: Verify the followin information,(1)     The SR speed/distance window is closed, DMI displays System info window after received packet EVC-24
            Test Step Comment: (1) MMI_gen 5507 (partly: SR speed/distance window, abort an already pending data entry process, received packet of different window from ETCS onboard);
            */
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 4;
            EVC8_MMIDriverMessage.Send();

            EVC11_MMICurrentSRRules.DataElements = new List<Variables.DataElement>
            {
                new Variables.DataElement
                {
                    Identifier = 0,
                    EchoText = "0",
                    QDataCheck = (ushort) Variables.Q_DATA_CHECK.All_checks_passed
                },
                new Variables.DataElement
                {
                    Identifier = 1,
                    EchoText = "0",
                    QDataCheck = (ushort) Variables.Q_DATA_CHECK.All_checks_passed
                }
            };

            EVC11_MMICurrentSRRules.Send();

            // DMI is not closing the window as it should

            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Close_current_return_to_parent;
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.Adhesion;
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_LOW = true;
            EVC30_MMIRequestEnable.Send();

            XML_10_4_1_3_a_b(msgType.typeb);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The SR speed/distance window is closed and DMI displays the System info window.");

            TraceHeader("Test Step 4");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Perform the following procedure,At System info window, press ‘close’ button.Open Adhesion windowRepeat action step 2-3");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the followin information,(1)     The Adhesion window is closed, DMI displays System info window after received packet EVC-24");
            /*
            Test Step 4
            Action: Perform the following procedure,At System info window, press ‘close’ button.Open Adhesion windowRepeat action step 2-3
            Expected Result: Verify the followin information,(1)     The Adhesion window is closed, DMI displays System info window after received packet EVC-24
            Test Step Comment: (1) MMI_gen 5507 (partly: Adhesion window, abort an already pending data entry process, received packet of different window from ETCS onboard);
            */
            DmiActions.ShowInstruction(this,
                @" Press the ‘Close’ button in the System info window. Open the Special window, then press the ‘Adhesion’ button");

            XML_10_4_1_3_a_b(msgType.typea);
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The hourglass symbol ST05 is displayed in the window title area.");

            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 4;
            EVC8_MMIDriverMessage.Send();
            EVC30_MMIRequestEnable.Send();

            XML_10_4_1_3_a_b(msgType.typeb);
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The Adhesion window is closed and DMI displays the System info window.");

            TraceHeader("Test Step 5");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("End of test");
            
            /*
            Test Step 5
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }

        #region Send_XML_10_4_1_3_a_b_DMI_Test_Specification

        enum msgType
        {
            typea,
            typeb
        }

        private void XML_10_4_1_3_a_b(msgType type)
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
                EVC24_MMISystemInfo.MMI_T_TIMEOUT_BRAKE = 0x5695224c; // 1452614220
                EVC24_MMISystemInfo.MMI_T_TIMEOUT_BTM = 0x54b3eecc; // 1421078220
                EVC24_MMISystemInfo.MMI_T_TIMEOUT_TBSW = 0x538b4d4c; // 1401638220
                EVC24_MMISystemInfo.MMI_M_ETC_VER = 0xffaa0f; // 16755215
                EVC24_MMISystemInfo.MMI_M_AVAIL_SERVICES = 0xffff; // 65535 

                // Discrepancy betwee spec (config = 55)
                EVC24_MMISystemInfo.MMI_M_BRAKE_CONFIG = 55; // 236 in xml
                EVC24_MMISystemInfo.MMI_M_LEVEL_INST = 248;

                EVC24_MMISystemInfo.Send();
            }
        }

        #endregion
    }
}
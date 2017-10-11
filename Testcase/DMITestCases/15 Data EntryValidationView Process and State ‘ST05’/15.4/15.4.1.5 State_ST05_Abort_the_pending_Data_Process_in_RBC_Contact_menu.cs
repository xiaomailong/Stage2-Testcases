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
    /// 15.4.1.5 State ‘ST05’: Abort the pending Data Process in RBC Contact menu
    /// TC-ID: 10.4.1.5
    /// 
    /// This test case verifies that the process of data entry window in state ST05 is aborted by a received packet of different window type (i.e., data view window) from ETCS onboard.
    /// 
    /// Tested Requirements:
    /// MMI_gen 5507 (partly: RBC Data window, Radio Network ID window, abort an already pending data entry processes, received packet of different window from ETCS onboard);
    /// 
    /// Scenario:
    /// 1.Verify the display information when execute the test script files when open the windows below,RBC Data windowRadio Network ID window
    /// 
    /// Used files:
    /// 10_4_1_5_a.xml, 10_4_1_5_b.xml, 10_4_1_5.utt
    /// </summary>
    public class TC_ID_10_4_1_5_State_ST05 : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:

            // Call the TestCaseBase PreExecution
            base.PreExecution();

            // Test system is powered onCabin is activeSoM is performed until level 2 is selected and confirmed.
            DmiActions.Start_ATP();

            // Set train running number, cab 1 active, and other defaults
            DmiActions.Activate_Cabin_1(this);

            // Set driver ID
            DmiActions.Set_Driver_ID(this, "1234");

            // Set to level 2 and SR mode
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Level = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_LEVEL.L2;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.StandBy;

            // Enable standard buttons including Start, and display Default window.
            DmiActions.Finished_SoM_Default_Window(this);
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in SB mode
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in SB mode, Level 1.");

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint


            /*
            Test Step 1
            Action: At the RBC contact window, press ‘RBC Data’ button
            Expected Result: DMI displays RBC Data window
            */
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = 255;
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.EnterRBCData;
            EVC30_MMIRequestEnable.Send();

            EVC22_MMICurrentRBC.MMI_NID_WINDOW = 5;
            EVC22_MMICurrentRBC.Send();
            
            DmiActions.ShowInstruction(this, @"Press the ‘RBC Data’ button in the RBC Contact window");

            EVC22_MMICurrentRBC.MMI_NID_WINDOW = 10;
            EVC22_MMICurrentRBC.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the RBC Data window.");

            /*
            Test Step 2
            Action: Use the test script file 10_4_1_5_a.xml to send EVC-8 withMMI_Q_TEXT_CRITERIA = 3 MMI_Q_TEXT = 716
            Expected Result: The hourglass symbol ST05 is displayed at window title area
            */
            XML_10_4_1_5_a_b(msgType.typea);
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The hourglass symbol ST05 is displayed in the window title area.");

            /*
            Test Step 3
            Action: Use the test script file 10_4_1_5_b.xml to send EVC-24 withMMI_NID_ENGINE_1 = 1234MMI_M_BRAKE_CONFIG = 55MMI_M_AVAIL_SERVICES = 65535MMI_M_ETC_VER = 16755215
            Expected Result: Verify the followin information,(1)     The RBC Data window is closed, DMI displays System info window after received packet EVC-24
            Test Step Comment: (1) MMI_gen 5507 (partly: RBC Data window, abort an already pending data entry process, received packet of different window from ETCS onboard);
            */
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = 254;
            EVC30_MMIRequestEnable.Send();

            XML_10_4_1_5_a_b(msgType.typeb);
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The RBC Data window is closed and DMI displays the System info window");

            /*
            Test Step 4
            Action: Perform the following procedure,At System info window, press ‘close’ button.Press and hold ‘Radio network ID’ button at least 2 seconds.Release the pressed area.Repeat action step 2-3
            Expected Result: Verify the followin information,(1)    The Radio Network ID window is closed, DMI displays System info window after received packet EVC-24
            Test Step Comment: (1) MMI_gen 5507 (partly: Radio Network ID window, abort an already pending data entry process, received packet of different window from ETCS onboard);
            */
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = 255;
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.RadioNetworkID;
            EVC30_MMIRequestEnable.Send();

            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button in the System info window. Press and hold the ‘Radio network ID’ button for at least 2 seconds." + Environment.NewLine +
                                             @"Release the area pressed");

            XML_10_4_1_5_a_b(msgType.typea);
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The hourglass symbol ST05 is displayed in the window title area.");

            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = 254;
            EVC30_MMIRequestEnable.Send();

            XML_10_4_1_5_a_b(msgType.typeb);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The Radio Network ID window is closed and DMI displays the System info window");

            /*
            Test Step 5
            Action: End of test
            Expected Result: 
            */
            
            return GlobalTestResult;
        }
        #region Send_XML_10_4_1_5_a_b_DMI_Test_Specification
        enum msgType
        {
            typea,
            typeb
        }

        private void XML_10_4_1_5_a_b(msgType type)
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
                //EVC24MMISystemInfo.MMI_NID_ENGINE_1 = 1234;
                //EVC24MMISystemInfo.MMI_T_TIMEOUT_BRAKES = 0x5695224c;         // 1452614220
                //EVC24MMISystemInfo.MMI_T_TIMEOUT_BTM = 0x54b3eecc;            // 1421078220
                //EVC24MMISystemInfo.MMI_T_TIMEOUT_TBSW = 0x538b4d4c;           // 1401638220
                //EVC24MMISystemInfo.MMI_ETC_VER = 0xffaa0f;                    // 16755215
                //EVC24MMISystemInfo.MMI_M_AVAIL_SERVICES = 0xffff;             // 65535 

                // Discrepancy betwee spec (config = 55)
                //EVC24MMISystemInfo.MMI_M_BRAKE_CONFIG = 55;                   // 236 in xml
                //EVC24MMISystemInfo.MMI_M_LEVEL_INSTALLED = 248;

                //EVC24MMISystemInfo.Send();
            }
        }
        #endregion

    }
}
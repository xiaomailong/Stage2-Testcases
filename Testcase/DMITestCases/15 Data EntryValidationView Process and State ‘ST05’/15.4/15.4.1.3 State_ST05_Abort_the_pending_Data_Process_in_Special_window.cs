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
            Action: At the Special window, press ‘SR speed/distance’’ button
            Expected Result: DMI displays SR speed/distance window
            */
            DmiActions.ShowInstruction(this, @"Close the Main window. Press the ‘SR speed/distance’ button in the Special window");
            
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the SR/speed distance window.");

            /*
            Test Step 2
            Action: Use the test script file 10_4_1_3_a.xml to send EVC-8 withMMI_Q_TEXT_CRITERIA = 3 MMI_Q_TEXT = 716
            Expected Result: The hourglass symbol ST05 is displayed at window title area
            */
            XML_10_4_1_3_a_b(msgType.typea);
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The hourglass symbol ST05 is displayed in the window title area.");

            /*
            Test Step 3
            Action: Use the test script file 10_4_1_3_b.xml to send EVC-24 withMMI_NID_ENGINE_1 = 1234MMI_M_BRAKE_CONFIG = 55MMI_M_AVAIL_SERVICES = 65535MMI_M_ETC_VER = 16755215
            Expected Result: Verify the followin information,(1)     The SR speed/distance window is closed, DMI displays System info window after received packet EVC-24
            Test Step Comment: (1) MMI_gen 5507 (partly: SR speed/distance window, abort an already pending data entry process, received packet of different window from ETCS onboard);
            */
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = 254;
            EVC30_MMIRequestEnable.Send();

            XML_10_4_1_3_a_b(msgType.typeb);
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The SR speed/distance window is closed and DMI displays the System info window.");

            /*
            Test Step 4
            Action: Perform the following procedure,At System info window, press ‘close’ button.Open Adhesion windowRepeat action step 2-3
            Expected Result: Verify the followin information,(1)     The Adhesion window is closed, DMI displays System info window after received packet EVC-24
            Test Step Comment: (1) MMI_gen 5507 (partly: Adhesion window, abort an already pending data entry process, received packet of different window from ETCS onboard);
            */
            DmiActions.ShowInstruction(this, @" Press the ‘Close’ button in the System info window. Open the Adhesion window");

            XML_10_4_1_3_a_b(msgType.typea);
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The hourglass symbol ST05 is displayed in the window title area.");

            XML_10_4_1_3_a_b(msgType.typeb);
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The Adhesion window is closed and DMI displays the System info window.");

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
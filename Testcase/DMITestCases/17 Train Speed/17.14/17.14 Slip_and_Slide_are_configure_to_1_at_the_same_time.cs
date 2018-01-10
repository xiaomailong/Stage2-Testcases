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
    /// 17.14 Slip and Slide are configured to 1 at the same time
    /// TC-ID: 12.14
    /// 
    /// This test case verifies the display of the ‘Slip/Slide’ indication when both of indication are configured enabled with ETC speed.
    /// 
    /// Tested Requirements:
    /// MMI_gen 1693;
    /// 
    /// Scenario:
    /// SoM is completed in SR mode, Level 1 and  SLIP_SPEEDMETER & SLIDE_SPEEDMETER are configured to be 1 (display)At 100 m, pass BG1 with pkt 12, pkt 21 and pkt 
    /// 27.Mode changes to FS mode.The ‘Slip/Slide’ indication is verified by the following cases:ATP disable MMI_M_SLIP and MMI_M_SLIDEATP enable MMI_M_SLIP and MMI_M_SLIDEATP enable MMI_M_SLIP but disable MMI_M_SLIDEATP disable MMI_M_SLIP butenable MMI_M_SLIDEThe train is stopped.
    /// 
    /// Used files:
    /// 12_14.tdg, 12_14_a.xml, 12_14_b.xml, 12_14_c.xml
    /// </summary>
    public class TC_ID_12_14_Train_Speed : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // Set the following tags name in configuration file (See the instruction in Appendix 1)
            // SLIP_SPEEDMETER = 1; SLIDE_SPEEDMETER = 1;

            // Call the TestCaseBase PreExecution
            base.PreExecution();
            DmiActions.Complete_SoM_L1_SR(this);
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in FS mode, level 1.

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint
            TraceInfo("This test case requires an ATP configuration change - " +
                      "See Precondition requirements. If this is not done manually, the test may fail!");

            /*
            Test Step 1
            Action: Driver the train forward
            Expected Result: DMI changes from SR mode to FS mode
            */
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.FullSupervision;
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 5;

            // Call generic Check Results Method
            DmiExpectedResults.FS_mode_displayed(this);

            /*
            Test Step 2
            Action: Drive the train forward with speed = 140 km/h
            Expected Result: The speed pointer is displayed with speed =140
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 140;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The speed pointer is displayed with speed = 140 km/h.");

            /*
            Test Step 3
            Action: Use the test script file 12_14_a.xml to send EVC-1 with,MMI_M_SLIP = 1MMI_M_SLIDE = 0
            Expected Result: The Slip indication is displayed and shown as arrow pointing clockwise
            */
            XML_12_14(msgType.typea);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The Slip indication is displayed and shown as arrow pointing clockwise.");

            /*
            Test Step 4
            Action: Use the test script file 12_14_b.xml to send EVC-1 with,MMI_M_SLIP = 0MMI_M_SLIDE = 1
            Expected Result: The Slide indication is displayed and shown as arrow pointing counterclockwise
            */
            XML_12_14(msgType.typeb);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The Slide indication is displayed and shown as arrow pointing clockwise.");

            /*
            Test Step 5
            Action: Use the test script file 12_14_c.xml to send EVC-1 with,MMI_M_SLIP = 1MMI_M_SLIDE = 1
            Expected Result: Verify the following information,The Slip indication is displayed and shown as arrow pointing clockwise
            Test Step Comment: (1) MMI_gen 1693;
            */
            XML_12_14(msgType.typec);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. (1)	The Slip indication is displayed and shown as arrow pointing clockwise.");

            /*
            Test Step 6
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }

        #region Send_XML_12_14_DMI_Test_Specification

        enum msgType
        {
            typea,
            typeb,
            typec
        }

        private void XML_12_14(msgType type)
        {
            // In other tests resetting these to 0 can cause test to fail
            //SITR.ETCS1.Dynamic.EVC01Validity1.Value = 0x0;
            //SITR.ETCS1.Dynamic.EVC01Validity2.Value = 0x0;

            if (type == msgType.typea)
            {
                EVC1_MMIDynamic.MMI_M_SLIDE = 0;
                EVC1_MMIDynamic.MMI_M_SLIP = 1;
                EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Normal_Status_Ceiling_Speed_Monitoring; // 0
                EVC1_MMIDynamic.MMI_A_TRAIN = 0;
                EVC1_MMIDynamic.MMI_V_TRAIN = 3888;
                EVC1_MMIDynamic.MMI_V_TARGET = 1111;
                EVC1_MMIDynamic.MMI_V_PERMITTED = 1111;
                EVC1_MMIDynamic.MMI_V_RELEASE = 555;
                EVC1_MMIDynamic.MMI_O_BRAKETARGET = 0;
                EVC1_MMIDynamic.MMI_O_IML = 0;
                EVC1_MMIDynamic.MMI_V_INTERVENTION = 0;
            }
            else if (type == msgType.typeb)
            {
                EVC1_MMIDynamic.MMI_M_SLIDE = 1;
                EVC1_MMIDynamic.MMI_M_SLIP = 0;
                EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Normal_Status_Ceiling_Speed_Monitoring; // 0
                EVC1_MMIDynamic.MMI_A_TRAIN = 0;
                EVC1_MMIDynamic.MMI_V_TRAIN = 3888;
                EVC1_MMIDynamic.MMI_V_TARGET = 1111;
                EVC1_MMIDynamic.MMI_V_PERMITTED = 1111;
                EVC1_MMIDynamic.MMI_V_RELEASE = 555;
                EVC1_MMIDynamic.MMI_O_BRAKETARGET = 0;
                EVC1_MMIDynamic.MMI_O_IML = 0;
                EVC1_MMIDynamic.MMI_V_INTERVENTION = 0;
            }
            else if (type == msgType.typec)
            {
                EVC1_MMIDynamic.MMI_M_SLIDE = 1;
                EVC1_MMIDynamic.MMI_M_SLIP = 1;
                EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Normal_Status_Ceiling_Speed_Monitoring; // 0
                EVC1_MMIDynamic.MMI_A_TRAIN = 0;
                EVC1_MMIDynamic.MMI_V_TRAIN = 3888;
                EVC1_MMIDynamic.MMI_V_TARGET = 1111;
                EVC1_MMIDynamic.MMI_V_PERMITTED = 1111;
                EVC1_MMIDynamic.MMI_V_RELEASE = 555;
                EVC1_MMIDynamic.MMI_O_BRAKETARGET = 0;
                EVC1_MMIDynamic.MMI_O_IML = 0;
                EVC1_MMIDynamic.MMI_V_INTERVENTION = 0;
            }
        }

        #endregion
    }
}
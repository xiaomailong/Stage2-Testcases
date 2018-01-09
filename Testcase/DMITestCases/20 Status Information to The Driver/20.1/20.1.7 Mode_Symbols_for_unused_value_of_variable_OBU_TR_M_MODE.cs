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
    /// 20.1.7 Mode Symbols for unused value of variable OBU_TR_M_MODE
    /// TC-ID: 15.1.7
    /// 
    /// This test case verifies the blank display on sub-area B7 (mode symbol) when DMI receives the unused value in packet information EVC-7.
    /// 
    /// Tested Requirements:
    /// MMI_gen 580;
    /// 
    /// Scenario:
    /// Use the test script file to send EVC-7 with specific unused values. Then, verify the blank display on sub-area B7.
    /// 
    /// Used files:
    /// 15_1_7_a.xml, 15_1_7_b.xml, 15_1_7_c.xml, 15_1_7_d.xml, 15_1_7_e.xml, 15_1_7_f.xml, 15_1_7_g.xml, 15_1_7_h.xml
    /// </summary>
    public class TC_15_1_7_ETCS_Mode_Symbols : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // Test system is powered onSoM is performed until level 1 is selected and confirmedMain window is closed

            // Call the TestCaseBase PreExecution
            base.PreExecution();
            DmiActions.Complete_SoM_L1_SB(this);
            //EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Level = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_LEVEL.L2;

        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in SB mode, Level 1.

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint


            /*
            Test Step 1
            Action: Use the test script file 15_1_7_a.xml to send EVC-7 with, MMI_OBU_TR_M_MODE = 17 (“Not used”)
            Expected Result: Verify the following information,
            (1)   There is no symbol displayed on sub-area B7
            Test Step Comment: (1) MMI_gen 580;
            */
            XML_15_1_7(msgType.typea);

            /*
            Test Step 2
            Action: Use the test script file 15_1_7_b.xml to send EVC-7 with, MMI_OBU_TR_M_MODE = 127 (“Not used”)
            Expected Result: Verify the following information,(1)   There is no symbol displayed on sub-area B7
            Test Step Comment: (1) MMI_gen 580;
            */

            XML_15_1_7(msgType.typeb);

            /*
            Test Step 3
            Action: Use the test script file 15_1_7_c.xml to send EVC-7 with, MMI_OBU_TR_M_MODE = 129 (“Not used”)
            Expected Result: Verify the following information,(1)   There is no symbol displayed on sub-area B7
            Test Step Comment: (1) MMI_gen 580;
            */

            XML_15_1_7(msgType.typec);

            /*
            Test Step 4
            Action: Use the test script file 15_1_7_d.xml to send EVC-7 with, MMI_OBU_TR_M_MODE = 255 (“Not used”)
            Expected Result: Verify the following information,(1)   There is no symbol displayed on sub-area B7
            Test Step Comment: (1) MMI_gen 580;
            */

            XML_15_1_7(msgType.typed);

            /*
            Test Step 5
            Action: Use the test script file 15_1_7_e.xml to send EVC-7 with, MMI_OBU_TR_M_MODE = 18 (“Not used”)
            Expected Result: Verify the following information,(1)   There is no symbol displayed on sub-area B7
            Test Step Comment: (1) MMI_gen 580;
            */

            XML_15_1_7(msgType.typee);

            /*
            Test Step 6
            Action: Use the test script file 15_1_7_f.xml to send EVC-7 with, MMI_OBU_TR_M_MODE = 126 (“Not used”)
            Expected Result: Verify the following information,(1)   There is no symbol displayed on sub-area B7
            Test Step Comment: (1) MMI_gen 580;
            */

            XML_15_1_7(msgType.typef);

            /*
            Test Step 7
            Action: Use the test script file 15_1_7_g.xml to send EVC-7 with, MMI_OBU_TR_M_MODE = 130 (“Not used”)
            Expected Result: Verify the following information,(1)   There is no symbol displayed on sub-area B7
            Test Step Comment: (1) MMI_gen 580;
            */

            XML_15_1_7(msgType.typeg);

            /*
            Test Step 8
            Action: Use the test script file 15_1_7_h.xml to send EVC-7 with, MMI_OBU_TR_M_MODE = 254 (“Not used”)
            Expected Result: Verify the following information,(1)   There is no symbol displayed on sub-area B7
            Test Step Comment: (1) MMI_gen 580;
            */

            XML_15_1_7(msgType.typeh);

            /*
            Test Step 9
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
        #region Send_XML_15_1_7_DMI_Test_Specification
        enum msgType
        {
            typea,
            typeb,
            typec,
            typed,
            typee,
            typef,
            typeg,
            typeh
        }

        private void XML_15_1_7(msgType type)
        {
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_EBTestInProgress = 0;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_EB_Status = 0;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_RadioStatus = 0;

            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_STM_HS_ENABLED = 0;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_STM_DA_ENABLED = 0;

            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_BrakeTest_Status =
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_BRAKETEST_STATUS.BrakeTestNotInProgress;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Level =
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_LEVEL.L0;
            
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_ADHESION = 0;
            EVC7_MMIEtcsMiscOutSignals.OBU_TR_NID_STM_HS = 0;
            EVC7_MMIEtcsMiscOutSignals.OBU_TR_NID_STM_DA = 0;
            EVC7_MMIEtcsMiscOutSignals.BRAKE_TEST_TIMEOUT = 0;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN = 10000000;

            SITR.ETCS1.EtcsMiscOutSignals.EVC7Validity1.Value = 4096;
            SITR.ETCS1.EtcsMiscOutSignals.EVC7Validity2.Value = 1;
            SITR.ETCS1.EtcsMiscOutSignals.EVC7SSW1.Value = 0;
            SITR.ETCS1.EtcsMiscOutSignals.EVC7SSW2.Value = 0;
            SITR.ETCS1.EtcsMiscOutSignals.EVC7SSW3.Value = 0;
            SITR.Client.Write("ETCS1_EtcsMiscOutSignals_SDT_UDV", 0);
            switch (type)
            {
                case msgType.typea:
                    SITR.ETCS1.EtcsMiscOutSignals.MmiObuTrMMode.Value = 17;
                    break;
                case msgType.typeb:
                    SITR.ETCS1.EtcsMiscOutSignals.MmiObuTrMMode.Value = 127;
                    break;
                case msgType.typec:
                    SITR.ETCS1.EtcsMiscOutSignals.MmiObuTrMMode.Value = 129;
                    break;
                case msgType.typed:
                    SITR.ETCS1.EtcsMiscOutSignals.MmiObuTrMMode.Value = 255;
                    break;
                case msgType.typee:
                    SITR.ETCS1.EtcsMiscOutSignals.MmiObuTrMMode.Value = 18;
                    break;
                case msgType.typef:
                    SITR.ETCS1.EtcsMiscOutSignals.MmiObuTrMMode.Value = 126;
                    break;
                case msgType.typeg:
                    SITR.ETCS1.EtcsMiscOutSignals.MmiObuTrMMode.Value = 130;
                    break;
                case msgType.typeh:
                    SITR.ETCS1.EtcsMiscOutSignals.MmiObuTrMMode.Value = 254;
                    break;
            }
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                      "There is no symbol displayed on sub-area B7.");

        }
        #endregion
    }
}
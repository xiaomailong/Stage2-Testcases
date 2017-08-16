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
using Testcase.XML;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 17.5.4 Circular Speed Gauge removal when received an invalid value of EVC-1 and EVC-7
    /// TC-ID: 12.5.4
    /// 
    /// This test case verifies that the circular speed gauge is removed refer to received packet EVC-1 and EVC-7 with an invalid value in specific variable.
    /// 
    /// Tested Requirements:
    /// MMI_gen 977;
    /// 
    /// Scenario:
    /// 1.Drive the train forward pass BG1 at position 100m to enter FS mode.       BG1: Packet 12, 21 and 27 (Entering FS)
    /// 2.Use the test script file to send an invalid value in packet EVC-1 and EVC-
    /// 7.Then, verify that the circular speed gauge is removed.
    /// 
    /// Used files:
    /// 12_5_4.tdg, 12_5_4_a.xml, 12_5_4_b.xml, 12_5_4_c.xml, 12_5_4_d.xml, 12_5_4_e.xml, 12_5_4_f.xml
    /// </summary>
    public class Circular_Speed_Gauge_removal_when_received_an_invalid_value_of_EVC_1_and_EVC_7 : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // System is power on.Cabin is activated.SoM is performed in SR mode, Level 1.

            // Call the TestCaseBase PreExecution
            base.PreExecution();

            DmiActions.Complete_SoM_L1_SR(this);
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in FS mode, Level 1.
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in FS mode, Level 1.");

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint

            EVC7_MMIEtcsMiscOutSignals.Initialise(this);
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.LimitedSupervision;

            EVC1_MMIDynamic.Initialise(this);

            /*
            Test Step 1
            Action: Drive the train forward pass BG1 with speed = 30km/h.Then, stop the train
            Expected Result: DMI displays in FS mode, Level 1
            */
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.FullSupervision;
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 30;
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in FS mode, Level 1.");

            EVC1_MMIDynamic.MMI_V_TRAIN = 0;

            /*
            Test Step 2
            Action: Use the test script file 12_5_4_a.xml to send EVC-1 with MMI_M_WARNING = 7
            Expected Result: Verify the following information,(1)   The Circular Speed Gauge is removed from sub-area B2.Note: The ciruclar speed guage is re-appear when DMI received packet EVC-1 from ETCS onboard
            Test Step Comment: (1) MMI_gen 977 (partly: MMI_M_WARNING);
            */
            XML_12_5_4_a.Send(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1.The Circular Speed Gauge is removed from sub-area B2.");

            EVC1_MMIDynamic.Initialise(this);       // sends a 'standard' EVC-1 packet so CSG should re-appear
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1.The Circular Speed Gauge re-appears in sub-area B2.");
            /*
            Test Step 3
            Action: Use the test script file 12_5_4_b.xml to send EVC-1 with,MMI_V_TARGET = 11112
            Expected Result: Verify the following information,(1)   The Circular Speed Gauge is removed from sub-area B2.Note: The ciruclar speed guage is re-appear when DMI received packet EVC-1 from ETCS onboard
            Test Step Comment: (1) MMI_gen 977 (partly: MMI_V_TARGET);
            */
            XML_12_5_4_b.Send(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1.The Circular Speed Gauge is removed from sub-area B2.");

            EVC1_MMIDynamic.Initialise(this);       // sends a 'standard' EVC-1 packet so CSG should re-appear
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1.The Circular Speed Gauge re-appears in sub-area B2.");

            /*
            Test Step 4
            Action: Use the test script file 12_5_4_c.xml to send EVC-1 with,MMI_V_PERMITTED = 11112
            Expected Result: Verify the following information,(1)   The Circular Speed Gauge is removed from sub-area B2.Note: The ciruclar speed guage is re-appear when DMI received packet EVC-1 from ETCS onboard
            Test Step Comment: (1) MMI_gen 977 (partly: MMI_V_PERMITTED);
            */
            XML_12_5_4_c.Send(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1.The Circular Speed Gauge is removed from sub-area B2.");


            EVC1_MMIDynamic.Initialise(this);       // sends a 'standard' EVC-1 packet so CSG should re-appear
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1.The Circular Speed Gauge re-appears in sub-area B2.");

            /*
            Test Step 5
            Action: Use the test script file 12_5_4_d.xml to send EVC-1 with,MMI_V_INTERVENTION = 11112
            Expected Result: Verify the following information,(1)   The Circular Speed Gauge is removed from sub-area B2.Note: The ciruclar speed guage is re-appear when DMI received packet EVC-1 from ETCS onboard
            Test Step Comment: (1) MMI_gen 977 (partly: MMI_V_INTERVENTION);
            */
            XML_12_5_4_d.Send(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1.The Circular Speed Gauge is removed from sub-area B2.");


            EVC1_MMIDynamic.Initialise(this);       // sends a 'standard' EVC-1 packet so CSG should re-appear
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1.The Circular Speed Gauge re-appears in sub-area B2.");

            /*
            Test Step 6
            Action: Use the test script file 12_5_4_e.xml to send EVC-1 with,MMI_V_RELEASE = 11112
            Expected Result: Verify the following information,(1)   The Circular Speed Gauge is removed from sub-area B2.Note: The ciruclar speed guage is re-appear when DMI received packet EVC-1 from ETCS onboard
            Test Step Comment: (1) MMI_gen 977 (partly: MMI_V_RELEASE);
            */
            XML_12_5_4_e.Send(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1.The Circular Speed Gauge is removed from sub-area B2.");


            EVC1_MMIDynamic.Initialise(this);       // sends a 'standard' EVC-1 packet so CSG should re-appear
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1.The Circular Speed Gauge re-appears in sub-area B2.");

            /*
            Test Step 7
            Action: Use the test script file 12_5_4_f.xml to send EVC-7 with,OBU_TR_M_MODE = 17
            Expected Result: Verify the following information,(1)   The Circular Speed Gauge is removed from sub-area B2.Note: The ciruclar speed guage is re-appear when DMI received packet EVC-7 from ETCS onboard
            Test Step Comment: (1) MMI_gen 977 (partly: OBU_TR_M_MODE);
            */
            // CSG needs to be displayed?? 
            //DmiActions.Complete_SoM_L1_FS(this);

            XML_12_5_4_f.Send(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1.The Circular Speed Gauge is removed from sub-area B2.");

            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.FullSupervision; 

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1.The Circular Speed Gauge re-appears in sub-area B2.");

            /*
            Test Step 8
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
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
    /// 17.7.4 Release Speed Digital: Release speed removal when received an invalid value of EVC-1 or EVC-7
    /// TC-ID: 12.7.4
    /// 
    /// This test case is verifies the display of release speed which removed when DMI received an invalid value of EVC-1 or EVC-7.
    /// 
    /// Tested Requirements:
    /// MMI_gen 6587;
    /// 
    /// Scenario:
    /// 1.Drive the train forward pass BG1 at position 100m.BG1: Packet 12, 21, 27 (Entering FS)
    /// 2.Use the test script file to send an invalid value of EVC-1 and EVC-7.Then, verify that reease speed digital is removed.
    /// 
    /// Used files:
    /// 12_7_4.tdg, 12_7_4_a.xml, 12_7_4_b.xml
    /// </summary>
    public class
        Release_Speed_Digital_Release_speed_removal_when_received_an_invalid_value_of_EVC_1_or_EVC_7 : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // System is power on.Cabin is activated.SoM is performed in SR mode, level 1.

            // Call the TestCaseBase PreExecution
            base.PreExecution();
            DmiActions.Complete_SoM_L1_SR(this);
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in FS mode, level 1.
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in FS mode, Level 1.");

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint

            /*
            Test Step 1
            Action: Drive the train forward pass BG1.Then, stop the train
            Expected Result: DMI displays in FS mode, level 1
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 5;

            // Simulate the train moving so the 'stop' should be discernible
            System.Threading.Thread.Sleep(5000);
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 0;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.FullSupervision;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in FS mode, Level 1.");

            // Call generic Check Results Method
            DmiExpectedResults.DMI_displays_in_FS_mode_level_1(this);

            /*
            Test Step 2
            Action: Use the test script file 12_7_4_a.xml to send EVC-1 with,MMI_M_WARNING = 7
            Expected Result: Verify the following information,(1)   The release speed in sub-area B2 and B6 are removed from the DMI.(2)   After test scipt file is executed, the release speed in sub-area B2 and B6 are re-appear refer to received packet EVC-1 from ETCS Onboard
            Test Step Comment: (1) MMI_gen 6587 (partly: MMI_M_WARNING is invalid);(2) MMI_gen 6587 (partly: toggle function is reset to default state);
            */
            XML_12_7_4_a.Send(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The release speed in sub-area B2 and B6 are removed from the DMI.");

            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Indication_Status_Release_Speed_Monitoring;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The release speed in sub-area B2 and B6 are re-displayed");
            /*
            Test Step 3
            Action: Use the test script file 12_7_4_b.xml to send EVC-7 with,OBU_TR_M_MODE = 17
            Expected Result: Verify the following information,(1)   The release speed are sub-area B2 and B6 is removed from the DMI.(2)   After test scipt file is executed, the release speed in sub-area B2 and B6 is are re-appear refer to received packet EVC-7 from ETCS Onboard
            Test Step Comment: (1) MMI_gen 6587 (partly: OBU_TR_M_MODE is invalid);(2) MMI_gen 6587 (partly: toggle function is reset to default state);
            */
            XML_12_7_4_b.Send(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The release speeds in sub-area B2 and B6 are removed from the DMI.");

            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.FullSupervision;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The release speeds in sub-area B2 and B6 are re-displayed.");

            /*
            Test Step 4
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
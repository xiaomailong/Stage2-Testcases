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
    /// 18.1.7.4 Distance to Target: Appearance of Distance to Target in LS, TR and PT mode
    /// TC-ID: 13.1.7.4
    /// 
    /// This test case verifies the display information of the distance to target bar and digital in LS, TR and PT mode. The display of distance to target bar and digital are comply with the received packet EVC-1 and EVC-7.  
    /// 
    /// Tested Requirements:
    /// MMI_gen 2567 (partly: LS mode, TR mode, PH mode); MMI_gen 107 (partly: Table 37, LS mode, TR mode, PT mode); MMI_gen 6658; MMI_gen 6774;
    /// 
    /// Scenario:
    /// 1.Drive the train forward pass BG1 at position 100m. Then, press an acknowledgement of LS mode and verify the display of distance to target bar and digital bar with received packet information EVC-1 and EVC-7.BG1: Packet 12, 21, 27 and 80 (Entering LS)
    /// 2.Continue to drive the train forward pass through the movement authority (300m) to entering TR mode. Then, verify the display of distance to target bar and digital with received packet.
    /// 3.Ackownledge PT mode . Then, verify the display of distance to target bar and digital with received packet.
    /// 
    /// Used files:
    /// 13_1_7_4.tdg
    /// </summary>
    public class TC_13_1_7_4_Brake : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // Call the TestCaseBase PreExecution
            base.PreExecution();

            // System is powered on.Cabin is activated.SoM is performed in SR mode, level 1
            DmiActions.Complete_SoM_L1_SR(this);
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in PT mode, Level 1
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in PT mode, Level 1.");

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint

            /*
            Test Step 1
            Action: Drive the train forward pass BG1. Then, press the LS mode acknowledgement in sub-area C1
            Expected Result: DMI displays in LS mode, level 1Verify the following information(1)    Use the log file to confirm that DMI receives the following packets information with a specific value,  EVC-7: OBU_TR_M_MODE = 12 (LS mode) (2)   The distance to target bar is not display in sub-area A3. (3)   The distance to target digital is not display in sub-area A2.(4)   Use the log file to confirm that DMI receives the packet EVC-1 with variable MMI_O_BRAKETARGET = -1 (Default)
            Test Step Comment: (1) MMI_gen 107 (partly: MMI_M_WARNING, OBU_TR_M_MODE, LS mode); MMI_gen 2567 (partly: MMI_M_WARNING, OBU_TR_M_MODE, LS mode);(2) MMI_gen 6658 (partly: not be shown); MMI_gen 107 (partly: Table 37, LS mode);(3) MMI_gen 2567 (partly: Table 38, LS mode); MMI_gen 6774 (partly: not be shown);(4) MMI_gen 6658 (partly: MMI_O_BRAKETARGET is less than zero); MMI_gen 6774 (partly: MMI_O_BRAKETARGET is less than zero);
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 10;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN = 10000;      // 100m
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.LimitedSupervision;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Does the DMI delete the SR mode symbol (MO09) and replace it with the LS mode symbol (MO21) in area B7?");

            DmiActions.ShowInstruction(this, "Acknowledgement LS mode by pressing in sub-area C1");

            EVC1_MMIDynamic.MMI_O_BRAKETARGET = -1;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The distance to target bar is not displayed in sub-area A3." + Environment.NewLine +
                                "2. The digital distance to target is not displayed in sub-area A2.");

            /*
            Test Step 2
            Action: Force the train into TR mode by moving the train forward to position of EOA.Then, stop the train
            Expected Result: DMI displays in TR mode, level 1.Verify the following information,(1)   Use the log file to confirm that DMI received the EVC-7 with variable OBU_TR_M_MODE = 7 (Trip)(2)   The distance to target bar is not display in sub-area A3. (3)   The distance to target digital is not display in sub-area A2.(4)   Use the log file to confirm that DMI receives the packet EVC-1 with variable MMI_O_BRAKETARGET = -1 (Default)
            Test Step Comment: (1) MMI_gen 107 (partly: MMI_M_WARNING, OBU_TR_M_MODE, TR mode);(2) MMI_gen 6658 (partly: not be shown); MMI_gen 107 (partly: Table 37, TR mode);(3) MMI_gen 2567 (partly: Table 38, TR mode); MMI_gen 6774 (partly: not be shown);(4) MMI_gen 6658 (partly: MMI_O_BRAKETARGET is less than zero); MMI_gen 6774 (partly: MMI_O_BRAKETARGET is less than zero);
            */
            EVC1_MMIDynamic.MMI_O_BRAKETARGET = 30000;                  // EOA
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN = 30500;      // past EOA
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 0;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.Trip;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Does the DMI delete the LS mode symbol (MO21) and replace it with the TR mode symbol (MO03) in area B7?" + Environment.NewLine + Environment.NewLine +
                                "2. The distance to target bar is not displayed in sub-area A3." + Environment.NewLine +
                                "3. The digital distance to target is not displayed in sub-area A2.");

            /*
            Test Step 3
            Action: Press the  PT mode acknowledgement in sub-area C1
            Expected Result: DMI displays in PT mode, level 1.Verify the following information,(1)   Use the log file to confirm that DMI received the EVC-7 with variable OBU_TR_M_MODE = 8 (Post trip)(2)   The distance to target bar is not display in sub-area A3. (3)   The distance to target digital is not display in sub-area A2.(4)   Use the log file to confirm that DMI receives the packet EVC-1 with variable MMI_O_BRAKETARGET = -1 (Default)
            Test Step Comment: (1) MMI_gen 107 (partly: MMI_M_WARNING, OBU_TR_M_MODE, PT mode);(2) MMI_gen 6658 (partly: not be shown); MMI_gen 107 (partly: Table 37, PT mode);(3) MMI_gen 2567 (partly: Table 38, PT mode); MMI_gen 6774 (partly: not be shown);(4) MMI_gen 6658 (partly: MMI_O_BRAKETARGET is less than zero); MMI_gen 6774 (partly: MMI_O_BRAKETARGET is less than zero);
            */
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.PostTrip;

            DmiActions.ShowInstruction(this, "Acknowledgement PT mode by pressing in sub-area C1");
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Does the DMI delete the TR mode symbol (MO03) and replace it with the PT mode symbol (MO06) in area B7?" + Environment.NewLine + Environment.NewLine +
                                "2. The distance to target bar is not displayed in sub-area A3." + Environment.NewLine +
                                "3. The digital distance to target is not displayed in sub-area A2.");

            /*
            Test Step 4
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
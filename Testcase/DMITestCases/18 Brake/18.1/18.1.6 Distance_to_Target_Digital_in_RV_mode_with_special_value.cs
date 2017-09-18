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
    /// 18.1.6 Distance to Target Digital in RV mode with special value
    /// TC-ID: 13.1.6
    /// 
    /// This test case verifies the presentation of the distance to target digital in RV mode when received the special value.
    /// 
    /// Tested Requirements:
    /// MMI_gen 6777; 
    /// 
    /// Scenario:
    /// 1.Drive the train forward pass BG1 at position 50m       BG1: packet 12, 21 and 27 (Entering FS)
    /// 2.Drive the train forward pass BG2 at position 200m.        BG2 packet138: D_STARTREVERSE 100, L_REVERSEAREA 400               packet139: D_REVERSE 32767, V_REVERSE 30      
    /// 3.Stop the train at position 700m.
    /// 4.Select and confirm reversing mode.
    /// 5.Drive the train backward and verify the display of distance to target on DMI.
    /// 
    /// Used files:
    /// 13_1_6.tdg
    /// </summary>
    public class TC_13_1_6_Brake : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:

            // Call the TestCaseBase PreExecution
            base.PreExecution();
            // Test system is power on.Cabin is activatedStart of Mission is completed in SR mode, level1 (set train length = 100m)
            DmiActions.Start_ATP();

            EVC2_MMIStatus.TrainRunningNumber = 1;
            EVC2_MMIStatus.MMI_M_ACTIVE_CABIN = Variables.MMI_M_ACTIVE_CABIN.Cabin1Active;
            EVC2_MMIStatus.MMI_M_ADHESION = 0x0;
            EVC2_MMIStatus.MMI_M_OVERRIDE_EOA = false;
            EVC2_MMIStatus.Send();

            EVC6_MMICurrentTrainData.MMI_L_TRAIN = 100;             // Set train length (m)
            EVC6_MMICurrentTrainData.Send();

            DmiActions.Set_Driver_ID(this, "1234");

            // Set to level 1 and SR mode
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Level = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_LEVEL.L1;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.StaffResponsible;

            // Enable standard buttons including Start, and display Default window.
            DmiActions.FinishedSoM_Default_Window(this);
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in RV mode, level 1.
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in RV mode, Level 1.");

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint

            /*
            Test Step 1
            Action: Drive the train forward passing BG1 with speed = 40 km/h until entering FS mode
            Expected Result: 
            */  // ?? Set an EOA so the DMI can display a target
            EVC1_MMIDynamic.MMI_O_BRAKETARGET = 300000;              // 3 km: will cause the target display to show a white arrow on top
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN = 0;       // just starting off

            // Set the permitted speed so the current speed is allowed
            EVC1_MMIDynamic.MMI_V_PERMITTED_KMH = 70;
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 40; 
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN = 5000;   // 50m

            /*
            Test Step 2
            Action: Continue drive the train forward passing BG2
            Expected Result: 
            */
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN = 20000;

            /*
            Test Step 3
            Action: The train is in reversing area
            Expected Result: 
            */
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN = 30000;
            EVC1_MMIDynamic.MMI_O_BRAKETARGET = 70000;               // in reversing area can travel 400m further ??

            /*
            Test Step 4
            Action: Stop the train
            Expected Result: The train is standstill.Driver is informed that reversing is possible
            */
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 5;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 286;    // Reversing possible
            EVC8_MMIDriverMessage.Send();

            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 0;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI indicates speed = 0 km/h" + Environment.NewLine +
                                "2. DMI displays message that reversing is possible and displays symbol ST06 in sub-area C6");

            /*
            Test Step 5
            Action: Change the direction of train to reverse. Then select and confirm RV mode
            Expected Result: DMI displays in RV mode, level 1.Verify the following information,(1)    Use the log file to confirm that DMI received packet EVC-1 with variable MMI_O_BRAKETARGET = 2147483647(2)    The symbol infinity '∞' is displayed for distance to target digital in sub-area A2.(3)    The symbol is be horizontally and vertically centered in Sub-Area A2
            Test Step Comment: (1) MMI_gen 6777  (partly: receive MMI_O_BRAKETARGET  equal special value);                   (2) MMI_gen 6777 (partly: when MMI_O_BRAKETARGET  equal special value); (3) MMI_gen 6777 (partly: horizontally and vertically centered);
            */
            DmiActions.ShowInstruction(this, "Change the direction of train to reverse. Select and confirm RV mode.");

            EVC1_MMIDynamic.MMI_O_BRAKETARGET = 2147483647;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.Reversing;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in RV mode, level 1." + Environment.NewLine +
                                "2. The  infinity symbol ‘∞’ is displayed for digital distance to target in sub-area A2, horizontally and vertically centered.");
            
            /*
            Test Step 6
            Action: End of test
            Expected Result: 
            */


            return GlobalTestResult;
        }
    }
}
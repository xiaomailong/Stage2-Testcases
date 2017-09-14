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
    /// 17.3.9 Speed Pointer: Colour of speed pointer in SB mode and NL mode
    /// TC-ID: 12.3.9
    /// 
    /// This test case verifies the colour of speed pointer which display refer to received packet EVC-1 and EVC-7 for SB mode and NL mode.
    /// 
    /// Tested Requirements:
    /// MMI_gen 6299 (partly: SB mode, NL mode);
    /// 
    /// Scenario:
    /// 1.Force the train roll away in SB mode. Then, verify that the colour of speed pointer is always grey.
    /// 2.Enter NL mode, level 
    /// 1.Then, drive the train with maximum speed and verify that the colour of speed pointer is always grey.
    /// 
    /// Used files:
    /// N/A
    /// </summary>
    public class TC_12_3_9_Train_Speed : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // Test system is powered on.Cabin is activated.SoM is performed in SB mode, Level 1.Main window is closed.
            EVC0_MMIStartATP.Evc0Type = EVC0_MMIStartATP.EVC0Type.GoToIdle;
            EVC0_MMIStartATP.Send();

            // Set train running number, cab 1 active, and other defaults
            DmiActions.Activate_Cabin_1(this);

            // Set driver ID
            DmiActions.Set_Driver_ID(this, "1234");

            // Set to level 1 and SR mode
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Level = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_LEVEL.L1;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.StandBy;

            // Do we need to close any open windows?
            // EVC30_MMIRequestEnable.SendBlank();

            // Call the TestCaseBase PreExecution
            base.PreExecution();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in NL mode, level 1
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in NL mode, Level 1.");

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint

            /*
            Test Step 1
            Action: Drive the train forward with speed = 10 km/h
            Expected Result: Verify the following information,(1)   The speed pointer is always display in grey colour even runaway movement is detected.(2)   Use the log file to confirm that DMI received packet EVC-7 with variable OBU_TR_M_MODE = 6 (Standby)
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, SB mode);(2) MMI_gen 6299 (partly: OBU_TR_M_MODE);
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 10;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Is the speed pointer grey?");

            /*
            Test Step 2
            Action: Stop the train.Then, perform the following procedure,Press on sub-area C9.Press ‘Main’ buttonForce the simulation to ‘Non-leading’Press and hold ‘Non-Leading’ button at least 2 second.Release the pressed button
            Expected Result: DMI displays in NL mode, level 1
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 10;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.NonLeading;

            WaitForVerification("Press on sub-area C9. Press ‘Main’ button. Force the simulation to ‘Non - leading’" + Environment.NewLine +
                                "Press and hold ‘Non - Leading’ button at least 2 second. Release the pressed button and check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in NL mode, level 1.");

            /*
            Test Step 3
            Action: Drive the train with speed = 400 km/h (Maximum speed range of speed dial)
            Expected Result: Verify the following information,(1)   The speed pointer is always display in grey colour..(2)   Use the log file to confirm that DMI received packet EVC-7 with variable OBU_TR_M_MODE = 11 (Non-leading)
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, NL mode);(2) MMI_gen 6299 (partly: OBU_TR_M_MODE);
            */

            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 400;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Is the speed pointer grey?");

            /*
            Test Step 4
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
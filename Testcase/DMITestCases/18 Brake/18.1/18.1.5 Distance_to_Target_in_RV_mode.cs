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
    /// 18.1.5 Distance to Target in RV mode
    /// TC-ID: 13.1.5
    /// 
    /// This test case verifies the presentation of the distance to target digital and the distance to target bar when DMI is displayed in RV mode.
    /// 
    /// Tested Requirements:
    /// MMI_gen 105 (partly: RV mode); MMI_gen 2567 (partly: RV mode); MMI_gen 107 (partly: ETCS mode, Table 37, RV mode);
    /// 
    /// Scenario:
    /// Activate cabin A. Performs SoM to SR mode, Level 1.Note: Set the train length to 100m during train data entry process.Drive the train forward pass BG1 at position 50mBG1: packet 12, 21 and 27Drive the train forward pass BG2 at position 200mBG2 Packet138: D_STARTREVERSE 100, L_REVERSEAREA 400        Packet139: D_REVERSE 200, V_REVERSE 30Stop the train at position 700m.Driver selects reversing mode and confirmmode change to RV mode.Drive the train backward and verify the display of distance to target on DMI.
    /// 
    /// Used files:
    /// 13_1_5.tdg
    /// </summary>
    public class Distance_to_Target_in_RV_mode : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:

            // Call the TestCaseBase PreExecution
            base.PreExecution();
            // System is power on.
            EVC0_MMIStartATP.Evc0Type = EVC0_MMIStartATP.EVC0Type.GoToIdle;
            EVC0_MMIStartATP.Send();
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
            Action: Activate cabin A
            Expected Result: DMI displays in SB mode. The Driver ID window is displayed
            */
            // Call generic Action Method
            DmiActions.Activate_Cabin_1(this);
            // Call generic Check Results Method
            DmiExpectedResults.DMI_displays_Driver_ID_window_in_SB_mode(this);

            /*
            Test Step 2
            Action: Driver performs SoM to SR mode, Level 1.Note: Please set Train length = 100m during train data entry process
            Expected Result: DMI displays in SR mode, level 1
            */
            DmiActions.ShowInstruction(this, "Perform SoM to SR mode, level 1: setting Train Length = 100m in train data entry");

            // Call generic Check Results Method
            DmiExpectedResults.SR_Mode_displayed(this);

            /*
            Test Step 3
            Action: Drive the train forward passing BG1Then drive the train forward  with speed = 40 km/h in FS mode
            Expected Result: DMI changes from SR to FS mode
            */

            // ?? Set an EOA so the DMI can display a target
            EVC1_MMIDynamic.MMI_O_BRAKETARGET = 300000;              // 3 km: will cause the target display to show a white arrow on top
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN = 0;       // just starting off

            // Set the permitted speed so the current speed is allowed
            EVC1_MMIDynamic.MMI_V_PERMITTED_KMH = 10;
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 5;
            
            EVC1_MMIDynamic.MMI_V_PERMITTED_KMH = 70;
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 40;

            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN = 5000;   // 50m

            // Call generic Check Results Method
            DmiExpectedResults.DMI_changes_from_SR_to_FS_mode(this);

            /*
            Test Step 4
            Action: Driving forward passing BG2
            Expected Result: 
            */
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN = 20000;   // 200m

            // ??? No observation: still in FS mode maybe

            /*
            Test Step 5
            Action: The train is in reversing area
            Expected Result: DMI remains displays in FS mode
            */
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN = 30000;   // 300m
            EVC1_MMIDynamic.MMI_O_BRAKETARGET = 70000;               // in reversing area can travel 400m further ??

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI still displays the FS mode symbol (MO11) in area B7");

            /*
            Test Step 6
            Action: Stop the train
            Expected Result: The train is at standstill.Driver is informed that reversing is possible
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
            Test Step 7
            Action: Change the direction of train to reverse. Then select and confirm RV mode
            Expected Result: DMI displays in RV mode, level 1.Verify the following information,Use the log file to confirm that the distance to target (bar and digital) is calculated from the received packet information EVC-7 and EVC-1 as follows,(EVC-7) OBU_TR_O_TRAIN – (EVC-1) MMI_O_BRAKE_TARGETExample: The observation point of the distance target is 407. [EVC-7.OBU_TR_O_TRAIN = 1000080700] – [EVC-1.MMI_O_BRAKETARGET = 1000040036] = 40664 (406.64 m)Use the log file to confirm that the distance to target bar is display when DMI received packet information EVC-7 with, OBU_TR_M_MODE = 14
            Test Step Comment: (1)MMI_gen 105           (partly: RV mode);                        (2) MMI_gen 2567 (partly RV mode); MMI_gen 107 (partly: ETCS mode, Table 37, RV mode);
            */
            DmiActions.ShowInstruction(this, "Change the direction of train to reverse. Select and confirm RV mode.");

            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.Reversing;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in RV mode, level 1.");

            /*
            Test Step 8
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
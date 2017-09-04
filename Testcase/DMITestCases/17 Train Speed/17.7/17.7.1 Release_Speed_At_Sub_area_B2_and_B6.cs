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
    /// 17.7.1 Release Speed: At Sub-area B2 and B6
    /// TC-ID: 12.7.1
    /// 
    /// This test case verifies the presentation of Release Speed and Relesae Speed digital in sub-area B2 and B6. The presentation of Release Speed shall comply with the requirement in chapter 7.2.1.7 of MMI Generic Requirements and [ERA] standard.
    /// 
    /// Tested Requirements:
    /// MMI_gen 9967; MMI_gen 6460; MMI_gen 6465; MMI_gen 6468 (partly: FS); MMI_gen 9969; MMI_gen 9970 (partly: outer part of CSG, separated from permitted speed);
    /// 
    /// Scenario:
    /// Activate Cabin A.Perform SoM in SR mode, Level 1.Drive the train forward pass BG1.BG1: Packet 12, 21 and 27Drive the train with speed = 45 km/h and verify the display information when entering release speed monitoring.Stop the train
    /// 
    /// Used files:
    /// 12_7_1.tdg
    /// </summary>
    public class Release_Speed_At_Sub_area_B2_and_B6 : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // System is power on.

            // Call the TestCaseBase PreExecution
            base.PreExecution();
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
            Action: Activate cabin A
            Expected Result: DMI displays in SB mode, level 1. The Driver ID window is displayed
            */
            // Call generic Action Method
            DmiActions.Activate_Cabin_1(this);
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.StandBy;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in SB mode, level 1." + Environment.NewLine +
                                "2. The Driver ID window is displayed.");

            /*
            Test Step 2
            Action: Driver performs SoM to SR mode
            Expected Result: DMI is displayed in SR mode, level 1
            */
            // ?????
            WaitForVerification("Perform SoM to SR mode, level 1 and check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in SR mode, level 1.");

            /*
            Test Step 3
            Action: Drive the train forward passing BG1
            Expected Result: The DMI changes from SR to FS mode
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 5;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.FullSupervision;
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The DMI changes from SR to FS mode.");

            /*
            Test Step 4
            Action: Driving the train with speed equal to 45 km/h
            Expected Result: Verify the following information,Verify that the release speed digital is displayed centred in sub-area B6 without leading zeros. The graphical presentation of release speed digital is displayed in area B2. (see the figure 1 and figure 2 in ‘Comment’ column)Use the log file to confirm that the appearance of the release speed digital is controlled by data packet from ETCS Onboard as follows,EVC-7: OBU_TR_M_MODE = 0 (FS Mode)EVC-1: MMI_M_WARNING = 15 (Supervision = Release speed monitoring)EVC-1: MMI_V_RELEASE = 1111 (~40 km/h)The Relaese speed is displayed at the outer part of CSG. (see the figure 2 in ‘Comment’ column)The Relaese speed is separated from the permitted speed. (see the figure 2 in ‘Comment’ column)When a Release speed exists, the presentation is displayed on the CSG according to table 33 (Speed monitoring is RSM)When a Release speed exists, the release speed digital is displayed as a numeric in medium grey colour
            Test Step Comment: (1) MMI_gen 6460;                 (2) MMI_gen 9967;          (3) MMI_gen 6468 (FS);           (4) MMI_gen 9970 (partly: outer part of CSG);                           (5) MMI_gen 9970 (partly: separated from permitted speed);                                      (6) MMI_gen 9969;                          (7) MMI_gen 6465;                                  Figure 1Figure 2
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 45;
            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Intervention_Status_Indication_Status_Release_Speed_Monitoring;
            EVC1_MMIDynamic.MMI_V_RELEASE = 1111;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The digital release speed  is centred in sub-area B6 without leading zeros." + Environment.NewLine +
                                "2) The graphical presentation of digital release speed  is displayed in area B2. (Refer to specification)" + Environment.NewLine +
                                "3. The Release speed is displayed at the outer part of CSG." + Environment.NewLine +
                                "4. The Release speed is separated from the permitted speed." + Environment.NewLine +
                                "5. When a Release speed exists, the presentation is displayed on the CSG according to table 33 (Speed monitoring is RSM)" + Environment.NewLine +
                                "6. When a Release speed exists, the release speed digital is displayed as a number in medium-grey");

            /*
            Test Step 5
            Action: Stop the train
            Expected Result: Train is standstill
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 0;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "Speed is displayed as 0 km/h");

            /*
            Test Step 6
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
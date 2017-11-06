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
    /// 22.9.1 Hide PA Function: General appearance
    /// TC-ID: 17.9.1
    /// 
    /// This test case verifies the general appearance of Hide PA function that operable in planning area. When driver presses NA01 symbol to activate show or hide the planning information function. The Hide PA function shall comply with [ERA-ERTMS] standard.
    /// 
    /// Tested Requirements:
    /// MMI_gen 7336; MMI_gen 7352 (partly: Set); MMI_gen 2996 (partly: 1st bullet, activation of ‘Hide’ and ‘Show’ button); MMI_gen 7339 (partly: Update); MMI_gen 7350 (partly: symbol NA01); MMI_gen 7353 (partly: Reset); MMI_gen 7349; MMI_gen 6962;      
    /// 
    /// Scenario:
    /// Active cabin A. Perform SoM to SR mode, level 1Start driving the train forward and Pass BG
    /// 1.Mode changes to FS mode
    /// 
    /// Used files:
    /// 17_9_1.tdg
    /// </summary>
    public class TC_ID_17_9_1_Hide_PA_Function_General_appearance : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // Set the following tags name in configuration file (See the instruction in Appendix 1)
            // HIDE_PA_FUNCTION = 0 (‘ON’ state) 

            // Call the TestCaseBase PreExecution
            base.PreExecution();

            // System is power ON.
            DmiActions.Start_ATP();
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
            Action: Activate cabin A
            Expected Result: DMI displays Driver ID window
            */            
            DmiActions.Activate_Cabin_1(this);            
            DmiActions.Set_Driver_ID(this, "1234");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine + 
                                "1. DMI displays the Driver ID window .");
            
            /*
            Test Step 2
            Action: Perform SoM to SR mode, level 1
            Expected Result: DMI displays in SR mode, level 1
            */
            // Tested elsewhere: force to SR mode/Level 1
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Level = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_LEVEL.L1;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.StaffResponsible;
            DmiActions.Finished_SoM_Default_Window(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in SR mode, Level 1.");

            /*
            Test Step 3
            Action: Drive the train forward with speed = 40 km/h pass BG1
            Expected Result: DMI displays in FS mode, level 1. The Planning area is displayed the planning information in main area D
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 40;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN = 10000;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.FullSupervision;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in FS mode, Level 1." + Environment.NewLine +
                                "2. The Planning Area is displayed in area D.");

            /*
            Test Step 4
            Action: Press the ‘NA01’ symbol in sub-area D14
            Expected Result: Verify the following information, The Planning area is disappeared from DMI.Use log file to verify tha DMI still received packet EVC-4 from ETCS Onboard
            Test Step Comment: (1) MMI_gen 7336 (partly: Hide); MMI_gen 7352 (partly: Set); MMI_gen 2996        (partly: 1st bullet, activation of ‘Hide’ button); MMI_gen 7339         (partly: Update);  (2) MMI_gen 6962 (partly: continuously updated objects in the background);         
            */
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN = 20000;
            DmiActions.ShowInstruction(this, @"Press the Hide planning information symbol, ‘NA01’, in sub-area D14");
            
            // Need to show that when hidden DMI is not responsive to EVC4
            EVC4_MMITrackDescription.MMI_G_GRADIENT_CURR = 0;
            EVC4_MMITrackDescription.MMI_V_MRSP_CURR_KMH = 40;
            EVC4_MMITrackDescription.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The Planning Area is removed from area D.");

            /*
            Test Step 5
            Action: Press at sensitive area in main area D
            Expected Result: Verify the following information, When driver presses main area D in sensitive area. The planning area is reappeared by this activation.NA01 symbol is still display in sub-area D14.All objects on the Planning area is updated according to the packet that sent from ETCS OB.Use log file to verify tha DMI still received packet EVC-4 from ETCS Onboard
            Test Step Comment: (1) MMI_gen 7336 (partly: Show); MMI_gen 7353 (partly: Reset);     MMI_gen 7349;     MMI_gen 2996        (partly: 1st bullet, activation of ‘Show’ button);   (2) MMI_gen 7350 (partly: symbol NA01);(3) MMI_gen 6962 (partly: updated in the background);(4) MMI_gen 6962 (partly: continuously be updated);
            */
            DmiActions.ShowInstruction(this, @"Press in the sensitive area in main area D");

            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN = 30000;
            EVC4_MMITrackDescription.TrackDescriptions = new List<TrackDescription>
            { new TrackDescription {MMI_O_MRSP =  40000, MMI_O_GRADIENT = 20000, MMI_V_MRSP_KMH = 35} };
            EVC4_MMITrackDescription.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The Planning Area is re-displayed in area D." + Environment.NewLine +
                                "2. The Hide planning information symbol, ‘NA01’, is still displayed in sub-area D14"  + Environment.NewLine +
                                "3. The PA is updated.");

            /*
            Test Step 6
            Action: Press the ‘NA01’ symbol in sub-area D14
            Expected Result: Verify the following information, (1)   The Planning area is disappeared from DMI.(2)   Use log file to verify tha DMI still received packet EVC-4 from ETCS Onboard
            Test Step Comment: (1) MMI_gen 6962 (partly: hidden objects);(2) MMI_gen 6962 (partly: continuously updated objects in the background);         
            */
            DmiActions.ShowInstruction(this, @"Press the ‘NA01’ symbol in sub-area D14");

            EVC4_MMITrackDescription.MMI_G_GRADIENT_CURR = 10;
            EVC4_MMITrackDescription.MMI_V_MRSP_CURR_KMH = 30;
            EVC4_MMITrackDescription.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The Planning Area is removed from area D.");

            /*
            Test Step 7
            Action: Press at sensitive area in main area D to display the Planning area
            Expected Result: The Planning area is reappeared in area D.Verify the following information, (1)   Use the log file to confirm that all objects on the Planning area are updated according to the received packet EVC-4 from ETCS Onboard
            Test Step Comment: (1) MMI_gen 6962;
            */
            DmiActions.ShowInstruction(this, @"Press in the sensitive area in main area D to re-display the Planning Area.");
            
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN = 30200;
            EVC4_MMITrackDescription.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The Planning Area is re-displayed in area D." + Environment.NewLine +
                                "2. The PA is updated.");

            /*
            Test Step 8
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
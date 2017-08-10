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
    /// 17.3.5 Speed Pointer: Colour of speed pointer in SH mode
    /// TC-ID: 12.3.6
    /// 
    /// This test case verifies the colour of speed pointer which display refer to received packet EVC-1 while the train is running in each supervision status and speed monitoring for SH mode.
    /// 
    /// Tested Requirements:
    /// MMI_gen 6299 (partly: SH mode); 
    /// 
    /// Scenario:
    /// 1.Enter SH mode, level 
    /// 1.Then, drive the train with specify speed and verify the display of speed pointer refer to received packet EVC-1.
    /// 
    /// Used files:
    /// N/A
    /// </summary>
    public class Speed_Pointer_Colour_of_speed_pointer_in_SH_mode : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // Test system is power on.Cabin is activated.Drive ID is entered and brake test in performed.Level 1 is selected and confirmed.

            EVC0_MMIStartATP.Evc0Type = EVC0_MMIStartATP.EVC0Type.GoToIdle;
            EVC0_MMIStartATP.Send();

            // Set train running number, cab 1 active, and other defaults
            DmiActions.Activate_Cabin_1(this);

            // Set driver ID
            DmiActions.Set_Driver_ID(this, "1234");

            // Set to level 1 and SR mode
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Level = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_LEVEL.L1;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.Shunting;

            // Enable standard buttons including Start, and display Default window.
            DmiActions.FinishedSoM_Default_Window(this);

            // Call the TestCaseBase PreExecution
            base.PreExecution();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in SH mode, level 1

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint

            EVC7_MMIEtcsMiscOutSignals.Initialise(this);

            EVC1_MMIDynamic.Initialise(this);
            EVC1_MMIDynamic.MMI_V_PERMITTED = 1111;
            EVC1_MMIDynamic.MMI_V_INTERVENTION_KMH = 35;
            //?? Send

            /*
            Test Step 1
            Action: Perform the following procedure,Press and hold ‘Shunting’ button at least 2 seconds.Release the pressed button
            Expected Result: DMI displays in SH mode, level 1
            */
            WaitForVerification("Press and hold the ‘Shunting’ button for at least 2 seconds. Release the pressed button." + Environment.NewLine +
                                "Then check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in SH mode, level 1.");

            /*
            Test Step 2
            Action: Drive the train with speed = 30 km/h
            Expected Result: Verify the following information,(1)   Use the log file to confirm that DMI received the packet information EVC-1 and EVC-7 with following variables,(EVC-7) OBU_TR_M_MODE = 3 (Shunting)(EVC-1) MMI_M_WARNING = 0 (Status = NoS, Supervision = CSM)(EVC-1) MMI_V_PERMITTED = 833 (30 km/h)(2)   The speed pointer display in grey colour
            Test Step Comment: (1) MMI_gen 6299 (partly: OBU_TR_M_MODE, MMI_M_WARNING, train speed in relation to permitted speed MMI_V_PERMITTED, SH mode in CSM supervision);(2) MMI_gen 6299 (partly: colour of speed pointer, SH mode in CSM supervision);
            */
            EVC1_MMIDynamic.MMI_V_PERMITTED = 833;
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 30;
            // ?? Send
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Is the speed pointer grey?");

            /*
            Test Step 3
            Action: Increase the train speed to 31 km/h
            Expected Result: Verify the following information,(1)   Use the log file to confirm that DMI received the packet information EVC-1 with the following condition,MMI_M_WARNING = 8 (Status = OvS, Supervision = CSM) while the value of MMI_V_TRAIN = 861 (31 km/h) which greater than MMI_V_PERMITTED(2)   The speed pointer display in orange colour
            Test Step Comment: (1) MMI_gen 6299 (partly: MMI_M_WARNING, train speed in relation to permitted speed MMI_V_PERMITTED, SH mode in CSM supervision);(2) MMI_gen 6299 (partly: colour of speed pointer, SH mode in CSM supervision);
            */
            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Overspeed_Status_Ceiling_Speed_Monitoring;
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 31;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Is the speed pointer orange?");

            /*
            Test Step 4
            Action: Increase the train speed to 35 km/h.Note: dV_warning_max is defined in chapter 3 of [SUBSET-026]
            Expected Result: Verify the following information,(1)   Use the log file to confirm that DMI received the packet information EVC-1 with the following condition,MMI_M_WARNING = 4 (Status = WaS, Supervision = CSM) while the value of MMI_V_TRAIN = 972 (35  km/h) which greater than MMI_V_PERMITTED but lower than MMI_V_INTERVENTION(2)   The speed pointer display in orange colour
            Test Step Comment: (1) MMI_gen 6299 (partly: MMI_M_WARNING, train speed in relation to permitted speed MMI_V_PERMITTED, SH mode in CSM supervision);(2) MMI_gen 6299 (partly: colour of speed pointer, SH mode in CSM supervision);
            */
            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Warning_Status_Ceiling_Speed_Monitoring;
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 35;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Is the speed pointer orange?");

            /*
            Test Step 5
            Action: Increase the train speed to 36 km/h
            Expected Result: The train speed is force to decrease because of emergency brake is applied by ETCS onboard.Verify the following information,Before train speed is decreased(1)   Use the log file to confirm that DMI received the packet information EVC-1 with the following condition,MMI_M_WARNING = 12 (Status = IntS, Supervision = CSM) while the value of MMI_V_TRAIN = 1000 (36 km/h) which greater than MMI_V_INTERVENTION(2)   The speed pointer display in red colourAfter train speed is decreased(3)   Use the log file to confirm that DMI received the packet information EVC-1 with the following condition,MMI_M_WARNING = 12 (Status = IntS, Supervision = CSM) while the value of MMI_V_TRAIN is lower than MMI_V_INTERVENTION(4)   The speed pointer display in grey colour
            Test Step Comment: (1) MMI_gen 6299 (partly: MMI_M_WARNING, train speed in relation to permitted speed MMI_V_PERMITTED, SH mode in CSM supervision);(2) MMI_gen 6299 (partly: colour of speed pointer, SH mode in CSM supervision);(3) MMI_gen 6299 (partly: MMI_M_WARNING, SH mode in CSM supervision);(4) MMI_gen 6299 (partly: colour of speed pointer, SH mode in CSM supervision);
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 36;
            DmiActions.Apply_Brakes(this);
            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Intervention_Status_Ceiling_Speed_Monitoring;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Is the speed pointer red?");

            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Intervention_Status_Ceiling_Speed_Monitoring;

            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 30;
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Has the speed decreased to 30 km/h" + Environment.NewLine +
                                "2. Is the speed pointer grey?");
            
            /*
            Test Step 6
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
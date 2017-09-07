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
    /// 18.1.1 Distance to Target  Bar: General Appearance
    /// TC-ID: 13.1.1
    /// 
    /// This test case verifies  the general appearance and properties of distance to target bar in sub-area A3. The dimensions and colour of distance to target bar shall comply with [ERA] standard and [MMI-ETCS-gen].
    /// 
    /// Tested Requirements:
    /// MMI_gen 986; MMI_gen 6613 (partly: left column); MMI_gen 1261 (partly: meter); MMI_gen 6659; MMI_gen 987 (partly: vertical rectangular, right column of sub-area A3, left aligned); MMI_gen 105 (partly: result of calculation); MMI_gen 6616 (meter); MMI_gen 6773 (meter);      
    /// 
    /// Scenario:
    /// Active cabin A. Perform SoM to SR mode, level 1.Drive the train forward and pass BG1 at position 250m. Then, verify the display information of distance target bar.BG1 giving: pkt 12, pkt 21 and 27Stop the train at position 3000m. Then, verify the display information of distance target bar.
    /// 
    /// Used files:
    /// 13_1_1.tdg
    /// </summary>
    public class Distance_to_Target_Bar_General_Appearance : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // Set the following tags name in configuration file (See the instruction in Appendix 1)   SPEED_UNIT_TYPE = 0 (meter)System is power on.
         

            // Call the TestCaseBase PreExecution
            base.PreExecution();

            EVC0_MMIStartATP.Evc0Type = EVC0_MMIStartATP.EVC0Type.GoToIdle;
            EVC0_MMIStartATP.Send();
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

            EVC14_MMICurrentDriverID.MMI_Q_ADD_ENABLE = EVC14_MMICurrentDriverID.MMI_Q_ADD_ENABLE_BUTTONS.Settings;
            EVC14_MMICurrentDriverID.MMI_Q_CLOSE_ENABLE = Variables.MMI_Q_CLOSE_ENABLE.Enabled;
            EVC14_MMICurrentDriverID.Send();

            // Call generic Check Results Method
            DmiExpectedResults.Driver_ID_window_displayed_in_SB_mode(this);

            /*
            Test Step 2
            Action: Driver performs SoM to SR mode
            Expected Result: DMI displays in SR mode, level 1
            */
            DmiActions.ShowInstruction(this, "Perform SoM to SR mode");

            // Call generic Check Results Method
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Does the DMI delete the SB mode symbol (MO13) and replace it with the SR mode symbol (MO09) in area B7");

            /*
            Test Step 3
            Action: Drive the train forward pass BG1
            Expected Result: DMI changes from SR to FS mode
            */
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.FullSupervision;
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 5;
            
            // Call generic Check Results Method
            DmiExpectedResults.FS_mode_displayed(this);

            /*
            Test Step 4
            Action: Drive the train follow the permitted speed
            Expected Result: Verify the following information,The distance to target bar is displayed in sub-area A3.The distance scale is displayed in left column of sub-area A3.The distance to target bar is displayed distance from zero to a maximum of 1000m according to the distance scale. Distances above 1000m is limited to the distance scale’s upper boundary.The distance to target bar is additional marked by a white arrow on top. (see the figure of a white arrow in ‘Comment’ column).The distance to target bar and distance scale are displayed as grey colour.The distance to target is indicated by a vertical rectangular bar at the right column of sub-area A3 with left aligned.Use the log file to confirm that the distance to target (bar and digital) is calculated from the received packet information EVC-7 and EVC-1 as follows,(EVC-1) MMI_O_BRAKETARGET - (EVC-7) OBU_TR_O_TRAINThe result of calculation is displayed in meter unit.Example: The observation point of the distance target is 445. [EVC-1.MMI_O_BRAKETARGET = 1000080700] - [EVC-7.OBU_TR_O_TRAIN = 1000040036] = 40664 cm (406.64 m, 444.71 yard).The distance target digital in sub-area A2 displays as 407 meters.The distance target bar in sub-area A3 displays over the indicator line No.5 (400m/704 yard)Note: Unit conversion1cm = 0.01m1m = 1.09361yard
            Test Step Comment: (1) MMI_gen 986;             (2) MMI_gen 6613 (partly: left column);                            (3) MMI_gen 1261 (partly: display distances);                          (4) MMI_gen 1261 (partly: Limitation);                  (5) MMI_gen 6659;                        (6) MMI_gen 987 (partly: vertical rectangular, right column of sub-area A3, left aligned);                  (7) MMI_gen 1261 (partly: calculation); MMI_gen 6616 (meter); MMI_gen 6773 (meter); MMI_gen 105 (partly: result of calculation);     
            */
            // Call generic Action Method
            // Set the permitted speed so the current speed is allowed
            EVC1_MMIDynamic.MMI_V_PERMITTED_KMH = 10;

            // ?? Set an EOA so the DMI can display a target
            EVC1_MMIDynamic.MMI_O_BRAKETARGET = 200000;             // 2 km
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN = 5000;   // 50m

            // Check log for 7.
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The distance to target bar is displayed in sub-area A3." + Environment.NewLine +
                                "2. The distance scale is displayed in left column of sub-area A3." + Environment.NewLine +
                                "3. The distance to target bar is displayed distance from zero to a maximum of 1000m according to the distance scale. " + Environment.NewLine +
                                    "Distances above 1000m is limited to the distance scale’s upper boundary." + Environment.NewLine +
                                "4. The distance to target bar has a white arrow on top. (see the Spec.)." + Environment.NewLine +
                                "5. The distance to target bar and distance scale are displayed in grey." + Environment.NewLine +
                                "6. The distance to target is indicated by a vertical rectangular bar left aligned in the right-hand column of sub-area A3." + Environment.NewLine +
                                "7. The digital distance to target displayed in sub-area A2 = 1950m (2000 - 50)");

            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN = 250000;   // 2.5 km

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The distance to target bar is not displayed in sub-area A3.");
            // Check log

            /*
            Test Step 5
            Action: Stop the train
            Expected Result: Verify the following information,Use the log file to check the different of the following received packets is less than zero(EVC-1) MMI_O_BRAKETARGET – (EVC-7) OBU_TR_O_TRAIN < 0If the result of calculation data is less than 0, The distance to target bar is not display in sub-area A3
            Test Step Comment: (1) MMI_gen 1261 (partly: If not positive distance, distance to target bar not be displayed);
            */
            EVC1_MMIDynamic.MMI_V_TRAIN = 0;    // Set speed to zero
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN = 250000;   // 2.5 km

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The distance to target bar is not displayed in sub-area A3.");
            
            /*
            Test Step 6
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
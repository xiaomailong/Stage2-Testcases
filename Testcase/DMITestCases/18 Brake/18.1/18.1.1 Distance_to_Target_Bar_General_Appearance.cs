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


            /*
            Test Step 1
            Action: Activate cabin A
            Expected Result: DMI displays in SB mode, level 1. The Driver ID window is displayed
            */
            // Call generic Action Method
            DmiActions.Activate_Cabin_1(this);
            // Call generic Check Results Method
            DmiExpectedResults.DMI_displays_in_SB_mode_level_1_Driver_ID_window_displayed(this);


            /*
            Test Step 2
            Action: Driver performs SoM to SR mode
            Expected Result: DMI displays in SR mode, level 1
            */
            // Call generic Action Method
            DmiActions.Driver_performs_SoM_to_SR_mode(this);
            // Call generic Check Results Method
            DmiExpectedResults.SR_Mode_displayed(this);


            /*
            Test Step 3
            Action: Drive the train forward pass BG1
            Expected Result: DMI changes from SR to FS mode
            */
            // Call generic Action Method
            DmiActions.Drive_the_train_forward_pass_BG1(this);
            // Call generic Check Results Method
            DmiExpectedResults.DMI_changes_from_SR_to_FS_mode(this);


            /*
            Test Step 4
            Action: Drive the train follow the permitted speed
            Expected Result: Verify the following information,The distance to target bar is displayed in sub-area A3.The distance scale is displayed in left column of sub-area A3.The distance to target bar is displayed distance from zero to a maximum of 1000m according to the distance scale. Distances above 1000m is limited to the distance scale’s upper boundary.The distance to target bar is additional marked by a white arrow on top. (see the figure of a white arrow in ‘Comment’ column).The distance to target bar and distance scale are displayed as grey colour.The distance to target is indicated by a vertical rectangular bar at the right column of sub-area A3 with left aligned.Use the log file to confirm that the distance to target (bar and digital) is calculated from the received packet information EVC-7 and EVC-1 as follows,(EVC-1) MMI_O_BRAKETARGET - (EVC-7) OBU_TR_O_TRAINThe result of calculation is displayed in meter unit.Example: The observation point of the distance target is 445. [EVC-1.MMI_O_BRAKETARGET = 1000080700] - [EVC-7.OBU_TR_O_TRAIN = 1000040036] = 40664 cm (406.64 m, 444.71 yard).The distance target digital in sub-area A2 displays as 407 meters.The distance target bar in sub-area A3 displays over the indicator line No.5 (400m/704 yard)Note: Unit conversion1cm = 0.01m1m = 1.09361yard
            Test Step Comment: (1) MMI_gen 986;             (2) MMI_gen 6613 (partly: left column);                            (3) MMI_gen 1261 (partly: display distances);                          (4) MMI_gen 1261 (partly: Limitation);                  (5) MMI_gen 6659;                        (6) MMI_gen 987 (partly: vertical rectangular, right column of sub-area A3, left aligned);                  (7) MMI_gen 1261 (partly: calculation); MMI_gen 6616 (meter); MMI_gen 6773 (meter); MMI_gen 105 (partly: result of calculation);     
            */
            // Call generic Action Method
            DmiActions.Drive_the_train_follow_the_permitted_speed(this);


            /*
            Test Step 5
            Action: Stop the train
            Expected Result: Verify the following information,Use the log file to check the different of the following received packets is less than zero(EVC-1) MMI_O_BRAKETARGET – (EVC-7) OBU_TR_O_TRAIN < 0If the result of calculation data is less than 0, The distance to target bar is not display in sub-area A3
            Test Step Comment: (1) MMI_gen 1261 (partly: If not positive distance, distance to target bar not be displayed);
            */
            // Call generic Action Method
            DmiActions.Stop_the_train(this);


            /*
            Test Step 6
            Action: End of test
            Expected Result: 
            */


            return GlobalTestResult;
        }
    }
}
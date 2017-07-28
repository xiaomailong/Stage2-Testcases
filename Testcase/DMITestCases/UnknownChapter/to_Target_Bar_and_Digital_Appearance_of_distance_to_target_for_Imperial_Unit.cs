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
    /// Distance to Target Bar and Digital: Appearance of distance to target for Imperial Unit
    /// TC-ID: 13.1.9
    /// 
    /// This test case verifie the display information of the distance to target bar and ditital including with PA distance scale when select a scaling configuration to Imperial unit (yard, mph).
    /// 
    /// Tested Requirements:
    /// MMI_gen 6616 (partly: yard); MMI_gen 6773 (partly: yard); MMI_gen 7110 (partly: yard); MMI_gen 1261 (partly: yard); MMI_gen 105 (partly: result of calculation);
    /// 
    /// Scenario:
    /// 1.Drive the train forward pass BG1 at 250m. Then, verify the display of distance to target bar, distance to target digital and PA speed profile compare with PA distance scale line.  BG1: Packet 12, 21 and 27 (Entering FS) 
    /// 2.Drive the train follow permitted speed. Then, verify the display information of distance target bar, distance to target digital and PA speed profile compare with PA distance scale line.
    /// 
    /// Used files:
    /// 13_1_9.tdg
    /// </summary>
    public class to_Target_Bar_and_Digital_Appearance_of_distance_to_target_for_Imperial_Unit : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // Set the following tags name in configuration file (See the instruction in Appendix 1)   SPEED_UNIT_TYPE = 1 (Yards)System is powered on.Cabin is activated.SoM is performed in SR mode, level 1.

            // Call the TestCaseBase PreExecution
            base.PreExecution();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in FS mode, Level 1

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint


            /*
            Test Step 1
            Action: Drive the train forward pass BG1 follow the permitted speed
            Expected Result: DMI displays in FS mode, level 1.Verify the following information,(1)    The distance to target bar is displayed distance from zero to a maximum of 1760yard according to the distance scale. Distances above 1760yard is limited to the distance scale’s upper boundary.(2)   Use the log file to confirm that the distance to target (bar and digital) is calculated from the received packet information EVC-7 and EVC-1 as follows,(EVC-1) MMI_O_BRAKETARGET - (EVC-7) OBU_TR_O_TRAINThe result of calculation is displayed in Yard unit.Example: The observation point of the distance target is 445. [EVC-1.MMI_O_BRAKETARGET = 1000080700] - [EVC-7.OBU_TR_O_TRAIN = 1000040036] = 40664 cm (406.64 m, 444.71 yard).       The distance target digital in sub-area A2 displays as 445 yard.The distance target bar in sub-area A3 displays over the indicator line No.3 (200m/352 yard)Note: Unit conversion1cm = 0.01m1m = 1.09361yard(3)   Use the log file to confirm that the movement authority is calculated from the received packet information EVC-7 and EVC-4 as follows,(EVC-4) MMI_O_MRSP[0] - (EVC-7) OBU_TR_O_TRAINThe result of calculation is displayed in Yard unit.Example: The observation point of the movement authority is 445. [EVC-4.MMI_O_MRSP[0]= 1000080700] – [EVC-7.OBU_TR_O_TRAIN = 1000040036] = 40664 cm (406.64 m, 444.71 yard). Note: Unit conversion1cm = 0.01m1m = 1.09361yard
            Test Step Comment: (1) MMI_gen 1261 (partly: yard);(2) MMI_gen 6616 (yard); MMI_gen 6773 (yard); MMI_gen 105 (partly: result of calculation);(3) MMI_gen 7110 (partly: yard);
            */


            /*
            Test Step 2
            Action: Stop the train
            Expected Result: Verify the following information,Use the log file to check the different of the following received packets is less than zero(EVC-1) MMI_O_BRAKETARGET – (EVC-7) OBU_TR_O_TRAIN < 0(1)    If the result of calculation data is less than 0, The distance to target bar is not display in sub-area A3
            Test Step Comment: (1) MMI_gen 1261 (partly: If not positive distance, distance to target bar not be displayed);
            */
            // Call generic Action Method
            DmiActions.Stop_the_train();


            /*
            Test Step 3
            Action: End of test
            Expected Result: 
            */


            return GlobalTestResult;
        }
    }
}
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
    /// 17.2.1 Speed Dial: Sub-Area B0
    /// TC-ID: 12.2
    /// 
    /// This test case verifies the display of the speed dial on DMI which complies with [ERA] standard and [MMI-ETCS-gen].
    /// 
    /// Tested Requirements:
    /// MMI_gen 5951; MMI_gen 102; MMI_gen 1655; MMI_gen 1653;  MMI_gen 1654; MMI_gen 5960; MMI_gen 9950; MMI_gen 5961; MMI_gen 9951;
    /// 
    /// Scenario:
    /// The property of the speed dial is configured before the test environment is (re)started as follows, see the properties in column Precondition:400 km/h dial: True, False, 400, 200, 50, 100, 4, 4, 0, 1, -144, 48, 144 respectively250 km/h dial: True, False, 250, 100, 20, 20, 1, 1, 0, 0, -144, -29, 144 respectively180 km/h dial: True, False, 180, 0, 20, 0, 1, 0, 0, 0, -144, 0, 144 respectively140 km/h dial: True, False, 140, 0, 20, 0, 1, 0, 0, 0, -144, 0, 144 respectivelySoM is performed until Level 1 is confirmed and the ‘Main’ window is closed.The speedometer is verified.
    /// 
    /// Used files:
    /// N/A
    /// </summary>
    public class Speed_Dial_Sub_Area_B0 : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // The speed dial properties below are configured for 400 km/h as specified: True, False, 400, 200, 50, 100, 4, 4, 0, 1, -144, 48, 144 respectively: Speed in km/h (otherwise mph)Display speed unitMaximum Speed (upper boundary of the entire Speed Dial)Transition Speed (boundary between the two segments, if 0 – only segment 1 available)Speed interval between subsequent speed labels in segment 1Speed interval between subsequent speed labels in segment 2Number of short scale divisions between long scale divisions in segment 1Number of short scale divisions between long scale divisions in segment 2Number of long scale divisions between labels in segment 1Number of long scale divisions between labels in segment 2Position of Zero point (angle in grad, 0 grad at 12 o’clock, counting clockwise)Position of Transition Speed (angle, see above)Position of Maximum Speed (angle, see above)Test system is powered on.Activate Cabin A.Enter Driver ID and perform brake test.Select and confirm Level 1

            // load config settings: TODO Check these
            // SPEED_UNIT_TYPE = 0
            // SPEED_UNIT_DISPLAY = 0
            // SPEED_DIAL_V_MAX = 400
            // SPEED_DIAL_V_TRANS = 100
            // SPEED_DIAL_V_NUMBER1 = 20
            // SPEED_DIAL_V_NUMBER2 = 20
            // SPEED_DIAL_N_SHORT_LINES1 = 1
            // SPEED_DIAL_N_SHORT_LINES2 = 1
            // SPEED_DIAL_N_LONG_LINES1 = 0
            // SPEED_DIAL_N_LONG_LINES2 = 0
            // SPEED_DIAL_ANGLE_V_0 = -144
            // SPEED_DIAL_ANGLE_V_TRANS = -29
            // SPEED_DIAL_ANGLE_V_MAX = 144

            DmiActions.Complete_SoM_L1_SB(this);
            // Call the TestCaseBase PreExecution
            base.PreExecution();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in SB mode, level 1
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in SB mode, Level 1.");

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint

            /*
            Test Step 1
            Action: Close the ‘Main’ window
            Expected Result: Verify the following information,Speed Dial is displayed in sub-area B0.Speed Dial is shaped with circular border and indicated speed scaling from 0km/h to 400km/h.The speed indicator with its numbers and lines is coloured white. Speed Dial is composed of short and long lines speed indicator drawn radially from limit of sub-area B0 towards the center of sub-area B0 at every 10km/h.No speed unit is beneath the speed pointer’s hub.The scaling numbers are positioned at the end of the related indicator line towards the centre of B0
            Test Step Comment: (1) MMI_gen 5951(2) MMI_gen 102, MMI_gen 1655 (a), MMI_gen 9951 (400 km/h), MMI_gen 1653,  MMI_gen 1654;      (3) MMI_gen 5960;    (4) MMI_gen 9950 (a);    (5) MMI_gen 5961 (NEGATIVE)(6) MMI_gen 9950 (partly: toward the center of B0);     
            */
            WaitForVerification("Close the main window and check the following" + Environment.NewLine + Environment.NewLine +
                                "1. Speed Dial is displayed in sub-area B0." + Environment.NewLine +
                                "2. Speed Dial a circular border and with speed scaling from 0 km/h to 400 km/h." + Environment.NewLine +
                                "3. The numbers and lines on the speed indicator are in white." + Environment.NewLine +
                                "4. Speed Dial hass short and long lines speed drawn radially from limit of sub-area B0 towards the center of sub-area B0 at every 10km/h." + Environment.NewLine +
                                "5. No speed unit is displayed beneath the speed pointer’s hub." + Environment.NewLine +
                                "6. The scaling numbers are positioned at the end of the related indicator line towards the centre of B0.");

            /*
            Test Step 2
            Action: Perform the following procedure,Power off the test environmentRe-configure the speed dial properties for 250 km/h as specified below.Start the test environment.Start the mission until Level 1 is confirmedClose the ‘Main’ windowConfiguration valueSPEED_UNIT_TYPE = 0SPEED_UNIT_DISPLAY= 0SPEED_DIAL_V_MAX = 250SPEED_DIAL_V_TRANS = 100SPEED_DIAL_V_NUMBER1 = 20SPEED_DIAL_V_NUMBER2 = 20SPEED_DIAL_N_SHORT_LINES1 = 1SPEED_DIAL_N_SHORT_LINES2 = 1SPEED_DIAL_N_LONG_LINES1 = 0SPEED_DIAL_N_LONG_LINES2 =0SPEED_DIAL_ANGLE_V_0 = -144SPEED_DIAL_ANGLE_V_TRANS = -29SPEED_DIAL_ANGLE_V_MAX = 144
            Expected Result: Verify the following information,Speed Dial is displayed in sub-area B0.Speed Dial is shaped with circular border and indicated speed scaling from 0km/h to 250km/h.The speed indicator with its numbers and lines is coloured white. Speed Dial is composed of short and long lines speed indicator drawn radially from limit of sub-area B0 towards the center of sub-area B0 at every 10km/h.No speed unit is beneath the speed pointer’s hub.The scaling numbers are positioned at the end of the related indicator line towards the centre of B0
            Test Step Comment: (1) MMI_gen 5951(2) MMI_gen 102, MMI_gen 1655 (b), MMI_gen 9951 (250 km/h), MMI_gen 1653,  MMI_gen 1654;      (3) MMI_gen 5960;    (4) MMI_gen 9950 (a);    (5) MMI_gen 5961 (NEGATIVE)(6) MMI_gen 9950 (partly: toward the center of B0);     
            */
            // ?? Power off the system

            // load config settings: 
            // SPEED_UNIT_TYPE = 0
            // SPEED_UNIT_DISPLAY = 0
            // SPEED_DIAL_V_MAX = 250
            // SPEED_DIAL_V_TRANS = 100
            // SPEED_DIAL_V_NUMBER1 = 20
            // SPEED_DIAL_V_NUMBER2 = 20
            // SPEED_DIAL_N_SHORT_LINES1 = 1
            // SPEED_DIAL_N_SHORT_LINES2 = 0
            // SPEED_DIAL_N_LONG_LINES1 = 0
            // SPEED_DIAL_N_LONG_LINES2 = 0
            // SPEED_DIAL_ANGLE_V_0 = -144
            // SPEED_DIAL_ANGLE_V_TRANS = -29
            // SPEED_DIAL_ANGLE_V_MAX = 144

            // ?? Power on the system
            DmiActions.Complete_SoM_L1_SB(this);

            WaitForVerification("Close the main window and check the following" + Environment.NewLine + Environment.NewLine +
                                "1. Speed Dial is displayed in sub-area B0." + Environment.NewLine +
                                "2. Speed Dial a circular border and with speed scaling from 0 km/h to 250 km/h." + Environment.NewLine +
                                "3. The numbers and lines on the speed indicator are in white." + Environment.NewLine +
                                "4. Speed Dial hass short and long lines speed drawn radially from limit of sub-area B0 towards the center of sub-area B0 at every 10km/h." + Environment.NewLine +
                                "5. No speed unit is displayed beneath the speed pointer’s hub." + Environment.NewLine +
                                "6. The scaling numbers are positioned at the end of the related indicator line towards the centre of B0.");

            /*
            Test Step 3
            Action: Perform the following procedure,Power off the test environmentRe-configure the speed dial properties for 180 km/h as specified below.Start the test environment.Start the mission until Level 1 is confirmedClose the ‘Main’ windowConfiguration valueSPEED_UNIT_TYPE = 0SPEED_UNIT_DISPLAY= 0SPEED_DIAL_V_MAX = 180SPEED_DIAL_V_TRANS = 0SPEED_DIAL_V_NUMBER1 = 20SPEED_DIAL_V_NUMBER2 = 0SPEED_DIAL_N_SHORT_LINES1 = 1SPEED_DIAL_N_SHORT_LINES2 = 0SPEED_DIAL_N_LONG_LINES1 = 0SPEED_DIAL_N_LONG_LINES2 =0SPEED_DIAL_ANGLE_V_0 = -144SPEED_DIAL_ANGLE_V_TRANS = 0SPEED_DIAL_ANGLE_V_MAX = 144
            Expected Result: Verify the following information,Speed Dial is display in sub-area B0.Speed Dial is shaped with circular border and indicated speed scaling from 0km/h to 180km/h.The speed indicator with its numbers and lines is coloured white. Speed Dial is composed of short and long lines speed indicator drawn radially from limit of sub-area B0 towards the center of sub-area B0 at every 10km/h.No speed unit is beneath the speed pointer’s hub.The scaling numbers are positioned at the end of the related indicator line towards the centre of B0
            Test Step Comment: (1) MMI_gen 5951(2) MMI_gen 102, MMI_gen 1655 ©, MMI_gen 9951 (180 km/h), MMI_gen 1653,  MMI_gen 1654;      (3) MMI_gen 5960;    (4) MMI_gen 9950 (a);    (5) MMI_gen 5961 (NEGATIVE)(6) MMI_gen 9950 (partly: toward the center of B0);     
            */
            // ?? Power off the system

            // load config settings: 
            // SPEED_UNIT_TYPE = 0
            // SPEED_UNIT_DISPLAY = 0
            // SPEED_DIAL_V_MAX = 180
            // SPEED_DIAL_V_TRANS = 0
            // SPEED_DIAL_V_NUMBER1 = 20
            // SPEED_DIAL_V_NUMBER2 = 0
            // SPEED_DIAL_N_SHORT_LINES1 = 1
            // SPEED_DIAL_N_SHORT_LINES2 = 0
            // SPEED_DIAL_N_LONG_LINES1 = 0
            // SPEED_DIAL_N_LONG_LINES2 = 0
            // SPEED_DIAL_ANGLE_V_0 = -144
            // SPEED_DIAL_ANGLE_V_TRANS = 0
            // SPEED_DIAL_ANGLE_V_MAX = 144

            // ?? Power on the system
            DmiActions.Complete_SoM_L1_SB(this);

            WaitForVerification("Close the main window and check the following" + Environment.NewLine + Environment.NewLine +
                                "1. Speed Dial is displayed in sub-area B0." + Environment.NewLine +
                                "2. Speed Dial a circular border and with speed scaling from 0 km/h to 180 km/h." + Environment.NewLine +
                                "3. The numbers and lines on the speed indicator are in white." + Environment.NewLine +
                                "4. Speed Dial hass short and long lines speed drawn radially from limit of sub-area B0 towards the center of sub-area B0 at every 10km/h." + Environment.NewLine +
                                "5. No speed unit is displayed beneath the speed pointer’s hub." + Environment.NewLine +
                                "6. The scaling numbers are positioned at the end of the related indicator line towards the centre of B0.");

            /*
            Test Step 4
            Action: Perform the following procedure,Power off the test environmentRe-configure the speed dial properties for 140 km/h without speed unit as specified below.Start the test environment.Start the mission until Level 1 is confirmedClose the ‘Main’ windowConfiguration valueSPEED_UNIT_TYPE = 0SPEED_UNIT_DISPLAY= 0SPEED_DIAL_V_MAX = 140SPEED_DIAL_V_TRANS = 0SPEED_DIAL_V_NUMBER1 = 20SPEED_DIAL_V_NUMBER2 = 0SPEED_DIAL_N_SHORT_LINES1 = 1SPEED_DIAL_N_SHORT_LINES2 = 0SPEED_DIAL_N_LONG_LINES1 = 0SPEED_DIAL_N_LONG_LINES2 =0SPEED_DIAL_ANGLE_V_0 = -144SPEED_DIAL_ANGLE_V_TRANS = 0SPEED_DIAL_ANGLE_V_MAX = 144
            Expected Result: Verify the following information,Speed Dial is display in sub-area B0.Speed Dial is shaped with circular border and and indicated speed scaling from 0km/h to 140km/h.The speed indicator with its numbers and lines is coloured white. Speed Dial is composed of short and long lines speed indicator drawn radially from limit of sub-area B0 towards the center of sub-area B0 at every 10km/h.No speed unit is beneath the speed pointer’s hub.The scaling numbers are positioned at the end of the related indicator line towards the centre of B0
            Test Step Comment: (1) MMI_gen 5951(2) MMI_gen 102, MMI_gen 1655 (d), MMI_gen 9951 (140 km/h), MMI_gen 1653,  MMI_gen 1654;      (3) MMI_gen 5960;    (4) MMI_gen 9950 (a);    (5) MMI_gen 5961 (NEGATIVE)(6) MMI_gen 9950 (partly: toward the center of B0);     
            */
            // ?? Power off the system

            // load config settings: 
            // SPEED_UNIT_TYPE = 0
            // SPEED_UNIT_DISPLAY = 0
            // SPEED_DIAL_V_MAX = 140
            // SPEED_DIAL_V_TRANS = 0
            // SPEED_DIAL_V_NUMBER1 = 20
            // SPEED_DIAL_V_NUMBER2 = 0
            // SPEED_DIAL_N_SHORT_LINES1 = 1
            // SPEED_DIAL_N_SHORT_LINES2 = 0
            // SPEED_DIAL_N_LONG_LINES1 = 0
            // SPEED_DIAL_N_LONG_LINES2 = 0
            // SPEED_DIAL_ANGLE_V_0 = -144
            // SPEED_DIAL_ANGLE_V_TRANS = 0
            // SPEED_DIAL_ANGLE_V_MAX = 144

            // ?? Power on the system
            DmiActions.Complete_SoM_L1_SB(this);

            WaitForVerification("Close the main window and check the following" + Environment.NewLine + Environment.NewLine +
                                "1. Speed Dial is displayed in sub-area B0." + Environment.NewLine +
                                "2. Speed Dial a circular border and with speed scaling from 0 km/h to 140 km/h." + Environment.NewLine +
                                "3. The numbers and lines on the speed indicator are in white." + Environment.NewLine +
                                "4. Speed Dial hass short and long lines speed drawn radially from limit of sub-area B0 towards the center of sub-area B0 at every 10km/h." + Environment.NewLine +
                                "5. No speed unit is displayed beneath the speed pointer’s hub." + Environment.NewLine +
                                "6. The scaling numbers are positioned at the end of the related indicator line towards the centre of B0.");

            /*
            Test Step 5
            Action: Perform the following procedure,Power off the test environmentRe-configure the speed dial properties for 140 km/h with speed unit as specified below.Start the test environment.Start the mission until Level 1 is confirmedClose the ‘Main’ windowConfiguration valueSPEED_UNIT_TYPE = 0SPEED_UNIT_DISPLAY= 1SPEED_DIAL_V_MAX = 140SPEED_DIAL_V_TRANS = 0SPEED_DIAL_V_NUMBER1 = 20SPEED_DIAL_V_NUMBER2 = 0SPEED_DIAL_N_SHORT_LINES1 = 1SPEED_DIAL_N_SHORT_LINES2 = 0SPEED_DIAL_N_LONG_LINES1 = 0SPEED_DIAL_N_LONG_LINES2 =0SPEED_DIAL_ANGLE_V_0 = -144SPEED_DIAL_ANGLE_V_TRANS = 0SPEED_DIAL_ANGLE_V_MAX = 144
            Expected Result: Verify the following information,Speed Dial is display in sub-area B0.Speed Dial is shaped with circular border and and indicated speed scaling from 0km/h to 140km/h.The speed indicator with itsbers and lines is coloured white. Speed Dial is composed of short and long lines speed indicator drawn radially from limit of sub-area B0 towards the center of sub-area B0 at every 10km/h.The speed unit is displayed centered beneath the speed pointer’s hub.The scaling numbers are positioned at the end of the related indicator line towards the centre of B0
            Test Step Comment: (1) MMI_gen 5951(2) MMI_gen 102, MMI_gen 1655 (d), MMI_gen 9951 (140 km/h), MMI_gen 1653,  MMI_gen 1654;      (3) MMI_gen 5960;    (4) MMI_gen 9950 (a);    (5) MMI_gen 5961; (6) MMI_gen 9950 (partly: toward the center of B0);          
            */
            // ?? Power off the system

            // load config settings: 
            // SPEED_UNIT_TYPE = 0
            // SPEED_UNIT_DISPLAY = 1
            // SPEED_DIAL_V_MAX = 140
            // SPEED_DIAL_V_TRANS = 0
            // SPEED_DIAL_V_NUMBER1 = 20
            // SPEED_DIAL_V_NUMBER2 = 0
            // SPEED_DIAL_N_SHORT_LINES1 = 1
            // SPEED_DIAL_N_SHORT_LINES2 = 0
            // SPEED_DIAL_N_LONG_LINES1 = 0
            // SPEED_DIAL_N_LONG_LINES2 = 0
            // SPEED_DIAL_ANGLE_V_0 = -144
            // SPEED_DIAL_ANGLE_V_TRANS = 0
            // SPEED_DIAL_ANGLE_V_MAX = 144

            // ?? Power on the system
            DmiActions.Complete_SoM_L1_SB(this);

            WaitForVerification("Close the main window and check the following" + Environment.NewLine + Environment.NewLine +
                                "1. Speed Dial is displayed in sub-area B0." + Environment.NewLine +
                                "2. Speed Dial a circular border and with speed scaling from 0 km/h to 140 km/h." + Environment.NewLine +
                                "3. The numbers and lines on the speed indicator are in white." + Environment.NewLine +
                                "4. Speed Dial hass short and long lines speed drawn radially from limit of sub-area B0 towards the center of sub-area B0 at every 10km/h." + Environment.NewLine +
                                "5. The speed unit is displayed centered beneath the speed pointer’s hub." + Environment.NewLine +
                                "6. The scaling numbers are positioned at the end of the related indicator line towards the centre of B0.");

           

            /*
            Test Step 6
            Action: End of test
            Expected Result: 
            */


            return GlobalTestResult;
        }
    }
}
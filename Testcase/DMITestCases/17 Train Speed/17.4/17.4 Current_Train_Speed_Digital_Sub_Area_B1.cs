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
    /// 17.4 Current Train Speed Digital: Sub-Area B1
    /// TC-ID: 12.4
    /// 
    /// This test case verifies the display of the speed digital on DMI which complies with [ERA] standard and [MMI-ETCS-gen].
    /// 
    /// Tested Requirements:
    /// MMI_gen 4244; MMI_gen 6303; MMI_gen 6304; MMI_gen 6307; MMI_gen 1279; MMI_gen 6306 (partly: INITIAL, different widths of digit in the location);
    /// 
    /// Scenario:
    /// SoM is performed in SR mode, Level 
    /// 1.The speedometer is verified with the following event:The train runs with speed 50 km/hr (over permitted speed).The train runs with lower speed 40 km/h through BG1 at position 250m:packet 12, 21 and 27: (Entering FS mode)The train runs with higher speed hit upper than 100km/h. Then, verify the 3 digits information of speed digital.Stop the train.Use the test script files to send EVC-
    /// 1.Then, verify the display information of speed digital is always rounded up.
    /// 
    /// Used files:
    /// 12_4.tdg,12_4_a.xml, 12_4_b.xml, 12_4_c.xml
    /// </summary>
    public class Current_Train_Speed_Digital_Sub_Area_B1 : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // Test system is powered on.Activate Cabin A.Enter Driver ID and perform brake test.Select and confirm Level 1.SoM in performed in SR mode, Level 1.

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
            Action: Press ‘Close’ button
            Expected Result: DMI displays Default window.Verify the following information,ETSC-DMI using EVC-1 with variable MMI_V_TRAIN = 0 as the train is standstill.The number of the current train speed is displayed in Sub-Area B1.Number 0 is black.The single integer number is aligned right
            Test Step Comment: (1) MMI_gen 6303;(2) MMI_gen 6304;(3) MMI_gen 6307 (partly: digital number is black)(4) MMI_gen 1279 (partly: right most sub-area, 1 digit, integer)
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press ‘Close’ button");


            /*
            Test Step 2
            Action: Drive the train forward with speed 50km/h
            Expected Result: Verify the following information,The number of the current train speed is coloured white when the speed pointer is red.The 2-digit interger number is aligned right without leading zeroes.The numbers of the current train speed on Speed hub are displayed by no leading with zero
            Test Step Comment: (1) MMI_gen 6307 (partly: Speed pointer has the red colour);      (2) MMI_gen 1279 (partly: right most sub-area, 2 digit, integer, no zeroes)(3) MMI_gen 4244;
            */


            /*
            Test Step 3
            Action: Drive the train and deaccelarate the speed to 40 km/hr through BG1
            Expected Result: Verify the following information,The number of the current train speed is coloured black.The 2-digit interger number is aligned right without leading zeroes
            Test Step Comment: (1) MMI_gen 6307 (partly: Speed pointer has no red colour);(2) MMI_gen 1279 (partly: right most sub-area, 2 digit, integer, no zeroes)
            */


            /*
            Test Step 4
            Action: Drive the train and accelarate the speed to 111 km/hr
            Expected Result: Verify the following information,Three of number 1 (“111”) are displayed in Sub-Area B1, as number 1 has the smallest width
            Test Step Comment: (1) MMI_gen 6306 (partly: INITIAL, different widths of digit in the location)
            */


            /*
            Test Step 5
            Action: Drive the train and accelarate the speed to 108 km/hr
            Expected Result: Verify the following information,Even though the numbers are changed (from “111” to “108), the positions of digits remain the same
            Test Step Comment: (1) MMI_gen 6306 (partly: different widths of digit in the location)
            */


            /*
            Test Step 6
            Action: Stop the train
            Expected Result: DMI displays the train speed as zero km/h
            */
            // Call generic Action Method
            DmiActions.Stop_the_train(this);


            /*
            Test Step 7
            Action: Use the test script file 12_4_a.xml to send EVC-1 with,MMI_V_TRAIN = 389 cm/s (14.004 km/h)Note: The result of test script file may interrupted by ATP-CU, need to execute test script file repeatly to see the result
            Expected Result: The speed digital is changed to 15 km/h
            Test Step Comment: MMI_gen 1279 (partly: decimal rounded up, near integer)
            */


            /*
            Test Step 8
            Action: Use the test script file 12_4_b.xml to send EVC-1 with,MMI_V_TRAIN = 500 cm/s (18.000 km/h)Note: The result of test script file may interrupted by ATP-CU, need to execute test script file repeatly to see the result
            Expected Result: The speed digital is 18 km/h
            Test Step Comment: MMI_gen 1279 (partly: NEGATIVE, decimal rounded up)
            */


            /*
            Test Step 9
            Action: Use the test script file 12_4_c.xml to send EVC-1 with,MMI_V_TRAIN = 625 cm/s (22.500 km/h)Note: The result of test script file may interrupted by ATP-CU, need to execute test script file repeatly to see the result
            Expected Result: The speed digital is 23 km/h
            Test Step Comment: MMI_gen 1279 (partly: decimal rounded up, far from integer)
            */


            /*
            Test Step 10
            Action: End of test
            Expected Result: 
            */


            return GlobalTestResult;
        }
    }
}
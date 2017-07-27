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
    /// 22.8 PA Indication Marker: Sub-Area D7
    /// TC-ID: 17.8
    /// 
    /// This test case verifies the visaulization of PA Indication marker in sub-area D7. It shall comply with condition of [MMI-ETCS-gen]
    /// 
    /// Tested Requirements:
    /// MMI_gen 649 (partly: yellow line and partly: length); MMI_gen 7330; MMI_gen 9947; MMI_gen 7332; MMI_gen 650;
    /// 
    /// Scenario:
    /// 1.Use the test script file to send EVC-1 and EVC-7 with indication marker location and current train position are less than zero or the result of calculation is less than zero. Then, verify that Indicator Marker is not appeared
    /// 2.Use the test script file to send EVC-1 and EVC-7 with indication marker location and current train position correct with condition of MMI_gen 
    /// 650.Then, verify that Indicator Marker is appeared.
    /// 3.Start driving the train forward. Then, verify that Indicator Marker shall move with train position calculation.
    /// 
    /// Used files:
    /// 17_8_a.xml, 17_8_b.xml, 17_8_c.xml 17_8_d.xml
    /// </summary>
    public class PA_Indication_Marker_Sub_Area_D7 : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // Set the following tags name in configuration file (See the instruction in Appendix 1)HIDE_PA_SR_MODE = 1Test system is powered onCabin is active
            
            // Call the TestCaseBase PreExecution
            base.PreExecution();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in SR mode, level 1.

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint

            
            /*
            Test Step 1
            Action: Driver performs SoM to SR mode, level 1
            Expected Result: DMI displays in SR mode, level 1 and the Planning Area is displayed in area D
            */
            // Call generic Action Method
            DmiActions.Driver_performs_SoM_to_SR_mode_level_1();
            
            
            /*
            Test Step 2
            Action: Use the test script file 17_8_a.xml to send EVC-1 with,MMI_O_IML = 4294967295 (-1)Note: The result of test script file may interrupted by ATP-CU, need to execute test script file repeatly to see the result.Note2: current position (OBU_TR_O_TRAIN) = 0 (1000000000)
            Expected Result: Verify the indication marker shall not be shown in area D7
            Test Step Comment: MMI_gen 7332 (Partly: MMI_O_IML is less than zero);
            */
            // Call generic Check Results Method
            DmiExpectedResults.Verify_the_indication_marker_shall_not_be_shown_in_area_D7();
            
            
            /*
            Test Step 3
            Action: Use the test script file 17_8_b.xml to send EVC-1 with,MMI_O_IML = 1000100000 (1000m)Note: The result of test script file may interrupted by ATP-CU, need to execute test script file repeatly to see the result.Note2: current position (OBU_TR_O_TRAIN) = 0 (1000000000)
            Expected Result: Verify the following information,The Indication Marker shall be drawn as a horizontal yellow line The length of Indication Marker shall be equal to the width of Sub-Area D7The Indication Marker shall be shown at position 1000 reference with the bottom of the horizontal yellow line and PA Distance Scale
            Test Step Comment: (1) MMI_gen 649 (partly: yellow line);(2) MMI_gen 649 (partly: length);(3) MMI_gen 650 (Partly: result of calculation, Y-coordinate); MMI_gen 7330; MMI_gen 9947;
            */
            
            
            /*
            Test Step 4
            Action: Press <Scale Down> button in area D12 to set PA Distance Scale range to 0-32000Use the test script file 17_8_c.xml to send EVC-1 with,MMI_O_IML = 1001000000 (10000m)Note: The result of test script file may interrupted by ATP-CU, need to execute test script file repeatly to see the result.Note2: current position (OBU_TR_O_TRAIN) = 0 (1000000000)
            Expected Result: Verify the Indication Marker shall be shown at position 10000 (between PA distance scale line 8000 and 16000)
            Test Step Comment: MMI_gen 650 (Partly: result of calculation, Y-coordinate);
            */
            
            
            /*
            Test Step 5
            Action: Perform the following procedure,Press <Scale Up> button in area D9 to set PA Distance Scale range to 0-1000Drive the train forward with speed 40km/h
            Expected Result: 
            */
            
            
            /*
            Test Step 6
            Action: Use the test script file 17_8_d.xml to send EVC-1 with,MMI_O_IML = 1000120000 (1200m)Note: The result of test script file may interrupted by ATP-CU, need to execute test script file repeatly to see the result
            Expected Result: Verify the indication marker shall not be shown in area D7
            Test Step Comment: MMI_gen 650 (partly: not shown by selected range);
            */
            // Call generic Action Method
            DmiActions.Use_the_test_script_file_17_8_d_xml_to_send_EVC_1_with_MMI_O_IML_1000120000_1200mNote_The_result_of_test_script_file_may_interrupted_by_ATP_CU_need_to_execute_test_script_file_repeatly_to_see_the_result();
            // Call generic Check Results Method
            DmiExpectedResults.Verify_the_indication_marker_shall_not_be_shown_in_area_D7();
            
            
            /*
            Test Step 7
            Action: Use the test script file 17_8_d.xml to send EVC-1 with,MMI_O_IML = 1000120000 (1200m)Note: The result of test script file may interrupted by ATP-CU, need to execute test script file repeatly to see the result
            Expected Result: The PA Indication Marker is display at the position lower than the PA distance scale line 1000m confirm the result of calculation between EVC-1 and EVC-7 as follows,(EVC-1) MMI_O_IML – (EVC-7) OBU_TR_O_TRAINExample: The train is stopped at position 500m. Result of calculation is [EVC-1.MMI_O_IML = 1000120000] – [EVC-7.OBU_TR_O_TRAIN = 1000050000] = 70000 (700m)
            Test Step Comment: MMI_gen 650;
            */
            // Call generic Action Method
            DmiActions.Use_the_test_script_file_17_8_d_xml_to_send_EVC_1_with_MMI_O_IML_1000120000_1200mNote_The_result_of_test_script_file_may_interrupted_by_ATP_CU_need_to_execute_test_script_file_repeatly_to_see_the_result();
            
            
            /*
            Test Step 8
            Action: Use the test script file 17_8_d.xml to send EVC-1 with,MMI_O_IML = 1000120000 (1200m)Note: The result of test script file may interrupted by ATP-CU, need to execute test script file repeatly to see the result
            Expected Result: Verify the indication marker shall not be shown in area D7
            Test Step Comment: MMI_gen 7332 (partly: Result of equation is less than zero);
            */
            // Call generic Action Method
            DmiActions.Use_the_test_script_file_17_8_d_xml_to_send_EVC_1_with_MMI_O_IML_1000120000_1200mNote_The_result_of_test_script_file_may_interrupted_by_ATP_CU_need_to_execute_test_script_file_repeatly_to_see_the_result();
            // Call generic Check Results Method
            DmiExpectedResults.Verify_the_indication_marker_shall_not_be_shown_in_area_D7();
            
            
            /*
            Test Step 9
            Action: Stop the train
            Expected Result: Train is standstill
            */
            // Call generic Action Method
            DmiActions.Stop_the_train();
            // Call generic Check Results Method
            DmiExpectedResults.Train_is_standstill();
            
            
            /*
            Test Step 10
            Action: End of test
            Expected Result: 
            */
            

            return GlobalTestResult;
        }
    }
}

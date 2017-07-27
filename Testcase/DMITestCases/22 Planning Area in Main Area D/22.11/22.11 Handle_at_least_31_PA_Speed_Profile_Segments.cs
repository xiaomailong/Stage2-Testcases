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
    /// 22.11 Handle at least 31 PA Speed Profile Segments
    /// TC-ID: 17.11
    /// 
    /// This test case verifies that the DMI can handle at least 31 PA Speed Profile segments. Also verify that the PA Speed Profile is continuously updated the value and the speed profile received from Pkt27 is applied at the correct position. This function shall comply with [MMI-ETCS-gen].
    /// 
    /// Tested Requirements:
    /// MMI_gen 7286;
    /// 
    /// Scenario:
    /// Activate cabin A. Driver performs Start of Mission to SR mode, Level 1Start driving to pass BG1 at 10m. which contains pkt 12, pkt21 and pkt 27 with 15 PA Speed ProfilesPass BG2 at 100 m which contains pkt 27 with 16 PA Speed ProfilesVerify thhat DMI shall handle at least 31 PA  speed profile segments
    /// 
    /// Used files:
    /// 17_11.tdg
    /// </summary>
    public class Handle_at_least_31_PA_Speed_Profile_Segments : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // System is power on.Train length = 100 mSet the following tags name in configuration file (See the instruction in Appendix 1)SPEED_DIAL_V_MAX = 400SPEED_DIAL_V_TRAINs = 100
            
            // Call the TestCaseBase PreExecution
            base.PreExecution();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in FS mode , level 1.

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint

            
            /*
            Test Step 1
            Action: Activate cabin A
            Expected Result: DMI displays the default window. The Driver ID window is displayed
            */
            // Call generic Action Method
            DmiActions.Activate_cabin_A();
            // Call generic Check Results Method
            DmiExpectedResults.DMI_displays_the_default_window_The_Driver_ID_window_is_displayed();
            
            
            /*
            Test Step 2
            Action: Driver performs SoM to SR mode, level 1
            Expected Result: DMI is displayed in SR mode, level 1
            */
            // Call generic Action Method
            DmiActions.Driver_performs_SoM_to_SR_mode_level_1();
            // Call generic Check Results Method
            DmiExpectedResults.DMI_is_displayed_in_SR_mode_level_1();
            
            
            /*
            Test Step 3
            Action: Drive the train forward with 40 km/h then pass BG1.Pkt 12 : L_ENDSECTION = 30000, V_MAIN = 40 (200 Km/h)Pkt 27 giving 16 segments of static speed profileD_STATIC = 0, V_STATIC = 8N_ITER = 151.      D_STATIC = 500, V_STATIC = 92.      D_STATIC = 500, V_STATIC = 103.      D_STATIC = 500, V_STATIC = 114.      D_STATIC = 500, V_STATIC = 125.      D_STATIC = 500, V_STATIC = 136.      D_STATIC = 500, V_STATIC = 147.      D_STATIC = 500, V_STATIC = 158.      D_STATIC = 500, V_STATIC = 169.      D_STATIC = 500, V_STATIC = 1710.     D_STATIC = 500, V_STATIC = 1811.     D_STATIC = 500, V_STATIC = 1912.     D_STATIC = 500, V_STATIC = 2013.     D_STATIC = 500, V_STATIC = 2114.     D_STATIC = 500, V_STATIC = 22   15.    D_STATIC = 500, V_STATIC = 23
            Expected Result: DMI changes from SR mode to FS mode.The planning area is displayed the PA Speed Profile segments
            Test Step Comment: MMI_gen 7286 (partly: PL21);
            */
            
            
            /*
            Test Step 4
            Action: Pass BG2 Pkt 27 giving 16 segments of static speed profileD_STATIC = 7900, V_STATIC = 24N_ITER = 1516.     D_STATIC = 500, V_STATIC = 2517.     D_STATIC = 500, V_STATIC = 2618.     D_STATIC = 500, V_STATIC = 2719.     D_STATIC = 500, V_STATIC = 2820.     D_STATIC = 500, V_STATIC = 2921.     D_STATIC = 500, V_STATIC = 3022.     D_STATIC = 500, V_STATIC = 2923.     D_STATIC = 500, V_STATIC = 2824.     D_STATIC = 500, V_STATIC = 2725.     D_STATIC = 500, V_STATIC = 2626.     D_STATIC = 500, V_STATIC = 2527.     D_STATIC = 500, V_STATIC = 2428.     D_STATIC = 500, V_STATIC = 2329.     D_STATIC = 500, V_STATIC = 22  30.     D_STATIC = 500, V_STATIC = 21
            Expected Result: The planning area keep showing PA Speed Profile segments
            */
            
            
            /*
            Test Step 5
            Action: Drive the train follow the permitted speed
            Expected Result: From step 6 to Step 27. Verify that the PA Speed Profile segment’s speed is higher than the the speed of the previous segment. DMI is displayed as symbol PL21 on the planning area. (see the figure in ‘Comment’ column)
            */
            // Call generic Action Method
            DmiActions.Drive_the_train_follow_the_permitted_speed();
            
            
            /*
            Test Step 6
            Action: SSP in Pkt27 of BG1 iteration 1 is supervised
            Expected Result: Verify that the CSG is indicated the speed equal 45km/h. The symbol PL21 is displayed on the planning area
            */
            
            
            /*
            Test Step 7
            Action: SSP in Pkt27 of BG1 iteration 2 is supervised
            Expected Result: Verify that the CSG is indicated the speed equal 50km/h. The symbol PL21 is displayed on the planning area
            */
            
            
            /*
            Test Step 8
            Action: SSP in Pkt27 of BG1 iteration 3 is supervised
            Expected Result: Verify that the CSG is indicated the speed equal 55km/h. The symbol PL21 is displayed on the planning area
            */
            
            
            /*
            Test Step 9
            Action: SSP in Pkt27 of BG1 iteration 4 is supervised
            Expected Result: Verify that the CSG is indicated the speed equal 60km/h. The symbol PL21 is displayed on the planning area
            */
            
            
            /*
            Test Step 10
            Action: SSP in Pkt27 of BG1 iteration 5 is supervised
            Expected Result: Verify that the CSG is indicated the speed equal 65km/h. The symbol PL21 is displayed on the planning area
            */
            
            
            /*
            Test Step 11
            Action: SSP in Pkt27 of BG1 iteration 6 is supervised
            Expected Result: Verify that the CSG is indicated the speed equal 70km/h. The symbol PL21 is displayed on the planning area
            */
            
            
            /*
            Test Step 12
            Action: SSP in Pkt27 of BG1 iteration 7 is supervised
            Expected Result: Verify that the CSG is indicated the speed equal 75km/h. The symbol PL21 is displayed on the planning area
            */
            
            
            /*
            Test Step 13
            Action: SSP in Pkt27 of BG1 iteration 8 is supervised
            Expected Result: Verify that the CSG is indicated the speed equal 80km/h. The symbol PL21 is displayed on the planning area
            */
            
            
            /*
            Test Step 14
            Action: SSP in Pkt27 of BG1 iteration 9 is supervised
            Expected Result: Verify that the CSG is indicated the speed equal 85km/h. The symbol PL21 is displayed on the planning area
            */
            
            
            /*
            Test Step 15
            Action: SSP in Pkt27 of BG1 iteration 10 is supervised
            Expected Result: Verify that the CSG is indicated the speed equal 90km/h. The symbol PL21 is displayed on the planning area
            */
            
            
            /*
            Test Step 16
            Action: SSP in Pkt27 of BG1 iteration 11 is supervised
            Expected Result: Verify that the CSG is indicated the speed equal 95km/h. The symbol PL21 is displayed on the planning area
            */
            
            
            /*
            Test Step 17
            Action: SSP in Pkt27 of BG1 iteration 12 is supervised
            Expected Result: Verify that the CSG is indicated the speed equal 100km/h. The symbol PL21 is displayed on the planning area
            */
            
            
            /*
            Test Step 18
            Action: SSP in Pkt27 of BG1 iteration 13 is supervised
            Expected Result: Verify that the CSG is indicated the speed equal 105km/h. The symbol PL21 is displayed on the planning area
            */
            
            
            /*
            Test Step 19
            Action: SSP in Pkt27 of BG1 iteration 14 is supervised
            Expected Result: Verify that the CSG is indicated the speed equal 110km/h. The symbol PL21 is displayed on the planning area
            */
            
            
            /*
            Test Step 20
            Action: SSP in Pkt27 of BG1 iteration 15 is supervised
            Expected Result: Verify that the CSG is indicated the speed equal 115km/h. The symbol PL21 is displayed on the planning area
            */
            
            
            /*
            Test Step 21
            Action: SSP in Pkt27 of BG2 iteration 16 is supervised
            Expected Result: Verify that the CSG is indicated the speed equal 120km/h. The symbol PL21 is displayed on the planning area
            */
            
            
            /*
            Test Step 22
            Action: SSP in Pkt27 of BG2 iteration 17 is supervised
            Expected Result: Verify that the CSG is indicated the speed equal 125km/h. The symbol PL21 is displayed on the planning area
            */
            
            
            /*
            Test Step 23
            Action: SSP in Pkt27 of BG2 iteration 18 is supervised
            Expected Result: Verify that the CSG is indicated the speed equal 130km/h. The symbol PL21 is displayed on the planning area
            */
            
            
            /*
            Test Step 24
            Action: SSP in Pkt27 of BG2 iteration 19 is supervised
            Expected Result: Verify that the CSG is indicated the speed equal 135km/h. The symbol PL21 is displayed on the planning area
            */
            
            
            /*
            Test Step 25
            Action: SSP in Pkt27 of BG2 iteration 20 is supervised
            Expected Result: Verify that the CSG is indicated the speed equal 140km/h. The symbol PL21 is displayed on the planning area
            */
            
            
            /*
            Test Step 26
            Action: SSP in Pkt27 of BG2 iteration 21 is supervised
            Expected Result: Verify that the CSG is indicated the speed equal 145km/h. The symbol PL21 is displayed on the planning area
            */
            
            
            /*
            Test Step 27
            Action: SSP in Pkt27 of BG2 iteration 22 is supervised
            Expected Result: Verify that the CSG is indicated the speed equal 150km/h. The symbol PL21 is displayed on the planning area
            */
            
            
            /*
            Test Step 28
            Action: SSP in Pkt27 of BG2 iteration 23 is supervised
            Expected Result: From step 28 to Step 41. Verify that the PA Speed Profile segment’s speed is lower than the the speed of the previous segment. DMI is displayed as symbol PL22 on the planning areaVerify that the CSG is indicated the speed equal 145km/h. The symbol PL22 is displayed on the planning area
            Test Step Comment: MMI_gen 7286 (partly: PL22);
            */
            
            
            /*
            Test Step 29
            Action: SSP in Pkt27 of BG2 iteration 24 is supervised
            Expected Result: Verify that the CSG is indicated the speed equal 140km/h. The symbol PL22 is displayed on the planning area
            */
            
            
            /*
            Test Step 30
            Action: SSP in Pkt27 of BG2 iteration 25 is supervised
            Expected Result: Verify that the CSG is indicated the speed equal 135km/h. The symbol PL22 is displayed on the planning area
            */
            
            
            /*
            Test Step 31
            Action: SSP in Pkt27 of BG2 iteration 26 is supervised
            Expected Result: Verify that the CSG is indicated the speed equal 130km/h. The symbol PL22 is displayed on the planning area
            */
            
            
            /*
            Test Step 32
            Action: SSP in Pkt27 of BG2 iteration 27 is supervised
            Expected Result: Verify that the CSG is indicated the speed equal 125km/h. The symbol PL22 is displayed on the planning area
            */
            
            
            /*
            Test Step 33
            Action: SSP in Pkt27 of BG2 iteration 28 is supervised
            Expected Result: Verify that the CSG is indicated the speed equal 120km/h. The symbol PL22 is displayed on the planning area
            */
            
            
            /*
            Test Step 34
            Action: SSP in Pkt27 of BG2 iteration 29 is supervised
            Expected Result: Verify that the CSG is indicated the speed equal 115km/h. The symbol PL22 is displayed on the planning area
            */
            
            
            /*
            Test Step 35
            Action: SSP in Pkt27 of BG2 iteration 30 is supervised
            Expected Result: Verify that the CSG is indicated the speed equal 110km/h. The symbol PL22 is displayed on the planning area
            */
            
            
            /*
            Test Step 36
            Action: SSP in Pkt27 of BG2 iteration 31 is supervised
            Expected Result: Verify that the CSG is indicated the speed equal 105 km/h. The symbol PL22 is displayed on the planning area
            */
            
            
            /*
            Test Step 37
            Action: Continue to drive the train forward
            Expected Result: Verify that The symbol PL23 is displayed on the planning area
            Test Step Comment: MMI_gen 7286 (partly: PL23);
            */
            // Call generic Action Method
            DmiActions.Continue_to_drive_the_train_forward();
            
            
            /*
            Test Step 38
            Action: End of test
            Expected Result: 
            */
            

            return GlobalTestResult;
        }
    }
}

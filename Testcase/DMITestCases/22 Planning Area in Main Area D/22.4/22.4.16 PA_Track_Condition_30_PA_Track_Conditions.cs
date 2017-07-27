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
    /// 22.4.16 PA Track Condition: 30 PA Track Conditions
    /// TC-ID: 17.4.16
    /// 
    /// This test case is to verify ETCS MMI can handle at least 30 PA the track conditions . 
    /// 
    /// Tested Requirements:
    /// MMI_gen 1282; MMI_gen 1317; MMI_gen 1652; MMI_gen 1050; MMI_gen 3051;
    /// 
    /// Scenario:
    /// 1.Drive the train forward pass BG0 at position 10m.BG0: pkt 12, 21 and 27 (Entering FS) 
    /// 2.Drive the train forward pass BG1 with containing 30 PA Track condtions       at position 100m 
    /// 3.Verify that PA track condition presents on Sub-Area D2, D3, D
    /// 44.Simulate commnunication loss then verify that all PA Track condtions shall be  removed
    /// 
    /// Used files:
    /// 17_4_16.tdg
    /// </summary>
    public class PA_Track_Condition_30_PA_Track_Conditions : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // Test system is power on.SoM is performed in SR mode, level 1.
            
            // Call the TestCaseBase PreExecution
            base.PreExecution();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in system failure mode

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint

            
            /*
            Test Step 1
            Action: Drive the train up to 20 km/h
            Expected Result: The speed pointer is indicated as 20  km/h
            */
            // Call generic Action Method
            DmiActions.Drive_the_train_up_to_20_kmh();
            // Call generic Check Results Method
            DmiExpectedResults.The_speed_pointer_is_indicated_as_20_kmh();
            
            
            /*
            Test Step 2
            Action: Pass BG0 with MA and Track descriptionPkt 12,21 and 27
            Expected Result: Mode changes to FS mode , L1
            */
            // Call generic Action Method
            DmiActions.Pass_BG0_with_MA_and_Track_descriptionPkt_12_21_and_27();
            // Call generic Check Results Method
            DmiExpectedResults.Mode_changes_to_FS_mode_L1();
            
            
            /*
            Test Step 3
            Action: Pass BG1 with  conatining 30 PA Track conditionPkt 65:M_TRACKCOND(0) =0M_TRACKCOND(1) =1M_TRACKCOND(2) =2M_TRACKCOND(3) =3M_TRACKCOND(4) =4M_TRACKCOND(5) =5M_TRACKCOND(6) =6M_TRACKCOND(7) =7M_TRACKCOND(8) =8M_TRACKCOND(9) = 0M_TRACKCOND(10) =1 M_TRACKCOND(11) =2M_TRACKCOND(12) =3M_TRACKCOND(13) =4M_TRACKCOND(14) =5M_TRACKCOND(15) =6M_TRACKCOND(16) =1M_TRACKCOND(17) =6M_TRACKCOND(18) =3M_TRACKCOND(19) =1M_TRACKCOND(20) =2M_TRACKCOND(21) =8M_TRACKCOND(22) =0M_TRACKCOND(23) =9M_TRACKCOND(24) =8M_TRACKCOND(25) =3M_TRACKCOND(26) =5M_TRACKCOND(27) =7M_TRACKCOND(28) =4M_TRACKCOND(29) =6
            Expected Result: DMI displays track condition symbol in sub-area D2,D3, D4
            Test Step Comment: MMI-gen_1282;MMI_gen 1317;
            */
            
            
            /*
            Test Step 4
            Action: Continue driving with 20 Km/h
            Expected Result: The PA Track condition symbols are going down to the first distance scale (zero line) and no symbol jumping between D2, D3 and D4 area
            Test Step Comment: MMI_gen 1652;MMI_gen 1050;
            */
            // Call generic Action Method
            DmiActions.Continue_driving_with_20_Kmh();
            
            
            /*
            Test Step 5
            Action: Continue driving with 20 Km/h
            Expected Result: DMI displays remianing track condition symbols on sub-area D2 and D3
            */
            // Call generic Action Method
            DmiActions.Continue_driving_with_20_Kmh();
            
            
            /*
            Test Step 6
            Action: Continue driving with 20 Km/h
            Expected Result: DMI displays remianing track condition symbols on sub-area D2
            */
            // Call generic Action Method
            DmiActions.Continue_driving_with_20_Kmh();
            
            
            /*
            Test Step 7
            Action: Simulate loss-communication between ETCS onboard and DMI
            Expected Result: DMI displays Default window with the  message “ATP Down Alarm” and sound alarmPA Track Condition symbol shall be removed from sub-area D2
            Test Step Comment: MMI_gen 3051;
            */
            // Call generic Action Method
            DmiActions.Simulate_loss_communication_between_ETCS_onboard_and_DMI();
            
            
            /*
            Test Step 8
            Action: End of test
            Expected Result: 
            */
            

            return GlobalTestResult;
        }
    }
}

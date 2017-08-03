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
    /// 16.2 Sound of Warning Speed Status and Over Speed Status in TSM
    /// TC-ID: 11.2
    /// 
    /// This test case verifes the sound played by DMI when current train speed is above the permitted speed for TSM supervision status. The sound S1 is played complied with [ERA] and the sound S2 is played when received MMI_M_WARNING = 5.
    /// 
    /// Tested Requirements:
    /// MMI_gen 5839; MMI_gen 5843; MMI_gen 4256 (partly: Sound S1 and S2 sound); MMI_gen 11921 (partly: MMI_M_WARNING = 5);
    /// 
    /// Scenario:
    /// 1.Drive the train forward pass BG1 at position 100m.      BG1: Packet 12, 21 and 27 (Entering FS)
    /// 2.Drive the train with specific speed. Then, verify the sound is playing refer to received packet EVC-1.
    /// 
    /// Used files:
    /// 11_2.tdg
    /// </summary>
    public class Sound_of_Warning_Speed_Status_and_Over_Speed_Status_in_TSM : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // System is power on.Cabin is activated.SoM is performed in SR mode, Level 1.

            // Call the TestCaseBase PreExecution
            base.PreExecution();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in FS mode, Level 1.

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint


            /*
            Test Step 1
            Action: Drive the train forward with speed = 40km/h pass BG1 at position 100m
            Expected Result: DMI displays in FS mode, Level 1
            */
            // Call generic Check Results Method
            DmiExpectedResults.DMI_displays_in_FS_mode_level_1(this);


            /*
            Test Step 2
            Action: Drive the train with speed = 41 km/h
            Expected Result: Verify the following information,(1)     The sound ‘S1’ is played once.(2)     Use the log file to confirm that DMI received packet EVC-1 with variable MMI_M_WARNING = 9 (OvS and IndS, supervision = TSM)
            Test Step Comment: (1) MMI_gen 5839 (partly: play sound S1 once); MMI_gen 4256 (partly: Sound S1 sound);(2) MM MMI_gen 5839 (partly: MMI_M_WARNING);
            */


            /*
            Test Step 3
            Action: Drive the train with speed = 45 km/hNote: dV_warning_max is defined in chapter 3 of [SUBSET-026]
            Expected Result: Verify the following information,(1)     The sound ‘S2’ is played continuously.(2)     Use the log file to confirm that DMI received packet EVC-1 with variable MMI_M_WARNING = 5 (WaS and IndS, supervision = TSM)
            Test Step Comment: (1) MMI_gen 5843 (partly: continuously play sound S2); MMI_gen 4256 (partly: Sound S2 sound); MMI_gen 11921 (partly: MMI_M_WARNING = 5);(2) MM MMI_gen 5843 (partly: MMI_M_WARNING);
            */


            /*
            Test Step 4
            Action: Drive the train with spped = 40 km/h
            Expected Result: Verify the following information,(1)     The sound ‘S2’ is muted
            Test Step Comment: (1) MMI_gen 11921 (partly: NEGATIVE, MMI_M_WARNING ≠ 5);
            */


            /*
            Test Step 5
            Action: End of test
            Expected Result: 
            */


            return GlobalTestResult;
        }
    }
}
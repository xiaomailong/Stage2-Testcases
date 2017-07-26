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
    /// 10.12.1 Navigation Button on the Planning Area
    /// TC-ID: 5.12.1
    /// 
    /// This test case verifies the navigation buttons of the planning area. This including scale up, scale down, and hide PA buttons. The symbol, type of button, states, sound, and button activation shall comply with [MMI-ETCS-gen]
    /// 
    /// Tested Requirements:
    /// MMI_gen 4392 (partly: bullet g, scale up NA03, scale down NA04, bullet h, show or hide NA01); MMI_gen 4394 (partly: scale up, scale down); MMI_gen 4396 (partly: scale up, scale down); MMI_gen 4440 (partly: shown, hide); MMI_gen 9391 (partly: MMI_gen 4381 (partly: the sound for Up-Type button, exit state ‘Pressed’, execute function associated to the button), MMI_gen 4382);
    /// 
    /// Scenario:
    /// At 100 m, pass BG1 with pkt 12, pkt 21, pkt 
    /// 27.Mode changes to FS mode.Verify the scale up/down button on the planning areaVerify the hide and show button on the planning area
    /// 
    /// Used files:
    /// 5_12_1.tdg
    /// </summary>
    public class Navigation_Button_on_the_Planning_Area : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // Configure HIDE_PA_FUNCTION to 0 (ON). See an instruction in Appendix 1Test system is power on. SoM is performed in SR mode, Level 1.

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
            Action: Drive the train forward with 40 km/h and pass BG1
            Expected Result: DMI displays  “Entering FS” message.The planning area is displayed the planning information.Scale Up button  NA03 is enabled in Sub-area D9Scale Down button  NA04 is enabled in Sub-area D12Show/Hide PA button NA01 is enabled in Sub-area D14
            Test Step Comment: (1) MMI_gen 4392 (partly: bullet g, scale up NA03);(2) MMI_gen 4392 (partly: bullet g, scale down NA04);(3) MMI_gen 4392 (partly: bullet h, show or hide NA01); MMI_gen 4440 (partly: hide, enabled);
            */

            /*
            Test Step 2
            Action: Press and hold scale up button
            Expected Result: The distance range on the Planning area is not changed.The sound ‘click’ is played once.
            Test Step Comment: (1) MMI_gen 9391 (partly: MMI_gen 4381 (partly: the sound for Up-Type button));
            */

            /*
            Test Step 3
            Action: Try to move outside of the scale up button area
            Expected Result: The distance range on the Planning area is not changed.The sound ‘click’ is not played.
            Test Step Comment: (1) MMI_gen 9391 (partly: MMI_gen 4382 (partly: when slide out with force applied, no sound));
            */

            /*
            Test Step 4
            Action: Try to move back into the scale up button area
            Expected Result: The distance range on the Planning area is not changed.The sound ‘click’ is not played.
            Test Step Comment: (1) MMI_gen 9391 (partly: MMI_gen 4382 (partly: when slide back, no sound));
            */

            /*
            Test Step 5
            Action: Release the scale up button
            Expected Result: Verify the following information,The distance range on the planning area is shifted to the next lower range.
            Test Step Comment: (1) MMI_gen 9391 (partly: MMI_gen 4381 (partly: exit state ‘Pressed’, execute function associated to the button)); 
            */

            /*
            Test Step 6
            Action: Press and release scale up button until the distance range is [0…1000]
            Expected Result: Verify the following information,The scale up button is changed to disabled state, The disabled scale up button NA05 symbol is replace in Sub-area D9.
            Test Step Comment: (1) MMI_gen 4394 (partly: Scale up);(2) MMI_gen 4396 (partly: Scale Up, NA05); MMI_gen 4440 (partly: other navigation buttons);
            */

            /*
            Test Step 7
            Action: Press and hold scale down button
            Expected Result: The distance range on the Planning range is not changed.The sound ‘click’ is played once.
            Test Step Comment: (1) MMI_gen 9391 (partly: MMI_gen 4381 (partly: the sound for Up-Type button));
            */

            /*
            Test Step 8
            Action: Try to move outside of the scale down button area
            Expected Result: The distance range on the Planning range is not changed.The sound ‘click’ is not played.
            Test Step Comment: (1) MMI_gen 9391 (partly: MMI_gen 4382 (partly: when slide out with force applied, no sound));
            */

            /*
            Test Step 9
            Action: Try to move back into the scale down button area
            Expected Result: The distance range on the Planning range is not changed.The sound ‘click’ is not played.
            Test Step Comment: (1) MMI_gen 9391 (partly: MMI_gen 4382 (partly: when slide back, no sound));
            */

            /*
            Test Step 10
            Action: Release scale down button
            Expected Result: Verify the following information,The distance range on the planning area is shifted to the next higher range.The eanbled scale up button NA03 symbol is replace in Sub-area D9.
            Test Step Comment: (1) MMI_gen 9391 (partly: MMI_gen 4381 (partly: exit state ‘Pressed’, execute function associated to the button));(2) MMI_gen 4396 (partly: Scale Up, NA03);
            */

            /*
            Test Step 11
            Action: Press and release scale down button until the distance range is [0…32000]
            Expected Result: Verify the following information,The scale down button is changed to disabled state. The disabled scale up button NA06 symbol is replace in Sub-area D12.
            Test Step Comment: (1) MMI_gen 4394 (partly: Scale down);(2) MMI_gen 4396 (partly: Scale down, NA06); MMI_gen 4440 (partly: other navigation buttons);
            */

            /*
            Test Step 12
            Action: Press scale up button. 
            Expected Result: Verify the following information,The enabled scale up button NA04 symbol is replace in Sub-area D12.
            Test Step Comment: (1) MMI_gen 4396 (partly: Scale Down, NA04);
            */

            /*
            Test Step 13
            Action: Press and hold hide PA button
            Expected Result: DMI still displays the planning area.The sound ‘click’ is played once.
            Test Step Comment: (1) MMI_gen 9391 (partly: MMI_gen 4381 (partly: the sound for Up-Type button));
            */

            /*
            Test Step 14
            Action: Try to move outside of the hide PA button area
            Expected Result: DMI still displays the planning area.The sound ‘click’ is not played.
            Test Step Comment: (1) MMI_gen 9391 (partly: MMI_gen 4382 (partly: when slide out with force applied, no sound));
            */

            /*
            Test Step 15
            Action: Try to move back into the hide PA button area
            Expected Result: DMI still displays the planning area.The sound ‘click’ is not played.
            Test Step Comment: (1) MMI_gen 9391 (partly: MMI_gen 4382 (partly: when slide back, no sound));
            */

            /*
            Test Step 16
            Action: Release hide PA button
            Expected Result: Verify the following information,The planning area is hidden. The main area D is empty.The NA01 symbol is still display in Sub-area D14.
            Test Step Comment: (1) MMI_gen 9391 (partly: MMI_gen 4381 (partly: exit state ‘Pressed’, execute function associated to the button));(2) MMI_gen 4404 (partly: show, enabled); 
            */

            /*
            Test Step 17
            Action: Press and hold in area D 
            Expected Result: The planning area is still hidden. The sound ‘click’ is played once
            Test Step Comment: (1) MMI_gen 9391 (partly: MMI_gen 4381 (partly: the sound for Up-Type button));
            */

            /*
            Test Step 18
            Action: Try to move outside  of the area D
            Expected Result: The planning area still hidden.The sound ‘click’ is not played.
            Test Step Comment: (1) MMI_gen 9391 (partly: MMI_gen 4382 (partly: when slide out with force applied, no sound));
            */

            /*
            Test Step 19
            Action: Try to move back into area D 
            Expected Result: The planning area still hidden.The sound ‘click’ is not played.
            Test Step Comment: (1) MMI_gen 9391 (partly: MMI_gen 4382 (partly: when slide back, no sound));
            */

            /*
            Test Step 20
            Action: Release area D
            Expected Result: Verify the following information,DMI displays the planning area.
            Test Step Comment: (1) MMI_gen 9391 (partly: MMI_gen 4381 (partly: exit state ‘Pressed’, execute function associated to the button));
            */

            /*
            Test Step 21
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
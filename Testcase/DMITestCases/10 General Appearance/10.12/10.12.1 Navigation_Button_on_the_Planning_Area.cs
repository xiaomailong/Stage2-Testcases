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
    public class TC_ID_5_12_1_Navigation_Buttons : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:

            // Call the TestCaseBase PreExecution
            base.PreExecution();

            // Configure HIDE_PA_FUNCTION to 0 (ON). See an instruction in Appendix 1Test system is power on. SoM is performed in SR mode, Level 1.
            DmiActions.Complete_SoM_L1_SR(this);
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in FS mode, Level 1.
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
            Action: Drive the train forward with 40 km/h and pass BG1
            Expected Result: DMI displays  “Entering FS” message.The planning area is displayed the planning information.Scale Up button  NA03 is enabled in Sub-area D9Scale Down button  NA04 is enabled in Sub-area D12Show/Hide PA button NA01 is enabled in Sub-area D14
            Test Step Comment: (1) MMI_gen 4392 (partly: bullet g, scale up NA03);(2) MMI_gen 4392 (partly: bullet g, scale down NA04);(3) MMI_gen 4392 (partly: bullet h, show or hide NA01); MMI_gen 4440 (partly: hide, enabled);
            */
            // signal to DMI after BG is passed??
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 40;
            WaitForVerification("Check the following and the planning information in the planning area:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the message ‘Entering FS’." + Environment.NewLine +
                                "2. Scale Up button NA03 is displayed enabled in sub-area D9." + Environment.NewLine +
                                "3. Scale Down button NA04 is displayed enabled in sub-area D12." + Environment.NewLine +
                                "4.	Show/Hide PA button NA01 is enabled in Sub-area D14");

            /*
            Test Step 2
            Action: Press and hold scale up button
            Expected Result: The distance range on the Planning area is not changed.The sound ‘click’ is played once
            Test Step Comment: (1) MMI_gen 9391 (partly: MMI_gen 4381 (partly: the sound for Up-Type button));
            */
            WaitForVerification("Press and keep the Scale Up button pressed and check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The distance range in the planning area does not change." + Environment.NewLine +
                                "2. The ‘click’ sound is played once.");

            /*
            Test Step 3
            Action: Try to move outside of the scale up button area
            Expected Result: The distance range on the Planning area is not changed.The sound ‘click’ is not played
            Test Step Comment: (1) MMI_gen 9391 (partly: MMI_gen 4382 (partly: when slide out with force applied, no sound));
            */
            WaitForVerification("Drag the Scale Up button outside of its area, keeping the button pressed, and check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The distance range in the planning area does not change." + Environment.NewLine +
                                "2. The ‘click’ sound is not played.");

            /*
            Test Step 4
            Action: Try to move back into the scale up button area
            Expected Result: The distance range on the Planning area is not changed.The sound ‘click’ is not played
            Test Step Comment: (1) MMI_gen 9391 (partly: MMI_gen 4382 (partly: when slide back, no sound));
            */
            WaitForVerification("Whilst keeping the Scale Up button pressed, drag it back inside its area and check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The distance range in the planning area does not change." + Environment.NewLine +
                                "2. The ‘click’ sound is not played.");

            /*
            Test Step 5
            Action: Release the scale up button
            Expected Result: Verify the following information,The distance range on the planning area is shifted to the next lower range
            Test Step Comment: (1) MMI_gen 9391 (partly: MMI_gen 4381 (partly: exit state ‘Pressed’, execute function associated to the button)); 
            */
            WaitForVerification("Release the Scale Up button and check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The distance range in the planning area changes to the next lower range.");

            /*
            Test Step 6
            Action: Press and release scale up button until the distance range is [0…1000]
            Expected Result: Verify the following information,The scale up button is changed to disabled state, The disabled scale up button NA05 symbol is replace in Sub-area D9
            Test Step Comment: (1) MMI_gen 4394 (partly: Scale up);(2) MMI_gen 4396 (partly: Scale Up, NA05); MMI_gen 4440 (partly: other navigation buttons);
            */
            WaitForVerification("Press and release the Scale Up button until the distance range is [0..1000] and check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The Scale Up button is displayed disabled." + Environment.NewLine +
                                "2. The disabled Scale Up button symbol NA05 is displayed in sub-area D9");

            /*
            Test Step 7
            Action: Press and hold scale down button
            Expected Result: The distance range on the Planning range is not changed.The sound ‘click’ is played once
            Test Step Comment: (1) MMI_gen 9391 (partly: MMI_gen 4381 (partly: the sound for Up-Type button));
            */
            WaitForVerification("Press and keep the Scale Down button pressed and check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The distance range in the planning area does not change." + Environment.NewLine +
                                "2. The ‘click’ sound is played once.");

            /*
            Test Step 8
            Action: Try to move outside of the scale down button area
            Expected Result: The distance range on the Planning range is not changed.The sound ‘click’ is not played
            Test Step Comment: (1) MMI_gen 9391 (partly: MMI_gen 4382 (partly: when slide out with force applied, no sound));
            */
            WaitForVerification("Drag the Scale Down button outside of its area, keeping the button pressed, and check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The distance range in the planning area does not change." + Environment.NewLine +
                                "2. The ‘click’ sound is not played.");

            /*
            Test Step 9
            Action: Try to move back into the scale down button area
            Expected Result: The distance range on the Planning range is not changed.The sound ‘click’ is not played
            Test Step Comment: (1) MMI_gen 9391 (partly: MMI_gen 4382 (partly: when slide back, no sound));
            */
            WaitForVerification("Whilst keeping the Scale Down button pressed, drag it back inside its area and check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The distance range in the planning area does not change." + Environment.NewLine +
                                "2. The ‘click’ sound is not played.");

            /*
            Test Step 10
            Action: Release scale down button
            Expected Result: Verify the following information,The distance range on the planning area is shifted to the next higher range.The eanbled scale up button NA03 symbol is replace in Sub-area D9
            Test Step Comment: (1) MMI_gen 9391 (partly: MMI_gen 4381 (partly: exit state ‘Pressed’, execute function associated to the button));(2) MMI_gen 4396 (partly: Scale Up, NA03);
            */
            WaitForVerification("Release the Scale Down button and check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The distance range in the planning area changes to the next higher range.");

            /*
            Test Step 11
            Action: Press and release scale down button until the distance range is [0…32000]
            Expected Result: Verify the following information,The scale down button is changed to disabled state. The disabled scale up button NA06 symbol is replace in Sub-area D12
            Test Step Comment: (1) MMI_gen 4394 (partly: Scale down);(2) MMI_gen 4396 (partly: Scale down, NA06); MMI_gen 4440 (partly: other navigation buttons);
            */
            // spec says up button but down is displayed
            WaitForVerification("Press and release the Scale Down button until the distance range is [0..32000] and check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The Scale Down button is displayed disabled." + Environment.NewLine +
                                "2. The disabled Scale Down button symbol NA06 is displayed in sub-area D12");

            /*
            Test Step 12
            Action: Press scale up button
            Expected Result: Verify the following information,The enabled scale up button NA04 symbol is replace in Sub-area D12
            Test Step Comment: (1) MMI_gen 4396 (partly: Scale Down, NA04);
            */
            WaitForVerification("Press Scale Up button and check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The enabled Scale Down button symbol NA04 is displayed in sub-area D12.");

            /*
            Test Step 13
            Action: Press and hold hide PA button
            Expected Result: DMI still displays the planning area.The sound ‘click’ is played once
            Test Step Comment: (1) MMI_gen 9391 (partly: MMI_gen 4381 (partly: the sound for Up-Type button));
            */
            WaitForVerification("Press and keep the hide PA button pressed and check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI still displays the planning area." + Environment.NewLine +
                                "2. The ‘click’ sound is played once.");

            /*
            Test Step 14
            Action: Try to move outside of the hide PA button area
            Expected Result: DMI still displays the planning area.The sound ‘click’ is not played
            Test Step Comment: (1) MMI_gen 9391 (partly: MMI_gen 4382 (partly: when slide out with force applied, no sound));
            */
            WaitForVerification("Drag the hide PA button outside of its area, keeping the button pressed, and check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI still displays the planning area." + Environment.NewLine +
                                "2. The ‘click’ sound is not played.");

            /*
            Test Step 15
            Action: Try to move back into the hide PA button area
            Expected Result: DMI still displays the planning area.The sound ‘click’ is not played
            Test Step Comment: (1) MMI_gen 9391 (partly: MMI_gen 4382 (partly: when slide back, no sound));
            */
            WaitForVerification("Whilst keeping the hide PA button pressed, drag it back inside its area and check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI still displays the planning area." + Environment.NewLine +
                                "2. The ‘click’ sound is not played.");

            /*
            Test Step 16
            Action: Release hide PA button
            Expected Result: Verify the following information,The planning area is hidden. The main area D is empty.The NA01 symbol is still display in Sub-area D14
            Test Step Comment: (1) MMI_gen 9391 (partly: MMI_gen 4381 (partly: exit state ‘Pressed’, execute function associated to the button));(2) MMI_gen 4404 (partly: show, enabled); 
            */
            WaitForVerification("Release the hide PA button and check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The planning area is hidden." + Environment.NewLine +
                                "2. The main area D is empty." + Environment.NewLine +
                                "3. The NA01 symbol is still displayed in sub-area D14");

            /*
            Test Step 17
            Action: Press and hold in area D
            Expected Result: The planning area is still hidden. The sound ‘click’ is played once
            Test Step Comment: (1) MMI_gen 9391 (partly: MMI_gen 4381 (partly: the sound for Up-Type button));
            */
            WaitForVerification("Press and keep the planning area pressed and check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The planning area is still hidden." + Environment.NewLine +
                                "2. The ‘click’ sound is played once.");

            /*
            Test Step 18
            Action: Try to move outside  of the area D
            Expected Result: The planning area still hidden.The sound ‘click’ is not played
            Test Step Comment: (1) MMI_gen 9391 (partly: MMI_gen 4382 (partly: when slide out with force applied, no sound));
            */
            WaitForVerification("Whilst still pressing on the screen, drag outside the planning area and check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The planning area is still hidden." + Environment.NewLine +
                                "2. The ‘click’ sound is not played.");

            /*
            Test Step 19
            Action: Try to move back into area D
            Expected Result: The planning area still hidden.The sound ‘click’ is not played
            Test Step Comment: (1) MMI_gen 9391 (partly: MMI_gen 4382 (partly: when slide back, no sound));
            */
            WaitForVerification("Whilst still pressing on the screen, drag back inside the planning area and check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The planning area is still hidden." + Environment.NewLine +
                                "2. The ‘click’ sound is not played.");

            /*
            Test Step 20
            Action: Release area D
            Expected Result: Verify the following information,DMI displays the planning area
            Test Step Comment: (1) MMI_gen 9391 (partly: MMI_gen 4381 (partly: exit state ‘Pressed’, execute function associated to the button));
            */
            WaitForVerification("Release the planning area and check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The planning area is displayed.");

            /*
            Test Step 21
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
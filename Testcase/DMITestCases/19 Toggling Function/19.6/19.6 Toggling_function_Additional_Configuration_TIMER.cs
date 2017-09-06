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
    /// 19.6 Toggling function: Additional Configuration ‘TIMER’
    /// TC-ID: 14.6
    /// 
    /// This test case verifies the display of related objects, basic speed hook(s), release speed digital and distance to target digital when driver activate the toggle function in cluding with retriggerable function, driver activate toggle function while the related objects still displaying.
    /// 
    /// Tested Requirements:
    /// MMI_gen 6897;
    /// 
    /// Scenario:
    /// 1.Perform SoM in SR mode, level 
    /// 1.Then, set the SR speed and SR distance.
    /// 2.Press on the specifc area to verify the toggle function.
    /// 3.Drive the train forward pass BG1 at position 100m. Then, acknowledge the OS mode and stop the train.BG1: packet 12, 21, 27 and 80 (Entering OS)
    /// 4.Press on the specifc area to verify the toggle function.
    /// 5.Perform the procedure to enter SH mode. Then, press on the specifc area to verify the toggle function.
    /// 
    /// Used files:
    /// 14_6.tdg
    /// </summary>
    public class Toggling_function_Additional_Configuration_TIMER2 : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // The DMI is configured TOGGLE_FUNCTION = 2 (‘TIMER’) and TOGGLE_TIMER = 10 (10 seconds), see Appendix 1.System is power on.Cabin is activated.

            // Call the TestCaseBase PreExecution
            base.PreExecution();

            EVC0_MMIStartATP.Evc0Type = EVC0_MMIStartATP.EVC0Type.GoToIdle;
            EVC0_MMIStartATP.Send();

            // Set train running number, cab 1 active, and other defaults
            DmiActions.Activate_Cabin_1(this);
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in SH mode, Level 1.
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in SH mode, Level 1.");

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint

            /*
            Test Step 1
            Action: Perform SoM in SR mode, level 1
            Expected Result: DMI displays Default window in SR mode, level 1.Note: The basic speed hook is appear for 10 seconds
            */
            DmiActions.ShowInstruction(this, "Perform SoM in SR mode, level 1.");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays Default window in SR mode, level 1." + Environment.NewLine +
                                "2. The Basic speed hook is displayed for 10s.");

            /*
            Test Step 2
            Action: Perform the following procedure, Press ‘Spec’ buttonPress ‘SR speed/distance’ buttonEnter and confirm the following data,SR speed = 40 km/hSR distance = 300 m
            Expected Result: DMI displays Special window
            */
            DmiActions.ShowInstruction(this, "Press the ‘Spec’ button. Press the ‘SR speed/distance’ button. Enter and confirm the following data, SR speed = 40 km/hSR distance = 300 m");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Special window.");

            /*
            Test Step 3
            Action: Perform the following procedure, Press ‘Close’ buttonPress on sub-area A1.Note: Stopwatch is required
            Expected Result: Verify the following information,(1)   The following objects are displays for 10 seconds before disappear.White basic speed hookMedium-grey basic speed hookDistance to target (digital)
            Test Step Comment: (1) MMI_gen 6897 (partly: switch on the affected ETCS-objects for the configured duration);
            */
            DmiActions.ShowInstruction(this, "Press the ‘Close’ button. Press in sub-area A1");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the White basic speed hook for 10s then removes it." + Environment.NewLine +
                                "2. DMI displays the Medium-grey basic speed hook for 10s then removes it." + Environment.NewLine +
                                "3. DMI displays the Digital distance to target for 10s then removes it.");

            /*
            Test Step 4
            Action: Repeat action step 3 for sub-area A2-A4 and area B respectively
            Expected Result: See expected result of step 3
            */
            DmiActions.ShowInstruction(this, "Press the ‘Close’ button. Press in sub-area A2");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the White basic speed hook for 10s then removes it." + Environment.NewLine +
                                "2. DMI displays the Medium-grey basic speed hook for 10s then removes it." + Environment.NewLine +
                                "3. DMI displays the Digital distance to target for 10s then removes it.");

            DmiActions.ShowInstruction(this, "Press the ‘Close’ button. Press in sub-area A3");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the White basic speed hook for 10s then removes it." + Environment.NewLine +
                                "2. DMI displays the Medium-grey basic speed hook for 10s then removes it." + Environment.NewLine +
                                "3. DMI displays the Digital distance to target for 10s then removes it.");

            DmiActions.ShowInstruction(this, "Press the ‘Close’ button. Press in sub-area A4");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the White basic speed hook for 10s then removes it." + Environment.NewLine +
                                "2. DMI displays the Medium-grey basic speed hook for 10s then removes it." + Environment.NewLine +
                                "3. DMI displays the Digital distance to target for 10s then removes it.");

            DmiActions.ShowInstruction(this, "Press the ‘Close’ button. Press in area B");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the White basic speed hook for 10s then removes it." + Environment.NewLine +
                                "2. DMI displays the Medium-grey basic speed hook for 10s then removes it." + Environment.NewLine +
                                "3. DMI displays the Digital distance to target for 10s then removes it.");

            /*
            Test Step 5
            Action: Perform the following procedure, Press on sub-area A1.Wait for 5 secondsPress on sub-area A1 again
            Expected Result: Verify the following information,(1)    The following objects are displays for 10 seconds before disappear.White basic speed hookMedium-grey basic speed hookDistance to target (digital)
            Test Step Comment: (1) MMI_gen 6897 (partly: retriggerable);
            */
            DmiActions.ShowInstruction(this, "Press in sub-area A1. Wait 5s, then press in sub-area A1 again");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the White basic speed hook for 10s then removes it." + Environment.NewLine +
                                "2. DMI displays the Medium-grey basic speed hook for 10s then removes it." + Environment.NewLine +
                                "3. DMI displays the Digital distance to target for 10s then removes it.");

            /*
            Test Step 6
            Action: Repeat action step 5 for sub-area A2-A4 and area B respectively
            Expected Result: See expected result of step 5
            */
            DmiActions.ShowInstruction(this, "Press in sub-area A2. Wait 5s, then press in sub-area A2 again");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the White basic speed hook for 10s then removes it." + Environment.NewLine +
                                "2. DMI displays the Medium-grey basic speed hook for 10s then removes it." + Environment.NewLine +
                                "3. DMI displays the Digital distance to target for 10s then removes it.");

            DmiActions.ShowInstruction(this, "Press in sub-area A3. Wait 5s, then press in sub-area A3 again");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the White basic speed hook for 10s then removes it." + Environment.NewLine +
                                "2. DMI displays the Medium-grey basic speed hook for 10s then removes it." + Environment.NewLine +
                                "3. DMI displays the Digital distance to target for 10s then removes it.");

            DmiActions.ShowInstruction(this, "Press in sub-area A4. Wait 5s, then press in sub-area A4 again");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the White basic speed hook for 10s then removes it." + Environment.NewLine +
                                "2. DMI displays the Medium-grey basic speed hook for 10s then removes it." + Environment.NewLine +
                                "3. DMI displays the Digital distance to target for 10s then removes it.");

            DmiActions.ShowInstruction(this, "Press in area B. Wait 5s, then press in area B again");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the White basic speed hook for 10s then removes it." + Environment.NewLine +
                                "2. DMI displays the Medium-grey basic speed hook for 10s then removes it." + Environment.NewLine +
                                "3. DMI displays the Digital distance to target for 10s then removes it.");

            /*
            Test Step 7
            Action: Drive the train forward with speed = 10 km/h
            Expected Result: Train is moving forward
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 10;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays speed = 10 km/h.");

            /*
            Test Step 8
            Action: Drive the train forward pass BG1. Then, press an acknowledgement of OS mode in sub-area C1
            Expected Result: DMI displays in OS mode, level 1.Note: The basic speed hook is appear for 10 seconds
            */
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.OnSight;


            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in OS mode, level 1." + Environment.NewLine +
                                "2. DMI displays the White basic speed hook for 10s then removes it.");

            /*
            Test Step 9
            Action: Stop the train.Then, repeat action step 3-5
            Expected Result: See expected result of step 3-5 for the following objects,White basic speed hookMedium-grey basic speed hookDistance to target (digital)Release speed digital
            Test Step Comment: (1) MMI_gen 6897;
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 0;

            DmiActions.ShowInstruction(this, "Press the ‘Close’ button. Press in sub-area A1");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the White basic speed hook for 10s then removes it." + Environment.NewLine +
                                "2. DMI displays the Medium-grey basic speed hook for 10s then removes it." + Environment.NewLine +
                                "3. DMI displays the Digital distance to target for 10s then removes it." + Environment.NewLine +
                                "4. DMI does not display the Digital release speed");

            DmiActions.ShowInstruction(this, "Press the ‘Close’ button. Press in sub-area A2");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the White basic speed hook for 10s then removes it." + Environment.NewLine +
                                "2. DMI displays the Medium-grey basic speed hook for 10s then removes it." + Environment.NewLine +
                                "3. DMI displays the Digital distance to target for 10s then removes it." + Environment.NewLine +
                                "4. DMI does not display the Digital release speed");

            DmiActions.ShowInstruction(this, "Press the ‘Close’ button. Press in sub-area A3");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the White basic speed hook for 10s then removes it." + Environment.NewLine +
                                "2. DMI displays the Medium-grey basic speed hook for 10s then removes it." + Environment.NewLine +
                                "3. DMI displays the Digital distance to target for 10s then removes it." + Environment.NewLine +
                                "4. DMI does not display the Digital release speed");

            DmiActions.ShowInstruction(this, "Press the ‘Close’ button. Press in sub-area A4");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the White basic speed hook for 10s then removes it." + Environment.NewLine +
                                "2. DMI displays the Medium-grey basic speed hook for 10s then removes it." + Environment.NewLine +
                                "3. DMI displays the Digital distance to target for 10s then removes it." + Environment.NewLine +
                                "4. DMI does not display the Digital release speed");

            DmiActions.ShowInstruction(this, "Press the ‘Close’ button. Press in area B");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the White basic speed hook for 10s then removes it." + Environment.NewLine +
                                "2. DMI displays the Medium-grey basic speed hook for 10s then removes it." + Environment.NewLine +
                                "3. DMI displays the Digital distance to target for 10s then removes it." + Environment.NewLine +
                                "4. DMI does not display the Digital release speed");

            DmiActions.ShowInstruction(this, "Press in sub-area A1. Wait 5s, then press in sub-area A1 again");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the White basic speed hook for 10s then removes it." + Environment.NewLine +
                                "2. DMI displays the Medium-grey basic speed hook for 10s then removes it." + Environment.NewLine +
                                "3. DMI displays the Digital distance to target for 10s then removes it." + Environment.NewLine + " +
                                "4. DMI does not display the Digital release speed");
                        
            /*
            Test Step 10
            Action: Perform the following procedure,Press ‘Main’ button.Press and hold ‘Shunting’ button at least 2 seconds.Release the pressed button
            Expected Result: DMI displays in SH mode, level 1.Note: The basic speed hook is appear for 10 seconds
            */
            DmiActions.ShowInstruction(this, "Press ‘Main’ button.Press and hold ‘Shunting’ button at least 2 seconds. Release the ‘Shunting’ button");

            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.Shunting;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in SH mode, level 1" + Environment.NewLine +
                                "2. DMI displays the Basic speed hook for 10s then removes it.");

            /*
            Test Step 11
            Action: Repeat action step 3-5
            Expected Result: See expected result of step 3-5 for the following objects,White basic speed hookMedium-grey basic speed hook (if any)
            Test Step Comment: (1) MMI_gen 6897;
            */
            DmiActions.ShowInstruction(this, "Press the ‘Close’ button. Press in sub-area A1");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the White basic speed hook for 10s then removes it." + Environment.NewLine +
                                "2. DMI displays the Medium-grey basic speed hook for 10s then removes it.");

            DmiActions.ShowInstruction(this, "Press the ‘Close’ button. Press in sub-area A2");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the White basic speed hook for 10s then removes it." + Environment.NewLine +
                                "2. DMI displays the Medium-grey basic speed hook for 10s then removes it.");

            DmiActions.ShowInstruction(this, "Press the ‘Close’ button. Press in sub-area A3");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the White basic speed hook for 10s then removes it." + Environment.NewLine +
                                "2. DMI displays the Medium-grey basic speed hook for 10s then removes it.");

            DmiActions.ShowInstruction(this, "Press the ‘Close’ button. Press in sub-area A4");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the White basic speed hook for 10s then removes it." + Environment.NewLine +
                                "2. DMI displays the Medium-grey basic speed hook for 10s then removes it.");

            DmiActions.ShowInstruction(this, "Press the ‘Close’ button. Press in area B");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the White basic speed hook for 10s then removes it." + Environment.NewLine +
                                "2. DMI displays the Medium-grey basic speed hook for 10s then removes it.");

            DmiActions.ShowInstruction(this, "Press in sub-area A1. Wait 5s, then press in sub-area A1 again");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the White basic speed hook for 10s then removes it." + Environment.NewLine +
                                "2. DMI displays the Medium-grey basic speed hook for 10s then removes it.");            

            /*
            Test Step 12
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
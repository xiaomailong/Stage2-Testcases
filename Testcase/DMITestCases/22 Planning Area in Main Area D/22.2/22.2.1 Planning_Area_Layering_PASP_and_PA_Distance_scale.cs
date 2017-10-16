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
    /// 22.2.1 Planning Area-Layering: PASP and PA Distance scale
    /// TC-ID: 17.2.1
    /// 
    /// This test case verifies order of each objects in Planning area which ascending order from background to foreground refer to chapter 7.3.2 of requirement specification.
    /// 
    /// Tested Requirements:
    /// MMI_gen 7108;
    /// 
    /// Scenario:
    /// Activate Cabin A.Perform SoM to SR mode, Level 1.Drive the train forward pass BG
    /// 1.Then, verify that DMI displays layer order of PA’s objects correctly.BG1: packet 12, 21,27 and 68
    /// 
    /// Used files:
    /// 17_2_1.tdg
    /// </summary>
    public class Planning_Area_Layering_PASP_and_PA_Distance_scale : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // System is power on.

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
            Action: Activate cabin A
            Expected Result: DMI displays Driver ID window
            */
            DmiActions.Activate_Cabin_1(this);

            EVC14_MMICurrentDriverID.MMI_X_DRIVER_ID = "1234";
            EVC14_MMICurrentDriverID.Send();

            DmiExpectedResults.Driver_ID_window_displayed(this);

            /*
            Test Step 2
            Action: Perform SoM in SR mode, Level 1
            Expected Result: DMI displays in SR mode, level 1
            */
            DmiActions.Perform_SoM_in_SR_mode_Level_1(this);
            
            DmiExpectedResults.SR_Mode_displayed(this);


            /*
            Test Step 3
            Action: Driver drives the train passing BG1
            Expected Result: DMI changes from SR mode to FS mode.Verify the order (background to fore ground) for each objects in PA as follows,PASPPA Distance ScaleIndication MarkerPA Track Condition, Gradient profile and Speed DiscontinuitiesHide/Show and Zoom PA buttons.Note: The object which have a lower order (i.e. PASP) cannot overlap the higher order object
            Test Step Comment: MMI_gen 7108;
            */
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.FullSupervision;

            WaitForVerification("Check that the following objects are displayed in order (from background to foreground):" + Environment.NewLine + Environment.NewLine +
                                "1. PASP." + Environment.NewLine +
                                "2. PA Distance Scale." + Environment.NewLine +
                                "3. Indication marker." + Environment.NewLine +
                                "4. PA Track Condition, Gradient Profile and Speed Discontinuities." + Environment.NewLine +
                                "5. Hide/Show and Zoom PA buttons." + Environment.NewLine +
                                "6. An object in the background of another object does not overlap it." + Environment.NewLine);
            /*
            Test Step 4
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
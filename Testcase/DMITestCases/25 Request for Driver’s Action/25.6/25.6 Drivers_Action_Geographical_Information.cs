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
using Testcase.Telegrams.DMItoEVC;
using Testcase.Telegrams.EVCtoDMI;

namespace Testcase.DMITestCases
{
    /// <summary>
    /// 25.6 Driver’s Action: Geographical Information
    /// TC-ID: 20.6
    /// 
    /// This test case verifies that DMI sends values of [MMI_DRIVER_REQUEST (EVC-101).MMI_M_REQUEST] correctly when a driver presses on sub-area G12 for requesting geographical information.
    /// 
    /// Tested Requirements:
    /// MMI_gen 151 (partly: MMI_M_REQUEST = 8); MMI_gen 11470 (partly: Bit # 30 and 33);
    /// 
    /// Scenario:
    /// 1.Drive the train forward pass BG1 at position 100mBG1: Packet 79 (Geographical Information)
    /// 2.Press at sub-area G
    /// 12.Then, verify the value in packet EVC-101.
    /// 
    /// Used files:
    /// 20_6.tdg
    /// </summary>
    public class Drivers_Action_Geographical_Information : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:

            // Call the TestCaseBase PreExecution
            base.PreExecution();

            // Test system is powered on.Cabin is activated.SoM is performed in SR mode, level 1.
            DmiActions.Complete_SoM_L1_SR(this);
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in SR mode, level 1

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint


            /*
            Test Step 1
            Action: Drive the train forward pass BG1
            Expected Result: The symbol ‘DR03’ is displayed in sub-area G12
            */
            // Not indicated what the default state of the G12 area indicator is: displayed or not
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays symbol DR03 in sub-area G12.");

            /*
            Test Step 2
            Action: Press on the ‘DR03’ symbol, sub-area G12
            Expected Result: DMI display the Geographical Position Indicator on sub-area G12.Verify the following information(1)   Use the log file to confirm that DMI sends out packet [MMI_DRIVER_REQUEST (EVC-101)] with the value of variable MMI_M_REQUEST = 8 (Geographical position request).(2)   Use the log file to confirm that DMI sends out packet [MMI_DRIVER_ACTION (EVC-152)] with the value of variable MMI_M_DRIVER_ACTION refer to sequence below,a)   MMI_M_DRIVER_ACTION = 30 (Request to show geographical position)
            Test Step Comment: (1) MMI_gen 151 (partly: MMI_M_REQUEST = 8);(2) MMI_gen 11470 (partly: Bit # 30);
            */
            DmiActions.ShowInstruction(this, @"Press on the ‘DR03’ symbol in sub-area G12");

            EVC101_MMIDriverRequest.CheckMRequestPressed = Variables.MMI_M_REQUEST.GeographicalPositionRequest;
            EVC152_MMIDriverAction.Check_MMI_M_DRIVER_ACTION = EVC152_MMIDriverAction.MMI_M_DRIVER_ACTION.RequestToShowGeographicalPosition;

            EVC5_MMIGeoPosition.MMI_M_ABSOLUTPOS = 8388609;
            EVC5_MMIGeoPosition.MMI_M_RELATIVPOS = 0;
            EVC5_MMIGeoPosition.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the ‘Geographical Position Indicator’ in sub-area G12.");

            /*
            Test Step 3
            Action: Press Geographical Position indicator on sub-area G12
            Expected Result: The symbol ‘DR03’ is resumed.Use the log file to confirm that DMI sends out packet [MMI_DRIVER_ACTION (EVC-152)] with the value of variable MMI_M_DRIVER_ACTION refer to sequence below,a)   MMI_M_DRIVER_ACTION = 33 (Request to hide geographical position)
            Test Step Comment: MMI_gen 11470 (partly: Bit # 33);
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Geographical Position Indicator’ in sub-area G12");

            EVC152_MMIDriverAction.Check_MMI_M_DRIVER_ACTION = EVC152_MMIDriverAction.MMI_M_DRIVER_ACTION.RequestToHideGeographicalPosition;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays symbol DR03 in sub-area G12.");

            /* 
            Test Step 4
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
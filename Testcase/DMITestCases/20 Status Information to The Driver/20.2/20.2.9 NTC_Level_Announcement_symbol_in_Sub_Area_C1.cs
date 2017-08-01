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
    /// 20.2.9 NTC Level :Announcement symbol in Sub-Area C1.
    /// TC-ID: 15.2.9
    /// 
    /// This test case verifies that the level announcement symbol is not presented if the mode acknowledgement symbol for NTC (MO20) still displays in sub-area C1 
    /// 
    /// Tested Requirements:
    /// MMI_gen 9429 (partly: NTC);
    /// 
    /// Scenario:
    /// 1.Perform Start of Misson to ATB STM until train running number entry 
    /// 2.Press the 'Start' button. 
    /// 3.Send packet EVC-8 with level announcement and verify that level announcement symbol is not presented as long as mode acknowledgement symbol still displays.
    /// 
    /// Used files:
    /// 15_2_9_a.xml
    /// </summary>
    public class NTC_Level_Announcement_symbol_in_Sub_Area_C1 : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // System is power OFF.Configure atpcu configuration file as following (See the instruction in Appendix 2)- M_InstalledLevels = 31- NID_NTC_Installe_0 = 1 (ATB)

            // Call the TestCaseBase PreExecution
            base.PreExecution();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in ATB STM mode, Level NTC.

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint


            /*
            Test Step 1
            Action: Peform following action:Power ON the systemActivate cabin A  perform Start of Mission to ATB STM until train running number entry
            Expected Result: DMI displays main window
            */


            /*
            Test Step 2
            Action: Press the ‘Start’ button
            Expected Result: The acknowledgement for NTC symbol (MO20) is displayed in area C1
            */


            /*
            Test Step 3
            Action: Use the test script file 15_2_9_a.xml to send EVC-8 with,MMI_Q_TEXT = 257MMI_Q_TEXT_CRITERIA = 1MMI_N_TEXT = 1MMI_X_TEXT = 0
            Expected Result: Verify the following information,The displays in sub-area C1 is not changed, DMI still displays MO20 symbol
            Test Step Comment: MMI_gen 9429 (partly, NTC);
            */


            /*
            Test Step 4
            Action: Confirm NTC
            Expected Result: DMI displays in ATB STM mode, Level NTC
            */
            // Call generic Check Results Method
            DmiExpectedResults.DMI_displays_in_ATB_STM_mode_Level_NTC(this);


            /*
            Test Step 5
            Action: End of test
            Expected Result: 
            */


            return GlobalTestResult;
        }
    }
}
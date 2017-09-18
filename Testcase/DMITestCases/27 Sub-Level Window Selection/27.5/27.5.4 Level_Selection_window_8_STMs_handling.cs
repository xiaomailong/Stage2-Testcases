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
    /// 27.5.4 Level Selection window: 8 STMs handling
    /// TC-ID: 22.5.4 
    /// 
    /// This test case is to verify that DMI can handle up to 8 STMs. 
    /// 
    /// Tested Requirements:
    /// MMI_gen 1077;
    /// 
    /// Scenario:
    /// 1.Power on the system and activate cabin.
    /// 2.Enter Driver ID and skip the brake test.
    /// 3.Verify that 8 STMs are available on the level selection window.
    /// 
    /// Used files:
    /// N/A
    /// </summary>
    public class TC_22_5_4_Level_Window : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // Power off the system Configure atpcu configuration file as following (See the instruction in Appendix 2)M_InstalledLevels = 4082NID_NTC_Installe_0 = 1NID_NTC_Installe_1 = 20NID_NTC_Installe_2 = 28NID_NTC_Installe_3 = 9NID_NTC_Installe_4 = 6NID_NTC_Installe_5 = 10NID_NTC_Installe_6 = 22NID_NTC_Installe_7 = 0

            // Call the TestCaseBase PreExecution
            base.PreExecution();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            //  DMI Displays in SB mode.

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint
            TraceInfo("This test case requires an ATP configuration change - " +
                        "See Precondition requirements. If this is not done manually, the test may fail!");
            // M_InstalledLevels = 4082NID_NTC_Installe_0 = 1NID_NTC_Installe_1 = 20NID_NTC_Installe_2 = 28NID_NTC_Installe_3 = 9NID_NTC_Installe_4 = 6NID_NTC_Installe_5 = 10NID_NTC_Installe_6 = 22NID_NTC_Installe_7 = 0

            /*
            Test Step 1
            Action: Power on the system and activate the cabin
            Expected Result: DMI displays Driver ID window in SB mode
            */
            EVC0_MMIStartATP.Evc0Type = EVC0_MMIStartATP.EVC0Type.GoToIdle;
            EVC0_MMIStartATP.Send();
            DmiActions.Activate_Cabin_1(this);

            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.StandBy;
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.DriverID;
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = 1;
            EVC30_MMIRequestEnable.Send();

            // Call generic Check Results Method
            DmiExpectedResults.Driver_ID_window_displayed_in_SB_mode(this);

            /*
            Test Step 2
            Action: Enetr Driver ID and skip brake test
            Expected Result: Verify the following information,The level selection window displays 8 folllowing STMs accroding to configuration settingATBTPWS/AWSTBL1+PZB/LZBPZBLZBATC2ASFA
            Test Step Comment: MMI_gen 1077;
            */
            DmiActions.ShowInstruction(this, "Enter Driver ID.");

            // The STMs will probably differ from these: check and alter!
            WaitForVerification("Check that the following STMs are displayed:" + Environment.NewLine + Environment.NewLine +
                                "1. ATB" + Environment.NewLine +
                                "2. TPWS/AWS" + Environment.NewLine +
                                "3. TLB1+S" + Environment.NewLine +
                                "4. PZB/LZB" + Environment.NewLine +
                                "5. PZB" + Environment.NewLine +
                                "6. LZB" + Environment.NewLine +
                                "7. ATC2" + Environment.NewLine +
                                "8. ASFA");

            /*
            Test Step 3
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
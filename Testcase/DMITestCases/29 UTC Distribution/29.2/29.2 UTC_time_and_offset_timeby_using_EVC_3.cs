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
    /// 29.2 UTC time and offset time(by using EVC-3)
    /// TC-ID: 29.2
    /// 
    /// This test case is to verify the UTC time/Offset time changed by time source:EVC-3.     It shall comply with [ERA] standard and [MMI-ETCS-gen].
    /// 
    /// Tested Requirements:
    /// MMI_gen 2429; MMI_gen 2421 (partly: ETC to ETCS-MMI);
    /// 
    /// Scenario:
    /// 1.Power on test system and activate cabin. 
    /// 2.Perform Start of Mission to L1, SR mode.
    /// 3.Use test script file to send packet EVC-
    /// 3.
    /// 4.Verify the DMI time is updated
    /// 5.Select ‘Settings’ button then ‘Settings’ window is opened.
    /// 6.Select ‘Set Clock’ button and verify DMI displays current date of UTC time and Offset time.
    /// 
    /// Used files:
    /// 29_2_a.xml, 29_2_a.xml
    /// </summary>
    public class TC_ID_29_2_UTC_time_and_offset_timeby_using_EVC_3 : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // Power off test system Set the following tags name in configuration file (See the instruction in Appendix 1)
            // CLOCK_TIME_SOURCE = 0 (by Deafult)

            // Call the TestCaseBase PreExecution
            base.PreExecution();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays SR mode, Level 1.

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint
            TraceInfo("This test case requires an ATP configuration change - " +
                      "See Precondition requirements. If this is not done manually, the test may fail!");

            /*
            Test Step 1
            Action: Power on the system and activate the cabin
            Expected Result: DMI displays SB mode
            */
            // tested in 29.1 so not repeated

            /*
            Test Step 2
            Action: Perform SoM to L1, SR mode
            Expected Result: Mode changes to SR mode , L1
            */
            DmiActions.Complete_SoM_L1_SR(this);

            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Default;      // Default window
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.SetLocalTimeDateAndOffset;
            EVC30_MMIRequestEnable.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in SR mode, Level 1.");
            
            /*
            Test Step 3
            Action: Use the test script file 29_2_a.xml to send EVC-3 with,MMI_T_UTC = 946728000(2000-01-01,12:00:00)MMI_T_Zone_OFFSET = 4Note: The resolution of MMI_T_Zone_OFFSET is 0.25 hour
            Expected Result: Verify the following information:DMI displays the updated time as 13:00:00
            Test Step Comment: MMI_gen 2429; MMI_gen 2421 (partly: ETC to ETCS-MMI);
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Settings’ button, then press the ‘Set clock’ button");

            XML_29_2(msgType.msgTypeA);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the updated time as ‘13:00:00’.");

            /*
            Test Step 4
            Action: Select ‘Settings’ button and press ‘Set clock’ button
            Expected Result: Verify the following information:The set clock window is openedThe DMI (yyyy-mm-dd) date is 2000-01-01
            Test Step Comment: MMI_gen 2429; 
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Settings’ button, then press the ‘Set clock’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Set clock window." + Environment.NewLine +
                                "2. The date displayed is ‘2000-01-01");

            /*
            Test Step 5
            Action: Use the test script file 29_2_b.xml to send EVC-3 with,MMI_T_UTC = 946771200(2000-01-02,12:00:00)MMI_T_Zone_OFFSET = 252Note: The resolution of MMI_T_Zone_OFFSET is 0.25 hour, set the overflow value for negative offset
            Expected Result: Verify the following information:DMI displays the updated time as 11:00:00
            Test Step Comment: MMI_gen 2429; MMI_gen 2421 (partly: ETC to ETCS-MMI);
            */
            DmiActions.ShowInstruction(this, @"Close the ‘Set clock’ window");

            XML_29_2(msgType.msgTypeB);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the updated time as ‘11:00:00’.");

            /*
            Test Step 6
            Action: Select ‘Settings’ button and press ‘Set clock’ button
            Expected Result: Verify the following information:The set clock window is openedThe DMI date (yyyy-mm-dd) is 2000-01-02
            Test Step Comment: MMI_gen 2429; 
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Settings’ button, then press the ‘Set clock’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Set clock window." + Environment.NewLine +
                                "2. The date displayed is ‘2000-01-02");

            /*
            Test Step 7
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }

        #region XML_29_2_DMI_Test_Specification

        private enum msgType : byte
        {
            msgTypeA,
            msgTypeB
        }

        private void XML_29_2(msgType packetSelector)
        {
            switch (packetSelector)
            {
                case msgType.msgTypeA:
                    EVC3_MMISetTimeATP.MMI_T_UTC = 946728000;
                    EVC3_MMISetTimeATP.MMI_T_ZONE_OFFSET = 4;
                    break;
                case msgType.msgTypeB:
                    EVC3_MMISetTimeATP.MMI_T_UTC = 946771200;
                    EVC3_MMISetTimeATP.MMI_T_ZONE_OFFSET = -4;      // 252 is 0xfc - as sbyte == -0x80 + 0x7c (-128 + 124)
                    break;
            }   
            
            EVC3_MMISetTimeATP.Send();
        }

        #endregion
    }
}
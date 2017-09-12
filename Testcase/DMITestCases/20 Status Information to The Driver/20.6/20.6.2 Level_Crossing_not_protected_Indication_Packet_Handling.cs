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
    /// 20.6.2 Level Crossing “not protected” Indication: Packet Handling
    /// TC-ID: 15.6.2
    /// 
    /// This test case verifies the placment of objects in sub-area B3-B5 refer to received packet information EVC-33 (Level crossing not protected symbol) and EVC-32 (Track condition symbol) including with an internal storage to keep an information when all specified areas are in used.
    /// 
    /// Tested Requirements:
    /// MMI_gen 10482; MMI_gen 9499; MMI_gen 9500; MMI_gen 10480;
    /// 
    /// Scenario:
    /// Use the test script file to send packet information (both of EVC-33 and EVC-32). Then, verify the display information of LX01 symbol in sub-area B3-B
    /// 5.
    /// 
    /// Used files:
    /// 15_6_2_a.xml, 15_6_2_b.xml, 15_6_2_c.xml, 15_6_2_d.xml, 15_6_2_e.xml, 15_6_2_f.xml, 15_6_2_g.xml, 15_6_2_h.xml, 15_6_2_i.xml, 15_6_2_j.xml
    /// </summary>
    public class TC_15_6_2_Level_Crossing_not_protected_Indication : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // Test system is powered on.Activate Cabin A.SoM in performed in SR mode, Level 1.

            // Call the TestCaseBase PreExecution
            base.PreExecution();
            DmiActions.Complete_SoM_L1_SR(this);
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in SR mode, level 1.
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in SR mode, Level 1.");

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint

            /*
            Test Step 1
            Action: Use the test script file 15_6_2_a.xml to send EVC-33 withMMI_Q_TRACKCOND_STEP = 8MMI_M_TRACKCOND_TYPE = 16
            Expected Result: Verify the following information,There is no symbol display in sub-area B3-B5
            Test Step Comment: (1) MMI_gen 10482 (partly: invalid MMI_M_TRACKCOND_STEP);
            */
            XML.XML_15_6_2_a.Send(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI does not display a symbol in sub-areas B3-B5.");
            /*
            Test Step 2
            Action: Use the test script file 15_6_2_b.xml to send EVC-33 withMMI_Q_TRACKCOND_STEP = 1MMI_M_TRACKCOND_TYPE = 64
            Expected Result: Verify the following information,There is no symbol display in sub-area B3-B5
            Test Step Comment: (1) MMI_gen 10482 (partly: invalid MMI_M_TRACKCOND_TYPE);
            */
            XML.XML_15_6_2_b.Send(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI does not display a symbol in sub-areas B3-B5.");

            /*
            Test Step 3
            Action: Use the test script file 15_6_2_c.xml to send EVC-33 withMMI_Q_TRACKCOND_STEP = 1MMI_M_TRACKCOND_TYPE = 16MMI_NID_TRACKCOND = 0
            Expected Result: Verify the following information,DMI displays LX01 symbol in sub-area B3
            Test Step Comment: (1) MMI_gen 9499 (partly: B3); MMI_gen 9500 (partly: filling B3);
            */
            XML.XML_15_6_2_c.Send(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays symbol LX01 in sub-area B3.");

            /*
            Test Step 4
            Action: Use the test script file 15_6_2_d.xml to send EVC-33 withMMI_Q_TRACKCOND_STEP = 4MMI_NID_TRACKCOND = 0
            Expected Result: The LX01 symbol is removed from sub-area B3
            */
            XML.XML_15_6_2_d.Send(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI removes symbol LX01 from sub-area B3.");

            /*
            Test Step 5
            Action: Use the test script file 15_6_2_e.xml to send EVC-32 with,MMI_NID_TRACKCOND = 29MMI_Q_TRACKCOND_STEP = 1MMI_M_TRACKCOND_TYPE =3 Then, send EVC-33 with,MMI_Q_TRACKCOND_STEP = 1MMI_M_TRACKCOND_TYPE = 16MMI_NID_TRACKCOND = 0
            Expected Result: Verify the following information,After DMI displayed the TC03 symbol in sub-area B3 with yellow flashing frame, The LX01 symbol is display in sub-area B4
            Test Step Comment: (1) MMI_gen 9499 (partly: B4); MMI_gen 9500 (partly: left to right, filling B4, next area shall be used);
            */
            XML.XML_15_6_2_e.Send(this);

            /*
            Test Step 6
            Action: Use the test script file 15_6_2_d.xml to send EVC-33 withMMI_Q_TRACKCOND_STEP = 4MMI_NID_TRACKCOND = 0
            Expected Result: The LX01 symbol is removed from sub-area B4
            */
            XML.XML_15_6_2_d.Send(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI removes symbol LX01 from sub-area B4.");

            /*
            Test Step 7
            Action: Use the test script file 15_6_2_f.xml to send EVC-32 with,MMI_NID_TRACKCOND = 30MMI_Q_TRACKCOND_STEP = 1MMI_M_TRACKCOND_TYPE =3 MMI_Q_TRACKCOND_ACTION_START = 1Then, send EVC-33 with,MMI_Q_TRACKCOND_STEP = 1MMI_M_TRACKCOND_TYPE = 16MMI_NID_TRACKCOND = 0
            Expected Result: Verify the following information,After DMI displayed the TC02 symbol in sub-area B4, The LX01 symbol is display in sub-area B5
            Test Step Comment: (1) MMI_gen 9499 (partly: B5); MMI_gen 9500 (partly: left to right, filling B5, next area shall be used);
            */
            XML.XML_15_6_2_f.Send(this);

            /*
            Test Step 8
            Action: Use the test script file 15_6_2_d.xml to send EVC-33 withMMI_Q_TRACKCOND_STEP = 4MMI_NID_TRACKCOND = 0
            Expected Result: The LX01 symbol is removed from sub-area B5
            */
            // Call generic Action Method
            XML.XML_15_6_2_d.Send(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI removes symbol LX01 from sub-area B5.");

            /*
            Test Step 9
            Action: Use the test script file 15_6_2_g.xml to send EVC-32 with,MMI_NID_TRACKCOND = 31MMI_Q_TRACKCOND_STEP = 2MMI_M_TRACKCOND_TYPE =3 MMI_Q_TRACKCOND_ACTION_START = 1Then, send a two  packets EVC-33 with,Common variablesMMI_Q_TRACKCOND_STEP = 1MMI_M_TRACKCOND_TYPE = 16The order of MMI_NID_TRACKCOND in each packetMMI_NID_TRACKCOND = 0MMI_NID_TRACKCOND =1
            Expected Result: DMI displays TC01 symbol in sub-area B5
            */
            XML.XML_15_6_2_g.Send(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays symbol TC01 in sub-area B5.");

            /*
            Test Step 10
            Action: Use the test script file 15_6_2_h.xml to send EVC-32 with,MMI_NID_TRACKCOND = 32MMI_Q_TRACKCOND_STEP = 4
            Expected Result: Verify the following information,After TC01 symbol is removed from sub-area B5, The LX01 symbol is display in sub-area B5
            Test Step Comment: (1) MMI_gen 9500 (partly: Wait until B5 is free);
            */
            XML.XML_15_6_2_h.Send(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI removes symbol TC01 and displays symbol LX01 in sub-area B5.");
            /*
            Test Step 11
            Action: Use the test script file 15_6_2_i.xml to send EVC-32 with,MMI_NID_TRACKCOND = 31MMI_Q_TRACKCOND_STEP = 4
            Expected Result: Verify the following information,After TC02 symbol is removed from sub-area B4, The LX01 symbol in sub-area B5 is moved to sub-area B4.The new LX01 symbol is display in sub-area B5
            Test Step Comment: (1) MMI_gen 9500 (partly: Wait until B4 is free); MMI_gen 10480;
            */
            XML.XML_15_6_2_i.Send(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI removes symbol TC01 and displays symbol LX01 in sub-area B4.");

            /*
            Test Step 12
            Action: Use the test script file 15_6_2_j.xml to send EVC-33 with,MMI_Q_TRACKCOND_STEP = 1MMI_M_TRACKCOND_TYPE = 16MMI_NID_TRACKCOND =2Then, send EVC-32 with,MMI_NID_TRACKCOND = 29MMI_Q_TRACKCOND_STEP = 4
            Expected Result: Verify the following information,After TC03 symbol is removed from sub-area B3, The LX01 symbol in sub-area B4 is moved to sub-area B3.The LX01 symbol in sub-area B5 is moved to sub-area B4.The new LX01 symbol is display in sub-area B5
            Test Step Comment: (1) MMI_gen 9500 (partly: Wait until B3 is free);
            */
            XML.XML_15_6_2_j.Send(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI removes symbol TC02 and displays symbol LX01 in sub-area B3.");

            /*
            Test Step 13
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
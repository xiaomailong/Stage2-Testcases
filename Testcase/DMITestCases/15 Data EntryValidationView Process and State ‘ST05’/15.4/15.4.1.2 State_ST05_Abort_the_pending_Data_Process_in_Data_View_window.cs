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
    /// 15.4.1.2 State ‘ST05’: Abort the pending Data Process in Data View window
    /// TC-ID: 10.4.1.2
    /// 
    /// This test case verifies that the process of data view window in state ST05 is aborted by a received packet of different window type (i.e., data entry window) from ETCS onboard.
    /// 
    /// Tested Requirements:
    /// MMI_gen 5507 (partly: Data View window, abort an already pending data view process, received packet of different window from ETCS onboard);
    /// 
    /// Scenario:
    /// 1.Verify the display information when execute the test script files when open the windows below,Data view windowSystem Info windowSystem Version window
    /// 
    /// Used files:
    /// 10_4_1_2_a.xml, 10_4_1_2_b.xml
    /// </summary>
    public class TC_ID_10_4_1_2_State_ST05 : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:

            // Call the TestCaseBase PreExecution
            base.PreExecution();
            // Test system is powered onCabin is activeSoM is performed until level 1 is selected and confirmed.Main window is closed
            DmiActions.Complete_SoM_L1_SB(this);
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in SB mode
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in SB mode, Level 1.");

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint

            /*
            Test Step 1
            Action: At the Default window, press ‘Data View’ button
            Expected Result: DMI displays Data View window
            */
            DmiActions.ShowInstruction(this, @"Press ‘Data View’ button");

            // EVC13_MMIDataView.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Data View window.");

            /*
            Test Step 2
            Action: Use the test script file 10_4_1_2_a.xml to send EVC-8 withMMI_Q_TEXT_CRITERIA = 3 MMI_Q_TEXT = 716
            Expected Result: The hourglass symbol ST05 is displayed at window title area
            */
            XML.XML_10_4_1_2_a.Send(this);
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The hourglass symbol ST05 is displayed in the window title area.");

            /*
            Test Step 3
            Action: Use the test script file 10_4_1_2_b.xml to send EVC-14 withMMI_X_DRIVER_ID = ‘4444’
            Expected Result: Verify the followin information,(1)     The Data View window is closed, DMI displays Driver ID window after received packet EVC-14
            Test Step Comment: (1) MMI_gen 5507 (partly: Data View window, abort an already pending data view process, received packet of different window from ETCS onboard);
            */
            XML.XML_10_4_1_2_b.Send(this);
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The Data View window is closed and DMI displays the Driver ID window");

            /*
            Test Step 4
            Action: Perform the following procedure,At Driver ID window, press ‘close’ button.Open System Info windowRepeat action step 2-3
            Expected Result: Verify the followin information,(1)     The System Info window is closed, DMI displays Driver ID window after received packet EVC-14
            Test Step Comment: (1) MMI_gen 5507 (partly: System Info window, abort an already pending data view process, received packet of different window from ETCS onboard);
            */
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_LOW = true;
            EVC30_MMIRequestEnable.Send();

            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button in the Driver ID window. Open the System info window");

            //EVC13_MMISystemInfo.Send();

            XML.XML_10_4_1_2_a.Send(this);
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The hourglass symbol ST05 is displayed in the window title area."); 

            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = 254;
            EVC30_MMIRequestEnable.Send();

            XML.XML_10_4_1_2_b.Send(this);
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The System info window is closed and DMI displays the Driver ID window");

            /*
            Test Step 5
            Action: Perform the following procedure,At Driver ID window, press ‘close’ button.Open System version windowRepeat action step 2-3
            Expected Result: Verify the followin information,(1)     The System version window is closed, DMI displays Driver ID window after received packet EVC-14
            Test Step Comment: (1) MMI_gen 5507 (partly: System version window, abort an already pending data view process, received packet of different window from ETCS onboard);
            */
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.SystemVersion;
            EVC30_MMIRequestEnable.Send();

            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button in the Driver ID window. Open the System version window");

            XML.XML_10_4_1_2_a.Send(this);
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The hourglass symbol ST05 is displayed in the window title area.");

            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = 254;
            EVC30_MMIRequestEnable.Send();

            XML.XML_10_4_1_2_b.Send(this);
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The System version window is closed and DMI displays the Driver ID window");

            /*
            Test Step 6
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
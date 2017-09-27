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
    /// 27.17.2 Layers
    /// TC-ID: 8.2.1
    /// 
    /// This test case verifies the presentation of Driver ID window that appears in area D, F and G cover the Half-Grid array each object (excluding Keyboards) shall comply with [ERA-ERTMS] standard.
    /// 
    /// Tested Requirements:
    /// MMI_gen 8033 (partly: MMI_gen 4722 (partly: Half-Grid Array), MMI_gen 5189 (partly: touch screen));
    /// 
    /// Scenario:
    /// Activate cabin A.  
    /// 1.Driver enters the Driver ID.
    /// 2.Perform brake test. Observe and verify that the presentation of the Driver ID window layout is displayed in Half-Grid array covering on Main-area D, F and G.
    /// 
    /// Used files:
    /// N/A
    /// </summary>
    public class TC_ID_8_2_1_Layers : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:

            // Call the TestCaseBase PreExecution
            base.PreExecution();

            // System is power on.
            DmiActions.Start_ATP();

        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in SB mode.

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint
            
            /*
            Test Step 1
            Action: Activate cabin A
            Expected Result: The Driver ID window is displayed cover  the half-grid array in area D, F and G
            */
            // Call generic Action Method
            DmiActions.Activate_Cabin_1(this);

            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.StandBy;

            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.None;
            EVC30_MMIRequestEnable.Send();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = 1;
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.DriverID |
                                                               EVC30_MMIRequestEnable.EnabledRequests.StartBrakeTest;
            EVC30_MMIRequestEnable.Send();

            EVC14_MMICurrentDriverID.MMI_X_DRIVER_ID = "4444";
            EVC14_MMICurrentDriverID.MMI_Q_ADD_ENABLE = (EVC14_MMICurrentDriverID.MMI_Q_ADD_ENABLE_BUTTONS)0;
            EVC14_MMICurrentDriverID.MMI_Q_CLOSE_ENABLE = Variables.MMI_Q_CLOSE_ENABLE.Enabled;
            EVC14_MMICurrentDriverID.Send();

            WaitForVerification("Check the following (* indicates sub-areas drawn as one area):" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Driver ID window with 3 layers as a half-grid array." + Environment.NewLine +
                                "2. Layer 0 is displayed in areas D, F and G." + Environment.NewLine +
                                "3. Layer 1 is displayed in areas A1, (A2 + A3)*, A4, B*, C1, (C2 + C3 + C4)*, C5, C6, C7, C8, C9, E1, E2, E3, E4, (E5 - E9)*." + Environment.NewLine +
                                "4. Layer 2 is displayed in areas B3, B4, B5, B6, B7." + Environment.NewLine +
                                "5. Refer to DMI_RS_ETCS_R4.docx for the presentation of the window.");

            /*
            Test Step 2
            Action: Driver enters the Driver ID
            Expected Result: Verify that the layers on half grid array are displayed as 1. Layer 0: Main area D, F and G2. Layer -1: Area A1, (A2+A3)*, A4, B*, C1, (C2+C3+C4)*, C5, C6, C7, C8, C9, E1, E2, E3, E4, (E5-E9)*.3. Layer -2: Area B3, B4, B5, B6 and B7.4. Each object are follow the dimension and position as example picture in comment.Note: ‘*’ symbol is mean that specified area are drawn as one area
            Test Step Comment: MMI_gen 8033 (partly: MMI_gen 4722 (partly: Half-Grid Array),  MMI_gen 5189 (partly: touch screen))
            */
            DmiActions.ShowInstruction(this, "Enter ‘1234’ for the Driver ID");

            /*
            Test Step 3
            Action: Confirm the Driver ID and perform brake test
            Expected Result: DMI displays the message ‘Brake test in progress’
            */
            DmiActions.ShowInstruction(this, "Confirm the Driver ID by pressing in the data input field, then press the ‘Brake test’ button");

            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 2;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 514;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the message ‘Brake test in progress’.");

            /*
            Test Step 4
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
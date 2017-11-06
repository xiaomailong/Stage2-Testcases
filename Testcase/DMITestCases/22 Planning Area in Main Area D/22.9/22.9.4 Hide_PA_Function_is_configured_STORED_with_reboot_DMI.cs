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
    /// 22.9.4 Hide PA Function is configured ‘STORED’ with reboot DMI
    /// TC-ID: 17.9.4
    /// 
    /// This test case verifies that if the Hide PA Function is configured as “Stored” and then reboot the DMI. The all PA’s objects shall be hidden, configuration of Hide PA Function is not effect when DMI reboot.
    /// 
    /// Tested Requirements:
    /// MMI_gen 7341; MMI_gen 2996 (partly: Stored);
    /// 
    /// Scenario:
    /// Activate cabin A. Driver enters the Driver ID and performs brake test. Then the driver selects level 1, Train data, and validate the train data. After that driver enter Train running number and confirm SR mode. At 100 m, pass BG1 with pkt 12, pkt 21 and pkt 
    /// 27.Mode changes to FS modeTurn off and then turn on DMI. The Hide PA button is appeared on the area D of the DMI.
    /// 
    /// Used files:
    /// 17_9_4.tdg
    /// </summary>
    public class TC_ID_17_9_4_Hide_PA_Function_is_configured_STORED_with_reboot_DMI : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // Set the following tags name in configuration file (See the instruction in Appendix 1)
            // HIDE_PA_FUNCTION = 2 (‘Stored’ state)

            // Call the TestCaseBase PreExecution
            base.PreExecution();

            // System is power off
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in FS mode, Level 1.

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
            Action: Power On the system
            Expected Result: DMI displays the default window
            */
            DmiActions.Start_ATP();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Default window.");


            /*
            Test Step 2
            Action: Activate cabin A and Perform SoM to SR mode, Level 1
            Expected Result: DMI displays in SR mode and level 1
            */
            // Tested elsewhere, force SoM
            DmiActions.Activate_Cabin_1(this);
            DmiActions.Set_Driver_ID(this, "1234");
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Level = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_LEVEL.L1;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.StaffResponsible;
            DmiActions.Finished_SoM_Default_Window(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in SR mode, Level 1.");

            /*
            Test Step 3
            Action: Drive the train forward with speed = 40 km/h pass BG1
            Expected Result: DMI shows “Entering FS” messageDMI displays the Planning area in main area D.The Hide PA button is appeared on  the main area D of the DMI
            Test Step Comment: (1) MMI_gen 7341;   
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 40;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN = 10000;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.FullSupervision;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 274;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the message ‘Entering FS’." + Environment.NewLine +
                                "2. The Planning Area is displayed in area D." + Environment.NewLine +
                                "3. The ‘Hide PA’ button is displayed in area D.");

            // Remove the message
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 4;
            EVC8_MMIDriverMessage.Send();

            /*
            Test Step 4
            Action: Press Hide PA button
            Expected Result: The Planning area is disappeared from the main area D of DMI
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Hide PA’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The Planning Area is removed from area D.");

            /*
            Test Step 5
            Action: Turn off power of DMI
            Expected Result: DMI is power off
            */
            DmiActions.ShowInstruction(this, @"Power down the system");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI is blank.");

            /*
            Test Step 6
            Action: Turn on power of DMI
            Expected Result: DMI is power on DMI displays the Planning area. The Hide PA button is appeared on the main area D of DMI
            Test Step Comment: MMI_gen 7341;  MMI_gen 2996 (partly: Stored); Hide PA icon
            */
            DmiActions.ShowInstruction(this, @"Wait 10s and power up the system");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The Planning Area is re-displayed in area D." + Environment.NewLine +
                                "2. The ‘Hide PA’ button is re-displayed in area D.");

            /*
            Test Step 7
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
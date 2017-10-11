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
using static Testcase.Telegrams.EVCtoDMI.Variables;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 27.5.2 Level Selection window: Packet sending/receiving
    /// TC-ID: 22.5.2
    /// 
    /// This test case verifies the general appearance of Level window refer to received packet information EVC-20.
    /// 
    /// Tested Requirements:
    /// MMI_gen 1630 (partly: NEGATIVE, 2nd bullet); MMI_gen 8025;
    /// 
    /// Scenario:
    /// Use the test script file to send packet information. Then, verify that Level window is not display.Activate Cabin A.Use the test script file to send packet information. Then, verify that Level window is not display.Perform action to open Level window. Then, verify that the display information of Level window is correct refer to received packet information EVC-20.Use the test script file to send packet information. Then, verify that Level window is updated correctly refer to received packet information EVC-20.
    /// 
    /// Used files:
    /// 22_5_2_a.xml, 22_5_2_b.xml, 22_5_2_c.xml
    /// </summary>
    public class TC_22_5_2_Level_Window : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // Use the ATP config editor to set the following parameters as follows (See the instruction in Appendix 2),M_InstalledLevels = 255M_DefaulLevels = 255NID_NTC_Installed_0 = 1NID_NTC_Installed_1 = 9NID_NTC_Installed_2 = 20NID_NTC_Installed_3 = 22NID_NTC_Default_0 = 1NID_NTC_Default_1 = 9NID_NTC_Default_2 = 20NID_NTC_Default_3 = 22Test system is power on.

            // Call the TestCaseBase PreExecution
            base.PreExecution();

            DmiActions.Start_ATP();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in SB mode

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
            Action: Use the test script file 22_5_2_a.xml to send EVC-20 with,MMI_N_LEVELS = 2MMI_Q_LEVEL_NTC_ID[0] = 1MMI_M_CURRENT_LEVEL[0]  = 0MMI_M_LEVEL_FLAG[0] = 1MMI_M_INHIBITED_LEVEL[0] = 0MMI_M_INHIBIT_ENABLE[0] = 1MMI_M_LEVEL_NTC_ID[0] = 0MMI_Q_LEVEL_NTC_ID[1] = 1MMI_M_CURRENT_LEVEL[1]  = 0MMI_M_LEVEL_FLAG[1] = 1MMI_M_INHIBITED_LEVEL[1] = 1MMI_M_INHIBIT_ENABLE[1] = 1MMI_M_LEVEL_NTC_ID[1] = 2
            Expected Result: Verify the following information,Level window is not display on DMI
            Test Step Comment: (1) MMI_gen 1630 (partly: NEAGTIVE, 1st bullet);
            */
            XML.XML_22_5_2_a.Send(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI does not display the Level window ");
            /*
            Test Step 2
            Action: Activate Cabin A
            Expected Result: DMI displays Driver ID window
            */
            // Call generic Action Method
            DmiActions.Activate_Cabin_1(this);

            EVC14_MMICurrentDriverID.Send();

            // Call generic Check Results Method
            DmiExpectedResults.Driver_ID_window_displayed(this);

            /*
            Test Step 3
            Action: Use the test script file 22_5_2_b.xml to send EVC-20 with,MMI_N_LEVELS = 0
            Expected Result: Verify the following information,DMI still displays Driver ID window
            Test Step Comment: (1) MMI_gen 1630 (partly: NEAGTIVE, 2nd  bullet);
            */
            XML.XML_22_5_2_b.Send(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI still displays the Driver ID window ");

            /*
            Test Step 4
            Action: Perform the following procedure,Enter Driver ID and perform brake test.Select and confirm Level 1.Press ‘Level’ button
            Expected Result: DMI displays Level window.Verify the following information,Use the log file to confirm the amount of buttons which displayed in Level window are consisted with value of variable MMI_N_LEVELS (EVC-20)Example:MMI_N_LEVELS = 8, 8 keypad buttons.Use the log file to confirm an information for the ETCS Levels, the label of each button are presented to driver correctly refer to each index of variable MMI_Q_LEVEL_NTC_ID (EVC-20) and MMI_M_LEVEL_NTC_ID (EVC-20) as follows,Level 1MMI_Q_LEVEL_NTC_ID[0] = 1MMI_M_LEVEL_NTC_ID[0] = 1MMI_M_INHIBITED_LEVEL[0] = 0Level 2MMI_Q_LEVEL_NTC_ID[1] = 1MMI_M_LEVEL_NTC_ID[1] = 2MMI_M_INHIBITED_LEVEL[1] = 0Level 3MMI_Q_LEVEL_NTC_ID[2] = 1MMI_M_LEVEL_NTC_ID[2] = 3MMI_M_INHIBITED_LEVEL[2] = 0Level 0MMI_Q_LEVEL_NTC_ID[3] = 1MMI_M_LEVEL_NTC_ID[3] = 0MMI_M_INHIBITED_LEVEL[3] = 0Level ATBMMI_Q_LEVEL_NTC_ID[4] = 0MMI_M_LEVEL_NTC_ID[4] = 1MMI_M_INHIBITED_LEVEL[4] = 0Level PZB/LZBMMI_Q_LEVEL_NTC_ID[5] = 0MMI_M_LEVEL_NTC_ID[5] = 9MMI_M_INHIBITED_LEVEL[5] = 0Level TPWS/AWSMMI_Q_LEVEL_NTC_ID[6] = 0MMI_M_LEVEL_NTC_ID[6] = 20MMI_M_INHIBITED_LEVEL[6] = 0Level ATC SE/NOMMI_Q_LEVEL_NTC_ID[7] = 0MMI_M_LEVEL_NTC_ID[7] = 22MMI_M_INHIBITED_LEVEL[7] = 0Note: The first index of parameter is the topmost position in packet EVC-20.The position each buttons are displayed correctly refer to received EVC-20 as picture below,Note: The label of NTC buttons are replaced refer to value of MMI_Q_LEVEL_NTC_ID and MMI_M_LEVEL_NTC_ID in expected result (2).The text colour of each button are displayed as grey refer to each value of MMI_M_INHIBITED_LEVEL inexpected result (2) .Use the log file to confirm that every index of variable MMI_M_LEVEL_FLAG (EVC-20) = 1.All buttons in keypad are enabled refer to each value of MMI_M_LEVEL_FLAG in expected result (5)
            Test Step Comment: (1) MMI_gen 1972 (partly: contain all levels reported by packet EVC-20);(2) MMI_gen 1978 (partly: occurrence in packet EVC-20);(3) MMI_gen 1972 (partly: presented to the driver, presented by one object in the list);                        MMI_gen 1978 (partly: The selection list, the topmost position of the selection list);                          MMI_gen 8025;(4) MMI_gen 1979 (partly: MMI_M_INHIBITED_LEVEL , appropriate colour);(5) MMI_gen 1979 (partly: MMI_M_LEVEL_FLAG);(6) MMI_gen 1979 (partly: not inhibited levels button enabling);
            */
            // Skip brake test: tested elsewhere
            DmiActions.ShowInstruction(this, "Enter and confirm Driver ID");

            EVC20_MMISelectLevel.MMI_Q_CLOSE_ENABLE = Variables.MMI_Q_CLOSE_ENABLE.Disabled;
            EVC20_MMISelectLevel.MMI_Q_LEVEL_NTC_ID = new Variables.MMI_Q_LEVEL_NTC_ID[] { Variables.MMI_Q_LEVEL_NTC_ID.ETCS_Level };
            EVC20_MMISelectLevel.MMI_M_CURRENT_LEVEL = new Variables.MMI_M_CURRENT_LEVEL[] { Variables.MMI_M_CURRENT_LEVEL.LastUsedLevel };
            EVC20_MMISelectLevel.MMI_M_LEVEL_FLAG = new Variables.MMI_M_LEVEL_FLAG[] { Variables.MMI_M_LEVEL_FLAG.MarkedLevel };
            EVC20_MMISelectLevel.MMI_M_INHIBITED_LEVEL = new Variables.MMI_M_INHIBITED_LEVEL[] { Variables.MMI_M_INHIBITED_LEVEL.NotInhibited };
            EVC20_MMISelectLevel.MMI_M_INHIBIT_ENABLE = new Variables.MMI_M_INHIBIT_ENABLE[] { Variables.MMI_M_INHIBIT_ENABLE.AllowedForInhibiting };
            EVC20_MMISelectLevel.MMI_M_LEVEL_NTC_ID = new Variables.MMI_M_LEVEL_NTC_ID[] { Variables.MMI_M_LEVEL_NTC_ID.L1 };
            EVC20_MMISelectLevel.Send();

            DmiActions.ShowInstruction(this, "Select and confirm Level 1");

            DmiActions.ShowInstruction(this, "Press the ‘Level’ button");

            Variables.MMI_Q_LEVEL_NTC_ID[] paramEvc20MmiQLevelNtcId =
                { MMI_Q_LEVEL_NTC_ID.ETCS_Level,
                MMI_Q_LEVEL_NTC_ID.ETCS_Level,
                MMI_Q_LEVEL_NTC_ID.ETCS_Level, 
                MMI_Q_LEVEL_NTC_ID.ETCS_Level,
                MMI_Q_LEVEL_NTC_ID.STM_ID,
                MMI_Q_LEVEL_NTC_ID.STM_ID };
            Variables.MMI_M_CURRENT_LEVEL[] paramEvc20MmiMCurrentLevel =
                { MMI_M_CURRENT_LEVEL.LastUsedLevel,
                MMI_M_CURRENT_LEVEL.NotLastUsedLevel,
                MMI_M_CURRENT_LEVEL.NotLastUsedLevel,
                MMI_M_CURRENT_LEVEL.NotLastUsedLevel,
                MMI_M_CURRENT_LEVEL.NotLastUsedLevel,
                MMI_M_CURRENT_LEVEL.NotLastUsedLevel };
            Variables.MMI_M_LEVEL_FLAG[] paramEvc20MmiMLevelFlag =
                { MMI_M_LEVEL_FLAG.MarkedLevel,
                MMI_M_LEVEL_FLAG.MarkedLevel,
                MMI_M_LEVEL_FLAG.MarkedLevel,
                MMI_M_LEVEL_FLAG.MarkedLevel,
                MMI_M_LEVEL_FLAG.MarkedLevel,
                MMI_M_LEVEL_FLAG.MarkedLevel };
            Variables.MMI_M_INHIBITED_LEVEL[] paramEvc20MmiMInhibitedLevel =
                { MMI_M_INHIBITED_LEVEL.NotInhibited,
                MMI_M_INHIBITED_LEVEL.NotInhibited,
                MMI_M_INHIBITED_LEVEL.NotInhibited,
                MMI_M_INHIBITED_LEVEL.NotInhibited,
                MMI_M_INHIBITED_LEVEL.NotInhibited,
                MMI_M_INHIBITED_LEVEL.NotInhibited };
            Variables.MMI_M_INHIBIT_ENABLE[] paramEvc20MmiMInhibitEnable =
                { MMI_M_INHIBIT_ENABLE.AllowedForInhibiting,
                MMI_M_INHIBIT_ENABLE.AllowedForInhibiting,
                MMI_M_INHIBIT_ENABLE.AllowedForInhibiting,
                MMI_M_INHIBIT_ENABLE.AllowedForInhibiting,
                MMI_M_INHIBIT_ENABLE.AllowedForInhibiting,
                MMI_M_INHIBIT_ENABLE.AllowedForInhibiting };
            Variables.MMI_M_LEVEL_NTC_ID[] paramEvc20MmiMLevelNtcId =
                { MMI_M_LEVEL_NTC_ID.L1,
                MMI_M_LEVEL_NTC_ID.L2,
                MMI_M_LEVEL_NTC_ID.L3,
                MMI_M_LEVEL_NTC_ID.L0,
                MMI_M_LEVEL_NTC_ID.CBTC,
                MMI_M_LEVEL_NTC_ID.AWS_TPWS };

            EVC20_MMISelectLevel.MMI_Q_LEVEL_NTC_ID = paramEvc20MmiQLevelNtcId;
            EVC20_MMISelectLevel.MMI_M_CURRENT_LEVEL = paramEvc20MmiMCurrentLevel;
            EVC20_MMISelectLevel.MMI_M_LEVEL_FLAG = paramEvc20MmiMLevelFlag;
            EVC20_MMISelectLevel.MMI_M_INHIBITED_LEVEL = paramEvc20MmiMInhibitedLevel;
            EVC20_MMISelectLevel.MMI_M_INHIBIT_ENABLE = paramEvc20MmiMInhibitEnable;
            EVC20_MMISelectLevel.MMI_M_LEVEL_NTC_ID = paramEvc20MmiMLevelNtcId;
            EVC20_MMISelectLevel.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Level window." + Environment.NewLine +
                                "2. A keyboard with a 3 x 2 array of buttons is displayed with the following buttons (left to right):" + Environment.NewLine +
                                "3. ‘Level 1’, ‘Level 2’, ‘Level3’ in the top row; ‘Level 0’, ‘CBTC’, ‘AWS_TPWS’ in the bottom row." + Environment.NewLine +
                                "4. The button text is grey." + Environment.NewLine +
                                "5. The buttons are displayed enabled.");

            /*
            Test Step 5
            Action: Use the test script file 22_5_2_c.xml to send EVC-20 with,MMI_N_LEVELS = 3MMI_Q_LEVEL_NTC_ID[0] = 1MMI_M_CURRENT_LEVEL[0]  = 1MMI_M_LEVEL_FLAG[0] = 1MMI_M_INHIBITED_LEVEL[0] = 0MMI_M_INHIBIT_ENABLE[0] = 1MMI_M_LEVEL_NTC_ID[0] = 0MMI_Q_LEVEL_NTC_ID[1] = 1MMI_M_CURRENT_LEVEL[1]  = 0MMI_M_LEVEL_FLAG[1] = 1MMI_M_INHIBITED_LEVEL[1] = 0MMI_M_INHIBIT_ENABLE[1] = 1MMI_M_LEVEL_NTC_ID[1] = 2MMI_Q_LEVEL_NTC_ID[2] = 0MMI_M_CURRENT_LEVEL[2]  = 0MMI_M_LEVEL_FLAG[2] = 1MMI_M_INHIBITED_LEVEL[2] = 0MMI_M_INHIBIT_ENABLE[2] = 1MMI_M_LEVEL_NTC_ID[2] = 20
            Expected Result: Verify the following information,The buttons of Level window and the value of an input field are changed refer to received packet EVC-20 as picture below,
            Test Step Comment: (1) MMI_gen 2197;    
            */
            XML.XML_22_5_2_c.Send(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. A keyboard with a 3 x 1 array of buttons is displayed with the following buttons(left to right):" + Environment.NewLine +
                                "2. ‘Level 0’, ‘Level 2’, ‘AWS_TPWS’ 3 in the top row; ‘Level 0’, ‘CBTC’, ‘AWS_TPWS’ in the bottom row.");

            /*
            Test Step 6
            Action: Use the test script file 22_5_2_b.xml to send EVC-20 again
            Expected Result: Verify the following information,The Level window is closed.Use the log file to confirm that there is no packet information (i.e. EVC-101, EVC-121) send out from DMI
            Test Step Comment: (1) MMI_gen 2277 (partly: immediately close the Level window);(2) MMI_gen 2277 (partly: No response shall be transmitted);
            */
            XML.XML_22_5_2_b.Send(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI closes the Level window");

            /*
            Test Step 7
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
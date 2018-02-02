using System;
using Testcase.Telegrams.EVCtoDMI;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 29.1 UTC time and offset time(by driver)
    /// TC-ID: 29.1
    /// 
    /// This test case is to verify the UTC time/ Offset time changed by driver. It shall comply with [ERA] standard and [MMI-ETCS-gen].
    /// 
    /// Tested Requirements:
    /// MMI_gen 2432; MMI_gen 2421 (partly: source by Driver);
    /// 
    /// Scenario:
    /// 1.Power on test system and activate cabin.
    /// 2.Perform Start of Mission to L1, SR mode
    /// 3.Select ‘Settings’ button then ‘Settings’ window is opened.
    /// 4.Select ‘Set Clock’ button  then chage Date and time and verify the changes shall be transmited to ETCS OB by packet EVC-109 to ETCS OB.
    /// 
    /// Used files:
    /// N/A
    /// </summary>
    public class TC_ID_29_1_UTC_time_and_offset_timeby_driver : TestcaseBase
    {

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint
            TraceInfo("This test case requires an ATP configuration change - " +
                      "See Precondition requirements. If this is not done manually, the test may fail!");

            /*
            Test Step 1
            Action: Power on the system and activate cabin
            Expected Result: DMI displays SB mode
            */
            DmiActions.ShowInstruction(this, "Power up the system");

            DmiActions.Start_ATP();
            DmiActions.Activate_Cabin_1(this);
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.StandBy;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in SB mode");

            /*
            Test Step 2
            Action: Perform SoM to L1, SR mode
            Expected Result: Mode changes to SR mode , L1
            */
            DmiActions.Set_Driver_ID(this, "1234");
            DmiActions.ShowInstruction(this, "Enter and confirm Driver ID");

            // Skip brake test

            DmiActions.Display_Level_Window(this);
            DmiActions.ShowInstruction(this, "Select and enter Level 1");

            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode =
                EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.StaffResponsible;
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Default; // Default window
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH =
                EVC30_MMIRequestEnable.EnabledRequests.SetLocalTimeDateAndOffset;
            EVC30_MMIRequestEnable.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in SR mode, Level 1.");

            /*
            Test Step 3
            Action: Select ‘Settings’ button and press ‘Set clock’ button
            Expected Result: The set clock window is opened
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Settings’ button, then press the ‘Set clock’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Set clock window with the title ‘Set clock (1/2)’." +
                                Environment.NewLine +
                                "2. Three data input fields for ‘Year’, ‘Month’ and ‘Day’ are displayed." +
                                Environment.NewLine +
                                "3. The data input fields have a label part with grey text (right-aligned) on a Dark-grey background." +
                                Environment.NewLine +
                                "4. The data part of the data input fields has left aligned text, the first field has Medium-grey text on a grey background;" +
                                Environment.NewLine +
                                "   the other data input fields data have dark-grey text on a Medium-grey background" +
                                Environment.NewLine +
                                "5. A dedicated numeric keypad is displayed below the data input fields with keys <1> to <9>," +
                                Environment.NewLine +
                                "   <Del> and <0>." + Environment.NewLine +
                                "6. Enabled ‘Close’ and ‘Next’ and a disabled ‘Previous’ button are displayed below the keypad." +
                                Environment.NewLine +
                                "7. Echo texts for UTC and local time are displayed to the left of the keypad in white." +
                                "8. An enabled ‘Yes’ button with the label ‘Clock set?’ is displayed in the bottom-left part of the window");

            /*
            Test Step 4
            Action: Change Data and Time to be different  from current values for example:Year = current year +1Month = current month +2Day = current day +3Hour = current hour +4Minute = current minute +5Second =current second +6Offset = current offset + 12
            Expected Result: Verify the following information,The new UTC time and offset time are changed and displayed according to the entered data from the driver.Use the log file to verify that DMI sends out packet EVC-109 to ETCS OB correctly
            Test Step Comment: MMI_gen 2432;MMI_gen 2421 (partly: source by Driver);
            */
            // Call generic Check Results Method
            DmiActions.ShowInstruction(this, "Set the date as follows:" + Environment.NewLine +
                                             "Year = ‘2018’, Month = ‘12’, Day = ‘30’; then press the ‘Next button’ and set the time as follows:" +
                                             Environment.NewLine +
                                             "Hour = ‘12’, Minute = ‘0’, Second = ‘0’ and Offset = ‘+15’");

            //EVC109_MMISetTimeMMI.MMI_T_UTC = 1546135200;
            //EVC109_MMISetTimeMMI.MMI_T_ZONE_OFFSET = 40;
            //EVC109_MMISetTimeMMI.CheckTelegram();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The time and date are displayed as set" + Environment.NewLine +
                                "2. The local time is displayed as ‘2018-12-30: 12:00:00’." + Environment.NewLine +
                                "3. The UTC time is displayed as  ‘2018-12-30: 02:00:00’.");

            /*
            Test Step 5
            Action: Change Data and Time to be different  from current values for example:Year = curtrent year +1Month = current month +2Day = current day +3Hour = current hour +4Minute = current minute +5Second =current second +6Offset = current offset - 12
            Expected Result: Verify the following information,The new UTC time and offset time are changed and displayed according to the entered data from the driver.Use the log file to verify that DMI sends out packet EVC-109 to ETCS OB correctly
            Test Step Comment: MMI_gen 2432;MMI_gen 2421 (partly: source by Driver);
            */
            DmiActions.ShowInstruction(this, "Set the date as follows:" + Environment.NewLine +
                                             "Year = ‘2017’, Month = ‘11’, Day = ‘29’; then press the ‘Next button’ and set the time as follows:" +
                                             Environment.NewLine +
                                             "Hour = ‘11’, Minute = ‘59’, Second = ‘59’ and Offset = ‘+9’");

            //EVC109_MMISetTimeMMI.MMI_T_UTC = 1511956799;
            //EVC109_MMISetTimeMMI.MMI_T_ZONE_OFFSET = 36;
            //EVC109_MMISetTimeMMI.CheckTelegram();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The time and date are displayed as set" + Environment.NewLine +
                                "2. The local time is displayed as ‘2017-11-29: 11:59:59’." + Environment.NewLine +
                                "3. The UTC time is displayed as  ‘2017-11-29: 02:59:59’.");

            /*
            Test Step 6
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
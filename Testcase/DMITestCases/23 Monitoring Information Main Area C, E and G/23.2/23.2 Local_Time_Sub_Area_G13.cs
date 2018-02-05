using System;

namespace Testcase.DMITestCases
{
    /// <summary>
    /// 23.2 Local Time: Sub-Area G13
    /// TC-ID: 18.2
    /// 
    /// This test case verifies the display of the local time on DMI that shall comply with [ERA-ERTMS] standard and [MMI-ETCS-gen].
    /// 
    /// Tested Requirements:
    /// MMI_gen 176; MMI_gen 3613; MMI_gen 3852-1 (THR)(partly: flashing colons);
    /// 
    /// Scenario:
    /// 1.Test system is powered on
    /// 2.Local time data is verified
    /// 3.SoM is completed in SR mode, ETCS level 
    /// 14.Local time format is verified
    /// 
    /// Used files:
    /// N/A
    /// </summary>
    public class TC_ID_18_2_Local_Time_Sub_Area_G13 : TestcaseBase
    {
        public override bool TestcaseEntryPoint()
        {
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 0;
            // Testcase entrypoint


            MakeTestStepHeader(1, UniqueIdentifier++, "Power on the test system",
                "The driver message “Driver’s cab not active” displays on the DMI screen with timestamp on the left. Use the log file to confirm the timestamp is equal to MMI_T_UTC + MMI_T_ZONE_OFFSET from EVC-3");
            /*
            Test Step 1
            Action: Power on the test system
            Expected Result: The driver message “Driver’s cab not active” displays on the DMI screen with timestamp on the left. Use the log file to confirm the timestamp is equal to MMI_T_UTC + MMI_T_ZONE_OFFSET from EVC-3
            Test Step Comment: (1) MMI_gen 176 (partly: derived time)
            */
            StartUp();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the message ‘Driver’s cab not active’, with a timestamp to the left.");

            MakeTestStepHeader(2, UniqueIdentifier++,
                "Perform SoM to SR mode, ETCS level 1 and verified the presentation on the DMI screen",
                "DMI displays in SR mode, Level 1. The local time is displayed in format hh:mm:ss (24h) on sub-area G13.DMI displays the local time as a single line in grey colour. The background colour is dark-blue.The colon ‘:’ of local time flashes (shown and hide)");
            /*
            Test Step 2
            Action: Perform SoM to SR mode, ETCS level 1 and verified the presentation on the DMI screen
            Expected Result: DMI displays in SR mode, Level 1. The local time is displayed in format hh:mm:ss (24h) on sub-area G13.DMI displays the local time as a single line in grey colour. The background colour is dark-blue.The colon ‘:’ of local time flashes (shown and hide)
            Test Step Comment: (1) MMI_gen 176 (partly: format, sub-area G13)(2) MMI_gen 3613(3) MMI_gen 3852-1 (THR) (partly: flashing colons)
            */
            DmiActions.Complete_SoM_L1_SR(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in SR mode, Level 1" + Environment.NewLine +
                                "2. The local time is displayed in sub-area G13 in hh:mm:ss format." +
                                Environment.NewLine +
                                "3. Local time is displayed on one line in grey on a Dark-blue background." +
                                Environment.NewLine +
                                "4. The colons in the time are displayed flashing (shown/hidden).");

            MakeTestStepHeader(3, UniqueIdentifier++, "End of test", "");

            /*
            Test Step 3
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
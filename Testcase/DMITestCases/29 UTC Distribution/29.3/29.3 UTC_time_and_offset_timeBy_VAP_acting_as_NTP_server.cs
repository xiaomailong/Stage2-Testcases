using System;
using Testcase.Telegrams.EVCtoDMI;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 29.3 UTC time and offset time(By VAP acting as NTP server)
    /// TC-ID: 29.3
    /// 
    /// This test case is to verify the UTC time/Offset time changed by time source: VAP acting as NTP server and it complies with [ERA] standard and [MMI-ETCS-gen].
    /// 
    /// Tested Requirements:
    /// MMI_gen 12062; MMI_gen 2421 (partly: VAP acting as NTP server); MMI_gen 11284; MMI_gen 58;
    /// 
    /// Scenario:
    /// 1.Power on test system and activate cabin A (MMI 1)
    /// 2.Perform Start of Mission to L1, SR mode.
    /// 3.Use test script file to send data packet of UTC time/Offset time to DMI.
    /// 4.Verify that DMI time is updated
    /// 5.Select ‘Settings’ button then ‘Settings’ window is opened.
    /// 6.Select ‘Set Clock’ button and verify date and time that display on DMI.
    /// 7.Restart OTE and retest step 1-6 with Cabin B (MMI 2)
    /// 
    /// Used files:
    /// 29_3_1.xml, 29_3_2.xml.
    /// </summary>
    public class TC_ID_29_3_UTC_time_and_offset_timeBy_VAP_acting_as_NTP_server : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // 1.  Power off test system 2.  Set the following tags name in configuration file (See the instruction in Appendix 1)
            // CLOCK_TIME_SOURCE =  5 (by VAP)

            // Call the TestCaseBase PreExecution
            base.PreExecution();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays SR mode.

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 0;
            // Testcase entrypoint
            TraceInfo("This test case requires an ATP configuration change - " +
                      "See Precondition requirements. If this is not done manually, the test may fail!");

            TraceHeader("Test Step 1");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Power on test system and activate the cabin A (MMI 1)");
            TraceReport("Expected Result");
            TraceInfo("DMI displays SB mode");
            /*
            Test Step 1
            Action: Power on test system and activate the cabin A (MMI 1)
            Expected Result: DMI displays SB mode
            */
            DmiActions.Start_ATP();
            DmiActions.Activate_Cabin_1(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in SB mode, Level 1.");

            TraceHeader("Test Step 2");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Perform SoM to L1, SR mode");
            TraceReport("Expected Result");
            TraceInfo("Mode changes to SR mode");
            /*
            Test Step 2
            Action: Perform SoM to L1, SR mode
            Expected Result: Mode changes to SR mode
            */
            DmiActions.Complete_SoM_L1_SR(this);

            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Default; // Default window
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH =
                EVC30_MMIRequestEnable.EnabledRequests.SetLocalTimeDateAndOffset;
            EVC30_MMIRequestEnable.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in SR mode, Level 1.");

            TraceHeader("Test Step 3");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Use the test script file 29_3_1.xml to send packet EVC-TBD with,MMI_T_UTC = 946728000(2000-01-01,12:00:00)MMI_T_Zone_OFFSET =+5");
            TraceReport("Expected Result");
            TraceInfo("Verify the following information:(1) DMI time is updated only its offset time");
            /*
            Test Step 3
            Action: Use the test script file 29_3_1.xml to send packet EVC-TBD with,MMI_T_UTC = 946728000(2000-01-01,12:00:00)MMI_T_Zone_OFFSET =+5
            Expected Result: Verify the following information:(1) DMI time is updated only its offset time
            Test Step Comment: (1) MMI_gen 12062; MMI_gen 2421 (partly: VAP acting as NTP server);MMI_gen 11284;Note: ‘EVC-TBD’ will be provided by VSIS in order to support VAP interface 
            */
            XML_29_3(msgType.msgType1);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI just changes the local time displayed, adjusted by the offset time (+ 1 1/4 h)");

            TraceHeader("Test Step 4");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Select ‘Settings’ button and press ‘Set clock’ button");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following information:The set clock window is opened(1) The DMI date (yyyy-mm-dd) shall not be changed by test script");
            /*
            Test Step 4
            Action: Select ‘Settings’ button and press ‘Set clock’ button
            Expected Result: Verify the following information:The set clock window is opened(1) The DMI date (yyyy-mm-dd) shall not be changed by test script
            Test Step Comment: (1) MMI_gen 12062; MMI_gen 11284;
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Settings’ button, then press the ‘Set clock’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Set clock window." + Environment.NewLine +
                                "2. The date displayed is unchanged.");

            TraceHeader("Test Step 5");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Use the test script file 29_3_2.xml to send packet EVC-TBD with,MMI_T_UTC = 946771200(2000-01-02,12:00:00)MMI_T_Zone_OFFSET =-5");
            TraceReport("Expected Result");
            TraceInfo("Verify the following information:(1) DMI time is updated only its offset time");
            /*
            Test Step 5
            Action: Use the test script file 29_3_2.xml to send packet EVC-TBD with,MMI_T_UTC = 946771200(2000-01-02,12:00:00)MMI_T_Zone_OFFSET =-5
            Expected Result: Verify the following information:(1) DMI time is updated only its offset time
            Test Step Comment: (1) MMI_gen 12062; MMI_gen 2421 (partly: VAP acting as NTP server);MMI_gen 11284;Note: ‘EVC-TBD’ will be provided by VSIS in order to support VAP interface
            */
            DmiActions.ShowInstruction(this, @"Close the ‘Set clock’ window");

            XML_29_3(msgType.msgType2);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI just changes the local time displayed, adjusted by the offset time (- 1 1/4 h)");

            TraceHeader("Test Step 6");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Select ‘Settings’ button and press ‘Set clock’ button.Then, observe information that will display on the Set clock window");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following information:The set clock window is opened(1) The DMI date (yyyy-mm-dd) is not be changed by test script");
            /*
            Test Step 6
            Action: Select ‘Settings’ button and press ‘Set clock’ button.Then, observe information that will display on the Set clock window
            Expected Result: Verify the following information:The set clock window is opened(1) The DMI date (yyyy-mm-dd) is not be changed by test script
            Test Step Comment: (1) MMI_gen 12062; MMI_gen 11284;
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Settings’ button, then press the ‘Set clock’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Set clock window." + Environment.NewLine +
                                "2. The date displayed is unchanged.");

            TraceHeader("Test Step 7");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Restart OTE and DMI then retest step1 to to 6 with cabin B (MMI 2)");
            TraceReport("Expected Result");
            TraceInfo("Same as the test result in step 1 to 6");
            /*
            Test Step 7
            Action: Restart OTE and DMI then retest step1 to to 6 with cabin B (MMI 2)
            Expected Result: Same as the test result in step 1 to 6
            Test Step Comment: MMI_gen 58;
            */
            DmiActions.ShowInstruction(this, @"Power down the system, wait at least 10s then power up the system.");

            // force SoM as above with Cabin 2
            DmiActions.Start_ATP();

            // Set train running number, cab 1 active, and other defaults
            DmiActions.Activate_Cabin_1(this);

            // Set driver ID
            DmiActions.Set_Driver_ID(this, "1234");

            // Set to level 1 and SR mode
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Level = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_LEVEL.L1;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode =
                EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.StaffResponsible;

            // Enable standard buttons including Start, and display Default window.
            DmiActions.Finished_SoM_Default_Window(this);

            // Ignore Step 1 as above...

            // Repeat Step 2
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Default; // Default window
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH =
                EVC30_MMIRequestEnable.EnabledRequests.SetLocalTimeDateAndOffset;
            EVC30_MMIRequestEnable.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in SR mode, Level 1.");

            // Repeat Step 3
            XML_29_3(msgType.msgType1);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI just changes the local time displayed, adjusted by the offset time (+ 1 1/4 h)");

            DmiActions.ShowInstruction(this, @"Press the ‘Settings’ button, then press the ‘Set clock’ button");

            // Repeat Step 4
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Set clock window." + Environment.NewLine +
                                "2. The date displayed is unchanged.");

            // Repeat Step 5
            DmiActions.ShowInstruction(this, @"Close the ‘Set clock’ window");

            XML_29_3(msgType.msgType2);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI just changes the local time displayed, adjusted by the offset time (- 1 1/4 h)");

            // Repeat Step 6
            DmiActions.ShowInstruction(this, @"Press the ‘Settings’ button, then press the ‘Set clock’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Set clock window." + Environment.NewLine +
                                "2. The date displayed is unchanged.");

            TraceHeader("Test Step 8");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("End of test");
            TraceReport("Expected Result");
            TraceInfo("");
            /*
            Test Step 8
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }

        #region XML_29_3_DMI_Test_Specification

        private enum msgType : byte
        {
            msgType1,
            msgType2
        }

        private void XML_29_3(msgType packetSelector)
        {
            switch (packetSelector)
            {
                case msgType.msgType1:
                    EVC3_MMISetTimeATP.MMI_T_UTC = 946728000;
                    EVC3_MMISetTimeATP.MMI_T_ZONE_OFFSET = 5;
                    break;
                case msgType.msgType2:
                    EVC3_MMISetTimeATP.MMI_T_UTC = 946771200;
                    EVC3_MMISetTimeATP.MMI_T_ZONE_OFFSET = -5;
                    break;
            }

            EVC3_MMISetTimeATP.Send();
        }

        #endregion
    }
}
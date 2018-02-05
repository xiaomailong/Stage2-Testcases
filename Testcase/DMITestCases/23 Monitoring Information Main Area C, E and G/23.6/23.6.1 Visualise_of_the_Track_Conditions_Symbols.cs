using System;
using System.Collections.Generic;
using Testcase.Telegrams.EVCtoDMI;

namespace Testcase.DMITestCases
{
    /// <summary>
    /// 23.6.1 Visualise of the Track Conditions Symbols
    /// TC-ID: 18.6.1
    /// 
    /// This test case verifies the display information of Track conditions symbols, The symbols are display/remove correctly refer to requirement specification.
    /// 
    /// Tested Requirements:
    /// MMI_gen 7082; MMI_gen 10469; MMI_gen 663; MMI_gen 664; MMI_gen 10488;  MMI_gen 10471; MMI_gen 667; MMI_gen 7085; MMI_gen 9516 (partly: symbol requires driver's action, non-acknowledgable); MMI_gen 12025 (partly: symbol requires driver's action, non-acknowledgable);
    /// 
    /// Scenario:
    /// Use the test script file to send a packet to DMI. Then, verifies the display information of track condition symbols..Perform SoM until 'Start' button is pressed. Then, verify that track condition symbol is disappear due to there is pending acknowledgement.Note: Each step of test script file in executed continuously, Tester need to verify expected result within specific time (5 second).
    /// 
    /// Used files:
    /// 18_6_1.xml
    /// </summary>
    public class TC_ID_18_6_1_Track_Conditions : TestcaseBase
    {

        public override bool TestcaseEntryPoint()
        {
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 0;
            // Testcase entrypoint
            StartUp();
            DmiActions.Complete_SoM_L1_SB(this);

            MakeTestStepHeader(1, UniqueIdentifier++,
                "Use the test script file 18_6_1.xml to send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 0MMI_N_TRACKCONDITIONS = 1MMI_NID_TRACKCOND = 0 MMI_Q_TRACKCOND_STEP = 1MMI_M_TRACKCOND_TYPE = 64MMI_Q_TRACKCOND_ACTION_START = 1MMI_Q_TRACKCOND_ACTION_END = 0",
                "Verify the following information,(1)   There is no symbol display in sub-area B3-B5");
            /*
            Test Step 1
            Action: Use the test script file 18_6_1.xml to send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 0MMI_N_TRACKCONDITIONS = 1MMI_NID_TRACKCOND = 0 MMI_Q_TRACKCOND_STEP = 1MMI_M_TRACKCOND_TYPE = 64MMI_Q_TRACKCOND_ACTION_START = 1MMI_Q_TRACKCOND_ACTION_END = 0
            Expected Result: Verify the following information,(1)   There is no symbol display in sub-area B3-B5
            Test Step Comment: (1) MMI_gen 7082 (partly: MMI_M_TRACKCOND_TYPE is invalid);
            */
            // Steps 1-9 are carried out in XML_18_6_1.cs
            XML_18_6_1();

            MakeTestStepHeader(2, UniqueIdentifier++,
                "Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 0MMI_N_TRACKCONDITIONS = 1MMI_NID_TRACKCOND = 1MMI_Q_TRACKCOND_STEP = 8MMI_M_TRACKCOND_TYPE = 3MMI_Q_TRACKCOND_ACTION_START = 1MMI_Q_TRACKCOND_ACTION_END = 0",
                "Verify the following information,(1)   There is no symbol display in sub-area B3-B5");
            /*
            Test Step 2
            Action: Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 0MMI_N_TRACKCONDITIONS = 1MMI_NID_TRACKCOND = 1MMI_Q_TRACKCOND_STEP = 8MMI_M_TRACKCOND_TYPE = 3MMI_Q_TRACKCOND_ACTION_START = 1MMI_Q_TRACKCOND_ACTION_END = 0
            Expected Result: Verify the following information,(1)   There is no symbol display in sub-area B3-B5
            Test Step Comment: (1) MMI_gen 7082 (partly: MMI_Q_TRACKCOND_STEP is invalid);
            */

            MakeTestStepHeader(3, UniqueIdentifier++,
                "Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 0MMI_N_TRACKCONDITIONS = 1MMI_NID_TRACKCOND = 2 MMI_Q_TRACKCOND_STEP = 1MMI_M_TRACKCOND_TYPE = 3MMI_Q_TRACKCOND_ACTION_START = 1MMI_Q_TRACKCOND_ACTION_END = 0",
                "Verify the following information,(1)   DMI displays symbol TC02 in sub-area B3.(2)   The symbols is displayed without yellow flashing frame");
            /*
            Test Step 3
            Action: Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 0MMI_N_TRACKCONDITIONS = 1MMI_NID_TRACKCOND = 2 MMI_Q_TRACKCOND_STEP = 1MMI_M_TRACKCOND_TYPE = 3MMI_Q_TRACKCOND_ACTION_START = 1MMI_Q_TRACKCOND_ACTION_END = 0
            Expected Result: Verify the following information,(1)   DMI displays symbol TC02 in sub-area B3.(2)   The symbols is displayed without yellow flashing frame
            Test Step Comment: (1) MMI_gen 10488 (partly: left to right filling B3);(2) MMI_gen 664 (partly: ACTION = 1);
            */

            MakeTestStepHeader(4, UniqueIdentifier++,
                "Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 1MMI_N_TRACKCONDITIONS = 1MMI_NID_TRACKCOND = 3 MMI_Q_TRACKCOND_STEP = 1MMI_M_TRACKCOND_TYPE = 3MMI_Q_TRACKCOND_ACTION_START = 0MMI_Q_TRACKCOND_ACTION_END = 0",
                "Verify the following information,(1)    DMI displays symbol TC03 in sub-area B4.(2)   Sound Sinfo is played.(3)   The yellow flashing frame is displayed surrond TC03 symbol");
            /*
            Test Step 4
            Action: Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 1MMI_N_TRACKCONDITIONS = 1MMI_NID_TRACKCOND = 3 MMI_Q_TRACKCOND_STEP = 1MMI_M_TRACKCOND_TYPE = 3MMI_Q_TRACKCOND_ACTION_START = 0MMI_Q_TRACKCOND_ACTION_END = 0
            Expected Result: Verify the following information,(1)    DMI displays symbol TC03 in sub-area B4.(2)   Sound Sinfo is played.(3)   The yellow flashing frame is displayed surrond TC03 symbol
            Test Step Comment: (1) MMI_gen 10469 (partly: MMI_Q_TRACKCOND_UPDATE = 1, MMI_gen 662); MMI_gen 10488 (partly: Next area shall be used, left to right filling B4);(2) MMI_gen 663; MMI_gen 9516 (partly: symbol requires driver's action, non-acknowledgable); MMI_gen 12025 (partly: symbol requires driver's action, non-acknowledgable);(3) MMI_gen 664 (partly: ACTION = 0);
            */

            MakeTestStepHeader(5, UniqueIdentifier++,
                "Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 1MMI_N_TRACKCONDITIONS = 1MMI_NID_TRACKCOND = 4MMI_Q_TRACKCOND_STEP = 2MMI_M_TRACKCOND_TYPE = 3MMI_Q_TRACKCOND_ACTION_START = 1MMI_Q_TRACKCOND_ACTION_END = 0",
                "Verify the following information,(1)    DMI displays symbol TC01 in sub-area B5");
            /*
            Test Step 5
            Action: Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 1MMI_N_TRACKCONDITIONS = 1MMI_NID_TRACKCOND = 4MMI_Q_TRACKCOND_STEP = 2MMI_M_TRACKCOND_TYPE = 3MMI_Q_TRACKCOND_ACTION_START = 1MMI_Q_TRACKCOND_ACTION_END = 0
            Expected Result: Verify the following information,(1)    DMI displays symbol TC01 in sub-area B5
            Test Step Comment: (1) MMI_gen 10488 (partly: Next area shall be used, left to right filling B5);
            */

            MakeTestStepHeader(6, UniqueIdentifier++,
                "Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 1MMI_N_TRACKCONDITIONS = 1MMI_NID_TRACKCOND = 5MMI_Q_TRACKCOND_STEP = 3MMI_M_TRACKCOND_TYPE = 3MMI_Q_TRACKCOND_ACTION_START = 0MMI_Q_TRACKCOND_ACTION_END = 1",
                "Verify the following information,(1)    The display in sub-area B3-B5 still not change because of all areas are already displaying symbols");
            /*
            Test Step 6
            Action: Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 1MMI_N_TRACKCONDITIONS = 1MMI_NID_TRACKCOND = 5MMI_Q_TRACKCOND_STEP = 3MMI_M_TRACKCOND_TYPE = 3MMI_Q_TRACKCOND_ACTION_START = 0MMI_Q_TRACKCOND_ACTION_END = 1
            Expected Result: Verify the following information,(1)    The display in sub-area B3-B5 still not change because of all areas are already displaying symbols
            Test Step Comment: (1) MMI_gen 10488 (partly: wait that B3, B4 or B5 is free);
            */

            MakeTestStepHeader(7, UniqueIdentifier++,
                "Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 1MMI_N_TRACKCONDITIONS = 1MMI_NID_TRACKCOND = 3MMI_Q_TRACKCOND_STEP = 4",
                "Verify the following information,(1)   The symbol TC03 in sub-area B4 is removed.(2)   The symbol TC01 in sub-area B5 is moved to sub-area B4.(3)   The symbol TC04 is display in sub-area B5");
            /*
            Test Step 7
            Action: Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 1MMI_N_TRACKCONDITIONS = 1MMI_NID_TRACKCOND = 3MMI_Q_TRACKCOND_STEP = 4
            Expected Result: Verify the following information,(1)   The symbol TC03 in sub-area B4 is removed.(2)   The symbol TC01 in sub-area B5 is moved to sub-area B4.(3)   The symbol TC04 is display in sub-area B5
            Test Step Comment: (1) MMI_gen 10471;(2) MMI_gen 667;(3) MMI_gen 10488 (partly: next area is used);
            */

            MakeTestStepHeader(8, UniqueIdentifier++,
                "Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 1MMI_N_TRACKCONDITIONS = 1MMI_NID_TRACKCOND = 6MMI_Q_TRACKCOND_STEP = 1MMI_M_TRACKCOND_TYPE = 3MMI_Q_TRACKCOND_ACTION_START = 0MMI_Q_TRACKCOND_ACTION_END = 0",
                "Verify the following information,(1)    The display in sub-area B3-B5 still not change because of all areas are already displaying symbols");
            /*
            Test Step 8
            Action: Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 1MMI_N_TRACKCONDITIONS = 1MMI_NID_TRACKCOND = 6MMI_Q_TRACKCOND_STEP = 1MMI_M_TRACKCOND_TYPE = 3MMI_Q_TRACKCOND_ACTION_START = 0MMI_Q_TRACKCOND_ACTION_END = 0
            Expected Result: Verify the following information,(1)    The display in sub-area B3-B5 still not change because of all areas are already displaying symbols
            Test Step Comment: (1) MMI_gen 10488 (partly: wait that B3, B4 or B5 is free);
            */

            MakeTestStepHeader(9, UniqueIdentifier++,
                "Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 0MMI_N_TRACKCONDITIONS = 1MMI_NID_TRACKCOND = 0 MMI_Q_TRACKCOND_STEP = 3MMI_M_TRACKCOND_TYPE = 3MMI_Q_TRACKCOND_ACTION_START = 0MMI_Q_TRACKCOND_ACTION_END = 0",
                "Verify the following information,(1)   All of symbols TC02, TC01 and TC04 are removed from sub-area B3-B5.(2)   DMI displays symbol TC05 at sub-area B3.(3)   The symbol TC03 is not display because of stored track conditions is deleted");
            /*
            Test Step 9
            Action: Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 0MMI_N_TRACKCONDITIONS = 1MMI_NID_TRACKCOND = 0 MMI_Q_TRACKCOND_STEP = 3MMI_M_TRACKCOND_TYPE = 3MMI_Q_TRACKCOND_ACTION_START = 0MMI_Q_TRACKCOND_ACTION_END = 0
            Expected Result: Verify the following information,(1)   All of symbols TC02, TC01 and TC04 are removed from sub-area B3-B5.(2)   DMI displays symbol TC05 at sub-area B3.(3)   The symbol TC03 is not display because of stored track conditions is deleted
            Test Step Comment: (1) MMI_gen 10469 (partly: MMI_Q_TRACKCOND_UPDATE = 0, delete all stored track conditions);(2) MMI_gen 10469 (partly: use new track conditions received);  (3) MMI_gen 10469 (partly: Delete all stored track conditions);
            */

            MakeTestStepHeader(10, UniqueIdentifier++, "Perform SoM until 'Start' button is pressed",
                "DMI is display only MO10 symbol in sub-area C1");
            /*
            Test Step 10
            Action: Perform SoM until 'Start' button is pressed
            Expected Result: DMI is display only MO10 symbol in sub-area C1
            Test Step Comment: MMI_gen 7085;
            */
            DmiActions.ShowInstruction(this, "Perform SoM until after pressing the ‘Start’ button.");
            EVC32_MMITrackConditions.MMI_Q_TRACKCOND_UPDATE = 0;
            EVC32_MMITrackConditions.TrackConditions = new List<TrackCondition>();
            EVC32_MMITrackConditions.Send();
            DmiActions.Finished_SoM_Default_Window(this);
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 263;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI (only) displays symbol MO10 in sub-area C1");

            MakeTestStepHeader(11, UniqueIdentifier++, "End of test", "");

            /*
            Test Step 11
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }

        #region Send_XML_18_6_1_DMI_Test_Specification

        private void XML_18_6_1()
        {
            // Step 1
            EVC32_MMITrackConditions.MMI_Q_TRACKCOND_UPDATE = 0;

            TrackCondition trackCondition = new TrackCondition
            {
                MMI_O_TRACKCOND_ANNOUNCE = 0,
                MMI_O_TRACKCOND_START = 0,
                MMI_O_TRACKCOND_END = 0,
                MMI_NID_TRACKCOND = 0,
                MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Invalid,
                MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.AnnounceArea,
                MMI_Q_TRACKCOND_ACTION_START = Variables.MMI_Q_TRACKCOND_ACTION.WithoutDriverAction,
                MMI_Q_TRACKCOND_ACTION_END = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction
            };

            List<TrackCondition> trackConditionList = new List<TrackCondition> {trackCondition};

            EVC32_MMITrackConditions.TrackConditions = trackConditionList;

            EVC32_MMITrackConditions.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays no symbol in sub-areas B3-B5.");

            // Step 2
            trackCondition.MMI_NID_TRACKCOND = 1;
            trackCondition.MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Pantograph;
            trackCondition.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.Spare8;
            trackCondition.MMI_Q_TRACKCOND_ACTION_START = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction;

            EVC32_MMITrackConditions.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays no symbol in sub-areas B3-B5.");

            // Step 3
            trackCondition.MMI_NID_TRACKCOND = 2;
            trackCondition.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.AnnounceArea;
            trackCondition.MMI_Q_TRACKCOND_ACTION_START = Variables.MMI_Q_TRACKCOND_ACTION.WithoutDriverAction;

            EVC32_MMITrackConditions.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays symbol TC02 in sub-area B3 without a flashing yellow frame.");

            // Step 4
            EVC32_MMITrackConditions.MMI_Q_TRACKCOND_UPDATE = 1;

            trackCondition.MMI_NID_TRACKCOND = 3;
            trackCondition.MMI_Q_TRACKCOND_ACTION_START = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction;

            EVC32_MMITrackConditions.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays symbol TC03 in sub-area B4 with a flashing yellow frame." +
                                Environment.NewLine +
                                "2. The ‘Sinfo’ sound is played.");

            // Step 5
            trackCondition.MMI_NID_TRACKCOND = 4;
            trackCondition.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.InsideArea_Active;
            trackCondition.MMI_Q_TRACKCOND_ACTION_START = Variables.MMI_Q_TRACKCOND_ACTION.WithoutDriverAction;

            EVC32_MMITrackConditions.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays symbol TC01 in sub-area B5.");

            // Step 6
            trackCondition.MMI_NID_TRACKCOND = 5;
            trackCondition.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.LeaveArea;
            trackCondition.MMI_Q_TRACKCOND_ACTION_START = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction;
            trackCondition.MMI_Q_TRACKCOND_ACTION_END = Variables.MMI_Q_TRACKCOND_ACTION.WithoutDriverAction;

            EVC32_MMITrackConditions.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI does not change sub-areas B3-B5 (all areas are already displaying symbols.");

            // Step 7
            trackCondition.MMI_NID_TRACKCOND = 3;
            trackCondition.MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Non_Stopping_Area;
            trackCondition.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.RemoveTC;
            trackCondition.MMI_Q_TRACKCOND_ACTION_END = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction;

            EVC32_MMITrackConditions.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI removes symbol TC03 in sub-area B4." + Environment.NewLine +
                                "2. DMI removes symbol TC01 from sub-area B5 and displays it in sub-area B4" +
                                Environment.NewLine +
                                "2. DMI displays symbol TC04 in sub-area B5");

            // Step 8
            trackCondition.MMI_NID_TRACKCOND = 6;
            trackCondition.MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Pantograph;
            trackCondition.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.AnnounceArea;
            trackCondition.MMI_Q_TRACKCOND_ACTION_END = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction;

            EVC32_MMITrackConditions.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI does not change sub-areas B3-B5 (all areas are already displaying symbols.");

            // Step 9
            EVC32_MMITrackConditions.MMI_Q_TRACKCOND_UPDATE = 0;

            trackCondition.MMI_NID_TRACKCOND = 0;
            trackCondition.MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Pantograph;
            trackCondition.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.LeaveArea;
            trackCondition.MMI_Q_TRACKCOND_ACTION_END = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction;

            EVC32_MMITrackConditions.Send();

            WaitForVerification(
                "Check the following (TC-03 is not displayed because stored track conditions are deleted):" +
                Environment.NewLine + Environment.NewLine +
                "1. DMI removes symbols TC02, TC01 and TC04 from sub-areas B3-B5." + Environment.NewLine +
                "2. DMI displays symbol TC05 in sub-area B3");
        }

        #endregion
    }
}
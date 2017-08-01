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
    public class Visualise_of_the_Track_Conditions_Symbols : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // Test system is powered on.Activate Cabin A.SoM is performed until Level 1 is selected and confirmed.Main window is closed.

            // Call the TestCaseBase PreExecution
            base.PreExecution();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in SB mode, Level 1.

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint


            /*
            Test Step 1
            Action: Use the test script file 18_6_1.xml to send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 0MMI_N_TRACKCONDITIONS = 1MMI_NID_TRACKCOND = 0 MMI_Q_TRACKCOND_STEP = 1MMI_M_TRACKCOND_TYPE = 64MMI_Q_TRACKCOND_ACTION_START = 1MMI_Q_TRACKCOND_ACTION_END = 0
            Expected Result: Verify the following information,(1)   There is no symbol display in sub-area B3-B5
            Test Step Comment: (1) MMI_gen 7082 (partly: MMI_M_TRACKCOND_TYPE is invalid);
            */
            // Call generic Check Results Method
            DmiExpectedResults.Verify_the_following_information_1_There_is_no_symbol_display_in_sub_area_B3_B5(this);


            /*
            Test Step 2
            Action: Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 0MMI_N_TRACKCONDITIONS = 1MMI_NID_TRACKCOND = 1MMI_Q_TRACKCOND_STEP = 8MMI_M_TRACKCOND_TYPE = 3MMI_Q_TRACKCOND_ACTION_START = 1MMI_Q_TRACKCOND_ACTION_END = 0
            Expected Result: Verify the following information,(1)   There is no symbol display in sub-area B3-B5
            Test Step Comment: (1) MMI_gen 7082 (partly: MMI_Q_TRACKCOND_STEP is invalid);
            */
            // Call generic Check Results Method
            DmiExpectedResults.Verify_the_following_information_1_There_is_no_symbol_display_in_sub_area_B3_B5(this);


            /*
            Test Step 3
            Action: Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 0MMI_N_TRACKCONDITIONS = 1MMI_NID_TRACKCOND = 2 MMI_Q_TRACKCOND_STEP = 1MMI_M_TRACKCOND_TYPE = 3MMI_Q_TRACKCOND_ACTION_START = 1MMI_Q_TRACKCOND_ACTION_END = 0
            Expected Result: Verify the following information,(1)   DMI displays symbol TC02 in sub-area B3.(2)   The symbols is displayed without yellow flashing frame
            Test Step Comment: (1) MMI_gen 10488 (partly: left to right filling B3);(2) MMI_gen 664 (partly: ACTION = 1);
            */


            /*
            Test Step 4
            Action: Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 1MMI_N_TRACKCONDITIONS = 1MMI_NID_TRACKCOND = 3 MMI_Q_TRACKCOND_STEP = 1MMI_M_TRACKCOND_TYPE = 3MMI_Q_TRACKCOND_ACTION_START = 0MMI_Q_TRACKCOND_ACTION_END = 0
            Expected Result: Verify the following information,(1)    DMI displays symbol TC03 in sub-area B4.(2)   Sound Sinfo is played.(3)   The yellow flashing frame is displayed surrond TC03 symbol
            Test Step Comment: (1) MMI_gen 10469 (partly: MMI_Q_TRACKCOND_UPDATE = 1, MMI_gen 662); MMI_gen 10488 (partly: Next area shall be used, left to right filling B4);(2) MMI_gen 663; MMI_gen 9516 (partly: symbol requires driver's action, non-acknowledgable); MMI_gen 12025 (partly: symbol requires driver's action, non-acknowledgable);(3) MMI_gen 664 (partly: ACTION = 0);
            */


            /*
            Test Step 5
            Action: Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 1MMI_N_TRACKCONDITIONS = 1MMI_NID_TRACKCOND = 4MMI_Q_TRACKCOND_STEP = 2MMI_M_TRACKCOND_TYPE = 3MMI_Q_TRACKCOND_ACTION_START = 1MMI_Q_TRACKCOND_ACTION_END = 0
            Expected Result: Verify the following information,(1)    DMI displays symbol TC01 in sub-area B5
            Test Step Comment: (1) MMI_gen 10488 (partly: Next area shall be used, left to right filling B5);
            */


            /*
            Test Step 6
            Action: Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 1MMI_N_TRACKCONDITIONS = 1MMI_NID_TRACKCOND = 5MMI_Q_TRACKCOND_STEP = 3MMI_M_TRACKCOND_TYPE = 3MMI_Q_TRACKCOND_ACTION_START = 0MMI_Q_TRACKCOND_ACTION_END = 1
            Expected Result: Verify the following information,(1)    The display in sub-area B3-B5 still not change because of all areas are already displaying symbols
            Test Step Comment: (1) MMI_gen 10488 (partly: wait that B3, B4 or B5 is free);
            */
            // Call generic Check Results Method
            DmiExpectedResults
                .Verify_the_following_information_1_The_display_in_sub_area_B3_B5_still_not_change_because_of_all_areas_are_already_displaying_symbols(this);


            /*
            Test Step 7
            Action: Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 1MMI_N_TRACKCONDITIONS = 1MMI_NID_TRACKCOND = 3MMI_Q_TRACKCOND_STEP = 4
            Expected Result: Verify the following information,(1)   The symbol TC03 in sub-area B4 is removed.(2)   The symbol TC01 in sub-area B5 is moved to sub-area B4.(3)   The symbol TC04 is display in sub-area B5
            Test Step Comment: (1) MMI_gen 10471;(2) MMI_gen 667;(3) MMI_gen 10488 (partly: next area is used);
            */
            // Call generic Action Method
            DmiActions
                .Send_EVC_32_with_MMI_Q_TRACKCOND_UPDATE_1MMI_N_TRACKCONDITIONS_1MMI_NID_TRACKCOND_3MMI_Q_TRACKCOND_STEP_4(this);


            /*
            Test Step 8
            Action: Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 1MMI_N_TRACKCONDITIONS = 1MMI_NID_TRACKCOND = 6MMI_Q_TRACKCOND_STEP = 1MMI_M_TRACKCOND_TYPE = 3MMI_Q_TRACKCOND_ACTION_START = 0MMI_Q_TRACKCOND_ACTION_END = 0
            Expected Result: Verify the following information,(1)    The display in sub-area B3-B5 still not change because of all areas are already displaying symbols
            Test Step Comment: (1) MMI_gen 10488 (partly: wait that B3, B4 or B5 is free);
            */
            // Call generic Check Results Method
            DmiExpectedResults
                .Verify_the_following_information_1_The_display_in_sub_area_B3_B5_still_not_change_because_of_all_areas_are_already_displaying_symbols(this);


            /*
            Test Step 9
            Action: Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 0MMI_N_TRACKCONDITIONS = 1MMI_NID_TRACKCOND = 0 MMI_Q_TRACKCOND_STEP = 3MMI_M_TRACKCOND_TYPE = 3MMI_Q_TRACKCOND_ACTION_START = 0MMI_Q_TRACKCOND_ACTION_END = 0
            Expected Result: Verify the following information,(1)   All of symbols TC02, TC01 and TC04 are removed from sub-area B3-B5.(2)   DMI displays symbol TC05 at sub-area B3.(3)   The symbol TC03 is not display because of stored track conditions is deleted
            Test Step Comment: (1) MMI_gen 10469 (partly: MMI_Q_TRACKCOND_UPDATE = 0, delete all stored track conditions);(2) MMI_gen 10469 (partly: use new track conditions received);  (3) MMI_gen 10469 (partly: Delete all stored track conditions);
            */


            /*
            Test Step 10
            Action: Perform SoM until 'Start' button is pressed
            Expected Result: DMI is display only MO10 symbol in sub-area C1
            Test Step Comment: MMI_gen 7085;
            */


            /*
            Test Step 11
            Action: End of test
            Expected Result: 
            */


            return GlobalTestResult;
        }
    }
}
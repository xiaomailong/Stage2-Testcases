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
    /// 11.3 Acknowledgements: Priority of new incoming acknowledgements
    /// TC-ID: 6.3
    /// 
    /// This test case verifies the display of acknowledgements refer to group of priority which specified in [MMI-ETCS-gen]. The maximum of an amount for pending acknowledgements is 10, remove the oldest acknowledgement if the list is overflow.
    /// 
    /// Tested Requirements:
    /// MMI_gen 4484; MMI_gen 4482; MMI_gen 4486; MMI_gen 4498; MMI_gen 4485 (partly: ETCS Onboard); MMI_gen 6923;
    /// 
    /// Scenario:
    /// 1.Use the test script file to send a packet information EVC-
    /// 8.Then, verify the display of acknowledgement on DMI.
    /// 2.Use the test script file to send a packet information EVC-
    /// 8.Then, press an acknowledgement in specify area and verify the display of acknowledgement on DMI.
    /// 
    /// Used files:
    /// 6_3_a.xml, 6_3_b.xml, 6_3_c.xml, 6_3_d.xml
    /// </summary>
    public class Acknowledgements_Priority_of_new_incoming_acknowledgements : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // System is powered onCabin is activatedPerform SoM until level 1 is selected and confirmedMain window is closed.
            
            // Call the TestCaseBase PreExecution
            base.PreExecution();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in SB mode, level 1

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint

            
            /*
            Test Step 1
            Action: Use the test script file 6_3_a.xml to send EVC-8 with,MMI_Q_TEXT = 280MMI_Q_TEXT_CRITERIA = 1MMI_I_TEXT = 1
            Expected Result: DMI displays the text message ‘Emergency stop’ in sub-area E5
            */
            
            
            /*
            Test Step 2
            Action: (Continue from step 1)Send EVC-8 with, MMI_Q_TEXT = 257MMI_Q_TEXT_CRITERIA = 1MMI_I_TEXT = 2MMI_N_TEXT = 1MMI_X_TEXT = 0
            Expected Result: Verify the following information,(1)   The text message in sub-area E5 is disappeared and DMI displays LE07 symbol with yellow flashing frame in sub-area C1 instead
            Test Step Comment: (1) MMI_gen 4484;
            */
            
            
            /*
            Test Step 3
            Action: (Continue from step 2)Send EVC-8 with, MMI_Q_TEXT = 257MMI_Q_TEXT_CRITERIA = 1MMI_I_TEXT = 3MMI_N_TEXT = 1MMI_X_TEXT = 1
            Expected Result: DMI displays LE11 symbol with yellow flashing frame in sub-area C1
            */
            
            
            /*
            Test Step 4
            Action: (Continue from step 3)Send EVC-8 with, MMI_Q_TEXT = 259MMI_Q_TEXT_CRITERIA = 1MMI_I_TEXT = 4
            Expected Result: Verify the following information,(1)   DMI displays MO08 symbol with yellow flashing frame in sub-area C1 instead of LE11 symbol
            Test Step Comment: (1) MMI_gen 4484;
            */
            
            
            /*
            Test Step 5
            Action: (Continue from step 4)Send EVC-8 with, MMI_Q_TEXT = 298MMI_Q_TEXT_CRITERIA = 1MMI_I_TEXT = 5
            Expected Result: Verify the following information,(1)   The symbol in sub-area C1 is disappeared and DMI displays the symbol DR02 in area D instead
            Test Step Comment: (1) MMI_gen 4484;
            */
            
            
            /*
            Test Step 6
            Action: (Continue from step 5)Send EVC-8 with, MMI_Q_TEXT = 260MMI_Q_TEXT_CRITERIA = 1MMI_I_TEXT = 6
            Expected Result: Verify the following information,(1)   The symbol in area D is disappeared and DMI displays the ST01 symbol on sub-area C9 instead
            Test Step Comment: (1) MMI_gen 4484;
            */
            
            
            /*
            Test Step 7
            Action: Use the test script file 6_3_b.xml to send EVC-8 with,MMI_Q_TEXT = 264MMI_Q_TEXT_CRITERIA = 1MMI_I_TEXT = 7
            Expected Result: Verify the following information,(1)   The display information on DMI still not change, ST01 symbol is displayed on sub-area C9
            Test Step Comment: (1) MMI_gen 4484 (partly: NEGATIVE, lower priority, focus not moved);
            */
            
            
            /*
            Test Step 8
            Action: (Continue from step 7)Send EVC-8 with, MMI_Q_TEXT = 269MMI_Q_TEXT_CRITERIA = 1MMI_I_TEXT = 8
            Expected Result: The display information on DMI still not change, ST01 symbol is displayed on sub-area C9
            */
            // Call generic Check Results Method
            DmiExpectedResults.The_display_information_on_DMI_still_not_change_ST01_symbol_is_displayed_on_sub_area_C9();
            
            
            /*
            Test Step 9
            Action: (Continue from step 8)Send EVC-8 with, MMI_Q_TEXT = 268MMI_Q_TEXT_CRITERIA = 1MMI_I_TEXT = 9
            Expected Result: The display information on DMI still not change, ST01 symbol is displayed on sub-area C9
            */
            // Call generic Check Results Method
            DmiExpectedResults.The_display_information_on_DMI_still_not_change_ST01_symbol_is_displayed_on_sub_area_C9();
            
            
            /*
            Test Step 10
            Action: (Continue from step 9)Send EVC-8 with, MMI_Q_TEXT = 267MMI_Q_TEXT_CRITERIA = 1MMI_I_TEXT = 10
            Expected Result: The display information on DMI still not change, ST01 symbol is displayed on sub-area C9
            */
            // Call generic Check Results Method
            DmiExpectedResults.The_display_information_on_DMI_still_not_change_ST01_symbol_is_displayed_on_sub_area_C9();
            
            
            /*
            Test Step 11
            Action: Use the test script file 6_3_c.xml to send EVC-8 with,MMI_Q_TEXT = 554MMI_Q_TEXT_CRITERIA = 1MMI_I_TEXT = 11
            Expected Result: The display information on DMI still not change, ST01 symbol is displayed on sub-area C9
            */
            // Call generic Check Results Method
            DmiExpectedResults.The_display_information_on_DMI_still_not_change_ST01_symbol_is_displayed_on_sub_area_C9();
            
            
            /*
            Test Step 12
            Action: Use the test script file 6_3_d.xml to send EVC-8 with,MMI_Q_TEXT_CRITERIA = 4MMI_I_TEXT = 6Then, press at sub-area C9
            Expected Result: Verify the following information,(1)   The symbol ST01 in sub-area C9 is removed.(2)   Use the log file to confirm that DMI is not send out packet EVC-111.(3)   After 1 second, the symbol DR02 is displayed on area D
            Test Step Comment: (1) MMI_gen 4485 (partly: ETCS Onboard); MMI_gen 4498 (partly: disappear);(2) MMI_gen 4498 (partly: sensitive area is removed);(3) MMI_gen 4498 (partly: reappear, next pending acknowledgement);
            */
            
            
            /*
            Test Step 13
            Action: Confirm an acknowledgement of TAF by pressing at area D
            Expected Result: Verify the following information,(1)    The symbols are removed refer to pressed area. DMI displays the symbol MO08 in sub-area C1. (The oldest entry of the highest priority in the list)
            Test Step Comment: (1) MMI_gen 4486 (partly: mode acknowledgement); MMI_gen 4482 (moveable focus);
            */
            
            
            /*
            Test Step 14
            Action: Press an acknowledgement on sub-area C1
            Expected Result: DMI displays MO17 symbol on sub-area C1
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(@"Press an acknowledgement on sub-area C1");
            
            
            /*
            Test Step 15
            Action: Press an acknowledgement on sub-area C1
            Expected Result: Verify the following information,(1)    DMI displays the symbol LE07 in sub-area C1. (The oldest entry of the highest priority in the list)
            Test Step Comment: (1) MMI_gen 4486 (partly: level acknowledgement); MMI_gen 4482 (partly: moveable focus);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(@"Press an acknowledgement on sub-area C1");
            
            
            /*
            Test Step 16
            Action: Press an acknowledgement on sub-area C1
            Expected Result: Verify the following information,(1)    The symbols is removed refer to pressed area. DMI displays the text message ‘Runaway movement’ (The oldest entry of the highest priority in the list).(2)   The text message ‘Runaway movement’ is displayed instead of ‘Emergency Stop’ from step 1
            Test Step Comment: (1) MMI_gen 4486 (partly: other acknowledgement);(2) MMI_gen 4482 (partly: overflow);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(@"Press an acknowledgement on sub-area C1");
            
            
            /*
            Test Step 17
            Action: Press an acknowledgement on sub-area E5
            Expected Result: Verify the following information,(1)    The text message ‘Runaway movement’ is removed refer to pressed area. DMI displays the text message ‘Communication error’ (The oldest entry of the highest priority in the list)
            Test Step Comment: (1) MMI_gen 4486 (partly: the oldest entry);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(@"Press an acknowledgement on sub-area E5");
            
            
            /*
            Test Step 18
            Action: Press an acknowledgement on sub-area E5
            Expected Result: Verify the following information,(1)    The text message ‘Communication error’ is removed refer to pressed area. DMI displays the text message ‘Balise read error’ (The oldest entry of the highest priority in the list)
            Test Step Comment: (1) MMI_gen 4486 (partly: the oldest entry);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(@"Press an acknowledgement on sub-area E5");
            
            
            /*
            Test Step 19
            Action: Press an acknowledgement on sub-area E5
            Expected Result: Verify the following information,(1)    The text message ‘Balise read error’ is removed refer to pressed area. DMI displays the text message ‘Reactivate the Cabin!’ (The oldest entry of the highest priority in the list)
            Test Step Comment: (1) MMI_gen 4486 (partly: the oldest entry); MMI_gen 4482 (partly: 10 pending acknowledgements);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(@"Press an acknowledgement on sub-area E5");
            
            
            /*
            Test Step 20
            Action: Use the test script file 6_3_a.xml
            Expected Result: See the expected result No.1-6
            */
            
            
            /*
            Test Step 21
            Action: Simulate loss-communication between ETCS and DMI
            Expected Result: DMI displays Default window with the  message “ATP Down Alarm” and sound alarm
            Test Step Comment: MMI_gen 6923 (partly: MMI_gen 244);
            */
            // Call generic Check Results Method
            DmiExpectedResults.DMI_displays_Default_window_with_the_message_ATP_Down_Alarm_and_sound_alarm();
            
            
            /*
            Test Step 22
            Action: Press an acknowledgement on sub-area E5
            Expected Result: All entire acknowledgement lists is flushed, DMI displays text message ‘ATP Down Alarm’ without yellow flashing frame
            Test Step Comment: MMI_gen 6923 (partly: flush the entire acknowledgement list);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(@"Press an acknowledgement on sub-area E5");
            
            
            /*
            Test Step 23
            Action: End of test
            Expected Result: 
            */
            

            return GlobalTestResult;
        }
    }
}

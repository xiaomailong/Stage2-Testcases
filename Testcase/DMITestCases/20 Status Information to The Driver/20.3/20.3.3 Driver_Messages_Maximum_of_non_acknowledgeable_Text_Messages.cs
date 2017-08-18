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
    /// 20.3.3 Driver Messages: Maximum of non-acknowledgeable Text Messages
    /// TC-ID: 15.3.3
    /// 
    /// This test case verify a Driver message list handling for non-acknowledgeable text messages, order of message removing and maximum of message in list.
    /// 
    /// Tested Requirements:
    /// MMI_gen 135 (partly: 50 entries in total); MMI_gen 138 (partly: remove oldest important message, move the visibility window on top of the Message List, remove oldest auxiliary text message); MMI_gen 7048;
    /// 
    /// Scenario:
    /// Use the test script file to send EVC-8 to DMI. Then, verifies the display information.Press ‘Down’ button until it’s disabled. Then, verifies the display information.Use the test script file to send EVC-8 to DMI. Then, verifies the display information.Press ‘Down’ button until it’s disabled. Then, verifies the display information.Use the test script file to send EVC-8 to DMI. Then, verifies the display information.Simulate loss communication between DMI and ETCS.Re-establish communication. Then, verifies the display information.Note: Each step of test script file in executed continuously, Tester need to confim expected result within specific time (5 second).
    /// 
    /// Used files:
    /// 15_3_3_a.xml, 15_3_3_b.xml, 15_3_3_c.xml
    /// </summary>
    public class Driver_Messages_Maximum_of_non_acknowledgeable_Text_Messages : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // Test system is power onPerform SoM until Level 1 is selected and confirmed.Main window is closed.

            // Call the TestCaseBase PreExecution
            base.PreExecution();
            // DMI is power on.Cabin A is activated.SoM is perform until Level 1 is selected and confirmed.Main window is closed.
            DmiActions.Complete_SoM_L1_SB(this);
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in SB mode, level 1
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
            Action: Use the test script file 15_3_3_a.xml to send multiple packets EVC-8 with the following value,Common variablesMMI_Q_TEXT_CRITERIA = 3MMI_Q_TEXT_CLASS = 1The order of MMI_Q_TEXT value in each packetMMI_Q_TEXT = 0MMI_Q_TEXT = 1MMI_Q_TEXT = 267MMI_Q_TEXT = 268MMI_Q_TEXT = 269MMI_Q_TEXT = 274MMI_Q_TEXT = 275MMI_Q_TEXT = 280MMI_Q_TEXT = 290MMI_Q_TEXT = 292MMI_Q_TEXT = 296MMI_Q_TEXT = 299MMI_Q_TEXT = 305MMI_Q_TEXT = 310MMI_Q_TEXT = 315MMI_Q_TEXT = 316MMI_Q_TEXT = 320MMI_Q_TEXT = 321MMI_Q_TEXT = 514MMI_Q_TEXT = 515MMI_Q_TEXT = 516MMI_Q_TEXT = 520MMI_Q_TEXT = 521MMI_Q_TEXT = 524MMI_Q_TEXT = 526MMI_Q_TEXT = 527MMI_Q_TEXT = 531MMI_Q_TEXT = 532MMI_Q_TEXT = 533MMI_Q_TEXT = 536MMI_Q_TEXT = 540MMI_Q_TEXT = 552MMI_Q_TEXT = 554MMI_Q_TEXT = 560MMI_Q_TEXT = 563MMI_Q_TEXT = 572MMI_Q_TEXT = 606MMI_Q_TEXT = 580MMI_Q_TEXT = 581MMI_Q_TEXT = 582MMI_Q_TEXT = 621MMI_Q_TEXT = 622MMI_Q_TEXT = 701MMI_Q_TEXT = 702MMI_Q_TEXT = 703MMI_Q_TEXT = 706MMI_Q_TEXT = 711MMI_Q_TEXT = 712MMI_Q_TEXT = 713MMI_Q_TEXT = 714Note: MMI_I_TEXT is unique
            Expected Result: Verify the following information,(1)   The following text messages are displays on sub-area E5 respectively with sound Sinfo,Level crossing not protected AcknowledgementBalise read errorCommunication errorRunaway movementEntering FSEntering OSEmergency stopSH refusedSH request failedTrackside not compatibleTrain is rejectedTrain dividedTrain data changedSR Distance exceededSR stop orderRV distance exceededETCS IsolatedPerform Brake Test!Unable to start Brake TestBrake Test in ProgressLZB Partial Block ModeOverride LZB Partial Block ModeBrake Test successfulBrake Test TimeoutBrake Test aborted, perform new Test?BTM Test in ProgressBTM Test FailureBTM Test TimeoutRestart ATP!No Level available OnboardAnnounced levels(s) not supported OnboardReactivate the Cabin!Trackside malfunctionTrackside Level(s) not supported OnboardNo Track DescriptionSH Stop OrderProcedure Brake Percentage Entry terminated by ATPProcedure Wheel Diameter Entry terminated by ATPProcedure Doppler Radar Entry terminated by ATPUnable to start Brake Test, vehicle not readyUnblock EBRoute unsuitable – axle load categoryRoute unsuitable – loading gaugeRoute unsuitable – traction systemNo valid authentication keyNL-input signal is withdrawnWheel data settings were successfully changedDoppler radar settings were successfully changedBrake percentage was successfully changedNote: When the new message is added in sub-area E5, an every older message are moved down 1 line
            Test Step Comment: (1) MMI_gen 135 (partly: 50 entries in total, first group); MMI_gen 138 (partly: first group, sound of the first group);
            */
            XML.XML_15_3_3_a.Send(this);

            /*
            Test Step 2
            Action: Use the test script file 15_3_3_b.xml to send EVC-8 with, MMI_Q_TEXT = 715MMI_Q_TEXT_CRITERIA = 3MMI_Q_TEXT_CLASS = 0MMI_I_TEXT = 51
            Expected Result: The text messages in area E5-E9 are not change.Verify the following information,(1)   There is no sound ‘Sinfo’
            Test Step Comment: (1) MMI_gen 138 (partly: NEGATIVE, no sound of the second group);
            */


            /*
            Test Step 3
            Action: Press <Down> button until it is disabled
            Expected Result: Verify the following information,(1)   The text message ‘Level crossing not protected’ is changed to ‘No Country Selection in LZB PB Mode’ with regular style in sub-area E9
            Test Step Comment: (1) MMI_gen 138 (partly: remove oldest important message, new of the second group); MMI_gen 135 (partly: 50 entries in total, first group topmost);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press <Down> button until it is disabled");


            /*
            Test Step 4
            Action: Use the test script file 15_3_3_c.xml to send EVC-8 with, MMI_Q_TEXT = 278MMI_Q_TEXT_CRITERIA = 3MMI_Q_TEXT_CLASS = 0MMI_I_TEXT = 52
            Expected Result: Verify the following points,(1)   The visibility window is not moved.(2)   The text message ‘No Country Selection in LZB PB Mode’ is changed to ‘Emergency Brake Failure’.(3)   There is no sound ‘Sinfo’
            Test Step Comment: (1) MMI_gen 138 (partly: the second group, not necessary moving the visibility window);(2) MMI_gen 138 (partly: remove oldest auxiliary message, new of the second group);(3) MMI_gen 138 (partly: negative, no sound of the second group);
            */


            /*
            Test Step 5
            Action: (Continue from step 4) send EVC-8 with, MMI_Q_TEXT = 273MMI_Q_TEXT_CRITERIA = 3MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 53
            Expected Result: Verify the following points,The visibility window is moved on top of the message list, <Up> button is disabled and DMI display text message “Unauthorized passing of EOA / LOA” in sub-area E5
            Test Step Comment: (1)  MMI_gen 138 (partly: move the visibility window on top of the Message List);
            */


            /*
            Test Step 6
            Action: Press <Down> button until it is disabled
            Expected Result: Verify the following information,(1)  The text message ‘Emergency Brake Failure’ is removed from sub-are E9, DMI displays the text message ‘Acknowledgement’ instead
            Test Step Comment: (1) MMI_gen 138 (partly: remove oldest auxiliary text message);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press <Down> button until it is disabled");


            /*
            Test Step 7
            Action: Simulate loss communication between DMI and ETCS
            Expected Result: DMI displays the message “ATP Down Alarm” with sound alarm.Verify the following information,(1)   The non-acknowledgeable message list is flushed, no driver message display in area E5-E9
            Test Step Comment: (1) MMI_gen 7048 (partly: MMI_gen 240);
            */
            // Call generic Check Results Method
            DmiExpectedResults
                .DMI_displays_the_message_ATP_Down_Alarm_with_sound_alarm_Verify_the_following_information_1_The_non_acknowledgeable_message_list_is_flushed_no_driver_message_display_in_area_E5_E9(this);


            /*
            Test Step 8
            Action: Re-establish communication between DMI and ETCS
            Expected Result: Verify the following information,(1)  If ETCS Onboard re-transmits the driver messages, the messages re-appear
            Test Step Comment: Note under MMI_gen 7048;
            */


            /*
            Test Step 9
            Action: Use the test script file 15_3_3_b.xml to send EVC-8.Then, perform the following procedure, De-activate DMI cabin. Simulate loss communication between DMI and ETCS
            Expected Result: DMI displays the message “ATP Down Alarm” with sound alarm.Verify the following information,(1)   The non-acknowledgeable message list is flushed, no driver message display in area E5-E9
            Test Step Comment: (1) MMI_gen 7048 (partly: MMI_gen 244);
            */
            // Call generic Check Results Method
            DmiExpectedResults
                .DMI_displays_the_message_ATP_Down_Alarm_with_sound_alarm_Verify_the_following_information_1_The_non_acknowledgeable_message_list_is_flushed_no_driver_message_display_in_area_E5_E9(this);


            /*
            Test Step 10
            Action: End of test
            Expected Result: 
            */


            return GlobalTestResult;
        }
    }
}
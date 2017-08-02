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
    /// 21.1 TAF Question Box
    /// TC-ID: 16.1
    /// 
    /// This test case verifies the property and presentation of TAF Question including with an other areas which effected when TAF Question is displayed.
    /// 
    /// Tested Requirements:
    /// MMI_gen 6963; MMI_gen 3966; MMI_gen 4504 (partly: TAF Question Box); MMI_gen 6946; MMI_gen 6948; MMI_gen 6949; MMI_gen 6950; MMI_gen 6951; MMI_gen 6953; MMI_gen 4381; MMI_gen 4382; MMI_gen 968; MMI_gen 9512; arn_043#3843; MMI_gen 3374 (partly: not covered by opened half-grid array window); MMI_gen 3200 (partly: TAF);
    /// 
    /// Scenario:
    /// Perform SoM to SR mode, level 
    /// 2.Then, verify that PA, Scale up button and Scale down button are not displayed .Drive the train forward to receive information from RBC at 70m.Message 3: Packet 15,21,27 and 80 (Entering FS and get OS mode acknowledgement area)Continue to drive the train forward and acknowledge OS mode at position 250m.Open the Main window at the position 300m (before received Track ahead free request).Drive the train forward to receive Track ahead free request from RBC at position 350m.Perform the following procedure to verify that ‘Yes’ button in TAF Question box is up-type,Press and hold button.Slide out button.Slide back into button.Release pressed button.Use the test script file to send EVC-
    /// 8.Then, verify that TAF Question box is displayed only ‘Yes’ button (Ack button).Force the train to display brake intervention symbol (ST01). Then, verify that TAF Question box is removed due to focus of acknowledgement list is changed.
    /// 
    /// Used files:
    /// 16_1.tdg, 16_1.utt, 16_1.xml
    /// </summary>
    public class TAF_Question_Box : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // System is power on.Cabin is activate.

            // Call the TestCaseBase PreExecution
            base.PreExecution();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in OS mode, level 2.

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint


            /*
            Test Step 1
            Action: Perform SoM to SR mode, level 2.Then, drive the train forward with speed = 30km/h
            Expected Result: DMI displays in SR mode, level 2
            */
            // Call generic Action Method
            DmiActions.Perform_SoM_to_SR_mode_level_2_Then_drive_the_train_forward_with_speed_30kmh(this);
            // Call generic Check Results Method
            DmiExpectedResults.DMI_displays_in_SR_mode_level_2(this);


            /*
            Test Step 2
            Action: Received information from RBC
            Expected Result: DMI changes from SR mode to FS mode, level 2
            */
            // Call generic Action Method
            DmiActions.Received_information_from_RBC(this);
            // Call generic Check Results Method
            DmiExpectedResults.DMI_changes_from_SR_mode_to_FS_mode_level_2(this);


            /*
            Test Step 3
            Action: Acknowledge OS mode by press at area C1
            Expected Result: DMI changes from FS mode to OS mode, level 2
            */
            // Call generic Action Method
            DmiActions.Acknowledge_OS_mode_by_press_at_area_C1(this);
            // Call generic Check Results Method
            DmiExpectedResults.DMI_changes_from_FS_mode_to_OS_mode_level_2(this);


            /*
            Test Step 4
            Action: Press ‘Main’ button
            Expected Result: The Main window is displayed
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press ‘Main’ button");
            // Call generic Check Results Method
            DmiExpectedResults.The_Main_window_is_displayed(this);


            /*
            Test Step 5
            Action: Received information from RBC.Then, stop the train
            Expected Result: Verify the following information,DMI displays the TAF Question box in area D and cover a currently open Main window. TAF Question box is divided into a Question part and Answer part.The Question part is placed to the left of the Answer part.The symbol DR02 is displayed in centered of the Question part.The background colour of Question part is Dark-Grey.The label ‘Yes’ is displayed in Answer part with black colour.The background colour of Answer part is Medium-Grey.The Main window is forced to background, The background of area D is blank.The area G is shown the Default window’s contents.The area F is displayed into the foreground with the buttons.Use the log file to confirm that DMI received packet information MMI_DRIVER_MESSAGE (EVC-8) with following variables,MMI_Q_TEXT = 298 (Confirm Track Ahead Free)MMI_Q_TEXT_CRITERIA = 1 (Removed after Ack)The border of Question part and Answer Part are drawn with medium grey colour in the following positions,LeftTopRightBottom
            Test Step Comment: (1) MMI_gen 3966 (partly:Placed in Main-Area D);              MMI_gen 6963 (partly: Placed in Main-Area D, in the focus of acknowledgement list); MMI_gen 3374 (partly: TAF, button is visible);(2) MMI_gen 6946; MMI_gen 3374 (partly: button is not faulty);(3 ) MMI_gen 6948;(4) MMI_gen 6949 (partly: DR02);(5) MMI_gen 6949 (partly: Background colour of Question part);(6) MMI_gen 6950 (partly: Label ‘Yes’);(7) MMI_gen 6950 (partly: Background colour of Answer part);(8) MMI_gen 3966 (partly: 1st bullet); MMI_gen 3374 (partly: not covered by open half-grid array window);(9) MMI_gen 3966 (partly: 2nd bullet);(10) MMI_gen 3966 (partly: 3rd  bullet);(11) MMI_gen 6963 (partly: 1st bullet); MMI_gen 6951 (partly: Enabled Yes button by EVC-8); MMI_gen 3374 (partly: enabled by ETCS);(12) MMI_gen 6953 (partly: same border, MMI_gen 4211 (partly: medium grey colour line));
            */
            // Call generic Action Method
            DmiActions.Received_information_from_RBC_Then_stop_the_train(this);


            /*
            Test Step 6
            Action: Press at Question part of Track Ahead Free
            Expected Result: Verify the following information,Use the log file to confirm that DMI did not send out packet information MMI_DRIVER_MESSAGE_ACK (EVC-111)
            Test Step Comment: (1) MMI_gen 6951 (partly: NEGATIVE, sensitive area);
            */


            /*
            Test Step 7
            Action: Press and hold Answer part of Track Ahead Free.(‘Yes’ button)
            Expected Result: Verify the following information,(1)   ‘The ‘Yes’ button is shown as pressed state. (2)   The sound ‘Click’ is played once.(3)   Use the log file to confirm that DMI send out packet information MMI_DRIVER_MESSAGE_ACK (EVC-111) with the following variables,MMI_Q_ACK = 1MMI_Q_BUTTON = 1MMI_T_BUTTONEVENT  is not blank
            Test Step Comment: (1) MMI_gen 6951 (partly: Up-Type, enabled according packet EVC-8);                                MMI_gen 4381 (partly: change to state ‘Pressed’ as long as remain actuated)(2) MMI_gen 4381 (partly: the sound for Up-Type button); MMI_gen 9512; MMI_gen 968 (partly: use the ‘click’ sound);(3) MMI_gen 6951 (partly: MMI_gen 11387 (partly: send events of Pressed independently to ETCS), MMI_gen 11907 (partly: EVC-111, timestamp))); arn_043#3843; MMI_gen 3200 (partly: TAF, pressed);
            */


            /*
            Test Step 8
            Action: Slide out ‘Yes’ button
            Expected Result: Verify the following information,The ‘Yes’ button becomes the ‘Enabled’ state without a sound
            Test Step Comment: (1) MMI_gen 6951 (partly: Up-Type);              MMI_gen 4382 (partly: state ‘Enabled’ when slide out with force applied, no sound);                       
            */


            /*
            Test Step 9
            Action: Slide back into ‘Yes’ button
            Expected Result: Verify the following information,(1)   The ‘Yes’ button turns to the ‘Pressed’ state without a sound
            Test Step Comment: (1) MMI_gen 6951 (partly: Up-Type);              MMI_gen 4382 (partly: state ‘Pressed’ when slide back, no sound))); 
            */


            /*
            Test Step 10
            Action: Release ‘Yes’ button
            Expected Result: Verify the following information,TAF Question box is removed from area D.Use the log file to confirm that DMI send out packet information MMI_DRIVER_MESSAGE_ACK (EVC-111) with the following variables,MMI_Q_ACK = 1MMI_Q_BUTTON = 0MMI_T_BUTTONEVENT  is not blank
            Test Step Comment: (1) MMI_gen 6951 (sensitive area);(2) MMI_gen 6951 (partly: Safe-Up-Type, MMI_gen 11387 (partly: send events of Pressed independently to ETCS),  MMI_gen 11907 (partly: EVC-111, timestamp))); MMI_gen 4381 (partly: exit state ‘Pressed’, execute function associated to the button); arn_043#3843; MMI_gen 3200 (partly: TAF, released);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Release ‘Yes’ button");


            /*
            Test Step 11
            Action: Use the test script file 16_1.xml to send EVC-8 with,MMI_Q_TEXT_CRITERIA = 2MMI_Q_TEXT = 298
            Expected Result: Verify the following information,DMI displays TAF Question box which contain only ‘Yes’ button
            Test Step Comment: (1) MMI_gen 4504 (partly: TAF Question Box);
            */


            /*
            Test Step 12
            Action: Perform the following procedure,Set the train direction to ‘Neutral’Drive the train forward
            Expected Result: Verify the following information,TAF Question box is removed after ST01 symbol is displayed in sub-area C9
            Test Step Comment: (1) MMI_gen 6963 (partly: NEGATIVE, 2nd bullet);
            */


            /*
            Test Step 13
            Action: End of test
            Expected Result: 
            */


            return GlobalTestResult;
        }
    }
}
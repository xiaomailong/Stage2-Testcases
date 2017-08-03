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
    /// 22.3 Planning Area: PA Distance Scale
    /// TC-ID: 17.3
    /// 
    /// This test case verifies the presentation of PA distance scale which displays distance range scale on the Planning Area. The unit of scale shall display in Meter units. The presentation of PA distance scale in sub-areas D1-D7 shall comply with [ERA-ERTMS] standard and [MMI-ETCS-gen].
    /// 
    /// Tested Requirements:
    /// MMI_gen 7110 (Meter); MMI_gen 9938 (partly: MMI_gen 7116; (partly: If not specified, range [0..4000] is default); MMI_gen 7213 (partly: Scale number colour); MMI_gen 7212 (partly: 9 distance scale lines), functions displayed in D2-D8); MMI_gen 7147; MMI_gen 7148 (partly:[0..4000]);                     
    /// 
    /// Scenario:
    /// Activate Cabin A and Perform SoM to SR mode, level 1.Drive the train forward pass BG1 at 100m. Note: BG1: packet 12, 21 and 27Stop the train. Then, verifies the display information.Press ‘Scale Up’ button. Then, verify that the PA distance scale is updated according to the selected range.Press ‘Scale Down’ button. Then, verify that the PA distance scale is updated according to the selected range.Simulate the communication loss between DMI and ETCS Onboard.Re-establish connection between DMI and ETCS Onboard. Then, verify that the PA distance scale range is not changed.Power OFF system.Power ON system and repeat action to drive train forward pass BG1 again. Then, verify that the PA distance scale is the default range.
    /// 
    /// Used files:
    /// 17_3.tdg
    /// </summary>
    public class Planning_Area_PA_Distance_Scale : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // Set the following tags name in configuration file (See the instruction in Appendix 1)SPEED_UNIT_TYPE = 0 (Metric, km/h)System is power on.

            // Call the TestCaseBase PreExecution
            base.PreExecution();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in FS mode, level 1.

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint


            /*
            Test Step 1
            Action: Activate cabin A. Then, perform SoM in SR mode, Level 1
            Expected Result: DMI displays in SR mode, level 1
            */
            // Call generic Check Results Method
            DmiExpectedResults.SR_Mode_displayed(this);


            /*
            Test Step 2
            Action: Drive the train forward passing BG1
            Expected Result: DMI changes from SR mode to FS mode
            */
            // Call generic Action Method
            DmiActions.Drive_train_forward_passing_BG1(this);
            // Call generic Check Results Method
            DmiExpectedResults.DMI_changes_from_SR_to_FS_mode(this);


            /*
            Test Step 3
            Action: Stop the train
            Expected Result: The Planning Area is displayed in area DVerify the following points,The scale numbers are displayed in Medium-Grey colour and vertically centred on the distance scale lines.It is presented aligned to the right of Sub-Area D1.There are 9 distance scale lines displayed crossing sub-areas D2 – D7.From the bottom to top each line of distance scale are displayed with following colour,1st, 6th and 9th distance scale line are Medium-GreyOther distance scale line are Dark-greyVerify that the DMI displays the distance ranges from 0 to 4000 meter as default value.The following PA distance scale number are displayed in meter (see comment).0500100020004000At the following PA distance scale number, there are distance scale line displayed.0100200300400500100020004000Note: Need a self calculation for distance scale number in some location which not have distance scale number specify.The position of PASP is consisted with train position refer to PA distance scale number and distance scale line
            Test Step Comment: (1) MMI_gen 9938 (partly: MMI_gen 7213 (partly: Scale number colour));  (2) MMI_gen 9938 (partly: MMI_gen 7212 (partly: 9 distance scale lines));  (3) MMI_gen 9938 (partly: MMI_gen 7213 (partly: Distance scale lines colour)); (4) MMI_gen 9938 (partly: MMI_gen 7148        (partly: If not specified, range [0..4000] is default)); (5) MMI_gen 9938 (partly: MMI_gen 7116 (partly: [0..4000], Displayed Numbers in Units));(6) MMI_gen 9938 (partly: MMI_gen 7116 (partly: [0..4000], Displayed Distance Scale Lines));(7) MMI_gen 9938 (partly: functions displayed in D2-D8);          Note: MMI_gen 7212 and MMI_gen 7213 shall also verify by Code Review in Chapter 39.
            */
            // Call generic Action Method
            DmiActions.Stop_the_train(this);


            /*
            Test Step 4
            Action: Press ‘Scale up’ button
            Expected Result: Verify that the DMI displays the distance ranges from 0 to 2000.The scale numbers are displayed in Medium-Grey colour and vertically centred on the distance scale lines.It is presented aligned to the right of Sub-Area D1.There are 9 distance scale lines displayed crossing sub-areas D2 – D7.From the bottom to top each line of distance scale are displayed with following colour,1st, 6th and 9th distance scale line are Medium-GreyOther distance scale line are Dark-greyThe following PA distance scale number are displayed in meter (see comment).025050010002000At the following PA distance scale number, there are distance scale line displayed.05010015020025050010002000Note: Need a self calculation for distance scale number in some location which not have distance scale number specify.The position of PASP is consisted with train position refer to PA distance scale number and distance scale line
            Test Step Comment: (1) MMI_gen 9938 (partly: MMI_gen 7213 (partly: Scale number colour)); (2) MMI_gen 9938 (partly: MMI_gen 7212      (partly: 9 distance scale lines));(3) MMI_gen 9938 (partly: MMI_gen 7213   (partly: Distance scale lines colour)); (4) MMI_gen 9938 (partly: MMI_gen 7116 (partly: [0..2000], Displayed Numbers in Units));(5) MMI_gen 9938 (partly: MMI_gen 7116 (partly: [0..2000], Displayed Distance Scale Lines));(6) MMI_gen 9938 (partly: functions displayed in D2-D8);         Note: MMI_gen 7212 and MMI_gen 7213 shall also verify by Code Review in Chapter 39. 
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press ‘Scale up’ button");


            /*
            Test Step 5
            Action: Press ‘Scale up’ button
            Expected Result: Verify that the DMI displays the distance ranges from 0 to 1000.The scale numbers are displayed in Medium-Grey colour and vertically centred on the distance scale lines.It is presented aligned to the right of Sub-Area D1.There are 9 distance scale lines displayed crossing sub-areas D2 – D7.From the bottom to top each line of distance scale are displayed with following colour,1st, 6th and 9th distance scale line are Medium-GreyOther distance scale line are Dark-greyThe following PA distance scale number are displayed in meter (see comment).01252505001000At the following PA distance scale number, there are distance scale line displayed.02550751001252505001000Note: Need a self calculation for distance scale number in some location which not have distance scale number specify.The position of PASP is consisted with train position refer to PA distance scale number and distance scale line
            Test Step Comment: (1) MMI_gen 9938 (partly: MMI_gen 7213 (partly: Scale number colour));(2) MMI_gen 9938 (partly: MMI_gen 7212      (partly: 9 distance scale lines)); (3) MMI_gen 9938 (partly: MMI_gen 7213   (partly: Distance scale lines colour));(4) MMI_gen 9938 (partly: MMI_gen 7116 (partly: [0..1000], Displayed Numbers in Units));(5) MMI_gen 9938 (partly: MMI_gen 7116 (partly: [0..1000], Displayed Distance Scale Lines));(6) MMI_gen 9938 (partly: functions displayed in D2-D8);          Note: MMI_gen 7212 and MMI_gen 7213 shall also verify by Code Review in Chapter 39. 
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press ‘Scale up’ button");


            /*
            Test Step 6
            Action: Press ‘Scale down’ button three times
            Expected Result: Verify that the DMI displays the distance ranges from 0 to 8000.The scale numbers are displayed in Medium-Grey colour and vertically centred on the distance scale lines.It is presented aligned to the right of Sub-Area D1.There are 9 distance scale lines displayed crossing sub-areas D2 – D7.From the bottom to top each line of distance scale are displayed with following colour,1st, 6th and 9th distance scale line are Medium-GreyOther distance scale line are Dark-greyThe following PA distance scale number are displayed in meter (see comment).01000200040008000At the following PA distance scale number, there are distance scale line displayed.02004006008001000200040008000Note: Need a self calculation for distance scale number in some location which not have distance scale number specify.The position of PASP is consisted with train position refer to PA distance scale number and distance scale line.Use the log file to confirm that the movement authority is calculated from the received packet information EVC-7 and EVC-4 as follows,(EVC-4) MMI_O_MRSP[0] - (EVC-7) OBU_TR_O_TRAINThe result of calculation is displayed in Meter unit.Example: The observation point of the movement authority is 407. [EVC-4.MMI_O_MRSP[0]= 1000080700] – [EVC-7.OBU_TR_O_TRAIN = 1000040036] = 40664 (406.64 m),
            Test Step Comment: (1) MMI_gen 9938 (partly: MMI_gen 7213 (partly: Scale number colour));(2) MMI_gen 9938 (partly: MMI_gen 7212      (partly: 9 distance scale lines));(3) MMI_gen 9938 (partly: MMI_gen 7213   (partly: Distance scale lines colour));(4) MMI_gen 9938 (partly: MMI_gen 7116 (partly: [0..8000], Displayed Numbers in Units));(5) MMI_gen 9938 (partly: MMI_gen 7116 (partly: [0..8000], Displayed Distance Scale Lines));(6) MMI_gen 9938 (partly: functions displayed in D2-D8);         (7) MMI_gen 7110 (partly: Meter); Note: MMI_gen 7212 and MMI_gen 7213 shall also verify by Code Review in Chapter 39. 
            */


            /*
            Test Step 7
            Action: Press ‘Scale down’ button
            Expected Result: Verify that the DMI displays the distance ranges from 0 to 16000.The scale numbers are displayed in Medium-Grey colour and vertically centred on the distance scale lines.It is presented aligned to the right of Sub-Area D1.There are 9 distance scale lines displayed crossing sub-areas D2 – D7.From the bottom to top each line of distance scale are displayed with following colour,1st, 6th and 9th distance scale line are Medium-GreyOther distance scale line are Dark-greyThe following PA distance scale number are displayed in meter (see comment).020004000800016000At the following PA distance scale number, there are distance scale line displayed.04008001200160020004000800016000Note: Need a self calculation for distance scale number in some location which not have distance scale number specify.The position of PASP is consisted with train position refer to PA distance scale number and distance scale line
            Test Step Comment: (1) MMI_gen 9938 (partly: MMI_gen 7213 (partly: Scale number colour)); (2) MMI_gen 9938 (partly: MMI_gen 7212      (partly: 9 distance scale lines));(3) MMI_gen 9938 (partly: MMI_gen 7213   (partly: Distance scale lines colour));(4) MMI_gen 9938 (partly: MMI_gen 7116 (partly: [0..16000], Displayed Numbers in Units));(5) MMI_gen 9938 (partly: MMI_gen 7116 (partly: [0..16000], Displayed Distance Scale Lines));(6) MMI_gen 9938 (partly: functions displayed in D2-D8);         Note: MMI_gen 7212 and MMI_gen 7213 shall also verify by Code Review in Chapter 39. 
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press ‘Scale down’ button");


            /*
            Test Step 8
            Action: Press ‘Scale down’ button
            Expected Result: Verify that the DMI displays the distance ranges from 0 to 32000.The scale numbers are displayed in Medium-Grey colour and vertically centred on the distance scale lines.It is presented aligned to the right of Sub-Area D1.There are 9 distance scale lines displayed crossing sub-areas D2 – D7.From the bottom to top each line of distance scale are displayed with following colour,1st, 6th and 9th distance scale line are Medium-GreyOther distance scale line are Dark-greyThe following PA distance scale number are displayed in meter (see comment).0400080001600032000At the following PA distance scale number, there are distance scale line displayed.0800160024003200400080001600032000Note: Need a self calculation for distance scale number in some location which not have distance scale number specify.The position of PASP is consisted with train position refer to PA distance scale number and distance scale line
            Test Step Comment: (1) MMI_gen 9938 (partly: MMI_gen 7213 (partly: Scale number colour));(2) MMI_gen 9938 (partly: MMI_gen 7212      (partly: 9 distance scale lines));(3) MMI_gen 9938 (partly: MMI_gen 7213   (partly: Distance scale lines colour));(4) MMI_gen 9938 (partly: MMI_gen 7116 (partly: [0..32000], Displayed Numbers in Units));(5) MMI_gen 9938 (partly: MMI_gen 7116 (partly: [0..32000], Displayed Distance Scale Lines));(6) MMI_gen 9938 (partly: functions displayed in D2-D8);      Note: MMI_gen 7212 and MMI_gen 7213 shall also verify by Code Review in Chapter 39. 
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press ‘Scale down’ button");


            /*
            Test Step 9
            Action: Simulate the communication loss between ETCS Onboard and DMI
            Expected Result: DMI displays the message “ATP Down Alarm” with sound.The PA is removed from DMI
            */
            // Call generic Action Method
            DmiActions.Simulate_the_communication_loss_between_ETCS_Onboard_and_DMI(this);


            /*
            Test Step 10
            Action: Re-establish the communication between ETCS onboard and DMI
            Expected Result: Verify that PA distance scale is not changed the selected range of PA distance scale, still display range as [0..32000]
            Test Step Comment: MMI_gen 7147 (partly: Communication loss);
            */
            // Call generic Action Method
            DmiActions.Re_establish_the_communication_between_ETCS_onboard_and_DMI(this);


            /*
            Test Step 11
            Action: Power OFF system.Then, power ON system and repeat action step 1-3
            Expected Result: Verify that the DMI displays the distance ranges from 0 to 4000 meter as default value
            Test Step Comment: MMI_gen 7148 (partly: Default applies after power loss);
            */


            /*
            Test Step 12
            Action: End of test
            Expected Result: 
            */


            return GlobalTestResult;
        }
    }
}
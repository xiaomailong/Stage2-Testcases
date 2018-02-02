using System;
using System.Collections.Generic;
using Testcase.Telegrams.EVCtoDMI;


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
    public class TC_ID_22_3_Planning_Area_PA_Distance_Scale : TestcaseBase
    {
        public override bool TestcaseEntryPoint()
        {
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 0;
            // Testcase entrypoint


            MakeTestStepHeader(1, UniqueIdentifier++, "Activate cabin A. Then, perform SoM in SR mode, Level 1",
                "DMI displays in SR mode, level 1");
            /*
            Test Step 1
            Action: Activate cabin A. Then, perform SoM in SR mode, Level 1
            Expected Result: DMI displays in SR mode, level 1
            */
            // Call generic Check Results Method
            DmiActions.Complete_SoM_L1_SR(this);

            DmiExpectedResults.SR_Mode_displayed(this);

            MakeTestStepHeader(2, UniqueIdentifier++, "Drive the train forward passing BG1",
                "DMI changes from SR mode to FS mode");
            /*
            Test Step 2
            Action: Drive the train forward passing BG1
            Expected Result: DMI changes from SR mode to FS mode
            */
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.FullSupervision;

            DmiExpectedResults.FS_mode_displayed(this);

            MakeTestStepHeader(3, UniqueIdentifier++, "Stop the train",
                "The Planning Area is displayed in area DVerify the following points,The scale numbers are displayed in Medium-Grey colour and vertically centred on the distance scale lines.It is presented aligned to the right of Sub-Area D1.There are 9 distance scale lines displayed crossing sub-areas D2 – D7.From the bottom to top each line of distance scale are displayed with following colour,1st, 6th and 9th distance scale line are Medium-GreyOther distance scale line are Dark-greyVerify that the DMI displays the distance ranges from 0 to 4000 meter as default value.The following PA distance scale number are displayed in meter (see comment).0500100020004000At the following PA distance scale number, there are distance scale line displayed.0100200300400500100020004000Note: Need a self calculation for distance scale number in some location which not have distance scale number specify.The position of PASP is consisted with train position refer to PA distance scale number and distance scale line");
            /*
            Test Step 3
            Action: Stop the train
            Expected Result: The Planning Area is displayed in area DVerify the following points,The scale numbers are displayed in Medium-Grey colour and vertically centred on the distance scale lines.It is presented aligned to the right of Sub-Area D1.There are 9 distance scale lines displayed crossing sub-areas D2 – D7.From the bottom to top each line of distance scale are displayed with following colour,1st, 6th and 9th distance scale line are Medium-GreyOther distance scale line are Dark-greyVerify that the DMI displays the distance ranges from 0 to 4000 meter as default value.The following PA distance scale number are displayed in meter (see comment).0500100020004000At the following PA distance scale number, there are distance scale line displayed.0100200300400500100020004000Note: Need a self calculation for distance scale number in some location which not have distance scale number specify.The position of PASP is consisted with train position refer to PA distance scale number and distance scale line
            Test Step Comment: (1) MMI_gen 9938 (partly: MMI_gen 7213 (partly: Scale number colour));  (2) MMI_gen 9938 (partly: MMI_gen 7212 (partly: 9 distance scale lines));  (3) MMI_gen 9938 (partly: MMI_gen 7213 (partly: Distance scale lines colour)); (4) MMI_gen 9938 (partly: MMI_gen 7148        (partly: If not specified, range [0..4000] is default)); (5) MMI_gen 9938 (partly: MMI_gen 7116 (partly: [0..4000], Displayed Numbers in Units));(6) MMI_gen 9938 (partly: MMI_gen 7116 (partly: [0..4000], Displayed Distance Scale Lines));(7) MMI_gen 9938 (partly: functions displayed in D2-D8);          Note: MMI_gen 7212 and MMI_gen 7213 shall also verify by Code Review in Chapter 39.
            */
            // Call generic Action Method
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 0;

            // Need to set track descriptions for scale to display correctly
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN = 20000; // 200m

            List<TrackDescription> descriptionsList = new List<TrackDescription>()
            {
                new TrackDescription
                {
                    MMI_G_GRADIENT = 10,
                    MMI_O_GRADIENT = 10000,
                    MMI_O_MRSP = 10500,
                    MMI_V_MRSP = 800
                },
                new TrackDescription
                {
                    MMI_G_GRADIENT = 15,
                    MMI_O_GRADIENT = 20000,
                    MMI_O_MRSP = 15000,
                    MMI_V_MRSP = 700
                },
                new TrackDescription {MMI_G_GRADIENT = 20, MMI_O_GRADIENT = 30000, MMI_O_MRSP = 20500, MMI_V_MRSP = 600}
            };

            EVC4_MMITrackDescription.TrackDescriptions = descriptionsList;
            EVC4_MMITrackDescription.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Planning Area in area D" + Environment.NewLine +
                                "2. Scale numbers are displayed in Medium-grey, vertically centred on the distance scale lines and aligned to the right of sub-area D1." +
                                Environment.NewLine +
                                "3. 9 distance scale lines are displayed crossing sub-areas D2 – D7." +
                                Environment.NewLine +
                                "4. From the bottom to top the 1st, 6th and 9th distance scale lines are in Medium-grey" +
                                Environment.NewLine +
                                "5. All other distance scale lines are in Dark-grey." + Environment.NewLine +
                                "6. DMI displays the distance range 0 to 4000 m by default." + Environment.NewLine +
                                "7. The PA distance scale values 0, 500, 1000, 2000 and 4000 are displayed in m" +
                                Environment.NewLine +
                                "8. Distance scale lines are displayed at PA distance scale values 0, 100, 200, 300, 400, 500, 1000, 2000, 4000" +
                                Environment.NewLine +
                                "9. The position of PASP is consistent with the train position, PA distance scale value and distance scale lines.");

            MakeTestStepHeader(4, UniqueIdentifier++, "Press ‘Scale up’ button",
                "Verify that the DMI displays the distance ranges from 0 to 2000.The scale numbers are displayed in Medium-Grey colour and vertically centred on the distance scale lines.It is presented aligned to the right of Sub-Area D1.There are 9 distance scale lines displayed crossing sub-areas D2 – D7.From the bottom to top each line of distance scale are displayed with following colour,1st, 6th and 9th distance scale line are Medium-GreyOther distance scale line are Dark-greyThe following PA distance scale number are displayed in meter (see comment).025050010002000At the following PA distance scale number, there are distance scale line displayed.05010015020025050010002000Note: Need a self calculation for distance scale number in some location which not have distance scale number specify.The position of PASP is consisted with train position refer to PA distance scale number and distance scale line");
            /*
            Test Step 4
            Action: Press ‘Scale up’ button
            Expected Result: Verify that the DMI displays the distance ranges from 0 to 2000.The scale numbers are displayed in Medium-Grey colour and vertically centred on the distance scale lines.It is presented aligned to the right of Sub-Area D1.There are 9 distance scale lines displayed crossing sub-areas D2 – D7.From the bottom to top each line of distance scale are displayed with following colour,1st, 6th and 9th distance scale line are Medium-GreyOther distance scale line are Dark-greyThe following PA distance scale number are displayed in meter (see comment).025050010002000At the following PA distance scale number, there are distance scale line displayed.05010015020025050010002000Note: Need a self calculation for distance scale number in some location which not have distance scale number specify.The position of PASP is consisted with train position refer to PA distance scale number and distance scale line
            Test Step Comment: (1) MMI_gen 9938 (partly: MMI_gen 7213 (partly: Scale number colour)); (2) MMI_gen 9938 (partly: MMI_gen 7212      (partly: 9 distance scale lines));(3) MMI_gen 9938 (partly: MMI_gen 7213   (partly: Distance scale lines colour)); (4) MMI_gen 9938 (partly: MMI_gen 7116 (partly: [0..2000], Displayed Numbers in Units));(5) MMI_gen 9938 (partly: MMI_gen 7116 (partly: [0..2000], Displayed Distance Scale Lines));(6) MMI_gen 9938 (partly: functions displayed in D2-D8);         Note: MMI_gen 7212 and MMI_gen 7213 shall also verify by Code Review in Chapter 39. 
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press the ‘Scale up’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Verify that the DMI displays the distance ranges from 0 to 2000." +
                                Environment.NewLine +
                                "2. Scale numbers are in Medium-grey, vertically centred on the distance scale lines and aligned to the right of sub-area D1." +
                                Environment.NewLine +
                                "3. 9 distance scale lines are displayed crossing sub-areas D2 – D7." +
                                Environment.NewLine +
                                "4. From the bottom to top the 1st, 6th and 9th distance scale lines are in Medium-grey." +
                                Environment.NewLine +
                                "5. All other distance scale lines are in Dark-grey" + Environment.NewLine +
                                "6. The PA distance scale values 0, 250, 500, 1000, 2000 are displayed in m." +
                                Environment.NewLine +
                                "7. Distance scale lines are displayed at PA distance scale values 0, 50, 100, 150, 200, 250, 500, 1000, 2000." +
                                Environment.NewLine +
                                "8. The position of PASP is consistent with the train position, PA distance scale value and distance scale lines.");

            MakeTestStepHeader(5, UniqueIdentifier++, "Press ‘Scale up’ button",
                "Verify that the DMI displays the distance ranges from 0 to 1000.The scale numbers are displayed in Medium-Grey colour and vertically centred on the distance scale lines.It is presented aligned to the right of Sub-Area D1.There are 9 distance scale lines displayed crossing sub-areas D2 – D7.From the bottom to top each line of distance scale are displayed with following colour,1st, 6th and 9th distance scale line are Medium-GreyOther distance scale line are Dark-greyThe following PA distance scale number are displayed in meter (see comment).01252505001000At the following PA distance scale number, there are distance scale line displayed.02550751001252505001000Note: Need a self calculation for distance scale number in some location which not have distance scale number specify.The position of PASP is consisted with train position refer to PA distance scale number and distance scale line");
            /*
            Test Step 5
            Action: Press ‘Scale up’ button
            Expected Result: Verify that the DMI displays the distance ranges from 0 to 1000.The scale numbers are displayed in Medium-Grey colour and vertically centred on the distance scale lines.It is presented aligned to the right of Sub-Area D1.There are 9 distance scale lines displayed crossing sub-areas D2 – D7.From the bottom to top each line of distance scale are displayed with following colour,1st, 6th and 9th distance scale line are Medium-GreyOther distance scale line are Dark-greyThe following PA distance scale number are displayed in meter (see comment).01252505001000At the following PA distance scale number, there are distance scale line displayed.02550751001252505001000Note: Need a self calculation for distance scale number in some location which not have distance scale number specify.The position of PASP is consisted with train position refer to PA distance scale number and distance scale line
            Test Step Comment: (1) MMI_gen 9938 (partly: MMI_gen 7213 (partly: Scale number colour));(2) MMI_gen 9938 (partly: MMI_gen 7212      (partly: 9 distance scale lines)); (3) MMI_gen 9938 (partly: MMI_gen 7213   (partly: Distance scale lines colour));(4) MMI_gen 9938 (partly: MMI_gen 7116 (partly: [0..1000], Displayed Numbers in Units));(5) MMI_gen 9938 (partly: MMI_gen 7116 (partly: [0..1000], Displayed Distance Scale Lines));(6) MMI_gen 9938 (partly: functions displayed in D2-D8);          Note: MMI_gen 7212 and MMI_gen 7213 shall also verify by Code Review in Chapter 39. 
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press the ‘Scale up’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Verify that the DMI displays the distance ranges from 0 to 1000." +
                                Environment.NewLine +
                                "2. Scale numbers are in Medium-grey, vertically centred on the distance scale lines and aligned to the right of sub-area D1." +
                                Environment.NewLine +
                                "3. 9 distance scale lines are displayed crossing sub-areas D2 – D7." +
                                Environment.NewLine +
                                "4. From the bottom to top the 1st, 6th and 9th distance scale lines are in Medium-grey" +
                                Environment.NewLine +
                                "5. All other distance scale lines are in Dark-grey" + Environment.NewLine +
                                "6. The PA distance scale values 0, 125, 250, 500, 1000 are displayed in m." +
                                Environment.NewLine +
                                "7. Distance scale lines are displayed at PA distance scale values 0, 25, 50, 75, 100, 125, 250, 500, 1000." +
                                Environment.NewLine +
                                "8. The position of PASP is consistent with the train position, PA distance scale value and distance scale lines.");

            MakeTestStepHeader(6, UniqueIdentifier++, "Press ‘Scale down’ button three times",
                "Verify that the DMI displays the distance ranges from 0 to 8000.The scale numbers are displayed in Medium-Grey colour and vertically centred on the distance scale lines.It is presented aligned to the right of Sub-Area D1.There are 9 distance scale lines displayed crossing sub-areas D2 – D7.From the bottom to top each line of distance scale are displayed with following colour,1st, 6th and 9th distance scale line are Medium-GreyOther distance scale line are Dark-greyThe following PA distance scale number are displayed in meter (see comment).01000200040008000At the following PA distance scale number, there are distance scale line displayed.02004006008001000200040008000Note: Need a self calculation for distance scale number in some location which not have distance scale number specify.The position of PASP is consisted with train position refer to PA distance scale number and distance scale line.Use the log file to confirm that the movement authority is calculated from the received packet information EVC-7 and EVC-4 as follows,(EVC-4) MMI_O_MRSP[0] - (EVC-7) OBU_TR_O_TRAINThe result of calculation is displayed in Meter unit.Example: The observation point of the movement authority is 407. [EVC-4.MMI_O_MRSP[0]= 1000080700] – [EVC-7.OBU_TR_O_TRAIN = 1000040036] = 40664 (406.64 m),");
            /*
            Test Step 6
            Action: Press ‘Scale down’ button three times
            Expected Result: Verify that the DMI displays the distance ranges from 0 to 8000.The scale numbers are displayed in Medium-Grey colour and vertically centred on the distance scale lines.It is presented aligned to the right of Sub-Area D1.There are 9 distance scale lines displayed crossing sub-areas D2 – D7.From the bottom to top each line of distance scale are displayed with following colour,1st, 6th and 9th distance scale line are Medium-GreyOther distance scale line are Dark-greyThe following PA distance scale number are displayed in meter (see comment).01000200040008000At the following PA distance scale number, there are distance scale line displayed.02004006008001000200040008000Note: Need a self calculation for distance scale number in some location which not have distance scale number specify.The position of PASP is consisted with train position refer to PA distance scale number and distance scale line.Use the log file to confirm that the movement authority is calculated from the received packet information EVC-7 and EVC-4 as follows,(EVC-4) MMI_O_MRSP[0] - (EVC-7) OBU_TR_O_TRAINThe result of calculation is displayed in Meter unit.Example: The observation point of the movement authority is 407. [EVC-4.MMI_O_MRSP[0]= 1000080700] – [EVC-7.OBU_TR_O_TRAIN = 1000040036] = 40664 (406.64 m),
            Test Step Comment: (1) MMI_gen 9938 (partly: MMI_gen 7213 (partly: Scale number colour));(2) MMI_gen 9938 (partly: MMI_gen 7212      (partly: 9 distance scale lines));(3) MMI_gen 9938 (partly: MMI_gen 7213   (partly: Distance scale lines colour));(4) MMI_gen 9938 (partly: MMI_gen 7116 (partly: [0..8000], Displayed Numbers in Units));(5) MMI_gen 9938 (partly: MMI_gen 7116 (partly: [0..8000], Displayed Distance Scale Lines));(6) MMI_gen 9938 (partly: functions displayed in D2-D8);         (7) MMI_gen 7110 (partly: Meter); Note: MMI_gen 7212 and MMI_gen 7213 shall also verify by Code Review in Chapter 39. 
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Scale down’ button three times");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Verify that the DMI displays the distance ranges from 0 to 8000." +
                                Environment.NewLine +
                                "2. Scale numbers are in Medium-grey, vertically centred on the distance scale lines and aligned to the right of sub-area D1." +
                                Environment.NewLine +
                                "3. 9 distance scale lines are displayed crossing sub-areas D2 – D7." +
                                Environment.NewLine +
                                "4. From the bottom to top the 1st, 6th and 9th distance scale lines are in Medium-grey" +
                                Environment.NewLine +
                                "5. All other distance scale lines are in Dark-grey" + Environment.NewLine +
                                "6. The PA distance scale values 0, 1000, 2000, 4000, 8000 are displayed in m." +
                                Environment.NewLine +
                                "7. Distance scale lines are displayed at PA distance scale values 0, 200, 400, 600, 800, 1000, 2000, 4000, 8000." +
                                Environment.NewLine +
                                "8. The position of PASP is consistent with the train position, PA distance scale value and distance scale lines.");

            MakeTestStepHeader(7, UniqueIdentifier++, "Press ‘Scale down’ button",
                "Verify that the DMI displays the distance ranges from 0 to 16000.The scale numbers are displayed in Medium-Grey colour and vertically centred on the distance scale lines.It is presented aligned to the right of Sub-Area D1.There are 9 distance scale lines displayed crossing sub-areas D2 – D7.From the bottom to top each line of distance scale are displayed with following colour,1st, 6th and 9th distance scale line are Medium-GreyOther distance scale line are Dark-greyThe following PA distance scale number are displayed in meter (see comment).020004000800016000At the following PA distance scale number, there are distance scale line displayed.04008001200160020004000800016000Note: Need a self calculation for distance scale number in some location which not have distance scale number specify.The position of PASP is consisted with train position refer to PA distance scale number and distance scale line");
            /*
            Test Step 7
            Action: Press ‘Scale down’ button
            Expected Result: Verify that the DMI displays the distance ranges from 0 to 16000.The scale numbers are displayed in Medium-Grey colour and vertically centred on the distance scale lines.It is presented aligned to the right of Sub-Area D1.There are 9 distance scale lines displayed crossing sub-areas D2 – D7.From the bottom to top each line of distance scale are displayed with following colour,1st, 6th and 9th distance scale line are Medium-GreyOther distance scale line are Dark-greyThe following PA distance scale number are displayed in meter (see comment).020004000800016000At the following PA distance scale number, there are distance scale line displayed.04008001200160020004000800016000Note: Need a self calculation for distance scale number in some location which not have distance scale number specify.The position of PASP is consisted with train position refer to PA distance scale number and distance scale line
            Test Step Comment: (1) MMI_gen 9938 (partly: MMI_gen 7213 (partly: Scale number colour)); (2) MMI_gen 9938 (partly: MMI_gen 7212      (partly: 9 distance scale lines));(3) MMI_gen 9938 (partly: MMI_gen 7213   (partly: Distance scale lines colour));(4) MMI_gen 9938 (partly: MMI_gen 7116 (partly: [0..16000], Displayed Numbers in Units));(5) MMI_gen 9938 (partly: MMI_gen 7116 (partly: [0..16000], Displayed Distance Scale Lines));(6) MMI_gen 9938 (partly: functions displayed in D2-D8);         Note: MMI_gen 7212 and MMI_gen 7213 shall also verify by Code Review in Chapter 39. 
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press the ‘Scale down’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Verify that the DMI displays the distance ranges from 0 to 16000." +
                                Environment.NewLine +
                                "2. Scale numbers are in Medium-grey, vertically centred on the distance scale lines and aligned to the right of sub-area D1." +
                                Environment.NewLine +
                                "3. 9 distance scale lines are displayed crossing sub-areas D2 – D7." +
                                Environment.NewLine +
                                "4. From the bottom to top the 1st, 6th and 9th distance scale lines are in Medium-grey" +
                                Environment.NewLine +
                                "5. All other distance scale lines are in Dark-grey" + Environment.NewLine +
                                "6. The PA distance scale values 0, 2000, 4000, 8000, 16000 are displayed in m." +
                                Environment.NewLine +
                                "7. Distance scale lines are displayed at PA distance scale values 0, 400, 800, 1200, 1600, 2000, 4000, 8000, 16000." +
                                Environment.NewLine +
                                "8. The position of PASP is consistent with the train position, PA distance scale value and distance scale lines.");

            MakeTestStepHeader(8, UniqueIdentifier++, "Press ‘Scale down’ button",
                "Verify that the DMI displays the distance ranges from 0 to 32000.The scale numbers are displayed in Medium-Grey colour and vertically centred on the distance scale lines.It is presented aligned to the right of Sub-Area D1.There are 9 distance scale lines displayed crossing sub-areas D2 – D7.From the bottom to top each line of distance scale are displayed with following colour,1st, 6th and 9th distance scale line are Medium-GreyOther distance scale line are Dark-greyThe following PA distance scale number are displayed in meter (see comment).0400080001600032000At the following PA distance scale number, there are distance scale line displayed.0800160024003200400080001600032000Note: Need a self calculation for distance scale number in some location which not have distance scale number specify.The position of PASP is consisted with train position refer to PA distance scale number and distance scale line");
            /*
            Test Step 8
            Action: Press ‘Scale down’ button
            Expected Result: Verify that the DMI displays the distance ranges from 0 to 32000.The scale numbers are displayed in Medium-Grey colour and vertically centred on the distance scale lines.It is presented aligned to the right of Sub-Area D1.There are 9 distance scale lines displayed crossing sub-areas D2 – D7.From the bottom to top each line of distance scale are displayed with following colour,1st, 6th and 9th distance scale line are Medium-GreyOther distance scale line are Dark-greyThe following PA distance scale number are displayed in meter (see comment).0400080001600032000At the following PA distance scale number, there are distance scale line displayed.0800160024003200400080001600032000Note: Need a self calculation for distance scale number in some location which not have distance scale number specify.The position of PASP is consisted with train position refer to PA distance scale number and distance scale line
            Test Step Comment: (1) MMI_gen 9938 (partly: MMI_gen 7213 (partly: Scale number colour));(2) MMI_gen 9938 (partly: MMI_gen 7212      (partly: 9 distance scale lines));(3) MMI_gen 9938 (partly: MMI_gen 7213   (partly: Distance scale lines colour));(4) MMI_gen 9938 (partly: MMI_gen 7116 (partly: [0..32000], Displayed Numbers in Units));(5) MMI_gen 9938 (partly: MMI_gen 7116 (partly: [0..32000], Displayed Distance Scale Lines));(6) MMI_gen 9938 (partly: functions displayed in D2-D8);      Note: MMI_gen 7212 and MMI_gen 7213 shall also verify by Code Review in Chapter 39. 
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press the ‘Scale down’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Verify that the DMI displays the distance ranges from 0 to 32000." +
                                Environment.NewLine +
                                "2. Scale numbers are in Medium-grey, vertically centred on the distance scale lines and aligned to the right of sub-area D1." +
                                Environment.NewLine +
                                "3. 9 distance scale lines are displayed crossing sub-areas D2 – D7." +
                                Environment.NewLine +
                                "4. From the bottom to top the 1st, 6th and 9th distance scale lines are in Medium-grey" +
                                Environment.NewLine +
                                "5. All other distance scale lines are in Dark-grey" + Environment.NewLine +
                                "6. The PA distance scale values 0, 4000, 8000, 16000, 32000 are displayed in m." +
                                Environment.NewLine +
                                "7. Distance scale lines are displayed at PA distance scale values 0, 800, 1600, 2400, 3200, 4000, 8000, 16000, 32000." +
                                Environment.NewLine +
                                "8. The position of PASP is consistent with the train position, PA distance scale value and distance scale lines.");
            MakeTestStepHeader(9, UniqueIdentifier++, "Simulate the communication loss between ETCS Onboard and DMI",
                "DMI displays the message “ATP Down Alarm” with sound.The PA is removed from DMI");
            /*
            Test Step 9
            Action: Simulate the communication loss between ETCS Onboard and DMI
            Expected Result: DMI displays the message “ATP Down Alarm” with sound.The PA is removed from DMI
            */
            // Call generic Action Method
            DmiActions.Simulate_communication_loss_EVC_DMI(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the message ‘ATP Down Alarm’." + Environment.NewLine +
                                "2. The ‘Alarm’ sound is played." + Environment.NewLine +
                                "3. DMI stops displaying the Planning Area.");

            MakeTestStepHeader(10, UniqueIdentifier++, "Re-establish the communication between ETCS onboard and DMI",
                "Verify that PA distance scale is not changed the selected range of PA distance scale, still display range as [0..32000]");
            /*
            Test Step 10
            Action: Re-establish the communication between ETCS onboard and DMI
            Expected Result: Verify that PA distance scale is not changed the selected range of PA distance scale, still display range as [0..32000]
            Test Step Comment: MMI_gen 7147 (partly: Communication loss);
            */
            DmiActions.Re_establish_communication_EVC_DMI(this);
            EVC4_MMITrackDescription.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Planning Area" + Environment.NewLine +
                                "2. The PA distance scale has the range 0-32000m");

            MakeTestStepHeader(11, UniqueIdentifier++,
                "Power OFF system.Then, power ON system and repeat action step 1-3",
                "Verify that the DMI displays the distance ranges from 0 to 4000 meter as default value");
            /*
            Test Step 11
            Action: Power OFF system.Then, power ON system and repeat action step 1-3
            Expected Result: Verify that the DMI displays the distance ranges from 0 to 4000 meter as default value
            Test Step Comment: MMI_gen 7148 (partly: Default applies after power loss);
            */
            // ?? Does this re-boot
            // Repeat Step 1
            DmiActions.Complete_SoM_L1_SR(this);

            // Repeat Step 2
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.FullSupervision;

            // Repeat Step 3       
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN = 20000; // 200m

            EVC4_MMITrackDescription.TrackDescriptions = descriptionsList;
            EVC4_MMITrackDescription.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the distance range 0 to 4000 m by default.");

            MakeTestStepHeader(12, UniqueIdentifier++, "End of test", "");

            /*
            Test Step 12
            Action: End of test
            Expected Result: 
            */


            return GlobalTestResult;
        }
    }
}
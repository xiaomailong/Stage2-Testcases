using System;
using System.Collections.Generic;
using Testcase.Telegrams.EVCtoDMI;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 22.5.2 PA Gradient Profile:  Display of many PA Gradient Profile
    /// TC-ID: 17.5.2
    /// 
    /// This test case verifies the PA Gradient Profile displays on sub-area D5. The Gradient Profile shall display PA Gradient Profile segments. The condition to display the gradient profiles shall comply with [MMI-ETCS-gen].
    /// 
    /// Tested Requirements:
    /// MMI_gen 7262; MMI_gen 7264 (partly: 2nd – 5th bullet); MMI_gen 7266 (partly: 2nd – 4th bullet);
    /// 
    /// Scenario:
    /// Activate cabin A. Perform SoM to SR mode, level 
    /// 1.Drive the train forward pass BG1 at position 
    /// 100.Then, verify that PA is displayed in area D.BG1 giving pkt12, pkt 21 and pkt
    /// 27.Press <Scale Down> button 3 times. Then, verify that the gradient segments are update accordingly.Press <Scale Up> button 3 times.Continue to drive train forward. Then, verify that the gradient segments are update accordingly.
    /// 
    /// Used files:
    /// 17_5_2.tdg
    /// </summary>
    public class TC_ID_17_5_2_PA_Gradient_Profile_Display_of_many_PA_Gradient_Profile : TestcaseBase
    {
        public override bool TestcaseEntryPoint()
        {
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 23746;
            // Testcase entrypoint

            MakeTestStepHeader(1, UniqueIdentifier++, "Activate cabin A. Then  perform SoM to SR mode, level 1",
                "DMI displays SR mode, level 1");
            /*
            Test Step 1
            Action: Activate cabin A. Then  perform SoM to SR mode, level 1
            Expected Result: DMI displays SR mode, level 1
            */
            // Force this, tested elsewhere
            StartUp();
            DmiActions.Complete_SoM_L1_SR(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in SR mode, Level 1.");

            MakeTestStepHeader(2, UniqueIdentifier++, "Drive the train forward pass BG1.Then, stop the train",
                "DMI changes from SR to FS mode. The Planning Area is displayed in area D.The order of PA Gradient Profile segments are displayed correctly refer to received EVC-4 packet (see figure in comment),0-250m: Gradient Profile value = 2 (grey colour)251-500m: Gradient Profile value = 5 (grey colour)501-1000m: Gradient Profile value = 20 (grey colour)1001-2000m: Gradient Profile value = 16 (dark-grey colour)2001-4000m: Gradient Profile value = 10 (grey colour)The lower boundary of each PA Gradient are placed in sub-area D5 at following position,0 m250 m500 m1000 m2000 mUse the log file to confirm that DMI receives EVC-4 packet with the following variables,MMI_G_GRADIENT_CURR = 2MMI_N_GRADIENT = 4MMI_G_GRADIENT[0] = 5MMI_G_GRADIENT[1] = 20MMI_G_GRADIENT[2] = -16MMI_G_GRADIENT[3] = 10Note: The first index of parameter is the topmost position in packet EVC-4");
            /*
            Test Step 2
            Action: Drive the train forward pass BG1.Then, stop the train
            Expected Result: DMI changes from SR to FS mode. The Planning Area is displayed in area D.The order of PA Gradient Profile segments are displayed correctly refer to received EVC-4 packet (see figure in comment),0-250m: Gradient Profile value = 2 (grey colour)251-500m: Gradient Profile value = 5 (grey colour)501-1000m: Gradient Profile value = 20 (grey colour)1001-2000m: Gradient Profile value = 16 (dark-grey colour)2001-4000m: Gradient Profile value = 10 (grey colour)The lower boundary of each PA Gradient are placed in sub-area D5 at following position,0 m250 m500 m1000 m2000 mUse the log file to confirm that DMI receives EVC-4 packet with the following variables,MMI_G_GRADIENT_CURR = 2MMI_N_GRADIENT = 4MMI_G_GRADIENT[0] = 5MMI_G_GRADIENT[1] = 20MMI_G_GRADIENT[2] = -16MMI_G_GRADIENT[3] = 10Note: The first index of parameter is the topmost position in packet EVC-4
            Test Step Comment: (1) MMI_gen 7262 (partly: process all valid entries); (2) MMI_gen 7264 (partly: 2nd  bullet, result of calculation);    MMI_gen 7264 (partly: 3rd bullet, lower boundary); (3) MMI_gen 7262 (partly: reception of packet EVC-4); 
            */
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.FullSupervision;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN = 10000;

            List<TrackDescription> descriptionsList = new List<TrackDescription>()
            {
                new TrackDescription {MMI_G_GRADIENT = 2, MMI_O_GRADIENT = 25000},
                new TrackDescription {MMI_G_GRADIENT = 5, MMI_O_GRADIENT = 50000},
                new TrackDescription {MMI_G_GRADIENT = 20, MMI_O_GRADIENT = 100000},
                new TrackDescription {MMI_G_GRADIENT = -16, MMI_O_GRADIENT = 200000},
                new TrackDescription {MMI_G_GRADIENT = 10, MMI_O_GRADIENT = 400000},
            };

            EVC4_MMITrackDescription.TrackDescriptions = descriptionsList;
            EVC4_MMITrackDescription.MMI_G_GRADIENT_CURR = 2;
            EVC4_MMITrackDescription.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in FS mode, Level 1." + Environment.NewLine +
                                "2. The Gradient Profile displays in sub-area D5, shown as a set of bars with different gradients." +
                                Environment.NewLine +
                                "3. 5 Gradient Profile segment bars are displayed, described from the bottom upwards:" +
                                Environment.NewLine +
                                "4. The gradient values are displayed in the centre of the bar." + Environment.NewLine +
                                "5  Bars with positive gradient values have a ‘+’ sign at the top and bottom edges." +
                                Environment.NewLine +
                                "6. Bar #4 has a negative gradient value indicated by ‘-’ signs at the top and bottom edges." +
                                Environment.NewLine +
                                "7. Bar #1 has the value ‘2’ in black on a grey background (from 0 to 250), with its lower border at 0m." +
                                Environment.NewLine +
                                "8. Bar #2 has the value ‘5’ in black on a grey background (from 251 to 500), with its lower border at 250m.." +
                                Environment.NewLine +
                                "9. Bar #3 has the value ‘20’ in black on a grey background (from 501 to 1000), with its lower border at 500m.." +
                                Environment.NewLine +
                                "10. Bar #4 has the value ‘16 in white  on a Dark-grey background (from 1001 to 2000), with its lower border at 2000m." +
                                Environment.NewLine +
                                "11. Bar #5 has the value ‘10’ in black on a grey background (from 2001 to 4000)." +
                                Environment.NewLine +
                                "12. The window contains scale up (+) and scale down (-) buttons." +
                                Environment.NewLine +
                                "13. The gradients are on a scale from 0 to 4000m.");

            MakeTestStepHeader(3, UniqueIdentifier++, "Press <Scale Down> button 3 times",
                "The distance scale is changed to 0-32000 m.The PA Gradient Profiles are displayed within MA (0-8000m), the upper boundary of PA Gradient profile is displayed at the same position of target zero speed (8000m)");
            /*
            Test Step 3
            Action: Press <Scale Down> button 3 times
            Expected Result: The distance scale is changed to 0-32000 m.The PA Gradient Profiles are displayed within MA (0-8000m), the upper boundary of PA Gradient profile is displayed at the same position of target zero speed (8000m)
            Test Step Comment: (1) MMI_gen 7264 (partly: 5th bullet);
            */
            DmiActions.ShowInstruction(this, "Press the Scale Down button 3 times");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The distance scale changes to 0-32000m.");

            MakeTestStepHeader(4, UniqueIdentifier++, "Press <Scale Up> button 3 times.Then, drive the train forward",
                "The PA Gradient Profile segment bars are continuously updated.At the lowest PA Gradient Profile, the lower boundary is stuck to the zero line.The PA Gradient Profile bar is become shorter accordingly");
            /*
            Test Step 4
            Action: Press <Scale Up> button 3 times.Then, drive the train forward
            Expected Result: The PA Gradient Profile segment bars are continuously updated.At the lowest PA Gradient Profile, the lower boundary is stuck to the zero line.The PA Gradient Profile bar is become shorter accordingly
            Test Step Comment: (1) MMI_gen 7266 (partly: 4th bullet, lower boundary);          MMI_gen 7266 (partly: 2nd bullet, result of calculation);                      (2) MMI_gen 7266 (partly: 3rd bar become shorter);             MMI_gen 7264 (partly: 4th  bullet, shortened);                   
            */
            DmiActions.ShowInstruction(this, "Press the Scale Up button 3 times");

            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN = 30000;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The PA Gradient Profiles segment bars are updated." + Environment.NewLine +
                                "2. The lower border of the lowest PA Gradient Profile is fixed at the zero line." +
                                Environment.NewLine +
                                "3. The PA Gradient Profile bar shortens accordingly");

            MakeTestStepHeader(5, UniqueIdentifier++, "Continue to drive the train forward",
                "The PA Gradient Profile segment bars are continuously updated.Verify the following information,PA Gradient Profile Segment is moved down to the Zero Line then its change to the next value");
            /*
            Test Step 5
            Action: Continue to drive the train forward
            Expected Result: The PA Gradient Profile segment bars are continuously updated.Verify the following information,PA Gradient Profile Segment is moved down to the Zero Line then its change to the next value
            Test Step Comment: (1) MMI_gen 7266 (partly: 2nd bullet, result of calculation);
            */
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN = 35000;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The PA Gradient Profiles segment bars are updated." + Environment.NewLine +
                                "2. The upper border of the lowest PA Gradient Profile reaches the zero line." +
                                Environment.NewLine +
                                "3. The lower border of the next (second) PA Gradient Profile bar reaches the zero line.");

            MakeTestStepHeader(6, UniqueIdentifier++, "End of test", "");

            /*
            Test Step 6
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
using System;
using System.Collections.Generic;
using Testcase.Telegrams.EVCtoDMI;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 22.5.1 PA Gradient Profile:  General appearance
    /// TC-ID: 17.5.1
    /// 
    /// This test case verifies that when DMI receives the data packet of gradient profiles from ETCS Onboard, the PA Gradient Profile displays in sub-area D5 correctly for uphill, downhill and zero gradient. Also verify the situation of communication lost, PAGP shall be removed. The PA Gradient Profile shall comply with [ERA-ERTMS] standard.
    /// 
    /// Tested Requirements:
    /// MMI_gen 638; MMI_gen 7271; MMI_gen 640;  MMI_gen 7268; MMI_gen 639;  MMI_gen 2605; MMI_gen 9940; MMI_gen 3050 (partly: white line); MMI_gen 3034 (partly: grey line); MMI_gen 7270 (partly: black line); Note under MMI_gen 7271;
    /// 
    /// Scenario:
    /// Activate cabin A. Perform SoM to SR mode, level 
    /// 1.Drive the train forward pass BG1 at position 
    /// 100.BG1: Packet 12, 21 and 27D_GRADIENT = 0, Q_GDIR=1, G_A=2 (uphill gradient)  N_ITER = 4D_GRADIENT = 200, Q_GDIR=1, G_A =5 (uphill gradient) D_GRADIENT = 200, Q_GDIR=0, G_A =20 (downhill gradient)D_GRADIENT = 200, Q_GDIR=1, G_A =0 (zero gradient)D_GRADIENT = 200, Q_GDIR=1, G_A =10 (uphill gradient)Drive the train until position 900m. Verify the correctness of the gradient profile displayed on the Planning area.Then simulate the communication loss between ETCS Onboard and DMI. After that re-establish the communication again.
    /// 
    /// Used files:
    /// 17_5_1.tdg
    /// </summary>
    public class TC_ID_17_5_1_PA_Gradient_Profile_General_appearance : TestcaseBase
    {

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint


            /*
            Test Step 1
            Action: Activate cabin A. Driver performs SoM to SR mode, level 1
            Expected Result: DMI displays in SR mode, level 1
            */
            // Force this, tested elsewhere
            DmiActions.Complete_SoM_L1_SR(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in SR mode, Level 1.");

            /*
            Test Step 2
            Action: Drive the train forward passing BG1
            Expected Result: DMI changes from SR mode to FS mode.The Planning Area is displayed in area D.Verify that the Gradient Profile is displayed in sub-area D5 and shown as a set of bars with different gradients. (see the second figure in ‘Comment’ column)The Gradient Profile segment bar is displayed two ‘+’ signs for uphill gradient, two ‘-‘ signs for downhill gradient, and no sign for zero gradient. The gradient value is displayed in the middle of the bar. (see the second figure in ‘Comment’ column)The Downhill PA Gradient Profile segment bars are displayed in dark-grey colour with the value and sign of gradient in white. The Uphill and zero PA Gradient Profile segment bars are displayed in grey colour with the value and sign of gradient in black.The Uphill and zero PA Gradient Profile have a white line on their upper and left boundary.The Downhill PA Gradient Profile have a grey line on their upper and left boundary.All PA gradient Profile have a black line on their lower boundary
            Test Step Comment: (1) MMI_gen 638;      MMI_gen 9940;    (2) MMI_gen 640;              MMI_gen 7268;                       (3) MMI_gen 639;             (4) MMI_gen 2605;       (5) MMI_gen 3050 (partly: white line);         (6) MMI_gen 3034 (partly: grey line);           (7) MMI_gen 7270   (partly: black line);
            */
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.FullSupervision;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN = 300000;

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
                new TrackDescription
                {
                    MMI_G_GRADIENT = 20,
                    MMI_O_GRADIENT = 30000,
                    MMI_O_MRSP = 20500,
                    MMI_V_MRSP = 600
                },
                new TrackDescription {MMI_G_GRADIENT = -5, MMI_O_GRADIENT = 40000, MMI_O_MRSP = 30000, MMI_V_MRSP = 400}
            };

            EVC4_MMITrackDescription.TrackDescriptions = descriptionsList;
            EVC4_MMITrackDescription.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in FS mode, Level 1." + Environment.NewLine +
                                "2. The Gradient Profile displays in sub-area D5, shown as a set of bars with different gradients." +
                                Environment.NewLine +
                                "3. The Gradient Profile segment bar displays two ‘+’ signs for uphill gradient," +
                                Environment.NewLine +
                                "   two ‘-’ signs for downhill gradient and no sign for zero gradient." +
                                Environment.NewLine +
                                "4. The gradient value is displayed in the centre of the bar." + Environment.NewLine +
                                "5. The Downhill PA Gradient Profile segment bars have values in white on a Dark-grey background." +
                                Environment.NewLine +
                                "6. The Uphill and zero PA Gradient Profile segment bars have values (and the signs) in black on a grey background." +
                                Environment.NewLine +
                                "7. The Uphill and zero PA Gradient Profiles have  white upper and left borders." +
                                Environment.NewLine +
                                "8. The Downhill PA Gradient Profiles have grey upper and left borders." +
                                Environment.NewLine +
                                "9. The lower borders of all PA Gradient Profiles are black");

            /*
            Test Step 3
            Action: Simulate the communication loss between ETCS Onboard and DMI
            Expected Result: DMI displays the  message “ATP Down Alarm” with sound.Verify that the PA Gradient Profile is removed from DMI
            Test Step Comment: MMI_gen 7271;   
            */
            // Call generic Action Method
            DmiActions.Simulate_communication_loss_EVC_DMI(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the ‘ATP Down Alarm’." + Environment.NewLine +
                                "2. The ‘Alarm’ sound is played." + Environment.NewLine +
                                "3. DMI does not display PA Gradient Profiles.");

            /*
            Test Step 4
            Action: Re-establish the communication between ETCS onboard and DMI
            Expected Result: DMI displays in FS mode, level 1. The PA Gradient Profile is resumed
            Test Step Comment: Note under MMI_gen 7271;
            */
            // Call generic Action Method
            DmiActions.Re_establish_communication_EVC_DMI(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in FS mode, Level 1." + Environment.NewLine +
                                "2. The PA Gradient Profiles are re-displayed.");

            /*
            Test Step 5
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
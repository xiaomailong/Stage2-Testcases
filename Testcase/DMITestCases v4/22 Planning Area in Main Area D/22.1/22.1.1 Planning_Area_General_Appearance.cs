using System;
using System.Collections.Generic;
using Testcase.Telegrams.EVCtoDMI;

namespace Testcase.DMITestCases
{
    /// <summary>
    /// 22.1.1 Planning Area: General Appearance
    /// TC-ID: 17.1.1
    /// 
    /// This test case verifies the presentation of planning area in area D when DMI changes to FS mode.
    /// The planning area shall display the planning information and all objects to driver.
    /// The presentation of the planning area shall comply with [ERA-ERTMS] standard and [MMI-ETCS-gen].
    /// 
    /// Tested Requirements:
    /// MMI_gen 3063 (partly: FS mode);
    /// MMI_gen 7101 (partly: default configuration);
    /// MMI_gen 7102 (partly: default configuration); MMI_gen 9937;
    /// 
    /// Scenario:
    /// Activate Cabin A. Perform SoM in SR mode, Level 1. Drive train forward pass BG1 at 100 m.
    /// Then, verify that PA is not displayed in the SR mode.
    /// BG1: Packet 21 and 27. Drive train forward pass BG2 at 150 m.
    /// Then, verify that all objects in MMI_gen 9937 are displayed BG2: Packet 12 and 68
    /// 
    /// Used files:
    /// 17_1_1.tdg
    /// </summary>
    public class TC_ID_17_1_1_Planning_Area : TestcaseBase
    {
        public override bool TestcaseEntryPoint()
        {
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 23268;
            // Testcase entrypoint

            MakeTestStepHeader(1, UniqueIdentifier++, "Activate cabin A", "DMI displays Driver ID window");
            /*
            Test Step 1
            Action: Activate cabin A
            Expected Result: DMI displays Driver ID window
            */
            // Call generic Action Method
            StartUp();

            MakeTestStepHeader(2, UniqueIdentifier++, "Perform SoM to SR mode, level 1",
                "DMI displays in SR mode, level 1");
            /*
            Test Step 2
            Action: Perform SoM to SR mode, level 1
            Expected Result: DMI displays in SR mode, level 1
            */
            DmiActions.Complete_SoM_L1_SR(this);
            DmiActions.Finished_SoM_Default_Window(this);

            // Call generic Check Results Method
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The DMI shows Staff Responsible Mode, Level 1." + Environment.NewLine +
                                "2. Confirm that the planning area is NOT displayed.");

            MakeTestStepHeader(3, UniqueIdentifier++, "Drive the train forward passing BG1",
                "DMI remain displays in SR mode, level 1. Verify that the Planning area is NOT displayed in main area D.");
            /*
            Test Step 3
            Action: Drive the train forward passing BG1
            Expected Result: DMI remain displays in SR mode, level 1. Verify that the Planning area is not displayed in main area D
            Test Step Comment: MMI_gen 7101 (partly: default  configuration);              
            */
            // Call generic Action Method

            MakeTestStepHeader(4, UniqueIdentifier++, "Drive the train forward passing BG2",
                "DMI changes from SR mode to FS mode.");
            /*
            Test Step 4
            Action: Drive the train forward passing BG2
            Expected Result: DMI changes from SR mode to FS mode.
                            The Planning area is displayed the planning information in main area D.
                            The planning area is displayed the information following:
                                Distance scale
                                Order and announcement of track condition
                                Gradient profile
                                Speed profile discontinuities
                                PASPIndication marker
                                Hide and show planning information
                                Zoom function (see the example in ‘Comment’ column)
            Test Step Comment: (1) MMI_gen 3063 (partly: FS mode); MMI_gen 7102 (partly: default  configuration);
                                (2) MMI_gen 9937;   
            */
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.FullSupervision;

            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN_M = 1;
            EVC1_MMIDynamic.MMI_O_BRAKETARGET_M = 2000;

            TrackDescription trackDesc1 = new TrackDescription { MMI_V_MRSP_KMH = 40, MMI_O_MRSP_M = 200, MMI_G_GRADIENT = 10, MMI_O_GRADIENT_M = 200 };
            TrackDescription trackDesc2 = new TrackDescription { MMI_V_MRSP_KMH = 50, MMI_O_MRSP_M = 400, MMI_G_GRADIENT = 20, MMI_O_GRADIENT_M = 400 };
            TrackDescription trackDesc3 = new TrackDescription { MMI_V_MRSP_KMH = 60, MMI_O_MRSP_M = 600, MMI_G_GRADIENT = -30, MMI_O_GRADIENT_M = 600 };
            TrackDescription trackDesc4 = new TrackDescription { MMI_V_MRSP_KMH = 70, MMI_O_MRSP_M = 800, MMI_G_GRADIENT = -15, MMI_O_GRADIENT_M = 800 };

            EVC4_MMITrackDescription.MMI_G_GRADIENT_CURR = 5;
            EVC4_MMITrackDescription.MMI_V_MRSP_CURR_KMH = 30;
            EVC4_MMITrackDescription.TrackDescriptions = new List<TrackDescription>{trackDesc1, trackDesc2, trackDesc3, trackDesc4};
            EVC4_MMITrackDescription.Send();

            // EVC-32 values
            TrackCondition trackCond = new TrackCondition
            {
                MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Air_tightness,
                MMI_NID_TRACKCOND = 1,
                MMI_O_TRACKCOND_ANNOUNCE = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN + 1500,
                MMI_O_TRACKCOND_START = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN + 30000,
                MMI_O_TRACKCOND_END = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN + 60000,
                MMI_Q_TRACKCOND_ACTION_START = Variables.MMI_Q_TRACKCOND_ACTION.WithoutDriverAction,
                MMI_Q_TRACKCOND_ACTION_END = Variables.MMI_Q_TRACKCOND_ACTION.WithoutDriverAction,
                MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.AnnounceArea
            };

            EVC32_MMITrackConditions.MMI_Q_TRACKCOND_UPDATE = 0;
            EVC32_MMITrackConditions.TrackConditions = new List<TrackCondition>{trackCond};
            EVC32_MMITrackConditions.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The DMI show Full Supervision Mode." + Environment.NewLine +
                                "2. Confirm that the planning area is displayed with the following items:" + Environment.NewLine
                                 + Environment.NewLine +
                                "a. Distance scale" + Environment.NewLine +
                                "b. Order and announcement of Air Tightness track condition" + Environment.NewLine +
                                "c. Gradient profile" + Environment.NewLine +
                                "d. Speed profile discontinuities" + Environment.NewLine +
                                "e. PASPIndication marker" + Environment.NewLine +
                                "f. Hide and show planning information" + Environment.NewLine +
                                "g. Zoom function");

            TraceHeader("End of test");

            /*
            Test Step 5
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
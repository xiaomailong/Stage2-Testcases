using System;
using System.Collections.Generic;
using Testcase.Telegrams.EVCtoDMI;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 22.4.16 PA Track Condition: 30 PA Track Conditions
    /// TC-ID: 17.4.16
    /// 
    /// This test case is to verify ETCS MMI can handle at least 30 PA the track conditions . 
    /// 
    /// Tested Requirements:
    /// MMI_gen 1282; MMI_gen 1317; MMI_gen 1652; MMI_gen 1050; MMI_gen 3051;
    /// 
    /// Scenario:
    /// 1.Drive the train forward pass BG0 at position 10m.BG0: pkt 12, 21 and 27 (Entering FS) 
    /// 2.Drive the train forward pass BG1 with containing 30 PA Track condtions       at position 100m 
    /// 3.Verify that PA track condition presents on Sub-Area D2, D3, D
    /// 44.Simulate commnunication loss then verify that all PA Track condtions shall be  removed
    /// 
    /// Used files:
    /// 17_4_16.tdg
    /// </summary>
    public class TC_ID_17_4_16_PA_Track_Condition_30_PA_Track_Conditions : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // Test system is power on.SoM is performed in SR mode, level 1.

            // Call the TestCaseBase PreExecution
            base.PreExecution();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in system failure mode

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 0;
            // Testcase entrypoint

            TraceHeader("Test Step 1");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Drive the train up to 20 km/h");
            TraceReport("Expected Result");
            TraceInfo("The speed pointer is indicated as 20  km/h");
            /*
            Test Step 1
            Action: Drive the train up to 20 km/h
            Expected Result: The speed pointer is indicated as 20  km/h
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 20;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in SB mode, Level 1.");

            TraceHeader("Test Step 2");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Drive the train forward pass BG0 with MA and Track descriptionPkt 12,21 and 27");
            TraceReport("Expected Result");
            TraceInfo("Mode changes to FS mode , L1");
            /*
            Test Step 2
            Action: Drive the train forward pass BG0 with MA and Track descriptionPkt 12,21 and 27
            Expected Result: Mode changes to FS mode , L1
            */
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.FullSupervision;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in FS mode, Level 1.");

            TraceHeader("Test Step 3");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Pass BG1 with Track conditionPkt 68:D_TRACKCOND = 500L_TRACKCOND = 200M_TRACKCOND = 8(Switch off magnetic shoe brake)");
            TraceReport("Expected Result");
            TraceInfo("Mode remians in FS mode");
            /*
            Test Step 3
            Action: Pass BG1 with Track conditionPkt 68:D_TRACKCOND = 500L_TRACKCOND = 200M_TRACKCOND = 8(Switch off magnetic shoe brake)
            Expected Result: Mode remians in FS mode
            */
            EVC32_MMITrackConditions.MMI_Q_TRACKCOND_UPDATE = 0;

            List<TrackCondition> trackConditions =
                new List<TrackCondition>
                {
                    /* 0 */
                    new TrackCondition
                    {
                        MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Regenerative_Brakes,
                        MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.AnnounceArea,
                        MMI_Q_TRACKCOND_ACTION_START = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction
                    },
                    /* 1 */
                    new TrackCondition
                    {
                        MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Tunnel_Stopping_Area,
                        MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.AnnounceArea,
                        MMI_Q_TRACKCOND_ACTION_START = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction
                    },
                    /* 2 */
                    new TrackCondition
                    {
                        MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Magnetic_Shoe_Brakes,
                        MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.AnnounceArea,
                        MMI_Q_TRACKCOND_ACTION_START = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction
                    },
                    /* 3 */
                    new TrackCondition
                    {
                        MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Main_power_switch_Neutral_Section,
                        MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.AnnounceArea,
                        MMI_Q_TRACKCOND_ACTION_START = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction
                    },
                    /* 4 */
                    new TrackCondition
                    {
                        MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Radio_hole,
                        MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.InsideArea_Active
                    },
                    /* 5 */
                    new TrackCondition
                    {
                        MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Air_tightness,
                        MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.AnnounceArea,
                        MMI_Q_TRACKCOND_ACTION_START = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction
                    },
                    /* 6 */
                    new TrackCondition
                    {
                        MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Non_Stopping_Area,
                        MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.AnnounceArea,
                        MMI_Q_TRACKCOND_ACTION_START = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction
                    },
                    /* 7 */
                    new TrackCondition
                    {
                        MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Pantograph,
                        MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.InsideArea_Active,
                        MMI_Q_TRACKCOND_ACTION_START = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction
                    },
                    /* 8 */
                    new TrackCondition
                    {
                        MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Magnetic_Shoe_Brakes,
                        MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.AnnounceArea,
                        MMI_Q_TRACKCOND_ACTION_START = Variables.MMI_Q_TRACKCOND_ACTION.WithoutDriverAction
                    },
                    /* 0 */
                    new TrackCondition
                    {
                        MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Regenerative_Brakes,
                        MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.AnnounceArea,
                        MMI_Q_TRACKCOND_ACTION_START = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction
                    },
                    /* 1 */
                    new TrackCondition
                    {
                        MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Tunnel_Stopping_Area,
                        MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.AnnounceArea,
                        MMI_Q_TRACKCOND_ACTION_START = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction
                    },
                    /* 2 */
                    new TrackCondition
                    {
                        MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Magnetic_Shoe_Brakes,
                        MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.AnnounceArea,
                        MMI_Q_TRACKCOND_ACTION_START = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction
                    },
                    /* 3 */
                    new TrackCondition
                    {
                        MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Main_power_switch_Neutral_Section,
                        MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.AnnounceArea,
                        MMI_Q_TRACKCOND_ACTION_START = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction
                    },
                    /* 4 */
                    new TrackCondition
                    {
                        MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Radio_hole,
                        MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.InsideArea_Active
                    },
                    /* 5 */
                    new TrackCondition
                    {
                        MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Air_tightness,
                        MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.AnnounceArea,
                        MMI_Q_TRACKCOND_ACTION_START = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction
                    },
                    /* 6 */
                    new TrackCondition
                    {
                        MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Non_Stopping_Area,
                        MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.AnnounceArea,
                        MMI_Q_TRACKCOND_ACTION_START = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction
                    },
                    /* 1 */
                    new TrackCondition
                    {
                        MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Tunnel_Stopping_Area,
                        MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.AnnounceArea,
                        MMI_Q_TRACKCOND_ACTION_START = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction
                    },
                    /* 6 */
                    new TrackCondition
                    {
                        MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Non_Stopping_Area,
                        MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.AnnounceArea,
                        MMI_Q_TRACKCOND_ACTION_START = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction
                    },
                    /* 3 */
                    new TrackCondition
                    {
                        MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Main_power_switch_Neutral_Section,
                        MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.AnnounceArea,
                        MMI_Q_TRACKCOND_ACTION_START = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction
                    },
                    /* 1 */
                    new TrackCondition
                    {
                        MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Tunnel_Stopping_Area,
                        MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.AnnounceArea,
                        MMI_Q_TRACKCOND_ACTION_START = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction
                    },
                    /* 2 */
                    new TrackCondition
                    {
                        MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Magnetic_Shoe_Brakes,
                        MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.AnnounceArea,
                        MMI_Q_TRACKCOND_ACTION_START = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction
                    },
                    /* 8 */
                    new TrackCondition
                    {
                        MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Magnetic_Shoe_Brakes,
                        MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.AnnounceArea,
                        MMI_Q_TRACKCOND_ACTION_START = Variables.MMI_Q_TRACKCOND_ACTION.WithoutDriverAction
                    },
                    /* 0 */
                    new TrackCondition
                    {
                        MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Regenerative_Brakes,
                        MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.AnnounceArea,
                        MMI_Q_TRACKCOND_ACTION_START = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction
                    },
                    /* 9 */
                    new TrackCondition
                    {
                        MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Eddy_Current_Brakes,
                        MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.AnnounceArea,
                        MMI_Q_TRACKCOND_ACTION_START = Variables.MMI_Q_TRACKCOND_ACTION.WithoutDriverAction
                    },
                    /* 8 */
                    new TrackCondition
                    {
                        MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Magnetic_Shoe_Brakes,
                        MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.AnnounceArea,
                        MMI_Q_TRACKCOND_ACTION_START = Variables.MMI_Q_TRACKCOND_ACTION.WithoutDriverAction
                    },
                    /* 3 */
                    new TrackCondition
                    {
                        MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Main_power_switch_Neutral_Section,
                        MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.AnnounceArea,
                        MMI_Q_TRACKCOND_ACTION_START = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction
                    },
                    /* 5 */
                    new TrackCondition
                    {
                        MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Air_tightness,
                        MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.AnnounceArea,
                        MMI_Q_TRACKCOND_ACTION_START = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction
                    },
                    /* 7 */
                    new TrackCondition
                    {
                        MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Pantograph,
                        MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.InsideArea_Active,
                        MMI_Q_TRACKCOND_ACTION_START = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction
                    },
                    /* 4 */
                    new TrackCondition
                    {
                        MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Radio_hole,
                        MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.InsideArea_Active
                    },
                    /* 6 */
                    new TrackCondition
                    {
                        MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Non_Stopping_Area,
                        MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.AnnounceArea,
                        MMI_Q_TRACKCOND_ACTION_START = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction
                    }
                };

            EVC32_MMITrackConditions.TrackConditions = trackConditions;
            EVC32_MMITrackConditions.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays track condition symbols in sub-areas D2, D3, D4.");

            TraceHeader("Test Step 4");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Continue driving with 20 Km/h");
            TraceReport("Expected Result");
            TraceInfo(
                "The PA Track condition symbols are going down to the first distance scale (zero line) and no symbol jumping between D2, D3 and D4 area");
            /*
            Test Step 4
            Action: Continue driving with 20 Km/h
            Expected Result: The PA Track condition symbols are going down to the first distance scale (zero line) and no symbol jumping between D2, D3 and D4 area
            Test Step Comment: MMI_gen 1652;MMI_gen 1050;
            */
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN = 50000;
            // In diagram first scale line is at 125, bottom of symbol at ~60 (m)
            // O_TRACKCOND_ANNOUNCE - MMI_OBU_TR_O_TRAIN should == 6000

            // Set up 3 groups of 20m separated symbols (each 10th potentially over-lapping)
            int tc = 0;
            const int separation = 20000; // (200m)
            const int initialAnnouncement = 320000; // (3000m)
            foreach (TrackCondition trackCondition in trackConditions)
            {
                trackCondition.MMI_O_TRACKCOND_ANNOUNCE = initialAnnouncement - (tc * separation);
                tc = (++tc) % 10;
            }

            EVC32_MMITrackConditions.Send();

            Wait_Realtime(1000);
            for (tc = 0; tc < 5; tc++)
            {
                EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN += 20000;
                Wait_Realtime(500);
            }

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The track condition symbols move towards the first distance scale (zero line)." +
                                Environment.NewLine +
                                "2. No symbols jump between sub-areas D2, D3 and D4.");

            TraceHeader("Test Step 5");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Continue driving with 20 Km/h");
            TraceReport("Expected Result");
            TraceInfo("DMI displays remianing track condition symbols on sub-area D2 and D3");
            /*
            Test Step 5
            Action: Continue driving with 20 Km/h
            Expected Result: DMI displays remianing track condition symbols on sub-area D2 and D3
            */
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN += 10000;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the remaining track condition symbols in sub-areas D2 and D3.");

            TraceHeader("Test Step 6");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Continue driving with 20 Km/h");
            TraceReport("Expected Result");
            TraceInfo("DMI displays remianing track condition symbols on sub-area D2");
            /*
            Test Step 6
            Action: Continue driving with 20 Km/h
            Expected Result: DMI displays remianing track condition symbols on sub-area D2
            */
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN += 10000;
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the remaining track condition symbols in sub-area D2.");


            TraceHeader("Test Step 7");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Simulate loss-communication between ETCS onboard and DMI");
            TraceReport("Expected Result");
            TraceInfo(
                "DMI displays Default window with the  message “ATP Down Alarm” and sound alarmPA Track Condition symbol shall be removed from sub-area D2");
            /*
            Test Step 7
            Action: Simulate loss-communication between ETCS onboard and DMI
            Expected Result: DMI displays Default window with the  message “ATP Down Alarm” and sound alarmPA Track Condition symbol shall be removed from sub-area D2
            Test Step Comment: MMI_gen 3051;
            */
            DmiActions.Simulate_communication_loss_EVC_DMI(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the message ‘ATP Down Alarm’." + Environment.NewLine +
                                "2. The ‘Alarm’ sound is played." + Environment.NewLine +
                                "3. DMI does not display the track condition symbols.");

            TraceHeader("Test Step 8");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("End of test");
            
            /*
            Test Step 8
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
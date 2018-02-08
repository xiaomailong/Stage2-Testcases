using System;
using System.Collections.Generic;
using Testcase.Telegrams.EVCtoDMI;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 22.11 Handle at least 31 PA Speed Profile Segments
    /// TC-ID: 17.11
    /// 
    /// This test case verifies that the DMI can handle at least 31 PA Speed Profile segments. Also verify that the PA Speed Profile is continuously updated the value and the speed profile received from Pkt27 is applied at the correct position. This function shall comply with [MMI-ETCS-gen].
    /// 
    /// Tested Requirements:
    /// MMI_gen 7286;
    /// 
    /// Scenario:
    /// Activate cabin A. Driver performs Start of Mission to SR mode, Level 1Start driving to pass BG1 at 10m. which contains pkt 12, pkt21 and pkt 27 with 15 PA Speed ProfilesPass BG2 at 100 m which contains pkt 27 with 16 PA Speed ProfilesVerify thhat DMI shall handle at least 31 PA  speed profile segments
    /// 
    /// Used files:
    /// 17_11.tdg
    /// </summary>
    public class TC_ID_17_11_Handle_at_least_31_PA_Speed_Profile_Segments : TestcaseBase
    {
        public override bool TestcaseEntryPoint()
        {
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 24214;
            // Testcase entrypoint

            MakeTestStepHeader(1, UniqueIdentifier++, "Activate cabin A",
                "DMI displays the default window. The Driver ID window is displayed");
            /*
            Test Step 1
            Action: Activate cabin A
            Expected Result: DMI displays the default window. The Driver ID window is displayed
            */
            StartUp();
            DmiActions.Set_Driver_ID(this, "1234");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in SB mode." + Environment.NewLine +
                                "2. The Driver ID window is displayed.");

            MakeTestStepHeader(2, UniqueIdentifier++, "Driver performs SoM to SR mode, level 1",
                "DMI is displayed in SR mode, level 1");
            /*
            Test Step 2
            Action: Driver performs SoM to SR mode, level 1
            Expected Result: DMI is displayed in SR mode, level 1
            */
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Level = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_LEVEL.L1;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode =
                EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.StaffResponsible;
            DmiActions.Finished_SoM_Default_Window(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in SR mode, Level 1.");

            MakeTestStepHeader(3, UniqueIdentifier++,
                "Drive the train forward with 40 km/h then pass BG1.Pkt 12 : L_ENDSECTION = 30000, V_MAIN = 40 (200 Km/h)Pkt 27 giving 16 segments of static speed profileD_STATIC = 0, V_STATIC = 8N_ITER = 151.      D_STATIC = 500, V_STATIC = 92.      D_STATIC = 500, V_STATIC = 103.      D_STATIC = 500, V_STATIC = 114.      D_STATIC = 500, V_STATIC = 125.      D_STATIC = 500, V_STATIC = 136.      D_STATIC = 500, V_STATIC = 147.      D_STATIC = 500, V_STATIC = 158.      D_STATIC = 500, V_STATIC = 169.      D_STATIC = 500, V_STATIC = 1710.     D_STATIC = 500, V_STATIC = 1811.     D_STATIC = 500, V_STATIC = 1912.     D_STATIC = 500, V_STATIC = 2013.     D_STATIC = 500, V_STATIC = 2114.     D_STATIC = 500, V_STATIC = 22   15.    D_STATIC = 500, V_STATIC = 23",
                "DMI changes from SR mode to FS mode.The planning area is displayed the PA Speed Profile segments");
            /*
            Test Step 3
            Action: Drive the train forward with 40 km/h then pass BG1.Pkt 12 : L_ENDSECTION = 30000, V_MAIN = 40 (200 Km/h)Pkt 27 giving 16 segments of static speed profileD_STATIC = 0, V_STATIC = 8N_ITER = 151.      D_STATIC = 500, V_STATIC = 92.      D_STATIC = 500, V_STATIC = 103.      D_STATIC = 500, V_STATIC = 114.      D_STATIC = 500, V_STATIC = 125.      D_STATIC = 500, V_STATIC = 136.      D_STATIC = 500, V_STATIC = 147.      D_STATIC = 500, V_STATIC = 158.      D_STATIC = 500, V_STATIC = 169.      D_STATIC = 500, V_STATIC = 1710.     D_STATIC = 500, V_STATIC = 1811.     D_STATIC = 500, V_STATIC = 1912.     D_STATIC = 500, V_STATIC = 2013.     D_STATIC = 500, V_STATIC = 2114.     D_STATIC = 500, V_STATIC = 22   15.    D_STATIC = 500, V_STATIC = 23
            Expected Result: DMI changes from SR mode to FS mode.The planning area is displayed the PA Speed Profile segments
            Test Step Comment: MMI_gen 7286 (partly: PL21);
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 30;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.FullSupervision;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN = 10000;
            EVC4_MMITrackDescription.MMI_G_GRADIENT_CURR = 0;
            EVC4_MMITrackDescription.MMI_V_MRSP_CURR_KMH = 30;

            List<TrackDescription> trackDescriptions = new List<TrackDescription>
            {
                new TrackDescription {MMI_V_MRSP_KMH = 45, MMI_O_MRSP = 60000},
                new TrackDescription {MMI_V_MRSP_KMH = 50, MMI_O_MRSP = 110000},
                new TrackDescription {MMI_V_MRSP_KMH = 55, MMI_O_MRSP = 160000},
                new TrackDescription {MMI_V_MRSP_KMH = 60, MMI_O_MRSP = 210000},
                new TrackDescription {MMI_V_MRSP_KMH = 65, MMI_O_MRSP = 26000},
                new TrackDescription {MMI_V_MRSP_KMH = 70, MMI_O_MRSP = 310000},
                new TrackDescription {MMI_V_MRSP_KMH = 75, MMI_O_MRSP = 360000},
                new TrackDescription {MMI_V_MRSP_KMH = 80, MMI_O_MRSP = 410000},
                new TrackDescription {MMI_V_MRSP_KMH = 85, MMI_O_MRSP = 460000},
                new TrackDescription {MMI_V_MRSP_KMH = 90, MMI_O_MRSP = 510000},
                new TrackDescription {MMI_V_MRSP_KMH = 95, MMI_O_MRSP = 560000},
                new TrackDescription {MMI_V_MRSP_KMH = 100, MMI_O_MRSP = 610000},
                new TrackDescription {MMI_V_MRSP_KMH = 105, MMI_O_MRSP = 660000},
                new TrackDescription {MMI_V_MRSP_KMH = 110, MMI_O_MRSP = 710000},
                new TrackDescription {MMI_V_MRSP_KMH = 115, MMI_O_MRSP = 760000},
                new TrackDescription {MMI_V_MRSP_KMH = 120, MMI_O_MRSP = 810000}
            };
            EVC4_MMITrackDescription.TrackDescriptions = trackDescriptions;
            EVC4_MMITrackDescription.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI changes from SR to FS mode, Level 1." + Environment.NewLine +
                                "2. The Planning Area is displayed in area D." + Environment.NewLine +
                                "3. Speed discontinuity increases, symbol PL21, are displayed at positions 500, 100, 1500, 2000, 2500, 3000, 3500 and 4000m.");

            MakeTestStepHeader(4, UniqueIdentifier++,
                "Pass BG2 Pkt 27 giving 16 segments of static speed profileD_STATIC = 7900, V_STATIC = 24N_ITER = 1516.     D_STATIC = 500, V_STATIC = 2517.     D_STATIC = 500, V_STATIC = 2618.     D_STATIC = 500, V_STATIC = 2719.     D_STATIC = 500, V_STATIC = 2820.     D_STATIC = 500, V_STATIC = 2921.     D_STATIC = 500, V_STATIC = 3022.     D_STATIC = 500, V_STATIC = 2923.     D_STATIC = 500, V_STATIC = 2824.     D_STATIC = 500, V_STATIC = 2725.     D_STATIC = 500, V_STATIC = 2626.     D_STATIC = 500, V_STATIC = 2527.     D_STATIC = 500, V_STATIC = 2428.     D_STATIC = 500, V_STATIC = 2329.     D_STATIC = 500, V_STATIC = 22  30.     D_STATIC = 500, V_STATIC = 21",
                "The planning area keep showing PA Speed Profile segments");
            /*
            Test Step 4
            Action: Pass BG2 Pkt 27 giving 16 segments of static speed profileD_STATIC = 7900, V_STATIC = 24N_ITER = 1516.     D_STATIC = 500, V_STATIC = 2517.     D_STATIC = 500, V_STATIC = 2618.     D_STATIC = 500, V_STATIC = 2719.     D_STATIC = 500, V_STATIC = 2820.     D_STATIC = 500, V_STATIC = 2921.     D_STATIC = 500, V_STATIC = 3022.     D_STATIC = 500, V_STATIC = 2923.     D_STATIC = 500, V_STATIC = 2824.     D_STATIC = 500, V_STATIC = 2725.     D_STATIC = 500, V_STATIC = 2626.     D_STATIC = 500, V_STATIC = 2527.     D_STATIC = 500, V_STATIC = 2428.     D_STATIC = 500, V_STATIC = 2329.     D_STATIC = 500, V_STATIC = 22  30.     D_STATIC = 500, V_STATIC = 21
            // D_STATIC = 7900, V_STATIC = 24N_ITER = 1516.     D_STATIC = 500, V_STATIC = 2517
		    Expected Result: The planning area keep showing PA Speed Profile segments
            */
            trackDescriptions.AddRange(new List<TrackDescription>
            {
                new TrackDescription {MMI_V_MRSP_KMH = 125, MMI_O_MRSP = 860000},
                new TrackDescription {MMI_V_MRSP_KMH = 130, MMI_O_MRSP = 910000},
                new TrackDescription {MMI_V_MRSP_KMH = 135, MMI_O_MRSP = 960000},
                new TrackDescription {MMI_V_MRSP_KMH = 140, MMI_O_MRSP = 1100000},
                new TrackDescription {MMI_V_MRSP_KMH = 145, MMI_O_MRSP = 1160000},
                new TrackDescription {MMI_V_MRSP_KMH = 150, MMI_O_MRSP = 1210000},
                new TrackDescription {MMI_V_MRSP_KMH = 145, MMI_O_MRSP = 1260000},
                new TrackDescription {MMI_V_MRSP_KMH = 140, MMI_O_MRSP = 1310000},
                new TrackDescription {MMI_V_MRSP_KMH = 135, MMI_O_MRSP = 1360000},
                new TrackDescription {MMI_V_MRSP_KMH = 130, MMI_O_MRSP = 1410000},
                new TrackDescription {MMI_V_MRSP_KMH = 125, MMI_O_MRSP = 1460000},
                new TrackDescription {MMI_V_MRSP_KMH = 120, MMI_O_MRSP = 1510000},
                new TrackDescription {MMI_V_MRSP_KMH = 115, MMI_O_MRSP = 1560000},
                new TrackDescription {MMI_V_MRSP_KMH = 110, MMI_O_MRSP = 1610000},
                new TrackDescription {MMI_V_MRSP_KMH = 105, MMI_O_MRSP = 1660000}
            });

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The Planning Area is still displayed in area D." + Environment.NewLine +
                                "2. Speed discontinuity increases, symbol PL21, are still displayed at positions 500, 100, 1500, 2000, 2500, 3000, 3500 and 4000m.");

            MakeTestStepHeader(5, UniqueIdentifier++, "Drive the train follow the permitted speed",
                "From step 6 to Step 27. Verify that the PA Speed Profile segment’s speed is higher than the the speed of the previous segment. DMI is displayed as symbol PL21 on the planning area. (see the figure in ‘Comment’ column)");
            /*
            Test Step 5
            Action: Drive the train follow the permitted speed
            Expected Result: From step 6 to Step 27. Verify that the PA Speed Profile segment’s speed is higher than the the speed of the previous segment. DMI is displayed as symbol PL21 on the planning area. (see the figure in ‘Comment’ column)
            */
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN = 15000;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Each successive speed discontinuity increases in speed from the previous.");
            MakeTestStepHeader(6, UniqueIdentifier++, "SSP in Pkt27 of BG1 iteration 1 is supervised",
                "Verify that the CSG is indicated the speed equal 45km/h. The symbol PL21 is displayed on the planning area");
            /*
            Test Step 6
            Action: SSP in Pkt27 of BG1 iteration 1 is supervised
            Expected Result: Verify that the CSG is indicated the speed equal 45km/h. The symbol PL21 is displayed on the planning area
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 45;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN = 50000;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The speed increases to that of the nearest discontinuity." + Environment.NewLine +
                                "2. The speed increase symbol, PL21, is displayed in the Planning Area.");
            MakeTestStepHeader(7, UniqueIdentifier++, "SSP in Pkt27 of BG1 iteration 2 is supervised",
                "Verify that the CSG is indicated the speed equal 50km/h. The symbol PL21 is displayed on the planning area");
            /*
            Test Step 7
            Action: SSP in Pkt27 of BG1 iteration 2 is supervised
            Expected Result: Verify that the CSG is indicated the speed equal 50km/h. The symbol PL21 is displayed on the planning area
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH += 5;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN += 50000;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The speed increases to that of the nearest discontinuity." + Environment.NewLine +
                                "2. The speed increase symbol, PL21, is displayed in the Planning Area.");
            MakeTestStepHeader(8, UniqueIdentifier++, "SSP in Pkt27 of BG1 iteration 3 is supervised",
                "Verify that the CSG is indicated the speed equal 55km/h. The symbol PL21 is displayed on the planning area");
            /*
            Test Step 8
            Action: SSP in Pkt27 of BG1 iteration 3 is supervised
            Expected Result: Verify that the CSG is indicated the speed equal 55km/h. The symbol PL21 is displayed on the planning area
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH += 5;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN += 50000;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The speed increases to that of the nearest discontinuity." + Environment.NewLine +
                                "2. The speed increase symbol, PL21, is displayed in the Planning Area.");

            MakeTestStepHeader(9, UniqueIdentifier++, "SSP in Pkt27 of BG1 iteration 4 is supervised",
                "Verify that the CSG is indicated the speed equal 60km/h. The symbol PL21 is displayed on the planning area");
            /*
            Test Step 9
            Action: SSP in Pkt27 of BG1 iteration 4 is supervised
            Expected Result: Verify that the CSG is indicated the speed equal 60km/h. The symbol PL21 is displayed on the planning area
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH += 5;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN += 50000;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The speed increases to that of the nearest discontinuity." + Environment.NewLine +
                                "2. The speed increase symbol, PL21, is displayed in the Planning Area.");

            MakeTestStepHeader(10, UniqueIdentifier++, "SSP in Pkt27 of BG1 iteration 5 is supervised",
                "Verify that the CSG is indicated the speed equal 65km/h. The symbol PL21 is displayed on the planning area");
            /*
            Test Step 10
            Action: SSP in Pkt27 of BG1 iteration 5 is supervised
            Expected Result: Verify that the CSG is indicated the speed equal 65km/h. The symbol PL21 is displayed on the planning area
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH += 5;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN += 50000;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The speed increases to that of the nearest discontinuity." + Environment.NewLine +
                                "2. The speed increase symbol, PL21, is displayed in the Planning Area.");

            MakeTestStepHeader(11, UniqueIdentifier++, "SSP in Pkt27 of BG1 iteration 6 is supervised",
                "Verify that the CSG is indicated the speed equal 70km/h. The symbol PL21 is displayed on the planning area");
            /*
            Test Step 11
            Action: SSP in Pkt27 of BG1 iteration 6 is supervised
            Expected Result: Verify that the CSG is indicated the speed equal 70km/h. The symbol PL21 is displayed on the planning area
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH += 5;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN += 50000;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The speed increases to that of the nearest discontinuity." + Environment.NewLine +
                                "2. The speed increase symbol, PL21, is displayed in the Planning Area.");

            MakeTestStepHeader(12, UniqueIdentifier++, "SSP in Pkt27 of BG1 iteration 7 is supervised",
                "Verify that the CSG is indicated the speed equal 75km/h. The symbol PL21 is displayed on the planning area");
            /*
            Test Step 12
            Action: SSP in Pkt27 of BG1 iteration 7 is supervised
            Expected Result: Verify that the CSG is indicated the speed equal 75km/h. The symbol PL21 is displayed on the planning area
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH += 5;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN += 50000;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The speed increases to that of the nearest discontinuity." + Environment.NewLine +
                                "2. The speed increase symbol, PL21, is displayed in the Planning Area.");

            MakeTestStepHeader(13, UniqueIdentifier++, "SSP in Pkt27 of BG1 iteration 8 is supervised",
                "Verify that the CSG is indicated the speed equal 80km/h. The symbol PL21 is displayed on the planning area");
            /*
            Test Step 13
            Action: SSP in Pkt27 of BG1 iteration 8 is supervised
            Expected Result: Verify that the CSG is indicated the speed equal 80km/h. The symbol PL21 is displayed on the planning area
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH += 5;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN += 50000;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The speed increases to that of the nearest discontinuity." + Environment.NewLine +
                                "2. The speed increase symbol, PL21, is displayed in the Planning Area.");

            MakeTestStepHeader(14, UniqueIdentifier++, "SSP in Pkt27 of BG1 iteration 9 is supervised",
                "Verify that the CSG is indicated the speed equal 85km/h. The symbol PL21 is displayed on the planning area");
            /*
            Test Step 14
            Action: SSP in Pkt27 of BG1 iteration 9 is supervised
            Expected Result: Verify that the CSG is indicated the speed equal 85km/h. The symbol PL21 is displayed on the planning area
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH += 5;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN += 50000;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The speed increases to that of the nearest discontinuity." + Environment.NewLine +
                                "2. The speed increase symbol, PL21, is displayed in the Planning Area.");

            MakeTestStepHeader(15, UniqueIdentifier++, "SSP in Pkt27 of BG1 iteration 10 is supervised",
                "Verify that the CSG is indicated the speed equal 90km/h. The symbol PL21 is displayed on the planning area");
            /*
            Test Step 15
            Action: SSP in Pkt27 of BG1 iteration 10 is supervised
            Expected Result: Verify that the CSG is indicated the speed equal 90km/h. The symbol PL21 is displayed on the planning area
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH += 5;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN += 50000;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The speed increases to that of the nearest discontinuity." + Environment.NewLine +
                                "2. The speed increase symbol, PL21, is displayed in the Planning Area.");

            MakeTestStepHeader(16, UniqueIdentifier++, "SSP in Pkt27 of BG1 iteration 11 is supervised",
                "Verify that the CSG is indicated the speed equal 95km/h. The symbol PL21 is displayed on the planning area");
            /*
            Test Step 16
            Action: SSP in Pkt27 of BG1 iteration 11 is supervised
            Expected Result: Verify that the CSG is indicated the speed equal 95km/h. The symbol PL21 is displayed on the planning area
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH += 5;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN += 50000;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The speed increases to that of the nearest discontinuity." + Environment.NewLine +
                                "2. The speed increase symbol, PL21, is displayed in the Planning Area.");

            MakeTestStepHeader(17, UniqueIdentifier++, "SSP in Pkt27 of BG1 iteration 12 is supervised",
                "Verify that the CSG is indicated the speed equal 100km/h. The symbol PL21 is displayed on the planning area");
            /*
            Test Step 17
            Action: SSP in Pkt27 of BG1 iteration 12 is supervised
            Expected Result: Verify that the CSG is indicated the speed equal 100km/h. The symbol PL21 is displayed on the planning area
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH += 5;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN += 50000;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The speed increases to that of the nearest discontinuity." + Environment.NewLine +
                                "2. The speed increase symbol, PL21, is displayed in the Planning Area.");

            MakeTestStepHeader(18, UniqueIdentifier++, "SSP in Pkt27 of BG1 iteration 13 is supervised",
                "Verify that the CSG is indicated the speed equal 105km/h. The symbol PL21 is displayed on the planning area");
            /*
            Test Step 18
            Action: SSP in Pkt27 of BG1 iteration 13 is supervised
            Expected Result: Verify that the CSG is indicated the speed equal 105km/h. The symbol PL21 is displayed on the planning area
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH += 5;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN += 50000;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The speed increases to that of the nearest discontinuity." + Environment.NewLine +
                                "2. The speed increase symbol, PL21, is displayed in the Planning Area.");

            MakeTestStepHeader(19, UniqueIdentifier++, "SSP in Pkt27 of BG1 iteration 14 is supervised",
                "Verify that the CSG is indicated the speed equal 110km/h. The symbol PL21 is displayed on the planning area");
            /*
            Test Step 19
            Action: SSP in Pkt27 of BG1 iteration 14 is supervised
            Expected Result: Verify that the CSG is indicated the speed equal 110km/h. The symbol PL21 is displayed on the planning area
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH += 5;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN += 50000;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The speed increases to that of the nearest discontinuity." + Environment.NewLine +
                                "2. The speed increase symbol, PL21, is displayed in the Planning Area.");

            MakeTestStepHeader(20, UniqueIdentifier++, "SSP in Pkt27 of BG1 iteration 15 is supervised",
                "Verify that the CSG is indicated the speed equal 115km/h. The symbol PL21 is displayed on the planning area");
            /*
            Test Step 20
            Action: SSP in Pkt27 of BG1 iteration 15 is supervised
            Expected Result: Verify that the CSG is indicated the speed equal 115km/h. The symbol PL21 is displayed on the planning area
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH += 5;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN += 50000;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The speed increases to that of the nearest discontinuity." + Environment.NewLine +
                                "2. The speed increase symbol, PL21, is displayed in the Planning Area.");

            MakeTestStepHeader(21, UniqueIdentifier++, "SSP in Pkt27 of BG2 iteration 16 is supervised",
                "Verify that the CSG is indicated the speed equal 120km/h. The symbol PL21 is displayed on the planning area");
            /*
            Test Step 21
            Action: SSP in Pkt27 of BG2 iteration 16 is supervised
            Expected Result: Verify that the CSG is indicated the speed equal 120km/h. The symbol PL21 is displayed on the planning area
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH += 5;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN += 50000;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The speed increases to that of the nearest discontinuity." + Environment.NewLine +
                                "2. The speed increase symbol, PL21, is displayed in the Planning Area.");

            MakeTestStepHeader(22, UniqueIdentifier++, "SSP in Pkt27 of BG2 iteration 17 is supervised",
                "Verify that the CSG is indicated the speed equal 125km/h. The symbol PL21 is displayed on the planning area");
            /*
            Test Step 22
            Action: SSP in Pkt27 of BG2 iteration 17 is supervised
            Expected Result: Verify that the CSG is indicated the speed equal 125km/h. The symbol PL21 is displayed on the planning area
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH += 5;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN += 50000;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The speed increases to that of the nearest discontinuity." + Environment.NewLine +
                                "2. The speed increase symbol, PL21, is displayed in the Planning Area.");

            MakeTestStepHeader(23, UniqueIdentifier++, "SSP in Pkt27 of BG2 iteration 18 is supervised",
                "Verify that the CSG is indicated the speed equal 130km/h. The symbol PL21 is displayed on the planning area");
            /*
            Test Step 23
            Action: SSP in Pkt27 of BG2 iteration 18 is supervised
            Expected Result: Verify that the CSG is indicated the speed equal 130km/h. The symbol PL21 is displayed on the planning area
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH += 5;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN += 50000;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The speed increases to that of the nearest discontinuity." + Environment.NewLine +
                                "2. The speed increase symbol, PL21, is displayed in the Planning Area.");

            MakeTestStepHeader(24, UniqueIdentifier++, "SSP in Pkt27 of BG2 iteration 19 is supervised",
                "Verify that the CSG is indicated the speed equal 135km/h. The symbol PL21 is displayed on the planning area");
            /*
            Test Step 24
            Action: SSP in Pkt27 of BG2 iteration 19 is supervised
            Expected Result: Verify that the CSG is indicated the speed equal 135km/h. The symbol PL21 is displayed on the planning area
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH += 5;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN += 50000;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The speed increases to that of the nearest discontinuity." + Environment.NewLine +
                                "2. The speed increase symbol, PL21, is displayed in the Planning Area.");

            MakeTestStepHeader(25, UniqueIdentifier++, "SSP in Pkt27 of BG2 iteration 20 is supervised",
                "Verify that the CSG is indicated the speed equal 140km/h. The symbol PL21 is displayed on the planning area");
            /*
            Test Step 25
            Action: SSP in Pkt27 of BG2 iteration 20 is supervised
            Expected Result: Verify that the CSG is indicated the speed equal 140km/h. The symbol PL21 is displayed on the planning area
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH += 5;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN += 50000;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The speed increases to that of the nearest discontinuity." + Environment.NewLine +
                                "2. The speed increase symbol, PL21, is displayed in the Planning Area.");

            MakeTestStepHeader(26, UniqueIdentifier++, "SSP in Pkt27 of BG2 iteration 21 is supervised",
                "Verify that the CSG is indicated the speed equal 145km/h. The symbol PL21 is displayed on the planning area");
            /*
            Test Step 26
            Action: SSP in Pkt27 of BG2 iteration 21 is supervised
            Expected Result: Verify that the CSG is indicated the speed equal 145km/h. The symbol PL21 is displayed on the planning area
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH += 5;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN += 50000;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The speed increases to that of the nearest discontinuity." + Environment.NewLine +
                                "2. The speed increase symbol, PL21, is displayed in the Planning Area.");

            MakeTestStepHeader(27, UniqueIdentifier++, "SSP in Pkt27 of BG2 iteration 22 is supervised",
                "Verify that the CSG is indicated the speed equal 150km/h. The symbol PL21 is displayed on the planning area");
            /*
            Test Step 27
            Action: SSP in Pkt27 of BG2 iteration 22 is supervised
            Expected Result: Verify that the CSG is indicated the speed equal 150km/h. The symbol PL21 is displayed on the planning area
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH += 5;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN += 50000;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The speed increases to that of the nearest discontinuity." + Environment.NewLine +
                                "2. The speed increase symbol, PL21, is displayed in the Planning Area.");

            MakeTestStepHeader(28, UniqueIdentifier++, "SSP in Pkt27 of BG2 iteration 23 is supervised",
                "From step 28 to Step 41. Verify that the PA Speed Profile segment’s speed is lower than the the speed of the previous segment. DMI is displayed as symbol PL22 on the planning areaVerify that the CSG is indicated the speed equal 145km/h. The symbol PL22 is displayed on the planning area");
            /*
            Test Step 28
            Action: SSP in Pkt27 of BG2 iteration 23 is supervised
            Expected Result: From step 28 to Step 41. Verify that the PA Speed Profile segment’s speed is lower than the the speed of the previous segment. DMI is displayed as symbol PL22 on the planning areaVerify that the CSG is indicated the speed equal 145km/h. The symbol PL22 is displayed on the planning area
            Test Step Comment: MMI_gen 7286 (partly: PL22);
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH -= 5;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN += 50000;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The speed decreases to that of the nearest discontinuity." + Environment.NewLine +
                                "2. The speed decrease symbol, PL22, is displayed in the Planning Area.");

            MakeTestStepHeader(29, UniqueIdentifier++, "SSP in Pkt27 of BG2 iteration 24 is supervised",
                "Verify that the CSG is indicated the speed equal 140km/h. The symbol PL22 is displayed on the planning area");
            /*
            Test Step 29
            Action: SSP in Pkt27 of BG2 iteration 24 is supervised
            Expected Result: Verify that the CSG is indicated the speed equal 140km/h. The symbol PL22 is displayed on the planning area
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH -= 5;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN += 50000;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The speed decreases to that of the nearest discontinuity." + Environment.NewLine +
                                "2. The speed decrease symbol, PL22, is displayed in the Planning Area.");

            MakeTestStepHeader(30, UniqueIdentifier++, "SSP in Pkt27 of BG2 iteration 25 is supervised",
                "Verify that the CSG is indicated the speed equal 135km/h. The symbol PL22 is displayed on the planning area");
            /*
            Test Step 30
            Action: SSP in Pkt27 of BG2 iteration 25 is supervised
            Expected Result: Verify that the CSG is indicated the speed equal 135km/h. The symbol PL22 is displayed on the planning area
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH -= 5;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN += 50000;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The speed decreases to that of the nearest discontinuity." + Environment.NewLine +
                                "2. The speed decrease symbol, PL22, is displayed in the Planning Area.");

            MakeTestStepHeader(31, UniqueIdentifier++, "SSP in Pkt27 of BG2 iteration 26 is supervised",
                "Verify that the CSG is indicated the speed equal 130km/h. The symbol PL22 is displayed on the planning area");
            /*
            Test Step 31
            Action: SSP in Pkt27 of BG2 iteration 26 is supervised
            Expected Result: Verify that the CSG is indicated the speed equal 130km/h. The symbol PL22 is displayed on the planning area
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH -= 5;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN += 50000;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The speed decreases to that of the nearest discontinuity." + Environment.NewLine +
                                "2. The speed decrease symbol, PL22, is displayed in the Planning Area.");

            MakeTestStepHeader(32, UniqueIdentifier++, "SSP in Pkt27 of BG2 iteration 27 is supervised",
                "Verify that the CSG is indicated the speed equal 125km/h. The symbol PL22 is displayed on the planning area");
            /*
            Test Step 32
            Action: SSP in Pkt27 of BG2 iteration 27 is supervised
            Expected Result: Verify that the CSG is indicated the speed equal 125km/h. The symbol PL22 is displayed on the planning area
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH -= 5;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN += 50000;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The speed decreases to that of the nearest discontinuity." + Environment.NewLine +
                                "2. The speed decrease symbol, PL22, is displayed in the Planning Area.");

            MakeTestStepHeader(33, UniqueIdentifier++, "SSP in Pkt27 of BG2 iteration 28 is supervised",
                "Verify that the CSG is indicated the speed equal 120km/h. The symbol PL22 is displayed on the planning area");
            /*
            Test Step 33
            Action: SSP in Pkt27 of BG2 iteration 28 is supervised
            Expected Result: Verify that the CSG is indicated the speed equal 120km/h. The symbol PL22 is displayed on the planning area
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH -= 5;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN += 50000;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The speed decreases to that of the nearest discontinuity." + Environment.NewLine +
                                "2. The speed decrease symbol, PL22, is displayed in the Planning Area.");

            MakeTestStepHeader(34, UniqueIdentifier++, "SSP in Pkt27 of BG2 iteration 29 is supervised",
                "Verify that the CSG is indicated the speed equal 115km/h. The symbol PL22 is displayed on the planning area");
            /*
            Test Step 34
            Action: SSP in Pkt27 of BG2 iteration 29 is supervised
            Expected Result: Verify that the CSG is indicated the speed equal 115km/h. The symbol PL22 is displayed on the planning area
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH -= 5;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN += 50000;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The speed decreases to that of the nearest discontinuity." + Environment.NewLine +
                                "2. The speed decrease symbol, PL22, is displayed in the Planning Area.");

            MakeTestStepHeader(35, UniqueIdentifier++, "SSP in Pkt27 of BG2 iteration 30 is supervised",
                "Verify that the CSG is indicated the speed equal 110km/h. The symbol PL22 is displayed on the planning area");
            /*
            Test Step 35
            Action: SSP in Pkt27 of BG2 iteration 30 is supervised
            Expected Result: Verify that the CSG is indicated the speed equal 110km/h. The symbol PL22 is displayed on the planning area
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH -= 5;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN += 50000;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The speed decreases to that of the nearest discontinuity." + Environment.NewLine +
                                "2. The speed decrease symbol, PL22, is displayed in the Planning Area.");

            MakeTestStepHeader(36, UniqueIdentifier++, "SSP in Pkt27 of BG2 iteration 31 is supervised",
                "Verify that the CSG is indicated the speed equal 105 km/h. The symbol PL22 is displayed on the planning area");
            /*
            Test Step 36
            Action: SSP in Pkt27 of BG2 iteration 31 is supervised
            Expected Result: Verify that the CSG is indicated the speed equal 105 km/h. The symbol PL22 is displayed on the planning area
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH -= 5;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN += 50000;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The speed decreases to that of the nearest discontinuity." + Environment.NewLine +
                                "2. The speed decrease symbol, PL22, is displayed in the Planning Area.");

            MakeTestStepHeader(37, UniqueIdentifier++, "Continue to drive the train forward",
                "Verify that The symbol PL23 is displayed on the planning area");
            /*
            Test Step 37
            Action: Continue to drive the train forward
            Expected Result: Verify that The symbol PL23 is displayed on the planning area
            Test Step Comment: MMI_gen 7286 (partly: PL23);
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH -= 5;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN += 50000;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The speed decrease to zero target symbol, PL23, is displayed in the Planning Area.");

            MakeTestStepHeader(38, UniqueIdentifier++, "End of test", "");

            /*
            Test Step 38
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
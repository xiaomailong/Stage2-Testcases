using System;
using System.Collections.Generic;
using Testcase.Telegrams.EVCtoDMI;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 22.4.17 PA Track Condition: First symbol prevails over the next coming symbol
    /// TC-ID: 17.4.17
    /// 
    /// This test case is to verify that if two or more PA Track condition symbols located closely. The first coming symbol shall display in the foreground of the next coming symbol.
    /// 
    /// Tested Requirements:
    /// MMI_gen 1417;
    /// 
    /// Scenario:
    /// 1.Power on test system and activate cabin.
    /// 2.Perform Start of Mission to L1, SR mode
    /// 3.Pass BG0 with MA and Track desciption
    /// 4.Mode changes to FS mode 
    /// 5.Pass BG1 with containing pkt68 (Track condtion)M_TRACKCOND = 0 (Non Stopping area) M_TRACKCOND = 2 (Sound Horn) M_TRACKCOND = 4 (Radio Hole)  M_TRACKCOND = 0 (Non Stopping area)      
    /// 6.Verify the Track condition symbos display on area D2, D3 and D4
    /// 
    /// Used files:
    /// 17_4_17.tdg
    /// </summary>
    public class TC_ID_17_4_17_PA_Track_Condition_First_symbol_prevails_over_the_next_coming_symbol : TestcaseBase
    {
        public override bool TestcaseEntryPoint()
        {
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 0;
            // Testcase entrypoint
            TraceInfo("This test case requires an ATP configuration change - " +
                      "See Precondition requirements. If this is not done manually, the test may fail!");

            MakeTestStepHeader(1, UniqueIdentifier++, "Power on the system and activate cabin",
                "DMI displays in SB mode");
            /*
            Test Step 1
            Action: Power on the system and activate cabin
            Expected Result: DMI displays in SB mode
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, "Power on the system");
            DmiActions.Start_ATP();
            DmiActions.Activate_Cabin_1(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in SB mode");

            MakeTestStepHeader(2, UniqueIdentifier++, "Perform SoM to L1, SR mode", "Mode changes to SR mode , L1");
            /*
            Test Step 2
            Action: Perform SoM to L1, SR mode
            Expected Result: Mode changes to SR mode , L1
            */
            // Testing to SR mode is done elsewhere: force...
            DmiActions.Set_Driver_ID(this, "1234");
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Level = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_LEVEL.L1;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode =
                EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.StaffResponsible;
            DmiActions.Finished_SoM_Default_Window(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in SR mode, Level 1.");

            MakeTestStepHeader(3, UniqueIdentifier++, "Drive the train up to 20 km/h",
                "The speed pointer is indicated as 20  km/h");
            /*
            Test Step 3
            Action: Drive the train up to 20 km/h
            Expected Result: The speed pointer is indicated as 20  km/h
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 20;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The speed pointer is displayed with speed = 20 km/h.");

            MakeTestStepHeader(4, UniqueIdentifier++, "Pass BG0 with MA and Track descriptionPkt 12,21 and 27",
                "Mode changes to FS mode , Level 1");
            /*
            Test Step 4
            Action: Pass BG0 with MA and Track descriptionPkt 12,21 and 27
            Expected Result: Mode changes to FS mode , Level 1
            */
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.FullSupervision;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in FS mode, Level 1.");

            MakeTestStepHeader(5, UniqueIdentifier++,
                "Pass BG1 with 4Track conditions Pkt 68:D_TRACKCOND(1) = 400L_TRACKCOND(1) = 200M_TRACKCOND(1) = 0D_TRACKCOND(2) = 0L_TRACKCOND(2) = 200M_TRACKCOND(2) = 2D_TRACKCOND(3) = 5L_TRACKCOND(3) = 200M_TRACKCOND(3) = 4D_TRACKCOND(4) = 10L_TRACKCOND(4) = 200M_TRACKCOND(4) = 0",
                "Mode remins in FS mode");
            /*
            Test Step 5
            Action: Pass BG1 with 4Track conditions Pkt 68:D_TRACKCOND(1) = 400L_TRACKCOND(1) = 200M_TRACKCOND(1) = 0D_TRACKCOND(2) = 0L_TRACKCOND(2) = 200M_TRACKCOND(2) = 2D_TRACKCOND(3) = 5L_TRACKCOND(3) = 200M_TRACKCOND(3) = 4D_TRACKCOND(4) = 10L_TRACKCOND(4) = 200M_TRACKCOND(4) = 0
            Expected Result: Mode remins in FS mode
            */
            EVC32_MMITrackConditions.MMI_Q_TRACKCOND_UPDATE = 0;

            EVC32_MMITrackConditions.TrackConditions =
                new List<TrackCondition>
                {
                    /* 0 */
                    new TrackCondition
                    {
                        MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Non_Stopping_Area,
                        MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.AnnounceArea,
                        MMI_Q_TRACKCOND_ACTION_START = Variables.MMI_Q_TRACKCOND_ACTION.WithoutDriverAction
                    },
                    /* 1 */
                    new TrackCondition
                    {
                        MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Sound_Horn,
                        MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.AnnounceArea,
                        MMI_Q_TRACKCOND_ACTION_START = Variables.MMI_Q_TRACKCOND_ACTION.WithoutDriverAction
                    },
                    /* 2 */
                    new TrackCondition
                    {
                        MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Radio_hole,
                        MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.AnnounceArea,
                        MMI_Q_TRACKCOND_ACTION_START = Variables.MMI_Q_TRACKCOND_ACTION.WithoutDriverAction
                    },
                    /* 3 */
                    new TrackCondition
                    {
                        MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Non_Stopping_Area,
                        MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.AnnounceArea,
                        MMI_Q_TRACKCOND_ACTION_START = Variables.MMI_Q_TRACKCOND_ACTION.WithoutDriverAction
                    }
                };

            EVC32_MMITrackConditions.Send();

            MakeTestStepHeader(6, UniqueIdentifier++, "Continue the train speed at 20 km/h",
                "Verify the following informationDMI displays Track condition symbol “ Non-stopping area” over “ Sound horn”");
            /*
            Test Step 6
            Action: Continue the train speed at 20 km/h
            Expected Result: Verify the following informationDMI displays Track condition symbol “ Non-stopping area” over “ Sound horn”
            Test Step Comment: MMI_gen 1417;
            */
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays track condition symbol ‘Non-stopping area’, TC10, over symbol ‘Non-stopping area’, TC35.");

            MakeTestStepHeader(7, UniqueIdentifier++, "End of test", "");

            /*
            Test Step 7
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
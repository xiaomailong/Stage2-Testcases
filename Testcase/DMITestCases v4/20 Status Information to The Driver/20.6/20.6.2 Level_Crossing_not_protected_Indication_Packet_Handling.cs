using System;
using System.Collections.Generic;
using Testcase.Telegrams.EVCtoDMI;

namespace Testcase.DMITestCases
{
    /// <summary>
    /// 20.6.2 Level Crossing “not protected” Indication: Packet Handling
    /// TC-ID: 15.6.2
    /// 
    /// This test case verifies the placment of objects in sub-area B3-B5 refer to received packet information EVC-33 (Level crossing not protected symbol) and EVC-32 (Track condition symbol) including with an internal storage to keep an information when all specified areas are in used.
    /// 
    /// Tested Requirements:
    /// MMI_gen 10482; MMI_gen 9499; MMI_gen 9500; MMI_gen 10480;
    /// 
    /// Scenario:
    /// Use the test script file to send packet information (both of EVC-33 and EVC-32). Then, verify the display information of LX01 symbol in sub-area B3-B
    /// 5.
    /// 
    /// Used files:
    /// 15_6_2_a.xml, 15_6_2_b.xml, 15_6_2_c.xml, 15_6_2_d.xml, 15_6_2_e.xml, 15_6_2_f.xml, 15_6_2_g.xml, 15_6_2_h.xml, 15_6_2_i.xml, 15_6_2_j.xml
    /// </summary>
    public class TC_ID_15_6_2_Level_Crossing_not_protected_Indication : TestcaseBase
    {
        public override bool TestcaseEntryPoint()
        {
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 23203;
            // Testcase entrypoint
            StartUp();
            DmiActions.Complete_SoM_L1_SR(this);

            MakeTestStepHeader(1, UniqueIdentifier++,
                "Use the test script file 15_6_2_a.xml to send EVC-33 withMMI_Q_TRACKCOND_STEP = 8MMI_M_TRACKCOND_TYPE = 16",
                "Verify the following information,There is no symbol display in sub-area B3-B5");
            /*
            Test Step 1
            Action: Use the test script file 15_6_2_a.xml to send EVC-33 withMMI_Q_TRACKCOND_STEP = 8MMI_M_TRACKCOND_TYPE = 16
            Expected Result: Verify the following information,There is no symbol display in sub-area B3-B5
            Test Step Comment: (1) MMI_gen 10482 (partly: invalid MMI_M_TRACKCOND_STEP);
            */

            EVC33_MMIAdditionalOrder.MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Level_Crossing;
            EVC33_MMIAdditionalOrder.MMI_NID_TRACKCOND = 0;
            EVC33_MMIAdditionalOrder.MMI_Q_TRACKCOND_ACTION = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction;
            EVC33_MMIAdditionalOrder.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.Spare8;
            EVC33_MMIAdditionalOrder.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI does not display a symbol in sub-areas B3-B5.");
            MakeTestStepHeader(2, UniqueIdentifier++,
                "Use the test script file 15_6_2_b.xml to send EVC-33 withMMI_Q_TRACKCOND_STEP = 1MMI_M_TRACKCOND_TYPE = 64",
                "Verify the following information,There is no symbol display in sub-area B3-B5");
            /*
            Test Step 2
            Action: Use the test script file 15_6_2_b.xml to send EVC-33 withMMI_Q_TRACKCOND_STEP = 1MMI_M_TRACKCOND_TYPE = 64
            Expected Result: Verify the following information,There is no symbol display in sub-area B3-B5
            Test Step Comment: (1) MMI_gen 10482 (partly: invalid MMI_M_TRACKCOND_TYPE);
            */

            EVC33_MMIAdditionalOrder.MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Invalid;
            EVC33_MMIAdditionalOrder.MMI_NID_TRACKCOND = 0;
            EVC33_MMIAdditionalOrder.MMI_Q_TRACKCOND_ACTION = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction;
            EVC33_MMIAdditionalOrder.MMI_Q_TRACKCOND_STEP =
                Variables.MMI_Q_TRACKCOND_STEP.AnnounceArea; // Announce area
            EVC33_MMIAdditionalOrder.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI does not display a symbol in sub-areas B3-B5.");

            MakeTestStepHeader(3, UniqueIdentifier++,
                "Use the test script file 15_6_2_c.xml to send EVC-33 withMMI_Q_TRACKCOND_STEP = 1MMI_M_TRACKCOND_TYPE = 16MMI_NID_TRACKCOND = 0",
                "Verify the following information,DMI displays LX01 symbol in sub-area B3");
            /*
            Test Step 3
            Action: Use the test script file 15_6_2_c.xml to send EVC-33 withMMI_Q_TRACKCOND_STEP = 1MMI_M_TRACKCOND_TYPE = 16MMI_NID_TRACKCOND = 0
            Expected Result: Verify the following information,DMI displays LX01 symbol in sub-area B3
            Test Step Comment: (1) MMI_gen 9499 (partly: B3); MMI_gen 9500 (partly: filling B3);
            */

            EVC33_MMIAdditionalOrder.MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Level_Crossing;
            EVC33_MMIAdditionalOrder.MMI_NID_TRACKCOND = 0;
            EVC33_MMIAdditionalOrder.MMI_Q_TRACKCOND_ACTION = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction;
            EVC33_MMIAdditionalOrder.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.AnnounceArea;
            EVC33_MMIAdditionalOrder.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays symbol LX01 in sub-area B3.");

            MakeTestStepHeader(4, UniqueIdentifier++,
                "Use the test script file 15_6_2_d.xml to send EVC-33 withMMI_Q_TRACKCOND_STEP = 4MMI_NID_TRACKCOND = 0",
                "The LX01 symbol is removed from sub-area B3");
            /*
            Test Step 4
            Action: Use the test script file 15_6_2_d.xml to send EVC-33 withMMI_Q_TRACKCOND_STEP = 4MMI_NID_TRACKCOND = 0
            Expected Result: The LX01 symbol is removed from sub-area B3
            */

            EVC33_MMIAdditionalOrder.MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Non_Stopping_Area;
            EVC33_MMIAdditionalOrder.MMI_NID_TRACKCOND = 0;
            EVC33_MMIAdditionalOrder.MMI_Q_TRACKCOND_ACTION = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction;
            EVC33_MMIAdditionalOrder.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.RemoveTC;
            EVC33_MMIAdditionalOrder.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI removes symbol LX01 from sub-area B3.");

            MakeTestStepHeader(5, UniqueIdentifier++,
                "Use the test script file 15_6_2_e.xml to send EVC-32 with,MMI_NID_TRACKCOND = 29MMI_Q_TRACKCOND_STEP = 1MMI_M_TRACKCOND_TYPE =3 Then, send EVC-33 with,MMI_Q_TRACKCOND_STEP = 1MMI_M_TRACKCOND_TYPE = 16MMI_NID_TRACKCOND = 0",
                "Verify the following information,After DMI displayed the TC03 symbol in sub-area B3 with yellow flashing frame, The LX01 symbol is display in sub-area B4");
            /*
            Test Step 5
            Action: Use the test script file 15_6_2_e.xml to send EVC-32 with,MMI_NID_TRACKCOND = 29MMI_Q_TRACKCOND_STEP = 1MMI_M_TRACKCOND_TYPE =3 Then, send EVC-33 with,MMI_Q_TRACKCOND_STEP = 1MMI_M_TRACKCOND_TYPE = 16MMI_NID_TRACKCOND = 0
            Expected Result: Verify the following information,After DMI displayed the TC03 symbol in sub-area B3 with yellow flashing frame, The LX01 symbol is display in sub-area B4
            Test Step Comment: (1) MMI_gen 9499 (partly: B4); MMI_gen 9500 (partly: left to right, filling B4, next area shall be used);
            */

            EVC32_MMITrackConditions.MMI_Q_TRACKCOND_UPDATE = 1;
            EVC32_MMITrackConditions.TrackConditions = new List<TrackCondition>
            {
                {
                    new TrackCondition
                    {
                        MMI_O_TRACKCOND_ANNOUNCE = 0,
                        MMI_O_TRACKCOND_START = 0,
                        MMI_O_TRACKCOND_END = 0,
                        MMI_NID_TRACKCOND = 29,
                        MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Pantograph,
                        MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.AnnounceArea,
                        MMI_Q_TRACKCOND_ACTION_START = 0,
                        MMI_Q_TRACKCOND_ACTION_END = 0
                    }
                }
            };

            EVC32_MMITrackConditions.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays symbol TC03 in sub-area B3 with a yellow flashing frame.");

            // Wait a few seconds
            Wait_Realtime(3000);

            EVC33_MMIAdditionalOrder.MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Level_Crossing;
            EVC33_MMIAdditionalOrder.MMI_NID_TRACKCOND = 0;
            EVC33_MMIAdditionalOrder.MMI_Q_TRACKCOND_ACTION = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction;
            EVC33_MMIAdditionalOrder.MMI_Q_TRACKCOND_STEP =
                Variables.MMI_Q_TRACKCOND_STEP.AnnounceArea; // Announce area
            EVC33_MMIAdditionalOrder.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. After 3s, DMI displays symbol LX01 in sub-area B4.");

            MakeTestStepHeader(6, UniqueIdentifier++,
                "Use the test script file 15_6_2_d.xml to send EVC-33 withMMI_Q_TRACKCOND_STEP = 4MMI_NID_TRACKCOND = 0",
                "The LX01 symbol is removed from sub-area B4");
            /*
            Test Step 6
            Action: Use the test script file 15_6_2_d.xml to send EVC-33 withMMI_Q_TRACKCOND_STEP = 4MMI_NID_TRACKCOND = 0
            Expected Result: The LX01 symbol is removed from sub-area B4
            */

            EVC33_MMIAdditionalOrder.MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Non_Stopping_Area;
            EVC33_MMIAdditionalOrder.MMI_NID_TRACKCOND = 0;
            EVC33_MMIAdditionalOrder.MMI_Q_TRACKCOND_ACTION = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction;
            EVC33_MMIAdditionalOrder.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.RemoveTC;
            EVC33_MMIAdditionalOrder.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI removes symbol LX01 from sub-area B4.");

            MakeTestStepHeader(7, UniqueIdentifier++,
                "Use the test script file 15_6_2_f.xml to send EVC-32 with,MMI_NID_TRACKCOND = 30MMI_Q_TRACKCOND_STEP = 1MMI_M_TRACKCOND_TYPE =3 MMI_Q_TRACKCOND_ACTION_START = 1Then, send EVC-33 with,MMI_Q_TRACKCOND_STEP = 1MMI_M_TRACKCOND_TYPE = 16MMI_NID_TRACKCOND = 0",
                "Verify the following information,After DMI displayed the TC02 symbol in sub-area B4, The LX01 symbol is display in sub-area B5");
            /*
            Test Step 7
            Action: Use the test script file 15_6_2_f.xml to send EVC-32 with,MMI_NID_TRACKCOND = 30MMI_Q_TRACKCOND_STEP = 1MMI_M_TRACKCOND_TYPE =3 MMI_Q_TRACKCOND_ACTION_START = 1Then, send EVC-33 with,MMI_Q_TRACKCOND_STEP = 1MMI_M_TRACKCOND_TYPE = 16MMI_NID_TRACKCOND = 0
            Expected Result: Verify the following information,After DMI displayed the TC02 symbol in sub-area B4, The LX01 symbol is display in sub-area B5
            Test Step Comment: (1) MMI_gen 9499 (partly: B5); MMI_gen 9500 (partly: left to right, filling B5, next area shall be used);
            */

            EVC32_MMITrackConditions.MMI_Q_TRACKCOND_UPDATE = 1;
            EVC32_MMITrackConditions.TrackConditions = new List<TrackCondition>
            {
                {
                    new TrackCondition
                    {
                        MMI_O_TRACKCOND_ANNOUNCE = 0,
                        MMI_O_TRACKCOND_START = 0,
                        MMI_O_TRACKCOND_END = 0,
                        MMI_NID_TRACKCOND = 30,
                        MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Pantograph,
                        MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.AnnounceArea,
                        MMI_Q_TRACKCOND_ACTION_START = Variables.MMI_Q_TRACKCOND_ACTION.WithoutDriverAction,
                        MMI_Q_TRACKCOND_ACTION_END = 0
                    }
                }
            };

            EVC32_MMITrackConditions.Send();


            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays symbol TC02 in sub-area B4.");

            // Wait a few seconds
            Wait_Realtime(3000);

            EVC33_MMIAdditionalOrder.MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Level_Crossing;
            EVC33_MMIAdditionalOrder.MMI_NID_TRACKCOND = 0;
            EVC33_MMIAdditionalOrder.MMI_Q_TRACKCOND_ACTION = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction;
            EVC33_MMIAdditionalOrder.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.AnnounceArea;
            EVC33_MMIAdditionalOrder.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. After 3s, DMI displays symbol LX01 in sub-area B5.");

            MakeTestStepHeader(8, UniqueIdentifier++,
                "Use the test script file 15_6_2_d.xml to send EVC-33 withMMI_Q_TRACKCOND_STEP = 4MMI_NID_TRACKCOND = 0",
                "The LX01 symbol is removed from sub-area B5");
            /*
            Test Step 8
            Action: Use the test script file 15_6_2_d.xml to send EVC-33 withMMI_Q_TRACKCOND_STEP = 4MMI_NID_TRACKCOND = 0
            Expected Result: The LX01 symbol is removed from sub-area B5
            */
            // Call generic Action Method

            EVC33_MMIAdditionalOrder.MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Non_Stopping_Area;
            EVC33_MMIAdditionalOrder.MMI_NID_TRACKCOND = 0;
            EVC33_MMIAdditionalOrder.MMI_Q_TRACKCOND_ACTION = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction;
            EVC33_MMIAdditionalOrder.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.RemoveTC;
            EVC33_MMIAdditionalOrder.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI removes symbol LX01 from sub-area B5.");

            MakeTestStepHeader(9, UniqueIdentifier++,
                "Use the test script file 15_6_2_g.xml to send EVC-32 with,MMI_NID_TRACKCOND = 31MMI_Q_TRACKCOND_STEP = 2MMI_M_TRACKCOND_TYPE =3 MMI_Q_TRACKCOND_ACTION_START = 1Then, send a two  packets EVC-33 with,Common variablesMMI_Q_TRACKCOND_STEP = 1MMI_M_TRACKCOND_TYPE = 16The order of MMI_NID_TRACKCOND in each packetMMI_NID_TRACKCOND = 0MMI_NID_TRACKCOND =1",
                "DMI displays TC01 symbol in sub-area B5");
            /*
            Test Step 9
            Action: Use the test script file 15_6_2_g.xml to send EVC-32 with,MMI_NID_TRACKCOND = 31MMI_Q_TRACKCOND_STEP = 2MMI_M_TRACKCOND_TYPE =3 MMI_Q_TRACKCOND_ACTION_START = 1Then, send a two  packets EVC-33 with,Common variablesMMI_Q_TRACKCOND_STEP = 1MMI_M_TRACKCOND_TYPE = 16The order of MMI_NID_TRACKCOND in each packetMMI_NID_TRACKCOND = 0MMI_NID_TRACKCOND =1
            Expected Result: DMI displays TC01 symbol in sub-area B5
            */

            EVC32_MMITrackConditions.MMI_Q_TRACKCOND_UPDATE = 1;
            EVC32_MMITrackConditions.TrackConditions = new List<TrackCondition>
            {
                {
                    new TrackCondition
                    {
                        MMI_O_TRACKCOND_ANNOUNCE = 0,
                        MMI_O_TRACKCOND_START = 0,
                        MMI_O_TRACKCOND_END = 0,
                        MMI_NID_TRACKCOND = 31,
                        MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Pantograph,
                        MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.InsideArea_Active,
                        MMI_Q_TRACKCOND_ACTION_START = Variables.MMI_Q_TRACKCOND_ACTION.WithoutDriverAction,
                        MMI_Q_TRACKCOND_ACTION_END = 0
                    }
                }
            };

            EVC32_MMITrackConditions.Send();

            // Wait a few seconds between telegrams
            Wait_Realtime(3000);

            EVC33_MMIAdditionalOrder.MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Level_Crossing;
            EVC33_MMIAdditionalOrder.MMI_NID_TRACKCOND = 0;
            EVC33_MMIAdditionalOrder.MMI_Q_TRACKCOND_ACTION = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction;
            EVC33_MMIAdditionalOrder.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.AnnounceArea;
            EVC33_MMIAdditionalOrder.Send();

            // Wait a few seconds between telegrams
            Wait_Realtime(3000);

            EVC33_MMIAdditionalOrder.MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Level_Crossing;
            EVC33_MMIAdditionalOrder.MMI_NID_TRACKCOND = 1;
            EVC33_MMIAdditionalOrder.MMI_Q_TRACKCOND_ACTION = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction;
            EVC33_MMIAdditionalOrder.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.AnnounceArea;
            EVC33_MMIAdditionalOrder.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays symbol TC01 in sub-area B5.");

            MakeTestStepHeader(10, UniqueIdentifier++,
                "Use the test script file 15_6_2_h.xml to send EVC-32 with,MMI_NID_TRACKCOND = 32MMI_Q_TRACKCOND_STEP = 4",
                "Verify the following information,After TC01 symbol is removed from sub-area B5, The LX01 symbol is display in sub-area B5");
            /*
            Test Step 10
            Action: Use the test script file 15_6_2_h.xml to send EVC-32 with,MMI_NID_TRACKCOND = 32MMI_Q_TRACKCOND_STEP = 4
            Expected Result: Verify the following information,After TC01 symbol is removed from sub-area B5, The LX01 symbol is display in sub-area B5
            Test Step Comment: (1) MMI_gen 9500 (partly: Wait until B5 is free);
            */

            EVC32_MMITrackConditions.MMI_Q_TRACKCOND_UPDATE = 1;
            EVC32_MMITrackConditions.TrackConditions = new List<TrackCondition>
            {
                {
                    new TrackCondition
                    {
                        MMI_O_TRACKCOND_ANNOUNCE = 0,
                        MMI_O_TRACKCOND_START = 0,
                        MMI_O_TRACKCOND_END = 0,
                        MMI_NID_TRACKCOND = 31,
                        MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Pantograph,
                        MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.RemoveTC,
                        MMI_Q_TRACKCOND_ACTION_START = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction,
                        MMI_Q_TRACKCOND_ACTION_END = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction
                    }
                }
            };

            EVC32_MMITrackConditions.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI removes symbol TC01 and displays symbol LX01 in sub-area B5.");
            MakeTestStepHeader(11, UniqueIdentifier++,
                "Use the test script file 15_6_2_i.xml to send EVC-32 with,MMI_NID_TRACKCOND = 31MMI_Q_TRACKCOND_STEP = 4",
                "Verify the following information,After TC02 symbol is removed from sub-area B4, The LX01 symbol in sub-area B5 is moved to sub-area B4.The new LX01 symbol is display in sub-area B5");
            /*
            Test Step 11
            Action: Use the test script file 15_6_2_i.xml to send EVC-32 with,MMI_NID_TRACKCOND = 31MMI_Q_TRACKCOND_STEP = 4
            Expected Result: Verify the following information,After TC02 symbol is removed from sub-area B4, The LX01 symbol in sub-area B5 is moved to sub-area B4.The new LX01 symbol is display in sub-area B5
            Test Step Comment: (1) MMI_gen 9500 (partly: Wait until B4 is free); MMI_gen 10480;
            */

            EVC32_MMITrackConditions.MMI_Q_TRACKCOND_UPDATE = 1;
            EVC32_MMITrackConditions.TrackConditions = new List<TrackCondition>
            {
                {
                    new TrackCondition
                    {
                        MMI_O_TRACKCOND_ANNOUNCE = 0,
                        MMI_O_TRACKCOND_START = 0,
                        MMI_O_TRACKCOND_END = 0,
                        MMI_NID_TRACKCOND = 30,
                        MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Pantograph,
                        MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.RemoveTC,
                        MMI_Q_TRACKCOND_ACTION_START = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction,
                        MMI_Q_TRACKCOND_ACTION_END = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction
                    }
                }
            };

            EVC32_MMITrackConditions.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI removes symbol TC01 and displays symbol LX01 in sub-area B4.");

            MakeTestStepHeader(12, UniqueIdentifier++,
                "Use the test script file 15_6_2_j.xml to send EVC-33 with,MMI_Q_TRACKCOND_STEP = 1MMI_M_TRACKCOND_TYPE = 16MMI_NID_TRACKCOND =2Then, send EVC-32 with,MMI_NID_TRACKCOND = 29MMI_Q_TRACKCOND_STEP = 4",
                "Verify the following information,After TC03 symbol is removed from sub-area B3, The LX01 symbol in sub-area B4 is moved to sub-area B3.The LX01 symbol in sub-area B5 is moved to sub-area B4.The new LX01 symbol is display in sub-area B5");
            /*
            Test Step 12
            Action: Use the test script file 15_6_2_j.xml to send EVC-33 with,MMI_Q_TRACKCOND_STEP = 1MMI_M_TRACKCOND_TYPE = 16MMI_NID_TRACKCOND =2Then, send EVC-32 with,MMI_NID_TRACKCOND = 29MMI_Q_TRACKCOND_STEP = 4
            Expected Result: Verify the following information,After TC03 symbol is removed from sub-area B3, The LX01 symbol in sub-area B4 is moved to sub-area B3.The LX01 symbol in sub-area B5 is moved to sub-area B4.The new LX01 symbol is display in sub-area B5
            Test Step Comment: (1) MMI_gen 9500 (partly: Wait until B3 is free);
            */

            EVC33_MMIAdditionalOrder.MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Level_Crossing;
            EVC33_MMIAdditionalOrder.MMI_NID_TRACKCOND = 2;
            EVC33_MMIAdditionalOrder.MMI_Q_TRACKCOND_ACTION = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction;
            EVC33_MMIAdditionalOrder.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.AnnounceArea;
            EVC33_MMIAdditionalOrder.Send();

            // Wait a few seconds between telegrams
            Wait_Realtime(3000);

            EVC32_MMITrackConditions.MMI_Q_TRACKCOND_UPDATE = 1;
            EVC32_MMITrackConditions.TrackConditions = new List<TrackCondition>
            {
                {
                    new TrackCondition
                    {
                        MMI_O_TRACKCOND_ANNOUNCE = 0,
                        MMI_O_TRACKCOND_START = 0,
                        MMI_O_TRACKCOND_END = 0,
                        MMI_NID_TRACKCOND = 29,
                        MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Non_Stopping_Area,
                        MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.RemoveTC,
                        MMI_Q_TRACKCOND_ACTION_START = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction,
                        MMI_Q_TRACKCOND_ACTION_END = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction
                    }
                }
            };

            EVC32_MMITrackConditions.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI removes symbol TC02 and displays symbol LX01 in sub-area B3.");

            TraceHeader("End of test");

            /*
            Test Step 13
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
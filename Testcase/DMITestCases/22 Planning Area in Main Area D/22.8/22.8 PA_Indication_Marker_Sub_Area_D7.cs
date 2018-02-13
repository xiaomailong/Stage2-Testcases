using System;
using Testcase.Telegrams.EVCtoDMI;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 22.8 PA Indication Marker: Sub-Area D7
    /// TC-ID: 17.8
    /// 
    /// This test case verifies the visaulization of PA Indication marker in sub-area D7. It shall comply with condition of [MMI-ETCS-gen]
    /// 
    /// Tested Requirements:
    /// MMI_gen 649 (partly: yellow line and partly: length); MMI_gen 7330; MMI_gen 9947; MMI_gen 7332; MMI_gen 650;
    /// 
    /// Scenario:
    /// 1.Use the test script file to send EVC-1 and EVC-7 with indication marker location and current train position are less than zero or the result of calculation is less than zero. Then, verify that Indicator Marker is not appeared
    /// 2.Use the test script file to send EVC-1 and EVC-7 with indication marker location and current train position correct with condition of MMI_gen 
    /// 650.Then, verify that Indicator Marker is appeared.
    /// 3.Start driving the train forward. Then, verify that Indicator Marker shall move with train position calculation.
    /// 
    /// Used files:
    /// 17_8_a.xml, 17_8_b.xml, 17_8_c.xml 17_8_d.xml
    /// </summary>
    public class TC_ID_17_8_PA_Indication_Marker_Sub_Area_D7 : TestcaseBase
    {
        public override bool TestcaseEntryPoint()
        {
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 23880;
            // Testcase entrypoint
            TraceInfo("This test case requires an ATP configuration change - " +
                      "See Precondition requirements. If this is not done manually, the test may fail!");

            MakeTestStepHeader(1, UniqueIdentifier++, "Driver performs SoM to SR mode, level 1",
                "DMI displays in SR mode, level 1 and the Planning Area is displayed in area D");
            /*
            Test Step 1
            Action: Driver performs SoM to SR mode, level 1
            Expected Result: DMI displays in SR mode, level 1 and the Planning Area is displayed in area D
            */
            // Tested elsewhere, force SR mode/level 1
            StartUp();
            DmiActions.Complete_SoM_L1_SR(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in SR Mode, Level 1.");

            MakeTestStepHeader(2, UniqueIdentifier++,
                "Use the test script file 17_8_a.xml to send EVC-1 with,MMI_O_IML = 4294967295 (-1)",
                "Verify the indication marker shall not be shown in area D7");
            /*
            Test Step 2
            Action: Use the test script file 17_8_a.xml to send EVC-1 with,MMI_O_IML = 4294967295 (-1)
            Note: The result of test script file may interrupted by ATP-CU, need to execute test script file repeatly to see the result.
            Note2: current position (OBU_TR_O_TRAIN) = 0 (1000000000)
            Expected Result: Verify the indication marker shall not be shown in area D7
            Test Step Comment: MMI_gen 7332 (Partly: MMI_O_IML is less than zero);
            */
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN = 1000000000;

            // Set packet 4 times and wait
            for (int w = 0; w < 4; ++w)
            {
                XML_22_8(msgType.typeA);
                Wait_Realtime(200);
            }

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. No indication markers are displayed in sub-area D7.");

            MakeTestStepHeader(3, UniqueIdentifier++,
                "Use the test script file 17_8_b.xml to send EVC-1 with,MMI_O_IML = 1000100000 (1000m)Note: The result of test script file may interrupted by ATP-CU, need to execute test script file repeatly to see the result.Note2: current position (OBU_TR_O_TRAIN) = 0 (1000000000)",
                "Verify the following information,The Indication Marker shall be drawn as a horizontal yellow line The length of Indication Marker shall be equal to the width of Sub-Area D7The Indication Marker shall be shown at position 1000 reference with the bottom of the horizontal yellow line and PA Distance Scale");
            /*
            Test Step 3
            Action: Use the test script file 17_8_b.xml to send EVC-1 with,MMI_O_IML = 1000100000 (1000m)Note: The result of test script file may interrupted by ATP-CU, need to execute test script file repeatly to see the result.Note2: current position (OBU_TR_O_TRAIN) = 0 (1000000000)
            Expected Result: Verify the following information,The Indication Marker shall be drawn as a horizontal yellow line The length of Indication Marker shall be equal to the width of Sub-Area D7The Indication Marker shall be shown at position 1000 reference with the bottom of the horizontal yellow line and PA Distance Scale
            Test Step Comment: (1) MMI_gen 649 (partly: yellow line);(2) MMI_gen 649 (partly: length);(3) MMI_gen 650 (Partly: result of calculation, Y-coordinate); MMI_gen 7330; MMI_gen 9947;
            */
            // Set packet 4 times and wait
            for (int w = 0; w < 4; ++w)
            {
                XML_22_8(msgType.typeB);
                Wait_Realtime(200);
            }

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. A yellow horizontal line is displayed as an Indication Marker." +
                                Environment.NewLine +
                                "2. The PA Indication Marker length is equal to the sub-area D7 width." +
                                Environment.NewLine +
                                "3. The PA Indication Marker is positioned (with the lower edge of the line) at 1000m with respsect to the zero line.");

            MakeTestStepHeader(4, UniqueIdentifier++,
                "Press <Scale Down> button in area D12 to set PA Distance Scale range to 0-32000Use the test script file 17_8_c.xml to send EVC-1 with,MMI_O_IML = 1001000000 (10000m)Note: The result of test script file may interrupted by ATP-CU, need to execute test script file repeatly to see the result.Note2: current position (OBU_TR_O_TRAIN) = 0 (1000000000)",
                "Verify the Indication Marker shall be shown at position 10000 (between PA distance scale line 8000 and 16000)");
            /*
            Test Step 4
            Action: Press <Scale Down> button in area D12 to set PA Distance Scale range to 0-32000Use the test script file 17_8_c.xml to send EVC-1 with,MMI_O_IML = 1001000000 (10000m)Note: The result of test script file may interrupted by ATP-CU, need to execute test script file repeatly to see the result.Note2: current position (OBU_TR_O_TRAIN) = 0 (1000000000)
            Expected Result: Verify the Indication Marker shall be shown at position 10000 (between PA distance scale line 8000 and 16000)
            Test Step Comment: MMI_gen 650 (Partly: result of calculation, Y-coordinate);
            */
            DmiActions.ShowInstruction(this,
                "Press the ‘Scale Down’ button in area D12 to adjust the PA Distance Scale range to 0-32000");

            // Set packet 4 times and wait
            for (int w = 0; w < 4; ++w)
            {
                XML_22_8(msgType.typeC);
                Wait_Realtime(200);
            }

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The PA Indication Marker is displayed between PA distance scale lines 800 and 16000 at position 1000m.");

            MakeTestStepHeader(5, UniqueIdentifier++,
                "Perform the following procedure,Press <Scale Up> button in area D9 to set PA Distance Scale range to 0-1000Drive the train forward with speed 40km/h",
                "");

            /*
            Test Step 5
            Action: Perform the following procedure,Press <Scale Up> button in area D9 to set PA Distance Scale range to 0-1000Drive the train forward with speed 40km/h
            Expected Result: 
            */
            DmiActions.ShowInstruction(this,
                "Press the ‘Scale Up’ button in area D12 to adjust the PA Distance Scale range to 0-1000");

            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 40;

            MakeTestStepHeader(6, UniqueIdentifier++,
                "Use the test script file 17_8_d.xml to send EVC-1 with,MMI_O_IML = 1000120000 (1200m)Note: The result of test script file may interrupted by ATP-CU, need to execute test script file repeatly to see the result",
                "Verify the indication marker shall not be shown in area D7");
            /*
            Test Step 6
            Action: Use the test script file 17_8_d.xml to send EVC-1 with,MMI_O_IML = 1000120000 (1200m)Note: The result of test script file may interrupted by ATP-CU, need to execute test script file repeatly to see the result
            Expected Result: Verify the indication marker shall not be shown in area D7
            Test Step Comment: MMI_gen 650 (partly: not shown by selected range);
            */
            // Set packet 4 times and wait
            for (int w = 0; w < 4; ++w)
            {
                XML_22_8(msgType.typeD);
                Wait_Realtime(200);
            }

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. No indication markers are displayed in sub-area D7.");

            MakeTestStepHeader(7, UniqueIdentifier++,
                "Use the test script file 17_8_d.xml to send EVC-1 with,MMI_O_IML = 1000120000 (1200m)Note: The result of test script file may interrupted by ATP-CU, need to execute test script file repeatly to see the result",
                "The PA Indication Marker is display at the position lower than the PA distance scale line 1000m confirm the result of calculation between EVC-1 and EVC-7 as follows,(EVC-1) MMI_O_IML – (EVC-7) OBU_TR_O_TRAINExample: The train is stopped at position 500m. Result of calculation is [EVC-1.MMI_O_IML = 1000120000] – [EVC-7.OBU_TR_O_TRAIN = 1000050000] = 70000 (700m)");
            /*
            Test Step 7
            Action: Use the test script file 17_8_d.xml to send EVC-1 with,MMI_O_IML = 1000120000 (1200m)Note: The result of test script file may interrupted by ATP-CU, need to execute test script file repeatly to see the result
            Expected Result: The PA Indication Marker is display at the position lower than the PA distance scale line 1000m confirm the result of calculation between EVC-1 and EVC-7 as follows,(EVC-1) MMI_O_IML – (EVC-7) OBU_TR_O_TRAINExample: The train is stopped at position 500m. Result of calculation is [EVC-1.MMI_O_IML = 1000120000] – [EVC-7.OBU_TR_O_TRAIN = 1000050000] = 70000 (700m)
            Test Step Comment: MMI_gen 650;
            */
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN = 1000120000;

            // Set packet 4 times and wait
            for (int w = 0; w < 4; ++w)
            {
                XML_22_8(msgType.typeD);
                Wait_Realtime(200);
            }

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The PA Indication Marker is displayed between the zero and 1000m scale lines at position ~970m.");

            MakeTestStepHeader(8, UniqueIdentifier++,
                "Use the test script file 17_8_d.xml to send EVC-1 with,MMI_O_IML = 1000120000 (1200m)Note: The result of test script file may interrupted by ATP-CU, need to execute test script file repeatly to see the result",
                "Verify the indication marker shall not be shown in area D7");
            /*
            Test Step 8
            Action: Use the test script file 17_8_d.xml to send EVC-1 with,MMI_O_IML = 1000120000 (1200m)Note: The result of test script file may interrupted by ATP-CU, need to execute test script file repeatly to see the result
            Expected Result: Verify the indication marker shall not be shown in area D7
            Test Step Comment: MMI_gen 7332 (partly: Result of equation is less than zero);
            */
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN = 100120000;

            // Set packet 4 times and wait
            for (int w = 0; w < 4; ++w)
            {
                XML_22_8(msgType.typeD);
                Wait_Realtime(200);
            }

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. No indication markers are displayed in sub-area D7.");

            MakeTestStepHeader(9, UniqueIdentifier++, "Stop the train", "Train is standstill");
            /*
            Test Step 9
            Action: Stop the train
            Expected Result: Train is standstill
            */
            EVC1_MMIDynamic.MMI_V_TRAIN = 0;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Speed pointer displays ‘0’ km/h.");

            TraceHeader("End of test");

            /*
            Test Step 10
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }

        #region Send_XML_22_8_DMI_Test_Specification

        private enum msgType : byte
        {
            typeA,
            typeB,
            typeC,
            typeD
        };

        private void XML_22_8(msgType packetSelector)
        {
            EVC1_MMIDynamic.MMI_M_SLIP = EVC1_MMIDynamic.MMI_M_SLIDE = 0;
            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Normal_Status_Ceiling_Speed_Monitoring;
            EVC1_MMIDynamic.MMI_A_TRAIN = 0;
            EVC1_MMIDynamic.MMI_V_TRAIN = EVC1_MMIDynamic.MMI_V_TARGET =
                EVC1_MMIDynamic.MMI_V_RELEASE = EVC1_MMIDynamic.MMI_V_INTERVENTION = 0;
            EVC1_MMIDynamic.MMI_V_PERMITTED = 0;
            EVC1_MMIDynamic.MMI_O_BRAKETARGET = 0;
            SITR.ETCS1.Dynamic.EVC01Validity1.Value = 0x01;
            SITR.ETCS1.Dynamic.EVC01Validity2.Value = 0x01;

            switch (packetSelector)
            {
                case msgType.typeA:
                    EVC1_MMIDynamic.MMI_O_IML = -1;
                    break;
                case msgType.typeB:
                    EVC1_MMIDynamic.MMI_O_IML = 1000100000;
                    break;
                case msgType.typeC:
                    EVC1_MMIDynamic.MMI_O_IML = 1001000000;
                    break;
                case msgType.typeD:
                    EVC1_MMIDynamic.MMI_O_IML = 1000120000;
                    break;
            }
        }

        #endregion
    };
}
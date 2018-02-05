using System;
using Testcase.Telegrams.EVCtoDMI;
using Testcase.Telegrams.DMItoEVC;

namespace Testcase.DMITestCases
{
    /// <summary>
    /// 23.4.1 Geographical Position: General presentation
    /// TC-ID: 18.4.1
    /// 
    /// Tested Requirements:
    /// MMI_gen 9866; MMI_gen 9872; MMI_gen 9873 (partly: touchscreen); MMI_gen 9875; MMI_gen 9877; 
    /// MMI_gen 9878; MMI_gen 9879; MMI_gen 9874 (partly: toggled on);
    /// MMI_gen 2495 (partly: maximum 3 digits of meter, maximum 4 digits of kilometer, kilometer_meter format);
    /// MMI_gen 9416; MMI_gen 656 (partly: transmit EVC-101, touch screen); MMI_gen 655;
    /// MMI_gen 2498; MMI_gen 1088 (partly: Bit #23);
    /// 
    /// Scenario:
    /// Perform SoM to SR mode, L1
    /// 
    /// Pass BG1 at 100 m: DMI changes from SR mode to FS mode.
    /// Packet 12: L_ENDSECTION = 3000 m
    /// Packet 21: G_A = 0
    /// Packet 27: V_STATIC = 150 km/h
    /// 
    /// Pass BG2 at 200 m:
    /// Packet 79: NID_BG = 2
    ///     M_POSITION = 1000000
    ///     D_POSOFF = 0
    ///     NID_BG = 3
    ///     M_POSITION = 900
    ///     D_POSOFF = 0
    /// 
    /// Pass BG3 at 1000 m
    /// (No packet information, use as reference location)
    /// Stop the train.
    /// Select Shunting mode
    /// Exit Shunting mode and perform SoM to SR mode, L1
    /// 
    /// Pass BG4 at 1700 m:
    /// Packet 79:
    ///     NID_BG = 2
    ///     M_POSITION = 1000000
    ///     D_POSOFF = 0
    ///     NID_BG = 3
    ///     M_POSITION = 900
    ///     D_POSOFF = 0
    /// Use the test script file to send an invalid value of EVC-5.
    /// Observer and verify Geographical Position indication at three locations:
    ///     Balise#1 at 100 m to transition from SR to FS mode
    /// Continue driving train to reach balise#2 at 200 m
    /// Continue driving train to reach balise#3 at 1000 m
    /// Continue driving train to reach balise#4 at 1700 m and received packet EVC-5 from test script file
    /// 
    /// Used files:
    /// 18_4_1.tdg, 18_4_1.xml
    /// </summary>
    public class TC_18_4_1_Geographical_Position : TestcaseBase
    {

        public override bool TestcaseEntryPoint()
        {
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 0;
            // Testcase entrypoint
            StartUp();
            DmiActions.Complete_SoM_L1_SR(this);

            MakeTestStepHeader(1, UniqueIdentifier++, "Drive the train forward with the permitted speed",
                "DMI displays in SR mode, level 1");
            /*
            Test Step 1
            Action: Drive the train forward with the permitted speed
            Expected Result: DMI displays in SR mode, level 1
            */
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "DMI displays in SR mode, level 1");

            MakeTestStepHeader(2, UniqueIdentifier++, "Pass BG1 with Pkt 12, 21, and 27",
                "DMI displays in FS mode, level 1");
            /*
            Test Step 2
            Action: Pass BG1 with Pkt 12, 21, and 27
            Expected Result: DMI displays in FS mode, level 1
            */
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.FullSupervision;
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "DMI displays in FS mode, level 1");

            MakeTestStepHeader(3, UniqueIdentifier++, "Pass BG2 with packet 79 Geographical position",
                "Verify the Geographical Position indicator");
            /*
            Test Step 3
            Action: Pass BG2 with packet 79 Geographical position
            Expected Result: Verify the Geographical Position indicator
                    The symbol ‘DR03’ is displayed in sub-area G12 as toggled off (the position is known by onboard)
                    DMI receives EVC-30 with bit No.23 of variable MMI_Q_REQUEST_ENABLE_64 = 1 (DMI displays that position is known by onboard)
            Test Step Comment: (1) MMI_gen 9866, MMI_gen 9872, MMI_gen 9875;
                                (2) MMI_gen 9416 (partly: EVC-30. MMI_Q_REQUEST_ENABLE_64 = 1 = known onboard);
                                    MMI_gen 1088 (partly: Bit #23);
            */
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN = 1000000;
            EVC5_MMIGeoPosition.MMI_M_ABSOLUTPOS = 1000000;
            EVC5_MMIGeoPosition.MMI_M_RELATIVPOS = 1;
            EVC5_MMIGeoPosition.Send();

            EVC30_MMIRequestEnable.SendBlank();

            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.No_window_specified;
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = Variables.standardFlags |
                                                               EVC30_MMIRequestEnable.EnabledRequests
                                                                   .GeographicalPosition;
            EVC30_MMIRequestEnable.Send();

            // Call generic Check Results Method
            DmiExpectedResults.Driver_symbol_displayed(this, "Geographical Position", "DR03", "G12", false);

            MakeTestStepHeader(4, UniqueIdentifier++,
                "Press on the ‘DR03’ symbol, on sub-area G12 to toggle on the Geographical Position function",
                "Verify the Geographical Position indicator.");
            /*
            Test Step 4
            Action: Press on the ‘DR03’ symbol, on sub-area G12 to toggle on the Geographical Position function
                    and verify the presentation on the screen
            Expected Result: Verify the Geographical Position indicator.
                The sub-area G12 displays a grey background colour with black text colour showing numbers in the following format
                nnnn_ddd as shown in the figure below where nnnn are the km digits, _ is a space character and ddd are the metres
                DMI displays the geographical position same as a value of variable MMI_M_ABSOLUTPOS of EVC-5.
                DMI sends EVC-101 with variable MMI_M_REQUEST = 8 (Figure 117, [MMI-ETCS-gen])
            Test Step Comment: (1) MMI_gen 9872, MMI_gen 9873 (partly: toggle on state for touchscreen); MMI_gen 9877;MMI_gen 9878;
                                    MMI_gen 2495 (partly: kilometer_meter format); MMI_gen 2498;
                                (2) MMI_gen 655,
                                (3) MMI_gen 656 (partly: transmit EVC-101, touch screen)
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press on the ‘DR03’ symbol, on sub-area G12.");

            // Check if Geographic Position button has been pressed
            // This wil not work: only release event is trackable
            //EVC101_MMIDriverRequest.CheckMRequestReleased = Variables.MMI_M_REQUEST.GeographicalPositionRequest;
            EVC5_MMIGeoPosition.MMI_M_ABSOLUTPOS = 8388609;
            EVC5_MMIGeoPosition.MMI_M_RELATIVPOS = 0;
            EVC5_MMIGeoPosition.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The sub-area G12 displays a grey background with black text showing numbers " +
                                "in the correct format." + Environment.NewLine +
                                string.Format("2. The geographical position value = {0}.",
                                    EVC5_MMIGeoPosition.MMI_M_ABSOLUTPOS));

            MakeTestStepHeader(5, UniqueIdentifier++,
                "Press on the ‘DR03’ symbol on sub-area G12 to toggle off the Geographical Position function and",
                "(1) The grey background colour in previous step is replaced by symbol ‘DR03’ in sub-area G12");
            /*
            Test Step 5
            Action: Press on the ‘DR03’ symbol on sub-area G12 to toggle off the Geographical Position function and
                    verify the presentation on the screen.
            Expected Result: (1) The grey background colour in previous step is replaced by symbol ‘DR03’ in sub-area G12
            Test Step Comment: (1) MMI_gen 9872, MMI_gen 9873 (partly: toggle off state for touchscreen); MMI_gen 9875;
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press on the ‘DR03’ symbol on sub-area G12.");

            // Call generic Check Results Method
            DmiExpectedResults.Driver_symbol_displayed(this, "Geographical Position", "DR03", "G12", false);

            MakeTestStepHeader(6, UniqueIdentifier++,
                "Stop the train, the driver presses the symbol of Geographical Position at sub-area G12 again.",
                "The Geographical Position is displayed with valid value of the train position.");
            /*
            Test Step 6
            Action: Stop the train, the driver presses the symbol of Geographical Position at sub-area G12 again.
            Expected Result: The Geographical Position is displayed with valid value of the train position.
                            Verify that the Geographic Position is displaying the fractional part that consists of three digits.
                            The integral part consists of four digits. A space character is inserted between the kilometre
                            and the metre parts. The full sub-area G12 is displayed as grey background.
                            The geographical position is displayed in black and located at centre of the G12 area.
            Test Step Comment: (1) MMI_gen 2495 ( partly: maximum 3 digits of meter, maximum 4 digits of kilometer); MMI_gen 9872;
                                (2) MMI_gen 9878;
                                (3) MMI_gen 9873 (partly: toggle on state for touchscreen);                                 
            */
            // Check if Geographic Position button has been pressed
            EVC101_MMIDriverRequest.CheckMRequestReleased = Variables.MMI_M_REQUEST.GeographicalPositionRequest;
            EVC5_MMIGeoPosition.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The sub-area G12 displays a grey background with black text showing numbers " +
                                "in the correct format." + Environment.NewLine +
                                string.Format("2. The geographical position value = {0}.",
                                    EVC5_MMIGeoPosition.MMI_M_ABSOLUTPOS));

            MakeTestStepHeader(7, UniqueIdentifier++, "Start driving the train forward",
                "Verify that the last status of geographical position is not changed.");
            /*
            Test Step 7
            Action: Start driving the train forward
            Expected Result: Verify that the last status of geographical position is not changed.
                            The full sub-area G12 is displayed as grey background.
                            The geographical position is displayed in black and located at centre of the G12 area
            Test Step Comment: MMI_gen 9874 (partly: toggled on);   
            */
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN = 900000;
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The geographical position value is still equal to" +
                                string.Format(" {0}.", EVC5_MMIGeoPosition.MMI_M_ABSOLUTPOS));

            MakeTestStepHeader(8, UniqueIdentifier++,
                "Press on the ‘DR03’ symbol on sub-area G12 to toggle off the Geographical Position function and",
                "The grey background colour in previous step is replaced by symbol ‘DR03’ in sub-area G12");
            /*
            Test Step 8
            Action: Press on the ‘DR03’ symbol on sub-area G12 to toggle off the Geographical Position function and
                    verify the presentation on the screen
            Expected Result: The grey background colour in previous step is replaced by symbol ‘DR03’ in sub-area G12
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press on the ‘DR03’ symbol in sub-area G12.");

            EVC101_MMIDriverRequest.CheckMRequestReleased = Variables.MMI_M_REQUEST.GeographicalPositionRequest;

            WaitForVerification("Check the following: " + Environment.NewLine + Environment.NewLine +
                                "1. The grey background of the geographical position is replaced by symbol DR03");

            MakeTestStepHeader(9, UniqueIdentifier++, "Pass BG3 with the new Geographical position",
                "The symbol ‘DR03’ remains in sub-area G12");
            /*
            Test Step 9
            Action: Pass BG3 with the new Geographical position
            Expected Result: The symbol ‘DR03’ remains in sub-area G12
            */
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN = 1000000;
            EVC5_MMIGeoPosition.MMI_M_ABSOLUTPOS = 1000000;

            WaitForVerification("Check the following: " + Environment.NewLine + Environment.NewLine +
                                "1. The DR03 symbol is still displayed in sub-area G12");

            MakeTestStepHeader(10, UniqueIdentifier++,
                "Press on the ‘DR03’ symbol, on sub-area G12 to toggle on the Geographical Position function and",
                "Verify the Geographical Position indicator");
            /*
            Test Step 10
            Action: Press on the ‘DR03’ symbol, on sub-area G12 to toggle on the Geographical Position function and
                    verify the presentation on the screen
            Expected Result: Verify the Geographical Position indicator
                            The sub-area G12 displays a grey background with black text showing numbers in the following format
                            nnnn_ddd as shown in the figure below
            Test Step Comment: MMI_gen 2495 ( partly: 2 digits of meter, at least 1 digit of kilometer);    
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press on the ‘DR03’ symbol in sub-area G12.");

            EVC101_MMIDriverRequest.CheckMRequestReleased = Variables.MMI_M_REQUEST.GeographicalPositionRequest;
            EVC5_MMIGeoPosition.Send();

            WaitForVerification("Check the following: " + Environment.NewLine + Environment.NewLine +
                                "1. A number in the format nnnn_ddd is displayed in black on a grey background in sub-area G12");

            MakeTestStepHeader(11, UniqueIdentifier++, "Perform the following procedure",
                "DMI displays in SH mode, level 1.");
            /*
            Test Step 11
            Action: Perform the following procedure
                    Stop the train.
                    Press ‘Main’ button.
                    Press and hold ‘Shunting’ button for at least 2 seconds.
                    Release the pressed area
            Expected Result: DMI displays in SH mode, level 1.
                            Verify that the symbol of Geographical Position at sub-area G12 is not displayed.
                            In sub-area G12, it is not displayed as a sensitive area for toggle on/off.
                            DMI receives EVC-30 with bit No.23 of variable MMI_Q_REQUEST_ENABLE_64 = 0 or
                                EVC-5 with variable MMI_M_ABSOLUTPOS < 0 (DMI displays that position is NOT known by onboard)
            Test Step Comment: (1) MMI_gen 9879;   (2) MMI_gen 9416 (partly: not known by onboard, EVC-30)
            */
            DmiActions.Send_SH_Mode(this);
            EVC5_MMIGeoPosition.MMI_M_ABSOLUTPOS = -1;
            EVC5_MMIGeoPosition.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. There is no symbol displayed in area G12.");

            MakeTestStepHeader(12, UniqueIdentifier++, "Perform the following procedure:",
                "DMI displays in SR mode, Level 1");
            /*
            Test Step 12
            Action: Perform the following procedure:
                    Press ‘Main’ button.
                    Press and hold ‘Exit Shunting’ button for at least 2 seconds.
                    Release the pressed area.
                    Perform SoM in SR mode, Level 1.
                    Drive the train forward
            Expected Result: DMI displays in SR mode, Level 1
            */

            DmiActions.Send_SR_Mode(this);
            // Call generic Check Results Method
            WaitForVerification("Check the following: " + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in SR mode, Level 1.");

            MakeTestStepHeader(13, UniqueIdentifier++, "Pass BG4 with the new Geographical position",
                "The symbol ‘DR03’ displays in sub-area G12");
            /*
            Test Step 13
            Action: Pass BG4 with the new Geographical position
            Expected Result: The symbol ‘DR03’ displays in sub-area G12
            */
            EVC5_MMIGeoPosition.MMI_M_ABSOLUTPOS = 1000000;
            EVC5_MMIGeoPosition.Send();
            // Call generic Check Results Method
            DmiExpectedResults.Driver_symbol_displayed(this, "Geographical Position", "DR03", "G12", false);

            MakeTestStepHeader(14, UniqueIdentifier++, "Perform the following procedure:",
                "Verify that the symbol of Geographical Position at sub-area G12 is not displayed.");
            /*
            Test Step 14
            Action: Perform the following procedure:
                    Stop the train.
                    Use the test script file 18_4_1.xml to send EVC-5 with MMI_M_ABSOLUTPOS = 8388609
                    Press at sub-area G12
            Expected Result: Verify that the symbol of Geographical Position at sub-area G12 is not displayed.
                            In sub-area G12, it is not displayed as a sensitive area for toggle on/off.
                            Use the log file to confirm that DMI did not send out packet EVC-101 with variable MMI_M_REQUEST = 8
            Test Step Comment: (1) MMI_gen 9416 (partly: not known by onboard, EVC-5); MMI_gen 9879 (partly: not display symbol DR03);
                                (2) MMI_gen 9879 (partly: G12 not be sensitive);   
            */
            EVC5_MMIGeoPosition.MMI_M_ABSOLUTPOS = 8388609;
            EVC5_MMIGeoPosition.MMI_M_RELATIVPOS = 0;
            EVC5_MMIGeoPosition.Send();

            WaitForVerification("Check the following: " + Environment.NewLine + Environment.NewLine +
                                "1. DMI removes the DR03 symbol in sub-area G12" + Environment.NewLine +
                                "2. Sub-area G12 is not sensitive for toggling on/off");

            MakeTestStepHeader(15, UniqueIdentifier++, "End of test", "");

            /*
            Test Step 15
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
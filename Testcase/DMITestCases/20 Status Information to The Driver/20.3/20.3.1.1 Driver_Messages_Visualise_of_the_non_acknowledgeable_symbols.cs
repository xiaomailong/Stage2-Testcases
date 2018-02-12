using Testcase.Telegrams.EVCtoDMI;

namespace Testcase.DMITestCases
{
    /// <summary>
    /// 20.3.1.1 Driver Messages: Visualise of the non-acknowledgeable symbols
    /// TC-ID: 15.3.1.1
    /// 
    /// This test case verifies the display information of each symbol refers to received packet information EVC-8.
    /// The location of symbols are comply with [ERA-ERTMS].
    /// 
    /// Tested Requirements:
    /// MMI_gen 7022 (partly: exclude radio connection symbols);
    /// MMI_gen 3005 (partly: exclude radio connection symbols);
    /// MMI_gen 144 (partly: Symbols); MMI_gen 1699 (partly: non-acknowledgement, symbol);
    /// 
    /// Scenario:
    /// At the default window, use the test script file to send Driver message to DMI.
    /// Verify the display of normal non-acknowledgement symbols.
    /// 
    /// Notes: 
    /// 1. Each step of test script file in executed continuously, the tester needs to confirm each expected result.
    /// 2. The symbol of Radio connection will be verified in another test case.
    /// 
    /// Used files:
    /// 15_3_1_1_a.xml, 15_3_1_1_b.xml, 15_3_1_1_c.xml
    /// </summary>
    public class TC_15_3_1_1_Driver_Messages : TestcaseBase
    {
        public override bool TestcaseEntryPoint()
        {
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 22561;
            // Testcase entrypoint
            StartUp();
            DmiActions.Complete_SoM_L1_SB(this);

            MakeTestStepHeader(1, UniqueIdentifier++,
                "Use the test script file 15_3_1_1_a.xml to send EVC-8 with,MMI_Q_TEXT = 260MMI_Q_TEXT_CRITERIA = 3MMI_I_TEXT = 1",
                "DMI displays ST01 symbol in sub-area C9 without yellow flashing frame");
            /*
            Test Step 1
            Action: Use the test script file 15_3_1_1_a.xml to send EVC-8 with,MMI_Q_TEXT = 260MMI_Q_TEXT_CRITERIA = 3MMI_I_TEXT = 1
            Expected Result: DMI displays ST01 symbol in sub-area C9 without yellow flashing frame
            Test Step Comment: MMI_gen 7022 (partly: exclude radio connection symbols);
                                MMI_gen 3005 (partly: exclude radio connection symbols);
                                MMI_gen 1699 (partly: non-acknowledgement, symbol);
            */

            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 260;
            EVC8_MMIDriverMessage.Send();

            DmiExpectedResults.Driver_symbol_displayed(this, "Brake Intervention", "ST01", "C9", false);

            MakeTestStepHeader(2, UniqueIdentifier++,
                "(Continue from step 1)Send EVC-8 with,MMI_Q_TEXT = 286MMI_Q_TEXT_CRITERIA = 3MMI_I_TEXT = 2",
                "Verify the following information,(1)    DMI displays ST06 symbol in sub-area C6 without yellow flashing frame");
            /*
            Test Step 2
            Action: (Continue from step 1)Send EVC-8 with,MMI_Q_TEXT = 286MMI_Q_TEXT_CRITERIA = 3MMI_I_TEXT = 2
            Expected Result: Verify the following information,(1)    DMI displays ST06 symbol in sub-area C6 without yellow flashing frame
            Test Step Comment: (1) MMI_gen 7022 (partly: exclude radio connection symbols);  MMI_gen 3005 (partly: exclude radio connection symbols);  MMI_gen 1699 (partly: non-acknowledgement, symbol);
            */

            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 2;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 286;
            EVC8_MMIDriverMessage.Send();

            DmiExpectedResults.Driver_symbol_displayed(this, "Reversing permitted", "ST06", "C6", false);

            MakeTestStepHeader(3, UniqueIdentifier++,
                "(Continue from step 2)Send EVC-8 with,MMI_Q_TEXT = 298MMI_Q_TEXT_CRITERIA = 3MMI_I_TEXT = 3",
                "Verify the following information,(1)    DMI displays DR02 symbol in sub-area D without yellow flashing frame");
            /*
            Test Step 3
            Action: (Continue from step 2)Send EVC-8 with,MMI_Q_TEXT = 298MMI_Q_TEXT_CRITERIA = 3MMI_I_TEXT = 3
            Expected Result: Verify the following information,(1)    DMI displays DR02 symbol in sub-area D without yellow flashing frame
            Test Step Comment: (1) MMI_gen 7022 (partly: exclude radio connection symbols);  MMI_gen 3005 (partly: exclude radio connection symbols);  MMI_gen 1699 (partly: non-acknowledgement, symbol);
            */

            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 3;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 298;
            EVC8_MMIDriverMessage.Send();

            DmiExpectedResults.Driver_symbol_displayed(this, "Track Ahead Free", "DR02", "D", false);

            MakeTestStepHeader(4, UniqueIdentifier++,
                "(Continue from step 3)Send EVC-8 with,MMI_Q_TEXT = 710MMI_Q_TEXT_CRITERIA = 3MMI_I_TEXT = 4",
                "Verify the following information,(1)    DMI displays train divided symbol in sub-area C1 without yellow flashing frame.Note: The information of this symbol is not provided by [ERA-ERTMS] and [GenVSIS], it’s used the same reference from DMI-DOS");
            /*
            Test Step 4
            Action: (Continue from step 3)Send EVC-8 with,MMI_Q_TEXT = 710MMI_Q_TEXT_CRITERIA = 3MMI_I_TEXT = 4
            Expected Result: Verify the following information,(1)    DMI displays train divided symbol in sub-area C1 without yellow flashing frame.Note: The information of this symbol is not provided by [ERA-ERTMS] and [GenVSIS], it’s used the same reference from DMI-DOS
            Test Step Comment: (1) MMI_gen 7022 (partly: exclude radio connection symbols);  MMI_gen 3005 (partly: exclude radio connection symbols);  MMI_gen 1699 (partly: non-acknowledgement, symbol);
            */

            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 4;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 710;
            EVC8_MMIDriverMessage.Send();

            DmiExpectedResults.Driver_symbol_displayed(this, "#3 (Train divided)", "DR02", "C1", false);

            MakeTestStepHeader(5, UniqueIdentifier++,
                "(Continue from step 4)Send EVC-8 with,MMI_Q_TEXT = 276MMI_Q_TEXT_CRITERIA = 3MMI_N_TEXT = 1MMI_X_TEXT = 0MMI_I_TEXT = 4",
                "Verify the following information,(1)    DMI displays LE06 symbol in sub-area C1 without yellow flashing frame");
            /*
            Test Step 5
            Action: (Continue from step 4)Send EVC-8 with,MMI_Q_TEXT = 276MMI_Q_TEXT_CRITERIA = 3MMI_N_TEXT = 1MMI_X_TEXT = 0MMI_I_TEXT = 4
            Expected Result: Verify the following information,(1)    DMI displays LE06 symbol in sub-area C1 without yellow flashing frame
            Test Step Comment: (1) MMI_gen 7022 (partly: exclude radio connection symbols);  MMI_gen 1699 (partly: non-acknowledgement, symbol); MMI_gen 147 (partly: symbol, remove the same index, MMI_gen 144 (partly: symbol, the same index); MMI_gen 3005 (partly: exclude radio connection symbols); 
            */

            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 4;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 276;
            EVC8_MMIDriverMessage.PlainTextMessage = "0"; // Level 0
            EVC8_MMIDriverMessage.Send();

            DmiExpectedResults.Driver_symbol_displayed(this, "Level 0 announcement", "LE06", "C1", false);

            MakeTestStepHeader(6, UniqueIdentifier++,
                "(Continue from step 5)Send EVC-8 with,MMI_Q_TEXT = 276MMI_Q_TEXT_CRITERIA = 3MMI_N_TEXT = 1MMI_X_TEXT = 1MMI_I_TEXT = 4",
                "Verify the following information,(1)    DMI displays LE10 symbol in sub-area C1 without yellow flashing frame");
            /*
            Test Step 6
            Action: (Continue from step 5)Send EVC-8 with,MMI_Q_TEXT = 276MMI_Q_TEXT_CRITERIA = 3MMI_N_TEXT = 1MMI_X_TEXT = 1MMI_I_TEXT = 4
            Expected Result: Verify the following information,(1)    DMI displays LE10 symbol in sub-area C1 without yellow flashing frame
            Test Step Comment: (1) MMI_gen 7022 (partly: exclude radio connection symbols);  MMI_gen 1699 (partly: non-acknowledgement, symbol); MMI_gen 147 (partly: symbol, remove the same index, MMI_gen 144 (partly: symbol, the same index); MMI_gen 3005 (partly: exclude radio connection symbols); 
            */

            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 4;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 276;
            EVC8_MMIDriverMessage.PlainTextMessage = "1"; // Level 1
            EVC8_MMIDriverMessage.Send();

            DmiExpectedResults.Driver_symbol_displayed(this, "Level 1 announcement", "LE10", "C1", false);

            MakeTestStepHeader(7, UniqueIdentifier++,
                "(Continue from step 6)Send EVC-8 with,MMI_Q_TEXT = 276MMI_Q_TEXT_CRITERIA = 3MMI_N_TEXT = 1MMI_X_TEXT = 2MMI_I_TEXT = 4",
                "Verify the following information,(1)    DMI displays LE12 symbol in sub-area C1 without yellow flashing frame");
            /*
            Test Step 7
            Action: (Continue from step 6)Send EVC-8 with,MMI_Q_TEXT = 276MMI_Q_TEXT_CRITERIA = 3MMI_N_TEXT = 1MMI_X_TEXT = 2MMI_I_TEXT = 4
            Expected Result: Verify the following information,(1)    DMI displays LE12 symbol in sub-area C1 without yellow flashing frame
            Test Step Comment: (1) MMI_gen 7022 (partly: exclude radio connection symbols);  MMI_gen 1699 (partly: non-acknowledgement, symbol); MMI_gen 147 (partly: symbol, remove the same index, MMI_gen 144 (partly: symbol, the same index); MMI_gen 3005 (partly: exclude radio connection symbols); 
            */

            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 4;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 276;
            EVC8_MMIDriverMessage.PlainTextMessage = "2"; // Level 2
            EVC8_MMIDriverMessage.Send();

            DmiExpectedResults.Driver_symbol_displayed(this, "Level 2 announcement", "LE12", "C1", false);

            MakeTestStepHeader(8, UniqueIdentifier++,
                "(Continue from step 7)Send EVC-8 with,MMI_Q_TEXT = 276MMI_Q_TEXT_CRITERIA = 3MMI_N_TEXT = 1MMI_X_TEXT = 3MMI_I_TEXT = 4",
                "Verify the following information,(1)    DMI displays LE14 symbol in sub-area C1 without yellow flashing frame");
            /*
            Test Step 8
            Action: (Continue from step 7)Send EVC-8 with,MMI_Q_TEXT = 276MMI_Q_TEXT_CRITERIA = 3MMI_N_TEXT = 1MMI_X_TEXT = 3MMI_I_TEXT = 4
            Expected Result: Verify the following information,(1)    DMI displays LE14 symbol in sub-area C1 without yellow flashing frame
            Test Step Comment: (1) MMI_gen 7022 (partly: exclude radio connection symbols);  MMI_gen 1699 (partly: non-acknowledgement, symbol); MMI_gen 147 (partly: symbol, remove the same index, MMI_gen 144 (partly: symbol, the same index); MMI_gen 3005 (partly: exclude radio connection symbols); 
            */

            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 4;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 276;
            EVC8_MMIDriverMessage.PlainTextMessage = "3"; // Level 3
            EVC8_MMIDriverMessage.Send();

            DmiExpectedResults.Driver_symbol_displayed(this, "Level 3 announcement", "LE14", "C1", false);

            MakeTestStepHeader(9, UniqueIdentifier++,
                "(Continue from step 8)Send EVC-8 with,MMI_Q_TEXT = 259MMI_Q_TEXT_CRITERIA = 3MMI_I_TEXT = 4",
                "Verify the following information,(1)    DMI displays MO08 symbol in sub-area C1 without yellow flashing frame");
            /*
            Test Step 9
            Action: (Continue from step 8)Send EVC-8 with,MMI_Q_TEXT = 259MMI_Q_TEXT_CRITERIA = 3MMI_I_TEXT = 4
            Expected Result: Verify the following information,(1)    DMI displays MO08 symbol in sub-area C1 without yellow flashing frame
            Test Step Comment: (1) MMI_gen 7022 (partly: exclude radio connection symbols);  MMI_gen 1699 (partly: non-acknowledgement, symbol); MMI_gen 147 (partly: symbol, remove the same index, MMI_gen 144 (partly: symbol, the same index); MMI_gen 3005 (partly: exclude radio connection symbols); 
            */

            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 4;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 259;
            EVC8_MMIDriverMessage.Send();

            DmiExpectedResults.Driver_symbol_displayed(this, "Acknowledgement for On Sight", "MO08", "C1",
                false);


            MakeTestStepHeader(10, UniqueIdentifier++,
                "(Continue from step 9)Send EVC-8 with,MMI_Q_TEXT = 262MMI_Q_TEXT_CRITERIA = 3MMI_I_TEXT = 4",
                "Verify the following information,(1)    DMI displays MO15 symbol in sub-area C1 without yellow flashing frame");
            /*
            Test Step 10
            Action: (Continue from step 9)Send EVC-8 with,MMI_Q_TEXT = 262MMI_Q_TEXT_CRITERIA = 3MMI_I_TEXT = 4
            Expected Result: Verify the following information,(1)    DMI displays MO15 symbol in sub-area C1 without yellow flashing frame
            Test Step Comment: (1) MMI_gen 7022 (partly: exclude radio connection symbols);  MMI_gen 1699 (partly: non-acknowledgement, symbol); MMI_gen 147 (partly: symbol, remove the same index, MMI_gen 144 (partly: symbol, the same index); MMI_gen 3005 (partly: exclude radio connection symbols); 
            */

            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 4;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 262;
            EVC8_MMIDriverMessage.Send();

            DmiExpectedResults.Driver_symbol_displayed(this, "Acknowledgement for Reversing", "MO15", "C1",
                false);

            MakeTestStepHeader(11, UniqueIdentifier++,
                "(Continue from step 10)Send EVC-8 with,MMI_Q_TEXT = 263MMI_Q_TEXT_CRITERIA = 3MMI_I_TEXT = 4",
                "Verify the following information,(1)    DMI displays MO10 symbol in sub-area C1 without yellow flashing frame");
            /*
            Test Step 11
            Action: (Continue from step 10)Send EVC-8 with,MMI_Q_TEXT = 263MMI_Q_TEXT_CRITERIA = 3MMI_I_TEXT = 4
            Expected Result: Verify the following information,(1)    DMI displays MO10 symbol in sub-area C1 without yellow flashing frame
            Test Step Comment: (1) MMI_gen 7022 (partly: exclude radio connection symbols);  MMI_gen 1699 (partly: non-acknowledgement, symbol); MMI_gen 147 (partly: symbol, remove the same index, MMI_gen 144 (partly: symbol, the same index); MMI_gen 3005 (partly: exclude radio connection symbols); 
            */

            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 4;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 263;
            EVC8_MMIDriverMessage.Send();

            DmiExpectedResults.Driver_symbol_displayed(this, "Acknowledgement for Staff Responsible", "MO10",
                "C1", false);

            MakeTestStepHeader(12, UniqueIdentifier++,
                "(Continue from step 11)Send EVC-8 with,MMI_Q_TEXT = 264MMI_Q_TEXT_CRITERIA = 3MMI_I_TEXT = 4",
                "Verify the following information,(1)    DMI displays MO17 symbol in sub-area C1 without yellow flashing frame");
            /*
            Test Step 12
            Action: (Continue from step 11)Send EVC-8 with,MMI_Q_TEXT = 264MMI_Q_TEXT_CRITERIA = 3MMI_I_TEXT = 4
            Expected Result: Verify the following information,(1)    DMI displays MO17 symbol in sub-area C1 without yellow flashing frame
            Test Step Comment: (1) MMI_gen 7022 (partly: exclude radio connection symbols);  MMI_gen 1699 (partly: non-acknowledgement, symbol); MMI_gen 147 (partly: symbol, remove the same index, MMI_gen 144 (partly: symbol, the same index); MMI_gen 3005 (partly: exclude radio connection symbols); 
            */

            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 4;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 264;
            EVC8_MMIDriverMessage.Send();

            DmiExpectedResults.Driver_symbol_displayed(this, "Acknowledgement for Unfitted", "MO17", "C1",
                false);

            MakeTestStepHeader(13, UniqueIdentifier++,
                "(Continue from step 12)Send EVC-8 with,MMI_Q_TEXT = 265MMI_Q_TEXT_CRITERIA = 3MMI_I_TEXT = 4",
                "Verify the following information,(1)    DMI displays MO02 symbol in sub-area C1 without yellow flashing frame");
            /*
            Test Step 13
            Action: (Continue from step 12)Send EVC-8 with,MMI_Q_TEXT = 265MMI_Q_TEXT_CRITERIA = 3MMI_I_TEXT = 4
            Expected Result: Verify the following information,(1)    DMI displays MO02 symbol in sub-area C1 without yellow flashing frame
            Test Step Comment: (1) MMI_gen 7022 (partly: exclude radio connection symbols);  MMI_gen 1699 (partly: non-acknowledgement, symbol); MMI_gen 147 (partly: symbol, remove the same index, MMI_gen 144 (partly: symbol, the same index); MMI_gen 3005 (partly: exclude radio connection symbols); 
            */

            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 4;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 265;
            EVC8_MMIDriverMessage.Send();

            DmiExpectedResults.Driver_symbol_displayed(this, "Acknowledgement for Shunting", "MO02", "C1",
                false);

            MakeTestStepHeader(14, UniqueIdentifier++,
                "(Continue from step 13)Send EVC-8 with,MMI_Q_TEXT = 266MMI_Q_TEXT_CRITERIA = 3MMI_I_TEXT = 4",
                "Verify the following information,(1)    DMI displays MO05 symbol in sub-area C1 without yellow flashing frame");
            /*
            Test Step 14
            Action: (Continue from step 13)Send EVC-8 with,MMI_Q_TEXT = 266MMI_Q_TEXT_CRITERIA = 3MMI_I_TEXT = 4
            Expected Result: Verify the following information,(1)    DMI displays MO05 symbol in sub-area C1 without yellow flashing frame
            Test Step Comment: (1) MMI_gen 7022 (partly: exclude radio connection symbols);  MMI_gen 1699 (partly: non-acknowledgement, symbol); MMI_gen 147 (partly: symbol, remove the same index, MMI_gen 144 (partly: symbol, the same index); MMI_gen 3005 (partly: exclude radio connection symbols); 
            */

            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 4;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 266;
            EVC8_MMIDriverMessage.Send();

            DmiExpectedResults.Driver_symbol_displayed(this, "Acknowledgement for Trip", "MO05", "C1", false);

            MakeTestStepHeader(15, UniqueIdentifier++,
                "(Continue from step 14)Send EVC-8 with,MMI_Q_TEXT = 709MMI_Q_TEXT_CRITERIA = 3MMI_I_TEXT = 4",
                "Verify the following information,(1)    DMI displays MO22 symbol in sub-area C1 without yellow flashing frame");
            /*
            Test Step 15
            Action: (Continue from step 14)Send EVC-8 with,MMI_Q_TEXT = 709MMI_Q_TEXT_CRITERIA = 3MMI_I_TEXT = 4
            Expected Result: Verify the following information,(1)    DMI displays MO22 symbol in sub-area C1 without yellow flashing frame
            Test Step Comment: (1) MMI_gen 7022 (partly: exclude radio connection symbols);  MMI_gen 1699 (partly: non-acknowledgement, symbol); MMI_gen 147 (partly: symbol, remove the same index, MMI_gen 144 (partly: symbol, the same index); MMI_gen 3005 (partly: exclude radio connection symbols); 
            */

            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 4;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 709;
            EVC8_MMIDriverMessage.Send();

            DmiExpectedResults.Driver_symbol_displayed(this, "Acknowledgement for Limited Supervision", "MO22",
                "C1", false);

            MakeTestStepHeader(16, UniqueIdentifier++,
                "(Continue from step 15)Send EVC-8 with,MMI_Q_TEXT = 257MMI_Q_TEXT_CRITERIA = 3MMI_N_TEXT = 1MMI_X_TEXT = 0MMI_I_TEXT = 4",
                "Verify the following information,(1)    DMI displays LE07 symbol in sub-area C1 without yellow flashing frame");
            /*
            Test Step 16
            Action: (Continue from step 15)Send EVC-8 with,MMI_Q_TEXT = 257MMI_Q_TEXT_CRITERIA = 3MMI_N_TEXT = 1MMI_X_TEXT = 0MMI_I_TEXT = 4
            Expected Result: Verify the following information,(1)    DMI displays LE07 symbol in sub-area C1 without yellow flashing frame
            Test Step Comment: (1) MMI_gen 7022 (partly: exclude radio connection symbols);  MMI_gen 1699 (partly: non-acknowledgement, symbol); MMI_gen 147 (partly: symbol, remove the same index, MMI_gen 144 (partly: symbol, the same index); MMI_gen 3005 (partly: exclude radio connection symbols); 
            */

            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 4;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 257;
            EVC8_MMIDriverMessage.PlainTextMessage = "0"; // Level 0
            EVC8_MMIDriverMessage.Send();

            DmiExpectedResults.Driver_symbol_displayed(this, "Level 0 announcement", "LE07", "C1", false);

            MakeTestStepHeader(17, UniqueIdentifier++,
                "(Continue from step 16)Send EVC-8 with,MMI_Q_TEXT = 257MMI_Q_TEXT_CRITERIA = 3MMI_N_TEXT = 1MMI_X_TEXT = 1MMI_I_TEXT = 4",
                "Verify the following information,(1)    DMI displays LE11 symbol in sub-area C1 without yellow flashing frame");
            /*
            Test Step 17
            Action: (Continue from step 16)Send EVC-8 with,MMI_Q_TEXT = 257MMI_Q_TEXT_CRITERIA = 3MMI_N_TEXT = 1MMI_X_TEXT = 1MMI_I_TEXT = 4
            Expected Result: Verify the following information,(1)    DMI displays LE11 symbol in sub-area C1 without yellow flashing frame
            Test Step Comment: (1) MMI_gen 7022 (partly: exclude radio connection symbols);  MMI_gen 1699 (partly: non-acknowledgement, symbol); MMI_gen 147 (partly: symbol, remove the same index, MMI_gen 144 (partly: symbol, the same index); MMI_gen 3005 (partly: exclude radio connection symbols); 
            */

            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 4;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 257;
            EVC8_MMIDriverMessage.PlainTextMessage = "1"; // Level 1
            EVC8_MMIDriverMessage.Send();

            DmiExpectedResults.Driver_symbol_displayed(this, "Level 1 announcement", "LE11", "C1", false);

            MakeTestStepHeader(18, UniqueIdentifier++,
                "(Continue from step 17)Send EVC-8 with,MMI_Q_TEXT = 257MMI_Q_TEXT_CRITERIA = 3MMI_N_TEXT = 1MMI_X_TEXT = 2MMI_I_TEXT = 4",
                "Verify the following information,(1)    DMI displays LE13 symbol in sub-area C1 without yellow flashing frame");
            /*
            Test Step 18
            Action: (Continue from step 17)Send EVC-8 with,MMI_Q_TEXT = 257MMI_Q_TEXT_CRITERIA = 3MMI_N_TEXT = 1MMI_X_TEXT = 2MMI_I_TEXT = 4
            Expected Result: Verify the following information,(1)    DMI displays LE13 symbol in sub-area C1 without yellow flashing frame
            Test Step Comment: (1) MMI_gen 7022 (partly: exclude radio connection symbols);  MMI_gen 1699 (partly: non-acknowledgement, symbol); MMI_gen 147 (partly: symbol, remove the same index, MMI_gen 144 (partly: symbol, the same index); MMI_gen 3005 (partly: exclude radio connection symbols); 
            */

            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 4;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 257;
            EVC8_MMIDriverMessage.PlainTextMessage = "2"; // Level 2
            EVC8_MMIDriverMessage.Send();

            DmiExpectedResults.Driver_symbol_displayed(this, "Level 2 announcement", "LE13", "C1", false);

            MakeTestStepHeader(19, UniqueIdentifier++,
                "(Continue from step 18)Send EVC-8 with,MMI_Q_TEXT = 257MMI_Q_TEXT_CRITERIA = 3MMI_N_TEXT = 1MMI_X_TEXT = 3MMI_I_TEXT = 4",
                "Verify the following information,(1)    DMI displays LE15 symbol in sub-area C1 without yellow flashing frame");
            /*
            Test Step 19
            Action: (Continue from step 18)Send EVC-8 with,MMI_Q_TEXT = 257MMI_Q_TEXT_CRITERIA = 3MMI_N_TEXT = 1MMI_X_TEXT = 3MMI_I_TEXT = 4
            Expected Result: Verify the following information,(1)    DMI displays LE15 symbol in sub-area C1 without yellow flashing frame
            Test Step Comment: (1) MMI_gen 7022 (partly: exclude radio connection symbols);  MMI_gen 1699 (partly: non-acknowledgement, symbol); MMI_gen 147 (partly: symbol, remove the same index, MMI_gen 144 (partly: symbol, the same index); MMI_gen 3005 (partly: exclude radio connection symbols); 
            */

            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 4;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 257;
            EVC8_MMIDriverMessage.PlainTextMessage = "3"; // Level 3
            EVC8_MMIDriverMessage.Send();

            DmiExpectedResults.Driver_symbol_displayed(this, "Level 0 announcement", "LE15", "C1", false);

            MakeTestStepHeader(20, UniqueIdentifier++,
                "Press ‘Main’ button.Then, use the test script file 15_3_1_1_b.xml to Send EVC-8 with,MMI_Q_TEXT = 716MMI_Q_TEXT_CRITERIA = 3MMI_I_TEXT = 5",
                "Verify the following information,(1)    DMI displays ST05 symbol in the window title area without yellow flashing frame");
            /*
            Test Step 20
            Action: Press ‘Main’ button.Then, use the test script file 15_3_1_1_b.xml to Send EVC-8 with,MMI_Q_TEXT = 716MMI_Q_TEXT_CRITERIA = 3MMI_I_TEXT = 5
            Expected Result: Verify the following information,(1)    DMI displays ST05 symbol in the window title area without yellow flashing frame
            Test Step Comment: (1) MMI_gen 7022 (partly: exclude radio connection symbols);  MMI_gen 3005 (partly: exclude radio connection symbols);  MMI_gen 1699 (partly: non-acknowledgement, symbol);
            */

            DmiActions.ShowInstruction(this, "Please press the \"Main\" button on the DMI.");

            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 5;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 716;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification(
                "Is the ST05 symbol displayed in the window title area without a flashing yellow frame?");

            MakeTestStepHeader(21, UniqueIdentifier++,
                "Use the test script file 15_3_1_1_c.xml to send EVC-8 with,MMI_Q_TEXT_CRITERIA = 4MMI_I_TEXT = 5",
                "Verify the following information,(1)    The symbol ST05 is removed from window title area");
            /*
            Test Step 21
            Action: Use the test script file 15_3_1_1_c.xml to send EVC-8 with,MMI_Q_TEXT_CRITERIA = 4MMI_I_TEXT = 5
            Expected Result: Verify the following information,(1)    The symbol ST05 is removed from window title area
            Test Step Comment: (1) MMI_gen 144 (partly: Symbols, removed by Q_TEXT_CRITERIA);
            */

            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 4;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 5;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 0;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Has the ST05 symbol been removed from the window title area?");

            MakeTestStepHeader(22, UniqueIdentifier++, "End of test", "");

            /*
            Test Step 22
            Action: End of test
            Expected Result: 
            */


            return GlobalTestResult;
        }
    }
}
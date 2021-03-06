using System;
using Testcase.Telegrams.EVCtoDMI;

namespace Testcase.DMITestCases
{
    /// <summary>
    /// 20.3.4 Driver Messages: Undefined value of MMI_Q_TEXT_CRITERIA
    /// TC-ID: 15.3.4
    /// 
    /// This test case verifies the undefined value of variable MMI_Q_TEXT_CRITERIA for symbols/fixed text messages.
    /// 
    /// Tested Requirements:
    /// MMI_gen 7040; MMI_gen 147 (partly: new symbol, MMI_gen 3005); MMI_gen 1699 (partly: The Driver message is displayed as a symbol);
    /// 
    /// Scenario:
    /// At the default screen, use the test script file to send EVC-
    /// 8.Then, verify the display of driver message with undefined values of MMI_Q_TEXT_CRITERIA for a fixed text message.Use the test script file to send EVC-
    /// 8.Then, verify the display of driver message with undefined values of MMI_Q_TEXT_CRITERIA for a symbol.Use the test script file to send EVC-
    /// 8.Then, verify the display of driver message with undefined values of MMI_Q_TEXT_CRITERIA for a driver message with an existing index number.Note: Each step of test script file in executed continuously, Tester need to confim expected result within specific time (5 second).
    /// 
    /// Used files:
    /// 15_3_4.xml
    /// </summary>
    public class TC_15_3_4_Driver_Messages : TestcaseBase
    {
        public override bool TestcaseEntryPoint()
        {
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 23090;
            // Testcase entrypoint
            StartUp();
            DmiActions.Complete_SoM_L1_SB(this);

            // These steps are carried out in XML_15_3_4.cs
            MakeTestStepHeader(1, UniqueIdentifier++,
                "Use the test script file 15_3_4.xml to send EVC-8 with,MMI_Q_TEXT = 527MMI_Q_TEXT_CRITERIA = 5MMI_Q_TEXT_CLASS = 0MMI_I_TEXT = 1",
                "Verify the following information,(1)    DMI displays the driver message ‘Brake Test aborted, perform new Test?’ in sub-area E5 without yellow flashing frame");
            /*
            Test Step 1
            Action: Use the test script file 15_3_4.xml to send EVC-8 with,MMI_Q_TEXT = 527MMI_Q_TEXT_CRITERIA = 5MMI_Q_TEXT_CLASS = 0MMI_I_TEXT = 1
            Expected Result: Verify the following information,(1)    DMI displays the driver message ‘Brake Test aborted, perform new Test?’ in sub-area E5 without yellow flashing frame
            Test Step Comment: (1) MMI_gen 7040 (partly: text message, decision path is not affected, Note 1 under MMI_gen 7040: CRITERIA = 5));
            */

            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.AuxiliaryInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 5;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 527;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the driver message ‘Brake Test aborted, perform new Test?’ in sub-area E5 without yellow flashing frame");

            MakeTestStepHeader(2, UniqueIdentifier++,
                "(Continue from step 1)Send EVC-8 with,MMI_Q_TEXT = 259MMI_Q_TEXT_CRITERIA = 5MMI_Q_TEXT_CLASS = 0MMI_I_TEXT = 2",
                "Verify the following information,(1)    DMI displays the Symbol MO08 in sub-area C1 without yellow flashing frame");
            /*
            Test Step 2
            Action: (Continue from step 1)Send EVC-8 with,MMI_Q_TEXT = 259MMI_Q_TEXT_CRITERIA = 5MMI_Q_TEXT_CLASS = 0MMI_I_TEXT = 2
            Expected Result: Verify the following information,(1)    DMI displays the Symbol MO08 in sub-area C1 without yellow flashing frame
            Test Step Comment: (1) MMI_gen 7040 (partly: symbol, decision path is not affected, Note 1 and Note 2 under MMI_gen 7040: CRITERIA = 5); MMI_gen 1699 (partly: The Driver message is displayed as a symbol); MMI_gen 147 (partly: driver message is a symbol, MMI_gen 3005);
            */

            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 5;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 2;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 259;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Symbol MO08 in sub-area C1 without yellow flashing frame");

            MakeTestStepHeader(3, UniqueIdentifier++,
                "(Continue from step 1)Send EVC-8 with,MMI_Q_TEXT = 263MMI_Q_TEXT_CRITERIA = 2MMI_Q_TEXT_CLASS = 0MMI_I_TEXT = 2",
                "Verify the following information,(1)    DMI displays the Symbol MO10 in sub-area C1 without yellow flashing frame");
            /*
            Test Step 3
            Action: (Continue from step 1)Send EVC-8 with,MMI_Q_TEXT = 263MMI_Q_TEXT_CRITERIA = 2MMI_Q_TEXT_CLASS = 0MMI_I_TEXT = 2
            Expected Result: Verify the following information,(1)    DMI displays the Symbol MO10 in sub-area C1 without yellow flashing frame
            Test Step Comment: (1) MMI_gen 7040 (partly: symbol, decision path is not affected, Note 2 under MMI_gen 7040: CRITERIA = 2); MMI_gen 1699 (partly: The Driver message is displayed as a symbol); MMI_gen 147 (partly: driver message is a symbol, MMI_gen 3005);
            */

            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 2;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 2;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 263;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Symbol MO10 in sub-area C1 without yellow flashing frame");

            TraceHeader("End of test");

            /*
            Test Step 4
            Action: End of test
            Expected Result: 
            */


            return GlobalTestResult;
        }
    }
}
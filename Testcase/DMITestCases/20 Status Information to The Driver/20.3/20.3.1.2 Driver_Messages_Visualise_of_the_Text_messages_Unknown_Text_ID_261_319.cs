using System;
using Testcase.Telegrams.EVCtoDMI;

namespace Testcase.DMITestCases
{
    /// <summary>
    /// 20.3.1.2 Driver Messages: Visualise of the Text messages (Unknown Text ID 261-319)
    /// TC-ID: 15.3.1.2
    /// 
    /// This test case verifies the visualization of text message when DMI received EVC-8 with unknown driver message ID refer to the values of MMI_Q_TEXT in Interface specification.
    /// 
    /// Tested Requirements:
    /// MMI_gen 148 (partly: acknowledging); 
    /// 
    /// Scenario:
    /// At the default window, Use the test script file to send Driver message to DMI. Then, verifies the display information.Note: Each step of test script file in executed continuously, Tester need to confim expected result within specific time (5 second).
    /// 
    /// Used files:
    /// 15_3_1_2.xml
    /// </summary>
    public class TC_15_3_1_2_Driver_Messages : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // DMI is power on.Cabin A is activated.SoM is performed until Level 1 is selected and confirmed.Main window is closed.

            // Call the TestCaseBase PreExecution
            base.PreExecution();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in SB mode, level 1

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 22593;
            // Testcase entrypoint
            StartUp();
            DmiActions.Complete_SoM_L1_SB(this);

            var text_ids = new[]
            {
                261, 270, 271, 272, 281, 283, 284, 285, 287, 288, 289, 291, 293, 294, 295, 297, 301, 302, 303, 304, 306,
                307, 308, 309, 311, 312, 313, 314, 317, 318, 319
            };

            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;

            int teststep = 1;
            foreach (int textId in text_ids)
            {
                MakeTestStepHeader(teststep++, UniqueIdentifier++,
                    "Send EVC-8 with MMI_Q_TEXT = " + textId +
                    ", MMI_Q_TEXT_CRITERIA = 1, MMI_Q_TEXT_CLASS = 1, MMI_I_TEXT = 1",
                    "Verifies the display information as follows,\r\nThe text message ‘’Fixed Text Message " + textId +
                    "’ is display in the area E5.\r\nNo flashing frame display.\r\nThere is no sound played");

                EVC8_MMIDriverMessage.MMI_Q_TEXT = (ushort) textId;
                EVC8_MMIDriverMessage.Send();

                WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                    "1. The text message ‘Fixed Text Message " + textId + "’ is displayed in area E5." +
                                    Environment.NewLine +
                                    "2. No flashing frame is displayed" + Environment.NewLine +
                                    "3. No sound is played.");
            }

            TraceHeader("End of test");

            /*
            Test Step 32
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
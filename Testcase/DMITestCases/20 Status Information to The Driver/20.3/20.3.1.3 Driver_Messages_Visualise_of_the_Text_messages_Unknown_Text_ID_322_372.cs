using System;
using Testcase.Telegrams.EVCtoDMI;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 20.3.1.3 Driver Messages: Visualise of the Text messages (Unknown Text ID 322-372)
    /// TC-ID: 15.3.1.3
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
    /// 15_3_1_3.xml
    /// </summary>
    public class TC_15_3_1_3_Driver_Messages : TestcaseBase
    {
        public override bool TestcaseEntryPoint()
        {
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 22635;
            // Testcase entrypoint
            StartUp();
            DmiActions.Complete_SoM_L1_SB(this);

            var text_ids = new[]
            {
                322, 323, 324, 325, 326, 327, 328, 329, 330, 331, 332, 333, 334, 335, 336, 337, 338, 339, 340, 341, 342,
                343, 344, 345, 346, 347, 348, 349, 350, 351, 352, 353, 354, 355, 356, 357, 358, 359, 360, 361, 362, 363,
                364, 365, 366, 367, 368, 369, 370, 371, 372
            };

            int teststep = 1;
            foreach (var textId in text_ids)
            {
                MakeTestStepHeader(teststep++, UniqueIdentifier++,
                    "Send EVC-8 with MMI_Q_TEXT = " + textId +
                    ", MMI_Q_TEXT_CRITERIA = 1, MMI_Q_TEXT_CLASS = 1, MMI_I_TEXT = 1",
                    "Verifies the display information as follows, The text message ‘’Fixed Text Message " + textId +
                    "’ is display in the area E5. No flashing frame display. There is no sound played");

                EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
                EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
                EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
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
            Test Step 52
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
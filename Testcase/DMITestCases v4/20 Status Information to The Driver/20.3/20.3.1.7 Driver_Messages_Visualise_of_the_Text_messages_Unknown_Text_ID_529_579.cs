using System;
using Testcase.Telegrams.EVCtoDMI;

namespace Testcase.DMITestCases
{
    /// <summary>
    /// 20.3.1.7 Driver Messages: Visualise of the Text messages (Unknown Text ID 529-579)
    /// TC-ID: 15.3.1.7
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
    /// 15_3_1_7.xml
    /// </summary>
    public class TC_15_3_1_7_Driver_Messages : TestcaseBase
    {
        public override bool TestcaseEntryPoint()
        {
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 22874;

            StartUp();
            DmiActions.Complete_SoM_L1_SB(this);

            var text_ids = new[]
            {
                529, 530, 537, 538, 539, 541, 542, 544, 546, 547, 548, 549, 550, 551, 553, 556, 557, 558, 559, 561, 562,
                566, 567, 571, 575, 576, 577, 578, 579
            };

            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;

            int teststep = 1;
            foreach (var textId in text_ids)
            {
                MakeTestStepHeader(teststep++, UniqueIdentifier++,
                    "Send EVC-8 with MMI_Q_TEXT = " + textId +
                    ", MMI_Q_TEXT_CRITERIA = 1, MMI_Q_TEXT_CLASS = 1, MMI_I_TEXT = 1",
                    "Verifies the display information as follows, The text message ‘’Fixed Text Message " + textId +
                    "’ is display in the area E5. No flashing frame display. There is no sound played");

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
            Test Step 30
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
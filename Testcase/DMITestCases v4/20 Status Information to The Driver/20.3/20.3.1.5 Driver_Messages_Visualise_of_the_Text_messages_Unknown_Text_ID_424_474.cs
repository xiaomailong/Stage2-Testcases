using System;
using Testcase.Telegrams.EVCtoDMI;

namespace Testcase.DMITestCases
{
    /// <summary>
    /// 20.3.1.5 Driver Messages: Visualise of the Text messages (Unknown Text ID 424-474)
    /// TC-ID: 15.3.1.5
    /// 
    /// This test case verifies the visualise of text message when DMI received EVC-8 with unknown driver message ID refer to  value of MMI_Q_TEXT in Interface specification.
    /// 
    /// Tested Requirements:
    /// MMI_gen 148 (partly: acknowledging);         
    /// 
    /// Scenario:
    /// At the default window, Use the test script file to send Driver message to DMI. Then, verifies the display information.Note: Each step of test script file in executed continuously, Tester need to confim expected result within specific time (5 second).
    /// 
    /// Used files:
    /// 15_3_1_5.xml
    /// </summary>
    public class TC_15_3_1_5_Driver_Messages : TestcaseBase
    {
        public override bool TestcaseEntryPoint()
        {
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 22759;
            // Testcase entrypoint
            StartUp();
            DmiActions.Complete_SoM_L1_SB(this);

            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;

            int teststep = 1;
            for (var textId = 424; textId <= 473; textId++)
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
            Test Step 52
            Action: End of test
            Expected Result: 
            */


            return GlobalTestResult;
        }
    }
}
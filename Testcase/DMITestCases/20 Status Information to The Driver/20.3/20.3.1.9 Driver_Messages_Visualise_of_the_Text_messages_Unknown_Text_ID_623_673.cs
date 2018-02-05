using System;
using Testcase.Telegrams.EVCtoDMI;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 20.3.1.9 Driver Messages: Visualise of the Text messages (Unknown Text ID 623-673)
    /// TC-ID: 15.3.1.9
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
    /// 15_3_1_9.xml
    /// </summary>
    public class TC_15_3_1_9_Driver_Messages : TestcaseBase
    {

        public override bool TestcaseEntryPoint()
        {
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 0;
            // Testcase entrypoint
            StartUp();
            DmiActions.Complete_SoM_L1_SB(this);

            // There is a conflict between xml and spec: xml step 2 tests MMI_Q_TEXT = 624 which is not an invalid value. There are 36 steps in the xml
            // and 35 in the spec. Xml step 2 is omitted as per spec.

            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;

            int teststep = 1;
            for (int i = 640; i <= 673; i++)
            {
                MakeTestStepHeader(teststep, UniqueIdentifier++, "Send EVC-8 with MMI_Q_TEXT = 623, MMI_Q_TEXT_CRITERIA = 1, MMI_Q_TEXT_CLASS = 1, MMI_I_TEXT = 1", "Verifies the display information as follows,The text message ‘’Fixed Text Message " + i +
                                                                                                                                                                    "’ is display in the area E5.No flashing frame display.There is no sound played");
                EVC8_MMIDriverMessage.MMI_Q_TEXT = (ushort) i;
                EVC8_MMIDriverMessage.Send();

                WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                    "1. The text message ‘Fixed Text Message " + i + "’ is displayed in area E5." +
                                    Environment.NewLine +
                                    "2. No flashing frame is displayed" + Environment.NewLine +
                                    "3. No sound is played.");
            }


            TraceHeader("Test Step " + teststep);
            TraceReport("Action");
            TraceInfo("End of test");


            return GlobalTestResult;
        }
    }
}
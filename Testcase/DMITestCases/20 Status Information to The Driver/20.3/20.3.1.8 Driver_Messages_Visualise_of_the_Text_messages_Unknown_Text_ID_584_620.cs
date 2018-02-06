using System;
using Testcase.Telegrams.EVCtoDMI;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 20.3.1.8 Driver Messages: Visualise of the Text messages (Unknown Text ID 584-620)
    /// TC-ID: 15.3.1.8
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
    /// 15_3_1_8.xml
    /// </summary>
    public class TC_15_3_1_8_Driver_Messages : TestcaseBase
    {
        public override bool TestcaseEntryPoint()
        {
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 0;
            // Testcase entrypoint

            StartUp();
            DmiActions.Complete_SoM_L1_SB(this);

            XML_15_3_1_8();
            // All the following steps are carried out in XML_15_3_1_8.cs
            MakeTestStepHeader(1, UniqueIdentifier++,
                "Use the test script file 15_3_1_8.xml to send EVC-8 with,MMI_Q_TEXT = 584MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1",
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 584’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 1
            Action: Use the test script file 15_3_1_8.xml to send EVC-8 with,MMI_Q_TEXT = 584MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 584’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            MakeTestStepHeader(2, UniqueIdentifier++,
                "Send EVC-8 with,MMI_Q_TEXT = 585MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1",
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 585’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 2
            Action: Send EVC-8 with,MMI_Q_TEXT = 585MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 585’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            MakeTestStepHeader(3, UniqueIdentifier++,
                "Send EVC-8 with,MMI_Q_TEXT = 586MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1",
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 586’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 3
            Action: Send EVC-8 with,MMI_Q_TEXT = 586MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 586’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            MakeTestStepHeader(4, UniqueIdentifier++,
                "Send EVC-8 with,MMI_Q_TEXT = 587MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1",
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 587’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 4
            Action: Send EVC-8 with,MMI_Q_TEXT = 587MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 587’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            MakeTestStepHeader(5, UniqueIdentifier++,
                "Send EVC-8 with,MMI_Q_TEXT = 588MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1",
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 588’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 5
            Action: Send EVC-8 with,MMI_Q_TEXT = 588MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 588’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            MakeTestStepHeader(6, UniqueIdentifier++,
                "Send EVC-8 with,MMI_Q_TEXT = 589MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 17MMI_I_TEXT = 1",
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 589’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 6
            Action: Send EVC-8 with,MMI_Q_TEXT = 589MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 17MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 589’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            MakeTestStepHeader(7, UniqueIdentifier++,
                "Send EVC-8 with,MMI_Q_TEXT = 590MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1",
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 590’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 7
            Action: Send EVC-8 with,MMI_Q_TEXT = 590MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 590’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            MakeTestStepHeader(8, UniqueIdentifier++,
                "Send EVC-8 with,MMI_Q_TEXT = 591MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1",
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 591’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 8
            Action: Send EVC-8 with,MMI_Q_TEXT = 591MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 591’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            MakeTestStepHeader(9, UniqueIdentifier++,
                "Send EVC-8 with,MMI_Q_TEXT = 592MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1",
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 592’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 9
            Action: Send EVC-8 with,MMI_Q_TEXT = 592MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 592’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            MakeTestStepHeader(10, UniqueIdentifier++,
                "Send EVC-8 with,MMI_Q_TEXT = 593MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1",
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 593’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 10
            Action: Send EVC-8 with,MMI_Q_TEXT = 593MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 593’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            MakeTestStepHeader(11, UniqueIdentifier++,
                "Send EVC-8 with,MMI_Q_TEXT = 594MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1",
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 594’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 11
            Action: Send EVC-8 with,MMI_Q_TEXT = 594MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 594’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            MakeTestStepHeader(12, UniqueIdentifier++,
                "Send EVC-8 with,MMI_Q_TEXT = 595MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1",
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 595’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 12
            Action: Send EVC-8 with,MMI_Q_TEXT = 595MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 595’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            MakeTestStepHeader(13, UniqueIdentifier++,
                "Send EVC-8 with,MMI_Q_TEXT = 596MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1",
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 596’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 13
            Action: Send EVC-8 with,MMI_Q_TEXT = 596MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 596’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            MakeTestStepHeader(14, UniqueIdentifier++,
                "Send EVC-8 with,MMI_Q_TEXT = 597MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1",
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 597’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 14
            Action: Send EVC-8 with,MMI_Q_TEXT = 597MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 597’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            MakeTestStepHeader(15, UniqueIdentifier++,
                "Send EVC-8 with,MMI_Q_TEXT = 598MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1",
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 598’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 15
            Action: Send EVC-8 with,MMI_Q_TEXT = 598MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 598’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            MakeTestStepHeader(16, UniqueIdentifier++,
                "Send EVC-8 with,MMI_Q_TEXT = 599MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1",
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 599’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 16
            Action: Send EVC-8 with,MMI_Q_TEXT = 599MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 599’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            MakeTestStepHeader(17, UniqueIdentifier++,
                "Send EVC-8 with,MMI_Q_TEXT = 600MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1",
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 600’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 17
            Action: Send EVC-8 with,MMI_Q_TEXT = 600MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 600’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            MakeTestStepHeader(18, UniqueIdentifier++,
                "Send EVC-8 with,MMI_Q_TEXT = 601MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1",
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 601’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 18
            Action: Send EVC-8 with,MMI_Q_TEXT = 601MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 601’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            MakeTestStepHeader(19, UniqueIdentifier++,
                "Send EVC-8 with,MMI_Q_TEXT = 602MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1",
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 602’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 19
            Action: Send EVC-8 with,MMI_Q_TEXT = 602MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 602’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            MakeTestStepHeader(20, UniqueIdentifier++,
                "Send EVC-8 with,MMI_Q_TEXT = 603MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1",
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 603’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 20
            Action: Send EVC-8 with,MMI_Q_TEXT = 603MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 603’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            MakeTestStepHeader(21, UniqueIdentifier++,
                "Send EVC-8 with,MMI_Q_TEXT = 604MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1",
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 604’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 21
            Action: Send EVC-8 with,MMI_Q_TEXT = 604MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 604’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            MakeTestStepHeader(22, UniqueIdentifier++,
                "Send EVC-8 with,MMI_Q_TEXT = 605MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1",
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 605’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 22
            Action: Send EVC-8 with,MMI_Q_TEXT = 605MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 605’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            MakeTestStepHeader(23, UniqueIdentifier++,
                "Send EVC-8 with,MMI_Q_TEXT = 607MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1",
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 607’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 23
            Action: Send EVC-8 with,MMI_Q_TEXT = 607MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 607’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            MakeTestStepHeader(24, UniqueIdentifier++,
                "Send EVC-8 with,MMI_Q_TEXT = 608MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1",
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 608’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 24
            Action: Send EVC-8 with,MMI_Q_TEXT = 608MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 608’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            MakeTestStepHeader(25, UniqueIdentifier++,
                "Send EVC-8 with,MMI_Q_TEXT = 616MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1",
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 616’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 25
            Action: Send EVC-8 with,MMI_Q_TEXT = 616MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 616’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            MakeTestStepHeader(26, UniqueIdentifier++,
                "Send EVC-8 with,MMI_Q_TEXT = 617MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1",
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 617’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 26
            Action: Send EVC-8 with,MMI_Q_TEXT = 617MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 617’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            MakeTestStepHeader(27, UniqueIdentifier++,
                "Send EVC-8 with,MMI_Q_TEXT = 618MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1",
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 618’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 27
            Action: Send EVC-8 with,MMI_Q_TEXT = 618MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 618’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            MakeTestStepHeader(28, UniqueIdentifier++,
                "Send EVC-8 with,MMI_Q_TEXT = 619MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1",
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 619’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 28
            Action: Send EVC-8 with,MMI_Q_TEXT = 619MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 619’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            MakeTestStepHeader(29, UniqueIdentifier++,
                "Send EVC-8 with,MMI_Q_TEXT = 620MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1",
                "Verifies the display information as follows,The text msage ‘’Fixed Text Message 620’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 29
            Action: Send EVC-8 with,MMI_Q_TEXT = 620MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text msage ‘’Fixed Text Message 620’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            MakeTestStepHeader(30, UniqueIdentifier++, "End of test", "");

            /*
            Test Step 30
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }

        #region Send_XML_15_3_1_8_DMI_Test_Specification

        private void XML_15_3_1_8()
        {
            /// *************** Discrepancy between 15_3_1_8.xml and spec in MMI_Q_TEXT_CRITERIA
            ///                                         3    <=>       1

            // Step 1
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 584;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 584’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");


            // Step 2
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 585;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 585’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 3
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 586;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 586’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 4
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 587;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 587’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 5
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 588;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 588’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 6
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 589;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 589’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 7
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 590;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 590’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 8
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 591;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 591’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 9
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 592;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 592’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 10
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 593;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 593’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 11
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 594;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 594’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 12
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 595;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 595’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 13
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 596;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 596’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 14
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 597;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 597’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 15
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 598;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 598’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 16
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 599;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 599’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 17
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 600;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 600’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 18
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 601;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 601’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 19
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 602;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 602’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 20
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 603;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 603’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 21
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 604;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 604’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 22
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 605;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 605’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 23
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 607;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 607’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 24
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 608;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 608’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 25
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 616;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 616’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 26
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 617;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 617’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 27
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 618;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 618’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 28
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 619;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 619’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 29
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 620;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 620’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");
        }

        #endregion
    }
}
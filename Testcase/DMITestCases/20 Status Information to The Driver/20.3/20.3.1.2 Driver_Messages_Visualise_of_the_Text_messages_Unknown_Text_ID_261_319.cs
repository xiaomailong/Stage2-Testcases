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

            // All the following tests are done in XML_15_3_1_2.cs
            MakeTestStepHeader(1, UniqueIdentifier++,
                "Use the test script file 15_3_1_2.xml to send EVC-8 with,MMI_Q_TEXT = 261MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1",
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 261’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 1
            Action: Use the test script file 15_3_1_2.xml to send EVC-8 with,MMI_Q_TEXT = 261MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 261’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */
            XML_15_3_1_2();

            MakeTestStepHeader(2, UniqueIdentifier++,
                "Send EVC-8 with,MMI_Q_TEXT = 270MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1",
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 270’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 2
            Action: Send EVC-8 with,MMI_Q_TEXT = 270MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 270’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            MakeTestStepHeader(3, UniqueIdentifier++,
                "Send EVC-8 with,MMI_Q_TEXT = 271MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1",
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 271’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 3
            Action: Send EVC-8 with,MMI_Q_TEXT = 271MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 271’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            MakeTestStepHeader(4, UniqueIdentifier++,
                "Send EVC-8 with,MMI_Q_TEXT = 272MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1",
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 272’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 4
            Action: Send EVC-8 with,MMI_Q_TEXT = 272MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 272’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            MakeTestStepHeader(5, UniqueIdentifier++,
                "Send EVC-8 with,MMI_Q_TEXT = 281MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1",
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 281’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 5
            Action: Send EVC-8 with,MMI_Q_TEXT = 281MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 281’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            MakeTestStepHeader(6, UniqueIdentifier++,
                "Send EVC-8 with,MMI_Q_TEXT = 283MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1",
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 283’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 6
            Action: Send EVC-8 with,MMI_Q_TEXT = 283MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 283’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            MakeTestStepHeader(7, UniqueIdentifier++,
                "Send EVC-8 with,MMI_Q_TEXT = 284MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1",
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 284’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 7
            Action: Send EVC-8 with,MMI_Q_TEXT = 284MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 284’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            MakeTestStepHeader(8, UniqueIdentifier++,
                "Send EVC-8 with,MMI_Q_TEXT = 285MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1",
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 285’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 8
            Action: Send EVC-8 with,MMI_Q_TEXT = 285MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 285’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            MakeTestStepHeader(9, UniqueIdentifier++,
                "Send EVC-8 with,MMI_Q_TEXT = 287MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1",
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 287’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 9
            Action: Send EVC-8 with,MMI_Q_TEXT = 287MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 287’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            MakeTestStepHeader(10, UniqueIdentifier++,
                "Send EVC-8 with,MMI_Q_TEXT = 288MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1",
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 288’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 10
            Action: Send EVC-8 with,MMI_Q_TEXT = 288MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 288’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            MakeTestStepHeader(11, UniqueIdentifier++,
                "Send EVC-8 with,MMI_Q_TEXT = 289MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1",
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 289’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 11
            Action: Send EVC-8 with,MMI_Q_TEXT = 289MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 289’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            MakeTestStepHeader(12, UniqueIdentifier++,
                "Send EVC-8 with,MMI_Q_TEXT = 291MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1",
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 291’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 12
            Action: Send EVC-8 with,MMI_Q_TEXT = 291MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 291’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            MakeTestStepHeader(13, UniqueIdentifier++,
                "Send EVC-8 with,MMI_Q_TEXT = 293MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1",
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 293’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 13
            Action: Send EVC-8 with,MMI_Q_TEXT = 293MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 293’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            MakeTestStepHeader(14, UniqueIdentifier++,
                "Send EVC-8 with,MMI_Q_TEXT = 294MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1",
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 294’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 14
            Action: Send EVC-8 with,MMI_Q_TEXT = 294MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 294’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            MakeTestStepHeader(15, UniqueIdentifier++,
                "Send EVC-8 with,MMI_Q_TEXT = 295MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1",
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 295’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 15
            Action: Send EVC-8 with,MMI_Q_TEXT = 295MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 295’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            MakeTestStepHeader(16, UniqueIdentifier++,
                "Send EVC-8 with,MMI_Q_TEXT = 297MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1",
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 297’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 16
            Action: Send EVC-8 with,MMI_Q_TEXT = 297MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 297’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            MakeTestStepHeader(17, UniqueIdentifier++,
                "Send EVC-8 with,MMI_Q_TEXT = 301MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1",
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 301’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 17
            Action: Send EVC-8 with,MMI_Q_TEXT = 301MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 301’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            MakeTestStepHeader(18, UniqueIdentifier++,
                "Send EVC-8 with,MMI_Q_TEXT = 302MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1",
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 302’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 18
            Action: Send EVC-8 with,MMI_Q_TEXT = 302MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 302’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            MakeTestStepHeader(19, UniqueIdentifier++,
                "Send EVC-8 with,MMI_Q_TEXT = 303MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1",
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 303’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 19
            Action: Send EVC-8 with,MMI_Q_TEXT = 303MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 303’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            MakeTestStepHeader(20, UniqueIdentifier++,
                "Send EVC-8 with,MMI_Q_TEXT = 304MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1",
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 304’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 20
            Action: Send EVC-8 with,MMI_Q_TEXT = 304MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 304’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            MakeTestStepHeader(21, UniqueIdentifier++,
                "Send EVC-8 with,MMI_Q_TEXT = 306MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1",
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 306’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 21
            Action: Send EVC-8 with,MMI_Q_TEXT = 306MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 306’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            MakeTestStepHeader(22, UniqueIdentifier++,
                "Send EVC-8 with,MMI_Q_TEXT = 307MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1",
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 307’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 22
            Action: Send EVC-8 with,MMI_Q_TEXT = 307MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 307’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            MakeTestStepHeader(23, UniqueIdentifier++,
                "Send EVC-8 with,MMI_Q_TEXT = 308MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1",
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 308’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 23
            Action: Send EVC-8 with,MMI_Q_TEXT = 308MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 308’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            MakeTestStepHeader(24, UniqueIdentifier++,
                "Send EVC-8 with,MMI_Q_TEXT = 309MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1",
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 309’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 24
            Action: Send EVC-8 with,MMI_Q_TEXT = 309MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 309’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            MakeTestStepHeader(25, UniqueIdentifier++,
                "Send EVC-8 with,MMI_Q_TEXT = 311MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1",
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 311’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 25
            Action: Send EVC-8 with,MMI_Q_TEXT = 311MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 311’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            MakeTestStepHeader(26, UniqueIdentifier++,
                "Send EVC-8 with,MMI_Q_TEXT = 312MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1",
                "Verifies the display information as follows,1.    The text message ‘’Fixed Text Message 312’ is display in the area E5.2.    No flashing frame display.3.    There is no sound played");
            /*
            Test Step 26
            Action: Send EVC-8 with,MMI_Q_TEXT = 312MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,1.    The text message ‘’Fixed Text Message 312’ is display in the area E5.2.    No flashing frame display.3.    There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            MakeTestStepHeader(27, UniqueIdentifier++,
                "Send EVC-8 with,MMI_Q_TEXT = 313MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1",
                "Verifies the display information as follows,1.     The text message ‘’Fixed Text Message 313’ is display in the area E5.2.    No flashing frame display.3.    There is no sound played");
            /*
            Test Step 27
            Action: Send EVC-8 with,MMI_Q_TEXT = 313MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,1.     The text message ‘’Fixed Text Message 313’ is display in the area E5.2.    No flashing frame display.3.    There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            MakeTestStepHeader(28, UniqueIdentifier++,
                "Send EVC-8 with,MMI_Q_TEXT = 314MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1",
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 314’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 28
            Action: Send EVC-8 with,MMI_Q_TEXT = 314MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 314’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            MakeTestStepHeader(29, UniqueIdentifier++,
                "Send EVC-8 with,MMI_Q_TEXT = 317MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1",
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 317’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 29
            Action: Send EVC-8 with,MMI_Q_TEXT = 317MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 317’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            MakeTestStepHeader(30, UniqueIdentifier++,
                "Send EVC-8 with,MMI_Q_TEXT = 318MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1",
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 318’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 30
            Action: Send EVC-8 with,MMI_Q_TEXT = 318MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 318’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            MakeTestStepHeader(31, UniqueIdentifier++,
                "Send EVC-8 with,MMI_Q_TEXT = 319MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1",
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 319’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 31
            Action: Send EVC-8 with,MMI_Q_TEXT = 319MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 319’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            MakeTestStepHeader(32, UniqueIdentifier++, "End of test", "");

            /*
            Test Step 32
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }

        #region Send_XML_15_3_1_2_DMI_Test_Specification

        private void XML_15_3_1_2()
        {
            /// *************** Discrepancy between 15_3_1_2.xml and spec in MMI_Q_TEXT_CRITERIA
            ///                                         3    <=>       1


            // Step 1
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 261;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 261’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 2
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 270;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 270’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 3
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 271;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 271’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 4
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 272;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 272’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 5
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 281;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 281’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 6
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 283;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 283’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 7
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 284;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 284’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 8
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 285;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 285’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 9
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 287;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 287’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 10
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 288;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 288’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 11
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 289;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 289’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 12
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 291;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 291’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 13
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 293;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 293’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 14
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 294;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 288’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 15
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 295;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 295’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 16
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 297;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 297’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 17
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 301;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 301’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 18
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 302;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 302’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 19
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 303;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 303’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 20
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 304;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 304’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 21
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 306;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 306’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 22
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 307;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 307’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 23
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 308;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 308’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 24
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 309;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 309’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 25
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 311;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 311’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 26
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 312;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 312’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 27
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 313;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 313’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 28
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 314;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 314’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 29
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 317;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 317’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 30
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 318;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 318’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 31
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 319;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 319’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");
        }

        #endregion
    }
}
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
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:

            // Call the TestCaseBase PreExecution
            base.PreExecution();
            // DMI is power on.Cabin A is activated.SoM is perform until Level 1 is selected and confirmed.Main window is closed.
            DmiActions.Complete_SoM_L1_SB(this);
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
            UniqueIdentifier = 0;
            // Testcase entrypoint


            XML_15_3_1_7();
            // All the following steps are carried out in XML_15_3_1_7.cs
            TraceHeader("Test Step 1");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Use the test script file 15_3_1_7.xml to send EVC-8 with,MMI_Q_TEXT = 529MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1");
            TraceReport("Expected Result");
            TraceInfo(
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 529’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 1
            Action: Use the test script file 15_3_1_7.xml to send EVC-8 with,MMI_Q_TEXT = 529MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 529’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            TraceHeader("Test Step 2");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Send EVC-8 with,MMI_Q_TEXT = 530MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1");
            TraceReport("Expected Result");
            TraceInfo(
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 530’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 2
            Action: Send EVC-8 with,MMI_Q_TEXT = 530MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 530’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            TraceHeader("Test Step 3");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Send EVC-8 with,MMI_Q_TEXT = 537MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1");
            TraceReport("Expected Result");
            TraceInfo(
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 537’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 3
            Action: Send EVC-8 with,MMI_Q_TEXT = 537MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 537’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            TraceHeader("Test Step 4");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Send EVC-8 with,MMI_Q_TEXT = 538MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1");
            TraceReport("Expected Result");
            TraceInfo(
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 538’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 4
            Action: Send EVC-8 with,MMI_Q_TEXT = 538MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 538’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            TraceHeader("Test Step 5");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Send EVC-8 with,MMI_Q_TEXT = 539MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1");
            TraceReport("Expected Result");
            TraceInfo(
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 539’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 5
            Action: Send EVC-8 with,MMI_Q_TEXT = 539MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 539’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            TraceHeader("Test Step 6");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Send EVC-8 with,MMI_Q_TEXT = 541MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1");
            TraceReport("Expected Result");
            TraceInfo(
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 541’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 6
            Action: Send EVC-8 with,MMI_Q_TEXT = 541MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 541’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            TraceHeader("Test Step 7");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Send EVC-8 with,MMI_Q_TEXT = 542MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1");
            TraceReport("Expected Result");
            TraceInfo(
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 542’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 7
            Action: Send EVC-8 with,MMI_Q_TEXT = 542MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 542’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            TraceHeader("Test Step 8");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Send EVC-8 with,MMI_Q_TEXT = 544MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1");
            TraceReport("Expected Result");
            TraceInfo(
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 544’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 8
            Action: Send EVC-8 with,MMI_Q_TEXT = 544MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 544’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            TraceHeader("Test Step 9");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Send EVC-8 with,MMI_Q_TEXT = 546MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1");
            TraceReport("Expected Result");
            TraceInfo(
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 546’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 9
            Action: Send EVC-8 with,MMI_Q_TEXT = 546MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 546’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            TraceHeader("Test Step 10");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Send EVC-8 with,MMI_Q_TEXT = 547MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1");
            TraceReport("Expected Result");
            TraceInfo(
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 547’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 10
            Action: Send EVC-8 with,MMI_Q_TEXT = 547MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 547’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            TraceHeader("Test Step 11");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Send EVC-8 with,MMI_Q_TEXT = 548MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1");
            TraceReport("Expected Result");
            TraceInfo(
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 548’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 11
            Action: Send EVC-8 with,MMI_Q_TEXT = 548MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 548’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            TraceHeader("Test Step 12");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Send EVC-8 with,MMI_Q_TEXT = 549MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1");
            TraceReport("Expected Result");
            TraceInfo(
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 549’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 12
            Action: Send EVC-8 with,MMI_Q_TEXT = 549MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 549’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            TraceHeader("Test Step 13");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Send EVC-8 with,MMI_Q_TEXT = 550MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1");
            TraceReport("Expected Result");
            TraceInfo(
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 550’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 13
            Action: Send EVC-8 with,MMI_Q_TEXT = 550MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 550’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            TraceHeader("Test Step 14");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Send EVC-8 with,MMI_Q_TEXT = 551MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1");
            TraceReport("Expected Result");
            TraceInfo(
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 551’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 14
            Action: Send EVC-8 with,MMI_Q_TEXT = 551MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 551’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            TraceHeader("Test Step 15");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Send EVC-8 with,MMI_Q_TEXT = 553MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1");
            TraceReport("Expected Result");
            TraceInfo(
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 553’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 15
            Action: Send EVC-8 with,MMI_Q_TEXT = 553MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 553’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            TraceHeader("Test Step 16");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Send EVC-8 with,MMI_Q_TEXT = 556MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1");
            TraceReport("Expected Result");
            TraceInfo(
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 556’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 16
            Action: Send EVC-8 with,MMI_Q_TEXT = 556MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 556’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            TraceHeader("Test Step 17");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Send EVC-8 with,MMI_Q_TEXT = 557MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1");
            TraceReport("Expected Result");
            TraceInfo(
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 557’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 17
            Action: Send EVC-8 with,MMI_Q_TEXT = 557MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 557’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            TraceHeader("Test Step 18");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Send EVC-8 with,MMI_Q_TEXT = 558MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1");
            TraceReport("Expected Result");
            TraceInfo(
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 558’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 18
            Action: Send EVC-8 with,MMI_Q_TEXT = 558MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 558’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            TraceHeader("Test Step 19");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Send EVC-8 with,MMI_Q_TEXT = 559MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1");
            TraceReport("Expected Result");
            TraceInfo(
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 559’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 19
            Action: Send EVC-8 with,MMI_Q_TEXT = 559MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 559’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            TraceHeader("Test Step 20");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Send EVC-8 with,MMI_Q_TEXT = 561MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1");
            TraceReport("Expected Result");
            TraceInfo(
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 561’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 20
            Action: Send EVC-8 with,MMI_Q_TEXT = 561MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 561’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            TraceHeader("Test Step 21");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Send EVC-8 with,MMI_Q_TEXT = 562MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1");
            TraceReport("Expected Result");
            TraceInfo(
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 562’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 21
            Action: Send EVC-8 with,MMI_Q_TEXT = 562MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 562’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            TraceHeader("Test Step 22");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Send EVC-8 with,MMI_Q_TEXT = 566MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1");
            TraceReport("Expected Result");
            TraceInfo(
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 566’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 22
            Action: Send EVC-8 with,MMI_Q_TEXT = 566MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 566’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            TraceHeader("Test Step 23");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Send EVC-8 with,MMI_Q_TEXT = 567MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1");
            TraceReport("Expected Result");
            TraceInfo(
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 567’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 23
            Action: Send EVC-8 with,MMI_Q_TEXT = 567MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 567’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            TraceHeader("Test Step 24");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Send EVC-8 with,MMI_Q_TEXT = 571MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1");
            TraceReport("Expected Result");
            TraceInfo(
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 571’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 24
            Action: Send EVC-8 with,MMI_Q_TEXT = 571MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 571’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            TraceHeader("Test Step 25");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Send EVC-8 with,MMI_Q_TEXT = 575MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1");
            TraceReport("Expected Result");
            TraceInfo(
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 575’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 25
            Action: Send EVC-8 with,MMI_Q_TEXT = 575MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 575’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            TraceHeader("Test Step 26");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Send EVC-8 with,MMI_Q_TEXT = 576MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1");
            TraceReport("Expected Result");
            TraceInfo(
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 576’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 26
            Action: Send EVC-8 with,MMI_Q_TEXT = 576MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 576’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            TraceHeader("Test Step 27");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Send EVC-8 with,MMI_Q_TEXT = 577MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1");
            TraceReport("Expected Result");
            TraceInfo(
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 577’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 27
            Action: Send EVC-8 with,MMI_Q_TEXT = 577MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 577’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            TraceHeader("Test Step 28");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Send EVC-8 with,MMI_Q_TEXT = 578MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1");
            TraceReport("Expected Result");
            TraceInfo(
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 578’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 28
            Action: Send EVC-8 with,MMI_Q_TEXT = 578MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 578’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            TraceHeader("Test Step 29");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Send EVC-8 with,MMI_Q_TEXT = 579MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1");
            TraceReport("Expected Result");
            TraceInfo(
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 579’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 29
            Action: Send EVC-8 with,MMI_Q_TEXT = 579MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 579’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            TraceHeader("Test Step 30");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("End of test");
            TraceReport("Expected Result");
            TraceInfo("");
            /*
            Test Step 30
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }

        #region Send_XML_15_3_1_7_DMI_Test_Specification

        private void XML_15_3_1_7()
        {
            /// *************** Discrepancy between 15_3_1_7.xml and spec in MMI_Q_TEXT_CRITERIA
            ///                                         3    <=>       1

            // Step 1
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 529;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 529’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");


            // Step 2
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 530;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 530’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 3
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 537;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 537’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 4
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 538;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 538’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 5
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 539;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 539’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 6
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 541;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 541’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 7
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 542;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 542’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 8
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 544;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 544’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 9
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 546;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 546’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 10
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 547;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 547’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 11
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 548;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 548’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 12
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 549;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 549’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 13
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 550;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 550’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 14
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 551;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 551’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 15
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 553;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 553’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 16
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 556;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 556’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 17
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 557;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 557’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 18
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 558;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 558’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 19
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 559;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 559’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 20
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 561;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 561’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 21
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 562;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 562’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 22
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 566;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 566’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 23
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 567;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 567’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 24
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 571;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 571’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 25
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 575;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 575’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 26
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 576;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 576’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 27
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 577;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 577’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 28
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 578;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 578’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 29
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 579;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 579’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");
        }

        #endregion
    }
}
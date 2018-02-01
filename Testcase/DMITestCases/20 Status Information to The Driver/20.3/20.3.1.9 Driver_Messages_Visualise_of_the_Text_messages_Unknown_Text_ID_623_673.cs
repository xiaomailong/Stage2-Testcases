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

            XML_15_3_1_9();
            // All the following steps are carried out in XML_15_3_1_9.cs
            TraceHeader("Test Step 1");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Use the test script file 15_3_1_9.xml to send EVC-8 with,MMI_Q_TEXT = 623MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1");
            TraceReport("Expected Result");
            TraceInfo(
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 623’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 1
            Action: Use the test script file 15_3_1_9.xml to send EVC-8 with,MMI_Q_TEXT = 623MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 623’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            TraceHeader("Test Step 2");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Send EVC-8 with,MMI_Q_TEXT = 640MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1");
            TraceReport("Expected Result");
            TraceInfo(
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 640’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 2
            Action: Send EVC-8 with,MMI_Q_TEXT = 640MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 640’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            TraceHeader("Test Step 3");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Send EVC-8 with,MMI_Q_TEXT = 641MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1");
            TraceReport("Expected Result");
            TraceInfo(
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 641’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 3
            Action: Send EVC-8 with,MMI_Q_TEXT = 641MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 641’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            TraceHeader("Test Step 4");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Send EVC-8 with,MMI_Q_TEXT = 642MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1");
            TraceReport("Expected Result");
            TraceInfo(
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 642’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 4
            Action: Send EVC-8 with,MMI_Q_TEXT = 642MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 642’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            TraceHeader("Test Step 5");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Send EVC-8 with,MMI_Q_TEXT = 643MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1");
            TraceReport("Expected Result");
            TraceInfo(
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 643’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 5
            Action: Send EVC-8 with,MMI_Q_TEXT = 643MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 643’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            TraceHeader("Test Step 6");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Send EVC-8 with,MMI_Q_TEXT = 644MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1");
            TraceReport("Expected Result");
            TraceInfo(
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 644’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 6
            Action: Send EVC-8 with,MMI_Q_TEXT = 644MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 644’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            TraceHeader("Test Step 7");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Send EVC-8 with,MMI_Q_TEXT = 645MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1");
            TraceReport("Expected Result");
            TraceInfo(
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 645’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 7
            Action: Send EVC-8 with,MMI_Q_TEXT = 645MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 645’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            TraceHeader("Test Step 8");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Send EVC-8 with,MMI_Q_TEXT = 646MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1");
            TraceReport("Expected Result");
            TraceInfo(
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 646’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 8
            Action: Send EVC-8 with,MMI_Q_TEXT = 646MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 646’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            TraceHeader("Test Step 9");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Send EVC-8 with,MMI_Q_TEXT = 647MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1");
            TraceReport("Expected Result");
            TraceInfo(
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 647’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 9
            Action: Send EVC-8 with,MMI_Q_TEXT = 647MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 647’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            TraceHeader("Test Step 10");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Send EVC-8 with,MMI_Q_TEXT = 648MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1");
            TraceReport("Expected Result");
            TraceInfo(
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 648’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 10
            Action: Send EVC-8 with,MMI_Q_TEXT = 648MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 648’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            TraceHeader("Test Step 11");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Send EVC-8 with,MMI_Q_TEXT = 649MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1");
            TraceReport("Expected Result");
            TraceInfo(
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 649’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 11
            Action: Send EVC-8 with,MMI_Q_TEXT = 649MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 649’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            TraceHeader("Test Step 12");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Send EVC-8 with,MMI_Q_TEXT = 650MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1");
            TraceReport("Expected Result");
            TraceInfo(
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 650’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 12
            Action: Send EVC-8 with,MMI_Q_TEXT = 650MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 650’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            TraceHeader("Test Step 13");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Send EVC-8 with,MMI_Q_TEXT = 651MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1");
            TraceReport("Expected Result");
            TraceInfo(
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 651’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 13
            Action: Send EVC-8 with,MMI_Q_TEXT = 651MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 651’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            TraceHeader("Test Step 14");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Send EVC-8 with,MMI_Q_TEXT = 652MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1");
            TraceReport("Expected Result");
            TraceInfo(
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 652’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 14
            Action: Send EVC-8 with,MMI_Q_TEXT = 652MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 652’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            TraceHeader("Test Step 15");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Send EVC-8 with,MMI_Q_TEXT = 653MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1");
            TraceReport("Expected Result");
            TraceInfo(
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 653’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 15
            Action: Send EVC-8 with,MMI_Q_TEXT = 653MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 653’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            TraceHeader("Test Step 16");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Send EVC-8 with,MMI_Q_TEXT = 654MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1");
            TraceReport("Expected Result");
            TraceInfo(
                "Verifies the display information as follows,The text msage ‘’Fixed Text Message 654’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 16
            Action: Send EVC-8 with,MMI_Q_TEXT = 654MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text msage ‘’Fixed Text Message 654’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            TraceHeader("Test Step 17");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Send EVC-8 with,MMI_Q_TEXT = 655MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1");
            TraceReport("Expected Result");
            TraceInfo(
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 655’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 17
            Action: Send EVC-8 with,MMI_Q_TEXT = 655MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 655’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            TraceHeader("Test Step 18");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Send EVC-8 with,MMI_Q_TEXT = 656MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1");
            TraceReport("Expected Result");
            TraceInfo(
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 656’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 18
            Action: Send EVC-8 with,MMI_Q_TEXT = 656MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 656’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            TraceHeader("Test Step 19");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Send EVC-8 with,MMI_Q_TEXT = 657MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1");
            TraceReport("Expected Result");
            TraceInfo(
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 657’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 19
            Action: Send EVC-8 with,MMI_Q_TEXT = 657MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 657’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            TraceHeader("Test Step 20");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Send EVC-8 with,MMI_Q_TEXT = 658MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1");
            TraceReport("Expected Result");
            TraceInfo(
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 658’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 20
            Action: Send EVC-8 with,MMI_Q_TEXT = 658MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 658’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            TraceHeader("Test Step 21");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Send EVC-8 with,MMI_Q_TEXT = 659MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1");
            TraceReport("Expected Result");
            TraceInfo(
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 659’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 21
            Action: Send EVC-8 with,MMI_Q_TEXT = 659MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 659’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            TraceHeader("Test Step 22");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Send EVC-8 with,MMI_Q_TEXT = 660MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1");
            TraceReport("Expected Result");
            TraceInfo(
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 660’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 22
            Action: Send EVC-8 with,MMI_Q_TEXT = 660MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 660’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            TraceHeader("Test Step 23");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Send EVC-8 with,MMI_Q_TEXT = 661MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1");
            TraceReport("Expected Result");
            TraceInfo(
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 661’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 23
            Action: Send EVC-8 with,MMI_Q_TEXT = 661MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 661’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            TraceHeader("Test Step 24");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Send EVC-8 with,MMI_Q_TEXT = 662MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1");
            TraceReport("Expected Result");
            TraceInfo(
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 662’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 24
            Action: Send EVC-8 with,MMI_Q_TEXT = 662MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 662’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            TraceHeader("Test Step 25");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Send EVC-8 with,MMI_Q_TEXT = 663MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1");
            TraceReport("Expected Result");
            TraceInfo(
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 663’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 25
            Action: Send EVC-8 with,MMI_Q_TEXT = 663MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 663’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            TraceHeader("Test Step 26");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Send EVC-8 with,MMI_Q_TEXT = 664MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1");
            TraceReport("Expected Result");
            TraceInfo(
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 664’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 26
            Action: Send EVC-8 with,MMI_Q_TEXT = 664MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 664’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            TraceHeader("Test Step 27");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Send EVC-8 with,MMI_Q_TEXT = 665MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1");
            TraceReport("Expected Result");
            TraceInfo(
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 665 is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 27
            Action: Send EVC-8 with,MMI_Q_TEXT = 665MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 665 is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            TraceHeader("Test Step 28");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Send EVC-8 with,MMI_Q_TEXT = 666MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1");
            TraceReport("Expected Result");
            TraceInfo(
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 666’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 28
            Action: Send EVC-8 with,MMI_Q_TEXT = 666MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 666’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            TraceHeader("Test Step 29");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Send EVC-8 with,MMI_Q_TEXT = 667MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1");
            TraceReport("Expected Result");
            TraceInfo(
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 667’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 29
            Action: Send EVC-8 with,MMI_Q_TEXT = 667MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 667’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            TraceHeader("Test Step 30");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Send EVC-8 with,MMI_Q_TEXT = 668MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1");
            TraceReport("Expected Result");
            TraceInfo(
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 668’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 30
            Action: Send EVC-8 with,MMI_Q_TEXT = 668MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 668’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            TraceHeader("Test Step 31");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Send EVC-8 with,MMI_Q_TEXT = 669MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1");
            TraceReport("Expected Result");
            TraceInfo(
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 669’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 31
            Action: Send EVC-8 with,MMI_Q_TEXT = 669MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 669’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            TraceHeader("Test Step 32");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Send EVC-8 with,MMI_Q_TEXT = 670MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1");
            TraceReport("Expected Result");
            TraceInfo(
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 670’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 32
            Action: Send EVC-8 with,MMI_Q_TEXT = 670MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 670’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            TraceHeader("Test Step 33");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Send EVC-8 with,MMI_Q_TEXT = 671MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1");
            TraceReport("Expected Result");
            TraceInfo(
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 671’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 33
            Action: Send EVC-8 with,MMI_Q_TEXT = 671MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 671’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            TraceHeader("Test Step 34");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Send EVC-8 with,MMI_Q_TEXT = 672MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1");
            TraceReport("Expected Result");
            TraceInfo(
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 672’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 34
            Action: Send EVC-8 with,MMI_Q_TEXT = 672MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 672’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            TraceHeader("Test Step 35");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Send EVC-8 with,MMI_Q_TEXT = 673MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1");
            TraceReport("Expected Result");
            TraceInfo(
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 673’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 35
            Action: Send EVC-8 with,MMI_Q_TEXT = 673MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 673’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            TraceHeader("Test Step 36");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("End of test");
            
            /*
            Test Step 36
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }

        #region Send_XML_15_3_1_9_DMI_Test_Specification

        private void XML_15_3_1_9()
        {
            // There is a conflict between xml and spec: xml step 2 tests MMI_Q_TEXT = 624 which is not an invalid value. There are 36 steps in the xml
            // and 35 in the spec. Xml step 2 is omitted as per spec.

            // Step 1
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 623;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 623’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 2
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 640;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 640’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 3
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 641;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 641’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 4
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 642;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 642’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 5
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 643;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 643’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 6
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 644;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 644’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 7
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 645;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 645’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 8
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 646;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 646’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 9
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 647;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 647’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 10
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 648;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 648’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 11
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 649;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 649’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 12
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 650;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 650’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 13
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 651;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 651’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 14
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 652;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 652’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 15
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 653;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 653’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 16
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 654;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 654’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 17
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 655;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 655’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 18
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 656;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 656’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 19
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 657;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 657’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 20
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 658;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 658’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 21
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 659;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 659’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 22
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 660;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 660’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 23
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 661;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 661’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 24
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 662;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 662’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 25
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 663;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 663’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 26
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 664;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 664’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 27
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 665;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 665’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 28
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 666;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 666’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 29
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 667;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 667’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 30
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 668;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 668’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 31
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 669;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 669’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 32
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 670;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 670’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 33
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 671;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 671’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 34
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 672;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 672’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 35
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 673;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 673’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");
        }

        #endregion
    }
}
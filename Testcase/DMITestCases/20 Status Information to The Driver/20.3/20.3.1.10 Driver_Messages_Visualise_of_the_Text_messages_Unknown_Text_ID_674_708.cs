using System;
using Testcase.Telegrams.EVCtoDMI;

namespace Testcase.DMITestCases
{
    /// <summary>
    /// 20.3.1.10 Driver Messages: Visualise of the Text messages (Unknown Text ID 674-708)
    /// TC-ID: 15.3.1.10
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
    /// 15_3_1_10.xml
    /// </summary>
    public class TC_15_3_1_10_Driver_Messages : TestcaseBase
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

            XML_15_3_1_10();
            // All the following steps are carried out in XML_15_3_1_10.cs
            TraceHeader("Test Step 1");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Use the test script file 15_3_1_10.xml to send EVC-8 with,MMI_Q_TEXT = 674MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1");
            TraceReport("Expected Result");
            TraceInfo(
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 674’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 1
            Action: Use the test script file 15_3_1_10.xml to send EVC-8 with,MMI_Q_TEXT = 674MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 674’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            TraceHeader("Test Step 2");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Send EVC-8 with,MMI_Q_TEXT = 675MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1");
            TraceReport("Expected Result");
            TraceInfo(
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 675’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 2
            Action: Send EVC-8 with,MMI_Q_TEXT = 675MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 675’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            TraceHeader("Test Step 3");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Send EVC-8 with,MMI_Q_TEXT = 676MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1");
            TraceReport("Expected Result");
            TraceInfo(
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 676’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 3
            Action: Send EVC-8 with,MMI_Q_TEXT = 676MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 676’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            TraceHeader("Test Step 4");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Send EVC-8 with,MMI_Q_TEXT = 677MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1");
            TraceReport("Expected Result");
            TraceInfo(
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 677’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 4
            Action: Send EVC-8 with,MMI_Q_TEXT = 677MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 677’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            TraceHeader("Test Step 5");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Send EVC-8 with,MMI_Q_TEXT = 678MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1");
            TraceReport("Expected Result");
            TraceInfo(
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 678’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 5
            Action: Send EVC-8 with,MMI_Q_TEXT = 678MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 678’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            TraceHeader("Test Step 6");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Send EVC-8 with,MMI_Q_TEXT = 679MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1");
            TraceReport("Expected Result");
            TraceInfo(
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 679’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 6
            Action: Send EVC-8 with,MMI_Q_TEXT = 679MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 679’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            TraceHeader("Test Step 7");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Send EVC-8 with,MMI_Q_TEXT = 680MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1");
            TraceReport("Expected Result");
            TraceInfo(
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 680’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 7
            Action: Send EVC-8 with,MMI_Q_TEXT = 680MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 680’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            TraceHeader("Test Step 8");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Send EVC-8 with,MMI_Q_TEXT = 681MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1");
            TraceReport("Expected Result");
            TraceInfo(
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 681’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 8
            Action: Send EVC-8 with,MMI_Q_TEXT = 681MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 681’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            TraceHeader("Test Step 9");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Send EVC-8 with,MMI_Q_TEXT = 682MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1");
            TraceReport("Expected Result");
            TraceInfo(
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 682’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 9
            Action: Send EVC-8 with,MMI_Q_TEXT = 682MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 682’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            TraceHeader("Test Step 10");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Send EVC-8 with,MMI_Q_TEXT = 683MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1");
            TraceReport("Expected Result");
            TraceInfo(
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 683’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 10
            Action: Send EVC-8 with,MMI_Q_TEXT = 683MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 683’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            TraceHeader("Test Step 11");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Send EVC-8 with,MMI_Q_TEXT = 684MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1");
            TraceReport("Expected Result");
            TraceInfo(
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 684’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 11
            Action: Send EVC-8 with,MMI_Q_TEXT = 684MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 684’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            TraceHeader("Test Step 12");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Send EVC-8 with,MMI_Q_TEXT = 685MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1");
            TraceReport("Expected Result");
            TraceInfo(
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 685’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 12
            Action: Send EVC-8 with,MMI_Q_TEXT = 685MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 685’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            TraceHeader("Test Step 13");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Send EVC-8 with,MMI_Q_TEXT = 686MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1");
            TraceReport("Expected Result");
            TraceInfo(
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 686’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 13
            Action: Send EVC-8 with,MMI_Q_TEXT = 686MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 686’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            TraceHeader("Test Step 14");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Send EVC-8 with,MMI_Q_TEXT = 687MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1");
            TraceReport("Expected Result");
            TraceInfo(
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 687’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 14
            Action: Send EVC-8 with,MMI_Q_TEXT = 687MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 687’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            TraceHeader("Test Step 15");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Send EVC-8 with,MMI_Q_TEXT = 688MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1");
            TraceReport("Expected Result");
            TraceInfo(
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 688’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 15
            Action: Send EVC-8 with,MMI_Q_TEXT = 688MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 688’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            TraceHeader("Test Step 16");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Send EVC-8 with,MMI_Q_TEXT = 689MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1");
            TraceReport("Expected Result");
            TraceInfo(
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 689’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 16
            Action: Send EVC-8 with,MMI_Q_TEXT = 689MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 689’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            TraceHeader("Test Step 17");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Send EVC-8 with,MMI_Q_TEXT = 690MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1");
            TraceReport("Expected Result");
            TraceInfo(
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 690’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 17
            Action: Send EVC-8 with,MMI_Q_TEXT = 690MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 690’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            TraceHeader("Test Step 18");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Send EVC-8 with,MMI_Q_TEXT = 691MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1");
            TraceReport("Expected Result");
            TraceInfo(
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 691’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 18
            Action: Send EVC-8 with,MMI_Q_TEXT = 691MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 691’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            TraceHeader("Test Step 19");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Send EVC-8 with,MMI_Q_TEXT = 692MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1");
            TraceReport("Expected Result");
            TraceInfo(
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 692’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 19
            Action: Send EVC-8 with,MMI_Q_TEXT = 692MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 692’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            TraceHeader("Test Step 20");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Send EVC-8 with,MMI_Q_TEXT = 693MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1");
            TraceReport("Expected Result");
            TraceInfo(
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 693’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 20
            Action: Send EVC-8 with,MMI_Q_TEXT = 693MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 693’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            TraceHeader("Test Step 21");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Send EVC-8 with,MMI_Q_TEXT = 694MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1");
            TraceReport("Expected Result");
            TraceInfo(
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 694’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 21
            Action: Send EVC-8 with,MMI_Q_TEXT = 694MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 694’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            TraceHeader("Test Step 22");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Send EVC-8 with,MMI_Q_TEXT = 695MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1");
            TraceReport("Expected Result");
            TraceInfo(
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 695’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 22
            Action: Send EVC-8 with,MMI_Q_TEXT = 695MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 695’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            TraceHeader("Test Step 23");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Send EVC-8 with,MMI_Q_TEXT = 696MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1");
            TraceReport("Expected Result");
            TraceInfo(
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 696’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 23
            Action: Send EVC-8 with,MMI_Q_TEXT = 696MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 696’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            TraceHeader("Test Step 24");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Send EVC-8 with,MMI_Q_TEXT = 697MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1");
            TraceReport("Expected Result");
            TraceInfo(
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 697’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 24
            Action: Send EVC-8 with,MMI_Q_TEXT = 697MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 697’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            TraceHeader("Test Step 25");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Send EVC-8 with,MMI_Q_TEXT = 698MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1");
            TraceReport("Expected Result");
            TraceInfo(
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 698’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 25
            Action: Send EVC-8 with,MMI_Q_TEXT = 698MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 698’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            TraceHeader("Test Step 26");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Send EVC-8 with,MMI_Q_TEXT = 699MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1");
            TraceReport("Expected Result");
            TraceInfo(
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 699’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 26
            Action: Send EVC-8 with,MMI_Q_TEXT = 699MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 699’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            TraceHeader("Test Step 27");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Send EVC-8 with,MMI_Q_TEXT = 707MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1");
            TraceReport("Expected Result");
            TraceInfo(
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 707’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 27
            Action: Send EVC-8 with,MMI_Q_TEXT = 707MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 707’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            TraceHeader("Test Step 28");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Send EVC-8 with,MMI_Q_TEXT = 708MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1");
            TraceReport("Expected Result");
            TraceInfo(
                "Verifies the display information as follows,The text message ‘’Fixed Text Message 708’ is display in the area E5.No flashing frame display.There is no sound played");
            /*
            Test Step 28
            Action: Send EVC-8 with,MMI_Q_TEXT = 708MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 708’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            TraceHeader("Test Step 29");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("End of test");
            
            /*
            Test Step 29
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }

        #region Send_XML_15_3_1_10_DMI_Test_Specification

        private void XML_15_3_1_10()
        {
            /// *************** Discrepancy between 15_3_1_10.xml and spec in MMI_Q_TEXT_CRITERIA
            ///                                         3    <=>       1

            // Step 1
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 674;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 674’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 2
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 675;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 675’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 3
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 676;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 676’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 4
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 677;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 677’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 5
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 678;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 678’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 6
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 679;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 679’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 7
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 680;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 680’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 8
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 681;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 681’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 9
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 682;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 682’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 10
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 683;
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
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 684;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 684’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 12
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 685;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 685’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 13
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 686;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 686’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 14
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 687;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 687’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 15
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 688;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 688’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 16
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 689;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 689’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 17
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 690;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 690’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 18
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 691;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 691’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 19
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 692;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 692’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 20
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 693;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 693’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 21
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 694;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 694’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 22
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 695;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 695’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 23
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 696;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 696’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 24
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 697;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 697’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 25
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 698;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 698’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 26
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 699;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 699’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 27
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 707;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 707’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");

            // Step 28
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 708;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The text message ‘Fixed Text Message 708’ is displayed in area E5." +
                                Environment.NewLine +
                                "2. No flashing frame is displayed" + Environment.NewLine +
                                "3. No sound is played.");
        }

        #endregion
    }
}
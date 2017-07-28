using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BT_Tools;
using BT_CSB_Tools;
using BT_CSB_Tools.Logging;
using BT_CSB_Tools.Utils.Xml;
using BT_CSB_Tools.SignalPoolGenerator.Signals;
using BT_CSB_Tools.SignalPoolGenerator.Signals.MwtSignal;
using BT_CSB_Tools.SignalPoolGenerator.Signals.MwtSignal.Misc;
using BT_CSB_Tools.SignalPoolGenerator.Signals.PdSignal;
using BT_CSB_Tools.SignalPoolGenerator.Signals.PdSignal.Misc;
using CL345;

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
    public class Driver_Messages_Visualise_of_the_Text_messages_Unknown_Text_ID_674_708 : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // DMI is power on.Cabin A is activated.SoM is perform until Level 1 is selected and confirmed.Main window is closed.

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
            // Testcase entrypoint


            /*
            Test Step 1
            Action: Use the test script file 15_3_1_10.xml to send EVC-8 with,MMI_Q_TEXT = 674MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 674’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            /*
            Test Step 2
            Action: Send EVC-8 with,MMI_Q_TEXT = 675MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 675’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            /*
            Test Step 3
            Action: Send EVC-8 with,MMI_Q_TEXT = 676MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 676’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            /*
            Test Step 4
            Action: Send EVC-8 with,MMI_Q_TEXT = 677MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 677’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            /*
            Test Step 5
            Action: Send EVC-8 with,MMI_Q_TEXT = 678MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 678’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            /*
            Test Step 6
            Action: Send EVC-8 with,MMI_Q_TEXT = 679MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 679’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            /*
            Test Step 7
            Action: Send EVC-8 with,MMI_Q_TEXT = 680MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 680’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            /*
            Test Step 8
            Action: Send EVC-8 with,MMI_Q_TEXT = 681MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 681’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            /*
            Test Step 9
            Action: Send EVC-8 with,MMI_Q_TEXT = 682MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 682’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            /*
            Test Step 10
            Action: Send EVC-8 with,MMI_Q_TEXT = 683MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 683’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            /*
            Test Step 11
            Action: Send EVC-8 with,MMI_Q_TEXT = 684MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 684’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            /*
            Test Step 12
            Action: Send EVC-8 with,MMI_Q_TEXT = 685MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 685’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            /*
            Test Step 13
            Action: Send EVC-8 with,MMI_Q_TEXT = 686MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 686’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            /*
            Test Step 14
            Action: Send EVC-8 with,MMI_Q_TEXT = 687MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 687’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            /*
            Test Step 15
            Action: Send EVC-8 with,MMI_Q_TEXT = 688MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 688’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            /*
            Test Step 16
            Action: Send EVC-8 with,MMI_Q_TEXT = 689MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 689’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            /*
            Test Step 17
            Action: Send EVC-8 with,MMI_Q_TEXT = 690MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 690’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            /*
            Test Step 18
            Action: Send EVC-8 with,MMI_Q_TEXT = 691MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 691’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            /*
            Test Step 19
            Action: Send EVC-8 with,MMI_Q_TEXT = 692MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 692’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            /*
            Test Step 20
            Action: Send EVC-8 with,MMI_Q_TEXT = 693MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 693’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            /*
            Test Step 21
            Action: Send EVC-8 with,MMI_Q_TEXT = 694MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 694’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            /*
            Test Step 22
            Action: Send EVC-8 with,MMI_Q_TEXT = 695MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 695’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            /*
            Test Step 23
            Action: Send EVC-8 with,MMI_Q_TEXT = 696MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 696’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            /*
            Test Step 24
            Action: Send EVC-8 with,MMI_Q_TEXT = 697MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 697’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            /*
            Test Step 25
            Action: Send EVC-8 with,MMI_Q_TEXT = 698MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 698’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            /*
            Test Step 26
            Action: Send EVC-8 with,MMI_Q_TEXT = 699MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 699’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            /*
            Test Step 27
            Action: Send EVC-8 with,MMI_Q_TEXT = 707MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 707’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            /*
            Test Step 28
            Action: Send EVC-8 with,MMI_Q_TEXT = 708MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 708’ is display in the area E5.No flashing frame display.There is no sound played
            Test Step Comment: MMI_gen 148;         
            */


            /*
            Test Step 29
            Action: End of test
            Expected Result: 
            */


            return GlobalTestResult;
        }
    }
}
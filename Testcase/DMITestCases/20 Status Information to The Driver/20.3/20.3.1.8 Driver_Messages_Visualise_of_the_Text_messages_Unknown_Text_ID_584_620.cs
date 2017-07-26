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
    public class Driver_Messages_Visualise_of_the_Text_messages_Unknown_Text_ID_584_620 : TestcaseBase
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
            Action: Use the test script file 15_3_1_8.xml to send EVC-8 with,MMI_Q_TEXT = 584MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 584’ is display in the area E5.No flashing frame display.There is no sound played.
            Test Step Comment: MMI_gen 148;         
            */

            /*
            Test Step 2
            Action: Send EVC-8 with,MMI_Q_TEXT = 585MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 585’ is display in the area E5.No flashing frame display.There is no sound played.
            Test Step Comment: MMI_gen 148;         
            */

            /*
            Test Step 3
            Action: Send EVC-8 with,MMI_Q_TEXT = 586MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 586’ is display in the area E5.No flashing frame display.There is no sound played.
            Test Step Comment: MMI_gen 148;         
            */

            /*
            Test Step 4
            Action: Send EVC-8 with,MMI_Q_TEXT = 587MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 587’ is display in the area E5.No flashing frame display.There is no sound played.
            Test Step Comment: MMI_gen 148;         
            */

            /*
            Test Step 5
            Action: Send EVC-8 with,MMI_Q_TEXT = 588MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 588’ is display in the area E5.No flashing frame display.There is no sound played.
            Test Step Comment: MMI_gen 148;         
            */

            /*
            Test Step 6
            Action: Send EVC-8 with,MMI_Q_TEXT = 589MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 17MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 589’ is display in the area E5.No flashing frame display.There is no sound played.
            Test Step Comment: MMI_gen 148;         
            */

            /*
            Test Step 7
            Action: Send EVC-8 with,MMI_Q_TEXT = 590MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 590’ is display in the area E5.No flashing frame display.There is no sound played.
            Test Step Comment: MMI_gen 148;         
            */

            /*
            Test Step 8
            Action: Send EVC-8 with,MMI_Q_TEXT = 591MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 591’ is display in the area E5.No flashing frame display.There is no sound played.
            Test Step Comment: MMI_gen 148;         
            */

            /*
            Test Step 9
            Action: Send EVC-8 with,MMI_Q_TEXT = 592MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 592’ is display in the area E5.No flashing frame display.There is no sound played.
            Test Step Comment: MMI_gen 148;         
            */

            /*
            Test Step 10
            Action: Send EVC-8 with,MMI_Q_TEXT = 593MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 593’ is display in the area E5.No flashing frame display.There is no sound played.
            Test Step Comment: MMI_gen 148;         
            */

            /*
            Test Step 11
            Action: Send EVC-8 with,MMI_Q_TEXT = 594MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 594’ is display in the area E5.No flashing frame display.There is no sound played.
            Test Step Comment: MMI_gen 148;         
            */

            /*
            Test Step 12
            Action: Send EVC-8 with,MMI_Q_TEXT = 595MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 595’ is display in the area E5.No flashing frame display.There is no sound played.
            Test Step Comment: MMI_gen 148;         
            */

            /*
            Test Step 13
            Action: Send EVC-8 with,MMI_Q_TEXT = 596MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 596’ is display in the area E5.No flashing frame display.There is no sound played.
            Test Step Comment: MMI_gen 148;         
            */

            /*
            Test Step 14
            Action: Send EVC-8 with,MMI_Q_TEXT = 597MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 597’ is display in the area E5.No flashing frame display.There is no sound played.
            Test Step Comment: MMI_gen 148;         
            */

            /*
            Test Step 15
            Action: Send EVC-8 with,MMI_Q_TEXT = 598MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 598’ is display in the area E5.No flashing frame display.There is no sound played.
            Test Step Comment: MMI_gen 148;         
            */

            /*
            Test Step 16
            Action: Send EVC-8 with,MMI_Q_TEXT = 599MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 599’ is display in the area E5.No flashing frame display.There is no sound played.
            Test Step Comment: MMI_gen 148;         
            */

            /*
            Test Step 17
            Action: Send EVC-8 with,MMI_Q_TEXT = 600MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 600’ is display in the area E5.No flashing frame display.There is no sound played.
            Test Step Comment: MMI_gen 148;         
            */

            /*
            Test Step 18
            Action: Send EVC-8 with,MMI_Q_TEXT = 601MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 601’ is display in the area E5.No flashing frame display.There is no sound played.
            Test Step Comment: MMI_gen 148;         
            */

            /*
            Test Step 19
            Action: Send EVC-8 with,MMI_Q_TEXT = 602MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 602’ is display in the area E5.No flashing frame display.There is no sound played.
            Test Step Comment: MMI_gen 148;         
            */

            /*
            Test Step 20
            Action: Send EVC-8 with,MMI_Q_TEXT = 603MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 603’ is display in the area E5.No flashing frame display.There is no sound played.
            Test Step Comment: MMI_gen 148;         
            */

            /*
            Test Step 21
            Action: Send EVC-8 with,MMI_Q_TEXT = 604MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 604’ is display in the area E5.No flashing frame display.There is no sound played.
            Test Step Comment: MMI_gen 148;         
            */

            /*
            Test Step 22
            Action: Send EVC-8 with,MMI_Q_TEXT = 605MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 605’ is display in the area E5.No flashing frame display.There is no sound played.
            Test Step Comment: MMI_gen 148;         
            */

            /*
            Test Step 23
            Action: Send EVC-8 with,MMI_Q_TEXT = 607MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 607’ is display in the area E5.No flashing frame display.There is no sound played.
            Test Step Comment: MMI_gen 148;         
            */

            /*
            Test Step 24
            Action: Send EVC-8 with,MMI_Q_TEXT = 608MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 608’ is display in the area E5.No flashing frame display.There is no sound played.
            Test Step Comment: MMI_gen 148;         
            */

            /*
            Test Step 25
            Action: Send EVC-8 with,MMI_Q_TEXT = 616MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 616’ is display in the area E5.No flashing frame display.There is no sound played.
            Test Step Comment: MMI_gen 148;         
            */

            /*
            Test Step 26
            Action: Send EVC-8 with,MMI_Q_TEXT = 617MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 617’ is display in the area E5.No flashing frame display.There is no sound played.
            Test Step Comment: MMI_gen 148;         
            */

            /*
            Test Step 27
            Action: Send EVC-8 with,MMI_Q_TEXT = 618MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 618’ is display in the area E5.No flashing frame display.There is no sound played.
            Test Step Comment: MMI_gen 148;         
            */

            /*
            Test Step 28
            Action: Send EVC-8 with,MMI_Q_TEXT = 619MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text message ‘’Fixed Text Message 619’ is display in the area E5.No flashing frame display.There is no sound played.
            Test Step Comment: MMI_gen 148;         
            */

            /*
            Test Step 29
            Action: Send EVC-8 with,MMI_Q_TEXT = 620MMI_Q_TEXT_CRITERIA = 1MMI_Q_TEXT_CLASS = 1MMI_I_TEXT = 1
            Expected Result: Verifies the display information as follows,The text msage ‘’Fixed Text Message 620’ is display in the area E5.No flashing frame display.There is no sound played.
            Test Step Comment: MMI_gen 148;         
            */

            /*
            Test Step 30
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
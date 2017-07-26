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
    /// 10.12.3 Navigation button: Up and Down buttons
    /// TC-ID: 5.12.3
    /// 
    /// This test case verifies the navigation buttons of the ‘Up’ and ‘Down’ buttons. The symbol, type of button, states, sound, and button activation shall comply with [MMI-ETCS-gen]
    /// 
    /// Tested Requirements:
    /// MMI_gen 4394 (partly: up,down); MMI_gen 4396 (partly: up, down); MMI_gen 4392 (partly: symbol NA13, scroll up, symbol NA14, scroll down); MMI_gen 11470 (partly: Bit # 41 and 42);
    /// 
    /// Scenario:
    /// 1.Use the test script file to send packet information EVC-
    /// 8.Then, verify the display information of ‘Up’ and ‘Down’ buttons.
    /// 2.Verify the down-type buttons for ‘Up’ and ‘Down’ button.
    /// 
    /// Used files:
    /// 5_12_3_a.xml
    /// </summary>
    public class Navigation_button_Up_and_Down_buttons : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // System is powered onCabin is activatedSoM is performed until level 1 is selected and confirmedMain window is closed.

            // Call the TestCaseBase PreExecution
            base.PreExecution();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in SB mode, level 1.

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint


            /*
            Test Step 1
            Action: Use the test script file 5_12_3_a.xml  to send multiple packets EVC-8 with the following value,Common variableMMI_Q_TEXT_CLASS = 1MMI_Q_TEXT_CRITERIA =3The order of MMI_Q_TEXT value in each packetMMI_Q_TEXT = 267MMI_Q_TEXT = 560MMI_Q_TEXT = 268MMI_Q_TEXT = 274MMI_Q_TEXT = 275MMI_Q_TEXT = 290MMI_Q_TEXT = 292MMI_Q_TEXT = 296MMI_Q_TEXT = 310MMI_Q_TEXT = 299
            Expected Result: DMI displays the following messages in sub-area E5-E9 are correct refer to following message respectively,Balise read errorTrackside malfunctionCommunication errorEntering FSEntering OSSH refusedSH request failedTrackside not compatibleTrain data changedTrain is rejectedNote: The new text message is displayed in sub-area E5, old text messages are moved down automatically.Verify the following information,(1)   The disabled ‘Up’ button is displayed as NA15 symbol in sub-area E10.(2)   The enabled ‘Down’ button is displayed as NA14 symbol in sub-area E11.
            Test Step Comment: (1) MMI_gen 4394 (partly: up, disabled); MMI_gen 4396 (partly: up, NA15);(2) MMI_gen 4394 (partly: down, enabled); MMI_gen 4396 (partly: down, NA14);
            */

            /*
            Test Step 2
            Action: Press and hold ‘Down’ button at least 2 second.Note: Stopwatch is required.
            Expected Result: Verify the following information,While press and hold button less than 1.5 sec(1)   Sound ‘Click’ is played once.(2)   The state of button is changed to ‘Pressed’ and immediately back to ‘Enabled’ state.(3)   The visibility of sub-area E5-E9 is moved down.While press and hold button over 1.5 sec(4)    The state ‘pressed’ and ‘released’ are switched repeatly while button is pressed and the visibility of sub-area E5-E9 is moving down repeatly refer to pressed state.(5)   The sound ‘Click’ is played repeatly while button is pressed.
            Test Step Comment: (1) MMI_gen 4393 (partly: down, MMI_gen 4384 (partly: sound ‘Click’));(2) MMI_gen 4393 (partly: MMI_gen 4384 (partly: Change to state ‘Pressed’ and immediately back to state ‘Enabled’));(3) MMI_gen 4393 (partly: down, MMI_gen 4384 (partly: ETCS-MMI’s function associated to the button)); MMI_gen 4392 (partly: symbol NA14, scroll down);(4) MMI_gen 4393 (partly: down, MMI_gen 4386 (partly: visual of repeat function));(5) MMI_gen 4393 (partly: down, MMI_gen 4386 (partly: audible of repeat function));
            */

            /*
            Test Step 3
            Action: Hold the pressed area until the text message ‘Train is rejected’ is displayed on sub-area E5.Then, release the pressed area.
            Expected Result: Verify the following information,(1)   The state of pressed button is changed to ‘Disabled’ state.(2)   The disabled ‘Down’ button is displayed as NA16 symbol in sub-area E11.(3)   The enabled ‘Up’ button is displayed as NA13 symbol in sub-area E10.(4)   Use the log file to confirm that DMI sends out packet [MMI_DRIVER_ACTION (EVC-152)] with the value of variable MMI_M_DRIVER_ACTION refer to sequence below,a)   MMI_M_DRIVER_ACTION = 42 (Scroll down button activated)
            Test Step Comment: (1) MMI_gen 4393 (partly: down, MMI_gen 4384 (partly: ETCS-MMI’s function associated to the button));(2) MMI_gen 4394 (partly: down, disabled); MMI_gen 4396 (partly: down, NA16);(3) MMI_gen 4394 (partly: up, enabled); MMI_gen 4396 (partly: up, NA13);(4) MMI_gen 11470 (partly: Bit # 42);
            */

            /*
            Test Step 4
            Action: Perform action step 2-3 for the ‘Up’ button.
            Expected Result: See the expected results of Step 2 – Step 3 and the following additional information,(1)    The visibility of sub-area E5-E9 is moved up.(2)   Use the log file to confirm that DMI sends out packet [MMI_DRIVER_ACTION (EVC-152)] with the value of variable MMI_M_DRIVER_ACTION refer to sequence below,a)   MMI_M_DRIVER_ACTION = 41 (Scroll up button activated)
            Test Step Comment: (1) MMI_gen 4393 (partly: up button); MMI_gen 4392 (partly: symbol NA13, scroll up);(2) MMI_gen 11470 (partly: Bit # 41);
            */

            /*
            Test Step 5
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
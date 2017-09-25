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
using Testcase.Telegrams.EVCtoDMI;
using Testcase.Telegrams.DMItoEVC;
using static Testcase.Telegrams.EVCtoDMI.Variables;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 27.9.1 SR Speed/Distance window: General appearance
    /// TC-ID: 22.9.1
    /// 
    /// This test case verifies the display of the ‘SR Speed/Distance’ window on DMI that shall comply with [ERA-ERTMS] standard and [MMI-ETCS-gen].
    /// 
    /// Tested Requirements:
    /// MMI_gen 1704; MMI_gen 9887; MMI_gen 1705 (partly: exclude MMI_gen 9508); MMI_gen 1716; MMI_gen 8296; MMI_gen 9891; MMI_gen 9893; MMI_gen 8298; MMI_gen 1709; MMI_gen 9509 (partly: [Enter], [Yes], EVC-106); MMI_gen 8299; MMI_gen 1713; MMI_gen 1710; MMI_gen 9510 (partly: [Enter], [Yes], EVC-106); MMI_gen 8300; MMI_gen 8295 (partly: MMI_gen 4888, MMI_gen 4799 (partly: Close button, Previous button, Next button, Window Title), MMI_gen 4891 (partly: Yes button, Area for [Window Title] Entry complete?), MMI_gen 4910 (partly: Disabled, MMI_gen 4211 (partly: colour)), MMI_gen 4909, MMI_gen 4908 (partly: extended), MMI_gen 4637 (partly: Main-areas D and F), MMI_gen 4640, MMI_gen 4641, MMI_gen 9412, MMI_gen 4645, MMI_gen 4646 (partly: right aligned), MMI_gen 4647 (partly: left aligned), MMI_gen 4648, MMI_gen 4720, MMI_gen 4651, MMI_gen 4683, MMI_gen 5211, MMI_gen 4649, MMI_gen 4912, MMI_gen 4678, MMI_gen 5190, MMI_gen 4696, MMI_gen 4697, MMI_gen 4701, MMI_gen 4702 (partly: right aligned), MMI_gen 4704 (partly: left aligned), MMI_gen 4700 (partly: otherwise, grey), MMI_gen 4691 (partly: flash), MMI_gen 4689, MMI_gen 4698, MMI_gen 4890, MMI_gen 4690, MMI_gen 4913 (partly: MMI_gen 4384, MMI_gen 4386), MMI_gen 4679, MMI_gen 4642, MMI_gen 4692, MMI_gen 4682, MMI_gen 4634, MMI_gen 4652, MMI_gen 4684, MMI_gen 4651, MMI_gen 4681, MMI_gen 4694 (partly: MMI_gen 4246), MMI_gen 4911 (partly: MMI_gen 4381, MMI_gen 4382), MMI_gen 4686, MMI_gen 4680, MMI_gen 4685, MMI_gen 5003); MMI_gen 4392 (partly: [Close] NA11, [Delete] NA21, [Enter], touch screen, returning to the parent window); MMI_gen 4355 (partly: Buttons, Close button); MMI_gen 4377 (partly: shown); MMI_gen 4355 (partly: input fields); MMI_gen 4374; MMI_gen 4375; MMI_gen 9512; MMI_gen 968; MMI_gen 5387; MMI_gen 4241; MMI_gen 9390 (partly: SR Speed/Distance window); MMI_gen 4393 (partly: [Delete]);
    /// 
    /// Scenario:
    /// SR speed/distance window in cabin inactive state is verified.Perform SoM in SR mode, Level 
    /// 1.Then, open SR speed/distance window, the window appearance is verified.The data entry functionality of the SR speed/distance window is verified with the following type of button in keypad,The SR speed input field with Numeric keyboard.The SR Distance input field with Numeric keyboard.The Up-type button on ‘Yes’ button.The data revalidation functionality of the SR speed/distance window is verified.The Up-Type button on each label part of an input field is verified.The Up-Type button on each data part of an input field is verified.The packet information handling including with an invalid value is verified.The functionality of ‘Close’ button is verified.
    /// 
    /// Used files:
    /// 22_9_1_a.xml, 22_9_1_b.xml
    /// </summary>
    public class TC_22_9_1_SR_Speed_Distance_window : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:

            // Call the TestCaseBase PreExecution
            base.PreExecution();

            // Test system is powered onCabin is inactive
            DmiActions.Start_ATP();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in SR mode, level 1.

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint

            /*
            Test Step 1
            Action: Use the test script file 22_9_1_a.xml to send EVC-11,MMI_N_DATA_ELEMENTS = 0MMI_V_STFF = 100MMI_L_STFF = 100000
            Expected Result: DMI does not display SR speed/distance window
            Test Step Comment: MMI_gen 1704 (partly: NEGATIVE, inactive);
            */
            XML.XML_22_9_1_a.Send(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI does not display the SR/speed distance window");

            /*
            Test Step 2
            Action: Perform the following procedure,Activate cabin A.Perform SoM in SR mode, Level 1.Press ‘Spec’ buttonPress ‘SR speed/distance’ button
            Expected Result: Verify the following information,Data entry windowThe window title is ‘SR speed / distance’.The text label of the window title is right aligned.The following objects are displayed in Train data window,  Enabled Close button (NA11)Window TitleInput fieldThe following objects are additionally displayed in Train data window,Yes buttonThe text label ‘SR speed / distance entry complete?’Yes button is displayed in Disabled state as follows,Text label is black Background colour is dark-greyThe border colour is medium-grey the same as the input field’s colour.The sensitive area of Yes button is extended from text label ‘SR speed / distance entry complete?’Input fieldsThe input fields are located on Main area D and F.Each input field is devided into a Label Area and a Data Area.The Label Area is give the topic of the input field.The Label Area text is displayed corresponding to the input field i.e. SR Speed and SR distance. The Label Area is placed to the left of The Data Area.The text in the Label Area is aligned to the right.The value of data in the Data Area is aligned to the left.The text colour of the Label Area is grey and the background colour of the Label Area is dark-grey.There are only 2 input fields displayed in the window.The first input field is in state ‘Selected’ as follows,The background colour of the Data Area is medium-grey.The colour of data value is black.All other input fields are in state ‘Not selected’ as follows,The background colour of the Data Area is dark-grey.The colour of data value is grey.KeyboardThe keyboard associated to selected input field ‘SR Speed’ is Numeric keyboard.The keyboard contains enabled button for the number <1> to <9>, <Delete>(NA21) , <0> and disabled <Decimal_Separator>. NA21, Delete buttonLayersThe level of layers of all areas in window are in Layer 0.Echo TextsAn echo text is composed of Label Part and Data Part.The Label Part of an echo texts is same as The Label area of an input fields.The echo texts are displayed in main area A, B, C and E with same order as their related input fields.The Label part of echo text is right aligned.The Data part of echo text is left aligned.The colour of texts in echo texts are grey.Entering CharactersThe cursor is flashed by changing from visible to not visible.The cursor is displayed as a horizontal line below the position of the next character to be entered.Packet transmissionUse the log file to confirm that DMI received packet EVC-11 with the variable MMI_N_DATA_ELEMENTS = 0.DMI displays SR Speed/Distance window.The data part of each input field is filled according to received packet EVC-11 with following variables,MMI_V_STFF = SR speedMMI_L_STFF = SR Distance
            Test Step Comment: (1) MMI_gen 8296;(2) MMI_gen 8295 (partly: MMI_gen 4888);(3) MMI_gen 8295 (partly: MMI_gen 4799 (partly: Close button, Previous button, Next button, Window Title)); MMI_gen 4392 (partly: [Close] NA11); MMI_gen 4355 (partly: Buttons, Close button);(4) MMI_gen 8295 (partly: MMI_gen 4891 (partly: Yes button, Area for [Window Title] Entry complete?));(5) MMI_gen 8295 (partly: MMI_gen 4910 (partly: Disabled, MMI_gen 4211 (partly: colour)), MMI_gen 4909 (partly: Disabled)); MMI_gen 4377 (partly: shown);(6) MMI_gen 8295 (partly: MMI_gen 4908 (partly: extended));(7) MMI_gen 8295 (partly: MMI_gen 4637 (partly: Main-areas D and F)); MMI_gen 4355 (partly: input fields);(8) MMI_gen 8295 (partly: MMI_gen 4640);(9) MMI_gen 8295 (partly: MMI_gen 4641);(10) MMI_gen 8295 (partly: MMI_gen 9412);(11) MMI_gen 8295 (partly: MMI_gen 4645);(12) MMI_gen 8295 (partly: MMI_gen 4646 (partly: right aligned));(13) MMI_gen 8295 (partly: MMI_gen 4647 (partly: left aligned));(14) MMI_gen 8295 (partly: MMI_gen 4648);(15) MMI_gen 8295 (partly: MMI_gen 4720);(16) MMI_gen 8295 (partly: MMI_gen 4651 (partly: SR Speed), MMI_gen 4683 (partly: selected), MMI_gen 5211 (partly: selected));(17) MMI_gen 8295 (partly: MMI_gen 4649 (partly: selected ‘SR Speed’), MMI_gen 4651 (partly: SR Distance), MMI_gen 4683 (partly: not selected), MMI_gen 5211 (partly: not selected));(18) MMI_gen 8300 (partly: SR Speed); MMI_gen 8295 (partly: MMI_gen 4912 ( partly: SR Speed), MMI_gen 4678 (partly: SR Speed));(19) MMI_gen 8295  (partly: MMI_gen 5003 (partly: SR Speed)); MMI_gen 4392 (partly: [Delete] NA21);(20) MMI_gen 8295 (partly: MMI_gen 5190);(21) MMI_gen 8295 (partly: MMI_gen 4696);(22) MMI_gen 8295 (partly: MMI_gen 4697);(23) MMI_gen 8295 (partly: MMI_gen 4701);(24) MMI_gen 8295 (partly: MMI_gen 4702 (partly: right aligned));(25) MMI_gen 8295 (partly: MMI_gen 4704 (partly: left aligned));(26) MMI_gen 8295 (partly: MMI_gen 4700 (partly: otherwise, grey)); MMI_gen 4241;(27) MMI_gen 8295 (partly: MMI_gen 4691 (partly: flash, SR Speed));(28) MMI_gen 8295 (partly: MMI_gen 4689, MMI_gen 4690);(29) MMI_gen 1704 (partly: EVC-11); MMI_gen 9887 (partly: MMI_N_DATA_ELEMENTS);(30) MMI_gen 1704 (partly: Display SR Speed/Distance);(31) MMI_gen 9887 (partly: data part of the input fields);
            */
            DmiActions.Complete_SoM_L1_SR(this);
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = 1;
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.SRSpeedDistance;
            EVC30_MMIRequestEnable.Send();

            DmiActions.ShowInstruction(this, "Press the ‘Spec’ button, then press the ‘SR speed/distance’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the SR/speed distance window with the title ‘SR speed / distance’." + Environment.NewLine +
                                "2. The text of the window title is right-aligned." + Environment.NewLine +
                                "3. The window displays an enabled ‘Close’ button, symbol NA11, 2 data input fields, a ‘SR speed / distance entry complete?’ label," + Environment.NewLine +
                                    "a disabled ‘Yes’ button with black text on a Dark-grey background colour with a Medium grey border colour is medium - grey the same as the input field’s colour." + Environment.NewLine +
                                "4. The sensitive area of the ‘Yes’ button extends from the label ‘SR speed / distance entry complete?’" + Environment.NewLine +
                                "5. The data input fields are displayed in areas D and F." + Environment.NewLine +
                                "6. Data input fields have a Label Area and a Data Area." + Environment.NewLine +
                                "7. The Label Area text corresponds to the input field i.e. SR Speed and SR distance." + Environment.NewLine +
                                "8. The Label Area is displayed to the left of The Data Area with right-aligned text in grey on a Dark-grey background." + Environment.NewLine +
                                "9. The first data input field is ‘Selected’: the value in the Data Area is left-aligned in black on a Medium-grey background." + Environment.NewLine +
                                "10. The other input field is ‘Not selected’ with grey text on a Dark-grey background." + Environment.NewLine +
                                "11. A numeric keypad is displayed for the selected input field ‘SR Speed’." + Environment.NewLine +
                                "12. The keypad contains enabled buttons for the numbers <1> to <9>, <Del> (symbol NA21), <0> and (disabled) <Decimal_Separator>." + Environment.NewLine +
                                "13. All areas in the window are in Layer 0." + Environment.NewLine +
                                "14. Echo texts are displayed in areas A, B, C and E in the same order as their related data input fields." + Environment.NewLine +
                                "15. Echo texts comprise a Label Part and a Data Part." + Environment.NewLine +
                                "16. The Label Part of an echo text has the same text as The Label Area of the related data input field." + Environment.NewLine +
                                "17. The text of the Label part of an echo text is right-aligned." + Environment.NewLine +
                                "18. The Data part of an echo text has left-aligned text." + Environment.NewLine +
                                "19. The text in an echo texts is grey." + Environment.NewLine +
                                "20. A flashing (visible/invisible) underscore is displayed as a cursor below the position of the next character to be entered." + Environment.NewLine +
                                "21. The SR speed data input field displays ‘100’." + Environment.NewLine +
                                "22. The SR Distance data input field displays ‘100000’.");

            /*
            Test Step 3
            Action: Press and hold ‘0’ button
            Expected Result: Verify the following information,The state of button is changed to ‘Pressed’ and immediately back to ‘Enabled’ state.The sound ‘Click’ is played once.The Input Field displays the value associated to the data key according to the pressings in state ‘Pressed’.The cursor is displayed as horizontal line below the value of the numeric-keyboard data key in the input field.The input field is used to enter the SR speed
            Test Step Comment: (1) MMI_gen 8295 (partly: MMI_gen 4913 (partly: SR Speed), MMI_gen 4384 (partly: Change to state ‘Pressed’ and immediately back to state ‘Enabled’));   (2) MMI_gen 8295 (partly: MMI_gen 4913 (partly: SR Speed), MMI_gen 4384 (partly: sound ‘Click’)); MMI_gen 9512; MMI_gen 968;(3) MMI_gen 8295 (partly: MMI_gen 4679 (partly: SR Speed), MMI_gen 4913 (partly: SR Speed), MMI_gen 4384 (partly: ETCS-MMI’s function associated to the button));(4) MMI_gen 8295 (partly: MMI_gen 4689, MMI_gen 4690);(5) MMI_gen 8298 (partly: entry);
            */
            DmiActions.ShowInstruction(this, @"Press and hold the <0> key");

            // Spec. says cursor under character entered: gen 4690 says under the next character entry position
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The key is displayed pressed and immediately re-displayed enabled" + Environment.NewLine + 
                                "2. The ‘Click’ sound is played once." + Environment.NewLine +
                                "3. The SR speed data input field displays the value of the key pressed." + Environment.NewLine +
                                "4. An underscore character is displayed after the ‘0’ at the end of the SR Speed data input field.");

            /*
            Test Step 4
            Action: Release the pressed button
            Expected Result: Verify the following information,The state of released button is changed to enabled
            Test Step Comment: (1) MMI_gen 8295 (partly: MMI_gen 4913 (partly: SR Speed), MMI_gen 4384 (partly: ETCS-MMI’s function associated to the button));
            */
            DmiActions.ShowInstruction(this, @"Release the <0> key");


            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The <0> key is displayed enabled");

            /*
            Test Step 5
            Action: Perform action step 3-4 for the ‘1’ to ‘9’ buttons.Note: Press the ‘Del’ button to delete an information when entered data is out of input field range is acceptable
            Expected Result: See the expected results of Step 3 – Step 4 and the following additional information,The pressed key is added in an input field immediately. The cursor is jumped to next position after entered the character immediately
            Test Step Comment: (1) MMI_gen 8295 (partly: MMI_gen 4642 (partly: SR Speed));  (2) MMI_gen 8295 (partly: MMI_gen 4692 (partly: SR Speed));  
            */
            // Repeat for the ‘1’ key
            DmiActions.ShowInstruction(this, @"Press and hold the <1> key");

            // Spec. says cursor under character entered: gen 4690 says under the next character entry position
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The key is displayed pressed and immediately re-displayed enabled" + Environment.NewLine +
                                "2. The ‘Click’ sound is played once." + Environment.NewLine +
                                "3. The SR speed data input field displays ‘1’ at the end of the data input field." + Environment.NewLine +
                                "4. An underscore character is displayed after the ‘1’ at the end of the SR Speed data input field.");

            DmiActions.ShowInstruction(this, @"Release the <1> key");


            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The <1> key is displayed enabled");
            
            // Repeat for the <2> key
            DmiActions.ShowInstruction(this, @"Press and hold the <2> key");

            // Spec. says cursor under character entered: gen 4690 says under the next character entry position
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The key is displayed pressed and immediately re-displayed enabled" + Environment.NewLine +
                                "2. The ‘Click’ sound is played once." + Environment.NewLine +
                                "3. The SR speed data input field displays ‘2’ at the end of the data input field." + Environment.NewLine +
                                "4. An underscore character is displayed after the ‘2’ at the end of the SR Speed data input field.");

            DmiActions.ShowInstruction(this, @"Release the <2> key");


            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The <2> key is displayed enabled");

            // Repeat for the <3> key
            DmiActions.ShowInstruction(this, @"Press and hold the <3> key");

            // Spec. says cursor under character entered: gen 4690 says under the next character entry position
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The key is displayed pressed and immediately re-displayed enabled" + Environment.NewLine +
                                "2. The ‘Click’ sound is played once." + Environment.NewLine +
                                "3. The SR speed data input field displays ‘3’ at the end of the data input field." + Environment.NewLine +
                                "4. An underscore character is displayed after the ‘3’ at the end of the SR Speed data input field.");

            DmiActions.ShowInstruction(this, @"Release the <3> key");


            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The <3> key is displayed enabled");

            // Repeat for the <4> key
            DmiActions.ShowInstruction(this, @"Press and hold the <4> key");

            // Spec. says cursor under character entered: gen 4690 says under the next character entry position
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The key is displayed pressed and immediately re-displayed enabled" + Environment.NewLine +
                                "2. The ‘Click’ sound is played once." + Environment.NewLine +
                                "3. The SR speed data input field displays ‘4’ at the end of the data input field." + Environment.NewLine +
                                "4. An underscore character is displayed after the ‘4’ at the end of the SR Speed data input field.");

            DmiActions.ShowInstruction(this, @"Release the <4> key");


            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The <4> key is displayed enabled");

            // Repeat for the <5> key
            DmiActions.ShowInstruction(this, @"Press and hold the <5> key");

            // Spec. says cursor under character entered: gen 4690 says under the next character entry position
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The key is displayed pressed and immediately re-displayed enabled" + Environment.NewLine +
                                "2. The ‘Click’ sound is played once." + Environment.NewLine +
                                "3. The SR speed data input field displays ‘5’ at the end of the data input field." + Environment.NewLine +
                                "4. An underscore character is displayed after the ‘5’ at the end of the SR Speed data input field.");

            DmiActions.ShowInstruction(this, @"Release the <5> key");


            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The <5> key is displayed enabled");

            // Repeat for the <6> key
            DmiActions.ShowInstruction(this, @"Press and hold the <6> key");

            // Spec. says cursor under character entered: gen 4690 says under the next character entry position
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The key is displayed pressed and immediately re-displayed enabled" + Environment.NewLine +
                                "2. The ‘Click’ sound is played once." + Environment.NewLine +
                                "3. The SR speed data input field displays ‘6’ at the end of the data input field." + Environment.NewLine +
                                "4. An underscore character is displayed after the ‘6’ at the end of the SR Speed data input field.");

            DmiActions.ShowInstruction(this, @"Release the <6> key");


            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The <6> key is displayed enabled");

            // Repeat for the <7> key
            DmiActions.ShowInstruction(this, @"Press and hold the <7> key");

            // Spec. says cursor under character entered: gen 4690 says under the next character entry position
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The key is displayed pressed and immediately re-displayed enabled" + Environment.NewLine +
                                "2. The ‘Click’ sound is played once." + Environment.NewLine +
                                "3. The SR speed data input field displays ‘7’ at the end of the data input field." + Environment.NewLine +
                                "4. An underscore character is displayed after the ‘7’ at the end of the SR Speed data input field.");

            DmiActions.ShowInstruction(this, @"Release the <7> key");


            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The <7> key is displayed enabled");

            // Repeat for the <8> key
            DmiActions.ShowInstruction(this, @"Press and hold the <8> key");

            // Spec. says cursor under character entered: gen 4690 says under the next character entry position
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The key is displayed pressed and immediately re-displayed enabled" + Environment.NewLine +
                                "2. The ‘Click’ sound is played once." + Environment.NewLine +
                                "3. The SR speed data input field displays ‘8’ at the end of the data input field." + Environment.NewLine +
                                "4. An underscore character is displayed after the ‘8’ at the end of the SR Speed data input field.");

            DmiActions.ShowInstruction(this, @"Release the <8> key");


            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The <8> key is displayed enabled");

            // Repeat for the <9> key
            DmiActions.ShowInstruction(this, @"Press and hold the <9> key");

            // Spec. says cursor under character entered: gen 4690 says under the next character entry position
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The key is displayed pressed and immediately re-displayed enabled" + Environment.NewLine +
                                "2. The ‘Click’ sound is played once." + Environment.NewLine +
                                "3. The SR speed data input field displays ‘9’ at the end of the data input field." + Environment.NewLine +
                                "4. An underscore character is displayed after the ‘9’ at the end of the SR Speed data input field.");

            DmiActions.ShowInstruction(this, @"Release the <9> key");


            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The <9> key is displayed enabled");

            /*
            Test Step 6
            Action: Press and hold ‘Del’ button.Note: Stopwatch is required
            Expected Result: Verify the following information,While press and hold button less than 1.5 secSound ‘Click’ is played once.The state of button is changed to ‘Pressed’ and immediately back to ‘Enabled’ state.The last character is removed from an input field after pressing the button.While press and hold button over 1.5 secThe state ‘pressed’ and ‘released’ are switched repeatly while button is pressed and the characters are removed from an input field repeatly refer to pressed state.The sound ‘Click’ is played repeatly while button is pressed
            Test Step Comment: (1) MMI_gen 8295 (partly: MMI_gen 4913 (partly: SR Speed), MMI_gen 4384 (partly: sound ‘Click’));(2) MMI_gen 8295 (partly: MMI_gen 4913 (partly: SR Speed), MMI_gen 4384 (partly: Change to state ‘Pressed’ and immediately back to state ‘Enabled’));   (3) MMI_gen 8295 (partly: MMI_gen 4913 (partly: SR Speed), MMI_gen 4384 (partly: ETCS-MMI’s function associated to the button)); MMI_gen 4393 (partly: [Delete]);(4) MMI_gen 8295 (partly: MMI_gen 4913 (partly: SR Speed), MMI_gen 4386 (partly: visual of repeat function));(5) MMI_gen 8295 (partly: MMI_gen 4913 (partly: SR Speed), MMI_gen 4386 (partly: audible of repeat function));
            */
            DmiActions.ShowInstruction(this, @"Press and hold the ‘Del’ key for more than 1.5s. Note: Stopwatch is required");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Click’ sound is played once." + Environment.NewLine +
                                "2. The key is displayed pressed and immediately re-displayed enabled." + Environment.NewLine +
                                "3. The last number entered in the SR speed data input field is deleted." + Environment.NewLine +
                                "4. When the key has been held pressed for 1.5s the key is displayed pressed and immediately re-displayed enabled." + Environment.NewLine +
                                "5. Characters are repeatedly deleted from the end of the SR speed data input field." +
                                "6. The ‘Click’ sound is played repeatedly.");

            /*
            Test Step 7
            Action: Release ‘Del’ button
            Expected Result: Verify the following information, The character is stop removing
            Test Step Comment: (1) MMI_gen 8295 (partly: MMI_gen 4913 (partly: SR Speed)), MMI_gen 4384 (partly: ETCS-MMI’s function associated to the button));
            */
            DmiActions.ShowInstruction(this, @"Release the ‘Del’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. No more characters are deleted from the end of the SR data input field.");

            /*
            Test Step 8
            Action: Enter the data value with 3 characters
            Expected Result: The 3 characters are added on an input field
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Enter the value ‘123’ in the SR speed data input field.");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The SR speed data input field displays ‘123’");

            /*
            Test Step 9
            Action: Continue to enter the 4th character
            Expected Result: Verify the following information,The value pressed key is not added into an input field
            Test Step Comment: (1) MMI_gen 1709 (partly: 3 digit);
            */
            DmiActions.ShowInstruction(this, "Press the <4> key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The SR speed data input field still displays ‘123’");

            /*
            Test Step 10
            Action: Delete the old value and enter the value ‘40’ for SR speed.Then, confirm an entered data by pressing an input field
            Expected Result: Verify the following information,Input fieldsThe associated ‘Enter’ button is data field itself.An input field is used to allow the driver to enter data.The state of ‘SR Speed’ input field is changed to ‘accepted’ as follows,The background colour of the Data Area is dark-grey.The colour of data value is white.The next input field ‘SR Distance’ is in state ‘selected’ as follows,The background colour of the Data Area is medium-grey.The colour of data value is black.Echo TextsThe echo text of ‘SR Speed’ is changed to white colour.The value of echo text is changed refer to entered data.Entering CharactersThe cursor is displayed as a horizontal line below the position of the next character to be entered.The cursor is flashed by changing from visible to not visible.KeyboardThe keyboard associated to selected input field ‘SR Distance’ is Numeric keyboard.The keyboard contains enabled button for the number <1> to <9>, <Delete>(NA21) , <0> and disabled <Decimal_Separator>. NA21, Delete button.Packet transmissionUse the log file to confirm that DMI sent out packet [MMI_NEW_SR_RULES (EVC-106)] with following variablesMMI_V_STFF = 40 MMI_N_DATA_ELEMENTS = 1MMI_NID_DATA = 15 (SR Speed)MMI_M_BUTTONS = 254Use the log file to confirm that the Data part of echo texts are displayed correspond with each index of variables in received packet EVC-11 as follows,For MMI_NID_DATA= 15 (SR speed)MMI_N_TEXT = number of characters of SR speed value.MMI_X_TEXT[x] = each characters which displayed as value of SR speed.For MMI_NID_DATA = 16 (SR distance)MMI_N_TEXT = number of characters of SR distance value.MMI_X_TEXT[x] = each characters which displayed as value of SR distance.Note: x is index of characters
            Test Step Comment: (1) MMI_gen 8295 (partly: MMI_gen 4682 (partly: SR Speed));(2) MMI_gen 8295 (partly: MMI_gen 4634 (partly: SR Speed));(3) MMI_gen 8295 (partly: MMI_gen 4652 (partly: SR Speed), MMI_gen 4684 (partly: accepted, SR Speed));(4) MMI_gen 8295 (partly: MMI_gen 4684 (partly: SR Distance, selected automatically), MMI_gen 4651 (partly: SR Distance));(5) MMI_gen 8295 (partly: MMI_gen 4700 (partly: SR Speed));(6) MMI_gen 8295 (partly: MMI_gen 4681 (partly: SR Speed), MMI_gen 4890, MMI_gen 4698);(7) MMI_gen 8295 (partly: MMI_gen 4689, MMI_gen 4690);(8) MMI_gen 8295 (partly: MMI_gen 4691 (partly: flash, SR Distance));(9) MMI_gen 8300 (partly: SR Distance); MMI_gen 8295 (partly: MMI_gen 4912 (partly: SR Distance), MMI_gen 4678 (partly: SR Distance));(10) MMI_gen 8295 (partly: MMI_gen 5003 (partly: SR Distance)); MMI_gen 4392 (partly: [Delete] NA21);(11) MMI_gen 9509 (partly: [Enter] EVC-106); (12) MMI_gen 9891;
            */
            DmiActions.ShowInstruction(this, "Delete the value in the SR speed data input field, enter the value ‘40’ and confirm by pressing in the data input field");

            //EVC106_MMINewSRRules.CheckMmiVStff = 40;
            //EVC106_MMINewSRRules.CheckMmiNidData = 15;
            //EVC106_MMINewSRRules.CheckMMiMButtons = Variables.MMI_M_BUTTONS.No_Button;

            List<DataElement> dataElements = new List<DataElement> { new DataElement { Identifier = 15, EchoText = "40", QDataCheck = 1 },
                                                                     new DataElement { Identifier = 16, EchoText = "", QDataCheck = 1} };
            EVC11_MMICurrentSRRules.DataElements = dataElements;
            EVC11_MMICurrentSRRules.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The SR speed data input field acts as an ‘Enter’ button" + Environment.NewLine +
                                "2. Data can be entered in the SR speed data." + Environment.NewLine +
                                "3. The SR speed data input field is ‘Accepted’, with the value in white on a Dark-grey background." + Environment.NewLine +
                                "4. The next data input field (SR Distance) is ‘Selected’, with the value in black on a Medium-grey background." + Environment.NewLine +
                                "5. The SR speed echo text displays ‘40’ in white." + Environment.NewLine +
                                "6. A flashing (visible/invisible) underscore cursor is displayed after the value ‘40’ in the SR speed data input field." + Environment.NewLine +
                                "7. A numeric keypad is displayed for SR Distance." + Environment.NewLine +
                                "8. The keypad contains enabled buttons for the numbers <1> to <9>, <Del> (symbol NA21), <0> and (disabled) <Decimal_Separator>.");

            /*
            Test Step 11
            Action: Perform action step 3-7 for keypad of the ‘SR Distance’ input field
            Expected Result: See the expected results of Step 3 – Step 7 and the following additional information,The pressed key is added in an input field immediately. The cursor is jumped to next position after entered the character immediately.The input field is used to enter the SR Distance
            Test Step Comment: (1) MMI_gen 8295 (partly: MMI_gen 4642 (partly: SR Distance));  (2) MMI_gen 8295 (partly: MMI_gen 4692 (partly: SR Distance));  (3) MMI_gen 8299 (partly: entry);
            */
            DmiActions.ShowInstruction(this, @"Press and hold the <0> key");

            // Spec. says cursor under character entered: gen 4690 says under the next character entry position
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The key is displayed pressed and immediately re-displayed enabled" + Environment.NewLine +
                                "2. The ‘Click’ sound is played once." + Environment.NewLine +
                                "3. The SR distance data input field displays the value of the key pressed." + Environment.NewLine +
                                "4. An underscore character is displayed after the ‘0’ at the end of the SR Speed data input field.");

            DmiActions.ShowInstruction(this, @"Release the <0> key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The <0> key is displayed enabled");

            // Repeat for the ‘1’ key
            DmiActions.ShowInstruction(this, @"Press and hold the <1> key");

            // Spec. says cursor under character entered: gen 4690 says under the next character entry position
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The key is displayed pressed and immediately re-displayed enabled" + Environment.NewLine +
                                "2. The ‘Click’ sound is played once." + Environment.NewLine +
                                "3. The SR distance data input field displays ‘1’ at the end of the data input field." + Environment.NewLine +
                                "4. An underscore character is displayed after the ‘1’ at the end of the SR Speed data input field.");

            DmiActions.ShowInstruction(this, @"Release the <1> key");


            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The <1> key is displayed enabled");

            // Repeat for the <2> key
            DmiActions.ShowInstruction(this, @"Press and hold the <2> key");

            // Spec. says cursor under character entered: gen 4690 says under the next character entry position
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The key is displayed pressed and immediately re-displayed enabled" + Environment.NewLine +
                                "2. The ‘Click’ sound is played once." + Environment.NewLine +
                                "3. The SR distance data input field displays ‘2’ at the end of the data input field." + Environment.NewLine +
                                "4. An underscore character is displayed after the ‘2’ at the end of the SR Speed data input field.");

            DmiActions.ShowInstruction(this, @"Release the <2> key");


            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The <2> key is displayed enabled");

            // Repeat for the <3> key
            DmiActions.ShowInstruction(this, @"Press and hold the <3> key");

            // Spec. says cursor under character entered: gen 4690 says under the next character entry position
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The key is displayed pressed and immediately re-displayed enabled" + Environment.NewLine +
                                "2. The ‘Click’ sound is played once." + Environment.NewLine +
                                "3. The SR distance data input field displays ‘3’ at the end of the data input field." + Environment.NewLine +
                                "4. An underscore character is displayed after the ‘3’ at the end of the SR Speed data input field.");

            DmiActions.ShowInstruction(this, @"Release the <3> key");


            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The <3> key is displayed enabled");

            // Repeat for the <4> key
            DmiActions.ShowInstruction(this, @"Press and hold the <4> key");

            // Spec. says cursor under character entered: gen 4690 says under the next character entry position
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The key is displayed pressed and immediately re-displayed enabled" + Environment.NewLine +
                                "2. The ‘Click’ sound is played once." + Environment.NewLine +
                                "3. The SR distance data input field displays ‘4’ at the end of the data input field." + Environment.NewLine +
                                "4. An underscore character is displayed after the ‘4’ at the end of the SR Speed data input field.");

            DmiActions.ShowInstruction(this, @"Release the <4> key");


            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The <4> key is displayed enabled");

            // Repeat for the <5> key
            DmiActions.ShowInstruction(this, @"Press and hold the <5> key");

            // Spec. says cursor under character entered: gen 4690 says under the next character entry position
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The key is displayed pressed and immediately re-displayed enabled" + Environment.NewLine +
                                "2. The ‘Click’ sound is played once." + Environment.NewLine +
                                "3. The SR distance data input field displays ‘5’ at the end of the data input field." + Environment.NewLine +
                                "4. An underscore character is displayed after the ‘5’ at the end of the SR Speed data input field.");

            DmiActions.ShowInstruction(this, @"Release the <5> key");


            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The <5> key is displayed enabled");

            // Repeat for the <6> key
            DmiActions.ShowInstruction(this, @"Press and hold the <6> key");

            // Spec. says cursor under character entered: gen 4690 says under the next character entry position
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The key is displayed pressed and immediately re-displayed enabled" + Environment.NewLine +
                                "2. The ‘Click’ sound is played once." + Environment.NewLine +
                                "3. The SR distance data input field displays ‘6’ at the end of the data input field." + Environment.NewLine +
                                "4. An underscore character is displayed after the ‘6’ at the end of the SR Speed data input field.");

            DmiActions.ShowInstruction(this, @"Release the <6> key");


            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The <6> key is displayed enabled");

            // Repeat for the <7> key
            DmiActions.ShowInstruction(this, @"Press and hold the <7> key");

            // Spec. says cursor under character entered: gen 4690 says under the next character entry position
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The key is displayed pressed and immediately re-displayed enabled" + Environment.NewLine +
                                "2. The ‘Click’ sound is played once." + Environment.NewLine +
                                "3. The SR distance data input field displays ‘7’ at the end of the data input field." + Environment.NewLine +
                                "4. An underscore character is displayed after the ‘7’ at the end of the SR Speed data input field.");

            DmiActions.ShowInstruction(this, @"Release the <7> key");


            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The <7> key is displayed enabled");

            // Repeat for the <8> key
            DmiActions.ShowInstruction(this, @"Press and hold the <8> key");

            // Spec. says cursor under character entered: gen 4690 says under the next character entry position
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The key is displayed pressed and immediately re-displayed enabled" + Environment.NewLine +
                                "2. The ‘Click’ sound is played once." + Environment.NewLine +
                                "3. The SR distance data input field displays ‘8’ at the end of the data input field." + Environment.NewLine +
                                "4. An underscore character is displayed after the ‘8’ at the end of the SR Speed data input field.");

            DmiActions.ShowInstruction(this, @"Release the <8> key");


            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The <8> key is displayed enabled");

            // Repeat for the <9> key
            DmiActions.ShowInstruction(this, @"Press and hold the <9> key");

            // Spec. says cursor under character entered: gen 4690 says under the next character entry position
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The key is displayed pressed and immediately re-displayed enabled" + Environment.NewLine +
                                "2. The ‘Click’ sound is played once." + Environment.NewLine +
                                "3. The SR distance data input field displays ‘9’ at the end of the data input field." + Environment.NewLine +
                                "4. An underscore character is displayed after the ‘9’ at the end of the SR Speed data input field.");

            DmiActions.ShowInstruction(this, @"Release the <9> key");


            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The <9> key is displayed enabled");
            DmiActions.ShowInstruction(this, @"Release the ‘Del’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. No more characters are deleted from the end of the SR data input field.");

            DmiActions.ShowInstruction(this, "Delete the value in the SR distance data input field");

            /*
            Test Step 12
            Action: Enter the data value with 5 characters
            Expected Result: Verify the following information,The 5 characters are added on an input field as one group. (e.g. ‘10000’)
            Test Step Comment: (1) MMI_gen 8295 (partly: MMI_gen 4694 (partly: NEGATIVE, 6th character));
            */
            DmiActions.ShowInstruction(this, @"Enter the value ‘10000’ in the SR distance data input field.");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The SR speed data input field displays ‘10000’");

            /*
            Test Step 13
            Action: Continue to enter the 6th character
            Expected Result: Verify the following information,The fifth character is shown after a gap of fourth character, separated as 2 groups (e.g. 1000 00)
            Test Step Comment: (1) MMI_gen 8295 (partly: MMI_gen 4694 (partly: MMI_gen 4246));
            */
            DmiActions.ShowInstruction(this, @"Press the <0> key");
            
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The SR speed data input field displays ‘1000 00’ (with a space after the 4th character).");

            /*
            Test Step 14
            Action: Continue to enter the 7th character without deletion
            Expected Result: Verify the following information,The value pressed key is not added into an input field
            Test Step Comment: (1) MMI_gen 1713;
            */
            DmiActions.ShowInstruction(this, @"Press the <0> key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The SR speed data input field still displays ‘1000 00’ (no character added).");
            /*
            Test Step 15
            Action: Delete the old value and enter the new value ‘1000’ for SR Distance.Then, confirm an entered data by pressing an input field
            Expected Result: Verify the following information,Input fieldsThe associated ‘Enter’ button is data field itself.An input field is used to allow the driver to enter data.The state of ‘SR Distance’ input field is changed to ‘accepted’ as follows,The background colour of the Data Area is dark-grey.The colour of data value is white.There is no input field selected.Echo TextsThe echo text of ‘SR Distance’ is changed to white colour.The value of echo text is changed refer to entered data.Data Entry windowThe state of ‘Yes’ button below text label ‘Train data Entry is complete?’ is enabled as follows,The background colour of the Data Area is medium-grey.The colour of data value is black.The colour of border is medium-grey.Packet transmissionUse the log file to confirm that DMI received packet [MMI_CURRENT_SR_RULES (EVC-11)] with variable MMI_M_BUTTONS = 36.Use the log file to confirm that DMI sent out packet [MMI_NEW_SR_RULES (EVC-106)] with following variablesMMI_L_STFF = 1000 MMI_N_DATA_ELEMENTS = 1MMI_NID_DATA = 16 (SR Distance)MMI_M_BUTTONS = 254
            Test Step Comment: (1) MMI_gen 8295 (partly: MMI_gen 4682 (partly: SR Distance));(2) MMI_gen 8295 (partly: MMI_gen 4634 (partly: SR Distance));(3) MMI_gen 8295 (partly: MMI_gen 4652 (partly: SR Distance), MMI_gen 4684 (partly: accepted, SR Distance));(4) MMI_gen 8295 (partly: MMI_gen 4684 (partly: No next input field, data entry process terminated));(5) MMI_gen 8295 (partly: MMI_gen 4700 (partly: SR Distance));(6) MMI_gen 8295 (partly: MMI_gen 4681 (partly: SR Distance), MMI_gen 4698, MMI_gen 4890);(7) MMI_gen 8295 (partly: MMI_gen 4909 (partly: Enabled), MMI_gen 4910 (partly: Enabled, MMI_gen 4211 (partly: colour))); MMI_gen 4374;(8) MMI_gen 9893;(9) MMI_gen 9510 (partly: [Enter] EVC-106); 
            */
            DmiActions.ShowInstruction(this, "Delete the value in the SR distance data input field, enter the value ‘1000’ and confirm by pressing in the data input field");

            //EVC106_MMINewSRRules.CheckMmiLStff = 1000;
            //EVC106_MMINewSRRules.CheckMmiNidData = 16;
            //EVC106_MMINewSRRules.CheckMMiMButtons = Variables.MMI_M_BUTTONS.BTN_ENTER;

            EVC11_MMICurrentSRRules.MMI_M_BUTTONS = MMI_M_BUTTONS.BTN_YES_DATA_ENTRY_COMPLETE;
            EVC11_MMICurrentSRRules.Send();

            // Spec refers to Train data Entry is complete? label: should be ‘SR speed / distance entry complete?’ label
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The SR distance data input field acts as an ‘Enter’ button" + Environment.NewLine +
                                "2. Data can be entered in the SR distance data." + Environment.NewLine +
                                "3. The SR distance data input field is ‘Accepted’, with the value in white on a Dark-grey background." + Environment.NewLine +
                                "4. No input field (SR Distance) is ‘Selected’." + Environment.NewLine +
                                "5. The SR distance echo text displays ‘1000’ in white." + Environment.NewLine +
                                "6. The button below the ‘SR speed / distance entry complete?’ label is displayed enabled, with black text on a Medium-grey background and a Medium-grey border." + Environment.NewLine +
                                "7. A flashing (visible/invisible) underscore cursor is displayed after the value ‘40’ in the SR distance data input field." + Environment.NewLine +
                                "7. A numeric keypad is displayed for SR Distance." + Environment.NewLine +
                                "8. The keypad contains enabled buttons for the numbers <1> to <9>, <Del> (symbol NA21), <0> and (disabled) <Decimal_Separator>.");

            /*
            Test Step 16
            Action: Perform the following procedure,Select ‘SR Speed’ input field.Enter new value for ‘SR Speed’.Select ‘SR Distance’ input field
            Expected Result: Verify the following information,The state of ‘Yes’ button below text label ‘SR speed/distance entry is complete?’ is disabled. The state of input field ‘SR Speed’ is changed to ‘Not selected’ as follows,The value of ‘SR Speed’ input field is removed, display as blank.The background colour of the input field is dark-grey
            Test Step Comment: (1) MMI_gen 8295 (partly: MMI_gen 4909 (partly: state selected and with recently entered key), MMI_gen 4680 (partly: value has been modified));(2) MMI_gen 8295 (partly: MMI_gen 4680 (partly: SR Speed, Not selected, Data area is blank), MMI_gen 4649 (partly: data entry, background colour));
            */
            DmiActions.ShowInstruction(this, "Select the SR speed data input field and enter the value ‘50’, then select the SR distance data input field");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The button below the ‘SR speed / distance entry complete?’ label is displayed disabled." + Environment.NewLine +
                                "2. The SR speed data input field is displayed ‘Not Selected’." + Environment.NewLine +
                                "3. The SR speed data input field displays a blank value on a Dark-grey background.");

            /*
            Test Step 17
            Action: Confirm the value of ‘SR Distance’
            Expected Result: Verify the following information,The state of input field ‘SR Speed’ is changed to ‘Selected’
            Test Step Comment: (1) MMI_gen 8295 (partly: MMI_gen 4685);
            */
            DmiActions.ShowInstruction(this, "Confirm the SR distance value by pressing on its data input field");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The SR distance data input field is displayed ‘Selected’.");

            /*
            Test Step 18
            Action: Enter the value ‘40’ for ‘SR Speed’ value.Press and hold ‘Yes’ button
            Expected Result: Verify the following information,The state of button is changed to ‘Pressed’, the border of button is removed.The sound ‘Click’ is played once
            Test Step Comment: (1) MMI_gen 8295 (partly: MMI_gen 4911 (partly: MMI_gen 4381 (partly: change to state ‘Pressed’ as long as remain actuated)); MMI_gen 4375;(2) MMI_gen 8295 (partly: MMI_gen 4911 (partly: MMI_gen 4381 (partly: sound ‘Click’))); MMI_gen 9512; MMI_gen 968;
            */
            DmiActions.ShowInstruction(this, @"Enter the value ‘40’ for SR Speed, then press and hold the ‘Yes’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Yes’ button is displayed pressed, without a border." + Environment.NewLine +
                                "2. The ‘Click’ sound is played once");

            /*
            Test Step 19
            Action: Slide out the ‘Yes’ button
            Expected Result: Verify the following information,The border of the button is shown (state ‘Enabled’) without a sound
            Test Step Comment: (1) MMI_gen 8295 (partly: MMI_gen 4911 (partly: MMI_gen 4382 (partly: state ‘Enabled’ when slide out with force applied, no sound))); MMI_gen 4374;
            */
            DmiActions.ShowInstruction(this, @"Whilst keeping the ‘Yes’ button pressed, drag it out of its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘Yes’ button is displayed enabled, with a border." + Environment.NewLine +
                                "2. No sound is played.");

            /*
            Test Step 20
            Action: Slide back into the ‘Yes’ button
            Expected Result: Verify the following information,The button is back to state ‘Pressed’ without a sound
            Test Step Comment: (1) MMI_gen 8295 (partly: MMI_gen 4911 (partly: MMI_gen 4382 (partly: state ‘Pressed’ when slide back, no sound))); MMI_gen 4375;
            */
            DmiActions.ShowInstruction(this, @"Whilst keeping the ‘Yes’ button pressed, drag it back inside its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘Yes’ button is displayed pressed." + Environment.NewLine +
                                "2. No sound is played.");

            /*
            Test Step 21
            Action: Release ‘Yes’ button
            Expected Result: Verify the following information,DMI displays Special window.Use the log file to confirm that DMI sent out packet [MMI_NEW_SR_RULES (EVC-106)] with following variablesMMI_N_DATA_ELEMENTS = 0MMI_M_BUTTONS = 36
            Test Step Comment: (1) MMI_gen 8295 (partly: MMI_gen 4911 (partly: MMI_gen 4381 (partly: exit state ‘pressed’), MMI_gen 4384 (partly: ETCS-MMI’s function associated to the button))); MMI_gen 5387 (partly: closure);(2) MMI_gen 9509 (partly: [Yes] EVC-106); MMI_gen 9510 (partly: [Yes] EVC-106); MMI_gen 5387 (partly: transmission);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Release the ‘Yes’ button");

            //EVC106_MMINewSRRules.CheckMMiMButtons = Variables.MMI_M_BUTTONS.BTN_YES_DATA_ENTRY_COMPLETE;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Special window.");

            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = 1;
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.SRSpeedDistance;
            EVC30_MMIRequestEnable.Send();
            /*
            Test Step 22
            Action: Press ‘SR speed / distance’ button
            Expected Result: DMI displays SR speed/distance window.Verify the following information,An entered value from step 8 and 13 are set to the data value of input fields and echo texts as follows,SR Speed = 40SR Distance = 1000
            Test Step Comment: (1) MMI_gen 9509 (partly: All data elements values); MMI_gen 9510 (partly: All data elements values);
            */
            DmiActions.ShowInstruction(this, @"Press the ‘SR speed / distance’ button");

            EVC11_MMICurrentSRRules.MMI_V_STFF = 40;
            EVC11_MMICurrentSRRules.MMI_L_STFF = 1000;
            EVC11_MMICurrentSRRules.DataElements = dataElements;
            EVC11_MMICurrentSRRules.Send();
            
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the SR speed/distance window." + Environment.NewLine +
                                "2. The SR speed data input field and its echo text display the value ‘40’." + Environment.NewLine +
                                "3. The SR distance data input field and its echo text display the value ‘1000’.");

            /*
            Test Step 23
            Action: Confirm the current data without re-entry SR speed and SR distance.Then, press ‘Yes’ button
            Expected Result: Verify the following information,An input field is used to revalidation the SR Speed and SR Distance, DMI displays Special window
            Test Step Comment: (1) MMI_gen 8298 (partly: revalidation); MMI_gen 8299 (partly: revalidation); 
            */
            DmiActions.ShowInstruction(this, @"Confirm the current data by pressing on the data input fields without re-entering the data, then press the ‘Yes’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The data input fields are used to re-validate the SR speed and distance values." + Environment.NewLine +
                                "2. DMI displays the Special window.");

            /*
            Test Step 24
            Action: Press ‘SR spee/distance’ button
            Expected Result: DMI displays SR speed/distance window
            */
            DmiActions.ShowInstruction(this, @"Press the ‘SR speed / distance’ button");

            EVC11_MMICurrentSRRules.MMI_V_STFF = 40;
            EVC11_MMICurrentSRRules.MMI_L_STFF = 1000;
            EVC11_MMICurrentSRRules.DataElements = dataElements;
            EVC11_MMICurrentSRRules.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the SR speed/distance window.");

            /*
            Test Step 25
            Action: Press and hold the Label area of ‘SR Distance’ input field
            Expected Result: Verify the following information,The state of ‘SR Distance’ input field is changed to ‘Pressed’, the border of button is removed.The state of ‘SR Distance’ input field remains ‘not selected’. The state of ‘SR Speed’ input field remains ‘selected’.The sound ‘Click’ is played once
            Test Step Comment: (1) MMI_gen 8295 (partly: MMI_gen 4686 (partly: Label area, SR Distance), MMI_gen 4381 (partly: change to state ‘Pressed’ as long as remain actuated))); MMI_gen 4392 (partly: [Enter], touch screen); MMI_gen 4375;(2) MMI_gen 8295 (partly: MMI_gen 4686 (partly: Label area, SR Distance), MMI_gen 4381 (partly: the sound for Up-Type button));
            */
            DmiActions.ShowInstruction(this, @"Press and hold the Label area of the SR distance data input field");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The SR distance data input field is displayed enabled, with a border." + Environment.NewLine +
                                "2. The SR distance data input field stays ‘Not Selected’." + Environment.NewLine +
                                "3. The SR speed data input field stays ‘Selected’.");

            /*
            Test Step 26
            Action: Slide out the Label area of ‘SR Distance’ input field
            Expected Result: Verify the following information,The border of ‘SR Distance’ input field is shown (state ‘Enabled’) without a sound.The state of ‘SR Distance’ input field remains ‘not selected’. The state of ‘SR Speed’ input field remains ‘selected’
            Test Step Comment: (1) MMI_gen 8295 (partly: MMI_gen 4686 (partly: Label area, SR Distance), MMI_gen 4382 (partly: state ‘Enabled’ when slide out with force applied, no sound); MMI_gen 4374;
            */
            DmiActions.ShowInstruction(this, @"Whilst keeping the SR distance data input field pressed, drag it out of its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The SR distance data input field is displayed enabled, with a border." + Environment.NewLine +
                                "2. No sound is played." + Environment.NewLine +
                                "3. The SR distance data input field stays ‘Not Selected’." + Environment.NewLine +
                                "4. The SR speed data input field stays ‘Selected’.");

            /*
            Test Step 27
            Action: Slide back into the Label area of ‘SR Distance’ input field
            Expected Result: Verify the following information,The state of ‘SR Distance’ input field is changed to ‘Pressed’, the border of button is removed.The state of ‘SR Distance’ input field remains ‘not selected’. The state of ‘SR Speed’ input field remains ‘selected’
            Test Step Comment: (1) MMI_gen 8295 (partly: MMI_gen 4686 (partly: Label area, SR Distance), MMI_gen 4382 (partly: state ‘Pressed’ when slide back, no sound); MMI_gen 4375;
            */

            DmiActions.ShowInstruction(this, @"Whilst keeping the SR distance data input field pressed, drag it back inside its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The SR distance data input field is displayed pressed, without a border." + Environment.NewLine +
                                "2. The SR distance data input field stays ‘Not Selected’." + Environment.NewLine +
                                "3. The SR speed data input field stays ‘Selected’.");
            
            /*
            Test Step 28
            Action: Release the pressed area
            Expected Result: Verify the following information,The state of ‘SR Distance’ input field is changed to selected
            Test Step Comment: (1) MMI_gen 8295 (partly: MMI_gen 4686 (partly: Label area, SR Distance), MMI_gen 4381 (partly: exit state ‘Pressed’, execute function associated to the button)); MMI_gen 4374;
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Release the pressed area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The SR distance data input field is displayed ‘Selected’.");

            /*
            Test Step 29
            Action: Perform action step 21-24 for the Label area of SR Speed input field
            Expected Result: Verify the following information,The state of an input field is changed to ‘selected’ when release the pressed area at the Label area of input field
            Test Step Comment: (1) MMI_gen 8295 (partly: MMI_gen 4686 (partly: Label area, Up-Type button));
            */
            // Done releasing, do steps 22-24
            DmiActions.ShowInstruction(this, @"Press and release the ‘Yes’ button, then press the ‘SR speed / distance’ button");

            EVC11_MMICurrentSRRules.MMI_V_STFF = 40;
            EVC11_MMICurrentSRRules.MMI_L_STFF = 1000;
            EVC11_MMICurrentSRRules.DataElements = dataElements;
            EVC11_MMICurrentSRRules.Send();

            DmiActions.ShowInstruction(this, @"Confirm the current data by pressing on the Label area of the SR speed data input field without re-entering the data, then release the Label area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The SR speed data input field is displayed ‘Selected’.");

            DmiActions.ShowInstruction(this, @" then press the ‘Yes’ button");            

            DmiActions.ShowInstruction(this, @"Press the ‘SR speed / distance’ button");

            EVC11_MMICurrentSRRules.MMI_V_STFF = 40;
            EVC11_MMICurrentSRRules.MMI_L_STFF = 1000;
            EVC11_MMICurrentSRRules.DataElements = dataElements;
            EVC11_MMICurrentSRRules.Send();

            /*
            Test Step 30
            Action: Perform action step 21-24 for the Data area of each input field
            Expected Result: Verify the following information,The state of an input field is changed to ‘selected’ when release the pressed area at the Data area of input field
            Test Step Comment: (1) MMI_gen 8295 (partly: MMI_gen 4686 (partly: Data area)); MMI_gen 9390 (partly: SR Speed/Distance window);
            */
            // Makes no sense, repeat 29 for each data input field

            DmiActions.ShowInstruction(this, @"Press and release the ‘Yes’ button, then press the ‘SR speed / distance’ button");
            
            EVC11_MMICurrentSRRules.MMI_V_STFF = 40;
            EVC11_MMICurrentSRRules.MMI_L_STFF = 1000;
            EVC11_MMICurrentSRRules.DataElements = dataElements;
            EVC11_MMICurrentSRRules.Send();

            DmiActions.ShowInstruction(this, @"Confirm the current data by pressing on the Data area of the SR speed data input field without re-entering the data, then release the Label area");
            
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The SR speed data input field is displayed ‘Selected’.");
            
            /*
            Test Step 31
            Action: Use the test script file 22_9_1_a.xml to send EVC-11
            Expected Result: Verify the following information,The value of an input fields for SR speed and SR distance are changed refer to received packet as follows,SR Speed = 100SR Distance = 100000
            Test Step Comment: (1) MMI_gen 1705 (partly: MMI_gen 9887); MMI_gen 1709 (partly: displayed in integers, no leading ‘0’ displayed); MMI_gen 1710 (partly: displayed in integers, no leading ‘0’ displayed);
            */
            XML.XML_22_9_1_a.Send(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The SR speed data input field displays ‘100’." + Environment.NewLine +
                                "2. The SR distance data input field displays ‘100000’.");

            /*
            Test Step 32
            Action: Use the test script file 22_9_1_b.xml to send EVC-11 with,MMI_N_DATA_ELEMENTS = 0MMI_V_STFF = 601MMI_L_STFF = 100001
            Expected Result: Verify the following information,The value of input fields for SR speed and SR distance are changed to blank
            Test Step Comment: (1) MMI_gen 1709 (partly: out of range); MMI_gen 1710 (partly: out of range);
            */
            XML.XML_22_9_1_b.Send(this);


            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The SR speed data input field is blank." + Environment.NewLine +
                                "2. The SR distance data input field is blank.");
            
            /*
            Test Step 33
            Action: Press ‘Close’ button
            Expected Result: Verify the following information,Use the log file to confirm that DMI sent out packet [MMI_DRIVER_REQUEST (EVC-101)] with variable MMI_M_REQUEST = 12 (Exit Change SR rules).Use the log file to confirm that DMI sent out packet [MMI_ENABLE_REQUEST (EVC-30)] with variable MMI_NID_WINDOW = 254.The window is closed and the Special window is displayed
            Test Step Comment: (1) MMI_gen 1716 (partly: EVC-101);(2) MMI_gen 1716 (partly: MMI_gen 12071 (partly: EVC-30), MMI_gen 4355 (partly: [Close]));(3) MMI_gen 1716 (partly: MMI_gen 12071 (partly: closure), MMI_gen 4355 (partly: [Close])); MMI_gen 4392 (partly: returning to the parent window);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button");

            EVC101_MMIDriverRequest.CheckMRequestPressed = MMI_M_REQUEST.ExitChangeSRrules;

            // Spec says DMI receives EVC30 ??
            //EVC30_MMIRequestEnable.SendBlank();
            //EVC30_MMIRequestEnable.MMI_NID_WINDOW = 254;
            //EVC30_MMIRequestEnable.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI closes the SR speed/distance window and displays the Special window.");

            /*
            Test Step 34
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
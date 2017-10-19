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
    /// 27.8.1.1
    /// TC-ID: 22.8.1.1
    /// 
    /// This test case verifies the display of the ‘RBC ID’ window on DMI that shall comply with [ERA-ERTMS] standard and [MMI-ETCS-gen].
    /// 
    /// Tested Requirements:
    /// MMI_gen 9445; MMI_gen 9455; MMI_gen 8054; MMI_gen 8055; MMI_gen 1628; MMI_gen 9457; MMI_gen 1616; MMI_gen 1620; MMI_gen 1623; MMI_gen 9458 (partly: touch screen, MMI_gen 1623, MMI_gen 1625); MMI_gen 9472 (partly: [yes], EVC-112, MMI_gen 1623); MMI_gen 8057; MMI_gen 8058; MMI_gen 8053 (partly: MMI_gen 4888, MMI_gen 4799 (partly: Close button, Previous button, Next button, Window Title), MMI_gen 4891 (partly: Yes button, Area for [Window Title] Entry complete?), MMI_gen 4910 (partly: Disabled, MMI_gen 4211 (partly: colour)), MMI_gen 4909, MMI_gen 4908 (partly: extended), MMI_gen 4637 (partly: Main-areas D and F), MMI_gen 4640, MMI_gen 4641, MMI_gen 9412, MMI_gen 4645, MMI_gen 4646 (partly: right aligned), MMI_gen 4647 (partly: left aligned), MMI_gen 4648, MMI_gen 4720, MMI_gen 4651, MMI_gen 4683, MMI_gen 5211, MMI_gen 4649, MMI_gen 4912, MMI_gen 4678, MMI_gen 5190, MMI_gen 4691 (partly: flash), MMI_gen 4689, MMI_gen 4690, MMI_gen 4913 (partly: MMI_gen 4384, MMI_gen 4386), MMI_gen 4679, MMI_gen 4642, MMI_gen 4692, MMI_gen 4682, MMI_gen 4634, MMI_gen 4652, MMI_gen 4684, MMI_gen 4681, MMI_gen 4694 (partly: MMI_gen 4246), MMI_gen 4911 (partly: MMI_gen 4381, MMI_gen 4382), MMI_gen 4686, MMI_gen 4680, MMI_gen 4685, MMI_gen 5003); MMI_gen 4392 (partly: [Close] NA11, [Delete] NA21, [Enter], touch screen, returning to the parent window); MMI_gen 4355 (partly: Buttons, Close button); MMI_gen 4377 (partly: shown); MMI_gen 4355 (partly: input fields); MMI_gen 4374; MMI_gen 4375; MMI_gen 9512; MMI_gen 968; MMI_gen 9468; MMI_gen 9454; MMI_gen 9390 (partly: RBC Data window); MMI_gen 4393 (partly: [Delete]);
    /// 
    /// Scenario:
    /// RBC ID window in cabin inactive state is verified.Perform SoM until Level 2 is selected and confirmed. After open RBC ID window, the window appearance is verified.The data entry functionality of the RBC ID window is verified with the following type of button in keypad,The RBC ID input field with Numeric keyboard.The RBC Phone number input field with Numeric keyboard.The Up-type button on ‘Yes’ button.The data revalidation functionality of the RBC ID window is verified.The Up-Type button on each label part of an input field is verified.The Up-Type button on each data part of an input field is verified.The packet information handling including with an special value is verified.The functionality of ‘Close’ button is verified.
    /// 
    /// Used files:
    /// 22_8_1_1.utt, 22_8_1_1_a.xml, 22_8_1_1_b.xml, 22_8_1_1_c.xml, 22_8_1_1_d.xml
    /// </summary>
    public class TC_ID_22_8_1_1_RBC_Data_window : TestcaseBase
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
            // DMI displays in SB mode, level 2

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint

            /*
            Test Step 1
            Action: Use the test script file 22_8_1_1_a.xml to send EVC-22,MMI_NID_WINDOW = 10MMI_N_DATA_ELEMENTS = 0MMI_NID_RBC = 1234MMI_NID_RADIO = 0x1234FFFFFFFFFFFF
            Expected Result: DMI does not display RBC data window
            Test Step Comment: MMI_gen 9445 (partly: NEGATIVE, inactive);
            */
            XML_22_8_1_1(msgType.typea);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. DMI does not display the RBC data window");

            /*
            Test Step 2
            Action: Perform the following procedure,Activate cabin A.Enter ID and perform brake test.Select and confirm Level 2.Press ‘Enter RBC data’ button
            Expected Result: Verify the following information,Data entry windowThe window title is ‘RBC data’.The text label of the window title is right aligned.The following objects are displayed in Train data window,  Enabled Close button (NA11)Window TitleInput fieldThe following objects are additionally displayed in Train data window,Yes buttonThe text label ‘RBC data entry complete?’Yes button is displayed in Disabled state as follows,Text label is black Background colour is dark-greyThe border colour is medium-grey the same as the input field’s colour.The sensitive area of Yes button is extended from text label ‘RBC data entry complete?’Input fieldsThe input fields are located on Main area D and F.Each input field is devided into a Label Area and a Data Area.The Label Area is give the topic of the input field.The Label Area text is displayed corresponding to the input field i.e. RBC ID, RBC Phone Number. The Label Area is placed to the left of The Data Area.The text in the Label Area is aligned to the right.The value of data in the Data Area is aligned to the left.The text colour of the Label Area is grey and the background colour of the Label Area is dark-grey.There are only 2 input fields displayed in the window.The first input field is in state ‘Selected’ as follows,The background colour of the Data Area is medium-grey.The colour of data value is black.All other input fields are in state ‘Not selected’ as follows,The background colour of the Data Area is dark-grey.The colour of data value is grey.KeyboardThe keyboard associated to selected input field ‘RBC ID’ is Numeric keyboard.The keyboard contains enabled button for the number <1> to <9>, <Delete>(NA21) , <0> and disabled <Decimal_Separator>. NA21, Delete buttonLayersThe level of layers of all areas in window are in Layer 0.Entering CharactersThe cursor is flashed by changing from visible to not visible.The cursor is displayed as a horizontal line below the position of the next character to be entered.Packet transmissionUse the log file to confirm that DMI received packet EVC-22 with the variable MMI_NID_WINDOW = 10.DMI displays RBC data window.The data part of each input field is filled according to received packet EVC-22 with following variables,MMI_NID_RBC = RBC IDMMI_NID_RADIO = RBC Phone number
            Test Step Comment: (1) MMI_gen 8054;(2) MMI_gen 8053 (partly: MMI_gen 4888);(3) MMI_gen 8053 (partly: MMI_gen 4799 (partly: Close button, Previous button, Next button, Window Title)); MMI_gen 4392 (partly: [Close] NA11); MMI_gen 4355 (partly: Buttons, Close button);(4) MMI_gen 8053 (partly: MMI_gen 4891 (partly: Yes button, Area for [Window Title] Entry complete?));(5) MMI_gen 8053 (partly: MMI_gen 4910 (partly: Disabled, MMI_gen 4211 (partly: colour)), MMI_gen 4909 (partly: Disabled)); MMI_gen 4377 (partly: shown);(6) MMI_gen 8053 (partly: MMI_gen 4908 (partly: extended));(7) MMI_gen 8053 (partly: MMI_gen 4637 (partly: Main-areas D and F)); MMI_gen 4355 (partly: input fields);(8) MMI_gen 8053 (partly: MMI_gen 4640);(9) MMI_gen 8053 (partly: MMI_gen 4641);(10) MMI_gen 8053 (partly: MMI_gen 9412);(11) MMI_gen 8053 (partly: MMI_gen 4645); MMI_gen 8055 (partly: label); MMI_gen 8057 (partly: label);(12) MMI_gen 8053 (partly: MMI_gen 4646 (partly: right aligned));(13) MMI_gen 8053 (partly: MMI_gen 4647 (partly: left aligned));(14) MMI_gen 8053 (partly: MMI_gen 4648);(15) MMI_gen 8053 (partly: MMI_gen 4720);(16) MMI_gen 8053 (partly: MMI_gen 4651 (partly: RBC ID), MMI_gen 4683 (partly: selected), MMI_gen 5211 (partly: selected));(17) MMI_gen 8053 (partly: MMI_gen 4649 (partly: selected ‘RBC ID’), MMI_gen 4651 (partly: SR Distance), MMI_gen 4683 (partly: not selected), MMI_gen 5211 (partly: not selected));(18) MMI_gen 8058 (partly: RBC ID); MMI_gen 8053 (partly: MMI_gen 4912 ( partly: RBC ID), MMI_gen 4678 (partly: RBC ID));(19) MMI_gen 8053  (partly: MMI_gen 5003 (partly: RBC ID)); MMI_gen 4392 (partly: [Delete] NA21);(20) MMI_gen 8053 (partly: MMI_gen 5190);(21) MMI_gen 8053 (partly: MMI_gen 4691 (partly: flash, RBC ID));(22) MMI_gen 8053 (partly: MMI_gen 4689, MMI_gen 4690);(23) MMI_gen 9445 (partly: EVC-22); (34) MMI_gen 9445 (partly: Display RBC data window);(25) MMI_gen 9457;
            */
            DmiActions.Activate_Cabin_1(this);
            DmiActions.Set_Driver_ID(this, "1234");
            DmiActions.ShowInstruction(this, "Accept the Driver ID");

            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Level = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_LEVEL.L2;
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = 1;
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.EnterRBCData;
            EVC30_MMIRequestEnable.Send();

            DmiActions.ShowInstruction(this, "Press the ‘Enter RBC data’ button");

            EVC22_MMICurrentRBC.MMI_NID_WINDOW = 10;
            EVC22_MMICurrentRBC.NID_RBC = 1234;
            EVC22_MMICurrentRBC.MMI_NID_RADIO = 0x1234FFFFFFFFFFFF;
            EVC22_MMICurrentRBC.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the RBC data window with the title ‘RBC data’, right-aligned." + Environment.NewLine +
                                "2. The window displays an Enabled Close button, symbol NA11, two data input fields, a ‘Yes’ button and a label with ‘RBC data entry complete?’." + Environment.NewLine +
                                "3. The ‘Yes’ button is displayed disabled with black text label on a Dark-grey Background with a Medium-grey border." + Environment.NewLine +
                                "4. The data input field has a Medium-grey border." + Environment.NewLine +
                                "5. The sensitive area of Yes button extends from the label ‘RBC data entry complete?’." + Environment.NewLine +
                                "6.	The data input fields are displayed in areas D and F and contain a Label Area and a Data Area." + Environment.NewLine +
                                "7. The Label Area is displayed to the left of The Data Area with right-aligned text in grey on a Dark-grey background." + Environment.NewLine +
                                "8. The value of the data input field is left-aligned." + Environment.NewLine +
                                "9. The first data input field is ‘Selected’, with the value in black on a Medium-grey background." + Environment.NewLine +
                                "10. The other data input field is ‘Not selected’ with the value in grey on a Dark-grey background." + Environment.NewLine +
                                "11. A numeric keypad is displayed for the (‘Selected’) ‘RBC ID’ data input field." + Environment.NewLine +
                                "12. The keypad contains enabled keys for the numbers <1> to <9>, a <Delete> button, symbol NA21, a <0> and a (disabled) <Decimal_Separator>." + Environment.NewLine +
                                "13. All areas in the window are in Layer 0." + Environment.NewLine +
                                "14. A flashing (visible/invisible) underscore is displayed as a cursor, at the position of the next character to be entered." + Environment.NewLine +
                                "15. The data input fields display ‘1234’ for RBC ID and ‘3054632954294967295’ for RBC Phone number.");

            /*
            Test Step 3
            Action: Press and hold ‘0’ button
            Expected Result: Verify the following information,The state of button is changed to ‘Pressed’ and immediately back to ‘Enabled’ state.The sound ‘Click’ is played once.The Input Field displays the value associated to the data key according to the pressings in state ‘Pressed’.The cursor is displayed as horizontal line below the value of the numeric-keyboard data key in the input field.The input field is used to enter the RBC ID
            Test Step Comment: (1) MMI_gen 8053 (partly: MMI_gen 4913 (partly: RBC ID), MMI_gen 4384 (partly: Change to state ‘Pressed’ and immediately back to state ‘Enabled’));   (2) MMI_gen 8053 (partly: MMI_gen 4913 (partly: RBC ID), MMI_gen 4384 (partly: sound ‘Click’)); MMI_gen 9512; MMI_gen 968;(3) MMI_gen 8053 (partly: MMI_gen 4679 (partly: RBC ID), MMI_gen 4913 (partly: RBC ID), MMI_gen 4384 (partly: ETCS-MMI’s function associated to the button));(4) MMI_gen 8053 (partly: MMI_gen 4689, MMI_gen 4690);(5) MMI_gen 8055 (partly: entry);
            */
            DmiActions.ShowInstruction(this, @"Press and hold the ‘0’ button");

            // Spec says cursor is underneath the character entered but gen 4690 says under the next character to be entered...
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The button is displayed pressed and immedately re-displayed enabled." + Environment.NewLine +
                                @"2. The ‘Click’ sound is played once." + Environment.NewLine +
                                "3. The RBC ID data input field displays the value of the button pressed." + Environment.NewLine +
                                "4. A cursor is displayed after the character just added to the data input field.");


            /*
            Test Step 4
            Action: Release the pressed button
            Expected Result: Verify the following information,The state of released button is changed to enabled
            Test Step Comment: (1) MMI_gen 8053 (partly: MMI_gen 4913 (partly: RBC ID), MMI_gen 4384 (partly: ETCS-MMI’s function associated to the button));
            */
            DmiActions.ShowInstruction(this, @"Release the pressed button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The button is displayed enabled.");

            /*
            Test Step 5
            Action: Perform action step 3-4 for the ‘1’ to ‘9’ buttons.Note: Press the ‘Del’ button to delete an information when entered data is out of input field range is acceptable
            Expected Result: See the expected results of Step 3 – Step 4 and the following additional information,The pressed key is added in an input field immediately. The cursor is jumped to next position after entered the character immediately
            Test Step Comment: (1) MMI_gen 8053 (partly: MMI_gen 4642 (partly: RBC ID));  (2) MMI_gen 8053 (partly: MMI_gen 4692 (partly: RBC ID));  
            */
            // Repeat for the 1 button
            DmiActions.ShowInstruction(this, @"Press and hold the ‘1’ button");

            // Spec says cursor is underneath the character entered but gen 4690 says under the next character to be entered...
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The button is displayed pressed and immedately re-displayed enabled." + Environment.NewLine +
                                @"2. The ‘Click’ sound is played once." + Environment.NewLine +
                                "3. The RBC ID data input field displays the value of the button pressed." + Environment.NewLine +
                                "4. A cursor is displayed after the character just added to the data input field.");

            DmiActions.ShowInstruction(this, @"Release the pressed button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The button is displayed enabled.");

            // Repeat for the 2 button
            DmiActions.ShowInstruction(this, @"Press and hold the ‘2’ button");

            // Spec says cursor is underneath the character entered but gen 4690 says under the next character to be entered...
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The button is displayed pressed and immedately re-displayed enabled." + Environment.NewLine +
                                @"2. The ‘Click’ sound is played once." + Environment.NewLine +
                                "3. The RBC ID data input field displays the value of the button pressed." + Environment.NewLine +
                                "4. A cursor is displayed after the character just added to the data input field.");

            DmiActions.ShowInstruction(this, @"Release the pressed button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The button is displayed enabled.");

            // Repeat for the 3 button
            DmiActions.ShowInstruction(this, @"Press and hold the ‘3’ button");

            // Spec says cursor is underneath the character entered but gen 4690 says under the next character to be entered...
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The button is displayed pressed and immedately re-displayed enabled." + Environment.NewLine +
                                @"2. The ‘Click’ sound is played once." + Environment.NewLine +
                                "3. The RBC ID data input field displays the value of the button pressed." + Environment.NewLine +
                                "4. A cursor is displayed after the character just added to the data input field.");

            DmiActions.ShowInstruction(this, @"Release the pressed button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The button is displayed enabled.");
            // Repeat for the 4 button
            DmiActions.ShowInstruction(this, @"Press and hold the ‘4’ button");

            // Spec says cursor is underneath the character entered but gen 4690 says under the next character to be entered...
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The button is displayed pressed and immedately re-displayed enabled." + Environment.NewLine +
                                @"2. The ‘Click’ sound is played once." + Environment.NewLine +
                                "3. The RBC ID data input field displays the value of the button pressed." + Environment.NewLine +
                                "4. A cursor is displayed after the character just added to the data input field.");

            DmiActions.ShowInstruction(this, @"Release the pressed button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The button is displayed enabled.");

            // Repeat for the 5 button
            DmiActions.ShowInstruction(this, @"Press and hold the ‘5’ button");

            // Spec says cursor is underneath the character entered but gen 4690 says under the next character to be entered...
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The button is displayed pressed and immedately re-displayed enabled." + Environment.NewLine +
                                @"2. The ‘Click’ sound is played once." + Environment.NewLine +
                                "3. The RBC ID data input field displays the value of the button pressed." + Environment.NewLine +
                                "4. A cursor is displayed after the character just added to the data input field.");

            DmiActions.ShowInstruction(this, @"Release the pressed button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The button is displayed enabled.");

            // Repeat for the 6 button
            DmiActions.ShowInstruction(this, @"Press and hold the ‘6’ button");

            // Spec says cursor is underneath the character entered but gen 4690 says under the next character to be entered...
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The button is displayed pressed and immedately re-displayed enabled." + Environment.NewLine +
                                @"2. The ‘Click’ sound is played once." + Environment.NewLine +
                                "3. The RBC ID data input field displays the value of the button pressed." + Environment.NewLine +
                                "4. A cursor is displayed after the character just added to the data input field.");

            DmiActions.ShowInstruction(this, @"Release the pressed button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The button is displayed enabled.");

            // Repeat for the 7 button
            DmiActions.ShowInstruction(this, @"Press and hold the ‘7’ button");

            // Spec says cursor is underneath the character entered but gen 4690 says under the next character to be entered...
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The button is displayed pressed and immedately re-displayed enabled." + Environment.NewLine +
                                @"2. The ‘Click’ sound is played once." + Environment.NewLine +
                                "3. The RBC ID data input field displays the value of the button pressed." + Environment.NewLine +
                                "4. A cursor is displayed after the character just added to the data input field.");

            DmiActions.ShowInstruction(this, @"Release the pressed button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The button is displayed enabled.");

            // Repeat for the 8 button
            DmiActions.ShowInstruction(this, @"Press and hold the ‘8’ button");

            // Spec says cursor is underneath the character entered but gen 4690 says under the next character to be entered...
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The button is displayed pressed and immedately re-displayed enabled." + Environment.NewLine +
                                @"2. The ‘Click’ sound is played once." + Environment.NewLine +
                                "3. The RBC ID data input field displays the value of the button pressed." + Environment.NewLine +
                                "4. A cursor is displayed after the character just added to the data input field.");

            DmiActions.ShowInstruction(this, @"Release the pressed button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The button is displayed enabled.");

            // Repeat for the 9 button
            DmiActions.ShowInstruction(this, @"Press and hold the ‘9’ button");

            // Spec says cursor is underneath the character entered but gen 4690 says under the next character to be entered...
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The button is displayed pressed and immedately re-displayed enabled." + Environment.NewLine +
                                @"2. The ‘Click’ sound is played once." + Environment.NewLine +
                                "3. The RBC ID data input field displays the value of the button pressed." + Environment.NewLine +
                                "4. A cursor is displayed after the character just added to the data input field.");

            DmiActions.ShowInstruction(this, @"Release the pressed button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The button is displayed enabled.");

            /*
            Test Step 6
            Action: Press and hold ‘Del’ button.Note: Stopwatch is required
            Expected Result: Verify the following information,While press and hold button less than 1.5 secSound ‘Click’ is played once.The state of button is changed to ‘Pressed’ and immediately back to ‘Enabled’ state.The last character is removed from an input field after pressing the button.While press and hold button over 1.5 secThe state ‘pressed’ and ‘released’ are switched repeatly while button is pressed and the characters are removed from an input field repeatly refer to pressed state.The sound ‘Click’ is played repeatly while button is pressed
            Test Step Comment: (1) MMI_gen 8053 (partly: MMI_gen 4913 (partly: RBC ID), MMI_gen 4384 (partly: sound ‘Click’));(2) MMI_gen 8053 (partly: MMI_gen 4913 (partly: RBC ID), MMI_gen 4384 (partly: Change to state ‘Pressed’ and immediately back to state ‘Enabled’));    (3) MMI_gen 8053 (partly: MMI_gen 4913 (partly: RBC ID), MMI_gen 4384 (partly: ETCS-MMI’s function associated to the button)); MMI_gen 4393 (partly: [Delete]);(4) MMI_gen 8053  (partly: MMI_gen 4913 (partly: RBC ID), MMI_gen 4386 (partly: visual of repeat function));(5) MMI_gen 8053 (partly: MMI_gen 4913 (partly: RBC ID), MMI_gen 4386 (partly: audible of repeat function));
            */
            DmiActions.ShowInstruction(this, @"Press and hold the ‘Del’ button. Note: Stopwatch is required");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The button is displayed pressed and immedately re-displayed enabled." + Environment.NewLine +
                                @"2. The ‘Click’ sound is played once." + Environment.NewLine +
                                "3. The last character in the data input field is deleted." +
                                "4. After the button has been pressed for 1.5s, the button changes repeatedly between pressed and enabled." + Environment.NewLine +
                                "5. The ‘Click’ sound is played repeatedly." + Environment.NewLine +
                                "6. The last character in the data input field is deleted repeatedly.");

            /*
            Test Step 7
            Action: Release ‘Del’ button
            Expected Result: Verify the following information, The character is stop removing
            Test Step Comment: (1) MMI_gen 8053 (partly: MMI_gen 4913 (partly: RBC ID)), MMI_gen 4384 (partly: ETCS-MMI’s function associated to the button));
            */
            DmiActions.ShowInstruction(this, @"Release ‘Del’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Characters in the data input field are not deleted.");
            
            /*
            Test Step 8
            Action: Enter the data value with 5 characters
            Expected Result: Verify the following information,The 5 characters are added on an input field as one group. (e.g. ‘10000’)
            Test Step Comment: (1) MMI_gen 8053 (partly: MMI_gen 4694 (partly: NEGATIVE, 6th character));
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Enter the data value ‘10000’");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The data input field displays ‘10000’, with no spaces between the digits.");

            /*
            Test Step 9
            Action: Continue to enter the 6th character
            Expected Result: Verify the following information,The fifth character is shown after a gap of fourth character, separated as 2 groups (e.g. 1000 00)
            Test Step Comment: (1) MMI_gen 8053 (partly: MMI_gen 4694 (partly: MMI_gen 4246));
            */
            DmiActions.ShowInstruction(this, @"Press the ‘0’ key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The data input field displays ‘1000 00’, with a space between ‘1000’ and ‘00’.");

            /*
            Test Step 10
            Action: Continue to enter the new value more than 8 characters
            Expected Result: Verify the following information,The data value is separated as 2 lines. In each line is displayed only 8 characters
            Test Step Comment: (1) MMI_gen 8053 (partly: MMI_gen 4694 (partly: MMI_gen 4247));
            */
            DmiActions.ShowInstruction(this, @"Press the ‘0’ key twice, then press the ‘1’ key twice");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The data input field displays the value on two lines: " + Environment.NewLine +
                                "‘10000000’" + Environment.NewLine +
                                "‘11’" + Environment.NewLine +
                                "   a total of eight characters on line 1 (with no space between ‘1000’ and ‘0000’.");

            /*
            Test Step 11
            Action: Delete the old value and enter the new value ‘6996969’ for RBC ID.Then, confirm an entered data by pressing an input field
            Expected Result: Verify the following information,Input fieldsThe associated ‘Enter’ button is data field itself.An input field is used to allow the driver to enter data.The state of ‘RBC ID’ input field is changed to ‘accepted’ as follows,The background colour of the Data Area is dark-grey.The colour of data value is white.The next input field ‘RBC Phone number’ is in state ‘selected’ as follows,The background colour of the Data Area is medium-grey.The colour of data value is black.Entering CharactersThe cursor is displayed as a horizontal line below the position of the next character to be entered.The cursor is flashed by changing from visible to not visible.KeyboardThe keyboard associated to selected input field ‘RBC Phone number’ is Numeric keyboard.The keyboard contains enabled button for the number <1> to <9>, <Delete>(NA21) , <0> and disabled <Decimal_Separator>. NA21, Delete button.Packet transmissionUse the log file to confirm that DMI sent out packet [MMI_NEW_RBC_DATA (EVC-112)] with following variables,MMI_N_DATA_ELEMENTS = 1MMI_NID_DATA = 4 (RBC ID)MMI_M_BUTTONS = 254MMI_NID_RBC = 6996969
            Test Step Comment: (1) MMI_gen 8053 (partly: MMI_gen 4682 (partly: RBC ID));(2) MMI_gen 8053 (partly: MMI_gen 4634 (partly: RBC ID));(3) MMI_gen 8053 (partly: MMI_gen 4652 (partly: RBC ID), MMI_gen 4684 (partly: accepted, RBC ID), MMI_gen 4681 (partly: RBC ID));(4) MMI_gen 8053 (partly: MMI_gen 4684 (partly: RBC Phone number, selected automatically), MMI_gen 4651 (partly: RBC Phone number));(5) MMI_gen 8053 (partly: MMI_gen 4689, MMI_gen 4690);(6) MMI_gen 8053 (partly: MMI_gen 4691 (partly: flash, RBC Phone number));(7) MMI_gen 8058 (partly: RBC Phone number); MMI_gen 8053 (partly: MMI_gen 4912 (partly: RBC Phone number), MMI_gen 4678 (partly: RBC Phone number));(8) MMI_gen 8053 (partly: MMI_gen 5003 (partly: RBC Phone number)); MMI_gen 4392 (partly: [Delete] NA21);(9) MMI_gen 9458 (partly: [Enter] EVC-112, RBC ID, touch screen);  
            */
            DmiActions.ShowInstruction(this, @"Delete the old value and enter ‘6996969’ for RBC ID, then confirm by pressing in the data input field");

            EVC112_MMINewRbcData.MMI_NID_DATA = new List<byte> { 4 };
            EVC112_MMINewRbcData.MMI_M_BUTTONS = Variables.MMI_M_BUTTONS_RBC_DATA.BTN_ENTER;
            EVC112_MMINewRbcData.MMI_NID_RBC = 6996969;
            EVC112_MMINewRbcData.CheckPacketContent();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The data input field acts as an ‘Enter’ button." + Environment.NewLine +
                                "2. Data can be entered in the data input field" + Environment.NewLine +
                                "3. The data input field is displayed with the value in white on a Dark-grey background." + Environment.NewLine +
                                "4. The next data input field is displayed ‘Selected’ with the value in black on a Medium-grey background.");

            /*
            Test Step 12
            Action: Perform action step 2-7 for ‘RBC Phone number’ input field
            Expected Result: See the expected results of Step 2 – Step 7 and the following additional information,The pressed key is added in an input field immediately. The cursor is jumped to next position after entered the character immediately.The input field is used to enter the RBC Phone number
            Test Step Comment: (1) MMI_gen 8053 (partly: MMI_gen 4642 (partly: RBC Phone number));  (2) MMI_gen 8053 (partly: MMI_gen 4692 (partly: RBC Phone number));  (3) MMI_gen 8057 (partly: entry);
            */
            // Not step 2 but 3-7!
            DmiActions.ShowInstruction(this, @"Press and hold the ‘0’ button");

            // Spec says cursor is underneath the character entered but gen 4690 says under the next character to be entered...
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The button is displayed pressed and immedately re-displayed enabled." + Environment.NewLine +
                                @"2. The ‘Click’ sound is played once." + Environment.NewLine +
                                "3. The RBC Phone number data input field displays the value of the button pressed." + Environment.NewLine +
                                "4. A cursor is displayed after the character just added to the data input field.");
            DmiActions.ShowInstruction(this, @"Release the pressed button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The button is displayed enabled.");
            // Repeat for the 1 button
            DmiActions.ShowInstruction(this, @"Press and hold the ‘1’ button");

            // Spec says cursor is underneath the character entered but gen 4690 says under the next character to be entered...
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The button is displayed pressed and immedately re-displayed enabled." + Environment.NewLine +
                                @"2. The ‘Click’ sound is played once." + Environment.NewLine +
                                "3. The RBC Phone number data input field displays the value of the button pressed." + Environment.NewLine +
                                "4. A cursor is displayed after the character just added to the data input field.");

            DmiActions.ShowInstruction(this, @"Release the pressed button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The button is displayed enabled.");

            // Repeat for the 2 button
            DmiActions.ShowInstruction(this, @"Press and hold the ‘2’ button");

            // Spec says cursor is underneath the character entered but gen 4690 says under the next character to be entered...
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The button is displayed pressed and immedately re-displayed enabled." + Environment.NewLine +
                                @"2. The ‘Click’ sound is played once." + Environment.NewLine +
                                "3. The RBC Phone number data input field displays the value of the button pressed." + Environment.NewLine +
                                "4. A cursor is displayed after the character just added to the data input field.");

            DmiActions.ShowInstruction(this, @"Release the pressed button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The button is displayed enabled.");

            // Repeat for the 3 button
            DmiActions.ShowInstruction(this, @"Press and hold the ‘3’ button");

            // Spec says cursor is underneath the character entered but gen 4690 says under the next character to be entered...
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The button is displayed pressed and immedately re-displayed enabled." + Environment.NewLine +
                                @"2. The ‘Click’ sound is played once." + Environment.NewLine +
                                "3. The RBC Phone number data input field displays the value of the button pressed." + Environment.NewLine +
                                "4. A cursor is displayed after the character just added to the data input field.");

            DmiActions.ShowInstruction(this, @"Release the pressed button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The button is displayed enabled.");
            // Repeat for the 4 button
            DmiActions.ShowInstruction(this, @"Press and hold the ‘4’ button");

            // Spec says cursor is underneath the character entered but gen 4690 says under the next character to be entered...
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The button is displayed pressed and immedately re-displayed enabled." + Environment.NewLine +
                                @"2. The ‘Click’ sound is played once." + Environment.NewLine +
                                "3. The RBC Phone number data input field displays the value of the button pressed." + Environment.NewLine +
                                "4. A cursor is displayed after the character just added to the data input field.");

            DmiActions.ShowInstruction(this, @"Release the pressed button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The button is displayed enabled.");

            // Repeat for the 5 button
            DmiActions.ShowInstruction(this, @"Press and hold the ‘5’ button");

            // Spec says cursor is underneath the character entered but gen 4690 says under the next character to be entered...
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The button is displayed pressed and immedately re-displayed enabled." + Environment.NewLine +
                                @"2. The ‘Click’ sound is played once." + Environment.NewLine +
                                "3. The RBC Phone number data input field displays the value of the button pressed." + Environment.NewLine +
                                "4. A cursor is displayed after the character just added to the data input field.");

            DmiActions.ShowInstruction(this, @"Release the pressed button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The button is displayed enabled.");

            // Repeat for the 6 button
            DmiActions.ShowInstruction(this, @"Press and hold the ‘6’ button");

            // Spec says cursor is underneath the character entered but gen 4690 says under the next character to be entered...
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The button is displayed pressed and immedately re-displayed enabled." + Environment.NewLine +
                                @"2. The ‘Click’ sound is played once." + Environment.NewLine +
                                "3. The RBC Phone number data input field displays the value of the button pressed." + Environment.NewLine +
                                "4. A cursor is displayed after the character just added to the data input field.");

            DmiActions.ShowInstruction(this, @"Release the pressed button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The button is displayed enabled.");

            // Repeat for the 7 button
            DmiActions.ShowInstruction(this, @"Press and hold the ‘7’ button");

            // Spec says cursor is underneath the character entered but gen 4690 says under the next character to be entered...
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The button is displayed pressed and immedately re-displayed enabled." + Environment.NewLine +
                                @"2. The ‘Click’ sound is played once." + Environment.NewLine +
                                "3. The RBC Phone number data input field displays the value of the button pressed." + Environment.NewLine +
                                "4. A cursor is displayed after the character just added to the data input field.");

            DmiActions.ShowInstruction(this, @"Release the pressed button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The button is displayed enabled.");

            // Repeat for the 8 button
            DmiActions.ShowInstruction(this, @"Press and hold the ‘8’ button");

            // Spec says cursor is underneath the character entered but gen 4690 says under the next character to be entered...
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The button is displayed pressed and immedately re-displayed enabled." + Environment.NewLine +
                                @"2. The ‘Click’ sound is played once." + Environment.NewLine +
                                "3. The RBC Phone number data input field displays the value of the button pressed." + Environment.NewLine +
                                "4. A cursor is displayed after the character just added to the data input field.");

            DmiActions.ShowInstruction(this, @"Release the pressed button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The button is displayed enabled.");

            // Repeat for the 9 button
            DmiActions.ShowInstruction(this, @"Press and hold the ‘9’ button");

            // Spec says cursor is underneath the character entered but gen 4690 says under the next character to be entered...
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The button is displayed pressed and immedately re-displayed enabled." + Environment.NewLine +
                                @"2. The ‘Click’ sound is played once." + Environment.NewLine +
                                "3. The RBC Phone number data input field displays the value of the button pressed." + Environment.NewLine +
                                "4. A cursor is displayed after the character just added to the data input field.");

            DmiActions.ShowInstruction(this, @"Release the pressed button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The button is displayed enabled.");
            DmiActions.ShowInstruction(this, @"Press and hold the ‘Del’ button. Note: Stopwatch is required");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The button is displayed pressed and immedately re-displayed enabled." + Environment.NewLine +
                                @"2. The ‘Click’ sound is played once." + Environment.NewLine +
                                "3. The last character in the data input field is deleted." +
                                "4. After the button has been pressed for 1.5s, the button changes repeatedly between pressed and enabled." + Environment.NewLine +
                                "5. The ‘Click’ sound is played repeatedly." + Environment.NewLine +
                                "6. The last character in the data input field is deleted repeatedly.");

            DmiActions.ShowInstruction(this, @"Release ‘Del’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Characters in the data input field are not deleted.");

            /*
            Test Step 13
            Action: Enter the data value with 5 characters
            Expected Result: Verify the following information,The 5 characters are added on an input field as one group. (e.g. ‘10000’)
            Test Step Comment: (1) MMI_gen 8053 (partly: MMI_gen 4694 (partly: NEGATIVE, 6th character));
            */
            DmiActions.ShowInstruction(this, @"Enter the data value ‘10000’");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The data input field displays ‘10000’, with no spaces between the digits.");

            /*
            Test Step 14
            Action: Continue to enter the 6th character
            Expected Result: Verify the following information,The fifth character is shown after a gap of fourth character, separated as 2 groups (e.g. 1000 00)
            Test Step Comment: (1) MMI_gen 8053 (partly: MMI_gen 4694 (partly: MMI_gen 4246));
            */
            DmiActions.ShowInstruction(this, @"Press the ‘0’ key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The data input field displays ‘1000 00’, with a space between ‘1000’ and ‘00’.");

            /*
            Test Step 15
            Action: Continue to enter the new value more than 8 characters
            Expected Result: Verify the following information,The data value is separated as 2 lines. In each line is displayed only 8 characters
            Test Step Comment: (1) MMI_gen 8053 (partly: MMI_gen 4694 (partly: MMI_gen 4247));
            */
            DmiActions.ShowInstruction(this, @"Press the ‘0’ key twice, then press the ‘1’ key twice");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The data input field displays the value on two lines: " + Environment.NewLine +
                                "‘10000000’" + Environment.NewLine +
                                "‘11’" + Environment.NewLine +
                                "   a total of eight characters on line 1 (with no space between ‘1000’ and ‘0000’.");

            /*
            Test Step 16
            Action: Delete the old value and enter the new value ‘0031840880100’ for RBC Phone number.Then, confirm an entered data by pressing an input field
            Expected Result: Verify the following information,Input fieldsThe associated ‘Enter’ button is data field itself.An input field is used to allow the driver to enter data.The state of ‘RBC Phone number’ input field is changed to ‘accepted’ as follows,The background colour of the Data Area is dark-grey.The colour of data value is white.There is no input field selected.Data Entry windowThe state of ‘Yes’ button below text label ‘Train data Entry is complete?’ is enabled as follows,The background colour of the Data Area is medium-grey.The colour of data value is black.The border colour is medium-grey.Packet transmissionUse the log file to confirm that DMI sent out packet [MMI_NEW_RBC_DATA (EVC-112)] with following variables,MMI_N_DATA_ELEMENTS = 1MMI_NID_DATA = 5 (RBC Phone number)MMI_M_BUTTONS = 254MMI_NID_RADIO = 0x0031840880100FFFUse the log file to confirm that DMI sent out packet [MMI_CURRENT_RBC_DATA (EVC-22)] with variable MMI_M_BUTTONS = 36
            Test Step Comment: (1) MMI_gen 8053 (partly: MMI_gen 4682 (partly: RBC Phone number));(2) MMI_gen 8053 (partly: MMI_gen 4634 (partly: RBC Phone number));(3) MMI_gen 8053 (partly: MMI_gen 4652 (partly: RBC Phone number), MMI_gen 4684 (partly: accepted, RBC Phone number), MMI_gen 4681 (partly: RBC Phone number));(4) MMI_gen 8053 (partly: MMI_gen 4684 (partly: No next input field, data entry process terminated));(5) MMI_gen 8053 (partly: MMI_gen 4909 (partly: Enabled), MMI_gen 4910 (partly: Enabled, MMI_gen 4211 (partly: colour))); MMI_gen 4374;(6) MMI_gen 9458 (partly: [Enter] EVC-112, RBC Phone number, touch screen, MMI_gen 1623, MMI_gen 1625); MMI_gen 1623; MMI_gen 1625 (partly: data field is not empty);(7) MMI_gen 9468;
            */
            DmiActions.ShowInstruction(this, @"Delete the old value and enter ‘0031840880100’ for RBC Phone number, then confirm by pressing in the data input field");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The data input field acts as an ‘Enter’ button." + Environment.NewLine +
                                "2. Data can be entered in the data input field" + Environment.NewLine +
                                "3. The data input field is displayed with the value in white on a Dark-grey background." + Environment.NewLine +
                                "4. The next data input field is displayed ‘Selected’ with the value in black on a Medium-grey background.");

            /*
            Test Step 17
            Action: Perform the following procedure,Select ‘RBC ID’ input field.Enter new value for ‘RBC ID’.Select ‘RBC Phone number’ input field
            Expected Result: Verify the following information,The state of ‘Yes’ button below text label ‘RBC data entry is complete?’ is disabled. The state of input field ‘RBC ID’ is changed to ‘Not selected’ as follows,The value of ‘RBC ID’ input field is removed, display as blank.The background colour of the input field is dark-grey
            Test Step Comment: (1) MMI_gen 8053 (partly: MMI_gen 4909 (partly: state selected and with recently entered key), MMI_gen 4680 (partly: value has been modified));(2) MMI_gen 8053 (partly: MMI_gen 4680 (partly: RBC ID, Not selected, Data area is blank), MMI_gen 4649 (partly: data entry, background colour));
            */
            DmiActions.ShowInstruction(this, @"Select the ‘RBC ID’ data input field and enter a new value for ‘RBC ID’, then select the ‘RBC Phone number’ data input field");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Yes’ button below the label label ‘RBC data entry is complete?’ is displayed disabled." + Environment.NewLine +
                                "2. The RBC ID data input field is displayed ‘Not Selected’, with a blank value on a Dark-grey background.");

            /*
            Test Step 18
            Action: Confirm the value of ‘RBC Phone number’
            Expected Result: Verify the following information,The state of input field ‘RBC ID’ is changed to ‘Selected’
            Test Step Comment: (1) MMI_gen 8053 (partly: MMI_gen 4685);
            */
            DmiActions.ShowInstruction(this, @"Confirm the ‘RBC Phone number’ value");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The RBC ID data input field is displayed ‘Selected’.");

            /*
            Test Step 19
            Action: Enter the new value ‘6996969’ for RBC ID.Then, press and hold ‘Yes’ button
            Expected Result: Verify the following information,The state of button is changed to ‘Pressed’, the border of button is removed.The sound ‘Click’ is played once
            Test Step Comment: (1) MMI_gen 8053 (partly: MMI_gen 4911 (partly: MMI_gen 4381 (partly: change to state ‘Pressed’ as long as remain actuated)); MMI_gen 4375;(2) MMI_gen 8053 (partly: MMI_gen 4911 (partly: MMI_gen 4381 (partly: sound ‘Click’))); MMI_gen 9512; MMI_gen 968;
            */
            DmiActions.ShowInstruction(this, @"Enter the value ‘6996969’ for RBC ID, then, press and hold the ‘Yes’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The button is displayed pressed, without a border." + Environment.NewLine +
                                @"2. The ‘Click’ sound is played once.");

            /*
            Test Step 20
            Action: Slide out the ‘Yes’ button
            Expected Result: Verify the following information,The border of the button is shown (state ‘Enabled’) without a sound
            Test Step Comment: (1) MMI_gen 8053 (partly: MMI_gen 4911 (partly: MMI_gen 4382 (partly: state ‘Enabled’ when slide out with force applied, no sound))); MMI_gen 4374;
            */
            DmiActions.ShowInstruction(this, "Whilst keeping the ‘Yes’ button pressed, drag it out of its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Yes’ button is displayed enabled, with a border." + Environment.NewLine +
                                "2. No sound is played.");

            /*
            Test Step 21
            Action: Slide back into the ‘Yes’ button
            Expected Result: Verify the following information,The button is back to state ‘Pressed’ without a sound
            Test Step Comment: (1) MMI_gen 8053 (partly: MMI_gen 4911 (partly: MMI_gen 4382 (partly: state ‘Pressed’ when slide back, no sound))); MMI_gen 4375;
            */
            DmiActions.ShowInstruction(this, "Whilst keeping the ‘Yes’ button pressed, drag it back inside its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Yes’ button is displayed pressed." + Environment.NewLine +
                                "2. No sound is played.");

            /*
            Test Step 22
            Action: Release ‘Yes’ button
            Expected Result: Verify the following information,DMI displays Main window.Use the log file to confirm that DMI sent out packet [MMI_NEW_RBC_DATA (EVC-112)] with following variables,MMI_N_DATA_ELEMENTS = 0MMI_M_BUTTONS = 36MMI_NID_RBC = 6996969MMI_NID_RADIO = 0x0031840880100FFF
            Test Step Comment: (1) MMI_gen 8053 (partly: MMI_gen 4911 (partly: MMI_gen 4381 (partly: exit state ‘pressed’), MMI_gen 4384 (partly: ETCS-MMI’s function associated to the button))); (2) MMI_gen 9472 (partly: [Yes] EVC-112, MMI_gen 1623); 
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Release the ‘Yes’ button");

            EVC112_MMINewRbcData.MMI_NID_DATA = new List<byte> { 4 };
            EVC112_MMINewRbcData.MMI_M_BUTTONS = Variables.MMI_M_BUTTONS_RBC_DATA.BTN_YES_DATA_ENTRY_COMPLETE;
            EVC112_MMINewRbcData.MMI_NID_RBC = 6996969;
            EVC112_MMINewRbcData.MMI_NID_RADIO = 0x0031840880100FFF;
            EVC112_MMINewRbcData.CheckPacketContent();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Main window.");

            /*
            Test Step 23
            Action: Perform the following procedure,Select and confirm Level 2.Press ‘Enter RBC data’ button
            Expected Result: DMI displays RBC data window
            */
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = 1;
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.EnterRBCData;
            EVC30_MMIRequestEnable.Send();

            DmiActions.ShowInstruction(this, @"Press the ‘Enter RBC data’ button");

            EVC22_MMICurrentRBC.MMI_NID_WINDOW = 10;
            EVC22_MMICurrentRBC.NID_RBC = 6996969;
            EVC22_MMICurrentRBC.MMI_NID_RADIO = 0x0031840880100FFF;
            EVC22_MMICurrentRBC.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the RBC data window.");

            /*
            Test Step 24
            Action: Confirm the current data without re-entry RBC ID and RBC Phone number.Then, press ‘Yes’ button
            Expected Result: Verify the following information,An input field is used to revalidation RBC ID and RBC Phone number, DMI displays Main window
            Test Step Comment: (1) MMI_gen 8055 (partly: revalidation); MMI_gen 8057 (partly: revalidation);
            */
            DmiActions.ShowInstruction(this, "Confirm the data displayed without re-entering RBC ID or RBC Phone number, by pressing on their data input fields, then, press the ‘Yes’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The data input fields can be used to re-validate the data values" + Environment.NewLine +
                                "2. DMI displays the Main window.");

            /*
            Test Step 25
            Action: Perform the following procedure,Select and confirm Level 2.Press ‘Enter RBC data’ button
            Expected Result: DMI displays RBC data window
            */
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = 1;
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.EnterRBCData;
            EVC30_MMIRequestEnable.Send();

            DmiActions.ShowInstruction(this, @"Press the ‘Enter RBC data’ button");

            EVC22_MMICurrentRBC.MMI_NID_WINDOW = 10;
            EVC22_MMICurrentRBC.NID_RBC = 6996969;
            EVC22_MMICurrentRBC.MMI_NID_RADIO = 0x0031840880100FFF;
            EVC22_MMICurrentRBC.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the RBC data window.");

            /*
            Test Step 26
            Action: Press and hold the Label area of ‘RBC Phone number’ input field
            Expected Result: Verify the following information,The state of ‘RBC Phone number’ input field is changed to ‘Pressed’, the border of button is removed.The state of ‘RBC Phone number’ input field remains ‘not selected’. The state of ‘RBC ID’ input field remains ‘selected’.The sound ‘Click’ is played once
            Test Step Comment: (1) MMI_gen 8053 (partly: MMI_gen 4686 (partly: Label area, RBC Phone number), MMI_gen 4381 (partly: change to state ‘Pressed’ as long as remain actuated))); MMI_gen 4392 (partly: [Enter], touch screen); MMI_gen 4375;(2) MMI_gen 8053 (partly: MMI_gen 4686 (partly: Label area, RBC Phone number), MMI_gen 4381 (partly: the sound for Up-Type button));
            */
            DmiActions.ShowInstruction(this, @"Press and hold the label part of the ‘RBC Phone number’ data input field");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘RBC Phone number’ data input field is displayed pressed, without a border." + Environment.NewLine +
                                "2. The ‘RBC Phone number’ data input field stays ‘Not Selected’." + Environment.NewLine +
                                "3. The ‘Click’ sound is played once.");

            /*
            Test Step 27
            Action: Slide out the Label area of ‘RBC Phone number’ input field
            Expected Result: Verify the following information,The border of ‘RBC Phone number’ input field is shown (state ‘Enabled’) without a sound.The state of ‘RBC Phone number’ input field remains ‘not selected’. The state of ‘RBC ID’ input field remains ‘selected’
            Test Step Comment: (1) MMI_gen 8053 (partly: MMI_gen 4686 (partly: Label area, RBC Phone number), MMI_gen 4382 (partly: state ‘Enabled’ when slide out with force applied, no sound); MMI_gen 4374;
            */
            DmiActions.ShowInstruction(this, "Whilst keeping the ‘RBC Phone number’ data input field pressed, drag it out of its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘RBC Phone number’ data input field is displayed enabled, with a border." + Environment.NewLine +
                                "2. The ‘RBC Phone number’ data input field stays ‘Not Selected’." + Environment.NewLine +
                                "3. The ‘RBC ID’ data input field stays ‘Selected’." + Environment.NewLine +
                                "4. No sound is played.");

            /*
            Test Step 28
            Action: Slide back into the Label area of ‘RBC Phone number’ input field
            Expected Result: Verify the following information,The state of ‘RBC Phone number’ input field is changed to ‘Pressed’, the border of button is removed.The state of ‘RBC Phone number’ input field remains ‘not selected’. The state of ‘RBC ID’ input field remains ‘selected’
            Test Step Comment: (1) MMI_gen 8053 (partly: MMI_gen 4686 (partly: Label area, RBC Phone number), MMI_gen 4382 (partly: state ‘Pressed’ when slide back, no sound); MMI_gen 4375;
            */
            DmiActions.ShowInstruction(this, "Whilst keeping the ‘RBC Phone number’ data input field pressed, drag it back inside its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘RBC Phone number’ data input field is displayed pressed, without a border." + Environment.NewLine + 
                                "2. The ‘RBC Phone number’ data input field stays ‘Not Selected’." + Environment.NewLine +
                                "3. The ‘RBC ID’ data input field stays ‘Selected’.");
            
            /*
            Test Step 29
            Action: Release the pressed area
            Expected Result: Verify the following information,The state of ‘RBC Phone number’ input field is changed to selected
            Test Step Comment: (1) MMI_gen 8053 (partly: MMI_gen 4686 (partly: Label area, RBC Phone number), MMI_gen 4381 (partly: exit state ‘Pressed’, execute function associated to the button)); MMI_gen 4374;
            */
            DmiActions.ShowInstruction(this, @"Release the pressed area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘RBC Phone number’ data input field is displayed ‘Selected’.");

            /*
            Test Step 30
            Action: Perform action step 26-29 for the Label area of RBC Data input field
            Expected Result: Verify the following information,The state of an input field is changed to ‘selected’ when release the pressed area at the Label area of input field
            Test Step Comment: (1) MMI_gen 8053 (partly: MMI_gen 4686 (partly: Label area, Up-Type button));
            */
            DmiActions.ShowInstruction(this, @"Press and hold the label part of the ‘RBC ID’ data input field");
            
            DmiActions.ShowInstruction(this, "Whilst keeping the ‘RBC ID’ data input field pressed, drag it out of its area");
            
            DmiActions.ShowInstruction(this, "Whilst keeping the ‘RBC ID’ data input field pressed, drag it back inside its area");

            DmiActions.ShowInstruction(this, @"Release the pressed area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘RBC ID’ data input field is displayed ‘Selected’.");

            /*
            Test Step 31
            Action: Perform action step 26-29 for the Data area of each input field
            Expected Result: Verify the following information,The state of an input field is changed to ‘selected’ when release the pressed area at the Data part of input field
            Test Step Comment: (1) MMI_gen 8053 (partly: MMI_gen 4686 (partly: Data area, Up-Type button)); MMI_gen 9390 (partly: RBC Data window);
            */
            DmiActions.ShowInstruction(this, @"Press and hold the data part of the ‘RBC Phone number’ data input field");

            DmiActions.ShowInstruction(this, "Whilst keeping the ‘RBC Phone number’ data input field pressed, drag it out of its area");

            DmiActions.ShowInstruction(this, "Whilst keeping the ‘RBC Phone number’ data input field pressed, drag it back inside its area");

            DmiActions.ShowInstruction(this, @"Release the pressed area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘RBC Phone number’ data input field is displayed ‘Selected’.");

            // Repeat for RBC ID
            DmiActions.ShowInstruction(this, @"Press and hold the data part of the ‘RBC ID’ data input field");

            DmiActions.ShowInstruction(this, "Whilst keeping the ‘RBC ID’ data input field pressed, drag it out of its area");

            DmiActions.ShowInstruction(this, "Whilst keeping the ‘RBC ID’ data input field pressed, drag it back inside its area");

            DmiActions.ShowInstruction(this, @"Release the pressed area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘RBC ID’ data input field is displayed ‘Selected’.");
            /*
            Test Step 32
            Action: Use the test script file 22_8_1_1_a.xml to send EVC-22
            Expected Result: Verify the following information,The value of input fields for RBC ID and RBC Phone number are changed refer to received packet as follows,RBC ID = 1234RBC Phone number = 1234
            Test Step Comment: (1) MMI_gen 1628; MMI_gen 1616 (partly: [0-9]); MMI_gen 1620 (partly: displayed);
            */
            XML_22_8_1_1(msgType.typea);

            // Spec says phone number and ID are 1234, surely what was saved before would be expected?
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘RBC Phone number’ data input field displays ‘0031840880100’." + Environment.NewLine +
                                "2. The ‘RBC ID’ data input field displays ‘6996969’.");

            /*
            Test Step 33
            Action: Use the test script file 22_8_1_1_b.xml to send EVC-22,MMI_NID_WINDOW = 10MMI_N_DATA_ELEMENTS = 0MMI_NID_RADIO = 0xFFFFFFFFFFFFFFFF
            Expected Result: Verify the following information,The value of input fields for RBC Phone number are changed to empty refer to received packet
            Test Step Comment: (1) MMI_gen 1620 (partly: empty, unknown); MMI_gem 1616 (partly: only contains F);
            */
            XML_22_8_1_1(msgType.typeb);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘RBC Phone number’ data input field is blank.");

            /*
            Test Step 34
            Action: Use the test script file 22_8_1_1_c.xml to send EVC-22,MMI_NID_WINDOW = 10MMI_N_DATA_ELEMENTS = 0MMI_NID_RADIO = 0xABCDEFFFFFFFFFFF
            Expected Result: erify the following information,The value of input fields for RBC Phone number are changed to empty refer to received packet
            Test Step Comment: (1) MMI_gen 1620 (partly: empty, unused);
            */
            XML_22_8_1_1(msgType.typec);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘RBC Phone number’ data input field is blank.");

            /*
            Test Step 35
            Action: Confirm blank value of RBC Phone number by pressing an input field
            Expected Result: Verify the following information,Use the log file to confirm that DMI sent out packet [MMI_NEW_RBC_DATA (EVC-112)] with following variables,MMI_NID_DATA = 5 (RBC Phone number)MMI_NID_RADIO = 0xFFFFFFFFFFFFFFFF
            Test Step Comment: (1) MMI_gen 9458 (partly: MMI_gen 1625); MMI_gen 1625 (partly: unknown);
            */
            DmiActions.ShowInstruction(this, @"Confirm the (blank) value of RBC Phone number by pressing its data input field");
            
            EVC112_MMINewRbcData.MMI_NID_DATA = new List<byte> { 5 };
            EVC112_MMINewRbcData.MMI_NID_RADIO = 0xffffffffffffffff;
            EVC112_MMINewRbcData.CheckPacketContent();

            /*
            Test Step 36
            Action: Use the test script file 22_8_1_1_d.xml to send EVC-22,MMI_NID_WINDOW = 10MMI_N_DATA_ELEMENTS = 2MMI_NID_RADIO = 0x5678EFFFFFFFFFFFMMI_NID_RBC = 5678MMI_NID_DATA[0] = 4MMI_NID_DATA[1] = 5MMI_N_TEXT[0] = 1MMI_X_TEXT[0] = 48MMI_N_TEXT[1] = 1MMI_X_TEXT[1] = 48
            Expected Result: Verify the following information,(1)   The result of variable MMI_N_DATA_ELEMENTS and MMI_Q_DATA_CHECK are ignored, the value of each input field is changed refer to the value of variable MMI_NID_RBC and MMI_NID_RADIO as follows,  RBC ID = 5678  RBC Phone number = 5678
            Test Step Comment: (1) MMI_gen 9454;
            */
            XML_22_8_1_1(msgType.typed);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘RBC Phone number’ data input field displays ‘5678’." + Environment.NewLine +
                                "2. The ‘RBC ID’ data input field displays ‘5678’.");

            /*
            Test Step 37
            Action: Press ‘Close’ button
            Expected Result: Verify the following information,Use the log file to confirm that DMI sent out packet [MMI_DRIVER_REQUEST (EVC-101)] with variable MMI_M_REQUEST = 33 (Exit RBC Data Entry).DMI displays RBC contact window
            Test Step Comment: (1) MMI_gen 9455;(2) MMI_gen 4392 (partly: returning to the parent window); 
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press ‘Close’ button");

            EVC101_MMIDriverRequest.CheckMRequestReleased = Variables.MMI_M_REQUEST.ExitRBCdataEntry;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the RBC Contact window");
            /*
            Test Step 38
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }

        #region Send_XML_8_1_1_DMI_Test_Specification
        enum msgType
        {
            typea,
            typeb,
            typec,
            typed
        }

        private void XML_22_8_1_1(msgType type)
        {
            EVC22_MMICurrentRBC.MMI_NID_WINDOW = 10;
            EVC22_MMICurrentRBC.MMI_Q_CLOSE_ENABLE = Variables.MMI_Q_CLOSE_ENABLE.Enabled;
            EVC22_MMICurrentRBC.MMI_M_BUTTONS = EVC22_MMICurrentRBC.EVC22BUTTONS.NoButton;
            switch (type)
            {
                case msgType.typea:
                    EVC22_MMICurrentRBC.NID_RBC = 1234;
                    EVC22_MMICurrentRBC.MMI_NID_RADIO = 0x1234ffffffffffff;
                    break;
                case msgType.typeb:
                    EVC22_MMICurrentRBC.NID_RBC = 1234;
                    EVC22_MMICurrentRBC.MMI_NID_RADIO = 0xffffffffffffffff;
                    break;
                case msgType.typec:
                    EVC22_MMICurrentRBC.NID_RBC = 1234;
                    EVC22_MMICurrentRBC.MMI_NID_RADIO = 0xabcdefffffffffff;
                    break;
                case msgType.typed:
                    EVC22_MMICurrentRBC.NID_RBC = 5648;
                    // spec says 5678ef....
                    EVC22_MMICurrentRBC.MMI_NID_RADIO = 0x5678ffffffffffff;
                    EVC22_MMICurrentRBC.DataElements = new List<DataElement> { new DataElement { Identifier = 0, QDataCheck = 4, EchoText = "\0x1\0x30" },
                        new DataElement { Identifier = 1, QDataCheck = 5, EchoText = "\0x1\0x30"} };
                    break;
            }
            EVC22_MMICurrentRBC.Send();
        }
        #endregion


    }
}
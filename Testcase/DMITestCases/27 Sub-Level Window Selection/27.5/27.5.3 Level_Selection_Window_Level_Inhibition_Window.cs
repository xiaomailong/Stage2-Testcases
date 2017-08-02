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
    /// 27.5.3 Level Selection Window: Level Inhibition Window
    /// TC-ID: 22.5.3
    /// 
    /// This test case verifies the general appearance of Level inhibition window. The Level inhibition window shall comply with [ERA-ERTMS] standard and [MMI-ETCS-gen].
    /// 
    /// Tested Requirements:
    /// MMI_gen 2201 (partly: MMI_gen 4381, MMI_gen 4382); MMI_gen 2207; MMI_gen 2215; MMI_gen 2216; MMI_gen 2217; MMI_gen 2218; MMI_gen 2219; MMI_gen 2278; MMI_gen 2199; MMI_gen 2254; MMI_gen 2258; MMI_gen 2259; MMI_gen 1784 (partly: MMI_M_INHIBIT_ENABLE = 1, MMI_M_CURRENT_LEVEL =1, Up-type, MMI_gen 4381, MMI_gen 4382, NEGATIVE for input field data area is blank); MMI_gen 1871 (partly: single input field, only data part, half grid array, MMI_gen 5189 (partly: touch screen), MMI_gen 5944 (partly: touch screen), MMI_gen 4640 (partly: only data area), MMI_gen 4720, MMI_gen 4889 (partly: merge label and data), MMI_gen 4722 (partly: Table 12 <Close> button, Window title , Input field), MMI_gen 4637 (partly: Main-areas D and F), note under the MMI_gen 9412, MMI_gen 4679, MMI_gen 4913 (partly:  MMI_gen 4384), MMI_gen 4634, MMI_gen 4651, MMI_gen 4647 (partly: aligned left), MMI_gen 4681); MMI_gen 1979 (partly: table 54, MMI_M_INHIBITED_LEVEL); MMI_gen 8864 (partly: Level inhibition window);
    /// 
    /// Scenario:
    /// Enter Driver ID and perform brake test. Then, verify the state of ‘L inh’ button in the Level window.Use the ATP config editor to set the value of parameter. Then, open Level window to verify the state of ‘L inh’ button and text colour of each button in Level window.Perform the following actions to verify type of ‘L inh’ button.Press the button and holdSlide the button out with force appliedSlide the button back with force appliedRelease the buttonVerify the display of Level inhibition window.Select and confirm Level 
    /// 2.Then, verify the updated information in Level window.Press ‘Level 2’ button again to verify that inhibited level is disabled.Use the test script file to send EVC-
    /// 20.Then, verify that Level inhibition window is closed.Open Level inhibition window. Then, perform the following actions to verify type of ‘Close’ button.Press the button and holdSlide the button out with force appliedSlide the button back with force appliedRelease the buttonOpen Level inhibition window. Use the test script file to send EVC-
    /// 20.Then, verify the state of ‘Close’ button.
    /// 
    /// Used files:
    /// 22_5_3_a.xml, 22_5_3_b.xml
    /// </summary>
    public class Level_Selection_Window_Level_Inhibition_Window : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // Use the ATP config editor to set the following parameters as follows,M_DefaultLevels = 15M_InstalledLevels = 15InhibitEnable_1 = 16InhibitEnable_2 = 16InhibitEnable_3 = 16InhibitEnable_4 = 16InhibitEnable_5 = 16Test system is power on.Cabin is activated.

            // Call the TestCaseBase PreExecution
            base.PreExecution();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in SB mode.

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint


            /*
            Test Step 1
            Action: Enter Driver ID and perform brake test
            Expected Result: Verify the following information,The state of ‘L inh’ button in sub-area G13 is disabled.  Use the log file to confirm that DMI received packet information EVC-20 with every index of variable MMI_M_INHIBIT_ENABLE = 0 (not allowed for inhibiting)
            Test Step Comment: (1) MMI_gen 1784 (partly: NEGATIVE, condition is not fulfilled);(2) MMI_gen 1784 (partly: NEGATIVE, 1st bullet);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Enter Driver ID and perform brake test");


            /*
            Test Step 2
            Action: Perform the following procedure, Power off system.Use the ATP config editor to set the value of parameters refer to note belowPower on systemEnter Driver ID and perform brake test.Note:  InhibitEnable_1 = 1InhibitEnable_2 = 2InhibitEnable_3 = 3InhibitEnable_4 = 4
            Expected Result: DMI displays Level window.Verify the following information,The state of ‘L inh’ button in sub-area G13 is enabled.  The text colour of each button is grey.Use the log file to confirm that DMI received packet information EVC-20 with the following variables,Level 1MMI_Q_LEVEL_NTC_ID[0] = 1MMI_M_LEVEL_NTC_ID[0] = 1MMI_M_INHIBIT_ENABLE[0] = 1MMI_M_INHIBITED_LEVEL[0] =0 Level 2MMI_Q_LEVEL_NTC_ID[1] = 1MMI_M_LEVEL_NTC_ID[1] = 2MMI_M_INHIBIT_ENABLE[1] = 1MMI_M_INHIBITED_LEVEL[1] =0 Level 3MMI_Q_LEVEL_NTC_ID[2] = 1MMI_M_LEVEL_NTC_ID[2] = 3MMI_M_INHIBIT_ENABLE[2] = 1MMI_M_INHIBITED_LEVEL[2] =0 Level 0MMI_Q_LEVEL_NTC_ID[3] = 1MMI_M_LEVEL_NTC_ID[3] = 0MMI_M_INHIBIT_ENABLE[3] = 1MMI_M_INHIBITED_LEVEL[3] =0 Note: The first index of parameter is the topmost position in packet EVC-20
            Test Step Comment: (1) MMI_gen 1784 (partly: enabled);(2) MMI_gen 2217 (partly: Level window, grey); MMI_gen 1979 (partly: table 54, grey);(3) MMI_gen 1784 (partly: 1st bullet);                  MMI_gen 2216 (partly: EVC-20);                                 MMI_gen 2219 (partly: EVC-20);                                   MMI_gen 2218 (partly: EVC-20); MMI_gen 2217 (partly: MMI_gen 2216);
            */


            /*
            Test Step 3
            Action: Press and hold ‘L inh’ button
            Expected Result: The ‘Inhibit/Enable’ button is shown as pressed state
            Test Step Comment: MMI_gen 1784 (partly: MMI_gen 4381 (partly: change to state ‘Pressed’ as long as remain actuated));
            */


            /*
            Test Step 4
            Action: Slide out ‘L inh’ button
            Expected Result: The ‘Inhibit/Enable’ button becomes the ‘Enabled’ state without a sound
            Test Step Comment: MMI_gen 1784 (partly: MMI_gen 4382 (partly: state ‘Enabled’ when slide out with force applied, no sound));
            */


            /*
            Test Step 5
            Action: Slide back into ‘L inh’ button
            Expected Result: The ‘Inhibit/Enable’ button is shown as pressed state and no sound ‘Click’ is played
            Test Step Comment: MMI_gen 1784 (partly: MMI_gen 4382 (partly: state ‘Pressed’ when slide back, no sound));
            */


            /*
            Test Step 6
            Action: Release ‘L inh’ button
            Expected Result: Verify the following information,DMI displays Level inhibition window.LayersThe level of layers in each area of window as follows,Layer 0: Main-Area D, F, G, Y and Z.Layer -1: A1, A2+A3*, A4, B*, C1, C2+C3+C4*, C5, C6, C7, C8, C9, E1, E2, E3, E4, E5-E9*Layer -2: B3, B4, B5, B6, B7Note: ‘*’ symbol is mean that specified areas are drawn as one area.Data Entry windowThe window title is displayed with text ‘Level inhibition’.Verify that the Level window is displayed in main area D, F and G as half-grid array.A data entry window is containing only one input field covers the Main area D, F and GThe following objects are displayed in Level window.Close buttonWindow TitleInput FieldThe 4buttons are displayed in Level Inhibition window,Level 0Level 1Level 2Level 3The ‘Close’ button of Level inhibition window is enabled.Input fieldThe input field is located in main area D and F.For a single input field, the window title is clearly explaining the topic of the input field. The Level inhibition window is contained a single input field with only the data part.The data area of the input field remains empty.KeyboardThe colour of text label in each button is grey
            Test Step Comment: (1) MMI_gen 1784 (partly: MMI_gen 4381 (partly: exit state ‘Pressed’, execute function associated to the button)); MMI_gen 2207;(2) MMI_gen 1871 (partly: MMI_gen 5189 (partly: touch screen), MMI_gen 5944 (partly: touch screen)));(3) MMI_gen 2215;(4) MMI_gen 1871 (partly: half grid array);(5) MMI_gen 1871 (partly: MMI_gen 4640 (partly: only data area), MMI_gen 4720, MMI_gen 4889 (partly: merge label and data));(6) MMI_gen 1871 (party: MMI_gen 4722 (partly: Table 12 <Close> button, Window title , Input field));(7) MMI_gen 2218 (partly: Selection list);                         MMI_gen 2217 (partly: enabled buttons); MMI_gen 2216 (partly: selection list);(8) MMI_gen 2254;(9) MMI_gen 1871 (partly: MMI_gen 4637 (partly: Main-areas D and F));(10) MMI_gen 1871 (partly: note under the MMI_gen 9412);(11) MMI_gen 1871 (partly: single input field, only data part);(12) MMI_gen 2219 (partly: No level with MMI_M_INHIBITED_LEVEL =1);(13) MMI_gen 2217 (partly: Level inhibition window, grey colour);
            */


            /*
            Test Step 7
            Action: Press and hold ‘Level2’ button
            Expected Result: Verify the following information,(1)    The value of input field is replaced by the pressed button.(2)    Sound ‘Click’ is played once.(3)    The state of button is changed to ‘Pressed’ and immediately back to ‘Enabled’ state.(4)    The Input Field displays the level associated to the data key according to the pressings in state ‘Pressed’.(5)    An input field is used to enter the level inhibition.(6)    The data value is displayed as black colour and the background of the data area is displayed as medium-grey colour.(7)    The data value of the input field is aligned to the left of the data area
            Test Step Comment: (1) MMI_gen 1871 (partly: MMI_gen 4679);(2) MMI_gen 1871 (partly: MMI_gen 4913 (partly: MMI_gen 4384 (partly: sound ‘Click’));(3) MMI_gen 1871 (partly: MMI_gen 4913 (partly: MMI_gen 4384 (partly: Change to state ‘Pressed’ and immediately back to state ‘Enabled’));   (4) MMI_gen 1871 (partly: MMI_gen 4913); (5) MMI_gen 1871 (partly: MMI_gen 4634); (6) MMI_gen 1871 (partly: MMI_gen 4651);(7) MMI_gen 1871 (partly: MMI_gen 4647 (partly: aligned left));  
            */


            /*
            Test Step 8
            Action: Release the pressed button.Note: Please verify the state of all Level buttons refer to action step No.7-8
            Expected Result: Verify the following information,(1)    The state of pressed button is changed to ‘Enabled’ state
            Test Step Comment: (1) MMI_gen 1871 (partly: MMI_gen 4913 (partly: MMI_gen 4384 (partly: ETCS-MMI’s function associated to the button)));
            */


            /*
            Test Step 9
            Action: Select and confirm ‘Level 2’.Then, acknowledge the change of inhibit level
            Expected Result: Verify the following information,DMI closes the Level Inhibition window and displays Level window.The text colour of ‘Level 2’ button is changed to orange.Use the log file to confirm that DMI send out EVC-121 with the following variables,Level 1MMI_Q_LEVEL_NTC_ID[0] = 1MMI_M_LEVEL_NTC_ID[0] = 1MMI_M_INHIBITED_LEVEL[0] = 0MMI_M_LEVEL_FLAG[0] = 1Level 2MMI_Q_LEVEL_NTC_ID[1] = 1MMI_M_LEVEL_NTC_ID[1] = 2MMI_M_INHIBITED_LEVEL[1] = 1MMI_M_LEVEL_FLAG[1] = 0Level 3MMI_Q_LEVEL_NTC_ID[2] = 1MMI_M_LEVEL_NTC_ID[2] = 3MMI_M_INHIBITED_LEVEL[2] = 0MMI_M_LEVEL_FLAG[2] = 1Level 0MMI_Q_LEVEL_NTC_ID[3] = 1MMI_M_LEVEL_NTC_ID[3] = 0MMI_M_INHIBITED_LEVEL[3] = 0MMI_M_LEVEL_FLAG[3] = 1Note: The first index of parameter is the topmost position in packet EVC-121.Use the log file to confirm that DMI received packet EVC-20 which all index of variable MMI_M_INHIBITED_LEVEL are same as packet EVC-121 from expected result No.3
            Test Step Comment: (1) MMI_gen 2258 (partly: 2nd bullet);(2) MMI_gen 2217 (partly: Level window, orange colour); MMI_gen 1979 (partly: table 54, orange);(3) MMI_gen 2258 (partly: 1st bullet);                    MMI_gen 2259;(4) MMI_gen 1979 (partly: MMI_M_INHIBITED_LEVEL); MMI_gen 1871 (partly: MMI_gen 4681 (partly: accept the entered value));
            */


            /*
            Test Step 10
            Action: Press ‘Level 2’ button
            Expected Result: Verify that the state of ‘Level 2’ button is not changed and the value of an input field is not changed
            Test Step Comment: MMI_gen 1979 (partly: Buttons of inhibited level always be disabled);
            */


            /*
            Test Step 11
            Action: Press ‘L inh’ button
            Expected Result: DMI displays Level inhibition window.Verify that the text colour of ‘Level 2’ button is changed to yellow.The value of an input field is ‘Level 2’ The text colour of an input field is black
            Test Step Comment: (1) MMI_gen 2217 (partly: Level inhibition window, yellow); MMI_gen 1979 (partly: table 54, yellow);(2) MMI_gen 2219 (partly lnside the data area of input field);                                (3) MMI_gen 1871 (partly: MMI_gen 4651 (partly: data value colour));
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press ‘L inh’ button");


            /*
            Test Step 12
            Action: Perform action step 9-11 for each button on keypad of Level inhibition window.Note: This procedure is include the previous ‘Leve’ in step 9 -11 which already executed
            Expected Result: Verify the following information,(1)    The text buttons in Level inhibition window are changed to yellow colour.(2)    The text buttons in Level window are changed to orange colour. DMI also updates and changes information according to the driver’s action
            Test Step Comment: (1) MMI_gen 2217 (partly: Level inhibition window, yellow); MMI_gen 1979 (partly: table 54, yellow);(2) MMI_gen 2217 (partly: Level window, orange colour); MMI_gen 1979 (partly: table 54, orange); MMI_gen 1871 (partly: MMI_gen 4681 (partly: replace the current data)); MMI_gen 8864 (partly: Level inhibition window);
            */


            /*
            Test Step 13
            Action: Use the test script file 22_5_3_a.xml to send EVC-20 with,MMI_N_LEVELS = 0
            Expected Result: Verify the following information,DMI close the Level inhibition window and displays Level window instead.Use the log file to confirm that there is no packet information (i.e. EVC-101, EVC-121) send out from DMI
            Test Step Comment: (1) MMI_gen 2278 (partly: close Level inhibition window);(2) MMI_gen 2278 (partly: No response is transmitted to onboard);
            */


            /*
            Test Step 14
            Action: Press ‘L inh’ button.Then, select ‘Level 3’ button without confirmation
            Expected Result: DMI displays Level inhibition window with the value of input field is changed refer to driver’s selection
            */


            /*
            Test Step 15
            Action: Press and hold ‘Close’ button
            Expected Result: The ‘Close’ button is shown as pressed state
            Test Step Comment: MMI_gen 2201 (partly: MMI_gen 4381 (partly: change to state ‘Pressed’ as long as remain actuated));
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press and hold ‘Close’ button");


            /*
            Test Step 16
            Action: Slide out ‘Close’ button
            Expected Result: The ‘Close’ button becomes the ‘Enabled’ state without a sound
            Test Step Comment: MMI_gen2201 (partly: MMI_gen 4382 (partly: state ‘Enabled’ when slide out with force applied, no sound));
            */
            // Call generic Action Method
            DmiActions.Slide_out_Close_button(this);


            /*
            Test Step 17
            Action: Slide back into ‘Close’ button
            Expected Result: The ‘Close’ button is shown as pressed state and no sound ‘Click’ is played
            Test Step Comment: MMI_gen 2201 (partly: MMI_gen 4382 (partly: state ‘Pressed’ when slide back, no sound));
            */
            // Call generic Action Method
            DmiActions.Slide_back_into_Close_button(this);


            /*
            Test Step 18
            Action: Release ‘Close’ button
            Expected Result: DMI close the Level inhibition window and displays Level window instead
            Test Step Comment: MMI_gen 2201 (partly: Up-type, Level window reappear);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Release ‘Close’ button");


            /*
            Test Step 19
            Action: Press ‘L inh’ button
            Expected Result: Verify the following information,(1)    DMI displays the Level inhibition window with the value of input field is different from driver’s selection on step 14
            Test Step Comment: (1) MMI_gen 2201 (partly: discarded);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press ‘L inh’ button");


            /*
            Test Step 20
            Action: Use the test script file 22_5_3_b.xml to send EVC-20 with,MMI_N_LEVELS = 2MMI_Q_LEVEL_NTC_ID[0] = 1MMI_M_CURRENT_LEVEL[0]  = 0MMI_M_LEVEL_FLAG[0] = 1MMI_M_INHIBITED_LEVEL[0] = 0MMI_M_INHIBIT_ENABLE[0] = 1MMI_M_LEVEL_NTC_ID[0] = 0MMI_Q_LEVEL_NTC_ID[1] = 1MMI_M_CURRENT_LEVEL[1]  = 0MMI_M_LEVEL_FLAG[1] = 1MMI_M_INHIBITED_LEVEL[1] = 1MMI_M_INHIBIT_ENABLE[1] = 1MMI_M_LEVEL_NTC_ID[1] = 2
            Expected Result: Verify the following information,DMI displays Level window.The state of ‘L inh’ button in sub-area G13 is disabled.  Use the log file to confirm that DMI received packet information EVC-20 with every index of variable MMI_M_CURRENT_LEVEL = 0.Use the log file to confirm that there is no packet information (i.e. EVC-101, EVC-121) send out from DMI.The value of an input field is blank refer to received packet EVC-20
            Test Step Comment: (1) MMI_gen 2199 (partly: close the 'Level Inhibition' window, Open and updated ‘Level’ window);(2) MMI_gen 1784 (partly: condition is not fulfilled);(3) MMI_gen 1784 (partly: NEGATIVE, 2nd bullet);(4) MMI_gen 2199 (partly: discard allentries of the driver);(5) MMI_gen 1784 (partly: NEGATIVE, 3rd bullet);
            */


            /*
            Test Step 21
            Action: End of test
            Expected Result: 
            */


            return GlobalTestResult;
        }
    }
}
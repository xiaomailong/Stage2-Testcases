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
    /// 27.5.1 Level Selection Window: General appearance
    /// TC-ID: 22.5.1
    /// 
    /// This test case verifies the general appearance of Level window that contains all ERTMS/ETCS levels for driver selection. The Level window shall comply with [ERA-ERTMS] standard and [MMI-ETCS-gen].
    /// 
    /// Tested Requirements:
    /// MMI_gen 1630 (partly: fulfilled condition, window opening, EVC-20); MMI_gen 7986; MMI_gen 7987; MMI_gen 7988; MMI_gen 11383; MMI_gen 2194; MMI_gen 2206; MMI_gen 2205-1 (THR) (partly: MMI_gen 11387, MMI_gen 11907 (partly: EVC-101, timestamp), MMI_gen 4381, MMI_gen 4382 , MMI_gen 9512, MMI_gen 968); MMI_gen 7985 (partly: half grid array, single input field, only data part, MMI_gen 5189 (partly: touch screen), MMI_gen 5944 (partly: touch screen), MMI_gen 4640 (partly: only data area), MMI_gen 4679,MMI_gen 4720, MMI_gen 4889 (partly: merge label and data), MMI_gen 4722 (partly: Table 12 <Close> button, Window title, Input field), MMI_gen 4637 (partly: Main-areas D and F), note under the MMI_gen 9412, MMI_gen 4912, MMI_gen 4678, MMI_gen 4913 (partly: MMI_gen 4384), MMI_gen 4634, MMI_gen 4651, MMI_gen 4682, MMI_gen 4681, MMI_gen 4647 (partly: left aligned), MMI_gen 4684 (partly: terminated)); MMI_gen 2197; MMI_gen 1979 (partly: ETCS level, MMI_M_LEVEL_FLAG, button enabling); MMI_gen 1978; MMI_gen 1972; MMI_gen 2277; MMI_gen 4392 (partly: [Close NA11]); MMI_gen 3375; MMI_gen 8864 (partly: Level window);
    /// 
    /// Scenario:
    /// The displayed information is verified after Level window is opened.The Down-type button on keypad is verified.The Safe Up-type button on input field is verified.The data entry functionality of the Train Running Number window is verified.The revlidate data entry of the Train Running Number window is verified.The window closure is verified.
    /// 
    /// Used files:
    /// 22_5_1_a.xml, 22_5_1_b.xml
    /// </summary>
    public class Level_Selection_Window_General_appearance : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // Use the ATP config editor to set the following parameters as follows (See the instruction in Appendix 2),M_InstalledLevels = 15M_DefaultLevels = 15Test system is power on.Cabin is activated.
            
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
            Action: Enter the Driver ID and perform brake test
            Expected Result: The Level window is displayed on the right half part of the window.LayersThe layers of window on half-grid array is displayed as followsLayer 0: Main-Area D, F, G, Y and Z.Layer -1: A1, A2+A3*, A4, B*, C1, C2+C3+C4*, C5, C6, C7, C8, C9, E1, E2, E3, E4, E5-E9*Layer -2: B3, B4, B5, B6, B7Note: ‘*’ symbol is mean that specified areas are drawn as one area.Data Entry windowThe window title is displayed with text ‘Level’.Verify that the Level window is displayed in main area D, F and G as half-grid array.A data entry window is containing only one input field covers the Main area D, F and GThe following objects are display in Level window. Disabled Close button (NA12)Window TitleInput FieldInput fieldThe input field is located in main area D and F.For a single input field, the window title is clearly explaining the topic of the input field. The Level window is displayed as a single input field with only the data part.The data area of the input field remains empty.KeyboardThe keyboard associated to the Level window is displayed as dedicated keyboard.The keyboard is presented below the area of input field.The data keys are displayed the with label according to each index of variable MMI_M_LEVEL_NTC_ID in received packet EVC-20 respectively. (See the position in picture below)     e.g. the first index is displayed at key #1Received Packet (EVC-20)Use the log file to confirm that DMI receives packet EVC-20 with the following variables,MMI_SELECT_LEVEL.MMI_N_LEVELS > 0 MMI_M_CURRENT_LEVEL = 0Several MMI_M_LEVEL_NTC_ID which are listed together with each level group.The value of every index of variable MMI_M_LEVEL_FLAG are equal to 1 (makred for editable).All keypad buttons are enabled refer to the value of MMI_M_LEVEL_FLAG
            Test Step Comment: (1) MMI_gen 7985 (partly: MMI_gen 5189 (partly: touch screen), MMI_gen 5944 (partly: touch screen)));(2) MMI_gen 7986;(3) MMI_gen 7985 (partly: half grid array);(4) MMI_gen 7985 (partly: MMI_gen 4640 (partly: only data area), MMI_gen 4720, MMI_gen 4889 (partly: merge label and data));(5) MMI_gen 7985 (party: MMI_gen 4722 (partly: Table 12 <Close> button, Window title , Input field)); (6) MMI_gen 7985 (partly: MMI_gen 4637 (partly: Main-areas D and F));(7) MMI_gen 7985 (partly: note under the MMI_gen 9412);(8) MMI_gen 7985 (partly: single input field, only data part);(9) MMI_gen 2194 (partly: no marked);(10) MMI_gen 7988; MMI_gen 7985 (partly: MMI_gen 4912);(11) MMI_gen 7985 (partly: MMI_gen 4678);(12) MMI_gen 1972; MMI_gen 1978;(13) MMI_gen 1630 (partly: fulfilled condition, EVC-20); (14) MMI_gen 2194 (partly: no marked, EVC-20, [GenVSIS]);(15) MMI_gen 2206 (partly: list provided by EVC-20);(16) MMI_gen 1979 (partly: ETCS Level, MMI_M_LEVEL_FLAG);(17) MMI_gen 1979 (partly: button enabling);
            */
            
            
            /*
            Test Step 2
            Action: Press and hold ‘Level 0’ button
            Expected Result: Verify the following information,The value of input field is replaced by the pressed button.Sound ‘Click’ is played once.The state of button is changed to ‘Pressed’ and immediately back to ‘Enabled’ state.The Input Field displays the level associated to the data key according to the pressings in state ‘Pressed’.An input field is used to enter the level selection.The data value is displayed as black colour and the background of the data area is displayed as medium-grey colour.The data value of the input field is aligned to the left of the data area
            Test Step Comment: (1) MMI_gen 7985 (partly: MMI_gen 4679);(2) MMI_gen 7985 (partly: MMI_gen 4913 (partly:  MMI_gen 4384 (partly: sound ‘Click’));(3) MMI_gen 7985 (partly: MMI_gen 4913 (partly:  MMI_gen 4384 (partly: Change to state ‘Pressed’ and immediately back to state ‘Enabled’));   (4) MMI_gen 7985 (partly: MMI_gen 4913); MMI_gen 2194 (partly: acts on level selection buttons); (5) MMI_gen 7987 (partly: entry);MMI_gen 7985 (partly: MMI_gen 4634); (6) MMI_gen 7985 (partly: MMI_gen 4651);(7) MMI_gen 7985 (partly: MMI_gen 4647 (partly: aligned left));  
            */
            
            
            /*
            Test Step 3
            Action: Release the pressed button.Note: Please verify the state of all Level buttons refer to action step No.2-3
            Expected Result: Verify the following information,The state of pressed button is changed to ‘Enabled’ state
            Test Step Comment: (1) MMI_gen 7985 (partly: MMI_gen 4913 (partly:  MMI_gen 4384 (partly: ETCS-MMI’s function associated to the button)));
            */
            // Call generic Check Results Method
            DmiExpectedResults.Verify_the_following_information_The_state_of_pressed_button_is_changed_to_Enabled_state();
            
            
            /*
            Test Step 4
            Action: Press ‘ Level 1’ button Then, press and hold an input field
            Expected Result: Verify the following information,Use the log file to confirm that DMI sends EVC-101 with variable MMI_M_REQUEST = 40 (Level entered), MMI_Q_BUTTON = 1 (pressed) and MMI_T_BUTTONEVENT is not blank.The sound ‘Click’ is played once
            Test Step Comment: (1) MMI_gen 2205-1 (THR) (partly: 1st bullet, MMI_gen 11387 (partly: send events of Pressed independently to ETCS),  MMI_gen 11907 (partly: EVC-101, timestamp))); MMI_gen 3375; (2) MMI_gen 2205-1 (THR) (partly: up-type button,  MMI_gen 4381 (partly: the sound for Up-Type button), MMI_gen 9512, MMI_gen 968);
            */
            
            
            /*
            Test Step 5
            Action: Slide out an input field
            Expected Result: DMI still displays Level window.No sound ‘Click’ is played
            Test Step Comment: (1) MMI_gen 2205-1 (THR) (partly: safe-up type, MMI_gen 11387 (partly: button Up-Type, MMI_gen 4382 (partly: when slide out with force applied, no sound)));
            */
            // Call generic Action Method
            DmiActions.Slide_out_an_input_field();
            // Call generic Check Results Method
            DmiExpectedResults.DMI_still_displays_Level_window_No_sound_Click_is_played();
            
            
            /*
            Test Step 6
            Action: Slide back into an input field
            Expected Result: DMI still displays Level window.No sound ‘Click’ is played
            Test Step Comment: (1) MMI_gen 2205-1 (THR) (partly: safe-up type, MMI_gen 11387 (partly: button Up-Type, MMI_gen 4382 (partly: when slide back, no sound)));
            */
            // Call generic Action Method
            DmiActions.Slide_back_into_an_input_field();
            // Call generic Check Results Method
            DmiExpectedResults.DMI_still_displays_Level_window_No_sound_Click_is_played();
            
            
            /*
            Test Step 7
            Action: Release the pressed area. This is to confirm Level 1
            Expected Result: Use the log file to verify the following information,Packet (EVC-101)Use the log file to confirm that DMI sends EVC-101 with variable MMI_M_REQUEST = 40 (Level entered), MMI_Q_BUTTON = 0 (released) and MMI_T_BUTTONEVENT is not blank.Packet (EVC-121)MMI_M_LEVEL_NTC_ID which is listed the same order as EVC-20 in Step 1.MMI_M_LEVEL_FLAG in each level group has the same value as the each group of variable MMI_M_CURRENT_LEVEL (EVC-20) in Step 1.Note: The previous value and current selected value are excluded.The Level window is closed and Main window is displayed instead. DMI displays in SB mode, Level 1
            Test Step Comment: (1) MMI_gen 2205-1 (THR) (partly: 1st bullet, MMI_gen 11387 (partly: send events of Released independently to ETCS)); MMI_gen 11907 (partly: EVC-101, timestamp)); MMI_gen 3375; MMI_gen 4681 (partly: accept the entered value);(2) MMI_gen 2205-1 (THR) (partly: 2nd bullet); MMI_gen 2206 (partly: EVC-121, 1st bullet);(3) MMI_gen 2206 (partly: EVC-121, 1st bullet, 3rd bullet);(4) MMI_gen 2205-1 (THR) (partly: 3rd bullet, MMI_gen 4381 (partly: exit state ‘Pressed’ execute function associated to the button);
            */
            
            
            /*
            Test Step 8
            Action: Press ‘Level’ button
            Expected Result: Verify the following information,The input field displays the previous entered value from Step 5.The enabled Close button NA11 is display in area G. Received Packet (EVC-20)Use the log file to confirm that DMI receives packet EVC-20 with the following variables,MMI_SELECT_LEVEL.MMI_N_LEVELS > 0 Several MMI_Q_LEVEL_NTC_ID with MMI_M_CURRENT_LEVEL which are listed together with each level group
            Test Step Comment: (1) MMI_gen 2194 (partly: present the first level marked); MMI_gen 2206 (partly: 2nd bullet); MMI_gen 8864 (partly: the value stored onboard);(2) MMI_gen 4392 (partly: [Close] NA11);(3) MMI_gen 1630 (partly: fulfilled condition, EVC-20);(4) MMI_gen 2206 (partly: list provided by EVC-20);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(@"Press ‘Level’ button");
            
            
            /*
            Test Step 9
            Action: Confirm the current data without re-entry by pressing the input field
            Expected Result: Verify the following information, Use the log file to confirm that DMI sends out the packet EVC-101 with variable MMI_M_REQUEST = 40 (Level entered) and MMI_Q_BUTTON = 1 and = 0 respectively
            Test Step Comment: (1) MMI_gen 7987 (partly: revalidation); MMI_gen 2205-1 (THR); MMI_gen 11450-1 (THR) (partly: safe, send events of Pressed and Released independently to ETCS), MMI_gen 11907 (partly: EVC-101, timestamp));
            */
            
            
            /*
            Test Step 10
            Action: Press ‘Level’ button.Then, repeat action Step 4-9 with every buttons on the dedicated keyboard respectively
            Expected Result: Use the log file to verify the following information,Packet EVC-101DMI sends out the packet EVC-101 with variable MMI_M_REQUEST = 40 (Level entered).Packet EVC-121DMI sends out the packet EVC-121 with the variable MMI_M_LEVEL_NTC_ID in each group is listed the same order as EVC-20 from Step 9 as follows,ETCS Level (MMI_Q_LEVEL_NTC_ID = 1)Level1: MMI_M_LEVEL_NTC_ID = 1Level2: MMI_M_LEVEL_NTC_ID = 2Level3: MMI_M_LEVEL_NTC_ID = 3Level0: MMI_M_LEVEL_NTC_ID = 0NTC Level (if any) (MMI_Q_LEVEL_NTC_ID = 0)Level PZB/LZB: MMI_M_LEVEL_NTC_ID = 9Level TPWS/AWS: MMI_M_LEVEL_NTC_ID = 20Level ATC SE/NO: MMI_M_LEVEL_NTC_ID = 22MMI_M_CURRENT_LEVEL in each level groups has the same value as EVC-20 in Step 9.Note: The previous value and current selected value are excluded.(4)   When the Level window is opened, the value of an input field is changed refer to driver entered data. DMI also updates and changes to the new level according to the entered data
            Test Step Comment: (1) MMI_gen 2205-1 (THR) (partly: EVC-101, EVC-121, all levels); MMI_gen 4681 (partly: replace the current data with the entered data value);(2) MMI_gen 2205-1 (partly: EVC-121); MMI_gen 2206 (partly: EVC-121, listed as EVC-20, all levels);(3) MMI_gen 2206 (partly: EVC-121, listed as EVC-20, states unchanged);(4) MMI_gen 7985 (partly: MMI_gen 4681); MMI_gen 8864 (partly: Level window);
            */
            
            
            /*
            Test Step 11
            Action: Perform the following procedure,Press ‘Level 2’ button.Press and hold ‘Close’ button
            Expected Result: Verify the following information,The ‘Close’ button is shown as pressed state. The sound ‘Click’ is played once
            Test Step Comment: (1) MMI_gen 11383 (partly: MMI_gen 4381 (partly: change to state ‘Pressed’ as long as remain actuated));(2) MMI_gen 11383 (partly: MMI_gen 4381 (partly: the sound for Up-Type button)); MMI_gen 9512; MMI_gen 968;
            */
            
            
            /*
            Test Step 12
            Action: Slide out ‘Close’ button
            Expected Result: DMI still displays the Level window.Verify the following information,The ‘Close’ button becomes the ‘Enabled’ state without a sound
            Test Step Comment: (1) MMI_gen 11383 (partly: MMI_gen 4382 (partly: state ‘Enabled’ when slide out with force applied, no sound));
            */
            // Call generic Action Method
            DmiActions.Slide_out_Close_button();
            
            
            /*
            Test Step 13
            Action: Slide back into ‘Close’ button
            Expected Result: DMI still displays the Level window.Verify the following information,The ‘Close’ button is shown as pressed state and no sound ‘Click’ is played
            Test Step Comment: (1) MMI_gen 11383 (partly: MMI_gen 4382 (partly: state ‘Pressed’ when slide back, no sound));
            */
            // Call generic Action Method
            DmiActions.Slide_back_into_Close_button();
            
            
            /*
            Test Step 14
            Action: Release ‘Close’ button
            Expected Result: Verify the following information,Use the log file to confirm that DMI sends out the packet [MMI_DRIVER_REQUEST (EVC-101)])  with variable MMI_M_REQUEST] = 32 (Exit Change Level).The Level window is closed, DMI displays Main window
            Test Step Comment: (1) MMI_gen 11383 (partly: EVC-101, MMI_gen 4381 (partly: exit state ‘Pressed’, execute function associated to the button));(2) MMI_gen 11383 (partly: closure);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(@"Release ‘Close’ button");
            
            
            /*
            Test Step 15
            Action: Press ‘Level’ button
            Expected Result: Verify the following information,The input field displays the previous entered value from step 10
            Test Step Comment: (1) MMI_gen 11383 (partly: discarded);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(@"Press ‘Level’ button");
            
            
            /*
            Test Step 16
            Action: Use the test script file 22_5_1_a.xml to send EVC-20 with,MMI_N_LEVELS = 1MMI_M_CURRENT_LEVEL = 1MMI_M_LEVEL_FLAG = 1MMI_M_LEVEL_NTC_ID = 3
            Expected Result: Verify the following information,(1)   The level window is updated according to the following,-      There is only one button ‘Level 3’ displayed in keypad-      The value of input field is changed to ‘Level 3’
            Test Step Comment: (1) MMI_gen 2197;
            */
            
            
            /*
            Test Step 17
            Action: Use the test script file 22_5_1_b.xml to send EVC-20 with,MMI_N_LEVELS = 0
            Expected Result: Verify the following information,DMI displays Main window
            Test Step Comment: (1) MMI_gen 1630 (partly: NEAGTIVE, 2nd  bullet); MMI_gen 2277;
            */
            // Call generic Check Results Method
            DmiExpectedResults.Verify_the_following_information_DMI_displays_Main_window();
            
            
            /*
            Test Step 18
            Action: End of test
            Expected Result: 
            */
            

            return GlobalTestResult;
        }
    }
}

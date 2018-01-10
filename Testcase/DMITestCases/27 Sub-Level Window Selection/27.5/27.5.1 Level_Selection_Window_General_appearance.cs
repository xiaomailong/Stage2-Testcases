using System;
using Testcase.Telegrams.EVCtoDMI;
using Testcase.Telegrams.DMItoEVC;
using static Testcase.Telegrams.EVCtoDMI.Variables;


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
    public class TC_22_5_1_Level_Window : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // Use the ATP config editor to set the following parameters as follows (See the instruction in Appendix 2),M_InstalledLevels = 15M_DefaultLevels = 15Test system is power on.Cabin is activated.

            // Call the TestCaseBase PreExecution
            base.PreExecution();

            DmiActions.Start_ATP();

            DmiActions.Activate_Cabin_1(this);
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
            TraceInfo("This test case requires an ATP configuration change - " +
                      "See Precondition requirements. If this is not done manually, the test may fail!");

            /*
            Test Step 1
            Action: Enter the Driver ID and perform brake test
            Expected Result: The Level window is displayed on the right half part of the window.LayersThe layers of window on half-grid array is displayed as followsLayer 0: Main-Area D, F, G, Y and Z.Layer -1: A1, A2+A3*, A4, B*, C1, C2+C3+C4*, C5, C6, C7, C8, C9, E1, E2, E3, E4, E5-E9*Layer -2: B3, B4, B5, B6, B7Note: ‘*’ symbol is mean that specified areas are drawn as one area.Data Entry windowThe window title is displayed with text ‘Level’.Verify that the Level window is displayed in main area D, F and G as half-grid array.A data entry window is containing only one input field covers the Main area D, F and GThe following objects are display in Level window. Disabled Close button (NA12)Window TitleInput FieldInput fieldThe input field is located in main area D and F.For a single input field, the window title is clearly explaining the topic of the input field. The Level window is displayed as a single input field with only the data part.The data area of the input field remains empty.KeyboardThe keyboard associated to the Level window is displayed as dedicated keyboard.The keyboard is presented below the area of input field.The data keys are displayed the with label according to each index of variable MMI_M_LEVEL_NTC_ID in received packet EVC-20 respectively. (See the position in picture below)     e.g. the first index is displayed at key #1Received Packet (EVC-20)Use the log file to confirm that DMI receives packet EVC-20 with the following variables,MMI_SELECT_LEVEL.MMI_N_LEVELS > 0 MMI_M_CURRENT_LEVEL = 0Several MMI_M_LEVEL_NTC_ID which are listed together with each level group.The value of every index of variable MMI_M_LEVEL_FLAG are equal to 1 (makred for editable).All keypad buttons are enabled refer to the value of MMI_M_LEVEL_FLAG
            Test Step Comment: (1) MMI_gen 7985 (partly: MMI_gen 5189 (partly: touch screen), MMI_gen 5944 (partly: touch screen)));(2) MMI_gen 7986;(3) MMI_gen 7985 (partly: half grid array);(4) MMI_gen 7985 (partly: MMI_gen 4640 (partly: only data area), MMI_gen 4720, MMI_gen 4889 (partly: merge label and data));(5) MMI_gen 7985 (party: MMI_gen 4722 (partly: Table 12 <Close> button, Window title , Input field)); (6) MMI_gen 7985 (partly: MMI_gen 4637 (partly: Main-areas D and F));(7) MMI_gen 7985 (partly: note under the MMI_gen 9412);(8) MMI_gen 7985 (partly: single input field, only data part);(9) MMI_gen 2194 (partly: no marked);(10) MMI_gen 7988; MMI_gen 7985 (partly: MMI_gen 4912);(11) MMI_gen 7985 (partly: MMI_gen 4678);(12) MMI_gen 1972; MMI_gen 1978;(13) MMI_gen 1630 (partly: fulfilled condition, EVC-20); (14) MMI_gen 2194 (partly: no marked, EVC-20, [GenVSIS]);(15) MMI_gen 2206 (partly: list provided by EVC-20);(16) MMI_gen 1979 (partly: ETCS Level, MMI_M_LEVEL_FLAG);(17) MMI_gen 1979 (partly: button enabling);
            */
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Main;
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH =
                EVC30_MMIRequestEnable.EnabledRequests.DriverID | EVC30_MMIRequestEnable.EnabledRequests.Level;
            EVC30_MMIRequestEnable.Send();

            EVC14_MMICurrentDriverID.Send();
            DmiActions.ShowInstruction(this, "Enter and confirm the Driver ID");

            EVC20_MMISelectLevel.MMI_Q_CLOSE_ENABLE = MMI_Q_CLOSE_ENABLE.Disabled;
            Variables.MMI_Q_LEVEL_NTC_ID[] paramEvc20MmiQLevelNtcId =
            {
                MMI_Q_LEVEL_NTC_ID.ETCS_Level,
                MMI_Q_LEVEL_NTC_ID.ETCS_Level,
                MMI_Q_LEVEL_NTC_ID.ETCS_Level,
                MMI_Q_LEVEL_NTC_ID.ETCS_Level,
                MMI_Q_LEVEL_NTC_ID.STM_ID,
                MMI_Q_LEVEL_NTC_ID.STM_ID
            };
            Variables.MMI_M_CURRENT_LEVEL[] paramEvc20MmiMCurrentLevel =
            {
                MMI_M_CURRENT_LEVEL.NotLastUsedLevel,
                MMI_M_CURRENT_LEVEL.NotLastUsedLevel,
                MMI_M_CURRENT_LEVEL.NotLastUsedLevel,
                MMI_M_CURRENT_LEVEL.NotLastUsedLevel,
                MMI_M_CURRENT_LEVEL.NotLastUsedLevel,
                MMI_M_CURRENT_LEVEL.NotLastUsedLevel
            };
            Variables.MMI_M_LEVEL_FLAG[] paramEvc20MmiMLevelFlag =
            {
                MMI_M_LEVEL_FLAG.MarkedLevel,
                MMI_M_LEVEL_FLAG.MarkedLevel,
                MMI_M_LEVEL_FLAG.MarkedLevel,
                MMI_M_LEVEL_FLAG.MarkedLevel,
                MMI_M_LEVEL_FLAG.MarkedLevel,
                MMI_M_LEVEL_FLAG.MarkedLevel
            };
            Variables.MMI_M_INHIBITED_LEVEL[] paramEvc20MmiMInhibitedLevel =
            {
                MMI_M_INHIBITED_LEVEL.NotInhibited,
                MMI_M_INHIBITED_LEVEL.NotInhibited,
                MMI_M_INHIBITED_LEVEL.NotInhibited,
                MMI_M_INHIBITED_LEVEL.NotInhibited,
                MMI_M_INHIBITED_LEVEL.NotInhibited,
                MMI_M_INHIBITED_LEVEL.NotInhibited
            };
            Variables.MMI_M_INHIBIT_ENABLE[] paramEvc20MmiMInhibitEnable =
            {
                MMI_M_INHIBIT_ENABLE.AllowedForInhibiting,
                MMI_M_INHIBIT_ENABLE.AllowedForInhibiting,
                MMI_M_INHIBIT_ENABLE.AllowedForInhibiting,
                MMI_M_INHIBIT_ENABLE.AllowedForInhibiting,
                MMI_M_INHIBIT_ENABLE.AllowedForInhibiting,
                MMI_M_INHIBIT_ENABLE.AllowedForInhibiting
            };
            Variables.MMI_M_LEVEL_NTC_ID[] paramEvc20MmiMLevelNtcId =
            {
                MMI_M_LEVEL_NTC_ID.L1,
                MMI_M_LEVEL_NTC_ID.L2,
                MMI_M_LEVEL_NTC_ID.L3,
                MMI_M_LEVEL_NTC_ID.L0,
                MMI_M_LEVEL_NTC_ID.CBTC,
                MMI_M_LEVEL_NTC_ID.AWS_TPWS
            };

            EVC20_MMISelectLevel.MMI_Q_LEVEL_NTC_ID = paramEvc20MmiQLevelNtcId;
            EVC20_MMISelectLevel.MMI_M_CURRENT_LEVEL = paramEvc20MmiMCurrentLevel;
            EVC20_MMISelectLevel.MMI_M_LEVEL_FLAG = paramEvc20MmiMLevelFlag;
            EVC20_MMISelectLevel.MMI_M_INHIBITED_LEVEL = paramEvc20MmiMInhibitedLevel;
            EVC20_MMISelectLevel.MMI_M_INHIBIT_ENABLE = paramEvc20MmiMInhibitEnable;
            EVC20_MMISelectLevel.MMI_M_LEVEL_NTC_ID = paramEvc20MmiMLevelNtcId;
            EVC20_MMISelectLevel.Send();

            WaitForVerification("Check the following (* indicates sub-areas drawn as one area):" + Environment.NewLine +
                                Environment.NewLine +
                                "1. DMI displays the Level window in the right half of the screen." +
                                "2. The following screen areas are in Layer 0: D, F, G, Z and Y." +
                                Environment.NewLine +
                                "3. The following screen areas are in Layer 1: A1, (A2 + A3)*, A4, B*, C1, (C2 + C3 + C4)*, C5, C6, C7, C8, C9, E1, E2, E3, E4, (E5-E9)." +
                                Environment.NewLine +
                                "4. The following screen areas are in Layer 2: B3, B4, B5, B6, B7" +
                                Environment.NewLine +
                                "5. The window title is ‘Level’." + Environment.NewLine +
                                "6. The Level window comprises areas D, F and G as a half-grid array." +
                                Environment.NewLine +
                                "7. A data entry window with one input field covers areas D, F and G in the Level window." +
                                Environment.NewLine +
                                "8. The Level window contains a disabled ‘Close’ button (symbol NA12)." +
                                Environment.NewLine +
                                "9. The data entry field is in areas D and F." + Environment.NewLine +
                                "10. The window title describes the single input field." + Environment.NewLine +
                                "11. The level window displays one data entry field containing only a data part." +
                                Environment.NewLine +
                                "13. The data entry field is blank." + Environment.NewLine +
                                "14. A keyboard is displayed below the data entry field which operates only on the Level window." +
                                Environment.NewLine +
                                "15. The keyboard has a 3 x 2 array of buttons with the following buttons (left to right):" +
                                Environment.NewLine +
                                "16. ‘Level 1’, ‘Level 2’, ‘Level3’ in the top row; ‘Level 0’, ‘CBTC’, ‘AWS_TPWS’ in the bottom row.");

            /*
            Test Step 2
            Action: Press and hold ‘Level 0’ button
            Expected Result: Verify the following information,The value of input field is replaced by the pressed button.Sound ‘Click’ is played once.The state of button is changed to ‘Pressed’ and immediately back to ‘Enabled’ state.The Input Field displays the level associated to the data key according to the pressings in state ‘Pressed’.An input field is used to enter the level selection.The data value is displayed as black colour and the background of the data area is displayed as medium-grey colour.The data value of the input field is aligned to the left of the data area
            Test Step Comment: (1) MMI_gen 7985 (partly: MMI_gen 4679);(2) MMI_gen 7985 (partly: MMI_gen 4913 (partly:  MMI_gen 4384 (partly: sound ‘Click’));(3) MMI_gen 7985 (partly: MMI_gen 4913 (partly:  MMI_gen 4384 (partly: Change to state ‘Pressed’ and immediately back to state ‘Enabled’));   (4) MMI_gen 7985 (partly: MMI_gen 4913); MMI_gen 2194 (partly: acts on level selection buttons); (5) MMI_gen 7987 (partly: entry);MMI_gen 7985 (partly: MMI_gen 4634); (6) MMI_gen 7985 (partly: MMI_gen 4651);(7) MMI_gen 7985 (partly: MMI_gen 4647 (partly: aligned left));  
            */
            DmiActions.ShowInstruction(this, "Press and hold the ‘Level 0’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The input field displays ‘Level 0’." + Environment.NewLine +
                                "2. The ‘Click’ sound is played once." + Environment.NewLine +
                                "3. The ‘Level 0’ button is displayed pressed and immediately re-displayed enabled" +
                                Environment.NewLine +
                                "4. The input field can be used to enter data (enabled)." + Environment.NewLine +
                                "5. The input field data are in black on a Medium-grey background." +
                                Environment.NewLine +
                                "6. The input field data are left-aligned in the data area.");
            /*
            Test Step 3
            Action: Release the pressed button.Note: Please verify the state of all Level buttons refer to action step No.2-3
            Expected Result: Verify the following information,The state of pressed button is changed to ‘Enabled’ state
            Test Step Comment: (1) MMI_gen 7985 (partly: MMI_gen 4913 (partly:  MMI_gen 4384 (partly: ETCS-MMI’s function associated to the button)));
            */
            DmiActions.ShowInstruction(this, "Release the ‘Level 0’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Level 0’ button is displayed enabled.");

            DmiActions.ShowInstruction(this,
                "For each of the other buttons displayed on the keyboard, repeat these steps: " + Environment.NewLine +
                "1. Press and hold the button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The input field displays the value of the pressed button’." + Environment.NewLine +
                                "2. The ‘Click’ sound is played once." + Environment.NewLine +
                                "3. The button is displayed pressed and immediately re-displayed enabled" +
                                Environment.NewLine +
                                "4. The input field can be used to enter data (enabled)." + Environment.NewLine +
                                "5. The input field data are in black on a Medium-grey background." +
                                Environment.NewLine +
                                "6. The input field data are left-aligned in the data area.");

            DmiActions.ShowInstruction(this, "2. Release the button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The button is displayed enabled.");

            /* 
            Test Step 4
            Action: Press ‘ Level 1’ button Then, press and hold an input field
            Expected Result: Verify the following information,Use the log file to confirm that DMI sends EVC-101 with variable MMI_M_REQUEST = 40 (Level entered), MMI_Q_BUTTON = 1 (pressed) and MMI_T_BUTTONEVENT is not blank.The sound ‘Click’ is played once
            Test Step Comment: (1) MMI_gen 2205-1 (THR) (partly: 1st bullet, MMI_gen 11387 (partly: send events of Pressed independently to ETCS),  MMI_gen 11907 (partly: EVC-101, timestamp))); MMI_gen 3375; (2) MMI_gen 2205-1 (THR) (partly: up-type button,  MMI_gen 4381 (partly: the sound for Up-Type button), MMI_gen 9512, MMI_gen 968);
            */
            DmiActions.ShowInstruction(this, "Press the ‘Level 1’ button, then press and hold the input field");

            EVC101_MMIDriverRequest.CheckMRequestReleased = MMI_M_REQUEST.LevelEntered;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Click’ sound is played once.");

            /*
            Test Step 5
            Action: Slide out an input field
            Expected Result: DMI still displays Level window.No sound ‘Click’ is played
            Test Step Comment: (1) MMI_gen 2205-1 (THR) (partly: safe-up type, MMI_gen 11387 (partly: button Up-Type, MMI_gen 4382 (partly: when slide out with force applied, no sound)));
            */
            DmiActions.ShowInstruction(this, "Whilst keeping the input field pressed, drag it out of its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI still displays the Level window." + Environment.NewLine +
                                @"2. The ‘Click’ sound is not played.");

            /*
            Test Step 6
            Action: Slide back into an input field
            Expected Result: DMI still displays Level window.No sound ‘Click’ is played
            Test Step Comment: (1) MMI_gen 2205-1 (THR) (partly: safe-up type, MMI_gen 11387 (partly: button Up-Type, MMI_gen 4382 (partly: when slide back, no sound)));
            */
            DmiActions.ShowInstruction(this, "Whilst keeping the input field pressed, drag it back inside its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI still displays the Level window." + Environment.NewLine +
                                @"2. The ‘Click’ sound is not played.");

            /*
            Test Step 7
            Action: Release the pressed area. This is to confirm Level 1
            Expected Result: Use the log file to verify the following information,Packet (EVC-101)Use the log file to confirm that DMI sends EVC-101 with variable MMI_M_REQUEST = 40 (Level entered), MMI_Q_BUTTON = 0 (released) and MMI_T_BUTTONEVENT is not blank.Packet (EVC-121)MMI_M_LEVEL_NTC_ID which is listed the same order as EVC-20 in Step 1.MMI_M_LEVEL_FLAG in each level group has the same value as the each group of variable MMI_M_CURRENT_LEVEL (EVC-20) in Step 1.Note: The previous value and current selected value are excluded.The Level window is closed and Main window is displayed instead. DMI displays in SB mode, Level 1
            Test Step Comment: (1) MMI_gen 2205-1 (THR) (partly: 1st bullet, MMI_gen 11387 (partly: send events of Released independently to ETCS)); MMI_gen 11907 (partly: EVC-101, timestamp)); MMI_gen 3375; MMI_gen 4681 (partly: accept the entered value);(2) MMI_gen 2205-1 (THR) (partly: 2nd bullet); MMI_gen 2206 (partly: EVC-121, 1st bullet);(3) MMI_gen 2206 (partly: EVC-121, 1st bullet, 3rd bullet);(4) MMI_gen 2205-1 (THR) (partly: 3rd bullet, MMI_gen 4381 (partly: exit state ‘Pressed’ execute function associated to the button);
            */
            DmiActions.ShowInstruction(this, "Release the input field");

            EVC101_MMIDriverRequest.CheckMRequestReleased = MMI_M_REQUEST.LevelEntered;

            EVC121_MMINewLevel.LevelSelected = MMI_M_LEVEL_NTC_ID.L1;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI closes the Level window and displays the Main window" + Environment.NewLine +
                                "2. DMI displays in SB mode, Level 1.");

            /*
            Test Step 8
            Action: Press ‘Level’ button
            Expected Result: Verify the following information,The input field displays the previous entered value from Step 5.The enabled Close button NA11 is display in area G. Received Packet (EVC-20)Use the log file to confirm that DMI receives packet EVC-20 with the following variables,MMI_SELECT_LEVEL.MMI_N_LEVELS > 0 Several MMI_Q_LEVEL_NTC_ID with MMI_M_CURRENT_LEVEL which are listed together with each level group
            Test Step Comment: (1) MMI_gen 2194 (partly: present the first level marked); MMI_gen 2206 (partly: 2nd bullet); MMI_gen 8864 (partly: the value stored onboard);(2) MMI_gen 4392 (partly: [Close] NA11);(3) MMI_gen 1630 (partly: fulfilled condition, EVC-20);(4) MMI_gen 2206 (partly: list provided by EVC-20);
            */
            /*
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Level;
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.DriverID;
            EVC30_MMIRequestEnable.Send();
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Level’ button");

            EVC20_MMISelectLevel.MMI_Q_CLOSE_ENABLE = MMI_Q_CLOSE_ENABLE.Enabled;
            paramEvc20MmiMCurrentLevel[0] = MMI_M_CURRENT_LEVEL.LastUsedLevel;
            EVC20_MMISelectLevel.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The input field displays ‘Level 1’." + Environment.NewLine +
                                "2. The Level window contains an enabled ‘Close’ button (symbol NA11).");
            /*
            Test Step 9
            Action: Confirm the current data without re-entry by pressing the input field
            Expected Result: Verify the following information, Use the log file to confirm that DMI sends out the packet EVC-101 with variable MMI_M_REQUEST = 40 (Level entered) and MMI_Q_BUTTON = 1 and = 0 respectively
            Test Step Comment: (1) MMI_gen 7987 (partly: revalidation); MMI_gen 2205-1 (THR); MMI_gen 11450-1 (THR) (partly: safe, send events of Pressed and Released independently to ETCS), MMI_gen 11907 (partly: EVC-101, timestamp));
            */
            DmiActions.ShowInstruction(this,
                @"Press the input field to confirm the current value (without entering data)");

            EVC101_MMIDriverRequest.CheckMRequestReleased = MMI_M_REQUEST.LevelEntered;

            /*
            Test Step 10
            Action: Press ‘Level’ button.Then, repeat action Step 4-9 with every buttons on the dedicated keyboard respectively
            Expected Result: Use the log file to verify the following information,Packet EVC-101DMI sends out the packet EVC-101 with variable MMI_M_REQUEST = 40 (Level entered).Packet EVC-121DMI sends out the packet EVC-121 with the variable MMI_M_LEVEL_NTC_ID in each group is listed the same order as EVC-20 from Step 9 as follows,ETCS Level (MMI_Q_LEVEL_NTC_ID = 1)Level1: MMI_M_LEVEL_NTC_ID = 1Level2: MMI_M_LEVEL_NTC_ID = 2Level3: MMI_M_LEVEL_NTC_ID = 3Level0: MMI_M_LEVEL_NTC_ID = 0NTC Level (if any) (MMI_Q_LEVEL_NTC_ID = 0)Level PZB/LZB: MMI_M_LEVEL_NTC_ID = 9Level TPWS/AWS: MMI_M_LEVEL_NTC_ID = 20Level ATC SE/NO: MMI_M_LEVEL_NTC_ID = 22MMI_M_CURRENT_LEVEL in each level groups has the same value as EVC-20 in Step 9.Note: The previous value and current selected value are excluded.(4)   When the Level window is opened, the value of an input field is changed refer to driver entered data. DMI also updates and changes to the new level according to the entered data
            Test Step Comment: (1) MMI_gen 2205-1 (THR) (partly: EVC-101, EVC-121, all levels); MMI_gen 4681 (partly: replace the current data with the entered data value);(2) MMI_gen 2205-1 (partly: EVC-121); MMI_gen 2206 (partly: EVC-121, listed as EVC-20, all levels);(3) MMI_gen 2206 (partly: EVC-121, listed as EVC-20, states unchanged);(4) MMI_gen 7985 (partly: MMI_gen 4681); MMI_gen 8864 (partly: Level window);
            */
            // Repeat Steps 4-9 for Level 2 button
            DmiActions.ShowInstruction(this, "Press the ‘Level 2’ button, then press and hold the input field");

            EVC101_MMIDriverRequest.CheckMRequestReleased = MMI_M_REQUEST.LevelEntered;

            DmiActions.ShowInstruction(this, "Whilst keeping the input field pressed, drag it out of its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI still displays the Level window." + Environment.NewLine +
                                @"2. The ‘Click’ sound is not played.");

            DmiActions.ShowInstruction(this, "Whilst keeping the input field pressed, drag it back inside its area");
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI still displays the Level window." + Environment.NewLine +
                                @"2. The ‘Click’ sound is not played.");

            DmiActions.ShowInstruction(this, "Release the input field");

            EVC101_MMIDriverRequest.CheckMRequestReleased = MMI_M_REQUEST.LevelEntered;

            EVC121_MMINewLevel.LevelSelected = MMI_M_LEVEL_NTC_ID.L2;

            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Level = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_LEVEL.L2;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI closes the Level window and displays the Main window" + Environment.NewLine +
                                "2. DMI displays in SB mode, Level 2.");

            DmiActions.ShowInstruction(this, @"Press the ‘Level’ button");

            EVC20_MMISelectLevel.MMI_Q_CLOSE_ENABLE = MMI_Q_CLOSE_ENABLE.Enabled;
            paramEvc20MmiMCurrentLevel[0] = MMI_M_CURRENT_LEVEL.NotLastUsedLevel;
            paramEvc20MmiMCurrentLevel[1] = MMI_M_CURRENT_LEVEL.LastUsedLevel;
            EVC20_MMISelectLevel.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The input field displays ‘Level 2’.");

            DmiActions.ShowInstruction(this,
                @"Press the input field to confirm the current value (without entering data)");

            EVC101_MMIDriverRequest.CheckMRequestReleased = MMI_M_REQUEST.LevelEntered;

            // Repeat Steps 4-9 for Level 3 button
            DmiActions.ShowInstruction(this, "Press the ‘Level 3’ button, then press and hold the input field");

            EVC101_MMIDriverRequest.CheckMRequestReleased = MMI_M_REQUEST.LevelEntered;

            DmiActions.ShowInstruction(this, "Whilst keeping the input field pressed, drag it out of its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI still displays the Level window." + Environment.NewLine +
                                @"2. The ‘Click’ sound is not played.");

            DmiActions.ShowInstruction(this, "Whilst keeping the input field pressed, drag it back inside its area");
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI still displays the Level window." + Environment.NewLine +
                                @"2. The ‘Click’ sound is not played.");


            DmiActions.ShowInstruction(this, "Release the input field");

            EVC101_MMIDriverRequest.CheckMRequestReleased = MMI_M_REQUEST.LevelEntered;

            EVC121_MMINewLevel.LevelSelected = MMI_M_LEVEL_NTC_ID.L3;

            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Level = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_LEVEL.L3;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI closes the Level window and displays the Main window" + Environment.NewLine +
                                "2. DMI displays in SB mode, Level 3.");

            DmiActions.ShowInstruction(this, @"Press the ‘Level’ button");

            EVC20_MMISelectLevel.MMI_Q_CLOSE_ENABLE = MMI_Q_CLOSE_ENABLE.Enabled;
            paramEvc20MmiMCurrentLevel[1] = MMI_M_CURRENT_LEVEL.NotLastUsedLevel;
            paramEvc20MmiMCurrentLevel[2] = MMI_M_CURRENT_LEVEL.LastUsedLevel;
            EVC20_MMISelectLevel.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The input field displays ‘Level 3’.");

            DmiActions.ShowInstruction(this,
                @"Press the input field to confirm the current value (without entering data)");

            EVC101_MMIDriverRequest.CheckMRequestReleased = MMI_M_REQUEST.LevelEntered;

            // Repeat Steps 4-9 for Level 0 button
            DmiActions.ShowInstruction(this, "Press the ‘Level 0’ button, then press and hold the input field");
            DmiActions.ShowInstruction(this, "Whilst keeping the input field pressed, drag it out of its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI still displays the Level window." + Environment.NewLine +
                                @"2. The ‘Click’ sound is not played.");

            DmiActions.ShowInstruction(this, "Whilst keeping the input field pressed, drag it back inside its area");
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI still displays the Level window." + Environment.NewLine +
                                @"2. The ‘Click’ sound is not played.");

            //EVC101_MMIDriverRequest.CheckMRequestReleased = MMI_M_REQUEST.LevelEntered;


            DmiActions.ShowInstruction(this, "Release the input field");

            EVC101_MMIDriverRequest.CheckMRequestReleased = MMI_M_REQUEST.LevelEntered;

            EVC121_MMINewLevel.LevelSelected = MMI_M_LEVEL_NTC_ID.L0;

            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Level = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_LEVEL.L0;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in SB mode, Level 0.");

            DmiActions.ShowInstruction(this, @"Press the ‘Level’ button");

            EVC20_MMISelectLevel.MMI_Q_CLOSE_ENABLE = MMI_Q_CLOSE_ENABLE.Enabled;
            paramEvc20MmiMCurrentLevel[2] = MMI_M_CURRENT_LEVEL.NotLastUsedLevel;
            paramEvc20MmiMCurrentLevel[3] = MMI_M_CURRENT_LEVEL.LastUsedLevel;
            EVC20_MMISelectLevel.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The input field displays ‘Level 0’.");

            DmiActions.ShowInstruction(this,
                @"Press the input field to confirm the current value (without entering data)");

            EVC101_MMIDriverRequest.CheckMRequestReleased = MMI_M_REQUEST.LevelEntered;

            // Repeat Steps 4-9 for CBTC button
            DmiActions.ShowInstruction(this, "Press the ‘CBTC’ button, then press and hold the input field");

            EVC101_MMIDriverRequest.CheckMRequestReleased = MMI_M_REQUEST.LevelEntered;

            DmiActions.ShowInstruction(this, "Whilst keeping the input field pressed, drag it out of its area");
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI still displays the Level window." + Environment.NewLine +
                                @"2. The ‘Click’ sound is not played.");

            DmiActions.ShowInstruction(this, "Whilst keeping the input field pressed, drag it back inside its area");
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI still displays the Level window." + Environment.NewLine +
                                @"2. The ‘Click’ sound is not played.");

            DmiActions.ShowInstruction(this, "Release the input field");

            EVC101_MMIDriverRequest.CheckMRequestReleased = MMI_M_REQUEST.LevelEntered;

            EVC121_MMINewLevel.LevelSelected = MMI_M_LEVEL_NTC_ID.CBTC;

            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Level = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_LEVEL.LNTC;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in SB mode, Level NTC.");

            DmiActions.ShowInstruction(this, @"Press the ‘Level’ button");

            EVC20_MMISelectLevel.MMI_Q_CLOSE_ENABLE = MMI_Q_CLOSE_ENABLE.Enabled;
            paramEvc20MmiMCurrentLevel[3] = MMI_M_CURRENT_LEVEL.NotLastUsedLevel;
            paramEvc20MmiMCurrentLevel[4] = MMI_M_CURRENT_LEVEL.LastUsedLevel;
            EVC20_MMISelectLevel.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The input field displays ‘CBTC’.");

            DmiActions.ShowInstruction(this,
                @"Press the input field to confirm the current value (without entering data)");

            EVC101_MMIDriverRequest.CheckMRequestReleased = MMI_M_REQUEST.LevelEntered;

            // Repeat Steps 4-9 for AWS_TPWS button
            DmiActions.ShowInstruction(this, "Press the ‘AWS_TPWS’ button, then press and hold the input field");

            EVC101_MMIDriverRequest.CheckMRequestReleased = MMI_M_REQUEST.LevelEntered;

            DmiActions.ShowInstruction(this, "Whilst keeping the input field pressed, drag it out of its area");
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI still displays the Level window." + Environment.NewLine +
                                @"2. The ‘Click’ sound is not played.");

            DmiActions.ShowInstruction(this, "Whilst keeping the input field pressed, drag it back inside its area");
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI still displays the Level window." + Environment.NewLine +
                                @"2. The ‘Click’ sound is not played.");

            DmiActions.ShowInstruction(this, "Release the input field");

            EVC101_MMIDriverRequest.CheckMRequestReleased = MMI_M_REQUEST.LevelEntered;
            EVC121_MMINewLevel.LevelSelected = MMI_M_LEVEL_NTC_ID.AWS_TPWS;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Level = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_LEVEL.LNTC;
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in SB mode, Level NTC.");

            DmiActions.ShowInstruction(this, @"Press the ‘Level’ button");

            EVC20_MMISelectLevel.MMI_Q_CLOSE_ENABLE = MMI_Q_CLOSE_ENABLE.Enabled;
            paramEvc20MmiMCurrentLevel[4] = MMI_M_CURRENT_LEVEL.NotLastUsedLevel;
            paramEvc20MmiMCurrentLevel[5] = MMI_M_CURRENT_LEVEL.LastUsedLevel;

            EVC20_MMISelectLevel.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The input field displays ‘AWS_TPWS’.");

            DmiActions.ShowInstruction(this,
                @"Press the input field to confirm the current value (without entering data)");

            EVC101_MMIDriverRequest.CheckMRequestReleased = MMI_M_REQUEST.LevelEntered;

            /*
            Test Step 11
            Action: Perform the following procedure,Press ‘Level 2’ button.Press and hold ‘Close’ button
            Expected Result: Verify the following information,The ‘Close’ button is shown as pressed state. The sound ‘Click’ is played once
            Test Step Comment: (1) MMI_gen 11383 (partly: MMI_gen 4381 (partly: change to state ‘Pressed’ as long as remain actuated));(2) MMI_gen 11383 (partly: MMI_gen 4381 (partly: the sound for Up-Type button)); MMI_gen 9512; MMI_gen 968;
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Level 2’ button, then press and hold the ‘Close’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Close’ button is displayed enabled" + Environment.NewLine +
                                "2. The ‘Click’ sound is played once.");

            /*
            Test Step 12
            Action: Slide out ‘Close’ button
            Expected Result: DMI still displays the Level window.Verify the following information,The ‘Close’ button becomes the ‘Enabled’ state without a sound
            Test Step Comment: (1) MMI_gen 11383 (partly: MMI_gen 4382 (partly: state ‘Enabled’ when slide out with force applied, no sound));
            */
            DmiActions.ShowInstruction(this, "Whilst keeping the ‘Close’ button pressed, drag it out of its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI still displays the Level window." + Environment.NewLine +
                                "2. The ‘Close’ button is displayed enabled." + Environment.NewLine +
                                "3. No sound is played.");

            /*
            Test Step 13
            Action: Slide back into ‘Close’ button
            Expected Result: DMI still displays the Level window.Verify the following information,The ‘Close’ button is shown as pressed state and no sound ‘Click’ is played
            Test Step Comment: (1) MMI_gen 11383 (partly: MMI_gen 4382 (partly: state ‘Pressed’ when slide back, no sound));
            */
            DmiActions.ShowInstruction(this, "Whilst keeping the ‘Close’ button pressed, drag it back inside its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI still displays the Level window." + Environment.NewLine +
                                "2. The ‘Close’ button is displayed pressed." + Environment.NewLine +
                                "3. No sound is played.");

            /*
            Test Step 14
            Action: Release ‘Close’ button
            Expected Result: Verify the following information,Use the log file to confirm that DMI sends out the packet [MMI_DRIVER_REQUEST (EVC-101)])  with variable MMI_M_REQUEST] = 32 (Exit Change Level).The Level window is closed, DMI displays Main window
            Test Step Comment: (1) MMI_gen 11383 (partly: EVC-101, MMI_gen 4381 (partly: exit state ‘Pressed’, execute function associated to the button));(2) MMI_gen 11383 (partly: closure);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Release the ‘Close’ button");

            EVC101_MMIDriverRequest.CheckMRequestReleased = MMI_M_REQUEST.ExitChangeLevel;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI closes the Level window and displays the Main window");

            /*
            Test Step 15
            Action: Press ‘Level’ button
            Expected Result: Verify the following information,The input field displays the previous entered value from step 10
            Test Step Comment: (1) MMI_gen 11383 (partly: discarded);
            */
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Main; // Main
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.Level;
            EVC30_MMIRequestEnable.Send();

            DmiActions.ShowInstruction(this, @"Press the ‘Level’ button");

            EVC20_MMISelectLevel.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The input field displays ‘AWS_TPWS’.");

            /*
            Test Step 16
            Action: Use the test script file 22_5_1_a.xml to send EVC-20 with,MMI_N_LEVELS = 1MMI_M_CURRENT_LEVEL = 1MMI_M_LEVEL_FLAG = 1MMI_M_LEVEL_NTC_ID = 3
            Expected Result: Verify the following information,(1)   The level window is updated according to the following,-      There is only one button ‘Level 3’ displayed in keypad-      The value of input field is changed to ‘Level 3’
            Test Step Comment: (1) MMI_gen 2197;
            */
            XML_22_5_1(msgType.typea);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The Level window displays only one button (‘Level 3’) on the keypad." +
                                Environment.NewLine +
                                "2. The data input field displays ‘Level 3’.");

            /*
            Test Step 17
            Action: Use the test script file 22_5_1_b.xml to send EVC-20 with,MMI_N_LEVELS = 0
            Expected Result: Verify the following information,DMI displays Main window
            Test Step Comment: (1) MMI_gen 1630 (partly: NEAGTIVE, 2nd  bullet); MMI_gen 2277;
            */
            XML_22_5_1(msgType.typeb);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Main window.");

            /*
            Test Step 18
            Action: End of test
            Expected Result: 
            */


            return GlobalTestResult;
        }

        #region Send_XML_22_5_1_DMI_Test_Specification

        enum msgType
        {
            typea,
            typeb
        }

        private void XML_22_5_1(msgType type)
        {
            EVC20_MMISelectLevel.MMI_Q_CLOSE_ENABLE = Variables.MMI_Q_CLOSE_ENABLE.Disabled;
            switch (type)
            {
                case msgType.typea:
                    EVC20_MMISelectLevel.MMI_Q_LEVEL_NTC_ID = new Variables.MMI_Q_LEVEL_NTC_ID[]
                        {Variables.MMI_Q_LEVEL_NTC_ID.ETCS_Level};
                    EVC20_MMISelectLevel.MMI_M_CURRENT_LEVEL = new Variables.MMI_M_CURRENT_LEVEL[]
                        {Variables.MMI_M_CURRENT_LEVEL.LastUsedLevel};
                    EVC20_MMISelectLevel.MMI_M_LEVEL_FLAG = new Variables.MMI_M_LEVEL_FLAG[]
                        {Variables.MMI_M_LEVEL_FLAG.MarkedLevel};
                    EVC20_MMISelectLevel.MMI_M_INHIBITED_LEVEL = new Variables.MMI_M_INHIBITED_LEVEL[]
                        {Variables.MMI_M_INHIBITED_LEVEL.NotInhibited};
                    EVC20_MMISelectLevel.MMI_M_INHIBIT_ENABLE = new Variables.MMI_M_INHIBIT_ENABLE[]
                        {Variables.MMI_M_INHIBIT_ENABLE.AllowedForInhibiting};
                    EVC20_MMISelectLevel.MMI_M_LEVEL_NTC_ID = new Variables.MMI_M_LEVEL_NTC_ID[]
                        {Variables.MMI_M_LEVEL_NTC_ID.L3};
                    break;
                case msgType.typeb:
                    EVC20_MMISelectLevel.MMI_Q_LEVEL_NTC_ID = new MMI_Q_LEVEL_NTC_ID[0];
                    EVC20_MMISelectLevel.MMI_M_CURRENT_LEVEL = new MMI_M_CURRENT_LEVEL[0];
                    EVC20_MMISelectLevel.MMI_M_LEVEL_FLAG = new MMI_M_LEVEL_FLAG[0];
                    EVC20_MMISelectLevel.MMI_M_INHIBITED_LEVEL = new MMI_M_INHIBITED_LEVEL[0];
                    EVC20_MMISelectLevel.MMI_M_INHIBIT_ENABLE = new MMI_M_INHIBIT_ENABLE[0];
                    EVC20_MMISelectLevel.MMI_M_LEVEL_NTC_ID = new MMI_M_LEVEL_NTC_ID[0];
                    break;
            }
            EVC20_MMISelectLevel.Send();
        }

        #endregion
    }
}
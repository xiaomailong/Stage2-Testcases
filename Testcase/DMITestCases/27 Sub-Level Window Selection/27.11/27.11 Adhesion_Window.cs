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
    /// 27.11 Adhesion Window
    /// TC-ID: 22.11
    /// 
    /// This test case verifies the display of the ‘Adhesion’ window on DMI that shall comply with [ERA-ERTMS] standard and [MMI-ETCS-gen].
    /// 
    /// Tested Requirements:
    /// MMI_gen 8307; MMI_gen 8308 (partly: entry); MMI_gen 11089; MMI_gen 8309; MMI_gen 11449-1 (THR) (partly: MMI_gen 11387, MMI_gen 11907 (partly: EVC-101, timestamp), MMI_gen 4381, MMI_gen 4382, MMI_gen 9512, MMI_gen 968); MMI_gen 8306 (partly: half grid array, single input field, only data part, MMI_gen 5189 (partly: touch screen), MMI_gen 5944 (partly: touch screen), MMI_gen 4640 (partly: only data area), MMI_gen 4679, MMI_gen 4720, MMI_gen 4889 (partly: merge label and data), MMI_gen 4722 (partly: Table 12 <Close> button, Window title, Input field), MMI_gen 4637 (partly: Main-areas D and F), note under the MMI_gen 9412, MMI_gen 4912, MMI_gen 4678, MMI_gen 4913 (partly: MMI_gen 4384), MMI_gen 4634, MMI_gen 4651, MMI_gen 4682, MMI_gen 4647 (partly: left aligned), MMI_gen 4684 (partly: terminated)); MMI_gen 4392 (partly: [Close] NA11, returning to the parent window, [Enter], touch screen); MMI_gen 3375;  MMI_gen 4350; MMI_gen 4351; MMI_gen 4353;
    /// 
    /// Scenario:
    /// Drive the train pass BG1 at position 100m.BG1: Packet 3 (Q_NVDRIVER_ADHES = 1)The Down-type button on keypad is verified.The data entry of the ‘Adhesion’ window is verified.The type of enter button is verified.
    /// 
    /// Used files:
    /// 22_11.tdg
    /// </summary>
    public class TC_ID_22_11_Adhesion_Window : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:

            // Call the TestCaseBase PreExecution
            base.PreExecution();

            // Test system is powered ONCabin is activeSoM is completed in SR mode, Level 1
            DmiActions.Complete_SoM_L1_SR(this);

        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in SR mode, level 1

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint

            /*
            Test Step 1
            Action: Drive the train forward pass BG1. Then press ‘Special menu’ button
            Expected Result: The Special window is displayed with enabled/disabled sub-menus button.Verify that the Adhesion button is enabled
            */
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.EVC30WindowID.Main;
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.None;
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.Adhesion;
            EVC30_MMIRequestEnable.Send();

            DmiActions.ShowInstruction(this, "Press the ‘Spec’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Special window." + Environment.NewLine +
                                "2. The ‘Adhesion’ button is enabled");

            /*
            Test Step 2
            Action: Press ‘Adhesion’ button
            Expected Result: DMI displays the Adhesion window on the right half part of the window.Layers(1)   The layers of window on half-grid array is displayed as followsLayer 0: Main-Area D, F, G, Y and Z.Layer -1: A1, A2+A3*, A4, B*, C1, C2+C3+C4*, C5, C6, C7, C8, C9, E1, E2, E3, E4, E5-E9*Layer -2: B3, B4, B5, B6, B7Note: ‘*’ symbol is mean that specified areas are drawn as one area.Data Entry windowThe window title is displayed with text “Adhesion”.Verify that the Adhesion window is displayed in main area D, F and G as half-grid array.A data entry window is containing only one input field covers the Main area D, F and G.The following objects are display in Adhesion window. Enabled Close button (NA11)Window TitleInput FieldInput fieldThe input field is located in main area D and F.For a single input field, the window title is clearly explaining the topic of the input field. The Adhesion window is displayed as a single input field with only the data part.KeyboardThe keyboard associated to the Adhesion window is presented as a dedicated keyboard and displayed with Non slippery rail and Slippery rail for driver selection.The keyboard is presented below the area of input field.General property of windowThe Adhesion window is presented with objects and buttons which is the one of several levels and allocated to areas of DMI. All objects, text messages and buttons are presented within the same layer.(13)  The Default window is not displayed and covered the current window
            Test Step Comment: (1) MMI_gen 8306 (partly: MMI_gen 5189 (partly: touch screen), MMI_gen 5944 (partly: touch screen));(2) MMI_gen 8037;(3) MMI_gen 8306 (partly: half grid array);(4) MMI_gen 8306 (partly: MMI_gen 4640 (partly: only data area), MMI_gen 4720, MMI_gen 4889 (partly: merge label and data));(5) MMI_gen 8306 (party: MMI_gen 4722 (partly: Table 12 <Close> button, Window title, Input field)); MMI_gen 4392 (partly: [Close] NA11);(6) MMI_gen 8306 (partly: MMI_gen 4637 (partly: Main-areas D and F));(7) MMI_gen 8306 (partly: note under the MMI_gen 9412);(8) MMI_gen 8306 (partly: single input field, only data part);(9) MMI_gen 8309; MMI_gen 8306 (partly: MMI_gen 4912);(10) MMI_gen 8306 (partly: MMI_gen 4678);(11) MMI_gen 4350;(12) MMI_gen 4351;(13) MMI_gen 4353;
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Adhesion’ button");

            WaitForVerification("Check the following (* indicates sub-areas drawn as one area):" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Adhesion window as a half-grid array in the right half of the window with 3 layers, with the title ‘Adhesion’." + Environment.NewLine +
                                "2. Layer 0 is displayed in areas D, F, G, Y and Z." + Environment.NewLine +
                                "3. Layer 1 is displayed in areas A1, (A2 + A3)*, A4, B*, C1, (C2 + C3 + C4)*, C5, C6, C7, C8, C9, E1, E2, E3, E4, (E5 - E9)*." + Environment.NewLine +
                                "4. Layer 2 is displayed in areas B3, B4, B5, B6, B7."  + 
                                "5. A data entry window (containing one data input field) covers areas D, F and G." + Environment.NewLine +
                                "6. The Adhesion window also contains an enabled Close button (symbol NA11)" + Environment.NewLine +
                                "7. A data input field is displayed in areas D and F." + Environment.NewLine +
                                "8. The Adhesion window displays one data input field with only a data part." + Environment.NewLine +
                                "9  The Adhesion window displays a dedicated keypad with ‘Non slippery rail’ and ‘Slippery rail’ keys." + Environment.NewLine +
                                "10. The keypad is displayed below the data input field." + Environment.NewLine +
                                "11. Objects, text messages and buttons can be displayed in several levels. Within a level they are allocated to areas." + Environment.NewLine +
                                "12. All objects, text messages and buttons are displayed in the same layer." + Environment.NewLine +
                                "13.  The Default window not displayed covering the current window.");

            /*
            Test Step 3
            Action: Press and hold every buttons on the dedicate keyboard respectively
            Expected Result: Verify the following information,The value of input field is replaced by the pressed button.Sound ‘Click’ is played once.The state of button is changed to ‘Pressed’ and immediately back to ‘Enabled’ state.The Input Field displays the language associated to the data key according to the pressings in state ‘Pressed’.An input field is used to enter the adhesion status.The data value is displayed as black colour and the background of the data area is displayed as medium-grey colour.The data value of the input field is aligned to the left of the data area
            Test Step Comment: (1) MMI_gen 8306 (partly: MMI_gen 4679);(2) MMI_gen 8306 (partly: MMI_gen 4913 (partly: MMI_gen 4384 (partly: sound Click’)));(3) MMI_gen 8306 (partly: MMI_gen 4913 (partly: MMI_gen 4384 (partly: Change to state ‘Pressed’ and immediately back to state ‘Enabled’)));   (4) MMI_gen 8306 (partly: MMI_gen 4913);                      (5) MMI_gen 8308 (partly: entry); MMI_gen 8306 (partly: MMI_gen 4634);(6) MMI_gen 8306 (partly: MMI_gen 4651);(7) MMI_gen 8306 (partly: MMI_gen 4647 (partly: left aligned));
            */
            DmiActions.ShowInstruction(this, @"Press and hold the ‘Non slippery rail’ key on the dedicated keypad");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘Non slippery rail’ key is displayed pressed and immediately displayed enabled." + Environment.NewLine +
                                @"2. The ‘Click’ sound is played once." + Environment.NewLine +
                                "3. The data input field displays ‘Non slippery rail’ in black on a Medium-grey background." + Environment.NewLine +
                                "4. The data input field is used to display the adhesion status.");

            DmiActions.ShowInstruction(this, @"Press and hold the ‘Slippery rail’ key on the dedicated keypad");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                @"1. The ‘Slippery rail’ key is displayed pressed and immediately displayed enabled." + Environment.NewLine +
                                @"2. The ‘Click’ sound is played once." + Environment.NewLine +
                                "3. The data input field displays ‘Slippery rail’ in black on a Medium-grey background." + Environment.NewLine +
                                "4. The data input field is used to display the adhesion status.");
            /*
            Test Step 4
            Action: Released the pressed button
            Expected Result: Verify the following information, The state of button is changed to ‘Enabled
            Test Step Comment: (1) MMI_gen 8306 (partly: MMI_gen 4913 (partly: MMI_gen 4384 (partly: ETCS-MMI’s function associated to the button)));
            */
            DmiActions.ShowInstruction(this, @"Released the pressed key");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The key is displayed enabled");

            /*
            Test Step 5
            Action: Select and confirm ‘Slippery rail’ button
            Expected Result: Verify the following information, Use the log file to confirm that DMI sends out the packet [MMI_DRIVER_REQUEST (EVC-101)] with variable [MMI_DRIVER_REQUEST (EVC-101).MMI_M_REQUEST] = 11.The window is closed and returned to the ‘Special’ window.Note: The adhesion symbol ST02 is displayed in sub-area A4
            Test Step Comment: (1) MMI_gen 11089 (partly: Slippery rail);               MMI_gen 8306 (partly: MMI_gen 4682)); MMI_gen 4392 (partly: [Enter], touch screen);                                                          (2) MMI_gen 11089 (partly: closure), MMI_gen 8306 (partly: MMI_gen 4684 (partly: terminated)); 
            */
            DmiActions.ShowInstruction(this, @"Select the ‘Slippery rail’ key and confirm the value");

            EVC101_MMIDriverRequest.CheckMRequestReleased = MMI_M_REQUEST.SetAdhesionCoefficientToSlipperyRail;
            
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI closes the Adhesion window and displays the Special window." + Environment.NewLine +
                                "2. DMI displays the adhesion symbol ST02 in sub-area A4");

            /*
            Test Step 6
            Action: Press ‘Adhesion’ button
            Expected Result: The Adhesion window is displayed
            */
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.EVC30WindowID.Main;
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.None;
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.Adhesion;
            EVC30_MMIRequestEnable.Send();

            DmiActions.ShowInstruction(this, @"Press the ‘Adhesion’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Adhesion window");

            /*
            Test Step 7
            Action: Select and confirm ‘Non Slippery rail’ button
            Expected Result: Verify the following information, Use the log file to confirm that DMI sends out the packet [MMI_DRIVER_REQUEST (EVC-101)] with variable [MMI_DRIVER_REQUEST (EVC-101).MMI_M_REQUEST] = 10. The window is closed and returned to the ‘Special’ window.Note: The adhesion symbol ST02 is removed
            Test Step Comment: (1) MMI_gen 11089 (partly: Non Slippery rail);               MMI_gen 8306 (partly: MMI_gen 4682));  MMI_gen 4392 (partly: [Enter], touch screen);                                                (2) MMI_gen 11089 (partly: closure), MMI_gen 8306 (partly: MMI_gen 4684 (partly: terminated)); 
            */
            DmiActions.ShowInstruction(this, @"Select the ‘Non Slippery rail’ key and confirm the value");

            EVC101_MMIDriverRequest.CheckMRequestReleased = MMI_M_REQUEST.RestoreAdhesionCoefficientToNonSlipperyRail;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI closes the Adhesion window and displays the Special window." + Environment.NewLine +
                                "2. DMI does not display the adhesion symbol ST02");

            /*
            Test Step 8
            Action: Press ‘Adhesion’ button
            Expected Result: The Adhesion window is displayed
            */
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.EVC30WindowID.Main;
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.None;
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.Adhesion;
            EVC30_MMIRequestEnable.Send();

            DmiActions.ShowInstruction(this, @"Press the ‘Adhesion’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Adhesion window");

            /*
            Test Step 9
            Action: Select ‘Non slippery rail’.Then, press and hold an input field
            Expected Result: Verify the following information,Use the log file to confirm that DMI sends out the packet [MMI_DRIVER_REQUEST (EVC-101)] with variable [MMI_DRIVER_REQUEST (EVC-101).MMI_Q_BUTTON] =  1 and MMI_T_BUTTONEVENT is not blank.The sound ‘Click’ played once
            Test Step Comment: (1) MMI_gen 11449-1 (THR) (partly: MMI_gen 11387 (partly: send events of Pressed independently to ETCS, MMI_gen 11907 (partly: EVC-101, timestamp))); MMI_gen 3375;(2) MMI_gen 11449-1 (THR) (partly: up-type button, MMI_gen 4381 (partly: the sound for Up-Type button), MMI_gen 9512, MMI_gen 968);
            */
            DmiActions.ShowInstruction(this, @"Select the ‘Non Slippery rail’ key, then press and hold the data input field");

            // This will not work: DMI only sends the packet when the button has been released
            //EVC101_MMIDriverRequest.CheckMRequestPressed = MMI_M_REQUEST.RestoreAdhesionCoefficientToNonSlipperyRail;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Click’ sound is played once.");

            /*
            Test Step 10
            Action: Slide out of an input field
            Expected Result: Verify the following information,No sound ‘Click’ is played
            Test Step Comment: (1) MMI_gen 11449-1 (THR) (partly: safe-up type, MMI_gen 11387 (partly: button Up-Type, MMI_gen 4382 (partly: when slide out with force applied, no sound)));
            */
            DmiActions.ShowInstruction(this, @"Whilst keeping the data input field pressed, drag it out of its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. No sound is played.");

            /*
            Test Step 11
            Action: Slide back into an input field
            Expected Result: Verify the following information,No sound ‘Click’ is played
            Test Step Comment: (1) MMI_gen 11449-1 (THR) (partly: safe-up type, MMI_gen 11387 (partly: button Up-Type, MMI_gen 4382 (partly: when slide back, no sound)));
            */
            DmiActions.ShowInstruction(this, @"Whilst keeping the data input field pressed, drag it back inside its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. No sound is played.");

            /*
            Test Step 12
            Action: Release the pressed area
            Expected Result: Verify the following information,Use the log file to confirm that DMI sends out the packet [MMI_DRIVER_REQUEST (EVC-101)] with variable [MMI_DRIVER_REQUEST (EVC-101).MMI_Q_BUTTON] = 0 and MMI_T_BUTTONEVENT is not blank
            Test Step Comment: (1) MMI_gen 11449-1 (THR) (partly: 1st bullet, MMI_gen 11387 (partly: send events of Released independently to ETCS)); MMI_gen 3375;
            */
            DmiActions.ShowInstruction(this, @"Release the data input field");

            // No EVC101 ACK for button released

            // Need to close this window and re-display the Special window
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.EVC30WindowID.Close_current_return_to_parent;        // close window
            EVC30_MMIRequestEnable.Send();

            /*
            Test Step 13
            Action: Press the ‘Adhesion’ button.Then, press the ‘Close’ button
            Expected Result: Verify the following information,(1)   DMI displays Special window
            Test Step Comment: (1) MMI_gen 4392 (partly: returning to the parent window);
            */
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.EVC30WindowID.Main;        // close window
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.None;
            EVC30_MMIRequestEnable.Send();
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.Adhesion;
            EVC30_MMIRequestEnable.Send();

            DmiActions.ShowInstruction(this, @"Press the ‘Adhesion’ button, then press the ‘Close’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Special window");

            /*
            Test Step 14
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
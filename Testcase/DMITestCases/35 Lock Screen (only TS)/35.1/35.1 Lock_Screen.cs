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


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 35.1 Lock Screen
    /// TC-ID: 32.1
    /// 
    /// This test case verifies the properties of Lock Screen function that do not have any effect to operation.
    /// 
    /// Tested Requirements:
    /// MMI_gen 2520; MMI_gen 2521; MMI_gen 2522; MMI_gen 2523; MMI_gen 1097; MMI_gen 4256 (partly: Sinfo sound); MMI_gen 9516 (partly: lock screen function); MMI_gen 12025 (partly: lock screen function);
    /// 
    /// Scenario:
    /// Activate cabin A. Enter Driver ID and perform brake test.Enter and validate Train data. Then, enter train running number.Open Settings window. Then, verify the state of ‘Lock Screen’ button.Press ‘Lock Screen’ button. Then, verify the display information and sound.Open Main window. Then, press ‘Start’ button and acknowledge SR mode.Open Setting window and press ‘Lock Screen’ button.Drive the train forward. Then, verify the display information and sound.
    /// 
    /// Used files:
    /// N/A
    /// </summary>
    public class TC_ID_32_1_Lock_Screen : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:

            // Call the TestCaseBase PreExecution
            base.PreExecution();

            // System is power on.
            DmiActions.Start_ATP();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays  in SR mode, Level 1
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in SR mode, Level 1.");

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint

            /*
            Test Step 1
            Action: Perform the following procedure,Activate Cabin A.Enter Driver ID and perform brake test.Select and confirm Level 1.Press ‘Train data button.Enter and confirm all data. Then, press ‘Yes’ button.Press ‘Yes’ button and Confirm entered data by pressing an input field.Enter and confirm Train running numberPress ‘Close’ button
            Expected Result: DMI displays Default window in SB mode and Level 1
            */
            DmiActions.Activate_Cabin_1(this);
            DmiActions.Set_Driver_ID(this, "1234");

            DmiActions.Send_SB_Mode(this);
            DmiActions.ShowInstruction(this, "Enter and confirm Driver ID");

            DmiActions.Request_Brake_Test(this);
            DmiActions.ShowInstruction(this, "Perform Brake Test");

            DmiActions.Display_Level_Window(this);
            DmiActions.ShowInstruction(this, "Select and enter Level 1");

            DmiActions.Display_Main_Window_with_Start_button_enabled(this);
            DmiActions.ShowInstruction(this, @"Press ‘Train data’ button");

            //won't know what data to display in validation: DmiActions.Display_Train_Data_Window(this);
            DmiActions.Send_EVC6_MMICurrentTrainData(Variables.MMI_M_DATA_ENABLE.TrainSetID |
                                                     Variables.MMI_M_DATA_ENABLE.TrainCategory |
                                                     Variables.MMI_M_DATA_ENABLE.TrainLength |
                                                     Variables.MMI_M_DATA_ENABLE.BrakePercentage |
                                                     Variables.MMI_M_DATA_ENABLE.MaxTrainSpeed |
                                                     Variables.MMI_M_DATA_ENABLE.AxleLoadCategory |
                                                     Variables.MMI_M_DATA_ENABLE.Airtightness,
                                                     100, 200,
                                                     Variables.MMI_NID_KEY.PASS2,
                                                     70,
                                                     Variables.MMI_NID_KEY.CATA,
                                                     0,
                                                     Variables.MMI_NID_KEY.G1,
                                                     EVC6_MMICurrentTrainData.MMI_M_BUTTONS_CURRENT_TRAIN_DATA.BTN_YES_DATA_ENTRY_COMPLETE,
                                                     0, 0, new[] { "FLU", "RLU", "Rescue" }, null);

            DmiActions.ShowInstruction(this, @"Perform the following actions on the DMI: " + Environment.NewLine + Environment.NewLine +
                                "1. Confirm value in each input field." + Environment.NewLine +
                                "2. Press ‘Yes’ button.");

            DmiActions.Send_EVC10_MMIEchoedTrainData(this,
                                                     Variables.MMI_M_DATA_ENABLE.TrainSetID |
                                                     Variables.MMI_M_DATA_ENABLE.TrainCategory |
                                                     Variables.MMI_M_DATA_ENABLE.TrainLength |
                                                     Variables.MMI_M_DATA_ENABLE.BrakePercentage |
                                                     Variables.MMI_M_DATA_ENABLE.MaxTrainSpeed |
                                                     Variables.MMI_M_DATA_ENABLE.AxleLoadCategory |
                                                     Variables.MMI_M_DATA_ENABLE.Airtightness,
                                                     100, 200,
                                                     Variables.MMI_NID_KEY.PASS2,
                                                     70, 
                                                     Variables.MMI_NID_KEY.CATA,
                                                     0,
                                                     Variables.MMI_NID_KEY.G1,
                                                     new[] { "FLU", "RLU", "Rescue" });

            // test wrong: pressing Yes button confirms the data?
            DmiActions.ShowInstruction(this, @"Perform the following actions on the DMI: " + Environment.NewLine + Environment.NewLine +
                                "1. Press ‘Yes’ button." + Environment.NewLine +
                                "2. Confirmed the selected values by pressing  a data input field.");

            DmiActions.Display_TRN_Window(this);
            DmiActions.ShowInstruction(this, "Enter and confirm Train Running Number and press the ‘Close’ button");
            
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in SB mode, Level 1.");
            
            /*
            Test Step 2
            Action: The train is at standstill. Press ’Settings’ button
            Expected Result: The speed pointer is indicated to position 0 km/h.The Settings menu is displayed with enabled ’Lock Screen’ button
            Test Step Comment: MMI_gen 2520;
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 0;
            DmiActions.ShowInstruction(this, "Press the ‘Settings’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays speed = 0 km/h." + Environment.NewLine +
                                "2. DMI displays the Settings menu with the ‘Lock Screen’ button enabled.");

            /*
            Test Step 3
            Action: Press ‘Lock Screen’ button
            Expected Result: Verify the following information,The ‘Lock Screen’ function is activated.The activation of this function is clearly visualised and displayed as countdown. This function has a maximum duration of 10s, The countdown is start from 10 to 0.Note: Text “Screen locked for X” disappears when X=0.DMI plays Sinfo sound when the countdown text is “Screen locked for 1”.DMI displays Settings window when the Lock Screen function is deactivated
            Test Step Comment: (1) MMI_gen 2520 (partly: train is at standstill);                                               (2) MMI_gen 2521 (partly: clrealy visualize);                        (3) MMI_gen 2521 (partly: maximum duration);                                     (4) MMI_gen 2522;                  MMI_gen 1097; MMI_gen 9516 (partly: deactivation of lock screen function); MMI_gen 12025 (partly: deactivation of lock screen function);                                    (5) MMI_gen2523;
            */
            DmiActions.ShowInstruction(this, "Press the ‘Lock Screen’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Lock Screen’ function is activated and displays ‘Screen locked for 10’." + Environment.NewLine +
                                "2. After each second the number displayed in ‘Screen locked for ..’ decreases by 1." + Environment.NewLine +
                                "3. When the screen displays ‘Screen locked for 1’ DMI plays the Sinfo sound." + Environment.NewLine +
                                "4. After 10s the Lock Screen function is deactivated and DMI displays the Settings window");

            /*
            Test Step 4
            Action: Press ‘Close’ button on Settings window
            Expected Result: DMI displays Default window
            */
            DmiActions.ShowInstruction(this, "Press the ‘Close’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                               "1. DMI displays the Default window");

            /*
            Test Step 5
            Action: Press ‘Main’ button and select ‘Start’ button. Then, acknowledge SR mode
            Expected Result: DMI displays in SR mode and Level 1
            */
            DmiActions.ShowInstruction(this, "Press the ‘Main’ button, then select the ‘Start’ button");

            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 263;            // Ack SR mode
            EVC8_MMIDriverMessage.Send();

            DmiActions.ShowInstruction(this, "Acknowledge SR mode");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in SR mode and Level 1.");

            // Remove SR ACK symbol
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 4;
            EVC8_MMIDriverMessage.Send();

            /*
            Test Step 6
            Action: Press ‘Settings menu’ button and select ‘Lock Screen’ button
            Expected Result: The ‘Lock Screen’ is activated
            */
            DmiActions.ShowInstruction(this, "Press the ‘Settings menu’ button. Press the ‘Lock Screen’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Lock Screen’ function is activated.");

            /*
            Test Step 7
            Action: Drive the train forward with speed = 40km/h
            Expected Result: Verify the following information,Verify that DMI displays the default window after 1 second from the speed digital increased.The sound ‘Sinfo’ is played
            Test Step Comment: (1) MMI_gen 2520 (partly: train starts moving);     (2) MMI_gen 2520 (partly: sound Sinfo); MMI_gen 4256 (partly: Sinfo sound); MMI_gen 9516 (partly: activation of lock screen function); MMI_gen 12025 (partly: activation of lock screen function);    
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 40;
            
            // Will this work?
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Note that the speed registered is 40 km/h and after 1s DMI displays the Default window." + Environment.NewLine +
                                "2. DMI plays the Sinfo sound.");

            /*
            Test Step 8
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
using System;
using BT_CSB_Tools;
using Testcase.Telegrams.EVCtoDMI;

namespace Testcase.DMITestCases
{
    /// <summary>
    /// Response Time with Up-Type Button
    /// TC-ID: 1.11
    /// Doors unique ID: TP-35816
    /// This test case verifies that response time of DMI starting from up-type button is released until related message is available to the GPP is not exceeded 130 ms.
    /// 
    /// Tested Requirements:
    /// MMI_gen 65;
    /// 
    /// Scenario:
    /// 1. Perform SoM to Level 1 in SR mode.
    /// 2. Verify response time of DMI on actuation of ‘Train data’ button in ‘Main’ window.
    /// 3. Verify response time of DMI on actuation of ‘Start’ button in ‘Main’ window.
    /// 4. Press ‘Data view’ button in sub area F3 and then press ‘Close’ button in ‘Data view’ window.
    /// 5. Press SE04 symbol in sub area F5 and then press ‘System Version’ button in ‘Settings’ window.
    /// 
    /// Used files:
    /// N/A
    /// </summary>
    public class TC_ID_1_11_Response_Time_with_Up_Type_Button : TestcaseBase
    {
        public override void PreExecution()
        {
            /* Pre-conditions from TestSpec
            	System is power on.
            */

            TraceInfo("Pre-condition: " + "System is power on.");

            base.PreExecution();
        }

        public override void PostExecution()
        {
            /* Post-conditions from TestSpec
            	DMI displays in SR mode, Level 1.
            */

            TraceInfo("Post-condition: " + "DMI displays in SR mode, Level 1.");

            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            throw new TestcaseException("The performance bits of this testcase is currently impossible to perform");
            /*
            Test Step 1
            Action:
            	Perform SoM to Level 1 in SR mode
            Expected Result:
            	ETCS OB enters SR mode in Level 1
            */
            MakeTestStepHeader(1, 35829, "Perform SoM to Level 1 in SR mode", "ETCS OB enters SR mode in Level 1");

            StartUp();

            DmiActions.Display_Driver_ID_Window(this, "1234");
            DmiActions.Send_SB_Mode(this);
            DmiActions.ShowInstruction(this, "Enter and confirm Driver ID");


            DmiActions.Request_Brake_Test(this);
            DmiActions.ShowInstruction(this, "Perform Brake Test");


            DmiActions.Display_Level_Window(this);
            DmiActions.ShowInstruction(this, "Select and enter Level 1");


            DmiActions.Display_Main_Window_with_Start_button_not_enabled(this);

            DmiExpectedResults.SR_Mode_displayed(this);
            DmiExpectedResults.Driver_symbol_displayed(this, "Level 1", "LE03", "C8", true);

            /*
            Test Step 2
            Action:
            	Verify response time of DMI on actuation of ‘Train data’ button in ‘Main’ window
            Expected Result:
            	Use log file to verify that response time of DMI on actuation of the ‘Train data’ buttons is not exceeded 130 ms when DMI sends EVC-101 with verification below:-
            	- In packet EVC-101 that variable [MMI_M_REQUEST = 3] and [MMI_Q_BUTTON = 0], the different time between time when ODL log receives this packet and timestamp in variable ‘MMI_T_BUTTONEVENT’ is not exceeded 130 ms. 
            	Note Use epoch & unix timestamp converter to convert timestamp recorded in variable ‘MMI_T_BUTTONEVENT’
            Test Step Comment:
            	MMI_gen 65 (partly: ‘Train data’ button);
            	Note All buttons of a menu window (TS) shall be up-type buttons, refers to MMI_gen 4557
            */
            MakeTestStepHeader(2, 35830,
                "Verify response time of DMI on actuation of ‘Train data’ button in ‘Main’ window",
                "Use log file to verify that response time of DMI on actuation of the ‘Train data’ buttons is not exceeded 130 ms when DMI sends EVC-101 with verification below:-" +
                Environment.NewLine +
                "- In packet EVC-101 that variable [MMI_M_REQUEST = 3] and [MMI_Q_BUTTON = 0], the different time between time when ODL log receives this packet and timestamp in variable ‘MMI_T_BUTTONEVENT’ is not exceeded 130 ms. " +
                Environment.NewLine +
                "Note Use epoch & unix timestamp converter to convert timestamp recorded in variable ‘MMI_T_BUTTONEVENT’");

            DmiExpectedResults.Train_Data_Button_pressed_and_released(this);

            WaitForVerification("Verify the following:" + Environment.NewLine + Environment.NewLine +
                                "Actuation of the ‘Train data’ buttons is not exceeded 130 ms when DMI sends EVC-101");

            DmiActions.Display_Fixed_Train_Data_Window(this);
            DmiActions.ShowInstruction(this, @"Perform the following actions on the DMI: " + Environment.NewLine +
                                             Environment.NewLine +
                                             "1. Enter FLU and confirm value in each input field." +
                                             Environment.NewLine +
                                             "2. Press OK on THIS window.");

            DmiActions.Enable_Fixed_Train_Data_Validation(this, Variables.Fixed_Trainset_Captions.FLU);
            DmiActions.ShowInstruction(this, @"Perform the following actions on the DMI: " + Environment.NewLine +
                                             Environment.NewLine +
                                             "1. Press ‘Yes’ button." + Environment.NewLine +
                                             "2. Press OK on THIS window.");

            DmiActions.Complete_Fixed_Train_Data_Entry(this);
            DmiActions.Display_Train_data_validation_Window(this);
            DmiActions.ShowInstruction(this, @"Perform the following actions on the DMI: " + Environment.NewLine +
                                             Environment.NewLine +
                                             "1. Press ‘Yes’ button." + Environment.NewLine +
                                             "2. Confirmed the selected value by pressing the input field." +
                                             Environment.NewLine +
                                             "3. Press OK on THIS window.");

            DmiActions.Display_TRN_Window(this);
            DmiActions.ShowInstruction(this, @"Perform the following actions on the DMI: " + Environment.NewLine +
                                             Environment.NewLine +
                                             "1. Enter and confirm Train Running Number." + Environment.NewLine +
                                             "2. Press OK on THIS window.");

            DmiActions.Display_Main_Window_with_Start_button_enabled(this);

            /*
            Test Step 3
            Action:
            	Verify response time of DMI on actuation of ‘Start’ button in ‘Main’ window
            Expected Result:
            	Use log file to verify that response time of DMI on actuation of the ‘Start’ buttons is not exceeded 130 ms when DMI sends EVC-101 with verification below:-
            	- In packet EVC-101 that variable [MMI_M_REQUEST = 9] and [MMI_Q_BUTTON = 0], the different time between time when ODL log receives this packet and timestamp in variable ‘MMI_T_BUTTONEVENT’ is not exceeded 130 ms. 
            	Note Use epoch & unix timestamp converter to convert timestamp recorded in variable ‘MMI_T_BUTTONEVENT’
            Test Step Comment:
            	MMI_gen 65 (partly: ‘Start’ button);
            */
            MakeTestStepHeader(3, 35831, "Verify response time of DMI on actuation of ‘Start’ button in ‘Main’ window",
                "Use log file to verify that response time of DMI on actuation of the ‘Start’ buttons is not exceeded 130 ms when DMI sends EVC-101 with verification below:-" +
                Environment.NewLine +
                "- In packet EVC-101 that variable [MMI_M_REQUEST = 9] and [MMI_Q_BUTTON = 0], the different time between time when ODL log receives this packet and timestamp in variable ‘MMI_T_BUTTONEVENT’ is not exceeded 130 ms. " +
                Environment.NewLine +
                "Note Use epoch & unix timestamp converter to convert timestamp recorded in variable ‘MMI_T_BUTTONEVENT’");

            DmiExpectedResults.Start_Button_Pressed(this);

            WaitForVerification("Verify the following:" + Environment.NewLine + Environment.NewLine +
                                "Actuation of the ‘Start’ buttons is not exceeded 130 ms when DMI sends EVC-101");

            DmiActions.Send_SR_Mode_Ack(this);
            DmiActions.ShowInstruction(this, @"Perform the following action after pressing OK: " + Environment.NewLine +
                                             Environment.NewLine +
                                             "1. Press DMI Sub Area C1.");
            DmiExpectedResults.SR_Mode_Ack_pressed_and_hold(this);

            DmiActions.Send_SR_Mode(this);
            DmiActions.Send_L1(this);
            DmiActions.Finished_SoM_Default_Window(this);

            /*
            Test Step 4
            Action:
            	Press ‘Data view’ button in sub area F3 and then press ‘Close’ button in ‘Data view’ window.
            Expected Result:
            	Use log file to verify that response time of DMI on actuation of the ‘Data view’ buttons is not exceeded 130 ms when DMI sends EVC-101 with verification below:-
            	- In packet EVC-101 that variable [MMI_M_REQUEST = 21] and [MMI_Q_BUTTON = 0], the different time between time when ODL log receives this packet and timestamp in variable ‘MMI_T_BUTTONEVENT’ is not exceeded 130 ms. 
            	Note Use epoch & unix timestamp converter to convert timestamp recorded in variable ‘MMI_T_BUTTONEVENT’
            Test Step Comment:
            	MMI_gen 65 (partly: ‘Data view’ button);
            */
            MakeTestStepHeader(4, 35832,
                "Press ‘Data view’ button in sub area F3 and then press ‘Close’ button in ‘Data view’ window." +
                Environment.NewLine + "",
                "Use log file to verify that response time of DMI on actuation of the ‘Data view’ buttons is not exceeded 130 ms when DMI sends EVC-101 with verification below:-" +
                Environment.NewLine +
                "- In packet EVC-101 that variable [MMI_M_REQUEST = 21] and [MMI_Q_BUTTON = 0], the different time between time when ODL log receives this packet and timestamp in variable ‘MMI_T_BUTTONEVENT’ is not exceeded 130 ms. " +
                Environment.NewLine +
                "Note Use epoch & unix timestamp converter to convert timestamp recorded in variable ‘MMI_T_BUTTONEVENT’");

            DmiExpectedResults.Data_View_Button_Pressed(this);
            DmiActions.Display_Data_View_Window(this);

            WaitForVerification("Verify the following:" + Environment.NewLine + Environment.NewLine +
                                "Actuation of the ‘Data view’ buttons is not exceeded 130 ms when DMI sends EVC-101");

            DmiActions.ShowInstruction(this, "Press Close Button.");

            /*
            Test Step 5
            Action:
            	Press SE04 symbol in sub area F5. 
            	Then press ‘System Version’ button in ‘Settings’ window
            Expected Result:
            	Use log file to verify that response time of DMI on actuation of the ‘System Version’ buttons is not exceeded 130 ms when DMI sends EVC-101 with verification below:-
            	- In packet EVC-101 that variable [MMI_M_REQUEST = 55] and [MMI_Q_BUTTON = 0], the different time between time when ODL log receives this packet and timestamp in variable ‘MMI_T_BUTTONEVENT’ is not exceeded 130 ms. 
            	Note Use epoch & unix timestamp converter to convert timestamp recorded in variable ‘MMI_T_BUTTONEVENT’
            Test Step Comment:
            	MMI_gen 65 (partly: ‘System Version’ button);
            */
            MakeTestStepHeader(5, 35833,
                "Press SE04 symbol in sub area F5. " + Environment.NewLine + "" + Environment.NewLine +
                "Then press ‘System Version’ button in ‘Settings’ window",
                "Use log file to verify that response time of DMI on actuation of the ‘System Version’ buttons is not exceeded 130 ms when DMI sends EVC-101 with verification below:-" +
                Environment.NewLine +
                "- In packet EVC-101 that variable [MMI_M_REQUEST = 55] and [MMI_Q_BUTTON = 0], the different time between time when ODL log receives this packet and timestamp in variable ‘MMI_T_BUTTONEVENT’ is not exceeded 130 ms. " +
                Environment.NewLine +
                "Note Use epoch & unix timestamp converter to convert timestamp recorded in variable ‘MMI_T_BUTTONEVENT’");

            DmiExpectedResults.Settings_Button_Pressed(this);
            DmiExpectedResults.DMI_displays_Settings_window(this);
            DmiExpectedResults.System_Version_Button_Pressed(this);

            WaitForVerification("Verify the following:" + Environment.NewLine + Environment.NewLine +
                                "Actuation of the ‘System Version’ buttons is not exceeded 130 ms when DMI sends EVC-101");

            /* End Of Test */
            TraceHeader("End Of Test");


            return GlobalTestResult;
        }
    }
}
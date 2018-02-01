namespace Testcase.DMITestCases
{
    /// <summary>
    /// 8.1 DMI language selection: Configurable at least eight langauges
    /// TC-ID: 3.1
    /// 
    /// This test case verifies the configurable of languages selection. DMI provides at least eight languages for driver selection. The selected languages effect to text labels on the buttons, fixed text messages, all text labels (e.g. Data Entry fields), texts strings used for messages to the driver and window headlines. The configurable shall comply with [MMI-ETCS-gen].
    /// 
    /// Tested Requirements:
    /// MMI_gen 63;
    /// 
    /// Scenario:
    /// Set  DEFAULT_LANGUAGE configuration as 1 (Deutsch language)Power on DMI. Activate cabin A. Then enter the Driver ID and perform brake test.Select and confirm level 1.After that press at sub-area F1 (‘Main menu’ button).Verify that all texts on the buttons, text messages and text labels are   displayed in Deutsch language. Close the window.Press sub-area F4 (‘Special menu’ button)Verify that all texts on the buttons, text messages and text labels are displayed in Deutsch language. Close the window.After that press at sub-area F5 (‘Settings menu’ button)Verify that all texts on the buttons, text messages and text labels are displayed in Deutsch language. Close the window.Power off DMI. Then set  DEFAULT_LANGUAGE configuration as 2 (Swedish language) and repeat the actions listed above. After that continue testing by setting DEFAULT_LANGUAGE configuration as 3 (Dutch language), 4 (Spanish language), 5 (Polish language), 6 (Hungarian Language) and 7 (Czech language) and repeat the actions listed above in the scenario.
    /// 
    /// Used files:
    /// N/A
    /// </summary>
    public class TC_3_1_DMI_language_selection : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // System is power off. Configure DEFAULT_LANGUAGE to 1 (Deutsch), 2 (Swedish language), 3 (Dutch language), 4 (Spanish language), 5 (Polish language), 6 (Hungarian Language) and 7 (Czech language)  See the instruction in Appendix 1.

            // Call the TestCaseBase PreExecution
            base.PreExecution();
            DmiActions.ShowInstruction(this,
                "Configure DEFAULT_LANGUAGE to: 1 (Deutsch), 2 (Swedish language), 3 (Dutch language), 4 (Spanish language), 5 (Polish language), 6 (Hungarian Language) and 7 (Czech language)");
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
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 0;
            // Testcase entrypoint


            TraceHeader("Test Step 1");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Power on DMI");
            TraceReport("Expected Result");
            TraceInfo("‘Driver’s desk not active’ is displayed on DMI in Deutsch language");
            /*
            Test Step 1
            Action: Power on DMI
            Expected Result: ‘Driver’s desk not active’ is displayed on DMI in Deutsch language
            */

            DmiActions.Start_ATP();
            WaitForVerification("‘Driver’s desk not active’ is displayed on DMI in Deutsch language");

            TraceHeader("Test Step 2");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Activate cabin A");
            TraceReport("Expected Result");
            TraceInfo("DMI displays the Driver ID window.");
            /*
            Test Step 2
            Action: Activate cabin A
            Expected Result: DMI displays the Driver ID window.
            Verify the following information,
            The window headline of the Driver ID window is displayed in Deutsch language
            Test Step Comment: (1) MMI_gen 63 (partly: Window headline);
            */

            DmiActions.Activate_Cabin_1(this);
            DmiActions.Set_Driver_ID(this, "1234");
            DmiActions.Send_SB_Mode(this);

            WaitForVerification("The window headline of the Driver ID window is displayed in Deutsch language");


            TraceHeader("Test Step 3");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Enter the Driver ID");
            TraceReport("Expected Result");
            TraceInfo("Verify the following information,");
            /*
            Test Step 3
            Action: Enter the Driver ID
            Expected Result: Verify the following information,
            DMI displays the acknowledgement message of perform brake test at area E in Deutsch language
            Test Step Comment: (1) MMI_gen 63 (partly: Fixed text messages, text strings use for messages to the driver);
            */

            DmiExpectedResults.Driver_ID_entered(this);
            DmiActions.Request_Brake_Test(this, 1);

            WaitForVerification(
                "DMI displays the acknowledgement message of perform brake test at area E in Deutsch language");


            TraceHeader("Test Step 4");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Acknowledge the message");
            TraceReport("Expected Result");
            TraceInfo("Verify the following information,");
            /*
            Test Step 4
            Action: Acknowledge the message
            Expected Result: Verify the following information,
            DMI displays the text message ‘Brake Test in progress’ in Deutsch language
            Test Step Comment: (1) MMI_gen 63 (partly: Fixed text messages, text strings use for messages to the driver);
            */

            DmiExpectedResults.Brake_Test_Perform_Order(this, true);
            DmiActions.Perform_Brake_Test(this, 2);

            WaitForVerification("DMI displays the text message ‘Brake Test in progress’ in Deutsch language");

            TraceHeader("Test Step 5");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Select and confirm level 1");
            TraceReport("Expected Result");
            TraceInfo("DMI displays in SB mode, level 1.");
            /*
            Test Step 5
            Action: Select and confirm level 1
            Expected Result: DMI displays in SB mode, level 1.
            Verify the following information,
            The buttons at sub-area F1-F4 is displayed in Deutsch language
            Test Step Comment: (1) MMI_gen 63 (partly: text labels displayed on buttons);
            */

            Wait_Realtime(5000);

            DmiActions.Display_Brake_Test_Successful(this, 3);

            DmiActions.Display_Level_Window(this);
            DmiExpectedResults.Level_window_displayed(this);

            DmiActions.Delete_Brake_Test_Successful(this, 3);

            WaitForVerification("The buttons at sub-area F1-F4 is displayed in Deutsch language");

            TraceHeader("Test Step 6");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Select all buttons in the Main menu window");
            TraceReport("Expected Result");
            TraceInfo("Verify the following information,");
            /*
            Test Step 6
            Action: Select all buttons in the Main menu window
            Expected Result: Verify the following information,
            Verify that all texts in the window headline, buttons, input fields, echo texts, text labels of input fields are displayed in Deutsch language
            Test Step Comment: (1) MMI_gen 63;
            */


            TraceHeader("Test Step 7");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Close ‘Main menu’ window and then press sub-area F4");
            TraceReport("Expected Result");
            TraceInfo("Verify the following information,The Special window is displayed in Deutsch language");
            /*
            Test Step 7
            Action: Close ‘Main menu’ window and then press sub-area F4
            Expected Result: Verify the following information,The Special window is displayed in Deutsch language
            Test Step Comment: (1) MMI_gen 63 (partly: all text labels);
            */


            TraceHeader("Test Step 8");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Select all buttons in the Special window");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following information,All texts in the window headline, buttons, input fields, echo texts, text labels of input fields are displayed in Deutsch language");
            /*
            Test Step 8
            Action: Select all buttons in the Special window
            Expected Result: Verify the following information,All texts in the window headline, buttons, input fields, echo texts, text labels of input fields are displayed in Deutsch language
            Test Step Comment: (1) MMI_gen 63;
            */


            TraceHeader("Test Step 9");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Close the Special window, then select  Settings menu button");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following information,The Settings window is displayed with the window headline in Deutsch language");
            /*
            Test Step 9
            Action: Close the Special window, then select  Settings menu button
            Expected Result: Verify the following information,The Settings window is displayed with the window headline in Deutsch language
            Test Step Comment: (1) MMI_gen 63 (partly: Window headline);
            */


            TraceHeader("Test Step 10");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Select all buttons in the Settings window");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following information,All texts in the window headline, buttons, input fields, echo texts, text labels of input fields are displayed in Deutsch language.The Language Selection window is displayed eight language buttons");
            /*
            Test Step 10
            Action: Select all buttons in the Settings window
            Expected Result: Verify the following information,All texts in the window headline, buttons, input fields, echo texts, text labels of input fields are displayed in Deutsch language.The Language Selection window is displayed eight language buttons
            Test Step Comment: (1) MMI_gen 63;
            */


            TraceHeader("Test Step 11");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Close the Settings window");
            TraceReport("Expected Result");
            TraceInfo("DMI displays the default window");
            /*
            Test Step 11
            Action: Close the Settings window
            Expected Result: DMI displays the default window
            */
            // Call generic Action Method
            DmiActions.Close_the_Settings_window(this);
            // Call generic Check Results Method
            DmiExpectedResults.DMI_displays_the_default_window(this);


            TraceHeader("Test Step 12");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Power off the DMI");
            
            /*
            Test Step 12
            Action: Power off the DMI
            Expected Result: 
            */


            TraceHeader("Test Step 13");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Set DEFAULT_LANGUAGE configuration as 2 (Swedish language). Repeat the actions following step 1-12");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following information,All texts in the window headline, buttons, input fields, echo texts, text labels of input fields are displayed in Swedish language");
            /*
            Test Step 13
            Action: Set DEFAULT_LANGUAGE configuration as 2 (Swedish language). Repeat the actions following step 1-12
            Expected Result: Verify the following information,All texts in the window headline, buttons, input fields, echo texts, text labels of input fields are displayed in Swedish language
            Test Step Comment: (1) MMI_gen 63;
            */


            TraceHeader("Test Step 14");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Set DEFAULT_LANGUAGE configuration as 3 (Dutch language). Repeat the actions following step 1-12");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following information,All texts in the window headline, buttons, input fields, echo texts, text labels of input fields are displayed in Dutch language");
            /*
            Test Step 14
            Action: Set DEFAULT_LANGUAGE configuration as 3 (Dutch language). Repeat the actions following step 1-12
            Expected Result: Verify the following information,All texts in the window headline, buttons, input fields, echo texts, text labels of input fields are displayed in Dutch language
            Test Step Comment: (1) MMI_gen 63;
            */


            TraceHeader("Test Step 15");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Set DEFAULT_LANGUAGE configuration as 4 (Spanish language). Repeat the actions following step 1-12");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following information,All texts in the window headline, buttons, input fields, echo texts, text labels of input fields are displayed in Spanish language");
            /*
            Test Step 15
            Action: Set DEFAULT_LANGUAGE configuration as 4 (Spanish language). Repeat the actions following step 1-12
            Expected Result: Verify the following information,All texts in the window headline, buttons, input fields, echo texts, text labels of input fields are displayed in Spanish language
            Test Step Comment: (1) MMI_gen 63;
            */


            TraceHeader("Test Step 16");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Set DEFAULT_LANGUAGE configuration as 5 (Polish language). Repeat the actions following step 1-12");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following information,All texts in the window headline, buttons, input fields, echo texts, text labels of input fields are displayed in Polish language");
            /*
            Test Step 16
            Action: Set DEFAULT_LANGUAGE configuration as 5 (Polish language). Repeat the actions following step 1-12
            Expected Result: Verify the following information,All texts in the window headline, buttons, input fields, echo texts, text labels of input fields are displayed in Polish language
            Test Step Comment: (1) MMI_gen 63;
            */


            TraceHeader("Test Step 17");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Set DEFAULT_LANGUAGE configuration as 6 (Hungarian language). Repeat the actions following step 1-12");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following information,All texts in the window headline, buttons, input fields, echo texts, text labels of input fields are displayed in Hungarian language");
            /*
            Test Step 17
            Action: Set DEFAULT_LANGUAGE configuration as 6 (Hungarian language). Repeat the actions following step 1-12
            Expected Result: Verify the following information,All texts in the window headline, buttons, input fields, echo texts, text labels of input fields are displayed in Hungarian language
            Test Step Comment: (1) MMI_gen 63;
            */


            TraceHeader("Test Step 18");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Set DEFAULT_LANGUAGE configuration as 7 (Czech language). Repeat the actions following step 1-12");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following information,All texts in the window headline, buttons, input fields, echo texts, text labels of input fields are displayed in Czech language");
            /*
            Test Step 18
            Action: Set DEFAULT_LANGUAGE configuration as 7 (Czech language). Repeat the actions following step 1-12
            Expected Result: Verify the following information,All texts in the window headline, buttons, input fields, echo texts, text labels of input fields are displayed in Czech language
            Test Step Comment: (1) MMI_gen 63;
            */


            TraceHeader("Test Step 19");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("End of test");
            
            /*
            Test Step 19
            Action: End of test
            Expected Result: 
            */


            return GlobalTestResult;
        }
    }
}
using System;
using BT_CSB_Tools;

namespace Testcase.DMITestCases
{
    /// <summary>
    /// Performance of the new selection language
    /// TC-ID: 1.3.1
    /// Doors unique ID: TP-35690
    /// This test case verifies the presentation of all predefined  texts with default language when DMI powered on and start up. DMI shall support and save entire new selection language which made by driver.
    /// 
    /// Tested Requirements:
    /// MMI_gen 60; MMI_gen 61; MMI_gen 62,MMI_gen 11892; MMI_gen 11893; MMI_gen 4368; MMI_gen 7506; MMI_gen 7507; MMI_gen 11470 (partly: Bit # 29); arn_043#4702;
    /// 
    /// Scenario:
    /// Power on the system
    /// Activate cabin A.
    /// Enter the Driver ID and perform brake test.
    /// Select and confirm level 1.
    /// Verify that the text on windows, sub windows, buttons and text messages are displayed accroding to selected language
    /// Power off DMI for 10 mins and power on again.
    /// Verify that the default window represents the text with the selected language
    /// Restest with all stored languages
    /// 
    /// Used files:
    /// N/A
    /// </summary>
    public class TC_ID_1_3_1_Performance_of_the_new_selection_language : TestcaseBase
    {
        public override void PreExecution()
        {
            /* Pre-conditions from TestSpec
            	System is powered off.
            	Default Language Is Engilsh
            */

            TraceInfo(
                "Pre-condition: " + "System is powered off." + Environment.NewLine + "Default Language Is English");

            base.PreExecution();
        }

        public override void PostExecution()
        {
            /* Post-conditions from TestSpec
            	DMI displays in SB mode.
            */

            TraceInfo("Post-condition: " + "DMI displays in SB mode.");

            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            throw new TestcaseException(
                "Before migration to v6 this test was commented out and marked as obsolete, to be reviewed (old code can be found in git)");
            /*
            Test Step 1
            Action:
            	Power on DMI and start up the system.
            Expected Result:
            	‘Driver’s desk not active’ is displayed on DMI
            */
            MakeTestStepHeader(1, 35707, "Power on DMI and start up the system." + Environment.NewLine + "",
                "" + Environment.NewLine + "‘Driver’s desk not active’ is displayed on DMI");

            /*
            Test Step 2
            Action:
            	Activate cabin A
            Expected Result:
            	The Driver ID window is displayed.
            	Time between step 1 and the display of the Driver ID window is <= 120 seconds
            Test Step Comment:
            	MMI_gen 60;
            */
            MakeTestStepHeader(2, 35708, "Activate cabin A",
                "The Driver ID window is displayed." + Environment.NewLine +
                "Time between step 1 and the display of the Driver ID window is <= 120 seconds");

            /*
            Test Step 3
            Action:
            	Perform the following procedure,
            	Enter Driver ID and perform brake test.
            	Select and confirm Level 1
            Expected Result:
            	The Main window is presented with the text message ‘Brake test in progress’ is displayed as English language.
            Test Step Comment:
            	MMI_gen 61 (partly: Engilsh);
            */
            MakeTestStepHeader(3, 35709,
                "Perform the following procedure," + Environment.NewLine + "Enter Driver ID and perform brake test." +
                Environment.NewLine + "Select and confirm Level 1",
                "The Main window is presented with the text message ‘Brake test in progress’ is displayed as English language." +
                Environment.NewLine + "");

            /*
            Test Step 4
            Action:
            	Enter all sub windows on DMI
            Expected Result:
            	The text on sub windows and buttons are presented in Engilsh language
            Test Step Comment:
            	MMI_gen 61 (partly: Engilsh);MMI_gen_11892 (partly: Engilsh);MMI_gen 4368 (partly: (partly: language dependent text));
            */
            MakeTestStepHeader(4, 35710, "Enter all sub windows on DMI",
                "The text on sub windows and buttons are presented in Engilsh language");

            /*
            Test Step 5
            Action:
            	Complete start of mission
            Expected Result:
            	The text on windows, sub windows buttons and text messages are presented in Engilsh language
            Test Step Comment:
            	MMI_gen 61 (partly: Engilsh);MMI_gen_11892 (partly: Engilsh);MMI_gen 4368 (partly: language dependent text);
            */
            MakeTestStepHeader(5, 35711, "Complete start of mission",
                "The text on windows, sub windows buttons and text messages are presented in Engilsh language");

            /*
            Test Step 6
            Action:
            	Power off DMI for 10 min and then power on again
            Expected Result:
            	The default window represents the objects in the language selected by the driver
            Test Step Comment:
            	MMI_gen 62;
            */
            MakeTestStepHeader(6, 35712, "Power off DMI for 10 min and then power on again",
                "The default window represents the objects in the language selected by the driver");

            /*
            Test Step 7
            Action:
            	Selects ‘Settings’ button
            	  SE 04
            Expected Result:
            	(1) The Settings window is displayed with all button with text and symbols
            	SE01  
            	SE02 
            	SE 03 
            	(2) The button with text is displayed 2 lines of text and truncated when the text exceeds the button’s width
            Test Step Comment:
            	(1) MMI_gen 4368 (partly:symbol);
            	(2) MMI_gen 7506;MMI_gen 7507;
            */
            MakeTestStepHeader(7, 35713, "Selects ‘Settings’ button" + Environment.NewLine + "  SE 04",
                "(1) The Settings window is displayed with all button with text and symbols" + Environment.NewLine +
                "SE01  " + Environment.NewLine + "SE02 " + Environment.NewLine + "SE 03 " + Environment.NewLine + "" +
                Environment.NewLine +
                "(2) The button with text is displayed 2 lines of text and truncated when the text exceeds the button’s width" +
                Environment.NewLine + "");

            /*
            Test Step 8
            Action:
            	Press ‘Language’ icon menu
            Expected Result:
            	The selection language window is displayed with all stored language data for driver to make a new selection.
            Test Step Comment:
            	MMI_gen 61 (partly:selected languages);MMI_gen 11893;
            */
            MakeTestStepHeader(8, 35714, "Press ‘Language’ icon menu",
                "The selection language window is displayed with all stored language data for driver to make a new selection." +
                Environment.NewLine + "" + Environment.NewLine + "");

            /*
            Test Step 9
            Action:
            	Change to all stored Languages and retest with step 3 to 7
            Expected Result:
            	Verify the following information,
            	(1)   The text on window, sub windows,  buttons and text messages are presented according to selected language.
            	(2)    Use the log file to confirm that DMI sends out packet [MMI_DRIVER_ACTION (EVC-152)] with the value of variable MMI_M_DRIVER_ACTION refer to sequence below,a)   MMI_M_DRIVER_ACTION = 29 (Selection of Language)
            Test Step Comment:
            	(1) MMI_gen 61 (partly:selected languages);MMI_gen 11892 (partly:selected languages);MMI_gen 4368 (partly:selected language); arn_043#4702;
            	(2) MMI_gen 11470 (partly: Bit # 29);
            */
            MakeTestStepHeader(9, 35715, "Change to all stored Languages and retest with step 3 to 7",
                "Verify the following information," + Environment.NewLine +
                "(1)   The text on window, sub windows,  buttons and text messages are presented according to selected language." +
                Environment.NewLine +
                "(2)    Use the log file to confirm that DMI sends out packet [MMI_DRIVER_ACTION (EVC-152)] with the value of variable MMI_M_DRIVER_ACTION refer to sequence below,a)   MMI_M_DRIVER_ACTION = 29 (Selection of Language)");

            /* End Of Test */
            TraceHeader("End Of Test");


            return GlobalTestResult;
        }
    }
}
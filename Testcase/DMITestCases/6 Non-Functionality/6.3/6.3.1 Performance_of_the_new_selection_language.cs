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
    /// 6.3.1 Performance of the new selection language
    /// TC-ID: 1.3.1
    /// 
    /// This test case verifies the presentation of all predefined  texts with default language when DMI powered on and start up. DMI shall support and save entire new selection language which made by driver.
    /// 
    /// Tested Requirements:
    /// MMI_gen 60; MMI_gen 61; MMI_gen 62,MMI_gen 11892; MMI_gen 11893; MMI_gen 4368; MMI_gen 7506; MMI_gen 7507; MMI_gen 11470 (partly: Bit # 29);
    /// 
    /// Scenario:
    /// Power on the systemActivate cabin A.Enter the Driver ID and perform brake test.Select and confirm level 1.Verify that the text on windows, sub windows, buttons and text messages are displayed accroding to selected languagePower off DMI for 10 mins and power on again.Verify that the default window represents the text with the selected languageRestest with all stored languages
    /// 
    /// Used files:
    /// N/A
    /// </summary>
    public class Performance_of_the_new_selection_language : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // System is powered off.Default Language Is Engilsh

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
            Action: Power on DMI and start up the system.
            Expected Result: ‘Driver’s desk not active’ is displayed on DMI. 
            */

            /*
            Test Step 2
            Action: Activate cabin A.
            Expected Result: The Driver ID window is displayed.Time between step 1 and the display of the Driver ID window is <= 120 seconds.
            Test Step Comment: MMI_gen 60;
            */

            /*
            Test Step 3
            Action: Perform the following procedure,Enter Driver ID and perform brake test.Select and confirm Level 1.
            Expected Result: The Main window is presented with the text message ‘Brake test in progress’ is displayed as English language.
            Test Step Comment: MMI_gen 61 (partly: Engilsh);
            */

            /*
            Test Step 4
            Action: Enter all sub windows on DMI
            Expected Result: The text on sub windows and buttons are presented in Engilsh language
            Test Step Comment: MMI_gen 61 (partly: Engilsh);MMI_gen_11892 (partly: Engilsh);MMI_gen 4368 (partly: (partly: language dependent text));
            */

            /*
            Test Step 5
            Action: Complete start of mission 
            Expected Result: The text on winsows, sub windows buttons and text messages are presented in Engilsh language
            Test Step Comment: MMI_gen 61 (partly: Engilsh);MMI_gen_11892 (partly: Engilsh);MMI_gen 4368 (partly: language dependent text);
            */

            /*
            Test Step 6
            Action: Power off DMI for 10 min and then power on again.
            Expected Result: The default window represents the objects in the language selected by the driver.
            Test Step Comment: MMI_gen 62;
            */

            /*
            Test Step 7
            Action: Selects ‘Settings’ button  SE 04
            Expected Result: (1) The Settings window is displayed with all button with text and symbolsSE01  SE02 SE 03 (2) The button with text is displayed 2 lines of text and truncated when the text exceeds the button’s width
            Test Step Comment: (1) MMI_gen 4368 (partly:symbol);(2) MMI_gen 7506;MMI_gen 7507;
            */

            /*
            Test Step 8
            Action: Press ‘Language’ icon menu
            Expected Result: The selection language window is displayed with all stored language data for driver to make a new selection.
            Test Step Comment: MMI_gen 61 (partly:selected languages);MMI_gen 11893;
            */

            /*
            Test Step 9
            Action: Change to all stored languges and retest with step 3 to 7
            Expected Result: Verify the following information,(1)   The text on window, sub windows,  buttons and text messages are presented according to selected language.(2)    Use the log file to confirm that DMI sends out packet [MMI_DRIVER_ACTION (EVC-152)] with the value of variable MMI_M_DRIVER_ACTION refer to sequence below,a)   MMI_M_DRIVER_ACTION = 29 (Selection of Language)
            Test Step Comment: (1) MMI_gen 61 (partly:selected languages);MMI_gen 11892 (partly:selected languages);MMI_gen 4368 (partly:selected language);(2) MMI_gen 11470 (partly: Bit # 29);
            */

            /*
            Test Step 10
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
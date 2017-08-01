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
    /// 6.6 Adjustment of Sound Volume
    /// TC-ID: 1.6
    /// 
    /// This test case verifies the configuration of the default and minimum volume level and the volume adjustment of DMI acoustic. The adjustment and the presentation of the Volume window shall comply with [ERA] standard and [MMI-ETCS-gen].
    /// 
    /// Tested Requirements:
    /// MMI_gen 257; MMI_gen 1687; MMI_gen 3093; MMI_gen 3094;
    /// 
    /// Scenario:
    /// 1.Activate cabin A. Then, press the icon of ‘Settings menu’ button.
    /// 2.Press the icon of ‘Volume’ button. 
    /// 3.Press and hold the button ‘-‘ to decrease the acoustic volume until minimum leve.
    /// 4.Simulate the communication loss between ETCS Onboard and DMI. Then, verifies the ATP down alarm sound volume is always play as maximum volume 
    /// 5.Driver deactivates cabin A.
    /// 6.Re-test again for the volume increse by using ‘+’ button instead. 
    /// 
    /// Used files:
    /// N/A
    /// </summary>
    public class Adjustment_of_Sound_Volume : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // Configure the tags in configuration file, DEFAULT_VOLUME = 70 and IN_VOLUME = 10., See the instruction in the Appendix 1.                                                                                        DMI is power on.

            // Call the TestCaseBase PreExecution
            base.PreExecution();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // Cabin A is deactivated.

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint


            /*
            Test Step 1
            Action: Activate cabin A
            Expected Result: DMI displays Driver ID window
            */
            // Call generic Action Method
            DmiActions.Activate_cabin_A(this);
            // Call generic Check Results Method
            DmiExpectedResults.DMI_displays_Driver_ID_window(this);


            /*
            Test Step 2
            Action: Press ‘Settings’ button
            Expected Result: The Settings window is presented with all sub-menus
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(@"Press ‘Settings’ button");
            // Call generic Check Results Method
            DmiExpectedResults.The_Settings_window_is_presented_with_all_sub_menus(this);


            /*
            Test Step 3
            Action: Press ‘Volume’ button
            Expected Result: Verify the following information,The Volume window is presented to the driver to adjust the DMI acoustic and the Volume window is displayed with the default volume as 70
            Test Step Comment: (1) MMI_gen 3094 (partly: 1st bullet);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(@"Press ‘Volume’ button");


            /*
            Test Step 4
            Action: Adjust acoustic volume by press and hold ‘-‘ button to the minimum level in order to decrease the acoustic volume
            Expected Result: The adjusted acoustic volume is used by DMI.Verify that the minimum level of the volume is 10, as defined in the precondition and the ‘Click’ sound is lower and lower (never quiet)
            Test Step Comment: (1) MMI_gen 257 (partly: on adjusting);     MMI_gen 1687;    MMI_gen 3093 (partly: sound ‘Click’, on adjusting); MMI_gen 3094 (partly: 2nd bullet); 
            */


            /*
            Test Step 5
            Action: Press an input field to confirm adjusted volume
            Expected Result: The Settings window is displayed. Verify that the acoustic sound from driver clicking button remains as a minimum level that was adjusted
            Test Step Comment: (1) MMI_gen 257 (partly: adjusted (saved));               MMI_gen 3093 (partly: sound ‘Click’);     
            */


            /*
            Test Step 6
            Action: Close the Settings window
            Expected Result: Verify the following information,The acoustic sound from driver clicking button remains as a minimum level that was adjusted
            Test Step Comment: (1) MMI_gen 257 (partly: adjusted (after saved));               MMI_gen 3093 (partly: sound ‘Click’);     
            */
            // Call generic Action Method
            DmiActions.Close_the_Settings_window(this);


            /*
            Test Step 7
            Action: Simulate the communication lost between ETCS Onboard and DMI by unplugging the MVB cable
            Expected Result: DMI displays the message ‘ATP Down Alarm’ with sound alarm.Verify that the driver can not adjust the acoustic sound volume for the ATP down alarm. The sound is played at maximum volume 100%. (Louder than the ‘Click’ sound at volume level 10)
            Test Step Comment: (1) MMI_gen 3093 (partly: ATP-Down alarm, non-adjustable);  
            */


            /*
            Test Step 8
            Action: Re-establish the communication between ETCS onboard and DMI.Then, deactivates cabin A
            Expected Result: Cabin A is deactivated
            */
            // Call generic Check Results Method
            DmiExpectedResults.Cabin_A_is_deactivated(this);


            /*
            Test Step 9
            Action: Repeat step 1-8 with press and hold ‘+’ button to the maximum level instead
            Expected Result: Verify the following points,The maximum level of volume is 100 and and the ‘Click’ sound is louder and louder.When ‘Close’ button is pressed, sound from driver clicking button remain as a maximum level instead
            Test Step Comment: (1) MMI_gen 3094 (partly: recommended range 0..100 percentage) MMI_gen 257 (partly: driver’s adjustment of the volume);               MMI_gen 3093 (partly: sound ‘Click’);(2) MMI_gen 257 (partly: adjusted (after saved));               MMI_gen 3093 (partly: sound ‘Click’);     
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
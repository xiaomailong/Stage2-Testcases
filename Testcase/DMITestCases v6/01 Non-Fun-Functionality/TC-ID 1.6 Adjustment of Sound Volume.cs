using System;

namespace Testcase.DMITestCases
{
    /// <summary>
    /// Adjustment of Sound Volume
    /// TC-ID: 1.6
    /// Doors unique ID: TP-35718
    /// This test case verifies the configuration of the default and minimum volume level and the volume adjustment of DMI acoustic. The adjustment and the presentation of the Volume window shall comply with [ERA] standard and [MMI-ETCS-gen].
    /// 
    /// Tested Requirements:
    /// MMI_gen 257; MMI_gen 1687; MMI_gen 3093; MMI_gen 3094;
    /// 
    /// Scenario:
    /// 1. Activate cabin A. Then, press the icon of ‘Settings menu’ button.
    /// 2. Press the icon of ‘Volume’ button. 
    /// 3. Press and hold the button ‘-‘ to decrease the acoustic volume until minimum leve.
    /// 4. Simulate the communication loss between ETCS Onboard and DMI. Then, verifies the ATP down alarm sound volume is always play as maximum volume 
    /// 5. Driver deactivates cabin A.
    /// 6. Re-test again for the volume increse by using ‘+’ button instead. 
    /// 
    /// Used files:
    /// N/A
    /// </summary>
    public class TC_ID_1_6_Adjustment_of_Sound_Volume : TestcaseBase
    {
        public override void PreExecution()
        {
            /* Pre-conditions from TestSpec
            	Configure the tags in configuration file,  MIN_VOLUME = 20., 
            	See the instruction in the Appendix 1.                                                                                        DMI is power on.
            */

            TraceInfo("Pre-condition: " + "Configure the tags in configuration file,  MIN_VOLUME = 20., " +
                      Environment.NewLine +
                      "See the instruction in the Appendix 1.                                                                                        DMI is power on.");

            base.PreExecution();
        }

        public override void PostExecution()
        {
            /* Post-conditions from TestSpec
            	Cabin A is deactivated.
            */

            TraceInfo("Post-condition: " + "Cabin A is deactivated.");

            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            /*
            Test Step 1
            Action:
            	Activate cabin A
            Expected Result:
            	DMI displays Driver ID window
            */
            MakeTestStepHeader(1, 35733, "Activate cabin A", "DMI displays Driver ID window");

            StartUp();
            DmiActions.Display_Driver_ID_Window(this, "1234");

            DmiExpectedResults.Driver_ID_window_displayed(this);

            /*
            Test Step 2
            Action:
            	Press ‘Settings’ button
            Expected Result:
            	The Settings window is presented with all sub-menus
            */
            MakeTestStepHeader(2, 35734, "Press ‘Settings’ button",
                "The Settings window is presented with all sub-menus");

            DmiActions.ShowInstruction(this, @"Press ‘Settings’ button");
            DmiActions.Open_the_Settings_window(this);

            DmiExpectedResults.DMI_displays_Settings_window(this);

            /*
            Test Step 3
            Action:
            	Press ‘Volume’ button
            Expected Result:
            	Verify the following information,
            	The Volume window is presented to the driver to adjust the DMI acoustic and the Volume window is displayed with value of an input field is 60 (median value of 20 and 100)
            Test Step Comment:
            	(1) MMI_gen 3094 (partly: median value used as default volume);
            */
            MakeTestStepHeader(3, 35735, "Press ‘Volume’ button",
                "Verify the following information," + Environment.NewLine +
                "The Volume window is presented to the driver to adjust the DMI acoustic and the Volume window is displayed with value of an input field is 60 (median value of 20 and 100)");

            DmiActions.ShowInstruction(this, @"Press ‘Volume’ button");
            WaitForVerification(
                "The Volume window is presented to the driver to adjust the DMI acoustic, the Volume window is displayed, and the value of the input field is 60 (median value of 20 and 100)");


            /*
            Test Step 4
            Action:
            	Adjust acoustic volume by press and hold ‘-‘ button to the minimum level in order to decrease the acoustic volume
            Expected Result:
            	The adjusted acoustic volume is used by DMI.
            	Verify that the minimum level of the volume is 20, as defined in the precondition and the ‘Click’ sound is lower and lower (never quiet)
            Test Step Comment:
            	(1) MMI_gen 257 (partly: on adjusting);     MMI_gen 1687;    MMI_gen 3093 (partly: sound ‘Click’, on adjusting); MMI_gen 3094 (partly: configurable minimum volume); 
            */
            MakeTestStepHeader(4, 35736,
                "Adjust acoustic volume by press and hold ‘-‘ button to the minimum level in order to decrease the acoustic volume",
                "The adjusted acoustic volume is used by DMI." + Environment.NewLine +
                "Verify that the minimum level of the volume is 20, as defined in the precondition and the ‘Click’ sound is lower and lower (never quiet)");

            DmiActions.ShowInstruction(this,
                @"Adjust acoustic volume by press and hold ‘-‘ button to the minimum level in order to decrease the acoustic volume");
            WaitForVerification("The adjusted acoustic volume is used by DMI." + Environment.NewLine +
                                "Verify that the minimum level of the volume is 20, as defined in the precondition and the ‘Click’ sound is lower and lower(never quiet)");


            /*
            Test Step 5
            Action:
            	Press an input field to confirm adjusted volume
            Expected Result:
            	The Settings window is displayed. 
            	Verify that the acoustic sound from driver clicking button remains as a minimum level that was adjusted
            Test Step Comment:
            	(1) MMI_gen 257 (partly: adjusted (saved));               MMI_gen 3093 (partly: sound ‘Click’);     
            */
            MakeTestStepHeader(5, 35737, "Press an input field to confirm adjusted volume",
                "The Settings window is displayed. " + Environment.NewLine +
                "Verify that the acoustic sound from driver clicking button remains as a minimum level that was adjusted");

            DmiActions.ShowInstruction(this, @"Press an input field to confirm adjusted volume");
            DmiActions.Open_the_Settings_window(this);

            DmiExpectedResults.DMI_displays_Settings_window(this);
            WaitForVerification(
                "Verify that the acoustic sound from driver clicking button remains as a minimum level that was adjusted.");

            /*
            Test Step 6
            Action:
            	Close the Settings window
            Expected Result:
            	Verify the following information,
            	The acoustic sound from driver clicking button remains as a minimum level that was adjusted
            Test Step Comment:
            	(1) MMI_gen 257 (partly: adjusted (after saved));               MMI_gen 3093 (partly: sound ‘Click’);     
            */
            MakeTestStepHeader(6, 35738, "Close the Settings window",
                "Verify the following information," + Environment.NewLine +
                "The acoustic sound from driver clicking button remains as a minimum level that was adjusted");

            DmiActions.ShowInstruction(this, @"Press ‘Close’ button");
            WaitForVerification(
                "The acoustic sound from driver clicking button remains as a minimum level that was adjusted");

            /*
            Test Step 7
            Action:
            	Press ‘Volume’ button.
            	Then, press and hold ‘+’ button in order to increase the acoustic volume without confirmation
            Expected Result:
            	The acoustic sound is increase according to the entered data
            */
            MakeTestStepHeader(7, 35739,
                "Press ‘Volume’ button." + Environment.NewLine +
                "Then, press and hold ‘+’ button in order to increase the acoustic volume without confirmation",
                "The acoustic sound is increase according to the entered data");

            DmiActions.ShowInstruction(this,
                "Press the ‘Volume’ button" + Environment.NewLine +
                "Then press and hold the '+' button in order to increase the acoustic volume without confirmation");
            WaitForVerification("The acoustic sound is increased according to the entered data");

            /*
            Test Step 8
            Action:
            	Close the Settings window
            Expected Result:
            	Verify the following information,
            	(1)   The acoustic sound is set back to minimum level due to an entered data is still not saved by driver
            Test Step Comment:
            	MMI_gen 257 (partly: NEGATIVE, adjusted (not saved));               
            */
            MakeTestStepHeader(8, 35740, "Close the Settings window",
                "Verify the following information," + Environment.NewLine +
                "(1)   The acoustic sound is set back to minimum level due to an entered data is still not saved by driver");

            DmiActions.ShowInstruction(this, "Close the Settings Window");
            WaitForVerification(
                "Is the volume set back to the minumum level? (due to the entered data not being saved by the driver)");

            /*
            Test Step 9
            Action:
            	Simulate the communication lost between ETCS Onboard and DMI by unplugging the MVB cable
            Expected Result:
            	DMI displays the message ‘ATP Down Alarm’ with sound alarm.
            	Verify that the driver can not adjust the acoustic sound volume for the ATP down alarm. The sound is played at maximum volume 100%. (Louder than the ‘Click’ sound at volume level 10)
            Test Step Comment:
            	(1) MMI_gen 3093 (partly: ATP-Down alarm, non-adjustable);  
            */
            MakeTestStepHeader(9, 35741,
                "Simulate the communication lost between ETCS Onboard and DMI by unplugging the MVB cable",
                "DMI displays the message ‘ATP Down Alarm’ with sound alarm." + Environment.NewLine +
                "Verify that the driver can not adjust the acoustic sound volume for the ATP down alarm. The sound is played at maximum volume 100%. (Louder than the ‘Click’ sound at volume level 10)");

            DmiActions.Force_Loss_Communication(this);
            WaitForVerification("Is the DMI displaying the message ‘ATP Down Alarm’ with sound alarm.");
            WaitForVerification("Is the sound played at maximum volume 100%");

            /*
            Test Step 10
            Action:
            	Re-establish the communication between ETCS onboard and DMI.
            	Then, deactivates cabin A
            Expected Result:
            	Cabin A is deactivated
            */
            MakeTestStepHeader(10, 35742,
                "Re-establish the communication between ETCS onboard and DMI." + Environment.NewLine +
                "Then, deactivates cabin A", "Cabin A is deactivated");

            DmiActions.Restablish_Communication(this);
            DmiExpectedResults.Cab_deactivated(this);

            /*
            Test Step 11
            Action:
            	Repeat step 1-8 with press and hold ‘+’ button to the maximum level instead
            Expected Result:
            	Verify the following points,
            	The maximum level of volume is 100 and and the ‘Click’ sound is louder and louder.
            	When ‘Close’ button is pressed, sound from driver clicking button remain as a maximum level instead
            Test Step Comment:
            	(1) MMI_gen 3094 (partly: recommended range 0..100 percentage) MMI_gen 257 (partly: driver’s adjustment of the volume);               MMI_gen 3093 (partly: sound ‘Click’);
            	(2) MMI_gen 257 (partly: adjusted (after saved));               MMI_gen 3093 (partly: sound ‘Click’);     
            */
            MakeTestStepHeader(11, 35743, "Repeat step 1-8 with press and hold ‘+’ button to the maximum level instead",
                "Verify the following points," + Environment.NewLine +
                "The maximum level of volume is 100 and and the ‘Click’ sound is louder and louder." +
                Environment.NewLine +
                "When ‘Close’ button is pressed, sound from driver clicking button remain as a maximum level instead");

            WaitForVerification(
                "Go back to the volume settings window and press and hold the '+' button til the maximum level has been achieved" +
                Environment.NewLine +
                "Is the maximum value of the volume 100 and does the click sound become louder and louder as it increases?" +
                Environment.NewLine +
                "When the close button is pressed, button click sounds remain at the maximum level");
            // TODO this step doesn't actually say to press the input field but close, does that mean the volume will go back to previous value and this test is broken? Or have I understood the function wrong? / JS


            /*
            Test Step 12
            Action:
            	Perform the following procedure,
            	Power off DMI
            	Re-configure the minimum volume ‘MIN_VOLUME’ to ‘0’
            	Power on DMI
            	Open ‘Volume’ window
            Expected Result:
            	The value of an input field is still be 100 same as the last stored volume
            Test Step Comment:
            	MMI_gen 3094 (partly: last stored volume).
            */
            MakeTestStepHeader(12, 35744,
                "Perform the following procedure," + Environment.NewLine + "Power off DMI" + Environment.NewLine +
                "Re-configure the minimum volume ‘MIN_VOLUME’ to ‘0’" + Environment.NewLine + "Power on DMI" +
                Environment.NewLine + "Open ‘Volume’ window",
                "The value of an input field is still be 100 same as the last stored volume");

            DmiActions.ShowInstruction(this,
                "Reconfigure the minimum volume of the DMI to '0' (not sure how, use the force), and reboot it");
            StartUp();
            WaitForVerification("Open the volume window, is the volume still 100?");

            /*
            Test Step 13
            Action:
            	Adjust acoustic volume by press and hold ‘-‘ button to the minimum level in order to decrease the acoustic volume
            Expected Result:
            	The adjusted acoustic volume is used by DMI.
            	(1)    Verify that the minimum level of the volume is 0, as defined in the precondition and the ‘Click’ sound is lower and quite when the value is ‘0’
            Test Step Comment:
            	(1) MMI_gen 3094 (partly: configurable minimum volume, ‘0’);
            */
            MakeTestStepHeader(13, 35745,
                "Adjust acoustic volume by press and hold ‘-‘ button to the minimum level in order to decrease the acoustic volume",
                "The adjusted acoustic volume is used by DMI." + Environment.NewLine +
                "(1)    Verify that the minimum level of the volume is 0, as defined in the precondition and the ‘Click’ sound is lower and quite when the value is ‘0’");

            WaitForVerification(
                "Adjust the volume by pressing and holding the '-' buttong til the minimum level is achieved" +
                Environment.NewLine +
                "Verify that the minimum level of the volume is 0, as defined in the precondition and the 'Click' sound is lower and quiet when the value is '0'");

            /* End Of Test */
            TraceHeader("End Of Test");


            return GlobalTestResult;
        }
    }
}
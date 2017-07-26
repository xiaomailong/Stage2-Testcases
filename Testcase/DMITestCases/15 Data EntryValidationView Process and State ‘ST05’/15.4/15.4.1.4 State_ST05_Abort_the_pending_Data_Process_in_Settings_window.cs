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
    /// 15.4.1.4 State ‘ST05’: Abort the pending Data Process in Settings window
    /// TC-ID: 10.4.1.4
    /// 
    /// This test case verifies that the process of data entry and validation window in state ST05 is aborted by a received packet of different window type (i.e., data view window) from ETCS onboard.
    /// 
    /// Tested Requirements:
    /// MMI_gen 5507 (partly: Settings window, abort an already pending data entry and validation processes, received packet of different window from ETCS onboard);
    /// 
    /// Scenario:
    /// 1.Verify the display information when execute the test script files when open the windows below,Maintenance password windowWheel diameter windowWheel diameter validation windowRadar windowRadar validation windowLanguage windowVolume windowBrightness windowSet VBC windowSet VBC validation windowRemove VBC windowRemove VBC validation windowBrake percentage windowBrake percentage validation window
    /// 
    /// Used files:
    /// 10_4_1_4_a.xml, 10_4_1_4_b.xml
    /// </summary>
    public class State_ST05_Abort_the_pending_Data_Process_in_Settings_window : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // The ‘ATC-2’ level is configured in ATP-CU.NID_NTC_Installed = 22PB_SAFETY_LEVEL = 2NTC_HW_ADDR = 92NID_NTC_Default = 22 Note: (M_InstalledLevels and M_DefaultLevels have to be updated according to the number of enabling NTC/STM levels, by bitmasks)Test system is powered on with STM ATC-2 is started in ‘CO’ stateCabin is activeSettings window is openedMaintenance window is opened

            // Call the TestCaseBase PreExecution
            base.PreExecution();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in SB mode

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint


            /*
            Test Step 1
            Action: At Maintenance password window, use the test script file 10_4_1_4_a.xml to send EVC-8 withMMI_Q_TEXT_CRITERIA = 3 MMI_Q_TEXT = 716
            Expected Result: The hourglass symbol ST05 is displayed at window title area
            */

            /*
            Test Step 2
            Action: Use the test script file 10_4_1_4_b.xml to send EVC-24 withMMI_NID_ENGINE_1 = 1234MMI_M_BRAKE_CONFIG = 55MMI_M_AVAIL_SERVICES = 65535MMI_M_ETC_VER = 16755215
            Expected Result: Verify the followin information,(1)     The Maintenance password window is closed, DMI displays System info window after received packet EVC-24.
            Test Step Comment: (1) MMI_gen 5507 (partly: Maintenance password window, abort an already pending data entry process, received packet of different window from ETCS onboard);
            */

            /*
            Test Step 3
            Action: Perform the following procedure,At System info window, press ‘close’ button.Open Wheel diameter windowRepeat action step 1-2.
            Expected Result: Verify the followin information,(1)    The Wheel diameter Number window is closed, DMI displays System info window after received packet EVC-24.
            Test Step Comment: (1) MMI_gen 5507 (partly: Wheel diameter window, abort an already pending data entry process, received packet of different window from ETCS onboard);
            */

            /*
            Test Step 4
            Action: Perform the following procedure,At System info window, press ‘close’ button.Open Wheel diameter validation windowRepeat action step 1-2.
            Expected Result: Verify the followin information,(1)    The Wheel diameter validation window is closed, DMI displays System info window after received packet EVC-24.
            Test Step Comment: (1) MMI_gen 5507 (partly: Wheel diameter validation window, abort an already pending data validation process, received packet of different window from ETCS onboard);
            */

            /*
            Test Step 5
            Action: Perform the following procedure,At System info window, press ‘close’ button.Open Radar windowRepeat action step 1-2.
            Expected Result: Verify the followin information,(1)    The Radar Number window is closed, DMI displays System info window after received packet EVC-24.
            Test Step Comment: (1) MMI_gen 5507 (partly: Radar window, abort an already pending data entry process, received packet of different window from ETCS onboard);
            */

            /*
            Test Step 6
            Action: Perform the following procedure,At System info window, press ‘close’ button.Open Radar validation windowRepeat action step 1-2.
            Expected Result: Verify the followin information,(1)    The Radar validation window is closed, DMI displays System info window after received packet EVC-24.
            Test Step Comment: (1) MMI_gen 5507 (partly: Radar validation window, abort an already pending data validation process, received packet of different window from ETCS onboard);
            */

            /*
            Test Step 7
            Action: Perform the following procedure,At System info window, press ‘close’ button.Open Language windowRepeat action step 1-2.
            Expected Result: Verify the followin information,(1)    The Language window is closed, DMI displays System info window after received packet EVC-24.
            Test Step Comment: (1) MMI_gen 5507 (partly: Language window, abort an already pending data entry process, received packet of different window from ETCS onboard);
            */

            /*
            Test Step 8
            Action: Perform the following procedure,At System info window, press ‘close’ button.Open Volume windowRepeat action step 1-2.
            Expected Result: Verify the followin information,(1)    The Volume window is closed, DMI displays System info window after received packet EVC-24.
            Test Step Comment: (1) MMI_gen 5507 (partly: Volume window, abort an already pending data entry process, received packet of different window from ETCS onboard);
            */

            /*
            Test Step 9
            Action: Perform the following procedure,At System info window, press ‘close’ button.Open Brightness windowRepeat action step 1-2.
            Expected Result: Verify the followin information,(1)    The Brightness window is closed, DMI displays System info window after received packet EVC-24.
            Test Step Comment: (1) MMI_gen 5507 (partly: Brightness window, abort an already pending data entry process, received packet of different window from ETCS onboard);
            */

            /*
            Test Step 10
            Action: Perform the following procedure,At System info window, press ‘close’ button.Open Set VBC windowRepeat action step 1-2.
            Expected Result: Verify the followin information,(1)    The Set VBC window is closed, DMI displays System info window after received packet EVC-24.
            Test Step Comment: (1) MMI_gen 5507 (partly: Set VBC window, abort an already pending data entry process, received packet of different window from ETCS onboard);
            */

            /*
            Test Step 11
            Action: Perform the following procedure,At System info window, press ‘close’ button.Open Set VBC validation windowRepeat action step 1-2.
            Expected Result: Verify the followin information,(1)    The Set VBC validation window is closed, DMI displays System info window after received packet EVC-24.
            Test Step Comment: (1) MMI_gen 5507 (partly: Set VBC validation window, abort an already pending data validation process, received packet of different window from ETCS onboard);
            */

            /*
            Test Step 12
            Action: Perform the following procedure,At System info window, press ‘close’ button.Open Remove VBC windowRepeat action step 1-2.
            Expected Result: Verify the followin information,(1)    The Remove VBC window is closed, DMI displays System info window after received packet EVC-24.
            Test Step Comment: (1) MMI_gen 5507 (partly: Remove VBC window, abort an already pending data entry process, received packet of different window from ETCS onboard);
            */

            /*
            Test Step 13
            Action: Perform the following procedure,At System info window, press ‘close’ button.Open Remove VBC validation windowRepeat action step 1-2.
            Expected Result: Verify the followin information,(1)    The Remove VBC validation window is closed, DMI displays System info window after received packet EVC-24.
            Test Step Comment: (1) MMI_gen 5507 (partly: Remove VBC validation window, abort an already pending data validation process, received packet of different window from ETCS onboard);
            */

            /*
            Test Step 14
            Action: Perform the following procedure,At System info window, press ‘close’ button.Open Brake Percentage windowRepeat action step 1-2.
            Expected Result: Verify the followin information,(1)    The Brake Percentage window is closed, DMI displays System info window after received packet EVC-24.
            Test Step Comment: (1) MMI_gen 5507 (partly: Brake Percentage window, abort an already pending data entry process, received packet of different window from ETCS onboard);
            */

            /*
            Test Step 15
            Action: Perform the following procedure,At System info window, press ‘close’ button.Open Brake Percentage validation windowRepeat action step 1-2.
            Expected Result: Verify the followin information,(1)    The Brake Percentage validation window is closed, DMI displays System info window after received packet EVC-24.
            Test Step Comment: (1) MMI_gen 5507 (partly: Brake Percentage validation window, abort an already pending data validation process, received packet of different window from ETCS onboard);
            */

            /*
            Test Step 16
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}
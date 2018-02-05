using System;
using Testcase.Telegrams.EVCtoDMI;

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
    public class TC_ID_10_4_1_4_State_ST05 : TestcaseBase
    {

        public override bool TestcaseEntryPoint()
        {
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 0;

            StartUp();

            // Testcase entrypoint
            TraceInfo("This test case requires an ATP configuration change - " +
                      "See Precondition requirements. If this is not done manually, the test may fail!");

            MakeTestStepHeader(1, UniqueIdentifier++,
                "At Maintenance password window, use the test script file 10_4_1_4_a.xml to send EVC-8 withMMI_Q_TEXT_CRITERIA = 3 MMI_Q_TEXT = 716",
                "The hourglass symbol ST05 is displayed at window title area");
            /*
            Test Step 1
            Action: At Maintenance password window, use the test script file 10_4_1_4_a.xml to send EVC-8 withMMI_Q_TEXT_CRITERIA = 3 MMI_Q_TEXT = 716
            Expected Result: The hourglass symbol ST05 is displayed at window title area
            */
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Default;
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH =
                EVC30_MMIRequestEnable.EnabledRequests.EnableWheelDiameter;
            EVC30_MMIRequestEnable.Send();

            DmiActions.ShowInstruction(this, @"Press the ‘Settings’ button, then press the ‘Maintenance’ button");

            XML_10_4_1_4_a_b(msgType.typea);
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The hourglass symbol ST05 is displayed in the window title area.");

            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 4;
            EVC8_MMIDriverMessage.Send();

            DmiActions.ShowInstruction(this,
                @"Enter the maintenance password (as in the system configuration) and confirm");

            MakeTestStepHeader(2, UniqueIdentifier++,
                "Use the test script file 10_4_1_4_b.xml to send EVC-24 withMMI_NID_ENGINE_1 = 1234MMI_M_BRAKE_CONFIG = 55MMI_M_AVAIL_SERVICES = 65535MMI_M_ETC_VER = 16755215",
                "Verify the followin information,(1)     The Maintenance password window is closed, DMI displays System info window after received packet EVC-24");
            /*
            Test Step 2
            Action: Use the test script file 10_4_1_4_b.xml to send EVC-24 withMMI_NID_ENGINE_1 = 1234MMI_M_BRAKE_CONFIG = 55MMI_M_AVAIL_SERVICES = 65535MMI_M_ETC_VER = 16755215
            Expected Result: Verify the followin information,(1)     The Maintenance password window is closed, DMI displays System info window after received packet EVC-24
            Test Step Comment: (1) MMI_gen 5507 (partly: Maintenance password window, abort an already pending data entry process, received packet of different window from ETCS onboard);
            */
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.No_window_specified;
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH =
                EVC30_MMIRequestEnable.EnabledRequests.EnableWheelDiameter;
            EVC30_MMIRequestEnable.Send();

            XML_10_4_1_4_a_b(msgType.typeb);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The Maintenance password window is closed and DMI displays the System info window.");

            MakeTestStepHeader(3, UniqueIdentifier++,
                "Perform the following procedure,At System info window, press ‘close’ button.Open Wheel diameter windowRepeat action step 1-2",
                "Verify the followin information,(1)    The Wheel diameter Number window is closed, DMI displays System info window after received packet EVC-24");
            /*
            Test Step 3
            Action: Perform the following procedure,At System info window, press ‘close’ button.Open Wheel diameter windowRepeat action step 1-2
            Expected Result: Verify the followin information,(1)    The Wheel diameter Number window is closed, DMI displays System info window after received packet EVC-24
            Test Step Comment: (1) MMI_gen 5507 (partly: Wheel diameter window, abort an already pending data entry process, received packet of different window from ETCS onboard);
            */
            DmiActions.ShowInstruction(this,
                @"Press the ‘Close’ button in the System info window. Open the Wheel diameter window");

            EVC40_MMICurrentMaintenanceData.MMI_M_SDU_WHEEL_SIZE_1 = (Variables.MMI_M_SDU_WHEEL_SIZE) 100;
            EVC40_MMICurrentMaintenanceData.MMI_M_SDU_WHEEL_SIZE_2 = (Variables.MMI_M_SDU_WHEEL_SIZE) 40;
            EVC40_MMICurrentMaintenanceData.MMI_M_WHEEL_SIZE_ERR = (Variables.MMI_M_WHEEL_SIZE_ERR) 1;
            EVC40_MMICurrentMaintenanceData.Send();

            XML_10_4_1_4_a_b(msgType.typea);
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The hourglass symbol ST05 is displayed in the window title area.");

            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 4;
            EVC8_MMIDriverMessage.Send();
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Close_current_return_to_parent;
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH =
                EVC30_MMIRequestEnable.EnabledRequests.EnableWheelDiameter;
            EVC30_MMIRequestEnable.Send();

            XML_10_4_1_4_a_b(msgType.typeb);
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The Wheel diameter window is closed and DMI displays the System info window.");

            MakeTestStepHeader(4, UniqueIdentifier++,
                "Perform the following procedure,At System info window, press ‘close’ button.Open Wheel diameter validation windowRepeat action step 1-2",
                "Verify the followin information,(1)    The Wheel diameter validation window is closed, DMI displays System info window after received packet EVC-24");
            /*
            Test Step 4
            Action: Perform the following procedure,At System info window, press ‘close’ button.Open Wheel diameter validation windowRepeat action step 1-2
            Expected Result: Verify the followin information,(1)    The Wheel diameter validation window is closed, DMI displays System info window after received packet EVC-24
            Test Step Comment: (1) MMI_gen 5507 (partly: Wheel diameter validation window, abort an already pending data validation process, received packet of different window from ETCS onboard);
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button in the System info window");

            EVC40_MMICurrentMaintenanceData.MMI_M_SDU_WHEEL_SIZE_1 = (Variables.MMI_M_SDU_WHEEL_SIZE) 100;
            EVC40_MMICurrentMaintenanceData.MMI_M_SDU_WHEEL_SIZE_2 = (Variables.MMI_M_SDU_WHEEL_SIZE) 40;
            EVC40_MMICurrentMaintenanceData.MMI_M_WHEEL_SIZE_ERR = (Variables.MMI_M_WHEEL_SIZE_ERR) 1;
            EVC40_MMICurrentMaintenanceData.Send();

            XML_10_4_1_4_a_b(msgType.typea);
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The hourglass symbol ST05 is displayed in the window title area.");

            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 4;
            EVC8_MMIDriverMessage.Send();

            EVC41_MMIEchoedMaintenanceData.MMI_M_SDU_WHEEL_SIZE_1_ = (Variables.MMI_M_SDU_WHEEL_SIZE) 100;
            EVC41_MMIEchoedMaintenanceData.MMI_M_SDU_WHEEL_SIZE_2_ = (Variables.MMI_M_SDU_WHEEL_SIZE) 40;
            EVC41_MMIEchoedMaintenanceData.MMI_M_WHEEL_SIZE_ERR_ = (Variables.MMI_M_WHEEL_SIZE_ERR) 1;
            EVC41_MMIEchoedMaintenanceData.Send();

            DmiActions.ShowInstruction(this, "Confirm the values in the Wheel diameter window");

            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Close_current_return_to_parent;
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH =
                EVC30_MMIRequestEnable.EnabledRequests.EnableWheelDiameter;
            EVC30_MMIRequestEnable.Send();

            XML_10_4_1_4_a_b(msgType.typeb);
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The Wheel diameter validation window is closed and DMI displays the System info window.");

            MakeTestStepHeader(5, UniqueIdentifier++,
                "Perform the following procedure,At System info window, press ‘close’ button.Open Radar windowRepeat action step 1-2",
                "Verify the followin information,(1)    The Radar Number window is closed, DMI displays System info window after received packet EVC-24");
            /*
            Test Step 5
            Action: Perform the following procedure,At System info window, press ‘close’ button.Open Radar windowRepeat action step 1-2
            Expected Result: Verify the followin information,(1)    The Radar Number window is closed, DMI displays System info window after received packet EVC-24
            Test Step Comment: (1) MMI_gen 5507 (partly: Radar window, abort an already pending data entry process, received packet of different window from ETCS onboard);
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button in the System info window");

            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Settings;
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.EnableDoppler;
            EVC30_MMIRequestEnable.Send();

            DmiActions.ShowInstruction(this,
                "Press the ‘Maintenance’ button, enter the maintenance password, then open the Radar window");

            EVC40_MMICurrentMaintenanceData.MMI_Q_MD_DATASET = Variables.MMI_Q_MD_DATASET.Doppler;
            EVC40_MMICurrentMaintenanceData.Send();

            XML_10_4_1_4_a_b(msgType.typea);
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The hourglass symbol ST05 is displayed in the window title area.");

            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 4;
            EVC8_MMIDriverMessage.Send();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Close_current_return_to_parent;
            EVC30_MMIRequestEnable.Send();
            XML_10_4_1_4_a_b(msgType.typeb);
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The Radar Number window is closed and DMI displays the System info window.");

            MakeTestStepHeader(6, UniqueIdentifier++,
                "Perform the following procedure,At System info window, press ‘close’ button.Open Radar validation windowRepeat action step 1-2",
                "Verify the followin information,(1)    The Radar validation window is closed, DMI displays System info window after received packet EVC-24");
            /*
            Test Step 6
            Action: Perform the following procedure,At System info window, press ‘close’ button.Open Radar validation windowRepeat action step 1-2
            Expected Result: Verify the followin information,(1)    The Radar validation window is closed, DMI displays System info window after received packet EVC-24
            Test Step Comment: (1) MMI_gen 5507 (partly: Radar validation window, abort an already pending data validation process, received packet of different window from ETCS onboard);
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button in the System info window");

            EVC40_MMICurrentMaintenanceData.MMI_Q_MD_DATASET = Variables.MMI_Q_MD_DATASET.Doppler;
            EVC40_MMICurrentMaintenanceData.Send();

            DmiActions.ShowInstruction(this, "Confirm the Radar data to open the validation window");

            EVC41_MMIEchoedMaintenanceData.MMI_Q_MD_DATASET_ = Variables.MMI_Q_MD_DATASET.Doppler;
            EVC41_MMIEchoedMaintenanceData.MMI_M_PULSE_PER_KM_1_ = (Variables.MMI_M_PULSE_PER_KM) 980;
            EVC41_MMIEchoedMaintenanceData.MMI_M_PULSE_PER_KM_2_ = (Variables.MMI_M_PULSE_PER_KM) 980;
            EVC41_MMIEchoedMaintenanceData.Send();

            XML_10_4_1_4_a_b(msgType.typea);
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The hourglass symbol ST05 is displayed in the window title area.");

            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Close_current_return_to_parent;
            EVC30_MMIRequestEnable.Send();

            XML_10_4_1_4_a_b(msgType.typeb);
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The Radar validation window is closed and DMI displays the System info window.");

            MakeTestStepHeader(7, UniqueIdentifier++,
                "Perform the following procedure,At System info window, press ‘close’ button.Open Language windowRepeat action step 1-2",
                "Verify the followin information,(1)    The Language window is closed, DMI displays System info window after received packet EVC-24");
            /*
            Test Step 7
            Action: Perform the following procedure,At System info window, press ‘close’ button.Open Language windowRepeat action step 1-2
            Expected Result: Verify the followin information,(1)    The Language window is closed, DMI displays System info window after received packet EVC-24
            Test Step Comment: (1) MMI_gen 5507 (partly: Language window, abort an already pending data entry process, received packet of different window from ETCS onboard);
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button in the System info window");

            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.No_window_specified;
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.Language;
            EVC30_MMIRequestEnable.Send();

            DmiActions.ShowInstruction(this, @"Open the Settings window, then press the ‘Language’ button");

            XML_10_4_1_4_a_b(msgType.typea);
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The hourglass symbol ST05 is displayed in the window title area.");

            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 4;
            EVC8_MMIDriverMessage.Send();

            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Close_current_return_to_parent;
            EVC30_MMIRequestEnable.Send();

            XML_10_4_1_4_a_b(msgType.typeb);
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The Language window is closed and DMI displays the System info window.");

            MakeTestStepHeader(8, UniqueIdentifier++,
                "Perform the following procedure,At System info window, press ‘close’ button.Open Volume windowRepeat action step 1-2",
                "Verify the followin information,(1)    The Volume window is closed, DMI displays System info window after received packet EVC-24");
            /*
            Test Step 8
            Action: Perform the following procedure,At System info window, press ‘close’ button.Open Volume windowRepeat action step 1-2
            Expected Result: Verify the followin information,(1)    The Volume window is closed, DMI displays System info window after received packet EVC-24
            Test Step Comment: (1) MMI_gen 5507 (partly: Volume window, abort an already pending data entry process, received packet of different window from ETCS onboard);
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button in the System info window");

            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.No_window_specified;
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.Volume;
            EVC30_MMIRequestEnable.Send();

            DmiActions.ShowInstruction(this, "Open the Settings window, then press the ‘Volume’ button");

            XML_10_4_1_4_a_b(msgType.typea);
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The hourglass symbol ST05 is displayed in the window title area.");

            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 4;
            EVC8_MMIDriverMessage.Send();
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Close_current_return_to_parent;
            EVC30_MMIRequestEnable.Send();

            XML_10_4_1_4_a_b(msgType.typeb);
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The Volume window is closed and DMI displays the System info window.");

            MakeTestStepHeader(9, UniqueIdentifier++,
                "Perform the following procedure,At System info window, press ‘close’ button.Open Brightness windowRepeat action step 1-2",
                "Verify the followin information,(1)    The Brightness window is closed, DMI displays System info window after received packet EVC-24");
            /*
            Test Step 9
            Action: Perform the following procedure,At System info window, press ‘close’ button.Open Brightness windowRepeat action step 1-2
            Expected Result: Verify the followin information,(1)    The Brightness window is closed, DMI displays System info window after received packet EVC-24
            Test Step Comment: (1) MMI_gen 5507 (partly: Brightness window, abort an already pending data entry process, received packet of different window from ETCS onboard);
            */
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Settings;
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.Brightness;
            EVC30_MMIRequestEnable.Send();

            DmiActions.ShowInstruction(this,
                @"Press the ‘Close’ button in the System info window. Open the Settings window, then press the ‘Brightness’ button");

            XML_10_4_1_4_a_b(msgType.typea);
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The hourglass symbol ST05 is displayed in the window title area.");

            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 4;
            EVC8_MMIDriverMessage.Send();
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Close_current_return_to_parent;
            EVC30_MMIRequestEnable.Send();

            XML_10_4_1_4_a_b(msgType.typeb);
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The Brightness window is closed and DMI displays the System info window.");

            MakeTestStepHeader(10, UniqueIdentifier++,
                "Perform the following procedure,At System info window, press ‘close’ button.Open Set VBC windowRepeat action step 1-2",
                "Verify the followin information,(1)    The Set VBC window is closed, DMI displays System info window after received packet EVC-24");
            /*
            Test Step 10
            Action: Perform the following procedure,At System info window, press ‘close’ button.Open Set VBC windowRepeat action step 1-2
            Expected Result: Verify the followin information,(1)    The Set VBC window is closed, DMI displays System info window after received packet EVC-24
            Test Step Comment: (1) MMI_gen 5507 (partly: Set VBC window, abort an already pending data entry process, received packet of different window from ETCS onboard);
            */
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 4;
            EVC8_MMIDriverMessage.Send();
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.No_window_specified;
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.SetVBC;
            EVC30_MMIRequestEnable.Send();

            DmiActions.ShowInstruction(this,
                @"Press the ‘Close’ button in the System info window. Open the Settings window, then press the ‘Set VBC’ button");

            EVC18_MMISetVBC.MMI_M_BUTTONS = Variables.MMI_M_BUTTONS_VBC.BTN_YES_DATA_ENTRY_COMPLETE;
            EVC18_MMISetVBC.MMI_N_VBC = 0;
            EVC18_MMISetVBC.Send();

            XML_10_4_1_4_a_b(msgType.typea);
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The hourglass symbol ST05 is displayed in the window title area.");

            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 4;
            EVC8_MMIDriverMessage.Send();
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Close_current_return_to_parent;
            EVC30_MMIRequestEnable.Send();

            XML_10_4_1_4_a_b(msgType.typeb);
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The Set VBC window is closed and DMI displays the System info window.");

            MakeTestStepHeader(11, UniqueIdentifier++,
                "Perform the following procedure,At System info window, press ‘close’ button.Open Set VBC validation windowRepeat action step 1-2",
                "Verify the followin information,(1)    The Set VBC validation window is closed, DMI displays System info window after received packet EVC-24");
            /*
            Test Step 11
            Action: Perform the following procedure,At System info window, press ‘close’ button.Open Set VBC validation windowRepeat action step 1-2
            Expected Result: Verify the followin information,(1)    The Set VBC validation window is closed, DMI displays System info window after received packet EVC-24
            Test Step Comment: (1) MMI_gen 5507 (partly: Set VBC validation window, abort an already pending data validation process, received packet of different window from ETCS onboard);
            */
            DmiActions.ShowInstruction(this,
                @"Press the ‘Close’ button in the System info window, then press the ‘Set VBC’ button");

            //EVC30_MMIRequestEnable.SendBlank();
            //EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.No_window_specified;
            //EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.SetVBC;
            //EVC30_MMIRequestEnable.Send();
            EVC18_MMISetVBC.MMI_N_VBC = 0;
            EVC18_MMISetVBC.MMI_M_BUTTONS = Variables.MMI_M_BUTTONS_VBC.BTN_YES_DATA_ENTRY_COMPLETE;
            EVC18_MMISetVBC.Send();

            DmiActions.ShowInstruction(this, @"Confirm the data to open the Set VBC validation window");

            EVC28_MMIEchoedSetVBCData.MMI_M_VBC_CODE_ = 0;
            EVC28_MMIEchoedSetVBCData.Send();

            XML_10_4_1_4_a_b(msgType.typea);
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The hourglass symbol ST05 is displayed in the window title area.");

            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 4;
            EVC8_MMIDriverMessage.Send();
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Close_current_return_to_parent;
            EVC30_MMIRequestEnable.Send();

            XML_10_4_1_4_a_b(msgType.typeb);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The Set VBC validation window is closed and DMI displays the System info window.");

            MakeTestStepHeader(12, UniqueIdentifier++,
                "Perform the following procedure,At System info window, press ‘close’ button.Open Remove VBC windowRepeat action step 1-2",
                "Verify the followin information,(1)    The Remove VBC window is closed, DMI displays System info window after received packet EVC-24");
            /*
            Test Step 12
            Action: Perform the following procedure,At System info window, press ‘close’ button.Open Remove VBC windowRepeat action step 1-2
            Expected Result: Verify the followin information,(1)    The Remove VBC window is closed, DMI displays System info window after received packet EVC-24
            Test Step Comment: (1) MMI_gen 5507 (partly: Remove VBC window, abort an already pending data entry process, received packet of different window from ETCS onboard);
            */
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.No_window_specified;
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.RemoveVBC;
            EVC30_MMIRequestEnable.Send();

            DmiActions.ShowInstruction(this,
                @"Press the ‘Close’ button in the System info window. Open the Settings window, then press the ‘Remove VBC’ button");

            EVC19_MMIRemoveVBC.MMI_N_VBC = 0;
            EVC19_MMIRemoveVBC.ECHO_TEXT = "";
            EVC19_MMIRemoveVBC.Send();

            XML_10_4_1_4_a_b(msgType.typea);
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The hourglass symbol ST05 is displayed in the window title area.");

            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 4;
            EVC8_MMIDriverMessage.Send();
            EVC19_MMIRemoveVBC.MMI_N_VBC = 1;
            EVC19_MMIRemoveVBC.MMI_M_BUTTONS = Variables.MMI_M_BUTTONS_VBC.BTN_SETTINGS;
            EVC19_MMIRemoveVBC.MMI_Q_DATA_CHECK = Variables.Q_DATA_CHECK.All_checks_passed;
            EVC19_MMIRemoveVBC.Send();

            XML_10_4_1_4_a_b(msgType.typeb);
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The Remove VBC window is closed and DMI displays the System info window.");

            MakeTestStepHeader(13, UniqueIdentifier++,
                "Perform the following procedure,At System info window, press ‘close’ button.Open Remove VBC validation windowRepeat action step 1-2",
                "Verify the followin information,(1)    The Remove VBC validation window is closed, DMI displays System info window after received packet EVC-24");
            /*
            Test Step 13
            Action: Perform the following procedure,At System info window, press ‘close’ button.Open Remove VBC validation windowRepeat action step 1-2
            Expected Result: Verify the followin information,(1)    The Remove VBC validation window is closed, DMI displays System info window after received packet EVC-24
            Test Step Comment: (1) MMI_gen 5507 (partly: Remove VBC validation window, abort an already pending data validation process, received packet of different window from ETCS onboard);
            */
            DmiActions.ShowInstruction(this,
                @"Press the ‘Close’ button in the System info window. Open the Settings window, then press the ‘Remove VBC’ button");

            EVC19_MMIRemoveVBC.MMI_N_VBC = 0;
            EVC19_MMIRemoveVBC.MMI_M_BUTTONS = Variables.MMI_M_BUTTONS_VBC.BTN_YES_DATA_ENTRY_COMPLETE_DELAY_TYPE;
            EVC19_MMIRemoveVBC.Send();

            DmiActions.ShowInstruction(this, @"Confirm the data to open the Remove VBC validation window");

            EVC29_MMIEchoedRemoveVBCData.MMI_M_VBC_CODE_ = 0;
            EVC29_MMIEchoedRemoveVBCData.Send();

            XML_10_4_1_4_a_b(msgType.typea);
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The hourglass symbol ST05 is displayed in the window title area.");

            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 4;
            EVC8_MMIDriverMessage.Send();

            DmiActions.ShowInstruction(this, @"Confirm the data");

            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Close_current_return_to_parent;
            EVC30_MMIRequestEnable.Send();

            XML_10_4_1_4_a_b(msgType.typeb);
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The Remove VBC validation window is closed and DMI displays the System info window.");

            MakeTestStepHeader(14, UniqueIdentifier++,
                "Perform the following procedure,At System info window, press ‘close’ button.Open Brake Percentage windowRepeat action step 1-2",
                "Verify the followin information,(1)    The Brake Percentage window is closed, DMI displays System info window after received packet EVC-24");
            /*
            Test Step 14
            Action: Perform the following procedure,At System info window, press ‘close’ button.Open Brake Percentage windowRepeat action step 1-2
            Expected Result: Verify the followin information,(1)    The Brake Percentage window is closed, DMI displays System info window after received packet EVC-24
            Test Step Comment: (1) MMI_gen 5507 (partly: Brake Percentage window, abort an already pending data entry process, received packet of different window from ETCS onboard);
            */
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Settings;
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH =
                EVC30_MMIRequestEnable.EnabledRequests.EnableBrakePercentage;
            EVC30_MMIRequestEnable.Send();

            DmiActions.ShowInstruction(this,
                @"Press the ‘Close’ button in the System info window. Open the Brake window and press the ‘Brake Percentage’ button");

            EVC50_MMICurrentBrakePercentage.MMI_M_BP_CURRENT = 70;
            EVC50_MMICurrentBrakePercentage.Send();

            XML_10_4_1_4_a_b(msgType.typea);
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The hourglass symbol ST05 is displayed in the window title area.");

            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 4;
            EVC8_MMIDriverMessage.Send();

            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Close_current_return_to_parent;
            EVC30_MMIRequestEnable.Send();

            // This test is invalid: to leave the brake percentage window Exit must be pressed
            DmiActions.ShowInstruction(this, "Press the ‘Exit’ button");

            XML_10_4_1_4_a_b(msgType.typeb);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The Brake Percentage window is closed and DMI displays the System info window.");

            MakeTestStepHeader(15, UniqueIdentifier++,
                "Perform the following procedure,At System info window, press ‘close’ button.Open Brake Percentage validation windowRepeat action step 1-2",
                "Verify the followin information,(1)    The Brake Percentage validation window is closed, DMI displays System info window after received packet EVC-24");
            /*
            Test Step 15
            Action: Perform the following procedure,At System info window, press ‘close’ button.Open Brake Percentage validation windowRepeat action step 1-2
            Expected Result: Verify the followin information,(1)    The Brake Percentage validation window is closed, DMI displays System info window after received packet EVC-24
            Test Step Comment: (1) MMI_gen 5507 (partly: Brake Percentage validation window, abort an already pending data validation process, received packet of different window from ETCS onboard);
            */
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.No_window_specified;
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.StartBrakeTest |
                                                               EVC30_MMIRequestEnable.EnabledRequests
                                                                   .EnableBrakePercentage;
            EVC30_MMIRequestEnable.Send();

            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button in the System info window");

            // The test is invalid, the work flow is to do brake percentage and either press Exit or continue and validate
            //EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Close_current_return_to_parent;
            //EVC30_MMIRequestEnable.Send();

            DmiActions.ShowInstruction(this, @"Press ‘Brake percentage’ button.");

            EVC50_MMICurrentBrakePercentage.MMI_M_BP_CURRENT = 90;
            EVC50_MMICurrentBrakePercentage.MMI_M_BP_ORIG = 95;
            EVC50_MMICurrentBrakePercentage.MMI_M_BP_MEASURED = 92;
            EVC50_MMICurrentBrakePercentage.Send();

            DmiActions.ShowInstruction(this, @"Validate the data");

            EVC51_MMIEchoedBrakePercentage.MMI_M_BP_CURRENT_ = 90;
            EVC51_MMIEchoedBrakePercentage.Send();

            XML_10_4_1_4_a_b(msgType.typea);
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The hourglass symbol ST05 is displayed in the window title area.");

            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 4;
            EVC8_MMIDriverMessage.Send();

            DmiActions.ShowInstruction(this, @"Validate the data");

            XML_10_4_1_4_a_b(msgType.typeb);
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The Brake Percentage validation window is closed and DMI displays the System info window.");

            MakeTestStepHeader(16, UniqueIdentifier++, "End of test", "");

            /*
            Test Step 16
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }

        #region Send_XML_10_4_1_4_a_b_DMI_Test_Specification

        enum msgType
        {
            typea,
            typeb
        }

        private void XML_10_4_1_4_a_b(msgType type)
        {
            if (type == msgType.typea)
            {
                EVC8_MMIDriverMessage.MMI_Q_TEXT = 716;
                EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
                EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
                EVC8_MMIDriverMessage.MMI_I_TEXT = 1;

                EVC8_MMIDriverMessage.Send();
            }
            else if (type == msgType.typeb)
            {
                EVC24_MMISystemInfo.MMI_NID_ENGINE_1 = 1234;
                EVC24_MMISystemInfo.MMI_T_TIMEOUT_BRAKE = 0x5695224c; // 1452614220
                EVC24_MMISystemInfo.MMI_T_TIMEOUT_BTM = 0x54b3eecc; // 1421078220
                EVC24_MMISystemInfo.MMI_T_TIMEOUT_TBSW = 0x538b4d4c; // 1401638220
                EVC24_MMISystemInfo.MMI_M_ETC_VER = 0xffaa0f; // 16755215
                EVC24_MMISystemInfo.MMI_M_AVAIL_SERVICES = 0xffff; // 65535 

                // Discrepancy between spec (config = 55)
                EVC24_MMISystemInfo.MMI_M_BRAKE_CONFIG = 55; // 236 in xml
                EVC24_MMISystemInfo.MMI_M_LEVEL_INST = 248;

                EVC24_MMISystemInfo.Send();
            }
        }

        #endregion
    }
}
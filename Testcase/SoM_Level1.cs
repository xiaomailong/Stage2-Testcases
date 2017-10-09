#region usings

using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BT_CSB_Tools;
using BT_CSB_Tools.Logging;
using BT_CSB_Tools.Utils.Xml;
using BT_CSB_Tools.SignalPoolGenerator.Signals;
using BT_CSB_Tools.SignalPoolGenerator.Signals.MwtSignal;
using BT_CSB_Tools.SignalPoolGenerator.Signals.PdSignal.Misc;
using CL345;
using System.Windows.Forms;
using Testcase.Telegrams;
using Testcase.Telegrams.EVCtoDMI;
using Testcase.TemporaryFunctions;

#endregion

namespace Testcase
{
    public class SoM_Level1 : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-test configuration.
            base.PreExecution();
        }

        public override void PostExecution()
        {
            // Post-test cleanup.
        }

        public override bool TestcaseEntryPoint()
        {
            // Test case entry point. 

            // ETCS->DMI: EVC-0 MMI_START_ATP
            EVC0_MMIStartATP.Evc0Type = EVC0_MMIStartATP.EVC0Type.VersionInfo;
            EVC0_MMIStartATP.Send();

            // DMI->ETCS: EVC-100 MMI_START_MMI

            // ETCS->DMI: EVC-0 MMI_START_ATP
            EVC0_MMIStartATP.Evc0Type = EVC0_MMIStartATP.EVC0Type.GoToIdle;
            EVC0_MMIStartATP.Send();

            // Possible send EVC-3 MMI_SET_TIME_ATP packet
            EVC3_MMISetTimeATP.MMI_T_UTC = (uint)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
            EVC3_MMISetTimeATP.MMI_T_ZONE_OFFSET = 0;
            EVC3_MMISetTimeATP.Send();

            // Possibly send EVC-30 MMI_Enable_Request packet

            //ETCS->DMI: EVC-2 MMI_STATUS
            EVC2_MMIStatus.TrainRunningNumber = 0xffffffff;
            EVC2_MMIStatus.MMI_M_ACTIVE_CABIN = Variables.MMI_M_ACTIVE_CABIN.Cabin1Active;
            EVC2_MMIStatus.MMI_M_ADHESION = 0x0;
            EVC2_MMIStatus.MMI_M_OVERRIDE_EOA = false;
            EVC2_MMIStatus.Send();

            // ETCS->DMI: EVC-14 MMI_CURRENT_DRIVER_ID
            EVC14_MMICurrentDriverID.MMI_X_DRIVER_ID = "1234";
            EVC14_MMICurrentDriverID.MMI_Q_ADD_ENABLE = EVC14_MMICurrentDriverID.MMI_Q_ADD_ENABLE_BUTTONS.Settings |
                                                        EVC14_MMICurrentDriverID.MMI_Q_ADD_ENABLE_BUTTONS.TRN;
            EVC14_MMICurrentDriverID.MMI_Q_CLOSE_ENABLE = Variables.MMI_Q_CLOSE_ENABLE.Enabled;
            EVC14_MMICurrentDriverID.Send();

            // Receive EVC-104 MMI_NEW_DRIVER_DATA   
            // DMI input required

            // Send EVC-8 MMI_DRIVER_MESSAGE
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 2;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 5;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 514;
            EVC8_MMIDriverMessage.Send();

            // Wait for Perform Brake Test input on DMI
            // Send "NO" back to EVC (EVC-111 MMI_DRIVER_MESSAGE_ACK)

            // TODO Have I set the correct flags?
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = 255;
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = Variables.standardFlags;
            EVC30_MMIRequestEnable.Send();

            //ETCS->DMI: EVC-20 MMI_SELECT_LEVEL
            //Temporary.SendEVC20_MMISelectLevel_AllLevels(this);
            DMITestCases.DmiActions.Send_EVC20_MMISelectLevel_AllLevels(this);

            //ETCS->DMI: Send EVC-6 MMI_CURRENT TRAIN_DATA
            //Temporary.SendEVC6_MMICurrentTrainData_FixedDataEntry(new[] {"FLU", "RLU", "Rescue"}, 2);

            //ETCS->DMI: Send EVC-10 MMI_ECHOED_TRAIN_DATA
            //Temporary.SendEVC10_MMIEchoedTrainData(this);

            //Receive EVC-101

            // Send EVC-22
            EVC22_MMICurrentRBC.MMI_NID_RADIO = 07123456;
            EVC22_MMICurrentRBC.MMI_NID_WINDOW = 9;
            EVC22_MMICurrentRBC.MMI_M_BUTTONS = EVC22_MMICurrentRBC.EVC22BUTTONS.BTN_YES_DATA_ENTRY_COMPLETE;
            EVC22_MMICurrentRBC.NID_RBC = 1234;
            EVC22_MMICurrentRBC.MMI_Q_CLOSE_ENABLE = Variables.MMI_Q_CLOSE_ENABLE.Enabled;

            EVC22_MMICurrentRBC.NetworkCaptions = new List<string> { "RBC1", "RBC2", "RBC3" };
            EVC22_MMICurrentRBC.Send();

            //EVC22_MMICurrentRBC.DataElements = new List<Variables.DataElement>{
            //    { new Variables.DataElement { Identifier = 1, QDataCheck = 0, EchoText = "1234" } },
            //    { new Variables.DataElement { Identifier = 2, QDataCheck = 0, EchoText = "07123456" } },
            //};

            // Send EVC-30 MMI_REQUEST_ENABLE
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = 255;
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH =
                Variables.standardFlags | EVC30_MMIRequestEnable.EnabledRequests.ContactLastRBC;
            EVC30_MMIRequestEnable.Send();

            // Send EVC-16 MMI_CURRENT_TRAIN_NUMBER
            EVC16_CurrentTrainNumber.TrainRunningNumber = 0xffffffff;
            EVC16_CurrentTrainNumber.Send();

            // Receive packet EVC-116 MMI_NEW_TRAIN_NUMBER

            // Send Cab active with echoed train number
            EVC2_MMIStatus.TrainRunningNumber = 0x1111;
            EVC2_MMIStatus.Send();

            // Send EVC-30 MMI_ENABLE_REQUEST
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = 255;
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH =
                Variables.standardFlags | EVC30_MMIRequestEnable.EnabledRequests.ContactLastRBC |
                EVC30_MMIRequestEnable.EnabledRequests.Start;
            EVC30_MMIRequestEnable.Send();

            // Receive packet EVC-101 MMI_DRIVER_REQUEST (Driver presses Start Button)

            // Send EVC-8 MMI_DRIVER_MESSAGE
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 263;     // "#3 MO10 (Ack Staff Responsible Mode)"
            EVC8_MMIDriverMessage.Send();

            // Receive packet EVC-111 MMI_DRIVER_MESSAGE_ACK (Driver acknowledges SR Mode)

            return GlobalTestResult;
        }
    }
}
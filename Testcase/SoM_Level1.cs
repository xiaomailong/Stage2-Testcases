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
using Testcase.TemporaryFunctions;

#endregion

namespace Testcase
{
    public class SoM_Level1 : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-test configuration.
        }

        public override void PostExecution()
        {
            // Post-test cleanup.
        }

        public override bool TestcaseEntryPoint()
        {
            // Test case entry point. 

            // TODO move initialisation to base class when there are multiple test cases?
            TraceInfo("Initialise Default Values");
            EVC0_MMIStartATP.Initialise(this);
            EVC1_MMIDynamic.Initialise(this);
            EVC2_MMIStatus.Initialise(this);
            EVC6_MMICurrentTrainData.Initialise(this);
            EVC14_MMICurrentDriverID.Initialise(this);
            EVC16_CurrentTrainNumber.Initialise(this);
            EVC30_MMIRequestEnable.Initialise(this);

            // Initialise Dynamic Arrays
            Initialize_DynamicArrays();

            // ETCS->DMI: EVC-0 MMI_START_ATP
            EVC0_MMIStartATP.Evc0Type = EVC0_MMIStartATP.EVC0Type.VersionInfo;
            EVC0_MMIStartATP.Send();

            // DMI->ETCS: EVC-100 MMI_START_MMI

            // ETCS->DMI: EVC-0 MMI_START_ATP
            EVC0_MMIStartATP.Evc0Type = EVC0_MMIStartATP.EVC0Type.GoToIdle;
            EVC0_MMIStartATP.Send();

            // Possible send EVC-3 MMI_SET_TIME_ATP packet      (Wireshark log)
            // Possibly send EVC-30 MMI_Enable_Request packet   (Wireshark log)

            //ETCS->DMI: EVC-2 MMI_STATUS
            EVC2_MMIStatus.TrainRunningNumber = 0xffffffff;
            EVC2_MMIStatus.Send();

            // ETCS->DMI: EVC-14 MMI_CURRENT_DRIVER_ID
            EVC14_MMICurrentDriverID.MMI_X_DRIVER_ID = "1234";
            EVC14_MMICurrentDriverID.MMI_Q_ADD_ENABLE = EVC14_MMICurrentDriverID.MMIQADDENABLEBUTTONS.Settings |
                                                        EVC14_MMICurrentDriverID.MMIQADDENABLEBUTTONS.TRN;
            EVC14_MMICurrentDriverID.MMI_Q_CLOSE_ENABLE = false;
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

            // Send EVC-30 MMI_REQUEST_ENABLE
            var standardflags = EVC30_MMIRequestEnable.EnabledRequests.EnableDoppler |
                                EVC30_MMIRequestEnable.EnabledRequests.EnableWheelDiameter |
                                EVC30_MMIRequestEnable.EnabledRequests.StartBrakeTest |
                                EVC30_MMIRequestEnable.EnabledRequests.SetLocalOffset |
                                EVC30_MMIRequestEnable.EnabledRequests.RemoveVBC |
                                EVC30_MMIRequestEnable.EnabledRequests.SetVBC |
                                EVC30_MMIRequestEnable.EnabledRequests.SystemVersion |
                                EVC30_MMIRequestEnable.EnabledRequests.Brightness |
                                EVC30_MMIRequestEnable.EnabledRequests.Valume |
                                EVC30_MMIRequestEnable.EnabledRequests.NonLeading |
                                EVC30_MMIRequestEnable.EnabledRequests.Shunting |
                                EVC30_MMIRequestEnable.EnabledRequests.TrainRunningNumber |
                                EVC30_MMIRequestEnable.EnabledRequests.Level;


            // TODO Have I set the correct flags?
            // SendEVC30_MMIRequestEnable(255, 0b0001_1101_0000_0011_1110_0000_0011_1110);
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = 255;
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = standardflags;
            EVC30_MMIRequestEnable.Send();

            //ETCS->DMI: EVC-20 MMI_SELECT_LEVEL
            Temporary.SendEVC20_MMISelectLevel_AllLevels(this);

            //ETCS->DMI: Send EVC-6 MMI_CURRENT TRAIN_DATA

            Temporary.SendEVC6_MMICurrentTrainData_FixedDataEntry(new[] {"FLU", "RLU", "Rescue"}, 2);

            //SendEVC6_MMICurrentTrainData(param_EVC6_MmiMDataEnable, param_EVC6_MmiLTrain, param_EVC6_MmiVMaxtrain, param_EVC6_MmiNidKeyTrainCat,
            //    param_EVC6_MmiMBrakePerc, param_EVC6_MmiNidKeyAxleLoad, param_EVC6_MmiMAirtight, param_EVC6_MmiNidKeyLoadGauge,
            //    param_EVC6_MmiMButtons, param_EVC6_MTrainsetId, param_EVC6_MAltDem, param_EVC6_MmiNTrainsets, param_EVC6_MmiNCaptionTrainset,
            //    param_EVC6_MmiXCaptionTrainset, param_EVC6_MmiNDataElements, null, null, null, null);

            //ETCS->DMI: Send EVC-10 MMI_ECHOED_TRAIN_DATA
            //SendEVC10_MMIEchoedTrainData();

            ////Receive EVC-101

            // Send EVC-30 MMI_REQUEST_ENABLE
            //SendEVC30_MMIRequestEnable(255, 0b0001_1101_0000_0011_1111_0000_0011_1110);
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = 255;
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH =
                standardflags | EVC30_MMIRequestEnable.EnabledRequests.ContactLastRBC;
            EVC30_MMIRequestEnable.Send();

            // Send EVC-16 MMI_CURRENT_TRAIN_NUMBER
            EVC16_CurrentTrainNumber.TrainRunningNumber = 0xffffffff;
            EVC16_CurrentTrainNumber.Send();

            // Receive packet EVC-116 MMI_NEW_TRAIN_NUMBER

            // Send Cab active with echoed train number
            EVC2_MMIStatus.TrainRunningNumber = 0xffffffff;
            EVC2_MMIStatus.Send();

            // Send EVC-30 MMI_ENABLE_REQUEST
            // SendEVC30_MMIRequestEnable(255, 0b0001_1101_0000_0011_1111_0000_0011_1111);
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = 255;
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH =
                standardflags | EVC30_MMIRequestEnable.EnabledRequests.ContactLastRBC |
                EVC30_MMIRequestEnable.EnabledRequests.Start;
            EVC30_MMIRequestEnable.Send();

            // Receive packet EVC-101 MMI_DRIVER_REQUEST (Driver presses Start Button)

            // Send EVC-8 MMI_DRIVER_MESSAGE
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 263;
            EVC8_MMIDriverMessage.Send(); // "#3 MO10 (Ack Staff Responsible Mode)"

            // Receive packet EVC-111 MMI_DRIVER_MESSAGE_ACK (Driver acknowledges SR Mode)

            return GlobalTestResult;
        }

        /// <summary>
        /// Initialises all EVC packets that contain dynamic arrays
        /// </summary>
        private void Initialize_DynamicArrays()
        {
            SITR.SMDCtrl.ETCS1.SelectLevel.Value = 0x8;
            SITR.SMDCtrl.ETCS1.SetVbc.Value = 0x8;
            SITR.SMDCtrl.ETCS1.RemoveVbc.Value = 0x8;
            SITR.SMDCtrl.ETCS1.TrackDescription.Value = 0x8;

            SITR.SMDCtrl.ETCS1.EchoedTrainData.Value = 0x8;
        }


        /// <summary>
        /// Bit-reverses a 32-bit number
        /// </summary>
        /// <param name="intToBeReversed"></param>
        /// <returns>Reversed 32-bit uint</returns>
        public static uint BitReverser32(uint intToBeReversed)
        {
            uint y = 0;

            for (int i = 0; i < 32; i++)
            {
                y <<= 1;
                y |= intToBeReversed & 1;
                intToBeReversed >>= 1;
            }

            return y;
        }

        /// <summary>
        /// Bit-reverses a 16-bit number
        /// </summary>
        /// <param name="intToBeReversed"></param>
        /// <returns>Reversed 16-bit uint</returns>
        public static ushort BitReverser16(ushort intToBeReversed)
        {
            int y = 0;

            for (int i = 0; i < 16; i++)
            {
                y <<= 1;
                y |= intToBeReversed & 1;
                intToBeReversed >>= 1;
            }

            ushort reversedInt = Convert.ToUInt16(y);
            return reversedInt;
        }
    }
}
﻿#region usings

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

#endregion

namespace Testcase
{
    public class SoM_Level1 : SignalPool
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

            // Initialise Dynamic Values
            Initialize_DynamicValues();

            // ETCS->DMI: EVC-0 MMI_START_ATP
            SendEVC0_MMIStartATP_VersionInfo();

            // DMI->ETCS: EVC-100 MMI_START_MMI

            // ETCS->DMI: EVC-0 MMI_START_ATP
            SendEVC0_MMIStartATP_GoToIdle();

            // Possible send EVC-3 MMI_SET_TIME_ATP packet      (Wireshark log)
            // Possibly send EVC-30 MMI_Enable_Request packet   (Wireshark log)

            //ETCS->DMI: EVC-2 MMI_STATUS
            SendEVC2_MMIStatus_Cab1Active(0xffffffff);

            // ETCS->DMI: EVC-14 MMI_CURRENT_DRIVER_ID
            SendEVC14_MMICurrentDriverID("1234", true, true, false);

            // Receive EVC-104 MMI_NEW_DRIVER_DATA   
            // DMI input required

            // Send EVC-8 MMI_DRIVER_MESSAGE
            SendEVC8_MMIDriverMessage(true, 2, 5, 514);

            //Wait for Perform Brake Test input on DMI
            //Send "NO" back to EVC (EVC-111 MMI_DRIVER_MESSAGE_ACK)

            //Send EVC-30 MMI_REQUEST_ENABLE
            SendEVC30_MMIRequestEnable(255, 0b0001_1101_0000_0011_1110_0000_0011_1110);

            //Send EVC-20 MMI_SELECT_LEVEL
            SITR.ETCS1.SelectLevel.MmiMPacket.Value = 20;
            SITR.ETCS1.SelectLevel.MmiNLevels.Value = 0x7;
            SITR.ETCS1.SelectLevel.EVC20alias1.Value = 0xe8;
            SITR.ETCS1.SelectLevel.MmiMLevelNtcId.Value = 0x1;
            SITR.ETCS1.SelectLevel.MmiQCloseEnable.Value = 168;
            SITR.ETCS1.SelectLevel.MmiLPacket.Value = 168;
            SITR.SMDCtrl.ETCS1.SelectLevel.Value = 1;

            ////Receive EVC-101

            ////Send EVC-6 MMI_CURRENT TRAIN_DATA
            //SITR.ETCS1.CurrentTrainData.MmiMPacket.Value = 6;
            //SITR.ETCS1.CurrentTrainData.MmiMDataEnable.Value = 0x7f00;      // 32512
            //SITR.ETCS1.CurrentTrainData.MmiLTrain.Value = 0x64;             // 100 metres
            //SITR.ETCS1.CurrentTrainData.MmiVMaxtrain.Value = 0xa0;          // 160
            //SITR.ETCS1.CurrentTrainData.MmiNidKeyTrainCat.Value = 0x3;      // 3
            //SITR.ETCS1.CurrentTrainData.MmiMBrakePerc.Value = 0x46;         // 70
            //SITR.ETCS1.CurrentTrainData.MmiNidKeyAxleLoad.Value = 0x15;     // 21
            //SITR.ETCS1.CurrentTrainData.MmiMAirtight.Value = 0x0;           // 0
            //SITR.ETCS1.CurrentTrainData.MmiNidKeyLoadGauge.Value = 0x26;    // 38
            //SITR.ETCS1.CurrentTrainData.MmiMButtons.Value = 0xff;           // 255
            //SITR.ETCS1.CurrentTrainData.EVC6alias1.Value = 0x0;             // 0
            //SITR.ETCS1.CurrentTrainData.MmiNTrainset.Value = 0x0;           // 0
            //SITR.ETCS1.CurrentTrainData.MmiNCaptionTrainset.Value = 0x0;    // 0
            //SITR.ETCS1.CurrentTrainData.MmiLPacket.Value = 176;
            //SITR.SMDCtrl.ETCS1.CurrentTrainData.Value = 1;
            ////SITR.SMDCtrl.ETCS1.CurrentTrainData.Value = 0;

            //// Receive telegram EVC-107

            ////Send EVC-6 MMI_CURRENT TRAIN_DATA
            //SITR.ETCS1.CurrentTrainData.MmiMPacket.Value = 6;
            //SITR.ETCS1.CurrentTrainData.MmiMDataEnable.Value = 0x7f00;          // 32512
            //SITR.ETCS1.CurrentTrainData.MmiLTrain.Value = 0x64;                 // 100 metres
            //SITR.ETCS1.CurrentTrainData.MmiVMaxtrain.Value = 0xa0;              // 160
            //SITR.ETCS1.CurrentTrainData.MmiNidKeyTrainCat.Value = 0x3;          // 3
            //SITR.ETCS1.CurrentTrainData.MmiMBrakePerc.Value = 0x46;             // 70
            //SITR.ETCS1.CurrentTrainData.MmiNidKeyAxleLoad.Value = 0x15;         // 21
            //SITR.ETCS1.CurrentTrainData.MmiMAirtight.Value = 0x0;               // 0
            //SITR.ETCS1.CurrentTrainData.MmiNidKeyLoadGauge.Value = 0x26;        // 38
            //SITR.ETCS1.CurrentTrainData.MmiMButtons.Value = 0xff;               // 255
            //SITR.ETCS1.CurrentTrainData.EVC6alias1.Value = 0x0;                 // 0
            //SITR.ETCS1.CurrentTrainData.MmiNTrainset.Value = 0x0;               // 0
            //SITR.ETCS1.CurrentTrainData.MmiNCaptionTrainset.Value = 0x1;        // 1
            //SITR.ETCS1.CurrentTrainData.MmiXCaptionTrainset.Value = (char)7;    // Bell
            //SITR.ETCS1.CurrentTrainData.MmiNDataElements.Value = 0x0;           // 0
            //SITR.ETCS1.CurrentTrainData.MmiNidData.Value = 0x6;                 // 6
            //SITR.ETCS1.CurrentTrainData.MmiQDataCheck.Value = 0x50;             // 80
            //SITR.ETCS1.CurrentTrainData.MmiNText.Value = 0x4153;                // 16723
            //SITR.ETCS1.CurrentTrainData.MmiXText.Value = (char)83;              // 'S'
            //SITR.ETCS1.CurrentTrainData.MmiLPacket.Value = 256;
            //SITR.SMDCtrl.ETCS1.CurrentTrainData.Value = 1;
            ////SITR.SMDCtrl.ETCS1.CurrentTrainData.Value = 0;

            //// Receive telegram EVC-107

            ////Send EVC-6 MMI_CURRENT TRAIN_DATA
            //SITR.ETCS1.CurrentTrainData.MmiMPacket.Value = 6;
            //SITR.ETCS1.CurrentTrainData.MmiMDataEnable.Value = 0x7f00;          // 32512
            //SITR.ETCS1.CurrentTrainData.MmiLTrain.Value = 0x64;                 // 100 metres
            //SITR.ETCS1.CurrentTrainData.MmiVMaxtrain.Value = 0xa0;              // 160
            //SITR.ETCS1.CurrentTrainData.MmiNidKeyTrainCat.Value = 0x3;          // 3
            //SITR.ETCS1.CurrentTrainData.MmiMBrakePerc.Value = 0x46;             // 70
            //SITR.ETCS1.CurrentTrainData.MmiNidKeyAxleLoad.Value = 0x15;         // 21
            //SITR.ETCS1.CurrentTrainData.MmiMAirtight.Value = 0x0;               // 0
            //SITR.ETCS1.CurrentTrainData.MmiNidKeyLoadGauge.Value = 0x26;        // 38
            //SITR.ETCS1.CurrentTrainData.MmiMButtons.Value = 0xff;               // 255
            //SITR.ETCS1.CurrentTrainData.EVC6alias1.Value = 0x0;                 // 0
            //SITR.ETCS1.CurrentTrainData.MmiNTrainset.Value = 0x0;               // 0
            //SITR.ETCS1.CurrentTrainData.MmiNCaptionTrainset.Value = 0x1;        // 1
            //SITR.ETCS1.CurrentTrainData.MmiXCaptionTrainset.Value = (char)8;    // Bell
            //SITR.ETCS1.CurrentTrainData.MmiNDataElements.Value = 0x0;           // 0
            //SITR.ETCS1.CurrentTrainData.MmiNidData.Value = 0x3;                 // 3
            //SITR.ETCS1.CurrentTrainData.MmiQDataCheck.Value = 0x31;             // 49
            //SITR.ETCS1.CurrentTrainData.MmiNText.Value = 0x3030;                // 12336
            //SITR.ETCS1.CurrentTrainData.MmiLPacket.Value = 232;
            //SITR.SMDCtrl.ETCS1.CurrentTrainData.Value = 1;
            ////SITR.SMDCtrl.ETCS1.CurrentTrainData.Value = 0;

            //// Receive telegram EVC-107

            ////Send EVC-6 MMI_CURRENT TRAIN_DATA
            //SITR.ETCS1.CurrentTrainData.MmiMPacket.Value = 6;
            //SITR.ETCS1.CurrentTrainData.MmiMDataEnable.Value = 0x7f00;          // 32512
            //SITR.ETCS1.CurrentTrainData.MmiLTrain.Value = 0x64;                 // 100 metres
            //SITR.ETCS1.CurrentTrainData.MmiVMaxtrain.Value = 0xa0;              // 160
            //SITR.ETCS1.CurrentTrainData.MmiNidKeyTrainCat.Value = 0x3;          // 3
            //SITR.ETCS1.CurrentTrainData.MmiMBrakePerc.Value = 0x46;             // 70
            //SITR.ETCS1.CurrentTrainData.MmiNidKeyAxleLoad.Value = 0x15;         // 21
            //SITR.ETCS1.CurrentTrainData.MmiMAirtight.Value = 0x0;               // 0
            //SITR.ETCS1.CurrentTrainData.MmiNidKeyLoadGauge.Value = 0x26;        // 38
            //SITR.ETCS1.CurrentTrainData.MmiMButtons.Value = 0xff;               // 255
            //SITR.ETCS1.CurrentTrainData.EVC6alias1.Value = 0x0;                 // 0
            //SITR.ETCS1.CurrentTrainData.MmiNTrainset.Value = 0x0;               // 0
            //SITR.ETCS1.CurrentTrainData.MmiNCaptionTrainset.Value = 0x1;        // 1
            //SITR.ETCS1.CurrentTrainData.MmiXCaptionTrainset.Value = (char)9;    // Bell
            //SITR.ETCS1.CurrentTrainData.MmiNDataElements.Value = 0x0;           // 0
            //SITR.ETCS1.CurrentTrainData.MmiNidData.Value = 0x0;                 // 0
            //SITR.ETCS1.CurrentTrainData.MmiQDataCheck.Value = 0x37;             // 55
            //SITR.ETCS1.CurrentTrainData.MmiLPacket.Value = 224;
            //SITR.SMDCtrl.ETCS1.CurrentTrainData.Value = 1;
            ////SITR.SMDCtrl.ETCS1.CurrentTrainData.Value = 0;

            //// Receive telegram EVC-107

            ////Send EVC-6 MMI_CURRENT TRAIN_DATA
            //SITR.ETCS1.CurrentTrainData.MmiMPacket.Value = 6;
            //SITR.ETCS1.CurrentTrainData.MmiMDataEnable.Value = 0x7f00;          // 32512
            //SITR.ETCS1.CurrentTrainData.MmiLTrain.Value = 0x64;                 // 100 metres
            //SITR.ETCS1.CurrentTrainData.MmiVMaxtrain.Value = 0xa0;              // 160
            //SITR.ETCS1.CurrentTrainData.MmiNidKeyTrainCat.Value = 0x3;          // 3
            //SITR.ETCS1.CurrentTrainData.MmiMBrakePerc.Value = 0x46;             // 70
            //SITR.ETCS1.CurrentTrainData.MmiNidKeyAxleLoad.Value = 0x15;         // 21
            //SITR.ETCS1.CurrentTrainData.MmiMAirtight.Value = 0x0;               // 0
            //SITR.ETCS1.CurrentTrainData.MmiNidKeyLoadGauge.Value = 0x26;        // 38
            //SITR.ETCS1.CurrentTrainData.MmiMButtons.Value = 0xff;               // 255
            //SITR.ETCS1.CurrentTrainData.EVC6alias1.Value = 0x0;                 // 0
            //SITR.ETCS1.CurrentTrainData.MmiNTrainset.Value = 0x0;               // 0
            //SITR.ETCS1.CurrentTrainData.MmiNCaptionTrainset.Value = 0x1;        // 1
            //SITR.ETCS1.CurrentTrainData.MmiXCaptionTrainset.Value = (char)10;   // LF
            //SITR.ETCS1.CurrentTrainData.MmiNDataElements.Value = 0x0;           // 0
            //SITR.ETCS1.CurrentTrainData.MmiNidData.Value = 0x3;                 // 3
            //SITR.ETCS1.CurrentTrainData.MmiQDataCheck.Value = 0x31;             // 49
            //SITR.ETCS1.CurrentTrainData.MmiNText.Value = 0x3630;                // 13872
            //SITR.ETCS1.CurrentTrainData.MmiLPacket.Value = 232;
            //SITR.SMDCtrl.ETCS1.CurrentTrainData.Value = 1;
            ////SITR.SMDCtrl.ETCS1.CurrentTrainData.Value = 0;

            //// Receive telegram EVC-107

            ////Send EVC-6 MMI_CURRENT TRAIN_DATA
            //SITR.ETCS1.CurrentTrainData.MmiMPacket.Value = 6;
            //SITR.ETCS1.CurrentTrainData.MmiMDataEnable.Value = 0x7f00;          // 32512
            //SITR.ETCS1.CurrentTrainData.MmiLTrain.Value = 0x64;                 // 100 metres
            //SITR.ETCS1.CurrentTrainData.MmiVMaxtrain.Value = 0xa0;              // 160
            //SITR.ETCS1.CurrentTrainData.MmiNidKeyTrainCat.Value = 0x3;          // 3
            //SITR.ETCS1.CurrentTrainData.MmiMBrakePerc.Value = 0x46;             // 70
            //SITR.ETCS1.CurrentTrainData.MmiNidKeyAxleLoad.Value = 0x15;         // 21
            //SITR.ETCS1.CurrentTrainData.MmiMAirtight.Value = 0x0;               // 0
            //SITR.ETCS1.CurrentTrainData.MmiNidKeyLoadGauge.Value = 0x26;        // 38
            //SITR.ETCS1.CurrentTrainData.MmiMButtons.Value = 0xff;               // 255
            //SITR.ETCS1.CurrentTrainData.EVC6alias1.Value = 0x0;                 // 0
            //SITR.ETCS1.CurrentTrainData.MmiNTrainset.Value = 0x0;               // 0
            //SITR.ETCS1.CurrentTrainData.MmiNCaptionTrainset.Value = 0x1;        // 1
            //SITR.ETCS1.CurrentTrainData.MmiXCaptionTrainset.Value = (char)11;   // Vertical tab
            //SITR.ETCS1.CurrentTrainData.MmiNDataElements.Value = 0x0;           // 0
            //SITR.ETCS1.CurrentTrainData.MmiNidData.Value = 0x1;                 // 1
            //SITR.ETCS1.CurrentTrainData.MmiQDataCheck.Value = 0x41;             // 65
            //SITR.ETCS1.CurrentTrainData.MmiLPacket.Value = 216;
            //SITR.SMDCtrl.ETCS1.CurrentTrainData.Value = 1;
            ////SITR.SMDCtrl.ETCS1.CurrentTrainData.Value = 0;

            //// Receive telegram EVC-107

            ////Send EVC-6 MMI_CURRENT TRAIN_DATA
            //SITR.ETCS1.CurrentTrainData.MmiMPacket.Value = 6;
            //SITR.ETCS1.CurrentTrainData.MmiMDataEnable.Value = 0x7f00;          // 32512
            //SITR.ETCS1.CurrentTrainData.MmiLTrain.Value = 0x64;                 // 100 metres
            //SITR.ETCS1.CurrentTrainData.MmiVMaxtrain.Value = 0xa0;              // 160
            //SITR.ETCS1.CurrentTrainData.MmiNidKeyTrainCat.Value = 0x3;          // 3
            //SITR.ETCS1.CurrentTrainData.MmiMBrakePerc.Value = 0x46;             // 70
            //SITR.ETCS1.CurrentTrainData.MmiNidKeyAxleLoad.Value = 0x15;         // 21
            //SITR.ETCS1.CurrentTrainData.MmiMAirtight.Value = 0x0;               // 0
            //SITR.ETCS1.CurrentTrainData.MmiNidKeyLoadGauge.Value = 0x26;        // 38
            //SITR.ETCS1.CurrentTrainData.MmiMButtons.Value = 0xff;               // 255
            //SITR.ETCS1.CurrentTrainData.EVC6alias1.Value = 0x0;                 // 0
            //SITR.ETCS1.CurrentTrainData.MmiNTrainset.Value = 0x0;               // 0
            //SITR.ETCS1.CurrentTrainData.MmiNCaptionTrainset.Value = 0x1;        // 1
            //SITR.ETCS1.CurrentTrainData.MmiXCaptionTrainset.Value = (char)12;   // 
            //SITR.ETCS1.CurrentTrainData.MmiNDataElements.Value = 0x0;           // 0
            //SITR.ETCS1.CurrentTrainData.MmiNidData.Value = 0x2;                 // 2
            //SITR.ETCS1.CurrentTrainData.MmiQDataCheck.Value = 0x4e;             // 78
            //SITR.ETCS1.CurrentTrainData.MmiLPacket.Value = 224;
            //SITR.SMDCtrl.ETCS1.CurrentTrainData.Value = 1;
            ////SITR.SMDCtrl.ETCS1.CurrentTrainData.Value = 0;

            //// Receive telegram EVC-107

            ////Send EVC-6 MMI_CURRENT TRAIN_DATA
            //SITR.ETCS1.CurrentTrainData.MmiMPacket.Value = 6;
            //SITR.ETCS1.CurrentTrainData.MmiMDataEnable.Value = 0x7f00;          // 32512
            //SITR.ETCS1.CurrentTrainData.MmiLTrain.Value = 0x64;                 // 100 metres
            //SITR.ETCS1.CurrentTrainData.MmiVMaxtrain.Value = 0xa0;              // 160
            //SITR.ETCS1.CurrentTrainData.MmiNidKeyTrainCat.Value = 0x3;          // 3
            //SITR.ETCS1.CurrentTrainData.MmiMBrakePerc.Value = 0x46;             // 70
            //SITR.ETCS1.CurrentTrainData.MmiNidKeyAxleLoad.Value = 0x15;         // 21
            //SITR.ETCS1.CurrentTrainData.MmiMAirtight.Value = 0x0;               // 0
            //SITR.ETCS1.CurrentTrainData.MmiNidKeyLoadGauge.Value = 0x26;        // 38
            //SITR.ETCS1.CurrentTrainData.MmiMButtons.Value = 0xff;               // 255
            //SITR.ETCS1.CurrentTrainData.EVC6alias1.Value = 0x0;                 // 0
            //SITR.ETCS1.CurrentTrainData.MmiNTrainset.Value = 0x0;               // 0
            //SITR.ETCS1.CurrentTrainData.MmiNCaptionTrainset.Value = 0x1;        // 1
            //SITR.ETCS1.CurrentTrainData.MmiXCaptionTrainset.Value = (char)13;   // 
            //SITR.ETCS1.CurrentTrainData.MmiNDataElements.Value = 0x0;           // 0
            //SITR.ETCS1.CurrentTrainData.MmiNidData.Value = 0x9;                 // 9
            //SITR.ETCS1.CurrentTrainData.MmiQDataCheck.Value = 0x4f;             // 79
            //SITR.ETCS1.CurrentTrainData.MmiNText.Value = 0x7574;                // 30068
            //SITR.ETCS1.CurrentTrainData.MmiXText.Value = (char)32;              // 
            //SITR.ETCS1.CurrentTrainData.MmiLPacket.Value = 280;
            //SITR.SMDCtrl.ETCS1.CurrentTrainData.Value = 1;
            ////SITR.SMDCtrl.ETCS1.CurrentTrainData.Value = 0;

            //Receive packet EVC-107

            //Send EVC-30 MMI_REQUEST_ENABLE
            SendEVC30_MMIRequestEnable(255, 0b0001_1101_0000_0011_1111_0000_0011_1110);

            //Send EVC-16 MMI_CURRENT_TRAIN_NUMBER
            SendEVC16_CurrentTrainNumber(0xffffffff);

            //Receive packet EVC-116 MMI_NEW_TRAIN_NUMBER

            //Send Cab active with echoed train number
            SendEVC2_MMIStatus_Cab1Active(0xffffffff);

            //Send EVC-30 MMI_ENABLE_REQUEST
            SendEVC30_MMIRequestEnable(255, 0b0001_1101_0000_0011_1111_0000_0011_1111);

            //Receive packet EVC-101 MMI_DRIVER_REQUEST (Driver presses Start Button)

            //send EVC-8 MMI_DRIVER_MESSAGE
            SendEVC8_MMIDriverMessage(true, 1, 1, 263);             // "#3 MO10 (Ack Staff Responsible Mode)"

            //Receive packet EVC-111 MMI_DRIVER_MESSAGE_ACK (Driver acknowledges SR Mode)

            return GlobalTestResult;
        }

        public void Initialize_DynamicValues()
        {
            TraceInfo("Set Initial Dynamic values");

            SITR.ETCS1.Dynamic.EVC1alias1.Value = 0;
            SITR.ETCS1.Dynamic.MmiVTrain.Value = 0;
            SITR.ETCS1.Dynamic.MmiATrain.Value=0;
            SITR.ETCS1.Dynamic.MmiVTarget.Value=-1;
            SITR.ETCS1.Dynamic.MmiVPermitted.Value=0;
            SITR.ETCS1.Dynamic.MmiVIntervention.Value=-1;
            SITR.ETCS1.Dynamic.MmiVRelease.Value=-1;
            SITR.ETCS1.Dynamic.MmiOBraketarget.Value=-1;
            SITR.ETCS1.Dynamic.MmiOIml.Value=-1;
            SITR.ETCS1.Dynamic.EVC01Validity1.Value=0xc800;     // 51200 in decimal
            SITR.ETCS1.Dynamic.EVC01Validity1.Value=0xff00;     // 65280 in decimal
            SITR.ETCS1.Dynamic.EVC01SSW1.Value=0x8000;          // 32768 in decimal
            SITR.ETCS1.Dynamic.EVC01SSW2.Value=0x8000;          // 32768 in decimal
            SITR.ETCS1.Dynamic.EVC01SSW3.Value=0x8000;          // 32768 in decimal
            SITR.ETCS1.Dynamic.SDT.UDV.Value=0x1;               // 1 in decimal                      
        }

        public void SendEVC0_MMIStartATP_VersionInfo()
        {
            TraceInfo("ETCS->DMI: EVC-0 (MMI_START_ATP) \"Version info request\"");

            SITR.ETCS1.StartAtp.MmiMPacket.Value=0;
            SITR.ETCS1.StartAtp.MmiMStartReq.Value=0;
            SITR.ETCS1.StartAtp.MmiLPacket.Value=40;
            SITR.SMDCtrl.ETCS1.StartAtp.Value = 1;
        }

        public void SendEVC0_MMIStartATP_GoToIdle()
        {
            TraceInfo("ETCS->DMI: EVC-0 (MMI_START_ATP) \"Go to Idle state\"");
           
            SITR.ETCS1.StartAtp.MmiMPacket.Value=0;
            SITR.ETCS1.StartAtp.MmiMStartReq.Value=1;
            SITR.ETCS1.StartAtp.MmiLPacket.Value=40;
            SITR.SMDCtrl.ETCS1.StartAtp.Value = 1;            
        }

        public void SendEVC2_MMIStatus_Cab1Active(uint TrainNumber)
        {
            TraceInfo("ETCS->DMI: EVC-2 (MMI_STATUS) \"Cab 1 Active\"");
            
            SITR.ETCS1.Status.MmiMPacket.Value=2;
            SITR.ETCS1.Status.MmiLPacket.Value=72;
            SITR.ETCS1.Status.MmiNidOperation.Value= TrainNumber; //Train running number 4 294 967 295
            SITR.ETCS1.Status.EVC2alias1.Value=16;              //Cab 1 active
            SITR.SMDCtrl.ETCS1.Status.Value = 1;
        }

        /// <summary>
        /// Sends EVC-14 Current Driver ID telegram with enable/disable options for the TRN, Settings, and Close buttons.
        /// </summary>
        /// <param name="strDriverID">
        /// Current Driver ID.</param>
        /// <param name="blTRNButtonEnabled">
        /// Enable/disable TRN button.</param>
        /// <param name="blSettingsButtonEnabled">
        /// Enable/disable settings button.</param>
        /// <param name="blCloseButtonEnabled">
        /// Enable/disable Close button.</param>
        public void SendEVC14_MMICurrentDriverID(string strDriverID, bool blTRNButtonEnabled, bool blSettingsButtonEnabled, bool blCloseButtonEnabled)
        {
            TraceInfo("ETCS->DMI: EVC-14 (MMI_CURRENT_DRIVER_ID), Driver ID = {0}, TRN button enabled: {1}, Settings button enabled: {2}, Close Enabled: {3}",
                                                                    strDriverID, blTRNButtonEnabled, blSettingsButtonEnabled, blCloseButtonEnabled);

            //convert boolean to uint for bit shifting
            uint uintTRNButton = Convert.ToUInt32(blTRNButtonEnabled);
            uintTRNButton = uintTRNButton << 7;

            //convert boolean to uint for bit shifting
            uint uintSettingsButton = Convert.ToUInt32(blSettingsButtonEnabled);
            uintSettingsButton = uintSettingsButton << 6;

            //combined "TRN" and "Settings" button bit-masks
            byte MmiQAddEnable = Convert.ToByte(uintTRNButton | uintSettingsButton);

            SITR.ETCS1.CurrentDriverId.MmiMPacket.Value=14;
            SITR.ETCS1.CurrentDriverId.MmiLPacket.Value=172;
            SITR.ETCS1.CurrentDriverId.MmiQCloseEnable.Value= Convert.ToByte(blCloseButtonEnabled);
            SITR.ETCS1.CurrentDriverId.MmiQAddEnable.Value = MmiQAddEnable;
            SITR.ETCS1.CurrentDriverId.MmiXDriverId.Value=strDriverID;
            SITR.SMDCtrl.ETCS1.CurrentDriverId.Value=1;
        } 

        /// <summary>
        /// SendEVC8_MMIDriver_Message
        /// Sends pre-programmed Driver text message
        /// <param name="blImportant">
        /// Used to indicate whether message is important (True) or Auxilliary (False).</param>
        /// <param name="MMI_Q_Text_Criteria">
        /// Message display type: <br/>
        /// 0 = "Add text/symbol with ACK prompt, to be kept after ACK."
        /// 1 = "Add text/symbol with ACK prompt, to be removed after ACK."
        /// 2 = "Add text with ACK/NACK prompt, to be removed after ACK/NACK."
        /// 3 = "Add informative text/symbol."
        /// 4 = "Remove text/symbol. Text/symbol to be removed is defined by MMI_I_TEXT."
        /// 5 = "Text still incomplete. Another instance of EVC-8 follows."</param>
        /// <param name="MMI_I_Text">
        /// Unique text message ID.</param>
        /// <param name="MMI_Q_Text">
        /// Pre-defined text message ID.</param>
        /// </summary>
        // 0 = "Level crossing not protected"
        // 1 = "Acknowledgement"
        // 2..255 = "Reserved for application specific coded text messages from wayside packet #76."    
        // 256 = "#1 (plain text only)"                                                                 
        // 257 = "#3 LE07/LE11/LE13/LE15 (Ack Transition to Level #4)"                                  
        // 258 = "#3 LE09 (Ack Transition to NTC #2)"                                                   
        // 259 = "#3 MO08 (Ack On Sight Mode)"                                                          
        // 260 = "#3 ST01 (Brake intervention)"                                                         
        // 261 = "Spare"                                                                                
        // 262 = "#3 MO15 (Ack Reversing Mode)"                                                         
        // 263 = "#3 MO10 (Ack Staff Responsible Mode)"                                                 
        // 264 = "#3 MO17 (Ack Unfitted Mode)"                                                          
        // 265 = "#3 MO02 (Ack Shunting ordered by Trackside)"                                          
        // 266 = "#3 MO05 (Ack Train Trip)"                                                             
        // 267 = "Balise read error"                                                                    
        // 268 = "Communication error"                                                                  
        // 269 = "Runaway movement"                                                                     
        // 270..272 = "Spare"                                                                           
        // 273 = "Unauthorized passing of EOA / LOA"                                                    
        // 274 = "Entering FS"                                                                          
        // 275 = "Entering OS"                                                                          
        // 276 = "#3 LE06/LE10/LE12/LE14 (Transition to Level #4)"                                      
        // 277 = "#3 LE08 (Transition to NTC #2)"                                                       
        // 278 = "Emergency Brake Failure"                                                              
        // 279 = "Apply brakes"                                                                         
        // 280 = " Emergency stop"                                                                      
        // 281 = "Spare"                                                                                
        // 282 = "#3 ST04 (Connection Lost/Set-Up failed)"                                              
        // 283..285 = "Spare"                                                                           
        // 286 = "#3 ST06 (Reversing is possible)"                                                      
        // 287..289 = "Spare"                                                                           
        // 290 = "SH refused"                                                                           
        // 291 = "Spare"                                                                                
        // 292 = "SH request failed"                                                                    
        // 293..295 = "Spare"                                                                           
        // 296 = "Trackside not compatible"                                                             
        // 297 = "Spare"                                                                                
        // 298 = "#3 DR02 (Confirm Track Ahead Free)"                                                   
        // 299 = "Train is rejected"                                                                    
        // 300 = "No MA received at level transition"                                                   
        // 301..304 = "Spare"                                                                           
        // 305 = "Train divided"                                                                        
        // 306..309 = "Spare"                                                                           
        // 310 = "Train data changed"                                                                   
        // 311..314 = "Spare"                                                                           
        // 315 = "SR distance exceeded"                                                                 
        // 316 = "SR stop order"                                                                        
        // 317..319 = "Spare"                                                                           
        // 320 = "RV distance exceeded"                                                                 
        // 321 = "ETCS Isolated"                                                                        
        // 322..513 = "Spare"                                                                           
        // 514 = "Perform Brake Test!"                                                                  
        // 515 = "Unable to start Brake Test"                                                           
        // 516 = "Brake Test in Progress"                                                               
        // 517 = "Brake Test failed, perform new Test!"                                                 
        // 518..519 = "Spare"                                                                           
        // 520 = "LZB Partial Block Mode"                                                               
        // 521 = "Override LZB Partial Block Mode"                                                      
        // 522 = "Restriction #1 km/h in Release Speed Area"                                            
        // 523 = "Spare"                                                                                
        // 524 = "Brake Test successful"                                                                
        // 525 = "Brake Test timeout in #1 Hours"                                                       
        // 526 = "Brake Test Timeout"                                                                   
        // 527 = "Brake Test aborted, perform new Test?"                                                
        // 528 = "Apply Brakes!"                                                                        
        // 529..530 = "Spare"                                                                           
        // 531 = "BTM Test in Progress"                                                                 
        // 532 = "BTM Test Failure"                                                                     
        // 533 = "BTM Test Timeout"                                                                     
        // 534 = "BTM Test Timeout in #1 hours"                                                         
        // 535 = "ATP Restart required in #1 Hours"                                                     
        // 536 = "Restart ATP!"                                                                         
        // 537..539 = "Spare"                                                                           
        // 540 = "No Level available Onboard"                                                           
        // 541..542 = "Spare"                                                                           
        // 543 = "#2 failed"                                                                            
        // 544 = "Spare"                                                                                
        // 545 = "#3 LE02A (Confirm LZB NTC)"                                                           
        // 546..551 = "Spare"                                                                           
        // 552 = "Announced level(s) not supported Onboard"                                             
        // 553 = "Spare"                                                                                
        // 554 = "Reactivate the Cabin!"                                                                
        // 555 = "#3 MO20 (Ack SN Mode)"                                                                
        // 556..559 = "Spare"                                                                           
        // 560 = "Trackside malfunction"                                                                
        // 561..562 = "Spare"                                                                           
        // 563 = "Trackside Level(s) not supported Onboard"                                             
        // 564 = "Confirm change of inhibit Level #1"                                                   
        // 565 = "Confirm change of inhibit STM #2"                                                     
        // 566..567 = "Spare"                                                                           
        // 568 = "#3 ST03 (Connection established)"                                                     
        // 569 = "Radio network registration failed"                                                    
        // 570 = "Shunting rejected due to #2 Trip"                                                     
        // 571 = "Spare"                                                                                
        // 572 = "No Track Description"                                                                 
        // 573 = "#2 needs data"                                                                        
        // 574 = "Cabin Reactivation required in #1 hours"                                              
        // 575..579 = "Spare"                                                                           
        // 580 = "Procedure Brake Percentage Entry terminated by ATP"                                   
        // 581 = "Procedure Wheel Diameter Entry terminated by ATP"                                     
        // 582 = "Procedure Doppler Radar Entry terminated by ATP"                                      
        // 583 = "Doppler error"                                                                        
        // 584..605 = "Spare"                                                                           
        // 606 = "SH Stop Order"                                                                        
        // 607..608 = "Spare"                                                                           
        // 609 = "#3 Symbol ST100 (Network registered via one modem)"                                   
        // 610 = "#3 Symbol ST102 (Network registered via two modems)"                                  
        // 613 = "#3 Symbol ST103 (Connection Up) "                                                     
        // 614 = "#3 Symbol ST03B (Connection Up with two RBCs)"                                        
        // 615 = "#3 Symbol ST03C (Connection Lost/Set-Up failed)"                                      
        // 616..620 = "Spare"                                                                           
        // 621 = "Unable to start Brake Test, vehicle not ready"                                        
        // 622 = "Unblock EB"                                                                           
        // 623 = "Spare"                                                                                
        // 624 = "ETCS Failure"                                                                         
        // 625 = "Tachometer error"                                                                     
        // 626 = "SDU error"                                                                            
        // 627 = "Speed Sensor failure"                                                                 
        // 628 = "ETCS Service Brake not available"                                                     
        // 629 = "ETCS Traction Cut-off not available"                                                  
        // 630 = "ETCS Isolation Switch failure"                                                        
        // 631 = "#2 Isolation input not recognized"                                                    
        // 632 = "Coasting input not recognised"                                                        
        // 633 = "Brake Bypass failure"                                                                 
        // 634 = "Special brake input failure"                                                          
        // 635 = "Juridical Recording not available"                                                    
        // 636 = "Euroloop not available"                                                               
        // 637 = "TIMS not available"                                                                   
        // 638 = "Degraded Radio service"                                                               
        // 639 = "No Radio connection possible"                                                         
        // 640..699 = "Spare"                                                                           
        // 700 = "#2 brake demand"                                                                      
        // 701 = "Route unsuitable – axle load category"                                                
        // 702 = "Route unsuitable – loading gauge"                                                     
        // 703 = "Route unsuitable – traction system"                                                   
        // 704 = "#2 is not available"                                                                  
        // 705 = "New power-up required in #1 hours"                                                    
        // 706 = "No valid authentication key"                                                          
        // 707 = "Spare"                                                                                
        // 708 = "Spare"                                                                                
        // 709 = "#3 MO22 (Acknowledgement for Limited Supervision)"                                    
        // 710 = "#3 (Train divided)"                                                                   
        // 711 = "NL-input signal is withdrawn"                                                         
        // 712 = "Wheel data settings were successfully changed"                                        
        // 713 = "Doppler radar settings were successfully changed"                                     
        // 714 = "Brake percentage was successfully changed"                                            
        // 715 = "No Country Selection in LZB PB Mode"                                                  
        // 716 = "#3 Symbol ST05 (hour glass)"
        public void SendEVC8_MMIDriverMessage(bool blImportant, ushort MMI_Q_Text_Criteria, byte MMI_I_Text, ushort MMI_Q_Text)
        {
            TraceInfo("ETCS->DMI: EVC-8 (MMI_Driver_Message) MMI_Q_Text_Class = {0}, MMI_Q_Text_Criteria = {1}, MMI_I_Text = {2}, MMI_Q_Text = {3}", 
                                                                                    blImportant, MMI_Q_Text_Criteria, MMI_I_Text, MMI_Q_Text);

            SITR.ETCS1.DriverMessage.MmiMPacket.Value = 8;                  // Packet ID

            uint byteImportant = Convert.ToUInt32(blImportant);             // True = Important, False = Auxilliary
            byteImportant = byteImportant << 7;

            byte EVC8_alias_1 = Convert.ToByte(byteImportant | MMI_Q_Text_Criteria);

            SITR.ETCS1.DriverMessage.EVC8alias1.Value = EVC8_alias_1;       
            SITR.ETCS1.DriverMessage.MmiIText.Value = MMI_I_Text;           // ID number
            SITR.ETCS1.DriverMessage.MmiNText.Value = 0x0;                  // Number of customs text characters. i.e. 0
            SITR.ETCS1.DriverMessage.MmiQText.Value = MMI_Q_Text;           // Pre-defined text message number (see above)
            SITR.ETCS1.DriverMessage.MmiLPacket.Value = 80;                 // Packet length
            SITR.SMDCtrl.ETCS1.DriverMessage.Value = 1;                     // Send packet
        }

        public void SendEVC16_CurrentTrainNumber(uint TrainNumber)
        {
            SITR.ETCS1.CurrentTrainNumber.MmiMPacket.Value = 16;
            SITR.ETCS1.CurrentTrainNumber.MmiLPacket.Value = 64;
            SITR.ETCS1.CurrentTrainNumber.MmiNidOperation.Value = TrainNumber;
            SITR.SMDCtrl.ETCS1.CurrentTrainNumber.Value = 1;
        }

        /// <summary>
        ///     SendEVC6_MMI_Current_Train_Data
        ///     Sends existing Train Data values to the DMI
        /// <param name="MMI_Q_Data_Enable">Enable mask:
        ///     Bits:
        ///     0 = "Train Set ID"
        ///     1 = "Train Category"
        ///     2 = "Train Length"
        ///     3 = "Brake Percentage"
        ///     4 = "Max. Train Speed"
        ///     5 = "Axle Load Category"
        ///     6 = "Airtightness"
        ///     7 = "Loading Gauge"
        ///     8..15 = "Spare"</param>
        /// <param name="MMI_L_Train">Max train length</param>
        /// <param name="MMI_V_MaxTrain">Max train speed</param>
        /// <param name="MMI_Nid_Key_Train_Cat">Train category (range 3-20)
        ///     Values:
        ///     3 = "PASS 1"
        ///     4 = "PASS 2"
        ///     5 = "PASS 3"
        ///     6 = "TILT 1"
        ///     7 = "TILT 2"
        ///     8 = "TILT 3"
        ///     9 = "TILT 4"
        ///     10 = "TILT 5"
        ///     11 = "TILT 6"
        ///     12 = "TILT 7"
        ///     13 = "FP 1"
        ///     14 = "FP 2"
        ///     15 = "FP 3"
        ///     16 = "FP 4"
        ///     17 = "FG 1"
        ///     18 = "FG 2"
        ///     19 = "FG 3"
        ///     20 = "FG 4" </param>
        /// <param name="MMI_M_Brake_Perc">Brake percentage</param>
        /// <param name="MMI_Nid_Key_Axle_Load">Axle load category (range 21-33)
        ///     Values:
        ///     21 = "A"
        ///     22 = "HS17"
        ///     23 = "B1"
        ///     24 = "B2"
        ///     25 = "C2"
        ///     26 = "C3"
        ///     27 = "C4"
        ///     28 = "D2"
        ///     29 = "D3"
        ///     30 = "D4"
        ///     31 = "D4XL"
        ///     32 = "E4"
        ///     33 = "E5"
        /// </param>    
        /// <param name="MMI_M_Airtight">Train equipped with airtight system</param>
        /// <param name="MMI_Nid_Key_Load_Gauge">Axle load category (range 34-38)
        ///     Values:
        ///     34 = "G1"
        ///     35 = "GA"
        ///     36 = "GB"
        ///     37 = "GC"
        ///     38 = "Out of GC"
        /// <param name="MMI_M_Buttons">Intended to be used to dstinguish between 'BTN_YES_DATA_ENTRY_COMPLETE', 'BTN_YES_DATA_ENTRY_COMPLETE_DELAY_TYPE','no button' </param>    
        /// <param name="MMI_M_Trainset_ID">ID of preconfigured train data set</param>
        /// <param name="MMI_M_Alt_Dem">Control variable for alternative train data entry method</param>
        /// <param name="MMI_N_Trainsets">Number of trainsets to be shown for fixed TDE</param>
        /// <param name="MMI_N_Data_Elements">Number of entries in the following array</param>
        public void SendEVC6_MMICurrentTrainData(ushort MMI_M_Data_Enable, ushort MMI_L_Train, ushort MMI_V_MaxTrain, byte MMI_Nid_Key_Train_Cat,
            byte MMI_M_Brake_Perc, byte MMI_Nid_Key_Axle_Load, byte MMI_M_Airtight, byte MMI_Nid_Key_Load_Gauge, byte MMI_M_Buttons,
            uint MMI_M_Trainset_ID, uint MMI_M_Alt_Dem, ushort MMI_N_Trainsets, ushort MMI_N_Data_Elements)

        {
            TraceInfo("ETCS->DMI: EVC-6 (MMI_Current_Train_Data) MMI_M_Data_Enable {0}, MMI_L_Train {1}, MMI_V_MaxTrain {2}, MMI_Nid_Key_Train_Cat {3}, MMI_M_Brake_Perc {4}, MMI_Nid_Key_Axle_Load {5}, MMI_M_Airtight {6}, MMI_Nid_Key_Load_Gauge {7}, MMI_M_Buttons {8}, MMI_M_Trainset_ID {9}, MMI_M_Alt_Dem {10}, MMI_N_Trainsets {11}, MMI_N_Data,Elements {12}",
                MMI_M_Data_Enable, MMI_L_Train, MMI_V_MaxTrain, MMI_Nid_Key_Train_Cat, MMI_M_Brake_Perc, MMI_Nid_Key_Axle_Load, MMI_M_Airtight, MMI_Nid_Key_Load_Gauge, MMI_M_Buttons, MMI_M_Trainset_ID, MMI_M_Alt_Dem, MMI_N_Trainsets, MMI_N_Data_Elements);

            SITR.ETCS1.CurrentTrainData.MmiMPacket.Value = 6;                               // Packet ID

            // Train data enabled
            ushort Reversed_MMI_M_Data_Enable = BitReverser16(MMI_M_Data_Enable);
            SITR.ETCS1.CurrentTrainData.MmiMDataEnable.Value = Reversed_MMI_M_Data_Enable;    
                   
            SITR.ETCS1.CurrentTrainData.MmiLTrain.Value = MMI_L_Train;                      // Train length
            SITR.ETCS1.CurrentTrainData.MmiVMaxtrain.Value = MMI_V_MaxTrain;                // Max train speed
            SITR.ETCS1.CurrentTrainData.MmiNidKeyTrainCat.Value = MMI_Nid_Key_Train_Cat;    // Train category
            SITR.ETCS1.CurrentTrainData.MmiMBrakePerc.Value = MMI_M_Brake_Perc;             // Brake percentage
            SITR.ETCS1.CurrentTrainData.MmiNidKeyAxleLoad.Value = MMI_Nid_Key_Axle_Load;    // Axle load category
            SITR.ETCS1.CurrentTrainData.MmiMAirtight.Value = MMI_M_Airtight;                // Train equipped with airtight system
            SITR.ETCS1.CurrentTrainData.MmiNidKeyLoadGauge.Value = MMI_Nid_Key_Load_Gauge;  // Loading gauge type of train 
            SITR.ETCS1.CurrentTrainData.MmiMButtons.Value = MMI_M_Buttons;                  // Button available
            //implementing EVC6_alias_1
            MMI_M_Trainset_ID = MMI_M_Trainset_ID << 4;
            MMI_M_Alt_Dem = MMI_M_Alt_Dem << 2;
            byte EVC6_alias_1 = Convert.ToByte(MMI_M_Trainset_ID | MMI_M_Alt_Dem);
            SITR.ETCS1.CurrentTrainData.EVC6alias1.Value = EVC6_alias_1;

            SITR.ETCS1.CurrentTrainData.MmiNTrainset.Value = MMI_N_Trainsets;               // Number of trainset
            SITR.ETCS1.CurrentTrainData.MmiNDataElements.Value = MMI_N_Data_Elements;       // Number of train data to enter

            // Packet length
            SITR.ETCS1.CurrentTrainData.MmiLPacket.Value = Convert.ToUInt16(176 + MMI_N_Trainsets * 16 + MMI_N_Data_Elements * 32);

            SITR.SMDCtrl.ETCS1.CurrentTrainData.Value = 1;                                  // Send packet
        }

        /// <summary>
        /// Sends EVC-30 packet for specifying which window the DMI should display
        /// and which buttons are enabled.
        /// </summary>
        /// <param name="MMINidWindow">
        /// Window ID</param>
        /// <param name="MMI_Q_Request_Enable">
        /// Bits 31 to 0 of MMI_Q_Request_Enable</param>
        //  MMI_NID_WINDOW
        //  0 = "Default"
        //  1 = "Main"
        //  2 = "Override"
        //  3 = "Special"
        //  4 = "Settings"
        //  5 = "RBC contact"
        //  6 = "Train running number"
        //  7 = "Level"
        //  8 = "Driver ID"
        //  9 = "radio network ID"
        //  10 = "RBC data"
        //  11 = "Train data"
        //  12 = "SR speed/distance"
        //  13 = "Adhesion"
        //  14 = "Set VBC"
        //  15 = "Remove VBC"
        //  16 = "Train data validation"
        //  17 = "Set VBC validation"
        //  18 = "Remove VBC validation"
        //  19 = "Data View"
        //  20 = "System version"
        //  21 = "NTC data entry selection"
        //  22 = "NTC X data"
        //  23 = "NTC X data validation"
        //  24 = "NTC X data view"
        //  25..252 = "Spare"
        //  253 = "Language"
        //  254 = "close current window, return to parent"
        //  255 = "no window specified"
        //
        //  MMI_Q_Request_Enable
        //  0 = "Start"
        //  1 = "Driver ID"
        //  2 = "Train data"
        //  3 = "Level"
        //  4 = "Train running number"
        //  5 = "Shunting"
        //  6 = "Exit Shunting"
        //  7 = "Non-Leading"
        //  8 = "Maintain Shunting"
        //  9 = "EOA"
        //  10 = "Adhesion"
        //  11 = "SR speed / distance"
        //  12 = "Train integrity"
        //  13 = "Language"
        //  14 = "Volume"
        //  15 = "Brightness"
        //  16 = "System version"
        //  17 = "Set VBC"
        //  18 = "Remove VBC"
        //  19 = "Contact last RBC"
        //  20 = "Use short number"
        //  21 = "Enter RBC data"
        //  22 = "Radio Network ID"
        //  23 = "Geographical position"
        //  24 = "End of data entry (NTC)"
        //  25 = "Set local time, date and offset"
        //  26 = "Set local offset"
        //  27 = "Reserved"
        //  28 = "Start Brake Test"
        //  29 = "Enable wheel diameter"
        //  30 = "Enable doppler"
        //  31 = "Enable brake percentage"
        //  32 = "System info"
        public void SendEVC30_MMIRequestEnable(byte MMINidWindow, uint MMI_Q_Request_Enable)
        {
            uint Reversed_MMI_Q_Request_Enable = BitReverser32(MMI_Q_Request_Enable);

            TraceInfo("ETCS->DMI: EVC-30 (MMI_Request_Enable) MMI Window ID: {0}, MMI Q Request (bit 31 to 0): {1}", 
                                                                            MMINidWindow, Reversed_MMI_Q_Request_Enable);

            SITR.ETCS1.EnableRequest.MmiMPacket.Value = 30;
            SITR.ETCS1.EnableRequest.MmiNidWindow.Value = MMINidWindow;
            SITR.ETCS1.EnableRequest.MmiQRequestEnable.Value = new uint[2] { Reversed_MMI_Q_Request_Enable, 0x80000000 };
            SITR.ETCS1.EnableRequest.MmiLPacket.Value = 128;
            SITR.SMDCtrl.ETCS1.EnableRequest.Value = 1;
        }
        
        /// <summary>
        /// Bit-reverses a 32-bit number
        /// </summary>
        /// <param name="IntToBeReversed"></param>
        /// <returns>Reversed 32-bit uint</returns>
        public uint BitReverser32(uint IntToBeReversed)
        {
            uint y = 0;

            for (int i=0; i<32; i++)
            {
                y <<= 1;
                y |= (IntToBeReversed & 1);
                IntToBeReversed >>= 1;
            }

            return y;
        }

        /// <summary>
        /// Bit-reverses a 16-bit number
        /// </summary>
        /// <param name="IntToBeReversed"></param>
        /// <returns>Reversed 16-bit uint</returns>
        public ushort BitReverser16(ushort IntToBeReversed)
        {
            int y = 0;

            for (int i = 0; i < 16; i++)
            {
                y <<= 1;
                y |= (IntToBeReversed & 1);
                IntToBeReversed >>= 1;
            }

            ushort reversedInt  = Convert.ToUInt16(y);
            return reversedInt;
        }
    }
}

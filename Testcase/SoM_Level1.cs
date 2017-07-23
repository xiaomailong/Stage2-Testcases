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
            SendEVC30_MMIRequestEnable(255, 0b0001_1101_0000_0011_1110_0000_0011_1110);

            //ETCS->DMI: EVC-20 MMI_SELECT_LEVEL
            SendEVC20_MMISelectLevel_AllLevels();

            //ETCS->DMI: Send EVC-6 MMI_CURRENT TRAIN_DATA

            SendEVC6_MMICurrentTrainData_FixedDataEntry(new[] {"FLU", "RLU", "Rescue"}, 2);

            //SendEVC6_MMICurrentTrainData(param_EVC6_MmiMDataEnable, param_EVC6_MmiLTrain, param_EVC6_MmiVMaxtrain, param_EVC6_MmiNidKeyTrainCat,
            //    param_EVC6_MmiMBrakePerc, param_EVC6_MmiNidKeyAxleLoad, param_EVC6_MmiMAirtight, param_EVC6_MmiNidKeyLoadGauge,
            //    param_EVC6_MmiMButtons, param_EVC6_MTrainsetId, param_EVC6_MAltDem, param_EVC6_MmiNTrainsets, param_EVC6_MmiNCaptionTrainset,
            //    param_EVC6_MmiXCaptionTrainset, param_EVC6_MmiNDataElements, null, null, null, null);

            //ETCS->DMI: Send EVC-10 MMI_ECHOED_TRAIN_DATA
            //SendEVC10_MMIEchoedTrainData();

            ////Receive EVC-101

            // Send EVC-30 MMI_REQUEST_ENABLE
            SendEVC30_MMIRequestEnable(255, 0b0001_1101_0000_0011_1111_0000_0011_1110);

            // Send EVC-16 MMI_CURRENT_TRAIN_NUMBER
            SendEVC16_CurrentTrainNumber(0xffffffff);

            // Receive packet EVC-116 MMI_NEW_TRAIN_NUMBER

            // Send Cab active with echoed train number
            EVC2_MMIStatus.TrainRunningNumber = 0xffffffff;
            EVC2_MMIStatus.Send();

            // Send EVC-30 MMI_ENABLE_REQUEST
            SendEVC30_MMIRequestEnable(255, 0b0001_1101_0000_0011_1111_0000_0011_1111);

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
        public void Initialize_DynamicArrays()
        {
            SITR.SMDCtrl.ETCS1.SelectLevel.Value = 0x8;
            SITR.SMDCtrl.ETCS1.SetVbc.Value = 0x8;
            SITR.SMDCtrl.ETCS1.RemoveVbc.Value = 0x8;
            SITR.SMDCtrl.ETCS1.TrackDescription.Value = 0x8;

            SITR.SMDCtrl.ETCS1.EchoedTrainData.Value = 0x8;
        }

        /// <summary>
        ///     SendEVC6_MMI_Current_Train_Data
        ///     Sends existing Train Data values to the DMI
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
        /// </param>
        /// <param name="MMI_M_Buttons">Intended to be used to dstinguish between 'BTN_YES_DATA_ENTRY_COMPLETE', 'BTN_YES_DATA_ENTRY_COMPLETE_DELAY_TYPE','no button' </param>    
        /// <param name="MMI_M_Trainset_ID">ID of preconfigured train data set</param>
        /// <param name="MMI_M_Alt_Dem">Control variable for alternative train data entry method</param>
        /// <param name="MMI_N_Trainsets">Number of trainsets to be shown for fixed TDE</param>
        /// <param name="MMI_N_Data_Elements">Number of entries in the following array</param>
        /// </summary>
        public void SendEVC6_MMICurrentTrainData(MMI_M_DATA_ENABLE MMI_M_Data_Enable, ushort MMI_L_Train,
            ushort MMI_V_MaxTrain,
            MMI_NID_KEY MMI_Nid_Key_Train_Cat,
            byte MMI_M_Brake_Perc, MMI_NID_KEY MMI_Nid_Key_Axle_Load, byte MMI_M_Airtight,
            MMI_NID_KEY MMI_Nid_Key_Load_Gauge,
            byte MMI_M_Buttons,
            ushort MMI_M_Trainset_ID, ushort MMI_M_Alt_Dem, string[] TrainSetCaptions,
            TrainDataElement[] trainDataElements)

        {
            // Train data enabled

            EVC6_MMICurrentTrainData.MMI_M_DATA_ENABLE = MMI_M_Data_Enable;

            EVC6_MMICurrentTrainData.MMI_L_TRAIN = MMI_L_Train; // Train length

            EVC6_MMICurrentTrainData.MMI_V_MAXTRAIN = MMI_V_MaxTrain; // Max train speed
            EVC6_MMICurrentTrainData.MMI_NID_KEY_TRAIN_CAT = MMI_Nid_Key_Train_Cat; // Train category
            EVC6_MMICurrentTrainData.MMI_M_BRAKE_PERC = MMI_M_Brake_Perc; // Brake percentage
            EVC6_MMICurrentTrainData.MMI_NID_KEY_AXLE_LOAD = MMI_Nid_Key_Axle_Load; // Axle load category
            EVC6_MMICurrentTrainData.MMI_M_AIRTIGHT = MMI_M_Airtight; // Train equipped with airtight system
            EVC6_MMICurrentTrainData.MMI_NID_KEY_LOAD_GAUGE =
                MMI_Nid_Key_Load_Gauge; // Loading gauge type of train 
            EVC6_MMICurrentTrainData.MMI_M_BUTTONS = MMI_M_Buttons; // Button available

            EVC6_MMICurrentTrainData.MMI_M_TRAINSET_ID = MMI_M_Trainset_ID;
            EVC6_MMICurrentTrainData.MMI_M_ALT_DEM = MMI_M_Alt_Dem;

            EVC6_MMICurrentTrainData.TrainSetCaptions = new List<string>(TrainSetCaptions);
            EVC6_MMICurrentTrainData.TrainDataElements = new List<TrainDataElement>(trainDataElements);


            TraceInfo(
                "ETCS->DMI: EVC-6 (MMI_Current_Train_Data)");

            EVC6_MMICurrentTrainData.Send();
        }

        /// <summary>
        /// Sends EVC-6 telegram with Fixed Data Entry for up to 9 trainset strings.
        /// </summary>
        /// <param name="Fixed_Trainset_Captions"> Array of strings for trainset captions</param>
        /// <param name="MMI_M_Trainset_ID">Index of trainset to be pre-selected on DMI</param>
        public void SendEVC6_MMICurrentTrainData_FixedDataEntry(string[] Fixed_Trainset_Captions,
            ushort MMI_M_Trainset_ID)

        {
            // Train data enabled
            EVC6_MMICurrentTrainData.MMI_M_DATA_ENABLE = MMI_M_DATA_ENABLE.TrainSetID; // "Train Set ID" data enabled

            EVC6_MMICurrentTrainData.MMI_L_TRAIN = 0; // Train length
            EVC6_MMICurrentTrainData.MMI_V_MAXTRAIN = 0; // Max train speed

            EVC6_MMICurrentTrainData.MMI_NID_KEY_TRAIN_CAT = MMI_NID_KEY.NoDedicatedKey; // Train category
            EVC6_MMICurrentTrainData.MMI_M_BRAKE_PERC = 0; // Brake percentage
            EVC6_MMICurrentTrainData.MMI_NID_KEY_AXLE_LOAD = MMI_NID_KEY.NoDedicatedKey; // Axle load category
            EVC6_MMICurrentTrainData.MMI_M_AIRTIGHT = 0; // Train equipped with airtight system
            EVC6_MMICurrentTrainData.MMI_NID_KEY_LOAD_GAUGE =
                MMI_NID_KEY.NoDedicatedKey; // Loading gauge type of train 
            EVC6_MMICurrentTrainData.MMI_M_BUTTONS = 0; // No Buttons available

            EVC6_MMICurrentTrainData.MMI_M_TRAINSET_ID = MMI_M_Trainset_ID; // Preselected Trainset ID
            // MMI_Alt_Dem = 0: No alternative train data entry method available

            EVC6_MMICurrentTrainData.TrainSetCaptions = new List<string>(Fixed_Trainset_Captions);
            EVC6_MMICurrentTrainData.TrainDataElements = new List<TrainDataElement>(); // no train data elements

            TraceInfo("ETCS->DMI: EVC-6 (MMI_Current_Train_Data)");
            EVC6_MMICurrentTrainData.Send();
        }


        /// <summary>
        /// Sends EVC-10 telegram with echoed train data. Reads all existing Current train data and bit-inverses them.
        /// </summary>
        public void SendEVC10_MMIEchoedTrainData()
        {
            // Packet ID
            SITR.ETCS1.EchoedTrainData.MmiMPacket.Value = 10;

            ushort EVC6_MmiNTrainset = SITR.ETCS1.CurrentTrainData.MmiNTrainset.Value;
            SITR.ETCS1.EchoedTrainData.MmiNTrainsetsR.Value = Convert.ToUInt16(~EVC6_MmiNTrainset);

            // Dynamic fields 1st dimension
            ushort numberOfCaptionTrainset = 0; // To be used for Packet length

            for (int k = 0; k < EVC6_MmiNTrainset; k++)
            {
                // Bit-inverted Trainset caption text length
                ushort EVC6_MmiNCaptionTrainset = Convert.ToUInt16(SITR.Client.Read(
                    "ETCS1_CurrentTrainData_EVC06CurrentTrainDataSub1" + k +
                    "_MmiNCaptionTrainset"));

                SITR.Client.Write("ETCS1_EchoedTrainData_EVC10EchoedTrainDataSub1" + k + "_MmiNCaptionTrainsetR",
                    Convert.ToUInt16(~EVC6_MmiNCaptionTrainset));

                numberOfCaptionTrainset +=
                    EVC6_MmiNCaptionTrainset; // Total number of CaptionTrainset for the whole telegram

                // Dynamic fields 2nd dimension
                for (int l = 0; l < EVC6_MmiNCaptionTrainset; l++)
                {
                    // Bit-inverted Trainset caption text
                    if (l < 10)
                    {
                        ushort EVC6_MmiXCaptionTrainset = Convert.ToUInt16(SITR.Client.Read(
                            "ETCS1_CurrentTrainData_EVC06CurrentTrainDataSub1" + k +
                            "_EVC06CurrentTrainDataSub110" + l + "_MmiXCaptionTrainset"));

                        SITR.Client.Write("ETCS1_EchoedTrainData_EVC10EchoedTrainDataSub1" + k +
                                          "_EVC10EchoedTrainDataSub110" + l +
                                          "_MmiXCaptionTrainsetR", Convert.ToChar(~EVC6_MmiXCaptionTrainset));
                    }

                    else
                    {
                        ushort EVC6_MmiXCaptionTrainset = Convert.ToUInt16(SITR.Client.Read(
                            "ETCS1_CurrentTrainData_EVC06CurrentTrainDataSub1" + k +
                            "_EVC06CurrentTrainDataSub11" + l + "_MmiXCaptionTrainset"));

                        SITR.Client.Write("ETCS1_EchoedTrainData_EVC10EchoedTrainDataSub1" + k +
                                          "_EVC10EchoedTrainDataSub11" + l +
                                          "_MmiXCaptionTrainsetR", Convert.ToChar(~EVC6_MmiXCaptionTrainset));
                    }
                }
            }

            // EVC10_alias_1
            ushort EVC6_alias_1 = Convert.ToUInt16(SITR.ETCS1.CurrentTrainData.EVC6alias1.Value);
            SITR.ETCS1.EchoedTrainData.EVC10alias1.Value = Convert.ToByte(~EVC6_alias_1);

            // Bit-inverted Loading gauge type of train 
            ushort EVC6_MMINidKeyLoadGauge = Convert.ToUInt16(SITR.ETCS1.CurrentTrainData.MmiNidKeyLoadGauge.Value);
            SITR.ETCS1.EchoedTrainData.MmiNidKeyLoadGaugeR.Value = Convert.ToByte(~EVC6_MMINidKeyLoadGauge);

            // Bit-inverted Train equipped with airtight system
            ushort EVC6_MAirtight = Convert.ToUInt16(SITR.ETCS1.CurrentTrainData.MmiMAirtight.Value);
            SITR.ETCS1.EchoedTrainData.MmiMAirtightR.Value = Convert.ToByte(~EVC6_MAirtight);

            // Bit-inverted Axle load category 
            ushort EVC6_MmiNidKeyAxleLoad = Convert.ToUInt16(SITR.ETCS1.CurrentTrainData.MmiNidKeyAxleLoad.Value);
            SITR.ETCS1.EchoedTrainData.MmiNidKeyAxleLoadR.Value = Convert.ToByte(~EVC6_MmiNidKeyAxleLoad);

            // Bit-inverted Max train speed
            ushort EVC6_VMaxTrain = Convert.ToUInt16(SITR.ETCS1.CurrentTrainData.MmiVMaxtrain.Value);
            SITR.ETCS1.EchoedTrainData.MmiVMaxtrainR.Value = Convert.ToUInt16(~EVC6_VMaxTrain);

            // Bit-inverted Max train length
            ushort EVC6_LTrain = Convert.ToUInt16(SITR.ETCS1.CurrentTrainData.MmiLTrain.Value);
            SITR.ETCS1.EchoedTrainData.MmiLTrainR.Value = Convert.ToUInt16(~EVC6_LTrain);

            // Bit-inverted Brake percentage
            ushort EVC6_MmiMBrakePerc = Convert.ToUInt16(SITR.ETCS1.CurrentTrainData.MmiMBrakePerc.Value);
            SITR.ETCS1.EchoedTrainData.MmiMBrakePercR.Value = Convert.ToByte(~EVC6_MmiMBrakePerc);

            // Bit-inverted Train category
            ushort EVC6_MmiNidKeyTrainCat = Convert.ToUInt16(SITR.ETCS1.CurrentTrainData.MmiNidKeyTrainCat.Value);
            SITR.ETCS1.EchoedTrainData.MmiNidKeyTrainCatR.Value = Convert.ToByte(EVC6_MmiNidKeyTrainCat);

            // Bit-inverted Train data enabled
            ushort EVC6_MmiMDataEnable = Convert.ToUInt16(SITR.ETCS1.CurrentTrainData.MmiMDataEnable.Value);
            SITR.ETCS1.EchoedTrainData.MmiMDataEnableR.Value = Convert.ToUInt16(EVC6_MmiMDataEnable);

            // Packet length
            SITR.ETCS1.EchoedTrainData.MmiLPacket.Value =
                Convert.ToUInt16(144 + EVC6_MmiNTrainset * 16 + numberOfCaptionTrainset * 8);

            SITR.SMDCtrl.ETCS1.EchoedTrainData.Value = 0x09;
        }

        /// <summary>
        /// Sends EVC-16 telegram with current Train Running Number.
        /// </summary>
        /// <param name="TrainNumber">Train Running Number (TRN)</param>
        public void SendEVC16_CurrentTrainNumber(uint TrainNumber)
        {
            SITR.ETCS1.CurrentTrainNumber.MmiMPacket.Value = 16;
            SITR.ETCS1.CurrentTrainNumber.MmiLPacket.Value = 64;
            SITR.ETCS1.CurrentTrainNumber.MmiNidOperation.Value = TrainNumber;
            SITR.SMDCtrl.ETCS1.CurrentTrainNumber.Value = 1;
        }

        /// <summary>
        ///     SendEVC20_MMI_Select_Level
        ///     Sends ETCS and NTC levels and related additional status information.
        /// <param name="MMI_N_Levels">Number of levels</param>
        /// <param name="MMI_Q_Level_Ntc_Id[k]">Qualifier for the variable MMI_M_LEVEL_NTC_ID for the specific level</param>
        /// <param name="MMI_M_Current_Level[k]">Last used level</param>
        /// <param name="MMI_M_Level_Flag[k]">Marker to indicate if a level button is enabled or disabled.</param>
        /// <param name="MMI_M_Inhibited_Level[k]">Inhibit status</param>
        /// <param name="MMI_M_Inhibit_Enable[k]">Inhibit enabled</param>
        /// <param name="MMI_M_Level_NTC_ID[k]">Identity of level or NTC</param>
        /// <param name="MMI_Q_Close_Enable">Close Button Enable</param>
        public void SendEVC20_MMISelectLevel(bool[] MMI_Q_Level_Ntc_ID, bool[] MMI_M_Current_Level,
            bool[] MMI_M_Level_Flag,
            bool[] MMI_M_Inhibited_Level, bool[] MMI_M_Inhibit_Enable, uint[] MMI_M_Level_NTC_ID,
            bool MMI_Q_Close_Enable)
        {
            SITR.ETCS1.SelectLevel.MmiMPacket.Value = 20; // Packet Id

            ushort numberOfLevels = (ushort) (MMI_Q_Level_Ntc_ID.Length);
            SITR.ETCS1.SelectLevel.MmiNLevels.Value = numberOfLevels; // Number of levels

            // Dynamic fields
            for (int k = 0; k < numberOfLevels; k++)
            {
                // Implementing EVC20_alias_1[k]
                uint uintMMI_Q_Level_Ntc_ID = Convert.ToUInt32(MMI_Q_Level_Ntc_ID[k]);
                uintMMI_Q_Level_Ntc_ID <<= 7;

                uint uintMMI_M_Current_Level = Convert.ToUInt32(MMI_M_Current_Level[k]);
                uintMMI_M_Current_Level <<= 6;

                uint uintMMI_M_Level_Flag = Convert.ToUInt32(MMI_M_Level_Flag[k]);
                uintMMI_M_Level_Flag <<= 5;

                uint uintMMI_M_Inhibited_Level = Convert.ToUInt32(MMI_M_Inhibited_Level[k]);
                uintMMI_M_Inhibited_Level <<= 4;

                uint uintMMI_M_Inhibit_Enable = Convert.ToUInt32(MMI_M_Inhibit_Enable[k]);
                uintMMI_M_Inhibit_Enable <<= 3;


                byte EVC20_alias_1 = Convert.ToByte(uintMMI_Q_Level_Ntc_ID | uintMMI_M_Current_Level |
                                                    uintMMI_M_Level_Flag | uintMMI_M_Inhibited_Level |
                                                    uintMMI_M_Inhibit_Enable);

                if (k < 10)
                {
                    SITR.Client.Write("ETCS1_SelectLevel_EVC20SelectLevelSub0" + k + "_EVC20alias1", EVC20_alias_1);
                    SITR.Client.Write("ETCS1_SelectLevel_EVC20SelectLevelSub0" + k + "_MmiMLevelNtcId",
                        Convert.ToByte(MMI_M_Level_NTC_ID[k]));
                }
                else
                {
                    SITR.Client.Write("ETCS1_SelectLevel_EVC20SelectLevelSub" + k + "_EVC20alias1", EVC20_alias_1);
                    SITR.Client.Write("ETCS1_SelectLevel_EVC20SelectLevelSub" + k + "_MmiMLevelNtcId",
                        Convert.ToByte(MMI_M_Level_NTC_ID[k]));
                }
            }

            uint uintMMI_Q_Close_Enable = Convert.ToUInt32(MMI_Q_Close_Enable);
            uintMMI_Q_Close_Enable <<= 7;

            SITR.ETCS1.SelectLevel.MmiQCloseEnable.Value =
                Convert.ToByte(uintMMI_Q_Close_Enable); // Close Button enable?
            SITR.ETCS1.SelectLevel.MmiLPacket.Value = Convert.ToUInt16(56 + numberOfLevels * 16); // Packet length

            SITR.SMDCtrl.ETCS1.SelectLevel.Value = 0x9;
        }

        /// <summary>
        /// Send standard EVC-20 telegram with Levels 0-3, CBTC, and AWS/TPWS selectable. Level 1 is preselected.
        /// </summary>
        public void SendEVC20_MMISelectLevel_AllLevels()
        {
            bool[] param_EVC20_MMI_Q_Level_Ntc_Id = {true, true, true, true, false, false};
            bool[] param_EVC20_MMI_M_Current_Level = {false, true, false, false, false, false};
            bool[] param_EVC20_MMI_M_Level_Flag = {true, true, true, true, true, true};
            bool[] param_EVC20_MMI_M_Inhibited_Level = {false, false, false, false, false, false};
            bool[] param_EVC20_MMI_M_Inhibit_Enable = {true, true, true, true, true, true};
            uint[] param_EVC20_MMI_M_Level_Ntc_Id = {0, 1, 2, 3, 50, 20}; // 50 = CBTC, 20 = AWS/TPWS

            SendEVC20_MMISelectLevel(param_EVC20_MMI_Q_Level_Ntc_Id, param_EVC20_MMI_M_Current_Level,
                param_EVC20_MMI_M_Level_Flag, param_EVC20_MMI_M_Inhibited_Level,
                param_EVC20_MMI_M_Inhibit_Enable, param_EVC20_MMI_M_Level_Ntc_Id,
                true);
        }

        /// <summary>
        /// Sends EVC-20 telegram to cancel previous MMI_Select_Level presentation
        /// </summary>
        public void SendEVC20_MMISelectLevel_Cancel()
        {
            SITR.ETCS1.SelectLevel.MmiMPacket.Value = 20; // Packet Id
            SITR.ETCS1.SelectLevel.MmiNLevels.Value = 0; // No levels - Cancel presentation of previous MMI_Select_Level
            SITR.ETCS1.SelectLevel.MmiQCloseEnable.Value = 0x08; // Close enabled
            SITR.ETCS1.SelectLevel.MmiLPacket.Value = 56; // Packet length
        }

        /// <summary>
        /// Send_EVC22_MMI_Current_Rbc_Data sends RBC Data to the DMI
        /// </summary>
        public void Send_EVC22_MMI_Current_Rbc(uint Mmi_Nid_Rbc, uint[] Mmi_Nid_Radio, byte Mmi_Nid_Window,
            byte Mmi_Q_Close_Enable,
            byte Mmi_M_Buttons, string[] Caption_Networks, byte[] Mmi_Nid_Data, byte[] Mmi_Q_Data_check,
            string[] Text_Data_Elements)
        {
            SITR.ETCS1.CurrentRbcData.MmiMPacket.Value = 22; // Packet Id
            SITR.ETCS1.CurrentRbcData.MmiNidRbc.Value = Mmi_Nid_Rbc; // RBC Id
            SITR.ETCS1.CurrentRbcData.MmiNidRadio.Value = Mmi_Nid_Radio; // RBC phone number
            SITR.ETCS1.CurrentRbcData.MmiNidWindow.Value = Mmi_Nid_Window; // ETCS Window Id
            SITR.ETCS1.CurrentRbcData.MmiQCloseEnable.Value = Mmi_Q_Close_Enable; // Close button enable?
            SITR.ETCS1.CurrentRbcData.MmiMButtons.Value = Mmi_M_Buttons; // Buttons available

            //Networks information
            ushort NumberOfNetworks = Convert.ToUInt16(Caption_Networks.Length);

            // Limit the number of networks to 10 (range : 0 - 9 according to VSIS 2.8)
            if (NumberOfNetworks <= 10)
            {
                SITR.ETCS1.CurrentRbcData.MmiNNetworks.Value = NumberOfNetworks; // Number of networks
            }
            else
            {
                TraceError(
                    "{0} networks were attempted to be displayed. Only 10 are allowed, the rest have been discarded!!");
                NumberOfNetworks = 10;
                SITR.ETCS1.CurrentRbcData.MmiNNetworks.Value = NumberOfNetworks; // Number of networks
            }

            ushort totalNumberofCaptionsNetwork = 0; // To be used for packet length

            //For all networks
            for (int k = 0; k < NumberOfNetworks; k++)
            {
                char[] NetworkCaptionChars = Caption_Networks[k].ToArray();
                ushort NumberNetworkCaptionChars = Convert.ToUInt16(NetworkCaptionChars.Length);
                totalNumberofCaptionsNetwork +=
                    NumberNetworkCaptionChars; // Total number of CaptionXNetworks chars for the whole telegram

                // Limit number of caption characters to 16
                if (NumberNetworkCaptionChars > 16)
                {
                    Array.Resize(ref NetworkCaptionChars, 16);
                }

                // Write individual network chars
                SITR.Client.Write("ETCS1_CurrentTrainData_EVC22CurrentRbcDataSub1" + k + "_MmiNCaptionNetwork",
                    NumberNetworkCaptionChars);

                // Dynamic fields 2nd dimension
                for (int l = 0; l < NumberNetworkCaptionChars; l++)
                {
                    // Network caption text character
                    if (l < 10)
                    {
                        SITR.Client.Write(
                            "ETCS1_CurrentTrainData_EVC22CurrentRbcDataSub1" + k + "_EVC22CurrentRbcDataSub110" + l +
                            "_MmiXCaptionNetwork",
                            NetworkCaptionChars[l]);
                    }
                    else
                    {
                        SITR.Client.Write(
                            "ETCS1_CurrentTrainData_EVC22CurrentRbcDataSub1" + k + "_EVC22CurrentRbcDataSub11" + l +
                            "_MmiXCaptionNetwork",
                            NetworkCaptionChars[l]);
                    }
                }
            }

            ushort numberOfDataElements = Convert.ToUInt16(Text_Data_Elements.Length);

            // Limit the number of data elements to 9 (range : 0 - 8 according to VSIS 2.8)
            if (numberOfDataElements <= 9)
            {
                SITR.ETCS1.CurrentRbcData.MmiNDataElements.Value =
                    numberOfDataElements; // Number of data elements to enter
            }
            else
            {
                TraceError(
                    "{0} networks were attempted to be displayed. Only 9 are allowed, the rest have been discarded!!");
                numberOfDataElements = 9;
                SITR.ETCS1.CurrentRbcData.MmiNDataElements.Value =
                    numberOfDataElements; // Number of data elements to enter
            }

            ushort totalNumberOfDataElementsText = 0; // To be used for packet length

            // For all data elements
            for (int k = 0; k < numberOfDataElements; k++)
            {
                SITR.Client.Write("ETCS1_CurrentRbcData_EVC22CurrentRbcDataSub2" + k + "_MmiNidData",
                    Mmi_Nid_Data[k]); // Data entry element Id
                SITR.Client.Write("ETCS1_CurrentRbcData_EVC22CurrentRbcDataSub2" + k + "_MmiQDataCheck",
                    Mmi_Q_Data_check[k]); // Data Check Result for data element  

                char[] dataElementsChars = Text_Data_Elements[k].ToArray();
                ushort numberDataElementsChars = Convert.ToUInt16(dataElementsChars.Length);
                totalNumberOfDataElementsText +=
                    numberDataElementsChars; // Total number of XTexts chars for the whole telegram

                // Limit number of caption characters to 16
                if (numberDataElementsChars > 16)
                {
                    Array.Resize(ref dataElementsChars, 16);
                }

                // Write individual data element chars
                SITR.Client.Write("ETCS1_CurrentRbcData_EVC22CurrentRbcDataSub2" + k + "_MmiNText",
                    numberDataElementsChars);

                // Dynamic fields 2nd dimension
                for (int l = 0; l < numberDataElementsChars; l++)
                {
                    // Data element text character
                    if (l < 10)
                    {
                        SITR.Client.Write(
                            "ETCS1_CurrentRbcData_EVC22CurrentRbcDataSub2" + k + "_EVC22CurrentRbcDataSub210" + l +
                            "_MmiXText",
                            dataElementsChars[l]);
                    }
                    else
                    {
                        SITR.Client.Write(
                            "ETCS1_CurrentRbcData_EVC22CurrentRbcDataSub2" + k + "_EVC22CurrentRbcDataSub21" + l +
                            "_MmiXText",
                            dataElementsChars[l]);
                    }
                }
            }

            // Packet length
            SITR.ETCS1.CurrentTrainData.MmiLPacket.Value = Convert.ToUInt16(
                192 + NumberOfNetworks * 16 + totalNumberofCaptionsNetwork * 8
                + numberOfDataElements * 32 + totalNumberOfDataElementsText * 8);
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
            SITR.ETCS1.EnableRequest.MmiQRequestEnable.Value = new uint[2] {Reversed_MMI_Q_Request_Enable, 0x80000000};
            SITR.ETCS1.EnableRequest.MmiLPacket.Value = 128;
            SITR.SMDCtrl.ETCS1.EnableRequest.Value = 1;
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
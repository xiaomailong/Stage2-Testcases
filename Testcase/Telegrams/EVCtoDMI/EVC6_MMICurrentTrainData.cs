﻿using System;
using System.Collections.Generic;
using CL345;

namespace Testcase.Telegrams.EVCtoDMI
{
    /// <summary>
    /// This packet is sent sporadically by ETC and is intended to support the following use cases:
    /// 1.) Display Train Data when entering Train Data Entry (TDE) window.
    /// 2.) Display/change echo text after data checks have been performed by ETC;
    ///     This also includes control over the allowed driver actions in case some data check has failed.
    /// It also gives the ETC the ability to control the status/type of the "Yes" button, if specified by functional requirements for ETC and DMI.
    /// Note: Parameter 'MMI_N_DATA_ELEMENTS' distinguishes between use case 1.) and 2.)
    /// </summary>
    public static class EVC6_MMICurrentTrainData
    {
        private static SignalPool _pool;
        private static int _trainsetid;
        private static int _maltdem;
        private const string BaseString = "ETCS1_CurrentTrainData_EVC06CurrentTrainDataSub";

        /// <summary>
        /// Initialise an instance of EVC-6 MMI Current Train Data telegram.
        /// </summary>
        /// <param name="pool">SignalPool</param>
        public static void Initialise(SignalPool pool)
        {
            _pool = pool;
            TrainSetCaptions = new List<string>();
            DataElements = new List<Variables.DataElement>();

            // Set as dynamic
            _pool.SITR.SMDCtrl.ETCS1.CurrentTrainData.Value = 0x0008;

            // Set default values
            _pool.SITR.ETCS1.CurrentTrainData.MmiMPacket.Value = 6; // Packet ID
            MMI_M_ALT_DEM = 0;
            MMI_M_TRAINSET_ID = 0;
        }

        // Change the EVC6_Alias_1 by bit-shifting the MMI_TRAINSET_ID and MMI_ALT_DEM values
        private static void SetAlias()
        {
            _pool.SITR.ETCS1.CurrentTrainData.EVC6alias1.Value = (byte) (_trainsetid << 4 | _maltdem << 2);
        }

        // Send instance of EVC-6 Current Train Data telegram
        public static void Send()
        {
            if (TrainSetCaptions.Count > 9)
                throw new ArgumentOutOfRangeException();
            if (DataElements.Count > 9)
                throw new ArgumentOutOfRangeException();

            // Set initial telegram size
            ushort totalSizeCounter = 176;

            // Set number of trainset captions
            _pool.SITR.ETCS1.CurrentTrainData.MmiNTrainset.Value = (ushort) TrainSetCaptions.Count;

            // Populate the array of trainset captions
            for (int trainsetIndex = 0; trainsetIndex < TrainSetCaptions.Count; trainsetIndex++)
            {
                var charArray = TrainSetCaptions[trainsetIndex].ToCharArray();

                if (charArray.Length > 12)
                    throw new ArgumentOutOfRangeException();

                // Set length of char array
                _pool.SITR.Client.Write(string.Format("{0}1{1}_MmiNCaptionTrainset", BaseString, trainsetIndex), charArray.Length);

                totalSizeCounter += 16;

                for (int charIndex = 0; charIndex < charArray.Length; charIndex++)
                {
                    char character = charArray[charIndex];

                    _pool.SITR.Client.Write(string.Format("{0}{1}",
                        string.Format("{0}1{1}_EVC06CurrentTrainDataSub11", BaseString, trainsetIndex),
                        string.Format("{0}_MmiXCaptionTrainset", charIndex.ToString("00"))), character);

                    totalSizeCounter += 8;
                }
            }

            // Set number of train data elements
            _pool.SITR.ETCS1.CurrentTrainData.MmiNDataElements.Value = (ushort) DataElements.Count;

            totalSizeCounter = Variables.PopulateDataElements(string.Format("{0}2", BaseString), totalSizeCounter, DataElements, _pool);

            // Set the total length of the packet
            _pool.SITR.ETCS1.CurrentTrainData.MmiLPacket.Value = totalSizeCounter;

            // Send the telegram
            _pool.SITR.SMDCtrl.ETCS1.CurrentTrainData.Value = 0x0009;
        }

        // Set the value for an instance of EVC-6 Current Train Data telegram WITHOUT SENDING IT!
        // This method should preceed EVC-10 packet sending
        public static void SetWithoutSending()
        {
            if (TrainSetCaptions.Count > 9)
                throw new ArgumentOutOfRangeException();
            if (DataElements.Count > 9)
                throw new ArgumentOutOfRangeException();

            // Set initial telegram size
            ushort totalSizeCounter = 176;

            // Set number of trainset captions
            _pool.SITR.ETCS1.CurrentTrainData.MmiNTrainset.Value = (ushort) TrainSetCaptions.Count;

            // Populate the array of trainset captions
            for (int trainsetIndex = 0; trainsetIndex < TrainSetCaptions.Count; trainsetIndex++)
            {
                var charArray = TrainSetCaptions[trainsetIndex].ToCharArray();

                if (charArray.Length > 12)
                    throw new ArgumentOutOfRangeException();

                // Set length of char array
                _pool.SITR.Client.Write(string.Format("{0}1{1}_MmiNCaptionTrainset", BaseString, trainsetIndex), charArray.Length);

                totalSizeCounter += 16;

                for (int charIndex = 0; charIndex < charArray.Length; charIndex++)
                {
                    char character = charArray[charIndex];

                    _pool.SITR.Client.Write(
                        string.Format("{0}1{1}_EVC06CurrentTrainDataSub11", BaseString, trainsetIndex) +
                        string.Format("{0}_MmiXCaptionTrainset", charIndex.ToString("00")), character);

                    totalSizeCounter += 8;
                }
            }

            // Set number of train data elements
            _pool.SITR.ETCS1.CurrentTrainData.MmiNDataElements.Value = (ushort) DataElements.Count;

            totalSizeCounter = Variables.PopulateDataElements(string.Format("{0}2", BaseString), totalSizeCounter, DataElements, _pool);

            // Set the total length of the packet
            _pool.SITR.ETCS1.CurrentTrainData.MmiLPacket.Value = totalSizeCounter;
        }

        /// <summary>
        /// A bit mask that, for each variable, tells if a data value is enabled (e.g. for 'edit' in EVC-6).
        /// 1 == 'enabled'.
        /// The variable supports the following use cases:
        /// 1.) Controls edit ability of related data object during TDE procedure (EVC-6, no data view).
        /// 2.) In case of a Train Data View procedure this variable controls visibility of data items (ERA_ERTMS_015560, v3.4.0, chapter 11.5.1.5).
        /// 3.) In packet EVC-10 this variable controls highlighting of changed data items (ERA_ERTMS_015560, v3.4.0, chapter 11.4.1.4, 10.3.3.5).
        /// </summary>
        public static Variables.MMI_M_DATA_ENABLE MMI_M_DATA_ENABLE
        {
            get { return (Variables.MMI_M_DATA_ENABLE) (_pool.SITR.ETCS1.CurrentTrainData.MmiMDataEnable.Value); }
            set { _pool.SITR.ETCS1.CurrentTrainData.MmiMDataEnable.Value = (ushort) value; }
        }

        /// <summary>
        /// Max train length
        /// 
        /// Values:
        /// 0 = "'No default value' =&gt; Data field shall remain empty"
        /// 1..4095 = "total train length"
        /// 4096..65535 = "Reserved"
        /// </summary>
        public static ushort MMI_L_TRAIN
        {
            get { return _pool.SITR.ETCS1.CurrentTrainData.MmiLTrain.Value; }
            set { _pool.SITR.ETCS1.CurrentTrainData.MmiLTrain.Value = value; }
        }

        /// <summary>
        /// Max train speed
        /// 
        /// Values:
        /// 0 = "'No default value' =&gt; TD entry field shall remain empty."
        /// 1..600 = "max train speed"
        /// 601..65535 = "Reserved"
        /// </summary>
        public static ushort MMI_V_MAXTRAIN
        {
            get { return _pool.SITR.ETCS1.CurrentTrainData.MmiVMaxtrain.Value; }
            set { _pool.SITR.ETCS1.CurrentTrainData.MmiVMaxtrain.Value = value; }
        }

        /// <summary>
        /// Identifies the train category related subset of MMI_NID_KEY.
        /// 
        /// Value range 3-20
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
        ///     20 = "FG 4"
        /// </summary>
        public static Variables.MMI_NID_KEY MMI_NID_KEY_TRAIN_CAT
        {
            get { return (Variables.MMI_NID_KEY) (_pool.SITR.ETCS1.CurrentTrainData.MmiNidKeyTrainCat.Value); }
            set { _pool.SITR.ETCS1.CurrentTrainData.MmiNidKeyTrainCat.Value = (byte) value; }
        }

        /// <summary>
        /// Brake percentage as input for calculation of braking characteristics.
        /// 
        /// Values:
        /// 0 = "No default value' =&gt; Data field shall remain empty"
        /// 1..9 = "Reserved"
        /// 10..250 = "Brake Percentage given in '%'"
        /// 251..255 = "Reserved"
        /// </summary>
        public static ushort MMI_M_BRAKE_PERC
        {
            get { return _pool.SITR.ETCS1.CurrentTrainData.MmiMBrakePerc.Value; }
            set { _pool.SITR.ETCS1.CurrentTrainData.MmiMBrakePerc.Value = (byte) value; }
        }

        /// <summary>
        /// Identifies the axle load category related subset of MMI_NID_KEY.
        /// 
        /// Value range 21-33
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
        /// </summary>
        public static Variables.MMI_NID_KEY MMI_NID_KEY_AXLE_LOAD
        {
            get { return (Variables.MMI_NID_KEY) (_pool.SITR.ETCS1.CurrentTrainData.MmiNidKeyAxleLoad.Value); }
            set { _pool.SITR.ETCS1.CurrentTrainData.MmiNidKeyAxleLoad.Value = (byte) value; }
        }

        /// <summary>
        /// Train equipped with airtight system.
        /// 
        /// Values:
        /// 0 = "Not equipped"
        /// 1 = "Equipped"
        /// 2 = "'No default value' =&gt; TD entry field shall remain empty"
        /// 3..255 = "Spare"
        /// </summary>
        public static ushort MMI_M_AIRTIGHT
        {
            get { return _pool.SITR.ETCS1.CurrentTrainData.MmiMAirtight.Value; }
            set { _pool.SITR.ETCS1.CurrentTrainData.MmiMAirtight.Value = (byte) value; }
        }

        /// <summary>
        /// Identifies the loading gauge category related subset of MMI_NID_KEY.
        /// 
        /// Value range 34-38
        ///     Values:
        ///     34 = "G1"
        ///     35 = "GA"
        ///     36 = "GB"
        ///     37 = "GC"
        ///     38 = "Out of GC"
        /// </summary>
        public static Variables.MMI_NID_KEY_Load_Gauge MMI_NID_KEY_LOAD_GAUGE
        {
            get { return (Variables.MMI_NID_KEY_Load_Gauge) (_pool.SITR.ETCS1.CurrentTrainData.MmiNidKeyLoadGauge.Value); }
            set { _pool.SITR.ETCS1.CurrentTrainData.MmiNidKeyLoadGauge.Value = (byte) value; }
        }

        /// <summary>
        /// Identifier of MMI Buttons.
        /// 
        /// Values:
        /// 36 = "BTN_YES_DATA_ENTRY_COMPLETE"
        /// 37 = "BTN_YES_DATA_ENTRY_COMPLETE_DELAY_TYPE"
        /// 255 = "no button"
        /// 
        /// Note: the definition is according to preliminary SubSet-121 'M_BUTTONS' definition.
        /// </summary>
        public static MMI_M_BUTTONS_CURRENT_TRAIN_DATA MMI_M_BUTTONS
        {
            get { return (MMI_M_BUTTONS_CURRENT_TRAIN_DATA) _pool.SITR.ETCS1.CurrentTrainData.MmiMButtons.Value; }
            set { _pool.SITR.ETCS1.CurrentTrainData.MmiMButtons.Value = (byte) value; }
        }

        /// <summary>
        /// ID of selected pre-configured train data set.
        /// 
        /// Values:
        /// 0 = "Train data entry method by train data set is not selected --&gt; use 'flexible TDE'"
        /// 1..9 = "Train data set ID 1..9"
        /// 10..14 = "Spare"
        /// 15 = "no Train data set specified"
        /// </summary>
        public static ushort MMI_M_TRAINSET_ID
        {
            get { return (ushort) ((_pool.SITR.ETCS1.CurrentTrainData.EVC6alias1.Value & 0xF0) >> 4); }

            set
            {
                _trainsetid = value;
                SetAlias();
            }
        }

        /// <summary>
        /// Control information for alternative train data entry method.
        /// 
        /// Values:
        /// 0 = "No alternative train data entry method enabled (covers 'fixed train data entry' and 'flexible
        /// train data entry' according to ERA_ERTMS_15560, v3.4.0, ch. 11.3.9.6.a+b)"
        /// 1 = "Flexible train data entry &lt;-&gt; train data entry for Train Sets (covers 'switchable train data
        /// entry' according to ERA_ERTMS_15560, v3.4.0, ch. 11.3.9.6.c)"
        /// 2 = "Reserved"
        /// 3 = "Reserved"
        /// 
        /// Note: In case no alternative TDE method is enabled, the variable "MMI_M_TRAINSET_ID" determines between
        /// "flexible TDE" (MMI_M_TRAINSET_ID = 0) or "train set TDE" (MMI_M_TRAINSET_ID != 0).
        /// This approach is chosen to deviate not too much between BL2 and BL3 interface.
        /// </summary>
        public static ushort MMI_M_ALT_DEM
        {
            get { return (ushort) ((_pool.SITR.ETCS1.CurrentTrainData.EVC6alias1.Value & 0x0C) >> 2); }

            set
            {
                _maltdem = value;
                SetAlias();
            }
        }

        public static List<string> TrainSetCaptions { get; set; }

        public static List<Variables.DataElement> DataElements { get; set; }

        /// <summary>
        /// MMI_M_Buttons for EVC-6 enum
        /// 
        /// Values:
        /// 36 = "BTN_YES_DATA_ENTRY_COMPLETE"
        /// 37 = "BTN_YES_DATA_ENTRY_COMPLETE_DELAY_TYPE"
        /// 255 = "no button"
        /// </summary>
        public enum MMI_M_BUTTONS_CURRENT_TRAIN_DATA : byte
        {
            BTN_YES_DATA_ENTRY_COMPLETE = 36,
            BTN_YES_DATA_ENTRY_COMPLETE_DELAY_TYPE = 37,
            NoButton = 255
        }
    }
}
﻿using System;
using System.Collections.Generic;
using CL345;
using static Testcase.Telegrams.EVCtoDMI.Variables;

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
    public static class EVC10_MMIEchoedTrainData
    {
        private static SignalPool _pool;
        const string BaseStringEVC06 = "ETCS1_CurrentTrainData_EVC06CurrentTrainDataSub";
        const string BaseStringEVC06_1 = "_EVC06CurrentTrainDataSub11";
        const string BaseStringEVC10 = "ETCS1_EchoedTrainData_EVC10EchoedTrainDataSub";
        const string BaseStringEVC10_1 = "_EVC10EchoedTrainDataSub11";


        /// <summary>
        /// Initialise an instance of EVC-10 MMI Echoed Train Data telegram.
        /// </summary>
        /// <param name="pool"></param>
        public static void Initialise(SignalPool pool)
        {
            _pool = pool;
            TrainSetCaptions = new List<string>();

            // Set as dynamic
            _pool.SITR.SMDCtrl.ETCS1.EchoedTrainData.Value = 0x0008;

            // Set default values
            _pool.SITR.ETCS1.EchoedTrainData.MmiMPacket.Value = 10; // Packet ID
        }

        /// <summary>
        /// Sends the EVC10 packet after having filled its dynamic fields 
        /// </summary>
        public static void Send()
        {
            // Number of traiset captions shall not be higher than 9
            if (TrainSetCaptions.Count > 9)
                throw new ArgumentOutOfRangeException();
            // Number of traiset captions set in parameter shall be the same than the one set in EVC-6
            if (TrainSetCaptions.Count != _pool.SITR.ETCS1.CurrentTrainData.MmiNTrainset.Value)
                throw new Exception("MmiNTrainset from EVC-6 and number of captions do not match!");

            // Set packet size
            ushort totalSizeCounter = 144;

            // Populate the array of trainset captions
            for (var trainsetIndex = 0; trainsetIndex < TrainSetCaptions.Count; trainsetIndex++)
            {
                // Get each trainset caption from function parameter
                var charArray = TrainSetCaptions[trainsetIndex].ToCharArray();
                // Get MmiNCaptionTrainset from EVC-6
                ushort evc6MmiNCaptionTrainset = (ushort)_pool.SITR.Client.Read($"{BaseStringEVC06}1{trainsetIndex}_MmiNCaptionTrainset");

                // trainset caption length from function parameter shall not be bigger than 12 ...
                if (charArray.Length > 12)
                    throw new ArgumentOutOfRangeException();
                // ... and shall be equal to the value extract from EVC-6 packet
                if (charArray.Length != evc6MmiNCaptionTrainset)
                    throw new Exception("MmiNCaptionTrainset from EVC-6 and length of caption do not match!");

                // Set length of char array, but bit-inverted
                _pool.SITR.Client.Write($"{BaseStringEVC10}1{trainsetIndex}_MmiNCaptionTrainsetR", (ushort)~evc6MmiNCaptionTrainset);

                // Set packet size
                totalSizeCounter += 16;

                // Populate the characters of trainset captions 
                for (var charIndex = 0; charIndex < charArray.Length; charIndex++)
                {
                    // Get each trainset caption character from function parameter
                    var character = charArray[charIndex];
                    // Declare value to get MmiXCaptionTrainset from EVC-6 packet
                    char evc6MmiXCaptionTrainset;

                    // Trainset caption text character
                    if (charIndex < 10)
                    {
                        // Get MmiXCaptionTrainset from EVC-6
                        evc6MmiXCaptionTrainset = (char)_pool.SITR.Client.Read($"{BaseStringEVC06}1{trainsetIndex}{BaseStringEVC06_1}0{charIndex}_MmiXCaptionTrainset");
                        // value from function paramater shall be equal to the value extract from EVC-6 packet
                        if (character != evc6MmiXCaptionTrainset)
                            throw new Exception("MmiNCaptionTrainset from EVC-6 and length of caption do not match!");
                        // Set char value but bit-inverted
                        _pool.SITR.Client.Write($"{BaseStringEVC10}1{trainsetIndex}{BaseStringEVC10_1}0{charIndex}_MmiXCaptionTrainsetR", (char)~evc6MmiXCaptionTrainset);
                    }
                    else
                    {
                        // Get MmiXCaptionTrainset from EVC-6 
                        evc6MmiXCaptionTrainset = (char)_pool.SITR.Client.Read($"{BaseStringEVC06}1{trainsetIndex}{BaseStringEVC06_1}{charIndex}_MmiXCaptionTrainset");
                        // value from function paramater shall be equal to the value extract from EVC-6 packet
                        if (character != evc6MmiXCaptionTrainset)
                            throw new Exception("MmiNCaptionTrainset from EVC-6 and length of caption do not match!");
                        // Set char value but bit-inverted
                        _pool.SITR.Client.Write($"{BaseStringEVC10}1{trainsetIndex}{BaseStringEVC10_1}{charIndex}_MmiXCaptionTrainsetR", (char)~evc6MmiXCaptionTrainset);
                    }

                    // Set packet size
                    totalSizeCounter += 8;
                }
            }

            // Set the total length of the packet
            _pool.SITR.ETCS1.EchoedTrainData.MmiLPacket.Value = totalSizeCounter;
            // Send telegram
            _pool.SITR.SMDCtrl.ETCS1.EchoedTrainData.Value = 0x0009;
        }

        /// <summary>
        /// Quantifier, number of trainsets to be shown for fixed TDE.
        /// 
        /// Values:
        /// 0 = "No trainset available, flexible TDE may apply"
        /// 1..9 = "applicable number of trainsets"
        /// </summary>
        public static ushort MMI_N_TRAINSETS_R
        {
            get => _pool.SITR.ETCS1.EchoedTrainData.MmiNTrainsetsR.Value;
            set => _pool.SITR.ETCS1.EchoedTrainData.MmiNTrainsetsR.Value = value;
        }
        
        /// <summary>
        /// A bit mask that, for each variable, tells if a data value is enabled (e.g. for 'edit' in EVC-6).
        /// 1 == 'enabled'.
        /// The variable supports the following use cases:
        /// 1.) Controls edit ability of related data object during TDE procedure (EVC-6, no data view).
        /// 2.) In case of a Train Data View procedure this variable controls visibility of data items (ERA_ERTMS_015560, v3.4.0, chapter 11.5.1.5).
        /// 3.) In packet EVC-10 this variable controls highlighting of changed data items (ERA_ERTMS_015560, v3.4.0, chapter 11.4.1.4, 10.3.3.5).
        /// </summary>
        public static ushort MMI_M_DATA_ENABLE_R
        {
            get => _pool.SITR.ETCS1.CurrentTrainData.MmiMDataEnable.Value;
            set => _pool.SITR.ETCS1.EchoedTrainData.MmiMDataEnableR.Value = value;
        }

        /// <summary>
        /// Max train length
        /// 
        /// Values:
        /// 0 = "'No default value' =&gt; Data field shall remain empty"
        /// 1..4095 = "total train length"
        /// 4096..65535 = "Reserved"
        /// </summary>
        public static ushort MMI_L_TRAIN_R
        {
            get => _pool.SITR.ETCS1.EchoedTrainData.MmiLTrainR.Value;
            set => _pool.SITR.ETCS1.EchoedTrainData.MmiLTrainR.Value = value;
        }

        /// <summary>
        /// Max train speed
        /// 
        /// Values:
        /// 0 = "'No default value' =&gt; TD entry field shall remain empty."
        /// 1..600 = "max train speed"
        /// 601..65535 = "Reserved"
        /// </summary>
        public static ushort MMI_V_MAXTRAIN_R
        {
            get => _pool.SITR.ETCS1.EchoedTrainData.MmiVMaxtrainR.Value;
            set => _pool.SITR.ETCS1.EchoedTrainData.MmiVMaxtrainR.Value = value;
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
        public static byte MMI_NID_KEY_TRAIN_CAT_R
        {
            get => _pool.SITR.ETCS1.EchoedTrainData.MmiNidKeyTrainCatR.Value;
            set => _pool.SITR.ETCS1.EchoedTrainData.MmiNidKeyTrainCatR.Value = value;
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
        public static byte MMI_M_BRAKE_PERC_R
        {
            get => _pool.SITR.ETCS1.EchoedTrainData.MmiMBrakePercR.Value;
            set => _pool.SITR.ETCS1.EchoedTrainData.MmiMBrakePercR.Value = value;
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
        public static byte MMI_NID_KEY_AXLE_LOAD_R
        {
            get => _pool.SITR.ETCS1.EchoedTrainData.MmiNidKeyAxleLoadR.Value;
            set => _pool.SITR.ETCS1.EchoedTrainData.MmiNidKeyAxleLoadR.Value = value;
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
        public static byte MMI_M_AIRTIGHT_R
        {
            get => _pool.SITR.ETCS1.EchoedTrainData.MmiMAirtightR.Value;
            set => _pool.SITR.ETCS1.EchoedTrainData.MmiMAirtightR.Value = value;
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
        public static byte MMI_NID_KEY_LOAD_GAUGE_R
        {
            get => _pool.SITR.ETCS1.EchoedTrainData.MmiNidKeyLoadGaugeR.Value;
            set => _pool.SITR.ETCS1.EchoedTrainData.MmiNidKeyLoadGaugeR.Value = value;
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
        public static byte EVC10_alias_1
        {
            get => _pool.SITR.ETCS1.EchoedTrainData.EVC10alias1.Value;
            set => _pool.SITR.ETCS1.EchoedTrainData.EVC10alias1.Value = value;
        }

        public static List<string> TrainSetCaptions { get; set; }

    }
}
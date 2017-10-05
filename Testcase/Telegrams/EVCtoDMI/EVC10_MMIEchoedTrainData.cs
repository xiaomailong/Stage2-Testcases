using System;
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
        private static int _trainsetid;
        private static int _maltdem;
        const string BaseString = "ETCS1_CurrentTrainData_EVC06CurrentTrainDataSub";

        /// <summary>
        /// Initialise an instance of EVC-6 MMI Current Train Data telegram.
        /// </summary>
        /// <param name="pool"></param>
        public static void Initialise(SignalPool pool)
        {
            _pool = pool;
            TrainSetCaptions = new List<string>();
            DataElements = new List<DataElement>();

            // Set as dynamic
            _pool.SITR.SMDCtrl.ETCS1.CurrentTrainData.Value = 0x8;

            // Set default values
            _pool.SITR.ETCS1.CurrentTrainData.MmiMPacket.Value = 6; // Packet ID
        }

        private static void SetAlias()
        {
            _pool.SITR.ETCS1.CurrentTrainData.EVC6alias1.Value = (byte) (_trainsetid << 4 | _maltdem << 2);
        }

        public static void Send()
        {
            if (TrainSetCaptions.Count > 9)
                throw new ArgumentOutOfRangeException();            

            ushort totalSizeCounter = 144;

            // Set number of trainset captions
            _pool.SITR.ETCS1.EchoedTrainData.MmiNTrainsetsR.Value = (ushort) TrainSetCaptions.Count;

            // Populate the array of trainset captions
            for (var trainsetIndex = 0; trainsetIndex < TrainSetCaptions.Count; trainsetIndex++)
            {
                var charArray = TrainSetCaptions[trainsetIndex].ToCharArray();
                if (charArray.Length > 12)
                    throw new ArgumentOutOfRangeException();

                // Set length of char array
                _pool.SITR.Client.Write($"{BaseString}1{trainsetIndex}_MmiNCaptionTrainset", charArray.Length);

                totalSizeCounter += 16;

                for (var charIndex = 0; charIndex < charArray.Length; charIndex++)
                {
                    var character = charArray[charIndex];

                    // Trainset caption text character
                    if (charIndex < 10)
                    {
                        _pool.SITR.Client.Write($"{BaseString}1{trainsetIndex}_EVC06CurrentTrainDataSub110{charIndex}_MmiXCaptionTrainset", character);
                    }
                    else
                    {
                        _pool.SITR.Client.Write($"{BaseString}1{trainsetIndex}_EVC06CurrentTrainDataSub11{charIndex}_MmiXCaptionTrainset", character);
                    }

                    totalSizeCounter += 8;
                }
            }

            // Set number of train data elements
            _pool.SITR.ETCS1.CurrentTrainData.MmiNDataElements.Value = (ushort) DataElements.Count;

            totalSizeCounter = PopulateDataElements($"{BaseString}2", totalSizeCounter, DataElements, _pool);

            // Set the total length of the packet
            _pool.SITR.ETCS1.CurrentTrainData.MmiLPacket.Value = totalSizeCounter;

            _pool.SITR.SMDCtrl.ETCS1.CurrentTrainData.Value = 0x09;
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
        public static byte MMI_NID_KEY_LOAD_GAUGE
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
        /// Note: In case no alternative TDE method is enabled, the variable "MMI_M_TRAINSET_ID"
        /// determines between "flexible TDE" (MMI_M_TRAINSET_ID = 0) or "train set TDE"
        /// (MMI_M_TRAINSET_ID != 0). This approach is chosen to deviate not too much between BL2 and BL3 interface.
        /// </summary>
        public static ushort MMI_M_ALT_DEM
        {
            set
            {
                _maltdem = value;
                SetAlias();
            }
        }

        public static List<string> TrainSetCaptions { get; set; }

        public static List<DataElement> DataElements { get; set; }
    }
}
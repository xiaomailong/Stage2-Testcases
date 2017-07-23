﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CL345;

namespace Testcase.Telegrams
{
    static class EVC6_MMICurrentTrainData
    {
        private static SignalPool _pool;
        private static int _trainsetid = 0;
        private static int _maltdem = 0;

        public static void Initialise(SignalPool pool)
        {
            _pool = pool;
            TrainSetCaptions = new List<string>();
            TrainDataElements = new List<TrainDataElement>();

            // set as dynamic
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
            if (TrainDataElements.Count > 9)
                throw new ArgumentOutOfRangeException();

            int totalsizecounter = 160;

            // set number of trainset captions
            _pool.SITR.ETCS1.CurrentTrainData.MmiNTrainset.Value = (ushort)TrainSetCaptions.Count;

            // populate the array of trainset captions
            for (var trainsetindex = 0; trainsetindex < TrainSetCaptions.Count; trainsetindex++)
            {
                var charArray = TrainSetCaptions[trainsetindex].ToCharArray();
                if (charArray.Length > 12)
                    throw new ArgumentOutOfRangeException();

                // set length of char array
                _pool.SITR.Client.Write(
                    $"ETCS1_CurrentTrainData_EVC06CurrentTrainDataSub1{trainsetindex}_MmiNCaptionTrainset",
                    charArray.Length);
                totalsizecounter += 16;

                for (var charindex = 0; charindex < charArray.Length; charindex++)
                {
                    var character = charArray[charindex];

                    // Trainset caption text character
                    if (charindex < 10)
                    {
                        _pool.SITR.Client.Write(
                            $"ETCS1_CurrentTrainData_EVC06CurrentTrainDataSub1{trainsetindex}_EVC06CurrentTrainDataSub110{charindex}_MmiXCaptionTrainset",
                            character);
                    }
                    else
                    {
                        _pool.SITR.Client.Write(
                            $"ETCS1_CurrentTrainData_EVC06CurrentTrainDataSub1{trainsetindex}_EVC06CurrentTrainDataSub11{charindex}_MmiXCaptionTrainset",
                            character);
                    }
                    totalsizecounter += 8;
                }
            }

            // set number of train data elements
            _pool.SITR.ETCS1.CurrentTrainData.MmiNDataElements.Value = (ushort) TrainDataElements.Count;

            // populate the train data elements array
            for (var tdeindex = 0; tdeindex < TrainDataElements.Count; tdeindex++)
            {
                var traindataelement = TrainDataElements[tdeindex];
                var varnamestring = $"ETCS1_CurrentTrainData_EVC06CurrentTrainDataSub2{tdeindex}_";
                var charArray = traindataelement.EchoText.ToCharArray();
                if(charArray.Length > 10)
                    throw new ArgumentOutOfRangeException();

                // set identifier
                _pool.SITR.Client.Write(varnamestring + "MmiNidData", traindataelement.Identifier);

                // set data check result
                _pool.SITR.Client.Write(varnamestring + "MmiQDataCheck", traindataelement.QDataCheck);
                
                // set number of chars
                _pool.SITR.Client.Write(varnamestring + "MmiNText", charArray.Length);
                

                totalsizecounter += 32;

                // populate the array
                
                for (var charindex = 0; charindex < charArray.Length; charindex++)
                {
                    var character = charArray[charindex];

                    if (charindex < 10)
                    {
                        _pool.SITR.Client.Write(
                            $"EVC06CurrentTrainDataSub210{charindex}_MmiXText",
                            character);
                    }
                    else
                    {
                        _pool.SITR.Client.Write(
                            $"EVC06CurrentTrainDataSub21{charindex}_MmiXText",
                            character);
                    }
                    totalsizecounter += 8;
                }
            }

            // set the total length of the packet
            _pool.SITR.ETCS1.CurrentTrainData.MmiLPacket.Value = (ushort) totalsizecounter;

            _pool.SITR.SMDCtrl.ETCS1.CurrentTrainData.Value = 0x09;
        }

        /// <summary>
        /// A bit mask that, for each variable, tells if a data value is enabled (e.g. for 'edit' in EVC-6). 1==
        /// 'enabled'.
        /// The variable supports the following use cases:
        /// 1.) Controls edit ability of related data object during TDE procedure (EVC-6, no data view).
        /// 2.) In case of a Train Data View procedure this variable controls visibility of data items
        /// (ERA_ERTMS_015560, v3.4.0, chapter 11.5.1.5).
        /// 3.) In packet EVC-10 this variable controls highlighting of changed data items
        /// (ERA_ERTMS_015560, v3.4.0, chapter 11.4.1.4, 10.3.3.5).
        /// </summary>
        public static MMI_M_DATA_ENABLE MMI_M_DATA_ENABLE
        {
            set => _pool.SITR.ETCS1.CurrentTrainData.MmiMDataEnable.Value = (ushort) value;
        }

        /// <summary>
        /// Max train length
        /// Values:
        /// 0 = "'No default value' =&gt; Data field shall remain empty"
        /// 1..4095 = "total train length"
        /// 4096..65535 = "Reserved"
        /// </summary>
        public static ushort MMI_L_TRAIN
        {
            set => _pool.SITR.ETCS1.CurrentTrainData.MmiLTrain.Value = value;
        }

        /// <summary>
        /// Max train speed
        /// Values:
        /// 0 = "'No default value' =&gt; TD entry field shall remain empty."
        /// 1..600 = "max train speed"
        /// 601..65535 = "Reserved"
        /// </summary>
        public static ushort MMI_V_MAXTRAIN
        {
            set => _pool.SITR.ETCS1.CurrentTrainData.MmiVMaxtrain.Value = value;
        }

        /// <summary>
        /// Identifies the train category related subset of MMI_NID_KEY.
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
        public static MMI_NID_KEY MMI_NID_KEY_TRAIN_CAT
        {
            set => _pool.SITR.ETCS1.CurrentTrainData.MmiNidKeyTrainCat.Value = (byte) value;
        }

        /// <summary>
        /// Brake percentage as input for calculation of braking characteristics
        /// Values:
        /// 0 = "No default value' =&gt; Data field shall remain empty"
        /// 1..9 = "Reserved"
        /// 10..250 = "Brake Percentage given in '%'"
        /// 251..255 = "Reserved"
        /// </summary>
        public static ushort MMI_M_BRAKE_PERC
        {
            set => _pool.SITR.ETCS1.CurrentTrainData.MmiMBrakePerc.Value = (byte) value;
        }

        /// <summary>
        /// Identifies the axle load category related subset of MMI_NID_KEY.
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
        public static MMI_NID_KEY MMI_NID_KEY_AXLE_LOAD
        {
            set => _pool.SITR.ETCS1.CurrentTrainData.MmiNidKeyAxleLoad.Value = (byte) value;
        }

        /// <summary>
        /// Train equipped with airtight system
        /// Values:
        /// 0 = "Not equipped"
        /// 1 = "Equipped"
        /// 2 = "'No default value' =&gt; TD entry field shall remain empty"
        /// 3..255 = "Spare"
        /// </summary>
        public static ushort MMI_M_AIRTIGHT
        {
            set => _pool.SITR.ETCS1.CurrentTrainData.MmiMAirtight.Value = (byte) value;
        }

        /// <summary>
        /// Identifies the loading gauge category related subset of MMI_NID_KEY.
        /// Value range 34-38
        ///     Values:
        ///     34 = "G1"
        ///     35 = "GA"
        ///     36 = "GB"
        ///     37 = "GC"
        ///     38 = "Out of GC"
        /// </summary>
        public static MMI_NID_KEY MMI_NID_KEY_LOAD_GAUGE
        {
            set { _pool.SITR.ETCS1.CurrentTrainData.MmiNidKeyLoadGauge.Value = (byte) value; }
        }

        /// <summary>
        /// Identifier of MMI Buttons
        /// Values:
        /// 0 = "BTN_MAIN"
        /// 1 = "BTN_OVERRIDE"
        /// 2 = "BTN_DATA_VIEW"
        /// 3 = "BTN_SPECIAL"
        /// 4 = "BTN_SETTINGS"
        /// 5 = "BTN_START"
        /// 6 = "BTN_DRIVER_ID"
        /// 7 = "BTN_TRAIN_DATA"
        /// 8 = "BTN_LEVEL"
        /// 9 = "BTN_TRAIN_RUNNING_NUMBER"
        /// 10 = "BTN_SHUNTING"
        /// 11 = "BTN_EXIT_SHUNTING"
        /// 12 = "BTN_NON_LEADING"
        /// 13 = "BTN_MAINTAIN_SHUNTING"
        /// 14 = "BTN_OVERRIDE_EOA"
        /// 15 = "BTN_ADHESION"
        /// 16 = "BTN_SR_SPEED_DISTANCE"
        /// 17 = "BTN_TRAIN_INTEGRITY"
        /// 18 = "BTN_SYSTEM_VERSION"
        /// 19 = "BTN_SET_VBC"
        /// 20 = "BTN_REMOVE_VBC"
        /// 21 = "BTN_CONTACT_LAST_RBC"
        /// 22 = "BTN_USE_SHORT_NUMBER"
        /// 23 = "BTN_ENTER_RBC_DATA"
        /// 24 = "BTN_RADIO_NETWORK_ID"
        /// 25 = "BTN_DRIVERID_TRAIN_RUNNING_NUMBER "
        /// 26 = "BTN_DRIVERID_SETTINGS"
        /// 27 = "BTN_SWITCH_FIXED_TRAIN_DATA_ENTRY"
        /// 28 = "BTN_SWITCH_FLEXIBLE_TRAIN_DATA_ENTRY"
        /// 29 = "BTN_TOGGLE_TUNNELSTOP_AREA"
        /// 30 = "BTN_TOGGLE_SPEED_DISTANCE_INFO"
        /// 31 = "BTN_YES_TRACK_AHEAD_FREE"
        /// 32 = "BTN_TOGGLE_GEOPOS"
        /// 33 = "BTN_CLOSE"
        /// 34 = "BTN_SCROLL_UP"
        /// 35 = "BTN_SCROLL_DOWN"
        /// 36 = "BTN_YES_DATA_ENTRY_COMPLETE"
        /// 37 = "BTN_YES_DATA_ENTRY_COMPLETE_DELAY_TYPE"
        /// 38 = "BTN_STM_DATA_ENTRY_SELECTION_POS1"
        /// 39 = "BTN_STM_DATA_ENTRY_SELECTION_POS2"
        /// 40 = "BTN_STM_DATA_ENTRY_SELECTION_POS3"
        /// 41 = "BTN_STM_DATA_ENTRY_SELECTION_POS4"
        /// 42 = "BTN_STM_DATA_ENTRY_SELECTION_POS5"
        /// 43 = "BTN_STM_DATA_ENTRY_SELECTION_POS6"
        /// 44 = "BTN_STM_DATA_ENTRY_SELECTION_POS7"
        /// 45 = "BTN_STM_DATA_ENTRY_SELECTION_POS8"
        /// 46 = "BTN_STM_END_OF_DATA_ENTRY"
        /// 47..252 = "Spare"
        /// 253 = "BTN_ENTER_DELAY_TYPE"
        /// 254 = "BTN_ENTER"
        /// 255 = "no button"
        /// Note: the definition is according to preliminary SubSet-121 'M_BUTTONS' definition.
        /// </summary>
        public static ushort MMI_M_BUTTONS
        {
            set => _pool.SITR.ETCS1.CurrentTrainData.MmiMButtons.Value = (byte) value;
        }

        /// <summary>
        /// Id of selected preconfigured train data set
        /// Values:
        /// 0 = "Train data entry method by train data set is not selected --&gt; use 'flexible TDE'"
        /// 1..9 = "Train data set ID 1..9"
        /// 10..14 = "Spare"
        /// 15 = "no Train data set specified"
        /// </summary>
        public static ushort MMI_M_TRAINSET_ID
        {
            set
            {
                _trainsetid = value;
                SetAlias();
            }
        }

        /// <summary>
        /// Control information for alternative train data entry method
        /// Values:
        /// 0 = "No alternative train data entry method enabled (covers 'fixed train data entry' and 'flexible
        /// train data entry' according to ERA_ERTMS_15560, v3.4.0, ch. 11.3.9.6.a+b)"
        /// 1 = "Flexible train data entry &lt;-&gt; train data entry for Train Sets (covers 'switchable train data
        /// entry' according to ERA_ERTMS_15560, v3.4.0, ch. 11.3.9.6.c)"
        /// 2 = "Reserved"
        /// 3 = "Reserved"
        /// Note: In case no alternative TDE method is enabled, the variable "MMI_M_TRAINSET_ID"
        /// determines between "flexible TDE" (MMI_M_TRAINSET_ID = 0) or "train set TDE"
        /// (MMI_M_TRAINSET_ID != 0). This approach is chosen to deviate not too much between BL2 and
        /// BL3 interface.
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

        public static List<TrainDataElement> TrainDataElements { get; set; }
    }

    [Flags]
    public enum MMI_M_DATA_ENABLE : ushort
    {
        NONE = 0,
        TrainSetID = 0x8000,
        TrainCategory = 0x4000,
        TrainLength = 0x2000,
        BrakePercentage = 0x1000,
        MaxTrainSpeed = 0x800,
        AxleLoadCategory = 0x400,
        Airtightness = 0x200,
        LoadingGauge = 0x100
    }

    public enum MMI_NID_KEY : byte
    {
        NoDedicatedKey = 0,
        No = 1,
        Yes = 2,
        PASS1 = 3,
        PASS2 = 4,
        PASS3 = 5,
        TILT1 = 6,
        TILT2 = 7,
        TILT3 = 8,
        TILT4 = 9,
        TILT5 = 10,
        TILT6 = 11,
        TILT7 = 12,

        FP1 = 13,
        FP2 = 14,
        FP3 = 15,
        FP4 = 16,
        FG1 = 17,
        FG2 = 18,
        FG3 = 19,
        FG4 = 20,
        CATA = 21,
        CATHS17 = 22,
        CATB1 = 23,
        CATB2 = 24,
        CATC2 = 25,
        CATC3 = 26,
        CATC4 = 27,
        CATD2 = 28,
        CATD3 = 29,
        CATD4 = 30,
        CATD4XL = 31,
        CATE4 = 32,
        CATE5 = 33,
        G1 = 34,
        GA = 35,
        GB = 36,
        GC = 37,
        OutofGC = 38,
        Nonslipperyrail = 39,
        Slipperyrail = 40,
        Level1 = 41,
        Level2 = 42,
        Level3 = 43,
        Level0 = 44
    }

    public class TrainDataElement
    {
        public ushort Identifier { get; set; }
        public ushort QDataCheck { get; set; }
        public string EchoText { get; set; }
    }
}
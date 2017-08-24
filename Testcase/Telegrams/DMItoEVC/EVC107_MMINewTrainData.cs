using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using CL345;
using static Testcase.Telegrams.EVCtoDMI.Variables;

namespace Testcase.Telegrams.DMItoEVC
{
    /// <summary>
    /// This packet shall be sent when the driver acts during the Train Data Entry procedure. It covers the following use cases:
    /// 1. Driver accepts a data element by pressing 'Enter'.
    /// 2. Driver accepts a data element by pressing 'Enter_Delay_Type'.
    /// 3. Driver completes entering a data block by pressing 'Yes'.
    /// 4. Driver overrules an operational check rule by pressing 'Delay Type Yes'.
    /// </summary>
    public static class EVC107_MMINewTrainData
    {
        private static SignalPool _pool;
        private static bool _bResult;
        private static string _varPath;
        private static ushort _lTrain;
        private static ushort _vMaxtrain;
        private static byte _nidKeyTrainCat;
        private static byte _mBrakePerc;
        private static byte _nidKeyAxleLoad;
        private static byte _mAirtight;
        private static byte _nidKeyLoadGauge;
        private static byte _mButtons;
        private static ushort _mTrainsetId;
        private static ushort _mAltDem;
        private static byte _evc107alias1;
        private static List<byte> _trainData;


        /// <summary>
        /// Initialise EVC107 MMI_New_Train_Data telegram.
        /// </summary>
        /// <param name="pool"></param>
        public static void Initialise(SignalPool pool)
        {
            _pool = pool;
            _trainData = new List<byte>();
             
        }

        private static void CheckSimpleField(ushort varToCheck, bool bResult, string varPath)
        {
            // get field name
            string _varName = (varPath.Split('_'))[2];

            if (bResult) // if check passes
            {
                _pool.TraceReport("DMI->ETCS: EVC-107 [MMI_NEW_TRAIN_DATA." + _varName + "] => " +
                    varToCheck + " PASSED.");
            }
            else // else display the real value extracted from EVC-107 [MMI_NEW_TRAIN_DATA]
            {
                _pool.TraceError("DMI->ETCS: Check EVC-107 [MMI_NEW_TRAIN_DATA." + _varName + "] => " +
                    _pool.SITR.Client.Read(varPath).ToString() + " FAILED.");
            }
        }

        private static void CheckEVC107_alias_1(ushort varToCheck, ushort varExtracted, string varName)
        {
            // compare the value measured with the expected one
            _bResult = varExtracted.Equals(varToCheck);

            if (_bResult) // if check passes
            {
                _pool.TraceReport("DMI->ETCS: EVC-107 [MMI_NEW_TRAIN_DATA." + varName + "] => " +
                    varToCheck + " PASSED.");
            }
            else // else display the real value extracted from EVC-107 [MMI_NEW_TRAIN_DATA]
            {
                _pool.TraceError("DMI->ETCS: Check EVC-107 [MMI_NEW_TRAIN_DATA." + varName + "] => " +
                    varExtracted + " FAILED.");
            }
        }

        /// <summary>
        /// Total length of the current train.
        /// Values:
        /// 0 = "'No default value' => Data field shall remain empty"
        /// 1..4095 = "total train length"
        /// 4096..65535 = "Reserved"
        /// </summary>
        public static ushort Check_MMI_L_TRAIN
        {
            set
            {
                _lTrain = value; 
                _varPath = "CCUO_ETCS1NewTrainData_MmiLPacket";
                _bResult = _pool.SITR.Client.Read(_varPath).Equals(_lTrain);
                CheckSimpleField(_lTrain, _bResult, _varPath);
            }
        }

        /// <summary>
        /// Estimated train speed at the actual time, without tolerance added
        ///     Values:
        ///     1 = "Speed unknown"
        /// </summary>
        public static ushort Check_MMI_V_MAXTRAIN
        {
            set
            {
                _vMaxtrain = value;
                _varPath = "CCUO_ETCS1NewTrainData_MmiVMaxtrain";
                _bResult = _pool.SITR.Client.Read(_varPath).Equals(_vMaxtrain);
                CheckSimpleField(_vMaxtrain, _bResult, _varPath);
            }
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
        public static MMI_NID_KEY Check_MMI_NID_KEY_TRAIN_CAT
        {
            set
            {
                _nidKeyTrainCat = (byte) value;
                _varPath = "CCUO_ETCS1NewTrainData_MmiNidKeyTrainCat";
                _bResult = _pool.SITR.Client.Read(_varPath).Equals(_nidKeyTrainCat);
                CheckSimpleField(_nidKeyTrainCat, _bResult, _varPath);
            }
        }

        /// <summary>
        /// Brake percentage as input for calculation of braking characteristics.
        /// Values:
        /// 0 = "No default value' =&gt; Data field shall remain empty"
        /// 1..9 = "Reserved"
        /// 10..250 = "Brake Percentage given in '%'"
        /// 251..255 = "Reserved"
        /// </summary>
        public static byte Check_MMI_M_BRAKE_PERC
        {
            set
            {
                _mBrakePerc = (byte)value;
                _varPath = "CCUO_ETCS1NewTrainData_MmiMBrakePerc";
                _bResult = _pool.SITR.Client.Read(_varPath).Equals(_mBrakePerc);
                CheckSimpleField(_mBrakePerc, _bResult, _varPath);
            }
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
        public static MMI_NID_KEY Check_MMI_NID_KEY_AXLE_LOAD
        {
            set
            {
                _nidKeyAxleLoad = (byte)value;
                _varPath = "CCUO_ETCS1NewTrainData_MmiNidKeyAxleLoad";
                _bResult = _pool.SITR.Client.Read(_varPath).Equals(_nidKeyAxleLoad);
                CheckSimpleField(_nidKeyAxleLoad, _bResult, _varPath);
            }
        }

        /// <summary>
        /// Train equipped with airtight system.
        ///     Values:
        ///     0 = "Not equipped"
        ///     1 = "Equipped"
        ///     2 = "'No default value' =&gt; TD entry field shall remain empty"
        ///     3..255 = "Spare"
        /// </summary>
        public static byte Check_MMI_M_AIRTIGHT
        {
            set
            {
                _mAirtight = (byte)value;
                _varPath = "CCUO_ETCS1NewTrainData_MmiMAirtight";
                _bResult = _pool.SITR.Client.Read(_varPath).Equals(_mAirtight);
                CheckSimpleField(_mAirtight, _bResult, _varPath);
            }
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
        public static MMI_NID_KEY Check_MMI_NID_KEY_LOAD_GAUGE
        {
            set
            {
                _nidKeyLoadGauge = (byte)value;
                _varPath = "CCUO_ETCS1NewTrainData_MmiNidKeyLoadGauge";
                _bResult = _pool.SITR.Client.Read(_varPath).Equals(_nidKeyLoadGauge);
                CheckSimpleField(_nidKeyLoadGauge, _bResult, _varPath);
            }
        }

        /// <summary>
        /// ID of selected pre-configured train data set. 
        /// Values:
        /// 0 = "Train data entry method by train data set is not selected --&gt; use 'flexible TDE'"
        /// 1..9 = "Train data set ID 1..9"
        /// 10..14 = "Spare"
        /// 15 = "no Train data set specified"
        /// </summary>
        public static ushort Check_MMI_M_TRAINSET_ID
        {
            set
            {
                _mTrainsetId = value;
                _evc107alias1 = _pool.SITR.CCUO.ETCS1NewTrainData.EVC107alias1.Value;
                // Extract MMI_M_TRAINSET_ID (Bits 8 -> 5 according to VSIS 2.9)
                byte _mmiMTransetId = (byte)((_evc107alias1 & 0xF0) >> 4); //xxxx xxxx -> xxxx 0000-> 0000 xxxx
                CheckEVC107_alias_1(_mTrainsetId, _mmiMTransetId, "MMI_M_TRAINSET_ID");
            }
        }

        /// <summary>
        /// Control information for alternative train data entry method. 
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
        public static ushort Check_MMI_M_ALT_DEM
        {
            set
            {
                _mAltDem = value;
                _evc107alias1 = _pool.SITR.CCUO.ETCS1NewTrainData.EVC107alias1.Value;
                // Extract MMI_M_TRAINSET_ID (Bits 4 & 3 according to VSIS 2.9)
                byte _mmiMAltDem = (byte)((_evc107alias1 & 0x0C) >> 2); //xxxx xxxx -> 0000 xx00-> 0000 00xx
                CheckEVC107_alias_1(_mAltDem, _mmiMAltDem, "MMI_M_ALT_DEM");
            }
        }

        /// <summary>
        /// The identity of an data entry element
        /// Values:
        /// 0 = "Train running number"
        /// 1 = "ERTMS/ETCS Level"
        /// 2 = "Driver ID"
        /// 3 = "Radio network ID"
        /// 4 = "RBC ID"
        /// 5 = "RBC phone number"
        /// 6 = "Train Type (Train Data Set Identifier)"
        /// 7 = "Train category"
        /// 8 = "Length"
        /// 9 = "Brake percentage"
        /// 10 = "Maximun speed"
        /// 11 = "Axle load category"
        /// 12 = "Airtight"
        /// 13 = "Loading gauge"
        /// 14 = "Operated system version"
        /// 15 = "SR Speed"
        /// 16 = "SR Distance"
        /// 17 = "Adhesion"
        /// 18 = "Set VBC code"
        /// 19 = "Remove VBC code"
        /// 20..254 = "spare"
        /// 255 = "no specific data element defined" 
        /// Note: the definition is according to preliminary SubSet-121 'NID_DATA' definition.
        /// </summary>
        private static List<byte> Check_MMI_NID_DATA
        {
            set
            {
                _trainData = value;

                // get the "static part" of the variable name
                string varPath = "CCUO_ETCS1NewTrainData_EVC107NewTrainDataSub";
                byte _dataElement;

                if (_trainData.Count > 19) //if too many data elements were entered 
                    throw new ArgumentOutOfRangeException();
                // if the number of data element entred does not match with what is received from the DMI
                if (_trainData.Count != _pool.SITR.CCUO.ETCS1NewTrainData.MmiNDataElements.Value)
                    throw new Exception("Number of Train data elements and number of captions do not match!");

                // for every data element entered for verification
                for (int trainDataIndex = 0; trainDataIndex < _trainData.Count; trainDataIndex++)
                {
                    // get the matching element from EVC-107 telegran
                    if (trainDataIndex < 10)
                        _dataElement = (byte)_pool.SITR.Client.Read(varPath + "0" + trainDataIndex + "_MmiNidData");
                    else
                        _dataElement = (byte)_pool.SITR.Client.Read(varPath + trainDataIndex + "_MmiNidData");

                    // compare the value measured with the expected one
                    _bResult = _dataElement.Equals(_trainData[trainDataIndex]);

                    if (_bResult) // if check passes
                    {
                        _pool.TraceReport("DMI->ETCS: EVC-107 [MMI_NEW_TRAIN_DATA.MMI_NID_DATA[" + trainDataIndex + "]] => " +
                            _trainData[trainDataIndex] + " PASSED.");
                    }
                    else // else display the real value extracted from EVC-107 [MMI_NEW_TRAIN_DATA]
                    {
                        _pool.TraceError("DMI->ETCS: Check EVC-107 [MMI_NEW_TRAIN_DATA.MMI_NID_DATA[" + trainDataIndex + "]] => " +
                            _dataElement + " FAILED.");
                    }
                }
            }
        }

        /// <summary>
        /// Identifier of MMI Buttons. 
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
        public static MMI_M_BUTTONS Check_MMI_M_BUTTONS
        {
            set
            {
                _mButtons = (byte)value;
                _varPath = "CCUO_ETCS1NewTrainData_MmiMButtons";
                _bResult = _pool.SITR.Client.Read(_varPath).Equals(_mButtons);
                CheckSimpleField(_mButtons, _bResult, _varPath);
            }
        }

    }
}
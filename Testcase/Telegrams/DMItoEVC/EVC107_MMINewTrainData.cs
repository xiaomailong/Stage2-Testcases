using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using CL345;
using Testcase.Telegrams.EVCtoDMI;
using Testcase.DMITestCases;
using BT_CSB_Tools.SignalPoolGenerator.Signals.PdSignal.Misc;
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
        private static bool _checkResult;
        private static Fixed_Trainset_Captions _trainsetSelected;
        private static ushort _nDataElements;
        private static MMI_M_BUTTONS_TRAIN_DATA _mButtons;
        private static byte _nidData;

        /*
        private static string _varPath;
        private static ushort _lTrain;
        private static ushort _vMaxtrain;
        private static byte _nidKeyTrainCat;
        private static byte _mBrakePerc;
        private static byte _nidKeyAxleLoad;
        private static byte _mAirtight;
        private static byte _nidKeyLoadGauge;
        
        private static ushort _mTrainsetId;
        private static ushort _mAltDem;
        private static byte _evc107alias1;
        */

        private const string BaseString0 = "DMI->ETCS: EVC-107 [MMI_NEW_TRAIN_DATA]";
        private const string BaseString1 = "CCUO_ETCS1NewTrainData_EVC107NewTrainDataSub";

        /// <summary>
        /// Initialise EVC107 MMI_New_Train_Data telegram.
        /// </summary>
        /// <param name="pool"></param>
        public static void Initialise(SignalPool pool)
        {
            _pool = pool;
            _pool.SITR.SMDCtrl.CCUO.ETCS1NewTrainData.Value = 0x0009;
            _pool.SITR.SMDStat.CCUO.ETCS1NewTrainData.Value = 0x00;            
        }

        private static void CheckFixedTrainDataEntered(Fixed_Trainset_Captions mmiMTrainsetId)
        {
            // Reset telegram received flag in RTSim
            _pool.SITR.SMDStat.CCUO.ETCS1NewTrainData.Value = 0x00;

            // Check if telegram received flag has been set. Allows 20 seconds to enter train data.
            if (_pool.SITR.SMDStat.CCUO.ETCS1NewTrainData.WaitForCondition(Is.Equal, 1, 20000, 100))
            {
                // Check all static fields
                _checkResult = _pool.SITR.CCUO.ETCS1NewTrainData.MmiLTrain.Value.Equals(0) &
                               _pool.SITR.CCUO.ETCS1NewTrainData.MmiVMaxtrain.Value.Equals(0) &
                               _pool.SITR.CCUO.ETCS1NewTrainData.MmiNidKeyTrainCat.Value.Equals((byte)MMI_NID_KEY.NoDedicatedKey) &
                               _pool.SITR.CCUO.ETCS1NewTrainData.MmiMBrakePerc.Value.Equals(0) &
                               _pool.SITR.CCUO.ETCS1NewTrainData.MmiNidKeyAxleLoad.Value.Equals((byte)MMI_NID_KEY.NoDedicatedKey) &
                               _pool.SITR.CCUO.ETCS1NewTrainData.MmiMAirtight.Value.Equals(0) &
                               _pool.SITR.CCUO.ETCS1NewTrainData.MmiNidKeyLoadGauge.Value.Equals((byte)MMI_NID_KEY.NoDedicatedKey) &
                               _pool.SITR.CCUO.ETCS1NewTrainData.EVC107alias1.Value.Equals((byte)mmiMTrainsetId << 4) &
                               _pool.SITR.CCUO.ETCS1NewTrainData.MmiMButtons.Value.Equals((byte)_mButtons);
                            
                // If check passes
                if (_checkResult)
                {
                    _pool.TraceReport(BaseString0 + Environment.NewLine +
                        "MMI_L_TRAIN = " + 0 + Environment.NewLine +
                        "MMI_V_MAXTRAIN = " + 0 + Environment.NewLine +
                        "MMI_NID_KEY_TRAIN_CAT = \"" + MMI_NID_KEY.NoDedicatedKey + "\"" + Environment.NewLine +
                        "MMI_M_BRAKE_PERC = " + 0 + Environment.NewLine +
                        "MMI_NID_KEY_AXLE_LOAD = \"" + MMI_NID_KEY.NoDedicatedKey + "\"" + Environment.NewLine +
                        "MMI_M_AIRTIGHT = " + 0 + Environment.NewLine +
                        "MMI_NID_KEY_LOAD_GAUGE = \"" + MMI_NID_KEY.NoDedicatedKey + "\"" + Environment.NewLine +
                        "MMI_M_TRAINSET_ID = \"" + mmiMTrainsetId + "\"" + Environment.NewLine +
                        "MMI_M_ALT_DEM = " + 0 + Environment.NewLine +
                        "MMI_M_BUTTONS = \"" + _mButtons + Environment.NewLine +
                        "Result = PASSED.");
                }
                // Else display the real value extracted from EVC-107
                else
                {
                    _pool.TraceError(BaseString0 + Environment.NewLine +
                        "MMI_L_TRAIN = \"" + _pool.SITR.CCUO.ETCS1NewTrainData.MmiLTrain.Value + "\"" + Environment.NewLine +
                        "MMI_V_MAXTRAIN = \"" + _pool.SITR.CCUO.ETCS1NewTrainData.MmiLTrain.Value + "\"" + Environment.NewLine +
                        "MMI_NID_KEY_TRAIN_CAT = \"" + Enum.GetName(typeof(MMI_NID_KEY), _pool.SITR.CCUO.ETCS1NewTrainData.MmiNidKeyTrainCat.Value) + "\"" + Environment.NewLine +
                        "MMI_M_BRAKE_PERC = \"" + _pool.SITR.CCUO.ETCS1NewTrainData.MmiLTrain.Value + "\"" + Environment.NewLine +
                        "MMI_NID_KEY_AXLE_LOAD = \"" + Enum.GetName(typeof(MMI_NID_KEY), _pool.SITR.CCUO.ETCS1NewTrainData.MmiNidKeyAxleLoad.Value) + "\"" + Environment.NewLine +
                        "MMI_M_AIRTIGHT = \"" + _pool.SITR.CCUO.ETCS1NewTrainData.MmiLTrain.Value + "\"" + Environment.NewLine +
                        "MMI_NID_KEY_LOAD_GAUGE = \"" + Enum.GetName(typeof(MMI_NID_KEY), _pool.SITR.CCUO.ETCS1NewTrainData.MmiNidKeyLoadGauge.Value) + "\"" + Environment.NewLine +
                        "MMI_M_TRAINSET_ID = \"" + Enum.GetName(typeof(Fixed_Trainset_Captions), ((_pool.SITR.CCUO.ETCS1NewTrainData.EVC107alias1.Value & 0xF0) >> 4)) + "\"" + Environment.NewLine +
                        "MMI_M_ALT_DEM = \"" + Enum.GetName(typeof(Fixed_Trainset_Captions), ((_pool.SITR.CCUO.ETCS1NewTrainData.EVC107alias1.Value & 0x0C) >> 2)) + "\"" + Environment.NewLine +
                        "MMI_M_BUTTONS = \"" + Enum.GetName(typeof(MMI_M_BUTTONS), _pool.SITR.CCUO.ETCS1NewTrainData.MmiMButtons.Value) + Environment.NewLine +
                        "Result: FAILED!");
                }

                // Get number of data element.                
                _nDataElements = _pool.SITR.CCUO.ETCS1NewTrainData.MmiNDataElements.Value;

                // MMI_gen 9460: "[..] In case of [Enter] | [Enter_Delay_Type] the [MMI_NEW_TRAIN_DATA (EVC-107)].MMI_N_DATA_ELEMENTS shall be set to '1', 
                // as the driver is only allowed to accept one data at a time. "
                if (_pool.SITR.CCUO.ETCS1NewTrainData.MmiMButtons.Value.Equals((byte)MMI_M_BUTTONS_TRAIN_DATA.BTN_ENTER))
                {                
                    if (_nDataElements.Equals(1))
                    {
                        // Get MMI_NID_DATA
                        _nidData = (byte)_pool.SITR.Client.Read($"{BaseString1}0_MmiNidData"); //to be changed once the EVC107 rtsim configuration is updated

                        // For Trainset selection, MMI_NID_DATA should be equal to 6 (= Train Type)
                        _checkResult = _nidData.Equals(6); 
                        
                        // If check passes
                        if (_checkResult)
                        {
                            _pool.TraceReport("MMI_N_DATA_ELEMENTS = " + 1 + Environment.NewLine +
                            "MMI_NID_DATA[0] = " + 6  + Environment.NewLine +
                            "Result = PASSED.");
                        }
                        // Else display the real value extracted
                        else

                        {
                            _pool.TraceError("MMI_N_DATA_ELEMENTS = " + 1 + Environment.NewLine +
                            "MMI_NID_DATA[0] = " + _nidData + Environment.NewLine +
                            "Result = FAILED!");
                        }
                    }
                    else
                    {
                        // Display the real value extracted
                        _pool.TraceError("MMI_N_DATA_ELEMENTS = " + _nDataElements);
                        for (int k = 0; k < _nDataElements; k++)
                        {
                            _nidData = (byte)_pool.SITR.Client.Read($"{BaseString1}0{k}_MmiNidData");
                            _pool.TraceError($"MMI_NID_DATA[{k}] = " + _nidData + ". Result = FAILED!");
                        }
                    }
                }

                // MMI_gen 9460 "[...] In case of [Yes] | [Yes_Delay_Type] the [MMI_NEW_TRAIN_DATA (EVC-107)].MMI_N_DATA_ELEMENTS shall be set to '0', 
                // as the [MMI_NEW_TRAIN_DATA (EVC-107)].MMI_M_BUTTONS clearly indicates that all data are affected. 
                else if (_pool.SITR.CCUO.ETCS1NewTrainData.MmiMButtons.Value.Equals((byte)MMI_M_BUTTONS_TRAIN_DATA.BTN_YES_DATA_ENTRY_COMPLETE))
                {
                    _checkResult = _nDataElements.Equals(0);

                    // If check passes
                    if (_checkResult)
                    {
                        _pool.TraceReport("MMI_N_DATA_ELEMENTS = " + 0 + Environment.NewLine +
                        "Result = PASSED.");
                    }
                    // Else display the real value extracted
                    else
                    {
                        // Display the real value extracted
                        _pool.TraceError("MMI_N_DATA_ELEMENTS = " + _nDataElements);
                        for (int k = 0; k < _nDataElements; k++)
                        {
                            _nidData = (byte)_pool.SITR.Client.Read($"{BaseString1}0{k}_MmiNidData");
                            _pool.TraceError($"MMI_NID_DATA[{k}] = " + _nidData + ". Result = FAILED!");
                        }
                    }
                }

                else
                {
                    _pool.TraceReport("MMI_N_DATA_ELEMENTS = " + _nDataElements);
                    for (int k = 0; k < _nDataElements; k++)
                    {
                        _nidData = (byte)_pool.SITR.Client.Read($"{BaseString1}0{k}_MmiNidData");
                        _pool.TraceError($"MMI_NID_DATA[{k}] = " + _nidData + ".");
                    }
                }
            }
            // Show generic DMI -> EVC telegram failure
            else
            {
                DmiExpectedResults.DMItoEVC_Telegram_Not_Received(_pool, BaseString0);
            }
        }

        /// <summary>
        /// Identity of the fixed trainset selected.
        /// 
        /// Values:
        /// FLU = 1
        /// RLU = 2
        /// Rescue = 3
        /// </summary>
        public static Fixed_Trainset_Captions TrainsetSelected
        {
            set
            {
                _trainsetSelected = value;
                CheckFixedTrainDataEntered(_trainsetSelected);
            }
        }

        /// <summary>
        /// Only MMI Buttons used in EVC-6 and EVC-107. 
        /// 
        /// Values:        
        /// 36 = "BTN_YES_DATA_ENTRY_COMPLETE"
        /// 37 = "BTN_YES_DATA_ENTRY_COMPLETE_DELAY_TYPE"       
        /// 253 = "BTN_ENTER_DELAY_TYPE"
        /// 254 = "BTN_ENTER"
        /// 255 = "no button"
        /// Note: the definition is according to preliminary SubSet-121 'M_BUTTONS' definition.
        /// </summary>
        public static MMI_M_BUTTONS_TRAIN_DATA MMI_M_BUTTONS
        {
            set => _mButtons = value;
        }

        #region to be deleted?
        /*
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
      
        */
        #endregion
    }
}
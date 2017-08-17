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

        /// <summary>
        /// Initialise EVC107 MMI_New_Train_Data telegram.
        /// </summary>
        /// <param name="pool"></param>
        public static void Initialise(SignalPool pool)
        {
            _pool = pool;
        }

        private static void CheckSimpleField(ushort varToCheck, bool bResult, string varPath)
        {
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

        public static byte Check_MMI_M_BRAKE_PERC
        {
            set
            {
                _nidKeyTrainCat = (byte)value;
                _varPath = "CCUO_ETCS1NewTrainData_MmiMBrakePerc";
                _bResult = _pool.SITR.Client.Read(_varPath).Equals(_mBrakePerc);
                CheckSimpleField(_mBrakePerc, _bResult, _varPath);
            }
        }

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
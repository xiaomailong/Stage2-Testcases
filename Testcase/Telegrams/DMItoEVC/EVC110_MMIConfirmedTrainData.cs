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
    public static class EVC110_MMIConfirmedTrainData
    {
        private static SignalPool _pool;
        private static bool _checkResult;
        private static byte _trainsetID;
        private static byte _mAltDem;

        private const string BaseString = "DMI->ETCS: EVC-110 [MMI_CONFIRMED_TRAIN_DATA]";

        /// <summary>
        /// Initialise EVC110 MMI_Confirmed_Train_Data telegram.
        /// </summary>
        /// <param name="pool"></param>
        public static void Initialise(SignalPool pool)
        {
            _pool = pool;
            _pool.SITR.SMDCtrl.CCUO.ETCS1ConfirmedTrainData.Value = 0x0001;
            _pool.SITR.SMDStat.CCUO.ETCS1ConfirmedTrainData.Value = 0x00;            
        }

        private static void CheckConfirmedTrainData()
        {
            // Reset telegram received flag in RTSim
            _pool.SITR.SMDStat.CCUO.ETCS1ConfirmedTrainData.Value = 0x00;

            // Check if telegram received flag has been set. Allows 20 seconds to enter train data.
            if (_pool.SITR.SMDStat.CCUO.ETCS1ConfirmedTrainData.WaitForCondition(Is.Equal, 1, 20000, 100))
            {
                // Check all static fields
                _checkResult = _pool.SITR.CCUO.ETCS1ConfirmedTrainData.MmiLTrainR.Value.Equals(Convert.ToUInt16(~EVC6_MMICurrentTrainData.MMI_L_TRAIN)) &
                               _pool.SITR.CCUO.ETCS1ConfirmedTrainData.MmiVMaxtrainR.Value.Equals(Convert.ToUInt16(~EVC6_MMICurrentTrainData.MMI_M_DATA_ENABLE)) &
                               _pool.SITR.CCUO.ETCS1ConfirmedTrainData.MmiNidKeyTrainCatR.Value.Equals(Convert.ToByte(~EVC6_MMICurrentTrainData.MMI_NID_KEY_TRAIN_CAT)) &
                               _pool.SITR.CCUO.ETCS1ConfirmedTrainData.MmiMBrakePercR.Value.Equals(Convert.ToByte(~EVC6_MMICurrentTrainData.MMI_M_BRAKE_PERC)) &
                               _pool.SITR.CCUO.ETCS1ConfirmedTrainData.MmiNidKeyAxleLoadR.Value.Equals(Convert.ToByte(~EVC6_MMICurrentTrainData.MMI_NID_KEY_AXLE_LOAD)) &
                               _pool.SITR.CCUO.ETCS1ConfirmedTrainData.MmiMAirtightR.Value.Equals(Convert.ToByte(~EVC6_MMICurrentTrainData.MMI_M_AIRTIGHT)) &
                               _pool.SITR.CCUO.ETCS1ConfirmedTrainData.MmiNidKeyLoadGaugeR.Value.Equals(Convert.ToByte(~EVC6_MMICurrentTrainData.MMI_NID_KEY_LOAD_GAUGE));
                            
                // If check passes
                if (_checkResult)
                {
                    _pool.TraceReport(BaseString + Environment.NewLine +
                        "MMI_L_TRAIN = " + 0 + Environment.NewLine +
                        "MMI_V_MAXTRAIN = " + 0 + Environment.NewLine +
                        "MMI_NID_KEY_TRAIN_CAT = \"" + MMI_NID_KEY.NoDedicatedKey + "\"" + Environment.NewLine +
                        "MMI_M_BRAKE_PERC = " + 0 + Environment.NewLine +
                        "MMI_NID_KEY_AXLE_LOAD = \"" + MMI_NID_KEY.NoDedicatedKey + "\"" + Environment.NewLine +
                        "MMI_M_AIRTIGHT = " + 0 + Environment.NewLine +
                        "MMI_NID_KEY_LOAD_GAUGE = \"" + MMI_NID_KEY.NoDedicatedKey + "\"" + Environment.NewLine +
                        "MMI_M_ALT_DEM = " + 0 + Environment.NewLine +
                        "Result = PASSED.");
                }
                // Else display the real value extracted from EVC-107
                else
                {
                    _pool.TraceError(BaseString + Environment.NewLine +
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
            }
            // Show generic DMI -> EVC telegram failure
            else
            {
                DmiExpectedResults.DMItoEVC_Telegram_Not_Received(_pool, BaseString);
            }
        }   
    }
}
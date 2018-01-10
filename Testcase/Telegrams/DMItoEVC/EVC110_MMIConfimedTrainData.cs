﻿using System;
using CL345;
using Testcase.Telegrams.EVCtoDMI;
using Testcase.DMITestCases;
using BT_CSB_Tools.SignalPoolGenerator.Signals.PdSignal.Misc;

namespace Testcase.Telegrams.DMItoEVC
{
    /// <summary>
    /// This packet shall be sent when the driver acts during the Train Data Entry procedure. It covers the following use cases:
    /// 1. Driver accepts a data element by pressing 'Enter'.
    /// 2. Driver accepts a data element by pressing 'Enter_Delay_Type'.
    /// 3. Driver completes entering a data block by pressing 'Yes'.
    /// 4. Driver overrules an operational check rule by pressing 'Delay Type Yes'.
    /// </summary>
    public static class EVC110_MMIConfimedTrainData
    {
        private static SignalPool _pool;
        private static bool _checkResult;
        private static ushort _trainsetID;
        private static ushort _mAltDem;

        static string baseString = "DMI->ETCS: EVC-110 [MMI_CONFIRMED_TRAIN_DATA]";
        static string baseString1 = "ETCS->DMI: EVC-6 [MMI_CURRENT_TRAIN_DATA]";

        /// <summary>
        /// Initialise EVC110 MMI_New_Train_Data telegram.
        /// </summary>
        /// <param name="pool"></param>
        public static void Initialise(SignalPool pool)
        {
            _pool = pool;
            _pool.SITR.SMDCtrl.CCUO.ETCS1ConfirmedTrainData.Value = 0x0001;
            _pool.SITR.SMDStat.CCUO.ETCS1ConfirmedTrainData.Value = 0x00;
        }

        public static void CheckConfirmedTrainData()
        {
            // Reset telegram received flag in RTSim
            _pool.SITR.SMDStat.CCUO.ETCS1ConfirmedTrainData.Value = 0x00;

            // Check if telegram received flag has been set. Allows 20 seconds to enter train data.
            if (_pool.SITR.SMDStat.CCUO.ETCS1ConfirmedTrainData.WaitForCondition(Is.Equal, 1, 20000, 100))
            {
                _trainsetID = (ushort) (_pool.SITR.CCUO.ETCS1ConfirmedTrainData.EVC110alias1.Value & 0x0F);
                _mAltDem = (ushort) ((_pool.SITR.CCUO.ETCS1ConfirmedTrainData.EVC110alias1.Value & 0xC0) >> 6);

                // Check all static fields
                _checkResult =
                    _pool.SITR.CCUO.ETCS1ConfirmedTrainData.MmiLTrainR.Value.Equals(EVC10_MMIEchoedTrainData
                        .MMI_L_TRAIN_) &
                    _pool.SITR.CCUO.ETCS1ConfirmedTrainData.MmiVMaxtrainR.Value.Equals(EVC10_MMIEchoedTrainData
                        .MMI_V_MAXTRAIN_) &
                    _pool.SITR.CCUO.ETCS1ConfirmedTrainData.MmiNidKeyTrainCatR.Value.Equals(EVC10_MMIEchoedTrainData
                        .MMI_NID_KEY_TRAIN_CAT_) &
                    _pool.SITR.CCUO.ETCS1ConfirmedTrainData.MmiMBrakePercR.Value.Equals(EVC10_MMIEchoedTrainData
                        .MMI_M_BRAKE_PERC_) &
                    _pool.SITR.CCUO.ETCS1ConfirmedTrainData.MmiNidKeyAxleLoadR.Value.Equals(EVC10_MMIEchoedTrainData
                        .MMI_NID_KEY_AXLE_LOAD_R) &
                    _pool.SITR.CCUO.ETCS1ConfirmedTrainData.MmiMAirtightR.Value.Equals(EVC10_MMIEchoedTrainData
                        .MMI_M_AIRTIGHT_R) &
                    _pool.SITR.CCUO.ETCS1ConfirmedTrainData.MmiNidKeyLoadGaugeR.Value.Equals(EVC10_MMIEchoedTrainData
                        .MMI_NID_KEY_LOAD_GAUGE_) &
                    _mAltDem.Equals((EVC10_MMIEchoedTrainData.EVC10_alias_1 & 0x0C) >> 2) &
                    _trainsetID.Equals(Convert.ToUInt16(~EVC6_MMICurrentTrainData.MMI_M_TRAINSET_ID & 0xF0) >> 4);

                // If check passes
                if (_checkResult)
                {
                    _pool.TraceReport(baseString + " = " + baseString1 + " bit-inverted." + Environment.NewLine +
                                      "Result = PASSED.");
                }
                // Else display the real value extracted from EVC-110 and EVC-6 bit inverted
                else
                {
                    _pool.TraceError($"{baseString} ({baseString1} bit-inverted):" + Environment.NewLine +
                                     "MMI_V_MAXTRAIN_R = " +
                                     _pool.SITR.CCUO.ETCS1ConfirmedTrainData.MmiVMaxtrainR.Value + Environment.NewLine +
                                     "MMI_L_TRAIN_R = " + _pool.SITR.CCUO.ETCS1ConfirmedTrainData.MmiLTrainR.Value +
                                     Environment.NewLine +
                                     "MMI_M_ALT_DEM_R = " + _mAltDem + Environment.NewLine +
                                     "MMI_M_TRAINSET_ID_R = " + _trainsetID + Environment.NewLine +
                                     "MMI_NID_KEY_LOAD_GAUGE_R = " +
                                     _pool.SITR.CCUO.ETCS1ConfirmedTrainData.MmiNidKeyLoadGaugeR.Value +
                                     Environment.NewLine +
                                     "MMI_M_AIRTIGHT_R = \"" +
                                     _pool.SITR.CCUO.ETCS1ConfirmedTrainData.MmiMAirtightR.Value + Environment.NewLine +
                                     "MMI_NID_KEY_AXLE_LOAD_R = " +
                                     _pool.SITR.CCUO.ETCS1ConfirmedTrainData.MmiNidKeyAxleLoadR.Value +
                                     Environment.NewLine +
                                     "MMI_M_BRAKE_PERC_R = " +
                                     _pool.SITR.CCUO.ETCS1ConfirmedTrainData.MmiMBrakePercR.Value +
                                     Environment.NewLine +
                                     "MMI_NID_KEY_TRAIN_CAT_R = " +
                                     _pool.SITR.CCUO.ETCS1ConfirmedTrainData.MmiNidKeyTrainCatR.Value +
                                     Environment.NewLine +
                                     "Result: FAILED!");
                }
            }
            // Show generic DMI -> EVC telegram failure
            else
            {
                DmiExpectedResults.DMItoEVC_Telegram_Not_Received(_pool, baseString);
            }
        }
    }
}
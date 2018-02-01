using System;
using CL345;
using Testcase.DMITestCases;
using BT_CSB_Tools.SignalPoolGenerator.Signals.PdSignal.Misc;


namespace Testcase.Telegrams.DMItoEVC
{
    /// <summary>
    /// This packet shall be sent when the driver has entered or validated the train running number.
    /// </summary>
    public static class EVC116_MMINewTrainNumber
    {
        private static SignalPool _pool;
        private static bool _checkResult;
        private static uint _nidOperation;
        const string baseString = "DMI->ETCS: EVC-116 [MMI_NEW_TRAIN_NUMBER]";

        /// <summary>
        /// Initialise EVC-116 MMI_New_Train_Number.
        /// </summary>
        /// <param name="pool"></param>
        public static void Initialise(SignalPool pool)
        {
            _pool = pool;
            _pool.SITR.SMDStat.CCUO.ETCS1NewTrainNumber.Value = 0x00;
            _pool.SITR.SMDCtrl.CCUO.ETCS1NewTrainNumber.Value = 0x0001;
        }

        private static void CheckNidOperation(uint nidOperation)
        {
            // Check if telegram received flag has been set. Allows 20 seconds to enter Train Running Number.
            if (_pool.SITR.SMDStat.CCUO.ETCS1NewTrainNumber.WaitForCondition(Is.Equal, 1, 20000, 100))
            {
                _checkResult = _pool.SITR.CCUO.ETCS1NewTrainNumber.MmiNidOperation.Value.Equals(nidOperation);

                // If check passes
                if (_checkResult)
                {
                    _pool.TraceReport(string.Format("{0} - [MMI_NID_OPERATION] = {1}", baseString, nidOperation) + Environment.NewLine +
                                      "Result: PASSED.");
                }
                // Else display the real value extracted from EVC-104
                else
                {
                    _pool.TraceError(string.Format("{0} - [MMI_NID_OPERATION] = ", baseString) +
                                     _pool.SITR.CCUO.ETCS1NewTrainNumber.MmiNidOperation.Value +
                                     Environment.NewLine + "Result: FAILED");
                }
            }
            // Show generic DMI -> EVC telegram failure
            else
            {
                DmiExpectedResults.DMItoEVC_Telegram_Not_Received(_pool, baseString);
            }

            // Reset telegram received flag in RTSim
            _pool.SITR.SMDStat.CCUO.ETCS1NewTrainNumber.Value = 0x00;
        }

        /// <summary>
        /// This is the operational train running number.
        /// 
        /// Note: Definition according to Subset-026, 7.5.1.92.
        /// Binary Coded Decimal
        /// 
        /// For each digit: 
        /// 0-9: Digit value
        /// A-E: Not used, spare
        /// F: No digit (used for shorter numbers or when not applicable) or special value (see below)
        /// 
        /// E.g. “1234567” is coded as 0x1234567F 
        /// 
        /// Special values:
        /// 0xFFFFFFFF = 'Unknown Train Running Number'
        /// </summary>
        public static uint Check_NID_OPERATION
        {
            set
            {
                _nidOperation = value;
                CheckNidOperation(_nidOperation);
            }
        }

        /// <summary>
        /// This is the operational train running number.
        /// 
        /// Note: Definition according to Subset-026, 7.5.1.92.
        /// Binary Coded Decimal
        /// 
        /// For each digit: 
        /// 0-9: Digit value
        /// A-E: Not used, spare
        /// F: No digit (used for shorter numbers or when not applicable) or special value (see below)
        /// 
        /// E.g. “1234567” is coded as 0x1234567F 
        /// 
        /// Special values:
        /// 0xFFFFFFFF = 'Unknown Train Running Number'
        /// </summary>
        public static uint Get_NID_OPERATION
        {
            get
            {
                // Reset telegram received flag in RTSim
                _pool.SITR.SMDStat.CCUO.ETCS1NewTrainNumber.Value = 0x00;

                if (_pool.SITR.SMDStat.CCUO.ETCS1NewTrainNumber.WaitForCondition(Is.Equal, 1, 20000, 100))
                {
                    _nidOperation = _pool.SITR.CCUO.ETCS1NewTrainNumber.MmiNidOperation.Value;
                    return _nidOperation;
                }
                else
                {
                    DmiExpectedResults.DMItoEVC_Telegram_Not_Received(_pool, baseString);
                    return 0xffffffff;
                }
            }
        }
    }
}
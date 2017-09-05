using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using CL345;
using Testcase.Telegrams.DMItoEVC;
using Testcase.Telegrams.EVCtoDMI;
using static Testcase.Telegrams.EVCtoDMI.Variables;


namespace Testcase.Telegrams.DMItoEVC
{
    /// <summary>
    /// This packet shall be sent when the driver has entered or validated the train running number.
    /// </summary>
    public static class EVC116_MMINewTrainNumber
    {
        private static SignalPool _pool;
        private static bool _bResult;
        private static uint _nidOperation;

        /// <summary>
        /// Initialise EVC-116 MMI_New_Train_Number.
        /// </summary>
        /// <param name="pool"></param>
        public static void Initialise(SignalPool pool)
        {
            _pool = pool;
        }

        private static void CheckNidOperation(uint nidOperation)
        {
            _bResult = _pool.SITR.CCUO.ETCS1NewTrainNumber.MmiNidOperation.Value.Equals(nidOperation);            
           
            if (_bResult) // if check passes
            {
                _pool.TraceReport("DMI->ETCS: EVC-116 [MMI_NEW_TRAIN_NUMBER.MMI_NID_OPERATION] => " +
                    nidOperation + " PASSED.");
            }
            else // else display the real value extracted from EVC-104 [MMI_NEW_DRIVER_DATA.MMI_X_DRIVER_ID]
            {
                _pool.TraceError("DMI->ETCS: Check EVC-116 [MMI_NEW_TRAIN_NUMBER.MMI_NID_OPERATION] => " +
                    _pool.SITR.CCUO.ETCS1NewTrainNumber.MmiNidOperation.Value + " FAILED");
            }
        }

        /// <summary>
        /// This is the operational train running number.
        /// Note: Definition according to Subset-026, 7.5.1.92.
        /// Binary Coded Decimal
        /// For each digit: 
        /// Values 0-9	Digit value
        /// Values A-E Not used, spare
        /// Value F No digit(used for shorter numbers or when not applicable) or special value(see below)
        /// E.g. “1234567” is coded as 0x1234567F 
        /// Special values:
        /// 0xFFFFFFFF	'Unknown Train Running Number'
        /// </summary>
        public static uint Check_NID_OPERATION
        {
            set
            {
                _nidOperation = value;
                CheckNidOperation(_nidOperation);
            }
        }
    }
}
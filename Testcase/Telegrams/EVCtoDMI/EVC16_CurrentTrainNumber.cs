using CL345;

namespace Testcase.Telegrams.EVCtoDMI
{
    /// <summary>
    /// This packet shall be sent when the driver is intended to enter/validate/view train running number.
    /// </summary>
    public static class EVC16_CurrentTrainNumber
    {
        private static SignalPool _pool;

        /// <summary>
        /// Initialse EVC-16 Current_Train_Number telegram.
        /// </summary>
        /// <param name="pool"></param>
        public static void Initialise(SignalPool pool)
        {
            _pool = pool;

            // set default values
            _pool.SITR.ETCS1.CurrentTrainNumber.MmiMPacket.Value = 16;
            _pool.SITR.ETCS1.CurrentTrainNumber.MmiLPacket.Value = 64;
        }

        /// <summary>
        /// Note: Definition according to Subset-026, 7.5.1.92.
        /// Binary Coded Decimal
        /// For each digit:
        /// Values 0-9 Digit value
        /// Values A-E Not used, spare
        /// Value F No digit (used for shorter numbers or when not applicable) or special value
        /// (see below)
        /// E.g. “1234567” is coded as 0x1234567F
        /// Special values:
        /// 0xFFFFFFFF 'Unknown Train Running Number'
        /// </summary>
        public static uint TrainRunningNumber
        {
            set => _pool.SITR.ETCS1.CurrentTrainNumber.MmiNidOperation.Value = value;
        }

        /// <summary>
        /// Send EVC-16 telegram.
        /// </summary>
        public static void Send()
        {
            _pool.SITR.SMDCtrl.ETCS1.CurrentTrainNumber.Value = 1;
        }
    }
}
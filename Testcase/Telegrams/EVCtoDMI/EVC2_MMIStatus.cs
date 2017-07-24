using CL345;

namespace Testcase.Telegrams.EVCtoDMI
{
    static class EVC2_MMIStatus
    {
        private static SignalPool _pool;

        public static void Initialise(SignalPool pool)
        {
            _pool = pool;

            // Set default values
            _pool.TraceInfo("ETCS->DMI: EVC-2 (MMI_STATUS) \"Cab 1 Active\"");

            _pool.SITR.ETCS1.Status.MmiMPacket.Value = 2;
            _pool.SITR.ETCS1.Status.MmiLPacket.Value = 72;
            _pool.SITR.ETCS1.Status.EVC2alias1.Value = 16; //Cab 1 active
            _pool.SITR.SMDCtrl.ETCS1.Status.Value = 1;
        }

        public static uint TrainRunningNumber
        {
            get => _pool.SITR.ETCS1.Status.MmiNidOperation.Value;
            set => _pool.SITR.ETCS1.Status.MmiNidOperation.Value = value;
        }

        public static void Send()
        {
        }
    }
}
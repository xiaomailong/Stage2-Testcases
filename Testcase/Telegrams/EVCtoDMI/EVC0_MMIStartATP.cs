using CL345;

namespace Testcase.Telegrams.EVCtoDMI
{
    /// <summary>
    /// This packet shall be sent when the ETC needs to establish contact and exchange start-up information with the MMI.
    /// </summary>
    static class EVC0_MMIStartATP
    {
        private static SignalPool _pool;

        /// <summary>
        /// Initialise EVC-0 telegram.
        /// </summary>
        /// <param name="pool"></param>
        public static void Initialise(SignalPool pool)
        {
            _pool = pool;

            // Set default values
            _pool.SITR.ETCS1.StartAtp.MmiMPacket.Value = 0;
            _pool.SITR.ETCS1.StartAtp.MmiLPacket.Value = 40;
        }

        public static EVC0Type Evc0Type { get; set; }

        /// <summary>
        /// Send EVC-0 telegram.
        /// </summary>
        public static void Send()
        {
            switch (Evc0Type)
            {
                case EVC0Type.GoToIdle:
                    _pool.TraceInfo("ETCS->DMI: EVC-0 (MMI_START_ATP) \"Go to Idle state\"");
                    _pool.SITR.ETCS1.StartAtp.MmiMStartReq.Value = 1;
                    _pool.SITR.SMDCtrl.ETCS1.StartAtp.Value = 1;
                    break;
                case EVC0Type.VersionInfo:
                    _pool.TraceInfo("ETCS->DMI: EVC-0 (MMI_START_ATP) \"Version info request\"");
                    _pool.SITR.ETCS1.StartAtp.MmiMStartReq.Value = 0;
                    _pool.SITR.SMDCtrl.ETCS1.StartAtp.Value = 1;
                    break;
            }
        }

        /// <summary>
        /// EVC-0 telegram start-up type
        /// 
        /// Values:
        /// Request Version Information
        /// Go to Idle
        /// </summary>
        public enum EVC0Type
        {
            VersionInfo,
            GoToIdle
        }
    }
}
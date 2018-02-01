#region usings

using CL345;

#endregion

namespace Testcase.Telegrams.EVCtoDMI
{
    /// <summary>
    /// This packet shall be sent when the ETC needs to establish contact and exchange start-up information with the MMI.
    /// </summary>
    public static class EVC0_MMIStartATP
    {
        private static SignalPool _pool;

        /// <summary>
        /// Initialise EVC-0 MMI Start ATP telegram.
        /// </summary>
        /// <param name="pool">The SignalPool</param>
        public static void Initialise(SignalPool pool)
        {
            _pool = pool;

            // Set default values
            _pool.SITR.ETCS1.StartAtp.MmiMPacket.Value = 0;
            _pool.SITR.ETCS1.StartAtp.MmiLPacket.Value = 40;
            _pool.SITR.ETCS1.StartAtp.MmiMStartReq.Value = 0;
        }

        public static EVC0Type Evc0Type { get; set; }

        /// <summary>
        /// Send EVC-0 MMI Start ATP telegram.
        /// </summary>
        public static void Send()
        {
            switch (Evc0Type)
            {
                case EVC0Type.GoToIdle:
                    _pool.TraceInfo("ETCS->DMI: EVC-0 (MMI_START_ATP) \"Go to Idle state\"");
                    _pool.SITR.ETCS1.StartAtp.MmiMStartReq.Value = 1;
                    _pool.SITR.SMDCtrl.ETCS1.StartAtp.Value = 0x0001;
                    break;
                case EVC0Type.VersionInfo:
                    _pool.TraceInfo("ETCS->DMI: EVC-0 (MMI_START_ATP) \"Version info request\"");
                    _pool.SITR.ETCS1.StartAtp.MmiMStartReq.Value = 0;
                    _pool.SITR.SMDCtrl.ETCS1.StartAtp.Value = 0x0001;
                    break;
            }
        }

        /// <summary>
        /// EVC-0 telegram start-up type
        /// 
        /// Values:
        /// 0 = "Version info request"
        /// 1 = "Go to Idle state"
        /// 2 = "Error: MMI type not supported"
        /// 3 = "Error: Incompatible IF versions"
        /// 4 = "Error: Incompatible SW versions"
        /// 5..9 = "Spare"
        /// 10 = "DMI reboot. Indication error"
        /// 11..255 = "Spare"
        /// </summary>
        public enum EVC0Type : byte
        {
            VersionInfo = 0,
            GoToIdle = 1,
            MMITypeNotSupported = 2,
            IncompatibleIFVersions = 3,
            IncompatibleSWVersions = 4,
            DMIRebootIndicationError = 10
        }
    }
}
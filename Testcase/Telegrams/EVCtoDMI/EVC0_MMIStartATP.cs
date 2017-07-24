using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CL345;

namespace Testcase.Telegrams
{
    static class EVC0_MMIStartATP
    {
        private static SignalPool _pool;

        public static void Initialise(SignalPool pool)
        {
            _pool = pool;

            // Set default values
            _pool.SITR.ETCS1.StartAtp.MmiMPacket.Value = 0;
            _pool.SITR.ETCS1.StartAtp.MmiLPacket.Value = 40;
        }

        public static EVC0Type Evc0Type { get; set; }

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

        public enum EVC0Type
        {
            VersionInfo,
            GoToIdle
        }
    }
}
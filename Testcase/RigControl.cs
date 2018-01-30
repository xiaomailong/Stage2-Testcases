using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CL345;

namespace Testcase
{
    static class RigControl
    {
        public static void DeActivateBothCabs(SignalPool pool)
        {
            pool.SITR.Client.Write("SCT1_MCS_Shutdown", 1);
            pool.SITR.Client.Write("SCT1_MCS_Forward", 0);
            pool.SITR.Client.Write("SCT1_MCS_Secure", 0);
            pool.SITR.Client.Write("SCT1_MCS_Recovery", 0);
            pool.SITR.Client.Write("SCT1_MCS_Reverse", 0);
            pool.SITR.Client.Write("SCT1_MCS_KeySwitch", 0);

            pool.SITR.Client.Write("SCT2_MCS_Shutdown", 1);
            pool.SITR.Client.Write("SCT2_MCS_Forward", 0);
            pool.SITR.Client.Write("SCT2_MCS_Secure", 0);
            pool.SITR.Client.Write("SCT2_MCS_Recovery", 0);
            pool.SITR.Client.Write("SCT2_MCS_Reverse", 0);
            pool.SITR.Client.Write("SCT2_MCS_KeySwitch", 0);
        }

        public static void ActivateCab1(SignalPool pool)
        {
            var shutdown = Convert.ToInt32(pool.SITR.Client.Read("SCT1_MCS_Shutdown"));
            var forward = Convert.ToInt32(pool.SITR.Client.Read("SCT1_MCS_Forward"));
            var secure = Convert.ToInt32(pool.SITR.Client.Read("SCT1_MCS_Secure"));
            var recovery = Convert.ToInt32(pool.SITR.Client.Read("SCT1_MCS_Recovery"));
            var reverse = Convert.ToInt32(pool.SITR.Client.Read("SCT1_MCS_Reverse"));
            var keyswitch = Convert.ToInt32(pool.SITR.Client.Read("SCT1_MCS_KeySwitch"));

            if (shutdown != 1 && forward != 0 && secure != 0 && recovery != 0 && reverse != 0 && keyswitch != 0)
            {
                pool.TraceError("RigControl: Cab 1 activation commanded but the MCS is in the wrong state");
            }

            pool.SITR.Client.Write("SCT1_MCS_Shutdown", 0);
            pool.SITR.Client.Write("SCT1_MCS_Forward", 1);
            pool.SITR.Client.Write("SCT1_MCS_Secure", 0);
            pool.SITR.Client.Write("SCT1_MCS_Recovery", 0);
            pool.SITR.Client.Write("SCT1_MCS_Reverse", 0);
            pool.SITR.Client.Write("SCT1_MCS_KeySwitch", 1);
        }

        public static void ActivateCab2(SignalPool pool)
        {
            var shutdown = Convert.ToInt32(pool.SITR.Client.Read("SCT2_MCS_Shutdown"));
            var forward = Convert.ToInt32(pool.SITR.Client.Read("SCT2_MCS_Forward"));
            var secure = Convert.ToInt32(pool.SITR.Client.Read("SCT2_MCS_Secure"));
            var recovery = Convert.ToInt32(pool.SITR.Client.Read("SCT2_MCS_Recovery"));
            var reverse = Convert.ToInt32(pool.SITR.Client.Read("SCT2_MCS_Reverse"));
            var keyswitch = Convert.ToInt32(pool.SITR.Client.Read("SCT2_MCS_KeySwitch"));

            if (shutdown != 1 && forward != 0 && secure != 0 && recovery != 0 && reverse != 0 && keyswitch != 0)
            {
                pool.TraceError("RigControl: Cab 2 activation commanded but the MCS is in the wrong state");
            }

            pool.SITR.Client.Write("SCT2_MCS_Shutdown", 0);
            pool.SITR.Client.Write("SCT2_MCS_Forward", 1);
            pool.SITR.Client.Write("SCT2_MCS_Secure", 0);
            pool.SITR.Client.Write("SCT2_MCS_Recovery", 0);
            pool.SITR.Client.Write("SCT2_MCS_Reverse", 0);
            pool.SITR.Client.Write("SCT2_MCS_KeySwitch", 1);
        }

    }
}

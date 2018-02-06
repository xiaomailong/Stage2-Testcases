using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CL345;

namespace Testcase
{
    internal static class RigControl
    {
        /// <summary>
        /// Sets state of cab 1 or 2
        /// Setting one cab automatically deactivates the other
        /// </summary>
        /// <param name="cabNo">1 or 2 for DMS1 or DMS2</param>
        /// <param name="state">The state of the MCS switch, Shutdown implies KeySwitch off</param>
        public static void SetMCSState(SignalPool pool, int cabNo, CabState state)
        {
            switch (state)
            {
                case CabState.Shutdown:
                    pool.SITR.Client.Write("SCT" + cabNo + "_MCS_Shutdown", 1);
                    pool.SITR.Client.Write("SCT" + cabNo + "_MCS_Forward", 0);
                    pool.SITR.Client.Write("SCT" + cabNo + "_MCS_Secure", 0);
                    pool.SITR.Client.Write("SCT" + cabNo + "_MCS_Recovery", 0);
                    pool.SITR.Client.Write("SCT" + cabNo + "_MCS_Reverse", 0);
                    pool.SITR.Client.Write("SCT" + cabNo + "_MCS_KeySwitch", 0);
                    break;
                case CabState.Forward:
                    pool.SITR.Client.Write("SCT" + cabNo + "_MCS_Shutdown", 0);
                    pool.SITR.Client.Write("SCT" + cabNo + "_MCS_Forward", 1);
                    pool.SITR.Client.Write("SCT" + cabNo + "_MCS_Secure", 0);
                    pool.SITR.Client.Write("SCT" + cabNo + "_MCS_Recovery", 0);
                    pool.SITR.Client.Write("SCT" + cabNo + "_MCS_Reverse", 0);
                    pool.SITR.Client.Write("SCT" + cabNo + "_MCS_KeySwitch", 1);
                    break;
                case CabState.Secure:
                    pool.SITR.Client.Write("SCT" + cabNo + "_MCS_Shutdown", 0);
                    pool.SITR.Client.Write("SCT" + cabNo + "_MCS_Forward", 0);
                    pool.SITR.Client.Write("SCT" + cabNo + "_MCS_Secure", 1);
                    pool.SITR.Client.Write("SCT" + cabNo + "_MCS_Recovery", 0);
                    pool.SITR.Client.Write("SCT" + cabNo + "_MCS_Reverse", 0);
                    pool.SITR.Client.Write("SCT" + cabNo + "_MCS_KeySwitch", 1);
                    break;
                case CabState.Recovery:
                    pool.SITR.Client.Write("SCT" + cabNo + "_MCS_Shutdown", 0);
                    pool.SITR.Client.Write("SCT" + cabNo + "_MCS_Forward", 0);
                    pool.SITR.Client.Write("SCT" + cabNo + "_MCS_Secure", 0);
                    pool.SITR.Client.Write("SCT" + cabNo + "_MCS_Recovery", 1);
                    pool.SITR.Client.Write("SCT" + cabNo + "_MCS_Reverse", 0);
                    pool.SITR.Client.Write("SCT" + cabNo + "_MCS_KeySwitch", 1);
                    break;
                case CabState.Reverse:
                    pool.SITR.Client.Write("SCT" + cabNo + "_MCS_Shutdown", 0);
                    pool.SITR.Client.Write("SCT" + cabNo + "_MCS_Forward", 0);
                    pool.SITR.Client.Write("SCT" + cabNo + "_MCS_Secure", 0);
                    pool.SITR.Client.Write("SCT" + cabNo + "_MCS_Recovery", 0);
                    pool.SITR.Client.Write("SCT" + cabNo + "_MCS_Reverse", 1);
                    pool.SITR.Client.Write("SCT" + cabNo + "_MCS_KeySwitch", 1);
                    break;
                default:
                    throw new ArgumentOutOfRangeException("state", state, null);
            }

            int invCab = cabNo == 1 ? 2 : 1;

            pool.SITR.Client.Write("SCT" + invCab + "_MCS_Shutdown", 1);
            pool.SITR.Client.Write("SCT" + invCab + "_MCS_Forward", 0);
            pool.SITR.Client.Write("SCT" + invCab + "_MCS_Secure", 0);
            pool.SITR.Client.Write("SCT" + invCab + "_MCS_Recovery", 0);
            pool.SITR.Client.Write("SCT" + invCab + "_MCS_Reverse", 0);
            pool.SITR.Client.Write("SCT" + invCab + "_MCS_KeySwitch", 0);
        }

        /// <summary>
        /// State of MCS, Shutdown implies KeySwitch off, rest are KeySwitch on
        /// </summary>
        public enum CabState
        {
            Shutdown,
            Forward,
            Secure,
            Recovery,
            Reverse
        }
    }
}
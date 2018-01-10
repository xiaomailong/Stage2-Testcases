#region usings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using CL345;
using Testcase.Telegrams.EVCtoDMI;
using Testcase.DMITestCases;
using BT_CSB_Tools.SignalPoolGenerator.Signals.PdSignal.Misc;
using static Testcase.Telegrams.EVCtoDMI.Variables;

#endregion

namespace Testcase.Telegrams.DMItoEVC
{
    /// <summary>
    /// This packet shall be sent whenever the time is changed in the MMI clock function. The content is the same as in packet 3.
    /// </summary>
    public static class EVC109_MMISetTimeMMI
    {
        private static SignalPool _pool;
        private static bool _checkResult;
        private const string BaseString = "DMI->ETCS: EVC-109 [MMI_SET_TIME_MMI]";

        /// <summary>
        /// Initialise EVC-109 MMI_SET_TIME_MMI.
        /// </summary>
        /// <param name="pool">SignalPool</param>
        public static void Initialise(SignalPool pool)
        {
            _pool = pool;
            _pool.SITR.SMDStat.CCUO.ETCS1SetTimeMmi.Value = 0x00;
            _pool.SITR.SMDCtrl.CCUO.ETCS1SetTimeMmi.Value = 0x0001;
        }

        /// <summary>
        /// Check the time and time zone, sent via EVC109, match that entered on the DMI.
        /// </summary>
        private static void CheckTimeAndZone(uint totalSeconds, sbyte zoneOffset)
        {
            // Reset telegram received flag in RTSim
            _pool.SITR.SMDStat.CCUO.ETCS1SetTimeMmi.Value = 0x00;

            // Check if telegram received flag has been set. Allow 60 seconds to enter Time.
            if (_pool.SITR.SMDStat.CCUO.ETCS1SetTimeMmi.WaitForCondition(Is.Equal, 1, 60000, 100))
            {
                // Check if UTC time, and zone offset are as expected
                _checkResult = _pool.SITR.CCUO.ETCS1SetTimeMmi.MmiTUTC.Value.Equals(totalSeconds) &
                               _pool.SITR.CCUO.ETCS1SetTimeMmi.MmiTZoneOffset.Equals(zoneOffset);

                // If check passes
                if (_checkResult)
                {
                    _pool.TraceReport($"{BaseString} - MMI_T_UTC = {totalSeconds}" + Environment.NewLine +
                                      $"MMI_T_ZONE_OFFSET = {zoneOffset}" + Environment.NewLine +
                                      "Result = PASSED.");
                }
                // Else display the real value extracted from EVC-109
                else
                {
                    _pool.TraceError($"{BaseString} - MMI_T_UTC = {_pool.SITR.CCUO.ETCS1SetTimeMmi.MmiTUTC.Value}" +
                                     Environment.NewLine +
                                     $"MMI_T_ZONE_OFFSET = {_pool.SITR.CCUO.ETCS1SetTimeMmi.MmiTZoneOffset.Value}" +
                                     Environment.NewLine +
                                     "Result: FAILED");
                }
            }
            // Show generic DMI -> EVC telegram failure
            else
            {
                DmiExpectedResults.DMItoEVC_Telegram_Not_Received(_pool, BaseString);
            }
        }

        /// <summary>
        /// This method calculates the number of seconds that have elapsed
        /// since 01.01.1970 00:00:00, and then passes the result to CheckTimeAndZone.
        /// Parameter T_UTC must be set to include the Year, Month, Day, Hours, Minutes and Seconds.
        /// </summary>
        public static void Check_MMI_Set_Time(DateTime T_UTC, sbyte T_Zone_Offset)
        {
            {
                uint seconds = (uint) (T_UTC - new DateTime(1970, 1, 1)).TotalSeconds;

                CheckTimeAndZone(seconds, T_Zone_Offset);
            }
        }
    }
}
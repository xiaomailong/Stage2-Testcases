using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using CL345;
using Testcase.DMITestCases;
using BT_CSB_Tools.SignalPoolGenerator.Signals.PdSignal.Misc;


namespace Testcase.Telegrams.DMItoEVC
{
    /// <summary>
    /// This packet shall be sent as the MMI's answer to MMI_START_ATP and contains version and status information.
    /// </summary>
    public static class EVC100_MMIStartMmi
    {
        private static SignalPool _pool;
        private static bool _checkResult;
        private static uint _mIfVer;
        private static uint _mSwVer;
        private static byte _mStartStatus;
        private const string BaseString = "DMI->ETCS: EVC-100 [MMI_START_MMI]";

        /// <summary>
        /// Initialise EVC-110 MMI_Start_Mmi.
        /// </summary>
        /// <param name="pool"></param>
        public static void Initialise(SignalPool pool)
        {
            _pool = pool;
            _pool.SITR.SMDStat.CCUO.ETCS1StartMmi.Value = 0x00;
            _pool.SITR.SMDCtrl.CCUO.ETCS1StartMmi.Value = 0x0001;
        }

        private static void CheckTelegram()
        {
            // Reset telegram received flag in RTSim
            _pool.SITR.SMDStat.CCUO.ETCS1StartMmi.Value = 0x00;

            // Check if telegram received flag has been set. Allows 10 seconds.
            if (_pool.SITR.SMDStat.CCUO.ETCS1StartMmi.WaitForCondition(Is.Equal, 1, 10000, 100))
            {
                // Check if values match
                _checkResult = _pool.SITR.CCUO.ETCS1StartMmi.MmiMIfVer.Value.Equals(_mIfVer) &
                               _pool.SITR.CCUO.ETCS1StartMmi.MmiMSwVer.Value.Equals(_mSwVer) &
                               _pool.SITR.CCUO.ETCS1StartMmi.MmiMStartStatus.Value.Equals(_mStartStatus);

                // If check passes
                if (_checkResult)
                {
                    _pool.TraceReport($"{BaseString}:" + Environment.NewLine +
                                      $"MMI_M_IF_VER = {(_mIfVer & 0xFF000000) >> 24}.{(_mIfVer & 0x00FF0000) >> 16}.{(_mIfVer & 0x0000FF00) >> 8} " + Environment.NewLine +
                                      $"MMI_M_SW_VER = {(_mSwVer & 0xFF000000) >> 24}.{(_mSwVer & 0x00FF0000) >> 16}.{(_mSwVer & 0x0000FF00) >> 8} " + Environment.NewLine +
                                      $"MMI_START_STATUS = {_mStartStatus} " + Environment.NewLine +
                                      "Result = PASSED.");
                }
                // Else display the real value extracted from EVC-100
                else
                {
                    _pool.TraceError($"{BaseString}:" + Environment.NewLine +
                                      $"MMI_M_IF_VER = {(_pool.SITR.CCUO.ETCS1StartMmi.MmiMIfVer.Value & 0xFF000000) >> 24}.{(_pool.SITR.CCUO.ETCS1StartMmi.MmiMIfVer.Value & 0x00FF0000) >> 16}.{(_pool.SITR.CCUO.ETCS1StartMmi.MmiMIfVer.Value & 0x0000FF00) >> 8} " + Environment.NewLine +
                                      $"MMI_M_SW_VER = {(_pool.SITR.CCUO.ETCS1StartMmi.MmiMSwVer.Value & 0xFF000000) >> 24}.{(_pool.SITR.CCUO.ETCS1StartMmi.MmiMSwVer.Value & 0x00FF0000) >> 16}.{(_pool.SITR.CCUO.ETCS1StartMmi.MmiMSwVer.Value & 0x0000FF00) >> 8} " + Environment.NewLine +
                                      $"MMI_START_STATUS = {_pool.SITR.CCUO.ETCS1StartMmi.MmiMStartStatus.Value} " + Environment.NewLine +
                                      "Result: FAILED.");
                }
            }
            // Show generic DMI -> EVC telegram failure
            else
            {
                DmiExpectedResults.DMItoEVC_Telegram_Not_Received(_pool, BaseString);
            }
        }

        /// <summary>
        /// This variable contains IF version, used by MMI X.Y.Z (first byte is X).
        /// 
        /// Bits:
        /// 0..7 = "X : UNSIGNED8"
        /// 8..15 = "Y : UNSIGNED8"
        /// 16..23 = "Z : UNSIGNED8"
        /// 24..31 = "spare"
        /// Note: 255 = This digit is not used
        /// Only digit Z or both Y and Z can be not used
        /// Examples:
        /// Data 1, 2, 5 = 0x01020500 means version 1.2.5
        /// Data 2, 0, 255 = 0x0200FF00 means version 2.0
        /// </summary>
        public static uint MMI_M_IF_VER
        {
            set => _mIfVer = value;
        }

        /// <summary>
        /// This variable contains MMI version X.Y.Z (first byte is X).
        /// 
        /// Bits:
        /// 0..7 = "X : UNSIGNED8"
        /// 8..15 = "Y : UNSIGNED8"
        /// 16..23 = "Z : UNSIGNED8"
        /// 24..31 = "spare"
        /// Note: 255 = This figure is not used
        /// Only figure Z or both Y and Z can be "not used"
        /// Examples:
        /// Data 1, 2, 5 = 0x01020500 means version 1.2.5
        /// Data 2, 0, 255 = 0x0200FF00 means version 2.0
        /// </summary>
        public static uint MMI_M_SW_VER
        {
            set => _mSwVer = value;
        }

        /// <summary>
        /// This variable indicates the start-up status of the MMI
        /// 
        /// Values:
        /// 0 = "MMI is OK"
        /// 1 = "Error"
        /// 2..255 = "Spare"
        /// </summary>
        public static byte MMI_M_START_STATUS
        {
            set => _mStartStatus = value;
        }
    }
}
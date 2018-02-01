using System;
using CL345;
using Testcase.DMITestCases;
using BT_CSB_Tools.SignalPoolGenerator.Signals.PdSignal.Misc;


namespace Testcase.Telegrams.DMItoEVC
{
    /// <summary>
    /// This packet shall be sent sporadically from DMI when the driver has confirmed data in the 'Set VBC' validation window.
    /// </summary>
    public static class EVC128_MMIConfirmedSetVBC
    {
        private const string BaseString = "DMI->ETCS: EVC-128 [MMI_CONFIRMED_SET_VBC]";

        private static SignalPool _pool;
        private static uint _vbcCode;

        /// <summary>
        /// Initialise EVC-128 MMI_Confirmed_Set_VBC
        /// </summary>
        /// <param name="pool"></param>
        public static void Initialise(SignalPool pool)
        {
            _pool = pool;
            _pool.SITR.SMDStat.CCUO.ETCS1ConfirmedSetVbc.Value = 0x00;
            _pool.SITR.SMDCtrl.CCUO.ETCS1ConfirmedSetVbc.Value = 0x01;
        }

        private static void CheckVBCCode()
        {
            uint invertedVBCCode = ~_vbcCode;

            // Check if telegram received flag has been set. Allows 20 seconds to enter driver ID.
            if (_pool.SITR.SMDStat.CCUO.ETCS1ConfirmedSetVbc.WaitForCondition(Is.Equal, 1, 20000, 100))
            {
                // Check if Driver ID matches
                if (_pool.SITR.CCUO.ETCS1ConfirmedSetVbc.MmiMVbcCodeR.Value.Equals(invertedVBCCode))
                {
                    _pool.TraceReport(string.Format("{0} - MMI_M_VBC_CODE_ = {1} - echoes value {2}", BaseString,
                                          invertedVBCCode, _vbcCode) +
                                      Environment.NewLine +
                                      "Result = PASSED.");
                }
                // Else display the real value extracted from EVC-104
                else
                {
                    _pool.TraceError(string.Format("{0} - MMI_M_VBC_CODE_ = ", BaseString) +
                                     _pool.SITR.CCUO.ETCS1ConfirmedSetVbc.MmiMVbcCodeR.Value +
                                     string.Format(" - should echo {0}", _vbcCode) + Environment.NewLine +
                                     "Result: FAILED");
                }
            }
            // Show generic DMI -> EVC telegram failure
            else
            {
                DmiExpectedResults.DMItoEVC_Telegram_Not_Received(_pool, BaseString);
            }

            // Reset telegram received flag in RTSim
            _pool.SITR.SMDStat.CCUO.ETCS1ConfirmedSetVbc.Value = 0x00;
        }

        /// <summary>
        /// The value set is used to check the current value in the EVC128 packet 
        /// The value in the packet is bit-inverted: accepts the echoed value (un-inverted)
        /// Values:
        /// 0..9 = "NID_C"
        /// 10..15 = "NID_VBCMK"
        /// 16..23 = "T_VBC"
        /// 24..31 = "spare"
        /// 
        /// </summary>
        public static uint Check_VBC_Code
        {
            set
            {
                _vbcCode = value;
                CheckVBCCode();
            }
        }
    }
}
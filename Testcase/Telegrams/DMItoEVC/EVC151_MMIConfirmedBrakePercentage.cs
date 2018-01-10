using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BT_CSB_Tools.SignalPoolGenerator.Signals.PdSignal.Misc;
using CL345;
using Testcase.DMITestCases;

namespace Testcase.Telegrams.DMItoEVC
{
    public static class EVC151_MMIConfirmedBrakePercentage
    {
        private const string BASE_STRING = "DMI->ETCS: EVC-151 [MMI_CONFIRMED_BRAKE_PERCENTAGE]";

        private static SignalPool _pool;
        private static bool _checkResult;

        public static byte MMI_M_BP_ORIG { get; set; }
        public static byte MMI_M_BP_CURRENT { get; set; }
        public static byte MMI_M_BP_MEASURED { get; set; }


        /// <summary>
        /// Initialise EVC-151 MMI_CONFIRMED_BRAKE_PERCENTAGE
        /// </summary>
        /// <param name="pool"></param>
        public static void Initialise(SignalPool pool)
        {
            _pool = pool;
            _pool.SITR.SMDStat.CCUO.ETCS1ConfirmedBrakePercentage.Value = 0x00;
            _pool.SITR.SMDCtrl.CCUO.ETCS1ConfirmedBrakePercentage.Value = 0x01;
        }

        public static void CheckTelegram()
        {
            // Reset telegram received flag in RTSim
            _pool.SITR.SMDStat.CCUO.ETCS1ConfirmedBrakePercentage.Value = 0x00;

            _checkResult = false;

            // Check if telegram received flag has been set. Allows 10 seconds.
            if (_pool.SITR.SMDStat.CCUO.ETCS1ConfirmedBrakePercentage.WaitForCondition(Is.Equal, 1, 10000, 100))
            {
                // Check all static fields

                _checkResult = _pool.SITR.CCUO.ETCS1ConfirmedBrakePercentage.MmiMBpOrigR.Value.Equals(MMI_M_BP_ORIG) &&
                               _pool.SITR.CCUO.ETCS1ConfirmedBrakePercentage.MmiMBpCurrentR.Value.Equals(
                                   MMI_M_BP_CURRENT) &&
                               _pool.SITR.CCUO.ETCS1ConfirmedBrakePercentage.MmiMBpMeasuredR.Value.Equals(
                                   MMI_M_BP_MEASURED);


                // Check if values match
                // If check passes
                if (_checkResult)
                {
                    _pool.TraceReport(BASE_STRING + Environment.NewLine +
                                      "MMI_M_BP_ORIG = " + MMI_M_BP_ORIG + Environment.NewLine +
                                      "MMI_M_BP_CURRENT = \"" + MMI_M_BP_CURRENT + "\"" + Environment.NewLine +
                                      "MMI_M_BP_MEASURED = \"" + MMI_M_BP_MEASURED + "\"" + Environment.NewLine +
                                      "Result: PASSED.");
                }
                // Else display the real value extracted from EVC-118
                else
                {
                    _pool.TraceError(BASE_STRING + Environment.NewLine +
                                     "MMI_M_BP_ORIG = \"" +
                                     _pool.SITR.CCUO.ETCS1ConfirmedBrakePercentage.MmiMBpOrigR.Value + "\"" +
                                     Environment.NewLine +
                                     "MMI_M_BP_CURRENT = \"" +
                                     _pool.SITR.CCUO.ETCS1ConfirmedBrakePercentage.MmiMBpCurrentR.Value + "\"" +
                                     Environment.NewLine +
                                     "MMI_M_BP_MEASURED = \"" +
                                     _pool.SITR.CCUO.ETCS1ConfirmedBrakePercentage.MmiMBpMeasuredR.Value + "\"" +
                                     Environment.NewLine +
                                     "Result: FAILED!");
                }
            }
            // Show generic DMI -> EVC telegram failure
            else
            {
                DmiExpectedResults.DMItoEVC_Telegram_Not_Received(_pool, BASE_STRING);
            }
        }
    }
}
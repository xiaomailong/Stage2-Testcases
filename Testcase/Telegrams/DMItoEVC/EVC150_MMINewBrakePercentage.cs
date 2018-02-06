using System;
using BT_CSB_Tools.SignalPoolGenerator.Signals.PdSignal.Misc;
using CL345;
using Testcase.DMITestCases;

namespace Testcase.Telegrams.DMItoEVC
{
    /// <summary>
    /// This packet shall be sent when the presented brake percentage data have been accepted.The content
    /// is the same as in packet EVC-50,“MMI_CURRENT_BRAKE_PERCENTAGE”. Thispacket is used in relation 
    /// with packets EVC-50, EVC-51 and EVC-151.
    /// </summary>
    public static class EVC150_MMINewBrakePercentage
    {
        private const string BASE_STRING = "DMI->ETCS: EVC-150 [MMI_NEW_BRAKE_PERCENTAGE]";

        private static TestcaseBase _pool;
        private static bool _checkResult;

        public static byte MMI_M_BP_ORIG { get; set; }
        public static byte MMI_M_BP_CURRENT { get; set; }
        public static byte MMI_M_BP_MEASURED { get; set; }


        /// <summary>
        /// Initialise EVC-150 MMI_NEW_BRAKE_PERCENTAGE
        /// </summary>
        /// <param name="pool"></param>
        public static void Initialise(TestcaseBase pool)
        {
            _pool = pool;
            _pool.SITR.SMDStat.CCUO.ETCS1NewBrakePercentage.Value = 0x00;
            _pool.SITR.SMDCtrl.CCUO.ETCS1NewBrakePercentage.Value = 0x01;
        }

        public static void CheckTelegram()
        {
            _checkResult = false;

            // Check if telegram received flag has been set. Allows 10 seconds.
            if (_pool.SITR.SMDStat.CCUO.ETCS1NewBrakePercentage.WaitForCondition(Is.Equal, 1, 10000, 100))
            {
                // Check all static fields

                _checkResult = _pool.SITR.CCUO.ETCS1NewBrakePercentage.MmiMBpOrig.Value.Equals(MMI_M_BP_ORIG) &&
                               _pool.SITR.CCUO.ETCS1NewBrakePercentage.MmiMBpCurrent.Value.Equals(MMI_M_BP_CURRENT) &&
                               _pool.SITR.CCUO.ETCS1NewBrakePercentage.MmiMBpMeasured.Value.Equals(MMI_M_BP_MEASURED);


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
                                     "MMI_M_BP_ORIG = \"" + _pool.SITR.CCUO.ETCS1NewBrakePercentage.MmiMBpOrig.Value +
                                     "\"" + Environment.NewLine +
                                     "MMI_M_BP_CURRENT = \"" +
                                     _pool.SITR.CCUO.ETCS1NewBrakePercentage.MmiMBpCurrent.Value + "\"" +
                                     Environment.NewLine +
                                     "MMI_M_BP_MEASURED = \"" +
                                     _pool.SITR.CCUO.ETCS1NewBrakePercentage.MmiMBpMeasured.Value + "\"" +
                                     Environment.NewLine +
                                     "Result: FAILED!");
                }
            }
            // Show generic DMI -> EVC telegram failure
            else
            {
                DmiExpectedResults.DMItoEVC_Telegram_Not_Received(_pool, BASE_STRING);
            }

            // Reset telegram received flag in RTSim
            _pool.SITR.SMDStat.CCUO.ETCS1NewBrakePercentage.Value = 0x00;
        }
    }
}